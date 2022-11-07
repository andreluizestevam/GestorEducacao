//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE COLABORADORES
// OBJETIVO: CADASTRAMENTO DE COLABORADORES FUNCIONAIS (Funcionários/Professores)
// DATA DE CRIAÇÃO: 
//-------------------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-------------------------------------------------------------------------------------
//  DATA       |  NOME DO PROGRAMADOR            | DESCRIÇÃO RESUMIDA
// ------------+---------------------------------+-------------------------------------
// 14/07/2016  | Tayguara Acioli  TA.14/07/2016  | Adicionei a flag de tecnico.
// 09/12/2016  | Alex Ribeiro                    | Alteração do label Departamento para Departamento/Local.

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.SqlClient;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7300_CtrlCadastralInforFunc._7310_CadastroColabor
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                txtDtSituacaoColab.Text = DateTime.Now.ToString("dd/MM/yyyy");

            if (!IsPostBack)
            {
                ///Define altura e largura da imagem do funcionário
                upImagemColab.ImagemLargura = 90;
                upImagemColab.ImagemAltura = 122;

                CarregaSituacao();
                CarregaUfs(ddlUfColab);
                CarregaUfs(ddlUfNacionalidadeColab);
                CarregaCidades();
                CarregaBairros();
                CarregaUfs(ddlUfTituloColab);
                CarregaUfs(ddlIdentidadeUFColab);
                CarregaUfs(ddlUfEntidProfi);
                ddlUfEntidProfi.Items.Insert(0, new ListItem("", ""));
                CarregaGrausInstrucao();
                CarregaTiposContrato();
                CarregaGrupoCBO();
                CarregaFuncoes();
                CarregaDepartamentos();
                CarregaTipoPonto();
                CarregaGrupoEspecialidade();
                CarregaTiposSalario();
                CarregaUnidadeFuncional();
                CarregaUnidadeContrato();
                CarregaClassificacoes();

                ddlUnidadeFuncionalColab.SelectedValue = ddlUnidadeContrato.SelectedValue = LoginAuxili.CO_EMP.ToString();
            }
        }

        /// <summary>
        /// Chamada do método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        /// <summary>
        /// Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        /// </summary>
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            //if (Page.IsValid)
            //{
            if (txtCargaHorariaColab.Text != "")
            {
                int intCH = int.Parse(txtCargaHorariaColab.Text);

                if (intCH > 9999)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Carga Horária do funcionário deve ser menor que 9999.");
                    return;
                }
            }

            //Valida se o usuário selecionou que o usuário pode fazer plantões, caso ele tenha selecionado, se torna obrigatório os campos de Qtd Plantões e Qtd Hr Descanso.
            if (chkPerPlantao.Checked)
            {
                if (txtQtdHrDescanPlantao.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Selecionado permite plantão, favor informar o Campo Qtd. Descanso entre Plantões!");
                    return;
                }
                if (txtQtdHrMaxPlantao.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Selecionado permite plantão, favor informar o Campo Qtd. Hora Plantão!");
                    return;
                }
            }

            //Valida para que, se o usuário tiver selecionado uma sigla para Entidade Profissional, ele tenha que preencher as informações complementares sobre
            if (!string.IsNullOrEmpty(ddlSiglaEntidProfi.SelectedValue))
            {
                if(string.IsNullOrEmpty(txtNrEntidProfi.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Selecionada a Sigla para Entidade Profissional, favor informar o Número do Registro.");
                    return;
                }

                if (string.IsNullOrEmpty(ddlUfEntidProfi.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Selecionada a Sigla para Entidade Profissional, favor informar a UF de Emissão do Registro.");
                    return;
                }

                if (string.IsNullOrEmpty(txtDtEmissEntidProfi.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Selecionada a Sigla para Entidade Profissional, favor informar a Data de Emissão do Registro.");
                    return;
                }
            }

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                //----------------> Verifica se existe ocorrência de funcionário para o CPF informado ( quando inserção )
                int ocorrCol = (from lTb03 in TB03_COLABOR.RetornaTodosRegistros()
                                where lTb03.NU_CPF_COL.Equals(txtCPFColab.Text.Replace("-", "").Replace(".", ""))
                                && lTb03.CO_EMP == LoginAuxili.CO_EMP
                                select new { lTb03.CO_COL }).Count();

                if (ocorrCol > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Funcionário já cadastrado no sistema.");
                    return;
                }
            }

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                int coCol = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCol);

                //----------------> Verifica se existe ocorrência de funcionário para o CPF informado e diferente de id de funcionário informado ( quando alteração )
                int ocorrCol = (from lTb03 in TB03_COLABOR.RetornaTodosRegistros()
                                where lTb03.NU_CPF_COL.Equals(txtCPFColab.Text.Replace("-", "").Replace(".", "")) && lTb03.CO_COL != coCol
                                && lTb03.CO_EMP == LoginAuxili.CO_EMP
                                select new { lTb03.CO_COL }).Count();

                if (ocorrCol > 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Funcionário já cadastrado no sistema.");
                    return;
                }
            }

            int codImagem = upImagemColab.GravaImagem();

            TB03_COLABOR tb03 = RetornaEntidade();

            int intRetorno = 0;
            decimal decimalRetorno = 0;
            double doubleRetorno = 0;
            DateTime now = DateTime.Now;

            if (tb03.CO_EMP.Equals(0) || tb03.CO_COL.Equals(0))
            {
                tb03.CO_EMP = int.Parse(ddlUnidadeFuncionalColab.SelectedValue);
                //----------------> Na inserção o CO_UNID e o CO_EMP serão iguais
                tb03.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(tb03.CO_EMP);
                tb03.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(tb03.CO_EMP);
                tb03.ORG_CODIGO_ORGAO = LoginAuxili.ORG_CODIGO_ORGAO;
            }

            string NomeApeliCol = "";

            if (String.IsNullOrEmpty(txtApelidoColab.Text))
            {
                var nomeCol = txtNomeColab.Text.Split(' ');
                NomeApeliCol = nomeCol[0] + (nomeCol.Length > 1 ? " " + nomeCol[1] : "");
            }
            else
                NomeApeliCol = txtApelidoColab.Text;

            tb03.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(codImagem);
            tb03.CO_EMP_UNID_CONT = int.Parse(ddlUnidadeContrato.SelectedValue);
            tb03.CO_MAT_COL = txtMatriculaColab.Text.Replace(".", "").Replace("-", "");
            tb03.FLA_PROFESSOR = ddlCategFuncColab.SelectedValue;
            tb03.NU_NIS_COL = decimal.TryParse(txtNISColab.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            tb03.NO_COL = txtNomeColab.Text.ToUpper();
            tb03.NO_APEL_COL = NomeApeliCol.ToUpper();
            tb03.CO_SEXO_COL = ddlSexoColab.SelectedValue;
            tb03.CO_TIPO_SANGUE_COL = ddlTpSangueFColab.SelectedValue != "" ? ddlTpSangueFColab.SelectedValue : null;
            tb03.CO_STATUS_SANGUE_COL = ddlStaSangueFColab.SelectedValue != "" ? ddlStaSangueFColab.SelectedValue : null;
            tb03.DT_NASC_COL = DateTime.Parse(txtDtNascColab.Text);
            tb03.CO_NACI_COL = ddlNacionalidadeColab.SelectedValue;
            tb03.DE_NATU_COL = txtNaturalidadeColab.Text != "" ? txtNaturalidadeColab.Text : null;
            tb03.CO_UF_NATU_COL = ddlUfNacionalidadeColab.SelectedValue != "" ? ddlUfNacionalidadeColab.SelectedValue : null;
            tb03.CO_ORIGEM_COL = ddlOrigem.SelectedValue;
            tb03.TP_RACA = ddlCorRacaColab.SelectedValue;
            tb03.TP_DEF = ddlDeficienciaColab.SelectedValue;
            tb03.CO_ESTADO_CIVIL = ddlEstadoCivilColab.SelectedValue != "" ? ddlEstadoCivilColab.SelectedValue : null;
            tb03.CO_INST = int.Parse(ddlGrauInstrucaoColab.SelectedValue);
            tb03.CO_EMAI_COL = txtEmailColab.Text;
            tb03.NU_TELE_RESI_COL = txtTelResidencialColab.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "");
            tb03.NU_TELE_CELU_COL = txtTelCelularColab.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "");
            tb03.NO_FUNC_MAE = txtNomeMaeColab.Text.Trim() == "" ? null : txtNomeMaeColab.Text.Trim();
            tb03.NO_FUNC_PAI = txtNomePaiColab.Text.Trim() == "" ? null : txtNomePaiColab.Text.Trim();
            tb03.CO_FLAG_CONJUG_FUNC = chkConjFunc.Checked ? "S" : "N";
            tb03.NO_CONJUG_COL = txtNomeConjugue.Text != "" ? txtNomeConjugue.Text : null;
            tb03.DT_NASC_CONJUG_COL = txtDtConju.Text == "" ? null : (DateTime?)Convert.ToDateTime(txtDtConju.Text);
            tb03.CO_SEXO_CONJUG_COL = ddlSexoConjug.SelectedValue;
            tb03.NU_CPF_CONJUG_COL = txtCPFConjug.Text.Replace("-", "").Replace(".", "");
            tb03.CO_RG_COL = txtIdentidadeColab.Text;
            tb03.DT_EMIS_RG_COL = DateTime.Parse(txtDtEmissaoColab.Text);
            tb03.CO_EMIS_RG_COL = txtOrgEmissorColab.Text;
            tb03.CO_ESTA_RG_COL = ddlIdentidadeUFColab.SelectedValue;
            tb03.NU_TIT_ELE = txtNumeroTituloColab.Text;
            tb03.NU_ZONA_ELE = txtZonaColab.Text;
            tb03.NU_SEC_ELE = txtSecaoColab.Text;
            tb03.CO_ESTA_RG_TIT = ddlUfTituloColab.SelectedValue;
            tb03.CO_CTPS_NUMERO = decimal.TryParse(txtNumeroCtpsColab.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            tb03.CO_CTPS_SERIE = decimal.TryParse(txtSerieCtpsColab.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            tb03.CO_CTPS_VIA = decimal.TryParse(txtViaColab.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            tb03.CO_CTPS_UF = ddlCtpsUFColab.SelectedValue;
            tb03.CO_CNH_NREG = decimal.TryParse(txtRegCnhColab.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            tb03.CO_CNH_NDOC = decimal.TryParse(txtDocCnhColab.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            tb03.CO_CNH_CATEG = txtCatCnhColab.Text.Trim() == "" ? null : txtCatCnhColab.Text.Trim();
            tb03.CO_CNH_VALID = txtValidadeCnhColab.Text == "" ? null : (DateTime?)Convert.ToDateTime(txtValidadeCnhColab.Text);
            tb03.NU_CPF_COL = txtCPFColab.Text.Replace("-", "").Replace(".", "");
            tb03.CO_PIS_PASEP = decimal.TryParse(txtPisPasepColab.Text.Replace("-", "").Replace(".", ""), out decimalRetorno) ? (decimal?)decimalRetorno : null;
            tb03.NU_PASSAPORTE = int.TryParse(txtPassaporteColab.Text, out intRetorno) ? (int?)intRetorno : null;
            tb03.NU_CARTAO_SAUDE = decimal.TryParse(txtConvSaudeColab.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            tb03.NU_CEP_ENDE_COL = txtCepColab.Text.Replace("-", "");
            tb03.DE_ENDE_COL = txtLogradouroColab.Text;
            tb03.NU_ENDE_COL = decimal.TryParse(txtNumeroColab.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
            tb03.DE_COMP_ENDE_COL = txtComplementoColab.Text;
            tb03.CO_BAIRRO = int.TryParse(ddlBairroColab.SelectedValue, out intRetorno) ? (int?)intRetorno : null;
            tb03.CO_CIDADE = int.TryParse(ddlCidadeColab.SelectedValue, out intRetorno) ? (int?)intRetorno : null;
            tb03.CO_ESTA_ENDE_COL = ddlUfColab.SelectedValue;
            tb03.CO_FLAG_RESID_PROPR = chkResPro.Checked ? "S" : "N";
            tb03.CO_TPCON = int.TryParse(ddlTipoContratoColab.SelectedValue, out intRetorno) ? (int?)intRetorno : null;
            tb03.DT_INIC_ATIV_COL = DateTime.Parse(txtDtAdmissaoColab.Text);
            tb03.DT_TERM_ATIV_COL = txtDtSaidaColab.Text == "" ? null : (DateTime?)Convert.ToDateTime(txtDtSaidaColab.Text);
            tb03.CO_DEPTO = int.TryParse(ddlDepartamentoColab.SelectedValue, out intRetorno) ? (int?)intRetorno : null;
            tb03.ID_TIPO_PONTO = int.TryParse(ddlTipoPontoColab.SelectedValue, out intRetorno) ? (int?)intRetorno : null;
            tb03.CO_ESPEC = int.TryParse(ddlEspecialidadeColab.SelectedValue, out intRetorno) ? (int?)intRetorno : null;
            tb03.CO_FUN = int.Parse(ddlFuncaoColab.SelectedValue);
            tb03.FL_MULTI_FREQU = ddlPermiMultiFrequ.SelectedValue;
            tb03.CO_TPCAL = int.TryParse(ddlSalarioTipoColab.SelectedValue, out intRetorno) ? (int?)intRetorno : null;
            tb03.NU_CARGA_HORARIA = int.Parse(txtCargaHorariaColab.Text);
            tb03.VL_SALAR_COL = double.TryParse(txtSalarioBaseColab.Text, out doubleRetorno) ? (double?)doubleRetorno : null;
            tb03.CO_EMAIL_FUNC_COL = txtEmailFuncColab.Text;
            tb03.NU_TELE_FUNC_COL = txtTelFuncColab.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "");
            tb03.DE_OBS_COL = txtObservacoesFuncColab.Text;
            tb03.CO_SITU_COL = ddlSituacaoColab.SelectedValue;
            tb03.TIPO_SITU = ddlTipoVinculoColab.SelectedValue;
            tb03.DT_SITU_COL = DateTime.Parse(txtDtSituacaoColab.Text);
            tb03.DT_ALT_REGISTRO = now;
            tb03.DT_CADA_COL = QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao) ? now : tb03.DT_CADA_COL;
            tb03.DE_FUNC_COL = txtFuncaoCol.Text;
            tb03.FL_PERM_PLANT = chkPerPlantao.Checked ? "S" : "N";
            tb03.FL_PARM_PLANT_MULTI = chkPerPlantaoMultiUnid.Checked ? "S" : "N";
            tb03.QT_HORAS_PLANT = (txtQtdHrMaxPlantao.Text != "" ? int.Parse(txtQtdHrMaxPlantao.Text) : (int?)null);
            tb03.QT_HORAS_DESCA = (txtQtdHrDescanPlantao.Text != "" ? int.Parse(txtQtdHrDescanPlantao.Text) : (int?)null);
            tb03.VL_HORA_PLANT = (!string.IsNullOrEmpty(txtVlPlantao.Text) ? decimal.Parse(txtVlPlantao.Text.Replace(",00", "").Replace(".","").ToString()) : (decimal?)null);
            tb03.IP_REGIS_PONTO = txtBatimentoPontoIP.Text;
            //Persiste informações sobre a Entidade Profissional
            tb03.CO_SIGLA_ENTID_PROFI = (!string.IsNullOrEmpty(ddlSiglaEntidProfi.SelectedValue) ? ddlSiglaEntidProfi.SelectedValue : null);
            tb03.NU_ENTID_PROFI = (!string.IsNullOrEmpty(txtNrEntidProfi.Text) ? txtNrEntidProfi.Text : null);
            tb03.CO_UF_ENTID_PROFI = (!string.IsNullOrEmpty(ddlUfEntidProfi.SelectedValue) ? ddlUfEntidProfi.SelectedValue : null);
            tb03.DT_EMISS_ENTID_PROFI = (!string.IsNullOrEmpty(txtDtEmissEntidProfi.Text) ? DateTime.Parse(txtDtEmissEntidProfi.Text) : (DateTime?)null);
            tb03.FL_PROFI_AVALI = chkAvaliador.Checked ? "S" : "N";
            //tb03.CO_CLASS_PROFI = (rdbClassProfissEnferm.Checked ? "E" : rdbClassProfissMedico.Checked ? "M" :
            //    rdbClassProfissOdonto.Checked ? "D" : rdbClassProfissOftalm.Checked ? "F" : rdbClassProfissPiscic.Checked ?
            //    "P" : rdbClassProfissFisiot.Checked ? "I" : rdbClassTerapeutaOcupacional.Checked ? "T" :
            //    rdbClassProfissFonoau.Checked ? "N"  : rdbClassProfissOutros.Checked ? "O" : null);
            tb03.CO_CLASS_PROFI = !String.IsNullOrEmpty(ddlClassFuncional.SelectedValue) ? ddlClassFuncional.SelectedValue : null;
            tb03.FL_TECNICO = chkTHD.Checked ? "S" : "N";
            tb03.FL_RESP_EXAME = chkRespExames.Checked ? "S" : "N";

            var connStr = "Data Source=user-pc;Initial Catalog=BDGC_XX_MODELO;Persist Security Info=True;User ID=sa;Password=@#!CJr;MultipleActiveResultSets=True";

            
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connStr);
            try
            {
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand(@"select CO_UF from TSN010_UF where DE_SIGLA = @UF", conn);
                cmd1.Parameters.AddWithValue("@UF", tb03.CO_ESTA_RG_COL);
                SqlDataReader reader = cmd1.ExecuteReader();
                int CO_RG_UF = 0;
                if (reader.Read())
                {
                    CO_RG_UF = reader.GetInt32(0);
                }
                else
                {
                    CO_RG_UF = -1;
                }
                
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(@"INSERT INTO [BDGC_XX_Modelo].[dbo].[TSN013_PROFISSIONAIS]
                   ([CO_CREDENCIADO]
                   ,[CO_SERVICOS_CREDENCIADO]
                   ,[NM_PROFISSIONAL]
                   ,[CO_APELIDO]
                   ,[DT_NASCIMENTO]
                   ,[CO_SEXO]
                   ,[CO_CPF]
                   ,[CO_RG_NUMERO]
                   ,[CO_RG_EMISSOR]
                   ,[CO_RG_UF]
                   ,[DE_EMAIL]
                   ,[DE_TELEFONE_CELULAR]
                   ,[DE_TELEFONE_FIXO]
                   ,[CO_CLASSE_ORGAO]
                   ,[CO_CLASSE_NUMERO]
                   ,[CO_CLASSE_UF]
                   ,[IM_FOTO_PROFISSIONAL_URL]
                   ,[CO_SITUACAO]
                   ,[DT_SITUACAO]) values(@CO_CREDENCIADO,@CO_SERVICOS_CREDENCIADO,@NM_PROFISSIONAL,@CO_APELIDO,@DT_NASCIMENTO,
                @CO_SEXO,@CO_CPF,@CO_RG_NUMERO,
                @CO_RG_EMISSOR,@CO_RG_UF,@DE_EMAIL,@DE_TELEFONE_CELULAR,@DE_TELEFONE_FIXO,
                @CO_CLASSE_ORGAO,@CO_CLASSE_NUMERO,@CO_CLASSE_UF,@IM_FOTO_PROFISSIONAL_URL,@CO_SITUACAO,@DT_SITUACAO)", conn);
                cmd.Parameters.AddWithValue("@CO_CREDENCIADO", tb03.TB25_EMPRESA.CO_EMP);
                cmd.Parameters.AddWithValue("@CO_SERVICOS_CREDENCIADO", 15);
                cmd.Parameters.AddWithValue("@NM_PROFISSIONAL", tb03.NO_COL);
                cmd.Parameters.AddWithValue("@CO_APELIDO", tb03.NO_APEL_COL);
                cmd.Parameters.AddWithValue("@DT_NASCIMENTO", tb03.DT_NASC_COL);
                cmd.Parameters.AddWithValue("@CO_SEXO", tb03.CO_SEXO_COL);
                cmd.Parameters.AddWithValue("@CO_CPF", tb03.NU_CPF_COL);
                cmd.Parameters.AddWithValue("@CO_RG_NUMERO", tb03.CO_RG_COL);
                cmd.Parameters.AddWithValue("@CO_RG_EMISSOR", tb03.CO_EMIS_RG_COL);
                cmd.Parameters.AddWithValue("@CO_RG_UF", CO_RG_UF);
                cmd.Parameters.AddWithValue("@DE_EMAIL", tb03.CO_EMAI_COL);
                cmd.Parameters.AddWithValue("@DE_TELEFONE_CELULAR", tb03.NU_TELE_CELU_COL);
                cmd.Parameters.AddWithValue("@DE_TELEFONE_FIXO", tb03.NU_TELE_RESI_COL);
                cmd.Parameters.AddWithValue("@CO_CLASSE_ORGAO", tb03.ORG_CODIGO_ORGAO);
                cmd.Parameters.AddWithValue("@CO_CLASSE_NUMERO", tb03.CO_RG_COL);
                cmd.Parameters.AddWithValue("@CO_CLASSE_UF", 7);
                cmd.Parameters.AddWithValue("@IM_FOTO_PROFISSIONAL_URL", "http://192.168.0.15:8088/thiago.jpeg");
                cmd.Parameters.AddWithValue("@CO_SITUACAO", tb03.CO_SITU_COL.Substring(0,1));
                cmd.Parameters.AddWithValue("@DT_SITUACAO", tb03.DT_SITU_COL);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write("Error Occurred:" + ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }

            CurrentPadraoCadastros.CurrentEntity = tb03;

            

            
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB03_COLABOR tb03 = RetornaEntidade();

            if (tb03 != null)
            {
                if (tb03.Image != null)
                    upImagemColab.CarregaImagem(tb03.Image.ImageId);
                else
                    upImagemColab.CarregaImagem(0);

                txtMatriculaColab.Text = tb03.CO_MAT_COL;
                ddlCategFuncColab.SelectedValue = tb03.FLA_PROFESSOR;
                txtNISColab.Text = tb03.NU_NIS_COL.ToString();
                txtNomeColab.Text = tb03.NO_COL.ToUpper();
                txtApelidoColab.Text = tb03.NO_APEL_COL.ToUpper();
                ddlSexoColab.SelectedValue = tb03.CO_SEXO_COL;
                ddlTpSangueFColab.SelectedValue = tb03.CO_TIPO_SANGUE_COL != null ? tb03.CO_TIPO_SANGUE_COL.Trim() : "";
                ddlStaSangueFColab.SelectedValue = tb03.CO_STATUS_SANGUE_COL != null ? tb03.CO_STATUS_SANGUE_COL : "";
                txtDtNascColab.Text = tb03.DT_NASC_COL.ToString("dd/MM/yyyy");
                ddlNacionalidadeColab.SelectedValue = tb03.CO_NACI_COL != null ? tb03.CO_NACI_COL.ToString() : "B";
                txtNaturalidadeColab.Text = tb03.DE_NATU_COL != null ? tb03.DE_NATU_COL.ToString() : "";
                ddlUfNacionalidadeColab.Enabled = ddlNacionalidadeColab.SelectedValue == "B";
                ddlUfNacionalidadeColab.SelectedValue = tb03.CO_UF_NATU_COL != null ? tb03.CO_UF_NATU_COL.ToString() : LoginAuxili.CO_UF_INSTITUICAO;
                ddlOrigem.SelectedValue = tb03.CO_ORIGEM_COL != null ? tb03.CO_ORIGEM_COL : "SR";
                ddlCorRacaColab.SelectedValue = tb03.TP_RACA;
                ddlDeficienciaColab.SelectedValue = tb03.TP_DEF;
                ddlEstadoCivilColab.SelectedValue = tb03.CO_ESTADO_CIVIL != null ? tb03.CO_ESTADO_CIVIL : "";
                ddlGrauInstrucaoColab.SelectedValue = tb03.CO_INST.ToString();
                txtEmailColab.Text = tb03.CO_EMAI_COL;
                txtTelResidencialColab.Text = tb03.NU_TELE_RESI_COL;
                txtTelCelularColab.Text = tb03.NU_TELE_CELU_COL;
                txtNomeMaeColab.Text = tb03.NO_FUNC_MAE;
                txtNomePaiColab.Text = tb03.NO_FUNC_PAI;
                chkConjFunc.Checked = tb03.CO_FLAG_CONJUG_FUNC == "S" ? true : false;
                txtNomeConjugue.Text = tb03.NO_CONJUG_COL != null ? tb03.NO_CONJUG_COL.ToString() : "";
                txtDtConju.Text = tb03.DT_NASC_CONJUG_COL != null ? ((DateTime)tb03.DT_NASC_CONJUG_COL).ToString("dd/MM/yyyy") : "";
                ddlSexoConjug.SelectedValue = tb03.CO_SEXO_CONJUG_COL != null ? tb03.CO_SEXO_CONJUG_COL.ToString() : "";
                txtCPFConjug.Text = tb03.NU_CPF_CONJUG_COL != null ? tb03.NU_CPF_CONJUG_COL.ToString() : "";
                txtIdentidadeColab.Text = tb03.CO_RG_COL;
                txtDtEmissaoColab.Text = tb03.DT_EMIS_RG_COL != null ? ((DateTime)tb03.DT_EMIS_RG_COL).ToString("dd/MM/yyyy") : "";
                txtOrgEmissorColab.Text = tb03.CO_EMIS_RG_COL;
                ddlIdentidadeUFColab.SelectedValue = tb03.CO_ESTA_RG_COL;
                txtNumeroTituloColab.Text = tb03.NU_TIT_ELE;
                txtZonaColab.Text = tb03.NU_ZONA_ELE;
                txtSecaoColab.Text = tb03.NU_SEC_ELE;
                ddlUfTituloColab.SelectedValue = tb03.CO_ESTA_RG_TIT;
                txtNumeroCtpsColab.Text = tb03.CO_CTPS_NUMERO.ToString();
                txtSerieCtpsColab.Text = tb03.CO_CTPS_SERIE.ToString();
                txtViaColab.Text = tb03.CO_CTPS_VIA.ToString();
                ddlCtpsUFColab.SelectedValue = tb03.CO_CTPS_UF;
                txtRegCnhColab.Text = tb03.CO_CNH_NREG.ToString();
                txtDocCnhColab.Text = tb03.CO_CNH_NDOC.ToString();
                txtCatCnhColab.Text = tb03.CO_CNH_CATEG;
                txtValidadeCnhColab.Text = tb03.CO_CNH_VALID != null ? ((DateTime)tb03.CO_CNH_VALID).ToString("dd/MM/yyyy") : "";
                txtCPFColab.Text = tb03.NU_CPF_COL;
                txtPisPasepColab.Text = tb03.CO_PIS_PASEP.ToString();
                txtPassaporteColab.Text = tb03.NU_PASSAPORTE != null ? tb03.NU_PASSAPORTE.ToString() : "";
                txtConvSaudeColab.Text = tb03.NU_CARTAO_SAUDE.ToString();
                txtCepColab.Text = tb03.NU_CEP_ENDE_COL;
                txtLogradouroColab.Text = tb03.DE_ENDE_COL;
                txtNumeroColab.Text = tb03.NU_ENDE_COL.ToString();
                txtComplementoColab.Text = tb03.DE_COMP_ENDE_COL;
                ddlUfColab.SelectedValue = tb03.CO_ESTA_ENDE_COL;
                CarregaCidades();
                ddlCidadeColab.SelectedValue = tb03.CO_CIDADE.ToString();
                CarregaBairros();
                ddlBairroColab.SelectedValue = tb03.CO_BAIRRO.ToString();
                chkResPro.Checked = tb03.CO_FLAG_RESID_PROPR == "S" ? true : false;
                ddlTipoContratoColab.SelectedValue = tb03.CO_TPCON.ToString();
                txtDtAdmissaoColab.Text = tb03.DT_INIC_ATIV_COL.ToString("dd/MM/yyyy");
                txtDtSaidaColab.Text = tb03.DT_TERM_ATIV_COL != null ? ((DateTime)tb03.DT_TERM_ATIV_COL).ToString("dd/MM/yyyy") : "";
                ddlUnidadeFuncionalColab.SelectedValue = tb03.CO_EMP.ToString();
                ddlUnidadeContrato.SelectedValue = tb03.CO_EMP_UNID_CONT.ToString();
                ddlDepartamentoColab.SelectedValue = tb03.CO_DEPTO.ToString();
                ddlTipoPontoColab.SelectedValue = tb03.ID_TIPO_PONTO.ToString();
                ddlEspecialidadeColab.SelectedValue = tb03.CO_ESPEC.ToString();
                txtFuncaoCol.Text = tb03.DE_FUNC_COL != null ? tb03.DE_FUNC_COL : "";
                chkPerPlantao.Checked = tb03.FL_PERM_PLANT == "S" ? true : false;
                chkPerPlantaoMultiUnid.Checked = tb03.FL_PARM_PLANT_MULTI == "S" ? true : false;
                txtQtdHrMaxPlantao.Text = tb03.QT_HORAS_PLANT.ToString();
                txtQtdHrDescanPlantao.Text = tb03.QT_HORAS_DESCA.ToString();
                txtBatimentoPontoIP.Text = tb03.IP_REGIS_PONTO;
                chkAvaliador.Checked = tb03.FL_PROFI_AVALI == "S" ? true : false;
                chkTHD.Checked = tb03.FL_TECNICO == "S" ? true : false;
                chkRespExames.Checked = tb03.FL_RESP_EXAME == "S" ? true : false;

                //Verifica se existe na tabela de funções, o código da Função cadastrada no objeto do colaborador
                var tb15 = (from iTb15 in TB15_FUNCAO.RetornaTodosRegistros()
                            where iTb15.CO_FUN == tb03.CO_FUN
                            select iTb15).FirstOrDefault();

                if (tb15 != null)
                {
                    tb15.TB316_CBO_GRUPOReference.Load();

                    ddlGrupoCBO.SelectedValue = tb15.TB316_CBO_GRUPO != null ? tb15.TB316_CBO_GRUPO.CO_CBO_GRUPO : "";
                    CarregaFuncoes();
                    ddlFuncaoColab.SelectedValue = tb03.CO_FUN.ToString();
                }

                //Verifica se existe na tabela de especialidades, o código da especialidade cadastrada no objeto do colaborador
                var tb63 = (from tb63o in TB63_ESPECIALIDADE.RetornaTodosRegistros()
                            where tb63o.CO_ESPECIALIDADE == tb03.CO_ESPEC
                            select tb63o).FirstOrDefault();

                if (tb63 != null)
                {
                    tb63.TB115_GRUPO_ESPECIALIDADEReference.Load();
                    ddlGrupoEspecialidadeColab.SelectedValue = (tb63.TB115_GRUPO_ESPECIALIDADE != null ? tb63.TB115_GRUPO_ESPECIALIDADE.ID_GRUPO_ESPECI.ToString() : "");
                    CarregaEspecialidades();
                    ddlEspecialidadeColab.SelectedValue = tb03.CO_ESPEC.ToString();
                }

                ddlPermiMultiFrequ.SelectedValue = tb03.FL_MULTI_FREQU != null ? tb03.FL_MULTI_FREQU : "N";
                ddlSalarioTipoColab.SelectedValue = tb03.CO_TPCAL.ToString();
                txtCargaHorariaColab.Text = tb03.NU_CARGA_HORARIA.ToString();
                txtSalarioBaseColab.Text = string.Format("{0:n}", tb03.VL_SALAR_COL);
                txtVlPlantao.Text = tb03.VL_HORA_PLANT.ToString();

                double valorBaseSalario;
                double.TryParse(txtSalarioBaseColab.Text, out valorBaseSalario);

                int cargaHorariaTrab;
                int.TryParse(txtCargaHorariaColab.Text, out cargaHorariaTrab);

                //------------> Pelo Tipo de salário, faz o cálculo do salário do funcionário
                switch (ddlSalarioTipoColab.SelectedValue)
                {
                    //----------------> Diário
                    case "1":
                        txtSalarioColab.Text = string.Format("{0:n}", valorBaseSalario * 30);
                        break;
                    //----------------> Semanal
                    case "2":
                        txtSalarioColab.Text = string.Format("{0:n}", valorBaseSalario * 4);
                        break;
                    //----------------> Mensal
                    case "3":
                        txtSalarioColab.Text = txtSalarioBaseColab.Text;
                        break;
                    //----------------> Hora
                    case "4":
                        txtSalarioColab.Text = string.Format("{0:n}", valorBaseSalario * cargaHorariaTrab);
                        break;
                    default:
                        txtSalarioColab.Text = "";
                        break;
                }
                txtEmailFuncColab.Text = tb03.CO_EMAIL_FUNC_COL;
                txtTelFuncColab.Text = tb03.NU_TELE_FUNC_COL;
                txtObservacoesFuncColab.Text = tb03.DE_OBS_COL;
                ddlSituacaoColab.SelectedValue = tb03.CO_SITU_COL;
                ddlTipoVinculoColab.SelectedValue = tb03.TIPO_SITU;
                txtDtSituacaoColab.Text = tb03.DT_SITU_COL.ToString("dd/MM/yyyy");

                //Entidade Profissional
                ddlSiglaEntidProfi.SelectedValue = tb03.CO_SIGLA_ENTID_PROFI;
                txtNrEntidProfi.Text = tb03.NU_ENTID_PROFI;
                ddlUfEntidProfi.SelectedValue = tb03.CO_UF_ENTID_PROFI;
                txtDtEmissEntidProfi.Text = tb03.DT_EMISS_ENTID_PROFI.ToString();

                divVlPla.Visible = chkPerPlantao.Checked;

                ddlClassFuncional.SelectedValue = tb03.CO_CLASS_PROFI;
                //Classificação profissional
                //switch(tb03.CO_CLASS_PROFI)
                //{
                //    //case "A":
                //    //    rdbClassAvaliador.Checked = true;
                //    //    break;
                //    case "E":
                //        rdbClassProfissEnferm.Checked = true;
                //        break;
                //    case "M":
                //        rdbClassProfissMedico.Checked = true;
                //        break;
                //    case "D":
                //        rdbClassProfissOdonto.Checked = true;
                //        break;
                //    case "F":
                //        rdbClassProfissOftalm.Checked = true;
                //        break;
                //    case "P":
                //        rdbClassProfissPiscic.Checked = true;
                //        break;
                //    case "I":
                //        rdbClassProfissFisiot.Checked = true;
                //        break;
                //    case "N":
                //        rdbClassProfissFonoau.Checked = true;
                //        break;
                //    case "T":
                //        rdbClassTerapeutaOcupacional.Checked = true;
                //        break;
                //    case "O":
                //        rdbClassProfissOutros.Checked = true;
                //        break;
                //}
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB03_COLABOR</returns>
        private TB03_COLABOR RetornaEntidade()
        {
            TB03_COLABOR tb03 = TB03_COLABOR.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoEmp), QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoCol));
            return (tb03 == null) ? new TB03_COLABOR() : tb03;
        }

        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de UFs
        /// </summary>
        /// <param name="ddl">DropDownList UF</param>
        private void CarregaUfs(DropDownList ddl)
        {
            ddl.DataSource = TB74_UF.RetornaTodosRegistros();
            ddl.DataTextField = "CODUF";
            ddl.DataValueField = "CODUF";
            ddl.DataBind();
            if (ddl == ddlUfNacionalidadeColab)
            {
                ddl.Items.Insert(0, "");
            }

            ddlCtpsUFColab.DataSource = TB74_UF.RetornaTodosRegistros();
            ddlCtpsUFColab.DataTextField = "CODUF";
            ddlCtpsUFColab.DataValueField = "CODUF";
            ddlCtpsUFColab.DataBind();
            ddlCtpsUFColab.Items.Insert(0, "");
        }

        /// <summary>
        /// Método que carrega o dropdown de Cidade
        /// </summary>
        private void CarregaCidades()
        {
            ddlCidadeColab.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUfColab.SelectedValue);

            ddlCidadeColab.DataTextField = "NO_CIDADE";
            ddlCidadeColab.DataValueField = "CO_CIDADE";
            ddlCidadeColab.DataBind();

            ddlCidadeColab.Enabled = ddlCidadeColab.Items.Count > 0;
            ddlCidadeColab.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Bairro
        /// </summary>
        private void CarregaBairros()
        {
            int coCidade = ddlCidadeColab.SelectedValue != "" ? int.Parse(ddlCidadeColab.SelectedValue) : 0;

            if (coCidade == 0)
            {
                ddlBairroColab.Enabled = false;
                ddlBairroColab.Items.Clear();
                return;
            }
            else
            {
                ddlBairroColab.DataSource = TB905_BAIRRO.RetornaPelaCidade(coCidade);

                ddlBairroColab.DataTextField = "NO_BAIRRO";
                ddlBairroColab.DataValueField = "CO_BAIRRO";
                ddlBairroColab.DataBind();

                ddlBairroColab.Enabled = ddlBairroColab.Items.Count > 0;
                ddlBairroColab.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Grau de Instrução
        /// </summary>
        private void CarregaGrausInstrucao()
        {
            ddlGrauInstrucaoColab.DataSource = TB18_GRAUINS.RetornaTodosRegistros().OrderBy(g => g.NU_CLASSI_IMPRES);

            ddlGrauInstrucaoColab.DataTextField = "NO_INST";
            ddlGrauInstrucaoColab.DataValueField = "CO_INST";
            ddlGrauInstrucaoColab.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipo de Contrato
        /// </summary>
        private void CarregaTiposContrato()
        {
            ddlTipoContratoColab.DataSource = TB20_TIPOCON.RetornaTodosRegistros();

            ddlTipoContratoColab.DataTextField = "NO_TPCON";
            ddlTipoContratoColab.DataValueField = "CO_TPCON";
            ddlTipoContratoColab.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Funcão
        /// </summary>
        private void CarregaFuncoes()
        {
            ddlFuncaoColab.DataSource = (from tb15 in TB15_FUNCAO.RetornaTodosRegistros()
                                         where tb15.TB316_CBO_GRUPO.CO_CBO_GRUPO == ddlGrupoCBO.SelectedValue
                                         select new { tb15.CO_FUN, NO_FUN = tb15.CO_CBO_FUN + " - " + tb15.NO_FUN, tb15.CO_CBO_FUN }).OrderBy(p => p.CO_CBO_FUN);

            ddlFuncaoColab.DataTextField = "NO_FUN";
            ddlFuncaoColab.DataValueField = "CO_FUN";
            ddlFuncaoColab.DataBind();

            ddlFuncaoColab.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Departamento
        /// </summary>
        private void CarregaDepartamentos()
        {
            ddlDepartamentoColab.DataSource = TB14_DEPTO.RetornaTodosRegistros().Where(p => p.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP);

            ddlDepartamentoColab.DataTextField = "CO_SIGLA_DEPTO";
            ddlDepartamentoColab.DataValueField = "CO_DEPTO";
            ddlDepartamentoColab.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipo de Ponto
        /// </summary>
        private void CarregaTipoPonto()
        {
            var lista = TB300_QUADRO_HORAR_FUNCI.RetornaTodosRegistros().Where(p => p.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP).ToList();

            var result = (from res in lista
                          select new
                          {
                              res.ID_QUADRO_HORAR_FUNCI,
                              Descricao = string.Format("{0} ({1} às {2})", res.CO_SIGLA_TIPO_PONTO, res.HR_LIMIT_ENTRA.Insert(2, ":"), res.HR_LIMIT_SAIDA_EXTRA.Insert(2, ":"))
                          }).OrderBy(o => o.Descricao);

            ddlTipoPontoColab.DataSource = result;

            ddlTipoPontoColab.DataTextField = "Descricao";
            ddlTipoPontoColab.DataValueField = "ID_QUADRO_HORAR_FUNCI";
            ddlTipoPontoColab.DataBind();

            ddlTipoPontoColab.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega todas os grupos de especialidades
        /// </summary>
        private void CarregaGrupoEspecialidade()
        {
            AuxiliCarregamentos.CarregaGrupoEspecialidade(ddlGrupoEspecialidadeColab, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Especialidades
        /// </summary>
        private void CarregaEspecialidades()
        {
            int coGrupo = (ddlGrupoEspecialidadeColab.SelectedValue != "" ? int.Parse(ddlGrupoEspecialidadeColab.SelectedValue) : 0);
            AuxiliCarregamentos.CarregaEspeciacialidades(ddlEspecialidadeColab, LoginAuxili.CO_EMP, coGrupo, false);

        }

        /// <summary>
        /// Método que carrega o dropdown de Tipo de Salário
        /// </summary>
        private void CarregaTiposSalario()
        {
            ddlSalarioTipoColab.DataSource = TB21_TIPOCAL.RetornaTodosRegistros();

            ddlSalarioTipoColab.DataTextField = "NO_TPCAL";
            ddlSalarioTipoColab.DataValueField = "CO_TPCAL";
            ddlSalarioTipoColab.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidade Funcional
        /// </summary>
        private void CarregaUnidadeFuncional()
        {
            ddlUnidadeFuncionalColab.Items.Clear();
            ddlUnidadeFuncionalColab.Items.AddRange(AuxiliBaseApoio.UnidadesDDL(LoginAuxili.IDEADMUSUARIO));
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidade de Contrato
        /// </summary>
        private void CarregaUnidadeContrato()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidadeContrato, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }

        /// <summary>
        /// Método que carrega o grupo CBO
        /// </summary>
        private void CarregaGrupoCBO()
        {
            ddlGrupoCBO.DataSource = (from tb316 in TB316_CBO_GRUPO.RetornaTodosRegistros()
                                      join tb15 in TB15_FUNCAO.RetornaTodosRegistros() on tb316.CO_CBO_GRUPO equals tb15.TB316_CBO_GRUPO.CO_CBO_GRUPO into resultado
                                      from tb15 in resultado.DefaultIfEmpty()
                                      where tb15 != null
                                      select new
                                      {
                                          tb316.CO_CBO_GRUPO,
                                          DESC = tb316.CO_CBO_GRUPO + "-" + tb316.DE_CBO_GRUPO
                                      }).OrderBy(e => e.DESC).DistinctBy(d => d.CO_CBO_GRUPO);

            ddlGrupoCBO.DataValueField = "CO_CBO_GRUPO";
            ddlGrupoCBO.DataTextField = "DESC";
            ddlGrupoCBO.DataBind();

            ddlGrupoCBO.Items.Insert(0, new ListItem("", ""));
        }
        /// <summary>
        /// Carrega a situação de colaboradores
        /// </summary>
        private void CarregaSituacao()
        {
            ddlSituacaoColab.Items.Clear();
            ddlSituacaoColab.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(statusColaborador.ResourceManager));
        }

        /// <summary>
        /// Carrega as Classificações Funcionais
        /// </summary>
        private void CarregaClassificacoes()
        {
            AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassFuncional, false, LoginAuxili.CO_EMP, AuxiliCarregamentos.ETiposClassificacoes.agendamento);
        }

        #endregion

        #region Validadores

        //====> Validador de CPF do Funcionário
        protected void cvValidaCPF(object source, ServerValidateEventArgs e)
        {
            string cpf = e.Value.Replace(".", "").Replace("-", "");
            e.IsValid = AuxiliValidacao.ValidaCpf(cpf);

            AuxiliPagina.EnvioMensagemErro(this, MensagensErro.CPFInvalido);
        }

        //====> Validador de Matrícula do Funcionário
        protected void cvValidaMatricula(object source, ServerValidateEventArgs e)
        {
            int ocorMatricula = (from tb03 in TB03_COLABOR.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                 where tb03.CO_MAT_COL.Equals(e.Value.Replace(".", "").Replace("-", ""))
                                 select new { tb03.CO_COL }).Count();

            if (ocorMatricula > 0 && QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                e.IsValid = false;
            else
                e.IsValid = true;
        }
        #endregion

        //====> Verifica se o CEP informado tem endereço cadastrado, se sim, completa os campos de endereço com o respectivo resultado
        protected void btnPesquisarCepColab_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCepColab.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCepColab.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where(c => c.CO_CEP == numCep).FirstOrDefault();

                if (tb235 != null)
                {
                    txtLogradouroColab.Text = tb235.NO_ENDER_CEP;
                    tb235.TB905_BAIRROReference.Load();
                    ddlUfColab.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    CarregaCidades();
                    ddlCidadeColab.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    CarregaBairros();
                    ddlBairroColab.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                }
                else
                {
                    txtLogradouroColab.Text = ddlBairroColab.SelectedValue = ddlCidadeColab.SelectedValue = "";
                    ddlUfColab.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                    AuxiliPagina.EnvioMensagemErro(this.Page, "CEP não encontrado");
                    return;
                }
            }
        }

        protected void ddlUf_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades();
            CarregaBairros();
        }

        protected void ddlCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairros();
        }

        protected void ddlNacionalidadeColab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNacionalidadeColab.SelectedValue == "B")
            {
                ddlUfNacionalidadeColab.Enabled = true;
            }
            else
            {
                ddlUfNacionalidadeColab.Enabled = false;
                ddlUfNacionalidadeColab.SelectedValue = "";
            }
        }

        protected void ddlGrupoCBO_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFuncoes();
        }

        protected void ddlGrupoEspecialidadeColab_SelectedIndexChange(object sender, EventArgs e)
        {
            CarregaEspecialidades();
        }

        protected void chkPerPlantao_OnCheckedChanged(object sender, EventArgs e)
        {
            divVlPla.Visible = chkPerPlantao.Checked;
        }
    }
}
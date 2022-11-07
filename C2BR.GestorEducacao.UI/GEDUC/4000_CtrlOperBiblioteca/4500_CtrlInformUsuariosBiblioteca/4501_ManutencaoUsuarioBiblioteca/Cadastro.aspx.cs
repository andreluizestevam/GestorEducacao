//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE BIBLIOTECA
// SUBMÓDULO: MANUTENÇÃO DE USUÁRIOS DE BIBLIOTECA
// OBJETIVO: MANUTENÇÃO DE USUÁRIO DE BIBLIOTECA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4500_CtrlInformUsuariosBiblioteca.F4501_ManutencaoUsuarioBiblioteca
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
            if (IsPostBack) return;

            if (QueryStringAuxili.OperacaoCorrenteQueryString == QueryStrings.OperacaoInsercao)
            {
                HabilitaControles(false);
                ddlTipo.Enabled = ddlUnidade.Enabled = true;
                ddlNome.Visible = liUnidade.Visible = txtNumeroControle.Enabled = txtDtSituacao.Enabled = false;
                txtDtSituacao.Text = DateTime.Now.ToString("dd/MM/yyyy");                
            }

            upImagem.ImagemLargura = 66;
            CarregaUnidades();
            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
            CarregaUfs(ddlUf);
            CarregaCidades();
            CarregaBairros();
            CarregaUfs(ddlUfTitulo);
            CarregaUfs(ddlIdentidadeUF);
            CarregaGrausInstrucao();
            CarregaCursosFormacao();
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (!Page.IsValid || !ValidaCampos()) return;

            int imageId = upImagem.GravaImagem();

            TB205_USUARIO_BIBLIOT tb205 = RetornaEntidade();

            int intRetorno = 0;
            decimal decimalRetorno = 0;
            DateTime dataAtual = DateTime.Now;

            if (tb205 == null)
            {
                if (ddlTipo.SelectedValue == "A")
                {
                    int coAlu = ddlNome.SelectedValue != "" ? int.Parse(ddlNome.SelectedValue) : 0;
                    
                    var verifAluno = from lTb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                     where lTb205.TB07_ALUNO.CO_ALU == coAlu
                                     select new { lTb205.CO_USUARIO_BIBLIOT };

                    if (verifAluno.Count() > 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Já existe usuário cadastrado com os dados informados.");
                        return;
                    }
                }
                else if (ddlTipo.SelectedValue == "F" || ddlTipo.SelectedValue == "P")
                {
                    int coCol = ddlNome.SelectedValue != "" ? int.Parse(ddlNome.SelectedValue) : 0;

                    var verifFunc = from lTb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                    where lTb205.TB03_COLABOR.CO_COL == coCol
                                    select new { lTb205.CO_USUARIO_BIBLIOT };

                    if (verifFunc.Count() > 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Já existe usuário cadastrado com os dados informados.");
                        return;
                    }
                }
                else if (ddlTipo.SelectedValue == "O")
                {
                    string strCpfUsu = txtCPF.Text.Replace("-", "").Replace(".", "");

                    var verifUsu = from lTb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                   where lTb205.NU_CPF_USU_BIB == strCpfUsu
                                   select new { lTb205.CO_USUARIO_BIBLIOT };

                    if (verifUsu.Count() > 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Já existe usuário cadastrado com os dados informados.");
                        return;
                    }
                }

                tb205 = new TB205_USUARIO_BIBLIOT();
                tb205.ORG_CODIGO_ORGAO = LoginAuxili.ORG_CODIGO_ORGAO;
                tb205.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
            }

//--------> Se for Aluno
            if (ddlTipo.SelectedValue == "A")
            {
                var refAluno = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddlNome.SelectedValue));
                refAluno.TB25_EMPRESA1Reference.Load();

                tb205.TB07_ALUNO = refAluno;
                tb205.TB25_EMPRESA = refAluno.TB25_EMPRESA1;

                tb205.NO_USU_BIB = tb205.TB07_ALUNO.NO_ALU;
                tb205.DT_NASC_USU_BIB = tb205.TB07_ALUNO.DT_NASC_ALU.Value;
                tb205.CO_SEXO_USU_BIB = tb205.TB07_ALUNO.CO_SEXO_ALU;
            }
//--------> Se for Funcionário
            else if (ddlTipo.SelectedValue == "F" || ddlTipo.SelectedValue == "P")
            {
                var refColabor = TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlNome.SelectedValue));
                refColabor.TB25_EMPRESA1Reference.Load();

                tb205.TB03_COLABOR = refColabor;
                tb205.TB25_EMPRESA = refColabor.TB25_EMPRESA1;

                tb205.NO_USU_BIB = tb205.TB03_COLABOR.NO_COL;
                tb205.DT_NASC_USU_BIB = tb205.TB03_COLABOR.DT_NASC_COL;
                tb205.CO_SEXO_USU_BIB = tb205.TB03_COLABOR.CO_SEXO_COL;
            }
//--------> Se for Outro
            else if (ddlTipo.SelectedValue == "O")
            {
                tb205.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                tb205.NO_USU_BIB = txtNome.Text;
                tb205.NO_APEL_USU_BIB = txtApelido.Text;
                tb205.CO_SEXO_USU_BIB = ddlSexo.SelectedValue; 
                tb205.DT_NASC_USU_BIB = DateTime.Parse(txtDtNasc.Text);
                tb205.CO_ESTADO_CIVIL = ddlEstadoCivil.SelectedValue;
                tb205.TP_DEF = ddlDeficiencia.SelectedValue;
                tb205.TB18_GRAUINS = TB18_GRAUINS.RetornaPelaChavePrimaria(int.Parse(ddlGrauInstrucao.SelectedValue));
                tb205.DE_ENDE_USU_BIB = txtLogradouro.Text;
                tb205.NU_ENDE_USU_BIB = decimal.TryParse(txtNumero.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                tb205.DE_COMP_ENDE_USU_BIB = txtComplemento.Text;
                tb205.TB904_CIDADE = int.TryParse(ddlCidade.SelectedValue, out intRetorno) ? TB904_CIDADE.RetornaPelaChavePrimaria(intRetorno) : null;
                tb205.CO_BAIRRO = int.TryParse(ddlBairro.SelectedValue, out intRetorno) ? (int?)intRetorno : null;
                tb205.CO_ESTA_ENDE_USU_BIB = ddlUf.SelectedValue;
                tb205.NU_CEP_ENDE_USU_BIB = txtCep.Text.Replace("-", "");
                tb205.NU_TELE_RESI_USU_BIB = txtTelResidencial.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "");
                tb205.NU_TELE_CELU_USU_BIB = txtTelCelular.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "");
                tb205.CO_CTPS_NUMERO = decimal.TryParse(txtNumeroCtps.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                tb205.CO_CTPS_SERIE = decimal.TryParse(txtSerieCtps.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                tb205.CO_CTPS_VIA = decimal.TryParse(txtVia.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                tb205.CO_CTPS_UF = ddlCtpsUF.SelectedValue;
                tb205.CO_RG_USU_BIB = txtIdentidade.Text;
                tb205.DT_EMIS_RG_USU_BIB = DateTime.Parse(txtDtEmissao.Text);
                tb205.CO_EMIS_RG_USU_BIB = txtOrgEmissor.Text;
                tb205.CO_ESTA_RG_USU_BIB = ddlIdentidadeUF.SelectedValue;
                tb205.NU_CPF_USU_BIB = txtCPF.Text.Replace("-", "").Replace(".", "");
                tb205.NU_TIT_ELE_USU_BIB = txtNumeroTitulo.Text;
                tb205.NU_ZONA_ELE_USU_BIB = txtZona.Text;
                tb205.NU_SEC_ELE_USU_BIB = txtSecao.Text;
                tb205.CO_ESTA_RG_TIT_USU_BIB = ddlUfTitulo.SelectedValue;
                tb205.CO_CNH_NREG = decimal.TryParse(txtRegCnh.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                tb205.CO_CNH_NDOC = decimal.TryParse(txtDocCnh.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                tb205.CO_CNH_CATEG = txtCatCnh.Text.Trim() != "" ? txtCatCnh.Text.Trim() : null;
                tb205.CO_CNH_VALID = txtValidadeCnh.Text != "" ? (DateTime?)Convert.ToDateTime(txtValidadeCnh.Text) : null;
                tb205.NO_USU_BIB_MAE = txtNomeMae.Text.Trim() != "" ? txtNomeMae.Text.Trim() : null;
                tb205.NO_USU_BIB_PAI = txtNomePai.Text.Trim() != "" ? txtNomePai.Text.Trim() : null;
                tb205.DE_DEPTO = txtDepartamento.Text;
                tb205.DE_FUN = txtFuncao.Text;
                tb205.DT_INIC_ATIV_USU_BIB = txtDtAdmissao.Text != "" ? (DateTime?)DateTime.Parse(txtDtAdmissao.Text) : null;
                tb205.TB100_ESPECIALIZACAO = int.TryParse(ddlCursoFormacao.SelectedValue, out intRetorno) ? TB100_ESPECIALIZACAO.RetornaPeloCoEspec(intRetorno) : null;
                tb205.CO_EMAI_USU_BIB_EMP = txtEmail.Text;
                tb205.NU_TELE_EMP_USU_BIB = txtTelefoneComercial.Text != "" ? txtTelefoneComercial.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                tb205.NU_RAMAL_EMP_USU_BIB = int.TryParse(txtRamalComercial.Text, out intRetorno) ? (int?)intRetorno : null;
                tb205.CO_SITU_USU_BIB = ddlSituacao.SelectedValue;
                tb205.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(imageId);
            }

            tb205.TP_USU_BIB = ddlTipo.SelectedValue;
            tb205.DT_ALT_REGISTRO = dataAtual;
            tb205.DT_CADA_USU_BIB = dataAtual;
            tb205.CO_SITU_USU_BIB = ddlSituacao.SelectedValue;
            tb205.DT_SITU_USU_BIB = dataAtual;

            CurrentPadraoCadastros.CurrentEntity = tb205;
        }        
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB205_USUARIO_BIBLIOT tb205 = RetornaEntidade();

            if (tb205 != null)
            {
                tb205.TB904_CIDADEReference.Load();
                tb205.TB18_GRAUINSReference.Load();
                tb205.TB100_ESPECIALIZACAOReference.Load();
                tb205.TB07_ALUNOReference.Load();

                if (tb205.TB07_ALUNO != null)
                    tb205.TB07_ALUNO.TB25_EMPRESA1Reference.Load();                
                tb205.TB03_COLABORReference.Load();
                if (tb205.TB03_COLABOR != null)
                    tb205.TB03_COLABOR.TB25_EMPRESA1Reference.Load();                
                ddlTipo.SelectedValue = tb205.TP_USU_BIB;

                if (ddlTipo.SelectedValue == "A")
                    ddlUnidade.SelectedValue = tb205.TB07_ALUNO.TB25_EMPRESA1.CO_EMP.ToString();
                else if (ddlTipo.SelectedValue == "P" || ddlTipo.SelectedValue == "F")
                    ddlUnidade.SelectedValue = tb205.TB03_COLABOR.TB25_EMPRESA1.CO_EMP.ToString();

                AlteraUnidadeOuTipo();

                if (ddlTipo.SelectedValue == "A")
                {
                    tb205.TB07_ALUNO.ImageReference.Load();

                    if (tb205.TB07_ALUNO.Image != null)
                        upImagem.CarregaImagem(tb205.TB07_ALUNO.Image.ImageId);
                    else
                        upImagem.CarregaImagem(0);

                    ddlNome.SelectedValue = tb205.TB07_ALUNO.CO_ALU.ToString();
                }
                else if (ddlTipo.SelectedValue == "P" || ddlTipo.SelectedValue == "F")
                {
                    tb205.TB03_COLABOR.ImageReference.Load();

                    if (tb205.TB03_COLABOR.Image != null)
                        upImagem.CarregaImagem(tb205.TB03_COLABOR.Image.ImageId);
                    else
                        upImagem.CarregaImagem(0);

                    ddlNome.SelectedValue = tb205.TB03_COLABOR.CO_COL.ToString();
                }
                else if (ddlTipo.SelectedValue == "O")
                {
                    if (tb205.Image != null)
                        upImagem.CarregaImagem(tb205.Image.ImageId);
                    else
                        upImagem.CarregaImagem(0);

                    txtNome.Text = tb205.NO_USU_BIB_PAI;
                    txtApelido.Text = tb205.NO_APEL_USU_BIB;
                    txtCep.Text = tb205.NU_CEP_ENDE_USU_BIB;
                    txtComplemento.Text = tb205.DE_COMP_ENDE_USU_BIB;
                    txtCPF.Text = tb205.NU_CPF_USU_BIB;
                    txtDtAdmissao.Text = tb205.DT_INIC_ATIV_USU_BIB.HasValue ? tb205.DT_INIC_ATIV_USU_BIB.Value.ToString("dd/MM/yyyy") : "";
                    txtDtEmissao.Text = tb205.DT_EMIS_RG_USU_BIB.HasValue ? tb205.DT_EMIS_RG_USU_BIB.Value.ToString("dd/MM/yyyy") : "";
                    txtDtNasc.Text = tb205.DT_NASC_USU_BIB.ToString("dd/MM/yyyy");
                    txtEmail.Text = tb205.CO_EMAI_USU_BIB_EMP;
                    txtIdentidade.Text = tb205.CO_RG_USU_BIB;
                    txtLogradouro.Text = tb205.DE_ENDE_USU_BIB;
                    txtNome.Text = tb205.NO_USU_BIB;
                    txtNumero.Text = tb205.NU_ENDE_USU_BIB.ToString();
                    txtNumeroTitulo.Text = tb205.NU_TIT_ELE_USU_BIB;
                    txtOrgEmissor.Text = tb205.CO_EMIS_RG_USU_BIB;
                    txtSecao.Text = tb205.NU_SEC_ELE_USU_BIB;
                    txtTelCelular.Text = tb205.NU_TELE_CELU_USU_BIB;
                    txtTelResidencial.Text = tb205.NU_TELE_RESI_USU_BIB;
                    txtTelefoneComercial.Text = tb205.NU_TELE_EMP_USU_BIB;
                    txtRamalComercial.Text = tb205.NU_RAMAL_EMP_USU_BIB.ToString();
                    txtZona.Text = tb205.NU_ZONA_ELE_USU_BIB;
                    ddlUf.SelectedValue = tb205.CO_ESTA_ENDE_USU_BIB;
                    CarregaCidades();
                    ddlCidade.SelectedValue = tb205.TB904_CIDADE.CO_CIDADE.ToString();
                    CarregaBairros();
                    ddlBairro.SelectedValue = tb205.CO_BAIRRO.ToString();
                    ddlGrauInstrucao.SelectedValue = tb205.TB18_GRAUINS.CO_INST.ToString();
                    CarregaCursosFormacao();
                    ddlCursoFormacao.SelectedValue = tb205.TB100_ESPECIALIZACAO != null ? tb205.TB100_ESPECIALIZACAO.CO_ESPEC.ToString() : "";
                    ddlDeficiencia.SelectedValue = tb205.TP_DEF;
                    txtDepartamento.Text = tb205.DE_DEPTO;
                    ddlEstadoCivil.SelectedValue = tb205.CO_ESTADO_CIVIL;
                    txtFuncao.Text = tb205.DE_FUN;
                    ddlIdentidadeUF.SelectedValue = tb205.CO_ESTA_RG_USU_BIB;
                    ddlUfTitulo.SelectedValue = tb205.CO_ESTA_RG_TIT_USU_BIB;
                    txtNumeroCtps.Text = tb205.CO_CTPS_NUMERO.ToString();
                    txtSerieCtps.Text = tb205.CO_CTPS_SERIE.ToString();
                    txtVia.Text = tb205.CO_CTPS_VIA.ToString();
                    ddlCtpsUF.SelectedValue = tb205.CO_CTPS_UF;
                    txtRegCnh.Text = tb205.CO_CNH_NREG.ToString();
                    txtDocCnh.Text = tb205.CO_CNH_NDOC.ToString();
                    txtCatCnh.Text = tb205.CO_CNH_CATEG;
                    txtValidadeCnh.Text = tb205.CO_CNH_VALID != null ? tb205.CO_CNH_VALID.Value.ToString("dd/MM/yyyy") : "";
                    txtNomeMae.Text = tb205.NO_USU_BIB_MAE;
                    txtNomePai.Text = tb205.NO_USU_BIB_PAI;
                }

                txtNumeroControle.Text = tb205.ORG_CODIGO_ORGAO.ToString("000") + "." + tb205.CO_USUARIO_BIBLIOT.ToString("00000");
                ddlSituacao.SelectedValue = tb205.CO_SITU_USU_BIB;
                txtDtSituacao.Text = tb205.DT_SITU_USU_BIB.ToString("dd/MM/yyyy");
                ddlSexo.SelectedValue = tb205.CO_SEXO_USU_BIB;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB205_USUARIO_BIBLIOT</returns>
        private TB205_USUARIO_BIBLIOT RetornaEntidade()
        {
            return TB205_USUARIO_BIBLIOT.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }

        /// <summary>
        /// Método que controla visibilidade de acordo com a alteração da Unidade e Tipo de Usuário
        /// </summary>
        private void AlteraUnidadeOuTipo()
        {
            switch (ddlTipo.SelectedValue)
            {
                case "":
                    HabilitaControles(false);
                    liUnidade.Visible = txtNome.Visible = true;
                    ddlNome.Visible = false;                    
                    break;
                case "A":
                    HabilitaControles(false);
                    liUnidade.Visible = ddlNome.Visible = true;
                    txtNome.Visible = txtDtSituacao.Enabled = false;
                    CarregaAluno();
                    break;
                case "P":
                    HabilitaControles(false);
                    liUnidade.Visible = ddlNome.Visible = true;
                    txtNome.Visible = txtDtSituacao.Enabled = false;
                    CarregaProfessores();
                    break;
                case "F":
                    HabilitaControles(false);
                    liUnidade.Visible = ddlNome.Visible = true;
                    txtNome.Visible = txtDtSituacao.Enabled = false;
                    CarregaFuncionarios();
                    break;
                case "O":
                    HabilitaControles(true);
                    liUnidade.Visible = ddlNome.Visible = txtDtSituacao.Enabled = false;
                    txtNome.Visible = true;                    
                    break;
            }

            ddlNome.Enabled = txtNome.Enabled = ddlTipo.Enabled = ddlUnidade.Enabled = ddlSituacao.Enabled = true;
            txtNumeroControle.Enabled = false;
        }

        /// <summary>
        /// Método que habilita/desabilita controles do formulário
        /// </summary>
        /// <param name="enable">Boolean habilita</param>
        private void HabilitaControles(bool enable)
        {
            ControlCollection controls = this.Form.FindControl("content").Controls;

            foreach (WebControl control in controls.OfType<TextBox>())
                control.Enabled = enable;

            foreach (WebControl control in controls.OfType<DropDownList>())
                control.Enabled = enable;

            ddlBairro.Enabled = ddlCidade.Enabled = ddlUf.Enabled = enable;
        }

        #endregion

        #region Carregamento DropDown

//====> 
        /// <summary>
        /// Método que carrega o dropdown de UFs
        /// </summary>
        /// <param name="ddl">DropDown UF</param>
        private void CarregaUfs(DropDownList ddl)
        {
            ddl.DataSource = TB74_UF.RetornaTodosRegistros();

            ddl.DataTextField = "CODUF";
            ddl.DataValueField = "CODUF";
            ddl.DataBind();

            ddlCtpsUF.DataSource = TB74_UF.RetornaTodosRegistros();

            ddlCtpsUF.DataTextField = "CODUF";
            ddlCtpsUF.DataValueField = "CODUF";
            ddlCtpsUF.DataBind();

            ddlCtpsUF.Items.Insert(0, "");
        }

        /// <summary>
        /// Método que carrega o dropdown de Cidades
        /// </summary>
        private void CarregaCidades()
        {
            ddlCidade.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUf.SelectedValue);

            ddlCidade.DataTextField = "NO_CIDADE";
            ddlCidade.DataValueField = "CO_CIDADE";
            ddlCidade.DataBind();

            ddlCidade.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Bairro
        /// </summary>
        private void CarregaBairros()
        {
            int coCidade = ddlCidade.SelectedValue != "" ? int.Parse(ddlCidade.SelectedValue) : 0;

            ddlBairro.DataSource = TB905_BAIRRO.RetornaPelaCidade(coCidade);

            ddlBairro.DataTextField = "NO_BAIRRO";
            ddlBairro.DataValueField = "CO_BAIRRO";
            ddlBairro.DataBind();

            ddlBairro.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Grau de Instrução
        /// </summary>
        private void CarregaGrausInstrucao()
        {
            ddlGrauInstrucao.DataSource = TB18_GRAUINS.RetornaTodosRegistros().OrderBy( g => g.NU_CLASSI_IMPRES );

            ddlGrauInstrucao.DataTextField = "NO_INST";
            ddlGrauInstrucao.DataValueField = "CO_INST";
            ddlGrauInstrucao.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Cursos de Formação
        /// </summary>
        private void CarregaCursosFormacao()
        {
            string tipo = TB18_GRAUINS.RetornaPelaChavePrimaria(int.Parse(ddlGrauInstrucao.SelectedValue)).CO_SIGLA_INST;
            ddlCursoFormacao.DataSource = TB100_ESPECIALIZACAO.RetornaPeloTipo(tipo).OrderBy( e => e.DE_ESPEC );

            ddlCursoFormacao.DataTextField = "DE_ESPEC";
            ddlCursoFormacao.DataValueField = "CO_ESPEC";
            ddlCursoFormacao.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlNome.DataSource = (from tb07 in TB07_ALUNO.RetornaPelaUnid(coEmp)
                                  select new { tb07.NO_ALU, tb07.CO_ALU });

            ddlNome.DataTextField = "NO_ALU";
            ddlNome.DataValueField = "CO_ALU";
            ddlNome.DataBind();

            ddlNome.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Funcionários
        /// </summary>
        private void CarregaFuncionarios()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlNome.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                  where tb03.FLA_PROFESSOR == "N"
                                  select new { tb03.NO_COL, tb03.CO_COL });

            ddlNome.DataTextField = "NO_COL";
            ddlNome.DataValueField = "CO_COL";
            ddlNome.DataBind();

            ddlNome.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Professores
        /// </summary>
        private void CarregaProfessores()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlNome.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                  where tb03.FLA_PROFESSOR == "S"
                                  select new { tb03.NO_COL, tb03.CO_COL });

            ddlNome.DataTextField = "NO_COL";
            ddlNome.DataValueField = "CO_COL";
            ddlNome.DataBind();

            ddlNome.Items.Insert(0, new ListItem("", ""));
        }
        #endregion

        #region Validadores

        private bool ValidaCampos()
        {
            bool flagValido = true;

//--------> Se for Aluno
            if (ddlTipo.SelectedValue == "A")
            {
                ValidateItem(ddlUnidade.SelectedValue, "Unidade deve ser selecionada", ref flagValido);
                ValidateItem(ddlNome.SelectedValue, "Nome deve ser selecionado", ref flagValido);
            }
//--------> Se for Funcionário
            else if (ddlTipo.SelectedValue == "F" || ddlTipo.SelectedValue == "P")
            {
                ValidateItem(ddlUnidade.SelectedValue, "Unidade deve ser selecionada", ref flagValido);
                ValidateItem(ddlNome.SelectedValue, "Nome deve ser selecionado", ref flagValido);
            }
//--------> Se for Outro
            else if (ddlTipo.SelectedValue == "O")
            {
                ValidateItem(txtNome.Text, "Nome deve ser informado", ref flagValido);

                string strCpf = txtCPF.Text.Replace(".", "").Replace("-", "");
                if (strCpf != "" && !AuxiliValidacao.ValidaCpf(strCpf))
                {
                    AuxiliPagina.EnvioMensagemErro(this, MensagensErro.CPFInvalido);
                    flagValido = false;
                }

                ValidateItem(txtDtNasc.Text, "Data de Nascimento deve ser informada", ref flagValido);
                ValidateItem(txtCep.Text, "CEP deve ser informado", ref flagValido);
                ValidateItem(txtLogradouro.Text, "Logradouro deve ser informado", ref flagValido);
                ValidateItem(ddlSexo.SelectedValue, "Sexo deve ser informado", ref flagValido);
                ValidateItem(ddlUf.SelectedValue, "UF deve ser informada", ref flagValido);
                ValidateItem(ddlCidade.SelectedValue, "Cidade deve ser informada", ref flagValido);
                ValidateItem(ddlBairro.SelectedValue, "Bairro deve ser informado", ref flagValido);
                ValidateItem(txtIdentidade.Text, "Identidade deve ser informada", ref flagValido);
                ValidateItem(ddlIdentidadeUF.SelectedValue, "UF da Identidade deve ser informada", ref flagValido);
                ValidateItem(txtOrgEmissor.Text, "Órgão Emissor da Identidade deve ser informado", ref flagValido);
                ValidateItem(txtDtEmissao.Text, "Data de Emissão da Identidade deve ser informada", ref flagValido);
                ValidateItem(txtCPF.Text, "CPF deve ser informado", ref flagValido);
            }

            ValidateItem(ddlSituacao.Text, "Situação deve ser informada", ref flagValido);
            ValidateItem(txtDtSituacao.Text, "Data da Situação deve ser informada", ref flagValido);

            return flagValido;
        }

//====> Método que valida um determinado campo do formulário
        private void ValidateItem(string strValor, string strMensagemErro, ref bool flagValidacao)
        {
            if (strValor == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, strMensagemErro);
                flagValidacao = false;
            }
        }
        #endregion

        protected void ddlGrauInstrucao_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCursosFormacao();
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

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AlteraUnidadeOuTipo();
        }

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            AlteraUnidadeOuTipo();
        }
    }
}
//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE OCORRÊNCIAS DISCIPLINARES
// OBJETIVO: CADASTRAMENTO DE OCORRÊNCIAS DISCIPLINARES DE ALUNOS
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
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3640_CtrlOcorrenciasAlunos.F3641_CadastramentoOcorrDiscAluno
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
            if (!Page.IsPostBack)
            {
                CarregaUnidade();
                CarregaTipoOcorrencia();
                CarregaAlunos();
                CarregaFuncionarios(LoginAuxili.CO_EMP);
                CarregaCategorias();
                CarregaTiposOcorrencias(ddlCategoria.SelectedValue, ddlTipoOcorrencia.SelectedValue);

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    string dataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDataCadastro.Text = txtDataOcorrencia.Text = dataAtual;
                    txtHoraOcorr.Text = DateTime.Now.ToString("HH:mm");
                }
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            try
            {
                if (Page.IsValid)
                {
                    int idFlex = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
                    int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

                    int coRespOcorr = int.Parse(ddlResponsavel.SelectedValue);
                    var dadosResp = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                     where tb03.CO_COL == coRespOcorr
                                     select new { tb03.CO_EMP, tb03.CO_COL }).FirstOrDefault();

                    string dt = txtDataOcorrencia.Text;
                    //Prepara a data da ocorrência
                    DateTime datat = DateTime.Parse(dt).AddHours(int.Parse(txtHoraOcorr.Text.Substring(0, 2))).AddMinutes(int.Parse(txtHoraOcorr.Text.Substring(3, 2)));

                    TB191_OCORR_ALUNO tb191 = RetornaEntidade();

                    tb191.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                    tb191.ID_RECEB_OCORR = idFlex; // id do recebedor da ocorrência (Aluno, Responsável, Professor ou Funcionário)
                    tb191.CO_CATEG = ddlCategoria.SelectedValue; // flag que identifica a categoria como A, R, P ou F de acordo com o acima
                    tb191.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                    tb191.TB150_TIPO_OCORR = TB150_TIPO_OCORR.RetornaPelaChavePrimaria(ddlTipoOcorrencia.SelectedValue);
                    tb191.DT_OCORR = datat;
                    tb191.DE_OCORR = txtOcorrencia.Text;
                    tb191.DT_CADASTRO = DateTime.Parse(txtDataCadastro.Text);
                    tb191.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(dadosResp.CO_EMP, dadosResp.CO_COL);
                    tb191.FL_HOMOL_OCORR = ckHomologa.Checked ? "S" : "N";
                    tb191.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tb191.CO_EMP_CADAS = LoginAuxili.CO_UNID_FUNC;

                    tb191.TB44_MODULO = (!string.IsNullOrEmpty(ddlModalidade.SelectedValue) ? TB44_MODULO.RetornaPelaChavePrimaria(int.Parse(ddlModalidade.SelectedValue)) : null);
                    tb191.CO_CUR = (!string.IsNullOrEmpty(ddlCurso.SelectedValue) ? int.Parse(ddlCurso.SelectedValue) : (int?)null);
                    tb191.CO_TUR = (!string.IsNullOrEmpty(ddlTurma.SelectedValue) ? int.Parse(ddlTurma.SelectedValue) : (int?)null);
                    tb191.CO_ANO = (!string.IsNullOrEmpty(ddlAno.SelectedValue) ? ddlAno.SelectedValue : null);
                    tb191.DE_ACAO_TOMAD = (!string.IsNullOrEmpty(txtAcaoTomada.Text) ? txtAcaoTomada.Text : null);

                    //Caso tenha sido selecionado o tipo de ocorrência, salva o objeto correspondente
                    tb191.TBE196_OCORR_DISCI = (!string.IsNullOrEmpty(ddlTpOcorrTbxxx.SelectedValue) ? TBE196_OCORR_DISCI.RetornaPelaChavePrimaria(int.Parse(ddlTpOcorrTbxxx.SelectedValue)) : null);

                    //Prepara é concatena as informações para apresentar o código da ocorrência disciplinar
                    #region Prepara Código da Ocorrência

                    var re = (from tb000 in TB000_INSTITUICAO.RetornaTodosRegistros()
                              where tb000.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                              select new
                              {
                                  tb000.TB149_PARAM_INSTI
                              }).FirstOrDefault();

                    //Busca o número da próxima ocorrência no cadastro da instituição
                    string proxCod = "";
                    if (re != null)
                    {
                        if (re.TB149_PARAM_INSTI != null)
                        {
                            proxCod = re.TB149_PARAM_INSTI.NU_PROX_OCORR;
                        }
                    }
                    int codOcor = (!string.IsNullOrEmpty(proxCod) ? int.Parse(proxCod) : 0);
                    /*Padrão: OD + ano(2 digitos) + mes(2 digitos) + codigo incremental da tb000(5 digitos)*/
                    string CodigoOcorrencia = "OD" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + codOcor.ToString().PadLeft(5, '0');
                    tb191.CO_REGIS_OCORR = CodigoOcorrencia;

                    #region Salva o Próximo código de ocorrências

                    TB000_INSTITUICAO tb00 = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                    tb00.TB149_PARAM_INSTIReference.Load();
                    tb00.TB149_PARAM_INSTI.NU_PROX_OCORR = (codOcor + 1).ToString();
                    TB000_INSTITUICAO.SaveOrUpdate(tb00, true);

                    #endregion

                    #endregion

                    #region SMS

                    //Envia sms caso tenha sido selecionado para tal
                    if (ckEnviaSMS.Checked)
                    {
                        string cel = RetornaCelular(idFlex, ddlCategoria.SelectedValue);
                        bool sucess = EnviaSMS(DateTime.Parse(txtDataOcorrencia.Text), coEmp, ddlTipoOcorrencia.SelectedItem.Text, ddlCategoria.SelectedItem.Text, ddlCategoria.SelectedValue, cel, ddlAluno.SelectedItem.Text, idFlex);

                        //Se for aluno, além de enviar sms para o aluno em questão, envia também para o responsável
                        if (ddlCategoria.SelectedValue == "A")
                        {
                            string celResp = "";
                            var r = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                     where tb07.CO_ALU == idFlex
                                     select new { tb07.TB108_RESPONSAVEL }).FirstOrDefault();

                            //Coleta o número de celular do responsável
                            if (r != null)
                            {
                                if (r.TB108_RESPONSAVEL != null)
                                {
                                    celResp = r.TB108_RESPONSAVEL.NU_TELE_CELU_RESP;
                                }
                            }

                            EnviaSMS(DateTime.Parse(txtDataOcorrencia.Text), coEmp, ddlTipoOcorrencia.SelectedItem.Text, ddlCategoria.SelectedItem.Text, ddlCategoria.SelectedValue, celResp, ddlAluno.SelectedItem.Text, int.Parse(ddlAluno.SelectedValue));
                        }

                        tb191.FL_AVISO_SMS = (sucess ? "S" : "N"); //Salva se foi enviado sms apenas se o sms tiver sido enviado com Êxito
                    }
                    else
                        tb191.FL_AVISO_SMS = "N";

                    #endregion

                    CurrentPadraoCadastros.CurrentEntity = tb191;
                }
            }
            catch (Exception ex)
            {

                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
            }

        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB191_OCORR_ALUNO tb191 = RetornaEntidade();

            if (tb191 != null)
            {
                tb191.TB03_COLABORReference.Load();
                tb191.TB150_TIPO_OCORRReference.Load();
                tb191.TB44_MODULOReference.Load();
                tb191.TB03_COLABORReference.Load();
                tb191.TBE196_OCORR_DISCIReference.Load();
                tb191.TB25_EMPRESAReference.Load();

                ddlUnidade.SelectedValue = tb191.TB25_EMPRESA.CO_EMP.ToString();
                CarregaDadosFlex(tb191.CO_CATEG); //Carrega as opções de acordo com a categoria salva no objeto 
                ddlAluno.SelectedValue = tb191.ID_RECEB_OCORR.ToString(); //Seleciona o receptor da ocorrência 
                ddlUnidade.SelectedValue = tb191.TB03_COLABOR.CO_COL.ToString();
                ddlTipoOcorrencia.SelectedValue = tb191.TB150_TIPO_OCORR.CO_SIGL_OCORR;
                txtDataOcorrencia.Text = tb191.DT_OCORR.ToString("dd/MM/yyyy");
                txtHoraOcorr.Text = tb191.DT_OCORR.ToString("HH:mm");
                txtOcorrencia.Text = tb191.DE_OCORR;
                txtDataCadastro.Text = tb191.DT_CADASTRO.ToString("dd/MM/yyyy");
                ckHomologa.Checked = tb191.FL_HOMOL_OCORR == "S" ? true : false;
                ddlUnidade.Enabled = ddlAluno.Enabled = ddlCategoria.Enabled = false;
                txtCodigo.Text = tb191.CO_REGIS_OCORR;

                if (tb191.TBE196_OCORR_DISCI != null)
                    ddlTpOcorrTbxxx.SelectedValue = tb191.TBE196_OCORR_DISCI.ID_OCORR_DISCI.ToString();

                //Informações à serem carregadas quando o registro de ocorrência for de aluno
                if (tb191.CO_CATEG == "A")
                {
                    CarregaModalidades(tb191.ID_RECEB_OCORR.Value);
                    CarregaSeriesCursos(tb191.ID_RECEB_OCORR.Value);
                    CarregaTurmas(tb191.ID_RECEB_OCORR.Value);
                    CarregaAno(tb191.ID_RECEB_OCORR.Value);
                    ddlModalidade.SelectedValue = (tb191.TB44_MODULO != null ? tb191.TB44_MODULO.CO_MODU_CUR.ToString() : "");
                    ddlCurso.SelectedValue = (tb191.CO_CUR != null ? tb191.CO_CUR.ToString() : "");
                    ddlTurma.SelectedValue = (tb191.CO_TUR != null ? tb191.CO_TUR.ToString() : "");
                    ddlAno.SelectedValue = (!string.IsNullOrEmpty(tb191.CO_ANO) ? tb191.CO_ANO : "");
                    ddlResponsavel.SelectedValue = tb191.TB03_COLABOR.CO_COL.ToString();
                    txtAcaoTomada.Text = tb191.DE_ACAO_TOMAD;

                    liInfosAluno.Visible = true;
                }
                else
                    liInfosAluno.Visible = false;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB191_OCORR_ALUNO</returns>
        private TB191_OCORR_ALUNO RetornaEntidade()
        {
            TB191_OCORR_ALUNO tb191 = TB191_OCORR_ALUNO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb191 == null) ? new TB191_OCORR_ALUNO() : tb191;
        }

        /// <summary>
        /// Retorna o número de celular de acordo com a categoria recebida
        /// </summary>
        /// <returns></returns>
        private string RetornaCelular(int CO_RECEP, string CO_CATEG)
        {
            string s = "";
            switch (CO_CATEG)
            {
                case "A":
                    s = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == CO_RECEP).FirstOrDefault().NU_TELE_CELU_ALU;
                    break;
                case "R":
                    s = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(CO_RECEP).NU_TELE_CELU_RESP;
                    break;
                case "P":
                case "F":
                    s = TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == CO_RECEP).FirstOrDefault().NU_TELE_CELU_COL;
                    break;
            }
            return s;
        }

        /// <summary>
        /// Método responsável por enviar SMS caso a opção correspondente tenha sido selecionada
        /// </summary>
        private bool EnviaSMS(DateTime data, int CO_EMP, string NO_TIPO, string NO_CATEG, string CO_CATEG, string NU_CELULAR, string NO_ATOR, int CO_ATOR)
        {
            //***IMPORTANTE*** - O limite máximo de caracteres de acordo com a ZENVIA que é quem presta o serviço de envio,
            //é de 140 caracteres para NEXTEL e 150 para DEMAIS OPERADORAS
            string texto = txtMensagem.Text;
           // texto = "Registro de " + NO_TIPO.ToLower() + " lancado para o(a) " + NO_CATEG.ToLower() + " " + (NO_ATOR.Length > 45 ? NO_ATOR.Substring(0, 45) : NO_ATOR) + " no dia " + data.ToString("dd/MM/yy");
            int aux = texto.Length; // para auxiliar o desenvolvimento à saber qual quantidade de caracteres
            bool sucesso = false;

            //Envia a mensagem apenas se o número do celular for diferente de nulo
            if (!string.IsNullOrEmpty(NU_CELULAR))
            {
                var admUsuario = ADMUSUARIO.RetornaPelaChavePrimaria(LoginAuxili.IDEADMUSUARIO);
                string retorno = "";

                if (admUsuario.QT_SMS_MES_USR != null && admUsuario.QT_SMS_MAXIM_USR != null)
                {
                    if (admUsuario.QT_SMS_MAXIM_USR <= admUsuario.QT_SMS_MES_USR)
                    {
                        retorno = "Saldo do envio de SMS para outras pessoas ultrapassado.";
                        return sucesso;
                    }
                }

                if (!Page.IsValid)
                    return sucesso;
                try
                {
                    //Salva na tabela de mensagens enviadas, as informações pertinentes
                    TB249_MENSAG_SMS tb249 = new TB249_MENSAG_SMS();
                    tb249.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);
                    tb249.CO_RECEPT = CO_ATOR;
                    tb249.CO_EMP_RECEPT = CO_EMP;
                    tb249.NO_RECEPT_SMS = NO_ATOR;
                    tb249.DT_ENVIO_MENSAG_SMS = DateTime.Now;
                    tb249.DES_MENSAG_SMS = texto;
                    tb249.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                    string desLogin = LoginAuxili.DESLOGIN.Trim().Length > 15 ? LoginAuxili.DESLOGIN.Trim().Substring(0, 15) : LoginAuxili.DESLOGIN.Trim();

                    SMSAuxili.SMSRequestReturn sMSRequestReturn = SMSAuxili.EnvioSMS(desLogin, Extensoes.RemoveAcentuacoes(texto + "(" + desLogin + ")"),
                                                "55" + NU_CELULAR.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", ""),
                                                DateTime.Now.Ticks.ToString());

                    if ((int)sMSRequestReturn == 0)
                    {
                        admUsuario.QT_SMS_TOTAL_USR = admUsuario.QT_SMS_TOTAL_USR != null ? admUsuario.QT_SMS_TOTAL_USR + 1 : 1;
                        admUsuario.QT_SMS_MES_USR = admUsuario.QT_SMS_MES_USR != null ? admUsuario.QT_SMS_MES_USR + 1 : 1;
                        ADMUSUARIO.SaveOrUpdate(admUsuario, false);

                        tb249.FLA_SMS_SUCESS = "S";
                    }
                    else
                        tb249.FLA_SMS_SUCESS = "N";

                    tb249.CO_TP_CONTAT_SMS = CO_CATEG;

                    if ((int)sMSRequestReturn == 13)
                        retorno = "Número do destinatário está incompleto ou inválido.";
                    else if ((int)sMSRequestReturn == 80)
                        retorno = "Já foi enviada uma mensagem de sua conta com o mesmo identificador.";
                    else if ((int)sMSRequestReturn == 900)
                        retorno = "Erro de autenticação em account e/ou code.";
                    else if ((int)sMSRequestReturn == 990)
                        retorno = "Seu limite de segurança foi atingido. Contate nosso suporte para verificação/liberação.";
                    else if ((int)sMSRequestReturn == 998)
                        retorno = "Foi invocada uma operação inexistente.";
                    else if ((int)sMSRequestReturn == 999)
                        retorno = "Erro desconhecido. Contate nosso suporte.";


                    tb249.ID_MENSAG_OPERAD = (int)sMSRequestReturn;

                    if ((int)sMSRequestReturn == 0)
                    {
                        tb249.CO_STATUS = "E";
                        sucesso = true;
                    }
                    else
                    {
                        tb249.CO_STATUS = "N";
                        sucesso = false;
                    }

                    TB249_MENSAG_SMS.SaveOrUpdate(tb249, false);
                }
                catch (Exception)
                {
                    retorno = "Mensagem não foi enviada com sucesso.";
                }
                //GestorEntities.CurrentContext.SaveChanges();
            }
            return sucesso;
        }

        /// <summary>
        /// Carrega os funcionários cadastrados na unidade
        /// </summary>
        private void CarregaFuncionarios(int CO_EMP)
        {
            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       where tb03.CO_EMP == CO_EMP
                       select new { tb03.CO_COL, tb03.NO_COL }).ToList();

            ddlResponsavel.DataTextField = "NO_COL";
            ddlResponsavel.DataValueField = "CO_COL";
            ddlResponsavel.DataSource = res;
            ddlResponsavel.DataBind();

            //Garante que a opção existe antes de selecionar o item
            ListItem liBR = ddlResponsavel.Items.FindByValue(LoginAuxili.CO_COL.ToString());
            if (liBR != null)
                ddlResponsavel.SelectedValue = LoginAuxili.CO_COL.ToString();
        }

        /// <summary>
        /// Carrega todas os Anos onde o Aluno possui matrícula
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void CarregaAno(int CO_ALU)
        {
            var res = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                       where tb08.CO_ALU == CO_ALU
                       select new
                       {
                           tb08.CO_ANO_MES_MAT
                       }).Distinct().OrderBy(w => w.CO_ANO_MES_MAT).ToList();

            ddlAno.DataTextField = "CO_ANO_MES_MAT";
            ddlAno.DataValueField = "CO_ANO_MES_MAT";
            ddlAno.DataSource = res;
            ddlAno.DataBind();

            ddlAno.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Carrega todas as modalidades onde o aluno em questão possui matrícula
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void CarregaModalidades(int CO_ALU)
        {
            var res = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                       where tb08.CO_ALU == CO_ALU
                       select new
                       {
                           tb08.TB44_MODULO.CO_MODU_CUR,
                           tb08.TB44_MODULO.DE_MODU_CUR,
                       }).Distinct().OrderBy(w => w.DE_MODU_CUR).ToList();

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataSource = res;
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Carrega todos os cursos onde o aluno em questão possui matrícula
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void CarregaSeriesCursos(int CO_ALU)
        {
            var res = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                       join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                       where tb08.CO_ALU == CO_ALU
                       select new
                       {
                           tb01.CO_CUR,
                           tb01.NO_CUR,
                       }).Distinct().OrderBy(w => w.NO_CUR).ToList();

            ddlCurso.DataTextField = "NO_CUR";
            ddlCurso.DataValueField = "CO_CUR";
            ddlCurso.DataSource = res;
            ddlCurso.DataBind();

            ddlCurso.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Carrega todas as turmas onde o aluno em questão possui matrícula
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void CarregaTurmas(int CO_ALU)
        {
            var res = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                       join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb129.CO_TUR
                       where tb08.CO_ALU == CO_ALU
                       select new
                       {
                           tb129.NO_TURMA,
                           tb129.CO_TUR,
                       }).Distinct().OrderBy(w => w.NO_TURMA).ToList();

            ddlTurma.DataTextField = "NO_TURMA";
            ddlTurma.DataValueField = "CO_TUR";
            ddlTurma.DataSource = res;
            ddlTurma.DataBind();

            ddlTurma.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Carrega as categorias
        /// </summary>
        private void CarregaCategorias()
        {
            AuxiliCarregamentos.CarregaCategoriaOcorrencias(ddlCategoria, true, false);
        }

        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaPeloUsuario(LoginAuxili.CO_COL)
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Ocorrência
        /// </summary>
        private void CarregaTipoOcorrencia()
        {
            ddlTipoOcorrencia.DataSource = TB150_TIPO_OCORR.RetornaTodosRegistros().Where(p => p.TP_USU.Equals("A")).OrderBy(t => t.DE_TIPO_OCORR);

            ddlTipoOcorrencia.DataTextField = "DE_TIPO_OCORR";
            ddlTipoOcorrencia.DataValueField = "CO_SIGL_OCORR";
            ddlTipoOcorrencia.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        protected void CarregaAlunos()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlAluno.DataSource = (from tb07 in TB07_ALUNO.RetornaPelaUnid(coEmp)
                                   select new { tb07.NO_ALU, tb07.CO_ALU });

            ddlAluno.DataTextField = "NO_ALU";
            ddlAluno.DataValueField = "CO_ALU";
            ddlAluno.DataBind();

            ddlAluno.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega os tipos de ocorrências já salvas de acordo com a categoria recebida como parâmetro
        /// </summary>
        /// <param name="CO_CATEG"></param>
        private void CarregaTiposOcorrencias(string CO_CATEG, string SGL_TIPO)
        {
            var res = (from tbe196 in TBE196_OCORR_DISCI.RetornaTodosRegistros()
                       where tbe196.CO_CATEG == CO_CATEG
                       && tbe196.TB150_TIPO_OCORR.CO_SIGL_OCORR == SGL_TIPO
                       select new
                       {
                           tbe196.ID_OCORR_DISCI,
                           tbe196.DE_OCORR,
                       }).ToList();

            ddlTpOcorrTbxxx.DataTextField = "DE_OCORR";
            ddlTpOcorrTbxxx.DataValueField = "ID_OCORR_DISCI";
            ddlTpOcorrTbxxx.DataSource = res;
            ddlTpOcorrTbxxx.DataBind();

            ddlTpOcorrTbxxx.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Carrega os dados flexivelmente de acordo com o selecionado em categoria
        /// </summary>
        private void CarregaDadosFlex(string CATEG)
        {
            int coEmp = (!string.IsNullOrEmpty(ddlUnidade.SelectedValue) && ddlUnidade.SelectedValue != "0" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP);
            liInfosAluno.Visible = false; //esconde as informações do aluno
            switch (CATEG)
            {
                case "F":
                    AuxiliCarregamentos.CarregaFuncionarios(ddlAluno, coEmp, true);
                    lblFlex.Text = "Funcionário(a)";
                    break;
                case "P":
                    AuxiliCarregamentos.carregaProfessores(ddlAluno, coEmp, true, true);
                    lblFlex.Text = "Professor(a)";
                    break;
                case "R":
                    AuxiliCarregamentos.CarregaResponsaveis(ddlAluno, LoginAuxili.ORG_CODIGO_ORGAO, true);
                    lblFlex.Text = "Responsável";
                    break;
                case "A":
                default:
                    AuxiliCarregamentos.CarregaAlunosDaUnidade(ddlAluno, coEmp, true);
                    lblFlex.Text = "Aluno(a)";
                    liInfosAluno.Visible = true; //mostra as informações caso o a categoria selecionada seja o aluno
                    break;
            }

        }

        #endregion

        #region Funções de Campo

        protected void ddlCategoria_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDadosFlex(ddlCategoria.SelectedValue);
            CarregaTiposOcorrencias(ddlCategoria.SelectedValue, ddlTipoOcorrencia.SelectedValue);
        }

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAlunos();
            CarregaFuncionarios(!string.IsNullOrEmpty(ddlUnidade.SelectedValue) ? int.Parse(ddlUnidade.SelectedValue) : 0);
        }

        protected void ddlAluno_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlAluno.SelectedValue))
            {
                int coAlu = int.Parse(ddlAluno.SelectedValue);
                CarregaModalidades(coAlu);
                CarregaSeriesCursos(coAlu);
                CarregaTurmas(coAlu);
                CarregaAno(coAlu);
            }
        }

        protected void ddlTipoOcorrencia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTiposOcorrencias(ddlCategoria.SelectedValue, ddlTipoOcorrencia.SelectedValue);
        }

        #endregion
    }
}



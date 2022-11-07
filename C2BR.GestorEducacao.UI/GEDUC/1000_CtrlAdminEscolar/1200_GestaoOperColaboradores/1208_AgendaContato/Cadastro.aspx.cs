//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE COLABORADORES
// OBJETIVO: REGISTRO DE OCORRÊNCIAS FUNCIONAIS DE COLABORADORES
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Linq;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1208_AgendaContato
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
                CarregaUnidades();
                CarregaContato();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
                }
            }
        }

        /// <summary>
        /// Verifica o tipo da unidade e adequa os campos necessários
        /// </summary>
        private void VerificaTipoUnid()
        {
            switch (LoginAuxili.CO_TIPO_UNID)
            {
                case "PGS":
                    //Alterações do tipo
                    ddlTipoContato.Items.Clear();
                    ddlTipoContato.Items.Insert(0, new ListItem("Outro", "O"));
                    ddlTipoContato.Items.Insert(0, new ListItem("Responsável", "R"));
                    ddlTipoContato.Items.Insert(0, new ListItem("Profissional Saúde", "S"));
                    ddlTipoContato.Items.Insert(0, new ListItem("Paciente", "A"));
                    ddlTipoContato.Items.Insert(0, new ListItem("Funcionário", "F"));
                    break;
                case "PGE":
                default:
                    //Alterações do tipo
                    ddlTipoContato.Items.Clear();
                    ddlTipoContato.Items.Insert(0, new ListItem("Outro", "O"));
                    ddlTipoContato.Items.Insert(0, new ListItem("Responsável", "R"));
                    ddlTipoContato.Items.Insert(0, new ListItem("Professor(a)", "S"));
                    ddlTipoContato.Items.Insert(0, new ListItem("Aluno", "A"));
                    ddlTipoContato.Items.Insert(0, new ListItem("Funcionário", "F"));
                    break;
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (ddlTipoContato.SelectedValue == "O")
            {
                if (txtNomeContato.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Nome do Contato deve ser informado.");
                    return;
                }
            }
            else
            {
                if (ddlTipoContato.SelectedValue == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Contato deve ser informado.");
                    return;
                }
            }

            if ((txtTelCelulContato.Text == "") && (txtTelComerContato.Text == "") && (txtTelResidContato.Text == ""))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Um dos telefones deve ser informado.");
                return;
            }

            if (txtDataNascto.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data de Nascimento deve ser informada.");
                return;
            }

            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coContato = ddlContato.SelectedValue != "" ? int.Parse(ddlContato.SelectedValue) : 0;            

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                if (ddlTipoContato.SelectedValue == "O")
                {
                    DateTime dataNascto = DateTime.Parse(txtDataNascto.Text);
                    var ocoContat = (from iTb306 in TB306_AGEND_CONTATO.RetornaTodosRegistros()
                                     where iTb306.NO_CONTAT == txtNomeContato.Text && iTb306.DT_NASCTO_CONTAT == dataNascto
                                     && iTb306.CO_USUA_CONTAT == LoginAuxili.CO_COL
                                     select iTb306).FirstOrDefault();

                    if (ocoContat != null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Contato já cadastrado.");
                        return;
                    }
                }
                else if (ddlTipoContato.SelectedValue == "F" || ddlTipoContato.SelectedValue == "P")
                {
                    var ocoContat = (from iTb306 in TB306_AGEND_CONTATO.RetornaTodosRegistros()
                                     where iTb306.CO_CONTAT == coContato && (iTb306.TP_CONTAT.Equals("P") || iTb306.TP_CONTAT.Equals("F"))
                                     && iTb306.CO_USUA_CONTAT == LoginAuxili.CO_COL
                                     select new { iTb306.CO_CONTAT }).FirstOrDefault();

                    if (ocoContat != null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Contato já cadastrado.");
                        return;
                    }
                }
                else if (ddlTipoContato.SelectedValue == "A")
                {
                    var ocoContat = (from iTb306 in TB306_AGEND_CONTATO.RetornaTodosRegistros()
                                     where iTb306.CO_CONTAT == coContato && iTb306.TP_CONTAT.Equals("A")
                                     && iTb306.CO_USUA_CONTAT == LoginAuxili.CO_COL
                                     select new { iTb306.CO_CONTAT }).FirstOrDefault();

                    if (ocoContat != null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Contato já cadastrado.");
                        return;
                    }
                }
                else if (ddlTipoContato.SelectedValue == "R")
                {
                    var ocoContat = (from iTb306 in TB306_AGEND_CONTATO.RetornaTodosRegistros()
                                     where iTb306.CO_CONTAT == coContato && iTb306.TP_CONTAT.Equals("R")
                                     && iTb306.CO_USUA_CONTAT == LoginAuxili.CO_COL
                                     select new { iTb306.CO_CONTAT }).FirstOrDefault();

                    if (ocoContat != null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Contato já cadastrado.");
                        return;
                    }
                }
            }
            TB306_AGEND_CONTATO tb306 = RetornaEntidade();

            tb306.TP_CONTAT = ddlTipoContato.SelectedValue;

            if (ddlTipoContato.SelectedValue == "O")
            {
                tb306.CO_EMP_CONTAT = null;
                tb306.CO_CONTAT = null;
                tb306.NO_CONTAT = txtNomeContato.Text;
                tb306.NO_APELI_CONTAT = txtApeliContato.Text != "" ? txtApeliContato.Text : null;
                tb306.NU_TELE_CELU_CONTAT = txtTelCelulContato.Text != "" ? txtTelCelulContato.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                tb306.NU_TELE_COME_CONTAT = txtTelComerContato.Text != "" ? txtTelComerContato.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                tb306.NU_TELE_RESI_CONTAT = txtTelResidContato.Text != "" ? txtTelResidContato.Text.Replace("-", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "") : null;
                tb306.CO_SEXO_CONTAT = ddlSexoContato.SelectedValue;
                tb306.DE_EMAIL_CONTAT = txtEmailContato.Text != "" ? txtEmailContato.Text : null;
                tb306.DT_NASCTO_CONTAT = txtDataNascto.Text != "" ? (DateTime?)DateTime.Parse(txtDataNascto.Text) : null;
            }
            else if (ddlTipoContato.SelectedValue == "R")
            {
                tb306.CO_EMP_CONTAT = null;
                tb306.CO_CONTAT = coContato;
                tb306.NO_CONTAT = ddlContato.SelectedItem.ToString();
            }
            else
            {
                tb306.CO_EMP_CONTAT = coEmp;
                tb306.CO_CONTAT = coContato;
                tb306.NO_CONTAT = ddlContato.SelectedItem.ToString();
            }

            tb306.CO_USUA_CONTAT = LoginAuxili.CO_COL;

            CurrentPadraoCadastros.CurrentEntity = tb306;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {           
            TB306_AGEND_CONTATO tb306 = RetornaEntidade();

            if (tb306 != null)
            {
                ddlTipoContato.SelectedValue = tb306.TP_CONTAT;
                CarregaUnidades();
                CarregaContato();

                if (tb306.CO_EMP_CONTAT != null)                    
                    ddlUnidade.SelectedValue = tb306.CO_EMP_CONTAT.ToString();

                if (tb306.CO_CONTAT != null)
                    ddlContato.SelectedValue = tb306.CO_CONTAT.ToString();

                if (ddlTipoContato.SelectedValue == "F" || ddlTipoContato.SelectedValue == "P")
                {
                    var varTb03 = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                  where tb03.CO_COL == tb306.CO_CONTAT
                                  select new 
                                  { 
                                      tb03.NO_COL, tb03.NO_APEL_COL, tb03.NU_TELE_CELU_COL, tb03.NU_TELE_COME_COL, tb03.NU_TELE_RESI_COL,
                                      tb03.CO_SEXO_COL, tb03.DT_NASC_COL, tb03.CO_EMAI_COL
                                  }).FirstOrDefault();

                    if (varTb03 != null)
                    {
                        txtApeliContato.Text = varTb03.NO_APEL_COL != null ? varTb03.NO_APEL_COL : "";
                        txtDataNascto.Text = varTb03.DT_NASC_COL.ToString("dd/MM/yyyy");
                        txtEmailContato.Text = varTb03.CO_EMAI_COL != null ? varTb03.CO_EMAI_COL : "";
                        txtNomeContato.Text = varTb03.NO_COL;
                        txtTelCelulContato.Text = varTb03.NU_TELE_CELU_COL != null ? varTb03.NU_TELE_CELU_COL : "";
                        txtTelComerContato.Text = varTb03.NU_TELE_COME_COL != null ? varTb03.NU_TELE_COME_COL : "";
                        txtTelResidContato.Text = varTb03.NU_TELE_RESI_COL != null ? varTb03.NU_TELE_RESI_COL : "";
                        ddlSexoContato.SelectedValue = varTb03.CO_SEXO_COL;
                    }
                }
                else if (ddlTipoContato.SelectedValue == "A")
                {
                    var varTb07 = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                   where tb07.CO_ALU == tb306.CO_CONTAT
                                   select new
                                   {
                                       tb07.NO_ALU, tb07.NO_APE_ALU, tb07.NU_TELE_CELU_ALU, tb07.NU_TELE_COME_ALU,
                                       tb07.NU_TELE_RESI_ALU, tb07.CO_SEXO_ALU, tb07.DT_NASC_ALU, tb07.NO_WEB_ALU
                                   }).FirstOrDefault();

                    if (varTb07 != null)
                    {
                        txtApeliContato.Text = varTb07.NO_APE_ALU != null ? varTb07.NO_APE_ALU : "";
                        txtDataNascto.Text = varTb07.DT_NASC_ALU.Value.ToString("dd/MM/yyyy");
                        txtEmailContato.Text = varTb07.NO_WEB_ALU != null ? varTb07.NO_WEB_ALU : "";
                        txtNomeContato.Text = varTb07.NO_ALU;
                        txtTelCelulContato.Text = varTb07.NU_TELE_CELU_ALU != null ? varTb07.NU_TELE_CELU_ALU : "";
                        txtTelComerContato.Text = varTb07.NU_TELE_COME_ALU != null ? varTb07.NU_TELE_COME_ALU : "";
                        txtTelResidContato.Text = varTb07.NU_TELE_RESI_ALU != null ? varTb07.NU_TELE_RESI_ALU : "";
                        ddlSexoContato.SelectedValue = varTb07.CO_SEXO_ALU;
                    }
                }
                else if (ddlTipoContato.SelectedValue == "R")
                {
                    var varTb108 = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                    where tb108.CO_RESP == tb306.CO_CONTAT
                                   select new
                                   {
                                       tb108.NO_RESP, tb108.NO_APELIDO_RESP, tb108.NU_TELE_CELU_RESP, tb108.NU_TELE_COME_RESP, 
                                       tb108.NU_TELE_RESI_RESP, tb108.CO_SEXO_RESP, tb108.DT_NASC_RESP, tb108.DES_EMAIL_RESP
                                   }).FirstOrDefault();

                    if (varTb108 != null)
                    {
                        txtApeliContato.Text = varTb108.NO_APELIDO_RESP != null ? varTb108.NO_APELIDO_RESP : "";
                        txtDataNascto.Text = varTb108.DT_NASC_RESP.Value.ToString("dd/MM/yyyy");
                        txtEmailContato.Text = varTb108.DES_EMAIL_RESP != null ? varTb108.DES_EMAIL_RESP : "";
                        txtNomeContato.Text = varTb108.NO_RESP;
                        txtTelCelulContato.Text = varTb108.NU_TELE_CELU_RESP != null ? varTb108.NU_TELE_CELU_RESP : "";
                        txtTelComerContato.Text = varTb108.NU_TELE_COME_RESP != null ? varTb108.NU_TELE_COME_RESP : "";
                        txtTelResidContato.Text = varTb108.NU_TELE_RESI_RESP != null ? varTb108.NU_TELE_RESI_RESP : "";
                        ddlSexoContato.SelectedValue = varTb108.CO_SEXO_RESP;
                    }
                }
                else
                {
                    txtApeliContato.Text = tb306.NO_APELI_CONTAT != null ? tb306.NO_APELI_CONTAT : "";
                    txtDataNascto.Text = tb306.DT_NASCTO_CONTAT.Value.ToString("dd/MM/yyyy");
                    txtEmailContato.Text = tb306.DE_EMAIL_CONTAT != null ? tb306.DE_EMAIL_CONTAT : "";
                    txtNomeContato.Text = tb306.NO_CONTAT;
                    txtTelCelulContato.Text = tb306.NU_TELE_CELU_CONTAT != null ? tb306.NU_TELE_CELU_CONTAT : "";
                    txtTelComerContato.Text = tb306.NU_TELE_COME_CONTAT != null ? tb306.NU_TELE_COME_CONTAT : "";
                    txtTelResidContato.Text = tb306.NU_TELE_RESI_CONTAT != null ? tb306.NU_TELE_RESI_CONTAT : "";
                    ddlSexoContato.SelectedValue = tb306.CO_SEXO_CONTAT;
                }   
            }            
        }

//====> 
        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB306_AGEND_CONTATO</returns>
        private TB306_AGEND_CONTATO RetornaEntidade()
        {
            TB306_AGEND_CONTATO tb306 = TB306_AGEND_CONTATO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb306 == null) ? new TB306_AGEND_CONTATO() : tb306;
        }
        #endregion      

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o DropDown de Unidades
        /// </summary>
        protected void CarregaUnidades()
        {
            if (ddlTipoContato.SelectedValue == "R" || ddlTipoContato.SelectedValue == "O")
            {
                ddlUnidade.Items.Clear();
                ddlUnidade.Enabled = false;
            }
            else
            {
                ddlUnidade.Enabled = true;
                ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                         where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                         select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).OrderBy(e => e.NO_FANTAS_EMP);

                ddlUnidade.DataTextField = "NO_FANTAS_EMP";
                ddlUnidade.DataValueField = "CO_EMP";
                ddlUnidade.DataBind();

                ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Contatos de acordo com o tipo de contato "ddlTipoContato"
        /// </summary>
        protected void CarregaContato()
        {
            if ((ddlTipoContato.SelectedValue == "O"))
            {
                ddlContato.Enabled = false;
                ddlContato.Items.Clear();
                ddlSexoContato.Enabled = true;
                txtApeliContato.Enabled = true;
                txtDataNascto.Enabled = true;
                txtEmailContato.Enabled = true;
                txtNomeContato.Enabled = true;
                txtTelCelulContato.Enabled = true;
                txtTelComerContato.Enabled = true;
                txtTelResidContato.Enabled = true;                   
            }
            else
            {
                ddlContato.Enabled = true;
                ddlSexoContato.Enabled = false;
                txtApeliContato.Enabled = false;
                txtDataNascto.Enabled = false;
                txtEmailContato.Enabled = false;
                txtNomeContato.Enabled = false;
                txtTelCelulContato.Enabled = false;
                txtTelComerContato.Enabled = false;
                txtTelResidContato.Enabled = false;
                int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

                if (ddlTipoContato.SelectedValue == "P")
                {                    
                    ddlContato.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                             where tb03.FLA_PROFESSOR == "S"
                                             select new { tb03.NO_COL, tb03.CO_COL }).OrderBy(c => c.NO_COL);

                    ddlContato.DataTextField = "NO_COL";
                    ddlContato.DataValueField = "CO_COL";
                    ddlContato.DataBind(); 
                }
                else if (ddlTipoContato.SelectedValue == "F")
                {
                    ddlContato.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(coEmp)
                                             where tb03.FLA_PROFESSOR == "N"
                                             select new { tb03.NO_COL, tb03.CO_COL }).OrderBy(c => c.NO_COL);

                    ddlContato.DataTextField = "NO_COL";
                    ddlContato.DataValueField = "CO_COL";
                    ddlContato.DataBind();
                }
                else if (ddlTipoContato.SelectedValue == "A")
                {                    
                    ddlContato.DataSource = (from tb07 in TB07_ALUNO.RetornaPelaUnid(coEmp)
                                             select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(c => c.NO_ALU);

                    ddlContato.DataTextField = "NO_ALU";
                    ddlContato.DataValueField = "CO_ALU";
                    ddlContato.DataBind();
                }
                else if (ddlTipoContato.SelectedValue == "R")
                {
                    ddlContato.DataSource = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                             select new { tb108.NO_RESP, tb108.CO_RESP }).OrderBy(c => c.NO_RESP);

                    ddlContato.DataTextField = "NO_RESP";
                    ddlContato.DataValueField = "CO_RESP";
                    ddlContato.DataBind();
                }

                int coContato = ddlContato.SelectedValue != "" ? int.Parse(ddlContato.SelectedValue) : 0;

                if (ddlTipoContato.SelectedValue == "F" || ddlTipoContato.SelectedValue == "P")
                {
                    var varTb03 = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                   where tb03.CO_COL == coContato
                                   select new
                                   {
                                       tb03.NO_COL,
                                       tb03.NO_APEL_COL,
                                       tb03.NU_TELE_CELU_COL,
                                       tb03.NU_TELE_COME_COL,
                                       tb03.NU_TELE_RESI_COL,
                                       tb03.CO_SEXO_COL,
                                       tb03.DT_NASC_COL,
                                       tb03.CO_EMAI_COL
                                   }).FirstOrDefault();

                    if (varTb03 != null)
                    {
                        txtApeliContato.Text = varTb03.NO_APEL_COL != null ? varTb03.NO_APEL_COL : "";
                        txtDataNascto.Text = varTb03.DT_NASC_COL.ToString("dd/MM/yyyy");
                        txtEmailContato.Text = varTb03.CO_EMAI_COL != null ? varTb03.CO_EMAI_COL : "";
                        txtNomeContato.Text = varTb03.NO_COL;
                        txtTelCelulContato.Text = varTb03.NU_TELE_CELU_COL != null ? varTb03.NU_TELE_CELU_COL : "";
                        txtTelComerContato.Text = varTb03.NU_TELE_COME_COL != null ? varTb03.NU_TELE_COME_COL : "";
                        txtTelResidContato.Text = varTb03.NU_TELE_RESI_COL != null ? varTb03.NU_TELE_RESI_COL : "";
                        ddlSexoContato.SelectedValue = varTb03.CO_SEXO_COL;
                    }
                }
                else if (ddlTipoContato.SelectedValue == "A")
                {
                    var varTb07 = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                   where tb07.CO_ALU == coContato
                                   select new
                                   {
                                       tb07.NO_ALU,
                                       tb07.NO_APE_ALU,
                                       tb07.NU_TELE_CELU_ALU,
                                       tb07.NU_TELE_COME_ALU,
                                       tb07.NU_TELE_RESI_ALU,
                                       tb07.CO_SEXO_ALU,
                                       tb07.DT_NASC_ALU,
                                       tb07.NO_WEB_ALU
                                   }).FirstOrDefault();

                    if (varTb07 != null)
                    {
                        txtApeliContato.Text = varTb07.NO_APE_ALU != null ? varTb07.NO_APE_ALU : "";
                        txtDataNascto.Text = varTb07.DT_NASC_ALU.Value.ToString("dd/MM/yyyy");
                        txtEmailContato.Text = varTb07.NO_WEB_ALU != null ? varTb07.NO_WEB_ALU : "";
                        txtNomeContato.Text = varTb07.NO_ALU;
                        txtTelCelulContato.Text = varTb07.NU_TELE_CELU_ALU != null ? varTb07.NU_TELE_CELU_ALU : "";
                        txtTelComerContato.Text = varTb07.NU_TELE_COME_ALU != null ? varTb07.NU_TELE_COME_ALU : "";
                        txtTelResidContato.Text = varTb07.NU_TELE_RESI_ALU != null ? varTb07.NU_TELE_RESI_ALU : "";
                        ddlSexoContato.SelectedValue = varTb07.CO_SEXO_ALU;
                    }
                }
                else if (ddlTipoContato.SelectedValue == "R")
                {
                    var varTb108 = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                    where tb108.CO_RESP == coContato
                                    select new
                                    {
                                        tb108.NO_RESP,
                                        tb108.NO_APELIDO_RESP,
                                        tb108.NU_TELE_CELU_RESP,
                                        tb108.NU_TELE_COME_RESP,
                                        tb108.NU_TELE_RESI_RESP,
                                        tb108.CO_SEXO_RESP,
                                        tb108.DT_NASC_RESP,
                                        tb108.DES_EMAIL_RESP
                                    }).FirstOrDefault();

                    if (varTb108 != null)
                    {
                        txtApeliContato.Text = varTb108.NO_APELIDO_RESP != null ? varTb108.NO_APELIDO_RESP : "";
                        txtDataNascto.Text = varTb108.DT_NASC_RESP.Value.ToString("dd/MM/yyyy");
                        txtEmailContato.Text = varTb108.DES_EMAIL_RESP != null ? varTb108.DES_EMAIL_RESP : "";
                        txtNomeContato.Text = varTb108.NO_RESP;
                        txtTelCelulContato.Text = varTb108.NU_TELE_CELU_RESP != null ? varTb108.NU_TELE_CELU_RESP : "";
                        txtTelComerContato.Text = varTb108.NU_TELE_COME_RESP != null ? varTb108.NU_TELE_COME_RESP : "";
                        txtTelResidContato.Text = varTb108.NU_TELE_RESI_RESP != null ? varTb108.NU_TELE_RESI_RESP : "";
                        ddlSexoContato.SelectedValue = varTb108.CO_SEXO_RESP;
                    }
                }
            }
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaContato();
        }

        protected void ddlTipoContato_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNomeContato.Text = "";
            txtApeliContato.Text = "";
            txtDataNascto.Text = "";
            txtEmailContato.Text = "";
            txtTelCelulContato.Text = "";
            txtTelComerContato.Text = "";
            txtTelResidContato.Text = "";
            ddlSexoContato.SelectedValue = "M";
            CarregaUnidades();
            CarregaContato();
        }

        protected void ddlContato_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coContato = ddlContato.SelectedValue != "" ? int.Parse(ddlContato.SelectedValue) : 0;

            if (ddlTipoContato.SelectedValue == "F" || ddlTipoContato.SelectedValue == "P")
            {
                var varTb03 = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                               where tb03.CO_COL == coContato
                               select new
                               {
                                   tb03.NO_COL,
                                   tb03.NO_APEL_COL,
                                   tb03.NU_TELE_CELU_COL,
                                   tb03.NU_TELE_COME_COL,
                                   tb03.NU_TELE_RESI_COL,
                                   tb03.CO_SEXO_COL,
                                   tb03.DT_NASC_COL,
                                   tb03.CO_EMAI_COL
                               }).FirstOrDefault();

                if (varTb03 != null)
                {
                    txtApeliContato.Text = varTb03.NO_APEL_COL != null ? varTb03.NO_APEL_COL : "";
                    txtDataNascto.Text = varTb03.DT_NASC_COL.ToString("dd/MM/yyyy");
                    txtEmailContato.Text = varTb03.CO_EMAI_COL != null ? varTb03.CO_EMAI_COL : "";
                    txtNomeContato.Text = varTb03.NO_COL;
                    txtTelCelulContato.Text = varTb03.NU_TELE_CELU_COL != null ? varTb03.NU_TELE_CELU_COL : "";
                    txtTelComerContato.Text = varTb03.NU_TELE_COME_COL != null ? varTb03.NU_TELE_COME_COL : "";
                    txtTelResidContato.Text = varTb03.NU_TELE_RESI_COL != null ? varTb03.NU_TELE_RESI_COL : "";
                    ddlSexoContato.SelectedValue = varTb03.CO_SEXO_COL;
                }
            }
            else if (ddlTipoContato.SelectedValue == "A")
            {
                var varTb07 = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                               where tb07.CO_ALU == coContato
                               select new
                               {
                                   tb07.NO_ALU,
                                   tb07.NO_APE_ALU,
                                   tb07.NU_TELE_CELU_ALU,
                                   tb07.NU_TELE_COME_ALU,
                                   tb07.NU_TELE_RESI_ALU,
                                   tb07.CO_SEXO_ALU,
                                   tb07.DT_NASC_ALU,
                                   tb07.NO_WEB_ALU
                               }).FirstOrDefault();

                if (varTb07 != null)
                {
                    txtApeliContato.Text = varTb07.NO_APE_ALU != null ? varTb07.NO_APE_ALU : "";
                    txtDataNascto.Text = varTb07.DT_NASC_ALU.Value.ToString("dd/MM/yyyy");
                    txtEmailContato.Text = varTb07.NO_WEB_ALU != null ? varTb07.NO_WEB_ALU : "";
                    txtNomeContato.Text = varTb07.NO_ALU;
                    txtTelCelulContato.Text = varTb07.NU_TELE_CELU_ALU != null ? varTb07.NU_TELE_CELU_ALU : "";
                    txtTelComerContato.Text = varTb07.NU_TELE_COME_ALU != null ? varTb07.NU_TELE_COME_ALU : "";
                    txtTelResidContato.Text = varTb07.NU_TELE_RESI_ALU != null ? varTb07.NU_TELE_RESI_ALU : "";
                    ddlSexoContato.SelectedValue = varTb07.CO_SEXO_ALU;
                }
            }
            else if (ddlTipoContato.SelectedValue == "R")
            {
                var varTb108 = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                where tb108.CO_RESP == coContato
                                select new
                                {
                                    tb108.NO_RESP,
                                    tb108.NO_APELIDO_RESP,
                                    tb108.NU_TELE_CELU_RESP,
                                    tb108.NU_TELE_COME_RESP,
                                    tb108.NU_TELE_RESI_RESP,
                                    tb108.CO_SEXO_RESP,
                                    tb108.DT_NASC_RESP,
                                    tb108.DES_EMAIL_RESP
                                }).FirstOrDefault();

                if (varTb108 != null)
                {
                    txtApeliContato.Text = varTb108.NO_APELIDO_RESP != null ? varTb108.NO_APELIDO_RESP : "";
                    txtDataNascto.Text = varTb108.DT_NASC_RESP.Value.ToString("dd/MM/yyyy");
                    txtEmailContato.Text = varTb108.DES_EMAIL_RESP != null ? varTb108.DES_EMAIL_RESP : "";
                    txtNomeContato.Text = varTb108.NO_RESP;
                    txtTelCelulContato.Text = varTb108.NU_TELE_CELU_RESP != null ? varTb108.NU_TELE_CELU_RESP : "";
                    txtTelComerContato.Text = varTb108.NU_TELE_COME_RESP != null ? varTb108.NU_TELE_COME_RESP : "";
                    txtTelResidContato.Text = varTb108.NU_TELE_RESI_RESP != null ? varTb108.NU_TELE_RESI_RESP : "";
                    ddlSexoContato.SelectedValue = varTb108.CO_SEXO_RESP;
                }
            }
        }
    }
}

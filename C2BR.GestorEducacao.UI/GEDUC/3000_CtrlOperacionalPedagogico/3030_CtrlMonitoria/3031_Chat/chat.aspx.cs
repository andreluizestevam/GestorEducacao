//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
//           |                            | 

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Data;
using System.ServiceModel;
using System.IO;
using System.Reflection;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3010_CtrlPedagogicoSeries;
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3030_CtrlMonitoria._3031_Chat
{
    public partial class chat : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            //string eventTarget = Request["__EVENTTARGET"];

            //if (eventTarget == "EnvMsg")
            //{
            //    EventArgs ea = new EventArgs();
            //    lnkEnvMsg_Click(lnkEnvMsg, ea);
            //}

            //if (eventTarget == "updMensagens")
            //{
            //    ChatOnline.coSala = int.Parse(hidCoSala.Value);
            //    ChatOnline.coEmp = LoginAuxili.CO_EMP;
            //    ChatOnline.CarregaGridMsg();
            //    txtMsg.Text = "";
            //}

            //ScriptManager.RegisterStartupScript(
            //        updMensagens,
            //        this.GetType(),
            //        "Acao",
            //        "scrollMsg();",
            //        true
            //    );


            if (!IsPostBack)
            {
                CarregaSala();
                //CarregaUsuarios();
            }
        }

        protected void CarregaSala()
        {
            var res = (from sala in TB161_SALA.RetornaTodosRegistros()
                       join moni in TB189_AGEND_MONIT_PROFE.RetornaTodosRegistros() on sala.ID_AGEND_MONIT_PROFE equals moni.ID_AGEND_MONIT_PROFE into m1
                       from m2 in m1.DefaultIfEmpty()
                       where sala.CO_SITUA == "A"
                       select new SaidaSala
                       {
                           CO_SALA = sala.CO_SALA,
                           NO_SALA = sala.NO_SALA,
                           ID_AGEND = m2.ID_AGEND_MONIT_PROFE,
                           HR_INICIO = "00:00",
                           DE_TEMA = "TEMA"
                       });


            grdSala.DataSource = res;
            grdSala.DataBind();
        }

        public class SaidaSala
        {
            public int CO_SALA { get; set; }
            public string HR_INICIO { get; set; }
            public string DE_TEMA { get; set; }
            public string NO_SALA { get; set; }
            public int? ID_AGEND { get; set; }
        }

        public void CarregaGridMsg()
        {
            int coSala = int.Parse(hidCoSala.Value);
            var res = (from msg in TB170_MENSAGEM.RetornaTodosRegistros()
                       join usu in ADMUSUARIO.RetornaTodosRegistros() on msg.ADMUSUARIO.ideAdmUsuario equals usu.ideAdmUsuario
                       where msg.TB161_SALA.CO_SALA == coSala
                       select new SaidaMsg
                       {
                           NO_USU = usu.desLogin,
                           DE_MSG = msg.DE_MSG,
                           DT_MSG = msg.DT_MSG
                       });

            grdMsg.DataSource = res;
            grdMsg.DataBind();
        }

        public class SaidaMsg
        {
            public string NO_USU { get; set; }
            public string DE_MSG { get; set; }
            public DateTime DT_MSG { get; set; }
            public string DATA
            {
                get
                {
                    return this.DT_MSG.ToString("dd/MM/yy");
                }
            }
            public string HORA
            {
                get
                {
                    return this.DT_MSG.ToString("hh:mm");
                }
            }
        }

        public void CarregaMonitorPrincipal()
        {
            int coSala = int.Parse(hidCoSala.Value);

            SaidaMonitorPrincipal res = (from usu in ADMUSUARIO.RetornaTodosRegistros()
                                        join col in TB03_COLABOR.RetornaTodosRegistros() on usu.CodUsuario equals col.CO_COL
                                        join usuS in TB163_SALA_USUARIO.RetornaTodosRegistros() on usu.ideAdmUsuario equals usuS.ideAdmUsuario
                                        where usuS.CO_SALA == coSala
                                        && usuS.CO_TIPO_USUAR == "M"
                                        && usuS.FL_MONIT_PRINC == "S"
                                        select new SaidaMonitorPrincipal
                                        {
                                            noUsu = col.NO_COL,
                                            nuCpf = col.NU_CPF_COL,
                                            usu = usu.desLogin,
                                            nuTel = col.NU_TELE_RESI_COL,
                                            deEmail = col.CO_EMAIL_FUNC_COL
                                        }).FirstOrDefault();

            lblValNome.Text = res.noUsu;
            lblValCodigo.Text = res.cpf;
            lblValUsuario.Text = res.usu;
            lblValTelefone.Text = res.tel;
            lblValEmail.Text = res.email;
        }

        public class SaidaMonitorPrincipal
        {
            public string noUsu { get; set; }

            public string nuCpf { get; set; }
            public string cpf
            {
                get
                {
                    return this.nuCpf != "" && this.nuCpf != null ? String.Format(@"{0:000\.000\.000\-00}", decimal.Parse(this.nuCpf)) : "******";
                }
            }

            public string usu { get; set; }

            public string nuTel { get; set; }
            public string tel
            {
                get
                {
                    return this.nuTel != "" && this.nuTel != null ? String.Format(@"{0:(00) 0000\-0000}", decimal.Parse(this.nuTel)) : "******";
                }
            }

            public string deEmail { get; set; }

            public string email
            {
                get
                {
                    return this.deEmail != null && this.deEmail != "" ? this.deEmail : "******";
                }
            }

        }

        public void CarregaGridMonitores()
        {
            int coSala = int.Parse(hidCoSala.Value);

            var res = (from tb163 in TB163_SALA_USUARIO.RetornaTodosRegistros()
                       join usu in ADMUSUARIO.RetornaTodosRegistros() on tb163.ideAdmUsuario equals usu.ideAdmUsuario
                       join col in TB03_COLABOR.RetornaTodosRegistros() on usu.CodUsuario equals col.CO_COL
                       where tb163.CO_SALA == coSala
                       && tb163.ideAdmUsuario != LoginAuxili.IDEADMUSUARIO
                       select new SaidaGridMonitores
                       {
                           cpf = col.NU_CPF_COL,
                           nome = col.NO_APEL_COL
                       });

            grdMonitAuxili.DataSource = res;
            grdMonitAuxili.DataBind();
        }

        public class SaidaGridMonitores
        {
            public string cpf { get; set; }
            public string nome { get; set; }
            public string monitor
            {
                get
                {
                    string c = this.cpf != null && this.cpf != "" ? this.cpf : "******";
                    string n = this.nome != null && this.nome != "" ? this.nome : "******";
                    return c + " - " + n;
                }
            }
        }

        public void CarregaComboMonitores()
        {
            int coSala = int.Parse(hidCoSala.Value);

            var res = (from tb163 in TB163_SALA_USUARIO.RetornaTodosRegistros()
                       join usu in ADMUSUARIO.RetornaTodosRegistros() on tb163.ideAdmUsuario equals usu.ideAdmUsuario
                       join col in TB03_COLABOR.RetornaTodosRegistros() on usu.CodUsuario equals col.CO_COL
                       where tb163.CO_SALA == coSala
                       && tb163.ideAdmUsuario != LoginAuxili.IDEADMUSUARIO
                       select new
                       {
                           coUsu = usu.ideAdmUsuario,
                           noUsu = col.NO_COL
                       });

            ddlOutrosAlunos.DataTextField = "noUsu";
            ddlOutrosAlunos.DataValueField = "coUsu";

            ddlOutrosAlunos.DataSource = res;
            ddlOutrosAlunos.DataBind();
        }

        public void CarregaGridMsgOutros()
        {
            int coSala = int.Parse(hidCoSala.Value);
            var res = (from msg in TB170_MENSAGEM.RetornaTodosRegistros()
                       join usu in ADMUSUARIO.RetornaTodosRegistros() on msg.ADMUSUARIO.ideAdmUsuario equals usu.ideAdmUsuario
                       where msg.TB161_SALA.CO_SALA == coSala
                       select new 
                       {
                           NO_USU = usu.desLogin,
                           DE_MSG = msg.DE_MSG
                       });

            grdMsgOutros.DataSource = res;
            grdMsgOutros.DataBind();
        }
        //protected void CarregaUsuarios()
        //{
        //    var res = (from usu in ADMUSUARIO.RetornaTodosRegistros()
        //               select new SaidaUsuario
        //               {
        //                   idUsu = usu.ideAdmUsuario,
        //                   coUsu = usu.CodUsuario,
        //                   coEmp = usu.CO_EMP,
        //                   tpUsu = usu.TipoUsuario
        //               }).ToList();

        //    List<SaidaUsuario> lu = new List<SaidaUsuario>();

        //    foreach (SaidaUsuario r in res)
        //    {
        //        switch (r.tpUsu)
        //        {
        //            case "P":
        //                TB03_COLABOR pro = TB03_COLABOR.RetornaPelaChavePrimaria(r.coEmp, r.coUsu);
        //                if (pro != null)
        //                {
        //                    r.noUsu = pro.NO_COL;
        //                }
        //                else
        //                {
        //                    lu.Add(r);
        //                }
        //                break;
        //            case "F":
        //                TB03_COLABOR col = TB03_COLABOR.RetornaPelaChavePrimaria(r.coEmp, r.coUsu);
        //                if (col != null)
        //                {
        //                    r.noUsu = col.NO_COL;
        //                }
        //                else
        //                {
        //                    lu.Add(r);
        //                }
        //                break;
        //            case "A":
        //                TB07_ALUNO alu = TB07_ALUNO.RetornaPelaChavePrimaria(r.coUsu, r.coEmp);
        //                if (alu != null)
        //                {
        //                    r.noUsu = alu.NO_ALU;
        //                }
        //                else
        //                {
        //                    lu.Add(r);
        //                }
        //                break;
        //        }
        //    }

        //    foreach (SaidaUsuario l in lu)
        //    {
        //        res.Remove(l);
        //    }

        //    grdUsuario.DataSource = res;
        //    grdUsuario.DataBind();
        //}

        //public class SaidaUsuario
        //{
        //    public int idUsu { get; set; }
        //    public int coUsu { get; set; }
        //    public int coEmp { get; set; }
        //    public string tpUsu { get; set; }
        //    public string noUsu { get; set; }
        //}

        //public void lnkEnvMsg_Click(object sender, EventArgs e)
        //{
        //    AuxiliChat chat = new AuxiliChat();

        //    chat.enviaMsg(TipoMsg.Msg, int.Parse(hidCoSala.Value), txtMsg.Text);

        //    ChatOnline.coSala = int.Parse(hidCoSala.Value);
        //    ChatOnline.coEmp = LoginAuxili.CO_EMP;
        //    ChatOnline.CarregaGridMsg();
        //    txtMsg.Text = "";
        //    txtMsg.Focus();

        //    updMensagens.Update();
        //}

        //public void lnkAddSala_Click(object sender, EventArgs e)
        //{
        //    AuxiliChat chat = new AuxiliChat();

        //    //int coSala = chat.criaSala();
        //}

        //public void lnkIncSala_Click(object sender, EventArgs e)
        //{
        //    AuxiliChat chat = new AuxiliChat();

        //    int coSala = chat.criaSala(hidNomSalaN.Value, LoginAuxili.IDEADMUSUARIO, LoginAuxili.CO_EMP);

        //    hidCoSala.Value = coSala.ToString();

        //    lblTitChat.Text = TB161_SALA.RetornaPelaChavePrimaria(coSala).NO_SALA;

        //    CarregaSala();
        //    ChatOnline.coSala = int.Parse(hidCoSala.Value);
        //    ChatOnline.coEmp = LoginAuxili.CO_EMP;
        //    ChatOnline.CarregaGridMsg();
        //    txtMsg.Text = "";

        //    updMensagens.Update();
        //}

        //public void lnkSairSala_Click(object sender, EventArgs e)
        //{
        //    AuxiliChat chat = new AuxiliChat();
        //    int coSala = int.Parse(hidCoSala.Value);

        //    if (!chat.sairSala(coSala, LoginAuxili.IDEADMUSUARIO, LoginAuxili.CO_EMP))
        //    {
        //        AuxiliPagina.EnvioMensagemErro(this.Page, "Ocorreu um erro ao tentar sair da sala, tente novamente.");
        //        return;
        //    }

        //    CarregaSala();

        //    hidCoSala.Value = "0";

        //    ChatOnline.coSala = int.Parse(hidCoSala.Value);
        //    ChatOnline.coEmp = LoginAuxili.CO_EMP;
        //    ChatOnline.titChat = hidNomSala.Value;
        //    ChatOnline.CarregaGridMsg();
        //    txtMsg.Text = "";

        //    updMensagens.Update();
        //}

        //public void txtNomSala_TextChange(object sender, EventArgs e)
        //{
        //    TextBox tb = (TextBox)sender;
        //    hidNomSala.Value = tb.Text;
        //}

        public void CarregaAlunos()
        {
            int coSala = int.Parse(hidCoSala.Value);

            var res = (from tb163 in TB163_SALA_USUARIO.RetornaTodosRegistros()
                       join usu in ADMUSUARIO.RetornaTodosRegistros() on tb163.ideAdmUsuario equals usu.ideAdmUsuario
                       join alu in TB07_ALUNO.RetornaTodosRegistros() on usu.CodUsuario equals alu.CO_ALU
                       where tb163.CO_SALA == coSala
                       && tb163.ideAdmUsuario != LoginAuxili.IDEADMUSUARIO
                       && tb163.CO_TIPO_USUAR == "A"
                       select new
                       {
                           coUsu = usu.ideAdmUsuario,
                           noUsu = alu.NO_APE_ALU
                       });

            ddlAluno.DataValueField = "coUsu";
            ddlAluno.DataTextField = "noUsu";

            ddlAluno.DataSource = res;
            ddlAluno.DataBind();
        }

        public void lnkSelSala_Click(object sender, EventArgs e)
        {
            CheckBox sel = (CheckBox)sender;

            foreach (GridViewRow l in grdSala.Rows)
            {
                CheckBox chkL = (CheckBox)l.Cells[0].FindControl("chkSelSala");
                LinkButton lbg = (LinkButton)l.Cells[0].FindControl("lnkSelSala");
                HiddenField hidS = (HiddenField)l.Cells[0].FindControl("hidCoSalaG");
                HiddenField hidN = (HiddenField)l.Cells[0].FindControl("hidNomSalaG");

                if (sel.ClientID == chkL.ClientID)
                {
                    TB161_SALA sala = TB161_SALA.RetornaPelaChavePrimaria(int.Parse(hidS.Value));
                    TB189_AGEND_MONIT_PROFE monit = sala.ID_AGEND_MONIT_PROFE != null ? TB189_AGEND_MONIT_PROFE.RetornaPelaChavePrimaria(sala.ID_AGEND_MONIT_PROFE.Value) : null;
                    TB188_MONIT_CURSO_PROFE mp = sala.ID_AGEND_MONIT_PROFE != null ? TB188_MONIT_CURSO_PROFE.RetornaPelaChavePrimaria(monit.ID_AGEND_MONIT_PROFE) : null;
                    TB44_MODULO mod = monit != null ? TB44_MODULO.RetornaPelaChavePrimaria(mp.CO_MODU_CUR) : null;
                    TB01_CURSO cur = monit != null ? TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, mod.CO_MODU_CUR, mp.CO_CUR) : null;
                    TB129_CADTURMAS tur = monit != null ? TB129_CADTURMAS.RetornaPelaChavePrimaria(mp.CO_TUR) : null;
                    TB02_MATERIA mat = monit != null ? TB02_MATERIA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, mp.CO_MODU_CUR, mp.CO_MAT, mp.CO_CUR) : null;
                    TB107_CADMATERIAS cm = mat != null ? TB107_CADMATERIAS.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, mat.ID_MATERIA) : null;
                    string noMat = mat != null ? cm.NO_MATERIA : "";

                    hidCoSala.Value = hidS.Value;
                    hidNomSala.Value = hidN.Value;

                    if (monit != null)
                    {
                        //coMsg.titChat = mod.CO_SIGLA_MODU_CUR + " - " + cur.NO_CUR + " - " + tur.NO_TURMA + " - " + noMat + " - " + hidNomSala.Value;
                        //lblTitChat.Text = mod.CO_SIGLA_MODU_CUR + " - " + cur.NO_CUR + " - " + tur.NO_TURMA + " - " + noMat + " - " + hidNomSala.Value;
                    }
                    else
                    {
                        //coMsg.titChat = hidNomSala.Value;
                        //lblTitChat.Text = hidNomSala.Value;
                    }

                    imgMonitor.ImageUrl = "../../../../Library/IMG/Chat/CordovaJr.jpg";
                    imgAluno.ImageUrl = "../../../../Library/IMG/Chat/Nathalya.jpg";

                    CarregaGridMsg();
                    CarregaGridMsgOutros();
                    CarregaMonitorPrincipal();
                    CarregaComboMonitores();
                    CarregaGridMonitores();
                    CarregaAlunos();
                    //txtMsg.Text = "";

                    updMensagens.Update();
                    updMsgOutros.Update();
                    updAreaPostagem.Update();
                }
                else
                {
                    chkL.Checked = false;
                }
            }
        }

        //public void lnkEnvConv_Click(object sender, EventArgs e)
        //{

        //}
    }
}
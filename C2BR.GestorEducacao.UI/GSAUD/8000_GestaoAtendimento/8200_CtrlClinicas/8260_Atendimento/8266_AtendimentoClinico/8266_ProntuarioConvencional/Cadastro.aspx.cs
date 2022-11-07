//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: Registrar atendimento das consultas da tbs174, devidamente cruzando informações com os planejamentos   
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR  |   O.S.  | DESCRIÇÃO RESUMIDA
// ---------+-----------------------+---------+--------------------------------
// 14/07/14 | Maxwell Almeida       |         | Criação da funcionalidade para registro de atendimento Odontológico
// ---------+-----------------------+---------+--------------------------------
// 27/04/16 | Filipe Rodrigues      | FSP0035 | Alteração na exibição da lista de profissionais para não aparecer caso não seja master

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Data.Objects;
using System.Reflection;
using System.Data;
using Resources;
using System.IO;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8221_AtendimentoOdonto;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAtendimetoMedico;
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8200_CtrlExames;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8260_Atendimento._8266_AtendimentoClinico._8266_ProntuarioConvencional
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }
        public PadraoCadastros CurrentCadastroMasterPage { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtIniPront.Text = DateTime.Now.AddDays(-15).ToShortDateString();
                txtFimPront.Text = DateTime.Now.ToShortDateString();
                carregarQualificacaoProntuario();
            }
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
           
        }

        #endregion


        #region Prontuário Convencional

        private void CarregarModalProntuCon(int coAlu, int qualifPront, DateTime? ini, DateTime? fim)
        {
            try
            {
                var tbs400 = TBS400_PRONT_MASTER.RetornaTodosRegistros()
                        .Where(x => (x.CO_ALU == coAlu)
                               && (qualifPront > 0 ? x.TBS418_CLASS_PRONT.ID_CLASS_PRONT == qualifPront : 0 == 0)
                               && (ini.HasValue ? EntityFunctions.TruncateTime(x.DT_CADAS) >= EntityFunctions.TruncateTime(ini.Value) : 0 == 0)
                               && (fim.HasValue ? EntityFunctions.TruncateTime(x.DT_CADAS) <= EntityFunctions.TruncateTime(fim.Value) : 0 == 0)
                               ).ToList();

                string texto = "";

                if (tbs400 != null)
                {
                    foreach (var it in tbs400)
                    {
                        it.TB14_DEPTOReference.Load();
                        it.TBS418_CLASS_PRONTReference.Load();
                        string qual = it.TBS418_CLASS_PRONT != null ? it.TBS418_CLASS_PRONT.NO_CLASS_PRONT : "Sem Qualificação";

                        var tb03 = TB03_COLABOR.RetornaPeloCoCol((it.CO_COL.HasValue ? it.CO_COL.Value : it.CO_COL_CADAS));

                        texto += "<b style='color:blue; font-weight: 600;'>" + it.DT_CADAS.ToString("dd/MM/yyyy HH:mm") + "  -  " + tb03.NO_APEL_COL + "  " + tb03.CO_SIGLA_ENTID_PROFI + " " + tb03.NU_ENTID_PROFI + "/" + tb03.CO_UF_ENTID_PROFI + " - " + qual + " " + (it.TB14_DEPTO != null ? "  -  " + it.TB14_DEPTO.CO_SIGLA_DEPTO : "") + "</b>" + "</br>" + "<p>" + it.ANAMNSE.Replace("<BR>", "</p>") + "</p>" + "</br> </br>";
                    }
                }
                divObsProntuCon.InnerHtml = texto;
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, ex.Message);
            }
        }

        private void carregarQualificacaoProntuario()
        {
            var res = TBS418_CLASS_PRONT.RetornaTodosRegistros()
                        .Select(x => new
                        {
                            x.NO_CLASS_PRONT,
                            x.ID_CLASS_PRONT
                        });
            ddlQualifPront.DataSource = res;
            ddlQualifPront.DataTextField = "NO_CLASS_PRONT";
            ddlQualifPront.DataValueField = "ID_CLASS_PRONT";
            ddlQualifPront.DataBind();
            ddlQualifPront.Items.Insert(0, new ListItem("Todos", ""));
        }

        protected void BtnProntuCon_Click(object sender, EventArgs e)
        {
            try
            {
                divObsProntuCon.InnerHtml = hidIdProntuCon.Value = "";
                carregarQualificacaoProntuario();
                txtNumPasta.Text = "";
                txtNumPront.Text = "";
                txtPacienteProntuCon.Text = "";
                drpPacienteProntuCon.DataSource = null;
                drpPacienteProntuCon.DataBind();
                ddlQualifPront.SelectedValue = "";
                txtIniPront.Text = DateTime.Now.AddDays(-15).ToShortDateString();
                txtFimPront.Text = DateTime.Now.ToShortDateString();
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, ex.Message);
            }
        }

        protected void imgbPesqPacNome_OnClick(object sender, EventArgs e)
        {
            if (txtPacienteProntuCon.Text.Length < 3)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Digite pelo menos 3 letras para consultar o paciente.");
                txtPacienteProntuCon.Focus();
                return;
            }

            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where tb07.NO_ALU.Contains(txtPacienteProntuCon.Text)
                             && tb07.CO_SITU_ALU != "H" && tb07.CO_SITU_ALU != "O"
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            if (res != null)
            {
                drpPacienteProntuCon.DataTextField = "NO_ALU";
                drpPacienteProntuCon.DataValueField = "CO_ALU";
                drpPacienteProntuCon.DataSource = res;
                drpPacienteProntuCon.DataBind();
            }

            drpPacienteProntuCon.Items.Insert(0, new ListItem("Selecione", ""));
            OcultarPesquisa(true);
        }

        protected void imgbVoltarPesq_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisa(false);
            divObsProntuCon.InnerHtml = hidIdProntuCon.Value = "";
        }

        private void OcultarPesquisa(bool ocultar)
        {
            txtPacienteProntuCon.Visible =
            imgbPesqPacienteProntuCon.Visible = !ocultar;
            drpPacienteProntuCon.Visible =
            imgbVoltPacienteProntuCon.Visible = ocultar;
        }

        protected void drpPacienteProntuCon_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNumPasta.Text = "";
            txtNumPront.Text = "";
            ddlQualifPront.SelectedValue = "";
        }

        protected void imgBtnPesqPront_OnClick(object sender, EventArgs e)
        {
            try
            {
                string pasta = txtNumPasta.Text;
                int nire = !string.IsNullOrEmpty(txtNumPront.Text) ? int.Parse(txtNumPront.Text) : 0;
                int pac = !string.IsNullOrEmpty(drpPacienteProntuCon.SelectedValue) ? int.Parse(drpPacienteProntuCon.SelectedValue) : 0;
                int qual = !string.IsNullOrEmpty(ddlQualifPront.SelectedValue) ? int.Parse(ddlQualifPront.SelectedValue) : 0;
                DateTime? ini = !string.IsNullOrEmpty(txtIniPront.Text) ? DateTime.Parse(txtIniPront.Text) : (DateTime?)null;
                DateTime? fim = !string.IsNullOrEmpty(txtFimPront.Text) ? DateTime.Parse(txtFimPront.Text) : (DateTime?)null;

                var tb07 = TB07_ALUNO.RetornaTodosRegistros()
                            .Where(x => !string.IsNullOrEmpty(pasta) ? x.DE_PASTA_CONTR == pasta : false
                                   || pac > 0 ? x.CO_ALU == pac : false
                                   || nire > 0 ? x.NU_NIRE == nire : false)
                            .Select(x => new
                            {
                                x.NU_NIRE,
                                x.CO_ALU,
                                x.NO_ALU,
                                x.DE_PASTA_CONTR
                            })
                            .FirstOrDefault();

                if (tb07 != null)
                {
                    txtNumPront.Text = tb07.NU_NIRE.toNire();
                    txtNumPasta.Text = tb07.DE_PASTA_CONTR;
                    if (string.IsNullOrEmpty(drpPacienteProntuCon.SelectedValue))
                    {
                        OcultarPesquisa(true);
                        var _tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(x => x.CO_ALU == tb07.CO_ALU).Select(x => new { x.CO_ALU, x.NO_ALU });
                        drpPacienteProntuCon.DataSource = _tb07;
                        drpPacienteProntuCon.DataTextField = "NO_ALU";
                        drpPacienteProntuCon.DataValueField = "CO_ALU";
                        drpPacienteProntuCon.DataBind();
                    }
                    CarregarModalProntuCon(tb07.CO_ALU, qual, ini, fim);
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Paciente não encontrado!");
                }
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, ex.Message);
            }
        }

        private void SalvarProntuCon(TBS400_PRONT_MASTER tbs400)
        {
            try
            {
                if (String.IsNullOrEmpty(txtCadObsProntuCon.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "É necessário inserir uma anamnese.");
                    txtCadObsProntuCon.Focus();
                    return;
                }
                if (String.IsNullOrEmpty(drpPacienteProntuCon.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o paciente para salvar o prontuário");
                    drpPacienteProntuCon.Focus();
                    return;
                }

                tbs400.ANAMNSE = txtCadObsProntuCon.Text;
                tbs400.CO_ALU = int.Parse(drpPacienteProntuCon.SelectedValue);
                tbs400.CO_COL = LoginAuxili.CO_COL;
                tbs400.CO_COL_CADAS = LoginAuxili.CO_COL;
                tbs400.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs400.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                tbs400.CO_EMP_COL_CADAS = LoginAuxili.CO_EMP;
                tbs400.CO_EMP_COL_SITUA = LoginAuxili.CO_EMP;
                tbs400.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs400.CO_SITUA = "A";
                tbs400.DT_CADAS = DateTime.Now;
                tbs400.DT_SITUA = DateTime.Now;
                tbs400.IP_CADAS = Request.UserHostAddress;
                tbs400.IP_SITUA = Request.UserHostAddress;
                tbs400.TB14_DEPTO = TB14_DEPTO.RetornaPelaChavePrimaria(LoginAuxili.CO_DEPTO);
                tbs400.TBS418_CLASS_PRONT = !string.IsNullOrEmpty(ddlQualifPront.Text) ? TBS418_CLASS_PRONT.RetornaPelaChavePrimaria(int.Parse(ddlQualifPront.Text)) : null;

                var ultimoElemento = TBS400_PRONT_MASTER.RetornaTodosRegistros().ToList().OrderByDescending(x => x.ID_PRONT_MASTER).FirstOrDefault();
                string nuRegis = (DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + ultimoElemento.ID_PRONT_MASTER + 1).ToString();
                tbs400.NU_REGIS = nuRegis;
                tbs400.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                
                tbs400.TBS401_PRONT_INTENS = null;
                TBS400_PRONT_MASTER.SaveOrUpdate(tbs400);
                int qual = !string.IsNullOrEmpty(ddlQualifPront.Text) ? int.Parse(ddlQualifPront.Text) : 0;
                CarregarModalProntuCon(int.Parse(drpPacienteProntuCon.SelectedValue), qual, DateTime.Parse(txtIniPront.Text), DateTime.Parse(txtFimPront.Text));
                txtCadObsProntuCon.Text = "";

                divObsProntuCon.InnerHtml = hidIdProntuCon.Value = "";
                carregarQualificacaoProntuario();
                txtNumPasta.Text = "";
                txtNumPront.Text = "";
                txtPacienteProntuCon.Text = "";
                drpPacienteProntuCon.DataSource = null;
                drpPacienteProntuCon.DataBind();
                ddlQualifPront.SelectedValue = "";
                txtIniPront.Text = DateTime.Now.AddDays(-15).ToShortDateString();
                txtFimPront.Text = DateTime.Now.ToShortDateString();
            }
            catch (Exception ex) { AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message); }
        }

        protected void lnkbImprimirProntuCon_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(drpPacienteProntuCon.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o paciente para salvar o prontuário");
                    //AbreModalPadrao("AbreModalProntuCon();");
                    return;
                }

                #region Salvar Laudo

                var tbs400 = new TBS400_PRONT_MASTER();

                if (!String.IsNullOrEmpty(hidIdProntuCon.Value))
                {
                    tbs400 = TBS400_PRONT_MASTER.RetornaPelaChavePrimaria(int.Parse(hidIdProntuCon.Value));

                    //Caso tenha alterado algum dado do laudo atual ele salva como um novo laudo
                    //caso contrario só carrega as entidades para emitir o relatório
                    if (tbs400 != null)
                    {
                        tbs400 = new TBS400_PRONT_MASTER();
                        SalvarProntuCon(tbs400);
                    }
                }
                else
                    SalvarProntuCon(tbs400);

                CarregarModalProntuCon(int.Parse(drpPacienteProntuCon.SelectedValue), 0, DateTime.Parse(txtIniPront.Text), DateTime.Parse(txtFimPront.Text));

                #endregion

                //RptLaudo rpt = new RptLaudo();
                //var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
                //var lRetorno = rpt.InitReport(tbs403.DE_TITULO, infos, LoginAuxili.CO_EMP, tbs403.TB07_ALUNO.CO_ALU, tbs403.DE_LAUDO, tbs403.DT_LAUDO, tbs403.TB03_COLABOR.CO_COL);

                //GerarRelatorioPadrão(rpt, lRetorno);
            }
            catch { }
        }

        protected void imgBRel_OnClick(object sender, EventArgs e) 
        {
            try
            {
                string pasta = txtNumPasta.Text;
                int nire = !string.IsNullOrEmpty(txtNumPront.Text) ? int.Parse(txtNumPront.Text) : 0;
                int pac = !string.IsNullOrEmpty(drpPacienteProntuCon.SelectedValue) ? int.Parse(drpPacienteProntuCon.SelectedValue) : 0;
                int qual = !string.IsNullOrEmpty(ddlQualifPront.SelectedValue) ? int.Parse(ddlQualifPront.SelectedValue) : 0;
                DateTime? ini = !string.IsNullOrEmpty(txtIniPront.Text) ? DateTime.Parse(txtIniPront.Text) : (DateTime?)null;
                DateTime? fim = !string.IsNullOrEmpty(txtFimPront.Text) ? DateTime.Parse(txtFimPront.Text) : (DateTime?)null;

                var tb07 = TB07_ALUNO.RetornaTodosRegistros()
                            .Where(x => !string.IsNullOrEmpty(pasta) ? x.DE_PASTA_CONTR == pasta : false
                                   || pac > 0 ? x.CO_ALU == pac : false
                                   || nire > 0 ? x.NU_NIRE == nire : false)
                            .Select(x => new
                            {
                                x.NU_NIRE,
                                x.CO_ALU,
                                x.NO_ALU,
                                x.DE_PASTA_CONTR
                            })
                            .FirstOrDefault();

                if (tb07 != null)
                {
                    var tbs400 = TBS400_PRONT_MASTER.RetornaTodosRegistros()
                        .Where(x => (x.CO_ALU == pac)
                               && (qual > 0 ? x.TBS418_CLASS_PRONT.ID_CLASS_PRONT == qual : 0 == 0)
                               && (ini.HasValue ? EntityFunctions.TruncateTime(x.DT_CADAS) >= EntityFunctions.TruncateTime(ini.Value) : 0 == 0)
                               && (fim.HasValue ? EntityFunctions.TruncateTime(x.DT_CADAS) <= EntityFunctions.TruncateTime(fim.Value) : 0 == 0)
                               ).ToList();

               var idPront = new List<int>();

                if (tbs400 != null)
                {
                    foreach (var it in tbs400)
                    {
                        idPront.Add(it.ID_PRONT_MASTER);
                    }
                }
                    txtNumPront.Text = tb07.NU_NIRE.toNire();
                    txtNumPasta.Text = tb07.DE_PASTA_CONTR;
                    RptProntConvencional rpt = new RptProntConvencional();
                    var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
                    var lRetorno = rpt.InitReport(tb07.CO_ALU, idPront, infos, LoginAuxili.CO_EMP, qual, ini, fim);

                    GerarRelatorioPadrão(rpt, lRetorno);
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Paciente não encontrado!");
                }
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, ex.Message);
            }
        }

        private void GerarRelatorioPadrão(DevExpress.XtraReports.UI.XtraReport rpt, int lRetorno)
        {
            if (lRetorno == 0)
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro na geração do Relatório! Tente novamente.");
            else if (lRetorno < 0)
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não existem dados para a impressão do formulário solicitado.");
            else
            {
                Session["Report"] = rpt;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
                //----------------> Limpa a var de sessão com o url do relatório.
                Session.Remove("URLRelatorio");

                //----------------> Limpa a ref da url utilizada para carregar o relatório.
                PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                isreadonly.SetValue(this.Request.QueryString, false, null);
                isreadonly.SetValue(this.Request.QueryString, true, null);
            }
        }

        #endregion       
    }
}
//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Saúde
// PROGRAMADOR: Equipe Desenvolvimento
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR  |   O.S.  | DESCRIÇÃO RESUMIDA
// ---------+-----------------------+---------+--------------------------------
// 16/11/16 | Samira Lira           |         | Criação da funcionalidade para atendimento de serviços ambulatoriais
// ---------+-----------------------+---------+--------------------------------

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
using System.Data;
using Resources;
using System.Data.Objects;
using System.IO;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8200_CtrlExames;
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario;
using System.Drawing;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8260_Atendimento._8273_AtendimentoServFarmaceuticos
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
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            Page.Form.Enctype = "multipart/form-data";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string data = DateTime.Now.ToString("dd/MM/yyyy");
                string ontem = DateTime.Now.ToString("14/11/2016");

                IniPeriAG.Text = ontem;
                FimPeriAG.Text = data;

                carregarPacientes();
                carregarProfissionais();
                carregarGridAgendamentos();
            }
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            try
            {
                if (grdItensServAmbulatorias.Rows.Count > 0)
                {
                    foreach (GridViewRow row in grdItensServAmbulatorias.Rows)
                    {
                        CheckBox chk = (CheckBox)row.Cells[0].FindControl("chkSelectServAmbulatorial");
                        DropDownList ddlProfi = (DropDownList)row.FindControl("ddlProfiAmbul");
                        DropDownList ddlLocal = (DropDownList)row.FindControl("ddlLocalAmbul");
                        HiddenField hid = (HiddenField)row.FindControl("hidId");
                        HiddenField hidItem = (HiddenField)row.FindControl("hidIdItem");
                        int idItem = int.Parse(hidItem.Value);

                        if (ddlLocal.SelectedValue.Equals("0"))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Selecione o atual local onde o serviço ambulatorial está sendo feito.");
                            ddlLocal.Focus();
                            return;
                        }

                        if (ddlProfi.SelectedValue.Equals("0"))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Selecione qual profissional está executando o serviço ambulatório.");
                            ddlProfi.Focus();
                            return;
                        }
                        if (chk.Enabled)
                        {
                            if (string.IsNullOrEmpty(hid.Value))
                            {
                                string txtDTEntrega = ((TextBox)row.FindControl("txtEntrega")).Text;
                                string txtHREntrega = ((TextBox)row.FindControl("txtHoraEntrega")).Text;
                                string txtDTCadastro = ((TextBox)row.FindControl("txtCadastro")).Text;
                                string txtHRCadastro = ((TextBox)row.FindControl("txtHoraCadastro")).Text;
                                string txtObservacao = ((TextBox)row.FindControl("txtObsItem")).Text;
                                var atend = TBS426_SERVI_AMBUL.RetornaPelaChavePrimaria(int.Parse(hidServ.Value));

                                TBS428_APLIC_SERVI_AMBUL tbs428 = new TBS428_APLIC_SERVI_AMBUL();
                                tbs428.IS_APLIC_SERVI_AMBUL = chk.Checked ? "S" : "N";
                               // tbs428 = TBS427_SERVI_AMBUL_ITENS.RetornaPelaChavePrimaria(idItem);
                                tbs428.CO_COL_APLIC = int.Parse(ddlProfi.SelectedValue);
                                tbs428.CO_EMP_APLIC = LoginAuxili.CO_EMP;
                                tbs428.TB14_DEPTO = TB14_DEPTO.RetornaPelaChavePrimaria(int.Parse(ddlLocal.SelectedValue));
                                tbs428.DT_APLIC__SERVI_AMBUL = Convert.ToDateTime((txtDTCadastro + " " + txtHRCadastro));
                                if (!String.IsNullOrEmpty(txtDTEntrega))
                                {
                                    tbs428.DT_ENTREGA = Convert.ToDateTime((txtDTEntrega.Substring(0, 10) + " " + txtHREntrega));
                                }
                                tbs428.DT_PEDIDO = atend.DT_CADASTRO;

                                tbs428.OBSERVACAO = txtObservacao;
                                tbs428.IP_APLIC_SERVI_AMBUL = Request.UserHostAddress;

                                TBS428_APLIC_SERVI_AMBUL.SaveOrUpdate(tbs428, true);
                            }
                            else
                            {
                                int id = int.Parse(hid.Value);
                                var tbs428 = TBS428_APLIC_SERVI_AMBUL.RetornaPelaChavePrimaria(id);
                                string txtDTEntrega = ((TextBox)row.FindControl("txtEntrega")).Text;
                                string txtHREntrega = ((TextBox)row.FindControl("txtHoraEntrega")).Text;
                                string txtDTCadastro = ((TextBox)row.FindControl("txtCadastro")).Text;
                                string txtHRCadastro = ((TextBox)row.FindControl("txtHoraCadastro")).Text;
                                string txtObservacao = ((TextBox)row.FindControl("txtObsItem")).Text;
                                var atend = TBS426_SERVI_AMBUL.RetornaPelaChavePrimaria(int.Parse(hidServ.Value));

                                tbs428.TBS427_SERVI_AMBUL_ITENS = TBS427_SERVI_AMBUL_ITENS.RetornaPelaChavePrimaria(idItem);
                                tbs428.IS_APLIC_SERVI_AMBUL = chk.Checked ? "S" : "N";
                                tbs428.CO_COL_APLIC = int.Parse(ddlProfi.SelectedValue);
                                tbs428.CO_EMP_APLIC = LoginAuxili.CO_EMP;
                                tbs428.TB14_DEPTO = TB14_DEPTO.RetornaPelaChavePrimaria(int.Parse(ddlLocal.SelectedValue));
                                tbs428.DT_APLIC__SERVI_AMBUL = Convert.ToDateTime((txtDTCadastro + " " + txtHRCadastro));
                                if (!String.IsNullOrEmpty(txtDTEntrega))
                                {
                                    tbs428.DT_ENTREGA = Convert.ToDateTime((txtDTEntrega.Substring(0, 10) + " " + txtHREntrega));
                                }
                                tbs428.DT_PEDIDO = atend.DT_CADASTRO;

                                tbs428.OBSERVACAO = txtObservacao;
                                tbs428.IP_APLIC_SERVI_AMBUL = Request.UserHostAddress;

                                TBS428_APLIC_SERVI_AMBUL.SaveOrUpdate(tbs428, true);
                            }
                            AuxiliPagina.EnvioMensagemSucesso(this.Page, "Registro salvo/alterado com sucesso!");
                        }
                    }
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhuma opção de Serviço Ambulatorial foi aplicada.");
                    return;
                }
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi possível salvar/alterar o registro, por favor tente novamente ou entre em contato com o suporte.");
                return;
            }
        }

        #endregion

        #region Carregamentos

        private void carregarPacientes()
        {
            DateTime dtIni = (!string.IsNullOrEmpty(IniPeriAG.Text) ? DateTime.Parse(IniPeriAG.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(FimPeriAG.Text) ? DateTime.Parse(FimPeriAG.Text) : DateTime.Now);


            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       where ((EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni))
                       && (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim)))
                       && (((LoginAuxili.CLASSIFICACAO_USU_LOGADO != "M") && (LoginAuxili.FLA_PROFESSOR == "S")) ? tbs174.CO_COL == LoginAuxili.CO_COL : 0 == 0)
                       && (!String.IsNullOrEmpty(tbs174.FL_JUSTI_CANCE) ? tbs174.FL_JUSTI_CANCE != "M" : "" == "")
                       select new
                       {
                           tbs174.CO_ALU,
                           tb07.NO_ALU
                       }).OrderBy(x => x.NO_ALU).ToList();

            drpPacientes.DataSource = res;
            drpPacientes.DataTextField = "NO_ALU";
            drpPacientes.DataValueField = "CO_ALU";
            drpPacientes.DataBind();

            drpPacientes.Items.Insert(0, new ListItem("Todos", "0"));
        }

        private void carregarProfissionais()
        {
            int coEmp = LoginAuxili.CO_EMP;

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       where tb03.CO_EMP == coEmp
                       && tb03.FLA_PROFESSOR == "S"
                       && tb03.CO_SITU_COL == "ATI"
                       select new
                       {
                           tb03.NO_COL,
                           tb03.CO_COL
                       }).OrderBy(w => w.NO_COL).ToList();

            drpProfissional.DataValueField = "CO_COL";
            drpProfissional.DataTextField = "NO_COL";
            drpProfissional.DataSource = res;
            drpProfissional.DataBind();
            drpProfissional.Items.Insert(0, new ListItem("Todos", "0"));
        }

        private void carregarProfissionaisAmbul(DropDownList ddl)
        {
            int coEmp = LoginAuxili.CO_EMP;

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       where tb03.CO_EMP == coEmp
                       && tb03.FL_TECNICO.Equals("S")
                       && tb03.CO_SITU_COL.Equals("ATI")
                       select new
                       {
                           tb03.NO_COL,
                           tb03.CO_COL
                       }).OrderBy(w => w.NO_COL).ToList();

            ddl.DataValueField = "CO_COL";
            ddl.DataTextField = "NO_COL";
            ddl.DataSource = res;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        private void carregarLocalAmbul(DropDownList ddl)
        {
            int coEmp = LoginAuxili.CO_EMP;

            var res = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                       where tb14.FL_AMBUL.Equals("S")
                       && tb14.CO_SITUA_DEPTO.Equals("A")
                       select new
                       {
                           tb14.NO_DEPTO,
                           tb14.CO_DEPTO
                       }).OrderBy(w => w.NO_DEPTO).ToList();

            ddl.DataValueField = "CO_DEPTO";
            ddl.DataTextField = "NO_DEPTO";
            ddl.DataSource = res;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Local", "0"));
        }

        private void carregarGridAgendamentos()
        {
            DateTime dtIni = (!string.IsNullOrEmpty(IniPeriAG.Text) ? DateTime.Parse(IniPeriAG.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(FimPeriAG.Text) ? DateTime.Parse(FimPeriAG.Text) : DateTime.Now);
            int paciente = !string.IsNullOrEmpty(drpPacientes.SelectedValue) ? int.Parse(drpPacientes.SelectedValue) : 0;
            int profissional = !string.IsNullOrEmpty(drpProfissional.SelectedValue) ? int.Parse(drpProfissional.SelectedValue) : 0;

            var res = (from tbs426 in TBS426_SERVI_AMBUL.RetornaTodosRegistros()
                       join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tbs426.TBS174_AGEND_HORAR.ID_AGEND_HORAR equals tbs174.ID_AGEND_HORAR
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                       join tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs390.TBS174_AGEND_HORAR.ID_AGEND_HORAR
                       join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs390.TB14_DEPTO.CO_DEPTO equals tb14.CO_DEPTO
                       where EntityFunctions.TruncateTime(tbs426.DT_CADASTRO) >= EntityFunctions.TruncateTime(dtIni)
                            && EntityFunctions.TruncateTime(tbs426.DT_CADASTRO) <= EntityFunctions.TruncateTime(dtFim)
                            && tb07.CO_ALU == (paciente > 0 ? paciente : tb07.CO_ALU)
                            && tb03.CO_COL == (profissional > 0 ? profissional : tb03.CO_COL)
                       select new Paciente
                       {
                           nomePaciente = tb07.NO_ALU,
                           coPaciente = tb07.CO_ALU,
                           coProfissional = tb03.CO_COL,
                           nomeProfissional = tb03.NO_COL,
                           dataCadastro = tbs426.DT_CADASTRO,
                           numRegistroConsulta = tbs174.NU_REGIS_CONSUL,
                           classRisco = tbs390.CO_TIPO_RISCO,
                           idAgendaAtendimento = tbs174.ID_AGEND_HORAR,
                           idServAmbulatorial = tbs426.ID_SERVI_AMBUL,
                           especProfissional = tb03.DE_FUNC_COL,
                           Local = tb14.CO_SIGLA_DEPTO,
                           sexoPaciente = tb07.CO_SEXO_ALU,
                           dataNascimento = tb07.DT_NASC_ALU
                       }).OrderByDescending(x => x.dataCadastro);


            grdAgendamentosPacientes.DataSource = res;
            grdAgendamentosPacientes.DataBind();

            foreach (GridViewRow row in grdAgendamentosPacientes.Rows)
            {
                CheckBox chk = (CheckBox)row.Cells[0].FindControl("chkSelectPaciente");
                chk.CssClass = "noText";

                HiddenField hid = (HiddenField)row.Cells[5].FindControl("hidClassRisco");
                TextBox txt = (TextBox)row.Cells[5].FindControl("classRiscCorSelec");
                txt.Enabled = false;
                switch (hid.Value)
                {
                    case "0":
                        txt.BackColor = Color.White;
                        txt.ToolTip = "Sem classificação.";
                        break;
                    case "1":
                        txt.BackColor = Color.Red;
                        txt.ToolTip = "Emergência.";
                        break;
                    case "2":
                        txt.BackColor = Color.Orange;
                        txt.ToolTip = "Muito Urgente.";
                        break;
                    case "3":
                        txt.BackColor = Color.Yellow;
                        txt.ToolTip = "Urgente.";
                        break;
                    case "4":
                        txt.BackColor = Color.Green;
                        txt.ToolTip = "Pouco Urgente.";
                        break;
                    case "5":
                        txt.BackColor = Color.Blue;
                        txt.ToolTip = "Não Urgente.";
                        break;
                    default:
                        txt.BackColor = Color.White;
                        txt.ToolTip = "Sem classificação.";
                        break;
                }

            }

        }

        private void carregarGridItensServAmbulatoriais(int idServAmbulatorial)
        {
            var procMedico = (from tbs427 in TBS427_SERVI_AMBUL_ITENS.RetornaTodosRegistros()
                              join tbs426 in TBS426_SERVI_AMBUL.RetornaTodosRegistros() on tbs427.TBS426_SERVI_AMBUL.ID_SERVI_AMBUL equals tbs426.ID_SERVI_AMBUL
                              where tbs427.TBS426_SERVI_AMBUL.ID_SERVI_AMBUL == idServAmbulatorial
                              select new
                              {
                                  tbs427.TIPO_SERVI_AMBUL,
                                  idServ = tbs427.TIPO_SERVI_AMBUL.Equals("P") ? tbs427.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE : tbs427.TB90_PRODUTO.CO_PROD,
                                  tbs427.ID_LISTA_SERVI_AMBUL,

                              }).ToList();


            List<ItensServiAmbulatoriais> itensServAmbulatoriais = new List<ItensServiAmbulatoriais>();
            foreach (var p in procMedico)
            {
                ItensServiAmbulatoriais item = new ItensServiAmbulatoriais();
                if (p.TIPO_SERVI_AMBUL.Equals("P"))
                {
                    var tbs356 = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(p.idServ);
                    item.nomeItem = tbs356 != null ? tbs356.NM_PROC_MEDI : "-";
                    item.descItem = tbs356 != null ? tbs356.DE_OBSE_PROC_MEDI : "-";

                    tbs356.TBS353_VALOR_PROC_MEDIC_PROCE.Load();
                    if (tbs356.TBS353_VALOR_PROC_MEDIC_PROCE != null && tbs356.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(x => x.FL_STATU == "A") != null)
                        item.valorItem = tbs356.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(x => x.FL_STATU == "A").VL_BASE;
                }
                else
                {
                    var tb90 = TB90_PRODUTO.RetornaPeloCoProd(p.idServ);
                    tb90.TBS457_CLASS_TERAPReference.Load();
                    item.nomeItem = tb90 != null ? tb90.TBS457_CLASS_TERAP != null ? tb90.NO_PROD_RED + " (" + tb90.TBS457_CLASS_TERAP.DE_CLASS_TERAP + ")" : tb90.NO_PROD_RED : "-";
                    item.descItem = tb90 != null ? tb90.DES_PROD : "-";
                    item.valorItem = tb90 != null ? tb90.VL_CUSTO : 0;
                }
                item.idItemServAmbulatorial = p.ID_LISTA_SERVI_AMBUL;

                var tbs428 = TBS428_APLIC_SERVI_AMBUL.RetornaTodosRegistros().Where(x => x.TBS427_SERVI_AMBUL_ITENS.ID_LISTA_SERVI_AMBUL == p.ID_LISTA_SERVI_AMBUL).FirstOrDefault();

                tbs428.TBS427_SERVI_AMBUL_ITENSReference.Load();

                if (tbs428 != null)
                {
                    item.idItem = tbs428.ID_APLIC_SERVI_AMBUL;
                    item.Efetuado = tbs428.IS_APLIC_SERVI_AMBUL.Equals("S") ? true : false;
                    item.DisableOk = tbs428.IS_APLIC_SERVI_AMBUL.Equals("S") ? false : true;
                    item.complItem = tbs428.TBS427_SERVI_AMBUL_ITENS.DE_COMPL != null ? tbs428.TBS427_SERVI_AMBUL_ITENS.DE_COMPL : "";
                    item.DT_ENTREGA = tbs428.DT_ENTREGA;
                    item.DisableDTEntrega = tbs428.DT_ENTREGA.HasValue && !item.DisableOk ? false : true;
                    item.DT_PEDIDO = tbs428.DT_PEDIDO;
                    item.DisableDTPedido = tbs428.DT_PEDIDO.HasValue && !item.DisableOk ? false : true;
                    item.obsItem = tbs428.OBSERVACAO;
                    item.DT_CADASTRO = tbs428.IS_APLIC_SERVI_AMBUL.Equals("S") ? tbs428.DT_APLIC__SERVI_AMBUL : (DateTime?)null;
                    item.idItemLocal = tbs428.TB14_DEPTO != null ? tbs428.TB14_DEPTO.CO_DEPTO : 0;
                    item.idItemCoCol = tbs428.CO_COL_APLIC;

                    item.tipoItem = tbs428.TBS427_SERVI_AMBUL_ITENS.TIPO_SERVI_AMBUL;
                }
                else
                {
                    item.DisableOk = true;
                    //campoData
                    item.DisableDTEntrega = true;
                    item.DisableDTPedido = true;
                }

                itensServAmbulatoriais.Add(item);

            }

            grdItensServAmbulatorias.DataSource = itensServAmbulatoriais;
            grdItensServAmbulatorias.DataBind();

            foreach (GridViewRow li in grdItensServAmbulatorias.Rows)
            {
                var ddlProfiAmbul = (DropDownList)li.FindControl("ddlProfiAmbul");
                carregarProfissionaisAmbul(ddlProfiAmbul);

                var ddlLocalAmbul = (DropDownList)li.FindControl("ddlLocalAmbul");
                carregarLocalAmbul(ddlLocalAmbul);

                var hidItem = (HiddenField)li.FindControl("hidId");
                if (!String.IsNullOrEmpty(hidItem.Value))
                {
                    var tbs428 = TBS428_APLIC_SERVI_AMBUL.RetornaPelaChavePrimaria(int.Parse(hidItem.Value));
                    tbs428.TB14_DEPTOReference.Load();

                    ddlLocalAmbul.SelectedValue = tbs428.TB14_DEPTO != null ? tbs428.TB14_DEPTO.CO_DEPTO.ToString() : "0";
                    ddlProfiAmbul.SelectedValue = tbs428.CO_COL_APLIC.ToString();
                }

            }
        }

        #endregion

        #region Funções de Campo

        protected void chkSelectPaciente_OnCheckedChanged(object sender, EventArgs e)
        {
            if (grdAgendamentosPacientes.Rows.Count > 0)
            {
                foreach (GridViewRow row in grdAgendamentosPacientes.Rows)
                {
                    CheckBox chk = (CheckBox)row.Cells[0].FindControl("chkSelectPaciente");
                    if (chk.Checked)
                    {
                        HiddenField hid = (HiddenField)row.FindControl("hidIdServAmbulatorial");
                        HiddenField hidAtende = (HiddenField)row.FindControl("hidIdAtenAgend");
                        int id = int.Parse(hid.Value);
                        CheckBox thisCheck = sender as CheckBox;
                        int idThis = int.Parse(thisCheck.Text);
                        if (id == idThis)
                        {
                            hidServ.Value = hid.Value;
                            carregarGridItensServAmbulatoriais(id);
                            liServAmbulatoriais.Visible = true;
                        }
                        else
                        {
                            chk.Checked = false;
                        }
                    }
                }
            }
        }

        protected void chkSelectServAmbulatorial_OnCheckedChanged(object sender, EventArgs e)
        { }

        protected void imgPesqAgendamentos_Click(object sender, ImageClickEventArgs e)
        {
            carregarGridAgendamentos();
        }

        protected void lnkbtnSalvarItem_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton atual = (LinkButton)sender;
                LinkButton lnk;
                if (grdItensServAmbulatorias.Rows.Count > 0)
                {
                    foreach (GridViewRow row in grdItensServAmbulatorias.Rows)
                    {
                        lnk = (LinkButton)row.FindControl("btnSalvarItem");
                        CheckBox chk = (CheckBox)row.FindControl("chkSelectServAmbulatorial");
                        DropDownList ddlProfi = (DropDownList)row.FindControl("ddlProfiAmbul");
                        DropDownList ddlLocal = (DropDownList)row.FindControl("ddlLocalAmbul");
                        HiddenField hid = (HiddenField)row.FindControl("hidId");
                        HiddenField hidItem = (HiddenField)row.FindControl("hidIdItem");
                        int idItem = int.Parse(hidItem.Value);

                        if (ddlLocal.SelectedValue.Equals("0"))
                        {
                            AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Selecione o atual local onde o serviço ambulatorial está sendo feito.");
                            ddlLocal.Focus();
                            return;
                        }

                        if (ddlProfi.SelectedValue.Equals("0"))
                        {
                            AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Selecione qual profissional está executando o serviço ambulatório.");
                            ddlProfi.Focus();
                            return;
                        }

                        if (lnk.ClientID == atual.ClientID)
                        {
                            if (string.IsNullOrEmpty(hid.Value))
                            {
                                string txtDTEntrega = ((TextBox)row.FindControl("txtEntrega")).Text;
                                string txtHREntrega = ((TextBox)row.FindControl("txtHoraEntrega")).Text;
                                string txtDTCadastro = ((TextBox)row.FindControl("txtCadastro")).Text;
                                string txtHRCadastro = ((TextBox)row.FindControl("txtHoraCadastro")).Text;
                                string txtObservacao = ((TextBox)row.FindControl("txtObsItem")).Text;
                                var atend = TBS426_SERVI_AMBUL.RetornaPelaChavePrimaria(int.Parse(hidServ.Value));

                                TBS428_APLIC_SERVI_AMBUL tbs428 = new TBS428_APLIC_SERVI_AMBUL();
                                tbs428.IS_APLIC_SERVI_AMBUL = chk.Checked ? "S" : "N";
                                tbs428.TBS427_SERVI_AMBUL_ITENS = TBS427_SERVI_AMBUL_ITENS.RetornaPelaChavePrimaria(idItem);
                                tbs428.CO_COL_APLIC = int.Parse(ddlProfi.SelectedValue);
                                tbs428.CO_EMP_APLIC = LoginAuxili.CO_EMP;
                                tbs428.TB14_DEPTO = TB14_DEPTO.RetornaPelaChavePrimaria(int.Parse(ddlLocal.SelectedValue));
                                tbs428.DT_APLIC__SERVI_AMBUL = Convert.ToDateTime((txtDTCadastro + " " + txtHRCadastro));
                                if (!String.IsNullOrEmpty(txtDTEntrega))
                                {
                                    tbs428.DT_ENTREGA = Convert.ToDateTime((txtDTEntrega.Substring(0, 10) + " " + txtHREntrega));
                                }
                                tbs428.DT_PEDIDO = atend.DT_CADASTRO;

                                tbs428.OBSERVACAO = txtObservacao;
                                tbs428.IP_APLIC_SERVI_AMBUL = Request.UserHostAddress;

                                TBS428_APLIC_SERVI_AMBUL.SaveOrUpdate(tbs428, true);
                            }
                            else
                            {
                                int id = int.Parse(hid.Value);
                                var tbs428 = TBS428_APLIC_SERVI_AMBUL.RetornaPelaChavePrimaria(id);
                                string txtDTEntrega = ((TextBox)row.FindControl("txtEntrega")).Text;
                                string txtHREntrega = ((TextBox)row.FindControl("txtHoraEntrega")).Text;
                                string txtDTCadastro = ((TextBox)row.FindControl("txtCadastro")).Text;
                                string txtHRCadastro = ((TextBox)row.FindControl("txtHoraCadastro")).Text;
                                string txtObservacao = ((TextBox)row.FindControl("txtObsItem")).Text;
                                var atend = TBS426_SERVI_AMBUL.RetornaPelaChavePrimaria(int.Parse(hidServ.Value));

                                tbs428.TBS427_SERVI_AMBUL_ITENS = TBS427_SERVI_AMBUL_ITENS.RetornaPelaChavePrimaria(idItem);
                                tbs428.IS_APLIC_SERVI_AMBUL = chk.Checked ? "S" : "N";
                                tbs428.CO_COL_APLIC = int.Parse(ddlProfi.SelectedValue);
                                tbs428.CO_EMP_APLIC = LoginAuxili.CO_EMP;
                                tbs428.TB14_DEPTO = TB14_DEPTO.RetornaPelaChavePrimaria(int.Parse(ddlLocal.SelectedValue));
                                tbs428.DT_APLIC__SERVI_AMBUL = Convert.ToDateTime((txtDTCadastro + " " + txtHRCadastro));
                                if (!String.IsNullOrEmpty(txtDTEntrega))
                                {
                                    tbs428.DT_ENTREGA = Convert.ToDateTime((txtDTEntrega.Substring(0, 10) + " " + txtHREntrega));
                                }
                                tbs428.DT_PEDIDO = atend.DT_CADASTRO;

                                tbs428.OBSERVACAO = txtObservacao;
                                tbs428.IP_APLIC_SERVI_AMBUL = Request.UserHostAddress;

                                TBS428_APLIC_SERVI_AMBUL.SaveOrUpdate(tbs428, true);
                            }
                            AuxiliPagina.EnvioMensagemSucesso(this.Page, "Registro de serviço salvo/alterado com sucesso!");
                        }
                    }
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhuma opção de Serviço Ambulatorial foi aplicada.");
                    return;
                }
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi possível salvar/alterar o registro, por favor tente novamente ou entre em contato com o suporte.");
                return;
            }
        }

        protected void lnkBtnRelatorio_Click(object sender, EventArgs e)
        {
            if (grdAgendamentosPacientes.Rows.Count > 0)
            {
                foreach (GridViewRow row in grdAgendamentosPacientes.Rows)
                {
                    CheckBox chk = (CheckBox)row.Cells[0].FindControl("chkSelectPaciente");
                    if (chk.Checked)
                    {
                        int lRetorno = 0;

                        HiddenField hidServ = (HiddenField)row.Cells[1].FindControl("hidIdServAmbulatorial");
                        HiddenField hidCoAlu = (HiddenField)row.Cells[1].FindControl("hidCoAlu");
                        HiddenField hidCoAgend = (HiddenField)row.Cells[1].FindControl("hidIdAtenAgend");
                        int idServ = int.Parse(hidServ.Value);
                        int coAlu = int.Parse(hidCoAlu.Value);
                        int CoAgend = int.Parse(hidCoAgend.Value);

                        RptAmbulatorio rpt = new RptAmbulatorio();
                        lRetorno = rpt.InitReport(coAlu, idServ, CoAgend, LoginAuxili.CO_EMP);
                        GerarRelatorioPadrão(rpt, lRetorno);
                    }
                }
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor selecione um registro");
                return;
            }
        }
        #endregion

        #region Relatórios

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
                System.Reflection.PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                isreadonly.SetValue(this.Request.QueryString, false, null);
                isreadonly.SetValue(this.Request.QueryString, true, null);
            }
        }

        #endregion

        #region class Paciente/Itens

        public class Paciente
        {
            public string nomePaciente { get; set; }
            public string sexoPaciente { get; set; }
            public DateTime? dataNascimento { get; set; }
            public string idadePaciente
            {
                get
                {
                    return this.dataNascimento != null ? AuxiliFormatoExibicao.FormataDataNascimento(this.dataNascimento, AuxiliFormatoExibicao.ETipoDataNascimento.padraoSaudeCompleto) : "-";
                }
            }
            public int coPaciente { get; set; }
            public string nomeProfissional { get; set; }
            public int coProfissional { get; set; }
            public DateTime? dataCadastro { get; set; }
            public string numRegistroConsulta { get; set; }
            public string especProfissional { get; set; }
            public string Local { get; set; }
            public int idAgendaAtendimento { get; set; }
            public int idServAmbulatorial { get; set; }
            public int? classRisco { get; set; }
            public string risco
            {
                get
                {
                    return classRisco != null ? classRisco.ToString() : "0";
                }
            }
        }

        public class ItensServiAmbulatoriais
        {
            public int? idItem { get; set; }
            public int idItemServAmbulatorial { get; set; }
            public int idItemLocal { get; set; }
            public int idItemCoCol { get; set; }
            public string nomeItem { get; set; }
            public string nomeItemV
            {
                get
                {
                    return String.IsNullOrEmpty(this.complItem) ? this.nomeItem : this.nomeItem + " | " + this.complItem;
                }
            }
            public string tipoItem { get; set; }
            public string tipoItemV
            {
                get
                {
                    return this.tipoItem.Equals("P") ? "PRO" : "MED";
                }
            }
            public string descItem { get; set; }
            public string complItem { get; set; }
            public Decimal? valorItem { get; set; }
            public bool Efetuado { get; set; }
            public DateTime? DT_PEDIDO { get; set; }
            public string dataPedidoItem
            {
                get
                {
                    return DT_PEDIDO.HasValue ? DT_PEDIDO.ToString() : "";
                }
            }
            public DateTime? DT_ENTREGA { get; set; }
            public string dataEntregaItem
            {
                get
                {
                    return DT_ENTREGA.HasValue ? DT_ENTREGA.ToString() : DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
            public string horaEntregaItem
            {
                get
                {
                    return DT_ENTREGA.HasValue ? DT_ENTREGA.Value.ToString("HH:mm") : DateTime.Now.ToString("HH:mm");
                }
            }
            public string obsItem { get; set; }
            public bool DisableOk { get; set; }
            public bool DisableDTEntrega { get; set; }
            public bool DisableDTPedido { get; set; }
            public DateTime? DT_CADASTRO { get; set; }
            public string dataCadastro
            {
                get
                {
                    return DT_CADASTRO.HasValue ? DT_CADASTRO.Value.ToShortDateString() : DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
            public string horaCadastro
            {
                get
                {
                    return DT_CADASTRO.HasValue ? DT_CADASTRO.Value.ToString("HH:mm") : DateTime.Now.ToString("HH:mm");
                }
            }
        }

        #endregion

    }
}
//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PGS - Portal Gestor Saúde
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: Atendimento
// SUBMÓDULO: Atendimento Internar
// DATA DE CRIAÇÃO: 12/01/2017
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//12/01/2017| Samira Lira                | Criação da página para atendimento no processo de internação hospitalar


using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
using System.Data.Objects;
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8260_Atendimento._8271_AgendaDePacienteAPlantonista
{
    public partial class Cadastro : System.Web.UI.Page
    {
        #region Eventos

        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregarUnidade();
                CarregaDepartamento();
                CarregaEspecialidade();
                txtDataIni.Text = DateTime.Now.ToShortDateString();
                txtDataFim.Text = DateTime.Now.AddDays(1).ToShortDateString();
                carregarGridProfissionalPlantao();
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //    --------> criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        /// <summary>
        /// Salva as informações nas tabelas cabíveis, TB108, TB07 e TBS195
        /// </summary>
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            SalvarEntidades();
        }

        #endregion

        #region Carregamentos

        private void SalvarEntidades()
        {
            try
            {
                int coPlantao = 0;
                int local = 0;
                foreach (GridViewRow row in grdProfi.Rows)
                {
                    var chk = (CheckBox)row.Cells[0].FindControl("ckSelect");
                    if (chk.Checked)
                    {
                        coPlantao = int.Parse(((HiddenField)row.Cells[0].FindControl("hidPlantao")).Value);
                        local = int.Parse(((DropDownList)row.Cells[7].FindControl("ddlLocalGrid")).SelectedValue);
                    }
                }

                int count = 0;
                foreach (GridViewRow row in grdPacientes.Rows)
                {
                    var chk = (CheckBox)row.Cells[0].FindControl("chkselect");
                    if (chk.Checked)
                    {
                        int idAgendProfMedic = int.Parse(((HiddenField)row.Cells[0].FindControl("hidIdProfAgendInter")).Value);
                        int idAgendInter = int.Parse(((HiddenField)row.Cells[0].FindControl("hidIdAtendInter")).Value);
                        var res = TBS455_AGEND_PROF_INTER.RetornaPelaChavePrimaria(idAgendProfMedic);
                        var tbs455 = res != null ? res : new TBS455_AGEND_PROF_INTER();

                        tbs455.CO_COL_CADAS = LoginAuxili.CO_COL;
                        tbs455.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        tbs455.CO_SITUA_AGEND_PROFI = "A";
                        tbs455.DE_ACOMP_PROFI = ((TextBox)row.Cells[7].FindControl("txtObsPaciente")).Text;
                        tbs455.DT_CADAS = DateTime.Now;
                        tbs455.FL_CLASS_ATEND = "N";
                        tbs455.HR_ACOMP_PROFI = TimeSpan.Parse(((TextBox)row.Cells[6].FindControl("txtHoraAtendimento")).Text);
                        tbs455.DT_ACOMP_PROFI = DateTime.Parse(((TextBox)row.Cells[5].FindControl("txtDataAtendimento")).Text);
                        tbs455.IP_CADAS = Request.UserHostAddress;
                        tbs455.TB159_AGENDA_PLANT_COLABOR = TB159_AGENDA_PLANT_COLABOR.RetornaPelaChavePrimaria(coPlantao);
                        string origem = ((HiddenField)row.Cells[0].FindControl("hidORIGEM")).Value;
                        if (origem.Equals("1"))
                        {
                            tbs455.TBS174_AGEND_HORAR = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgendInter);
                        }
                        else
                        {
                            tbs455.TBS451_INTER_REGIST = TBS451_INTER_REGIST.RetornaPelaChavePrimaria(idAgendInter);
                        }
                        TBS455_AGEND_PROF_INTER.SaveOrUpdate(tbs455, true);
                        count++;
                    }
                }
                if (count == 0)
                {
                    throw new ArgumentException("Nenhum registro foi salvo.");
                }
                AuxiliPagina.EnvioMensagemSucesso(this.Page, "Operação realizada com sucesso!");
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro: " + ex.Message);
                return;
            }
        }

        private void carregarGridProfissionalPlantao()
        {
            int unidade = !string.IsNullOrEmpty(ddlUnidPlant.SelectedValue) ? int.Parse(ddlUnidPlant.SelectedValue) : 0;
            int local = !string.IsNullOrEmpty(ddlLocalPlant.SelectedValue) ? int.Parse(ddlLocalPlant.SelectedValue) : 0;
            int especialidade = !string.IsNullOrEmpty(ddlEspecPlant.SelectedValue) ? int.Parse(ddlEspecPlant.SelectedValue) : 0;
            DateTime? dataini = !string.IsNullOrEmpty(txtDataIni.Text) ? DateTime.Parse(txtDataIni.Text) : (DateTime?)null;
            DateTime? datafim = !string.IsNullOrEmpty(txtDataFim.Text) ? DateTime.Parse(txtDataFim.Text) : (DateTime?)null;

            var res = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb159.TB03_COLABOR.CO_COL equals tb03.CO_COL
                       join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tb159.TB14_DEPTO.CO_DEPTO equals tb14.CO_DEPTO
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb03.CO_EMP equals tb25.CO_EMP
                       join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                       where tb03.FL_PERM_PLANT == "S"
                          && (unidade > 0 ? tb25.CO_EMP == unidade : 0 == 0)
                          && (local > 0 ? tb14.CO_DEPTO == local : 0 == 0)
                          && (especialidade > 0 ? tb63.CO_ESPECIALIDADE == especialidade : 0 == 0)
                          && (dataini.HasValue && datafim.HasValue ? (EntityFunctions.TruncateTime(tb159.DT_INICIO_PREV) >= (EntityFunctions.TruncateTime(dataini)) && (EntityFunctions.TruncateTime(tb159.DT_INICIO_PREV) <= EntityFunctions.TruncateTime(datafim))) : 0 == 0)
                       select (new ProfissionalPlantao
                       {
                           CO_COL = tb03.CO_COL,
                           CO_DEPT = tb14.CO_DEPTO,
                           CO_EMP = tb25.CO_EMP,
                           CO_ESPEC = tb63.CO_ESPEC,
                           CO_PLANT = tb159.CO_AGEND_PLANT_COLAB,
                           dtFimPlantao = tb159.DT_TERMIN_PREV,
                           dtIniPlantao = tb159.DT_INICIO_PREV,
                           MATR_COL = tb03.CO_MAT_COL,
                           NO_COL = tb03.CO_MAT_COL + " - " + tb03.NO_APEL_COL,
                           NO_DEPT = tb14.NO_DEPTO,
                           NO_EMP = tb25.sigla,
                           NO_ESPEC = tb63.NO_ESPECIALIDADE,
                           NU_TEL = tb03.NU_TELE_CELU_COL
                       })).OrderBy(x => x.NO_COL).ToList();


            grdProfi.DataSource = res;
            grdProfi.DataBind();

            grdPacientes.DataSource = null;
            grdPacientes.DataBind();
        }

        protected void grdProfi_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in grdProfi.Rows)
            {
                int coDepto = int.Parse(((HiddenField)row.Cells[0].FindControl("hidcoDept")).Value);
                int coEmp = int.Parse(((HiddenField)row.Cells[0].FindControl("hidEmp")).Value);

                DropDownList localGrid = (DropDownList)row.Cells[7].FindControl("ddlLocalGrid");

                CheckBox chk = (CheckBox)row.Cells[0].FindControl("ckSelect");
                AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
                trigger.ControlID = chk.UniqueID;
                trigger.EventName = "CheckedChanged";
                UpdatePanel1.Triggers.Add(trigger);

                var res = from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                          where coEmp != 0 ? tb14.TB25_EMPRESA.CO_EMP == coEmp : 0 == 0
                          select new { tb14.NO_DEPTO, tb14.CO_DEPTO };

                localGrid.DataSource = res;
                localGrid.DataTextField = "NO_DEPTO";
                localGrid.DataValueField = "CO_DEPTO";
                localGrid.DataBind();
                localGrid.Items.Insert(0, new ListItem("Todos", "0"));
                localGrid.SelectedValue = coDepto.ToString();
            }

            grdPacientes.DataSource = null;
            grdPacientes.DataBind();
            UpdatePanel0.Update();
            UpdatePanel1.Update();
        }

        private void carregarGridPacientes(int coDepto, DateTime dtIni, DateTime dtFim, int coPlantao)
        {
            var tbs455 = TBS455_AGEND_PROF_INTER.RetornaTodosRegistros()
                .Where(x => (x.TB159_AGENDA_PLANT_COLABOR.CO_AGEND_PLANT_COLAB == coPlantao) && (coDepto > 0 && x.TBS451_INTER_REGIST != null ? x.TBS451_INTER_REGIST.TB14_DEPTO.CO_DEPTO == coDepto : coDepto > 0 && x.TBS174_AGEND_HORAR != null ? x.TBS174_AGEND_HORAR.CO_DEPT == coDepto : 0 == 0)).ToList();

            //var tbs174 = TBS174_AGEND_HORAR.RetornaTodosRegistros()
            //    .Where(x => (x.DT_AGEND_HORAR >= dtIni && x.DT_AGEND_HORAR <= dtFim) && (coDepto > 0 ? x.ID_DEPTO_LOCAL_ATENDI == coDepto : 0 == 0) && x.CO_ALU.HasValue)
            //            .Select(z => new
            //                {
            //                    z.CO_ALU,
            //                    z.DT_AGEND_HORAR,
            //                    z.HR_AGEND_HORAR,
            //                    z.ID_AGEND_HORAR,
            //                    z.ID_DEPTO_LOCAL_ATENDI,
            //                    z.CO_COL
            //                }).ToList();

            var tbs451 = TBS451_INTER_REGIST.RetornaTodosRegistros().Where(w => w.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TB07_ALUNO.CO_SITU_ALU.Equals("H") && (coDepto > 0 ? w.TB14_DEPTO.CO_DEPTO == coDepto : 0 == 0))
                             .Select(z => new
                                 {
                                     z.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TB07_ALUNO.CO_ALU,
                                     z.DT_INTER,
                                     z.HR_INTER,
                                     z.ID_INTER_REGIS,
                                     z.TB14_DEPTO.CO_DEPTO,
                                     z.CO_COL_INTER
                                 }).ToList();

            List<AtendimentoPaciente> pacienteItens = new List<AtendimentoPaciente>();
            foreach (var item in tbs455)
            {
                TB07_ALUNO tb07 = null;
                TB03_COLABOR tb03 = null;
                item.TB159_AGENDA_PLANT_COLABORReference.Load();
                item.TB159_AGENDA_PLANT_COLABOR.TB14_DEPTOReference.Load();
                item.TBS174_AGEND_HORARReference.Load();
                item.TBS451_INTER_REGISTReference.Load();
                item.TBS451_INTER_REGIST.TB14_DEPTOReference.Load();
                int origemAtendInter = item.TBS451_INTER_REGIST != null ? 2 : 1;
                int departamanto = 0;

                if (item.TBS451_INTER_REGIST != null)
                {
                    if (item.TBS451_INTER_REGIST.TB14_DEPTO != null)
                    {
                        departamanto = item.TBS451_INTER_REGIST.TB14_DEPTO.CO_DEPTO;
                    }
                    else if (item.TBS174_AGEND_HORAR != null)
                    {
                        if (item.TBS174_AGEND_HORAR.CO_DEPT.HasValue)
                        {
                            departamanto = item.TBS174_AGEND_HORAR.CO_DEPT.Value;
                        }
                    }
                    tb03 = TB03_COLABOR.RetornaPeloCoCol(item.TBS451_INTER_REGIST.CO_COL_INTER);
                    item.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPIReference.Load();
                    item.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGENDReference.Load();
                    item.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TB07_ALUNOReference.Load();
                }
                else if (item.TBS174_AGEND_HORAR != null)
                {
                    if (item.TBS174_AGEND_HORAR.CO_DEPT.HasValue)
                    {
                        departamanto = item.TBS174_AGEND_HORAR.CO_DEPT.Value;
                    }
                    tb07 = TB07_ALUNO.RetornaPeloCoAlu(item.TBS174_AGEND_HORAR.CO_ALU.Value);
                }
                else
                {
                    if (item.TB159_AGENDA_PLANT_COLABOR.TB14_DEPTO != null)
                        departamanto = item.TB159_AGENDA_PLANT_COLABOR.TB14_DEPTO.CO_DEPTO;
                }

                TB14_DEPTO tb14 = TB14_DEPTO.RetornaPelaChavePrimaria(departamanto);

                var pac = new AtendimentoPaciente();
                pac.ano = tb07 != null ? tb07.DT_NASC_ALU.Value.Year : item.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TB07_ALUNO.DT_NASC_ALU.Value.Year;
                pac.CO_ALU = tb07 != null ? tb07.CO_ALU : item.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TB07_ALUNO.CO_ALU;
                pac.dtProvavel = item.DT_ACOMP_PROFI;
                pac.HR_ATEND = item.HR_ACOMP_PROFI.ToString();
                pac.ID_AGEND_INTER = tb07 != null ? item.TBS174_AGEND_HORAR.ID_AGEND_HORAR : item.TBS451_INTER_REGIST.ID_INTER_REGIS;
                pac.ID_AGEND_PROF_MEDIC = item.ID_AGEND_PROFI_INTER;
                pac.ida = item.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TB07_ALUNO.DT_NASC_ALU.HasValue ? item.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TB07_ALUNO.DT_NASC_ALU.Value : (DateTime?)null;
                pac.LOCAL = tb14 != null ? tb14.NO_DEPTO : null;
                pac.NO_ALU = tb07 != null ? tb07.NO_ALU : item.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TB07_ALUNO.NO_ALU;
                pac.MED_ORIGEM = tb03 != null ? tb03.CO_MAT_COL + " - " + tb03.NO_APEL_COL : "";
                //indica que tem origem no agendamento
                pac.OBSERVACAO = item.DE_ACOMP_PROFI;
                pac.ORIGEM = origemAtendInter;
                pac.MAT_ALU = item.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TB07_ALUNO.NU_NIRE;
                pac.SEXO = tb07 != null ? tb07.CO_SEXO_ALU : item.TBS451_INTER_REGIST.TBS448_ATEND_INTER_HOSPI.TBS390_ATEND_AGEND.TB07_ALUNO.CO_SEXO_ALU;
                pac.Check = true;

                pacienteItens.Add(pac);
            }

            //foreach (var item in tbs174)
            //{
            //    int coAlu = item.CO_ALU.HasValue ? item.CO_ALU.Value : 0;
            //    int coLocal = item.ID_DEPTO_LOCAL_ATENDI.HasValue ? item.ID_DEPTO_LOCAL_ATENDI.Value : 0;
            //    var Paciente = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
            //    var Depto = TB14_DEPTO.RetornaPelaChavePrimaria(coLocal);

            //    var pac = new AtendimentoPaciente();
            //    pac.ID_AGEND_PROF_MEDIC = 0;
            //    pac.ano = Paciente.DT_NASC_ALU.Value != null ? Paciente.DT_NASC_ALU.Value.Year : 0;
            //    pac.CO_ALU = coAlu;
            //    pac.dtProvavel = item.DT_AGEND_HORAR;
            //    pac.HR_ATEND = item.HR_AGEND_HORAR;
            //    pac.ID_AGEND_INTER = item.ID_AGEND_HORAR;
            //    pac.ida = pac.ano.HasValue ? DateTime.Now.Year - int.Parse(pac.ano.ToString()) : 0;
            //    pac.LOCAL = Depto != null ? Depto.NO_DEPTO : null;
            //    pac.NO_ALU = Paciente.NO_ALU;
            //    //indica que tem origem no agendamento
            //    pac.ORIGEM = 1;
            //    pac.SEXO = Paciente.CO_SEXO_ALU;
            //    pac.Check = false;
            //    pac.OBSERVACAO = "";

            //    if (!pacienteItens.Any(x => x.ID_AGEND_INTER == pac.ID_AGEND_INTER))
            //    {
            //        pacienteItens.Add(pac);   
            //    }                
            //}

            foreach (var item in tbs451)
            {
                var Paciente = TB07_ALUNO.RetornaPeloCoAlu(item.CO_ALU);
                var Depto = TB14_DEPTO.RetornaPelaChavePrimaria(item.CO_DEPTO);
                var Col = TB03_COLABOR.RetornaPeloCoCol(item.CO_COL_INTER);

                var pac = new AtendimentoPaciente();
                pac.ID_AGEND_PROF_MEDIC = 0;
                pac.ano = Paciente.DT_NASC_ALU.Value.Year;
                pac.CO_ALU = Paciente.CO_ALU;
                pac.dtProvavel = item.DT_INTER;
                pac.HR_ATEND = item.HR_INTER.Value.ToString();
                pac.ID_AGEND_INTER = item.ID_INTER_REGIS;
                pac.ida = Paciente.DT_NASC_ALU.HasValue ? Paciente.DT_NASC_ALU.Value : (DateTime?)null;
                pac.LOCAL = Depto != null ? Depto.NO_DEPTO : null;
                pac.NO_ALU = Paciente.NO_ALU;
                pac.MED_ORIGEM = Col.NO_APEL_COL;
                //indica que tem origem na internação
                pac.ORIGEM = 2;
                pac.MAT_ALU = Paciente.NU_NIRE;
                pac.SEXO = Paciente.CO_SEXO_ALU;
                pac.Check = false;
                pac.OBSERVACAO = "";

                if (!pacienteItens.Any(x => x.ID_AGEND_INTER == pac.ID_AGEND_INTER))
                {
                    pacienteItens.Add(pac);
                }
            }

            grdPacientes.DataSource = pacienteItens.DistinctBy(x => x.ID_AGEND_INTER).OrderBy(x => x.LOCAL).ThenBy(x => x.NO_ALU);
            grdPacientes.DataBind();

            UpdatePanel1.Update();
        }

        private void carregarUnidade()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidPlant, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        private void CarregaDepartamento()
        {
            int coEmp = ddlUnidPlant.SelectedValue != "" ? int.Parse(ddlUnidPlant.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaDepartamentos(ddlLocalPlant, coEmp, true);
        }

        private void CarregaEspecialidade()
        {
            AuxiliCarregamentos.CarregaEspeciacialidades(ddlEspecPlant, LoginAuxili.CO_EMP, null, true);
        }

        private void ExecutaJavaScript()
        {
            ScriptManager.RegisterStartupScript(
                UpdatePanel2,
                this.GetType(),
                "Acao",
                "carregaPadroes();",
                true
            );
        }

        private void AbreModalPadrao(string funcao)
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                funcao,
                true
            );
        }

        #endregion

        #region Funções de Campo
        protected void imgPesqGrid_OnClick(object sender, EventArgs e)
        {
            carregarGridProfissionalPlantao();
        }

        protected void ckSelect_CheckedChange(object sender, EventArgs e)
        {
            var atual = (CheckBox)sender;
            GridViewRow linha = (GridViewRow)atual.Parent.Parent;
            int index = linha.RowIndex;
            int coDepto = 0;
            int coPlantao = 0;
            DateTime dtIni = DateTime.Now;
            DateTime dtFim = DateTime.Now;

            if (atual.Checked)
            {
                foreach (GridViewRow row in grdProfi.Rows)
                {
                    var chk = (CheckBox)row.Cells[0].FindControl("ckSelect");
                    if (row.RowIndex == index)
                    {
                        coPlantao = int.Parse(((HiddenField)row.Cells[0].FindControl("hidPlantao")).Value);
                        coDepto = int.Parse(((DropDownList)row.Cells[7].FindControl("ddlLocalGrid")).SelectedValue);
                        dtIni = DateTime.Parse(row.Cells[5].Text).Date;
                        dtFim = DateTime.Parse(row.Cells[6].Text).Date;
                        carregarGridPacientes(coDepto, dtIni, dtFim, coPlantao);
                    }
                    else
                    {
                        chk.Checked = false;
                    }
                }
            }
            else
            {
                grdPacientes.DataSource = null;
                grdPacientes.DataBind();
                UpdatePanel1.Update();
            }

        }

        protected void ddlUnidPlant_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDepartamento();
            ExecutaJavaScript();
        }

        protected void ddlLocalGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            var atual = (DropDownList)sender;
            GridViewRow linha = (GridViewRow)atual.Parent.Parent;
            int index = linha.RowIndex;

            foreach (GridViewRow row in grdProfi.Rows)
            {
                var chk = (CheckBox)row.Cells[0].FindControl("ckSelect");
                if (row.RowIndex == index)
                {
                    chk.Checked = false;
                }
            }
        }

        protected void btnRelatorio_Click(object sender, EventArgs e)
        {
            int coDepto = 0;
            int coPlantao = 0;
            DateTime dtIni = DateTime.Now;
            DateTime dtFim = DateTime.Now;
            try
            {
                foreach (GridViewRow row in grdProfi.Rows)
                {
                    var chk = (CheckBox)row.Cells[0].FindControl("ckSelect");
                    if (chk.Checked)
                    {
                        coPlantao = int.Parse(((HiddenField)row.Cells[0].FindControl("hidPlantao")).Value);
                        coDepto = int.Parse(((DropDownList)row.Cells[7].FindControl("ddlLocalGrid")).SelectedValue);
                        dtIni = DateTime.Parse(row.Cells[5].Text).Date;
                        dtFim = DateTime.Parse(row.Cells[6].Text).Date;
                    }
                }
                if (coPlantao == 0)
                {
                    throw new ArgumentException("Por favor, selecione um registro na grade de Profissionais.");
                }

                var infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
                RptGuiaAtendPlantao rpt = new RptGuiaAtendPlantao();
                var lRetorno = rpt.InitReport(coPlantao, coDepto, dtIni, dtFim, LoginAuxili.CO_EMP);
                GerarRelatorioPadrão(rpt, lRetorno);
            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
                return;
            }

        }

        #endregion

        #region Relatorio

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

        #region Classes de Saída

        public class AtendimentoPaciente
        {
            public int ID_AGEND_PROF_MEDIC { get; set; }
            public int ID_AGEND_INTER { get; set; }
            public int ORIGEM { get; set; }
            public int CO_ALU { get; set; }
            public string LOCAL { get; set; }
            public string NO_ALU { get; set; }
            public int MAT_ALU { get; set; }
            public string SEXO { get; set; }
            public DateTime? ida { get; set; }
            public string IDADE
            {
                get { return AuxiliFormatoExibicao.FormataDataNascimento(this.ida, AuxiliFormatoExibicao.ETipoDataNascimento.padraoSaudeCompleto); }
            }
            public string MED_ORIGEM { get; set; }
            public DateTime dtProvavel { get; set; }
            public string DT_ATEND { get { return this.dtProvavel.ToShortDateString(); } }
            public string HR_ATEND { get; set; }
            public int? ano { get; set; }
            public bool Check { get; set; }
            public string OBSERVACAO { get; set; }
            public string PACIENTE
            {
                get { return this.MAT_ALU.toNire() + " - " + this.NO_ALU; }
            }
        }

        public class ProfissionalPlantao
        {
            public int CO_PLANT { get; set; }
            public string NU_TEL { get; set; }
            public int CO_COL { get; set; }
            public int CO_EMP { get; set; }
            public string NO_COL { get; set; }
            public DateTime dtIniPlantao { get; set; }
            public string DT_INI_PLANTAO
            {
                get { return this.dtIniPlantao.ToShortDateString() + " " + this.dtIniPlantao.ToShortTimeString(); }
            }
            public DateTime dtFimPlantao { get; set; }
            public string DT_FIM_PLANTAO
            {
                get { return this.dtFimPlantao.ToShortDateString() + " " + this.dtFimPlantao.ToShortTimeString(); }
            }
            public string MATR_COL { get; set; }
            public string NO_EMP { get; set; }
            public int CO_ESPEC { get; set; }
            public string NO_ESPEC { get; set; }
            public string NO_DEPT { get; set; }
            public int CO_DEPT { get; set; }
        }
        #endregion
    }
}
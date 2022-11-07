//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE ALUNOS
// OBJETIVO: Registro de Atendimento do Usuário
// DATA DE CRIAÇÃO: 08/03/2014
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 09/06/2014|Victor Martins Machado      | Criada a grid que lista os profissionais
//           |                            | e a função que carrega os mesmos nela.
//           |                            | 

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
using System.Data.Objects;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3010_CtrlPedagogicoSeries;
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno;
using System.Globalization;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8150_ExclusaoAgenda
{
    public partial class RemoverAgenda : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //====> CRIAÇÃO DAS INSTÂNCIAS UTILIZADAS
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            //CurrentPadraoCadastros.BarraCadastro.OnDelete += new Library.Componentes.BarraCadastro.OnDeleteHandler(CurrentPadraoCadastro_OnDelete);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ddlPaciente.Items.Insert(0, new ListItem("Todos", "0"));
                CarregaUnidades();
                CarregaClassificacoes();
                CarregaDepartamento();
                CarregaProfissionaisSaude();

                txtDtIni.Text = DateTime.Now.ToShortDateString();
                txtDtFim.Text = DateTime.Now.ToShortDateString();
            }
        }


        public class obValidacaoAgenda
        {
            public string erro;
            public bool comErro = false;
        }

        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (chkExcluirRegistroDuplicado.Checked)
            {
                DateTime? dtIni = !string.IsNullOrEmpty(txtDtIni.Text) ? DateTime.Parse(txtDtIni.Text) : (DateTime?)null;
                DateTime? dtFim = !string.IsNullOrEmpty(txtDtFim.Text) ? DateTime.Parse(txtDtFim.Text) : (DateTime?)null;

                try
                {
                    if (dtIni.HasValue && dtFim.HasValue)
                    {
                        int prof = !string.IsNullOrEmpty(ddlProfissionalSaude.SelectedValue) ? int.Parse(ddlProfissionalSaude.SelectedValue) : 0;
                        if (prof > 0)
                        {
                            var query = TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                                .Where(w => w.DT_AGEND_HORAR >= dtIni
                                                        && w.DT_AGEND_HORAR <= dtFim
                                                        && w.CO_COL == prof
                                                        && w.CO_ALU == null)
                                           .Union(TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                                    .Where(w => w.DT_AGEND_HORAR >= dtIni
                                                            && w.DT_AGEND_HORAR <= dtFim
                                                            && w.CO_COL == prof
                                                            && w.CO_ALU != null))
                                           .GroupBy(x => new { x.DT_AGEND_HORAR, x.HR_AGEND_HORAR })
                                           .Where(x => x.Count() > 1)
                                           .Select(x => new { x })
                                           .ToList();

                            foreach (var item in query)
                            {
                                foreach (var li in item.x.ToList())
                                {
                                    if (li.CO_ALU == null)
                                    {
                                        var lstEncam = TBS195_ENCAM_MEDIC.RetornaTodosRegistros().Where(w => w.TBS174_AGEND_HORAR.ID_AGEND_HORAR == li.ID_AGEND_HORAR).ToList();
                                        foreach (var ilse in lstEncam)
                                            TBS195_ENCAM_MEDIC.Delete(ilse, true);

                                        //logs de alteração de status associados
                                        var lstLogAltStatus = TBS375_LOG_ALTER_STATUS_AGEND_HORAR.RetornaTodosRegistros().Where(w => w.TBS174_AGEND_HORAR.ID_AGEND_HORAR == li.ID_AGEND_HORAR).ToList();
                                        foreach (var ilsl in lstLogAltStatus)
                                            TBS375_LOG_ALTER_STATUS_AGEND_HORAR.Delete(ilsl, true);

                                        //logs de itens de agenda
                                        var lstLogItAgend = TBS374_LOG_ITENS_AGEND.RetornaTodosRegistros().Where(w => w.TBS174_AGEND_HORAR.ID_AGEND_HORAR == li.ID_AGEND_HORAR).ToList();
                                        foreach (var ilslig in lstLogItAgend)
                                            TBS374_LOG_ITENS_AGEND.Delete(ilslig, true);

                                        var tbs430 = TBS430_HISTO_AGEND_HORAR.RetornaTodosRegistros().Where(x => x.TBS174_AGEND_HORAR.ID_AGEND_HORAR == li.ID_AGEND_HORAR).FirstOrDefault();
                                        if (tbs430 != null)
                                        {
                                            TBS430_HISTO_AGEND_HORAR.Delete(tbs430, true);
                                        }

                                        TBS174_AGEND_HORAR.Delete(li, true);
                                    }
                                }
                            }
                        }
                        else
                        {
                            throw new ArgumentException("Por favor, selecione o profissional.");
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Por favor, selecione o período.");
                    }

                }
                catch (Exception ex)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
                }
            }

            foreach (GridViewRow l in grdProfi.Rows)
            {
                CheckBox chkL = ((CheckBox)l.Cells[0].FindControl("ckSelect"));

                if (chkL.Checked)
                {
                    string hidIdAgendaHora = ((HiddenField)l.Cells[0].FindControl("hidIdAgendaHora")).Value;
                    int Id = int.Parse(hidIdAgendaHora);
                    var agenda = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(Id);

                    if (agenda != null)
                    {
                        #region Exclusão de todos os registros associados

                        //Encaminhamentos associados
                        var lstEncam = TBS195_ENCAM_MEDIC.RetornaTodosRegistros().Where(w => w.TBS174_AGEND_HORAR.ID_AGEND_HORAR == Id).ToList();
                        foreach (var ilse in lstEncam)
                            TBS195_ENCAM_MEDIC.Delete(ilse, true);

                        //logs de alteração de status associados
                        var lstLogAltStatus = TBS375_LOG_ALTER_STATUS_AGEND_HORAR.RetornaTodosRegistros().Where(w => w.TBS174_AGEND_HORAR.ID_AGEND_HORAR == Id).ToList();
                        foreach (var ilsl in lstLogAltStatus)
                            TBS375_LOG_ALTER_STATUS_AGEND_HORAR.Delete(ilsl, true);

                        //logs de itens de agenda
                        var lstLogItAgend = TBS374_LOG_ITENS_AGEND.RetornaTodosRegistros().Where(w => w.TBS174_AGEND_HORAR.ID_AGEND_HORAR == Id).ToList();
                        foreach (var ilslig in lstLogItAgend)
                            TBS374_LOG_ITENS_AGEND.Delete(ilslig, true);

                        #region Exclusão de registros de pagamento da consulta

                        //Forma de pagamento de consulta
                        var lstFormPgto = TBS363_CONSUL_PAGTO.RetornaTodosRegistros().Where(w => w.TBS174_AGEND_HORAR.ID_AGEND_HORAR == Id).ToList();
                        foreach (var ilfpgto in lstFormPgto)
                        {
                            //Registros de pagamento em cartão
                            var lspgtoCartao = TBS364_PAGTO_CARTAO.RetornaTodosRegistros().Where(w => w.TBS363_CONSUL_PAGTO.ID_CONSUL_PAGTO == ilfpgto.ID_CONSUL_PAGTO).ToList();
                            foreach (var ipgtocartao in lspgtoCartao)
                                TBS364_PAGTO_CARTAO.Delete(ipgtocartao, true);

                            //Registros de pagamento em cheque
                            var lstpgtoCheque = TBS365_PAGTO_CHEQUE.RetornaTodosRegistros().Where(w => w.TBS363_CONSUL_PAGTO.ID_CONSUL_PAGTO == ilfpgto.ID_CONSUL_PAGTO).ToList();
                            foreach (var ipgtoCheque in lstpgtoCheque)
                                TBS365_PAGTO_CHEQUE.Delete(ipgtoCheque, true);

                            //Por fim, apaga o registro de pagamento da consulta
                            TBS363_CONSUL_PAGTO.Delete(ilfpgto, true);
                        }

                        #endregion

                        #region Exclusão de registros de atendimento da consulta

                        //Lista de atendimentos da consulta
                        var lstAtendimentos = TBS219_ATEND_MEDIC.RetornaTodosRegistros().Where(w => w.TBS174_AGEND_HORAR.ID_AGEND_HORAR == Id).ToList();
                        foreach (var ilstAtendim in lstAtendimentos)
                        {
                            //Exames associados ao atendimento
                            var lstExamAtend = TBS218_EXAME_MEDICO.RetornaPeloIDAtendimento(ilstAtendim.ID_ATEND_MEDIC);
                            foreach (var ilstExamAtend in lstExamAtend)
                                TBS218_EXAME_MEDICO.Delete(ilstExamAtend, true);

                            //Receituário do atendimento
                            var lstReceitAtend = TBS330_RECEI_ATEND_MEDIC.RetornaPeloIDAtendimento(ilstAtendim.ID_ATEND_MEDIC);
                            foreach (var ilstReceitAtend in lstReceitAtend)
                                TBS330_RECEI_ATEND_MEDIC.Delete(ilstReceitAtend, true);

                            //Serviços Ambulatoriais
                            var lstServAmbuAtend = TBS332_ATEND_SERV_AMBUL.RetornaPeloIDAtendimento(ilstAtendim.ID_ATEND_MEDIC);
                            foreach (var ilstServAmbuAtend in lstServAmbuAtend)
                                TBS332_ATEND_SERV_AMBUL.Delete(ilstServAmbuAtend, true);

                            //Atestados do Atendimento
                            var lstAtestAtend = TBS333_ATEST_MEDIC_PACIE.RetornaTodosRegistros().Where(w => w.ID_ATEND_MEDIC == ilstAtendim.ID_ATEND_MEDIC).ToList();
                            foreach (var ilstAtestAtend in lstAtestAtend)
                                TBS333_ATEST_MEDIC_PACIE.Delete(ilstAtestAtend, true);

                            //Diagnósticos do atendimento
                            var lstDiagnAtend = TBS334_DIAGN_ATEND_MEDIC.RetornaTodosRegistros().Where(w => w.TBS219_ATEND_MEDIC.ID_ATEND_MEDIC == ilstAtendim.ID_ATEND_MEDIC).ToList();
                            foreach (var ilstDiagnAtend in lstDiagnAtend)
                                TBS334_DIAGN_ATEND_MEDIC.Delete(ilstDiagnAtend, true);

                            #region Central de Regulação

                            //centrais de regulação do atendimento
                            var lstCentralRegul = TBS347_CENTR_REGUL.RetornaTodosRegistros().Where(w => w.ID_CENTR_REGUL == ilstAtendim.ID_ATEND_MEDIC).ToList();
                            foreach (var ilstCentralRegul in lstCentralRegul)
                            {
                                //Logs da central de regulação
                                var lstLogCentralRegul = TBS348_LOG_CENTR_REGUL.RetornaTodosRegistros().Where(w => w.TBS347_CENTR_REGUL.ID_CENTR_REGUL == ilstCentralRegul.ID_CENTR_REGUL).ToList();
                                foreach (var ilstLogCentralRegul in lstLogCentralRegul)
                                    TBS348_LOG_CENTR_REGUL.Delete(ilstLogCentralRegul, true);

                                //Ocorrências da central de regulação
                                var lstOcorreCentralRegul = TBS349_CENTR_REGUL_OCORR.RetornaTodosRegistros().Where(w => w.TBS347_CENTR_REGUL.ID_CENTR_REGUL == ilstCentralRegul.ID_CENTR_REGUL).ToList();
                                foreach (var ilstOcorreCentralRegul in lstOcorreCentralRegul)
                                    TBS349_CENTR_REGUL_OCORR.Delete(ilstOcorreCentralRegul, true);

                                //Itens da central de regulação
                                var lstItensCentralRegul = TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros().Where(w => w.TBS347_CENTR_REGUL.ID_CENTR_REGUL == ilstCentralRegul.ID_CENTR_REGUL).ToList();
                                foreach (var ilstItensCentralRegul in lstItensCentralRegul)
                                    TBS350_ITEM_CENTR_REGUL.Delete(ilstItensCentralRegul, true);
                            }

                            #endregion
                        }

                        #endregion

                        //logs de associações a planejamentos


                        var Tbs389 = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(X => X.TBS174_AGEND_HORAR.ID_AGEND_HORAR == agenda.ID_AGEND_HORAR).ToList();

                        if (Tbs389.Count > 0)
                        {
                            foreach (var i in Tbs389)
                            {
                                i.TBS174_AGEND_HORARReference.Load();
                                i.TBS386_ITENS_PLANE_AVALIReference.Load();

                                var Tbs386 = TBS386_ITENS_PLANE_AVALI.RetornaPelaChavePrimaria(i.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI);
                                Tbs386.TBS370_PLANE_AVALIReference.Load();

                                //var Tbs370 = TBS370_PLANE_AVALI.RetornaPelaChavePrimaria(Tbs386.TBS370_PLANE_AVALI.ID_PLANE_AVALI);

                                TBS389_ASSOC_ITENS_PLANE_AGEND.Delete(i, true);
                                TBS386_ITENS_PLANE_AVALI.Delete(Tbs386, true);
                                TBS370_PLANE_AVALI.Delete(Tbs386.TBS370_PLANE_AVALI, true);

                                //TBS389_ASSOC_ITENS_PLANE_AGEND.Delete(i, true);

                                //TBS386_ITENS_PLANE_AVALI.Delete(Tbs386, true);

                                //TBS370_PLANE_AVALI.Delete(Tbs370, true);
                            }
                        }
                        #endregion

                        #region Excluir histórico de agendamento

                        var tbs430 = TBS430_HISTO_AGEND_HORAR.RetornaTodosRegistros().Where(x => x.TBS174_AGEND_HORAR.ID_AGEND_HORAR == agenda.ID_AGEND_HORAR).FirstOrDefault();

                        TBS430_HISTO_AGEND_HORAR.Delete(tbs430, true);

                        #endregion


                        #region Tbs458 (Tabela referentes ao tratamento odontológico)

                        var tbs458 = TBS458_TRATA_PLANO.RetornaTodosRegistros().Where(x => x.TBS174_AGEND_HORAR.ID_AGEND_HORAR == agenda.ID_AGEND_HORAR).ToList();
                        foreach (var item in tbs458)
                        {
                            var _tbs458 = TBS458_TRATA_PLANO.RetornaPelaChavePrimaria(item.ID_TRATA_PLANO);
                            TBS458_TRATA_PLANO.Delete(_tbs458, true);
                        }

                        #endregion
                        TBS174_AGEND_HORAR.Delete(agenda, true);
                    }
                }
            }
            CarregaGridHorario();

            AuxiliPagina.EnvioMensagemSucesso(this.Page, "Alterações realizadas com sucesso!");
        }

        #region Funções de carga de dados

        /// <summary>
        /// Método que carrega as unidades
        /// </summary>
        private void CarregaUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnid, LoginAuxili.ORG_CODIGO_ORGAO, true, true, false);
        }

        /// <summary>
        /// Método que carrega os departamentos
        /// </summary>
        private void CarregaDepartamento()
        {
            int coEmp = ddlUnid.SelectedValue != "" ? int.Parse(ddlUnid.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaDepartamentos(ddlDepto, coEmp, true);
        }

        /// <summary>
        /// Carrega  Profissional  saúde
        /// </summary>
        private void CarregaProfissionaisSaude()
        {
            int coEmp = ddlUnid.SelectedValue != "" ? int.Parse(ddlUnid.SelectedValue) : 0;
            int codepto = (!string.IsNullOrEmpty(ddlDepto.SelectedValue) ? int.Parse(ddlDepto.SelectedValue) : 0);
            string classFunc = ddlClassFunc.SelectedValue;
            AuxiliCarregamentos.CarregaProfissionaisSaude(ddlProfissionalSaude, coEmp, true, classFunc, true, codepto);
        }

        /// <summary>
        /// Carrega a grid de horarios de acordo com o profissional de saúde recebido como parâmetro
        /// </summary>
        private void CarregaGridHorario()
        {
            try
            {
                if (string.IsNullOrEmpty(txtDtIni.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data inicial está vazia ");
                    return;
                }

                if (string.IsNullOrEmpty(txtDtFim.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Data Final está vazia ");
                    return;
                }

                DateTime dtIni = DateTime.Parse(txtDtIni.Text);
                DateTime dtFim = DateTime.Parse(txtDtFim.Text);

                if (dtFim < dtIni)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Data de término não pode ser menor que a data de início.");
                    return;
                }

                //-----------------------------------------------------------------------------------------
                TimeSpan? hrInicio = txtHrIni.Text != "" ? TimeSpan.Parse(txtHrIni.Text) : (TimeSpan?)null;
                TimeSpan? hrFim = txtHrFim.Text != "" ? TimeSpan.Parse(txtHrFim.Text) : (TimeSpan?)null;
                //-----------------------------------------------------------------------------------------

                if (hrInicio.HasValue && hrFim.HasValue && hrFim < hrInicio)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Horário de Fim não pode ser menor que o Horário de Início.");
                    txtHrIni.Focus();
                    return;
                }

                int coCol = ddlProfissionalSaude.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlProfissionalSaude.SelectedValue);
                int codp = ddlDepto.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlDepto.SelectedValue);
                string ClassFunc = ddlClassFunc.SelectedValue == "0" ? "" : ddlClassFunc.SelectedValue;
                int coEmp = ddlUnid.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlUnid.SelectedValue);
                int IdPaciente = !string.IsNullOrEmpty(ddlPaciente.SelectedValue) || ddlPaciente.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlPaciente.SelectedValue);

                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(w => w.DT_AGEND_HORAR >= dtIni && w.DT_AGEND_HORAR <= dtFim && IdPaciente != 0 ? w.CO_ALU == IdPaciente : 0 == 0)
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                           where (coCol != 0 ? tbs174.CO_COL == coCol : 0 == 0 && IdPaciente != 0 ? tbs174.CO_ALU == IdPaciente : 0 == 0)
                               //&& (EntityFunctions.TruncateTime(a.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni)
                               //&& EntityFunctions.TruncateTime(a.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim))
                           && (codp != 0 ? tbs174.CO_DEPT == codp : 0 == 0)
                           && (ClassFunc != "" ? tb03.CO_CLASS_PROFI == ClassFunc : 0 == 0)
                           && (coEmp != 0 ? tb03.CO_EMP == coEmp : 0 == 0)
                           && (!String.IsNullOrEmpty(tbs174.FL_JUSTI_CANCE) ? tbs174.FL_JUSTI_CANCE != "M" && tbs174.FL_JUSTI_CANCE != "C" && tbs174.FL_JUSTI_CANCE != "P" : "" == "")
                           select new HorarioSaida
                           {
                               ID_AGEND_HORA = tbs174.ID_AGEND_HORAR,
                               dt = tbs174.DT_AGEND_HORAR,
                               hr = tbs174.HR_AGEND_HORAR,
                               CO_ALU = tbs174.CO_ALU,
                               TP_CONSUL = tbs174.TP_CONSU,
                               CO_SITU = tbs174.CO_SITUA_AGEND_HORAR,
                               CO_AGEND = tbs174.ID_AGEND_HORAR,
                               FL_CONF = tbs174.FL_CONF_AGEND,
                               CO_DEPTO = tbs174.CO_DEPT,
                               CO_EMP = tbs174.CO_EMP,
                               CO_ESPEC = tbs174.CO_ESPEC,
                               CO_TP_AGEND = tbs174.TP_AGEND_HORAR,
                               CO_TP_CONSUL = tbs174.TP_CONSU,
                               CO_PLAN = tbs174.TB251_PLANO_OPERA.ID_PLAN,
                               CO_OPER = tbs174.TB250_OPERA.ID_OPER,
                               ID_PROC = tbs174.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                               CO_COL = tb03.CO_COL,
                               NO_COL_RECEB = tb03.NO_COL,
                               MATR_COL = tb03.CO_MAT_COL,
                               CO_CLASS = tb03.CO_CLASS_PROFI,
                           }).OrderBy(w => w.dt).ThenBy(w => w.hr).ToList();

                var lst = new List<HorarioSaida>();

                #region Verifica os itens a serem excluídos

                if (!chkDom.Checked && !chkSeg.Checked && !chkTer.Checked && !chkQua.Checked && !chkQui.Checked && !chkSex.Checked && !chkSab.Checked)
                {
                    chkDom.Checked =
                    chkSeg.Checked =
                    chkTer.Checked =
                    chkQua.Checked =
                    chkQui.Checked =
                    chkSex.Checked =
                    chkSab.Checked = true;
                }

                if (res.Count > 0)
                {
                    int aux = 0;
                    foreach (var i in res)
                    {
                        int dia = (int)i.dt.DayOfWeek;

                        switch (dia)
                        {
                            case 0:
                                if (!chkDom.Checked)
                                { lst.Add(i); }
                                break;
                            case 1:
                                if (!chkSeg.Checked)
                                { lst.Add(i); }
                                break;
                            case 2:
                                if (!chkTer.Checked)
                                { lst.Add(i); }
                                break;
                            case 3:
                                if (!chkQua.Checked)
                                { lst.Add(i); }
                                break;
                            case 4:
                                if (!chkQui.Checked)
                                { lst.Add(i); }
                                break;
                            case 5:
                                if (!chkSex.Checked)
                                { lst.Add(i); }
                                break;
                            case 6:
                                if (!chkSab.Checked)
                                { lst.Add(i); }
                                break;
                        }
                        aux++;
                    }
                }
                #endregion

                var resNew = res.Except(lst).Where(w => w.dt >= dtIni && w.dt <= dtFim).ToList();

                //Se tiver horario de inicio, filtra
                if (hrInicio != null)
                    resNew = resNew.Where(a => a.hrC >= hrInicio).ToList();

                //Se tiver horario de termino, filtra
                if (hrFim != null)
                    resNew = resNew.Where(a => a.hrC <= hrFim).ToList();

                grdProfi.DataSource = resNew;
                grdProfi.DataBind();

            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro !" + ex.Message);
            }
        }

        /// <summary>
        /// Carrega as classificações
        /// </summary>
        private void CarregaClassificacoes()
        {
            AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassFunc, true, LoginAuxili.CO_EMP, AuxiliCarregamentos.ETiposClassificacoes.agendamento);
            // AuxiliCarregamentos.CarregaTiposAgendamento(ddlClassFunc, true, false, false, LoginAuxili.CO_EMP, AuxiliCarregamentos.ETiposClassificacoes.agendamento);
        }




        public class HorarioSaida
        {
            //Carrega informações gerais do agendamento

            public int ID_AGEND_HORA { get; set; }
            public DateTime dt { get; set; }
            public string hr { get; set; }
            public TimeSpan hrC
            {
                get
                {
                    //DateTime d = DateTime.Parse(hr);
                    return TimeSpan.Parse((hr + ":00"));
                }
            }
            public string hora
            {
                get
                {
                    string diaSemana = this.dt.ToString("ddd", new CultureInfo("pt-BR"));
                    return this.dt.ToShortDateString() + " - " + this.hr + " " + diaSemana;
                }
            }
            public int CO_AGEND { get; set; }
            public int? CO_ESPEC { get; set; }
            public int? CO_DEPTO { get; set; }
            public string NO_DEPTO
            {
                get
                {
                    string local = "";

                    if (this.CO_DEPTO.HasValue)
                    {
                        var tb14 = TB14_DEPTO.RetornaPelaChavePrimaria(this.CO_DEPTO.Value);
                        local = tb14.CO_SIGLA_DEPTO;
                    }

                    return local;
                }
            }
            public int? CO_EMP { get; set; }

            //Carrega as informações do usuário quando já houver agendamento para o horário em questão
            public string FL_CONF { get; set; }
            public bool FL_CONF_VALID
            {
                get
                {
                    return this.FL_CONF == "S" ? true : false;
                }
            }
            public string NO_PAC
            {
                get
                {
                    return (this.CO_ALU.HasValue ? TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == this.CO_ALU).FirstOrDefault().NO_ALU : " - ");
                }
            }
            public int? CO_ALU { get; set; }
            public string CO_SITU { get; set; }
            public string CO_SITU_VALID
            {
                get
                {
                    string situacao = "";
                    switch (this.CO_SITU)
                    {
                        case "A":
                            situacao = "Aberto";
                            break;
                        case "C":
                            situacao = "Cancelado";
                            break;
                        case "I":
                            situacao = "Inativo";
                            break;
                        case "S":
                            situacao = "Suspenso";
                            break;
                    }

                    return situacao;
                }
            }
            public string TP_CONSUL { get; set; }
            public string TP_CONSUL_VALID
            {
                get
                {
                    string tipo = "";
                    switch (this.TP_CONSUL)
                    {
                        case "N":
                            tipo = "Normal";
                            break;
                        case "R":
                            tipo = "Retorno";
                            break;
                        case "U":
                            tipo = "Urgência";
                            break;
                        default:
                            tipo = " - ";
                            break;
                    }
                    return tipo;
                }
            }
            public string NO_OPERA { get; set; }

            public string CO_TP_AGEND { get; set; }
            public string CO_TP_CONSUL { get; set; }
            public int? CO_OPER { get; set; }
            public int? CO_PLAN { get; set; }
            public int? ID_PROC { get; set; }




            public string MATR_COL { get; set; }
            public string NO_COL_RECEB { get; set; }
            public int CO_COL { get; set; }

            public string CO_CLASS { get; set; }
            public string NO_COL
            {
                get
                {
                    string maCol = this.MATR_COL.PadLeft(6, '0').Insert(2, ".").Insert(6, "-");
                    string noCol = (this.NO_COL_RECEB.Length > 70 ? this.NO_COL_RECEB.Substring(0, 70) + "..." : this.NO_COL_RECEB);
                    return maCol + " - " + noCol;
                }
            }
            public string NO_CLASS_PROFI
            {
                get
                {
                    return AuxiliGeral.GetNomeClassificacaoFuncional(this.CO_CLASS);
                }
            }
        }

        #endregion

        #region Funções de Campo



        protected void ddlddlProfissionalSaude_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int CO_EMP = ddlUnid.SelectedValue == "" ? 0 : ddlUnid.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlUnid.SelectedValue);
            int CO_COL = ddlProfissionalSaude.SelectedValue == "" ? 0 : ddlProfissionalSaude.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlProfissionalSaude.SelectedValue);
            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       where (CO_EMP == 0 ? CO_EMP == 0 : tb07.CO_EMP == CO_EMP && tbs174.CO_COL == CO_COL)
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).ToList();

            if (res != null)
            {
                ddlPaciente.DataTextField = "NO_ALU";
                ddlPaciente.DataValueField = "CO_ALU";
                ddlPaciente.DataSource = res.DistinctBy(w => w.CO_ALU);
                ddlPaciente.DataBind();
            }
            ddlPaciente.Items.Insert(0, new ListItem("Todos", "0"));
        }

        protected void ddlUnid_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDepartamento();
            CarregaProfissionaisSaude();
        }

        protected void ddlDepto_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProfissionaisSaude();
        }

        protected void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow l in grdProfi.Rows)
            {

                CheckBox chkMarca = ((CheckBox)grdProfi.HeaderRow.Cells[0].FindControl("ChkTodos"));
                if (chkMarca.Checked)
                {
                    CheckBox chkL = ((CheckBox)l.Cells[0].FindControl("ckSelect"));
                    chkL.Checked = true;
                }
                else
                {
                    CheckBox chkL = ((CheckBox)l.Cells[0].FindControl("ckSelect"));
                    chkL.Checked = false;
                }
            }
        }

        protected void ddlClassFunc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProfissionaisSaude();
        }

        protected void imgCpfResp_Click(object sender, ImageClickEventArgs e)
        {
            CarregaGridHorario();
        }

        protected void imgPesqPaciente_OnClick(object sender, EventArgs e)
        {
            if (txtNomePaciente.Text.Length < 3)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Digite pelo menos 3 letras para filtar por paciente.");
                txtNomePaciente.Focus();
                return;
            }
            ddlPaciente.Items.Clear();
            OcultarPesquisaPaciente(true);
            string nome = txtNomePaciente.Text;

            var pac = TB07_ALUNO.RetornaTodosRegistros()
                      .Where(x => x.NO_ALU.Contains(nome) && x.CO_SITU_ALU == "A")
                      .Select(x => new
                      {
                          x.NO_ALU,
                          x.CO_ALU
                      }).OrderBy(x => x.NO_ALU);

            ddlPaciente.DataSource = pac;
            ddlPaciente.DataTextField = "NO_ALU";
            ddlPaciente.DataValueField = "CO_ALU";
            ddlPaciente.DataBind();
            ddlPaciente.Items.Insert(0, new ListItem("Todos", "0"));
        }

        private void OcultarPesquisaPaciente(bool ocultar)
        {
            txtNomePaciente.Visible =
            imgPesqPaciente.Visible = !ocultar;
            ddlPaciente.Visible =
            imgVoltarPesqPaciente.Visible = ocultar;
        }

        protected void imgVoltarPesqPaciente_OnClick(object sender, EventArgs e)
        {
            OcultarPesquisaPaciente(false);
        }

        #endregion
    }
}
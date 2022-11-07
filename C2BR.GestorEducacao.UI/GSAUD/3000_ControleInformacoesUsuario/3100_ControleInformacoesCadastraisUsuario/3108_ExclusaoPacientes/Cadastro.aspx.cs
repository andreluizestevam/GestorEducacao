//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CIDADE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 10/12/14 | Maxwell Almeida            | Criação da funcionalidade para Cadastro de Operadoras


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

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3108_ExclusaoPacientes
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public enum ETiposRegistros
        {
            agendamentoAtendimento = 0,
            agendamentoAvaliacao = 1,
            atendimento = 2,
            recepcaoSolicitacoes = 3,
            campanhaSaude = 4,
            preAtendimento = 5,
            encaminhamento = 6,
        }


        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        string salvaDTSitu;

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
                AuxiliPagina.RedirecionaParaPaginaErro("É preciso selecionar o(a) Paciente que deverá ser excluído(a).", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());

            if (!IsPostBack)
            {
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB07_ALUNO tb07 = RetornaEntidade();

        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            try
            {
                TB07_ALUNO tb07 = RetornaEntidade();

                if (tb07 != null)
                {
                    hidCoPac.Value = tb07.CO_ALU.ToString();
                    lblCabecalho.Text = string.Format("{0} Paciente {1} possui as seguintes referências associadas ao seu registro: ", tb07.CO_SEXO_ALU == "F" ? "A" : "O", tb07.NO_ALU);
                    CarregaReferencias(tb07.CO_ALU);
                }
            }
            catch (Exception ex)
            {

                AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao caregar  formulário" + " - " + ex.Message);
                return;
            }
        }

        /// <summary>
        /// Carrega as referências que existem para o paciente
        /// </summary>
        private void CarregaReferencias(int CO_ALU)
        {
            List<saidaGrid> listaReferencias = new List<saidaGrid>();
            int nu = 0;

            #region Verifica referência de Agendamento para Atendimento

            var resAgenAtend = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                where tbs174.CO_ALU == CO_ALU
                                select new { tbs174.ID_AGEND_HORAR }).ToList();

            //Se houver algum registro, acrescenta à lista o item com a quantidade e nº sequencial
            if (resAgenAtend.Count > 0)
                listaReferencias.Add(new saidaGrid { NO_REFER = "Agendamento para Atendimento", NU = nu, QTDE = resAgenAtend.Count, Etipo = ETiposRegistros.agendamentoAtendimento.ToString() });

            #endregion

            #region Verifica referência de Agendamento para Avaliação

            var resAgenAval = (from tbs372 in TBS372_AGEND_AVALI.RetornaTodosRegistros()
                               where tbs372.CO_ALU == CO_ALU
                               select new { tbs372.ID_AGEND_AVALI }).ToList();

            //Se houver algum registro, acrescenta à lista o item com a quantidade e nº sequencial
            if (resAgenAval.Count > 0)
                listaReferencias.Add(new saidaGrid { NO_REFER = "Agendamento para Avaliação", NU = nu, QTDE = resAgenAval.Count, Etipo = ETiposRegistros.agendamentoAvaliacao.ToString() });

            #endregion

            #region Verifica referência de Atendimento

            var resAtend = (from tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros()
                            where tbs219.CO_ALU == CO_ALU
                            select new { tbs219.ID_ATEND_MEDIC }).ToList();

            //Se houver algum registro, acrescenta à lista o item com a quantidade e nº sequencial
            if (resAtend.Count > 0)
                listaReferencias.Add(new saidaGrid { NO_REFER = "Atendimento", NU = nu, QTDE = resAtend.Count, Etipo = ETiposRegistros.atendimento.ToString() });

            #endregion

            #region Verifica referência de Recepção de Solicitações

            var resRecepSolic = (from tbs367 in TBS367_RECEP_SOLIC.RetornaTodosRegistros()
                                 where tbs367.CO_ALU == CO_ALU
                                 select new { tbs367.ID_RECEP_SOLIC }).ToList();

            //Se houver algum registro, acrescenta à lista o item com a quantidade e nº sequencial
            if (resRecepSolic.Count > 0)
                listaReferencias.Add(new saidaGrid { NO_REFER = "Recepção de Solicitações", NU = nu, QTDE = resRecepSolic.Count, Etipo = ETiposRegistros.recepcaoSolicitacoes.ToString() });

            #endregion

            #region Verifica referência de Pré-Atendimento

            var resPreAtend = (from tbs194 in TBS194_PRE_ATEND.RetornaTodosRegistros()
                               where tbs194.CO_ALU == CO_ALU
                               select new { tbs194.ID_PRE_ATEND }).ToList();

            //Se houver algum registro, acrescenta à lista o item com a quantidade e nº sequencial
            if (resPreAtend.Count > 0)
                listaReferencias.Add(new saidaGrid { NO_REFER = "Pré-Atendimento", NU = nu, QTDE = resPreAtend.Count, Etipo = ETiposRegistros.preAtendimento.ToString() });

            #endregion

            #region Verifica referência de Encaminhamento

            var resEncam = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                            where tbs195.CO_ALU == CO_ALU
                            select new { tbs195.ID_ENCAM_MEDIC }).ToList();

            //Se houver algum registro, acrescenta à lista o item com a quantidade e nº sequencial
            if (resEncam.Count > 0)
                listaReferencias.Add(new saidaGrid { NO_REFER = "Encaminhamento", NU = nu, QTDE = resEncam.Count, Etipo = ETiposRegistros.encaminhamento.ToString() });

            #endregion

            #region Verifica referência de Campanha de Saúde

            var resCampanha = (from tbs341 in TBS341_CAMP_ATEND.RetornaTodosRegistros()
                               where tbs341.CO_ALU == CO_ALU
                               select new { tbs341.ID_CAMP_ATEND }).ToList();

            //Se houver algum registro, acrescenta à lista o item com a quantidade e nº sequencial
            if (resCampanha.Count > 0)
                listaReferencias.Add(new saidaGrid { NO_REFER = "Campanha de Saúde", NU = nu, QTDE = resCampanha.Count, Etipo = ETiposRegistros.campanhaSaude.ToString() });

            #endregion

            listaReferencias = listaReferencias.OrderBy(w => w.NO_REFER).ToList();

            //Numera cada item
            foreach (var i in listaReferencias)
            {
                nu++;
                i.NU = nu;
                i.NO_REFER = i.NO_REFER.ToUpper();
            }

            grdReferencias.DataSource = listaReferencias;
            grdReferencias.DataBind();
        }

        public class saidaGrid
        {
            public string Etipo { get; set; }
            public int NU { get; set; }
            public string NU_V
            {
                get
                {
                    return this.NU.ToString().PadLeft(2, '0');
                }
            }
            public string NO_REFER { get; set; }
            public int QTDE { get; set; }
            public string QTDE_V
            {
                get
                {
                    return this.QTDE.ToString().PadLeft(2, '0');
                }
            }
        }

        /// <summary>
        /// Exclui os agendamentos do atendimento de atendimento do paciente
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void ExcluiAgendamentosAtendimento(int CO_ALU)
        {
            var resAgenAtend = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                where tbs174.CO_ALU == CO_ALU
                                select tbs174).ToList();

            //Deleta todos os agendamentos do paciente
            foreach (var i in resAgenAtend)
            {
                //Encaminhamentos associados
                var lstEncam = TBS195_ENCAM_MEDIC.RetornaTodosRegistros().Where(w => w.TBS174_AGEND_HORAR.ID_AGEND_HORAR == i.ID_AGEND_HORAR).ToList();
                foreach (var ilse in lstEncam)
                    TBS195_ENCAM_MEDIC.Delete(ilse, true);

                //Não precisa apagar os logs já que o registro não será apagado, apenas removidas as referências de paciente
                //logs de alteração de status associados
                //var lstLogAltStatus = TBS375_LOG_ALTER_STATUS_AGEND_HORAR.RetornaTodosRegistros().Where(w => w.TBS174_AGEND_HORAR.ID_AGEND_HORAR == i.ID_AGEND_HORAR).ToList();
                //foreach (var ilsl in lstLogAltStatus)
                //    TBS375_LOG_ALTER_STATUS_AGEND_HORAR.Delete(ilsl, true);

                ////logs de itens de agenda
                //var lstLogItAgend = TBS374_LOG_ITENS_AGEND.RetornaTodosRegistros().Where(w => w.TBS174_AGEND_HORAR.ID_AGEND_HORAR == i.ID_AGEND_HORAR).ToList();
                //foreach (var ilslig in lstLogItAgend)
                //    TBS374_LOG_ITENS_AGEND.Delete(ilslig, true);

                #region Exclusão de registros de pagamento da consulta

                //Forma de pagamento de consulta
                var lstFormPgto = TBS363_CONSUL_PAGTO.RetornaTodosRegistros().Where(w => w.TBS174_AGEND_HORAR.ID_AGEND_HORAR == i.ID_AGEND_HORAR).ToList();
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
                var lstAtendimentos = TBS219_ATEND_MEDIC.RetornaTodosRegistros().Where(w => w.TBS174_AGEND_HORAR.ID_AGEND_HORAR == i.ID_AGEND_HORAR).ToList();
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

                //Por fim remove o paciente da agenda
                #region remoção do paciente da agenda

                #region Altera as informações dos agendamentos para nulos

                i.CO_ALU = (int?)null;
                i.TP_CONSU = null;
                i.FL_CONF_AGEND = "N";
                i.FL_CONFIR_CONSUL_SMS = null;
                i.VL_CONSU_BASE = (decimal?)null;
                i.VL_DESCT = (decimal?)null;
                i.TB250_OPERA = null;
                i.TB251_PLANO_OPERA = null;
                i.NU_PLAN_SAUDE = null;
                i.TBS356_PROC_MEDIC_PROCE = null;
                i.VL_CONSUL = (decimal?)null;
                i.CO_CLASS_RISCO = (int?)null;
                i.TP_AGEND_HORAR = null;
                i.CO_TIPO_PROC_MEDI = null;

                TBS174_AGEND_HORAR.SaveOrUpdate(i, true);

                #endregion

                #region Salva no log

                TBS374_LOG_ITENS_AGEND tbs374 = new TBS374_LOG_ITENS_AGEND();

                tbs374.TBS174_AGEND_HORAR = i;

                tbs374.CO_ALU_ANTES = (int?)null;
                tbs374.TP_CONSUL_ANTES = null;
                tbs374.FL_CONFIR_AGEND_ANTES = null;
                tbs374.FL_CONFIR_CONSUL_SMS_ANTES = null;
                tbs374.VL_CONSU_BASE_ANTES = (decimal?)null;
                tbs374.VL_DESCT_ANTES = (decimal?)null;
                tbs374.ID_OPER_ANTES = (int?)null;
                tbs374.ID_PLAN_ANTES = (int?)null;
                tbs374.NU_PLAN_SAUDE = (int?)null;
                tbs374.ID_PROC_MEDI_PROCE_ANTES = (int?)null;
                tbs374.VL_CONSUL_ANTES = (decimal?)null;
                tbs374.CO_CLASS_RISCO_ANTES = (int?)null;
                tbs374.TP_AGEND_HORAR_ANTES = null;
                tbs374.CO_TIPO_PROC_MEDI_ANTES = null;

                tbs374.CO_ALU_DEPOIS = (int?)null;
                tbs374.TP_CONSUL_DEPOIS = null;
                tbs374.FL_CONFIR_AGEND_DEPOIS = null;
                tbs374.FL_CONFIR_CONSUL_SMS_DEPOIS = null;
                tbs374.VL_CONSU_BASE_DEPOIS = (decimal?)null;
                tbs374.VL_DESCT_DEPOIS = (decimal?)null;
                tbs374.ID_OPER_DEPOIS = (int?)null;
                tbs374.ID_PLAN_DEPOIS = (int?)null;
                tbs374.NU_PLAN_DEPOIS = (int?)null;
                tbs374.ID_PROC_MEDI_PROCE_DEPOIS = (int?)null;
                tbs374.VL_CONSUL_DEPOIS = (decimal?)null;
                tbs374.CO_CLASS_RISCO_DEPOIS = (int?)null;
                tbs374.TP_AGEND_HORAR_DEPOIS = null;
                tbs374.CO_TIPO_PROC_MEDI_DEPOIS = null;

                tbs374.CO_COL_EXEC = LoginAuxili.CO_COL;
                tbs374.CO_EMP_COL_EXEC = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                tbs374.CO_EMP_EXEC = LoginAuxili.CO_EMP;
                tbs374.DT_EXEC = DateTime.Now;
                tbs374.IP_EXEC = Request.UserHostAddress;
                tbs374.CO_TIPO_ALTER_ITENS_AGENDA = "E";

                TBS374_LOG_ITENS_AGEND.SaveOrUpdate(tbs374, true);

                #endregion

                #endregion
            }
        }

        /// <summary>
        /// Exclui os agendamentos de avaliação do paciente
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void ExcluiAgendamentosAvaliacao(int CO_ALU)
        {
            var resAgenAval = (from tbs372 in TBS372_AGEND_AVALI.RetornaTodosRegistros()
                               where tbs372.CO_ALU == CO_ALU
                               select tbs372).ToList();

            //Deleta todos os agendamentos de avaliação do paciente
            foreach (var i in resAgenAval)
            {
                //Itens de avaliação do agendamento
                var lstItensSolicAgenAval = TBS373_AGEND_AVALI_ITENS.RetornaTodosRegistros().Where(w => w.TBS372_AGEND_AVALI.ID_AGEND_AVALI == i.ID_AGEND_AVALI).ToList();
                foreach (var ilstItensSolicAgenAval in lstItensSolicAgenAval)
                    TBS373_AGEND_AVALI_ITENS.Delete(ilstItensSolicAgenAval, true);

                //Exclui o agendamento de avaliação

                var res = (from tbs370 in TBS380_LOG_ALTER_STATUS_AGEND_AVALI.RetornaTodosRegistros()
                           where tbs370.TBS372_AGEND_AVALI.ID_AGEND_AVALI == i.ID_AGEND_AVALI
                           select tbs370).ToList();


                foreach (var item in res)
                {
                    TBS380_LOG_ALTER_STATUS_AGEND_AVALI tbs = new TBS380_LOG_ALTER_STATUS_AGEND_AVALI();
                    item.TBS372_AGEND_AVALI = null;
                    tbs = item;
                    TBS380_LOG_ALTER_STATUS_AGEND_AVALI.SaveOrUpdate(tbs, true);

                }


                TBS372_AGEND_AVALI.Delete(i, true);
            }
        }

        /// <summary>
        /// Exclui atendimentos do paciente recebido como parâmetro
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void ExcluiAtendimentos(int CO_ALU)
        {
            var resAtend = (from tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros()
                            where tbs219.CO_ALU == CO_ALU
                            select tbs219).ToList();

            foreach (var ilstAtendim in resAtend)
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

                //Exclusão de atendimento
                TBS219_ATEND_MEDIC.Delete(ilstAtendim, true);
            }
        }

        /// <summary>
        /// Exclusão das recepções do paciente
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void ExcluiRecepcoes(int CO_ALU)
        {
            var resRecepSolic = (from tbs367 in TBS367_RECEP_SOLIC.RetornaTodosRegistros()
                                 where tbs367.CO_ALU == CO_ALU
                                 select tbs367).ToList();


            //Deleta todos os agendamentos de avaliação do paciente
            foreach (var i in resRecepSolic)
            {
                //Itens de regulação de itens da recepção
                var lstRecepSolicItensRegul = TBS369_RECEP_REGUL_ITENS.RetornaTodosRegistros().Where(w => w.TBS367_RECEP_SOLIC.ID_RECEP_SOLIC == i.ID_RECEP_SOLIC).ToList();
                foreach (var ilstRecepSolicItensRegul in lstRecepSolicItensRegul)
                    TBS369_RECEP_REGUL_ITENS.Delete(ilstRecepSolicItensRegul, true);

                //Itens de solicitação na recepção
                var lstRecepSolicItens = TBS368_RECEP_SOLIC_ITENS.RetornaTodosRegistros().Where(w => w.TBS367_RECEP_SOLIC.ID_RECEP_SOLIC == i.ID_RECEP_SOLIC).ToList();
                foreach (var ilstRecepSolicItens in lstRecepSolicItens)
                {
                    //Os planejamentos do item solicitado
                    //var lstPlanItens = TBS370_PLANO_ACAO_PROCE.RetornaTodosRegistros().Where(w => w.TBS368_RECEP_SOLIC_ITENS1.ID_RECEP_SOLIC_ITENS == ilstRecepSolicItens.ID_RECEP_SOLIC_ITENS).ToList();
                    //foreach (var ilstPlanItens in lstPlanItens)
                    //    TBS370_PLANO_ACAO_PROCE.Delete(ilstPlanItens, true);

                    //Avaliações associadas
                    //var lstAvalItensSolic = TBS371_PESQU_AVALI_PROCE_SOLIC.RetornaTodosRegistros().Where(w => w.TBS368_RECEP_SOLIC_ITENS.ID_RECEP_SOLIC_ITENS == ilstRecepSolicItens.ID_RECEP_SOLIC_ITENS).ToList();
                    //foreach (var ilstAvalItensSolic in lstAvalItensSolic)
                    //    TBS371_PESQU_AVALI_PROCE_SOLIC.Delete(ilstAvalItensSolic, true);

                    //Por fim, o registro do item de solicitação
                    TBS368_RECEP_SOLIC_ITENS.Delete(ilstRecepSolicItens, true);
                }

                //Por fim, o registro da recepção
                ExcluiAvaliacao(i.ID_RECEP_SOLIC, CO_ALU);
                TBS367_RECEP_SOLIC.Delete(i, true);
            }
        }

        /// <summary>
        /// Exclui os pré-atendimentos do paciente
        /// </summary>
        private void ExcluiPreAtendimentos(int CO_ALU)
        {
            var resPreAtend = (from tbs194 in TBS194_PRE_ATEND.RetornaTodosRegistros()
                               where tbs194.CO_ALU == CO_ALU
                               select tbs194).ToList();

            //Deleta todos os agendamentos de avaliação do paciente
            foreach (var i in resPreAtend)
                TBS194_PRE_ATEND.Delete(i, true);
        }

        /// <summary>
        /// Exclui Avaliação 
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void ExcluiAvaliacao(int ID_RECEP_SOLIC, int CO_ALU)
        {
            var resEncam = (from tbs370 in TBS370_PLANE_AVALI.RetornaTodosRegistros()
                            where tbs370.TBS367_RECEP_SOLIC.ID_RECEP_SOLIC == ID_RECEP_SOLIC
                            select tbs370).ToList();

            //Deleta todos os Encaminhamentos
            foreach (var i in resEncam)
            {

                var res = (from tbs370 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                           where tbs370.TBS370_PLANE_AVALI.ID_PLANE_AVALI == i.ID_PLANE_AVALI
                           select tbs370).ToList();


                foreach (var item in res)
                {
                    TBS174_AGEND_HORAR tbs = new TBS174_AGEND_HORAR();
                    item.TBS370_PLANE_AVALI = null;
                    tbs = item;
                    TBS174_AGEND_HORAR.SaveOrUpdate(tbs, true);

                }
                //TBS386_ITENS_PLANE_AVALI
                var resItens = (from tbs370 in TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros()
                                where tbs370.TBS370_PLANE_AVALI.ID_PLANE_AVALI == i.ID_PLANE_AVALI
                                select tbs370).ToList();
                foreach (var item in resItens)
                {
                    var S = (from tbss in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros()
                             where tbss.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI == item.ID_ITENS_PLANE_AVALI
                             select tbss).ToList();
                    foreach (var it in S)
                    {
                        TBS389_ASSOC_ITENS_PLANE_AGEND ntbs = new TBS389_ASSOC_ITENS_PLANE_AGEND();
                        it.TBS386_ITENS_PLANE_AVALI = null;
                        ntbs = it;
                        TBS389_ASSOC_ITENS_PLANE_AGEND.SaveOrUpdate(ntbs, true);
                    }
                    TBS386_ITENS_PLANE_AVALI tbs = new TBS386_ITENS_PLANE_AVALI();
                    item.TBS370_PLANE_AVALI = null;
                    tbs = item;
                    TBS386_ITENS_PLANE_AVALI.Delete(tbs, true);

                }

                var resdel = TBS370_PLANE_AVALI.RetornaPelaChavePrimaria(i.ID_PLANE_AVALI);

                TBS370_PLANE_AVALI.Delete(resdel, true);
            }

        }


        /// <summary>
        /// Exclui os Encaminhamentos do Paciente
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void ExcluiEncaminhamentos(int CO_ALU)
        {
            var resEncam = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                            where tbs195.CO_ALU == CO_ALU
                            select tbs195).ToList();

            //Deleta todos os Encaminhamentos
            foreach (var i in resEncam)
                TBS195_ENCAM_MEDIC.Delete(i, true);
        }
        /// <summary>
        /// Exclui as campanhas do paciente
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void ExcluiCampanhas(int CO_ALU)
        {
            var resCampanha = (from tbs341 in TBS341_CAMP_ATEND.RetornaTodosRegistros()
                               where tbs341.CO_ALU == CO_ALU
                               select tbs341).ToList();

            //Deleta todos os agendamentos de avaliação do paciente
            foreach (var i in resCampanha)
                TBS341_CAMP_ATEND.Delete(i, true);
        }

        /// <summary>
        /// Efetua as devidas exclusões
        /// </summary>
        private void EfetuaExclusoes(int CO_ALU)
        {
            try
            {
                #region Exclui a referência de Agendamento para Atendimento

                ExcluiAgendamentosAtendimento(CO_ALU);

                #endregion

                #region Exclui referência de Agendamento para Avaliação

                ExcluiAgendamentosAvaliacao(CO_ALU);

                #endregion

                #region Exclui referência de Atendimento

                ExcluiAtendimentos(CO_ALU);

                #endregion

                #region Exclui referência de Recepção de Solicitações

                ExcluiRecepcoes(CO_ALU);

                #endregion

                #region Exclui referência de Encaminhamento

                ExcluiEncaminhamentos(CO_ALU);

                #endregion

                #region Exclui referência de Pré-Atendimento

                ExcluiPreAtendimentos(CO_ALU);

                #endregion

                #region Exclui referência de Campanha de Saúde

                ExcluiCampanhas(CO_ALU);

                #endregion

                #region Exclui o paciente

                TB07_ALUNO.Delete(TB07_ALUNO.RetornaPeloCoAlu(CO_ALU), true);

                #endregion
            }
            catch (Exception e)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Ocorreu um erro ao excluir alguma referência. " + e.Message);
                return;
            }
        }

        /// <summary>
        /// Retorna a entidade e contexto, quando houver
        /// </summary>
        /// <returns></returns>
        /// 
        private TB07_ALUNO RetornaEntidade()
        {
            TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb07 == null) ? new TB07_ALUNO() : tb07;
        }

        #endregion

        #region Funções de Campo

        protected void lnkSim_OnClick(object sender, EventArgs e)
        {
            EfetuaExclusoes(int.Parse(hidCoPac.Value));
            AuxiliPagina.RedirecionaParaPaginaSucesso("Registro(s) excluído(s) com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
        }

        protected void lnkNao_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
        }

        protected void imgExc_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            int aux = 0;
            if (grdReferencias.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdReferencias.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgExc");

                    if (img.ClientID == atual.ClientID)
                    {
                        string tipo = (((TextBox)linha.FindControl("txtTipo")).Text);
                        int coAlu = int.Parse(hidCoPac.Value);

                        if (tipo == ETiposRegistros.agendamentoAtendimento.ToString())
                            ExcluiAgendamentosAtendimento(coAlu);
                        else if (tipo == ETiposRegistros.agendamentoAvaliacao.ToString())
                            ExcluiAgendamentosAvaliacao(coAlu);
                        else if (tipo == ETiposRegistros.atendimento.ToString())
                            ExcluiAtendimentos(coAlu);
                        else if (tipo == ETiposRegistros.recepcaoSolicitacoes.ToString())
                            ExcluiRecepcoes(coAlu);
                        else if (tipo == ETiposRegistros.campanhaSaude.ToString())
                            ExcluiCampanhas(coAlu);
                        else if (tipo == ETiposRegistros.preAtendimento.ToString())
                            ExcluiPreAtendimentos(coAlu);
                        else if (tipo == ETiposRegistros.encaminhamento.ToString())
                            ExcluiEncaminhamentos(coAlu);


                        // ExcluiAvaliacao(coAlu);
                        CarregaReferencias(coAlu);
                    }
                }
            }
        }

        #endregion
    }
}
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
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------

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
using System.Globalization;
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
using System.Data.Objects;
using System.Transactions;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_CtrlConsultas;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8160_AssocProcAgenda
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            ///Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                grdHistorPaciente.DataSource = grdProfi.DataSource = grdHorario.DataSource = null;
                grdHistorPaciente.DataBind(); grdProfi.DataBind(); grdHorario.DataBind();

                CarregaClassificacoes();
                CarregaUnidades(ddlUnidResCons);
                CarregaDepartamento();
                CarregaPacientes();
                CarregaGridProfi();
                txtDtIniHistoUsuar.Text = DateTime.Now.AddDays(-5).ToString();
                txtDtFimHistoUsuar.Text = DateTime.Now.AddDays(30).ToString();

                AuxiliCarregamentos.CarregaUFs(ddlUFOrgEmis, false, LoginAuxili.CO_EMP, true);
                AuxiliCarregamentos.CarregaUFs(ddlUF, false, LoginAuxili.CO_EMP);
                ddlUFOrgEmis.Items.Insert(0, new ListItem("", ""));
                carregaCidade();
                carregaBairro();

                txtDtIniAgenda.Text = DateTime.Now.ToString();
                txtDtFimAgenda.Text = DateTime.Now.AddMonths(6).ToString();

                CarregaOperadoras(ddlOperadora, "");
                CarregarPlanosSaude(ddlPlano, ddlOperadora);

                CarregarGrupos(ddlGrupoPr, false, false, true);
                CarregarSubGrupos(ddlSubGrupoPr, ddlGrupoPr);
                CarregaProcedimentos(ddlProcedimento, ddlOperadora, ddlGrupoPr, ddlSubGrupoPr, null, true);
            }
        }

        #endregion

        #region Carregamento

        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            //Lista onde serão armazenados os itens que se deseja salvar, para poder ser percorrida depois
            List<saidaClassHorarios> lstItensAgenda = new List<saidaClassHorarios>();
            foreach (GridViewRow li in grdHorario.Rows)
            {
                //Apenas nos itens selecionados
                if (((CheckBox)li.FindControl("ckSelectHr")).Checked)
                {
                    DropDownList proced = (((DropDownList)li.FindControl("ddlProcedimento")));
                    TextBox nrAcao = (((TextBox)li.FindControl("txtNuAcao")));
                    TextBox deAcao = (((TextBox)li.FindControl("txtDesAcao")));
                    int idAgenda = int.Parse(((HiddenField)li.FindControl("hidCoAgenda")).Value);
                    string IdItemPlan = (((HiddenField)li.FindControl("hidIdItemPlane")).Value);

                    #region Validações

                    //Se foi selecionado o procedimento
                    if (string.IsNullOrEmpty(proced.SelectedValue))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "O Procedimento precisa ser selecionado!");
                        proced.Focus();
                        return;
                    }

                    //Se foi informado o número da ação
                    if (string.IsNullOrEmpty(nrAcao.Text))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "O Nº da Ação precisa ser preenchido!");
                        nrAcao.Focus();
                        return;
                    }

                    #endregion

                    //Adiciona mais um item na lista que será persistida
                    saidaClassHorarios ob = new saidaClassHorarios();
                    ob.dt_agenda = DateTime.Parse(((HiddenField)li.FindControl("hidDtAgenda")).Value);
                    ob.DE_RESUM_ACAO = deAcao.Text;
                    ob.NR_ACAO = int.Parse(nrAcao.Text);
                    ob.proced = int.Parse(proced.SelectedValue);
                    ob.idAgenda = idAgenda;
                    ob.CO_ALU = int.Parse(ddlNomeUsu.SelectedValue);
                    ob.tbs370 = RecuperaPlanejamento(ob.proced, int.Parse(ddlNomeUsu.SelectedValue));
                    ob.tbs356 = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(ob.proced);
                    ob.ID_ITEM_PLANE = (!string.IsNullOrEmpty(IdItemPlan) ? int.Parse(IdItemPlan) : (int?)null);
                    lstItensAgenda.Add(ob);
                }
            }

            ////cria escopo de transação para garantir atomicidade
            //using (TransactionScope scope = new TransactionScope())
            //{
            //Percorre a lista de itens de planejamento à serem salvos e executa as devidas persistências
            foreach (var i in lstItensAgenda)
            {
                #region Inclui o Item de Planjamento

                //Se tem item de planjamento, trás objeto da entidade dele, se não, cria novo objeto da entidade
                TBS386_ITENS_PLANE_AVALI tbs386 = (i.ID_ITEM_PLANE.HasValue ? TBS386_ITENS_PLANE_AVALI.RetornaPelaChavePrimaria(i.ID_ITEM_PLANE.Value) : new TBS386_ITENS_PLANE_AVALI());

                //Dados da situação
                tbs386.CO_SITUA = "A";
                tbs386.DT_SITUA = DateTime.Now;
                tbs386.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs386.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                tbs386.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs386.IP_SITUA = Request.UserHostAddress;
                tbs386.DE_RESUM_ACAO = (!string.IsNullOrEmpty(i.DE_RESUM_ACAO) ? i.DE_RESUM_ACAO : null);

                //Estes dados, são inseridos apenas quando é um novo objeto da entidade
                if (!i.ID_ITEM_PLANE.HasValue)
                {
                    //Dados básicos do item de planejamento
                    tbs386.TBS370_PLANE_AVALI = i.tbs370;
                    tbs386.TBS356_PROC_MEDIC_PROCE = i.tbs356;
                    tbs386.NR_ACAO = i.NR_ACAO;
                    tbs386.QT_SESSO = lstItensAgenda.Where(w => w.proced == i.proced).Count(); //Conta quantos itens existem na lista para este mesmo procedimento
                    tbs386.DT_INICI = lstItensAgenda.ToList().OrderBy(w => w.dt_agenda).FirstOrDefault().dt_agenda; //Verifica qual a primeira data na lista
                    tbs386.DT_FINAL = lstItensAgenda.ToList().OrderByDescending(w => w.dt_agenda).FirstOrDefault().dt_agenda; //Verifica qual a última data na lista
                    tbs386.FL_AGEND_FEITA_PLANE = "N";

                    //Dados do cadastro
                    tbs386.DT_CADAS = DateTime.Now;
                    tbs386.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs386.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                    tbs386.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    tbs386.IP_CADAS = Request.UserHostAddress;

                    //Data prevista é a data do agendamento associado
                    tbs386.DT_AGEND = i.dt_agenda;
                }

                TBS386_ITENS_PLANE_AVALI.SaveOrUpdate(tbs386);

                #endregion

                //Não tem necessidade de alterar esses dados em uma edição, pois eles serão os mesmos que forma inseridos na altura do cadastro
                if (!i.ID_ITEM_PLANE.HasValue)
                {
                    #region Atualiza o agendamento

                    TBS174_AGEND_HORAR tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(i.idAgenda);

                    //Agendamento associado ao item de planejamento
                    tbs174.TBS386_ITENS_PLANE_AVALI = tbs386;
                    tbs174.TP_AGEND_HORAR = TB03_COLABOR.RetornaPeloCoCol(tbs174.CO_COL.Value).CO_CLASS_PROFI;
                    tbs174.CO_ALU = i.CO_ALU;
                    tbs174.CO_EMP_ALU = (TB07_ALUNO.RetornaPeloCoAlu(i.CO_ALU).CO_EMP);
                    tbs174.TP_CONSU = "N"; // normal de padrão
                    tbs174.FL_CONF_AGEND = "N";
                    tbs174.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(i.proced);
                    tbs174.TB250_OPERA = (!string.IsNullOrEmpty(ddlOperadora.SelectedValue) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlOperadora.SelectedValue)) : null);
                    tbs174.TB251_PLANO_OPERA = (!string.IsNullOrEmpty(ddlPlano.SelectedValue) ? TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlano.SelectedValue)) : null);

                    #region Gera Código da Consulta

                    string coUnid = LoginAuxili.CO_UNID.ToString();
                    int coEmp = LoginAuxili.CO_EMP;
                    string ano = DateTime.Now.Year.ToString().Substring(2, 2);

                    var res = (from tbs174pesq in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               where tbs174pesq.CO_EMP == coEmp && tbs174pesq.NU_REGIS_CONSUL != null
                               select new { tbs174pesq.NU_REGIS_CONSUL }).OrderByDescending(w => w.NU_REGIS_CONSUL).FirstOrDefault();

                    string seq;
                    int seq2;
                    int seqConcat;
                    string seqcon;
                    if (res == null)
                    {
                        seq2 = 1;
                    }
                    else
                    {
                        seq = res.NU_REGIS_CONSUL.Substring(7, 7);
                        seq2 = int.Parse(seq);
                    }

                    seqConcat = seq2 + 1;
                    seqcon = seqConcat.ToString().PadLeft(7, '0');

                    tbs174.NU_REGIS_CONSUL = ano + coUnid.PadLeft(3, '0') + "CO" + seqcon;

                    #endregion

                    TBS174_AGEND_HORAR.SaveOrUpdate(tbs174, true);

                    #endregion
                }
            }

            //scope.Complete();
            //}

            AuxiliPagina.RedirecionaParaPaginaSucesso("Operação realizada com Sucesso!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
        }

        public class saidaClassHorarios
        {
            public TBS356_PROC_MEDIC_PROCE tbs356 { get; set; }
            public TBS370_PLANE_AVALI tbs370 { get; set; }
            public DateTime dt_agenda { get; set; }
            public string DE_RESUM_ACAO { get; set; }
            public int NR_ACAO { get; set; }
            public int proced { get; set; }
            public int idAgenda { get; set; }
            public int CO_ALU { get; set; }
            public int? ID_ITEM_PLANE { get; set; }
        }

        /// <summary>
        /// Retorna um objeto do planejamento de determinado procedimento/paciente recebidos como parâmetro
        /// </summary>
        /// <returns></returns>
        private TBS370_PLANE_AVALI RecuperaPlanejamento(int ID_PROC, int CO_ALU)
        {
            //Cria objeto de planejamento do paciente no parâmetro que esteja ativo que possua nos itens o id do procedimento do parâmetro
            var res = (from tbs370 in TBS370_PLANE_AVALI.RetornaTodosRegistros()
                       join tbs386 in TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros() on tbs370.ID_PLANE_AVALI equals tbs386.TBS370_PLANE_AVALI.ID_PLANE_AVALI
                       where tbs370.CO_SITUA == "A"
                       && tbs370.CO_ALU == CO_ALU
                       && tbs386.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == ID_PROC
                       select tbs370).DistinctBy(w => w.ID_PLANE_AVALI).FirstOrDefault();

            if (res != null)
                return res;
            else // Já que não tem ainda, cria um novo planejamento e retorna um objeto do mesmo no método
            {
                TBS370_PLANE_AVALI tbs370 = new TBS370_PLANE_AVALI();
                tbs370.CO_ALU = CO_ALU;

                //Dados do cadastro
                tbs370.DT_CADAS = DateTime.Now;
                tbs370.CO_COL_CADAS = LoginAuxili.CO_COL;
                tbs370.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                tbs370.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                tbs370.IP_CADAS = Request.UserHostAddress;

                //Dados da situação
                tbs370.CO_SITUA = "A";
                tbs370.DT_SITUA = DateTime.Now;
                tbs370.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs370.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                tbs370.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs370.IP_SITUA = Request.UserHostAddress;
                TBS370_PLANE_AVALI.SaveOrUpdate(tbs370, true);

                return tbs370;
            }
        }

        /// <summary>
        /// Recupera o próximo número de ação para o paciente e procedimento recebidos como parâmetro
        /// </summary>
        /// <param name="CO_ALU"></param>
        /// <param name="ID_PROC"></param>
        private int RecuperaUltimoNrAcao(int CO_ALU, int ID_PROC)
        {
            int? maiorAcaoGrid = (int?)null;
            //Primeiramente, verifica se já não existe na grid o mesmo procedimento para este paciente com ação já carregada porém ainda não salva
            foreach (GridViewRow i in grdHorario.Rows)
            {
                if (((CheckBox)i.FindControl("ckSelectHr")).Checked)
                {
                    DropDownList ddlProc = (((DropDownList)i.FindControl("ddlProcedimento")));

                    if (!string.IsNullOrEmpty(ddlProc.SelectedValue))
                    {
                        if (int.Parse(ddlProc.SelectedValue) == ID_PROC) //Se for mesmo procedimento
                        {
                            TextBox txtNrAcao = (((TextBox)i.FindControl("txtNuAcao")));
                            if (!string.IsNullOrEmpty(txtNrAcao.Text))
                            {
                                int nr = int.Parse(txtNrAcao.Text);
                                //Se esse número de ação para este procedimento e paciente for maior que o armazenado, o substitui
                                //E se maior nr ação ainda não tiver valor, já insere o nr encontrado
                                maiorAcaoGrid = (maiorAcaoGrid.HasValue ? (nr > maiorAcaoGrid.Value ? nr : maiorAcaoGrid.Value) : nr);
                            }
                        }
                    }
                }
            }

            var res = (from tbs386 in TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros()
                       where tbs386.TBS370_PLANE_AVALI.CO_SITUA == "A"
                       && tbs386.TBS370_PLANE_AVALI.CO_ALU == CO_ALU
                       select new { tbs386.NR_ACAO }).OrderByDescending(w => w.NR_ACAO).FirstOrDefault();

            /*
             *Verifica se existe algum nº de ação na grid para este procedimento e paciente, se houver,
             *retorna ele +1 pois é o próximo, se não houver, então retorna o último nº ação encontrado +1
             */
            if (res != null)
                return (maiorAcaoGrid.HasValue ? maiorAcaoGrid.Value + 1 : res.NR_ACAO + 1);
            else
                return (maiorAcaoGrid.HasValue ? maiorAcaoGrid.Value + 1 : 1);
        }

        /// <summary>
        /// Carrega a grid de horarios de acordo com o profissional de saúde recebido como parâmetro
        /// </summary>
        private void CarregaGridHorario(int coCol, int coAlu)
        {
            DateTime dtIni, dtFim;
            if (!DateTime.TryParse(txtDtIniAgenda.Text, out dtIni))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "A data inicial informada não é válida!");
                return;
            }

            if (!DateTime.TryParse(txtDtFimAgenda.Text, out dtFim))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "A data final informada não é válida!");
                return;
            }

            var res = (from a in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       where a.CO_COL == coCol
                       && a.CO_ALU == coAlu
                       && a.CO_SITUA_AGEND_HORAR == "A"
                       && (EntityFunctions.TruncateTime(a.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni)  //dtInici
                       && (EntityFunctions.TruncateTime(a.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim))) //dtFimC
                       select new HorarioSaida
                       {
                           dt = a.DT_AGEND_HORAR,
                           hr = a.HR_AGEND_HORAR,
                           CO_ALU = a.CO_ALU,
                           CO_SITU = a.CO_SITUA_AGEND_HORAR,
                           CO_AGEND = a.ID_AGEND_HORAR,
                           CO_COL = a.CO_COL,
                           CO_PLAN = a.TB251_PLANO_OPERA.ID_PLAN,
                           CO_OPER = a.TB250_OPERA.ID_OPER,
                           ID_PROC = a.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                           ID_GRUPO = (a.TBS386_ITENS_PLANE_AVALI != null ? a.TBS386_ITENS_PLANE_AVALI.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO : (int?)null),
                           ID_SUB_GRUPO = (a.TBS386_ITENS_PLANE_AVALI != null ? a.TBS386_ITENS_PLANE_AVALI.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP : (int?)null),
                           ID_PROCED = (a.TBS386_ITENS_PLANE_AVALI != null ? a.TBS386_ITENS_PLANE_AVALI.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE : (int?)null),
                           NR_ACAO = (a.TBS386_ITENS_PLANE_AVALI != null ? a.TBS386_ITENS_PLANE_AVALI.NR_ACAO : (int?)null),
                           DE_ACAO = (a.TBS386_ITENS_PLANE_AVALI != null ? a.TBS386_ITENS_PLANE_AVALI.DE_RESUM_ACAO : ""),
                           ID_ITEM_PLAN = (a.TBS386_ITENS_PLANE_AVALI != null ? a.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI : (int?)null),
                           //Se tem item de planejamento, verifica se ainda está aberto, e retorna true caso sim, caso contrário retorna false
                           ItemAberto = (a.TBS386_ITENS_PLANE_AVALI != null ? (a.TBS386_ITENS_PLANE_AVALI.CO_SITUA == "A" ? true : false) : true),
                           //Libera para poder cancelar/descancelar item de planejamento apenas quando o mesmo estiver em aberto ou cancelado
                           podeExcluir = (a.TBS386_ITENS_PLANE_AVALI != null ? (a.TBS386_ITENS_PLANE_AVALI.CO_SITUA == "A" || a.TBS386_ITENS_PLANE_AVALI.CO_SITUA == "C" ? true : false) : false),
                           NM_PROCED = (a.TBS386_ITENS_PLANE_AVALI != null ? (a.TBS386_ITENS_PLANE_AVALI.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI) : ""),
                           coSituaItemPlanej = (a.TBS386_ITENS_PLANE_AVALI != null ? (a.TBS386_ITENS_PLANE_AVALI.CO_SITUA) : ""),
                       }).OrderBy(w => w.dt).ToList();

            grdHorario.DataSource = res;
            grdHorario.DataBind();
        }

        /// <summary>
        /// Carrega os grupos de procedimentos
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregarGrupos(DropDownList ddl, bool Relatorio = false, bool mostraPadrao = false, bool insereVazio = true)
        {
            AuxiliCarregamentos.CarregaGruposProcedimentosMedicos(ddl, Relatorio, mostraPadrao, insereVazio);
        }

        /// <summary>
        /// Carrega os subgrupos de procedimentos
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="ddlGrupo"></param>
        private void CarregarSubGrupos(DropDownList ddl, DropDownList ddlGrupo, bool Relatorio = false, bool mostraPadrao = false, bool insereVazio = true)
        {
            AuxiliCarregamentos.CarregaSubGruposProcedimentosMedicos(ddl, ddlGrupo, Relatorio, mostraPadrao, insereVazio);
        }

        /// <summary>
        /// Calcula os valores de tabela e calculado de acordo com desconto concedido à determinada operadora e plano de saúde informados no cadastro na página
        /// </summary>
        private void CalcularPreencherValoresTabelaECalculado(DropDownList ddlProcConsul, DropDownList ddlOperPlano, DropDownList ddlPlano, TextBox txtValor)
        {
            int idProc = (!string.IsNullOrEmpty(ddlProcConsul.SelectedValue) ? int.Parse(ddlProcConsul.SelectedValue) : 0);
            int idOper = (!string.IsNullOrEmpty(ddlOperPlano.SelectedValue) ? int.Parse(ddlOperPlano.SelectedValue) : 0);
            int idPlan = (!string.IsNullOrEmpty(ddlPlano.SelectedValue) ? int.Parse(ddlPlano.SelectedValue) : 0);

            //Apenas se tiver sido escolhido algum procedimento
            if (idProc != 0)
            {
                int? procAgrupador = (int?)null; // Se for procedimento de alguma operadora, verifica qual o id do procedimento agrupador do mesmo
                if (!string.IsNullOrEmpty(ddlOperPlano.SelectedValue))
                    procAgrupador = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(idProc).ID_AGRUP_PROC_MEDI_PROCE;

                AuxiliCalculos.ValoresProcedimentosMedicos ob = AuxiliCalculos.RetornaValoresProcedimentosMedicos((procAgrupador.HasValue ? procAgrupador.Value : idProc), idOper, idPlan);
                txtValor.Text = ob.VL_CALCULADO.ToString("N2"); // Insere o valor Calculado
            }
        }

        /// <summary>
        /// Grava na tabela de financeiro de procedimentos os devidos dados
        /// </summary>
        private void GravaFinanceiroProcedimentos(TBS356_PROC_MEDIC_PROCE tbs356, int CO_ALU, int CO_RESP, int ID_PLAN, int ID_OPER, int ID_AGEND_HORAR, int CO_COL)
        {
            var re = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                      where tb03.CO_COL == CO_COL
                      select new { tb03.CO_EMP }).FirstOrDefault();

            TBS357_PROC_MEDIC_FINAN tbs357 = new TBS357_PROC_MEDIC_FINAN();

            //Recebe objeto com o valor corrente do procedimento para determinado plano de saúde (Quando esta for a situação)
            AuxiliCalculos.ValoresProcedimentosMedicos valPrc = AuxiliCalculos.RetornaValoresProcedimentosMedicos(tbs356.ID_PROC_MEDI_PROCE, ID_OPER, ID_PLAN);

            tbs357.TBS174_AGEND_HORAR = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(ID_AGEND_HORAR);
            tbs357.TB250_OPERA = (ID_OPER != 0 ? TB250_OPERA.RetornaPelaChavePrimaria(ID_OPER) : null);
            tbs357.CO_COL_INCLU_LANC = LoginAuxili.CO_COL;
            tbs357.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            tbs357.IP_INCLU_LANC = Request.UserHostAddress;
            tbs357.CO_COL_PROFI_ATEND = CO_COL;
            tbs357.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(re.CO_EMP);
            tbs357.FL_SITUA = "A";
            tbs357.CO_COL_SITUA = LoginAuxili.CO_COL;
            tbs357.CO_EMP_SITUA = LoginAuxili.CO_EMP;
            tbs357.DT_SITUA = DateTime.Now;
            tbs357.IP_SITUA = Request.UserHostAddress;
            tbs357.DT_INCLU_LANC = DateTime.Now;
            tbs357.ID_ITEM = tbs356.ID_PROC_MEDI_PROCE;
            tbs357.NM_ITEM = (tbs356.NM_PROC_MEDI.Length > 100 ? tbs356.NM_PROC_MEDI.Substring(0, 100) : tbs356.NM_PROC_MEDI);
            tbs357.CO_TIPO_ITEM = "PCM";
            tbs357.CO_ORIGEM = "C"; //Determina que a origem desse registro financeiro é uma consulta
            tbs357.CO_ALU = CO_ALU;
            tbs357.CO_RESP = CO_RESP;
            //tbs357.DT_EVENT = DateTime.Now;

            //Questão de valores
            tbs357.VL_CUSTO_PROC = valPrc.VL_CUSTO;
            tbs357.VL_RESTI = valPrc.VL_RESTI;
            tbs357.VL_BASE = valPrc.VL_BASE;
            tbs357.VL_PROCE_LIQUI = valPrc.VL_CALCULADO;
            tbs357.VL_DSCTO = valPrc.VL_DESCONTO;
            tbs357.TBS353_VALOR_PROC_MEDIC_PROCE = (valPrc.ID_VALOR_PROC_MEDIC_PROCE != 0 ? TBS353_VALOR_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(valPrc.ID_VALOR_PROC_MEDIC_PROCE) : null);
            tbs357.TBS361_CONDI_PLANO_SAUDE = (valPrc.ID_CONDI_PLANO_SAUDE != 0 ? TBS361_CONDI_PLANO_SAUDE.RetornaPelaChavePrimaria(valPrc.ID_CONDI_PLANO_SAUDE) : null);

            TBS357_PROC_MEDIC_FINAN.SaveOrUpdate(tbs357, true);
        }

        /// <summary>
        /// Responsável por carregar os pacientes de acordo com o cpf concedido
        /// </summary>
        private void PesquisaPaciente()
        {
            //Verifica se o usuário optou por pesquisar por CPF ou por NIRE
            if (chkPesqCpf.Checked)
            {
                string cpf = (txtCPFPaci.Text != "" ? txtCPFPaci.Text.Replace(".", "").Replace("-", "").Trim() : "");

                //Valida se o usuário digitou ou não o CPF
                if (txtCPFPaci.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o CPF para pesquisa");
                    return;
                }

                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where tb07.NU_CPF_ALU == cpf
                           select new { tb07.CO_ALU, tb07.NU_NIS }).FirstOrDefault();

                ddlNomeUsu.SelectedValue = res.CO_ALU.ToString();
                txtNisUsu.Text = res.NU_NIS.ToString();
            }
            else if (chkPesqNire.Checked)
            {
                //Valida se o usuário deixou o campo em branco.
                if (txtNirePaci.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o NIRE para pesquisa");
                    return;
                }

                int nire = int.Parse(txtNirePaci.Text.Trim());

                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where tb07.NU_NIRE == nire
                           select new { tb07.CO_ALU, tb07.NU_NIS }).FirstOrDefault();

                ddlNomeUsu.SelectedValue = res.CO_ALU.ToString();
                txtNisUsu.Text = res.NU_NIS.ToString();
            }
        }

        /// <summary>
        /// Método responsável por enviar SMS caso a opção correspondente tenha sido selecionada
        /// </summary>
        private void EnviaSMS(bool NovoAgendamento, string hora, DateTime data, int CO_COL, int CO_ESPEC, int CO_EMP, string NU_CELULAR, string NO_ALU, int CO_ALU)
        {
            //***IMPORTANTE*** - O limite máximo de caracteres de acordo com a ZENVIA que é quem presta o serviço de envio,
            //é de 140 caracteres para NEXTEL e 150 para DEMAIS OPERADORAS
            TB03_COLABOR tb03 = TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == CO_COL).FirstOrDefault();
            string noEspec = TB63_ESPECIALIDADE.RetornaTodosRegistros().Where(w => w.CO_ESPECIALIDADE == CO_ESPEC).FirstOrDefault().NO_ESPECIALIDADE;
            string noEmp = TB25_EMPRESA.RetornaPelaChavePrimaria(CO_EMP).NO_FANTAS_EMP;

            noEspec = noEspec.Length > 23 ? noEspec.Substring(0, 23) : noEspec;
            bool masc = tb03.CO_SEXO_COL == "M" ? true : false;
            string noCol = tb03.NO_COL.Length > 40 ? tb03.NO_COL.Substring(0, 40) : tb03.NO_COL;
            string texto = "";
            if (NovoAgendamento)
                texto = "Consulta na especialidade " + noEspec + " com " + (masc ? "o Dr" : "a Dra ") + noCol + " agendada para o dia " + data.ToString("dd/MM") + ", às " + hora;
            else
                texto = "Consulta na especialidade " + noEspec + " com " + (masc ? "o Dr" : "a Dra ") + noCol + " reagendada para o dia " + data.ToString("dd/MM") + ", às " + hora;

            //Envia a mensagem apenas se o número do celular for diferente de nulo
            if (NU_CELULAR != null)
            {
                var admUsuario = ADMUSUARIO.RetornaPelaChavePrimaria(LoginAuxili.IDEADMUSUARIO);
                string retorno = "";

                if (admUsuario.QT_SMS_MES_USR != null && admUsuario.QT_SMS_MAXIM_USR != null)
                {
                    if (admUsuario.QT_SMS_MAXIM_USR <= admUsuario.QT_SMS_MES_USR)
                    {
                        retorno = "Saldo do envio de SMS para outras pessoas ultrapassado.";
                        return;
                    }
                }

                if (!Page.IsValid)
                    return;
                try
                {
                    //Salva na tabela de mensagens enviadas, as informações pertinentes
                    TB249_MENSAG_SMS tb249 = new TB249_MENSAG_SMS();
                    tb249.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);
                    tb249.CO_RECEPT = CO_ALU;
                    tb249.CO_EMP_RECEPT = CO_EMP;
                    tb249.NO_RECEPT_SMS = NO_ALU != "" ? NO_ALU : NO_ALU;
                    tb249.DT_ENVIO_MENSAG_SMS = DateTime.Now;
                    tb249.DES_MENSAG_SMS = texto.Length > 150 ? texto.Substring(0, 150) : texto;
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

                    tb249.CO_TP_CONTAT_SMS = "A";

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
                        tb249.CO_STATUS = "E";
                    else
                        tb249.CO_STATUS = "N";

                    TB249_MENSAG_SMS.SaveOrUpdate(tb249, false);
                }
                catch (Exception)
                {
                    retorno = "Mensagem não foi enviada com sucesso.";
                }
                //GestorEntities.CurrentContext.SaveChanges();
            }
        }

        /// <summary>
        /// Carrega especialidades de acordo com a empresa selecionada.
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregaEspecialidade(DropDownList ddl)
        {
            int coEmp = ddlUnidResCons.SelectedValue != "" ? int.Parse(ddlUnidResCons.SelectedValue) : 0;

            //Carrega apenas as especialidades que possuem algum colaborador associado
            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                       where tb03.FLA_PROFESSOR == "S"
                        && (coEmp != 0 ? tb63.CO_EMP == coEmp : 0 == 0)
                       select new { tb63.NO_ESPECIALIDADE, tb63.CO_ESPECIALIDADE }).Distinct().OrderBy(w => w.NO_ESPECIALIDADE).ToList();

            ddl.DataTextField = "NO_ESPECIALIDADE";
            ddl.DataValueField = "CO_ESPECIALIDADE";
            ddl.DataSource = res;
            ddl.DataBind();

            if (res.Count() > 0)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Sem Especialidades com Plantonistas", ""));
        }

        /// <summary>
        /// Carrega as unidades de acordo com a Instituição logada.
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregaUnidades(DropDownList ddl)
        {
            //AuxiliCarregamentos.CarregaUnidade(ddl, LoginAuxili.ORG_CODIGO_ORGAO, true);

            //Carrega apenas as unidades que possuem algum colaborador com FLAG de Profissional de Saúde
            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb03.TB25_EMPRESA.CO_EMP equals tb25.CO_EMP
                       where tb03.FLA_PROFESSOR == "S"
                       select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(w => w.NO_FANTAS_EMP).ToList();

            ddl.DataTextField = "NO_FANTAS_EMP";
            ddl.DataValueField = "CO_EMP";
            ddl.DataSource = res;
            ddl.DataBind();

            if (res.Count() > 0)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Sem Unidades com Plantonistas", ""));
        }

        /// <summary>
        /// Carrega as Classificações Profissionais
        /// </summary>
        private void CarregaClassificacoes()
        {
            AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassProfi, true);
        }

        /// <summary>
        /// Carrega os pacientes
        /// </summary>
        private void CarregaPacientes()
        {
            AuxiliCarregamentos.CarregaPacientes(ddlNomeUsu, LoginAuxili.CO_EMP, false);
        }

        /// <summary>
        /// Carrega os departamentos de acordo com a empresa selecionada.
        /// </summary>
        private void CarregaDepartamento()
        {
            int coEmp = (ddlUnidResCons.SelectedValue != "" ? int.Parse(ddlUnidResCons.SelectedValue) : 0);
            //AuxiliCarregamentos.CarregaDepartamentos(ddlDept, coEmp, true);
            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tb03.CO_DEPTO equals tb14.CO_DEPTO
                       where (coEmp != 0 ? tb14.TB25_EMPRESA.CO_EMP == coEmp : 0 == 0)
                       select new { tb14.NO_DEPTO, tb14.CO_DEPTO }).Distinct().OrderBy(i => i.NO_DEPTO).ToList();

            ddlDept.DataTextField = "NO_DEPTO";
            ddlDept.DataValueField = "CO_DEPTO";
            ddlDept.DataSource = res;
            ddlDept.DataBind();

            ddlDept.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// É chamado quando se clica em um registro na grid de horários que já possua Agendamento, é rensável por providenciar o carregamento do Paciente e Tipo de Consulta
        /// </summary>
        private void CarregaInfosAgenda(int CO_ALU, string TP_CONSUL)
        {
            if (CO_ALU != 0)
                ddlNomeUsu.SelectedValue = CO_ALU.ToString();

            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where tb07.CO_ALU == CO_ALU
                       select new { tb07.NU_NIS }).FirstOrDefault();

            //updTopo.Update();

            //Atribui o nis do registro de agendamento ao campo correto
            //if (res != null)
            //    txtNisUsu.Text = res.NU_NIS.ToString();

            ////Atribui as informações aos campos correspondentes
            //if (CO_EMP != 0)
            //    ddlUnidResCons.SelectedValue = CO_EMP.ToString();

            //if (CO_DEPTO != 0)
            //    ddlDept.SelectedValue = CO_DEPTO.ToString();

            //CarregaGridProfi();

            ////Responsável por procurar o colaborador que relacionado à agenda corrente e marcá-lo.
            //if (CO_COL != 0)
            //{
            //    foreach (GridViewRow li in grdProfi.Rows)
            //    {
            //        string coCol = ((HiddenField)li.Cells[0].FindControl("hidCoCol")).Value;
            //        int coColI = (!string.IsNullOrEmpty(coCol) ? int.Parse(coCol) : 0);
            //        CheckBox ck = (((CheckBox)li.Cells[0].FindControl("ckSelect")));

            //        if (coColI == CO_COL)
            //            ck.Checked = true;
            //    }
            //}            
        }

        /// <summary>
        /// Carrega as Cidades relacionadas ao Bairro selecionado anteriormente.
        /// </summary>
        private void carregaCidade()
        {
            string uf = ddlUF.SelectedValue;
            AuxiliCarregamentos.CarregaCidades(ddlCidade, false, uf, LoginAuxili.CO_EMP, true, true);
            //if (uf != "")
            //{
            //    var res = (from tb904 in TB904_CIDADE.RetornaTodosRegistros()
            //               where tb904.CO_UF == uf
            //               select new { tb904.NO_CIDADE, tb904.CO_CIDADE });

            //    ddlCidade.DataTextField = "NO_CIDADE";
            //    ddlCidade.DataValueField = "CO_CIDADE";
            //    ddlCidade.DataSource = res;
            //    ddlCidade.DataBind();

            //    ddlCidade.Items.Insert(0, new ListItem("Selecione", ""));
            //}
            //else
            //{
            //    ddlCidade.Items.Clear();
            //    ddlCidade.Items.Insert(0, new ListItem("Selecione", ""));
            //}
        }

        /// <summary>
        /// Carrega os Bairros ligados à UF e Cidade selecionados anteriormente.
        /// </summary>
        private void carregaBairro()
        {
            string uf = ddlUF.SelectedValue;
            int cid = ddlCidade.SelectedValue != "" ? int.Parse(ddlCidade.SelectedValue) : 0;

            if ((uf != "") && (cid != 0))
            {
                var res = (from tb905 in TB905_BAIRRO.RetornaTodosRegistros()
                           where tb905.CO_CIDADE == cid
                           && (tb905.CO_UF == uf)
                           select new { tb905.NO_BAIRRO, tb905.CO_BAIRRO });

                ddlBairro.DataTextField = "NO_BAIRRO";
                ddlBairro.DataValueField = "CO_BAIRRO";
                ddlBairro.DataSource = res;
                ddlBairro.DataBind();

                ddlBairro.Items.Insert(0, new ListItem("Selecione", ""));
            }
            else
            {
                ddlBairro.Items.Clear();
                ddlBairro.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        /// <summary>
        /// Carrega o histórico de agendamentos do paciente recebido como parâmetro;
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void CarregaGridHistoricoPaciente(int CO_ALU)
        {
            CarregaDatasIniFimPaciente(CO_ALU);
            DateTime dtIni = (!string.IsNullOrEmpty(txtDtIniHistoUsuar.Text) ? DateTime.Parse(txtDtIniHistoUsuar.Text) : DateTime.Now);
            DateTime dtFim = (!string.IsNullOrEmpty(txtDtFimHistoUsuar.Text) ? DateTime.Parse(txtDtFimHistoUsuar.Text) : DateTime.Now);

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                       where tbs174.CO_ALU == CO_ALU
                       && (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni)
                       && EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim))
                       //&&   a.DT_AGEND_HORAR >= dtIni && a.DT_AGEND_HORAR <= dtFim
                       select new HorarioHistoricoPaciente
                       {
                           OPER = tbs174.TB250_OPERA != null ? tbs174.TB250_OPERA.NM_SIGLA_OPER : " - ",
                           PROCED = tbs174.TBS386_ITENS_PLANE_AVALI != null ? tbs174.TBS386_ITENS_PLANE_AVALI.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI : " - ",
                           NR_ACAO_R = (tbs174.TBS386_ITENS_PLANE_AVALI != null ? tbs174.TBS386_ITENS_PLANE_AVALI.NR_ACAO : (int?)null),
                           STATUS = tbs174.CO_SITUA_AGEND_HORAR,
                           TP_PROCED = tbs174.CO_TIPO_PROC_MEDI,
                           DT = tbs174.DT_AGEND_HORAR,
                           HR = tbs174.HR_AGEND_HORAR,
                           NO_PROFI = tb03.NO_APEL_COL,
                           CO_CLASS = tb03.CO_CLASS_PROFI,
                           FL_CONFIR = tbs174.FL_CONF_AGEND,
                           FL_CANCE_JUSTIF = tbs174.FL_JUSTI_CANCE,
                           FL_AGEND_ENCAM = tbs174.FL_AGEND_ENCAM,
                       }).OrderBy(w => w.DT).ToList();

            grdHistorPaciente.DataSource = res;
            grdHistorPaciente.DataBind();
        }

        /// <summary>
        /// Carrega os procedimentos da instituição e seleciona o recebido como parâmetro
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="selec"></param>
        private void CarregaProcedimentos(DropDownList ddl, DropDownList ddlOper, DropDownList ddlGrupo, DropDownList ddlSubGrupo, string selec = null, bool insereVazio = false)
        {
            int idOper = (!string.IsNullOrEmpty(ddlOper.SelectedValue) ? int.Parse(ddlOper.SelectedValue) : 0);
            int grupo = (!string.IsNullOrEmpty(ddlGrupo.SelectedValue) ? int.Parse(ddlGrupo.SelectedValue) : 0);
            int Subgrupo = (!string.IsNullOrEmpty(ddlSubGrupo.SelectedValue) ? int.Parse(ddlSubGrupo.SelectedValue) : 0);
            var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                       where tbs356.CO_SITU_PROC_MEDI == "A"
                       && (idOper != 0 ? tbs356.TB250_OPERA.ID_OPER == idOper : tbs356.TB250_OPERA == null)
                       && (grupo != 0 ? tbs356.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == grupo : 0 == 0)
                       && (Subgrupo != 0 ? tbs356.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP == Subgrupo : 0 == 0)
                       select new { tbs356.ID_PROC_MEDI_PROCE, CO_PROC_MEDI = tbs356.CO_PROC_MEDI + " - " + tbs356.NM_PROC_MEDI }).OrderBy(w => w.CO_PROC_MEDI).ToList();

            if (res != null)
            {
                ddl.DataTextField = "CO_PROC_MEDI";
                ddl.DataValueField = "ID_PROC_MEDI_PROCE";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (insereVazio)
                ddl.Items.Insert(0, new ListItem("", ""));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));

            if (!string.IsNullOrEmpty(selec))
                ddl.SelectedValue = selec;
        }

        /// <summary>
        /// Sobrecarga do método que carrega as operadoras de plano de saúde já selecionando o valor recebido como parâmetro
        /// </summary>
        private void CarregaOperadoras(DropDownList ddl, string selec)
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddl, false, false, true);

            if (!string.IsNullOrEmpty(selec))
                ddl.SelectedValue = selec;
        }

        /// <summary>
        /// Carrega os planos de saúde da operadora recebida como parâmetro
        /// </summary>
        /// <param name="ddlPlan"></param>
        /// <param name="ddlOper"></param>
        private void CarregarPlanosSaude(DropDownList ddlPlan, DropDownList ddlOper)
        {
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlan, ddlOper, false, false, true);
        }

        /// <summary>
        /// Executa método javascript que mostra a Modal com o histórico de ocorrências
        /// </summary>
        private void abreModalInfosCadastrais()
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                "AbreModalInfosCadas();",
                true
            );
        }

        /// <summary>
        /// Executa método javascript que mostra a Modal com o Registro de Informações Financeiras
        /// </summary>
        private void abreModalInfosFinanceiras()
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                "AbreModalInfosFinan();",
                true
            );

            //UpdFinanceiro.Update();
        }

        /// <summary>
        /// Carrega as datas de início e fim de consultas de um determinado profissional recebido como parâmetro
        /// </summary>
        private void CarregaDatasIniFim(int CO_COL)
        {
            //txtDtIniResCons.Text = DateTime.Now.AddMonths(-2).ToString();
            //txtDtFimResCons.Text = DateTime.Now.AddDays(3).ToString();
        }

        /// <summary>
        /// Carrega a primeira data de início e final para o paciente recebido como parâmetro
        /// </summary>
        /// <param name="CO_COL"></param>
        private void CarregaDatasIniFimPaciente(int CO_ALU)
        {
            //txtDtIniHistoUsuar.Text = DateTime.Now.AddMonths(-2).ToString();
            //txtDtFimHistoUsuar.Text = DateTime.Now.ToString();
            //var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
            //           where tbs174.CO_ALU == CO_ALU
            //           select new
            //           {
            //               tbs174.DT_AGEND_HORAR,
            //           }).ToList();

            ////Seta a primeira e última data de consultas do colaborador recebido como parâmetro
            //if (res.Count > 0)
            //{
            //    txtDtIniHistoUsuar.Text = (res != null ? res.FirstOrDefault().DT_AGEND_HORAR.ToString() : DateTime.Now.ToString());
            //    txtDtFimHistoUsuar.Text = (res != null ? res.LastOrDefault().DT_AGEND_HORAR.ToString() : DateTime.Now.ToString());
            //}
        }

        /// <summary>
        /// Pesquisa se já existe um responsável com o CPF informado na tabela de responsáveis, se já existe ele preenche as informações do responsável 
        /// </summary>
        private void PesquisaCarregaResp(int? co_resp)
        {
            string cpfResp = txtCPFResp.Text.Replace(".", "").Replace("-", "");

            var res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                       where (co_resp.HasValue ? tb108.CO_RESP == co_resp : tb108.NU_CPF_RESP == cpfResp)
                       select new
                       {
                           tb108.NO_RESP,
                           tb108.CO_RG_RESP,
                           tb108.CO_ORG_RG_RESP,
                           tb108.NU_CPF_RESP,
                           tb108.CO_ESTA_RG_RESP,
                           tb108.DT_NASC_RESP,
                           tb108.CO_SEXO_RESP,
                           tb108.CO_CEP_RESP,
                           tb108.CO_ESTA_RESP,
                           tb108.CO_CIDADE,
                           tb108.CO_BAIRRO,
                           tb108.DE_ENDE_RESP,
                           tb108.DES_EMAIL_RESP,
                           tb108.NU_TELE_CELU_RESP,
                           tb108.NU_TELE_RESI_RESP,
                           tb108.CO_ORIGEM_RESP,
                           tb108.CO_RESP,
                           tb108.NU_TELE_WHATS_RESP,
                           tb108.NM_FACEBOOK_RESP,
                           tb108.NU_TELE_COME_RESP,
                       }).FirstOrDefault();

            if (res != null)
            {
                txtNomeResp.Text = res.NO_RESP;
                txtCPFResp.Text = res.NU_CPF_RESP;
                txtNuIDResp.Text = res.CO_RG_RESP;
                txtOrgEmiss.Text = res.CO_ORG_RG_RESP;
                ddlUFOrgEmis.SelectedValue = res.CO_ESTA_RG_RESP;
                txtDtNascResp.Text = res.DT_NASC_RESP.ToString();
                ddlSexResp.SelectedValue = res.CO_SEXO_RESP;
                txtCEP.Text = res.CO_CEP_RESP;
                ddlUF.SelectedValue = res.CO_ESTA_RESP;
                carregaCidade();
                ddlCidade.SelectedValue = res.CO_CIDADE != null ? res.CO_CIDADE.ToString() : "";
                carregaBairro();
                ddlBairro.SelectedValue = res.CO_BAIRRO != null ? res.CO_BAIRRO.ToString() : "";
                txtLograEndResp.Text = res.DE_ENDE_RESP;
                txtEmailResp.Text = res.DES_EMAIL_RESP;
                txtTelCelResp.Text = res.NU_TELE_CELU_RESP;
                txtTelFixResp.Text = res.NU_TELE_RESI_RESP;
                txtNuWhatsResp.Text = res.NU_TELE_WHATS_RESP;
                txtTelComResp.Text = res.NU_TELE_COME_RESP;
                txtDeFaceResp.Text = res.NM_FACEBOOK_RESP;
                hidCoResp.Value = res.CO_RESP.ToString();
            }
            //updCadasUsuario.Update();
        }

        /// <summary>
        /// Método que duplica as informações do responsável nos campos do paciente, quando o usuário clica em Paciente é o Responsável.
        /// </summary>
        private void carregaPaciehoResponsavel()
        {
            if (chkPaciEhResp.Checked)
            {
                txtCPFMOD.Text = txtCPFResp.Text;
                txtnompac.Text = txtNomeResp.Text;
                txtDtNascPaci.Text = txtDtNascResp.Text;
                ddlSexoPaci.SelectedValue = ddlSexResp.SelectedValue;
                txtTelCelPaci.Text = txtTelCelResp.Text;
                txtTelResPaci.Text = txtTelFixResp.Text;
                ddlGrParen.SelectedValue = "OU";
                txtEmailPaci.Text = txtEmailResp.Text;
                txtWhatsPaci.Text = txtNuWhatsResp.Text;

                txtEmailPaci.Enabled = false;
                txtCPFMOD.Enabled = false;
                txtnompac.Enabled = false;
                txtDtNascPaci.Enabled = false;
                ddlSexoPaci.Enabled = false;
                txtTelCelPaci.Enabled = false;
                txtTelCelPaci.Enabled = false;
                txtTelResPaci.Enabled = false;
                ddlGrParen.Enabled = false;
                txtWhatsPaci.Enabled = false;

                #region Verifica se já existe

                string cpf = txtCPFMOD.Text.Replace(".", "").Replace("-", "").Trim();

                var tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_CPF_ALU == cpf).FirstOrDefault();

                if (tb07 != null)
                    hidCoPac.Value = tb07.CO_ALU.ToString();

                #endregion

            }
            else
            {
                txtCPFMOD.Text = "";
                txtnompac.Text = "";
                txtDtNascPaci.Text = "";
                ddlSexoPaci.SelectedValue = "";
                txtTelCelPaci.Text = "";
                txtTelResPaci.Text = "";
                ddlGrParen.SelectedValue = "";
                txtEmailPaci.Text = "";
                txtWhatsPaci.Text = "";

                txtCPFMOD.Enabled = true;
                txtnompac.Enabled = true;
                txtDtNascPaci.Enabled = true;
                ddlSexoPaci.Enabled = true;
                txtTelCelPaci.Enabled = true;
                txtTelCelPaci.Enabled = true;
                txtTelResPaci.Enabled = true;
                ddlGrParen.Enabled = true;
                txtEmailPaci.Enabled = true;
                txtWhatsPaci.Enabled = true;
                hidCoPac.Value = "";
            }
            //updCadasUsuario.Update();
        }

        /// <summary>
        /// Torna a primeira letra maiúscula
        /// </summary>
        private static string PrimeiraLetraMaiuscula(string tex)
        {
            if (string.IsNullOrEmpty(tex))
                return string.Empty;

            return tex.Substring(0, 1).ToUpper() + tex.Substring(1);
        }

        public class HorarioHistoricoPaciente
        {
            public string OPER { get; set; }
            public string TP_PROCED { get; set; }
            public string TP_PROCED_V
            {
                get
                {
                    switch (this.TP_PROCED)
                    {
                        case "CO":
                            return "Consulta";
                        case "EX":
                            return "Exame";
                        case "SS":
                            return "Serv.Saúde";
                        case "PR":
                            return "Procedimento";
                        case "SA":
                            return "Serv. Ambulatorial";
                        case "OU":
                            return "Outros";
                        default:
                            return " - ";
                    }
                }
            }
            public string PROCED { get; set; }
            public string STATUS { get; set; }
            public DateTime DT { get; set; }
            public string HR { get; set; }
            public string DT_HORAR
            {
                get
                {
                    string diaSemana = this.DT.ToString("ddd", new CultureInfo("pt-BR"));
                    return this.DT.ToShortDateString() + " - " + this.HR + " " + diaSemana;
                }
            }
            public int? NR_ACAO_R { get; set; }
            public string NR_ACAO
            {
                get
                {
                    return (this.NR_ACAO_R.HasValue ? this.NR_ACAO_R.Value.ToString("00") : " - ");
                }
            }
            public string NO_PROFI { get; set; }
            public string CO_CLASS { get; set; }
            public string CO_CLASS_V
            {
                get
                {
                    return (AuxiliGeral.GetNomeClassificacaoFuncional(this.CO_CLASS, true));
                }
            }

            public string FL_CONFIR { get; set; }
            public string FL_CANCE_JUSTIF { get; set; }
            public string FL_AGEND_ENCAM { get; set; }
            public string IMG_URL
            {
                get
                {
                    string s = "";
                    switch (this.STATUS)
                    {
                        //Pode estar ativa, mas já encaminhada ou confirmada, então precisa tratar as três
                        case "A":
                            if (this.FL_AGEND_ENCAM == "S")
                                s = "/Library/IMG/PGS_SF_AgendaEncaminhada.png"; // Primeiramente se está encaminhado
                            else if (this.FL_CONFIR == "S")
                                s = "/Library/IMG/PGS_SF_AgendaConfirmada.png"; // Confirmado
                            else
                                s = "/Library/IMG/PGS_SF_AgendaEmAberto.png"; // Ou apenas ativo
                            break;
                        case "C": //Se está cancelada (por ora, o status de falta ainda está junto ao de cancelamento, e aqui é tratado o justificado ou não)
                            s = (this.FL_CANCE_JUSTIF == "S" ? "/Library/IMG/PGS_SF_AgendaFaltaJustificada.png" : "/Library/IMG/PGS_SF_AgendaFaltaNaoJustificada.png");
                            break;
                        case "R": //Se está realizada
                            s = "/Library/IMG/PGS_SF_AgendaRealizada.png";
                            break;
                        default:
                            s = "/Library/IMG/Gestor_SemImagem.png";
                            break;
                    }
                    return s;
                }
            }
            public string IMG_TIP
            {
                get
                {
                    string s = "";
                    switch (this.STATUS)
                    {
                        //Pode estar ativa, mas já encaminhada ou confirmada, então precisa tratar as três
                        case "A":
                            if (this.FL_AGEND_ENCAM == "S")
                                s = "Agendamento Encaminhado para Atendimento"; // Primeiramente se está encaminhado
                            else if (this.FL_CONFIR == "S")
                                s = "Agendamento Confirmado"; // Confirmado
                            else
                                s = "Agendamento em Aberto"; // Ou apenas em Aberto
                            break;
                        case "C": //Se está cancelada (por ora, o status de falta ainda está junto ao de cancelamento, e aqui é tratado o justificado ou não)
                            s = (this.FL_CANCE_JUSTIF == "S" ? "Agendamento com Falta Justificada" : "Agendamento com falta NÃO Justificada");
                            break;
                        case "R": //Se está realizada
                            s = "Agendamento para Agendamento Realizado";
                            break;
                        default:
                            s = "**Agendamento Sem Situação**";
                            break;
                    }
                    return s;
                }
            }
        }

        public class HorarioSaida
        {
            //Carrega informações gerais do agendamento
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
            public int? CO_COL { get; set; }
            public string NM_PROCED { get; set; }
            public string coSituaItemPlanej { get; set; }

            //Dados da Grid
            public int? ID_GRUPO { get; set; }
            public int? ID_SUB_GRUPO { get; set; }
            public int? ID_PROCED { get; set; }
            public int? NR_ACAO { get; set; }
            public string NR_ACAO_V
            {
                get
                {
                    return (this.NR_ACAO.HasValue ? this.NR_ACAO.Value.ToString("00") : "");
                }
            }
            public string DE_ACAO { get; set; }
            public bool ItemAberto { get; set; }
            public bool podeExcluir { get; set; }

            //Carrega as informações do usuário quando já houver agendamento para o horário em questão
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

            public int? CO_OPER { get; set; }
            public int? CO_PLAN { get; set; }
            public int? ID_PROC { get; set; }

            public int? ID_ITEM_PLAN { get; set; }
        }

        /// <summary>
        /// Carrega a grid de profissionais da saúde
        /// </summary>
        private void CarregaGridProfi()
        {
            int coEmp = (!string.IsNullOrEmpty(ddlUnidResCons.SelectedValue) ? int.Parse(ddlUnidResCons.SelectedValue) : 0);
            int coDep = (!string.IsNullOrEmpty(ddlDept.SelectedValue) ? int.Parse(ddlDept.SelectedValue) : 0);
            string coClassProfi = ddlClassProfi.SelectedValue;

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb03.CO_EMP equals tb25.CO_EMP
                       where tb03.FLA_PROFESSOR == "S"
                       && (coEmp != 0 ? tb03.CO_EMP == coEmp : coEmp == 0)
                       && (coDep != 0 ? tb03.CO_DEPTO == coDep : coDep == 0)
                       && (coClassProfi != "0" ? tb03.CO_CLASS_PROFI == coClassProfi : 0 == 0)
                       select new GrdProfiSaida
                       {
                           CO_COL = tb03.CO_COL,
                           NO_COL_RECEB = tb03.NO_COL,
                           NO_APEL_COL = tb03.NO_APEL_COL,
                           NO_EMP = tb25.sigla,
                           MATR_COL = tb03.CO_MAT_COL,
                           NU_TEL = tb03.NU_TELE_CELU_COL,
                           CO_CLASS_FUNC = tb03.CO_CLASS_PROFI,
                       }).OrderBy(o => o.NO_COL_RECEB).ToList();

            grdProfi.DataSource = res;
            grdProfi.DataBind();
        }

        public class GrdProfiSaida
        {
            public int CO_COL { get; set; }
            public string MATR_COL { get; set; }
            public string NO_COL
            {
                get
                {
                    string maCol = this.MATR_COL.PadLeft(6, '0').Insert(2, ".").Insert(6, "-");
                    string noCol = (this.NO_COL_RECEB.Length > 34 ? this.NO_COL_RECEB.Substring(0, 34) + "..." : this.NO_COL_RECEB);
                    return maCol + " - " + noCol;
                }
            }
            public string NO_APEL_COL { get; set; }
            public string NO_COL_RECEB { get; set; }
            public string NO_EMP { get; set; }
            public string DE_ESP { get; set; }
            public string NU_TEL { get; set; }
            public string NU_TEL_V
            {
                get
                {
                    return AuxiliFormatoExibicao.PreparaTelefone(this.NU_TEL);
                }
            }
            public string CO_CLASS_FUNC { get; set; }
            public string DE_CLASS_FUNC
            {
                get
                {
                    return AuxiliGeral.GetNomeClassificacaoFuncional(this.CO_CLASS_FUNC, true);
                }
            }
        }

        #endregion

        #region Eventos de componentes

        protected void grdHorario_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                DropDownList ddlGrupo, ddlSubGrupo, ddlProced;
                ddlGrupo = (((DropDownList)e.Row.FindControl("ddlGrupo")));
                ddlSubGrupo = (((DropDownList)e.Row.FindControl("ddlSubGrupo")));
                ddlProced = (((DropDownList)e.Row.FindControl("ddlProcedimento")));

                string idGrupo, idSubGrupo, idProced;
                idGrupo = (((HiddenField)e.Row.FindControl("hidIdGrupo")).Value);
                idSubGrupo = (((HiddenField)e.Row.FindControl("hidIdSubGrupo")).Value);
                idProced = (((HiddenField)e.Row.FindControl("hidIdProced")).Value);

                //Carrega e seleciona grupo se houver
                CarregarGrupos(ddlGrupo, false, true, false);
                if (!string.IsNullOrEmpty(idGrupo))
                    ddlGrupo.SelectedValue = idGrupo;

                //Carrega e seleciona subgrupo se houver
                CarregarSubGrupos(ddlSubGrupo, ddlGrupo, false, true, false);
                if (!string.IsNullOrEmpty(idSubGrupo))
                    ddlSubGrupo.SelectedValue = idSubGrupo;

                //Carrega e seleciona procedimento se houver
                CarregaProcedimentos(ddlProced, ddlOperadora, ddlGrupo, ddlSubGrupo, idProced);

                //Se o item de planejamento estiver cancelado, destaca em cor salmão
                if((((HiddenField)e.Row.FindControl("hidSituaItemPlanej")).Value) == "C")
                    e.Row.BackColor = System.Drawing.Color.AntiqueWhite;
            }
        }

        protected void ddlUnidResCons_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDepartamento();
        }

        protected void imgCpfResp_OnClick(object sender, EventArgs e)
        {
            PesquisaPaciente();
            //PesquisaCarregaResp(null);
        }

        protected void chkPesqNire_OnCheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                txtNirePaci.Enabled = true;
                chkPesqCpf.Checked = txtCPFPaci.Enabled = false;
                txtCPFPaci.Text = "";
            }
            else
            {
                txtNirePaci.Enabled = false;
                txtNirePaci.Text = "";
            }
        }

        protected void chkPesqCpf_OnCheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                txtCPFPaci.Enabled = true;
                chkPesqNire.Checked = txtNirePaci.Enabled = false;
                txtNirePaci.Text = "";
            }
            else
            {
                txtNirePaci.Enabled = false;
                txtNirePaci.Text = "";
            }
        }

        protected void imgCadPac_OnClick(object sender, EventArgs e)
        {
            divResp.Visible = true;
            divSuccessoMessage.Visible = false;
            //updCadasUsuario.Update();
            abreModalInfosCadastrais();
        }

        protected void lnkSalvarPaciente_OnClick(object sender, EventArgs e)
        {
            TB07_ALUNO tb07 = new TB07_ALUNO();

            //tb07.NO_ALU = txtNomeAluMod.Text;
            //tb07.NU_CPF_ALU = txtCpfAluMod.Text.Replace(".", "").Replace("-", "").Trim();
            //tb07.NU_NIS = (!string.IsNullOrEmpty(txtNisAluMod.Text) ? decimal.Parse(txtNisAluMod.Text) : (decimal?)null);
            //tb07.DT_NASC_ALU = DateTime.Parse(txtDataNascimentoAluMod.Text);
            //tb07.CO_SEXO_ALU = ddlSexoAluMod.SelectedValue;
            ////tb07.NU_TELE_CELU_ALU = txtTelCelUsu.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            ////tb07.NU_TELE_RESI_ALU = txtTelResUsu.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            ////tb07.CO_GRAU_PAREN_RESP = ddlGrauParen.SelectedValue;
            //tb07.CO_EMP = LoginAuxili.CO_EMP;
            //tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            //tb07.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            ////tb07.TB108_RESPONSAVEL = (tb108 != null ? tb108 : null);

            ////Salva os valores para os campos not null da tabela de Usuário
            //tb07.CO_SITU_ALU = "A";
            //tb07.TP_DEF = "N";

            //#region trata para criação do nire

            //var resNire = (from tb07pesq in TB07_ALUNO.RetornaTodosRegistros()
            //               select new { tb07pesq.NU_NIRE }).OrderByDescending(w => w.NU_NIRE).FirstOrDefault();

            //int nir = 0;
            //if (resNire == null)
            //{
            //    nir = 1;
            //}
            //else
            //{
            //    nir = resNire.NU_NIRE;
            //}

            //int nirTot = nir + 1;

            //#endregion
            //tb07.NU_NIRE = nirTot;

            TB07_ALUNO.SaveOrUpdate(tb07, true);
        }

        protected void chkPaciEhResp_OnCheckedChanged(object sender, EventArgs e)
        {
            carregaPaciehoResponsavel();

            if (chkPaciEhResp.Checked)
                chkPaciMoraCoResp.Checked = true;

            abreModalInfosCadastrais();
        }

        protected void imgPesqCEP_OnClick(object sender, EventArgs e)
        {
            if (txtCEP.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCEP.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where(c => c.CO_CEP == numCep).FirstOrDefault();

                if (tb235 != null)
                {
                    txtLograEndResp.Text = tb235.NO_ENDER_CEP;
                    tb235.TB905_BAIRROReference.Load();
                    ddlUF.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    carregaCidade();
                    ddlCidade.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    carregaBairro();
                    ddlBairro.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                }
                else
                {
                    txtLograEndResp.Text = "";
                    ddlBairro.SelectedValue = "";
                    ddlCidade.SelectedValue = "";
                    ddlUF.SelectedValue = "";
                }
            }
            abreModalInfosCadastrais();
            //updCadasUsuario.Update();
        }

        protected void ddlUF_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaCidade();
            ddlCidade.Focus();
            abreModalInfosCadastrais();
        }

        protected void ddlCidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaBairro();
            ddlBairro.Focus();
            abreModalInfosCadastrais();
        }

        protected void lnkSalvar_OnClick(object sender, EventArgs e)
        {
            abreModalInfosCadastrais();
            if (string.IsNullOrEmpty(txtNuNisPaci.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Nis do Paciente é requerido");
                txtNuNisPaci.Focus();
                //updCadasUsuario.Update();
                return;
            }

            //Salva os dados do Responsável na tabela 108
            #region Salva Responsável na tb108

            TB108_RESPONSAVEL tb108;
            //Verifica se já não existe um responsável cadastrdado com esse CPF, caso não exista ele cadastra um novo com os dados informados
            if (string.IsNullOrEmpty(hidCoResp.Value))
            {
                tb108 = new TB108_RESPONSAVEL();

                tb108.NO_RESP = txtNomeResp.Text;
                tb108.NU_CPF_RESP = txtCPFResp.Text.Replace("-", "").Replace(".", "").Trim();
                tb108.CO_RG_RESP = txtNuIDResp.Text;
                tb108.CO_ORG_RG_RESP = txtOrgEmiss.Text;
                tb108.CO_ESTA_RG_RESP = ddlUFOrgEmis.SelectedValue;
                tb108.DT_NASC_RESP = DateTime.Parse(txtDtNascResp.Text);
                tb108.CO_SEXO_RESP = ddlSexResp.SelectedValue;
                tb108.CO_CEP_RESP = txtCEP.Text;
                tb108.CO_ESTA_RESP = ddlUF.SelectedValue;
                tb108.CO_CIDADE = int.Parse(ddlCidade.SelectedValue);
                tb108.CO_BAIRRO = int.Parse(ddlBairro.SelectedValue);
                tb108.DE_ENDE_RESP = txtLograEndResp.Text;
                tb108.DES_EMAIL_RESP = txtEmailResp.Text;
                tb108.NU_TELE_CELU_RESP = txtTelCelResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.NU_TELE_RESI_RESP = txtTelFixResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.NU_TELE_WHATS_RESP = txtNuWhatsResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.NU_TELE_COME_RESP = txtTelComResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb108.CO_ORIGEM_RESP = "NN";
                tb108.CO_SITU_RESP = "A";

                //Atribui valores vazios para os campos not null da tabela de Responsável.
                tb108.FL_NEGAT_CHEQUE = "V";
                tb108.FL_NEGAT_SERASA = "V";
                tb108.FL_NEGAT_SPC = "V";
                tb108.CO_INST = 0;
                tb108.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                tb108 = TB108_RESPONSAVEL.SaveOrUpdate(tb108);
            }
            else
            {
                //Busca em um campo na página, que é preenchido quando se pesquisa um responsável, o CO_RESP, usado pra instanciar um objeto da entidade do responsável em questão.
                if (string.IsNullOrEmpty(hidCoResp.Value))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um Responsável para dar continuidade no encaminhamento.");
                    return;
                }

                int coRe = int.Parse(hidCoResp.Value);
                tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(coRe);
            }

            #endregion

            //Salva os dados do Usuário em um registro na tb07
            #region Salva o Usuário na TB07

            ////Verifica antes se já existe o paciente algum paciente com o mesmo CPF e NIS informados nos campos, caso não exista, cria um novo
            //string cpfPac = txtCpfPaci.Text.Replace("-", "").Replace(".", "").Trim();
            //var realu = (from tb07li in TB07_ALUNO.RetornaTodosRegistros()
            //             where tb07li.NU_CPF_ALU == cpfPac
            //             select new { tb07li.CO_ALU }).FirstOrDefault();

            //int? paExis = (realu != null ? realu.CO_ALU : (int?)null);

            //Decimal nis = 0;
            //if (!string.IsNullOrEmpty(txtNuNisPaci.Text))
            //{
            //    nis = decimal.Parse(txtNuNisPaci.Text.Trim());
            //}

            //var realu2 = (from tb07ob in TB07_ALUNO.RetornaTodosRegistros()
            //              where tb07ob.NU_NIS == nis
            //              select new { tb07ob.CO_ALU }).FirstOrDefault();

            //int? paExisNis = (realu2 != null ? realu2.CO_ALU : (int?)null);

            TB07_ALUNO tb07;
            //if ((!paExis.HasValue) && (!paExisNis.HasValue))
            //{
            if (string.IsNullOrEmpty(hidCoPac.Value))
            {
                tb07 = new TB07_ALUNO();

                #region Bloco foto
                int codImagem = upImagemAluno.GravaImagem();
                tb07.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(codImagem);
                #endregion

                tb07.NO_ALU = txtnompac.Text;
                tb07.NU_CPF_ALU = txtCPFMOD.Text.Replace(".", "").Replace("-", "").Trim();
                tb07.NU_NIS = decimal.Parse(txtNuNisPaci.Text);
                tb07.DT_NASC_ALU = DateTime.Parse(txtDtNascPaci.Text);
                tb07.CO_SEXO_ALU = ddlSexoPaci.SelectedValue;
                tb07.NU_TELE_CELU_ALU = txtTelCelPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb07.NU_TELE_RESI_ALU = txtTelResPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb07.NU_TELE_WHATS_ALU = txtNuWhatsResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb07.CO_GRAU_PAREN_RESP = ddlGrParen.SelectedValue;
                tb07.NO_EMAIL_PAI = txtEmailPaci.Text;
                tb07.CO_EMP = LoginAuxili.CO_EMP;
                tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                tb07.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                tb07.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(tb108.CO_RESP);

                if (chkPaciMoraCoResp.Checked)
                {
                    tb07.CO_CEP_ALU = txtCEP.Text;
                    tb07.CO_ESTA_ALU = ddlUF.SelectedValue;
                    tb07.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairro.SelectedValue));
                    tb07.DE_ENDE_ALU = txtLograEndResp.Text;
                }

                //Salva os valores para os campos not null da tabela de Usuário
                tb07.CO_SITU_ALU = "A";
                tb07.TP_DEF = "N";

                #region trata para criação do nire

                var res = (from tb07pesq in TB07_ALUNO.RetornaTodosRegistros()
                           select new { tb07pesq.NU_NIRE }).OrderByDescending(w => w.NU_NIRE).FirstOrDefault();

                int nir = 0;
                if (res == null)
                {
                    nir = 1;
                }
                else
                {
                    nir = res.NU_NIRE;
                }

                int nirTot = nir + 1;

                #endregion
                tb07.NU_NIRE = nirTot;

                tb07 = TB07_ALUNO.SaveOrUpdate(tb07);
            }
            else
            {
                //if (string.IsNullOrEmpty(hidCoPac.Value))
                //{
                //    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um Paciente para dar continuidade no encaminhamento.");
                //    return;
                //}

                //Busca em um campo na página, que é preenchido quando se pesquisa um Paciente, o CO_ALU, usado pra instanciar um objeto da entidade do Paciente em questão.
                int coPac = int.Parse(hidCoPac.Value);
                tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == coPac).FirstOrDefault();
            }

            divResp.Visible = false;
            divSuccessoMessage.Visible = true;
            lblMsg.Text = "Usuário salvo com êxito!";
            lblMsgAviso.Text = "Clique no botão Fechar para voltar a tela inicial.";
            lblMsg.Visible = true;
            lblMsgAviso.Visible = true;

            CarregaPacientes();
            ddlNomeUsu.SelectedValue = tb07.CO_ALU.ToString();
            //updTopo.Update();

            #endregion
        }

        protected void ddlNomeUsu_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
            {
                CarregaGridHistoricoPaciente(int.Parse(ddlNomeUsu.SelectedValue));

                var res = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddlNomeUsu.SelectedValue));
                res.TB250_OPERAReference.Load();
                res.TB251_PLANO_OPERAReference.Load();

                //Carrega a operadora se houver
                CarregaOperadoras(ddlOperadora, "");
                if (res.TB250_OPERA != null)
                    ddlOperadora.SelectedValue = res.TB250_OPERA.ID_OPER.ToString();

                //Carrega o plano se houver
                CarregarPlanosSaude(ddlPlano, ddlOperadora);
                if (res.TB251_PLANO_OPERA != null)
                    ddlPlano.SelectedValue = res.TB251_PLANO_OPERA.ID_PLAN.ToString();

                //Recarrega os procedimentos de acordo com a operadora
                CarregaProcedimentos(ddlProcedimento, ddlOperadora, ddlGrupoPr, ddlSubGrupoPr, null, true);
            }

            //Apenas carrega os horários se já tiver sido selecionado o paciente e o profissional
            if ((!string.IsNullOrEmpty(hidCoColSelec.Value) && (hidCoColSelec.Value != "0") && (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))))
                CarregaGridHorario(int.Parse(hidCoColSelec.Value), int.Parse(ddlNomeUsu.SelectedValue));
        }

        protected void imgPesqProfissionais_OnClick(object sender, EventArgs e)
        {
            CarregaGridProfi();
        }

        protected void imgPesqHistPaciente_OnClick(object sender, EventArgs e)
        {
            CarregaGridHistoricoPaciente(ddlNomeUsu.SelectedValue != "" ? int.Parse(ddlNomeUsu.SelectedValue) : 0);
        }

        protected void ckSelect_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;

            foreach (GridViewRow l in grdProfi.Rows)
            {
                CheckBox chk = ((CheckBox)l.FindControl("ckSelect"));

                if (atual.ClientID != chk.ClientID)
                {
                    chk.Checked = false;
                }
                else
                {
                    if (chk.Checked == true)
                    {
                        string coCol = ((HiddenField)l.Cells[0].FindControl("hidCoCol")).Value;
                        string apelCo = (((TextBox)l.FindControl("txtNmCol")).Text);
                        hidCoColSelec.Value = coCol;
                        lblNomeProfi.Text = apelCo;
                        int coColI = (!string.IsNullOrEmpty(coCol) ? int.Parse(coCol) : 0);

                        //Apenas carrega os horários se já tiver sido selecionado o paciente e o profissional
                        if (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))
                            CarregaGridHorario(coColI, int.Parse(ddlNomeUsu.SelectedValue));
                        else
                        { grdHorario.DataSource = null; grdHorario.DataBind(); }
                    }
                    else
                    {
                        grdHorario.DataSource = null;
                        grdHorario.DataBind();
                        hidCoColSelec.Value = lblNomeProfi.Text = string.Empty;
                    }
                }
            }
        }

        protected void imgPesqGridAgenda_OnClick(object sender, EventArgs e)
        {
            //Apenas carrega os horários se já tiver sido selecionado o paciente e o profissional
            if ((!string.IsNullOrEmpty(hidCoColSelec.Value) && (!string.IsNullOrEmpty(ddlNomeUsu.SelectedValue))))
                CarregaGridHorario(int.Parse(hidCoColSelec.Value), int.Parse(ddlNomeUsu.SelectedValue));
        }

        protected void ddlGrupoPr_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarSubGrupos(ddlSubGrupoPr, ddlGrupoPr);

            //Cada item da grade de horários selecionado
            foreach (GridViewRow i in grdHorario.Rows)
            {
                CheckBox chk = (((CheckBox)i.FindControl("ckSelectHr")));
                if (chk.Checked) // Apenas nos marcados
                {
                    HiddenField hdIdItemPlan = (((HiddenField)i.FindControl("hidIdItemPlane")));

                    //Se não houver item de planejamento, replica o grupo selecionado no master
                    if (string.IsNullOrEmpty(hdIdItemPlan.Value))
                    {
                        //Seleciona o mesmo grupo selecionado no campo de cima
                        DropDownList ddlGrupo = (((DropDownList)i.FindControl("ddlGrupo")));
                        DropDownList ddlSubGrupo = (((DropDownList)i.FindControl("ddlSubGrupo")));
                        ddlGrupo.SelectedValue = ddlGrupoPr.SelectedValue;

                        CarregarSubGrupos(ddlSubGrupo, ddlGrupoPr, false, true, false);
                    }
                }
            }
        }

        protected void ddlSubGrupoPr_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProcedimentos(ddlProcedimento, ddlOperadora, ddlGrupoPr, ddlSubGrupoPr, null, true);

            //Cada item da grade de horários selecionado
            foreach (GridViewRow i in grdHorario.Rows)
            {
                CheckBox chk = (((CheckBox)i.FindControl("ckSelectHr")));
                if (chk.Checked) // Apenas nos marcados
                {
                    HiddenField hdIdItemPlan = (((HiddenField)i.FindControl("hidIdItemPlane")));

                    //Se não houver item de planejamento, replica o subgrupo selecionado no master
                    if (string.IsNullOrEmpty(hdIdItemPlan.Value))
                    {
                        //Seleciona o mesmo subgrupo selecionado no campo de cima
                        DropDownList ddlGrupo = (((DropDownList)i.FindControl("ddlGrupo")));
                        DropDownList ddlSubGrupo = (((DropDownList)i.FindControl("ddlSubGrupo")));
                        DropDownList ddlProced = (((DropDownList)i.FindControl("ddlProcedimento")));
                        ddlSubGrupo.SelectedValue = ddlSubGrupoPr.SelectedValue;

                        CarregaProcedimentos(ddlProced, ddlOperadora, ddlSubGrupo, ddlGrupoPr, null, false);
                    }
                }
            }
        }

        protected void ddlProcedimento_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string nomeProced = "";
            //Coloca o nome do procedimento no campo
            if (!string.IsNullOrEmpty(ddlProcedimento.SelectedValue))
                txtDesProcedimento.Text = nomeProced = (TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProcedimento.SelectedValue)).NM_PROC_MEDI);

            //Cada item da grade de horários selecionado
            foreach (GridViewRow i in grdHorario.Rows)
            {
                CheckBox chk = (((CheckBox)i.FindControl("ckSelectHr")));
                if (chk.Checked) // Apenas nos marcados
                {
                    HiddenField hdIdItemPlan = (((HiddenField)i.FindControl("hidIdItemPlane")));

                    //Se não houver item de planejamento, replica o procedimento selecionado no master
                    if (string.IsNullOrEmpty(hdIdItemPlan.Value))
                    {
                        //Seleciona o mesmo procedimento selecionado no campo de cima
                        DropDownList ddlProced = (((DropDownList)i.FindControl("ddlProcedimento")));
                        TextBox txtDesProced = (((TextBox)i.FindControl("txtDesProced")));
                        ddlProced.SelectedValue = ddlProcedimento.SelectedValue;
                        TextBox txtNrAcao = (TextBox)i.FindControl("txtNuAcao");
                        txtDesProced.Text = nomeProced;

                        //Calcula qual o próximo número para ação
                        #region Nº AÇÃO

                        //Insere no campo do número da ação, o próximo número da ação encontrado
                        if (!string.IsNullOrEmpty(ddlProced.SelectedValue))
                            txtNrAcao.Text = RecuperaUltimoNrAcao(int.Parse(ddlNomeUsu.SelectedValue), int.Parse(ddlProced.SelectedValue)).ToString("00");

                        #endregion
                    }
                }
            }
        }

        protected void txtDeAcao_OnTextChanged(object sender, EventArgs e)
        {
            //Cada item da grade de horários selecionado
            foreach (GridViewRow i in grdHorario.Rows)
            {
                CheckBox chk = (((CheckBox)i.FindControl("ckSelectHr")));
                if (chk.Checked) // Apenas nos marcados
                {
                    //Seleciona o mesmo procedimento selecionado no campo de cima
                    TextBox txtDescrAcao = (((TextBox)i.FindControl("txtDesAcao")));
                    txtDescrAcao.Text = txtDeAcao.Text;
                }
            }
        }

        protected void ddlOperadora_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarPlanosSaude(ddlPlano, ddlOperadora);
            CarregaProcedimentos(ddlProcedimento, ddlOperadora, ddlGrupoPr, ddlSubGrupoPr, null, true);
        }

        protected void chkReplicar_OnCheckedChanged(object sender, EventArgs e)
        {
            //Marca ou desmarca de acordo com o selecionado
            foreach (GridViewRow i in grdHorario.Rows)
            {
                CheckBox chk = (((CheckBox)i.FindControl("ckSelectHr")));
                chk.Checked = chkReplicar.Checked;

                //Instancia os objetos
                DropDownList ddlGrupo = (((DropDownList)i.FindControl("ddlGrupo")));
                DropDownList ddlSubGrupo = (((DropDownList)i.FindControl("ddlSubGrupo")));
                DropDownList ddlProced = (((DropDownList)i.FindControl("ddlProcedimento")));
                TextBox txtDescrAcao = (((TextBox)i.FindControl("txtDesAcao")));
                TextBox txtDesProc = (((TextBox)i.FindControl("txtDesProced")));
                TextBox txtNrAcao = (TextBox)i.FindControl("txtNuAcao");
                HiddenField hdIdItemPlan = (((HiddenField)i.FindControl("hidIdItemPlane")));

                if (chk.Checked) //Se estiver marcando, replica os dados encontrados
                {
                    //Se não houver item de planejamento, carrega de acordo com os masters
                    if (string.IsNullOrEmpty(hdIdItemPlan.Value))
                    {
                        #region Sem planejamento associado

                        ddlGrupo.SelectedValue = ddlGrupoPr.SelectedValue;
                        CarregarSubGrupos(ddlSubGrupo, ddlGrupoPr, false, true, false);
                        ddlSubGrupo.SelectedValue = ddlSubGrupoPr.SelectedValue;
                        CarregaProcedimentos(ddlProced, ddlOperadora, ddlGrupo, ddlSubGrupo, null, false);
                        ddlProced.SelectedValue = ddlProcedimento.SelectedValue;
                        txtDesProc.Text = txtDesProcedimento.Text;
                        txtDescrAcao.Text = txtDeAcao.Text;

                        //Libera os campos
                        ddlGrupo.Enabled = ddlSubGrupo.Enabled = ddlProced.Enabled = txtDescrAcao.Enabled = true;

                        //Insere no campo do número da ação, o próximo número da ação encontrado
                        if (!string.IsNullOrEmpty(ddlProced.SelectedValue))
                            txtNrAcao.Text = RecuperaUltimoNrAcao(int.Parse(ddlNomeUsu.SelectedValue), int.Parse(ddlProced.SelectedValue)).ToString("00");

                        #endregion
                    }
                    else //Se houver item de planejamento, replica e libera apenas a descrição da ação
                    {
                        txtDescrAcao.Text = txtDeAcao.Text;
                        txtDescrAcao.Enabled = true;
                    }
                }
                else //Se estiver desmarcando, limpa os dados
                {
                    //Apenas se não houver item de planejamento, limpa todos os campos
                    if (string.IsNullOrEmpty(hdIdItemPlan.Value))
                        ddlGrupo.SelectedValue = ddlSubGrupo.SelectedValue = ddlProced.SelectedValue
                            = txtDesProc.Text = txtDescrAcao.Text = txtNrAcao.Text = "";

                    //Bloqueia os campos
                    ddlGrupo.Enabled = ddlSubGrupo.Enabled = ddlProced.Enabled = txtDescrAcao.Enabled = false;
                }
            }
        }

        protected void ckSelectHr_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;

            foreach (GridViewRow l in grdHorario.Rows)
            {
                CheckBox chk = ((CheckBox)l.FindControl("ckSelectHr"));

                //Instancia os objetos
                DropDownList ddlGrupo = (((DropDownList)l.FindControl("ddlGrupo")));
                DropDownList ddlSubGrupo = (((DropDownList)l.FindControl("ddlSubGrupo")));
                DropDownList ddlProced = (((DropDownList)l.FindControl("ddlProcedimento")));
                TextBox txtDescrAcao = (((TextBox)l.FindControl("txtDesAcao")));
                TextBox txtNuAc = (((TextBox)l.FindControl("txtNuAcao")));
                TextBox txtDesProc = (((TextBox)l.FindControl("txtDesProced")));
                HiddenField hdIdItemPlan = (((HiddenField)l.FindControl("hidIdItemPlane")));

                if (chk.ClientID == atual.ClientID)
                {
                    //Se estiver marcando, coloca os dados de acordo com os acima
                    if (chk.Checked == true)
                    {
                        //Seleciona/preenche os correspondentes
                        //Se não houver item de planejamento, libera todos os campos e carrega de acordo com os masters
                        if (string.IsNullOrEmpty(hdIdItemPlan.Value))
                        {
                            ddlGrupo.SelectedValue = ddlGrupoPr.SelectedValue;
                            CarregarSubGrupos(ddlSubGrupo, ddlGrupoPr, false, true, false);
                            ddlSubGrupo.SelectedValue = ddlSubGrupoPr.SelectedValue;
                            CarregaProcedimentos(ddlProced, ddlOperadora, ddlGrupo, ddlSubGrupo, null, false);
                            ddlProced.SelectedValue = ddlProcedimento.SelectedValue;
                            txtDesProc.Text = txtDesProcedimento.Text;
                            txtDescrAcao.Text = txtDeAcao.Text;

                            //Libera os campos
                            ddlGrupo.Enabled = ddlSubGrupo.Enabled = ddlProced.Enabled = txtDescrAcao.Enabled = true;
                        }
                        else //Se houver item de planejamento, libera apenas a descrição da ação para edição
                            txtDescrAcao.Enabled = true;
                    }
                    else
                    {
                        //Apenas se não houver item de planejamento, limpa todos os campos
                        if (string.IsNullOrEmpty(hdIdItemPlan.Value))
                            ddlGrupo.SelectedValue = ddlSubGrupo.SelectedValue = ddlProced.SelectedValue
                                = txtDesProc.Text = txtDescrAcao.Text = txtNuAc.Text = "";

                        //Bloqueia os campos
                        ddlGrupo.Enabled = ddlSubGrupo.Enabled = ddlProced.Enabled = txtDescrAcao.Enabled = false;
                    }
                }
            }
        }

        protected void ddlGrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddl, ddlSubGrupo;

            if (grdHorario.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdHorario.Rows)
                {
                    ddl = (DropDownList)linha.FindControl("ddlGrupo");
                    ddlSubGrupo = (DropDownList)linha.FindControl("ddlSubGrupo");

                    //Atribui à session o id da central de regulação clicada para ser usada no popup que será aberto
                    if (ddl.ClientID == atual.ClientID)
                        CarregarSubGrupos(ddlSubGrupo, ddl, false, true, false);
                }
            }
        }

        protected void ddlSubGrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddl;

            if (grdHorario.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdHorario.Rows)
                {
                    ddl = (DropDownList)linha.FindControl("ddlSubGrupo");
                    DropDownList ddlGrupo = (DropDownList)linha.FindControl("ddlGrupo");
                    DropDownList ddlProced = (DropDownList)linha.FindControl("ddlProcedimento");

                    //Atribui à session o id da central de regulação clicada para ser usada no popup que será aberto
                    if (ddl.ClientID == atual.ClientID)
                        CarregaProcedimentos(ddlProced, ddlOperadora, ddlGrupo, ddl, null);
                }
            }
        }

        protected void ddlProcedimentoGr_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddl;

            if (grdHorario.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdHorario.Rows)
                {
                    ddl = (DropDownList)linha.FindControl("ddlProcedimento");
                    DropDownList ddlGrupo = (DropDownList)linha.FindControl("ddlGrupo");
                    DropDownList ddlSubGrupo = (DropDownList)linha.FindControl("ddlSubGrupo");
                    TextBox txtNrAcao = (TextBox)linha.FindControl("txtNuAcao");
                    TextBox txtDesProced = (TextBox)linha.FindControl("txtDesProced");

                    //Atribui à session o id da central de regulação clicada para ser usada no popup que será aberto
                    if (ddl.ClientID == atual.ClientID)
                    {
                        if (!string.IsNullOrEmpty(ddl.SelectedValue))
                        {
                            txtDesProced.Text = (TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddl.SelectedValue)).NM_PROC_MEDI);

                            //Insere no campo do número da ação, o próximo número da ação encontrado
                            txtNrAcao.Text = RecuperaUltimoNrAcao(int.Parse(ddlNomeUsu.SelectedValue), int.Parse(ddl.SelectedValue)).ToString("00");
                        }
                        else
                            txtNrAcao.Text = txtDesProced.Text = ""; // Limpa os dois campos caso esteja deselecionando o procedimento
                    }
                }
            }
        }

        protected void imgExc_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdHorario.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdHorario.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgExc");

                    if (img.ClientID == atual.ClientID)
                    {
                        //Atualiza os dados do item de planejamento para cancelar
                        int idItemPlan = int.Parse(((HiddenField)linha.FindControl("hidIdItemPlane")).Value);
                        string statusItemPlanej = (((HiddenField)linha.FindControl("hidSituaItemPlanej")).Value);
                        var tbs386 = TBS386_ITENS_PLANE_AVALI.RetornaPelaChavePrimaria(idItemPlan);

                        //Altera os dados da situação
                        tbs386.CO_SITUA = (statusItemPlanej == "C" ? "A" : "C"); //Se o status atual for cancelado, alterar para em aberto, se for em aberto, altera para cancelado
                        tbs386.DT_SITUA = DateTime.Now;
                        tbs386.CO_COL_SITUA = LoginAuxili.CO_COL;
                        tbs386.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                        tbs386.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        tbs386.IP_SITUA = Request.UserHostAddress;

                        TBS386_ITENS_PLANE_AVALI.SaveOrUpdate(tbs386, true);

                        AuxiliPagina.EnvioAvisoGeralSistema(this.Page, string.Format("Item de Planejamento de ação nº {0} do procedimento {1} cancelado com êxito!", (((TextBox)linha.FindControl("txtNuAcao")).Text), (((TextBox)linha.FindControl("txtDesProced")).Text)));
                    }
                }
            }
        }

        #endregion
    }
}
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
//25/08/2014| Maxwell Almeida            | Criação da funcionalidade de homologação da Central de Regulação

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Data;
using System.Data.Objects;
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

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3220_CtrlCentralRegulacao._3221_Homologacao
{
    public partial class Cadastro : System.Web.UI.Page
    {
        #region Váriaveis

        string qtdLinhasGrid = "";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CarregaInfosAbas(0);
                CarregaGridAtendimentosPendentes();
            }
        }

        #region Pesquisas

        //======>Responsável por pesquisar na HISTÓRICO DE DIAGNÓSTICOS e preencher os devidos campos
        protected void imgPesqHistDiag_OnClick(object sender, EventArgs e)
        {
            ControlaTabs("HDP", "");

            if (string.IsNullOrEmpty(txtIniDtHistDiag.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a data de Início para o período de pesquisa.");
                return;
            }
            if (string.IsNullOrEmpty(txtFimDtHistDiag.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a data de Término para o período de pesquisa.");
                return;
            }
            if (string.IsNullOrEmpty(hidCoPac.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o paciente para atendimento.");
                return;
            }
            CarregaGridHistoricDiagnostic(int.Parse(hidCoPac.Value));
        }

        //======>Responsável por pesquisar na HISTÓRICO DE SERVIÇOS AMBULATORIAIS e preencher os devidos campos
        protected void imgHistServAmbu_OnClick(object sender, EventArgs e)
        {
            ControlaTabs("HSP", "");

            if (string.IsNullOrEmpty(txtIniPeriHisServAmbu.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a data de Início para o período de pesquisa.");
                return;
            }
            if (string.IsNullOrEmpty(txtFimPeriHisServAmbu.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a data de Término para o período de pesquisa.");
                return;
            }
            if (string.IsNullOrEmpty(hidCoPac.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o paciente para atendimento.");
                return;
            }

            CarregaGridHistoricServAmbu(int.Parse(hidCoPac.Value));
        }

        //======>Responsável por pesquisar na HISTÓRICO DE EXAMES MÉDICOS e preencher os devidos campos
        protected void imgHistExames_OnClick(object sender, EventArgs e)
        {
            ControlaTabs("HEM", "");

            if (string.IsNullOrEmpty(txtIniPeriHistExames.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a data de Início para o período de pesquisa.");
                return;
            }
            if (string.IsNullOrEmpty(txtFimPeriHistExames.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a data de Término para o período de pesquisa.");
                return;
            }
            if (string.IsNullOrEmpty(hidCoPac.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o paciente para atendimento.");
                return;
            }
            CarregaGridHistoricExamesMedicos(int.Parse(hidCoPac.Value));
        }

        //======>Responsável por pesquisar na HISTÓRICO DE CONSULTAS MÉDICAS e preencher os devidos campos
        protected void imgHistConsul_OnClick(object sender, EventArgs e)
        {
            ControlaTabs("HCM", "");

            if (string.IsNullOrEmpty(txtIniPeriHistConsul.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a data de Início para o período de pesquisa.");
                return;
            }
            if (string.IsNullOrEmpty(txtFimPeriHistConsul.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a data de Término para o período de pesquisa.");
                return;
            }
            if (string.IsNullOrEmpty(hidCoPac.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o paciente para atendimento.");
                return;
            }
            CarregaGridHistoricConsultaMedica(int.Parse(hidCoPac.Value));
        }

        //======>Responsável por pesquisar na HISTÓRICO DE RECEITAS MÉDICAS e preencher os devidos campos
        protected void imgPesqHistReceiMedic_OnClick(object sender, EventArgs e)
        {
            ControlaTabs("HRM", "");

            if (string.IsNullOrEmpty(txtIniPeriHistConsul.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a data de Início para o período de pesquisa.");
                return;
            }
            if (string.IsNullOrEmpty(txtFimPeriHistConsul.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a data de Término para o período de pesquisa.");
                return;
            }
            if (string.IsNullOrEmpty(hidCoPac.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o paciente para atendimento.");
                return;
            }
            CarregaGridHistoricReceitaMedica(int.Parse(hidCoPac.Value));
        }

        //======>Responsável por pesquisar na HISTÓRICO DE ATESTADOS MÉDICOS e preencher os devidos campos
        protected void imgHistAtestMedic_OnClick(object sender, EventArgs e)
        {
            ControlaTabs("HAP", "");

            if (string.IsNullOrEmpty(txtIniPeriHistoAtestMedic.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a data de Início para o período de pesquisa.");
                return;
            }
            if (string.IsNullOrEmpty(txtFimPeriHistoAtestMedic.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a data de Término para o período de pesquisa.");
                return;
            }
            if (string.IsNullOrEmpty(hidCoPac.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o paciente para atendimento.");
                return;
            }
            CarregaGridHistoricAtestados(int.Parse(hidCoPac.Value));
        }

        #endregion

        #region Carregamentos

        #region Carregamento de Grides

        /// <summary>
        /// Carrega na grid todos os atendimentos que possuem algum encaminhamento pendente
        /// </summary>
        private void CarregaGridAtendimentosPendentes(string ord = "")
        {
            var res = (from tbs347 in TBS347_CENTR_REGUL.RetornaTodosRegistros()
                       join tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros() on tbs347.TBS219_ATEND_MEDIC.ID_ATEND_MEDIC equals tbs219.ID_ATEND_MEDIC
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs219.CO_ALU equals tb07.CO_ALU
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs219.CO_EMP equals tb25.CO_EMP
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs219.CO_COL equals tb03.CO_COL
                       join tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros() on tbs347.ID_CENTR_REGUL equals tbs350.TBS347_CENTR_REGUL.ID_CENTR_REGUL
                       where tbs350.FL_APROV_ENCAM != "S"
                       select new AtendimentosPendentes
                       {
                           NO_PAC = tb07.NO_ALU,
                           NIS_PAC = tb07.NU_NIS,
                           CO_ALU = tb07.CO_ALU,
                           CO_SEXO = tb07.CO_SEXO_ALU,
                           dt_nascimento = tb07.DT_NASC_ALU,
                           UNID = tb25.sigla,
                           dataAtendMed = tbs219.DT_ATEND_CADAS,
                           CO_ATEND = tbs219.CO_ATEND_MEDIC,
                           ID_ATEND_MEDIC = tbs347.TBS219_ATEND_MEDIC.ID_ATEND_MEDIC,
                           ID_CENTR_REGUL = tbs347.ID_CENTR_REGUL,
                           FL_USO = tbs347.FL_USO,

                           //Medico
                           NO_COL_RECEB = tb03.NO_COL,
                           CO_MATRIC_COL = tb03.CO_MAT_COL,
                           SIGLA_ENT = tb03.CO_SIGLA_ENTID_PROFI,
                           NUMER_ENT = tb03.NU_ENTID_PROFI,
                           UF_ENT = tb03.CO_UF_ENTID_PROFI,
                           DT_ENT = tb03.DT_EMISS_ENTID_PROFI,

                       }).DistinctBy(w => w.ID_ATEND_MEDIC).OrderByDescending(w => w.dataAtendMed).ToList();

            //Ordena de acordo com parâmetro
            switch (ord)
            {
                case "P":
                    res = res.OrderBy(w => w.NO_PAC).OrderBy(w => w.CO_TIPO_RISCO).ToList();
                    break;

                case "U":
                    res = res.OrderBy(w => w.UNID).OrderBy(w => w.CO_TIPO_RISCO).ToList();
                    break;

                default:
                    res = res.OrderBy(w => w.CO_TIPO_RISCO).ToList();
                    break;
            }

            grdAtendimPendentes.DataSource = res;
            grdAtendimPendentes.DataBind();
        }

        /// <summary>
        /// Método responsável por carregar a grid de detalhe de pendências
        /// </summary>
        private void CarregaGridItensPendentes(int ID_CENTR_REGUL, string co_sigla, string ord = "")
        {
            var res = (from tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                       join tbs347 in TBS347_CENTR_REGUL.RetornaTodosRegistros() on tbs350.TBS347_CENTR_REGUL.ID_CENTR_REGUL equals tbs347.ID_CENTR_REGUL
                       where tbs350.TBS347_CENTR_REGUL.ID_CENTR_REGUL == ID_CENTR_REGUL
                       && (co_sigla != "0" ? tbs350.CO_SIGLA_ITEM_ENCAM == co_sigla : 0 == 0)
                       select new DetalhePendencia
                       {
                           DT_ALTER = tbs350.DT_ALTER_ENCAM ?? tbs350.DT_SOLIC_ENCAM,
                           CO_SIGLA = tbs350.CO_SIGLA_ITEM_ENCAM,
                           ID_ATEND_MEDIC = tbs347.TBS219_ATEND_MEDIC.ID_ATEND_MEDIC,
                           ID_ITEM = tbs350.ID_ITEM_ENCAM,
                           ID_CENTR_REGUL = tbs350.TBS347_CENTR_REGUL.ID_CENTR_REGUL,
                           NO_ITEM = tbs350.NO_ITEM,
                           CO_PROTOCOLO = tbs350.NU_REGIS_ITEM,
                           ID_ITEM_CENTR_REGUL = tbs350.ID_ITEM_CENTR_REGUL,
                           CO_ATEND_MEDIC = tbs347.CO_ATEND_MEDIC,
                       }).OrderByDescending(W => W.DT_ALTER).ThenByDescending(w => w.DT_ALTER).ToList();

            //Ordena de acordo com parâmetro
            switch (ord)
            {
                case "R":
                    res = res.OrderBy(w => w.CO_PROTOCOLO).ToList();
                    break;

                case "U":
                    res = res.OrderBy(w => w.CO_CAD_ITEM).ToList();
                    break;

                default:
                    res = res.OrderBy(w => w.NO_ITEM).ToList();
                    break;
            }

            grdDetalhePendencia.DataSource = res;
            grdDetalhePendencia.DataBind();

            liItensPendentes.Visible = true;
        }

        /// <summary>
        /// Carrega a grid de histórico de diagnósticos do paciente de acordo com o código recebido como parâmetro
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void CarregaGridHistoricDiagnostic(int CO_ALU)
        {
            int cid = ddlCIDHistDiag.SelectedValue != "" ? int.Parse(ddlCIDHistDiag.SelectedValue) : 0;
            int unid = ddlUnidHistDiag.SelectedValue != "" ? int.Parse(ddlUnidHistDiag.SelectedValue) : 0;
            DateTime dtIni = DateTime.Parse(txtIniDtHistDiag.Text);
            DateTime dtFim = DateTime.Parse(txtFimDtHistDiag.Text);

            var res = (from tbs334 in TBS334_DIAGN_ATEND_MEDIC.RetornaTodosRegistros()
                       join tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros() on tbs334.TBS219_ATEND_MEDIC.ID_ATEND_MEDIC equals tbs219.ID_ATEND_MEDIC
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs334.CO_COL_MEDIC equals tb03.CO_COL
                       join tb117 in TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros() on tbs334.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID equals tb117.IDE_CID
                       where tbs219.CO_ALU == CO_ALU
                       && (cid != 0 ? tbs334.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID == cid : 0 == 0)
                       && (unid != 0 ? tbs334.TB25_EMPRESA.CO_EMP == unid : 0 == 0)
                           //&&   ((tbs334.DT_DIAGN >= dtIni) && (tbs334.DT_DIAGN <= dtFim))
                           //&& (((tbs334.DT_DIAGN > dtIni) || (tbs334.DT_DIAGN == dtIni)) && ((tbs334.DT_DIAGN < dtFim) || (tbs334.DT_DIAGN == dtFim)))
                       && ((EntityFunctions.TruncateTime(tbs334.DT_DIAGN) >= EntityFunctions.TruncateTime(dtIni)) && (EntityFunctions.TruncateTime(tbs334.DT_DIAGN) <= EntityFunctions.TruncateTime(dtFim)))

                       select new HistoricoDiagnosticos
                       {
                           DIAG = tbs334.DE_DIAGN,
                           CO_CID = tb117.CO_CID,
                           DE_CID = tb117.NO_CID,
                           dt = tbs334.DT_DIAGN,
                           CO_REGIS = tbs334.NU_REGIS_DIAGN,

                           NO_COL_RECEB = tb03.NO_COL,
                           CO_MATRIC_COL = tb03.CO_MAT_COL,
                           SIGLA_ENT = tb03.CO_SIGLA_ENTID_PROFI,
                           NUMER_ENT = tb03.NU_ENTID_PROFI,
                           UF_ENT = tb03.CO_UF_ENTID_PROFI,
                           DT_ENT = tb03.DT_EMISS_ENTID_PROFI,
                       }).OrderByDescending(w => w.dt).ThenBy(w => w.DE_CID).ToList();

            grdHistDiag.DataSource = res;
            grdHistDiag.DataBind();
        }

        /// <summary>
        /// Carrega a grid de histórico de Serviços Ambulatoriais do paciente de acordo com o código recebido como parâmetro
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void CarregaGridHistoricServAmbu(int CO_ALU)
        {
            DateTime dtIni = DateTime.Parse(txtIniPeriHisServAmbu.Text);
            DateTime dtFim = DateTime.Parse(txtFimPeriHisServAmbu.Text);
            int unidade = (!string.IsNullOrEmpty(ddlUnidHistServAmbu.SelectedValue) ? int.Parse(ddlUnidHistServAmbu.SelectedValue) : 0);
            int servico = (!string.IsNullOrEmpty(ddlTpServAmbuHistorico.SelectedValue) ? int.Parse(ddlTpServAmbuHistorico.SelectedValue) : 0);
            string tpServ = ddlHistoTipoServ.SelectedValue;
            var res = (from tbs332 in TBS332_ATEND_SERV_AMBUL.RetornaTodosRegistros()
                       join tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros() on tbs332.ID_ATEND_MEDIC equals tbs219.ID_ATEND_MEDIC
                       join tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs332.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE equals tbs356.ID_PROC_MEDI_PROCE
                       join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs332.CO_DEPTO equals tb14.CO_DEPTO
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs332.TB25_EMPRESA.CO_EMP equals tb25.CO_EMP
                       where tbs219.CO_ALU == CO_ALU
                       && (unidade != 0 ? tbs332.TB25_EMPRESA.CO_EMP == unidade : 0 == 0)
                       && (servico != 0 ? tbs332.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == servico : 0 == 0)
                       && (tpServ != "0" ? tbs332.TP_APLIC == tpServ : 0 == 0)
                           //&& (((tbs332.DT_SERVI_AMBUL > dtIni) || (tbs332.DT_SERVI_AMBUL == dtIni)) && ((tbs332.DT_SERVI_AMBUL < dtFim) || (tbs332.DT_SERVI_AMBUL == dtFim)))
                       && ((EntityFunctions.TruncateTime(tbs332.DT_SERVI_AMBUL) >= EntityFunctions.TruncateTime(dtIni)) && (EntityFunctions.TruncateTime(tbs332.DT_SERVI_AMBUL) <= EntityFunctions.TruncateTime(dtFim)))
                       select new RequiServicoEmbula
                       {
                           ID_ATEND_SERV_AMBUL = tbs332.ID_ATEND_SERV_AMBUL,
                           aplicacao = tbs332.TP_APLIC,
                           tipo = tbs332.TP_SERVI,
                           servico = tbs356.NM_PROC_MEDI,
                           dt = tbs332.DT_SERVI_AMBUL,
                           NO_DEPTO = tb14.NO_DEPTO,
                           NO_EMP = tb25.NO_FANTAS_EMP,
                           CO_REGIS = tbs332.NU_REGIS_SERVI_AMBUL,
                       }).OrderByDescending(w => w.dt).ThenBy(w => w.servico).ToList();

            grdHistServAmbu.DataSource = res;
            grdHistServAmbu.DataBind();
        }

        /// <summary>
        /// Carrega o histórico de atestados médicos relacionados à um determinado paciente recebido como parâmetro
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void CarregaGridHistoricAtestados(int CO_ALU)
        {
            DateTime dtIni = DateTime.Parse(txtIniPeriHisServAmbu.Text);
            DateTime dtFim = DateTime.Parse(txtFimPeriHisServAmbu.Text);
            int unidade = (!string.IsNullOrEmpty(ddlUnidHistAtestMedic.SelectedValue) ? int.Parse(ddlUnidHistAtestMedic.SelectedValue) : 0);
            int tipo = (!string.IsNullOrEmpty(ddlTipoAtestMedic.SelectedValue) ? int.Parse(ddlTipoAtestMedic.SelectedValue) : 0);
            int cid = (!string.IsNullOrEmpty(ddlCIDHistAtestados.SelectedValue) ? int.Parse(ddlCIDHistAtestados.SelectedValue) : 0);

            var res = (from tbs333 in TBS333_ATEST_MEDIC_PACIE.RetornaTodosRegistros()
                       join tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros() on tbs333.CO_ALU equals tbs219.CO_ALU
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs333.CO_EMP equals tb25.CO_EMP
                       join tb009 in TB009_RTF_DOCTOS.RetornaTodosRegistros() on tbs333.ID_DOCUM equals tb009.ID_DOCUM
                       join tb117 in TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros() on tbs333.IDE_CID equals tb117.IDE_CID
                       where tbs219.CO_ALU == CO_ALU
                       && (unidade != 0 ? tbs333.CO_EMP == unidade : 0 == 0)
                       && (tipo != 0 ? tbs333.ID_DOCUM == tipo : 0 == 0)
                       && (cid != 0 ? tbs333.IDE_CID == cid : 0 == 0)
                       && (((tbs333.DT_ATEST_MEDIC > dtIni) || (tbs333.DT_ATEST_MEDIC == dtIni)) && ((tbs333.DT_ATEST_MEDIC < dtFim) || (tbs333.DT_ATEST_MEDIC == dtFim)))
                       select new RegistroAtestMedic
                       {
                           ATESTADO = tb009.NM_DOCUM,
                           CID = tb117.NO_CID,
                           DATA_receb = tbs333.DT_ATEST_MEDIC,
                           DIASREP = tbs333.QT_DIAS,
                           UNIDADE = tb25.NO_FANTAS_EMP,
                       }).OrderByDescending(w => w.DATA_receb).ThenBy(w => w.ATESTADO).ToList();

            grdHistAtestados.DataSource = res;
            grdHistAtestados.DataBind();
        }

        /// <summary>
        /// Carrega o histórico de Exames Médicos relacionados à um determinado paciente recebido como parâmetro
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void CarregaGridHistoricExamesMedicos(int CO_ALU)
        {
            DateTime dtIni = DateTime.Parse(txtIniPeriHistExames.Text);
            DateTime dtFim = DateTime.Parse(txtFimPeriHistExames.Text);
            int coEmp = ddlUnidHistExames.SelectedValue != "" ? int.Parse(ddlUnidHistExames.SelectedValue) : 0;
            int coDept = ddlLocalHistExames.SelectedValue != "" ? int.Parse(ddlLocalHistExames.SelectedValue) : 0;
            int coExame = ddlExamHistExames.SelectedValue != "" ? int.Parse(ddlExamHistExames.SelectedValue) : 0;

            var res = (from tbs218 in TBS218_EXAME_MEDICO.RetornaTodosRegistros()
                       join tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros() on tbs218.ID_ATEND_MEDIC equals tbs219.ID_ATEND_MEDIC
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs218.TB25_EMPRESA.CO_EMP equals tb25.CO_EMP
                       join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs218.TB14_DEPTO.CO_DEPTO equals tb14.CO_DEPTO
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs218.CO_COL_MEDIC equals tb03.CO_COL
                       where tbs219.CO_ALU == CO_ALU
                       && (coEmp != 0 ? tbs218.TB25_EMPRESA.CO_EMP == coEmp : 0 == 0)
                       && (coDept != 0 ? tbs218.TB14_DEPTO.CO_DEPTO == coDept : 0 == 0)
                       && (coExame != 0 ? tbs218.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == coExame : 0 == 0)
                       && tbs218.TBS356_PROC_MEDIC_PROCE.CO_TIPO_PROC_MEDI == "EX"
                           //&& (((tbs218.DT_EXAME > dtIni) || (tbs218.DT_EXAME == dtIni)) && ((tbs218.DT_EXAME < dtFim) || (tbs218.DT_EXAME == dtFim)))
                       && ((EntityFunctions.TruncateTime(tbs218.DT_EXAME) >= EntityFunctions.TruncateTime(dtIni)) && (EntityFunctions.TruncateTime(tbs218.DT_EXAME) <= EntityFunctions.TruncateTime(dtFim)))
                       select new RequiExamesMedicos
                       {
                           exame = tbs218.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                           unidade = tb25.NO_FANTAS_EMP,
                           local = tb14.NO_DEPTO,
                           dt = tbs218.DT_EXAME,
                           NU_REGIS = tbs218.NU_REGIS_EXAME,

                           NO_COL_RECEB = tb03.NO_COL,
                           CO_MATRIC_COL = tb03.CO_MAT_COL,
                           SIGLA_ENT = tb03.CO_SIGLA_ENTID_PROFI,
                           NUMER_ENT = tb03.NU_ENTID_PROFI,
                           UF_ENT = tb03.CO_UF_ENTID_PROFI,
                           DT_ENT = tb03.DT_EMISS_ENTID_PROFI,
                       }).OrderByDescending(w => w.dt).ToList();

            grdHistExames.DataSource = res;
            grdHistExames.DataBind();
        }

        /// <summary>
        /// Carrega o histórico de Exames Médicos relacionados à um determinado paciente recebido como parâmetro
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void CarregaGridHistoricConsultaMedica(int CO_ALU)
        {
            DateTime dtIni = DateTime.Parse(txtIniPeriHisServAmbu.Text);
            DateTime dtFim = DateTime.Parse(txtFimPeriHisServAmbu.Text);
            int coEmp = ddlUnidHistConsultas.SelectedValue != "" ? int.Parse(ddlUnidHistConsultas.SelectedValue) : 0;
            int coEspec = ddlEspecHistConsultas.SelectedValue != "" ? int.Parse(ddlEspecHistConsultas.SelectedValue) : 0;
            int coDept = ddlDeptHistConsultas.SelectedValue != "" ? int.Parse(ddlDeptHistConsultas.SelectedValue) : 0;
            string tpConsul = ddlConsuHistTipo.SelectedValue;
            string situa = ddlSituHistConsultas.SelectedValue;

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                       join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs174.CO_DEPT equals tb14.CO_DEPTO
                       join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tbs174.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                       where tbs174.CO_ALU == CO_ALU
                       && (coEmp != 0 ? tbs174.CO_EMP == coEmp : 0 == 0)
                       && (coEspec != 0 ? tbs174.CO_ESPEC == coEspec : 0 == 0)
                       && (coDept != 0 ? tbs174.CO_DEPT == coDept : 0 == 0)
                       && (tpConsul != "0" ? tbs174.TP_CONSU == tpConsul : 0 == 0)
                       && (situa != "0" ? tbs174.CO_SITUA_AGEND_HORAR == situa : 0 == 0)
                       && (((tbs174.DT_AGEND_HORAR > dtIni) || (tbs174.DT_AGEND_HORAR == dtIni)) && ((tbs174.DT_AGEND_HORAR < dtFim) || (tbs174.DT_AGEND_HORAR == dtFim)))
                       select new HistoricoConsultas
                       {
                           CO_MATRIC_COL = tb03.CO_MAT_COL,
                           NO_COL_RECEB = tb03.NO_COL,
                           DEPTO = tb14.NO_DEPTO,
                           dt = tbs174.DT_AGEND_HORAR,
                           hr = tbs174.HR_AGEND_HORAR,
                           NO_ESPEC = tb63.NO_ESPECIALIDADE,
                           TP_CONSUL = tbs174.TP_CONSU,
                           CO_SITU = tbs174.CO_SITUA_AGEND_HORAR,
                       }).OrderByDescending(w => w.dt).ToList();

            grdHistConsul.DataSource = res;
            grdHistConsul.DataBind();
        }

        /// <summary>
        /// Carrega o histórico de Receitas Médicas relacionadas à um determinado paciente recebido como parâmetro
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void CarregaGridHistoricReceitaMedica(int CO_ALU)
        {
            int coEmp = ddlUnidHistReceitMedic.SelectedValue != "" ? int.Parse(ddlUnidHistReceitMedic.SelectedValue) : 0;
            int coProd = ddlProduHistReceitMedic.SelectedValue != "" ? int.Parse(ddlProduHistReceitMedic.SelectedValue) : 0;
            DateTime dtIni = DateTime.Parse(txtIniPeriHistReceiMedic.Text);
            DateTime dtFim = DateTime.Parse(txtFimPeriHistReceiMedic.Text);

            var res = (from tbs330 in TBS330_RECEI_ATEND_MEDIC.RetornaTodosRegistros()
                       join tb90 in TB90_PRODUTO.RetornaTodosRegistros() on tbs330.CO_MEDIC equals tb90.CO_PROD
                       join tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros() on tbs330.ID_ATEND_MEDIC equals tbs219.ID_ATEND_MEDIC
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs330.CO_COL_MEDIC equals tb03.CO_COL
                       where tbs219.CO_ALU == CO_ALU
                       && (coEmp != 0 ? tbs330.TB25_EMPRESA1.CO_EMP == coEmp : 0 == 0)
                       && (coProd != 0 ? tbs330.CO_MEDIC == coProd : 0 == 0)
                       && (((tbs330.DT_RECEI > dtIni) || (tbs330.DT_RECEI == dtIni)) && ((tbs330.DT_RECEI < dtFim) || (tbs330.DT_RECEI == dtFim)))
                       select new Receituario
                       {
                           medicamento = tb90.NO_PROD,
                           qtd = tbs330.QT_MEDIC,
                           uso = tbs330.QT_USO,
                           prescricao = tbs330.DE_PRESC,
                           NO_COL_RECEB = tb03.NO_COL,
                           CO_MATRIC_COL = tb03.CO_MAT_COL,
                           dt = tbs330.DT_RECEI,
                       }).OrderByDescending(w => w.dt).ToList();

            grdHistReceiMedic.DataSource = res;
            grdHistReceiMedic.DataBind();
        }

        // Classes de Saída que auxiliam no carregamento das informações nas grides
        #region Classes de Saída

        public class AtendimentosPendentes
        {
            //Dados do(a) Paciente
            public string NO_PAC { get; set; }
            public decimal? NIS_PAC { get; set; }
            public int CO_ALU { get; set; }
            public DateTime? dt_nascimento { get; set; }
            public string NU_IDADE
            {
                get
                {
                    string idade = " - ";

                    //Calcula a idade do Paciente de acordo com a data de nascimento do mesmo.
                    if (this.dt_nascimento.HasValue)
                    {
                        int anos = DateTime.Now.Year - dt_nascimento.Value.Year;

                        if (DateTime.Now.Month < dt_nascimento.Value.Month || (DateTime.Now.Month == dt_nascimento.Value.Month && DateTime.Now.Day < dt_nascimento.Value.Day))
                            anos--;

                        idade = anos.ToString();
                    }
                    return idade;
                }
            }
            public string CO_SEXO { get; set; }
            public string NO_PAC_V
            {
                get
                {
                    //Coleta apenas o primeiro nome
                    string[] nome = NO_PAC.Split(' ');
                    string nomeV = nome[0];
                    return (this.NIS_PAC.HasValue ? this.NIS_PAC.ToString().PadLeft(7, '0') + " " + nomeV : nomeV);
                }
            }
            public int ID_CENTR_REGUL { get; set; }
            public string FL_USO { get; set; }

            //Dados Gerais
            public string UNID { get; set; }
            public int ID_ATEND_MEDIC { get; set; }
            public int CO_TIPO_RISCO
            {
                get
                {
                    var res = (from tb219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros()
                               where tb219.ID_ATEND_MEDIC == this.ID_ATEND_MEDIC
                               select tb219).FirstOrDefault();

                    res.TBS194_PRE_ATENDReference.Load();
                    return res.TBS194_PRE_ATEND.CO_TIPO_RISCO;
                }
            }
            public string CLASS_RISCO
            {
                get
                {
                    string s = "";
                    switch (this.CO_TIPO_RISCO)
                    {
                        case 1:
                            s = "EMERGÊNCIA";
                            break;

                        case 2:
                            s = "MUITO URGENTE";
                            break;

                        case 3:
                            s = "URGENTE";
                            break;

                        case 4:
                            s = "POUCO URGENTE";
                            break;

                        case 5:
                            s = "NÃO URGENTE";
                            break;
                    }

                    return s;
                }
            }

            //Trata data e hora do Atendimento
            public DateTime? dataAtendMed { get; set; }
            public string dataEMValid
            {
                get
                {
                    return this.dataAtendMed.Value.ToString("dd/MM/yy");
                }
            }
            public string horaEMValid
            {
                get
                {
                    return this.dataAtendMed.Value.ToString("HH:mm");
                }
            }
            public string DT_ATEND
            {
                get
                {
                    return this.dataEMValid + " " + this.horaEMValid;
                }
            }

            //Trata as cores de acordo com a classificação de risco
            public bool DIV_1
            {
                get
                {
                    return (this.CO_TIPO_RISCO == 1 ? true : false);
                }
            }
            public bool DIV_2
            {
                get
                {
                    return (this.CO_TIPO_RISCO == 2 ? true : false);
                }
            }
            public bool DIV_3
            {
                get
                {
                    return (this.CO_TIPO_RISCO == 3 ? true : false);
                }
            }
            public bool DIV_4
            {
                get
                {
                    return (this.CO_TIPO_RISCO == 4 ? true : false);
                }
            }
            public bool DIV_5
            {
                get
                {
                    return (this.CO_TIPO_RISCO == 5 ? true : false);
                }
            }

            public string CO_ATEND { get; set; }
            public string CO_ATEND_V
            {
                get
                {
                    return this.CO_ATEND.Insert(2, ".").Insert(6, ".").Insert(9, ".");
                }
            }

            //Dados do Médico(a)
            public string SIGLA_ENT { get; set; }
            public string NUMER_ENT { get; set; }
            public string UF_ENT { get; set; }
            public DateTime? DT_ENT { get; set; }
            public string ENT_CONCAT
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.SIGLA_ENT) ? this.SIGLA_ENT + " " + this.NUMER_ENT + " - " + this.UF_ENT : "");
                }
            }
            public string CO_MATRIC_COL { get; set; }
            public string NO_COL_RECEB { get; set; }
            public string NO_COL
            {
                get
                {
                    //Trata o tamanho e apresentação do nome do médico
                    string nomeCol = (this.NO_COL_RECEB.Length > 25 ? this.NO_COL_RECEB.Substring(0, 25) + "..." : this.NO_COL_RECEB);
                    //Coleta apenas o primeiro nome
                    string[] nome = nomeCol.Split(' ');
                    string nomeV = nome[0];

                    //Concatena a matrícula e o nome do colaborador responsável pelo diagnóstico
                    return (!string.IsNullOrEmpty(this.ENT_CONCAT) ? this.ENT_CONCAT + " - " + nomeV : (!string.IsNullOrEmpty(this.CO_MATRIC_COL) ? this.CO_MATRIC_COL.Insert(5, "-").Insert(2, ".") + " - " + nomeV : nomeV));
                }
            }

            //Tratacomo mostrar os status

            //Exames
            public bool SW_NAO_PRETO_EX
            {
                get
                {
                    //Verifica se existe qualquer exame do atendimento selecionado em aberto. Caso não exista nenhum retorna TRUE para mostrar NÃO em "preto", caso exista algum, retorna FALSE para ocultar o "NÃO".
                    return (!TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros().Where(w => w.TBS347_CENTR_REGUL.ID_CENTR_REGUL == this.ID_CENTR_REGUL && w.CO_SIGLA_ITEM_ENCAM == "EX").Any());
                }
            }
            public bool SW_SIM_PRETO_EX
            {
                get
                {
                    //Faz uma lista de todos os itens de exame para o atendimento em questão na central de regulação
                    var res = (from tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                               where tbs350.TBS347_CENTR_REGUL.ID_CENTR_REGUL == this.ID_CENTR_REGUL && tbs350.CO_SIGLA_ITEM_ENCAM == "EX"
                               select new { tbs350.ID_ITEM_ENCAM, tbs350.FL_APROV_ENCAM }).Distinct().ToList();

                    int a = 0;
                    foreach (var li in res)
                    {

                        //Conta, dos lançamentos mais recentes de cada exame, quantos existem de cada tipo.
                        switch (li.FL_APROV_ENCAM)
                        {
                            case "A":
                                a++;
                                break;
                        }
                    }

                    //Testa, se está para mostrar NÃO em preto, a conta feita neste bloco de código não será válida
                    if (SW_NAO_PRETO_EX)
                        return false;
                    //Caso todos os itens que existem estejam em análise, mostra o SIM preto.
                    else
                        return (a == res.Count ? true : false);
                }
            }
            public bool SW_SIM_AZUL_EX
            {
                get
                {
                    //Faz uma lista de todos os itens de exame para o atendimento em questão na central de regulação
                    var res = (from tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                               where tbs350.TBS347_CENTR_REGUL.ID_CENTR_REGUL == this.ID_CENTR_REGUL && tbs350.CO_SIGLA_ITEM_ENCAM == "EX"
                               select new { tbs350.ID_ITEM_ENCAM, tbs350.FL_APROV_ENCAM }).Distinct().ToList();

                    int a = 0;
                    foreach (var li in res)
                    {

                        //Conta, dos lançamentos mais recentes de cada exame, quantos existem de cada tipo.
                        switch (li.FL_APROV_ENCAM)
                        {
                            case "A":
                                a++;
                                break;
                        }
                    }

                    //Testa, se está para mostrar NÃO em preto, a conta feita neste bloco de código não será válida
                    if (SW_NAO_PRETO_EX)
                        return false;
                    //Caso todos os itens que existem tenham sido Analisados(seja autorizado ou não), mostra o SIM Azul.
                    else
                        return (a == 0 ? true : false);
                }
            }
            public bool SW_SIM_VERMELHO_EX
            {
                get
                {
                    //Faz uma lista de todos os itens de exame para o atendimento em questão na central de regulação
                    var res = (from tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                               where tbs350.TBS347_CENTR_REGUL.ID_CENTR_REGUL == this.ID_CENTR_REGUL && tbs350.CO_SIGLA_ITEM_ENCAM == "EX"
                               select new { tbs350.ID_ITEM_ENCAM, tbs350.FL_APROV_ENCAM }).Distinct().ToList();

                    int a = 0;
                    foreach (var li in res)
                    {

                        //Conta, dos lançamentos mais recentes de cada exame, quantos existem de cada tipo.
                        switch (li.FL_APROV_ENCAM)
                        {
                            case "A":
                                a++;
                                break;
                        }
                    }

                    //Caso alguns itens(não todos) ainda estejam pendentes de aprovação, mostra SIM vermelho
                    return (a > 0 && a != res.Count ? true : false);
                }
            }

            //Serviços Ambulatoriais
            public bool SW_NAO_PRETO_SA
            {
                get
                {
                    //Verifica se existe qualquer exame do atendimento selecionado em aberto. Caso não exista nenhum retorna TRUE para mostrar NÃO em "preto", caso exista algum, retorna FALSE para ocultar o "NÃO".
                    return (!TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros().Where(w => w.TBS347_CENTR_REGUL.ID_CENTR_REGUL == this.ID_CENTR_REGUL && w.CO_SIGLA_ITEM_ENCAM == "SA").Any());
                }
            }
            public bool SW_SIM_PRETO_SA
            {
                get
                {
                    //Faz uma lista de todos os itens de exame para o atendimento em questão na central de regulação
                    var res = (from tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                               where tbs350.TBS347_CENTR_REGUL.ID_CENTR_REGUL == this.ID_CENTR_REGUL && tbs350.CO_SIGLA_ITEM_ENCAM == "SA"
                               select new { tbs350.ID_ITEM_ENCAM, tbs350.FL_APROV_ENCAM }).Distinct().ToList();

                    int a = 0;
                    foreach (var li in res)
                    {

                        //Conta, dos lançamentos mais recentes de cada exame, quantos existem de cada tipo.
                        switch (li.FL_APROV_ENCAM)
                        {
                            case "A":
                                a++;
                                break;
                        }
                    }

                    //Testa, se está para mostrar NÃO em preto, a conta feita neste bloco de código não será válida
                    if (SW_NAO_PRETO_SA)
                        return false;
                    //Caso todos os itens que existem estejam em análise, mostra o SIM preto.
                    else
                        return (a == res.Count ? true : false);
                }
            }
            public bool SW_SIM_AZUL_SA
            {
                get
                {
                    //Faz uma lista de todos os itens de exame para o atendimento em questão na central de regulação
                    var res = (from tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                               where tbs350.TBS347_CENTR_REGUL.ID_CENTR_REGUL == this.ID_CENTR_REGUL && tbs350.CO_SIGLA_ITEM_ENCAM == "SA"
                               select new { tbs350.ID_ITEM_ENCAM, tbs350.FL_APROV_ENCAM }).Distinct().ToList();

                    int a = 0;
                    foreach (var li in res)
                    {

                        //Conta, dos lançamentos mais recentes de cada exame, quantos existem de cada tipo.
                        switch (li.FL_APROV_ENCAM)
                        {
                            case "A":
                                a++;
                                break;
                        }
                    }

                    //Testa, se está para mostrar NÃO em preto, a conta feita neste bloco de código não será válida
                    if (SW_NAO_PRETO_SA)
                        return false;
                    //Caso todos os itens que existem tenham sido Analisados(seja autorizado ou não), mostra o SIM Azul.
                    else
                        return (a == 0 ? true : false);
                }
            }
            public bool SW_SIM_VERMELHO_SA
            {
                get
                {
                    //Faz uma lista de todos os itens de exame para o atendimento em questão na central de regulação
                    var res = (from tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                               where tbs350.TBS347_CENTR_REGUL.ID_CENTR_REGUL == this.ID_CENTR_REGUL && tbs350.CO_SIGLA_ITEM_ENCAM == "SA"
                               select new { tbs350.ID_ITEM_ENCAM, tbs350.FL_APROV_ENCAM }).Distinct().ToList();

                    int a = 0;
                    foreach (var li in res)
                    {

                        //Conta, dos lançamentos mais recentes de cada exame, quantos existem de cada tipo.
                        switch (li.FL_APROV_ENCAM)
                        {
                            case "A":
                                a++;
                                break;
                        }
                    }

                    //Caso alguns itens(não todos) ainda estejam pendentes de aprovação, mostra SIM vermelho
                    return (a > 0 && a != res.Count ? true : false);
                }
            }
        }

        public class DetalhePendencia
        {
            public string CO_ATEND_MEDIC { get; set; }
            public int ID_ITEM_CENTR_REGUL { get; set; }
            public string CO_SIGLA { get; set; }
            public int ID_ATEND_MEDIC { get; set; }
            public int ID_ITEM { get; set; }
            public string CO_PROTOCOLO { get; set; }
            public string CO_CAD_ITEM
            {
                get
                {
                    switch (this.CO_SIGLA)
                    {
                        case "EX":
                            var resex = (from tbs218 in TBS218_EXAME_MEDICO.RetornaTodosRegistros()
                                         where tbs218.ID_EXAME == this.ID_ITEM
                                         select tbs218).FirstOrDefault();

                            resex.TBS356_PROC_MEDIC_PROCEReference.Load();
                            return resex.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI;

                        case "SA":
                            var ressa = (from tbs332 in TBS332_ATEND_SERV_AMBUL.RetornaTodosRegistros()
                                         where tbs332.ID_ATEND_SERV_AMBUL == this.ID_ITEM
                                         select tbs332).FirstOrDefault();

                            ressa.TBS356_PROC_MEDIC_PROCEReference.Load();
                            return ressa.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI;

                        default:
                            return " - ";
                    }
                }
            }
            public string NO_ITEM { get; set; }
            public string CO_STATUS
            {
                get
                {
                    //Retorna o status mais recente lançado para o item em questão
                    var res = (from tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                               where tbs350.ID_ITEM_ENCAM == this.ID_ITEM
                               select new { tbs350.FL_APROV_ENCAM }).FirstOrDefault();

                    return res.FL_APROV_ENCAM;
                }
            }
            public string NO_TIPO_ITEM
            {
                get
                {
                    string s = "";
                    switch (this.CO_SIGLA)
                    {
                        case "EX":
                            s = "EXA";
                            break;
                        case "SA":
                            s = "SRA";
                            break;
                        case "RM":
                            s = "RME";
                            break;
                        default:
                            s = " - ";
                            break;
                    }
                    return s;
                }
            }
            public int ID_CENTR_REGUL { get; set; }

            public string DT_ALTER_V
            {
                get
                {
                    return this.DT_ALTER.ToString("dd/MM/yy") + " " + this.DT_ALTER.ToString("HH:mm") + " / ";
                }
            }
            public DateTime DT_ALTER { get; set; }

            //Trata o status que será apresentado
            public bool SW_NAO_VERMELHO
            {
                get
                {
                    return (this.CO_STATUS == "N" ? true : false);
                }
            }
            public bool SW_SIM_VERDE
            {
                get
                {
                    return (this.CO_STATUS == "S" ? true : false);
                }
            }
            public bool SW_ANALISE_AZUL
            {
                get
                {
                    return (this.CO_STATUS == "A" ? true : false);
                }
            }
            public bool SW_PENDENTE_ABOBORA
            {
                get
                {
                    return (this.CO_STATUS == "P" ? true : false);
                }
            }
        }

        public class Receituario
        {
            public string medicamento { get; set; }
            public int? qtd { get; set; }
            public int? uso { get; set; }
            public string prescricao { get; set; }
            public int ID_RECEI { get; set; }
            public int CO_PROD { get; set; }
            public DateTime? dt { get; set; }
            public string DATA
            {
                get
                {
                    return (this.dt.HasValue ? this.dt.Value.ToString("dd/MM/yyy") : "");
                }
            }
            public string CO_MATRIC_COL { get; set; }
            public string NO_COL_RECEB { get; set; }
            public string NO_COL
            {
                get
                {
                    //Trata o tamanho e apresentação do nome do médico
                    string nomeCol = (this.NO_COL_RECEB.Length > 25 ? this.NO_COL_RECEB.Substring(0, 25) + "..." : this.NO_COL_RECEB);

                    //Concatena a matrícula e o nome do colaborador responsável pelo diagnóstico
                    return (!string.IsNullOrEmpty(this.CO_MATRIC_COL) ? this.CO_MATRIC_COL.Insert(5, "-").Insert(2, ".") + " - " + nomeCol : nomeCol);
                }
            }
            public string FP { get; set; }
            public bool FP_V
            {
                get
                {
                    return (this.FP == "S" ? true : false);
                }
            }
            public bool HABIL_SELEC
            {
                get
                {
                    return (this.FP == "S" ? decimal.Parse(this.ESTOQUE) > decimal.Parse("0") : true ? false : false);
                }
            }
            public string ESTOQUE
            {
                get
                {
                    var varTb96 = TB96_ESTOQUE.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, this.CO_PROD);

                    if (varTb96 != null)
                        return varTb96.QT_SALDO_EST.ToString();
                    else
                        return "0";
                }
            }
        }

        public class RequiExamesMedicos
        {
            public string exame { get; set; }
            public string unidade { get; set; }
            public string local { get; set; }
            public int ID_EXAME { get; set; }
            public DateTime? dt { get; set; }
            public string DT_V
            {
                get
                {
                    return (this.dt.HasValue ? this.dt.Value.ToString("dd/MM/yy") : " - ");
                }
            }
            public string NU_REGIS { get; set; }
            public string CO_REGIS_V
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.NU_REGIS) ? this.NU_REGIS.Insert(4, ".").Insert(8, ".") : " - ");
                }
            }

            public string SIGLA_ENT { get; set; }
            public string NUMER_ENT { get; set; }
            public string UF_ENT { get; set; }
            public DateTime? DT_ENT { get; set; }
            public string ENT_CONCAT
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.SIGLA_ENT) ? this.SIGLA_ENT + " " + this.NUMER_ENT + " - " + this.UF_ENT : "");
                }
            }
            public string CO_MATRIC_COL { get; set; }
            public string NO_COL_RECEB { get; set; }
            public string NO_COL
            {
                get
                {
                    //Trata o tamanho e apresentação do nome do médico
                    string nomeCol = (this.NO_COL_RECEB.Length > 25 ? this.NO_COL_RECEB.Substring(0, 25) + "..." : this.NO_COL_RECEB);
                    //Coleta apenas o primeiro nome
                    string[] nome = nomeCol.Split(' ');
                    string nomeV = nome[0];

                    //Concatena a matrícula e o nome do colaborador responsável pelo diagnóstico
                    return (!string.IsNullOrEmpty(this.ENT_CONCAT) ? this.ENT_CONCAT + " - " + nomeV : (!string.IsNullOrEmpty(this.CO_MATRIC_COL) ? this.CO_MATRIC_COL.Insert(5, "-").Insert(2, ".") + " - " + nomeV : nomeV));
                }
            }
        }

        public class RequiServicoEmbula
        {
            public string NO_EMP { get; set; }
            public string NO_DEPTO { get; set; }

            public string servico { get; set; }
            public int ID_ATEND_SERV_AMBUL { get; set; }
            public DateTime? dt { get; set; }
            public string dt_valid
            {
                get
                {
                    return (this.dt.HasValue ? this.dt.Value.ToString("dd/MM/yy") : "");
                }
            }
            public string tipo { get; set; }
            public string tipoValid
            {
                get
                {
                    string aux = "";
                    switch (this.tipo)
                    {
                        case "M":
                            aux = "Medicação";
                            break;
                        case "A":
                            aux = "Acompanhamento";
                            break;
                        case "C":
                            aux = "Curativo";
                            break;
                        case "O":
                            aux = "Outras";
                            break;
                        default:
                            aux = "Outras";
                            break;
                    }
                    return aux;
                }
            }
            public string aplicacao { get; set; }
            public string aplicacaoValid
            {
                get
                {
                    string aux = "";
                    switch (this.aplicacao)
                    {
                        case "N":
                            aux = "Nenhum";
                            break;
                        case "O":
                            aux = "Via Oral";
                            break;
                        case "I":
                            aux = "Via Intravenosa";
                            break;
                        default:
                            aux = "Nenhum";
                            break;
                    }
                    return aux;
                }
            }
            public string CO_REGIS { get; set; }
            public string CO_REGIS_V
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.CO_REGIS) ? this.CO_REGIS.Insert(4, ".").Insert(8, ".") : " - ");
                }
            }
        }

        public class RegistroAtestMedic
        {
            public string ATESTADO { get; set; }
            public int ID_ATESTADO { get; set; }
            public string UNIDADE { get; set; }
            public string DATA
            {
                get
                {
                    return (this.DATA_receb.HasValue ? this.DATA_receb.Value.ToString("dd/MM/yyyy") : "");
                }
            }
            public DateTime? DATA_receb { get; set; }
            public int? DIASREP { get; set; }
            public string CID { get; set; }
        }

        public class HistoricoDiagnosticos
        {
            public DateTime dt { get; set; }
            public string dt_valid
            {
                get
                {
                    return this.dt.ToString("dd/MM/yy");
                }
            }
            public string CO_MATRIC_COL { get; set; }
            public string NO_COL_RECEB { get; set; }
            public string NO_COL
            {
                get
                {
                    //Trata o tamanho e apresentação do nome do médico
                    string nomeCol = (this.NO_COL_RECEB.Length > 25 ? this.NO_COL_RECEB.Substring(0, 25) + "..." : this.NO_COL_RECEB);
                    //Coleta apenas o primeiro nome
                    string[] nome = nomeCol.Split(' ');
                    string nomeV = nome[0];

                    //Concatena a matrícula e o nome do colaborador responsável pelo diagnóstico
                    return (!string.IsNullOrEmpty(this.ENT_CONCAT) ? this.ENT_CONCAT + " - " + nomeV : (!string.IsNullOrEmpty(this.CO_MATRIC_COL) ? this.CO_MATRIC_COL.Insert(5, "-").Insert(2, ".") + " - " + nomeV : nomeV));
                }
            }
            public string DIAG { get; set; }
            public string CO_CID { get; set; }
            public string DE_CID { get; set; }
            public string CID
            {
                get
                {
                    return this.CO_CID + " - " + this.DE_CID;
                }
            }
            public string CO_REGIS { get; set; }
            public string CO_REGIS_V
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.CO_REGIS) ? this.CO_REGIS.Insert(4, ".").Insert(8, ".") : " - ");
                }
            }

            public string SIGLA_ENT { get; set; }
            public string NUMER_ENT { get; set; }
            public string UF_ENT { get; set; }
            public DateTime? DT_ENT { get; set; }
            public string ENT_CONCAT
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.SIGLA_ENT) ? this.SIGLA_ENT + " " + this.NUMER_ENT + " - " + this.UF_ENT : "");
                }
            }
        }

        public class HistoricoConsultas
        {
            public DateTime dt { get; set; }
            public string hr { get; set; }
            public string hora
            {
                get
                {
                    return this.dt.ToString("dd/MM/yy") + " - " + this.hr;
                }
            }
            public string NO_ESPEC { get; set; }
            public string DEPTO { get; set; }
            public string CO_MATRIC_COL { get; set; }
            public string NO_COL_RECEB { get; set; }
            public string NO_COL
            {
                get
                {
                    //Trata o tamanho e apresentação do nome do médico
                    string nomeCol = (this.NO_COL_RECEB.Length > 25 ? this.NO_COL_RECEB.Substring(0, 25) + "..." : this.NO_COL_RECEB);

                    //Concatena a matrícula e o nome do colaborador responsável pelo diagnóstico
                    return (!string.IsNullOrEmpty(this.CO_MATRIC_COL) ? this.CO_MATRIC_COL.Insert(5, "-").Insert(2, ".") + " - " + nomeCol : nomeCol);
                }
            }
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
        }

        #endregion

        #endregion

        /// <summary>
        /// Responsável por carregar as informações em todas as abas do atendimento
        /// </summary>
        private void CarregaInfosAbas(int CO_ALU)
        {

            //====================> Carregamento dos campos na aba de HISTÓRICO DE DIAGNÓSTICOS já com informações pré-definidas e um período inicial de 3 meses podendo ser alterado conforme necessidade
            AuxiliCarregamentos.CarregaUnidade(ddlUnidHistDiag, LoginAuxili.ORG_CODIGO_ORGAO, true, false);
            AuxiliCarregamentos.CarregaCID(ddlCIDHistDiag, true, true);
            txtIniDtHistDiag.Text = DateTime.Now.AddMonths(-3).ToString();
            txtFimDtHistDiag.Text = DateTime.Now.ToString();
            CarregaGridHistoricDiagnostic(CO_ALU);

            //====================> Carregamento dos campos na aba de HISTÓRICO DE SERVIÇOS AMBULATORIAIS já com informações pré-definidas e um período inicial de 3 meses podendo ser alterado conforme necessidade
            AuxiliCarregamentos.CarregaUnidade(ddlUnidHistServAmbu, LoginAuxili.ORG_CODIGO_ORGAO, true, false);
            AuxiliCarregamentos.CarregaServicosAmbulatoriais(ddlTpServAmbuHistorico, true);
            txtIniPeriHisServAmbu.Text = DateTime.Now.AddMonths(-3).ToString();
            txtFimPeriHisServAmbu.Text = DateTime.Now.ToString();
            CarregaGridHistoricServAmbu(CO_ALU);

            //====================> Carregamento dos campos na aba de HISTÓRICO DE ATESTADOS MÉDICOS já com informações pré-definidas e um período inicial de 3 meses podendo ser alterado conforme necessidade
            AuxiliCarregamentos.CarregaUnidade(ddlUnidHistAtestMedic, LoginAuxili.ORG_CODIGO_ORGAO, true, false);
            CarregaTiposAtestados(ddlTipoAtestMedic);
            AuxiliCarregamentos.CarregaCID(ddlCIDHistAtestados, true, true);
            txtIniPeriHistoAtestMedic.Text = DateTime.Now.AddMonths(-3).ToString();
            txtFimPeriHistoAtestMedic.Text = DateTime.Now.ToString();
            CarregaGridHistoricAtestados(CO_ALU);

            //=====================> Carregamento dos campos na aba de HISTÓRICO DE EXAMES já com informações pré-definidas e um período inicial de 3 meses podendo ser alterado conforme necessidade
            AuxiliCarregamentos.CarregaUnidade(ddlUnidHistExames, LoginAuxili.ORG_CODIGO_ORGAO, true, false);
            AuxiliCarregamentos.CarregaDepartamentos(ddlLocalHistExames, LoginAuxili.CO_EMP, true);
            AuxiliCarregamentos.CarregaExamesMedicos(ddlExamHistExames, true);
            txtIniPeriHistExames.Text = DateTime.Now.AddMonths(-3).ToString();
            txtFimPeriHistExames.Text = DateTime.Now.ToString();
            CarregaGridHistoricExamesMedicos(CO_ALU);

            //=====================> Carregamento dos campos na aba de HISTÓRICO DE CONSULTAS MÉDICAS já com informações pré-definidas e um período inicial de 3 meses podendo ser alterado conforme necessidade
            AuxiliCarregamentos.CarregaUnidade(ddlUnidHistConsultas, LoginAuxili.ORG_CODIGO_ORGAO, true, false);
            AuxiliCarregamentos.CarregaEspeciacialidades(ddlEspecHistConsultas, LoginAuxili.CO_EMP, null, true);
            AuxiliCarregamentos.CarregaDepartamentos(ddlDeptHistConsultas, LoginAuxili.CO_EMP, true);
            txtIniPeriHistConsul.Text = DateTime.Now.AddMonths(-3).ToString();
            txtFimPeriHistConsul.Text = DateTime.Now.ToString();
            CarregaGridHistoricConsultaMedica(CO_ALU);

            //=====================> Carregamento dos campos na aba de HISTÓRICO DE RECEITAS MÉDICAS já com informações pré-definidas e um período inicial de 3 meses podendo ser alterado conforme necessidade
            AuxiliCarregamentos.CarregaUnidade(ddlUnidHistReceitMedic, LoginAuxili.ORG_CODIGO_ORGAO, true, false);
            AuxiliCarregamentos.CarregaProdutos(ddlProduHistReceitMedic, LoginAuxili.CO_EMP, true);
            txtIniPeriHistReceiMedic.Text = DateTime.Now.AddMonths(-3).ToString();
            txtFimPeriHistReceiMedic.Text = DateTime.Now.ToString();
            CarregaGridHistoricReceitaMedica(CO_ALU);
        }

        /// <summary>
        /// Responsável por carregar os tipos de atestados médicos (Acompanhamento, afastamento, etc.)
        /// </summary>
        private void CarregaTiposAtestados(DropDownList ddl)
        {
            var res = (from tb009 in TB009_RTF_DOCTOS.RetornaTodosRegistros()
                       where tb009.TP_DOCUM == "AM"
                       select new { tb009.ID_DOCUM, tb009.NM_DOCUM }).ToList();

            ddl.DataTextField = "NM_DOCUM";
            ddl.DataValueField = "ID_DOCUM";
            ddl.DataSource = res;
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que controla visibilidade das Tabs da tela de matrícula
        /// </summary>
        /// <param name="tab">Nome da tab</param>
        protected void ControlaTabs(string tab, string tabOrigem)
        {
            tabDiagHist.Style.Add("display", "none");
            tabServAmbuHist.Style.Add("display", "none");
            tabAtestMedcHist.Style.Add("display", "none");
            tabExamHist.Style.Add("display", "none");
            tabConsHist.Style.Add("display", "none");
            tabhistReceiMedic.Style.Add("display", "none");

            //===========================================> Históricos do Paciente <================================================
            if (tab == "HDP")
                tabDiagHist.Style.Add("display", "block");
            else if (tab == "HSP")
                tabServAmbuHist.Style.Add("display", "block");
            else if (tab == "HAP")
                tabAtestMedcHist.Style.Add("display", "block");
            else if (tab == "HEM")
                tabExamHist.Style.Add("display", "block");
            else if (tab == "HCM")
                tabConsHist.Style.Add("display", "block");
            else if (tab == "HRM")
                tabhistReceiMedic.Style.Add("display", "block");

            updMenuLateral.Update();
            updDiagHist.Update();
            updServAmbuHist.Update();
            updAtestMedcHist.Update();
            updExamHist.Update();
            updConsHist.Update();
            updhistReceiMedic.Update();
        }

        /// <summary>
        /// Altera o atributo "enable" das opções do histórico do paciente
        /// </summary>
        /// <param name="acao"></param>
        private void CarregaChecksHistoricos(bool acao)
        {
            chkDiagPaci.Enabled = chkConsPaci.Enabled = chkMedicPaci.Enabled = chkHistReceiMedic.Enabled =
                chkExmPaci.Enabled = chkServAmbPaci.Enabled = chkAtestMedcPaci.Enabled
                = chkImgAtendPaci.Enabled = acao;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método responsável por percorrer a grid de itens pendentes e realizar as devidas alterações
        /// </summary>
        private void SalvaAlterItensPendentes()
        {
            foreach (GridViewRow li in grdDetalhePendencia.Rows)
            {
                int idCentralRegu = int.Parse(((HiddenField)li.Cells[5].FindControl("hidIdCentrRegu")).Value);
                int idItemCentrRegul = int.Parse(((HiddenField)li.Cells[5].FindControl("hidIdItemCentrRegul")).Value);
                string flAprov = (((DropDownList)li.Cells[5].FindControl("ddlAprovacao")).SelectedValue);
                string flSituAtual = (((HiddenField)li.Cells[5].FindControl("hidCoStatus")).Value);
                string observ = (((TextBox)li.Cells[5].FindControl("txtObser")).Text);

                ///Persiste o objeto e seus atributos apenas se a situação tiver sido alterada da original
                if (flAprov != flSituAtual)
                {
                    var dadosCol = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
                    dadosCol.TB25_EMPRESAReference.Load();

                    //Persiste os dados de alteração no log
                    TBS348_LOG_CENTR_REGUL tbs348 = new TBS348_LOG_CENTR_REGUL();
                    tbs348.TBS350_ITEM_CENTR_REGUL = TBS350_ITEM_CENTR_REGUL.RetornaPelaChavePrimaria(idItemCentrRegul);
                    tbs348.TBS347_CENTR_REGUL = TBS347_CENTR_REGUL.RetornaPelaChavePrimaria(idCentralRegu);
                    tbs348.FL_APROV_ENCAM = flAprov;
                    tbs348.CO_EMP_MEDICO_ENC = dadosCol.TB25_EMPRESA.CO_EMP;
                    tbs348.CO_COL_MEDICO_ENC = dadosCol.CO_COL;
                    tbs348.DT_ALTER_ENCAM = DateTime.Now;
                    tbs348.HR_ALTER_ENCAM = DateTime.Now.ToString("HH:mm");
                    tbs348.IP_ALTER_ENCAM = Request.UserHostAddress;
                    tbs348.DE_OBSER = observ;
                    TBS348_LOG_CENTR_REGUL.SaveOrUpdate(tbs348, true);

                    //Persiste os dados de alteração no registro da central de regulação
                    TBS350_ITEM_CENTR_REGUL tbs350 = TBS350_ITEM_CENTR_REGUL.RetornaPelaChavePrimaria(idItemCentrRegul);
                    tbs350.FL_APROV_ENCAM = flAprov;
                    tbs350.CO_EMP_MEDICO_ENC = dadosCol.TB25_EMPRESA.CO_EMP;
                    tbs350.CO_COL_MEDICO_ENC = dadosCol.CO_COL;
                    tbs350.DT_ALTER_ENCAM = DateTime.Now;
                    tbs350.HR_ALTER_ENCAM = DateTime.Now.ToString("HH:mm");
                    tbs350.IP_ALTER_ENCAM = Request.UserHostAddress;
                    tbs350.DE_OBSER = observ;

                    //Gera o código de aprovação caso tenha sido aprovado
                    #region Código Aprovação

                    if (flAprov == "S")
                    {
                        var res = (from tbs350pesq in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                                   where tbs350pesq.NU_APROV != null
                                   select new { tbs350pesq.NU_APROV }).OrderByDescending(w => w.NU_APROV).FirstOrDefault();

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
                            seq = res.NU_APROV.Substring(2, 7);
                            seq2 = int.Parse(seq);
                        }

                        seqConcat = seq2 + 1;
                        seqcon = seqConcat.ToString().PadLeft(7, '0');

                        tbs350.NU_APROV = "AP" + seqcon;
                    }

                    #endregion

                    TBS350_ITEM_CENTR_REGUL.SaveOrUpdate(tbs350, true);
                }
            }

            CarregaGridAtendimentosPendentes();
            CarregaGridItensPendentes(0, "");
        }

        /// <summary>
        /// Carrega os tipos de itens existentes de acordo com o atendimento selecionado
        /// </summary>
        private void CarregaTiposItensExistentes(int ID_CENTR_REGUL)
        {
            ddlTipoItem.Items.Clear();

            bool temExame = TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros().Where(w => w.TBS347_CENTR_REGUL.ID_CENTR_REGUL == ID_CENTR_REGUL && w.CO_SIGLA_ITEM_ENCAM == "EX").Any();
            bool temSerAmbul = TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros().Where(w => w.TBS347_CENTR_REGUL.ID_CENTR_REGUL == ID_CENTR_REGUL && w.CO_SIGLA_ITEM_ENCAM == "SA").Any();

            if (temSerAmbul)
                ddlTipoItem.Items.Insert(0, new ListItem("Serviços Ambulatoriais", "SA"));

            if (temExame)
                ddlTipoItem.Items.Insert(0, new ListItem("Exame", "EX"));

            ddlTipoItem.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Carrega todos os tipos de itens possíveis para filtro dos itens pendentes
        /// </summary>
        private void CarregaTiposItensGeral()
        {
            ddlTipoItem.Items.Clear();
            ddlTipoItem.Items.Insert(0, new ListItem("Encaminhamentos Internação", "EI"));
            ddlTipoItem.Items.Insert(0, new ListItem("Encaminhamentos Médicos", "EM"));
            ddlTipoItem.Items.Insert(0, new ListItem("Reserva de Medicamentos", "RM"));
            ddlTipoItem.Items.Insert(0, new ListItem("Serviços Ambulatoriais", "SA"));
            ddlTipoItem.Items.Insert(0, new ListItem("Exame", "EX"));
            ddlTipoItem.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Devido ao método de reload na grid de Atendimentos Pendentes, ela perde a seleção do checkbox, este método coleta 
        /// </summary>
        private void selecionaGridAtendPendentes()
        {
            CheckBox chk;
            string idCentrRegu;
            // Valida se a grid de atividades possui algum registro
            if (grdAtendimPendentes.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdAtendimPendentes.Rows)
                {
                    idCentrRegu = ((HiddenField)linha.Cells[0].FindControl("hidCoCentrRegul")).Value;
                    int coate = (int)HttpContext.Current.Session["VL_Atend_AP"];

                    if (int.Parse(idCentrRegu) == coate)
                    {
                        chk = (CheckBox)linha.Cells[0].FindControl("chkSelecGridDetalhada");
                        chk.Checked = true;
                    }
                }
            }
        }

        /// <summary>
        /// Método responsável por verificar qual registro está em uso e bloquear o acesso
        /// </summary>
        private void VerificaRegistroUso()
        {
            foreach (GridViewRow li in grdAtendimPendentes.Rows)
            {
                if (((HiddenField)li.Cells[0].FindControl("hidFlUso")).Value == "S")
                {
                    string idCentr = (((HiddenField)li.Cells[0].FindControl("hidCoCentrRegul")).Value);

                    if (idCentr != hidItemSelec.Value)
                    {
                        CheckBox ck = (((CheckBox)li.Cells[0].FindControl("chkSelecGridDetalhada")));
                        ck.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// Executa método javascript que mostra a Modal para registro de ocorrência
        /// </summary>
        private void AbreModalCliente()
        {
            ScriptManager.RegisterStartupScript(
                updModal,
                this.GetType(),
                "Acao",
                "AbreModal();",
                true
            );

            //ScriptManager.RegisterClientScriptBlock(updModal, this.GetType(), "Acao", "function AbreModal() {"
            //+ "$('#divLoadRegisOcorr').dialog({ autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: 'REGISTRO DE OCORRÊNCIA - CENTRAL DE REGULAÇÃO',"
            //    + "open: function () { $('#divLoadRegisOcorr').show(); }"
            //    + "});"
            //    + "}",true);
        }

        /// <summary>
        /// Limpa os campos do registro de ocorrência
        /// </summary>
        private void LimpaCamposOcorrencia()
        {
            txtNuAtendMod.Text = txtdtAtenMod.Text = txtHrAtenMod.Text = txtClasRiscMod.Text = txtCRMMedSoliMod.Text
                = txtNomMedSoliMod.Text = txtSglUnidSoliMod.Text = txtLocalMedicSoliMod.Text = txtNisPaciMod.Text = txtNoPacMod.Text
                = txtSexPacMod.Text = txtIdaPacMod.Text = txtRegisItemMod.Text = ddlTipoItemMod.SelectedValue =
                txtNomeItemMod.Text = txtDeOcorrMod.Text = txtCRMMedAnaMod.Text = txtNomMedAnaMod.Text = txtUnidAnaMod.Text
                = txtRegistrante.Text = txtUniRegisMod.Text = txtDataOcoMod.Text = txtHrOcoMod.Text = "";
        }

        //[System.Web.Services.WebMethod()]
        //public void ArmazenaDescOcorrencia(string de)
        //{
        //    qtdLinhasGrid = de;
        //    HttpContext.Current.Session.Add("desc_ocorr_AP", de);
        //}

        #endregion

        #region Funções de Campo

        protected void ddlUnidHistExames_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ControlaTabs("HEM", "");
            if (ddlUnidHistExames.SelectedValue != "")
            {
                AuxiliCarregamentos.CarregaDepartamentos(ddlUnidHistExames, int.Parse(ddlUnidHistExames.SelectedValue), true);
            }
        }

        protected void ddlUnidHistConsultas_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUnidHistConsultas.SelectedValue != "")
            {
                ControlaTabs("HCM", "");
                AuxiliCarregamentos.CarregaEspeciacialidades(ddlEspecHistConsultas, int.Parse(ddlUnidHistConsultas.SelectedValue), null, true);
                AuxiliCarregamentos.CarregaDepartamentos(ddlDeptHistConsultas, int.Parse(ddlUnidHistConsultas.SelectedValue), true);
            }
        }

        protected void ddlUnidHistReceitMedic_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUnidHistReceitMedic.SelectedValue != "")
            {
                ControlaTabs("HRM", "");
                AuxiliCarregamentos.CarregaProdutos(ddlProduHistReceitMedic, int.Parse(ddlUnidHistReceitMedic.SelectedValue), true);
            }
        }

        protected void chkSelecGridDetalhada_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            // Valida se a grid de atividades possui algum registro
            if (grdAtendimPendentes.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdAtendimPendentes.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("chkSelecGridDetalhada");
                    HiddenField hidSele = ((HiddenField)linha.Cells[0].FindControl("hidLocalSelecionado"));
                    int idCentrRegul = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoCentrRegul")).Value);

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID != atual.ClientID)
                    {
                        bool sele = false;
                        if (chk.Checked)
                            sele = true;

                        chk.Checked = false;

                        //Só libera o item se for um item que estava selecionado anteriormente
                        if (sele)
                        {
                            TBS347_CENTR_REGUL tb347 = TBS347_CENTR_REGUL.RetornaPelaChavePrimaria(idCentrRegul);
                            tb347.FL_USO = "N";
                            TBS347_CENTR_REGUL.SaveOrUpdate(tb347, true);
                        }
                    }
                    else
                    {
                        if (chk.Checked)
                        {
                            int idCentrRegu = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoCentrRegul")).Value);
                            int coAlu = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoAlu")).Value);
                            //string idCentRegu = ((HiddenField)linha.Cells[0].FindControl("hidCoCentrRegul")).Value;

                            CarregaGridItensPendentes(idCentrRegu, ddlTipoItem.SelectedValue);

                            //Bloqueia o registro
                            hidItemSelec.Value = idCentrRegul.ToString();

                            //Salva no banco que o registro em questão está em uso
                            TBS347_CENTR_REGUL tb347 = TBS347_CENTR_REGUL.RetornaPelaChavePrimaria(idCentrRegul);
                            tb347.FL_USO = "S";
                            TBS347_CENTR_REGUL.SaveOrUpdate(tb347, true);

                            //Resgata informações do paciente para usá-las posteriormente
                            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                       where tb07.CO_ALU == coAlu
                                       select new { tb07.NO_ALU }).FirstOrDefault();

                            //Atribui valores à variáveis que serão usadas posteriormente
                            hidCoPac.Value = coAlu.ToString();
                            hidIdCentrRegul.Value = idCentrRegu.ToString();
                            txtPaciente.Text = res.NO_ALU;

                            //Carrega as informações existentes DO HISTÓRICO do paciente
                            CarregaInfosAbas(coAlu);
                            CarregaChecksHistoricos(true);
                            updMenuLateral.Update();

                            //Guarda a FLAG para saber se o Checkbox está sendo clicado para marcar ou desmarcar, neste caso, ele grava como para marcar.
                            HttpContext.Current.Session.Add("FL_Select_Grid_AP", "S");

                            //Guarda o Valor do Pré-Atendimento, para fins de posteriormente comparar este valor 
                            HttpContext.Current.Session.Add("VL_Atend_AP", idCentrRegu);

                            CarregaTiposItensExistentes(idCentrRegu);
                            updItensPendentes.Update();
                        }
                        else
                        {
                            CarregaChecksHistoricos(false);
                            hidCoPac.Value = txtPaciente.Text = hidIdCentrRegul.Value = "";
                            CarregaTiposItensGeral();

                            HttpContext.Current.Session.Remove("VL_Atend_AP");
                            HttpContext.Current.Session.Add("FL_Select_Grid_AP", "N");

                            //Libera o registro
                            hidItemSelec.Value = "";

                            //Salva no banco que o registro não está em uso
                            TBS347_CENTR_REGUL tb347 = TBS347_CENTR_REGUL.RetornaPelaChavePrimaria(idCentrRegul);
                            tb347.FL_USO = "N";
                            TBS347_CENTR_REGUL.SaveOrUpdate(tb347, true);

                            CarregaGridItensPendentes(0, "");

                            updItensPendentes.Update();

                            //Se o usuário está clicando para desmarcar o registro, não faz sentido redirecioná-lo para outra aba
                            ControlaTabs("SLP", "SLC");
                        }
                    }
                }
            }
        }

        protected void ddlTipoItem_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridItensPendentes((!string.IsNullOrEmpty(hidIdCentrRegul.Value) ? int.Parse(hidIdCentrRegul.Value) : 0), ddlTipoItem.SelectedValue, ddlOrdDetalhePendencia.SelectedValue);
            updItensPendentes.Update();
        }

        protected void ddlOrdeAtendPend_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridAtendimentosPendentes(ddlOrdeAtendPend.SelectedValue);
        }

        protected void ddlOrdDetalhePendencia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridItensPendentes((!string.IsNullOrEmpty(hidIdCentrRegul.Value) ? int.Parse(hidIdCentrRegul.Value) : 0), ddlTipoItem.SelectedValue, ddlOrdDetalhePendencia.SelectedValue);
        }

        protected void grdAtendimPendentes_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.DataItem != null)
            //{
            //    if (((HiddenField)e.Row.Cells[0].FindControl("hidFlUso")).Value == "S")
            //    {
            //        string idCentr = (((HiddenField)e.Row.Cells[0].FindControl("hidCoCentrRegul")).Value);

            //        if (idCentr != hidItemSelec.Value)
            //        {
            //            CheckBox ck = (((CheckBox)e.Row.Cells[0].FindControl("chkSelecGridDetalhada")));
            //            ck.Enabled = false;

            //            e.Row.Attributes.Add("onMouseOver", "this.style.cursor='default';");
            //            e.Row.BackColor = System.Drawing.Color.WhiteSmoke;
            //        }
            //    }
            //}
        }

        protected void grdDetalhePendencia_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                DropDownList ddl = (((DropDownList)e.Row.Cells[5].FindControl("ddlAprovacao")));
                string flSituAtual = (((HiddenField)e.Row.Cells[5].FindControl("hidCoStatus")).Value);

                ddl.SelectedValue = flSituAtual;
            }
        }

        protected void lnkFinaGrid_OnClick(object sender, EventArgs e)
        {
            SalvaAlterItensPendentes();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            CarregaGridAtendimentosPendentes();
            //int idCentrRegul = (!string.IsNullOrEmpty(hidIdCentrRegul.Value) ? int.Parse(hidIdCentrRegul.Value) : 0);
            //CarregaGridItensPendentes(idCentrRegul, ddlTipoItem.SelectedValue);

            //Verifica se a grid já possuia um registro selecionado antes, e chama um método responsável por selecioná-lo de novo no PRÉ-ATENDIMENTO
            if ((string)HttpContext.Current.Session["FL_Select_Grid_AP"] == "S")
            {
                selecionaGridAtendPendentes();
            }

            updAtendPenden.Update();
            //updItensPendentes.Update();
        }

        protected void imgReg_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;

            if (grdDetalhePendencia.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdDetalhePendencia.Rows)
                {
                    img = (ImageButton)linha.Cells[7].FindControl("imgReg");
                    int idCentrRegul = int.Parse(((HiddenField)linha.Cells[5].FindControl("hidIdCentrRegu")).Value);
                    int idItemCentrlRegu = int.Parse(((HiddenField)linha.Cells[5].FindControl("hidIdItemCentrRegul")).Value);
                    string coAtend = (((HiddenField)linha.Cells[5].FindControl("hidCoAtend")).Value);

                    //Atribui à session o id da central de regulação clicada para ser usada no popup que será aberto
                    if (img.ClientID == atual.ClientID)
                    {
                        //Coleta dados da central de regulação
                        var dadosCentrRegu = (from tbs347 in TBS347_CENTR_REGUL.RetornaTodosRegistros()
                                              join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs347.CO_EMP_SOLIC equals tb25.CO_EMP
                                              join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs347.CO_COL_SOLIC equals tb03.CO_COL
                                              join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tb03.CO_DEPTO equals tb14.CO_DEPTO into l1
                                              from ls in l1.DefaultIfEmpty()
                                              where tbs347.ID_CENTR_REGUL == idCentrRegul
                                              select new
                                              {
                                                  tb03.NO_COL,
                                                  tb25.sigla,
                                                  tbs347.TBS219_ATEND_MEDIC.ID_ATEND_MEDIC,
                                                  depto = ls.CO_SIGLA_DEPTO,
                                                  codFunc = (!string.IsNullOrEmpty(tb03.CO_SIGLA_ENTID_PROFI) ? tb03.CO_SIGLA_ENTID_PROFI + " " + tb03.NU_ENTID_PROFI + " - " + tb03.CO_UF_ENTID_PROFI : tb03.CO_MAT_COL),
                                              }).FirstOrDefault();

                        var tbs347ob = TBS347_CENTR_REGUL.RetornaPelaChavePrimaria(idCentrRegul);
                        tbs347ob.TBS219_ATEND_MEDICReference.Load();

                        //Coelta dados do atendimento médico
                        var res = (from tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros()
                                   join tbs194 in TBS194_PRE_ATEND.RetornaTodosRegistros() on tbs219.TBS194_PRE_ATEND.ID_PRE_ATEND equals tbs194.ID_PRE_ATEND
                                   join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs219.CO_ALU equals tb07.CO_ALU
                                   where tbs219.ID_ATEND_MEDIC == tbs347ob.TBS219_ATEND_MEDIC.ID_ATEND_MEDIC
                                   select new
                                   {
                                       tbs219.CO_ATEND_MEDIC,
                                       tbs219.DT_ATEND_CADAS,
                                       tbs194.CO_TIPO_RISCO,
                                       codPac = tb07.NU_NIS,
                                       tb07.NO_ALU,
                                       tb07.CO_SEXO_ALU,
                                       tb07.DT_NASC_ALU,
                                   }).FirstOrDefault();

                        //Coleta dados do colaborador
                        var dadoRegis = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                         join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb03.TB25_EMPRESA.CO_EMP equals tb25.CO_EMP
                                         where tb03.CO_COL == LoginAuxili.CO_COL
                                         select new
                                         {
                                             tb25.sigla,
                                             tb03.NO_COL,
                                         }).FirstOrDefault();

                        txtDeOcorrMod.Text = "";
                        hidIdCentralRegu.Value = idCentrRegul.ToString();
                        hidIdItemCentralRegu.Value = idItemCentrlRegu.ToString();
                        hidCoAtend.Value = coAtend;
                        txtNuAtendMod.Text = res.CO_ATEND_MEDIC.Insert(2, ".").Insert(6, ".").Insert(9, ".");
                        txtdtAtenMod.Text = res.DT_ATEND_CADAS.Value.ToString("dd/MM/yyyy");
                        txtHrAtenMod.Text = res.DT_ATEND_CADAS.Value.ToString("HH:mm");
                        txtClasRiscMod.Text = AuxiliFormatoExibicao.RetornaNomeClassificacaoRisco(res.CO_TIPO_RISCO);
                        txtNomMedSoliMod.Text = dadosCentrRegu.NO_COL;
                        txtCRMMedSoliMod.Text = dadosCentrRegu.codFunc;
                        txtSglUnidSoliMod.Text = dadosCentrRegu.sigla;
                        txtLocalMedicSoliMod.Text = dadosCentrRegu.depto;
                        txtNisPaciMod.Text = res.codPac.ToString().PadLeft(7, '0');
                        txtNoPacMod.Text = res.NO_ALU;
                        txtSexPacMod.Text = AuxiliFormatoExibicao.RetornaSexo(res.CO_SEXO_ALU);
                        txtIdaPacMod.Text = AuxiliCalculos.CalculaIdade(res.DT_NASC_ALU);
                        txtRegisItemMod.Text = linha.Cells[3].Text;
                        txtNomeItemMod.Text = (((HiddenField)linha.Cells[5].FindControl("hidNoItem")).Value);
                        ddlTipoItemMod.SelectedValue = (((HiddenField)linha.Cells[5].FindControl("hidCoStatus")).Value);
                        txtRegistrante.Text = dadoRegis.NO_COL;
                        txtUniRegisMod.Text = dadoRegis.sigla;

                        txtDataOcoMod.Text = DateTime.Now.ToString();
                        txtHrOcoMod.Text = DateTime.Now.ToString("HH:mm");

                        updDiagHist.Update();
                        updModal.Update();
                        AbreModalCliente();

                        break;
                    }
                }
            }
        }

        protected void lnkRegisOcorrencia_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidIdCentralRegu.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o item da central de regulação para o qual a Ocorrência será Registrda");
                return;
            }

            if (string.IsNullOrEmpty(txtDeOcorrMod.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a descrição da ocorrência para registrar");
                return;
            }

            var resColab = TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == LoginAuxili.CO_COL).FirstOrDefault();
            
            TBS349_CENTR_REGUL_OCORR tbs349 = new TBS349_CENTR_REGUL_OCORR();
            tbs349.TBS347_CENTR_REGUL = TBS347_CENTR_REGUL.RetornaPelaChavePrimaria(int.Parse(hidIdCentralRegu.Value));
            tbs349.TBS350_ITEM_CENTR_REGUL = TBS350_ITEM_CENTR_REGUL.RetornaPelaChavePrimaria(int.Parse(hidIdItemCentralRegu.Value));
            tbs349.CO_ATEND_MEDIC = hidCoAtend.Value;
            tbs349.DT_REGIS_OCORR = DateTime.Now;
            tbs349.HR_REGIS_OCORR_ITEM = DateTime.Now.ToString("HH:mm");
            tbs349.TP_REGIS_OCORR_ITEM = ddlTipoItemMod.SelectedValue;
            tbs349.DE_REGIS_OCORR_ITEM = txtDeOcorrMod.Text;
            tbs349.CO_COL_MEDICO_OCORR = LoginAuxili.CO_COL;
            tbs349.CO_EMP_MEDICO_OCORR = LoginAuxili.CO_EMP;
            tbs349.FL_SITUA_OCORR_ITEM = "A";
            tbs349.CO_COL_REGIS_OCORR = LoginAuxili.CO_COL;
            tbs349.CO_EMP_REGIS_OCORR = LoginAuxili.CO_EMP;
            tbs349.TB14_DEPTO = (resColab.CO_DEPTO.HasValue ? TB14_DEPTO.RetornaPelaChavePrimaria(resColab.CO_DEPTO.Value) : null);
            tbs349.DT_REGIS_OCORR_ITEM = DateTime.Now;
            tbs349.IP_REGIS_OCORRO = Request.UserHostAddress;

            #region Sequencial NR Registro

            string coUnid = LoginAuxili.CO_UNID.ToString();
            int coEmp = LoginAuxili.CO_EMP;
            string ano = DateTime.Now.Year.ToString().Substring(2, 2);

            var res = (from tbs349pesq in TBS349_CENTR_REGUL_OCORR.RetornaTodosRegistros()
                       where tbs349pesq.CO_EMP_REGIS_OCORR == coEmp && tbs349pesq.NR_OCORR_CENTR_REGUL != null
                       select new { tbs349pesq.NR_OCORR_CENTR_REGUL }).OrderByDescending(w => w.NR_OCORR_CENTR_REGUL).FirstOrDefault();

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
                seq = res.NR_OCORR_CENTR_REGUL.Substring(7, 6);
                seq2 = int.Parse(seq);
            }

            seqConcat = seq2 + 1;
            seqcon = seqConcat.ToString().PadLeft(6, '0');

            tbs349.NR_OCORR_CENTR_REGUL = "OR" + ano + coUnid.PadLeft(3, '0') + seqcon;

            #endregion

            TBS349_CENTR_REGUL_OCORR.SaveOrUpdate(tbs349, true);

            //LimpaCamposOcorrencia();
        }

        #endregion
    }
}
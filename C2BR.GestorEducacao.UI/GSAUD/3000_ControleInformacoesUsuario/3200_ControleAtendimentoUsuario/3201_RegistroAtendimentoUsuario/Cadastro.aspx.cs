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
//25/08/2014| Maxwell Almeida            | Criação da funcionalidade de atendimento ao usuário
//          |                            |
//30/12/2014| Maxwell Almeida            | Inserção de regra para inserir o código do atendimento 
//          |                            | em todos os itens dependentes dele, como Exames, Serviços Ambulatoriais,
//          |                            | Reserva de medicamentos, receitas médicas, atestados médicos e diagnósticos.
//          |                            |
//05/01/2015| Maxwell Almeida            | Viabilizada a possibilidade de salvar como execução na instituição ou não
//

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
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3010_CtrlPedagogicoSeries;
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno;
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario;


namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3201_RegistroAtendimentoUsuario
{
    public partial class Cadastro : System.Web.UI.Page
    {
        #region Váriaveis
        int qtdLinhasGridExamesMOD = 0;
        int qtdLinhasGridProcedMOD = 0;
        #endregion

        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            //Atualiza estas informações independentemente de ser postback ou não
            txtDataAtes.Text = DateTime.Now.ToString();
            txtHrAtes.Text = DateTime.Now.ToString("HH:mm");

            if (!Page.IsPostBack)
            {


                //------------> Tamanho da imagem de Aluno
                upImagemAluno.ImagemLargura = 90;
                upImagemAluno.ImagemAltura = 98;
                upImagemAluno.MostraProcurar = false;

                CarregaMedicosEncaConsul();
                CarregaMedicosConsultas();
                carregaMedicosEncam();
                CarregaGridEncaminhamentos();
                CarregaUnidExame(ddlUnidAtestMedic);
                CarregaUnidadesMedidas(ddlUnidServAmbu);
                CarregaClassRisco(ddlClassRisco);
                CarregaDepartamentoReqAmbu();
                AuxiliCarregamentos.CarregaUnidade(ddlUnidReqExam, LoginAuxili.ORG_CODIGO_ORGAO, false, true, true, false);
                AuxiliCarregamentos.CarregaDepartamentos(ddlLocalReqExam, LoginAuxili.CO_EMP, false, true, false);
                AuxiliCarregamentos.CarregaUnidade(ddlUnidAtendServAmbu, LoginAuxili.ORG_CODIGO_ORGAO, false);
                txtDataAtend.Text = DateTime.Now.ToString();
                txtDataAtes.Text = DateTime.Now.ToString();
                txtHrAtes.Text = DateTime.Now.ToString("HH:mm");
                CarregaGridConsultas();
                //CarregaGridMedicamentos(0, LoginAuxili.CO_EMP);
                CarregaReceitasMedicas(0, grdMedicReceitados);

                #region Tabelas em memória

                #region Receita Médica

                DataTable dtATM = new DataTable();
                DataColumn dcATM;

                dcATM = new DataColumn();
                dcATM.DataType = System.Type.GetType("System.String");
                dcATM.ColumnName = "CO_PROD";
                dtATM.Columns.Add(dcATM);

                dcATM = new DataColumn();
                dcATM.DataType = System.Type.GetType("System.String");
                dcATM.ColumnName = "ID_RECEI";
                dtATM.Columns.Add(dcATM);

                dcATM = new DataColumn();
                dcATM.DataType = System.Type.GetType("System.String");
                dcATM.ColumnName = "medicamento";
                dtATM.Columns.Add(dcATM);

                dcATM = new DataColumn();
                dcATM.DataType = System.Type.GetType("System.String");
                dcATM.ColumnName = "qtd";
                dtATM.Columns.Add(dcATM);

                dcATM = new DataColumn();
                dcATM.DataType = System.Type.GetType("System.String");
                dcATM.ColumnName = "uso";
                dtATM.Columns.Add(dcATM);

                dcATM = new DataColumn();
                dcATM.DataType = System.Type.GetType("System.String");
                dcATM.ColumnName = "prescricao";
                dtATM.Columns.Add(dcATM);

                int i = 1;
                DataRow linha;
                while (i <= 15)
                {
                    linha = dtATM.NewRow();
                    linha["medicamento"] = "";
                    linha["qtd"] = "";
                    linha["uso"] = "";
                    linha["prescricao"] = "";
                    dtATM.Rows.Add(linha);
                    i++;
                }

                Session["TabelaMedicAtendMed"] = dtATM;

                grdMedcAtendMed.DataSource = dtATM;
                grdMedcAtendMed.DataBind();

                #endregion

                #region Colunas da tabela de medicamentos

                DataTable dt = new DataTable();
                DataColumn dc;

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "Medicamento";
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "GRUPO";
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "CO_PROD";
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "QTM1";
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "QTM2";
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "QTM3";
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "QTM4";
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "TOTAL";
                dt.Columns.Add(dc);

                Session["TabelaMedcamentos"] = dt;

                i = 1;
                while (i <= 15)
                {
                    linha = dt.NewRow();
                    linha["Medicamento"] = "";
                    linha["GRUPO"] = "";
                    linha["CO_PROD"] = "";
                    linha["QTM1"] = "";
                    linha["QTM2"] = "";
                    linha["QTM3"] = "";
                    linha["QTM4"] = "";
                    linha["TOTAL"] = "";
                    dt.Rows.Add(linha);
                    i++;
                }

                grdMedicamentos.DataSource = dt;
                grdMedicamentos.DataBind();

                #endregion

                #region ReservaMedicamentos

                //DataTable dtRMM = new DataTable();
                //DataColumn dcRMM;

                //dcRMM = new DataColumn();
                //dcRMM.DataType = System.Type.GetType("System.Boolean");
                //dcRMM.ColumnName = "HABIL_SELEC";
                //dtRMM.Columns.Add(dcRMM);

                //dcRMM = new DataColumn();
                //dcRMM.DataType = System.Type.GetType("System.String");
                //dcRMM.ColumnName = "CO_PROD ";
                //dtRMM.Columns.Add(dcRMM);

                //dcRMM = new DataColumn();
                //dcRMM.DataType = System.Type.GetType("System.String");
                //dcRMM.ColumnName = "ID_RECEI";
                //dtRMM.Columns.Add(dcRMM);

                //dcRMM = new DataColumn();
                //dcRMM.DataType = System.Type.GetType("System.String");
                //dcRMM.ColumnName = "medicamento";
                //dtRMM.Columns.Add(dcRMM);

                //dcRMM = new DataColumn();
                //dcRMM.DataType = System.Type.GetType("System.String");
                //dcRMM.ColumnName = "qtd";
                //dtRMM.Columns.Add(dcRMM);

                //dcRMM = new DataColumn();
                //dcRMM.DataType = System.Type.GetType("System.String");
                //dcRMM.ColumnName = "uso";
                //dtRMM.Columns.Add(dcRMM);

                //dcRMM = new DataColumn();
                //dcRMM.DataType = System.Type.GetType("System.String");
                //dcRMM.ColumnName = "prescricao";
                //dtRMM.Columns.Add(dcRMM);

                //dcRMM = new DataColumn();
                //dcRMM.DataType = System.Type.GetType("System.String");
                //dcRMM.ColumnName = "FP_V";
                //dtRMM.Columns.Add(dcRMM);

                //dcRMM = new DataColumn();
                //dcRMM.DataType = System.Type.GetType("System.String");
                //dcRMM.ColumnName = "ESTOQUE";
                //dtRMM.Columns.Add(dcRMM);

                //i = 1;
                //while (i <= 15)
                //{
                //    linha = dtRMM.NewRow();
                //    linha["HABIL_SELEC"] = true;
                //    linha["CO_PROD"] = "";
                //    linha["ID_RECEI"] = "";
                //    linha["medicamento"] = "";
                //    linha["qtd"] = "";
                //    linha["uso"] = "";
                //    linha["prescricao"] = "";
                //    linha["FP_V"] = "";
                //    linha["ESTOQUE"] = "";
                //    dtRMM.Rows.Add(linha);
                //    i++;
                //}

                //Session["TabelaMedicsRecReserva"] = dtRMM;

                //grdMedicReceitados.DataSource = dtRMM;
                //grdMedicReceitados.DataBind();

                #endregion

                #region Exames

                DataTable dtEATM = new DataTable();
                DataColumn dcEATM;

                dcEATM = new DataColumn();
                dcEATM.DataType = System.Type.GetType("System.String");
                dcEATM.ColumnName = "ID_EXAME";
                dtEATM.Columns.Add(dcEATM);

                dcEATM = new DataColumn();
                dcEATM.DataType = System.Type.GetType("System.String");
                dcEATM.ColumnName = "exame";
                dtEATM.Columns.Add(dcEATM);

                dcEATM = new DataColumn();
                dcEATM.DataType = System.Type.GetType("System.String");
                dcEATM.ColumnName = "unidade";
                dtEATM.Columns.Add(dcEATM);

                dcEATM = new DataColumn();
                dcEATM.DataType = System.Type.GetType("System.String");
                dcEATM.ColumnName = "local";
                dtEATM.Columns.Add(dcEATM);

                i = 1;
                while (i <= 15)
                {
                    linha = dtEATM.NewRow();
                    linha["exame"] = "";
                    linha["unidade"] = "";
                    linha["local"] = "";
                    dtEATM.Rows.Add(linha);
                    i++;
                }

                Session["TabelaExameAtendMed"] = dtEATM;

                grdExaAtendMed.DataSource = dtEATM;
                grdExaAtendMed.DataBind();

                #endregion

                #region Atestados

                DataTable dtEAME = new DataTable();
                DataColumn dcEAMEC;

                dcEAMEC = new DataColumn();
                dcEAMEC.DataType = System.Type.GetType("System.String");
                dcEAMEC.ColumnName = "ID_ATESTADO";
                dtEAME.Columns.Add(dcEAMEC);

                dcEAMEC = new DataColumn();
                dcEAMEC.DataType = System.Type.GetType("System.String");
                dcEAMEC.ColumnName = "ATESTADO";
                dtEAME.Columns.Add(dcEAMEC);

                dcEAMEC = new DataColumn();
                dcEAMEC.DataType = System.Type.GetType("System.String");
                dcEAMEC.ColumnName = "UNIDADE";
                dtEAME.Columns.Add(dcEAMEC);

                dcEAMEC = new DataColumn();
                dcEAMEC.DataType = System.Type.GetType("System.String");
                dcEAMEC.ColumnName = "DATA";
                dtEAME.Columns.Add(dcEAMEC);

                dcEAMEC = new DataColumn();
                dcEAMEC.DataType = System.Type.GetType("System.String");
                dcEAMEC.ColumnName = "DIASREP";
                dtEAME.Columns.Add(dcEAMEC);

                dcEAMEC = new DataColumn();
                dcEAMEC.DataType = System.Type.GetType("System.String");
                dcEAMEC.ColumnName = "CID";
                dtEAME.Columns.Add(dcEAMEC);

                i = 1;
                while (i <= 15)
                {
                    linha = dtEAME.NewRow();
                    linha["ID_ATESTADO"] = "";
                    linha["ATESTADO"] = "";
                    linha["UNIDADE"] = "";
                    linha["DATA"] = "";
                    linha["DIASREP"] = "";
                    dtEAME.Rows.Add(linha);
                    i++;
                }

                grdAtesMedic.DataSource = dtEAME;
                grdAtesMedic.DataBind();

                #endregion

                #region Serviços Ambulatoriais

                DataTable dtSAMBU = new DataTable();
                DataColumn dcSAMBU;

                dcSAMBU = new DataColumn();
                dcSAMBU.DataType = System.Type.GetType("System.String");
                dcSAMBU.ColumnName = "ID_ATEND_SERV_AMBUL";
                dtSAMBU.Columns.Add(dcSAMBU);

                dcSAMBU = new DataColumn();
                dcSAMBU.DataType = System.Type.GetType("System.String");
                dcSAMBU.ColumnName = "servico";
                dtSAMBU.Columns.Add(dcSAMBU);

                dcSAMBU = new DataColumn();
                dcSAMBU.DataType = System.Type.GetType("System.String");
                dcSAMBU.ColumnName = "tipoValid";
                dtSAMBU.Columns.Add(dcSAMBU);

                dcSAMBU = new DataColumn();
                dcSAMBU.DataType = System.Type.GetType("System.String");
                dcSAMBU.ColumnName = "aplicacaoValid";
                dtSAMBU.Columns.Add(dcSAMBU);


                i = 1;
                while (i <= 15)
                {
                    linha = dtSAMBU.NewRow();
                    linha["servico"] = "";
                    linha["tipoValid"] = "";
                    linha["aplicacaoValid"] = "";
                    dtSAMBU.Rows.Add(linha);
                    i++;
                }

                Session["TabelaServAtendMed"] = dtEATM;

                grdServAmbu.DataSource = dtSAMBU;
                grdServAmbu.DataBind();

                #endregion

                #endregion
            }
            else
            {

            }
        }

        #endregion

        #region Inclusões

        protected void lnkEfetAtendMed_Click(object sender, EventArgs e)
        {
            bool sucesso = SalvaAtendimento();

            if (sucesso == false)
                AuxiliPagina.EnvioMensagemErro(this.Page, "Atendimento não efetivado com êxito, favor rever as informações");
        }

        protected void lnkIncMedicaGrid_OnClick(object sender, EventArgs e)
        {
            ControlaTabs("RCM", "");

            if (txtQtdMedcaAtendMed.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a quantidade em números");
                return;
            }

            if (txtDiasUsoMedcaAtendMed.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a quantidade de dias de uso.");
                return;
            }

            if (hidCoMedic.Value == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um medicamento");
                return;
            }

            if (string.IsNullOrEmpty(hidCoProfSaud.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um Profissional de Saúde");
                return;
            }

            if (string.IsNullOrEmpty(hidIdAtendimentoMedico.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor Efetivar o Atendimento antes de registrar informações de Receita Médica.");
                ControlaTabs("ATM", "RCM");
                chkAtdMedico.Checked = true;
                chkReqMedicPaci.Checked = false;
                return;
            }

            int cocol = int.Parse(hidCoProfSaud.Value);
            var re = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                      where tb03.CO_COL == cocol
                      select new { tb03.CO_EMP }).FirstOrDefault();

            int idAtend = int.Parse(hidIdAtendimentoMedico.Value);

            //Salva as informações de receituário preenchidas
            TBS330_RECEI_ATEND_MEDIC tbs330 = new TBS330_RECEI_ATEND_MEDIC();
            tbs330.QT_MEDIC = int.Parse(txtQtdMedcaAtendMed.Text);
            tbs330.QT_USO = int.Parse(txtDiasUsoMedcaAtendMed.Text);
            tbs330.DT_RECEI = DateTime.Now;
            tbs330.DE_PRINC_ATIVO = txtPrincAtivMedc.Text;
            tbs330.DE_PRESC = txtPrescMedicaAtendMed.Text;
            tbs330.CO_MEDIC = int.Parse(hidCoMedic.Value);
            tbs330.CO_COL_MEDIC = int.Parse(hidCoProfSaud.Value);
            tbs330.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(re.CO_EMP);
            tbs330.CO_ALU = int.Parse(hidCoPac.Value);
            tbs330.ID_ATEND_MEDIC = idAtend;
            tbs330.CO_ATEND_MEDIC = TBS219_ATEND_MEDIC.RetornaPelaChavePrimaria(idAtend).CO_ATEND_MEDIC;
            tbs330.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            #region Sequencial NR Registro

            string coUnid = LoginAuxili.CO_UNID.ToString();
            int coEmp = LoginAuxili.CO_EMP;
            string ano = DateTime.Now.Year.ToString().Substring(2, 2);

            var res = (from tbs330pesq in TBS330_RECEI_ATEND_MEDIC.RetornaTodosRegistros()
                       where tbs330pesq.TB25_EMPRESA.CO_EMP == coEmp && tbs330pesq.NU_REGIS_RECEI != null
                       select new { tbs330pesq.NU_REGIS_RECEI }).OrderByDescending(w => w.NU_REGIS_RECEI).FirstOrDefault();

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
                seq = res.NU_REGIS_RECEI.Substring(7, 6);
                seq2 = int.Parse(seq);
            }

            seqConcat = seq2 + 1;
            seqcon = seqConcat.ToString().PadLeft(6, '0');

            tbs330.NU_REGIS_RECEI = "RE" + ano + coUnid.PadLeft(3, '0') + seqcon;

            #endregion

            TBS330_RECEI_ATEND_MEDIC.SaveOrUpdate(tbs330, true);

            //Limpa todos os campos depois de salvar a informação
            txtQtdMedcaAtendMed.Text = txtDiasUsoMedcaAtendMed.Text = txtPrincAtivMedc.Text = txtPrescMedicaAtendMed.Text = hidCoMedic.Value = txtCodMedic.Text = txtdescMedic.Text = "";

            CarregaReceitasMedicas(int.Parse(hidIdAtendimentoMedico.Value), grdMedcAtendMed);
            CarregaReceitasMedicas(int.Parse(hidIdAtendimentoMedico.Value), grdMedicReceitados);
            lnkImpReceita.Enabled = true;
        }

        protected void lnkIncExam_OnClick(object sender, EventArgs e)
        {
            ControlaTabs("RQE", "");

            if (string.IsNullOrEmpty(hidIdAtendimentoMedico.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor Efetivar o Atendimento antes de registrar Requisições de Exames.");
                ControlaTabs("ATM", "RQE");
                chkAtdMedico.Checked = true;
                chkReqMedicPaci.Checked = false;
                return;
            }

            if (string.IsNullOrEmpty(hidCoProfSaud.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um Profissional de Saúde");
                return;
            }

            if (hidCodExame.Value == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um tipo de Exame");
                return;
            }

            //Duas verificações que serão realizadas apenas se o exame estiver selecionado para ser executado na instituição
            if (chkExecuExameInsti.Checked)
            {
                if (ddlUnidReqExam.SelectedValue == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecione a empresa na qual será feito o exame");
                    ddlUnidReqExam.Focus();
                    return;
                }

                if (ddlLocalReqExam.SelectedValue == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o local do Exame");
                    ddlLocalReqExam.Focus();
                    return;
                }
            }

            int idExame = int.Parse(hidCodExame.Value);

            //Responsável por verificar se o exame está devidamente associado à unidade
            #region Tratamento do Exame

            //Verifica se o procedimento correspondente ao código inserido está associado à unidade logada
            //Faz selects distintos para Com operadora ou não
            bool EstaAssoc = false;

            int idAtend = (!string.IsNullOrEmpty(hidIdAtendimentoMedico.Value) ? int.Parse(hidIdAtendimentoMedico.Value) : 0);
            int idOper = CarregaOperaAtendimento(idAtend);
            int coEMp = (!string.IsNullOrEmpty(ddlUnidReqExam.SelectedValue) ? int.Parse(ddlUnidReqExam.SelectedValue) : 0);

            //Só valida as informações (Se o exame em questão está associado à unidade selecionada) se o exame for ser executado na instituição
            if (chkExecuExameInsti.Checked)
            {
                if (idOper == 0)
                {
                    //Verifica se o procedimento correspondente ao código inserido está associado à unidade logada
                    var res2 = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                                join tbs358 in TBS358_PROC_MEDIC_UNID.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs358.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                                where tbs356.CO_SITU_PROC_MEDI == "A"
                                && tbs358.FL_SITUA_ASSOC_PROC_UNID == "A"
                                && tbs358.TB25_EMPRESA.CO_EMP == coEMp
                                && tbs356.CO_OPER == "999"
                                && tbs356.ID_PROC_MEDI_PROCE == idExame
                                select tbs358).FirstOrDefault();

                    if (res2 == null) // Se não tiver associação
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "O Exame em questão não está associado à Unidade selecionada.");
                        return;
                    }
                    else if (res2.FL_PROC_MEDIC_DISPO == "N") // Se a associação estiver como indisponível
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "O Exame em questão está associado à Unidade selecionada, porém encontra-se indisponível.");
                        return;
                    }
                }
                else
                {
                    //Valida se o exame em questão possui agrupador, se possuir verifica se o mesmo está associado à unidade selecionada
                    if (TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(idExame).ID_AGRUP_PROC_MEDI_PROCE != null)
                    {
                        var res2 = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                                    join tbs358 in TBS358_PROC_MEDIC_UNID.RetornaTodosRegistros() on tbs356.ID_AGRUP_PROC_MEDI_PROCE equals tbs358.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                                    where tbs356.CO_SITU_PROC_MEDI == "A"
                                    && tbs358.FL_SITUA_ASSOC_PROC_UNID == "A"
                                    && tbs358.TB25_EMPRESA.CO_EMP == coEMp
                                    && tbs356.TB250_OPERA.ID_OPER == idOper
                                    && tbs356.ID_PROC_MEDI_PROCE == idExame
                                    select tbs356).FirstOrDefault();

                        if (res2 == null) // Se não tiver associação
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "O Exame agrupador deste não está associada à Unidade selecionada.");
                            return;
                        }
                        else if (res2.FL_PROC_MEDIC_DISPO == "N") // Se a associação estiver como indisponível
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "O Exame agrupador deste está associado à Unidade selecionada, porém encontra-se indisponível.");
                            return;
                        }
                    }
                    else
                    {
                        var res2 = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                                    join tbs358 in TBS358_PROC_MEDIC_UNID.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs358.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                                    where tbs356.CO_SITU_PROC_MEDI == "A"
                                    && tbs358.FL_SITUA_ASSOC_PROC_UNID == "A"
                                    && tbs358.TB25_EMPRESA.CO_EMP == coEMp
                                        //&& tbs356.TB250_OPERA.ID_OPER == idOper
                                    && tbs356.ID_PROC_MEDI_PROCE == idExame
                                    select tbs356).FirstOrDefault();

                        if (res2 == null) // Se não tiver associação
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "O Exame não está associada à Unidade selecionada.");
                            return;
                        }
                        else if (res2.FL_PROC_MEDIC_DISPO == "N") // Se a associação estiver como indisponível
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "O Exame está associado à Unidade selecionada, porém encontra-se indisponível.");
                            return;
                        }
                    }
                }
            }

            #endregion

            //Verifica se este exame já foi requisitado para este paciente neste atendimento
            #region Verificação

            bool jaRequisitado = TBS218_EXAME_MEDICO.RetornaTodosRegistros().Where(w => w.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                == idExame && w.ID_ATEND_MEDIC == idAtend).Any();

            if (jaRequisitado) //Se já existir requisitação
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Este Exame já foi requisitado para este atendimento!");
                txtCodExame.Focus();
                return;
            }

            #endregion

            //Coleta dados do Colaborador
            int cocol = int.Parse(hidCoProfSaud.Value);
            var re = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                      where tb03.CO_COL == cocol
                      select new { tb03.CO_EMP }).FirstOrDefault();

            //Coleta dados do atendimento
            var dadosAtend = (from tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros()
                              where tbs219.ID_ATEND_MEDIC == idAtend
                              select new { tbs219.CO_ALU, tbs219.TB108_RESPONSAVEL.CO_RESP }).FirstOrDefault();

            var resProc = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(idExame);

            //Salva as informações do exame preenchidas
            TBS218_EXAME_MEDICO tbs218 = new TBS218_EXAME_MEDICO();
            tbs218.TBS356_PROC_MEDIC_PROCE = resProc;
            tbs218.TB25_EMPRESA = (ddlUnidReqExam.SelectedValue != "" ? TB25_EMPRESA.RetornaPelaChavePrimaria(int.Parse(ddlUnidReqExam.SelectedValue)) : null);
            tbs218.TB14_DEPTO = (ddlLocalReqExam.SelectedValue != "" ? TB14_DEPTO.RetornaPelaChavePrimaria(int.Parse(ddlLocalReqExam.SelectedValue)) : null);
            tbs218.ID_ATEND_MEDIC = idAtend;
            tbs218.CO_ATEND_MEDIC = TBS219_ATEND_MEDIC.RetornaPelaChavePrimaria(idAtend).CO_ATEND_MEDIC;
            tbs218.CO_COL_MEDIC = int.Parse(hidCoProfSaud.Value);
            tbs218.CO_EMP_MEDIC = re.CO_EMP;
            tbs218.DT_EXAME = DateTime.Now;
            tbs218.CO_TIPO_EXECU = chkExecuExameInsti.Checked ? "I" : "E"; //Trata para salvar interno ou externo

            #region Sequencial NR Registro

            string coUnid = LoginAuxili.CO_UNID.ToString();
            int coEmp = LoginAuxili.CO_EMP;
            string ano = DateTime.Now.Year.ToString().Substring(2, 2);

            var res = (from tbs218pesq in TBS218_EXAME_MEDICO.RetornaTodosRegistros()
                       where tbs218pesq.TB25_EMPRESA.CO_EMP == coEmp && tbs218pesq.NU_REGIS_EXAME != null
                       select new { tbs218pesq.NU_REGIS_EXAME }).OrderByDescending(w => w.NU_REGIS_EXAME).FirstOrDefault();

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
                seq = res.NU_REGIS_EXAME.Substring(7, 6);
                seq2 = int.Parse(seq);
            }

            seqConcat = seq2 + 1;
            seqcon = seqConcat.ToString().PadLeft(6, '0');

            tbs218.NU_REGIS_EXAME = "EX" + ano + coUnid.PadLeft(3, '0') + seqcon;

            #endregion

            tbs218 = TBS218_EXAME_MEDICO.SaveOrUpdate(tbs218);

            //Grava o Financeiro e encaminha para a central de regulação, apenas se o exame for ser executado na instituição
            if (chkExecuExameInsti.Checked)
            {
                //Grava o financeiro do procedimento
                GravaFinanceiroProcedimentos(resProc, dadosAtend.CO_ALU, dadosAtend.CO_RESP);

                //Salva o registro da central de regulação caso seja requerido
                if (FL_REQUE_APROV_EX.Value == "S")
                    SalvaRegistroCentralRegulacao(tbs218.ID_EXAME, "EX", tbs218.NU_REGIS_EXAME, tbs218.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI);
            }

            ddlLocalReqExam.SelectedValue = "";
            ddlUnidReqExam.SelectedValue = LoginAuxili.CO_EMP.ToString();
            txtCodExame.Text = hidCodExame.Value = txtExameAtendMed.Text = FL_REQUE_APROV_EX.Value = "";

            //Responsável por recarregar a grid
            CarregaReqExamesMedicos(int.Parse(hidIdAtendimentoMedico.Value));
            lnkImpGuiaExame.Enabled = true;
            CarregaGridHistoricExamesMedicos(int.Parse(hidCoPac.Value));
        }

        protected void lnkIncServAmbu_OnClick(object sender, EventArgs e)
        {
            ControlaTabs("RSA", "");

            if (string.IsNullOrEmpty(hidIdAtendimentoMedico.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor Efetivar o Atendimento antes de registrar Requisições de Serviços Ambulatoriais.");
                ControlaTabs("ATM", "RSA");
                chkAtdMedico.Checked = true;
                chkReqMedicPaci.Checked = false;
                return;
            }

            if (string.IsNullOrEmpty(hidCoProfSaud.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um Profissional de Saúde");
                return;
            }

            if (hidCodServAmbu.Value == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um tipo de Serviço Ambulatorial");
                return;
            }

            //Realiza estas validações apenas quando o serviço ambulatorial for ser executado na instituição
            if (chkExecuServAmbuInst.Checked)
            {
                if (ddlUnidAtendServAmbu.SelectedValue == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar a Unidade onde será prestado o Serviço Ambulatorial.");
                    ddlUnidAtendServAmbu.Focus();
                    return;
                }

                if (ddlLocalServAmbu.SelectedValue == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar o Local onde será feito o atendimento do Procedimento");
                    ddlLocalServAmbu.Focus();
                    return;
                }
            }

            int idProcedimento = int.Parse(hidCodServAmbu.Value);

            //Responsável por verificar se o exame está devidamente associado à unidade
            #region Tratamento do Procedimento

            //Verifica se o procedimento correspondente ao código inserido está associado à unidade logada
            //Faz selects distintos para Com operadora ou não

            int idAtend = (!string.IsNullOrEmpty(hidIdAtendimentoMedico.Value) ? int.Parse(hidIdAtendimentoMedico.Value) : 0);
            int idOper = CarregaOperaAtendimento(idAtend);
            int coEMp = (!string.IsNullOrEmpty(ddlUnidReqExam.SelectedValue) ? int.Parse(ddlUnidReqExam.SelectedValue) : 0);

            //Só valida as informações (Se o exame em questão está associado à unidade selecionada) se o exame for ser executado na instituição
            if (chkExecuServAmbuInst.Checked)
            {
                if (idOper == 0)
                {
                    //Verifica se o procedimento correspondente ao código inserido está associado à unidade logada
                    var res2 = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                                join tbs358 in TBS358_PROC_MEDIC_UNID.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs358.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                                where tbs356.CO_SITU_PROC_MEDI == "A"
                                && tbs358.FL_SITUA_ASSOC_PROC_UNID == "A"
                                && tbs358.TB25_EMPRESA.CO_EMP == coEMp
                                && tbs356.CO_OPER == "999"
                                && tbs356.ID_PROC_MEDI_PROCE == idProcedimento
                                select tbs358).FirstOrDefault();

                    if (res2 == null) // Se não tiver associação
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "O Procedimento Médico em questão não está associado à Unidade selecionada.");
                        return;
                    }
                    else if (res2.FL_PROC_MEDIC_DISPO == "N") // Se a associação estiver como indisponível
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "O Procedimento Médico em questão está associado à Unidade selecionada, porém encontra-se indisponível.");
                        return;
                    }
                }
                else
                {
                    //Valida se o exame em questão possui agrupador, se possuir verifica se o mesmo está associado à unidade selecionada
                    if (TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(idProcedimento).ID_AGRUP_PROC_MEDI_PROCE != null)
                    {
                        var res2 = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                                    join tbs358 in TBS358_PROC_MEDIC_UNID.RetornaTodosRegistros() on tbs356.ID_AGRUP_PROC_MEDI_PROCE equals tbs358.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                                    where tbs356.CO_SITU_PROC_MEDI == "A"
                                    && tbs358.FL_SITUA_ASSOC_PROC_UNID == "A"
                                    && tbs358.TB25_EMPRESA.CO_EMP == coEMp
                                    && tbs356.TB250_OPERA.ID_OPER == idOper
                                    && tbs356.ID_PROC_MEDI_PROCE == idProcedimento
                                    select tbs356).FirstOrDefault();

                        if (res2 == null) // Se não tiver associação
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "O Procedimento Médico agrupador deste não está associada à Unidade selecionada.");
                            return;
                        }
                        else if (res2.FL_PROC_MEDIC_DISPO == "N") // Se a associação estiver como indisponível
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "O Procedimento Médico agrupador deste está associado à Unidade selecionada, porém encontra-se indisponível.");
                            return;
                        }
                    }
                    else
                    {
                        var res2 = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                                    join tbs358 in TBS358_PROC_MEDIC_UNID.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs358.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                                    where tbs356.CO_SITU_PROC_MEDI == "A"
                                    && tbs358.FL_SITUA_ASSOC_PROC_UNID == "A"
                                    && tbs358.TB25_EMPRESA.CO_EMP == coEMp
                                        //&& tbs356.TB250_OPERA.ID_OPER == idOper
                                    && tbs356.ID_PROC_MEDI_PROCE == idProcedimento
                                    select tbs356).FirstOrDefault();

                        if (res2 == null) // Se não tiver associação
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "O Procedimento Médico não está associada à Unidade Selecionada.");
                            return;
                        }
                        else if (res2.FL_PROC_MEDIC_DISPO == "N") // Se a associação estiver como indisponível
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "O Exame está associado à Unidade Selecionada, porém encontra-se indisponível.");
                            return;
                        }
                    }
                }
            }

            #endregion

            //Verifica se este exame já foi requisitado para este paciente neste atendimento
            #region Verificação

            bool jaRequisitado = TBS332_ATEND_SERV_AMBUL.RetornaTodosRegistros().Where(w => w.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                == idProcedimento && w.ID_ATEND_MEDIC == idAtend).Any();

            if (jaRequisitado) //Se já existir requisitação
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Este Procedimento Médico já foi requisitado para este atendimento!");
                txtCodExame.Focus();
                return;
            }

            #endregion

            int cocol = int.Parse(hidCoProfSaud.Value);
            var re = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                      where tb03.CO_COL == cocol
                      select new { tb03.CO_EMP }).FirstOrDefault();

            var resProc = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(idProcedimento);

            var dadosAtend = (from tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros()
                              where tbs219.ID_ATEND_MEDIC == idAtend
                              select new { tbs219.CO_ALU, tbs219.TB108_RESPONSAVEL.CO_RESP }).FirstOrDefault();

            int unidAtend = (!string.IsNullOrEmpty(ddlUnidAtendServAmbu.SelectedValue) ? int.Parse(ddlUnidAtendServAmbu.SelectedValue) : 0);
            //Salva as informações da Requisição de Serviço Ambulatorial
            TBS332_ATEND_SERV_AMBUL tbs332 = new TBS332_ATEND_SERV_AMBUL();
            tbs332.TBS356_PROC_MEDIC_PROCE = resProc;
            tbs332.CO_DEPTO = (ddlLocalServAmbu.SelectedValue != "" ? int.Parse(ddlLocalServAmbu.SelectedValue) : (int?)null);
            tbs332.TP_SERVI = (!string.IsNullOrEmpty(ddlTipoServAmbu.SelectedValue) ? ddlTipoServAmbu.SelectedValue : null);
            tbs332.TP_APLIC = (ddlTipoAplicServAmbu.SelectedValue != "N" ? ddlTipoAplicServAmbu.SelectedValue : null);
            tbs332.QT_APLIC = txtQtdServAmbu.Text;
            tbs332.TB89_UNIDADES = (!string.IsNullOrEmpty(txtQtdServAmbu.Text) ? TB89_UNIDADES.RetornaPelaChavePrimaria(int.Parse(ddlUnidServAmbu.SelectedValue)) : null);
            tbs332.CO_COL_MEDIC = int.Parse(hidCoProfSaud.Value);
            tbs332.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(re.CO_EMP);
            tbs332.ID_ATEND_MEDIC = idAtend;
            tbs332.CO_ATEND_MEDIC = TBS219_ATEND_MEDIC.RetornaPelaChavePrimaria(idAtend).CO_ATEND_MEDIC;
            tbs332.DT_SERVI_AMBUL = DateTime.Now;
            tbs332.TB25_EMPRESA1 = (unidAtend != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(unidAtend) : null);
            tbs332.CO_TIPO_EXECU = chkExecuServAmbuInst.Checked ? "I" : "E"; //Trata para salvar interno ou externo

            #region Sequencial NR Registro

            string coUnid = LoginAuxili.CO_UNID.ToString();
            int coEmp = LoginAuxili.CO_EMP;
            string ano = DateTime.Now.Year.ToString().Substring(2, 2);

            var res = (from tbs332pesq in TBS332_ATEND_SERV_AMBUL.RetornaTodosRegistros()
                       where tbs332pesq.TB25_EMPRESA.CO_EMP == coEmp && tbs332pesq.NU_REGIS_SERVI_AMBUL != null
                       select new { tbs332pesq.NU_REGIS_SERVI_AMBUL }).OrderByDescending(w => w.NU_REGIS_SERVI_AMBUL).FirstOrDefault();

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
                seq = res.NU_REGIS_SERVI_AMBUL.Substring(7, 6);
                seq2 = int.Parse(seq);
            }

            seqConcat = seq2 + 1;
            seqcon = seqConcat.ToString().PadLeft(6, '0');

            tbs332.NU_REGIS_SERVI_AMBUL = "SA" + ano + coUnid.PadLeft(3, '0') + seqcon;

            #endregion

            TBS332_ATEND_SERV_AMBUL.SaveOrUpdate(tbs332, true);

            //Grava o Financeiro e encaminha para a central de regulação, apenas se o Procedimento Médico for ser executado na instituição
            if (chkExecuExameInsti.Checked)
            {
                //Grava o financeiro do procedimento
                GravaFinanceiroProcedimentos(resProc, dadosAtend.CO_ALU, dadosAtend.CO_RESP);

                //Salva o registro da central de regulação caso seja requerido
                if (FL_REQUE_APROV_SA.Value == "S")
                    SalvaRegistroCentralRegulacao(tbs332.ID_ATEND_SERV_AMBUL, "SA", tbs332.NU_REGIS_SERVI_AMBUL, TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(hidCodServAmbu.Value)).NM_PROC_MEDI);
            }

            txtCodServAmbu.Text = txtNoServAmu.Text = txtQtdServAmbu.Text = ddlTipoServAmbu.SelectedValue = hidCodServAmbu.Value
                = FL_REQUE_APROV_SA.Value = "";

            ddlLocalServAmbu.SelectedValue = "";
            ddlTipoAplicServAmbu.SelectedValue = "N";

            CarregaGridRequiAmbu(int.Parse(hidIdAtendimentoMedico.Value));
            lnkImpGuiaServAmbu.Enabled = true;
            CarregaGridHistoricServAmbu(int.Parse(hidCoPac.Value));
        }

        protected void lnkIncMed_Click(object sender, EventArgs e)
        {
            ControlaTabs("MED", "");
            int coProd = hidCoProdResMedic.Value != "" ? int.Parse(hidCoProdResMedic.Value) : 0;
            bool SalvoApartirReceita = false;

            foreach (GridViewRow lis in grdMedicReceitados.Rows)
            {
                if (((CheckBox)lis.Cells[0].FindControl("ckSelectResMedic")).Checked)
                {
                    int coProdRec = int.Parse((((HiddenField)lis.Cells[0].FindControl("hidCoProd")).Value));

                    CarregaSessionReserMedicamentos(coProdRec);
                    SalvoApartirReceita = true;
                }
            }

            //Deseleciona caso tenha sido selecionado algum item na grid do receituário
            if (SalvoApartirReceita == true)
            {
                foreach (GridViewRow lis in grdMedicReceitados.Rows)
                {
                    CheckBox ck = ((CheckBox)lis.Cells[0].FindControl("ckSelectResMedic"));
                    ck.Checked = false;
                }
            }

            //Se não foi salvo a partir da receita médica, valida se o hidden com os dados do medicamento está vazio
            if ((SalvoApartirReceita == false) && (string.IsNullOrEmpty(hidCoProdResMedic.Value)))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhum medicamento selecionado para inclusão, favor informar.");
                return;
            }

            //Salva a partir dos campos digitados apenas se não tiver sido salvo a partir da receita médica
            if (SalvoApartirReceita == false)
                CarregaSessionReserMedicamentos(coProd);

            //Limpa os dados usados para persistir as informações
            txtMedicamento.Text = hidCoProdResMedic.Value = hidNoGrupo.Value = txtDescMedicamento.Text =
                txtQTM1.Text = txtQTM2.Text = txtQTM3.Text = txtQTM4.Text = "";

            lnkEmissGuiaReserMedic.Enabled = true;
        }

        protected void lnkIncAtesMedic_OnClick(object sender, EventArgs e)
        {
            ControlaTabs("AME", "");

            if (string.IsNullOrEmpty(hidIdAtendimentoMedico.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor Efetivar o Atendimento antes de registrar informações de Atestados Médicos.");
                ControlaTabs("ATM", "AME");
                chkAtdMedico.Checked = true;
                chkReqMedicPaci.Checked = false;
                return;
            }

            if (string.IsNullOrEmpty(hidCoProfSaud.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um Profissional de Saúde");
                return;
            }

            if (hidCodAtesMedic.Value == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um documento de atestado");
                return;
            }

            if (ddlCidAtesMedc.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar CID que o paciente porta");
                return;
            }

            if (hidCoPac.Value == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um paciente para atendimento, antes de incluir atestados");
                return;
            }

            if (txtQtDiasAtes.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a quantidade de dias de afastamento");
                return;
            }

            if (txtDataAtes.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a Data do atestado");
                return;
            }

            int cocol = int.Parse(hidCoProfSaud.Value);
            var re = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                      where tb03.CO_COL == cocol
                      select new { tb03.CO_EMP }).FirstOrDefault();

            int coPac = int.Parse(hidCoPac.Value);
            var pac = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where tb07.CO_ALU == coPac
                       select tb07).FirstOrDefault();

            int idEnca = int.Parse(hidIdEncam.Value);
            int coResp = TBS195_ENCAM_MEDIC.RetornaPelaChavePrimaria(idEnca).CO_RESP;

            int idAtend = int.Parse(hidIdAtendimentoMedico.Value);

            TBS333_ATEST_MEDIC_PACIE tbs333 = new TBS333_ATEST_MEDIC_PACIE();
            tbs333.ID_DOCUM = int.Parse(hidCodAtesMedic.Value);
            tbs333.IDE_CID = int.Parse(ddlCidAtesMedc.SelectedValue);
            tbs333.ID_ATEND_MEDIC = idAtend;
            tbs333.CO_ATEND_MEDIC = TBS219_ATEND_MEDIC.RetornaPelaChavePrimaria(idAtend).CO_ATEND_MEDIC;
            tbs333.CO_ALU = int.Parse(hidCoPac.Value);
            tbs333.QT_DIAS = int.Parse(txtQtDiasAtes.Text);
            tbs333.NU_HORAS = txtHrAtes.Text;
            tbs333.DE_OBSER = txtObsAtesMedic.Text;
            tbs333.DT_ATEST_MEDIC = DateTime.Parse(txtDataAtes.Text);
            tbs333.DT_CADAS = DateTime.Now;
            tbs333.CO_EMP_MEDIC = re.CO_EMP;
            tbs333.CO_COL_MEDIC = int.Parse(hidCoProfSaud.Value);
            tbs333.CO_EMP = LoginAuxili.CO_EMP;
            tbs333.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(coResp);

            #region Sequencial NR Registro

            string coUnid = LoginAuxili.CO_UNID.ToString();
            int coEmp = LoginAuxili.CO_EMP;
            string ano = DateTime.Now.Year.ToString().Substring(2, 2);

            var res = (from tbs333pesq in TBS333_ATEST_MEDIC_PACIE.RetornaTodosRegistros()
                       where tbs333pesq.CO_EMP == coEmp && tbs333pesq.NU_REGIS_ATEST_MEDIC != null
                       select new { tbs333pesq.NU_REGIS_ATEST_MEDIC }).OrderByDescending(w => w.NU_REGIS_ATEST_MEDIC).FirstOrDefault();

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
                seq = res.NU_REGIS_ATEST_MEDIC.Substring(7, 6);
                seq2 = int.Parse(seq);
            }

            seqConcat = seq2 + 1;
            seqcon = seqConcat.ToString().PadLeft(6, '0');

            tbs333.NU_REGIS_ATEST_MEDIC = "AT" + ano + coUnid.PadLeft(3, '0') + seqcon;

            #endregion

            TBS333_ATEST_MEDIC_PACIE.SaveOrUpdate(tbs333, true);

            hidCodAtesMedic.Value = ddlCidAtesMedc.SelectedValue = txtQtDiasAtes.Text = txtObsAtesMedic.Text = txtCodAtestado.Text = txtAtestMedic.Text = "";
            CarregaGridAtestMedic(int.Parse(hidIdAtendimentoMedico.Value));
        }

        #endregion

        #region Exclusões

        //=======>Método que exclui as receitas médicas de um determinado atendimento
        protected void lnkDelMedicaGrid_OnClick(object sender, EventArgs e)
        {
            ControlaTabs("RCM", "");
            bool selec = false;
            //--------> Percorre todas as linhas da grid de receita
            foreach (GridViewRow linha in grdMedcAtendMed.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    selec = true;
                    HiddenField hidIdReceitu = ((HiddenField)linha.Cells[0].FindControl("hidIdReceitu"));
                    int idRece = Convert.ToInt32(hidIdReceitu.Value);

                    TBS330_RECEI_ATEND_MEDIC recei = TBS330_RECEI_ATEND_MEDIC.RetornaPelaChavePrimaria(idRece);

                    TBS330_RECEI_ATEND_MEDIC.Delete(recei, true);

                    CarregaReceitasMedicas(int.Parse(hidIdAtendimentoMedico.Value), grdMedcAtendMed);
                }
            }

            if (!selec)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar ao menos um item à ser deletado.");
                return;
            }

            if (grdMedcAtendMed.Rows.Count == 0)
                lnkImpReceita.Enabled = false;
        }

        //======>Método que exclui as requisições de Exames de um determinado atendimento
        protected void lnkExcReqExam_OnClick(object sender, EventArgs e)
        {
            ControlaTabs("RQE", "");
            bool Selec = false;

            //--------> Percorre todas as linhas da grid de receita
            foreach (GridViewRow linha in grdExaAtendMed.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    Selec = true;
                    HiddenField hidIdExam = ((HiddenField)linha.Cells[0].FindControl("hidCoReqExam"));
                    int idExam = Convert.ToInt32(hidIdExam.Value);

                    TBS218_EXAME_MEDICO recei = TBS218_EXAME_MEDICO.RetornaPelaChavePrimaria(idExam);

                    TBS218_EXAME_MEDICO.Delete(recei, true);

                    CarregaReqExamesMedicos(int.Parse(hidIdAtendimentoMedico.Value));
                }
            }

            if (!Selec)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar ao menos um item à ser deletado.");
                return;
            }

            if (grdExaAtendMed.Rows.Count == 0)
                lnkImpGuiaExame.Enabled = false;
        }

        //======>Método que exclui as Requisições de Serviços Ambulatoriais de um determinado atendimento
        protected void lnkExcServAmbu_OnClick(object sender, EventArgs e)
        {
            ControlaTabs("RSA", "");
            bool Selec = false;

            //--------> Percorre todas as linhas da grid de receita
            foreach (GridViewRow linha in grdServAmbu.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    Selec = true;
                    HiddenField hidIdServ = ((HiddenField)linha.Cells[0].FindControl("hidCoServAmbu"));
                    int idServ = Convert.ToInt32(hidIdServ.Value);

                    TBS332_ATEND_SERV_AMBUL serv = TBS332_ATEND_SERV_AMBUL.RetornaPelaChavePrimaria(idServ);

                    TBS332_ATEND_SERV_AMBUL.Delete(serv, true);

                    CarregaGridRequiAmbu(int.Parse(hidIdAtendimentoMedico.Value));
                }
            }

            if (!Selec)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar ao menos um item à ser deletado.");
                return;
            }

            if (grdServAmbu.Rows.Count == 0)
                lnkImpGuiaServAmbu.Enabled = false;
        }

        //====> Método que exclui registro de Atestado Médico
        protected void lnkExcAtesMedic_OnClick(object sender, EventArgs e)
        {
            //ControlaTabs("AME");

            ////--------> Percorre todas as linhas da grid de receita
            //foreach (GridViewRow linha in grdAtesMedic.Rows)
            //{
            //    if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
            //    {
            //        HiddenField hididAtes = ((HiddenField)linha.Cells[0].FindControl("hidIdAtesMedic"));
            //        int idAtes = Convert.ToInt32(hididAtes.Value);

            //        TBS333_ATEST_MEDIC_PACIE atest = TBS333_ATEST_MEDIC_PACIE.RetornaPelaChavePrimaria(idAtes);

            //        TBS333_ATEST_MEDIC_PACIE.Delete(atest, true);

            //        CarregaGridRequiAmbu(int.Parse(hidIdAtendimentoMedico.Value));
            //    }
            //}
        }

        protected void lnkExcMed_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["TabelaMedcamentos"];
            bool Selec = false;
            int i = 0;
            foreach (GridViewRow linha in grdMedicamentos.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    Selec = true;
                    dt.Rows[i].Delete();
                }
                i++;
            }
            dt.AcceptChanges();

            if (!Selec)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar ao menos um item à ser deletado.");
                return;
            }

            Session["TabelaMedcamentos"] = dt;

            grdMedicamentos.DataSource = dt;
            grdMedicamentos.DataBind();

            if (grdMedicamentos.Rows.Count == 0)
                lnkEmissGuiaReserMedic.Enabled = false;
        }

        #endregion

        #region Finalizações

        //=======>Método responsável por preparar as telas ao finalizar a parte de RECEITAS MÉDICAS
        protected void lnkFinalReceit_OnClick(object sender, EventArgs e)
        {
            chkReqMedicPaci.Checked = false;
            lblReqMediPaci.Visible = chkReqExamPaci.Checked = true;
            ControlaTabs("RQE", "RCM");
        }

        //======>Método responsável por preparar as telas ao finalizar a parte de REQUISIÇÃO DE EXAMES
        protected void lnkFimReqExam_OnClick(object sender, EventArgs e)
        {
            chkReqExamPaci.Checked = false;
            lblReqExamPaci.Visible = chkReqSevAmbuPaci.Checked = true;
            ControlaTabs("RSA", "RQE");
        }

        //======>Método responsável por preparar as telas ao finalizar a parte de REQUISIÇÃO DE SERVIÇO AMBULATORIAL
        protected void lnkFimReqServAmbu_OnClick(object sender, EventArgs e)
        {
            chkReqSevAmbuPaci.Checked = false;
            lblReqSevAmbuPaci.Visible = chkResMedicamentos.Checked = true;
            ControlaTabs("MED", "RSA");
        }

        //======>Método responsável por preparar as telas ao finalizar a parte de RESERVA DE MEDICAMENTOS
        protected void lkbFinalizarMedicamento_Click(object sender, EventArgs e)
        {
            ControlaChecks(chkEndAddAlu);



            this.lblResMedicamentos.Visible = true;
            chkResMedicamentos.Enabled = false;
            chkRegAtestMedcPaci.Checked = true;

            ControlaTabs("AME", "MED");
        }

        //======>Método responsável por preparar as telas ao finalizar a parte de REGISTRO DE ATESTADOS MÉDICOS
        protected void lnkFimAtesMedic_OnClick(object sender, EventArgs e)
        {
            chkRegAtestMedcPaci.Checked = false;
            lblRegAtestMedcPaci.Visible = chkEncaMedicPaci.Checked = true;
            ControlaTabs("ENM", "AME");
        }

        #endregion

        #region Impressões

        //======>Faz a impressão da receita médica
        protected void lnkImpReceita_OnClick(object sender, EventArgs e)
        {
            string infos;

            int coAtendimento = int.Parse(hidIdAtendimentoMedico.Value);
            int coEmp = LoginAuxili.CO_EMP;
            int lRetorno;
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptReceitaMedicaR fpcb = new RptReceitaMedicaR();
            lRetorno = fpcb.InitReport(infos, coEmp, coAtendimento);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
            //----------------> Limpa a var de sessão com o url do relatório.
            Session.Remove("URLRelatorio");

            //----------------> Limpa a ref da url utilizada para carregar o relatório.
            PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            isreadonly.SetValue(this.Request.QueryString, false, null);
            isreadonly.SetValue(this.Request.QueryString, true, null);

            //AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        //======>Faz a impressão da Guia de Exames
        protected void lnkImpGuiaExame_OnClick(object sender, EventArgs e)
        {
            string parametros = "";
            string infos;

            int coAtendimento = int.Parse(hidIdAtendimentoMedico.Value);
            int coEmp = LoginAuxili.CO_EMP;
            int lRetorno;
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptGuiaExame fpcb = new RptGuiaExame();
            lRetorno = fpcb.InitReport(parametros, infos, coEmp, coAtendimento, 0, ddlTipoEmissGuiaExame.SelectedValue);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
            //----------------> Limpa a var de sessão com o url do relatório.
            Session.Remove("URLRelatorio");

            //----------------> Limpa a ref da url utilizada para carregar o relatório.
            PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            isreadonly.SetValue(this.Request.QueryString, false, null);
            isreadonly.SetValue(this.Request.QueryString, true, null);
        }

        //======>Faz a impressão da Guia de Serviços Ambula
        protected void lnkImpGuiaServAmbu_OnClick(object sender, EventArgs e)
        {
            string parametros = "";
            string infos;

            int coAtendimento = int.Parse(hidIdAtendimentoMedico.Value);
            int coEmp = LoginAuxili.CO_EMP;
            int lRetorno;
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptGuiaServAmbul fpcb = new RptGuiaServAmbul();
            lRetorno = fpcb.InitReport(parametros, infos, coEmp, coAtendimento, 0, ddlTipoEmissGuiaServAmbu.SelectedValue);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
            //----------------> Limpa a var de sessão com o url do relatório.
            Session.Remove("URLRelatorio");

            //----------------> Limpa a ref da url utilizada para carregar o relatório.
            PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            isreadonly.SetValue(this.Request.QueryString, false, null);
            isreadonly.SetValue(this.Request.QueryString, true, null);
        }

        //======>Faz a impressão da Guia de Reserva
        protected void lnkEmissGuiaReserMedic_OnClick(object sender, EventArgs e)
        {
            string parametros = "";
            string infos;

            int coAtendimento = int.Parse(hidIdAtendimentoMedico.Value);
            int coEmp = LoginAuxili.CO_EMP;
            int lRetorno;
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptGuiaReserMedic fpcb = new RptGuiaReserMedic();
            lRetorno = fpcb.InitReport(parametros, infos, coEmp, coAtendimento);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
            //----------------> Limpa a var de sessão com o url do relatório.
            Session.Remove("URLRelatorio");

            //----------------> Limpa a ref da url utilizada para carregar o relatório.
            PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            isreadonly.SetValue(this.Request.QueryString, false, null);
            isreadonly.SetValue(this.Request.QueryString, true, null);
        }

        //======>Faz a impressão do Atestado médico
        protected void lnkImpAtesMedic_OnClick(object sender, EventArgs e)
        {
            int codAte = 0;
            foreach (GridViewRow li in grdAtesMedic.Rows)
            {
                CheckBox chk = (((CheckBox)li.Cells[0].FindControl("ckSelectAt")));
                if (chk.Checked)
                {
                    codAte = int.Parse(((HiddenField)li.Cells[0].FindControl("hidIdAtesMedic")).Value);
                }
            }

            string infos;
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            int lRetorno;
            int coAtendimento = int.Parse(hidIdAtendimentoMedico.Value);

            RptAtestadoMedico fpcb = new RptAtestadoMedico();
            lRetorno = fpcb.InitReport(infos, LoginAuxili.CO_EMP, coAtendimento, codAte);
            Session["Report"] = fpcb;
            Session["URLRelatorioAt"] = "/GeducReportViewer.aspx";

            AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorioAt"].ToString());
            //----------------> Limpa a var de sessão com o url do relatório.
            Session.Remove("URLRelatorioAt");

            //----------------> Limpa a ref da url utilizada para carregar o relatório.
            PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            isreadonly.SetValue(this.Request.QueryString, false, null);
            isreadonly.SetValue(this.Request.QueryString, true, null);
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método responsável por verificar se já existe um CID com os dados informados e salvar um novo caso não exista.
        /// </summary>
        /// <returns>Retorna o ID do cid já existente ou o salvo neste método, para fins de alimentar a tabela de atendimento</returns>
        private int? SalvaCID()
        {
            string CO_CID = txtCidAtendMed.Text.Trim();

            if ((!string.IsNullOrEmpty(txtCidAtendMed.Text)) && (!string.IsNullOrEmpty(txtDescCID.Text)))
            {
                var res = (from tb117 in TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros()
                           where tb117.CO_CID == CO_CID
                           select new { tb117.NO_CID, tb117.IDE_CID, tb117.CO_CID }).FirstOrDefault();

                //Salva o CID caso já não exista um registro com o código digitado
                if (res == null)
                {
                    TB117_CODIGO_INTERNACIONAL_DOENCA tb117 = new TB117_CODIGO_INTERNACIONAL_DOENCA();
                    tb117.CO_CID = txtCidAtendMed.Text;
                    tb117.NO_CID = txtDescCID.Text;
                    tb117 = TB117_CODIGO_INTERNACIONAL_DOENCA.SaveOrUpdate(tb117);
                    return tb117.IDE_CID;
                }
                else
                {
                    TB117_CODIGO_INTERNACIONAL_DOENCA tb117ob = TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaPelaChavePrimaria(res.IDE_CID);
                    tb117ob.NO_CID = txtDescCID.Text;
                    tb117ob = TB117_CODIGO_INTERNACIONAL_DOENCA.SaveOrUpdate(tb117ob);
                    return tb117ob.IDE_CID;
                }
            }
            return (int?)null;
        }

        /// <summary>
        /// Método responsável por verificar se já existe um CID com os dados informados e salvar um novo caso não exista.
        /// </summary>
        /// <returns>Retorna o ID do cid já existente ou o salvo neste método, para fins de alimentar a tabela de atendimento</returns>
        private int? SalvaCID2()
        {
            string CO_CID = txtCidAtendMed2.Text.Trim();

            if ((!string.IsNullOrEmpty(txtCidAtendMed2.Text)) && (!string.IsNullOrEmpty(txtDescCID2.Text)))
            {
                var res = (from tb117 in TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros()
                           where tb117.CO_CID == CO_CID
                           select new { tb117.NO_CID, tb117.IDE_CID, tb117.CO_CID }).FirstOrDefault();

                //Salva o CID caso já não exista um registro com o código digitado
                if (res == null)
                {
                    TB117_CODIGO_INTERNACIONAL_DOENCA tb117 = new TB117_CODIGO_INTERNACIONAL_DOENCA();
                    tb117.CO_CID = txtCidAtendMed2.Text;
                    tb117.NO_CID = txtDescCID2.Text;
                    tb117 = TB117_CODIGO_INTERNACIONAL_DOENCA.SaveOrUpdate(tb117);
                    return tb117.IDE_CID;
                }
                else
                {
                    TB117_CODIGO_INTERNACIONAL_DOENCA tb117ob = TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaPelaChavePrimaria(res.IDE_CID);
                    tb117ob.NO_CID = txtDescCID2.Text;
                    tb117ob = TB117_CODIGO_INTERNACIONAL_DOENCA.SaveOrUpdate(tb117ob);
                    return tb117ob.IDE_CID;
                }
            }
            return (int?)null;
        }

        /// <summary>
        /// Método responsável por verificar se já existe um CID com os dados informados e salvar um novo caso não exista.
        /// </summary>
        /// <returns>Retorna o ID do cid já existente ou o salvo neste método, para fins de alimentar a tabela de atendimento</returns>
        private int? SalvaCID3()
        {
            string CO_CID = txtCidAtendMed3.Text.Trim();

            if ((!string.IsNullOrEmpty(txtCidAtendMed3.Text)) && (!string.IsNullOrEmpty(txtDescCID3.Text)))
            {
                var res = (from tb117 in TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros()
                           where tb117.CO_CID == CO_CID
                           select new { tb117.NO_CID, tb117.IDE_CID, tb117.CO_CID }).FirstOrDefault();

                //Salva o CID caso já não exista um registro com o código digitado
                if (res == null)
                {
                    TB117_CODIGO_INTERNACIONAL_DOENCA tb117 = new TB117_CODIGO_INTERNACIONAL_DOENCA();
                    tb117.CO_CID = txtCidAtendMed3.Text;
                    tb117.NO_CID = txtDescCID3.Text;
                    tb117 = TB117_CODIGO_INTERNACIONAL_DOENCA.SaveOrUpdate(tb117);
                    return tb117.IDE_CID;
                }
                else
                {
                    TB117_CODIGO_INTERNACIONAL_DOENCA tb117ob = TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaPelaChavePrimaria(res.IDE_CID);
                    tb117ob.NO_CID = txtDescCID3.Text;
                    tb117ob = TB117_CODIGO_INTERNACIONAL_DOENCA.SaveOrUpdate(tb117ob);
                    return tb117ob.IDE_CID;
                }
            }
            return (int?)null;
        }

        /// <summary>
        /// Método responsável por salvar o atendimento e as informações de diagnóstico, CID e pré-atendimento relacionadas à ele.
        /// </summary>
        private bool SalvaAtendimento()
        {
            int? cid = SalvaCID();
            int? cid2 = SalvaCID2();
            int? cid3 = SalvaCID3();

            if (string.IsNullOrEmpty(hidCoPac.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Para efetivar o Atendimento Médico, é preciso selecionar o Paciente que deverá ser atendido");
                ControlaTabs("SLP", "ATM");
                chkAtdMedico.Checked = false;
                chkSelPacien.Checked = true;
                return false;
            }

            if ((string.IsNullOrEmpty(hidIdEncam.Value)) && (string.IsNullOrEmpty(hidCoConsul.Value)))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Para efetivar o Atendimento Médico, é preciso selecionar paciente que será atendido.");
                ControlaTabs("SLP", "ATM");
                chkAtdMedico.Checked = false;
                chkSelPacien.Checked = true;
                return false;
            }

            if (string.IsNullOrEmpty(hidCoProfSaud.Value))
            {
                ControlaTabs("ATM", "");
                AuxiliPagina.EnvioMensagemErro(this.Page, "Para efetivar o Atendimento Médico, é preciso selecionar o Médico que está fazendo o Atendimento.");
                return false;
            }

            if (string.IsNullOrEmpty(txtDataAtend.Text))
            {
                ControlaTabs("ATM", "");
                AuxiliPagina.EnvioMensagemErro(this.Page, "Para efetivar o Atendimento Médico, é preciso informar a Data do Atendimento Médico.");
                return false;
            }

            if (string.IsNullOrEmpty(txtDiagAtendMed.Text))
            {
                ControlaTabs("ATM", "");
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor preencher a informação do diagnóstico para efetivação do atendimento");
                return false;
            }

            if (cid == null)
            {
                ControlaTabs("ATM", "");
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor preencher as informações do CID");
                return false;
            }

            //if (string.IsNullOrEmpty(txtDtNascUsuAteMed.Text))
            //{
            //    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a Data de Nascimento do Paciente");
            //    return false;
            //}

            //if (string.IsNullOrEmpty(ddlClassRisco.SelectedValue))
            //{
            //    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o Tipo da Classificação de Risco");
            //    return false;
            //}

            ControlaTabs("ATM", "");

            int coCol = int.Parse(hidCoProfSaud.Value);
            int empCol = TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == coCol).FirstOrDefault().CO_EMP;

            //Salva as informações do Atendimento
            TBS219_ATEND_MEDIC tbs219 = (!string.IsNullOrEmpty(hidIdAtendimentoMedico.Value) ? TBS219_ATEND_MEDIC.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimentoMedico.Value)) : new TBS219_ATEND_MEDIC());
            tbs219.CO_ALU = int.Parse(hidCoPac.Value);
            tbs219.TB108_RESPONSAVEL = (!string.IsNullOrEmpty(hidCoResp.Value) ? TB108_RESPONSAVEL.RetornaPelaChavePrimaria(int.Parse(hidCoResp.Value)) : null);
            tbs219.CO_COL = coCol;
            tbs219.CO_EMP_COL = empCol;
            tbs219.CO_EMP = LoginAuxili.CO_EMP;
            tbs219.DT_ATEND_MEDIC = DateTime.Parse(txtDataAtend.Text);
            tbs219.IDE_CID = cid;
            tbs219.TB117_CODIGO_INTERNACIONAL_DOENCA = (cid2.HasValue ? TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaPelaChavePrimaria(cid2.Value) : null);
            tbs219.TB117_CODIGO_INTERNACIONAL_DOENCA1 = (cid3.HasValue ? TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaPelaChavePrimaria(cid3.Value) : null);
            tbs219.DE_DIAGN = txtDiagAtendMed.Text;
            tbs219.DE_ANAMN = txtAnamAtendMed.Text;
            tbs219.DT_ATEND_CADAS = DateTime.Now;
            tbs219.CO_COL_CADAS = LoginAuxili.CO_COL;
            tbs219.CO_EMP_CADAS = LoginAuxili.CO_EMP;
            tbs219.NR_IP_CADAS = Request.UserHostAddress;
            tbs219.TB250_OPERA = (!string.IsNullOrEmpty(hidIdOper.Value) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(hidIdOper.Value)) : null);
            tbs219.TB251_PLANO_OPERA = (!string.IsNullOrEmpty(hidIdPlano.Value) ? TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(hidIdPlano.Value)) : null);
            tbs219.CO_ESPECIALIDADE = (!string.IsNullOrEmpty(hidIdEspec.Value) ? int.Parse(hidIdEspec.Value) : (int?)null);

            //verifica se as variáveis de auxílio estão preenchidas e salva seus respectivos códigos
            if (!string.IsNullOrEmpty(hidIdEncam.Value))
            {
                tbs219.ID_ENCAM_MEDIC = int.Parse(hidIdEncam.Value);
                tbs219.CO_ENCAM_MEDIC = TBS195_ENCAM_MEDIC.RetornaPelaChavePrimaria(int.Parse(hidIdEncam.Value)).CO_ENCAM_MEDIC;
            }
            else if (!string.IsNullOrEmpty(hidCoConsul.Value))
            {
                tbs219.TBS174_AGEND_HORAR = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidCoConsul.Value));
                tbs219.NU_REGIS_CONSUL = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidCoConsul.Value)).NU_REGIS_CONSUL;
            }

            if (string.IsNullOrEmpty(hidIdAtendimentoMedico.Value))
            {
                #region Trata o sequencial

                string coUnidA = LoginAuxili.CO_UNID.ToString();
                int coEmpA = LoginAuxili.CO_EMP;
                string anoA = DateTime.Now.Year.ToString().Substring(2, 2);

                var res1 = (from tbs219pesq in TBS219_ATEND_MEDIC.RetornaTodosRegistros()
                            where tbs219pesq.CO_EMP == coEmpA && tbs219pesq.CO_ATEND_MEDIC != null
                            select new { tbs219pesq.CO_ATEND_MEDIC }).OrderByDescending(w => w.CO_ATEND_MEDIC).FirstOrDefault();

                string seq1;
                int seq21;
                int seqConcat1;
                string seqcon1;
                if (res1 == null)
                {
                    seq21 = 1;
                }
                else
                {
                    seq1 = res1.CO_ATEND_MEDIC.Substring(7, 7);
                    seq21 = int.Parse(seq1);
                }

                seqConcat1 = seq21 + 1;
                seqcon1 = seqConcat1.ToString().PadLeft(7, '0');

                tbs219.CO_ATEND_MEDIC = anoA + coUnidA.PadLeft(3, '0') + "AM" + seqcon1;

                #endregion
            }

            tbs219 = TBS219_ATEND_MEDIC.SaveOrUpdate(tbs219);

            //Salva as informações do Diagnóstico
            TBS334_DIAGN_ATEND_MEDIC tbs334 = (!string.IsNullOrEmpty(hidIdDiagnostico.Value) ? TBS334_DIAGN_ATEND_MEDIC.RetornaPelaChavePrimaria(int.Parse(hidIdDiagnostico.Value)) : new TBS334_DIAGN_ATEND_MEDIC());
            tbs334.CO_COL_MEDIC = coCol;
            tbs334.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(empCol);
            tbs334.TBS219_ATEND_MEDIC = tbs219;
            tbs334.CO_ATEND_MEDIC = tbs219.CO_ATEND_MEDIC;
            tbs334.TB117_CODIGO_INTERNACIONAL_DOENCA = TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaPelaChavePrimaria(cid.Value);
            tbs334.TB117_CODIGO_INTERNACIONAL_DOENCA1 = (cid2.HasValue ? TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaPelaChavePrimaria(cid2.Value) : null);
            tbs334.TB117_CODIGO_INTERNACIONAL_DOENCA2 = (cid3.HasValue ? TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaPelaChavePrimaria(cid3.Value) : null);
            tbs334.DT_DIAGN = DateTime.Now;
            tbs334.DE_DIAGN = txtDiagAtendMed.Text;

            if (string.IsNullOrEmpty(hidIdDiagnostico.Value))
            {
                #region Sequencial NR Registro

                string coUnid2 = LoginAuxili.CO_UNID.ToString();
                int coEmp2 = LoginAuxili.CO_EMP;
                string ano2 = DateTime.Now.Year.ToString().Substring(2, 2);

                var res2 = (from tbs334pesq in TBS334_DIAGN_ATEND_MEDIC.RetornaTodosRegistros()
                            where tbs334pesq.TB25_EMPRESA.CO_EMP == coEmp2 && tbs334pesq.NU_REGIS_DIAGN != null
                            select new { tbs334pesq.NU_REGIS_DIAGN }).OrderByDescending(w => w.NU_REGIS_DIAGN).FirstOrDefault();

                string seq2;
                int seq22;
                int seqConcat2;
                string seqcon2;
                if (res2 == null)
                {
                    seq22 = 1;
                }
                else
                {
                    seq2 = res2.NU_REGIS_DIAGN.Substring(7, 6);
                    seq22 = int.Parse(seq2);
                }

                seqConcat2 = seq22 + 1;
                seqcon2 = seqConcat2.ToString().PadLeft(6, '0');

                tbs334.NU_REGIS_DIAGN = "DG" + ano2 + coUnid2.PadLeft(3, '0') + seqcon2;

                #endregion
            }

            TBS334_DIAGN_ATEND_MEDIC.SaveOrUpdate(tbs334, true);

            //Atribui o Código do Atendimento à um Hidden que será usado para gravar registros relacionados à este atendimento
            hidIdAtendimentoMedico.Value = tbs219.ID_ATEND_MEDIC.ToString();
            hidIdDiagnostico.Value = tbs334.ID_DIAGN_ATEND_MEDIC.ToString();

            //Salva/Atualiza as informações do pré-atendimento
            //Verifica se existe um pré-atendimento associado ao encaminhamento que originou este atendimento, caso exista, instancia um novo objeto da entidade
            //que será usado para atualizar os dados que o médico alterar;
            //Caso não exista, instancia um novo objeto da entidade que será usado para persistir os dados que o médico informar, com a uma FLAG que indicará
            //que o pré-atendimento em questão foi feito dentro do atendimento, pelo médico.
            TBS194_PRE_ATEND tbs194;
            if (string.IsNullOrEmpty(hidCoPreAtend.Value))
            {
                int coRe = (!string.IsNullOrEmpty(hidCoResp.Value) ? int.Parse(hidCoResp.Value) : 0);
                int coPa = (!string.IsNullOrEmpty(hidCoPac.Value) ? int.Parse(hidCoPac.Value) : 0);
                string cpfPaci = (coRe != 0 ? TB108_RESPONSAVEL.RetornaPelaChavePrimaria(coRe).NU_CPF_RESP.Replace(".", "").Replace("-", "").Trim() : null);
                string cpfResp = (coPa != 0 ? TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == coPa).FirstOrDefault().NU_CPF_ALU : null);

                tbs194 = new TBS194_PRE_ATEND();

                tbs194.CO_EMP = LoginAuxili.CO_EMP;

                //Provisóriamente setando os campos obrigatórios "na mão"
                tbs194.CO_SEXO_USU = "M";
                tbs194.DT_NASC_USU = DateTime.Now;

                //tbs194.CO_TIPO_RISCO = int.Parse(hidCoTpRisco.Value);
                tbs194.NO_USU = txtPaciente.Text;
                tbs194.NU_CPF_RESP = (!string.IsNullOrEmpty(cpfResp) ? cpfResp : null);
                tbs194.NU_CPF_USU = (!string.IsNullOrEmpty(cpfPaci) ? cpfPaci : null);

                tbs194.CO_TIPO_RISCO = 5;
                tbs194.DT_PRE_ATEND = DateTime.Now;
                tbs194.CO_EMP_FUNC = empCol;
                tbs194.CO_COL_FUNC = int.Parse(hidCoProfSaud.Value);
                tbs194.CO_SITUA_PRE_ATEND = "A";
                tbs194.DT_SITUA_PRE_ATEND = DateTime.Now;

                //Leitura
                tbs194.NU_PESO = (!string.IsNullOrEmpty(txtPesoUsuPreAtend.Text) ? decimal.Parse(txtPesoUsuPreAtend.Text) : (decimal?)null);
                tbs194.NU_ALTU = (!string.IsNullOrEmpty(txtAltuUsuPreAtend.Text) ? decimal.Parse(txtAltuUsuPreAtend.Text) : (decimal?)null);
                tbs194.NU_PRES_ARTE = (!string.IsNullOrEmpty(txtPresUsuPreAtend.Text) ? txtPresUsuPreAtend.Text : null);
                tbs194.HR_PRES_ARTE = (!string.IsNullOrEmpty(txtHoraPressArt.Text) ? txtHoraPressArt.Text : null);
                tbs194.NU_TEMP = (!string.IsNullOrEmpty(txtTemper.Text) ? decimal.Parse(txtTemper.Text) : (decimal?)null);
                tbs194.HR_TEMP = (!string.IsNullOrEmpty(txtHoraTemper.Text) ? txtHoraTemper.Text : null);
                tbs194.NU_GLICE = (!string.IsNullOrEmpty(txtGlicem.Text) ? int.Parse(txtGlicem.Text) : (int?)null);
                tbs194.HR_GLICE = (!string.IsNullOrEmpty(txtHoraGlicem.Text) ? txtHoraGlicem.Text : null);

                //Registro de Risco
                tbs194.FL_DIABE = (chkDiabetsPreAtend.Checked ? "S" : "N");
                tbs194.DE_DIABE = (chkDiabetsPreAtend.Checked ? ddlDiabetsPreAtend.SelectedValue : null);
                tbs194.FL_HIPER_TENSO = (chkHipertPreAtend.Checked ? "S" : "N");
                tbs194.DE_HIPER_TENSO = (chkHipertPreAtend.Checked ? (!string.IsNullOrEmpty(txtHiperPreAtend.Text) ? txtHiperPreAtend.Text : null) : null);
                tbs194.FL_FUMAN = ddlFumanPreAtend.SelectedValue;
                tbs194.NU_TEMPO_FUMAN = (!string.IsNullOrEmpty(txtTempFumanPreAtend.Text) ? int.Parse(txtTempFumanPreAtend.Text) : (int?)null);
                tbs194.FL_ALCOO = ddlAlcooPreAtend.SelectedValue;
                tbs194.NU_TEMPO_ALCOO = (!string.IsNullOrEmpty(txtTempAlcooPreAtend.Text) ? int.Parse(txtTempAlcooPreAtend.Text) : (int?)null);
                tbs194.FL_CIRUR = (chkCirurPreAtend.Checked ? "S" : "N");
                tbs194.DE_CIRUR = (chkCirurPreAtend.Checked ? (!string.IsNullOrEmpty(txtCirurPreAtend.Text) ? txtCirurPreAtend.Text : null) : null);
                tbs194.FL_ALERG = (chkAlergia.Checked ? "S" : "N");
                tbs194.DE_ALERG = (chkAlergia.Checked ? (!string.IsNullOrEmpty(txtAlergia.Text) ? txtAlergia.Text : null) : null);
                tbs194.FL_MARCA_PASSO = (chkMarcPass.Checked ? "S" : "N");
                tbs194.DE_MARCA_PASSO = (chkMarcPass.Checked ? (!string.IsNullOrEmpty(txtMarcPass.Text) ? txtMarcPass.Text : null) : null);
                tbs194.FL_VALVU = (chkValvulas.Checked ? "S" : "N");
                tbs194.DE_VALVU = (chkValvulas.Checked ? (!string.IsNullOrEmpty(txtValvula.Text) ? txtValvula.Text : null) : null);
                tbs194.FL_SINTO_ENJOO = ddlEnjoo.SelectedValue;
                tbs194.FL_SINTO_VOMIT = ddlVomito.SelectedValue;
                tbs194.FL_SINTO_FEBRE = ddlFebre.SelectedValue;
                tbs194.FL_SINTO_DORES = ddlDores.SelectedValue;

                //Medicação
                tbs194.DE_MEDIC_USO_CONTI = (!string.IsNullOrEmpty(txtMedicUsoContiPreAtend.Text) ? txtMedicUsoContiPreAtend.Text : null);
                tbs194.DE_MEDIC = (!string.IsNullOrEmpty(txtMedicacaoAdmin.Text) ? txtMedicacaoAdmin.Text : null);

                //Informações Prévias
                tbs194.DE_SINTO = (!string.IsNullOrEmpty(txtCirurPreAtend.Text) ? txtCirurPreAtend.Text : null);

                //Gerais
                tbs194.FL_ATEND_MEDIC = "S";
                tbs194.NR_IP_FUNC = Request.UserHostAddress;
                tbs194.DE_MEDIC_USO_CONTI = (!string.IsNullOrEmpty(txtMedicUsoContiPreAtend.Text) ? txtMedicUsoContiPreAtend.Text : null);

                //Verifica se o atendimento é de uma consulta e guarda a flag como tal
                if (!string.IsNullOrEmpty(hidCoConsul.Value))
                    tbs194.FL_CONSUL_MEDIC = "S";

                //Trata e concatena o Código do Pré-Atendimento (Verifica qual o ultimo número do Pré-Atendimento cadastrado no banco, e acrescenta + 1 no registro atual, 
                //caso não haja registro ainda no banco ele inicia uma contagem do 1).
                #region Trata o sequencial

                string coUnid = LoginAuxili.CO_UNID.ToString();
                int coEmp = LoginAuxili.CO_EMP;
                string ano = DateTime.Now.Year.ToString().Substring(2, 2);

                var res = (from tbs194pesq in TBS194_PRE_ATEND.RetornaTodosRegistros()
                           where tbs194pesq.CO_EMP == coEmp
                           select new { tbs194pesq.CO_PRE_ATEND }).OrderByDescending(w => w.CO_PRE_ATEND).FirstOrDefault();

                string seq;
                int seq23;
                int seqConcat;
                string seqcon;
                if (res == null)
                {
                    seq23 = 1;
                }
                else
                {
                    seq = res.CO_PRE_ATEND.Substring(7, 7);
                    seq23 = int.Parse(seq);
                }

                seqConcat = seq23 + 1;
                seqcon = seqConcat.ToString().PadLeft(7, '0');

                tbs194.CO_PRE_ATEND = ano + coUnid.PadLeft(3, '0') + "PA" + seqcon;

                #endregion

                tbs194 = TBS194_PRE_ATEND.SaveOrUpdate(tbs194);
                tbs219.TBS194_PRE_ATEND = TBS194_PRE_ATEND.RetornaPelaChavePrimaria(tbs194.ID_PRE_ATEND);

                //Atribui à uma hidden o id do pre atendimento para o caso de se desejar fazer alguma alteração
                hidCoPreAtend.Value = tbs194.ID_PRE_ATEND.ToString();

                //Se estiver com o código do encaminhamento nulo é porque o atendimento é originado de uma consulta
                if (!string.IsNullOrEmpty(hidIdEncam.Value))
                {
                    int coEmc = int.Parse(hidIdEncam.Value);

                    //Atualiza o cadastro do encaminhamento médico, inserindo a informação do ID do pré-atendimento.
                    TBS195_ENCAM_MEDIC tbs195 = TBS195_ENCAM_MEDIC.RetornaPelaChavePrimaria(coEmc);
                    tbs195.ID_PRE_ATEND = tbs194.ID_PRE_ATEND;
                    TBS195_ENCAM_MEDIC.SaveOrUpdate(tbs195);
                }
            }
            else
            {
                int coPre = int.Parse(hidCoPreAtend.Value);
                //Instancia um objeto da entidade do pré-atendimento relacionado ao encaminhamento selecionado.
                tbs194 = TBS194_PRE_ATEND.RetornaPelaChavePrimaria(coPre);

                //Seta provisoriamente "na mão" as informações
                tbs194.CO_SEXO_USU = "M";
                tbs194.DT_NASC_USU = DateTime.Now;

                //Leitura
                tbs194.NU_ALTU = (!string.IsNullOrEmpty(txtAltuUsuPreAtend.Text) ? decimal.Parse(txtAltuUsuPreAtend.Text) : (decimal?)null);
                tbs194.NU_PESO = (!string.IsNullOrEmpty(txtPesoUsuPreAtend.Text) ? decimal.Parse(txtPesoUsuPreAtend.Text) : (decimal?)null);
                tbs194.NU_PRES_ARTE = (!string.IsNullOrEmpty(txtPresUsuPreAtend.Text) ? txtPresUsuPreAtend.Text : null);
                tbs194.HR_PRES_ARTE = (!string.IsNullOrEmpty(txtHoraPressArt.Text) ? txtHoraPressArt.Text : null);
                tbs194.NU_TEMP = (!string.IsNullOrEmpty(txtTemper.Text) ? decimal.Parse(txtTemper.Text) : (decimal?)null);
                tbs194.HR_TEMP = (!string.IsNullOrEmpty(txtHoraTemper.Text) ? txtHoraTemper.Text : null);
                tbs194.NU_GLICE = (!string.IsNullOrEmpty(txtGlicem.Text) ? int.Parse(txtGlicem.Text) : (int?)null);
                tbs194.HR_GLICE = (!string.IsNullOrEmpty(txtHoraGlicem.Text) ? txtHoraGlicem.Text : null);

                //Registro de Risco
                tbs194.FL_DIABE = (chkDiabetsPreAtend.Checked ? "S" : "N");
                tbs194.DE_DIABE = (chkDiabetsPreAtend.Checked ? ddlDiabetsPreAtend.SelectedValue : null);
                tbs194.FL_HIPER_TENSO = (chkHipertPreAtend.Checked ? "S" : "N");
                tbs194.DE_HIPER_TENSO = (chkHipertPreAtend.Checked ? txtHiperPreAtend.Text : null);
                tbs194.FL_FUMAN = ddlFumanPreAtend.SelectedValue;
                tbs194.NU_TEMPO_FUMAN = (!string.IsNullOrEmpty(txtTempFumanPreAtend.Text) ? int.Parse(txtTempFumanPreAtend.Text) : (int?)null);
                tbs194.FL_ALCOO = ddlAlcooPreAtend.SelectedValue;
                tbs194.NU_TEMPO_ALCOO = (!string.IsNullOrEmpty(txtTempAlcooPreAtend.Text) ? int.Parse(txtTempAlcooPreAtend.Text) : (int?)null);
                tbs194.FL_CIRUR = (chkCirurPreAtend.Checked ? "S" : "N");
                tbs194.DE_CIRUR = (chkCirurPreAtend.Checked ? txtCirurPreAtend.Text : null);
                tbs194.FL_ALERG = (chkAlergia.Checked ? "S" : "N");
                tbs194.DE_ALERG = (chkAlergia.Checked ? txtAlergia.Text : null);
                tbs194.FL_MARCA_PASSO = (chkMarcPass.Checked ? "S" : "N");
                tbs194.DE_MARCA_PASSO = (chkMarcPass.Checked ? txtMarcPass.Text : null);
                tbs194.FL_VALVU = (chkValvulas.Checked ? "S" : "N");
                tbs194.DE_VALVU = (chkValvulas.Checked ? txtValvula.Text : null);
                tbs194.FL_SINTO_ENJOO = ddlEnjoo.SelectedValue;
                tbs194.FL_SINTO_VOMIT = ddlVomito.SelectedValue;
                tbs194.FL_SINTO_FEBRE = ddlFebre.SelectedValue;
                tbs194.FL_SINTO_DORES = ddlDores.SelectedValue;

                //Medicação
                tbs194.DE_MEDIC_USO_CONTI = (!string.IsNullOrEmpty(txtMedicUsoContiPreAtend.Text) ? txtMedicUsoContiPreAtend.Text : null);
                tbs194.DE_MEDIC = (!string.IsNullOrEmpty(txtMedicacaoAdmin.Text) ? txtMedicacaoAdmin.Text : null);

                //Informações Prévias
                tbs194.DE_SINTO = txtCirurPreAtend.Text;

                TBS194_PRE_ATEND.SaveOrUpdate(tbs194, true);
            }

            //Atualiza o atendimento definindo o id da avaliação da tbs194
            tbs219.TBS194_PRE_ATEND = tbs194;
            tbs219 = TBS219_ATEND_MEDIC.SaveOrUpdate(tbs219);

            AtualizaPreAtendAtend();

            ControlaTabs("RCM", "ATM");
            chkReqMedicPaci.Checked = true;
            chkAtdMedico.Checked = false;
            lblAtdMedico.Visible = true;
            CarregaCheckLancamentos(true);
            chkSelPacien.Enabled = false;
            CarregaGridHistoricDiagnostic(int.Parse(hidCoPac.Value));

            //Desabilita o botão de efetivação do atendimento médico para evitar duplicatas
            //lnkEfetAtendMed.Enabled = false;
            lnkFinAtendMedic.Enabled = true;
            return true;
        }

        /// <summary>
        /// Salva um registro na central de regulação, caso o serviço que esteja sendo solicitado requeira autorização
        /// </summary>
        /// <param name="ID_ITEM">CHAVE PRIMÁRIA DO ITEM À SER AUTORIZADO</param>
        /// <param name="CO_SIGLA">Sigla do Item que será salvo (Exame: "EX" ; Serviço Ambulatorial: "SA"; Reserva Medicamentos: "RM")</param>
        private void SalvaRegistroCentralRegulacao(int ID_ITEM, string CO_SIGLA, string NU_REGIS, string NO_ITEM)
        {
            int ID_ATEND_MEDIC = int.Parse(hidIdAtendimentoMedico.Value);

            TBS347_CENTR_REGUL res = (from tbs347ob in TBS347_CENTR_REGUL.RetornaTodosRegistros()
                                      where tbs347ob.TBS219_ATEND_MEDIC.ID_ATEND_MEDIC == ID_ATEND_MEDIC
                                      select tbs347ob).FirstOrDefault();

            TBS347_CENTR_REGUL tbs347 = res;
            TBS347_CENTR_REGUL tbs347JaExistia = null;
            string CO_ATEND = TBS219_ATEND_MEDIC.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimentoMedico.Value)).CO_ATEND_MEDIC;
            //Somente salva registro da central de regulação caso ainda não haja um registro para o atendimento em contexto
            if (res == null)
            {
                tbs347 = new TBS347_CENTR_REGUL();
                tbs347.TBS219_ATEND_MEDIC = TBS219_ATEND_MEDIC.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimentoMedico.Value));
                tbs347.CO_ATEND_MEDIC = CO_ATEND;
                tbs347.FL_APROV_ENCAM = "A";
                tbs347.FL_USO = "N";
                tbs347.CO_COL_SOLIC = LoginAuxili.CO_COL;
                tbs347.CO_EMP_SOLIC = LoginAuxili.CO_EMP;
                tbs347.DT_SOLIC_ENCAM = DateTime.Now;
                tbs347.IP_SOLIC_ENCAM = Request.UserHostAddress;
                TBS347_CENTR_REGUL.SaveOrUpdate(tbs347);
                tbs347JaExistia = tbs347;
            }

            //Salva o item da central de regulação
            TBS350_ITEM_CENTR_REGUL tbs350 = new TBS350_ITEM_CENTR_REGUL();
            tbs350.TBS347_CENTR_REGUL = (res != null ? res : tbs347JaExistia);
            tbs350.ID_ITEM_ENCAM = ID_ITEM;
            tbs350.CO_SIGLA_ITEM_ENCAM = CO_SIGLA;
            tbs350.NU_REGIS_ITEM = NU_REGIS;
            tbs350.NO_ITEM = NO_ITEM;
            tbs350.CO_ATEND_MEDIC = CO_ATEND;
            tbs350.DT_SOLIC_ENCAM = DateTime.Now;
            tbs350.IP_SOLIC_ENCAM = Request.UserHostAddress;
            tbs350.FL_APROV_ENCAM = "A";
            tbs350.CO_COL_SOLIC = LoginAuxili.CO_COL;
            tbs350.CO_EMP_SOLIC = LoginAuxili.CO_EMP;
            TBS350_ITEM_CENTR_REGUL.SaveOrUpdate(tbs350, true);
        }

        /// <summary>
        /// Grava na tabela de financeiro de procedimentos os devidos dados
        /// </summary>
        private void GravaFinanceiroProcedimentos(TBS356_PROC_MEDIC_PROCE tbs356, int CO_ALU, int CO_RESP)
        {
            int idOper = CarregaOperaAtendimento(int.Parse(hidIdAtendimentoMedico.Value));
            int cocol = int.Parse(hidCoProfSaud.Value);
            var re = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                      where tb03.CO_COL == cocol
                      select new { tb03.CO_EMP }).FirstOrDefault();

            TBS357_PROC_MEDIC_FINAN tbs357 = new TBS357_PROC_MEDIC_FINAN();

            int ID_PLAN = (!string.IsNullOrEmpty(hidIdPlano.Value) ? int.Parse(hidIdPlano.Value) : 0);
            int ID_CONDI_PLANO_SAUDE = 0;
            int ID_VALOR_PROC_MEDIC_PROCE = 0;
            decimal VL_CUSTO = 0;
            decimal? VL_RESTI = 0;
            decimal? VL_BASE = 0;
            decimal valorTotal = 0;
            decimal valorDesconto = 0;

            //Faz os devidos tratamentos para descobrir o valor corrente do procedimento para determinado plano de saúde (Quando esta for a situação)
            #region Tratamentos

            #region Valor do Procedimento
            //Descobre o valor corrente do procedimento, caso o atendimento não esteja sendo feito por plano de saúde,
            //as informações de valores vão ser salvas apenas de acordo com o valor do procedimento em si
            var vlProc = (from tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                          where tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == tbs356.ID_PROC_MEDI_PROCE
                          && tbs353.FL_STATU == "A"
                          select new
                          {
                              tbs353.ID_VALOR_PROC_MEDIC_PROCE,
                              tbs353.VL_BASE,
                              tbs353.VL_CUSTO,
                              tbs353.VL_RESTI,
                          }).FirstOrDefault();

            if (vlProc != null) //Se não houver valor associado ao procedimento, ele fica 0
            {
                ID_VALOR_PROC_MEDIC_PROCE = vlProc.ID_VALOR_PROC_MEDIC_PROCE;
                VL_CUSTO = vlProc.VL_CUSTO;
                valorTotal = vlProc.VL_BASE;
                VL_RESTI = vlProc.VL_RESTI;
                VL_BASE = vlProc.VL_BASE;
            }
            #endregion

            //Apenas se vier de um plano de saúde
            if (ID_PLAN != 0)
            {
                //Instancia um objeto com as condições atuais do procedimento em contexto para o plano de saúde em contexto
                var res = (from tbs361 in TBS361_CONDI_PLANO_SAUDE.RetornaTodosRegistros()
                           join tbs362 in TBS362_ASSOC_PLANO_PROCE.RetornaTodosRegistros() on tbs361.TBS362_ASSOC_PLANO_PROCE.ID_ASSOC_PLANO_PROCE equals tbs362.ID_ASSOC_PLANO_PROCE
                           where tbs362.TB251_PLANO_OPERA.ID_PLAN == ID_PLAN
                           && tbs362.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == tbs356.ID_PROC_MEDI_PROCE
                           && tbs362.FL_STATU == "A"
                           select new
                           {
                               tbs361.ID_CONDI_PLANO_SAUDE,
                               tbs361.CO_REFER_TIPO,
                               tbs361.VL_CONTE_REFER,
                           }).FirstOrDefault();

                if (res != null) //Se houver associação entre o plano de saúde e o procedimento
                {
                    //Descobre o valor corrente do procedimento
                    var valorProce = (from tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                                      where tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == tbs356.ID_PROC_MEDI_PROCE
                                      && tbs353.FL_STATU == "A"
                                      select new
                                      {
                                          tbs353.ID_VALOR_PROC_MEDIC_PROCE,
                                          tbs353.VL_BASE,
                                          tbs353.VL_CUSTO,
                                          tbs353.VL_RESTI,
                                      }).FirstOrDefault();

                    if (valorProce != null) //Se não houver valor associado ao procedimento, ele fica 0
                    {
                        ID_CONDI_PLANO_SAUDE = res.ID_CONDI_PLANO_SAUDE;
                        ID_VALOR_PROC_MEDIC_PROCE = valorProce.ID_VALOR_PROC_MEDIC_PROCE;
                        VL_CUSTO = valorProce.VL_CUSTO;
                        VL_RESTI = valorProce.VL_RESTI;
                        VL_BASE = valorProce.VL_BASE;

                        //Calcula o valor à ser cobrado de acordo com o valor total do procedimento e as condições 
                        //de cálculo deste procedimento para o plano de saúde em contexto
                        if (res.CO_REFER_TIPO == "V")
                        {
                            //Calcula o valor, sendo que se o valor de abatimento for maior que o valor base do procedimento
                            //o valor resultante fica 0, pois não pode ser negativo, do contrário, é subtraído o valor de abatimento
                            //do valor base do procedimento
                            if(valorProce.VL_BASE < res.VL_CONTE_REFER)
                            {
                                valorTotal = 0;
                                valorDesconto = valorProce.VL_BASE;
                            }
                            else
                            {
                                valorTotal = valorProce.VL_BASE - res.VL_CONTE_REFER;
                                valorDesconto = res.VL_CONTE_REFER;
                            }
                        }
                        else // Calcula o desconto em porcentagem
                        {
                            double percentual = (double)res.VL_CONTE_REFER / 100; // calcula porcentagem
                            double valorFinal = (double)valorProce.VL_BASE - (percentual * (double)valorProce.VL_BASE);
                            valorTotal = (decimal)valorFinal;
                            valorDesconto = (decimal)(percentual * (double)valorProce.VL_BASE);
                        }
                    }
                }
            }

            #endregion

            tbs357.TBS219_ATEND_MEDIC = TBS219_ATEND_MEDIC.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimentoMedico.Value));
            tbs357.TB250_OPERA = (idOper != 0 ? TB250_OPERA.RetornaPelaChavePrimaria(idOper) : null);
            tbs357.CO_COL_INCLU_LANC = LoginAuxili.CO_COL;
            tbs357.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            tbs357.IP_INCLU_LANC = Request.UserHostAddress;
            tbs357.CO_COL_PROFI_ATEND = cocol;
            tbs357.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(re.CO_EMP);
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
            tbs357.CO_ORIGEM = "A"; //Determina que a origem desse registro financeiro é um Atendimento Médico
            tbs357.CO_ALU = CO_ALU;
            tbs357.CO_RESP = CO_RESP;
            //tbs357.DT_EVENT = DateTime.Now;

            tbs357.VL_CUSTO_PROC = VL_CUSTO;
            tbs357.VL_RESTI = VL_RESTI;
            tbs357.VL_BASE = VL_BASE;
            tbs357.VL_PROCE_LIQUI = valorTotal;
            tbs357.VL_DSCTO = valorDesconto;
            tbs357.TBS353_VALOR_PROC_MEDIC_PROCE = (ID_VALOR_PROC_MEDIC_PROCE != 0 ? TBS353_VALOR_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(ID_VALOR_PROC_MEDIC_PROCE) : null);
            tbs357.TBS361_CONDI_PLANO_SAUDE = (ID_CONDI_PLANO_SAUDE != 0 ? TBS361_CONDI_PLANO_SAUDE.RetornaPelaChavePrimaria(ID_CONDI_PLANO_SAUDE) : null);

            TBS357_PROC_MEDIC_FINAN.SaveOrUpdate(tbs357, true);
        }

        #endregion

        #region Carregamento

        //Agrupamento dos métodos responsáveis pelo correto carregamento de todas as grides da funcionalidade de Atendimento.
        #region Carregamento de Grides

        /// <summary>
        /// Carrega a grid inicial de consultas agendadas para a data existente no campo superior esquerdo (geralmente data atual) e médico seleciondo.
        /// </summary>
        private void CarregaGridConsultas(int coCol = 0)
        {
            DateTime data = DateTime.Parse(txtDataAtend.Text);
            //int col = ddlMedico.SelectedValue != "" ? int.Parse(ddlMedico.SelectedValue) : 0;

            var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                       join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tbs174.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                       where (coCol != 0 ? tbs174.CO_COL == coCol : 0 == 0)
                       && ( tbs174.CO_SITUA_AGEND_HORAR == "A" ||  tbs174.CO_SITUA_AGEND_HORAR == "E" )
                       && (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) == EntityFunctions.TruncateTime(data))
                       select new AgendaDeConsultasMedicas
                       {
                           CO_ALU = tb07.CO_ALU,
                           CO_COL = tbs174.CO_COL,
                           CO_AGEND_MEDIC = tbs174.ID_AGEND_HORAR,
                           CO_SEXO = tb07.CO_SEXO_ALU,
                           dt_nascimento = tb07.DT_NASC_ALU,
                           NO_PAC_RECEB = tb07.NO_ALU,
                           dt_Consul = tbs174.DT_AGEND_HORAR,
                           hr_Consul = tbs174.HR_AGEND_HORAR,
                           CO_SITU = tbs174.CO_SITUA_AGEND_HORAR,
                           FL_CONF = tbs174.FL_CONF_AGEND,
                           TP_CONSUL = tbs174.TP_CONSU,
                           NO_ESPEC = tb63.NO_ESPECIALIDADE,
                           
                       }).OrderByDescending(w => w.dt_Consul).ThenBy(y => y.hr_Consul).ToList();

            //Faz uma lista com AS 10 últimos Consultas e insere na lista anterior
            #region 10 últimos acolhimentos
            var resAntigos = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                              join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                              join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tbs174.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                              where (coCol != 0 ? tbs174.CO_COL == coCol : 0 == 0)
                              && tbs174.CO_SITUA_AGEND_HORAR == "A"
                              && (EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) < EntityFunctions.TruncateTime(data))
                              select new AgendaDeConsultasMedicas
                              {
                                  CO_ALU = tb07.CO_ALU,
                                  CO_COL = tbs174.CO_COL,
                                  CO_AGEND_MEDIC = tbs174.ID_AGEND_HORAR,
                                  CO_SEXO = tb07.CO_SEXO_ALU,
                                  dt_nascimento = tb07.DT_NASC_ALU,
                                  NO_PAC_RECEB = tb07.NO_ALU,
                                  dt_Consul = tbs174.DT_AGEND_HORAR,
                                  hr_Consul = tbs174.HR_AGEND_HORAR,
                                  CO_SITU = tbs174.CO_SITUA_AGEND_HORAR,
                                  FL_CONF = tbs174.FL_CONF_AGEND,
                                  TP_CONSUL = tbs174.TP_CONSU,
                                  NO_ESPEC = tb63.NO_ESPECIALIDADE,
                                  ANTIGOS = 1,
                              }).OrderByDescending(w => w.dt_Consul).ThenBy(y => y.hr_Consul).Take(10).ToList();

            foreach (var i in resAntigos) // passa por cada item da segunda lista e insere na primeira
            {
                res.Add(i);
            }

            //Reordena os itens
            res = res.OrderByDescending(w => w.dt_Consul).ThenBy(q => q.hr_Consul).ToList();
            #endregion

            grdAgendConsulMedic.DataSource = res;
            grdAgendConsulMedic.DataBind();

            VerificaConsultaAtrasada();
        }

        /// <summary>
        /// Carrega a grid de encaminhamentos médicos de acordo com o médico selecionado no combobox
        /// </summary>
        private void CarregaGridEncaminhamentos(int coCol = 0)
        {
            DateTime dtIni = DateTime.Now.AddDays(-1);
            TimeSpan ts = new TimeSpan(18, 0, 0);
            dtIni = dtIni.Date + ts;

            //int coCol = ddlMedico.SelectedValue != "" ? int.Parse(ddlMedico.SelectedValue) : 0;
            var res = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                       join tb250 in TB250_OPERA.RetornaTodosRegistros() on tbs195.ID_OPER equals tb250.ID_OPER into l1
                       from loper in l1.DefaultIfEmpty()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs195.CO_ALU equals tb07.CO_ALU
                       join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tbs195.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                       where tbs195.CO_SITUA_ENCAM_MEDIC == "A"
                       && (coCol != 0 ? tbs195.CO_COL == coCol : 0 == 0)
                       && (tbs195.DT_CADAS_ENCAM >= dtIni)
                       && (tbs195.TBS174_AGEND_HORAR == null) // NÃO MOSTRA DIRECIONAMENTOS DE CONSULTAS
                       select new EncaminhamentosMedicos
                       {
                           CO_TIPO_RISCO = tbs195.NR_CLASS_RISCO,
                           NO_PAC_RECEB = tb07.NO_ALU,
                           ID_ENCAM_MEDIC = tbs195.ID_ENCAM_MEDIC,
                           NO_ESPEC = tb63.NO_ESPECIALIDADE,
                           CO_ESPEC = tb63.CO_ESPECIALIDADE,
                           CO_ENCAM_MEDIC = tbs195.CO_ENCAM_MEDIC,
                           dt_nascimento = tb07.DT_NASC_ALU,
                           ID_PRE_ATEND = tbs195.ID_PRE_ATEND,
                           CO_SEXO = tb07.CO_SEXO_ALU,
                           dataEncamMed = tbs195.DT_CADAS_ENCAM,
                           CO_ALU = tbs195.CO_ALU,
                           CO_RESP = tbs195.CO_RESP,
                           CO_COL = tbs195.CO_COL,
                           NM_OPER = loper.NOM_OPER,
                           ID_OPER = loper.ID_OPER,
                           ID_PLAN = tbs195.ID_PLAN,
                       }).OrderBy(w => w.CO_TIPO_RISCO).ThenByDescending(w => w.dataEncamMed).ToList();

            //Faz uma lista com os 10 últimos acolhimentos e insere na lista anterior
            #region 10 últimos acolhimentos
            var resAntigos = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                       join tb250 in TB250_OPERA.RetornaTodosRegistros() on tbs195.ID_OPER equals tb250.ID_OPER into l1
                       from loper in l1.DefaultIfEmpty()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs195.CO_ALU equals tb07.CO_ALU
                       join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tbs195.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                       where tbs195.CO_SITUA_ENCAM_MEDIC == "A"
                       && (coCol != 0 ? tbs195.CO_COL == coCol : 0 == 0)
                       && (tbs195.DT_CADAS_ENCAM < dtIni)

                       select new EncaminhamentosMedicos
                       {
                           CO_TIPO_RISCO = tbs195.NR_CLASS_RISCO,
                           NO_PAC_RECEB = tb07.NO_ALU,
                           ID_ENCAM_MEDIC = tbs195.ID_ENCAM_MEDIC,
                           NO_ESPEC = tb63.NO_ESPECIALIDADE,
                           CO_ESPEC = tb63.CO_ESPECIALIDADE,
                           CO_ENCAM_MEDIC = tbs195.CO_ENCAM_MEDIC,
                           dt_nascimento = tb07.DT_NASC_ALU,
                           ID_PRE_ATEND = tbs195.ID_PRE_ATEND,
                           CO_SEXO = tb07.CO_SEXO_ALU,
                           dataEncamMed = tbs195.DT_CADAS_ENCAM,
                           CO_ALU = tbs195.CO_ALU,
                           CO_RESP = tbs195.CO_RESP,
                           CO_COL = tbs195.CO_COL,
                           NM_OPER = loper.NOM_OPER,
                           ID_OPER = loper.ID_OPER,
                           ID_PLAN = tbs195.ID_PLAN,
                           ANTIGO = 1,
                       }).OrderByDescending(w => w.dataEncamMed).Take(10).ToList();

            foreach (var i in resAntigos) // passa por cada item da segunda lista e insere na primeira
            {
                res.Add(i);
            }

            //Reordena os itens
            res = res.OrderBy(w => w.CO_TIPO_RISCO).ThenByDescending(q => q.dataEncamMed).ToList();

            #endregion

            grdEncamMedic.DataSource = res;
            grdEncamMedic.DataBind();
        }

        /// <summary>
        /// Método responsável por carregar as requisições de exames relacionados à um determinado atendimento médico
        /// </summary>
        /// <param name="ID_ATEND_MEDIC"></param>
        private void CarregaReqExamesMedicos(int ID_ATEND_MEDIC)
        {
            var res = (from tbs218 in TBS218_EXAME_MEDICO.RetornaTodosRegistros()
                       where tbs218.ID_ATEND_MEDIC == ID_ATEND_MEDIC
                       select new RequiExamesMedicos
                       {
                           exame = tbs218.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                           unidade = (tbs218.TB25_EMPRESA != null ? tbs218.TB25_EMPRESA.NO_FANTAS_EMP : "*Fora da Instituição*"),
                           local = (tbs218.TB14_DEPTO != null ? tbs218.TB14_DEPTO.NO_DEPTO : "*Fora da Instituição*"),
                           ID_EXAME = tbs218.ID_EXAME,
                           dt = tbs218.DT_EXAME,
                       }).OrderByDescending(w => w.dt).ThenBy(w => w.exame).ToList();

            grdExaAtendMed.DataSource = res;
            grdExaAtendMed.DataBind();
        }

        /// <summary>
        /// Carrega o receituário de acordo com o atendimento recebido como parâmetro na grid recebida como parâmetro
        /// </summary>
        /// <param name="ID_ATEND_MEDIC"></param>
        /// <param name="grid"></param>
        private void CarregaReceitasMedicas(int ID_ATEND_MEDIC, GridView grid)
        {
            var res = (from tbs330 in TBS330_RECEI_ATEND_MEDIC.RetornaTodosRegistros()
                       join tb90 in TB90_PRODUTO.RetornaTodosRegistros() on tbs330.CO_MEDIC equals tb90.CO_PROD
                       where tbs330.ID_ATEND_MEDIC == ID_ATEND_MEDIC
                       select new Receituario
                       {
                           medicamento = tb90.NO_PROD,
                           qtd = tbs330.QT_MEDIC,
                           uso = tbs330.QT_USO,
                           prescricao = tbs330.DE_PRESC,
                           ID_RECEI = tbs330.ID_RECEI_ATEND_MEDIC,
                           CO_PROD = tbs330.CO_MEDIC,
                           dt = tbs330.DT_RECEI,
                           FP = tb90.FL_FARM_POP,
                       }).OrderByDescending(w => w.dt).ThenBy(w => w.medicamento).ToList();

            grid.DataSource = res;
            grid.DataBind();

            //Só executa se for a grid da reserva de medicamentos
            if (grid == this.grdMedicReceitados)
                TrataMedicamentosFaltaGrid();
        }

        /// <summary>
        /// Carrega as requisições de serviços ambulatoriais de acordo com o Código do Atendimento recebido como parâmetro
        /// </summary>
        /// <param name="ID_ATEND_MEDIC"></param>
        private void CarregaGridRequiAmbu(int ID_ATEND_MEDIC)
        {
            var res = (from tbs332 in TBS332_ATEND_SERV_AMBUL.RetornaTodosRegistros()
                       where tbs332.ID_ATEND_MEDIC == ID_ATEND_MEDIC
                       select new RequiServicoEmbula
                       {
                           ID_ATEND_SERV_AMBUL = tbs332.ID_ATEND_SERV_AMBUL,
                           aplicacao = tbs332.TP_APLIC,
                           tipo = tbs332.TP_SERVI,
                           servico = tbs332.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                           dt = tbs332.DT_SERVI_AMBUL,
                           CO_REGIS = tbs332.NU_REGIS_SERVI_AMBUL,
                       }).OrderByDescending(w => w.dt).ThenBy(w => w.servico).ToList();

            grdServAmbu.DataSource = res;
            grdServAmbu.DataBind();
        }

        /// <summary>
        /// Carrega os Atestados Médicos de acordo com o Código do Atendimento recebido como parâmetro
        /// </summary>
        /// <param name="ID_ATEND_MEDIC"></param>
        private void CarregaGridAtestMedic(int ID_ATEND_MEDIC)
        {
            var res = (from tbs333 in TBS333_ATEST_MEDIC_PACIE.RetornaTodosRegistros()
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs333.CO_EMP equals tb25.CO_EMP
                       join tb009 in TB009_RTF_DOCTOS.RetornaTodosRegistros() on tbs333.ID_DOCUM equals tb009.ID_DOCUM
                       join tb117 in TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros() on tbs333.IDE_CID equals tb117.IDE_CID
                       where tbs333.ID_ATEND_MEDIC == ID_ATEND_MEDIC
                       select new RegistroAtestMedic
                       {
                           UNIDADE = tb25.NO_FANTAS_EMP,
                           ATESTADO = tb009.NM_DOCUM,
                           DATA_receb = tbs333.DT_ATEST_MEDIC,
                           ID_ATESTADO = tbs333.ID_ATEST_MEDIC_PACIE,
                           DIASREP = tbs333.QT_DIAS,
                           CID = tb117.NO_CID,
                       }).OrderByDescending(w => w.DATA_receb).ThenBy(w => w.ATESTADO).ToList();

            grdAtesMedic.DataSource = res;
            grdAtesMedic.DataBind();
        }

        /// <summary>
        /// Carrega a grid de medicamentos reservados de acordo com o id do atendimento medico recebido
        /// </summary>
        /// <param name="ID_ATEND_MEDIC"></param>
        private void CarregaReservaMedicamentos(int ID_ATEND_MEDIC)
        {
            var res = (from tb92 in TB092_RESER_MEDIC.RetornaTodosRegistros()
                       join tb94 in TB094_ITEM_RESER_MEDIC.RetornaTodosRegistros() on tb92.ID_RESER_MEDIC equals tb94.TB092_RESER_MEDIC.ID_RESER_MEDIC
                       join tb90 in TB90_PRODUTO.RetornaTodosRegistros() on tb94.TB90_PRODUTO.CO_PROD equals tb90.CO_PROD
                       join tb260 in TB260_GRUPO.RetornaTodosRegistros() on tb90.TB260_GRUPO.ID_GRUPO equals tb260.ID_GRUPO into l1
                       from ls in l1.DefaultIfEmpty()
                       where tb92.TBS219_ATEND_MEDIC.ID_ATEND_MEDIC == ID_ATEND_MEDIC
                       select new ReservaMedicamentos
                       {
                           Medicamento = tb90.NO_PROD,
                           GRUPO = ls.NOM_GRUPO,
                           QTM1 = tb94.QT_MES1,
                           QTM2 = tb94.QT_MES2,
                           QTM3 = tb94.QT_MES2,
                           QTM4 = tb94.QT_MES2,
                           dt = tb92.DT_RESER_MEDIC,
                           CO_PROD = tb90.CO_PROD,
                       }).OrderBy(w => w.dt).ToList();

            grdMedicamentos.DataSource = res;
            grdMedicamentos.DataBind();
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
            int unidade = (ddlUnidHistServAmbu.SelectedValue != "" ? int.Parse(ddlUnidHistServAmbu.SelectedValue) : 0);
            int servico = (ddlTpServAmbuHistorico.SelectedValue != "" ? int.Parse(ddlTpServAmbuHistorico.SelectedValue) : 0);
            string tpServ = ddlHistoTipoServ.SelectedValue;
            var res = (from tbs332 in TBS332_ATEND_SERV_AMBUL.RetornaTodosRegistros()
                       join tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros() on tbs332.ID_ATEND_MEDIC equals tbs219.ID_ATEND_MEDIC
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
                           servico = tbs332.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
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
            int unidade = (ddlUnidHistAtestMedic.SelectedValue != "" ? int.Parse(ddlUnidHistAtestMedic.SelectedValue) : 0);
            int tipo = (ddlTipoAtestMedic.SelectedValue != "" ? int.Parse(ddlTipoAtestMedic.SelectedValue) : 0);
            int cid = (ddlCIDHistAtestados.SelectedValue != "" ? int.Parse(ddlCIDHistAtestados.SelectedValue) : 0);

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
                           unidade = (tbs218.TB25_EMPRESA != null ? tbs218.TB25_EMPRESA.NO_FANTAS_EMP : "*Fora da Instituição*"),
                           local = (tbs218.TB14_DEPTO != null ? tbs218.TB14_DEPTO.NO_DEPTO : "*Fora da Instituição*"),
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

        /// <summary>
        /// Carrega a lista de exames médicos disponíveis de acordo com a operadora do plano de saúde recebida como parâmetro
        /// </summary>
        /// <param name="ID_GRUPO"></param>
        /// <param name="ID_SUB_GRUPO"></param>
        /// <param name="NO_PROCEDIMENTO"></param>
        /// <param name="ID_OPER"></param>
        private void CarregaGridExamesMedicosDisponiveis(int ID_GRUPO, int ID_SUB_GRUPO, string NO_PROCEDIMENTO, int CO_EMP, int ID_OPER = 0)
        {
            int plan = (!string.IsNullOrEmpty(hidIdPlano.Value) ? int.Parse(hidIdPlano.Value) : 0);
            if (CO_EMP == 0) // Se receber 0 no CO_EMP, então carrega todos os exames independente de associação
            {
                var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                           join tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE into l1
                           from ls in l1.DefaultIfEmpty()

                           where tbs356.CO_TIPO_PROC_MEDI == "EX"
                           && tbs356.CO_OPER == "999"
                           && (ID_GRUPO != 0 ? tbs356.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == ID_GRUPO : 0 == 0)
                           && (ID_SUB_GRUPO != 0 ? tbs356.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP == ID_SUB_GRUPO : 0 == 0)
                           && (!string.IsNullOrEmpty(NO_PROCEDIMENTO) ? tbs356.NM_PROC_MEDI.Contains(NO_PROCEDIMENTO) : 0 == 0)
                           //&& ls.FL_STATU == "A"
                           select new ProcedimentosDisponiveis
                           {
                               ID_PROC_MEDI_PROCE = tbs356.ID_PROC_MEDI_PROCE,
                               CO_PROC_MEDI = tbs356.CO_PROC_MEDI,
                               NM_PROC_MEDIC_GRUPO = tbs356.TBS354_PROC_MEDIC_GRUPO.NM_PROC_MEDIC_GRUPO,
                               NM_PROC_MEDIC_SGRUP = tbs356.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP,
                               NM_PROC_MEDI = tbs356.NM_PROC_MEDI,
                               STATUS = tbs356.CO_SITU_PROC_MEDI == "A" ? "Ativo" : "Inativo",
                               DISP = " - ",
                               vl_tab = (ls != null ? ls.VL_BASE : (decimal?)null),
                               ID_OPER = ID_OPER,
                               ID_PLAN = plan,
                           }).OrderBy(w => w.NM_PROC_MEDI).ToList();

                grdListarExames.DataSource = res;
                grdListarExames.DataBind();
            }
            else //Do contrário carrega os exames de acordo com a unidade recebida como parâmetro
            {
                //Gera lista e associa à GRID com TODOS os EXAMES da INSTITUIÇÃO
                var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                           join tbs358 in TBS358_PROC_MEDIC_UNID.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs358.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE

                           join tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE into l1
                           from ls in l1.DefaultIfEmpty()

                           where tbs358.FL_SITUA_ASSOC_PROC_UNID == "A"
                           && tbs356.CO_TIPO_PROC_MEDI == "EX"
                           && tbs356.CO_OPER == "999"
                           && (ID_GRUPO != 0 ? tbs356.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == ID_GRUPO : 0 == 0)
                           && (ID_SUB_GRUPO != 0 ? tbs356.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP == ID_SUB_GRUPO : 0 == 0)
                           && (!string.IsNullOrEmpty(NO_PROCEDIMENTO) ? tbs356.NM_PROC_MEDI.Contains(NO_PROCEDIMENTO) : 0 == 0)
                               //Se recebe 0 como parâmetro no co_emp, então trazer de padrão da unidade logada
                           && (CO_EMP != 0 ? tbs358.TB25_EMPRESA.CO_EMP == CO_EMP : 0 == 0)
                           select new ProcedimentosDisponiveis
                           {
                               ID_PROC_MEDI_PROCE = tbs356.ID_PROC_MEDI_PROCE,
                               CO_PROC_MEDI = tbs356.CO_PROC_MEDI,
                               NM_PROC_MEDIC_GRUPO = tbs356.TBS354_PROC_MEDIC_GRUPO.NM_PROC_MEDIC_GRUPO,
                               NM_PROC_MEDIC_SGRUP = tbs356.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP,
                               NM_PROC_MEDI = tbs356.NM_PROC_MEDI,
                               STATUS = tbs356.CO_SITU_PROC_MEDI == "A" ? "Ativo" : "Inativo",
                               DISP = tbs358.FL_PROC_MEDIC_DISPO == "S" ? "Sim" : "Não",
                               vl_tab = (ls != null ? ls.VL_BASE : (decimal?)null),
                               ID_OPER = ID_OPER,
                               ID_PLAN = plan,
                           }).OrderBy(w => w.NM_PROC_MEDI).ToList();

                grdListarExames.DataSource = res;
                grdListarExames.DataBind();
            }
        }

        /// <summary>
        /// Carrega a lista de Procedimentos médicos disponíveis de acordo com a operadora do plano de saúde recebida como parâmetro
        /// </summary>
        /// <param name="ID_GRUPO"></param>
        /// <param name="ID_SUB_GRUPO"></param>
        /// <param name="NO_PROCEDIMENTO"></param>
        /// <param name="ID_OPER"></param>
        private void CarregaGridProcedimentosMedicosDisponiveis(int ID_GRUPO, int ID_SUB_GRUPO, string NO_PROCEDIMENTO, int CO_EMP, int ID_OPER = 0)
        {
            int plan = (!string.IsNullOrEmpty(hidIdPlano.Value) ? int.Parse(hidIdPlano.Value) : 0);

            if (CO_EMP == 0) // Se receber 0 no CO_EMP, então carrega todos os exames independente de associação
            {
                var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()

                           join tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE into l1
                           from ls in l1.DefaultIfEmpty()

                           where tbs356.CO_TIPO_PROC_MEDI != "EX" && tbs356.CO_TIPO_PROC_MEDI != "CO"
                           && tbs356.CO_OPER == "999"
                           && (ID_GRUPO != 0 ? tbs356.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == ID_GRUPO : 0 == 0)
                           && (ID_SUB_GRUPO != 0 ? tbs356.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP == ID_SUB_GRUPO : 0 == 0)
                           && (!string.IsNullOrEmpty(NO_PROCEDIMENTO) ? tbs356.NM_PROC_MEDI.Contains(NO_PROCEDIMENTO) : 0 == 0)

                           select new ProcedimentosDisponiveis
                           {
                               ID_PROC_MEDI_PROCE = tbs356.ID_PROC_MEDI_PROCE,
                               CO_PROC_MEDI = tbs356.CO_PROC_MEDI,
                               NM_PROC_MEDIC_GRUPO = tbs356.TBS354_PROC_MEDIC_GRUPO.NM_PROC_MEDIC_GRUPO,
                               NM_PROC_MEDIC_SGRUP = tbs356.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP,
                               NM_PROC_MEDI = tbs356.NM_PROC_MEDI,
                               STATUS = tbs356.CO_SITU_PROC_MEDI == "A" ? "Ativo" : "Inativo",
                               DISP = " - ",
                               vl_tab = (ls != null ? ls.VL_BASE : (decimal?)null),
                               ID_OPER = ID_OPER,
                               ID_PLAN = plan,
                           }).OrderBy(w => w.NM_PROC_MEDI).ToList();

                grdListarProcMedic.DataSource = res;
                grdListarProcMedic.DataBind();
            }
            else  //Do contrário carrega os exames de acordo com a unidade recebida como parâmetro
            {
                var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                           join tbs358 in TBS358_PROC_MEDIC_UNID.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs358.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE

                           join tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE into l1
                           from ls in l1.DefaultIfEmpty()

                           where tbs358.FL_SITUA_ASSOC_PROC_UNID == "A"
                           && tbs356.CO_TIPO_PROC_MEDI != "EX" && tbs356.CO_TIPO_PROC_MEDI != "CO"
                           && tbs356.CO_OPER == "999"
                           && (ID_GRUPO != 0 ? tbs356.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == ID_GRUPO : 0 == 0)
                           && (ID_SUB_GRUPO != 0 ? tbs356.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP == ID_SUB_GRUPO : 0 == 0)
                           && (!string.IsNullOrEmpty(NO_PROCEDIMENTO) ? tbs356.NM_PROC_MEDI.Contains(NO_PROCEDIMENTO) : 0 == 0)
                               //Se recebe 0 como parâmetro no co_emp, então trazer de padrão da unidade logada
                           && (CO_EMP != 0 ? tbs358.TB25_EMPRESA.CO_EMP == CO_EMP : 0 == 0)
                           select new ProcedimentosDisponiveis
                           {
                               ID_PROC_MEDI_PROCE = tbs356.ID_PROC_MEDI_PROCE,
                               CO_PROC_MEDI = tbs356.CO_PROC_MEDI,
                               NM_PROC_MEDIC_GRUPO = tbs356.TBS354_PROC_MEDIC_GRUPO.NM_PROC_MEDIC_GRUPO,
                               NM_PROC_MEDIC_SGRUP = tbs356.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP,
                               NM_PROC_MEDI = tbs356.NM_PROC_MEDI,
                               STATUS = tbs356.CO_SITU_PROC_MEDI == "A" ? "Ativo" : "Inativo",
                               DISP = tbs358.FL_PROC_MEDIC_DISPO == "S" ? "Sim" : "Não",
                               vl_tab = (ls != null ? ls.VL_BASE : (decimal?)null),
                               ID_OPER = ID_OPER,
                               ID_PLAN = plan,
                           }).OrderBy(w => w.NM_PROC_MEDI).ToList();

                grdListarProcMedic.DataSource = res;
                grdListarProcMedic.DataBind();
            }
        }

        // Classes de Saída que auxiliam no carregamento das informações nas grides
        #region Classes de Saída

        public class AgendaDeConsultasMedicas
        {
            public int ANTIGOS { get; set; }

            public int CO_ALU { get; set; }
            public string NO_PAC
            {
                get
                {
                    return (this.NO_PAC_RECEB.Length > 25 ? this.NO_PAC_RECEB.Substring(0, 25) + "..." : this.NO_PAC_RECEB);
                }
            }
            public string NO_PAC_RECEB { get; set; }
            public DateTime? dt_nascimento { get; set; }
            public string NU_IDADE
            {
                get
                {
                    string idade = " - ";

                    //Calcula a idade do Paciente de acordo com a data de nascimento do mesmo.
                    if (this.dt_nascimento.HasValue)
                    {
                        idade = AuxiliFormatoExibicao.FormataDataNascimento(this.dt_nascimento);
                    }
                    return idade;
                }
            }
            public string CO_SEXO { get; set; }

            public int? CO_COL { get; set; }
            public int CO_AGEND_MEDIC { get; set; }

            //Carrega informações gerais do agendamento
            public DateTime dt_Consul { get; set; }
            public string hr_Consul { get; set; }
            public string hora
            {
                get
                {
                    return this.dt_Consul.ToString("dd/MM/yy") + " - " + this.hr_Consul;
                }
            }
            public string NO_ESPEC { get; set; }
            public int CO_ESPEC { get; set; }

            public string FL_CONF { get; set; }
            public bool FL_CONF_VALID
            {
                get
                {
                    return this.FL_CONF == "S" ? true : false;
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

        public class EncaminhamentosMedicos
        {
            public int ANTIGO { get; set; }

            //Informações do Paciente
            public int CO_RESP { get; set; }
            public int CO_ALU { get; set; }
            public int? ID_OPER { get; set; }
            public int? ID_PLAN { get; set; }
            public string NO_PAC
            {
                get
                {
                    return (this.NO_PAC_RECEB.Length > 25 ? this.NO_PAC_RECEB.Substring(0, 25) + "..." : this.NO_PAC_RECEB);
                }
            }
            public string NO_PAC_RECEB { get; set; }
            public DateTime? dt_nascimento { get; set; }
            public string NU_IDADE
            {
                get
                {
                    string idade = " - ";

                    //Calcula a idade do Paciente de acordo com a data de nascimento do mesmo.
                    if (this.dt_nascimento.HasValue)
                    {
                        idade = AuxiliFormatoExibicao.FormataDataNascimento(this.dt_nascimento);
                    }
                    return idade;
                }
            }
            public string CO_SEXO { get; set; }

            //Informações do médico
            public int? CO_COL { get; set; }

            //Informações do pré-atendimento
            public int? ID_PRE_ATEND { get; set; }
            public string NU_SENHA
            {
                get
                {
                    //Resgata a informação de senha do pré-atendimento
                    string senha = " - ";
                    if (this.ID_PRE_ATEND.HasValue)
                    {
                        var res = (from tbs194 in TBS194_PRE_ATEND.RetornaTodosRegistros()
                                   where tbs194.ID_PRE_ATEND == this.ID_PRE_ATEND.Value
                                   select new { tbs194.NU_SENHA_ATEND }).FirstOrDefault();
                        senha = res != null ? res.NU_SENHA_ATEND : " - ";
                    }
                    return senha;
                }
            }
            public DateTime? dataPreAtend
            {
                get
                {
                    //Resgata a informação de data do pré-atendimento
                    DateTime? dt = (DateTime?)null;
                    if (ID_PRE_ATEND.HasValue)
                    {
                        dt = TBS194_PRE_ATEND.RetornaPelaChavePrimaria(this.ID_PRE_ATEND.Value).DT_PRE_ATEND;
                    }
                    return dt;
                }
            }
            public string dataPreAtendValid
            {
                get
                {
                    return (this.dataPreAtend.HasValue ? this.dataPreAtend.Value.ToString("dd/MM/yy") : "");
                }
            }
            public string horaPreAtendValid
            {
                get
                {
                    return (this.dataPreAtend.HasValue ? this.dataPreAtend.Value.ToString("HH:mm") : "");
                }
            }
            public string DTHRPreAtend
            {
                get
                {
                    return this.dataPreAtendValid + " - " + this.horaPreAtendValid;
                }
            }

            //Informações gerais do encaminhamento
            public int CO_TIPO_RISCO { get; set; }
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
            public int ID_ENCAM_MEDIC { get; set; }
            public string NO_ESPEC { get; set; }
            public int CO_ESPEC { get; set; }
            public string CO_ENCAM_MEDIC { get; set; }
            public string NM_OPER { get; set; }

            //Trata data e hora do encaminhamento
            public DateTime? dataEncamMed { get; set; }
            public string dataEMValid
            {
                get
                {
                    return this.dataEncamMed.Value.ToString("dd/MM/yy");
                }
            }
            public string horaEMValid
            {
                get
                {
                    return this.dataEncamMed.Value.ToString("HH:mm");
                }
            }
            public string DTHREncamMed
            {
                get
                {
                    return this.dataEMValid + " - " + this.horaEMValid;
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

        public class ReservaMedicamentos
        {
            public string Medicamento { get; set; }
            public string GRUPO { get; set; }
            public decimal QTM1 { get; set; }
            public decimal QTM2 { get; set; }
            public decimal QTM3 { get; set; }
            public decimal QTM4 { get; set; }
            public decimal TOTAL
            {
                get
                {
                    return this.QTM1 + this.QTM2 + this.QTM3 + this.QTM4;
                }
            }
            public DateTime dt { get; set; }
            public int CO_PROD { get; set; }
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

        public class ProcedimentosDisponiveis
        {
            public int ID_OPER { get; set; }
            public int ID_PLAN { get; set; }

            public int ID_PROC_MEDI_PROCE { get; set; }
            public string CO_PROC_MEDI { get; set; }
            public string NM_PROC_MEDIC_GRUPO { get; set; }
            public string NM_PROC_MEDIC_SGRUP { get; set; }
            public string NM_PROC_MEDI { get; set; }

            public string STATUS { get; set; }
            public string DISP { get; set; }
            public string CO
            {
                get
                {
                    //Verifica se existe algum procedimento para a operadora ID_OPER de acordo com o ID_PROC_MEDI_PROCE
                    if (this.ID_PLAN != 0)
                    {
                        //Instancia um objeto com as condições atuais do procedimento em contexto para o plano de saúde em contexto
                        var res = (from tbs361 in TBS361_CONDI_PLANO_SAUDE.RetornaTodosRegistros()
                                   join tbs362 in TBS362_ASSOC_PLANO_PROCE.RetornaTodosRegistros() on tbs361.TBS362_ASSOC_PLANO_PROCE.ID_ASSOC_PLANO_PROCE equals tbs362.ID_ASSOC_PLANO_PROCE
                                   where tbs362.TB251_PLANO_OPERA.ID_PLAN == this.ID_PLAN
                                   && tbs362.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == this.ID_PROC_MEDI_PROCE
                                   && tbs362.FL_STATU == "A"
                                   select new
                                   {
                                       tbs361.CO_REFER_TIPO,
                                       tbs361.VL_CONTE_REFER,
                                   }).FirstOrDefault();

                        return (res != null ? "Sim" : "Não");
                    }
                    else
                        return "SIM";
                }
            }
            public decimal? vl_tab { get; set; }
            public string VL_TABELA
            {
                get
                {
                    return (this.vl_tab.HasValue ? this.vl_tab.Value.ToString("N2") : " - ");
                }
            }
            public string VL_OPERADORA
            {
                get
                {
                    //Procura pelo procedimento da Operadora ID_OPER correspondente ao ID_PROC_MEDI_PROC associados pelo campo agrupador para retornar o valor
                    var resusu = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                                  where tbs356.ID_AGRUP_PROC_MEDI_PROCE == this.ID_PROC_MEDI_PROCE
                                  && tbs356.TB250_OPERA.ID_OPER == this.ID_OPER
                                  && tbs356.CO_SITU_PROC_MEDI == "A"
                                  select new { tbs356.ID_PROC_MEDI_PROCE }).FirstOrDefault();

                    if (resusu != null)
                    {

                        //Apenas se vier de um plano de saúde
                        if (this.ID_PLAN != 0)
                        {
                            //Instancia um objeto com as condições atuais do procedimento em contexto para o plano de saúde em contexto
                            var res = (from tbs361 in TBS361_CONDI_PLANO_SAUDE.RetornaTodosRegistros()
                                       join tbs362 in TBS362_ASSOC_PLANO_PROCE.RetornaTodosRegistros() on tbs361.TBS362_ASSOC_PLANO_PROCE.ID_ASSOC_PLANO_PROCE equals tbs362.ID_ASSOC_PLANO_PROCE
                                       where tbs362.TB251_PLANO_OPERA.ID_PLAN == this.ID_PLAN
                                       && tbs362.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == resusu.ID_PROC_MEDI_PROCE
                                       && tbs362.FL_STATU == "A"
                                       select new
                                       {
                                           tbs361.CO_REFER_TIPO,
                                           tbs361.VL_CONTE_REFER,
                                       }).FirstOrDefault();

                            if (res != null) //Se houver associação entre o plano de saúde e o procedimento
                            {
                                //Descobre o valor corrente do procedimento
                                var valorProce = (from tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                                                  where tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == resusu.ID_PROC_MEDI_PROCE
                                                  && tbs353.FL_STATU == "A"
                                                  select new
                                                  {
                                                      tbs353.VL_BASE,
                                                      tbs353.VL_CUSTO,
                                                  }).FirstOrDefault();

                                if (valorProce == null) //Se não houver valor associado ao procedimento, ele fica 0
                                    return "0,00";

                                //Calcula o valor à ser cobrado de acordo com o valor total do procedimento e as condições 
                                //de cálculo deste procedimento para o plano de saúde em contexto
                                decimal valorTotal = 0;

                                if (res.CO_REFER_TIPO == "V")
                                {
                                    //Calcula o valor, sendo que se o valor de abatimento for maior que o valor base do procedimento
                                    //o valor resultante fica 0, pois não pode ser negativo, do contrário, é subtraído o valor de abatimento
                                    //do valor base do procedimento
                                    valorTotal = (valorProce.VL_BASE < res.VL_CONTE_REFER ? 0 : valorProce.VL_BASE - res.VL_CONTE_REFER);
                                }
                                else // Calcula o desconto em porcentagem
                                {
                                    double percentual = (double)res.VL_CONTE_REFER / 100; // calcula porcentagem
                                    double valorFinal = (double)valorProce.VL_BASE - (percentual * (double)valorProce.VL_BASE);
                                    valorTotal = (decimal)valorFinal;
                                }

                                return valorTotal.ToString("N2"); // Retorna o valor total correspondente
                            }
                            else //O Plano de saúde do usuário não está associado ao procedimento (Este plano não cobre este procedimento nesta unidade)
                                return " - ";
                        }
                        else
                            return " - ";
                    }
                    else
                        return " - ";
                }
            }
        }

        #endregion

        #endregion

        #region Carregamento Itens em Busca

        /// <summary>
        /// Responsável por carregar o exame de acordo com o recebido como parâmetro ou informado em campo na página
        /// </summary>
        /// <param name="ID_PROC_MEDI_PROCE"></param>
        private void CarregaExameMedicoReq(int ID_OPER, int ID_PROC_MEDI_PROCE = 0)
        {
            ControlaTabs("RQE", "");

            if (ID_PROC_MEDI_PROCE == 0)
            {
                if (txtCodExame.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o código que deverá ser pesquisado.");
                    return;
                }
            }

            //Método responsável por fazer uma busca pelo código informado e preencher os devidos campos
            string codProcMedic = txtCodExame.Text.Replace(".", "").Trim();

            var res = TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros().FirstOrDefault();

            //Executa este bloco se o código do exame tiver sido informado para pesquisa
            #region Desuso

            //if (!string.IsNullOrEmpty(codProcMedic))
            //{
            //    res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
            //           where tbs356.CO_PROC_MEDI == codProcMedic
            //           select tbs356).FirstOrDefault();

            //    if (res == null)
            //    {
            //        AuxiliPagina.EnvioMensagemErro(this.Page, "Não existe exame cadastrado com o código informado!");
            //        txtExameAtendMed.Text = hidCodExame.Value = FL_REQUE_APROV_EX.Value = "";
            //        return;
            //    }
            //    else if (res.CO_SITU_PROC_MEDI != "A")
            //    {
            //        AuxiliPagina.EnvioMensagemErro(this.Page, "O Exame de código informado está inativo!");
            //        txtExameAtendMed.Text = hidCodExame.Value = FL_REQUE_APROV_EX.Value = "";
            //        return;
            //    }

            //}

            #endregion

            //Se o ID_OPER for diferente de 0, então, será pesquisado, de acordo com o id do procedimento, pelo procedimento correspondente associado ao ID_OPER
            if (ID_OPER != 0)
            {
                //Instancia o objeto do procedimento que possua o id agrupador = ao id recebido como parâmetro e ID_OPER = ID_OPER recebido como parâmetro
                //Mas se o ID do procedimento for 0, então o método foi acionado para pesquisa pelo código, o que se faz
                //necessário que o código seja pesquisado, e não o ID
                res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                       //Viabiliza que este select sirva tanto para a pesquisa pelo campo, como pelo clique na grid
                       where (ID_PROC_MEDI_PROCE != 0 ? tbs356.ID_AGRUP_PROC_MEDI_PROCE == ID_PROC_MEDI_PROCE : tbs356.CO_PROC_MEDI == codProcMedic)
                       && tbs356.TB250_OPERA.ID_OPER == ID_OPER
                       && tbs356.CO_SITU_PROC_MEDI == "A"
                       select tbs356).FirstOrDefault();

                //Verifica se o select acima retornou algo, ou seja, se existe um exame da operadora para este procedimento,
                //caso não exista, insere o exame correspondente da própria instituição

                if (res == null)
                {
                    res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                           where (ID_PROC_MEDI_PROCE != 0 ? tbs356.ID_PROC_MEDI_PROCE == ID_PROC_MEDI_PROCE : tbs356.CO_PROC_MEDI == codProcMedic)
                           select tbs356).FirstOrDefault();
                }

                if (res == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Não existe exame cadastrado com o código informado");
                    txtExameAtendMed.Text = hidCodExame.Value = FL_REQUE_APROV_EX.Value = "";
                    return;
                }
            }
            else // Se não for por operadora, verifica pelo ID do procedimento ou pelo código do procedimento
            {
                res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                       where (ID_PROC_MEDI_PROCE != 0 ? tbs356.ID_PROC_MEDI_PROCE == ID_PROC_MEDI_PROCE : tbs356.CO_PROC_MEDI == codProcMedic)
                       select tbs356).FirstOrDefault();

                if (res == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Não existe exame cadastrado com o código informado");
                    txtExameAtendMed.Text = hidCodExame.Value = FL_REQUE_APROV_EX.Value = "";
                    return;
                }
                else if (res.CO_SITU_PROC_MEDI != "A")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Procedimento Médico de código informado está inativo!");
                    txtNoServAmu.Text = hidCodServAmbu.Value = FL_REQUE_APROV_SA.Value = "";
                    return;
                }
            }

            if (res != null)
            {
                txtCodExame.Text = res.CO_PROC_MEDI;
                txtExameAtendMed.Text = res.NM_PROC_MEDI;
                hidCodExame.Value = res.ID_PROC_MEDI_PROCE.ToString();
                FL_REQUE_APROV_EX.Value = res.FL_AUTO_PROC_MEDI;
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não existe exame cadastrado com o código informado");
                txtExameAtendMed.Text = hidCodExame.Value = FL_REQUE_APROV_EX.Value = "";
                return;
            }
        }

        /// <summary>
        /// Responsável por carregar o exame de acordo com o recebido como parâmetro ou informado em campo na página
        /// </summary>
        /// <param name="ID_PROC_MEDI_PROCE"></param>
        private void CarregaProcedMedicoReq(int ID_OPER, int ID_PROC_MEDI_PROCE = 0)
        {
            ControlaTabs("RSA", "");

            if (ID_PROC_MEDI_PROCE == 0)
            {
                if (txtCodServAmbu.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o código que deverá ser pesquisado.");
                    return;
                }
            }

            //Método responsável por fazer uma busca pelo código informado e preencher os devidos campos
            string codProcMedic = txtCodServAmbu.Text.Replace(".", "").Trim();
            var res = TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros().FirstOrDefault();

            //Executa este bloco se o código do Procedimento tiver sido informado para pesquisa
            #region Desuso

            //if (!string.IsNullOrEmpty(codProcMedic))
            //{
            //    res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
            //           where tbs356.CO_PROC_MEDI == codProcMedic
            //           select tbs356).FirstOrDefault();

            //    if (res == null)
            //    {
            //        AuxiliPagina.EnvioMensagemErro(this.Page, "Não existe Procedimento Médico cadastrado com o código informado!");
            //        txtNoServAmu.Text = hidCodServAmbu.Value = FL_REQUE_APROV_SA.Value = "";
            //        return;
            //    }
            //    else if (res.CO_SITU_PROC_MEDI != "A")
            //    {
            //        AuxiliPagina.EnvioMensagemErro(this.Page, "O Procedimento Médico de código informado está inativo!");
            //        txtNoServAmu.Text = hidCodServAmbu.Value = FL_REQUE_APROV_SA.Value = "";
            //        return;
            //    }

            //}

            #endregion

            //Se o ID_OPER for diferente de 0, então, será pesquisado, de acordo com o id do procedimento, pelo procedimento correspondente associado ao ID_OPER
            if (ID_OPER != 0)
            {
                //Instancia o objeto do procedimento que possua o id agrupador = ao id recebido como parâmetro e ID_OPER = ID_OPER recebido como parâmetro
                //Mas se o ID do procedimento for 0, então o método foi acionado para pesquisa pelo código, o que se faz
                //necessário que o código seja pesquisado, e não o ID
                res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                       //Viabiliza que este select sirva tanto para a pesquisa pelo campo, como pelo clique na grid
                       where (ID_PROC_MEDI_PROCE != 0 ? tbs356.ID_AGRUP_PROC_MEDI_PROCE == ID_PROC_MEDI_PROCE : tbs356.CO_PROC_MEDI == codProcMedic)
                       && tbs356.TB250_OPERA.ID_OPER == ID_OPER
                       && tbs356.CO_SITU_PROC_MEDI == "A"
                       select tbs356).FirstOrDefault();

                //Verifica se o select acima retornou algo, ou seja, se existe um exame da operadora para este procedimento,
                //caso não exista, insere o exame correspondente da própria instituição

                if (res == null)
                {
                    res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                           where (ID_PROC_MEDI_PROCE != 0 ? tbs356.ID_PROC_MEDI_PROCE == ID_PROC_MEDI_PROCE : tbs356.CO_PROC_MEDI == codProcMedic)
                           select tbs356).FirstOrDefault();
                }

                if (res == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Não existe Procedimento Médico cadastrado com o código informado");
                    txtNoServAmu.Text = hidCodServAmbu.Value = FL_REQUE_APROV_SA.Value = "";
                    return;
                }
                else if (res.CO_SITU_PROC_MEDI != "A")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Procedimento Médico de código informado está inativo!");
                    txtNoServAmu.Text = hidCodServAmbu.Value = FL_REQUE_APROV_SA.Value = "";
                    return;
                }
            }
            else // Se não for por operadora, verifica pelo ID do procedimento ou pelo código do procedimento
            {
                res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                       where (ID_PROC_MEDI_PROCE != 0 ? tbs356.ID_PROC_MEDI_PROCE == ID_PROC_MEDI_PROCE : tbs356.CO_PROC_MEDI == codProcMedic)
                       select tbs356).FirstOrDefault();

                if (res == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Não existe Procedimento Médico cadastrado com o código informado");
                    txtNoServAmu.Text = hidCodServAmbu.Value = FL_REQUE_APROV_SA.Value = "";
                    return;
                }
                else if (res.CO_SITU_PROC_MEDI != "A")
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Procedimento Médico de código informado está inativo!");
                    txtNoServAmu.Text = hidCodServAmbu.Value = FL_REQUE_APROV_SA.Value = "";
                    return;
                }
            }

            if (res != null)
            {
                txtCodServAmbu.Text = res.CO_PROC_MEDI;
                txtNoServAmu.Text = res.NM_PROC_MEDI;
                hidCodServAmbu.Value = res.ID_PROC_MEDI_PROCE.ToString();
                FL_REQUE_APROV_SA.Value = res.FL_AUTO_PROC_MEDI;
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não existe Procedimento cadastrado com o código informado");
                txtCodServAmbu.Text = txtNoServAmu.Text = hidCodServAmbu.Value = FL_REQUE_APROV_SA.Value = "";
                return;
            }
        }

        #endregion

        #region DropDownList's Modal's

        /// <summary>
        /// Carrega os grupos de procedimentos de acordo com a unidade logada e Operadora ou não
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="ID_OPER"></param>
        private void CarregaGruposExames(DropDownList ddl, int ID_OPER)
        {
            if (ID_OPER == 0)
            {
                var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                           join tbs358 in TBS358_PROC_MEDIC_UNID.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs358.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                           where tbs356.CO_SITU_PROC_MEDI == "A"
                            && tbs358.FL_SITUA_ASSOC_PROC_UNID == "A"
                            && tbs358.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                            && tbs356.CO_TIPO_PROC_MEDI == "EX"
                            && tbs356.CO_OPER == "999"
                           select new
                           {
                               tbs356.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO,
                               tbs356.TBS354_PROC_MEDIC_GRUPO.NM_PROC_MEDIC_GRUPO,
                           }).Distinct().OrderBy(w => w.NM_PROC_MEDIC_GRUPO).ToList();

                ddl.DataTextField = "NM_PROC_MEDIC_GRUPO";
                ddl.DataValueField = "ID_PROC_MEDIC_GRUPO";
                ddl.DataSource = res;
                ddl.DataBind();
            }
            else
            {
                var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                           join tbs358 in TBS358_PROC_MEDIC_UNID.RetornaTodosRegistros() on tbs356.ID_AGRUP_PROC_MEDI_PROCE equals tbs358.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                           where tbs356.CO_SITU_PROC_MEDI == "A"
                           && tbs358.FL_SITUA_ASSOC_PROC_UNID == "A"
                           && tbs358.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                           && tbs356.CO_TIPO_PROC_MEDI == "EX"
                           && (tbs356.TB250_OPERA.ID_OPER == ID_OPER)
                           select new
                           {
                               tbs356.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO,
                               tbs356.TBS354_PROC_MEDIC_GRUPO.NM_PROC_MEDIC_GRUPO,
                           }).Distinct().OrderBy(w => w.NM_PROC_MEDIC_GRUPO).ToList();

                ddl.DataTextField = "NM_PROC_MEDIC_GRUPO";
                ddl.DataValueField = "ID_PROC_MEDIC_GRUPO";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            ddl.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Carrega os subgrupos de procedimentos de acordo com a unidade logada e Operadora ou não
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="ID_OPER"></param>
        private void CarregaSubGruposExames(DropDownList ddl, int ID_OPER, int ID_GRUPO)
        {
            if (ID_OPER == 0)
            {
                var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                           join tbs358 in TBS358_PROC_MEDIC_UNID.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs358.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                           where tbs356.CO_SITU_PROC_MEDI == "A"
                           && tbs358.FL_SITUA_ASSOC_PROC_UNID == "A"
                           && tbs358.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                           && tbs356.CO_TIPO_PROC_MEDI == "EX"
                           && tbs356.CO_OPER == "999"
                           && tbs356.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == ID_GRUPO
                           select new
                           {
                               tbs356.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP,
                               tbs356.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP,
                           }).Distinct().OrderBy(w => w.NM_PROC_MEDIC_SGRUP).ToList();

                ddl.DataTextField = "NM_PROC_MEDIC_SGRUP";
                ddl.DataValueField = "ID_PROC_MEDIC_SGRUP";
                ddl.DataSource = res;
                ddl.DataBind();
            }
            else
            {
                var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                           join tbs358 in TBS358_PROC_MEDIC_UNID.RetornaTodosRegistros() on tbs356.ID_AGRUP_PROC_MEDI_PROCE equals tbs358.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                           where tbs356.CO_SITU_PROC_MEDI == "A"
                           && tbs358.FL_SITUA_ASSOC_PROC_UNID == "A"
                           && tbs358.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                           && tbs356.CO_TIPO_PROC_MEDI == "EX"
                           && tbs356.TB250_OPERA.ID_OPER == ID_OPER
                           && tbs356.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == ID_GRUPO
                           select new
                           {
                               tbs356.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP,
                               tbs356.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP,
                           }).Distinct().OrderBy(w => w.NM_PROC_MEDIC_SGRUP).ToList();

                ddl.DataTextField = "NM_PROC_MEDIC_SGRUP";
                ddl.DataValueField = "ID_PROC_MEDIC_SGRUP";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            ddl.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Carrega os grupos de procedimentos de acordo com a unidade logada e Operadora ou não
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="ID_OPER"></param>
        private void CarregaGruposProcMedic(DropDownList ddl, int ID_OPER)
        {
            if (ID_OPER == 0)
            {
                var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                           join tbs358 in TBS358_PROC_MEDIC_UNID.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs358.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                           where tbs356.CO_SITU_PROC_MEDI == "A"
                            && tbs358.FL_SITUA_ASSOC_PROC_UNID == "A"
                            && tbs358.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                            && tbs356.CO_TIPO_PROC_MEDI != "EX" && tbs356.CO_TIPO_PROC_MEDI != "CO"
                            && tbs356.CO_OPER == "999"
                           select new
                           {
                               tbs356.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO,
                               tbs356.TBS354_PROC_MEDIC_GRUPO.NM_PROC_MEDIC_GRUPO,
                           }).Distinct().OrderBy(w => w.NM_PROC_MEDIC_GRUPO).ToList();

                ddl.DataTextField = "NM_PROC_MEDIC_GRUPO";
                ddl.DataValueField = "ID_PROC_MEDIC_GRUPO";
                ddl.DataSource = res;
                ddl.DataBind();
            }
            else
            {
                var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                           join tbs358 in TBS358_PROC_MEDIC_UNID.RetornaTodosRegistros() on tbs356.ID_AGRUP_PROC_MEDI_PROCE equals tbs358.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                           where tbs356.CO_SITU_PROC_MEDI == "A"
                           && tbs358.FL_SITUA_ASSOC_PROC_UNID == "A"
                           && tbs358.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                           && tbs356.CO_TIPO_PROC_MEDI != "EX" && tbs356.CO_TIPO_PROC_MEDI != "CO"
                           && (tbs356.TB250_OPERA.ID_OPER == ID_OPER)
                           select new
                           {
                               tbs356.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO,
                               tbs356.TBS354_PROC_MEDIC_GRUPO.NM_PROC_MEDIC_GRUPO,
                           }).Distinct().OrderBy(w => w.NM_PROC_MEDIC_GRUPO).ToList();

                ddl.DataTextField = "NM_PROC_MEDIC_GRUPO";
                ddl.DataValueField = "ID_PROC_MEDIC_GRUPO";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            ddl.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Carrega os subgrupos de procedimentos de acordo com a unidade logada e Operadora ou não
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="ID_OPER"></param>
        private void CarregaSubGruposProcMedic(DropDownList ddl, int ID_OPER, int ID_GRUPO)
        {
            if (ID_OPER == 0)
            {
                var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                           join tbs358 in TBS358_PROC_MEDIC_UNID.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs358.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                           where tbs356.CO_SITU_PROC_MEDI == "A"
                           && tbs358.FL_SITUA_ASSOC_PROC_UNID == "A"
                           && tbs358.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                           && tbs356.CO_TIPO_PROC_MEDI != "EX" && tbs356.CO_TIPO_PROC_MEDI != "CO"
                           && tbs356.CO_OPER == "999"
                           && tbs356.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == ID_GRUPO
                           select new
                           {
                               tbs356.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP,
                               tbs356.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP,
                           }).Distinct().OrderBy(w => w.NM_PROC_MEDIC_SGRUP).ToList();

                ddl.DataTextField = "NM_PROC_MEDIC_SGRUP";
                ddl.DataValueField = "ID_PROC_MEDIC_SGRUP";
                ddl.DataSource = res;
                ddl.DataBind();
            }
            else
            {
                var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                           join tbs358 in TBS358_PROC_MEDIC_UNID.RetornaTodosRegistros() on tbs356.ID_AGRUP_PROC_MEDI_PROCE equals tbs358.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                           where tbs356.CO_SITU_PROC_MEDI == "A"
                           && tbs358.FL_SITUA_ASSOC_PROC_UNID == "A"
                           && tbs358.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                           && tbs356.CO_TIPO_PROC_MEDI != "EX" && tbs356.CO_TIPO_PROC_MEDI != "CO"
                           && tbs356.TB250_OPERA.ID_OPER == ID_OPER
                           && tbs356.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == ID_GRUPO
                           select new
                           {
                               tbs356.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP,
                               tbs356.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP,
                           }).Distinct().OrderBy(w => w.NM_PROC_MEDIC_SGRUP).ToList();

                ddl.DataTextField = "NM_PROC_MEDIC_SGRUP";
                ddl.DataValueField = "ID_PROC_MEDIC_SGRUP";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            ddl.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion

        /// <summary>
        /// Verifica se o atendimento recebido como parâmetro tem operadora atrelada ou não
        /// </summary>
        /// <returns>caso tenha operadora no atendimento retorna o id dela, caso contrário retorna 0</returns>
        private int CarregaOperaAtendimento(int idAtend)
        {
            int idOper = 0;
            //Resgata o ID da Operadora do plano de saúde em contexto
            TBS219_ATEND_MEDIC tbs219 = TBS219_ATEND_MEDIC.RetornaPelaChavePrimaria(idAtend);
            if (tbs219 != null)
            {
                tbs219.TB250_OPERAReference.Load();
                if (tbs219.TB250_OPERA != null)
                    idOper = tbs219.TB250_OPERA.ID_OPER;
            }

            return idOper;
        }

        /// <summary>
        /// Calcula dias, meses e anos da idade
        /// </summary>
        private string CarregaIdadePaciente(DateTime DT_NASC)
        {
            return AuxiliFormatoExibicao.FormataDataNascimento(DT_NASC);
        }

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
        /// Método responsável por limpar todos os dados da aba de informações de pré-atendimento, para evitar de aparecerem dados de outro paciente selecionado anteriormente
        /// </summary>
        private void LimpaDadosPreAtend()
        {
            //ddlSexoUsuAteMed.SelectedValue = "M";
            //txtDtNascUsuAteMed
            txtIdadeUsuAteMed.Text = txtPesoUsuPreAtend.Text = txtAltuUsuPreAtend.Text = txtTemper.Text = txtPresUsuPreAtend.Text =
            txtHiperPreAtend.Text = txtTempFumanPreAtend.Text = txtTempAlcooPreAtend.Text = txtMarcPass.Text = txtValvula.Text = txtAlergia.Text =
            txtCirurPreAtend.Text = txtMedicUsoContiPreAtend.Text = txtMedicUsoContiPreAtend.Text = txtCidAtendMed.Text = txtDescCID.Text = txtDiagAtendMed.Text =
            txtAnamAtendMed.Text = "";

            lnkEfetAtendMed.Enabled = true;

            ddlDiabetsPreAtend.SelectedValue = ddlFumanPreAtend.SelectedValue = ddlAlcooPreAtend.SelectedValue = ddlDores.SelectedValue = ddlEnjoo.SelectedValue =
            ddlFebre.SelectedValue = ddlDores.SelectedValue = ddlVomito.SelectedValue = "N";
            chkDiabetsPreAtend.Checked = chkHipertPreAtend.Checked = chkMarcPass.Checked = chkValvulas.Checked = chkAlergia.Checked = chkCirurPreAtend.Checked = false;
        }

        /// <summary>
        /// Método que altera a cor da linha da consulta que estiver atrasada
        /// </summary>
        private void VerificaConsultaAtrasada()
        {
            foreach (GridViewRow li in grdAgendConsulMedic.Rows)
            {
                string hr = (((HiddenField)li.Cells[0].FindControl("hidCoHr")).Value).Replace(":", "");
                string hratu = DateTime.Now.ToShortTimeString().Replace(":", "");
                if (int.Parse(hr) <= int.Parse(hratu))
                {
                    //li.BackColor = System.Drawing.Color.Tomato;
                    li.BackColor = System.Drawing.Color.Coral;
                }

                //Se for registro antigo(um dos 10 antigos que são também apresentados), destaca ele em cor salmão
                if (((HiddenField)li.Cells[0].FindControl("hidAntigos")).Value == "1")
                    li.BackColor = System.Drawing.Color.AntiqueWhite;
            }
        }

        /// <summary>
        /// Este método é responsável por verificar qual o CheckBox está selecionado, e abrir a aba corretamente de acordo com o mesmo
        /// </summary>
        private void VerificaSelecaoMenu()
        {
            //    //Esta parte é responsável por verificar qual a opção do menu que está selecionada, e abrir a aba corretamente, pois normalmente no postback se perde a aba aberta.
            if (chkSelPacien.Checked)
                ControlaTabs("SLP", "");
            else if (chkAtdMedico.Checked)
                ControlaTabs("ATM", "");
            else if (chkReqMedicPaci.Checked)
                ControlaTabs("RCM", "");
            else if (chkReqExamPaci.Checked)
                ControlaTabs("RQE", "");
            else if (chkRegResExame.Checked)
                ControlaTabs("RSE", "");
            else if (chkReqSevAmbuPaci.Checked)
                ControlaTabs("RSA", "");
            else if (chkRegAtestMedcPaci.Checked)
                ControlaTabs("AME", "");
            else if (chkResMedicamentos.Checked)
                ControlaTabs("MED", "");
            else if (chkEncaMedicPaci.Checked)
                ControlaTabs("ENM", "");
            else if (chkEncaIntern.Checked)
                ControlaTabs("ENI", "");
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
        /// Responsável por carregar as unidades no controle de requisição de exames
        /// </summary>
        private void CarregaUnidExame(DropDownList ddl)
        {
            var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                       where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                       select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP }).OrderBy(w => w.NO_FANTAS_EMP).ToList();

            ddl.DataTextField = "NO_FANTAS_EMP";
            ddl.DataValueField = "CO_EMP";
            ddl.DataSource = res;
            ddl.DataBind();

            ddl.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Verifica qual a origem do atendimento, se vem de encaminhamento ou de consulta
        /// </summary>
        /// <param name="grd"></param>
        /// <returns></returns>
        private bool VerificaOrigemAtendimento(GridView grd)
        {
            bool marcado = false;
            foreach (GridViewRow li in grd.Rows)
            {
                CheckBox chk = (((CheckBox)li.Cells[0].FindControl("chkselectEn")));

                if (marcado == false)
                {
                    if (chk.Checked)
                        marcado = true;
                }
            }
            return marcado;
        }

        /// <summary>
        /// Carrega as Classificações de risco padrão
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregaClassRisco(DropDownList ddl)
        {
            AuxiliCarregamentos.CarregaClassificacaoRisco(ddl, false);
        }

        /// <summary>
        /// Atualiza o código do pre-atendimento no cadastro do atendimento
        /// </summary>
        private void AtualizaPreAtendAtend()
        {
            TBS219_ATEND_MEDIC t2 = TBS219_ATEND_MEDIC.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimentoMedico.Value));
            t2.TBS194_PRE_ATENDReference.Load();
            t2.TBS194_PRE_ATEND = TBS194_PRE_ATEND.RetornaPelaChavePrimaria(int.Parse(hidCoPreAtend.Value));
            t2.CO_EMP_CADAS = LoginAuxili.CO_EMP;
            t2.TBS194_PRE_ATENDReference.Load();
            TBS219_ATEND_MEDIC.SaveOrUpdate(t2, true);
        }

        /// <summary>
        /// Carrega as CID's já selecionanda a devida de acordo com o código recebido como parâmetro
        /// </summary>
        /// <param name="ddlCID"></param>
        /// <param name="cid"></param>
        private void CarregaCIDSelec(DropDownList ddlCID, string cid)
        {
            var res = (from tb117 in TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros()
                       select new { tb117.IDE_CID, tb117.NO_CID }).OrderBy(w => w.NO_CID).ToList();

            ddlCID.DataTextField = "NO_CID";
            ddlCID.DataValueField = "IDE_CID";
            ddlCID.DataSource = res;
            ddlCID.DataBind();

            ddlCID.Items.Insert(0, new ListItem("Selecione", ""));

            if ((res != null) && (!string.IsNullOrEmpty(cid)))
                ddlCID.SelectedValue = cid;
        }

        /// <summary>
        /// Carrega as UF's já selecionando a devida de acordo com o código recebido como parâmetro
        /// </summary>
        /// <param name="ddluf"></param>
        /// <param name="uf"></param>
        private void CarregaUFSelec(DropDownList ddluf, string ufS)
        {
            var res = (from uf in TB74_UF.RetornaTodosRegistros()
                       select new { uf.CODUF }).ToList();

            ddluf.DataTextField = "CODUF";
            ddluf.DataValueField = "CODUF";
            ddluf.DataSource = res;
            ddluf.DataBind();

            if ((res != null) && (!string.IsNullOrEmpty(ufS)))
                ddluf.SelectedValue = ufS;
        }

        /// <summary>
        /// Carrega os responsáveis já selecionando o devido de acordo com o código recebido como parâmetro
        /// </summary>
        /// <param name="ddlResp"></param>
        /// <param name="co_resp"></param>
        private void CarregaResponsaveis(DropDownList ddlResp, string co_resp)
        {
            var res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                       select new { tb108.NO_RESP, tb108.CO_RESP }).ToList();

            ddlResp.DataTextField = "NO_RESP";
            ddlResp.DataValueField = "CO_RESP";
            ddlResp.DataSource = res;
            ddlResp.DataBind();

            if ((res != null) && (!string.IsNullOrEmpty(co_resp)))
                ddlResp.SelectedValue = co_resp;
        }

        /// <summary>
        /// Calcula e retorna a idade em string de acordo com data recebida como parâmetro
        /// </summary>
        private string CalculaIdadeUsu(DateTime dt)
        {
            int anos = DateTime.Now.Year - dt.Year;

            if (DateTime.Now.Month < dt.Month || (DateTime.Now.Month == dt.Month && DateTime.Now.Day < dt.Day))
                anos--;

            string idade = anos.ToString();

            return idade;
        }

        /// <summary>
        /// Carrega as informações do encaminhamento médico e pré-atendimento nos campos correspondentes.
        /// </summary>
        /// <param name="ID_ENCAM_MEDIC"></param>
        private void carregaInfosAtendi(int ID_ENCAM_MEDIC)
        {
            var res = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs195.CO_ALU equals tb07.CO_ALU
                       where tbs195.ID_ENCAM_MEDIC == ID_ENCAM_MEDIC
                       select new { tbs195.ID_PRE_ATEND, tb07.CO_SEXO_ALU, tb07.DT_NASC_ALU, tb07.CO_ALU }).FirstOrDefault();

            //Atribui algumas informações do cadastro de Pacientes aos campos correspondentes
            //ddlSexoUsuAteMed.SelectedValue = res.CO_SEXO_ALU;
            txtDtNascUsuAteMed.Text = (res.DT_NASC_ALU.HasValue ? res.DT_NASC_ALU.Value.ToString("dd/MM/yyyy") : "");
            txtIdadeUsuAteMed.Text = (res.DT_NASC_ALU.HasValue ? CalculaIdadeUsu(res.DT_NASC_ALU.Value) : "");

            lblIdadeCalc.Text = res.DT_NASC_ALU.HasValue ? CarregaIdadePaciente(res.DT_NASC_ALU.Value) : "";
            lblDtNascPaci.Text = res.DT_NASC_ALU.HasValue ? "Nasc " + res.DT_NASC_ALU.Value.ToString("dd/MM/yyyy") : "Nasc - ";

            //------------> Tamanho da imagem de Aluno
            upImagemAluno.ImagemLargura = 90;
            upImagemAluno.ImagemAltura = 98;
            upImagemAluno.MostraProcurar = false;

            //Resgata a foto do Paciente
            var tb07ob = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == res.CO_ALU).FirstOrDefault();
            tb07ob.ImageReference.Load();

            if (tb07ob.Image != null)
                upImagemAluno.CarregaImagem(tb07ob.Image.ImageId);
            else
                upImagemAluno.CarregaImagem(0);

            //Verifica se existe e atribui as informações do pré-atendimento
            if (res.ID_PRE_ATEND.HasValue)
            {
                var tbs194 = TBS194_PRE_ATEND.RetornaPelaChavePrimaria(res.ID_PRE_ATEND.Value);

                //Leitura
                txtAltuUsuPreAtend.Text = tbs194.NU_ALTU.ToString();
                txtPesoUsuPreAtend.Text = tbs194.NU_PESO.ToString();
                txtPresUsuPreAtend.Text = tbs194.NU_PRES_ARTE;
                txtHoraPressArt.Text = tbs194.HR_PRES_ARTE;
                txtTemper.Text = tbs194.NU_TEMP.ToString();
                txtHoraTemper.Text = tbs194.HR_TEMP;
                txtGlicem.Text = (tbs194.NU_GLICE.HasValue ? tbs194.NU_GLICE.Value.ToString() : "");
                txtHoraGlicem.Text = tbs194.HR_GLICE;

                //Registro de Risco
                chkDiabetsPreAtend.Checked = (tbs194.FL_DIABE == "S" ? true : false);
                ddlDiabetsPreAtend.SelectedValue = (tbs194.FL_DIABE != "N" ? tbs194.DE_DIABE : "N");
                chkHipertPreAtend.Checked = (tbs194.FL_HIPER_TENSO == "S" ? true : false);
                txtHiperPreAtend.Text = (tbs194.DE_HIPER_TENSO != null ? tbs194.DE_HIPER_TENSO : "");
                ddlFumanPreAtend.SelectedValue = tbs194.FL_FUMAN.ToString();
                txtTempFumanPreAtend.Text = (tbs194.NU_TEMPO_FUMAN.HasValue ? tbs194.NU_TEMPO_FUMAN.ToString() : "");
                ddlAlcooPreAtend.SelectedValue = tbs194.FL_ALCOO.ToString();
                txtTempAlcooPreAtend.Text = (tbs194.NU_TEMPO_ALCOO.HasValue ? tbs194.NU_TEMPO_ALCOO.ToString() : "");
                chkCirurPreAtend.Checked = (tbs194.FL_CIRUR == "S" ? true : false);
                txtCirurPreAtend.Text = tbs194.DE_CIRUR != null ? tbs194.DE_CIRUR : "";
                chkAlergia.Checked = (tbs194.FL_ALERG == "S" ? true : false);
                txtAlergia.Text = (tbs194.DE_ALERG != null ? tbs194.DE_ALERG : "");
                chkMarcPass.Checked = (tbs194.FL_MARCA_PASSO == "S" ? true : false);
                txtMarcPass.Text = (tbs194.DE_MARCA_PASSO != null ? tbs194.DE_MARCA_PASSO : "");
                chkValvulas.Checked = (tbs194.FL_VALVU == "S" ? true : false);
                txtValvula.Text = (tbs194.DE_VALVU != null ? tbs194.DE_VALVU : "");
                ddlEnjoo.SelectedValue = tbs194.FL_SINTO_ENJOO;
                ddlVomito.SelectedValue = tbs194.FL_SINTO_VOMIT;
                ddlFebre.SelectedValue = tbs194.FL_SINTO_FEBRE;
                ddlDores.SelectedValue = tbs194.FL_SINTO_DORES;
                txtDtDor.Text = tbs194.DT_DOR.ToString();
                txtHrDor.Text = (tbs194.DT_DOR.HasValue ? tbs194.DT_DOR.Value.ToString("HH:mm") : DateTime.Now.ToString("HH:mm"));

                //Medicação
                txtMedicUsoContiPreAtend.Text = tbs194.DE_MEDIC_USO_CONTI;
                txtMedicacaoAdmin.Text = tbs194.DE_MEDIC;

                //Informações Prévias
                txtSintomas.Text = tbs194.DE_SINTO;

                //Código
                hidCoPreAtend.Value = tbs194.ID_PRE_ATEND.ToString();
            }
        }

        /// <summary>
        /// Método responsável por carregar os médicos ou trazer apenas o médico logado se este o for (Traz apenas médicos que possuem consultas)
        /// </summary>
        private void CarregaMedicosConsultas()
        {
            //Verifica se o usuário logado é profissional de saúde, caso seja mostra apenas o nome dele já selecionado e sem poder alterar, caso não seja mostra todos
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {

                var resCons = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb03o in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03o.CO_COL
                               select new { tb03o.CO_COL, tb03o.NO_COL }).OrderBy(w => w.NO_COL).ToList().Distinct();

                ddlMedicoCo.DataTextField = "NO_COL";
                ddlMedicoCo.DataValueField = "CO_COL";
                ddlMedicoCo.DataSource = resCons;
                ddlMedicoCo.DataBind();

                ddlMedicoCo.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
            {
                var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                           where tb03.CO_COL == LoginAuxili.CO_COL
                           select new { tb03.CO_COL, tb03.NO_COL }).ToList();

                ddlMedicoCo.DataValueField = "CO_COL";
                ddlMedicoCo.DataTextField = "NO_COL";
                ddlMedicoCo.DataSource = res;
                ddlMedicoCo.DataBind();

                ddlMedicoCo.Enabled = false;
            }
        }

        /// <summary>
        /// Método responsável por carregar os médicos ou trazer apenas o médico logado se este o for (Traz apenas médicos que possuem encaminhamentos e consultas)
        /// </summary>
        private void CarregaMedicosEncaConsul()
        {
            //Verifica se o usuário logado é profissional de saúde, caso seja mostra apenas o nome dele já selecionado e sem poder alterar, caso não seja mostra todos
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {

                var resCons = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb03o in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03o.CO_COL
                               select new { tb03o.CO_COL, tb03o.NO_COL }).OrderBy(w => w.NO_COL).ToList().Distinct();

                ddlMedico.DataTextField = "NO_COL";
                ddlMedico.DataValueField = "CO_COL";
                ddlMedico.DataSource = resCons;
                ddlMedico.DataBind();

                var resEnca = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                               join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs195.CO_COL equals tb03.CO_COL
                               select new { tb03.CO_COL, tb03.NO_COL }).OrderByDescending(w => w.NO_COL).OrderBy(w => w.NO_COL).ToList().Distinct();

                foreach (var li in resEnca)
                {
                    //if (ddlMedico.Items.FindByValue(li.CO_COL.ToString()).Value != li.CO_COL.ToString())
                    //{
                    ddlMedico.Items.Insert(0, new ListItem(li.NO_COL, li.CO_COL.ToString()));
                    //}
                }

                ddlMedico.Items.Insert(0, new ListItem("Todos", "0"));

                ddlMedico.Items.Insert(1, new ListItem("=============== ENCAMINHAMENTOS ================", "ENC"));
                //ddlMedico.Items[1].Attributes.Add("style", "font-weight:bold");

                int aux = resCons.Count();
                ddlMedico.Items.Insert(aux, new ListItem("================== CONSULTAS ===================", "CON"));
                //ddlMedico.Items[aux].Attributes.Add("style", "font-weight:bold");
                ddlMedico.Items[aux].Attributes.Add("background-color:", "red");
            }
            else
            {
                var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                           where tb03.CO_COL == LoginAuxili.CO_COL
                           select new { tb03.CO_COL, tb03.NO_COL }).ToList();

                ddlMedico.DataValueField = "CO_COL";
                ddlMedico.DataTextField = "NO_COL";
                ddlMedico.DataSource = res;
                ddlMedico.DataBind();

                ddlMedico.Enabled = false;
            }
        }

        /// <summary>
        /// Método responsável por carregar os médicos ou trazer apenas o médico logado se este o for (Traz apenas médicos que possuem encaminhamentos)
        /// </summary>
        private void carregaMedicosEncam()
        {
            //Verifica se o usuário logado é profissional de saúde, caso seja mostra apenas o nome dele já selecionado e sem poder alterar, caso não seja mostra todos
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                var resEnca = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                               join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs195.CO_COL equals tb03.CO_COL
                               select new { tb03.CO_COL, tb03.NO_COL }).OrderByDescending(w => w.NO_COL).OrderBy(w => w.NO_COL).ToList().Distinct();

                ddlMedicoEn.DataTextField = "NO_COL";
                ddlMedicoEn.DataValueField = "CO_COL";
                ddlMedicoEn.DataSource = resEnca;
                ddlMedicoEn.DataBind();

                ddlMedicoEn.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
            {
                var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                           where tb03.CO_COL == LoginAuxili.CO_COL
                           select new { tb03.CO_COL, tb03.NO_COL }).ToList();

                ddlMedicoEn.DataValueField = "CO_COL";
                ddlMedicoEn.DataTextField = "NO_COL";
                ddlMedicoEn.DataSource = res;
                ddlMedicoEn.DataBind();

                ddlMedicoEn.Enabled = false;
            }
        }

        /// <summary>
        /// Trata a linha dos medicamentos que estão em falta na grid de reserva de medicamentos
        /// </summary>
        private void TrataMedicamentosFaltaGrid()
        {
            foreach (GridViewRow li in grdMedicReceitados.Rows)
            {
                CheckBox chkFP = (((CheckBox)li.Cells[0].FindControl("ckFP")));
                decimal qtEstoque = decimal.Parse(((HiddenField)li.Cells[0].FindControl("hidQtEstoque")).Value);

                //Tratamentos especial para as linhas dos medicamentos que não estiverem disponíveis em estoque ou não forem de Farmácia Popular
                if ((chkFP.Checked == false) || (qtEstoque <= 0))
                {
                    li.Attributes.Add("onMouseOver", "this.style.cursor='default';");
                    li.BackColor = System.Drawing.Color.WhiteSmoke;
                }
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Unidades de Medida
        /// </summary>
        private void CarregaUnidadesMedidas(DropDownList ddl)
        {
            ddl.DataSource = TB89_UNIDADES.RetornaTodosRegistros().Where(u => (u.FL_CATEG_UNID == "T") || (u.FL_CATEG_UNID == "S")).OrderBy(u => u.SG_UNIDADE);

            ddl.DataTextField = "SG_UNIDADE";
            ddl.DataValueField = "CO_UNID_ITEM";
            ddl.DataBind();
        }

        /// <summary>
        /// Método padrão para carregamento das Unidades
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregaUnidades(DropDownList ddl)
        {
            var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                       select new
                       {
                           tb25.CO_EMP,
                           tb25.NO_FANTAS_EMP
                       });

            ddl.DataValueField = "CO_EMP";
            ddl.DataTextField = "NO_FANTAS_EMP";

            ddl.DataSource = res;
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Todas", "0"));
        }

        /// <summary>
        /// Método padrão para carregamento dos departamentos para requisição de serviços ambulatoriais
        /// </summary>
        private void CarregaDepartamentoReqAmbu()
        {
            AuxiliCarregamentos.CarregaDepartamentos(ddlLocalServAmbu, LoginAuxili.CO_EMP, false, false, true);
        }

        /// <summary>
        /// Método responsável por salvar em session as informações para posteriormente salvar os dados da reserva de medicamentos
        /// </summary>
        private void CarregaSessionReserMedicamentos(int coProd)
        {
            int idReser = (!string.IsNullOrEmpty(hidCoReservaMedicamentos.Value) ? int.Parse(hidCoReservaMedicamentos.Value) : 0);
            TB092_RESER_MEDIC tb092 = (idReser != 0 ? TB092_RESER_MEDIC.RetornaPelaChavePrimaria(idReser) : new TB092_RESER_MEDIC());
            if (string.IsNullOrEmpty(hidCoReservaMedicamentos.Value))
            {
                #region Grava a reserva de medicamento

                int? ideCid = SalvaCID();

                #region Validações
                if (ideCid == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Código Internacional de Doença (CID) não está informado na aba do atendimento médico");
                    return;
                }

                if (string.IsNullOrEmpty(hidCoPac.Value))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Paciente não foi selecionado, favor providenciar.");
                    return;
                }

                if (string.IsNullOrEmpty(hidIdAtendimentoMedico.Value))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor efetivar o atendimento antes de prosseguir");
                    return;
                }

                #endregion

                //Coleta os dados da instituição logada
                var dadosInst = (from tb000 in TB000_INSTITUICAO.RetornaTodosRegistros()
                                 where tb000.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                 select new
                                 {
                                     tb000.ORG_NOME_ORGAO,
                                     tb000.ORG_NUMERO_CNPJ,
                                 }).FirstOrDefault();

                int coCol = int.Parse(hidCoProfSaud.Value);
                var dadosMedico = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                   where tb03.CO_COL == coCol
                                   select new
                                   {
                                       tb03.NO_COL,
                                       tb03.NU_ENTID_PROFI,
                                       tb03.CO_ESTA_ENDE_COL,
                                       tb03.CO_CNES,
                                   }).FirstOrDefault();

                int coAlu = int.Parse(hidCoPac.Value);
                var dadosPaciente = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                     where tb07.CO_ALU == coAlu
                                     select new
                                     {
                                         tb07.CO_ALU,
                                         tb07.TB108_RESPONSAVEL,
                                     }).FirstOrDefault();

                int coResp = dadosPaciente.TB108_RESPONSAVEL != null ? dadosPaciente.TB108_RESPONSAVEL.CO_RESP : dadosPaciente.CO_ALU;

                #region Sequencial NR Registro

                string coUnid = LoginAuxili.CO_UNID.ToString();
                int coEmp = LoginAuxili.CO_EMP;
                string ano = DateTime.Now.Year.ToString().Substring(2, 2);

                var res = (from tbs092pesq in TB092_RESER_MEDIC.RetornaTodosRegistros()
                           where tbs092pesq.CO_EMP == coEmp && tbs092pesq.CO_RESER_MEDIC != null
                           select new { tbs092pesq.CO_RESER_MEDIC }).OrderByDescending(w => w.CO_RESER_MEDIC).FirstOrDefault();

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
                    seq = res.CO_RESER_MEDIC.Substring(7, 6);
                    seq2 = int.Parse(seq);
                }

                seqConcat = seq2 + 1;
                seqcon = seqConcat.ToString().PadLeft(6, '0');

                tb092.CO_RESER_MEDIC = "RM" + ano + coUnid.PadLeft(3, '0') + seqcon;

                #endregion

                tb092.CO_RESER_ANO = DateTime.Now.Year.ToString();
                //tb092.CO_RESER_MEDIC = pegaRegistroReserv(DateTime.Now.Year.ToString(), LoginAuxili.CO_EMP);
                tb092.DT_RESER_MEDIC = DateTime.Now;
                tb092.NU_PESO_USUA = (!string.IsNullOrEmpty(txtPesoUsuPreAtend.Text) ? decimal.Parse(txtPesoUsuPreAtend.Text) : (decimal?)null);
                tb092.NU_ALTU_USUA = (!string.IsNullOrEmpty(txtAltuUsuPreAtend.Text) ? decimal.Parse(txtAltuUsuPreAtend.Text) : (decimal?)null);
                tb092.TB117_CODIGO_INTERNACIONAL_DOENCA = TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaPelaChavePrimaria(ideCid.Value);
                tb092.DE_DIAG = txtDiagAtendMed.Text;
                tb092.DE_ANAM = txtAnamAtendMed.Text;
                tb092.TP_INFO = "R";
                tb092.NO_INST_SOLI = dadosInst.ORG_NOME_ORGAO;
                tb092.NU_INST_SOLI_CNPJ = dadosInst.ORG_NUMERO_CNPJ.ToString();
                tb092.CO_INST_SOLI_CNES = dadosInst.ORG_NUMERO_CNPJ.ToString();
                tb092.NO_MEDI_SOLI = dadosMedico.NO_COL;
                tb092.NU_MEDI_SOLI_CRM = dadosMedico.NU_ENTID_PROFI ?? "0";
                tb092.CO_MEDI_SOLI_UF = dadosMedico.CO_ESTA_ENDE_COL;
                tb092.NU_MEDI_SOLI_CNES = dadosMedico.CO_CNES ?? "";
                tb092.FL_DOCU_RECEI = "S";
                tb092.FL_DOCU_JUDIC = "N";
                tb092.CO_EMP = LoginAuxili.CO_EMP;
                tb092.CO_COL = LoginAuxili.CO_COL;
                tb092.CO_RESP = coResp;
                tb092.CO_ALU = coAlu;
                tb092.DT_CADAS_RESER_MEDIC = DateTime.Now;
                tb092.ST_RESER_MEDIC = "A";
                tb092.TBS219_ATEND_MEDIC = TBS219_ATEND_MEDIC.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimentoMedico.Value));
                tb092.CO_ATEND_MEDIC = TBS219_ATEND_MEDIC.RetornaPelaChavePrimaria(int.Parse(hidIdAtendimentoMedico.Value)).CO_ATEND_MEDIC;

                tb092 = TB092_RESER_MEDIC.SaveOrUpdate(tb092);
                hidCoReservaMedicamentos.Value = tb092.ID_RESER_MEDIC.ToString();

                #endregion
            }

            #region Grava os items da reserva
            TB094_ITEM_RESER_MEDIC tb094;

            var resdad = TB092_RESER_MEDIC.RetornaPelaChavePrimaria(tb092.ID_RESER_MEDIC);

            tb094 = new TB094_ITEM_RESER_MEDIC();

            var dadosAlu = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                            where tb07.CO_ALU == resdad.CO_ALU
                            select new
                            {
                                tb07.CO_ALU,
                                tb07.TB108_RESPONSAVEL,
                            }).FirstOrDefault();

            int coRespba = dadosAlu.TB108_RESPONSAVEL != null ? dadosAlu.TB108_RESPONSAVEL.CO_RESP : dadosAlu.CO_ALU;

            tb094.TB092_RESER_MEDIC = tb092;
            tb094.TB90_PRODUTO = TB90_PRODUTO.RetornaTodosRegistros().Where(p => p.CO_PROD == coProd).FirstOrDefault();
            tb094.QT_MES1 = (!string.IsNullOrEmpty(txtQTM1.Text) ? int.Parse(txtQTM1.Text) : 0);
            tb094.QT_MES2 = (!string.IsNullOrEmpty(txtQTM2.Text) ? int.Parse(txtQTM2.Text) : 0);
            tb094.QT_MES3 = (!string.IsNullOrEmpty(txtQTM3.Text) ? int.Parse(txtQTM3.Text) : 0);
            tb094.QT_MES4 = (!string.IsNullOrEmpty(txtQTM4.Text) ? int.Parse(txtQTM4.Text) : 0);
            tb094.QT_ENTR_MES1 = 0;
            tb094.QT_ENTR_MES2 = 0;
            tb094.QT_ENTR_MES3 = 0;
            tb094.QT_ENTR_MES4 = 0;
            tb094.CO_ALU = resdad.CO_ALU;
            tb094.CO_RESP = coRespba;
            tb094.CO_COL = LoginAuxili.CO_COL;
            tb094.CO_EMP = LoginAuxili.CO_EMP;

            TB094_ITEM_RESER_MEDIC.SaveOrUpdate(tb094);

            #endregion

            ControlaTabs("MED", "");
            CarregaReservaMedicamentos(int.Parse(hidIdAtendimentoMedico.Value));
            AtivaChecks(true);
        }

        /// <summary>
        /// Carregamento padrão dos departamentos
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="ddlUnid"></param>
        private void CarregaDepartamento(DropDownList ddl, DropDownList ddlUnid)
        {
            int coEmp = int.Parse(ddlUnid.SelectedValue);

            var res = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                       where (coEmp != 0 ? tb14.TB25_EMPRESA.CO_EMP == coEmp : 0 == 0)
                       select new
                       {
                           tb14.CO_DEPTO,
                           tb14.NO_DEPTO
                       });

            ddl.DataTextField = "NO_DEPTO";
            ddl.DataValueField = "CO_DEPTO";

            ddl.DataSource = res;
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carregamento padrão para os tipos de exames
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregaTipoExame(DropDownList ddl)
        {
            var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                       where tbs356.CO_TIPO_PROC_MEDI == "EX"
                       select new
                       {
                           tbs356.NM_PROC_MEDI,
                           tbs356.ID_PROC_MEDI_PROCE
                       });

            ddl.DataTextField = "NM_PROC_MEDI";
            ddl.DataValueField = "ID_PROC_MEDI_PROCE";

            ddl.DataSource = res;
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// 
        /// </summary>
        private void CarregaGridHorario()
        {
            DateTime dtIni;
            if (!DateTime.TryParse(txtDtIniResCons.Text, out dtIni))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data de início do período do horário inválida");
                return;
            }

            DateTime dt;
            DateTime? dtFim = null;
            if (!DateTime.TryParse(txtDtFimResCons.Text, out dt))
            {
                dtFim = null;
            }
            else
            {
                dtFim = dt;
            }

            List<HorarioSaida> lHr = new List<HorarioSaida>();
            List<string> lHH = new List<string>();
            lHH.Add("07:30");
            lHH.Add("08:00");
            lHH.Add("08:30");
            lHH.Add("09:00");
            lHH.Add("09:30");
            lHH.Add("10:00");
            lHH.Add("10:30");
            lHH.Add("11:00");
            lHH.Add("11:30");
            lHH.Add("12:00");
            lHH.Add("12:30");
            lHH.Add("13:00");
            lHH.Add("13:30");
            lHH.Add("14:00");
            lHH.Add("14:30");
            lHH.Add("15:00");
            lHH.Add("15:30");
            HorarioSaida hs;
            string hr = "07:30:00";
            string dtHr = "";
            dtFim = dtFim != null ? dtFim : dtIni.AddDays(3);
            int i = 1;
            dt = dtIni;
            while (dt != dtFim)
            {
                foreach (string h in lHH)
                {
                    dtHr = dt.ToString("dd/MM/yyyy") + " - " + h;
                    hs = new HorarioSaida();
                    hs.hr = dtHr;
                    lHr.Add(hs);
                }
                dt = dtIni.AddDays(i);
                i++;
            }

            grdHorario.DataSource = lHr;
            grdHorario.DataBind();
        }

        public class HorarioSaida
        {
            public string hr { get; set; }
        }

        private void CarregaGridProfi()
        {
            int coEsp = int.Parse(ddlEspMedResCons.SelectedValue);
            int coEmp = int.Parse(ddlUnidResCons.SelectedValue);

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb03.CO_EMP equals tb25.CO_EMP
                       join tb100 in TB100_ESPECIALIZACAO.RetornaTodosRegistros() on tb03.CO_ESPEC equals tb100.CO_ESPEC
                       where tb03.FLA_PROFESSOR == "S"
                       && (coEsp != 0 ? tb03.CO_ESPEC == coEsp : coEsp == 0)
                       && (coEmp != 0 ? tb03.CO_EMP == coEmp : coEmp == 0)
                       select new GrdProfiSaida
                       {
                           CO_COL = tb03.CO_COL,
                           NO_COL = tb03.NO_COL,
                           NO_EMP = tb25.sigla,
                           DE_ESP = tb100.NO_SIGLA_ESPEC
                       });

            grdProfi.DataSource = res;
            grdProfi.DataBind();
        }

        public class GrdProfiSaida
        {
            public int CO_COL { get; set; }
            public string NO_COL { get; set; }
            public string NO_EMP { get; set; }
            public string DE_ESP { get; set; }
        }

        /// <summary>
        /// Método que carrega o dropdown de UFs
        /// </summary>
        /// <param name="ddlUF">DropDown de UF</param>
        private void CarregaUfs(DropDownList ddlUF)
        {
            ddlUF.DataSource = TB74_UF.RetornaTodosRegistros();

            ddlUF.DataTextField = "CODUF";
            ddlUF.DataValueField = "CODUF";
            ddlUF.DataBind();

            ddlUF.Items.Insert(0, new ListItem("", ""));
        }

        private void CarregaGridMedicamentos(int coAlu, int coEmp)
        {
            grdMedicamentos.DataSource = (from tb241 in TB241_ALUNO_ENDERECO.RetornaTodosRegistros()
                                          where tb241.TB07_ALUNO.CO_ALU == coAlu && tb241.TB07_ALUNO.CO_EMP == coEmp
                                          select new
                                          {
                                              tb241.TB238_TIPO_ENDERECO.NM_TIPO_ENDERECO,
                                              tb241.ID_ALUNO_ENDERECO,
                                              cep = tb241.CO_CEP.Insert(5, "-"),
                                              tb241.DS_ENDERECO,
                                              tb241.NR_ENDERECO,
                                              tb241.DS_COMPLEMENTO,
                                              tb241.TB905_BAIRRO.NO_BAIRRO
                                          });

            grdMedicamentos.DataBind();
        }

        /// <summary>
        /// Carrega as famílias na combo
        /// </summary>
        /// <param name="ddl">Combo que será populada</param>
        private void CarregaFamilia(DropDownList ddl)
        {
            var res = (from tb075 in TB075_FAMILIA.RetornaTodosRegistros()
                       select new
                       {
                           tb075.ID_FAMILIA,
                           tb075.NO_RESP_FAM
                       }).OrderBy(o => o.NO_RESP_FAM);

            ddl.DataTextField = "NO_RESP_FAM";
            ddl.DataValueField = "ID_FAMILIA";

            ddl.DataSource = res;
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega as Operadoras na combo "ddl"
        /// </summary>
        /// <param name="ddl">Combo que seceberá as operadoras</param>
        private void CarregaOperadora(DropDownList ddl)
        {
            var res = (from tb250 in TB250_OPERA.RetornaTodosRegistros()
                       select new
                       {
                           tb250.ID_OPER,
                           tb250.NOM_OPER
                       }).OrderBy(o => o.NOM_OPER);

            ddl.DataTextField = "NOM_OPER";
            ddl.DataValueField = "ID_OPER";

            ddl.DataSource = res;
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Não Informado", ""));
        }

        /// <summary>
        /// Método que carrega os planos das operadoras
        /// </summary>
        /// <param name="ddl">Combo que será populada</param>
        /// <param name="ddlOper">Combo de operadora utilizada na consulta</param>
        private void CarregaPlanos(DropDownList ddl, DropDownList ddlOper)
        {
            int idOper = ddlOper.SelectedValue != "" ? int.Parse(ddlOper.SelectedValue) : 0;

            var res = (from tb251 in TB251_PLANO_OPERA.RetornaTodosRegistros()
                       where tb251.TB250_OPERA.ID_OPER == idOper
                       select new
                       {
                           tb251.NOM_PLAN,
                           tb251.ID_PLAN
                       }).OrderBy(p => p.NOM_PLAN);

            ddl.DataTextField = "NOM_PLAN";
            ddl.DataValueField = "ID_PLAN";

            ddl.DataSource = res;
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        public class ComboResponsavel
        {
            public int coResp { get; set; }
            public string noResp { get; set; }
            public string nuCpf { get; set; }
            public string nome
            {
                get
                {
                    if (nuCpf != "")
                    {
                        string c = this.nuCpf.Length > 11 ? this.nuCpf + " - " + noResp : Convert.ToInt64(nuCpf).ToString(@"000\.000\.000\-00") + " - " + noResp;
                        return c;
                    }
                    else
                    {
                        return "***.***.***-** - " + noResp;
                    }
                }
            }
        }

        public class ComboUsuario
        {
            public string noUsu { get; set; }
            public int nuNire { get; set; }
            public int coAlu { get; set; }
            public string nome
            {
                get
                {
                    return nuNire.ToString().PadLeft(7, '0') + " - " + noUsu;
                }
            }
        }

        #endregion

        #region Pesquisas

        //======>Responsável por pesquisar na TELA DE ATENDIMENTO os CIDS existentes na tabela
        protected void imgPesqCID_OnClick(object sender, EventArgs e)
        {
            ControlaTabs("ATM", "");
            string CO_CID = txtCidAtendMed.Text.Trim();

            var res = (from tb117 in TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros()
                       where tb117.CO_CID == CO_CID
                       select new { tb117.NO_CID }).FirstOrDefault();

            if (res != null)
                txtDescCID.Text = res.NO_CID.ToUpper();
        }

        //======>Responsável por pesquisar na TELA DE ATENDIMENTO os CIDS existentes na tabela
        protected void imgPesqCID2_OnClick(object sender, EventArgs e)
        {
            ControlaTabs("ATM", "");
            string CO_CID = txtCidAtendMed2.Text.Trim();

            var res = (from tb117 in TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros()
                       where tb117.CO_CID == CO_CID
                       select new { tb117.NO_CID }).FirstOrDefault();

            if (res != null)
                txtDescCID2.Text = res.NO_CID.ToUpper();
        }

        //======>Responsável por pesquisar na TELA DE ATENDIMENTO os CIDS existentes na tabela
        protected void imgPesqCID3_OnClick(object sender, EventArgs e)
        {
            ControlaTabs("ATM", "");
            string CO_CID = txtCidAtendMed3.Text.Trim();

            var res = (from tb117 in TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros()
                       where tb117.CO_CID == CO_CID
                       select new { tb117.NO_CID }).FirstOrDefault();

            if (res != null)
                txtDescCID3.Text = res.NO_CID.ToUpper();
        }

        //======>Responsável por pesquisar na TELA DE EXAMES e preencher os devidos campos
        protected void lnkPesExaAtendMed_OnClick(object sender, EventArgs e)
        {
            int idAtend = (!string.IsNullOrEmpty(hidIdAtendimentoMedico.Value) ? int.Parse(hidIdAtendimentoMedico.Value) : 0);
            int idOper = CarregaOperaAtendimento(idAtend);
            CarregaExameMedicoReq(idOper);
        }

        //======>Responsável por pesquisar na TELA DE REQUISIÇÃO DE SERVIÇOS AMBULATORIAIS e preencher os devidos campos
        protected void imgPesqReqServAmbu_OnClick(object sender, EventArgs e)
        {
            int idAtend = (!string.IsNullOrEmpty(hidIdAtendimentoMedico.Value) ? int.Parse(hidIdAtendimentoMedico.Value) : 0);
            int idOper = CarregaOperaAtendimento(idAtend);
            CarregaProcedMedicoReq(idOper);
        }

        //======>Responsável por pesquisar na TELA DE REGISTRO DE ATESTADOS MÉDICOS e preencher os devidos campos
        protected void imgbtnPesqAtestado_OnClick(object sender, EventArgs e)
        {
            ControlaTabs("AME", "");

            if (txtCodAtestado.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o código que deverá ser pesquisado.");
                return;
            }

            //Método responsável por fazer uma busca pelo código informado e preencher os devidos campos
            int codAtest = int.Parse(txtCodAtestado.Text.Trim());
            var res = (from tb009 in TB009_RTF_DOCTOS.RetornaTodosRegistros()
                       where tb009.ID_DOCUM == codAtest
                       && tb009.TP_DOCUM == "DT"
                       select new { tb009.ID_DOCUM, tb009.NM_DOCUM }).FirstOrDefault();

            if (res != null)
            {
                txtAtestMedic.Text = res.NM_DOCUM;
                hidCodAtesMedic.Value = res.ID_DOCUM.ToString();
            }
        }

        //======>Responsável por pesquisar na TELA DE RESERVA DE MEDICAMENTOS e preencher os devidos campos
        protected void btnPesqMed_Click(object sender, EventArgs e)
        {
            ControlaTabs("MED", "");

            if (string.IsNullOrEmpty(txtMedicamento.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o Código do Medicamento que deverá ser pesquisado");
                return;
            }

            string CO_REFE_PROD = txtMedicamento.Text.Trim();

            var res = (from tb90 in TB90_PRODUTO.RetornaTodosRegistros()
                       join tb260 in TB260_GRUPO.RetornaTodosRegistros() on tb90.TB260_GRUPO.ID_GRUPO equals tb260.ID_GRUPO into l1
                       from ls in l1.DefaultIfEmpty()
                       where tb90.CO_REFE_PROD == CO_REFE_PROD
                       select new { tb90.NO_PROD, tb90.CO_PROD, noGrup = ls != null ? ls.NOM_GRUPO : " - " }).FirstOrDefault();

            hidCoProdResMedic.Value = res.CO_PROD.ToString();
            txtDescMedicamento.Text = res.NO_PROD;
            hidNoGrupo.Value = res.noGrup;
        }

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

        #region Controles

        /// <summary>
        /// Método responsável por abrir a modal com os exames cadastrados em procedimentos médicos
        /// </summary>
        private void AbreModalExames()
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                "mostraModalExames();",
                true
            );
        }

        /// <summary>
        /// Método responsável por abrir a modal com os Procedimentos Médicos cadastrados
        /// </summary>
        private void AbreModalProceMed()
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                "mostraModalProce();",
                true
            );
        }

        /// <summary>
        /// Ativa os checks
        /// </summary>
        /// <param name="status"></param>
        private void AtivaChecks(bool status)
        {
            chkResConsulta.Enabled = status;
            chkResConsulta.Checked = status;
            ControlaTabs("MED", "");
            //chkResExames.Enabled = status;
            chkResMedicamentos.Enabled = status;
            //chkAtdAmbulatorial.Enabled = status;
            chkAtdMedico.Enabled = status;
            //chkPrtAtendimento.Enabled = status;
            //chkOutros.Enabled = status;

            chkEndAddAlu.Enabled = status;
            chkTelAddAlu.Enabled = status;
            chkCuiEspAlu.Enabled = status;
            chkResAliAlu.Enabled = status;
        }

        /// <summary>
        /// Método que controla visibilidade das Tabs da tela de matrícula
        /// </summary>
        /// <param name="tab">Nome da tab</param>
        protected void ControlaTabs(string tab, string tabOrigem)
        {
            tabResConsultas.Style.Add("display", "none");
            tabResMedicamentos.Style.Add("display", "none");
            tabSelPacien.Style.Add("display", "none");
            tabReqMedic.Style.Add("display", "none");
            tabAtendMedic.Style.Add("display", "none");
            tabReqExam.Style.Add("display", "none");
            tabRegResExam.Style.Add("display", "none");
            tabReqServAmbu.Style.Add("display", "none");
            tabRegAtestMedc.Style.Add("display", "none");
            tabEncamMed.Style.Add("display", "none");
            tabEncamIntern.Style.Add("display", "none");
            tabDiagHist.Style.Add("display", "none");
            tabServAmbuHist.Style.Add("display", "none");
            tabAtestMedcHist.Style.Add("display", "none");
            tabExamHist.Style.Add("display", "none");
            tabConsHist.Style.Add("display", "none");
            tabhistReceiMedic.Style.Add("display", "none");

            if (tab == "ENA")
                tabResConsultas.Style.Add("display", "block");
            else if (tab == "ATM")
                tabAtendMedic.Style.Add("display", "block");
            else if (tab == "RCM")
                tabReqMedic.Style.Add("display", "block");
            else if (tab == "SLP")
                tabSelPacien.Style.Add("display", "block");
            else if (tab == "RQE")
                tabReqExam.Style.Add("display", "block");
            else if (tab == "RSE")
                tabRegResExam.Style.Add("display", "block");
            else if (tab == "RSA")
                tabReqServAmbu.Style.Add("display", "block");
            else if (tab == "AME")
                tabRegAtestMedc.Style.Add("display", "block");
            else if (tab == "MED")
                tabResMedicamentos.Style.Add("display", "block");
            else if (tab == "ENM")
                tabEncamMed.Style.Add("display", "block");
            else if (tab == "ENI")
                tabEncamIntern.Style.Add("display", "block");
            //===========================================> Históricos do Paciente <================================================
            else if (tab == "HDP")
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

            //Atualiza apenas os Update Panels necessários para deixar o método mais leve
            //if(tabOrigem == "SLC")
            //    updSelecPaci.Update();
            //else if(tabOrigem == "ATM")
            //    updAtenMedic.Update();
            //else if(tabOrigem == "RCM")
            //    updReceiMedic.Update();
        }

        /// <summary>
        /// Método para controlar Checks da tela de cadastro de matrícula
        /// </summary>
        /// <param name="chk"></param>
        protected void ControlaChecks(CheckBox chk)
        {
            chkCuiEspAlu.Checked = chkMenEscAlu.Checked =
            chkResAliAlu.Checked = chkEndAddAlu.Checked = chkTelAddAlu.Checked =
            chkResMedicamentos.Checked = false;

            chk.Checked = true;
        }

        /// <summary>
        /// Altera o atributo "enable" das opções do histórico do paciente
        /// </summary>
        /// <param name="acao"></param>
        private void CarregaChecksHistoricos(bool acao)
        {
            chkDiagPaci.Enabled = chkConsPaci.Enabled = chkMedicPaci.Enabled = chkHistReceiMedic.Enabled =
                chkExmPaci.Enabled = chkServAmbPaci.Enabled = chkAtestMedcPaci.Enabled
                = chkImgAtendPaci.Enabled = chkAtdMedico.Enabled = acao;
        }

        /// <summary>
        /// Altera o atributo "enable" das opções de Lançamento de Itens no paciente
        /// </summary>
        /// <param name="acao"></param>
        private void CarregaCheckLancamentos(bool acao)
        {
            chkReqMedicPaci.Enabled = chkReqExamPaci.Enabled = chkRegResExame.Enabled = chkReqSevAmbuPaci.Enabled =
                chkResMedicamentos.Enabled = chkRegAtestMedcPaci.Enabled = chkEncaMedicPaci.Enabled = chkEncaIntern.Enabled = acao;
        }

        #endregion

        #region Eventos personalizados

        private TBS195_ENCAM_MEDIC RetornaEncamDataUsu(DateTime dt, int coAlu)
        {
            DateTime dtI = dt;
            DateTime dtF = dt.AddHours(23);
            dtF = dtF.AddMinutes(59);
            dtF = dtF.AddMilliseconds(59);

            return (from t in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                    where t.DT_ENCAM_MEDIC >= dtI && t.DT_ENCAM_MEDIC <= dtF
                    && t.CO_ALU == coAlu
                    select t).OrderByDescending(o => o.DT_ENCAM_MEDIC).FirstOrDefault();
        }
        #endregion

        #region Eventos de componentes

        protected void chkselectEn_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            int coEncamMedic;

            // Valida se a grid de atividades possui algum registro
            if (grdEncamMedic.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdEncamMedic.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("chkselectEn");

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID != atual.ClientID)
                    {
                        chk.Checked = false;
                    }
                    else
                    {
                        if (chk.Checked)
                        {
                            LimpaDadosPreAtend();

                            coEncamMedic = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidIdEncam")).Value);
                            int coAlu = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoAlu")).Value);
                            string tpRisco = (((HiddenField)linha.Cells[0].FindControl("hidCoTpRisco")).Value);
                            string coresp = (((HiddenField)linha.Cells[0].FindControl("hidCoResp")).Value);
                            string coEspec = (((HiddenField)linha.Cells[0].FindControl("hidCoEspec")).Value);
                            string idOper = (((HiddenField)linha.Cells[0].FindControl("hidIdOper")).Value);
                            string idPlan = (((HiddenField)linha.Cells[0].FindControl("hidIdPlan")).Value);

                            //Coleta o código do profissional de saúde do encaminhamento clicado para selecionar na DropDownList superior o médico correspondente
                            int coCol = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoCol")).Value);
                            hidCoProfSaud.Value = coCol.ToString();

                            if (ddlMedico.Items.Contains(new ListItem("", coCol.ToString())))
                                ddlMedico.SelectedValue = coCol.ToString();

                            if (ddlMedicoEn.Items.Contains(new ListItem("", coCol.ToString())))
                                ddlMedicoEn.SelectedValue = coCol.ToString();

                            //Resgata informações do paciente para usá-las posteriormente
                            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                       where tb07.CO_ALU == coAlu
                                       select new { tb07.NO_ALU }).FirstOrDefault();

                            //Atribui valores à variáveis que serão usadas posteriormente
                            hidCoPac.Value = coAlu.ToString();
                            txtPaciente.Text = res.NO_ALU;
                            hidIdEncam.Value = coEncamMedic.ToString();
                            //hidCoTpRisco.Value = tpRisco;
                            ddlClassRisco.SelectedValue = tpRisco;
                            hidCoResp.Value = coresp;
                            hidIdOper.Value = idOper;
                            hidIdPlano.Value = idPlan;
                            hidIdEspec.Value = coEspec;

                            //Carrega as informações existentes no pré-atendimento e encaminhamento médico
                            carregaInfosAtendi(coEncamMedic);
                            CarregaChecksHistoricos(true);

                            //limpa a variável que guarda o código da consulta possivelmente selecionada 
                            hidCoConsul.Value = "";

                            CarregaInfosAbas(coAlu);

                            //Guarda a FLAG para saber se o Checkbox está sendo clicado para marcar ou desmarcar, neste caso, ele grava como para marcar.
                            HttpContext.Current.Session.Add("FL_Select_Grid_AT", "S");
                            //Guarda o Valor do Pré-Atendimento, para fins de posteriormente comparar este valor 
                            HttpContext.Current.Session.Add("VL_PreAtend_AT", coEncamMedic);

                            //Já que foi clicado para se atender um encaminhamento, é preciso desmarcar o registro selecionado na grid de Consultas
                            foreach (GridViewRow cons in grdAgendConsulMedic.Rows)
                            {
                                CheckBox chkEn = (((CheckBox)cons.Cells[0].FindControl("chkSelectCon")));
                                chkEn.Checked = false;
                            }

                            //Atende os padrões de selecionar o checkbox da aba seguinte e mostrar o label OK!
                            chkSelPacien.Checked = false;
                            lblSelPacien.Visible = true;
                            chkAtdMedico.Checked = true;
                            ControlaTabs("ATM", "SLC");
                        }
                        else
                        {
                            //Guarda a FLAG para saber se o Checkbox está sendo clicado para marcar ou desmarcar, neste caso, ele grava como desmarcada.
                            HttpContext.Current.Session.Add("FL_Select_Grid_AT", "N");
                            HttpContext.Current.Session.Remove("VL_PreAtend_AT");

                            lblSelPacien.Visible = false;
                            CarregaChecksHistoricos(false);


                            hidCoPac.Value = txtPaciente.Text = hidIdEncam.Value = hidIdEspec.Value
                                = ddlClassRisco.SelectedValue = hidCoTpRisco.Value
                                = hidIdOper.Value = hidIdPlano.Value = hidCoResp.Value = "";

                            //Se o usuário está clicando para desmarcar o registro, não faz sentido redirecioná-lo para outra aba
                            ControlaTabs("SLP", "SLC");
                        }

                        //updTopo.Update();
                        //updInfosAtend.Update();
                        //updHistPaci.Update();
                    }
                }
            }
        }

        protected void chkselectCon_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            int coConsul;

            // Valida se a grid de atividades possui algum registro
            if (grdAgendConsulMedic.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdAgendConsulMedic.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("chkselectCon");

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID != atual.ClientID)
                    {
                        chk.Checked = false;
                    }
                    else
                    {
                        if (chk.Checked)
                        {
                            LimpaDadosPreAtend();

                            coConsul = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoConsul")).Value);
                            int coAlu = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoAlu")).Value);
                            string coEspec = (((HiddenField)linha.Cells[0].FindControl("hidCoEspec")).Value);

                            //Coleta o código do profissional de saúde das Consultas clicado para selecionar na DropDownList superior o médico correspondente
                            int coCol = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoCol")).Value);
                            hidCoProfSaud.Value = coCol.ToString();

                            if (ddlMedico.Items.Contains(new ListItem("", coCol.ToString())))
                                ddlMedico.SelectedValue = coCol.ToString();

                            if (ddlMedicoCo.Items.Contains(new ListItem("", coCol.ToString())))
                                ddlMedicoCo.SelectedValue = coCol.ToString();

                            //Resgata informações do paciente para usá-las posteriormente
                            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                       where tb07.CO_ALU == coAlu
                                       select new { tb07.NO_ALU }).FirstOrDefault();

                            hidCoPac.Value = coAlu.ToString();
                            txtPaciente.Text = res.NO_ALU;
                            hidCoConsul.Value = coConsul.ToString();
                            hidIdEspec.Value = coEspec;

                            //Carrega as informações existentes no pré-atendimento e encaminhamento médico
                            CarregaChecksHistoricos(true);

                            //limpa a variável que guarda o código do encaminhamento possivelmente selecionado 
                            hidIdEncam.Value = "";

                            CarregaInfosAbas(coAlu);

                            //Atende os padrões de selecionar o checkbox da aba seguinte e mostrar o label OK!
                            chkSelPacien.Checked = false;
                            lblSelPacien.Visible = true;
                            chkAtdMedico.Checked = true;

                            //Já que foi clicado para se atender uma consulta, é preciso desmarcar o registro selecionado na grid de encaminhamentos
                            foreach (GridViewRow enc in grdEncamMedic.Rows)
                            {
                                CheckBox chkEn = (((CheckBox)enc.Cells[0].FindControl("chkselectEn")));
                                chkEn.Checked = false;
                            }


                            ControlaTabs("ATM", "SLC");
                        }
                        else
                        {
                            lblSelPacien.Visible = false;
                            CarregaChecksHistoricos(false);

                            hidCoPac.Value = txtPaciente.Text = hidIdEncam.Value = ddlClassRisco.SelectedValue
                                = hidCoTpRisco.Value = hidCoResp.Value = hidIdOper.Value = hidIdPlano.Value = hidIdEspec.Value = "";

                            //Se o usuário está clicando para desmarcar o registro, não faz sentido redirecioná-lo para outra aba
                            ControlaTabs("SLP", "SLC");
                        }

                        //updTopo.Update();
                        //updInfosAtend.Update();
                        //updHistPaci.Update();
                    }
                }
            }
        }

        protected void ckSelectResMedic_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            ControlaTabs("MED", "");
            // Valida se a grid de atividades possui algum registro
            if (grdMedicReceitados.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdMedicReceitados.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("ckSelectResMedic");

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID != atual.ClientID)
                    {
                        chk.Checked = false;
                    }
                    else
                    {
                        if (chk.Checked)
                        {
                            string qtd = ((HiddenField)linha.Cells[0].FindControl("hidQTD")).Value;
                            string uso = ((HiddenField)linha.Cells[0].FindControl("hidUSO")).Value;

                            txtQTM1.Text = qtd;

                            //Insere os valores em todos os meses caso tenha sido receitado uso contínuo
                            if (uso == "0")
                                txtQTM2.Text = txtQTM3.Text = txtQTM4.Text = qtd;
                        }
                        else
                            txtQTM1.Text = txtQTM2.Text = txtQTM3.Text = txtQTM4.Text = "";
                    }
                }
            }
        }

        protected void ddlMedico_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            VerificaSelecaoMenu();
            if (ddlMedico.SelectedValue != "0")
            {
                hidCoProfSaud.Value = ddlMedico.SelectedValue;
            }
            CarregaGridEncaminhamentos(int.Parse(ddlMedico.SelectedValue));
            CarregaGridConsultas(int.Parse(ddlMedico.SelectedValue));
        }

        protected void ddlMedicoEn_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            VerificaSelecaoMenu();
            if (ddlMedicoEn.SelectedValue != "0")
            {
                hidCoProfSaud.Value = ddlMedicoEn.SelectedValue;
            }
            CarregaGridEncaminhamentos(int.Parse(ddlMedicoEn.SelectedValue));
        }

        protected void ddlMedicoCo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            VerificaSelecaoMenu();
            if (ddlMedicoCo.SelectedValue != "0")
            {
                hidCoProfSaud.Value = ddlMedicoCo.SelectedValue;
            }
            CarregaGridConsultas(int.Parse(ddlMedicoCo.SelectedValue));
        }

        protected void ckSelectAt_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            // Valida se a grid de atividades possui algum registro
            if (grdAtesMedic.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdAtesMedic.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("ckSelectAt");

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID != atual.ClientID)
                    {
                        chk.Checked = false;
                    }
                    else
                    {
                        if (chk.Checked)
                            lnkImpAtesMedic.Enabled = true;
                        else
                            lnkImpAtesMedic.Enabled = false;
                    }
                }
            }
        }

        //======>Evento responsável por atualizar continuamente de 10 em 10 segundos a grid de encaminhamentos do médico
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //Esta parte é responsável por verificar qual a opção do menu que está selecionada, e abrir a aba corretamente, pois normalmente no postback se perde a aba aberta.
            VerificaSelecaoMenu();

            CarregaGridEncaminhamentos();

            if ((string)HttpContext.Current.Session["FL_Select_Grid_AT"] == "S")
            {
                selecionaGridEncamMedic();
            }
        }

        /// <summary>
        /// Devido ao método de reload na grid de Pré-Atendimento, ela perde a seleção do checkbox, este método coleta 
        /// </summary>
        private void selecionaGridEncamMedic()
        {
            CheckBox chk;
            string coPreAtend;
            // Valida se a grid de atividades possui algum registro
            if (grdEncamMedic.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdEncamMedic.Rows)
                {
                    coPreAtend = ((HiddenField)linha.Cells[0].FindControl("hidIdEncam")).Value;
                    int coPre = (int)HttpContext.Current.Session["VL_PreAtend_AT"];

                    if (int.Parse(coPreAtend) == coPre)
                    {
                        chk = (CheckBox)linha.Cells[0].FindControl("chkselectEn");
                        chk.Checked = true;
                    }
                }
            }
        }

        protected void imgCoMedic_OnClick(object sender, EventArgs e)
        {
            string CO_REFE_PROD = txtCodMedic.Text.Trim();
            var res = (from tb90 in TB90_PRODUTO.RetornaTodosRegistros()
                       where tb90.CO_REFE_PROD == CO_REFE_PROD
                       select new { tb90.CO_PROD, tb90.NO_PROD, tb90.NO_PRINCIPIO_ATIVO }).FirstOrDefault();

            if (res != null)
            {
                txtdescMedic.Text = res.NO_PROD;
                txtPrincAtivMedc.Text = (!string.IsNullOrEmpty(res.NO_PRINCIPIO_ATIVO) ? res.NO_PRINCIPIO_ATIVO.Length > 100 ? res.NO_PRINCIPIO_ATIVO.Substring(0, 100) : res.NO_PRINCIPIO_ATIVO : "");
                hidCoMedic.Value = res.CO_PROD.ToString();
            }

            ControlaTabs("RCM", "");
        }

        protected void OnClick_imgPesqExames(object sender, EventArgs e)
        {
            ControlaTabs("RQE", "");
            int idAtend = (!string.IsNullOrEmpty(hidIdAtendimentoMedico.Value) ? int.Parse(hidIdAtendimentoMedico.Value) : 0);
            int idOper = CarregaOperaAtendimento(idAtend);

            CarregaGruposExames(ddlGrupExamMOD, idOper);
            CarregaSubGruposExames(ddlSubGrupExamMOD, idOper, 0);

            //Carrega as Unidades para filtro dos exames apenas se o exame estiver marcado para ser executado na instituição
            if (chkExecuExameInsti.Checked)
                AuxiliCarregamentos.CarregaUnidade(ddlUnidExecExamMOD, LoginAuxili.ORG_CODIGO_ORGAO, false, false, true);


            //Filtra os exames de acordo com a unidade selecionada para execução
            int coEmpSelec = (!string.IsNullOrEmpty(ddlUnidReqExam.SelectedValue) ? int.Parse(ddlUnidReqExam.SelectedValue) : 0);
            CarregaGridExamesMedicosDisponiveis(0, 0, "", coEmpSelec, idOper);
            AbreModalExames();
        }

        protected void imgListaServAmbu_OnClick(object sender, EventArgs e)
        {
            ControlaTabs("RSA", "");

            int idAtend = (!string.IsNullOrEmpty(hidIdAtendimentoMedico.Value) ? int.Parse(hidIdAtendimentoMedico.Value) : 0);
            int idOper = CarregaOperaAtendimento(idAtend);

            CarregaGruposProcMedic(ddlGrupProcMOD, idOper);
            CarregaSubGruposProcMedic(ddlSubGrupProcMOD, idOper, 0);

            //Carrega as Unidades para filtro dos exames apenas se o exame estiver marcado para ser executado na instituição
            if (chkExecuServAmbuInst.Checked)
                AuxiliCarregamentos.CarregaUnidade(ddlUnidExecServAMbuMOD, LoginAuxili.ORG_CODIGO_ORGAO, false, false, true);

            //Filtra os exames de acordo com a unidade selecionada para execução
            int coEmpSelec = (!string.IsNullOrEmpty(ddlUnidExecServAMbuMOD.SelectedValue) ? int.Parse(ddlUnidExecServAMbuMOD.SelectedValue) : 0);
            CarregaGridProcedimentosMedicosDisponiveis(0, 0, "", coEmpSelec, idOper);
            AbreModalProceMed();
        }

        protected void lnkFicha_Click(object sender, EventArgs e)
        {

        }

        protected void ddlNomeUsuAteMed_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;

            TB07_ALUNO alu = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddl.SelectedValue));

            if (alu != null)
            {
                txtNisUsuAteMed.Text = alu.NU_NIRE.ToString();
                //ddlSexoUsuAteMed.SelectedValue = alu.CO_SEXO_ALU;
                txtDtNascUsuAteMed.Text = alu.DT_NASC_ALU != null ? alu.DT_NASC_ALU.Value.ToString("dd/MM/yyyy") : "";

                DateTime dtNasci = DateTime.Parse(txtDtNascUsuAteMed.Text);
                int anos = DateTime.Now.Year - dtNasci.Year;

                if (DateTime.Now.Month < dtNasci.Month || (DateTime.Now.Month == dtNasci.Month && DateTime.Now.Day < dtNasci.Day))
                    anos--;

                string idade = anos.ToString();


                txtIdadeUsuAteMed.Text = idade;
                //uppUsuAtendMed2.Update();
            }
            else
            {
                txtNisUsuAteMed.Text = "";
                //ddlSexoUsuAteMed.SelectedValue = "";
                //txtDtNascUsuAteMed.Text = "";
                txtIdadeUsuAteMed.Text = "";
                //uppUsuAtendMed2.Update();
            }

            ControlaTabs("ATM", "");
        }

        protected string pegaRegistroReserv(string ano, int coEmp)
        {
            string reg = "";
            int seq = 0;

            TB092_RESER_MEDIC lastReser = TB092_RESER_MEDIC.RetornaTodosRegistros().OrderByDescending(o => o.ID_RESER_MEDIC).FirstOrDefault();

            if (lastReser != null)
            {
                seq = int.Parse(lastReser.CO_RESER_MEDIC.ToString().Substring(7, 7));
                seq++;
                reg = ano + coEmp.ToString() + seq.ToString().PadLeft(7, '0');
            }
            else
            {
                reg = ano + coEmp.ToString() + "0000001";
            }

            return reg;
        }

        protected void ddlNomeUsu_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;

            TB07_ALUNO alu = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(ddl.SelectedValue));

            // Carrega os dados do encaminhamento médico
            TBS195_ENCAM_MEDIC enc = RetornaEncamDataUsu(DateTime.Parse(txtDataAtend.Text), alu.CO_ALU);

            // Carrega os dados do pré atendimento médico, se houver
            TBS194_PRE_ATEND preA = null;
            if (enc.ID_PRE_ATEND != null)
            {
                preA = TBS194_PRE_ATEND.RetornaPelaChavePrimaria(enc.ID_PRE_ATEND.Value);
            }

            if (alu != null)
            {
                //txtNisUsu.Text = alu.NU_NIRE.ToString();
                //ddlSexoUsuAteMed.SelectedValue = alu.CO_SEXO_ALU;
                //txtDtNascUsuAteMed.Text = alu.DT_NASC_ALU != null ? alu.DT_NASC_ALU.Value.ToString("dd/MM/yyyy") : "";

                //DateTime dtNasci = DateTime.Parse(txtDtNascUsuAteMed.Text);
                //int anos = DateTime.Now.Year - dtNasci.Year;

                //if (DateTime.Now.Month < dtNasci.Month || (DateTime.Now.Month == dtNasci.Month && DateTime.Now.Day < dtNasci.Day))
                //    anos--;

                //string idade = anos.ToString();


                #region Carrega os dados do encaminhamento



                #endregion


                //txtIdadeUsuAteMed.Text = idade;
                //uppUsuAtendMed2.Update();
                //UpdatePanel5.Update();
            }
            else
            {
                //txtNisUsu.Text = "";
                //ddlSexoUsuAteMed.SelectedValue = "";
                //txtDtNascUsuAteMed.Text = "";
                txtIdadeUsuAteMed.Text = "";
                //uppUsuAtendMed2.Update();
                //UpdatePanel5.Update();
            }

        }

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

        protected void ddlUnidReqExam_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlUnidExecExamMOD.SelectedValue = ddlUnidReqExam.SelectedValue; //Altera a unidade do filtro de acordo com a selecionada para execução do exame

            if (ddlUnidReqExam.SelectedValue != "")
            {
                ControlaTabs("RQE", "");
                AuxiliCarregamentos.CarregaDepartamentos(ddlLocalReqExam, int.Parse(ddlUnidReqExam.SelectedValue), false, false, true);
            }
        }

        protected void lnkFinAtendMedic_lnkFinAtendMedic(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidCoPac.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um paciente antes de finalizar o Atendimento");
                return;
            }

            //Chama o método responsável por salvar as informações do Atendimento
            //bool sucesso = SalvaAtendimento();

            //if (sucesso == false)
            //{
            //    return;
            //}

            //Atualiza a situação do encaminhamento médico para "Finalizado"

            if (!string.IsNullOrEmpty(hidIdEncam.Value))
            {
                int coEnca = int.Parse(hidIdEncam.Value);
                TBS195_ENCAM_MEDIC tbs195 = TBS195_ENCAM_MEDIC.RetornaPelaChavePrimaria(coEnca);
                tbs195.CO_SITUA_ENCAM_MEDIC = "F";
                TBS195_ENCAM_MEDIC.SaveOrUpdate(tbs195, true);
            }
            else
            {
                int coCon = int.Parse(hidCoConsul.Value);
                TBS174_AGEND_HORAR tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(coCon);
                tbs174.CO_SITUA_AGEND_HORAR = "R";
                TBS174_AGEND_HORAR.SaveOrUpdate(tbs174);
            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Atendimento Finalizado com sucesso!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
        }

        protected void txtDataAtend_OnTextChanged(object sender, EventArgs e)
        {
            CarregaGridEncaminhamentos();
            CarregaGridConsultas();
        }

        protected void chkExecuExameInsti_OnCheckedChanged(object sender, EventArgs e)
        {
            ControlaTabs("RQE", "");
            //Se o exame nãco for ocorrer na instituição, desmarca os itens selecionados nos campos de unidade e local
            if (!chkExecuExameInsti.Checked)
            {
                ddlUnidReqExam.Items.Clear();
                ddlLocalReqExam.Items.Clear();
                ddlUnidExecExamMOD.Items.Clear();

                ddlUnidReqExam.Enabled = ddlLocalReqExam.Enabled = ddlUnidExecExamMOD.Enabled = false;
            }
            else
            {
                AuxiliCarregamentos.CarregaUnidade(ddlUnidReqExam, LoginAuxili.ORG_CODIGO_ORGAO, false, true, true);
                AuxiliCarregamentos.CarregaUnidade(ddlUnidExecExamMOD, LoginAuxili.ORG_CODIGO_ORGAO, false, false, true);
                AuxiliCarregamentos.CarregaDepartamentos(ddlLocalReqExam, LoginAuxili.CO_EMP, false, true);

                ddlUnidReqExam.SelectedValue = LoginAuxili.CO_EMP.ToString();
                ddlUnidReqExam.Enabled = ddlLocalReqExam.Enabled = ddlUnidExecExamMOD.Enabled = true;
            }
        }

        protected void chkExecuServAmbuInst_OnCheckedChanged(object sender, EventArgs e)
        {
            ControlaTabs("RSA", "");
            //Se o Procedimento não for ocorrer na instituição, desmarca os itens selecionados nos campos de unidade e local
            if (!chkExecuServAmbuInst.Checked)
            {
                ddlUnidAtendServAmbu.Items.Clear();
                ddlLocalServAmbu.Items.Clear();
                ddlUnidExecServAMbuMOD.Items.Clear();

                ddlUnidAtendServAmbu.Enabled = ddlLocalServAmbu.Enabled = ddlUnidExecServAMbuMOD.Enabled = false;
            }
            else
            {
                AuxiliCarregamentos.CarregaUnidade(ddlUnidAtendServAmbu, LoginAuxili.ORG_CODIGO_ORGAO, false, true, true);
                AuxiliCarregamentos.CarregaUnidade(ddlUnidExecServAMbuMOD, LoginAuxili.ORG_CODIGO_ORGAO, false, false, true);
                AuxiliCarregamentos.CarregaDepartamentos(ddlLocalServAmbu, LoginAuxili.CO_EMP, false, true);

                ddlUnidAtendServAmbu.SelectedValue = LoginAuxili.CO_EMP.ToString();
                ddlUnidAtendServAmbu.Enabled = ddlLocalServAmbu.Enabled = ddlUnidExecServAMbuMOD.Enabled = true;
            }
        }

        protected void grdEncamMedic_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                //Se for registro antigo(um dos 10 antigos que são também apresentados), destaca ele em cor salmão
                if (((HiddenField)e.Row.Cells[0].FindControl("hidAntigos")).Value == "1")
                    e.Row.BackColor = System.Drawing.Color.AntiqueWhite;
            }
        }

        #region Modal Exames

        protected void ddlUnidExecExamMOD_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            qtdLinhasGridExamesMOD = 0;
            int idAtend = (!string.IsNullOrEmpty(hidIdAtendimentoMedico.Value) ? int.Parse(hidIdAtendimentoMedico.Value) : 0);
            int idOper = CarregaOperaAtendimento(idAtend);

            int idGrupo = ddlGrupExamMOD.SelectedValue != "" ? int.Parse(ddlGrupExamMOD.SelectedValue) : 0;
            int idSubGrupo = ddlSubGrupExamMOD.SelectedValue != "" ? int.Parse(ddlSubGrupExamMOD.SelectedValue) : 0;
            int coEmp = ddlUnidExecExamMOD.SelectedValue != "" ? int.Parse(ddlUnidExecExamMOD.SelectedValue) : 0;
            CarregaSubGruposExames(ddlSubGrupExamMOD, idOper, idGrupo);
            CarregaGridExamesMedicosDisponiveis(idGrupo, idSubGrupo, txtNomeExamMOD.Text, coEmp, idOper);

            AbreModalExames();
        }

        protected void ddlGrupExamMOD_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int idAtend = (!string.IsNullOrEmpty(hidIdAtendimentoMedico.Value) ? int.Parse(hidIdAtendimentoMedico.Value) : 0);
            int idOper = CarregaOperaAtendimento(idAtend);

            int idGrupo = ddlGrupExamMOD.SelectedValue != "" ? int.Parse(ddlGrupExamMOD.SelectedValue) : 0;
            int idSubGrupo = ddlSubGrupExamMOD.SelectedValue != "" ? int.Parse(ddlSubGrupExamMOD.SelectedValue) : 0;
            int coEmp = ddlUnidExecExamMOD.SelectedValue != "" ? int.Parse(ddlUnidExecExamMOD.SelectedValue) : 0;
            CarregaSubGruposExames(ddlSubGrupExamMOD, idOper, idGrupo);
            CarregaGridExamesMedicosDisponiveis(idGrupo, idSubGrupo, txtNomeExamMOD.Text, coEmp, idOper);

            ddlSubGrupExamMOD.Focus();
            AbreModalExames();
        }

        protected void ddlSubGrupExamMOD_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int idAtend = (!string.IsNullOrEmpty(hidIdAtendimentoMedico.Value) ? int.Parse(hidIdAtendimentoMedico.Value) : 0);
            int idOper = CarregaOperaAtendimento(idAtend);

            int idGrupo = ddlGrupExamMOD.SelectedValue != "" ? int.Parse(ddlGrupExamMOD.SelectedValue) : 0;
            int idSubGrupo = ddlSubGrupExamMOD.SelectedValue != "" ? int.Parse(ddlSubGrupExamMOD.SelectedValue) : 0;
            int coEmp = ddlUnidExecExamMOD.SelectedValue != "" ? int.Parse(ddlUnidExecExamMOD.SelectedValue) : 0;
            CarregaGridExamesMedicosDisponiveis(idGrupo, idSubGrupo, txtNomeExamMOD.Text, coEmp, idOper);

            txtNomeExamMOD.Focus();
            AbreModalExames();
        }

        protected void txtNomeExamMOD_OnTextChanged(object sender, EventArgs e)
        {
            int idAtend = (!string.IsNullOrEmpty(hidIdAtendimentoMedico.Value) ? int.Parse(hidIdAtendimentoMedico.Value) : 0);
            int idOper = CarregaOperaAtendimento(idAtend);

            int idGrupo = ddlGrupExamMOD.SelectedValue != "" ? int.Parse(ddlGrupExamMOD.SelectedValue) : 0;
            int idSubGrupo = ddlSubGrupExamMOD.SelectedValue != "" ? int.Parse(ddlSubGrupExamMOD.SelectedValue) : 0;
            int coEmp = ddlUnidExecExamMOD.SelectedValue != "" ? int.Parse(ddlUnidExecExamMOD.SelectedValue) : 0;
            CarregaGridExamesMedicosDisponiveis(idGrupo, idSubGrupo, txtNomeExamMOD.Text, coEmp, idOper);

            AbreModalExames();
        }

        #endregion

        #region Serviços Ambulatoriais

        protected void ddlUnidExecServAMbuMOD_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            qtdLinhasGridProcedMOD = 0;
            int idAtend = (!string.IsNullOrEmpty(hidIdAtendimentoMedico.Value) ? int.Parse(hidIdAtendimentoMedico.Value) : 0);
            int idOper = CarregaOperaAtendimento(idAtend);

            int idGrupo = ddlGrupProcMOD.SelectedValue != "" ? int.Parse(ddlGrupProcMOD.SelectedValue) : 0;
            int idSubGrupo = ddlSubGrupProcMOD.SelectedValue != "" ? int.Parse(ddlSubGrupProcMOD.SelectedValue) : 0;
            int coEmp = ddlUnidExecServAMbuMOD.SelectedValue != "" ? int.Parse(ddlUnidExecServAMbuMOD.SelectedValue) : 0;
            CarregaSubGruposExames(ddlSubGrupProcMOD, idOper, idGrupo);
            CarregaGridProcedimentosMedicosDisponiveis(idGrupo, idSubGrupo, txtNomeProcMOD.Text, coEmp, idOper);

            AbreModalProceMed();
        }

        protected void ddlGrupProcMOD_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int idAtend = (!string.IsNullOrEmpty(hidIdAtendimentoMedico.Value) ? int.Parse(hidIdAtendimentoMedico.Value) : 0);
            int idOper = CarregaOperaAtendimento(idAtend);

            int idGrupo = ddlGrupProcMOD.SelectedValue != "" ? int.Parse(ddlGrupProcMOD.SelectedValue) : 0;
            int idSubGrupo = ddlSubGrupProcMOD.SelectedValue != "" ? int.Parse(ddlSubGrupProcMOD.SelectedValue) : 0;
            int coEmp = ddlUnidExecServAMbuMOD.SelectedValue != "" ? int.Parse(ddlUnidExecServAMbuMOD.SelectedValue) : 0;
            CarregaSubGruposProcMedic(ddlSubGrupProcMOD, idOper, idGrupo);
            CarregaGridProcedimentosMedicosDisponiveis(idGrupo, idSubGrupo, txtNomeProcMOD.Text, coEmp, idOper);

            ddlSubGrupProcMOD.Focus();
            AbreModalProceMed();
        }

        protected void ddlSubGrupProcMOD_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int idAtend = (!string.IsNullOrEmpty(hidIdAtendimentoMedico.Value) ? int.Parse(hidIdAtendimentoMedico.Value) : 0);
            int idOper = CarregaOperaAtendimento(idAtend);

            int idGrupo = ddlGrupProcMOD.SelectedValue != "" ? int.Parse(ddlGrupProcMOD.SelectedValue) : 0;
            int idSubGrupo = ddlSubGrupProcMOD.SelectedValue != "" ? int.Parse(ddlSubGrupProcMOD.SelectedValue) : 0;
            int coEmp = ddlUnidExecServAMbuMOD.SelectedValue != "" ? int.Parse(ddlUnidExecServAMbuMOD.SelectedValue) : 0;
            CarregaGridProcedimentosMedicosDisponiveis(idGrupo, idSubGrupo, txtNomeProcMOD.Text, coEmp, idOper);

            txtNomeProcMOD.Focus();
            AbreModalProceMed();
        }

        protected void txtNomeProcMOD_OnTextChanged(object sender, EventArgs e)
        {
            int idAtend = (!string.IsNullOrEmpty(hidIdAtendimentoMedico.Value) ? int.Parse(hidIdAtendimentoMedico.Value) : 0);
            int idOper = CarregaOperaAtendimento(idAtend);

            int idGrupo = ddlGrupExamMOD.SelectedValue != "" ? int.Parse(ddlGrupExamMOD.SelectedValue) : 0;
            int idSubGrupo = ddlSubGrupExamMOD.SelectedValue != "" ? int.Parse(ddlSubGrupExamMOD.SelectedValue) : 0;
            int coEmp = ddlUnidExecServAMbuMOD.SelectedValue != "" ? int.Parse(ddlUnidExecServAMbuMOD.SelectedValue) : 0;
            CarregaGridProcedimentosMedicosDisponiveis(idGrupo, idSubGrupo, txtNomeProcMOD.Text, coEmp, idOper);

            AbreModalProceMed();
        }

        #endregion

        #endregion

        #region Torna Grids Clicáveis

        #region Modal Exames

        /// <summary>
        /// Evento necessário para que a grid de exames médicos disponíveis "clicável" funcione
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdListarExames_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                //Trata para que, quando o exame estiver inativo, a linha seja destacada em vermelho
                if ((e.Row.Cells[5].Text == "Inativo") || (e.Row.Cells[6].Text == "N&#227;o"))
                {
                    e.Row.BackColor = System.Drawing.Color.LightSalmon;
                    //e.Row.BackColor = System.Drawing.Color.Coral;
                }
                else
                {
                    e.Row.Attributes.Add("onMouseOver", "this.style.cursor='pointer';");
                    e.Row.Attributes.Add("onClick", "javascript:__doPostBack('" + grdListarExames.UniqueID + "','Select$" + qtdLinhasGridExamesMOD + "')");
                }
                qtdLinhasGridExamesMOD = qtdLinhasGridExamesMOD + 1;
            }
        }

        /// <summary>
        /// Evento chamado ao clicar em qualquer ponto de uma linha de exames
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdListarExames_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //--------> Define o código do procedimento médico clicado
            if (grdListarExames.DataKeys[grdListarExames.SelectedIndex].Value != null)
            {
                int idAtend = (!string.IsNullOrEmpty(hidIdAtendimentoMedico.Value) ? int.Parse(hidIdAtendimentoMedico.Value) : 0);
                int idOper = CarregaOperaAtendimento(idAtend);

                //Altera a Unidade para a qual o exame será requisitado caso a unidade usada na pesquisa da grid seja diferente de vazio
                if (!string.IsNullOrEmpty(ddlUnidExecExamMOD.SelectedValue))
                    ddlUnidReqExam.SelectedValue = ddlUnidExecExamMOD.SelectedValue;

                //Limpa os campos para que não haja conflitos no carregamento
                txtExameAtendMed.Text = hidCodExame.Value = FL_REQUE_APROV_EX.Value = "";

                //Chama o método responsável pelo carregamento
                int ID_PROC_MEDI_PROCE = Convert.ToInt32(grdListarExames.DataKeys[grdListarExames.SelectedIndex].Value);
                CarregaExameMedicoReq(idOper, ID_PROC_MEDI_PROCE);
            }
        }

        #endregion

        #region Modal Serviços Ambulatoriais

        /// <summary>
        /// Evento necessário para que a grid de exames médicos disponíveis "clicável" funcione
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdListarProcMedic_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                //Trata para que, quando o Procedimento estiver inativo, a linha seja destacada em vermelho
                if ((e.Row.Cells[5].Text == "Inativo") || (e.Row.Cells[6].Text == "N&#227;o"))
                {
                    e.Row.BackColor = System.Drawing.Color.LightSalmon;
                    //e.Row.BackColor = System.Drawing.Color.Coral;
                }
                else
                {
                    e.Row.Attributes.Add("onMouseOver", "this.style.cursor='pointer';");
                    e.Row.Attributes.Add("onClick", "javascript:__doPostBack('" + grdListarProcMedic.UniqueID + "','Select$" + qtdLinhasGridProcedMOD + "')");
                }
                qtdLinhasGridProcedMOD = qtdLinhasGridProcedMOD + 1;
            }
        }

        /// <summary>
        /// Evento chamado ao clicar em qualquer ponto de uma linha de exames
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdListarProcMedic_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //--------> Define o código do procedimento médico clicado
            if (grdListarProcMedic.DataKeys[grdListarProcMedic.SelectedIndex].Value != null)
            {
                int idAtend = (!string.IsNullOrEmpty(hidIdAtendimentoMedico.Value) ? int.Parse(hidIdAtendimentoMedico.Value) : 0);
                int idOper = CarregaOperaAtendimento(idAtend);

                //Altera a Unidade para a qual o exame será requisitado caso a unidade usada na pesquisa da grid seja diferente de vazio
                if (!string.IsNullOrEmpty(ddlUnidExecServAMbuMOD.SelectedValue))
                    ddlUnidAtendServAmbu.SelectedValue = ddlUnidExecServAMbuMOD.SelectedValue;

                //Chama o método responsável pelo carregamento
                int ID_PROC_MEDI_PROCE = Convert.ToInt32(grdListarProcMedic.DataKeys[grdListarProcMedic.SelectedIndex].Value);
                CarregaProcedMedicoReq(idOper, ID_PROC_MEDI_PROCE);
            }
        }

        #endregion

        #endregion
    }
}
//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: GESTÃO DE DADOS DE ALUNOS
// OBJETIVO: RELAÇÃO DE ALUNOS - ANIVERSARIANTES
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//25/04/2014 |   Maxwell Almeida         | Criação da página com a funcionalidade
//           |                           | de registro de material coletivo/Uniforme.
                    

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
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
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno;


namespace C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno
{
    public partial class RegistroMaterialColetivo : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //    --------> criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                carregaModalidade();
                carregaSerieCurso();
                carregaTurma();
				CarregaGrideSolicitacao();
                CarregaDiasVencimento();
                CarregaGridVazia();
                chkConsolValorTitul.Enabled = chkConsolValorTitul.Checked = true;

                ddlAluno.Items.Insert(0, new ListItem("Selecione", "0"));

                txtDtVectoSolic.Text = DateTime.Now.ToString();
            }

        }
		
		void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coAlu = Convert.ToInt32(ddlAluno.SelectedValue);
            var refAluno = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
            int proxTurma = Convert.ToInt32(this.ddlTurma.SelectedValue);
            int coEmp = TB129_CADTURMAS.RetornaPelaChavePrimaria(proxTurma).CO_EMP_UNID_CONT;
            int modalidade = Convert.ToInt32(this.ddlModalidade.SelectedValue);
            int proxSerie = Convert.ToInt32(this.ddlSerieCurso.SelectedValue);
            int codResp = TB07_ALUNO.RetornaPelaChavePrimaria(coAlu, coEmp).TB108_RESPONSAVEL.CO_RESP;
            //int qtdZero = 6 - coAlu.ToString().Length;
            string proxAno = DateTime.Now.Year.ToString();
            //string proxAno = PreAuxili.proximoAnoMat<string>(txtAno.Text);
            string turno = TB06_TURMAS.RetornaPeloCodigo(proxTurma).CO_PERI_TUR;
            TB47_CTA_RECEB tb47;
            TB01_CURSO hTb01 = TB01_CURSO.RetornaPeloCoCur(proxSerie);
            //TB08_MATRCUR tb08 = TB08_MATRCUR.RetornaPelaChavePrimaria(coAlu, proxSerie, proxAno, "1");
            DateTime dtPrev;
            if (txtDtPrevisao.Text != "")
            {
                dtPrev = DateTime.Parse(txtDtPrevisao.Text);

                if (dtPrev.Date < DateTime.Now.Date)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possível efetivar porque a data de previsão da entrega não pode ser uma data passada.");
                    return;
                }
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possível efetivar porque a data de previsão da entrega não foi informada.");
                return;
            }


            #region Atualiza o fardamento da matrícula
            ///Varre toda a gride de Solicitações e salva na tabela TB114_FARDMAT 
            foreach (GridViewRow linha in grdSolicitacoes.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked && txtDtPrevisao.Text != "")
                {
                    var tb66 = TB66_TIPO_SOLIC.RetornaPelaChavePrimaria(Convert.ToInt32(grdSolicitacoes.DataKeys[linha.RowIndex].Values[0]));
                    tb66.TB89_UNIDADESReference.Load();
                    decimal? valorUnitario = tb66.VL_UNIT_SOLI;
                    int? coUnid = tb66.TB89_UNIDADES != null ? (int?)tb66.TB89_UNIDADES.CO_UNID_ITEM : null;
                    int qtde = ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text != "" ? int.Parse(((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text) : 1;
                    decimal vlDescSol = ((TextBox)linha.Cells[0].FindControl("txtDescSolic")).Text != "" ? decimal.Parse(((TextBox)linha.Cells[0].FindControl("txtDescSolic")).Text) : 0;
                    int flDescPorc = ((CheckBox)linha.Cells[0].FindControl("chkDescPer")).Checked ? 1 : 0;


                    TB114_FARDMAT tb114 =
                        new TB114_FARDMAT
                        {
                            TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO),
                            CO_EMP = LoginAuxili.CO_EMP,
                            CO_ALU = coAlu,
                            CO_ANO_MATRIC = proxAno,
                            CO_MOD = modalidade,
                            CO_CUR = proxSerie,
                            CO_TUR = proxTurma,
                            TB66_TIPO_SOLIC = TB66_TIPO_SOLIC.RetornaPelaChavePrimaria(Convert.ToInt32(grdSolicitacoes.DataKeys[linha.RowIndex].Values[0])),
                            ID_UNID_TPSOLIC = coUnid,
                            VL_UNIT_TPSOLIC = valorUnitario,
                            QT_SOLIC = qtde,
                            DT_SOLIC_TPSOLIC = DateTime.Now,
                            DT_PREVI_TPSOLIC = dtPrev,
                            FL_SITUA_FINAN = null,
                            NM_DOCUM_TITUL = null,
                            DT_REGIS_TPSOLIC = DateTime.Now,
                            CO_COL = LoginAuxili.CO_COL,
                            VL_DESC_TPSOLIC = vlDescSol,
                            FL_DESC_PORCENT = flDescPorc
                        };

                    GestorEntities.SaveOrUpdate(tb114);


                }

                if ((((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked) && (((HiddenField)linha.Cells[0].FindControl("hidTxMatric")).Value == "S"))
                {
                    var varTb08 = TB08_MATRCUR.RetornaPelaChavePrimaria(coAlu, proxSerie, proxAno, "1");
                    varTb08.VL_TAXA_MATRIC = decimal.Parse(((Label)linha.Cells[2].FindControl("lblValor")).Text);
                    GestorEntities.SaveOrUpdate(varTb08);
                }

            }

            #endregion

            #region Atualiza o financeiro de itens da secretaria

            if ((ckbAtualizaFinancSolic.Checked) && (txtValorTotal.Text != "") && (txtDtVectoSolic.Text != ""))
            {
                foreach (GridViewRow linha in grdParcelasMaterial.Rows)
                {
                    int coHist = hTb01.ID_HISTO_MATERIAL != null ? hTb01.ID_HISTO_MATERIAL.Value : 0;
                    decimal valPar = decimal.Parse(linha.Cells[3].Text);
                    decimal valDes = decimal.Parse(linha.Cells[4].Text);
                    decimal valMul = decimal.Parse(linha.Cells[6].Text);
                    decimal valJur = decimal.Parse(linha.Cells[7].Text);

                    if (coHist != 0)
                    {
                        TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                        TB108_RESPONSAVEL tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(codResp);
                        TB44_MODULO tb44 = TB44_MODULO.RetornaPelaChavePrimaria(modalidade);
                        tb108.TB74_UFReference.Load();

                        if (tb108 != null)
                        {
                            tb47 = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(refAluno.TB25_EMPRESA1.CO_EMP, linha.Cells[0].Text, int.Parse(linha.Cells[1].Text));

                            if (tb47 == null)
                            {
                                tb47 = new TB47_CTA_RECEB();
                                //tb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();
                                //tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTEReference.Load();
                                //tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIAReference.Load();
                                //tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCOReference.Load();
                                tb47.CO_ALU = coAlu;
                                tb47.TB108_RESPONSAVEL = tb108;
                                tb47.CO_ANO_MES_MAT = PreAuxili.proximoAnoMat<string>(proxAno);
                                tb47.CO_CUR = proxSerie;
                                tb47.CO_TUR = proxTurma;
                                tb47.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(refAluno.TB25_EMPRESA1.CO_EMP);
                                tb47.CO_EMP_UNID_CONT = tb25.CO_EMP;
                                tb47.CO_FLAG_TP_VALOR_MUL = tb25.CO_FLAG_TP_VALOR_MUL != null ? tb25.CO_FLAG_TP_VALOR_MUL : "V";
                                tb47.CO_FLAG_TP_VALOR_JUR = tb25.CO_FLAG_TP_VALOR_JUROS != null ? tb25.CO_FLAG_TP_VALOR_JUROS : "V";
                                tb47.CO_FLAG_TP_VALOR_DES = "V";
                                tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO = "V";
                                tb47.CO_FLAG_TP_VALOR_OUT = "V";
                                tb47.FL_EMITE_BOLETO = tb25.CO_FLAG_GERA_BOLETO_SERV_SECR == "S" ? "S" : "N";
                                tb47.CO_MODU_CUR = modalidade;


                                tb47.CO_NOS_NUM = (tb25.CO_PROX_NOS_NUM != null ? tb25.CO_PROX_NOS_NUM : "");

                                tb47.TB39_HISTORICO = TB39_HISTORICO.RetornaPelaChavePrimaria(coHist);
                                tb47.DE_COM_HIST = "Solicitação de material coletivo / uniforme realizada no dia " + DateTime.Now.ToString("dd/MM/yyyy");
                                tb47.DT_ALT_REGISTRO = DateTime.Now;
                                tb47.DT_CAD_DOC = DateTime.Now;
                                tb47.DT_EMISS_DOCTO = DateTime.Now;
                                tb47.DT_SITU_DOC = DateTime.Now;
                                tb47.DT_VEN_DOC = DateTime.Parse(linha.Cells[2].Text);
                                tb47.IC_SIT_DOC = "A";
                                tb47.NU_DOC = linha.Cells[0].Text;
                                tb47.NU_PAR = int.Parse(linha.Cells[1].Text);
                                tb47.QT_PAR = grdParcelasMaterial.Rows.Count;
                                tb47.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                                tb47.TB086_TIPO_DOC = (from t in TB086_TIPO_DOC.RetornaTodosRegistros() where t.SIG_TIPO_DOC.ToUpper() == "BOL" select t).FirstOrDefault();
                                tb47.TB099_CENTRO_CUSTO = (tb25.CO_CENT_CUSSOL.HasValue ? (TB099_CENTRO_CUSTO.RetornaPelaChavePrimaria(tb25.CO_CENT_CUSSOL.Value)) : null);
                                tb47.TB227_DADOS_BOLETO_BANCARIO = (tb25.CO_FLAG_GERA_BOLETO_SERV_SECR == "S" ? (ddlBoletoSolic.SelectedValue != "" ? TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(int.Parse(ddlBoletoSolic.SelectedValue)) : null) : null);
                                tb47.TB39_HISTORICO = TB39_HISTORICO.RetornaPelaChavePrimaria(coHist);
                                tb47.CO_AGRUP_RECDESP = TB83_PARAMETRO.RetornaPelaChavePrimaria(refAluno.TB25_EMPRESA1.CO_EMP).CO_AGRUP_REC;
                                tb47.TB56_PLANOCTA = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb44.CO_SEQU_PC.Value);
                                tb47.CO_SEQU_PC_BANCO = tb44.CO_SEQU_PC_BANCO.Value;
                                tb47.CO_SEQU_PC_CAIXA = tb44.CO_SEQU_PC_CAIXA.Value;
                                tb47.TP_CLIENTE_DOC = "A";
                                tb47.VR_JUR_DOC = valJur;
                                tb47.VR_MUL_DOC = valMul;
                                tb47.VR_PAR_DOC = valPar;
                                tb47.VR_DES_DOC = valDes;
                                tb47.VR_TOT_DOC = (txtValorTotal.Text != "" ? decimal.Parse(txtValorTotal.Text) : 0);

                                GestorEntities.SaveOrUpdate(tb47);

                            }
                            else
                            {
                                AuxiliPagina.EnvioMensagemErro(this, "Não é possível efetivar e atualizar o financeiro porque já existe no sistema um título com este Número de Documento.");
                                return;
                            }
                        }
                    }
                    else
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Não é possível efetivar e atualizar o financeiro porque não existe histórico de material coletivo / uniforme cadastrado para o curso.");
                        return;
                    }
                }


            }

            #endregion

            #region Habilita/Desabilia botões

            lnkBoletoMater.Enabled = true;
            ckbAtualizaFinancSolic.Enabled = false;
            chkConsolValorTitul.Enabled = false;
            txtValorTotal.Enabled = false;
            txtQtdParcelas.Enabled = false;
            txtDtVectoSolic.Enabled = false;
            ddlDiaVectoParcMater.Enabled = false;
            ddlTipoDesctoParc.Enabled = false;
            txtQtdeMesesDesctoParc.Enabled = false;
            txtDesctoMensaParc.Enabled = false;
            txtMesIniDescontoParc.Enabled = false;
            ddlBoletoSolic.Enabled = false;
            txtDtPrevisao.Enabled = false;
            lnkMontaGridParcMater.Enabled = false;

            // Desabilita os campos da grid de itens da solicitação
            foreach (GridViewRow linha in grdSolicitacoes.Rows)
            {
                ((CheckBox)linha.Cells[0].FindControl("chkSelect")).Enabled = false;
                ((TextBox)linha.Cells[3].FindControl("txtDescSolic")).Enabled = false;
                ((CheckBox)linha.Cells[3].FindControl("chkDescPer")).Enabled = false;
                ((TextBox)linha.Cells[4].FindControl("txtQtdeSolic")).Enabled = false;
            }
            #endregion

            AuxiliPagina.EnvioMensagemSucesso(this, "Material(s) e Uniforme(s) efetivado com sucesso.");
            //ControlaTabs("UMA");
        }
		

        #endregion  

        #region Carregamentos

        private void CarregaGridVazia()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NU_DOC";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NU_PAR";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "dtVencimento";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "valorParcela";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "valorDescto";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "valorLiquido";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "valorMulta";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "valorJuros";
            dtV.Columns.Add(dcATM);

            //int i = 1;
            DataRow linha;
            //while (i <= 2)
            //{
                linha = dtV.NewRow();
                linha["NU_DOC"] = "";
                linha["NU_PAR"] = "";
                linha["dtVencimento"] = "";
                linha["valorParcela"] = "";
                linha["valorDescto"] = "";
                linha["valorLiquido"] = "";
                linha["valorMulta"] = "";
                linha["valorJuros"] = "";
                dtV.Rows.Add(linha);

            //}

            //Session["TabelaMedicAtendMed"] = dtV;

            grdParcelasMaterial.DataSource = dtV;
            grdParcelasMaterial.DataBind();
        }

        /// <summary>
        /// Carrega os dias de vencimento para as mensalidades da matrícula (Será alterado para buscar em uma tabela no banco)
        /// </summary>
        private void CarregaDiasVencimento()
        {
            ddlDiaVectoParcMater.Items.AddRange(AuxiliBaseApoio.DiaVencimentoTitulo());
            int modalidade = (ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int serie = (ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0);
            var curso = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
            int diaVencimento = ((curso != null && curso.NU_MDV != null) ? (curso.NU_MDV ?? 5) : 5);

            if (ddlDiaVectoParcMater.Items.FindByValue(diaVencimento.ToString()) != null)
                ddlDiaVectoParcMater.SelectedValue = diaVencimento.ToString();
        }

		private void CarregaGrideSolicitacao()
        {
            var lstTb66 = from tb66 in TB66_TIPO_SOLIC.RetornaTodosRegistros()
                          where tb66.FL_ITEM_MATRIC_TPSOLIC == "S"
                          select new{
                              Codigo = tb66.CO_TIPO_SOLI, 
                              Descricao =  tb66.NO_TIPO_SOLI,
                              Valor = tb66.VL_UNIT_SOLI != null ? tb66.VL_UNIT_SOLI.Value : 0,
                              DescUnidade = tb66.TB89_UNIDADES != null ? tb66.TB89_UNIDADES.SG_UNIDADE : "",
                              Qtde = "",
                              Desconto = "",
                              Total = "",
                              Inclu = true,
                              Checked = false,
                              txMatric = tb66.FL_TAXA_MATRIC
                          };

            grdSolicitacoes.DataKeyNames = new string[] { "Codigo" };
            grdSolicitacoes.DataSource = lstTb66;
            grdSolicitacoes.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Boletos
        /// </summary>
        private void CarregaBoletos()
        {
            int coEmp = TB129_CADTURMAS.RetornaPelaChavePrimaria(int.Parse(ddlTurma.SelectedValue)).CO_EMP_UNID_CONT;
            int coBoleto = (TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, int.Parse(ddlModalidade.SelectedValue), int.Parse(ddlSerieCurso.SelectedValue)).ID_BOLETO_MATR ?? 0);
            
            // Retorna os boletos cadastrador para Material e Fardamento
            AuxiliCarregamentos.CarregaBoletos(ddlBoletoSolic, coEmp, "U", 0, 0, false, false);
            
            ddlBoletoSolic.Items.Insert(0, new ListItem("Nenhum", ""));

            if (coBoleto > 0 && ddlBoletoSolic.Items.FindByValue(coBoleto.ToString()) != null)
                ddlBoletoSolic.SelectedValue = coBoleto.ToString();

            var tb83 = (from iTb83 in TB83_PARAMETRO.RetornaTodosRegistros()
                        where iTb83.CO_EMP == coEmp
                        select new { iTb83.ID_BOLETO_MATRIC, iTb83.ID_BOLETO_MENSA, iTb83.ID_BOLETO_MATER_UNIFO }).FirstOrDefault();

            if (tb83 != null)
            {
                //if (tb83.ID_BOLETO_MENSA != null)
                //{
                //    ddlBoleto.SelectedValue = tb83.ID_BOLETO_MENSA.ToString();
                //}

                if (tb83.ID_BOLETO_MATER_UNIFO != null)
                {
                    ddlBoletoSolic.SelectedValue = tb83.ID_BOLETO_MATER_UNIFO.ToString();
                }
            }

        }

        /// <summary>
        /// Carrega as Modalidades
        /// </summary>
        private void carregaModalidade()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }

        /// <summary>
        /// Carrega a Série e Curso de acordo com a modalidade informada.
        /// </summary>
        private void carregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, LoginAuxili.CO_EMP, false);
        }

        /// <summary>
        /// Carrega as Turmas de acordo com série e curso.
        /// </summary>
        private void carregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaTurmas(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, false);
        }

        protected void MontaGridParcelasMaterial()
        {
            int i = 1; // Contador das parcelas
            int coEmp = TB129_CADTURMAS.RetornaPelaChavePrimaria(int.Parse(ddlTurma.SelectedValue)).CO_EMP_UNID_CONT;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            var varSer = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
            var tb83 = TB83_PARAMETRO.RetornaPelaChavePrimaria(coEmp);
            string turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;
            //int numNire = txtNireAluno.Text.Replace(".", "").Replace("-", "") != "" ? int.Parse(txtNireAluno.Text.Replace(".", "").Replace("-", "")) : 0;
            int colu = int.Parse(ddlAluno.SelectedValue);
            int numNire = TB07_ALUNO.RetornaPeloCoAlu(colu).NU_NIRE;
            decimal valPar = 0;
            decimal valLiq = 0;
            decimal valDes = 0;
            decimal valMul = tb83.VL_PERCE_MULTA != null ? (decimal)tb83.VL_PERCE_MULTA : 0;
            decimal valJur = tb83.VL_PERCE_JUROS != null ? (decimal)tb83.VL_PERCE_JUROS : 0;
            decimal ValTotal = txtValorTotal.Text != "" ? decimal.Parse(txtValorTotal.Text) : 0;
            int QtdPar = txtQtdParcelas.Text != "" ? int.Parse(txtQtdParcelas.Text) : 1;
            string nuPar = "";
            DateTime dataSelec = txtDtVectoSolic.Text != "" ? DateTime.Parse(txtDtVectoSolic.Text) : DateTime.Now;
            int mesVenc = 1;
            string tpDesconto = ddlTipoDesctoParc.SelectedValue;
            int QtdMesDesconto = txtQtdeMesesDesctoParc.Text != "" ? int.Parse(txtQtdeMesesDesctoParc.Text) : 0;
            decimal vlDesconto = txtDesctoMensaParc.Text != "" ? decimal.Parse(txtDesctoMensaParc.Text) : 0;
            int PID = txtMesIniDescontoParc.Text != "" ? int.Parse(txtMesIniDescontoParc.Text) : 0;
            decimal vlValidTotal = 0;
            int dia = int.Parse(ddlDiaVectoParcMater.SelectedValue);
            int mes = 0;
            int ano = 0;
            string tpDesc = ddlTipoDesctoParc.SelectedValue;


            grdParcelasMaterial.DataKeyNames = new string[] { "CO_EMP", "NU_DOC", "NU_PAR", "DT_CAD_DOC" };

            DataTable Dt = new DataTable();
            Dt.Columns.Add("CO_EMP");
            Dt.Columns.Add("NU_DOC");
            Dt.Columns.Add("NU_PAR");
            Dt.Columns.Add("DT_CAD_DOC");
            Dt.Columns.Add("dtVencimento");
            Dt.Columns.Add("valorParcela");
            Dt.Columns.Add("valorDescto");
            Dt.Columns.Add("valorLiquido");
            Dt.Columns.Add("valorMulta");
            Dt.Columns.Add("valorJuros");

            if (chkConsolValorTitul.Checked)
            {
                #region Títulos consolidados

                valPar = Math.Round((ValTotal / QtdPar), 2);

                if (valPar <= 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar parcelas com valores menores ou iguais a 0.");
                    return;
                }

                int qtMes = QtdMesDesconto;
                while (i <= QtdPar)
                {
                    mes = dataSelec.Month;
                    ano = dataSelec.Year;
                    nuPar = i.ToString().Length == 1 ? "0" + i.ToString() : i.ToString();
                    valDes = 0;
                    valLiq = valPar;
                    vlValidTotal = valPar * QtdPar;

                    // Valida se o valor total das parcelas está de acordo com o valor total dos itens selecionados
                    // se os valores forem diferentes o sistema calcula a diferencia e adiciona na ultima parcela
                    if (vlValidTotal != ValTotal && i == QtdPar)
                    {
                        valPar = ValTotal > vlValidTotal ? valPar + (ValTotal - vlValidTotal) : valPar + (vlValidTotal - ValTotal);
                    }

                    // Determina a data de vencimento das parcelas, a partir da segunda parcela, de acordo com as informações de data da primeira parcela, melhor dia de
                    // vencimento e o ano, ajustado se necessário
                    if (i != 1)
                    {
                        mes = mes + 1;
                        //if (mesVenc == 11)
                        //{
                        //    mesVenc = 1;
                        //    mes = 1;
                        //    ano = ano + 1;
                        //}
                        if (mes == 13)
                        {
                            mes = 1;
                            ano = ano + 1;
                        }
                        dataSelec = DateTime.Parse(dia.ToString("D2") + "/" + mes.ToString("D2") + "/" + ano);
                    }

                    if (tpDesc == "M")
                    {
                        // Determina em qual parcela irá iniciar o desconto, de acordo com o PID informado
                        if (i >= PID)
                        {
                            if (qtMes > 0)
                            {
                                valDes = vlDesconto;
                                valLiq = valPar - valDes;
                                qtMes--;
                            }
                        }
                    }
                    else
                    {
                        valDes = Math.Round((vlDesconto / QtdPar), 2);
                        valLiq = valPar - valDes;
                    }

                    if (valDes >= valPar)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar parcelas com descontos maiores ou iguais ao valor das parcelas.");
                        return;
                    }

                    // Monta as linhas da GRID
                    Dt.Rows.Add(
                        coEmp,
                        "MU" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + nuPar,
                        nuPar,
                        DateTime.Now.ToString("dd/MM/yyyy"),
                        dataSelec.ToString("dd/MM/yyyy"),
                        valPar.ToString("N2"),
                        valDes.ToString("N2"),
                        valLiq.ToString("N2"),
                        valMul.ToString("N2"),
                        valJur.ToString("N3")
                    );
                    mesVenc++;
                    i++;
                }
                #endregion
            }
            else
            {
                #region Títulos não consolidados

                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    // Faz a verificação dos itens marcados na Grid de Itens Emprestados
                    if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                    {
                        valPar = decimal.Parse(((Label)linha.Cells[2].FindControl("lblTotal")).Text);
                        if (valPar <= 0)
                        {
                            AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar parcelas com valores menores ou iguais a 0.");
                            return;
                        }
                        mes = dataSelec.Month;
                        ano = dataSelec.Year;
                        nuPar = i.ToString().Length == 1 ? "0" + i.ToString() : i.ToString();
                        valDes = 0;
                        valLiq = valPar;

                        // Determina a data de vencimento das parcelas, a partir da segunda parcela, de acordo com as informações de data da primeira parcela, melhor dia de
                        // vencimento e o ano, ajustado se necessário
                        if (i != 1)
                        {
                            mes = mes + 1;
                            if (mesVenc == 11)
                            {
                                mesVenc = 1;
                                mes = 1;
                                ano = ano + 1;
                            }
                            dataSelec = DateTime.Parse(dia.ToString("D2") + "/" + mes.ToString("D2") + "/" + ano);
                        }

                        // Monta as linhas da GRID
                        Dt.Rows.Add(
                            coEmp,
                            "MU" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + nuPar,
                            nuPar,
                            DateTime.Now.ToString("dd/MM/yyyy"),
                            dataSelec.ToString("dd/MM/yyyy"),
                            valPar.ToString("N2"),
                            valDes.ToString("N2"),
                            valLiq.ToString("N2"),
                            valMul.ToString("N2"),
                            valJur.ToString("N3")
                        );

                        mesVenc++;
                        i++;
                    }
                }
                #endregion
            }

            grdParcelasMaterial.DataSource = Dt;
            grdParcelasMaterial.DataBind();
        }

        /// <summary>
        /// Carrega os Alunos de acordo com as informações de Modalidade, Série e Turma para que o aluno que se deseja associar esse registro de material coletivo seja selecionado.
        /// </summary>
        private void CarregaAluno()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serieCurso = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string anoGrade = DateTime.Now.Year.ToString();

            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb07.CO_ALU equals tb08.CO_ALU
                       where (
                       (modalidade != 0 ? tb08.TB44_MODULO.CO_MODU_CUR == modalidade : 0 == 0)
                       && (serieCurso != 0 ? tb08.CO_CUR == serieCurso : 0 == 0)
                       && (turma != 0 ? tb08.CO_TUR == turma : 0 == 0)
                       && (anoGrade != "0" ? tb08.CO_ANO_MES_MAT == anoGrade : 0 == 0)
                       && (tb08.CO_SIT_MAT == "A")
                       )
                       //where (( tb07.CO_EMP == LoginAuxili.CO_EMP) && (  ))
                       select new { tb08.CO_ALU, tb08.TB07_ALUNO.NO_ALU }).OrderBy(m => m.NO_ALU);

            ddlAluno.DataTextField = "NO_ALU";
            ddlAluno.DataValueField = "CO_ALU";

            ddlAluno.DataSource = res;
            ddlAluno.DataBind();

            ddlAluno.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        /// <summary>
        /// Método para controlar Checks da tela de cadastro de matrícula
        /// </summary>
        /// <param name="chk"></param>
        //protected void ControlaChecks(CheckBox chk)
        //{
        //    chkCuiEspAlu.Checked = chkMatEsc.Checked = chkMenEscAlu.Checked = chkRegAtiExt.Checked =
        //    chkResAliAlu.Checked = chkEndAddAlu.Checked = chkDocMat.Checked = chkTelAddAlu.Checked = false;

        //    chk.Checked = true;
        //}

        #endregion

        #region Funções de Campo


        protected void ddlSerieCurso_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaTurma();
            CarregaAluno();
            CarregaDiasVencimento();

        }

        protected void ddlModalidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaSerieCurso();
            CarregaAluno();
            CarregaDiasVencimento();

        }

        protected void ddlTurma_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
            CarregaDiasVencimento();
            CarregaBoletos();
        }

        protected void ddlTipoDesctoParc_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string tpDesc = ddlTipoDesctoParc.SelectedValue;

            switch (tpDesc)
            {
                case "T":
                    txtQtdeMesesDesctoParc.Enabled = false;
                    txtQtdeMesesDesctoParc.Text = "";

                    txtMesIniDescontoParc.Enabled = false;
                    txtMesIniDescontoParc.Text = "";
                    break;
                case "M":
                    txtQtdeMesesDesctoParc.Enabled = true;
                    txtQtdeMesesDesctoParc.Text = "";

                    txtMesIniDescontoParc.Enabled = true;
                    txtMesIniDescontoParc.Text = "";
                    break;
            }
        }

        protected void chkConsolValorTitul_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConsolValorTitul.Checked)
            {
                txtQtdParcelas.Enabled = true;
                ddlTipoDesctoParc.Enabled = true;
                if (ddlTipoDesctoParc.SelectedValue == "M")
                {
                    txtQtdeMesesDesctoParc.Enabled = true;
                    txtMesIniDescontoParc.Enabled = true;
                }
                else
                {
                    txtQtdeMesesDesctoParc.Enabled = false;
                    txtMesIniDescontoParc.Enabled = false;
                }
                txtDesctoMensaParc.Enabled = true;
                txtQtdParcelas.Text = "";

                txtQtdParcelas.Text = "1";
            }
            else
            {
                txtQtdParcelas.Enabled = false;
                ddlTipoDesctoParc.Enabled = false;
                txtQtdeMesesDesctoParc.Enabled = false;
                txtMesIniDescontoParc.Enabled = false;
                txtDesctoMensaParc.Enabled = false;
                txtQtdParcelas.Text = "";
            }

            //if (chkConsolValorTitul.Checked)
            //{
            //    ddlHistorico.Enabled = ddlAgrupador.Enabled = 
            //    ddlGrupoContaA.Enabled = ddlSubGrupoContaA.Enabled = ddlContaContabilA.Enabled = ddlTipoContaA.Enabled =
            //    ddlGrupoContaB.Enabled = ddlSubGrupoContaB.Enabled = ddlContaContabilB.Enabled = ddlTipoContaB.Enabled =
            //    ddlGrupoContaC.Enabled = ddlSubGrupoContaC.Enabled = ddlContaContabilC.Enabled = ddlTipoContaC.Enabled = true;
            //}
            //else
            //{
            //    ddlHistorico.Enabled = ddlAgrupador.Enabled =
            //    ddlGrupoContaA.Enabled = ddlSubGrupoContaA.Enabled = ddlContaContabilA.Enabled = ddlTipoContaA.Enabled =
            //    ddlGrupoContaB.Enabled = ddlSubGrupoContaB.Enabled = ddlContaContabilB.Enabled = ddlTipoContaB.Enabled =
            //    ddlGrupoContaC.Enabled = ddlSubGrupoContaC.Enabled = ddlContaContabilC.Enabled = ddlTipoContaC.Enabled = false;
            //    ddlHistorico.SelectedValue = ddlAgrupador.SelectedValue =
            //    ddlGrupoContaA.SelectedValue = ddlSubGrupoContaA.SelectedValue = ddlContaContabilA.SelectedValue =
            //    ddlGrupoContaB.SelectedValue = ddlSubGrupoContaB.SelectedValue = ddlContaContabilB.SelectedValue =
            //    ddlGrupoContaC.SelectedValue = ddlSubGrupoContaC.SelectedValue = ddlContaContabilC.SelectedValue = "";
            //}
        }

        protected void ckbAtualizaFinancSolic_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbAtualizaFinancSolic.Checked)
            {
                #region chkConsolValorTitul
                int i = 0;
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                    {
                        i++;
                    }
                }

                if (i > 1)
                {
                    chkConsolValorTitul.Checked = false;
                    chkConsolValorTitul.Enabled = true;
                }
                else
                {
                    chkConsolValorTitul.Checked = false;
                    chkConsolValorTitul.Enabled = false;
                }
                #endregion

                if (chkConsolValorTitul.Checked)
                {
                    txtQtdParcelas.Text = "";
                    txtQtdParcelas.Enabled = true;

                    ddlTipoDesctoParc.SelectedValue = "T";
                    ddlTipoDesctoParc.Enabled = true;

                    txtDesctoMensaParc.Text = "";
                    txtDesctoMensaParc.Enabled = true;

                    //lnkMatUniAlu.Enabled = true;

                    lnkBoletoMater.Enabled = true;
                }

                ddlDiaVectoParcMater.Enabled = true;

                //lnkGrava.Enabled = true;

                lnkMontaGridParcMater.Enabled = true;

                ddlBoletoSolic.SelectedValue = "";
                ddlBoletoSolic.Enabled = true;
            }
            else
            {
                chkConsolValorTitul.Checked = false;
                chkConsolValorTitul.Enabled = false;

                txtQtdParcelas.Text = "";
                txtQtdParcelas.Enabled = false;

                txtDtVectoSolic.Enabled = false;

                ddlDiaVectoParcMater.Enabled = false;

                ddlTipoDesctoParc.SelectedValue = "T";
                ddlTipoDesctoParc.Enabled = false;

                txtQtdeMesesDesctoParc.Text = "";
                txtQtdeMesesDesctoParc.Enabled = false;

                txtDesctoMensaParc.Text = "";
                txtDesctoMensaParc.Enabled = false;

                txtMesIniDescontoParc.Text = "";
                txtMesIniDescontoParc.Enabled = false;

                ddlBoletoSolic.SelectedValue = "";
                ddlBoletoSolic.Enabled = false;

                lnkMontaGridParcMater.Enabled = false;

                lnkBoletoMater.Enabled = false;
            }
        }
		
		protected void lnkBoletoMater_Click(object sender, EventArgs e)
        {
            //--------> Instancia um novo conjunto de dados de boleto na sessão
            Session[SessoesHttp.BoletoBancarioCorrente] = new List<InformacoesBoletoBancario>();

            //--------> Dados do Aluno e Unidade
            int coAlu = int.Parse(ddlAluno.SelectedValue);
            int coemp = TB129_CADTURMAS.RetornaPelaChavePrimaria(int.Parse(ddlTurma.SelectedValue)).CO_EMP_UNID_CONT;
            int coModu = Convert.ToInt32(this.ddlModalidade.SelectedValue);
            int coCur = Convert.ToInt32(this.ddlSerieCurso.SelectedValue);
            int coTur = Convert.ToInt32(this.ddlTurma.SelectedValue);


            //--------> Recupera dados do Responsável do Aluno
            var s = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                     join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb108.CO_RESP equals tb07.TB108_RESPONSAVEL.CO_RESP
                     where tb07.CO_ALU == coAlu
                     join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb108.CO_BAIRRO equals tb905.CO_BAIRRO
                     join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb108.CO_CIDADE equals tb904.CO_CIDADE
                     select new
                     {
                         NOME = tb108.NO_RESP,
                         BAIRRO = tb905.NO_BAIRRO,
                         CEP = tb108.CO_CEP_RESP,
                         CIDADE = tb904.NO_CIDADE,
                         ENDERECO = tb108.DE_ENDE_RESP,
                         NUMERO = tb108.NU_ENDE_RESP,
                         COMPL = tb108.DE_COMP_RESP,
                         UF = tb904.CO_UF,
                         CPFCNPJ = tb108.NU_CPF_RESP.Length >= 11 ? tb108.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb108.NU_CPF_RESP
                     }).FirstOrDefault();

            int iGrdNeg = 1;

            foreach (GridViewRow linha in grdParcelasMaterial.Rows)
            {
                int coEmp = Convert.ToInt32(grdParcelasMaterial.DataKeys[linha.RowIndex].Values[0]);
                string nuDoc = grdParcelasMaterial.DataKeys[linha.RowIndex].Values[1].ToString();
                int nuPar = Convert.ToInt32(grdParcelasMaterial.DataKeys[linha.RowIndex].Values[2]);
                string strInstruBoleto = "";

                TB47_CTA_RECEB tb47 = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(LoginAuxili.CO_EMP, nuDoc, nuPar);

                tb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();
                if (tb47.TB227_DADOS_BOLETO_BANCARIO == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto, Título sem Boleto Associado!");
                    return;
                }
                tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTEReference.Load();
                tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIAReference.Load();
                tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCOReference.Load();
                tb47.TB108_RESPONSAVELReference.Load();

                //------------> Se o título for gerado para um aluno:
                if (tb47.TB108_RESPONSAVEL == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto, Título sem Responsável!");
                    return;
                }

                if (tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto. Nosso número não cadastrado para banco informado.");
                    return;
                }

                //------------> Obtém a unidade
                TB25_EMPRESA unidade = TB25_EMPRESA.RetornaPelaChavePrimaria(coemp);

                InformacoesBoletoBancario boleto = new InformacoesBoletoBancario();

                //------------> Informações do Boleto
                boleto.Carteira = tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CARTEIRA.Trim();
                boleto.CodigoBanco = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO;

                /*
                 * Esta parte do código valida se o título já possui um nosso número, se já tiver, ele usa o NossoNúmero do título, registrado na tabela TB47, caso contrário,
                 * ele pega o próximo NossoNúmero registrado no banco, tabela TB29.
                 * */
                if (tb47.CO_NOS_NUM != null && tb47.CO_NOS_NUM.Trim() != "")
                {
                    boleto.NossoNumero = tb47.CO_NOS_NUM.Trim();
                }
                else
                {
                    boleto.NossoNumero = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM.Trim();
                    tb47.CO_NOS_NUM = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM.Trim();
                    TB47_CTA_RECEB.SaveOrUpdate(tb47, true);
                }

                boleto.NumeroDocumento = tb47.NU_DOC + "-" + tb47.NU_PAR;
                boleto.Valor = tb47.VR_PAR_DOC; //valor da parcela do documento
                if (tb47.VR_DES_DOC != null)
                {
                    boleto.Desconto = tb47.VR_DES_DOC.Value; // Valor do desconto do documento
                }
                boleto.Vencimento = tb47.DT_VEN_DOC;

                //------------> Informações do Cedente
                boleto.NumeroConvenio = tb47.TB227_DADOS_BOLETO_BANCARIO.NU_CONVENIO;
                boleto.Agencia = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.CO_AGENCIA + "-" +
                                    tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.DI_AGENCIA; //titulo AGENCIA E DIGITO

                boleto.CodigoCedente = tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CEDENTE.Trim();
                boleto.Conta = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_CONTA.Trim();
                boleto.CpfCnpjCedente = unidade.CO_CPFCGC_EMP;
                boleto.NomeCedente = unidade.NO_RAZSOC_EMP;

                /**
                 * Esta validação verifica o tipo de boleto para incluir o valor de desconto nas intruções se o tipo for "M" - Modelo 4.
                 * */
                #region Valida layout do boleto gerado
                TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                tb25.TB000_INSTITUICAOReference.Load();
                tb25.TB000_INSTITUICAO.TB149_PARAM_INSTIReference.Load();
                //TB000_INSTITUICAO tb000 = TB000_INSTITUICAO.RetornaPelaChavePrimaria(tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO);

                if (tb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.TP_CTRLE_SECRE_ESCOL == "I")
                {
                    switch (tb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.TP_BOLETO_BANC)
                    {
                        case "M":
                            strInstruBoleto = "<b>Desconto total: </b>" + string.Format("{0:C}", boleto.Desconto) + "<br>";
                            break;
                    }
                }
                else
                {
                    switch (tb25.TP_BOLETO_BANC)
                    {
                        case "M":
                            strInstruBoleto = "<b>Desconto total: </b>" + string.Format("{0:C}", boleto.Desconto) + "<br>";
                            break;
                    }
                }
                #endregion

                if (tb47.TB227_DADOS_BOLETO_BANCARIO.FL_DESC_DIA_UTIL == "S" && tb47.TB227_DADOS_BOLETO_BANCARIO.VL_DESC_DIA_UTIL.HasValue)
                {
                    var desc = boleto.Valor - tb47.TB227_DADOS_BOLETO_BANCARIO.VL_DESC_DIA_UTIL.Value;
                    strInstruBoleto = "Receber até o 5º dia útil (" + AuxiliCalculos.RetornarDiaUtilMes(boleto.Vencimento.Year, boleto.Vencimento.Month, 5).ToShortDateString() + ") o valor: " + string.Format("{0:C}", desc) + "<br>";
                }

                //------------> Prenche as instruções do boleto de acordo com o cadastro do boleto informado
                if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO != null)
                    strInstruBoleto += tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO + "</br>";

                if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO != null)
                    strInstruBoleto += tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO + "</br>";

                if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO != null)
                    strInstruBoleto += tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO + "</br>";

                boleto.Instrucoes = strInstruBoleto;

                //------------> Chave do Título do Contas a Receber
                boleto.CO_EMP = tb47.CO_EMP;
                boleto.NU_DOC = tb47.NU_DOC;
                boleto.NU_PAR = tb47.NU_PAR;
                boleto.DT_CAD_DOC = tb47.DT_CAD_DOC;

                //------------> Faz a adição de instruções ao Boleto
                boleto.Instrucoes += "<br>";
                //boleto.Instrucoes += "(*) " + multaMoraDesc + "<br>";


                //------------> Coloca na Instrução as Informações do Responsável do Aluno ou Informações do Cliente
                string CnpjCPF = "";

                //------------> Ano Refer: - Matrícula: - Nº NIRE:
                //------------> Modalidade: - Série: - Turma: - Turno:
                var inforAluno = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                  where tb07.CO_ALU == tb47.CO_ALU
                                  select new
                                  {
                                      tb07.NU_NIRE,
                                      tb07.NO_ALU
                                  }).FirstOrDefault();

                string deModu = "";
                if (tb47.CO_MODU_CUR != null)
                {
                    deModu = TB44_MODULO.RetornaPelaChavePrimaria(tb47.CO_MODU_CUR.Value).DE_MODU_CUR;
                }
                else
                {
                    deModu = TB44_MODULO.RetornaPelaChavePrimaria(coModu).DE_MODU_CUR;
                }

                string noCur = "";
                if (tb47.CO_CUR != null)
                {
                    noCur = TB01_CURSO.RetornaPelaChavePrimaria(coemp, coModu, tb47.CO_CUR.Value).NO_CUR;
                }
                else
                {
                    noCur = TB01_CURSO.RetornaPelaChavePrimaria(coemp, coModu, coCur).NO_CUR;
                }

                var infTur = (from tb129 in TB129_CADTURMAS.RetornaTodosRegistros()
                              join tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                on tb129.CO_TUR equals tb06.CO_TUR
                              where tb129.CO_TUR == coTur
                              select new
                              {
                                  tb129.CO_SIGLA_TURMA,
                                  tb06.CO_PERI_TUR
                              }).FirstOrDefault();

                string turno = "";
                switch (infTur.CO_PERI_TUR)
                {
                    case "M":
                        turno = "Matutino";
                        break;
                    case "V":
                        turno = "Vespertino";
                        break;
                    case "N":
                        turno = "Noturno";
                        break;
                }

                if (inforAluno != null)
                {
                    CnpjCPF = "Aluno(a): " + inforAluno.NO_ALU + "<br>Nº NIRE: " + inforAluno.NU_NIRE.ToString() +
                                // Esta informação não é necessária no boleto por se tratar de um código interno de identificação do aluno
                                // por isso a mesma foi retirada das observações do boleto.
                                // " - Matrícula: " + inforAluno.CO_ALU_CAD.Insert(2, ".").Insert(6, ".") +
                                 " - Ano/Mês Refer: " + tb47.NU_PAR.ToString().PadLeft(2, '0') + "/" + tb47.CO_ANO_MES_MAT.Trim() +
                                 "<br>" + deModu + " - Série: " + noCur +
                                 " - Turma: " + infTur.CO_SIGLA_TURMA + " - Turno: " + turno;
                    //CnpjCPF = "Ano Refer: " + inforAluno.CO_ANO_MES_MAT.Trim() + " - Matrícula: " + inforAluno.CO_ALU_CAD.Insert(2, ".").Insert(6, ".") + " - Nº NIRE: " +
                    //    inforAluno.NU_NIRE.ToString() + "<br> Modalidade: " + inforAluno.DE_MODU_CUR + " - Série: " + inforAluno.NO_CUR +
                    //    " - Turma: " + inforAluno.CO_SIGLA_TURMA + " - Turno: " + inforAluno.TURNO + " <br> Aluno(a): " + inforAluno.NO_ALU;

                    boleto.ObsCanhoto = string.Format("Aluno: {0}", inforAluno.NO_ALU);
                }

                boleto.Instrucoes += CnpjCPF + "<br>*** Referente: " + tb47.DE_COM_HIST + " ***";

                //------------> Informações do Sacado
                boleto.BairroSacado = s.BAIRRO;
                boleto.CepSacado = s.CEP;
                boleto.CidadeSacado = s.CIDADE;
                boleto.CpfCnpjSacado = s.CPFCNPJ;
                boleto.EnderecoSacado = s.ENDERECO + " " + s.NUMERO + " " + s.COMPL;
                boleto.NomeSacado = s.NOME;
                boleto.UfSacado = s.UF;

                //------------> Adiciona o título atual na Sessão
                ((List<InformacoesBoletoBancario>)Session[SessoesHttp.BoletoBancarioCorrente]).Add(boleto);

                /*
                * Esta parte do código atualiza o NossoNúmero do título (TB47).
                * Esta linha foi incluída para resolver o problema de boletos diferentes sendo gerados para um mesmo
                * título
                * */

                tb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();
                tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTEReference.Load();
                tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIAReference.Load();
                TB29_BANCO u = TB29_BANCO.RetornaPelaChavePrimaria(tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO);
                //===> Incluí o nosso número na tabela de nossos números por título
                TB045_NOS_NUM tb045 = TB045_NOS_NUM.RetornaPelaChavePrimaria(tb47.CO_EMP, tb47.NU_DOC, tb47.NU_PAR, tb47.DT_CAD_DOC, tb47.CO_NOS_NUM);

                if (tb045 == null)
                {
                    tb045 = new TB045_NOS_NUM();
                    tb045.NU_DOC = tb47.NU_DOC;
                    tb045.NU_PAR = tb47.NU_PAR;
                    tb045.DT_CAD_DOC = tb47.DT_CAD_DOC;
                    tb045.DT_NOS_NUM = DateTime.Now;
                    tb045.IP_NOS_NUM = LoginAuxili.IP_USU;
                    //===> Pega as informações da empresa/unidade
                    TB25_EMPRESA emp = TB25_EMPRESA.RetornaPelaChavePrimaria(tb47.CO_EMP);
                    tb045.TB25_EMPRESA = emp;
                    //===> Pega as informações do colaborador
                    TB03_COLABOR tb03 = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);
                    tb045.TB03_COLABOR = tb03;
                    tb045.CO_NOS_NUM = tb47.CO_NOS_NUM;
                    tb045.CO_BARRA_DOC = tb47.CO_BARRA_DOC;
                    GestorEntities.SaveOrUpdate(tb045, true);
                }

                long nossoNumero = long.Parse(u.CO_PROX_NOS_NUM) + 1;
                int casas = u.CO_PROX_NOS_NUM.Length;
                string mask = string.Empty;
                foreach (char ch in u.CO_PROX_NOS_NUM) mask += "0";
                u.CO_PROX_NOS_NUM = nossoNumero.ToString(mask);
                GestorEntities.SaveOrUpdate(u, true);

                iGrdNeg++;
            }

            //--------> Gera e exibe os boletos
            BoletoBancarioHelpers.GeraBoletos(this);
        }

        protected void chkDescPer_ChenckedChanged(object sender, EventArgs e)
        {
            int retornaInt = 0;
            int qtdItens = 0;
            decimal dcmValor = 0;
            decimal valorTotal = 0;
            decimal valorDesconto = 0;

            foreach (GridViewRow linha in grdSolicitacoes.Rows)
            {

                if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                {
                    qtdItens++;
                    ((TextBox)linha.Cells[4].FindControl("txtQtdeSolic")).Enabled = true;
                    ((TextBox)linha.Cells[4].FindControl("txtDescSolic")).Enabled = true;
                    ((CheckBox)linha.Cells[4].FindControl("chkDescPer")).Enabled = true;
                    if (((TextBox)linha.Cells[4].FindControl("txtQtdeSolic")).Text == "")
                    {
                        ((TextBox)linha.Cells[4].FindControl("txtQtdeSolic")).Text = "1";
                        //qtdTotalItem = qtdTotalItem + 1;
                        if (decimal.TryParse(((Label)linha.Cells[2].FindControl("lblValor")).Text, out dcmValor))
                        {
                            if (((CheckBox)linha.Cells[0].FindControl("chkDescPer")).Checked)
                            {
                                if (decimal.TryParse(((TextBox)linha.Cells[4].FindControl("txtDescSolic")).Text, out valorDesconto))
                                {
                                    valorTotal = valorTotal + (dcmValor - ((valorDesconto / 100) * dcmValor));
                                    ((Label)linha.Cells[5].FindControl("lblTotal")).Text = (dcmValor - ((valorDesconto / 100) * dcmValor)).ToString("#,##0.00");
                                }
                                else
                                {
                                    valorTotal = valorTotal + dcmValor;
                                    ((Label)linha.Cells[5].FindControl("lblTotal")).Text = dcmValor.ToString("#,##0.00");
                                }
                            }
                            else
                            {
                                if (decimal.TryParse(((TextBox)linha.Cells[4].FindControl("txtDescSolic")).Text, out valorDesconto))
                                {
                                    valorTotal = valorTotal + (dcmValor - valorDesconto);
                                    ((Label)linha.Cells[5].FindControl("lblTotal")).Text = (dcmValor - valorDesconto).ToString("#,##0.00");
                                }
                                else
                                {
                                    valorTotal = valorTotal + dcmValor;
                                    ((Label)linha.Cells[5].FindControl("lblTotal")).Text = dcmValor.ToString("#,##0.00");
                                }
                            }
                        }
                        else
                        {
                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = "";
                        }
                    }
                    else
                    {
                        if (int.TryParse(((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text, out retornaInt))
                        {
                            if (decimal.TryParse(((Label)linha.Cells[2].FindControl("lblValor")).Text, out dcmValor))
                            {
                                if (retornaInt > 0)
                                {
                                    //qtdTotalItem = qtdTotalItem + retornaInt;
                                    if (((CheckBox)linha.Cells[0].FindControl("chkDescPer")).Checked)
                                    {
                                        if (decimal.TryParse(((TextBox)linha.Cells[4].FindControl("txtDescSolic")).Text, out valorDesconto))
                                        {
                                            valorTotal = valorTotal + ((dcmValor - ((valorDesconto / 100) * dcmValor)) * retornaInt);
                                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = ((dcmValor - ((valorDesconto / 100) * dcmValor)) * retornaInt).ToString("#,##0.00");
                                        }
                                        else
                                        {
                                            valorTotal = valorTotal + (dcmValor * retornaInt);
                                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = (dcmValor * retornaInt).ToString("#,##0.00");
                                        }
                                    }
                                    else
                                    {
                                        if (decimal.TryParse(((TextBox)linha.Cells[4].FindControl("txtDescSolic")).Text, out valorDesconto))
                                        {
                                            valorTotal = valorTotal + ((dcmValor - valorDesconto) * retornaInt);
                                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = ((dcmValor - valorDesconto) * retornaInt).ToString("#,##0.00");
                                        }
                                        else
                                        {
                                            valorTotal = valorTotal + (dcmValor * retornaInt);
                                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = (dcmValor * retornaInt).ToString("#,##0.00");
                                        }
                                    }
                                    //valorTotal = valorTotal + (dcmValor * retornaInt);
                                }
                                else
                                {
                                    ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text = "1";
                                    if (((CheckBox)linha.Cells[0].FindControl("chkDescPer")).Checked)
                                    {
                                        if (decimal.TryParse(((TextBox)linha.Cells[4].FindControl("txtDescSolic")).Text, out valorDesconto))
                                        {
                                            valorTotal = valorTotal + (dcmValor - ((valorDesconto / 100) * dcmValor));
                                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = ((valorDesconto / 100) * dcmValor).ToString("#,##0.00");
                                        }
                                        else
                                        {
                                            valorTotal = valorTotal + dcmValor;
                                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = dcmValor.ToString("#,##0.00");
                                        }
                                    }
                                    else
                                    {
                                        if (decimal.TryParse(((TextBox)linha.Cells[4].FindControl("txtDescSolic")).Text, out valorDesconto))
                                        {
                                            valorTotal = valorTotal + (dcmValor - valorDesconto);
                                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = (dcmValor - valorDesconto).ToString("#,##0.00");
                                        }
                                        else
                                        {
                                            valorTotal = valorTotal + dcmValor;
                                            ((Label)linha.Cells[5].FindControl("lblTotal")).Text = dcmValor.ToString("#,##0.00");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ((Label)linha.Cells[5].FindControl("lblTotal")).Text = "";
                            }
                        }
                        else
                        {
                            ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text = "1";
                            //qtdTotalItem = qtdTotalItem + 1;
                            if (decimal.TryParse(((Label)linha.Cells[2].FindControl("lblValor")).Text, out dcmValor))
                            {
                                if (((CheckBox)linha.Cells[0].FindControl("chkDescPer")).Checked)
                                {
                                    if (decimal.TryParse(((TextBox)linha.Cells[4].FindControl("txtDescSolic")).Text, out valorDesconto))
                                    {
                                        valorTotal = valorTotal + (dcmValor - ((valorDesconto / 100) * dcmValor));
                                        ((Label)linha.Cells[5].FindControl("lblTotal")).Text = (dcmValor - ((valorDesconto / 100) * dcmValor)).ToString("#,##0.00");
                                    }
                                    else
                                    {
                                        valorTotal = valorTotal + dcmValor;
                                        ((Label)linha.Cells[5].FindControl("lblTotal")).Text = dcmValor.ToString("#,##0.00");
                                    }
                                }
                                else
                                {
                                    if (decimal.TryParse(((TextBox)linha.Cells[4].FindControl("txtDescSolic")).Text, out valorDesconto))
                                    {
                                        valorTotal = valorTotal + (dcmValor - valorDesconto);
                                        ((Label)linha.Cells[5].FindControl("lblTotal")).Text = (dcmValor - valorDesconto).ToString("#,##0.00");
                                    }
                                    else
                                    {
                                        valorTotal = valorTotal + dcmValor;
                                        ((Label)linha.Cells[5].FindControl("lblTotal")).Text = dcmValor.ToString("#,##0.00");
                                    }
                                }

                            }
                            else
                            {
                                ((Label)linha.Cells[5].FindControl("lblTotal")).Text = "";
                            }
                        }
                    }
                }
                else
                {
                    ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Enabled = false;
                    ((TextBox)linha.Cells[3].FindControl("txtQtdeSolic")).Text = "";
                    ((CheckBox)linha.Cells[0].FindControl("chkDescPer")).Checked = false;
                    ((TextBox)linha.Cells[4].FindControl("txtDescSolic")).Text = "";
                    ((CheckBox)linha.Cells[0].FindControl("chkDescPer")).Enabled = false;
                    ((TextBox)linha.Cells[4].FindControl("txtDescSolic")).Enabled = false;
                    ((Label)linha.Cells[5].FindControl("lblTotal")).Text = "";
                }

            }
            if (qtdItens > 1)
            {
                if (ckbAtualizaFinancSolic.Checked)
                {
                    if (!chkConsolValorTitul.Checked)
                    {
                        chkConsolValorTitul.Enabled = true;
                        //chkConsolValorTitul.Checked = false;

                        txtQtdParcelas.Text = "";
                        txtQtdParcelas.Enabled = false;

                        ddlTipoDesctoParc.Enabled = false;

                        txtDesctoMensaParc.Text = "";
                        txtDesctoMensaParc.Enabled = false;

                        txtQtdeMesesDesctoParc.Text = "";
                        txtQtdeMesesDesctoParc.Enabled = false;

                        txtMesIniDescontoParc.Text = "";
                        txtMesIniDescontoParc.Enabled = false;
                    }
                }
                else
                {
                    //chkConsolValorTitul.Enabled = false;
                    //chkConsolValorTitul.Checked = false;

                    txtQtdParcelas.Text = "";
                    txtQtdParcelas.Enabled = false;

                    ddlTipoDesctoParc.Enabled = false;

                    txtDesctoMensaParc.Text = "";
                    txtDesctoMensaParc.Enabled = false;

                    txtQtdeMesesDesctoParc.Text = "";
                    txtQtdeMesesDesctoParc.Enabled = false;

                    txtMesIniDescontoParc.Text = "";
                    txtMesIniDescontoParc.Enabled = false;
                }
            }
            else
            {
                if (ckbAtualizaFinancSolic.Checked)
                {
                    //chkConsolValorTitul.Enabled = false;
                    //chkConsolValorTitul.Checked = false;

                    txtQtdParcelas.Text = "";
                    txtQtdParcelas.Enabled = true;

                    ddlTipoDesctoParc.Enabled = true;
                    ddlTipoDesctoParc.SelectedValue = "T";

                    txtDesctoMensaParc.Text = "";
                    txtDesctoMensaParc.Enabled = true;

                    txtQtdeMesesDesctoParc.Text = "";
                    txtQtdeMesesDesctoParc.Enabled = false;

                    txtMesIniDescontoParc.Text = "";
                    txtMesIniDescontoParc.Enabled = false;
                }
                else
                {
                    chkConsolValorTitul.Enabled = true;
                    chkConsolValorTitul.Checked = false;

                    txtQtdParcelas.Text = "";
                    txtQtdParcelas.Enabled = true;

                    ddlTipoDesctoParc.Enabled = true;
                    ddlTipoDesctoParc.SelectedValue = "T";

                    txtDesctoMensaParc.Text = "";
                    txtDesctoMensaParc.Enabled = false;

                    txtQtdeMesesDesctoParc.Text = "";
                    txtQtdeMesesDesctoParc.Enabled = false;

                    txtMesIniDescontoParc.Text = "";
                    txtMesIniDescontoParc.Enabled = false;
                }
            }
            txtValorTotal.Text = valorTotal.ToString("#,##0.00");
            CarregaAluno();
        }

        protected void lnkMontaGridParcMaterial_Click(object sender, EventArgs e)
        {
            //ControlaTabs("UMA");
            //ControlaChecks(chkMenEscAlu);

            if (ddlSerieCurso.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar grid de mensalidade porque série não foi selecionada.");
                return;
            }

            if (!ckbAtualizaFinancSolic.Checked)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar grid de mensalidade porque não será atualizado no financeiro.");
                return;
            }

            if (ddlBoletoSolic.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar grid de mensalidade porque não foi informado o boleto bancário");
                return;
            }


            if (chkConsolValorTitul.Checked)
            {
                if (txtQtdParcelas.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar grid de mensalidade porque não foi informado a quantidade de parcelas (QT).");
                    return;
                }

                if (txtDtVectoSolic.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar grid de mensalidade porque não foi informada a data de vencimento da primeira parcela.");
                    return;
                }
            }

            MontaGridParcelasMaterial();
        }

        //protected void lnkMatUniAlu_Click(object sender, EventArgs e)
        //{
        //    //int nuNire = int.Parse(txtNireAluno.Text);
        //    //int coAlu = TB07_ALUNO.RetornaTodosRegistros().Where(a => a.NU_NIRE == nuNire).FirstOrDefault().CO_ALU;
        //    int coEmp = TB129_CADTURMAS.RetornaPelaChavePrimaria(int.Parse(ddlTurma.SelectedValue)).CO_EMP_UNID_CONT;
        //    int coMod = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
        //    int coCur = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
        //    int coTur = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
        //    DateTime dtPrev;
        //    if (txtDtPrevisao.Text != "")
        //    {
        //        dtPrev = DateTime.Parse(txtDtPrevisao.Text);
        //    }
        //    else
        //    {
        //        AuxiliPagina.EnvioMensagemErro(this, "Não é possível finalizar porque a data de previsão da entrega não foi informada.");
        //        return;
        //    }


        //    #region Gravação dos títulos

        //    if (!ckbAtualizaFinancSolic.Checked)
        //    {
        //        AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar a grid financeira porque a opção de atualização do financeiro não foi selecionada.");
        //        return;

        //    }
        //    #endregion

        //    AuxiliPagina.EnvioMensagemSucesso(this, "Material(s) e Uniforme(s) atualizado(s) com sucesso.");

        //    //chkMatEsc.Enabled = false;
        //    //lblSucMatEsc.Visible = true;
        //    //chkDocMat.Checked = true;
        //    //ControlaTabs("DOC");
        //}

        #endregion
    }
}
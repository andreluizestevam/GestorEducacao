//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: MATRÍCULA NOVA PARA ALUNO
// OBJETIVO: MATRÍCULAS DE ALUNOS NOVOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------

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

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5214_IncFinanParcMatric
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }
        private Dictionary<string, string> tipoContrato = AuxiliBaseApoio.chave(tipoContratoCurso.ResourceManager);
        private Dictionary<string, string> tipoValor = AuxiliBaseApoio.chave(tipoValorCurso.ResourceManager);
        private Dictionary<string, string> tipoTurma = AuxiliBaseApoio.chave(tipoTurnoTurma.ResourceManager);

        #region Eventos
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            //CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidades();
                CarregaAnos();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
                CarregaALuno();
                CarregaTipoValor();
                CarregaTipoContrato();
                CarregaBolsasAlt();
                CarregaDiasVencimento();
                divFin.Visible = false;
            }
        }

        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coMod = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coCur = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int coTur = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            string coAno = ddlAno.SelectedValue;

            TB07_ALUNO alu = ddlAluno.SelectedValue != "" && ddlUnidade.SelectedValue != "" ? TB07_ALUNO.RetornaPelaChavePrimaria(int.Parse(ddlAluno.SelectedValue), int.Parse(ddlUnidade.SelectedValue)) : (TB07_ALUNO)null;

            /*int contTit = TB47_CTA_RECEB.RetornaTodosRegistros().Where(w => w.CO_ALU == coAlu && w.CO_EMP == coEmp && w.CO_MODU_CUR == coMod && w.CO_CUR == coCur && w.CO_TUR == coTur && w.CO_ANO_MES_MAT == coAno).Count();

            if (contTit == 0)
            {*/
                #region Atualiza o financeiro
                if (chkAtualiFinan.Checked)
                {
                    int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

                    int proxSerie = Convert.ToInt32(this.ddlSerieCurso.SelectedValue);
                    int proxTurma = Convert.ToInt32(this.ddlTurma.SelectedValue);

                    TB01_CURSO hTb01 = TB01_CURSO.RetornaPeloCoCur(proxSerie);

                    var refAluno = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                    refAluno.TB108_RESPONSAVELReference.Load();

                    TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);

                    TB44_MODULO tb44 = TB44_MODULO.RetornaPelaChavePrimaria(modalidade);

                    if (tb44.CO_SEQU_PC != null && tb44.CO_SEQU_PC_BANCO != null && tb44.CO_SEQU_PC_CAIXA != null)
                    {
                        //----------------> Cria uma lista da TB47_CTA_RECEB
                        List<TB47_CTA_RECEB> lstTb47 = new List<TB47_CTA_RECEB>();

                        TB47_CTA_RECEB tb47;

                        //----------------> Lança a(s) parcela(s) no contas a receber
                        for (int i = 0; i < grdNegociacao.Rows.Count; i++)
                        {
                            tb47 = new TB47_CTA_RECEB();
                            tb47.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(refAluno.TB25_EMPRESA1.CO_EMP);
                            tb47.CO_EMP_UNID_CONT = coEmp;
                            tb47.NU_DOC = grdNegociacao.Rows[i].Cells[0].Text;
                            tb47.NU_PAR = int.Parse(grdNegociacao.Rows[i].Cells[1].Text);
                            tb47.QT_PAR = grdNegociacao.Rows.Count;
                            tb47.DT_CAD_DOC = DateTime.Now;
                            //tb47.DT_CAD_DOC = DateTime.Parse("05/01/" + proxAno);
                            tb47.DE_COM_HIST = "VALOR MENSALIDADE ESCOLAR.";
                            tb47.VR_TOT_DOC = decimal.Parse(txtTotalMensa.Text);
                            tb47.VR_PAR_DOC = decimal.Parse(grdNegociacao.Rows[i].Cells[3].Text);
                            tb47.DT_VEN_DOC = DateTime.Parse(grdNegociacao.Rows[i].Cells[2].Text);
                            tb47.VL_DES_BOLSA_ALUNO = decimal.Parse(grdNegociacao.Rows[i].Cells[4].Text);
                            tb47.VR_DES_DOC = decimal.Parse(grdNegociacao.Rows[i].Cells[5].Text);
                            tb47.VR_MUL_DOC = decimal.Parse(grdNegociacao.Rows[i].Cells[7].Text);
                            tb47.VR_JUR_DOC = decimal.Parse(string.Format("{0:0.0000}", decimal.Parse(grdNegociacao.Rows[i].Cells[8].Text)));
                            tb47.DT_EMISS_DOCTO = DateTime.Now;


                            // Alterar para o campo do CO_AGRUP_REC
                            tb47.CO_AGRUP_RECDESP = TB83_PARAMETRO.RetornaPelaChavePrimaria(refAluno.TB25_EMPRESA1.CO_EMP).CO_AGRUP_REC;
                            //tb47.DT_EMISS_DOCTO = DateTime.Parse("05/01/" + proxAno);

                            //--------------------> Flag emissão boleto "S"im ou "N"ão
                            if (ddlBoleto.SelectedValue != "")
                            {
                                tb47.FL_EMITE_BOLETO = "S";
                                tb47.FL_TIPO_PREV_RECEB = "B";
                                //------------------------> Salvando o tipo de documento "Boleto Bancário"
                                tb47.TB086_TIPO_DOC = TB086_TIPO_DOC.RetornaPeloCoTipoDoc(3);

                                //------------------------> Dados do boleto bancário
                                tb47.TB227_DADOS_BOLETO_BANCARIO = TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(int.Parse(ddlBoleto.SelectedValue));
                            }
                            else
                            {
                                tb47.FL_EMITE_BOLETO = "N";
                                //------------------------> Salvando o tipo de documento "Recibo"
                                tb47.TB086_TIPO_DOC = TB086_TIPO_DOC.RetornaPeloCoTipoDoc(1);
                            }

                            tb47.TB39_HISTORICO = hTb01.ID_HISTO_MENSA == null ? (from tb39 in TB39_HISTORICO.RetornaTodosRegistros()
                                                                                  where tb39.DE_HISTORICO.Contains("Mensalidade")
                                                                                  select tb39).FirstOrDefault() : TB39_HISTORICO.RetornaPelaChavePrimaria((int)hTb01.ID_HISTO_MENSA);

                            if (tb44.CO_CENT_CUSTO != null)
                                tb47.TB099_CENTRO_CUSTO = TB099_CENTRO_CUSTO.RetornaPelaChavePrimaria(tb44.CO_CENT_CUSTO.Value);

                            tb47.TB56_PLANOCTA = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb44.CO_SEQU_PC.Value);
                            tb47.CO_SEQU_PC_BANCO = tb44.CO_SEQU_PC_BANCO.Value;
                            tb47.CO_SEQU_PC_CAIXA = tb44.CO_SEQU_PC_CAIXA.Value;

                            tb47.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                            tb47.CO_FLAG_TP_VALOR_MUL = "P";
                            tb47.CO_FLAG_TP_VALOR_JUR = tb25.CO_FLAG_TP_VALOR_JUROS != null ? tb25.CO_FLAG_TP_VALOR_JUROS : "P";
                            tb47.CO_FLAG_TP_VALOR_DES = "V";
                            tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO = "V";
                            tb47.CO_FLAG_TP_VALOR_OUT = "V";

                            tb47.IC_SIT_DOC = "A";
                            tb47.TP_CLIENTE_DOC = "A";
                            ///Formato =>Ano: XXXX - Série: XXXXX - Turma: XXXXX - Turno: XXXXX
                            tb47.DE_OBS_BOL_MAT = "Ano: " + DateTime.Parse(grdNegociacao.Rows[i].Cells[2].Text).ToString("yyyy") + " - Série/Curso: " + ddlSerieCurso.SelectedItem + " - Turma: " + ddlTurma.SelectedItem + " - Turno: " + ddlAno.SelectedValue;

                            tb47.DE_OBS = "MENSALIDADE ESCOLAR";

                            tb47.TP_CLIENTE_DOC = "A";

                            tb47.CO_ALU = coAlu;
                            //tb47.CO_ANO_MES_MAT = DateTime.Now.Year.ToString();
                            tb47.CO_ANO_MES_MAT = PreAuxili.proximoAnoMat<string>(ddlAno.SelectedValue);
                            tb47.NU_SEM_LET = "1";
                            tb47.CO_CUR = proxSerie;
                            tb47.CO_TUR = proxTurma;
                            tb47.CO_MODU_CUR = modalidade;
                            tb47.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(refAluno.TB108_RESPONSAVEL.CO_RESP);

                            tb47.DT_SITU_DOC = DateTime.Now;
                            tb47.DT_ALT_REGISTRO = DateTime.Now;
                            //tb47.DT_ALT_REGISTRO = DateTime.Parse("05/01/" + proxAno);
                            ///Atualiza o código da bolsa
                            refAluno.TB148_TIPO_BOLSAReference.Load();
                            if (refAluno.TB148_TIPO_BOLSA != null)
                                tb47.TB148_TIPO_BOLSA = TB148_TIPO_BOLSA.RetornaPelaChavePrimaria(refAluno.TB148_TIPO_BOLSA.CO_TIPO_BOLSA);

                            //lstTb47.Add(tb47);
                            TB47_CTA_RECEB.SaveOrUpdate(tb47);

                        }
                    }
                }
                #endregion

                #region Atualiza valores no cadastro da matrícula

                //Resgata um objeto da entidade de Matrícula para atualizar os valores financeiros.
                TB08_MATRCUR tb08 = TB08_MATRCUR.RetornaPeloAluno(coAlu, coAno);

                if (chkAtualiFinan.Checked)
                {
                    tb08.VL_TOT_MODU_MAT = decimal.Parse(txtTotalMensa.Text);
                    tb08.VL_DES_MOD_MAT = decimal.Parse(txtTotalDesctoEspec.Text);
                    tb08.VL_DES_BOL_MOD_MAT = decimal.Parse(txtTotalDesctoBolsa.Text);
                    tb08.VL_ENT_MOD_MAT = decimal.Parse(grdNegociacao.Rows[0].Cells[3].Text);
                    tb08.VL_PAR_MOD_MAT = decimal.Parse(grdNegociacao.Rows[0].Cells[3].Text);
                    int qtPar = 0;
                    foreach (GridViewRow l in grdNegociacao.Rows)
                    {
                        if (int.Parse(l.Cells[1].Text) != 0)
                        {
                            qtPar++;
                        }
                        else
                        {
                            tb08.VL_TAXA_MATRIC = decimal.Parse(l.Cells[3].Text);
                        }
                    }

                    //tb08.QT_PAR_MOD_MAT = grdNegociacao.Rows.Count;
                    tb08.QT_PAR_MOD_MAT = qtPar;

                    //==========> Este if verifica se existe mais de um registro na grid de neciação
                    if (grdNegociacao.Rows.Count > 1)
                    {
                        //=========> Caso tenha mais de 1 registro, ele pega o valor do próximo registro
                        tb08.NU_DIA_VEN_MOD_MAT = DateTime.Parse(grdNegociacao.Rows[1].Cells[2].Text).Day;
                        tb08.VL_PAR_MOD_MAT = decimal.Parse(grdNegociacao.Rows[1].Cells[3].Text);
                    }
                    else
                    {
                        //=========> Caso tenha somente 1 registro, ele pega o valor deste único registro
                        tb08.NU_DIA_VEN_MOD_MAT = DateTime.Parse(grdNegociacao.Rows[0].Cells[2].Text).Day;
                        tb08.VL_PAR_MOD_MAT = decimal.Parse(grdNegociacao.Rows[0].Cells[3].Text);
                    }
                    tb08.DT_PRI_PAR_MOD_MAT = DateTime.Parse(grdNegociacao.Rows[0].Cells[2].Text);
                    tb08.QT_PAR_DES_MAT = txtQtdeMesesDesctoMensa.Text != "" ? (int?)int.Parse(txtQtdeMesesDesctoMensa.Text) : null;

                    TB08_MATRCUR.SaveOrUpdate(tb08, true);
                }
                #endregion

                AuxiliPagina.EnvioAvisoGeralSistema(this, "Títulos de Mensalidades do Alunos Matriculado Gravados com Sucesso no Financeiro. A impressão dos Boletos devera ser na funcionalidade específica.");
            /*}
            else
            {
                AuxiliPagina.EnvioMensagemErro(this, "Já existem Títulos Lançados para esta Matrícula.");
                return;
            }*/
        }
        #endregion

        #region Carregamento
        private void CarregaUnidades()
        {
            var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                       select new
                       {
                           tb25.NO_FANTAS_EMP,
                           tb25.CO_EMP
                       });

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";

            ddlUnidade.DataSource = res;
            ddlUnidade.DataBind();
        }

        /// <summary>
        /// Carrega os anos que tiveram matrículas
        /// </summary>
        private void CarregaAnos()
        {
            AuxiliCarregamentos.CarregaAno(ddlAno, LoginAuxili.CO_EMP, false);
        }

        /// <summary>
        /// Carrega as Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }

        /// <summary>
        /// Carrega as Séries de acordo com a modalidade selecionada
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            string anoGrade = PreAuxili.proximoAnoMat<string>(ddlAno.SelectedValue);
            AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, LoginAuxili.CO_EMP, false);
        }

        /// <summary>
        /// Carrega as Turmas de acordo com a modalidade e série selecionadas.
        /// </summary>
        private void CarregaTurma()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaTurmasSigla(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, false);
        }

        /// <summary>
        /// Carrega os alunos matriculados nas modalidade serie e curso selecionadas.
        /// </summary>
        private void CarregaALuno()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coMod = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coCur = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int coTur = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string coAno = ddlAno.SelectedValue;
            AuxiliCarregamentos.CarregaAlunoMatriculado(ddlAluno, LoginAuxili.CO_EMP, coMod, coCur, coTur, coAno, false);
        }

        private void CarregaBolsas()
        {
            ddlBolsaAlunoAlt.DataSource = TB148_TIPO_BOLSA.RetornaTodosRegistros().Where(c => c.TP_GRUPO_BOLSA == ddlTpBolsaAlt.SelectedValue && c.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                && c.CO_SITUA_TIPO_BOLSA == "A");

            ddlBolsaAlunoAlt.DataTextField = "NO_TIPO_BOLSA";
            ddlBolsaAlunoAlt.DataValueField = "CO_TIPO_BOLSA";
            ddlBolsaAlunoAlt.DataBind();

            ddlBolsaAlunoAlt.Items.Insert(0, new ListItem("Nenhuma", ""));
        }

        private void CarregaTipoValor()
        {
            ddlTipoValorCurso.Items.Clear();
            ddlTipoValorCurso.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(tipoValorCurso.ResourceManager));
            ddlTipoValorCurso.SelectedValue = tipoValor[tipoValorCurso.P];
        }

        private void CarregaTipoContrato()
        {
            ddlTipoContrato.Items.Clear();
            ddlTipoContrato.Items.AddRange(AuxiliBaseApoio.BaseApoioDDL(tipoContratoCurso.ResourceManager));
        }

        private void CarregaBolsasAlt()
        {
            ddlBolsaAlunoAlt.DataSource = TB148_TIPO_BOLSA.RetornaTodosRegistros().Where(c => c.TP_GRUPO_BOLSA == ddlTpBolsaAlt.SelectedValue && c.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                && c.CO_SITUA_TIPO_BOLSA == "A");

            ddlBolsaAlunoAlt.DataTextField = "NO_TIPO_BOLSA";
            ddlBolsaAlunoAlt.DataValueField = "CO_TIPO_BOLSA";
            ddlBolsaAlunoAlt.DataBind();

            ddlBolsaAlunoAlt.Items.Insert(0, new ListItem("Nenhuma", ""));
            ddlBolsaAlunoAlt.Items.Insert(1, new ListItem("Livre", "0"));

            txtValorDescto.Text = txtPeriodoIniDesconto.Text = txtPeriodoFimDesconto.Text = "";
            chkManterDescontoPerc.Checked = chkManterDescontoPerc.Enabled =
            txtValorDescto.Enabled = txtPeriodoIniDesconto.Enabled = txtPeriodoFimDesconto.Enabled = false;
        }

        private void CarregaDiasVencimento()
        {
            ddlDiaVecto.Items.Clear();
            ddlDiaVecto.Items.AddRange(AuxiliBaseApoio.DiaVencimentoTitulo());

            int modalidade = (ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int serie = (ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0);
            var curso = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
            int diaVencimento = ((curso != null && curso.NU_MDV != null) ? (curso.NU_MDV ?? 5) : 5);

            if (ddlDiaVecto.Items.FindByValue(diaVencimento.ToString()) != null)
                ddlDiaVecto.SelectedValue = diaVencimento.ToString();
        }

        private void CarregaBoletos()
        {
            int coEmp = TB129_CADTURMAS.RetornaPelaChavePrimaria(int.Parse(ddlTurma.SelectedValue)).CO_EMP_UNID_CONT;
            int coBoleto = (TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, int.Parse(ddlModalidade.SelectedValue), int.Parse(ddlSerieCurso.SelectedValue)).ID_BOLETO_MATR ?? 0);
            int coMod = (!string.IsNullOrEmpty(ddlModalidade.SelectedValue) ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int coCur = (!string.IsNullOrEmpty(ddlSerieCurso.SelectedValue) ? int.Parse(ddlSerieCurso.SelectedValue) : 0);

            AuxiliCarregamentos.CarregaBoletos(ddlBoleto, coEmp, "E", coMod, coCur, false, false);
            
            ddlBoleto.Items.Insert(0, new ListItem("Nenhum", ""));
            if (coBoleto > 0 && ddlBoleto.Items.FindByValue(coBoleto.ToString()) != null)
                ddlBoleto.SelectedValue = coBoleto.ToString();
        }

        /// <summary>
        /// Carrega os valores do contrato.
        /// </summary>
        protected void CarregaValorContrato()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);
            string turnoTurma = "";
            if (!String.IsNullOrEmpty(ddlTurma.SelectedValue))
                turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;

            //-----------> Valida o turno do curso e coloca o valor do contrato no campo de valor do contrato de acordo com o turno da turma.
            if (varSer == null)
            {
                txtValorContratoCalc.Text = "";
                return;
            }
            switch (turnoTurma)
            {
                case "M":
                    //--------> Turma Matutina
                    #region Turno Matutino
                    switch (ddlTipoContrato.SelectedValue)
                    {
                        case "P":
                            if (varSer.VL_CONTMAN_APRAZ == null)
                            {
                                txtValorContratoCalc.Text = "";
                                return;
                            }
                            //---------> A Prazo
                            txtValorContratoCalc.Text = varSer.VL_CONTMAN_APRAZ.ToString();
                            break;
                        case "V":
                            if (varSer.VL_CONTMAN_AVIST == null)
                            {
                                txtValorContratoCalc.Text = "";
                                return;
                            }
                            //---------> A Vista
                            txtValorContratoCalc.Text = varSer.VL_CONTMAN_AVIST.ToString();
                            break;
                    }
                    break;
                    #endregion
                case "V":
                    //--------> Turma Vespertina
                    #region Turno Vespertino
                    switch (ddlTipoContrato.SelectedValue)
                    {
                        case "P":
                            if (varSer.VL_CONTTAR_APRAZ == null)
                            {
                                txtValorContratoCalc.Text = "";
                                return;
                            }
                            //---------> A Prazo
                            txtValorContratoCalc.Text = varSer.VL_CONTTAR_APRAZ.ToString();
                            break;
                        case "V":
                            if (varSer.VL_CONTTAR_AVIST == null)
                            {
                                txtValorContratoCalc.Text = "";
                                return;
                            }
                            //---------> A Vista
                            txtValorContratoCalc.Text = varSer.VL_CONTTAR_AVIST.ToString();
                            break;
                    }
                    break;
                    #endregion
                case "N":
                    //--------> Turma Noturna
                    #region Turno Noturno
                    switch (ddlTipoContrato.SelectedValue)
                    {
                        case "P":
                            if (varSer.VL_CONTNOI_APRAZ == null)
                            {
                                txtValorContratoCalc.Text = "";
                                return;
                            }
                            //---------> A Prazo
                            txtValorContratoCalc.Text = varSer.VL_CONTNOI_APRAZ.ToString();
                            break;
                        case "V":
                            if (varSer.VL_CONTNOI_AVIST == null)
                            {
                                txtValorContratoCalc.Text = "";
                                return;
                            }
                            //---------> A Vista
                            txtValorContratoCalc.Text = varSer.VL_CONTNOI_AVIST.ToString();
                            break;
                    }
                    break;
                    #endregion
            }
        }

        /// <summary>
        /// Carrega a data e valor da primeira parcela
        /// </summary>
        protected void CarregaDataValorPrimParc()
        {
            if (txtDtPrimeiraParcela.Text == "")
            {
                txtDtPrimeiraParcela.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }

            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);
            string turnoTurma = "";
            if (!String.IsNullOrEmpty(ddlTurma.SelectedValue)) 
                turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;
            if (varSer == null)
            {
                txtQtdeParcelas.Text = "";
                return;
            }

            txtQtdeParcelas.Text = varSer.NU_QUANT_MESES.ToString();
            decimal x = 0;
            string retornoValor = PreAuxili.valorContratoCurso(ddlTipoValorCurso.SelectedValue,
                ddlTipoContrato.SelectedValue,
                turnoTurma,
                varSer,
                this.Page, false);
            if (retornoValor == string.Empty)
                return;
            else
            {
                x = decimal.Parse(retornoValor);
                txtValorPrimParce.Text = Math.Round(x, 2).ToString();
            }

        }

        /// <summary>
        /// Carrega os valores de Taxa de Matrícula no campo responsável por apresentá-los
        /// </summary>
        protected void carregaValorTaxaMatricula()
        {
            //--------> Retorna o turno da turma selecionada (V - Vespertino, M - Matutino, N - Noturno)
            string turnoTurma = "";
            if (!String.IsNullOrEmpty(ddlTurma.SelectedValue)) 
                turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);
            decimal menCurPre = 0;

            ///Verifica se a primeira mensalidade é a taxa de matrícula e se foi informada a taxa de matrícula
            if (varSer == null)
            {
               
            }
            else if (!varSer.FL_MENS_TAXA_MATR && varSer.FL_VALCON_TXMAT == "S")
            {
                if (turnoTurma == tipoTurma[tipoTurnoTurma.M])
                    menCurPre = varSer.VL_TXMAT_MAN ?? decimal.Zero;
                else if (turnoTurma == tipoTurma[tipoTurnoTurma.V])
                    menCurPre = varSer.VL_TXMAT_TAR ?? decimal.Zero;
                else if (turnoTurma == tipoTurma[tipoTurnoTurma.N])
                    menCurPre = varSer.VL_TXMAT_NOI ?? decimal.Zero;
                else if (turnoTurma == tipoTurma[tipoTurnoTurma.I])
                    menCurPre = varSer.VL_TXMAT_INT ?? decimal.Zero;
                else if (turnoTurma == tipoTurma[tipoTurnoTurma.E])
                    menCurPre = varSer.VL_TXMAT_ESP ?? decimal.Zero;
            }

            txtVlTxMatricula.Text = menCurPre.ToString();
            txtVlTxMatricula.Enabled = true;
        }

        protected void MontaGridNegociacao()
        {
            int coEmp = TB129_CADTURMAS.RetornaPelaChavePrimaria(int.Parse(ddlTurma.SelectedValue)).CO_EMP_UNID_CONT;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            TB07_ALUNO tb07 = TB07_ALUNO.RetornaPelaChavePrimaria(coAlu, LoginAuxili.CO_EMP);

            //--------> Retorna o turno da turma selecionada (V - Vespertino, M - Matutino, N - Noturno)
            string turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;

            var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);
            var tb83 = TB83_PARAMETRO.RetornaPelaChavePrimaria(coEmp);

            //--------> Mensalidade do Curso
            decimal menCur = 0;

            /// Valor taxa matrícula
            decimal menCurPre = 0;

            //--------> Verifica se o usuário selecionou um tipo de contrato
            if (ddlTipoContrato.SelectedValue == "")
            {
                ddlTipoContrato.SelectedValue = "P";
                ddlTipoContrato.DataBind();
            }

            #region Verifica o tipo de valor

            string retornoValor = PreAuxili.valorContratoCurso(ddlTipoValorCurso.SelectedValue,
                ddlTipoContrato.SelectedValue,
                turnoTurma,
                varSer,
                this.Page, false);
            if (retornoValor == string.Empty)
                return;
            else
            {
                if (chkAlterValorContr.Checked)
                {
                    ///Valida o tipo de calculo do valor (P - Proporcional a quantidade de meses, T - Total de meses do curso)
                    switch (ddlValorContratoCalc.SelectedValue)
                    {
                        ///Proporcional a quantidade de meses
                        case "P":
                            if (txtValorContratoCalc.Text != "")
                            {
                                ///menCur = (Decimal.Parse(txtValorContratoCalc.Text)/varSer.NU_QUANT_MESES) * Decimal.Parse(txtQtdeParcelas.Text);
                                menCur = Decimal.Parse(txtValorContratoCalc.Text);
                            }
                            else
                                menCur = (Decimal.Parse(retornoValor) / varSer.NU_QUANT_MESES) * Decimal.Parse(txtQtdeParcelas.Text);
                            break;

                        ///Total de meses do curso
                        case "T":
                            if (txtValorContratoCalc.Text != "")
                            {
                                menCur = Decimal.Parse(txtValorContratoCalc.Text);
                            }
                            else
                                menCur = Decimal.Parse(retornoValor);
                            break;
                    }
                }
                else
                {
                    menCur = Decimal.Parse(retornoValor);
                }
            }

            #endregion

            if (menCur == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Série/Curso informado não apresenta mensalidade.");
                return;
            }
            else
            {
                if (chkDataPrimeiraParcela.Checked)
                {
                    if (Decimal.Parse(txtValorPrimParce.Text) > menCur)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valor da 1ª Parcela esta inconsistente.");
                        return;
                    }
                }

                if (txtDesctoMensa.Text != "")
                {
                    if (Decimal.Parse(txtDesctoMensa.Text) > menCur)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Valor de Desconto Especial inconsistente.");
                        return;
                    }
                }
            }
            int qtdParcCur = int.Parse(txtQtdeParcelas.Text);
            if (qtdParcCur == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Quantidade de parcelas informada para Série/Curso deve ser maior que zero.");
                return;
            }
            int qtdDiasInterMeses = varSer.QT_INTERV_DIAS != null ? varSer.QT_INTERV_DIAS.Value : 0;
            decimal desCur = 0;
            decimal totalMensa = 0;
            decimal totalDescto = 0;
            decimal totalDesctoEspec = 0;
            decimal totalValorLiqui = 0;
            decimal multaUnid = tb83.VL_PERCE_MULTA != null ? (decimal)tb83.VL_PERCE_MULTA : 0;
            decimal jurosUnid = tb83.VL_PERCE_JUROS != null ? (decimal)tb83.VL_PERCE_JUROS : 0;
            int diaVenctoUnid = int.Parse(ddlDiaVecto.SelectedValue);
            int qtdeMesesDescto = txtQtdeMesesDesctoMensa.Text != "" ? int.Parse(txtQtdeMesesDesctoMensa.Text) : 0;
            int mesDescto = txtMesIniDesconto.Text != "" ? int.Parse(txtMesIniDesconto.Text) : 0;
            //int numNire = txtNireAluno.Text.Replace(".", "").Replace("-", "") != "" ? int.Parse(txtNireAluno.Text.Replace(".", "").Replace("-", "")) : 0;
            int numNire = tb07.NU_NIRE;

            if ((menCur > 0) && (numNire > 0))
            {
                DateTime dataSelec;
                if (chkDataPrimeiraParcela.Checked && txtDtPrimeiraParcela.Text != "")
                {
                    dataSelec = DateTime.Parse(txtDtPrimeiraParcela.Text);
                }
                else
                {
                    dataSelec = DateTime.Now.Year == PreAuxili.proximoAnoMat<int>(ddlAno.SelectedValue) ? (int.Parse(ddlDiaVecto.SelectedValue) > 27 && DateTime.Now.Month == 2 ? DateTime.Parse("27/" + DateTime.Now.Month.ToString("D2") + '/' + DateTime.Now.Year.ToString()) : DateTime.Parse(ddlDiaVecto.SelectedValue + '/' + DateTime.Now.Month.ToString("D2") + '/' + DateTime.Now.Year.ToString())) : DateTime.Parse(ddlDiaVecto.SelectedValue + "/01/" + PreAuxili.proximoAnoMat<string>(ddlAno.SelectedValue));
                    if (DateTime.Now.Year == PreAuxili.proximoAnoMat<int>(ddlAno.SelectedValue) && dataSelec < DateTime.Now)
                    {
                        if (varSer.QT_DIAS_PARC1 != null)
                        {
                            if (varSer.QT_DIAS_PARC1 > 0)
                            {
                                dataSelec = DateTime.Now.AddDays((double)varSer.QT_DIAS_PARC1);
                            }
                        }
                        else
                            dataSelec = DateTime.Now;
                    }
                    else
                    {
                        if (varSer.QT_DIAS_PARC1 != null)
                        {
                            if (varSer.QT_DIAS_PARC1 > 0)
                            {
                                dataSelec = dataSelec.AddDays((double)varSer.QT_DIAS_PARC1);
                            }
                        }
                    }
                }

                if (int.Parse(txtQtdeParcelas.Text) > varSer.NU_QUANT_MESES)
                {
                    return;
                }

                //DateTime dataSelec = DateTime.Parse( ddlDiaVecto.SelectedValue + "/01/" + txtAno.Text);  
                int parcelas = 0;

                //--------> Valida se o usuário escolheu calcular o valor do contrato e selecionou a opção "Todos (Todos os meses)"
                //if (chkValorContratoCalc.Checked && ddlValorContratoCalc.SelectedValue == "T" && !String.IsNullOrEmpty(varSer.NU_QUANT_MESES.ToString())
                //    && )
                //{
                //    parcelas = varSer.NU_QUANT_MESES;
                //}
                //else
                //{
                //    parcelas = qtdParcCur > 0 && qtdParcCur == 12 && (!chkGeraTotalParce.Checked) ? qtdParcCur - dataSelec.Month + 1 : qtdParcCur;
                //}
                if (ddlValorContratoCalc.SelectedValue == "P")
                {
                    parcelas = qtdParcCur > 0 && qtdParcCur == 12 && (!chkGeraTotalParce.Checked) ? qtdParcCur - dataSelec.Month + 1 : qtdParcCur;
                }
                else
                {
                    parcelas = 12;
                }

                int anoDesctoIni = txtPeriodoIniDesconto.Text != "" ? DateTime.Parse(txtPeriodoIniDesconto.Text).Year : dataSelec.Year;
                int anoDesctoFim = txtPeriodoFimDesconto.Text != "" ? DateTime.Parse(txtPeriodoFimDesconto.Text).Year : 0;
                int mesIniDescto = txtPeriodoIniDesconto.Text != "" ? DateTime.Parse(txtPeriodoIniDesconto.Text).Month : 0;
                int mesFimDescto = txtPeriodoFimDesconto.Text != "" ? DateTime.Parse(txtPeriodoFimDesconto.Text).Month : 0;
                int mesSelecionado = dataSelec.Month;
                decimal desEspec = txtDesctoMensa.Text != "" ? (decimal)decimal.Parse(txtDesctoMensa.Text) : 0;
                decimal valorLiqui = 0;
                decimal valorPrimParce = txtValorPrimParce.Text != "" ? Decimal.Parse(txtValorPrimParce.Text) : 0;
                int countDesconto = 1; // Contador para o desconto especial                

                if (ddlTipoContrato.SelectedValue == "V")
                {
                    parcelas = 1;
                }

                //Verifica se a quantidade de parcelas do desconto especial é maior que a qtde de parcelas geradas
                if (txtDesctoMensa.Text != "")
                {
                    DateTime dataTeste = dataSelec;
                    int contParcTeste = 1;
                    for (int i = 2; i <= parcelas; i++)
                    {
                        contParcTeste = i;
                        if (i == 2)
                        {
                            dataTeste = qtdDiasInterMeses > 0 ? dataTeste.AddDays((double)(qtdDiasInterMeses + 1)) : dataTeste.AddMonths(i - 1);
                        }
                        else
                            dataTeste = qtdDiasInterMeses > 0 ? dataTeste.AddDays((double)(qtdDiasInterMeses)) : dataTeste.AddMonths(1);

                        if (qtdDiasInterMeses == 0)
                        {
                            dataTeste = DateTime.Parse(((diaVenctoUnid != 0) ? (diaVenctoUnid > 27 && dataTeste.Month == 2 ? "27" : diaVenctoUnid.ToString("00")) : "05") + "/" + dataTeste.Month.ToString("00") + "/" + dataTeste.Year.ToString());
                        }

                        //-----> Caso o usuário não tenha escolhido a quantidade de parcelas e o ano da parcela for maior que o 
                        if ((!chkGeraTotalParce.Checked) && (dataTeste.Year > dataSelec.Year) && (ddlValorContratoCalc.SelectedValue != "T"))
                        {
                            i = parcelas;
                        }
                    }

                    if (ddlTipoDesctoMensa.SelectedValue == "M")
                    {
                        if (int.Parse(txtQtdeMesesDesctoMensa.Text) > (parcelas == contParcTeste ? parcelas : (contParcTeste - 1)))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Qtde meses de desconto especial inconsistente.");
                            return;
                        }

                        if (mesDescto > (parcelas == contParcTeste ? parcelas : (contParcTeste - 1)))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Mês de início de desconto (MID) inválido.");
                            return;
                        }

                        if ((mesDescto + int.Parse(txtQtdeMesesDesctoMensa.Text) - 1) > (parcelas == contParcTeste ? parcelas : (contParcTeste - 1)))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Qtde de mes de desconto especial combinado ao mês de início de desconto estão inconsistentes.");
                            return;
                        }
                    }
                    else
                    {
                        qtdeMesesDescto = (parcelas == contParcTeste ? parcelas : (contParcTeste - 1));
                        desEspec = desEspec / (parcelas == contParcTeste ? parcelas : (contParcTeste - 1));
                    }
                }

                decimal valorDemaisParc = 0;
                if (valorPrimParce != 0 && chkDataPrimeiraParcela.Checked && parcelas > 1)
                {
                    valorDemaisParc = Decimal.Parse(((menCur - valorPrimParce) / (parcelas - 1)).ToString("N2"));
                    menCur = valorPrimParce;
                }
                else
                {
                    //------------> Divide o valor total do curso pela quantidade de parcela do curso, encontrando o valor mensal
                    menCur = Decimal.Parse((menCur / parcelas).ToString("N2"));
                }

                //------------> Verifica se o valor do desconto especial é maior que o valor da parcela, se for o caso, o sistema apresenta uma mensagem de erro.
                if (desEspec > menCur)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de desconto especial inconsistente.");
                    return;
                }

                if (txtValorDescto.Text != "")
                {
                    desCur = chkManterDescontoPerc.Checked ? ((menCur * decimal.Parse(txtValorDescto.Text)) / 100) : decimal.Parse(txtValorDescto.Text);
                }
                else
                    desCur = 0;

                if (chkDataPrimeiraParcela.Checked == false)
                {
                    txtValorPrimParce.Text = menCur.ToString("N2");
                }

                grdNegociacao.DataKeyNames = new string[] { "CO_EMP", "NU_DOC", "NU_PAR", "DT_CAD_DOC" };

                DataTable Dt = new DataTable();

                Dt.Columns.Add("CO_EMP");

                Dt.Columns.Add("NU_DOC");

                Dt.Columns.Add("NU_PAR");

                Dt.Columns.Add("DT_CAD_DOC");

                Dt.Columns.Add("dtVencimento");

                Dt.Columns.Add("valorParcela");

                Dt.Columns.Add("valorBolsa");

                Dt.Columns.Add("valorDescto");

                Dt.Columns.Add("valorLiquido");

                Dt.Columns.Add("valorMulta");

                Dt.Columns.Add("valorJuros");

                #region Adicionar taxa de matrícula

                ///Verifica se a primeira mensalidade é a taxa de pré-matrícula
                //if (!varSer.FL_MENS_TAXA_MATR)
                if (varSer.FL_VALCON_TXMAT != "S")
                {
                    AuxiliPagina.EnvioMensagemSucesso(this.Page, "Não existe taxa de matrícula na série/curso escolhida.");
                    chkTaxaMatricula.Checked = chkTaxaMatricula.Enabled = false;
                }
                else
                {
                    //chkTaxaMatricula.Checked = chkTaxaMatricula.Enabled = true;

                    //Verifica se está marcada a opção de gerar com a taxa de matrícula
                    if (chkTaxaMatricula.Checked == true)
                    {
                        ///Verifica se a primeira mensalidade é a taxa de matrícula e se foi informada a taxa de matrícula
                        if (!varSer.FL_MENS_TAXA_MATR && varSer.FL_VALCON_TXMAT == "S")
                        {
                            //if (turnoTurma == tipoTurma[tipoTurnoTurma.M])
                            //    menCurPre = varSer.VL_TXMAT_MAN ?? decimal.Zero;
                            //else if (turnoTurma == tipoTurma[tipoTurnoTurma.V])
                            //    menCurPre = varSer.VL_TXMAT_TAR ?? decimal.Zero;
                            //else if (turnoTurma == tipoTurma[tipoTurnoTurma.N])
                            //    menCurPre = varSer.VL_TXMAT_NOI ?? decimal.Zero;
                            //else if (turnoTurma == tipoTurma[tipoTurnoTurma.I])
                            //    menCurPre = varSer.VL_TXMAT_INT ?? decimal.Zero;
                            //else if (turnoTurma == tipoTurma[tipoTurnoTurma.E])
                            //    menCurPre = varSer.VL_TXMAT_ESP ?? decimal.Zero;

                            menCurPre = (txtVlTxMatricula.Text != "" ? Decimal.Parse(txtVlTxMatricula.Text) : 0);
                            if (menCurPre > 0)
                            {
                                Dt.Rows.Add(coEmp,
                                    "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + "00", "00",
                                    DateTime.Now.ToString("dd/MM/yyyy"),
                                    DateTime.Now.ToString("dd/MM/yyyy"),
                                    menCurPre.ToString("N2"),
                                    decimal.Zero.ToString("N2"),
                                    decimal.Zero.ToString("N2"),
                                    menCurPre.ToString("N2"),
                                    decimal.Zero.ToString("N2"),
                                    decimal.Zero.ToString("N3")
                                    );
                            }
                            else
                            {
                                ///Verifica se a taxa de pré-matrícula é igual ou menos a zero
                                if (menCurPre <= 0)
                                    AuxiliPagina.EnvioMengaemConfirmacao(this.Page, "Valor da taxa de matrícula não é superior a zero.");
                            }
                        }
                    }
                }

                #endregion

                var numPar = chkGeraTotalParce.Checked && !String.IsNullOrEmpty(txtNPI.Text) ? int.Parse(txtNPI.Text) : 1;

                if ((anoDesctoFim != 0) && (anoDesctoFim >= dataSelec.Year))
                {
                    if ((anoDesctoIni < dataSelec.Year) && (anoDesctoFim > dataSelec.Year))
                    {
                        valorLiqui = menCur - desCur;
                        if (valorLiqui < 0)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                            grdNegociacao.DataSource = null;
                            grdNegociacao.DataBind();
                            return;
                        }
                        totalMensa = totalMensa + menCur;
                        totalDescto = totalDescto + desCur;
                        totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                        if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                            grdNegociacao.DataSource = null;
                            grdNegociacao.DataBind();
                            return;
                        }
                        valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                        totalValorLiqui = totalValorLiqui + valorLiqui;
                        Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + numPar.ToString("D2"), numPar.ToString("D2"), dataSelec.ToString("dd/MM/yyyy"),
                            dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                            ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                            valorLiqui.ToString("N2"),
                            multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                    }
                    else if ((anoDesctoIni < dataSelec.Year) && (anoDesctoFim == dataSelec.Year))
                    {
                        if (dataSelec.Month <= mesFimDescto)
                        {
                            valorLiqui = menCur - desCur;
                            if (valorLiqui < 0)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            totalMensa = totalMensa + menCur;
                            totalDescto = totalDescto + desCur;
                            totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                            if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                            totalValorLiqui = totalValorLiqui + valorLiqui;
                            Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + numPar.ToString("D2"), numPar.ToString("D2"), dataSelec.ToString("dd/MM/yyyy"),
                                dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                                ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                valorLiqui.ToString("N2"),
                                multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                        }
                        else
                        {
                            desCur = 0;
                            valorLiqui = menCur - desCur;
                            if (valorLiqui < 0)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            totalMensa = totalMensa + menCur;
                            totalDescto = totalDescto + desCur;
                            totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                            if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                            totalValorLiqui = totalValorLiqui + valorLiqui;
                            Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + numPar.ToString("D2"), numPar.ToString("D2"), dataSelec.ToString("dd/MM/yyyy"),
                                dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                                ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                valorLiqui.ToString("N2"),
                                multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                        }
                    }
                    else if ((anoDesctoIni == dataSelec.Year) && (anoDesctoFim > dataSelec.Year))
                    {
                        if (dataSelec.Month >= mesIniDescto)
                        {
                            valorLiqui = menCur - desCur;
                            if (valorLiqui < 0)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            totalMensa = totalMensa + menCur;
                            totalDescto = totalDescto + desCur;
                            totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                            if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                            totalValorLiqui = totalValorLiqui + valorLiqui;
                            Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + numPar.ToString("D2"), numPar.ToString("D2"), dataSelec.ToString("dd/MM/yyyy"),
                                dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                                ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                valorLiqui.ToString("N2"),
                                multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                        }
                        else
                        {
                            desCur = 0;
                            valorLiqui = menCur - desCur;
                            if (valorLiqui < 0)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            totalMensa = totalMensa + menCur;
                            totalDescto = totalDescto + desCur;
                            totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                            if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                            totalValorLiqui = totalValorLiqui + valorLiqui;
                            Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + numPar.ToString("D2"), numPar.ToString("D2"), dataSelec.ToString("dd/MM/yyyy"),
                                dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                                ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                valorLiqui.ToString("N2"),
                                multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                        }
                    }
                    else if ((anoDesctoIni == dataSelec.Year) && (anoDesctoFim == dataSelec.Year))
                    {
                        if ((dataSelec.Month >= mesIniDescto) && (dataSelec.Month <= mesFimDescto))
                        {
                            valorLiqui = menCur - desCur;
                            if (valorLiqui < 0)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            totalMensa = totalMensa + menCur;
                            totalDescto = totalDescto + desCur;
                            totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                            if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                            totalValorLiqui = totalValorLiqui + valorLiqui;
                            Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + numPar.ToString("D2"), numPar.ToString("D2"), dataSelec.ToString("dd/MM/yyyy"),
                                dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                                ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                valorLiqui.ToString("N2"),
                                multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                        }
                        else
                        {
                            desCur = 0;
                            valorLiqui = menCur - desCur;
                            if (valorLiqui < 0)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            totalMensa = totalMensa + menCur;
                            totalDescto = totalDescto + desCur;
                            totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                            if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                            totalValorLiqui = totalValorLiqui + valorLiqui;
                            Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + numPar.ToString("D2"), numPar.ToString("D2"), dataSelec.ToString("dd/MM/yyyy"),
                                dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                                ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                valorLiqui.ToString("N2"),
                                multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                        }
                    }
                }
                else
                {
                    desCur = 0;
                    valorLiqui = menCur - desCur;
                    if (valorLiqui < 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                        grdNegociacao.DataSource = null;
                        grdNegociacao.DataBind();
                        return;
                    }
                    totalMensa = totalMensa + menCur;
                    totalDescto = totalDescto + desCur;
                    totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                    if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                        grdNegociacao.DataSource = null;
                        grdNegociacao.DataBind();
                        return;
                    }
                    valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                    totalValorLiqui = totalValorLiqui + valorLiqui;
                    Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + numPar.ToString("D2"), numPar.ToString("D2"), dataSelec.ToString("dd/MM/yyyy"),
                        dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                        ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                        valorLiqui.ToString("N2"),
                        multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                }

                if (valorPrimParce != 0 && chkDataPrimeiraParcela.Checked)
                {
                    menCur = valorDemaisParc;
                }
                DateTime dataVectoMensa = dataSelec;
                DateTime dtInicDescto, dtFimDescto = DateTime.Now;
                int mesDescontoFim = 0;
                qtdeMesesDescto = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0) > 0 ? qtdeMesesDescto - 1 : qtdeMesesDescto;
                if (parcelas > 1)
                {
                    decimal t = 0;
                    if (valorPrimParce != 0 && chkDataPrimeiraParcela.Checked)
                    {
                        t = valorPrimParce;
                    }
                    else
                    {
                        t = 0;
                    }

                    numPar++;
                    
                    for (int i = numPar; i < (parcelas + (numPar - 1)); i++)
                    {
                        t = t + menCur;

                        if (i == parcelas)
                        {
                            decimal tt = 0;
                            if (valorPrimParce == 0 || !chkDataPrimeiraParcela.Checked)
                            {
                                tt = menCur * parcelas;
                            }
                            else
                            {
                                tt = (menCur * (parcelas - 1)) + valorPrimParce;
                            }

                            if (tt > decimal.Parse(txtValorContratoCalc.Text))
                            {
                                decimal d = tt - decimal.Parse(txtValorContratoCalc.Text);
                                menCur = menCur - d;
                            }
                            else
                            {
                                if (tt < decimal.Parse(txtValorContratoCalc.Text))
                                {
                                    decimal d = decimal.Parse(txtValorContratoCalc.Text) - tt;
                                    menCur = menCur + d;
                                }
                            }
                        }

                        mesSelecionado++;
                        // Determina o Mês fim para o desconto
                        mesDescontoFim = mesDescto + (qtdeMesesDescto - 1);
                        if (mesDescontoFim > 12)
                        {
                            mesDescontoFim = mesDescontoFim - 12;
                        }

                        if (dataVectoMensa.Month >= mesDescto && dataVectoMensa.Year == DateTime.Now.Year)
                        {
                            countDesconto++;
                        }
                        else
                        {
                            if (dataVectoMensa.Year > DateTime.Now.Year && dataVectoMensa.Month >= mesDescontoFim)
                            {
                                countDesconto++;
                            }
                        }

                        if (i == numPar)
                        {
                            dataVectoMensa = qtdDiasInterMeses > 0 ? dataVectoMensa.AddDays((double)(qtdDiasInterMeses + 1)) : dataVectoMensa.AddMonths(i - 1);
                        }
                        else
                            dataVectoMensa = qtdDiasInterMeses > 0 ? dataVectoMensa.AddDays((double)(qtdDiasInterMeses)) : dataVectoMensa.AddMonths(1);

                        if (qtdDiasInterMeses == 0)
                        {
                            dataVectoMensa = DateTime.Parse(((diaVenctoUnid != 0) ? (diaVenctoUnid > 27 && dataVectoMensa.Month == 2 ? "27" : diaVenctoUnid.ToString("00")) : "05") + "/" + dataVectoMensa.Month.ToString("00") + "/" + dataVectoMensa.Year.ToString());
                        }

                        //-----> Caso o usuário não tenha escolhido a quantidade de parcelas e o ano da parcela for maior que o 
                        if ((!chkGeraTotalParce.Checked) && (dataVectoMensa.Year > dataSelec.Year) && (ddlValorContratoCalc.SelectedValue != "T"))
                        {
                            i = parcelas;
                        }
                        else
                        {
                            if (i == numPar)
                            {
                                desCur = 0;
                                if (DateTime.TryParse(txtPeriodoIniDesconto.Text, out dtInicDescto) && DateTime.TryParse(txtPeriodoFimDesconto.Text, out dtFimDescto))
                                {
                                    if ((dtInicDescto <= dataVectoMensa) && (dtFimDescto >= dataVectoMensa))
                                    {
                                        if (txtValorDescto.Text != "")
                                        {
                                            desCur = chkManterDescontoPerc.Checked ? ((menCur * decimal.Parse(txtValorDescto.Text)) / 100) : decimal.Parse(txtValorDescto.Text);
                                        }
                                        else
                                            desCur = 0;
                                        valorLiqui = menCur - desCur;
                                        if (valorLiqui < 0)
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        totalMensa = totalMensa + menCur;
                                        totalDescto = totalDescto + desCur;
                                        totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0);
                                        if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0))
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                                        totalValorLiqui = totalValorLiqui + valorLiqui;
                                        Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + i.ToString("D2"), i.ToString("D2"), dataSelec.ToShortDateString(),
                                            dataVectoMensa.ToShortDateString(), menCur.ToString("N2"), desCur.ToString("N2"),
                                            ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                            valorLiqui.ToString("N2"),
                                            multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                                    }
                                    else
                                    {
                                        valorLiqui = menCur - desCur;
                                        if (valorLiqui < 0)
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        totalMensa = totalMensa + menCur;
                                        totalDescto = totalDescto + desCur;
                                        totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0);
                                        if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0))
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                                        totalValorLiqui = totalValorLiqui + valorLiqui;
                                        Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + i.ToString("D2"), i.ToString("D2"), dataSelec.ToShortDateString(),
                                            dataVectoMensa.ToShortDateString(), menCur.ToString("N2"), desCur.ToString("N2"),
                                            ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                            valorLiqui.ToString("N2"),
                                            multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                                    }
                                }
                                else
                                {
                                    valorLiqui = menCur - desCur;
                                    if (valorLiqui < 0)
                                    {
                                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                        grdNegociacao.DataSource = null;
                                        grdNegociacao.DataBind();
                                        return;
                                    }
                                    totalMensa = totalMensa + menCur;
                                    totalDescto = totalDescto + desCur;
                                    totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0);
                                    if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0))
                                    {
                                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                        grdNegociacao.DataSource = null;
                                        grdNegociacao.DataBind();
                                        return;
                                    }
                                    valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                                    totalValorLiqui = totalValorLiqui + valorLiqui;
                                    Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + i.ToString("D2"), i.ToString("D2"), dataSelec.ToShortDateString(),
                                        dataVectoMensa.ToShortDateString(), menCur.ToString("N2"), desCur.ToString("N2"),
                                        ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                        valorLiqui.ToString("N2"),
                                        multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                                }
                            }
                            else
                            {
                                desCur = 0;
                                if (DateTime.TryParse(txtPeriodoIniDesconto.Text, out dtInicDescto) && DateTime.TryParse(txtPeriodoFimDesconto.Text, out dtFimDescto))
                                {
                                    if ((dtInicDescto <= dataVectoMensa) && (dtFimDescto >= dataVectoMensa))
                                    {
                                        if (txtValorDescto.Text != "")
                                        {
                                            desCur = chkManterDescontoPerc.Checked ? ((menCur * decimal.Parse(txtValorDescto.Text)) / 100) : decimal.Parse(txtValorDescto.Text);
                                        }
                                        else
                                            desCur = 0;
                                        valorLiqui = menCur - desCur;
                                        if (valorLiqui < 0)
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        totalMensa = totalMensa + menCur;
                                        totalDescto = totalDescto + desCur;
                                        totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0);
                                        if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0))
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                                        totalValorLiqui = totalValorLiqui + valorLiqui;
                                        Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + i.ToString("D2"), i.ToString("D2"), dataSelec.ToShortDateString(),
                                            dataVectoMensa.ToShortDateString(), menCur.ToString("N2"), desCur.ToString("N2"),
                                            ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                            valorLiqui.ToString("N2"),
                                            multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                                    }
                                    else
                                    {
                                        valorLiqui = menCur - desCur;
                                        if (valorLiqui < 0)
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        totalMensa = totalMensa + menCur;
                                        totalDescto = totalDescto + desCur;
                                        totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0);
                                        if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0))
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                                        totalValorLiqui = totalValorLiqui + valorLiqui;
                                        Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + i.ToString("D2"), i.ToString("D2"), dataSelec.ToShortDateString(),
                                            dataVectoMensa.ToShortDateString(), menCur.ToString("N2"), desCur.ToString("N2"),
                                            ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                            valorLiqui.ToString("N2"),
                                            multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                                    }
                                }
                                else
                                {
                                    valorLiqui = menCur - desCur;
                                    if (valorLiqui < 0)
                                    {
                                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                        grdNegociacao.DataSource = null;
                                        grdNegociacao.DataBind();
                                        return;
                                    }
                                    totalMensa = totalMensa + menCur;
                                    totalDescto = totalDescto + desCur;
                                    totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0);
                                    if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0))
                                    {
                                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                        grdNegociacao.DataSource = null;
                                        grdNegociacao.DataBind();
                                        return;
                                    }
                                    valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                                    totalValorLiqui = totalValorLiqui + valorLiqui;
                                    Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + i.ToString("D2"), i.ToString("D2"), dataSelec.ToShortDateString(),
                                        dataVectoMensa.ToShortDateString(), menCur.ToString("N2"), desCur.ToString("N2"),
                                        ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                        valorLiqui.ToString("N2"),
                                        multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                                }
                            }
                        }
                        qtdeMesesDescto = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0) > 0 ? qtdeMesesDescto - 1 : qtdeMesesDescto;
                    }
                }

                grdNegociacao.DataSource = Dt;
                grdNegociacao.DataBind();

                txtTotalMensa.Text = totalMensa.ToString("N2");
                txtTotalDesctoBolsa.Text = totalDescto.ToString("N2");
                txtTotalDesctoEspec.Text = totalDesctoEspec.ToString("N2");
                txtTotalLiquiContra.Text = totalValorLiqui.ToString("N2");
            }
        }

        #endregion

        #region Funções de campo
        protected void lnkCarregaInfo_Click(object sender, EventArgs e)
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coMod = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coCur = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int coTur = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            string coAno = ddlAno.SelectedValue;

            TB07_ALUNO alu = ddlAluno.SelectedValue != "" && ddlUnidade.SelectedValue != "" ? TB07_ALUNO.RetornaPelaChavePrimaria(int.Parse(ddlAluno.SelectedValue), int.Parse(ddlUnidade.SelectedValue)) : (TB07_ALUNO)null;

            //int contTit = TB47_CTA_RECEB.RetornaTodosRegistros().Where(w => w.CO_ALU == coAlu && w.CO_EMP == coEmp && w.CO_MODU_CUR == coMod && w.CO_CUR == coCur && w.CO_TUR == coTur && w.CO_ANO_MES_MAT == coAno).Count();

            if (0 == 0)//(contTit == 0)
            {
                CarregaValorContrato();
                CarregaDataValorPrimParc();

                #region Carrega a quantidade de parcelas do Curso
                if (!String.IsNullOrEmpty(ddlSerieCurso.SelectedValue))
                {
                    TB01_CURSO serie = TB01_CURSO.RetornaPeloCoCur(int.Parse(this.ddlSerieCurso.SelectedValue));

                    txtQtdeParcelas.Text = serie.NU_QUANT_MESES.ToString();
                }

                if (txtValorContratoCalc.Text == "")
                {
                    txtValorContratoCalc.Text = "0";
                }
                if (txtQtdeParcelas.Text == "")
                {
                    txtQtdeParcelas.Text = "0";
                }

                if (txtValorContratoCalc.Text != "0" && txtQtdeParcelas.Text != "0")
                {
                    decimal vlParc = (decimal.Parse(txtValorContratoCalc.Text) / decimal.Parse(txtQtdeParcelas.Text));
                    txtValorPrimParce.Text = Math.Round(vlParc, 2).ToString("N2");
                }
                carregaValorTaxaMatricula();
                #endregion

                divFin.Visible = true;

                if (alu != null)
                {
                    alu.TB148_TIPO_BOLSAReference.Load();
                    if (alu.TB148_TIPO_BOLSA != null && ddlBolsaAlunoAlt.Items.FindByValue(alu.TB148_TIPO_BOLSA.CO_TIPO_BOLSA.ToString()) != null)
                        ddlBolsaAlunoAlt.SelectedValue = alu.TB148_TIPO_BOLSA.CO_TIPO_BOLSA.ToString();
                }
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this, "Já existem Títulos Lançados para esta Matrícula.");
                divFin.Visible = false;
                return;
            }
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaALuno();
            CarregaBoletos();
        }

        protected void chkAtualiFinan_CheckedChanged(object sender, EventArgs e)
        {
            grdNegociacao.DataSource = null;
            grdNegociacao.DataBind();

            if (chkAtualiFinan.Checked)
            {
                ddlBoleto.Enabled = ddlTipoDesctoMensa.Enabled = txtDesctoMensa.Enabled = true;
                ddlTipoDesctoMensa.SelectedValue = "T";
                #region Habilita os campos
                ddlTipoValorCurso.Enabled =
                chkTipoContrato.Enabled =
                chkValorContratoCalc.Enabled =
                chkAlterValorContr.Enabled =
                chkGeraTotalParce.Enabled =
                chkDataPrimeiraParcela.Enabled =
                chkManterDesconto.Enabled =
                ddlTipoDesctoMensa.Enabled =
                txtDesctoMensa.Enabled =
                ddlBoleto.Enabled =
                ddlDiaVecto.Enabled =
                chkTipoContrato.Enabled = true;
                txtMesIniDesconto.Enabled = false;
                #endregion

                var admUsu = (from adUs in ADMUSUARIO.RetornaTodosRegistros()
                              where adUs.CodUsuario == LoginAuxili.CO_COL
                              select new { adUs.FLA_ALT_BOL_ALU, adUs.FLA_ALT_BOL_ESPE_ALU, adUs.FLA_ALT_PARAM_MAT }).FirstOrDefault();

                if (admUsu != null)
                {
                    //-----------> Valida se o usuário possui permissão para alterar o desconto dado ao aluno.
                    if (admUsu.FLA_ALT_BOL_ALU == "S")
                    {
                        chkManterDesconto.Enabled = true;
                    }
                    else
                    {
                        chkManterDesconto.Enabled =
                        txtValorDescto.Enabled =
                        chkManterDescontoPerc.Enabled =
                        txtPeriodoIniDesconto.Enabled =
                        txtPeriodoFimDesconto.Enabled = false;
                    }

                    //-----------> Valida se o usuário possui permissão para alterar o desconto especial dado ao aluno.
                    if (admUsu.FLA_ALT_BOL_ESPE_ALU == "S")
                    {
                        ddlTipoDesctoMensa.Enabled =
                        txtDesctoMensa.Enabled = true;
                    }
                    else
                    {
                        ddlTipoDesctoMensa.Enabled =
                        txtDesctoMensa.Enabled =
                        txtMesIniDesconto.Enabled = false;
                    }

                    //-----------> Valida se o usuário possui permissão para alterar parâmetros da matrícula
                    if (admUsu.FLA_ALT_PARAM_MAT == "S")
                    {
                        chkTipoContrato.Enabled =
                        ddlTipoValorCurso.Enabled =
                        chkGeraTotalParce.Enabled =
                        chkValorContratoCalc.Enabled =
                        chkAlterValorContr.Enabled =
                        chkDataPrimeiraParcela.Enabled = true;
                    }
                    else
                    {
                        chkTipoContrato.Enabled =
                        ddlTipoValorCurso.Enabled =
                        chkGeraTotalParce.Enabled =
                        chkValorContratoCalc.Enabled =
                        txtValorContratoCalc.Enabled =
                        chkAlterValorContr.Enabled =
                        chkDataPrimeiraParcela.Enabled = false;
                    }
                }
                else
                {
                    chkManterDesconto.Enabled =
                        ddlTpBolsaAlt.Enabled =
                        ddlBolsaAlunoAlt.Enabled =
                        txtValorDescto.Enabled =
                        chkManterDescontoPerc.Enabled =
                        txtPeriodoIniDesconto.Enabled =
                        txtPeriodoFimDesconto.Enabled = false;
                    ddlTipoDesctoMensa.Enabled =
                        txtQtdeMesesDesctoMensa.Enabled =
                        txtDesctoMensa.Enabled =
                        txtMesIniDesconto.Enabled =
                        chkTipoContrato.Enabled =
                        ddlTipoValorCurso.Enabled =
                        chkGeraTotalParce.Enabled =
                        chkValorContratoCalc.Enabled =
                        chkAlterValorContr.Enabled =
                        txtValorContratoCalc.Enabled =
                        chkDataPrimeiraParcela.Enabled = false;
                }
            }
            else
            {
                ddlBoleto.Enabled = ddlTipoDesctoMensa.Enabled = txtQtdeMesesDesctoMensa.Enabled = txtDesctoMensa.Enabled = false;
                txtQtdeMesesDesctoMensa.Text = txtDesctoMensa.Text = txtTotalMensa.Text =
                    txtTotalDesctoBolsa.Text = txtTotalDesctoEspec.Text = txtTotalLiquiContra.Text = "";
                ddlTipoDesctoMensa.SelectedValue = "T";
                #region Desabilita os campos
                ddlTipoContrato.Enabled =
                    ddlTipoValorCurso.Enabled =
                    chkTipoContrato.Enabled =
                    ddlTipoContrato.Enabled =
                    ddlTipoValorCurso.Enabled =
                    chkValorContratoCalc.Enabled =
                    txtValorContratoCalc.Enabled =
                    chkAlterValorContr.Enabled =
                    ddlValorContratoCalc.Enabled =
                    chkGeraTotalParce.Enabled =
                    txtQtdeParcelas.Enabled =
                    txtNPI.Enabled =
                    RequiredFieldValidator6.Enabled =
                    chkDataPrimeiraParcela.Enabled =
                    txtDtPrimeiraParcela.Enabled =
                    txtValorPrimParce.Enabled =
                    chkManterDesconto.Enabled =
                    ddlTpBolsaAlt.Enabled =
                    ddlBolsaAlunoAlt.Enabled =
                    txtValorDescto.Enabled =
                    chkManterDescontoPerc.Enabled =
                    txtPeriodoIniDesconto.Enabled =
                    txtPeriodoFimDesconto.Enabled =
                    ddlTipoDesctoMensa.Enabled =
                    txtQtdeMesesDesctoMensa.Enabled =
                    txtDesctoMensa.Enabled =
                    txtMesIniDesconto.Enabled =
                    ddlBoleto.Enabled =
                    ddlDiaVecto.Enabled =
                    chkTipoContrato.Enabled = false;
                #endregion
            }
        }

        protected void chkTipoContrato_CheckedChange(object sender, EventArgs e)
        {
            if (chkTipoContrato.Checked)
            {
                ddlTipoContrato.Enabled =
                    ddlTipoValorCurso.Enabled = true;
            }
            else
            {
                ddlTipoContrato.SelectedValue = "P";
                ddlTipoValorCurso.SelectedValue = tipoValor[tipoValorCurso.P];

                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);
                string turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;
                txtQtdeParcelas.Text = varSer.NU_QUANT_MESES.ToString();
                string retornoValor = PreAuxili.valorContratoCurso(ddlTipoValorCurso.SelectedValue,
                    ddlTipoContrato.SelectedValue,
                    turnoTurma,
                    varSer,
                    this.Page, false);
                if (retornoValor == string.Empty)
                    return;
                else
                    txtValorContratoCalc.Text = retornoValor;

                ddlTipoContrato.Enabled =
                    ddlTipoValorCurso.Enabled = false;
            }
        }

        protected void ddlTipoValorCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int unidade = (ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP);
            var varSer = TB01_CURSO.RetornaPelaChavePrimaria(unidade, modalidade, serie);
            string turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;
            string retornoValor = PreAuxili.valorContratoCurso(
                ((DropDownList)sender).SelectedValue,
                ddlTipoContrato.SelectedValue,
                turnoTurma,
                varSer,
                this.Page, false);
            if (retornoValor == string.Empty)
                return;
            else
                txtValorContratoCalc.Text = retornoValor;
        }

        protected void chkAlterValorContr_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAlterValorContr.Checked)
            {
                txtValorContratoCalc.Enabled = true;
            }
            else
            {
                txtValorContratoCalc.Enabled = false;
                txtValorContratoCalc.Text = "";

                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);
                string turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;

                //-----------> Valida o turno do curso e coloca o valor do contrato no campo de valor do contrato de acordo com o turno da turma.
                switch (turnoTurma)
                {
                    case "M":
                        //--------> Turma Matutina
                        #region Turno Matutino
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            case "P":
                                if (varSer.VL_CONTMAN_APRAZ == null)
                                {
                                    txtValorContratoCalc.Text = "";
                                    return;
                                }
                                //---------> A Prazo
                                txtValorContratoCalc.Text = varSer.VL_CONTMAN_APRAZ.ToString();
                                break;
                            case "V":
                                if (varSer.VL_CONTMAN_AVIST == null)
                                {
                                    txtValorContratoCalc.Text = "";
                                    return;
                                }
                                //---------> A Vista
                                txtValorContratoCalc.Text = varSer.VL_CONTMAN_AVIST.ToString();
                                break;
                        }
                        break;
                        #endregion
                    case "V":
                        //--------> Turma Vespertina
                        #region Turno Vespertino
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            case "P":
                                if (varSer.VL_CONTTAR_APRAZ == null)
                                {
                                    txtValorContratoCalc.Text = "";
                                    return;
                                }
                                //---------> A Prazo
                                txtValorContratoCalc.Text = varSer.VL_CONTTAR_APRAZ.ToString();
                                break;
                            case "V":
                                if (varSer.VL_CONTTAR_AVIST == null)
                                {
                                    txtValorContratoCalc.Text = "";
                                    return;
                                }
                                //---------> A Vista
                                txtValorContratoCalc.Text = varSer.VL_CONTTAR_AVIST.ToString();
                                break;
                        }
                        break;
                        #endregion
                    case "N":
                        //--------> Turma Noturna
                        #region Turno Noturno
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            case "P":
                                if (varSer.VL_CONTNOI_APRAZ == null)
                                {
                                    txtValorContratoCalc.Text = "";
                                    return;
                                }
                                //---------> A Prazo
                                txtValorContratoCalc.Text = varSer.VL_CONTNOI_APRAZ.ToString();
                                break;
                            case "V":
                                if (varSer.VL_CONTNOI_AVIST == null)
                                {
                                    txtValorContratoCalc.Text = "";
                                    return;
                                }
                                //---------> A Vista
                                txtValorContratoCalc.Text = varSer.VL_CONTNOI_AVIST.ToString();
                                break;
                        }
                        break;
                        #endregion
                }
            }
        }

        protected void chkValorContratoCalc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkValorContratoCalc.Checked)
            {
                ddlValorContratoCalc.Enabled = true;
            }
            else
            {
                ddlValorContratoCalc.Enabled = false;
            }
        }

        protected void chkGeraTotalParce_CheckedChanged(object sender, EventArgs e)
        {
            TB01_CURSO serie = TB01_CURSO.RetornaPeloCoCur(int.Parse(this.ddlSerieCurso.SelectedValue));

            txtQtdeParcelas.Text = serie.NU_QUANT_MESES.ToString();
            txtNPI.Text = "1";
            txtQtdeParcelas.Enabled =
            txtNPI.Enabled = chkGeraTotalParce.Checked;
        }

        protected void txtQtdeParcelas_TextChanged(object sender, EventArgs e)
        {
            //-----------> Pega o Curso
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);

            if (ddlTipoContrato.SelectedValue == "P")
            {
                if (txtQtdeParcelas.Text != "")
                {
                    if (int.Parse(txtQtdeParcelas.Text) <= 1)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "A quantidade de parcelas a prazo deve ser maior que 1.");
                        return;
                    }

                    if (int.Parse(txtQtdeParcelas.Text) > varSer.NU_QUANT_MESES)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "A quantidade de parcelas a prazo não pode ser maior que quantidade de parcelas do contrato.");
                        return;
                    }
                }
            }
        }

        protected void chkDataPrimeiraParcela_CheckedChange(object sender, EventArgs e)
        {
            if (chkDataPrimeiraParcela.Checked)
            {
                txtDtPrimeiraParcela.Enabled = txtValorPrimParce.Enabled = true;
            }
            else
            {
                txtDtPrimeiraParcela.Enabled = txtValorPrimParce.Enabled = false;
            }
        }

        protected void chkManterDesconto_CheckedChanged(object sender, EventArgs e)
        {

            if (chkManterDesconto.Checked)
            {
                // Habilita os campos de alteração do desconto do aluno
                ddlTpBolsaAlt.Enabled =
                ddlBolsaAlunoAlt.Enabled =
                txtPeriodoIniDesconto.Enabled =
                txtPeriodoFimDesconto.Enabled = true;

                //// Garante que o tipo de desconto, da alteração, seja igual ao tipo de desconto do cadastro de aluno
                //ddlTpBolsaAlt.SelectedValue = ddlTipoBolsa.SelectedValue;

                //CarregaBolsasAlt();

                //ddlBolsaAlunoAlt.SelectedValue = ddlBolsaAluno.SelectedValue;

                //txtPeriodoIniDesconto.Text = txtPeriodoDeIniBolAluno.Text;
                //txtPeriodoFimDesconto.Text = txtPeriodoTerBolAluno.Text;

                //txtValorDescto.Text = txtDescontoAluno.Text;
                //chkManterDescontoPerc.Checked = chkDesctoPercBolsa.Checked;

                #region Carrega a bolsa cadastrada para o aluno
                //int coBolsa = int.Parse(ddlBolsaAluno.SelectedValue);
                //var tb148 = (from iTb148 in TB148_TIPO_BOLSA.RetornaTodosRegistros()
                //             where iTb148.CO_TIPO_BOLSA == coBolsa
                //             select new { iTb148.VL_TIPO_BOLSA, iTb148.FL_TIPO_VALOR_BOLSA, iTb148.DT_INICI_TIPO_BOLSA, iTb148.DT_FIM_TIPO_BOLSA }).FirstOrDefault();

                //if (tb148 != null)
                //{
                //    txtValorDescto.Text = tb148.VL_TIPO_BOLSA != null ? String.Format("{0:N}", tb148.VL_TIPO_BOLSA) : "";
                //    chkManterDescontoPerc.Checked = tb148.FL_TIPO_VALOR_BOLSA == "P";

                //    if (tb148.DT_INICI_TIPO_BOLSA != null)
                //    {
                //        txtPeriodoIniDesconto.Text = tb148.DT_INICI_TIPO_BOLSA.Value.ToString("dd/MM/yyyy");
                //        txtPeriodoFimDesconto.Text = tb148.DT_FIM_TIPO_BOLSA.Value.ToString("dd/MM/yyyy");
                //    }
                //    else
                //    {
                //        txtPeriodoIniDesconto.Text = "";
                //        txtPeriodoFimDesconto.Text = "";
                //    }
                //}
                #endregion

            }
            else
            {
                // Habilita os campos de alteração do desconto do aluno
                ddlTpBolsaAlt.Enabled =
                ddlBolsaAlunoAlt.Enabled =
                txtPeriodoIniDesconto.Enabled =
                txtValorDescto.Enabled =
                chkManterDescontoPerc.Enabled =
                txtPeriodoFimDesconto.Enabled = false;

                //// Garante que o tipo de desconto, da alteração, seja igual ao tipo de desconto do cadastro de aluno
                //ddlTpBolsaAlt.SelectedValue = ddlTipoBolsa.SelectedValue;

                //CarregaBolsasAlt();

                //ddlBolsaAlunoAlt.SelectedValue = ddlBolsaAluno.SelectedValue;

                //txtPeriodoIniDesconto.Text = txtPeriodoDeIniBolAluno.Text;
                //txtPeriodoFimDesconto.Text = txtPeriodoTerBolAluno.Text;

                //txtValorDescto.Text = txtDescontoAluno.Text;
                //chkManterDescontoPerc.Checked = chkDesctoPercBolsa.Checked;

                #region Carrega a bolsa cadastrada para o aluno
                //int coBolsa = int.Parse(ddlBolsaAluno.SelectedValue);
                //var tb148 = (from iTb148 in TB148_TIPO_BOLSA.RetornaTodosRegistros()
                //             where iTb148.CO_TIPO_BOLSA == coBolsa
                //             select new { iTb148.VL_TIPO_BOLSA, iTb148.FL_TIPO_VALOR_BOLSA, iTb148.DT_INICI_TIPO_BOLSA, iTb148.DT_FIM_TIPO_BOLSA }).FirstOrDefault();

                //if (tb148 != null)
                //{
                //    txtValorDescto.Text = tb148.VL_TIPO_BOLSA != null ? String.Format("{0:N}", tb148.VL_TIPO_BOLSA) : "";
                //    chkManterDescontoPerc.Checked = tb148.FL_TIPO_VALOR_BOLSA == "P";

                //    if (tb148.DT_INICI_TIPO_BOLSA != null)
                //    {
                //        txtPeriodoIniDesconto.Text = tb148.DT_INICI_TIPO_BOLSA.Value.ToString("dd/MM/yyyy");
                //        txtPeriodoFimDesconto.Text = tb148.DT_FIM_TIPO_BOLSA.Value.ToString("dd/MM/yyyy");
                //    }
                //    else
                //    {
                //        txtPeriodoIniDesconto.Text = "";
                //        txtPeriodoFimDesconto.Text = "";
                //    }
                //}
                #endregion
            }

        }

        protected void ddlTpBolsaAlt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTpBolsaAlt.SelectedValue == "")
            {
                txtValorDescto.Text = "";
                txtPeriodoFimDesconto.Text = "";
                txtPeriodoFimDesconto.Text = "";

                txtValorDescto.Enabled =
                txtPeriodoFimDesconto.Enabled =
                ddlBolsaAlunoAlt.Enabled =
                txtPeriodoFimDesconto.Enabled = false;

                CarregaBolsasAlt();
            }
            else
            {
                CarregaBolsasAlt();
            }
        }

        protected void ddlBolsaAlunoAlt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBolsaAlunoAlt.SelectedValue == "")
            {
                txtValorDescto.Text = txtPeriodoIniDesconto.Text = txtPeriodoFimDesconto.Text = "";
                txtValorDescto.Enabled = chkManterDescontoPerc.Checked = chkManterDescontoPerc.Enabled = txtPeriodoIniDesconto.Enabled = txtPeriodoFimDesconto.Enabled = false;
            }
            else
            {
                if (ddlBolsaAlunoAlt.SelectedValue == "0")
                {
                    txtValorDescto.Enabled =
                    chkManterDescontoPerc.Enabled =
                    txtPeriodoIniDesconto.Enabled =
                    txtPeriodoFimDesconto.Enabled = true;

                    txtValorDescto.Text = "";
                    txtPeriodoIniDesconto.Text = "";
                    txtPeriodoFimDesconto.Text = "";
                    chkManterDescontoPerc.Checked = true;
                }
                else
                {
                    txtPeriodoIniDesconto.Enabled = txtPeriodoFimDesconto.Enabled = true;
                    txtValorDescto.Enabled = chkManterDescontoPerc.Enabled = false;
                    int coBolsa = int.Parse(ddlBolsaAlunoAlt.SelectedValue);

                    var tb148 = (from iTb148 in TB148_TIPO_BOLSA.RetornaTodosRegistros()
                                 where iTb148.CO_TIPO_BOLSA == coBolsa
                                 select new { iTb148.VL_TIPO_BOLSA, iTb148.FL_TIPO_VALOR_BOLSA, iTb148.DT_INICI_TIPO_BOLSA, iTb148.DT_FIM_TIPO_BOLSA }).FirstOrDefault();

                    if (tb148 != null)
                    {
                        txtValorDescto.Text = tb148.VL_TIPO_BOLSA != null ? String.Format("{0:N}", tb148.VL_TIPO_BOLSA) : "";
                        chkManterDescontoPerc.Checked = tb148.FL_TIPO_VALOR_BOLSA == "P";

                        if (tb148.DT_INICI_TIPO_BOLSA != null)
                        {
                            txtPeriodoIniDesconto.Text = tb148.DT_INICI_TIPO_BOLSA.Value.ToString("dd/MM/yyyy");
                            txtPeriodoFimDesconto.Text = tb148.DT_FIM_TIPO_BOLSA.Value.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            txtPeriodoIniDesconto.Text = "";
                            txtPeriodoFimDesconto.Text = "";
                        }
                    }
                }
            }
        }

        protected void ddlTipoDesctoMensa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoDesctoMensa.SelectedValue == "M")
            {
                txtQtdeMesesDesctoMensa.Enabled = true;
                txtMesIniDesconto.Enabled = true;
            }
            else
            {
                txtQtdeMesesDesctoMensa.Enabled = false;
                txtQtdeMesesDesctoMensa.Text = "";
                txtMesIniDesconto.Enabled = false;
                txtMesIniDesconto.Text = "";
            }
        }

        protected void lnkMontaGridMensa_Click(object sender, EventArgs e)
        {
            int parcelas = 12; //- DateTime.Now.Month + 1;

            if (ddlSerieCurso.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar grid de mensalidade porque série não foi selecionada.");
                return;
            }

            if (ddlAluno.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar grid de mensalidade porque o Aluno não foi selecionado.");
                return;
            }

            if (!chkAtualiFinan.Checked)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar grid de mensalidade porque não será atualizado no financeiro.");
                return;
            }

            if (chkDataPrimeiraParcela.Checked)
            {
                if (txtValorPrimParce.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Valor da primeira parcela deve ser informado.");
                    return;
                }
                else
                {
                    if (Decimal.Parse(txtValorPrimParce.Text) == 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Valor 1ª Parcela não pode ser zero ou negativo.");
                        return;
                    }
                }
            }

            if (chkAlterValorContr.Checked)
            {
                if (txtValorContratoCalc.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Valor de Contrato deve ser informado.");
                    return;
                }
                else
                {
                    if (Decimal.Parse(txtValorContratoCalc.Text) == 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Valor Editado de Contrato não pode ser zero ou negativo.");
                        return;
                    }
                }
            }

            if (chkManterDesconto.Checked)
            {
                if (txtPeriodoIniDesconto.Text != "" && txtPeriodoFimDesconto.Text != "")
                {
                    DateTime validaData;
                    if (DateTime.TryParse(txtPeriodoIniDesconto.Text, out validaData) && DateTime.TryParse(txtPeriodoFimDesconto.Text, out validaData))
                    {
                        if (DateTime.Parse(txtPeriodoIniDesconto.Text) > DateTime.Parse(txtPeriodoFimDesconto.Text))
                        {
                            AuxiliPagina.EnvioMensagemErro(this, "Data de fim de período inválida");
                            return;
                        }
                    }
                }
            }

            if (txtDesctoMensa.Text != "")
            {
                if (Decimal.Parse(txtDesctoMensa.Text) == 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Valor de Desconto Especial não pode ser zero ou negativo.");
                    return;
                }
            }

            if ((ddlTipoDesctoMensa.SelectedValue == "M") && (txtDesctoMensa.Text != ""))
            {
                if (txtQtdeMesesDesctoMensa.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Quantidade de meses de desconto especial deve ser informada.");
                    return;
                }

                if (txtMesIniDesconto.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Número da Parcela de início de desconto deve ser informada.");
                    return;
                }

                if (int.Parse(txtQtdeMesesDesctoMensa.Text) == 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar grid de mensalidade porque mês informado igual a zero.");
                    return;
                }

                if (int.Parse(txtMesIniDesconto.Text) == 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar grid de mensalidade porque número da parcela de início informado igual a zero.");
                    return;
                }
            }

            if (chkGeraTotalParce.Checked && String.IsNullOrEmpty(txtNPI.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Número da Parcela Inicial deve ser informado.");
                return;
            }
            else if (chkGeraTotalParce.Checked && txtNPI.Text == "0")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Número da Parcela Inicial não pode ser zero.");
                return;
            }

            MontaGridNegociacao();
        }

        protected void lnkMenAlu_Click(object sender, EventArgs e)
        {
            if (chkAtualiFinan.Checked)
            {
                if (grdNegociacao.Rows.Count == 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "É necessário criar a grid de mensalidades.");
                    return;
                }
            }

            //lnkSucMenEscAlu.Visible = true;
            //chkMenEscAlu.Enabled = false;

            //AuxiliPagina.EnvioMensagemSucesso(this, "Mensalidade(s) verificada(s) com sucesso.");

            chkAtualiFinan.Enabled = ddlBoleto.Enabled = ddlTipoDesctoMensa.Enabled =
                txtQtdeMesesDesctoMensa.Enabled = txtDesctoMensa.Enabled = lnkMontaGridMensa.Enabled = false;

            #region Altera o desconto do aluno
            decimal decimalRetorno;
            DateTime dataRetorno;

            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
            if (tb07 != null)
            {
                tb07.TB148_TIPO_BOLSA = this.ddlBolsaAlunoAlt.SelectedValue != "" && this.ddlBolsaAlunoAlt.SelectedValue != "0" ? TB148_TIPO_BOLSA.RetornaPelaChavePrimaria(int.Parse(this.ddlBolsaAlunoAlt.SelectedValue)) : null;
                if (chkManterDescontoPerc.Checked)
                {
                    tb07.NU_PEC_DESBOL = decimal.TryParse(this.txtValorDescto.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                    tb07.NU_VAL_DESBOL = null;
                }
                else
                {
                    tb07.NU_PEC_DESBOL = null;
                    tb07.NU_VAL_DESBOL = decimal.TryParse(this.txtValorDescto.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                }

                tb07.DT_VENC_BOLSA = DateTime.TryParse(this.txtPeriodoIniDesconto.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                tb07.DT_VENC_BOLSAF = DateTime.TryParse(this.txtPeriodoFimDesconto.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
            }
            TB07_ALUNO.SaveOrUpdate(tb07);
            #endregion

            //chkEndAddAlu.Checked = true;
            //ControlaTabs("ENA");
        }

        protected void txtValorContratoCalc_TextChanged(object sender, EventArgs e)
        {
            int qtPar = int.Parse(txtQtdeParcelas.Text);
            decimal vlContr = decimal.Parse(txtValorContratoCalc.Text);

            decimal vlParc = Math.Round(vlContr / qtPar);

            txtValorPrimParce.Text = vlParc.ToString("N2");
        }

        protected void chkTaxaMatricula_OnCheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                carregaValorTaxaMatricula();
            }
            else
            {
                txtVlTxMatricula.Enabled = false;
                txtVlTxMatricula.Text = "";
            }
        }

        #endregion
    }
}
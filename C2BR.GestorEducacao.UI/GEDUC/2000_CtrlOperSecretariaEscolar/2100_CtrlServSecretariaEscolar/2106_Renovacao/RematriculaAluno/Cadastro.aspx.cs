//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: RENOVAÇÃO DE MATRÍCULAS DE ALUNOS
// OBJETIVO: REMATRÍCULA DE ALUNOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2106_Renovacao.RematriculaAluno
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }
        private Dictionary<string, string> turnos = AuxiliBaseApoio.chave(tipoTurnoTurma.ResourceManager);

        #region Eventos
        
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            CarregaAno();
            CarregaModalidades();
            CarregaSerieCurso();
            divGrid.Visible = false;
            CurrentPadraoCadastros.BarraCadastro.HabilitarBotoes("btnNewSearch", false);
        }

        ///Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB079_HIST_ALUNO tb79;
            TB48_GRADE_ALUNO tb48;
            TB08_MATRCUR tb08;
            TB80_MASTERMATR tb80;
            int qtdAlunos = 0;
            
            ///Varre toda a gride de Aluno
            foreach (GridViewRow linha in grdAlunos.Rows)
            {                
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    if (((DropDownList)linha.Cells[4].FindControl("ddlProxTurma")).SelectedValue == "")
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Turma deve ser selecionada");
                        return;
                    }
                    else
                    {
                        qtdAlunos += 1;

                        int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                        int anoInt = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
                        int proxSerie = Convert.ToInt32(((HiddenField)linha.Cells[4].FindControl("hdProxSerie")).Value);
                        int proxTurma = Convert.ToInt32(((DropDownList)linha.Cells[4].FindControl("ddlProxTurma")).SelectedValue);
                        string strProxMatricula = ((Label)linha.Cells[1].FindControl("lblMatricula")).Text;
                        string strProxAno = (anoInt + 1).ToString();
                        strProxMatricula = (strProxAno.Substring(2, 2) + strProxMatricula.Substring(2, strProxMatricula.Length - 2)).Replace(".", "");
                        int coAlu = Convert.ToInt32(grdAlunos.DataKeys[linha.RowIndex].Values[0]);

                        ///Atualiza informações financeiro na tabela de matrícula se habilitado
                        decimal totalCur = 0;
                        decimal totalDesctoBolsa = 0;
                        DateTime? dtPrimeiParce = null;
                        int diaVecto = 0;
                        int qtdeParceDescto = 0;
 
                        ///Retorna o turno da próxima turma
                        string strTurno = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                           where tb06.CO_EMP == LoginAuxili.CO_EMP && tb06.CO_CUR == proxSerie 
                                           && tb06.CO_MODU_CUR == modalidade && tb06.CO_TUR == proxTurma
                                           select new { tb06.CO_PERI_TUR }).FirstOrDefault().CO_PERI_TUR;

                        #region Atualiza o histórico do aluno
                        ///Segundo: Retorna a grade da Série para fazer a inserção das informações no Histórico (TB079_HIST_ALUNO) e Grade de Aluno (TB48_GRADE_ALUNO)
                        List<TB43_GRD_CURSO> lstTb43 = GradeSerie(proxSerie);

                        foreach (var tb43 in lstTb43)
                        {
                            tb79 = new TB079_HIST_ALUNO();

                            tb79.CO_ALU = coAlu;
                            tb79.CO_ANO_REF = strProxAno;
                            tb79.CO_CUR = proxSerie;
                            tb79.CO_EMP = LoginAuxili.CO_EMP;
                            tb79.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                            tb79.CO_MAT = tb43.CO_MAT;
                            tb79.CO_MODU_CUR = modalidade;
                            tb79.CO_TUR = proxTurma;
                            tb79.CO_USUARIO = LoginAuxili.CO_COL;
                            //tb79.DT_LANC = DateTime.Now;
                            tb79.DT_LANC = DateTime.Parse("05/01/" + strProxAno);
                            tb79.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(modalidade);
                            tb79.CO_FLAG_STATUS = "A";
                            TB079_HIST_ALUNO.SaveOrUpdate(tb79, false);
                            
                            tb48 = new TB48_GRADE_ALUNO();

                            tb48.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                            tb48.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                            tb48.CO_ANO_MES_MAT = strProxAno;
                            tb48.CO_CUR = proxSerie;
                            tb48.CO_MAT = tb43.CO_MAT;
                            tb48.CO_MODU_CUR = modalidade;
                            tb48.CO_STAT_MATE = "E";
                            tb48.CO_TUR = proxTurma;
                            tb48.NU_SEM_LET = "1";
                            tb48.CO_FLAG_STATUS = "A";
                            tb48.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                            tb48.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(modalidade);

                            TB48_GRADE_ALUNO.SaveOrUpdate(tb48, false);
                        }

                        #endregion

                        #region Atualiza a master matrícula do aluno
                        ///Terceiro: Faz a inserção das informações na tabela de matrícula Master (TB80_MASTERMATR)
                        tb80 = TB80_MASTERMATR.RetornaPelaChavePrimaria(modalidade, coAlu, strProxAno, null);

                        if (tb80 == null)
                        {
                            tb80 = new TB80_MASTERMATR();

                            tb80.CO_ALU = coAlu;
                            tb80.CO_ALU_CAD = strProxMatricula;
                            tb80.CO_ANO_MES_MAT = strProxAno;
                            tb80.CO_COL = LoginAuxili.CO_COL;
                            tb80.CO_CUR = proxSerie;
                            tb80.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                            tb80.CO_SITU_MTR = "A";
                            /*
                            tb80.DT_CADA_MTR = DateTime.Now;
                            tb80.DT_SITU_MTR = DateTime.Now;
                            tb80.DT_ALT_REGISTRO = DateTime.Now;
                             */
                            tb80.DT_CADA_MTR = DateTime.Parse("05/01/" + strProxAno);
                            tb80.DT_SITU_MTR = DateTime.Parse("05/01/" + strProxAno);
                            tb80.DT_ALT_REGISTRO = DateTime.Parse("05/01/" + strProxAno);                            
                            tb80.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                            tb80.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(modalidade);
                        }
                        else
                        {
                            tb80.CO_SITU_MTR = "A";
                            //tb80.DT_ALT_REGISTRO = DateTime.Now;
                            tb80.DT_ALT_REGISTRO = DateTime.Parse("05/01/" + strProxAno);
                        }

                        TB80_MASTERMATR.SaveOrUpdate(tb80, false);

                        #endregion

                        #region Atualiza a quantidade vagas

                        ///Quarto: Faz a alteração do campo de quantidade de alunos matriculados da tabela TB289_DISP_VAGA_TURMA; adiciona mais um no mesmo
                        var tb289 = (from iTb289 in TB289_DISP_VAGA_TURMA.RetornaTodosRegistros()
                                     where iTb289.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && iTb289.TB44_MODULO.CO_MODU_CUR == modalidade
                                     && iTb289.CO_CUR == proxSerie && iTb289.CO_ANO == strProxAno && iTb289.CO_TUR == proxTurma && iTb289.CO_PERI_TUR == strTurno
                                     select iTb289).FirstOrDefault();

                        if (tb289 != null)
                        {
                            if (tb289.QTDE_VAG_MAT == null)
                                tb289.QTDE_VAG_MAT = 1;
                            else
                                tb289.QTDE_VAG_MAT = tb289.QTDE_VAG_MAT + 1;

                            TB289_DISP_VAGA_TURMA.SaveOrUpdate(tb289, false);
                        }

                        #endregion

                        var varTur = TB129_CADTURMAS.RetornaPelaChavePrimaria(proxTurma);

                        #region Atualiza o financeiro

                        if (((CheckBox)linha.Cells[6].FindControl("ckSelectFinan")).Checked)
                        {                            
                            var inforFinan = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                             join tb83 in TB83_PARAMETRO.RetornaTodosRegistros() on tb07.CO_EMP equals tb83.CO_EMP
                                              where tb07.CO_ALU == coAlu && tb83.CO_EMP == varTur.CO_EMP_UNID_CONT
                                             select new
                                             {
                                                 tb83.NU_DIA_VENCTO, tb83.VL_PERCE_JUROS, tb83.VL_PERCE_MULTA, tb83.ID_BOLETO_MATRIC_RENOV, tb07.NU_NIRE,
                                                 tb07.NU_PEC_DESBOL, tb07.NU_VAL_DESBOL, tb07.DT_VENC_BOLSA, tb07.DT_VENC_BOLSAF, tb07.TB108_RESPONSAVEL.CO_RESP, tb07.TB148_TIPO_BOLSA.CO_TIPO_BOLSA
                                             }).FirstOrDefault();

                            if (inforFinan != null)
                            {
                                var varSer = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, proxSerie);                                

                                decimal menCur = varSer != null ? Decimal.Parse(varSer.VL_TOTA_CUR.ToString()) : 0;
                                decimal multaUnid = inforFinan.VL_PERCE_MULTA != null ? (decimal)inforFinan.VL_PERCE_MULTA : 0;
                                decimal jurosUnid = inforFinan.VL_PERCE_JUROS != null ? (decimal)inforFinan.VL_PERCE_JUROS : 0;
                                int diaVenctoUnid = inforFinan.NU_DIA_VENCTO != null ? (int)inforFinan.NU_DIA_VENCTO : 0;

                                totalCur = menCur;
                                menCur = menCur / 12;

                                var varModal = (from tb44 in TB44_MODULO.RetornaTodosRegistros()
                                                where tb44.CO_MODU_CUR == modalidade
                                                select tb44).FirstOrDefault();

                                if ((menCur != 0) && (varModal.CO_SEQU_PC != null && varModal.CO_SEQU_PC_CAIXA != null && varModal.CO_SEQU_PC_BANCO != null))
                                {
                                    List<TB47_CTA_RECEB> lstTb47 = new List<TB47_CTA_RECEB>();

                                    TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(varTur.CO_EMP_UNID_CONT);

                                    TB47_CTA_RECEB tb47;

                                    diaVecto = inforFinan.NU_DIA_VENCTO != null ? (int)inforFinan.NU_DIA_VENCTO : 5;
                                    dtPrimeiParce = DateTime.Parse(diaVecto.ToString("00") + "/01/" + strProxAno);

                                    for (int i = 1; i <= 12; i++)
                                    {
                                        DateTime dataSelec = DateTime.Now;
                                        //Para popular a base com as matrículas específicas
                                        dataSelec = DateTime.Parse(diaVecto.ToString("00") + "/01/" + strProxAno);

                                        DateTime dataVcto = DateTime.Parse(diaVecto.ToString("00") + "/" + i.ToString("00") + "/" + strProxAno);                                        

                                        tb47 = new TB47_CTA_RECEB();
                                        tb47.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                                        tb47.CO_EMP_UNID_CONT = varTur.CO_EMP_UNID_CONT;
                                        tb47.NU_DOC = "RN" + dataSelec.ToString("yy") + '.' + inforFinan.NU_NIRE.ToString().PadLeft(10, '0') + '.' + i.ToString("00");
                                        tb47.NU_PAR = i;
                                        tb47.QT_PAR = 12;
                                        //tb47.DT_CAD_DOC = DateTime.Now;
                                        tb47.DT_CAD_DOC = DateTime.Parse("05/01/" + strProxAno);
                                        tb47.DE_COM_HIST = "VALOR MENSALIDADE ESCOLAR.";
                                        tb47.VR_TOT_DOC = menCur * 12;
                                        tb47.VR_PAR_DOC = menCur;
                                        tb47.DT_VEN_DOC = dataVcto;
                                        tb47.VR_DES_DOC = null;
                                        tb47.VR_MUL_DOC = inforFinan.VL_PERCE_MULTA != null ? inforFinan.VL_PERCE_MULTA : null;
                                        if (inforFinan.VL_PERCE_JUROS != null)
                                        {
                                            decimal juros = decimal.Zero + (decimal)inforFinan.VL_PERCE_JUROS;
                                            tb47.VR_JUR_DOC = decimal.Parse(string.Format("{0:0.0000}", juros));
                                        }
                                        else
                                            tb47.VR_JUR_DOC = null;
                                        tb47.DT_EMISS_DOCTO = dataSelec;

                                        ///Flag emissão boleto "S"im ou "N"ão
                                        if (inforFinan.ID_BOLETO_MATRIC_RENOV != null)
                                        {
                                            tb47.FL_EMITE_BOLETO = "S";
                                            ///Salvando o tipo de documento "Boleto Bancário"
                                            tb47.TB086_TIPO_DOC = TB086_TIPO_DOC.RetornaPeloCoTipoDoc(3);

                                            ///Dados do boleto bancário
                                            tb47.TB227_DADOS_BOLETO_BANCARIO = TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria((int)inforFinan.ID_BOLETO_MATRIC_RENOV);
                                        }
                                        else
                                        {
                                            tb47.FL_EMITE_BOLETO = "N";
                                            ///Salvando o tipo de documento "Recibo"
                                            tb47.TB086_TIPO_DOC = TB086_TIPO_DOC.RetornaPeloCoTipoDoc(1);
                                        }

                                        ///Salvando o "Local de Cobrança"
                                        tb47.TB101_LOCALCOBRANCA = TB101_LOCALCOBRANCA.RetornaPelaChavePrimaria(0);

                                        if (tb25.CO_HIST_MAT != null)
                                            tb47.TB39_HISTORICO = TB39_HISTORICO.RetornaPelaChavePrimaria(tb25.CO_HIST_MAT.Value);

                                        if (varModal.CO_CENT_CUSTO != null)
                                            tb47.TB099_CENTRO_CUSTO = TB099_CENTRO_CUSTO.RetornaPelaChavePrimaria(varModal.CO_CENT_CUSTO.Value);

                                        tb47.TB56_PLANOCTA = TB56_PLANOCTA.RetornaPelaChavePrimaria(varModal.CO_SEQU_PC.Value);
                                        tb47.CO_SEQU_PC_BANCO = varModal.CO_SEQU_PC_BANCO;
                                        tb47.CO_SEQU_PC_CAIXA = varModal.CO_SEQU_PC_CAIXA;

                                        tb47.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                                        tb47.CO_FLAG_TP_VALOR_MUL = "P";
                                        tb47.CO_FLAG_TP_VALOR_JUR = "P";
                                        tb47.CO_FLAG_TP_VALOR_DES = "V";
                                        tb47.CO_FLAG_TP_VALOR_OUT = "V";
                                        tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO = "V";

                                        tb47.IC_SIT_DOC = "A";
                                        tb47.TP_CLIENTE_DOC = "A";

                                        ///Formato =>Ano: XXXX - Série: XXXXX - Turma: XXXXX - Turno: XXXXX
                                        tb47.DE_OBS_BOL_MAT = "Ano: " + dataVcto.ToString("yyyy") + " - Série/Curso: " + ((Label)linha.Cells[4].FindControl("lblSerieDest")).Text + " - Turma: " + varTur.NO_TURMA + " - Turno: " + turnos[strTurno];
                                        
                                        tb47.DE_OBS = "MENSALIDADE ESCOLAR";

                                        tb47.CO_ALU = coAlu;
                                        tb47.CO_ANO_MES_MAT = strProxAno;
                                        tb47.NU_SEM_LET = "1";
                                        tb47.CO_CUR = proxSerie;
                                        tb47.CO_TUR = proxTurma;
                                        tb47.CO_MODU_CUR = modalidade;
                                        tb47.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(inforFinan.CO_RESP);
                                        tb47.DT_ALT_REGISTRO = DateTime.Now;

                                        ///Atualiza o código da bolsa
                                        if (inforFinan.CO_TIPO_BOLSA != null)
                                            tb47.TB148_TIPO_BOLSA = TB148_TIPO_BOLSA.RetornaPelaChavePrimaria(inforFinan.CO_TIPO_BOLSA);

                                        ///Faz a verificação para saber se aluno terá desconto de bolsa
                                        if ((inforFinan.DT_VENC_BOLSA != null) && (inforFinan.DT_VENC_BOLSAF != null) &&
                                            (inforFinan.NU_PEC_DESBOL != null || inforFinan.NU_VAL_DESBOL != null))
                                        {
                                            if ((dataVcto >= inforFinan.DT_VENC_BOLSA) && (dataVcto <= inforFinan.DT_VENC_BOLSAF))
                                            {
                                                if (inforFinan.NU_VAL_DESBOL != null)
                                                {
                                                    qtdeParceDescto++;
                                                    totalDesctoBolsa = totalDesctoBolsa + inforFinan.NU_VAL_DESBOL.Value;
                                                    tb47.VL_DES_BOLSA_ALUNO = inforFinan.NU_VAL_DESBOL;
                                                    tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO = "V";

                                                }
                                                else
                                                {
                                                    qtdeParceDescto++;
                                                    totalDesctoBolsa = totalDesctoBolsa + ((menCur * inforFinan.NU_PEC_DESBOL.Value)/100);
                                                    tb47.VL_DES_BOLSA_ALUNO = inforFinan.NU_PEC_DESBOL;
                                                    tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO = "P";
                                                }
                                            }
                                        }

                                        lstTb47.Add(tb47);
                                    }
                                }                                
                            }
                        }

                        #endregion

                        #region Atualia a matrícula dados finais

                        ///Primeiro: Faz a inserção das informações na tabela de matrícula (TB08_MATRCUR)
                        tb08 = new TB08_MATRCUR();                        

                        tb08.CO_ALU_CAD = strProxMatricula;
                        tb08.CO_ANO_MES_MAT = strProxAno;
                        tb08.CO_COL = LoginAuxili.CO_COL;
                        tb08.CO_CUR = proxSerie;
                        tb08.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                        tb08.CO_EMP_UNID_CONT = varTur.CO_EMP_UNID_CONT;
                        tb08.CO_SIT_MAT = "A";
                        tb08.CO_TUR = proxTurma;
                        tb08.DT_CAD_MAT = DateTime.Parse("05/01/" + strProxAno);
                        tb08.DT_CADASTRO = DateTime.Parse("05/01/" + strProxAno);
                        tb08.DT_EFE_MAT = DateTime.Parse("05/01/" + strProxAno);
                        tb08.DT_SIT_MAT = DateTime.Parse("05/01/" + strProxAno);
                        tb08.CO_TURN_MAT = strTurno;
                        tb08.FLA_REMATRICULADO = null;
                        tb08.FLA_ESCOLA_NOVATO = false;
                        tb08.FLA_ESCOLA_ANO_NOVATO = false;
                        tb08.NU_SEM_LET = "1";
                        tb08.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                        tb08.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(modalidade);

                        #region Atualiza os valores da no cadastro da matrícula

                        if (((CheckBox)linha.Cells[6].FindControl("ckSelectFinan")).Checked)
                        {
                            tb08.VL_TOT_MODU_MAT = totalCur;
                            tb08.VL_DES_BOL_MOD_MAT = totalDesctoBolsa;
                            tb08.VL_ENT_MOD_MAT = totalCur/12;
                            tb08.VL_PAR_MOD_MAT = totalCur / 12;
                            tb08.QT_PAR_MOD_MAT = 12;
                            tb08.NU_DIA_VEN_MOD_MAT = diaVecto;
                            tb08.DT_PRI_PAR_MOD_MAT = dtPrimeiParce;
                            tb08.QT_PAR_DES_MAT = qtdeParceDescto;
                        }

                        #endregion

                        TB08_MATRCUR.SaveOrUpdate(tb08, false);

                        #endregion

                        int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

                        ///Atualiza a flag de rematrículado para "S" do aluno selecionado 
                        var atualizaFlagRematric = TB08_MATRCUR.RetornaPelaChavePrimaria(coAlu, serie, anoInt.ToString(), "1");

                        atualizaFlagRematric.FLA_REMATRICULADO = "S";

                        TB08_MATRCUR.SaveOrUpdate(atualizaFlagRematric, false);

                        GestorEntities.CurrentContext.SaveChanges();
                    }
                }
            }

            if (qtdAlunos > 0)
                AuxiliPagina.RedirecionaParaPaginaSucesso("Registro Salvo com Sucesso", Request.Url.AbsoluteUri);
            else
                AuxiliPagina.EnvioMensagemErro(this, "Não há alunos para rematrícula");
          
        }        
        #endregion

        #region Carregamento

        /// <summary>
        /// Carrega todos os anos com matrícula ativa
        /// </summary>
        private void CarregaAno()
        {
            ddlAno.Items.Clear();
            ddlAno.Items.AddRange(AuxiliBaseApoio.AnosDDL(LoginAuxili.CO_EMP, true));
            if (ddlAno.Items.FindByText(DateTime.Now.Year.ToString()) != null)
                ddlAno.SelectedValue = DateTime.Now.Year.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.Items.Clear();
            ddlModalidade.Items.AddRange(AuxiliBaseApoio.ModulosDDL(LoginAuxili.ORG_CODIGO_ORGAO, true));
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            ddlSerieCurso.Items.Clear();
            ddlSerieCurso.Items.AddRange(AuxiliBaseApoio.SeriesDDL(LoginAuxili.CO_EMP, ddlModalidade.SelectedValue, ddlAno.SelectedValue, true));

        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            ddlTurma.Items.Clear();
            ddlTurma.Items.AddRange(AuxiliBaseApoio.TurmasDDL(LoginAuxili.CO_EMP, ddlModalidade.SelectedValue, ddlSerieCurso.SelectedValue, ddlAno.SelectedValue, true));
           
        }

        /// <summary>
        /// Método que retorna a grade de série de acordo com o código informado
        /// </summary>
        /// <param name="serie">Id da série</param>
        /// <returns></returns>
        private List<TB43_GRD_CURSO> GradeSerie(int serie)
        {
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;

            string proxAno = (ano + 1).ToString();

            var tb43 = (from lTb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                        where lTb43.CO_EMP == LoginAuxili.CO_EMP && lTb43.CO_ANO_GRADE == proxAno && lTb43.CO_CUR == serie
                        select lTb43).ToList();

            return tb43;
        }

        /// <summary>
        /// Método que carrega a grid de Alunos
        /// </summary>
        private void CarregaGrid()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string anoAtual = ddlAno.SelectedValue;
            int anoAtualInt = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
            if (modalidade == 0 || serie == 0 || turma == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Modalidade, Série/Curso e turma devem ser selecionados");
                grdAlunos.DataBind();
                return;
            }
            
            var resultado = (from m08 in TB08_MATRCUR.RetornaTodosRegistros()
                              join a07 in TB07_ALUNO.RetornaTodosRegistros()
                              on m08.CO_ALU equals a07.CO_ALU
                              join s01 in TB01_CURSO.RetornaTodosRegistros()
                              on m08.CO_CUR equals s01.CO_CUR
                              where m08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP 
                              && m08.CO_ANO_MES_MAT == anoAtual 
                              && m08.CO_CUR == serie
                              && m08.CO_TUR == turma 
                              && m08.TB44_MODULO.CO_MODU_CUR == modalidade 
                              && (m08.FLA_REMATRICULADO == null || m08.FLA_REMATRICULADO == "N")
                              select new
                              {    
                                  m08.CO_ALU, 
                                  CO_ALU_CAD = m08.CO_ALU_CAD.Insert(2,".").Insert(6,"."),
                                  NU_NIRE = a07.NU_NIRE,
                                  a07.NO_ALU, 
                                  m08.CO_SIT_MAT,
                                  PROX_CURSO = ((m08.CO_STA_APROV == "A" && (m08.CO_STA_APROV_FREQ == "A" || m08.CO_STA_APROV_FREQ == null)) ? s01.CO_PREDEC_CUR : ((m08.CO_STA_APROV == "R" || m08.CO_STA_APROV_FREQ == "R") ? m08.CO_CUR : 0)),
                                  STATUS = ((m08.CO_STA_APROV == "A" && (m08.CO_STA_APROV_FREQ == "A" || m08.CO_STA_APROV_FREQ == null)) ? "Aprovado" : ((m08.CO_STA_APROV == "R" || m08.CO_STA_APROV_FREQ == "R") ? "Reprovado" : "Com Pendência"))
                              }).ToList();

            var resultado2 = (from result in resultado  
                              join tb01 in TB01_CURSO.RetornaTodosRegistros() on result.PROX_CURSO equals tb01.CO_CUR into novaSerie
                              from novSer in novaSerie.DefaultIfEmpty()
                              select new
                              {
                                  CO_ALU = result.CO_ALU, 
                                  CO_ALU_CAD = result.CO_ALU_CAD, 
                                  NU_NIRE = result.NU_NIRE,
                                  CO_SIT_MAT = result.CO_SIT_MAT, 
                                  NO_ALU = result.NO_ALU,
                                  PROX_CURSO = (result.PROX_CURSO ?? 0), 
                                  STATUS = result.STATUS, 
                                  NO_CUR = (novSer != null ? novSer.NO_CUR : "")
                              }).OrderBy(p => p.NO_ALU);

            divGrid.Visible = true;

            if (resultado2.Count() > 0)
            {                               
               //Habilitar botão de salvar
            }
            else
            {             
                grdAlunos.DataBind();
                return;
            }
            List<listaAlunos> listaGrid = new List<listaAlunos>();
            foreach(var linha in resultado2)
            {
                listaAlunos novaLinha = new listaAlunos();
                novaLinha.CO_ALU = linha.CO_ALU;
                novaLinha.CO_ALU_CAD = linha.CO_ALU_CAD;
                novaLinha.NU_NIRE = linha.NU_NIRE; 
                novaLinha.CO_SIT_MAT = linha.CO_SIT_MAT;
                novaLinha.NO_ALU = linha.NO_ALU;
                novaLinha.PROX_CURSO = linha.PROX_CURSO;
                novaLinha.STATUS = linha.STATUS;
                novaLinha.NO_CUR = linha.NO_CUR;
                if (linha.CO_SIT_MAT == "F")
                {
                    var matriculado = TB08_MATRCUR.RetornaPelaChavePrimaria(linha.CO_ALU, linha.PROX_CURSO, (anoAtualInt + 1).ToString(), "1");
                    if (matriculado != null)
                    {
                        if (matriculado.CO_SIT_MAT == "R")
                        {
                            novaLinha.CO_SIT_MAT = "R";
                            novaLinha.STATUS = "Pré-Renovado";
                        }
                        else
                        {
                            novaLinha.CO_SIT_MAT = "M";
                            novaLinha.STATUS = "Renovado";
                        }
                    }

                }
                listaGrid.Add(novaLinha);
            }

            grdAlunos.DataKeyNames = new string[] { "CO_ALU" };

            grdAlunos.DataSource = listaGrid;
            grdAlunos.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        /// <param name="ddl">DropDown de turma</param>
        /// <param name="serie">Id da série</param>
        private void CarregaTurmaDdlGrid(DropDownList ddl, int serie)
        {
            var resultado = (from t06 in TB06_TURMAS.RetornaTodosRegistros()
                             join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on t06.CO_TUR equals tb129.CO_TUR
                             where t06.CO_CUR == serie && t06.CO_EMP == LoginAuxili.CO_EMP
                             select new { tb129.CO_TUR, tb129.NO_TURMA, t06.QTD_ALUNO_TUR }).ToList();

            var resultado2 = (from result in resultado
                              select new { result.CO_TUR, NO_TURMA = result.NO_TURMA }).OrderBy(p => p.NO_TURMA);

            ddl.DataSource = resultado2;

            ddl.DataTextField = "NO_TURMA";
            ddl.DataValueField = "CO_TUR";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion    

        #region Eventos de componentes
        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            grdAlunos.DataBind();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            grdAlunos.DataBind();
        }        

        protected void grdAlunos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField lblProxSerie = (HiddenField)e.Row.FindControl("hdProxSerie");
                int proxSerie = Convert.ToInt32(lblProxSerie.Value);
                
                DropDownList ddlTurma = (DropDownList)e.Row.FindControl("ddlProxTurma");
                
                CarregaTurmaDdlGrid(ddlTurma, proxSerie);

                HiddenField hdSituacaoMatricula = (HiddenField)e.Row.FindControl("hdSitMat");
                CheckBox chkSelect = (CheckBox)e.Row.FindControl("ckSelect");
                CheckBox ckSelectFinan = (CheckBox)e.Row.FindControl("ckSelectFinan");
                ckSelectFinan.Enabled = false;                

//------------> Habilita apenas os alunos que apresentam matrícula finalizada
                if (hdSituacaoMatricula.Value == "F")
                {
                    chkSelect.Enabled = ddlTurma.Enabled = true;
                    ckSelectFinan.Enabled = TB83_PARAMETRO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP).FL_INTEG_FINAN == "S";

                    e.Row.ControlStyle.BackColor = System.Drawing.Color.FromArgb(251,233,183);

                    List<TB43_GRD_CURSO> lstTb43 = GradeSerie(proxSerie);

                    if (lstTb43.Count <= 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Série/Curso de Destino não possui Grade de Matérias gerado");
                        return;
                    }
                }
                else
                    chkSelect.Enabled = ddlTurma.Enabled = false;      
            }
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidades();
        }

        #endregion

        #region Classe
        private class listaAlunos
        {
            public int CO_ALU { get; set; }
            public string CO_ALU_CAD { get; set; }
            public int NU_NIRE { get; set; }
            public string CO_SIT_MAT { get; set; } 
            public string NO_ALU { get; set; }
            public int PROX_CURSO { get; set; } 
            public string STATUS { get; set; }
            public string NO_CUR { get; set; }
        }
        #endregion


    }
}

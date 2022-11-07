//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE FREQÜÊNCIA ESCOLAR DO ALUNO
// OBJETIVO: LANÇAMENTO E MANUTENÇÃO INDIVIDUAL DE FREQÜÊNCIA DE ALUNOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 16/07/2013| André Nobre Vinagre        | Colocado o tratamento da data de lançamento do bimestre
//           |                            | vindo da tabela de unidade
//           |                            |
// ----------+----------------------------+-------------------------------------

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.GEDUC.F3000_CtrlOperacionalPedagogico.F3400_CtrlFrequenciaEscolar.F3401_LancManutIndivFreqAluno
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
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao) ||
                QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                AuxiliPagina.RedirecionaParaPaginaMensagem("Favor, selecione um aluno.", Request.Url.AbsolutePath.ToLower().Replace("cadastro", "busca") + "&moduloNome=" + Request.QueryString["moduloNome"], C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Error);          
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0;
            int coAnoRefer = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            int coCol = ddlResponsavel.SelectedValue != "" ? int.Parse(ddlResponsavel.SelectedValue) : 0;

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb25.TB82_DTCT_EMPReference.Load();

            DateTime dataLacto = DateTime.Now.Date;
            if (tb25.TB82_DTCT_EMP != null)
            {
                if (ddlBimestre.SelectedValue == "B1")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM1 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM1 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM1 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Frequência do 1º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else if (ddlBimestre.SelectedValue == "B2")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM2 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM2 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM2 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Frequência do 2º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else if (ddlBimestre.SelectedValue == "B3")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM3 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM3 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM3 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Frequência do 3º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else if (ddlBimestre.SelectedValue == "B4")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM4 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_BIM4 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_BIM4 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Frequência do 4º Bimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else if (ddlBimestre.SelectedValue == "T1")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI1 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI1 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI1 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI1 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Frequência do 1º Trimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else if (ddlBimestre.SelectedValue == "T2")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI2 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI2 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI2 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI2 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Frequência do 2º Trimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
                else if (ddlBimestre.SelectedValue == "T3")
                {
                    if (tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI3 != null && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI3 != null)
                    {
                        if (!(tb25.TB82_DTCT_EMP.DT_LACTO_INICI_TRI3 <= dataLacto && tb25.TB82_DTCT_EMP.DT_LACTO_FINAL_TRI3 >= dataLacto))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Frequência do 3º Trimestre inválida. Para alterar a mesma acessar o cadastro da unidade.");
                            return;
                        }
                    }
                }
            }

            DateTime dataFrequencia = DateTime.Parse(ddlDataFrequencia.SelectedValue);

            if (dataFrequencia > DateTime.Now)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data de Lançamento de Frequência não pode ser superior a data atual.");
                return;
            }

            TB132_FREQ_ALU tb132 = RetornaEntidade();

            if (tb132.ID_FREQ_ALUNO == 0)
            {
                tb132 = (from lTb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                         where lTb132.TB07_ALUNO.CO_ALU == coAlu && lTb132.CO_TUR == turma && lTb132.TB01_CURSO.CO_EMP == LoginAuxili.CO_EMP
                         && lTb132.TB01_CURSO.CO_MODU_CUR == modalidade && lTb132.TB01_CURSO.CO_CUR == serie
                         select lTb132).FirstOrDefault();

                if (tb132 == null)
                {
                    tb132 = new TB132_FREQ_ALU();

                    tb132.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
                    tb132.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                    tb132.CO_ANO_REFER_FREQ_ALUNO = coAnoRefer;
                    tb132.CO_TUR = turma;
                }                                
            }

            tb132.CO_COL = coCol;
            tb132.CO_MAT = materia;
            tb132.CO_FLAG_FREQ_ALUNO = ddlFrequencia.SelectedValue;

            if (tb132.CO_FLAG_FREQ_ALUNO == "N")
            {
                tb132.CO_FLAG_ATESTADO = ddlAtestado.SelectedValue;
                tb132.DE_JUSTI_FREQ_ALUNO = txtJustificativa.Text;
                if (ddlAtestado.SelectedValue == "S")
                {
                    CadastraAtestado(tb132);
                } 
            }
            else
            {
                tb132.CO_FLAG_ATESTADO = null;
                tb132.DE_JUSTI_FREQ_ALUNO = null;
            }

            tb132.DT_FRE = dataFrequencia;

            tb132.CO_ATIV_PROF_TUR = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                                      where tb119.DT_ATIV_REAL == dataFrequencia && tb119.CO_EMP == LoginAuxili.CO_EMP
                                      && tb119.CO_MODU_CUR == modalidade && tb119.CO_CUR == serie && tb119.CO_TUR == turma
                                      select new { tb119.CO_ATIV_PROF_TUR }).FirstOrDefault().CO_ATIV_PROF_TUR;

            tb132.DT_LANCTO_FREQ_ALUNO = DateTime.Now;

            CurrentPadraoCadastros.CurrentEntity = tb132;
            
            if (chkAtualizaHist.Checked)
            {
                tb132.TB01_CURSOReference.Load();
                tb132.TB07_ALUNOReference.Load();

                AuxiliGeral.AtualizaHistFreqAlu(this, tb132.TB01_CURSO.CO_EMP, tb132.CO_ANO_REFER_FREQ_ALUNO, tb132.TB01_CURSO.CO_MODU_CUR, tb132.TB01_CURSO.CO_CUR, tb132.CO_TUR, ddlBimestre.SelectedValue);
            }
        }

        public void CadastraAtestado(TB132_FREQ_ALU tb132)
        {
            DateTime dataInicioAtest = DateTime.Parse(txtDataAtestado.Text);
            short qtdDias = Int16.Parse(ddlQtdDias.SelectedValue);

            TBG101_ATESTADOS tbg101 = TBG101_ATESTADOS.RetornaPelaFreq(tb132.ID_FREQ_ALUNO);
            if (tbg101 == null)
            {
                tbg101 = new TBG101_ATESTADOS();
            }

            tbg101.CL_ATEST = "A";
            tbg101.TP_ATEST = ddlTipo.SelectedValue;
            tbg101.TB132_FREQ_ALU = tb132;
            tbg101.NM_PROFI_SAUDE = txtProfiSaude.Text;
            tbg101.NR_REGIS_ORGAO = txtNumRegistro.Text;
            tbg101.CO_REGIS_ORGAO = ddlUF.SelectedValue;
            tbg101.UF_REGIS_ORGAO = ddlUF.SelectedValue != "" ? ddlUF.SelectedValue : "";
            tbg101.DT_EMISS_ATEST = dataInicioAtest;
            tbg101.QT_DIAS_ATEST = qtdDias;
            tbg101.DT_TERMI_ATEST = dataInicioAtest.AddDays(qtdDias);
            tbg101.CO_CID_PRINC_ATEST = txtCID.Text;
            tbg101.DE_OBSER_ATEST = txtJustificativa.Text;
            tbg101.IM_ATEST = upImagemAtest.RetornaImagem();           
            tbg101.NR_IP_SITUA_ATEST = LoginAuxili.IP_USU;
            tbg101.TB03_COLABOR1 = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);
            tbg101.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);

            TBG101_ATESTADOS.SaveOrUpdate(tbg101);

        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            CarregaColabs();
            CarregaAnos();
            CarregaModalidades();
            CarregaMedidas();
            CarregaQtdDias();
            CarregaUfs();

            TB132_FREQ_ALU tb132 = RetornaEntidade();

            if (tb132 != null)
            {
                tb132.TB01_CURSOReference.Load();
                tb132.TB07_ALUNOReference.Load();

                ddlAno.SelectedValue = tb132.CO_ANO_REFER_FREQ_ALUNO.ToString();
                ddlModalidade.SelectedValue = tb132.TB01_CURSO.CO_MODU_CUR.ToString();
                CarregaSerieCurso();
                ddlSerieCurso.SelectedValue = tb132.TB01_CURSO.CO_CUR.ToString();
                CarregaTurma();
                ddlTurma.SelectedValue = tb132.CO_TUR.ToString();
                CarregaAluno();
                ddlAluno.SelectedValue = tb132.TB07_ALUNO.CO_ALU.ToString();
                CarregaMatriculaAluno();

                //int coTurFrequencia = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                //                       where tb119.CO_ATIV_PROF_TUR == tb132.CO_ATIV_PROF_TUR && tb119.CO_EMP == tb132.TB01_CURSO.CO_EMP
                //                       && tb119.CO_MODU_CUR == tb132.TB01_CURSO.CO_MODU_CUR && tb119.CO_CUR == tb132.TB01_CURSO.CO_CUR
                //                       select new { tb119.CO_TUR }).FirstOrDefault().CO_TUR;

                int coTurFrequencia = tb132.CO_TUR;

                //CarregaTurmaFreq();
                //ddlTurmaFreq.SelectedValue = coTurFrequencia != 0 ? coTurFrequencia.ToString() : "";
                CarregaMaterias();
                ddlMateria.SelectedValue = tb132.CO_MAT.ToString();
                ddlResponsavel.SelectedValue = tb132.CO_COL.ToString();
                ddlAtestado.SelectedValue = tb132.CO_FLAG_ATESTADO != null ? tb132.CO_FLAG_ATESTADO.ToString() : "";
                ddlFrequencia.SelectedValue = tb132.CO_FLAG_FREQ_ALUNO.ToString();
                CarregaMatriculaAluno();
                CarregaMatriculaResponsavel();
                CarregaDataFrequencia();
                ddlDataFrequencia.SelectedValue = tb132.DT_FRE.ToString("dd/MM/yyyy");
                txtDataLancamento.Text = tb132.DT_LANCTO_FREQ_ALUNO.ToString("dd/MM/yyyy");
                txtJustificativa.Text = tb132.DE_JUSTI_FREQ_ALUNO != null ? tb132.DE_JUSTI_FREQ_ALUNO : "";

                TBG101_ATESTADOS tbg101 = TBG101_ATESTADOS.RetornaPelaFreq(tb132.ID_FREQ_ALUNO);

                if (tbg101 != null)
                {
                    txtProfiSaude.Text = tbg101.NM_PROFI_SAUDE;
                    ddlTipo.SelectedValue = tbg101.TP_ATEST;
                    txtNumRegistro.Text = tbg101.NR_REGIS_ORGAO;
                    txtCID.Text = tbg101.CO_CID_PRINC_ATEST;
                    ddlQtdDias.SelectedValue = tbg101.QT_DIAS_ATEST.ToString();
                    txtDataAtestado.Text = tbg101.DT_EMISS_ATEST.ToString("dd/MM/yyyy");
                    ddlUF.SelectedValue = tbg101.UF_REGIS_ORGAO;
                    upImagemAtest.CarregaImagem(tbg101.ID_ATEST);
                }
                HabilitaAtestadoEJustificativa();
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB132_FREQ_ALU</returns>
        private TB132_FREQ_ALU RetornaEntidade()
        {
            TB132_FREQ_ALU tb132 = TB132_FREQ_ALU.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb132 == null) ? new TB132_FREQ_ALU() : tb132;
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que desabilita atestado e justificativa se aluno presente
        /// </summary>
        private void HabilitaAtestadoEJustificativa()
        {
            if (ddlFrequencia.SelectedValue == "N")
            {
                ddlAtestado.Enabled = txtJustificativa.Enabled = true;
            }
            else
            {
                ddlAtestado.Enabled = txtJustificativa.Enabled = false;
                ddlAtestado.SelectedValue = "N";
                txtJustificativa.Text = "";                
            }
        }

        /// <summary>
        /// Método que carrega a matrícula do Funcionário selecionado
        /// </summary>
        private void CarregaMatriculaResponsavel()
        {
            txtResponsavel.Text = TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlResponsavel.SelectedValue)).CO_MAT_COL;
        }

        /// <summary>
        /// Método que carrega Matrícula do Aluno selecionado
        /// </summary>
        private void CarregaMatriculaAluno()
        {
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            string coAnoMesMat = ddlAno.SelectedValue;
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;

            if (coAlu > 0)
            {
                string strMatriculAluno = TB08_MATRCUR.RetornaPelaChavePrimaria(coAlu, serie, coAnoMesMat, "1").CO_ALU_CAD;

                txtAluno.Text = strMatriculAluno;
            }            
        }

        /// <summary>
        /// Método que carrega DropDown de Datas de Frequência
        /// </summary>
        private void CarregaDataFrequencia()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;

            if (modalidade == 0 || serie == 0 || turma == 0 || coAlu == 0)
                return;

            var tbTransfIntAtual = (from tbTransfInt in TB_TRANSF_INTERNA.RetornaTodosRegistros()
                                    where tbTransfInt.CO_CURSO_DESTI == serie && tbTransfInt.CO_UNIDA_DESTI == LoginAuxili.CO_EMP
                                    && tbTransfInt.CO_TURMA_ATUAL == turma && tbTransfInt.TB07_ALUNO.CO_ALU == coAlu
                                    select tbTransfInt);

            var tbTransfIntDest = (from tbTransfInt in TB_TRANSF_INTERNA.RetornaTodosRegistros()
                                   where tbTransfInt.CO_CURSO_DESTI == serie && tbTransfInt.CO_UNIDA_DESTI == LoginAuxili.CO_EMP
                                   && tbTransfInt.CO_TURMA_DESTI == turma && tbTransfInt.TB07_ALUNO.CO_ALU == coAlu
                                   select tbTransfInt);

            var resultado = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                             where tb119.CO_EMP == LoginAuxili.CO_EMP && tb119.CO_MODU_CUR == modalidade && tb119.CO_CUR == serie && tb119.CO_TUR == turma
                             select new { tb119.DT_ATIV_REAL }).ToList().Distinct();

            var resultadoFreq = (from result in resultado
                                 select new { DT_ATIV_REAL = result.DT_ATIV_REAL.ToString("dd/MM/yyyy") });

            if (tbTransfIntAtual.Count() > 0 && tbTransfIntDest.Count() > 0)
            {
                if (tbTransfIntAtual.Max(p => p.DT_EFETI_TRANSF) > tbTransfIntDest.Max(p => p.DT_EFETI_TRANSF))
                {
                    resultado = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                                 join tbTransfInterna in TB_TRANSF_INTERNA.RetornaTodosRegistros() on tb119.CO_TUR equals tbTransfInterna.CO_TURMA_ATUAL
                                 where tb119.CO_EMP == LoginAuxili.CO_EMP && tb119.CO_MODU_CUR == modalidade && tb119.CO_CUR == serie
                                 && tb119.CO_TUR == turma && tb119.DT_ATIV_REAL < tbTransfInterna.DT_EFETI_TRANSF
                                 select new { tb119.DT_ATIV_REAL }).ToList().Distinct();

                    resultadoFreq = (from result in resultado
                                     select new { DT_ATIV_REAL = result.DT_ATIV_REAL.ToString("dd/MM/yyyy") }).OrderBy(r => r.DT_ATIV_REAL);
                }   
            }            

            // ele fala que, neste if, a variável tbTransfIntAtual ta sem resultado
            if (tbTransfIntAtual.Count() > 0 && tbTransfIntDest.Count() > 0)
            {
                if (tbTransfIntDest.Max(p => p.DT_EFETI_TRANSF) > tbTransfIntAtual.Max(p => p.DT_EFETI_TRANSF))
                {
                    resultado = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                                 join tbTransfInterna in TB_TRANSF_INTERNA.RetornaTodosRegistros() on tb119.CO_TUR equals tbTransfInterna.CO_TURMA_DESTI
                                 where tb119.CO_EMP == LoginAuxili.CO_EMP && tb119.CO_MODU_CUR == modalidade && tb119.CO_CUR == serie
                                 && tb119.CO_TUR == turma && tb119.DT_ATIV_REAL > tbTransfInterna.DT_EFETI_TRANSF
                                 select new { tb119.DT_ATIV_REAL }).ToList().Distinct();

                    resultadoFreq = (from result in resultado
                                     select new { DT_ATIV_REAL = result.DT_ATIV_REAL.ToString("dd/MM/yyyy") }).OrderBy(r => r.DT_ATIV_REAL);
                }   
            }            

            ddlDataFrequencia.DataSource = resultadoFreq;

            ddlDataFrequencia.DataTextField = "DT_ATIV_REAL";
            ddlDataFrequencia.DataValueField = "DT_ATIV_REAL";
            ddlDataFrequencia.DataBind();

            ddlDataFrequencia.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Funcionários Responsáveis
        /// </summary>
        private void CarregaColabs()
        {
            ddlResponsavel.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                         select new { tb03.CO_COL, tb03.NO_COL }).OrderBy( c => c.NO_COL );

            ddlResponsavel.DataTextField = "NO_COL";
            ddlResponsavel.DataValueField = "CO_COL";
            ddlResponsavel.DataBind();

            CarregaMatriculaResponsavel();
        }
        /// <summary>
        /// Método que carrega o dropdown de UFs
        /// </summary>
        /// <param name="ddl">DropDownList UF</param>
        private void CarregaUfs()
        {
            ddlUF.DataSource = TB74_UF.RetornaTodosRegistros();
            ddlUF.DataTextField = "CODUF";
            ddlUF.DataValueField = "CODUF";
            ddlUF.DataBind();          
        }
        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = ddlAno.SelectedValue;

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb01.CO_MODU_CUR == modalidade
                                            join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.CO_ANO_GRADE == anoGrade && tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy( c => c.NO_CUR );

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }
            else
                ddlSerieCurso.Items.Clear();

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Matérias
        /// </summary>
        private void CarregaMaterias()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            ddlMateria.DataSource = (from tb02 in TB02_MATERIA.RetornaPelaModalidadeSerie(LoginAuxili.CO_EMP, modalidade, serie)
                                     join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                     select new { tb02.CO_MAT, tb107.NO_MATERIA }).OrderBy( m => m.NO_MATERIA );

            ddlMateria.DataTextField = "NO_MATERIA";
            ddlMateria.DataValueField = "CO_MAT";
            ddlMateria.DataBind();

            if (ddlMateria.Items.Count <= 0) 
                ddlMateria.Items.Clear();

            ddlMateria.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if ((modalidade != 0) && (serie != 0))
            {
//------------> Faz a verificação para saber se o tipo de frequência é por matéria ou por dia                
                string strTipoFrequencia = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb01.CO_MODU_CUR == modalidade && tb01.CO_CUR == serie
                                            select new { tb01.CO_PARAM_FREQ_TIPO }).FirstOrDefault().CO_PARAM_FREQ_TIPO;

                liDisciplina.Visible = strTipoFrequencia == "M";

                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new { tb06.TB129_CADTURMAS.CO_SIGLA_TURMA, tb06.CO_TUR }).OrderBy(t => t.CO_SIGLA_TURMA);

                ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();

            ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
        }

        ///// <summary>
        ///// Método que carrega o dropdown de Turma
        ///// </summary>
        //private void CarregaTurmaFreq()
        //{
        //    int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
        //    int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;

        //    if (coAlu != 0)
        //    {
        //        var turmasTransfDestino = (from lTbTransfInterna in TB_TRANSF_INTERNA.RetornaTodosRegistros()
        //                                   where lTbTransfInterna.TB07_ALUNO.CO_ALU == coAlu && lTbTransfInterna.TB07_ALUNO.CO_EMP == LoginAuxili.CO_EMP
        //                                   && lTbTransfInterna.CO_CURSO_DESTI == serie
        //                                   select new { CO_TURMA = lTbTransfInterna.CO_TURMA_DESTI });

        //        var turmasTransfAtual = (from lTbTransfInterna in TB_TRANSF_INTERNA.RetornaTodosRegistros()
        //                                 where lTbTransfInterna.TB07_ALUNO.CO_ALU == coAlu && lTbTransfInterna.TB07_ALUNO.CO_EMP == LoginAuxili.CO_EMP
        //                                 && lTbTransfInterna.CO_CURSO_DESTI == serie
        //                                 select new { CO_TURMA = lTbTransfInterna.CO_TURMA_ATUAL });

        //        if (turmasTransfAtual.FirstOrDefault() != null && turmasTransfDestino.FirstOrDefault() != null)
        //        {
        //            var resultado = (from turmaAtual in turmasTransfAtual
        //                             select new { turmaAtual.CO_TURMA }).Distinct();

        //            resultado = (from turmaDestino in turmasTransfDestino
        //                         select new { turmaDestino.CO_TURMA }).Distinct();

        //            var resultado2 = (from result in resultado
        //                              join tb06 in TB06_TURMAS.RetornaTodosRegistros() on result.CO_TURMA equals tb06.CO_TUR
        //                              where tb06.CO_EMP == LoginAuxili.CO_EMP
        //                              select new { result.CO_TURMA, tb06.TB129_CADTURMAS.CO_SIGLA_TURMA }).Distinct().OrderBy(r => r.CO_SIGLA_TURMA);

        //            ddlTurmaFreq.Items.Clear();

        //            ddlTurmaFreq.DataSource = resultado2;

        //            ddlTurmaFreq.DataTextField = "CO_SIGLA_TURMA";
        //            ddlTurmaFreq.DataValueField = "CO_TURMA";
        //            ddlTurmaFreq.DataBind();
        //        }
        //        else
        //        {
        //            ddlTurmaFreq.Items.Clear();
        //            if(ddlTurma.SelectedValue != "")
        //                ddlTurmaFreq.Items.Insert(0, new ListItem(ddlTurma.SelectedItem.Text, ddlTurma.SelectedValue));
        //        }
        //    }
        //    else
        //        ddlTurmaFreq.Items.Clear();

        //    ddlTurmaFreq.Items.Insert(0, new ListItem("Selecione", ""));
        //    if (ddlTurmaFreq.Items.Count > 1)
        //    {
        //        ddlTurmaFreq.SelectedValue = ddlTurma.SelectedValue;
        //        CarregaDataFrequencia();
        //    }
        //}

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string ano = ddlAno.SelectedValue.Trim();

            ddlAluno.Items.Clear();

            if (turma != 0)
            {
                ddlAluno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                       where (modalidade != 0 ? tb08.TB44_MODULO.CO_MODU_CUR == modalidade : modalidade == 0)
                                       && (serie != 0 ? tb08.CO_CUR == serie : serie == 0) && tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                       && (turma != 0 ? tb08.CO_TUR == turma : turma == 0)
                                       select new { tb08.CO_ALU, tb08.TB07_ALUNO.NO_ALU }).OrderBy( m => m.NO_ALU );

                ddlAluno.DataTextField = "NO_ALU";
                ddlAluno.DataValueField = "CO_ALU";
                ddlAluno.DataBind();
            }

            ddlAluno.Items.Insert(0, new ListItem("Selecione", ""));
            CarregaMatriculaAluno();
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            ddlAno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                 where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                 select new { CO_ANO_MES_MAT = tb08.CO_ANO_MES_MAT.Trim() }).OrderByDescending( m => m.CO_ANO_MES_MAT ).Distinct();

            ddlAno.DataTextField = "CO_ANO_MES_MAT";
            ddlAno.DataValueField = "CO_ANO_MES_MAT";
            ddlAno.DataBind();
        }

        //====> Método que carrega o DropDown de Medidas
        private void CarregaMedidas()
        {
            var tipo = "B";

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb25.TB83_PARAMETROReference.Load();
            if (tb25.TB83_PARAMETRO != null)
                tipo = tb25.TB83_PARAMETRO.TP_PERIOD_AVAL;

            AuxiliCarregamentos.CarregaMedidasTemporais(ddlBimestre, false, tipo, false, true);
        }
        private void CarregaQtdDias() {
            for (int i = 1; i < 61; i++)
            {
                ddlQtdDias.Items.Insert(i-1, new ListItem(i.ToString(), i.ToString()));
            }            
        }

        #endregion

        protected void ddlFrequencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitaAtestadoEJustificativa();
        }

        protected void ddlDataFrequencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            decimal ano = Decimal.Parse(ddlAno.SelectedValue.Trim());
            int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;

            string flagFreq = (from lTb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                               where lTb132.CO_ANO_REFER_FREQ_ALUNO == ano && lTb132.TB01_CURSO.CO_EMP == LoginAuxili.CO_EMP && lTb132.TB07_ALUNO.CO_ALU == coAlu
                               && lTb132.TB01_CURSO.CO_MODU_CUR == modalidade && lTb132.TB01_CURSO.CO_CUR == serie && lTb132.CO_TUR == turma
                               select new { lTb132.CO_FLAG_FREQ_ALUNO }).FirstOrDefault().CO_FLAG_FREQ_ALUNO;

            ddlFrequencia.SelectedValue = flagFreq != "" ? flagFreq : "S";
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
            CarregaMaterias();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaAluno();
            CarregaMaterias();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
            CarregaDataFrequencia();
            //ddlTurmaFreq.Items.Clear();
        }

        protected void ddlResponsavel_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMatriculaResponsavel();
        }

        protected void ddlAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMatriculaAluno();
            //CarregaTurmaFreq();
        }

        protected void ddlMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0;

            if (modalidade == 0 || serie == 0 || turma == 0 || materia != 0)
                return;

            var resultado = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                             where tb119.CO_EMP == LoginAuxili.CO_EMP && tb119.CO_MODU_CUR == modalidade && tb119.CO_CUR == serie
                             && tb119.CO_TUR == turma && tb119.CO_MAT == materia
                             select new { tb119.DT_ATIV_REAL }).ToList().Distinct();

            var resultado2 = (from result in resultado
                              select new { DT_ATIV_REAL = result.DT_ATIV_REAL.ToString("dd/MM/yyyy") }).OrderBy( r => r.DT_ATIV_REAL );

            ddlDataFrequencia.DataSource = resultado2;

            ddlDataFrequencia.DataTextField = "DT_ATIV_REAL";
            ddlDataFrequencia.DataValueField = "DT_ATIV_REAL";
            ddlDataFrequencia.DataBind();

            ddlDataFrequencia.Items.Insert(0, new ListItem("Selecione", ""));
        }

        //protected void ddlTurmaFreq_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CarregaDataFrequencia();
        //}

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaMaterias();
            CarregaAluno();
        }
    }
}

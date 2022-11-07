//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE TRANSFERÊNCIA ESCOLAR
// OBJETIVO: REGISTRO DE TRANSFERÊNCIA DE ALUNOS ENTRE TURMAS DA MESMA ESCOLA
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
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3300_CtrlTransferenciaEscolar.F3301_RegistroTransfAlunoTurma
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
            if (!IsPostBack)
            {
                string dataAtual = DateTime.Now.Date.ToString("dd/MM/yyyy");

                txtDtEfetivacao.Text = txtDtTransf.Text = dataAtual;
                CarregaFuncionario();
                CarregaAnos();
                CarregaModalidades();
                ddlAluno.Items.Insert(0, new ListItem("Selecione", ""));
                ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
                ddlTurmaO.Items.Insert(0, new ListItem("Selecione", ""));
                ddlTurmaD.Items.Insert(0, new ListItem("Selecione", ""));
                hdCodTran.Value = "";
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (ddlTurmaD.Enabled)
            {
                int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                int turmaOrigem = ddlTurmaO.SelectedValue != "" ? int.Parse(ddlTurmaO.SelectedValue) : 0;
                int turmaDestino = ddlTurmaD.SelectedValue != "" ? int.Parse(ddlTurmaD.SelectedValue) : 0;
                string strMatricula;
                TB129_CADTURMAS CadTurmaDestino = TB129_CADTURMAS.RetornaPelaChavePrimaria(turmaDestino);
                if (CadTurmaDestino == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não foi possível localizar a turma de destino.");
                    return;
                }
                int coEmpUnidCont = CadTurmaDestino.CO_EMP_UNID_CONT;
                string anoStr = ddlAno.SelectedValue != "" ? ddlAno.SelectedValue : DateTime.Now.Year.ToString();
                //try
                //{
                //--------> Faz a alteração da turma da matrícula do aluno
                TB08_MATRCUR tb08 = (from iTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                     where iTb08.CO_CUR == serie
                                     && iTb08.CO_ALU == coAlu
                                     && iTb08.CO_ANO_MES_MAT == anoStr
                                     select iTb08).First();

                strMatricula = tb08.CO_ALU_CAD;
                tb08.CO_TUR = turmaDestino;
                tb08.CO_EMP_UNID_CONT = coEmpUnidCont;

                TB08_MATRCUR.SaveOrUpdate(tb08, false);

                //--------> Salva o registro na TB_TRANSF_INTERNA
                DateTime dataEfetivacao = DateTime.MinValue;
                if (txtDtEfetivacao.Text != "")
                    DateTime.TryParse(txtDtEfetivacao.Text, out dataEfetivacao);

                // ESTA PARTE DO CÓDIGO FOI COMENTADO PARA PERMITIR A TRANSFERÊNCIA DE ALUNOS EM QUALQUER DATA, IDEPENDENTE DE EXISTIR OU NÃO TRANSFERÊNCIAS RECENTES
                //int ocorTransInt = (from lTbTransfInterna in TB_TRANSF_INTERNA.RetornaTodosRegistros()
                //                    where lTbTransfInterna.TB07_ALUNO.CO_ALU == coAlu
                //                    && lTbTransfInterna.TB07_ALUNO.CO_EMP == LoginAuxili.CO_EMP
                //                    && lTbTransfInterna.CO_MATRI_ATUAL == strMatricula
                //                    && lTbTransfInterna.DT_EFETI_TRANSF >= dataEfetivacao
                //                    select new { lTbTransfInterna.CO_MATRI_ATUAL }).Count();

                //if (ocorTransInt > 0 && hdCodTran.Value == "")
                //{
                //    AuxiliPagina.EnvioMensagemErro(this, "Data Inválida, existe transferência recente.");
                //    return;
                //}

                TB_TRANSF_INTERNA tbTranfInterna;

                var aluno = TB07_ALUNO.RetornaPeloCoAlu(coAlu);

                if (hdCodTran.Value != "")
                {
                    int nuREf = Convert.ToInt32(hdCodTran.Value);

                    tbTranfInterna = (from t in TB_TRANSF_INTERNA.RetornaTodosRegistros()
                                      where t.NU_REF_TRANSF == nuREf
                                      select t).FirstOrDefault();
                }
                else
                {
                    tbTranfInterna = new TB_TRANSF_INTERNA();

                    tbTranfInterna.TB07_ALUNO = aluno;
                    tbTranfInterna.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);

                }

                tbTranfInterna.CO_CURSO_ATUAL = serie;
                tbTranfInterna.CO_CURSO_DESTI = serie;
                tbTranfInterna.CO_MATRI_ATUAL = strMatricula;
                tbTranfInterna.CO_MATRI_DESTI = strMatricula;
                tbTranfInterna.CO_TURMA_ATUAL = turmaOrigem;
                tbTranfInterna.CO_TURMA_DESTI = turmaDestino;
                tbTranfInterna.CO_UNIDA_ATUAL = tbTranfInterna.CO_UNIDA_DESTI = tbTranfInterna.CO_UNIDA_REGIST = LoginAuxili.CO_EMP;
                tbTranfInterna.DE_OBSER_TRANSF = txtObs.Text;
                tbTranfInterna.DT_EFETI_TRANSF = tbTranfInterna.DT_REGIS_TRANSF = DateTime.Now;
                aluno.TB25_EMPRESA1Reference.Load();
                tbTranfInterna.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(aluno.TB25_EMPRESA1.CO_EMP);

                TB_TRANSF_INTERNA.SaveOrUpdate(tbTranfInterna, false);

                //--------> Faz a alteração da turma do Histórico do Aluno
                var lstTb079 = (from lTb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                where lTb079.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                && lTb079.CO_ALU == coAlu
                                && lTb079.CO_MODU_CUR == modalidade
                                && lTb079.CO_CUR == serie
                                && lTb079.CO_ANO_REF == anoStr
                                select lTb079);

                TB079_HIST_ALUNO tb079;

                foreach (var iTb079 in lstTb079)
                {
                    tb079 = (from h in TB079_HIST_ALUNO.RetornaTodosRegistros()
                             where h.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                 && h.CO_ALU == coAlu
                                 && h.CO_MODU_CUR == modalidade
                                 && h.CO_CUR == serie
                                 && h.CO_ANO_REF == anoStr
                                 && h.CO_MAT == iTb079.CO_MAT
                             select h).FirstOrDefault();


                    tb079.CO_TUR = turmaDestino;

                    TB079_HIST_ALUNO.SaveOrUpdate(tb079, false);
                }

                //--------> Faz a alteração da turma da Grade do Aluno
                var lstTb48 = (from lTb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                               where lTb48.CO_ALU == coAlu
                               && lTb48.CO_ANO_MES_MAT == anoStr
                               && lTb48.CO_CUR == serie
                               && lTb48.CO_MODU_CUR == modalidade
                               select lTb48);

                TB48_GRADE_ALUNO tb48;

                foreach (var iTb48 in lstTb48)
                {
                    tb48 = (from lTb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                            where lTb48.CO_ALU == iTb48.CO_ALU
                            && lTb48.CO_ANO_MES_MAT == iTb48.CO_ANO_MES_MAT
                            && lTb48.CO_CUR == iTb48.CO_CUR
                            && lTb48.CO_MODU_CUR == iTb48.CO_MODU_CUR
                            && lTb48.CO_MAT == iTb48.CO_MAT
                            select lTb48).FirstOrDefault();

                    tb48.CO_TUR = turmaDestino;

                    TB48_GRADE_ALUNO.SaveOrUpdate(tb48, false);
                }

                //--------> Faz a alteração dos titulos da Grade do Aluno
                var lstTb47 = (from lTb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                               where lTb47.CO_ALU == coAlu
                               && lTb47.CO_ANO_MES_MAT == anoStr
                               && lTb47.CO_CUR == serie
                               && lTb47.CO_MODU_CUR == modalidade
                               select lTb47);

                TB47_CTA_RECEB tb47;

                foreach (var iTb47 in lstTb47)
                {
                    tb47 = (from lTb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                            where lTb47.CO_EMP == iTb47.CO_EMP
                            && lTb47.NU_DOC == iTb47.NU_DOC
                            && lTb47.NU_PAR == iTb47.NU_PAR
                            && lTb47.DT_CAD_DOC == iTb47.DT_CAD_DOC
                            select lTb47).FirstOrDefault();

                    tb47.CO_TUR = turmaDestino;
                    tb47.CO_EMP_UNID_CONT = coEmpUnidCont;

                    TB47_CTA_RECEB.SaveOrUpdate(iTb47, false);
                }

                //--------> Faz a alteração da Turma do Aluno
                TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);

                tb07.CO_TUR = turmaDestino;

                TB07_ALUNO.SaveOrUpdate(tb07, false);

                GestorEntities.CurrentContext.SaveChanges();

                AuxiliPagina.RedirecionaParaPaginaSucesso("Transferência Efetuada com Sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
            }
            else
            {
                if (hdCodTran.Value != "")
                {
                    int nuREf = Convert.ToInt32(hdCodTran.Value);

                    TB_TRANSF_INTERNA TranfInterna = (from t in TB_TRANSF_INTERNA.RetornaTodosRegistros()
                                      where t.NU_REF_TRANSF == nuREf
                                      select t).FirstOrDefault();
                    TranfInterna.DE_OBSER_TRANSF = txtObs.Text;
                    TB_TRANSF_INTERNA.SaveOrUpdate(TranfInterna, false);
                    GestorEntities.CurrentContext.SaveChanges();
                }
                AuxiliPagina.RedirecionaParaPaginaSucesso("Alterações salvas com Sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
            }
            //}
            //catch (Exception)
            //{
            //    AuxiliPagina.RedirecionaParaPaginaErro("Transferência não foi realizada com Sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
            //}
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            hdCodTran.Value = QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Id).ToString();
            int intNuRefTransf = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

            var tbTransfInterna = (from lTbTransfInterna in TB_TRANSF_INTERNA.RetornaTodosRegistros()
                                   join tb01 in TB01_CURSO.RetornaTodosRegistros() on lTbTransfInterna.CO_CURSO_DESTI equals tb01.CO_CUR
                                   where lTbTransfInterna.NU_REF_TRANSF == intNuRefTransf
                                   select new 
                                   {
                                       lTbTransfInterna.CO_CURSO_DESTI, tb01.CO_MODU_CUR, lTbTransfInterna.CO_TURMA_DESTI, 
                                       lTbTransfInterna.DT_EFETI_TRANSF, lTbTransfInterna.DT_REGIS_TRANSF, lTbTransfInterna.DE_OBSER_TRANSF, 
                                       lTbTransfInterna.CO_TURMA_ATUAL, lTbTransfInterna.TB07_ALUNO.CO_ALU
                                   }).FirstOrDefault();


            if (tbTransfInterna != null)
            {
                ddlModalidade.SelectedValue = tbTransfInterna.CO_MODU_CUR.ToString();
                CarregaSerieCurso(tbTransfInterna.DT_EFETI_TRANSF.Year);
                ddlSerieCurso.SelectedValue = tbTransfInterna.CO_CURSO_DESTI.ToString();
                CarregaTurmaOrigem();
                ddlTurmaO.SelectedValue = tbTransfInterna.CO_TURMA_ATUAL.ToString();
                CarregaTurmaDestino();
                ddlTurmaD.SelectedValue = tbTransfInterna.CO_TURMA_DESTI.ToString();
                CarregaAluno("D", tbTransfInterna.DT_EFETI_TRANSF.Year, tbTransfInterna.CO_ALU);
                ddlAluno.SelectedValue = tbTransfInterna.CO_ALU.ToString();
                CarregaMatricula();
                txtObs.Text = tbTransfInterna.DE_OBSER_TRANSF;
                txtDtEfetivacao.Text = tbTransfInterna.DT_EFETI_TRANSF.ToString("dd/MM/yyyy");
                txtDtTransf.Text = tbTransfInterna.DT_REGIS_TRANSF.ToString("dd/MM/yyyy");
                ddlAno.SelectedValue = tbTransfInterna.DT_EFETI_TRANSF.Year.ToString();
                ddlAno.Enabled = txtMatricula.Enabled = ddlModalidade.Enabled = 
                ddlSerieCurso.Enabled = ddlTurmaD.Enabled = ddlTurmaO.Enabled = ddlAluno.Enabled = false;  
            }                      
        }
        
        /// <summary>
        /// Método que seleciona no dropdown de Funcionários o Responsável pela transferência e adiciona no campo txtCoCol a matrícula do mesmo
        /// </summary>
        private void CarregaFuncionario()
        {
            var tb03 = (from lTb03 in TB03_COLABOR.RetornaTodosRegistros()
                        where lTb03.CO_COL.Equals(LoginAuxili.CO_COL)
                        select new { lTb03.CO_COL, lTb03.NO_COL, lTb03.CO_MAT_COL }).FirstOrDefault();

            ddlFuncionario.Items.Insert(0, new ListItem(tb03.NO_COL, tb03.CO_COL.ToString()));

            txtCoCol.Text = tb03.CO_MAT_COL.ToString().Insert(5, "-").Insert(2, ".");
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos (permite apenas o Ano atual e o Próximo Ano, se houver Turma associada)
        /// </summary>
        private void CarregaAnos()
        {
            ddlAno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                 where tb08.CO_EMP == LoginAuxili.CO_EMP
                                 select new { tb08.CO_ANO_MES_MAT }).Distinct().OrderByDescending(p => p.CO_ANO_MES_MAT);

            ddlAno.DataTextField = "CO_ANO_MES_MAT";
            ddlAno.DataValueField = "CO_ANO_MES_MAT";
            ddlAno.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso(int ano = 0)
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = ano == 0 ? ddlAno.SelectedValue : ano.ToString();

            ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                        join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                                        where tb43.TB44_MODULO.CO_MODU_CUR == modalidade 
                                        && tb43.CO_ANO_GRADE == anoGrade
                                        select new { tb01.NO_CUR, tb01.CO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

            ddlSerieCurso.DataTextField = "NO_CUR";
            ddlSerieCurso.DataValueField = "CO_CUR";
            ddlSerieCurso.DataBind();

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas de Origem
        /// </summary>
        private void CarregaTurmaOrigem()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            ddlTurmaO.DataSource = (from tb06 in TB06_TURMAS.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                    join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb06.CO_TUR equals tb129.CO_TUR
                                    where tb06.CO_CUR == serie && tb06.CO_MODU_CUR == modalidade
                                    select new { tb129.CO_SIGLA_TURMA, tb129.CO_TUR }).OrderBy( t => t.CO_SIGLA_TURMA );

            ddlTurmaO.DataTextField = "CO_SIGLA_TURMA";
            ddlTurmaO.DataValueField = "CO_TUR";
            ddlTurmaO.DataBind();

            ddlTurmaO.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas de Destino
        /// </summary>
        private void CarregaTurmaDestino()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            ddlTurmaD.DataSource = (from tb06 in TB06_TURMAS.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                    join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb06.CO_TUR equals tb129.CO_TUR
                                    where tb06.CO_CUR == serie && tb06.CO_MODU_CUR == modalidade
                                    select new { tb129.CO_SIGLA_TURMA, tb129.CO_TUR }).OrderBy( t => t.CO_SIGLA_TURMA );

            ddlTurmaD.DataTextField = "CO_SIGLA_TURMA";
            ddlTurmaD.DataValueField = "CO_TUR";
            ddlTurmaD.DataBind();

            ddlTurmaD.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        /// <param name="strTipoTurma">Tipo de turma</param>
        private void CarregaAluno(string strTipoTurma, int ano = 0, int coAlu = 0)
        {
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = 0;
            if (strTipoTurma == "O" && ddlTurmaO.SelectedValue != "")
            {
                turma = int.Parse(ddlTurmaO.SelectedValue);
            }
            else if (strTipoTurma != "O" && ddlTurmaD.SelectedValue != "")
            {
                turma = int.Parse(ddlTurmaD.SelectedValue);
            }
            string anoRef = ano == 0 ? ddlAno.SelectedValue : ano.ToString();
            ddlAluno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                   where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP 
                                   && tb08.CO_CUR == serie 
                                   && (tb08.CO_TUR == turma || (ano == 0 ? tb08.CO_TUR == turma : tb08.CO_ALU == coAlu))
                                   && tb08.CO_ANO_MES_MAT == anoRef
                                   && (strTipoTurma == "O" ? (tb08.CO_SIT_MAT == "A" || tb08.CO_SIT_MAT == "R") : strTipoTurma == "D")
                                   select new { tb08.TB07_ALUNO.NO_ALU, tb08.TB07_ALUNO.CO_ALU }).OrderBy( m => m.NO_ALU );

            ddlAluno.DataTextField = "NO_ALU";
            ddlAluno.DataValueField = "CO_ALU";
            ddlAluno.DataBind();

            ddlAluno.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega a o NU_NIS do Aluno selecionado
        /// </summary>
        private void CarregaMatricula()
        {
            string codigoAluno = ddlAluno.SelectedValue != "" ? ddlAluno.SelectedValue : "0";
            int coAlu = Convert.ToInt32(codigoAluno);
            if (coAlu > 0)
            {
                txtMatricula.Text = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                     where tb07.CO_ALU == coAlu
                                     select new { tb07.NU_NIRE }).FirstOrDefault().NU_NIRE.ToString();
            }
        }
        #endregion

        #region Eventos de componentes
        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurmaOrigem();
            CarregaTurmaDestino();
            CarregaAluno("O");
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurmaOrigem();
            CarregaTurmaDestino();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno("O");
            CarregaTurmaDestino();
            ddlTurmaD.Items.RemoveAt(ddlTurmaO.SelectedIndex);
        }

        protected void ddlAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMatricula();
        }

        #endregion
    }
}

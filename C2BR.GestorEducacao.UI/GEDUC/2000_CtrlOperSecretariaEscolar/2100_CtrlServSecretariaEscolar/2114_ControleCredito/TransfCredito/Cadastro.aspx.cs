//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE CRÉDITO
// OBJETIVO: TRANSFERÊNCIA DE CRÉDITO DE MATÉRIAS
// DATA DE CRIAÇÃO: 22/04/2014
//------------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//------------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
//           |                            | 

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
using System.Web;
using System.Data;
using System.Reflection;
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2114_ControleCredito;

namespace C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2114_ControleCredito.TransfCredito
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
            if (Session["ApresentaRelatorio"] != null)
            {
                if (int.Parse(Session["ApresentaRelatorio"].ToString()) == 1)
                {
                    AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
                    //----------------> Limpa a var de sessão com o url do relatório.
                    Session.Remove("URLRelatorio");
                    Session.Remove("ApresentaRelatorio");
                    //----------------> Limpa a ref da url utilizada para carregar o relatório.
                    PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                    isreadonly.SetValue(this.Request.QueryString, false, null);
                    isreadonly.SetValue(this.Request.QueryString, true, null);
                }
            }
            else
            {
                Session.Remove("URLRelatorio");
                Session.Remove("ApresentaRelatorio");
            }

            if (!IsPostBack)
            {
                CarregaAno();
                CarregaModalidade();
                CarregaUfs(ddlUfEndAluT);
                CarregaUfs(ddlUfRgAluT);
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB48_GRADE_ALUNO tb48;
            TB122_ALUNO_CREDI tb122;
            TB07_ALUNO tb07;
            TB108_RESPONSAVEL tb108;
            RegistroLog log = new RegistroLog();

            #region Pega as informações gerais

            int coEmp = LoginAuxili.CO_EMP;
            int coMod = int.Parse(ddlModalidade.SelectedValue);
            int coCur = int.Parse(ddlSerie.SelectedValue);
            int coTur = int.Parse(ddlTurma.SelectedValue);
            int coAlu = int.Parse(ddlAluno.SelectedValue); // Aluno que está passando o crédito
            string coAno = ddlAno.SelectedValue;

            #endregion

            #region Pega as informações do aluno

            string nomeAlu = txtNomeAluT.Text;
            string sexoAlu = ddlSexoAluT.SelectedValue;
            string cpfAlu = txtCpfAluT.Text.Replace(".", "").Replace("-", "");
            string rgAlu = txtRgAluT.Text;
            string orgRgAlu = txtOrgRgAluT.Text;
            string ufRgAlu = ddlUfRgAluT.SelectedValue;
            string nomMaeAlu = txtMaeAluT.Text;
            DateTime dtNasc;
            if (!DateTime.TryParse(txtDtNascAluT.Text, out dtNasc))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data de nascimento inválida");
                return;
            }
            string endAlu = txtEndAluT.Text;
            string compEndAlu = txtCompEndAluT.Text;
            decimal numEndAlu;
            if (!decimal.TryParse(txtEndNumAluT.Text, out numEndAlu))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Número inválido");
                return;
            }
            string ufEndAlu = ddlUfEndAluT.SelectedValue;
            int cidEndAlu = 0;
            int baiEndAlu = 0;

            int.TryParse(ddlCidEndAluT.SelectedValue, out cidEndAlu);
            int.TryParse(ddlBaiEndAluT.SelectedValue, out baiEndAlu);

            string telResAlu = txtTelAluT.Text.Replace("(", "").Replace(") ", "").Replace("-", "").Trim();
            string telCelAlu = txtCelAluT.Text.Replace("(", "").Replace(") ", "").Replace("-", "").Trim();
            string telComAlu = txtTelComAluT.Text.Replace("(", "").Replace(") ", "").Replace("-", "").Trim();
            string emailAlu = txtEmailAluT.Text;

            #endregion

            #region Grava as informações do responsável
            tb108 = TB108_RESPONSAVEL.RetornaTodosRegistros().Where(w => w.NU_CPF_RESP == cpfAlu).FirstOrDefault();
            if (tb108 == null)
            {
                tb108 = new TB108_RESPONSAVEL();
                tb108.NO_RESP = nomeAlu;
                tb108.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
                tb108.CO_INST = 51;
                tb108.CO_ORIGEM_RESP = "DF";
                tb108.FL_NEGAT_CHEQUE = "N";
                tb108.FL_NEGAT_SERASA = "N";
                tb108.FL_NEGAT_SPC = "N";
                tb108.CO_SEXO_RESP = sexoAlu;
                tb108.NU_CPF_RESP = cpfAlu;
                tb108.CO_RG_RESP = rgAlu;
                tb108.CO_ORG_RG_RESP = orgRgAlu;
                tb108.CO_ESTA_RG_RESP = ufRgAlu;
                tb108.NO_MAE_RESP = nomMaeAlu;
                tb108.DT_NASC_RESP = dtNasc;
                tb108.DE_ENDE_RESP = endAlu;
                tb108.NU_ENDE_RESP = numEndAlu;
                tb108.DE_COMP_RESP = compEndAlu;
                tb108.CO_ESTA_RESP = ufEndAlu;
                tb108.CO_BAIRRO = baiEndAlu;
                tb108.TB904_CIDADE = TB904_CIDADE.RetornaTodosRegistros().Where(w => w.CO_CIDADE == cidEndAlu).FirstOrDefault();
                tb108.NU_TELE_RESI_RESP = telResAlu;
                tb108.NU_TELE_CELU_RESP = telCelAlu;
                tb108.NU_TELE_COME_RESP = telComAlu;
                tb108.DES_EMAIL_RESP = emailAlu;
                tb108 = TB108_RESPONSAVEL.SaveOrUpdate(tb108);

                log.RegistroLOG(tb108, RegistroLog.ACAO_GRAVAR);
            }
            #endregion

            #region Grava as informações do aluno
            tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_CPF_ALU == cpfAlu).FirstOrDefault();
            int lastNuNire = (from lTb07 in TB07_ALUNO.RetornaTodosRegistros() select new { lTb07.NU_NIRE }).OrderByDescending(a => a.NU_NIRE).FirstOrDefault().NU_NIRE;
            if (tb07 == null)
            {
                tb07 = new TB07_ALUNO();
                tb07.NO_ALU = nomeAlu;
                tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                tb07.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                tb07.NU_NIRE = lastNuNire + 1;
                tb07.CO_SEXO_ALU = sexoAlu;
                tb07.NU_CPF_ALU = cpfAlu;
                tb07.CO_RG_ALU = rgAlu;
                tb07.CO_ORG_RG_ALU = orgRgAlu;
                tb07.CO_ESTA_RG_ALU = ufRgAlu;
                tb07.NO_MAE_ALU = nomMaeAlu;
                tb07.DT_NASC_ALU = dtNasc;
                tb07.DE_ENDE_ALU = endAlu;
                tb07.NU_ENDE_ALU = numEndAlu;
                tb07.DE_COMP_ALU = compEndAlu;
                tb07.CO_UF_NATU_ALU = ufEndAlu;
                tb07.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(baiEndAlu);
                tb07.NU_TELE_RESI_ALU = telResAlu;
                tb07.NU_TELE_CELU_ALU = telCelAlu;
                tb07.NU_TELE_COME_ALU = telComAlu;
                tb07.CO_SITU_ALU = "A";
                tb07.TP_DEF = "N";
                tb07.TB108_RESPONSAVEL = tb108;
                tb07 = TB07_ALUNO.SaveOrUpdate(tb07);

                log.RegistroLOG(tb07, RegistroLog.ACAO_GRAVAR);

            }
            #endregion

            int coMatGrid = 0;
            decimal vlCred = 0;
            DateTime dtCred;
            foreach (GridViewRow r in grdGradeAluno.Rows)
            {
                CheckBox chk = ((CheckBox)r.Cells[0].FindControl("ckSelect"));
                if (chk.Checked)
                {
                    coMatGrid = int.Parse(((HiddenField)r.Cells[0].FindControl("hidCoMat")).Value);
                    string vlCredT = ((TextBox)r.Cells[4].FindControl("txtVlCred")).Text;
                    if (!decimal.TryParse(vlCredT, out vlCred))
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Valor de crédito na linha " + r.RowIndex + " na grid de matérias do aluno selecionado é inválido.");
                        return;
                    }

                    string dtCredT = ((TextBox)r.Cells[5].FindControl("txtDtCred")).Text;
                    if (!DateTime.TryParse(dtCredT, out dtCred))
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Data do crédito na linha " + r.RowIndex + " na grid de matérias do aluno selecionado é inválida.");
                        return;
                    }

                    #region Altera o status da matéria na grade do aluno

                    tb48 = TB48_GRADE_ALUNO.RetornaTodosRegistros().Where(w => w.CO_EMP == coEmp && w.CO_ALU == coAlu && w.CO_MODU_CUR == coMod && w.CO_CUR == coCur && w.CO_ANO_MES_MAT == coAno && w.CO_MAT == coMatGrid).FirstOrDefault();
                    tb48.CO_FLAG_STATUS = "T";
                    tb48 = TB48_GRADE_ALUNO.SaveOrUpdate(tb48);

                    log.RegistroLOG(tb48, RegistroLog.ACAO_EDICAO);

                    #endregion

                    #region Grava as informações do crédito

                    tb122 = new TB122_ALUNO_CREDI();
                    tb122.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                    tb122.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(coMod);
                    tb122.CO_CUR = coCur;
                    tb122.CO_MAT = coMatGrid;
                    tb122.CO_ALU = coAlu;
                    tb122.CO_ALU_CRED = tb07.CO_ALU;
                    tb122.VL_CRED = vlCred;
                    tb122.DT_CRED = dtCred;
                    tb122.CO_COL = LoginAuxili.CO_COL;
                    tb122.CO_EMP_COL = LoginAuxili.CO_EMP;
                    tb122.NR_IP_COL = LoginAuxili.IP_USU;
                    tb122.DT_CAD_CRED = DateTime.Now;
                    tb122.CO_SITUA_CRED = "A";
                    tb122.DT_SITUA_CRED = DateTime.Now;
                    tb122 = TB122_ALUNO_CREDI.SaveOrUpdate(tb122);

                    log.RegistroLOG(tb122, RegistroLog.ACAO_GRAVAR);

                    #endregion
                }
            }
            AuxiliPagina.EnvioMensagemSucesso(this, "Crédito Transferido com sucesso");
        }

        private void CarregaFormulario()
        {
            ddlUfEndAluT.SelectedValue = LoginAuxili.CO_UF_EMP;
            ddlUfRgAluT.SelectedValue = LoginAuxili.CO_UF_EMP;

            CarregaCidades();
            ddlCidEndAluT.SelectedValue = LoginAuxili.CO_CIDADE_INSTITUICAO.ToString();

            CarregaBairros();
        }
        #endregion

        #region Classes de formatação de saída

        /// <summary>
        /// Classe que formata a saída da combo de ano (criada para evitar ano com espaços em branco)
        /// </summary>
        public class ComboAno
        {
            public string coAno { get; set; }
            public string ano
            {
                get
                {
                    return this.coAno.Trim();
                }
            }
        }

        /// <summary>
        /// Classe que formata a saída da grid de grade do aluno
        /// </summary>
        public class GridGradeAluno
        {
            public string noMat { get; set; }
            public string noSig { get; set; }
            public int qtCh { get; set; }
            public int coMat { get; set; }
            public string coFlag { get; set; }
            public bool chkSel { get; set; }
            public bool chkSelEna
            {
                get
                {
                    return this.coFlag == "T" ? false : true;
                }
            }
        }

        /// <summary>
        /// Classe que formata a saída da grid de grade dos cursos
        /// </summary>
        public class GridGradeCursos
        {
            public string noMat { get; set; }
            public string noCur { get; set; }
            public string nome
            {
                get
                {
                    return this.noCur + " - " + this.noMat;
                }
            }
            public string noSig { get; set; }
            public int qtCh { get; set; }
            public int coMat { get; set; }
            public int coCur { get; set; }
            public bool chkSel { get; set; }
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Mátodo que carrega a grid com a grade do aluno na grid grdGradeAluno
        /// </summary>
        protected void CarregaGridGradeAluno()
        {
            int coEmp = LoginAuxili.CO_EMP;
            int coMod = int.Parse(ddlModalidade.SelectedValue);
            int coCur = int.Parse(ddlSerie.SelectedValue);
            int coTur = int.Parse(ddlTurma.SelectedValue);
            int coAlu = int.Parse(ddlAluno.SelectedValue);
            string coAno = ddlAno.SelectedValue;

            var res = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                       join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb48.CO_MAT equals tb02.CO_MAT
                       join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                       where tb48.CO_EMP == coEmp
                       && tb48.CO_MODU_CUR == coMod
                       && tb48.CO_CUR == coCur
                       && tb48.CO_TUR == coTur
                       && tb48.CO_ALU == coAlu
                       && tb48.CO_ANO_MES_MAT == coAno
                       select new GridGradeAluno
                       {
                           noMat = tb107.NO_MATERIA,
                           noSig = tb107.NO_SIGLA_MATERIA,
                           qtCh = tb02.QT_CARG_HORA_MAT,
                           coMat = tb02.CO_MAT,
                           coFlag = tb48.CO_FLAG_STATUS,
                           chkSel = true
                       }).OrderBy(o => o.noMat);

            grdGradeAluno.DataSource = res;
            grdGradeAluno.DataBind();
        }

        /// <summary>
        /// Método que carrega a grid com as matérias de todos os cursos, menos do curso selecionado pelo usuário. Carrega a grid grdGradeCursos
        /// </summary>
        protected void CarregaGridGradeCursos()
        {
            //int coEmp = LoginAuxili.CO_EMP;
            //int coMod = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            //int coCur = ddlSerie.SelectedValue != "" ? int.Parse(ddlSerie.SelectedValue) : 0;
            //int coTur = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            //int coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            //int coCurExt = ddlCurExt.SelectedValue != "" ? int.Parse(ddlCurExt.SelectedValue) : 0;
            //string coAno = ddlAno.SelectedValue;

            //if (coCurExt == 0)
            //{
            //    var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
            //               join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
            //               join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
            //               join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb02.CO_CUR equals tb01.CO_CUR
            //               where tb43.CO_ANO_GRADE == coAno
            //               && tb43.CO_CUR != coCur
            //               select new GridGradeCursos
            //               {
            //                   noMat = tb107.NO_MATERIA,
            //                   noSig = tb107.NO_SIGLA_MATERIA,
            //                   qtCh = tb02.QT_CARG_HORA_MAT,
            //                   coMat = tb02.CO_MAT,
            //                   noCur = tb01.NO_CUR,
            //                   coCur = tb01.CO_CUR
            //               }).Distinct().ToList();

            //    grdGradeCrusos.DataSource = res;
            //    grdGradeCrusos.DataBind();
            //}
            //else
            //{
            //    var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
            //               join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
            //               join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
            //               join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb02.CO_CUR equals tb01.CO_CUR
            //               where tb43.CO_ANO_GRADE == coAno
            //               && tb43.CO_CUR == coCurExt
            //               select new GridGradeCursos
            //               {
            //                   noMat = tb107.NO_MATERIA,
            //                   noSig = tb107.NO_SIGLA_MATERIA,
            //                   qtCh = tb02.QT_CARG_HORA_MAT,
            //                   coMat = tb02.CO_MAT,
            //                   noCur = tb01.NO_CUR,
            //                   coCur = tb01.CO_CUR
            //               }).Distinct().ToList();

            //    grdGradeCrusos.DataSource = res;
            //    grdGradeCrusos.DataBind();
            //}
        }

        /// <summary>
        /// Mátodo que carrega os anos na combo ddlAno
        /// </summary>
        protected void CarregaAno()
        {
            var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                       select new ComboAno
                       {
                           coAno = tb43.CO_ANO_GRADE
                       }).Distinct().OrderByDescending(o => o.coAno);

            ddlAno.DataTextField = "ano";
            ddlAno.DataValueField = "ano";

            ddlAno.DataSource = res;
            ddlAno.DataBind();
        }

        /// <summary>
        /// Método que carrega as modalidades na combo de ddlModalidade
        /// </summary>
        protected void CarregaModalidade()
        {
            int coOrg = LoginAuxili.ORG_CODIGO_ORGAO;

            var res = (from tb44 in TB44_MODULO.RetornaTodosRegistros()
                       where tb44.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == coOrg
                       select new
                       {
                           tb44.DE_MODU_CUR,
                           tb44.CO_MODU_CUR
                       });

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";

            ddlModalidade.DataSource = res;
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Selecinoe", "0"));
        }

        /// <summary>
        /// Método que carrega os cursos na combo ddlSerie
        /// </summary>
        protected void CarregaSerie()
        {
            int coEmp = LoginAuxili.CO_EMP;
            int coMod = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            var res = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                       where tb01.CO_EMP == coEmp
                       && tb01.CO_MODU_CUR == coMod
                       select new
                       {
                           tb01.NO_CUR,
                           tb01.CO_CUR
                       });

            ddlSerie.DataTextField = "NO_CUR";
            ddlSerie.DataValueField = "CO_CUR";

            ddlSerie.DataSource = res;
            ddlSerie.DataBind();

            ddlSerie.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        /// <summary>
        /// Método que carrega as matérias dos cursos, fora o curso que o usuário selecionou nos filtros
        /// </summary>
        protected void CarregaSerieExt()
        {
            //int coCur = ddlSerie.SelectedValue != "" ? int.Parse(ddlSerie.SelectedValue) : 0;

            //var res = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
            //           where tb01.CO_CUR != coCur
            //           select new
            //           {
            //               tb01.NO_CUR,
            //               tb01.CO_CUR
            //           });

            //ddlCurExt.DataTextField = "NO_CUR";
            //ddlCurExt.DataValueField = "CO_CUR";

            //ddlCurExt.DataSource = res;
            //ddlCurExt.DataBind();

            //ddlCurExt.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Método que carrega as turmas na combo ddlTurma
        /// </summary>
        protected void CarregaTurma()
        {
            int coEmp = LoginAuxili.CO_EMP;
            int coMod = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coCur = ddlSerie.SelectedValue != "" ? int.Parse(ddlSerie.SelectedValue) : 0;

            var res = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                       join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb06.CO_TUR equals tb129.CO_TUR
                       where tb06.CO_EMP == coEmp
                       && tb06.CO_MODU_CUR == coMod
                       && tb06.CO_CUR == coCur
                       select new
                       {
                           tb129.NO_TURMA,
                           tb06.CO_TUR
                       });

            ddlTurma.DataTextField = "NO_TURMA";
            ddlTurma.DataValueField = "CO_TUR";

            ddlTurma.DataSource = res;
            ddlTurma.DataBind();

            ddlTurma.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        /// <summary>
        /// Método que carrega os alunos na combo ddlAluno
        /// </summary>
        protected void CarregaAluno()
        {
            int coEmp = LoginAuxili.CO_EMP;
            int coMod = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int coCur = ddlSerie.SelectedValue != "" ? int.Parse(ddlSerie.SelectedValue) : 0;
            int coTur = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string coAno = ddlAno.SelectedValue;

            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb07.CO_ALU equals tb08.CO_ALU
                       where tb08.CO_EMP == coEmp
                       && tb08.TB44_MODULO.CO_MODU_CUR == coMod
                       && tb08.CO_CUR == coCur
                       && tb08.CO_TUR == coTur
                       && tb08.CO_ANO_MES_MAT == coAno
                       && tb08.CO_SIT_MAT == "A"
                       select new
                       {
                           tb07.NO_ALU,
                           tb07.CO_ALU
                       }).OrderBy(o => o.NO_ALU);

            ddlAluno.DataTextField = "NO_ALU";
            ddlAluno.DataValueField = "CO_ALU";

            ddlAluno.DataSource = res;
            ddlAluno.DataBind();

            ddlAluno.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        /// <summary>
        /// Método que carrega as UFs na combo de UF do endereço do aluno (ddlUfEndAluT)
        /// </summary>
        private void CarregaUfs(DropDownList ddl)
        {
            ddl.DataSource = TB74_UF.RetornaTodosRegistros();

            ddl.DataTextField = "CODUF";
            ddl.DataValueField = "CODUF";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("", ""));

            // Atribui o valor padrão, a UF da unidade
            ddl.SelectedValue = LoginAuxili.CO_UF_EMP;
        }

        /// <summary>
        /// Método que carrega as cidades do endereço do aluno (ddlCidEndAluT)
        /// </summary>
        private void CarregaCidades()
        {
            ddlCidEndAluT.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUfEndAluT.SelectedValue);

            ddlCidEndAluT.DataTextField = "NO_CIDADE";
            ddlCidEndAluT.DataValueField = "CO_CIDADE";
            ddlCidEndAluT.DataBind();

            ddlCidEndAluT.Enabled = ddlCidEndAluT.Items.Count > 0;
            ddlCidEndAluT.Items.Insert(0, "");

            // Carrega o valor da cidade da empresa como valor default
            //ddlCidade.SelectedValue = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP).CO_CIDADE.ToString();
        }

        /// <summary>
        /// Método que carrega os bairros do endereço do aluno (ddlBaiEndAluT)
        /// </summary>
        private void CarregaBairros()
        {
            int coCidade = ddlCidEndAluT.SelectedValue != "" ? int.Parse(ddlCidEndAluT.SelectedValue) : 0;

            if (coCidade == 0)
            {
                ddlBaiEndAluT.Enabled = false;
                ddlBaiEndAluT.Items.Clear();
                return;
            }
            else
            {
                ddlBaiEndAluT.DataSource = TB905_BAIRRO.RetornaPelaCidade(coCidade);

                ddlBaiEndAluT.DataTextField = "NO_BAIRRO";
                ddlBaiEndAluT.DataValueField = "CO_BAIRRO";
                ddlBaiEndAluT.DataBind();

                ddlBaiEndAluT.Enabled = ddlBaiEndAluT.Items.Count > 0;
                ddlBaiEndAluT.Items.Insert(0, "");

                //ddlBairro.SelectedValue = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP).CO_BAIRRO.ToString();
            }
        }

        #endregion

        #region Métodos de campo

        /// <summary>
        /// Método que é executado quando uma modalidade é selecionada
        /// </summary>
        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerie();
        }

        /// <summary>
        /// Método que é executado quando a série é selecionada
        /// </summary>
        protected void ddlSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }

        /// <summary>
        /// Método que é executado quando a turma é selecionada
        /// </summary>
        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
        }

        /// <summary>
        /// Método que é executado quando o aluno é selecionado
        /// </summary>
        protected void ddlAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridGradeAluno();
        }

        /// <summary>
        /// Método executando quando uma UF, no endereço do aluno, é selecionada
        /// </summary>
        protected void ddlUfEndAluT_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades();
        }

        /// <summary>
        /// Método executado quando uma cidade, no endereço do aluno, é selecionada
        /// </summary>
        protected void ddlCidEndAluT_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairros();
        }

        /// <summary>
        /// Método executado quando o CPF do aluno é informado
        /// </summary>
        protected void txtCpfAluT_TextChanged(object sender, EventArgs e)
        {
            string cpf = txtCpfAluT.Text.Replace(".","").Replace("-","").Trim();
            TB07_ALUNO tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_CPF_ALU == cpf).FirstOrDefault();

            if (tb07 != null)
            {
                tb07.TB905_BAIRROReference.Load();
                tb07.TB108_RESPONSAVELReference.Load();

                txtNomeAluT.Text = tb07.NO_ALU;
                hidCoAluC.Value = tb07.CO_ALU.ToString();
                ddlSexoAluT.SelectedValue = tb07.CO_SEXO_ALU;
                txtCpfAluT.Text = String.Format("{0:000.000.000-00}", tb07.NU_CPF_ALU);
                txtRgAluT.Text = tb07.CO_RG_ALU;
                txtOrgRgAluT.Text = tb07.CO_ORG_RG_ALU;
                ddlUfRgAluT.SelectedValue = tb07.CO_ESTA_RG_ALU;
                txtMaeAluT.Text = tb07.NO_MAE_ALU;
                txtDtNascAluT.Text = tb07.DT_NASC_ALU != null ? tb07.DT_NASC_ALU.Value.ToString("dd/MM/yyyy") : "";
                txtEndAluT.Text = tb07.DE_ENDE_ALU;
                txtCompEndAluT.Text = tb07.DE_COMP_ALU;
                txtEndNumAluT.Text = tb07.NU_ENDE_ALU.ToString();
                ddlUfEndAluT.SelectedValue = tb07.CO_UF_NATU_ALU;

                CarregaCidades();
                ddlCidEndAluT.SelectedValue = tb07.TB905_BAIRRO.CO_CIDADE.ToString();

                CarregaBairros();
                ddlBaiEndAluT.SelectedValue = tb07.TB905_BAIRRO.CO_BAIRRO.ToString();
                txtTelAluT.Text = String.Format("{0:(###) ####-####}", tb07.NU_TELE_RESI_ALU);
                txtCelAluT.Text = String.Format("{0:(###) ####-####}", tb07.NU_TELE_CELU_ALU);
                txtTelComAluT.Text = String.Format("{0:(###) ####-####}", tb07.NU_TELE_COME_ALU);
                txtEmailAluT.Text = tb07.TB108_RESPONSAVEL.DES_EMAIL_RESP;

                txtNomeAluT.Enabled = false;
                ddlSexoAluT.Enabled = false;
                txtRgAluT.Enabled = false;
                txtOrgRgAluT.Enabled = false;
                ddlUfRgAluT.Enabled = false;
                txtMaeAluT.Enabled = false;
                txtDtNascAluT.Enabled = false;
                txtEndAluT.Enabled = false;
                txtEndNumAluT.Enabled = false;
                txtCompEndAluT.Enabled = false;
                ddlUfEndAluT.Enabled = false;
                ddlCidEndAluT.Enabled = false;
                ddlBaiEndAluT.Enabled = false;
                txtTelAluT.Enabled = false;
                txtCelAluT.Enabled = false;
                txtTelComAluT.Enabled = false;
                txtEmailAluT.Enabled = false;
            }
            else
            {
                hidCoAluC.Value = "";
                txtNomeAluT.Enabled = true;
                ddlSexoAluT.Enabled = true;
                txtRgAluT.Enabled = true;
                txtOrgRgAluT.Enabled = true;
                ddlUfRgAluT.Enabled = true;
                txtMaeAluT.Enabled = true;
                txtDtNascAluT.Enabled = true;
                txtEndAluT.Enabled = true;
                txtEndNumAluT.Enabled = true;
                txtCompEndAluT.Enabled = true;
                ddlUfEndAluT.Enabled = true;
                ddlCidEndAluT.Enabled = true;
                ddlBaiEndAluT.Enabled = true;
                txtTelAluT.Enabled = true;
                txtCelAluT.Enabled = true;
                txtTelComAluT.Enabled = true;
                txtEmailAluT.Enabled = true;
            }
        }

        /// <summary>
        /// Método executado quando uma matéria na grid é selecionada
        /// </summary>
        protected void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow l in grdGradeAluno.Rows)
            {
                CheckBox chk = ((CheckBox)l.Cells[0].FindControl("ckSelect"));
                if (chk.Checked)
                {
                    ((TextBox)l.Cells[4].FindControl("txtVlCred")).Text = "";
                    ((TextBox)l.Cells[4].FindControl("txtVlCred")).Enabled = true;
                    ((TextBox)l.Cells[5].FindControl("txtDtCred")).Text = "";
                    ((TextBox)l.Cells[5].FindControl("txtDtCred")).Enabled = true;
                }
                else
                {
                    ((TextBox)l.Cells[4].FindControl("txtVlCred")).Text = "";
                    ((TextBox)l.Cells[4].FindControl("txtVlCred")).Enabled = false;
                    ((TextBox)l.Cells[5].FindControl("txtDtCred")).Text = "";
                    ((TextBox)l.Cells[5].FindControl("txtDtCred")).Enabled = false;
                }
            }
        }

        /// <summary>
        /// Método executado quando a grid de matérias da grade do aluno é carregada
        /// </summary>
        protected void grdGradeAluno_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("ckSelect");

                if (chk.Enabled == false)
                    e.Row.ControlStyle.BackColor = System.Drawing.Color.FromArgb(251, 233, 183);
            }
        }

        protected void lnkImpGuia_Click(object sender, EventArgs e)
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno, coEmp, coAlu, coAluC;

            //--------> Variáveis de parâmetro do Relatório
            string infos, parametros;

            DateTime dtIni, dtFim;

            //--------> Inicializa as variáveis
            coEmp = LoginAuxili.CO_EMP;
            coAlu = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0;
            if (coAlu == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Aluno Inválido");
                return;
            }
            coAluC = hidCoAluC.Value != "" ? int.Parse(hidCoAluC.Value) : 0;
            if (coAluC == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Beneficiário Inválido");
                return;
            }
            dtIni = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy")); // Pega a data atual sem o horário, para não interferir no SQL
            dtFim = DateTime.Now;
            parametros = "Unidade: " + TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp).NO_FANTAS_EMP +
                " - Aluno: " + TB07_ALUNO.RetornaPelaChavePrimaria(coAlu, coEmp).NO_ALU +
                " - Beneficiário: " + TB07_ALUNO.RetornaPelaChavePrimaria(coAluC, coEmp).NO_ALU +
                " - Período: de " + dtIni.ToString("dd/MM/yyyy") + " à " + dtFim.ToString("dd/MM/yyyy");
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptExtratoTransfCredi rpt = new RptExtratoTransfCredi();

            lRetorno = rpt.InitReport(parametros, coEmp, infos, coAlu, coAluC, dtIni, dtFim, LoginAuxili.CO_COL);
            if (lRetorno == 1)
            {
                Session["Report"] = rpt;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";
                HttpContext.Current.Session["ApresentaRelatorio"] = 1;
            }
            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }
        #endregion
    }
}
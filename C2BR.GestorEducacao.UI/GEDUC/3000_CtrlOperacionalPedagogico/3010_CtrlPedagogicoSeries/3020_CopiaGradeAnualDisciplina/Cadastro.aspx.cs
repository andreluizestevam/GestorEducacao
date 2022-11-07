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
// 05/01/2014| Maxwell Almeida           | Criação da funcionalidade com o intuito de copiar a grade anual de outros anos, agindo como facilitador

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
using System.Data.Objects;
using C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_CtrlConsultas;

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3010_CtrlPedagogicoSeries._3020_CopiaGradeAnualDisciplina
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
                txtAnoDesti.Text = DateTime.Now.Year.ToString();
                CarregaAno();
                CarregaModalidade(ddlModaOri);
                CarregaModalidade(ddlModalDesti);
                CarregaCurso(ddlCursoOri, 0);
                CarregaCurso(ddlCursoDesti, 0);

                CarregaParametros();
            }
        }

        #endregion

        #region Carregamento

        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            bool Selec = false;
            foreach (GridViewRow li in grdGradeOrigem.Rows)
            {
                if (((CheckBox)li.Cells[0].FindControl("chkSelectGridOrigem")).Checked)
                {
                    Selec = true;
                    break;
                }
            }

            //Apresenta erro caso nenhuma disciplina da grade de origem tenha sido selecionada
            if (!Selec)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso selecionar ao menos uma disciplina de Origem!");
            }


            //Faz as validações
            if (string.IsNullOrEmpty(txtAnoDesti.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Ano de destino deve ser informado!");
                return;
            }
            if (string.IsNullOrEmpty(ddlModalDesti.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de destino deve ser informada!");
                return;
            }
            if (string.IsNullOrEmpty(ddlCursoDesti.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Curso de destino deve ser informado!");
                return;
            }

            //Percorre todos os itens selecionados na grade de origem e realiza as cópias
            foreach (GridViewRow lis in grdGradeOrigem.Rows)
            {
                string CO_ANO = (((HiddenField)lis.Cells[0].FindControl("hidCoAno")).Value);
                int CO_EMP = int.Parse((((HiddenField)lis.Cells[0].FindControl("hidCoEmp")).Value));
                int CO_CUR = int.Parse((((HiddenField)lis.Cells[0].FindControl("hidCoCur")).Value));
                int CO_MAT = int.Parse((((HiddenField)lis.Cells[0].FindControl("hidCoMat")).Value));
                var tb43Origem = TB43_GRD_CURSO.RetornaPelaChavePrimaria(CO_EMP, CO_ANO, CO_CUR, CO_MAT);
                tb43Origem.TB44_MODULOReference.Load();

                //Dados inseridos para destino
                int coModu = int.Parse(ddlModalDesti.SelectedValue);
                int coCur = int.Parse(ddlCursoDesti.SelectedValue);
                string ano = txtAnoDesti.Text;

                //Executará a cópia apenas se a disciplina ainda não estiver na grade do curso destino
                if (tb43Origem != null)
                {
                    //copia as informacoes do objeto instanciado anteriormente para o novo objeto que sera persistido
                    TB43_GRD_CURSO tb43 = new TB43_GRD_CURSO();
                    tb43.CO_EMP = tb43Origem.CO_EMP;

                    #region Informações da grade de Destino
                    tb43.CO_ANO_GRADE = ano;
                    tb43.CO_CUR = coCur;
                    tb43.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(coModu);
                    #endregion

                    tb43.NU_SEM_GRADE = tb43Origem.NU_SEM_GRADE;
                    //tb43.CO_CUR = tb43Origem.CO_CUR;
                    tb43.CO_MAT = tb43Origem.CO_MAT;
                    tb43.CO_SITU_MATE_GRC = tb43Origem.CO_SITU_MATE_GRC;
                    tb43.DT_SITU_MATE_GRC = tb43Origem.DT_SITU_MATE_GRC;
                    tb43.QTDE_AULA_SEM = tb43Origem.QTDE_AULA_SEM;
                    tb43.QTDE_CH_SEM = tb43Origem.QTDE_CH_SEM;
                    tb43.ID_MATER_AGRUP = tb43Origem.ID_MATER_AGRUP;
                    tb43.QT_AULAS_BIM1 = tb43Origem.QT_AULAS_BIM1;
                    tb43.QT_AULAS_BIM2 = tb43Origem.QT_AULAS_BIM2;
                    tb43.QT_AULAS_BIM3 = tb43Origem.QT_AULAS_BIM3;
                    tb43.QT_AULAS_BIM4 = tb43Origem.QT_AULAS_BIM4;
                    tb43.CO_MATER_ANTER = tb43Origem.CO_MATER_ANTER;
                    tb43.CO_MATER_POSTE = tb43Origem.CO_MATER_POSTE;
                    tb43.CO_ORDEM_IMPRE = tb43Origem.CO_ORDEM_IMPRE;
                    tb43.FL_DISCI_AGRUPA = tb43Origem.FL_DISCI_AGRUPA;
                    tb43.VL_NOTA_MAXIM = tb43Origem.VL_NOTA_MAXIM;
                    tb43.VL_NOTA_MAXIM_PROVA = tb43Origem.VL_NOTA_MAXIM_PROVA;
                    tb43.VL_NOTA_MAXIM_SIMUL = tb43Origem.VL_NOTA_MAXIM_SIMUL;
                    tb43.VL_NOTA_MAXIM_ATIVI = tb43Origem.VL_NOTA_MAXIM_ATIVI;
                    tb43.FL_NOTA1_MEDIA = tb43Origem.FL_NOTA1_MEDIA;
                    tb43.FL_LANCA_NOTA = tb43Origem.FL_LANCA_NOTA;
                    tb43.FL_LANCA_FREQU = tb43Origem.FL_LANCA_FREQU;
                    TB43_GRD_CURSO.SaveOrUpdate(tb43, true);
                }
            }

            var parametros = ddlAnoOri.SelectedValue + ";" + ddlModaOri.SelectedValue + ";" + ddlCursoOri.SelectedValue + ";" + txtAnoDesti.Text + ";"
                     + ddlModalDesti.SelectedValue + ";" + ddlCursoDesti.SelectedValue;

            HttpContext.Current.Session["BuscaSuperiorCPGA"] = parametros;

            AuxiliPagina.RedirecionaParaPaginaSucesso("Itens selecionados copiados com exito!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
        }

        private void CarregaParametros()
        {
            if (HttpContext.Current.Session["BuscaSuperiorCPGA"] != null)
            {
                var parametros = HttpContext.Current.Session["BuscaSuperiorCPGA"].ToString();

                if (!string.IsNullOrEmpty(parametros))
                {
                    var par = parametros.ToString().Split(';');
                    var anoOri = par[0];
                    var modalidadeOri = par[1];
                    var cursoOri = par[2];
                    var anoDest = par[3];
                    var modalidadeDesti = par[4];
                    var cursoDesti = par[5];

                    ddlAnoOri.SelectedValue = anoOri;
                    ddlModaOri.SelectedValue = modalidadeOri;
                    CarregaCurso(ddlCursoOri, (ddlModaOri.SelectedValue != "" ? int.Parse(ddlModaOri.SelectedValue) : 0));
                    ddlCursoOri.SelectedValue = cursoOri;

                    txtAnoDesti.Text = anoDest;
                    ddlModalDesti.SelectedValue = modalidadeDesti;
                    CarregaCurso(ddlCursoDesti, (ddlModalDesti.SelectedValue != "" ? int.Parse(ddlModalDesti.SelectedValue) : 0));
                    ddlCursoDesti.SelectedValue = cursoDesti;

                    HttpContext.Current.Session.Remove("BuscaSuperiorCPGA");
                    
                    CarregaGradeCursoOrigem(grdGradeOrigem, anoOri, int.Parse(modalidadeOri), int.Parse(cursoOri));
                    CarregaGradeCursoOrigem(grdGradeDestino, anoDest, int.Parse(modalidadeDesti), int.Parse(cursoDesti));
                }
            }
        }

        /// <summary>
        /// Carrega os Anos
        /// </summary>
        private void CarregaAno()
        {
            AuxiliCarregamentos.CarregaAnoGrdCurs(ddlAnoOri, LoginAuxili.CO_EMP, false);
        }

        /// <summary>
        /// Carrega as Modalidades
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregaModalidade(DropDownList ddl)
        {
            AuxiliCarregamentos.carregaModalidades(ddl, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }

        /// <summary>
        /// Carrega os Cursos
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="CO_MODU_CUR"></param>
        private void CarregaCurso(DropDownList ddl, int CO_MODU_CUR)
        {
            AuxiliCarregamentos.carregaSeriesCursos(ddl, CO_MODU_CUR, LoginAuxili.CO_EMP, false);
        }

        /// <summary>
        /// Carrega a Grade do curso de Origem
        /// </summary>
        private void CarregaGradeCursoOrigem(GridView GRD, string ano, int modalidade, int curso)
        {
            var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                       where tb43.CO_EMP == LoginAuxili.CO_EMP
                       && tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                       && tb43.CO_CUR == curso
                       && tb43.CO_ANO_GRADE == ano

                       join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                       join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                       join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb43.CO_CUR equals tb01.CO_CUR
                       join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb43.TB44_MODULO.CO_MODU_CUR equals tb44.CO_MODU_CUR

                       join tb02ag in TB02_MATERIA.RetornaTodosRegistros() on tb43.ID_MATER_AGRUP equals tb02ag.CO_MAT into l1
                       from lr1 in l1.DefaultIfEmpty()

                       join tb107ag in TB107_CADMATERIAS.RetornaTodosRegistros() on lr1.ID_MATERIA equals tb107ag.ID_MATERIA into l2
                       from lrma in l2.DefaultIfEmpty()
                       select new saidaGrade
                       {
                           NO_MAT = (tb107.NO_MATERIA.Length > 80 ? tb107.NO_MATERIA.Substring(0, 60) + "..." : tb107.NO_MATERIA),
                           ORDIMP = tb43.CO_ORDEM_IMPRE,
                           CH = tb43.QTDE_CH_SEM,
                           QTA = tb43.QTDE_AULA_SEM,
                           DISC_AGRUP = lrma.NO_MATERIA,
                           NOTA = tb43.FL_LANCA_NOTA == "S" ? "SIM" : "NÃO",
                           FREQ = tb43.FL_LANCA_FREQU == "S" ? "SIM" : "NÃO",

                           CO_ANO = tb43.CO_ANO_GRADE,
                           CO_EMP = tb43.CO_EMP,
                           CO_CUR = tb43.CO_CUR,
                           CO_MAT = tb43.CO_MAT,
                       }).ToList();

            GRD.DataSource = res;
            GRD.DataBind();
        }

        public class saidaGrade
        {
            public string NO_MAT { get; set; }
            public int? CH { get; set; }
            public int? QTA { get; set; }
            public int? ORDIMP { get; set; }
            public string DISC_AGRUP { get; set; }
            public string NOTA { get; set; }
            public string FREQ { get; set; }

            public string CO_ANO { get; set; }
            public int CO_EMP { get; set; }
            public int CO_CUR { get; set; }
            public int CO_MAT { get; set; }
        }

        #endregion

        #region Funções de Campo

        protected void ddlModaOri_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCurso(ddlCursoOri,(ddlModaOri.SelectedValue != "" ? int.Parse(ddlModaOri.SelectedValue) : 0));
        }

        protected void ddlModalDesti_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCurso(ddlCursoDesti, (ddlModalDesti.SelectedValue != "" ? int.Parse(ddlModalDesti.SelectedValue) : 0));

            if (ddlModaOri.SelectedValue != "" && ddlModalDesti.SelectedValue != "")
            {
                if (ddlModaOri.SelectedValue != ddlModalDesti.SelectedValue)
                {
                    AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "AVISO: Você está fazendo a duplicação de disciplinas entre modalidades diferentes!!! Deseja realmente continuar ?");
                }
            }
        }

        protected void imgPesqGradeOri_OnClick(object sender, EventArgs e)
        {
            string ano = ddlAnoOri.SelectedValue;
            int modalidade = ddlModaOri.SelectedValue != "" ? int.Parse(ddlModaOri.SelectedValue) : 0;
            int curso = ddlCursoOri.SelectedValue != "" ? int.Parse(ddlCursoOri.SelectedValue) : 0;

            CarregaGradeCursoOrigem(grdGradeOrigem, ano, modalidade, curso);
        }

        protected void ddlCursoDesti_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string ano = txtAnoDesti.Text;
            int modalidade = ddlModalDesti.SelectedValue != "" ? int.Parse(ddlModalDesti.SelectedValue) : 0;
            int curso = ddlCursoDesti.SelectedValue != "" ? int.Parse(ddlCursoDesti.SelectedValue) : 0;

            CarregaGradeCursoOrigem(grdGradeDestino, ano, modalidade, curso);

            if (ddlCursoOri.SelectedValue != "" && ddlCursoDesti.SelectedValue != "")
            {
                if (ddlCursoOri.SelectedValue != ddlCursoDesti.SelectedValue)
                {
                    AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "AVISO: Você está fazendo a duplicação de disciplinas entre Cursos diferentes!!! Deseja realmente continuar ?");
                }
            }
        }

        protected void chkMarcaTodosIteOrigem_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkMarca = ((CheckBox)grdGradeOrigem.HeaderRow.Cells[0].FindControl("chkMarcaTodosIteOrigem"));

            //Percorre alterando o checkbox de acordo com o selecionado em marcar todos
            foreach (GridViewRow li in grdGradeOrigem.Rows)
            {
                CheckBox ck = (((CheckBox)li.Cells[0].FindControl("chkSelectGridOrigem")));
                ck.Checked = chkMarca.Checked;
            }
        }

        #endregion
    }
}

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
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3030_CtrlMonitoria._3031_Chat
{
    public partial class CriaSalaChat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaGridVazia();
                CarregaGridVaziaMonitores();
                CarregaGridVaziaParticipantes();

                carregaModalidade();

                ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
                ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
                ddlMateria.Items.Insert(0, new ListItem("Selecione", ""));
            }

        }

        #region Carregamentos

        /// <summary>
        /// Carrega as Modalidades para filtragem das informações de Série/Curso, Turma e Matéria.
        /// </summary>
        private void carregaModalidade()
        {
            var res = (from tb44 in TB44_MODULO.RetornaTodosRegistros()
                       select new
                       {
                           tb44.CO_MODU_CUR,
                           tb44.DE_MODU_CUR
                       });

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";

            ddlModalidade.DataSource = res;
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega a Série e Curso de acordo com a modalidade informada.
        /// </summary>
        private void carregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            //string anoGrade = ddlAno.SelectedValue;

            if (modalidade != 0)
            {
                var res = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                           where tb01.CO_MODU_CUR == modalidade
                           select new { tb01.CO_CUR, tb01.NO_CUR });

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";

                ddlSerieCurso.DataSource = res;
                ddlSerieCurso.DataBind();
                ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
            }
            else
            {
                ddlTurma.Items.Clear();
                ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        /// <summary>
        /// Carrega as Turmas de acordo com série e curso.
        /// </summary>
        private void carregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if ((modalidade != 0) && (serie != 0))
            {

                var res = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                           where tb06.CO_CUR == serie && tb06.CO_MODU_CUR == modalidade
                           select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";

                ddlTurma.DataSource = res;
                ddlTurma.DataBind();
                ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
            }
            else
            {
                ddlTurma.Items.Clear();
                ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        /// <summary>
        /// Carrega as Matérias na ComboBox
        /// </summary>
        private void carregaMateria()
        {
            int mod = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int ser = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int tur = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;

            var res = (from tb107 in TB107_CADMATERIAS.RetornaTodosRegistros()
                       join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb107.ID_MATERIA equals tb02.ID_MATERIA
                       join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb02.CO_MAT equals tb43.CO_MAT
                       where
                          (mod != 0 ? tb02.CO_MODU_CUR == mod : 0 == 0)
                       && (ser != 0 ? tb43.CO_CUR == ser : 0 == 0)

                       select new { tb107.NO_MATERIA, tb107.ID_MATERIA }).Distinct();

            ddlMateria.DataTextField = "NO_MATERIA";
            ddlMateria.DataValueField = "ID_MATERIA";

            ddlMateria.DataSource = res;
            ddlMateria.DataBind();


            if (ddlMateria.Items.Count == 0)
            {
                ddlMateria.Enabled = false;
                ddlMateria.Items.Insert(0, new ListItem("Sem matérias para a grade Informada", ""));
            }
            else
            {
                ddlMateria.Enabled = true;
            }
        }


        private void CarregaGridVazia()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "DTINI";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "DTFIM";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "SALA";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "DISCI";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "MONIT";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "SITUA";
            dtV.Columns.Add(dcATM);

            //int i = 1;
            DataRow linha;
            //while (i <= 2)
            //{
            linha = dtV.NewRow();
            linha["DTINI"] = "12/10/2014";
            linha["DTFIM"] = "19/10/2014";
            linha["SALA"] = "SALA B04 - C";
            linha["DISCI"] = "Língua Estrangeira Moderna - Inglês";
            linha["MONIT"] = "VICTOR SOUZA COSTA SOARES DE AZEVEDO";
            linha["SITUA"] = "EM ABERTO";
            dtV.Rows.Add(linha);

            //}

            //Session["TabelaMedicAtendMed"] = dtV;

            grdAgendaMonitoria.DataSource = dtV;
            grdAgendaMonitoria.DataBind();
        }

        private void CarregaGridVaziaMonitores()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NOME";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "USER";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "DISCI";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "MONIT_PRIN";
            dtV.Columns.Add(dcATM);

            DataRow linha;

            linha = dtV.NewRow();
            linha["NOME"] = "CESAR FONTES";
            linha["USER"] = "CESAR.FOTES";
            linha["DISCI"] = "Educação Física";
            linha["MONIT_PRIN"] = "";

            dtV.Rows.Add(linha);

            grdMonitores.DataSource = dtV;
            grdMonitores.DataBind();
        }

        private void CarregaGridVaziaParticipantes()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NIRE";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NOME";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "SEXO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CURSO";
            dtV.Columns.Add(dcATM);

            DataRow linha;

            linha = dtV.NewRow();
            linha["NIRE"] = "4856398";
            linha["NOME"] = "LUCCILENE AMADO AIRES COSTA";
            linha["SEXO"] = "F";
            linha["CURSO"] = "EM - 1ºANO-EM - 1ºB-EM";

            dtV.Rows.Add(linha);

            grdPartici.DataSource = dtV;
            grdPartici.DataBind();
        }

        #endregion

        #region Funções de Campo

        protected void ddlModalidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaSerieCurso();
            carregaTurma();
            carregaMateria();
        }

        protected void ddlSerieCurso_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaTurma();
            carregaMateria();
        }

        protected void OnClickPesq(object sender, EventArgs e)
        {

        }

        #endregion

    }
}
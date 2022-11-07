//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CIDADE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 03/03/14 | Maxwell Almeida            | Criação da funcionalidade para Associação de Monitorias


//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3030_CtrlMonitoria._3032_AssociacaoMonitoria
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }
        #region Eventos
        string salvaDTSitu;
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
                txtAno.Text = DateTime.Now.Year.ToString();
                carregaModalidade();
                carregaMateria();
                carregaProfessor();

                ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
                ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
                ddlMateria.Items.Insert(0, new ListItem("Selecione", ""));
                IniPeri.Text = DateTime.Now.ToString();
                FimPeri.Text = DateTime.Now.AddDays(7).ToString();
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if ((verificaAssociacao() > 0) && (txtIsEd.Text != "sim"))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Professor informado Já está associado à Modalidade, Série/Curso e Matéria informados");
            }
            else
            {
                if (DateTime.Parse(IniPeri.Text) > DateTime.Parse(FimPeri.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Data Inicial não pode ser maior que a Data Final.");
                }
                else
                {
                    TB188_MONIT_CURSO_PROFE tb188 = RetornaEntidade();

                    tb188.CO_ANO_LET = txtAno.Text;
                    tb188.CO_COL = int.Parse(ddlProfessor.SelectedValue);
                    tb188.CO_MODU_CUR = int.Parse(ddlModalidade.SelectedValue);
                    tb188.CO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
                    tb188.CO_TUR = int.Parse(ddlTurma.SelectedValue);
                    tb188.CO_EMP = LoginAuxili.CO_EMP;
                    tb188.CO_MAT = int.Parse(ddlMateria.SelectedValue);
                    tb188.CO_SITUA_MONIT = ddlSitu.SelectedValue;
                    tb188.VL_HORA = decimal.Parse(txtVlHr.Text);

                    if (txtSituAlter.Text != ddlSitu.SelectedValue)
                    {
                        tb188.DT_SITUA_MONIT = DateTime.Now;
                        tb188.CO_IP_SITUA_MONIT = Request.UserHostAddress;
                        tb188.CO_COL_SITUA_MONIT = LoginAuxili.CO_COL;
                        tb188.CO_EMP_SITUA_MONIT = LoginAuxili.CO_EMP;
                    }

                    if (txtIsEd.Text != "S")
                    {
                        tb188.DT_CADAS = DateTime.Now;
                        tb188.CO_IP_CADAS_MONIT = Request.UserHostAddress;
                        tb188.CO_EMP_CADAS_MONIT = LoginAuxili.CO_EMP;
                        tb188.CO_COL_CADAS_MONIT = LoginAuxili.CO_COL;
                    }

                    CurrentPadraoCadastros.CurrentEntity = tb188;
                }
            }
        }
        #endregion

        #region Métodos

        private int verificaAssociacao()
        {
            int cocol = int.Parse(ddlProfessor.SelectedValue);
            int como = int.Parse(ddlModalidade.SelectedValue);
            int ser = int.Parse(ddlSerieCurso.SelectedValue);
            int mat = int.Parse(ddlMateria.SelectedValue);

            //DateTime dataIni1;
            //if (!DateTime.TryParse(IniPeri.Text, out dataIni1))
            //{
            //    return 0;
            //}

            //DateTime dataFim1;
            //if (!DateTime.TryParse(FimPeri.Text, out dataFim1))
            //{
            //    return 0;
            //}

            var res = (from tb188 in TB188_MONIT_CURSO_PROFE.RetornaTodosRegistros()
                       where (tb188.CO_COL == cocol)
                       && (tb188.CO_MODU_CUR == como)
                       && (tb188.CO_CUR == ser)
                       && (tb188.CO_MAT == mat)                       
                       select new
                       {
                           tb188.CO_COL,
                           tb188.ID_MONIT_CURSO_PROFE
                       });

            return res.Count();
        }

        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB188_MONIT_CURSO_PROFE tb188 = RetornaEntidade();

            if (tb188 != null)
            {
                txtIsEd.Text = "sim";
                txtSituAlter.Text = tb188.CO_SITUA_MONIT;
                txtAno.Text = tb188.CO_ANO_LET;
                ddlModalidade.SelectedValue = tb188.CO_MODU_CUR.ToString();
                carregaSerieCurso();
                ddlSerieCurso.SelectedValue = tb188.CO_CUR.ToString();
                carregaTurma();
                ddlTurma.SelectedValue = tb188.CO_TUR.ToString();
                carregaMateria();
                ddlMateria.SelectedValue = tb188.CO_MAT.ToString();
                ddlProfessor.SelectedValue = tb188.CO_COL.ToString();
                ddlSitu.SelectedValue = tb188.CO_SITUA_MONIT.ToString();
				txtVlHr.Text = tb188.VL_HORA.ToString();
            }
          
        }

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
            string ano = txtAno.Text;

            var res = (from tb107 in TB107_CADMATERIAS.RetornaTodosRegistros()
                       join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb107.ID_MATERIA equals tb02.ID_MATERIA
                       join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb02.CO_MAT equals tb43.CO_MAT
                       where
                          (mod != 0 ? tb02.CO_MODU_CUR == mod : 0 == 0)
                       && (ser != 0 ? tb43.CO_CUR == ser : 0 == 0)
                       //&& (tur != 0 ? tb02.CO_TUR == tur : 0 == 0)
                       && (ano != "0" ? tb43.CO_ANO_GRADE == ano : 0 == 0)

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

        private void carregaProfessor()
        {
                var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros() 
                           where (tb03.FLA_PROFESSOR == "S")
                           && (tb03.FL_ATIVI_MONIT == "S")
                           && (tb03.CO_SITU_COL == "ATI")
                           select new { tb03.CO_COL, tb03.NO_COL }).ToList();

                ddlProfessor.DataTextField = "NO_COL";
                ddlProfessor.DataValueField = "CO_COL";
                ddlProfessor.DataSource = res;
                ddlProfessor.DataBind();
                ddlProfessor.Items.Insert(0, new ListItem("Selecione", ""));


        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB904_CIDADE</returns>
        private TB188_MONIT_CURSO_PROFE RetornaEntidade()
        {
            TB188_MONIT_CURSO_PROFE tb188 = TB188_MONIT_CURSO_PROFE.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb188 == null) ? new TB188_MONIT_CURSO_PROFE() : tb188;
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

        #endregion

    }
}
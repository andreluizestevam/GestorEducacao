//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PLANEJAMENTO ESCOLAR (CENÁRIO ALUNOS)
// OBJETIVO: REGISTRO DO PERFIL DE DESEMPENHO DE ALUNOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1120_PlanejEscolarAlunos.F1125_RegisPerfilDesemAluno
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidade();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaMaterias();
                CarregaTurma();
                CarregaMaterias();
                CarregaAnos();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_UNIDADE_PERFIL_DESEM" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField 
            { 
                DataField = "sigla", 
                HeaderText = "Unidade" 
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField 
            { 
                DataField = "NR_ANO", 
                HeaderText = "Ano" 
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField 
            { 
                DataField = "NO_CUR", 
                HeaderText = "Série" 
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField 
            { 
                DataField = "CO_SIGLA_TURMA", 
                HeaderText = "Turma" 
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField 
            { 
                DataField = "NO_MATERIA", 
                HeaderText = "Matéria" 
            });

            BoundField bf1 = new BoundField();
            bf1.DataField = "NR_MEDIA_BIM1_DESEMP";
            bf1.HeaderText = "1º Bim";
            bf1.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf1);

            BoundField bf2 = new BoundField();
            bf2.DataField = "NR_MEDIA_BIM2_DESEMP";
            bf2.HeaderText = "2º Bim";
            bf2.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf2);

            BoundField bf3 = new BoundField();
            bf3.DataField = "NR_MEDIA_BIM3_DESEMP";
            bf3.HeaderText = "3º Bim";
            bf3.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf3);

            BoundField bf4 = new BoundField();
            bf4.DataField = "NR_MEDIA_BIM4_DESEMP";
            bf4.HeaderText = "4º Bim";
            bf4.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf4);

            BoundField bf5 = new BoundField();
            bf5.DataField = "Media";
            bf5.HeaderText = "Média Anual";
            bf5.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            bf5.DataFormatString = "{0:N2}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf5);       
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlMateria.SelectedValue != "" ? int.Parse(ddlMateria.SelectedValue) : 0;

            Decimal decimalNotaMinima = 0;
            Decimal.TryParse(txtnotamenor.Text, out decimalNotaMinima);

            Decimal decimalNotaMaxima = 0;
            Decimal.TryParse(txtnotamaior.Text, out decimalNotaMaxima);

            bool bolIntervaloNota = (txtnotamenor.Text != "" && txtnotamaior.Text != "");

            var resultado = (from tb247 in TB247_UNIDADE_PERFIL_DESEMPENHO.RetornaTodosRegistros()
                             join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb247.ID_MATERIA equals tb107.ID_MATERIA
                             join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb247.TB06_TURMAS.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                             where ( ddlAno.SelectedValue != "" ? tb247.NR_ANO == ddlAno.SelectedValue : ddlAno.SelectedValue == "0") 
                             && coEmp != 0 ? tb247.TB06_TURMAS.CO_EMP == coEmp : coEmp == 0 
                             && modalidade != 0 ?  tb247.TB06_TURMAS.CO_MODU_CUR == modalidade : modalidade == 0 
                             && serie != 0 ? tb247.TB06_TURMAS.CO_CUR == serie : serie == 0
                             && turma != 0 ? tb247.TB06_TURMAS.CO_TUR == turma : turma == 0 
                             && materia != 0 ? tb247.ID_MATERIA == materia : materia == 0
                             && tb134.ADMUSUARIO.ideAdmUsuario == LoginAuxili.IDEADMUSUARIO
                             && (!bolIntervaloNota ||
                             (((tb247.NR_MEDIA_BIM1_DESEMP + tb247.NR_MEDIA_BIM2_DESEMP + tb247.NR_MEDIA_BIM3_DESEMP + tb247.NR_MEDIA_BIM4_DESEMP) / 4 >= decimalNotaMinima) &&
                             ((tb247.NR_MEDIA_BIM1_DESEMP + tb247.NR_MEDIA_BIM2_DESEMP + tb247.NR_MEDIA_BIM3_DESEMP + tb247.NR_MEDIA_BIM4_DESEMP) / 4 <= decimalNotaMaxima))
                              )
                              orderby tb247.TB06_TURMAS.TB01_CURSO.TB25_EMPRESA.sigla, tb247.NR_ANO, tb247.TB06_TURMAS.TB01_CURSO.NO_CUR,
                              tb247.TB06_TURMAS.TB129_CADTURMAS.CO_SIGLA_TURMA, tb107.NO_MATERIA
                              select new
                              {
                                tb247.ID_UNIDADE_PERFIL_DESEM, tb247.TB06_TURMAS.TB01_CURSO.TB25_EMPRESA.sigla, tb247.NR_ANO,
                                tb247.TB06_TURMAS.TB01_CURSO.NO_CUR, tb247.TB06_TURMAS.TB129_CADTURMAS.CO_SIGLA_TURMA, tb107.NO_MATERIA,
                                tb247.NR_MEDIA_BIM1_DESEMP, tb247.NR_MEDIA_BIM2_DESEMP, tb247.NR_MEDIA_BIM3_DESEMP, tb247.NR_MEDIA_BIM4_DESEMP,
                                Media = ((tb247.NR_MEDIA_BIM1_DESEMP + tb247.NR_MEDIA_BIM2_DESEMP + tb247.NR_MEDIA_BIM3_DESEMP + tb247.NR_MEDIA_BIM4_DESEMP) / 4)
                              });

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado.Distinct() : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_UNIDADE_PERFIL_DESEM"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamendo DropDown

//====> Método que carrega o DropDown de Unidades Escolares
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).OrderBy( e => e.NO_FANTAS_EMP ).Distinct();

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("Todas", ""));
            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

//====> Método que carrega o DropDown de Modalidades
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);
            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Todos", ""));
        }

//====> Método que carrega o DropDown de Séries
        private void CarregaSerieCurso()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                        where tb01.CO_EMP == coEmp && tb01.CO_MODU_CUR == modalidade
                                        select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy( c => c.NO_CUR );

            ddlSerieCurso.DataTextField = "NO_CUR";
            ddlSerieCurso.DataValueField = "CO_CUR";
            ddlSerieCurso.DataBind();

            ddlSerieCurso.Items.Insert(0, new ListItem("Todas", ""));
        }

//====> Método que carrega o DropDown de Turmas
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                   where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                   select new { tb06.TB129_CADTURMAS.CO_SIGLA_TURMA, tb06.CO_TUR }).Distinct().OrderBy(t => t.CO_SIGLA_TURMA);

            ddlTurma.DataTextField = "CO_SIGLA_TURMA";
            ddlTurma.DataValueField = "CO_TUR";
            ddlTurma.DataBind();

            ddlTurma.Items.Insert(0, new ListItem("Todas", "0"));
        }

//====> Método que carrega o DropDown de Matérias
        private void CarregaMaterias()
        {
            ddlMateria.Items.Clear();

            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if (serie != 0 && modalidade != 0)
            {
                ddlMateria.DataSource = (from tb02 in TB02_MATERIA.RetornaTodosRegistros()
                                         join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                         where tb02.CO_CUR == serie && tb02.CO_MODU_CUR == modalidade
                                         select new { tb107.ID_MATERIA, tb107.NO_MATERIA }).Distinct().OrderBy( m => m.NO_MATERIA );

                ddlMateria.DataTextField = "NO_MATERIA";
                ddlMateria.DataValueField = "ID_MATERIA";
                ddlMateria.DataBind();
            }

            ddlMateria.Items.Insert(0, new ListItem("Todas", ""));
        }

//====> Método que carrega o DropDown de Anos
        private void CarregaAnos()
        {
            ddlAno.Items.Clear();

            for (int a = DateTime.Now.Year - 5; a < DateTime.Now.Year + 5; a++)
                ddlAno.Items.Add(new ListItem(a.ToString(), a.ToString()));
            ddlAno.SelectedValue = DateTime.Now.Year.ToString("0000");
        }
        #endregion

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaMaterias();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaMaterias();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaMaterias();
        }
    }
}

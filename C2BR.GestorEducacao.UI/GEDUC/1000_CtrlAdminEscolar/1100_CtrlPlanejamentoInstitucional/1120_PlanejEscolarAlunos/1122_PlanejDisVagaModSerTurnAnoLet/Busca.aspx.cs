//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PLANEJAMENTO ESCOLAR (CENÁRIO ALUNOS)
// OBJETIVO: PLANEJAMENTO DISPONIBILIDADE DE VAGAS POR MODALIDADE/SÉRIE/TURNO NO ANO LETIVO *** REGISTRO ANUAL DE DISPONIBILIDADE DE VAGAS MODALIDADE/SÉRIE/TURMA DE UNIDADE DE ENSINO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1120_PlanejEscolarAlunos.F1122_PlanejDisVagaModSerTurnAnoLet
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
                CarregaAnos();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_DISP_VAGA_TURMA" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_ANO",
                HeaderText = "Ano"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_MODU_CUR",
                HeaderText = "Modalidade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_CUR",
                HeaderText = "Série"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_TURMA",
                HeaderText = "Turma"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "Turno",
                HeaderText = "Turno"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "QTDE_VAG_DISP",
                HeaderText = "Disponível"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "QTDE_VAG_MAT",
                HeaderText = "Matriculado"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView() 
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            var resultado = (from tb289 in TB289_DISP_VAGA_TURMA.RetornaTodosRegistros()
                             join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb289.CO_TUR equals tb129.CO_TUR
                             join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb289.CO_CUR equals tb01.CO_CUR
                             where tb289.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                             && ( modalidade != 0 ? tb289.TB44_MODULO.CO_MODU_CUR == modalidade : modalidade == 0)
                             && ( serie != 0 ? tb289.CO_CUR == serie : serie == 0)
                             && ( ddlAno.SelectedValue != "" ? tb289.CO_ANO == ddlAno.SelectedValue : ddlAno.SelectedValue == "")                             
                             select new 
                             {
                                tb289.ID_DISP_VAGA_TURMA, tb129.NO_TURMA, tb289.CO_ANO, 
                                tb289.TB44_MODULO.CO_MODU_CUR, tb289.CO_CUR,
                                Turno = (tb289.CO_PERI_TUR == "M" ? "Matutino" : tb289.CO_PERI_TUR == "V" ? "Vespertino" : "Noturno"),
                                tb289.QTDE_VAG_DISP, tb289.QTDE_VAG_MAT, tb01.NO_CUR, tb289.TB44_MODULO.DE_MODU_CUR, tb289.NU_SEM_LET
                             }).OrderBy( d => d.CO_ANO ).ThenBy( d => d.DE_MODU_CUR ).ThenBy( d => d.NO_CUR );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }        

        void CurrentPadraoBuscas_OnDefineQueryStringIds() 
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_DISP_VAGA_TURMA"));      

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Anos
        private void CarregaAnos() 
        {
            var res = (from tb289 in TB289_DISP_VAGA_TURMA.RetornaTodosRegistros()
                                 where tb289.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                 select new { tb289.CO_ANO }).OrderByDescending(d => d.CO_ANO).Distinct();
        if (res.Count() != 0)
        {
            ddlAno.DataValueField = "CO_ANO";
            ddlAno.DataTextField = "CO_ANO";
            ddlAno.DataSource = res;
            ddlAno.DataBind();
            
            }else
            {
                var ano = DateTime.Now.Year.ToString();
                ddlAno.Items.Insert(0, new ListItem(ano, ano));
                ddlAno.SelectedValue = ano;
            }
        }


//====> Método que carrega o DropDown de Modalidades
        private void CarregaModalidades() 
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);
            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Todas", ""));
        }

//====> Método que carrega o DropDown de Séries
        private void CarregaSerieCurso() 
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                       where tb01.CO_MODU_CUR == modalidade
                                       select new { tb01.CO_CUR, tb01.NO_CUR }).OrderBy( c => c.NO_CUR );

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
                                   select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR }).Distinct().OrderBy(t => t.NO_TURMA);

            ddlTurma.DataTextField = "NO_TURMA";
            ddlTurma.DataValueField = "CO_TUR";
            ddlTurma.DataBind();

            ddlTurma.Items.Insert(0, new ListItem("Todas", ""));
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e) 
        {
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e) 
        {
            CarregaTurma();
        }
    }
}

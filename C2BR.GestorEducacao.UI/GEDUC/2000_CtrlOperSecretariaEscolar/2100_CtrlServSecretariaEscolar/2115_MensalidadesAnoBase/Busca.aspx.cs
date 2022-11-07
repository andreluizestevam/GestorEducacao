//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE SÉRIES OU CURSOS
// OBJETIVO: CADASTRAMENTO DE SÉRIES/CURSOS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2115_MensalidadesAnoBase
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

        void Page_Load()
        {
            if (!Page.IsPostBack)
            {
                CarregaAnos();
                CarregaModalidades();
                CarregaSerieCurso();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_ANO_BASE", "ID_MENSA_ANO_BASE", "CO_CUR", "CO_MODU_CUR" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_ANO_BASE",
                HeaderText = "Ano Base",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "VALOR_MANHA",
                HeaderText = "Manhã",
            });
          
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "VALOR_TARDE",
                HeaderText = "Tarde",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "VALOR_NOITE",
                HeaderText = "Noite",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "VALOR_INTEGRAL",
                HeaderText = "Integral",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "VALOR_ESPECIAL",
                HeaderText = "Especial",
            });

        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int ano = ddlAno.SelectedValue != "0" ? int.Parse(ddlAno.SelectedValue) : 0;
            int modalidade = ddlModalidades.SelectedValue != "0" ? int.Parse(ddlModalidades.SelectedValue) : 0;
            int curso = ddlSerieCurso.SelectedValue != "0" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            var resultado = (from tb430 in TB430_MENSA_ANOBASE.RetornaTodosRegistros()
                                 join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb430.TB01_CURSO.CO_CUR equals tb01.CO_CUR
                                 join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb430.TB01_CURSO.TB44_MODULO.CO_MODU_CUR equals tb44.CO_MODU_CUR
                                 where (curso == 0? 0 == 0 : tb01.CO_CUR == curso)
                                 && (modalidade == 0? 0 == 0 : tb44.CO_MODU_CUR == modalidade)
                                 && (ano == 0? 0 == 0 : tb430.CO_ANO_BASE == ano)
                                 select new
                            {
                                CO_CUR = tb01.CO_CUR,
                                ID_MENSA_ANO_BASE = tb430.ID_MENSA_ANO_BASE,
                                CO_MODU_CUR = tb01.TB44_MODULO.CO_MODU_CUR,
                                CO_ANO_BASE = tb430.CO_ANO_BASE,
                                VALOR_MANHA = tb430.VALOR_MANHA,
                                VALOR_TARDE = tb430.VALOR_TARDE,
                                VALOR_NOITE = tb430.VALOR_NOITE,
                                VALOR_INTEGRAL = tb430.VALOR_INTEGRAL,
                                VALOR_ESPECIAL = tb430.VALOR_ESPECIAL,
                            }).OrderBy( c => c.CO_ANO_BASE ).ToList();

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Ano, "CO_ANO_BASE"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoCur, "CO_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_MENSA_ANO_BASE"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoModuCur, "CO_MODU_CUR"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            ddlAno.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                 select new { tb43.CO_ANO_GRADE }).Distinct().OrderByDescending(g => g.CO_ANO_GRADE);

            //ddlAno.DataSource = from re in resultado
            //                    select new { CO_ANO_GRADE = re.CO_ANO_GRADE.Trim() };


            ddlAno.DataTextField = "CO_ANO_GRADE";
            ddlAno.DataValueField = "CO_ANO_GRADE";
            ddlAno.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades, verifica se o usuário logado é professor.
        /// </summary>
        private void CarregaModalidades()
        {
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;

            AuxiliCarregamentos.carregaModalidades(ddlModalidades, LoginAuxili.ORG_CODIGO_ORGAO, true);

                //var res = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                //           join mo in TB44_MODULO.RetornaTodosRegistros() on rm.CO_MODU_CUR equals mo.CO_MODU_CUR
                //           where rm.CO_COL_RESP == LoginAuxili.CO_COL
                //           && rm.CO_ANO_REF == ano
                //           select new
                //           {
                //               mo.DE_MODU_CUR,
                //               rm.CO_MODU_CUR
                //           }).Distinct();

                //ddlModalidades.DataTextField = "DE_MODU_CUR";
                //ddlModalidades.DataValueField = "CO_MODU_CUR";
                //ddlModalidades.DataSource = res;
                //ddlModalidades.DataBind();

                //if (res.Count() != 1)
                //    ddlModalidades.Items.Insert(0, new ListItem("Todos", "0"));

            
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries, verifica se o usuário logado  é professor.
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidades.SelectedValue != "" ? int.Parse(ddlModalidades.SelectedValue) : 0;
            string anoGrade = ddlAno.SelectedValue;
            int ano = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;

            AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, LoginAuxili.CO_EMP, true);
            
                //var res = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                //           join c in TB01_CURSO.RetornaTodosRegistros() on rm.CO_CUR equals c.CO_CUR
                //           join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on rm.CO_CUR equals tb43.CO_CUR
                //           where rm.CO_COL_RESP == LoginAuxili.CO_COL
                //           && rm.CO_MODU_CUR == modalidade
                //           && rm.CO_ANO_REF == ano
                //           select new
                //           {
                //               c.NO_CUR,
                //               rm.CO_CUR
                //           }).Distinct();

                //ddlSerieCurso.DataTextField = "NO_CUR";
                //ddlSerieCurso.DataValueField = "CO_CUR";
                //ddlSerieCurso.DataSource = res;
                //ddlSerieCurso.DataBind();

                //if (res.Count() != 1)
                //    ddlSerieCurso.Items.Insert(0, new ListItem("Todos", "0"));
            
        }
        #endregion

        #region Eventos componentes

        protected void ddlAno_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaModalidades();
            CarregaSerieCurso();

        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }

        #endregion
    }
}

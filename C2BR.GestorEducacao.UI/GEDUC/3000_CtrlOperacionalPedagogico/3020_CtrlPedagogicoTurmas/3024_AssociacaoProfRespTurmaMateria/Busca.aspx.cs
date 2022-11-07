//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE TURMAS POR SÉRIES/CURSOS
// OBJETIVO: ASSOCIAÇÃO PROFESSOR RESPONSÁVEL TURMA E/OU MATÉRIA.
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3020_CtrlPedagogicoTurmas.F3024_AssociacaoProfRespTurmaMateria
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
            if (IsPostBack) return;
            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_RESP_MAT" };



            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_ANO_REF",
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
                DataField = "CO_SIGLA_TURMA",
                HeaderText = "Turma"
            });

            BoundField bfRealizado2 = new BoundField();
            bfRealizado2.DataField = "nomeMateria";
            bfRealizado2.HeaderText = "Sigla Mat";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado2);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_CLASS_RESP",
                HeaderText = "Classificação"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_COL",
                HeaderText = "Nome"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string classificacao = ddlClassificacao.SelectedValue;
            int ano = int.Parse(ddlAno.SelectedValue);

            var resultado = (from tbResponMateria in TB_RESPON_MATERIA.RetornaTodosRegistros()
                            where tbResponMateria.CO_EMP == LoginAuxili.CO_EMP 
                            && (modalidade != 0 ? tbResponMateria.CO_MODU_CUR == modalidade : modalidade == 0)
                            && (serie != 0 ? tbResponMateria.CO_CUR == serie : serie == 0) 
                            && (turma != 0 ? tbResponMateria.CO_TUR == turma : turma == 0)
                            && tbResponMateria.CO_ANO_REF == ano
                            && (classificacao != "0" ? tbResponMateria.CO_CLASS_RESP == classificacao : 0 == 0)
                            join tb06 in TB06_TURMAS.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tbResponMateria.CO_TUR equals tb06.CO_TUR
                            join tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tbResponMateria.CO_CUR equals tb01.CO_CUR
                            join tb44 in TB44_MODULO.RetornaTodosRegistros() on tbResponMateria.CO_MODU_CUR equals tb44.CO_MODU_CUR
                            join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbResponMateria.CO_COL_RESP equals tb03.CO_COL
                            join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tbResponMateria.CO_MAT equals tb02.CO_MAT into resultadoM
                            from tb02 in resultadoM.DefaultIfEmpty()
                            join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA into resultadoCM
                            from tb107 in resultadoCM.DefaultIfEmpty()
                            select new saida
                            {
                                nomeMateria = (tb02 != null && tb107 != null) ? tb107.NO_SIGLA_MATERIA : "",
                                CO_MODU_CUR = tbResponMateria.CO_MODU_CUR, 
                                CO_CUR = tbResponMateria.CO_CUR, 
                                CO_TUR = tbResponMateria.CO_TUR, 
                                ID_RESP_MAT = tbResponMateria.ID_RESP_MAT,
                                CO_SIGLA_TURMA = tb06.TB129_CADTURMAS.CO_SIGLA_TURMA, 
                                NO_CUR = tb01.CO_SIGL_CUR,
                                DE_MODU_CUR_R = tb44.DE_MODU_CUR,
                                CO_ANO_REF = tbResponMateria.CO_ANO_REF,
                                NO_COL_R = tb03.NO_COL, 
                                CO_CLASS_RESP = (tbResponMateria.CO_CLASS_RESP == "P" ? "Professor Responsável" : 
                                (tbResponMateria.CO_CLASS_RESP == "C" ? "Coordenador de Turma" :
                                (tbResponMateria.CO_CLASS_RESP == "A" ? "Professor Adjunto" : (tbResponMateria.CO_CLASS_RESP == "M" ? "Monitor" : "Auxiliar"))))
                            }).Distinct().OrderByDescending(r => r.CO_ANO_REF).OrderBy(r => r.DE_MODU_CUR_R).ThenBy(r => r.NO_CUR).ThenBy(r => r.CO_SIGLA_TURMA);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        public class saida
        {
            public string nomeMateria { get; set; }
            public int CO_MODU_CUR { get; set;} 
            public int CO_CUR { get; set;}
            public int CO_TUR { get; set;}
            public int ID_RESP_MAT { get; set;}
            public string CO_SIGLA_TURMA { get; set;}
            public string NO_CUR { get; set;}
            public string DE_MODU_CUR_R { get; set; }
            public string DE_MODU_CUR
            {
                get
                {
                    return this.DE_MODU_CUR_R.Length > 30 ? this.DE_MODU_CUR_R.Substring(0, 30) + "..." : this.DE_MODU_CUR_R;
                }
            }
            public int CO_ANO_REF { get; set; }
            public string NO_COL_R { get; set; }
            public string NO_COL
            {
                get
                {
                    return this.NO_COL_R.Length > 30 ? this.NO_COL_R.Substring(0, 30) + "..." : this.NO_COL_R;
                }
            }
            public string CO_CLASS_RESP { get; set; }
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_RESP_MAT"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            AuxiliCarregamentos.CarregaAnoGrdCurs(ddlAno, LoginAuxili.CO_EMP, false);
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
            string ano = ddlAno.SelectedValue != "" ? ddlAno.SelectedValue : DateTime.Now.Year.ToString();
            ddlSerieCurso.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                        where tb43.CO_ANO_GRADE == ano
                                        join tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb43.CO_CUR equals tb01.CO_CUR
                                        where tb43.TB44_MODULO.CO_MODU_CUR == modalidade && tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                                        select new { tb01.CO_CUR, tb01.CO_SIGL_CUR }).Distinct().OrderBy(g => g.CO_SIGL_CUR);

            ddlSerieCurso.DataTextField = "CO_SIGL_CUR";
            ddlSerieCurso.DataValueField = "CO_CUR";
            ddlSerieCurso.DataBind();

            ddlSerieCurso.Items.Insert(0, new ListItem("Todas", ""));
        }

//====> Método que carrega o DropDown de Turmas
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            ddlTurma.Items.Clear(); 
            if ((modalidade != 0) && (serie != 0))
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();

            }
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

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
        }
    }
}

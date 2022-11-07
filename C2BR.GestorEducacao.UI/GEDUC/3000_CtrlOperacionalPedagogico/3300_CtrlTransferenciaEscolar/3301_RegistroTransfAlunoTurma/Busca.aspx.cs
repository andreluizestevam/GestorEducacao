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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3300_CtrlTransferenciaEscolar.F3301_RegistroTransfAlunoTurma
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

            CarregaModalidades();
            CarregaSerieCurso();
            CarregaAnos();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "NU_REF_TRANSF" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DT_EFETI_TRANSF",
                HeaderText = "Efetivação",
                 DataFormatString = "{0:d}"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_ALU",
                HeaderText = "Aluno"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_MODU_CUR",
                HeaderText = "Modalidade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_CUR",
                HeaderText = "Série/Curso"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_TURMA",
                HeaderText = "Destino"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int intAnoRefer = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            var resultado = (from tbTransfInt in TB_TRANSF_INTERNA.RetornaTodosRegistros()
                             where serie == 0 ? serie == 0 : tbTransfInt.CO_CURSO_DESTI == serie 
                             && tbTransfInt.CO_UNIDA_DESTI == LoginAuxili.CO_EMP                                        
                             join tb01 in TB01_CURSO.RetornaTodosRegistros() on tbTransfInt.CO_CURSO_DESTI equals tb01.CO_CUR
                             join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb01.CO_MODU_CUR equals tb44.CO_MODU_CUR
                             join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tbTransfInt.CO_TURMA_DESTI equals tb129.CO_TUR 
                             join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbTransfInt.TB07_ALUNO.CO_ALU equals tb07.CO_ALU
                             where tb01.CO_MODU_CUR == modalidade 
                             && tbTransfInt.DT_EFETI_TRANSF.Year == intAnoRefer
                             select new { tbTransfInt.NU_REF_TRANSF, tbTransfInt.DT_EFETI_TRANSF, tb44.DE_MODU_CUR, tb01.NO_CUR, tb07.NO_ALU, tb129.NO_TURMA });

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "NU_REF_TRANSF"));
            
            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion       

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Modalidades
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();
        }

//====> Método que carrega o DropDown de Anos
        private void CarregaAnos()
        {
            ddlAno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                 where tb08.CO_EMP == LoginAuxili.CO_EMP
                                 select new { tb08.CO_ANO_MES_MAT }).Distinct().OrderByDescending(p => p.CO_ANO_MES_MAT);

            ddlAno.DataTextField = "CO_ANO_MES_MAT";
            ddlAno.DataValueField = "CO_ANO_MES_MAT";
            ddlAno.DataBind();
        }

//====> Método que carrega o DropDown de Séries
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string coAnoGrade = ddlAno.SelectedValue;

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb01.CO_MODU_CUR == modalidade
                                            join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.CO_ANO_GRADE == coAnoGrade && tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy( c => c.NO_CUR );
                 
                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }
            else
                ddlSerieCurso.Items.Clear();

            ddlSerieCurso.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidades();
            CarregaSerieCurso();
        }
    }
}

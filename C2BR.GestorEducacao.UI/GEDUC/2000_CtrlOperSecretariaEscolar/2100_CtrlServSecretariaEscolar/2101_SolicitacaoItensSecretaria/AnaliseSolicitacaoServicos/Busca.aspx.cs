//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: SOLICITAÇÃO DE ITENS DE SECRETARIA ESCOLAR
// OBJETIVO: ANALISE SOLICITAÇÃO DE SERVIÇOS.
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
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2101_SolicitacaoItensSecretaria.AnaliseSolicitacaoServicos
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas {get { return ((PadraoBuscas)this.Master); } }

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
            if (!Page.IsPostBack)
            {
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
                CarregaSolicitacoes();
            }
        }

        protected void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_EMP", "CO_ALU", "CO_CUR", "CO_TIPO_SOLI", "CO_SOLI_ATEN" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_DCTO_SOLIC",
                HeaderText = "Nº Solicitação"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_TIPO_SOLI",
                HeaderText = "Descrição da Solicitação"
            });
            
            BoundField bfRealizado = new BoundField();
            bfRealizado.DataField = "DT_PREV_ENTR";
            bfRealizado.HeaderText = "Previsão";
            bfRealizado.ItemStyle.CssClass = "codCol";
            bfRealizado.DataFormatString = "{0:d}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado);
            
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_ALU",
                HeaderText = "Beneficiário"
            });
            
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "sigla",
                HeaderText = "Unid Origem"
            });
        }

        protected void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;            
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int coTipoSoli = ddlSolicitacoes.SelectedValue != "" ? int.Parse(ddlSolicitacoes.SelectedValue) : 0;   

            var resultado = (from tb64 in TB64_SOLIC_ATEND.RetornaTodosRegistros()
                            where tb64.CO_EMP_ALU == LoginAuxili.CO_EMP 
                            && (turma != 0 ? tb64.CO_TUR == turma : turma == 0) && (modalidade != 0 ? tb64.CO_MODU_CUR == modalidade : modalidade == 0)
                            join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb64.CO_ALU equals tb07.CO_ALU
                            join tb65 in TB65_HIST_SOLICIT.RetornaTodosRegistros() on tb64.CO_SOLI_ATEN equals tb65.CO_SOLI_ATEN
                            where tb65.CO_EMP == tb64.CO_EMP && tb65.CO_ALU == tb64.CO_ALU &&
                            tb65.CO_CUR == tb64.CO_CUR && (tb65.CO_SITU_SOLI == "A") && (coTipoSoli != 0 ? tb65.CO_TIPO_SOLI == coTipoSoli : coTipoSoli == 0)
                            select new
                            {
                                tb07.NU_NIRE, tb64.TB25_EMPRESA.sigla, tb64.CO_CUR, tb64.CO_ALU, tb64.CO_EMP, tb07.NO_ALU, tb64.TB108_RESPONSAVEL.NO_RESP,
                                tb64.DT_SOLI_ATEN, tb64.DT_PREV_ENTR, tb64.CO_SOLI_ATEN, tb64.NU_DCTO_SOLIC, tb65.TB66_TIPO_SOLIC.NO_TIPO_SOLI, tb65.TB66_TIPO_SOLIC.CO_TIPO_SOLI
                            }).OrderByDescending( s => s.DT_SOLI_ATEN ).ThenBy( s => s.NU_DCTO_SOLIC );
            if (resultado.Count() > 0)
            {
                var res = resultado.Where(r => !resultado.Any(a => (r != a && r.CO_SOLI_ATEN == a.CO_SOLI_ATEN)));
                CurrentPadraoBuscas.GridBusca.DataSource = res;
            }
        }

        protected void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {             
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoEmp, "CO_EMP"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoAlu, "CO_ALU"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoCur, "CO_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_SOLI_ATEN"));
            queryStringKeys.Add(new KeyValuePair<string, string>("tip", "CO_TIPO_SOLI"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Solicitações
        private void CarregaSolicitacoes()
        {
            ddlSolicitacoes.DataSource = TB66_TIPO_SOLIC.RetornaTodosRegistros().OrderBy( t => t.NO_TIPO_SOLI );
            ddlSolicitacoes.DataTextField = "NO_TIPO_SOLI";
            ddlSolicitacoes.DataValueField = "CO_TIPO_SOLI";
            ddlSolicitacoes.DataBind();

            ddlSolicitacoes.Items.Insert(0, new ListItem("Todas", ""));
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
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb01.CO_MODU_CUR == modalidade
                                            select tb01).OrderBy( c => c.NO_CUR );

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }
            else
                ddlSerieCurso.Items.Clear();

            ddlSerieCurso.Items.Insert(0, new ListItem("Todas", ""));
        }

//====> Método que carrega o DropDown de Turmas
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if ((modalidade != 0) && (serie != 0))
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR }).OrderBy( t => t.NO_TURMA );

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();

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
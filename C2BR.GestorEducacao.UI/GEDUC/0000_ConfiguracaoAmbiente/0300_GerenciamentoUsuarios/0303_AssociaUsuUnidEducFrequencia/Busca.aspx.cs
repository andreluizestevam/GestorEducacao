//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: GERENCIAMENTO DE USUÁRIOS DO GE
// OBJETIVO: ASSOCIA USUÁRIO A UNIDADES EDUCACIONAIS DE FREQUÊNCIA
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0300_GerenciamentoUsuarios.F0303_AssociaUsuUnidEducFrequencia
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
            if (!Page.IsPostBack)
                CarregaUsuario();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_COL" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_MAT_COL",
                HeaderText = "Matrícula"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_COL",
                HeaderText = "Usuário"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_FANTAS_EMP",
                HeaderText = "Unidade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CATEGORIA",
                HeaderText = "Categoria"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "SITUACAO",
                HeaderText = "Situação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coCol = ddlUsuarioAssoc.SelectedValue != "" ? int.Parse(ddlUsuarioAssoc.SelectedValue) : 0;
            switch (LoginAuxili.CO_TIPO_UNID)
            {
                case "PGE":
                    var resultado = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                     where tb03.CO_EMP == LoginAuxili.CO_EMP && (coCol != 0 ? tb03.CO_COL == coCol : coCol == 0)
                                     select new
                                     {
                                         CO_MAT_COL = tb03.CO_MAT_COL != "" ? tb03.CO_MAT_COL.Insert(5, "-").Insert(2, "-") : "",
                                         tb03.CO_COL,
                                         tb03.CO_EMP,
                                         tb03.NO_COL,
                                         tb03.TB25_EMPRESA.NO_FANTAS_EMP,
                                         CATEGORIA = tb03.FLA_PROFESSOR == "S" ? "Professor" : "Funcionário",
                                         SITUACAO = tb03.CO_SITU_COL == "ATI" ? "Atividade Interna" : (tb03.CO_SITU_COL == "ATE" ? "Atividade Externa" :
                                             (tb03.CO_SITU_COL == "FCE" ? "Cedido" : (tb03.CO_SITU_COL == "FES" ? "Estagiário" : (tb03.CO_SITU_COL == "LFR" ?
                                             "Licença Funcional" : (tb03.CO_SITU_COL == "LME" ? "Licença Médica" : (tb03.CO_SITU_COL == "LMA" ? "Licença Maternidade" :
                                             (tb03.CO_SITU_COL == "SUS" ? "Suspenso" : (tb03.CO_SITU_COL == "TRE" ? "Treinamento" : (tb03.CO_SITU_COL == "FER" ? "Férias" :
                                             (tb03.CO_SITU_COL))))))))))
                                     }).OrderBy(c => c.NO_COL).ThenBy(c => c.NO_FANTAS_EMP);

                    CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
                    break;
                case "PGS":
                    var resultadoSaude = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                     where tb03.CO_EMP == LoginAuxili.CO_EMP && (coCol != 0 ? tb03.CO_COL == coCol : coCol == 0)
                                     select new
                                     {
                                         CO_MAT_COL = tb03.CO_MAT_COL != "" ? tb03.CO_MAT_COL.Insert(5, "-").Insert(2, "-") : "",
                                         tb03.CO_COL,
                                         tb03.CO_EMP,
                                         tb03.NO_COL,
                                         tb03.TB25_EMPRESA.NO_FANTAS_EMP,
                                         CATEGORIA = tb03.FLA_PROFESSOR == "S" ? "Prof Saúde" : "Funcionário",
                                         SITUACAO = tb03.CO_SITU_COL == "ATI" ? "Atividade Interna" : (tb03.CO_SITU_COL == "ATE" ? "Atividade Externa" :
                                             (tb03.CO_SITU_COL == "FCE" ? "Cedido" : (tb03.CO_SITU_COL == "FES" ? "Estagiário" : (tb03.CO_SITU_COL == "LFR" ?
                                             "Licença Funcional" : (tb03.CO_SITU_COL == "LME" ? "Licença Médica" : (tb03.CO_SITU_COL == "LMA" ? "Licença Maternidade" :
                                             (tb03.CO_SITU_COL == "SUS" ? "Suspenso" : (tb03.CO_SITU_COL == "TRE" ? "Treinamento" : (tb03.CO_SITU_COL == "FER" ? "Férias" :
                                             (tb03.CO_SITU_COL))))))))))
                                     }).OrderBy(c => c.NO_COL).ThenBy(c => c.NO_FANTAS_EMP);

                    CurrentPadraoBuscas.GridBusca.DataSource = (resultadoSaude.Count() > 0) ? resultadoSaude : null;
                    break;

            }



        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoCol, "CO_COL"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

        //====> Método que carrega o DropDown de Usuários
        private void CarregaUsuario()
        {
            ddlUsuarioAssoc.DataSource = (from c in TB03_COLABOR.RetornaTodosRegistros()
                                          where c.CO_EMP.Equals(LoginAuxili.CO_EMP)
                                          select new { c.CO_COL, c.NO_COL }).OrderBy(p => p.NO_COL);
            ddlUsuarioAssoc.DataTextField = "NO_COL";
            ddlUsuarioAssoc.DataValueField = "CO_COL";
            ddlUsuarioAssoc.DataBind();

            ddlUsuarioAssoc.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion
    }
}

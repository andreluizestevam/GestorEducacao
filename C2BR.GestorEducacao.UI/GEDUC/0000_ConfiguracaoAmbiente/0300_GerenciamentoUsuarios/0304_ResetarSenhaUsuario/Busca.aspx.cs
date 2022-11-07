//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: GERENCIAMENTO DE USUÁRIOS DO GE
// OBJETIVO: MANUTENÇÃO DE USUÁRIOS DO SISTEMA.
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0300_GerenciamentoUsuarios.F0304_ResetarSenhaUsuario
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
                CarregaUnidade();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ideAdmUsuario", "CO_COL", "tipo" };

            BoundField bfMatricula = new BoundField();
            bfMatricula.DataField = "CO_MAT_COL";
            bfMatricula.HeaderText = "Matrícula";
            bfMatricula.ItemStyle.CssClass = "codCol";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfMatricula);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_COL",
                HeaderText = "Usuário"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_USU",
                HeaderText = "Login"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "classificacao",
                HeaderText = "Class",
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "tipo",
                HeaderText = "Tipo",
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "situacao",
                HeaderText = "Situação",
            });
        
            BoundField bfRealizado = new BoundField();
            bfRealizado.DataField = "DataStatus";
            bfRealizado.HeaderText = "Dt Situação";
            bfRealizado.ItemStyle.CssClass = "codCol";
            bfRealizado.DataFormatString = "{0:d}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado);
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coEmp = ddlUnidadeMUS.SelectedValue != "" ? int.Parse(ddlUnidadeMUS.SelectedValue) : 0;
            string strClasUsu = ddlClassUsuarioMUS.SelectedValue;
            string strStatusUsu = ddlStatusUsuarioMUS.SelectedValue;
            string strTipoUsu = ddlTipoUsuarioMUS.SelectedValue;

            var resultado = (from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                             join tb03 in TB03_COLABOR.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb03.CO_COL
                             where admUsuario.CO_EMP == coEmp && tb03.CO_EMP == admUsuario.CO_EMP
                             && (strClasUsu != "" ? admUsuario.ClassifUsuario == strClasUsu : strClasUsu == "")
                             && (strStatusUsu != "" ? admUsuario.SituUsuario == strStatusUsu : strStatusUsu == "")
                             && (strTipoUsu != "" ? admUsuario.TipoUsuario == strTipoUsu : strTipoUsu == "")
                             && (txtNomeUsuarioMUS.Text != "" ? tb03.NO_COL.Contains(txtNomeUsuarioMUS.Text) : txtNomeUsuarioMUS.Text == "")
                             && admUsuario.ideAdmUsuario != 60
                             && admUsuario.TipoUsuario != "R"
                             select new
                             {
                                 CO_COL = tb03.CO_COL,
                                 CO_MAT_COL = string.IsNullOrEmpty(tb03.CO_MAT_COL) ? "" : tb03.CO_MAT_COL.Insert(5, "-").Insert(2, "-"),
                                 NO_COL = tb03.NO_COL,
                                 DataStatus = admUsuario.DataStatus,
                                 ideAdmUsuario = admUsuario.ideAdmUsuario,
                                 classificacao = (admUsuario.ClassifUsuario == "C" ? "Comum" : (admUsuario.ClassifUsuario == "M" ? "Master" : 
                                 (admUsuario.ClassifUsuario == "S" ? "Suporte" : "Especial"))),
                                 tipo = (admUsuario.TipoUsuario == "S" || admUsuario.TipoUsuario == "P" ? "Professor" : (admUsuario.TipoUsuario == "F" ? "Funcionário" : admUsuario.TipoUsuario == "R" ? "Responsável" : "Outros")),
                                 situacao = (admUsuario.SituUsuario == "A" ? "Ativo" : (admUsuario.SituUsuario == "I" ? "Inativo" : "Suspenso")),                                
                                 NO_USU = admUsuario.desLogin,
                             }).Concat(
                                from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb108.CO_RESP
                                 where admUsuario.CO_EMP == coEmp && tb108.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == admUsuario.CodInstituicao
                                 && (strClasUsu != "" ? admUsuario.ClassifUsuario == strClasUsu : 0 == 0)
                                 && (strStatusUsu != "" ? admUsuario.SituUsuario == strStatusUsu : 0 == 0)
                                 && (strTipoUsu != "" ? admUsuario.TipoUsuario == strTipoUsu : strTipoUsu == "")
                                 && (!string.IsNullOrEmpty(txtNomeUsuarioMUS.Text) ? tb108.NO_RESP.Contains(txtNomeUsuarioMUS.Text): 0 == 0)
                                 && admUsuario.ideAdmUsuario != 60
                                 && admUsuario.TipoUsuario == "R"
                                select new
                                {
                                    CO_COL = (tb108.CO_RESP),
                                    CO_MAT_COL = "-",
                                    NO_COL = (tb108.NO_RESP),
                                    DataStatus = admUsuario.DataStatus,
                                    ideAdmUsuario = admUsuario.ideAdmUsuario,
                                    classificacao = (admUsuario.ClassifUsuario == "C" ? "Comum" : (admUsuario.ClassifUsuario == "M" ? "Master" :
                                    (admUsuario.ClassifUsuario == "S" ? "Suporte" : "Especial"))),
                                    tipo = (admUsuario.TipoUsuario == "S" || admUsuario.TipoUsuario == "P" ? "Professor" : (admUsuario.TipoUsuario == "F" ? "Funcionário" : admUsuario.TipoUsuario == "R" ? "Responsável" : "Outros")),
                                    situacao = (admUsuario.SituUsuario == "A" ? "Ativo" : (admUsuario.SituUsuario == "I" ? "Inativo" : "Suspenso")),
                                    NO_USU = admUsuario.desLogin
                                }).OrderBy(a => a.NO_COL);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ideAdmUsuario"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoCol, "CO_COL"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoMat, "tipo"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Unidades
        private void CarregaUnidade()
        {
            ddlUnidadeMUS.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                        join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                        where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                        select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidadeMUS.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeMUS.DataValueField = "CO_EMP";
            ddlUnidadeMUS.DataBind();

            ddlUnidadeMUS.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }
        #endregion
    }
}
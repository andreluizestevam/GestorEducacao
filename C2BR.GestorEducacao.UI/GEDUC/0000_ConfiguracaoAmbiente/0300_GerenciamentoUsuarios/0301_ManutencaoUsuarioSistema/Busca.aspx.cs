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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0300_GerenciamentoUsuarios.F0301_ManutencaoUsuarioSistema
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
                CarregaCamposUnidEscol();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_COL", "co_tipo" };

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
                             && (strClasUsu != "" ? admUsuario.ClassifUsuario == strClasUsu : 0 == 0)
                             && (strStatusUsu != "" ? admUsuario.SituUsuario == strStatusUsu : 0 == 0)
                             && (strTipoUsu != "0" ? admUsuario.TipoUsuario == strTipoUsu : 0 == 0)
                             && (!string.IsNullOrEmpty(txtNomeUsuarioMUS.Text) ? tb03.NO_COL.Contains(txtNomeUsuarioMUS.Text) : 0 == 0)
                             && admUsuario.TipoUsuario != "R"
                             && admUsuario.TipoUsuario != "A"
                             select new saida
                             {
                                 CO_COL = (tb03.CO_COL),
                                 CO_MAT_COL = string.IsNullOrEmpty(tb03.CO_MAT_COL) ? "-" : tb03.CO_MAT_COL.Insert(5, "-").Insert(2, "-"),
                                 NO_COL = (tb03.NO_COL),
                                 DataStatus = admUsuario.DataStatus,
                                 classificacao = (admUsuario.ClassifUsuario == "C" ? "Comum" : (admUsuario.ClassifUsuario == "M" ? "Master" :
                                 (admUsuario.ClassifUsuario == "S" ? "Suporte" : "Especial"))),
                                 co_tipo = admUsuario.TipoUsuario,
                                 situacao = (admUsuario.SituUsuario == "A" ? "Ativo" : (admUsuario.SituUsuario == "I" ? "Inativo" : "Suspenso")),
                                 NO_USU = admUsuario.desLogin,
                                 tp_unidade = LoginAuxili.CO_TIPO_UNID,
                             }).Concat(
                                from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb108.CO_RESP
                                where admUsuario.CO_EMP == coEmp && tb108.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == admUsuario.CodInstituicao
                                && (strClasUsu != "" ? admUsuario.ClassifUsuario == strClasUsu : 0 == 0)
                                && (strStatusUsu != "" ? admUsuario.SituUsuario == strStatusUsu : 0 == 0)
                                && (strTipoUsu != "0" ? admUsuario.TipoUsuario == strTipoUsu : 0 == 0)
                                && (!string.IsNullOrEmpty(txtNomeUsuarioMUS.Text) ? tb108.NO_RESP.Contains(txtNomeUsuarioMUS.Text) : 0 == 0)
                                && admUsuario.TipoUsuario == "R"
                                select new saida
                                {
                                    CO_COL = (tb108.CO_RESP),
                                    CO_MAT_COL = "-",
                                    NO_COL = (tb108.NO_RESP),
                                    DataStatus = admUsuario.DataStatus,
                                    classificacao = (admUsuario.ClassifUsuario == "C" ? "Comum" : (admUsuario.ClassifUsuario == "M" ? "Master" :
                                    (admUsuario.ClassifUsuario == "S" ? "Suporte" : "Especial"))),
                                    co_tipo = admUsuario.TipoUsuario,
                                    situacao = (admUsuario.SituUsuario == "A" ? "Ativo" : (admUsuario.SituUsuario == "I" ? "Inativo" : "Suspenso")),
                                    NO_USU = admUsuario.desLogin,
                                    tp_unidade = LoginAuxili.CO_TIPO_UNID,
                                }).Concat(
                                   from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                   join tb07 in TB07_ALUNO.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb07.CO_ALU
                                   where admUsuario.CO_EMP == coEmp && tb07.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == admUsuario.CodInstituicao
                                   && (strClasUsu != "" ? admUsuario.ClassifUsuario == strClasUsu : 0 == 0)
                                   && (strStatusUsu != "" ? admUsuario.SituUsuario == strStatusUsu : 0 == 0)
                                   && (strTipoUsu != "0" ? admUsuario.TipoUsuario == strTipoUsu : 0 == 0)
                                   && (!string.IsNullOrEmpty(txtNomeUsuarioMUS.Text) ? tb07.NO_ALU.Contains(txtNomeUsuarioMUS.Text) : 0 == 0)
                                   && admUsuario.TipoUsuario == "A"
                                   select new saida
                                   {
                                       CO_COL = (tb07.CO_ALU),
                                       CO_MAT_COL = "-",
                                       NO_COL = (tb07.NO_ALU),
                                       DataStatus = admUsuario.DataStatus,
                                       classificacao = (admUsuario.ClassifUsuario == "C" ? "Comum" : (admUsuario.ClassifUsuario == "M" ? "Master" :
                                       (admUsuario.ClassifUsuario == "S" ? "Suporte" : "Especial"))),
                                       co_tipo = admUsuario.TipoUsuario,
                                       situacao = (admUsuario.SituUsuario == "A" ? "Ativo" : (admUsuario.SituUsuario == "I" ? "Inativo" : "Suspenso")),
                                       NO_USU = admUsuario.desLogin,
                                       tp_unidade = LoginAuxili.CO_TIPO_UNID,
                                   }).OrderBy(a => a.NO_COL);

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        public class saida
        {
            public int CO_COL { get; set; }
            public string CO_MAT_COL { get; set; }
            public string NO_COL { get; set; }
            public DateTime DataStatus { get; set; }
            public string classificacao { get; set; }
            public string situacao { get; set; }
            public string tp_unidade { get; set; }
            public string co_tipo { get; set; }
            public string tipo
            {
                get
                {
                    if (this.tp_unidade == "PGS")
                        return (this.co_tipo == "P" ? "Prof. Saúde" : (this.co_tipo == "F" ? "Funcionário" : "Outros"));
                    else
                        return (this.co_tipo == "P" || this.co_tipo == "S" ? "Professor" : (this.co_tipo == "F" ? "Funcionário" : (this.co_tipo == "R" ? "Responsável" : "Outros")));
                }
            }
            public string NO_USU { get; set; }
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoCol, "CO_COL"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoEmp, ddlUnidadeMUS.SelectedValue));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "co_tipo"));

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

        /// <summary>
        /// Carrega a categoria funcional de acordo com o tipo de unidade logada
        /// </summary>
        private void CarregaCamposUnidEscol()
        {
            switch (LoginAuxili.CO_TIPO_UNID)
            {
                case "PGS":
                    ddlTipoUsuarioMUS.Items.Clear();
                    ddlTipoUsuarioMUS.Items.Insert(0, new ListItem("Outro", "O"));
                    ddlTipoUsuarioMUS.Items.Insert(0, new ListItem("Profissional Saúde", "P"));
                    ddlTipoUsuarioMUS.Items.Insert(0, new ListItem("Funcionário", "F"));
                    ddlTipoUsuarioMUS.Items.Insert(0, new ListItem("Estagiário(a)", "E"));
                    ddlTipoUsuarioMUS.Items.Insert(0, new ListItem("Todos", "0"));
                    break;
                case "PGE":
                default:
                    ddlTipoUsuarioMUS.Items.Clear();
                    ddlTipoUsuarioMUS.Items.Insert(0, new ListItem("Outro", "O"));
                    ddlTipoUsuarioMUS.Items.Insert(0, new ListItem("Aluno", "A"));
                    ddlTipoUsuarioMUS.Items.Insert(0, new ListItem("Responsável", "R"));
                    ddlTipoUsuarioMUS.Items.Insert(0, new ListItem("Professor(a)", "S"));
                    ddlTipoUsuarioMUS.Items.Insert(0, new ListItem("Funcionário", "F"));
                    ddlTipoUsuarioMUS.Items.Insert(0, new ListItem("Estagiário(a)", "E"));
                    ddlTipoUsuarioMUS.Items.Insert(0, new ListItem("Todos", "0"));
                    break;
            }
        }

        #endregion
    }
}
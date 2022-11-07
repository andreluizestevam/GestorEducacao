//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: GERENCIAMENTO DE USUÁRIOS DO GE
// OBJETIVO: ASSOCIA USUÁRIO A UNIDADE EDUCACIONAL E PERFIL DE ACESSO.
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0300_GerenciamentoUsuarios.F0302_AssociaUsuUnidEducPerfilAcesso
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
            {
                CarregaUnidade();
                CarregaUsuario();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "IDE_USREMP" };

            BoundField bf0 = new BoundField();
            bf0.DataField = "sigla";
            bf0.HeaderText = "UNI ORI";
            bf0.ItemStyle.Width = 40;
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf0);

            BoundField bf1 = new BoundField();
            bf1.DataField = "NO_COL";
            bf1.HeaderText = "Funcionário";
            bf1.ItemStyle.Width = 190;
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf1);

            BoundField bf2 = new BoundField();
            bf2.DataField = "NO_FANTAS_EMP";
            bf2.HeaderText = "Unidade";
            bf2.ItemStyle.Width = 170;
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf2);

            BoundField bf3 = new BoundField();
            bf3.DataField = "nomeTipoPerfilAcesso";
            bf3.HeaderText = "Perfil";
            bf3.ItemStyle.Width = 110;
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf3);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "situacao",
                HeaderText = "Status"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "FLA_ORIGEM",
                HeaderText = "UP"
            });

            CurrentPadraoBuscas.GridBusca.PageSize = 12;
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int idUsuario = ddlUsuarioAssoc.SelectedValue != "" ? int.Parse(ddlUsuarioAssoc.SelectedValue) : 0;
            int idEmp = ddlEmp.SelectedValue != "" ? int.Parse(ddlEmp.SelectedValue) : 0;
            var resultado = (from tb134 in TB134_USR_EMP.RetornaTodosRegistros()
                             join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb134.ADMUSUARIO.CodUsuario equals tb03.CO_COL
                             where tb134.AdmPerfilAcesso.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             && (idUsuario != 0 ? tb134.ADMUSUARIO.ideAdmUsuario == idUsuario : 0 == 0)
                             && (idEmp != 0 ? tb134.TB25_EMPRESA.CO_EMP == idEmp : 0==0)
                             && tb134.IDE_USREMP != 1
                             && tb134.ADMUSUARIO.TipoUsuario != "R"
                             select new
                             {
                                sigla = tb03.TB25_EMPRESA.sigla , NO_COL = tb03.NO_COL, NO_FANTAS_EMP = tb134.TB25_EMPRESA.NO_FANTAS_EMP, nomeTipoPerfilAcesso = tb134.AdmPerfilAcesso.nomeTipoPerfilAcesso, IDE_USREMP = tb134.IDE_USREMP,
                                situacao = (tb134.FLA_STATUS == "A" ? "Ativo" : "Inativo"), FLA_ORIGEM = (tb134.FLA_ORIGEM == "S" ? "Sim" : "Não")
                             }).Concat(
                               from tb134 in TB134_USR_EMP.RetornaTodosRegistros()
                               join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tb134.ADMUSUARIO.CodUsuario equals tb108.CO_RESP
                               where tb134.AdmPerfilAcesso.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                               && (idUsuario != 0 ? tb134.ADMUSUARIO.ideAdmUsuario == idUsuario : 0 == 0)
                               && (idEmp != 0 ? tb134.TB25_EMPRESA.CO_EMP == idEmp : 0 == 0)
                               && tb134.IDE_USREMP != 1
                               && tb134.ADMUSUARIO.TipoUsuario == "R"
                               select new
                               {
                                   sigla = tb134.TB25_EMPRESA.sigla,
                                   NO_COL = tb108.NO_RESP,
                                   NO_FANTAS_EMP = tb134.TB25_EMPRESA.NO_FANTAS_EMP,
                                   nomeTipoPerfilAcesso = tb134.AdmPerfilAcesso.nomeTipoPerfilAcesso,
                                   IDE_USREMP = tb134.IDE_USREMP,
                                   situacao = (tb134.FLA_STATUS == "A" ? "Ativo" : "Inativo"),
                                   FLA_ORIGEM = (tb134.FLA_ORIGEM == "S" ? "Sim" : "Não")
                               }
                             ).Concat(
                               from tb134 in TB134_USR_EMP.RetornaTodosRegistros()
                               join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb134.ADMUSUARIO.CodUsuario equals tb07.CO_ALU
                               where tb134.AdmPerfilAcesso.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                               && (idUsuario != 0 ? tb134.ADMUSUARIO.ideAdmUsuario == idUsuario : 0 == 0)
                               && (idEmp != 0 ? tb134.TB25_EMPRESA.CO_EMP == idEmp : 0 == 0)
                               && tb134.IDE_USREMP != 1
                               && tb134.ADMUSUARIO.TipoUsuario == "A"
                               select new
                               {
                                   sigla = tb134.TB25_EMPRESA.sigla,
                                   NO_COL = tb07.NO_ALU,
                                   NO_FANTAS_EMP = tb134.TB25_EMPRESA.NO_FANTAS_EMP,
                                   nomeTipoPerfilAcesso = tb134.AdmPerfilAcesso.nomeTipoPerfilAcesso,
                                   IDE_USREMP = tb134.IDE_USREMP,
                                   situacao = (tb134.FLA_STATUS == "A" ? "Ativo" : "Inativo"),
                                   FLA_ORIGEM = (tb134.FLA_ORIGEM == "S" ? "Sim" : "Não")
                               }
                             ).OrderBy( u => u.NO_FANTAS_EMP ).ThenBy( u => u.NO_COL );

            CurrentPadraoBuscas.GridBusca.DataSource = (

                resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "IDE_USREMP"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregamento DropDown

        private void CarregaUnidade()
        {
            ddlEmp.Items.Clear();
            ddlEmp.DataSource = (from emp in TB25_EMPRESA.RetornaTodosRegistros()
                                 where emp.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                 select new
                                 {
                                     emp.CO_EMP, emp.NO_FANTAS_EMP
                                 }
                                     );
            ddlEmp.DataTextField = "NO_FANTAS_EMP";
            ddlEmp.DataValueField = "CO_EMP";
            ddlEmp.DataBind();
            ddlEmp.Items.Insert(0, new ListItem("Todos", ""));
        }
//====> Método que carrega o DropDown de Usuários
        private void CarregaUsuario()
        {
            int codigoEmp = ddlEmp.SelectedValue == "" ? 0 : int.Parse(ddlEmp.SelectedValue);
            TB25_EMPRESA tb25Resp = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP); 
            ddlUsuarioAssoc.Items.Clear();
            ddlUsuarioAssoc.DataSource = (from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                          join tb03 in TB03_COLABOR.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb03.CO_COL
                                          join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb03.CO_EMP equals tb25.CO_EMP
                                          where admUsuario.CodInstituicao == LoginAuxili.ORG_CODIGO_ORGAO
                                          && (codigoEmp == 0 ? 0 == 0 : tb03.CO_EMP == codigoEmp)
                                          && admUsuario.TipoUsuario != "R"
                                          && admUsuario.TipoUsuario != "A"
                                          select new usuariosSaida { ID = admUsuario.ideAdmUsuario, NO_COL = tb03.NO_COL, NO_EMP = tb25.sigla }
                                          ).Concat(from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                                   join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb108.CO_RESP
                                                   where admUsuario.CodInstituicao == LoginAuxili.ORG_CODIGO_ORGAO
                                                   && admUsuario.TipoUsuario == "R"
                                                   select new usuariosSaida { ID = admUsuario.ideAdmUsuario, NO_COL = tb108.NO_RESP, NO_EMP = tb25Resp.sigla }
                                          ).Concat(from admUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                                   join tb07 in TB07_ALUNO.RetornaTodosRegistros() on admUsuario.CodUsuario equals tb07.CO_ALU
                                                   where admUsuario.CodInstituicao == LoginAuxili.ORG_CODIGO_ORGAO
                                                   && (codigoEmp == 0 ? 0 == 0 : tb07.CO_EMP == codigoEmp)
                                                   && admUsuario.TipoUsuario == "A"
                                                   select new usuariosSaida { ID = admUsuario.ideAdmUsuario, NO_COL = tb07.NO_ALU, NO_EMP = tb25Resp.sigla }
                                          ).OrderBy(c => c.NO_COL);

            ddlUsuarioAssoc.DataTextField = "CONCAT_NOME";
            ddlUsuarioAssoc.DataValueField = "ID";
            ddlUsuarioAssoc.DataBind();

            ddlUsuarioAssoc.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion

        protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUsuario();
        }

        public class usuariosSaida
        {
            public int ID { get; set; }
            public string NO_COL { get; set; }
            public string NO_EMP { get; set; }
            public string CONCAT_NOME
            {
                get
                {
                    return this.NO_EMP + " - " + this.NO_COL;
                }
            }
        }
    }
}

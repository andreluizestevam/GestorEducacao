//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE BIBLIOTECA
// SUBMÓDULO: RESERVA DE EMPRÉSTIMO DE ACERVO
// OBJETIVO: RESERVA DE ITEM DE ACERVO
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
namespace C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4200_CtrlInformReservaBilblioteca.F4201_ReservaItemAcervo
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

        protected void Page_Load()
        {
            if (IsPostBack) return;

            CarregaUnidade();
            CarregaUsuario();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_RESERVA_BIBLIOTECA" };

            BoundField bfCod = new BoundField();
            bfCod.DataField = "CO_RESERVA_BIBLIOTECA";
            bfCod.HeaderText = "Código";
            bfCod.ItemStyle.CssClass = "codCol";
            bfCod.DataFormatString = "{0:d}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfCod);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_USU_BIB",
                HeaderText = "Usuário"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "FLA_AVISAR_SMS_RESERVA",
                HeaderText = "Avisar SMS"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "FLA_AVISAR_EMAIL_RESERVA",
                HeaderText = "Avisar E-mail"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DT_RESERVA",
                HeaderText = "Dt Reserva",
                DataFormatString = "{0:d}"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DT_LIMITE_NECESSI_RESERVA",
                HeaderText = "Lim. Necessidade",
                DataFormatString = "{0:d}"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coUsuarioBibli = ddlUsuario.SelectedValue != "" ? int.Parse(ddlUsuario.SelectedValue) : 0;

            var resultado = (from tb206 in TB206_RESERVA_BIBLIOTECA.RetornaTodosRegistros()
                            where (coUsuarioBibli == 0 ? 0 == 0 : tb206.TB205_USUARIO_BIBLIOT.CO_USUARIO_BIBLIOT == coUsuarioBibli)
                            && tb206.TB03_COLABOR.CO_EMP == LoginAuxili.CO_EMP
                            select new
                            {
                                tb206.CO_RESERVA_BIBLIOTECA, tb206.TB205_USUARIO_BIBLIOT.NO_USU_BIB, tb206.DT_RESERVA, tb206.DT_LIMITE_NECESSI_RESERVA,
                                FLA_AVISAR_EMAIL_RESERVA = tb206.FLA_AVISAR_EMAIL_RESERVA == "S" ? "Sim" : "Não",
                                FLA_AVISAR_SMS_RESERVA = tb206.FLA_AVISAR_SMS_RESERVA == "S" ? "Sim" : "Não"
                            });

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_RESERVA_BIBLIOTECA"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion        

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Unidades
        private void CarregaUnidade()
        {
            ddlUnidadeUsuario.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                            select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP });

            ddlUnidadeUsuario.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeUsuario.DataValueField = "CO_EMP";
            ddlUnidadeUsuario.DataBind();

            ddlUnidadeUsuario.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

//====> Método que carrega o DropDown de Usuários
        private void CarregaUsuario()
        {
            int coEmp = ddlUnidadeUsuario.SelectedValue != "" ? int.Parse(ddlUnidadeUsuario.SelectedValue) : 0;

            string strTpUsuario = ddlTipoUsuario.SelectedValue;

            ddlUsuario.Items.Clear();

            switch (strTpUsuario)
            {
                case "A":
                    ddlUsuario.DataSource = (from tb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                             where (coEmp != 0 ? tb205.TB07_ALUNO.TB25_EMPRESA1.CO_EMP == coEmp : coEmp == 0)
                                             && tb205.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                             && tb205.TP_USU_BIB == "A"
                                             select new { tb205.CO_USUARIO_BIBLIOT, tb205.NO_USU_BIB }).OrderBy( u => u.NO_USU_BIB );

                    ddlUsuario.DataValueField = "CO_USUARIO_BIBLIOT";
                    ddlUsuario.DataTextField = "NO_USU_BIB";
                    ddlUsuario.DataBind();
                    break;
                case "P":
                    ddlUsuario.DataSource = (from tb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                             where (coEmp != 0 ? tb205.TB03_COLABOR.TB25_EMPRESA1.CO_EMP == coEmp : coEmp == 0)
                                             && tb205.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                             && tb205.TP_USU_BIB == "P"
                                             select new { tb205.CO_USUARIO_BIBLIOT, tb205.NO_USU_BIB }).OrderBy( u => u.NO_USU_BIB );

                    ddlUsuario.DataValueField = "CO_USUARIO_BIBLIOT";
                    ddlUsuario.DataTextField = "NO_USU_BIB";
                    ddlUsuario.DataBind();
                    break;
                case "F":
                    ddlUsuario.DataSource = (from tb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                             where (coEmp != 0 ? tb205.TB03_COLABOR.TB25_EMPRESA1.CO_EMP == coEmp : coEmp == 0)
                                             && tb205.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                             && tb205.TP_USU_BIB == "F"
                                             select new { tb205.CO_USUARIO_BIBLIOT, tb205.NO_USU_BIB }).OrderBy( u => u.NO_USU_BIB );

                    ddlUsuario.DataValueField = "CO_USUARIO_BIBLIOT";
                    ddlUsuario.DataTextField = "NO_USU_BIB";
                    ddlUsuario.DataBind();
                    break;
                case "O":
                    ddlUsuario.DataSource = (from tb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                             where (coEmp != 0 ? tb205.TB03_COLABOR.TB25_EMPRESA1.CO_EMP == coEmp : coEmp == 0)
                                             && tb205.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                             && tb205.TP_USU_BIB == "O"
                                             select new { tb205.CO_USUARIO_BIBLIOT, tb205.NO_USU_BIB }).OrderBy( u => u.NO_USU_BIB );

                    ddlUsuario.DataValueField = "CO_USUARIO_BIBLIOT";
                    ddlUsuario.DataTextField = "NO_USU_BIB";
                    ddlUsuario.DataBind();
                    break;
                default:
                    ddlUsuario.DataSource = (from tb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                             where (coEmp != 0 ? tb205.TB03_COLABOR.TB25_EMPRESA1.CO_EMP == coEmp : coEmp == 0) ||
                                             (coEmp != 0 ? tb205.TB07_ALUNO.TB25_EMPRESA1.CO_EMP == coEmp : coEmp == 0)
                                             && tb205.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                             select new { tb205.CO_USUARIO_BIBLIOT, tb205.NO_USU_BIB }).OrderBy( u => u.NO_USU_BIB );

                    ddlUsuario.DataValueField = "CO_USUARIO_BIBLIOT";
                    ddlUsuario.DataTextField = "NO_USU_BIB";
                    ddlUsuario.DataBind();
                    break;
            }
        }
        #endregion

        protected void ddlTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUsuario();
        }

        protected void ddlUnidadeUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUsuario();
        }
    }
}
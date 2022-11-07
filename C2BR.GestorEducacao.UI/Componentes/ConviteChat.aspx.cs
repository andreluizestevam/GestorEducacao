//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// OBJETIVO: TROCAR ESCOLA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas 
using System;
using System.Collections.Generic;
using System.Linq;
using Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Linq.Expressions;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Web.Security;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Componentes
{
    public partial class ConviteChat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CarregaUsuarios(object sender, EventArgs e)
        {
            var res = (from usu in ADMUSUARIO.RetornaTodosRegistros()
                       select new SaidaUsuario
                       {
                           idUsu = usu.ideAdmUsuario,
                           coUsu = usu.CodUsuario,
                           tpUsu = usu.TipoUsuario
                       });

            foreach (var r in res)
            {
                switch (r.tpUsu)
                {
                    case "P":
                        r.noUsu = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, r.coUsu).NO_COL;
                        break;
                    case "F":
                        r.noUsu = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, r.coUsu).NO_COL;
                        break;
                    case "A":
                        r.noUsu = TB07_ALUNO.RetornaPelaChavePrimaria(r.coUsu, LoginAuxili.CO_EMP).NO_ALU;
                        break;
                }
            }
        }

        public class SaidaUsuario
        {
            public int idUsu { get; set; }
            public int coUsu { get; set; }
            public string tpUsu { get; set; }
            public string noUsu { get; set; }
        }

        protected void lnkConvUsu_Click(object sender, EventArgs e)
        {
            AuxiliChat chat = new AuxiliChat();
        }
    }
}
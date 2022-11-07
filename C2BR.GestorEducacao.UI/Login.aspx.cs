//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 25/04/2013| André Nobre Vinagre        |Setado o valor do cnpj da instituição(cliente)
//           |                            |em uma variável de sessão
//           |                            |

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects;
using C2BR.GestorEducacao.LicenseValidator;
using System.Configuration;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI
{
    public partial class Login : System.Web.UI.Page
    {
        #region Métodos de Acesso

        protected void page_Init()
        {
            var ip = Library.Auxiliares.Util.RetornaIpCliente().ToString();
            ///If que verifica se foi parametrizado no web.config para mostrar aviso de chave de registro
            if (ConfigurationSettings.AppSettings.AllKeys.Where(f => f == "SistemaEmManutencao").Count() > 0)
            {
                Boolean manutencao = false;
                if (Boolean.TryParse(ConfigurationSettings.AppSettings["SistemaEmManutencao"].ToString(), out manutencao)
                    && manutencao)
                    AuxiliPagina.RedirecionaParaPaginaManutencao();
            }
        }

        /// <summary>
        /// Realiza o login do Portal Educação
        /// </summary>
        /// <param name="strDesLogin">login de acesso</param>
        /// <param name="strDesSenha">senha de acesso</param>
        protected void RealizaLogin(string strDesLogin, string strDesSenha)
        {
            try
            {
                LoginAuxili.resultadoLogin logar = LoginAuxili.RealizarLogin(strDesLogin, strDesSenha);
                if (!logar.mostrarDivFuncionalidade ?? true)
                    divTelaFuncionalidadesCarregando.Style.Add("display", "none");
                if ((logar.mostrarDivErro ?? false))
                {
                    lblMensagErro.Text = logar.labelErro;
                    divErroMsg.Style.Add("display", "block");
                }
                if (logar.redirecionarDefault ?? false)
                {
                    FormsAuthentication.RedirectFromLoginPage(logar.nomeUsuarioLogado, false);
                }
                if(logar.codigoOrgao != null)
                    hdfORG_CODIGO_ORGAO.Value = logar.codigoOrgao.ToString();
                if(logar.mostrarLinhaLicenca ?? false)
                    liLicenca.Style.Add("display", "block");
                if(logar.leftDivErro != null && logar.leftDivErro != "")
                    divErroMsg.Style.Add("left", logar.leftDivErro);
                if (logar.exErro != null && logar.exErro != "")
                {
                    divTelaFuncionalidadesCarregando.Style.Add("display", "none");
                    lblMensagErro.Text = logar.exErro;
                    divErroMsg.Style.Add("display", "block");
                }

            }
            catch (Exception f)
            {
                divTelaFuncionalidadesCarregando.Style.Add("display", "none");
                lblMensagErro.Text = f.Message;
                divErroMsg.Style.Add("display", "block");
            }
        }



        #endregion

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
//--------> Realiza o login ao site do usuário informado
            RealizaLogin(UserNMLOGINUSUARIO.Value, LoginAuxili.GerarMD5(UserVLSENHAUSUARIO.Value));            
        }

        protected void btnSendLicenca_Click(object sender, EventArgs e)
        {           
            int cod = hdfORG_CODIGO_ORGAO.Value != "" ? int.Parse(hdfORG_CODIGO_ORGAO.Value) : 0;

            if (cod != 0)
            {
                //divErroMsg.Style.Add("display", "none");                
                //divTelaFuncionalidadesCarregando.Style.Add("display", "block");

                var inst = (from i in TB000_INSTITUICAO.RetornaTodosRegistros()
                            where i.ORG_CODIGO_ORGAO == cod
                            select i).FirstOrDefault();

                inst.ORG_LICENCA = txtLicenca.Text;
                TB000_INSTITUICAO.SaveOrUpdate(inst);
                txtLicenca.Text = "";
                divTelaFuncionalidadesCarregando.Style.Add("display", "none");
                divErroMsg.Style.Add("display", "none"); 
            }
            else
            {
                liLicenca.Style.Add("display", "none");
                lblMensagErro.Text = "Instituição informada não encontrada.";
            }
        }
    }
}

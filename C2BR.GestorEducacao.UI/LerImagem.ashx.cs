//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.IO;
using System.Drawing;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    public class LerImagem : IHttpHandler
    {
        #region Propriedades

        public bool IsReusable { get { return false; } }
        #endregion

        #region Métodos

        public void ProcessRequest(HttpContext httpContext)
        {
            try
            {
                string valorQueryString = httpContext.Request.QueryString["idimg"];
                if (httpContext.Request.QueryString.AllKeys.Contains("idimg"))
                {
//----------------> Define o tipo de imagem a ser retornada
                    httpContext.Response.ContentType = "image/jpeg";

                    int idImage = int.Parse(httpContext.Request.QueryString["idimg"]);
//----------------> Faz a busca da imagem no banco
                    var tbImage = (from lTbImage in C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaTodosRegistros()
                                   where lTbImage.ImageId.Equals(idImage)
                                   select new { lTbImage.ImageStream }).FirstOrDefault();


                    if (tbImage != null)
                        httpContext.Response.BinaryWrite((byte[])tbImage.ImageStream);
                }
                //else if (httpContext.Request.QueryString["idAtest"] != "")
                else if (httpContext.Request.QueryString.AllKeys.Contains("idAtest"))
                {
                    //----------------> Define o tipo de imagem a ser retornada
                    httpContext.Response.ContentType = "image/jpeg";

                    int idAtest = int.Parse(httpContext.Request.QueryString["idAtest"]);
                    //----------------> Faz a busca da imagem no banco
                    var tbAtest = (from tbg101 in C2BR.GestorEducacao.BusinessEntities.MSSQL.TBG101_ATESTADOS.RetornaTodosRegistros()
                                   where tbg101.ID_ATEST.Equals(idAtest)
                                   select new { tbg101.IM_ATEST }).FirstOrDefault();


                    if (tbAtest != null)
                        httpContext.Response.BinaryWrite((byte[])tbAtest.IM_ATEST);
                }
            }
            catch (Exception exError)
            {
                //exError.Message;
            }
        }
        #endregion        
    }
}

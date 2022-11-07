//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Linq.Expressions;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Library.Componentes
{
    public partial class ControleImagem : System.Web.UI.UserControl
    {        
        #region Variáveis

        int codImgSalva = 0;
        int imgAltura;
        int imgLargura;
        bool mostraProcurar = true;
        #endregion

        #region Propriedades

        public string OperacaoCorrenteQueryString { get { return QueryStringAuxili.OperacaoCorrenteQueryString; } }

        public int ImageId { get; set; }

        public int ImagemAltura
        {
            get { return imgAltura; }
            set { imgAltura = value; }
        }

        public int ImagemLargura
        {
            get { return imgLargura; }
            set { imgLargura = value; }
        }

        public bool MostraProcurar
        {
            get { return mostraProcurar; }
            set { mostraProcurar = value; }
        }
        #endregion        

        #region Eventos

//----> ID_IMAGEM: caso o usuário esteja na tela de edição o ID da Imagem é carregado na variável "ID_IMAGEM"
//----> ID_IMAGEM_UPLOAD: caso o usuario escolha uma nova imagem, ou seja, alterar a Imagem carregada; a nova Imagem é carregado na variável "ID_IMAGEM_UPLOAD"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fupld.ToolTip = "Envie somente arquivos no formato .JPG";                
//------------> Define tamanho das imagens
                if (imgAltura == 0)
                    ImagemAltura = 85;

                if (imgLargura == 0)
                    ImagemLargura = 64;

                imgInclusao.Width = ImagemLargura;
                imgInclusao.Height = ImagemAltura;
                ShowImg.Width = ImagemLargura;
                ShowImg.Height = ImagemAltura;
                divBtnProcu.Visible = divImgProcu.Visible = divlblProcu.Visible = mostraProcurar;

                imgInclusao.Visible = false;
                fupld.Attributes.Add("onchange", "javascript:__doPostBack('ctl00$content$" + this.ID + "$btnPreview','select$01')");
                if (fupld.Visible == true)
                {
                    if (OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                    {
                        ShowImg.Visible = false;
                        imgInclusao.Visible = true;
                        imgInclusao.Width = ImagemLargura;
                        imgInclusao.Height = ImagemAltura;
                        divBtnProcu.Visible = divImgProcu.Visible = divlblProcu.Visible = mostraProcurar;
                        HttpContext.Current.Session.Remove("ID_IMAGEM_UPLOAD");
                        HttpContext.Current.Session.Remove("ID_IMAGEM");
                    }
                }

                if (OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao) && Session["ID_IMAGEM"] != null)
                {
                    if (this.ImageId.Equals(0))
                        ShowImg.ImageUrl = "../../LerImagem.ashx?idimg=" + Session["ID_IMAGEM"];
                    else
                        ShowImg.ImageUrl = "../../LerImagem.ashx?idimg=" + this.ImageId;

                    ShowImg.Width = ImagemLargura;
                    ShowImg.Height = ImagemAltura;
                    divBtnProcu.Visible = divImgProcu.Visible = divlblProcu.Visible = mostraProcurar;
                }
                else
                {
                    if (ImageId != 0)
                    {
                        CarregaImagem(ImageId);
                    }
                    else
                    {
                        CarregaImagem(0);
                    }

                    HttpContext.Current.Session.Remove("ID_IMAGEM_UPLOAD");
                    HttpContext.Current.Session.Remove("ID_IMAGEM");
                }
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método que faz o retorno da entidade informada
        /// </summary>
        /// <returns>Entidade Image</returns>
        private C2BR.GestorEducacao.BusinessEntities.MSSQL.Image RetornaEntidade()
        {
            C2BR.GestorEducacao.BusinessEntities.MSSQL.Image image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria((int)Session["ID_IMAGEM"]);
            return (image == null) ? new C2BR.GestorEducacao.BusinessEntities.MSSQL.Image() : image;
        }

        /// <summary>
        /// Método utilizado para carregar a imagem
        /// </summary>
        public void CarregaImagem()
        {
            HttpContext.Current.Session.Remove("ID_IMAGEM");

            if (!this.ImageId.Equals(0))
            {
                ShowImg.Visible = true;
                imgInclusao.Visible = false;
                ShowImg.ImageUrl = "../../LerImagem.ashx?idimg=" + this.ImageId;
            }
        }
        
        /// <summary>
        /// Método utilizado para carregar a imagem com o parâmetro de Id da imagem
        /// </summary>
        /// <param name="idImage">Id da imagem</param>
        public void CarregaImagem(int idImage)
        {
            HttpContext.Current.Session.Remove("ID_IMAGEM");

            if (idImage != 0)
            {
                HttpContext.Current.Session.Add("ID_IMAGEM", idImage);
                ShowImg.ImageUrl = "../../LerImagem.ashx?idimg=" + idImage;
            }
            else
            {
                HttpContext.Current.Session.Add("ID_IMAGEM", null);
                ShowImg.ImageUrl = "../../LerImagem.ashx?idimg=" + "";
            }

            ShowImg.Width = ImagemLargura;
            ShowImg.Height = ImagemAltura;
        }

//====> 
        /// <summary>
        /// Método utilizado para gravar a imagem na tabela Image
        /// </summary>
        /// <returns>Inteiro com o Id da imagem salva</returns>
        public int GravaImagem()
        {
//--------> Atualiza a imagem no banco
            if (Session["ID_IMAGEM"] != null && Session["ID_IMAGEM_UPLOAD"] != null)
            {
                try
                {
                    C2BR.GestorEducacao.BusinessEntities.MSSQL.Image image = RetornaEntidade();
                    image.ImageStream = (byte[])Session["ID_IMAGEM_UPLOAD"];
                    C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.SaveOrUpdate(image);
                    codImgSalva = (Int32)Session["ID_IMAGEM"];
                }
                catch
                {
                    HttpContext.Current.Session.Remove("ID_IMAGEM_UPLOAD");
                    lblmsg.Text = " Erro ao Atualizar a imagem ";
                }
            }
//--------> Grava a imagem no banco
            else if (Session["ID_IMAGEM"] == null && Session["ID_IMAGEM_UPLOAD"] != null)
            {
                try
                {
//----------------> Executa a gravação na base de dados
                    C2BR.GestorEducacao.BusinessEntities.MSSQL.Image entImage = new C2BR.GestorEducacao.BusinessEntities.MSSQL.Image();
                    entImage.ImageStream = (byte[])Session["ID_IMAGEM_UPLOAD"];
                    C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.SaveOrUpdate(entImage);

//----------------> Recebe o id da ultima imagem gravada
                    var imagemGravada = (from tbimage in C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaTodosRegistros()
                                         orderby tbimage.ImageId descending
                                         select new
                                         {
                                             tbimage.ImageId
                                         }).Take(1).FirstOrDefault();


                    if (imagemGravada != null)
                    {
                        codImgSalva = imagemGravada.ImageId;
                    }
                    HttpContext.Current.Session.Remove("ID_IMAGEM_UPLOAD");
                    lblmsg.Text = "<br />Arquivo enviado com sucesso <br />";                    
                }
                catch
                {
                    HttpContext.Current.Session.Remove("ID_IMAGEM_UPLOAD");
                    lblmsg.Text = " Erro ao gravar a imagem ";
                }
            }
//--------> Se item editado e a imagem não, é retornado o código da mesma
            else if (Session["ID_IMAGEM"] != null)
            {
                codImgSalva = (Int32)Session["ID_IMAGEM"];
            }

            return codImgSalva;
        }      
        #endregion     
        
//====> Método utilizado para visualizar a imagem selecionada quando clicado no botão
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            if (fupld.Visible == true)
            {
                if (fupld.PostedFile != null)
                {
                    if (fupld.PostedFile == null || string.IsNullOrEmpty(fupld.PostedFile.FileName) || fupld.PostedFile.InputStream == null)
                    {
                        lblmsg.Text = "<br />Erro - Não foi possível localizar o arquivo.<br />";
                    }
                    else
                    {
                        string extension = Path.GetExtension(fupld.PostedFile.FileName).ToLower();

                        if (extension != ".jpg")
                        {
                            lblmsg.Text = "Imagem não é do tipo .jpg.";
                        }
                        else
                        {
                            ShowImg.Visible = false;
                            imgInclusao.Visible = true;
                            imgInclusao.Src = "~/Library/IMG/Gestor_ImagemSalva.JPG";
                            byte[] imageBytes = new byte[fupld.PostedFile.InputStream.Length + 1];
                            fupld.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);

                            HttpContext.Current.Session.Remove("ID_IMAGEM_UPLOAD");
                            HttpContext.Current.Session.Add("ID_IMAGEM_UPLOAD", imageBytes);
                            lblmsg.Text = "";
                        }
                    }
                }
            }
        }
    }
}
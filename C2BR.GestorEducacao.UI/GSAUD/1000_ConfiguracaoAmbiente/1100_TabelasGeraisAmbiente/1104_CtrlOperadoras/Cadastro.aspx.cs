//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CIDADE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 10/12/14 | Maxwell Almeida            | Criação da funcionalidade para Cadastro de Operadoras


//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Data;
using Resources;
using System.IO;

namespace C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1104_CtrlOperadoras
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }
        #region Eventos
        string salvaDTSitu;
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            string coOper = txtCodOper.Text;
            string cnpj = txtCNPJ.Text.Replace(".", "").Replace("/", "").Replace("-", "");

            bool existeCCodigo = TB250_OPERA.RetornaTodosRegistros().Where(w => w.CO_OPER == coOper).Any();
            bool existeCCNPJ = TB250_OPERA.RetornaTodosRegistros().Where(w => w.NU_CNPJ_OPER == cnpj).Any();

            if (string.IsNullOrEmpty(txtNomeOper.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o Nome da Operadora");
                return;
            }
            if (string.IsNullOrEmpty(txtCNPJ.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o CNPJ da Operadora");
                return;
            }
            TB250_OPERA tb250 = RetornaEntidade();
            switch (tb250.EntityState)
            {
                case EntityState.Added:
                case EntityState.Detached:
                    if (existeCCodigo)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Já existe uma Operadora com o Código informado");
                        return;
                    }
                    if (existeCCNPJ)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Já existe uma Operadora com o CNPJ informado");
                        return;
                    }
                    break;
            }

            if (!AuxiliValidacao.ValidaCnpj(cnpj))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O CNPJ informado é invalido");
                return;
            }

            tb250.NOM_OPER = txtNomeOper.Text;
            tb250.CO_OPER = txtCodOper.Text;
            tb250.NU_CNPJ_OPER = txtCNPJ.Text.Replace(".", "").Replace("/", "").Replace("-", "");
            tb250.NM_SIGLA_OPER = txtSigla.Text;
            //Verifica se foi alterada a situação, e salva as informações caso tenha sido
            if (hidSituacao.Value != ddlSitu.SelectedValue)
            {
                tb250.ST_OPER = ddlSitu.SelectedValue;
                tb250.FL_SITU_OPER = ddlSitu.SelectedValue;
                tb250.DT_SITU_OPER = DateTime.Now;
                tb250.CO_COL_SITU = LoginAuxili.CO_COL;
                tb250.CO_EMP_SITU = LoginAuxili.CO_EMP;
                tb250.IP_SITU_OPER = Request.UserHostAddress;
            }

            //Verifica se é uma nova entidade, caso seja salva as informações pertinentes
            switch (tb250.EntityState)
            {
                case EntityState.Added:
                case EntityState.Detached:
                    tb250.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tb250.DT_CADAS = DateTime.Now;
                    tb250.IP_CADAS = Request.UserHostAddress;
                    tb250.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    break;
            }
            if (!String.IsNullOrEmpty(hidImg.Value))
                tb250.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(int.Parse(hidImg.Value));

            CurrentPadraoCadastros.CurrentEntity = tb250;
        }
        #endregion
        #region Métodos
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB250_OPERA tb250 = RetornaEntidade();

            if (tb250 != null)
            {
                CarregaImagem();
                hidId.Value = tb250.ID_OPER.ToString();
                txtCodOper.Enabled = false;
                txtSigla.Text = tb250.NM_SIGLA_OPER;
                txtNomeOper.Text = tb250.NOM_OPER;
                txtCNPJ.Text = tb250.NU_CNPJ_OPER;
                txtCodOper.Text = tb250.CO_OPER;
                ddlSitu.SelectedValue =
                hidSituacao.Value = tb250.FL_SITU_OPER;
                chkOperaInst.Checked = tb250.FL_INSTI_OPERA == "A" ? true : false;
            }
        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB250_OPERA</returns>
        private TB250_OPERA RetornaEntidade()
        {
            TB250_OPERA tb250 = TB250_OPERA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb250 == null) ? new TB250_OPERA() : tb250;
        }

        protected void fpNovaImg_OnDataBinding(object sender, EventArgs e)
        {
            if (fpNovaImg.HasFile)
            {
                if (fpNovaImg.PostedFile == null || string.IsNullOrEmpty(fpNovaImg.PostedFile.FileName) || fpNovaImg.PostedFile.InputStream == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Erro: Não foi possível localizar o arquivo.");
                    return;
                }
                else
                {
                    string extension = Path.GetExtension(fpNovaImg.PostedFile.FileName).ToLower();

                    if (extension != ".jpg")
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Erro: Imagem não é do tipo .jpg.");
                        return;
                    }
                    else
                    {
                        byte[] imageBytes = new byte[fpNovaImg.PostedFile.InputStream.Length + 1];
                        fpNovaImg.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);

                        C2BR.GestorEducacao.BusinessEntities.MSSQL.Image image = new C2BR.GestorEducacao.BusinessEntities.MSSQL.Image();

                        image.ImageStream = imageBytes;

                        C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.SaveOrUpdate(image, true);

                        hidImg.Value = image.ImageId.ToString();
                    }
                }
            }
        }
        protected void lnkPreviewImg_OnClick(object sender, EventArgs e)
        {
            var tb250 = RetornaEntidade();
            tb250.ImageReference.Load();

            if (fpNovaImg.HasFile)
            {
                if (fpNovaImg.PostedFile == null || string.IsNullOrEmpty(fpNovaImg.PostedFile.FileName) || fpNovaImg.PostedFile.InputStream == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Erro: Não foi possível localizar o arquivo.");
                    return;
                }
                else
                {
                    string extension = Path.GetExtension(fpNovaImg.PostedFile.FileName).ToLower();

                    if (extension != ".jpg")
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Erro: Imagem não é do tipo .jpg.");
                        return;
                    }
                    else
                    {
                        byte[] imageBytes = new byte[fpNovaImg.PostedFile.InputStream.Length + 1];
                        fpNovaImg.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);

                        if (tb250.Image != null)
                        {
                            C2BR.GestorEducacao.BusinessEntities.MSSQL.Image image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(tb250.Image.ImageId);

                            image.ImageStream = imageBytes;

                            C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.SaveOrUpdate(image, true);

                            tb250.Image = image;

                            hidImg.Value = image.ImageId.ToString();

                            CarregaImagem(tb250.Image.ImageId);
                        }
                        else
                        {
                            C2BR.GestorEducacao.BusinessEntities.MSSQL.Image entImage = new C2BR.GestorEducacao.BusinessEntities.MSSQL.Image();

                            entImage.ImageStream = imageBytes;

                            C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.SaveOrUpdate(entImage, true);

                            tb250.Image = entImage;

                            hidImg.Value = entImage.ImageId.ToString();

                            CarregaImagem(tb250.Image.ImageId);
                        }
                    }
                }
            }
            else
            {
                if (tb250.Image != null)
                {
                    CarregaImagem(tb250.Image.ImageId);
                }
            }
        }

        protected void lnkLimparImg_OnClick(object sender, EventArgs e)
        {
            var tb250 = RetornaEntidade();
            tb250.ImageReference.Load();

            if (tb250.Image != null)
            {
                C2BR.GestorEducacao.BusinessEntities.MSSQL.Image image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(tb250.Image.ImageId);

                tb250.Image = null;
                TB250_OPERA.SaveOrUpdate(tb250, true);

                C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.Delete(image, true);
            }

            CarregaImagem();
        }

        /// <summary>
        /// Método utilizado para carregar a imagem
        /// </summary>
        public void CarregaImagem()
        {
            var tb250 = RetornaEntidade();
            tb250.ImageReference.Load();

            if (tb250.Image != null)
            {
                hidImg.Value = tb250.Image.ImageId.ToString();
                imgLogo.Src = "~/LerImagem.ashx?idimg=" + tb250.Image.ImageId;
            }
            else
            {
                imgLogo.Src = "~/Library/IMG/Gestor_SemImagem.png";
            }
        }

        /// <summary>
        /// Método utilizado para carregar a imagem com o parâmetro de Id da imagem
        /// </summary>
        /// <param name="idImage">Id da imagem</param>
        public void CarregaImagem(int idImage)
        {
            if (idImage != 0)
            {
                imgLogo.Src = "~/LerImagem.ashx?idimg=" + idImage;
            }
            else
            {
                imgLogo.Src = "~/Library/IMG/Gestor_SemImagem.png";
            }
        }
        #endregion
    }
}
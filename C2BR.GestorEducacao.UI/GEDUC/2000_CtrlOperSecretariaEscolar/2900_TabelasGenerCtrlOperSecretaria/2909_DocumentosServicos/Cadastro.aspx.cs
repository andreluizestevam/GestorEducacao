using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.IO;


namespace C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2900_TabelasGenerCtrlOperSecretaria._2909_DocumentosServicos
{

    public class PaginaDoc
    {
        public int Pagina { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
    }


    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }


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
                gvPaginas.DataSource = new List<TB010_RTF_ARQUIVO>();
                gvPaginas.DataBind();

                //Altera os tipos para o caso de a empresa logada ser uma empresa de Saúde
                //if (TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP).CO_FLAG_ENSIN_CURSO == "N")
                //{
                //    ddlTipoDoc.Items.Clear();
                //    ddlTipoDoc.Items.Insert(0, new ListItem("Todos", "T"));
                //    ddlTipoDoc.Items.Insert(0, new ListItem("Atestado", "AM"));
                //    ddlTipoDoc.Items.Insert(0, new ListItem("Contrato", "CO"));
                //    ddlTipoDoc.Items.Insert(0, new ListItem("Declaração", "DE"));
                //    ddlTipoDoc.Items.Insert(0, new ListItem("Recibo", "RE"));
                //    ddlTipoDoc.Items.Insert(0, new ListItem("Outros", "OT"));
                //}

            }
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                txtDtSituacao.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (txtNome.Text.Length >= 40)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Nome deve ter no máximo 40 caracteres!");
                return;
            }
            if (txtTitulo.Text.Length >= 120)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Titulo deve ter no máximo 120 caracteres!");
                return;
            }

            if (txtDescricao.Text.Length >= 250)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Titulo deve ter no máximo 250 caracteres!");
                return;
            }

            TB009_RTF_DOCTOS tb009 = RetornaEntidade();

            tb009.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
            tb009.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            tb009.TP_DOCUM = ddlTipoDoc.SelectedValue;
            tb009.NM_DOCUM = txtNome.Text;
            tb009.CO_SIGLA_DOCUM = txtSiglaDoc.Text;
            tb009.NM_TITUL_DOCUM = txtTitulo.Text;
            tb009.DE_DOCUM = txtDescricao.Text;
            tb009.CO_SITUS_DOCUM = ddlStatus.SelectedValue;
            tb009.FL_HIDELOGO = ddlLogo.SelectedValue;
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                tb009.DT_CADAS_DOCUM = DateTime.Now;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoAlteracao))
            {
                var lstOcoTb010 = (from iTb010 in TB010_RTF_ARQUIVO.RetornaTodosRegistros()
                                   where iTb010.TB009_RTF_DOCTOS.ID_DOCUM == tb009.ID_DOCUM
                                   select iTb010).ToList();

                if (lstOcoTb010.Count() > 0)
                {
                    foreach (var iTb010 in lstOcoTb010)
                    {
                        GestorEntities.Delete(iTb010, true);
                    }
                }
            }

            TB009_RTF_DOCTOS.SaveOrUpdate(tb009, true);

            List<TB010_RTF_ARQUIVO> lst = (Session["ListaDoc"] as List<TB010_RTF_ARQUIVO>);
            if (lst != null)
            {
                foreach (TB010_RTF_ARQUIVO item in lst)
                {
                    //tb009.TB010_RTF_ARQUIVO.Add(item);
                    TB010_RTF_ARQUIVO tb010 = new TB010_RTF_ARQUIVO();

                    tb010.NM_PATH = item.NM_PATH;
                    tb010.NU_PAGINA = item.NU_PAGINA;
                    tb010.TB009_RTF_DOCTOS = tb009;
                    tb010.AR_DADOS = item.AR_DADOS;

                    TB010_RTF_ARQUIVO.SaveOrUpdate(tb010, true);
                };
            }
            Session.Remove("ListaDoc");

            CurrentPadraoCadastros.CurrentEntity = tb009;
        }

        #region Métodos
        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB009_RTF_DOCTOS tb009 = RetornaEntidade();

            if (tb009 != null)
            {
                var tb010 = (from itb010 in TB010_RTF_ARQUIVO.RetornaTodosRegistros()
                             where itb010.TB009_RTF_DOCTOS.ID_DOCUM == tb009.ID_DOCUM
                             select itb010).ToList();


                ddlTipoDoc.SelectedValue = tb009.TP_DOCUM;
                txtNome.Text = tb009.NM_DOCUM;
                txtSiglaDoc.Text = tb009.CO_SIGLA_DOCUM;
                txtTitulo.Text = tb009.NM_TITUL_DOCUM;
                txtDescricao.Text = tb009.DE_DOCUM;
                txtDtSituacao.Text = tb009.DT_CADAS_DOCUM.ToString("dd/MM/yyyy");
                ddlStatus.SelectedValue = tb009.CO_SITUS_DOCUM;
                ddlLogo.SelectedValue = tb009.FL_HIDELOGO;
                Session["ListaDoc"] = tb010;
                gvPaginas.DataSource = tb010;
                gvPaginas.DataBind();
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB009_DOCTOS_RTF</returns>
        private TB009_RTF_DOCTOS RetornaEntidade()
        {
            TB009_RTF_DOCTOS tb009 = TB009_RTF_DOCTOS.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb009 == null) ? new TB009_RTF_DOCTOS() : tb009;
        }
        #endregion

        /// <summary>
        /// Executa Upload do arquivo selecionado
        /// </summary>
        /// <returns>void</returns>
        protected void UploadFile(FileUpload sender)
        {
            string str = "";

            if (sender.HasFile)
            {
                using (StreamReader srLeitor = new StreamReader(sender.PostedFile.InputStream))
                {
                    //srLeitor.BaseStream.Seek(0, SeekOrigin.Begin);
                    srLeitor.BaseStream.Position = 0;
                    str = srLeitor.ReadToEnd();

                    //while ((str = srLeitor.))
                    //{
                    //    str = str + srLeitor.ReadLine();
                    //}
                    srLeitor.Close();
                }

                List<TB010_RTF_ARQUIVO> lst;
                if (Session["ListaDoc"] == null)
                    lst = new List<TB010_RTF_ARQUIVO>();
                else
                    lst = (Session["ListaDoc"] as List<TB010_RTF_ARQUIVO>);

                if (lst.Count < 12)
                {
                    lst.Add(new TB010_RTF_ARQUIVO
                    {
                        NU_PAGINA = lst.Count + 1,
                        NM_PATH = NomeArquivo.PostedFile.FileName,
                        AR_DADOS = str
                    });
                    Session["ListaDoc"] = lst;
                    gvPaginas.DataSource = lst;
                    gvPaginas.DataBind();
                }
                else
                {
                  // Criar Messagem Informando que ultapassou 12 páginas, para entrar em contato com o suporte. Por: Wellington
                }
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            UploadFile(NomeArquivo);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            List<TB010_RTF_ARQUIVO> lst = (Session["ListaDoc"] as List<TB010_RTF_ARQUIVO>);
            if (lst != null)
            {
                lst.Clear();
                Session["ListaDoc"] = lst;
                gvPaginas.DataSource = lst;
                gvPaginas.DataBind();
            }
        }
    }
}
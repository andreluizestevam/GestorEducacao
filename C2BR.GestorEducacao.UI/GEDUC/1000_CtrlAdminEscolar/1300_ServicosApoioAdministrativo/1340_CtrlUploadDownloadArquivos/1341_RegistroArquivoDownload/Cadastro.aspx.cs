//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: CONTROLE UPLOAD / DOWNLOAD ARQUIVOS
// OBJETIVO: REGISTRO DE ARQUIVOS PARA DOWNLOAD
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
using System.IO;
using System.Data.Objects.DataClasses;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1340_CtrlUploadDownloadArquivos.F1341_RegistroArquivoDownload
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Propriedades

        public IEnumerable<int> CurrentIdUnidades { get { return (IEnumerable<int>)Session["GerenciarArquivosCurrentIds"]; } set { Session["GerenciarArquivosCurrentIds"] = value; } }

        #endregion

        #region Variaveis
         
        string nomeArquivo = "/ArquivosPublicados/";

        #endregion

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            CarregaValoresPadrao();

            if (!IsPostBack)
                CarregaListaDeUnidades();
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão e Alteração de Registros na Entidade do BD, após a ação de salvar
        protected void btnGravar_Click(object sender, EventArgs e)
        {
            RetornaEntidadeAtualizada();                
        }

//====> Processo de Exclusão de Registros na Entidade do BD, após a ação de salvar    
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ArquivoCompartilhado arqComp = RetornaEntidade();

            GerenciaExclusao(arqComp.ArquivoCompartilhadoId);

            ArquivoCompartilhado arqComp2 = RetornaEntidade();
            ArquivoCompartilhado.Delete(arqComp2, true);
            AuxiliPagina.RedirecionaParaPaginaMensagem("Arquivo excluído com sucesso.", Request.Url.AbsolutePath.ToLower().Replace("cadastro", "busca") + "&moduloNome=" + Request.QueryString["moduloNome"], C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Sucess);
        }

//====> Processo de Redirecionamento para tela de Busca
        protected void btnNewSearch_Click(object sender, EventArgs e)
        {
            AuxiliPagina.RedirecionaParaPaginaBusca();
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Chamada do método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            ArquivoCompartilhado arqCom = RetornaEntidade();

            txtArquivoNome.Text = arqCom.NomeArquivo;
            txtDescricao.Text = arqCom.Descricao;
            txtDataValidade.Text = arqCom.DataValidade.ToString("dd/MM/yyyy");
            txtDataPublicacao.Text = arqCom.DataPublicacao.ToString("dd/MM/yyyy");
            txtDataCadastro.Text = arqCom.DataCriacao.ToString("dd/MM/yyyy");
            chkAtivo.Checked = arqCom.Ativo;

            //this.CurrentIdUnidades = CarregaUnidadesAtuais(arqCom.ArquivoCompartilhadoId);
            CarregaUnidadesAtuais(arqCom.ArquivoCompartilhadoId);
        }

        /// <summary>
        /// Método que faz a chamada de outro método que faz a validação customizada
        /// </summary>
        public override void Validate()
        {
            base.Validate();
            ExecutaValidacaoCustomizada();
        }

        /// <summary>
        /// Método que faz a validação customizada
        /// </summary>
        private void ExecutaValidacaoCustomizada()
        {
//--------> Validações
            if (txtDescricao.Text.Length > 1024)
                AuxiliPagina.EnvioMensagemErro(this, "Descrição deve conter, no máximo, 1024 caracteres.");
            if (txtArquivoNome.Text.Length > 1024)
                AuxiliPagina.EnvioMensagemErro(this, "Nome do Arquivo deve conter, no máximo, 100 caracteres.");

            DateTime dataValidade = DateTime.Now;
            if (!DateTime.TryParse(txtDataValidade.Text, out dataValidade))
                AuxiliPagina.EnvioMensagemErro(this, "A Data de Validade informada não é uma data válida.");

            if (dataValidade.Date <= DateTime.Now.AddDays(1).Date || dataValidade.Date >= DateTime.Now.AddYears(4).Date)
                AuxiliPagina.EnvioMensagemErro(this, String.Format("A Data de Validade deve estar entre amanhã e {0}.", DateTime.Now.AddYears(4).ToString("dd/MM/yyyy")));

            DateTime dataCadastro = DateTime.Now;
            if (!DateTime.TryParse(txtDataCadastro.Text, out dataCadastro))
                AuxiliPagina.EnvioMensagemErro(this, "A Data de Cadastro informada não é uma data válida.");

            DateTime dataPublicacao = DateTime.Now;
            if (!DateTime.TryParse(txtDataPublicacao.Text, out dataPublicacao))
                AuxiliPagina.EnvioMensagemErro(this, "A Data de Publicação informada não é uma data válida.");

            if (dataPublicacao.Date < DateTime.Today.Date || dataPublicacao.Date > dataValidade.Date)
                AuxiliPagina.EnvioMensagemErro(this, "A Data de Publicação deve estar entre hoje e a Data de Validade.");

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                if (fuArquivoPublicar.FileBytes.Length <= 0)
                    AuxiliPagina.EnvioMensagemErro(this, "É necessário selecionar um arquivo para ser enviado.");
        }

        /// <summary>
        /// Método que carrega valores padrões
        /// </summary>
        private void CarregaValoresPadrao()
        {
            txtDataCadastro.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDataValidade.Text = DateTime.Now.AddMonths(1).ToString("dd/MM/yyyy");
            txtDataPublicacao.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Atualiza as propriedades da entidade de acordo com os valores dos campos do formulário.
        /// </summary>
        private void RetornaEntidadeAtualizada()
        {
            ArquivoCompartilhado arqCom = RetornaEntidade();
            
//--------> Só faz a alteração da data de criação quanto é um novo registro
            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                arqCom.DataCriacao = DateTime.Now;

            arqCom.URL = GerenciaArquivo(arqCom.URL);
            arqCom.NomeArquivo = txtArquivoNome.Text;
            arqCom.Descricao = txtDescricao.Text;
            arqCom.Ativo = chkAtivo.Checked;
            arqCom.DataValidade = DateTime.Parse(txtDataValidade.Text);
            arqCom.DataPublicacao = DateTime.Parse(txtDataPublicacao.Text);
            arqCom.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            GerenciaUnidadesDeCompartilhamento(ArquivoCompartilhado.SaveOrUpdate(arqCom));
        }

        void GerenciaUnidadesDeCompartilhamento(ArquivoCompartilhado arquivoCompartilhado)
        {
            IEnumerable<int> idsUnidadesSelecionadas = RetornaUnidadesSelecionadas();
            IEnumerable<int> idsAdicionar = idsUnidadesSelecionadas;

            if (this.CurrentIdUnidades != null)
            {
                IEnumerable<int> idsExcluir = this.CurrentIdUnidades.Except(idsUnidadesSelecionadas);

                foreach (int idExcluir in idsExcluir)
                {
                    ArquivoCompartilhadoTB25_EMPRESA arquivoCompartilhadoTB25_EMPRESA = (from iArqCompTb25 in ArquivoCompartilhadoTB25_EMPRESA.RetornaTodosRegistros()
                                                                                         where iArqCompTb25.ArquivoCompartilhado.ArquivoCompartilhadoId == arquivoCompartilhado.ArquivoCompartilhadoId
                                                                                         && iArqCompTb25.TB25_EMPRESA.CO_EMP == idExcluir
                                                                                         select iArqCompTb25).FirstOrDefault();                    
                    ArquivoCompartilhadoTB25_EMPRESA.Delete(arquivoCompartilhadoTB25_EMPRESA, true);
                }

                idsAdicionar = idsUnidadesSelecionadas.Except(this.CurrentIdUnidades);
            }

            ArquivoCompartilhadoTB25_EMPRESA arqCompTb25 = new ArquivoCompartilhadoTB25_EMPRESA();
            int quantidadeEmpresas = idsAdicionar.Count();
            for (int i = 0; i < quantidadeEmpresas; i++)
            {
                arqCompTb25 = new ArquivoCompartilhadoTB25_EMPRESA();

                TB25_EMPRESA tB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(idsAdicionar.ElementAt(i));
                GestorEntities.AttachToHandled(tB25_EMPRESA);

                arqCompTb25.TB25_EMPRESA = tB25_EMPRESA;
                arqCompTb25.ArquivoCompartilhado = arquivoCompartilhado;

//------------> Faz a adição da entidade no contexto sem ir ao banco
                GestorEntities.CurrentContext.AddToArquivoCompartilhadoTB25_EMPRESA(arqCompTb25);
            }

            GestorEntities.CurrentContext.SaveChanges();

            AuxiliPagina.RedirecionaParaPaginaMensagem(
                String.Format("Agendamento de Arquivo Compartilhado salvo com sucesso. O arquivo: {0} será publicado a partir de {1}", 
                                arquivoCompartilhado.NomeArquivo, 
                                arquivoCompartilhado.DataPublicacao.ToString("dd/MM/yyyy")),
                Request.Url.AbsolutePath.ToLower().Replace("cadastro", "busca") + "&moduloNome=" + Request.QueryString["moduloNome"], C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Sucess);
        }

        /// <summary>
        /// Retorna os Ids das Unidades selecionadas no CheckBoxList
        /// </summary>
        /// <returns>IEnumerable<int></returns>
        IEnumerable<int> RetornaUnidadesSelecionadas()
        {
            foreach (ListItem unidade in chkUnidades.Items)
                if (unidade.Selected)
                    yield return int.Parse(unidade.Value);
        }

        /// <summary>
        /// Método que preenche o CheckBoxList com as Unidades Escolares
        /// </summary>
        private void CarregaListaDeUnidades()
        {
            var lstUnidades = from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                           select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP };

            foreach (var unidade in lstUnidades)
                chkUnidades.Items.Add(new ListItem(unidade.NO_FANTAS_EMP, unidade.CO_EMP.ToString()));
        }

        /// <summary>
        /// IEnumerable<int> CarregaUnidadesAtuais(int arquivoCompartilhadoId)
        /// </summary>
        /// <param name="arquivoCompartilhadoId">Id do arquivo compartilhado</param>
        private void CarregaUnidadesAtuais(int arquivoCompartilhadoId)
        {            
            var unidades = from arqCompTb25 in ArquivoCompartilhadoTB25_EMPRESA.RetornaTodosRegistros()
                           where arqCompTb25.ArquivoCompartilhado.ArquivoCompartilhadoId == arquivoCompartilhadoId
                           select new { arqCompTb25.TB25_EMPRESA.CO_EMP };

            List<int> lstIntCoEmp = new List<int>();

            foreach (var unidade in unidades)
            {
                int coEmp = unidade.CO_EMP;

                chkUnidades.Items.FindByValue(coEmp.ToString()).Selected = true;
                if (chkUnidades.Items.FindByValue(coEmp.ToString()).Selected)
                    lstIntCoEmp.Add(coEmp);
                //yield return coEmp;
            }

            this.CurrentIdUnidades = lstIntCoEmp;
        }

        /// <summary>
        /// Faz o gerenciamento da exclusao de registros das tabelas informadas
        /// </summary>
        /// <param name="idArqComp">Id do arquivo compartilhado</param>
        private void GerenciaExclusao(int idArqComp)
        {
            ArquivoCompartilhado arquivoCompartilhado = ArquivoCompartilhado.RetornaPeloID(idArqComp);
            ExcluiArquivo(arquivoCompartilhado.URL);

            var unidades = ArquivoCompartilhadoTB25_EMPRESA.RetornaTodosRegistros().Where(r => r.ArquivoCompartilhado.ArquivoCompartilhadoId.Equals(idArqComp));

            foreach (ArquivoCompartilhadoTB25_EMPRESA arquivoCompartilhadoTB25_EMPRESA in unidades)
                ArquivoCompartilhadoTB25_EMPRESA.Delete(arquivoCompartilhadoTB25_EMPRESA, false);
            
//--------> Primeiro faz a exclusão dos relacionamentos
            GestorEntities.CurrentContext.SaveChanges();
        }

        #region Gerenciamento de Arquivo

        /// <summary>
        /// Faz a gerência do salvamento do arquivo e retorna o caminho do arquivo
        /// </summary>
        /// <param name="strCaminhoArqCorrente">Caminho do arquivo corrente</param>
        /// <returns>String com o caminho do arquivo</returns>
        private string GerenciaArquivo(string strCaminhoArqCorrente)
        {
//--------> Sempre que um arquivo for enviado
            if (fuArquivoPublicar.FileBytes.Length > 0)
            {
//------------> Exclui o arquivo antigo antes de salvar um novo
                if (!string.IsNullOrEmpty(strCaminhoArqCorrente) && File.Exists(HttpContext.Current.Server.MapPath(strCaminhoArqCorrente)))
                    ExcluiArquivo(strCaminhoArqCorrente);

//------------> Cria um novo nome único para o arquivo
                strCaminhoArqCorrente = String.Format("{0}(ID {1}) - {2}", nomeArquivo, Guid.NewGuid(), fuArquivoPublicar.FileName);

                SalvaArquivo(nomeArquivo, strCaminhoArqCorrente);
            }

            return strCaminhoArqCorrente;
        }

        /// <summary>
        /// Faz a exclusão do arquivo informado
        /// </summary>
        /// <param name="strCaminhoArqCorrente">Caminho do arquivo corrente</param>
        private static void ExcluiArquivo(string strCaminhoArqCorrente)
        {
            File.Delete(HttpContext.Current.Server.MapPath(strCaminhoArqCorrente));
        }

        /// <summary>
        /// Salva o arquivo informado no caminho passado como parâmetro
        /// </summary>
        /// <param name="strNomeArquivo">Nome do arquivo</param>
        /// <param name="strCaminhoArquivo">Caminho do arquivo</param>
        private void SalvaArquivo(string strNomeArquivo, string strCaminhoArquivo)
        {
//--------> Cria o diretório ArquivosPublicados caso não exista
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(strNomeArquivo)))
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(strNomeArquivo));

//--------> Salva o Arquivo na pasta ArquivosPublicados no diretório root da aplicação.
            File.WriteAllBytes(HttpContext.Current.Server.MapPath(strCaminhoArquivo), fuArquivoPublicar.FileBytes);
        }
        #endregion

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade ArquivoCompartilhado</returns>
        private ArquivoCompartilhado RetornaEntidade()
        {
            int idArqComp = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);
            ArquivoCompartilhado arqComp = ArquivoCompartilhado.RetornaTodosRegistros().Where( arqCom => arqCom.ArquivoCompartilhadoId.Equals(idArqComp) ).FirstOrDefault();

//--------> Se nenhuma entidade foi encontrada no banco, retorna uma nova entidade
            return arqComp ?? new ArquivoCompartilhado();
        }

        #endregion        
    }
}
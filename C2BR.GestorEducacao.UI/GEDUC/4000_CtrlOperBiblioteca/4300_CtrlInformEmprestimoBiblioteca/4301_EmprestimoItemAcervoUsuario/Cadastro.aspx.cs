//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL DE BIBLIOTECA
// SUBMÓDULO: REGISTRO E BAIXA DE EMPRÉSTIMOS
// OBJETIVO: EMPRÉSTIMO DE ITENS DE ACERVO BIBLIOGRÁFICO A USUÁRIOS
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Resources;
using System.IO;
using System.ServiceModel;
using System.Configuration;
using System.Reflection;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4300_CtrlInformEmprestimoBiblioteca.F4301_EmprestimoItemAcervoUsuario
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Variáveis

        private static List<DadosObra> lstDadosObra;

        #endregion

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            
//--------> Valida se o evento é de exibição do relatório gerado.
            if (Session["ApresentaRelatorio"] != null)
                if (int.Parse(Session["ApresentaRelatorio"].ToString()) == 1)
                {
                    AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
//----------------> Limpa a var de sessão com o url do relatório.
                    Session.Remove("URLRelatorio");
                    Session.Remove("ApresentaRelatorio");
//----------------> Limpa a ref da url utilizada para carregar o relatório.
                    PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                    isreadonly.SetValue(this.Request.QueryString, false, null);
                    isreadonly.SetValue(this.Request.QueryString, true, null);
                }

            txtData.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lstDadosObra = new List<DadosObra>();
            CarregaAreasConhecimento();
            CarregaAutores();
            CarregaEditoras();
            CarregaUsuarios();
            grvPesquisa.DataSource = null;
            grvPesquisa.DataBind();
            BindGridEmprestimo(null);
        }

//====> Processo de Empréstimo de Biblioteca
        protected void btnEmpreBibli_Click(object sender, EventArgs e)
        {
            int coUsuarioBibliot = ddlNome.SelectedValue != "" ? int.Parse(ddlNome.SelectedValue) : 0;

            if (DateTime.Parse(txtData.Text).Date > DateTime.Now.Date)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data não pode ser futura");
                return;
            }

//--------> Faz a verificação na unidade pela quantidade máxima de livros e o tempo de empréstimo
            TB83_PARAMETRO tb83 = TB83_PARAMETRO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            decimal? dcmValorMulta = tb83.VL_MULTA_DIA_ATRASO_BIBLI;
            DateTime dataEntrega = DateTime.Now;
            int intQtdeLivros = 999;

            if (tb83 != null)
            {
                dataEntrega = dataEntrega.AddDays(tb83.QT_DIAS_RESER_BIBLI != null ? tb83.QT_DIAS_RESER_BIBLI.Value : 0);
                intQtdeLivros = tb83.QT_ITENS_ALUNO_BIBLI != null ? tb83.QT_ITENS_ALUNO_BIBLI.Value : intQtdeLivros;
            }

            int intQtdeEmprUsu = CalculaEmprestimosAnteriores();

            if (grvEmprestimo.Rows.Count + intQtdeEmprUsu > intQtdeLivros)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Somente " + intQtdeLivros + "podem ser emprestados simultaneamente." +
                    (intQtdeEmprUsu > 0 ? "Pessoa já tem " + intQtdeEmprUsu + " emprestados." : ""));
                return;
            }

            TB36_EMPR_BIBLIOT tb36 = new TB36_EMPR_BIBLIOT();
            tb36.DE_OBS_EMP_BIBLIOT = txtObservacao.Text;
            tb36.DT_EMPR_BIBLIOT = DateTime.Now;
            tb36.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);
            tb36.TB205_USUARIO_BIBLIOT = TB205_USUARIO_BIBLIOT.RetornaPelaChavePrimaria(coUsuarioBibliot);
            tb36.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            tb36.VL_MULT_DIA_ATRASO = dcmValorMulta;

            if (GestorEntities.SaveOrUpdate(tb36) <= 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar empréstimo");
                return;
            }

            BindGridEmprestimo(null);

            foreach (DadosObra dadosObra in lstDadosObra)
            {
                TB204_ACERVO_ITENS tb204 = (from lTb204 in TB204_ACERVO_ITENS.RetornaTodosRegistros()
                                            where lTb204.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO && lTb204.CO_ISBN_ACER == dadosObra.CodigoIsbn
                                            && lTb204.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && lTb204.CO_SITU_ACERVO_ITENS == "D"
                                            && lTb204.CO_CTRL_INTERNO == dadosObra.CodigoInterno
                                            select lTb204).FirstOrDefault();

                TB123_EMPR_BIB_ITENS tb123 = new TB123_EMPR_BIB_ITENS();

                tb123.DE_OBS_EMP = dadosObra.Observacao;
                tb123.DT_PREV_DEVO_ACER = dadosObra.DataEntrega;
                tb123.TB204_ACERVO_ITENS = tb204;
                tb123.TB36_EMPR_BIBLIOT = tb36;
                tb123.CO_ISEN_MULT_ACER = "N";

                if (GestorEntities.SaveOrUpdate(tb123) <= 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar item do empréstimo");
                    return;
                }

                tb204.CO_SITU_ACERVO_ITENS = "E";

                if (GestorEntities.SaveOrUpdate(tb204) <= 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Erro ao atualizar item do acervo");
                    return;
                }
            }

//--------> GERAÇÃO DO RELATÓRIO

//--------> Variáveis obrigatórias para gerar o Relatório
            string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_CO_NUM_EMP, strP_DESC_USU;

            var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
            IRelatorioWeb lIRelatorioWeb;

            strIDSessao = Session.SessionID.ToString();
            strIdentFunc = WRAuxiliares.IdentFunc;
            strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelComprEmprBibli");

//--------> Criação da Pasta
            if (!Directory.Exists(@strCaminhoRelatorioGerado))
                Directory.CreateDirectory(@strCaminhoRelatorioGerado);

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_DESC_USU = lblInformacaoUsuario.Text;
            strP_CO_EMP = LoginAuxili.CO_EMP.ToString();
            strP_CO_NUM_EMP = tb36.CO_NUM_EMP.ToString();

            lIRelatorioWeb = varRelatorioWeb.CreateChannel();

            lRetorno = lIRelatorioWeb.RelComprEmprBibli(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_NUM_EMP, strP_DESC_USU, LoginAuxili.ORG_NUMERO_CNPJ.ToString());

            string strURL = WRAuxiliares.RetornaCaminhoRelatorioPDF("/TMP_Relatorios/" + strIDSessao + "/") + strNomeRelatorio;
            Session["URLRelatorio"] = strURL;
            Session["ApresentaRelatorio"] = "1";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

            varRelatorioWeb.Close();

            AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
        }
    
        #endregion

        #region Métodos

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB36_EMPR_BIBLIOT</returns>
        private TB36_EMPR_BIBLIOT RetornaEntidade()
        {
            return TB36_EMPR_BIBLIOT.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }

        /// <summary>
        /// Método que preenche informações do PopUp
        /// </summary>
        /// <param name="tb204">Entidade TB204_ACERVO_ITENS</param>
        private void CarregaPopup(TB204_ACERVO_ITENS tb204)
        {
            hdfVisible.Value = "S";

            tb204.TB35_ACERVOReference.Load();
            tb204.TB35_ACERVO.TB31_AREA_CONHECReference.Load();
            tb204.TB35_ACERVO.TB34_AUTORReference.Load();
            tb204.TB35_ACERVO.TB32_CLASSIF_ACERReference.Load();
            tb204.TB35_ACERVO.TB33_EDITORAReference.Load();

            lblAreaConhecimento.Text = tb204.TB35_ACERVO.TB31_AREA_CONHEC.NO_AREACON;
            lblAutor.Text = tb204.TB35_ACERVO.TB34_AUTOR.NO_AUTOR;
            lblClassificacao.Text = tb204.TB35_ACERVO.TB32_CLASSIF_ACER.NO_CLAS_ACER;
            lblCoordenada1.Text = tb204.DE_LOCAL_END1 != null ? tb204.DE_LOCAL_END1 : "-";
            lblCoordenada2.Text = tb204.DE_LOCAL_END2 != null ? tb204.DE_LOCAL_END2 : "-";
            lblCoordenada3.Text = tb204.DE_LOCAL_END3 != null ? tb204.DE_LOCAL_END3 : "-";
            lblEditora.Text = tb204.TB35_ACERVO.TB33_EDITORA.NO_EDITORA;
            lblIsbn.Text = tb204.TB35_ACERVO.CO_ISBN_ACER.ToString("000-00-0000-000-0");
            lblLocal.Text = tb204.DE_LOCAL != null ? tb204.DE_LOCAL : "-";
            lblNomeObra.Text = tb204.TB35_ACERVO.NO_ACERVO;
            lblPaginas.Text = tb204.TB35_ACERVO.NU_PAG_LIVRO != null ? tb204.TB35_ACERVO.NU_PAG_LIVRO.Value.ToString() : "-";
            lblSinopse.Text = tb204.TB35_ACERVO.DES_SINOPSE;
        }

        /// <summary>
        /// Método que Retorna Informações do Usuário
        /// </summary>
        /// <returns>String com informação do usuário</returns>
        private string RetornaInforUsuario() 
        {
            int coUsuarioBibliot = ddlNome.SelectedValue != "" ? int.Parse(ddlNome.SelectedValue) : 0;

            if (coUsuarioBibliot == 0) 
                return "";

            string strTexto, strDescricaoInforUsu = "";

            TB205_USUARIO_BIBLIOT tb205 = TB205_USUARIO_BIBLIOT.RetornaPelaChavePrimaria(coUsuarioBibliot);

            if (tb205 == null)
                return "";

            switch (tb205.TP_USU_BIB)
            {
                case "A":
                    tb205.TB07_ALUNOReference.Load();
                    var tb08 = (from lTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                join tb01 in TB01_CURSO.RetornaTodosRegistros() on lTb08.CO_CUR equals tb01.CO_CUR
                                join tb06 in TB06_TURMAS.RetornaTodosRegistros() on lTb08.CO_TUR equals tb06.CO_TUR
                                where lTb08.CO_ALU == tb205.TB07_ALUNO.CO_ALU
                                select new
                                {
                                    lTb08.TB44_MODULO.DE_MODU_CUR, tb01.NO_CUR, lTb08.DT_EFE_MAT, tb06.TB129_CADTURMAS.CO_SIGLA_TURMA, lTb08.CO_ALU_CAD,
                                    CO_PERI_TUR = tb06.CO_PERI_TUR == "M" ? "Manhã" : tb06.CO_PERI_TUR == "V" ? "Vespertino" : "Noturno"
                                }).OrderByDescending(m => m.DT_EFE_MAT).FirstOrDefault();

                    if (tb08 != null)
                    {
                        strDescricaoInforUsu = string.Format("(Modalidade: {0} - Série: {1} - Turma: {2} - Turno: {3} - Matrícula: {4})",
                                    tb08.DE_MODU_CUR, tb08.NO_CUR, tb08.CO_SIGLA_TURMA, tb08.CO_PERI_TUR, tb08.CO_ALU_CAD);
                    }
                    break;
                case "P":
                    tb205.TB03_COLABORReference.Load();
                    tb205.TB03_COLABOR.TB25_EMPRESAReference.Load();

                    strTexto = "Professor - Escola: {0}, Matrícula: {1}";
                    string strNoFantasEmpProfessor = tb205.TB03_COLABOR.TB25_EMPRESA.NO_FANTAS_EMP;
                    string strMatriculaProfessor = tb205.TB03_COLABOR.CO_MAT_COL;
                    strDescricaoInforUsu = string.Format(strTexto, strNoFantasEmpProfessor, strMatriculaProfessor);
                    break;
                case "F":
                    tb205.TB03_COLABORReference.Load();
                    tb205.TB03_COLABOR.TB25_EMPRESAReference.Load();

                    strTexto = "Função: {0}, Unidade: {1}, Matrícula: {2}";
                    string strFuncaoFuncionario = TB15_FUNCAO.RetornaPelaChavePrimaria(tb205.TB03_COLABOR.CO_FUN).NO_FUN;
                    string strNoFantasEmpFuncionario = tb205.TB03_COLABOR.TB25_EMPRESA.NO_FANTAS_EMP;
                    string strMatriculaFuncionario = tb205.TB03_COLABOR.CO_MAT_COL;
                    strDescricaoInforUsu = string.Format(strTexto, strFuncaoFuncionario, strNoFantasEmpFuncionario, strMatriculaFuncionario);
                    break;
                case "O":
                    strTexto = "Usuário externo - CPF: {0} - RG: {1}";
                    string strCpfUsuario = tb205.NU_CPF_USU_BIB.Length == 11 ? tb205.NU_CPF_USU_BIB.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb205.NU_CPF_USU_BIB;
                    strDescricaoInforUsu = string.Format(strTexto, strCpfUsuario, tb205.CO_RG_USU_BIB);
                    break;
                default:
                    strDescricaoInforUsu = "";
                    break;
            }

            return strDescricaoInforUsu;
        }

        /// <summary>
        /// Método que calcula Empréstimos Anteriores
        /// </summary>
        /// <returns>Quantidade de empréstimos</returns>
        private int CalculaEmprestimosAnteriores()
        {
            int coUsuarioBibliot = ddlNome.SelectedValue != "" ? int.Parse(ddlNome.SelectedValue) : 0;

            int intQtdeEmpre = (from tb123 in TB123_EMPR_BIB_ITENS.RetornaTodosRegistros()
                                where tb123.TB36_EMPR_BIBLIOT.TB205_USUARIO_BIBLIOT.CO_USUARIO_BIBLIOT == coUsuarioBibliot && tb123.DT_REAL_DEVO_ACER == null
                                select new { tb123.CO_EMPR_BIB_ITENS }).Count();

            return intQtdeEmpre;
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega a grid de Empréstimos
        /// </summary>
        /// <param name="indexLinhaExcluir">Índica da linha a ser excluída</param>
        private void BindGridEmprestimo(int? indexLinhaExcluir) 
        {
//--------> Varre toda a grid de empréstimos: atualizando a lista de dados
            foreach (GridViewRow row in grvEmprestimo.Rows) 
            {
                DadosObra obra = lstDadosObra[row.RowIndex];
                obra.DataEntrega = DateTime.Parse(((TextBox)row.Cells[2].FindControl("txtDataEntrega")).Text);
                obra.Observacao = ((TextBox)row.Cells[3].FindControl("txtObservacao")).Text;

                lstDadosObra[row.RowIndex] = obra;
            }

            if (indexLinhaExcluir.HasValue) 
            {
                decimal dcmCodigoIsbn = lstDadosObra[indexLinhaExcluir.Value].CodigoIsbn;
                lstDadosObra.RemoveAll( d => d.CodigoIsbn == dcmCodigoIsbn);
            }

            grvEmprestimo.DataKeyNames = new string[] { "CodigoIsbn" };
            grvEmprestimo.DataSource = lstDadosObra;
            grvEmprestimo.DataBind();
        }

        /// <summary>
        /// Método que carrega a grid de Pesquisa
        /// </summary>
        private void CarregaGridPesquisa() 
        {
            int coAreaCon = ddlAreaConhecimento.SelectedValue != "" ? int.Parse(ddlAreaConhecimento.SelectedValue) : 0;
            int coEditora = ddlAreaConhecimento.SelectedValue != "" ? int.Parse(ddlAreaConhecimento.SelectedValue) : 0;
            int coAutor = ddlAutor.SelectedValue != "" ? int.Parse(ddlAutor.SelectedValue) : 0;
            string strNoAcervo = txtNomeObra.Text.Trim();
            decimal coIsbnAcer = 0;
            decimal.TryParse(txtIsbn.Text.Trim().Replace("-", ""), out coIsbnAcer);

            var resultado = (from tb204 in TB204_ACERVO_ITENS.RetornaTodosRegistros()
                             where (coAreaCon != 0 ? tb204.TB35_ACERVO.TB31_AREA_CONHEC.CO_AREACON == coAreaCon : coAreaCon == 0)
                             && (coAutor != 0 ? tb204.TB35_ACERVO.TB34_AUTOR.CO_AUTOR == coAutor : coAutor == 0)
                             && (coEditora != 0 ? tb204.TB35_ACERVO.TB33_EDITORA.CO_EDITORA == coEditora : coEditora == 0)
                             && (strNoAcervo != "" ? tb204.TB35_ACERVO.NO_ACERVO.Contains(strNoAcervo) : strNoAcervo == "")
                             && (coIsbnAcer != 0 ? tb204.TB35_ACERVO.CO_ISBN_ACER == coIsbnAcer : coIsbnAcer == 0) && tb204.CO_SITU_ACERVO_ITENS == "D"
                             && tb204.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             && tb204.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                             select new
                             {
                                 tb204.TB35_ACERVO.CO_ISBN_ACER, tb204.TB35_ACERVO.NO_ACERVO, tb204.TB35_ACERVO.TB33_EDITORA.NO_EDITORA
                             }).Distinct().OrderBy( a => a.NO_ACERVO );

            grvPesquisa.DataKeyNames = new string[] { "CO_ISBN_ACER" };
            grvPesquisa.DataSource = resultado;
            grvPesquisa.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Áreas de Conhecimento
        /// </summary>
        private void CarregaAreasConhecimento()
        {
            ddlAreaConhecimento.DataSource = (from tb31 in TB31_AREA_CONHEC.RetornaTodosRegistros()
                                              where tb31.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                              select new { tb31.CO_AREACON, tb31.NO_AREACON });

            ddlAreaConhecimento.DataTextField = "NO_AREACON";
            ddlAreaConhecimento.DataValueField = "CO_AREACON";
            ddlAreaConhecimento.DataBind();

            ddlAreaConhecimento.Items.Insert(0, new ListItem("Todas", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Autores
        /// </summary>
        private void CarregaAutores()
        {
            ddlAutor.DataSource = (from tb34 in TB34_AUTOR.RetornaTodosRegistros()
                                   where tb34.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                   select new { tb34.CO_AUTOR, tb34.NO_AUTOR });

            ddlAutor.DataTextField = "NO_AUTOR";
            ddlAutor.DataValueField = "CO_AUTOR";
            ddlAutor.DataBind();

            ddlAutor.Items.Insert(0, new ListItem("Todos", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Editoras
        /// </summary>
        private void CarregaEditoras()
        {
            ddlEditora.DataSource = (from tb33 in TB33_EDITORA.RetornaTodosRegistros()
                                     where tb33.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                     select new { tb33.CO_EDITORA, tb33.NO_EDITORA });

            ddlEditora.DataTextField = "NO_EDITORA";
            ddlEditora.DataValueField = "CO_EDITORA";
            ddlEditora.DataBind();

            ddlEditora.Items.Insert(0, new ListItem("Todas", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Usuários
        /// </summary>
        private void CarregaUsuarios()
        {
            ddlNome.DataSource = (from tb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                  where (ddlTipo.SelectedValue != "" ? tb205.TP_USU_BIB == ddlTipo.SelectedValue : ddlTipo.SelectedValue == "")
                                  && tb205.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                  select new { tb205.CO_USUARIO_BIBLIOT, tb205.NO_USU_BIB });

            ddlNome.DataTextField = "NO_USU_BIB";
            ddlNome.DataValueField = "CO_USUARIO_BIBLIOT";
            ddlNome.DataBind();

            ddlNome.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        protected void btnPesquisar_Click(object sender, ImageClickEventArgs e)
        {
            CarregaGridPesquisa();
        }

        protected void btnAdicionar_Click(object sender, ImageClickEventArgs e)
        {
//--------> Faz a verificação na unidade pela quantidade máxima de livros e o tempo de empréstimo
            TB83_PARAMETRO tb83 = TB83_PARAMETRO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            DateTime dataEntrega = DateTime.Now;
            int intQtdeLivrosBibli = 99;

            if (tb83 != null)
            {
                dataEntrega = dataEntrega.AddDays(tb83.QT_DIAS_RESER_BIBLI != null ? tb83.QT_DIAS_RESER_BIBLI.Value : 0);
                intQtdeLivrosBibli = tb83.QT_ITENS_ALUNO_BIBLI != null ? tb83.QT_ITENS_ALUNO_BIBLI.Value : intQtdeLivrosBibli;
            }

//--------> Varre toda a grid de Pesquisa
            foreach (GridViewRow linha in grvPesquisa.Rows)
            {
//------------> Verifica se linha está checada
                if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                {
//----------------> Recebe os valores para adicionar na grid de empréstimo
                    decimal coIsbnAcer = decimal.Parse(linha.Cells[1].Text.Trim().Replace(".", "").Replace("-", ""));
                    string strNoAcervo = TB35_ACERVO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO, coIsbnAcer).NO_ACERVO;
                    string strCodInterno = ((DropDownList)linha.Cells[3].FindControl("ddlCodigo")).SelectedValue;

//----------------> Faz a remoção do livro se possuir o mesmo ISBN
                    lstDadosObra.RemoveAll(o => o.CodigoIsbn == coIsbnAcer);

//----------------> Faz a adicão apenas se o item não existir
                    lstDadosObra.Add(new DadosObra { CodigoIsbn = coIsbnAcer, NomeObra = strNoAcervo, DataEntrega = dataEntrega.Date, CodigoInterno = strCodInterno });
                }
            }

            if (lstDadosObra.Count > intQtdeLivrosBibli)
                AuxiliPagina.EnvioMensagemErro(this, "Somente " + intQtdeLivrosBibli + " podem ser emprestados simultaneamente.");

            BindGridEmprestimo(null);
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUsuarios();
            lblInformacaoUsuario.Text = RetornaInforUsuario();
        }

        protected void grvEmprestimo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            BindGridEmprestimo(e.RowIndex);
        }

        protected void grvPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal coIsbnAcer = decimal.Parse(e.Row.Cells[1].Text);

                ((DropDownList)e.Row.Cells[3].FindControl("ddlCodigo")).DataSource = from tb204 in TB204_ACERVO_ITENS.RetornaTodosRegistros()
                                                                                     where tb204.TB35_ACERVO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO 
                                                                                     && tb204.CO_ISBN_ACER == coIsbnAcer && tb204.CO_SITU_ACERVO_ITENS == "D"
                                                                                     && tb204.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                                                                     select new { tb204.CO_CTRL_INTERNO };

                ((DropDownList)e.Row.Cells[3].FindControl("ddlCodigo")).DataTextField = "CO_CTRL_INTERNO";
                ((DropDownList)e.Row.Cells[3].FindControl("ddlCodigo")).DataTextField = "CO_CTRL_INTERNO";
                ((DropDownList)e.Row.Cells[3].FindControl("ddlCodigo")).DataBind();

                e.Row.Cells[1].Text = coIsbnAcer.ToString("000-00-0000-000-0");
            }
        }

        protected void ddlNome_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblInformacaoUsuario.Text = RetornaInforUsuario();
        }

        protected void grvPesquisa_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
//--------> Retorna o item selecionado
            decimal coIsbnAcer = decimal.Parse(grvPesquisa.Rows[e.RowIndex].Cells[1].Text.Replace("-", ""));
            string coCtrlInterno = ((DropDownList)grvPesquisa.Rows[e.RowIndex].Cells[3].FindControl("ddlCodigo")).SelectedValue;

            TB204_ACERVO_ITENS tb204 = (from lTb204 in TB204_ACERVO_ITENS.RetornaTodosRegistros()
                                        where lTb204.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                        && lTb204.CO_ISBN_ACER == coIsbnAcer && lTb204.CO_CTRL_INTERNO == coCtrlInterno
                                        select lTb204).FirstOrDefault();

            CarregaPopup(tb204);
        }

        #region Tipo Dados Obra

//----> Representa os Dados de uma Obra
        private class DadosObra
        {
            public string CodigoInterno { get; set; }
            public decimal CodigoIsbn { get; set; }
            public string NomeObra { get; set; }
            public DateTime DataEntrega { get; set; }
            public string DataEntregaStr { get { return DataEntrega.ToString("dd/MM/yyyy"); } }
            public string Observacao { get; set; }
        }
        #endregion
    }
}
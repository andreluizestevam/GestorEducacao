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
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4200_CtrlInformReservaBilblioteca.F4201_ReservaItemAcervo
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
            CurrentPadraoCadastros.BarraCadastro.OnDelete += new Library.Componentes.BarraCadastro.OnDeleteHandler(BarraCadastro_OnDelete);
        }

        void BarraCadastro_OnDelete(System.Data.Objects.DataClasses.EntityObject entity)
        {

        }

        protected void Page_Load()
        {
            if (IsPostBack) return;

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                DateTime dataAtual = DateTime.Now;
                txtDataReserva.Text = txtDataSituacao.Text = dataAtual.ToString("dd/MM/yyyy");
                txtHoraReserva.Text = dataAtual.ToShortTimeString();
            }

            CarregaUnidade();
            CarregaUsuario();
            CarregaAreasConhecimento();
            CarregaClassificacoes();
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (CurrentPadraoCadastros.BarraCadastro.AcaoSolicitadaClique == CurrentPadraoCadastros.BarraCadastro.botaoSave)
            {
                #region Cadastro edição
                if (Page.IsValid)
                {
                    List<TB204_ACERVO_ITENS> lstTb204 = new List<TB204_ACERVO_ITENS>();

                    int intQtdeItensSelec = 0;

                    //------------> Varre toda a grid de Busca
                    foreach (GridViewRow linha in grdBusca.Rows)
                    {
                        //----------------> Verifica se item está selecionado, se sim, adiciona na lista de itensSelecionados do tipo "TB204_ACERVO_ITENS"
                        if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                        {
                            intQtdeItensSelec++;

                            decimal coIsbnAcer;
                            int coEmp, coAcervoAquisi, orgCodigoOrgao, coAcervoItens;

                            coIsbnAcer = Convert.ToDecimal(grdBusca.DataKeys[linha.RowIndex].Values[0]);
                            coEmp = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[1]);
                            coAcervoAquisi = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[2]);
                            orgCodigoOrgao = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[3]);
                            coAcervoItens = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[4]);

                            TB204_ACERVO_ITENS tb204 = TB204_ACERVO_ITENS.RetornaPelaChavePrimaria(orgCodigoOrgao, coIsbnAcer, coAcervoAquisi, coAcervoItens, coEmp);

                            if (tb204 != null)
                                lstTb204.Add(tb204);
                        }
                    }

                    int itensAdicionados = 0;

                    //------------> Faz a verificação para saber se algum item foi selecionado
                    if (intQtdeItensSelec == 0)
                    {
                        ServerValidateEventArgs eArgs = new ServerValidateEventArgs("", false);
                        AuxiliPagina.EnvioMensagemErro(this, "É necessário selecionar pelo menos um item.");
                        return;
                    }
                    else
                    {
                        int coUsuarioBibliot = ddlNome.SelectedValue != "" ? int.Parse(ddlNome.SelectedValue) : 0;

                        TB206_RESERVA_BIBLIOTECA tb206 = new TB206_RESERVA_BIBLIOTECA();
                        tb206.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);
                        tb206.TB205_USUARIO_BIBLIOT = TB205_USUARIO_BIBLIOT.RetornaPelaChavePrimaria(coUsuarioBibliot);
                        tb206.TP_USU_BIB = tb206.TB205_USUARIO_BIBLIOT.TP_USU_BIB;
                        tb206.FLA_AVISAR_EMAIL_RESERVA = chkFlagAvisarEmail.Checked ? "S" : "N";
                        tb206.FLA_AVISAR_SMS_RESERVA = chkFlagAvisarSms.Checked ? "S" : "N";
                        tb206.DT_RESERVA = DateTime.Now;
                        DateTime dtLimiteNecessidade;
                        DateTime.TryParse(txtDataLimiteNecessidade.Text + " " + txtHoraLimiteNecessidade.Text, out dtLimiteNecessidade);
                        if (dtLimiteNecessidade != null && dtLimiteNecessidade < DateTime.Now)
                        {
                            ServerValidateEventArgs eArgs = new ServerValidateEventArgs("", false);
                            AuxiliPagina.EnvioMensagemErro(this, "Data de limite de necessidade deve ser maior que a data atual.");
                            return;
                        }
                        tb206.DT_LIMITE_NECESSI_RESERVA = dtLimiteNecessidade;
                        tb206.CO_SITU_RESERVA = "A";
                        tb206.DT_SITU_RESERVA = DateTime.Parse(txtDataSituacao.Text);

                        //----------------> Faz a verificação para saber se a Reserva de Biblioteca foi salva com sucesso
                        if (TB206_RESERVA_BIBLIOTECA.SaveOrUpdate(tb206, true) > 0)
                        {
                            foreach (TB204_ACERVO_ITENS iTb204 in lstTb204)
                            {
                                iTb204.TB25_EMPRESAReference.Load();

                                TB207_RESERVA_ITENS_BIBLIOTECA tb207 = new TB207_RESERVA_ITENS_BIBLIOTECA();
                                tb207.TB206_RESERVA_BIBLIOTECA = tb206;
                                tb207.TB204_ACERVO_ITENS = iTb204;
                                tb207.CO_RESERVA_BIBLIOTECA = tb206.CO_RESERVA_BIBLIOTECA;
                                tb207.ORG_CODIGO_ORGAO = iTb204.ORG_CODIGO_ORGAO;
                                tb207.CO_ISBN_ACER = iTb204.CO_ISBN_ACER;
                                tb207.CO_ACERVO_AQUISI = iTb204.CO_ACERVO_AQUISI;
                                tb207.CO_ACERVO_ITENS = iTb204.CO_ACERVO_ITENS;
                                tb207.CO_SITU_ITEM_RESERVA = ddlSituacaoReserva.SelectedValue;
                                tb207.CO_EMP = iTb204.TB25_EMPRESA.CO_EMP;

                                if (TB207_RESERVA_ITENS_BIBLIOTECA.SaveOrUpdate(tb207, true) > 0)
                                    itensAdicionados++;
                            }
                        }
                    }

                    if (itensAdicionados > 0)
                        AuxiliPagina.RedirecionaParaPaginaSucesso("Reserva efetuada com sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca().Replace("Busca", "Cadastro"));

                }
                #endregion
            }
            else if(CurrentPadraoCadastros.BarraCadastro.AcaoSolicitadaClique == CurrentPadraoCadastros.BarraCadastro.botaoDelete)
            {
                TB206_RESERVA_BIBLIOTECA tb206 = RetornaEntidade();
                if (tb206 != null)
                {
                    var itens = (from it in TB207_RESERVA_ITENS_BIBLIOTECA.RetornaTodosRegistros()
                                 where it.CO_RESERVA_BIBLIOTECA == tb206.CO_RESERVA_BIBLIOTECA    
                                 select it
                                     );
                    if (itens != null && itens.Count() > 0)
                    {
                        foreach(TB207_RESERVA_ITENS_BIBLIOTECA linha in itens)
                        {
                            GestorEntities.Delete(linha);
                        }
                    }
                    if (GestorEntities.Delete(tb206) <= 0)
                        AuxiliPagina.EnvioMensagemErro(this, "Erro ao excluir registro.");
                    else
                    {
                        AuxiliPagina.RedirecionaParaPaginaSucesso("Registro excluído com sucesso.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                    }
                }
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB206_RESERVA_BIBLIOTECA tb206 = RetornaEntidade();

            if (tb206 != null)
            {
                tb206.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);
                tb206.TB205_USUARIO_BIBLIOTReference.Load();

                switch (tb206.TB205_USUARIO_BIBLIOT.TP_USU_BIB)
                {
                    case "A":
                        tb206.TB205_USUARIO_BIBLIOT.TB07_ALUNOReference.Load();
                        ddlUnidade.SelectedValue = tb206.TB205_USUARIO_BIBLIOT.TB07_ALUNO.CO_ALU.ToString();
                        break;
                    case "F":
                        tb206.TB205_USUARIO_BIBLIOT.TB03_COLABORReference.Load();
                        ddlUnidade.SelectedValue = tb206.TB205_USUARIO_BIBLIOT.TB03_COLABOR.CO_COL.ToString();
                        break;
                    case "P":
                        tb206.TB205_USUARIO_BIBLIOT.TB03_COLABORReference.Load();
                        ddlUnidade.SelectedValue = tb206.TB205_USUARIO_BIBLIOT.TB03_COLABOR.CO_COL.ToString();
                        break;
                    case "O":
                        ddlUnidade.Items.Clear();
                        ddlUnidade.Items.Add(new ListItem("N/A", "N/A"));
                        break;
                    default:
                        ddlUnidade.Items.Clear();
                        ddlUnidade.Items.Add(new ListItem("N/A", "N/A"));
                        break;
                }

                ddlTipoUsuario.SelectedValue = tb206.TP_USU_BIB;
                ddlNome.SelectedValue = tb206.TB205_USUARIO_BIBLIOT.CO_USUARIO_BIBLIOT.ToString();

//------------> Faz o carregamento dos itens Reservados
                grdItensSelecionados.DataSource = (from tb207 in TB207_RESERVA_ITENS_BIBLIOTECA.RetornaTodosRegistros()
                                                   where tb207.TB206_RESERVA_BIBLIOTECA.CO_RESERVA_BIBLIOTECA == tb206.CO_RESERVA_BIBLIOTECA
                                                   select new
                                                   {
                                                       tb207.TB204_ACERVO_ITENS.CO_ISBN_ACER, 
                                                       tb207.TB204_ACERVO_ITENS.TB35_ACERVO.NO_ACERVO,
                                                       SIGLA = tb207.TB204_ACERVO_ITENS.TB25_EMPRESA.sigla

                                                   }).OrderBy( r => r.NO_ACERVO ).ThenBy( r => r.SIGLA );
                grdItensSelecionados.DataBind();

                txtDataReserva.Text = tb206.DT_RESERVA.ToString("dd/MM/yyyy");
                txtHoraReserva.Text = tb206.DT_RESERVA.ToString("HH:mm");
                txtDataLimiteNecessidade.Text = tb206.DT_LIMITE_NECESSI_RESERVA.ToString("dd/MM/yyyy");
                txtHoraLimiteNecessidade.Text = tb206.DT_LIMITE_NECESSI_RESERVA.ToString("HH:mm");
                txtDataSituacao.Text = tb206.DT_SITU_RESERVA.ToString("dd/MM/yyyy");
                ddlSituacaoReserva.SelectedValue = tb206.CO_SITU_RESERVA;
                chkFlagAvisarSms.Checked = tb206.FLA_AVISAR_SMS_RESERVA == "S";
                chkFlagAvisarEmail.Checked = tb206.FLA_AVISAR_EMAIL_RESERVA == "S";
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB206_RESERVA_BIBLIOTECA</returns>
        private TB206_RESERVA_BIBLIOTECA RetornaEntidade()
        {
            return TB206_RESERVA_BIBLIOTECA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega a grid de Busca
        /// </summary>
        private void CarregaGrid()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coClasAcer = ddlClassificacao.SelectedValue != "" ? int.Parse(ddlClassificacao.SelectedValue) : 0;
            int coAreaCon = ddlAreaConhecimento.SelectedValue != "" ? int.Parse(ddlAreaConhecimento.SelectedValue) : 0;

            var lstTb204 = TB204_ACERVO_ITENS.RetornaTodosRegistros().Where(a => a.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            var resultado = (from tb35 in TB35_ACERVO.RetornaTodosRegistros()
                             join tb204 in
                                    (
                                       from tb204 in lstTb204
                                       where (coAreaCon != 0 ? tb204.TB35_ACERVO.TB31_AREA_CONHEC.CO_AREACON == coAreaCon : coAreaCon == 0)
                                       && (coClasAcer != 0 ? tb204.TB35_ACERVO.TB32_CLASSIF_ACER.CO_CLAS_ACER == coClasAcer : coClasAcer == 0)
                                       && (coEmp != 0 ? tb204.TB25_EMPRESA.CO_EMP == coEmp : coEmp == 0)
                                       && (tb204.TB25_EMPRESA.TB83_PARAMETRO.FLA_RESER_OUTRA_UNID == "S")
                                       && tb204.TB25_EMPRESA.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                       orderby tb204.TB25_EMPRESA.NO_FANTAS_EMP
                                       group tb204 by new { tb204.CO_ISBN_ACER, tb204.TB25_EMPRESA.CO_EMP } into g
                                       select g.FirstOrDefault()
                                    )
                             on tb35.CO_ISBN_ACER equals tb204.CO_ISBN_ACER
                             where tb35.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             select new
                             {
                                 tb35.ORG_CODIGO_ORGAO,
                                 tb204.CO_ACERVO_AQUISI,
                                 tb204.CO_ACERVO_ITENS,
                                 tb204.TB25_EMPRESA.CO_EMP,
                                 tb35.CO_ISBN_ACER,
                                 tb35.NO_ACERVO,
                                 tb35.TB34_AUTOR.NO_AUTOR,
                                 tb204.TB25_EMPRESA.NO_FANTAS_EMP,
                                 // faz uma count dos itens Disponíveis
                                 COUNT = (from lTb204 in lstTb204
                                          where lTb204.TB25_EMPRESA.CO_EMP == tb204.TB25_EMPRESA.CO_EMP && lTb204.CO_ISBN_ACER == tb204.CO_ISBN_ACER
                                          && lTb204.CO_SITU_ACERVO_ITENS == "D"
                                          select new { lTb204.CO_ISBN_ACER }).Count()
                             });

            grdBusca.DataKeyNames = new string[] { "CO_ISBN_ACER", "CO_EMP", "CO_ACERVO_AQUISI", "ORG_CODIGO_ORGAO", "CO_ACERVO_ITENS" };

            if (resultado.Count() > 0)
            {
                grdBusca.DataSource = resultado;
                grdBusca.DataBind();
            }
            else
            {
                grdBusca.DataSource = null;
                grdBusca.DataBind();
            }
        }

        /// <summary>
        /// Método que carrega do DropDown de Unidades Escolares
        /// </summary>
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy( e => e.NO_FANTAS_EMP );

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("Todas", ""));

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega do DropDown de Usuários
        /// </summary>
        private void CarregaUsuario()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            string tipoUsuario = ddlTipoUsuario.SelectedValue;

            ddlNome.Items.Clear();

            switch (tipoUsuario)
            {
                case "A":
                    ddlNome.DataSource = (from tb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                          where (coEmp != 0 ? tb205.TB07_ALUNO.TB25_EMPRESA1.CO_EMP == coEmp : coEmp == 0)
                                          && tb205.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO && tb205.TP_USU_BIB == "A"
                                          select new { tb205.CO_USUARIO_BIBLIOT, tb205.NO_USU_BIB }).OrderBy( u => u.NO_USU_BIB );

                    ddlNome.DataValueField = "CO_USUARIO_BIBLIOT";
                    ddlNome.DataTextField = "NO_USU_BIB";
                    ddlNome.DataBind();
                    break;
                case "P":
                    ddlNome.DataSource = (from tb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                          where (coEmp != 0 ? tb205.TB03_COLABOR.TB25_EMPRESA1.CO_EMP == coEmp : coEmp == 0)
                                          && tb205.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO && tb205.TP_USU_BIB == "P"
                                          select new { tb205.CO_USUARIO_BIBLIOT, tb205.NO_USU_BIB }).OrderBy( u => u.NO_USU_BIB );

                    ddlNome.DataValueField = "CO_USUARIO_BIBLIOT";
                    ddlNome.DataTextField = "NO_USU_BIB";
                    ddlNome.DataBind();
                    break;
                case "F":
                    ddlNome.DataSource = (from tb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                          where (coEmp != 0 ? tb205.TB03_COLABOR.TB25_EMPRESA1.CO_EMP == coEmp : coEmp == 0)
                                          && tb205.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO && tb205.TP_USU_BIB == "F"
                                          select new { tb205.CO_USUARIO_BIBLIOT, tb205.NO_USU_BIB }).OrderBy( u => u.NO_USU_BIB );

                    ddlNome.DataValueField = "CO_USUARIO_BIBLIOT";
                    ddlNome.DataTextField = "NO_USU_BIB";
                    ddlNome.DataBind();
                    break;
                case "O":
                    ddlNome.DataSource = (from tb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                          where (coEmp != 0 ? tb205.TB03_COLABOR.TB25_EMPRESA1.CO_EMP == coEmp : coEmp == 0)
                                          && tb205.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO && tb205.TP_USU_BIB == "O"
                                          select new { tb205.CO_USUARIO_BIBLIOT, tb205.NO_USU_BIB }).OrderBy( u => u.NO_USU_BIB );

                    ddlNome.DataValueField = "CO_USUARIO_BIBLIOT";
                    ddlNome.DataTextField = "NO_USU_BIB";
                    ddlNome.DataBind();
                    break;
                default:
                    ddlNome.DataSource = (from tb205 in TB205_USUARIO_BIBLIOT.RetornaTodosRegistros()
                                          where (tb205.TB03_COLABOR.TB25_EMPRESA1.CO_EMP == coEmp || tb205.TB07_ALUNO.TB25_EMPRESA1.CO_EMP == coEmp || coEmp == 0)
                                          && tb205.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                          select new { tb205.CO_USUARIO_BIBLIOT, tb205.NO_USU_BIB }).OrderBy( u => u.NO_USU_BIB );

                    ddlNome.DataValueField = "CO_USUARIO_BIBLIOT";
                    ddlNome.DataTextField = "NO_USU_BIB";
                    ddlNome.DataBind();
                    break;
            }

            ddlNome.Items.Insert(0, new ListItem("Todos", ""));
        }

        /// <summary>
        /// Método que carrega do DropDown de Classificação de Acervos
        /// </summary>
        private void CarregaClassificacoes()
        {
            int coAreaCon = ddlAreaConhecimento.SelectedValue != "" ? int.Parse(ddlAreaConhecimento.SelectedValue) : 0;

            ddlClassificacao.Items.Clear();

            if (coAreaCon != 0)
            {
                ddlClassificacao.DataSource = (from tb32 in TB32_CLASSIF_ACER.RetornaPelaAreaConhecimento(coAreaCon)
                                               select new { tb32.CO_CLAS_ACER, tb32.NO_CLAS_ACER });

                ddlClassificacao.DataTextField = "NO_CLAS_ACER";
                ddlClassificacao.DataValueField = "CO_CLAS_ACER";
                ddlClassificacao.DataBind();
            }

            ddlClassificacao.Items.Insert(0, new ListItem("Todas", ""));
        }

        /// <summary>
        /// Método que carrega do DropDown de Áreas de Conhecimento
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
        #endregion

        #region Changed
        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUsuario();
        }

        protected void ddlTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUsuario();
        }

        protected void btnListarItens_Click(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        protected void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
            List<TB204_ACERVO_ITENS> lstTb204 = new List<TB204_ACERVO_ITENS>();

//--------> Varre toda a grid de Busca
            foreach (GridViewRow linha in grdBusca.Rows)
            {
//------------> Verifica se item está selecionado, se sim, adiciona na lista de itensSelecionados do tipo "TB204_ACERVO_ITENS"
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    decimal coIsbnAcer;
                    int coEmp, coAcervoAquisi, orgCodigoOrgao, coAcervoItens;

                    coIsbnAcer = Convert.ToDecimal(grdBusca.DataKeys[linha.RowIndex].Values[0]);
                    coEmp = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[1]);
                    coAcervoAquisi = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[2]);
                    orgCodigoOrgao = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[3]);
                    coAcervoItens = Convert.ToInt32(grdBusca.DataKeys[linha.RowIndex].Values[4]);

                    TB204_ACERVO_ITENS tb204 = TB204_ACERVO_ITENS.RetornaPelaChavePrimaria(orgCodigoOrgao, coIsbnAcer, coAcervoAquisi, coAcervoItens, coEmp);
                    tb204.TB25_EMPRESAReference.Load();
                    tb204.TB35_ACERVOReference.Load();

                    if (tb204 != null)
                        lstTb204.Add(tb204);
                }
            }

            grdItensSelecionados.DataSource = from lTb204 in lstTb204
                                              select new 
                                              {
                                                  lTb204.CO_ISBN_ACER, lTb204.TB35_ACERVO.NO_ACERVO, 
                                                  lTb204.TB25_EMPRESA.NO_FANTAS_EMP, SIGLA = lTb204.TB25_EMPRESA.sigla
                                              };
            grdItensSelecionados.DataBind();
        }

        protected void ddlNome_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coUsuarioBibliot = ddlNome.SelectedValue != "" ? int.Parse(ddlNome.SelectedValue) : 0;

            string strTexto = "";

            TB205_USUARIO_BIBLIOT tb205 = TB205_USUARIO_BIBLIOT.RetornaPelaChavePrimaria(coUsuarioBibliot);

            if (tb205 != null && tb205.CO_USUARIO_BIBLIOT != 0)
            {
                switch (tb205.TP_USU_BIB)
                {
                    case "A":
                        tb205.TB07_ALUNOReference.Load();
                        var tb08 = (from lTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                    join tb01 in TB01_CURSO.RetornaTodosRegistros() on lTb08.CO_CUR equals tb01.CO_CUR
                                    join tb06 in TB06_TURMAS.RetornaTodosRegistros() on lTb08.CO_TUR equals tb06.CO_TUR
                                    where lTb08.CO_SIT_MAT == "A" && lTb08.CO_ALU == tb205.TB07_ALUNO.CO_ALU
                                    select new
                                    {
                                        lTb08.TB44_MODULO.DE_MODU_CUR, tb01.NO_CUR, lTb08.DT_EFE_MAT, tb06.TB129_CADTURMAS.NO_TURMA, lTb08.CO_ALU_CAD,
                                        CO_PERI_TUR = tb06.CO_PERI_TUR == "M" ? "Manhã" : tb06.CO_PERI_TUR == "V" ? "Vespertino" : "Noturno"                                             
                                    }).OrderByDescending( m => m.DT_EFE_MAT ).FirstOrDefault();

                        if (tb08 != null)
                        {
                            lblInfoUsuario.Text = string.Format("(Modalidade: {0} - Série: {1} - Turma: {2} - Turno: {3} - Matrícula: {4})",
                                        tb08.DE_MODU_CUR, tb08.NO_CUR, tb08.NO_TURMA, tb08.CO_PERI_TUR, tb08.CO_ALU_CAD);
                        }
                        break;
                    case "P":
                        tb205.TB03_COLABORReference.Load();
                        tb205.TB03_COLABOR.TB25_EMPRESAReference.Load();

                        strTexto = "Professor - Escola: {0}, Matrícula: {1}";
                        string strNoFantasEmpProfessor = tb205.TB03_COLABOR.TB25_EMPRESA.NO_FANTAS_EMP;
                        string strMatriculaProfessor = tb205.TB03_COLABOR.CO_MAT_COL;
                        lblInfoUsuario.Text = string.Format(strTexto, strNoFantasEmpProfessor, strMatriculaProfessor);
                        break;
                    case "F":
                        tb205.TB03_COLABORReference.Load();
                        tb205.TB03_COLABOR.TB25_EMPRESAReference.Load();

                        strTexto = "Função: {0}, Unidade: {1}, Matrícula: {2}";
                        string strFuncaoFuncionario = TB15_FUNCAO.RetornaPelaChavePrimaria(tb205.TB03_COLABOR.CO_FUN).NO_FUN;
                        string strNoFantasEmpFuncionario = tb205.TB03_COLABOR.TB25_EMPRESA.NO_FANTAS_EMP;
                        string strMatriculaFuncionario = tb205.TB03_COLABOR.CO_MAT_COL;
                        lblInfoUsuario.Text = string.Format(strTexto, strFuncaoFuncionario, strNoFantasEmpFuncionario, strMatriculaFuncionario);
                        break;
                    case "O":
                        strTexto = "Usuário externo - CPF: {0} - RG: {1}";
                        string strCpfUsuario = tb205.NU_CPF_USU_BIB.Length == 11 ? tb205.NU_CPF_USU_BIB.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb205.NU_CPF_USU_BIB;
                        lblInfoUsuario.Text = string.Format(strTexto, strCpfUsuario, tb205.CO_RG_USU_BIB);
                        break;
                    default:
                        lblInfoUsuario.Text = "";
                        break;
                }
            }
            else
                lblInfoUsuario.Text = "";
        }

        protected void ddlAreaConhecimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaClassificacoes();
        }
        #endregion
    }
}
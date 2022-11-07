//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: GERENCIAMENTO DE USUÁRIOS DO GE
// OBJETIVO: ASSOCIA USUÁRIO A UNIDADES EDUCACIONAIS DE FREQUÊNCIA
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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.App_Masters;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0300_GerenciamentoUsuarios.F0303_AssociaUsuUnidEducFrequencia
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Variaveis

        private static List<EstrutDadosFuncUnidFreq> dadosFuncUnidFreq;
        private static List<bool> verificaLista;
        private static int ultimoIndiceSelecionado;

        #endregion

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);          
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            dadosFuncUnidFreq = new List<EstrutDadosFuncUnidFreq>();
            ultimoIndiceSelecionado = -1;

            txtDataInicioAssoc.Enabled = txtDataTerminoAssoc.Enabled = ddlFuncaoAssoc.Enabled = ddlStatusAssoc.Enabled =
            ddlTipoPontoAssoc.Enabled = chkManhaAssoc.Enabled = chkTardeAssoc.Enabled = chkNoiteAssoc.Enabled = false;

            CarregaUsuario();
            ddlUsuarioAssoc.SelectedValue = QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.CoCol);

            CarregaUnidades();
            CarregaFuncao();
        }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coCol = ddlUsuarioAssoc.SelectedValue != "" ? int.Parse(ddlUsuarioAssoc.SelectedValue) : 0;

            if (ultimoIndiceSelecionado != -1 && cblUnidadeAssoc.Items[ultimoIndiceSelecionado].Selected)
            {
//------------> Adicionando dado selecionado a lista dadosFuncUnidFreq 
                EstrutDadosFuncUnidFreq estDadFunUniFre = new EstrutDadosFuncUnidFreq();

                estDadFunUniFre.NO_FANTAS_EMP = dadosFuncUnidFreq[ultimoIndiceSelecionado].NO_FANTAS_EMP;
                estDadFunUniFre.CO_EMP = dadosFuncUnidFreq[ultimoIndiceSelecionado].CO_EMP;
                estDadFunUniFre.dataInicio = txtDataInicioAssoc.Text != "" ? (DateTime?)DateTime.Parse(txtDataInicioAssoc.Text) : null;
                estDadFunUniFre.dataTermino = txtDataTerminoAssoc.Text != "" ? (DateTime?)DateTime.Parse(txtDataTerminoAssoc.Text) : null;
                estDadFunUniFre.funcao = ddlFuncaoAssoc.SelectedValue != "" ? int.Parse(ddlFuncaoAssoc.SelectedValue) : 0;
                estDadFunUniFre.manha = chkManhaAssoc.Checked;
                estDadFunUniFre.tarde = chkTardeAssoc.Checked;
                estDadFunUniFre.noite = chkNoiteAssoc.Checked;
                estDadFunUniFre.situacao = ddlStatusAssoc.SelectedValue;
                estDadFunUniFre.tipoPonto = ddlTipoPontoAssoc.SelectedValue == TipoPontoFrequencia.P.ToString() ? TipoPontoFrequencia.P : TipoPontoFrequencia.L;

                dadosFuncUnidFreq[ultimoIndiceSelecionado] = estDadFunUniFre;
            }

            for (int i = 0; i < cblUnidadeAssoc.Items.Count; i++) 
            {
                EstrutDadosFuncUnidFreq estDadFunUniFre = dadosFuncUnidFreq[i];
                TB198_USR_UNID_FREQ tb198 =
                    TB198_USR_UNID_FREQ.RetornaPeloColaboradorUnidadeFreq(coCol, LoginAuxili.CO_EMP, estDadFunUniFre.CO_EMP);

                if (cblUnidadeAssoc.Items[i].Selected)
                {
                    if (estDadFunUniFre.dataInicio == null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Data de Início na unidade " + estDadFunUniFre.NO_FANTAS_EMP + " deve ser informada");
                        return;
                    }

                    if (estDadFunUniFre.funcao == 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Função na unidade " + estDadFunUniFre.NO_FANTAS_EMP + " deve ser informada");
                        return;
                    }

                    if (estDadFunUniFre.situacao == "")
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Situação na unidade " + estDadFunUniFre.NO_FANTAS_EMP + " deve ser informada");
                        return;
                    }

                    if (tb198 == null)
                    {
                        tb198 = new TB198_USR_UNID_FREQ();

                        tb198.CO_COL = coCol;
                        tb198.CO_EMP = LoginAuxili.CO_EMP;
                        tb198.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(tb198.CO_EMP, tb198.CO_COL);
                        tb198.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(estDadFunUniFre.CO_EMP);
                        tb198.DT_CADASTRO = DateTime.Now;
                    }
                    tb198.TB15_FUNCAO = TB15_FUNCAO.RetornaPelaChavePrimaria(estDadFunUniFre.funcao);
                    tb198.CO_SITU_UNID_FREQ = estDadFunUniFre.situacao;
                    tb198.DT_INICIO_ATIVID = estDadFunUniFre.dataInicio.Value;
                    tb198.DT_TERMIN_ATIVID = estDadFunUniFre.dataTermino != null ? estDadFunUniFre.dataTermino : null;
                    tb198.FLA_TURNO_MANHA = estDadFunUniFre.manha ? "S" : "N";
                    tb198.FLA_TURNO_TARDE = estDadFunUniFre.tarde ? "S" : "N";
                    tb198.FLA_TURNO_NOITE = estDadFunUniFre.noite ? "S" : "N";
                    tb198.TP_PONTO_FREQ = estDadFunUniFre.tipoPonto.ToString();

                    if (GestorEntities.SaveOrUpdate(tb198) <= 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar item " + estDadFunUniFre.NO_FANTAS_EMP);
                        return;
                    }
                }
                else 
                {
                    if (tb198 != null && GestorEntities.Delete(tb198) <= 0)                    
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Erro ao excluir registro " + estDadFunUniFre.NO_FANTAS_EMP);
                        return;
                    }
                }
            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Registros salvos com sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
        }        

        #endregion

        #region Métodos

        /// <summary>
        /// Método que retorna o Índice selecionado do cblUnidadeAssoc
        /// </summary>
        /// <returns>Índice selecionado da cblUnidadeAssoc</returns>
        private int RetornaIndiceSelecionado()
        {
            if (cblUnidadeAssoc.SelectedIndex != -1 && verificaLista != null)
                for (int indice = 0; indice < cblUnidadeAssoc.Items.Count; indice++)
                    if (!verificaLista[indice] && cblUnidadeAssoc.Items[indice].Selected)
                        return indice;

            return cblUnidadeAssoc.SelectedIndex;
        }        
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o cblUnidadeAssoc com as Unidades de Frequência
        /// </summary>
        private void CarregaUnidades()
        {
            cblUnidadeAssoc.Items.Clear();
            dadosFuncUnidFreq.Clear();

            int coCol = ddlUsuarioAssoc.SelectedValue != "" ? int.Parse(ddlUsuarioAssoc.SelectedValue) : 0;

            if (coCol == 0) 
                return;

            var listaUnidade = (from tb25 in TB25_EMPRESA.RetornaPeloUsuario(int.Parse(ddlUsuarioAssoc.SelectedValue))
                                select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP }).OrderBy( e => e.NO_FANTAS_EMP );

            foreach (var unidade in listaUnidade)
            {
//------------> Pesquisa para saber se é uma unidade de frequência do usuário
                TB198_USR_UNID_FREQ tb198 = TB198_USR_UNID_FREQ.RetornaPeloColaboradorUnidadeFreq(coCol, LoginAuxili.CO_EMP, unidade.CO_EMP);

                ListItem lstItem = new ListItem(unidade.NO_FANTAS_EMP, unidade.CO_EMP.ToString());
                lstItem.Selected = tb198 != null;

//------------> Adiciona item ao cblUnidadeAssoc
                cblUnidadeAssoc.Items.Add(lstItem);

                EstrutDadosFuncUnidFreq estDadFunUniFre = new EstrutDadosFuncUnidFreq();
                estDadFunUniFre.CO_EMP = unidade.CO_EMP;
                estDadFunUniFre.NO_FANTAS_EMP = unidade.NO_FANTAS_EMP;

                if (tb198 != null)
                {
                    tb198.TB15_FUNCAOReference.Load();

                    estDadFunUniFre.dataInicio = tb198.DT_INICIO_ATIVID;
                    estDadFunUniFre.dataTermino = tb198.DT_TERMIN_ATIVID != null ? tb198.DT_TERMIN_ATIVID : null;
                    estDadFunUniFre.funcao = tb198.TB15_FUNCAO.CO_FUN;
                    estDadFunUniFre.manha = tb198.FLA_TURNO_MANHA == "S";
                    estDadFunUniFre.tarde = tb198.FLA_TURNO_TARDE == "S";
                    estDadFunUniFre.noite = tb198.FLA_TURNO_NOITE == "S";
                    estDadFunUniFre.situacao = tb198.CO_SITU_UNID_FREQ;
                    estDadFunUniFre.tipoPonto = tb198.TP_PONTO_FREQ == TipoPontoFrequencia.L.ToString() ? TipoPontoFrequencia.L : TipoPontoFrequencia.P;
                }

//------------> Adiciona item a lista da Estrutura de dadosFuncUnidFreq
                dadosFuncUnidFreq.Add(estDadFunUniFre);
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Usuários
        /// </summary>
        private void CarregaUsuario()
        {
            ddlUsuarioAssoc.DataSource = (from tb03 in TB03_COLABOR.RetornaPeloCoUnid(LoginAuxili.CO_EMP)
                                          select new { tb03.CO_COL, tb03.NO_COL }).OrderBy( c => c.NO_COL );

            ddlUsuarioAssoc.DataTextField = "NO_COL";
            ddlUsuarioAssoc.DataValueField = "CO_COL";
            ddlUsuarioAssoc.DataBind();

            ddlUsuarioAssoc.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Função do Funcionário
        /// </summary>
        private void CarregaFuncao()
        {
            ddlFuncaoAssoc.DataSource = (from tb15 in TB15_FUNCAO.RetornaTodosRegistros()
                                         select new { tb15.CO_FUN, tb15.NO_FUN }).OrderBy( f => f.NO_FUN );

            ddlFuncaoAssoc.DataTextField = "NO_FUN";
            ddlFuncaoAssoc.DataValueField = "CO_FUN";
            ddlFuncaoAssoc.DataBind();

            ddlFuncaoAssoc.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion        

        protected void cblUnidadeAssoc_SelectedIndexChanged(object sender, EventArgs e)
        {
//--------> Recebe o item selecionado            
            int indiceSelecionado = RetornaIndiceSelecionado();

            if (ultimoIndiceSelecionado != -1 && cblUnidadeAssoc.Items[ultimoIndiceSelecionado].Selected)
            {
//------------> Adiciona os dados na lista dadosFuncUnidFreq                
                EstrutDadosFuncUnidFreq estDadFunUniFre = new EstrutDadosFuncUnidFreq();

                estDadFunUniFre.NO_FANTAS_EMP = dadosFuncUnidFreq[ultimoIndiceSelecionado].NO_FANTAS_EMP;
                estDadFunUniFre.CO_EMP = dadosFuncUnidFreq[ultimoIndiceSelecionado].CO_EMP;
                estDadFunUniFre.dataInicio = txtDataInicioAssoc.Text != "" ? (DateTime?)DateTime.Parse(txtDataInicioAssoc.Text) : null;
                estDadFunUniFre.dataTermino = txtDataTerminoAssoc.Text != "" ? (DateTime?)DateTime.Parse(txtDataTerminoAssoc.Text) : null;
                estDadFunUniFre.funcao = ddlFuncaoAssoc.SelectedValue != "" ? int.Parse(ddlFuncaoAssoc.SelectedValue) : 0;
                estDadFunUniFre.manha = chkManhaAssoc.Checked;
                estDadFunUniFre.tarde = chkTardeAssoc.Checked;
                estDadFunUniFre.noite = chkNoiteAssoc.Checked;
                estDadFunUniFre.situacao = ddlStatusAssoc.SelectedValue;
                estDadFunUniFre.tipoPonto = ddlTipoPontoAssoc.SelectedValue == TipoPontoFrequencia.P.ToString() ? TipoPontoFrequencia.P : TipoPontoFrequencia.L;

                dadosFuncUnidFreq[ultimoIndiceSelecionado] = estDadFunUniFre;
            }

            if (indiceSelecionado != -1)
            {
//------------> Carrega os dados                
                int coEmp = int.Parse(cblUnidadeAssoc.Items[indiceSelecionado].Value);

                EstrutDadosFuncUnidFreq estDadFunUniFre = (from dados in dadosFuncUnidFreq
                                                           where dados.CO_EMP == coEmp
                                                           select dados).FirstOrDefault();

                txtDataInicioAssoc.Enabled = txtDataTerminoAssoc.Enabled = ddlFuncaoAssoc.Enabled = ddlStatusAssoc.Enabled =
                ddlTipoPontoAssoc.Enabled = chkManhaAssoc.Enabled = chkTardeAssoc.Enabled = chkNoiteAssoc.Enabled = true;
                lblUnidadeSelecionadaAssoc.Text = estDadFunUniFre.NO_FANTAS_EMP;
                txtDataInicioAssoc.Text = estDadFunUniFre.dataInicio != null ? estDadFunUniFre.dataInicio.Value.ToString("dd/MM/yyyy") : "";
                txtDataTerminoAssoc.Text = estDadFunUniFre.dataTermino != null ? estDadFunUniFre.dataTermino.Value.ToString("dd/MM/yyyy") : "";
                ddlFuncaoAssoc.SelectedValue = estDadFunUniFre.funcao != 0 ? estDadFunUniFre.funcao.ToString() : "";
                ddlStatusAssoc.SelectedValue = estDadFunUniFre.situacao;
                ddlTipoPontoAssoc.SelectedValue = estDadFunUniFre.tipoPonto.ToString();
                chkManhaAssoc.Checked = estDadFunUniFre.manha;
                chkTardeAssoc.Checked = estDadFunUniFre.tarde;
                chkNoiteAssoc.Checked = estDadFunUniFre.noite;
            }
            else
            {
//------------> Limpa os campos                
                txtDataInicioAssoc.Enabled = txtDataTerminoAssoc.Enabled = ddlFuncaoAssoc.Enabled = ddlStatusAssoc.Enabled = 
                ddlTipoPontoAssoc.Enabled = chkManhaAssoc.Enabled = chkTardeAssoc.Enabled = chkNoiteAssoc.Enabled = 
                chkManhaAssoc.Checked = chkTardeAssoc.Checked = chkNoiteAssoc.Checked = false;
                lblUnidadeSelecionadaAssoc.Text = txtDataInicioAssoc.Text = txtDataTerminoAssoc.Text = "";
                ddlFuncaoAssoc.SelectedIndex = ddlStatusAssoc.SelectedIndex = ddlTipoPontoAssoc.SelectedIndex = 0;                
            }

            ultimoIndiceSelecionado = indiceSelecionado;

            verificaLista = new List<bool>();
            foreach (ListItem item in cblUnidadeAssoc.Items)
                verificaLista.Add(item.Selected);
        }

        protected void ddlUsuarioAssoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUnidades();
            verificaLista = null;
        }

        #region Estruturas

//----> Estrutura de Dados do Funcionário na Unidade de Frequência
        private struct EstrutDadosFuncUnidFreq
        {
//--------> Código da unidade            
            public int CO_EMP;

//--------> Noma fantasia da unidade
            public string NO_FANTAS_EMP;

//--------> Função do funcionário
            public int funcao;

//--------> Tipo de ponto de frequência da unidade
            public TipoPontoFrequencia tipoPonto;

//--------> Disponibilidade do Turno da manhã da unidade            
            public bool manha;

//--------> Disponibilidade do Turno da tarde da unidade           
            public bool tarde;

//--------> Disponibilidade do Turno da noite da unidade
            public bool noite;

//--------> Data de Início das atividades do funcionário          
            public DateTime? dataInicio;

//--------> Data de Término das atividades do funcionário
            public DateTime? dataTermino;

//--------> Situação do funcionário
            public string situacao;
        }
        #endregion
    }
}
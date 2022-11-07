//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE SÉRIES OU CURSOS
// OBJETIVO: GERA GRADE ANUAL DE DISCIPLINA (MATÉRIA) DE SÉRIE/CURSO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+------------------------------------
// 17/10/2014| Maxwell Almeida            |  Criação da funcionalidade para Associação de Vacinas à Campanhas de Vacinação
//           |                            | 
//           |                            |  

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9100_CtrlCampanhasSaude._9110_Registros._9116_AssocVacinasCamp
{
    public partial class Cadastro : System.Web.UI.Page
    {
        #region Váriaveis
        int qtdLinhasGridCamp = 0;
        int qtdLinhasGridVacinas = 0;
        #endregion

        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaTipos();
                CarregaSituacoes();
                CarregaClassificacoes();
                CarregaCompetencias();
                CarregaGrid();
                CarregaGridVacinasdisponiveis();
                CarregaDados();
            }
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            updInfos.Update();
            SalvaEmSessaoDados();
            AssociaVacinas();
        }

        #region Carregamentos

        /// <summary>
        /// Carrega os tipos de campanhas
        /// </summary>
        private void CarregaTipos()
        {
            AuxiliCarregamentos.CarregaTiposCampanhaSaude(ddlTipoCamp, true);
            ddlTipoCamp.SelectedValue = "V";
            ddlTipoCamp.Enabled = false;
        }

        /// <summary>
        /// Carrega as competências
        /// </summary>
        private void CarregaCompetencias()
        {
            AuxiliCarregamentos.CarregaCompetenciasCampanhaSaude(ddlCompetencia, true);
        }

        /// <summary>
        /// Carrega as Classificações
        /// </summary>
        private void CarregaClassificacoes()
        {
            AuxiliCarregamentos.CarregaClassificacoesCampanhaSaude(ddlClassCamp, true);
        }

        /// <summary>
        /// Carrega as Situacoes
        /// </summary>
        private void CarregaSituacoes()
        {
            AuxiliCarregamentos.CarregaSituacaoCampanhaSaude(ddlSituCampSaude, true);
        }

        /// <summary>
        /// Carrega a Grid de campanhas em andamento
        /// </summary>
        private void CarregaGrid()
        {
            string comp = ddlCompetencia.SelectedValue;
            string classif = ddlClassCamp.SelectedValue;
            string tipo = ddlTipoCamp.SelectedValue;
            string situ = ddlSituCampSaude.SelectedValue;
            DateTime dtAtu = DateTime.Now;
            var res = (from tbs339 in TBS339_CAMPSAUDE.RetornaTodosRegistros()
                       where (comp != "0" ? tbs339.CO_COMPE_TIPO_CAMPAN == comp : 0 == 0)
                       && (classif != "0" ? tbs339.CO_CLASS_CAMPAN == classif : 0 == 0)
                       && (tipo != "0" ? tbs339.CO_TIPO_CAMPAN == tipo : 0 == 0)
                       && (situ != "0" ? tbs339.CO_SITUA_TIPO_CAMPAN == situ : 0 == 0)
                       && ((dtAtu >= tbs339.DT_INICI_CAMPAN) && (dtAtu <= tbs339.DT_TERMI_CAMPAN))
                       select new campanhaSaude
                       {
                           ID_CAMPAN = tbs339.ID_CAMPAN,
                           noCampa = tbs339.NM_CAMPAN,
                           tipo = tbs339.CO_TIPO_CAMPAN,
                           comp = tbs339.CO_COMPE_TIPO_CAMPAN,
                           dataInicio = tbs339.DT_INICI_CAMPAN,
                           HORA = tbs339.HR_INICI_CAMPAN,
                           classi = tbs339.CO_CLASS_CAMPAN,
                       }).ToList();

            grdCampSaude.DataSource = res;
            grdCampSaude.DataBind();
        }

        /// <summary>
        /// Carrega a grid de Vacinas disponíveis
        /// </summary>
        private void CarregaGridVacinasdisponiveis()
        {
            var res = (from tbs345 in TBS345_VACINA.RetornaTodosRegistros()
                       where (!string.IsNullOrEmpty(txtSiglaVacina.Text) ? tbs345.CO_SIGLA_VACINA.Contains(txtSiglaVacina.Text) : txtSiglaVacina.Text == "")
                       && (!string.IsNullOrEmpty(txtNomeVacina.Text) ? tbs345.NM_VACINA.Contains(txtNomeVacina.Text) : txtNomeVacina.Text == "")
                       && tbs345.CO_SITUA_VACINA == "A"
                       select new Vacinas
                       {
                           ID_VACINA = tbs345.ID_VACINA,
                           NM_VACINA = tbs345.NM_VACINA,
                           CO_SIGLA_VACINA = tbs345.CO_SIGLA_VACINA,
                       }).OrderBy(w => w.NM_VACINA).ToList();

            grdVacinasDisponiveis.DataSource = res;
            grdVacinasDisponiveis.DataBind();

            updVacinasDisponiveis.Update();
        }

        /// <summary>
        /// Carrega as Vacinas associadas à Campanha de Saúde recebida como parâmetro
        /// </summary>
        /// <param name="ID_CAMPAN"></param>
        private void CarregaGridVacinasAssociadas(int ID_CAMPAN)
        {
            var res = (from tbs360 in TBS360_VACIN_CAMPSAUDE.RetornaTodosRegistros()
                       where tbs360.TBS339_CAMPSAUDE.ID_CAMPAN == ID_CAMPAN
                       && tbs360.CO_SITUA == "A"
                       select new Vacinas
                       {
                           ID_VACINA = tbs360.TBS345_VACINA.ID_VACINA,
                           NM_VACINA = tbs360.TBS345_VACINA.NM_VACINA,
                           CO_SIGLA_VACINA = tbs360.TBS345_VACINA.CO_SIGLA_VACINA,
                           ID_VACIN_CAMPSAUDE = tbs360.ID_VACIN_CAMPSAUDE,
                       }).ToList();

            grdVacinasAssociadas.DataSource = res;
            grdVacinasAssociadas.DataBind();
        }

        /// <summary>
        /// Método responsável por realizar as associações das Vacinas selecionadas à campanha de saúde selecionada
        /// </summary>
        private void AssociaVacinas()
        {
            //Validações
            if (string.IsNullOrEmpty(hidIdCampa.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar a Campanha de Saúde para a qual será(ão) associada(s) a(s) Vacina(s)");
                return;
            }

            bool temSelec = false;
            int qtdVacinas = 0;
            foreach (GridViewRow li in grdVacinasDisponiveis.Rows)
            {
                if (((CheckBox)li.Cells[0].FindControl("chkSelectVacin")).Checked)
                {
                    int idVacina = int.Parse(((HiddenField)li.Cells[0].FindControl("hidIdVacina")).Value);

                    //Apenas salva associação se ela ainda não existir
                    if (!VerificaExisteAssociacao(int.Parse(hidIdCampa.Value), idVacina))
                    {
                        temSelec = true;
                        qtdVacinas++;

                        TBS360_VACIN_CAMPSAUDE tbs360 = new TBS360_VACIN_CAMPSAUDE();
                        tbs360.TBS339_CAMPSAUDE = TBS339_CAMPSAUDE.RetornaPelaChavePrimaria(int.Parse(hidIdCampa.Value));
                        tbs360.TBS345_VACINA = TBS345_VACINA.RetornaPelaChavePrimaria(idVacina);
                        tbs360.CO_COL_CADAS = LoginAuxili.CO_COL;
                        tbs360.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                        tbs360.DT_CADAS = DateTime.Now;
                        tbs360.IP_CADAS = Request.UserHostAddress;
                        tbs360.CO_SITUA = "A";
                        tbs360.CO_COL_SITUA = LoginAuxili.CO_COL;
                        tbs360.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        tbs360.DT_SITUA = DateTime.Now;
                        tbs360.IP_SITUA = Request.UserHostAddress;
                        TBS360_VACIN_CAMPSAUDE.SaveOrUpdate(tbs360, true);
                    }
                }
            }

            //Valida se foi selecionada alguma vacina
            if (!temSelec)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso selecionar a(s) Vacina(s) que será(ão) Associada(s)");
                return;
            }
            else
            {
                string dadosMultaveis = qtdVacinas > 1 ? " Vacinas associadas " : " Vacina associada ";
                int qtd = ((!string.IsNullOrEmpty(hidQtdAssoc.Value) ? int.Parse(hidQtdAssoc.Value) : 0)) + qtdVacinas;
                hidQtdAssoc.Value = qtd.ToString();
                AuxiliPagina.RedirecionaParaPaginaSucesso(qtdVacinas + dadosMultaveis + "à Campanha de Saúde selecionada", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
            }
        }

        /// <summary>
        /// Método responsável por verificar, de acordo com a campanha e vacina recebidas como parâmetro, se existe associação, e retornar o bool correspondente
        /// </summary>
        /// <param name="ID_CAMPAN"></param>
        /// <param name="ID_VACINA"></param>
        /// <returns></returns>
        private bool VerificaExisteAssociacao(int ID_CAMPAN, int ID_VACINA)
        {
            return TBS360_VACIN_CAMPSAUDE.RetornaTodosRegistros().Where(w => w.TBS339_CAMPSAUDE.ID_CAMPAN == ID_CAMPAN && w.TBS345_VACINA.ID_VACINA == ID_VACINA).Any();
        }

        /// <summary>
        /// Salva as informações em tela em session para recarregar posteriormente
        /// </summary>
        private void SalvaEmSessaoDados()
        {
            var parametros = hidIdCampa.Value + ";" + hidQtdAssoc.Value;

            HttpContext.Current.Session["InfosVacinasACVV"] = parametros;
        }

        /// <summary>
        /// Carrega os dados salvos em sessão
        /// </summary>
        private void CarregaDados()
        {
            if (HttpContext.Current.Session["InfosVacinasACVV"] != null)
            {
                var parametros = HttpContext.Current.Session["InfosVacinasACVV"].ToString();

                if (!string.IsNullOrEmpty(parametros))
                {
                    var par = parametros.ToString().Split(';');

                    var idCampanha = par[0];
                    var qtdAssoc = par[1];

                    hidIdCampa.Value = idCampanha;
                    hidQtdAssoc.Value = qtdAssoc;

                    #region Marca Campanha de Código idCampanha

                    foreach (GridViewRow li in grdCampSaude.Rows)
                    {
                        string idCa = ((HiddenField)li.Cells[0].FindControl("hidCoCampan")).Value;
                        //Se for a campanha correspondente, marca.
                        if (idCa == idCampanha)
                        {
                            ((CheckBox)li.Cells[0].FindControl("chkSelectCamp")).Checked = true;
                            break;
                        }
                    }

                    #endregion

                    CarregaGridVacinasdisponiveis();
                    CarregaGridVacinasAssociadas(int.Parse(idCampanha));
                    updCampanha.Update();
                    updVacinasAssociadas.Update();
                    updVacinasDisponiveis.Update();
                }
                HttpContext.Current.Session.Remove("InfosVacinasACVV");
            }
        }

        #endregion

        #region Classes

        public class campanhaSaude
        {
            public int ID_CAMPAN { get; set; }
            public string noCampa { get; set; }
            public string tipo { get; set; }
            public string tipo_Valid
            {
                get
                {
                    string tipo = "";
                    switch (this.tipo)
                    {
                        case "V":
                            tipo = "Vacinação";
                            break;
                        case "A":
                            tipo = "Ações";
                            break;
                        case "P":
                            tipo = "Programas";
                            break;
                    }

                    return tipo;
                }
            }
            public string comp { get; set; }
            public string comp_Valid
            {
                get
                {
                    string s = "";
                    switch (this.comp)
                    {
                        case "F":
                            s = "Federal";
                            break;
                        case "E":
                            s = "Estadual";
                            break;
                        case "M":
                            s = "Municipal";
                            break;
                        case "X":
                            s = "Conjunta";
                            break;
                        case "O":
                            s = "Outras";
                            break;
                    }
                    return s;

                }
            }
            public string classi { get; set; }
            public string classi_Valid
            {
                get
                {
                    string s = "";
                    switch (this.classi)
                    {
                        case "EDU":
                            s = "Educativa";
                            break;
                        case "TEM":
                            s = "Temática";
                            break;
                        case "PRO":
                            s = "Programada";
                            break;
                        case "EPI":
                            s = "Epidemia";
                            break;
                        case "OUT":
                            s = "Outras";
                            break;
                    }
                    return s;
                }
            }
            public DateTime dataInicio { get; set; }
            public string dataValid
            {
                get
                {
                    return this.dataInicio.ToString("dd/MM/yy");
                }
            }
            public string HORA { get; set; }
        }

        public class Vacinas
        {
            public int ID_VACINA { get; set; }
            public string NM_VACINA { get; set; }
            public string CO_SIGLA_VACINA { get; set; }
            public int ID_VACIN_CAMPSAUDE { get; set; }
        }

        #endregion

        #region Funções de Campo

        protected void grdCampSaude_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                e.Row.Attributes.Add("onClick", "javascript:__doPostBack('" + grdCampSaude.UniqueID + "','Select$" + qtdLinhasGridCamp + "')");
                qtdLinhasGridCamp = qtdLinhasGridCamp + 1;
            }
        }

        protected void grdCampSaude_SelectedIndexChanged(object sender, EventArgs e)
        {
            //--------> Define o código da Unidade Educacional Selecionada como a Unidade em uso
            if (grdCampSaude.DataKeys[grdCampSaude.SelectedIndex].Value != null)
            {
                // Passa por todos os registros da grid de Pré-Atendimentos
                foreach (GridViewRow linha in grdCampSaude.Rows)
                {
                    CheckBox chk = (CheckBox)linha.Cells[0].FindControl("chkSelectCamp");
                    int idCamp = int.Parse((((HiddenField)linha.Cells[0].FindControl("hidCoCampan")).Value));

                    //Verifica se foi clicada uma linha diferente da selecionada, caso tenha sido, desmarca a linha
                    if (idCamp == Convert.ToInt32(grdCampSaude.DataKeys[grdCampSaude.SelectedIndex].Value))
                    //Se for a mesma linha então seleciona o checkbox correspondente
                    {
                        //Verifico se já está selecionado, caso já esteja eu recorro aos padrões de limpeza de dados
                        if (chk.Checked)
                        {
                            chk.Checked = false;
                            hidIdCampa.Value = "";
                            grdVacinasAssociadas.DataSource = null;
                            grdVacinasAssociadas.DataBind();
                        }
                        //Caso não esteja selecionado ainda, chamo os métodos responsáveis pelos carregamentos
                        else
                        {
                            chk.Checked = true;
                            int idCampan = Convert.ToInt32(grdCampSaude.DataKeys[grdCampSaude.SelectedIndex].Value);
                            hidIdCampa.Value = idCampan.ToString();
                            CarregaGridVacinasAssociadas(idCampan);
                        }

                        CarregaGridVacinasdisponiveis();
                        updVacinasAssociadas.Update();
                    }
                    else
                        chk.Checked = false;
                }
            }
        }

        protected void chkSelectCamp_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            // Valida se a grid de atividades possui algum registro
            if (grdCampSaude.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdCampSaude.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("chkSelectCamp");

                    if (chk.ClientID == atual.ClientID)
                    {
                        if (chk.Checked)
                        {
                            int idCampan = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoCampan")).Value);
                            CarregaGridVacinasAssociadas(idCampan);
                            hidIdCampa.Value = idCampan.ToString();
                        }
                        else
                        {
                            hidIdCampa.Value = "";
                            grdVacinasAssociadas.DataSource = null;
                            grdVacinasAssociadas.DataBind();
                        }
                        CarregaGridVacinasdisponiveis();
                    }
                    else
                        chk.Checked = false;
                }
            }
        }

        protected void grdVacinasDisponiveis_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                //Verifica se existe associação da campanha selecionada com o a vacina em questão
                #region Verificação

                int idCampa = (!string.IsNullOrEmpty(hidIdCampa.Value) ? int.Parse(hidIdCampa.Value) : 0);
                int idVacina = int.Parse(((HiddenField)e.Row.Cells[0].FindControl("hidIdVacina")).Value);

                #endregion

                //Trata para que, quando já houver associação, a linha da vacina seja "desabilitada"
                if (VerificaExisteAssociacao(idCampa, idVacina))
                {
                    e.Row.Attributes.Add("onMouseOver", "this.style.cursor='default';");
                    e.Row.BackColor = System.Drawing.Color.LightSalmon;
                    //e.Row.BackColor = System.Drawing.Color.WhiteSmoke;
                    ((CheckBox)e.Row.Cells[0].FindControl("chkSelectVacin")).Visible = false;
                    e.Row.Visible = (chkSomenteDisponiveis.Checked ? false : true);
                }
                else
                {
                    e.Row.Attributes.Add("onMouseOver", "this.style.cursor='pointer';");
                    e.Row.Attributes.Add("onClick", "javascript:__doPostBack('" + grdVacinasDisponiveis.UniqueID + "','Select$" + qtdLinhasGridVacinas + "')");
                }
                qtdLinhasGridVacinas = qtdLinhasGridVacinas + 1;
            }
            updVacinasDisponiveis.Update();
        }

        protected void grdVacinasDisponiveis_SelectedIndexChanged(object sender, EventArgs e)
        {
            //--------> Define o código da Unidade Educacional Selecionada como a Unidade em uso
            if (grdVacinasDisponiveis.DataKeys[grdVacinasDisponiveis.SelectedIndex].Value != null)
            {
                // Passa por todos os registros da grid de Pré-Atendimentos
                foreach (GridViewRow linha in grdVacinasDisponiveis.Rows)
                {
                    CheckBox chk = (CheckBox)linha.Cells[0].FindControl("chkSelectVacin");
                    int idCamp = int.Parse((((HiddenField)linha.Cells[0].FindControl("hidIdVacina")).Value));

                    //Verifica se foi clicada uma linha diferente da selecionada, caso tenha sido, desmarca a linha
                    if (idCamp == Convert.ToInt32(grdVacinasDisponiveis.DataKeys[grdVacinasDisponiveis.SelectedIndex].Value))
                    //Se for a mesma linha então seleciona o checkbox correspondente
                    {
                        //Verifico se já está selecionado, caso já esteja eu recorro aos padrões de limpeza de dados
                        if (chk.Checked)
                        {
                            chk.Checked = false;
                        }
                        //Caso não esteja selecionado ainda, chamo os métodos responsáveis pelos carregamentos
                        else
                        {
                            chk.Checked = true;
                        }
                    }
                }
            }
        }

        protected void chkSelectVacin_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            // Valida se a grid de atividades possui algum registro
            if (grdVacinasDisponiveis.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdVacinasDisponiveis.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("chkSelectVacin");

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID == atual.ClientID)
                    {
                        if (chk.Checked)
                        {
                            int idCampan = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidIdVacina")).Value);
                            //CarregaDadosCampanha(idCampan);
                        }
                        else
                        {
                        }
                    }
                }
            }
        }

        protected void imgPesqVacinas_OnClick(object sende, EventArgs e)
        {
            CarregaGridVacinasdisponiveis();
        }

        protected void imgPesqCampV_OnClick(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        protected void lnkExc_OnClick(object sender, EventArgs e)
        {
            LinkButton atual = (LinkButton)sender;
            LinkButton img;
            int qtdExclu = 0;

            if (grdVacinasAssociadas.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdVacinasAssociadas.Rows)
                {
                    img = (LinkButton)linha.Cells[2].FindControl("lnkExc");

                    //Verifica se a imagem é a mesma que iniciou o postback
                    if (img.ClientID == atual.ClientID)
                    {
                        qtdExclu++;
                        HiddenField hidIdAssoc = ((HiddenField)linha.Cells[0].FindControl("hidIdAssoc"));
                        int idAssoc = Convert.ToInt32(hidIdAssoc.Value);

                        TBS360_VACIN_CAMPSAUDE assoc = TBS360_VACIN_CAMPSAUDE.RetornaPelaChavePrimaria(idAssoc);

                        TBS360_VACIN_CAMPSAUDE.Delete(assoc, true);
                    }
                }
            }

            int qtd = ((!string.IsNullOrEmpty(hidQtdAssoc.Value) ? int.Parse(hidQtdAssoc.Value) : 0)) + qtdExclu;
            hidQtdAssoc.Value = qtd.ToString();
            updInfos.Update();

            CarregaGridVacinasAssociadas((!string.IsNullOrEmpty(hidIdCampa.Value) ? int.Parse(hidIdCampa.Value) : 0));
            CarregaGridVacinasdisponiveis();
            updVacinasDisponiveis.Update();
        }

        protected void chkMarcaTodosItensPendentes_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkMarca = ((CheckBox)grdVacinasDisponiveis.HeaderRow.Cells[0].FindControl("chkMarcaTodasVacinas"));

            //Percorre alterando o checkbox de acordo com o selecionado em marcar todos
            foreach (GridViewRow li in grdVacinasDisponiveis.Rows)
            {
                CheckBox ck = (((CheckBox)li.Cells[0].FindControl("chkSelectVacin")));
                ck.Checked = chkMarca.Checked;
            }
        }

        protected void chkSomenteDisponiveis_OnCheckedChanged(object sender, EventArgs e)
        {
            CarregaGridVacinasdisponiveis();
        }

        #endregion
    }
}
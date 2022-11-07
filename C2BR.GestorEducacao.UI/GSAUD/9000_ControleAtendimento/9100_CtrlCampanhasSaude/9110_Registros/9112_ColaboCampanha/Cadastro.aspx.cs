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
// 17/10/2014| Maxwell Almeida            |  Criação da funcionalidade para registro de Colaboradores de Campanhas de Saúde
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
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9100_CtrlCampanhasSaude._9110_Registros._9112_ColaboCampanha
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDataIniCamp.Text = DateTime.Now.AddMonths(-1).ToString();
                txtDataFimCamp.Text = DateTime.Now.AddMonths(1).ToString();

                CarregaFuncionarios();
                CarregaSituacoesColabSaude();
                CarregaClassificacoes();
                CarregaCompetencias();
                CarregaTipos();
                CarregaSituacoes();
                CarregaGridCampanhasSaude();
            }
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            //Valida se todos os campos necessários foram selecionados
            #region Validações
            bool salvo = false;
            foreach (GridViewRow at in grdCampSaude.Rows)
            {
                if (((CheckBox)at.Cells[0].FindControl("chkSelectCamp")).Checked)
                {
                    salvo = true;
                    break;
                }
            }

            if (salvo == false)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Nenhuma Campanha de Saúde para a qual associar o(a) Colaborador(a) foi selecionada");
                return;
            }

            if (string.IsNullOrEmpty(txtNoColab.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Nome do Colaborador é requerido");
                return;
            }

            #endregion

            ///Percorre os itens de campanha de saúde selecionados e efetua a associação do colaborador para cada um deles
            foreach (GridViewRow li in grdCampSaude.Rows)
            {
                if (((CheckBox)li.Cells[0].FindControl("chkSelectCamp")).Checked)
                {
                    TBS340_CAMPSAUDE_COLABOR tbs340 = new TBS340_CAMPSAUDE_COLABOR();

                    tbs340.NM_COLABO_CAMPAN = txtNoColab.Text;
                    tbs340.NM_FUNCA_COLABO_CAMPAN = verificaStrings(txtFuncao.Text);
                    tbs340.CO_IDENT_COLABO_CAMPAN = verificaStrings(txtIdentidade.Text);
                    tbs340.CO_CPF_COLABO_CAMPAN = verificaStrings(PreparaDados(txtCPF.Text));
                    tbs340.NR_TELEF_COLABO_CAMPAN = PreparaDados(txtNrTele.Text);
                    tbs340.NR_CELUL_COLABO_CAMPAN = PreparaDados(txtNrCelu.Text);
                    tbs340.NM_EMAIL_COLABO_CAMPAN = verificaStrings(txtEmail.Text);
                    tbs340.VL_DIARI_COLABO_CAMPAN = (!string.IsNullOrEmpty(txtValDiario.Text) ? decimal.Parse(txtValDiario.Text) : (decimal?)null);
                    tbs340.VL_FINAL_COLABO_CAMPAN = (!string.IsNullOrEmpty(txtValFinal.Text) ? decimal.Parse(txtValFinal.Text) : (decimal?)null);
                    tbs340.CO_IP_REGIS_COLABO_CAMPAN = Request.UserHostAddress;

                    int idCamp = int.Parse((((HiddenField)li.Cells[0].FindControl("hidCoCampan")).Value));
                    tbs340.TBS339_CAMPSAUDE = TBS339_CAMPSAUDE.RetornaPelaChavePrimaria(idCamp);

                    //Salva os dados do responsável 
                    if (ddlColab.SelectedValue != "")
                    {
                        int co = int.Parse(ddlColab.SelectedValue);
                        var col = (from t03 in TB03_COLABOR.RetornaTodosRegistros()
                                   where t03.CO_COL == co
                                   select new { t03.NO_COL, t03.CO_EMP, t03.CO_COL }).FirstOrDefault();

                        tbs340.CO_EMP_COLABO_CAMPAN = col.CO_EMP;
                        tbs340.CO_COL_COLABO_CAMPAN = col.CO_COL;
                    }



                    //Salva essas informações apenas quando for cadastro novo
                    switch (tbs340.EntityState)
                    {
                        case EntityState.Added:
                        case EntityState.Detached:
                            tbs340.DT_CADAS = DateTime.Now;
                            tbs340.CO_COL_CADAS = LoginAuxili.CO_COL;

                            tbs340.DT_SITUA_COLABO_CAMPAN = DateTime.Now;
                            tbs340.CO_SITUA_COLABO_CAMPAN = "A";
                            tbs340.CO_COL_SITUA_COLABO_CAMPAN = LoginAuxili.CO_COL;
                            break;
                        case EntityState.Modified:
                            //Salva as informações de situação apenas quando esta tiver sido alterada.
                            if (ddlSituacao.SelectedValue != tbs340.CO_SITUA_COLABO_CAMPAN)
                            {
                                tbs340.DT_SITUA_COLABO_CAMPAN = DateTime.Now;
                                tbs340.CO_SITUA_COLABO_CAMPAN = "A";
                                tbs340.CO_COL_SITUA_COLABO_CAMPAN = LoginAuxili.CO_COL;
                            }
                            break;
                    }

                    TBS340_CAMPSAUDE_COLABOR.SaveOrUpdate(tbs340, true);
                }
            }
            AuxiliPagina.RedirecionaParaPaginaSucesso("Registro Incluso com sucesso.", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Método responsável por preparar o número retirando os caracteres especiais
        /// </summary>
        /// <param name="tel"></param>
        /// <returns></returns>
        public string PreparaDados(string tel)
        {
            return tel.Replace("(", "").Replace(")", "").Replace("-", "").Replace(".", "").Trim();
        }

        /// <summary>
        /// Método responsável por verificar as strings
        /// </summary>
        /// <param name="stg"></param>
        /// <returns></returns>
        public string verificaStrings(string stg)
        {
            return (!string.IsNullOrEmpty(stg) ? stg : null);
        }

        /// <summary>
        /// Carrega os tipos de campanhas
        /// </summary>
        private void CarregaTipos()
        {
            AuxiliCarregamentos.CarregaTiposCampanhaSaude(ddlTipoCamp, true);
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
        /// Carrega os funcionários
        /// </summary>
        private void CarregaFuncionarios()
        {
            AuxiliCarregamentos.CarregaFuncionarios(ddlColab, LoginAuxili.CO_EMP, false, false);
            ddlColab.Items.Insert(0, new ListItem("Outro", ""));
        }

        /// <summary>
        /// Carrega as possíveis situações
        /// </summary>
        private void CarregaSituacoesColabSaude()
        {
            AuxiliCarregamentos.CarregaSituacaoColaboradorCampanhaSaude(ddlSituacao, false);
        }

        /// <summary>
        /// Carrega a grid de Campanhas de Saúde
        /// </summary>
        private void CarregaGridCampanhasSaude()
        {
            string comp = ddlCompetencia.SelectedValue;
            string classif = ddlClassCamp.SelectedValue;
            string tipo = ddlTipoCamp.SelectedValue;
            string situ = ddlSituCampSaude.SelectedValue;
            DateTime dtini = DateTime.Parse(txtDataIniCamp.Text);
            DateTime dtFim = DateTime.Parse(txtDataFimCamp.Text);
            var res = (from tbs339 in TBS339_CAMPSAUDE.RetornaTodosRegistros()
                       where (comp != "0" ? tbs339.CO_COMPE_TIPO_CAMPAN == comp : 0 == 0)
                       && (classif != "0" ? tbs339.CO_CLASS_CAMPAN == classif : 0 == 0)
                       && (tipo != "0" ? tbs339.CO_TIPO_CAMPAN == tipo : 0 == 0)
                       && (situ != "0" ? tbs339.CO_SITUA_TIPO_CAMPAN == situ : 0 == 0)
                       && ((tbs339.DT_INICI_CAMPAN >= dtini) && (tbs339.DT_INICI_CAMPAN <= dtFim))
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
        /// Carrega as informações da equipe da campanha de saúde 
        /// </summary>
        /// <param name="ID_CAMPAN"></param>
        private void CarregaGridEquipeCampanha(int ID_CAMPAN)
        {
            var res = (from tbs340 in TBS340_CAMPSAUDE_COLABOR.RetornaTodosRegistros()
                       where tbs340.TBS339_CAMPSAUDE.ID_CAMPAN == ID_CAMPAN
                       select new equipeCampanha
                       {
                           noColab = tbs340.NM_COLABO_CAMPAN,
                           CO_COLAB_CAMPAN = tbs340.ID_CAMPSAUDE_COLABOR,
                           noFuncao = tbs340.NM_FUNCA_COLABO_CAMPAN,
                           nuTelef = tbs340.NR_TELEF_COLABO_CAMPAN,
                           CPF = tbs340.CO_CPF_COLABO_CAMPAN,
                           situa = tbs340.CO_SITUA_COLABO_CAMPAN,

                       }).ToList();

            grdEquipCamp.DataSource = res;
            grdEquipCamp.DataBind();
        }

        #region Classe Saída

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
                    switch(this.comp)
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
                    switch(this.classi)
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

        public class equipeCampanha
        {
            public string noColab { get; set; }
            public int CO_COLAB_CAMPAN { get; set; }
            public string noFuncao { get; set; }
            public string nuTelef { get; set; }
            public string nuTelef_Valid
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.nuTelef) ? this.nuTelef.Insert(0, "(").Insert(3, ")").Insert(8, "-") : " - ");
                }
            }
            public string CPF { get; set; }
            public string CPF_Valid
            {
                get
                {
                    return AuxiliFormatoExibicao.preparaCPFCNPJ(this.CPF);
                }
            }
            public string situa { get; set; }
            public string situa_Valid
            {
                get
                {
                    string s = "";
                    switch (this.situa)
                    {
                        case "A":
                            s = "Ativo";
                            break;
                        case "I":
                            s = "Inativo";
                            break;
                        case "S":
                            s = "Suspenso";
                            break;
                        case "C":
                            s = "Cancelado";
                            break;
                    }
                    return s;
                }
            }
        }

        #endregion

        #endregion

        #region Funções de Campo

        protected void imgPesq_OnClick(object sender, EventArgs e)
        {
            //Verifica os campos necessários
            if (string.IsNullOrEmpty(txtDataIniCamp.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a data inicial para pesquisar");
                return;
            }
            if (string.IsNullOrEmpty(txtDataFimCamp.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a data final para pesquisar");
                return;
            }

            CarregaGridCampanhasSaude();
            //updCampanha.Update();
            //updColab.Update();
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

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID != atual.ClientID)
                    {
                        //chk.Checked = false;
                    }
                    else
                    {
                        if (chk.Checked)
                        {
                            int idCampan = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoCampan")).Value);
                            CarregaGridEquipeCampanha(idCampan);
                        }
                        else
                        {
                            grdEquipCamp.DataSource = null;
                            grdEquipCamp.DataBind();
                        }
                        //updColab.Update();
                    }
                }
            }
        }

        protected void ddlColab_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Muda o campo do nome do colaborador de acordo com o selecionado
            if (ddlColab.SelectedValue != "")
            {
                txtNoColab.Text = ddlColab.SelectedItem.Text;
                int coCol = int.Parse(ddlColab.SelectedValue);
                TB03_COLABOR tb03 = TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == coCol).FirstOrDefault();

                txtFuncao.Text = tb03.DE_FUNC_COL;
                txtIdentidade.Text = tb03.CO_RG_COL;
                txtCPF.Text = AuxiliFormatoExibicao.preparaCPFCNPJ(tb03.NU_CPF_COL);
                txtNrTele.Text = tb03.NU_TELE_RESI_COL;
                txtNrCelu.Text = tb03.NU_TELE_CELU_COL;
                txtEmail.Text = tb03.CO_EMAI_COL;
            }
            else
            {
                //Se nenhum colaborador foi selecionado, os campos de informações são zerados
                txtNoColab.Text = txtFuncao.Text = txtIdentidade.Text = txtCPF.Text = txtNrTele.Text = txtNrCelu.Text =
                    txtEmail.Text = "";
            }
        }

        #endregion
    }
}
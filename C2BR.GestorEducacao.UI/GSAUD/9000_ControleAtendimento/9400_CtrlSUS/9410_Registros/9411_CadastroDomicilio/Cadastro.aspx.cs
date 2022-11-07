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
// 17/10/2014| Maxwell Almeida            |  Criação da funcionalidade para registrode Campanhas de Saúde
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
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9400_CtrlSUS._9410_Registros._9411_CadastroDomicilio
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUF();
                CarregaCidades();
                CarregaBairros();
                txtDataCadas.Text = DateTime.Now.ToShortDateString();

                TBS176_ESUS_CADAS_DOMIC tbs176 = RetornaEntidade();
                int codForm = TBS176_ESUS_CADAS_DOMIC.RetornaTodosRegistros().Where(x => x.TB03_COLABOR.CO_COL == LoginAuxili.CO_COL).ToList().Count + 1;
                if (tbs176 != null)
                {
                    txtCodForm.Text = LoginAuxili.CO_MAT_COL + "FCD" + codForm.ToString("00000000000");
                }
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            //Faz as verificações de consistências de dados
            if (String.IsNullOrEmpty(txtNumCartProfi.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor, insira o número do cartão SUS do profissional");
                txtNumCartProfi.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtCnesUnid.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor, insira o código CNES da unidade");
                txtCnesUnid.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtCodEquip.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor, insira o código da equipe");
                txtCodEquip.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtMicroarea.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor, insira o código da microarea");
                txtMicroarea.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtDataCadas.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor, insira a data de cadastro");
                txtDataCadas.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtLogradouro.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor, insira o logradouro");
                txtLogradouro.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtNumLogra.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor, insira o número do logradouro");
                txtNumLogra.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtCEP.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor, insira o número do CEP");
                txtCEP.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtCEP.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor, insira o número do CEP");
                txtCEP.Focus();
                return;
            }
            if (String.IsNullOrEmpty(ddlUFLocal.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor, selecionar uma UF para o cadastro");
                ddlUFLocal.Focus();
                return;
            }
            if (String.IsNullOrEmpty(ddlCidadeLocal.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor, selecionar um Município/Cidade para o cadastro");
                ddlCidadeLocal.Focus();
                return;
            }
            if (String.IsNullOrEmpty(ddlBairroLocal.SelectedValue))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor, selecionar um Bairro para o cadastro");
                ddlBairroLocal.Focus();
                return;
            }
            if (ddlSituMoradia.SelectedValue.Equals("0"))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor, selecionar a situação da moradia para o cadastro");
                ddlSituMoradia.Focus();
                return;
            }
            if (ddlLocalizaMoradia.SelectedValue.Equals("0"))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor, selecionar a localização do domicílio para o cadastro");
                ddlLocalizaMoradia.Focus();
                return;
            }
            if (ddlCondPosseTerra.Enabled && ddlCondPosseTerra.SelectedValue.Equals("0"))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor, selecionar a condição de posse e uso da terra para o cadastro");
                ddlCondPosseTerra.Focus();
                return;
            }

            TBS176_ESUS_CADAS_DOMIC tbs176 = RetornaEntidade();

            tbs176.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
            tbs176.TB74_UF = TB74_UF.RetornaPelaChavePrimaria(ddlUFLocal.SelectedValue);
            tbs176.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairroLocal.SelectedValue));
            tbs176.DT_SITUA = DateTime.Now;
            tbs176.CO_COL_SITUA = LoginAuxili.CO_COL;
            tbs176.CO_EMP_SITUA = LoginAuxili.CO_EMP;
            tbs176.CO_CADAS_DOMIC = txtCodForm.Text;
            tbs176.NU_CART_SUS_PROFI = Decimal.Parse(txtNumCartProfi.Text);
            tbs176.CO_CNES_UNID = Decimal.Parse(txtCnesUnid.Text);
            tbs176.CO_EQUIP_INE = Decimal.Parse(txtCodEquip.Text);
            tbs176.CO_MICROAREA = txtMicroarea.Text;
            tbs176.DT_CADAS_DOMIC = DateTime.Parse(txtDataCadas.Text);
            tbs176.TP_LOGRADOURO = txtTipoLogra.Text;
            tbs176.NO_LOGRADOURO = txtLogradouro.Text;
            tbs176.NU_LOGRADOURO = Decimal.Parse(txtNumLogra.Text);
            tbs176.NU_CEP = txtCEP.Text.Replace("-", "");
            tbs176.DE_COMPLEMENTO = txtComplemento.Text;
            tbs176.TP_DOMIC = int.Parse(ddlTipoMoradia.SelectedValue);
            tbs176.NU_TEL_RESID = PreparaTelefones(txtTelResid.Text);
            tbs176.NU_TEL_REFER = PreparaTelefones(txtTelRefer.Text);
            tbs176.CO_SITUA_DOMIC = int.Parse(ddlSituMoradia.SelectedValue);
            tbs176.CO_LOCALIZACAO = int.Parse(ddlLocalizaMoradia.SelectedValue);
            tbs176.CO_CONDI_DOMIC = int.Parse(ddlCondPosseTerra.SelectedValue);
            tbs176.QT_MORAD_DOMIC = int.Parse(txtNumMoradores.Text);
            tbs176.NU_COMOD_DOMIC = int.Parse(txtNumComodos.Text);
            tbs176.FL_ENERG_ELETR = chkEnergiaEletrica.Checked ? "S" : "N";
            tbs176.TP_ACESS_DOMIC = int.Parse(ddlAcessoMoradia.SelectedValue);
            tbs176.CO_MATER_PARED_EXTER = int.Parse(ddlParedeExterna.SelectedValue);
            tbs176.CO_ABAST_AGUA = int.Parse(ddlAbastAgua.SelectedValue);
            tbs176.CO_TRATA_AGUA = int.Parse(ddlTratAgua.SelectedValue);
            tbs176.CO_ESCOA_ESGOT = int.Parse(ddlEscoaSanitario.SelectedValue);
            tbs176.CO_DESTI_LIXO = int.Parse(ddlDestinoLixo.SelectedValue);
            tbs176.FL_ANIMAL = chkAnimais.Checked ? "S" : "N";
            tbs176.FL_GATO = chkGato.Checked ? "S" : "N";
            tbs176.FL_CACHOR = chkCachorro.Checked ? "S" : "N";
            tbs176.FL_PASSA = chkPassaro.Checked ? "S" : "N";
            tbs176.FL_CRIAC = chkCriacao.Checked ? "S" : "N";
            tbs176.FL_OUTRO = chkOutros.Checked ? "S" : "N";
            tbs176.QT_ANIMAL = String.IsNullOrEmpty(txtQntAnimais.Text) ? 0 : int.Parse(txtQntAnimais.Text);

            TBS176_ESUS_CADAS_DOMIC.SaveOrUpdate(tbs176, true);
                        
            if (grdFamilias.Rows.Count != 0)
            {
                foreach (GridViewRow li in grdFamilias.Rows)
                {
                    HiddenField hidCoFamilia = (HiddenField)li.FindControl("hidCoFamilia");
                    TextBox txtNumProntFamili = (TextBox)li.FindControl("txtNumProntFamili");
                    TextBox txtNumCartaoSUSRespo = (TextBox)li.FindControl("txtNumCartaoSUSRespo");
                    TextBox txtDataNasciRespo = (TextBox)li.FindControl("txtDataNasciRespo");
                    DropDownList ddlRendaFamili = (DropDownList)li.FindControl("ddlRendaFamili");
                    TextBox txtMembrosFamili = (TextBox)li.FindControl("txtMembrosFamili");
                    TextBox txtResideDesde = (TextBox)li.FindControl("txtResideDesde");
                    CheckBox chkMudou = (CheckBox)li.FindControl("chkMudou");

                    TBS177_ESUS_DOMIC_FAMILIA tbs177;
                    if (String.IsNullOrEmpty(hidCoFamilia.Value))
                        tbs177 = new TBS177_ESUS_DOMIC_FAMILIA();
                    else
                        tbs177 = TBS177_ESUS_DOMIC_FAMILIA.RetornaPelaChavePrimaria(int.Parse(hidCoFamilia.Value));

                    tbs177.TBS176_ESUS_CADAS_DOMIC = tbs176;
                    tbs177.NU_PRONT_FAMIL = Decimal.Parse(txtNumProntFamili.Text);
                    tbs177.NU_CART_SUS_RESPO = Decimal.Parse(txtNumCartaoSUSRespo.Text);
                    tbs177.DT_NASCI_RESPO = DateTime.Parse(txtDataNasciRespo.Text);
                    tbs177.CO_RENDA_FAMIL = int.Parse(ddlRendaFamili.SelectedValue);
                    tbs177.QT_MEMBR_FAMIL = int.Parse(txtMembrosFamili.Text);
                    tbs177.DT_RESID_DESDE = txtResideDesde.Text;
                    tbs177.FL_MUDOU = chkMudou.Checked ? "S" : "N";

                    TBS177_ESUS_DOMIC_FAMILIA.SaveOrUpdate(tbs177, true);
                }
            }

            CurrentPadraoCadastros.CurrentEntity = tbs176;
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Carrega as informações
        /// </summary>
        private void CarregaFormulario()
        {
            var tbs176 = RetornaEntidade();
            tbs176.TB03_COLABORReference.Load();
            tbs176.TB74_UFReference.Load();
            tbs176.TB905_BAIRROReference.Load();

            txtCodForm.Text = tbs176.CO_CADAS_DOMIC;
            txtNumCartProfi.Text = tbs176.NU_CART_SUS_PROFI.ToString();
            txtCnesUnid.Text = tbs176.CO_CNES_UNID.ToString();
            txtCodEquip.Text = tbs176.CO_EQUIP_INE.ToString();
            txtMicroarea.Text = tbs176.CO_MICROAREA;
            txtDataCadas.Text = tbs176.DT_CADAS_DOMIC.ToShortDateString();
            txtTipoLogra.Text = tbs176.TP_LOGRADOURO;
            txtLogradouro.Text = tbs176.NO_LOGRADOURO;
            txtNumLogra.Text = tbs176.NU_LOGRADOURO.ToString();
            txtCEP.Text = tbs176.NU_CEP;
            txtComplemento.Text = tbs176.DE_COMPLEMENTO;
            ddlTipoMoradia.SelectedValue = tbs176.TP_DOMIC.ToString();
            txtTelResid.Text = tbs176.NU_TEL_RESID;
            txtTelRefer.Text = tbs176.NU_TEL_REFER;
            ddlSituMoradia.SelectedValue = tbs176.CO_SITUA_DOMIC.ToString();
            ddlLocalizaMoradia.SelectedValue = tbs176.CO_LOCALIZACAO.ToString();
            ddlCondPosseTerra.SelectedValue = tbs176.CO_CONDI_DOMIC.ToString();
            txtNumMoradores.Text = tbs176.QT_MORAD_DOMIC.ToString();
            txtNumComodos.Text = tbs176.NU_COMOD_DOMIC.ToString();
            chkEnergiaEletrica.Checked = tbs176.FL_ENERG_ELETR.Equals("S") ? true : false;
            ddlAcessoMoradia.SelectedValue = tbs176.TP_ACESS_DOMIC.ToString();
            ddlParedeExterna.SelectedValue = tbs176.CO_MATER_PARED_EXTER.ToString();
            ddlAbastAgua.SelectedValue = tbs176.CO_ABAST_AGUA.ToString();
            ddlTratAgua.SelectedValue = tbs176.CO_TRATA_AGUA.ToString();
            ddlEscoaSanitario.SelectedValue = tbs176.CO_ESCOA_ESGOT.ToString();
            ddlDestinoLixo.SelectedValue = tbs176.CO_DESTI_LIXO.ToString();
            chkAnimais.Checked = tbs176.FL_ANIMAL.Equals("S") ? true : false;
            chkGato.Checked = tbs176.FL_GATO.Equals("S") ? true : false;
            chkCachorro.Checked = tbs176.FL_CACHOR.Equals("S") ? true : false;
            chkPassaro.Checked = tbs176.FL_PASSA.Equals("S") ? true : false;
            chkCriacao.Checked = tbs176.FL_CRIAC.Equals("S") ? true : false;
            chkOutros.Checked = tbs176.FL_OUTRO.Equals("S") ? true : false;
            txtQntAnimais.Text = tbs176.QT_ANIMAL.ToString();

            DataTable dt = new DataTable();
            CarregarGridFamilia(dt ,tbs176.ID_CADAS_DOMIC);
        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TBS339_CAMPSAUDE</returns>
        private TBS176_ESUS_CADAS_DOMIC RetornaEntidade()
        {
            TBS176_ESUS_CADAS_DOMIC tbs176 = TBS176_ESUS_CADAS_DOMIC.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tbs176 == null) ? new TBS176_ESUS_CADAS_DOMIC() : tbs176;
        }

        /// <summary>
        /// Método responsável por preparar o número retirando os caracteres especiais
        /// </summary>
        /// <param name="tel"></param>
        /// <returns></returns>
        public string PreparaTelefones(string tel)
        {
            return tel.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
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
        /// Carrega as UF's
        /// </summary>
        private void CarregaUF()
        {
            AuxiliCarregamentos.CarregaUFs(ddlUFLocal, false, LoginAuxili.CO_EMP);
            ddlUFLocal.Items.Insert(0, new ListItem("", ""));
            CarregaCidades();
        }

        /// <summary>
        /// Carrega as Cidades
        /// </summary>
        private void CarregaCidades()
        {
            AuxiliCarregamentos.CarregaCidades(ddlCidadeLocal, false, ddlUFLocal.SelectedValue, LoginAuxili.CO_EMP, false, false);
            ddlCidadeLocal.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Carrega os Bairros
        /// </summary>
        private void CarregaBairros()
        {
            int cida = (!string.IsNullOrEmpty(ddlCidadeLocal.SelectedValue) ? int.Parse(ddlCidadeLocal.SelectedValue) : 0);
            AuxiliCarregamentos.CarregaBairros(ddlBairroLocal, ddlUFLocal.SelectedValue, cida, false, false);
            ddlBairroLocal.Items.Insert(0, new ListItem("", ""));
        }

        #endregion

        #region Funções de Campo

        protected void chkAnimais_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            if (chk.Checked)
            {
                chkGato.Enabled = true;
                chkCachorro.Enabled = true;
                chkPassaro.Enabled = true;
                chkCriacao.Enabled = true;
                chkOutros.Enabled = true;
                txtQntAnimais.Enabled = true;
            }
            else
            {
                chkGato.Enabled = false;
                chkCachorro.Enabled = false;
                chkPassaro.Enabled = false;
                chkCriacao.Enabled = false;
                chkOutros.Enabled = false;
                txtQntAnimais.Enabled = false;
            }
            upAnimais.Update();
        }

        protected void ddlLocalizaMoradia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;

            if (ddl.SelectedValue.Equals("2"))
            {
                ddlCondPosseTerra.Enabled = true;
            }
            else
            {
                ddlCondPosseTerra.Enabled = false;
            }
            upCondPosseTerra.Update();
        }

        protected void ddlUFLocal_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades();
        }

        protected void ddlCidadeLocal_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairros();
        }

        #endregion

        #region Grid da família

        private void CarregarGridFamilia(DataTable dt, int cadasDomic = 0)
        {
            if (cadasDomic != 0)
            {
                var res = (from tbs177 in TBS177_ESUS_DOMIC_FAMILIA.RetornaTodosRegistros()
                           where tbs177.TBS176_ESUS_CADAS_DOMIC.ID_CADAS_DOMIC == cadasDomic
                           select new
                           {
                               tbs177.ID_DOMIC_FAMIL,
                               tbs177.NU_PRONT_FAMIL,
                               tbs177.NU_CART_SUS_RESPO,
                               tbs177.DT_NASCI_RESPO,
                               tbs177.CO_RENDA_FAMIL,
                               tbs177.QT_MEMBR_FAMIL,
                               tbs177.DT_RESID_DESDE,
                               tbs177.FL_MUDOU
                           }).ToList();

                foreach (var i in res)
                {
                    var linha = dt.NewRow();
                    linha["ID"] = i.ID_DOMIC_FAMIL;
                    linha["PRONTUARIO"] = i.NU_PRONT_FAMIL;
                    linha["CARTAOSUS"] = i.NU_CART_SUS_RESPO;
                    linha["DATANASCIMENTO"] = i.DT_NASCI_RESPO.Value.ToShortDateString();
                    linha["RENDA"] = i.CO_RENDA_FAMIL;
                    linha["NMEMBROS"] = i.QT_MEMBR_FAMIL;
                    linha["RESIDE"] = i.DT_RESID_DESDE;
                    linha["MUDOU"] = i.FL_MUDOU;
                    dt.Rows.Add(linha);
                }
            }

            grdFamilias.DataSource = dt;
            grdFamilias.DataBind();

            int aux = 0;
            foreach (GridViewRow li in grdFamilias.Rows)
            {
                var hidCoFamilia = (HiddenField)li.FindControl("hidCoFamilia");
                var ddlRendaFamili = (DropDownList)li.FindControl("ddlRendaFamili");
                var txtNumProntFamili = (TextBox)li.FindControl("txtNumProntFamili");
                var txtNumCartaoSUSRespo = (TextBox)li.FindControl("txtNumCartaoSUSRespo");
                var txtDataNasciRespo = (TextBox)li.FindControl("txtDataNasciRespo");
                var txtMembrosFamili = (TextBox)li.FindControl("txtMembrosFamili");
                var txtResideDesde = (TextBox)li.FindControl("txtResideDesde");
                var chkMudou = (CheckBox)li.FindControl("chkMudou");

                string id, renda, prontuario, cartaoSUS, dataNasci, membros, reside, mudou;

                //Coleta os valores do dtv da modal popup
                id = dt.Rows[aux]["ID"].ToString();
                renda = dt.Rows[aux]["RENDA"].ToString();
                prontuario = dt.Rows[aux]["PRONTUARIO"].ToString();
                cartaoSUS = dt.Rows[aux]["CARTAOSUS"].ToString();
                dataNasci = dt.Rows[aux]["DATANASCIMENTO"].ToString();
                membros = dt.Rows[aux]["NMEMBROS"].ToString();
                reside = dt.Rows[aux]["RESIDE"].ToString();
                mudou = dt.Rows[aux]["MUDOU"].ToString();

                //Seta os valores nos campos da modal popup
                hidCoFamilia.Value = id;
                ddlRendaFamili.SelectedValue = renda;
                txtNumProntFamili.Text = prontuario;
                txtNumCartaoSUSRespo.Text = cartaoSUS;
                txtDataNasciRespo.Text = dataNasci;
                txtMembrosFamili.Text = membros;
                txtResideDesde.Text = reside;
                chkMudou.Checked = mudou.Equals("S") ? true : false;
                aux++;
            }
        }

        private DataTable CriarGridFamilia()
        {
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            dt.Columns.Add(CriarColuna("ID"));
            dt.Columns.Add(CriarColuna("PRONTUARIO"));
            dt.Columns.Add(CriarColuna("CARTAOSUS"));
            dt.Columns.Add(CriarColuna("DATANASCIMENTO"));
            dt.Columns.Add(CriarColuna("RENDA"));
            dt.Columns.Add(CriarColuna("NMEMBROS"));
            dt.Columns.Add(CriarColuna("RESIDE"));
            dt.Columns.Add(CriarColuna("MUDOU"));

            foreach (GridViewRow li in grdFamilias.Rows)
            {
                var linha = dt.NewRow();
                linha["ID"] = (HiddenField)li.FindControl("hidCoFamilia");
                linha["RENDA"] = ((DropDownList)li.FindControl("ddlRendaFamili")).SelectedValue;
                linha["PRONTUARIO"] = ((TextBox)li.FindControl("txtNumProntFamili")).Text;
                linha["CARTAOSUS"] = ((TextBox)li.FindControl("txtNumCartaoSUSRespo")).Text;
                linha["DATANASCIMENTO"] = ((TextBox)li.FindControl("txtDataNasciRespo")).Text;
                linha["NMEMBROS"] = ((TextBox)li.FindControl("txtMembrosFamili")).Text;
                linha["RESIDE"] = ((TextBox)li.FindControl("txtResideDesde")).Text;
                linha["MUDOU"] = ((CheckBox)li.FindControl("chkMudou")).Checked;
                dt.Rows.Add(linha);
            }

            return dt;
        }

        private static DataColumn CriarColuna(String nome)
        {
            DataColumn dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = nome;
            return dc;
        }

        protected void lnkAddFamilia_OnClick(object sender, EventArgs e)
        {
            DataTable dt = CriarGridFamilia();

            DataRow linha = dt.NewRow();
            linha["ID"] = "";
            linha["RENDA"] = "";
            linha["PRONTUARIO"] = "";
            linha["CARTAOSUS"] = "";
            linha["DATANASCIMENTO"] = "";
            linha["NMEMBROS"] = "";
            linha["RESIDE"] = "";
            linha["MUDOU"] = "";
            dt.Rows.Add(linha);

            CarregarGridFamilia(dt);
        }

        protected void imgExcFamilia_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            int aux = -1;
            if (grdFamilias.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdFamilias.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgExcFamilia");
                    HiddenField hid = (HiddenField)linha.FindControl("hidCoFamilia");

                    if (img.ClientID == atual.ClientID)
                    {
                        aux = linha.RowIndex;
                        if (String.IsNullOrEmpty(hid.Value))
                        {
                            TBS177_ESUS_DOMIC_FAMILIA.DeletePorID(int.Parse(hid.Value));
                        }
                    }
                }

                if (aux != -1)
                {
                    DataTable dt = CriarGridFamilia();
                    dt.Rows.RemoveAt(aux);

                    CarregarGridFamilia(dt);
                }
            }
        }

        #endregion
    }
}
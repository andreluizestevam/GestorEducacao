//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PGS - Portal Gestor Saúde
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: --
// SUBMÓDULO: --
// OBJETIVO: --
// DATA DE CRIAÇÃO: 15/08/2016
//--------------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//--------------------------------------------------------------------------------
//  DATA       |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ------------+----------------------------+-------------------------------------
//  15/08/2016 | Tayguara Acioli            | Criei funcionalidade

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Resources;
using C2BR.GestorEducacao.UI.Library;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.Library.Componentes;
using System.Data.Objects;
using C2BR.GestorEducacao.UI.App_Masters;
using System.IO;

namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9302_ControleExames
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

        protected void Page_Init(object sender, EventArgs e)
        {
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaConsultasAgendadas(sender,e);
                CarregaContratacao();
                CarregaFuncionario();
                txtDtHrCadas.Text = DateTime.Today.ToString();
                txtHora.Text = DateTime.Now.ToString("HH:mm");
            }
        }

        /// <summary>
        /// Faz o Salvamento das informações do Pré-Atendimento na TBS194
        /// </summary>
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            foreach (GridViewRow l in grdAgendamentos.Rows)
            {
                CheckBox chkExame = (CheckBox)l.FindControl("chkselectEn");

                if (chkExame.Checked)
                {
                    var hidIdExameResul = (HiddenField)l.FindControl("hidIdExameResul");
                    var hidProced = (HiddenField)l.FindControl("hidProced");
                    var hidCoAlu = (HiddenField)l.FindControl("hidCoAlu");
                    var numRegistro = l.Cells[3].Text;
                    var dtSolic = l.Cells[1].Text;

                    TBS416_EXAME_RESUL tbs416 = new TBS416_EXAME_RESUL();

                    if (int.Parse(hidIdExameResul.Value) != 0)
                    {
                        tbs416 = TBS416_EXAME_RESUL.RetornaPelaChavePrimaria(int.Parse(hidIdExameResul.Value));
                    }

                    tbs416.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(hidProced.Value));
                    tbs416.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(hidCoAlu.Value));
                    tbs416.NU_REGISTRO = numRegistro;
                    tbs416.NU_REGIS_EXTER = null;
                    tbs416.DT_SOLIC = DateTime.Parse(dtSolic);
                    tbs416.FL_FUNCI = chkFuncCadas.Checked ? "S" : "N";
                    tbs416.TB03_COLABOR = chkFuncCadas.Checked ? TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlFunciCadas.SelectedValue)) : null;
                    tbs416.NO_AVALI_CADAS = chkFuncCadas.Checked ? ddlFunciCadas.SelectedItem.Text : txtFunciCadas.Text;
                    tbs416.DT_CADAS = DateTime.Parse((!String.IsNullOrEmpty(txtDtHrCadas.Text) ? txtDtHrCadas.Text : DateTime.Today.ToString()) + " " + (!String.IsNullOrEmpty(txtHora.Text) ? txtHora.Text : "00:00") + ":00");
                    tbs416.IP_CADAS = Request.UserHostAddress;
                    tbs416.DE_OBSER = txtObservacao.Text;

                    TBS416_EXAME_RESUL.SaveOrUpdate(tbs416, true);

                    foreach (GridViewRow x in grdItensAvali.Rows)
                    {
                        CheckBox chkItem = (CheckBox)x.FindControl("chkItem");

                        if (chkItem.Checked)
                        {
                            HiddenField IdExameResulItens = (HiddenField)x.FindControl("hidTbs417");
                            HiddenField hidIdItensAvali = (HiddenField)x.FindControl("hidIdItensAvali");
                            TextBox txtDtHr = (TextBox)x.FindControl("txtDtHr");
                            TextBox txtResultado = (TextBox)x.FindControl("txtResultado");
                            CheckBox flFunc = (CheckBox)x.FindControl("flagRow");
                            TextBox txtRespTec = (TextBox)x.FindControl("txtRespTecnico");
                            DropDownList ddlRespTec = (DropDownList)x.FindControl("ddlRespTecnico");
                            TextBox txtSigla = (TextBox)x.FindControl("txtSigla");
                            TextBox txtNumero = (TextBox)x.FindControl("txtNumero");
                            DropDownList ddlUF = (DropDownList)x.FindControl("ddlUF");
                            DropDownList drpTpResult = (DropDownList)x.FindControl("drpTpResult");

                            TBS417_EXAME_RESUL_ITENS tbs417 = new TBS417_EXAME_RESUL_ITENS();

                            if (int.Parse(IdExameResulItens.Value) != 0)
                            {
                                tbs417 = TBS417_EXAME_RESUL_ITENS.RetornaPelaChavePrimaria(int.Parse(IdExameResulItens.Value));
                            }

                            tbs417.TBS356_PROC_MEDIC_PROCE = tbs416.TBS356_PROC_MEDIC_PROCE;
                            tbs417.TBS416_EXAME_RESUL = tbs416;
                            tbs417.TBS414_EXAME_ITENS_AVALI = TBS414_EXAME_ITENS_AVALI.RetornaPelaChavePrimaria(int.Parse(hidIdItensAvali.Value));
                            tbs417.NO_ITENS_REFER = tbs417.TBS414_EXAME_ITENS_AVALI.NO_ITEM_AVALI;
                            tbs417.DT_RESUL_ITENS = !String.IsNullOrEmpty(txtDtHr.Text) ? DateTime.Parse(txtDtHr.Text) : DateTime.Now;
                            tbs417.VL_RESUL_ITENS = txtResultado.Text;
                            tbs417.CO_SITU_ITEM_EXAME = drpTpResult.SelectedValue;
                            tbs417.FL_FUNCI = flFunc.Checked ? "S" : "N";
                            tbs417.TB03_COLABOR = flFunc.Checked ? TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlRespTec.SelectedValue)) : null;
                            tbs417.NO_AVALI = flFunc.Checked ? ddlRespTec.SelectedItem.Text : txtRespTec.Text;
                            tbs417.CO_SIGLA_ENTID_AVALI = txtSigla.Text;
                            tbs417.NU_ENTID_AVALI = txtNumero.Text;
                            tbs417.CO_ENTID_UF_AVALI = ddlUF.SelectedValue;

                            TBS417_EXAME_RESUL_ITENS.SaveOrUpdate(tbs417, true);
                        }
                    }

                    break;
                }
            }

            AuxiliPagina.EnvioMensagemSucesso(this.Page,"Registro cadastrado com Sucesso!");
        }
        #endregion

        #region Carregamentos

        /// <summary>
        /// Carrega a Grid de Registros de Consultas em aberto para o dia
        /// </summary>
        public void CarregaConsultasAgendadas(object sender, EventArgs e)
        {
            int contratacao = !String.IsNullOrEmpty(ddlContratacao.SelectedValue) ? int.Parse(ddlContratacao.SelectedValue) : 0;

            var res = (from tbs398 in TBS398_ATEND_EXAMES.RetornaTodosRegistros()
                       join tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros() on tbs398.TBS390_ATEND_AGEND.ID_ATEND_AGEND equals tbs390.ID_ATEND_AGEND
                       join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tbs390.TBS174_AGEND_HORAR.ID_AGEND_HORAR equals tbs174.ID_AGEND_HORAR
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs390.CO_EMP_ATEND equals tb03.CO_EMP
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs390.TB07_ALUNO.CO_ALU equals tb07.CO_ALU
                       join tbs416 in TBS416_EXAME_RESUL.RetornaTodosRegistros() on tbs390.NU_REGIS equals tbs416.NU_REGISTRO into a
                       from tbs416 in a.DefaultIfEmpty()
                       where tbs390.CO_COL_ATEND == tb03.CO_COL
                        && (contratacao != 0 ? tbs174.TB250_OPERA.ID_OPER == contratacao : tbs174.TB250_OPERA.ID_OPER == tbs174.TB250_OPERA.ID_OPER)
                       select new Exames
                       {
                           DataHora = tbs398.DT_CADAS,
                           Exame = tbs398.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                           NuRegistro = tbs390.NU_REGIS,
                           Tipo = "EI",
                           Contratante = tbs174.TB250_OPERA.NOM_OPER,
                           Solicitante = tb03.NO_APEL_COL,
                           Paciente = tb07.NO_APE_ALU,
                           Crm = tb03.NO_APEL_COL,
                           NuCrm = tb03.NU_ENTID_PROFI,
                           Situacao = tbs398.CO_SITUA,
                           IdProced = tbs398.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                           IdExameResul = tbs416.ID_EXAME_RESUL != null ? tbs416.ID_EXAME_RESUL : 0,
                           CoAlu = tb07.CO_ALU,
                           CoAluEmp = tb07.CO_EMP
                       }).ToList();

            var res_ = (from tbs411 in TBS411_EXAMES_ESTERNOS.RetornaTodosRegistros()
                        join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs411.CO_ALU equals tb07.CO_ALU
                        join tbs416 in TBS416_EXAME_RESUL.RetornaTodosRegistros() on tbs411.NU_REGISTRO equals tbs416.NU_REGISTRO into a
                        from tbs416 in a.DefaultIfEmpty()
                        where (contratacao != 0 ? tbs411.TB250_OPERA.ID_OPER == contratacao : true)
                        select new Exames
                        {
                            DataHora = tbs411.DT_CADAS,
                            Exame = tbs411.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                            NuRegistro = tbs411.NU_REGISTRO,
                            Tipo = "EE",
                            Contratante = tbs411.TB250_OPERA.NOM_OPER,
                            Solicitante = tbs411.NO_SOLICITANTE,
                            Paciente = tb07.NO_APE_ALU,
                            Crm = tbs411.CO_SIGLA_ENTID_SOLIC,
                            NuCrm = tbs411.NU_ENTID_SOLIC,
                            Situacao = tbs411.CO_SITUA,
                            IdProced = tbs411.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                            IdExameResul = tbs416.ID_EXAME_RESUL != null ? tbs416.ID_EXAME_RESUL : 0,
                            CoAlu = tbs411.CO_ALU,
                            CoAluEmp = tb07.CO_EMP
                        }).ToList();

            res = res.Union(res_).ToList();

            grdAgendamentos.DataSource = res;
            grdAgendamentos.DataBind();
        }

        protected void lnkbAnexos_OnClick(object sender, EventArgs e)
        {
            foreach (GridViewRow l in grdAgendamentos.Rows)
            {
                CheckBox chkExame = (CheckBox)l.FindControl("chkselectEn");

                if (chkExame.Checked)
                {
                    var hidIdExameResul = (HiddenField)l.FindControl("hidIdExameResul");


                    if (string.IsNullOrEmpty(hidIdExameResul.Value))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um exame!");
                        grdAgendamentos.Focus();
                        return;
                    }

                    var hidCoAlu = (HiddenField)l.FindControl("hidCoAlu");


                    var btn = (LinkButton)sender;

                    hidTpAnexo.Value = btn.AccessKey;

                    switch (btn.AccessKey)
                    {
                        case "F":
                            lblTpAnexo.Text = "FOTO";
                            break;
                        case "V":
                            lblTpAnexo.Text = "VIDEO";
                            break;
                        case "U":
                            lblTpAnexo.Text = "ÁUDIO";
                            break;
                        case "A":
                            lblTpAnexo.Text = "ANEXO";
                            break;
                    }

                    var co_alu = int.Parse(hidCoAlu.Value);
                    CarregarAnexosAssociados(co_alu, btn.AccessKey);

                    hidIdExameAnexo.Value = hidIdExameResul.Value;
                    hidIdCoAluAnexo.Value = hidCoAlu.Value;

                    AbreModalPadrao("AbreModalAnexos();");
                }
            }
        }

        protected void lnkbAnexar_OnClick(object sender, EventArgs e)
        {
            if (!flupAnexo.HasFile)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Favor selecionar o arquivo");
                AbreModalPadrao("AbreModalAnexos();");
                return;
            }

            if (string.IsNullOrEmpty(hidIdExameAnexo.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor salvar o resultado do exame para anexar o arquivo");
                return;
            }

            try
            {
                var tbs392 = new TBS392_ANEXO_ATEND();
                tbs392.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(hidIdCoAluAnexo.Value));
                tbs392.NM_TITULO = txtNomeAnexo.Text;
                tbs392.TP_ANEXO = hidTpAnexo.Value;
                tbs392.DE_OBSER = txtObservAnexo.Text;

                Stream fs = flupAnexo.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                tbs392.ANEXO = bytes;
                tbs392.NM_ANEXO = flupAnexo.PostedFile.FileName;
                tbs392.EX_ANEXO = Path.GetExtension(flupAnexo.FileName);
                tbs392.NU_CLEN_ANEXO = flupAnexo.PostedFile.ContentLength;
                tbs392.DE_CTIP_ANEXO = flupAnexo.PostedFile.ContentType;

                //Dados do cadastro e da situação
                tbs392.CO_SITUA = "A";
                tbs392.DT_CADAS = tbs392.DT_SITUA = DateTime.Now;
                tbs392.CO_COL_CADAS = tbs392.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs392.CO_EMP_COL_CADAS = tbs392.CO_EMP_COL_SITUA = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;
                tbs392.CO_EMP_CADAS = tbs392.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs392.IP_CADAS = tbs392.IP_SITUA = Request.UserHostAddress;
                tbs392.TBS416_EXAME_RESUL = TBS416_EXAME_RESUL.RetornaPelaChavePrimaria(int.Parse(hidIdExameAnexo.Value));

                TBS392_ANEXO_ATEND.SaveOrUpdate(tbs392, true);

                AuxiliPagina.EnvioMensagemSucesso(this.Page, "Arquivo anexado com sucesso!");
            }
            catch (Exception erro)
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Ocorreu um erro ao salvar o arquivo. Erro:" + erro.Message);
                AbreModalPadrao("AbreModalAnexos();");
                return;
            }
        }

        protected void imgbBxrAnexo_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;

            foreach (GridViewRow li in grdAnexos.Rows)
            {
                img = (ImageButton)li.FindControl("imgbBxrAnexo");

                if (img.ClientID == atual.ClientID)
                {
                    var idAnexo = int.Parse(((HiddenField)li.FindControl("hidIdAnexo")).Value);

                    var tbs392 = TBS392_ANEXO_ATEND.RetornaPelaChavePrimaria(idAnexo);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = tbs392.DE_CTIP_ANEXO;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + tbs392.NM_ANEXO);
                    Response.BinaryWrite(tbs392.ANEXO);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void imgbExcAnexo_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;

            foreach (GridViewRow li in grdAnexos.Rows)
            {
                img = (ImageButton)li.FindControl("imgbExcAnexo");

                if (img.ClientID == atual.ClientID)
                {
                    var idAnexo = int.Parse(((HiddenField)li.FindControl("hidIdAnexo")).Value);

                    var tbs392 = TBS392_ANEXO_ATEND.RetornaPelaChavePrimaria(idAnexo);

                    TBS392_ANEXO_ATEND.Delete(tbs392, true);

                    AuxiliPagina.EnvioMensagemSucesso(this.Page, "Arquivo EXCLUIDO com sucesso!");
                }
            }
        }

        private void AbreModalPadrao(string funcao)
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                funcao,
                true
            );
        }

        public void CarregarAnexosAssociados(Int32 coAlu, String tpAnexo)
        {
            var res = (from tbs392 in TBS392_ANEXO_ATEND.RetornaTodosRegistros()
                       where tbs392.TB07_ALUNO.CO_ALU == coAlu && tbs392.TP_ANEXO == tpAnexo
                       select new
                       {
                           tbs392.ID_ANEXO_ATEND,
                           tbs392.NM_TITULO,
                           tbs392.DE_OBSER,
                           DE_OBSER_RES = tbs392.DE_OBSER.Length > 77 ? tbs392.DE_OBSER.Substring(0, 77) + "..." : tbs392.DE_OBSER,
                           tbs392.DT_CADAS,
                           tbs392.TBS390_ATEND_AGEND.NU_REGIS
                       }).ToList();

            grdAnexos.DataSource = res;
            grdAnexos.DataBind();
        }

        public void CarregaContratacao()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlContratacao, true);
        }

        public void CarregaTipoResultado(DropDownList drp, String selecionado)
        {
            AuxiliCarregamentos.CarregaTiposResultado(drp, false, false);

            if (!String.IsNullOrEmpty(selecionado) && drp.Items.FindByValue(selecionado) != null)
                drp.SelectedValue = selecionado;
        }

        public void CarregaResponsavelTecnico()
        {

            var resp = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                        where tb03.FL_RESP_EXAME == "S"
                        select new
                        {
                            tb03.CO_COL,
                            tb03.NO_APEL_COL,
                            tb03.CO_SIGLA_ENTID_PROFI,
                            tb03.NU_ENTID_PROFI,
                            tb03.CO_UF_ENTID_PROFI
                        }).ToList();

            foreach (GridViewRow l in grdItensAvali.Rows)
            {
                DropDownList ddlFunc = (DropDownList)l.FindControl("ddlRespTecnico");
                DropDownList ddlUf = (DropDownList)l.FindControl("ddlUF");
                CheckBox chkFl = (CheckBox)l.FindControl("flagRow");
                TextBox txtSigla = (TextBox)l.FindControl("txtSigla");
                TextBox txtNumero = (TextBox)l.FindControl("txtNumero");

                ddlFunc.DataTextField = "NO_APEL_COL";
                ddlFunc.DataValueField = "CO_COL";
                ddlFunc.DataSource = resp;
                ddlFunc.DataBind();

                if (chkFl.Checked)
                {
                    ddlUf.SelectedValue = resp.ElementAt(ddlFunc.SelectedIndex).CO_UF_ENTID_PROFI;
                    txtSigla.Text = resp.ElementAt(ddlFunc.SelectedIndex).CO_SIGLA_ENTID_PROFI;
                    txtNumero.Text = resp.ElementAt(ddlFunc.SelectedIndex).NU_ENTID_PROFI;
                }

            }
        }

        public void ddlRespTecnico_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var ddlAtual = (DropDownList)sender;

            foreach (GridViewRow l in grdItensAvali.Rows)
            {
                DropDownList ddl = (DropDownList)l.FindControl("ddlRespTecnico");

                if (ddlAtual.ClientID == ddl.ClientID)
                {
                    var tb03 = TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlAtual.SelectedValue));

                    DropDownList ddlUf = (DropDownList)l.FindControl("ddlUF");
                    TextBox txtSigla = (TextBox)l.FindControl("txtSigla");
                    TextBox txtNumero = (TextBox)l.FindControl("txtNumero");

                    ddlUf.SelectedValue = tb03.CO_UF_ENTID_PROFI;
                    txtSigla.Text = tb03.CO_SIGLA_ENTID_PROFI;
                    txtNumero.Text = tb03.NU_ENTID_PROFI;
                }
            }
        }

        public void chkselectEn_OnCheckedChanged(object sender, EventArgs e)
        {
            var chkAtual = (CheckBox)sender;

            foreach (GridViewRow l in grdAgendamentos.Rows)
            {
                CheckBox chk = (CheckBox)l.FindControl("chkselectEn");

                if (chkAtual.ClientID == chk.ClientID)
                {
                    if (chk.Checked)
                    {
                        var hidProced = (HiddenField)l.FindControl("hidProced");
                        var hidIdExameResul = (HiddenField)l.FindControl("hidIdExameResul");

                        CarregaGridItensAvali(int.Parse(hidProced.Value), int.Parse(hidIdExameResul.Value));
                    }
                    else
                    {
                        grdItensAvali.DataSource = null;
                        grdItensAvali.DataBind();
                    }
                }
                else
                    chk.Checked = false;
            }
        }

        public void flagRow_OnCheckedChanged(object sender, EventArgs e)
        {
            var chkAtual = (CheckBox)sender;

            foreach (GridViewRow l in grdItensAvali.Rows)
            {
                CheckBox flFunc = (CheckBox)l.FindControl("flagRow");

                if (chkAtual.ClientID == flFunc.ClientID)
                {                    
                    TextBox txtFunc = (TextBox)l.FindControl("txtRespTecnico");
                    DropDownList ddlFunc = (DropDownList)l.FindControl("ddlRespTecnico");
                    TextBox txtSigla = (TextBox)l.FindControl("txtSigla");
                    TextBox txtNumero = (TextBox)l.FindControl("txtNumero");
                    DropDownList ddlUF = (DropDownList)l.FindControl("ddlUF");

                    var tb03 = TB03_COLABOR.RetornaPeloCoCol(int.Parse(ddlFunc.SelectedValue));

                    if (flFunc.Checked)
                    {
                        ddlFunc.Visible = true;
                        txtFunc.Visible = false;
                        txtSigla.Enabled = false;
                        txtNumero.Enabled = false;
                        ddlUF.Enabled = false;
                        ddlUF.SelectedValue = tb03.CO_UF_ENTID_PROFI;
                        txtSigla.Text = tb03.CO_SIGLA_ENTID_PROFI;
                        txtNumero.Text = tb03.NU_ENTID_PROFI;
                    }
                    else
                    {
                        ddlFunc.Visible = false;
                        txtFunc.Visible = true;
                        txtSigla.Enabled = true;
                        txtNumero.Enabled = true;
                        ddlUF.Enabled = true;
                        ddlUF.SelectedValue = 
                        txtSigla.Text = 
                        txtNumero.Text = "";
                    }                    
                }
            }
        }

        public void CarregaGridItensAvali(int idProced, int idResult)
        {
            var res = (from tbs417 in TBS417_EXAME_RESUL_ITENS.RetornaTodosRegistros()
                       join tbs414 in TBS414_EXAME_ITENS_AVALI.RetornaTodosRegistros() on tbs417.TBS414_EXAME_ITENS_AVALI.ID_ITENS_AVALI equals tbs414.ID_ITENS_AVALI
                       where tbs417.TBS416_EXAME_RESUL.ID_EXAME_RESUL == idResult
                       select new ItemAvali
                       {
                           coGrupo = tbs414.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.CO_GRUPO,
                           Grupo = tbs414.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.NO_GRUPO_EXAME,
                           coSubgrupo = tbs414.TBS413_EXAME_SUBGR.CO_SUBGR,
                           Subgrupo = tbs414.TBS413_EXAME_SUBGR.NO_SUBGR_EXAME,
                           ItemAval = tbs414.NO_ITEM_AVALI,
                           IdItensAvali = tbs414.ID_ITENS_AVALI,
                           IdExameResulItem = tbs417.ID_EXAME_RESUL_ITENS,
                           FlFunc = tbs417.FL_FUNCI == "S" ? true : false,
                           RespTecnico = tbs417.FL_FUNCI == "S" ? (tbs417.TB03_COLABOR != null ? tbs417.TB03_COLABOR.NO_COL : "") : tbs417.NO_AVALI,
                           Resultado = tbs417.VL_RESUL_ITENS,
                           Ordem = tbs414.NU_ORDEM
                       }).ToList();

            var itens = new List<int>();

            foreach (var i in res)
                itens.Add(i.IdItensAvali);

            var res_ = (from tbs414 in TBS414_EXAME_ITENS_AVALI.RetornaTodosRegistros()
                        where tbs414.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == idProced
                        && !itens.Contains(tbs414.ID_ITENS_AVALI)
                        select new ItemAvali
                        {
                            coGrupo = tbs414.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.CO_GRUPO,
                            Grupo = tbs414.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.NO_GRUPO_EXAME,
                            coSubgrupo = tbs414.TBS413_EXAME_SUBGR.CO_SUBGR,
                            Subgrupo = tbs414.TBS413_EXAME_SUBGR.NO_SUBGR_EXAME,
                            ItemAval = tbs414.NO_ITEM_AVALI,
                            IdItensAvali = tbs414.ID_ITENS_AVALI,
                            Ordem = tbs414.NU_ORDEM
                        }).ToList();

            res = res.Union(res_).ToList();

            grdItensAvali.DataSource = res.OrderBy(x => x.Grupo).ThenBy(x => x.Subgrupo).ThenBy(x => x.Ordem).ThenBy(y => y.ItemAval).ToList();
            grdItensAvali.DataBind();

            CarregaResponsavelTecnico();

            foreach (GridViewRow l in grdItensAvali.Rows)
            {
                CheckBox flFunc = (CheckBox)l.FindControl("flagRow");
                TextBox txtFunc = (TextBox)l.FindControl("txtRespTecnico");
                DropDownList ddlFunc = (DropDownList)l.FindControl("ddlRespTecnico");
                TextBox txtSigla = (TextBox)l.FindControl("txtSigla");
                TextBox txtNumero = (TextBox)l.FindControl("txtNumero");
                DropDownList ddlUF = (DropDownList)l.FindControl("ddlUF");
                DropDownList drpTpResult = (DropDownList)l.FindControl("drpTpResult");

                AuxiliCarregamentos.CarregaUFs(ddlUF, false, null, false, true);
                CarregaTipoResultado(drpTpResult, "NR");

                if (flFunc.Checked)
                {
                    ddlFunc.Visible = true;
                    txtFunc.Visible = false;
                    txtSigla.Enabled = false;
                    txtNumero.Enabled = false;
                    ddlUF.Enabled = false;
                }
                else
                {
                    ddlFunc.Visible = false;
                    txtFunc.Visible = true;
                    txtSigla.Enabled = true;
                    txtNumero.Enabled = true;
                    ddlUF.Enabled = true;
                }
            }
        }

        public void CarregaFuncionario()
        {
            AuxiliCarregamentos.CarregaFuncionarios(ddlFunciCadas,LoginAuxili.CO_EMP,false);

            if (chkFuncCadas.Checked)
            {
                txtFunciCadas.Visible = false;
                ddlFunciCadas.Visible = true;
            }
            else
            {
                txtFunciCadas.Visible = true;
                ddlFunciCadas.Visible = false;
            }
        }

        public void chkFuncCadas_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkFuncCadas.Checked)
            {
                txtFunciCadas.Visible = false;
                ddlFunciCadas.Visible = true;
            }
            else
            {
                txtFunciCadas.Visible = true;
                ddlFunciCadas.Visible = false;
            }
        }
        #endregion
        
        #region Classes

        public class Exames
        {
            public DateTime? DataHora { get; set; }
            public string Exame { get; set; }
            public string NuRegistro { get; set; }
            public string Tipo { get; set; }
            public string Contratante { get; set; }
            public string Solicitante { get; set; }
            public string Paciente { get; set; }
            public string Crm { get; set; }
            public string NuCrm { get; set; }
            public string CoCrm
            {
                get { return Crm + " - " + NuCrm; }
            }
            public string Situacao { get; set; }
            public int? IdProced { get; set; }
            public int? IdExameResul { get; set; }
            public int? CoAlu { get; set; }
            public int? CoAluEmp { get; set; }
        }

        public class ItemAvali
        {
            public string Grupo { get; set; }
            public string coGrupo_;
            public string coGrupo
            {
                get
                {
                    return !String.IsNullOrEmpty(coGrupo_) ? coGrupo_ : (Grupo.Length > 15 ? Grupo.Substring(0, 13) + "..." : Grupo);
                }
                set
                {
                    coGrupo_ = value;
                }
            }
            public string Subgrupo { get; set; }
            public string coSubgrupo_ { get; set; }
            public string coSubgrupo
            {
                get
                {
                    return !String.IsNullOrEmpty(coSubgrupo_) ? coSubgrupo_ : (Subgrupo.Length > 15 ? Subgrupo.Substring(0, 13) + "..." : Subgrupo);
                }
                set
                {
                    coSubgrupo_ = value;
                }
            }
            public string ItemAval { get; set; }
            public string Resultado { get; set; }
            public string RespTecnico { get; set; }
            public string DataHora { get; set; }
            public Boolean FlFunc { get; set; }
            public int IdExameResulItem { get; set; }
            public int IdItensAvali { get; set; }
            public int? Ordem { get; set; }
        }

        #endregion
    }
}
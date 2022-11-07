using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5213_BaixaLoteTituloBoleto
{
    public partial class cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } } 
        private Dictionary<string, string> tipoAgrupador = AuxiliBaseApoio.chave(tipoAgrupadorFinanceiro.ResourceManager);
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            grdTitulos.Visible = true;
            if (!IsPostBack)
            {
                CarregarUnidade();
                CarregarAgrupadores();
                CarregarResponsaveis();
                CarregarAlunos(); 
            }
        }

        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            bool alterado = false;
            foreach (GridViewRow linha in grdTitulos.Rows)
            {
                // Verifica se linha está checada
                if (((CheckBox)linha.Cells[0].FindControl("cbMarcar")).Checked)
                {
                    decimal multa, juros, desc, descBolsa, outros, total;
                    int numParc;
                    string numDoc, tipoRec;
                    DateTime dataRecebimento = DateTime.Now;
                    decimal.TryParse(((TextBox)linha.Cells[6].FindControl("tbMulta")).Text, out multa);
                    decimal.TryParse(((TextBox)linha.Cells[7].FindControl("tbJuros")).Text, out juros);
                    decimal.TryParse(((TextBox)linha.Cells[8].FindControl("tbDesc")).Text, out desc);
                    decimal.TryParse(((TextBox)linha.Cells[9].FindControl("tbDescBolsa")).Text, out descBolsa);
                    decimal.TryParse(((TextBox)linha.Cells[10].FindControl("tbOutros")).Text, out outros);
                    decimal.TryParse(linha.Cells[11].Text, out total);
                    int.TryParse(linha.Cells[2].Text, out numParc);
                    numDoc = linha.Cells[1].Text;
                    DateTime.TryParse(((TextBox)linha.Cells[12].FindControl("tbData")).Text, out dataRecebimento);
                    tipoRec = ((DropDownList)linha.Cells[13].FindControl("ddlTipos")).SelectedValue;

                    if (dataRecebimento > DateTime.Now)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "A data de recebimento do título não pode ser maior que a data atual.");
                        return;
                    }

                    if (tipoRec == "")
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Selecione o tipo de recebimento.");
                        return;
                    }
                    TB47_CTA_RECEB conta = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(LoginAuxili.CO_EMP, numDoc, numParc);

                    if (conta == null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Não foi possível localizar os títulos alterados.");
                        return;
                    }
                    else
                    {
                        conta.FL_TIPO_RECEB = tipoRec;
                        conta.FL_ORIGEM_PGTO = "X";
                        conta.CO_COL_BAIXA = LoginAuxili.CO_COL;
                        conta.DT_REC_DOC = dataRecebimento;
                        conta.DT_MOV_CAIXA = dataRecebimento;
                        conta.DT_ALT_REGISTRO = DateTime.Now;
                        conta.IC_SIT_DOC = "Q";

                        conta.VL_EXCE_PAG = null;
                        conta.VR_MUL_PAG = multa;
                        conta.VR_JUR_PAG = juros;
                        conta.VR_DES_PAG = desc;
                        conta.VR_DES_BOLSA_PAG = descBolsa;
                        conta.VR_OUT_PAG = outros;
                        conta.VR_PAG = total;

                        TB47_CTA_RECEB.SaveOrUpdate(conta, false);
                        alterado = true;
                    }
                }
            }
            if (alterado)
            {
                if (GestorEntities.CurrentContext.SaveChanges() < 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Erro ao baixar os títulos.");
                    return;
                }
                else
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Títulos baixados com Sucesso!", Request.Url.AbsoluteUri);
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this, "Nenhum título alterado.");
                return;
            }
        }

        #region Carregadores DropDown
        private void CarregarUnidade()
        {
            ddlUnidade.Items.Clear();
            ddlUnidade.Items.AddRange(AuxiliBaseApoio.UnidadesDDL(LoginAuxili.IDEADMUSUARIO, todos:true));
            if (ddlUnidade.Items.FindByValue(LoginAuxili.CO_EMP.ToString()) != null)
                ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();

            ddlAgrupador.Items.Clear();
            ddlResponsavel.Items.Clear();
            ddlAluno.Items.Clear();
        }
        /// <summary>
        /// Carrega todos os agrupadores por tipo
        /// </summary>
        private void CarregarAgrupadores()
        {
            ddlAgrupador.Items.Clear();
            ddlAgrupador.Items.AddRange(AuxiliBaseApoio.AgrupadoresDDL(tipoAgrupador[tipoAgrupadorFinanceiro.R], todos:true));

            ddlResponsavel.Items.Clear();
            ddlAluno.Items.Clear();
        }
        /// <summary>
        /// Carrega todos os responsáveis do sistema
        /// </summary>
        private void CarregarResponsaveis()
        {
            ddlResponsavel.Items.Clear();
            ddlResponsavel.Items.AddRange(AuxiliBaseApoio.ResponsaveisDDL(LoginAuxili.ORG_CODIGO_ORGAO, todos:true));

            ddlAluno.Items.Clear();
        }
        /// <summary>
        /// Carrega todos os alunos do responsável
        /// </summary>
        private void CarregarAlunos()
        {
            ddlAluno.Items.Clear();
            ddlAluno.Items.AddRange(AuxiliBaseApoio.AlunosDDL(ddlResponsavel.SelectedValue, selecione: true));

            grdTitulos.DataSource = null;
            grdTitulos.DataBind();
        }
        #endregion

        #region Eventos de componentes
        protected void ddlUnidadeContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(((DropDownList)sender).SelectedValue != "")
                CarregarAgrupadores();
        }

        protected void ddlAgrupador_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregarResponsaveis();
        }

        protected void ddlResponsavel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregarAlunos();
        }

        protected void btnGerar_Click(object sender, EventArgs e)
        {
            if (ddlAluno.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "O aluno deve ser informado.");
                return;
            }
            int agrupador = int.Parse(ddlAgrupador.SelectedValue);
            int undContrato = int.Parse(ddlUnidade.SelectedValue);
            int coAlu = int.Parse(ddlAluno.SelectedValue);
            DateTime dataIni, dataFim;
            DateTime.TryParse(txtDataPeriodoIni.Text, out dataIni);
            DateTime.TryParse(txtDataPeriodoFim.Text, out dataFim);


            List<TB47_CTA_RECEB> lst = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                      where (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "R") 
                      && tb47.CO_ALU == coAlu
                      && (agrupador != -1 ? tb47.CO_AGRUP_RECDESP == agrupador : 0 == 0)
                      && (undContrato == -1 ? 0==0 : tb47.CO_EMP_UNID_CONT == undContrato)
                      && (dataIni != DateTime.MinValue ? (tb47.DT_VEN_DOC >= dataIni) : 0 == 0)
                      && (dataFim != DateTime.MinValue ? (tb47.DT_VEN_DOC <= dataFim) : 0 == 0)
                      orderby tb47.DT_CAD_DOC
                      select tb47).DefaultIfEmpty().ToList();
            if (lst == null || lst.ElementAt(0) == null)
            {
                grdTitulos.DataSource = null;
                grdTitulos.DataBind();
                return;
            }
            List<titulos> contas = new List<titulos>();
            foreach(TB47_CTA_RECEB linha in lst)
            {
                if (linha.TB39_HISTORICO == null)
                    linha.TB39_HISTORICOReference.Load();
                titulos novaLinha = new titulos();
                AuxiliCalculos.valoresCalculadosTitulo valores = AuxiliCalculos.calculaValoresTitulo(linha, DateTime.Now);
                novaLinha.tipo = linha.FL_TIPO_RECEB;
                novaLinha.data = DateTime.Now.ToString("dd/MM/yyyy");
                novaLinha.historico = linha.TB39_HISTORICO.DE_HISTORICO;
                novaLinha.marcar = false;
                novaLinha.numDocumento = linha.NU_DOC;
                novaLinha.numPa = linha.NU_PAR;
                novaLinha.desc = valores.valorDesconto.toReal();
                novaLinha.descBolsa = valores.valorDescontoBolsa.toReal();
                novaLinha.juros = valores.valorJuros.toReal();
                novaLinha.multa = valores.valorMulta.toReal();
                novaLinha.outros = valores.valorOutros.toReal();
                novaLinha.valor = valores.valorParcela;
                novaLinha.total = valores.valorTotal;
                novaLinha.dataVencimento = linha.DT_VEN_DOC.ToString("dd/MM/yyyy");
                novaLinha.agrupador = TB315_AGRUP_RECDESP.RetornaPelaChavePrimaria(linha.CO_AGRUP_RECDESP ?? 0).DE_SITU_AGRUP_RECDESP;
                contas.Add(novaLinha);
            }

            this.grdTitulos.DataSource = contas.ToList();
            this.grdTitulos.DataBind();

            divGrid.Visible = liGrid.Visible = true;
        }

        protected void cbMarcar_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox marcado = ((CheckBox)sender);
            foreach (GridViewRow linha in grdTitulos.Rows)
            {
                CheckBox marcador = ((CheckBox)linha.Cells[0].FindControl("cbMarcar"));
                if (marcado.ClientID.Equals(marcador.ClientID))
                {
                    bool habilitar = marcador.Checked;
                    ((TextBox)linha.Cells[6].FindControl("tbMulta")).Enabled = habilitar;
                    ((TextBox)linha.Cells[7].FindControl("tbJuros")).Enabled = habilitar;
                    ((TextBox)linha.Cells[8].FindControl("tbDesc")).Enabled = habilitar;
                    ((TextBox)linha.Cells[9].FindControl("tbDescBolsa")).Enabled = habilitar;
                    ((TextBox)linha.Cells[10].FindControl("tbOutros")).Enabled = habilitar;

                    ((TextBox)linha.Cells[12].FindControl("tbData")).Enabled = habilitar;
                    ((DropDownList)linha.Cells[13].FindControl("ddlTipos")).Enabled = habilitar;
                }
            }
        }

        protected void tbMulta_TextChanged(object sender, EventArgs e)
        {
            decimal valor;
            if (((TextBox)sender).Text != "" && decimal.TryParse(((TextBox)sender).Text, out valor))
                somarValores();
        }

        protected void tbJuros_TextChanged(object sender, EventArgs e)
        {
            decimal valor;
            if (((TextBox)sender).Text != "" && decimal.TryParse(((TextBox)sender).Text, out valor))
                somarValores();
        }

        protected void tbDesc_TextChanged(object sender, EventArgs e)
        {
            decimal valor;
            if (((TextBox)sender).Text != "" && decimal.TryParse(((TextBox)sender).Text, out valor))
                somarValores();
        }

        protected void tbDescBolsa_TextChanged(object sender, EventArgs e)
        {
            decimal valor;
            if (((TextBox)sender).Text != "" && decimal.TryParse(((TextBox)sender).Text, out valor))
                somarValores();
        }

        protected void tbOutros_TextChanged(object sender, EventArgs e)
        {
            decimal valor;
            if (((TextBox)sender).Text != "" && decimal.TryParse(((TextBox)sender).Text, out valor))
                somarValores();
        }

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregarAgrupadores();
        }

        protected void ddlTipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            somarValores();
        }

        #endregion 

        #region Classes
        public class titulos
        {
            public bool marcar { get; set; }
            public string numDocumento { get; set; }
            public string historico { get; set; }
            public int numPa { get; set; }
            public string dataVencimento { get; set; }
            public string agrupador { get; set; }
            public decimal valor { get; set; }
            public string multa { get; set; }
            public string juros { get; set; }
            public string desc { get; set; }
            public string descBolsa { get; set; }
            public string outros { get; set; }
            public decimal total { get; set; }
            public string data { get; set; }
            public string tipo { get; set; }
            public ListItem[] comboTipo {
                get
                {
                    return AuxiliBaseApoio.BaseApoioDDL(tipoRecebimentoFinanceiro.ResourceManager, true);
                }
            }
        } 
        #endregion


        private void somarValores()
        {
            foreach (GridViewRow linha in grdTitulos.Rows)
            {
                CheckBox marcador = ((CheckBox)linha.Cells[0].FindControl("cbMarcar"));
                if (marcador.Checked)
                {
                    decimal multa, juros, desc, descBolsa, outros, parcela;
                    decimal.TryParse(((TextBox)linha.Cells[6].FindControl("tbMulta")).Text, out multa);
                    decimal.TryParse(((TextBox)linha.Cells[7].FindControl("tbJuros")).Text, out juros);
                    decimal.TryParse(((TextBox)linha.Cells[8].FindControl("tbDesc")).Text, out desc);
                    decimal.TryParse(((TextBox)linha.Cells[9].FindControl("tbDescBolsa")).Text, out descBolsa);
                    decimal.TryParse(((TextBox)linha.Cells[10].FindControl("tbOutros")).Text, out outros);
                    decimal.TryParse(linha.Cells[5].Text, out parcela);
                    linha.Cells[11].Text = ((parcela - (desc + descBolsa)) + (multa + juros + outros)).toReal();
                }
            }
        }





    }
}
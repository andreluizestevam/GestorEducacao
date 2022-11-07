//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: COBRANÇA BANCÁRIA
// OBJETIVO: ATUALIZAR DADOS DA REMESSA 
// DATA DE CRIAÇÃO: 14/03/2013
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//    DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// -----------+----------------------------+-------------------------------------
// 14/03/2013 | CAIO BARBOSA MENDONÇA      | CRIAÇÃO DA TELA
//
//
//
//
//
//
// OBS.: Como não estava funcionando utilizar a master de busca em sua totalidade, 
//        achei melhor trazer a maioria das coisas para a própria página. 
//
//


//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5210_ManuArquivoRemessa
{
    public partial class Busca : System.Web.UI.Page
    {
        public Buscas CurrentPadraoBuscas { get { return ((Buscas)this.Master); } }
        RegistroLog registroLog = new RegistroLog();

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaBoleto();
                CarregaCPF();
                fldGrid.Visible = false;
            }
        }

        protected void PopulaGrid()
        {
            string strTipoPeriodo = "V";

            DateTime dataInicio = DateTime.MinValue;
            DateTime dataFim = DateTime.MinValue;
            DateTime? dataVerifIni = null;
            DateTime? dataVerifFim = null;

            DateTime.TryParse(txtPeriodoDe.Text, out dataInicio);
            DateTime.TryParse(txtPeriodoAte.Text, out dataFim);

            dataVerifIni = dataInicio == DateTime.MinValue ? null : (DateTime?)dataInicio;
            dataVerifFim = dataFim == DateTime.MinValue ? null : (DateTime?)dataFim;

            string cpf = ddlCpfSacado.SelectedValue;

            string banco = "0";
            int agencia = 0;
            string conta = "0";

            if (ddlBoleto.SelectedValue != "0")
            {
                string[] dados = ddlBoleto.SelectedValue.Split('*');
                banco = dados[0];
                agencia = int.Parse(dados[1]);
                conta = dados[2];
            }



            var resultado = (from rem in TB322_ARQ_REM_BOLETO.RetornaTodosRegistros()
                                where
                                 (dataVerifIni == null || (strTipoPeriodo == "V" && rem.DT_VENCIMENTO >= dataVerifIni))
                                 && (dataVerifFim == null || (strTipoPeriodo == "V" && rem.DT_VENCIMENTO <= dataVerifFim))
                                 && (rem.FLA_ENVIO_BANCO == "N")
                                 && (agencia != 0 ? rem.CO_AGENCIA == agencia : agencia == 0)
                                 && (banco != "0" ? rem.IDEBANCO == banco : banco == "0")
                                 && (conta != "0" ? rem.CO_CONTA == conta : conta == "0")
                                 && (cpf != "0" ? rem.NU_CPFCNPJ == cpf : cpf == "0")
                                select new {
                                    rem.NU_NOSSO_NUMERO,
                                    rem.DT_VENCIMENTO,
                                    rem.NU_CPFCNPJ,
                                    SITU = rem.CO_SITU == "A" ? "Ativo" : "Inativo",
                                    rem.VL_TITULO,
                                    rem.IDEBANCO,
                                    rem.CO_AGENCIA,
                                    rem.CO_CONTA,
                                    rem.ID_ARQ_REM_BOLETO
                                 }
                                 );


            grdBusca.DataSource = (resultado.Count() > 0) ? resultado.OrderBy(c => c.DT_VENCIMENTO) : null;

        }

        #endregion


        #region Eventos da Grid

        protected void grdBusca_DataBound(object sender, EventArgs e)
        {
            GridViewRow grdViewRow = grdBusca.BottomPagerRow;

            if (grdViewRow != null)
            {
                DropDownList ddlPaginaLista = (DropDownList)grdViewRow.Cells[0].FindControl("ddlGrdPaginas");

                if (ddlPaginaLista != null)
                    for (int i = 0; i < grdBusca.PageCount; i++)
                    {
                        int numeroPagina = i + 1;
                        ListItem lstItem = new ListItem(numeroPagina.ToString());

                        if (i == grdBusca.PageIndex)
                            lstItem.Selected = true;

                        ddlPaginaLista.Items.Add(lstItem);
                    }
            }
        }

        protected void ddlGrdPaginas_SelectedIndexChanged(Object sender, EventArgs e)
        {
            GridViewRow grdViewRow = grdBusca.BottomPagerRow;

            if (grdViewRow != null)
            {
                DropDownList ddlListaPagina = (DropDownList)grdViewRow.Cells[0].FindControl("ddlGrdPaginas");
                BindGrdBusca(ddlListaPagina.SelectedIndex);
            }
        }

        protected void grdBusca_PageIndexChanging(object sender, GridViewPageEventArgs e) { 
            BindGrdBusca(e.NewPageIndex); 
        }

        protected void AtualizaTodosCheck(object sender, EventArgs e)
        {

            CheckBox checkTot = (CheckBox)sender;

            foreach (GridViewRow linha in grdBusca.Rows)
            {
                if (grdBusca.Rows.Count > 0)
                {
                    ((CheckBox)linha.Cells[0].FindControl("check")).Checked = checkTot.Checked;
                }
            }
        }
        
        #endregion


        #region Carregamento

        private void CarregaCPF()
        {
            ddlCpfSacado.DataSource = (from rem in TB322_ARQ_REM_BOLETO.RetornaTodosRegistros()
                                       select new
                                       {
                                           CPF = rem.NU_CPFCNPJ,
                                           NOME = (rem.NU_CPFCNPJ + " - " + rem.NO_SACADO)
                                       }
                ).Distinct();

            ddlCpfSacado.DataTextField = "NOME";
            ddlCpfSacado.DataValueField = "CPF";
            ddlCpfSacado.DataBind();

            ddlCpfSacado.Items.Insert(0, new ListItem("Todos", "0"));
        }


        private void CarregaBoleto()
        {
           var resu  = (from rem in TB322_ARQ_REM_BOLETO.RetornaTodosRegistros()
                                       select new
                                       {
                                           rem.IDEBANCO,
                                           rem.CO_AGENCIA,
                                           rem.CO_CONTA,
                                       }
                ).ToList();

           ddlBoleto.DataSource = (from rem in resu
                                   select new
                                   {
                                       DADOS = string.Format("{0}*{1}*{2}", rem.IDEBANCO,
                                                rem.CO_AGENCIA, rem.CO_CONTA),
                                       NOME = string.Format("BCO {0} - AGE {1} - CTA {2}", rem.IDEBANCO,
                                                rem.CO_AGENCIA, rem.CO_CONTA)
                                   }).Distinct();

            ddlBoleto.DataTextField = "NOME";
            ddlBoleto.DataValueField = "DADOS";
            ddlBoleto.DataBind();

            ddlBoleto.Items.Insert(0, new ListItem("Todos", "0"));
        }


        #endregion


        private void BindGrdBusca(int newPageIndex)
        {
            PopulaGrid();

            fldGrid.Visible = true;
            btnBusca.Visible = true;

            grdBusca.PageIndex = newPageIndex;
            grdBusca.DataBind();
        }

        // Função do botão de buscar
        protected void btnBusca_Click(object sender, EventArgs e)
        {
            if (LoginAuxili.ORG_CODIGO_ORGAO == 0)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "LogOut", "javascript:parent.LogOut();", true);

            if (Page.IsValid)
            {
                registroLog.RegistroLOG(null, RegistroLog.ACAO_PESQUISA);
                BindGrdBusca(0);
            }
        }


        protected void ProcessaSelecionados(object sender, EventArgs e)
        {

            try
            {
                int cont = 0;

                foreach (GridViewRow linha in grdBusca.Rows)
                {
                    if (grdBusca.Rows.Count > 0)
                    {
                        if (((CheckBox)linha.Cells[0].FindControl("check")).Checked == true)
                        {
                            int id_arq = Convert.ToInt32(linha.Cells[0].Text);

                            TB322_ARQ_REM_BOLETO rem = TB322_ARQ_REM_BOLETO.RetornaPelaChavePrimaria(id_arq);

                            if(rem.CO_SITU == "A")
                                rem.CO_SITU = "I";
                            else
                                rem.CO_SITU = "A";

                            rem.DT_ALTERACAO = DateTime.Now;
                            rem.CO_EMP_ALTERACAO = LoginAuxili.CO_EMP;
                            rem.CO_COL_ALTERACAO = LoginAuxili.CO_COL;
                            rem.NR_IP_ACESS_ALTERACAO = LoginAuxili.IP_USU;

                            TB322_ARQ_REM_BOLETO.SaveOrUpdate(rem);
                            cont++;
                        }
                    }
                }

                TB322_ARQ_REM_BOLETO rm = new TB322_ARQ_REM_BOLETO();
                registroLog.RegistroLOG(rm, RegistroLog.ACAO_EDICAO);

                fldGrid.Visible = false;

                AuxiliPagina.EnvioMensagemSucesso(this.Page, "Dados alterados com sucesso.  " + cont.ToString() + " itens processados.");

            }
            catch (Exception ex)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, ex.ToString());
            }

        }

    }




}
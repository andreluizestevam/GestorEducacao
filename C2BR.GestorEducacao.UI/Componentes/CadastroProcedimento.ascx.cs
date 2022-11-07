using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace C2BR.GestorEducacao.UI.Componentes
{
    public partial class CadastroProcedimento1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void lnkConfirmarProced_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hidCoPaciProced.Value))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Selecione um paciente antes de atribuir procedimentos");
                    return;
                }

                int coPaci = int.Parse(hidCoPaciProced.Value);
                int coAgend = int.Parse(hidCoAgendProced.Value);

                int qntItensSelecionados = 0;
                //Inclui registro de item de planejamento na tabela TBS386_ITENS_PLANE_AVALI
                #region Inclui o Item de Planjamento
                //foreach (GridViewRow lis in grdHorario.Rows)
                //{
                //    //Verifica a linha que foi selecionada
                //    if (((CheckBox)lis.FindControl("ckSelectHr")).Checked)
                //    {
                //        qntItensSelecionados++;
                //    }
                //}


                //Verifica a linha que foi selecionada

                // recuperar o código da agenfa para efetuar o insert
                int hidCoAgend = 0;

                if (grdProcedimentos.Rows.Count != 0)
                {
                    foreach (GridViewRow li in grdProcedimentos.Rows)
                    {
                        DropDownList ddlContrat;
                        DropDownList ddlPlan;
                        DropDownList ddlProced;
                        DropDownList ddlSolic;
                        TextBox txtCart;
                        TextBox txtIdItem;
                        TextBox valorUnit;
                        TextBox valorTotal;
                        TextBox txtQtp;
                        CheckBox chkCort;
                        ddlSolic = ((DropDownList)li.FindControl("ddlSolicProc"));
                        ddlContrat = ((DropDownList)li.FindControl("ddlContratProc"));
                        ddlPlan = ((DropDownList)li.FindControl("ddlPlanoProc"));
                        ddlProced = ((DropDownList)li.FindControl("ddlProcMod"));
                        txtCart = ((TextBox)li.FindControl("txtNrCartProc"));
                        valorUnit = ((TextBox)li.FindControl("txtValorUnit"));
                        valorTotal = ((TextBox)li.FindControl("txtValorTotalMod"));
                        txtQtp = ((TextBox)li.FindControl("txtQTPMod"));
                        txtIdItem = ((TextBox)li.FindControl("txtCoItemProced"));
                        chkCort = ((CheckBox)li.FindControl("chkCortProc"));

                        if (string.IsNullOrEmpty(ddlProced.SelectedValue))
                        {
                            AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Selecione um procedimento");
                            ddlProced.Focus();
                            //AbreModalPadrao("AbreModalProcedHorar();");
                            return;
                        }
                        if (string.IsNullOrEmpty(txtQtp.Text))
                        {
                            AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Informe a quantidade de procedimentos");
                            txtQtp.Focus();
                            //AbreModalPadrao("AbreModalProcedHorar();");
                            return;
                        }
                        if (coPaci == 0 || coPaci <= 0)
                        {
                            AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Para confirmar, é necessário selecionar um paciente");
                            //AbreModalPadrao("AbreModalProcedHorar();");
                            return;
                        }

                        TBS174_AGEND_HORAR agend = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(hidCoAgend);

                        if (((agend.TB250_OPERA == null) || (agend.TB251_PLANO_OPERA == null) || (agend.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == null)) && (String.IsNullOrEmpty(txtIdItem.Text)))
                        {
                            agend.TB250_OPERA = TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlContrat.SelectedValue));
                            agend.TB251_PLANO_OPERA = TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlan.SelectedValue));
                            agend.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProced.SelectedValue));
                            agend.TBS370_PLANE_AVALI = RecuperaPlanejamento((agend.TBS370_PLANE_AVALI != null ? agend.TBS370_PLANE_AVALI.ID_PLANE_AVALI : (int?)null), coPaci);
                            agend.FL_CORTESIA = (chkCort.Checked ? "S" : "N");
                            agend.VL_CONSUL = (!string.IsNullOrEmpty(valorUnit.Text) ? decimal.Parse(valorUnit.Text) : (decimal?)null);
                            TBS174_AGEND_HORAR.SaveOrUpdate(agend, true);
                        }
                        //Se tem item de planjamento, trás objeto da entidade dele, se não, cria novo objeto da entidade
                        TBS386_ITENS_PLANE_AVALI tbs386;
                        if (String.IsNullOrEmpty(txtIdItem.Text) || int.Parse(txtIdItem.Text) <= 0)
                        {
                            tbs386 = new TBS386_ITENS_PLANE_AVALI();
                            //Dados do cadastro
                            tbs386.DT_CADAS = DateTime.Now;
                            tbs386.CO_COL_CADAS = LoginAuxili.CO_COL;
                            tbs386.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                            tbs386.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                            tbs386.IP_CADAS = Request.UserHostAddress;
                        }
                        else
                        {
                            tbs386 = TBS386_ITENS_PLANE_AVALI.RetornaPelaChavePrimaria(int.Parse(txtIdItem.Text));
                        }
                        //Dados da situação
                        tbs386.CO_SITUA = "A";
                        tbs386.DT_SITUA = DateTime.Now;
                        tbs386.CO_COL_SITUA = LoginAuxili.CO_COL;
                        tbs386.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                        tbs386.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                        tbs386.IP_SITUA = Request.UserHostAddress;
                        tbs386.DE_RESUM_ACAO = null;

                        //Dados básicos do item de planejamento
                        tbs386.TBS370_PLANE_AVALI = RecuperaPlanejamento((agend.TBS370_PLANE_AVALI != null ? agend.TBS370_PLANE_AVALI.ID_PLANE_AVALI : (int?)null), coPaci);
                        tbs386.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProced.SelectedValue));
                        tbs386.NR_ACAO = RecuperaUltimoNrAcao(tbs386.TBS370_PLANE_AVALI.ID_PLANE_AVALI);
                        tbs386.QT_SESSO = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaPelaAgendaEProcedimento(hidCoAgend, TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProced.SelectedValue)).ID_PROC_MEDI_PROCE).Count; //Conta quantos itens existem na lista para este mesmo e agenda
                        tbs386.DT_INICI = agend.DT_AGEND_HORAR; //Verifica qual a primeira data na lista
                        tbs386.DT_FINAL = agend.DT_AGEND_HORAR; //Verifica qual a última data na lista
                        tbs386.FL_AGEND_FEITA_PLANE = "N";
                        tbs386.QT_PROCED = int.Parse(txtQtp.Text);
                        tbs386.ID_OPER = String.IsNullOrEmpty(ddlContrat.Text) ? null : (int?)int.Parse(ddlContrat.Text);
                        tbs386.ID_PLAN = String.IsNullOrEmpty(ddlPlan.Text) ? null : (int?)int.Parse(ddlPlan.Text);
                        tbs386.FL_CORTESIA = (chkCort.Checked ? "S" : "N");

                        tbs386.VL_PROCED = (!string.IsNullOrEmpty(valorUnit.Text) ? decimal.Parse(valorUnit.Text) : (decimal?)null);

                        //Data prevista é a data do agendamento associado
                        tbs386.DT_AGEND = agend.DT_AGEND_HORAR;

                        TBS386_ITENS_PLANE_AVALI.SaveOrUpdate(tbs386, true);

                        //Associa o item criado ao agendamento em contexto na tabela TBS389_ASSOC_ITENS_PLANE_AGEND
                        #region Associa o Item ao Agendamento
                        TBS389_ASSOC_ITENS_PLANE_AGEND tbs389 = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(x => x.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI == tbs386.ID_ITENS_PLANE_AVALI).FirstOrDefault();
                        if (tbs389 == null)
                        {
                            tbs389 = new TBS389_ASSOC_ITENS_PLANE_AGEND();

                            tbs389.TBS174_AGEND_HORAR = agend;
                            tbs389.TBS386_ITENS_PLANE_AVALI = tbs386;
                        }
                        TBS389_ASSOC_ITENS_PLANE_AGEND.SaveOrUpdate(tbs389, true);


                        //agend.CO_ALU = coPaci;                        


                        #endregion
                    }
                }

                #endregion

                //LimparGridHorarios();
                //CarregaGridHorariosAlter();
                //carregaGridNovaComContextoProced();
                //AbreModalPadrao("AbreModalProcedHorar();");
            }
            catch (Exception) { }
        }
        private TBS370_PLANE_AVALI RecuperaPlanejamento(int? ID_PLANE_AVALI, int CO_ALU)
        {
            if (ID_PLANE_AVALI.HasValue)
                return TBS370_PLANE_AVALI.RetornaPelaChavePrimaria(ID_PLANE_AVALI.Value);
            else // Já que não tem ainda, cria um novo planejamento e retorna um objeto do mesmo no método
            {
                TBS370_PLANE_AVALI tbs370 = new TBS370_PLANE_AVALI();
                tbs370.CO_ALU = CO_ALU;

                //Dados do cadastro
                tbs370.DT_CADAS = DateTime.Now;
                tbs370.CO_COL_CADAS = LoginAuxili.CO_COL;
                tbs370.CO_EMP_COL_CADAS = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                tbs370.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                tbs370.IP_CADAS = Request.UserHostAddress;

                //Dados da situação
                tbs370.CO_SITUA = "A";
                tbs370.DT_SITUA = DateTime.Now;
                tbs370.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs370.CO_EMP_COL_SITUA = (TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP);
                tbs370.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs370.IP_SITUA = Request.UserHostAddress;
                TBS370_PLANE_AVALI.SaveOrUpdate(tbs370, true);

                return tbs370;
            }
        }
        private int RecuperaUltimoNrAcao(int ID_PLANE_AVALI)
        {
            var res = (from tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros()
                       where tbs389.TBS386_ITENS_PLANE_AVALI.TBS370_PLANE_AVALI.ID_PLANE_AVALI == ID_PLANE_AVALI
                       select new { tbs389.TBS386_ITENS_PLANE_AVALI.NR_ACAO }).OrderByDescending(w => w.NR_ACAO).FirstOrDefault();

            /*
             *Retorna o último número de ação encontrado (para a agenda e procedimento recebidos como parâmetro) + 1.
             *Se não houver, retorna o número 1
             */
            return (res != null ? res.NR_ACAO + 1 : 1);
        }
        protected void chkRetornaProced_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            if (atual.Checked)
            {
                int coPaci;
                int coAgend;
                //LimparGridProced();
                if (String.IsNullOrEmpty(hidCoPaciProced.Value) && String.IsNullOrEmpty(hidCoAgendProced.Value))
                {
                    //carregaGridProced();
                }
                else if (!String.IsNullOrEmpty(hidCoPaciProced.Value) && !String.IsNullOrEmpty(hidCoAgendProced.Value))
                {
                    coPaci = int.Parse(hidCoPaciProced.Value);
                    coAgend = int.Parse(hidCoAgendProced.Value);

                    //carregaGridProced(coAgend, coPaci, true);
                }
                else if (!String.IsNullOrEmpty(hidCoPaciProced.Value) && String.IsNullOrEmpty(hidCoAgendProced.Value))
                {
                    coPaci = int.Parse(hidCoPaciProced.Value);

                    //carregaGridProced(0, coPaci, true);
                }
            }
            else
            {
                int coPaci;
                int coAgend;
                //LimparGridProced();
                if (String.IsNullOrEmpty(hidCoPaciProced.Value) && String.IsNullOrEmpty(hidCoAgendProced.Value))
                {
                    //carregaGridProced();
                }
                else if (!String.IsNullOrEmpty(hidCoPaciProced.Value) && !String.IsNullOrEmpty(hidCoAgendProced.Value))
                {
                    coPaci = int.Parse(hidCoPaciProced.Value);
                    coAgend = int.Parse(hidCoAgendProced.Value);

                    //carregaGridProced(coAgend, coPaci, true);
                }
                else if (!String.IsNullOrEmpty(hidCoPaciProced.Value) && String.IsNullOrEmpty(hidCoAgendProced.Value))
                {
                    coPaci = int.Parse(hidCoPaciProced.Value);

                    //carregaGridProced(0, coPaci, true);
                }
            }
            //AbreModalPadrao("AbreModalProcedHorar();");
        }
        protected void chkCortProc_OnChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox ck;
            if (grdProcedimentos.Rows.Count != 0)
            {
                foreach (GridViewRow li in grdProcedimentos.Rows)
                {
                    TextBox valorUnit;
                    TextBox valorTotal;
                    TextBox txtQtp;
                    valorUnit = ((TextBox)li.FindControl("txtValorUnit"));
                    valorTotal = ((TextBox)li.FindControl("txtValorTotalMod"));
                    txtQtp = ((TextBox)li.FindControl("txtQTPMod"));
                    ck = ((CheckBox)li.FindControl("chkCortProc"));
                    if (ck.ClientID == atual.ClientID)
                    {
                        if (ck.Checked)
                        {
                            valorUnit.Enabled = false;
                            valorTotal.Enabled = false;
                        }
                    }
                }
            }
            //AbreModalPadrao("AbreModalProcedHorar();");
        }
    }
}
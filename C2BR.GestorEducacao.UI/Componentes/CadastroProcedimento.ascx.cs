using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System;
using System.Collections.Generic;
using System.Data;
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

        #region Código tela Procedimentos

        #region Montando a tela
        protected void lnkAddProcPla_OnClick(object sender, EventArgs e)
        {
            CriaNovaLinhaGridProced(20);
        }
        protected void CriaNovaLinhaGridProced(int paci = 0)
        {
            try
            {
                Session["GridSolic_PROC_PLA"] = null;

                DataTable dtV = CriarColunasELinhaGridProced();

                if (paci != 0)
                {
                    var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                               where (tb07.CO_ALU == paci)
                               select new CriaNovaLinhaGridProcedClass
                               {
                                   CO_ALU = tb07.CO_ALU,
                                   CONTRAT = tb07.TB250_OPERA.ID_OPER,
                                   PLANO = tb07.TB251_PLANO_OPERA.ID_PLAN,
                                   CART = tb07.NU_PLANO_SAUDE
                               }).FirstOrDefault();

                    DataRow linha = dtV.NewRow();
                    linha["CONTRAT"] = res.CONTRAT != null || res.CONTRAT != 0 ? res.CONTRAT.ToString() : "";
                    linha["PLANO"] = res.PLANO != null || res.PLANO != 0 ? res.PLANO.ToString() : "";
                    linha["CART"] = !String.IsNullOrEmpty(res.CART) ? res.CART : "";
                    linha["SOLIC"] = LoginAuxili.CO_COL.ToString();
                    linha["PROCED"] = "";
                    linha["CORT"] = false;
                    linha["UNIT"] = "";
                    linha["QTP"] = "";
                    linha["TOTAL"] = "";
                    linha["ID_ITENS_PLANE_AVALI"] = "";
                    dtV.Rows.Add(linha);
                }
                else
                {
                    DataRow linha = dtV.NewRow();
                    linha["CONTRAT"] = "";
                    linha["SOLIC"] = LoginAuxili.CO_COL.ToString();
                    linha["PLANO"] = "";
                    linha["CART"] = "";
                    linha["PROCED"] = "";
                    linha["CORT"] = false;
                    linha["UNIT"] = "";
                    linha["QTP"] = "";
                    linha["TOTAL"] = "";
                    linha["ID_ITENS_PLANE_AVALI"] = "";
                    dtV.Rows.Add(linha);
                }

                Session["GridSolic_PROC_PLA"] = dtV;
                carregaGridNovaComContextoProced();

            }
            catch (Exception) { }
        }
        protected void carregaGridNovaComContextoProced()
        {
            DataTable dtV = new DataTable();

            dtV = (DataTable)Session["GridSolic_PROC_PLA"];

            grdProcedimentos.DataSource = dtV;
            grdProcedimentos.DataBind();

            int aux = 0;
            foreach (GridViewRow li in grdProcedimentos.Rows)
            {
                DropDownList ddlContrat;
                DropDownList ddlPlan;
                DropDownList ddlProced;
                DropDownList ddlSolic;
                TextBox txtCart;
                TextBox valorUnit;
                TextBox valorTotal;
                TextBox txtQtp;
                CheckBox chkCort;
                TextBox idItemProced;
                ddlSolic = ((DropDownList)li.FindControl("ddlSolicProc"));
                ddlContrat = ((DropDownList)li.FindControl("ddlContratProc"));
                ddlPlan = ((DropDownList)li.FindControl("ddlPlanoProc"));
                ddlProced = ((DropDownList)li.FindControl("ddlProcMod"));
                txtCart = ((TextBox)li.FindControl("txtNrCartProc"));
                valorUnit = ((TextBox)li.FindControl("txtValorUnit"));
                valorTotal = ((TextBox)li.FindControl("txtValorTotalMod"));
                txtQtp = ((TextBox)li.FindControl("txtQTPMod"));
                chkCort = ((CheckBox)li.FindControl("chkCortProc"));
                idItemProced = ((TextBox)li.FindControl("txtCoItemProced"));

                string solic, contrat, plano, cart, proced, unit, qtp, total, cort, idItem;

                //Coleta os valores do dtv da modal popup
                solic = dtV.Rows[aux]["SOLIC"].ToString();
                contrat = dtV.Rows[aux]["CONTRAT"].ToString();
                plano = dtV.Rows[aux]["PLANO"].ToString();
                cart = dtV.Rows[aux]["CART"].ToString();
                proced = dtV.Rows[aux]["PROCED"].ToString();
                unit = dtV.Rows[aux]["UNIT"].ToString();
                qtp = dtV.Rows[aux]["QTP"].ToString();
                total = dtV.Rows[aux]["TOTAL"].ToString();
                cort = dtV.Rows[aux]["CORT"].ToString();
                idItem = dtV.Rows[aux]["ID_ITENS_PLANE_AVALI"].ToString();

                var opr = 0;

                //if (!String.IsNullOrEmpty(ddlPlanProcPlan.SelectedValue) && int.Parse(ddlPlanProcPlan.SelectedValue) != 0)
                //{
                //    var plan = TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlanProcPlan.SelectedValue));
                //    plan.TB250_OPERAReference.Load();
                //    opr = plan.TB250_OPERA != null ? plan.TB250_OPERA.ID_OPER : 0;
                //}

                //CarregarProcedimentos(ddlCodigoi, opr, "EX");

                var tbs174_tipoConsu = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidCoAgendProced.Value)).TP_CONSU;

                lblTituloProcedMod.Text = (tbs174_tipoConsu.Equals("P") ? "PROCEDIMENTO" : tbs174_tipoConsu.Equals("N") ? "CONSULTA" : tbs174_tipoConsu.Equals("E") ? "EXAME" : tbs174_tipoConsu.Equals("V") ? "VACINA" : tbs174_tipoConsu.Equals("C") ? "CIRURGIA" : "OUTROS");
                CarregaOperadoras(ddlContrat, contrat);
                CarregarPlanosSaude(ddlPlan, ddlContrat);
                CarregaProcedimentos(ddlProced, ddlContrat, proced, tbs174_tipoConsu);
                AuxiliCarregamentos.CarregaProfissionaisSaude(ddlSolic, LoginAuxili.CO_EMP, false, "0");
                //SelecionaOperadoraPlanoPaciente();
                //ddlContrat.SelectedValue = contrat;
                ddlSolic.SelectedValue = solic;
                ddlPlan.SelectedValue = plano;
                CalcularPreencherValoresTabelaECalculado(ddlProced, ddlContrat, ddlPlan, valorUnit);
                txtCart.Text = cart;
                valorTotal.Text = total;
                txtQtp.Text = qtp;
                chkCort.Checked = Convert.ToBoolean(cort);
                idItemProced.Text = idItem;
                aux++;
                if (chkCort.Checked)
                {
                    valorUnit.Enabled = false;
                    valorTotal.Enabled = false;
                }

                if (String.IsNullOrEmpty(cart))
                {
                    txtCart.Enabled = true;
                }
                else
                {
                    txtCart.Enabled = false;
                }
            }
        }


        private DataTable CriarColunasELinhaGridProced()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "ID_ITENS_PLANE_AVALI";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CONTRAT";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "SOLIC";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PLANO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CART";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PROCED";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.Boolean");
            dcATM.ColumnName = "CORT";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "UNIT";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "QTP";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "TOTAL";
            dtV.Columns.Add(dcATM);

            DataRow linha;

            foreach (GridViewRow li in grdProcedimentos.Rows)
            {
                linha = dtV.NewRow();
                linha["CONTRAT"] = (((DropDownList)li.FindControl("ddlContratProc")).SelectedValue);
                linha["PLANO"] = (((DropDownList)li.FindControl("ddlPlanoProc")).SelectedValue);
                linha["SOLIC"] = (((DropDownList)li.FindControl("ddlSolicProc")).SelectedValue);
                linha["CART"] = (((TextBox)li.FindControl("txtNrCartProc")).Text);
                linha["PROCED"] = (((DropDownList)li.FindControl("ddlProcMod")).SelectedValue);
                linha["CORT"] = (((CheckBox)li.FindControl("chkCortProc")).Checked);
                linha["UNIT"] = (((TextBox)li.FindControl("txtValorUnit")).Text);
                linha["QTP"] = (((TextBox)li.FindControl("txtQTPMod")).Text);
                linha["TOTAL"] = (((TextBox)li.FindControl("txtValorTotalMod")).Text);
                linha["ID_ITENS_PLANE_AVALI"] = (((TextBox)li.FindControl("txtCoItemProced")).Text);
                dtV.Rows.Add(linha);
            }

            return dtV;
        }
        public class CriaNovaLinhaGridProcedClass
        {
            public int CO_ALU { get; set; }
            public int? CONTRAT { get; set; }
            public int? PLANO { get; set; }
            public string CART { get; set; }
        }

        protected void ExcluiItemGridProced(int Index)
        {
            DataTable dtV = CriarColunasELinhaGridProced();
            //try
            //{
            //    if (idItem != 0)
            //    {
            //        var tbs386 = TBS386_ITENS_PLANE_AVALI.RetornaPelaChavePrimaria(idItem);
            //        var tbs389 = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(x => x.TBS386_ITENS_PLANE_AVALI.ID_ITENS_PLANE_AVALI == idItem).FirstOrDefault();

            //        TBS389_ASSOC_ITENS_PLANE_AGEND.Delete(tbs389, true);
            //        TBS386_ITENS_PLANE_AVALI.Delete(tbs386, true);
            //    }

            //}
            //catch (Exception) { }
            dtV.Rows.RemoveAt(Index); // Exclui o item de index correspondente
            Session["GridSolic_PROC_PLA"] = dtV;
            carregaGridNovaComContextoProced();
        }
        protected void LimparGridProced()
        {
            grdProcedimentos.DataSource = null;
            grdProcedimentos.DataBind();
        }
        #endregion
        #region CLIQUES
        protected void imgExcPla_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            int idItem = 0;
            int aux = 0;
            if (grdProcedimentos.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdProcedimentos.Rows)
                {
                    img = (ImageButton)linha.FindControl("imgExcPla");
                    TextBox hidIdItem = ((TextBox)linha.FindControl("txtCoItemProced"));
                    idItem = (String.IsNullOrEmpty(hidIdItem.Text) ? 0 : int.Parse(hidIdItem.Text));
                    if (img.ClientID == atual.ClientID)
                        aux = linha.RowIndex;
                }
            }
            ExcluiItemGridProced(aux);
            LimparGridProced();
            carregaGridNovaComContextoProced();
            //LimparGridHorarios();
            //CarregaGridHorariosAlter();
            //AbreModalPadrao("AbreModalProcedHorar();");
        }

        protected void Qtp_OnTextChanged(object sender, EventArgs e)
        {
            TextBox atual = (TextBox)sender;
            TextBox txt;

            foreach (GridViewRow li in grdProcedimentos.Rows)
            {
                txt = (TextBox)li.FindControl("txtQTPMod");
                //Só marca os outros, se o registro estiver selecionado
                if (txt.ClientID == atual.ClientID)
                {
                    TextBox txtValorUnit = (TextBox)li.FindControl("txtValorUnit");
                    TextBox txtValorTotal = (TextBox)li.FindControl("txtValorTotalMod");

                    decimal result = ((String.IsNullOrEmpty(txtValorUnit.Text) ? 0 : Decimal.Parse(txtValorUnit.Text)) * (String.IsNullOrEmpty(txt.Text) ? 0 : Decimal.Parse(txt.Text)));

                    txtValorTotal.Text = result.ToString();
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


        protected void ddlProcedAgend_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddlOper, ddlPlan, ddlProc;

            int qntProced = 0;
            bool existeProcedimento = false; //Define se existe um procedimento igual já selecionado

            if (grdProcedimentos.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdProcedimentos.Rows)
                {
                    ddlProc = (DropDownList)linha.FindControl("ddlProcMod");

                    if (ddlProc.SelectedValue.Equals(atual.SelectedValue))
                    {
                        qntProced++;
                        if (qntProced > 1)
                        {
                            existeProcedimento = true;
                            ddlProc.Focus();
                            break;
                        }
                    }
                }
            }

            if (!existeProcedimento)
            {
                if (grdProcedimentos.Rows.Count != 0)
                {
                    foreach (GridViewRow linha in grdProcedimentos.Rows)
                    {
                        ddlOper = (DropDownList)linha.FindControl("ddlContratProc");
                        ddlPlan = (DropDownList)linha.FindControl("ddlPlanoProc");
                        ddlProc = (DropDownList)linha.FindControl("ddlProcMod");
                        TextBox txtValor = (TextBox)linha.FindControl("txtValorUnit");

                        //Carrega os planos de saúde da operadora quando encontra o objeto que invocou o postback
                        if (ddlProc.ClientID == atual.ClientID)
                            CalcularPreencherValoresTabelaECalculado(ddlProc, ddlOper, ddlPlan, txtValor);
                    }
                }
            }
            else
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "MSG: Este procedimento já foi listado.");
                atual.SelectedValue = null;
            }
            //AbreModalPadrao("AbreModalProcedHorar();");
        }

        protected void chkRetornaProced_OnCheckedChanged(object sender, EventArgs e)
        {
            //CheckBox atual = (CheckBox)sender;
            //if (atual.Checked)
            //{
            //    int coPaci;
            //    int coAgend;
            //    LimparGridProced();
            //    if (String.IsNullOrEmpty(hidCoPaciProced.Value) && String.IsNullOrEmpty(hidCoAgendProced.Value))
            //    {
            //        carregaGridProced();
            //    }
            //    else if (!String.IsNullOrEmpty(hidCoPaciProced.Value) && !String.IsNullOrEmpty(hidCoAgendProced.Value))
            //    {
            //        coPaci = int.Parse(hidCoPaciProced.Value);
            //        coAgend = int.Parse(hidCoAgendProced.Value);

            //        carregaGridProced(coAgend, coPaci, true);
            //    }
            //    else if (!String.IsNullOrEmpty(hidCoPaciProced.Value) && String.IsNullOrEmpty(hidCoAgendProced.Value))
            //    {
            //        coPaci = int.Parse(hidCoPaciProced.Value);

            //        carregaGridProced(0, coPaci, true);
            //    }
            //}
            //else
            //{
            //    int coPaci;
            //    int coAgend;
            //    LimparGridProced();
            //    if (String.IsNullOrEmpty(hidCoPaciProced.Value) && String.IsNullOrEmpty(hidCoAgendProced.Value))
            //    {
            //        carregaGridProced();
            //    }
            //    else if (!String.IsNullOrEmpty(hidCoPaciProced.Value) && !String.IsNullOrEmpty(hidCoAgendProced.Value))
            //    {
            //        coPaci = int.Parse(hidCoPaciProced.Value);
            //        coAgend = int.Parse(hidCoAgendProced.Value);

            //        carregaGridProced(coAgend, coPaci, true);
            //    }
            //    else if (!String.IsNullOrEmpty(hidCoPaciProced.Value) && String.IsNullOrEmpty(hidCoAgendProced.Value))
            //    {
            //        coPaci = int.Parse(hidCoPaciProced.Value);

            //        carregaGridProced(0, coPaci, true);
            //    }
            //}
            //AbreModalPadrao("AbreModalProcedHorar();");
        }
        protected void ddlOpers_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddl;

            foreach (GridViewRow li in grdProcedimentos.Rows)
            {
                ddl = (DropDownList)li.FindControl("ddlContratProc");
                //Só marca os outros, se o registro estiver selecionado
                if (ddl.ClientID == atual.ClientID)
                {
                    DropDownList ddlPlan = (DropDownList)li.FindControl("ddlPlanoProc");
                    DropDownList ddlProc = (DropDownList)li.FindControl("ddlProcMod");
                    var tbs174_tipoConsu = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(hidCoAgendProced.Value)).TP_CONSU;

                    CarregarPlanosSaude(ddlPlan, ddl);
                    CarregaProcedimentos(ddlProc, ddl, null, tbs174_tipoConsu);
                }
            }
            AbreModalPadrao("AbreModalProcedHorar();");
        }
        #endregion
        #region Procedimentos

        private void CarregaOperadoras(DropDownList ddl, string selec)
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddl, false, false, true);
            ddl.SelectedValue = selec;
        }
        private void CarregarPlanosSaude(DropDownList ddlPlan, DropDownList ddlOper)
        {
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlan, ddlOper, false, false, true);
        }
        private void CarregaProcedimentos(DropDownList ddl, DropDownList ddlOper, string selec = null, string tipo = null)
        {

            int idOper = (!string.IsNullOrEmpty(ddlOper.SelectedValue) ? int.Parse(ddlOper.SelectedValue) : 0);
            string tipoV = (String.IsNullOrEmpty(tipo) ? null : tipo.Equals("P") ? "PR" : tipo.Equals("N") ? "CO" : tipo.Equals("R") ? "CO" : tipo.Equals("E") ? "EX" : tipo.Equals("V") ? "VA" : tipo.Equals("C") ? "CI" : tipo.Equals("O") ? "OU" : null);
            var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                       where tbs356.CO_SITU_PROC_MEDI == "A"
                       && (idOper != 0 ? tbs356.TB250_OPERA.ID_OPER == idOper : tbs356.TB250_OPERA == null)
                       && (String.IsNullOrEmpty(tipoV) ? 0 == 0 : tbs356.CO_TIPO_PROC_MEDI == tipoV)
                       select new { tbs356.ID_PROC_MEDI_PROCE, CO_PROC_MEDI = tbs356.CO_PROC_MEDI + " - " + tbs356.NM_PROC_MEDI }).OrderBy(w => w.CO_PROC_MEDI).ToList();

            if (res != null)
            {
                ddl.DataTextField = "CO_PROC_MEDI";
                ddl.DataValueField = "ID_PROC_MEDI_PROCE";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            ddl.Items.Insert(0, new ListItem(".'. Selecione .'.", ""));

            if (!string.IsNullOrEmpty(selec) && ddl.Items.FindByValue(selec) != null)
                ddl.SelectedValue = selec;
        }
        private void CalcularPreencherValoresTabelaECalculado(DropDownList ddlProcConsul, DropDownList ddlOperPlano, DropDownList ddlPlano, TextBox txtValor)
        {
            int idProc = (!string.IsNullOrEmpty(ddlProcConsul.SelectedValue) ? int.Parse(ddlProcConsul.SelectedValue) : 0);
            int idOper = (!string.IsNullOrEmpty(ddlOperPlano.SelectedValue) ? int.Parse(ddlOperPlano.SelectedValue) : 0);
            int idPlan = (!string.IsNullOrEmpty(ddlPlano.SelectedValue) ? int.Parse(ddlPlano.SelectedValue) : 0);

            //Apenas se tiver sido escolhido algum procedimento
            if (idProc != 0)
            {
                int? procAgrupador = (int?)null; // Se for procedimento de alguma operadora, verifica qual o id do procedimento agrupador do mesmo
                if (!string.IsNullOrEmpty(ddlOperPlano.SelectedValue))
                    procAgrupador = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(idProc).ID_AGRUP_PROC_MEDI_PROCE;

                AuxiliCalculos.ValoresProcedimentosMedicos ob = AuxiliCalculos.RetornaValoresProcedimentosMedicos((idProc), idOper, idPlan);
                txtValor.Text = ob.VL_CALCULADO.ToString("N2"); // Insere o valor Calculado
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
        #endregion
        #region gravar registro
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


                if (grdProcedimentos.Rows.Count != 0)
                {
                    foreach (GridViewRow li in grdProcedimentos.Rows)
                    {
                        DropDownList ddlContrat;
                        DropDownList ddlSolic;
                        DropDownList ddlPlan;
                        DropDownList ddlProced;
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
                            AbreModalPadrao("AbreModalProcedHorar();");
                            return;
                        }
                        if (string.IsNullOrEmpty(txtQtp.Text))
                        {
                            AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Informe a quantidade de procedimentos");
                            txtQtp.Focus();
                            AbreModalPadrao("AbreModalProcedHorar();");
                            return;
                        }
                        if (coPaci == 0 || coPaci <= 0)
                        {
                            AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "Para confirmar, é necessário selecionar um paciente");
                            AbreModalPadrao("AbreModalProcedHorar();");
                            return;
                        }

                        TBS174_AGEND_HORAR agend = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(coAgend);

                        if ((String.IsNullOrEmpty(txtIdItem.Text)))
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
                        tbs386.QT_SESSO = TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaPelaAgendaEProcedimento(coAgend, TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProced.SelectedValue)).ID_PROC_MEDI_PROCE).Count; //Conta quantos itens existem na lista para este mesmo e agenda
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

                        if (txtCart.Enabled == true && !String.IsNullOrEmpty(txtCart.Text))
                        {
                            var tb07 = TB07_ALUNO.RetornaPeloCoAlu(coPaci);
                            tb07.NU_CARTAO_SAUDE = txtCart.Text;
                            TB07_ALUNO.SaveOrUpdate(tb07, true);
                        }

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
        #endregion

        #endregion

    }
}

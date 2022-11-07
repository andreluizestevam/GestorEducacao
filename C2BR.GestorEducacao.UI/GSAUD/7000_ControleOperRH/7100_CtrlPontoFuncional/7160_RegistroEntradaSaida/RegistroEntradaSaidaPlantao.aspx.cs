//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE RH
// OBJETIVO: REGISTRO DO PONTO DO COLABORADOR
// DATA DE CRIAÇÃO: 07/05/2014
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//------------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 11/08/2014| Maxwell Almeida            | Criação da funcionalidade para alteração das informações de ponto do colaborador

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
using C2BR.GestorEducacao.UI.Library;
using System.Data.Objects;
using C2BR.GestorEducacao.UI.App_Masters;
using System.Net;

namespace C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7160_RegistroEntradaSaida
{
    public partial class RegistroEntradaSaidaPlantao : System.Web.UI.Page
    {
        
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Variaveis

        string ipM;
        int auxC;

        #endregion

        #region eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidades();
                CarregaLocal();
                carregaTiposPlant();
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //    --------> criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

         void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            bool selecPla = false;
            RegistroLog log = new RegistroLog();

            ///Varre toda a gride de Solicitações e salva na tabela TB114_FARDMAT 
            foreach (GridViewRow linha in grdAgendaPlantoes.Rows)
            {
                CheckBox grAg = ((CheckBox)linha.Cells[0].FindControl("chkselect"));

                if (grAg.Checked)
                {
					TextBox txtdtITes = (TextBox)linha.Cells[8].FindControl("txtDataRealiIni");
                    TextBox txthrITes = (TextBox)linha.Cells[8].FindControl("txtHoraRealiIni");
				if((!string.IsNullOrEmpty(txtdtITes.Text)) && (!string.IsNullOrEmpty(txthrITes.Text)))
				{
					TextBox txtdtFimTes = (TextBox)linha.Cells[8].FindControl("txtDataRealiFim");
					TextBox txthrFimTes = (TextBox)linha.Cells[8].FindControl("txtHoraRealiFim");
					
						if ((!string.IsNullOrEmpty(txtdtFimTes.Text)) && (!string.IsNullOrEmpty(txthrFimTes.Text)))
						{						
								HiddenField hdfPla = ((HiddenField)linha.Cells[0].FindControl("hidcoPla"));
                                int cop = (!string.IsNullOrEmpty(hdfPla.Value) ? int.Parse(hdfPla.Value) : 0);
                                string Situ = (((DropDownList)linha.Cells[9].FindControl("ddlSitu")).SelectedValue);
				
                                //DateTime dt = DateTime.Parse(txtData.Text);
                                //string dtt = dt.ToString("yyyy/MM/dd");
                                //DateTime dttConv = DateTime.Parse(dtt);
				
								var tb159ob = (from tb159i in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
												where (tb159i.CO_AGEND_PLANT_COLAB == cop)
                                                //&& (EntityFunctions.TruncateTime(tb159i.DT_INICIO_PREV) == EntityFunctions.TruncateTime(dttConv))
												select tb159i).FirstOrDefault();
												
								#region Atribui os Dados antigos às Variáveis para criação de Log
									
									DateTime? iniReal = tb159ob.DT_INICIO_REAL;
									DateTime? FimReal = tb159ob.DT_TERMIN_REAL;
								
								#endregion
				
								#region Trata Data e Hora inserida para gravar na Coluna de Data de Término
				
								TextBox txtdt = (TextBox)linha.Cells[8].FindControl("txtDataRealiFim");
								TextBox txthr = (TextBox)linha.Cells[8].FindControl("txtHoraRealiFim");
				
									DateTime dtT = Convert.ToDateTime(txtdt.Text);
                                    //DateTime dtH = Convert.ToDateTime(txthr.Text);
				
									int dd = int.Parse(txthr.Text.Substring(0, 2));
									int mm = int.Parse(txthr.Text.Substring(3, 2));
				
									DateTime totDH = dtT.AddHours(dd).AddMinutes(mm);
				
									DateTime dtr = totDH;
				
								    #endregion
				
									#region Trata Data e Hora Inserida para Gravar na Coluna de Data de Início
				
									TextBox txtdtI = (TextBox)linha.Cells[8].FindControl("txtDataRealiIni");
									TextBox txthrI = (TextBox)linha.Cells[8].FindControl("txtHoraRealiIni");
				
									DateTime dtTI = Convert.ToDateTime(txtdtI.Text);
                                    //DateTime dtHI = Convert.ToDateTime(txthrI.Text);
				
									int ddI = int.Parse(txthrI.Text.Substring(0, 2));
									int mmI = int.Parse(txthrI.Text.Substring(3, 2));
				
									DateTime totDHI = dtTI.AddHours(ddI).AddMinutes(mmI);
				
									DateTime dtrI = totDHI;
				
									#endregion
				
									tb159ob.DT_INICIO_REAL = totDHI;
									tb159ob.DT_TERMIN_REAL = totDH;
									tb159ob.FL_PONTO_COLAB_REGIS = "S";
                                    tb159ob.CO_SITUA_AGEND = Situ;
				
									GestorEntities.SaveOrUpdate(tb159ob);
									CurrentPadraoCadastros.CurrentEntity = tb159ob;
                                    log.RegistroLOG(tb159ob, RegistroLog.ACAO_EDICAO);

                                    //Valida se o usuário selecionou ou não um registro
                                    if (selecPla == false)
                                        selecPla = true;


                                    if ((!iniReal.HasValue) && (!FimReal.HasValue))
                                    {
                                        //Salva as Informações no Log tbs175
                                        TBS175_LOG_ALTER_AGENDA tbs175 = RetornaEntidade();

                                        tbs175.CO_EMP = LoginAuxili.CO_EMP;
                                        tbs175.CO_COL = LoginAuxili.CO_COL;
                                        tbs175.TB159_AGENDA_PLANT_COLABOR = TB159_AGENDA_PLANT_COLABOR.RetornaPelaChavePrimaria(cop);
                                        tbs175.DT_LOG = DateTime.Now;
                                        tbs175.DT_INICIO_ANTES = iniReal.Value;
                                        tbs175.DT_INICIO_DEPOIS = totDHI;
                                        tbs175.DT_FINAL_ANTES = FimReal.Value;
                                        tbs175.DT_FINAL_DEPOIS = totDH;
                                        tbs175.NR_IP_ACESS_LOG_ALTER_AGENDA = Request.UserHostAddress;

                                        CurrentPadraoCadastros.CurrentEntity = tbs175;
                                    }
						}
						else
							AuxiliPagina.EnvioMensagemErro(this.Page, "Favor preencher os campos de Data e Hora de Fim do Plantão");
					}
					else
						AuxiliPagina.EnvioMensagemErro(this.Page, "Favor preencher os campos de Data e Hora de Início do Plantão");
                }
            }

             //Valida se o usuário selecionou algum registro
            if (selecPla == false)
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar ao menos um registro ao salvar!");
		}
        
		/// Método de retorno da entidade informada
        /// </summary>
         /// <returns>Entidade TBS175_LOG_ALTER_AGENDA</returns>
        private TBS175_LOG_ALTER_AGENDA RetornaEntidade()
        {
            TBS175_LOG_ALTER_AGENDA tbs175 = TBS175_LOG_ALTER_AGENDA.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tbs175 == null) ? new TBS175_LOG_ALTER_AGENDA() : tbs175;
        }
		
		
        #endregion

        #region Carregamentos

        /// <summary>
        /// Carrega as Unidades
        /// </summary>
        private void CarregaUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega os Departamentos
        /// </summary>
        private void CarregaLocal()
        {
            int coEmp = (ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0);
            AuxiliCarregamentos.CarregaDepartamentos(ddlLocal, coEmp, true);
        }

        /// <summary>
        /// Carrega os tipos de plantões
        /// </summary>
        private void carregaTiposPlant()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaTiposPlantoes(ddlTipoPlantao, coEmp, true);
        }

        /// <summary>
        /// Carrega a grid de informações de ponto dos plantões
        /// </summary>
        private void CarregaGrid()
        {
            int coDept = int.Parse(ddlLocal.SelectedValue);
            int coEmp = int.Parse(ddlUnidade.SelectedValue);
			int tpPl = int.Parse(ddlTipoPlantao.SelectedValue);

            //Trata as datas para poder compará-las com as informações no banco
            DateTime dt = DateTime.Parse(IniPeri.Text);
            string dataConver = dt.ToString("yyyy/MM/dd");
            DateTime dtInici = DateTime.Parse(dataConver);

            //Trata as datas para poder compará-las com as informações no banco
            DateTime dtf = DateTime.Parse(FimPeri.Text);
            string dataConverF = dtf.ToString("yyyy/MM/dd");
            DateTime dtFim = DateTime.Parse(dataConverF);
            

            var res = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb159.TB03_COLABOR.CO_COL equals tb03.CO_COL
					   join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals tb63.CO_ESPECIALIDADE into l1
					   from l4 in l1.DefaultIfEmpty()

						where 
						   (coEmp != 0 ? tb159.CO_EMP_AGEND_PLANT == coEmp : 0 == 0 )
						&& (tpPl != 0 ? tb159.ID_TIPO_PLANT == tpPl : 0 == 0 )
                        && (coDept != 0 ? tb159.TB14_DEPTO.CO_DEPTO == coDept : 0 == 0 )
                        //&& (EntityFunctions.TruncateTime(tb159.DT_INICIO_PREV) == EntityFunctions.TruncateTime(dataFuncio))
                        && ((EntityFunctions.TruncateTime(tb159.DT_INICIO_PREV) >= EntityFunctions.TruncateTime(dtInici)) && (EntityFunctions.TruncateTime(tb159.DT_TERMIN_PREV) <= EntityFunctions.TruncateTime(dtFim)))

                       select new GrdProfissionais
                       {
                           NO_COL_RECEB = tb03.NO_COL,
                           APEL_COL = tb03.NO_APEL_COL,
						   ESPEC = l4.NO_SIGLA_ESPECIALIDADE,
                           MATR_COL = tb03.CO_MAT_COL,
						   CHP = tb03.QT_HORAS_PLANT,
						   DIP = tb03.QT_HORAS_DESCA,
						   INI_DT_HR = tb159.DT_INICIO_PREV,
                           FIM_DT_HR = tb159.DT_TERMIN_PREV,
                           CO_AGEND = tb159.CO_AGEND_PLANT_COLAB,
						   dtreal = tb159.DT_TERMIN_REAL,						   
                           dtIni = tb159.DT_INICIO_REAL,
                           situ = tb159.CO_SITUA_AGEND,
                       }).OrderByDescending(w => w.INI_DT_HR).ToList();

            grdAgendaPlantoes.DataSource = res;
            grdAgendaPlantoes.DataBind();

            //Carrega as situações e seleciona a devida
            foreach (GridViewRow li in grdAgendaPlantoes.Rows)
            {
                DropDownList ddl = (((DropDownList)li.Cells[9].FindControl("ddlSitu")));
                string situ = (((HiddenField)li.Cells[9].FindControl("hidCoSitu")).Value);
                ddl.SelectedValue = situ;
            }
        }

        #region Classes de Saída

        public class GrdProfissionais
        {
            //Carrega as informações do Colaborador
			public int CO_AGEND { get; set; }
            public string MATR_COL { get; set; }
            public string NO_COL
            {
                get
                {
                    string maCol = this.MATR_COL.PadLeft(6, '0');
                    string noCol = (this.NO_COL_RECEB.Length > 25 ? this.NO_COL_RECEB.Substring(0, 25) + "..." : this.NO_COL_RECEB);
                    return maCol + " - " + noCol;
                }
            }
            public string NO_COL_RECEB { get; set; }
            public string APEL_COL { get; set; }
            public string ESPEC { get; set; }
			public int? CHP { get; set; }
			public int? DIP { get; set; }

            //Tratamento da Data e Hora Prevista de Início
			public DateTime INI_DT_HR { get; set; }
			public string dtPrevIniV
			{
				get
				{
					return this.INI_DT_HR.ToString("dd/MM/yy");
				}
			}
			public string hrPrevIniV
			{
				get
				{
					return this.INI_DT_HR.ToString("HH:mm");
				}
			}

            //Tratamento da Data e Hora Prevista de Término
			public DateTime FIM_DT_HR { get; set; }
            public string dtPrevFimV
            {
                get
                {
                    return this.FIM_DT_HR.ToString("dd/MM/yy");
                }
            }
            public string hrPrevFimV
            {
                get
                {
                    return this.FIM_DT_HR.ToString("HH:mm");
                }
            }
            
            //Concatena Data e Hora prevista para Início e Término
            public string DtHrPrevConcatV
            {
                get
                {
                    return this.dtPrevIniV + " " + this.hrPrevIniV + " - " + this.dtPrevFimV + " " + this.hrPrevFimV;
                }
            }

            //Carregamento da Data e Hora de Início da Realização do Plantão
            public DateTime? dtIni { get; set; }
			public string dtIniV
			{
				get
				{
                    return (this.dtIni.HasValue ? this.dtIni.Value.ToString("dd/MM/yyyy") : "");
				}
			}
			public string hrIniV
			{
				get
				{
					return (this.dtIni != null ? this.dtIni.Value.ToString("HH:mm") : "");
				}
			}

            //Carregamento da Data e Hora de Término da Realização do Plantão
            public DateTime? dtreal { get; set; }
			public string dtRealV 
			{
				get
				{
					return (this.dtreal.HasValue ? this.dtreal.Value.ToString("dd/MM/yyyy") : "");
				}
			}
			public string hrRealV 
			{
				get
				{
                    return (this.dtreal != null ? this.dtreal.Value.ToString("HH:mm") : "" );
				}
			}

            //Situação
            public string situ { get; set; }
			
        }

        private void CarregaDropSitu(DropDownList ddl, string situ)
        {

        }

        #endregion

        #endregion

        #region Funções de Campo

        protected void OnClick_PesqGrid(object sender, EventArgs e)
        {
			CarregaGrid();
        }

        /// <summary>
        /// Ocorre quando o usuário clica no checkbox da grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkselect_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            // Valida se a grid de atividades possui algum registro
            if (grdAgendaPlantoes.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdAgendaPlantoes.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("chkselect");

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID == atual.ClientID)
                    {
                        //Responsável por habilitar ou desabilitar os campos conforme o usuário seleciona ou desmarca.
                        if (chk.Checked)
                        {
                            (((TextBox)linha.Cells[7].FindControl("txtDataRealiIni")).Enabled) = true;
                            (((TextBox)linha.Cells[7].FindControl("txtHoraRealiIni")).Enabled) = true;
                            (((TextBox)linha.Cells[8].FindControl("txtDataRealiFim")).Enabled) = true;
                            (((TextBox)linha.Cells[8].FindControl("txtHoraRealiFim")).Enabled) = true;
                            (((DropDownList)linha.Cells[9].FindControl("ddlSitu")).Enabled) = true;
                        }
                        else
                        {
                            (((TextBox)linha.Cells[7].FindControl("txtDataRealiIni")).Enabled) = false;
                            (((TextBox)linha.Cells[7].FindControl("txtHoraRealiIni")).Enabled) = false;
                            (((TextBox)linha.Cells[8].FindControl("txtDataRealiFim")).Enabled) = false;
                            (((TextBox)linha.Cells[8].FindControl("txtHoraRealiFim")).Enabled) = false;
                            (((DropDownList)linha.Cells[9].FindControl("ddlSitu")).Enabled) = false;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
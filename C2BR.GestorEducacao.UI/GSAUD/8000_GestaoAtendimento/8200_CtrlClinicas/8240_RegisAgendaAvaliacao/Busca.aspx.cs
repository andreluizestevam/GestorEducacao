//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: GERENCIAMENTO DE USUÁRIOS DO GE
// OBJETIVO: MANUTENÇÃO DE TIPO DE PERFIL DE ACESSO.
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 09/12/2014| Maxwell Almeida           | Criação da funcionalidade Busca por Operadoras

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8240_RegisAgendaAvaliacao
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CarregaOperadorasPlanSaude();
                AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, true, true, false, true);
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_AGEND_AVALI" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TIPO",
                HeaderText = "TP",
            });


            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "PACIENTE",
                HeaderText = "PACIENTE"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DTHR",
                HeaderText = "DT NASC"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TRATA_CLASS",
                HeaderText = "PROF"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "OPER",
                HeaderText = "OPERAD"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DIAGNOSTICO",
                HeaderText = "PRÉ-DIAGNÓSTICO"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            try
            {
                int IdOperadora = (ddlOperadora.SelectedValue != "" ? int.Parse(ddlOperadora.SelectedValue) : 0);

                DateTime? dtIniCons, dtFimCons;
                dtIniCons = (!string.IsNullOrEmpty(IniPeriCon.Text) ? DateTime.Parse(IniPeriCon.Text) : (DateTime?)null);
                dtFimCons = (!string.IsNullOrEmpty(FimPeriCon.Text) ? DateTime.Parse(FimPeriCon.Text) : (DateTime?)null);

                DateTime? dtIniCad, dtFimCad;
                dtIniCad = (!string.IsNullOrEmpty(IniPeriCad.Text) ? DateTime.Parse(IniPeriCad.Text) : (DateTime?)null);
                dtFimCad = (!string.IsNullOrEmpty(FimPeriCad.Text) ? DateTime.Parse(FimPeriCad.Text) : (DateTime?)null);

                DateTime? dtNascIni, dtNascFim;
                dtNascIni = (!string.IsNullOrEmpty(txtDataNascInicio.Text) ? DateTime.Parse(txtDataNascInicio.Text) : (DateTime?)null);
                dtNascFim = (!string.IsNullOrEmpty(txtDatanascFim.Text) ? DateTime.Parse(txtDatanascFim.Text) : (DateTime?)null);
                
                var resultado = (from tbs372 in TBS372_AGEND_AVALI.RetornaTodosRegistros()
                                 join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs372.CO_ALU equals tb07.CO_ALU
                                 where (ddlTipoPesquisa.SelectedValue != "0" ? tbs372.FL_TIPO_AGENDA == ddlTipoPesquisa.SelectedValue : 0 == 0)
                                 & (!string.IsNullOrEmpty(txtNomePaciente.Text) ? tb07.NO_ALU.Contains(txtNomePaciente.Text) : 0 == 0)
                                 & (IdOperadora != 0 ? tbs372.TB250_OPERA.ID_OPER == IdOperadora : 0 == 0)
                                 & (ddlSituacao.SelectedValue != "0" ? tbs372.CO_SITUA == ddlSituacao.SelectedValue : "" == "")
                                 & (chkFisio.Checked ? tbs372.CO_INDIC_PROFI_FISIO == "S" : true)
                                 & (chkFono.Checked ? tbs372.CO_INDIC_PROFI_FONOA == "S" : true)
                                 & (chkTeraOcup.Checked ? tbs372.CO_INDIC_PROFI_TEROC == "S" : true)
                                 & (chkPsico.Checked ? tbs372.CO_INDIC_PROFI_PSICO == "S" : true)
                                 & (chkOutro.Checked ? tbs372.CO_INDIC_PROFI_OUTRO == "S" : true)

                                 & (chkPscicPeda.Checked ? tbs372.CO_INDIC_PROFI_PEG == "S" : true)
                                 & (chkOdonto.Checked ? tbs372.CO_INDIC_PROFI_ODO == "S" : true)
                                 & (chkMed.Checked ? tbs372.CO_INDIC_PROFI_MEDICO == "S" : true)
                                 
                                 & (dtNascIni.HasValue && tb07.DT_NASC_ALU.HasValue ? EntityFunctions.TruncateTime(tb07.DT_NASC_ALU) >= EntityFunctions.TruncateTime(dtNascIni.Value) : 0 == 0)
                                 & (dtNascFim.HasValue && tb07.DT_NASC_ALU.HasValue ? EntityFunctions.TruncateTime(tb07.DT_NASC_ALU) <= EntityFunctions.TruncateTime(dtNascFim.Value) : 0 == 0)
                                 & (tb07.FL_LIST_ESP != "N")//Ou seja == A(mbos) ou S(im)

                                 & (ddlTipoPesquisa.SelectedValue == "0" || ddlTipoPesquisa.SelectedValue == "L" ?
                                    (
                                      (dtIniCad.HasValue ? EntityFunctions.TruncateTime(tbs372.DT_CADAS) >= EntityFunctions.TruncateTime(dtIniCad.Value) : 0 == 0)
                                    & (dtFimCad.HasValue ? EntityFunctions.TruncateTime(tbs372.DT_CADAS) <= EntityFunctions.TruncateTime(dtFimCad.Value) : 0 == 0)
                                    ) : 0 == 0)

                                 & (ddlTipoPesquisa.SelectedValue == "0" || ddlTipoPesquisa.SelectedValue == "C" ? 
                                    (
                                      (dtIniCons.HasValue && tbs372.DT_AGEND.HasValue ? EntityFunctions.TruncateTime(tbs372.DT_AGEND) >= EntityFunctions.TruncateTime(dtIniCons.Value) : 0 == 0)
                                    & (dtFimCons.HasValue && tbs372.DT_AGEND.HasValue ? EntityFunctions.TruncateTime(tbs372.DT_AGEND) <= EntityFunctions.TruncateTime(dtFimCons.Value) : 0 == 0)
                                    ) : 0 == 0)

                                 select new saida
                                 {
                                     ID_AGEND_AVALI = tbs372.ID_AGEND_AVALI,
                                     CO_TIPO = tbs372.FL_TIPO_AGENDA,
                                     TIPO = (tbs372.FL_TIPO_AGENDA == "C" ? "CO" : "LE"),
                                     DT = tb07.DT_NASC_ALU,
                                     HR = tbs372.HR_AGEND,
                                     FIS = tbs372.CO_INDIC_PROFI_FISIO,
                                     OUT = tbs372.CO_INDIC_PROFI_OUTRO,
                                     FON = tbs372.CO_INDIC_PROFI_FONOA,
                                     PSI = tbs372.CO_INDIC_PROFI_PSICO,
                                     TOC = tbs372.CO_INDIC_PROFI_TEROC,

                                    ODO = tbs372.CO_INDIC_PROFI_ODO,
                                   
                                     MED = tbs372.CO_INDIC_PROFI_MEDICO,

                                     NO_ALU = tb07.NO_ALU,
                                     CO_SITUA = tbs372.CO_SITUA,
                                     //DEF = tbs372. tb07.TBS387_DEFIC.NM_DEFIC == null ? "-" : tb07.TBS387_DEFIC.NM_DEFIC,
                                     OPER = tbs372.TB250_OPERA.NM_SIGLA_OPER == null ? "-" : tbs372.TB250_OPERA.NM_SIGLA_OPER == "" ? "-" : tbs372.TB250_OPERA.NM_SIGLA_OPER,
                                     DIAGN = tbs372.DE_OBSER_NECES != "" ? tbs372.DE_OBSER_NECES : " - "
                                 }).OrderByDescending(o => o.ID_AGEND_AVALI).DistinctBy(p => p.NO_ALU.Trim()).OrderByDescending(w => w.TIPO).OrderBy(w => w.NO_ALU).ToList();
                                 //}).ToList().OrderByDescending(w => w.TIPO).OrderBy(w => w.NO_ALU).ToList();



                //tbs372.CO_INDIC_PROFI_FISIO = (chkFisio.Checked ? "S" : "N");
                //tbs372.CO_INDIC_PROFI_FONOA = (chkFono.Checked ? "S" : "N");
                //tbs372.CO_INDIC_PROFI_TEROC = (chkTeraOcup.Checked ? "S" : "N");
                //tbs372.CO_INDIC_PROFI_PSICO = (chkPsico.Checked ? "S" : "N");
                //tbs372.CO_INDIC_PROFI_OUTRO = (chkOutro.Checked ? "S" : "N");

                CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
            }
            catch (Exception ex)
            {

                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
            }

        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_AGEND_AVALI"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        public class saida
        {
            public int ID_AGEND_AVALI { get; set; }
            public string CO_TIPO { get; set; }
            public string TIPO { get; set; }
            public string DEF
            {

                get
                {
                    var res = (from tbs373 in TBS373_AGEND_AVALI_ITENS.RetornaTodosRegistros()
                               where tbs373.TBS372_AGEND_AVALI.ID_AGEND_AVALI == ID_AGEND_AVALI
                               select new
                               {
                                 
                                   DE = tbs373.TBS387_DEFIC.NM_SIGLA_DEFIC,

                               }).ToList();
                    if (res.Count > 0)
                    {
                        string NOME = "-";
                        foreach (var item in res)
                        {
                            if (item.DE == null)
                            {
                                return "-";
                            }
                            return item.DE.ToString();
                            //NOME = TBS387_DEFIC.RetornaPelaChavePrimaria(item.TBS387_DEFIC.ID_DEFIC).NM_DEFIC == null ? "-" : TBS387_DEFIC.RetornaPelaChavePrimaria(item.TBS387_DEFIC.ID_DEFIC).NM_SIGLA_DEFIC; //.Where( a=> a.ID_DEFIC == ).;
                        }
                        return NOME;
                    }
                    else
                        return "-";



                }

            }
            public string DIAGN { get; set; }
            public string DIAGNOSTICO
            {
                get
                {
                    return (this.DIAGN.Length > 45 ? this.DIAGN.Substring(0, 45) + "..." : this.DIAGN);
                }
            }
            public string OPER { get; set; }
            public DateTime? DT { get; set; }
            public string HR { get; set; }
            public string DTHR
            {
                get
                {
                    //if (this.TIPO == "Consulta")
                        return this.DT.Value.ToString("dd/MM/yy"); //+ " - " + this.HR;
                    //else
                    //    return " - ";
                }
            }
            public string PACIENTE
            {
                get
                {
                    return (this.NO_ALU.Length > 20 ? this.NO_ALU.Substring(0, 20) + "..." : this.NO_ALU);
                }
            }
            public string NO_ALU { get; set; }
            public string PSI { get; set; }
            public string FON { get; set; }
            public string TOC { get; set; }
            public string FIS { get; set; }
            public string PEG { get; set; }
            public string OUT { get; set; }

            public string ODO { get; set; }
            public string MED { get; set; }

            public string TRATA_CLASS
            {
                get
                {
                    string s = "";
                    if (this.PSI == "S")
                        s += (!string.IsNullOrEmpty(s) ? "; " : "") + "PSI";

                    if (this.FON == "S")
                        s += (!string.IsNullOrEmpty(s) ? "; " : "") + "FON";

                    if (this.TOC == "S")
                        s += (!string.IsNullOrEmpty(s) ? "; " : "") + "TOC";

                    if (this.FIS == "S")
                        s += (!string.IsNullOrEmpty(s) ? "; " : "") + "FIS";

                    if (this.PEG == "S")
                        s += (!string.IsNullOrEmpty(s) ? "; " : "") + "PEG";

                    if (this.OUT == "S")
                        s += (!string.IsNullOrEmpty(s) ? ";" : "") + "OUT";

                    if (this.ODO == "S")
                        s += (!string.IsNullOrEmpty(s) ? "; " : "") + "ODO";

                    if (this.MED == "S")
                        s += (!string.IsNullOrEmpty(s) ? ";" : "") + "MED";

                    return (!string.IsNullOrEmpty(s) ? s : " - ");
                }
            }
            public string CO_SITUA { get; set; }
            public string CO_SITUA_V
            {
                get
                {
                    string e = "";
                    switch (this.CO_SITUA)
                    {
                        case "A":
                            e = "Em Aberto";
                            break;
                        case "C":
                            e = "Cancelado";
                            break;
                        case "E":
                            e = "Encaminhado";
                            break;
                        case "R":
                            e = "Realizado";
                            break;
                        default:
                            e = " - ";
                            break;
                    }
                    return e;
                }
            }

        }

        #endregion

        #region Carregamento DropDown

        private void CarregaOperadorasPlanSaude()
        {
            try
            {
                AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, true);
            }
            catch (Exception ex)
            {

                AuxiliPagina.EnvioMensagemErro(this.Page, ex.Message);
            }


        }

        #endregion

        #region Funções de Campo

        protected void ddlTipoPesquisa_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            liPeriCad.Visible = 
            liPeriCon.Visible = false;

            if (ddlTipoPesquisa.SelectedValue == "L") //Se for lista de espera, essas são as únicas opções
            {
                ddlSituacao.Items.Clear();
                ddlSituacao.Items.Insert(0, new ListItem("Cancelado", "C"));
                ddlSituacao.Items.Insert(0, new ListItem("Em Aberto", "A"));
                ddlSituacao.Items.Insert(0, new ListItem("Todos", "0"));
                liPeriCad.Visible = true;
            }
            else
            {
                ddlSituacao.Items.Clear();
                ddlSituacao.Items.Insert(0, new ListItem("Realizado", "R"));
                ddlSituacao.Items.Insert(0, new ListItem("Cancelado", "C"));
                ddlSituacao.Items.Insert(0, new ListItem("Encaminhado", "E"));
                ddlSituacao.Items.Insert(0, new ListItem("Em Aberto", "A"));
                ddlSituacao.Items.Insert(0, new ListItem("Todos", "0"));
                liPeriCon.Visible = true;

                if (ddlTipoPesquisa.SelectedValue == "0")
                    liPeriCad.Visible = true;
            }
        }

        #endregion
    }
}
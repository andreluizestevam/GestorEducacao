//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PS - Portal 
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE ALUNOS
// OBJETIVO: CADASTRAMENTO DE ALUNOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 06/03/2013|    Julio Gleisson          | Copia da tela 3611_CadastramentoAlunos
//           |                            | 
//           |                            | 

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

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3106_CadastramentoUsuariosSimp
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaDeficiencias();
                CarregaAnoOrigem();

                AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, true);
                AuxiliCarregamentos.CarregaRestricoesAtendimento(ddlRestrAtend, true);
                CarregaPlano();
            }
        }
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_ALU" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_NIRE",
                HeaderText = "Nº CONTROLE"
            });


            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_ALU",
                HeaderText = "Nome"
            });

            BoundField bfRealizado1 = new BoundField();
            bfRealizado1.DataField = "DT_NASC_ALU";
            bfRealizado1.HeaderText = "Dt Nasc";
            bfRealizado1.ItemStyle.CssClass = "codCol";
            bfRealizado1.DataFormatString = "{0:d}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado1);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TP_DEF",
                HeaderText = "Deficiência"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_RESTR",
                HeaderText = "RESTR ATEND"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SITUA_V",
                HeaderText = "SITUAÇÃO"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            if (txtNire.Text == "_________")
            {
                txtNire.Text = "";
            }

            int nuNire = 0;
            if (txtNire.Text != "" && !int.TryParse(txtNire.Text, out nuNire))
                return;
            DateTime dataInicio = DateTime.MinValue;
            DateTime dataFim = DateTime.MinValue;
            DateTime? dataVerifIni = null;
            DateTime? dataVerifFim = null;
            string deficiencia = ddlDeficiencia.SelectedValue;
            string anoOri = ddlAnoOri.SelectedValue;

            DateTime.TryParse(txtPeriodoDe.Text, out dataInicio);
            DateTime.TryParse(txtPeriodoAte.Text, out dataFim);

            dataVerifIni = dataInicio == DateTime.MinValue ? null : (DateTime?)dataInicio;
            dataVerifFim = dataFim == DateTime.MinValue ? null : (DateTime?)dataFim;
            int oper = (!string.IsNullOrEmpty(ddlOperadora.SelectedValue) ? int.Parse(ddlOperadora.SelectedValue) : 0);
            int plan = (!string.IsNullOrEmpty(ddlPlano.SelectedValue) ? int.Parse(ddlPlano.SelectedValue) : 0);
            int restr = (!string.IsNullOrEmpty(ddlRestrAtend.SelectedValue) ? int.Parse(ddlRestrAtend.SelectedValue) : 0);
            int def = (!string.IsNullOrEmpty(ddlDeficiencia.SelectedValue) ? int.Parse(ddlDeficiencia.SelectedValue) : 0);
            var coAlu = !String.IsNullOrEmpty(ddlNome.SelectedValue) ? int.Parse(ddlNome.SelectedValue) : 0;
            string situacao = ddlSituacaoAlu.SelectedValue;

            var resultado = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                             join tb47 in TB47_CTA_RECEB.RetornaTodosRegistros() on tb07.CO_ALU equals tb47.CO_ALU into l1
                             from ls in l1.DefaultIfEmpty()
                             where tb07.TB25_EMPRESA1.CO_EMP == LoginAuxili.CO_EMP
                             && (nuNire != 0 ? tb07.NU_NIRE == nuNire : 0 == 0)
                             && (coAlu != 0 ? tb07.CO_ALU == coAlu : 0 == 0)
                             && (txtNome.Text != "" ? tb07.NO_ALU.Contains(txtNome.Text) : 0 == 0)
                             && (txtCpf.Text != "" ? tb07.NU_CPF_ALU.Equals(txtCpf.Text.Replace(".", "").Replace("-", "")) : txtCpf.Text == "")
                             && (def != 0 ? tb07.TBS387_DEFIC.ID_DEFIC == def : 0 == 0)
                             && (dataVerifIni != null ? tb07.DT_NASC_ALU.Value >= dataVerifIni : 0 == 0)
                             && (dataVerifFim != null ? tb07.DT_NASC_ALU.Value <= dataVerifFim : 0 == 0)
                             && (anoOri == "" || anoOri == "-1" ? 0 == 0 : tb07.CO_ANO_ORI == anoOri)
                             && (oper != 0 ? tb07.TB250_OPERA.ID_OPER == oper : 0 == 0)
                             && (plan != 0 ? tb07.TB251_PLANO_OPERA.ID_PLAN == plan : 0 == 0)
                             && (situacao != "0" ? tb07.CO_SITU_ALU == situacao : 0 == 0)
                                 //Se for todos ,trás todos, se for Nenhum, trás os nulos, se for escolhido algum, seleciona o próprio
                             && (restr != 0 ? (ddlRestrAtend.SelectedValue == "N" ? tb07.TBS379_RESTR_ATEND == null : tb07.TBS379_RESTR_ATEND.ID_RESTR_ATEND == restr) : 0 == 0)
                             && (chkPendDocs.Checked ? tb07.FL_PENDE_DOCUM == "S" : 0 == 0)
                             && (chkPenPlano.Checked ? tb07.FL_PENDE_PLANO_CONVE == "S" : 0 == 0)
                             && (chkPenFinancGER.Checked ? tb07.FL_PENDE_FINAN_GER == "S" : 0 == 0)
                                 //Se tiver sido marcado para filtrar os pacientes com pendências financeiras
                             && (chkPenFinancCAR.Checked ? ls.IC_SIT_DOC == "A" : 0 == 0)
                             //&& tb07.FL_LIST_ESP != "S"//ou seja == A(mbos) ou N(ão)
                             select new listaAlunos
                             {
                                 CO_ALU = tb07.CO_ALU,
                                 NO_ALU = tb07.NO_ALU,
                                 NU_NIRER = tb07.NU_NIRE,
                                 NU_NIS = tb07.NU_NIS,
                                 DT_NASC_ALU = tb07.DT_NASC_ALU,
                                 CO_SITUA = tb07.CO_SITU_ALU,
                                 CO_RESTR = (tb07.TBS379_RESTR_ATEND != null ? tb07.TBS379_RESTR_ATEND.CO_RESTR : " - "),
                                 TP_DEF = (tb07.TBS387_DEFIC != null ? tb07.TBS387_DEFIC.NM_SIGLA_DEFIC : " - "),
                             }).OrderBy(a => a.NO_ALU);

            if (resultado != null && resultado.Count() > 0)
                CurrentPadraoBuscas.GridBusca.DataSource = resultado;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_ALU"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregadores
        /// <summary>
        /// Carrega todos os tipos de deficiências dos alunos
        /// </summary>
        private void CarregaDeficiencias()
        {
            AuxiliCarregamentos.CarregaDeficienciasNova(ddlDeficiencia, true);
        }

        /// <summary>
        /// Carrega os anos de origem da tabela de alunos do sistema
        /// </summary>
        private void CarregaAnoOrigem()
        {
            ddlAnoOri.Items.Clear();
            ddlAnoOri.Items.AddRange(AuxiliBaseApoio.AnosOrigemDDL(true, true));
        }

        private void CarregaPlano()
        {
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlano, ddlOperadora, true);
        }

        protected void ddlOperadora_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPlano();
        }

        protected void imgbPesqPacNome_OnClick(object sender, EventArgs e)
        {
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where tb07.NO_ALU.Contains(txtNome.Text) && tb07.FL_LIST_ESP != "S" && tb07.TB25_EMPRESA1.CO_EMP == LoginAuxili.CO_EMP
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            if (res != null)
            {
                ddlNome.DataTextField = "NO_ALU";
                ddlNome.DataValueField = "CO_ALU";
                ddlNome.DataSource = res;
                ddlNome.DataBind();
            }

            ddlNome.Items.Insert(0, new ListItem("Selecione", ""));

            OcultarPesquisa(true);
        }

        protected void imgbVoltarPesq_OnClick(object sender, EventArgs e)
        {
            ddlNome.Items.Clear();

            OcultarPesquisa(false);
        }

        private void OcultarPesquisa(bool ocultar)
        {
            txtNome.Visible =
            imgbPesqPacNome.Visible = !ocultar;
            ddlNome.Visible =
            imgbVoltarPesq.Visible = ocultar;
        }

        #endregion

        #region Classes
        /// <summary>
        /// Classe com os campos necessários para carregar a grid de buscas
        /// </summary>
        private class listaAlunos
        {
            public string CO_RESTR { get; set; }
            public int CO_ALU { get; set; }
            public string NO_ALU { get; set; }
            public int NU_NIRER { get; set; }
            public string NU_NIRE
            {
                get
                {
                    string NU = NU_NIRER.ToString();

                    return NU.PadLeft(7, '0');
                }
            }
            public decimal? NU_NIS { get; set; }
            public DateTime? DT_NASC_ALU { get; set; }
            public string TP_DEF { get; set; }
            public string CO_SITUA { get; set; }
            public string CO_SITUA_V
            {
                get
                {
                    return AuxiliFormatoExibicao.RetornaSituacaoPacienteSaude(this.CO_SITUA);
                }
            }
        }

        #endregion
    }
}
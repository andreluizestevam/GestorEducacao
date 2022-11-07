//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: CIDADE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 27/11/14 | Maxwell Almeida            | Criação da funcionalidade para busca Procedimentos Médicos


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

namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9110_CtrlProcedimentosMedicos._9116_ProcedimentosMedicosValores
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
                CarregaGrupos();
                CarregaTipos();
                CarregaOperadoras();
                CarregaSubGrupos();
                CarregaProcedimentosPadroesInstituicao();
                VerificaOperServiPublico();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new String[] { "ID_PROC_MEDI_PROCE" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_PROC_MEDIC_GRUPO_V",
                HeaderText = "GRUPO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_PROC_MEDIC_SGRUP_V",
                HeaderText = "SUBGRUPO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_PROC_MEDI_V",
                HeaderText = "PROCEDIMENTO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "OPERADORA",
                HeaderText = "OPERADORA"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "VALOR_BASE",
                HeaderText = "VALOR BASE"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_SITUACAO",
                HeaderText = "SITUAÇÃO"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coGrupo = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            int coSubGrupo = ddlSubGrupo.SelectedValue != "" ? int.Parse(ddlSubGrupo.SelectedValue) : 0;
            int idOper = ddlOper.SelectedValue != "" ? int.Parse(ddlOper.SelectedValue) : 0;
            int idAgrup = ddlAgrupador.SelectedValue != "" ? int.Parse(ddlAgrupador.SelectedValue) : 0;

            var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                       join tbs354 in TBS354_PROC_MEDIC_GRUPO.RetornaTodosRegistros() on tbs356.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO equals tbs354.ID_PROC_MEDIC_GRUPO
                       join tbs355 in TBS355_PROC_MEDIC_SGRUP.RetornaTodosRegistros() on tbs356.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP equals tbs355.ID_PROC_MEDIC_SGRUP
                       where (txtNoProcedimento.Text != "" ? tbs356.NM_PROC_MEDI.Contains(txtNoProcedimento.Text) : txtNoProcedimento.Text == "")
                       && (txtCodProcMedic.Text != "" ? tbs356.CO_PROC_MEDI.Contains(txtCodProcMedic.Text) : txtCodProcMedic.Text == "")
                       && (ddlSituacao.SelectedValue != "0" ? tbs354.FL_SITUA_PROC_MEDIC_GRUPO == ddlSituacao.SelectedValue : 0 == 0)
                       && (coGrupo != 0 ? tbs354.ID_PROC_MEDIC_GRUPO == coGrupo : 0 == 0)
                       && (coSubGrupo != 0 ? tbs355.ID_PROC_MEDIC_SGRUP == coSubGrupo : 0 == 0)
                       && (idOper != 0 ? tbs356.TB250_OPERA.ID_OPER == idOper : 0 == 0)
                       && (idAgrup != 0 ? tbs356.ID_AGRUP_PROC_MEDI_PROCE == idAgrup : 0 == 0)
                       && (ddlTipoProcedimento.SelectedValue != "0" ? tbs356.CO_TIPO_PROC_MEDI == ddlTipoProcedimento.SelectedValue : 0 == 0)
                       && (ddlRequerAutori.SelectedValue != "0" ? tbs356.FL_AUTO_PROC_MEDI == ddlRequerAutori.SelectedValue : 0 == 0)
                       select new saida
                       {
                           ID_PROC_MEDI_PROCE = tbs356.ID_PROC_MEDI_PROCE,
                           NM_PROC_MEDI = tbs356.NM_PROC_MEDI,
                           NM_PROC_MEDIC_SGRUP = tbs355.NM_PROC_MEDIC_SGRUP,
                           NM_PROC_MEDIC_GRUPO = tbs354.NM_PROC_MEDIC_GRUPO,
                           OPERADORA = (tbs356.TB250_OPERA != null ? tbs356.TB250_OPERA.NOM_OPER : "-"),
                           DE_SITUACAO = (tbs356.CO_SITU_PROC_MEDI == "A" ? "Ativo" : tbs356.CO_SITU_PROC_MEDI == "I" ? "Inativo" : "SUSPENSO"),
                       }).OrderBy(w => w.NM_PROC_MEDI);
            CurrentPadraoBuscas.GridBusca.DataSource = (res.Count() > 0) ? res : null;
        }

        public class saida
        {

            public int ID_PROC_MEDI_PROCE { get; set; }
            public string VALOR_BASE
            {
                get
                {
                    var res = (from tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                               where tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == this.ID_PROC_MEDI_PROCE
                               && tbs353.FL_STATU == "A"
                               select new { tbs353.VL_BASE }).FirstOrDefault();

                    if (res != null)
                        return res.VL_BASE.ToString("N2");
                    else
                        return " - ";
                }
            }


            public string OPERADORA { get; set; }
            public string NM_PROC_MEDI { get; set; }
            public string NM_PROC_MEDI_V
            {
                get
                {
                    if (this.NM_PROC_MEDI.Length > 30)
                    {
                        string NovoValor = NM_PROC_MEDI.Substring(0, 30);
                        return NovoValor + "...";
                    }
                    else
                    {
                        return this.NM_PROC_MEDI;
                    }
                }
            }
            public string NM_PROC_MEDIC_SGRUP { get; set; }
            public string NM_PROC_MEDIC_SGRUP_V
            {
                get
                {
                    if (this.NM_PROC_MEDIC_SGRUP.Length > 20)
                    {
                        string NovoValor = NM_PROC_MEDIC_SGRUP.Substring(0, 20);
                        return NovoValor + "...";
                    }
                    else
                    {
                        return this.NM_PROC_MEDIC_SGRUP;
                    }

                }
            }
            public string NM_PROC_MEDIC_GRUPO { get; set; }
            public string NM_PROC_MEDIC_GRUPO_V
            {
                get
                {
                    if (this.NM_PROC_MEDIC_GRUPO.Length > 30)
                    {
                        string NovoValor = NM_PROC_MEDIC_GRUPO.Substring(0, 30);
                        return NovoValor + "...";
                    }
                    else
                    {
                        return this.NM_PROC_MEDIC_GRUPO;
                    }


                }
            }
            public string DE_SITUACAO { get; set; }
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_PROC_MEDI_PROCE"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Carrega os Grupos
        /// </summary>
        private void CarregaGrupos()
        {
            AuxiliCarregamentos.CarregaGruposProcedimentosMedicos(ddlGrupo, true);
        }

        /// <summary>
        /// Carrega so SubGrupos
        /// </summary>
        private void CarregaSubGrupos()
        {
            int coGrupo = ddlGrupo.SelectedValue != "" ? int.Parse(ddlGrupo.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaSubGruposProcedimentosMedicos(ddlSubGrupo, coGrupo, true);
        }

        /// <summary>
        /// Carrega as Operadoras de Planos de Saúde
        /// </summary>
        private void CarregaOperadoras()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOper, true);
        }

        /// <summary>
        /// Carrega os tipos de procedimentos
        /// </summary>
        private void CarregaTipos()
        {
            AuxiliCarregamentos.CarregaTiposProcedimentosMedicos(ddlTipoProcedimento, true);
        }

        /// <summary>
        /// Carrega os procedimentos padrões da instituição
        /// </summary>
        private void CarregaProcedimentosPadroesInstituicao()
        {
            AuxiliCarregamentos.CarregaProcedimentosPadroesInstituicao(ddlAgrupador, false, false);
            ddlAgrupador.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Verifica se existe a operadora de serviço publico e cadastra caso não exista
        /// </summary>
        private void VerificaOperServiPublico()
        {
            bool temServiPublico = TB250_OPERA.RetornaTodosRegistros().Where(w => w.CO_OPER == "999").Any();

            /*Para todo procedimento, é preciso ter um padrão da instituição, então a instituição precisa estar cadastrada
             como operadora de plano de saúde*/
            if (!temServiPublico)
            {
                TB250_OPERA tb250 = new TB250_OPERA();
                tb250.NOM_OPER = "Instituição";
                tb250.NU_CNPJ_OPER = "99999999999999";
                tb250.ST_OPER = "A";
                tb250.CO_COL_CADAS = LoginAuxili.CO_COL;
                tb250.DT_CADAS = DateTime.Now;
                tb250.IP_CADAS = Request.UserHostAddress;
                tb250.CO_COL_SITU = LoginAuxili.CO_COL;
                tb250.FL_SITU_OPER = "A";
                tb250.DT_SITU_OPER = DateTime.Now;
                tb250.IP_SITU_OPER = Request.UserHostAddress;
                tb250.CO_OPER = "999";
                tb250.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                tb250.CO_EMP_SITU = LoginAuxili.CO_EMP;
                tb250.FL_INSTI_OPERA = "S";
                TB250_OPERA.SaveOrUpdate(tb250, true);
            }
        }

        #endregion

        #region Funções de Campo

        protected void ddlGrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupos();
        }

        #endregion
    }
}
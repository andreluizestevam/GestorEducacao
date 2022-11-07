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
// 15/07/2016| Filipe Rodrigues           | Criação da funcionalidade de busca de comissionamentos
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

namespace C2BR.GestorEducacao.UI.GSAUD._5000_CtrlFinanceira._5100_Recebimento._5105_Comissao
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarUnidades();
                CarregarProfissionais();
                CarregaGrupos();
                CarregaSubGrupos();
                CarregarProcedimentosMedicos();
                CarregarTiposComissao();
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
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_COMISS" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_COL",
                HeaderText = "Profissional"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "GRUPO",
                HeaderText = "Grupo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "SUBGRUPO",
                HeaderText = "Sub-Grupo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "PROCED",
                HeaderText = "Procedimento"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_AVALIACAO",
                HeaderText = "Avali"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_COBRANCA",
                HeaderText = "Cobrn"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_CONTRATO",
                HeaderText = "Contr"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_IND_PACIENTE",
                HeaderText = "Ind Pac"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_IND_PROCEDIMENTO",
                HeaderText = "Ind Prc"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_PLANEJAMENTO",
                HeaderText = "Planj"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "STATUS",
                HeaderText = "ST"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int unidade = int.Parse(ddlUnidade.SelectedValue);
            int profissional = int.Parse(ddlProfissional.SelectedValue);
            int grup = int.Parse(ddlGrupo.SelectedValue);
            int sgrup = int.Parse(ddlSubGrupo.SelectedValue);
            int proc = int.Parse(ddlProcedimento.SelectedValue);
            string situ = ddlSituacao.SelectedValue;
            string tipo = ddlTipo.SelectedValue;

            var resultado = (from tbs410 in TBS410_COMISSAO.RetornaTodosRegistros()
                             where (unidade != 0 ? tbs410.TB25_EMPRESA.CO_EMP == unidade : 0 == 0)
                             && (profissional != 0 ? tbs410.TB03_COLABOR.CO_COL == profissional : 0 == 0)
                             && (grup != 0 ? tbs410.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == grup : 0 == 0)
                             && (sgrup != 0 ? tbs410.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP == sgrup : 0 == 0)
                             && (proc != 0 ? tbs410.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == proc : 0 == 0)
                             && (situ != "0" ? tbs410.CO_SITUA == situ : 0 == 0)
                             && (tipo != "0" ? (tipo == "AVL" ? tbs410.VL_AVALIA.HasValue 
                                             : (tipo == "CBR" ? tbs410.VL_COBRAN.HasValue
                                             : (tipo == "CNT" ? tbs410.VL_CONTRT.HasValue
                                             : (tipo == "IPC" ? tbs410.VL_INDC_PAC.HasValue
                                             : (tipo == "IPR" ? tbs410.VL_INDC_PROC.HasValue 
                                      /*PLA*/: tbs410.VL_PLANEJ.HasValue))))) : 0 == 0)
                             select new listaComissoes
                             {
                                 ID_COMISS = tbs410.ID_COMISS,
                                 NO_COL = tbs410.TB03_COLABOR.NO_APEL_COL,
                                 GRUPO = tbs410.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.NM_PROC_MEDIC_GRUPO,
                                 SUBGRUPO = tbs410.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP,
                                 PROCED = tbs410.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,

                                 PC_AVALIACAO = tbs410.PC_AVALIA,
                                 VL_AVALIACAO = tbs410.VL_AVALIA,
                                 PC_COBRANCA = tbs410.PC_COBRAN,
                                 VL_COBRANCA = tbs410.VL_COBRAN,
                                 PC_CONTRATO = tbs410.PC_CONTRT,
                                 VL_CONTRATO = tbs410.VL_CONTRT,
                                 PC_IND_PACIENTE = tbs410.PC_INDC_PAC,
                                 VL_IND_PACIENTE = tbs410.VL_INDC_PAC,
                                 PC_IND_PROCEDIMENTO = tbs410.PC_INDC_PROC,
                                 VL_IND_PROCEDIMENTO = tbs410.VL_INDC_PROC,
                                 PC_PLANEJAMENTO = tbs410.PC_PLANEJ,
                                 VL_PLANEJAMENTO = tbs410.VL_PLANEJ,

                                 STATUS = tbs410.CO_SITUA
                             }).OrderBy(a => a.NO_COL);

            if (resultado != null && resultado.Count() > 0)
                CurrentPadraoBuscas.GridBusca.DataSource = resultado;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_COMISS"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #endregion

        #region Carregadores

        private void CarregarUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        private void CarregarProfissionais()
        {
            AuxiliCarregamentos.CarregaProfissionaisSaude(ddlProfissional, LoginAuxili.CO_EMP, true, "0", true, 0, false);
        }

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
        /// Carrega os procedimentos de acordo com grupo e subgrupo;
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregarProcedimentosMedicos()
        {
            AuxiliCarregamentos.CarregaProcedimentosMedicos(ddlProcedimento, ddlGrupo, ddlSubGrupo, 0, true, true, null, true, false);
        }

        private void CarregarTiposComissao()
        {
            AuxiliCarregamentos.CarregaTiposComissao(ddlTipo, true);
        }

        #endregion

        #region Classes

        /// <summary>
        /// Classe com os campos necessários para carregar a grid de buscas
        /// </summary>
        private class listaComissoes
        {
            public int ID_COMISS { get; set; }
            public string NO_COL { get; set; }
            public string GRUPO { get; set; }
            public string SUBGRUPO { get; set; }
            public string PROCED { get; set; }
            public string STATUS { get; set; }

            public string PC_AVALIACAO { get; set; }
            public decimal? VL_AVALIACAO { get; set; }
            public string DE_AVALIACAO
            {
                get
                {
                    return VL_AVALIACAO.HasValue ? (VL_AVALIACAO + (PC_AVALIACAO == "S" ? " %" : "")) : " - ";
                }
            }
            public string PC_COBRANCA { get; set; }
            public decimal? VL_COBRANCA { get; set; }
            public string DE_COBRANCA
            {
                get
                {
                    return VL_COBRANCA.HasValue ? (VL_COBRANCA + (PC_COBRANCA == "S" ? " %" : "")) : " - ";
                }
            }
            public string PC_CONTRATO { get; set; }
            public decimal? VL_CONTRATO { get; set; }
            public string DE_CONTRATO
            {
                get
                {
                    return VL_CONTRATO.HasValue ? (VL_CONTRATO + (PC_CONTRATO == "S" ? " %" : "")) : " - ";
                }
            }
            public string PC_IND_PACIENTE { get; set; }
            public decimal? VL_IND_PACIENTE { get; set; }
            public string DE_IND_PACIENTE
            {
                get
                {
                    return VL_IND_PACIENTE.HasValue ? (VL_IND_PACIENTE + (PC_IND_PACIENTE == "S" ? " %" : "")) : " - ";
                }
            }
            public string PC_IND_PROCEDIMENTO { get; set; }
            public decimal? VL_IND_PROCEDIMENTO { get; set; }
            public string DE_IND_PROCEDIMENTO
            {
                get
                {
                    return VL_IND_PROCEDIMENTO.HasValue ? (VL_IND_PROCEDIMENTO + (PC_IND_PROCEDIMENTO == "S" ? " %" : "")) : " - ";
                }
            }
            public string PC_PLANEJAMENTO { get; set; }
            public decimal? VL_PLANEJAMENTO { get; set; }
            public string DE_PLANEJAMENTO
            {
                get
                {
                    return VL_PLANEJAMENTO.HasValue ? (VL_PLANEJAMENTO + (PC_PLANEJAMENTO == "S" ? " %" : "")) : " - ";
                }
            }
        }

        #endregion

        #region Funções de Campo

        protected void ddlGrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupos();
            CarregarProcedimentosMedicos();
        }

        protected void ddlSubGrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarProcedimentosMedicos();
        }

        #endregion
    }
}
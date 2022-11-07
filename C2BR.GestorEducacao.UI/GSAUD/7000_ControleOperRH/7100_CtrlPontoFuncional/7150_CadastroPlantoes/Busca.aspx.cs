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
// 03/03/14 | Maxwell Almeida            | Criação da funcionalidade para busca Tipos de Plantões


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

namespace C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7150_CadastroPlantoes
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
                CarregaUnidades();
                CarregaDepartamentos();
                CarregaEspecialidades();
                AuxiliCarregamentos.CarregaSituacaoTipoPlantao(ddlSitu, true);
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new String[] { "ID_TIPO_PLANT" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "sigla",
                HeaderText = "UNIDADE"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_TIPO_PLANT",
                HeaderText = "PLANTÃO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SIGLA_TIPO_PLANT",
                HeaderText = "SIGLA"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SIGLA_DEPTO",
                HeaderText = "LOCAL"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_SIGLA_ESPECIALIDADE",
                HeaderText = "ESPEC"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "HR_INI_TIPO_PLANT",
                HeaderText = "INICIO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "QT_HORAS",
                HeaderText = "HRS"
            });

			CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
			{
				DataField = "CO_SITUA_TIPO_PLANT",
				HeaderText = "Situação"
			});

        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coEmp = (ddlUnid.SelectedValue != "" ? int.Parse(ddlUnid.SelectedValue) : 0);
            int coEspec = (ddlEspec.SelectedValue != "" ? int.Parse(ddlEspec.SelectedValue) : 0);
            int Depto = (ddlLocal.SelectedValue != "" ? int.Parse(ddlLocal.SelectedValue) : 0);

            var res = (from tb153 in TB153_TIPO_PLANT.RetornaTodosRegistros()
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb153.TB25_EMPRESA.CO_EMP equals tb25.CO_EMP
                       join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tb153.TB14_DEPTO.CO_DEPTO equals tb14.CO_DEPTO into ld
                       from ldept in ld.DefaultIfEmpty()
                       join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb153.CO_ESPEC equals tb63.CO_ESPECIALIDADE into le
                       from lesp in le.DefaultIfEmpty()
                       where ((txtNome.Text) != "" ? tb153.NO_TIPO_PLANT.Contains(txtNome.Text) : txtNome.Text == "")
                          && ((txtSigla.Text) != "" ? tb153.CO_SIGLA_TIPO_PLANT.Contains(txtSigla.Text) : txtSigla.Text == "")
                          && (coEmp != 0 ? tb153.TB25_EMPRESA.CO_EMP == coEmp : 0 == 0)
                          && (coEspec != 0 ? tb153.CO_ESPEC == coEspec : 0 == 0)
                          && (Depto != 0 ? tb153.TB14_DEPTO.CO_DEPTO == Depto : 0 == 0)
                          && (ddlSitu.SelectedValue != "0" ? tb153.CO_SITUA_TIPO_PLANT == ddlSitu.SelectedValue : 0 == 0)
                       select new
                       {
                           tb153.NO_TIPO_PLANT,
                           tb153.ID_TIPO_PLANT,
                           tb153.CO_SIGLA_TIPO_PLANT,
                           CO_SITUA_TIPO_PLANT = (tb153.CO_SITUA_TIPO_PLANT == "A" ? "Ativo" : tb153.CO_SITUA_TIPO_PLANT == "I" ? "Inativo" : "Suspenso"),
                           tb25.sigla,
                           lesp.NO_SIGLA_ESPECIALIDADE,
                           ldept.CO_SIGLA_DEPTO,
                           tb153.HR_INI_TIPO_PLANT,
                           tb153.QT_HORAS,
                       }).OrderBy(w => w.NO_TIPO_PLANT);

            CurrentPadraoBuscas.GridBusca.DataSource = (res.Count() > 0) ? res : null;
                            
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_TIPO_PLANT"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }

        #region Carregamentos

        /// <summary>
        /// Carrega as Unidades
        /// </summary>
        private void CarregaUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnid, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega os Departamentos
        /// </summary>
        private void CarregaDepartamentos()
        {
            AuxiliCarregamentos.CarregaDepartamentos(ddlLocal, LoginAuxili.CO_EMP, true);
        }

        /// <summary>
        /// Carrega as Especialidades
        /// </summary>
        private void CarregaEspecialidades()
        {
            AuxiliCarregamentos.CarregaEspeciacialidades(ddlEspec, LoginAuxili.CO_EMP, null, true);
        }

        protected void ddlUnid_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if((ddlUnid.SelectedValue != "") && (ddlUnid.SelectedValue != "0"))
            {
                int coEmp = int.Parse(ddlUnid.SelectedValue);
                AuxiliCarregamentos.CarregaDepartamentos(ddlLocal, coEmp, true);
                //AuxiliCarregamentos.CarregaEspeciacialidades(ddlEspec, coE
            }
        }

        #endregion

        #endregion
    }
}
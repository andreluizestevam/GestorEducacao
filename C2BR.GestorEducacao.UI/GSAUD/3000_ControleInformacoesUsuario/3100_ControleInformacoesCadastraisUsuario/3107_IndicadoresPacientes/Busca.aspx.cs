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

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3107_IndicadoresPacientes
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaClassificacoesFuncionais();
                CarregaUfs();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_CPF",
                HeaderText = "CPF",
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NM_PROFI_INDIC",
                HeaderText = "Nome"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CATEG",
                HeaderText = "Categoria"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "STATUS",
                HeaderText = "Situação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            try
            {
                string NumeroCpf = txtCPF.Text.Replace(".", "").Replace(".", "").Replace("-", "");
                string Cassifi = (!string.IsNullOrEmpty(ddlClassificacoesFuncionais.SelectedValue) ? ddlClassificacoesFuncionais.SelectedValue : null);
                string Sigla = (!string.IsNullOrEmpty(ddlSiglaEntidProfi.SelectedValue) ? ddlSiglaEntidProfi.SelectedValue : null);
                string UfEntidProfi = (!string.IsNullOrEmpty(ddlUfEntidProfi.SelectedValue) ? ddlUfEntidProfi.SelectedValue : null);

                DateTime? DataInicial = txtDtEmissEntidProfi.Text == "" ? null : DataInicial = Convert.ToDateTime(txtDtEmissEntidProfi.Text);

                var resultado = (from tbs377 in TBS377_INDIC_PACIENTES.RetornaTodosRegistros()
                                 where (NumeroCpf != "" ? tbs377.NU_CPF.Contains(NumeroCpf) : NumeroCpf == "")
                                 && (txtnome.Text != "" ? tbs377.NM_PROFI_INDIC.Equals(txtnome.Text) : txtnome.Text == "")
                                 && (txtNrEntidProfi.Text != "" ? tbs377.NU_ENTID_PROFI.Equals(NumeroCpf) : txtNrEntidProfi.Text == "")
                                 && (Cassifi != null ? tbs377.CO_CLASS_FUNC == Cassifi : 0 == 0)
                                 && (UfEntidProfi != null ? tbs377.CO_UF_ENTID_PROFI == UfEntidProfi : 0 == 0)
                                 && (ddlSitu.SelectedValue != "T" ? tbs377.CO_SITUA == ddlSitu.SelectedValue : "" == "")
                                 && (Sigla != null ? tbs377.CO_SITUA == Sigla : "" == "")
                                 && (txtDtEmissEntidProfi.Text != "" ? tbs377.DT_EMISS_ENTID_PROFI.Value == DataInicial.Value : "" == "")
                                 select new
                                 {
                                     ID = tbs377.ID_INDIC_PACIENTES,
                                     NU_CPF = tbs377.NU_CPF,
                                     NM_PROFI_INDIC = tbs377.NM_PROFI_INDIC,
                                     CATEG = tbs377.CO_CLASS_FUNC == "A" ? "Avaliador(a)" : tbs377.CO_CLASS_FUNC == "E" ? "Enfermeiro(a)" :
                                      tbs377.CO_CLASS_FUNC == "M" ? "Médico(a)" : tbs377.CO_CLASS_FUNC == "D" ? "Odontólogo(a)" : tbs377.CO_CLASS_FUNC == "S" ? "Esteticista" :
                                     tbs377.CO_CLASS_FUNC == "P" ? "Psicólogo(a)" : tbs377.CO_CLASS_FUNC == "I" ? "Fisioterapeuta" : tbs377.CO_CLASS_FUNC == "F" ? "Fonoaudiólogo(a)" :
                                      tbs377.CO_CLASS_FUNC == "T" ? "Terapeuta Ocupacional" : tbs377.CO_CLASS_FUNC == "N" ? "Nutricionista" : "Outros",
                                     STATUS = tbs377.CO_SITUA == "A" ? "Ativo" : tbs377.CO_SITUA == "I" ? "Inativo" : "Suspenso",

                                 }).OrderBy(e => e.NM_PROFI_INDIC);

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
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }


        #region Carregamento DropDown



        /// <summary>
        /// Método que carrega o dropdown de UFs
        /// </summary>
        /// <param name="ddl">DropDownList UF</param>
        private void CarregaUfs()
        {
            AuxiliCarregamentos.CarregaUFs(ddlUfEntidProfi, false);
        }

        private void CarregaClassificacoesFuncionais()
        {

            AuxiliCarregamentos.CarregaClassificacoesFuncionais(ddlClassificacoesFuncionais, false, LoginAuxili.CO_EMP);

        }

        //private class lista
        //{
        //    public int ID { get; set; }
        //    public string NU_CPF { get; set; }
        //    public string NM_PROFI_INDIC { get; set; }
        //    public string CATEG { get; set; }
        //    public string STATUS { get; set; }



        //}


        #endregion
    }
}
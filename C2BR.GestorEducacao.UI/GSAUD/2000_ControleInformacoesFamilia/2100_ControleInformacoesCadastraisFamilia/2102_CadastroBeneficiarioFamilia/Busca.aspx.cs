// OBJETIVO: Cadastro de Beneficiários da Família
// DATA DE CRIAÇÃO: 27/11/2021
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 27/11/2021| Fabricio Soares dos Santos | Criada a funcionalidade de cadastro beneficiários da família.
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

namespace C2BR.GestorEducacao.UI.GSAUD._2000_ControleInformacoesFamilia._2100_ControleInformacoesCadastraisFamilia._2102_CadastroBeneficiarioFamilia
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            ///Criação das instâncias utilizadas
            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUfs();
                CarregaCidades();
                CarregaRegiao();
                CarregaArea();
                CarregaSubArea();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_FAMIL_BENEF" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_FAMILIA",
                HeaderText = "Código"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_ALU",
                HeaderText = "Nome"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SEXO_ALU",
                HeaderText = "Sexo"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NU_CPF_ALU",
                HeaderText = "CPF"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TP_BENEF_FAMIL",
                HeaderText = "Beneficiário"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_GRAU_PAREN",
                HeaderText = "Grau Parent"
            });
            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_SITUA",
                HeaderText = "Situação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int idCidade = ddlCidade.SelectedValue != "" ? int.Parse(ddlCidade.SelectedValue) : 0;
            int idRegiao = ddlRegiao.SelectedValue != "" ? int.Parse(ddlRegiao.SelectedValue) : 0;
            int idArea = ddlArea.SelectedValue != "" ? int.Parse(ddlArea.SelectedValue) : 0;
            int idSubArea = ddlSubArea.SelectedValue != "" ? int.Parse(ddlArea.SelectedValue) : 0;

            var resultado = (from tbg76 in  TBG076_FAMIL_BENEF.RetornaTodosRegistros()
                             where (txtCodigo.Text != "" ? tbg76.TB075_FAMILIA.CO_FAMILIA == txtCodigo.Text : true)
                             && (txtNome.Text != "" ? tbg76.TB075_FAMILIA.NO_RESP_FAM.Contains(txtNome.Text) : true)
                             && (txtCep.Text != "" ?  tbg76.TB075_FAMILIA.CO_CEP_FAM == txtCep.Text : true)
                             && (ddlUf.SelectedValue != "" ? tbg76.TB075_FAMILIA.TB904_CIDADE.CO_UF == ddlUf.SelectedValue : true)                            
                             && (idCidade != 0 ? tbg76.TB075_FAMILIA.TB904_CIDADE.CO_CIDADE == idCidade : true)
                             && (idRegiao != 0 ? tbg76.TB075_FAMILIA.TB906_REGIAO.ID_REGIAO == idRegiao : true)
                             && (idArea != 0 ? tbg76.TB075_FAMILIA.TB907_AREA.ID_AREA == idArea : true)
                             && (idRegiao != 0 ? tbg76.TB075_FAMILIA.TB908_SUBAREA.ID_SUBAREA == idRegiao : true)
                             join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbg76.TB075_FAMILIA.ID_FAMILIA equals tb07.TB075_FAMILIA.ID_FAMILIA
                             select new { tbg76.ID_FAMIL_BENEF, tbg76.TB075_FAMILIA.CO_FAMILIA, tb07.NO_ALU, 
                                 CO_SEXO_ALU = tb07.CO_SEXO_ALU == "M" ? "Masculino" : "Feminino", tb07.NU_CPF_ALU,
                                          TP_BENEF_FAMIL = tbg76.TP_BENEF_FAMIL == "R" ? "Responsável" : "Paciente",
                                          tbg76.CO_GRAU_PAREN,
                                          CO_SITUA = tbg76.CO_SITUA == "A" ? "Ativo" : "Inativo"
                             });

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_FAMIL_BENEF"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }


        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de UF
        /// </summary>
        private void CarregaUfs()
        {
            ddlUf.DataSource = TB74_UF.RetornaTodosRegistros();

            ddlUf.DataTextField = "CODUF";
            ddlUf.DataValueField = "CODUF";
            ddlUf.DataBind();

            ddlUf.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Cidades
        /// </summary>
        private void CarregaCidades()
        {
            ddlCidade.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUf.SelectedValue);

            ddlCidade.DataTextField = "NO_CIDADE";
            ddlCidade.DataValueField = "CO_CIDADE";
            ddlCidade.DataBind();

            ddlCidade.Enabled = ddlCidade.Items.Count > 0;
            ddlCidade.Items.Insert(0, new ListItem("Selecione", ""));
        }

        //====> Método que carrega o DropDown de região
        private void CarregaRegiao()
        {
            ddlRegiao.DataSource = TB906_REGIAO.RetornaTodosRegistros().OrderBy(r => r.ID_REGIAO);

            ddlRegiao.DataTextField = "NM_REGIAO";
            ddlRegiao.DataValueField = "ID_REGIAO";
            ddlRegiao.DataBind();

            ddlRegiao.Items.Insert(0, new ListItem("Selecione", ""));
        }

        //====> Método que carrega o DropDown de Area
        private void CarregaArea()
        {
            int idRegiao = ddlRegiao.SelectedValue != "" ? int.Parse(ddlRegiao.SelectedValue) : 0;

            if (idRegiao != 0)
            {
                ddlArea.DataSource = TB907_AREA.RetornaPelaRegiao(idRegiao);

                ddlArea.DataTextField = "NM_AREA";
                ddlArea.DataValueField = "ID_AREA";
                ddlArea.DataBind();

                ddlArea.Enabled = ddlArea.Items.Count > 0;
                ddlArea.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        //====> Método que carrega o DropDown de Area
        private void CarregaSubArea()
        {
            int idArea = ddlArea.SelectedValue != "" ? int.Parse(ddlArea.SelectedValue) : 0;

            if (idArea != 0)
            {
                ddlSubArea.DataSource = (from tb908 in TB908_SUBAREA.RetornaPelaArea(idArea)
                                         select new { tb908.NM_SUBAREA, tb908.ID_SUBAREA });

                ddlSubArea.DataTextField = "NM_SUBAREA";
                ddlSubArea.DataValueField = "ID_SUBAREA";
                ddlSubArea.DataBind();
                ddlSubArea.Enabled = ddlSubArea.Items.Count > 0;
                ddlSubArea.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }
        #endregion

        protected void ddlUf_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades();
        }
        protected void ddlRegiao_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaArea();
        }

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubArea();
        }
    }
}
//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: BOLETO BANCÁRIO DE TÍTULOS DE RECEITAS/RECURSOS POR TURMA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

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

namespace C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5212_BoletoPorTurma
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
                CarregaAnos();
                CarregaUnidade();
                CarregaModalidade();
                CarregaSerie();
                CarregaTurma();
                CarregaBoletos();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_TUR", "ID_BOLETO", "TP_BOL", "CO_ANO", "CO_MODU_CUR", "CO_CUR", "TP_TAXA" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_TURMA",
                HeaderText = "TURMA"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            string strTpCliente = ddlTipo.SelectedValue;
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coUnidContr = ddlUnidadeContrato.SelectedValue != "" ? int.Parse(ddlUnidadeContrato.SelectedValue) : 0;
            int coModu = int.Parse(ddlModalidade.SelectedValue);
            int coCur = int.Parse(ddlSerie.SelectedValue);
            int coTur = int.Parse(ddlTurma.SelectedValue);
            int idBol = ddlBoleto.SelectedValue != "" ? int.Parse(ddlBoleto.SelectedValue) : 0;
            string tpTaxa = ddlTipoTaxaBoleto.SelectedValue;
            string ano = ddlAno.SelectedValue;
            string tpBol = ddlTipo.SelectedValue;

            //if (strTpCliente == "A")
            //{
                var resultado = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                 join tb06 in TB06_TURMAS.RetornaTodosRegistros() on tb47.CO_TUR equals tb06.CO_TUR
                                 join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb06.CO_TUR equals tb129.CO_TUR
                                 where tb47.CO_EMP == coEmp
                                 && (coUnidContr != 0 ? tb47.CO_EMP_UNID_CONT == coUnidContr : 0 == 0)
                                 && (coModu != 0 ? tb47.CO_MODU_CUR == coModu : 0 == 0)
                                 && (coCur != 0 ? tb47.CO_CUR == coCur : 0 == 0)
                                 && (coTur != 0 ? tb47.CO_TUR == coTur : 0 == 0)
                                 && tb47.CO_ANO_MES_MAT == ano
                                 select new
                                 {
                                     CO_TUR = tb06.CO_TUR,
                                     CO_MODU_CUR = tb47.CO_MODU_CUR,
                                     CO_CUR = tb47.CO_CUR,
                                     NO_TURMA = tb129.NO_TURMA,
                                     ID_BOLETO = idBol,
                                     TP_BOL = tpBol,
                                     CO_ANO = ano,
                                     TP_TAXA = tpTaxa
                                 }).Distinct();

                //var resultado = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                //                 join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb47.CO_ALU equals tb07.CO_ALU
                //                 where (txtNome.Text != "" ? tb07.NO_ALU.Contains(txtNome.Text) : txtNome.Text == "")
                //                 && tb47.CO_EMP == coEmp
                //                 && (coUnidContr != 0 ? tb47.CO_EMP_UNID_CONT == coUnidContr : coUnidContr == 0)
                //                 && tb47.IC_SIT_DOC == "A" && tb47.TP_CLIENTE_DOC == "A"
                //                 select new
                //                 {
                //                     tb07.TB25_EMPRESA1.CO_EMP,
                //                     TIPO = "A",
                //                     CODIGO = tb07.CO_ALU,
                //                     NOME = tb07.NO_ALU,
                //                     UF = tb07.CO_ESTA_ALU,
                //                     tb07.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                //                     CPFCNPJ = tb07.NU_NIRE,
                //                     TELEFONE = tb07.NU_TELE_RESI_ALU.Length >= 10 ? tb07.NU_TELE_RESI_ALU.Insert(6, "-").Insert(2, ") ").Insert(0, "(") : tb07.NU_TELE_RESI_ALU
                //                 }).Distinct().OrderBy(c => c.NOME);

                CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
            //}
            //else
            //{
            //    var resultado = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
            //                     join tb103 in TB103_CLIENTE.RetornaTodosRegistros() on tb47.TB103_CLIENTE.CO_CLIENTE equals tb103.CO_CLIENTE
            //                     where (txtNome.Text != "" ? tb47.TB103_CLIENTE.NO_FAN_CLI.Contains(txtNome.Text) : txtNome.Text == "")
            //                     && tb47.CO_EMP == coEmp
            //                     && (coUnidContr != 0 ? tb47.CO_EMP_UNID_CONT == coUnidContr : coUnidContr == 0)
            //                     && tb47.IC_SIT_DOC == "A" && tb47.TP_CLIENTE_DOC == "O"
            //                     select new
            //                     {
            //                         CO_EMP = coEmp,
            //                         TIPO = "O",
            //                         CODIGO = tb103.CO_CLIENTE,
            //                         NOME = tb103.NO_FAN_CLI,
            //                         UF = tb103.CO_UF_CLI,
            //                         NO_CIDADE = tb103.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
            //                         CPFCNPJ = (tb103.TP_CLIENTE == "F" && tb103.CO_CPFCGC_CLI.Length >= 11) ? tb103.CO_CPFCGC_CLI.Insert(9, "-").Insert(6, ".").Insert(3, ".") :
            //                             ((tb103.TP_CLIENTE == "J" && tb103.CO_CPFCGC_CLI.Length >= 14) ? tb103.CO_CPFCGC_CLI.Insert(12, "-").Insert(8, "/").Insert(5, ".").Insert(2, ".") : tb103.CO_CPFCGC_CLI),
            //                         TELEFONE = tb103.CO_TEL1_CLI.Length >= 10 ? tb103.CO_TEL1_CLI.Insert(6, "-").Insert(2, ") ").Insert(0, "(") : tb103.CO_TEL1_CLI
            //                     }).OrderBy(c => c.NOME).Distinct();

            //    CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
            //}
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoEmp, ddlUnidade.SelectedValue));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_BOLETO"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoModuCur, "CO_MODU_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoCur, "CO_CUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoTur, "CO_TUR"));
            queryStringKeys.Add(new KeyValuePair<string, string>("tp", "TP_BOL"));
            queryStringKeys.Add(new KeyValuePair<string, string>("ano", "CO_ANO"));
            queryStringKeys.Add(new KeyValuePair<string, string>("tpTaxa", "TP_TAXA"));


            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

        //====> Método que carrega o DropDown de Anos
        private void CarregaAnos()
        {
            ddlAno.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                 select new { tb43.CO_ANO_GRADE }).Distinct().OrderByDescending(g => g.CO_ANO_GRADE);

            ddlAno.DataTextField = "CO_ANO_GRADE";
            ddlAno.DataValueField = "CO_ANO_GRADE";
            ddlAno.DataBind();
        }

        //====> Método que carrega o DropDown de Unidades Escolares
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();

            ddlUnidadeContrato.DataSource = from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                            where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                            select new { tb25.CO_EMP, tb25.NO_FANTAS_EMP };

            ddlUnidadeContrato.DataTextField = "NO_FANTAS_EMP";
            ddlUnidadeContrato.DataValueField = "CO_EMP";
            ddlUnidadeContrato.DataBind();

            ddlUnidadeContrato.Items.Insert(0, new ListItem("Todas", ""));
        }

        //====> Método que carrega o DropDown de Modalidade
        private void CarregaModalidade()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(w => w.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";

            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Todas", "0"));
        }

        //====> Método que carrega o DropDown de Série/Curso
        private void CarregaSerie()
        {
            int coCur = int.Parse(ddlModalidade.SelectedValue);

            ddlSerie.DataSource = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                   where (coCur != 0 ? tb01.CO_MODU_CUR == coCur : 0 == 0)
                                   select new
                                   {
                                       tb01.CO_CUR,
                                       tb01.NO_CUR
                                   });
            ddlSerie.DataTextField = "NO_CUR";
            ddlSerie.DataValueField = "CO_CUR";
            ddlSerie.DataBind();

            ddlSerie.Items.Insert(0, new ListItem("Todas", "0"));
        }

        //====> Método que carrega o DropDown de Turma
        private void CarregaTurma()
        {
            int coModu = int.Parse(ddlModalidade.SelectedValue);
            int coCur = int.Parse(ddlSerie.SelectedValue);

            ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                   join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb06.CO_TUR equals tb129.CO_TUR
                                   where (coModu != 0 ? tb06.CO_MODU_CUR == coModu : 0 == 0)
                                   && (coCur != 0 ? tb06.CO_CUR == coCur : 0 == 0)
                                   select new
                                   {
                                       tb129.CO_TUR,
                                       tb129.NO_TURMA
                                   });
            ddlTurma.DataTextField = "NO_TURMA";
            ddlTurma.DataValueField = "CO_TUR";
            ddlTurma.DataBind();

            ddlTurma.Items.Insert(0, new ListItem("Todas", "0"));
        }

        //====> Método que carrega o DropDown de Boleto
        private void CarregaBoletos()
        {
            if (ddlTipoTaxaBoleto.SelectedValue != "" && ddlUnidadeContrato.SelectedValue != "")
            {
                int coEmp = ddlUnidadeContrato.SelectedValue != "" ? int.Parse(ddlUnidadeContrato.SelectedValue) : LoginAuxili.CO_EMP;

                AuxiliCarregamentos.CarregaBoletos(ddlBoleto, coEmp, ddlTipoTaxaBoleto.SelectedValue, 0, 0, false, false);
            }

            ddlBoleto.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        #region Métodos DropDown

        //====> Método que carrega o DropDown de Série/Curso quando uma modalidade é selecionada
        protected void ddlModalidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerie();
        }

        //====> Método que carrega o DropDown de Turma quando uma série/curso é selecionada
        protected void ddlSerie_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }

        //====> Método que carrega o DropDown de Boleto quando uma unidade de contrato é selecionada
        protected void ddlUnidadeContrato_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBoletos();
        }

        //====> Método que carrega o DropDown de Boleto quando um tipo de taxa é secionado
        protected void ddlTipoTaxaBoleto_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBoletos();
        }

        //====> Método que carrega os DropDown da página quando o ano é selecionado
        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUnidade();
            CarregaModalidade();
            CarregaSerie();
            CarregaTurma();
            CarregaBoletos();
        }
        #endregion
    }
}
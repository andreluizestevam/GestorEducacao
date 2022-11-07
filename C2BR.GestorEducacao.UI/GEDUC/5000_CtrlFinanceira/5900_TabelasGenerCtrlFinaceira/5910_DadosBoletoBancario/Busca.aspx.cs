//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: **********************
// SUBMÓDULO: *******************
// OBJETIVO: ********************
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

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5910_DadosBoletoBancario
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
                CarregaBancos();
                CarregaAgencias();
                CarregaModalidades();
                CarregaCursos();
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_BOLETO" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DESBANCO",
                HeaderText = "Banco"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_AGENCIA_DIG",
                HeaderText = "Agência"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_CEDENTE",
                HeaderText = "Cedente"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_CONTA_DIG",
                HeaderText = "Conta"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "TP_TAXA_BOLETO",
                HeaderText = "Tipo Taxa"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_MODU_CUR",
                HeaderText = "Modalidade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_CUR",
                HeaderText = "Curso"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            string strIdeBanco = ddlBanco.SelectedValue;
            int coAgencia = ddlAgencia.SelectedValue != "" ? int.Parse(ddlAgencia.SelectedValue) : 0;
            int coMod = (!string.IsNullOrEmpty(ddlModalidade.SelectedValue) ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int coCur = (!string.IsNullOrEmpty(ddlCurso.SelectedValue) ? int.Parse(ddlCurso.SelectedValue) : 0);

            var resultado = (from tb227 in TB227_DADOS_BOLETO_BANCARIO.RetornaTodosRegistros()
                             join tb224 in TB224_CONTA_CORRENTE.RetornaTodosRegistros() on tb227.TB224_CONTA_CORRENTE equals tb224
                             join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb227.CO_MODU_CUR equals tb44.CO_MODU_CUR into mod
                             from tb44 in mod.DefaultIfEmpty()
                             join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb227.CO_CUR equals tb01.CO_CUR into cur
                             from tb01 in cur.DefaultIfEmpty()
                            where (strIdeBanco != "" ? tb227.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.IDEBANCO == strIdeBanco : strIdeBanco == "")
                            && (coAgencia != 0 ? tb227.TB224_CONTA_CORRENTE.TB30_AGENCIA.CO_AGENCIA == coAgencia : coAgencia == 0)
                            && (tb224.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO)
                            && (coMod != 0 ? tb227.CO_MODU_CUR == coMod : true)
                            && (coCur != 0 ? tb227.CO_CUR == coCur : true)
                            select new
                            {
                                tb227.ID_BOLETO, DESBANCO = tb224.TB30_AGENCIA.TB29_BANCO.DESBANCO,
                                CO_AGENCIA = tb224.TB30_AGENCIA.CO_AGENCIA,
                                CO_CONTA = tb224.CO_CONTA, tb227.CO_CEDENTE,
                                TP_TAXA_BOLETO = tb227.TP_TAXA_BOLETO.Equals("M") ? "Matrícula" :
                                                    tb227.TP_TAXA_BOLETO.Equals("R") ? "Renovação" :
                                                    tb227.TP_TAXA_BOLETO.Equals("B") ? "Biblioteca" :
                                                    tb227.TP_TAXA_BOLETO.Equals("S") ? "Serv. Secretaria" :
                                                    tb227.TP_TAXA_BOLETO.Equals("D") ? "Serv. Diversos" :
                                                    tb227.TP_TAXA_BOLETO.Equals("A") ? "Atividades Extras" :
                                                    tb227.TP_TAXA_BOLETO.Equals("E") ? "Mensalidade" :
                                                    tb227.TP_TAXA_BOLETO.Equals("N") ? "Negociação" : "Outros",
                                tb224.CO_DIG_CONTA, tb224.TB30_AGENCIA.DI_AGENCIA,
                                tb44.DE_MODU_CUR,
                                tb01.NO_CUR
                                //CO_CONTA_DIG = string.Format("{0}-{1}", tb227.TB224_CONTA_CORRENTE.CO_CONTA.Trim(), tb227.TB224_CONTA_CORRENTE.CO_DIG_CONTA.Trim()),
                                //CO_AGENCIA_DIG = string.Format("{0}-{1}", tb227.TB224_CONTA_CORRENTE.CO_AGENCIA, tb227.TB224_CONTA_CORRENTE.TB30_AGENCIA.DI_AGENCIA.Trim()),
                            }).ToList();

            var resultado2 = (from tbRes in resultado
                              select new
                              {
                                  tbRes.ID_BOLETO, tbRes.DESBANCO, tbRes.CO_AGENCIA, tbRes.CO_CONTA, tbRes.CO_CEDENTE,
                                  tbRes.TP_TAXA_BOLETO,
                                  CO_CONTA_DIG = string.Format("{0}-{1}", tbRes.CO_CONTA.Trim(), tbRes.CO_DIG_CONTA.Trim()),
                                  CO_AGENCIA_DIG = string.Format("{0}-{1}", tbRes.CO_AGENCIA, tbRes.DI_AGENCIA.Trim()),
                                  tbRes.DE_MODU_CUR,
                                  tbRes.NO_CUR
                              }).OrderBy(o => o.DESBANCO).ThenBy(o => o.CO_AGENCIA).ThenBy(o => o.CO_CEDENTE).ToList();

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado2.Count() > 0) ? resultado2 : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_BOLETO"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }        
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Bancos
        private void CarregaBancos()
        {
            ddlBanco.DataSource = (from tb29 in TB29_BANCO.RetornaTodosRegistros().AsEnumerable()
                                   select new { tb29.IDEBANCO, DESBANCO = string.Format("{0} - {1}", tb29.IDEBANCO, tb29.DESBANCO) }).OrderBy(b => b.DESBANCO);

            ddlBanco.DataValueField = "IDEBANCO";
            ddlBanco.DataTextField = "DESBANCO";
            ddlBanco.DataBind();

            ddlBanco.Items.Insert(0, new ListItem("Todos", ""));
        }

//====> Método que carrega o DropDown de Agências
        private void CarregaAgencias()
        {
            string strIdeBanco = ddlBanco.SelectedValue;

            ddlAgencia.DataSource = (from tb30 in TB30_AGENCIA.RetornaTodosRegistros().AsEnumerable()
                                     where (strIdeBanco != "" ? tb30.IDEBANCO == strIdeBanco : strIdeBanco == "")
                                     select new
                                     {
                                         tb30.CO_AGENCIA,
                                         DESCRICAO = string.IsNullOrEmpty(ddlBanco.SelectedValue) ?
                                                 string.Format("({0}) {1} - {2}", tb30.IDEBANCO, tb30.CO_AGENCIA, tb30.NO_AGENCIA) :
                                                 string.Format("{0} - {1}", tb30.CO_AGENCIA, tb30.NO_AGENCIA)
                                     }).OrderBy( a => a.DESCRICAO );

            ddlAgencia.DataValueField = "CO_AGENCIA";
            ddlAgencia.DataTextField = "DESCRICAO";
            ddlAgencia.DataBind();

            ddlAgencia.Items.Insert(0, new ListItem("Todas", ""));
        }

        /// <summary>
        /// Carrega as modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega os Cursos
        /// </summary>
        private void CarregaCursos()
        {
            int coMod = (!string.IsNullOrEmpty(ddlModalidade.SelectedValue) ? int.Parse(ddlModalidade.SelectedValue) : 0);
            AuxiliCarregamentos.carregaSeriesCursos(ddlCurso, coMod, LoginAuxili.CO_EMP, true);
        }
        #endregion

        protected void ddlBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAgencias();
        }

        protected void ddlModalidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCursos();
        }
    }
}

//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE OCORRÊNCIAS DISCIPLINARES
// OBJETIVO: CADASTRAMENTO DE OCORRÊNCIAS DISCIPLINARES DE ALUNOS
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3640_CtrlOcorrenciasAlunos.F3641_CadastramentoOcorrDiscAluno
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
                CarregaUnidade();
                CarregaCategorias();
                CarregaTipoOcorrencia();
                CarregaTiposOcorrencias(ddlCategoria.SelectedValue, ddlTipoOcorrencia.SelectedValue);
                CarregaDadosFlex(ddlCategoria.SelectedValue);
            }
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "IDE_OCORR_ALUNO" };

            BoundField bfRealizado = new BoundField();
            bfRealizado.DataField = "DT_OCORR";
            bfRealizado.HeaderText = "DT/HR Ocorr";
            bfRealizado.ItemStyle.CssClass = "codCol";
            bfRealizado.DataFormatString = "{0:d}" + " {0:HH:mm}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bfRealizado);

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "COD_REGIS",
                HeaderText = "CÓDIGO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "categ",
                HeaderText = "CATEG"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NOME",
                HeaderText = "NOME"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_TIPO_OCORR",
                HeaderText = "CLASSIFICAÇÃO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "OCORRENCIA",
                HeaderText = "OCORRENCIA"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coFlex = ddlAluno.SelectedValue != "" ? int.Parse(ddlAluno.SelectedValue) : 0; // código flexível
            string categ = ddlCategoria.SelectedValue;
            int tpOcorrSv = (!string.IsNullOrEmpty(ddlTpOcorrTbxxx.SelectedValue) ? int.Parse(ddlTpOcorrTbxxx.SelectedValue) : 0);

            //Prepara as datas do filtro
            DateTime? dtIni = (!string.IsNullOrEmpty(txtDtIni.Text) ? DateTime.Parse(txtDtIni.Text) : (DateTime?)null);
            DateTime? dtFim = (!string.IsNullOrEmpty(txtDtFim.Text) ? DateTime.Parse(txtDtFim.Text) : (DateTime?)null);

            var resultado = (from tb191 in TB191_OCORR_ALUNO.RetornaTodosRegistros()
                             join tbe196 in TBE196_OCORR_DISCI.RetornaTodosRegistros() on tb191.TBE196_OCORR_DISCI.ID_OCORR_DISCI equals tbe196.ID_OCORR_DISCI into l1
                             from ls in l1.DefaultIfEmpty()
                             where (coFlex != 0 ? tb191.ID_RECEB_OCORR == coFlex : 0 == 0)
                             && (coEmp != 0 ? tb191.TB25_EMPRESA.CO_EMP == coEmp : 0 == 0)
                             && (ddlTipoOcorrencia.SelectedValue != "0" ? tb191.TB150_TIPO_OCORR.CO_SIGL_OCORR == ddlTipoOcorrencia.SelectedValue : 0 == 0)
                             && (tb191.CO_CATEG.Equals(categ))
                             && (tpOcorrSv != 0 ? tb191.TBE196_OCORR_DISCI.ID_OCORR_DISCI == tpOcorrSv : 0 == 0)
                             //Filtra as datas de início e fim somente se tiverem sido informadas, caso contrário trás todas
                             && ((dtIni.HasValue ? tb191.DT_OCORR >= dtIni.Value : 0 == 0) && (dtFim.HasValue ? tb191.DT_OCORR <= dtFim.Value : 0 == 0))
                             select new saida
                             {
                                 DT_OCORR = tb191.DT_OCORR,
                                 DE_TIPO_OCORR = tb191.TB150_TIPO_OCORR.DE_TIPO_OCORR,
                                 IDE_OCORR_ALUNO = tb191.IDE_OCORR_ALUNO,
                                 categ = (tb191.CO_CATEG == "A" ? "Aluno(a)" : tb191.CO_CATEG == "F" ? "Funcionário(a)" :
                                  tb191.CO_CATEG == "R" ? "Responsável" : tb191.CO_CATEG == "P" ? "Professor" : "Outro"),
                                 OCORRENCIA = ls.CO_SIGLA_OCORR,

                                 CO_CATEG = tb191.CO_CATEG,
                                 ID_RECEB = tb191.ID_RECEB_OCORR,
                                 COD_REGIS = tb191.CO_REGIS_OCORR,
                             }).ToList();

            resultado = resultado.OrderByDescending(w => w.DT_OCORR).ThenBy(w => w.NOME).ToList();

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        public class saida
        {
            public int IDE_OCORR_ALUNO { get; set; }
            public string OCORRENCIA { get; set; }
            public string categ { get; set; }
            public DateTime DT_OCORR { get; set; }
            public string DE_TIPO_OCORR { get; set; }
            public string COD_REGIS { get; set; }

            public string CO_CATEG { get; set; }
            public int? ID_RECEB { get; set; }
            public string NOME
            {
                get
                {
                    string s = "";

                    if (ID_RECEB.HasValue)
                    {
                        ///Coleta o nome de quem recebeu a ocorrência dinamicamente
                        switch (this.CO_CATEG)
                        {
                            case "A":
                                s = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == this.ID_RECEB).FirstOrDefault().NO_ALU;
                                break;

                            case "P":
                            case "F":
                                s = TB03_COLABOR.RetornaTodosRegistros().Where(wde => wde.CO_COL == this.ID_RECEB).FirstOrDefault().NO_COL;
                                break;

                            case "R":
                                s = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(this.ID_RECEB.Value).NO_RESP;
                                break;
                        }
                        return s;
                    }
                    else
                        return " - ";
                }
            }
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "IDE_OCORR_ALUNO"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

        //====> Método que carrega o DropDown de Unidades
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP });

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("Todos", ""));

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        //====> Método que carrega o DropDown de Unidades
        private void CarregaTipoOcorrencia()
        {
            //ddlTipoOcorrencia.DataSource = TB150_TIPO_OCORR.RetornaTodosRegistros().Where(p => p.TP_USU.Equals("A")).OrderBy(t => t.DE_TIPO_OCORR);

            AuxiliCarregamentos.CarregaTiposOcorrencias(ddlTipoOcorrencia, true, ddlCategoria.SelectedValue);
        }

        //====> Método que carrega o DropDown de Alunos
        protected void CarregaAlunos()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;

            ddlAluno.DataSource = (from tb07 in TB07_ALUNO.RetornaPelaUnid(coEmp)
                                   select new { tb07.NO_ALU, tb07.CO_ALU });

            ddlAluno.DataTextField = "NO_ALU";
            ddlAluno.DataValueField = "CO_ALU";
            ddlAluno.DataBind();

            ddlAluno.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega as categorias
        /// </summary>
        private void CarregaCategorias()
        {
            AuxiliCarregamentos.CarregaCategoriaOcorrencias(ddlCategoria, true, false);
        }

        /// <summary>
        /// Carrega os tipos de ocorrências já salvas de acordo com a categoria recebida como parâmetro
        /// </summary>
        /// <param name="CO_CATEG"></param>
        private void CarregaTiposOcorrencias(string CO_CATEG, string SGL_TIPO)
        {
            var res = (from tbe196 in TBE196_OCORR_DISCI.RetornaTodosRegistros()
                       where tbe196.CO_CATEG == CO_CATEG
                       && tbe196.TB150_TIPO_OCORR.CO_SIGL_OCORR == SGL_TIPO
                       select new
                       {
                           tbe196.ID_OCORR_DISCI,
                           tbe196.DE_OCORR,
                       }).ToList();

            ddlTpOcorrTbxxx.DataTextField = "DE_OCORR";
            ddlTpOcorrTbxxx.DataValueField = "ID_OCORR_DISCI";
            ddlTpOcorrTbxxx.DataSource = res;
            ddlTpOcorrTbxxx.DataBind();

            ddlTpOcorrTbxxx.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Carrega os dados flexivelmente de acordo com o selecionado em categoria
        /// </summary>
        private void CarregaDadosFlex(string CATEG)
        {
            int coEmp = (!string.IsNullOrEmpty(ddlUnidade.SelectedValue) && ddlUnidade.SelectedValue != "0" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP);
            switch (CATEG)
            {
                case "F":
                    AuxiliCarregamentos.CarregaFuncionarios(ddlAluno, coEmp, true);
                    lblFlex.Text = "Funcionário(a)";
                    break;
                case "P":
                    AuxiliCarregamentos.carregaProfessores(ddlAluno, coEmp, true, true);
                    lblFlex.Text = "Professor(a)";
                    break;
                case "R":
                    AuxiliCarregamentos.CarregaResponsaveis(ddlAluno, LoginAuxili.ORG_CODIGO_ORGAO, true);
                    lblFlex.Text = "Responsável";
                    break;
                case "A":
                default:
                    AuxiliCarregamentos.CarregaAlunosDaUnidade(ddlAluno, coEmp, true);
                    lblFlex.Text = "Aluno(a)";
                    break;
            }

        }

        #endregion

        protected void ddlCategoria_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDadosFlex(ddlCategoria.SelectedValue);
            CarregaTiposOcorrencias(ddlCategoria.SelectedValue, ddlTipoOcorrencia.SelectedValue);
            CarregaTipoOcorrencia();
        }

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDadosFlex(ddlCategoria.SelectedValue);
        }

        protected void ddlTipoOcorrencia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTiposOcorrencias(ddlCategoria.SelectedValue, ddlTipoOcorrencia.SelectedValue);
        }
    }
}

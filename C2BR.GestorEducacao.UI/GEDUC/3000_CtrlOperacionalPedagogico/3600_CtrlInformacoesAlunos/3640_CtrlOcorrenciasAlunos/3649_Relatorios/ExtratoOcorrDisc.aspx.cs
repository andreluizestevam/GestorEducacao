//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE ALUNOS
// OBJETIVO: RELAÇÃO DE INFORMAÇÕES GERAIS DO ALUNO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
// 20/02/2015| Maxwell Almeida           | Criação do relatório para emissão do extrato de ocorrências disciplinares

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;
using System.ServiceModel;
using System.Configuration;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos._3640_CtrlOcorrenciasDisciplinares;
using C2BR.GestorEducacao.Reports.GSAUD;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos._3640_CtrlOcorrenciasAlunos._3649_Relatorios
{
    public partial class ExtratoOcorrDisc : System.Web.UI.Page
    {
        #region Eventos

        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidade();
                CarregaCategorias();
                CarregaTipoOcorrencia();
                CarregaTiposOcorrencias(ddlCategoria.SelectedValue, ddlTipoOcorrencia.SelectedValue);
                CarregaDadosFlex(ddlCategoria.SelectedValue);
                txtDtEmissao.Text = DateTime.Now.ToString();
            }
        }

        /// <summary>
        /// Processo de Geração do Relatório
        /// </summary>
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            #region Validações

            if (txtDtIni.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data de início do período de busca não foi informado.");
                txtDtIni.Focus();
                return;
            }

            if (txtDtFim.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data de término do período de busca não foi informado.");
                txtDtFim.Focus();
                return;
            }

            #endregion

            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            int coEmp;
            int coAlu;
            int tpOcorrSv;
            string coCateg;
            string coClass;
            string tipoOcorr;
            DateTime dtIni;
            DateTime dtFim;
            string infos = "";
            string parametros = "";
            string noUnid, noCateg, noAlu, noTpOCorrencia, noClassificacao;

            //--------> Inicializa as variáveis
            coEmp = (!string.IsNullOrEmpty(ddlUnidade.SelectedValue) ?  int.Parse(ddlUnidade.SelectedValue) : 0);
            noUnid = ddlUnidade.SelectedItem.Text;
            coCateg = (!string.IsNullOrEmpty(ddlCategoria.SelectedValue) ? ddlCategoria.SelectedValue : "0");
            noCateg = ddlCategoria.SelectedItem.Text;
            coAlu = (!string.IsNullOrEmpty(ddlAluno.SelectedValue) ? int.Parse(ddlAluno.SelectedValue) : 0);
            noAlu = ddlAluno.SelectedItem.Text;
            tpOcorrSv = (!string.IsNullOrEmpty(ddlTpOcorrTbxxx.SelectedValue) ? int.Parse(ddlTpOcorrTbxxx.SelectedValue) : 0);
            noTpOCorrencia = ddlTpOcorrTbxxx.SelectedItem.Text;
            coClass = ddlTipoOcorrencia.SelectedValue;
            noClassificacao = ddlTipoOcorrencia.SelectedItem.Text;

            parametros = "( Unid: " + noUnid + " - Categ: " + noCateg + " - Receptor: " + noAlu + " - Tipo Ocorr: "
                + noTpOCorrencia + " - Classificação: " + noClassificacao;

            #region Valida se as datas inseridas são válidas
            if (!DateTime.TryParse(txtDtIni.Text, out dtIni))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data de início do período de busca informada é uma data inválida.");
                txtDtIni.Focus();
                return;
            }

            if (!DateTime.TryParse(txtDtFim.Text, out dtFim))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Data de término do período de busca informada é uma data inválida.");
                txtDtFim.Focus();
                return;
            }
            #endregion

            parametros += " - Período: de " + dtIni.ToString("dd/MM/yyyy") + " até " + dtFim.ToString("dd/MM/yyyy") + " )";

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            //Parte responsável por coletar o nome da funcionalidade cadastrada no banco e mandar como parâmetro para o 
            //relatório usar no cabeçalho
            #region
            string NO_RELATORIO = "";
            var res = (from adm in ADMMODULO.RetornaTodosRegistros()
                       where adm.nomURLModulo == "GEDUC/3000_CtrlOperacionalPedagogico/3600_CtrlInformacoesAlunos/3640_CtrlOcorrenciasAlunos/3649_Relatorios/ExtratoOcorrDisc.aspx"
                       select new { adm.nomItemMenu }).FirstOrDefault();
            NO_RELATORIO = (res != null ? res.nomItemMenu : "");
            #endregion

            RptExtratoOcorrDisc rpt = new RptExtratoOcorrDisc();
            lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, coEmp, coCateg, coAlu, dtIni, dtFim, coClass, tpOcorrSv, NO_RELATORIO.ToUpper(), txtDtEmissao.Text, LoginAuxili.ORG_CODIGO_ORGAO);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        #endregion

        #region Carregamentos

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

            if (LoginAuxili.TIPO_USU.Equals("R"))
            {
                ddlAluno.DataSource = (from tb07 in TB07_ALUNO.RetornaPelaUnid(coEmp)
                                       join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb07.CO_ALU equals tb08.TB07_ALUNO.CO_ALU
                                       where tb08.CO_ALU == tb07.CO_ALU
                                       && tb08.TB108_RESPONSAVEL.CO_RESP == LoginAuxili.CO_RESP
                                       select new { tb07.NO_ALU, tb07.CO_ALU });

                ddlAluno.DataTextField = "NO_ALU";
                ddlAluno.DataValueField = "CO_ALU";
                ddlAluno.DataBind();
            }
            else
            {
                ddlAluno.DataSource = (from tb07 in TB07_ALUNO.RetornaPelaUnid(coEmp)
                                       select new { tb07.NO_ALU, tb07.CO_ALU });

                ddlAluno.DataTextField = "NO_ALU";
                ddlAluno.DataValueField = "CO_ALU";
                ddlAluno.DataBind();
            }

            ddlAluno.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega as categorias
        /// </summary>
        private void CarregaCategorias()
        {
            AuxiliCarregamentos.CarregaCategoriaOcorrencias(ddlCategoria, true, true);
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
                    AuxiliCarregamentos.CarregaAlunosDaUnidade(ddlAluno, coEmp, true);
                    lblFlex.Text = "Aluno(a)";
                    break;
                default:
                    ddlAluno.Items.Clear();
                    ddlAluno.Items.Insert(0, new ListItem("Todos", "0"));
                    break;
            }
        }

        #endregion

        #region Métodos de Campos

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

        #endregion
    }
}
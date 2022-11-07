//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE AVALIAÇÃO ESCOLAR DO ALUNO
// OBJETIVO: BOLETIM DE DESEMPENHO ESCOLAR DO ALUNO - MODELO 2
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 02/05/2013| André Nobre Vinagre        |Criada a funcionalidade de de modelo de boletim 2
//           |                            |
// ----------+----------------------------+-------------------------------------
// 03/05/2013| André Nobre Vinagre        |Corrigido o linq para pegar as disciplinas que estão
//           |                            |com classificação diferente de "Não se aplica"
//           |                            |
// ----------+----------------------------+-------------------------------------
// 07/05/2013| Victor Martins Machado     | Foi incluído o IF que valida o CNPJ da instituição do usuário logado,
//           |                            | se for o do EDUCANDÁRIO DE MARIA, é utilizado um boletim específico.
//           |                            |
// ----------+----------------------------+-------------------------------------
// 08/05/2013| André Nobre Vinagre        | Corrigido detalhes do boletim gerado para o EDUCANDÁRIO DE MARIA.
//           |                            |
// ----------+----------------------------+-------------------------------------
// 10/05/2013| André Nobre Vinagre        |Criado a verificação pela unidade no linq do boletim
//           |                            |
// ----------+----------------------------+-------------------------------------
// 30/07/2013| Victor Martins Machado     | Criada a validação do tipo de atividade de Recuperação do primeiro
//           |                            | semestre, sigla RECSE, para, se não existir, o mesmo ser criado.
//           |                            |

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
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3599_Relatorios
{
    public partial class BoletEscolModelo2 : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }        

        #region Eventos

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
                CarregaUnidades();
                CarregaAnos();
                CarregaModalidades();
            }
        }

        /// <summary>
        /// Processo de Geração do Relatório
        /// </summary>
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {

//--------> Verifica se existe o tipo de atividade com a sigla RECSE, se não existir, o mesmo é criado
            TB273_TIPO_ATIVIDADE tb273 = (from t in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() where t.CO_SIGLA_ATIV == "RECSE" select t).FirstOrDefault();

            if (tb273 == null)
            {
                tb273 = new TB273_TIPO_ATIVIDADE();
                tb273.CO_CLASS_ATIV = "N";
                tb273.CO_PESO_ATIV = 1;
                tb273.CO_SIGLA_ATIV = "RECSE";
                tb273.CO_SITUA_ATIV = "A";
                tb273.DE_TIPO_ATIV = "Recuperação do 1° semestre";
                tb273.FL_LANCA_NOTA_ATIV = "S";
                tb273.NO_TIPO_ATIV = "Recuperação 1° Semestre";

                TB273_TIPO_ATIVIDADE.SaveOrUpdate(tb273, true);
            }

//--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, infos;
            Boolean boolP_CO_TOT;
//--------> Inicializa as variáveis
            strP_CO_EMP = null;
            strP_CO_MODU_CUR = null;
            strP_CO_CUR = null;
            strP_CO_TUR = null;           
            strP_CO_ANO_REF = null;
            strP_CO_ALU = null;
            boolP_CO_TOT = false;

//--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            strP_CO_EMP = ddlUnidade.SelectedValue;
            strP_CO_MODU_CUR = ddlModalidade.SelectedValue;
            strP_CO_CUR = ddlSerieCurso.SelectedValue;
            strP_CO_TUR = ddlTurma.SelectedValue;
            strP_CO_ANO_REF = ddlAnoRefer.SelectedValue;
            strP_CO_ALU = ddlAlunos.SelectedValue;
            boolP_CO_TOT = cbLinhatotal.Checked;

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptBoletEscolSimples rpt = new RptBoletEscolSimples();

            lRetorno = rpt.InitReport("", LoginAuxili.CO_EMP, infos, int.Parse(ddlUnidade.SelectedValue), strP_CO_ANO_REF, int.Parse(strP_CO_MODU_CUR), int.Parse(strP_CO_CUR), int.Parse(strP_CO_TUR), int.Parse(strP_CO_ALU), boolP_CO_TOT);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

        }                
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidade.Items.Clear();
            ddlUnidade.Items.AddRange(AuxiliBaseApoio.UnidadesDDL(LoginAuxili.IDEADMUSUARIO));
            if (ddlUnidade.Items.FindByValue(LoginAuxili.CO_EMP.ToString()) != null)
                ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaTurma()
        {
            ddlTurma.Items.Clear();
            ddlTurma.Items.AddRange(AuxiliBaseApoio.TurmasDDL(LoginAuxili.CO_EMP, ddlModalidade.SelectedValue, ddlSerieCurso.SelectedValue,ddlAnoRefer.SelectedValue, true));
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            ddlAnoRefer.Items.Clear();
            ddlAnoRefer.Items.AddRange(AuxiliBaseApoio.AnosDDL(LoginAuxili.CO_EMP));
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.Items.Clear();
            ddlModalidade.Items.AddRange(AuxiliBaseApoio.ModulosDDL(LoginAuxili.ORG_CODIGO_ORGAO, true));
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaSerieCurso()
        {
            ddlSerieCurso.Items.Clear();
            ddlSerieCurso.Items.AddRange(AuxiliBaseApoio.SeriesDDL(LoginAuxili.CO_EMP, ddlModalidade.SelectedValue, ddlAnoRefer.SelectedValue, true));
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            ddlAlunos.Items.Clear();
            ddlAlunos.Items.AddRange(AuxiliBaseApoio.AlunosDDL(LoginAuxili.CO_EMP, ddlModalidade.SelectedValue, ddlSerieCurso.SelectedValue, ddlTurma.SelectedValue, ddlAnoRefer.SelectedValue, codigoAluno:true, todos:true));
            ddlAlunos.SelectedValue = "-1";
        }
        #endregion

        #region Eventos de Componentes

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaSerieCurso();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaTurma();
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if(((DropDownList)sender).SelectedValue != "")
                CarregaAnos();
        }

        protected void ddlAnoRefer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(((DropDownList)sender).SelectedValue != "")
                CarregaModalidades();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedValue != "")
                CarregaAluno();
        }

        #endregion
    }
}

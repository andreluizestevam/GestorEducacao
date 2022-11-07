using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2111_PreMatriculaAluno;

namespace C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2113_PreMatriculaAluno
{
    public partial class DeclaracoesPreMatricula : System.Web.UI.Page
    {
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
                CarregarModulos();
                CarregarSerie();
                CarregarTurmas();
                CarregarAluno();
                carregaAno();
                CarregarDeclaracoes();
            }
        }
        //====> Processo de Geração do Relatório
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;


            string strCO_ALU_CAD;
            int coDoc = rbDeclaracoes.SelectedValue != "" ? int.Parse(rbDeclaracoes.SelectedValue) : 0;
            int coAlu = ddlAlunos.SelectedValue != "" ? int.Parse(ddlAlunos.SelectedValue) : 0;
            if (coDoc > 0 && coAlu > 0)
            {
                var lst = (from tb009 in TB009_RTF_DOCTOS.RetornaTodosRegistros()
                           from tb010 in tb009.TB010_RTF_ARQUIVO.DefaultIfEmpty()
                           where tb009.TP_DOCUM == "PR"
                           && tb009.ID_DOCUM == coDoc
                           select new
                           {
                               Pagina = tb010.NU_PAGINA,
                               Titulo = tb009.NM_TITUL_DOCUM,
                               Texto = tb010.AR_DADOS,
                               Codigo = tb009.ID_DOCUM
                           }).OrderBy(x => x.Pagina).FirstOrDefault();


                if (lst != null && lst.Pagina == 1)
                {
                    RptDeclaracoesPreMatricula rpt = new RptDeclaracoesPreMatricula();
                    lRetorno = rpt.InitReport(LoginAuxili.CO_EMP, coAlu, lst.Codigo);
                    Session["Report"] = rpt;
                    Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                    AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Arquivo não cadastrado na tabela de Arquivo RTF - Sigla DCC, Tipo de documento Declaração");
                    return;
                }
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Não foi encontrado os dados necessário para a geração do relatório.");
                return;
            }
        }
        
        #region Carregadores

        /// <summary>
        /// Carrega os Anos
        /// </summary>
        private void carregaAno()
        {
            AuxiliCarregamentos.CarregaAno(ddlAno, LoginAuxili.CO_EMP, false);
        }

        /// <summary>
        /// Carrega os tipos de declarações
        /// </summary>
        private void CarregarDeclaracoes()
        {
            rbDeclaracoes.DataSource = (from rtf in TB009_RTF_DOCTOS.RetornaTodosRegistros()
                                        where rtf.TP_DOCUM == "PR"
                                        select new { rtf.ID_DOCUM, rtf.DE_DOCUM});
            rbDeclaracoes.DataTextField = "DE_DOCUM";
            rbDeclaracoes.DataValueField = "ID_DOCUM";
            rbDeclaracoes.DataBind();
        }

        /// <summary>
        /// Carrega as Modalidades
        /// </summary>
        private void CarregarModulos()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }

        /// <summary>
        /// Carrega as Séries
        /// </summary>
        private void CarregarSerie()
        {
            int modalidade = (ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0);
            AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, LoginAuxili.CO_EMP, false);
        }
        
        /// <summary>
        /// Carrega as Turmas
        /// </summary>
        private void CarregarTurmas(int coMod = 0, int coCur = 0)
        {
            int modalidade = (ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int serieCurso = (ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0);
            AuxiliCarregamentos.CarregaTurmas(ddlTurma, LoginAuxili.CO_EMP, modalidade, serieCurso, false);
        }

        /// <summary>
        /// Carrega os Alunos Matriculados
        /// </summary>        
        private void CarregarAluno()
        {
            int modalidade = (ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int serieCurso = (ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0);
            int turma = (ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0);
            string anoRef = ddlAno.SelectedValue;
            AuxiliCarregamentos.CarregaAlunoMatriculado(ddlAlunos, LoginAuxili.CO_EMP, modalidade, serieCurso, turma, anoRef, false);
        }
        #endregion

        #region Eventos de componentes
        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarSerie();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarTurmas();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarAluno();
        }

        protected void ddlAlunos_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        #endregion
    }
}
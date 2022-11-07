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
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2101_ServSolicItensServicEscolar;

namespace C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2112_ContrDeDeclaracoes
{
    public partial class DeclaracaoParcelasEmAberto : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        #region Eventos iniciais
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            else
            {
                CarregaUnidades();
            }
        }
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            //--------> Variáveis obrigatórias para gerar o Relatório
            int lRetorno;
            DateTime dtInicio, dtFim;
            int strCO_EMP, strCO_MOD_CUR, strCO_CUR, strCO_TUR, strCO_ALU_CAD, strCO_RES, strCO_EMP_REF;
            string strParametrosRelatorio, strINFOS, strCO_ANO_INICIO, strCO_ANO_FIM;
            strCO_EMP = LoginAuxili.CO_EMP;
            strCO_EMP_REF = int.Parse(ddlUnidade.SelectedValue);
            strCO_MOD_CUR = int.Parse(ddlModalidade.SelectedValue);
            strCO_CUR = int.Parse(ddlSerieCurso.SelectedValue);
            strCO_TUR = int.Parse(ddlTurma.SelectedValue);
            strCO_ALU_CAD = int.Parse(ddlAlunos.SelectedValue);
            if (txtDtInicio.Text == "" || txtDtFim.Text == "" || !DateTime.TryParse(txtDtInicio.Text, out dtInicio) || !DateTime.TryParse(txtDtFim.Text, out dtFim))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O período deve ser informado, por favor informe a data de ínicio e uma data posterior para o fim do período. ");
                return;
            }
            strCO_ANO_INICIO = dtInicio.Year.ToString();
            strCO_ANO_FIM = dtFim.Year.ToString();
            strCO_RES = int.Parse(ddlResponsavel.SelectedValue);
            strINFOS = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            if (dtInicio != null && dtFim != null && dtInicio.Date < dtFim.Date )
            {
                RptDeclaracaoParcelasEmAberto rpt = new RptDeclaracaoParcelasEmAberto();
                strParametrosRelatorio = "( Ano: " + strCO_ANO_INICIO + " a " + strCO_ANO_FIM + " - Modalidade: " + ddlModalidade.SelectedItem.ToString() + " - Série: " + ddlSerieCurso.SelectedItem.ToString() + " - Turma: " + ddlTurma.SelectedItem.ToString() + " )";
                lRetorno = rpt.InitReport(strParametrosRelatorio, LoginAuxili.ORG_CODIGO_ORGAO, strCO_EMP, strCO_EMP_REF, strCO_ANO_INICIO, strCO_ANO_FIM, strCO_MOD_CUR, strCO_CUR, strCO_TUR, strCO_ALU_CAD, strCO_RES, dtInicio, dtFim, strINFOS);
                Session["Report"] = rpt;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";

                AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O período deve ser informado, por favor informe a data de ínicio e uma data posterior para o fim do período. ");
                return;
            }
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidade.Items.Clear();
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();
            ddlUnidade.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlUnidade.SelectedValue = "0";

            ddlModalidade.Items.Clear();
            ddlSerieCurso.Items.Clear();
            ddlTurma.Items.Clear();
            ddlResponsavel.Items.Clear();
            ddlAlunos.Items.Clear();

        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaTurma(int? coModuCur, int? coSerie)
        {
            if (coModuCur != null && coSerie != null)
            {
                ddlTurma.Items.Clear();
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where coModuCur == -1 ? 0 == 0 : tb06.CO_MODU_CUR == coModuCur
                                       && coSerie == -1 ? 0 == 0 : tb06.CO_CUR == coSerie
                                       select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });
                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
                if(ddlTurma.Items.Count > 0)
                    ddlTurma.Items.Insert(0, new ListItem("Todos", "-1"));
                ddlTurma.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlTurma.SelectedValue = "0";

                ddlResponsavel.Items.Clear();
                ddlAlunos.Items.Clear();
            }
            else
                ddlTurma.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();
            if(ddlModalidade.Items.Count > 0)
                ddlModalidade.Items.Insert(0, new ListItem("Todos", "-1"));
            ddlModalidade.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlModalidade.SelectedValue = "0";

            ddlSerieCurso.Items.Clear();
            ddlTurma.Items.Clear();
            ddlResponsavel.Items.Clear();
            ddlAlunos.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        /// <param name="coModuCur">Id da modalidade</param>
        private void CarregaSerieCurso(int? coModuCur)
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            //string anoGrade = ddlAnoRefer.SelectedValue;
            if (coModuCur != null)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                            join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb01.CO_CUR equals tb43.CO_CUR
                                            where coModuCur == -1 ? 0 == 0 : tb43.TB44_MODULO.CO_MODU_CUR == coModuCur 
                                            && tb01.CO_EMP == coEmp
                                            select new { tb01.NO_CUR, tb01.CO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
                if(ddlSerieCurso.Items.Count > 0)
                    ddlSerieCurso.Items.Insert(0, new ListItem("Todos", "-1"));
                ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlSerieCurso.SelectedValue = "0";

                ddlTurma.Items.Clear();
                ddlResponsavel.Items.Clear();
                ddlAlunos.Items.Clear();
            }
            else
                ddlSerieCurso.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaResponsaveis(int? codMod, int? codSer, int? codTur)
        {
            if (codMod != null
                && codSer != null
                && codTur != null)
            {
                ddlResponsavel.Items.Clear();
                int codEmp = int.Parse(ddlUnidade.SelectedValue);
                ddlResponsavel.DataSource = (from res in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                             where res.TB07_ALUNO.Where(alu => alu.CO_EMP == codEmp
                                                && codMod == -1 ? 0==0:alu.CO_MODU_CUR == codMod
                                                && codSer == -1 ? 0==0 : alu.CO_CUR == codSer
                                                && codTur == -1 ? 0==0 : alu.CO_TUR == codTur
                                                ).Count() > 0
                                             select new
                                             {
                                                 res.NO_RESP,
                                                 res.CO_RESP
                                             }).OrderBy(g => g.NO_RESP);

                ddlResponsavel.DataTextField = "NO_RESP";
                ddlResponsavel.DataValueField = "CO_RESP";
                ddlResponsavel.DataBind();
                ddlResponsavel.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlResponsavel.SelectedValue = "0";

                ddlAlunos.Items.Clear();
            }
            else
                ddlResponsavel.Items.Clear();
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno(int? codMod, int? codSer, int? codTur, int? codRes)
        {
            if (codMod != null
                && codSer != null
                && codTur != null
                && codRes != null)
            {
                ddlAlunos.Items.Clear();
                int codEmp = int.Parse(ddlUnidade.SelectedValue);
                ddlAlunos.DataSource = (from al in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                        where al.TB07_ALUNO.TB108_RESPONSAVEL.CO_RESP == codRes
                                        && al.TB07_ALUNO.CO_EMP == codEmp
                                        && codMod == -1 ? 0==0 : al.TB07_ALUNO.CO_MODU_CUR == codMod
                                        && codSer == -1 ? 0==0 : al.TB07_ALUNO.CO_CUR == codSer
                                        && codTur == -1 ? 0==0 : al.TB07_ALUNO.CO_TUR == codTur
                                        select new { al.TB07_ALUNO.CO_ALU, al.TB07_ALUNO.NO_ALU }).Distinct().OrderBy(g => g.NO_ALU);

                ddlAlunos.DataTextField = "NO_ALU";
                ddlAlunos.DataValueField = "CO_ALU";
                ddlAlunos.DataBind();
                if(ddlAlunos.Items.Count > 0)
                    ddlAlunos.Items.Insert(0, new ListItem("Todos", "-1"));
                ddlAlunos.Items.Insert(0, new ListItem("Selecione", "0"));
                ddlAlunos.SelectedValue = "0";

            }
            else
                ddlAlunos.Items.Clear();

            
        }
        #endregion

        #region Troca seleção DropDown
        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            int numMod = Convert.ToInt32(((DropDownList)sender).SelectedValue);
            if (numMod == -1)
            {
                CarregaSerieCurso(-1);
                CarregaTurma(-1, -1);
                ddlSerieCurso.SelectedValue = "-1";
                ddlTurma.SelectedValue = "-1";
                ddlSerieCurso.Enabled = false;
                ddlTurma.Enabled = false;
                CarregaResponsaveis(-1, -1, -1);
            }
            else
            {
                ddlSerieCurso.Enabled = true;
                ddlTurma.Enabled = true;
                CarregaSerieCurso(numMod);
            }
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            int numMod = Convert.ToInt32(ddlModalidade.SelectedValue);
            int numSerie = Convert.ToInt32(((DropDownList)sender).SelectedValue);
            CarregaTurma(numMod, numSerie);
        }

        protected void ddlUnidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CarregaModalidades();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            int numMod = Convert.ToInt32(ddlModalidade.SelectedValue);
            int numSerie = Convert.ToInt32(ddlSerieCurso.SelectedValue);
            int numTurma = Convert.ToInt32(ddlTurma.SelectedValue);
            CarregaResponsaveis(numMod, numSerie, numTurma);
        }

        protected void ddlResponsavel_SelectedIndexChanged1(object sender, EventArgs e)
        {
            int numResp = Convert.ToInt32(((DropDownList)sender).SelectedValue);
            int numMod = Convert.ToInt32(ddlModalidade.SelectedValue);
            int numSerie = Convert.ToInt32(ddlSerieCurso.SelectedValue);
            int numTurma = Convert.ToInt32(ddlTurma.SelectedValue);
            CarregaAluno(numMod, numSerie, numTurma, numResp);
        }
        
        #endregion

        

        
    }
}
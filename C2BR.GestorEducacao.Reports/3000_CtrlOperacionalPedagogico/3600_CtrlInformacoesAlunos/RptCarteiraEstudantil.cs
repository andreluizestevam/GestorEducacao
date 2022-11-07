using System;
using System.Linq;
using System.Drawing;
using C2BR.GestorEducacao.Reports.Helper;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects;
using System.Text;
using System.Data.SqlClient;
using System.IO;


namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos
{
    public partial class RptCarteiraEstudantil : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptCarteiraEstudantil()
        {
            InitializeComponent();
        }

        #region InitReport
        public int InitReport(
                                string infos,
                                string parametros,
                                string ano,
                                int coEmp,
                                int coEmpLog,
                                int coModu,
                                int coCur,
                                int coTur,
                                int coAlu,
                                string dtValidade,
                                bool ckfoto = true,
                                bool ckanoLetivo = true,
                                bool ckmodal = true,
                                bool ckCurso = true,
                                bool ckTurma = true
                             )
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;
                this.VisiblePageHeader = true;
                this.VisibleNumeroPage = false;
                this.VisibleDataHeader = false;
                this.VisibleHoraHeader = false;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmpLog);
                if (header == null)
                    return 0;
                string cpfCNPJUnid = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmpLog).CO_CPFCGC_EMP;
                // Setar o header do relatorio
                if ((cpfCNPJUnid == "15280144000116") || (cpfCNPJUnid == "03946574000145"))
                    this.BaseInit(header, ETipoCabecalho.ColegioSupremo);
                else // Setar o header do relatorio
                    this.BaseInit(header);

                #endregion

                //var tb25Img = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp).TB000_INSTITUICAO.Image3.ImageStream;

                //pbLogoP2.ImageUrl = pbLogoP.ImageUrl = "/Library/IMG/Logo_Objetivo_Pequena.png";
                //MemoryStream ms = new MemoryStream(tb25Img);
                //pbLogoP2. = Image.FromStream(ms);
                

                //string nomEmp = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp).NO_FANTAS_EMP;

                //lblUnidFrente.Text = nomEmp;
                //lblUnidVerso.Text = nomEmp;

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                // Retorna os alunos com os dados das matrículas
                var res = (from tb07 in ctx.TB07_ALUNO
                           join tb08 in ctx.TB08_MATRCUR on tb07.CO_ALU equals tb08.CO_ALU
                           join tb44 in ctx.TB44_MODULO on tb08.TB44_MODULO.CO_MODU_CUR equals tb44.CO_MODU_CUR
                           join tb01 in ctx.TB01_CURSO on tb08.CO_CUR equals tb01.CO_CUR
                           join tb129 in ctx.TB129_CADTURMAS on tb08.CO_TUR equals tb129.CO_TUR
                           join tb133 in ctx.TB133_CLASS_CUR on tb01.CO_NIVEL_CUR equals tb133.CO_SIGLA_CLASS_CUR
                           join tb25 in ctx.TB25_EMPRESA on tb07.CO_EMP equals tb25.CO_EMP

                           where (coEmp != 0 ? tb08.CO_EMP == coEmp : 0 == 0)
                           && (coModu != 0 ? tb44.CO_MODU_CUR == coModu : 0 == 0)
                           && (coCur != 0 ? tb01.CO_CUR == coCur : 0 == 0)
                           && (coTur != 0 ? tb129.CO_TUR == coTur : 0 == 0)
                           && (coAlu != 0 ? tb07.CO_ALU == coAlu : 0 == 0)
                           && tb08.CO_ANO_MES_MAT == ano
                           select new CarteiraEstudantil
                           {
                               // Dados Gerais
                               Ano = ano,
                               coMod = tb44.CO_MODU_CUR,
                               coCur = tb01.CO_CUR,
                               dtValidade = dtValidade,
                               noEscola = tb25.NO_FANTAS_EMP,
                               coUnid = tb25.CO_EMP,
                               noEmp = tb25.NO_FANTAS_EMP,
                               empFoto = tb25.Image.ImageStream,

                               // Dados do aluno
                               noAlu = tb07.NO_ALU,
                               noMae = tb07.NO_MAE_ALU,
                               nuNire = tb07.NU_NIRE,
                               nuRg = tb07.CO_RG_ALU,
                               orgRg = tb07.CO_ORG_RG_ALU,
                               ufRg = tb07.CO_ESTA_RG_ALU,
                               nuCpf = tb07.NU_CPF_ALU,
                               dtNascAlu = tb07.DT_NASC_ALU,
                               deModu = tb133.NO_CLASS_CUR,
                               sigCur = tb01.CO_SIGL_CUR,
                               sigTur = tb129.CO_SIGLA_TURMA,
                               coFoto = tb07.Image.ImageStream,

                               //Atribui os Filtros do Parâmetro às Variáveis.
                               ckFoto = ckfoto,
                               ckAnoLet = ckanoLetivo,
                               ckCurso = ckCurso,
                               ckTurma = ckTurma,
                               ckModal = ckmodal,
                           }).OrderBy(o => o.noAlu).ToList();

                // Valida se a consulta retornou algum aluno
                if (res.Count < 1)
                {
                    return -1;
                }

                bsReport.Clear();

                foreach (CarteiraEstudantil at in res)
                {
                    // Pega os dados da unidade selecionada
                    var emp = TB25_EMPRESA.RetornaPelaChavePrimaria(at.coUnid);

                    TB904_CIDADE empCid = TB904_CIDADE.RetornaPelaChavePrimaria(emp.CO_CIDADE);
                    emp.TB83_PARAMETROReference.Load();

                    string Dir = "";
                    if (emp.TB83_PARAMETRO.CO_DIR1 != null)
                        Dir = TB03_COLABOR.RetornaPelaChavePrimaria(emp.CO_EMP, emp.TB83_PARAMETRO.CO_DIR1.Value).NO_COL;

                    at.empEnd = emp.DE_END_EMP + ", " + emp.NU_END_EMP + " - " + empCid.NO_CIDADE + " - " + emp.CO_UF_EMP;
                    at.empTel = emp.CO_TEL1_EMP;
                    at.noWebEmp = emp.NO_WEB_EMP;
                    at.noDirecao = Dir;
                    this.lblUnidFrente.Text = (at.noEscola.Length > 35 ? at.noEscola.Substring(0, 35) : at.noEscola);
                    this.lblUnidVerso.Text = at.noEscola;

                    //Mostra o ano apenas se o usuário tiver marcado o CheckBox de Ano no filtro do relatório.
                    if (at.ckAnoLet == true)
                        this.lblAno.Visible = true;
                    //Mostra a Foto apenas se o usuário tiver marcado o CheckBox de Foto no filtro do relatório.
                    if (at.ckFoto == false)
                        this.pbFoto.Visible = false;

                    bsReport.Add(at);
                }

                return 1;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region Classe de retorno do relatório

        public class CarteiraEstudantil
        {
            // Dados gerais
            public string Ano { get; set; }
            public string empEnd { get; set; }
            public string noEmp { get; set; }
            public byte[] empFoto { get; set; }
            public System.Drawing.Image fotoEmp
            {
                get
                {
                    return this.empFoto != null ? System.Drawing.Image.FromStream(new MemoryStream(this.empFoto)) : null;
                }
            }
            public string empTel { get; set; }
            public string empTelF
            {
                get
                {
                    return this.empTel != null || this.empTel != "" ? String.Format("{0:(###) ####-####}", double.Parse(this.empTel)) : "********";
                }
            }
            public string empEmail { get; set; }
            public string noWebEmp { get; set; }
            public string Endereco
            {
                get
                {
                    return this.empEnd + "\n" + this.empTelF + "\n" + this.empEmail;
                }
            }
            public string noDirecao { get; set; }
            public int coMod { get; set; }
            public int coCur { get; set; }
            public long codBarras
            {
                get
                {
                    return Convert.ToInt64(this.coMod.ToString() + this.coCur.ToString() + this.nuNire.ToString().PadLeft(7, '0'));
                }
            }
            public string dtValidade { get; set; }
            public string noEscola { get; set; }
            public int coUnid { get; set; }

            // Dados do aluno
            public string noAlu { get; set; }
            public string noMae { get; set; }
            public string Mae
            {
                get
                {
                    return this.noMae.Length > 30 ? this.noMae.ToUpper().Substring(0, 27) + "..." : this.noMae.ToUpper();
                }
            }
            public int nuNire { get; set; }
            public string strNire
            {
                get
                {
                    return this.nuNire.ToString().PadLeft(7, '0');
                }
            }
            public string nuRg { get; set; }
            public string orgRg { get; set; }
            public string ufRg { get; set; }
            public string Rg
            {
                get
                {
                    return this.nuRg.Trim() != "" && this.nuRg != null ? this.nuRg + " " + this.orgRg + " " + this.ufRg : "********";
                }
            }
            public string nuCpf { get; set; }
            public string Cpf
            {
                get
                {
                    return this.nuCpf != null && this.nuCpf != "" ? this.nuCpf.Substring(0, 3) + "." + this.nuCpf.Substring(3, 3) + "." + this.nuCpf.Substring(6, 3) + "-" + this.nuCpf.Substring(9, 2) : "********";
                }
            }
            public DateTime? dtNascAlu { get; set; }
            public string dtNasc
            {
                get
                {
                    return this.dtNascAlu != null ? this.dtNascAlu.Value.ToString("dd/MM/yyyy") : "********";
                }
            }
            public string deModu { get; set; }
            public string sigCur { get; set; }
            public string sigTur { get; set; }

            /// <summary>
            /// Apresenta as informações conforme desejo do usuário ao imprimir a carteirinha.
            /// </summary>
            public bool ckFoto { get; set; }
            public bool ckAnoLet { get; set; }
            public bool ckModal { get; set; }            
            public bool ckCurso { get; set; }
            public bool ckTurma { get; set; }
            public string Curso
            {
                get
                {
                    string infosCurso = "";

                    if (this.ckModal == true)
                        infosCurso = this.deModu;
                    if (this.ckCurso == true)
                        infosCurso += (infosCurso != "" ? " - " + this.sigCur : this.sigCur);
                    if (this.ckTurma == true)
                        infosCurso += (infosCurso != "" ? " - " + this.sigTur : this.sigTur);

                    return infosCurso;
                }
            }

            public byte[] coFoto { get; set; }
            public System.Drawing.Image foto 
            {
                get
                {
                    return this.coFoto != null ? System.Drawing.Image.FromStream(new MemoryStream(this.coFoto)) : null;
                }
            }
        }

        #endregion
    }
}

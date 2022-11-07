using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.Reports.Helper;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Globalization;

namespace C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2114_ControleCredito.TransfCredito
{
    public partial class RptCreditosAlunos : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptCreditosAlunos()
        {
            InitializeComponent();
        }

        #region Init Report

                public int InitReport(string parametros,
                                string infos,
                                int alunoTrans,
                                int coEmp,
                                int materia,
                                int alunoReceb,
                                int modalidade,
                                int serieCurso,
                                int Turma,
                                string dataIni,
                                string dataFim,
                                int anoRef
                                )
                 {

            
            try
            {
                #region Setar o Header e as Labels

                DateTime dataIni1;
                if (!DateTime.TryParse(dataIni, out dataIni1))
                {
                    return 0;
                }

                DateTime dataFim1;
                if (!DateTime.TryParse(dataFim, out dataFim1))
                {
                    return 0;
                }
                string anoRef1 = anoRef.ToString();

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                lblParametros.Text = parametros;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
            #region Query

                var res = (from tb122 in TB122_ALUNO_CREDI.RetornaTodosRegistros()

                           join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb122.TB44_MODULO.CO_MODU_CUR equals tb44.CO_MODU_CUR
                           join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb122.CO_CUR equals tb01.CO_CUR
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb122.CO_ALU equals tb07.CO_ALU
                           join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb07.CO_ALU equals tb08.CO_ALU
                           join at in TB07_ALUNO.RetornaTodosRegistros() on tb122.CO_ALU equals at.CO_ALU
                           join ar in TB07_ALUNO.RetornaTodosRegistros() on tb122.CO_ALU_CRED equals ar.CO_ALU
                           join coM in TB02_MATERIA.RetornaTodosRegistros() on tb122.CO_MAT equals coM.CO_MAT
                           join m in TB107_CADMATERIAS.RetornaTodosRegistros() on coM.ID_MATERIA equals m.ID_MATERIA

                           where (modalidade != 0 ? tb44.CO_MODU_CUR == modalidade : 0 == 0)
                           &&
                                 (serieCurso != 0 ? tb01.CO_CUR == serieCurso : 0 == 0)
                           &&
                                 (Turma != 0 ? tb08.CO_TUR == Turma : 0 == 0)
                           &&
                                 (alunoTrans != 0 ? tb122.CO_ALU == alunoTrans : 0 == 0)
                           &&
                                 (alunoReceb != 0 ? tb122.CO_ALU_CRED == alunoReceb : 0 == 0)
                           &&
                                 (materia != 0 ? tb122.CO_MAT == materia : 0 == 0)
                           &&
                                 (anoRef1 != "0" ? tb08.CO_ANO_MES_MAT == anoRef1 : 0 == 0)
                           &&
                                 ((tb122.DT_CAD_CRED >= dataIni1) && (tb122.DT_CAD_CRED <= dataFim1))


                           select new MovimentacaoTransMateria
                           {
                               //Coleta Informações do Aluno
                               Aluno = at.NO_ALU,
                               ALunoCPF = at.NU_CPF_ALU,
                               AlunoNire = at.NU_NIRE,

                               //Coleta Informações do Beneficiário
                               Beneficiario = ar.NO_ALU,
                               BeneficiarioCPF = ar.NU_CPF_ALU,
                               BeneficiarioTel = ar.NU_TELE_CELU_ALU,
                               BeneficiarioTelRes = ar.NU_TELE_RESI_ALU,

                               //Coleta Informações Gerais
                               materia = m.NO_MATERIA,
                               codMat = m.NO_SIGLA_MATERIA,
                               Curso = tb01.NO_CUR,
                               AnoCurso = tb08.CO_ANO_MES_MAT,
                               dt_Movim = tb122.DT_CAD_CRED,
                               valor = tb122.VL_CRED,
                               situacaoCred = tb122.CO_SITUA_CRED

                           }).OrderBy(m => m.dt_Movim).ThenBy(n => n.Aluno).ToList();

            #endregion

        
                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (MovimentacaoTransMateria at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class MovimentacaoTransMateria
        {
            //Informações do Aluno
            public string Aluno { get; set; }
            public string ALunoCPF { get; set;}
            public string AlunoCPFValid
            {
                get
                {
                    if ((ALunoCPF == null) || (ALunoCPF == ""))
                    {
                        return "***.***.***-**";
                    }

                    return ALunoCPF.Insert(3, ".").Insert(7, ".").Insert(11, "-");
                }
            }
            public int AlunoNire { get; set; }
            public string AlunoCPFNire
            {
                get
                {
                    return this.AlunoCPFValid + "  /  " + this.AlunoNire;
                }
            }

            //Informações do Beneficiário
            public string Beneficiario { get; set; }
            public string BeneficiarioCPF { get; set; }
            public string BeneficiarioCPFValid
            {
                get
                {
                    if ((BeneficiarioCPF == null) || (BeneficiarioCPF == ""))
                    {
                        return "***.***.***-**";
                    }

                    return BeneficiarioCPF.Insert(3, ".").Insert(7, ".").Insert(11, "-");
                } 
            }
            public string BeneficiarioTel { get; set; }
            public string BeneficiarioTelValid
            {
                get
                {
                    if ((BeneficiarioTel == null) || (BeneficiarioTel == ""))
                    {
                        return "-";
                    }

                    return BeneficiarioTel.Insert(0, "(").Insert(3, ")").Insert(4, " ").Insert(9, "-");
                }
            }
            public string BeneficiarioCPFeTEL
            {
                get
                {
                    return this.BeneficiarioCPFValid + "  /  " + this.BeneficiarioTelValid + "  /  " + this.BeneficiarioTelResValid;
                }

            }
            public string BeneficiarioTelRes { get; set; }
            public string BeneficiarioTelResValid
            {
                get
                {
                    if ((BeneficiarioTelRes == null) || (BeneficiarioTelRes == ""))
                    {
                        return "-";
                    }

                    return BeneficiarioTelRes.Insert(0, "(").Insert(3, ")").Insert(4, " ").Insert(9, "-");
                }
            }

            //Informações da Matéria e Curso
            public string materia { get; set; }
            public string codMat { get; set; }
            public string MatEcod
            {
                get
                {
                     return this.materia + " ( " + this.codMat + " )";
                }
            }
            public string Curso { get; set;}

            //Informações de Data
            public string AnoCurso { get; set; }
            public DateTime dt_Movim { get; set; }
            public string dataTrans
            {
                get
                {
                    return this.dt_Movim.ToString("dd/MM/yy");
                }
            }

            //Informações do Crédito
            public decimal valor { get; set; }
            public string situacaoCred { get; set; }
            public string SituacaoCredito
            {
                get
                {
                    string s = "";

                    switch(this.situacaoCred)
                    {
                        case "A":
                            s = "Ativo";
                        break;

                        case "C":
                             s = "Cancelado";
                        break;

                        case "U":
                             s = "Utilizado";
                        break;

                        case "S":
                            s = "Suspenso";
                        break;
                    }
                    return s;
                }
            }


        }
    }
 }


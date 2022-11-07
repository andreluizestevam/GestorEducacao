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
    public partial class RptTransCredMat : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptTransCredMat()
        {
            InitializeComponent();
        }

          #region Init Report

                public int InitReport(string parametros,
                                string infos,
                                int aluno,
                                int coEmp,
                                int modalidade,
                                int serieCurso,
                                int turma,
                                string cpf,
                                string tipoRelatorio,
                                int ano
                                )
                 {


                     try
                     {
                         #region Setar o Header e as Labels

                         string anoRef1 = ano.ToString();
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

                         if (tipoRelatorio == "Aluno")
                         {

                             var lst = (from tb122 in TB122_ALUNO_CREDI.RetornaTodosRegistros()

                                        join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb122.TB44_MODULO.CO_MODU_CUR equals tb44.CO_MODU_CUR
                                        join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb122.CO_CUR equals tb01.CO_CUR
                                        join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb122.CO_ALU equals tb07.CO_ALU
                                        join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb07.CO_ALU equals tb08.CO_ALU
                                        join al in TB07_ALUNO.RetornaTodosRegistros() on tb122.CO_ALU equals al.CO_ALU
                                        join be in TB07_ALUNO.RetornaTodosRegistros() on tb122.CO_ALU_CRED equals be.CO_ALU
                                        join coM in TB02_MATERIA.RetornaTodosRegistros() on tb122.CO_MAT equals coM.CO_MAT
                                        join m in TB107_CADMATERIAS.RetornaTodosRegistros() on coM.ID_MATERIA equals m.ID_MATERIA

                                        where (modalidade != 0 ? tb44.CO_MODU_CUR == modalidade : 0 == 0)
                                        &&
                                              (serieCurso != 0 ? tb01.CO_CUR == serieCurso : 0 == 0)
                                        &&
                                              (turma != 0 ? tb08.CO_TUR == turma : 0 == 0)
                                        &&
                                              (aluno != 0 ? be.CO_ALU == aluno : 0 == 0)
                                        &&
                                              (anoRef1 != "0" ? tb08.CO_ANO_MES_MAT == anoRef1 : 0 == 0)


                                        select new ExtratoCreditoTransMat
                                        {
                                            //Coleta Informações do Aluno Beneficiado
                                            Aluno = be.NO_ALU,
                                            ALunoCPF = be.NU_CPF_ALU,
                                            AlunoNire = be.NU_NIRE,
                                            AlunoTel = be.NU_TELE_CELU_ALU,
                                            alunoTelRes = be.NU_TELE_RESI_ALU,

                                            //Coleta Informações da Origem do Crédito
                                            cpfOrig = al.NU_CPF_ALU,

                                            //Coleta Informações Gerais
                                            materia = m.NO_MATERIA,
                                            codMat = m.NO_SIGLA_MATERIA,
                                            curso = tb01.CO_SIGL_CUR,

                                            //Informações do Crédito
                                            situacaoCred = tb122.CO_SITUA_CRED,
                                            valorCred = tb122.VL_CRED,
                                            qcmCred = coM.QT_CRED_MAT,
                                            dtCred = tb122.DT_CRED.Value,

                                        //}).GroupBy(m => m.Aluno).ToList();
                                        }).OrderBy(p => p.Aluno).ThenBy(p => p.dtCred);

                             var res = lst.ToList();

                             // Erro: não encontrou registros
                             if (res.Count == 0)
                                 return -1;

                             // Seta os alunos no DataSource do Relatorio
                             bsReport.Clear();


                             int i = 1;
                             int countA = 0;
                             int countS = 0;
                             int countU = 0;
                             int countC = 0;

                             foreach (ExtratoCreditoTransMat at in res)
                             {
                                 at.count = i;
                                 bsReport.Add(at);
                                 i++;

                                 if (at.situacaoCred == "A") { countA++; }
                                 if (at.situacaoCred == "S") { countS++; }
                                 if (at.situacaoCred == "U") { countU++; }
                                 if (at.situacaoCred == "C") { countC++; }
                             }
                             string infosCreditos = "( " + countA + " Ativos - " + countU + " Utilizados - " + countC + " Suspensos - " + countC + " Cancelados )";
                             lblInfosSituCreditos.Text = "";

                             return 1;
                         }

                         else
                         {
                             var lst = (from tb122 in TB122_ALUNO_CREDI.RetornaTodosRegistros()

                                        join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb122.TB44_MODULO.CO_MODU_CUR equals tb44.CO_MODU_CUR
                                        join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb122.CO_CUR equals tb01.CO_CUR
                                        join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb122.CO_ALU equals tb07.CO_ALU
                                        join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb07.CO_ALU equals tb08.CO_ALU
                                        join al in TB07_ALUNO.RetornaTodosRegistros() on tb122.CO_ALU equals al.CO_ALU
                                        join be in TB07_ALUNO.RetornaTodosRegistros() on tb122.CO_ALU_CRED equals be.CO_ALU
                                        join coM in TB02_MATERIA.RetornaTodosRegistros() on tb122.CO_MAT equals coM.CO_MAT
                                        join m in TB107_CADMATERIAS.RetornaTodosRegistros() on coM.ID_MATERIA equals m.ID_MATERIA

                                        where (cpf == be.NU_CPF_ALU)
                                         &&
                                              (anoRef1 != "0" ? tb08.CO_ANO_MES_MAT == anoRef1 : 0 == 0)

                                        select new ExtratoCreditoTransMat
                                        {
                                            //Coleta Informações do Aluno Beneficiado
                                            Aluno = be.NO_ALU,
                                            ALunoCPF = be.NU_CPF_ALU,
                                            AlunoNire = be.NU_NIRE,
                                            AlunoTel = be.NU_TELE_CELU_ALU,
                                            alunoTelRes = be.NU_TELE_RESI_ALU,

                                            //Coleta Informações da Origem do Crédito
                                            cpfOrig = al.NU_CPF_ALU,

                                            //Coleta Informações Gerais
                                            materia = m.NO_MATERIA,
                                            codMat = m.NO_SIGLA_MATERIA,
                                            curso = tb01.CO_SIGL_CUR,

                                            //Informações do Crédito
                                            situacaoCred = tb122.CO_SITUA_CRED,
                                            valorCred = tb122.VL_CRED,
                                            qcmCred = coM.QT_CRED_MAT,
                                            dtCred = tb122.DT_CRED.Value,

                                            
                                            //int lstA = (from situ in TB122_ALUNO_CREDI.RetornaTodosRegistros()
                                            //            select new 
                                            //            {
                                            //               situ.CO_SITUA_CRED
                                            //            }

                                        }).OrderBy(p => p.Aluno).ThenBy(p => p.dtCred);

                             var res = lst.ToList();

                             // Erro: não encontrou registros
                             if (res.Count == 0)
                                 return -1;

                             // Seta os alunos no DataSource do Relatorio
                             bsReport.Clear();

                             string v = "";
                             int i = 1;
                             int countA = 0;
                             int countS = 0;
                             int countU = 0;
                             int countC = 0;

                             foreach (ExtratoCreditoTransMat at in res)
                             {
                                 //v = at.Aluno;
                                 //if (at.Aluno == v)
                                 //{
                                 at.count = i;
                                 bsReport.Add(at);
                                 i++;
                                 //}

                                 if (at.situacaoCred == "A") { countA++; }
                                 if (at.situacaoCred == "S") { countS++; }
                                 if (at.situacaoCred == "U") { countU++; }
                                 if (at.situacaoCred == "C") { countC++; }
                             }
                             string infosCreditos = "( " + countA + " Ativos - " + countU + " Utilizados - " + countC + " Suspensos - " + countC + " Cancelados )";
                             lblInfosSituCreditos.Text = infosCreditos;
                             lblInfosSituCreditos.Visible = true;

                             return 1;
                         }
                     }
                     catch { return 0; }
        }

        #endregion

                public class ExtratoCreditoTransMat
        {

            //Informações do Aluno
            public string Aluno { get; set; }
            public string AlunoValidaContagem { get; set; }
            public string AlunoUpper
            {
                get
                {
                    return Aluno.ToUpper();
                }
            }
            public string ALunoCPF { get; set;}
            public string AlunoCPFValid
            {
                get
                {
                    if ((ALunoCPF == null) || (ALunoCPF == ""))
                    {
                        return "---";
                    }

                    return ALunoCPF.Insert(3, ".").Insert(7, ".").Insert(11, "-");
                }
            }
            public int AlunoNire { get; set; }
            public string AlunoTel { get; set; }
            public string AlunoTelValid
            {
                get
                {
                    if ((AlunoTel == null) || (AlunoTel == ""))
                    {
                        return "-";
                    }

                    return AlunoTel.Insert(0, "(").Insert(3, ")").Insert(4, " ").Insert(9, "-");
                }
            }
            public string alunoTelRes { get; set; }
            public string AlunoTelResValid
            {
                get
                {
                    if ((alunoTelRes == null) || (alunoTelRes == ""))
                    {
                        return "-";
                    }

                    return alunoTelRes.Insert(0, "(").Insert(3, ")").Insert(4, " ").Insert(9, "-");
                }
            }
            public string alunoEmail { get; set; }
            public string AlunoEmailValid
            {
                get
                {
                    if ((alunoEmail == null) || (alunoEmail == ""))
                    {
                        return "--";
                    }
                    return alunoEmail;
                }
            }
            public string AlunoInfos
            {
                get
                {
                    return "CPF: " + this.AlunoCPFValid + " - Nire: " + this.AlunoNire + " - Telefone(s): " + this.AlunoTelValid + " - " + this.AlunoTelResValid + " - Email: " + AlunoEmailValid;
                }
            }
   
            //Informações do Aluno
            public string cpfOrig { get; set; }
            public string cpfOrigValid
            {
                get
                {
                    if((cpfOrig == null) || (cpfOrig == ""))
                    {
                        return "--";
                    }
                    return cpfOrig.Insert(3, ".").Insert(7, ".").Insert(11, "-");
                }
            }

            //Informações do Crédito
            public decimal valorCred { get; set; }
            public int? qcmCred { get; set; }
            public string qcmCredValid
            {
                get
                {
                    if(qcmCred == null)
                    {
                        return "--";
                    }
                    return qcmCred.ToString();
                }
            }
            public string situacaoCred { get; set; }
            public string SituacaoCredito
            {
                get
                {
                    string s = "";

                    switch (this.situacaoCred)
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
            public DateTime dtCred { get; set; }
            public string dt_cred_format
            {
                get
                {
                    return this.dtCred.ToString("dd/MM/yy");
                }
            }

            //Informações da situação do Crédito
            public int TotalAtivos { get; set; }
            public int totalUtilizados { get; set; }
            public int totalCancelados { get; set; }
            public int totalSuspensos { get; set; }

            public int count { get; set; }

            //INformações da Matéria
            public string materia { get; set; }
            public string curso { get; set; }
            public string codMat { get; set; }
            public string MatEcod
            {
                get
                {
                    return this.materia + " - (Código: " + this.codMat + " )";
                }
            }


             
        }
    }

    }

          #endregion
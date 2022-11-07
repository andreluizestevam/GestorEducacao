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

namespace C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar
{
    public partial class RptDebitosDocumentosAlunos : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptDebitosDocumentosAlunos()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                        string infos,
                        int coEmp,
                        int unidade,
                        int anoRef,
                        int modalidade,
                        int serieCurso,
                        int Turma,
                        int aluno,
                        string coSit
                        )
        {


            try
            {
                #region Setar o Header e as Labels

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

                //TB07_ALUNO tb07ob = TB07_ALUNO.RetornaPelaChavePrimaria(aluno, unidade);
                //TB108_RESPONSAVEL tb108ob = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(tb07ob.TB108_RESPONSAVEL.CO_RESP);

                //var res = (from tb208 in TB208_CURSO_DOCTOS.RetornaTodosRegistros()
                //           join tb121 in TB121_TIPO_DOC_MATRICULA.RetornaTodosRegistros() on tb208.CO_TP_DOC_MAT equals tb121.CO_TP_DOC_MAT
                //           where
                //          (tb208.CO_CUR == serieCurso)
                //           select new DebitoDocumentosAlunos
                //           {
                //               cotipdoc = tb208.CO_TP_DOC_MAT,
                //               NomeDocumento = tb121.DE_TP_DOC_MAT

                //           }).Distinct().OrderBy(m => m.NomeDocumento).ToList();

                var res = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb08.CO_ALU equals tb07.CO_ALU
                           join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tb07.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP
                           join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                           join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb08.TB44_MODULO.CO_MODU_CUR equals tb44.CO_MODU_CUR
                           join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb129.CO_TUR
                           where (unidade != 0 ? tb08.CO_EMP == unidade : 0 == 0)
                           && (anoRef1 != "0" ? tb08.CO_ANO_MES_MAT == anoRef1 : 0 == 0)
                           && (modalidade != 0 ? tb08.TB44_MODULO.CO_MODU_CUR == modalidade : 0 == 0)
                           && (serieCurso != 0 ? tb08.CO_CUR == serieCurso : 0 == 0)
                           && (Turma != 0 ? tb08.CO_TUR == Turma : 0 == 0)
                           && (aluno != 0 ? tb08.CO_ALU == aluno : 0 == 0)
                           select new DebitoDocumentosAlunos
                           {
                               //Informações do aluno
                               Aluno = tb07.NO_ALU,
                               coAlu = tb07.CO_ALU,
                               nireAluno = tb07.NU_NIRE,

                               //Informações do Responsável
                               Responsavel = tb108.NO_RESP,
                               nuTelCel = tb108.NU_TELE_CELU_RESP,
                               nuTelCom = tb108.NU_TELE_COME_RESP,
                               nuTelRes = tb108.NU_TELE_RESI_RESP,

                               //Informações da matrícula
                               noModalidade = tb44.DE_MODU_CUR,
                               noSerie = tb01.NO_CUR,
                               noTurma = tb129.NO_TURMA,

                               //Informações de Auxílio
                               coCur = tb08.CO_CUR,
                           }).OrderBy(y => y.noModalidade).ThenBy(z => z.noSerie).ThenBy(w => w.Aluno).ToList();

                #endregion


                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                //Variáveis usadas para Contabilizar o total geral
                int countP = 0;
                int countE = 0;
                int countT = 0;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (DebitoDocumentosAlunos at in res)
                {
                    //var resultMat = 

                    //if (coSit == "0")
                    //{
                        //Faz uma listagem de todos os documentos requeridos para o curso do aluno em questão.
                        var resultMat = (from tb208 in TB208_CURSO_DOCTOS.RetornaTodosRegistros()
                                         join tb121 in TB121_TIPO_DOC_MATRICULA.RetornaTodosRegistros() on tb208.CO_TP_DOC_MAT equals tb121.CO_TP_DOC_MAT
                                         where
                                        (tb208.CO_CUR == at.coCur)
                                         select new Documentos
                                         {
                                             cotipdoc = tb208.CO_TP_DOC_MAT,
                                             NomeDocumento = tb121.DE_TP_DOC_MAT
                                         }).Distinct().OrderBy(m => m.NomeDocumento).ToList();
                    //}
                    //else if (coSit == "Ent")
                    //{
                        //Faz uma listagem de todos os documentos requeridos para o curso do aluno em questão.
                        //var resultMat = (from tb208 in TB208_CURSO_DOCTOS.RetornaTodosRegistros()
                        //                 join tb121 in TB121_TIPO_DOC_MATRICULA.RetornaTodosRegistros() on tb208.CO_TP_DOC_MAT equals tb121.CO_TP_DOC_MAT
                        //                 where
                        //                (tb208.CO_CUR == at.coCur)
                        //                 select new Documentos
                        //                 {
                        //                     cotipdoc = tb208.CO_TP_DOC_MAT,
                        //                     NomeDocumento = tb121.DE_TP_DOC_MAT
                        //                 }).Distinct().OrderBy(m => m.NomeDocumento).ToList();
                    //}
                    //variáveis usadas para fazer a contagem da quantidade de Documentos Entregues e Pendentes
                    int countEnt = 0;
                    int countPen = 0;
                    int countTot = 0;
                    bool re = false;
                    //Verifica para cada documento requerido neste curso, se existe um registro com o mesmo na lista de documentos entregues pelo aluno,
                    //caso exista a situação fica marcada como Entregue, caso contrário como Pendente.
                    foreach (Documentos doc in resultMat)
                    {
                        re = (from tb120 in TB120_DOC_ALUNO_ENT.RetornaTodosRegistros()
                                   where
                                        (tb120.CO_ALU == at.coAlu)
                                   && (tb120.CO_TP_DOC_MAT == doc.cotipdoc)
                                   select new { tb120 }).Any();

                        if (re == false)
                        { doc.situacao = "Pendente"; countPen++; countP++; countTot++; countT++; }
                        else
                        { doc.situacao = "Entregue"; countEnt++; countE++; countTot++; countT++; }
                    }

                    //Concatena a string que será apresentada no rodapé de cada Aluno tratando se o usuário emitiu somente os pendentes ou entregues ou todos
                    if(coSit == "0")
                        at.totalAlunoDoc = " Total: " + countTot + " Documento(s). " + countEnt + " Entregue(s) e " + countPen + " Pendente(s) do Aluno " + at.Aluno + ".";
                    else if(coSit == "Pen")
                        at.totalAlunoDoc = " Total: " + countTot + " Documento(s). " + countPen + " Pendente(s) do Aluno " + at.Aluno + ".";
                    else if(coSit == "Ent")
                        at.totalAlunoDoc = " Total: " + countTot + " Documento(s). " + countEnt + " Entregue(s) do Aluno " + at.Aluno + ".";

                    //Atribui o resultado da lista à propriedade documento inclusa na classe DebitoDocumentosAlunos
                    at.Documentos = resultMat;
                    bsReport.Add(at);
                }

                //Concatena a string que aprensentará o total geral nos parâmetros escolhidos
                this.lblInfosTot.Text = " Total geral: " + countT + " Documentos." + countE + " Entregue(s) e " + countP + " Pendentes.";

                return 1;
            }
            catch { return 0; }
        }


        #endregion

        public class Documentos
        {
            //Informações do DOcumento
            public string NomeDocumento { get; set; }
            public int cotipdoc { get; set; }
            public string situacao { get; set; }
        }

        public class DebitoDocumentosAlunos
        {

            public DebitoDocumentosAlunos()
            {
                this.Documentos = new List<Documentos>();
            }

            public List<Documentos> Documentos { get; set; }
            public string totalAlunoDoc { get; set; }

            //Informações do Aluno e Responsável
            public string Aluno { get; set; }
            public int coAlu { get; set; }
            public int nireAluno { get; set; }
            public string AlunoValid
            {
                get
                {
                    return this.nireAluno + " - " + this.Aluno;
                }
            }

            public string noModalidade { get; set; }
            public string noSerie { get; set; }
            public string noTurma { get; set; }
            public string dadosMatricValid
            {
                get
                {
                    return this.noModalidade + " - " + this.noSerie + " - " + this.noTurma;
                }
            }

            public string Responsavel { get; set; }
            public int coCur { get; set; }
            public string nuTelCel { get; set; }
            public string nuTelCelValid
            {
                get
                {
                    if (string.IsNullOrEmpty(this.nuTelCel))
                    {
                        return "Cel: -";
                    }

                    return "Cel: " + this.nuTelCel.Insert(0, "(").Insert(3, ")").Insert(4, " ").Insert(9, "-");
                }
            }

            public string nuTelRes { get; set; }
            public string nuTelResValid
            {
                get
                {
                    if (string.IsNullOrEmpty(this.nuTelRes))
                    {
                        return "Res: -";
                    }

                    return "Res: " + this.nuTelRes.Insert(0, "(").Insert(3, ")").Insert(4, " ").Insert(9, "-");
                }
            }

            public string nuTelCom { get; set; }
            public string nuTelComValid
            {
                get
                {
                    if (string.IsNullOrEmpty(this.nuTelCom))
                    {
                        return "Com: -";
                    }

                    return "Com: " + this.nuTelCom.Insert(0, "(").Insert(3, ")").Insert(4, " ").Insert(9, "-");
                }
            }

            public string TodosOsTels
            {
                get
                {
                    return this.nuTelCelValid + " - " + this.nuTelResValid + " - " + this.nuTelComValid;
                }
            }

            public int totalEntregues { get; set; }
            public int totalPendentes { get; set; }
            public string infosTotais { get; set; }
        }
    }
}


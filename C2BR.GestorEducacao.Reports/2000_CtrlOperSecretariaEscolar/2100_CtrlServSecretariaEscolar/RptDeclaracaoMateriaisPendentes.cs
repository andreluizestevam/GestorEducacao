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
    public partial class RptDeclaracaoMateriaisPendentes : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptDeclaracaoMateriaisPendentes()
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
                        int aluno
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

                TB07_ALUNO tb07ob = TB07_ALUNO.RetornaPelaChavePrimaria(aluno, unidade);
                TB108_RESPONSAVEL tb108ob = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(tb07ob.TB108_RESPONSAVEL.CO_RESP);
                TB08_MATRCUR t08 = TB08_MATRCUR.RetornaPeloAluno(tb07ob.CO_ALU, anoRef1);
                TB44_MODULO t44 = TB44_MODULO.RetornaPelaChavePrimaria(t08.TB44_MODULO.CO_MODU_CUR);
                TB01_CURSO t01 = TB01_CURSO.RetornaPeloCoCur(t08.CO_CUR);
                TB06_TURMAS t6 = TB06_TURMAS.RetornaPeloCodigo(t08.CO_TUR.Value);
                TB129_CADTURMAS t129 = TB129_CADTURMAS.RetornaPelaChavePrimaria(t08.CO_TUR.Value);   

                var res = (from tb208 in TB208_CURSO_DOCTOS.RetornaTodosRegistros()
                           join tb121 in TB121_TIPO_DOC_MATRICULA.RetornaTodosRegistros() on tb208.CO_TP_DOC_MAT equals tb121.CO_TP_DOC_MAT
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb208.CO_EMP equals tb25.CO_EMP
                           where
                           (tb208.CO_CUR == serieCurso)
                           select new DebitoDocumentosAlunos
                           {
                               cotipdoc = tb208.CO_TP_DOC_MAT,
                               NomeDocumento = tb121.DE_TP_DOC_MAT,
                               nomeUnidade = tb25.NO_FANTAS_EMP,

                               Responsavel = tb108ob.NO_RESP,
                               nuTelCel = tb108ob.NU_TELE_CELU_RESP,
                               nuTelCom = tb108ob.NU_TELE_COME_RESP,
                               nuTelRes = tb108ob.NU_TELE_RESI_RESP,
                               CEPResp = tb108ob.CO_CEP_RESP,
                               ufResp = tb108ob.CO_ESTA_RESP,


                           }).Distinct().OrderBy(m => m.NomeDocumento).ToList();

                #endregion


                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                int countP = 0;
                int countE = 0;
                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (DebitoDocumentosAlunos at in res)
                {

                    bool re = (from tb120 in TB120_DOC_ALUNO_ENT.RetornaTodosRegistros()
                               where
                                    (tb120.CO_ALU == aluno)
                               && (tb120.CO_TP_DOC_MAT == at.cotipdoc)
                               select new { tb120 }).Any();

                    if (re == true)
                        countE++;

                    else
                        countP++;

                    at.situacao = (re == true ? "Entregue" : "Pendente");   

                    bsReport.Add(at);
                }

                lblInfosTotais.Text = countE + " Entregue(s) - " + countP + " Pendente(s).";

                string turno = "";
                switch (t6.CO_PERI_TUR)
                {
                    case "M":
                        turno = "Matutino";
                        break;

                    case "V":
                        turno = "Vespertino";
                        break;

                    case "N":
                        turno = "Noturno";
                        break;
                }

                string textDeclar = "       Apresentamos abaixo o quadro com a relação de documentos que fazem parte do processo de matrícula e documentação do aluno "
                        + tb07ob.NO_ALU + ", Número Interno de Registro Escolar (NIRE) " + tb07ob.NU_NIRE + ", matrículado neste ano letivo "
                        + anoRef1 + ", modalidade de ensino " + t44.DE_MODU_CUR + ", Curso/Série " + t01.NO_CUR + ", Turma " + t129.NO_TURMA + ", Turno " + turno;

                lblDeclar.Text = textDeclar;

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class DebitoDocumentosAlunos
        {
            //Informações do Aluno e Responsável
            public string Aluno { get; set; }
            public string Responsavel { get; set; }
            public string EndeRespons { get; set; }
            public int? cidaRepons { get; set; }
            public string cidaResponsValid
            {
                get
                {
                    if (this.cidaRepons.HasValue)
                        return TB904_CIDADE.RetornaPelaChavePrimaria(this.cidaRepons.Value).NO_CIDADE;

                    else
                        return ""; 
                }
            }
            public string CEPResp { get; set; }
            public string ufResp{ get; set; }

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

            //Informações do DOcumento
            public string nomeUnidade { get; set; }

            public string NomeDocumento { get; set; }
            public int cotipdoc { get; set; }
            public string situacao { get; set; }

            public int totalEntregues { get; set; }
            public int totalPendentes { get; set; }
            public string infosTotais { get; set; }
        }
    }
}


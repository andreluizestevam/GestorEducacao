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

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario
{
    public partial class RptDocumentosPacientes : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptDocumentosPacientes()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                        string infos,
                        int coEmp,
                        int paciente,
                        int operadora)
        {
            try
            {
                #region Setar o Header e as Labels

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

                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tb07.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP
                           where paciente != 0 ? tb07.CO_ALU == paciente : 0 == 0
                           && ((paciente == 0 && operadora != 0) ? (tb07.TB250_OPERA.ID_OPER == operadora || tb108.TB250_OPERA.ID_OPER == operadora) : 0 == 0)
                           select new DebitoDocumentos
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
                           }).OrderBy(w => w.Aluno).ToList();

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                //Variáveis usadas para Contabilizar o total geral
                int countP = 0;
                int countE = 0;
                int countT = 0;

                bsReport.Clear();

                foreach (DebitoDocumentos dc in res)
                {
                    List<int> Oprs = new List<int>();

                    //Caso a operadora esteja marcada como TODAS ele busca apenas dos documentos entregues 
                    //para não trazer todos os usuários do banco que não possuem documentação
                    if (operadora == 0)
                    {
                        var os = (from tb120 in TB120_DOC_ALUNO_ENT.RetornaTodosRegistros()
                                  where tb120.CO_ALU == dc.coAlu
                                  select tb120.TB250_OPERA.ID_OPER).ToList();

                        Oprs = os;
                    }
                    else
                        Oprs.Add(operadora);

                    //Faz uma listagem de todos os documentos requeridos para a(s) operadora(s) em questão.
                    var resultMat = (from tb402 in TBS402_OPER_DOCTOS.RetornaTodosRegistros()
                                     where Oprs.Contains(tb402.ID_OPER)
                                     select new Documentos
                                     {
                                         cotipdoc = tb402.CO_TP_DOC_MAT,
                                         NomeDocumento = tb402.TB121_TIPO_DOC_MATRICULA.DE_TP_DOC_MAT,
                                         coOperadora = tb402.ID_OPER,
                                         Operadora = tb402.TB250_OPERA.NM_SIGLA_OPER
                                     }).OrderBy(m => m.Operadora).ThenBy(m => m.NomeDocumento).ToList();

                    //Caso não tenha documentação nenhuma não adiciona no relatório
                    if (resultMat != null && resultMat.Count > 0)
                    {
                        //variáveis usadas para fazer a contagem da quantidade de Documentos Entregues e Pendentes
                        int countEnt = 0;
                        int countPen = 0;
                        int countTot = 0;
                        bool re = false;
                        //Verifica para cada documento requerido neste curso, se existe um registro com o mesmo na lista de documentos entregues pelo paciente,
                        //caso exista a situação fica marcada como Entregue, caso contrário como Pendente.
                        foreach (Documentos doc in resultMat)
                        {
                            re = (from tb120 in TB120_DOC_ALUNO_ENT.RetornaTodosRegistros()
                                  where tb120.CO_ALU == dc.coAlu
                                  && tb120.CO_TP_DOC_MAT == doc.cotipdoc
                                  && tb120.TB250_OPERA.ID_OPER == doc.coOperadora
                                  select new { tb120 }).Any();

                            if (re == false)
                            { doc.situacao = "Pendente"; countPen++; countP++; countTot++; countT++; }
                            else
                            { doc.situacao = "Entregue"; countEnt++; countE++; countTot++; countT++; }
                        }

                        //Concatena a string que será apresentada no rodapé de cada paciente tratando se o usuário emitiu somente os pendentes ou entregues ou todos
                        dc.totalAlunoDoc = " Total: " + countTot + " Documento(s). " + countEnt + " Entregue(s) e " + countPen + " Pendente(s) do(a) Paciente " + dc.Aluno + ".";

                        //Atribui o resultado da lista à propriedade documento inclusa na classe DebitoDocumentosAlunos
                        dc.Documentos = resultMat;
                        bsReport.Add(dc);
                    }
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
            public int coOperadora { get; set; }
            public string Operadora { get; set; }
            public string situacao { get; set; }
        }

        public class DebitoDocumentos
        {
            public DebitoDocumentos()
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

            public string Responsavel { get; set; }
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


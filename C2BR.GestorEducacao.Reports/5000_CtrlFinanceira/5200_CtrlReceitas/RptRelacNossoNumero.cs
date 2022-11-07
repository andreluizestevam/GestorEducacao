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


namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5200_CtrlReceitas
{
    public partial class RptRelacNossoNumero : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptRelacNossoNumero()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                        string infos,
                        int coEmp,
                        int unidade,                        
                        int modalidade,
                        int serieCurso,
                        int Turma,
                        int aluno,
                        DateTime DtIniPeri,
                        DateTime DtFimPeri,
                        bool chkPesqNsNu,
                        string nunossNumPesq
                        )
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

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query

                if (chkPesqNsNu == false)
                {
                    var res = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                               join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb47.CO_CUR equals tb01.CO_CUR
                               join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb47.CO_MODU_CUR equals tb44.CO_MODU_CUR
                               join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb47.CO_TUR equals tb129.CO_TUR
                               join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb47.CO_ALU equals tb07.CO_ALU
                               //join tb45 in TB045_NOS_NUM.RetornaTodosRegistros() on tb47.CO_NOS_NUM equals tb45.CO_NOS_NUM into l1
                               //from ls in l1.DefaultIfEmpty()
                               join tb45 in TB045_NOS_NUM.RetornaTodosRegistros() on tb47.NU_DOC equals tb45.NU_DOC into l1
                               from ls in l1.DefaultIfEmpty()
                               where (unidade != 0 ? tb47.CO_EMP == unidade : 0 == 0)
                               && (modalidade != 0 ? tb47.CO_MODU_CUR == modalidade : 0 == 0)
                               && (serieCurso != 0 ? tb47.CO_CUR == serieCurso : 0 == 0)
                               && (Turma != 0 ? tb47.CO_TUR == Turma : 0 == 0)
                               && (aluno != 0 ? tb47.CO_ALU == aluno : 0 == 0)
                               && ((tb47.DT_VEN_DOC >= DtIniPeri) && (tb47.DT_VEN_DOC <= DtFimPeri))
                               select new RelacaoNossosNumeros
                               {
                                   nossNu = ls.CO_NOS_NUM,
                                   NrArquivRet = ls.TB321_ARQ_RET_BOLETO.ID_ARQ_RET_BOLETO,
                                   nrDocumento = tb47.NU_DOC,
                                   nrParDocum = tb47.NU_PAR,
                                   //dtCred = 
                                   vlPgto = tb47.VR_PAG,
                                   dtPagto = tb47.DT_REC_DOC,
                                   dtVenc = tb47.DT_VEN_DOC,
                                   vlDcto = tb47.VR_PAR_DOC,

                                   //Dados do Aluno
                                   Aluno = tb07.NO_ALU,
                                   nuNire = tb07.NU_NIRE,
                                   cpfResp = tb07.TB108_RESPONSAVEL.NU_CPF_RESP,
                                   modalidade = tb44.CO_SIGLA_MODU_CUR,
                                   curso = tb01.CO_SIGL_CUR,
                                   turma = tb129.CO_SIGLA_TURMA,
                               }).ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelacaoNossosNumeros at in res)
                    bsReport.Add(at);

                return 1;

                }
                else
                {
                    var res = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                               join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb47.CO_CUR equals tb01.CO_CUR
                               join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb47.CO_MODU_CUR equals tb44.CO_MODU_CUR
                               join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb47.CO_TUR equals tb129.CO_TUR
                               join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb47.CO_ALU equals tb07.CO_ALU
                               //join tb45 in TB045_NOS_NUM.RetornaTodosRegistros() on tb47.CO_NOS_NUM equals tb45.CO_NOS_NUM into l1
                               //from ls in l1.DefaultIfEmpty()
                               join tb45 in TB045_NOS_NUM.RetornaTodosRegistros() on tb47.NU_DOC equals tb45.NU_DOC into l1
                               from ls in l1.DefaultIfEmpty()
                               where (ls.CO_NOS_NUM.Contains(nunossNumPesq))
                               && ((tb47.DT_VEN_DOC >= DtIniPeri) && (tb47.DT_VEN_DOC <= DtFimPeri))
                               select new RelacaoNossosNumeros
                               {
                                   nossNu = ls.CO_NOS_NUM,
                                   NrArquivRet = ls.TB321_ARQ_RET_BOLETO.ID_ARQ_RET_BOLETO,
                                   nrDocumento = tb47.NU_DOC,
                                   nrParDocum = tb47.NU_PAR,
                                   //dtCred = 
                                   vlPgto = tb47.VR_PAG,
                                   dtPagto = tb47.DT_REC_DOC,
                                   dtVenc = tb47.DT_VEN_DOC,
                                   vlDcto = tb47.VR_PAR_DOC,

                                   //Dados do Aluno
                                   Aluno = tb07.NO_ALU,
                                   nuNire = tb07.NU_NIRE,
                                   cpfResp = tb07.TB108_RESPONSAVEL.NU_CPF_RESP,
                                   modalidade = tb44.CO_SIGLA_MODU_CUR,
                                   curso = tb01.CO_SIGL_CUR,
                                   turma = tb129.CO_SIGLA_TURMA,
                               }).ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelacaoNossosNumeros at in res)
                    bsReport.Add(at);

                return 1;

                }
            }
            catch { return 0; }
        }

        public class RelacaoNossosNumeros
        {
            //Dados do Documento
            public string nossNumero
            {
                get
                {
                    return (this.nossNu != "" ? this.nossNu : "--");
                }
            }
            public string nossNu { get; set; }
            public int? NrArquivRet { get; set; }
            public string nrDocumento { get; set; }
            public int nrParDocum { get; set; }
            public DateTime dtCred { get; set; }
            public string dtCredValid 
            {
                get
                {
                    return this.dtCred.ToString("dd/MM/yy");
                }
            }
            public decimal? vlPgto { get; set; }
            public string vlPgtoValid
            {
                get
                {
                    return (this.vlPgto.HasValue ? this.vlPgto.Value.ToString() : " - ");
                }
            }
            public DateTime? dtPagto { get; set; }
            public string dtPagtoValid
            {
                get
                {
                    return ( this.dtPagto.HasValue ? dtPagto.Value.ToString("dd/MM/yy") : " - " );
                }
            }
            public DateTime dtVenc { get; set; }
            public string dtVencValid
            {
                get
                {
                    return this.dtVenc.ToString("dd/MM/yy");
                }
            }
            public decimal? vlDcto { get; set; }
            public string vlDctoValid
            {
                get
                {
                    return ( this.vlDcto.HasValue ? this.vlDcto.Value.ToString() : " - ");
                }
            }

            //Dados do Aluno
            public string Aluno { get; set; }
            public int nuNire { get; set; }
            public string AlunoValid
            {
                get
                {
                    return ("   " + this.nuNire.ToString().PadLeft(7, '0') + " - " + (this.Aluno.Length > 27 ? this.Aluno.Substring(0, 27) + "..." : this.Aluno));
                }
            }
            public string modalidade { get; set; }
            public string curso { get; set; }
            public string turma { get; set; }
            public string ano { get; set; }
            public string cpfResp { get; set; }
            public string cpfRespValid 
            {
                get
                {
                    if ((this.cpfResp == null) || (this.cpfResp == ""))
                    {
                        return "---";
                    }
                    return this.cpfResp.Insert(3, ".").Insert(7, ".").Insert(11, "-");
                } 
            }
        }
    }
}


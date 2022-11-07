using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;
using System.Data.Objects;

namespace C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar
{
    public partial class RptRelatorioDeChequesDeMatricula : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptRelatorioDeChequesDeMatricula()
        {
            InitializeComponent();
        }


        #region Init Report

        public int InitReport(
                           string parametros,
                           string infos,
                           int coEmp,
                           int coEmpLog,
                           int coCol,
                           int coMod,
                           int coCur,
                           int coTur,
                           string coAno,
                           DateTime dtIni,
                           DateTime dtFim)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros.ToUpper();

                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmpLog);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
                string dti = dtIni.ToString();
                string dtf = dtFim.ToString();

                //DateTime dataIni1;
                //if (!DateTime.TryParse(dti, out dataIni1))
                //{
                //    return 0;
                //}

                //DateTime dataFim1;
                //if (!DateTime.TryParse(dtf, out dataFim1))
                //{
                //    return 0;
                //}

                DateTime dataIni1;
                if (!DateTime.TryParse(dti, out dataIni1))
                {
                    return 0;
                }

                DateTime dataFim1;
                if (!DateTime.TryParse(dtf, out dataFim1))
                {
                    return 0;
                }

                #region Query Colaborador Parametrizada

                var lst = from tb220 in TBE220_MATRCUR_PAGTO.RetornaTodosRegistros()
                          join tbe222 in TBE222_PAGTO_CHEQUE.RetornaTodosRegistros() on tb220.ID_MATRCUR_PAGTO equals tbe222.TBE220_MATRCUR_PAGTO.ID_MATRCUR_PAGTO
                          join m in TB08_MATRCUR.RetornaTodosRegistros() on tb220.CO_ALU_CAD equals m.CO_ALU_CAD
                          join mo in TB44_MODULO.RetornaTodosRegistros() on m.TB44_MODULO.CO_MODU_CUR equals mo.CO_MODU_CUR
                          join c in TB01_CURSO.RetornaTodosRegistros() on m.CO_CUR equals c.CO_CUR
                          join t in TB129_CADTURMAS.RetornaTodosRegistros() on m.CO_TUR equals t.CO_TUR
                          join co in TB03_COLABOR.RetornaTodosRegistros() on m.CO_COL equals co.CO_COL
                          join a in TB07_ALUNO.RetornaTodosRegistros() on m.CO_ALU equals a.CO_ALU

                          where (coEmp != 0 ? tb220.CO_EMP == coEmp : 0 == 0)
                          && (coMod != 0 ? mo.CO_MODU_CUR == coMod : 0 == 0)
                          && (coCur != 0 ? c.CO_CUR == coCur : 0 == 0)
                          && (coTur != 0 ? t.CO_TUR == coTur : 0 == 0)
                          && (coCol != 0 ? co.CO_COL == coCol : 0 == 0)
                          && ((m.DT_EFE_MAT >= dataIni1) && (m.DT_EFE_MAT <= dataFim1))

                          select new Cheques
                          {
                              data = tb220.DT_CAD,
                              NomeAluno = a.NO_ALU,
                              nuNire = a.NU_NIRE,

                              SglModal = mo.CO_SIGLA_MODU_CUR,
                              SglCur = c.CO_SIGL_CUR,
                              SglTur = t.CO_SIGLA_TURMA,

                              ApelColab = co.NO_APEL_COL,
                              nrContrato = m.NR_CONTR_MATRI,

                              TitularCPF = tbe222.NR_CPF,
                              
                              NumeroBanco = tbe222.CO_BCO,
                              Conta = tbe222.NR_CONTA,
                              NumeroAgencia = tbe222.NR_AGENCI,
                              NumeroCheque = tbe222.NR_CHEQUE,
                              DataVencimento = tbe222.DT_VENC,
                              NometitularCheque = tbe222.NO_TITUL,
                              Valor = tbe222.VL_PAGTO,
                          };



                var res = lst.ToList();

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (Cheques at in lst)
                    bsReport.Add(at);

                return 1;

                #endregion ;

            }
            catch { return 0; }
        }

        #endregion

        public class Cheques
        {
            public DateTime data { get; set; }
            public string dataValid
            {
                get
                {
                   return this.data.ToString("dd/MM/yy");
                }
            }

            public string NomeAluno { get; set; }
            public int nuNire { get; set; }
            public string aluno
            {
                get
                {
                    string nire = this.nuNire.ToString().PadLeft(7, '0');
                    string nome = this.NomeAluno.Length > 25 ? this.NomeAluno.Substring(0, 25).ToUpper() + "..." : this.NomeAluno.ToUpper();

                    return nire + " - " + nome;
                }
            }

            public string SglModal { get; set; }
            public string SglCur { get; set; }
            public string SglTur { get; set; }

            public string ApelColab { get; set; }

            public string nrContrato { get; set; }
            public int? NumeroBanco { get; set; }
            public string NumeroBancoValid
            {
                get
                {
                    return (this.NumeroBanco.HasValue ? this.NumeroBanco.ToString().PadLeft(3, '0') : "");
                }
            }
            public string NumeroAgencia { get; set; }
            public string Conta { get; set; }
            public string NumeroCheque { get; set; }
            public DateTime? DataVencimento { get; set; }
            public string dtVencValid
            {
                get
                {
                    return (this.DataVencimento.HasValue ? this.DataVencimento.Value.ToString("dd/MM/yy") : "");
                }
            }

            public string NometitularCheque { get; set; }
            public string TitularCPF { get; set; }
            public string TitularValid
            {
                get
                {
                    string cpf = this.TitularCPF.ToString().Insert(3, ".").Insert(7, ".").Insert(11, "-");
                    string nome = this.NometitularCheque.Length > 25 ? this.NometitularCheque.Substring(0, 25).ToUpper() + "..." : this.NometitularCheque.ToUpper();

                    return cpf + " - " + nome;
                }
            }

            public decimal? Valor { get; set; }
            //public string Cpf { get; set; }
            //public String CPFForm
            //{
            //    get
            //    {
            //        if (this.Cpf.Length == 11)
            //        {
            //            String ret = this.Cpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");
            //            return ret; 
            //        }
            //        else
            //        {
            //            return this.Cpf;
            //        }
                   
            //    }

            //}




        }
    }
}

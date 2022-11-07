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
    public partial class RptMoviFinanMatric : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptMoviFinanMatric()
        {
            InitializeComponent();
        }

        public int InitReport(
            string parametros,
            string infos,
            int coEmp,
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
                #region Inicializa o header/Labels

                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                lblParametros.Visible = false;

                lblParametro2.Text = parametros;
                lblParametro2.Visible = true;


                // Cria o header a partir do cod da instituicao
                var header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return -1;

                // Inicializa o header
                base.BaseInit(header);
                //this.celFreq.Text = string.Format(celFreq.Text, anoBase);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
                
                var res = (from m in TB08_MATRCUR.RetornaTodosRegistros()
                           join mo in TB44_MODULO.RetornaTodosRegistros() on m.TB44_MODULO.CO_MODU_CUR equals mo.CO_MODU_CUR
                           join c in TB01_CURSO.RetornaTodosRegistros() on m.CO_CUR equals c.CO_CUR
                           join t in TB129_CADTURMAS.RetornaTodosRegistros() on m.CO_TUR equals t.CO_TUR
                           join co in TB03_COLABOR.RetornaTodosRegistros() on m.CO_COL equals co.CO_COL
                           join a in TB07_ALUNO.RetornaTodosRegistros() on m.CO_ALU equals a.CO_ALU
                           join fp in TBE220_MATRCUR_PAGTO.RetornaTodosRegistros() on m.CO_ALU_CAD equals fp.CO_ALU_CAD into fp1
                           from fp2 in fp1.DefaultIfEmpty()
                           where (coEmp != 0 ? m.CO_EMP == coEmp : 0 == 0)
                           && (coMod != 0 ? m.TB44_MODULO.CO_MODU_CUR == coMod : 0 == 0)
                           && (coCur != 0 ? m.CO_CUR == coCur : 0 == 0)
                           && (coTur != 0 ? m.CO_TUR == coTur : 0 == 0)
                           && (coCol != 0 ? m.CO_COL == coCol : 0 == 0)
                           && (coAno != "0" ? m.CO_ANO_MES_MAT == coAno : 0 == 0)
                           select new MoviFinanMatric
                           {
                               idFp = fp2.ID_MATRCUR_PAGTO,
                               CM = (fp2 != null ? "" : "*"),
                               coCol = co.CO_COL,
                               noCol = co.NO_COL,
                               cpfCol = co.NU_CPF_COL,
                               funCol = co.DE_FUNC_COL,
                               matrCol = co.CO_MAT_COL,
                               dtMatrcur = m.DT_EFE_MAT,
                               noAlu = a.NO_ALU,
                               nuNire = a.NU_NIRE,
                               noCur = c.CO_SIGL_CUR,
                               noTur = t.CO_SIGLA_TURMA,
                               vlContr = m.VL_TOT_MODU_MAT,
                               vlDinhe = fp2.VL_DINHE,
                               vlOutro = fp2.VL_OUTRO,
                               nuContr = m.NR_CONTR_MATRI,
                           }).OrderBy(o => o.dtMatrcur).ThenBy(o => o.noAlu).Distinct();

                decimal? vlCheque = 0;
                decimal? vlCartao = 0;
                bsReport.Clear();
                foreach (MoviFinanMatric r in res)
                {
                    //Workaround para filtrar as datas, pois o linq não aceita métodos não nativos do LINQ, e não havia outra forma de quebrar o tempo do Datetime
                    bool mostra = true;
                    if (!((FormataData(r.dtMatrcur) >= dtIni) && (FormataData(r.dtMatrcur) <= dtFim)))
                        mostra = false;

                    if (mostra == true)
                    {
                        //Calcula os valores cadastrados de Cheques relacionados à forma de pagamento contexto
                        if (TBE222_PAGTO_CHEQUE.RetornaTodosRegistros().Where(w => w.TBE220_MATRCUR_PAGTO.ID_MATRCUR_PAGTO == r.idFp).Any())
                        {
                            vlCheque = (from c in TBE222_PAGTO_CHEQUE.RetornaTodosRegistros()
                                        where c.TBE220_MATRCUR_PAGTO.ID_MATRCUR_PAGTO == r.idFp
                                        select c).Sum(s => s.VL_PAGTO.Value);
                        }
                        else
                        {
                            vlCheque = 0;
                        }

                        //Calcula os valores cadastrados de cartões relacionados à forma de pagamento contexto
                        if (TBE221_PAGTO_CARTAO.RetornaTodosRegistros().Where(w => w.TBE220_MATRCUR_PAGTO.ID_MATRCUR_PAGTO == r.idFp).Any())
                        {
                            vlCartao = (from c in TBE221_PAGTO_CARTAO.RetornaTodosRegistros()
                                         where c.TBE220_MATRCUR_PAGTO.ID_MATRCUR_PAGTO == r.idFp
                                         select c).Sum(s => s.VL_PAGTO.Value);
                        }
                        else
                        {
                            vlCartao = 0;
                        }

                        var tb220 = (from tb220li in TBE220_MATRCUR_PAGTO.RetornaTodosRegistros()
                                     where tb220li.ID_MATRCUR_PAGTO == r.idFp
                                     select tb220li).FirstOrDefault();

                        //Soma os totais de transações com bancos relacionadas à forma de pagamento contexto
                        decimal totBancos = 0;
                        if (tb220 != null)
                        {
                            if (tb220.VL_DEPOS.HasValue)
                                totBancos = tb220.VL_DEPOS.Value;
                            if (tb220.VL_DEBIT_CONTA.HasValue)
                                totBancos += tb220.VL_DEBIT_CONTA.Value;
                            if (tb220.VL_TRANS.HasValue)
                                totBancos += tb220.VL_TRANS.Value;
                        }

                        r.vlChequ = vlCheque != null ? vlCheque.Value : 0;
                        r.vlCartao = vlCartao != null ? vlCartao.Value : 0;
                        r.vlBanco = totBancos;

                        bsReport.Add(r);
                    }
                }


                return 1;
            }
            catch { return 0; }
        }

        private DateTime FormataData(DateTime dt)
        {
            string dtFormat = dt.ToString().Substring(0, 10);
            return DateTime.Parse(dtFormat);
        }

        public class MoviFinanMatric
        {
            public int? idFp { get; set; }

            public DateTime dtMatrcur { get; set; }
            public string data
            {
                get
                {
                    string hora = this.dtMatrcur.ToString("HH:mm");
                    return this.dtMatrcur.ToString("dd/MM/yy") + " - " + hora;
                }
            }
            public int nuNire { get; set; }
            public string nuContr { get; set; }
            public int coCol { get; set; }
            public string matrCol { get; set; }
            public string cpfCol { get; set; }
            public string funCol { get; set; }
            public string noCol { get; set; }

            public string CM { get; set; }




            public string colaborador
            {
                get
                {
                    string fun = this.funCol != null ? this.funCol : "******";

                    String r = String.Format("Colaborador: {0} - {1}  ", this.matrCol, this.noCol.ToUpper());

                    r += " ( CPF: " + cpfCol.Insert(3, ".").Insert(7, ".").Insert(11, "-") + " - Função: " + fun.ToUpper() + " )";
                    return r;
                }
            }

            public string noAlu { get; set; }
            public string aluno
            {
                get
                {
                    string nire = this.nuNire.ToString().PadLeft(7, '0');
                    string nuContr = this.nuContr;
                    string nome = this.noAlu.Length > 25 ? this.noAlu.Substring(0, 25).ToUpper() + "..." : this.noAlu.ToUpper();

                    return nuContr + " - " + nome;
                }
            }
            public string noCur { get; set; }
            public string noTur { get; set; }

            public decimal? vlContr { get; set; }
            public string vlContrS
            {
                get
                {
                    decimal vl = this.vlContr != null ? this.vlContr.Value : 0;

                    return vl.ToString("N2");
                }
            }

            public decimal? vlDinhe { get; set; }
            public string vlDinheS
            {
                get
                {
                    decimal vl = this.vlDinhe != null ? this.vlDinhe.Value : 0;

                    return vl.ToString("N2");
                }
            }

            public decimal? vlBanco { get; set; }
            public string vlBancoS
            {
                get
                {
                    decimal vl = this.vlBanco != null ? this.vlBanco.Value : 0;

                    return vl.ToString("N2");
                }
            }

            public decimal? vlChequ { get; set; }
            public string vlChequS
            {
                get
                {
                    decimal vl = this.vlChequ != null ? this.vlChequ.Value : 0;

                    return vl.ToString("N2");
                }
            }

            public decimal? vlCartao { get; set; }
            public string vlCartaoS
            {
                get
                {
                    decimal vl = this.vlCartao != null ? this.vlCartao.Value : 0;

                    return vl.ToString("N2");
                }
            }

            public decimal? vlOutro { get; set; }
            public string vlOutroS
            {
                get
                {
                    decimal vl = this.vlOutro != null ? this.vlOutro.Value : 0;

                    return vl.ToString("N2");
                }
            }

            public decimal vlTotal
            {
                get
                {
                    decimal vlD = this.vlDinhe != null ? this.vlDinhe.Value : 0;
                    decimal vlC = this.vlChequ != null ? this.vlChequ.Value : 0;
                    decimal vlCr = this.vlCartao != null ? this.vlCartao.Value : 0;
                    decimal vlO = this.vlOutro != null ? this.vlOutro.Value : 0;

                    return vlD + vlC + vlCr + vlO;
                }
            }
            public string vlTotalS
            {
                get
                {
                    decimal vl = this.vlTotal != null ? this.vlTotal : 0;

                    return vl.ToString("N2");
                }
            }

            public decimal vlDifer
            {
                get
                {
                    decimal vlC = this.vlContr != null ? this.vlContr.Value : 0;
                    decimal vlT = this.vlTotal != null ? this.vlTotal : 0;

                    return vlT - vlC;
                }
            }
            public string vlDiferS
            {
                get
                {
                    decimal vl = this.vlDifer;

                    return vl.ToString("N2");
                }
            }
        }

    }
}

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
using System.Data;
using System.Web;
using System.Resources;
using DevExpress.XtraCharts;

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3300_Consultas
{
    public partial class RptMovFinacConsultas : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptMovFinacConsultas()
        {
            InitializeComponent();
        }


        #region Init Report

        public int InitReport(string parametros, string infos, int coEmp,int UnidadeConsulta, int UnidadeDeCadastro, int UnidadeDeContrato, int Especialidade, string ClassificacaoProfissional, int ProfissionalSaude, string DataInicial, string DataFinal, string NomeFuncionalidade)
        {


            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;
                DateTime dtIni = Convert.ToDateTime(DataInicial);
                DateTime dtFim = Convert.ToDateTime(DataFinal);


                if (NomeFuncionalidade == "")
                {
                    lblTitulo.Text = "MOVIMENTO FINANCEIRO DE CONSULTAS *";
                }
                else
                {
                    lblTitulo.Text = NomeFuncionalidade;
                }


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

                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                           join tbs25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tbs25.CO_EMP
                           join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tbs174.CO_ESPEC equals tb63.CO_ESPECIALIDADE into les
                           from lespecag in les.DefaultIfEmpty()
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                           join fp in TBS363_CONSUL_PAGTO.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals fp.TBS174_AGEND_HORAR.ID_AGEND_HORAR into fp1
                           from fp2 in fp1.DefaultIfEmpty()
                           where (coEmp != 0 ? tbs25.CO_EMP == coEmp : 0 == 0)
                           && (UnidadeDeCadastro != 0 ? tb03.CO_EMP == UnidadeDeCadastro : 0 == 0)
                           && (UnidadeDeContrato != 0 ? tb03.CO_EMP_UNID_CONT == UnidadeDeContrato : 0 == 0)
                           && (UnidadeConsulta != 0 ? tbs174.CO_EMP == UnidadeConsulta : 0 == 0)
                           && (Especialidade != 0 ? lespecag.CO_ESPEC == Especialidade : 0 == 0)
                           && (ClassificacaoProfissional != "0" ? tb03.CO_CLASS_PROFI == ClassificacaoProfissional : 0 == 0)
                           && (ProfissionalSaude != 0 ? tb03.CO_ESPEC == ProfissionalSaude : 0 == 0)
                           select new MoviFinanSaude
                           {
                               Idconsulta = tbs174.ID_AGEND_HORAR,
                               idFp = fp2.ID_CONSUL_PAGTO,
                               CM = (fp2 != null ? "" : "*"),
                               coCol = tb03.CO_COL,
                               noCol = tb03.NO_COL,
                               cpfCol = tb03.NU_CPF_COL,
                               funCol = tb03.DE_FUNC_COL,
                               matrCol = tb03.CO_MAT_COL,
                               DataHora = tbs174.DT_AGEND_HORAR,
                               Hora = tbs174.HR_AGEND_HORAR,
                               noPaciente = tb07.NO_ALU,
                               nuNire = tbs174.NU_REGIS_CONSUL,
                               noEspec = (lespecag != null ? lespecag.NO_SIGLA_ESPECIALIDADE : " - "),
                               noUnidade = tbs25.sigla,
                               vlContr = tbs174.VL_CONSUL,
                               vlDinhe = fp2.VL_DINHE,
                               vlOutro = fp2.VL_OUTRO,
                               nuContr = tbs174.NU_REGIS_CONSUL,

                           }).DistinctBy(m => m.nuContr).OrderBy(m => m.DataHora).ToList();
                decimal? vlCheque = 0;
                decimal? vlCartao = 0;

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                bsReport.Clear();
                foreach (MoviFinanSaude r in res)
                {
                    //Workaround para filtrar as datas, pois o linq não aceita métodos não nativos do LINQ, e não havia outra forma de quebrar o tempo do Datetime
                    bool mostra = true;

                    if (!((FormataData(r.DataHora) >= dtIni) && (FormataData(r.DataHora) <= dtFim)))
                        mostra = false;

                    if (mostra == true)
                    {
                        //Calcula os valores cadastrados de Cheques relacionados à forma de pagamento contexto
                        if (TBS365_PAGTO_CHEQUE.RetornaTodosRegistros().Where(w => w.TBS363_CONSUL_PAGTO.CO_COL_CADAS == r.idFp).Any())
                        {
                            vlCheque = (from c in TBS365_PAGTO_CHEQUE.RetornaTodosRegistros()
                                        where c.TBS363_CONSUL_PAGTO.CO_COL_CADAS == r.idFp
                                        select c).Sum(s => s.VL_PAGTO.Value);
                        }
                        else
                        {
                            vlCheque = 0;
                        }

                        //Calcula os valores cadastrados de cartões relacionados à forma de pagamento contexto
                        if (TBS364_PAGTO_CARTAO.RetornaTodosRegistros().Where(w => w.TBS363_CONSUL_PAGTO.ID_CONSUL_PAGTO == r.idFp).Any())
                        {
                            vlCartao = (from c in TBS364_PAGTO_CARTAO.RetornaTodosRegistros()
                                        where c.TBS363_CONSUL_PAGTO.ID_CONSUL_PAGTO == r.idFp
                                        select c).Sum(s => s.VL_PAGTO.Value);
                        }
                        else
                        {
                            vlCartao = 0;
                        }

                        var tbs363 = (from tbs363li in TBS363_CONSUL_PAGTO.RetornaTodosRegistros()
                                     where tbs363li.ID_CONSUL_PAGTO == r.idFp
                                     select tbs363li).FirstOrDefault();

                        //Soma os totais de transações com bancos relacionadas à forma de pagamento contexto
                        decimal totBancos = 0;
                        if (tbs363 != null)
                        {
                            if (tbs363.VL_DEPOS.HasValue)
                                totBancos = tbs363.VL_DEPOS.Value;
                            //if (tbs363.VL_DEBIT_CONTA.HasValue)
                            //    totBancos += tbs363.VL_DEBIT_CONTA.Value;
                            if (tbs363.VL_TRANS.HasValue)
                                totBancos += tbs363.VL_TRANS.Value;
                        }

                        r.vlChequ = vlCheque != null ? vlCheque.Value : 0;
                        r.vlCartao = vlCartao != null ? vlCartao.Value : 0;
                        r.vlBanco = totBancos;

                        bsReport.Add(r);
                    }
                }
                #endregion

        #endregion

                return 1;
            }
            catch { return 0; }
        }
        private DateTime FormataData(DateTime dt)
        {

            string dtFormat = dt.ToString().Substring(0, 10);
            return DateTime.Parse(dtFormat);
        }
        public class MoviFinanSaude
        {
            public int? idFp { get; set; }
            public int Idconsulta { get; set; }

            public DateTime DataHora { get; set; }
            public string data
            {
                get
                {

                    //string  = DataHora.ToString("HH:mm");
                    return DataHora.ToString("dd/MM/yy") + " - " + Hora;
                }
            }
            public string Hora { get; set; }
            public string nuNire { get; set; }
            public string nuContr { get; set; }
            public int coCol { get; set; }
            public string matrCol { get; set; }
            public string cpfCol { get; set; }
            public string funCol { get; set; }
            public string noCol { get; set; }

            public string CM { get; set; }
            public string Profissionalsaude
            {
                get
                {
                    string fun = this.funCol != null ? this.funCol : "******";

                    String r = String.Format("Profissional Saúde: {0} - {1}  ", this.matrCol, this.noCol.ToUpper());

                    r += " ( CPF: " + cpfCol.Insert(3, ".").Insert(7, ".").Insert(11, "-") + " - Função: " + fun.ToUpper() + " )";
                    return r;
                }
            }

            public string noPaciente { get; set; }
            public string paciente
            {
                get
                {
                    string nire = this.nuNire.ToString().PadLeft(7, '0');
                    string nuContr = this.nuContr;
                    string nome = this.noPaciente.Length > 25 ? this.noPaciente.Substring(0, 25).ToUpper() + "..." : this.noPaciente.ToUpper();

                    return nuContr + " - " + nome;
                }
            }
            public string noEspec { get; set; }
            public string noUnidade { get; set; }

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

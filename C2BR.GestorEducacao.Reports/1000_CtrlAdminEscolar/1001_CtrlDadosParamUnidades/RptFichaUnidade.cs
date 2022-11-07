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

namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1001_CtrlDadosParamUnidades
{
    public partial class RptFichaUnidade : C2BR.GestorEducacao.Reports.RptRetrato
    {
        #region ctor

        public RptFichaUnidade()
        {
            InitializeComponent();
        }

        #endregion

        #region Init Report

        public int InitReport(int codEmp, int codUnd, string infos)
        {
            try
            {
                #region Anos

                int anoBase = DateTime.Now.Year;
                List<int> lstAnos = new List<int>() { anoBase, anoBase - 1, anoBase - 2, anoBase - 3 };
                List<string> lstAnosStr = new List<string>() { anoBase.ToString(), (anoBase - 1).ToString(),
                    (anoBase - 2).ToString(), (anoBase - 3).ToString() };

                celAlAno1.Text = lstAnos[0].ToString();
                celAlAno2.Text = lstAnos[1].ToString();
                celAlAno3.Text = lstAnos[2].ToString();
                celAlAno4.Text = lstAnos[3].ToString();

                celAno1.Text = lstAnos[0].ToString();
                celAno2.Text = lstAnos[1].ToString();
                celAno3.Text = lstAnos[2].ToString();
                celAno4.Text = lstAnos[3].ToString();

                celAno.Text = string.Format(celAno.Tag as String, anoBase);

                #endregion

                #region Inicializa o header/Labels

                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;

                // Cria o header a partir do cod da instituicao
                var header = ReportHeader.GetHeaderFromEmpresa(codEmp);
                if (header == null)
                    return -1;

                // Inicializa o header
                base.BaseInit(header);
                //this.celFreq.Text = string.Format(celFreq.Text, anoBase);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Unidade

                var und = (from e in ctx.TB25_EMPRESA
                           join cid in ctx.TB904_CIDADE on e.CO_CIDADE equals cid.CO_CIDADE into ci
                           from cid in ci.DefaultIfEmpty()
                           join bai in ctx.TB905_BAIRRO on e.CO_BAIRRO equals bai.CO_BAIRRO into ba
                           from bai in ba.DefaultIfEmpty()
                           join nuc in ctx.TB_NUCLEO_INST on e.CO_NUCLEO equals nuc.CO_NUCLEO into nu
                           from nuc in nu.DefaultIfEmpty()
                           where e.CO_EMP == codUnd
                           select new Unidade
                           {
                               Nome = e.NO_FANTAS_EMP,
                               RazaoSocial = e.NO_RAZSOC_EMP,
                               Sigla = e.CO_SIT_EMP,
                               Logomarca = e.Image.ImageStream,
                               Inep = e.NU_INEP,
                               Bairro = bai.NO_BAIRRO,
                               Classificacao = e.TB162_CLAS_INST.NO_CLAS,
                               Cidade = cid.NO_CIDADE,
                               Categoria = e.TB24_TPEMPRESA.NO_TIPOEMP,
                               CnpjNum = e.CO_CPFCGC_EMP,
                               Email = e.NO_EMAIL_EMP,
                               CEP = e.CO_CEP_EMP,
                               Endereco = e.DE_END_EMP,
                               Fax = e.CO_TEL2_EMP,
                               Telefone = e.CO_TEL1_EMP,
                               Site = e.NO_WEB_EMP,
                               Nucleo = nuc.DE_NUCLEO,
                               NumConstituicao = e.CO_DOCTO_CONST,
                               DtConstituicao = e.DT_CONST,
                               Funcionamento = e.TP_HORA_FUNC
                           });

                //var s = (lstf as ObjectQuery).ToTraceString();

                #endregion

                var emp = und.FirstOrDefault();

                // Se não encontrou a Unidade
                if (emp == null)
                    return -1;

                #region Gestores

                var lstG = (from g in ctx.TB59_GESTOR_UNIDAD
                            where g.TB25_EMPRESA.CO_EMP == codUnd
                            select new Gestor
                            {
                                DtInicio = g.DT_INICIO_ATIVID,
                                Funcao = g.TB15_FUNCAO.NO_FUN,
                                Matricula = g.TB03_COLABOR.CO_MAT_COL,
                                Nome = g.TB03_COLABOR.NO_COL,
                                Sexo = g.TB03_COLABOR.CO_SEXO_COL,
                                Telefone = g.TB03_COLABOR.NU_TELE_RESI_COL,
                                DtNasc = g.TB03_COLABOR.DT_NASC_COL
                            }).ToList();

                emp.Gestores = lstG;

                #endregion

                #region Qtd Alunos

                var lstQA = (from m in ctx.TB08_MATRCUR
                             join cur in ctx.TB01_CURSO on m.CO_CUR equals cur.CO_CUR into cr
                             from cur in cr.DefaultIfEmpty()
                             where m.TB25_EMPRESA.CO_EMP == codUnd
                             && lstAnosStr.Contains(m.CO_ANO_MES_MAT.Trim())
                             group new { m, cur }
                             by new
                                {
                                    Ano = m.CO_ANO_MES_MAT.Trim(),
                                    Curso = (cur.NO_CUR + " - " + cur.TB44_MODULO.DE_MODU_CUR)
                                } into grp
                             select new QtdAlunos
                             {
                                 Serie = grp.Key.Curso,
                                 Ano = grp.Key.Ano,
                                 Total = grp.Select(x => x.m).Count()
                             }).ToList();

                foreach (var q in lstQA)
                {
                    string ano1 = lstAnosStr[0];
                    string ano2 = lstAnosStr[1];
                    string ano3 = lstAnosStr[2];
                    string ano4 = lstAnosStr[3];

                    q.TotalAno1 = lstQA.Where(x => x.Serie == q.Serie && x.Ano == ano1).DefaultIfEmpty(new QtdAlunos()).First().Total;
                    q.TotalAno2 = lstQA.Where(x => x.Serie == q.Serie && x.Ano == ano2).DefaultIfEmpty(new QtdAlunos()).First().Total;
                    q.TotalAno3 = lstQA.Where(x => x.Serie == q.Serie && x.Ano == ano3).DefaultIfEmpty(new QtdAlunos()).First().Total;
                    q.TotalAno4 = lstQA.Where(x => x.Serie == q.Serie && x.Ano == ano4).DefaultIfEmpty(new QtdAlunos()).First().Total;
                }

                emp.QtdAlunos = lstQA.DistinctBy(x => x.Serie).ToList();

                #endregion

                #region Qtd Colaboradores

                var lstQC = (from c in ctx.TB03_COLABOR
                             from a in lstAnos
                             where c.TB25_EMPRESA.CO_EMP == codUnd
                             group c by new { c.FLA_PROFESSOR, Ano = a } into grp
                             select new QtdColaboradores
                             {
                                 TipoColab = grp.Key.FLA_PROFESSOR == "S" ? "Professor" : "Funcionário",
                                 Ano = grp.Key.Ano,
                                 Total = grp.Where(x => x.DT_INIC_ATIV_COL.Year >= lstAnos.Min()
                                     && x.DT_INIC_ATIV_COL.Year <= grp.Key.Ano
                                     && (!x.DT_TERM_ATIV_COL.HasValue || x.DT_TERM_ATIV_COL.Value.Year >= grp.Key.Ano))
                                     .Count()
                             }).ToList();

                foreach (var q in lstQC)
                {
                    int ano1 = lstAnos[0];
                    int ano2 = lstAnos[1];
                    int ano3 = lstAnos[2];
                    int ano4 = lstAnos[3];

                    q.Ano1 = lstQC.Where(x => x.TipoColab == q.TipoColab && x.Ano == ano1)
                        .DefaultIfEmpty(new QtdColaboradores()).First().Total;
                    q.Ano2 = lstQC.Where(x => x.TipoColab == q.TipoColab && x.Ano == ano2)
                        .DefaultIfEmpty(new QtdColaboradores()).First().Total;
                    q.Ano3 = lstQC.Where(x => x.TipoColab == q.TipoColab && x.Ano == ano3)
                        .DefaultIfEmpty(new QtdColaboradores()).First().Total;
                    q.Ano4 = lstQC.Where(x => x.TipoColab == q.TipoColab && x.Ano == ano4)
                        .DefaultIfEmpty(new QtdColaboradores()).First().Total;
                }

                emp.QtdColaboradores = lstQC.DistinctBy(x => x.TipoColab).ToList();

                #endregion

                #region Dist. Funcoes/Situacao

                var lstDF = (from c in ctx.TB03_COLABOR
                             join f in ctx.TB15_FUNCAO on c.CO_FUN equals f.CO_FUN into fj
                             from f in fj.DefaultIfEmpty()
                             where c.TB25_EMPRESA.CO_EMP == codUnd
                             group new { c, f } by f.NO_FUN into grp
                             select new Distribuicao
                             {
                                 Funcao = grp.Key,
                                 ATE = grp.Where(x => x.c.CO_SITU_COL == "ATE").Count(),
                                 ATI = grp.Where(x => x.c.CO_SITU_COL == "ATI").Count(),
                                 FCE = grp.Where(x => x.c.CO_SITU_COL == "FCE").Count(),
                                 FER = grp.Where(x => x.c.CO_SITU_COL == "FER").Count(),
                                 FES = grp.Where(x => x.c.CO_SITU_COL == "FES").Count(),
                                 LFR = grp.Where(x => x.c.CO_SITU_COL == "LFR").Count(),
                                 LMA = grp.Where(x => x.c.CO_SITU_COL == "LMA").Count(),
                                 LME = grp.Where(x => x.c.CO_SITU_COL == "LME").Count(),
                                 SUS = grp.Where(x => x.c.CO_SITU_COL == "SUS").Count(),
                                 TRE = grp.Where(x => x.c.CO_SITU_COL == "TRE").Count(),
                                 Prof = grp.Where(x => x.c.FLA_PROFESSOR == "S").Count(),
                                 Total = grp.Count()
                             }).ToList();

                emp.Distribuicoes = lstDF;

                #endregion

                // Adiciona a Unidade ao DataSource do Relatório
                bsReport.Clear();
                bsReport.Add(emp);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Unidade do Relatorio

        public class Unidade
        {
            public Unidade()
            {
                this.Gestores = new List<Gestor>();
                this.QtdAlunos = new List<QtdAlunos>();
                this.QtdColaboradores = new List<QtdColaboradores>();
                this.Distribuicoes = new List<Distribuicao>();
            }

            public byte[] Logomarca { get; set; }
            public string Nome { get; set; }
            public string Sigla { get; set; }
            public int? Inep { get; set; }
            public string Tipo { get; set; }
            public string RazaoSocial { get; set; }
            public string Categoria { get; set; }
            public string Nucleo { get; set; }
            public string Classificacao { get; set; }
            public string Endereco { get; set; }
            public string Bairro { get; set; }
            public string Cidade { get; set; }
            public string Email { get; set; }
            public string Site { get; set; }
            public string NumConstituicao { get; set; }
            public DateTime? DtConstituicao { get; set; }

            public string Criacao
            {
                get
                {
                    if (!DtConstituicao.HasValue || string.IsNullOrEmpty(NumConstituicao))
                        return "-";
                    else
                        return NumConstituicao + " - " + DtConstituicao.Value.ToString("dd/MM/yyyy");
                }
            }

            private string _funcionamento;
            public string Funcionamento
            {
                get { return Funcoes.GetHorarioFuncionamento(this._funcionamento); }
                set { this._funcionamento = value; }
            }

            private string _cnpjNum;
            public string CnpjNum
            {
                get { return this._cnpjNum.Format(TipoFormat.CNPJ); }
                set { this._cnpjNum = value; }
            }

            private string _CEP;
            public string CEP
            {
                get { return this._CEP.Format(TipoFormat.CEP); }
                set { this._CEP = value; }
            }

            private string _telefone;
            public string Telefone
            {
                get { return this._telefone.Format(TipoFormat.Telefone); }
                set { this._telefone = value; }
            }

            public string _fax;
            public string Fax
            {
                get { return this._fax.Format(TipoFormat.Telefone); }
                set { this._fax = value; }
            }

            public List<Gestor> Gestores { get; set; }
            public List<QtdAlunos> QtdAlunos { get; set; }
            public List<QtdColaboradores> QtdColaboradores { get; set; }
            public List<Distribuicao> Distribuicoes { get; set; }
        }

        public class Gestor
        {
            public string Nome { get; set; }
            public string Sexo { get; set; }
            public string Funcao { get; set; }
            public DateTime? DtInicio { get; set; }
            public DateTime DtNasc { get; set; }

            public int Idade
            {
                get { return Funcoes.GetIdade(DtNasc); }
            }

            public string _matricula;
            public string Matricula
            {
                get { return this._matricula.Format(TipoFormat.MatriculaColaborador); }
                set { this._matricula = value; }
            }

            public string _telefone;
            public string Telefone
            {
                get { return this._telefone.Format(TipoFormat.Telefone); }
                set { this._telefone = value; }
            }
        }

        public class QtdAlunos
        {
            public string Serie { get; set; }

            public string Ano { get; set; }
            public int Total { get; set; }

            public int? TotalAno1 { get; set; }
            public int? TotalAno2 { get; set; }
            public int? TotalAno3 { get; set; }
            public int? TotalAno4 { get; set; }
        }

        public class QtdColaboradores
        {
            public string TipoColab { get; set; }

            public int Ano { get; set; }
            public int Total { get; set; }

            public int? Ano1 { get; set; }
            public int? Ano2 { get; set; }
            public int? Ano3 { get; set; }
            public int? Ano4 { get; set; }
        }

        public class Distribuicao
        {
            public string Funcao { get; set; }

            public int? ATI { get; set; }
            public string ATIStr { get { return ATI.HasValue ? ATI.Value.ToString() : "-"; } }
            public int? ATE { get; set; }
            public string ATEStr { get { return ATE.HasValue ? ATE.Value.ToString() : "-"; } }
            public int? LME { get; set; }
            public string LMEStr { get { return LME.HasValue ? LME.Value.ToString() : "-"; } }
            public int? LFR { get; set; }
            public string LFRStr { get { return LFR.HasValue ? LFR.Value.ToString() : "-"; } }
            public int? TRE { get; set; }
            public string TREStr { get { return TRE.HasValue ? TRE.Value.ToString() : "-"; } }
            public int? SUS { get; set; }
            public string SUSStr { get { return SUS.HasValue ? SUS.Value.ToString() : "-"; } }
            public int? LMA { get; set; }
            public string LMAStr { get { return LMA.HasValue ? LMA.Value.ToString() : "-"; } }
            public int? FES { get; set; }
            public string FESStr { get { return FES.HasValue ? FES.Value.ToString() : "-"; } }
            public int? FER { get; set; }
            public string FERStr { get { return FER.HasValue ? FER.Value.ToString() : "-"; } }
            public int? FCE { get; set; }
            public string FCEStr { get { return FCE.HasValue ? FCE.Value.ToString() : "-"; } }
            public int? Prof { get; set; }
            public string ProfStr { get { return Prof.HasValue ? Prof.Value.ToString() : "-"; } }

            public int? Total { get; set; }
            public string TotalStr { get { return Total.HasValue ? Total.Value.ToString() : "-"; } }
        }

        #endregion
    }
}

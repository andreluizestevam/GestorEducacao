using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;
using System.Globalization;

namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1200_GestaoOperColaboradores
{
    public partial class RptDemonstrativoFreqFuncional : C2BR.GestorEducacao.Reports.RptRetrato
    {
        #region ctor

        public RptDemonstrativoFreqFuncional()
        {
            InitializeComponent();
        }

        #endregion

        #region Init Report

        public int InitReport(int codEmp, int codEmpRef, int codCol, int anoBase, string infos)
        {
            try
            {
                #region Inicializa o header/Labels

                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;

                // Cria o header a partir do cod da instituicao
                var header = ReportHeader.GetHeaderFromEmpresa(codEmp);
                if (header == null)
                    return -1;

                // Inicializa o header
                base.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Ocorrencias

                var col = (from c in ctx.TB03_COLABOR
                            join f in ctx.TB15_FUNCAO on c.CO_FUN equals f.CO_FUN into fi
                            from f in fi.DefaultIfEmpty()
                            where c.CO_EMP == codEmpRef && c.CO_COL == codCol
                            select new
                            {
                                Nome = c.NO_COL,
                                Funcao = f.NO_FUN,
                                DtNasc = c.DT_NASC_COL,
                                CPF = c.NU_CPF_COL,
                                Telefone = c.NU_TELE_RESI_COL
                            }).First();

                lblColaborador.Text = col.Nome;
                lblPeriodo.Text = anoBase.ToString();
                lblFuncao.Text = col.Funcao;
                lblDtNasc.Text = col.DtNasc.ToString("dd/MM/yyyy");
                lblCpf.Text = Funcoes.Format(col.CPF, TipoFormat.CPF);
                lblFone.Text = Funcoes.Format(col.Telefone, TipoFormat.Telefone);

                var res = (from m in ctx.TB199_FREQ_FUNC
                           where m.TB03_COLABOR.CO_COL == codCol
                           && m.DT_FREQ.Year == anoBase
                           && m.TP_FREQ == "E" && m.STATUS == "A"
                           select new
                           {
                               Data = m.DT_FREQ,
                               Flag = m.FLA_PRESENCA
                           }).ToList();

                var d = new DemonsFrequencia()
                {
                    JanFaltas = res.Where(x => x.Data.Month == 1 && (x.Flag == "N" || x.Flag == "F")).Count(),
                    FevFaltas = res.Where(x => x.Data.Month == 2 && (x.Flag == "N" || x.Flag == "F")).Count(),
                    MarFaltas = res.Where(x => x.Data.Month == 3 && (x.Flag == "N" || x.Flag == "F")).Count(),
                    AbrFaltas = res.Where(x => x.Data.Month == 4 && (x.Flag == "N" || x.Flag == "F")).Count(),
                    MaiFaltas = res.Where(x => x.Data.Month == 5 && (x.Flag == "N" || x.Flag == "F")).Count(),
                    JunFaltas = res.Where(x => x.Data.Month == 6 && (x.Flag == "N" || x.Flag == "F")).Count(),
                    JulFaltas = res.Where(x => x.Data.Month == 7 && (x.Flag == "N" || x.Flag == "F")).Count(),
                    AgoFaltas = res.Where(x => x.Data.Month == 8 && (x.Flag == "N" || x.Flag == "F")).Count(),
                    SetFaltas = res.Where(x => x.Data.Month == 9 && (x.Flag == "N" || x.Flag == "F")).Count(),
                    OutFaltas = res.Where(x => x.Data.Month == 10 && (x.Flag == "N" || x.Flag == "F")).Count(),
                    NovFaltas = res.Where(x => x.Data.Month == 11 && (x.Flag == "N" || x.Flag == "F")).Count(),
                    DezFaltas = res.Where(x => x.Data.Month == 12 && (x.Flag == "N" || x.Flag == "F")).Count(),
                    TotalFaltas = res.Where(x => x.Flag == "N" || x.Flag == "F").Count(),

                    JanPres = res.Where(x => x.Data.Month == 1 && x.Flag == "S").Count(),
                    FevPres = res.Where(x => x.Data.Month == 2 && x.Flag == "S").Count(),
                    MarPres = res.Where(x => x.Data.Month == 3 && x.Flag == "S").Count(),
                    AbrPres = res.Where(x => x.Data.Month == 4 && x.Flag == "S").Count(),
                    MaiPres = res.Where(x => x.Data.Month == 5 && x.Flag == "S").Count(),
                    JunPres = res.Where(x => x.Data.Month == 6 && x.Flag == "S").Count(),
                    JulPres = res.Where(x => x.Data.Month == 7 && x.Flag == "S").Count(),
                    AgoPres = res.Where(x => x.Data.Month == 8 && x.Flag == "S").Count(),
                    SetPres = res.Where(x => x.Data.Month == 9 && x.Flag == "S").Count(),
                    OutPres = res.Where(x => x.Data.Month == 10 && x.Flag == "S").Count(),
                    NovPres = res.Where(x => x.Data.Month == 11 && x.Flag == "S").Count(),
                    DezPres = res.Where(x => x.Data.Month == 12 && x.Flag == "S").Count(),
                    TotalPres = res.Where(x => x.Flag == "S").Count(),
                };

                #endregion

                // Adiciona ao DataSource do Relatório
                bsReport.Clear();
                bsReport.Add(d);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Class DemonsFrequencia

        public class DemonsFrequencia
        {
            public int JanFaltas { get; set; }
            public int JanPres { get; set; }
            public int FevFaltas { get; set; }
            public int FevPres { get; set; }
            public int MarFaltas { get; set; }
            public int MarPres { get; set; }
            public int AbrFaltas { get; set; }
            public int AbrPres { get; set; }
            public int MaiFaltas { get; set; }
            public int MaiPres { get; set; }
            public int JunFaltas { get; set; }
            public int JunPres { get; set; }
            public int JulFaltas { get; set; }
            public int JulPres { get; set; }
            public int AgoFaltas { get; set; }
            public int AgoPres { get; set; }
            public int SetFaltas { get; set; }
            public int SetPres { get; set; }
            public int OutFaltas { get; set; }
            public int OutPres { get; set; }
            public int NovFaltas { get; set; }
            public int NovPres { get; set; }
            public int DezFaltas { get; set; }
            public int DezPres { get; set; }

            public int TotalFaltas { get; set; }
            public int TotalPres { get; set; }

            public string StrJanFaltas { get { return JanFaltas == 0 ? "-" : JanFaltas.ToString(); } }
            public string StrFevFaltas { get { return FevFaltas == 0 ? "-" : FevFaltas.ToString(); } }
            public string StrMarFaltas { get { return MarFaltas == 0 ? "-" : MarFaltas.ToString(); } }
            public string StrAbrFaltas { get { return AbrFaltas == 0 ? "-" : AbrFaltas.ToString(); } }
            public string StrMaiFaltas { get { return MaiFaltas == 0 ? "-" : MaiFaltas.ToString(); } }
            public string StrJunFaltas { get { return JunFaltas == 0 ? "-" : JunFaltas.ToString(); } }
            public string StrJulFaltas { get { return JulFaltas == 0 ? "-" : JulFaltas.ToString(); } }
            public string StrAgoFaltas { get { return AgoFaltas == 0 ? "-" : AgoFaltas.ToString(); } }
            public string StrSetFaltas { get { return SetFaltas == 0 ? "-" : SetFaltas.ToString(); } }
            public string StrOutFaltas { get { return OutFaltas == 0 ? "-" : OutFaltas.ToString(); } }
            public string StrNovFaltas { get { return NovFaltas == 0 ? "-" : NovFaltas.ToString(); } }
            public string StrDezFaltas { get { return DezFaltas == 0 ? "-" : DezFaltas.ToString(); } }

            public string StrJanPres { get { return JanPres == 0 ? "-" : JanPres.ToString(); } }
            public string StrFevPres { get { return FevPres == 0 ? "-" : FevPres.ToString(); } }
            public string StrMarPres { get { return MarPres == 0 ? "-" : MarPres.ToString(); } }
            public string StrAbrPres { get { return AbrPres == 0 ? "-" : AbrPres.ToString(); } }
            public string StrMaiPres { get { return MaiPres == 0 ? "-" : MaiPres.ToString(); } }
            public string StrJunPres { get { return JunPres == 0 ? "-" : JunPres.ToString(); } }
            public string StrJulPres { get { return JulPres == 0 ? "-" : JulPres.ToString(); } }
            public string StrAgoPres { get { return AgoPres == 0 ? "-" : AgoPres.ToString(); } }
            public string StrSetPres { get { return SetPres == 0 ? "-" : SetPres.ToString(); } }
            public string StrOutPres { get { return OutPres == 0 ? "-" : OutPres.ToString(); } }
            public string StrNovPres { get { return NovPres == 0 ? "-" : NovPres.ToString(); } }
            public string StrDezPres { get { return DezPres == 0 ? "-" : DezPres.ToString(); } }
        }

        #endregion
    }
}

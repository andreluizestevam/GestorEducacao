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
    public partial class RptFolhadePonto : C2BR.GestorEducacao.Reports.RptRetrato
    {
        string mesAno;
        #region ctor

        public RptFolhadePonto()
        {
            InitializeComponent();
        }

        #endregion

        #region Init Report

        public int InitReport(int codEmp, int codEmpRef, int codCol, int anoBase, int mesRefer, string strMesRefere, string strTipoFuncionario, string infos)
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

                mesAno = "/" + mesRefer.ToString() + "/" + anoBase.ToString();
                lblMesAno.Text = strMesRefere.ToUpper() + " / " + anoBase.ToString();

                TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(codEmp);
                TB904_CIDADE tb904 = TB904_CIDADE.RetornaPelaChavePrimaria(tb25.CO_CIDADE);

                lblDescDataAtual.Text = tb904.NO_CIDADE + " - " + tb25.CO_UF_EMP + ", " + DateTime.Now.ToString("dd") + " de " + DateTime.Now.ToString("MMMM") + " de " + DateTime.Now.ToString("yyyy");

                #region Query Ocorrencias

                var col = (from c in ctx.TB03_COLABOR
                            join f in ctx.TB15_FUNCAO on c.CO_FUN equals f.CO_FUN into fi
                            from f in fi.DefaultIfEmpty()
                            where c.CO_EMP == codEmpRef && (codCol != 0 ? c.CO_COL == codCol : codCol == 0)
                            && (strTipoFuncionario != "T" ? c.FLA_PROFESSOR == strTipoFuncionario : 0 == 0)
                            && (c.CO_SITU_COL != "DEM")
                            select new Funcionario
                            {
                                Nome = c.NO_COL,
                                Funcao = c.DE_FUNC_COL.ToUpper(),
                                Matricula = c.CO_MAT_COL,
                                Codigo = c.CO_COL
                            }).OrderBy( c => c.Nome );

                var funcionario = col.ToList();

                #endregion

                #region Folha Ponto

                List<FolhaPonto> lstFolhaPonto = new List<FolhaPonto>();

                int qtdeDiasMes = DateTime.DaysInMonth(anoBase, mesRefer);
                DateTime dataRefer;

                for (int i = 1; i <= qtdeDiasMes; i++)                    
			    {
                    dataRefer = DateTime.Parse(i.ToString() + "/" + mesRefer.ToString() + "/" + anoBase.ToString());
                    TB157_CALENDARIO_ATIVIDADES calAtv = TB157_CALENDARIO_ATIVIDADES.RetornaTodosRegistros().Where(w => w.CAL_ANO_REFER_CALEND == dataRefer.Year && w.CAL_TIPO_DIA_CALEND == "F" && w.CAL_DATA_CALEND == dataRefer).FirstOrDefault();
                    string nmFeriado = calAtv != null ? calAtv.CAL_NOME_ATIVID_CALEND : "";

                    FolhaPonto fP = new FolhaPonto { 
                        Dia = i.ToString().PadLeft(2,'0'),
                        DiaSemana = Funcoes.GetDiaSemana(dataRefer.Year, dataRefer.Month, dataRefer.Day),
                        //DesDiaUtil = dataRefer.DayOfWeek == DayOfWeek.Saturday ? "SÁBADO" : dataRefer.DayOfWeek == DayOfWeek.Sunday ? "DOMINGO" : nmFeriado  
                        DesDiaUtil = calAtv != null ? "FERIADO" : ""
                    };

			        lstFolhaPonto.Add(fP);
			    }

                foreach (var lstFuncionario in funcionario)
                {
                    lstFuncionario.FolhaPontos = lstFolhaPonto;
                }

                #endregion

                // Adiciona ao DataSource do Relatório
                bsReport.Clear();

                foreach (var o in funcionario)
                    bsReport.Add(o);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Class Folha de Ponto

        public class Funcionario
        {
            public Funcionario()
            {
                this.FolhaPontos = new List<FolhaPonto>();
            }

            public int Codigo { get; set; }

            public string Matricula { get; set; }

            public string Nome { get; set; }

            public string Funcao { get; set; }

            public List<FolhaPonto> FolhaPontos { get; set; }
        }

        public class FolhaPonto
        {
            public string Dia { get; set; }

            public string DiaSemana { get; set; }

            public string DesDiaUtil { get; set; }
        }

        #endregion

        private void lblD1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            DateTime dt = DateTime.Parse(obj.Text + mesAno);

            if (!(dt.DayOfWeek != DayOfWeek.Saturday && dt.DayOfWeek != DayOfWeek.Sunday))
            {
                obj.BackColor = Color.LightGray;
                lblAssinatura1.BackColor = Color.LightGray;
                lblE1.BackColor = Color.LightGray;
                lblI1.BackColor = Color.LightGray;
                lblS1.BackColor = Color.LightGray;
                lblDiaSemana.BackColor = Color.LightGray;

                obj.BackColor = Color.LightGray;
                lblAssinatura2.BackColor = Color.LightGray;
                lblE2.BackColor = Color.LightGray;
                lblI2.BackColor = Color.LightGray;
                lblS2.BackColor = Color.LightGray;
            }
            else
            {
                // VALIDA SE O DIA E UM FERIADO
                if (TB157_CALENDARIO_ATIVIDADES.RetornaTodosRegistros().Where(w => w.CAL_ANO_REFER_CALEND == dt.Year && w.CAL_TIPO_DIA_CALEND == "F" && w.CAL_DATA_CALEND == dt).Any())
                {
                    obj.BackColor = Color.LightGray;
                    lblAssinatura1.BackColor = Color.LightGray;
                    lblE1.BackColor = Color.LightGray;
                    lblI1.BackColor = Color.LightGray;
                    lblS1.BackColor = Color.LightGray;
                    lblDiaSemana.BackColor = Color.LightGray;

                    obj.BackColor = Color.LightGray;
                    lblAssinatura2.BackColor = Color.LightGray;
                    lblE2.BackColor = Color.LightGray;
                    lblI2.BackColor = Color.LightGray;
                    lblS2.BackColor = Color.LightGray;
                }
                else
                {
                    obj.BackColor = Color.Transparent;
                    lblAssinatura1.BackColor = Color.Transparent;
                    lblE1.BackColor = Color.Transparent;
                    lblI1.BackColor = Color.Transparent;
                    lblS1.BackColor = Color.Transparent;
                    lblDiaSemana.BackColor = Color.Transparent;

                    obj.BackColor = Color.Transparent;
                    lblAssinatura2.BackColor = Color.Transparent;
                    lblE2.BackColor = Color.Transparent;
                    lblI2.BackColor = Color.Transparent;
                    lblS2.BackColor = Color.Transparent;
                }
            }
        }

        private void lblD2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;

            if (!(DateTime.Parse(obj.Text + mesAno).DayOfWeek != DayOfWeek.Saturday && DateTime.Parse(obj.Text + mesAno).DayOfWeek != DayOfWeek.Sunday))
            {
                obj.BackColor = Color.LightGray;
                lblAssinatura2.BackColor = Color.LightGray;
                lblE2.BackColor = Color.LightGray;
                lblI2.BackColor = Color.LightGray;
                lblS2.BackColor = Color.LightGray;
            }
            else
            {
                obj.BackColor = Color.Transparent;
                lblAssinatura2.BackColor = Color.Transparent;
                lblE2.BackColor = Color.Transparent;
                lblI2.BackColor = Color.Transparent;
                lblS2.BackColor = Color.Transparent;
            }
        }
    }
}

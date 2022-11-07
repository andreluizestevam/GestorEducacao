using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.Reports.Helper;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario
{
    public partial class rptRelacaoUsuarioCidadeBairro : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        int qdtDef = 0;
        int qdtMasc = 0;
        int qdtFem = 0;
        int qdtTotal = 0;

        public rptRelacaoUsuarioCidadeBairro()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                               int codEmp,
                               string codUndCont,
                               string codUF,
                               string codCidade,
                               string codBairro,
                               string infos)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(codEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Alunos

                int intCid = codCidade != "T" ? int.Parse(codCidade) : 0;
                int intBairro = codBairro != "T" ? int.Parse(codBairro) : 0;

                var lst = (from alu in ctx.TB07_ALUNO
                           join cur in ctx.TB01_CURSO on alu.CO_CUR equals cur.CO_CUR
                           join tur in ctx.TB06_TURMAS on alu.CO_TUR equals tur.CO_TUR
                           join cid in ctx.TB904_CIDADE on alu.TB905_BAIRRO.CO_CIDADE equals cid.CO_CIDADE
                           where (codUF != "T" ? cid.CO_UF == codUF : 0 == 0)
                           && (codCidade != "T" ? cid.CO_CIDADE == intCid : 0 == 0)
                            && (codBairro != "T" ? alu.TB905_BAIRRO.CO_BAIRRO == intBairro : 0 == 0)
                           select new AlunoCidadeBairro
                           {
                               NIRE = alu.NU_NIRE,
                               Nome = alu.NO_ALU,
                               Sexo = alu.CO_SEXO_ALU,
                               Deficiencia = alu.DES_DEF != "" ? alu.DES_DEF : "Nenhuma",
                               Serie = cur.CO_SIGL_CUR,
                               Turma = tur.TB129_CADTURMAS.CO_SIGLA_TURMA,
                               DataNasc = alu.DT_NASC_ALU.HasValue ? alu.DT_NASC_ALU.Value : DateTime.Now,
                               Endereco = alu.DE_ENDE_ALU,
                               Bairro = alu.TB905_BAIRRO.NO_BAIRRO,
                               Cidade = cid.NO_CIDADE,
                               UF = cid.CO_UF,
                               TelFixo = alu.NU_TELE_RESI_ALU

                           });

                var res = lst.OrderBy(x => new { x.Bairro, x.Nome }).ToList();


                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;


                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (AlunoCidadeBairro at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion


        private void OnSummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = "Total de Alunos:" + qdtTotal.ToString() + " ( Masculino: " + qdtMasc.ToString() + " Feminino: " + qdtFem.ToString() + " Deficiência: " + qdtDef.ToString() + " )";
            e.Handled = true;
        }

        private void OnSummaryReset(object sender, EventArgs e)
        {
            qdtDef = 0;
            qdtFem = 0;
            qdtMasc = 0;
            qdtTotal = 0;
        }

        private void colSexo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrLabel2_SummaryRowChanged(object sender, EventArgs e)
        {
            qdtTotal++;
            if (colSexo.Text != "M")
                qdtMasc++;
            else if (colSexo.Text != "F")
                qdtFem++;
            if (ColDef.Text != "Nenhuma")
                qdtDef++;
        }

    }

    public class AlunoCidadeBairro
    {
        public int NIRE { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNasc { get; set; }
        public string Deficiencia { get; set; }
        public string Serie { get; set; }
        public string Turma { get; set; }
        public string Endereco { get; set; }
        public string TelFixo { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }

        public string UFCidade { get { return "Cidade/UF: " + Cidade + "/" + UF; } }

        public string CodNIRE { get { return NIRE.ToString().PadLeft(11, '0'); } }

        public int Idade
        {
            get
            {
                DateTime now = DateTime.Today;
                int age = now.Year - DataNasc.Year;
                if (DataNasc > now.AddYears(-age))
                    age--;

                return age;
            }
        }

        public string TelDesc
        {
            get
            {
                if (TelFixo != null)
                    return TelFixo.ToString().Format(TipoFormat.Telefone);
                else
                    return "";
            }
        }
    }
}

using System;
using System.Linq;
using System.Drawing;
using C2BR.GestorEducacao.Reports.Helper;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects;
using System.Text;
using System.Data.SqlClient;
using System.IO;

namespace C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2114_ControleCredito
{
    public partial class RptExtratoTransfCredi : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptExtratoTransfCredi()
        {
            InitializeComponent();
        }

        public int InitReport(
            string parametros,
            int coEmp,
            string infos,
            int coAlu,
            int coAluC,
            DateTime dtIni,
            DateTime? dtFim,
            int coCol)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;
                this.VisiblePageHeader = true;
                this.VisibleNumeroPage = true;
                this.VisibleDataHeader = true;
                this.VisibleHoraHeader = true;

                // Instancia o header do relatorio
                ReportHeader header =  GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);

                #region Pega as informações do diretor
                TB25_EMPRESA emp = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                emp.TB83_PARAMETROReference.Load();

                TB03_COLABOR dir = null;
                if (emp.TB83_PARAMETRO.CO_DIR1 != null)
                    dir = TB03_COLABOR.RetornaPelaChavePrimaria(coEmp, emp.TB83_PARAMETRO.CO_DIR1.Value);

                string nomDiretor = "Diretor(a)";
                string cpfDiretor = "";
                if (dir != null)
                {
                    nomDiretor = dir.NO_COL;
                    cpfDiretor = Convert.ToUInt64(dir.NU_CPF_COL).ToString(@"000\.000\.000\-00");
                }

                lblNomeDiretor.Text = nomDiretor;
                lblCpfDiretor.Text = cpfDiretor;
                #endregion

                #region Pega as informações do funcionário logado
                TB03_COLABOR col = TB03_COLABOR.RetornaPelaChavePrimaria(coEmp, coCol);

                string nomCol = "";
                string cpfCol = "";
                if (col != null)
                {
                    nomCol = col.NO_COL;
                    cpfCol = col.NU_CPF_COL != null ? Convert.ToUInt64(col.NU_CPF_COL).ToString(@"000\.000\.000\-00") : "";
                }

                lblNomeColabor.Text = nomCol;
                lblCpfColabor.Text = cpfCol;
                #endregion

                #region Pega as informações do aluno
                TB07_ALUNO ialu = TB07_ALUNO.RetornaPelaChavePrimaria(coAlu, coEmp);

                string nomAluno = "";
                string cpfAluno = "";

                if (ialu != null)
                {
                    nomAluno = ialu.NO_ALU;
                    cpfAluno = ialu.NU_CPF_ALU != null && ialu.NU_CPF_ALU != "" ? Convert.ToUInt64(ialu.NU_CPF_ALU).ToString(@"000\.000\.000\-00") : "";
                }

                lblNomeAluno.Text = nomAluno;
                lblCpfAluno.Text = cpfAluno;
                #endregion

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                #region Query do relatório

                var res = (from cred in TB122_ALUNO_CREDI.RetornaTodosRegistros()
                           join alu in TB07_ALUNO.RetornaTodosRegistros() on cred.CO_ALU equals alu.CO_ALU
                           //join ufAlu in TB74_UF.RetornaTodosRegistros() on alu.CO_ESTA_ALU equals ufAlu.CODUF
                           join aluC in TB07_ALUNO.RetornaTodosRegistros() on cred.CO_ALU_CRED equals aluC.CO_ALU
                           //join ufAluC in TB74_UF.RetornaTodosRegistros() on aluC.CO_ESTA_ALU equals ufAluC.CODUF
                           join mat in TB02_MATERIA.RetornaTodosRegistros() on cred.CO_MAT equals mat.CO_MAT
                           join mate in TB107_CADMATERIAS.RetornaTodosRegistros() on mat.ID_MATERIA equals mate.ID_MATERIA
                           join mod in TB44_MODULO.RetornaTodosRegistros() on cred.TB44_MODULO.CO_MODU_CUR equals mod.CO_MODU_CUR
                           join cur in TB01_CURSO.RetornaTodosRegistros() on cred.CO_CUR equals cur.CO_CUR
                           where (coAlu != 0 ? cred.CO_ALU == coAlu : 0 == 0)
                           && (coAluC != 0 ? cred.CO_ALU_CRED == coAluC : 0 == 0)
                           //&& (cred.DT_CRED >= dtIni && cred.DT_CRED <= dtFim)
                           select new ExtratoTransfCredi
                           {
                               coCur = cur.CO_CUR,
                               noCur = cur.NO_CUR,

                               qtCredMat = mat.QT_CRED_MAT,

                               coAlu = alu.CO_ALU,
                               noAlu = alu.NO_ALU,
                               coSexoAlu = alu.CO_SEXO_ALU,
                               dtNascALu = alu.DT_NASC_ALU,
                               nuNireAlu = alu.NU_NIRE,
                               deEndAlu = alu.DE_ENDE_ALU,
                               compEndAlu = alu.DE_COMP_ALU,
                               noBaiAlu = alu.TB905_BAIRRO.NO_BAIRRO,
                               noCidAlu = alu.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                               noUfAlu = alu.CO_ESTA_ALU,
                               nuCepAlu = alu.CO_CEP_ALU,
                               coCpfAlu = alu.NU_CPF_ALU,
                               coRgAlu = alu.CO_RG_ALU,
                               nuTelAlu = alu.NU_TELE_RESI_ALU,
                               nuCelAlu = alu.NU_TELE_CELU_ALU,
                               nuTComAlu = alu.NU_TELE_COME_ALU,
                               coFotoAlu = alu.Image.ImageStream,

                               coAluC = aluC.CO_ALU,
                               noAluC = aluC.NO_ALU,
                               coSexoAluC = aluC.CO_SEXO_ALU,
                               dtNascALuC = aluC.DT_NASC_ALU,
                               nuNireAluC = aluC.NU_NIRE,
                               deEndAluC = aluC.DE_ENDE_ALU,
                               compEndAluC = aluC.DE_COMP_ALU,
                               noBaiAluC = aluC.TB905_BAIRRO.NO_BAIRRO,
                               noCidAluC = aluC.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                               noUfAluC = aluC.CO_ESTA_ALU,
                               nuCepAluC = aluC.CO_CEP_ALU,
                               coCpfAluC = aluC.NU_CPF_ALU,
                               coRgAluC = aluC.CO_RG_ALU,
                               nuTelAluC = aluC.NU_TELE_RESI_ALU,
                               nuCelAluC = aluC.NU_TELE_CELU_ALU,
                               nuTComAluC = aluC.NU_TELE_COME_ALU,
                               coFotoAluC = aluC.Image.ImageStream,

                               coMat = mat.CO_MAT,
                               noMat = mate.NO_MATERIA,
                               vlCred = cred.VL_CRED,
                               dtCredDT = cred.DT_CRED.Value
                           }).ToList();

                #endregion

                if (res.Count == 0)
                    return -1;

                // Seta os dados no DataSource do Relatorio
                bsReport.Clear();

                int i = 1;
                foreach (ExtratoTransfCredi at in res)
                {
                    at.cont = i;
                    bsReport.Add(at);
                    i++;
                }


                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public class ExtratoTransfCredi
        {
            public int cont { get; set; }

            // Informações do aluno que transferiu o crédito
            public int coAlu { get; set; }
            public string noAlu { get; set; }
            public string coSexoAlu { get; set; }
            public string SexoAlu
            {
                get
                {
                    return this.coSexoAlu != "M" ? "Feminino" : "Masculino";
                }
            }
            public DateTime? dtNascALu { get; set; }
            public string NascAlu
            {
                get
                {
                    return this.dtNascALu.HasValue ? this.dtNascALu.Value.ToString("dd/MM/yyyy") : "-";
                }
            }
            public int nuNireAlu { get; set; }
            public string NireAlu
            {
                get
                {
                    return this.nuNireAlu.ToString().PadLeft(7, '0');
                }
            }
            public string deEndAlu { get; set; }
            public string compEndAlu { get; set; }
            public string noBaiAlu { get; set; }
            public string noCidAlu { get; set; }
            public string noUfAlu { get; set; }
            public string nuCepAlu { get; set; }
            public string coCpfAlu { get; set; }
            public string CpfAlu
            {
                get
                {
                    return this.coCpfAlu != null && this.coCpfAlu != "" ? Convert.ToUInt64(this.coCpfAlu).ToString(@"000\.000\.000\-00") : "-";
                }
            }
            public string coRgAlu { get; set; }
            public string nuTelAlu { get; set; }
            public string nuCelAlu { get; set; }
            public string nuTComAlu { get; set; }
            public string nuTelAluF 
            {
                get
                {
                    return this.nuTelAlu != null ? String.Format("{0:(###) ####-####}", double.Parse(this.nuTelAlu)) : "-";
                }
            }
            public string nuCelAluF
            {
                get
                {
                    return this.nuCelAlu != null ? String.Format("{0:(###) ####-####}", double.Parse(this.nuCelAlu)) : "-";
                }
            }
            public string nuTComAluF
            {
                get
                {
                    return this.nuTComAlu != null ? String.Format("{0:(###) ####-####}", double.Parse(this.nuTComAlu)) : "-";
                }
            }
            public byte[] coFotoAlu { get; set; }
            public System.Drawing.Image fotoAlu
            {
                get
                {
                    return this.coFotoAlu != null ? System.Drawing.Image.FromStream(new MemoryStream(this.coFotoAlu)) : null;
                }
            }
            
            // Informações do aluno que recebeu o crédito
            public int coAluC { get; set; }
            public string noAluC { get; set; }
            public string coSexoAluC { get; set; }
            public string SexoAluC
            {
                get
                {
                    return this.coSexoAluC != "M" ? "Feminino" : "Masculino";
                }
            }
            public DateTime? dtNascALuC { get; set; }
            public string NascAluC
            {
                get
                {
                    return this.dtNascALuC.HasValue ? this.dtNascALuC.Value.ToString("dd/MM/yyyy") : "-";
                }
            }
            public int nuNireAluC { get; set; }
            public string NireAluC
            {
                get
                {
                    return this.nuNireAluC.ToString().PadLeft(7, '0');
                }
            }
            public string deEndAluC { get; set; }
            public string compEndAluC { get; set; }
            public string noBaiAluC { get; set; }
            public string noCidAluC { get; set; }
            public string noUfAluC { get; set; }
            public string nuCepAluC { get; set; }
            public string coCpfAluC { get; set; }
            public string CpfAluC
            {
                get
                {
                    return this.coCpfAluC != null && this.coCpfAluC != "" ? Convert.ToUInt64(this.coCpfAluC).ToString(@"000\.000\.000\-00") : "-";
                }
            }
            public string coRgAluC { get; set; }
            public string nuTelAluC { get; set; }
            public string nuCelAluC { get; set; }
            public string nuTComAluC { get; set; }
            public string nuTelAluFC
            {
                get
                {
                    return this.nuTelAluC != null ? String.Format("{0:(###) ####-####}", double.Parse(this.nuTelAluC)) : "-";
                }
            }
            public string nuCelAluFC
            {
                get
                {
                    return this.nuCelAluC != null ? String.Format("{0:(###) ####-####}", double.Parse(this.nuCelAluC)) : "-";
                }
            }
            public string nuTComAluFC
            {
                get
                {
                    return this.nuTComAluC != null ? String.Format("{0:(###) ####-####}", double.Parse(this.nuTComAluC)) : "-";
                }
            }
            public byte[] coFotoAluC { get; set; }
            public System.Drawing.Image fotoAluC
            {
                get
                {
                    return this.coFotoAluC != null ? System.Drawing.Image.FromStream(new MemoryStream(this.coFotoAluC)) : null;
                }
            }

            // Informações das matérias
            public int coMat { get; set; }
            public int coCur { get; set; }
            public string noCur { get; set; }
            public string noMat { get; set; }
            public int? qtCredMat { get; set; }
            public string qtCred
            {
                get
                {
                    return this.qtCredMat != null ? this.qtCredMat.Value.ToString() : "-";
                }
            }
            public decimal vlCred { get; set; }
            public string vlCredF
            {
                get
                {
                    return "R$ " + this.vlCred.ToString("N2");
                }
            }
            public DateTime dtCredDT { get; set; }
            public string dtCred
            {
                get
                {
                    return this.dtCredDT.ToString("dd/MM/yyyy");
                }
            }
            public string Declaro
            {
                get
                {
                    return "Declaro ter recebido nesta data  ___/___/_____  a guia de transferência de crédito " +
                        "confirmando a transferência de crédito da matéria " + this.noMat +
                        " do aluno " + this.noAlu.ToUpper() + ", Registro nr. " + this.NireAlu + ", " +
                        " para o aluno " + this.noAluC.ToUpper() + ", Registro nr. " + this.NireAluC + ".";
                }
            }

        }
    }
}

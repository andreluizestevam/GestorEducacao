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

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3700_CtrlInformacoesResponsaveis
{
    public partial class RtpResumoResponsavelAlunoMatriculado : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RtpResumoResponsavelAlunoMatriculado()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                                int codEmp,
                                int codUndCont,
                                string ano,
                                string strModalidade,
                                string strSerieCurso,
                                string strTurma,
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

                #region Query

                int intTurma = int.Parse(strTurma);
                int intModalidade = int.Parse(strModalidade);
                int intSerieCurso =  int.Parse(strSerieCurso);


                var lst = (from mat in ctx.TB08_MATRCUR
                           join curs in ctx.TB01_CURSO on mat.CO_CUR equals curs.CO_CUR
                           join turm in ctx.TB06_TURMAS on mat.CO_TUR equals turm.CO_TUR
                           join resp in ctx.TB108_RESPONSAVEL.DefaultIfEmpty()
                           on mat.TB07_ALUNO.TB108_RESPONSAVEL.CO_RESP equals resp.CO_RESP
                           join bair in ctx.TB905_BAIRRO on resp.CO_BAIRRO equals bair.CO_BAIRRO
                           join cid in ctx.TB904_CIDADE on resp.CO_CIDADE equals cid.CO_CIDADE
                           join uf in ctx.TB74_UF on cid.CO_UF equals uf.CODUF

                           where (mat.TB07_ALUNO.CO_EMP == codEmp) && (codUndCont != 0 ? mat.CO_EMP_UNID_CONT == codUndCont : 0 == 0)
                                && (intModalidade != 0 ? mat.TB44_MODULO.CO_MODU_CUR == intModalidade : 0 == 0)
                                && (intSerieCurso != 0 ? mat.CO_CUR == intSerieCurso : 0 == 0)
                                && (intTurma != 0 ? turm.CO_TUR == intTurma : 0 == 0)
                                && (mat.CO_ANO_MES_MAT == ano)
                           // group resp by new { resp, bair, cid, uf } into g

                           select new Responsavel
                            {
                                Codigo = resp.CO_RESP, //resp.CO_RESP,
                                Nome = resp.NO_RESP,
                                Sexo = resp.CO_SEXO_RESP == "F" ? "FEM" : "MAS",
                                CPF = resp.NU_CPF_RESP,
                                DataNasc = resp.DT_NASC_RESP,
                                TelCel = resp.NU_TELE_CELU_RESP,
                                TelResid = resp.NU_TELE_RESI_RESP,
                                TelComer = resp.NU_TELE_COME_RESP,
                                Facebook = resp.NM_FACEBOOK_RESP != "" ? resp.NM_FACEBOOK_RESP : "******",
                                Twitter = resp.NM_TWITTER_RESP != "" ? resp.NM_TWITTER_RESP : "******",
                                Email = resp.DES_EMAIL_RESP != "" ? resp.DES_EMAIL_RESP : "******",
                                Endereco = resp.DE_ENDE_RESP,
                                Bairro = bair.NO_BAIRRO,
                                Cidade = cid.NO_CIDADE,
                                UF = uf.CODUF,
                                CEP = resp.CO_CEP_RESP,
                                AluNIRE = mat.TB07_ALUNO.NU_NIRE,
                                AluParent = "Parentesco: " + (resp.DE_GRAU_PAREN != "" ? resp.DE_GRAU_PAREN : "*****"),
                                Aluno = mat.TB07_ALUNO.NO_ALU,
                                AluModulo = mat.TB44_MODULO.DE_MODU_CUR,
                                AluSerie = curs.NO_CUR,
                                AluTurma = turm.TB129_CADTURMAS.CO_SIGLA_TURMA,
                                AluTurno = turm.CO_PERI_TUR,
                                AluMae = mat.TB07_ALUNO.NO_MAE_ALU,
                                CelularMae = mat.TB07_ALUNO.NU_TEL_MAE,
                                ResidencialMae = "*****",
                                TwitterMae = mat.TB07_ALUNO.NO_TWITTER_MAE,
                                FacebookMae = mat.TB07_ALUNO.NO_FACEBOOK_MAE,
                                EmailMae = mat.TB07_ALUNO.NO_EMAIL_MAE,
                                AluPai = mat.TB07_ALUNO.NO_PAI_ALU,
                                CelularPai = mat.TB07_ALUNO.NU_TEL_PAI,
                                ResidencialPai = "*****",
                                TwitterPai = mat.TB07_ALUNO.NO_TWITTER_PAI,
                                FacebookPai = mat.TB07_ALUNO.NO_FACEBOOK_PAI,
                                EmailPai = mat.TB07_ALUNO.NO_EMAIL_PAI
                            }).OrderBy(x => x.Nome);




                var res = lst.ToList();



                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (Responsavel at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        private void xrTableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "")
                obj.Text = obj.Text.Format(TipoFormat.CPF);
        }

        private void xrTableCell14_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "")
                obj.Text = obj.Text.Format(TipoFormat.Telefone);
        }

        private void xrTableCell25_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell obj = (XRTableCell)sender;
            if (obj.Text != "")
                obj.Text = obj.Text.PadLeft(9, '0');
        }

        public int InitReport(string strParametrosRelatorio, int p, int strP_CO_EMP, string p_2)
        {
            throw new NotImplementedException();
        }
    }


    public class Responsavel
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Sexo { get; set; }
        public DateTime? DataNasc { get; set; }

        public string TelCel { get; set; }
        public string TelResid { get; set; }
        public string TelComer { get; set; }
        public string TelEmerg { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public string AluParent { get; set; }
        public string Aluno { get; set; }
        public int AluNIRE { get; set; }
        public string AluModulo { get; set; }
        public string AluTurma { get; set; }
        public string AluTurno { get; set; }
        public string AluSerie { get; set; }
        public string AluMae { get; set; }
        public string CelularMae { get; set; }
        public string ResidencialMae { get; set; }
        public string TwitterMae { get; set; }
        public string FacebookMae { get; set; }
        public string EmailMae { get; set; }
        public string AluPai { get; set; }
        public string CelularPai { get; set; }
        public string ResidencialPai { get; set; }
        public string TwitterPai { get; set; }
        public string FacebookPai { get; set; }
        public string EmailPai { get; set; }

        public string DataIdade
        {
            get
            {
                if (DataNasc.HasValue)
                {
                   DateTime hoje =  DateTime.Now;
                   int idade = 0;
                   if (DataNasc.Value.Month >= hoje.Month && DataNasc.Value.Day >= hoje.Day)
                       idade = hoje.Year - DataNasc.Value.Year;
                   else
                       idade = hoje.Year - DataNasc.Value.Year - 1;
                    
                    return DataNasc.Value.ToString("dd/MM/yyyy") + " (" + idade.ToString() + ")";

                }
                else return "";
            }
        }

        public string PaiDesc
        {
            get
            {
                return "( Pai: " + (AluPai != null ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AluPai.ToLower()) : "*****") +
                    " - Tels: " + (this.CelularPai != null ? Funcoes.Format(this.CelularPai, TipoFormat.Telefone) : "*****") +
                    " / " + (this.ResidencialPai != null ? Funcoes.Format(this.ResidencialPai, TipoFormat.Telefone) : "*****") +
                    " - Facebook: " + (this.FacebookPai != null ? this.FacebookPai : "*****") +
                    " - Twitter: " + (this.TwitterPai != null ? this.TwitterPai : "*****") +
                    " - Email: " + (this.EmailPai != null ? this.EmailPai : "*****") + " )";
            }
        }

        public string MaeDesc
        {
            get
            {
                return "( Mãe: " + (AluMae != null ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AluMae.ToLower()) : "*****") +
                    " - Tels: " + (this.CelularMae != null ? Funcoes.Format(this.CelularMae, TipoFormat.Telefone) : "*****") +
                    " / " + (this.ResidencialMae != null ? Funcoes.Format(this.ResidencialMae, TipoFormat.Telefone) : "*****") +
                    " - Facebook: " + (this.FacebookMae != null ? this.FacebookMae : "*****") +
                    " - Twitter: " + (this.TwitterMae != null ? this.TwitterMae : "*****") +
                    " - Email: " + (this.EmailMae != null ? this.EmailMae : "*****") + " )";
            }
        }

        public string AluTurmaDesc
        {
            get
            {
                return "(" + AluModulo + " - " + AluSerie + " - " + AluTurma + ")";
            }
        }

        public string AluDescricao
        {
            get
            {
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Aluno.ToLower());
            }
        }

        public string EnderDescricao
        {
            get
            {
                return Endereco + " - Bairro: " + Bairro + " - Cidade: " + Cidade + "-" + UF + " - CEP: " + CEP.Format(TipoFormat.CEP);
            }
        }
    }
}

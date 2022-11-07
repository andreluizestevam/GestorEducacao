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
    public partial class RtpRelacaoResponsavelAluno : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RtpRelacaoResponsavelAluno()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                                int codEmp,
                                string infos,
                                string codUnidade,
                                string codUf,
                                string codCidade,
                                string codBairro,
                                string ano,
                                string codMod,
                                string codSerieCur,
                                string codTurma,
                                string grInst,
                                string grParent)
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

                // Conversão das variáveis necessárias
                int int_codBairro = codBairro != "T" ? int.Parse(codBairro) : 0;
                int int_codCidade = codCidade != "T" ? int.Parse(codCidade) : 0;
                //int int_codSerieCur = codSerieCur != "T" ? int.Parse(codSerieCur) : 0;
                //int int_codTurma = codTurma != "T" ? int.Parse(codTurma) : 0;
                int int_codInst = grInst != "T" ? int.Parse(grInst) : 0;
                #region Query

                var lst = (from resp in ctx.TB108_RESPONSAVEL
                           join alu in ctx.TB07_ALUNO on resp.CO_RESP equals alu.TB108_RESPONSAVEL.CO_RESP
                           into y1 from alu in y1.DefaultIfEmpty()
                           join mat in ctx.TB08_MATRCUR on alu.CO_ALU equals mat.CO_ALU
                           into y2 from mat in y2.DefaultIfEmpty()
                           join cur in ctx.TB01_CURSO on mat.CO_CUR equals cur.CO_CUR
                           into y3 from cur in y3.DefaultIfEmpty()
                           join tur in ctx.TB06_TURMAS on mat.CO_TUR equals tur.CO_TUR
                           into y4 from tur in y4.DefaultIfEmpty()
                           join gra in ctx.TB18_GRAUINS on resp.CO_INST equals gra.CO_INST
                           into y5 from gra in y5.DefaultIfEmpty()
                           join bai in ctx.TB905_BAIRRO on resp.CO_BAIRRO equals bai.CO_BAIRRO

                           where (alu.CO_EMP == codEmp) && 
                                 (mat.CO_TUR == tur.CO_TUR) &&
                                 (codUf != "" ? resp.CO_ESTA_RESP == codUf : 0==0) &&
                                 (int_codCidade != 0 ? bai.TB904_CIDADE.CO_CIDADE == int_codCidade : 0==0) &&
                                 (int_codBairro != 0 ? bai.CO_BAIRRO == int_codBairro : 0==0) &&
                                 //(int_codSerieCur != 0 ? cur.CO_CUR == int_codSerieCur : 0==0) &&
                                 //(int_codTurma != 0 ? tur.CO_TUR == int_codTurma : 0==0) &&
                                 (int_codInst != 0 ? gra.CO_INST == int_codInst : 0==0) &&
                                 (grParent != "T" ? alu.CO_GRAU_PAREN_RESP == grParent : 0==0) &&
                                 (mat.CO_ANO_MES_MAT == ano)

                           select new ResponsavelAlu
                           {
                               Nome = resp.NO_RESP,
                               Sexo = resp.CO_SEXO_RESP,
                               DataNasc = resp.DT_NASC_RESP,
                               CidadeBAIRRO = bai.TB904_CIDADE.NO_CIDADE + "/" + bai.NO_BAIRRO,
                               nuCpf = resp.NU_CPF_RESP,
                               GrParent = alu.CO_GRAU_PAREN_RESP,
                               TelCel = resp.NU_TELE_CELU_RESP,
                               TelCom = resp.NU_TELE_COME_RESP,
                               TelRes = resp.NU_TELE_RESI_RESP,
                               Ano = mat.CO_ANO_MES_MAT
                           }

                           ).Distinct().OrderBy( x => x.Nome);

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (ResponsavelAlu at in res)
                {
                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class ResponsavelAlu
        {
            public string Nome { get; set; }
            public string Sexo { get; set; }
            public DateTime? DataNasc { get; set; }

            public string CidadeBAIRRO { get; set; }
            public string nuCpf { get; set; }
            public string Cpf
            {
                get
                {
                    string cpf = "";
                    if (nuCpf != "")
                    {
                        cpf = String.Format(@"{0:000\.000\.000\-00}", long.Parse(nuCpf));
                        return cpf;
                    }
                    else
                    {
                        return cpf;
                    }
                }
            }
            public string GrParent { get; set; }
            public string Ano { get; set; }
            public string Parentesco
            {
                get
                {
                    if (GrParent != "")
                    {
                        string retorno = "";
                        switch (GrParent)
                        {
                            case "PM":
                                retorno = "Pai/Mãe";
                                break;
                            case "TI":
                                retorno = "Tio(a)";
                                break;
                            case "AV":
                                retorno = "Avô/Avó";
                                break;
                            case "PR":
                                retorno = "Primo(a)";
                                break;
                            case "CN":
                                retorno = "Cunhado(a)";
                                break;
                            case "TU":
                                retorno = "Tutor(a)";
                                break;
                            case "IR":
                                retorno = "Irmão(ã)";
                                break;
                            case "OU":
                                retorno = "Outros";
                                break;
                        }

                        return retorno;
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            public string TelCel { get; set; }
            public string TelCom { get; set; }
            public string TelRes { get; set; }
            public string Telefone
            {
                get
                {
                    string t = "";
                    string tel = "";
                    if (TelCel != null && TelCel != "")
                    {
                        t = String.Format(@"{0:\(00\) 0000\-0000}", long.Parse(TelCel));
                    }
                    else
                    {
                        if (TelCom != null && TelCom != "")
                        {
                            t = String.Format(@"{0:\(00\) 0000\-0000}", long.Parse(TelCom));
                        }
                        else
                        {
                            if (TelRes != null && TelRes != "")
                            {
                                t = String.Format(@"{0:\(00\) 0000\-0000}", long.Parse(TelRes));
                            }
                            else
                            {
                                t = "**********";
                            }
                        }
                    }

                    return t;
                }
            }
            public string Idade
            {
                get
                {
                    if (DataNasc.HasValue)
                    {
                        DateTime hoje = DateTime.Now;
                        int idade = 0;
                        if (DataNasc.Value.Month >= hoje.Month && DataNasc.Value.Day >= hoje.Day)
                            idade = hoje.Year - DataNasc.Value.Year;
                        else
                            idade = hoje.Year - DataNasc.Value.Year - 1;

                        //return DataNasc.Value.ToString("dd/MM/yyyy") + " (" + idade.ToString() + ")";
                        return idade.ToString();

                    }
                    else return "";
                }
            }
        }

    }
}

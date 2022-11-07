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

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario
{
    public partial class RptRelInfGeraisUsuario : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptRelInfGeraisUsuario()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(string parametros, int codEmp, string infos, string strP_CO_EMP,   int codAlu)
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
                //this.celFreq.Text = string.Format(celFreq.Text, anoBase);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Aluno

                var lstf = (from alu in ctx.TB07_ALUNO
                            join cid in ctx.TB904_CIDADE on alu.TB905_BAIRRO.CO_CIDADE equals cid.CO_CIDADE
                            join uncont in ctx.TB25_EMPRESA on alu.CO_EMP equals uncont.CO_EMP

                            where (alu.CO_ALU == codAlu)
                            select new Aluno
                            {

                                Nome = alu.NO_ALU.ToUpper(),
                                Sexo = alu.CO_SEXO_ALU != null ? alu.CO_SEXO_ALU : "X",
                                NomeMae = alu.NO_MAE_ALU != null ? alu.NO_MAE_ALU.ToUpper() : "-",
                                NomePai = alu.NO_PAI_ALU != null ? alu.NO_PAI_ALU.ToUpper() : "-",
                                Nire = alu.NU_NIRE,
                                Nis = alu.NU_NIS.HasValue ? alu.NU_NIS.Value : 0,
                                DtNasc = alu.DT_NASC_ALU.HasValue ? alu.DT_NASC_ALU : DateTime.MinValue,
                                DtCadastro = alu.DT_CADA_ALU.HasValue ? alu.DT_CADA_ALU.Value : DateTime.MinValue,
                                TelCel = alu.NU_TELE_RESI_ALU,
                                TelResid = alu.NU_TELE_CELU_ALU,
                                Deficiência = alu.TP_DEF != null ? alu.TP_DEF : "-",
                                Apelido = alu.NO_APE_ALU != null ? alu.NO_APE_ALU : "-",
                                Imagem = alu.Image.ImageStream,


                                Endereco = new Endereco()
                                {
                                    Bairro = alu.TB905_BAIRRO.NO_BAIRRO != null ? alu.TB905_BAIRRO.NO_BAIRRO : "-",
                                    CEP = alu.CO_CEP_ALU != null ? alu.CO_CEP_ALU : "-",
                                    Cidade = cid.NO_CIDADE != null ? cid.NO_CIDADE : "-",
                                    Complemento = alu.DE_COMP_ALU != null ? alu.DE_COMP_ALU : "-",
                                    Estado = alu.CO_ESTA_ALU != null ? alu.CO_ESTA_ALU : "-",
                                    Logradouro = alu.DE_ENDE_ALU != null ? alu.DE_ENDE_ALU : "-",
                                    Numero = alu.NU_ENDE_ALU != null ? alu.NU_ENDE_ALU : 0
                                },

                                Documentos = new Documentos()
                                {
                                    Cpf = alu.NU_CPF_ALU,
                                    Rg = alu.CO_RG_ALU != null ? alu.CO_RG_ALU : "-",
                                    Titulo = alu.NU_TIT_ELE != null ? alu.NU_TIT_ELE : "-",
                                    CertidaoNasc = alu.NU_CERT != null ? alu.NU_CERT : "-",
                                },


                            });

                //var s = (lstf as ObjectQuery).ToTraceString();

                #endregion

                var alun = lstf.FirstOrDefault();

                // Se não encontrou o funcionário
                if (alun == null)
                    return -1;

                #region Restrições

                var lstRes = (from rest in ctx.TB294_RESTR_ALIMEN
                              where rest.TB07_ALUNO.CO_ALU == codAlu

                              select new Restricoes
                              {
                                  NomeRestricao = rest.NM_RESTR_ALIMEN != null ? rest.NM_RESTR_ALIMEN : "-",
                                  AcaoRestricao = rest.DE_ACAO_RESTR_ALIMEN != null ? rest.DE_ACAO_RESTR_ALIMEN : "-",
                                  DtInicioRes = rest.DT_INICIO_RESTR_ALIMEN,
                                  DtFimRes = rest.DT_TERMI_RESTR_ALIMEN.HasValue ? rest.DT_TERMI_RESTR_ALIMEN : DateTime.MinValue,
                                  TipoRestricao = rest.TP_RESTR_ALIMEN != null ? rest.TP_RESTR_ALIMEN : "-"
                              }

                ).ToList();
                alun.Restricoes = lstRes;

                #endregion

                #region Cuidados

                var lstCuid = (from cuid in ctx.TB293_CUIDAD_SAUDE
                               where cuid.TB07_ALUNO.CO_ALU == codAlu

                               select new Cuidados
                               {
                                   NomeRemedio = cuid.NM_REMEDIO_CUIDADO != null ? cuid.NM_REMEDIO_CUIDADO : "-",
                                   Observacao = cuid.DE_OBSERV_CUIDADO != null ? cuid.DE_OBSERV_CUIDADO : "-",
                                   DescricaoDose = cuid.DE_DOSE_REMEDIO_CUIDADO != null ? cuid.DE_DOSE_REMEDIO_CUIDADO : 0,
                                   DtInicioCuid = cuid.DT_RECEITA_INI.HasValue ? cuid.DT_RECEITA_INI : DateTime.MinValue,
                                   DtFimCuid = cuid.DT_RECEITA_FIM.HasValue ? cuid.DT_RECEITA_FIM : DateTime.MinValue
                               }

                ).ToList();
                alun.Cuidados = lstCuid;
                #endregion

                bsReport.Clear();
                bsReport.Add(alun);

                return 1;
            }
            catch { return 0; }
        }


        #endregion


        public class Aluno
        {
            public Aluno()
            {

                this.Restricoes = new List<Restricoes>();
                this.Cuidados = new List<Cuidados>();
                this.EnderecoAdicionais = new List<EnderecoAdicionais>();
                this.TelefonesAdicionais = new List<TelefonesAdicionais>();
            }

            public List<Restricoes> Restricoes { get; set; }
            public List<Cuidados> Cuidados { get; set; }
            public List<EnderecoAdicionais> EnderecoAdicionais { get; set; }
            public List<TelefonesAdicionais> TelefonesAdicionais { get; set; }
            public Endereco Endereco { get; set; }
            public Documentos Documentos { get; set; }

            public string Nome { get; set; }
            public string Sexo { get; set; }
            public string DeficienciaDesc
            {
                get
                {
                    if (this.Deficiência != null)
                    {
                        if (this.Deficiência == "N")
                            return "Nenhuma";
                        else if (this.Deficiência == "A")
                            return "Auditiva";
                        else if (this.Deficiência == "V")
                            return "Visual";
                        else if (this.Deficiência == "F")
                            return "Fisica";
                        else if (this.Deficiência == "M")
                            return "Mental";
                        else if (this.Deficiência == "P")
                            return "Múltiplas";
                        else if (this.Deficiência == "O")
                            return "Outras";
                        else
                            return "-";
                    }
                    else
                    {
                        return "-";
                    }
                }

            }

            public string NisDesc
            {
                get
                {
                    if (this.Nis == 0)
                    {
                        return "-";
                    }
                    else
                    {
                        var retorno = this.Nis.ToString();
                        return retorno;

                    }
                }
            }
            public string SexoDesc
            {
                get
                {
                    if (this.Sexo != null)
                    {
                        if (this.Sexo == "M")
                        {
                            return "Mas";
                        }
                        else if (this.Sexo == "F")
                        {
                            return "Fem";
                        }
                        else
                        {
                            return "-";
                        }
                    }
                    else
                    {
                        return "-";
                    }

                }


            }

            public string NomeMae { get; set; }
            public DateTime? DtNasc { get; set; }
            public int Nire { get; set; }
            public string Categoria { get; set; }
            public string Deficiência { get; set; }
            public string Apelido { get; set; }
            public string NomePai { get; set; }
            public decimal Nis { get; set; }
            public string TelResid { get; set; }
            public string TelCel { get; set; }
            public DateTime? DtCadastro { get; set; }
            public string idade
            {
                get
                {
                    if (DtNasc.HasValue)
                    {
                        DateTime now = DateTime.Today;
                        int age = now.Year - DtNasc.Value.Year;
                        if (DtNasc > now.AddYears(-age))
                            age--;
                        return "(" + age + ")";
                    }
                    else
                    {
                        return "(-)";
                    }
                }
            }
            public string NomeMaeDesc
            {
                get
                {
                    if (this.NomeMae.Length > 30)
                    {
                        var NomeRetorno = this.NomeMae.Substring(0, 27) + "...";
                        return NomeRetorno;
                    }
                    else
                    {

                        return this.NomeMae;
                    }
                }
            }
            public string NireDesc
            {
                get
                {
                    string descNire = this.Nire.ToString();
                    if (descNire.Length < 9)
                    {
                        while (descNire.Length < 9)
                        {
                            descNire = "0" + descNire;
                        }
                        return descNire;
                    }
                    else
                    {
                        return descNire;
                    }
                }
            }
            public string NomePaiDesc
            {
                get
                {
                    if (this.NomePai.Length > 30)
                    {
                        var NomeRetorno = this.NomePai.Substring(0, 27) + "...";
                        return NomeRetorno;
                    }
                    else
                    {

                        return this.NomePai;
                    }
                }
            }
            public string TelCelDesc
            {
                get
                {
                    if (this.TelCel != null && this.TelCel.Length == 10)
                    {
                        var ret1 = this.TelCel.Substring(0, 2);
                        var ret2 = this.TelCel.Substring(2, 4);
                        var ret3 = this.TelCel.Substring(6, 4);
                        var retorno = "(" + ret1 + ")" + ret2 + "-" + ret3;
                        return retorno;
                    }
                    else if (this.TelCel != null && this.TelCel.Length < 10)
                    {
                        return this.TelCel;
                    }
                    else
                    {
                        return "-";
                    }
                }
            }

            public string TelResidDesc
            {
                get
                {
                    if (this.TelResid != null && this.TelResid.Length == 10)
                    {
                        var ret1 = this.TelResid.Substring(0, 2);
                        var ret2 = this.TelResid.Substring(2, 4);
                        var ret3 = this.TelResid.Substring(6, 4);
                        var retorno = "(" + ret1 + ")" + " " + ret2 + "-" + ret3;
                        return retorno;
                    }
                    else if (this.TelResid != null && this.TelResid.Length < 10)
                    {
                        return this.TelResid;
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
            public string NomeDesc
            {
                get
                {
                    if (this.Nome.Length > 35)
                    {
                        var NomeRetorno = this.Nome.Substring(0, 32) + "...";
                        return NomeRetorno;
                    }
                    else
                    {

                        return this.Nome;
                    }
                }
            }
            public byte[] Imagem { get; set; }
            public string DataCadastroDesc
            {
                get
                {
                    if (this.DtCadastro == null)
                        return "-";
                    else
                    {
                        return this.DtCadastro.Value.ToString("dd/MM/yyyy");
                    }
                }
            }

            public string DtNascDesc
            {
                get
                {
                    if (this.DtNasc == null)
                    {
                        return "-";
                    }
                    else
                    {
                        var retorno1 = this.DtNasc.Value.ToString("dd/MM/yyyy");
                        var retorno2 = this.idade;
                        return retorno1 + " " + retorno2;

                    }
                }
            }
        }

        public class Endereco
        {
            public string Logradouro { get; set; }
            public decimal? Numero { get; set; }
            public string EnderecoStr
            {
                get { return Logradouro + ((Numero.HasValue) ? ", " + Numero.Value.ToString() : ""); }
            }
            public string Complemento { get; set; }
            public string Bairro { get; set; }
            public string Cidade { get; set; }
            public string Estado { get; set; }
            public string _CEP;
            public string CEP
            {
                get { return this._CEP.Format(TipoFormat.CEP); }
                set { this._CEP = value; }
            }

        }

        public class Documentos
        {
            public string Cpf { get; set; }
            public string Rg { get; set; }
            public string Titulo { get; set; }
            public string CertidaoNasc { get; set; }

            public string CpfDesc
            {
                get
                {
                    if (this.Cpf != null && this.Cpf.Length == 11)
                    {
                        var ret1 = this.Cpf.Substring(0, 3);
                        var ret2 = this.Cpf.Substring(3, 3);
                        var ret3 = this.Cpf.Substring(6, 3);
                        var ret4 = this.Cpf.Substring(9, 2);
                        var retorno = ret1 + "." + ret2 + "." + ret3 + "-" + ret4;
                        return retorno;
                    }
                    else
                    {
                        return "-";
                    }
                }
            }

        }
        public class Restricoes
        {
            public string NomeRestricao { get; set; }
            public string AcaoRestricao { get; set; }
            public string TipoRestricao { get; set; }
            public DateTime? DtInicioRes { get; set; }
            public DateTime? DtFimRes { get; set; }
            public string TipoRestricaoDesc
            {
                get
                {
                    if (this.TipoRestricao == "A")
                        return "Alimentar";
                    else if (this.TipoRestricao == "L")
                        return "Alergia";
                    else if (this.TipoRestricao == "M")
                        return "Médica";
                    else if (this.TipoRestricao == "R")
                        return "Responsável";
                    else
                        return "Outros";
                }
            }

            public string DtInicioResDesc
            {
                get
                {
                    if (this.DtInicioRes == null)
                    {
                        return "-";
                    }
                    else
                    {
                        return this.DtInicioRes.Value.ToString("dd/MM/yyyy");
                    }

                }
            }

            public string DtFimResDesc
            {
                get
                {
                    if (this.DtFimRes == null)
                    {
                        return "-";
                    }
                    else
                    {
                        return this.DtFimRes.Value.ToString("dd/MM/yyyy");
                    }

                }
            }
        }

        public class Cuidados
        {
            public string NomeRemedio { get; set; }
            public string Observacao { get; set; }
            public int? DescricaoDose { get; set; }
            public DateTime? DtInicioCuid { get; set; }
            public DateTime? DtFimCuid { get; set; }

            public string DtInicioCuidDesc
            {
                get
                {
                    if (this.DtInicioCuid == null)
                    {
                        return "-";
                    }
                    else
                    {
                        return this.DtInicioCuid.Value.ToString("dd/MM/yyyy");
                    }

                }
            }
            public string DtFimCuidDesc
            {
                get
                {
                    if (this.DtFimCuid == null)
                    {
                        return "-";
                    }
                    else
                    {
                        return this.DtFimCuid.Value.ToString("dd/MM/yyyy");
                    }

                }
            }
        }

        public class EnderecoAdicionais
        {
            public string estado { get; set; }
            public string descricao { get; set; }
            public decimal? numero { get; set; }
            public string complemento { get; set; }
            public string cidade { get; set; }
            public string bairro { get; set; }
            public string cep { get; set; }
            public string tipoEndereco { get; set; }
            public string situacao { get; set; }
            public string EndDesc
            {
                get
                {
                    var retorno = this.descricao + ", " + this.numero + " - "
                        + this.bairro + " - " + this.cidade + " - " + "CEP " + this.CEPAd;
                    return retorno;
                }
            }
            public string _CEPAd;
            public string CEPAd
            {
                get { return this._CEPAd.Format(TipoFormat.CEP); }
                set { this._CEPAd = value; }
            }
            public string situacaoDesc
            {
                get
                {
                    if (this.situacao == null)
                    {
                        return "-";
                    }
                    else
                    {
                        if (this.situacao == "A")
                        {
                            return "Ativo";
                        }
                        else
                        {
                            return "Inativo";
                        }
                    }
                }
            }
        }

        public class TelefonesAdicionais
        {
            public string NomeCont { get; set; }
            public decimal? NumeroTel { get; set; }
            public decimal? DDD { get; set; }
            public string TipoTel { get; set; }
            public string situacao { get; set; }
            public string NumeroTelDesc
            {
                get
                {
                    var ret1 = this.NumeroTel.ToString();
                    ret1 = ret1.Substring(0, 4);
                    var ret2 = ret1.Substring(4, 4);

                    return "(" + this.DDD + ")" + " " + ret1 + "-" + ret2;
                }
            }

            public string situacaoDesc
            {
                get
                {
                    if (this.situacao == null)
                    {
                        return "-";
                    }
                    else
                    {
                        if (this.situacao == "A")
                        {
                            return "Ativo";
                        }
                        else
                        {
                            return "Inativo";
                        }
                    }
                }
            }
        }
        
    }
}
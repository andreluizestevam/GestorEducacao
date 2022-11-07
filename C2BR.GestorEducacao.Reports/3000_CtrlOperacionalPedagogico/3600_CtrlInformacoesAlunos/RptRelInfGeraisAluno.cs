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
namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos
{
    public partial class RptRelInfGeraisAluno : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptRelInfGeraisAluno()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros, int codEmp, string infos, string strP_CO_EMP, int codAlu, int anoBase, string Situacao)
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
                            into c1 from cid in c1.DefaultIfEmpty()
                            join mat in ctx.TB08_MATRCUR on alu.CO_ALU equals mat.CO_ALU
                            into c2 from mat in c2.DefaultIfEmpty()
                            join uncont in ctx.TB25_EMPRESA on mat.CO_EMP_UNID_CONT equals uncont.CO_EMP
                            into c3 from uncont in c3.DefaultIfEmpty()
                            join cur in ctx.TB01_CURSO on mat.CO_CUR equals cur.CO_CUR
                            into c4 from cur in c4.DefaultIfEmpty()
                            join tur in ctx.TB06_TURMAS on mat.CO_TUR equals tur.CO_TUR
                            into c5 from tur in c5.DefaultIfEmpty()
                            join cadtur in ctx.TB129_CADTURMAS on tur.CO_TUR equals cadtur.CO_TUR
                            into c6 from cadtur in c6.DefaultIfEmpty()
                            where (alu.CO_ALU == codAlu)
                            select new Aluno
                            {

                                Nome = alu.NO_ALU.ToUpper(),
                                Sexo = alu.CO_SEXO_ALU != null ? alu.CO_SEXO_ALU : "X" ,
                                NomeMae = alu.NO_MAE_ALU != null ? alu.NO_MAE_ALU.ToUpper() : "-",
                                NomePai = alu.NO_PAI_ALU != null ? alu.NO_PAI_ALU.ToUpper() : "-",
                                Nire = alu.NU_NIRE,
                                Nis = alu.NU_NIS.HasValue ? alu.NU_NIS.Value : 0  ,
                                DtNasc = alu.DT_NASC_ALU.HasValue ? alu.DT_NASC_ALU: DateTime.MinValue,
                                DtCadastro = alu.DT_CADA_ALU.HasValue ? alu.DT_CADA_ALU.Value : DateTime.MinValue,
                                TelCel = alu.NU_TELE_RESI_ALU ,
                                TelResid = alu.NU_TELE_CELU_ALU ,
                                Deficiência = alu.TP_DEF != null ? alu.TP_DEF : "-" ,
                                Apelido = alu.NO_APE_ALU != null ? alu.NO_APE_ALU : "-",
                                Imagem = alu.Image.ImageStream,
                                

                                Endereco = new Endereco()
                                {
                                    Bairro = alu.TB905_BAIRRO.NO_BAIRRO != null ?  alu.TB905_BAIRRO.NO_BAIRRO : "-",
                                    CEP = alu.CO_CEP_ALU != null ? alu.CO_CEP_ALU : "-",
                                    Cidade = cid.NO_CIDADE != null ? cid.NO_CIDADE : "-" ,
                                    Complemento = alu.DE_COMP_ALU != null ? alu.DE_COMP_ALU : "-",
                                    Estado = alu.CO_ESTA_ALU != null ? alu.CO_ESTA_ALU : "-",
                                    Logradouro = alu.DE_ENDE_ALU != null ? alu.DE_ENDE_ALU : "-",
                                    Numero = alu.NU_ENDE_ALU != null ? alu.NU_ENDE_ALU : 0
                                },

                                Documentos = new Documentos()
                                {
                                    Cpf = alu.NU_CPF_ALU,
                                    Rg = alu.CO_RG_ALU != null ? alu.CO_RG_ALU : "-",
                                    Titulo = alu.NU_TIT_ELE != null ? alu.NU_TIT_ELE : "-" ,
                                    CertidaoNasc = alu.NU_CERT != null ? alu.NU_CERT : "-",
                                },

                                Informacoes = new InformacoesEscolares()
                                {
                                    UndContrato = uncont.sigla != null ? uncont.sigla : "-",
                                    Modalidade = mat.TB44_MODULO.DE_MODU_CUR != null ? mat.TB44_MODULO.DE_MODU_CUR : "-",
                                    Serie = cur.NO_CUR != null ?  cur.NO_CUR : "-"  ,
                                    Turma = cadtur.CO_SIGLA_TURMA != null ?  cadtur.CO_SIGLA_TURMA : "-",
                                    DtMatricula = mat.DT_CAD_MAT,
                                    AnoMat = mat.CO_ANO_MES_MAT 
                                }


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
                                  NomeRestricao = rest.NM_RESTR_ALIMEN != null ?  rest.NM_RESTR_ALIMEN : "-" ,
                                  AcaoRestricao = rest.DE_ACAO_RESTR_ALIMEN != null ? rest.DE_ACAO_RESTR_ALIMEN : "-",
                                  DtInicioRes = rest.DT_INICIO_RESTR_ALIMEN ,
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
                                  DescricaoDose = cuid.DE_DOSE_REMEDIO_CUIDADO != null ?  cuid.DE_DOSE_REMEDIO_CUIDADO : 0,
                                  DtInicioCuid = cuid.DT_RECEITA_INI.HasValue ? cuid.DT_RECEITA_INI: DateTime.MinValue ,
                                  DtFimCuid = cuid.DT_RECEITA_FIM.HasValue ? cuid.DT_RECEITA_FIM : DateTime.MinValue
                               }

                ).ToList();
                alun.Cuidados = lstCuid;

                #endregion

                #region Enderecos Adicionais

                var lstEndAd = (from endad in ctx.TB241_ALUNO_ENDERECO
                                join cid in ctx.TB904_CIDADE on endad.TB905_BAIRRO.CO_CIDADE equals cid.CO_CIDADE
                                where endad.TB07_ALUNO.CO_ALU == codAlu

                               select new EnderecoAdicionais
                               {
                                  bairro = endad.TB905_BAIRRO.NO_BAIRRO != null ? endad.TB905_BAIRRO.NO_BAIRRO : "-",
                                  cidade = cid.NO_CIDADE != null ? cid.NO_CIDADE : "-",
                                  CEPAd = endad.CO_CEP != null ? endad.CO_CEP : "-" ,
                                  complemento = endad.DS_COMPLEMENTO != null ? endad.DS_COMPLEMENTO : "-" ,
                                  descricao = endad.DS_ENDERECO != null ?  endad.DS_ENDERECO : "-" ,
                                  numero = endad.NR_ENDERECO != null ?  endad.NR_ENDERECO : 0,
                                  tipoEndereco = endad.TB238_TIPO_ENDERECO.NM_TIPO_ENDERECO != null ? endad.TB238_TIPO_ENDERECO.NM_TIPO_ENDERECO : "-",
                                  situacao = endad.CO_SITUACAO
                               }

                ).ToList();
                alun.EnderecoAdicionais = lstEndAd;

                #endregion

                #region Telefones Adicionais

                var lstTelAd = (from telad in ctx.TB242_ALUNO_TELEFONE
                                 where telad.TB07_ALUNO.CO_ALU == codAlu

                               select new TelefonesAdicionais
                               {
                                  DDD = telad.NR_DDD != null ? telad.NR_DDD : 000,
                                  NumeroTel = telad.NR_TELEFONE != null ? telad.NR_TELEFONE : 00000000,
                                  NomeCont = telad.NO_CONTATO != null ? telad.NO_CONTATO : "-",
                                  TipoTel = telad.TB239_TIPO_TELEFONE.NM_TIPO_TELEFONE != null ? telad.TB239_TIPO_TELEFONE.NM_TIPO_TELEFONE : "-",
                                  situacao = telad.CO_SITUACAO
                               }

                ).ToList();
                alun.TelefonesAdicionais = lstTelAd;

                #endregion

                #region Atividades Extras

                var lstAtvExt = (from atvext in ctx.TB106_ATIVEXTRA_ALUNO
                                 where atvext.TB07_ALUNO.CO_ALU == codAlu

                               select new AtividadesExtras
                               {
                                 Descricao = atvext.TB105_ATIVIDADES_EXTRAS.DES_ATIV_EXTRA != null ? atvext.TB105_ATIVIDADES_EXTRAS.DES_ATIV_EXTRA  : "-" ,
                                 DtInicioAtiv = atvext.DT_INI_ATIV,
                                 qtdmes = atvext.QT_MES_ATIV != null ? atvext.QT_MES_ATIV  : 0,
                                 Valor = atvext.VL_ATIV_EXTRA != null ? atvext.VL_ATIV_EXTRA  : 0,
                                 DtFimAtiv = atvext.DT_VENC_ATIV
                               }

                ).ToList();
                alun.AtividadesExtras = lstAtvExt;

                #endregion

                #region Entrega Documentos

                var lstDocEnt = (from entdoc in ctx.TB120_DOC_ALUNO_ENT
                                 where entdoc.TB07_ALUNO.CO_ALU == codAlu
                               select new EntregaDocumentos
                               {
                                 
                                   TipoDoc = entdoc.TB121_TIPO_DOC_MATRICULA.DE_TP_DOC_MAT != null ? entdoc.TB121_TIPO_DOC_MATRICULA.DE_TP_DOC_MAT : "-"
                               }

                ).ToList();
                alun.EntregaDocumetos = lstDocEnt;

                #endregion

                #region Pendencias Financeiras

                var lstPendFin = (from pend in ctx.TB47_CTA_RECEB
                                  where pend.CO_ALU == codAlu && (pend.IC_SIT_DOC != "Q") 
                                        && pend.DT_VEN_DOC < DateTime.Now
                                  select new PendenciasFinanceiras
                                  {
                                    NumeroDoc = pend.NU_DOC != null ? pend.NU_DOC : "-",
                                    DtCadastro = pend.DT_CAD_DOC,
                                    DtVencimento = pend.DT_VEN_DOC,
                                    ValorTotal = pend.VR_TOT_DOC != null ?  pend.VR_TOT_DOC  : 000,
                                    Parcela = pend.VR_PAR_DOC,
                                    Tipo = pend.TB086_TIPO_DOC.DES_TIPO_DOC,
                                    Historico = pend.TB39_HISTORICO.DE_HISTORICO,
                                    Responsavel = pend.TB108_RESPONSAVEL.NO_RESP
                                  }

                ).ToList();
                alun.PendenciasFinanceiras = lstPendFin;

                #endregion

                #region Ocorrencias Disciplinares

                var lstOcor = (from ocor in ctx.TB191_OCORR_ALUNO
                                where ocor.ID_RECEB_OCORR == codAlu
                                && ocor.CO_CATEG == "A"

                               select new OcorrenciasDisciplinares
                               {
                                   DescOcorr = ocor.DE_OCORR ,
                                   DtOcorr = ocor.DT_OCORR,
                                   TipoOcorr = ocor.TB150_TIPO_OCORR.DE_TIPO_OCORR 
                               }

                ).ToList();
                alun.OcorrenciasDisciplinares = lstOcor;

                #endregion

                #region Frequencia do Aluno

                var lstFreq = (from m in ctx.TB132_FREQ_ALU
                               where m.TB07_ALUNO.CO_ALU == codAlu
                               && m.DT_FRE.Year == anoBase 
                               
                               select new
                               {
                                   Data = m.DT_FRE,
                                   Flag = m.CO_FLAG_FREQ_ALUNO
                               }).DistinctBy(x => x.Data).ToList();

                var lstMes = lstFreq.GroupBy(x => x.Data.Month).OrderBy(x => x.Key);

                List<FrequenciaMensal> lstF = new List<FrequenciaMensal>();

                for (int mes = 1; mes < 13; mes++)
                {
                    if (lstMes.Any(x => x.Key == mes))
                    {
                        var li = lstMes.First(x => x.Key == mes);
                        FrequenciaMensal f = new FrequenciaMensal();
                        int tDias = DateTime.DaysInMonth(anoBase, li.Key);

                        f.Mes = Funcoes.GetMes(li.Key);
                        f.TotalFaltas = li.Count(x => x.Flag == "N");
                        f.TotalPresenca = li.Count(x => x.Flag == "S");
                        f.PercentualPresenca = ((decimal)f.TotalPresenca / (decimal)tDias);
                        f.PercentualFaltas = ((decimal)f.TotalFaltas / (decimal)tDias);
                        lstF.Add(f);
                    }
                    else
                    {
                        FrequenciaMensal fNew = new FrequenciaMensal();
                        fNew.Mes = Funcoes.GetMes(mes);
                        lstF.Add(fNew);
                    }
                }

                alun.Frequencias = lstF;

                #endregion

               /* #region Frequencia do Aluno

                var lstFreq = (from a in ctx.TB132_FREQ_ALU
                               where a.TB07_ALUNO.CO_ALU == codAlu
                               && a.DT_FRE.Year == anoBase
                               && a.TP_F == "E" && a.CO_SITU_FRE == "A"
                               select new
                               {
                                   Data = a.DT_FRE,
                                   Flag = a.FLA_PR
                               }).DistinctBy(x => x.Data).ToList();

                var lstMes = lstFreq.GroupBy(x => x.Data.Month).OrderBy(x => x.Key);

                List<FrequenciaMensal> lstF = new List<FrequenciaMensal>();

                for (int mes = 1; mes < 13; mes++)
                {
                    if (lstMes.Any(x => x.Key == mes))
                    {
                        var li = lstMes.First(x => x.Key == mes);
                        FrequenciaMensal f = new FrequenciaMensal();
                        int tDias = DateTime.DaysInMonth(anoBase, li.Key);

                        f.Mes = Funcoes.GetMes(li.Key);
                        f.TotalFaltas = li.Count(x => x.Flag == "N");
                        f.TotalPresenca = li.Count(x => x.Flag == "S");
                        f.PercentualPresenca = ((decimal)f.TotalPresenca / (decimal)tDias);
                        f.PercentualFaltas = ((decimal)f.TotalFaltas / (decimal)tDias);
                        lstF.Add(f);
                    }
                    else
                    {
                        FrequenciaMensal fNew = new FrequenciaMensal();
                        fNew.Mes = Funcoes.GetMes(mes);
                        lstF.Add(fNew);
                    }
                }

                func.Frequencias = lstF;

                #endregion*/
                // Adiciona o Funcionario ao DataSource do Relatório
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
                this.AtividadesExtras = new List<AtividadesExtras>();
                this.EntregaDocumetos = new List<EntregaDocumentos>();
                this.PendenciasFinanceiras = new List<PendenciasFinanceiras>();
                this.OcorrenciasDisciplinares = new List<OcorrenciasDisciplinares>();
            }
            public List<OcorrenciasDisciplinares> OcorrenciasDisciplinares { get; set; }
            public List<Restricoes> Restricoes { get; set; }
            public List<Cuidados> Cuidados { get; set; }
            public List<EnderecoAdicionais> EnderecoAdicionais { get; set; }
            public List<TelefonesAdicionais> TelefonesAdicionais { get; set; }
            public List<AtividadesExtras> AtividadesExtras { get; set; }
            public List<EntregaDocumentos> EntregaDocumetos { get; set; }
            public List<PendenciasFinanceiras> PendenciasFinanceiras { get; set; }
            public List<FrequenciaMensal> Frequencias { get; set; }
            public Endereco Endereco { get; set; }
            public Documentos Documentos { get; set; }
            public InformacoesEscolares Informacoes { get; set; }
            public string Nome { get; set; }
            public string Sexo { get; set; }
            public string DeficienciaDesc
            {
                get
                {
                    if(this.Deficiência != null)
                    {
                        if(this.Deficiência == "N")
                            return "Nenhuma";
                        else if(this.Deficiência == "A")
                            return "Auditiva";
                        else if(this.Deficiência == "V")
                            return "Visual";
                        else if(this.Deficiência == "F")
                            return "Fisica";
                        else if(this.Deficiência == "M")
                            return "Mental";
                        else if(this.Deficiência == "P")
                            return "Múltiplas";
                        else if(this.Deficiência == "O")
                            return "Outras";
                        else 
                            return "-";
                    }else{
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
                        return "("+age+")";
                    }
                    else {
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
                        var retorno = "("+ret1+")" + " " +ret2 + "-" + ret3;
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
                        return retorno1 +" " +retorno2;
                        
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

        public class InformacoesEscolares
        {
            public string UndContrato { get; set; }
            public string AnoMat { get; set; }
            public string Categoria
            {
                get
                {
                    if (this.Turma != null && this.Turma != "-")
                    {
                        return this.AnoMat + " - Matriculado";
                    }
                    else 
                    {
                        return "Não Matriculado";
                    }
                }
            }
            public string Modalidade { get; set; }
            public string Serie { get; set; }
            public string Turma { get; set; }
            public DateTime? DtMatricula { get; set; }
            public string TelCelular { get; set; }
            public string InfDesc {
                get
                {
                    
                    var retorno = this.Modalidade + " - " + this.Serie + " - " + this.Turma;
                    return retorno;
                }
            }
            public string DtMatriculaDesc
            {
                get
                {
                    if (this.DtMatricula == null)
                    {
                        return "-";
                    }
                    else
                    {
                        return this.DtMatricula.Value.ToString("dd/MM/yyyy");
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

        public class AtividadesExtras
        {
            public string Descricao { get; set; }
            public decimal? Valor { get; set; }
            public int? qtdmes { get; set; }
            public DateTime? DtInicioAtiv { get; set; }
            public DateTime? DtFimAtiv { get; set; }
            public string DtFimAtivDesc
            {
                get
                {
                    if (this.DtFimAtiv == null)
                    {
                        return "-";
                    }
                    else
                    {
                        return this.DtFimAtiv.Value.ToString("dd/MM/yyyy");
                    }

                }
            }

            public string DtInicioAtivDesc
            {
                get
                {
                    if (this.DtInicioAtiv == null)
                    {
                        return "-";
                    }
                    else
                    {
                        return this.DtInicioAtiv.Value.ToString("dd/MM/yyyy");
                    }

                }
            }

        }
        
        public class EntregaDocumentos
        {
            public string TipoDoc { get; set; }
        }
        
        public class OcorrenciasDisciplinares
        {
            public string DescOcorr { get; set; }
            public string TipoOcorr { get; set; }
            public DateTime DtOcorr { get; set; }
            public string DtOcorrDesc
            {
                get
                {
                    if (DtOcorr == null)
                    {
                        return "-";
                    }
                    else
                    {
                        return this.DtOcorr.ToString("dd/MM/yyyy");
                    }

                }
            }

        }

        public class FrequenciaMensal
        {
            public string Mes { get; set; }
            public int TotalPresenca { get; set; }
            public int TotalFaltas { get; set; }
            public int FaltasJustificadas { get; set; }
            public int FaltasNaoJustificadas { get; set; }
            public int LicencaMedica { get; set; }
            public decimal PercentualFaltas { get; set; }
            public decimal PercentualPresenca { get; set; }
        }

        public class PendenciasFinanceiras
        {
            public string NumeroDoc { get; set; }
            public DateTime? DtCadastro { get; set; }
            public DateTime? DtVencimento { get; set; }
            public string Situacao { get; set; }
            public decimal ValorTotal { get; set; }
            public decimal Parcela{get; set; }
            public string Tipo{get;set;}
            public string Historico{get;set;}
            public string Responsavel{get;set;}
            public string DtCadastroDesc
            {
                get
                {
                    if (this.DtCadastro == null)
                    {
                        return "-";
                    }
                    else
                    {
                        return this.DtCadastro.Value.ToString("dd/MM/yyyy");
                    }

                }
            }
            public string DtVencimentoDesc
            {
                get
                {
                    if (this.DtVencimento == null)
                    {
                        return "-";
                    }
                    else
                    {
                        return this.DtVencimento.Value.ToString("dd/MM/yyyy");
                    }

                }
            }



        }

        private void Detail2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DetailReportBand Detail = (DetailReportBand)sender;
             if (Detail.RowCount == 0)
             {
                 this.xrLabel5.Text = " ***Sem Registro";
             }
             else
             {
                 this.xrLabel5.Text = string.Empty;
             }
         
        }

        private void Detail3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DetailReportBand Detail = (DetailReportBand)sender;
             if (Detail.RowCount == 0)
             {
                 this.xrLabel6.Text = " ***Sem Registro";
             }
             else
             {
                 this.xrLabel6.Text = string.Empty;
             }
         
        }

        private void Detail4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DetailReportBand Detail = (DetailReportBand)sender;
            if (Detail.RowCount == 0)
            {
                this.xrLabel7.Text = " ***Sem Registro";
            }
            else
            {
                this.xrLabel7.Text = string.Empty;
            }

        }

        private void Detail5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DetailReportBand Detail = (DetailReportBand)sender;
            if (Detail.RowCount == 0)
            {
                this.xrLabel8.Text = " ***Sem Registro";
            }
            else
            {
                this.xrLabel8.Text = string.Empty;
            }

        }

        private void Detail6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DetailReportBand Detail = (DetailReportBand)sender;
            if (Detail.RowCount == 0)
            {
                this.xrLabel9.Text = " ***Sem Registro";
            }
            else
            {
                this.xrLabel9.Text = string.Empty;
            }

        }

        private void Detail0_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DetailReportBand Detail = (DetailReportBand)sender;
            if (Detail.RowCount < 1)
            {
                this.xrLabel100.Text = " ***Sem Registro";
            }
            else
            {
                this.xrLabel100.Text = string.Empty;
            }

        }

        private void Detail8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DetailReportBand Detail = (DetailReportBand)sender;
            if (Detail.RowCount == 0)
            {
                this.xrLabel11.Text = " ***Sem Registro";
            }
            else
            {
                this.xrLabel11.Text = string.Empty;
            }

        }

        private void Detail9_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DetailReportBand Detail = (DetailReportBand)sender;
            if (Detail.RowCount == 0)
            {
                this.xrLabel12.Text = " ***Sem Registro";
            }
            else
            {
                this.xrLabel12.Text = string.Empty;
            }

        }

        private void DetailReport1_DataSourceDemanded(object sender, EventArgs e)
        {

        }
     
    }

   
}

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
namespace C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar
{
    public partial class RptFichaMatricAluno : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptFichaMatricAluno()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros, int codEmp, string infos, string strP_CO_EMP, int codAlu, int anoBase, string Situacao, string codAlunoCad = "")
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
                            where ((codAlunoCad != "" && codAlu == 0 && Situacao == "Remat") ? mat.CO_ALU_CAD == codAlunoCad : alu.CO_ALU == codAlu)
                            select new Aluno
                            {
                                // DADOS DO ALUNO
                                nome = alu.NO_ALU,
                                sexo = alu.CO_SEXO_ALU != null ? alu.CO_SEXO_ALU == "M" ? "Mas" : "Fem" : "***",
                                dataNasc = alu.DT_NASC_ALU,
                                nire = alu.NU_NIRE,
                                categoria = mat.CO_ANO_MES_MAT != null ? mat.CO_ANO_MES_MAT : "***",
                                deficiencia = alu.DES_DEF != null ? alu.DES_DEF : "***",

                                // DADOS CADASTRAIS
                                apelido = alu.NO_APE_ALU != null ? alu.NO_APE_ALU : "***",
                                mae = alu.NO_MAE_ALU != null ? alu.NO_MAE_ALU : "***",
                                pai = alu.NO_PAI_ALU != null ? alu.NO_PAI_ALU : "***",
                                nis = alu.NU_NIS,
                                nacionalidade = alu.DE_NACI_ALU != null ? alu.DE_NACI_ALU : "***",
                                naturalidade = alu.DE_NATU_ALU != null ? alu.DE_NATU_ALU : "***",
                                corRaca = alu.TP_RACA != null ? alu.TP_RACA : "***",
                                email = alu.NO_ENDE_ELET_ALU != null ? alu.NO_ENDE_ELET_ALU : "***",
                                telRes = alu.NU_TELE_RESI_ALU != null ? alu.NU_TELE_RESI_ALU : "***",
                                telCel = alu.NU_TELE_CELU_ALU != null ? alu.NU_TELE_CELU_ALU : "***",
                                tipoSangue = alu.CO_TIPO_SANGUE_ALU != null ? alu.CO_TIPO_SANGUE_ALU : "***",
                                altura = "***",
                                peso = "***",
                                edFisica = "***",

                                // INFORMACOES DA MATRICULA
                                unidContrCodigo = mat.CO_EMP_UNID_CONT,
                                modalidade = mat.TB44_MODULO != null ? mat.TB44_MODULO.DE_MODU_CUR : "***",
                                serie = cur.NO_CUR != null ? cur.NO_CUR : "***",
                                turma = cadtur.NO_TURMA != null ? cadtur.NO_TURMA : "***",
                                turno = tur.CO_PERI_TUR != null ? tur.CO_PERI_TUR == "M" ? "Matutino" : tur.CO_PERI_TUR == "V" ? "Vespertino" : "Noturno" : "***",
                                data = cadtur.DT_STATUS_TURMA,

                                // DOCUMENTOS
                                cpf = alu.NU_CPF_ALU != null ? alu.NU_CPF_ALU : "***",
                                rg = alu.CO_RG_ALU != null ? alu.CO_RG_ALU : "***",
                                tituloEleitos = alu.NU_TIT_ELE != null ? alu.NU_TIT_ELE : "***",
                                certNasc = alu.NU_CERT != null ? alu.NU_CERT : "***",
                                cartaoSaude = alu.NU_CARTAO_SAUDE_ALU,
                                planoSaude = "***",

                                // INFORMACOES - RESPONSAVEL FINANCEIRO
                                nomeResp = alu.TB108_RESPONSAVEL != null ? alu.TB108_RESPONSAVEL.NO_RESP : "***",
                                dtNascResp = alu.TB108_RESPONSAVEL != null ? alu.TB108_RESPONSAVEL.DT_NASC_RESP : null,
                                sexpResp = alu.TB108_RESPONSAVEL != null ? alu.TB108_RESPONSAVEL.CO_SEXO_RESP == "M" ? "Mas" : "Fem" : "***",
                                instrucao = alu.TB108_RESPONSAVEL != null ? alu.TB108_RESPONSAVEL.CO_STATUS_GRAU_INSTR_RESP : "***",
                                endeResp = alu.TB108_RESPONSAVEL.DE_ENDE_RESP != null ? alu.TB108_RESPONSAVEL.DE_ENDE_RESP : "***",
                                telResResp = alu.TB108_RESPONSAVEL.NU_TELE_RESI_RESP != null ? alu.TB108_RESPONSAVEL.NU_TELE_RESI_RESP : "***",
                                telConResp = alu.TB108_RESPONSAVEL.NU_TELE_CELU_RESP != null ? alu.TB108_RESPONSAVEL.NU_TELE_CELU_RESP : "***",
                                emailResp = alu.TB108_RESPONSAVEL.DES_EMAIL_RESP != null ? alu.TB108_RESPONSAVEL.DES_EMAIL_RESP : "***",
                                cpfResp = alu.TB108_RESPONSAVEL.NU_CPF_RESP != null ? alu.TB108_RESPONSAVEL.NU_CPF_RESP : "***",
                                rgResp = alu.TB108_RESPONSAVEL.CO_RG_RESP != null ? alu.TB108_RESPONSAVEL.CO_RG_RESP : "***",
                                profissao = alu.TB108_RESPONSAVEL.NO_FUNCAO_RESP != null ? alu.TB108_RESPONSAVEL.NO_FUNCAO_RESP : "***",
                                empResp = alu.TB108_RESPONSAVEL.NO_EMPR_RESP != null ? alu.TB108_RESPONSAVEL.NO_EMPR_RESP : "***",
                                telComResp = alu.TB108_RESPONSAVEL.NU_TELE_COME_RESP != null ? alu.TB108_RESPONSAVEL.NU_TELE_COME_RESP : "***",
                                emailRespEmp = alu.TB108_RESPONSAVEL.DES_EMAIL_EMP != null ? alu.TB108_RESPONSAVEL.DES_EMAIL_EMP : "***"
                            });

                //var s = (lstf as ObjectQuery).ToTraceString();

                #endregion

                var alun = lstf.FirstOrDefault();

                // Se não encontrou o funcionário
                if (alun == null)
                    return -1;
              
                #region Restrições Alimentares

                var lstRes = (from rest in ctx.TB294_RESTR_ALIMEN
                               where rest.TB07_ALUNO.CO_ALU == codAlu

                               select new RestricoesAlimentares
                               {
                                  descricao = rest.NM_RESTR_ALIMEN != null ?  rest.NM_RESTR_ALIMEN : "***" ,
                                  acao = rest.DE_ACAO_RESTR_ALIMEN != null ? rest.DE_ACAO_RESTR_ALIMEN : "***",
                                  dataIni = rest.DT_INICIO_RESTR_ALIMEN ,
                                  dataFim = rest.DT_TERMI_RESTR_ALIMEN,
                                  tipo = rest.TP_RESTR_ALIMEN != null ? rest.TP_RESTR_ALIMEN : "***"
                               }

                ).ToList();
                alun.restricoesAlimentares = lstRes;

                #endregion

                #region Cuidados de Saude

                var lstCuid = (from cuid in ctx.TB293_CUIDAD_SAUDE
                               where cuid.TB07_ALUNO.CO_ALU == codAlu

                               select new CuidadosSaude
                               {
                                  remedio = cuid.NM_REMEDIO_CUIDADO != null ? cuid.NM_REMEDIO_CUIDADO : "-",
                                  observacao = cuid.DE_OBSERV_CUIDADO != null ? cuid.DE_OBSERV_CUIDADO : "-",
                                  dose = cuid.DE_DOSE_REMEDIO_CUIDADO ?? 0,
                                  dataIni = cuid.DT_RECEITA_INI.HasValue ? cuid.DT_RECEITA_INI: DateTime.MinValue ,
                                  dataFim = cuid.DT_RECEITA_FIM.HasValue ? cuid.DT_RECEITA_FIM: DateTime.MinValue
                               }

                ).ToList();
                alun.cuidadoSaude = lstCuid;

                #endregion

                #region Enderecos

                var lstEndAd = (from endad in ctx.TB241_ALUNO_ENDERECO
                                join cid in ctx.TB904_CIDADE on endad.TB905_BAIRRO.CO_CIDADE equals cid.CO_CIDADE
                                where endad.TB07_ALUNO.CO_ALU == codAlu

                               select new Enderecos
                               {
                                  descricao = endad.DS_ENDERECO != null ?  endad.DS_ENDERECO : "-" ,
                                  tipo = endad.TB238_TIPO_ENDERECO.NM_TIPO_ENDERECO != null ? endad.TB238_TIPO_ENDERECO.NM_TIPO_ENDERECO : "-",
                                  situacao = endad.CO_SITUACAO
                               }

                ).ToList();
                alun.enderecos = lstEndAd;

                #endregion

                #region Telefones

                var lstTelAd = (from telad in ctx.TB242_ALUNO_TELEFONE
                                 where telad.TB07_ALUNO.CO_ALU == codAlu

                               select new Telefones
                               {
                                  telefone = telad.NR_TELEFONE != null ? telad.NR_TELEFONE : 00000000,
                                  contato = telad.NO_CONTATO != null ? telad.NO_CONTATO : "-",
                                  tipo = telad.TB239_TIPO_TELEFONE.NM_TIPO_TELEFONE != null ? telad.TB239_TIPO_TELEFONE.NM_TIPO_TELEFONE : "-",
                                  situacao = telad.CO_SITUACAO,
                                  observacao = telad.DES_OBSERVACAO
                               }

                ).ToList();
                alun.telefones = lstTelAd;

                #endregion

                #region Atividades Extras

                var lstAtvExt = (from atvext in ctx.TB106_ATIVEXTRA_ALUNO
                                 where atvext.TB07_ALUNO.CO_ALU == codAlu

                               select new AtividadesExtras
                               {
                                 descricao = atvext.TB105_ATIVIDADES_EXTRAS.DES_ATIV_EXTRA != null ? atvext.TB105_ATIVIDADES_EXTRAS.DES_ATIV_EXTRA  : "-" ,
                                 dataIni = atvext.DT_INI_ATIV,
                                 qtdMes = atvext.QT_MES_ATIV ?? 0,
                                 valor = atvext.VL_ATIV_EXTRA ?? 0,
                                 dataFim = atvext.DT_VENC_ATIV
                               }

                ).ToList();
                alun.atividadesExtras = lstAtvExt;

                #endregion

                #region Documentos de matricula

                var lstDocEnt = (from entdoc in ctx.TB120_DOC_ALUNO_ENT
                                 where entdoc.TB07_ALUNO.CO_ALU == codAlu
                               select new DocumentosMatricula
                               {
                                 
                                   descricao = entdoc.TB121_TIPO_DOC_MATRICULA.DE_TP_DOC_MAT != null ? entdoc.TB121_TIPO_DOC_MATRICULA.DE_TP_DOC_MAT : "***"
                               }

                ).ToList();
                alun.documentosMatricula = lstDocEnt;

                #endregion

                #region Informacoes Financeiras

                var lstPendFin = (from pend in ctx.TB47_CTA_RECEB
                                  where pend.CO_ALU == codAlu && (pend.IC_SIT_DOC != "Q") 
                                        && pend.DT_VEN_DOC < DateTime.Now
                                  select new InformacoesFinanceiras
                                  {
                                    numero = pend.NU_DOC != null ? pend.NU_DOC : "-",
                                    vencimento = pend.DT_VEN_DOC,
                                    parcela = pend.VR_PAR_DOC,
                                    descBolsa = pend.VL_DES_BOLSA_ALUNO,
                                    descEspec = "***",
                                    historico = pend.TB39_HISTORICO.DE_HISTORICO
                                  }

                ).ToList();
                alun.informacoesFinanceiras = lstPendFin;

                #endregion

                #endregion

                bsReport.Clear();
                bsReport.Add(alun);

                return 1;
            }
            catch { return 0; }
        }

        public class Aluno
        {
            public Aluno()
            {
                this.enderecos = new List<Enderecos>();
                this.telefones = new List<Telefones>();
                this.cuidadoSaude = new List<CuidadosSaude>();
                this.restricoesAlimentares = new List<RestricoesAlimentares>();
                this.atividadesExtras = new List<AtividadesExtras>();
                this.documentosMatricula = new List<DocumentosMatricula>();
                this.informacoesFinanceiras = new List<InformacoesFinanceiras>();
            }

            #region LISTAS DE INFORMACOES
            public List<Enderecos> enderecos { get; set; }
            public List<Telefones> telefones { get; set; }
            public List<CuidadosSaude> cuidadoSaude { get; set; }
            public List<RestricoesAlimentares> restricoesAlimentares { get; set; }
            public List<AtividadesExtras> atividadesExtras { get; set; }
            public List<DocumentosMatricula> documentosMatricula { get; set; }
            public List<InformacoesFinanceiras> informacoesFinanceiras { get; set; }
            #endregion

            #region INFORMACOES DO ALUNO
            public string nome { get; set; }
            public string sexo { get; set; }
            public DateTime? dataNasc { get; set; }
            public int nire { get; set; }
            public string categoria { get; set; }
            public string deficiencia { get; set; }
            #endregion

            #region DADOS CADASTRAIS
            public string apelido { get; set; }
            public string mae { get; set; }
            public string pai { get; set; }
            public decimal? nis { get; set; }
            public string nacionalidade { get; set; }
            public string naturalidade { get; set; }
            public string corRaca { get; set; }
            public string email { get; set; }
            public string telRes { get; set; }
            public string telCel { get; set; }
            public string dataCad { get; set; }
            public string tipoSangue { get; set; }
            public string altura { get; set; }
            public string peso { get; set; }
            public string edFisica { get; set; }
            #endregion

            #region INFORMACOES DE MATRICULA
            public int unidContrCodigo { get; set; }
            public string unidContr {
                get 
                {
                    return TB25_EMPRESA.RetornaPelaChavePrimaria(this.unidContrCodigo).NO_FANTAS_EMP;
                } 
            }
            
            public string modalidade { get; set; }
            public string serie { get; set; }
            public string turma { get; set; }
            public string turno { get; set; }
            public DateTime? data { get; set; }
            #endregion

            #region DOCUMENTOS
            public string cpf { get; set; }
            public string rg { get; set; }
            public string tituloEleitos { get; set; }
            public string certNasc { get; set; }
            public decimal? cartaoSaude { get; set; }
            public string planoSaude { get; set; }
            #endregion

            #region INFORMACOES DO RESPONSAVEL FINANCEIRO
            public string nomeResp { get; set; }
            public string nascResp { get; set; }
            public DateTime? dtNascResp { get; set; }
            public string sexpResp { get; set; }
            public string instrucao { get; set; }
            public string endeResp { get; set; }
            public string telResResp { get; set; }
            public string telConResp { get; set; }
            public string telComResp { get; set; }
            public string emailResp { get; set; }
            public string cpfResp { get; set; }
            public string rgResp { get; set; }
            public string profissao { get; set; }
            public string empResp { get; set; }
            public string temEmpResp { get; set; }
            public string emailRespEmp { get; set; }
            #endregion
        }

        public class Enderecos
        {
            public string descricao { get; set; }
            public string tipo { get; set; }
            public string situacao { get; set; }
        }

        public class Telefones
        {
            public string tipo { get; set; }
            public decimal telefone { get; set; }
            public string contato { get; set; }
            public string situacao { get; set; }
            public string observacao { get; set; }
        }

        public class CuidadosSaude
        {
            public string remedio { get; set; }
            public int dose { get; set; }
            public DateTime? dataIni { get; set; }
            public DateTime? dataFim { get; set; }
            public string observacao { get; set; }
        }

        public class RestricoesAlimentares
        {
            public string descricao { get; set; }
            public string acao { get; set; }
            public string tipo { get; set; }
            public DateTime? dataIni { get; set; }
            public DateTime? dataFim { get; set; }
        }

        public class AtividadesExtras
        {
            public string descricao { get; set; }
            public DateTime? dataIni { get; set; }
            public DateTime? dataFim { get; set; }
            public decimal qtdMes { get; set; }
            public decimal valor { get; set; }
        }

        public class DocumentosMatricula
        {
            public string descricao { get; set; }
        }

        public class InformacoesFinanceiras
        {
            public decimal parcela { get; set; }
            public string numero { get; set; }
            public DateTime vencimento { get; set; }
            public decimal? descBolsa { get; set; }
            public string descEspec { get; set; }
            public string historico { get; set; }
        }

        private void Detail2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //DetailReportBand Detail = (DetailReportBand)sender;
            // if (Detail.RowCount == 0)
            // {
            //     this.xrLabel5.Text = " ***Sem Registro";
            // }
            // else
            // {
            //     this.xrLabel5.Text = string.Empty;
            // }
         
        }

        private void Detail3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //DetailReportBand Detail = (DetailReportBand)sender;
            // if (Detail.RowCount == 0)
            // {
            //     this.xrLabel6.Text = " ***Sem Registro";
            // }
            // else
            // {
            //     this.xrLabel6.Text = string.Empty;
            // }
         
        }

        private void Detail4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //DetailReportBand Detail = (DetailReportBand)sender;
            //if (Detail.RowCount == 0)
            //{
            //    this.xrLabel7.Text = " ***Sem Registro";
            //}
            //else
            //{
            //    this.xrLabel7.Text = string.Empty;
            //}

        }

        private void Detail5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //DetailReportBand Detail = (DetailReportBand)sender;
            //if (Detail.RowCount == 0)
            //{
            //    this.xrLabel8.Text = " ***Sem Registro";
            //}
            //else
            //{
            //    this.xrLabel8.Text = string.Empty;
            //}

        }

        private void Detail6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //DetailReportBand Detail = (DetailReportBand)sender;
            //if (Detail.RowCount == 0)
            //{
            //    this.xrLabel9.Text = " ***Sem Registro";
            //}
            //else
            //{
            //    this.xrLabel9.Text = string.Empty;
            //}

        }

        private void Detail0_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //DetailReportBand Detail = (DetailReportBand)sender;
            //if (Detail.RowCount < 1)
            //{
            //    this.xrLabel100.Text = " ***Sem Registro";
            //}
            //else
            //{
            //    this.xrLabel100.Text = string.Empty;
            //}

        }

        private void Detail8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //DetailReportBand Detail = (DetailReportBand)sender;
            //if (Detail.RowCount == 0)
            //{
            //    this.xrLabel11.Text = " ***Sem Registro";
            //}
            //else
            //{
            //    this.xrLabel11.Text = string.Empty;
            //}

        }

        private void Detail9_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //DetailReportBand Detail = (DetailReportBand)sender;
            //if (Detail.RowCount == 0)
            //{
            //    this.xrLabel12.Text = " ***Sem Registro";
            //}
            //else
            //{
            //    this.xrLabel12.Text = string.Empty;
            //}

        }

        private void DetailReport1_DataSourceDemanded(object sender, EventArgs e)
        {

        }
     
    }
}

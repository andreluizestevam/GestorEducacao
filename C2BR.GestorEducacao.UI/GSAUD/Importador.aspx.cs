using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Configuration;
using System.Data.SqlClient;

namespace C2BR.GestorEducacao.UI.GSAUD
{
    public partial class Importador : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            string sqlConnectionString = ConfigurationManager.AppSettings.Get("BancoOrthoLife");
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
            SqlCommand cmd = new SqlCommand();
            
            //SCRIPT para inserir uma coluna q tenha um referencial a cada linha importada
            //ALTER TABLE TBS47_CTA_RECEB ADD SEQ_IMP INT NULL;
            cmd.CommandText = @"SELECT
                                    SEQ_IMP
	                                ,TIT_CNR
	                                ,(SELECT COUNT(TIT_CNR) 
	                                  FROM FINANASS 
	                                  WHERE TIT_CNR = FS.TIT_CNR 
		                                AND ((FIN_VENC > '1899-30-12' AND FIN_VENC < '2020-30-12')
		                                AND (FIN_VAL >= 1 AND FIN_VAL < 2000)
		                                AND (FIN_PGTO > '2007-01-01' AND FIN_PGTO < '2016-04-04' OR FIN_PGTO IS NULL)
		                                AND (FIN_VRPG >= 1 AND FIN_VRPG < 2000 OR FIN_VRPG IS NULL)
                                        AND (FIN_CANC > '2008-01-01' AND FIN_CANC < '2016-04-04' OR FIN_CANC IS NULL))
	                                  GROUP BY TIT_CNR) QTD_PARC
                                    ,FIN_PARC
                                    ,FIN_VENC
                                    ,FIN_VAL
                                    ,FIN_PGTO
                                    ,FIN_CANC
                                    ,FIN_VRPG
                                    ,TRAT_NR
                                    ,PAG_COD
                                    ,NUM_DOC
                                    ,NUM_GUIA
                                    ,FIN_GERACAO
                                    ,US_ATUALIZACAO
                                    ,DT_ATUALIZACAO
                                    ,CONF_PREBAIXA
                                    ,DEP_CNR
                                    ,TRAT_TIPO
                                    ,SEQ_BOLETO
                                    ,VALOR_BOLETO
                                    ,TIPO_BOLETO
                                    ,OBS_BOLETO
                                    ,US_BAIXA
                                    ,DT_BAIXA
                                    ,DTGER_BOLETO
                                    ,BANCO
                                    ,AGENCIA
                                    ,CONTA
                                    ,IR
                                    ,NFe
                                    ,US_NEGOCIACAO
                                    ,DT_NEGOCIACAO
                                FROM FINANASS FS
                                WHERE 
                                    SEQ_IMP NOT IN(SELECT SEQ_IMP FROM MODCLISRV.dbo.TBS47_CTA_RECEB) AND
	                                ((FIN_VENC > '1899-30-12' AND FIN_VENC < '2020-30-12')
	                                AND (FIN_VAL >= 1 AND FIN_VAL < 2000)
	                                AND (FIN_PGTO > '2007-01-01' AND FIN_PGTO < '2016-01-04' OR FIN_PGTO IS NULL)
	                                AND (FIN_VRPG >= 1 AND FIN_VRPG < 2000 OR FIN_VRPG IS NULL)
                                    AND (FIN_CANC > '2008-01-01' AND FIN_CANC < '2016-04-04' OR FIN_CANC IS NULL))
                                ORDER BY SEQ_IMP";

            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 1000;

            sqlConnection.Open();
            
            SqlDataReader reader = cmd.ExecuteReader();

            //var res = new List<Finanass>();

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            var tpBoleto = TB086_TIPO_DOC.RetornaPeloCoTipoDoc(3);
            var tpBoleto1 = TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(1);
            var tpBoleto2 = TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(2);
            var tpBoleto3 = TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(3);
            var tpBoleto4 = TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(4);
            var tpBoleto5 = TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(5);
            var tpBoleto6 = TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(6);
            var tpBoleto7 = TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(7);
            var inst = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
            var hist = TB39_HISTORICO.RetornaPelaChavePrimaria(78);
            var agrup = TB83_PARAMETRO.RetornaPelaChavePrimaria(tb25.CO_EMP).CO_AGRUP_REC;

            while (reader.Read())
            {
                var nire = int.Parse(reader["TIT_CNR"].ToString());
                TB07_ALUNO tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(a => a.NU_NIRE == nire).FirstOrDefault();
                if (tb07 != null)
                {
                    tb07.TB108_RESPONSAVELReference.Load();

                    var tbs47 = new TBS47_CTA_RECEB();

                    tbs47.SEQ_IMP = int.Parse(reader["SEQ_IMP"].ToString());
                    tbs47.NU_CONTRATO = int.Parse(reader["TRAT_NR"].ToString()).ToString("D7");
                    //tbs47.TBS390_ATEND_AGEND = tbs390;
                    tbs47.CO_ALU = tb07.CO_ALU;
                    tbs47.TB108_RESPONSAVEL = tb07.TB108_RESPONSAVEL;
                    tbs47.TB25_EMPRESA = tb25;
                    tbs47.CO_EMP_UNID_CONT = tb25.CO_EMP;
                    tbs47.NU_PAR = int.Parse(reader["FIN_PARC"].ToString());
                    tbs47.QT_PAR = int.Parse(reader["QTD_PARC"].ToString());
                    tbs47.DT_VEN_DOC = !String.IsNullOrEmpty(reader["FIN_VENC"].ToString()) ? DateTime.Parse(reader["FIN_VENC"].ToString()) : DateTime.Now;
                    tbs47.DT_REC_DOC = !String.IsNullOrEmpty(reader["FIN_PGTO"].ToString()) ? DateTime.Parse(reader["FIN_PGTO"].ToString()) : (DateTime?)null;
                    tbs47.NU_DOC = "CT" + tbs47.DT_VEN_DOC.ToString("yy") + "." + tbs47.NU_CONTRATO + "." + tbs47.NU_PAR.ToString("D2");
                    tbs47.VL_PAR_DOC =
                    tbs47.VL_TOT_DOC =
                    tbs47.VL_LIQ_DOC = !String.IsNullOrEmpty(reader["FIN_VAL"].ToString()) ? decimal.Parse(reader["FIN_VAL"].ToString()) : 0;
                    tbs47.VL_PAG = !String.IsNullOrEmpty(reader["FIN_VRPG"].ToString()) ? decimal.Parse(reader["FIN_VRPG"].ToString()) : (decimal?)null;
                    tbs47.VL_DES_DOC =
                    tbs47.VL_MUL_DOC =
                    tbs47.VL_JUR_DOC = 0;

                    tbs47.CO_NOS_NUM = !String.IsNullOrEmpty(reader["SEQ_BOLETO"].ToString()) ? reader["SEQ_BOLETO"].ToString() : null;
                    tbs47.DE_COM_HIST = "TRATAMENTO Nº " + tbs47.NU_CONTRATO;
                    tbs47.DE_OBS = "TITÚLO MIGRADO DO SITEMA ANTERIOR";
                    tbs47.DE_OBS_BOL = reader["OBS_BOLETO"].ToString();

                    switch (reader["AGENCIA"].ToString())
                    {
                        case "0046550":
                            tbs47.TB227_DADOS_BOLETO_BANCARIO = tpBoleto1;
                            break;
                        case "0048951":
                            tbs47.TB227_DADOS_BOLETO_BANCARIO = tpBoleto2;
                            break;
                        case "0057667":
                            tbs47.TB227_DADOS_BOLETO_BANCARIO = tpBoleto3;
                            break;
                        case "0060269":
                            tbs47.TB227_DADOS_BOLETO_BANCARIO = tpBoleto4;
                            break;
                        case "1488826":
                            tbs47.TB227_DADOS_BOLETO_BANCARIO = tpBoleto5;
                            break;
                        case "0248879":
                            tbs47.TB227_DADOS_BOLETO_BANCARIO = tpBoleto6;
                            break;
                        case "0254860":
                            tbs47.TB227_DADOS_BOLETO_BANCARIO = tpBoleto7;
                            break;
                        default:
                            tbs47.TB227_DADOS_BOLETO_BANCARIO = tpBoleto2;
                            break;
                    }

                    tbs47.IC_SIT_DOC = "A";
                    tbs47.DT_SITU_DOC = !String.IsNullOrEmpty(reader["DT_ATUALIZACAO"].ToString()) ? DateTime.Parse(reader["DT_ATUALIZACAO"].ToString()) : (!String.IsNullOrEmpty(reader["FIN_GERACAO"].ToString()) ? DateTime.Parse(reader["FIN_GERACAO"].ToString()) : DateTime.Now);

                    if (!String.IsNullOrEmpty(reader["FIN_CANC"].ToString()))
                    {
                        tbs47.IC_SIT_DOC = "C";
                        tbs47.DT_SITU_DOC = DateTime.Parse(reader["FIN_CANC"].ToString());
                    }
                    else if (tbs47.DT_REC_DOC.HasValue || tbs47.VL_PAG.HasValue)
                    {
                        if (tbs47.VL_PAG.HasValue)
                            if (tbs47.VL_PAG < tbs47.VL_TOT_DOC)
                                tbs47.IC_SIT_DOC = "P";
                            else
                                tbs47.IC_SIT_DOC = "Q";

                        if (tbs47.DT_REC_DOC.HasValue)
                            tbs47.DT_SITU_DOC = tbs47.DT_REC_DOC;
                    }

                    tbs47.TB000_INSTITUICAO = inst;

                    tbs47.TB39_HISTORICO = hist;
                    tbs47.CO_AGRUP_RECDESP = agrup;

                    // Salvando o tipo de documento "Boleto Bancário"
                    tbs47.TB086_TIPO_DOC = tpBoleto;

                    tbs47.TP_CLIENTE_DOC = "A";
                    tbs47.FL_EMITE_BOLETO = "S";
                    tbs47.FL_TIPO_PREV_RECEB = "B";

                    tbs47.CO_FLAG_TP_VALOR_MUL = "P";
                    tbs47.CO_FLAG_TP_VALOR_JUR = "V";
                    tbs47.CO_FLAG_TP_VALOR_DES = "V";
                    tbs47.CO_FLAG_TP_VALOR_OUT = "V";

                    tbs47.DT_CAD_DOC =
                    tbs47.DT_EMISS_DOCTO = !String.IsNullOrEmpty(reader["DT_ATUALIZACAO"].ToString()) ? DateTime.Parse(reader["DT_ATUALIZACAO"].ToString()) : (!String.IsNullOrEmpty(reader["FIN_GERACAO"].ToString()) ? DateTime.Parse(reader["FIN_GERACAO"].ToString()) : DateTime.Now);
                    
                    tbs47.DT_ALT_REGISTRO = tbs47.DT_SITU_DOC;

                    TBS47_CTA_RECEB.SaveOrUpdate(tbs47);
                }

                //var fin = new Finanass();

                //fin.TIT_CNR = !String.IsNullOrEmpty(reader["TIT_CNR"].ToString()) ? int.Parse(reader["TIT_CNR"].ToString()) : 0;
                //fin.FIN_PARC = !String.IsNullOrEmpty(reader["FIN_PARC"].ToString()) ? int.Parse(reader["FIN_PARC"].ToString()) : 0;
                //fin.FIN_VENC = !String.IsNullOrEmpty(reader["FIN_VENC"].ToString()) ? DateTime.Parse(reader["FIN_VENC"].ToString()) : DateTime.Now;
                //fin.FIN_VAL = !String.IsNullOrEmpty(reader["FIN_VAL"].ToString()) ? decimal.Parse(reader["FIN_VAL"].ToString()) : 0;
                //fin.FIN_PGTO = !String.IsNullOrEmpty(reader["FIN_PGTO"].ToString()) ? DateTime.Parse(reader["FIN_PGTO"].ToString()) : DateTime.Now;
                //fin.FIN_CANC = !String.IsNullOrEmpty(reader["FIN_CANC"].ToString()) ? DateTime.Parse(reader["FIN_CANC"].ToString()) : DateTime.Now;
                //fin.FIN_VRPG = !String.IsNullOrEmpty(reader["FIN_VRPG"].ToString()) ? decimal.Parse(reader["FIN_VRPG"].ToString()) : 0;
                //fin.TRAT_NR = !String.IsNullOrEmpty(reader["TRAT_NR"].ToString()) ? int.Parse(reader["TRAT_NR"].ToString()) : 0;
                //fin.PAG_COD = !String.IsNullOrEmpty(reader["PAG_COD"].ToString()) ? int.Parse(reader["PAG_COD"].ToString()) : 0;
                //fin.NUM_DOC = reader["NUM_DOC"].ToString();
                //fin.NUM_GUIA = reader["NUM_GUIA"].ToString();
                //fin.FIN_GERACAO = !String.IsNullOrEmpty(reader["FIN_GERACAO"].ToString()) ? DateTime.Parse(reader["FIN_GERACAO"].ToString()) : DateTime.Now;
                //fin.US_ATUALIZACAO = reader["US_ATUALIZACAO"].ToString();
                //fin.DT_ATUALIZACAO = !String.IsNullOrEmpty(reader["DT_ATUALIZACAO"].ToString()) ? DateTime.Parse(reader["DT_ATUALIZACAO"].ToString()) : DateTime.Now;
                //fin.CONF_PREBAIXA = reader["CONF_PREBAIXA"].ToString();
                //fin.DEP_CNR = !String.IsNullOrEmpty(reader["DEP_CNR"].ToString()) ? int.Parse(reader["DEP_CNR"].ToString()) : 0;
                //fin.TRAT_TIPO = reader["TRAT_TIPO"].ToString();
                //fin.SEQ_BOLETO = !String.IsNullOrEmpty(reader["SEQ_BOLETO"].ToString()) ? int.Parse(reader["SEQ_BOLETO"].ToString()) : 0;
                //fin.VALOR_BOLETO = !String.IsNullOrEmpty(reader["VALOR_BOLETO"].ToString()) ? decimal.Parse(reader["VALOR_BOLETO"].ToString()) : 0;
                //fin.TIPO_BOLETO = reader["TIPO_BOLETO"].ToString();
                //fin.OBS_BOLETO = reader["OBS_BOLETO"].ToString();
                //fin.US_BAIXA = reader["US_BAIXA"].ToString();
                //fin.DT_BAIXA = !String.IsNullOrEmpty(reader["DT_BAIXA"].ToString()) ? DateTime.Parse(reader["DT_BAIXA"].ToString()) : DateTime.Now;
                //fin.DTGER_BOLETO = !String.IsNullOrEmpty(reader["DTGER_BOLETO"].ToString()) ? DateTime.Parse(reader["DTGER_BOLETO"].ToString()) : DateTime.Now;
                //fin.BANCO = reader["BANCO"].ToString();
                //fin.AGENCIA = reader["AGENCIA"].ToString();
                //fin.CONTA = reader["CONTA"].ToString();
                //fin.IR = reader["IR"].ToString();
                //fin.NFe = reader["NFe"].ToString();
                //fin.US_NEGOCIACAO = reader["US_NEGOCIACAO"].ToString();
                //fin.DT_NEGOCIACAO = !String.IsNullOrEmpty(reader["DT_NEGOCIACAO"].ToString()) ? DateTime.Parse(reader["DT_NEGOCIACAO"].ToString()) : DateTime.Now;

                //res.Add(fin);
            }

            sqlConnection.Close();
        }

        public class Finanass
        {
            public int TIT_CNR { get; set; }
            public int FIN_PARC { get; set; }
            public DateTime FIN_VENC { get; set; }
            public decimal FIN_VAL { get; set; }
            public DateTime FIN_PGTO { get; set; }
            public DateTime FIN_CANC { get; set; }
            public decimal FIN_VRPG { get; set; }
            public int TRAT_NR { get; set; }
            public int PAG_COD { get; set; }
            public string NUM_DOC { get; set; }
            public string NUM_GUIA { get; set; }
            public DateTime FIN_GERACAO { get; set; }
            public string US_ATUALIZACAO { get; set; }
            public DateTime DT_ATUALIZACAO { get; set; }
            public string CONF_PREBAIXA { get; set; }
            public int DEP_CNR { get; set; }
            public string TRAT_TIPO { get; set; }
            public int SEQ_BOLETO { get; set; }
            public decimal VALOR_BOLETO { get; set; }
            public string TIPO_BOLETO { get; set; }
            public string OBS_BOLETO { get; set; }
            public string US_BAIXA { get; set; }
            public DateTime DT_BAIXA { get; set; }
            public DateTime DTGER_BOLETO { get; set; }
            public string BANCO { get; set; }
            public string AGENCIA { get; set; }
            public string CONTA { get; set; }
            public string IR { get; set; }
            public string NFe { get; set; }
            public string US_NEGOCIACAO { get; set; }
            public DateTime DT_NEGOCIACAO { get; set; }
        }
    }
}
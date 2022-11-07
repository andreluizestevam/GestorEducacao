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

namespace C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2101_ServSolicItensServicEscolar
{
    public partial class RptDeclaracaoImposRenda : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptDeclaracaoImposRenda()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(string parametros,
                                string infos,
                                int strP_CO_EMP_REF,
                                string strP_CO_ANO_MES_MAT,
                                int strP_CO_CUR,
                                int strP_CO_MODU_CUR,
                                int strP_CO_TUR,
                                int strp_CO_ALU,
                                int coEmp,
                                string nomeFuncio
            )
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
                C2BR.GestorEducacao.Reports.ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);

                // Seta o título do relatório
                if (nomeFuncio != null)
                    lblTitulo.Text = nomeFuncio;
                else
                    lblTitulo.Text = "DECLARAÇÃO DO IMPOSTO DE RENDA";

                // Verifica se o cabeçalho está vazio
                if (header == null)
                {
                    return 0;
                }

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query
                // Query dos dados do Aluno, Responsável e Empresa 
                var dados = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                             join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb07.CO_EMP equals tb25.CO_EMP
                             join tb904Emp in TB904_CIDADE.RetornaTodosRegistros() on tb25.CO_CIDADE equals tb904Emp.CO_CIDADE
                             join tb904Alu in TB904_CIDADE.RetornaTodosRegistros() on tb07.TB905_BAIRRO.CO_CIDADE equals tb904Alu.CO_CIDADE
                             where
                             tb07.CO_EMP == strP_CO_EMP_REF
                             && tb07.CO_ALU == strp_CO_ALU
                             select new DeclaracaoImposRenda
                             {
                                 //Aluno
                                 rltNomeAluno = tb07.NO_ALU,
                                 
                                 //Responsável
                                 rltNomeResp = tb07.TB108_RESPONSAVEL.NO_RESP,
                                 _rltCpfResp = tb07.TB108_RESPONSAVEL.NU_CPF_RESP,
                                 rltEndResp = tb07.TB108_RESPONSAVEL.DE_ENDE_RESP,
                                 rltCidadResp = tb904Alu.NO_CIDADE,
                                 rltBairrResp = tb07.TB905_BAIRRO.NO_BAIRRO,
                                 _rltCepResp = tb07.TB108_RESPONSAVEL.CO_CEP_RESP,
                                 rltUfResp = tb07.TB108_RESPONSAVEL.TB74_UF.CODUF,
                                 
                                 //Empresa
                                 rltNomeEmp = tb07.TB25_EMPRESA.NO_FANTAS_EMP,
                                 _rltCnpjEmp = tb07.TB25_EMPRESA.CO_CPFCGC_EMP,
                                 rltCidadEmp = tb904Emp.NO_CIDADE,
                                 rltEstadEmp = tb07.TB25_EMPRESA.CO_UF_EMP
                             });

                // Pega o primeiro resultado
                var res = dados.First();

                // Verifica se foi encontrado um resultado
                if (res == null)
                    return -1;

                // Query dos dados de pagamento
                var result = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                              where tb47.CO_ALU == strp_CO_ALU
                              && tb47.CO_ANO_MES_MAT.Substring(0, 4) == strP_CO_ANO_MES_MAT.Substring(0, 4)
                              select new Pagamento
                              {
                                  //Pagamento
                                  rltPerioPgto = tb47.CO_ANO_MES_MAT.Substring(0, 4),
                                  rltDocPgto = tb47.NU_DOC,
                                  rltServiPgto = tb47.DE_OBS,
                                  rltParcPgto = tb47.NU_PAR,
                                  _rltVenciPgto = tb47.DT_VEN_DOC,
                                  rltValorPgto = tb47.VR_TOT_DOC,
                                  _rltDataPgto = tb47.DT_REC_DOC

                              }).OrderBy(w => w.rltParcPgto).ToList();

                bsReport.Clear();
                res.pagamento = result;

                #endregion

        #endregion

                bsReport.Add(res);

                return 1;

            }

            catch { return 0; }
        }

        #region Classe Boletim Aluno
        public class DeclaracaoImposRenda
        {
            public DeclaracaoImposRenda()
            {
                this.pagamento = new List<Pagamento>();
            }
            //Aluno
            public string rltNomeAluno { get; set; }

            //Responsável
            public string rltNomeResp;
            public string _rltCpfResp;
            public string rltCpfResp
            {
                get
                {
                    return _rltCpfResp.Insert(3, ".").Insert(7, ".").Insert(11, "-");
                }
            }
            public string rltNomeCpfResp { get { string rncr = rltNomeResp + "   -   " + rltCpfResp; return rncr; } }
            public string rltEndResp { get; set; }
            public string rltCidadResp { get; set; }
            public string rltBairrResp { get; set; }
            public string _rltCepResp;
            public string rltCepResp
            {
                get
                {
                    return _rltCepResp.Insert(5, "-");
                }
            }
            public string rltUfResp { get; set; }



            //Empresa
            public string rltNomeEmp { get; set; }
            public string _rltCnpjEmp;
            public string rltCnpjEmp
            {
                get
                {
                    return _rltCnpjEmp.Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-");
                }
            }

            //Outros
            public string rltCidadEmp;
            public string rltEstadEmp;
            public string rltMesExten
            {
                get
                {
                    string mesExtenso = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(DateTime.Now.Month).ToLower();

                    return mesExtenso;
                }
            }
            public string rltCidadData { get { string rcd = rltCidadEmp + " - " + rltEstadEmp + ", " + DateTime.Now.Day + " de " + rltMesExten + " de " + DateTime.Now.Year + "."; return rcd; } }


            public List<Pagamento> pagamento { get; set; }
        }

        public class Pagamento
        {

            //Pagamento
            public string rltPerioPgto { get; set; }
            public string rltDocPgto { get; set; }
            public string rltServiPgto { get; set; }
            public int rltParcPgto { get; set; }
            public DateTime _rltVenciPgto;
            public string rltVenciPgto
            {
                get
                {
                    return _rltVenciPgto.ToString("dd/MM/yyyy");
                }
            }
            public decimal? rltValorPgto { get; set; }
            public DateTime? _rltDataPgto;
            public string rltDataPgto
            {
                get
                {
                    return Convert.ToDateTime(_rltDataPgto).ToString("dd/MM/yyyy");
                }
            }
            public string rltTotalParcPgto { get; set; }
            public string rltTotalValorPgto { get; set; }
        }

        #endregion
    }
}

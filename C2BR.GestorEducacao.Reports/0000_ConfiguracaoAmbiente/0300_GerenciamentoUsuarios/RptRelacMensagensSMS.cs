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

namespace C2BR.GestorEducacao.Reports._0000_ConfiguracaoAmbiente._0300_GerenciamentoUsuarios
{
    public partial class RptRelacMensagensSMS : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptRelacMensagensSMS()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                              int coUnid,
                              int Colaborador,
                              string status,
                              string TipoContato,
                              int CodDestinatario,
                              string OutroDestinatario,
                              string DataIni,
                              string DataFim,
                              string no_relatorio

            )
        {
            try
            {
                #region Inicializa o header/Labels

                //Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.Parametros = parametros;

                //Cria o header a partir do cod da instituicao
                var header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return -1;

                //Inicializa o headero
                base.BaseInit(header);

                #endregion
                DateTime DataInical = Convert.ToDateTime(DataIni);
                DateTime DataFinal = Convert.ToDateTime(DataFim);
                //Seta o título dinamicamente inserindo um * caso não exista título definido de forma que apenas emitindo o relatório é possível saber se é dinamico ou não
                this.lblTitulo.Text = (!string.IsNullOrEmpty(no_relatorio) ? no_relatorio : "RELACAO DE  MENSAGENS (SMS) *");

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
                var res = (from tb249 in TB249_MENSAG_SMS.RetornaTodosRegistros()
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb249.TB03_COLABOR.CO_COL equals tb03.CO_COL
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb03.CO_EMP equals tb25.CO_EMP
                           where (tb249.DT_ENVIO_MENSAG_SMS >= DataInical && tb249.DT_ENVIO_MENSAG_SMS <= DataFinal
                            && (coUnid != 0 ? tb25.CO_EMP == coUnid : 0 == 0)
                            && (Colaborador != 0 ? tb03.CO_COL == Colaborador : 0 == 0)
                            && (status != "0" ? tb249.CO_STATUS == status : 0 == 0)
                            && (TipoContato != "0" ? tb249.CO_TP_CONTAT_SMS == TipoContato : 0 == 0)
                            && (OutroDestinatario != "" ? tb249.NO_RECEPT_SMS.Contains(OutroDestinatario) : 0 == 0)//0 == 0)
                            && (CodDestinatario != 0 ? tb249.CO_RECEPT == CodDestinatario : 0 == 0))
                           select new RelacaoDeMensagens
                           {
                               DataHora = tb249.DT_ENVIO_MENSAG_SMS,                             
                               NomeOutros =  tb249.NO_RECEPT_SMS,
                               Emissor = tb03.NO_COL,
                               Unidade = tb25.sigla,
                               TipoDeContato = tb249.CO_TP_CONTAT_SMS,
                               Status = tb249.CO_STATUS,
                               Mensagens = tb249.DES_MENSAG_SMS,
                               CO_RECEPT = tb249.CO_RECEPT,
                               CO_TP_RECEPT = tb249.CO_TP_CONTAT_SMS,
                               CodTipoContatoDestinatario = tb249.CO_RECEPT,

                           }).OrderBy(w => w.DataHora).OrderBy(y => y.DataHora).ToList();

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                //Adiciona ao DataSource do Relatório
                bsReport.Clear();


                foreach (RelacaoDeMensagens at in res)
                {
                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class RelacaoDeMensagens
        {
            public DateTime? DataHora { get; set; }
            public string Emissor { get; set; }
            public string NomeOutros { get; set; }
            public string Unidade { get; set; }
            public string TipoDeContato { get; set; }
            public string TipoDeContatoValor
            {
                get
                {
                    string s = "";
                    switch (this.TipoDeContato)
                    {
                        case "A":
                            s = "Aluno";
                            break;
                        case "R":
                            s = "Responsável";
                            break;
                        case "F":
                            s = "Funcionário";
                            break;
                        case "P":
                            s = "Professor";
                            break;
                        case "O":
                            s = this.NomeOutros;
                            break;
                        default:
                            s = this.NomeOutros;
                            break;
                    }
                    return s;

                }

            }
            public string Status { get; set; }
            public string StatusValor
            {
                get
                {
                    if (Status == "E")
                    {
                        return "Enviada";
                    }
                    else
                    {
                        return "Não Enviada";
                    }

                }

            }
            public string Mensagens { get; set; }
            public int CodTipoContatoDestinatario { get; set; }
            public int CO_RECEPT { get; set; }
            public string CO_TP_RECEPT { get; set; }
            public string NomeDestinatario
            {
                get
                {
                    string s = "";
                    switch (this.CO_TP_RECEPT)
                    {
                        case "A":
                            s = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == this.CodTipoContatoDestinatario).FirstOrDefault().NO_ALU;
                            break;
                        case "R":
                            s = TB108_RESPONSAVEL.RetornaTodosRegistros().Where(a => a.CO_RESP == this.CodTipoContatoDestinatario).FirstOrDefault().NO_RESP;

                            break;
                        case "F":
                        case "P":
                            s = TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == this.CO_RECEPT).FirstOrDefault().NO_COL;
                            break;
                        case "O":
                            s = this.NomeOutros;
                            break;
                        default:
                            s = this.NomeOutros;
                            break;
                    }
                    return s;
                }
            }
            public string DataHoraValor
            {

                get
                {
                    return (this.DataHora.HasValue ? this.DataHora.Value.ToString("dd/MM/yy") + " " + this.DataHora.Value.ToString("HH:mm") : "-");
                }
            }
            public string NomeDestinatarioValor
            {
                get
                {

                    return (this.NomeDestinatario.Length > 25 ? this.NomeDestinatario.Substring(0, 25) + "..." : this.NomeDestinatario);
                }

            }
        }

    }
}

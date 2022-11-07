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

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar
{
    public partial class RptRelacOcorrencias : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptRelacOcorrencias()
        {
            InitializeComponent();
        }
        public int InitReport(
                    string parametros,
                    string infos,
                    int coEmp,
                    int coMod,
                    int coCur,
                    int coTur,
                    string coAno,
                    int coAlu,
                    DateTime dtIni,
                    DateTime dtFim,
                    string NO_RELATORIO,
                    string tipoOcorre
            )
        {
            try
            {
                #region Inicializa o header/Labels

                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;

                // Cria o header a partir do cod da instituicao
                var header = ReportHeader.GetHeaderFromEmpresa(coEmp);

                //Seta o título dinamicamente inserindo um * caso não exista título definido de forma que apenas emitindo o relatório é possível saber se é dinamico ou não
                this.lblTitulo.Text = (!string.IsNullOrEmpty(NO_RELATORIO) ? NO_RELATORIO : "RELAÇÃO DE OCORRÊNCIAS POR ALUNO*");

                this.lblp.Text = parametros;

                if (header == null)
                    return -1;

                // Inicializa o header
                base.BaseInit(header);
                //this.celFreq.Text = string.Format(celFreq.Text, anoBase);

                #endregion

                // Instancia o contexto

                dtFim = DateTime.Parse(dtFim.ToString("dd/MM/yyyy") + " 23:59:59");
                var res = (from tb191 in TB191_OCORR_ALUNO.RetornaTodosRegistros()
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb191.TB03_COLABOR.CO_COL equals tb03.CO_COL
                           where (coEmp != 0 ? tb191.TB25_EMPRESA.CO_EMP == coEmp : 0 == 0)
                           && (coMod != 0 ? tb191.TB44_MODULO.CO_MODU_CUR == coMod : 0 == 0)
                           && (coCur != 0 ? tb191.CO_CUR == coCur : 0 == 0)
                           && (coTur != 0 ? tb191.CO_TUR == coTur : 0 == 0)
                           && (coAlu != 0 ? tb191.ID_RECEB_OCORR == coAlu : 0 == 0)
                           && (coAno != "0" ? tb191.CO_ANO == coAno : 0 == 0)
                           && (tipoOcorre != "0" ? tb191.TB150_TIPO_OCORR.CO_SIGL_OCORR == tipoOcorre : 0 == 0)
                           && ((tb191.DT_OCORR >= dtIni) && (tb191.DT_OCORR <= dtFim))
                           select new MoviFinanMatric
                           {
                               //Dados Gerais da Ocorrência 
                               DT_OCORR = tb191.DT_OCORR,
                               OCORRENCIA = tb191.DE_OCORR,
                               ACAO = tb191.DE_ACAO_TOMAD,
                               NO_RESP_OCORR = tb03.NO_COL,
                               NO_TIPO = tb191.TB150_TIPO_OCORR.DE_TIPO_OCORR,
                               SGL_TIPO = tb191.TB150_TIPO_OCORR.CO_SIGL_OCORR,
                               CO_CATEG = tb191.CO_CATEG,
                               ID_RECEB = tb191.ID_RECEB_OCORR.Value,

                               //Dados do Aluno
                               //NO_ALU = tb191.TB07_ALUNO.NO_ALU,
                               //CO_SEXO_ALU = tb191.TB07_ALUNO.CO_SEXO_ALU,
                               //NIRE_ALU = tb191.TB07_ALUNO.NU_NIRE,
                               //DT_NASC_ALU = tb191.TB07_ALUNO.DT_NASC_ALU,

                               ////Dados do Responsável
                               //NO_RESP = tb191.TB07_ALUNO.TB108_RESPONSAVEL.NO_RESP,
                               //CO_SEXO_RESP = tb191.TB07_ALUNO.TB108_RESPONSAVEL.CO_SEXO_RESP,
                               //NU_TEL = tb191.TB07_ALUNO.TB108_RESPONSAVEL.NU_TELE_RESI_RESP,
                               //NU_COM = tb191.TB07_ALUNO.TB108_RESPONSAVEL.NU_TELE_COME_RESP,
                               //NU_CEL = tb191.TB07_ALUNO.TB108_RESPONSAVEL.NU_TELE_CELU_RESP,
                               //CPF_RESP = tb191.TB07_ALUNO.TB108_RESPONSAVEL.NU_CPF_RESP,
                           }).ToList();

                res = res.OrderBy(o => o.NO_ALU).ThenByDescending(o => o.DT_OCORR).ToList();

                if (res.Count == 0)
                    return -1;

                //Prepara a legenda com as descrições dos tipos de Ocorrências
                #region Prepara a Legenda
                var noSgl = res.DistinctBy(w => w.NO_TIPO).ToList();
                string leg = "Legenda: ";
                foreach (var i in noSgl) // Percorre todas as siglas existentes no select e concatena a legenda com a sigla e respectiva descrição
                {
                    leg += "- " + i.SGL_TIPO + " (" + i.NO_TIPO + ") ";
                }
                lblLegenda.Text = leg;
                #endregion

                bsReport.Clear();
                foreach (MoviFinanMatric r in res)
                    bsReport.Add(r);

                return 1;
            }
            catch { return 0; }
        }

        public class MoviFinanMatric
        {
            //Dados Gerais da Ocorrência
            public DateTime DT_OCORR { get; set; }
            public string DT_OCORR_V
            {
                get
                {
                    return (this.DT_OCORR.ToString("dd/MM/yy") + " " + this.DT_OCORR.ToString("HH:mm"));
                }
            }
            public string OCORRENCIA { get; set; }
            public string ACAO { get; set; }
            public string NO_RESP_OCORR { get; set; }
            public string SGL_TIPO { get; set; }
            public string NO_TIPO { get; set; }
            public string CO_CATEG { get; set; }
            public int ID_RECEB { get; set; }

            //Dados do Aluno
            public string NO_ALU
            {
                get
                {
                    string s = "";

                    switch (CO_CATEG)
                    {
                        case "A":
                            var resDadAlu = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                             where tb07.CO_ALU == this.ID_RECEB
                                             select new
                                             {
                                                 tb07.NO_ALU,
                                             }).FirstOrDefault();

                            s = resDadAlu.NO_ALU.ToUpper();
                            break;
                        case "R":
                            var resDadResp = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                              where tb108.CO_RESP == this.ID_RECEB
                                              select new
                                              {
                                                  tb108.NO_RESP,
                                              }).FirstOrDefault();

                            s = resDadResp.NO_RESP.ToUpper();
                            break;
                        case "F":
                        case "P":
                            var resDadFunc = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                              where tb03.CO_COL == this.ID_RECEB
                                              select new
                                              {
                                                  tb03.NO_COL,
                                              }).FirstOrDefault();

                            s = resDadFunc.NO_COL.ToUpper();
                            break;
                    }
                    return s;
                }
            }
            public int NIRE_ALU { get; set; }
            public string CO_SEXO_ALU { get; set; }
            public string DE_SEXO_ALU
            {
                get
                {
                    return (Funcoes.RetornaSexo(this.CO_SEXO_ALU));
                }
            }
            public DateTime? DT_NASC_ALU { get; set; }
            public string CONCAT_ALU
            {
                get
                {
                    string s = "";

                    switch (CO_CATEG)
                    {
                        case "A":
                            var resDadAlu = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                             where tb07.CO_ALU == this.ID_RECEB
                                             select new
                                             {
                                                 tb07.NO_ALU,
                                                 tb07.NU_NIRE,
                                             }).FirstOrDefault();

                            s = "ALUNO(A): " + resDadAlu.NU_NIRE.ToString().PadLeft(7, '0') + " - " + resDadAlu.NO_ALU.ToUpper();
                            break;
                        case "R":
                            var resDadResp = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                              where tb108.CO_RESP == this.ID_RECEB
                                              select new
                                              {
                                                  tb108.NO_RESP,
                                                  tb108.NU_CPF_RESP,
                                              }).FirstOrDefault();

                            s = "RESPONSÁVEL: " + Funcoes.Format(resDadResp.NU_CPF_RESP, TipoFormat.CPF) + " - " + resDadResp.NO_RESP.ToUpper();
                            break;
                        case "F":
                        case "P":
                            var resDadFunc = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                              where tb03.CO_COL == this.ID_RECEB
                                              select new
                                              {
                                                  tb03.NO_COL,
                                                  tb03.CO_MAT_COL,
                                              }).FirstOrDefault();

                            s = (this.CO_CATEG == "F" ? "FUNCIONÁRIO(A): " : "PROFESSOR(A): ") + Funcoes.Format(resDadFunc.CO_MAT_COL, TipoFormat.MatriculaColaborador) + " - " + resDadFunc.NO_COL.ToUpper();
                            break;
                    }
                    return s;
                }
            }

            //Dados do responsável
            public string NO_RESP { get; set; }
            public string CPF_RESP { get; set; }
            public string CO_SEXO_RESP { get; set; }
            public string DE_SEXO_RESP
            {
                get
                {
                    return Funcoes.RetornaSexo(this.CO_SEXO_RESP);
                }
            }
            public string NU_TEL { get; set; }
            public string NU_CEL { get; set; }
            public string NU_COM { get; set; }
            public string concat_numeros
            {
                get
                {
                    return "CEL: " + Funcoes.Format(this.NU_CEL, TipoFormat.Telefone) + " - COM: " + Funcoes.Format(this.NU_COM, TipoFormat.Telefone) + " - TEL: " + Funcoes.Format(this.NU_TEL, TipoFormat.Telefone);
                }
            }
        }
    }
}

using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario
{
    public partial class RptRelacaoSituacaoContrato : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptRelacaoSituacaoContrato()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                               int codEmp,
                               int codUndCont,
                               string situacao,
                               int CodOrdem,
                               int Operadora,
                               string infos,
                               string NO_RELATORIO)
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

                //Seta o título dinamicamente inserindo um * caso não exista título definido de forma que apenas emitindo o relatório é possível saber se é dinamico ou não
                this.lblTitulo.Text = (!string.IsNullOrEmpty(NO_RELATORIO) ? NO_RELATORIO.ToUpper() : "RELAÇÃO DE PACIENTES*");

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Paciente

                var dt = DateTime.Now;
                var lst = new List<Aluno>();

                if (situacao != "F")
                    lst = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where (codUndCont != 0 ? tb07.CO_EMP == codUndCont : 0 == 0)
                              && (Operadora != 0 ? tb07.TB250_OPERA.ID_OPER == Operadora : 0 == 0)
                              && (situacao != "0" ? tb07.CO_SITU_ALU == situacao : 0 == 0)
                           select new Aluno
                           {
                               Sexo = tb07.CO_SEXO_ALU,
                               UndCont = tb07.TB25_EMPRESA.sigla,
                               DataNascimento = tb07.DT_NASC_ALU,
                               DataCadastro = tb07.DT_CADA_ALU,
                               dataEntrada = tb07.DT_ENTRA_INSTI,
                               Nire = tb07.NU_NIRE,
                               Nis = tb07.NU_NIS,
                               Nome = tb07.NO_ALU,
                               Responsavel = tb07.TB108_RESPONSAVEL.NO_APELIDO_RESP,
                               CPFResp = tb07.TB108_RESPONSAVEL.NU_CPF_RESP,
                               Situacao = tb07.CO_SITU_ALU,
                               DeficienciaRecebe = tb07.TP_DEF,
                               DataSituacao = tb07.DT_SITU_ALU,
                               Ano = tb07.CO_ANO_ORI,
                               Mes = tb07.CO_MES_REFER,
                               Operadora = tb07.TB250_OPERA.NM_SIGLA_OPER == null ? "--" : tb07.TB250_OPERA.NM_SIGLA_OPER,
                               Telefone = tb07.TB108_RESPONSAVEL.NU_TELE_CELU_RESP != null && tb07.TB108_RESPONSAVEL.NU_TELE_CELU_RESP != "" ? tb07.TB108_RESPONSAVEL.NU_TELE_CELU_RESP.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "--",
                           }).ToList();
                else
                    lst = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(a => a.DT_AGEND_HORAR >= dt)
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                           where (codUndCont != 0 ? tb07.CO_EMP == codUndCont : 0 == 0)
                              && (Operadora != 0 ? tb07.TB250_OPERA.ID_OPER == Operadora : 0 == 0)
                              && tbs174.CO_SITUA_AGEND_HORAR != "C" && tb07.CO_SITU_ALU == "A"
                           select new Aluno
                           {
                               Sexo = tb07.CO_SEXO_ALU,
                               UndCont = tb07.TB25_EMPRESA.sigla,
                               DataNascimento = tb07.DT_NASC_ALU,
                               DataCadastro = tb07.DT_CADA_ALU,
                               dataEntrada = tb07.DT_ENTRA_INSTI,
                               Nire = tb07.NU_NIRE,
                               Nis = tb07.NU_NIS,
                               Nome = tb07.NO_ALU,
                               Responsavel = tb07.TB108_RESPONSAVEL.NO_APELIDO_RESP,
                               CPFResp = tb07.TB108_RESPONSAVEL.NU_CPF_RESP,
                               Situacao = tb07.CO_SITU_ALU,
                               DeficienciaRecebe = tb07.TP_DEF,
                               DataSituacao = tbs174.DT_AGEND_HORAR,
                               Ano = tb07.CO_ANO_ORI,
                               Mes = tb07.CO_MES_REFER,
                               Operadora = tb07.TB250_OPERA.NM_SIGLA_OPER == null ? "--" : tb07.TB250_OPERA.NM_SIGLA_OPER,
                               Telefone = tb07.TB108_RESPONSAVEL.NU_TELE_CELU_RESP != null && tb07.TB108_RESPONSAVEL.NU_TELE_CELU_RESP != "" ? tb07.TB108_RESPONSAVEL.NU_TELE_CELU_RESP.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "--",
                           }).DistinctBy(a => a.Nire).ToList();

                var res = lst.ToList();

                switch (CodOrdem)
                {
                    case 1:
                        res = lst.OrderBy(p => p.UndCont).ThenBy(p => p.Nome).ToList();
                        break;

                    case 3:
                        res = lst.OrderBy(p => p.DataCadastro).ThenBy(p => p.Nome).ToList();
                        break;
                    case 4:
                        res = lst.OrderBy(p => p.Nome).ToList();
                        break;
                    case 5:
                        res = lst.OrderBy(p => p.Responsavel).ThenBy(p => p.Nome).ToList();
                        break;
                }

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (Aluno at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Alunos do Relatorio

        public class Aluno
        {
            public DateTime? dataEntrada { get; set; }
            public DateTime? DataCadastro { get; set; }
            public DateTime? DataNascimento { get; set; }
            public decimal? Nis { get; set; }
            public string Nome { get; set; }
            public string Operadora { get; set; }
            public string Responsavel { get; set; }
            public string CPFResp { get; set; }
            public string Sexo { get; set; }
            public string UndCont { get; set; }
            public string Telefone { get; set; }
            public int Nire { get; set; }
            public string Situacao { get; set; }
            public DateTime? DataSituacao { get; set; }
            public string DeficienciaRecebe { get; set; }

            public string Deficiencia
            {
                get
                {
                    if (this.DeficienciaRecebe != null)
                    {
                        if (this.DeficienciaRecebe == "N")
                            return "Nenhuma";
                        else if (this.DeficienciaRecebe == "A")
                            return "Auditiva";
                        else if (this.DeficienciaRecebe == "V")
                            return "Visual";
                        else if (this.DeficienciaRecebe == "F")
                            return "Fisica";
                        else if (this.DeficienciaRecebe == "M")
                            return "Mental";
                        else if (this.DeficienciaRecebe == "P")
                            return "Múltiplas";
                        else if (this.DeficienciaRecebe == "O")
                            return "Outras";
                        else
                            return "-";
                    }
                    else
                        return "--";
                }
            }

            public string NomeDesc
            {
                get { return this.Nome.ToUpper(); }
            }

            public string DataNascDesc
            {
                get { return this.DataNascimento != null ? this.DataNascimento.Value.ToString("dd/MM/yyyy") : "XX/XX/XX"; }
            }

            public string SituacaoDesc
            {
                get
                {
                    if (DataSituacao == null)
                    {
                        switch (this.Situacao)
                        {
                            case "A":
                                return "Em Atendimento" + " - " + "--";

                            case "V":
                                return "Em Analisa" + " - " + "--";

                            case "E":
                                return "Alta (Normal)" + " - " + "--";

                            case "D":
                                return "Alta (Desistência)" + " - " + "--";

                            case "I":
                                return "Inativo" + " - " + "--";
                            default:
                                return "--";
                        }
                    }
                    else
                    {
                        DateTime dtRecebe = this.DataSituacao.Value;
                        switch (this.Situacao)
                        {
                            case "A":
                                return "Em Atendimento" + " - " + dtRecebe.ToShortDateString();

                            case "V":
                                return "Em Analisa" + " - " + dtRecebe.ToShortDateString();

                            case "E":
                                return "Alta (Normal)" + " - " + dtRecebe.ToShortDateString();

                            case "D":
                                return "Alta (Desistência)" + " - " + dtRecebe.ToShortDateString();

                            case "I":
                                return "Inativo" + " - " + dtRecebe.ToShortDateString();
                            default:
                                return " -- ";
                        }
                    }

                }
            }

            public string Idade
            {
                get { return this.DataNascimento.HasValue ? Funcoes.FormataDataNascimento(this.DataNascimento.Value).ToString() : "--"; }
            }

            public string NireDesc
            {
                get { return this.Nire.ToString().PadLeft(7, '0'); }
            }

            public string RespDescricao { get { return this.Responsavel != null ? (CPFResp.Format(TipoFormat.CPF) + " - " + Responsavel.ToUpper()) : "--"; } }

            public int? Mes { get; set; }
            public string Ano { get; set; }
            public string AnoMes
            {
                get
                {
                    if (dataEntrada.HasValue)
                        return dataEntrada.Value.Year + "a " + dataEntrada.Value.Month + "m";
                    else
                        return " - ";
                }
            }
        }

        #endregion
    }
}

using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Text.RegularExpressions;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3300_Consultas
{
    public partial class RptExtratoConsultas : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public static int totMasc, totFemin, totDefic = 0;

        #region ctor

        public RptExtratoConsultas()
        {
            InitializeComponent();
        }
        #endregion

        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                              int UnidadeCadastro,
                              int UnidadeContrato,
                              int Especialidade,
                              string ClassificacaoProfissional,
                              int ProfissionalSaude,
                              int UnidadeConsulta,
                              int DeptLocal,
                              int EspecialidadeConsulta,
                              string noSituacao,
                              string noStatus,
                              string dataIni,
                              string dataFim,
                              bool VerValor,
            //int Unidade,
            //int LocalDept,
            //int Espec,
            //int Medico,
            //string status,
            //string situa,
            //string dataIni,
            //string dataFim,
                              string NO_RELATORIO
                               )
        {
            try
            {
                DateTime dataIni1;
                if (!DateTime.TryParse(dataIni, out dataIni1))
                {
                    return 0;
                }

                DateTime dataFim1;
                if (!DateTime.TryParse(dataFim, out dataFim1))
                {
                    return 0;
                }


                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                //Seta o título dinamicamente inserindo um * caso não exista título definido de forma que apenas emitindo o relatório é possível saber se é dinamico ou não
                this.lblTitulo.Text = (!string.IsNullOrEmpty(NO_RELATORIO) ? NO_RELATORIO : "AGENDA DE CONSULTAS*");

                // Setar o header do relatorio
                this.BaseInit(header);

                if (VerValor == true)
                {
                    tblValor.Text = "VALOR";

                    xrValor.Visible = true;
                }
                else
                {
                    tblValor.Text = "";
                    xrValor.Text = "";
                }
                

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Colaborador/Departamento
                DateTime DataInicial = Convert.ToDateTime(dataIni);
                DateTime DataFinal = Convert.ToDateTime(dataFim);
                var lst = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                           join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tbs174.CO_ESPEC equals tb63.CO_ESPECIALIDADE into lesAg
                           from lespecAgenda in lesAg.DefaultIfEmpty()
                           join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tbs174.CO_DEPT equals tb14.CO_DEPTO
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                           join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals tb63.CO_ESPECIALIDADE into le
                           from lesp in le.DefaultIfEmpty()
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tb25.CO_EMP
                           join t07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals t07.CO_ALU into l2
                           from lal in l2.DefaultIfEmpty()
                           where (coEmp != 0 ? tb25.CO_EMP == coEmp : 0 == 0)
                           && (UnidadeCadastro != 0 ? tb03.CO_EMP == UnidadeCadastro : 0 == 0)
                           && (UnidadeContrato != 0 ? tb03.CO_EMP_UNID_CONT == UnidadeContrato : 0 == 0)
                           && (Especialidade != 0 ? lespecAgenda.CO_ESPEC == Especialidade : 0 == 0)
                           && (ClassificacaoProfissional != "0" ? tb03.CO_CLASS_PROFI == ClassificacaoProfissional : 0 == 0)
                           && (ProfissionalSaude != 0 ? tb03.CO_COL == ProfissionalSaude : 0 == 0)
                           && (UnidadeConsulta != 0 ? tbs174.CO_EMP == UnidadeConsulta : 0 == 0)
                           && (DeptLocal != 0 ? tbs174.CO_DEPT == DeptLocal : 0 == 0)
                           && (EspecialidadeConsulta != 0 ? tbs174.CO_EMP == EspecialidadeConsulta : 0 == 0)
                           && (noSituacao != "0" ? noSituacao == "G" ? tbs174.CO_ALU != null : tbs174.CO_SITUA_AGEND_HORAR == noSituacao : 0 == 0)
                           && (noStatus != "0" ? tbs174.FL_CONF_AGEND == noStatus : 0 == 0)
                           && (tbs174.DT_AGEND_HORAR >= DataInicial && tbs174.DT_AGEND_HORAR <= DataFinal)
                           //Filtra a situação da consulta, caso seja G de agendada, verifica se o paciente é diferente de nulo
                           select new AgendaConsulta
                           {
                               NomePaciente = lal.NO_ALU,
                               nirePaciente = lal.NU_NIRE,
                               Medico = tb03.NO_COL,
                               CoMedico = tb03.CO_COL,
                               data = tbs174.DT_AGEND_HORAR,
                               hora = tbs174.HR_AGEND_HORAR,
                               TipoConsul = tbs174.TP_CONSU,
                               Unidade = tb25.sigla,
                               Situacao = tbs174.CO_SITUA_AGEND_HORAR,
                               Status = tbs174.FL_CONF_AGEND,
                               flAgendCons = tbs174.FL_AGEND_CONSU,
                               coPaciente = tbs174.CO_ALU,
                               cpfCol = tb03.NU_CPF_COL,
                               funCol = tb03.DE_FUNC_COL,
                               matrCol = tb03.CO_MAT_COL,
                               EspCol = lesp.NO_ESPECIALIDADE,
                               EspecConsul = (lespecAgenda != null ? lespecAgenda.NO_ESPECIALIDADE : " - "),
                               DepartConsul = tb14.NO_DEPTO,
                               Valor = tbs174.VL_CONSUL,

                           }).OrderBy(o => o.Medico).ThenBy(p => p.Unidade).ThenBy(p => p.data).ThenBy(y => y.hora);

                var res = lst.ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();

                //Variáveis de Auxílio
                int auxCount = 0;
                int auxCoCol = 0;
                int countGeral = 0;
                int qtCo = 0;
                foreach (AgendaConsulta at in res)
                {
                    countGeral++;
                    //parte responsável por garantir que o bloco acima seja executado apenas uma vez por colaborador
                    if (auxCoCol != at.CoMedico)
                    {
                        //Zera as variáveis de auxílio
                        auxCount = 0;
                        qtCo = 0;

                        //Inicia a variável de código do colaborador com o novo colaborador
                        auxCoCol = at.CoMedico;
                    }

                    //Conta quantos registros são referentes ao colaborador em questão, para concanetar a string total apenas no último registro do colaborador
                    if (auxCount == 0)
                        foreach (AgendaConsulta atin in res)
                        {
                            if (atin.CoMedico == at.CoMedico)
                                qtCo++;
                        }

                    auxCount++;

                    //Counts do colaborador em contexto só serão executados no último registro de cada colaborador para montar a string do total de cada colaborador
                    #region Counts Colaborador
                    if (auxCount == qtCo)
                    {
                        int qtComPac = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                        where (UnidadeConsulta != 0 ? tbs174.CO_EMP == UnidadeConsulta : 0 == 0)
                                        && (DeptLocal != 0 ? tbs174.CO_DEPT == DeptLocal : 0 == 0)
                                        && (EspecialidadeConsulta != 0 ? tbs174.CO_ESPEC == EspecialidadeConsulta : 0 == 0)
                                        && tbs174.CO_COL == at.CoMedico
                                        && (noStatus != "0" ? tbs174.FL_CONF_AGEND == noStatus : 0 == 0)
                                        && (noSituacao != "0" ? tbs174.CO_SITUA_AGEND_HORAR == noSituacao : 0 == 0)
                                        && (tbs174.CO_ALU != null)
                                        && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                        select new { tbs174.ID_AGEND_HORAR }).Count();

                        int qtSemPac = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                        where (UnidadeConsulta != 0 ? tbs174.CO_EMP == UnidadeConsulta : 0 == 0)
                                        && (DeptLocal != 0 ? tbs174.CO_DEPT == DeptLocal : 0 == 0)
                                        && (EspecialidadeConsulta != 0 ? tbs174.CO_ESPEC == EspecialidadeConsulta : 0 == 0)
                                        && tbs174.CO_COL == at.CoMedico
                                        && (noStatus != "0" ? tbs174.FL_CONF_AGEND == noStatus : 0 == 0)
                                        && (noSituacao != "0" ? tbs174.CO_SITUA_AGEND_HORAR == noSituacao : 0 == 0)
                                        && (tbs174.CO_ALU == null)
                                        && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                        select new { tbs174.ID_AGEND_HORAR }).Count();

                        at.totais = " / " + qtComPac + " com agendadamentos - " + qtSemPac + " sem agendamentos.";
                    }
                    #endregion

                    //Parte executada apenas no último registro de todo o relatório, para preparar a string que será apresentada no total geral
                    #region Counts Gerais

                    if (countGeral == res.Count)
                    {
                        int qtComPacGeral = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                             where (UnidadeConsulta != 0 ? tbs174.CO_EMP == UnidadeConsulta : 0 == 0)
                                             && (DeptLocal != 0 ? tbs174.CO_DEPT == DeptLocal : 0 == 0)
                                             && (EspecialidadeConsulta != 0 ? tbs174.CO_ESPEC == EspecialidadeConsulta : 0 == 0)
                                             && (ProfissionalSaude != 0 ? tbs174.CO_COL == ProfissionalSaude : 0 == 0)
                                             && (noStatus != "0" ? tbs174.FL_CONF_AGEND == noStatus : 0 == 0)
                                             && (noSituacao != "0" ? tbs174.CO_SITUA_AGEND_HORAR == noSituacao : 0 == 0)
                                             && (tbs174.CO_ALU != null)
                                             && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                             select new { tbs174.ID_AGEND_HORAR }).Count();

                        int qtSemPacGeral = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                             where (UnidadeConsulta != 0 ? tbs174.CO_EMP == UnidadeConsulta : 0 == 0)
                                             && (DeptLocal != 0 ? tbs174.CO_DEPT == DeptLocal : 0 == 0)
                                             && (EspecialidadeConsulta != 0 ? tbs174.CO_ESPEC == EspecialidadeConsulta : 0 == 0)
                                             && (ProfissionalSaude != 0 ? tbs174.CO_COL == ProfissionalSaude : 0 == 0)
                                             && (noStatus != "0" ? tbs174.FL_CONF_AGEND == noStatus : 0 == 0)
                                             && (noSituacao != "0" ? tbs174.CO_SITUA_AGEND_HORAR == noSituacao : 0 == 0)
                                             && (tbs174.CO_ALU == null)
                                             && ((tbs174.DT_AGEND_HORAR >= dataIni1) && (tbs174.DT_AGEND_HORAR <= dataFim1))
                                             select new { tbs174.ID_AGEND_HORAR }).Count();

                        at.totaisGerais = " / " + qtComPacGeral + " com agendadamentos - " + qtSemPacGeral + " sem agendamentos.";
                    }

                    #endregion

                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Colagorador/Departamento do Relatorio

        public class AgendaConsulta
        {
            //Dados da consulta
            public DateTime data { get; set; }
            public string dataValid
            {
                get
                {
                    return data.ToString("dd/MM/yy") + " - " + this.hora;
                    //eturn this.data.ToString("dd/MM/yy");
                }
            }
            public string hora { get; set; }
            public decimal? Valor { get; set; }
            public string TipoConsul { get; set; }
            public string TipoConsulValid
            {
                get
                {
                    string s = "";
                    switch (this.TipoConsul)
                    {
                        case "C":
                            s = "Consulta";
                            break;

                        case "R":
                            s = "Retorno";
                            break;

                        case "N":
                            s = "Normal";
                            break;

                        case "U":
                            s = "Urgência";
                            break;
                    }
                    return s;
                }
            }
            public string EspecConsul { get; set; }
            public string DepartConsul { get; set; }
            public string flAgendCons { get; set; }
            public string Status { get; set; }
            public string StatusValid
            {
                get
                {
                    string r = "";
                    switch (this.Status)
                    {
                        case "S":
                            r = "Sim";
                            break;

                        case "N":
                            r = "Não";
                            break;
                    }
                    if ((this.flAgendCons == "S") && (this.coPaciente == null))
                    {
                        return "Indisponível";
                    }
                    else
                    {
                        return r;
                    }
                }
            }

            //Dados do Paciente
            public int? coPaciente { get; set; }
            public string NomePaciente { get; set; }
            public int? nirePaciente { get; set; }
            public string NireValid
            {
                get
                {
                    string s = (this.nirePaciente.HasValue ? this.nirePaciente.Value.ToString() : "");
                    if (!string.IsNullOrEmpty(s)) { return s.PadLeft(7, '0'); }
                    else { return ""; }
                }
            }
            public string NomeNirePaciente
            {
                get
                {
                    return this.NireValid + " - " + (!string.IsNullOrEmpty(this.NomePaciente) ? (this.NomePaciente.Length > 25 ? this.NomePaciente.Substring(0, 25) + "..." : this.NomePaciente) : "");
                }
            }
            public string Medico { get; set; }
            public string Unidade { get; set; }

            //Dados do Profissional de Saúde
            public int CoMedico { get; set; }
            public string matrCol { get; set; }
            public string cpfCol { get; set; }
            public string funCol { get; set; }
            public string EspCol { get; set; }
            public string colaborador
            {
                get
                {
                    string fun = this.funCol != null ? this.funCol : "******";

                    String r = String.Format("Profissional da Saúde: {0} - {1}  ", this.matrCol, this.Medico.ToUpper());

                    r += " ( CPF: " + cpfCol.Insert(3, ".").Insert(7, ".").Insert(11, "-") + " - Função: " + fun.ToUpper() + " - Especialidade: " + this.EspCol.ToUpper() + " )";
                    return r;
                }
            }
            public string Situacao { get; set; }
            public string SituacaoValid
            {
                get
                {
                    string s = "";
                    switch (this.Situacao)
                    {
                        case "A":
                            //Trata para mostrar Agendado caso esteja com situação A e com paciente informado,
                            //e Em Aberto caso esteja A mas sem paciente
                            s = (this.coPaciente.HasValue ? "Agendado" : "Em Aberto");
                            break;

                        case "C":
                            s = "Cancelado";
                            break;

                        case "R":
                            s = "Realizada";
                            break;
                    }
                    return s;
                }
            }

            //Dados Gerais
            public string totais { get; set; }
            public string totaisGerais { get; set; }
        }
        #endregion
    }
}

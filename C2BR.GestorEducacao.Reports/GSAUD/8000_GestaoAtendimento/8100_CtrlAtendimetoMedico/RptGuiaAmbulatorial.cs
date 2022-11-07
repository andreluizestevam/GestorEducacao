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
using System.Globalization;

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAtendimetoMedico
{
    public partial class RptGuiaAmbulatorial : C2BR.GestorEducacao.Reports.RptRetratoCentro
    {
        public RptGuiaAmbulatorial()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(
                        string infos,
                        int coEmp,
                        int coAtendimento,
                        int coServAmbulatorial
                        )
        {


            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;

                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
                #region TBS426

                var res = (from tbs426 in TBS426_SERVI_AMBUL.RetornaTodosRegistros()
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs426.CO_COL_CADAS equals tb03.CO_COL
                           join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tbs426.TBS174_AGEND_HORAR.ID_AGEND_HORAR equals tbs174.ID_AGEND_HORAR
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs426.CO_EMP_CADAS equals tb25.CO_EMP
                           join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb25.CO_CIDADE equals tb904.CO_CIDADE
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                           join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb07.TB905_BAIRRO.CO_BAIRRO equals tb905.CO_BAIRRO into l1
                           from ls in l1.DefaultIfEmpty()
                           where tbs426.ID_SERVI_AMBUL == coServAmbulatorial
                           select new
                           {
                               tb07.NO_ALU,
                               tb07.DT_NASC_ALU,
                               tb07.CO_SEXO_ALU,
                               tb07.NU_NIRE,
                               ls.NO_BAIRRO,
                               ls.TB904_CIDADE.NO_CIDADE,
                               tb07.CO_ESTA_ALU,
                               tb03.NO_COL,
                               tb03.CO_SEXO_COL,
                               tb03.CO_SIGLA_ENTID_PROFI,
                               tb03.NU_ENTID_PROFI,
                               tb03.CO_UF_ENTID_PROFI,
                               tb03.DT_EMISS_ENTID_PROFI,
                               tbs426.DE_OBSER,
                               tbs426.DT_CADASTRO,
                               coAtendimento,
                               tb904.CO_UF,
                               tb25.NO_FANTAS_EMP
                           }).FirstOrDefault();

                // Erro: não encontrou registros
                if (res == null)
                    return -1;
                #endregion

                #region TBS427

                var procMedico = (from tbs427 in TBS427_SERVI_AMBUL_ITENS.RetornaTodosRegistros()
                                  join tbs426 in TBS426_SERVI_AMBUL.RetornaTodosRegistros() on tbs427.TBS426_SERVI_AMBUL.ID_SERVI_AMBUL equals tbs426.ID_SERVI_AMBUL
                                  where tbs427.TBS426_SERVI_AMBUL.ID_SERVI_AMBUL == coServAmbulatorial
                                  select new
                                  {
                                      tbs427.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                                  }).ToList();


                List<ItensServAmbulatoriais> ItensServAmbulatoriais = new List<ItensServAmbulatoriais>();
                int count = 0;
                foreach (var p in procMedico)
                {
                    ItensServAmbulatoriais item = new ItensServAmbulatoriais();
                    var proc = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(p.ID_PROC_MEDI_PROCE);
                    item.nomeProcAmbulatorial = proc != null ? proc.NM_PROC_MEDI : "-";
                    item.obsProcAmbulatorial = proc != null ? proc.DE_OBSE_PROC_MEDI : "-";

                    proc.TBS353_VALOR_PROC_MEDIC_PROCE.Load();
                    if (proc.TBS353_VALOR_PROC_MEDIC_PROCE != null && proc.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(x => x.FL_STATU == "A") != null)
                        item.valorProcAmbulatorial = proc.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(x => x.FL_STATU == "A").VL_BASE;
                    count++;
                    item.Contador = count;
                    ItensServAmbulatoriais.Add(item);
                }
                #endregion

                // Seta os alunos no DataSource do Relatorio

                //Trata a label de informações do dia
                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                int dia = DateTime.Now.Day;
                int ano = DateTime.Now.Year;
                string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(DateTime.Now.Month));
                string diasemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(DateTime.Now.DayOfWeek));
                string data = diasemana + ", " + dia + " de " + mes + " de " + ano;

                var Ambulatorio = new GuiaAmbulatorial
                {
                    //Informações do Paciente
                    Paciente = res.NO_ALU,
                    Nascimento = res.DT_NASC_ALU.HasValue ? res.DT_NASC_ALU.Value : new DateTime(1900, 1, 1),
                    SexoPac = !String.IsNullOrEmpty(res.CO_SEXO_ALU) ? res.CO_SEXO_ALU == "F" ? "Feminino" : "Masculino" : "-",
                    NirePaci = res.NU_NIRE,
                    Bairro = res.NO_BAIRRO ?? "",
                    Cidade = res.NO_CIDADE ?? "",
                    Uf = !String.IsNullOrEmpty(res.CO_ESTA_ALU) ? res.CO_ESTA_ALU : "-",

                    //Informações do Médico
                    nomeMedico = res.NO_COL,
                    sexoMedico = res.CO_SEXO_COL,
                    SIGLA_ENT = res.CO_SIGLA_ENTID_PROFI,
                    NUMER_ENT = res.NU_ENTID_PROFI,
                    UF_ENT = res.CO_UF_ENTID_PROFI,
                    DT_ENT = res.DT_EMISS_ENTID_PROFI,
                    Observacao = !String.IsNullOrEmpty(res.DE_OBSER) ? "Observação: " + res.DE_OBSER : "",

                    //Dados do atendimento 
                    dataAtend = res.DT_CADASTRO.HasValue ? res.DT_CADASTRO.Value : new DateTime(1900, 1, 1),
                    coAtendimento = coAtendimento,

                    //Informações gerais
                    nomeCidade = res.NO_CIDADE,
                    nomeUF = res.CO_UF,
                    unidEmiss = res.NO_FANTAS_EMP,
                    infoDia = res.NO_FANTAS_EMP + " - " + res.CO_UF + ", " + data,
                    itensServAmbulatoriais = ItensServAmbulatoriais
                };

                bsReport.Clear();
                bsReport.Add(Ambulatorio);
                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class GuiaAmbulatorial
        {
            public int coAtendimento { get; set; }
            public string Observacao { get; set; }
            //Informações do Paciente
            public string Paciente { get; set; }
            public DateTime Nascimento { get; set; }
            public string NascPac
            {
                get
                {
                    return Nascimento.ToShortDateString();
                }
            }
            public string SexoPac { get; set; }
            public string idadePac
            {
                get
                {
                    return Funcoes.FormataDataNascimento(Nascimento);
                }
            }
            public int NirePaci { get; set; }
            public string PacienteNireValid
            {
                get
                {
                    return this.NirePaci + " - " + this.Paciente;
                }
            }
            public string Bairro { get; set; }
            public string Cidade { get; set; }
            public string Uf { get; set; }
            public string concatEndereco
            {
                get
                {
                    return (this.Bairro != "" ? this.Bairro + ", " + this.Cidade + " - " + this.Uf : " - ");
                }
            }
            public string RG { get; set; }
            public string orgRG { get; set; }
            public string ufRG { get; set; }
            public string concatRG
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.RG) ? this.RG + " - " + this.orgRG + "/" + this.ufRG : " - ");
                }
            }

            //Dados do atendimento
            public string dadosAtend
            {
                get
                {
                    string valor = "(ATENDIMENTO Nº " + this.coAtendimento + " - Em " + this.dataAtend.ToString("dd/MM/yy") + " às " + this.dataAtend.ToString("HH:mm") + ")";
                    return valor;
                }
            }
            public string codigoAtend { get; set; }
            public DateTime dataAtend { get; set; }

            //Informações do Médico
            public string nomeMedico { get; set; }
            public string CRMMedico { get; set; }
            public string sexoMedico { get; set; }
            public string MedicoValid
            {
                get
                {
                    return (this.sexoMedico == "M" ? "Dr. " : "Dra. ") + this.nomeMedico;
                }
            }
            public string SIGLA_ENT { get; set; }
            public string NUMER_ENT { get; set; }
            public string UF_ENT { get; set; }
            public DateTime? DT_ENT { get; set; }
            public string ENT_CONCAT
            {
                get
                {
                    string valor = this.SIGLA_ENT + " " + this.NUMER_ENT + " - " + this.UF_ENT;
                    if (valor != null && valor != "")
                        return valor;
                    else
                        return "-";
                }
            }

            //Informações Gerais
            public string nomeCidade { get; set; }
            public string nomeUF { get; set; }
            public string unidEmiss { get; set; }
            public string infoDia { get; set; }

            //Itens
            public List<ItensServAmbulatoriais> itensServAmbulatoriais { get; set; }
        }

        public class ItensServAmbulatoriais
        {
            //Informações Serviços Ambulatoriais
            public int Contador { get; set; }
            public string cont { get {
                return Contador > 0 ? Contador.ToString() + "." : "-";
            } }
            public string nomeProcAmbulatorial { get; set; }
            public Decimal? valorProcAmbulatorial { get; set; }
            public string valProAmbulatorial
            {
                get
                {
                    return this.valorProcAmbulatorial.HasValue ? this.valorProcAmbulatorial.ToString() : "-";
                }
            }
            public string obsProcAmbulatorial { get; set; }
        }
    }
}
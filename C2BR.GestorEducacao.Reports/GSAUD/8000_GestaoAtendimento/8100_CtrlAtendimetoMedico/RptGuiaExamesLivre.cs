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
    public partial class RptGuiaExamesLivre : C2BR.GestorEducacao.Reports.RptRetratoCentro
    {
        public RptGuiaExamesLivre()
        {
        
            InitializeComponent();
        }
        
        #region Init Report

        public int InitReport(
                        string infos,
                        int coEmp,
                        int coAtendimento
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
                #region Query

                var res = (from tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs390.TBS174_AGEND_HORAR.CO_ALU equals tb07.CO_ALU
                           join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb07.TB905_BAIRRO.CO_BAIRRO equals tb905.CO_BAIRRO into l1
                           from ls in l1.DefaultIfEmpty()
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs390.CO_COL_ATEND equals tb03.CO_COL
                           join tbs398 in TBS398_ATEND_EXAMES.RetornaTodosRegistros().Where(x => x.FL_TIPO_PRESC_EXAMES == "L") on tbs390.ID_ATEND_AGEND equals tbs398.TBS390_ATEND_AGEND.ID_ATEND_AGEND
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs398.CO_EMP_CADAS equals tb25.CO_EMP
                           join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb25.CO_CIDADE equals tb904.CO_CIDADE
                           where (coAtendimento != 0 ? tbs398.TBS390_ATEND_AGEND.ID_ATEND_AGEND == coAtendimento : 0 == 0)
                           select new GuiaExame
                           {
                               //Informações do Paciente
                               Paciente = tb07.NO_ALU,
                               Nascimento = tb07.DT_NASC_ALU.HasValue ? tb07.DT_NASC_ALU.Value : new DateTime(1900, 1, 1),
                               SexoPac = !String.IsNullOrEmpty(tb07.CO_SEXO_ALU) ? tb07.CO_SEXO_ALU == "F" ? "Feminino" : "Masculino" : "-",
                               NirePaci = tb07.NU_NIRE,
                               Bairro = ls.NO_BAIRRO ?? "",
                               Cidade = ls.TB904_CIDADE.NO_CIDADE ?? "",
                               Uf = tb07.CO_ESTA_ALU,

                               //Orçamento 
                            
                             
                               obs = tbs398.DE_OBSER,
                          
                               //Informações do Médico
                               nomeMedico = tb03.NO_COL,
                               sexoMedico = tb03.CO_SEXO_COL,
                               SIGLA_ENT = tb03.CO_SIGLA_ENTID_PROFI,
                               NUMER_ENT = tb03.NU_ENTID_PROFI,
                               UF_ENT = tb03.CO_UF_ENTID_PROFI,
                               DT_ENT = tb03.DT_EMISS_ENTID_PROFI,

                               //Dados do atendimento 
                               dataAtend = tbs390.DT_CADAS,
                               coAtendimento = coAtendimento,

                               //Informações gerais
                               nomeCidade = tb904.NO_CIDADE,
                               nomeUF = tb904.CO_UF,
                               unidEmiss = tb25.NO_FANTAS_EMP,
                               IdExame = tbs398.ID_ATEND_EXAMES,
                               Texto = tbs398.DE_CONTE_MODEL_MEDIC
                           }).OrderByDescending(x => x.IdExame).First();

              
             
                #endregion

                // Erro: não encontrou registros
                if (res == null ||string.IsNullOrEmpty(res.Texto))
                    return -1;


                bsReport.Clear();
               
                    //Trata a label de informações do dia
                    CultureInfo culture = new CultureInfo("pt-BR");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    int dia = DateTime.Now.Day;
                    int ano = DateTime.Now.Year;
                    string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(DateTime.Now.Month));
                    string diasemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(DateTime.Now.DayOfWeek));
                    string data = diasemana + ", " + dia + " de " + mes + " de " + ano;

                    //Atribui algumas informações à label's no relatório
                    this.lblDoutor.Text = res.MedicoValid;
                    this.lblInfosDia.Text = res.nomeCidade + " - " + res.nomeUF + ", " + data;

                    this.xrTableCell3.Text = res.obs;

                    this.lblCRM.Text = res.ENT_CONCAT;
                    this.Parametros = res.dadosAtend;


                    bsReport.Add(res);
                

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class GuiaExame
        {
            public int coAtendimento { get; set; }
           
            public decimal Unit { get; set; }
            public string Texto { get; set; }
        
            public string obs { get; set; }
         
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



            public int IdExame { get; set; }
        }
    }
}

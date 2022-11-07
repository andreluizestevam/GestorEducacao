using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Text.RegularExpressions;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3700_CtrlInformacoesResponsaveis
{
    public partial class RptRelRespAlunosAssociados : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptRelRespAlunosAssociados()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(
                              int codInst,
                              string infos,
                              string strP_CO_TUR,
                              string strP_CO_EMP_REF,
                              string strP_CO_MODU_CUR,
                              string strP_CO_CUR,
                              string parametros,
                              string Ano,
                              string strP_CO_GRAU_INST,
                              string strP_TP_DEF
                              )
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(codInst);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
                //int int_TPDEF = strP_TP_DEF != "T" ? int.Parse(strP_TP_DEF) : 0;
                int int_GRAU_INST = strP_CO_GRAU_INST != "T" ? int.Parse(strP_CO_GRAU_INST) : 0 ;
                #region Query C

                List<RespAlunosAssociados> listAlunos = new List<RespAlunosAssociados>();
                int intCO_EMP_REF = strP_CO_EMP_REF != "T" ? int.Parse(strP_CO_EMP_REF) : 0;
                int intCO_MODU_CUR = strP_CO_MODU_CUR != "T" ? int.Parse(strP_CO_MODU_CUR) : 0;
                int intCO_CUR = strP_CO_CUR != "T" ? int.Parse(strP_CO_CUR) : 0;
                int intCO_TUR = strP_CO_TUR != "T" ? int.Parse(strP_CO_TUR) : 0;


                listAlunos =
                      (
                          from mat in ctx.TB08_MATRCUR
                          join alu in ctx.TB07_ALUNO on mat.CO_ALU equals alu.CO_ALU
                          join cid in ctx.TB904_CIDADE on alu.TB905_BAIRRO.CO_CIDADE equals cid.CO_CIDADE
                          join cur in ctx.TB01_CURSO on mat.CO_CUR equals cur.CO_CUR
                          join tur in ctx.TB06_TURMAS on mat.CO_TUR equals tur.CO_TUR
                          join ctur in ctx.TB129_CADTURMAS on tur.CO_TUR equals ctur.CO_TUR
                          join emp in ctx.TB25_EMPRESA on mat.CO_EMP_UNID_CONT equals emp.CO_EMP
                          join mod in ctx.TB44_MODULO on cur.CO_MODU_CUR equals mod.CO_MODU_CUR
                          join grinst in ctx.TB18_GRAUINS on alu.TB108_RESPONSAVEL.CO_INST equals grinst.CO_INST

                          where (alu.CO_EMP == codInst) && (mat.CO_ANO_MES_MAT == Ano) &&
                                (mat.CO_SIT_MAT == "A") && (mat.CO_ALU != null)
                                && (intCO_MODU_CUR != 0 ? mod.CO_MODU_CUR == intCO_MODU_CUR : 0 == 0) &&
                                (intCO_CUR != 0 ? cur.CO_CUR == intCO_CUR : 0 == 0) &&
                                (intCO_TUR != 0 ? tur.CO_TUR == intCO_TUR : 0 == 0) &&
                                (strP_TP_DEF != "T" ? alu.TP_DEF == strP_TP_DEF : 0 == 0) &&
                                (int_GRAU_INST != 0 ? alu.TB108_RESPONSAVEL.CO_INST == int_GRAU_INST : 0 == 0)

                          select new RespAlunosAssociados
                          {
                              Nome = alu.NO_ALU.ToUpper(),
                              Sexo = alu.CO_SEXO_ALU,
                              DataNasc = alu.DT_NASC_ALU != null ? alu.DT_NASC_ALU : DateTime.MinValue,
                              Serie = cur.CO_SERIE_REFER,
                              Turma = ctur.CO_SIGLA_TURMA,
                              Deficiencia = alu.TP_DEF,
                              Nire = alu.NU_NIRE,

                              Responsavel = alu.TB108_RESPONSAVEL.NO_RESP.ToUpper(),
                              SexoResp = alu.TB108_RESPONSAVEL.CO_SEXO_RESP,
                              DataNascResp = alu.TB108_RESPONSAVEL.DT_NASC_RESP != null ? alu.TB108_RESPONSAVEL.DT_NASC_RESP : DateTime.MinValue,
                              TipoResp = alu.CO_GRAU_PAREN_RESP,
                              GrInstResp = grinst.NO_INST,
                              TelResp = alu.TB108_RESPONSAVEL.NU_TELE_RESI_RESP



                            }

                  ).OrderBy(a => a.Nome).Distinct().ToList();

                //var res = lst.ToList();



                #endregion

                // Erro: não encontrou registros
                if (listAlunos.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (var at in listAlunos)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class RespAlunosAssociados
        {
            //dados do Aluno
            public string Nome { get; set; }
            public string NomeDesc
            {
                get
                {
                    if (this.Nome.Length > 41)
                    {
                        var NomeRetorno = this.Nome.Substring(0, 38) + "...";
                        return NomeRetorno;
                    }
                    else
                    {

                        return this.Nome;
                    }
                }
            }
            public string Sexo { get; set; }
            public DateTime? DataNasc { get; set; }
            public int idade
            {
                get
                {
                    DateTime now = DateTime.Today;
                    int age = now.Year - DataNasc.Value.Year;
                    if (DataNasc > now.AddYears(-age))
                        age--;
                    return age;
                }
            }
            public string Serie { get; set; }
            public string Turma { get; set; }
            public string SerieTurmaDesc
            {
                get
                {
                    return this.Serie + " / " + this.Turma;
                }
            }
            public string Deficiencia { get; set; }
            public string DeficienciaDesc
            {
                get
                {
                    if (this.Deficiencia == "N")
                        return Deficiencia = "Nenhuma";
                    else if (this.Deficiencia == "A")
                        return Deficiencia = "Auditiva";
                    else if (this.Deficiencia == "V")
                        return Deficiencia = "Visual";
                    else if (this.Deficiencia == "F")
                        return Deficiencia = "Física";
                    else if (this.Deficiencia == "M")
                        return Deficiencia = "Mental";
                    else if (this.Deficiencia == "I")
                        return Deficiencia = "Múltiplas";
                    else
                        return Deficiencia = "Outras";


                }
            }
            public int Nire { get; set; }
            public string nireDesc
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
            
            //dados do Responsavel
            public string Responsavel { get; set; }
            public string RespDesc
            {
                get
                {
                    if (this.Responsavel.Length > 30)
                    {
                        var NomeRetorno = this.Responsavel.Substring(0, 26) + "...";
                        return NomeRetorno;
                    }
                    else
                    {

                        return this.Responsavel;
                    }
                }
            }
            public string SexoResp { get; set; }
            public DateTime? DataNascResp { get; set; }
            public int idadeResp
            {
                get
                {
                    DateTime now = DateTime.Today;
                    int age = now.Year - DataNascResp.Value.Year;
                    if (DataNascResp > now.AddYears(-age))
                        age--;
                    return age;
                }
            }
            public string TipoResp { get; set; }
            public string TipoRespDesc
            {
                get
                {
                    string retorno = null;
                    if (this.TipoResp == "PM")
                    {
                        retorno = "Pai/Mãe";
                        return retorno;
                    }
                    else if (this.TipoResp == "AV")
                    {
                        retorno = "Avô/Avó";
                        return retorno;
                    }
                    else if (this.TipoResp == "IR")
                    {
                        retorno = "Irmão(ã)";
                        return retorno;
                    }
                    else if (this.TipoResp == "TI")
                    {
                        retorno = "Tio(a)";
                        return retorno;
                    }
                    else if (this.TipoResp == "PR")
                    {
                        retorno = "Primo(a)";
                        return retorno;
                    }
                    else if (this.TipoResp == "CN")
                    {
                        retorno = "Cunhado(a)";
                        return retorno;
                    }
                    else if (this.TipoResp == "TU")
                    {
                        retorno = "Tutor(a)";
                        return retorno;
                    }
                    else
                        return retorno = "Outros";


                }
            }
            public string GrInstResp { get; set; }
            public string TelResp { get; set; }
            public string TelRespDesc
            {
                get
                {
                    if (!String.IsNullOrEmpty(this.TelResp))
                    {
                        var telRetorno = String.Format("({0}) {1}-{2}", this.TelResp.Substring(0, 2).ToString(),
                                                                       this.TelResp.Substring(2, 4).ToString(),
                                                                       this.TelResp.Substring(6, 4).ToString());
                        return telRetorno;
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
            
            /*public string parametros
            {
                get
                {
                    return "(Unidade: " + this.Unidade + " - Ano de Referência: " + this.Ano + ")";
                }
            }
            public string parametros2
            {
                get
                {
                    string retorno;
                    retorno = "Modalidade: " + this.Modulo + " - Série: " + this.Serie + " - " + " Turma: " + this.Turma;
                    return retorno.ToUpper();
                }
            }
            */
        }
    }
}

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

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos
{
    public partial class RptRelacaoAlunosPorSerieTurma : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptRelacaoAlunosPorSerieTurma()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              string infos,
                              int strP_CO_EMP_REF,
                              string strP_CO_ANO_MES_MAT,
                              int strP_CO_MODU_CUR,
                              int strP_CO_CUR,
                              int strP_CO_TUR)
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

                #region Query C

                List<AlunosPorSerieTurma> listAlunos = new List<AlunosPorSerieTurma>();
               /* use Gestor; 
                select t1.NU_NIRE, t1.NO_ALU, t1.TP_DEF, t1.CO_SEXO_ALU, t1.DT_NASC_ALU, 
	                   t1.CO_GRAU_PAREN_RESP, 
	                   t2.NO_RESP,
	                   t3.NO_CIDADE,
	                   t4.NO_BAIRRO,
	                   t6.NO_CUR,
	                   t8.CO_SIGLA_TURMA,
	                   t9.DE_MODU_CUR	
                from  TB07_ALUNO t1 
                inner join TB108_RESPONSAVEL t2 on t1.CO_RESP = t2.CO_RESP
                inner join TB904_CIDADE t3 on t1.CO_CIDADE = t3.CO_CIDADE
                inner join TB905_BAIRRO t4 on t1.CO_BAIRRO = t4.CO_BAIRRO
                inner join TB08_MATRCUR t5 on t1.CO_ALU = t5.CO_ALU
                inner join TB01_CURSO t6 on t5.CO_CUR = t6.CO_CUR
                inner join TB06_TURMAS t7 on t5.CO_TUR = t7.CO_TUR
                inner join tb129_cadturmas t8 ON t7.CO_TUR = t8.CO_TUR
                inner join TB44_MODULO t9 on t5.CO_MODU_CUR = t9.CO_MODU_CUR
                order by  t9.DE_MODU_CUR, t6.NO_CUR,t8.CO_SIGLA_TURMA,t1.NO_ALU*/
              
                        listAlunos =
                              (
                                  from mat in ctx.TB08_MATRCUR
                                  join alu in ctx.TB07_ALUNO on mat.CO_ALU equals alu.CO_ALU
                                  join cid in ctx.TB904_CIDADE on alu.TB905_BAIRRO.CO_CIDADE equals cid.CO_CIDADE
                                  join cur in ctx.TB01_CURSO on mat.CO_CUR equals cur.CO_CUR
                                  join tur in ctx.TB06_TURMAS on mat.CO_TUR equals tur.CO_TUR
                                  join ctur in ctx.TB129_CADTURMAS on tur.CO_TUR equals ctur.CO_TUR
                                  join emp in ctx.TB25_EMPRESA on mat.CO_EMP equals emp.CO_EMP
                                  join empC in ctx.TB25_EMPRESA on mat.CO_EMP_UNID_CONT equals empC.CO_EMP
                                  join mod in ctx.TB44_MODULO on cur.CO_MODU_CUR equals mod.CO_MODU_CUR

                                  where (strP_CO_EMP_REF != 0 ? strP_CO_EMP_REF == mat.CO_EMP_UNID_CONT : strP_CO_EMP_REF == 0) &&
                                        (mat.CO_SIT_MAT == "A" || mat.CO_SIT_MAT == "R") &&
                                        (mat.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT) && (mat.CO_ALU != null)
                                         && (strP_CO_MODU_CUR != 0 ? mod.CO_MODU_CUR == strP_CO_MODU_CUR : strP_CO_MODU_CUR == 0) &&
                                        (strP_CO_CUR != 0 ? cur.CO_CUR == strP_CO_CUR : strP_CO_CUR == 0) &&
                                        (strP_CO_TUR != 0 ? tur.CO_TUR == strP_CO_TUR : strP_CO_TUR == 0) && (mat.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT)
                                        && (alu.CO_EMP == codInst)

                                  select new AlunosPorSerieTurma
                                  {
                                     
                                      Unidade = emp.NO_FANTAS_EMP,
                                      UnidContr = empC.sigla,
                                      Ano = strP_CO_ANO_MES_MAT,
                                      Nire = alu.NU_NIRE,
                                      Nome = alu.NO_ALU.ToUpper(),
                                      Sexo = alu.CO_SEXO_ALU,
                                      Deficiencia = alu.TP_DEF,
                                      Cidade = cid.NO_CIDADE,
                                      Bairro = alu.TB905_BAIRRO.NO_BAIRRO,
                                      Responsavel = alu.TB108_RESPONSAVEL.NO_RESP.ToUpper(),
                                      CPFResp = alu.TB108_RESPONSAVEL.NU_CPF_RESP,
                                      TipoResp = alu.CO_GRAU_PAREN_RESP,
                                      TelResp = alu.TB108_RESPONSAVEL.NU_TELE_RESI_RESP,
                                      Modulo = mat.TB44_MODULO.DE_MODU_CUR,
                                      Serie = cur.NO_CUR,
                                      Turma = ctur.CO_SIGLA_TURMA,
                                      DtNasc = alu.DT_NASC_ALU.HasValue ? alu.DT_NASC_ALU.Value : DateTime.MinValue,
                                      Situacao = (mat.CO_SIT_MAT == "C" ? "CAN" : (mat.CO_SIT_MAT == "A" ? "MAT" : (mat.CO_SIT_MAT == "T" ? "TRF" : (mat.CO_SIT_MAT == "R" ? "PRE" : "")))) 
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

        #region Classe AlunosPorSerieTurma

        public class AlunosPorSerieTurma
        {
           
            public string Ano{get;set;}
            public string Unidade { get; set; }
            public string UnidContr { get; set; }
            public DateTime DtNasc { get; set; }
            public int Nire { get; set; }
            public string Nome { get; set; }
            public string Sexo { get; set; }
            public string Deficiencia { get; set; }
            public string Cidade { get; set; }
            public string Bairro { get; set; }
            public string Responsavel { get; set; }
            public string CPFResp { get; set; }
            public string TipoResp { get; set; }
            public string TelResp { get; set; }
            public string Modulo { get; set; }
            public string Serie { get; set; }
            public string Turma { get; set; }
            public string Situacao { get; set; }
            public string parametros
            {
                get
                {
                    return "(Unidade: " + this.Unidade + " - Ano de Referência: " + this.Ano+")" ;
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
            public string BairrCidDesc
            {
                get {
                    var retorno = this.Cidade + "/" + this.Bairro;
                    if(retorno.Length > 21)
                        retorno = retorno.Substring(0, 18) + "...";
                    return retorno;
                }
            }
            public string idade
            {
                get
                {
                    DateTime now = DateTime.Today;
                    int ano = now.Year - DtNasc.Year;
                    if (DtNasc > now.AddYears(-ano))
                        ano--;
                    string age = ano.ToString();
                    if (DtNasc.Year < 1923)
                        age = "-";
                    return age;
                }
            }

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

            public string TipoRespDesc
            {
                get
                {
                    string retorno = null;
                    if (this.TipoResp == "PM"){
                        retorno = "Pai/Mãe";
                        return retorno;
                    }else if (this.TipoResp == "AV"){
                        retorno = "Avô/Avó";
                        return retorno;
                    }else if (this.TipoResp == "IR"){
                        retorno = "Irmão(ã)";
                        return retorno;
                    }else if (this.TipoResp == "TI"){
                        retorno = "Tio(a)";
                        return retorno;
                    }else if (this.TipoResp == "PR"){
                        retorno = "Primo(a)";
                        return retorno;
                    }else if (this.TipoResp == "CN"){
                        retorno = "Cunhado(a)";
                        return retorno;
                    }else if (this.TipoResp == "TU"){
                        retorno = "Tutor(a)";
                        return retorno;
                    }else
                        return retorno = "Outros";


                }
            }

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

            public string NomeDesc
            {
                get
                {
                    if (this.Nome.Length > 36)
                    {
                        var NomeRetorno = this.Nome.Substring(0, 33) + "...";
                        return NomeRetorno;
                    }
                    else
                    {

                        return this.Nome;
                    }
                }
            }

            public string RespDesc
            {
                get
                {
                    if (this.Responsavel == null)
                    {
                        this.Responsavel = "";
                    }
                    if (this.Responsavel.Length > 35)
                    {
                        var NomeRetorno = Funcoes.Format(this.CPFResp, TipoFormat.CPF) + " - " + this.Responsavel.Substring(0, 32) + "...";
                        return NomeRetorno;
                    }
                    else
                    {

                        return Funcoes.Format(this.CPFResp, TipoFormat.CPF) + " - " + this.Responsavel;
                    }
                }
            }

        }
    }
}
        #endregion
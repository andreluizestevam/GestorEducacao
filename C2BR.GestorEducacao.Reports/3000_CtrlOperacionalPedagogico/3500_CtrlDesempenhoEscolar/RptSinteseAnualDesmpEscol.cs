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


namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar
{
    public partial class RptSinteseAnualDesmpEscol : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptSinteseAnualDesmpEscol()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              int codInst,
                              string infos,
                              int strP_CO_EMP_REF,
                              string strP_CO_ANO_MES_MAT,
                              int strP_CO_ALU
                              )
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;
                //this.VisibleNumeroPage = false;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(codInst);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);
                
                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Aluno

               
                
                var lst =(from alu in ctx.TB07_ALUNO
                          join mat in ctx.TB08_MATRCUR on alu.CO_ALU equals mat.CO_ALU
                          join hist in ctx.TB079_HIST_ALUNO on alu.CO_ALU equals hist.CO_ALU
                          join ser in ctx.TB01_CURSO on hist.CO_CUR equals ser.CO_CUR
                          join tur in ctx.TB129_CADTURMAS on hist.CO_TUR equals tur.CO_TUR
                          join emp in ctx.TB25_EMPRESA on mat.CO_EMP_UNID_CONT equals emp.CO_EMP
                                                                              
                          where (strP_CO_EMP_REF != 0 ? mat.CO_EMP_UNID_CONT == strP_CO_EMP_REF:0==0) &&
                          (mat.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT)  &&
                          (alu.CO_ALU == strP_CO_ALU)

                          select new Aluno
                          {

                              Unidade = emp.NO_FANTAS_EMP,
                              NomeAluno = alu.NO_ALU.ToUpper(),
                              NomeMae = alu.NO_MAE_ALU != null ? alu.NO_MAE_ALU.ToUpper() : "-" ,
                              NomePai = alu.NO_PAI_ALU != null ? alu.NO_PAI_ALU.ToUpper() : "-",
                              NomeResp = alu.TB108_RESPONSAVEL.NO_RESP != null ? alu.TB108_RESPONSAVEL.NO_RESP.ToUpper(): "-" ,
                              CpfResp = alu.TB108_RESPONSAVEL.NU_CPF_RESP != null ? alu.TB108_RESPONSAVEL.NU_CPF_RESP : null, 
                              Nis = alu.NU_NIS,
                              Nire = alu.NU_NIRE,

                              Matricula = new Matricula()
                              {
                                    sitMat = mat.CO_SIT_MAT != null ? mat.CO_SIT_MAT : "-",
                                    Ano = hist.CO_ANO_REF != null ? hist.CO_ANO_REF : "-",
                                    Modulo = hist.TB44_MODULO.CO_SIGLA_MODU_CUR != null ? hist.TB44_MODULO.CO_SIGLA_MODU_CUR : "-",
                                    Serie = ser.NO_CUR != null ? ser.NO_CUR : "-",
                                    Turma = tur.CO_SIGLA_TURMA != null ? tur.CO_SIGLA_TURMA : "-",
                                    Turno = mat.CO_TURN_MAT != null ? mat.CO_TURN_MAT : "-"
                              }

                              
                          }

                  );
                var alun = lst.FirstOrDefault();
                //var res = lst.ToList();

                if (alun == null)
                    return -1;

                #endregion

                #region 

                var lstDis = (from alu in ctx.TB07_ALUNO
                            join mat in ctx.TB08_MATRCUR on alu.CO_ALU equals mat.CO_ALU
                            join hist in ctx.TB079_HIST_ALUNO on alu.CO_ALU equals hist.CO_ALU
                            join mater in ctx.TB02_MATERIA on hist.CO_MAT equals mater.CO_MAT
                            join materCad in ctx.TB107_CADMATERIAS on mater.ID_MATERIA equals materCad.ID_MATERIA

                            where (strP_CO_EMP_REF != 0 ? mat.CO_EMP_UNID_CONT == strP_CO_EMP_REF : 0 == 0) &&
                            (mat.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT) &&
                            (alu.CO_ALU == strP_CO_ALU)

                            select new InfDisciplinas
                            {
                                
                                Materia = materCad.NO_MATERIA,
                                Mb1 = hist.VL_NOTA_BIM1,
                                Mb2 = hist.VL_NOTA_BIM2,
                                Mb3 = hist.VL_NOTA_BIM3,
                                Mb4 = hist.VL_NOTA_BIM4,
                                Pf = hist.VL_PROVA_FINAL,
                                Mf = hist.VL_MEDIA_FINAL,
                                HsLetivas = mater.QT_CARG_HORA_MAT,
                                
                            }
                ).ToList();

                alun.InfDisciplinas = lstDis;
                #endregion

                bsReport.Clear();
                bsReport.Add(alun);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Aluno
        public class Aluno
        {
            public Aluno()
            {

                this.InfDisciplinas = new List<InfDisciplinas>();
               
            }
            public string Unidade { get; set; }
            public string NomeAluno { get; set; }
            public string NomeMae { get; set; }
            public string NomePai { get; set; }
            public string NomeResp { get; set; }
            public decimal? Nis { get; set; }
            public int Nire { get; set; }
            public string CpfResp { get; set; }
            public Matricula Matricula { get; set; }
            public List<InfDisciplinas> InfDisciplinas { get; set; }
            
            public string CpfRespDesc
            {
                get
                {
                    if (this.CpfResp != null)
                    {
                        return CpfResp.Format(TipoFormat.CPF);
                    }
                    else
                    {
                        return "-";
                    }

                }
            }

            public string NireDesc
            {
                get
                {
                    var strNire = this.Nire.ToString();
                    if(strNire.Length < 9 && strNire != null){
                        while(strNire.Length < 9){
                            strNire = '0'+ strNire;
                        }
                        return strNire;
                    }else{
                        return "-";
                    }
                }
            }
            public string NisDesc
            {
                get
                {
                    if(this.Nis != null){
                        return this.Nis.ToString();
                    }else{
                        return "-";
                    }
                }
            }
        }

        #endregion

        #region Classe Matricula
        public class Matricula
        {
            public string sitMat { get; set; }
            public string Ano { get; set; }
            public string Modulo { get; set; }
            public string Serie { get; set; }
            public string Turma { get; set; }
            public string Turno { get; set; }

            public string  TurnoDesc
            {
                get
                {
                    if (this.Turno != null)
                    {
                        if (this.Turno == "M")
                        {
                            return "Mat";
                        }
                        else if (this.Turno == "V")
                        {
                            return "Ves";
                        }
                        else
                        {
                            return "Not";
                        }
                    }else{
                        return "-";
                    }
                }
            }

            public string sitMatDesc
            {
                get
                {
                    if (this.sitMat == "A")
                    {
                        return "Matriculado";
                    }else if (this.sitMat == "C")
                    {
                        return "Cancelado";
                    }else if (this.sitMat == "D")
                    {
                        return "Desistente";
                    }else if (this.sitMat == "I")
                    {
                        return "Intercâmbio";
                    }else if (this.sitMat == "J")
                    {
                        return "Jubilado";
                    }else if (this.sitMat == "T")
                    {
                        return "Matrícula Trancada";
                    }else if (this.sitMat == "X")
                    {
                        return "Transferência Externa";
                    }else if (this.sitMat == "Y")
                    {
                        return "Transferência Curso";
                    }else
                    {
                        return "Sem Matrícula";
                    }
                }
            }
           
        }
        #endregion

        #region Classe Informações Escolares
        public class InfDisciplinas
        {
            public string Materia { get; set; }
            public decimal? Mb1 { get; set; }
            public decimal Mb1Dec
            {
                get
                {
                    return (decimal)this.Mb1;
                }
            }
            public decimal? Mb2 { get; set; }
            public decimal Mb2Dec
            {
                get
                {
                    return (decimal)this.Mb2;
                }
            }
            public decimal? Mb3 { get; set; }
            public decimal Mb3Dec
            {
                get
                {
                    return (decimal)this.Mb3;
                }
            }
            public decimal? Mb4 { get; set; }
            public decimal Mb4Dec
            {
                get
                {
                    return (decimal)this.Mb4;
                }
            }
            public decimal? Pf { get; set; }
            public decimal? Mf { get; set; }
            public int HsLetivas { get; set; }
            public int Faltas { get; set; }

            public string Mb1Desc
            {
                get
                {
                    if (this.Mb1 != null)
                    {
                        return this.Mb1.ToString();
                    }
                    else
                    {
                        return "-";
                    }
                }
            }

            public string Mb2Desc
            {
                get
                {
                    if (this.Mb2 != null)
                    {
                        return this.Mb2.ToString();
                    }
                    else
                    {
                        return "-";
                    }
                }
            }

            public string Mb3Desc
            {
                get
                {
                    if (this.Mb3 != null)
                    {
                        return this.Mb3.ToString();
                    }
                    else
                    {
                        return "-";
                    }
                }
            }

            public string Mb4Desc
            {
                get
                {
                    if (this.Mb4 != null)
                    {
                        return this.Mb4.ToString();
                    }
                    else
                    {
                        return "-";
                    }
                }
            }

            public string SbDesc
            {
                get
                {
                    if (this.Mb1 != null)
                    {
                        if (this.Mb2 != null)
                        {
                            if (this.Mb3 != null)
                            {
                                if (this.Mb4 != null)
                                {
                                    return ((this.Mb1Dec + this.Mb2Dec + this.Mb3Dec + this.Mb4Dec) / 2).ToString("0.00");
                                }
                                else
                                {
                                    return ((this.Mb1Dec + this.Mb2Dec + this.Mb3Dec) / 2).ToString("0.00");
                                }
                            }
                            else
                            {
                                return ((this.Mb1Dec + this.Mb2Dec) / 2).ToString("0.00");
                            }
                        }
                        else
                        {
                            return this.Mb1Dec.ToString("0.00");
                        }
                    }
                    else
                    {
                        return "-";
                    }

                }
            }

            public decimal? Sb
            {
                get
                {
                    if (this.Mb1 != null)
                    {
                        if (this.Mb2 != null)
                        {
                            if (this.Mb3 != null)
                            {
                                if (this.Mb4 != null)
                                {
                                    return ((this.Mb1 + this.Mb2 + this.Mb3 + this.Mb4) / 2);
                                }
                                else
                                {
                                    return ((this.Mb1 + this.Mb2 + this.Mb3) / 2);
                                }
                            }
                            else
                            {
                                return ((this.Mb1 + this.Mb2) / 2);
                            }
                        }
                        else
                        {
                            return this.Mb1;
                        }
                    }
                    else
                    {
                        return 0;
                    }

                }
            }


            public string PfDesc
            {
                get
                {
                    if (this.Pf != null)
                    {
                        return this.Pf.ToString();
                    }
                    else
                    {
                        return "-";
                    }
                }
            }

            public string MfDesc
            {
                get
                {
                    if (this.Mf != null)
                    {
                        return this.Mf.ToString();
                    }
                    else
                    {
                        return "-";
                    }
                }
            }

            public string HsLetDesc
            {
                get
                {
                    if (this.HsLetivas != 0)
                    {
                        return this.HsLetivas.ToString();
                    }
                    else
                    {
                        return "-";
                    }
                }
            }

            public string FaltasDesc
            {
                get
                {
                    if (this.Faltas != null)
                    {
                        return this.Faltas.ToString();
                    }
                    else
                    {
                        return "-";
                    }
                }
            }


        }
        #endregion
        
    }
}

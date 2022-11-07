//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 15/02/2014| Débora Lohane              | Removida a ordenação fiza por nome do aluno
//           |                            | 
//           |                            | 

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

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos
{
    public partial class RptRelacaoAlunosTelefones : C2BR.GestorEducacao.Reports.RptPaisagem
    {

        #region ctor
        public RptRelacaoAlunosTelefones()
        {
            InitializeComponent();
        }
        #endregion
        #region Init Report

        IEnumerable<AlunosTelefones> listAlunos;
        IEnumerable<AlunosTelefones> listAlunos2;
        public int InitReport(string parametros,
                              int codEmp,
                              string infos,
                              int unid,
                              string ordem,
                              string strP_CO_ANO_MES_MAT,
                              int strP_CO_MODU_CUR,
                              int strP_CO_CUR,
                              int strP_CO_TUR
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
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(codEmp);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
                #region Query Report

                        listAlunos = (from mat in ctx.TB08_MATRCUR
                                      join alu in ctx.TB07_ALUNO on mat.CO_ALU equals alu.CO_ALU
                                      join tur in ctx.TB06_TURMAS on mat.CO_TUR equals tur.CO_TUR
                                      //join tur in ctx.TB06_TURMAS on tur.CO_TUR equals mat.CO_TUR
                                      join cad in ctx.TB129_CADTURMAS on tur.CO_TUR equals cad.CO_TUR
                                      join mod in ctx.TB44_MODULO on tur.CO_MODU_CUR equals mod.CO_MODU_CUR
                                      join cur in ctx.TB01_CURSO on tur.CO_CUR equals cur.CO_CUR
                                      join emp in ctx.TB25_EMPRESA on mat.CO_EMP_UNID_CONT equals emp.CO_EMP
                                      join emp2 in ctx.TB25_EMPRESA on alu.CO_EMP equals emp2.CO_EMP

                                      where (alu.CO_EMP == codEmp) 
                                         && ( unid != 0 ? mat.CO_EMP_UNID_CONT == unid : mat.CO_EMP_UNID_CONT != null )
                                         && (mat.CO_ANO_MES_MAT == strP_CO_ANO_MES_MAT) 
                                         && (strP_CO_MODU_CUR != 0 ? mod.CO_MODU_CUR == strP_CO_MODU_CUR : strP_CO_MODU_CUR == 0) 
                                         && (strP_CO_CUR != 0 ? cur.CO_CUR == strP_CO_CUR : strP_CO_CUR == 0) 
                                         && (strP_CO_TUR != 0 ? tur.CO_TUR == strP_CO_TUR : strP_CO_TUR == 0)
                                      
                                      select new AlunosTelefones
                                      {
                                          Escola = emp2.sigla ,
                                          Nome = alu.NO_ALU.ToUpper(),
                                          DtNasc = alu.DT_NASC_ALU.HasValue ? alu.DT_NASC_ALU.Value : DateTime.MinValue,
                                          Sexo = alu.CO_SEXO_ALU  != "" ? alu.CO_SEXO_ALU : "-",
                                          NomeMae = alu.NO_MAE_ALU != "" ? alu.NO_MAE_ALU.ToUpper() : "-",
                                          Deficiencia = alu.TP_DEF != "" ? alu.TP_DEF : "-",
                                          UnCont = emp.sigla,
                                          Modulo = mod.CO_SIGLA_MODU_CUR,
                                          Serie = cur.CO_SIGL_CUR != "" ? cur.CO_SIGL_CUR : "-",
                                          Turma = cad.CO_SIGLA_TURMA != "" ? cad.CO_SIGLA_TURMA : "-",
                                          Unidade = emp.sigla != "" ? emp.sigla : "X",
                                          nire = alu.NU_NIRE,

                                          NomeResponsavel = alu.TB108_RESPONSAVEL.NO_RESP,
                                          ApelidoResponsavel = alu.TB108_RESPONSAVEL.NO_APELIDO_RESP,
                                          TelCelResp = alu.TB108_RESPONSAVEL.NU_TELE_CELU_RESP,
                                          TelResiResp = alu.TB108_RESPONSAVEL.NU_TELE_RESI_RESP,
                                          TelComResp = alu.TB108_RESPONSAVEL.NU_TELE_COME_RESP,
                                          TelComRamalResp = alu.TB108_RESPONSAVEL.NU_RAMA_COME_RESP,

                                          TeleResiAlu = alu.NU_TELE_RESI_ALU,
                                          TeleCelAlu = alu.NU_TELE_CELU_ALU,
                                          TeleContatoAlu = alu.NU_TELE_CONTAT_ALU,
                                          TeleComAlu = alu.NU_TELE_COME_ALU,
                                          TeleComRamalAlu = alu.NU_RAMA_COME_ALU,

                                          TelPai = alu.NU_TEL_PAI,
                                          TelMae = alu.NU_TEL_MAE
                                      }).Distinct();
                   
                //var res = lst.ToList;

                #endregion

                // Erro: não encontrou registros
                if (listAlunos == null)
                    return -1;

                // Seta os dados no DataSource do Relatorio
                bsReport.Clear();

                List<AlunosTelefones> lista = new List<AlunosTelefones>();

                lista = listAlunos.ToList();

                switch (ordem)
                {
                    case "nire":
                        lista = listAlunos.OrderBy(o => o.nireDesc).ToList();
                        break;
                    case "nome":
                        lista = listAlunos.OrderBy(o => o.NomeDesc).ToList();
                        break;
                }

                bsReport.DataSource = lista.Distinct().ToList();

                //foreach (var at in lista)
                //    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe AlunosPorEscola do Relatorio

        public class AlunosTelefones
        {
            public string Tipo2 { get; set; }
            public string Escola { get; set; }
            public string Unidade { get; set; }
            public int nire { get; set; }
            public string Matricula { get; set; }
            public string Nome { get; set; }
            public DateTime DtNasc { get; set; }
            public string Sexo { get; set; }
            public string Deficiencia { get; set; }
            public string UnCont { get; set; }
            public string Modulo { get; set; }
            public string Serie { get; set; }
            public string Turma { get; set; }
            public string NomeMae { get; set; }
            public string AnoBase2 { get; set; }
            public string Parametros {
                get
                {
                    return "" ;
                }
            }

            // Telefones

            public string NomeResponsavel { get; set; }
            public string ApelidoResponsavel { get; set; }
            public string TelCelResp { get; set; }
            public string TelResiResp { get; set; }
            public string TelComResp { get; set; }
            public string TelComRamalResp { get; set; }

            public string TeleResiAlu { get; set; }
            public string TeleCelAlu { get; set; }
            public string TeleContatoAlu { get; set; }
            public string TeleComAlu { get; set; }
            public string TeleComRamalAlu { get; set; }

            public string TelPai { get; set; }
            public string TelMae { get; set; }


            // Formatação dos telefones

            public string TelCelRespDesc
            {
                get
                {
                    if (!String.IsNullOrEmpty(this.TelCelResp))
                    {
                        var telRetorno = String.Format("({0}) {1}-{2}", this.TelCelResp.Substring(0, 2).ToString(),
                                                                       this.TelCelResp.Substring(2, 4).ToString(),
                                                                       this.TelCelResp.Substring(6, 4).ToString());
                        return telRetorno;
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
            public string TelResiRespDesc
            {
                get
                {
                    if (!String.IsNullOrEmpty(this.TelResiResp))
                    {
                        var telRetorno = String.Format("({0}) {1}-{2}", this.TelResiResp.Substring(0, 2).ToString(),
                                                                       this.TelResiResp.Substring(2, 4).ToString(),
                                                                       this.TelResiResp.Substring(6, 4).ToString());
                        return telRetorno;
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
            public string TelComRespDesc
            {
                get
                {
                    if (!String.IsNullOrEmpty(this.TelComResp))
                    {
                        var telRetorno = String.Format("({0}) {1}-{2}", this.TelComResp.Substring(0, 2).ToString(),
                                                                       this.TelComResp.Substring(2, 4).ToString(),
                                                                       this.TelComResp.Substring(6, 4).ToString());
                        return telRetorno;
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
            public string TeleResiAluDesc
            {
                get
                {
                    if (!String.IsNullOrEmpty(this.TeleResiAlu))
                    {
                        var telRetorno = String.Format("({0}) {1}-{2}", this.TeleResiAlu.Substring(0, 2).ToString(),
                                                                       this.TeleResiAlu.Substring(2, 4).ToString(),
                                                                       this.TeleResiAlu.Substring(6, 4).ToString());
                        return telRetorno;
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
            public string TeleCelAluDesc
            {
                get
                {
                    if (!String.IsNullOrEmpty(this.TeleCelAlu))
                    {
                        var telRetorno = String.Format("({0}) {1}-{2}", this.TeleCelAlu.Substring(0, 2).ToString(),
                                                                       this.TeleCelAlu.Substring(2, 4).ToString(),
                                                                       this.TeleCelAlu.Substring(6, 4).ToString());
                        return telRetorno;
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
            public string TeleContatoAluDesc
            {
                get
                {
                    if (!String.IsNullOrEmpty(this.TeleContatoAlu))
                    {
                        var telRetorno = String.Format("({0}) {1}-{2}", this.TeleContatoAlu.Substring(0, 2).ToString(),
                                                                       this.TeleContatoAlu.Substring(2, 4).ToString(),
                                                                       this.TeleContatoAlu.Substring(6, 4).ToString());
                        return telRetorno;
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
            public string TeleComAluDesc
            {
                get
                {
                    if (!String.IsNullOrEmpty(this.TeleComAlu))
                    {
                        var telRetorno = String.Format("({0}) {1}-{2}", this.TeleComAlu.Substring(0, 2).ToString(),
                                                                       this.TeleComAlu.Substring(2, 4).ToString(),
                                                                       this.TeleComAlu.Substring(6, 4).ToString());
                        return telRetorno;
                    }
                    else
                    {
                        return "-";
                    }
                }
            }            
            public string TelPaiDesc
            {
                get
                {
                    if (!String.IsNullOrEmpty(this.TelPai))
                    {
                        var telRetorno = String.Format("({0}) {1}-{2}", this.TelPai.Substring(0, 2).ToString(),
                                                                       this.TelPai.Substring(2, 4).ToString(),
                                                                       this.TelPai.Substring(6, 4).ToString());
                        return telRetorno;
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
            public string TelMaeDesc
            {
                get
                {
                    if (!String.IsNullOrEmpty(this.TelMae))
                    {
                        var telRetorno = String.Format("({0}) {1}-{2}", this.TelMae.Substring(0, 2).ToString(),
                                                                       this.TelMae.Substring(2, 4).ToString(),
                                                                       this.TelMae.Substring(6, 4).ToString());
                        return telRetorno;
                    }
                    else
                    {
                        return "-";
                    }
                }
            }

            public string DataNasctoDesc
            {
                get
                {
                    if (this.DtNasc == null)
                        return "-";
                    else
                    {
                        return DtNasc.ToString("dd/MM/yyyy");
                    }
                }
            }

            public string DeficienciaDesc
            {
                get
                {
                    if (this.Deficiencia == "N")
                        return Deficiencia = "Nenhuma";
                    else if(this.Deficiencia == "A")
                        return Deficiencia = "Auditiva";
                    else if(this.Deficiencia == "V")
                        return Deficiencia = "Visual";
                    else if(this.Deficiencia == "F")
                        return Deficiencia = "Física";
                    else if(this.Deficiencia == "M")
                        return Deficiencia = "Mental";
                    else if(this.Deficiencia == "I")
                        return Deficiencia = "Múltiplas";
                    else
                        return Deficiencia = "Outras";
                 

                }
            }
            public string matDesc
            {
                get
                {
                    if (!String.IsNullOrEmpty(this.Matricula))
                    {
                        var matRetorno = String.Format("{0}.{1}.{2}.{3}", this.Matricula.Substring(0, 2).ToString(),
                                                        this.Matricula.Substring(2, 3).ToString(),
                                                        this.Matricula.Substring(5, 4).ToString(),
                                                         this.Matricula.Substring(9, 2).ToString());
                        return matRetorno;
                    }
                    else
                    {
                        return "00.000.0000.00";
                    }
                }

            }
            
            public string NomeDesc
            {
                get 
                {
                    if (this.Nome.Length > 40)
                    {
                        var NomeRetorno = this.Nome.Substring(0, 37) + "...";
                        return NomeRetorno;
                    }else
                    {

                        return this.Nome;
                    }
                }
            }

            public string NomeMaeDesc
            {
                get
                {
                    if (this.NomeMae.Length > 40)
                    {
                        var NomeRetorno = this.NomeMae.Substring(0, 37) + "...";
                        return NomeRetorno;
                    }
                    else
                    {

                        return this.NomeMae;
                    }
                }
            }

            public int idade
            {
                get
                {
                    DateTime now = DateTime.Today;
                    int age = now.Year - DtNasc.Year;
                    if (DtNasc > now.AddYears(-age))
                        age--;
                    return age;
                }
            }

            public string nireDesc
            {
                get
                {
                    string descNire = this.nire.ToString();
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

          
        }
        #endregion
        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }
    }
}

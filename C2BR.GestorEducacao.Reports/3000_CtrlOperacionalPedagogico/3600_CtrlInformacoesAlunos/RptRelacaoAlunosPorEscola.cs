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
    public partial class RptRelacaoAlunosPorEscola : C2BR.GestorEducacao.Reports.RptPaisagem
    {

        #region ctor
        public RptRelacaoAlunosPorEscola()
        {
            InitializeComponent();
        }
        #endregion
        #region Init Report

        IEnumerable<AlunosPorEscola> listAlunos;
        IEnumerable<AlunosPorEscola> listAlunos2;
        public int InitReport(string parametros,
                              int codEmp,
                              string infos,
                              string tipo, 
                              int unid,
                              string ordem  
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
                string descTipo;
                #region Query Report
                if (tipo == "Mat")
                {
                    descTipo = "Matriculado";
                }
                else if(tipo == "NMat")
                {
                    descTipo = "Não Matriculado";
                }
                else if (tipo == "PMat")
                {
                    descTipo = "Pré-Matriculado";
                }else{
                    descTipo = "Todos";
                }


                if (unid != 0)
                {
                    
                    if (tipo == "Mat" || tipo == "PMat")
                    {
                        listAlunos = (from mat in ctx.TB08_MATRCUR
                                      join alu in ctx.TB07_ALUNO on mat.CO_ALU equals alu.CO_ALU
                                      join tur in ctx.TB06_TURMAS on mat.CO_TUR equals tur.CO_TUR
                                      //join tur in ctx.TB06_TURMAS on tur.CO_TUR equals mat.CO_TUR
                                      join cad in ctx.TB129_CADTURMAS on tur.CO_TUR equals cad.CO_TUR
                                      join mod in ctx.TB44_MODULO on tur.CO_MODU_CUR equals mod.CO_MODU_CUR
                                      join cur in ctx.TB01_CURSO on tur.CO_CUR equals cur.CO_CUR
                                      join emp in ctx.TB25_EMPRESA on mat.CO_EMP_UNID_CONT equals emp.CO_EMP
                                      join emp2 in ctx.TB25_EMPRESA on alu.CO_EMP equals emp2.CO_EMP

                                      where (tipo == "PMat" ? (mat.CO_SIT_MAT == "R") : (mat.CO_SIT_MAT == "A" || mat.CO_SIT_MAT == "F")) && (alu.CO_EMP == codEmp) && (mat.CO_EMP_UNID_CONT == unid)
                                      
                                      select new AlunosPorEscola
                                      {
                                          Tipo2 = descTipo,
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
                                          nire = alu.NU_NIRE
                                      }).Distinct();
                    }
                    else if (tipo == "NMat")
                    {
                        listAlunos = (from alu in ctx.TB07_ALUNO.AsQueryable()
                                      join emp2 in ctx.TB25_EMPRESA on alu.CO_EMP equals emp2.CO_EMP
                                      join mat in ctx.TB08_MATRCUR.AsQueryable()
                                      on alu.CO_ALU equals mat.CO_ALU into tg
                                      from mat in tg.DefaultIfEmpty()
                                      where (mat == null) 
                                      select new AlunosPorEscola
                                      {
                                          Tipo2 = descTipo,
                                          Escola = emp2.sigla ,
                                          Nome = alu.NO_ALU.ToUpper(),
                                          DtNasc = alu.DT_NASC_ALU.HasValue ? alu.DT_NASC_ALU.Value : DateTime.MinValue,
                                          Sexo = alu.CO_SEXO_ALU != "" ? alu.CO_SEXO_ALU : "-",
                                          NomeMae = alu.NO_MAE_ALU != "" ? alu.NO_MAE_ALU.ToUpper() : "-",
                                          Deficiencia = alu.TP_DEF != "" ? alu.TP_DEF : "-",
                                          UnCont = "-",
                                          Modulo = "-",
                                          Serie = "-",
                                          Turma = "-",
                                          Unidade = "-",
                                          nire = alu.NU_NIRE
                                      }).Distinct(); 
                    }
                    else
                    {
                        listAlunos = (from alu in ctx.TB07_ALUNO
                                      join mat in ctx.TB08_MATRCUR on alu.CO_ALU equals mat.CO_ALU
                                      into join1
                                      from mat in join1.DefaultIfEmpty()
                                      join tur in ctx.TB06_TURMAS on mat.CO_TUR equals tur.CO_TUR
                                      into join2
                                      from tur in join2.DefaultIfEmpty()
                                      //join tur in ctx.TB06_TURMAS on tur.CO_TUR equals mat.CO_TUR
                                      join cad in ctx.TB129_CADTURMAS on tur.CO_TUR equals cad.CO_TUR
                                      into join3
                                      from cad in join3.DefaultIfEmpty()
                                      join mod in ctx.TB44_MODULO on tur.CO_MODU_CUR equals mod.CO_MODU_CUR
                                      into join4
                                      from mod in join4.DefaultIfEmpty()
                                      join cur in ctx.TB01_CURSO on tur.CO_CUR equals cur.CO_CUR
                                      into join5
                                      from cur in join5.DefaultIfEmpty()
                                      join emp in ctx.TB25_EMPRESA on mat.CO_EMP_UNID_CONT equals emp.CO_EMP
                                      into join7
                                      from emp in join7.DefaultIfEmpty()
                                      join emp2 in ctx.TB25_EMPRESA on alu.CO_EMP equals emp2.CO_EMP

                                      where (alu.CO_EMP == codEmp) && (mat.CO_EMP_UNID_CONT == unid || mat.CO_EMP_UNID_CONT == null)

                                      select new AlunosPorEscola
                                      {
                                          Tipo2 = descTipo,
                                          Escola = emp2.sigla,
                                          Nome = alu.NO_ALU.ToUpper(),
                                          DtNasc = alu.DT_NASC_ALU.HasValue ? alu.DT_NASC_ALU.Value : DateTime.MinValue,
                                          Sexo = alu.CO_SEXO_ALU != "" ? alu.CO_SEXO_ALU : "-",
                                          NomeMae = alu.NO_MAE_ALU != "" ? alu.NO_MAE_ALU.ToUpper() : "-",
                                          Deficiencia = alu.TP_DEF != "" ? alu.TP_DEF : "-",
                                          UnCont = emp.sigla,
                                          Modulo = mod.CO_SIGLA_MODU_CUR,
                                          Serie = cur.CO_SIGL_CUR != "" ? cur.CO_SIGL_CUR : "-",
                                          Turma = cad.CO_SIGLA_TURMA != "" ? cad.CO_SIGLA_TURMA : "-",
                                          Unidade = emp.sigla != "" ? emp.sigla : "-",
                                          nire = alu.NU_NIRE
                                      }).Distinct();

                    }
                }
                else if (unid == 0)
                {
                    if (tipo == "Mat" || tipo == "PMat")
                    {
                        listAlunos = (from mat in ctx.TB08_MATRCUR
                                      join alu in ctx.TB07_ALUNO on mat.CO_ALU equals alu.CO_ALU
                                      join tur in ctx.TB06_TURMAS on mat.CO_TUR equals tur.CO_TUR
                                      //join tur in ctx.TB06_TURMAS on tur.CO_TUR equals mat.CO_TUR
                                      join cad in ctx.TB129_CADTURMAS on tur.CO_TUR equals cad.CO_TUR
                                      join mod in ctx.TB44_MODULO on tur.CO_MODU_CUR equals mod.CO_MODU_CUR
                                      join cur in ctx.TB01_CURSO on tur.CO_CUR equals cur.CO_CUR
                                      join emp in ctx.TB25_EMPRESA on mat.CO_EMP_UNID_CONT equals emp.CO_EMP
                                      join emp2 in ctx.TB25_EMPRESA on alu.CO_EMP equals emp2.CO_EMP

                                      where (tipo == "PMat" ? (mat.CO_SIT_MAT == "R") : (mat.CO_SIT_MAT == "A" || mat.CO_SIT_MAT == "F")) && (alu.CO_EMP == codEmp) 

                                      
                                      select new AlunosPorEscola
                                      {
                                          Tipo2 = descTipo,
                                          Escola = emp2.sigla,
                                          Nome = alu.NO_ALU.ToUpper(),
                                          DtNasc = alu.DT_NASC_ALU.HasValue ? alu.DT_NASC_ALU.Value : DateTime.MinValue,
                                          Sexo = alu.CO_SEXO_ALU != "" ? alu.CO_SEXO_ALU : "-",
                                          NomeMae = alu.NO_MAE_ALU != "" ? alu.NO_MAE_ALU.ToUpper() : "-",
                                          Deficiencia = alu.TP_DEF != "" ? alu.TP_DEF : "-",
                                          UnCont = emp.sigla,
                                          Modulo = mod.CO_SIGLA_MODU_CUR,
                                          Serie = cur.CO_SIGL_CUR != "" ? cur.CO_SIGL_CUR : "-",
                                          Turma = cad.CO_SIGLA_TURMA != "" ? cad.CO_SIGLA_TURMA : "-",
                                          Unidade = emp.sigla != "" ? emp.sigla : "-",
                                          nire = alu.NU_NIRE
                                      }).Distinct();
                    }
                    else if (tipo == "NMat")
                    {
                        listAlunos = (from alu in ctx.TB07_ALUNO.AsEnumerable()
                                      join mat in ctx.TB08_MATRCUR.AsEnumerable()
                                      on alu.CO_ALU equals mat.CO_ALU into tg
                                      from tcheck in tg.DefaultIfEmpty()
                                      join emp2 in ctx.TB25_EMPRESA on alu.CO_EMP equals emp2.CO_EMP
                                      where (tcheck == null) 
                                      select new AlunosPorEscola
                                      {
                                          Tipo2 = descTipo,
                                          Escola = emp2.sigla,
                                          Nome = alu.NO_ALU.ToUpper(),
                                          DtNasc = alu.DT_NASC_ALU.HasValue ? alu.DT_NASC_ALU.Value : DateTime.MinValue,
                                          Sexo = alu.CO_SEXO_ALU != "" ? alu.CO_SEXO_ALU : "-",
                                          NomeMae = alu.NO_MAE_ALU != "" ? alu.NO_MAE_ALU.ToUpper() : "-",
                                          Deficiencia = alu.TP_DEF != "" ? alu.TP_DEF : "-",
                                          UnCont = "-",
                                          Modulo = "-",
                                          Serie = "-",
                                          Turma = "-",
                                          Unidade = "-",
                                          nire = alu.NU_NIRE
                                      }).Distinct();

                        listAlunos2 = (from mat in ctx.TB08_MATRCUR
                                       join alu in ctx.TB07_ALUNO on mat.CO_ALU equals alu.CO_ALU
                                       join tur in ctx.TB06_TURMAS on mat.CO_TUR equals tur.CO_TUR
                                       //join tur in ctx.TB06_TURMAS on tur.CO_TUR equals mat.CO_TUR
                                       join cad in ctx.TB129_CADTURMAS on tur.CO_TUR equals cad.CO_TUR
                                       join mod in ctx.TB44_MODULO on tur.CO_MODU_CUR equals mod.CO_MODU_CUR
                                       join cur in ctx.TB01_CURSO on tur.CO_CUR equals cur.CO_CUR
                                       join emp in ctx.TB25_EMPRESA on mat.CO_EMP_UNID_CONT equals emp.CO_EMP
                                       join emp2 in ctx.TB25_EMPRESA on alu.CO_EMP equals emp2.CO_EMP

                                       where (alu.CO_SITU_ALU != "A") && (alu.CO_EMP == codEmp) 
                                       select new AlunosPorEscola
                                       {
                                           Tipo2 = descTipo,
                                           Escola = emp2.sigla,
                                           Nome = alu.NO_ALU.ToUpper(),
                                           DtNasc = alu.DT_NASC_ALU.HasValue ? alu.DT_NASC_ALU.Value : DateTime.MinValue,
                                           Sexo = alu.CO_SEXO_ALU != "" ? alu.CO_SEXO_ALU : "-",
                                           NomeMae = alu.NO_MAE_ALU != "" ? alu.NO_MAE_ALU.ToUpper() : "-",
                                           Deficiencia = alu.TP_DEF != "" ? alu.TP_DEF : "-",
                                           UnCont = emp.sigla,
                                           Modulo = mod.CO_SIGLA_MODU_CUR,
                                           Serie = cur.CO_SIGL_CUR != "" ? cur.CO_SIGL_CUR : "-",
                                           Turma = cad.CO_SIGLA_TURMA != "" ? cad.CO_SIGLA_TURMA : "-",
                                           Unidade = emp.sigla != "" ? emp.sigla : "-",
                                           nire = alu.NU_NIRE
                                       }).Distinct();
                        if(listAlunos2 != null && listAlunos2.Count() > 0 && listAlunos2.ElementAt(0) != null)
                        {
                            listAlunos.Concat(listAlunos2);
                        }
                    }
                    else
                    {
                        listAlunos = (from alu in ctx.TB07_ALUNO
                                      join mat in ctx.TB08_MATRCUR on alu.CO_ALU equals mat.CO_ALU
                                      into join1
                                      from mat in join1.DefaultIfEmpty()
                                      join tur in ctx.TB06_TURMAS on mat.CO_TUR equals tur.CO_TUR
                                      into join2
                                      from tur in join2.DefaultIfEmpty()
                                      //join tur in ctx.TB06_TURMAS on tur.CO_TUR equals mat.CO_TUR
                                      join cad in ctx.TB129_CADTURMAS on tur.CO_TUR equals cad.CO_TUR
                                      into join3
                                      from cad in join3.DefaultIfEmpty()
                                      join mod in ctx.TB44_MODULO on tur.CO_MODU_CUR equals mod.CO_MODU_CUR
                                      into join4
                                      from mod in join4.DefaultIfEmpty()
                                      join cur in ctx.TB01_CURSO on tur.CO_CUR equals cur.CO_CUR
                                      into join5
                                      from cur in join5.DefaultIfEmpty()
                                      join emp in ctx.TB25_EMPRESA on mat.CO_EMP_UNID_CONT equals emp.CO_EMP
                                      into join7
                                      from emp in join7.DefaultIfEmpty()
                                      join emp2 in ctx.TB25_EMPRESA on alu.CO_EMP equals emp2.CO_EMP
                                      where (alu.CO_EMP == codEmp) 
                                      select new AlunosPorEscola
                                      {
                                          Tipo2 = descTipo,
                                          Escola = emp2.sigla,
                                          Nome = alu.NO_ALU.ToUpper(),
                                          DtNasc = alu.DT_NASC_ALU.HasValue ? alu.DT_NASC_ALU.Value : DateTime.MinValue,
                                          Sexo = alu.CO_SEXO_ALU != "" ? alu.CO_SEXO_ALU : "-",
                                          NomeMae = alu.NO_MAE_ALU != "" ? alu.NO_MAE_ALU.ToUpper() : "-",
                                          Deficiencia = alu.TP_DEF != "" ? alu.TP_DEF : "-",
                                          UnCont = emp.sigla,
                                          Modulo = mod.CO_SIGLA_MODU_CUR,
                                          Serie = cur.CO_SIGL_CUR != "" ? cur.CO_SIGL_CUR : "-",
                                          Turma = cad.CO_SIGLA_TURMA != "" ? cad.CO_SIGLA_TURMA : "-",
                                          Unidade = emp.sigla != "" ? emp.sigla : "-",
                                          nire = alu.NU_NIRE
                                      }).Distinct();
                    }

                }
                //var res = lst.ToList;

                #endregion

                // Erro: não encontrou registros
                if (listAlunos == null)
                    return -1;

                // Seta os dados no DataSource do Relatorio
                bsReport.Clear();

                List<AlunosPorEscola> lista = new List<AlunosPorEscola>();

                switch(ordem)
                {
                    case "nire":
                        lista = listAlunos.OrderBy(o => o.nireDesc).ToList();
                        break;
                    case "nasc":
                        lista = listAlunos.OrderBy(o => o.DataNasctoDesc).ToList();
                        break;
                    case "mae":
                        lista = listAlunos.OrderBy(o => o.NomeMaeDesc).ToList();
                        break;
                    case "nome":
                    default:
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

        public class AlunosPorEscola
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
                    return "(Unidade/Escola: " + this.Escola + " - Unidade de Contrato: "  + this.UnCont + " - Tipo: "+ this.Tipo2 + ")" ;
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

using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.Reports.Helper;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Globalization;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3030_CtrlMonitoria
{
    public partial class RptExtMonitoria : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptExtMonitoria()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(string parametros,
                        string infos,
                        int coEmp,
                        int modalidade,
                        int serieCurso,
                        int Turma,
                        int materia,
                        int professor,
                        string ano
                        )
        {


            try
            {
                #region Setar o Header e as Labels
                //DateTime ano1 = Convert.ToDateTime(ano);
                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                lblParametros.Text = parametros;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
                #region Query

                var res = (from tb188 in TB188_MONIT_CURSO_PROFE.RetornaTodosRegistros()
                           join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb188.CO_MODU_CUR equals tb44.CO_MODU_CUR
                           join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb188.CO_CUR equals tb01.CO_CUR
                           join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb188.CO_TUR equals tb129.CO_TUR
                           join coM in TB02_MATERIA.RetornaTodosRegistros() on tb188.CO_MAT equals coM.CO_MAT
                           join m in TB107_CADMATERIAS.RetornaTodosRegistros() on coM.ID_MATERIA equals m.ID_MATERIA
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb188.CO_COL equals tb03.CO_COL

                           where (modalidade != 0 ? tb44.CO_MODU_CUR == modalidade : 0 == 0)
                           &&   (serieCurso != 0 ? tb01.CO_CUR == serieCurso : 0 == 0)
                           &&   (Turma != 0 ? tb129.CO_TUR == Turma : 0 == 0)
                           &&   (tb188.CO_ANO_LET == ano)
                           &&   (materia != 0 ? coM.CO_MAT == materia : 0 == 0)
                           &&   (professor != 0 ? tb188.CO_COL == professor : 0 == 0)
                           &&   (tb03.FL_ATIVI_MONIT == "S")
                           &&   (tb03.FLA_PROFESSOR == "S")

                           select new ExtratoMonitoria
                           {
                               Professor = tb03.NO_COL,
                               professorCPF = tb03.NU_CPF_COL,
                               Materia = m.NO_MATERIA,
                               SerieCurso = tb01.NO_CUR,
                               Turma = tb129.NO_TURMA,
                               Modalidade = tb44.DE_MODU_CUR

                           }).OrderBy(m => m.Professor).ToList();

                #endregion


                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (ExtratoMonitoria at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class ExtratoMonitoria
        {
            //Informações do Aluno
            public string Professor { get; set; }
            public string professorCPF { get; set; }
            public string professorCPFValid
            {
                get
                {
                    if (string.IsNullOrEmpty(professorCPF))
                    {
                        return "***.***.***-**";
                    }

                    return professorCPF.Insert(3, ".").Insert(7, ".").Insert(11, "-");
                }
            }
            public string Materia { get; set; }
            public string Modalidade{ get; set; }
            public string SerieCurso { get; set; }
            public string Turma { get; set; }


        }
    }
}


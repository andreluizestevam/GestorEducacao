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
    public partial class RptResumoAvaliacao : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptResumoAvaliacao()
        {
            InitializeComponent();   
        }

        #region Init Report  

        public int InitReport(string parametros,
                              int coEmp,
                              string infos,
                              int strP_CO_EMP_REF,
                              string strP_CO_ANO_MES_MAT,
                              int strP_CO_MODU_CUR,
                              int strP_CO_CUR,
                              int strP_CO_TUR,
                              int strP_CO_ALU,
                              int strP_CO_MAT,
                              int strBim
                              )
        {
            try
            {
                #region Setar o Header e as Labels
                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Aluno Recuperação

                var lst = (from ha in ctx.TB079_HIST_ALUNO
                           join ma in ctx.TB02_MATERIA on ha.CO_MAT equals ma.CO_MAT into resultado3
                           from ma in resultado3.DefaultIfEmpty()
                           join al in ctx.TB07_ALUNO on ha.CO_ALU equals al.CO_ALU into resultado
                           from al in resultado.DefaultIfEmpty()
                           join t in ctx.TB06_TURMAS on ha.CO_TUR equals t.CO_TUR into resultado1
                           from t in resultado1.DefaultIfEmpty()
                           join ct in ctx.TB129_CADTURMAS on ha.CO_TUR equals ct.CO_TUR into resultado2
                           from ct in resultado2.DefaultIfEmpty()
                           where al != null && t != null && ct != null && ma != null
                           && ha.CO_MODU_CUR == strP_CO_MODU_CUR
                           && ha.CO_CUR == strP_CO_CUR
                           && ha.CO_TUR == strP_CO_TUR
                           && ha.CO_ANO_REF == strP_CO_ANO_MES_MAT.Trim()
                           && (strP_CO_ALU == -1 ? 0 == 0 : ha.CO_ALU == strP_CO_ALU)
                           && (strP_CO_MAT == -1 ? 0==0 : ha.CO_MAT == strP_CO_MAT)
                           && ha.CO_EMP == strP_CO_EMP_REF
                           select new ResumoAvaliacao
                           {
                               NomeAluno = al.NO_ALU.ToUpper(),
                               NomePeriodo = (t == null ? "" : t.CO_PERI_TUR == "M" ? "Matutino" : (t.CO_PERI_TUR == "V" ? "Vespertino" : (t.CO_PERI_TUR == "N" ? "Noturno" : ""))),
                               NomeTurma = (ct == null ? "" : ct.NO_TURMA == null ? "" : ct.NO_TURMA),
                               NomeBimestre = (strBim == 1 ? "1º Bimestre" : (strBim == 2 ? "2º Bimestre" : (strBim == 3 ? "3º Bimestre" : (strBim == 4 ? "4º Bimestre" : "")))),
                               TextoAval = (strBim == 1 ? ha.DE_RES_AVAL_BIM1 : (strBim == 2? ha.DE_RES_AVAL_BIM2:(strBim==3?ha.DE_RES_AVAL_BIM3:(strBim==4?ha.DE_RES_AVAL_BIM4:"")))),
                               coAno = strP_CO_ANO_MES_MAT,
                               coCur = strP_CO_CUR,
                               coEmp = strP_CO_EMP_REF,
                               coMat = ha.CO_MAT,
                               idMat = ma.ID_MATERIA,
                               coMod = strP_CO_MODU_CUR,
                               coTur = strP_CO_TUR,
                               tipoFlag = t.CO_FLAG_RESP_TURMA
                           }
                  );
                if (lst == null || lst.Count() <= 0)
                    return -1;
                var res = lst.Distinct().OrderBy(r => r.NomeAluno).ThenBy(r => r.NomeTurma).ToList();

                #endregion

                if (res.Count == 0)
                    return -1;
                // Seta os dados no DataSource do Relatorio
                bsReport.Clear();
                foreach (var at in res)
                    bsReport.Add(at);
                GroupHeader1.GroupFields.Add(new GroupField("coMat", XRColumnSortOrder.Ascending));
                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe resumo avaliação
        public class ResumoAvaliacao
        {
            public string NomeEmpresa { get; set; }
            public string NomeAluno { get; set; }
            public string NomeTurma { get; set; }
            public string NomePeriodo { get; set; }
            public string NomeBimestre { get; set; }
            public string TextoAval { get; set; }

            public string coAno { get; set; }
            public int coCur { get; set; }
            public int coEmp { get; set; }
            public int coMat { get; set; }
            public int coMod { get; set; }
            public int coTur { get; set; }
            public int idMat { get; set; }
            public string tipoFlag { get; set; }

            public string NomeMateria { 
                get {
                    var ctx = GestorEntities.CurrentContext;
                    var materias = (from cm in ctx.TB107_CADMATERIAS
                                    where cm.ID_MATERIA == this.idMat
                                    && cm.NO_MATERIA != null
                                    select cm.NO_MATERIA    
                                        );
                    string nomeMateria = "Matéria sem nome cadastrado";
                    if (materias != null && materias.Count() > 0 && materias.First() != null)
                        nomeMateria = materias.First().ToString();
                    return nomeMateria;
                } 
            }
            public string NomeProfessor
            {
                get
                {
                    var ctx = GestorEntities.CurrentContext;
                    int codigoAno = int.Parse(this.coAno.Trim());
                    //Busca pelo nome do professor/colaborador
                    var nomes = (
                                from rm in ctx.TB_RESPON_MATERIA
                                join co in ctx.TB03_COLABOR on rm.CO_COL_RESP equals co.CO_COL into resultado
                                from co in resultado.DefaultIfEmpty()
                                where co != null
                                && co.NO_COL != null
                                && rm.CO_CUR == this.coCur
                                && rm.CO_EMP == this.coEmp
                                && rm.CO_MODU_CUR == this.coMod
                                && rm.CO_TUR == this.coTur
                                && (this.tipoFlag == "N" ? rm.CO_MAT == this.coMat : rm.CO_MAT == null)
                                && (rm.CO_CLASS_RESP != null ? rm.CO_CLASS_RESP == "P" : 0 == 0)
                                select co.NO_COL.ToUpper()
                                );
                    //Nome do professor a ser exibido
                    string nome = "Sem professor responsável principal";
                    //Verificação do nome do professor
                    if (nomes != null && nomes.Count() > 0 && nomes.First() != null)
                        nome = nomes.First().ToString();
                    return nome;

                }
            }

            public string NomeTurmaPeriodo
            {
                get
                {
                    return this.NomeTurma + " - " + this.NomePeriodo;
                }
            }
            
        }

        #endregion

    }
}

using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos
{
    public partial class RptAlunosAniverSimples : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        #region ctor

        public RptAlunosAniverSimples()
        {
            InitializeComponent();
        }
        #endregion

        #region Init Report

        public int InitReport(string parametros,
                               int codEmp,
                               int codUndCont,
                               int codMod,
                               int codCurso,
                               int codTurma,
                               DateTime? dtInicio,
                               DateTime? dtFim,
                               string strMesRef,
                               string sexo,
                               string tpSituAluno,
                               string infos)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(codEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Alunos

                dtFim = dtFim != null ? (DateTime?)DateTime.Parse(dtFim.Value.ToString("dd/MM/yyyy") + " 23:59:59") : null;
                int intMes = strMesRef != "T" ? int.Parse(strMesRef) : 0;

                var lst = tpSituAluno == "N" || tpSituAluno == "T" ?
                          from mat in ctx.TB07_ALUNO
                          where ( mat.DT_NASC_ALU != null ? (dtInicio != null ? (mat.DT_NASC_ALU.Value >= dtInicio.Value) : 0 == 0 )
                          && (dtFim != null ? (mat.DT_NASC_ALU.Value <= dtFim.Value) : 0 == 0) : mat.DT_NASC_ALU == null)
                          && (intMes != 0 ? mat.DT_NASC_ALU.Value.Month == intMes : 0 == 0)
                          && (sexo != "T" ? mat.CO_SEXO_ALU == sexo : 0 == 0)
                          && mat.DT_NASC_ALU != null
                          select new Aluno
                          {
                              DtNascimento = mat.DT_NASC_ALU.Value,
                              Nire = mat.NU_NIRE,
                              Nome = mat.NO_ALU,
                              TelefoneResiAlu = mat.NU_TELE_RESI_ALU != null && mat.NU_TELE_RESI_ALU != "" ? mat.NU_TELE_RESI_ALU.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                              TelefoneCeluAlu = mat.NU_TELE_CELU_ALU != null && mat.NU_TELE_CELU_ALU != "" ? mat.NU_TELE_CELU_ALU.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : ""
                          }
                :
                          from mat in ctx.TB08_MATRCUR
                          join alu in ctx.TB07_ALUNO on mat.CO_ALU equals alu.CO_ALU
                          join turma in ctx.TB06_TURMAS on mat.CO_TUR equals turma.CO_TUR
                          join cur in ctx.TB01_CURSO on mat.CO_CUR equals cur.CO_CUR
                          where (alu.DT_NASC_ALU != null ? (dtInicio != null ? (alu.DT_NASC_ALU.Value >= dtInicio.Value) : 0 == 0)
                          && (dtFim != null ? (alu.DT_NASC_ALU.Value <= dtFim.Value) : 0 == 0) : alu.DT_NASC_ALU == null)
                          && (sexo != "T" ? alu.CO_SEXO_ALU == sexo : sexo == "T")
                          && (codUndCont != 0 ? mat.CO_EMP == codUndCont : codUndCont == 0)
                          && (codTurma != 0 ? turma.CO_TUR == codTurma : codTurma == 0)
                          && (codCurso != 0 ? cur.CO_CUR == codCurso : codCurso == 0)
                          && (codMod != 0 ? turma.CO_MODU_CUR == codMod : codMod == 0)
                          && alu.DT_NASC_ALU != null
                          select new Aluno
                          {
                              DtNascimento = alu.DT_NASC_ALU.Value,
                              Nire = alu.NU_NIRE,
                              Nome = alu.NO_ALU,
                              TelefoneResiAlu = alu.NU_TELE_RESI_ALU != null && alu.NU_TELE_RESI_ALU != "" ? alu.NU_TELE_RESI_ALU.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                              TelefoneCeluAlu = alu.NU_TELE_CELU_ALU != null && alu.NU_TELE_CELU_ALU != "" ? alu.NU_TELE_CELU_ALU.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : ""
                          };

                var res = lst.OrderBy(x => x.Nome).ThenBy(x => x.Nome).ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (Aluno at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Alunos do Relatorio

        public class Aluno
        {
            public int Nire { get; set; }
            public string NireDesc
            {
                get { return this.Nire.ToString().PadLeft(9, '0'); }
            }
            public string Nome { get; set; }
            public string DescNome
            {
                get { return this.Nome.ToUpper(); }
            }
            public DateTime? DtNascimento { get; set; }
            public string DataNascimento
            {
                get
                {
                    if (DtNascimento == null)
                    {
                        return "-";
                    }
                    else
                    {
                        return this.DtNascimento.Value.ToString("dd/MM/yyyy");
                    }
                    
                }
            }
            public int Idade
            {
                get { return Funcoes.GetIdade(this.DtNascimento.Value); }
            }
            public string TelefoneResiAlu { get; set; }
            public string TelefoneCeluAlu { get; set; }
        }

        #endregion

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }
    }
}

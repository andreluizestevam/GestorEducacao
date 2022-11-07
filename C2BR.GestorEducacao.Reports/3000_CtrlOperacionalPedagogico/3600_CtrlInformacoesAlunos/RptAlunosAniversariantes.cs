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
    public partial class RptAlunosAniversariantes : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        #region ctor

        public RptAlunosAniversariantes()
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
                              Sexo = mat.CO_SEXO_ALU,
                              Serie = "",
                              CoCur = mat.CO_CUR,
                              UndCont = "",
                              Modalidade = "",
                              CoMod = mat.CO_MODU_CUR,
                              Turma = "",
                              CoTur = mat.CO_TUR,
                              DataNascimento = mat.DT_NASC_ALU.Value,
                              Nire = mat.NU_NIRE,
                              Nome = mat.NO_ALU,
                              CPFResponsavel = mat.TB108_RESPONSAVEL.NU_CPF_RESP,
                              TelefoneCeluResp = mat.TB108_RESPONSAVEL.NU_TELE_CELU_RESP != null && mat.TB108_RESPONSAVEL.NU_TELE_CELU_RESP != "" ? mat.TB108_RESPONSAVEL.NU_TELE_CELU_RESP.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                              TelefoneResiAlu = mat.NU_TELE_RESI_ALU != null && mat.NU_TELE_RESI_ALU != "" ? mat.NU_TELE_RESI_ALU.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                              TelefoneCeluAlu = mat.NU_TELE_CELU_ALU != null && mat.NU_TELE_CELU_ALU != "" ? mat.NU_TELE_CELU_ALU.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                              EmailAlu = mat.NO_WEB_ALU,
                              MesNascAlu = mat.DT_NASC_ALU.Value.Month,
                              DiaNascAlu = mat.DT_NASC_ALU.Value.Day
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
                              Sexo = alu.CO_SEXO_ALU,
                              Serie = cur.CO_SIGL_CUR,
                              UndCont = mat.TB25_EMPRESA.sigla,
                              Modalidade = mat.TB44_MODULO.CO_SIGLA_MODU_CUR,
                              Turma = turma.TB129_CADTURMAS.CO_SIGLA_TURMA,
                              DataNascimento = alu.DT_NASC_ALU.Value,
                              Nire = alu.NU_NIRE,
                              Nome = alu.NO_ALU,
                              CPFResponsavel = alu.TB108_RESPONSAVEL.NU_CPF_RESP,
                              TelefoneCeluResp = alu.TB108_RESPONSAVEL.NU_TELE_CELU_RESP != null && alu.TB108_RESPONSAVEL.NU_TELE_CELU_RESP != "" ? alu.TB108_RESPONSAVEL.NU_TELE_CELU_RESP.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                              TelefoneResiAlu = alu.NU_TELE_RESI_ALU != null && alu.NU_TELE_RESI_ALU != "" ? alu.NU_TELE_RESI_ALU.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                              TelefoneCeluAlu = alu.NU_TELE_CELU_ALU != null && alu.NU_TELE_CELU_ALU != "" ? alu.NU_TELE_CELU_ALU.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                              EmailAlu = alu.NO_WEB_ALU,
                              MesNascAlu = alu.DT_NASC_ALU.Value.Month,
                              DiaNascAlu = alu.DT_NASC_ALU.Value.Day
                          };

                var res = lst.OrderBy(x => x.DiaNascAlu).ThenBy(x => x.MesNascAlu).ThenBy(x => x.Nome).ToList();

                if (tpSituAluno == "T")
                {
                    foreach (var iLst in res)
                    {
                        if (iLst.CoMod != null && iLst.CoCur != null && iLst.CoTur != null)
                        {
                            var descSerie = (from tb01 in ctx.TB01_CURSO
                                             join tb06 in ctx.TB06_TURMAS on tb01.CO_CUR equals tb06.CO_CUR
                                             join tb25 in ctx.TB25_EMPRESA on tb06.TB129_CADTURMAS.CO_EMP_UNID_CONT equals tb25.CO_EMP
                                             where tb06.CO_CUR == iLst.CoCur && tb06.TB129_CADTURMAS.CO_TUR == iLst.CoTur
                                             && tb06.CO_MODU_CUR == iLst.CoMod
                                             select new
                                             {
                                                 tb01.CO_SIGL_CUR,
                                                 tb06.TB129_CADTURMAS.CO_SIGLA_TURMA,
                                                 tb01.TB44_MODULO.CO_SIGLA_MODU_CUR,
                                                 tb25.sigla
                                             }).FirstOrDefault();

                            if (descSerie != null)
                            {
                                iLst.Serie = descSerie.CO_SIGL_CUR;
                                iLst.Turma = descSerie.CO_SIGLA_TURMA;
                                iLst.Modalidade = descSerie.CO_SIGLA_MODU_CUR;
                                iLst.UndCont = descSerie.sigla;
                            }
                        }
                    }
                }

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
            public string Serie { get; set; }
            public DateTime DataNascimento { get; set; }
            public int MesNascAlu { get; set; }
            public int DiaNascAlu { get; set; }
            public int? CoMod { get; set; }
            public int? CoCur { get; set; }
            public int? CoTur { get; set; }
            public string Nome { get; set; }
            public string CPFResponsavel { get; set; }
            public string Sexo { get; set; }
            public string UndCont { get; set; }
            public string Modalidade { get; set; }
            public string Turma { get; set; }
            public int Nire { get; set; }
            public string TelefoneResiAlu { get; set; }
            public string TelefoneCeluAlu { get; set; }
            public string EmailAlu { get; set; }
            public string TelefoneCeluResp { get; set; }

            public string DescMesNascAlu
            {
                get
                {
                    if (this.MesNascAlu == 1)
                        return "JANEIRO";
                    else if (this.MesNascAlu == 2)
                        return "FEVEREIRO";
                    else if (this.MesNascAlu == 3)
                        return "MARÇO";
                    else if (this.MesNascAlu == 4)
                        return "ABRIL";
                    else if (this.MesNascAlu == 5)
                        return "MAIO";
                    else if (this.MesNascAlu == 6)
                        return "JUNHO";
                    else if (this.MesNascAlu == 7)
                        return "JULHO";
                    else if (this.MesNascAlu == 8)
                        return "AGOSTO";
                    else if (this.MesNascAlu == 9)
                        return "SETEMBRO";
                    else if (this.MesNascAlu == 10)
                        return "OUTUBRO";
                    else if (this.MesNascAlu == 11)
                        return "NOVEMBRO";
                    else
                        return "DEZEMBRO";
                }
            }

            public int Idade
            {
                get { return Funcoes.GetIdade(this.DataNascimento); }
            }

            public string NireDesc
            {
                get { return this.Nire.ToString().PadLeft(9, '0'); }
            }

            public string CPFRespDesc
            {
                get { return Funcoes.Format(this.CPFResponsavel, TipoFormat.CPF); }
            }

            public string DescNome
            {
                get { return this.Nome.ToUpper(); }
            }
        }

        #endregion

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }
    }
}

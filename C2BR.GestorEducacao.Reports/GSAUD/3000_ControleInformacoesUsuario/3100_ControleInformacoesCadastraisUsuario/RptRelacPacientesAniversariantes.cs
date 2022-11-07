using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario
{
    public partial class RptRelacPacientesAniversariantes : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptRelacPacientesAniversariantes()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                               int codEmp,
                               int Unidade,
                               DateTime? dtInicio,
                               DateTime? dtFim,
                               string strMesRef,
                               string sexo,
                               string infos,
             string NomeRelatorio
             )
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;
                if (NomeRelatorio == "")
                {
                    lblTitulo.Text = "---";
                }
                else
                {
                    lblTitulo.Text = NomeRelatorio.ToUpper();
                }

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

                var lst = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where tb07.DT_NASC_ALU.Value >= dtInicio && tb07.DT_NASC_ALU.Value <= dtFim
                           &&  (Unidade != 0 ? tb07.CO_EMP == Unidade : 0 == 0)
                           && (sexo != "T" ? tb07.CO_SEXO_ALU == sexo : 0 == 0)
                           && (intMes != 0 ? tb07.DT_NASC_ALU.Value.Month == intMes : 0 == 0)
                           select new Paciente
                             {
                                 Sexo = tb07.CO_SEXO_ALU,
                                 MesNascAlu = tb07.DT_NASC_ALU.Value.Month,
                                 Unidade = tb07.TB25_EMPRESA.sigla,
                                 DataNascimento = tb07.DT_NASC_ALU,
                                 EmailAlu = tb07.NO_WEB_ALU == "" ? "---" : tb07.NO_WEB_ALU == null ? "---" : tb07.NO_WEB_ALU,
                                 paciente = tb07.NO_ALU,
                                 Neri = tb07.NU_NIRE,
                                 Celular = tb07.NU_TELE_CELU_ALU != null && tb07.NU_TELE_CELU_ALU != "" ? tb07.NU_TELE_CELU_ALU.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                             }).ToList();
                var res = lst.ToList();


                #endregion
                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (Paciente at in res)
                    bsReport.Add(at);
                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Alunos do Relatorio

        public class Paciente
        {
            public string Unidade { get; set; }
            public string Serie { get; set; }
            public DateTime? DataNascimento { get; set; }
            public string Sexo { get; set; }
            public string EmailAlu { get; set; }
            public string Celular { get; set; }
            public string paciente { get; set; }
            public int Neri { get; set; }
            public int MesNascAlu { get; set; }
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
                get { return Funcoes.GetIdade(this.DataNascimento.Value); }
            }

            public string PacienteNeri
            {
                get
                {

                    return Convert.ToString(this.Neri).PadLeft(7, '0') + " - " + this.paciente;

                }
            }


        }

        #endregion
    }
}

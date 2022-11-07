using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;
using System.Globalization;

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario
{
    public partial class RptProntConvencional : C2BR.GestorEducacao.Reports.RptRetratoCentro
    {
        public RptProntConvencional()
        {
            InitializeComponent();
        }

        public int InitReport(
              int coAlu,
              List<int> idPront,
              string infos,
              int coEmp,
              int qualificacao,
              DateTime? dtIni,
              DateTime? dtFim
            )
        {
            try
            {
                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                var at = new Prontuario();
                var lDesc = new List<Desc>();
                foreach (var id in idPront)
                {
                    var it = TBS400_PRONT_MASTER.RetornaPelaChavePrimaria(id);
                    var texto = new Desc();

                    it.TB14_DEPTOReference.Load();
                    it.TBS418_CLASS_PRONTReference.Load();
                    string qual = it.TBS418_CLASS_PRONT != null ? it.TBS418_CLASS_PRONT.NO_CLASS_PRONT : "Sem Qualificação";

                    var tb03 = TB03_COLABOR.RetornaPeloCoCol((it.CO_COL.HasValue ? it.CO_COL.Value : it.CO_COL_CADAS));

                    
                    texto.cabecalho = it.DT_CADAS.ToString("dd/MM/yyyy HH:mm") + "  -  " + tb03.NO_APEL_COL + "  " + tb03.CO_SIGLA_ENTID_PROFI + " " + tb03.NU_ENTID_PROFI + "/" + tb03.CO_UF_ENTID_PROFI + " - " + (it.TB14_DEPTO != null ? "" + it.TB14_DEPTO.CO_SIGLA_DEPTO : "");
                    texto.desc = qual + ": " + it.ANAMNSE.Replace("<BR>", "\n");
                    lDesc.Add(texto);
                }

                if (lDesc != null)
                {
                    at.DesPront = lDesc;
                }

                var tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);

                if (tb07 != null)
                {
                    at.Paciente = tb07.NO_ALU;
                    at.Nascimento = tb07.DT_NASC_ALU.HasValue ? tb07.DT_NASC_ALU.Value : new DateTime(1900, 1, 1);
                    at.SexoPac = !String.IsNullOrEmpty(tb07.CO_SEXO_ALU) ? tb07.CO_SEXO_ALU == "F" ? "Feminino" : "Masculino" : "-";
                }

                //Atribui algumas informações à label's no relatório
                this.lblInfosDia.Text = "Brasília, " + DateTime.Now.ToLongDateString();

                //Adiciona ao DataSource do Relatório
                bsReport.Clear();
                bsReport.Add(at);

                return 1;

            }
            catch { return 0; }
        }

        public class Prontuario
        {
            public string Paciente { get; set; }
            public DateTime Nascimento { get; set; }
            public string NascPac
            {
                get
                {
                    return Nascimento.ToShortDateString();
                }
            }
            public string SexoPac { get; set; }
            public string idadePac
            {
                get
                {
                    return Funcoes.FormataDataNascimento(Nascimento);
                }
            }

            public List<Desc> DesPront { get; set; }

            public string nomeCidade { get; set; }
            public string nomeUF { get; set; }
        }

        public class Desc
        {
            public string cabecalho { get; set; }
            public string desc { get; set; }
        }
    }
}

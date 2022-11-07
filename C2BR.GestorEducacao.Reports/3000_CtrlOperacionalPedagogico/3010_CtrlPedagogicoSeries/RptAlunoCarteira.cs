using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3010_CtrlPedagogicoSeries
{
    public partial class RptAlunoCarteira : DevExpress.XtraReports.UI.XtraReport
    {
        public RptAlunoCarteira()
        {
            InitializeComponent();
        }

        public int InitReport(int str_CURSO, int strNIRE)
        {

            var ctx = GestorEntities.CurrentContext;

            var lst = (from  mat in ctx.TB08_MATRCUR
                       from cur in mat.TB44_MODULO.TB01_CURSO
                       where mat.TB07_ALUNO.NU_NIRE == strNIRE
                       && cur.CO_CUR == str_CURSO //mat.TB07_ALUNO.Image != null
                       select new Carteira
                       {
                           NomeAluno = mat.TB07_ALUNO.NO_ALU,
                           NIRE = mat.TB07_ALUNO.NU_NIRE,
                           Serie = cur.NO_CUR,
                           DtNasc = mat.TB07_ALUNO.DT_NASC_ALU,
                           FotoAluno = mat.TB07_ALUNO.Image != null ? mat.TB07_ALUNO.Image.ImageStream : null,
                           Autorizado = mat.TB07_ALUNO.FL_AUTORI_SAIDA,
                           DtValid = "31/12/2013"
                       });

            // Instancia o contexto
            var res = lst.ToList().LastOrDefault();


            // Erro: não encontrou registros
            if (res == null)
                return -1;

            // Seta os alunos no DataSource do Relatorio
            bsReport.Clear();
            MudaHeader(res.Autorizado);
            bsReport.Add(res);
            /*foreach (Carteira at in res)
            {
                MudaHeader(at.Autorizado);
                bsReport.Add(at);
            }*/
            this.UpdateLayout();
            return 1;
        }

        public void MudaHeader(string autoriza)
        {
            imgAssinatura.Image = null;
            imgAssinatura.ImageUrl = "/Library/IMG/Carteira/footer_Assinatura.png";
            if (autoriza == "S")
            {
                imgVerde.Visible = true;
                imgVerde.BringToFront();
                imgVerde.Image = null;
                imgVerde.ImageUrl = "/Library/IMG/Carteira/header_Green.png";
                imgVermelha.Visible = false;
                imgVermelha.SendToBack();
                
            }
            else
            {
                imgVermelha.Visible = true;
                imgVermelha.BringToFront();
                imgVermelha.Image = null;
                imgVermelha.ImageUrl = "/Library/IMG/Carteira/header_Red.png";
                imgVerde.Visible = false;
                imgVerde.SendToBack();
            }            
        }
    }

    public class Carteira
    {
        public string NomeAluno { get; set; }
        public DateTime? DtNasc { get; set; }
        public byte[] FotoAluno { get; set; }
        public string Serie { get; set; }
        public int NIRE { get; set; }
        public string DtValid { get; set; }
        public string Autorizado { get; set; }
    }


}


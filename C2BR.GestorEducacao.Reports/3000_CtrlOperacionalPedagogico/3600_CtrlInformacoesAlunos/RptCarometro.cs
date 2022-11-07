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
using System.Data.SqlClient;
using System.IO;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos
{
    public partial class RptCarometro : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptCarometro()
        {
            InitializeComponent();
        }

        #region InitReport
        public int InitReport(
                                string infos,
                                string parametros,
                                string ano,
                                string situacao,
                                int coEmp,
                                int coMod,
                                int coCur,
                                int coTur
                             )
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;
                this.VisiblePageHeader = true;
                this.VisibleNumeroPage = false;
                this.VisibleDataHeader = false;
                this.VisibleHoraHeader = false;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;
                string cpfCNPJUnid = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp).CO_CPFCGC_EMP;
                // Setar o header do relatorio
                if ((cpfCNPJUnid == "15280144000116") || (cpfCNPJUnid == "03946574000145"))
                    this.BaseInit(header, ETipoCabecalho.ColegioSupremo);
                else // Setar o header do relatorio
                    this.BaseInit(header);


                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                // Retorna os alunos com os dados das matrículas
                var res = (from tb07 in ctx.TB07_ALUNO
                           join tb08 in ctx.TB08_MATRCUR on tb07.CO_ALU equals tb08.CO_ALU                      
                           where coEmp != 0 ? tb08.CO_EMP == coEmp : 0 == 0
                           && ano != "" ? tb08.CO_ANO_MES_MAT == ano : 0 == 0
                           && coMod != 0 ? tb08.TB44_MODULO.CO_MODU_CUR == coMod : 0 == 0
                           && coCur != 0 ? tb08.CO_CUR == coCur : 0 == 0
                           && coTur != 0 ? tb08.CO_TUR == coTur : 0 == 0
                           && (situacao != "0" ? tb08.CO_SIT_MAT == situacao : 0 == 0)
                           select new RPTCAROMETRO
                           {
                               noAlu = tb07.NO_ALU,
                               nuNire = tb07.NU_NIRE,
                               foto = tb07.Image.ImageStream
                           }).OrderBy(o => o.noAlu).ToList();

                // Valida se a consulta retornou algum aluno
                if (res.Count < 1)
                {
                    return -1;
                }

                // Variável que conta a quantidade de alunos
                int i = 1;
                // Passa por cada aluno
                foreach (RPTCAROMETRO rc in res)
                {
                    if (i <= 40)
                    {
                        // Valida se o aluno possui foto
                        if (rc.foto != null)
                        {
                            // Carrega a foto no objeto PictureBox
                            ((XRPictureBox)this.FindControl("pb" + i.ToString().PadLeft(2, '0'), false)).Image = System.Drawing.Image.FromStream(new MemoryStream(rc.foto));
                        }
                        // Deixa o PictureBox visível
                        ((XRPictureBox)this.FindControl("pb" + i.ToString().PadLeft(2, '0'), false)).Visible = true;

                        // Carrega o nome do aluno no objeto Label
                        ((XRLabel)this.FindControl("lblNom" + i.ToString().PadLeft(2, '0'), false)).Text = rc.noAlu;
                        // Altera a fonte e o tamanho da fonte do label
                        ((XRLabel)this.FindControl("lblNom" + i.ToString().PadLeft(2, '0'), false)).Font = new Font("Arial", 7F);
                        // Deixa o label visível
                        ((XRLabel)this.FindControl("lblNom" + i.ToString().PadLeft(2, '0'), false)).Visible = true;

                        // Carrega o nire do aluno no objeto label
                        ((XRLabel)this.FindControl("lblNir" + i.ToString().PadLeft(2, '0'), false)).Text = rc.Nire;
                        // Altera a fonte e o tamanho da fonte do label
                        ((XRLabel)this.FindControl("lblNir" + i.ToString().PadLeft(2, '0'), false)).Font = new Font("Arial", 10F);
                        // Deixa o label visível
                        ((XRLabel)this.FindControl("lblNir" + i.ToString().PadLeft(2, '0'), false)).Visible = true;
                        i++;
                    }
                    else
                    {
                        continue;
                    }
                }

                return 1;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region Classe de retorno do relatório

        public class RPTCAROMETRO
        {
            public string noAlu { get; set; }
            public int nuNire { get; set; }
            public string Nire
            {
                get
                {
                    return this.nuNire.ToString().PadLeft(7, '0');
                }
            }
            public byte[]  foto { get; set; }
        }

        #endregion
    }
}

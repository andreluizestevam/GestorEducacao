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



namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3030_CtrlMonitoria._3031_RptAgendaMonitoria
{
    public partial class RptRelacaoDeMonitores : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptRelacaoDeMonitores()
        {
            InitializeComponent();
        }

        public int InitReport(int codEmp, int codEmpRef, string infos, string parametros, int Cod_Espc)
        {
            try
            {
                #region Inicializa o header/Labels

                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Cria o header a partir do cod da instituicao
                var header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(codEmp);
                
                if (header == null)
                    return -1;

                // Inicializa o headero
                base.BaseInit(header);


                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

             


                var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb03.CO_EMP equals tb25.CO_EMP
                           join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals tb63.CO_ESPECIALIDADE into l1
                           from lid in l1.DefaultIfEmpty()

                           where (codEmp != 0 ? tb03.CO_EMP == codEmp : 0 == 0)
                           &&
                              (Cod_Espc != 0 ? lid.CO_ESPEC == Cod_Espc : 0 == 0)
                           &&
                              (tb03.FL_ATIVI_MONIT == "S")


                           select new Monitores
                           {
                                Matricula =tb03.CO_MAT_COL,
                                Nome = tb03.NO_COL,
                                Sigla = tb25.sigla,
                                NomeUnidade =tb25.NO_FANTAS_EMP,
                                Especialidade = lid.NO_ESPECIALIDADE
                                





                           });





                if (res.Count() == 0)
                    return -1;

                //Erro: não encontrou registros


                // Adiciona ao DataSource do Relatório
                bsReport.Clear();

                foreach (Monitores at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

    }

    public class Monitores
    {
        public string Matricula { get; set; }
        public String Nome { get; set; }
        public string Sigla { get; set; }
        public string NomeUnidade { get; set; }
        public string Especialidade { get; set; }


        public String NomeSiglaUnidade
        {
            get
            {
                if ((this.NomeUnidade != null) && (this.Sigla != null))
                {
                    String retorno = String.Format("{0} - {1}", this.Sigla, this.NomeUnidade);
                    return retorno;
                }
                else
                {
                    return "Unidade não cadastrada";
                }

            }

        }
    }


}

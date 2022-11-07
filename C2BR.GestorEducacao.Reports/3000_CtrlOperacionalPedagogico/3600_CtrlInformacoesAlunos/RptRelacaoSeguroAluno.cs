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
    public partial class RptRelacaoSeguroAluno : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptRelacaoSeguroAluno()
        {
            InitializeComponent();
        }

        public int InitReport(string parametros,
                               int codEmp,
                               int codPadrao,
                               int coMod,
                               int coSer,
                               int coTur,
                               string coAno,
                               string infos,
                               bool cabecalho)
        {
            try
            {

                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(codEmp > 0 ? codEmp : codPadrao);
                if (header == null)
                    return 0;

                this.VisibleDataHeader = cabecalho;
                this.VisibleHoraHeader = cabecalho;
                this.VisibleNumeroPage = cabecalho;
                this.VisiblePageHeader = cabecalho;

                // Setar o header do relatorio
                this.BaseInit(header);

                mostrarCabecalho(cabecalho);

                #endregion

                #region Query
                var lst = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                           join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                           join tb06 in TB06_TURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb06.CO_TUR
                           where
                           ((codEmp != 0) ? tb08.CO_EMP == codEmp : true)
                           && ((coAno != "0") ? tb08.CO_ANO_MES_MAT == coAno : true)
                           && ((coMod != 0) ? tb08.TB44_MODULO.CO_MODU_CUR == coMod : true)
                           && ((coSer != 0) ? tb08.CO_CUR == coSer : true)
                           && ((coTur != 0) ? tb08.CO_TUR == coTur : true)
                           && (tb08.CO_SIT_MAT != null)
                           select new listaAlunos
                           {
                               nome = tb08.TB07_ALUNO.NO_ALU,
                               coSexo = tb08.TB07_ALUNO.CO_SEXO_ALU,
                               coIdentificador = tb08.TB07_ALUNO.NU_NIRE,
                               modalidade = tb08.TB44_MODULO.DE_MODU_CUR,
                               serie = tb01.NO_CUR,
                               turno = tb06.CO_PERI_TUR,
                               dataNascimento = tb08.TB07_ALUNO.DT_NASC_ALU
                           }).OrderBy(o => o.modalidade).ThenBy(t => t.serie).ThenBy(t => t.nome).ToList();
                #endregion

                if (lst == null || lst.Count() == 0)
                    return -1;

                foreach (var linha in lst)
                    bsReport.Add(linha);

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        #region Metodos personalizados

        private void mostrarCabecalho(bool mostrar)
        {
            lblTitulo.Visible = mostrar;
            lblParametros.Visible = mostrar;


        }

        #endregion

        #region Classe

        public class listaAlunos
        {
            public string nome { get; set; }
            public string tipo
            {
                get
                {
                    return "Aluno";
                }
            }
            public string coSexo
            {
                set
                {
                    this.sexo = (value.Trim().ToUpper() == "M" ? "Masculino" : (value.Trim().ToUpper() == "F" ? "Feminino" : "Não informado"));
                }
            }
            public string sexo { get; set; }
            public DateTime? dataNascimento { get; set; }
            public int coIdentificador
            {
                set
                {
                    this.identificador = value.ToString("0000000000");
                }
            }
            public string identificador { get; set; }
            public string modalidade { get; set; }
            public string serie { get; set; }
            public string turno { get; set; }
        }

        #endregion

    }
}

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


namespace C2BR.GestorEducacao.Reports.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional
{
    public partial class RptExtratoPlantao : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptExtratoPlantao()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                              string infos, 
                              int codEmp,
                              int codEmpRef,
                              string situacao,
                              int coDepto,
                              int coEspec,
                              int Ordenacao
            )
        {
            try
            {
                #region Inicializa o header/Labels

                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Cria o header a partir do cod da instituicao
                var header = ReportHeader.GetHeaderFromEmpresa(codEmp);
                if (header == null)
                    return -1;

                // Inicializa o headero
                base.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                var res = (from tb153 in TB153_TIPO_PLANT.RetornaTodosRegistros()
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb153.TB25_EMPRESA.CO_EMP equals tb25.CO_EMP
                           join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tb153.TB14_DEPTO.CO_DEPTO equals tb14.CO_DEPTO into ld
                           from ldept in ld.DefaultIfEmpty()
                           join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb153.CO_ESPEC equals tb63.CO_ESPECIALIDADE into le
                           from lespe in le.DefaultIfEmpty()

                           where (codEmp != 0 ? tb153.TB25_EMPRESA.CO_EMP == codEmp : codEmp == 0)
                           && (situacao != "0" ? tb153.CO_SITUA_TIPO_PLANT == situacao : situacao == "0")
                           && (coDepto != 0 ? tb153.TB14_DEPTO.CO_DEPTO == coDepto : 0 == 0)
                           && (coEspec != 0 ? tb153.CO_ESPEC == coEspec : 0 == 0)

                           select new ExtratoPlantao
                           {
                               tpPlantao_R = tb153.NO_TIPO_PLANT,
                               Sigla_R = tb153.CO_SIGLA_TIPO_PLANT,
                               SiglaUnidade = tb25.sigla,
                               QtHoras_R = tb153.QT_HORAS,
                               HoraInicial = tb153.HR_INI_TIPO_PLANT,
                               situacao = tb153.CO_SITUA_TIPO_PLANT,
                               Especialidade_R = lespe.NO_SIGLA_ESPECIALIDADE,
                               Local_R = ldept.CO_SIGLA_DEPTO,
                           }).ToList();

                //Ordena o relatório de acordo com o escolhido pelo usuário
                switch (Ordenacao)
                {
                    case 1:
                        res = res.OrderBy(o => o.tpPlantao_R).ToList();
                        break;
                    case 2:
                        res = res.OrderBy(p => p.SiglaUnidade).ThenBy(o => o.Local_R).ToList();
                        break;
                    case 3:
                        res = res.OrderBy(p => p.Especialidade_R).ToList();
                        break;
                }

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Adiciona ao DataSource do Relatório
                bsReport.Clear();

                foreach (ExtratoPlantao at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class ExtratoPlantao
        {
            public string SiglaUnidade { get; set; }
            public string tpPlantao_R { get; set; }
            public string tpPlantao
            {
                get
                {
                    return this.tpPlantao_R.ToUpper();
                }
            }
            public string Sigla_R { get; set; }
            public string Sigla
            {
                get
                {
                    return this.Sigla_R.ToUpper();
                }
            }
            public int QtHoras_R { get; set; }
            public string QtHoras
            {
                get
                {
                    return this.QtHoras_R.ToString().PadLeft(2, '0');
                }
            }
            public  string HoraInicial { get; set; }
            public string Local_R { get; set; }
            public string Local
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.Local_R) ? this.Local_R.ToUpper() : " - ");
                }
            }
            public string Especialidade_R { get; set; }
            public string Especialidade
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.Especialidade_R) ? this.Especialidade_R.ToUpper() : " - ");
                }
            }
            public string situacao { get; set; }
            public string Situacao_Valid
            {
                get {
                    if (situacao == "A")
                    {
                        situacao = "Ativo";
                    }
                    else
                    {
                        situacao = "Inativo";
                    }

                    return situacao;
                }
            }
        }
    }
}

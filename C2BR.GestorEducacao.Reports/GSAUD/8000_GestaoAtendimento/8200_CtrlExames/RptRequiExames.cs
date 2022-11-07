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

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8200_CtrlExames
{
    public partial class RptRequiExames : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptRequiExames()
        {
            InitializeComponent();
        }
        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                              int coUnid,
                              int coExame,
                              string dataIni,
                              string dataFim,
                              string no_relatorio
                              
            )
        {
            try
            {
                #region Inicializa o header/Labels

                DateTime dataIni1;
                if (!DateTime.TryParse(dataIni, out dataIni1))
                {
                    return 0;
                }

                DateTime dataFim1;
                if (!DateTime.TryParse(dataFim, out dataFim1))
                {
                    return 0;
                }

                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Cria o header a partir do cod da instituicao
                var header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return -1;

                // Inicializa o headero
                base.BaseInit(header);

                #endregion
                DateTime DataInical = Convert.ToDateTime(dataIni);
                DateTime DataFinal = Convert.ToDateTime(dataFim);
                //Seta o título dinamicamente inserindo um * caso não exista título definido de forma que apenas emitindo o relatório é possível saber se é dinamico ou não
                this.lblTitulo.Text = (!string.IsNullOrEmpty(no_relatorio) ? no_relatorio : "EXTRATO DE REQUISICAO DE EXAMES *");

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
                var res = (from tbs218 in TBS218_EXAME_MEDICO.RetornaTodosRegistros()
                           join tbs319 in TBS219_ATEND_MEDIC.RetornaTodosRegistros() on tbs218.ID_ATEND_MEDIC equals tbs319.ID_ATEND_MEDIC  //TBS219_ATEND_MEDIC. TBS340_CAMPSAUDE_COLABOR.RetornaTodosRegistros() on tbs339.ID_CAMPAN equals tbs340.TBS339_CAMPSAUDE.ID_CAMPAN
                           join tbs07 in TB07_ALUNO.RetornaTodosRegistros() on tbs319.CO_ALU equals tbs07.CO_ALU
                           join tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs218.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE equals tbs356.ID_PROC_MEDI_PROCE
                           where (tbs218.DT_EXAME >= DataInical && tbs218.DT_EXAME <= DataFinal)                         
                           select new RequisicaoExames
                           {
                               ////Dados Exames
                               NO_EXAME = tbs356.NM_PROC_MEDI,
                               co_exame = tbs356.CO_PROC_MEDI,
                               ID_EXAME = tbs218.ID_EXAME,
                               DT_REQUI = tbs218.DT_EXAME,
                               // //Dados do Paciente
                               nis = tbs07.NU_NIS,
                               NO_PAC = tbs07.NO_ALU,
                               DT_NASC = tbs07.DT_NASC_ALU,
                               NU_TEL = tbs07.NU_TELE_CELU_ALU,
                               logradouro = tbs07.DE_ENDE_ALU,
                               Cidade = tbs07.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                               Uf = tbs07.CO_ESTA_ALU,

                               FL_REQUER_AUTO = tbs356.FL_AUTO_PROC_MEDI,
                           }).OrderBy(w => w.NO_EXAME).OrderBy(y => y.NO_EXAME).ToList();

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Adiciona ao DataSource do Relatório
                bsReport.Clear();


                foreach (RequisicaoExames at in res)
                {
                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class RequisicaoExames
        {
            //Dados Exames
            public int ID_EXAME { get; set; }
            public string NO_EXAME { get; set; }
            public string co_exame { get; set; }
            public string EXAME_V
            {
                get
                {
                    return (this.co_exame + " - " + this.NO_EXAME);
                }
            }

            //Dados do Paciente
            public string logradouro { get; set; }
            public string Cidade { get; set; }
            public string Uf { get; set; }
            public string Endereco
            {
                get
                {
                    if (logradouro == null || Cidade == null || Uf == null)
                    {
                        return "-";

                    }
                    else
                    {
                        return (this.logradouro + " - " + this.Cidade + " - " + this.Uf);
                    }
                   
                }
            }
            public decimal? nis { get; set; }

            public string NIS_V 
            {
                get {

                    return (this.nis.HasValue ? this.nis.ToString().PadLeft(9, '0') : "-"); 
                        
                }
            
            }
            public string NO_PAC { get; set; }
            public string NO_PAC_V 
            {
                get {

                    return (this.NO_PAC.Length > 20 ? this.NO_PAC.Substring(0, 20) + "..." : this.NO_PAC);
                }
            
            }
            public DateTime? DT_NASC { get; set; }
            public DateTime? DT_REQUI { get; set; }
            public string DT_REQUI_V 
            {

                get {
                    return (this.DT_REQUI.HasValue ? this.DT_REQUI.Value.ToString("dd/MM/yy") + " " + this.DT_REQUI.Value.ToString("HH:mm") : "-");             
                }
            }
           
            public string DT_NASC_V
            {
                get
                {
                    return (this.DT_NASC.HasValue ? this.DT_NASC.Value.ToString("dd/MM/yyyy") : " - ");
                }
            }
            public string NU_TEL { get; set; }
            public string NU_TEL_V
            {
                get
                {
                    return (Funcoes.Format(this.NU_TEL, TipoFormat.Telefone));
                }
            }

            public string FL_REQUER_AUTO { get; set; }
            public string DT_APROV_EXAME
            {
                get
                {
                    if(this.FL_REQUER_AUTO == "S")
                    {
                        var res = (from tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                                   where tbs350.ID_ITEM_ENCAM == this.ID_EXAME
                                   select new { tbs350.FL_APROV_ENCAM, tbs350.DT_ALTER_ENCAM, tbs350.DT_SOLIC_ENCAM }).FirstOrDefault();
                        if (res != null)
                        {
                            if (res.FL_APROV_ENCAM == "S")
                            {
                                return " em " + (res.DT_ALTER_ENCAM.HasValue ? res.DT_ALTER_ENCAM.Value.ToString("dd/MM/yy") + " - " + res.DT_ALTER_ENCAM.Value.ToString("HH:mm") : res.DT_SOLIC_ENCAM.ToString("dd/MM/yy") + " - " + res.DT_SOLIC_ENCAM.ToString("HH:mm"));
                            }
                            else
                            {
                                return "-";
                            }
                        }
                        else
                        {
                            return "-";
                        }
                        
                    }
                    else
                    {
                        return this.DT_REQUI_V;
                    }
                   




                }

            }
        }
    }
}


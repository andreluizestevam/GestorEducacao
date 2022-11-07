using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Text.RegularExpressions;
using C2BR.GestorEducacao.Reports.Helper;
using System.Globalization;

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario
{
    public partial class RptExtratoFinanceiroAtendimento : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptExtratoFinanceiroAtendimento()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                        string infos,
                        int coEmp,
                        int co_unid,
                        int co_espec,
                        int co_alu,
                        string dataIni,
                        string dataFim,
                        int idAtend
                        )
        {


            try
            {
                #region Setar o Header e as Labels


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

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                lblParametros.Text = parametros;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
                #region Query

                var res = (from tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs219.CO_ALU equals tb07.CO_ALU
                           //Coleta Consultas caso haja
                           join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tbs219.TBS174_AGEND_HORAR.ID_AGEND_HORAR equals tbs174.ID_AGEND_HORAR into lcon
                           from lconsul in lcon.DefaultIfEmpty()

                           //Coleta Acolhimento caso haja
                           join tbs194 in TBS194_PRE_ATEND.RetornaTodosRegistros() on tbs219.TBS194_PRE_ATEND.ID_PRE_ATEND equals tbs194.ID_PRE_ATEND into laco
                           from lacolhi in laco.DefaultIfEmpty()

                           //Coleta Direcionamento caso haja
                           join tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros() on tbs219.ID_ENCAM_MEDIC equals tbs195.ID_ENCAM_MEDIC into lenc
                           from lencamin in lenc.DefaultIfEmpty()

                           where (co_unid != 0 ? tbs219.CO_EMP == co_unid : 0 == 0)
                           && (co_alu != 0 ? tbs219.CO_ALU == co_alu : 0 == 0)
                           && (co_espec != 0 ? tbs219.CO_ESPECIALIDADE == co_espec : 0 == 0)
                           && (tbs219.DT_ATEND_MEDIC >= dataIni1)
                           && (tbs219.DT_ATEND_MEDIC <= dataFim1)
                           && (idAtend != 0 ? tbs219.ID_ATEND_MEDIC == idAtend : 0 == 0)
                           select new ExtratoFinanceiroAtendimento
                           {
                               ID_ATEND_MEDIC = tbs219.ID_ATEND_MEDIC,

                               //Informações do Paciente
                               Paciente = tb07.NO_ALU,
                               NirePaci = tb07.NU_NIRE,
                               sexo = tb07.CO_SEXO_ALU,
                               dt_nasc = tb07.DT_NASC_ALU,

                               //Informações da Consulta
                               DATA_CONSULTA = (lconsul != null ? lconsul.DT_AGEND_HORAR : (DateTime?)null),
                               NU_CONSULTA = (lconsul != null ? lconsul.NU_REGIS_CONSUL : ""),
                               CO_PROFI_CONSULTA = lconsul.CO_COL,
                               CO_USUAR_CONSULTA = lconsul.CO_COL_SITUA,

                               //Informações do Acolhimento
                               DATA_ACOLHIMENTO = (lacolhi != null ? lacolhi.DT_PRE_ATEND : (DateTime?)null),
                               NU_ACOLHIMENTO = (lacolhi != null ? lacolhi.CO_PRE_ATEND : ""),
                               CO_PROFI_ACOLHIMENTO = lacolhi.CO_COL_FUNC,

                               //Informações do Direcionamento
                               DATA_DIRECIONAMENTO = (lencamin != null ? lencamin.DT_CADAS_ENCAM : (DateTime?)null),
                               NU_DIRECIONAMENTO = (lencamin != null ? lencamin.CO_ENCAM_MEDIC : ""),
                               CO_PROFI_DIRECIONAMENTO = lencamin.CO_COL,
                               CO_USUAR_DIRECIONAMENTO = lencamin.CO_COL_REAL_ENCAM,

                               //Informações do Atendimento
                               DATA_ATENDIMENTO = tbs219.DT_ATEND_CADAS,
                               NU_ATENDIMENTO = tbs219.CO_ATEND_MEDIC,
                               CO_PROFI_ATENDIMENTO = tbs219.CO_COL,
                               CO_USUAR_ATENDIMENTO = tbs219.CO_COL_CADAS,

                               QT_UNIT = 1,
                           }).OrderByDescending(w => w.DATA_ATENDIMENTO).ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();

                //Variáveis usadas no Relatório
                int qtGeralAtendimetnos = 0;
                int qtProcedimentos = 0;
                int qtProcedimentos_Geral = 0;
                int idAtendMedic = 0;

                //Valores dentro de Atendimentos
                decimal vl_total = 0;
                decimal vl_desconto = 0;
                decimal vl_liquido = 0;

                //Valores de Todos os Atendimentos
                decimal vl_total_Geral = 0;
                decimal vl_desconto_Geral = 0;
                decimal vl_liquido_Geral = 0;

                foreach (ExtratoFinanceiroAtendimento at in res)
                {
                    qtGeralAtendimetnos++;

                    //Gera lista de todos os Procedimentos requisitados no atendimento em contexto e atribui à classe como lista
                    #region Procedimentos

                    var lstExames = (from tbs357 in TBS357_PROC_MEDIC_FINAN.RetornaTodosRegistros()
                                     join tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs357.ID_ITEM equals tbs356.ID_PROC_MEDI_PROCE
                                     where tbs357.TBS219_ATEND_MEDIC.ID_ATEND_MEDIC == at.ID_ATEND_MEDIC
                                     select new ClExames
                                     {
                                         DT_EXAME = tbs357.DT_INCLU_LANC,
                                         NO_EXAME = tbs356.NM_PROC_MEDI,
                                         VL_DESCONTO = tbs357.VL_DSCTO,
                                         VL_LIQUIDO = tbs357.VL_PROCE_LIQUI,
                                         VL_TOTAL = tbs357.VL_BASE,
                                         CO_TIPO_PROC_MEDI = tbs356.CO_TIPO_PROC_MEDI,
                                     }).OrderBy(w => w.DT_EXAME).ToList();
                    #endregion

                    //De acordo com a lista gerada acima, é gerada a lista de cada um dos subgrupos
                    at.Exames = lstExames.Where(w => w.CO_TIPO_PROC_MEDI == "EX").ToList();
                    at.Procedimentos = lstExames.Where(w => w.CO_TIPO_PROC_MEDI == "PR").ToList();
                    at.ServicosAmbulatoriais = lstExames.Where(w => w.CO_TIPO_PROC_MEDI == "SA").ToList();
                    at.ServicosSaude = lstExames.Where(w => w.CO_TIPO_PROC_MEDI == "SS").ToList();
                    at.Outros = lstExames.Where(w => w.CO_TIPO_PROC_MEDI == "OU").ToList();
                    at.Consultas = lstExames.Where(w => w.CO_TIPO_PROC_MEDI == "CO").ToList();

                    #region Calcula os valores totais

                    //parte responsável por garantir que o bloco Abaixo seja executado apenas uma vez por Atendimento
                    if (idAtendMedic != at.ID_ATEND_MEDIC)
                    {
                        //Zera as variáveis de auxílio
                        vl_total =
                        vl_liquido =
                        vl_desconto = 0;
                        qtProcedimentos = 0;

                        //Inicia a variável de código do Atendimento com o novo Atendimento
                        idAtendMedic = at.ID_ATEND_MEDIC;
                    }

                    foreach (var li in lstExames)
                    {
                        qtProcedimentos_Geral++;
                        qtProcedimentos++;
                        //Valores de Atendimento
                        vl_total += (li.VL_TOTAL.HasValue ? li.VL_TOTAL.Value : 0);
                        vl_liquido += (li.VL_LIQUIDO.HasValue ? li.VL_LIQUIDO.Value : 0);
                        vl_desconto += (li.VL_DESCONTO.HasValue ? li.VL_DESCONTO.Value : 0);

                        //Valores de Todos os Atendimentos
                        vl_total_Geral += (li.VL_TOTAL.HasValue ? li.VL_TOTAL.Value : 0);
                        vl_liquido_Geral += (li.VL_LIQUIDO.HasValue ? li.VL_LIQUIDO.Value : 0);
                        vl_desconto_Geral += (li.VL_DESCONTO.HasValue ? li.VL_DESCONTO.Value : 0);
                    }

                    #region Seta os Valores

                    at.VL_TOTAL = vl_total;
                    at.VL_DESCTO = vl_desconto;
                    at.VL_LIQUID = vl_liquido;

                    //Seta apenas quando for o último registro
                    //if (qtGeralAtendimetnos == res.Count)
                    //{
                        at.VL_TOTAL_GERAL = "R$" + vl_total_Geral.ToString("N2");
                        at.VL_DESCTO_GERAL = "R$" + vl_desconto_Geral.ToString("N2");
                        at.VL_LIQUID_GERAL = "R$" + vl_liquido_Geral.ToString("N2");
                    //}

                    at.QT_PROC = qtProcedimentos;
                    at.QT_PROC_GERAL = qtProcedimentos_Geral;

                    #endregion

                    #endregion

                    #region Trata as listas vazias

                    #region Procedimentos

                    if (at.Procedimentos.Count > 0)
                    {
                        xrLabel5.Visible = false;
                        xrTable12.Visible = xrTable13.Visible = true;
                    }
                    else
                    {
                        xrLabel5.Visible = true;
                        xrTable12.Visible = xrTable13.Visible = false;
                    }

                    #endregion

                    #region Exames

                    if (at.Exames.Count > 0)
                    {
                        xrLabel6.Visible = false;
                        xrTable8.Visible = xrTable9.Visible = true;
                    }
                    else
                    {
                        xrLabel6.Visible = true;
                        xrTable8.Visible = xrTable9.Visible = false;
                    }

                    #endregion

                    #region Serviços Ambulatoriais

                    if (at.ServicosAmbulatoriais.Count > 0)
                    {
                        xrLabel8.Visible = false;
                        xrTable15.Visible = xrTable16.Visible = true;
                    }
                    else
                    {
                        xrLabel8.Visible = true;
                        xrTable15.Visible = xrTable16.Visible = false;
                    }

                    #endregion

                    #region Serviços de Saúde

                    if (at.ServicosSaude.Count > 0)
                    {
                        xrLabel10.Visible = false;
                        xrTable18.Visible = xrTable19.Visible = true;
                    }
                    else
                    {
                        xrLabel10.Visible = true;
                        xrTable18.Visible = xrTable19.Visible = false;
                    }

                    #endregion

                    #region Consultas

                    if (at.Consultas.Count > 0)
                    {
                        xrLabel12.Visible = false;
                        xrTable21.Visible = xrTable22.Visible = true;
                    }
                    else
                    {
                        xrLabel12.Visible = true;
                        xrTable21.Visible = xrTable22.Visible = false;
                    }

                    #endregion

                    #region Outros

                    if (at.Outros.Count > 0)
                    {
                        xrLabel15.Visible = false;
                        xrTable24.Visible = xrTable22.Visible = true;
                    }
                    else
                    {
                        xrLabel15.Visible = true;
                        xrTable24.Visible = xrTable22.Visible = false;
                    }

                    #endregion

                    #endregion

                    bsReport.Add(at);
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class ClExames
        {
            public int ID_PROC { get; set; }
            public string CO_TIPO_PROC_MEDI { get; set; }
            public DateTime? DT_EXAME { get; set; }
            public string DT_EXAME_V
            {
                get
                {
                    return (this.DT_EXAME.HasValue ? this.DT_EXAME.Value.ToString("dd/MM/yy") : " - ");
                }
            }
            public string HORA_EXAME
            {
                get
                {
                    return (this.DT_EXAME.HasValue ? this.DT_EXAME.Value.ToString("HH:mm") : " - ");
                }
            }
            public string NO_EXAME { get; set; }
            public decimal? VL_UNIT { get; set; }
            public decimal? VL_TOTAL { get; set; }
            public decimal? VL_DESCONTO { get; set; }
            public decimal? VL_LIQUIDO { get; set; }
        }

        public class ExtratoFinanceiroAtendimento
        {
            public ExtratoFinanceiroAtendimento()
            {
                this.Exames = new List<ClExames>();
                this.Procedimentos = new List<ClExames>();
                this.ServicosAmbulatoriais = new List<ClExames>();
                this.ServicosSaude = new List<ClExames>();
                this.Consultas = new List<ClExames>();
                this.Outros = new List<ClExames>();
            }
            public List<ClExames> Exames { get; set; }
            public List<ClExames> Procedimentos { get; set; }
            public List<ClExames> ServicosAmbulatoriais { get; set; }
            public List<ClExames> ServicosSaude { get; set; }
            public List<ClExames> Consultas { get; set; }
            public List<ClExames> Outros { get; set; }

            public int contador { get; set; }
            public int ID_ATEND_MEDIC { get; set; }

            //Informações do Paciente
            public string Paciente { get; set; }
            public int NirePaci { get; set; }
            public string PacienteNireValid
            {
                get
                {
                    return this.NirePaci + " - " + this.Paciente;
                }
            }
            public DateTime? dt_nasc { get; set; }
            public string DT_NASC_REFORM
            {
                get
                {
                    return (this.dt_nasc.HasValue ? Funcoes.FormataDataNascimento(this.dt_nasc.Value) : " - ");
                }
            }
            public string sexo { get; set; }
            public string sexo_V
            {
                get
                {
                    return (this.sexo == "M" ? "MAS" : "FEM");
                }
            }

            //Informações da Consulta
            public DateTime? DATA_CONSULTA { get; set; }
            public string DATA_CONSULTA_V
            {
                get
                {
                    return (this.DATA_CONSULTA.HasValue ? this.DATA_CONSULTA.Value.ToString("dd/MM/yy") : " - ");
                }
            }
            public string HORA_CONSULTA
            {
                get
                {
                    return (this.DATA_CONSULTA.HasValue ? this.DATA_CONSULTA.Value.ToString("HH:mm") : " - ");
                }
            }
            public string NU_CONSULTA { get; set; }
            public string NU_CONSULTA_V
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.NU_CONSULTA) ? Funcoes.TrataCodigoRegistroSaude(this.NU_CONSULTA) : " - ");
                }
            }
            public int? CO_PROFI_CONSULTA { get; set; }
            public string NO_PROFI_CONSULTA
            {
                get
                {
                    return (this.CO_PROFI_CONSULTA.HasValue ? TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == this.CO_PROFI_CONSULTA).FirstOrDefault().NO_COL : " - ");
                }
            }
            public int? CO_USUAR_CONSULTA { get; set; }
            public string NO_USUAR_CONSULTA
            {
                get
                {
                    return (this.CO_USUAR_CONSULTA.HasValue ? TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == this.CO_USUAR_CONSULTA).FirstOrDefault().NO_COL : " - ");
                }
            }

            //Informações do Acolhimento
            public DateTime? DATA_ACOLHIMENTO { get; set; }
            public string DATA_ACOLHIMENTO_V
            {
                get
                {
                    return (this.DATA_ACOLHIMENTO.HasValue ? this.DATA_ACOLHIMENTO.Value.ToString("dd/MM/yy") : " - ");
                }
            }
            public string HORA_ACOLHIMENTO
            {
                get
                {
                    return (this.DATA_ACOLHIMENTO.HasValue ? this.DATA_ACOLHIMENTO.Value.ToString("HH:mm") : " - ");
                }
            }
            public string NU_ACOLHIMENTO { get; set; }
            public string NU_ACOLHIMENTO_V
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.NU_ACOLHIMENTO) ? Funcoes.TrataCodigoRegistroSaude(this.NU_ACOLHIMENTO) : " - ");
                }
            }
            public int? CO_PROFI_ACOLHIMENTO { get; set; }
            public string NO_PROFI_ACOLHIMENTO
            {
                get
                {
                    return (this.CO_PROFI_ACOLHIMENTO.HasValue ? TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == this.CO_PROFI_ACOLHIMENTO).FirstOrDefault().NO_COL : " - ");
                }
            }

            //Informação do Direcionamento
            public DateTime? DATA_DIRECIONAMENTO { get; set; }
            public string DATA_DIRECIONAMENTO_V
            {
                get
                {
                    return (this.DATA_DIRECIONAMENTO.HasValue ? this.DATA_DIRECIONAMENTO.Value.ToString("dd/MM/yy") : " - ");
                }
            }
            public string HORA_DIRECIONAMENTO
            {
                get
                {
                    return (this.DATA_DIRECIONAMENTO.HasValue ? this.DATA_DIRECIONAMENTO.Value.ToString("HH:mm") : " - ");
                }
            }
            public string NU_DIRECIONAMENTO { get; set; }
            public string NU_DIRECIONAMENTO_V
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.NU_DIRECIONAMENTO) ? Funcoes.TrataCodigoRegistroSaude(this.NU_DIRECIONAMENTO) : " - ");
                }
            }
            public int? CO_PROFI_DIRECIONAMENTO { get; set; }
            public string NO_PROFI_DIRECIONAMENTO
            {
                get
                {
                    return (this.CO_PROFI_DIRECIONAMENTO.HasValue ? TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == this.CO_PROFI_DIRECIONAMENTO).FirstOrDefault().NO_COL : " - ");
                }
            }
            public int? CO_USUAR_DIRECIONAMENTO { get; set; }
            public string NO_USUAR_DIRECIONAMENTO
            {
                get
                {
                    return (this.CO_USUAR_DIRECIONAMENTO.HasValue ? TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == this.CO_USUAR_DIRECIONAMENTO).FirstOrDefault().NO_COL : " - ");
                }
            }

            //Informação do Atendimento
            public DateTime? DATA_ATENDIMENTO { get; set; }
            public string DATA_ATENDIMENTO_V
            {
                get
                {
                    return (this.DATA_ATENDIMENTO.HasValue ? this.DATA_ATENDIMENTO.Value.ToString("dd/MM/yy") : " - ");
                }
            }
            public string HORA_ATENDIMENTO
            {
                get
                {
                    return (this.DATA_ATENDIMENTO.HasValue ? this.DATA_ATENDIMENTO.Value.ToString("HH:mm") : " - ");
                }
            }
            public string NU_ATENDIMENTO { get; set; }
            public string NU_ATENDIMENTO_V
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.NU_ATENDIMENTO) ? Funcoes.TrataCodigoRegistroSaude(this.NU_ATENDIMENTO) : " - ");
                }
            }
            public int? CO_PROFI_ATENDIMENTO { get; set; }
            public string NO_PROFI_ATENDIMENTO
            {
                get
                {
                    return (this.CO_PROFI_ATENDIMENTO.HasValue ? TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == this.CO_PROFI_ATENDIMENTO).FirstOrDefault().NO_COL : " - ");
                }
            }
            public int? CO_USUAR_ATENDIMENTO { get; set; }
            public string NO_USUAR_ATENDIMENTO
            {
                get
                {
                    return (this.CO_USUAR_ATENDIMENTO.HasValue ? TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == this.CO_USUAR_ATENDIMENTO).FirstOrDefault().NO_COL : " - ");
                }
            }

            //Valores dentro de um atendimento
            public decimal VL_TOTAL { get; set; }
            public decimal VL_DESCTO { get; set; }
            public decimal VL_LIQUID { get; set; }
            public int QT_PROC { get; set; }

            //Valores somados em todos os atendimentos
            public string VL_TOTAL_GERAL { get; set; }
            public string VL_DESCTO_GERAL { get; set; }
            public string VL_LIQUID_GERAL { get; set; }
            public int QT_PROC_GERAL { get; set; }

            public int QT_UNIT { get; set; }
        }
    }
}


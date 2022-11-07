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
    public partial class RptAmbulatorio : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptAmbulatorio()
        {
            InitializeComponent();
        }

        public int InitReport(
            int coAlu,
            int idServ,
            int CoAgend,
            int coEmp
            )
        {
            try
            {
                #region Inicializa o header/Labels
                
                // Cria o header a partir do cod da instituicao
                var header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return -1;

                // Inicializa o headero
                base.BaseInit(header);

                #endregion

                var tbs426 = TBS426_SERVI_AMBUL.RetornaPelaChavePrimaria(idServ);
                if (tbs426 != null)
                {
                    var paciente = new Paciente();
                    tbs426.TBS390_ATEND_AGENDReference.Load();
                    tbs426.TBS390_ATEND_AGEND.TB07_ALUNOReference.Load();

                    paciente.Nome = tbs426.TBS390_ATEND_AGEND.TB07_ALUNO.NO_ALU;
                    paciente.dataNasc = tbs426.TBS390_ATEND_AGEND.TB07_ALUNO.DT_NASC_ALU;
                    paciente.sex = tbs426.TBS390_ATEND_AGEND.TB07_ALUNO.CO_SEXO_ALU;
                    paciente.dataAtendimento = tbs426.DT_CADASTRO;
                    var tb03 = TB03_COLABOR.RetornaPeloCoCol(tbs426.CO_COL_CADAS.Value);
                    paciente.Profissional = tb03.NO_APEL_COL;
                    paciente.Funcao = tb03.DE_FUNC_COL;
                    paciente.InformacoesPessoais = "Nascimento: " + paciente.dataNasc.Value.ToShortDateString() + " - Idade: " + paciente.Idade + " - Sexo: " + paciente.Sexo;
                    paciente.InformacoesProfissionais = "Atendido por: " + paciente.Profissional + " - (" + paciente.Funcao + ") " + paciente.dataAtendimento.Value.ToLongDateString();

                    var tbs427 = TBS427_SERVI_AMBUL_ITENS.RetornaTodosRegistros().Where(x => x.TBS426_SERVI_AMBUL.ID_SERVI_AMBUL == idServ).ToList();
                    List<ServicoAmbulatorial> servList = new List<ServicoAmbulatorial>();
                    foreach (var t in tbs427)
                    {
                        var tbs428 = TBS428_APLIC_SERVI_AMBUL.RetornaTodosRegistros().Where(x => x.TBS427_SERVI_AMBUL_ITENS.ID_LISTA_SERVI_AMBUL == t.ID_LISTA_SERVI_AMBUL).FirstOrDefault();

                        t.TBS356_PROC_MEDIC_PROCEReference.Load();
                        var serv = new ServicoAmbulatorial();
                        serv.Nome = t.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI;
                        serv.Descricao = tbs428 != null ? t.TBS356_PROC_MEDIC_PROCE.DE_OBSE_PROC_MEDI : "-";
                        serv.dataAplicado = tbs428 != null ? tbs428.DT_APLIC__SERVI_AMBUL : (DateTime?)null;
                        serv.isApl = tbs428 != null ? tbs428.IS_APLIC_SERVI_AMBUL : "-";
                        serv.entrega = tbs428 != null ? tbs428.DT_ENTREGA : (DateTime?)null;
                        serv.Observacao = tbs428 != null ? tbs428.OBSERVACAO : "-";
                        serv.pedido = tbs428 != null ? tbs428.DT_PEDIDO : (DateTime?)null;
                        var _tb03 = tbs428 != null ? TB03_COLABOR.RetornaPeloCoCol(tbs428.CO_COL_APLIC) : null;
                        serv.Profissional = _tb03 != null ? (_tb03.NO_APEL_COL + " (" + _tb03.DE_FUNC_COL + ")") : "-";
                        servList.Add(serv);
                    }

                    paciente.ServicosAmbulatorial = servList.Count > 0 ? servList : null;
                    
                    CultureInfo culture = new CultureInfo("pt-BR");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    int dia = DateTime.Now.Day;
                    int ano = DateTime.Now.Year;
                    string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(DateTime.Now.Month));
                    string diasemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(DateTime.Now.DayOfWeek));
                    string dataEmis = diasemana + ", " + dia + " de " + mes + " de " + ano;
                    paciente.IngormacoesData = dataEmis;

                    bsReport.Clear();
                    bsReport.Add(paciente);

                    return 1;
                }
                else
                {
                    return -1;
                }                
            }
            catch { return 0; }
        }

        public class Paciente
        {
            public string Nome { get; set; }
            public DateTime? dataNasc { get; set; }
            public string Idade
            {
                get
                {
                    return dataNasc.HasValue ? Funcoes.FormataDataNascimento(dataNasc.Value) : "";
                }
            }
            public string sex { get; set; }
            public string Sexo
            {
                get
                {
                    if (!string.IsNullOrEmpty(this.sex))
                    {
                        return this.sex.Equals("M") ? "Masculino" : "Feminino";
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            public DateTime? dataAtendimento { get; set; }
            public string Profissional { get; set; }
            public string Funcao { get; set; }
            public string InformacoesPessoais { get; set; }
            public string InformacoesProfissionais { get; set; }
            public string IngormacoesData { get; set; }
            public List<ServicoAmbulatorial> ServicosAmbulatorial { get; set; }
        }

        public class ServicoAmbulatorial
        {
            public string Nome { get; set; }
            public string Descricao { get; set; }
            public DateTime? pedido { get; set; }
            public string dataPedido
            {
                get
                {
                    return pedido.HasValue ? pedido.Value.ToShortDateString() : "";
                }
            }
            public DateTime? entrega { get; set; }
            public string dataEntrega
            {
                get
                {
                    return entrega.HasValue ? entrega.Value.ToShortDateString() : "";
                }
            }
            public DateTime? dataAplicado { get; set; }
            public string dtAplic
            {
                get
                {
                    return dataAplicado.HasValue ? dataAplicado.Value.ToShortDateString() : "";
                }
            }
            public string isApl { get; set; }
            public string Aplicado { get { return this.isApl.Equals("S") ? "Efetuado" : "Não efetuado"; } }
            public string Observacao { get; set; }
            public string Profissional { get; set; }
        }
    }
}

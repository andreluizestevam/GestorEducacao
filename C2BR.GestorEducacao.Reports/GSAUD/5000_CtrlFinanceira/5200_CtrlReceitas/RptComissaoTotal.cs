using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;
using C2BR.GestorEducacao.Reports;
using DevExpress.XtraCharts;

namespace C2BR.GestorEducacao.Reports.GSAUD._5000_CtrlFinanceira._5200_CtrlReceitas
{
    public partial class RptComissaoTotal : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptComissaoTotal()
        {
            InitializeComponent();
        }

        public int InitReport(
              string titulo,
              string parametros,
              string infos,
              int coEmp,
              int unidade,
              int profissional,
              int grup,
              int sgrup,
              int proc,
              string tipo
            )
        {
            try
            {
                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.Parametros = parametros;
                this.lblTitulo.Text = titulo.ToUpper();

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;
                // Setar o header do relatorio
                this.BaseInit(header);

                var res = new Relatorio();

                #region Avaliações

                if (tipo == "0" || tipo == "AVL")
                {
                    res.Avaliacoes = new List<Comissao>();

                    if (tipo == "AVL" && res.Avaliacoes.Count == 0)
                        return -1;
                }

                #endregion

                #region Cobranças

                if (tipo == "0" || tipo == "CBR")
                {
                    res.Cobranca = new List<Comissao>();

                    var titulos = (from tbs47 in TBS47_CTA_RECEB.RetornaTodosRegistros()
                                   where (profissional != 0 ? tbs47.TB03_COLABOR.CO_COL == profissional : tbs47.TB03_COLABOR != null)
                                   && (unidade != 0 ? tbs47.TB25_EMPRESA.CO_EMP == unidade : 0 == 0)
                                   select tbs47).ToList();

                    foreach (var t in titulos.DistinctBy(t => t.NU_CONTRATO))
                        t.TB03_COLABORReference.Load();

                    foreach (var t in titulos.DistinctBy(t => t.NU_CONTRATO))
                    {
                        var tbs404 = TBS404_CONTRATOS.RetornaPeloNumContrato(t.NU_CONTRATO);

                        if (tbs404 != null)
                        {
                            tbs404.TBS396_ATEND_ORCAM.Load();
                            var cobrador = t.TB03_COLABOR.CO_COL;
                            var qtd = titulos.Where(tit => tit.TB03_COLABOR.CO_COL == cobrador && tit.NU_CONTRATO == tbs404.NU_CONTRATO).Count();

                            var itens = tbs404.TBS396_ATEND_ORCAM.Where(i =>
                                (grup != 0 ? i.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == grup : 0 == 0)
                                && (sgrup != 0 ? i.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP == sgrup : 0 == 0)
                                && (proc != 0 ? i.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == proc : 0 == 0));

                            foreach (var i in itens)
                            {
                                i.TBS356_PROC_MEDIC_PROCEReference.Load();

                                var cobr = (from tbs410 in TBS410_COMISSAO.RetornaTodosRegistros()
                                            where (tbs410.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == i.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE)
                                            && (tbs410.TB03_COLABOR.CO_COL == cobrador) && tbs410.VL_COBRAN.HasValue
                                            select new Comissao
                                            {
                                                NO_COL = tbs410.TB03_COLABOR.NO_COL,
                                                GRUPO = tbs410.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.NM_PROC_MEDIC_GRUPO,
                                                SUBGRUPO = tbs410.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP,
                                                PROCED = tbs410.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                                                VL_UNITA = tbs410.TBS356_PROC_MEDIC_PROCE.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(p => p.FL_STATU == "A") != null ? tbs410.TBS356_PROC_MEDIC_PROCE.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(p => p.FL_STATU == "A").VL_BASE : 0,
                                                PC_REFERENCIA = tbs410.PC_COBRAN,
                                                VL_REFERENCIA = tbs410.VL_COBRAN,
                                                QTD = qtd
                                            }).OrderBy(a => a.NO_COL).ToList();

                                foreach (var c in cobr)
                                    res.Cobranca.Add(c);
                            }
                        }
                    }

                    if (tipo == "CBR" && res.Cobranca.Count == 0)
                        return -1;
                }

                #endregion

                #region Contratos

                if (tipo == "0" || tipo == "CNT")
                {
                    res.Contrato = new List<Comissao>();
                    var comissoes = new List<Comissao>();

                    var contratos = (from tbs404 in TBS404_CONTRATOS.RetornaTodosRegistros()
                                     where (profissional != 0 ? tbs404.CO_COL_CADAS == profissional : 0 == 0)
                                     && (unidade != 0 ? tbs404.CO_EMP_CADAS == unidade : 0 == 0)
                                     select tbs404).ToList();

                    foreach (var tbs404 in contratos)
                    {
                        tbs404.TBS396_ATEND_ORCAM.Load();

                        var itens = tbs404.TBS396_ATEND_ORCAM.Where(i =>
                            (grup != 0 ? i.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == grup : 0 == 0)
                            && (sgrup != 0 ? i.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP == sgrup : 0 == 0)
                            && (proc != 0 ? i.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == proc : 0 == 0));

                        foreach (var i in itens)
                        {
                            i.TBS356_PROC_MEDIC_PROCEReference.Load();

                            var cont = (from tbs410 in TBS410_COMISSAO.RetornaTodosRegistros()
                                        where (tbs410.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == i.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE)
                                        && (tbs410.TB03_COLABOR.CO_COL == tbs404.CO_COL_CADAS) && tbs410.VL_CONTRT.HasValue
                                        select new Comissao
                                        {
                                            CO_COL = tbs410.TB03_COLABOR.CO_COL,
                                            NO_COL = tbs410.TB03_COLABOR.NO_COL,
                                            GRUPO = tbs410.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.NM_PROC_MEDIC_GRUPO,
                                            SUBGRUPO = tbs410.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP,
                                            PROCED = tbs410.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                                            ID_PROCED = tbs410.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                                            VL_UNITA = tbs410.TBS356_PROC_MEDIC_PROCE.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(p => p.FL_STATU == "A") != null ? tbs410.TBS356_PROC_MEDIC_PROCE.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(p => p.FL_STATU == "A").VL_BASE : 0,
                                            PC_REFERENCIA = tbs410.PC_CONTRT,
                                            VL_REFERENCIA = tbs410.VL_CONTRT
                                        }).OrderBy(a => a.NO_COL).ToList();

                            foreach (var c in cont)
                                comissoes.Add(c);
                        }
                    }

                    foreach (var c in comissoes)
                    {
                        c.QTD = comissoes.Where(com => com.CO_COL == c.CO_COL && com.ID_PROCED == c.ID_PROCED).Count();

                        if (res.Contrato.Where(con => con.CO_COL == c.CO_COL && con.ID_PROCED == c.ID_PROCED).FirstOrDefault() == null)
                            res.Contrato.Add(c);
                    }

                    if (tipo == "CNT" && res.Contrato.Count == 0)
                        return -1;
                }

                #endregion

                #region Indicações de Paciente

                if (tipo == "0" || tipo == "IPC")
                {
                    res.IndPaciente = new List<Comissao>();
                    var comissoes = new List<Comissao>();

                    var indicacoes = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                      where (profissional != 0 ? tb07.CO_COL_INDICACAO == profissional : tb07.CO_COL_INDICACAO.HasValue)
                                      && (unidade != 0 ? tb07.CO_EMP == unidade : 0 == 0)
                                      select tb07.CO_ALU).ToList();

                    foreach (var alu in indicacoes)
                    {
                        var contratos = TBS404_CONTRATOS.RetornaTodosRegistros().Where(c => c.TB07_ALUNO.CO_ALU == alu);

                        foreach (var tbs404 in contratos)
                        {
                            tbs404.TBS396_ATEND_ORCAM.Load();

                            var itens = tbs404.TBS396_ATEND_ORCAM.Where(i =>
                                (grup != 0 ? i.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == grup : 0 == 0)
                                && (sgrup != 0 ? i.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP == sgrup : 0 == 0)
                                && (proc != 0 ? i.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == proc : 0 == 0));

                            foreach (var i in itens)
                            {
                                i.TBS356_PROC_MEDIC_PROCEReference.Load();

                                var indc = (from tbs410 in TBS410_COMISSAO.RetornaTodosRegistros()
                                            where (tbs410.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == i.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE)
                                            && (tbs410.TB03_COLABOR.CO_COL == tbs404.CO_COL_CADAS) && tbs410.VL_INDC_PAC.HasValue
                                            select new Comissao
                                            {
                                                CO_COL = tbs410.TB03_COLABOR.CO_COL,
                                                NO_COL = tbs410.TB03_COLABOR.NO_COL,
                                                GRUPO = tbs410.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.NM_PROC_MEDIC_GRUPO,
                                                SUBGRUPO = tbs410.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP,
                                                PROCED = tbs410.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                                                ID_PROCED = tbs410.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                                                VL_UNITA = tbs410.TBS356_PROC_MEDIC_PROCE.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(p => p.FL_STATU == "A") != null ? tbs410.TBS356_PROC_MEDIC_PROCE.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(p => p.FL_STATU == "A").VL_BASE : 0,
                                                PC_REFERENCIA = tbs410.PC_INDC_PAC,
                                                VL_REFERENCIA = tbs410.VL_INDC_PAC
                                            }).OrderBy(a => a.NO_COL).ToList();

                                foreach (var c in indc)
                                    comissoes.Add(c);
                            }
                        }
                    }

                    foreach (var c in comissoes)
                    {
                        c.QTD = comissoes.Where(com => com.CO_COL == c.CO_COL && com.ID_PROCED == c.ID_PROCED).Count();

                        if (res.IndPaciente.Where(con => con.CO_COL == c.CO_COL && con.ID_PROCED == c.ID_PROCED).FirstOrDefault() == null)
                            res.IndPaciente.Add(c);
                    }

                    if (tipo == "IPC" && res.IndPaciente.Count == 0)
                        return -1;
                }

                #endregion

                #region Indicações de Procedimentos

                if (tipo == "0" || tipo == "IPR")
                {
                    res.IndProcedimento = new List<Comissao>();
                    var comissoes = new List<Comissao>();

                    var itens = (from tbs396 in TBS396_ATEND_ORCAM.RetornaTodosRegistros()
                                 where (profissional != 0 ? tbs396.CO_COL_INDICACAO == profissional : tbs396.CO_COL_INDICACAO.HasValue)
                                 && (unidade != 0 ? tbs396.CO_EMP_INDICACAO == unidade : 0 == 0)
                                 && (grup != 0 ? tbs396.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == grup : 0 == 0)
                                 && (sgrup != 0 ? tbs396.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP == sgrup : 0 == 0)
                                 && (proc != 0 ? tbs396.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == proc : 0 == 0)
                                 select tbs396);

                    foreach (var i in itens)
                    {
                        i.TBS356_PROC_MEDIC_PROCEReference.Load();

                        var indc = (from tbs410 in TBS410_COMISSAO.RetornaTodosRegistros()
                                    where (tbs410.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == i.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE)
                                    && (tbs410.TB03_COLABOR.CO_COL == i.CO_COL_INDICACAO) && tbs410.VL_INDC_PROC.HasValue
                                    select new Comissao
                                    {
                                        CO_COL = tbs410.TB03_COLABOR.CO_COL,
                                        NO_COL = tbs410.TB03_COLABOR.NO_COL,
                                        GRUPO = tbs410.TBS356_PROC_MEDIC_PROCE.TBS354_PROC_MEDIC_GRUPO.NM_PROC_MEDIC_GRUPO,
                                        SUBGRUPO = tbs410.TBS356_PROC_MEDIC_PROCE.TBS355_PROC_MEDIC_SGRUP.NM_PROC_MEDIC_SGRUP,
                                        PROCED = tbs410.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                                        ID_PROCED = tbs410.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                                        VL_UNITA = tbs410.TBS356_PROC_MEDIC_PROCE.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(p => p.FL_STATU == "A") != null ? tbs410.TBS356_PROC_MEDIC_PROCE.TBS353_VALOR_PROC_MEDIC_PROCE.FirstOrDefault(p => p.FL_STATU == "A").VL_BASE : 0,
                                        PC_REFERENCIA = tbs410.PC_INDC_PROC,
                                        VL_REFERENCIA = tbs410.VL_INDC_PROC
                                    }).OrderBy(a => a.NO_COL).ToList();

                        foreach (var c in indc)
                            comissoes.Add(c);
                    }

                    foreach (var c in comissoes)
                    {
                        c.QTD = comissoes.Where(com => com.CO_COL == c.CO_COL && com.ID_PROCED == c.ID_PROCED).Count();

                        if (res.IndProcedimento.Where(con => con.CO_COL == c.CO_COL && con.ID_PROCED == c.ID_PROCED).FirstOrDefault() == null)
                            res.IndProcedimento.Add(c);
                    }

                    if (tipo == "IPR" && res.IndProcedimento.Count == 0)
                        return -1;
                }

                #endregion

                #region Planejamentos

                if (tipo == "0" || tipo == "PLA")
                {
                    res.Planejamento = new List<Comissao>();

                    if (tipo == "PLA" && res.Planejamento.Count == 0)
                        return -1;
                }

                #endregion

                if (tipo == "0" && res.Avaliacoes.Count == 0 && res.Cobranca.Count == 0 && res.Contrato.Count == 0 
                    && res.IndPaciente.Count == 0 && res.IndProcedimento.Count == 0 && res.Planejamento.Count == 0)
                    return -1;

                //Adiciona ao DataSource do Relatório
                bsReport.Clear();
                bsReport.Add(res);

                return 1;

            }
            catch { return 0; }
        }

        public class Relatorio
        {
            public List<Comissao> Avaliacoes { get; set; }
            public List<Comissao> Cobranca { get; set; }
            public List<Comissao> Contrato { get; set; }
            public List<Comissao> IndPaciente { get; set; }
            public List<Comissao> IndProcedimento { get; set; }
            public List<Comissao> Planejamento { get; set; }

            public int ITENS_FINAL
            {
                get
                {
                    var total = 0;

                    if (Avaliacoes != null)
                        total += Avaliacoes.Count;

                    if (Cobranca != null)
                        total += Cobranca.Count;

                    if (Contrato != null)
                        total += Contrato.Count;

                    if (IndPaciente != null)
                        total += IndPaciente.Count;

                    if (IndProcedimento != null)
                        total += IndProcedimento.Count;

                    return total;
                }
            }

            public int QTD_FINAL
            {
                get
                {
                    var total = 0;

                    if (Avaliacoes != null)
                        foreach (var a in Avaliacoes)
                            total += a.QTD;

                    if (Cobranca != null)
                        foreach (var c in Cobranca)
                            total += c.QTD;

                    if (Contrato != null)
                        foreach (var c in Contrato)
                            total += c.QTD;

                    if (IndPaciente != null)
                        foreach (var i in IndPaciente)
                            total += i.QTD;

                    if (IndProcedimento != null)
                        foreach (var i in IndProcedimento)
                            total += i.QTD;

                    if (Planejamento != null)
                        foreach (var p in Planejamento)
                            total += p.QTD;

                    return total;
                }
            }

            public decimal VL_UNIT_FINAL
            {
                get
                {
                    decimal total = 0;

                    if (Avaliacoes != null)
                        foreach (var a in Avaliacoes)
                            total += a.VL_UNITA;

                    if (Cobranca != null)
                        foreach (var c in Cobranca)
                            total += c.VL_UNITA;

                    if (Contrato != null)
                        foreach (var c in Contrato)
                            total += c.VL_UNITA;

                    if (IndPaciente != null)
                        foreach (var i in IndPaciente)
                            total += i.VL_UNITA;

                    if (IndProcedimento != null)
                        foreach (var i in IndProcedimento)
                            total += i.VL_UNITA;

                    if (Planejamento != null)
                        foreach (var p in Planejamento)
                            total += p.VL_UNITA;

                    return total;
                }
            }

            public decimal VL_TOTAL_FINAL
            {
                get
                {
                    decimal total = 0;

                    if (Avaliacoes != null)
                        foreach (var a in Avaliacoes)
                            total += a.VL_TOTAL;

                    if (Cobranca != null)
                        foreach (var c in Cobranca)
                            total += c.VL_TOTAL;

                    if (Contrato != null)
                        foreach (var c in Contrato)
                            total += c.VL_TOTAL;

                    if (IndPaciente != null)
                        foreach (var i in IndPaciente)
                            total += i.VL_TOTAL;

                    if (IndProcedimento != null)
                        foreach (var i in IndProcedimento)
                            total += i.VL_TOTAL;

                    if (Planejamento != null)
                        foreach (var p in Planejamento)
                            total += p.VL_TOTAL;

                    return total;
                }
            }

            public decimal VL_COMIS_FINAL
            {
                get
                {
                    decimal total = 0;

                    if (Avaliacoes != null)
                        foreach (var a in Avaliacoes)
                            total += a.VL_COMIS;

                    if (Cobranca != null)
                        foreach (var c in Cobranca)
                            total += c.VL_COMIS;

                    if (Contrato != null)
                        foreach (var c in Contrato)
                            total += c.VL_COMIS;

                    if (IndPaciente != null)
                        foreach (var i in IndPaciente)
                            total += i.VL_COMIS;

                    if (IndProcedimento != null)
                        foreach (var i in IndProcedimento)
                            total += i.VL_COMIS;

                    if (Planejamento != null)
                        foreach (var p in Planejamento)
                            total += p.VL_COMIS;

                    return total;
                }
            }
        }

        public class Comissao
        {
            public int CO_COL { get; set; }
            public string NO_COL { get; set; }
            public string GRUPO { get; set; }
            public string SUBGRUPO { get; set; }
            public int ID_PROCED { get; set; }
            public string PROCED { get; set; }

            public int QTD { get; set; }
            public decimal VL_UNITA { get; set; }
            public decimal VL_TOTAL
            {
                get
                {
                    return QTD * VL_UNITA;
                }
            }

            public string PC_REFERENCIA { get; set; }
            public decimal? VL_REFERENCIA { get; set; }
            public string REFERENCIA
            {
                get
                {
                    return VL_REFERENCIA.HasValue ? ((PC_REFERENCIA == "S" ? "" : "R$ ") + VL_REFERENCIA.Value.ToString("N") + (PC_REFERENCIA == "S" ? " %" : "")) : " - ";
                }
            }

            public decimal VL_COMIS
            {
                get
                {
                    if (PC_REFERENCIA == "S")
                        return (VL_TOTAL / 100) * VL_REFERENCIA.Value;
                    else
                        return VL_REFERENCIA.Value * QTD;
                }
            }
        }
    }
}

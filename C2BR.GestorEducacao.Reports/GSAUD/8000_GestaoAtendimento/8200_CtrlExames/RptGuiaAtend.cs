using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;
using System.Collections.Generic;

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8200_CtrlExames
{
    public partial class RptGuiaAtend : DevExpress.XtraReports.UI.XtraReport
    {
        public RptGuiaAtend()
        {
            InitializeComponent();
        }

        public int InitReport(
              int Paciente,
              string Observacoes,
              string Operadora,
              string DTGuia = "",
              int CoColSolic = 0,
              int idAgend = 0,
              bool Consolidados = false,
              DateTime? DTConsolIni = null,
              DateTime? DTConsolFim = null
            )
        {
            try
            {
                lblObsGuia.Text = Observacoes;

                if (!String.IsNullOrEmpty(Operadora) && !Operadora.Equals("0"))
                {
                    var Oper = TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(Operadora));
                    Oper.ImageReference.Load();

                    if (Oper != null && Oper.Image != null)
                    {
                        System.Drawing.Image logo;
                        ImageConverter imgConv = new ImageConverter();
                        logo = (System.Drawing.Image)imgConv.ConvertFrom(Oper.Image.ImageStream);
                        imgBox.Image = logo;
                    }
                }

                if (Consolidados)
                {
                    Resultado res = new Resultado();
                    if (Paciente != 0)
                    {
                        var result = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(x => x.DT_AGEND_HORAR >= DTConsolIni && x.DT_AGEND_HORAR <= DTConsolFim)
                                      join tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR
                                      join tbs386 in TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros() on tbs389.ID_ASSOC_ITENS_PLANE_AGEND equals tbs386.ID_ITENS_PLANE_AVALI
                                      join tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs386.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE equals tbs356.ID_PROC_MEDI_PROCE
                                      where (tbs174.CO_ALU == Paciente)
                                      select new
                                      {
                                          noProced = tbs356.NM_REDUZ_PROC_MEDI,
                                          idProced = tbs356.ID_PROC_MEDI_PROCE,
                                          coProced = tbs356.CO_PROC_MEDI,
                                          qntProced = tbs386.QT_PROCED
                                      }).ToList();

                        res.ListProced = new List<Procedimento>();

                        int index = 1;

                        if (result.Count > 0)
                        {
                            foreach (var item in result.DistinctBy(x => x.idProced))
                            {
                                Procedimento proced = new Procedimento();

                                proced.Index = index;
                                proced.TabelaProced = "";
                                proced.CoProced = item.coProced;
                                proced.IdProced = item.idProced;
                                proced.NoProced = item.noProced;
                                proced.QtProced = item.qntProced.HasValue ? item.qntProced.Value : 1;

                                index++;

                                res.ListProced.Add(proced);
                            }
                        }
                        else
                        {
                            for (int i = 1; i <= 5; i++)
                            {
                                Procedimento proced = new Procedimento();

                                proced.Index = i;
                                proced.TabelaProced = "";
                                proced.CoProced = "";
                                proced.IdProced = null;
                                proced.NoProced = "";
                                proced.QtProced = null;

                                res.ListProced.Add(proced);
                            }
                        }

                        var Aluno = TB07_ALUNO.RetornaPeloCoAlu(Paciente);

                        res.NomePaci = !String.IsNullOrEmpty(Aluno.NO_ALU) ? Aluno.NO_ALU : Aluno.NO_APE_ALU;
                        res.PaciCart = Aluno.NU_PLANO_SAUDE;
                        res.PaciCartSus = Aluno.NU_CARTAO_SAUDE;
                        res.PaciValid = Aluno.DT_VENC_PLAN.HasValue ? Aluno.DT_VENC_PLAN.Value.ToString("dd/MM/yyyy") : "_____/_____/__________";

                        if (CoColSolic != 0)
                        {
                            var Profi = TB03_COLABOR.RetornaPeloCoCol(CoColSolic);

                            res.NomeProfi = !String.IsNullOrEmpty(Profi.NO_COL) ? Profi.NO_COL : Profi.NO_APEL_COL;
                            res.ProfiSiglaEntid = String.IsNullOrEmpty(Profi.CO_SIGLA_ENTID_PROFI) ? "" : Profi.CO_SIGLA_ENTID_PROFI;
                            res.ProfiUfEntid = String.IsNullOrEmpty(Profi.CO_UF_ENTID_PROFI) ? "" : Profi.CO_UF_ENTID_PROFI;
                            res.ProfiNumEntid = String.IsNullOrEmpty(Profi.NU_ENTID_PROFI) ? "" : Profi.NU_ENTID_PROFI;
                        }

                        res.DTGuia = DTGuia;
                    }
                    else
                    {
                        for (int i = 1; i <= 5; i++)
                        {
                            Procedimento proced = new Procedimento();

                            proced.Index = i;
                            proced.TabelaProced = "";
                            proced.CoProced = "";
                            proced.IdProced = null;
                            proced.NoProced = "";
                            proced.QtProced = null;

                            res.ListProced.Add(proced);
                        }
                    }

                    bsReport.Add(res);

                }
                else if (Paciente != 0)
                {
                    Resultado res = new Resultado();

                    if (idAgend != 0)
                    {
                        var resultAgend = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(idAgend);

                        res.TpConsulta = resultAgend.TP_CONSU.Equals("N") ? "Consulta" : resultAgend.TP_CONSU.Equals("R") ? "Retorno" : resultAgend.TP_CONSU.Equals("P") ? "Procedimento" : resultAgend.TP_CONSU.Equals("E") ? "Exame" : resultAgend.TP_CONSU.Equals("C") ? "Cirurgia" : resultAgend.TP_CONSU.Equals("V") ? "Vacina" : "Outros";

                        var result = (from tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros().Where(x => x.TBS174_AGEND_HORAR.ID_AGEND_HORAR == resultAgend.ID_AGEND_HORAR)
                                      join tbs386 in TBS386_ITENS_PLANE_AVALI.RetornaTodosRegistros() on tbs389.ID_ASSOC_ITENS_PLANE_AGEND equals tbs386.ID_ITENS_PLANE_AVALI
                                      join tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs386.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE equals tbs356.ID_PROC_MEDI_PROCE
                                      select new
                                      {
                                          noProced = tbs356.NM_REDUZ_PROC_MEDI,
                                          idProced = tbs356.ID_PROC_MEDI_PROCE,
                                          coProced = tbs356.CO_PROC_MEDI,
                                          qntProced = tbs386.QT_PROCED
                                      }).ToList();

                        res.ListProced = new List<Procedimento>();

                        int index = 1;

                        if (result.Count > 0)
                        {
                            foreach (var item in result.DistinctBy(x => x.idProced))
                            {
                                Procedimento proced = new Procedimento();

                                proced.Index = index;
                                proced.TabelaProced = "";
                                proced.CoProced = item.coProced;
                                proced.IdProced = item.idProced;
                                proced.NoProced = item.noProced;
                                proced.QtProced = item.qntProced.HasValue ? item.qntProced.Value : 1;

                                index++;

                                res.ListProced.Add(proced);
                            }
                        }
                        else
                        {
                            for (int i = 1; i <= 5; i++)
                            {
                                Procedimento proced = new Procedimento();

                                proced.Index = i;
                                proced.TabelaProced = "";
                                proced.CoProced = "";
                                proced.IdProced = null;
                                proced.NoProced = "";
                                proced.QtProced = null;

                                res.ListProced.Add(proced);
                            }
                        }
                    }

                    var Aluno = TB07_ALUNO.RetornaPeloCoAlu(Paciente);

                    res.NomePaci = !String.IsNullOrEmpty(Aluno.NO_ALU) ? Aluno.NO_ALU : Aluno.NO_APE_ALU;
                    res.PaciCart = Aluno.NU_PLANO_SAUDE;
                    res.PaciCartSus = Aluno.NU_CARTAO_SAUDE;
                    res.PaciValid = Aluno.DT_VENC_PLAN.HasValue ? Aluno.DT_VENC_PLAN.Value.ToString("dd/MM/yyyy") : "_____/_____/__________";

                    if (CoColSolic != 0)
                    {
                        var Profi = TB03_COLABOR.RetornaPeloCoCol(CoColSolic);

                        res.NomeProfi = !String.IsNullOrEmpty(Profi.NO_COL) ? Profi.NO_COL : Profi.NO_APEL_COL;
                        res.ProfiSiglaEntid = String.IsNullOrEmpty(Profi.CO_SIGLA_ENTID_PROFI) ? "" : Profi.CO_SIGLA_ENTID_PROFI;
                        res.ProfiUfEntid = String.IsNullOrEmpty(Profi.CO_UF_ENTID_PROFI) ? "" : Profi.CO_UF_ENTID_PROFI;
                        res.ProfiNumEntid = String.IsNullOrEmpty(Profi.NU_ENTID_PROFI) ? "" : Profi.NU_ENTID_PROFI;
                    }

                    res.DTGuia = DTGuia;

                    bsReport.Add(res);
                }
                else
                {
                    Resultado res = new Resultado();

                    for (int i = 1; i <= 5; i++)
                    {
                        Procedimento proced = new Procedimento();

                        proced.Index = i;
                        proced.TabelaProced = "";
                        proced.CoProced = "";
                        proced.IdProced = null;
                        proced.NoProced = "";
                        proced.QtProced = null;

                        res.ListProced.Add(proced);

                        res.DTGuia = DTGuia;

                        bsReport.Add(res);
                    }
                }

                return 1;
            }
            catch { return 0; }
        }

        private class Resultado
        {
            public string NomePaci { get; set; }
            public string PaciCart { get; set; }
            public string PaciCartSus { get; set; }
            public string PaciValid { get; set; }

            public string NomeProfi { get; set; }
            public string ProfiUfEntid { get; set; }
            public string ProfiNumEntid { get; set; }
            public string ProfiSiglaEntid { get; set; }

            public string DTGuia { get; set; }

            public string TpConsulta { get; set; }

            public List<Procedimento> ListProced { get; set; }
        }

        private class Procedimento
        {
            public int Index { get; set; }
            public int? IdProced { get; set; }
            public string TabelaProced { get; set; }
            public string NoProced { get; set; }
            public string CoProced { get; set; }
            public int? QtProced { get; set; }
            public String IndexV
            {
                get
                {
                    return this.Index + " -  " + this.TabelaProced;
                }
            }
        }
    }
}

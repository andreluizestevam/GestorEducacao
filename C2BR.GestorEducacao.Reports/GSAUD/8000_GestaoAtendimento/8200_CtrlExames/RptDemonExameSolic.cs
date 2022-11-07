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
using C2BR.GestorEducacao.Reports;

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8200_CtrlExames
{
    public partial class RptDemonExameSolic : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptDemonExameSolic()
        {
            InitializeComponent();
        }

        public int InitReport(
              string nomeFunc,
              string parametros,
              string infos,
              int coEmp,
              int Paciente,
              int Operadora,
              int Procedimento,
              DateTime dataIni,
              DateTime dataFim
            )
        {
            try
            {
                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.Parametros = parametros;
                lblTitulo.Text = !String.IsNullOrEmpty(nomeFunc) ? nomeFunc.ToUpper() : "-";

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros().Where(a => a.CO_ALU == Paciente)
                           //join tbs416 in TBS416_EXAME_RESUL.RetornaTodosRegistros() on tb07.CO_ALU equals tbs416.TB07_ALUNO.CO_ALU
                           select new Resultado
                           {
                               Paciente = tb07.NO_ALU,
                               DataNasc = tb07.DT_NASC_ALU,
                               Sexo = !String.IsNullOrEmpty(tb07.CO_SEXO_ALU) ? tb07.CO_SEXO_ALU == "F" ? "Feminino" : "Masculino" : "-",
                               tipSanPac = tb07.CO_TIPO_SANGUE_ALU,
                               defPac = tb07.TBS387_DEFIC.NM_SIGLA_DEFIC,
                               nirePac = tb07.NU_NIRE,
                               mailPac = tb07.NO_WEB_ALU,
                               rgPac = tb07.CO_RG_ALU + " - " + (!String.IsNullOrEmpty(tb07.CO_ORG_RG_ALU) ? tb07.CO_ORG_RG_ALU + "/" + tb07.CO_ESTA_RG_ALU : ""),
                               cpfPac = tb07.NU_CPF_ALU,
                               Operadora = !String.IsNullOrEmpty(tb07.TB250_OPERA.NM_SIGLA_OPER) ? tb07.TB250_OPERA.NM_SIGLA_OPER : "-",
                               Plano = !String.IsNullOrEmpty(tb07.TB251_PLANO_OPERA.NM_SIGLA_PLAN) ? tb07.TB251_PLANO_OPERA.NM_SIGLA_PLAN : "-",
                               numPlano = tb07.NU_PLANO_SAUDE,
                               validPlano = tb07.DT_VENC_PLAN
                           }).FirstOrDefault();

                if (res == null)
                    return -1;
                
                dataFim = dataFim.AddDays(1);

                var results = (from tbs416 in TBS416_EXAME_RESUL.RetornaTodosRegistros().Where(e => e.DT_CADAS >= dataIni && e.DT_CADAS <= dataFim)
                               where tbs416.TB07_ALUNO.CO_ALU == Paciente
                               && (Procedimento != 0 ? tbs416.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == Procedimento : true)
                               && (Operadora != 0 ? tbs416.TBS356_PROC_MEDIC_PROCE.TB250_OPERA.ID_OPER == Operadora : true)
                               select tbs416).ToList();

                if (results.Count == 0)
                    return -1;

                res.Procedimentos = new List<Procedimento>();

                foreach (var r in results)
                {
                    r.TBS417_EXAME_RESUL_ITENS.Load();
                    r.TBS356_PROC_MEDIC_PROCEReference.Load();

                    var proced = new Procedimento();
                    if (res.Procedimentos.Where(x => x.Id == r.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE).FirstOrDefault() != null)
                        proced = res.Procedimentos.Where(x => x.Id == r.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE).FirstOrDefault();
                    else
                    {
                        proced.Id = r.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE;
                        proced.Proced = r.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI;

                        proced.GruposProced = new List<GrupoProced>();

                        res.Procedimentos.Add(proced);
                    }

                    foreach (var i in r.TBS417_EXAME_RESUL_ITENS)
                    {
                        i.TBS414_EXAME_ITENS_AVALIReference.Load();
                        i.TBS414_EXAME_ITENS_AVALI.TBS413_EXAME_SUBGRReference.Load();
                        i.TBS414_EXAME_ITENS_AVALI.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPOReference.Load();
                        i.TBS414_EXAME_ITENS_AVALI.TBS415_EXAME_ITENS_REFER.Load();

                        var grup = new GrupoProced();
                        if (proced.GruposProced.Where(x => x.Id == i.TBS414_EXAME_ITENS_AVALI.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.ID_GRUPO).FirstOrDefault() != null)
                            grup = proced.GruposProced.Where(x => x.Id == i.TBS414_EXAME_ITENS_AVALI.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.ID_GRUPO).FirstOrDefault();
                        else
                        {
                            grup.Id = i.TBS414_EXAME_ITENS_AVALI.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.ID_GRUPO;
                            grup.Grupo = i.TBS414_EXAME_ITENS_AVALI.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.NO_GRUPO_EXAME;
                            grup.Metodo = i.TBS414_EXAME_ITENS_AVALI.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.DE_METOD_EXAME;
                            grup.Material = i.TBS414_EXAME_ITENS_AVALI.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.DE_MATER_EXAME;
                            grup.Objetivo = i.TBS414_EXAME_ITENS_AVALI.TBS413_EXAME_SUBGR.TBS412_EXAME_GRUPO.DE_GRUPO_EXAME;
                            
                            grup.SubgruposProced = new List<SubgrupoProced>();

                            proced.GruposProced.Add(grup);
                        }

                        var subgr = new SubgrupoProced();
                        if (grup.SubgruposProced.Where(x => x.Id == i.TBS414_EXAME_ITENS_AVALI.TBS413_EXAME_SUBGR.ID_SUBGRUPO).FirstOrDefault() != null)
                            subgr = grup.SubgruposProced.Where(x => x.Id == i.TBS414_EXAME_ITENS_AVALI.TBS413_EXAME_SUBGR.ID_SUBGRUPO).FirstOrDefault();
                        else
                        {
                            subgr.Id = i.TBS414_EXAME_ITENS_AVALI.TBS413_EXAME_SUBGR.ID_SUBGRUPO;
                            subgr.Subgrupo = i.TBS414_EXAME_ITENS_AVALI.TBS413_EXAME_SUBGR.NO_SUBGR_EXAME;

                            subgr.ItensAvaliacao = new List<ItemAvaliacao>();

                            grup.SubgruposProced.Add(subgr);
                        }
                        
                        var item = new ItemAvaliacao();
                        if (subgr.ItensAvaliacao.Where(x => x.Id == i.TBS414_EXAME_ITENS_AVALI.ID_ITENS_AVALI).FirstOrDefault() != null)
                            item = subgr.ItensAvaliacao.Where(x => x.Id == i.TBS414_EXAME_ITENS_AVALI.ID_ITENS_AVALI).FirstOrDefault();
                        else
                        {
                            item.Id = i.TBS414_EXAME_ITENS_AVALI.ID_ITENS_AVALI;
                            item.Item = i.TBS414_EXAME_ITENS_AVALI.NO_ITEM_AVALI + "        " + i.VL_RESUL_ITENS;
                            item.Resultado = i.VL_RESUL_ITENS;
                            item.Unidade = "";

                            item.Referencias = new List<VlReferencia>();

                            subgr.ItensAvaliacao.Add(item);
                        }

                        foreach (var rf in i.TBS414_EXAME_ITENS_AVALI.TBS415_EXAME_ITENS_REFER)
                        {
                            var refer = new VlReferencia();
                            if (item.Referencias.Where(x => x.Id == rf.ID_ITENS_REFER).FirstOrDefault() != null)
                                refer = item.Referencias.Where(x => x.Id == rf.ID_ITENS_REFER).FirstOrDefault();
                            else
                            {
                                refer.Id = rf.ID_ITENS_REFER;
                                refer.Referencia = rf.NO_ITENS_REFER;
                                refer.Sexo = rf.FL_SEXO_REFER;
                                refer.Unidade = rf.TP_UNIDA_REFER;
                                refer.VlInicial = rf.VL_INICI_REFER;
                                refer.VlFinal = rf.VL_FINAL_REFER;

                                if (!String.IsNullOrEmpty(refer.Unidade) && String.IsNullOrEmpty(item.Unidade))
                                {
                                    item.Unidade = refer.Unidade;
                                    item.Item = item.Item + " " + item.Unidade;
                                }

                                item.Referencias.Add(refer);
                            }
                        }
                    }
                }

                //Adiciona ao DataSource do Relatório
                bsReport.Clear();
                bsReport.Add(res);
                return 1;

            }
            catch { return 0; }
        }

        public class Resultado
        {
            public DateTime DataInical { get; set; }
            public DateTime DataFinal { get; set; }
            public string Profissional { get; set; }
            public int CodPac { get; set; }
            public string Paciente { get; set; }
            public string Sexo { get; set; }
            public DateTime? DataNasc { get; set; }
            public string Plano { get; set; }
            public string Operadora { get; set; }
            public string tipSanPac { get; set; }
            public string defPac { get; set; }
            public int nirePac { get; set; }
            public string mailPac { get; set; }
            public string rgPac { get; set; }
            public string cpfPac { get; set; }
            public string numPlano { get; set; }
            public DateTime? validPlano { get; set; }

            public List<Procedimento> Procedimentos { get; set; }
        }

        public class Procedimento
        {
            public int Id { get; set; }

            public string Proced { get; set; }

            public List<GrupoProced> GruposProced { get; set; }
        }

        public class GrupoProced
        {
            public int Id { get; set; }

            public string Grupo { get; set; }

            public string Metodo { get; set; }

            public string Material { get; set; }

            public string Objetivo { get; set; }

            public List<SubgrupoProced> SubgruposProced { get; set; }
        }

        public class SubgrupoProced
        {
            public int Id { get; set; }

            public string Subgrupo { get; set; }

            public List<ItemAvaliacao> ItensAvaliacao { get; set; }

            public List<ItemResultado> Resultados { get; set; }
        }

        public class ItemResultado
        {
            public int IdItem { get; set; }

            public string Unidade { get; set; }

            public string Linha1 { get; set; }

            public string Linha2 { get; set; }
        }

        public class ItemAvaliacao
        {
            public int Id { get; set; }

            public string Item { get; set; }

            public string Resultado { get; set; }

            public string Unidade { get; set; }

            public List<VlReferencia> Referencias { get; set; }
        }

        public class VlReferencia
        {
            public int Id { get; set; }

            public string Referencia { get; set; }

            public string Sexo { get; set; }

            public string Unidade { get; set; }

            public string VlInicial { get; set; }

            public string VlFinal { get; set; }
        }
    }
}

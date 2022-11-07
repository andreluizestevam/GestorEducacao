using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.Reports.Helper;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1900_TabelasGenerCtrlAdmEscolar
{
    public partial class RptRelacCursosFormacao : C2BR.GestorEducacao.Reports.RptRetrato
    {
        #region ctor

        public RptRelacCursosFormacao()
        {
            InitializeComponent();
        } 

        #endregion

        #region Init Report

        public int InitReport(int codEmp, string tiposCursos, string infos)
        {
            try
            {
                #region Inicializa o header/Labels

                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;

                // Cria o header a partir do cod da instituicao
                var header = ReportHeader.GetHeaderFromEmpresa(codEmp);
                if (header == null)
                    return -1;

                // Inicializa o header
                base.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Cursos

                var lstCur = (from cf in ctx.TB100_ESPECIALIZACAO
                              select new Curso
                              {
                                  Pontuacao = cf.QT_PONTU,
                                  Sigla = cf.NO_SIGLA_ESPEC,
                                  Promocao = (cf.FLA_PROMO == "S") ? "Sim" : "Nao",
                                  Nome = cf.DE_ESPEC,
                                  Tipo = cf.TP_ESPEC
                              });

                #endregion

                string[] tipos = tiposCursos.Split(',');
                if (tipos.Length == 0)
                    return -1;

                List<Curso> lst = new List<Curso>();
                if (!tipos.Contains("0")) // Nao selecionou a opcao todos 
                {
                    foreach (var t in tipos)
                        lst.AddRange(lstCur.Where(x => x.Tipo == t).ToList());
                }
                else
                {
                    lst.AddRange(lstCur.ToList());
                }

                // Se não encontrou cursos
                if (lst.Count() == 0)
                    return -1;

                // Adiciona os Cursos ao DataSource do Relatório
                bsReport.Clear();
                foreach (var c in lst)
                    bsReport.Add(c);

                return 1;
            }
            catch { return 0; }
        } 

        #endregion

        #region Cursos Helper

        public class Curso
        {
            public string Nome { get; set; }
            public int? Pontuacao { get; set; }
            public string PontuacaoStr
            {
                get { return (Pontuacao.HasValue) ? Pontuacao.Value.ToString("n0") : "-"; }
            }

            public string Sigla { get; set; }
            public string Promocao { get; set; }
            public string Tipo { get; set; }
            public string TipoStr
            {
                get { return Funcoes.GetTipoEspecializacao(this.Tipo); }
            }
        } 

        #endregion
    }
}

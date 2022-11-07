using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.Reports.Helper;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Globalization;
using System.Data;
using System.Web;
using System.Resources;

namespace C2BR.GestorEducacao.Reports.GSAUD._7000_ControleOperRH._7100_CtrlCadastralInfosFuncionais._7120_DemonColaborComplet
{
    public partial class RptRelFuncParamComNPorUnid : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptRelFuncParamComNPorUnid()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                        string infos,
                        int coEmp,
                        int UnidCadastro,
                        int UnidContrato,
                        int regiao,
                        int area,
                        int subarea,
                        string uf,
                        int Cidade,
                        int Bairro,
                        int classFunc,
                        int categoria,
                        int especializa,
                        int pla
                        )
        {


            try
            {
                #region Setar o Header e as Labels

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

                #region desuso
                //var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                //           join tb128 in TB128_FUNCA_FUNCI.RetornaTodosRegistros() on tb03.TB128_FUNCA_FUNCI.ID_FUNCA_FUNCI equals tb128.ID_FUNCA_FUNCI
                //           where (classFunc != 0 ? tb128.ID_FUNCA_FUNCI == classFunc : 0 == 0)
                //           && (UnidContrato != 0 ? tb03.CO_EMP_UNID_CONT == UnidContrato : 0 == 0)
                //           && (uf != "0" ? tb03.CO_ESTA_ENDE_COL == uf : 0 == 0)
                //           && (Cidade != 0 ? tb03.CO_CIDADE == Cidade : 0 == 0)
                //           && (Bairro != 0 ? tb03.CO_BAIRRO == Bairro : 0 == 0)
                //           && (categoria != 0 ? tb03.TB127_CATEG_FUNCI.ID_CATEG_FUNCI == categoria : 0 == 0)
                //           && (especializa != 0 ? tb03.CO_ESPEC == especializa : 0 == 0)
                //           select new
                //           {
                //               coEmp = tb03.CO_EMP,
                //               noEmp = tb25.NO_FANTAS_EMP,

                //               noFunc = tb128.NO_FUNCA_FUNCI,
                //               flPermPlant = tb03.FL_PERM_PLANT,
                //               flAtiviInter = tb03.FL_ATIVI_INTER,
                //               flAtiviExter = tb03.FL_ATIVI_EXTER,
                //               flAtiviDomic = tb03.FL_ATIVI_DOMIC,
                //               coSitu = tb03.CO_SITU_COL,
                //               coTpCon = tb03.CO_TPCON
                //           }).ToList();

                //var l = (from r in res
                //         group r by new
                //         {
                //             noFunc = r.noFunc
                //         } into g
                //         select new DemonstrativoDistruibuicaoFunc
                //         {
                //             noFuncao = g.Key.noFunc,

                //             ATI = g.Count(c => c.coSitu == "ATI"),
                //             FER = g.Count(c => c.coSitu == "FER"),
                //             LME = g.Count(c => c.coSitu == "LME"),
                //             LMA = g.Count(c => c.coSitu == "LMA"),

                //             QPL = g.Count(c => c.flPermPlant == "S"),
                //             QAI = g.Count(c => c.flAtiviInter == "S"),
                //             QAE = g.Count(c => c.flAtiviExter == "S"),
                //             QAD = g.Count(c => c.flAtiviDomic == "S"),

                //             CLT = g.Count(c => c.coTpCon == 7),
                //             EST = g.Count(c => c.coTpCon == 5),
                //             RPA = g.Count(c => c.coTpCon == 6),
                //             PJ = g.Count(c => c.coTpCon == 3),
                //             CTR = g.Count(c => c.coTpCon == 8),
                //             OTR = g.Count(c => c.coTpCon == 14),

                //             total = g.Count()
                //         }).ToList();
                #endregion

                    var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                               join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb03.CO_EMP equals tb25.CO_EMP                               
                               //join uCad in TB25_EMPRESA.RetornaTodosRegistros() on tb03.CO_EMP equals uCad.CO_EMP
                               join uCon in TB25_EMPRESA.RetornaTodosRegistros() on tb03.CO_EMP_UNID_CONT equals uCon.CO_EMP
                               //join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals  tb63.CO_ESPEC
                               where (classFunc != 0 ? tb03.TB128_FUNCA_FUNCI.ID_FUNCA_FUNCI == classFunc : 0 == 0)
                               && (UnidCadastro != 0 ? tb03.CO_EMP == UnidCadastro : 0 == 0)
                               && (UnidContrato != 0 ? tb03.CO_EMP_UNID_CONT == UnidContrato : 0 == 0)
                               && (uf != "0" ? tb03.CO_ESTA_ENDE_COL == uf : 0 == 0)
                               && (Cidade != 0 ? tb03.CO_CIDADE == Cidade : 0 == 0)
                               && (Bairro != 0 ? tb03.CO_BAIRRO == Bairro : 0 == 0)
                               && (categoria != 0 ? tb03.TB127_CATEG_FUNCI.ID_CATEG_FUNCI == categoria : 0 == 0)
                               && (especializa != 0 ? tb03.CO_ESPEC == especializa : 0 == 0)
                               && (pla != 0 ? tb03.FL_PERM_PLANT == "S" : 0 == 0)
    
                               select new RelFuncionalColabor
                               {
                                   //Informações do Funcionário
                                   nMatricula = tb03.CO_MAT_COL,
                                   noCol = tb03.NO_COL,
                                   apCol = tb03.NO_APEL_COL,
                                   dtNasc = tb03.DT_NASC_COL,
                                   Defi = tb03.TP_DEF,

                                   //Informações do Cargo
                                   funcao = tb03.TB128_FUNCA_FUNCI.CO_FUNCA_FUNCI,
                                   dtAdm = tb03.DT_INIC_ATIV_COL,

                                   //Informações da Empresa
                                   unidCadastro = tb25.sigla,
                                   unidContr = uCon.sigla,

                               }).OrderBy(m => m.noCol).ThenBy(n => n.funcao).ToList();

                #endregion


                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                foreach (RelFuncionalColabor at in res)
                    bsReport.Add(at);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class RelFuncionalColabor
        {
            //situação do Funcionário
            public string nMatricula { get; set; }
            public string noCol { get; set; }
            public string apCol { get; set; }
            public DateTime dtNasc { get; set; }
            public string dtNascV
            {
                get
                {
                    return this.dtNasc.ToString("dd/MM/yy");
                }
            }
            public string idade 
            {
                get
                {
                    DateTime dtNasci = this.dtNasc;
                    int anos = DateTime.Now.Year - dtNasci.Year;

                    if (DateTime.Now.Month < dtNasci.Month || (DateTime.Now.Month == dtNasci.Month && DateTime.Now.Day < dtNasci.Day))
                        anos--;

                    string idade = anos.ToString();

                    return idade;

                    //timespan id = datetime.now.subtract(dtnasc);
                    //return id.tostring();
                }
            }
            public string Defi { get; set; }
            public string DefiValid
            {
                get
                {
                    if (this.Defi == "N")
                    {
                        return "NÃO";
                    }
                    else
                    {
                        return "SIM";
                    }
                }
            }

            public string regiao { get; set; }
            public string area { get; set; }
            public string subarea { get; set; }

            //Informações do Cargo
            public DateTime dtAdm { get; set; }
            public string dtAdmV
            {
                get
                {
                   return this.dtAdm.ToString("dd/MM/yy");
                }
            }
            public string tempEmp
            {
                get
                {
                    DateTime dtTem = this.dtAdm;
                    int anos = DateTime.Now.Year - dtTem.Year;

                    if (DateTime.Now.Month < dtTem.Month || (DateTime.Now.Month == dtTem.Month && DateTime.Now.Day < dtTem.Day))
                        anos--;

                    string tmp = anos.ToString();

                    return tmp;
                }
            }
            public string funcao { get; set; }

            //Informações da Unidade
            public string unidContr { get; set; }
            public string unidCadastro { get; set; }
        }
    }
}


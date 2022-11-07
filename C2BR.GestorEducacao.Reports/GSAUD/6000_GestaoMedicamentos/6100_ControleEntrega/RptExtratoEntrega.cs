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

namespace C2BR.GestorEducacao.Reports.GSAUD._6000_GestaoMedicamentos._6100_ControleEntrega
{
    public partial class RptExtratoEntrega : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptExtratoEntrega()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                                int coEmp,
                                string infos,
                                string tpRel,
                                int idReser,
                                int coUsuario,
                                int coResp,
                                DateTime? dtIniSolic,
                                DateTime? dtFimSolic,
                                DateTime? dtIniEntre,
                                DateTime? dtFimEntre,
                                int? coProd)
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                switch (tpRel)
                {
                    case "E":
                        lblTitulo.Text = "EXTRATO DE ENTREGA DE MEDICAMENTOS";
                        break;
                    case "G":
                        lblTitulo.Text = "GUIA DE ENTREGA DE MEDICAMENTOS";
                        break;
                }

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                TB25_EMPRESA emp = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
                emp.TB24_TPEMPRESAReference.Load();
                emp.TB000_INSTITUICAOReference.Load();
                TB904_CIDADE cidadeEmp = TB904_CIDADE.RetornaPelaChavePrimaria(emp.CO_CIDADE);
                TB905_BAIRRO bairroEmp = TB905_BAIRRO.RetornaPelaChavePrimaria(emp.CO_BAIRRO);

                lblInst.Text = emp.TB000_INSTITUICAO.ORG_NOME_FANTAS_ORGAO;

                lblUnidLogin.Text = "Unidade de Atendimento (UA): " + emp.NO_FANTAS_EMP.ToUpper();

                lblCidadeGuia.Text = "( " + bairroEmp.NO_BAIRRO + " - " + cidadeEmp.NO_CIDADE + " - " + emp.CO_UF_EMP + " )";

                lblTituloGuia.Text = "PROTOCOLO DE ENTREGA DE MEDICAMENTO(S)";

                switch (emp.TB24_TPEMPRESA.CO_TIPO_SIST)
                {
                    case "SAU":
                        xtcItem.Text = "MEDICAMENTO";
                        break;
                }

                switch (tpRel)
                {
                    case "E":
                        linhaGuia.Visible     = false;
                        lblInst.Visible       = false;
                        lblUnidLogin.Visible  = false;
                        lblTituloGuia.Visible = false;
                        lblTextoGuia.Visible  = false;
                        assGuia.Visible       = false;
                        lblRespGuia.Visible   = false;
                        lblResp.Visible       = false;
                        lbl5Guia.Visible      = false;
                        lblTerAssGuia.Visible = false;
                        lblTerNomGuia.Visible = false;
                        break;
                    case "G":
                        linhaGuia.Visible     = true;
                        lblInst.Visible       = true;
                        lblUnidLogin.Visible  = true;
                        lblTituloGuia.Visible = true;
                        lblTextoGuia.Visible  = true;
                        assGuia.Visible       = true;
                        lblRespGuia.Visible   = true;
                        lblResp.Visible       = true;
                        lbl5Guia.Visible      = true;
                        lblTerAssGuia.Visible = true;
                        lblTerNomGuia.Visible = true;
                        break;
                }

                #region Query

                var res = (from tb092 in TB092_RESER_MEDIC.RetornaTodosRegistros()
                           join tb094 in TB094_ITEM_RESER_MEDIC.RetornaTodosRegistros() on tb092.ID_RESER_MEDIC equals tb094.TB092_RESER_MEDIC.ID_RESER_MEDIC
                           join tb109 in TB109_DETAL_ENTRE.RetornaTodosRegistros() on tb094.ID_ITEM_RESER_MEDIC equals tb109.TB094_ITEM_RESER_MEDIC.ID_ITEM_RESER_MEDIC
                           join tb90 in TB90_PRODUTO.RetornaTodosRegistros() on tb094.TB90_PRODUTO.CO_PROD equals tb90.CO_PROD
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb092.CO_ALU equals tb07.CO_ALU
                           join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tb092.CO_RESP equals tb108.CO_RESP
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb092.CO_EMP equals tb25.CO_EMP
                           where (idReser != 0 ? tb092.ID_RESER_MEDIC == idReser : 0 == 0)
                           && (coUsuario != 0 ? tb092.CO_ALU == coUsuario : 0 == 0)
                           && (coResp != 0 ? tb092.CO_RESP == coResp : 0 == 0)
                           && ((dtIniSolic != null ? tb092.DT_RESER_MEDIC >= dtIniSolic : 0 == 0) && (dtFimSolic != null ? tb092.DT_RESER_MEDIC <= dtFimSolic : 0 == 0))
                           && ((dtIniEntre != null ? tb109.DT_ENTRE >= dtIniEntre : 0 == 0) && (dtFimEntre != null ? tb109.DT_ENTRE <= dtFimEntre : 0 == 0))
                           && (coProd != null ? tb094.TB90_PRODUTO.CO_PROD == coProd : 0 == 0)
                           select new ExtratoEntrega
                           {
                               idItemSoli = tb094.ID_ITEM_RESER_MEDIC,
                               noUsuario = tb07.NO_ALU,
                               noResp = tb108.NO_RESP,
                               coReser = tb092.CO_RESER_MEDIC,
                               idSoli = tb092.ID_RESER_MEDIC,
                               stReser = tb092.ST_RESER_MEDIC,
                               unidEntre = tb109.CO_EMP_ENTRE,
                               unidSolic = tb092.CO_EMP,
                               dtSolic = tb092.DT_RESER_MEDIC,
                               dtEntre = tb109.DT_ENTRE,
                               usuNirs = tb07.NU_NIRE,
                               noProdRed = tb90.NO_PROD_RED,
                               coRgResp = tb108.CO_RG_RESP,
                               coRgRespOrg = tb108.CO_ORG_RG_RESP,
                               noEmp = tb25.NO_FANTAS_EMP,
                               qtEntre = tb109.QT_ENTREGA,
                               qtMes1 = tb094.QT_MES1,
                               qtMes2 = tb094.QT_MES2,
                               qtMes3 = tb094.QT_MES3,
                               qtMes4 = tb094.QT_MES4,
                               mesRef = tb109.MES_REF,
                               usuNasc = tb07.DT_NASC_ALU,
                               usuSexo = tb07.CO_SEXO_ALU,
                               cpfResp = tb108.NU_CPF_RESP,
                               cnesResp = tb108.NU_NIS_RESP
                           }).Distinct().ToList();

                #endregion

                // Erro: não encontrou registros
                if (res.Count == 0)
                    return -1;

                // Seta os alunos no DataSource do Relatorio
                bsReport.Clear();
                int i = 1;
                int nirs = 0;
                foreach (ExtratoEntrega at in res)
                {
                    if (at.usuNirs != nirs)
                    {
                        nirs = at.usuNirs;
                        i = 1;
                    }
                            
                    at.cont = i;
                    bsReport.Add(at);
                    i++;
                }

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class ExtratoEntrega
        {
            public int cont { get; set; }
            public DateTime? usuNasc { get; set; }
            public string dtNascUsu
            {
                get
                {
                    string r = "";
                    DateTime d;
                    TimeSpan td;

                    if (this.usuNasc != null)
                    {
                        td = DateTime.Today - this.usuNasc.Value;

                        d = (new DateTime() + td).AddYears(-1).AddDays(-1);

                        r = this.usuNasc.Value.ToString("dd/MM/yyyy") + " ( " + d.Year.ToString() + "a )";
                    }
                    else
                    {
                        r = "****";
                    }

                    return r;
                }
            }
            public string usuSexo { get; set; }
            public string sexo
            {
                get
                {
                    string s = "";

                    switch (this.usuSexo)
                    {
                        case "M":
                            s = "MAS";
                            break;
                        case "F":
                            s = "FEM";
                            break;
                    }

                    return s;
                }
            }
            public int usuNirs { get; set; }
            public string Nirs
            {
                get
                {
                    return this.usuNirs.ToString().PadLeft(8, '0');
                }
            }
            public int usuCnes { get; set; }
            public string Cnes
            {
                get
                {
                    return this.usuCnes.ToString().PadLeft(10, '0');
                }
            }
            public int idSoli { get; set; }
            public int idItemSoli { get; set; }
            public string coReser { get; set; }
            public string stReser { get; set; }
            public string stReserItem
            {
                get
                {
                    string s = "";

                    switch (this.stReser)
                    {
                        case "A":
                            s = "Em Aberto";
                            break;
                        case "P":
                            s = "Entrega Parcial";
                            break;
                        case "T":
                            s = "Entregue";
                            break;
                        case "C":
                            s = "Cancelado";
                            break;
                    }

                    return s;
                }
            }
            public string coReserItem
            {
                get
                {
                    return Convert.ToInt64(this.coReser).ToString(@"0000\.000\.0000000");
                }
            }
            public string noEmp { get; set; }
            public int unidSolic { get; set; }
            public string noUnidSolic
            {
                get
                {
                    return TB25_EMPRESA.RetornaPelaChavePrimaria(this.unidSolic).sigla;
                }
            }
            public int unidEntre { get; set; }
            public string noUnidEntre
            {
                get
                {
                    return TB25_EMPRESA.RetornaPelaChavePrimaria(this.unidEntre).sigla;
                }
            }
            public DateTime dtSolic { get; set; }
            public string dtSolicItem
            {
                get
                {
                    return this.dtSolic.ToString("dd/MM/yyyy");
                }
            }
            public DateTime dtEntre { get; set; }
            public string dtEntreItem
            {
                get
                {
                    return this.dtEntre.ToString("dd/MM/yyyy");
                }
            }
            public string noUsuario { get; set; }
            public string noResp { get; set; }
            public string cpfResp { get; set; }
            public string cpf
            {
                get
                {
                    return this.cpfResp.Substring(0, 3) + "." + this.cpfResp.Substring(3, 3) + "." + this.cpfResp.Substring(6, 3) + "-" + this.cpfResp.Substring(9, 2);
                }
            }
            public decimal? cnesResp { get; set; }
            public string detalheResp
            {
                get
                {
                    return "Responsável: " + this.noResp + " - CPF: " + this.cpf + " - CNES: " + this.respCnes;
                }
            }
            public string detalheUsua
            {
                get
                {
                    return "Sexo: " + this.sexo + " - Nascto: " + this.dtNascUsu + " - NIRS: " + this.Nirs + " - CNES: " + this.Cnes;
                }
            }
            public string respCnes
            {
                get
                {
                    return this.cnesResp.ToString().PadLeft(10,'0');
                }
            }
            public string empResp { get; set; }
            public decimal qtEntre { get; set; }
            public decimal qtMes1 { get; set; }
            public decimal qtMes2 { get; set; }
            public decimal qtMes3 { get; set; }
            public decimal qtMes4 { get; set; }
            public int mesRef { get; set; }
            public decimal qtSaldo
            {
                get
                {
                    decimal q = 0;
                    decimal m = 0;

                    switch (this.mesRef)
                    {
                        case 1:
                            m = this.qtMes1;
                            break;
                        case 2:
                            m = this.qtMes2;
                            break;
                        case 3:
                            m = this.qtMes3;
                            break;
                        case 4:
                            m = this.qtMes4;
                            break;
                    }

                    q = m - this.qtEntre;

                    return q;
                }
            }
            public string noProdRed { get; set; }
            public string coRgResp { get; set; }
            public string coRgRespOrg { get; set; }
            public string lblRespGuia
            {
                get
                {
                    return this.coRgResp + "/" + this.coRgRespOrg + " - " + this.noResp;
                }
            }
            public string lblTextGuia
            {
                get
                {
                    return "Declaro ter recebido nesta data  ___/___/" + DateTime.Now.Year + " o(s) medicamento(s) e quantidade(s) descrita(s) na Guia de Entrega (Solicitação N° " + Convert.ToInt64(this.coReser).ToString(@"0000\.000\.0000000") + " - Unidade: " + this.noEmp.ToUpper() + ") referente ao usuário de saúde " + this.noUsuario.ToUpper() + " (NIRS: " + this.usuNirs + ").";
                }
            }
            public string lblTituloGuia
            {
                get
                {
                    return "PROTOCOLO DE ENTREGA DE MEDICAMENTO(S) - " + Convert.ToInt64(this.coReser).ToString(@"0000\.000\.0000000");
                }
            }
        }
    }
}

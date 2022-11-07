using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.Reports._5000_CtrlFinanceira._5500_GeraFluxoCaixa
{
    public partial class RptReciboTitulos : C2BR.GestorEducacao.Reports.RptRetrato
    {
        #region ctor

        public RptReciboTitulos()
        {
            InitializeComponent();
        }

        #endregion

        public int InitReport(int co_col,
                              int co_emp,
                              int co_resp,
                              List<int> co_alu,
                              string vl_total,
                              DateTime dtPagto,
                              string vl_desc,
                              string sub_total,
                              string vl_dinheiro,
                              string vl_debito,
                              string vl_credito,
                              string vl_cheque,
                              string vl_transferencia,
                              string vl_boleto,
                              string vl_recibo,
                              string vl_outros,
                              string Total,
                              string parametros = "",
                              string infos = "",
                              string multa = "")
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(co_emp);

                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                #region Alunos

                List<Aluno> listAlunos = new List<Aluno>();
                foreach (var coalu in co_alu)
                {
                    var alunos = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                  join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb48.CO_ALU equals tb07.CO_ALU
                                  join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb48.CO_MODU_CUR equals tb44.CO_MODU_CUR
                                  join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb48.CO_CUR equals tb01.CO_CUR
                                  join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb07.CO_TUR equals tb129.CO_TUR
                                  where tb07.CO_ALU == coalu
                                  select new Aluno
                                  {
                                      NO_ALU = tb07.NO_ALU,
                                      MODULO = tb44.DE_MODU_CUR,
                                      SERIE = tb01.NO_CUR,
                                      TURMA = tb129.NO_TURMA
                                  }
                                 ).OrderBy(x => x.NO_ALU).ToList().FirstOrDefault();

                    if (alunos != null)
                    {
                        listAlunos.Add(alunos);
                    }
                    else
                    {
                        return -1;
                    }
                }

                #endregion

                #region Responsavel

                var responsavel = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                   where tb108.CO_RESP == co_resp
                                   select new
                                   {
                                       tb108.NO_RESP,
                                       tb108.NU_CPF_RESP
                                   }).FirstOrDefault();

                if (responsavel == null)
                    return -1;

                #endregion

                #region Empresa

                var empresa = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                               where tb25.CO_EMP == co_emp
                               select new
                               {
                                   tb25.NO_FANTAS_EMP,
                                   tb25.TB000_INSTITUICAO.ORG_NUMERO_CNPJ,
                                   tb25.TB000_INSTITUICAO.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                                   tb25.TB000_INSTITUICAO.CID_CODIGO_UF
                               }).FirstOrDefault();

                #endregion

                #region Titulos
                if (empresa == null)
                    return -1;


                string valorTotal = toExtenso(decimal.Parse(vl_total));


                string dia = dtPagto.Day.ToString();
                var d = dtPagto.ToString("ddddddddddddddddddddddddddddd");
                string diaDaSemana = char.ToUpper(d[0]) + d.Substring(1);
                string m = dtPagto.ToString("MMMM");
                string mes = char.ToUpper(m[0]) + m.Substring(1);
                string ano = dtPagto.Year.ToString();
                var titulos = new Titulo
                                {
                                    ANO = ano,
                                    Aluno = listAlunos,
                                    CIDADE = empresa.NO_CIDADE + "-" + empresa.CID_CODIGO_UF,
                                    CNPJ_RESP = empresa.ORG_NUMERO_CNPJ.ToString(),
                                    DESCONTO = vl_desc,
                                    DIA = dia,
                                    DIA_SEMANA = diaDaSemana,
                                    MES = mes,
                                    NO_EMP = empresa.NO_FANTAS_EMP,
                                    NO_RESP = responsavel.NO_RESP,
                                    NU_RESP_CPF = responsavel.NU_CPF_RESP,
                                    SUB_TOTAL = sub_total,
                                    VL_BOLETO = vl_boleto,
                                    VL_CHEQUE = vl_cheque,
                                    VL_CREDITO = vl_credito,
                                    VL_DEBITO = vl_debito,
                                    VL_DINHERIO = vl_dinheiro,
                                    VL_OUTROS = vl_outros,
                                    VL_RECIBO = vl_recibo,
                                    VL_TOTAL = vl_total,
                                    TOTAL_TITULO = Total,
                                    VL_TRANFERENNCIA = vl_transferencia,
                                    DESC_VALOR = valorTotal != null ? valorTotal.ToString() : "",
                                    USUARIO_EMISSOR = TB03_COLABOR.RetornaPelaChavePrimaria(co_emp, co_col).NO_COL
                                };



                #endregion
                bsReport.Clear();
                bsReport.Add(titulos);

                return 1;
            }
            catch { return 0; }
        }

        #region Class Helper

        public class Aluno
        {
            public string NO_ALU { get; set; }
            public string MODULO { get; set; }
            public string SERIE { get; set; }
            public string TURMA { get; set; }
        }

        public class Titulo
        {
            public string NO_RESP { get; set; }
            public string CNPJ_RESP { get; set; }
            public string NU_RESP_CPF { get; set; }
            public string Info 
            {
                get 
                {
                    return "Recebemos de " + this.NO_RESP + ", CPF " + this.NU_RESP_CPF + " a quantia de " + this.total + " (" + this.DESC_VALOR + ") referente ao pagamento de títulos. Sendo verdade, firmamos o presente.";
                }
            }
            public string VL_TOTAL { get; set; }
            public string DESC_VALOR { get; set; }
            public string SUB_TOTAL { get; set; }
            public string DESCONTO { get; set; }
            public string CIDADE { get; set; }
            public string DIA { get; set; }
            public string DIA_SEMANA { get; set; }
            public string ANO { get; set; }
            public string MES { get; set; }
            public string DT_TITULO
            {
                get 
                { 
                    return this.CIDADE + ", " + this.DIA_SEMANA + " " + this.DIA + " de " + this.MES + " de " + this.ANO;
                }
            }
            public string NO_EMP { get; set; }
            public string VL_DINHERIO { get; set; }
            public string VL_DEBITO { get; set; }
            public string VL_CREDITO { get; set; }
            public string VL_CHEQUE { get; set; }
            public string VL_TRANFERENNCIA { get; set; }
            public string VL_BOLETO { get; set; }
            public string VL_RECIBO { get; set; }
            public string VL_OUTROS { get; set; }
            public string TOTAL_TITULO { get; set; }
            public string total 
            {
                get 
                {
                    return "R$ " + string.Format("{0:#.000,00}", this.VL_TOTAL);
                }
            }
            public string USUARIO_EMISSOR { get; set; }
            public IList<Aluno> Aluno { get; set; }
        }

        #endregion

        #region Events

        //private void xrTableCell6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    XRLabel lbl = sender as XRLabel;

        //    decimal vl;
        //    if (decimal.TryParse(lbl.Text, out vl))
        //    {
        //        if (vl == 0)
        //            lbl.ForeColor = Color.Black;
        //        else if (vl < 0)
        //            lbl.ForeColor = Color.Red;
        //        else
        //            lbl.ForeColor = Color.Navy;

        //    }
        //}

        //private void xrTableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    XRTableCell cel = sender as XRTableCell;
        //    DateTime dt = DateTime.ParseExact(cel.Text, "dd/MM/yy", null);
        //    if (dt > DateTime.Today)
        //        cel.Row.ForeColor = Color.Navy;
        //}

        #endregion

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        // O método toExtenso recebe um valor do tipo decimal
        public static string toExtenso(decimal valor)
        {
            if (valor <= 0 | valor >= 1000000000000000)
                return "Valor não suportado pelo sistema.";
            else
            {
                string strValor = valor.ToString("000000000000000.00");
                string valor_por_extenso = string.Empty;

                for (int i = 0; i <= 15; i += 3)
                {
                    valor_por_extenso += escreva_parte(Convert.ToDecimal(strValor.Substring(i, 3)));
                    if (i == 0 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(0, 3)) == 1)
                            valor_por_extenso += "  trilhão" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
                            valor_por_extenso += " trilhões" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " e " : string.Empty);
                    }
                    else if (i == 3 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(3, 3)) == 1)
                            valor_por_extenso += " bilhão" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
                            valor_por_extenso += " bilhões" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " e " : string.Empty);
                    }
                    else if (i == 6 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
                            valor_por_extenso += " milhão" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1)
                            valor_por_extenso += " milhões" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " e " : string.Empty);
                    }
                    else if (i == 9 & valor_por_extenso != string.Empty)
                        if (Convert.ToInt32(strValor.Substring(9, 3)) > 0)
                            valor_por_extenso += " mil" + ((Convert.ToDecimal(strValor.Substring(12, 3)) > 0) ? " e " : string.Empty);

                    if (i == 12)
                    {
                        if (valor_por_extenso.Length > 8)
                            if (valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "bilhão" | valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "milhão")
                                valor_por_extenso += " de";
                            else
                                if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "bilhões" | valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "milhões" | valor_por_extenso.Substring(valor_por_extenso.Length - 8, 7) == "trilhões")
                                    valor_por_extenso += " de";
                                else
                                    if (valor_por_extenso.Substring(valor_por_extenso.Length - 8, 8) == "trilhões")
                                        valor_por_extenso += " de";

                        if (Convert.ToInt64(strValor.Substring(0, 15)) == 1)
                            valor_por_extenso += " real";
                        else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1)
                            valor_por_extenso += " reais";

                        if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valor_por_extenso != string.Empty)
                            valor_por_extenso += " e ";
                    }

                    if (i == 15)
                        if (Convert.ToInt32(strValor.Substring(16, 2)) == 1)
                            valor_por_extenso += " centavo";
                        else if (Convert.ToInt32(strValor.Substring(16, 2)) > 1)
                            valor_por_extenso += " centavos";
                }
                return valor_por_extenso.ToUpper();
            }
        }

        static string escreva_parte(decimal valor)
        {
            if (valor <= 0)
                return string.Empty;
            else
            {
                string montagem = string.Empty;
                if (valor > 0 & valor < 1)
                {
                    valor *= 100;
                }
                string strValor = valor.ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));

                if (a == 1) montagem += (b + c == 0) ? "cem" : "cento";
                else if (a == 2) montagem += "duzentos";
                else if (a == 3) montagem += "trezentos";
                else if (a == 4) montagem += "quatrocentos";
                else if (a == 5) montagem += "quinhentos";
                else if (a == 6) montagem += "seiscentos";
                else if (a == 7) montagem += "setecentos";
                else if (a == 8) montagem += "oitocentos";
                else if (a == 9) montagem += "novecentos";

                if (b == 1)
                {
                    if (c == 0) montagem += ((a > 0) ? " e " : string.Empty) + "dez";
                    else if (c == 1) montagem += ((a > 0) ? " e " : string.Empty) + "onze";
                    else if (c == 2) montagem += ((a > 0) ? " e " : string.Empty) + "doze";
                    else if (c == 3) montagem += ((a > 0) ? " e " : string.Empty) + "treze";
                    else if (c == 4) montagem += ((a > 0) ? " e " : string.Empty) + "quatorze";
                    else if (c == 5) montagem += ((a > 0) ? " e " : string.Empty) + "quinze";
                    else if (c == 6) montagem += ((a > 0) ? " e " : string.Empty) + "dezesseis";
                    else if (c == 7) montagem += ((a > 0) ? " e " : string.Empty) + "dezessete";
                    else if (c == 8) montagem += ((a > 0) ? " e " : string.Empty) + "dezoito";
                    else if (c == 9) montagem += ((a > 0) ? " e " : string.Empty) + "dezenove";
                }
                else if (b == 2) montagem += ((a > 0) ? " e " : string.Empty) + "vinte";
                else if (b == 3) montagem += ((a > 0) ? " e " : string.Empty) + "trinta";
                else if (b == 4) montagem += ((a > 0) ? " e " : string.Empty) + "quarenta";
                else if (b == 5) montagem += ((a > 0) ? " e " : string.Empty) + "cinquenta";
                else if (b == 6) montagem += ((a > 0) ? " e " : string.Empty) + "sessenta";
                else if (b == 7) montagem += ((a > 0) ? " e " : string.Empty) + "setenta";
                else if (b == 8) montagem += ((a > 0) ? " e " : string.Empty) + "oitenta";
                else if (b == 9) montagem += ((a > 0) ? " e " : string.Empty) + "noventa";

                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty) montagem += " e ";

                if (strValor.Substring(1, 1) != "1")
                    if (c == 1) montagem += "um";
                    else if (c == 2) montagem += "dois";
                    else if (c == 3) montagem += "três";
                    else if (c == 4) montagem += "quatro";
                    else if (c == 5) montagem += "cinco";
                    else if (c == 6) montagem += "seis";
                    else if (c == 7) montagem += "sete";
                    else if (c == 8) montagem += "oito";
                    else if (c == 9) montagem += "nove";

                return montagem;
            }
        }
    }
}

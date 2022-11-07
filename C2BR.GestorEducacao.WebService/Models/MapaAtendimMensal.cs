using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.WebService.Models
{
    public class MapaAtendimMensal
    {
        public string Nome { get; set; }
        public string qtMes { get; set; }
        public int coMes { get; set; }
        public string nmMes
        {
            get
            {
                if (this.coMes == 0)
                    return "Total";
                else
                {
                    string mes = new DateTime(1900, this.coMes, 1).ToString("MMMM", new CultureInfo("pt-BR"));
                    return char.ToUpper(mes[0]) + mes.Substring(1);
                }
            }
        }
        public double? valorSessao { get; set; }
        public int coCol { get; set; }

        /// <summary>
        /// Quantidade de agendamentos totais
        /// </summary>
        public decimal AGE_R { get; set; }
        public string AGE
        {
            get
            {
                return this.AGE_R.ToString("00");
            }
        }

        /// <summary>
        /// Quantidade de cancelamentos
        /// </summary>
        public decimal CAN_R { get; set; }
        public string CAN
        {
            get
            {
                return this.CAN_R.ToString("00");
            }
        }

        /// <summary>
        /// Quantidade de atendimentos realizados
        /// </summary>
        public decimal QAR_R { get; set; }
        public string QAR
        {
            get
            {
                return this.QAR_R.ToString("00");
            }
        }

        /// <summary>
        /// Quantidade de agendamentos com falta
        /// </summary>
        public decimal FAL_R { get; set; }
        public string FAL
        {
            get
            {
                return this.FAL_R.ToString("00");
            }
        }

        /// <summary>
        /// Quantidade de agendamentos com falta justificada
        /// </summary>
        public decimal FAJ_R { get; set; }
        public string FAJ
        {
            get
            {
                return this.FAJ_R.ToString("00");
            }
        }

        /// <summary>
        /// Quantidade total de agendamentos com falta
        /// </summary>
        public decimal QTF_R { get; set; }
        public string QTF
        {
            get
            {
                return this.QTF_R.ToString("00");
            }
        }

        /// <summary>
        /// Quantidade total de presenças
        /// </summary>
        public decimal PRE_R { get; set; }
        public string PRE
        {
            get
            {
                return this.PRE_R.ToString("00");
            }
        }

        /// <summary>
        /// Quantidade total de agendamentos em aberto
        /// </summary>
        public decimal AGA_R { get; set; }
        public string AGA
        {
            get
            {
                return this.AGA_R.ToString("00");
            }
        }

        /// <summary>
        /// Quantidade total de faturamentos (PRE + FAL)
        /// </summary>
        public decimal FAT_R { get; set; }
        public string FAT
        {
            get
            {
                return this.FAT_R.ToString("00");
            }
        }

        /// <summary>
        /// Quantidade de agendamentos cancelados
        /// </summary>
        public decimal QCA_R { get; set; }
        public string QCA
        {
            get
            {
                return this.QCA_R.ToString("00");
            }
        }

        /// <summary>
        /// Quantidade de sessões faturadas
        /// </summary>
        public string QSF
        {
            get
            {
                return ((int.Parse(this.FAL) + (!string.IsNullOrEmpty(this.QAR) ? int.Parse(this.QAR) : 0)).ToString("00"));
            }
        }

        /// <summary>
        /// Atributo para receber o valor total de forma manual caso necessário
        /// </summary>
        public double? ValorTotalManual { get; set; }

        /// <summary>
        /// Valor calculado de acordo com a quantidade
        /// </summary>
        public string ValorTotal
        {
            get
            {
                if (ValorTotalManual.HasValue)
                    return this.ValorTotalManual.Value.ToString("N2");
                else
                {
                    var re = TB03_COLABOR.RetornaPeloCoCol(this.coCol);

                    return ((double.Parse(this.FAT) * (re.VL_SALAR_COL.HasValue ? re.VL_SALAR_COL.Value : 0)).ToString("N2"));
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2BR.GestorEducacao.WebService.Models
{
    public class QuantitativosAtendimento
    {
        /// <summary>
        /// Quantidade de agendamentos totais
        /// </summary>
        public int AGE { get; set; }
        public string AGE_V
        {
            get
            {
                return this.AGE.ToString("00");
            }
        }

        /// <summary>
        /// Quantidade de cancelamentos
        /// </summary>
        public int CAN { get; set; }
        public string CAN_V
        {
            get
            {
                return this.CAN.ToString("00");
            }
        }

        /// <summary>
        /// Quantidade de atendimentos realizados
        /// </summary>
        public int QAR { get; set; }
        public string QAR_V
        {
            get
            {
                return this.QAR.ToString("00");
            }
        }

        /// <summary>
        /// Quantidade de agendamentos com falta
        /// </summary>
        public int FAL { get; set; }
        public string FAL_V
        {
            get
            {
                return this.FAL.ToString("00");
            }
        }

        /// <summary>
        /// Quantidade de agendamentos com falta justificada
        /// </summary>
        public int FAJ { get; set; }
        public string FAJ_V
        {
            get
            {
                return this.FAJ.ToString("00");
            }
        }

        /// <summary>
        /// Quantidade total de agendamentos com falta
        /// </summary>
        public int QTF { get; set; }
        public string QTF_V
        {
            get
            {
                return this.QTF.ToString("00");
            }
        }

        /// <summary>
        /// Quantidade total de presenças
        /// </summary>
        public int PRE { get; set; }
        public string PRE_V
        {
            get
            {
                return this.PRE.ToString("00");
            }
        }

        /// <summary>
        /// Quantidade total de agendamentos em aberto
        /// </summary>
        public int AGA { get; set; }
        public string AGA_V
        {
            get
            {
                return this.AGA.ToString("00");
            }
        }

        /// <summary>
        /// Quantidade total de faturamentos (PRE + FAL)
        /// </summary>
        public int FAT { get; set; }
        public string FAT_V
        {
            get
            {
                return this.FAT.ToString("00");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TBS478_ATEND_GESTANTE_BO
    {
        public int ID_ATEND_GESTANTE { get; set; }
        public int CO_ALUNO { get; set; }
        public DateTime DUM { get; set; }
        public string OBS_DUM { get; set; }
        public DateTime DPP { get; set; }
        public string EDMA { get; set; }
        public string AUTURA_RPN { get; set; }
        public string BCF { get; set; }
        public string MF { get; set; }
        public string OBS_MF { get; set; }
        public string PC { get; set; }
        public string PESO { get; set; }
        public string PP { get; set; }
        public string IMC { get; set; }
        public string OBS_ANTRO { get; set; }
        public string TIPO_REG { get; set; }
        public string DT_REGISTRO { get; set; }
        public string IDADE_GESTANTE { get; set; }
        public string COD_GESTANTE { get; set; }
        public string OBS_COMPLEMENTO { get; set; }
        public string AUTURA_RA { get; set; }        
    }
}
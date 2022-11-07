using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TBS478_ATEND_GESTANTE
    {
        public bool InsertRegistroGestante(TBS478_ATEND_GESTANTE_BO BO)
        {
            try
            {
                Auxiliar.SQLDirectAcess direct = new Auxiliar.SQLDirectAcess();
                string SQL = "INSERT INTO [dbo].[TBS478_ATEND_GESTANTE] " +
                             "([CO_ALUNO]                               " +
                             ",[DUM]                                    " +
                             ",[OBS_DUM]                                " +
                             ",[DPP]                                    " +
                             ",[EDMA]                                   " +
                             ",[AUTURA_RPN]                             " +
                             ",[BCF]                                    " +
                             ",[MF]                                     " +
                             ",[OBS_MF]                                 " +
                             ",[PC]                                     " +
                             ",[PESO]                                   " +
                             ",[AUTURA_RA]                              " +
                             ",[PP]                                     " +
                             ",[IMC]                                    " +
                             ",[OBS_ANTRO]                              " +
                             ",[TIPO_REG]                               " +
                             ",[DT_REGISTRO]                            " +
                             ",[IDADE_GESTANTE]                         " +
                             ",[COD_GESTANTE]                           " +
                             ",[OBS_COMPLEMENTO])                       " +
                             " VALUES(" + BO.CO_ALUNO +
                                         ",'" + BO.DUM + "'" +
                                         ",'" + BO.OBS_DUM + "'" +
                                         ",'" + BO.DPP + "'" +
                                         ",'" + BO.EDMA + "'" +
                                         ",'" + BO.EDMA + "'" +
                                         ",'" + BO.AUTURA_RPN + "'" +
                                         ",'" + BO.BCF + "'" +
                                         ",'" + BO.MF + "'" +
                                         ",'" + BO.OBS_MF + "'" +
                                         ",'" + BO.PC + "'" +
                                         ",'" + BO.PESO + "'" +
                                         ",'" + BO.AUTURA_RA + "'" +
                                         ",'" + BO.IMC + "'" +
                                         ",'" + BO.OBS_ANTRO + "'" +
                                         ",'" + BO.TIPO_REG + "'" +
                                         ",'" + BO.DT_REGISTRO + "'" +
                                         ",'" + BO.IDADE_GESTANTE + "'" +
                                         ",'" + BO.COD_GESTANTE + "'" +
                                         ",'" + BO.OBS_COMPLEMENTO + "')";
                direct.InsereAltera(SQL);
                return true;
            }
            catch{return false;}
        }
    }
}

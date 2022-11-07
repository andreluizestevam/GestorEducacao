using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using C2BR.GestorEducacao.BusinessEntities.MSSQLPORTAL;
using System.Data.Objects;
using System.Data;

namespace C2BR.GestorEducacao.BusinessEntities.Auxiliares
{
    public static class BaseEntity
    {
        public static Boolean excluirRegistros<b, t>(ObjectQuery<t> valores)
        {
            if (typeof(b) == typeof(GestorEntities))
            {
                GestorEntities banco = new GestorEntities();
                foreach (var linha in valores)
                {
                    banco.DeleteObject(linha);
                }
                banco.SaveChanges();
            }
            else if (typeof(b) == typeof(BasePortal))
            {
                BasePortal banco = new BasePortal();
                foreach (var linha in valores)
                {
                    banco.DeleteObject(linha);
                }
                banco.SaveChanges();
            }
            return true;
        }
    }
}

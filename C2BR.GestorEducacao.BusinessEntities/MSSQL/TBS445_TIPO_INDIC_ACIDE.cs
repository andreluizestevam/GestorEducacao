//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TBS445_TIPO_INDIC_ACIDE
    {
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TBS445_TIPO_INDIC_ACIDE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static int DeletePorID(int ID_ITEM)
        {
            var tbs438 = RetornaPelaChavePrimaria(ID_ITEM);
            return GestorEntities.Delete(tbs438);
        }

        public static TBS445_TIPO_INDIC_ACIDE Delete(TBS445_TIPO_INDIC_ACIDE entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TBS445_TIPO_INDIC_ACIDE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TBS445_TIPO_INDIC_ACIDE SaveOrUpdate(TBS445_TIPO_INDIC_ACIDE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TBS445_TIPO_INDIC_ACIDE GetByEntityKey(EntityKey entityKey)
        {
            return (TBS445_TIPO_INDIC_ACIDE)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TBS445_TIPO_INDIC_ACIDE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS445_TIPO_INDIC_ACIDE.OrderBy(g => g.ID_TP_ACIDE).AsObjectQuery();
        }

        #endregion
        
        /// <summary>
        public static TBS445_TIPO_INDIC_ACIDE RetornaPelaChavePrimaria(int ID_TP_ACIDE)
        {
            return (from tbs440 in RetornaTodosRegistros()
                    where tbs440.ID_TP_ACIDE == ID_TP_ACIDE
                    select tbs440).OrderBy(g => g.ID_TP_ACIDE).FirstOrDefault();
        }     
        #endregion
    }
}

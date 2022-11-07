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
    public partial class TBS443_TIPO_INTER
    {
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TBS443_TIPO_INTER entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static int DeletePorID(int ID_ITEM)
        {
            var tbs438 = RetornaPelaChavePrimaria(ID_ITEM);
            return GestorEntities.Delete(tbs438);
        }

        public static TBS443_TIPO_INTER Delete(TBS443_TIPO_INTER entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TBS443_TIPO_INTER entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TBS443_TIPO_INTER SaveOrUpdate(TBS443_TIPO_INTER entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TBS443_TIPO_INTER GetByEntityKey(EntityKey entityKey)
        {
            return (TBS443_TIPO_INTER)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TBS443_TIPO_INTER> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS443_TIPO_INTER.OrderBy(g => g.ID_TP_INTER).AsObjectQuery();
        }

        #endregion
        
        /// <summary>
        public static TBS443_TIPO_INTER RetornaPelaChavePrimaria(int ID_TP_INTER)
        {
            return (from tbs440 in RetornaTodosRegistros()
                    where tbs440.ID_TP_INTER == ID_TP_INTER
                    select tbs440).OrderBy(g => g.ID_TP_INTER).FirstOrDefault();
        }     
        #endregion
    }
}

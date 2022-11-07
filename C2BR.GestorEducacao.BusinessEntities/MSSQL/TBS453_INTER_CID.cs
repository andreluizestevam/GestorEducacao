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
    public partial class TBS453_INTER_CID 
    {            
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TBS453_INTER_CID entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static int DeletePorID(int ID_ITEM)
        {
            var tbs438 = RetornaPelaChavePrimaria(ID_ITEM);
            return GestorEntities.Delete(tbs438);
        }

        public static TBS453_INTER_CID Delete(TBS453_INTER_CID entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TBS453_INTER_CID entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TBS453_INTER_CID SaveOrUpdate(TBS453_INTER_CID entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TBS453_INTER_CID GetByEntityKey(EntityKey entityKey)
        {
            return (TBS453_INTER_CID)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TBS453_INTER_CID> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS453_INTER_CID.OrderBy(g => g.ID_INTER_CID).AsObjectQuery();
        }

        #endregion
        
        /// <summary>
        public static TBS453_INTER_CID RetornaPelaChavePrimaria(int ID_INTER_CID)
        {
            return (from tbs453 in RetornaTodosRegistros()
                    where tbs453.ID_INTER_CID == ID_INTER_CID
                    select tbs453).OrderBy(g => g.ID_INTER_CID).FirstOrDefault();
        }     
        #endregion
    }
}

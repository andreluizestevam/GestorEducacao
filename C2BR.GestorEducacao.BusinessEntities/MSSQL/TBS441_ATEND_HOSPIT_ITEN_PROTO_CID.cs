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
    public partial class TBS441_ATEND_HOSPIT_ITEN_PROTO_CID
    {
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TBS441_ATEND_HOSPIT_ITEN_PROTO_CID entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static int DeletePorID(int ID_ITEM)
        {
            var tbs438 = RetornaPelaChavePrimaria(ID_ITEM);
            return GestorEntities.Delete(tbs438);
        }

        public static TBS441_ATEND_HOSPIT_ITEN_PROTO_CID Delete(TBS441_ATEND_HOSPIT_ITEN_PROTO_CID entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TBS441_ATEND_HOSPIT_ITEN_PROTO_CID entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TBS441_ATEND_HOSPIT_ITEN_PROTO_CID SaveOrUpdate(TBS441_ATEND_HOSPIT_ITEN_PROTO_CID entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TBS441_ATEND_HOSPIT_ITEN_PROTO_CID GetByEntityKey(EntityKey entityKey)
        {
            return (TBS441_ATEND_HOSPIT_ITEN_PROTO_CID)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TBS441_ATEND_HOSPIT_ITEN_PROTO_CID> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS441_ATEND_HOSPIT_ITEN_PROTO_CID.OrderBy(g => g.ID_ITEM_CID_ATEND).AsObjectQuery();
        }

        #endregion
        
        /// <summary>
        public static TBS441_ATEND_HOSPIT_ITEN_PROTO_CID RetornaPelaChavePrimaria(int ID_ITEM_CID_ATEND)
        {
            return (from tbs440 in RetornaTodosRegistros()
                    where tbs440.ID_ITEM_CID_ATEND == ID_ITEM_CID_ATEND
                    select tbs440).OrderBy(g => g.ID_ITEM_CID_ATEND).FirstOrDefault();
        }     
        #endregion
    }
}

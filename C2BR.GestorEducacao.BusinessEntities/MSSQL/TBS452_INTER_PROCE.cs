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
    public partial class TBS452_INTER_PROCE 
    {            
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TBS452_INTER_PROCE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static int DeletePorID(int ID_ITEM)
        {
            var tbs438 = RetornaPelaChavePrimaria(ID_ITEM);
            return GestorEntities.Delete(tbs438);
        }

        public static TBS452_INTER_PROCE Delete(TBS452_INTER_PROCE entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TBS452_INTER_PROCE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TBS452_INTER_PROCE SaveOrUpdate(TBS452_INTER_PROCE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TBS452_INTER_PROCE GetByEntityKey(EntityKey entityKey)
        {
            return (TBS452_INTER_PROCE)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TBS452_INTER_PROCE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS452_INTER_PROCE.OrderBy(g => g.ID_INTER_PROCE).AsObjectQuery();
        }

        #endregion
        
        /// <summary>
        public static TBS452_INTER_PROCE RetornaPelaChavePrimaria(int ID_INTER_PROCE)
        {
            return (from tbs452 in RetornaTodosRegistros()
                    where tbs452.ID_INTER_PROCE == ID_INTER_PROCE
                    select tbs452).OrderBy(g => g.ID_INTER_PROCE).FirstOrDefault();
        }     
        #endregion
    }
}

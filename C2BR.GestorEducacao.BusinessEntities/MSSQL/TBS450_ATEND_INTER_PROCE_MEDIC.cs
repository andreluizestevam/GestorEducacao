//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;
using System.Linq.Expressions;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TBS450_ATEND_INTER_PROCE_MEDIC
    {           
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TBS450_ATEND_INTER_PROCE_MEDIC entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static int DeletePorID(int ID_ITEM)
        {
            var tbs438 = RetornaPelaChavePrimaria(ID_ITEM);
            return GestorEntities.Delete(tbs438);
        }

        public static TBS450_ATEND_INTER_PROCE_MEDIC Delete(TBS450_ATEND_INTER_PROCE_MEDIC entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TBS450_ATEND_INTER_PROCE_MEDIC entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TBS450_ATEND_INTER_PROCE_MEDIC SaveOrUpdate(TBS450_ATEND_INTER_PROCE_MEDIC entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TBS450_ATEND_INTER_PROCE_MEDIC GetByEntityKey(EntityKey entityKey)
        {
            return (TBS450_ATEND_INTER_PROCE_MEDIC)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TBS450_ATEND_INTER_PROCE_MEDIC> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS450_ATEND_INTER_PROCE_MEDIC.OrderBy(g => g.ID_ATEND_INTER_PROCE).AsObjectQuery();
        }

        public static IQueryable<TBS450_ATEND_INTER_PROCE_MEDIC> RetornarRegistros(Expression<Func<TBS450_ATEND_INTER_PROCE_MEDIC, bool>> predicate)
        {
            return GestorEntities.CurrentContext.TBS450_ATEND_INTER_PROCE_MEDIC.Where(predicate);
        }

        #endregion
        
        /// <summary>
        public static TBS450_ATEND_INTER_PROCE_MEDIC RetornaPelaChavePrimaria(int ID_ATEND_INTER_PROCE)
        {
            return (from tbs450 in RetornaTodosRegistros()
                    where tbs450.ID_ATEND_INTER_PROCE == ID_ATEND_INTER_PROCE
                    select tbs450).OrderBy(g => g.ID_ATEND_INTER_PROCE).FirstOrDefault();
        }     
        #endregion
    }
}

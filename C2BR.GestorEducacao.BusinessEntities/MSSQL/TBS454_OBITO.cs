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
    public partial class TBS454_OBITO
    {
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TBS454_OBITO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static int DeletePorID(int ID_ITEM)
        {
            var tbs438 = RetornaPelaChavePrimaria(ID_ITEM);
            return GestorEntities.Delete(tbs438);
        }

        public static TBS454_OBITO Delete(TBS454_OBITO entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TBS454_OBITO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TBS454_OBITO SaveOrUpdate(TBS454_OBITO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TBS454_OBITO GetByEntityKey(EntityKey entityKey)
        {
            return (TBS454_OBITO)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TBS454_OBITO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS454_OBITO.OrderBy(g => g.ID_OBITO).AsObjectQuery();
        }

        public static IQueryable<TBS454_OBITO> RetornarRegistros(Expression<Func<TBS454_OBITO, bool>> predicate) {
            return GestorEntities.CurrentContext.TBS454_OBITO.Where(predicate);
        }

        public static TBS454_OBITO RetornarUmRegistro(Expression<Func<TBS454_OBITO, bool>> predicate)
        {
            return GestorEntities.CurrentContext.TBS454_OBITO.FirstOrDefault(predicate);
        }

        #endregion

        /// <summary>
        public static TBS454_OBITO RetornaPelaChavePrimaria(int ID_OBITO)
        {
            //return (from tbs454 in RetornaTodosRegistros()
            //        where tbs454.ID_OBITO == ID_OBITO
            //        select tbs454).OrderBy(g => g.ID_OBITO).FirstOrDefault();

            return GestorEntities.CurrentContext.TBS454_OBITO.FirstOrDefault(p => p.ID_OBITO == ID_OBITO);
        }
        #endregion
    }
}
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
    public partial class TBS448_ATEND_INTER_HOSPI 
    {            
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TBS448_ATEND_INTER_HOSPI entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static int DeletePorID(int ID_ITEM)
        {
            var tbs438 = RetornaPelaChavePrimaria(ID_ITEM);
            return GestorEntities.Delete(tbs438);
        }

        public static TBS448_ATEND_INTER_HOSPI Delete(TBS448_ATEND_INTER_HOSPI entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TBS448_ATEND_INTER_HOSPI entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TBS448_ATEND_INTER_HOSPI SaveOrUpdate(TBS448_ATEND_INTER_HOSPI entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TBS448_ATEND_INTER_HOSPI GetByEntityKey(EntityKey entityKey)
        {
            return (TBS448_ATEND_INTER_HOSPI)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TBS448_ATEND_INTER_HOSPI> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS448_ATEND_INTER_HOSPI.OrderBy(g => g.ID_ATEND_INTER).AsObjectQuery();
        }

        #endregion


        public static TBS448_ATEND_INTER_HOSPI RetornarUmRegistro(Expression<Func<TBS448_ATEND_INTER_HOSPI, bool>> predicate) {
            return GestorEntities.CurrentContext.TBS448_ATEND_INTER_HOSPI.FirstOrDefault(predicate);
        }

        /// <summary>
        public static TBS448_ATEND_INTER_HOSPI RetornaPelaChavePrimaria(int ID_ATEND_INTER)
        {
            //return (from tbs448 in RetornaTodosRegistros()
            //        where tbs448.ID_ATEND_INTER == ID_ATEND_INTER
            //        select tbs448).OrderBy(g => g.ID_ATEND_INTER).FirstOrDefault();

            return GestorEntities.CurrentContext.TBS448_ATEND_INTER_HOSPI.FirstOrDefault(p => p.ID_ATEND_INTER == ID_ATEND_INTER);
        }     
        #endregion
    }
}

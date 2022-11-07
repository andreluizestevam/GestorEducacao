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
    public partial class TBS457_CLASS_TERAP
    {
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TBS457_CLASS_TERAP entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static int DeletePorID(int ID_ITEM)
        {
            var tbs438 = RetornaPelaChavePrimaria(ID_ITEM);
            return GestorEntities.Delete(tbs438);
        }

        public static TBS457_CLASS_TERAP Delete(TBS457_CLASS_TERAP entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TBS457_CLASS_TERAP entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TBS457_CLASS_TERAP SaveOrUpdate(TBS457_CLASS_TERAP entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TBS457_CLASS_TERAP GetByEntityKey(EntityKey entityKey)
        {
            return (TBS457_CLASS_TERAP)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TBS457_CLASS_TERAP> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS457_CLASS_TERAP.OrderBy(g => g.ID_CLASS_TERAP).AsObjectQuery();
        }

        #endregion

        public static IQueryable<TBS457_CLASS_TERAP> RetornarRegistros(Expression<Func<TBS457_CLASS_TERAP, bool>> predicate) {
            return GestorEntities.CurrentContext.TBS457_CLASS_TERAP.Where(predicate);
        }

        /// <summary>
        public static TBS457_CLASS_TERAP RetornaPelaChavePrimaria(int ID_CLASS_TERAP)
        {
         
            return GestorEntities.CurrentContext.TBS457_CLASS_TERAP.FirstOrDefault(p => p.ID_CLASS_TERAP == ID_CLASS_TERAP);
        }
        #endregion
    }
}
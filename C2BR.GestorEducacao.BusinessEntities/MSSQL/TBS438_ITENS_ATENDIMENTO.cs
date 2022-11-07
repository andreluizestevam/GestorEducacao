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
    public partial class TBS438_ITENS_ATENDIMENTO
    {
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TBS438_ITENS_ATENDIMENTO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static int DeletePorID(int ID_ITEM)
        {
            var tbs438 = RetornaPelaChavePrimaria(ID_ITEM);
            return GestorEntities.Delete(tbs438);
        }

        public static TBS438_ITENS_ATENDIMENTO Delete(TBS438_ITENS_ATENDIMENTO entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TBS438_ITENS_ATENDIMENTO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TBS438_ITENS_ATENDIMENTO SaveOrUpdate(TBS438_ITENS_ATENDIMENTO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TBS438_ITENS_ATENDIMENTO GetByEntityKey(EntityKey entityKey)
        {
            return (TBS438_ITENS_ATENDIMENTO)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TBS438_ITENS_ATENDIMENTO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS438_ITENS_ATENDIMENTO.OrderBy(g => g.ID_ITEM).AsObjectQuery();
        }

        public static IQueryable<TBS438_ITENS_ATENDIMENTO> RetornarRegistros(Expression<Func<TBS438_ITENS_ATENDIMENTO, bool>> predicate) {

            return GestorEntities.CurrentContext.TBS438_ITENS_ATENDIMENTO.Where(predicate);
        }

        #endregion
        
        /// <summary>
        public static TBS438_ITENS_ATENDIMENTO RetornaPelaChavePrimaria(int ID_ITEM)
        {
            return (from tbs438 in RetornaTodosRegistros()
                    where tbs438.ID_ITEM == ID_ITEM
                    select tbs438).OrderBy(g => g.ID_ITEM).FirstOrDefault();
        }     
        #endregion
    }
}

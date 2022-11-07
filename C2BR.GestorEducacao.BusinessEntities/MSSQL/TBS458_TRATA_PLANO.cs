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
    public partial class TBS458_TRATA_PLANO
    {
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TBS458_TRATA_PLANO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static int DeletePorID(int ID_ITEM)
        {
            var tbs438 = RetornaPelaChavePrimaria(ID_ITEM);
            return GestorEntities.Delete(tbs438);
        }

        public static TBS458_TRATA_PLANO Delete(TBS458_TRATA_PLANO entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TBS458_TRATA_PLANO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TBS458_TRATA_PLANO SaveOrUpdate(TBS458_TRATA_PLANO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TBS458_TRATA_PLANO GetByEntityKey(EntityKey entityKey)
        {
            return (TBS458_TRATA_PLANO)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TBS458_TRATA_PLANO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS458_TRATA_PLANO.OrderBy(g => g.ID_TRATA_PLANO).AsObjectQuery();
        }

        #endregion

        /// <summary>
        public static TBS458_TRATA_PLANO RetornaPelaChavePrimaria(int ID_TRATA_PLANO)
        {
            return (from tbs457 in RetornaTodosRegistros()
                    where tbs457.ID_TRATA_PLANO == ID_TRATA_PLANO
                    select tbs457).OrderBy(g => g.ID_TRATA_PLANO).FirstOrDefault();
        }
        #endregion
    }
}
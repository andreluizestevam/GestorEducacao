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
    public partial class TBS455_AGEND_PROF_INTER
    {
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TBS455_AGEND_PROF_INTER entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static int DeletePorID(int ID_ITEM)
        {
            var tbs438 = RetornaPelaChavePrimaria(ID_ITEM);
            return GestorEntities.Delete(tbs438);
        }

        public static TBS455_AGEND_PROF_INTER Delete(TBS455_AGEND_PROF_INTER entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TBS455_AGEND_PROF_INTER entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TBS455_AGEND_PROF_INTER SaveOrUpdate(TBS455_AGEND_PROF_INTER entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TBS455_AGEND_PROF_INTER GetByEntityKey(EntityKey entityKey)
        {
            return (TBS455_AGEND_PROF_INTER)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TBS455_AGEND_PROF_INTER> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS455_AGEND_PROF_INTER.OrderBy(g => g.ID_AGEND_PROFI_INTER).AsObjectQuery();
        }

        #endregion

        /// <summary>
        public static TBS455_AGEND_PROF_INTER RetornaPelaChavePrimaria(int ID_AGEND_PROFI_INTER)
        {
            return (from tbs455 in RetornaTodosRegistros()
                    where tbs455.ID_AGEND_PROFI_INTER == ID_AGEND_PROFI_INTER
                    select tbs455).OrderBy(g => g.ID_AGEND_PROFI_INTER).FirstOrDefault();
        }
        #endregion
    }
}
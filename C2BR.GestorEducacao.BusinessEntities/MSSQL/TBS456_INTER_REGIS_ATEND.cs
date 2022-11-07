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
    public partial class TBS456_INTER_REGIS_ATEND
    {
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TBS456_INTER_REGIS_ATEND entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static int DeletePorID(int ID_ITEM)
        {
            var tbs438 = RetornaPelaChavePrimaria(ID_ITEM);
            return GestorEntities.Delete(tbs438);
        }

        public static TBS456_INTER_REGIS_ATEND Delete(TBS456_INTER_REGIS_ATEND entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TBS456_INTER_REGIS_ATEND entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TBS456_INTER_REGIS_ATEND SaveOrUpdate(TBS456_INTER_REGIS_ATEND entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TBS456_INTER_REGIS_ATEND GetByEntityKey(EntityKey entityKey)
        {
            return (TBS456_INTER_REGIS_ATEND)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TBS456_INTER_REGIS_ATEND> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS456_INTER_REGIS_ATEND.OrderBy(g => g.ID_REGIS_ATEND_INTER).AsObjectQuery();
        }

        #endregion

        /// <summary>
        public static TBS456_INTER_REGIS_ATEND RetornaPelaChavePrimaria(int ID_REGIS_ATEND_INTER)
        {
            return (from tbs456 in RetornaTodosRegistros()
                    where tbs456.ID_REGIS_ATEND_INTER == ID_REGIS_ATEND_INTER
                    select tbs456).OrderBy(g => g.ID_REGIS_ATEND_INTER).FirstOrDefault();
        }
        #endregion
    }
}
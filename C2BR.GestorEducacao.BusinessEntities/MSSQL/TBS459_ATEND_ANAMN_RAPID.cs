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
    public partial class TBS459_ATEND_ANAMN_RAPID
    {
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TBS459_ATEND_ANAMN_RAPID entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static int DeletePorID(int ID_ITEM)
        {
            var tbs438 = RetornaPelaChavePrimaria(ID_ITEM);
            return GestorEntities.Delete(tbs438);
        }

        public static TBS459_ATEND_ANAMN_RAPID Delete(TBS459_ATEND_ANAMN_RAPID entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TBS459_ATEND_ANAMN_RAPID entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TBS459_ATEND_ANAMN_RAPID SaveOrUpdate(TBS459_ATEND_ANAMN_RAPID entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TBS459_ATEND_ANAMN_RAPID GetByEntityKey(EntityKey entityKey)
        {
            return (TBS459_ATEND_ANAMN_RAPID)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TBS459_ATEND_ANAMN_RAPID> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS459_ATEND_ANAMN_RAPID.OrderBy(g => g.ID_ATEND_ANAMN_RAPID).AsObjectQuery();
        }

        #endregion

        /// <summary>
        public static TBS459_ATEND_ANAMN_RAPID RetornaPelaChavePrimaria(int ID_ATEND_ANAMN_RAPID)
        {
            return (from tbs459 in RetornaTodosRegistros()
                    where tbs459.ID_ATEND_ANAMN_RAPID == ID_ATEND_ANAMN_RAPID
                    select tbs459).OrderBy(g => g.ID_ATEND_ANAMN_RAPID).FirstOrDefault();
        }
        #endregion
    }
}
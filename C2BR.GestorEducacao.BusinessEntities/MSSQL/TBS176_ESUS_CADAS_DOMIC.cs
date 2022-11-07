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
    public partial class TBS176_ESUS_CADAS_DOMIC
    {
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TBS176_ESUS_CADAS_DOMIC entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static int DeletePorID(int ID_CADAS_DOMIC)
        {
            var tbs176 = RetornaPelaChavePrimaria(ID_CADAS_DOMIC);
            return GestorEntities.Delete(tbs176);
        }

        public static TBS176_ESUS_CADAS_DOMIC Delete(TBS176_ESUS_CADAS_DOMIC entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TBS176_ESUS_CADAS_DOMIC entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TBS176_ESUS_CADAS_DOMIC SaveOrUpdate(TBS176_ESUS_CADAS_DOMIC entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TBS176_ESUS_CADAS_DOMIC GetByEntityKey(EntityKey entityKey)
        {
            return (TBS176_ESUS_CADAS_DOMIC)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TBS176_ESUS_CADAS_DOMIC> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS176_ESUS_CADAS_DOMIC.OrderBy(g => g.ID_CADAS_DOMIC).AsObjectQuery();
        }

        #endregion
        
        /// <summary>
        public static TBS176_ESUS_CADAS_DOMIC RetornaPelaChavePrimaria(int ID_CADAS)
        {
            return (from tbs176 in RetornaTodosRegistros()
                    where tbs176.ID_CADAS_DOMIC == ID_CADAS
                    select tbs176).OrderBy(g => g.ID_CADAS_DOMIC).FirstOrDefault();
        }     
        #endregion
    }
}

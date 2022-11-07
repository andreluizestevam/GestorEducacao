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
    public partial class TBS177_ESUS_DOMIC_FAMILIA
    {
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TBS177_ESUS_DOMIC_FAMILIA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static int DeletePorID(int ID_DOMIC_FAMIL)
        {
            var tbs177 = RetornaPelaChavePrimaria(ID_DOMIC_FAMIL);
            return GestorEntities.Delete(tbs177);
        }

        public static TBS177_ESUS_DOMIC_FAMILIA Delete(TBS177_ESUS_DOMIC_FAMILIA entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TBS177_ESUS_DOMIC_FAMILIA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TBS177_ESUS_DOMIC_FAMILIA SaveOrUpdate(TBS177_ESUS_DOMIC_FAMILIA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TBS177_ESUS_DOMIC_FAMILIA GetByEntityKey(EntityKey entityKey)
        {
            return (TBS177_ESUS_DOMIC_FAMILIA)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TBS177_ESUS_DOMIC_FAMILIA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS177_ESUS_DOMIC_FAMILIA.OrderBy(g => g.ID_DOMIC_FAMIL).AsObjectQuery();
        }

        #endregion
        
        /// <summary>
        public static TBS177_ESUS_DOMIC_FAMILIA RetornaPelaChavePrimaria(int ID_DOMIC_FAMIL)
        {
            return (from tbs177 in RetornaTodosRegistros()
                    where tbs177.ID_DOMIC_FAMIL == ID_DOMIC_FAMIL
                    select tbs177).OrderBy(g => g.ID_DOMIC_FAMIL).FirstOrDefault();
        }     
        #endregion
    }
}

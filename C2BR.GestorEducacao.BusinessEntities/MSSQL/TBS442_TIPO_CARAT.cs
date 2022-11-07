﻿//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
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
    public partial class TBS442_TIPO_CARAT
    {
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TBS442_TIPO_CARAT entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static int DeletePorID(int ID_ITEM)
        {
            var tbs438 = RetornaPelaChavePrimaria(ID_ITEM);
            return GestorEntities.Delete(tbs438);
        }

        public static TBS442_TIPO_CARAT Delete(TBS442_TIPO_CARAT entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TBS442_TIPO_CARAT entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TBS442_TIPO_CARAT SaveOrUpdate(TBS442_TIPO_CARAT entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TBS442_TIPO_CARAT GetByEntityKey(EntityKey entityKey)
        {
            return (TBS442_TIPO_CARAT)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TBS442_TIPO_CARAT> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS442_TIPO_CARAT.OrderBy(g => g.ID_CARAT).AsObjectQuery();
        }

        #endregion
        
        /// <summary>
        public static TBS442_TIPO_CARAT RetornaPelaChavePrimaria(int ID_CARAT)
        {
            return (from tbs440 in RetornaTodosRegistros()
                    where tbs440.ID_CARAT == ID_CARAT
                    select tbs440).OrderBy(g => g.ID_CARAT).FirstOrDefault();
        }     
        #endregion
    }
}

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
    public partial class TBS444_TIPO_REGIM
    {
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TBS444_TIPO_REGIM entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static int DeletePorID(int ID_ITEM)
        {
            var tbs438 = RetornaPelaChavePrimaria(ID_ITEM);
            return GestorEntities.Delete(tbs438);
        }

        public static TBS444_TIPO_REGIM Delete(TBS444_TIPO_REGIM entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TBS444_TIPO_REGIM entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TBS444_TIPO_REGIM SaveOrUpdate(TBS444_TIPO_REGIM entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TBS444_TIPO_REGIM GetByEntityKey(EntityKey entityKey)
        {
            return (TBS444_TIPO_REGIM)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TBS444_TIPO_REGIM> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS444_TIPO_REGIM.OrderBy(g => g.ID_TP_REGIM).AsObjectQuery();
        }

        #endregion
        
        /// <summary>
        public static TBS444_TIPO_REGIM RetornaPelaChavePrimaria(int ID_TP_REGIM)
        {
            return (from tbs440 in RetornaTodosRegistros()
                    where tbs440.ID_TP_REGIM == ID_TP_REGIM
                    select tbs440).OrderBy(g => g.ID_TP_REGIM).FirstOrDefault();
        }     
        #endregion
    }
}

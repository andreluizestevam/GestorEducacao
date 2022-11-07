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
    public partial class TBS446_TIPO_DOENCA
    {
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TBS446_TIPO_DOENCA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static int DeletePorID(int ID_ITEM)
        {
            var tbs438 = RetornaPelaChavePrimaria(ID_ITEM);
            return GestorEntities.Delete(tbs438);
        }

        public static TBS446_TIPO_DOENCA Delete(TBS446_TIPO_DOENCA entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TBS446_TIPO_DOENCA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TBS446_TIPO_DOENCA SaveOrUpdate(TBS446_TIPO_DOENCA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TBS446_TIPO_DOENCA GetByEntityKey(EntityKey entityKey)
        {
            return (TBS446_TIPO_DOENCA)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TBS446_TIPO_DOENCA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS446_TIPO_DOENCA.OrderBy(g => g.ID_TP_DOENCA).AsObjectQuery();
        }

        #endregion
        
        /// <summary>
        public static TBS446_TIPO_DOENCA RetornaPelaChavePrimaria(int ID_TP_DOENCA)
        {
            return (from tbs440 in RetornaTodosRegistros()
                    where tbs440.ID_TP_DOENCA == ID_TP_DOENCA
                    select tbs440).OrderBy(g => g.ID_TP_DOENCA).FirstOrDefault();
        }     
        #endregion
    }
}

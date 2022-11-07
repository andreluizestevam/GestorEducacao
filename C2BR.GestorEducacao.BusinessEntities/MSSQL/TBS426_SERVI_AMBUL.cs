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
    public partial class TBS426_SERVI_AMBUL
    {
        #region Métodos

        #region Métodos Básicos

        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TBS426_SERVI_AMBUL entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static TBS426_SERVI_AMBUL Delete(TBS426_SERVI_AMBUL entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TBS426_SERVI_AMBUL entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TBS426_SERVI_AMBUL SaveOrUpdate(TBS426_SERVI_AMBUL entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TBS426_SERVI_AMBUL GetByEntityKey(EntityKey entityKey)
        {
            return (TBS426_SERVI_AMBUL)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TBS426_SERVI_AMBUL> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS426_SERVI_AMBUL.OrderBy(r => r.ID_SERVI_AMBUL).AsObjectQuery();
        }

        #endregion

        public static TBS426_SERVI_AMBUL RetornaPelaChavePrimaria(int _ID_SERVI_AMBUL)
        {
            return (from TBS426 in RetornaTodosRegistros()
                    where TBS426.ID_SERVI_AMBUL == _ID_SERVI_AMBUL
                    select TBS426).FirstOrDefault();
        }

        #endregion
    }
}
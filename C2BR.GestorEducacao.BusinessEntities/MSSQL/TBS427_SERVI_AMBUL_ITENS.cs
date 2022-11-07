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
    public partial class TBS427_SERVI_AMBUL_ITENS
    {
        #region Métodos

        #region Métodos Básicos

        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }


        public static int Delete(TBS427_SERVI_AMBUL_ITENS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static TBS427_SERVI_AMBUL_ITENS Delete(TBS427_SERVI_AMBUL_ITENS entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TBS427_SERVI_AMBUL_ITENS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TBS427_SERVI_AMBUL_ITENS SaveOrUpdate(TBS427_SERVI_AMBUL_ITENS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }
         
        public static TBS427_SERVI_AMBUL_ITENS GetByEntityKey(EntityKey entityKey)
        {
            return (TBS427_SERVI_AMBUL_ITENS)GestorEntities.GetByEntityKey(entityKey);
        }


        public static ObjectQuery<TBS427_SERVI_AMBUL_ITENS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS427_SERVI_AMBUL_ITENS.OrderBy(r => r.ID_LISTA_SERVI_AMBUL).AsObjectQuery();
        }

        #endregion


        public static TBS427_SERVI_AMBUL_ITENS RetornaPelaChavePrimaria(int _ID_LISTA_SERVI_AMBUL)
        {
            return (from TBS427 in RetornaTodosRegistros()
                    where TBS427.ID_LISTA_SERVI_AMBUL == _ID_LISTA_SERVI_AMBUL
                    select TBS427).FirstOrDefault();
        }

        #endregion
    }
}
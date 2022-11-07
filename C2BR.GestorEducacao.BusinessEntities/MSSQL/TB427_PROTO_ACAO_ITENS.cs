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
    public partial class TB427_PROTO_ACAO_ITENS
    {
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TB427_PROTO_ACAO_ITENS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static TB427_PROTO_ACAO_ITENS Delete(TB427_PROTO_ACAO_ITENS entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TB427_PROTO_ACAO_ITENS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TB427_PROTO_ACAO_ITENS SaveOrUpdate(TB427_PROTO_ACAO_ITENS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TB427_PROTO_ACAO_ITENS GetByEntityKey(EntityKey entityKey)
        {
            return (TB427_PROTO_ACAO_ITENS)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TB427_PROTO_ACAO_ITENS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB427_PROTO_ACAO_ITENS.OrderBy(g => g.ID_PROTO_ACAO_ITENS).AsObjectQuery();
        }

        #endregion
        
        /// <summary>
        public static TB427_PROTO_ACAO_ITENS RetornaPelaChavePrimaria(int ID_PROTO_ACAO_ITENS)
        {
            return (from tb427 in RetornaTodosRegistros()
                    where tb427.ID_PROTO_ACAO_ITENS == ID_PROTO_ACAO_ITENS
                    select tb427).OrderBy(g => g.ID_PROTO_ACAO_ITENS).FirstOrDefault();
        }     
        #endregion
    }
}

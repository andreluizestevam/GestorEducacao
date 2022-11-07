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
    public partial class TB426_PROTO_ACAO
    {
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TB426_PROTO_ACAO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static TB426_PROTO_ACAO Delete(TB426_PROTO_ACAO entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TB426_PROTO_ACAO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TB426_PROTO_ACAO SaveOrUpdate(TB426_PROTO_ACAO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TB426_PROTO_ACAO GetByEntityKey(EntityKey entityKey)
        {
            return (TB426_PROTO_ACAO)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TB426_PROTO_ACAO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB426_PROTO_ACAO.OrderBy(g => g.ID_PROTO_ACAO).AsObjectQuery();
        }

        #endregion
        
        /// <summary>
        public static TB426_PROTO_ACAO RetornaPelaChavePrimaria(int ID_PROTO_ACAO)
        {
            return (from tb426 in RetornaTodosRegistros()
                    where tb426.ID_PROTO_ACAO == ID_PROTO_ACAO
                    select tb426).OrderBy(g => g.ID_PROTO_ACAO).FirstOrDefault();
        }     
        #endregion
    }
}

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
    public partial class TBS436_ITEM_PROTO_CID
    {
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        /// Salva as alterações do contexto na base de dados.
        /// </summary>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        /// <summary>
        /// Exclue o registro da tabela TBS436_ITEM_PROTO_CID do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS436_ITEM_PROTO_CID entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS436_ITEM_PROTO_CID na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS436_ITEM_PROTO_CID.</returns>
        public static TBS436_ITEM_PROTO_CID Delete(TBS436_ITEM_PROTO_CID entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Faz a verificação para saber se inserção ou alteração e executa a ação necessária; você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int SaveOrUpdate(TBS436_ITEM_PROTO_CID entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS436_ITEM_PROTO_CID na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS436_ITEM_PROTO_CID.</returns>
        public static TBS436_ITEM_PROTO_CID SaveOrUpdate(TBS436_ITEM_PROTO_CID entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS436_ITEM_PROTO_CID de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS436_ITEM_PROTO_CID.</returns>
        public static TBS436_ITEM_PROTO_CID GetByEntityKey(EntityKey entityKey)
        {
            return (TBS436_ITEM_PROTO_CID)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS436_ITEM_PROTO_CID, ordenados pelo ano da grade "CO_ANO_GRADE".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB05_GRD_HORAR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS436_ITEM_PROTO_CID> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS436_ITEM_PROTO_CID.OrderBy(g => g.ID_ITEM_PROTO).AsObjectQuery();
        }

        #endregion
        
        /// <summary>
        /// Retorna um registro da entidade ID_ITEM_PROTO pelas chaves primárias "CO_EMP", "CO_MODU_CUR", "CO_CUR", "CO_TUR", "CO_MAT", "CO_DIA_SEMA_GRD", "TP_TURNO", "NR_TEMPO" e "CO_ANO_GRADE".
        /// </summary>
        /// <returns>Entidade ID_ITEM_PROTO</returns>
        public static TBS436_ITEM_PROTO_CID RetornaPelaChavePrimaria(int ID_ITEM_PROTO)
        {
            return (from tbs436 in RetornaTodosRegistros()
                    where tbs436.ID_ITEM_PROTO == ID_ITEM_PROTO
                    select tbs436).OrderBy(g => g.ID_ITEM_PROTO).FirstOrDefault();
        }     
        #endregion
    }
}

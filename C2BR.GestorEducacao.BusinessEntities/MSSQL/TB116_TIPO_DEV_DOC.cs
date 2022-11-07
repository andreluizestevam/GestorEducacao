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
    public partial class TB116_TIPO_DEV_DOC
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
        /// Exclue o registro da tabela TB116_TIPO_DEV_DOC do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB116_TIPO_DEV_DOC entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB116_TIPO_DEV_DOC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB116_TIPO_DEV_DOC.</returns>
        public static TB116_TIPO_DEV_DOC Delete(TB116_TIPO_DEV_DOC entity)
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
        public static int SaveOrUpdate(TB116_TIPO_DEV_DOC entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB116_TIPO_DEV_DOC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB116_TIPO_DEV_DOC.</returns>
        public static TB116_TIPO_DEV_DOC SaveOrUpdate(TB116_TIPO_DEV_DOC entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB116_TIPO_DEV_DOC de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB116_TIPO_DEV_DOC.</returns>
        public static TB116_TIPO_DEV_DOC GetByEntityKey(EntityKey entityKey)
        {
            return (TB116_TIPO_DEV_DOC)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB116_TIPO_DEV_DOC, ordenados pela descrição "DE_DEVOLUCAO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB116_TIPO_DEV_DOC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB116_TIPO_DEV_DOC> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB116_TIPO_DEV_DOC.OrderBy(l => l.DE_DEVOLUCAO).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB116_TIPO_DEV_DOC pela chave primária "CO_DEVOLUCAO".
        /// </summary>
        /// <param name="CO_DEVOLUCAO">Id da chave primária</param>
        /// <returns>Entidade TB116_TIPO_DEV_DOC</returns>
        public static TB116_TIPO_DEV_DOC RetornaPelaChavePrimaria(int CO_DEVOLUCAO)
        {
            return (from tb116 in RetornaTodosRegistros()
                    where tb116.CO_DEVOLUCAO == CO_DEVOLUCAO
                    select tb116).FirstOrDefault();
        }

        #endregion
    }
}

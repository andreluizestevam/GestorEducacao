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
    public partial class TBS460_AGEND_AVALI_PROFI
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
        /// Exclue o registro da tabela TBS460_AGEND_AVALI_PROFI do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS460_AGEND_AVALI_PROFI entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS460_AGEND_AVALI_PROFI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS460_AGEND_AVALI_PROFI.</returns>
        public static TBS460_AGEND_AVALI_PROFI Delete(TBS460_AGEND_AVALI_PROFI entity)
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
        public static int SaveOrUpdate(TBS460_AGEND_AVALI_PROFI entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS460_AGEND_AVALI_PROFI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS460_AGEND_AVALI_PROFI.</returns>
        public static TBS460_AGEND_AVALI_PROFI SaveOrUpdate(TBS460_AGEND_AVALI_PROFI entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS460_AGEND_AVALI_PROFI de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS460_AGEND_AVALI_PROFI.</returns>
        public static TBS460_AGEND_AVALI_PROFI GetByEntityKey(EntityKey entityKey)
        {
            return (TBS460_AGEND_AVALI_PROFI)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS460_AGEND_AVALI_PROFI, ordenados pela descrição "ID_AGEND_AVALI_PROFI".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS460_AGEND_AVALI_PROFI de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS460_AGEND_AVALI_PROFI> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS460_AGEND_AVALI_PROFI.OrderBy( t => t.ID_AGEND_AVALI_PROFI ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TBS460_AGEND_AVALI_PROFI pela chave primária "ID_AGEND_AVALI_PROFI".
        /// </summary>
        /// <param name="CO_TIP_PROD">Id da chave primária</param>
        /// <returns>Entidade TBS460_AGEND_AVALI_PROFI</returns>
        public static TBS460_AGEND_AVALI_PROFI RetornaPelaChavePrimaria(int ID_AGEND_AVALI_PROFI)
        {
            return (from tbS460 in RetornaTodosRegistros()
                    where tbS460.ID_AGEND_AVALI_PROFI == ID_AGEND_AVALI_PROFI
                    select tbS460).FirstOrDefault();
        }

        #endregion
    }
}

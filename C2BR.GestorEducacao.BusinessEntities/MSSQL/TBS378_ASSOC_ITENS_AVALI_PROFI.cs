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
    public partial class TBS378_ASSOC_ITENS_AVALI_PROFI
    {
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
        /// Exclue o registro da TBS378_ASSOC_ITENS_AVALI_PROFI do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS378_ASSOC_ITENS_AVALI_PROFI entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS378_ASSOC_ITENS_AVALI_PROFI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS378_ASSOC_ITENS_AVALI_PROFI.</returns>
        public static TBS378_ASSOC_ITENS_AVALI_PROFI Delete(TBS378_ASSOC_ITENS_AVALI_PROFI entity)
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
        public static int SaveOrUpdate(TBS378_ASSOC_ITENS_AVALI_PROFI entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS380_LOG_ALTER_STATUS_AGEND_AVALI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS378_ASSOC_ITENS_AVALI_PROFI.</returns>
        public static TBS378_ASSOC_ITENS_AVALI_PROFI SaveOrUpdate(TBS378_ASSOC_ITENS_AVALI_PROFI entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS378_ASSOC_ITENS_AVALI_PROFI de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS378_ASSOC_ITENS_AVALI_PROFI.</returns>
        public static TBS378_ASSOC_ITENS_AVALI_PROFI GetByEntityKey(EntityKey entityKey)
        {
            return (TBS378_ASSOC_ITENS_AVALI_PROFI)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS378_ASSOC_ITENS_AVALI_PROFI, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS378_ASSOC_ITENS_AVALI_PROFI de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS378_ASSOC_ITENS_AVALI_PROFI> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS378_ASSOC_ITENS_AVALI_PROFI.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS378_ASSOC_ITENS_AVALI_PROFI pela chave primária "ID_ASSOC_ITENS_AVALI_PROFI".
        /// </summary>
        /// <param name="ID_AGEND_HORAR">Id da chave primária</param>
        /// <returns>Entidade TBS378_ASSOC_ITENS_AVALI_PROFI</returns>
        public static TBS378_ASSOC_ITENS_AVALI_PROFI RetornaPelaChavePrimaria(int ID_ASSOC_ITENS_AVALI_PROFI)
        {
            return (from tbs378 in RetornaTodosRegistros()
                    where tbs378.ID_ASSOC_ITENS_AVALI_PROFI == ID_ASSOC_ITENS_AVALI_PROFI
                    select tbs378).FirstOrDefault();
        }

        #endregion
    }
}

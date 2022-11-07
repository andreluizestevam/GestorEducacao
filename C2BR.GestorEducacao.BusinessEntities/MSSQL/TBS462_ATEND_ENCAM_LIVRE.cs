using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TBS462_ATEND_ENCAM_LIVRE
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
        /// Exclue o registro da tabela TBS462_ATEND_ENCAM_LIVRE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS462_ATEND_ENCAM_LIVRE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS462_ATEND_ENCAM_LIVRE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS462_ATEND_ENCAM_LIVRE.</returns>
        public static TBS462_ATEND_ENCAM_LIVRE Delete(TBS462_ATEND_ENCAM_LIVRE entity)
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
        public static int SaveOrUpdate(TBS462_ATEND_ENCAM_LIVRE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS462_ATEND_ENCAM_LIVRE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS462_ATEND_ENCAM_LIVRE.</returns>
        public static TBS462_ATEND_ENCAM_LIVRE SaveOrUpdate(TBS462_ATEND_ENCAM_LIVRE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS462_ATEND_ENCAM_LIVRE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS462_ATEND_ENCAM_LIVRE.</returns>
        public static TBS462_ATEND_ENCAM_LIVRE GetByEntityKey(EntityKey entityKey)
        {
            return (TBS462_ATEND_ENCAM_LIVRE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS462_ATEND_ENCAM_LIVRE, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS394_RESUM_FINAN de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS462_ATEND_ENCAM_LIVRE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS462_ATEND_ENCAM_LIVRE.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS462_ATEND_ENCAM_LIVRE pela chave primária "ID_ATEND_MEDICAMENTOS".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS462_ATEND_ENCAM_LIVRE</returns>
        public static TBS462_ATEND_ENCAM_LIVRE RetornaPelaChavePrimaria(int ID_ATEND_ENCAM_LIVRE)
        {
            return (from tbs462 in RetornaTodosRegistros()
                    where tbs462.ID_ATEND_ENCAM_LIVRE == ID_ATEND_ENCAM_LIVRE
                    select tbs462).FirstOrDefault();
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TBS414_EXAME_ITENS_AVALI
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
        /// Exclue o registro da tabela TBS414_EXAME_ITENS_AVALI do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS414_EXAME_ITENS_AVALI entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS414_EXAME_ITENS_AVALI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS414_EXAME_ITENS_AVALI.</returns>
        public static TBS414_EXAME_ITENS_AVALI Delete(TBS414_EXAME_ITENS_AVALI entity)
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
        public static int SaveOrUpdate(TBS414_EXAME_ITENS_AVALI entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS414_EXAME_ITENS_AVALI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS414_EXAME_ITENS_AVALI.</returns>
        public static TBS414_EXAME_ITENS_AVALI SaveOrUpdate(TBS414_EXAME_ITENS_AVALI entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS414_EXAME_ITENS_AVALI de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS414_EXAME_ITENS_AVALI.</returns>
        public static TBS414_EXAME_ITENS_AVALI GetByEntityKey(EntityKey entityKey)
        {
            return (TBS414_EXAME_ITENS_AVALI)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS414_EXAME_ITENS_AVALI.
        /// </summary>
        public static ObjectQuery<TBS414_EXAME_ITENS_AVALI> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS414_EXAME_ITENS_AVALI.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS414_EXAME_ITENS_AVALI pela chave primária "ID_EXAME".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS414_EXAME_ITENS_AVALI</returns>
        public static TBS414_EXAME_ITENS_AVALI RetornaPelaChavePrimaria(int ID_ITENS_AVALI)
        {
            return (from tbs414 in RetornaTodosRegistros()
                    where tbs414.ID_ITENS_AVALI == ID_ITENS_AVALI
                    select tbs414).FirstOrDefault();
        }

        #endregion
    }
}


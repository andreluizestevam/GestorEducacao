using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TB419_EMAIL_USUAR_GEDUC
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
        /// Exclue o registro da tabela TB419_EMAIL_USUAR_GEDUC do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB419_EMAIL_USUAR_GEDUC entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB419_EMAIL_USUAR_GEDUC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB419_EMAIL_USUAR_GEDUC.</returns>
        public static TB419_EMAIL_USUAR_GEDUC Delete(TB419_EMAIL_USUAR_GEDUC entity)
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
        public static int SaveOrUpdate(TB419_EMAIL_USUAR_GEDUC entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB419_EMAIL_USUAR_GEDUC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB419_EMAIL_USUAR_GEDUC.</returns>
        public static TB419_EMAIL_USUAR_GEDUC SaveOrUpdate(TB419_EMAIL_USUAR_GEDUC entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB419_EMAIL_USUAR_GEDUC de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB419_EMAIL_USUAR_GEDUC.</returns>
        public static TB419_EMAIL_USUAR_GEDUC GetByEntityKey(EntityKey entityKey)
        {
            return (TB419_EMAIL_USUAR_GEDUC)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB419_EMAIL_USUAR_GEDUC.
        /// </summary>
        public static ObjectQuery<TB419_EMAIL_USUAR_GEDUC> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB419_EMAIL_USUAR_GEDUC.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TB419_EMAIL_USUAR_GEDUC pela chave primária "ID_EXAME".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TB419_EMAIL_USUAR_GEDUC</returns>
        public static TB419_EMAIL_USUAR_GEDUC RetornaPelaChavePrimaria(int _ID_USUAR_EMAIL)
        {
            return (from tbs419 in RetornaTodosRegistros()
                    where tbs419._ID_USUAR_EMAIL == _ID_USUAR_EMAIL
                    select tbs419).FirstOrDefault();
        }

        #endregion
    }
}

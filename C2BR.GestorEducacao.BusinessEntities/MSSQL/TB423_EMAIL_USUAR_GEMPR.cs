using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TB423_EMAIL_USUAR_GEMPR
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
        /// Exclue o registro da tabela TB423_EMAIL_USUAR_GEMPR do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB423_EMAIL_USUAR_GEMPR entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB423_EMAIL_USUAR_GEMPR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB423_EMAIL_USUAR_GEMPR.</returns>
        public static TB423_EMAIL_USUAR_GEMPR Delete(TB423_EMAIL_USUAR_GEMPR entity)
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
        public static int SaveOrUpdate(TB423_EMAIL_USUAR_GEMPR entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB423_EMAIL_USUAR_GEMPR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB423_EMAIL_USUAR_GEMPR.</returns>
        public static TB423_EMAIL_USUAR_GEMPR SaveOrUpdate(TB423_EMAIL_USUAR_GEMPR entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB423_EMAIL_USUAR_GEMPR de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB423_EMAIL_USUAR_GEMPR.</returns>
        public static TB423_EMAIL_USUAR_GEMPR GetByEntityKey(EntityKey entityKey)
        {
            return (TB423_EMAIL_USUAR_GEMPR)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB423_EMAIL_USUAR_GEMPR.
        /// </summary>
        public static ObjectQuery<TB423_EMAIL_USUAR_GEMPR> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB423_EMAIL_USUAR_GEMPR.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TB423_EMAIL_USUAR_GEMPR pela chave primária "ID_EXAME".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TB423_EMAIL_USUAR_GEMPR</returns>
        public static TB423_EMAIL_USUAR_GEMPR RetornaPelaChavePrimaria(int _ID_USUAR_EMAIL)
        {
            return (from tbs423 in RetornaTodosRegistros()
                    where tbs423._ID_USUAR_EMAIL == _ID_USUAR_EMAIL
                    select tbs423).FirstOrDefault();
        }

        #endregion
    }
}

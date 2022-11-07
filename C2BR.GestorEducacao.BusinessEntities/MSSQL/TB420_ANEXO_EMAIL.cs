using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TB420_ANEXO_EMAIL
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
        /// Exclue o registro da tabela TB420_ANEXO_EMAIL do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB420_ANEXO_EMAIL entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB420_ANEXO_EMAIL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB420_ANEXO_EMAIL.</returns>
        public static TB420_ANEXO_EMAIL Delete(TB420_ANEXO_EMAIL entity)
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
        public static int SaveOrUpdate(TB420_ANEXO_EMAIL entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB420_ANEXO_EMAIL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB420_ANEXO_EMAIL.</returns>
        public static TB420_ANEXO_EMAIL SaveOrUpdate(TB420_ANEXO_EMAIL entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB420_ANEXO_EMAIL de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB420_ANEXO_EMAIL.</returns>
        public static TB420_ANEXO_EMAIL GetByEntityKey(EntityKey entityKey)
        {
            return (TB420_ANEXO_EMAIL)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB420_ANEXO_EMAIL.
        /// </summary>
        public static ObjectQuery<TB420_ANEXO_EMAIL> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB420_ANEXO_EMAIL.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TB420_ANEXO_EMAIL pela chave primária "ID_EXAME".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TB420_ANEXO_EMAIL</returns>
        public static TB420_ANEXO_EMAIL RetornaPelaChavePrimaria(int _ID_ANEXO_EMAIL)
        {
            return (from tbs420 in RetornaTodosRegistros()
                    where tbs420._ID_ANEXO_EMAIL == _ID_ANEXO_EMAIL
                    select tbs420).FirstOrDefault();
        }

        #endregion
    }
}

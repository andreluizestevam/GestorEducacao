using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TB430_MENSA_ANOBASE
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
        /// Exclue o registro da tabela TB430_MENSA_ANOBASE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB430_MENSA_ANOBASE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB430_MENSA_ANOBASE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB419_EMAIL_USUAR_GEDUC.</returns>
        public static TB430_MENSA_ANOBASE Delete(TB430_MENSA_ANOBASE entity)
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
        public static int SaveOrUpdate(TB430_MENSA_ANOBASE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB419_EMAIL_USUAR_GEDUC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB430_MENSA_ANOBASE.</returns>
        public static TB430_MENSA_ANOBASE SaveOrUpdate(TB430_MENSA_ANOBASE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB430_MENSA_ANOBASE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB430_MENSA_ANOBASE.</returns>
        public static TB430_MENSA_ANOBASE GetByEntityKey(EntityKey entityKey)
        {
            return (TB430_MENSA_ANOBASE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB430_MENSA_ANOBASE.
        /// </summary>
        public static ObjectQuery<TB430_MENSA_ANOBASE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB430_MENSA_ANOBASE.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TB430_MENSA_ANOBASE pela chave primária "ID_EXAME".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TB430_MENSA_ANOBASE</returns>
        public static TB430_MENSA_ANOBASE RetornaPelaChavePrimaria(int ID_MENSA_ANOBASE)
        {
            return (from tb430 in RetornaTodosRegistros()
                    where tb430.ID_MENSA_ANO_BASE == ID_MENSA_ANOBASE
                    select tb430).FirstOrDefault();
        }

        #endregion
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TB424_EMAIL_USUAR_GSAUD
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
        /// Exclue o registro da tabela TB424_EMAIL_USUAR_GSAUD do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB424_EMAIL_USUAR_GSAUD entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB424_EMAIL_USUAR_GSAUD na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB424_EMAIL_USUAR_GSAUD.</returns>
        public static TB424_EMAIL_USUAR_GSAUD Delete(TB424_EMAIL_USUAR_GSAUD entity)
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
        public static int SaveOrUpdate(TB424_EMAIL_USUAR_GSAUD entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB424_EMAIL_USUAR_GSAUD na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB424_EMAIL_USUAR_GSAUD.</returns>
        public static TB424_EMAIL_USUAR_GSAUD SaveOrUpdate(TB424_EMAIL_USUAR_GSAUD entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB424_EMAIL_USUAR_GSAUD de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB424_EMAIL_USUAR_GSAUD.</returns>
        public static TB424_EMAIL_USUAR_GSAUD GetByEntityKey(EntityKey entityKey)
        {
            return (TB424_EMAIL_USUAR_GSAUD)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB424_EMAIL_USUAR_GSAUD.
        /// </summary>
        public static ObjectQuery<TB424_EMAIL_USUAR_GSAUD> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB424_EMAIL_USUAR_GSAUD.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TB424_EMAIL_USUAR_GSAUD pela chave primária "ID_EXAME".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TB424_EMAIL_USUAR_GSAUD</returns>
        public static TB424_EMAIL_USUAR_GSAUD RetornaPelaChavePrimaria(int _ID_USUAR_EMAIL)
        {
            return (from tb424 in RetornaTodosRegistros()
                    where tb424.ID_RESP_EMAIL == _ID_USUAR_EMAIL
                    select tb424).FirstOrDefault();
        }

        #endregion
    }
}

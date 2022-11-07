﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;


namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TSN008_ENDERECO
    {
        #region Métodos

        /// <summary>
        /// Salva as alterações do contexto na base de dados.
        /// </summary>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        /// <summary>
        /// Exclue o registro da tabela TSN008_ENDERECO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TSN008_ENDERECO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TSN008_ENDERECO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TSN008_ENDERECO.</returns>
        public static TSN008_ENDERECO Delete(TSN008_ENDERECO entity)
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
        public static int SaveOrUpdate(TSN008_ENDERECO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TSN007_FORMA_PAGTO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TSN008_ENDERECO.</returns>
        public static TSN008_ENDERECO SaveOrUpdate(TSN008_ENDERECO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TSN008_ENDERECO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TSN008_ENDERECO.</returns>
        public static TSN008_ENDERECO GetByEntityKey(EntityKey entityKey)
        {
            return (TSN008_ENDERECO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TSN008_ENDERECO, ordenados pelo Id "TSN008_ENDERECO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TSN007_FORMA_PAGTO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TSN008_ENDERECO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TSN008_ENDERECO.OrderBy(u => u.CO_ENDERECO).AsObjectQuery();
        }

        #endregion
    }
}

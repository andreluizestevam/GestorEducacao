﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TBS406_FORML_APLIC
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
        /// Exclue o registro da tabela TBS406_FORML_APLIC do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS406_FORML_APLIC entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS406_FORML_APLIC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS406_FORML_APLIC.</returns>
        public static TBS406_FORML_APLIC Delete(TBS406_FORML_APLIC entity)
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
        public static int SaveOrUpdate(TBS406_FORML_APLIC entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS406_FORML_APLIC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS406_FORML_APLIC.</returns>
        public static TBS406_FORML_APLIC SaveOrUpdate(TBS406_FORML_APLIC entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS406_FORML_APLIC de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS406_FORML_APLIC.</returns>
        public static TBS406_FORML_APLIC GetByEntityKey(EntityKey entityKey)
        {
            return (TBS406_FORML_APLIC)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS406_FORML_APLIC, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS394_RESUM_FINAN de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS406_FORML_APLIC> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS406_FORML_APLIC.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS406_FORML_APLIC pela chave primária "ID_ATEND_MEDICAMENTOS".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS406_FORML_APLIC</returns>
        public static TBS406_FORML_APLIC RetornaPelaChavePrimaria(int ID_FORML)
        {
            return (from tbs406 in RetornaTodosRegistros()
                    where tbs406.ID_FORML == ID_FORML
                    select tbs406).FirstOrDefault();
        }

        #endregion
    }
}
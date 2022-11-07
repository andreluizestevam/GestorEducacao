﻿//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
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
    public partial class TB41_FORNEC
    {
        #region Métodos

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
        /// Exclue o registro da tabela TB41_FORNEC do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB41_FORNEC entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB41_FORNEC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB41_FORNEC.</returns>
        public static TB41_FORNEC Delete(TB41_FORNEC entity)
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
        public static int SaveOrUpdate(TB41_FORNEC entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB41_FORNEC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB41_FORNEC.</returns>
        public static TB41_FORNEC SaveOrUpdate(TB41_FORNEC entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB41_FORNEC de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB41_FORNEC.</returns>
        public static TB41_FORNEC GetByEntityKey(EntityKey entityKey)
        {
            return (TB41_FORNEC)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB41_FORNEC, ordenados pelo nome razão social "DE_RAZSOC_FORN".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB41_FORNEC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB41_FORNEC> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB41_FORNEC.OrderBy( f => f.DE_RAZSOC_FORN ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB41_FORNEC pela chave primária "CO_FORN".
        /// </summary>
        /// <param name="CO_FORN">Id da chave primária</param>
        /// <returns>Entidade TB41_FORNEC</returns>
        public static TB41_FORNEC RetornaPelaChavePrimaria(int CO_FORN)
        {
            return (from tb41 in RetornaTodosRegistros()
                    where tb41.CO_FORN == CO_FORN
                select tb41).FirstOrDefault();
        }

        #endregion
    }
}

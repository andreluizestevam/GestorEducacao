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
    public partial class TB39_HISTORICO
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
        /// Exclue o registro da tabela TB39_HISTORICO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB39_HISTORICO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB39_HISTORICO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB39_HISTORICO.</returns>
        public static TB39_HISTORICO Delete(TB39_HISTORICO entity)
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
        public static int SaveOrUpdate(TB39_HISTORICO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB39_HISTORICO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB39_HISTORICO.</returns>
        public static TB39_HISTORICO SaveOrUpdate(TB39_HISTORICO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB39_HISTORICO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB39_HISTORICO.</returns>
        public static TB39_HISTORICO GetByEntityKey(EntityKey entityKey)
        {
            return (TB39_HISTORICO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB39_HISTORICO, ordenados pela descrição "DE_HISTORICO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB39_HISTORICO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB39_HISTORICO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB39_HISTORICO.OrderBy( h => h.DE_HISTORICO ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB39_HISTORICO pela chave primária "CO_HISTORICO".
        /// </summary>
        /// <param name="CO_HISTORICO">Id da chave primária</param>
        /// <returns>Entidade TB39_HISTORICO</returns>
        public static TB39_HISTORICO RetornaPelaChavePrimaria(int CO_HISTORICO)
        {
            return (from tb39 in RetornaTodosRegistros()
                    where tb39.CO_HISTORICO == CO_HISTORICO
                    select tb39).FirstOrDefault();
        }

        #endregion
    }
}
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
    public partial class TB192_CAIXA_LOGMOV
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
        /// Exclue o registro da tabela TB192_CAIXA_LOGMOV do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB192_CAIXA_LOGMOV entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB192_CAIXA_LOGMOV na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB236_LOG_ATIVIDADES.</returns>
        public static TB192_CAIXA_LOGMOV Delete(TB192_CAIXA_LOGMOV entity)
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
        public static int SaveOrUpdate(TB192_CAIXA_LOGMOV entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB236_LOG_ATIVIDADES na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB192_CAIXA_LOGMOV.</returns>
        public static TB192_CAIXA_LOGMOV SaveOrUpdate(TB192_CAIXA_LOGMOV entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB192_CAIXA_LOGMOV de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB236_LOG_ATIVIDADES.</returns>
        public static TB192_CAIXA_LOGMOV GetByEntityKey(EntityKey entityKey)
        {
            return (TB192_CAIXA_LOGMOV)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB192_CAIXA_LOGMOV, ordenados pela data de atividade do LOG "DT_LOG".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB236_LOG_ATIVIDADES de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB192_CAIXA_LOGMOV> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB192_CAIXA_LOGMOV.OrderBy(l => l.DT_LOG).AsObjectQuery();
        }
        #endregion

        #endregion
    }
}
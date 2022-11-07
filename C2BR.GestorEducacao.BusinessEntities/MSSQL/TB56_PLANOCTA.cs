//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
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
    public partial class TB56_PLANOCTA
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
        /// Exclue o registro da tabela TB56_PLANOCTA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB56_PLANOCTA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB56_PLANOCTA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB56_PLANOCTA.</returns>
        public static TB56_PLANOCTA Delete(TB56_PLANOCTA entity)
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
        public static int SaveOrUpdate(TB56_PLANOCTA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB56_PLANOCTA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB56_PLANOCTA.</returns>
        public static TB56_PLANOCTA SaveOrUpdate(TB56_PLANOCTA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB56_PLANOCTA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB56_PLANOCTA.</returns>
        public static TB56_PLANOCTA GetByEntityKey(EntityKey entityKey)
        {
            return (TB56_PLANOCTA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB56_PLANOCTA, ordenados pelo Id do subgrupo de conta "TB54_SGRP_CTA.CO_GRUP_CTA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB56_PLANOCTA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB56_PLANOCTA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB56_PLANOCTA.OrderBy( p => p.TB54_SGRP_CTA.CO_GRUP_CTA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB56_PLANOCTA pela chave primária "CO_SEQU_PC".
        /// </summary>
        /// <param name="CO_SEQU_PC">Id da chave primária</param>
        /// <returns>Entidade TB56_PLANOCTA</returns>
        public static TB56_PLANOCTA RetornaPelaChavePrimaria(int CO_SEQU_PC)
        {
            return (from tb56 in RetornaTodosRegistros()
                    where tb56.CO_SEQU_PC == CO_SEQU_PC
                    select tb56).FirstOrDefault();
        }

        #endregion
    }
}
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
    public partial class TB144_NEGOCIACAO
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
        /// Exclue o registro da tabela TB144_NEGOCIACAO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB144_NEGOCIACAO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB144_NEGOCIACAO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB144_NEGOCIACAO.</returns>
        public static TB144_NEGOCIACAO Delete(TB144_NEGOCIACAO entity)
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
        public static int SaveOrUpdate(TB144_NEGOCIACAO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB144_NEGOCIACAO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB144_NEGOCIACAO.</returns>
        public static TB144_NEGOCIACAO SaveOrUpdate(TB144_NEGOCIACAO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB144_NEGOCIACAO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB144_NEGOCIACAO.</returns>
        public static TB144_NEGOCIACAO GetByEntityKey(EntityKey entityKey)
        {
            return (TB144_NEGOCIACAO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB144_NEGOCIACAO, ordenados pelo código "CO_NEGOCIACAO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB144_NEGOCIACAO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB144_NEGOCIACAO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB144_NEGOCIACAO.OrderBy( n => n.CO_NEGOCIACAO ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB144_NEGOCIACAO pela chave primária "CO_NEGOCIACAO".
        /// </summary>
        /// <param name="CO_NEGOCIACAO">Id da chave primária</param>
        /// <returns>Entidade TB144_NEGOCIACAO</returns>
        public static TB144_NEGOCIACAO RetornaPelaChavePrimaria(int CO_NEGOCIACAO)
        {
            return (from tb144 in RetornaTodosRegistros()
                    where tb144.CO_NEGOCIACAO == CO_NEGOCIACAO
                    select tb144).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB144_NEGOCIACAO de acordo com a unidade "CO_EMP".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>Entidade TB144_NEGOCIACAO</returns>
        public static ObjectQuery<TB144_NEGOCIACAO> RetornaPelaEmpresa(int CO_EMP)
        {
            return GestorEntities.CurrentContext.TB144_NEGOCIACAO.Where( n => (n.TB25_EMPRESA == null || n.TB25_EMPRESA.CO_EMP == CO_EMP)).OrderBy( n => n.CO_NEGOCIACAO ).AsObjectQuery();
        }

        #endregion
    }
}
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
    public partial class TB166_EDIFICACAO
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
        /// Exclue o registro da tabela TB166_EDIFICACAO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB166_EDIFICACAO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB166_EDIFICACAO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB166_EDIFICACAO.</returns>
        public static TB166_EDIFICACAO Delete(TB166_EDIFICACAO entity)
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
        public static int SaveOrUpdate(TB166_EDIFICACAO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB166_EDIFICACAO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB166_EDIFICACAO.</returns>
        public static TB166_EDIFICACAO SaveOrUpdate(TB166_EDIFICACAO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB166_EDIFICACAO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB166_EDIFICACAO.</returns>
        public static TB166_EDIFICACAO GetByEntityKey(EntityKey entityKey)
        {
            return (TB166_EDIFICACAO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB166_EDIFICACAO, ordenados pela descrição "DE_EDIFI".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB166_EDIFICACAO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB166_EDIFICACAO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB166_EDIFICACAO.OrderBy( e => e.DE_EDIFI ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB166_EDIFICACAO pela chave primária "CO_EDIFI".
        /// </summary>
        /// <param name="CO_EDIFI">Id da chave primária</param>
        /// <returns>Entidade TB166_EDIFICACAO</returns>
        public static TB166_EDIFICACAO RetornaPelaChavePrimaria(int CO_EDIFI)
        {
            return (from tb166 in RetornaTodosRegistros()
                    where tb166.CO_EDIFI == CO_EDIFI
                    select tb166).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB166_EDIFICACAO de acordo com a unidade "CO_EMP".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB166_EDIFICACAO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB166_EDIFICACAO> RetornaPeloCoEmp(int CO_EMP)
        {
            return (from tb166 in RetornaTodosRegistros()
                    where tb166.TB25_EMPRESA.CO_EMP == CO_EMP
                    select tb166).AsObjectQuery();
        }

        #endregion
    }
}
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
    public partial class TB15_FUNCAO
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
        /// Exclue o registro da tabela TB15_FUNCAO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB15_FUNCAO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB15_FUNCAO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB15_FUNCAO.</returns>
        public static TB15_FUNCAO Delete(TB15_FUNCAO entity)
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
        public static int SaveOrUpdate(TB15_FUNCAO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB15_FUNCAO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB15_FUNCAO.</returns>
        public static TB15_FUNCAO SaveOrUpdate(TB15_FUNCAO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB15_FUNCAO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB15_FUNCAO.</returns>
        public static TB15_FUNCAO GetByEntityKey(EntityKey entityKey)
        {
            return (TB15_FUNCAO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB15_FUNCAO, ordenados pelo nome "NO_FUN".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB15_FUNCAO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB15_FUNCAO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB15_FUNCAO.OrderBy( f => f.NO_FUN ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB15_FUNCAO pela chave primária "CO_FUN".
        /// </summary>
        /// <param name="CO_FUN">Id da chave primária</param>
        /// <returns>Entidade TB15_FUNCAO</returns>
        public static TB15_FUNCAO RetornaPelaChavePrimaria(int CO_FUN)
        {
            return (from tb15 in RetornaTodosRegistros()
                    where tb15.CO_FUN == CO_FUN
                    select tb15).FirstOrDefault();
        }

        public static TB15_FUNCAO RetornaPeloNome(String NO_FUN)
        {
            return (from tb15 in RetornaTodosRegistros()
                    where tb15.NO_FUN == NO_FUN
                    select tb15).FirstOrDefault();
        }

        #endregion
    }
}
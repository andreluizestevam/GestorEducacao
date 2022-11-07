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
    public partial class TB66_TIPO_SOLIC
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
        /// Exclue o registro da tabela TB66_TIPO_SOLIC do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB66_TIPO_SOLIC entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB66_TIPO_SOLIC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB66_TIPO_SOLIC.</returns>
        public static TB66_TIPO_SOLIC Delete(TB66_TIPO_SOLIC entity)
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
        public static int SaveOrUpdate(TB66_TIPO_SOLIC entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB66_TIPO_SOLIC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB66_TIPO_SOLIC.</returns>
        public static TB66_TIPO_SOLIC SaveOrUpdate(TB66_TIPO_SOLIC entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB66_TIPO_SOLIC de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB66_TIPO_SOLIC.</returns>
        public static TB66_TIPO_SOLIC GetByEntityKey(EntityKey entityKey)
        {
            return (TB66_TIPO_SOLIC)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB66_TIPO_SOLIC, ordenados pelo nome "NO_TIPO_SOLI".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB66_TIPO_SOLIC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB66_TIPO_SOLIC> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB66_TIPO_SOLIC.OrderBy( t => t.NO_TIPO_SOLI ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB66_TIPO_SOLIC pela chave primária "CO_TIPO_SOLI".
        /// </summary>
        /// <param name="CO_TIPO_SOLI">Id da chave primária</param>
        /// <returns>Entidade TB66_TIPO_SOLIC</returns>
        public static TB66_TIPO_SOLIC RetornaPelaChavePrimaria(int CO_TIPO_SOLI)
        {
            return (from tb66 in RetornaTodosRegistros()
                    where tb66.CO_TIPO_SOLI == CO_TIPO_SOLI
                    select tb66).FirstOrDefault();
        }

        #endregion
    }
}

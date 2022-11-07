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
    public partial class TB18_GRAUINS
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
        /// Exclue o registro da tabela TB18_GRAUINS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB18_GRAUINS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB18_GRAUINS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB18_GRAUINS.</returns>
        public static TB18_GRAUINS Delete(TB18_GRAUINS entity)
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
        public static int SaveOrUpdate(TB18_GRAUINS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB18_GRAUINS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB18_GRAUINS.</returns>
        public static TB18_GRAUINS SaveOrUpdate(TB18_GRAUINS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB18_GRAUINS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB18_GRAUINS.</returns>
        public static TB18_GRAUINS GetByEntityKey(EntityKey entityKey)
        {
            return (TB18_GRAUINS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB18_GRAUINS, ordenados pelo nome "NO_INST".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB18_GRAUINS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB18_GRAUINS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB18_GRAUINS.OrderBy( g => g.NO_INST ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB18_GRAUINS pela chave primária "CO_INST".
        /// </summary>
        /// <param name="CO_INST">Id da chave primária</param>
        /// <returns>Entidade TB18_GRAUINS</returns>
        public static TB18_GRAUINS RetornaPelaChavePrimaria(int CO_INST)
        {
            return (from tb18 in RetornaTodosRegistros()
                    where tb18.CO_INST == CO_INST
                    select tb18).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TB18_GRAUINS pela sigla "CO_SIGLA_INST".
        /// </summary>
        /// <param name="CO_SIGLA_INST">Sigla do grau de instrução</param>
        /// <returns>Entidade TB18_GRAUINS</returns>
        public static TB18_GRAUINS RetornaPelaSigla(string CO_SIGLA_INST)
        {
            return (from tb18 in RetornaTodosRegistros()
                    where tb18.CO_SIGLA_INST.ToUpper() == CO_SIGLA_INST.ToUpper()
                    select tb18).FirstOrDefault();
        }

        #endregion
    }
}

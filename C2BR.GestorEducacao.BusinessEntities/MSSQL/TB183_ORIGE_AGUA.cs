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
    public partial class TB183_ORIGE_AGUA
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
        /// Exclue o registro da tabela TB183_ORIGE_AGUA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB183_ORIGE_AGUA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB183_ORIGE_AGUA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB183_ORIGE_AGUA.</returns>
        public static TB183_ORIGE_AGUA Delete(TB183_ORIGE_AGUA entity)
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
        public static int SaveOrUpdate(TB183_ORIGE_AGUA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB183_ORIGE_AGUA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB183_ORIGE_AGUA.</returns>
        public static TB183_ORIGE_AGUA SaveOrUpdate(TB183_ORIGE_AGUA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB183_ORIGE_AGUA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB183_ORIGE_AGUA.</returns>
        public static TB183_ORIGE_AGUA GetByEntityKey(EntityKey entityKey )
        {
            return (TB183_ORIGE_AGUA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB183_ORIGE_AGUA, ordenados pela descrição "DE_ORIGE_AGUA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB183_ORIGE_AGUA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB183_ORIGE_AGUA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB183_ORIGE_AGUA.OrderBy( o => o.DE_ORIGE_AGUA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB183_ORIGE_AGUA pela chave primária "CO_SIGLA_ORIGE_AGUA".
        /// </summary>
        /// <param name="CO_SIGLA_ORIGE_AGUA">Id da chave primária</param>
        /// <returns>Entidade TB183_ORIGE_AGUA</returns>
        public static TB183_ORIGE_AGUA RetornaPelaChavePrimaria(string CO_SIGLA_ORIGE_AGUA)
        {
            return (from tb183 in RetornaTodosRegistros()
                    where tb183.CO_SIGLA_ORIGE_AGUA == CO_SIGLA_ORIGE_AGUA
                    select tb183).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TB183_ORIGE_AGUA pelo código da origem de água "CO_ORIGE_AGUA".
        /// </summary>
        /// <param name="CO_ORIGE_AGUA">Código da origem de água</param>
        /// <returns>Entidade TB183_ORIGE_AGUA</returns>
        public static TB183_ORIGE_AGUA RetornaPeloCoOrigeAgua(int CO_ORIGE_AGUA)
        {
            return (from tb183 in RetornaTodosRegistros()
                    where tb183.CO_ORIGE_AGUA == CO_ORIGE_AGUA
                    select tb183).FirstOrDefault();
        }

        #endregion
    }
}
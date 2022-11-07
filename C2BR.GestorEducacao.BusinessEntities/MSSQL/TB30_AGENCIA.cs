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
    public partial class TB30_AGENCIA
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
        /// Exclue o registro da tabela TB30_AGENCIA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB30_AGENCIA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB30_AGENCIA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB30_AGENCIA.</returns>
        public static TB30_AGENCIA Delete(TB30_AGENCIA entity)
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
        public static int SaveOrUpdate(TB30_AGENCIA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB30_AGENCIA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB30_AGENCIA.</returns>
        public static TB30_AGENCIA SaveOrUpdate(TB30_AGENCIA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB30_AGENCIA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB30_AGENCIA.</returns>
        public static TB30_AGENCIA GetByEntityKey(EntityKey entityKey)
        {
            return (TB30_AGENCIA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB30_AGENCIA, ordenados pelo Id do Banco "IDEBANCO" e pelo Id da agência "CO_AGENCIA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB30_AGENCIA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB30_AGENCIA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB30_AGENCIA.OrderBy( a => a.IDEBANCO ).ThenBy( a => a.CO_AGENCIA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB30_AGENCIA pelas chaves primárias "IDEBANCO" e "CO_AGENCIA".
        /// </summary>
        /// <param name="IDEBANCO">Id do banco</param>
        /// <param name="CO_AGENCIA">Código da agência</param>
        /// <returns>Entidade TB30_AGENCIA</returns>
        public static TB30_AGENCIA RetornaPelaChavePrimaria(string IDEBANCO, int CO_AGENCIA)
        {
            return (from tb30 in RetornaTodosRegistros()
                    where tb30.IDEBANCO == IDEBANCO && tb30.CO_AGENCIA == CO_AGENCIA
                    select tb30).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB30_AGENCIA de acordo com o banco "IDEBANCO".
        /// </summary>
        /// <param name="IDEBANCO">Id do banco</param>
        /// <returns>Entidade TB30_AGENCIA</returns>
        public static ObjectQuery<TB30_AGENCIA> RetornaPeloBanco(string IDEBANCO)
        {
            return (from tb30 in RetornaTodosRegistros()
                    where tb30.IDEBANCO == IDEBANCO
                    select tb30).AsObjectQuery();
        }

        #endregion
    }
}
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
    public partial class TB224_CONTA_CORRENTE
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
        /// Exclue o registro da tabela TB224_CONTA_CORRENTE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB224_CONTA_CORRENTE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB224_CONTA_CORRENTE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB224_CONTA_CORRENTE.</returns>
        public static TB224_CONTA_CORRENTE Delete(TB224_CONTA_CORRENTE entity)
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
        public static int SaveOrUpdate(TB224_CONTA_CORRENTE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB224_CONTA_CORRENTE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB224_CONTA_CORRENTE.</returns>
        public static TB224_CONTA_CORRENTE SaveOrUpdate(TB224_CONTA_CORRENTE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB224_CONTA_CORRENTE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB224_CONTA_CORRENTE.</returns>
        public static TB224_CONTA_CORRENTE GetByEntityKey(EntityKey entityKey)
        {
            return (TB224_CONTA_CORRENTE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB224_CONTA_CORRENTE, ordenados pela data de abertura da conta "DT_ABERT_CTA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB224_CONTA_CORRENTE de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB224_CONTA_CORRENTE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB224_CONTA_CORRENTE.OrderBy( c => c.DT_ABERT_CTA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB224_CONTA_CORRENTE pelas chaves primárias "IDEBANCO", "CO_AGENCIA" e "CO_CONTA".
        /// </summary>
        /// <param name="IDEBANCO">Id do banco</param>
        /// <param name="CO_AGENCIA">Código da agência</param>
        /// <param name="CO_CONTA">Código da conta</param>
        /// <returns>Entidade TB224_CONTA_CORRENTE</returns>
        public static TB224_CONTA_CORRENTE RetornaPelaChavePrimaria(string IDEBANCO, int CO_AGENCIA, string CO_CONTA)
        {
            return (from tb224 in RetornaTodosRegistros()
                    where tb224.IDEBANCO == IDEBANCO && tb224.CO_AGENCIA == CO_AGENCIA && tb224.CO_CONTA == CO_CONTA
                    select tb224).FirstOrDefault();
        }

        #endregion
    }
}
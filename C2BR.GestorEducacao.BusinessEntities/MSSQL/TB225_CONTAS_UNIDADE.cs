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
    public partial class TB225_CONTAS_UNIDADE
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
        /// Exclue o registro da tabela TB225_CONTAS_UNIDADE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB225_CONTAS_UNIDADE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        // <summary>
        /// Exclue o registro da tabela TB225_CONTAS_UNIDADE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB225_CONTAS_UNIDADE.</returns>
        public static TB225_CONTAS_UNIDADE Delete(TB225_CONTAS_UNIDADE entity)
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
        public static int SaveOrUpdate(TB225_CONTAS_UNIDADE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB225_CONTAS_UNIDADE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB225_CONTAS_UNIDADE.</returns>
        public static TB225_CONTAS_UNIDADE SaveOrUpdate(TB225_CONTAS_UNIDADE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB225_CONTAS_UNIDADE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB225_CONTAS_UNIDADE.</returns>
        public static TB225_CONTAS_UNIDADE GetByEntityKey(EntityKey entityKey)
        {
            return (TB225_CONTAS_UNIDADE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB225_CONTAS_UNIDADE, ordenados pelo Id da unidade "CO_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB225_CONTAS_UNIDADE de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB225_CONTAS_UNIDADE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB225_CONTAS_UNIDADE.OrderBy( c => c.CO_EMP ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB225_CONTAS_UNIDADE pelas chaves primárias "CO_EMP", "IDEBANCO", "CO_AGENCIA" e "CO_CONTA".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="IDEBANCO">Id do banco</param>
        /// <param name="CO_AGENCIA">Código da agência</param>
        /// <param name="CO_CONTA">Código da conta</param>
        /// <returns>Entidade TB225_CONTAS_UNIDADE</returns>
        public static TB225_CONTAS_UNIDADE RetornaPelaChavePrimaria(int CO_EMP, string IDEBANCO, int CO_AGENCIA, string CO_CONTA)
        {
            return (from tb225 in RetornaTodosRegistros()
                    where tb225.CO_EMP == CO_EMP && tb225.IDEBANCO == IDEBANCO && tb225.CO_AGENCIA == CO_AGENCIA && tb225.CO_CONTA == CO_CONTA
                    select tb225).FirstOrDefault();
        }

        #endregion
    }
}
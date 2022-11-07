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
    public partial class TB296_CAIXA_MOVIMENTO
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
        /// Exclue o registro da tabela TB296_CAIXA_MOVIMENTO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB296_CAIXA_MOVIMENTO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB296_CAIXA_MOVIMENTO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB296_CAIXA_MOVIMENTO.</returns>
        public static TB296_CAIXA_MOVIMENTO Delete(TB296_CAIXA_MOVIMENTO entity)
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
        public static int SaveOrUpdate(TB296_CAIXA_MOVIMENTO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB296_CAIXA_MOVIMENTO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB296_CAIXA_MOVIMENTO.</returns>
        public static TB296_CAIXA_MOVIMENTO SaveOrUpdate(TB296_CAIXA_MOVIMENTO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB296_CAIXA_MOVIMENTO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB296_CAIXA_MOVIMENTO.</returns>
        public static TB296_CAIXA_MOVIMENTO GetByEntityKey(EntityKey entityKey)
        {
            return (TB296_CAIXA_MOVIMENTO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB296_CAIXA_MOVIMENTO, ordenados pelo código "CO_SEQMOV_CAIXA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB296_CAIXA_MOVIMENTO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB296_CAIXA_MOVIMENTO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB296_CAIXA_MOVIMENTO.OrderBy( c => c.CO_SEQMOV_CAIXA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB296_CAIXA_MOVIMENTO pela chave primária "CO_EMP", "CO_CAIXA", "DT_MOVIMENTO", "CO_COL" e "CO_SEQMOV_CAIXA".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_CAIXA">Código do Caixa</param>
        /// <param name="DT_MOVIMENTO">Data de movimentação</param>
        /// <param name="CO_COL">Id do funcionário</param>
        /// <param name="CO_SEQMOV_CAIXA">Id auto-incremento</param>
        /// <returns>Entidade TB296_CAIXA_MOVIMENTO</returns>
        public static TB296_CAIXA_MOVIMENTO RetornaPelaChavePrimaria(int CO_EMP, int CO_CAIXA, DateTime DT_MOVIMENTO, int CO_COL, int CO_SEQMOV_CAIXA)
        {
            return (from tb296 in RetornaTodosRegistros()
                    where tb296.CO_EMP == CO_EMP && tb296.CO_CAIXA == CO_CAIXA && tb296.DT_MOVIMENTO == DT_MOVIMENTO
                    && tb296.CO_COLABOR_CAIXA == CO_COL && tb296.CO_SEQMOV_CAIXA == CO_SEQMOV_CAIXA
                    select tb296).FirstOrDefault();
        }

        /// <summary>
        /// Método que retorna a Entidade TB296_CAIXA_MOVIMENTO de acordo com o sequencial do movimento
        /// </summary>
        /// <param name="CO_SEQMOV_CAIXA">Id sequencial do movimento</param>
        /// <returns>Entidade TB296_CAIXA_MOVIMENTO</returns>
        public static TB296_CAIXA_MOVIMENTO RetornaPeloSequencial(int CO_SEQMOV_CAIXA)
        {
            return (from tb296 in RetornaTodosRegistros()
                    where tb296.CO_SEQMOV_CAIXA == CO_SEQMOV_CAIXA
                    select tb296).FirstOrDefault();
        }

        #endregion
    }
}
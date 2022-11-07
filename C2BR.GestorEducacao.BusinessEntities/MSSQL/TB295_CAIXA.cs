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
    public partial class TB295_CAIXA
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
        /// Exclue o registro da tabela TB295_CAIXA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB295_CAIXA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB295_CAIXA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB295_CAIXA.</returns>
        public static TB295_CAIXA Delete(TB295_CAIXA entity)
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
        public static int SaveOrUpdate(TB295_CAIXA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB295_CAIXA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB295_CAIXA.</returns>
        public static TB295_CAIXA SaveOrUpdate(TB295_CAIXA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB295_CAIXA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB295_CAIXA.</returns>
        public static TB295_CAIXA GetByEntityKey(EntityKey entityKey)
        {
            return (TB295_CAIXA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB295_CAIXA, ordenados pelo código "CO_CAIXA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB295_CAIXA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB295_CAIXA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB295_CAIXA.OrderBy( c => c.CO_CAIXA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB295_CAIXA pelas chaves primárias "CO_EMP", "CO_CAIXA", "CO_COL" e "DT_MOVIMENTO".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_CAIXA">Código do caixa</param>
        /// <param name="CO_COL">Id do funcionário</param>
        /// <param name="DT_MOVIMENTO">Data de movimentação</param>
        /// <returns>Entidade TB295_CAIXA</returns>
        public static TB295_CAIXA RetornaPelaChavePrimaria(int CO_EMP, int CO_CAIXA, int CO_COL, DateTime DT_MOVIMENTO)
        {
            return (from tb295 in RetornaTodosRegistros()
                    where tb295.CO_EMP == CO_EMP && tb295.CO_COLABOR_CAIXA == CO_COL && tb295.CO_CAIXA == CO_CAIXA && tb295.DT_MOVIMENTO == DT_MOVIMENTO
                    select tb295).FirstOrDefault();
        }

        #endregion
    }
}
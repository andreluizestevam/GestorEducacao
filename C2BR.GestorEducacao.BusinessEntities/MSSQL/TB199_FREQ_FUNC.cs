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
    public partial class TB199_FREQ_FUNC
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
        /// Exclue o registro da tabela TB199_FREQ_FUNC do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB199_FREQ_FUNC entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB199_FREQ_FUNC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB199_FREQ_FUNC.</returns>
        public static TB199_FREQ_FUNC Delete(TB199_FREQ_FUNC entity)
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
        public static int SaveOrUpdate(TB199_FREQ_FUNC entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB199_FREQ_FUNC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB199_FREQ_FUNC.</returns>
        public static TB199_FREQ_FUNC SaveOrUpdate(TB199_FREQ_FUNC entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB199_FREQ_FUNC de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB199_FREQ_FUNC.</returns>
        public static TB199_FREQ_FUNC GetByEntityKey(EntityKey entityKey)
        {
            return (TB199_FREQ_FUNC)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB199_FREQ_FUNC, ordenados pelo Id do funcionário "CO_COL".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB199_FREQ_FUNC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB199_FREQ_FUNC> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB199_FREQ_FUNC.OrderBy( f => f.CO_COL ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB199_FREQ_FUNC pelas chaves primárias "CO_EMP", "CO_COL", "DT_FREQ", "HR_FREQ" e "CO_SEQ_FREQ".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_COL">Id do funcionário</param>
        /// <param name="DT_FREQ">Data da frequência</param>
        /// <param name="HR_FREQ">Hora da frequência</param>
        /// <param name="CO_SEQ_FREQ">Código sequencial da frequência</param>
        /// <returns>Entidade TB199_FREQ_FUNC</returns>
        public static TB199_FREQ_FUNC RetornaPelaChavePrimaria(int CO_EMP, int CO_COL, DateTime DT_FREQ, int HR_FREQ, int CO_SEQ_FREQ)
        {
            return (from tb199 in RetornaTodosRegistros()
                    where tb199.CO_EMP == CO_EMP && tb199.CO_COL == CO_COL && tb199.DT_FREQ == DT_FREQ && tb199.HR_FREQ == HR_FREQ
                    && tb199.CO_SEQ_FREQ == CO_SEQ_FREQ
                    select tb199).FirstOrDefault();
        }

        #endregion
    }
}

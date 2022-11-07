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
    public partial class TB101_LOCALCOBRANCA
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
        /// Exclue o registro da tabela TB101_LOCALCOBRANCA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB101_LOCALCOBRANCA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB101_LOCALCOBRANCA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB101_LOCALCOBRANCA.</returns>
        public static TB101_LOCALCOBRANCA Delete(TB101_LOCALCOBRANCA entity)
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
        public static int SaveOrUpdate(TB101_LOCALCOBRANCA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB101_LOCALCOBRANCA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB101_LOCALCOBRANCA.</returns>
        public static TB101_LOCALCOBRANCA SaveOrUpdate(TB101_LOCALCOBRANCA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB101_LOCALCOBRANCA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB101_LOCALCOBRANCA.</returns>
        public static TB101_LOCALCOBRANCA GetByEntityKey(EntityKey entityKey)
        {
            return (TB101_LOCALCOBRANCA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB101_LOCALCOBRANCA, ordenados pelo nome "NO_FAN_COB".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB101_LOCALCOBRANCA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB101_LOCALCOBRANCA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB101_LOCALCOBRANCA.OrderBy( l => l.NO_FAN_COB ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB101_LOCALCOBRANCA pela chaves primária "CO_LOC_COB".
        /// </summary>
        /// <param name="CO_LOC_COB">Id da chave primária</param>
        /// <returns></returns>
        public static TB101_LOCALCOBRANCA RetornaPelaChavePrimaria(int CO_LOC_COB)
        {
            return (from tb101 in RetornaTodosRegistros().Include(typeof(TB905_BAIRRO).Name)
                    where tb101.CO_LOC_COB == CO_LOC_COB
                    select tb101).FirstOrDefault();
        }

        #endregion
    }
}

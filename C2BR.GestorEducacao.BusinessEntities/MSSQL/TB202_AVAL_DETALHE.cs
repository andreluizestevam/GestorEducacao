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
    public partial class TB202_AVAL_DETALHE
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
        /// Exclue o registro da tabela TB202_AVAL_DETALHE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB202_AVAL_DETALHE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB202_AVAL_DETALHE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB202_AVAL_DETALHE.</returns>
        public static TB202_AVAL_DETALHE Delete(TB202_AVAL_DETALHE entity)
        {
            Delete(entity, true);
            return entity;
        }

        /// <summary>
        /// Faz a verificação para saber se inserção ou alteração e executa a ação necessária; você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int SaveOrUpdate(TB202_AVAL_DETALHE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB202_AVAL_DETALHE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB202_AVAL_DETALHE.</returns>
        public static TB202_AVAL_DETALHE SaveOrUpdate(TB202_AVAL_DETALHE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB202_AVAL_DETALHE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB202_AVAL_DETALHE.</returns>
        public static TB202_AVAL_DETALHE GetByEntityKey(EntityKey entityKey)
        {
            return (TB202_AVAL_DETALHE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB202_AVAL_DETALHE, ordenados pelo Id "NU_AVAL_MASTER".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB202_AVAL_DETALHE de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB202_AVAL_DETALHE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB202_AVAL_DETALHE.OrderBy( a => a.NU_AVAL_MASTER ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB202_AVAL_DETALHE pelas chaves primárias "NU_AVAL_MASTER", "CO_TIPO_AVAL", "CO_TITU_AVAL" e "NU_QUEST_AVAL".
        /// </summary>
        /// <param name="NU_AVAL_MASTER">Número da avaliação</param>
        /// <param name="CO_TIPO_AVAL">Id do tipo de avaliação</param>
        /// <param name="CO_TITU_AVAL">Id do título de avaliação</param>
        /// <param name="NU_QUEST_AVAL">Número da questão da avaliação</param>
        /// <returns>Entidade TB202_AVAL_DETALHE</returns>
        public static TB202_AVAL_DETALHE RetornaPelaChavePrimaria(int NU_AVAL_MASTER, int CO_TIPO_AVAL, int CO_TITU_AVAL, int NU_QUEST_AVAL)
        {
            return (from tb202 in RetornaTodosRegistros()
                    where tb202.NU_AVAL_MASTER == NU_AVAL_MASTER && tb202.CO_TIPO_AVAL == CO_TIPO_AVAL && tb202.CO_TITU_AVAL == CO_TITU_AVAL
                    && tb202.NU_QUEST_AVAL == NU_QUEST_AVAL
                    select tb202).FirstOrDefault();
        }

        #endregion
    }
}

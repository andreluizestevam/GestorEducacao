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
    public partial class TB81_DATA_CTRL
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
        /// Exclue o registro da tabela TB81_DATA_CTRL do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB81_DATA_CTRL entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB81_DATA_CTRL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB81_DATA_CTRL.</returns>
        public static TB81_DATA_CTRL Delete(TB81_DATA_CTRL entity)
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
        public static int SaveOrUpdate(TB81_DATA_CTRL entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB81_DATA_CTRL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB81_DATA_CTRL.</returns>
        public static TB81_DATA_CTRL SaveOrUpdate(TB81_DATA_CTRL entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB81_DATA_CTRL de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB81_DATA_CTRL.</returns>
        public static TB81_DATA_CTRL GetByEntityKey(EntityKey entityKey)
        {
            return (TB81_DATA_CTRL)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB81_DATA_CTRL, ordenados pelo Id da série "CO_CUR".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB81_DATA_CTRL de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB81_DATA_CTRL> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB81_DATA_CTRL.OrderBy( d => d.CO_CUR ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB81_DATA_CTRL pelas chaves primárias "CO_EMP" e "CO_CUR".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_CUR">Id da série</param>
        /// <returns>Entidade TB81_DATA_CTRL</returns>
        public static TB81_DATA_CTRL RetornaPelaChavePrimaria(int CO_EMP, int CO_CUR)
        {
            return (from tb81 in RetornaTodosRegistros()
                    where tb81.TB01_CURSO.CO_CUR == CO_CUR && tb81.TB01_CURSO.CO_EMP == CO_EMP
                    select tb81).FirstOrDefault();
        }

        #endregion
    }
}
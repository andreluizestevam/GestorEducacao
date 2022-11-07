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
    public partial class TB105_ATIVIDADES_EXTRAS
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
        /// Exclue o registro da tabela TB105_ATIVIDADES_EXTRAS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB105_ATIVIDADES_EXTRAS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB105_ATIVIDADES_EXTRAS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB105_ATIVIDADES_EXTRAS.</returns>
        public static TB105_ATIVIDADES_EXTRAS Delete(TB105_ATIVIDADES_EXTRAS entity)
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
        public static int SaveOrUpdate(TB105_ATIVIDADES_EXTRAS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB105_ATIVIDADES_EXTRAS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB105_ATIVIDADES_EXTRAS.</returns>
        public static TB105_ATIVIDADES_EXTRAS SaveOrUpdate(TB105_ATIVIDADES_EXTRAS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB105_ATIVIDADES_EXTRAS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB105_ATIVIDADES_EXTRAS.</returns>
        public static TB105_ATIVIDADES_EXTRAS GetByEntityKey(EntityKey entityKey)
        {
            return (TB105_ATIVIDADES_EXTRAS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB105_ATIVIDADES_EXTRAS, ordenados pela descrição "DES_ATIV_EXTRA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB105_ATIVIDADES_EXTRAS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB105_ATIVIDADES_EXTRAS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB105_ATIVIDADES_EXTRAS.OrderBy( a => a.DES_ATIV_EXTRA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB105_ATIVIDADES_EXTRAS pela chave primária "CO_ATIV_EXTRA".
        /// </summary>
        /// <param name="CO_ATIV_EXTRA">Id da chave primária</param>
        /// <returns>Entidade TB105_ATIVIDADES_EXTRAS</returns>
        public static TB105_ATIVIDADES_EXTRAS RetornaPelaChavePrimaria(int CO_ATIV_EXTRA)
        {
            return (from tb105 in RetornaTodosRegistros()
                    where tb105.CO_ATIV_EXTRA == CO_ATIV_EXTRA
                    select tb105).FirstOrDefault();
        }

        #endregion
    }
}
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
    public partial class TB17_PLANO_AULA
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
        /// Exclue o registro da tabela TB17_PLANO_AULA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB17_PLANO_AULA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB17_PLANO_AULA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB17_PLANO_AULA.</returns>
        public static TB17_PLANO_AULA Delete(TB17_PLANO_AULA entity)
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
        public static int SaveOrUpdate(TB17_PLANO_AULA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB17_PLANO_AULA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB17_PLANO_AULA.</returns>
        public static TB17_PLANO_AULA SaveOrUpdate(TB17_PLANO_AULA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB17_PLANO_AULA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB17_PLANO_AULA.</returns>
        public static TB17_PLANO_AULA GetByEntityKey(EntityKey entityKey)
        {
            return (TB17_PLANO_AULA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB17_PLANO_AULA, ordenados pelo Id da unidade "CO_EMP" e pelo Id da série "CO_CUR".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB17_PLANO_AULA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB17_PLANO_AULA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB17_PLANO_AULA.OrderBy( p => p.CO_EMP ).ThenBy( p => p.CO_CUR ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB17_PLANO_AULA pela chave primária "CO_PLA_AULA".
        /// </summary>
        /// <param name="CO_PLA_AULA">Id da chave primária</param>
        /// <returns>Entidade TB17_PLANO_AULA</returns>
        public static TB17_PLANO_AULA RetornaPelaChavePrimaria(int CO_PLA_AULA)
        {
            return (from tb17 in RetornaTodosRegistros()
                    where tb17.CO_PLA_AULA == CO_PLA_AULA
                    select tb17).FirstOrDefault();
        }  
        #endregion
    }
}
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
    public partial class TB421_PARCEIROS
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
        /// Exclue o registro da tabela TB421_PARCE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB421_PARCEIROS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB421_PARCE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB421_PARCE.</returns>
        public static TB421_PARCEIROS Delete(TB421_PARCEIROS entity)
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
        public static int SaveOrUpdate(TB421_PARCEIROS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB421_PARCE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB421_PARCE.</returns>
        public static TB421_PARCEIROS SaveOrUpdate(TB421_PARCEIROS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB421_PARCE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB421_PARCE.</returns>
        public static TB421_PARCEIROS GetByEntityKey(EntityKey entityKey)
        {
            return (TB421_PARCEIROS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB421_PARCE, ordenados pelo nome "NO_COL".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB421_PARCE de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB421_PARCEIROS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB421_PARCEIROS.OrderBy(c => c.CO_PARCE).AsObjectQuery();
        }

        #endregion

      
        /// <summary>
        /// Retorna um registro da entidade TB421_PARCE 
        /// </summary>
        /// <returns>Entidade TB421_PARCE</returns>
        public static TB421_PARCEIROS RetornaPelaChavePrimaria(int CO_PARCE)
        {
            return (from tb421 in RetornaTodosRegistros()
                    where tb421.CO_PARCE == CO_PARCE
                    select tb421).FirstOrDefault();
        }

        #endregion
    }
}
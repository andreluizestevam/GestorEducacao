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
    public partial class TB213_CLASSIF_PATR
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
        /// Exclue o registro da tabela TB213_CLASSIF_PATR do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB213_CLASSIF_PATR entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB213_CLASSIF_PATR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB213_CLASSIF_PATR.</returns>
        public static TB213_CLASSIF_PATR Delete(TB213_CLASSIF_PATR entity)
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
        public static int SaveOrUpdate(TB213_CLASSIF_PATR entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB213_CLASSIF_PATR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB213_CLASSIF_PATR.</returns>
        public static TB213_CLASSIF_PATR SaveOrUpdate(TB213_CLASSIF_PATR entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB213_CLASSIF_PATR de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB213_CLASSIF_PATR.</returns>
        public static TB213_CLASSIF_PATR GetByEntityKey(EntityKey entityKey)
        {
            return (TB213_CLASSIF_PATR)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB213_CLASSIF_PATR, ordenados pelo nome "NO_CLASSIF_PATR".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB213_CLASSIF_PATR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB213_CLASSIF_PATR> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB213_CLASSIF_PATR.OrderBy( c => c.NO_CLASSIF_PATR ).AsObjectQuery();
        }
        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB213_CLASSIF_PATR pela chave primária "CO_CLASSIF_PATR".
        /// </summary>
        /// <param name="CO_CLASSIF_PATR">Id da chave primária</param>
        /// <returns>Entidade TB213_CLASSIF_PATR</returns>
        public static TB213_CLASSIF_PATR RetornaPelaChavePrimaria(int CO_CLASSIF_PATR)
        {
            return (from tb213 in RetornaTodosRegistros()
                    where tb213.CO_CLASSIF_PATR == CO_CLASSIF_PATR
                    select tb213).FirstOrDefault();
        }
        #endregion
    }
}

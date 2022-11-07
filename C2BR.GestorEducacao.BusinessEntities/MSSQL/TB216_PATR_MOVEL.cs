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
    public partial class TB216_PATR_MOVEL
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
        /// Exclue o registro da tabela TB216_PATR_MOVEL do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB216_PATR_MOVEL entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB216_PATR_MOVEL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB216_PATR_MOVEL.</returns>
        public static TB216_PATR_MOVEL Delete(TB216_PATR_MOVEL entity)
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
        public static int SaveOrUpdate(TB216_PATR_MOVEL entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB216_PATR_MOVEL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB216_PATR_MOVEL.</returns>
        public static TB216_PATR_MOVEL SaveOrUpdate(TB216_PATR_MOVEL entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB216_PATR_MOVEL de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB216_PATR_MOVEL.</returns>
        public static TB216_PATR_MOVEL GetByEntityKey(EntityKey entityKey)
        {
            return (TB216_PATR_MOVEL)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB216_PATR_MOVEL, ordenados pelo código "CO_PATR_MOVEL".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB216_PATR_MOVEL de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB216_PATR_MOVEL> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB216_PATR_MOVEL.OrderBy( p => p.CO_PATR_MOVEL ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB216_PATR_MOVEL pela chave primária "CO_PATR_MOVEL".
        /// </summary>
        /// <param name="CO_PATR_MOVEL">Id da chave primária</param>
        /// <returns>Entidade TB216_PATR_MOVEL</returns>
        public static TB216_PATR_MOVEL RetornaPelaChavePrimaria(decimal CO_PATR_MOVEL)
        {
            return (from tb216 in RetornaTodosRegistros()
                    where tb216.CO_PATR_MOVEL == CO_PATR_MOVEL
                    select tb216).FirstOrDefault();
        }

        #endregion
    }
}

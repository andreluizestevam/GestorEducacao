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
    public partial class TB53_GRP_CTA
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
        /// Exclue o registro da tabela TB53_GRP_CTA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB53_GRP_CTA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB53_GRP_CTA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB53_GRP_CTA.</returns>
        public static TB53_GRP_CTA Delete(TB53_GRP_CTA entity)
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
        public static int SaveOrUpdate(TB53_GRP_CTA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB53_GRP_CTA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB53_GRP_CTA.</returns>
        public static TB53_GRP_CTA SaveOrUpdate(TB53_GRP_CTA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB53_GRP_CTA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB53_GRP_CTA.</returns>
        public static TB53_GRP_CTA GetByEntityKey(EntityKey entityKey)
        {
            return (TB53_GRP_CTA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB53_GRP_CTA, ordenados pela descrição "DE_GRUP_CTA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB53_GRP_CTA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB53_GRP_CTA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB53_GRP_CTA.OrderBy( g => g.DE_GRUP_CTA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB53_GRP_CTA pela chave primária "CO_GRUP_CTA".
        /// </summary>
        /// <param name="CO_GRUP_CTA">Id da chave primária</param>
        /// <returns>Entidade TB53_GRP_CTA</returns>
        public static TB53_GRP_CTA RetornaPelaChavePrimaria(int CO_GRUP_CTA)
        {
            return (from tb53 in RetornaTodosRegistros()
                    where tb53.CO_GRUP_CTA == CO_GRUP_CTA
                    select tb53).FirstOrDefault();
        }

        #endregion
    }
}
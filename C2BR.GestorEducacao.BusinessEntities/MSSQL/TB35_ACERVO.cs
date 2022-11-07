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
    public partial class TB35_ACERVO
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
        /// Exclue o registro da tabela TB35_ACERVO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB35_ACERVO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB35_ACERVO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB35_ACERVO.</returns>
        public static TB35_ACERVO Delete(TB35_ACERVO entity)
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
        public static int SaveOrUpdate(TB35_ACERVO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB35_ACERVO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB35_ACERVO.</returns>
        public static TB35_ACERVO SaveOrUpdate(TB35_ACERVO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB35_ACERVO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB35_ACERVO.</returns>
        public static TB35_ACERVO GetByEntityKey(EntityKey entityKey)
        {
            return (TB35_ACERVO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB35_ACERVO, ordenados pelo código ISBN "CO_ISBN_ACER".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB35_ACERVO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB35_ACERVO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB35_ACERVO.OrderBy( a => a.CO_ISBN_ACER ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB35_ACERVO pela chave primária "ORG_CODIGO_ORGAO" e "CO_ISBN_ACER".
        /// </summary>
        /// <param name="ORG_CODIGO_ORGAO">Id da instituição</param>
        /// <param name="CO_ISBN_ACER">Código ISBN</param>
        /// <returns>Entidade TB35_ACERVO</returns>
        public static TB35_ACERVO RetornaPelaChavePrimaria(int ORG_CODIGO_ORGAO, decimal CO_ISBN_ACER)
        {
            return (from tb35 in RetornaTodosRegistros()
                    where tb35.ORG_CODIGO_ORGAO == ORG_CODIGO_ORGAO && tb35.CO_ISBN_ACER == CO_ISBN_ACER
                    select tb35).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB35_ACERVO de acordo com a instituição "ORG_CODIGO_ORGAO".
        /// </summary>
        /// <param name="ORG_CODIGO_ORGAO">Id da instituição</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB35_ACERVO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB35_ACERVO> RetornaPelaInstituicao(int ORG_CODIGO_ORGAO)
        {
            return GestorEntities.CurrentContext.TB35_ACERVO.Where( a => a.ORG_CODIGO_ORGAO == ORG_CODIGO_ORGAO ).AsObjectQuery();
        }

        #endregion
    }
}
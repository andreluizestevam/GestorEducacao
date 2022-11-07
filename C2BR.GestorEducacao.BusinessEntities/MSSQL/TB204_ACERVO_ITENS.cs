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
    public partial class TB204_ACERVO_ITENS
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
        /// Exclue o registro da tabela TB204_ACERVO_ITENS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB204_ACERVO_ITENS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB204_ACERVO_ITENS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB204_ACERVO_ITENS.</returns>
        public static TB204_ACERVO_ITENS Delete(TB204_ACERVO_ITENS entity)
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
        public static int SaveOrUpdate(TB204_ACERVO_ITENS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB204_ACERVO_ITENS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB204_ACERVO_ITENS.</returns>
        public static TB204_ACERVO_ITENS SaveOrUpdate(TB204_ACERVO_ITENS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB204_ACERVO_ITENS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB204_ACERVO_ITENS.</returns>
        public static TB204_ACERVO_ITENS GetByEntityKey(EntityKey entityKey)
        {
            return (TB204_ACERVO_ITENS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB204_ACERVO_ITENS, ordenados pelo código "CO_ISBN_ACER".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB204_ACERVO_ITENS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB204_ACERVO_ITENS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB204_ACERVO_ITENS.OrderBy( a => a.CO_ISBN_ACER ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB204_ACERVO_ITENS pela chave primária "ORG_CODIGO_ORGAO", "CO_ISBN_ACER", "CO_ACERVO_AQUISI", "CO_ACERVO_ITENS" e "CO_EMP".
        /// </summary>
        /// <param name="ORG_CODIGO_ORGAO">Id da instituição</param>
        /// <param name="CO_ISBN_ACER">Código ISBN</param>
        /// <param name="CO_ACERVO_AQUISI">Id da aquisição do acervo</param>
        /// <param name="CO_ACERVO_ITENS">Id do item do acervo</param>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>Entidade TB204_ACERVO_ITENS</returns>
        public static TB204_ACERVO_ITENS RetornaPelaChavePrimaria(int ORG_CODIGO_ORGAO, decimal CO_ISBN_ACER, int CO_ACERVO_AQUISI, int CO_ACERVO_ITENS, int CO_EMP)
        {
            return (from tb204 in RetornaTodosRegistros()
                    where tb204.ORG_CODIGO_ORGAO == ORG_CODIGO_ORGAO && tb204.CO_ISBN_ACER == CO_ISBN_ACER && tb204.CO_ACERVO_AQUISI == CO_ACERVO_AQUISI
                    && tb204.CO_ACERVO_ITENS == CO_ACERVO_ITENS && tb204.TB25_EMPRESA.CO_EMP == CO_EMP
                    select tb204).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TB204_ACERVO_ITENS de acordo com a instituição "ORG_CODIGO_ORGAO", código isbn "CO_ISBN_ACER" e unidade "CO_EMP".
        /// </summary>
        /// <param name="ORG_CODIGO_ORGAO">Id da instituição</param>
        /// <param name="CO_ISBN_ACER">Código ISBN</param>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns></returns>
        public static TB204_ACERVO_ITENS RetornaPelaInstISBNUnid(int ORG_CODIGO_ORGAO, decimal CO_ISBN_ACER, int CO_EMP)
        {
            return (from tb204 in RetornaTodosRegistros()
                    where tb204.ORG_CODIGO_ORGAO == ORG_CODIGO_ORGAO && tb204.CO_ISBN_ACER == CO_ISBN_ACER && tb204.TB25_EMPRESA.CO_EMP == CO_EMP
                    select tb204).FirstOrDefault();
        }
        
        #endregion
    }
}

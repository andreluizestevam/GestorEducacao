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
    public partial class TB000_INSTITUICAO
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
        /// Exclue o registro da tabela TB000_INSTITUICAO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB000_INSTITUICAO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB000_INSTITUICAO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB000_INSTITUICAO.</returns>
        public static TB000_INSTITUICAO Delete(TB000_INSTITUICAO entity)
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
        public static int SaveOrUpdate(TB000_INSTITUICAO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB000_INSTITUICAO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB000_INSTITUICAO.</returns>
        public static TB000_INSTITUICAO SaveOrUpdate(TB000_INSTITUICAO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB000_INSTITUICAO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB000_INSTITUICAO.</returns>
        public static TB000_INSTITUICAO GetByEntityKey(EntityKey entityKey)
        {
            return (TB000_INSTITUICAO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB000_INSTITUICAO, ordenados pelo nome "ORG_NOME_ORGAO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB000_INSTITUICAO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB000_INSTITUICAO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB000_INSTITUICAO.OrderBy( i => i.ORG_NOME_ORGAO ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB000_INSTITUICAO onde o Id "ORG_CODIGO_ORGAO" é o informado no parâmetro.
        /// </summary>
        /// <param name="ORG_CODIGO_ORGAO">Id da chave primária</param>
        /// <returns>Entidade TB000_INSTITUICAO</returns>
        public static TB000_INSTITUICAO RetornaPelaChavePrimaria(int ORG_CODIGO_ORGAO)
        {
            return (from tb000 in RetornaTodosRegistros()
                    where tb000.ORG_CODIGO_ORGAO == ORG_CODIGO_ORGAO
                    select tb000).FirstOrDefault();
        }

        #endregion
    }
}

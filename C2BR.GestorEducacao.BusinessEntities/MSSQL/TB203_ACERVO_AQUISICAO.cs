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
    public partial class TB203_ACERVO_AQUISICAO
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
        /// Exclue o registro da tabela TB203_ACERVO_AQUISICAO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB203_ACERVO_AQUISICAO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB203_ACERVO_AQUISICAO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB203_ACERVO_AQUISICAO.</returns>
        public static TB203_ACERVO_AQUISICAO Delete(TB203_ACERVO_AQUISICAO entity)
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
        public static int SaveOrUpdate(TB203_ACERVO_AQUISICAO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB203_ACERVO_AQUISICAO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB203_ACERVO_AQUISICAO.</returns>
        public static TB203_ACERVO_AQUISICAO SaveOrUpdate(TB203_ACERVO_AQUISICAO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB203_ACERVO_AQUISICAO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB203_ACERVO_AQUISICAO.</returns>
        public static TB203_ACERVO_AQUISICAO GetByEntityKey(EntityKey entityKey)
        {
            return (TB203_ACERVO_AQUISICAO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB203_ACERVO_AQUISICAO, ordenados pela data de cadastro "DT_CADASTRO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB203_ACERVO_AQUISICAO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB203_ACERVO_AQUISICAO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB203_ACERVO_AQUISICAO.OrderBy( a => a.DT_CADASTRO).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB203_ACERVO_AQUISICAO pela chave primária "CO_ACERVO_AQUISI".
        /// </summary>
        /// <param name="CO_ACERVO_AQUISI">Id da chave primária</param>
        /// <returns>Entidade TB203_ACERVO_AQUISICAO</returns>
        public static TB203_ACERVO_AQUISICAO RetornaPelaChavePrimaria(int CO_ACERVO_AQUISI)
        {
            return (from tb203 in RetornaTodosRegistros()
                    where tb203.CO_ACERVO_AQUISI == CO_ACERVO_AQUISI
                    select tb203).FirstOrDefault();
        }

        #endregion
    }
}

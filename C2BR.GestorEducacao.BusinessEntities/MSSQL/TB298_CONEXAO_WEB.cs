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
    public partial class TB298_CONEXAO_WEB
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
        /// Exclue o registro da tabela TB298_CONEXAO_WEB do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB298_CONEXAO_WEB entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB298_CONEXAO_WEB na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB298_CONEXAO_WEB.</returns>
        public static TB298_CONEXAO_WEB Delete(TB298_CONEXAO_WEB entity)
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
        public static int SaveOrUpdate(TB298_CONEXAO_WEB entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB298_CONEXAO_WEB na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB298_CONEXAO_WEB.</returns>
        public static TB298_CONEXAO_WEB SaveOrUpdate(TB298_CONEXAO_WEB entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB298_CONEXAO_WEB de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB298_CONEXAO_WEB.</returns>
        public static TB298_CONEXAO_WEB GetByEntityKey(EntityKey entityKey)
        {
            return (TB298_CONEXAO_WEB)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB298_CONEXAO_WEB, ordenados pelo Id "ID_CONEX_WEB".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB298_CONEXAO_WEB de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB298_CONEXAO_WEB> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB298_CONEXAO_WEB.OrderBy( c => c.ID_CONEX_WEB ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB298_CONEXAO_WEB pela chave primária "ID_CONEX_WEB".
        /// </summary>
        /// <param name="ID_CONEX_WEB">Id da chave primária</param>
        /// <returns>Entidade TB298_CONEXAO_WEB</returns>
        public static TB298_CONEXAO_WEB RetornaPelaChavePrimaria(int ID_CONEX_WEB)
        {
            return (from tb298 in RetornaTodosRegistros()
                    where tb298.ID_CONEX_WEB == ID_CONEX_WEB
                    select tb298).FirstOrDefault();
        }

        #endregion
    }
}
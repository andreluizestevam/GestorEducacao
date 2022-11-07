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
    public partial class TB149_PARAM_INSTI
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
        /// Exclue o registro da tabela TB149_PARAM_INSTI do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB149_PARAM_INSTI entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB149_PARAM_INSTI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB149_PARAM_INSTI.</returns>
        public static TB149_PARAM_INSTI Delete(TB149_PARAM_INSTI entity)
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
        public static int SaveOrUpdate(TB149_PARAM_INSTI entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB149_PARAM_INSTI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB149_PARAM_INSTI.</returns>
        public static TB149_PARAM_INSTI SaveOrUpdate(TB149_PARAM_INSTI entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB149_PARAM_INSTI de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB149_PARAM_INSTI.</returns>
        public static TB149_PARAM_INSTI GetByEntityKey(EntityKey entityKey)
        {
            return (TB149_PARAM_INSTI)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB149_PARAM_INSTI, ordenados pelo Id "ORG_CODIGO_ORGAO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB149_PARAM_INSTI de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB149_PARAM_INSTI> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB149_PARAM_INSTI.OrderBy( p => p.ORG_CODIGO_ORGAO ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB149_PARAM_INSTI pela chave primária "ORG_CODIGO_ORGAO".
        /// </summary>
        /// <param name="ORG_CODIGO_ORGAO">Id da chave primária</param>
        /// <returns>Entidade TB149_PARAM_INSTI</returns>
        public static TB149_PARAM_INSTI RetornaPelaChavePrimaria(int ORG_CODIGO_ORGAO)
        {
            return (from tb149 in RetornaTodosRegistros()
                    where tb149.ORG_CODIGO_ORGAO == ORG_CODIGO_ORGAO
                    select tb149).FirstOrDefault();
        }

        #endregion
    }
}
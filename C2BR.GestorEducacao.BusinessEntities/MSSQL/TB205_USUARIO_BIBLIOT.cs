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
    public partial class TB205_USUARIO_BIBLIOT
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
        /// Exclue o registro da tabela TB205_USUARIO_BIBLIOT do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB205_USUARIO_BIBLIOT entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB205_USUARIO_BIBLIOT na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB205_USUARIO_BIBLIOT.</returns>
        public static TB205_USUARIO_BIBLIOT Delete(TB205_USUARIO_BIBLIOT entity)
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
        public static int SaveOrUpdate(TB205_USUARIO_BIBLIOT entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB205_USUARIO_BIBLIOT na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB205_USUARIO_BIBLIOT.</returns>
        public static TB205_USUARIO_BIBLIOT SaveOrUpdate(TB205_USUARIO_BIBLIOT entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB205_USUARIO_BIBLIOT de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB205_USUARIO_BIBLIOT.</returns>
        public static TB205_USUARIO_BIBLIOT GetByEntityKey(EntityKey entityKey)
        {
            return (TB205_USUARIO_BIBLIOT)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB205_USUARIO_BIBLIOT, ordenados pelo nome "NO_USU_BIB".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB205_USUARIO_BIBLIOT de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB205_USUARIO_BIBLIOT> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB205_USUARIO_BIBLIOT.OrderBy( u => u.NO_USU_BIB ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB205_USUARIO_BIBLIOT pela chave primária "CO_USUARIO_BIBLIOT".
        /// </summary>
        /// <param name="CO_USUARIO_BIBLIOT">Id da chave primária</param>
        /// <returns>Entidade TB205_USUARIO_BIBLIOT</returns>
        public static TB205_USUARIO_BIBLIOT RetornaPelaChavePrimaria(int CO_USUARIO_BIBLIOT)
        {
            return (from tb205 in RetornaTodosRegistros()
                    where tb205.CO_USUARIO_BIBLIOT == CO_USUARIO_BIBLIOT
                    select tb205).FirstOrDefault();
        }

        #endregion
    }
}

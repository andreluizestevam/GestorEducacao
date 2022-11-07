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
    public partial class TB115_GRUPO_ESPECIALIDADE
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
        /// Exclue o registro da tabela TB115_GRUPO_ESPECIALIDADE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB115_GRUPO_ESPECIALIDADE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB115_GRUPO_ESPECIALIDADE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB115_GRUPO_ESPECIALIDADE.</returns>
        public static TB115_GRUPO_ESPECIALIDADE Delete(TB115_GRUPO_ESPECIALIDADE entity)
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
        public static int SaveOrUpdate(TB115_GRUPO_ESPECIALIDADE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Retorna um registro da entidade TB129_CADTURMAS pela chave primária "CO_TUR".
        /// </summary>
        /// <param name="CO_TUR">Id da chave primária</param>
        /// <returns>Entidade TB129_CADTURMAS</returns>
        public static TB115_GRUPO_ESPECIALIDADE RetornaPelaChavePrimaria(int ID_GRUPO_ESPECI)
        {
            return (from tb128 in RetornaTodosRegistros()
                    where tb128.ID_GRUPO_ESPECI == ID_GRUPO_ESPECI
                    select tb128).FirstOrDefault();
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB115_GRUPO_ESPECIALIDADE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB115_GRUPO_ESPECIALIDADE.</returns>
        public static TB115_GRUPO_ESPECIALIDADE SaveOrUpdate(TB115_GRUPO_ESPECIALIDADE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB115_GRUPO_ESPECIALIDADE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB115_GRUPO_ESPECIALIDADE.</returns>
        public static TB115_GRUPO_ESPECIALIDADE GetByEntityKey(EntityKey entityKey)
        {
            return (TB115_GRUPO_ESPECIALIDADE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB115_GRUPO_ESPECIALIDADE, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB115_GRUPO_ESPECIALIDADE de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB115_GRUPO_ESPECIALIDADE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB115_GRUPO_ESPECIALIDADE.AsObjectQuery();
        }

        #endregion

        #endregion
    }
}

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
    public partial class TB59_GESTOR_UNIDAD
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
        /// Exclue o registro da tabela TB59_GESTOR_UNIDAD do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB59_GESTOR_UNIDAD entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB59_GESTOR_UNIDAD na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB59_GESTOR_UNIDAD.</returns>
        public static TB59_GESTOR_UNIDAD Delete(TB59_GESTOR_UNIDAD entity)
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
        public static int SaveOrUpdate(TB59_GESTOR_UNIDAD entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB59_GESTOR_UNIDAD na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB59_GESTOR_UNIDAD.</returns>
        public static TB59_GESTOR_UNIDAD SaveOrUpdate(TB59_GESTOR_UNIDAD entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB59_GESTOR_UNIDAD de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB59_GESTOR_UNIDAD.</returns>
        public static TB59_GESTOR_UNIDAD GetByEntityKey(EntityKey entityKey)
        {
            return (TB59_GESTOR_UNIDAD)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB59_GESTOR_UNIDAD, ordenados pelo Id "IDE_GESTOR_UNIDAD".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB59_GESTOR_UNIDAD de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB59_GESTOR_UNIDAD> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB59_GESTOR_UNIDAD.OrderBy( g => g.IDE_GESTOR_UNIDAD ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB59_GESTOR_UNIDAD pela chave primária "IDE_GESTOR_UNIDAD".
        /// </summary>
        /// <param name="IDE_GESTOR_UNIDAD">Id da chave primária</param>
        /// <returns>Entidade TB59_GESTOR_UNIDAD</returns>
        public static TB59_GESTOR_UNIDAD RetornaPelaChavePrimaria(int IDE_GESTOR_UNIDAD)
        {
            return (from tb59 in RetornaTodosRegistros()
                    where tb59.IDE_GESTOR_UNIDAD == IDE_GESTOR_UNIDAD
                    select tb59).FirstOrDefault();
        }

        #endregion
    }
}

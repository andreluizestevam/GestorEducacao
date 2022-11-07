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
    public partial class TB129_CADTURMAS
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
        /// Exclue o registro da tabela TB129_CADTURMAS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB129_CADTURMAS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB129_CADTURMAS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB129_CADTURMAS.</returns>
        public static TB129_CADTURMAS Delete(TB129_CADTURMAS entity)
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
        public static int SaveOrUpdate(TB129_CADTURMAS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB129_CADTURMAS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB129_CADTURMAS.</returns>
        public static TB129_CADTURMAS SaveOrUpdate(TB129_CADTURMAS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB129_CADTURMAS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB129_CADTURMAS.</returns>
        public static TB129_CADTURMAS GetByEntityKey(EntityKey entityKey)
        {
            return (TB129_CADTURMAS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB129_CADTURMAS, ordenados pelo nome "NO_TURMA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB129_CADTURMAS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB129_CADTURMAS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB129_CADTURMAS.OrderBy( c => c.NO_TURMA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB129_CADTURMAS pela chave primária "CO_TUR".
        /// </summary>
        /// <param name="CO_TUR">Id da chave primária</param>
        /// <returns>Entidade TB129_CADTURMAS</returns>
        public static TB129_CADTURMAS RetornaPelaChavePrimaria(int CO_TUR)
        {
            return (from tb129 in RetornaTodosRegistros()
                    where tb129.CO_TUR == CO_TUR
                    select tb129).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB129_CADTURMAS de acordo com a unidade "CO_EMP".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB129_CADTURMAS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB129_CADTURMAS> RetornaPelaEmpresa(int CO_EMP)
        {
            return (from tb129 in RetornaTodosRegistros()
                    where tb129.TB25_EMPRESA.CO_EMP == CO_EMP
                    select tb129).AsObjectQuery();
        }

        #endregion
    }
}
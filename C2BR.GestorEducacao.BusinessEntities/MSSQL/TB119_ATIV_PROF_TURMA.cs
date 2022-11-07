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
    public partial class TB119_ATIV_PROF_TURMA
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
        /// Exclue o registro da tabela TB119_ATIV_PROF_TURMA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB119_ATIV_PROF_TURMA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB119_ATIV_PROF_TURMA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB119_ATIV_PROF_TURMA.</returns>
        public static TB119_ATIV_PROF_TURMA Delete(TB119_ATIV_PROF_TURMA entity)
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
        public static int SaveOrUpdate(TB119_ATIV_PROF_TURMA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB119_ATIV_PROF_TURMA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB119_ATIV_PROF_TURMA.</returns>
        public static TB119_ATIV_PROF_TURMA SaveOrUpdate(TB119_ATIV_PROF_TURMA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB119_ATIV_PROF_TURMA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB119_ATIV_PROF_TURMA.</returns>
        public static TB119_ATIV_PROF_TURMA GetByEntityKey(EntityKey entityKey)
        {
            return (TB119_ATIV_PROF_TURMA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB119_ATIV_PROF_TURMA, ordenados pela Id "CO_ATIV_PROF_TUR".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB119_ATIV_PROF_TURMA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB119_ATIV_PROF_TURMA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB119_ATIV_PROF_TURMA.OrderBy( a => a.CO_ATIV_PROF_TUR ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB119_ATIV_PROF_TURMA pela chave primária "CO_ATIV_PROF_TUR".
        /// </summary>
        /// <param name="CO_ATIV_PROF_TUR">Id da chave primária</param>
        /// <returns>Entidade TB119_ATIV_PROF_TURMA</returns>
        public static TB119_ATIV_PROF_TURMA RetornaPelaChavePrimaria(int CO_ATIV_PROF_TUR)
        {
            return (from tb119 in RetornaTodosRegistros()
                    where tb119.CO_ATIV_PROF_TUR == CO_ATIV_PROF_TUR
                    select tb119).FirstOrDefault();
        }

        #endregion
    }
}
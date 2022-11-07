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
    public partial class TB142_LOG_ATIV_PROF_TURMA
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
        /// Exclue o registro da tabela TB142_LOG_ATIV_PROF_TURMA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB142_LOG_ATIV_PROF_TURMA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB142_LOG_ATIV_PROF_TURMA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB142_LOG_ATIV_PROF_TURMA.</returns>
        public static TB142_LOG_ATIV_PROF_TURMA Delete(TB142_LOG_ATIV_PROF_TURMA entity)
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
        public static int SaveOrUpdate(TB142_LOG_ATIV_PROF_TURMA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB142_LOG_ATIV_PROF_TURMA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB142_LOG_ATIV_PROF_TURMA.</returns>
        public static TB142_LOG_ATIV_PROF_TURMA SaveOrUpdate(TB142_LOG_ATIV_PROF_TURMA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB142_LOG_ATIV_PROF_TURMA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB142_LOG_ATIV_PROF_TURMA.</returns>
        public static TB142_LOG_ATIV_PROF_TURMA GetByEntityKey(EntityKey entityKey)
        {
            return (TB142_LOG_ATIV_PROF_TURMA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB142_LOG_ATIV_PROF_TURMA
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB142_LOG_ATIV_PROF_TURMA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB142_LOG_ATIV_PROF_TURMA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB142_LOG_ATIV_PROF_TURMA.AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB142_LOG_ATIV_PROF_TURMA pela chave primária "ID_LOG_ATIV".
        /// </summary>
        /// <param name="ID_LOG_ATIV">Id da chave primária</param>
        /// <returns>Entidade TB142_LOG_ATIV_PROF_TURMA</returns>
        public static TB142_LOG_ATIV_PROF_TURMA RetornaPelaChavePrimaria(int ID_LOG_ATIV)
        {
            return (from tb142 in TB142_LOG_ATIV_PROF_TURMA.RetornaTodosRegistros()
                    where tb142.ID_LOG_ATIV == ID_LOG_ATIV
                    select tb142).FirstOrDefault();
        }

        #endregion
    }
}

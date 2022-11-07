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
    public partial class TB141_LOG_FREQU_ALUNO
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
        /// Exclue o registro da tabela TB141_LOG_FREQU_ALUNO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB141_LOG_FREQU_ALUNO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB141_LOG_FREQU_ALUNO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB132_FREQ_ALU.</returns>
        public static TB141_LOG_FREQU_ALUNO Delete(TB141_LOG_FREQU_ALUNO entity)
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
        public static int SaveOrUpdate(TB141_LOG_FREQU_ALUNO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB141_LOG_FREQU_ALUNO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB141_LOG_FREQU_ALUNO.</returns>
        public static TB141_LOG_FREQU_ALUNO SaveOrUpdate(TB141_LOG_FREQU_ALUNO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB141_LOG_FREQU_ALUNO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB141_LOG_FREQU_ALUNO.</returns>
        public static TB141_LOG_FREQU_ALUNO GetByEntityKey(EntityKey entityKey)
        {
            return (TB141_LOG_FREQU_ALUNO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB141_LOG_FREQU_ALUNO
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB132_FREQ_ALU de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB141_LOG_FREQU_ALUNO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB141_LOG_FREQU_ALUNO.AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB141_LOG_FREQU_ALUNO pela chave primária "ID_LOG_FREQU".
        /// </summary>
        /// <param name="ID_LOG_FREQU">Id da chave primária</param>
        /// <returns>Entidade TB141_LOG_FREQU_ALUNO</returns>
        public static TB141_LOG_FREQU_ALUNO RetornaPelaChavePrimaria(int ID_LOG_FREQU)
        {
            return (from tb141 in TB141_LOG_FREQU_ALUNO.RetornaTodosRegistros()
                    where tb141.ID_LOG_FREQU == ID_LOG_FREQU
                    select tb141).FirstOrDefault();
        }

        #endregion
    }
}

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
    public partial class TB248_UNIDADE_SALAS_AULA
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
        /// Exclue o registro da tabela TB248_UNIDADE_SALAS_AULA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB248_UNIDADE_SALAS_AULA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB248_UNIDADE_SALAS_AULA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB248_UNIDADE_SALAS_AULA.</returns>
        public static TB248_UNIDADE_SALAS_AULA Delete(TB248_UNIDADE_SALAS_AULA entity)
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
        public static int SaveOrUpdate(TB248_UNIDADE_SALAS_AULA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB248_UNIDADE_SALAS_AULA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB248_UNIDADE_SALAS_AULA.</returns>
        public static TB248_UNIDADE_SALAS_AULA SaveOrUpdate(TB248_UNIDADE_SALAS_AULA entity)
        {
            SaveOrUpdate(entity, true);
            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB248_UNIDADE_SALAS_AULA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB248_UNIDADE_SALAS_AULA.</returns>
        public static TB248_UNIDADE_SALAS_AULA GetByEntityKey(EntityKey entityKey)
        {
            return (TB248_UNIDADE_SALAS_AULA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB248_UNIDADE_SALAS_AULA, ordenados pelo Id da unidade "TB25_EMPRESA.CO_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB248_UNIDADE_SALAS_AULA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB248_UNIDADE_SALAS_AULA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB248_UNIDADE_SALAS_AULA.OrderBy( u => u.TB25_EMPRESA.CO_EMP ).AsObjectQuery();
        }
        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB248_UNIDADE_SALAS_AULA pela chave primária "ID_SALA_AULA".
        /// </summary>
        /// <param name="ID_SALA_AULA">Id da chave primária</param>
        /// <returns>Entidade TB248_UNIDADE_SALAS_AULA</returns>
        public static TB248_UNIDADE_SALAS_AULA RetornaPelaChavePrimaria(int ID_SALA_AULA)
        {
            return (from tb248 in RetornaTodosRegistros()
                    where tb248.ID_SALA_AULA == ID_SALA_AULA
                    select tb248).FirstOrDefault();
        }
        #endregion
    }
}

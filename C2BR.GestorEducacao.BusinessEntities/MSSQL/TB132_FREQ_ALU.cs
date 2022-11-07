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
    public partial class TB132_FREQ_ALU
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
        /// Exclue o registro da tabela TB132_FREQ_ALU do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB132_FREQ_ALU entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB132_FREQ_ALU na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB132_FREQ_ALU.</returns>
        public static TB132_FREQ_ALU Delete(TB132_FREQ_ALU entity)
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
        public static int SaveOrUpdate(TB132_FREQ_ALU entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB132_FREQ_ALU na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB132_FREQ_ALU.</returns>
        public static TB132_FREQ_ALU SaveOrUpdate(TB132_FREQ_ALU entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB132_FREQ_ALU de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB132_FREQ_ALU.</returns>
        public static TB132_FREQ_ALU GetByEntityKey(EntityKey entityKey)
        {
            return (TB132_FREQ_ALU)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB132_FREQ_ALU, ordenados pelo Id do aluno "TB07_ALUNO.CO_ALU".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB132_FREQ_ALU de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB132_FREQ_ALU> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB132_FREQ_ALU.OrderBy( f => f.TB07_ALUNO.CO_ALU ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB132_FREQ_ALU pela chave primária "ID_FREQ_ALUNO".
        /// </summary>
        /// <param name="ID_FREQ_ALUNO">Id da chave primária</param>
        /// <returns>Entidade TB132_FREQ_ALU</returns>
        public static TB132_FREQ_ALU RetornaPelaChavePrimaria(int ID_FREQ_ALUNO)
        {
            return (from tb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                    where tb132.ID_FREQ_ALUNO == ID_FREQ_ALUNO
                    select tb132).FirstOrDefault();
        }

        #endregion
    }
}
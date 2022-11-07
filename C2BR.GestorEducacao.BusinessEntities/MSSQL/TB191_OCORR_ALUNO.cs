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
    public partial class TB191_OCORR_ALUNO
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
        /// Exclue o registro da tabela TB191_OCORR_ALUNO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB191_OCORR_ALUNO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB191_OCORR_ALUNO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB191_OCORR_ALUNO.</returns>
        public static TB191_OCORR_ALUNO Delete(TB191_OCORR_ALUNO entity)
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
        public static int SaveOrUpdate(TB191_OCORR_ALUNO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB191_OCORR_ALUNO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB191_OCORR_ALUNO.</returns>
        public static TB191_OCORR_ALUNO SaveOrUpdate(TB191_OCORR_ALUNO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB191_OCORR_ALUNO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB191_OCORR_ALUNO.</returns>
        public static TB191_OCORR_ALUNO GetByEntityKey(EntityKey entityKey)
        {
            return (TB191_OCORR_ALUNO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB191_OCORR_ALUNO, ordenados pelo Id "IDE_OCORR_ALUNO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB191_OCORR_ALUNO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB191_OCORR_ALUNO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB191_OCORR_ALUNO.OrderBy( o => o.IDE_OCORR_ALUNO ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB191_OCORR_ALUNO pela chave primária "IDE_OCORR_ALUNO".
        /// </summary>
        /// <param name="IDE_OCORR_ALUNO">Id da chave primária</param>
        /// <returns>Entidade TB191_OCORR_ALUNO</returns>
        public static TB191_OCORR_ALUNO RetornaPelaChavePrimaria(int IDE_OCORR_ALUNO)
        {
            return (from tb191 in RetornaTodosRegistros()
                    where tb191.IDE_OCORR_ALUNO == IDE_OCORR_ALUNO
                    select tb191).FirstOrDefault();
        }

        #endregion
    }
}
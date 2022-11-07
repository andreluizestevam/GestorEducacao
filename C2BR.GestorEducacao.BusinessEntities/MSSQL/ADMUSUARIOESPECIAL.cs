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
    public partial class ADMUSUARIOESPECIAL
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
        /// Exclue o registro da tabela ADMUSUARIOESPECIAL do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(ADMUSUARIOESPECIAL entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela ADMUSUARIOESPECIAL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade ADMUSUARIOESPECIAL.</returns>
        public static ADMUSUARIOESPECIAL Delete(ADMUSUARIOESPECIAL entity)
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
        public static int SaveOrUpdate(ADMUSUARIOESPECIAL entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela ADMUSUARIOESPECIAL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade ADMUSUARIOESPECIAL.</returns>
        public static ADMUSUARIOESPECIAL SaveOrUpdate(ADMUSUARIOESPECIAL entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade ADMUSUARIOESPECIAL de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade ADMUSUARIOESPECIAL.</returns>
        public static ADMUSUARIOESPECIAL GetByEntityKey(EntityKey entityKey)
        {
            return (ADMUSUARIOESPECIAL)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade ADMUSUARIOESPECIAL, ordenados pelo nome do usuário especial "NO_USUAR_ESPEC".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade ADMUSUARIOESPECIAL de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<ADMUSUARIOESPECIAL> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.ADMUSUARIOESPECIAL.OrderBy( a => a.NO_USUAR_ESPEC ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna o primeiro registro da entidade ADMUSUARIOESPECIAL onde o Id "IdeAdmUsuEsp" é o informado no parâmetro.
        /// </summary>
        /// <param name="IdeAdmUsuEsp">Id da chave primária</param>
        /// <returns>Entidade ADMUSUARIOESPECIAL</returns>
        public static ADMUSUARIOESPECIAL RetornaPelaChavePrimaria(int IdeAdmUsuEsp)
        {
            return (from admUsuEsp in RetornaTodosRegistros()
                    where admUsuEsp.IdeAdmUsuEsp == IdeAdmUsuEsp
                    select admUsuEsp).FirstOrDefault();
        }

        #endregion
    }
}

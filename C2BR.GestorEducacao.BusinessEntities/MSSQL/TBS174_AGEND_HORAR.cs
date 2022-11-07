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
    public partial class TBS174_AGEND_HORAR
    {
       
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
        /// Exclue o registro da tabela TBS174_AGEND_HORAR do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS174_AGEND_HORAR entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS174_AGEND_HORAR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS174_AGEND_HORAR.</returns>
        public static TBS174_AGEND_HORAR Delete(TBS174_AGEND_HORAR entity)
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
        public static int SaveOrUpdate(TBS174_AGEND_HORAR entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS174_AGEND_HORAR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS174_AGEND_HORAR.</returns>
        public static TBS174_AGEND_HORAR SaveOrUpdate(TBS174_AGEND_HORAR entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS174_AGEND_HORAR de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS174_AGEND_HORAR.</returns>
        public static TBS174_AGEND_HORAR GetByEntityKey(EntityKey entityKey)
        {
            return (TBS174_AGEND_HORAR)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS174_AGEND_HORAR, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS174_AGEND_HORAR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS174_AGEND_HORAR> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS174_AGEND_HORAR.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS174_AGEND_HORAR pela chave primária "ID_AGEND_HORAR".
        /// </summary>
        /// <param name="ID_AGEND_HORAR">Id da chave primária</param>
        /// <returns>Entidade TBS174_AGEND_HORAR</returns>
        public static TBS174_AGEND_HORAR RetornaPelaChavePrimaria(int ID_AGEND_HORAR)
        {
            var ret = GestorEntities.CurrentContext.TBS174_AGEND_HORAR.FirstOrDefault(p => p.ID_AGEND_HORAR == ID_AGEND_HORAR);
            return ret;
           
        
        }
        #endregion

        public string NO_EMP { get; set; }
    }
}

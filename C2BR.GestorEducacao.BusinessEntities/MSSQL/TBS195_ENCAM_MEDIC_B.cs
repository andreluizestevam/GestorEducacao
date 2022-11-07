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
    public partial class TBS195_ENCAM_MEDIC_B
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
        /// Exclue o registro da tabela TBS195_ENCAM_MEDIC_B do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS195_ENCAM_MEDIC_B entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS195_ENCAM_MEDIC_B na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS195_ENCAM_MEDIC_B.</returns>
        public static TBS195_ENCAM_MEDIC_B Delete(TBS195_ENCAM_MEDIC_B entity)
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
        public static int SaveOrUpdate(TBS195_ENCAM_MEDIC_B entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS195_ENCAM_MEDIC_B na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS195_ENCAM_MEDIC_B.</returns>
        public static TBS195_ENCAM_MEDIC_B SaveOrUpdate(TBS195_ENCAM_MEDIC_B entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS195_ENCAM_MEDIC_B de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS195_ENCAM_MEDIC_B.</returns>
        public static TBS195_ENCAM_MEDIC_B GetByEntityKey(EntityKey entityKey)
        {
            return (TBS195_ENCAM_MEDIC_B)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS195_ENCAM_MEDIC_B, ordenados pelo nome "NO_FUN".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB15_FUNCAO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS195_ENCAM_MEDIC_B> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS195_ENCAM_MEDIC_B.AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TBS195_ENCAM_MEDIC_B pela chave primária "ID_ENCAM_MEDIC".
        /// </summary>
        /// <param name="CO_FUN">Id da chave primária</param>
        /// <returns>Entidade TBS195_ENCAM_MEDIC_B</returns>
        public static TBS195_ENCAM_MEDIC_B RetornaPelaChavePrimaria(int ID_ENCAM_MEDIC)
        {
            return (from tbs195_B in RetornaTodosRegistros()
                    where tbs195_B.ID_ENCAM_MEDIC == ID_ENCAM_MEDIC
                    select tbs195_B).FirstOrDefault();
        }

        #endregion
    }
}

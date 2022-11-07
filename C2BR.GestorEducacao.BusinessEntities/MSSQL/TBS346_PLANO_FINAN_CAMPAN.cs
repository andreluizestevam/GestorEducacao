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
    public partial class TBS346_PLANO_FINAN_CAMPAN
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
        /// Exclue o registro da tabela TBS346_PLANO_FINAN_CAMPAN do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS346_PLANO_FINAN_CAMPAN entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS346_PLANO_FINAN_CAMPAN na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS346_PLANO_FINAN_CAMPAN.</returns>
        public static TBS346_PLANO_FINAN_CAMPAN Delete(TBS346_PLANO_FINAN_CAMPAN entity)
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
        public static int SaveOrUpdate(TBS346_PLANO_FINAN_CAMPAN entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS346_PLANO_FINAN_CAMPAN na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS346_PLANO_FINAN_CAMPAN.</returns>
        public static TBS346_PLANO_FINAN_CAMPAN SaveOrUpdate(TBS346_PLANO_FINAN_CAMPAN entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS345_VACINA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS345_VACINA.</returns>
        public static TBS346_PLANO_FINAN_CAMPAN GetByEntityKey(EntityKey entityKey)
        {
            return (TBS346_PLANO_FINAN_CAMPAN)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS346_PLANO_FINAN_CAMPAN, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS334_DIAGN_ATEND_MEDIC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS346_PLANO_FINAN_CAMPAN> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS346_PLANO_FINAN_CAMPAN.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS346_PLANO_FINAN_CAMPAN pela chave primária "ID_PLANO_FINAN_CAMPAN".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS345_VACINA</returns>
        public static TBS346_PLANO_FINAN_CAMPAN RetornaPelaChavePrimaria(int ID_PLANO_FINAN_CAMPAN)
        {
            return (from tbs346 in RetornaTodosRegistros()
                    where tbs346.ID_PLANO_FINAN_CAMPAN == ID_PLANO_FINAN_CAMPAN
                    select tbs346).FirstOrDefault();
        }

        #endregion
    }
}

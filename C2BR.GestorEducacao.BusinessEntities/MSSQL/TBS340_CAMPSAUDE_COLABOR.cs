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
    public partial class TBS340_CAMPSAUDE_COLABOR
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
        /// Exclue o registro da tabela TBS340_CAMPSAUDE_COLABOR do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS340_CAMPSAUDE_COLABOR entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS336_ISDA_ITENS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS330_RECEI_ATEND_MEDIC.</returns>
        public static TBS340_CAMPSAUDE_COLABOR Delete(TBS340_CAMPSAUDE_COLABOR entity)
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
        public static int SaveOrUpdate(TBS340_CAMPSAUDE_COLABOR entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS340_CAMPSAUDE_COLABOR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS340_CAMPSAUDE_COLABOR.</returns>
        public static TBS340_CAMPSAUDE_COLABOR SaveOrUpdate(TBS340_CAMPSAUDE_COLABOR entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS336_ISDA_ITENS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS336_ISDA_ITENS.</returns>
        public static TBS340_CAMPSAUDE_COLABOR GetByEntityKey(EntityKey entityKey)
        {
            return (TBS340_CAMPSAUDE_COLABOR)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS340_CAMPSAUDE_COLABOR, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS334_DIAGN_ATEND_MEDIC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS340_CAMPSAUDE_COLABOR> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS340_CAMPSAUDE_COLABOR.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS340_CAMPSAUDE_COLABOR pela chave primária "ID_CID_GERAL".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS338_CID_GERAL</returns>
        public static TBS340_CAMPSAUDE_COLABOR RetornaPelaChavePrimaria(int ID_CAMPSAUDE_COLABOR)
        {
            return (from tbs340 in RetornaTodosRegistros()
                    where tbs340.ID_CAMPSAUDE_COLABOR == ID_CAMPSAUDE_COLABOR
                    select tbs340).FirstOrDefault();
        }

        #endregion
    }
}

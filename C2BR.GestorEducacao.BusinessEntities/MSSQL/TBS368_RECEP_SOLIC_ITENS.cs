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
    public partial class TBS368_RECEP_SOLIC_ITENS
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
        /// Exclue o registro da tabela TBS368_RECEP_SOLIC_ITENS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS368_RECEP_SOLIC_ITENS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS368_RECEP_SOLIC_ITENS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS368_RECEP_SOLIC_ITENS.</returns>
        public static TBS368_RECEP_SOLIC_ITENS Delete(TBS368_RECEP_SOLIC_ITENS entity)
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
        public static int SaveOrUpdate(TBS368_RECEP_SOLIC_ITENS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS368_RECEP_SOLIC_ITENS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS368_RECEP_SOLIC_ITENS.</returns>
        public static TBS368_RECEP_SOLIC_ITENS SaveOrUpdate(TBS368_RECEP_SOLIC_ITENS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS368_RECEP_SOLIC_ITENS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS368_RECEP_SOLIC_ITENS.</returns>
        public static TBS368_RECEP_SOLIC_ITENS GetByEntityKey(EntityKey entityKey)
        {
            return (TBS368_RECEP_SOLIC_ITENS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS368_RECEP_SOLIC_ITENS, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS334_DIAGN_ATEND_MEDIC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS368_RECEP_SOLIC_ITENS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS368_RECEP_SOLIC_ITENS.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS368_RECEP_SOLIC_ITENS pela chave primária "ID_RECEP_SOLIC_ITENS".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS368_RECEP_SOLIC_ITENS</returns>
        public static TBS368_RECEP_SOLIC_ITENS RetornaPelaChavePrimaria(int ID_RECEP_SOLIC_ITENS)
        {
            return (from tbs368 in RetornaTodosRegistros()
                    where tbs368.ID_RECEP_SOLIC_ITENS == ID_RECEP_SOLIC_ITENS
                    select tbs368).FirstOrDefault();
        }

        #endregion
    }
}

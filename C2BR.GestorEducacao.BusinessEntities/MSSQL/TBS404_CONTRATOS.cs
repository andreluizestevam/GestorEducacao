using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TBS404_CONTRATOS
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
        /// Exclue o registro da tabela TBS404_CONTRATOS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS404_CONTRATOS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS404_CONTRATOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS404_CONTRATOS.</returns>
        public static TBS404_CONTRATOS Delete(TBS404_CONTRATOS entity)
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
        public static int SaveOrUpdate(TBS404_CONTRATOS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS404_CONTRATOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS404_CONTRATOS.</returns>
        public static TBS404_CONTRATOS SaveOrUpdate(TBS404_CONTRATOS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS404_CONTRATOS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS404_CONTRATOS.</returns>
        public static TBS404_CONTRATOS GetByEntityKey(EntityKey entityKey)
        {
            return (TBS404_CONTRATOS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS404_CONTRATOS, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS394_RESUM_FINAN de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS404_CONTRATOS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS404_CONTRATOS.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS404_CONTRATOS pela chave primária "ID_ATEND_MEDICAMENTOS".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS404_CONTRATOS</returns>
        public static TBS404_CONTRATOS RetornaPelaChavePrimaria(int ID_CONTRATO)
        {
            return (from tbs404 in RetornaTodosRegistros()
                    where tbs404.ID_CONTRATO == ID_CONTRATO
                    select tbs404).FirstOrDefault();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS404_CONTRATOS pela chave primária "ID_ATEND_MEDICAMENTOS".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS404_CONTRATOS</returns>
        public static TBS404_CONTRATOS RetornaPeloNumContrato(string NU_CONTRATO)
        {
            return (from tbs404 in RetornaTodosRegistros()
                    where tbs404.NU_CONTRATO == NU_CONTRATO
                    select tbs404).FirstOrDefault();
        }

        #endregion
    }
}

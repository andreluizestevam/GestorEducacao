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
    public partial class TBS358_PROC_MEDIC_UNID
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
        /// Exclue o registro da tabela TBS347_CENTR_REGUL do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS358_PROC_MEDIC_UNID entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS358_PROC_MEDIC_UNID na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS358_PROC_MEDIC_UNID.</returns>
        public static TBS358_PROC_MEDIC_UNID Delete(TBS358_PROC_MEDIC_UNID entity)
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
        public static int SaveOrUpdate(TBS358_PROC_MEDIC_UNID entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS358_PROC_MEDIC_UNID na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS358_PROC_MEDIC_UNID.</returns>
        public static TBS358_PROC_MEDIC_UNID SaveOrUpdate(TBS358_PROC_MEDIC_UNID entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS358_PROC_MEDIC_UNID de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS358_PROC_MEDIC_UNID.</returns>
        public static TBS358_PROC_MEDIC_UNID GetByEntityKey(EntityKey entityKey)
        {
            return (TBS358_PROC_MEDIC_UNID)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS358_PROC_MEDIC_UNID, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS334_DIAGN_ATEND_MEDIC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS358_PROC_MEDIC_UNID> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS358_PROC_MEDIC_UNID.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS358_PROC_MEDIC_UNID pela chave primária "ID_PROC_MEDIC_UNID".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS358_PROC_MEDIC_UNID</returns>
        public static TBS358_PROC_MEDIC_UNID RetornaPelaChavePrimaria(int ID_PROC_MEDIC_UNID)
        {
            return (from tbs358 in RetornaTodosRegistros()
                    where tbs358.ID_PROC_MEDIC_UNID == ID_PROC_MEDIC_UNID
                    select tbs358).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o registro de associação de acordo com o procedimento e unidade recebidos
        /// </summary>
        /// <param name="ID_PROC_MEDI_PROCE_ASSOC"></param>
        /// <param name="CO_EMP_ASSOC"></param>
        /// <returns></returns>
        public static TBS358_PROC_MEDIC_UNID RetornaPelaUnidadeProcedimento(int ID_PROC_MEDI_PROCE_ASSOC, int CO_EMP_ASSOC)
        {
            return (from tbs358 in RetornaTodosRegistros()
                    where tbs358.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == ID_PROC_MEDI_PROCE_ASSOC
                    && tbs358.TB25_EMPRESA.CO_EMP == CO_EMP_ASSOC
                    select tbs358).FirstOrDefault();
        }

        #endregion
    }
}

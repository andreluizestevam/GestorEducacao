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
    public partial class TBS383_INFOS_GERAIS_PACIENTE
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
        /// Exclue o registro da tabela TBS383_INFOS_GERAIS_PACIENTE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS383_INFOS_GERAIS_PACIENTE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS383_INFOS_GERAIS_PACIENTE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS383_INFOS_GERAIS_PACIENTE.</returns>
        public static TBS383_INFOS_GERAIS_PACIENTE Delete(TBS383_INFOS_GERAIS_PACIENTE entity)
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
        public static int SaveOrUpdate(TBS383_INFOS_GERAIS_PACIENTE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS380_LOG_ALTER_STATUS_AGEND_AVALI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS383_INFOS_GERAIS_PACIENTE.</returns>
        public static TBS383_INFOS_GERAIS_PACIENTE SaveOrUpdate(TBS383_INFOS_GERAIS_PACIENTE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS383_INFOS_GERAIS_PACIENTE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS383_INFOS_GERAIS_PACIENTE.</returns>
        public static TBS383_INFOS_GERAIS_PACIENTE GetByEntityKey(EntityKey entityKey)
        {
            return (TBS383_INFOS_GERAIS_PACIENTE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS380_LOG_ALTER_STATUS_AGEND_AVALI, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS380_LOG_ALTER_STATUS_AGEND_AVALI de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS383_INFOS_GERAIS_PACIENTE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS383_INFOS_GERAIS_PACIENTE.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS383_INFOS_GERAIS_PACIENTE pela chave primária "ID_INFOS_GERAIS_PACIENTE".
        /// </summary>
        /// <param name="ID_AGEND_HORAR">Id da chave primária</param>
        /// <returns>Entidade TBS383_INFOS_GERAIS_PACIENTE</returns>
        public static TBS383_INFOS_GERAIS_PACIENTE RetornaPelaChavePrimaria(int ID_INFOS_GERAIS_PACIENTE)
        {
            return (from tbs383 in RetornaTodosRegistros()
                    where tbs383.ID_INFOS_GERAIS_PACIENTE == ID_INFOS_GERAIS_PACIENTE
                    select tbs383).FirstOrDefault();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS383_INFOS_GERAIS_PACIENTE pelo CO_ALU e o ID_INFOS_GERAIS_PACIENTE
        /// </summary>
        /// <param name="ID_AGEND_HORAR">Id da chave primária</param>
        /// <returns>Entidade TBS383_INFOS_GERAIS_PACIENTE</returns>
        public static ObjectQuery<TBS383_INFOS_GERAIS_PACIENTE> RetornaPeloPacienteEInfo(int CO_ALU, int ID_INFOS_GERAIS_PACIENTE)
        {
            return (from tbs383 in RetornaTodosRegistros()
                    where tbs383.ID_INFOS_GERAIS_PACIENTE == ID_INFOS_GERAIS_PACIENTE
                    && tbs383.CO_ALU == CO_ALU
                    select tbs383).AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS383_INFOS_GERAIS_PACIENTE pelo CO_ALU e o ID_INFOS_GERAIS_PACIENTE
        /// </summary>
        /// <param name="ID_AGEND_HORAR">Id da chave primária</param>
        /// <returns>Entidade TBS383_INFOS_GERAIS_PACIENTE</returns>
        public static List<TBS383_INFOS_GERAIS_PACIENTE> RetornaPeloCoAu(int CO_ALU)
        {
            return (from tbs383 in RetornaTodosRegistros()
                    where tbs383.CO_ALU == CO_ALU
                    select tbs383).ToList();
        }

        #endregion
    }
}

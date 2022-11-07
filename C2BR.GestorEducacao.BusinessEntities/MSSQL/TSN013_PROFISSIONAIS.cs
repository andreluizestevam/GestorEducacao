//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TSN013_PROFISSIONAIS
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
        /// Exclue o registro da tabela TSN013_PROFISSIONAIS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TSN013_PROFISSIONAIS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TSN013_PROFISSIONAIS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TSN013_PROFISSIONAIS.</returns>
        public static TSN013_PROFISSIONAIS Delete(TSN013_PROFISSIONAIS entity)
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
        public static int SaveOrUpdate(TSN013_PROFISSIONAIS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TSN005_USUARIO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TSN013_PROFISSIONAIS.</returns>
        public static TSN013_PROFISSIONAIS SaveOrUpdate(TSN013_PROFISSIONAIS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TSN013_PROFISSIONAIS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TSN013_PROFISSIONAIS.</returns>
        public static TSN013_PROFISSIONAIS GetByEntityKey(EntityKey entityKey)
        {
            return (TSN013_PROFISSIONAIS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TSN013_PROFISSIONAIS, ordenados pelo Id "CO_MEDICAMENTOS".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TSN013_PROFISSIONAIS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TSN013_PROFISSIONAIS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TSN013_PROFISSIONAIS.OrderBy(u => u.CO_PROFISSIONAIS).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TSN013_PROFISSIONAIS pela chave primária "NM_PROFISSIONAL".
        /// </summary>
        /// <param name="NM_USUARIO">Id da chave primária</param>
        /// <returns>Entidade TSN013_PROFISSIONAIS</returns>
        public static TSN013_PROFISSIONAIS RetornaPelaChavePrimaria(string NM_PROFISSIONAL)
        {
            return (from TSN013 in RetornaTodosRegistros()
                    where TSN013.NM_PROFISSIONAL == NM_PROFISSIONAL
                    select TSN013).FirstOrDefault();
        }

        #endregion
    }
}

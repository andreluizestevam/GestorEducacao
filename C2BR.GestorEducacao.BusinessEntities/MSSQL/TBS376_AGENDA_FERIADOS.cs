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
    public partial class TBS376_AGENDA_FERIADOS
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
        /// Exclue o registro da tabela TBS376_AGENDA_FERIADOS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS376_AGENDA_FERIADOS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS376_AGENDA_FERIADOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS376_AGENDA_FERIADOS.</returns>
        public static TBS376_AGENDA_FERIADOS Delete(TBS376_AGENDA_FERIADOS entity)
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
        public static int SaveOrUpdate(TBS376_AGENDA_FERIADOS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS347_CENTR_REGUL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS376_AGENDA_FERIADOS.</returns>
        public static TBS376_AGENDA_FERIADOS SaveOrUpdate(TBS376_AGENDA_FERIADOS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS376_AGENDA_FERIADOS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS376_AGENDA_FERIADOS.</returns>
        public static TBS376_AGENDA_FERIADOS GetByEntityKey(EntityKey entityKey)
        {
            return (TBS376_AGENDA_FERIADOS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS377_INDIC_PACIENTES, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS334_DIAGN_ATEND_MEDIC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS376_AGENDA_FERIADOS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS376_AGENDA_FERIADOS.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS341_CAMP_ATEND pela chave primária "ID_CENTR_REGUL".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS376_AGENDA_FERIADOS</returns>
        public static TBS376_AGENDA_FERIADOS RetornaPelaChavePrimaria(int ID_AGENDA_FERIADOS)
        {
            return (from tbs376 in RetornaTodosRegistros()
                    where tbs376.ID_AGENDA_FERIADOS == ID_AGENDA_FERIADOS
                    select tbs376).FirstOrDefault();
        }

        #endregion
    }
}

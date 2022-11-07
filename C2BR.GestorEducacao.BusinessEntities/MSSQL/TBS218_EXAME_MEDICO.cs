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
    public partial class TBS218_EXAME_MEDICO
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
        /// Exclue o registro da tabela TBS218_EXAME_MEDICO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS218_EXAME_MEDICO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS218_EXAME_MEDICO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS219_ATEND_MEDIC.</returns>
        public static TBS218_EXAME_MEDICO Delete(TBS218_EXAME_MEDICO entity)
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
        public static int SaveOrUpdate(TBS218_EXAME_MEDICO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS219_ATEND_MEDIC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS218_EXAME_MEDICO.</returns>
        public static TBS218_EXAME_MEDICO SaveOrUpdate(TBS218_EXAME_MEDICO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS219_ATEND_MEDIC de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS219_ATEND_MEDIC.</returns>
        public static TBS218_EXAME_MEDICO GetByEntityKey(EntityKey entityKey)
        {
            return (TBS218_EXAME_MEDICO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS219_ATEND_MEDIC, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS219_ATEND_MEDIC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS218_EXAME_MEDICO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS218_EXAME_MEDICO.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS218_EXAME_MEDICO pela chave primária "ID_EXAME".
        /// </summary>
        /// <param name="ID_EXAME">Id da chave primária</param>
        /// <returns>Entidade TBS218_EXAME_MEDICO</returns>
        public static TBS218_EXAME_MEDICO RetornaPelaChavePrimaria(int ID_EXAME)
        {
            return (from tbs218 in RetornaTodosRegistros()
                    where tbs218.ID_EXAME == ID_EXAME
                    select tbs218).FirstOrDefault();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS218_EXAME_MEDICO pela chave primária "ID_ATEND_MEDIC".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id do Atendimento</param>
        /// <returns>Lista</returns>
        public static List<TBS218_EXAME_MEDICO> RetornaPeloIDAtendimento(int ID_ATEND_MEDIC)
        {
            return (from tbs218 in RetornaTodosRegistros()
                    where tbs218.ID_ATEND_MEDIC == ID_ATEND_MEDIC
                    select tbs218).ToList();
        }

        #endregion
    }
}

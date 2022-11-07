//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;
using System.Linq.Expressions;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TBS356_PROC_MEDIC_PROCE
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
        /// Exclue o registro da tabela TBS356_PROC_MEDIC_PROCE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS356_PROC_MEDIC_PROCE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS356_PROC_MEDIC_PROCE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS356_PROC_MEDIC_PROCE.</returns>
        public static TBS356_PROC_MEDIC_PROCE Delete(TBS356_PROC_MEDIC_PROCE entity)
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
        public static int SaveOrUpdate(TBS356_PROC_MEDIC_PROCE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS356_PROC_MEDIC_PROCE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS356_PROC_MEDIC_PROCE.</returns>
        public static TBS356_PROC_MEDIC_PROCE SaveOrUpdate(TBS356_PROC_MEDIC_PROCE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS356_PROC_MEDIC_PROCE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS341_CAMP_ATEND.</returns>
        public static TBS356_PROC_MEDIC_PROCE GetByEntityKey(EntityKey entityKey)
        {
            return (TBS356_PROC_MEDIC_PROCE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS356_PROC_MEDIC_PROCE, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS334_DIAGN_ATEND_MEDIC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS356_PROC_MEDIC_PROCE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS356_PROC_MEDIC_PROCE.AsObjectQuery();
        }

        public static IQueryable<TBS356_PROC_MEDIC_PROCE> RetornarRegistros(Expression<Func<TBS356_PROC_MEDIC_PROCE, bool>> predicate) {
            return GestorEntities.CurrentContext.TBS356_PROC_MEDIC_PROCE.Where(predicate);
        }

        /// <summary>
        /// Retorna um registro da entidade TBS341_CAMP_ATEND pela chave primária "ID_PROC_MEDI_PROCE".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS356_PROC_MEDIC_PROCE</returns>
        public static TBS356_PROC_MEDIC_PROCE RetornaPelaChavePrimaria(int ID_PROC_MEDI_PROCE)
        {
            return GestorEntities.CurrentContext.TBS356_PROC_MEDIC_PROCE.FirstOrDefault(p => p.ID_PROC_MEDI_PROCE == ID_PROC_MEDI_PROCE);
        }

        #endregion
    }
}

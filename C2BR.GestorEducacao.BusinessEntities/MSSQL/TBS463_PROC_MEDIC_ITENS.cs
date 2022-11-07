using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;
using System.Linq.Expressions;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TBS463_PROC_MEDIC_ITENS
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
        /// Exclue o registro da tabela TBS463_PROC_MEDIC_ITENS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS463_PROC_MEDIC_ITENS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS463_PROC_MEDIC_ITENS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS463_PROC_MEDIC_ITENS.</returns>
        public static TBS463_PROC_MEDIC_ITENS Delete(TBS463_PROC_MEDIC_ITENS entity)
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
        public static int SaveOrUpdate(TBS463_PROC_MEDIC_ITENS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS463_PROC_MEDIC_ITENS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS463_PROC_MEDIC_ITENS.</returns>
        public static TBS463_PROC_MEDIC_ITENS SaveOrUpdate(TBS463_PROC_MEDIC_ITENS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS463_PROC_MEDIC_ITENS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS463_PROC_MEDIC_ITENS.</returns>
        public static TBS463_PROC_MEDIC_ITENS GetByEntityKey(EntityKey entityKey)
        {
            return (TBS463_PROC_MEDIC_ITENS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS463_PROC_MEDIC_ITENS, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS394_RESUM_FINAN de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS463_PROC_MEDIC_ITENS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS463_PROC_MEDIC_ITENS.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS463_PROC_MEDIC_ITENS pela chave primária "ID_PROC_MEDI_ITENS".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS463_PROC_MEDIC_ITENS</returns>
        public static TBS463_PROC_MEDIC_ITENS RetornaPelaChavePrimaria(int ID_PROC_MEDI_ITENS)
        {
            return GestorEntities.CurrentContext.TBS463_PROC_MEDIC_ITENS.FirstOrDefault(p => p.ID_PROC_MEDI_ITENS == ID_PROC_MEDI_ITENS);
        }

        /// <summary>
        /// Obtem registros segundo a condição passada como parametro
        /// </summary>
        public static IQueryable<TBS463_PROC_MEDIC_ITENS> RetornarRegistros(Expression<Func<TBS463_PROC_MEDIC_ITENS, bool>> predicate)
        {
            return GestorEntities.CurrentContext.TBS463_PROC_MEDIC_ITENS.Where(predicate);
        }


        #endregion
    }
}

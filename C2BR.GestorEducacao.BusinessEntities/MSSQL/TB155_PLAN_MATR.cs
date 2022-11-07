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
    public partial class tb155_plan_matr
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
        /// Exclue o registro da tabela tb155_plan_matr do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(tb155_plan_matr entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela tb155_plan_matr na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade tb155_plan_matr.</returns>
        public static tb155_plan_matr Delete(tb155_plan_matr entity)
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
        public static int SaveOrUpdate(tb155_plan_matr entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela tb155_plan_matr na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade tb155_plan_matr.</returns>
        public static tb155_plan_matr SaveOrUpdate(tb155_plan_matr entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade tb155_plan_matr de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade tb155_plan_matr.</returns>
        public static tb155_plan_matr GetByEntityKey(EntityKey entityKey)
        {
            return (tb155_plan_matr)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade tb155_plan_matr, ordenados pelo ano de referência "co_ano_ref".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade tb155_plan_matr de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<tb155_plan_matr> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.tb155_plan_matr.OrderBy( p => p.co_ano_ref).ThenBy( p => p.co_cur ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade tb155_plan_matr pelas chaves primárias "co_cur" e "co_ano_ref".
        /// </summary>
        /// <param name="co_cur">Id da série</param>
        /// <param name="co_ano_ref">Ano de referência</param>
        /// <returns>Entidade tb155_plan_matr</returns>
        public static tb155_plan_matr RetornaPelaChavePrimaria(int co_cur, string co_ano_ref)
        {
            return (from tb155 in RetornaTodosRegistros()
                    where tb155.co_cur == co_cur && tb155.co_ano_ref == co_ano_ref
                    select tb155).FirstOrDefault();
        }

        #endregion
    }
}
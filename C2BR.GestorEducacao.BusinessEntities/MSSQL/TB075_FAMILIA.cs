using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TB075_FAMILIA
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
        /// Exclue o registro da tabela TB075_FAMILIA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB075_FAMILIA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB075_FAMILIA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB075_FAMILIA.</returns>
        public static TB075_FAMILIA Delete(TB075_FAMILIA entity)
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
        public static int SaveOrUpdate(TB075_FAMILIA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB075_FAMILIA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB075_FAMILIA.</returns>
        public static TB075_FAMILIA SaveOrUpdate(TB075_FAMILIA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB075_FAMILIA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB075_FAMILIA.</returns>
        public static TB075_FAMILIA GetByEntityKey(EntityKey entityKey)
        {
            return (TB075_FAMILIA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB075_FAMILIA, ordenados pelo nome "CO_FAMILIA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB07_ALUNO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB075_FAMILIA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB075_FAMILIA.OrderBy(a => a.CO_FAMILIA).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB075_FAMILIA pelas chaves primárias "CO_FAMILIA"
        /// </summary>
        /// <param name="CO_ALU">Id do aluno</param>
        /// <param name="CO_EMP">Id da Unidade</param>
        /// <returns>Entidade TB07_ALUNO</returns>
        public static TB075_FAMILIA RetornaPeloCoFamilia(string CO_FAMILIA)
        {
            return (from tb075 in RetornaTodosRegistros()
                    where tb075.CO_FAMILIA == CO_FAMILIA
                    select tb075).FirstOrDefault();
        }

        /// <summary>
        /// Retorna um registro da entidade TB075_FAMILIA pela chave primária "ID_FAMILIA".
        /// </summary>
        /// <param name="ID_FAMILIA">Id da chave primária</param>
        /// <returns>Entidade TB075_FAMILIA</returns>
        public static TB075_FAMILIA RetornaPelaChavePrimaria(decimal ID_FAMILIA)
        {
            return (from tbg75 in RetornaTodosRegistros()
                    where tbg75.ID_FAMILIA == ID_FAMILIA
                    select tbg75).FirstOrDefault();
        }      
        #endregion
    }
}

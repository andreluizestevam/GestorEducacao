using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
     public partial class TB061_GRUPO_SOLIC
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
        /// Exclue o registro da tabela TB061_GRUPO_SOLIC do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB061_GRUPO_SOLIC entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB061_GRUPO_SOLIC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB061_GRUPO_SOLIC.</returns>
        public static TB061_GRUPO_SOLIC Delete(TB061_GRUPO_SOLIC entity)
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
        public static int SaveOrUpdate(TB061_GRUPO_SOLIC entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB061_GRUPO_SOLIC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB061_GRUPO_SOLIC.</returns>
        public static TB061_GRUPO_SOLIC SaveOrUpdate(TB061_GRUPO_SOLIC entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB07_ALUNO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB07_ALUNO.</returns>
        public static TB061_GRUPO_SOLIC GetByEntityKey(EntityKey entityKey)
        {
            return (TB061_GRUPO_SOLIC)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB061_GRUPO_SOLIC, ordenados pelo nome "NM_DOCUM".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB061_GRUPO_SOLIC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB061_GRUPO_SOLIC> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB061_GRUPO_SOLIC.OrderBy(x => x.NM_GRUPO_SOLIC).AsObjectQuery();
        }

        #endregion


        /// <summary>
        /// Retorna um registro da entidade TB061_GRUPO_SOLIC pelas chaves primárias "ID".
        /// </summary>
        /// <param name="ID">Id do Documento</param>       
        /// <returns>Entidade TB009_DOCTOS_RTF</returns>
        public static TB061_GRUPO_SOLIC RetornaPelaChavePrimaria(int ID)
        {
            return (from tb061 in TB061_GRUPO_SOLIC.RetornaTodosRegistros()
                    where tb061.ID_GRUPO_SOLIC == ID
                    select tb061).FirstOrDefault();
        }
         

    }
}

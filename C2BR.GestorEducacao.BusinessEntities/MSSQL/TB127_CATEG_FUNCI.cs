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
    public partial class TB127_CATEG_FUNCI
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
        /// Exclue o registro da tabela TB127_CATEG_FUNCI do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB127_CATEG_FUNCI entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB127_CATEG_FUNCI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB127_CATEG_FUNCI.</returns>
        public static TB127_CATEG_FUNCI Delete(TB127_CATEG_FUNCI entity)
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
        public static int SaveOrUpdate(TB127_CATEG_FUNCI entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB127_CATEG_FUNCI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB127_CATEG_FUNCI.</returns>
        public static TB127_CATEG_FUNCI SaveOrUpdate(TB127_CATEG_FUNCI entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna um registro da entidade TB129_CADTURMAS pela chave primária "CO_TUR".
        /// </summary>
        /// <param name="CO_TUR">Id da chave primária</param>
        /// <returns>Entidade TB129_CADTURMAS</returns>
        public static TB127_CATEG_FUNCI RetornaPelaChavePrimaria(int ID_CATEG_FUNCI)
        {
            return (from tb128 in RetornaTodosRegistros()
                    where tb128.ID_CATEG_FUNCI == ID_CATEG_FUNCI
                    select tb128).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o registro da entidade TB127_CATEG_FUNCI de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB127_CATEG_FUNCI.</returns>
        public static TB127_CATEG_FUNCI GetByEntityKey(EntityKey entityKey)
        {
            return (TB127_CATEG_FUNCI)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB127_CATEG_FUNCI, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB127_CATEG_FUNCI de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB127_CATEG_FUNCI> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB127_CATEG_FUNCI.OrderBy(e => e.NO_CATEG_FUNCI).AsObjectQuery();
        }

        #endregion
    }
}

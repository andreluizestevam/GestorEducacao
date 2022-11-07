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
    public partial class TB128_FUNCA_FUNCI
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
        /// Exclue o registro da tabela TB128_FUNCA_FUNCI do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB128_FUNCA_FUNCI entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB128_FUNCA_FUNCI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB128_FUNCA_FUNCI.</returns>
        public static TB128_FUNCA_FUNCI Delete(TB128_FUNCA_FUNCI entity)
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
        public static int SaveOrUpdate(TB128_FUNCA_FUNCI entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB128_FUNCA_FUNCI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB128_FUNCA_FUNCI.</returns>
        public static TB128_FUNCA_FUNCI SaveOrUpdate(TB128_FUNCA_FUNCI entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB128_FUNCA_FUNCI de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB128_FUNCA_FUNCI.</returns>
        public static TB128_FUNCA_FUNCI GetByEntityKey(EntityKey entityKey)
        {
            return (TB128_FUNCA_FUNCI)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna um registro da entidade TB129_CADTURMAS pela chave primária "CO_TUR".
        /// </summary>
        /// <param name="CO_TUR">Id da chave primária</param>
        /// <returns>Entidade TB129_CADTURMAS</returns>
        public static TB128_FUNCA_FUNCI RetornaPelaChavePrimaria(int ID_FUNCA_FUNCI)
        {
            return (from tb128 in RetornaTodosRegistros()
                    where tb128.ID_FUNCA_FUNCI == ID_FUNCA_FUNCI
                    select tb128).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB128_FUNCA_FUNCI, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB128_FUNCA_FUNCI de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB128_FUNCA_FUNCI> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB128_FUNCA_FUNCI.OrderBy(e => e.NO_FUNCA_FUNCI).AsObjectQuery();
        }

        #endregion
    }
}

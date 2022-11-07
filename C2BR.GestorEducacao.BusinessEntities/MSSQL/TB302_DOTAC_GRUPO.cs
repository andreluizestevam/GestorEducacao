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
    public partial class TB302_DOTAC_GRUPO
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
        /// Exclue o registro da tabela TB302_DOTAC_GRUPO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB302_DOTAC_GRUPO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB302_DOTAC_GRUPO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB302_DOTAC_GRUPO.</returns>
        public static TB302_DOTAC_GRUPO Delete(TB302_DOTAC_GRUPO entity)
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
        public static int SaveOrUpdate(TB302_DOTAC_GRUPO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB302_DOTAC_GRUPO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB302_DOTAC_GRUPO.</returns>
        public static TB302_DOTAC_GRUPO SaveOrUpdate(TB302_DOTAC_GRUPO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB302_DOTAC_GRUPO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB302_DOTAC_GRUPO.</returns>
        public static TB302_DOTAC_GRUPO GetByEntityKey(EntityKey entityKey)
        {
            return (TB302_DOTAC_GRUPO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB302_DOTAC_GRUPO, ordenados pela descrição "DE_DOTAC_GRUPO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB302_DOTAC_GRUPO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB302_DOTAC_GRUPO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB302_DOTAC_GRUPO.OrderBy(g => g.DE_DOTAC_GRUPO).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB302_DOTAC_GRUPO pela chave primária "ID_DOTAC_GRUPO".
        /// </summary>
        /// <param name="ID_DOTAC_GRUPO">Id da chave primária</param>
        /// <returns>Entidade TB302_DOTAC_GRUPO</returns>
        public static TB302_DOTAC_GRUPO RetornaPelaChavePrimaria(int ID_DOTAC_GRUPO)
        {
            return (from tb302 in RetornaTodosRegistros()
                    where tb302.ID_DOTAC_GRUPO == ID_DOTAC_GRUPO
                    select tb302).FirstOrDefault();
        }

        #endregion
    }
}
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
    public partial class TB316_CBO_GRUPO
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
        /// Exclue o registro da tabela TB316_CBO_GRUPO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB316_CBO_GRUPO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB316_CBO_GRUPO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB316_CBO_GRUPO.</returns>
        public static TB316_CBO_GRUPO Delete(TB316_CBO_GRUPO entity)
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
        public static int SaveOrUpdate(TB316_CBO_GRUPO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB316_CBO_GRUPO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB316_CBO_GRUPO.</returns>
        public static TB316_CBO_GRUPO SaveOrUpdate(TB316_CBO_GRUPO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB316_CBO_GRUPO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB316_CBO_GRUPO.</returns>
        public static TB316_CBO_GRUPO GetByEntityKey(EntityKey entityKey)
        {
            return (TB316_CBO_GRUPO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB316_CBO_GRUPO, ordenados pela descrição "DE_CBO_GRUPO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB316_CBO_GRUPO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB316_CBO_GRUPO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB316_CBO_GRUPO.OrderBy(t => t.DE_CBO_GRUPO).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB316_CBO_GRUPO pela chave primária "CO_CBO_GRUPO".
        /// </summary>
        /// <param name="CO_CBO_GRUPO">Id da chave primária</param>
        /// <returns>Entidade TB316_CBO_GRUPO</returns>
        public static TB316_CBO_GRUPO RetornaPelaChavePrimaria(string _CO_CBO_GRUPO)
        {
            return (from tb316 in RetornaTodosRegistros()
                    where tb316.CO_CBO_GRUPO == _CO_CBO_GRUPO
                    select tb316).FirstOrDefault();
        }

        #endregion
    }
}
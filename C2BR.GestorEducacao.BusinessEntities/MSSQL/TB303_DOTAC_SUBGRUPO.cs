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
    public partial class TB303_DOTAC_SUBGRUPO
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
        /// Exclue o registro da tabela TB303_DOTAC_SUBGRUPO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB303_DOTAC_SUBGRUPO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB303_DOTAC_SUBGRUPO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB303_DOTAC_SUBGRUPO.</returns>
        public static TB303_DOTAC_SUBGRUPO Delete(TB303_DOTAC_SUBGRUPO entity)
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
        public static int SaveOrUpdate(TB303_DOTAC_SUBGRUPO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB303_DOTAC_SUBGRUPO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB303_DOTAC_SUBGRUPO.</returns>
        public static TB303_DOTAC_SUBGRUPO SaveOrUpdate(TB303_DOTAC_SUBGRUPO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB303_DOTAC_SUBGRUPO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB303_DOTAC_SUBGRUPO.</returns>
        public static TB303_DOTAC_SUBGRUPO GetByEntityKey(EntityKey entityKey)
        {
            return (TB303_DOTAC_SUBGRUPO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB303_DOTAC_SUBGRUPO, ordenados pela descrição "DE_DOTAC_SUBGRUPO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB303_DOTAC_SUBGRUPO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB303_DOTAC_SUBGRUPO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB303_DOTAC_SUBGRUPO.OrderBy(g => g.DE_DOTAC_SUBGRUPO).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB303_DOTAC_SUBGRUPO pela chave primária "ID_DOTAC_SUBGRUPO".
        /// </summary>
        /// <param name="ID_DOTAC_SUBGRUPO">Id da chave primária</param>
        /// <returns>Entidade TB303_DOTAC_SUBGRUPO</returns>
        public static TB303_DOTAC_SUBGRUPO RetornaPelaChavePrimaria(int ID_DOTAC_SUBGRUPO)
        {
            return (from tb303 in RetornaTodosRegistros()
                    where tb303.ID_DOTAC_SUBGRUPO == ID_DOTAC_SUBGRUPO
                    select tb303).FirstOrDefault();
        }

        #endregion
    }
}
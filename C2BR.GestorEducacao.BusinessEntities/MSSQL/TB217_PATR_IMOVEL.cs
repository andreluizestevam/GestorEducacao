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
    public partial class TB217_PATR_IMOVEL
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
        /// Exclue o registro da tabela TB217_PATR_IMOVEL do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB217_PATR_IMOVEL entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB217_PATR_IMOVEL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB217_PATR_IMOVEL.</returns>
        public static TB217_PATR_IMOVEL Delete(TB217_PATR_IMOVEL entity)
        {
            Delete(entity, true);

            return entity;
        }

        /// <summary>
        /// Faz a verificação para saber se inserção ou alteração e executa a ação necessária; você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int SaveOrUpdate(TB217_PATR_IMOVEL entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB217_PATR_IMOVEL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB217_PATR_IMOVEL.</returns>
        public static TB217_PATR_IMOVEL SaveOrUpdate(TB217_PATR_IMOVEL entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB217_PATR_IMOVEL de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB217_PATR_IMOVEL.</returns>
        public static TB217_PATR_IMOVEL GetByEntityKey(EntityKey entityKey)
        {
            return (TB217_PATR_IMOVEL)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB217_PATR_IMOVEL, ordenados pelo código "COD_PATR_IMOVEL".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB217_PATR_IMOVEL de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB217_PATR_IMOVEL> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB217_PATR_IMOVEL.OrderBy( p => p.COD_PATR_IMOVEL ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB217_PATR_IMOVEL pela chave primária "COD_PATR_IMOVEL".
        /// </summary>
        /// <param name="COD_PATR_IMOVEL">Id da chave primária</param>
        /// <returns>Entidade TB217_PATR_IMOVEL</returns>
        public static TB217_PATR_IMOVEL RetornaPelaChavePrimaria(decimal COD_PATR_IMOVEL)
        {
            return (from tb217 in RetornaTodosRegistros()
                    where tb217.COD_PATR_IMOVEL == COD_PATR_IMOVEL
                    select tb217).FirstOrDefault();
        }
        #endregion
    }
}

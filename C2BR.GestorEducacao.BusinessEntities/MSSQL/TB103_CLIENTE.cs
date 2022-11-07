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
    public partial class TB103_CLIENTE
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
        /// Exclue o registro da tabela TB103_CLIENTE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB103_CLIENTE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB103_CLIENTE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB103_CLIENTE.</returns>
        public static TB103_CLIENTE Delete(TB103_CLIENTE entity)
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
        public static int SaveOrUpdate(TB103_CLIENTE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB103_CLIENTE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB103_CLIENTE.</returns>
        public static TB103_CLIENTE SaveOrUpdate(TB103_CLIENTE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB103_CLIENTE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB103_CLIENTE.</returns>
        public static TB103_CLIENTE GetByEntityKey(EntityKey entityKey)
        {
            return (TB103_CLIENTE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB103_CLIENTE, ordenados pelo nome da razão social "DE_RAZSOC_CLI"
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB103_CLIENTE de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB103_CLIENTE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB103_CLIENTE.OrderBy( c => c.DE_RAZSOC_CLI ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB103_CLIENTE pela chaves primária "CO_CLIENTE".
        /// </summary>
        /// <param name="CO_CLIENTE">Id da chave primária</param>
        /// <returns></returns>
        public static TB103_CLIENTE RetornaPelaChavePrimaria(int CO_CLIENTE)
        {
            return (from tb103 in RetornaTodosRegistros()
                    where tb103.CO_CLIENTE == CO_CLIENTE
                    select tb103).FirstOrDefault();
        }

        #endregion
    }
}

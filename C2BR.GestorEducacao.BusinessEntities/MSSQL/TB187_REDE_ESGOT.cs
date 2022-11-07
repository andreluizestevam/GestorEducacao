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
    public partial class TB187_REDE_ESGOT
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
        /// Exclue o registro da tabela TB187_REDE_ESGOT do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB187_REDE_ESGOT entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB187_REDE_ESGOT na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB187_REDE_ESGOT.</returns>
        public static TB187_REDE_ESGOT Delete(TB187_REDE_ESGOT entity)
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
        public static int SaveOrUpdate(TB187_REDE_ESGOT entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB187_REDE_ESGOT na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB187_REDE_ESGOT.</returns>
        public static TB187_REDE_ESGOT SaveOrUpdate(TB187_REDE_ESGOT entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB187_REDE_ESGOT de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB187_REDE_ESGOT.</returns>
        public static TB187_REDE_ESGOT GetByEntityKey(EntityKey entityKey)
        {
            return (TB187_REDE_ESGOT)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB187_REDE_ESGOT, ordenados pela descrição "DE_REDE_ESGOT".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB187_REDE_ESGOT de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB187_REDE_ESGOT> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB187_REDE_ESGOT.OrderBy( r => r.DE_REDE_ESGOT ).AsObjectQuery();
        }
        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB187_REDE_ESGOT pela chave primária "CO_SIGLA_REDE_ESGOT".
        /// </summary>
        /// <param name="CO_SIGLA_REDE_ESGOT">Id da chave primária</param>
        /// <returns>Entidade TB187_REDE_ESGOT</returns>
        public static TB187_REDE_ESGOT RetornaPelaChavePrimaria(string CO_SIGLA_REDE_ESGOT)
        {
            return (from tb187 in RetornaTodosRegistros()
                    where tb187.CO_SIGLA_REDE_ESGOT == CO_SIGLA_REDE_ESGOT
                    select tb187).FirstOrDefault();
        }
        #endregion
    }
}
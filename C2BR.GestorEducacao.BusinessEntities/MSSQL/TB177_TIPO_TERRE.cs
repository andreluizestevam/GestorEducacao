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
    public partial class TB177_TIPO_TERRE
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
        /// Exclue o registro da tabela TB177_TIPO_TERRE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB177_TIPO_TERRE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB177_TIPO_TERRE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB177_TIPO_TERRE.</returns>
        public static TB177_TIPO_TERRE Delete(TB177_TIPO_TERRE entity)
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
        public static int SaveOrUpdate(TB177_TIPO_TERRE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB177_TIPO_TERRE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB177_TIPO_TERRE.</returns>
        public static TB177_TIPO_TERRE SaveOrUpdate(TB177_TIPO_TERRE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB177_TIPO_TERRE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB177_TIPO_TERRE.</returns>
        public static TB177_TIPO_TERRE GetByEntityKey(EntityKey entityKey)
        {
            return (TB177_TIPO_TERRE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB177_TIPO_TERRE, ordenados pela descrição "DE_TIPO_TERRE".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB177_TIPO_TERRE de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB177_TIPO_TERRE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB177_TIPO_TERRE.OrderBy( t => t.DE_TIPO_TERRE ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB177_TIPO_TERRE pela chave primária "CO_SIGLA_TIPO_TERRE".
        /// </summary>
        /// <param name="CO_SIGLA_TIPO_TERRE">Id da chave primária</param>
        /// <returns>Entidade TB177_TIPO_TERRE</returns>
        public static TB177_TIPO_TERRE RetornaPelaChavePrimaria(string CO_SIGLA_TIPO_TERRE)
        {
            return (from tb177 in RetornaTodosRegistros()
                    where tb177.CO_SIGLA_TIPO_TERRE == CO_SIGLA_TIPO_TERRE
                    select tb177).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TB177_TIPO_TERRE pelo código do tipo de terreno "CO_TIPO_TERRE".
        /// </summary>
        /// <param name="CO_TIPO_TERRE">Id do tipo de terreno</param>
        /// <returns>Entidade TB177_TIPO_TERRE</returns>
        public static TB177_TIPO_TERRE RetornaPeloCoTipoTerre(int CO_TIPO_TERRE)
        {
            return (from tb177 in RetornaTodosRegistros()
                    where tb177.CO_TIPO_TERRE == CO_TIPO_TERRE
                    select tb177).FirstOrDefault();
        }
        #endregion
    }
}
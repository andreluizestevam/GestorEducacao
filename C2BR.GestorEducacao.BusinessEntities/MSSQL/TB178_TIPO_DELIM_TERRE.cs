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
    public partial class TB178_TIPO_DELIM_TERRE
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
        /// Exclue o registro da tabela TB178_TIPO_DELIM_TERRE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB178_TIPO_DELIM_TERRE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB178_TIPO_DELIM_TERRE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB178_TIPO_DELIM_TERRE.</returns>
        public static TB178_TIPO_DELIM_TERRE Delete(TB178_TIPO_DELIM_TERRE entity)
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
        public static int SaveOrUpdate(TB178_TIPO_DELIM_TERRE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB178_TIPO_DELIM_TERRE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB178_TIPO_DELIM_TERRE.</returns>
        public static TB178_TIPO_DELIM_TERRE SaveOrUpdate(TB178_TIPO_DELIM_TERRE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB178_TIPO_DELIM_TERRE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB178_TIPO_DELIM_TERRE.</returns>
        public static TB178_TIPO_DELIM_TERRE GetByEntityKey(EntityKey entityKey)
        {
            return (TB178_TIPO_DELIM_TERRE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB178_TIPO_DELIM_TERRE, ordenados pela descrição "DE_TIPO_DELIM_TERRE".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB178_TIPO_DELIM_TERRE de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB178_TIPO_DELIM_TERRE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB178_TIPO_DELIM_TERRE.OrderBy( t => t.DE_TIPO_DELIM_TERRE ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB178_TIPO_DELIM_TERRE pela chave primária "CO_SIGLA_TIPO_DELIM_TERRE".
        /// </summary>
        /// <param name="CO_SIGLA_TIPO_DELIM_TERRE">Id da chave primária</param>
        /// <returns>Entidade TB178_TIPO_DELIM_TERRE</returns>
        public static TB178_TIPO_DELIM_TERRE RetornaPelaChavePrimaria(string CO_SIGLA_TIPO_DELIM_TERRE)
        {
            return (from tb178 in RetornaTodosRegistros()
                    where tb178.CO_SIGLA_TIPO_DELIM_TERRE == CO_SIGLA_TIPO_DELIM_TERRE
                    select tb178).FirstOrDefault();
        }


        /// <summary>
        /// Retorna o primeiro registro da entidade TB178_TIPO_DELIM_TERRE pelo código do tipo de delimitação do terreno "CO_TIPO_DELIM_TERRE".
        /// </summary>
        /// <param name="CO_TIPO_DELIM_TERRE">Id do tipo de delimitação do terreno</param>
        /// <returns>Entidade TB178_TIPO_DELIM_TERRE</returns>
        public static TB178_TIPO_DELIM_TERRE RetornaPeloCoTipoDelimTerre(int CO_TIPO_DELIM_TERRE)
        {
            return (from tb178 in RetornaTodosRegistros()
                    where tb178.CO_TIPO_DELIM_TERRE == CO_TIPO_DELIM_TERRE
                    select tb178).FirstOrDefault();
        }
        #endregion
    }
}
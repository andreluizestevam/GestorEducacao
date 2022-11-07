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
    public partial class TB181_TIPO_COBER
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
        /// Exclue o registro da tabela TB181_TIPO_COBER do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB181_TIPO_COBER entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB181_TIPO_COBER na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB181_TIPO_COBER.</returns>
        public static TB181_TIPO_COBER Delete(TB181_TIPO_COBER entity)
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
        public static int SaveOrUpdate(TB181_TIPO_COBER entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB181_TIPO_COBER na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB181_TIPO_COBER.</returns>
        public static TB181_TIPO_COBER SaveOrUpdate(TB181_TIPO_COBER entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB181_TIPO_COBER de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB181_TIPO_COBER.</returns>
        public static TB181_TIPO_COBER GetByEntityKey(EntityKey entityKey)
        {
            return (TB181_TIPO_COBER)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB181_TIPO_COBER, ordenados pela descrição "DE_TIPO_COBER".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB181_TIPO_COBER de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB181_TIPO_COBER> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB181_TIPO_COBER.OrderBy( t => t.DE_TIPO_COBER ).AsObjectQuery();
        }
        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB181_TIPO_COBER pela chave primária "CO_SIGLA_TIPO_COBER".
        /// </summary>
        /// <param name="CO_SIGLA_TIPO_COBER">Id da chave primária</param>
        /// <returns>Entidade TB181_TIPO_COBER</returns>
        public static TB181_TIPO_COBER RetornaPelaChavePrimaria(string CO_SIGLA_TIPO_COBER)
        {
            return (from tb181 in RetornaTodosRegistros()
                    where tb181.CO_SIGLA_TIPO_COBER == CO_SIGLA_TIPO_COBER
                    select tb181).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TB181_TIPO_COBER pelo tipo de cobertura "CO_TIPO_COBER".
        /// </summary>
        /// <param name="CO_TIPO_COBER">Id do tipo de cobertura</param>
        /// <returns>Entidade TB181_TIPO_COBER</returns>
        public static TB181_TIPO_COBER RetornaPeloCoTipoCober(int CO_TIPO_COBER)
        {
            return (from tb181 in RetornaTodosRegistros()
                    where tb181.CO_TIPO_COBER == CO_TIPO_COBER
                    select tb181).FirstOrDefault();
        }

        
        #endregion
    }
}
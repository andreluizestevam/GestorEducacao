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
    public partial class TB179_TIPO_EDIFI
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
        /// Exclue o registro da tabela TB179_TIPO_EDIFI do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB179_TIPO_EDIFI entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB179_TIPO_EDIFI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB179_TIPO_EDIFI.</returns>
        public static TB179_TIPO_EDIFI Delete(TB179_TIPO_EDIFI entity)
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
        public static int SaveOrUpdate(TB179_TIPO_EDIFI entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB179_TIPO_EDIFI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB179_TIPO_EDIFI.</returns>
        public static TB179_TIPO_EDIFI SaveOrUpdate(TB179_TIPO_EDIFI entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB179_TIPO_EDIFI de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB179_TIPO_EDIFI.</returns>
        public static TB179_TIPO_EDIFI GetByEntityKey(EntityKey entityKey)
        {
            return (TB179_TIPO_EDIFI)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB179_TIPO_EDIFI, ordenados pela descrição "DE_TIPO_EDIFI".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB179_TIPO_EDIFI de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB179_TIPO_EDIFI> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB179_TIPO_EDIFI.OrderBy( t => t.DE_TIPO_EDIFI ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB179_TIPO_EDIFI pela chave primária "CO_SIGLA_TIPO_EDIFI".
        /// </summary>
        /// <param name="CO_SIGLA_TIPO_EDIFI">Id da chave primária</param>
        /// <returns>Entidade TB179_TIPO_EDIFI</returns>
        public static TB179_TIPO_EDIFI RetornaPelaChavePrimaria(string CO_SIGLA_TIPO_EDIFI)
        {
            return (from tb179 in RetornaTodosRegistros()
                    where tb179.CO_SIGLA_TIPO_EDIFI == CO_SIGLA_TIPO_EDIFI
                    select tb179).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TB179_TIPO_EDIFI pelo código do tipo de edificação "CO_TIPO_EDIFI".
        /// </summary>
        /// <param name="CO_TIPO_EDIFI">Id do tipo de edificação</param>
        /// <returns>Entidade TB179_TIPO_EDIFI</returns>
        public static TB179_TIPO_EDIFI RetornaPeloCoTipoEdifi(int CO_TIPO_EDIFI)
        {
            return (from tb179 in RetornaTodosRegistros()
                    where tb179.CO_TIPO_EDIFI == CO_TIPO_EDIFI
                    select tb179).FirstOrDefault();
        }
        #endregion
    }
}
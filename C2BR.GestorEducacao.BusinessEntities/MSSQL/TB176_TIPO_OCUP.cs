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
    public partial class TB176_TIPO_OCUPA
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
        /// Exclue o registro da tabela TB176_TIPO_OCUPA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB176_TIPO_OCUPA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB176_TIPO_OCUPA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB176_TIPO_OCUPA.</returns>
        public static TB176_TIPO_OCUPA Delete(TB176_TIPO_OCUPA entity)
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
        public static int SaveOrUpdate(TB176_TIPO_OCUPA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB176_TIPO_OCUPA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB176_TIPO_OCUPA.</returns>
        public static TB176_TIPO_OCUPA SaveOrUpdate(TB176_TIPO_OCUPA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB176_TIPO_OCUPA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB176_TIPO_OCUPA.</returns>
        public static TB176_TIPO_OCUPA GetByEntityKey(EntityKey entityKey)
        {
            return (TB176_TIPO_OCUPA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB176_TIPO_OCUPA, ordenados pela descrição "DE_TIPO_OCUPA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB176_TIPO_OCUPA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB176_TIPO_OCUPA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB176_TIPO_OCUPA.OrderBy( t => t.DE_TIPO_OCUPA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB176_TIPO_OCUPA pela chave primária "CO_SIGLA_TIPO_OCUPA".
        /// </summary>
        /// <param name="CO_SIGLA_TIPO_OCUPA">Id da chave primária</param>
        /// <returns>Entidade TB176_TIPO_OCUPA</returns>
        public static TB176_TIPO_OCUPA RetornaPelaChavePrimaria(string CO_SIGLA_TIPO_OCUPA)
        {
            return (from tb176 in RetornaTodosRegistros()
                    where tb176.CO_SIGLA_TIPO_OCUPA == CO_SIGLA_TIPO_OCUPA
                    select tb176).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TB176_TIPO_OCUPA pelo código do tipo de ocupação "CO_TIPO_OCUPA".
        /// </summary>
        /// <param name="CO_TIPO_OCUPA">Id do tipo de ocupação</param>
        /// <returns>Entidade TB176_TIPO_OCUPA</returns>
        public static TB176_TIPO_OCUPA RetornaPeloCoTipoOcupa(int CO_TIPO_OCUPA)
        {
            return (from tb176 in RetornaTodosRegistros()
                    where tb176.CO_TIPO_OCUPA == CO_TIPO_OCUPA
                    select tb176).FirstOrDefault();
        }
        #endregion
    }
}
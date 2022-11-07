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
    public partial class TB135_PROG_SOCIAIS
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
        /// Exclue o registro da tabela TB135_PROG_SOCIAIS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB135_PROG_SOCIAIS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB135_PROG_SOCIAIS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB135_PROG_SOCIAIS.</returns>
        public static TB135_PROG_SOCIAIS Delete(TB135_PROG_SOCIAIS entity)
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
        public static int SaveOrUpdate(TB135_PROG_SOCIAIS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB135_PROG_SOCIAIS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB135_PROG_SOCIAIS.</returns>
        public static TB135_PROG_SOCIAIS SaveOrUpdate(TB135_PROG_SOCIAIS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB135_PROG_SOCIAIS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB135_PROG_SOCIAIS.</returns>
        public static TB135_PROG_SOCIAIS GetByEntityKey(EntityKey entityKey)
        {
            return (TB135_PROG_SOCIAIS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB135_PROG_SOCIAIS, ordenados pelo nome "NO_PROGR_SOCIA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB135_PROG_SOCIAIS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB135_PROG_SOCIAIS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB135_PROG_SOCIAIS.OrderBy( p => p.NO_PROGR_SOCIA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna todos os registros da entidade TB135_PROG_SOCIAIS de acordo com a instituição "ORG_CODIGO_ORGAO".
        /// </summary>
        /// <param name="ORG_CODIGO_ORGAO">Id da instituição</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB135_PROG_SOCIAIS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB135_PROG_SOCIAIS> RetornaPelaInstituicao(int ORG_CODIGO_ORGAO)
        {
            return (from tb135 in RetornaTodosRegistros()
                    where tb135.ORG_CODIGO_ORGAO == ORG_CODIGO_ORGAO
                    select tb135).AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TB135_PROG_SOCIAIS pela chave primária "CO_IDENT_PROGR_SOCIA".
        /// </summary>
        /// <param name="CO_IDENT_PROGR_SOCIA">Id da chave primária</param>
        /// <returns>Entidade TB135_PROG_SOCIAIS</returns>
        public static TB135_PROG_SOCIAIS RetornaPelaChavePrimaria(int CO_IDENT_PROGR_SOCIA)
        {
            return (from tb135 in RetornaTodosRegistros()
                    where tb135.CO_IDENT_PROGR_SOCIA == CO_IDENT_PROGR_SOCIA
                    select tb135).FirstOrDefault();
        }

        #endregion
    }
}
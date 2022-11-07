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
    public partial class TB148_TIPO_BOLSA
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
        /// Exclue o registro da tabela TB148_TIPO_BOLSA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB148_TIPO_BOLSA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB148_TIPO_BOLSA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB148_TIPO_BOLSA.</returns>
        public static TB148_TIPO_BOLSA Delete(TB148_TIPO_BOLSA entity)
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
        public static int SaveOrUpdate(TB148_TIPO_BOLSA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB148_TIPO_BOLSA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB148_TIPO_BOLSA.</returns>
        public static TB148_TIPO_BOLSA SaveOrUpdate(TB148_TIPO_BOLSA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB148_TIPO_BOLSA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB148_TIPO_BOLSA.</returns>
        public static TB148_TIPO_BOLSA GetByEntityKey(EntityKey entityKey)
        {
            return (TB148_TIPO_BOLSA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB148_TIPO_BOLSA, ordenados pela descrição "DE_TIPO_BOLSA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB148_TIPO_BOLSA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB148_TIPO_BOLSA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB148_TIPO_BOLSA.OrderBy( t => t.DE_TIPO_BOLSA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB148_TIPO_BOLSA pela chave primária "CO_TIPO_BOLSA".
        /// </summary>
        /// <param name="CO_TIPO_BOLSA">Id da chave primária</param>
        /// <returns>Entidade TB148_TIPO_BOLSA</returns>
        public static TB148_TIPO_BOLSA RetornaPelaChavePrimaria(int CO_TIPO_BOLSA)
        {
            return (from tb148 in RetornaTodosRegistros()
                    where tb148.CO_TIPO_BOLSA == CO_TIPO_BOLSA
                    select tb148).FirstOrDefault();
        }

        #endregion
    }
}

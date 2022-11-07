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
    public partial class TB309_CONTR_EXPOR_DADOS
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
        /// Exclue o registro da tabela TB309_CONTR_EXPOR_DADOS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB309_CONTR_EXPOR_DADOS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB309_CONTR_EXPOR_DADOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB309_CONTR_EXPOR_DADOS.</returns>
        public static TB309_CONTR_EXPOR_DADOS Delete(TB309_CONTR_EXPOR_DADOS entity)
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
        public static int SaveOrUpdate(TB309_CONTR_EXPOR_DADOS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB309_CONTR_EXPOR_DADOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB309_CONTR_EXPOR_DADOS.</returns>
        public static TB309_CONTR_EXPOR_DADOS SaveOrUpdate(TB309_CONTR_EXPOR_DADOS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB309_CONTR_EXPOR_DADOS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB309_CONTR_EXPOR_DADOS.</returns>
        public static TB309_CONTR_EXPOR_DADOS GetByEntityKey(EntityKey entityKey)
        {
            return (TB309_CONTR_EXPOR_DADOS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB309_CONTR_EXPOR_DADOS, ordenados pelo ID.
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB309_CONTR_EXPOR_DADOS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB309_CONTR_EXPOR_DADOS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB309_CONTR_EXPOR_DADOS.OrderBy(o => o.ID_EXPDADOS).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB309_CONTR_EXPOR_DADOS pela chave primária "ID_EXPDADOS"
        /// </summary>
        /// <param name="ID_EXPDADOS">Id da chave primária</param>
        /// <returns>Entidade TB309_CONTR_EXPOR_DADOS</returns>
        public static TB309_CONTR_EXPOR_DADOS RetornaPelaChavePrimaria(int ID_EXPDADOS)
        {
            return (from tb309 in RetornaTodosRegistros()
                    where tb309.ID_EXPDADOS == ID_EXPDADOS
                    select tb309).FirstOrDefault();
        }

        #endregion
    }
}
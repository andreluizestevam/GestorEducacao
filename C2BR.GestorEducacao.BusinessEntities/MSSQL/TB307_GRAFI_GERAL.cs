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
    public partial class TB307_GRAFI_GERAL
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
        /// Exclue o registro da tabela TB307_GRAFI_GERAL do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB307_GRAFI_GERAL entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB307_GRAFI_GERAL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB307_GRAFI_GERAL.</returns>
        public static TB307_GRAFI_GERAL Delete(TB307_GRAFI_GERAL entity)
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
        public static int SaveOrUpdate(TB307_GRAFI_GERAL entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB307_GRAFI_GERAL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB307_GRAFI_GERAL.</returns>
        public static TB307_GRAFI_GERAL SaveOrUpdate(TB307_GRAFI_GERAL entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB307_GRAFI_GERAL de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB307_GRAFI_GERAL.</returns>
        public static TB307_GRAFI_GERAL GetByEntityKey(EntityKey entityKey)
        {
            return (TB307_GRAFI_GERAL)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB307_GRAFI_GERAL, ordenados pelo Id "ID_GRAFI_GERAL".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB307_GRAFI_GERAL de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB307_GRAFI_GERAL> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB307_GRAFI_GERAL.OrderBy(q => q.ID_GRAFI_GERAL).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB307_GRAFI_GERAL pela chave primária "ID_GRAFI_GERAL".
        /// </summary>
        /// <param name="ID_GRAFI_GERAL">Id da chave primária</param>
        /// <returns>Entidade TB307_GRAFI_GERAL</returns>
        public static TB307_GRAFI_GERAL RetornaPelaChavePrimaria(int ID_GRAFI_GERAL)
        {
            return (from tb307 in RetornaTodosRegistros()
                    where tb307.ID_GRAFI_GERAL == ID_GRAFI_GERAL
                    select tb307).FirstOrDefault();
        }

        #endregion
    }
}
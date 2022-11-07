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
    public partial class TB308_GRAFI_USUAR
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
        /// Exclue o registro da tabela TB308_GRAFI_USUAR do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB308_GRAFI_USUAR entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB308_GRAFI_USUAR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB308_GRAFI_USUAR.</returns>
        public static TB308_GRAFI_USUAR Delete(TB308_GRAFI_USUAR entity)
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
        public static int SaveOrUpdate(TB308_GRAFI_USUAR entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB308_GRAFI_USUAR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB308_GRAFI_USUAR.</returns>
        public static TB308_GRAFI_USUAR SaveOrUpdate(TB308_GRAFI_USUAR entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB308_GRAFI_USUAR de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB308_GRAFI_USUAR.</returns>
        public static TB308_GRAFI_USUAR GetByEntityKey(EntityKey entityKey)
        {
            return (TB308_GRAFI_USUAR)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB308_GRAFI_USUAR, ordenados pelo Id "ID_GRAFI_USUAR".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB308_GRAFI_USUAR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB308_GRAFI_USUAR> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB308_GRAFI_USUAR.OrderBy(q => q.ID_GRAFI_USUAR).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB308_GRAFI_USUAR pela chave primária "ID_GRAFI_USUAR".
        /// </summary>
        /// <param name="ID_GRAFI_USUAR">Id da chave primária</param>
        /// <returns>Entidade TB308_GRAFI_USUAR</returns>
        public static TB308_GRAFI_USUAR RetornaPelaChavePrimaria(int ID_GRAFI_USUAR)
        {
            return (from tb308 in RetornaTodosRegistros()
                    where tb308.ID_GRAFI_USUAR == ID_GRAFI_USUAR
                    select tb308).FirstOrDefault();
        }

        #endregion
    }
}
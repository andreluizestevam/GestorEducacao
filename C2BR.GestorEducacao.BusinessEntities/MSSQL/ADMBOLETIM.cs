//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class ADMBOLETIM
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
        /// Exclue o registro da tabela ADMBOLETIM do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(ADMBOLETIM entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela ADMBOLETIM na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade ADMBOLETIM.</returns>
        public static ADMBOLETIM Delete(ADMBOLETIM entity)
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
        public static int SaveOrUpdate(ADMBOLETIM entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela ADMBOLETIM na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade ADMBOLETIM.</returns>
        public static ADMBOLETIM SaveOrUpdate(ADMBOLETIM entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade ADMBOLETIM de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade ADMBOLETIM.</returns>
        public static ADMBOLETIM GetByEntityKey(EntityKey entityKey)
        {
            return (ADMBOLETIM)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registros da entidade ADMBOLETIM onde o status "flaStat" é "A"tivo e ordenado pelo número de ordem "CO_BOL".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade ADMBOLETIM de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<ADMBOLETIM> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.ADMBOLETIM.OrderBy(o => o.CO_BOL).AsObjectQuery();
        }

        public static ADMBOLETIM RetornaPelaChavePrimaria( int CO_BOL)
        {
            return (from b in RetornaTodosRegistros()
                        where b.CO_BOL == CO_BOL
                        select b).FirstOrDefault();
        }

        #endregion

        #endregion
    }
}

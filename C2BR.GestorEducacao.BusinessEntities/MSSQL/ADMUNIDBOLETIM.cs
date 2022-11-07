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
    public partial class ADMUNIDBOLETIM
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
        public static int Delete(ADMUNIDBOLETIM entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela ADMBOLETIM na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade ADMBOLETIM.</returns>
        public static ADMUNIDBOLETIM Delete(ADMUNIDBOLETIM entity)
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
        public static int SaveOrUpdate(ADMUNIDBOLETIM entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela ADMBOLETIM na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade ADMBOLETIM.</returns>
        public static ADMUNIDBOLETIM SaveOrUpdate(ADMUNIDBOLETIM entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade ADMBOLETIM de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade ADMBOLETIM.</returns>
        public static ADMUNIDBOLETIM GetByEntityKey(EntityKey entityKey)
        {
            return (ADMUNIDBOLETIM)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registros da entidade ADMBOLETIM onde o status "flaStat" é "A"tivo e ordenado pelo número de ordem "CO_BOL".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade ADMBOLETIM de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<ADMUNIDBOLETIM> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.ADMUNIDBOLETIM.OrderBy(o => o.DT_CRIAC).AsObjectQuery();
        }

        public static ADMUNIDBOLETIM RetornaPelaChavePrimaria(int ID_UNID_BOL)
        {
            return (from b in RetornaTodosRegistros()
                    where b.ID_UNID_BOL == ID_UNID_BOL
                    select b).FirstOrDefault();
        }

        #endregion

        #endregion
    }
}

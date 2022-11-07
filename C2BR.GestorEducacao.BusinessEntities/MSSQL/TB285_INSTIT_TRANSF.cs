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
    public partial class TB285_INSTIT_TRANSF
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
        /// Exclue o registro da tabela TB285_INSTIT_TRANSF do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB285_INSTIT_TRANSF entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB285_INSTIT_TRANSF na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB285_INSTIT_TRANSF.</returns>
        public static TB285_INSTIT_TRANSF Delete(TB285_INSTIT_TRANSF entity)
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
        public static int SaveOrUpdate(TB285_INSTIT_TRANSF entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB285_INSTIT_TRANSF na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB285_INSTIT_TRANSF.</returns>
        public static TB285_INSTIT_TRANSF SaveOrUpdate(TB285_INSTIT_TRANSF entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB285_INSTIT_TRANSF de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB285_INSTIT_TRANSF.</returns>
        public static TB285_INSTIT_TRANSF GetByEntityKey(EntityKey entityKey)
        {
            return (TB285_INSTIT_TRANSF)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB285_INSTIT_TRANSF, ordenados pelo nome "NO_INSTIT_TRANSF".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB285_INSTIT_TRANSF de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB285_INSTIT_TRANSF> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB285_INSTIT_TRANSF.OrderBy( i => i.NO_INSTIT_TRANSF ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB285_INSTIT_TRANSF pela chave primária "ID_INSTIT_TRANSF".
        /// </summary>
        /// <param name="ID_INSTIT_TRANSF">Id da chave primária</param>
        /// <returns>Entidade TB285_INSTIT_TRANSF</returns>
        public static TB285_INSTIT_TRANSF RetornaPelaChavePrimaria(int ID_INSTIT_TRANSF)
        {
            return (from tb285 in RetornaTodosRegistros()
                    where tb285.ID_INSTIT_TRANSF == ID_INSTIT_TRANSF
                    select tb285).FirstOrDefault();
        }

        #endregion
    }
}
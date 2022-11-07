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
    public partial class VW78_03_PESQ_AVAL
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
        /// Exclue o registro da tabela VW78_03_PESQ_AVAL do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(VW78_03_PESQ_AVAL entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela VW78_03_PESQ_AVAL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade VW78_03_PESQ_AVAL.</returns>
        public static VW78_03_PESQ_AVAL Delete(VW78_03_PESQ_AVAL entity)
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
        public static int SaveOrUpdate(VW78_03_PESQ_AVAL entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela VW78_03_PESQ_AVAL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade VW78_03_PESQ_AVAL.</returns>
        public static VW78_03_PESQ_AVAL SaveOrUpdate(VW78_03_PESQ_AVAL entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade VW78_03_PESQ_AVAL de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade VW78_03_PESQ_AVAL.</returns>
        public static VW78_03_PESQ_AVAL GetByEntityKey(EntityKey entityKey)
        {
            return (VW78_03_PESQ_AVAL)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade VW78_03_PESQ_AVAL, ordenados pelo Id "CO_PESQ_AVAL".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade VW78_03_PESQ_AVAL de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<VW78_03_PESQ_AVAL> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.VW78_03_PESQ_AVAL.OrderBy( p => p.CO_PESQ_AVAL ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade VW78_03_PESQ_AVAL pela chave primária "CO_PESQ_AVAL".
        /// </summary>
        /// <param name="CO_PESQ_AVAL">Id da chave primária</param>
        /// <returns>Entidade VW78_03_PESQ_AVAL</returns>
        public static VW78_03_PESQ_AVAL RetornaPelaChavePrimaria(int CO_PESQ_AVAL)
        {
            return (from tb78 in VW78_03_PESQ_AVAL.RetornaTodosRegistros()
                    where tb78.CO_PESQ_AVAL == CO_PESQ_AVAL
                    select tb78).FirstOrDefault();
        }

        #endregion
    }
}
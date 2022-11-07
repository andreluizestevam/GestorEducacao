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
    public partial class VW_DEMON_DISTR_FUNCI
    {
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
        /// Exclue o registro da tabela VW_DEMON_DISTR_FUNCI do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(VW_DEMON_DISTR_FUNCI entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela VW_DEMON_DISTR_FUNCI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade VW_DEMON_DISTR_FUNCI.</returns>
        public static VW_DEMON_DISTR_FUNCI Delete(VW_DEMON_DISTR_FUNCI entity)
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
        public static int SaveOrUpdate(VW_DEMON_DISTR_FUNCI entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela VW_DEMON_DISTR_FUNCI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade VW_DEMON_DISTR_FUNCI.</returns>
        public static VW_DEMON_DISTR_FUNCI SaveOrUpdate(VW_DEMON_DISTR_FUNCI entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade VW_DEMON_DISTR_FUNCI de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade VW_DEMON_DISTR_FUNCI.</returns>
        public static VW_DEMON_DISTR_FUNCI GetByEntityKey(EntityKey entityKey)
        {
            return (VW_DEMON_DISTR_FUNCI)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade VW_DEMON_DISTR_FUNCI, ordenados pelo Id do aluno "CO_ALU".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade VW_DEMON_DISTR_FUNCI de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<VW_DEMON_DISTR_FUNCI> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.VW_DEMON_DISTR_FUNCI.OrderBy(h => h.CO_EMP).AsObjectQuery();
        }

        #endregion
    }
}

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
    public partial class TB37_RECDES_FIXA
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
        /// Exclue o registro da tabela TB37_RECDES_FIXA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB37_RECDES_FIXA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB37_RECDES_FIXA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB37_RECDES_FIXA.</returns>
        public static TB37_RECDES_FIXA Delete(TB37_RECDES_FIXA entity)
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
        public static int SaveOrUpdate(TB37_RECDES_FIXA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB37_RECDES_FIXA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB37_RECDES_FIXA.</returns>
        public static TB37_RECDES_FIXA SaveOrUpdate(TB37_RECDES_FIXA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB37_RECDES_FIXA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB37_RECDES_FIXA.</returns>
        public static TB37_RECDES_FIXA GetByEntityKey(EntityKey entityKey)
        {
            return (TB37_RECDES_FIXA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB37_RECDES_FIXA, ordenados pelo Id da unidade "CO_EMP" e pelo código "CO_CON_RECDES".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB37_RECDES_FIXA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB37_RECDES_FIXA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB37_RECDES_FIXA.OrderBy( r => r.CO_EMP).ThenBy( r => r.CO_CON_RECDES ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB37_RECDES_FIXA pelas chaves primárias "CO_EMP", "CO_CON_RECDES" e "CO_ADITI_RECDES".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_CON_RECDES">Código do documento</param>
        /// <param name="CO_ADITI_RECDES">Código do aditivo</param>
        /// <returns>Entidade TB37_RECDES_FIXA</returns>
        public static TB37_RECDES_FIXA RetornaPelaChavePrimaria(int CO_EMP, string CO_CON_RECDES, int CO_ADITI_RECDES)
        {
            return (from tb37 in RetornaTodosRegistros()
                    where tb37.CO_EMP == CO_EMP && tb37.CO_CON_RECDES == CO_CON_RECDES && tb37.CO_ADITI_RECDES == CO_ADITI_RECDES
                    select tb37).FirstOrDefault();
        }

        #endregion
    }
}
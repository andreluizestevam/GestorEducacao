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
    public partial class TB312_CONTR_COMPR
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
        /// Exclue o registro da tabela TB312_CONTR_COMPR do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB312_CONTR_COMPR entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB312_CONTR_COMPR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB312_CONTR_COMPR.</returns>
        public static TB312_CONTR_COMPR Delete(TB312_CONTR_COMPR entity)
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
        public static int SaveOrUpdate(TB312_CONTR_COMPR entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB312_CONTR_COMPR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB312_CONTR_COMPR.</returns>
        public static TB312_CONTR_COMPR SaveOrUpdate(TB312_CONTR_COMPR entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB312_CONTR_COMPR de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB312_CONTR_COMPR.</returns>
        public static TB312_CONTR_COMPR GetByEntityKey(EntityKey entityKey)
        {
            return (TB312_CONTR_COMPR)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB312_CONTR_COMPR, ordenados pelo nome "CO_CONTR_COMPR".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB312_CONTR_COMPR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB312_CONTR_COMPR> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB312_CONTR_COMPR.OrderBy(c => c.CO_CONTR_COMPR).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna primeiro registro da entidade TB312_CONTR_COMPR pelo cliente "ID_REFER_ATIVID_AULAS".
        /// </summary>
        /// <param name="ID_REFER_ATIVID_AULAS">Id da chave primária</param>
        /// <returns>Entidade TB312_CONTR_COMPR</returns>
        public static TB312_CONTR_COMPR RetornaPelaChavePrimaria(int CO_EMP, string CO_CONTR_COMPR, int CO_ADITI_CONTR_COMPR)
        {
            return (from tb312 in RetornaTodosRegistros()
                    where tb312.CO_EMP == CO_EMP && tb312.CO_CONTR_COMPR == CO_CONTR_COMPR && tb312.CO_ADITI_CONTR_COMPR == CO_ADITI_CONTR_COMPR
                    select tb312).FirstOrDefault();
        }

        #endregion
    }
}

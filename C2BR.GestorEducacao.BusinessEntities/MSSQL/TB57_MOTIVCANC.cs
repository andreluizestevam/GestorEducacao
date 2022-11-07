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
    public partial class TB57_MOTIVCANC
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
        /// Exclue o registro da tabela TB57_MOTIVCANC do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB57_MOTIVCANC entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB57_MOTIVCANC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB57_MOTIVCANC.</returns>
        public static TB57_MOTIVCANC Delete(TB57_MOTIVCANC entity)
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
        public static int SaveOrUpdate(TB57_MOTIVCANC entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB57_MOTIVCANC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB57_MOTIVCANC.</returns>
        public static TB57_MOTIVCANC SaveOrUpdate(TB57_MOTIVCANC entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB57_MOTIVCANC de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB57_MOTIVCANC.</returns>
        public static TB57_MOTIVCANC GetByEntityKey(EntityKey entityKey)
        {
            return (TB57_MOTIVCANC)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB57_MOTIVCANC, ordenados pela descrição "DE_MOTI_CANC".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB57_MOTIVCANC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB57_MOTIVCANC> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB57_MOTIVCANC.OrderBy( m => m.DE_MOTI_CANC ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB57_MOTIVCANC pela chave primária "CO_MOTI_CANC".
        /// </summary>
        /// <param name="CO_MOTI_CANC">Id da chave primária</param>
        /// <returns>Entidade TB57_MOTIVCANC</returns>
        public static TB57_MOTIVCANC RetornaPelaChavePrimaria(int CO_MOTI_CANC)
        {
            return (from tb57 in RetornaTodosRegistros()
                    where tb57.CO_MOTI_CANC == CO_MOTI_CANC
                    select tb57).FirstOrDefault();
        }

        #endregion
    }
}

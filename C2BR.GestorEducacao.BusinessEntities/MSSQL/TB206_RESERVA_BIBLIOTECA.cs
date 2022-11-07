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
    public partial class TB206_RESERVA_BIBLIOTECA
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
        /// Exclue o registro da tabela TB206_RESERVA_BIBLIOTECA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB206_RESERVA_BIBLIOTECA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB206_RESERVA_BIBLIOTECA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB206_RESERVA_BIBLIOTECA.</returns>
        public static TB206_RESERVA_BIBLIOTECA Delete(TB206_RESERVA_BIBLIOTECA entity)
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
        public static int SaveOrUpdate(TB206_RESERVA_BIBLIOTECA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB206_RESERVA_BIBLIOTECA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB206_RESERVA_BIBLIOTECA.</returns>
        public static TB206_RESERVA_BIBLIOTECA SaveOrUpdate(TB206_RESERVA_BIBLIOTECA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB206_RESERVA_BIBLIOTECA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB206_RESERVA_BIBLIOTECA.</returns>
        public static TB206_RESERVA_BIBLIOTECA GetByEntityKey(EntityKey entityKey)
        {
            return (TB206_RESERVA_BIBLIOTECA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB206_RESERVA_BIBLIOTECA, ordenados pela data de reserva "DT_RESERVA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB206_RESERVA_BIBLIOTECA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB206_RESERVA_BIBLIOTECA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB206_RESERVA_BIBLIOTECA.OrderBy( r => r.DT_RESERVA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB206_RESERVA_BIBLIOTECA pela chave primária "CO_RESERVA_BIBLIOTECA".
        /// </summary>
        /// <param name="CO_RESERVA_BIBLIOTECA">Id da chave primária</param>
        /// <returns>Entidade TB206_RESERVA_BIBLIOTECA</returns>
        public static TB206_RESERVA_BIBLIOTECA RetornaPelaChavePrimaria(int CO_RESERVA_BIBLIOTECA)
        {
            return (from tb206 in RetornaTodosRegistros()
                    where tb206.CO_RESERVA_BIBLIOTECA == CO_RESERVA_BIBLIOTECA
                    select tb206).FirstOrDefault();
        }

        #endregion
    }
}

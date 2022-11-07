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
    public partial class TB150_TIPO_OCORR
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
        /// Exclue o registro da tabela TB150_TIPO_OCORR do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB150_TIPO_OCORR entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB150_TIPO_OCORR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB150_TIPO_OCORR.</returns>
        public static TB150_TIPO_OCORR Delete(TB150_TIPO_OCORR entity)
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
        public static int SaveOrUpdate(TB150_TIPO_OCORR entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB150_TIPO_OCORR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB150_TIPO_OCORR.</returns>
        public static TB150_TIPO_OCORR SaveOrUpdate(TB150_TIPO_OCORR entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB150_TIPO_OCORR de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB150_TIPO_OCORR.</returns>
        public static TB150_TIPO_OCORR GetByEntityKey(EntityKey entityKey)
        {
            return (TB150_TIPO_OCORR)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB150_TIPO_OCORR, ordenados pela sigla "CO_SIGL_OCORR".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB150_TIPO_OCORR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB150_TIPO_OCORR> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB150_TIPO_OCORR.OrderBy( t => t.CO_SIGL_OCORR ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB150_TIPO_OCORR pela chave primária "CO_SIGL_OCORR".
        /// </summary>
        /// <param name="CO_SIGL_OCORR">Id da chave primária</param>
        /// <returns>Entidade TB150_TIPO_OCORR</returns>
        public static TB150_TIPO_OCORR RetornaPelaChavePrimaria(string CO_SIGL_OCORR)
        {
            return (from tb150 in RetornaTodosRegistros()
                    where tb150.CO_SIGL_OCORR == CO_SIGL_OCORR
                    select tb150).FirstOrDefault();
        }

        #endregion
    }
}

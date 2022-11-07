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
    public partial class TB215_PROGR_TIPO_SOCEDU
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
        /// Exclue o registro da tabela TB215_PROGR_TIPO_SOCEDU do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB215_PROGR_TIPO_SOCEDU entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB215_PROGR_TIPO_SOCEDU na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB215_PROGR_TIPO_SOCEDU.</returns>
        public static TB215_PROGR_TIPO_SOCEDU Delete(TB215_PROGR_TIPO_SOCEDU entity)
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
        public static int SaveOrUpdate(TB215_PROGR_TIPO_SOCEDU entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB215_PROGR_TIPO_SOCEDU na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB215_PROGR_TIPO_SOCEDU.</returns>
        public static TB215_PROGR_TIPO_SOCEDU SaveOrUpdate(TB215_PROGR_TIPO_SOCEDU entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB215_PROGR_TIPO_SOCEDU de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB215_PROGR_TIPO_SOCEDU.</returns>
        public static TB215_PROGR_TIPO_SOCEDU GetByEntityKey(EntityKey entityKey)
        {
            return (TB215_PROGR_TIPO_SOCEDU)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB215_PROGR_TIPO_SOCEDU, ordenados pelo nome "NO_PROGR_TP_SOCEDU".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB215_PROGR_TIPO_SOCEDU de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB215_PROGR_TIPO_SOCEDU> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB215_PROGR_TIPO_SOCEDU.OrderBy( p => p.NO_PROGR_TP_SOCEDU ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB215_PROGR_TIPO_SOCEDU pela chave primária "CO_PROGR_TP_SOCEDU".
        /// </summary>
        /// <param name="CO_PROGR_TP_SOCEDU">Id da chave primária</param>
        /// <returns>Entidade TB215_PROGR_TIPO_SOCEDU</returns>
        public static TB215_PROGR_TIPO_SOCEDU RetornaPelaChavePrimaria(int CO_PROGR_TP_SOCEDU)
        {
            return (from tb215 in RetornaTodosRegistros()
                    where tb215.CO_PROGR_TP_SOCEDU == CO_PROGR_TP_SOCEDU
                    select tb215).FirstOrDefault();
        }
        #endregion
    }
}

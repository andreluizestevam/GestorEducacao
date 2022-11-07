﻿//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
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
    public partial class TB249_MENSAG_SMS
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
        /// Exclue o registro da tabela TB249_MENSAG_SMS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB249_MENSAG_SMS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB249_MENSAG_SMS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB249_MENSAG_SMS.</returns>
        public static TB249_MENSAG_SMS Delete(TB249_MENSAG_SMS entity)
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
        public static int SaveOrUpdate(TB249_MENSAG_SMS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB249_MENSAG_SMS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB249_MENSAG_SMS.</returns>
        public static TB249_MENSAG_SMS SaveOrUpdate(TB249_MENSAG_SMS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB249_MENSAG_SMS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB249_MENSAG_SMS.</returns>
        public static TB249_MENSAG_SMS GetByEntityKey(EntityKey entityKey)
        {
            return (TB249_MENSAG_SMS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB249_MENSAG_SMS, ordenados pela data de envio da mensagem "DT_ENVIO_MENSAG_SMS".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB249_MENSAG_SMS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB249_MENSAG_SMS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB249_MENSAG_SMS.OrderBy( m => m.DT_ENVIO_MENSAG_SMS ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB249_MENSAG_SMS pela chave primária "ID_MENSAG_SMS".
        /// </summary>
        /// <param name="ID_MENSAG_SMS">Id da chave primária</param>
        /// <returns>Entidade TB249_MENSAG_SMS</returns>
        public static TB249_MENSAG_SMS RetornaPelaChavePrimaria(int ID_MENSAG_SMS)
        {
            return (from tb249 in RetornaTodosRegistros()
                    where tb249.ID_MENSAG_SMS == ID_MENSAG_SMS
                    select tb249).FirstOrDefault();
        }

        #endregion
    }
}
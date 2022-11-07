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
    public partial class TB214_ENTR_DOCUMENTO
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
        /// Exclue o registro da tabela TB214_ENTR_DOCUMENTO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB214_ENTR_DOCUMENTO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB214_ENTR_DOCUMENTO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB214_ENTR_DOCUMENTO.</returns>
        public static TB214_ENTR_DOCUMENTO Delete(TB214_ENTR_DOCUMENTO entity)
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
        public static int SaveOrUpdate(TB214_ENTR_DOCUMENTO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB214_ENTR_DOCUMENTO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB214_ENTR_DOCUMENTO.</returns>
        public static TB214_ENTR_DOCUMENTO SaveOrUpdate(TB214_ENTR_DOCUMENTO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB214_ENTR_DOCUMENTO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB214_ENTR_DOCUMENTO.</returns>
        public static TB214_ENTR_DOCUMENTO GetByEntityKey(EntityKey entityKey)
        {
            return (TB214_ENTR_DOCUMENTO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB214_ENTR_DOCUMENTO, ordenados pelo Id da série "TB211_SOLIC_DIPLOMA.CO_CUR".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB214_ENTR_DOCUMENTO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB214_ENTR_DOCUMENTO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB214_ENTR_DOCUMENTO.OrderBy( e => e.TB211_SOLIC_DIPLOMA.CO_CUR ).AsObjectQuery();
        }

        #endregion

        #endregion
    }
}
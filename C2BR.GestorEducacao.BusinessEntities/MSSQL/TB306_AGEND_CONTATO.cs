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
    public partial class TB306_AGEND_CONTATO
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
        /// Exclue o registro da tabela TB306_AGEND_CONTATO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB306_AGEND_CONTATO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB306_AGEND_CONTATO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB306_AGEND_CONTATO.</returns>
        public static TB306_AGEND_CONTATO Delete(TB306_AGEND_CONTATO entity)
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
        public static int SaveOrUpdate(TB306_AGEND_CONTATO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB306_AGEND_CONTATO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB306_AGEND_CONTATO.</returns>
        public static TB306_AGEND_CONTATO SaveOrUpdate(TB306_AGEND_CONTATO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB306_AGEND_CONTATO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB306_AGEND_CONTATO.</returns>
        public static TB306_AGEND_CONTATO GetByEntityKey(EntityKey entityKey)
        {
            return (TB306_AGEND_CONTATO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB306_AGEND_CONTATO, ordenados pelo Id "ID_AGEND_CONTAT".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB306_AGEND_CONTATO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB306_AGEND_CONTATO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB306_AGEND_CONTATO.OrderBy(c => c.ID_AGEND_CONTAT).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB306_AGEND_CONTATO pela chave primária "ID_AGEND_CONTAT".
        /// </summary>
        /// <param name="ID_AGEND_CONTAT">Id da chave primária</param>
        /// <returns>Entidade TB306_AGEND_CONTATO</returns>
        public static TB306_AGEND_CONTATO RetornaPelaChavePrimaria(int ID_AGEND_CONTAT)
        {
            return (from tb306 in RetornaTodosRegistros()
                    where tb306.ID_AGEND_CONTAT == ID_AGEND_CONTAT
                    select tb306).FirstOrDefault();
        }

        #endregion
    }
}
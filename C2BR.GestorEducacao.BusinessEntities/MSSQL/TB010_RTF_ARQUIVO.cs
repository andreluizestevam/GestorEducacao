﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TB010_RTF_ARQUIVO
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
        /// Exclue o registro da tabela TB010_RTF_ARQUIVO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB010_RTF_ARQUIVO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB010_RTF_ARQUIVO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB010_RTF_ARQUIVO.</returns>
        public static TB010_RTF_ARQUIVO Delete(TB010_RTF_ARQUIVO entity)
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
        public static int SaveOrUpdate(TB010_RTF_ARQUIVO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB010_RTF_ARQUIVO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB010_RTF_ARQUIVO.</returns>
        public static TB010_RTF_ARQUIVO SaveOrUpdate(TB010_RTF_ARQUIVO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB010_RTF_ARQUIVO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB010_RTF_ARQUIVO.</returns>
        public static TB010_RTF_ARQUIVO GetByEntityKey(EntityKey entityKey)
        {
            return (TB010_RTF_ARQUIVO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB010_RTF_ARQUIVO, ordenados pelo nome "NM_DOCUM".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB010_RTF_ARQUIVO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB010_RTF_ARQUIVO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB010_RTF_ARQUIVO.OrderBy(x => x.ID_ARQUI).AsObjectQuery();
        }

        #endregion


        /// <summary>
        /// Retorna um registro da entidade TB009_DOCTOS_RTF pelas chaves primárias "ID_ARQUI".
        /// </summary>
        /// <param name="ID">Id do Documento</param>       
        /// <returns>Entidade TB010_RTF_ARQUIVO</returns>
        public static TB010_RTF_ARQUIVO RetornaPelaChavePrimaria(int ID_ARQUI)
        {
            return (from tb010 in RetornaTodosRegistros()
                    where tb010.ID_ARQUI == ID_ARQUI
                    select tb010).FirstOrDefault();
        }
    }
}
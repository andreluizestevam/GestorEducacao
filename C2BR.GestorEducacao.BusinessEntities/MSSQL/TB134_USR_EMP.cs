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
    public partial class TB134_USR_EMP
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
        /// Exclue o registro da tabela TB134_USR_EMP do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB134_USR_EMP entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB134_USR_EMP na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB134_USR_EMP.</returns>
        public static TB134_USR_EMP Delete(TB134_USR_EMP entity)
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
        public static int SaveOrUpdate(TB134_USR_EMP entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB134_USR_EMP na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB134_USR_EMP.</returns>
        public static TB134_USR_EMP SaveOrUpdate(TB134_USR_EMP entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB134_USR_EMP de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB134_USR_EMP.</returns>
        public static TB134_USR_EMP GetByEntityKey(EntityKey entityKey)
        {
            return (TB134_USR_EMP)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB134_USR_EMP, ordenados pelo Id "IDE_USREMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB134_USR_EMP de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB134_USR_EMP> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB134_USR_EMP.OrderBy( u => u.IDE_USREMP ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB134_USR_EMP pela chave primária "IDE_USREMP".
        /// </summary>
        /// <param name="IDE_USREMP">Id da chave primária</param>
        /// <returns>Entidade TB134_USR_EMP</returns>
        public static TB134_USR_EMP RetornaPelaChavePrimaria(int IDE_USREMP)
        {
            return (from tb134 in RetornaTodosRegistros()
                    where tb134.IDE_USREMP == IDE_USREMP
                    select tb134).FirstOrDefault();
        }

        #endregion
    }
}

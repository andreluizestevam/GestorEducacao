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
    public partial class TB160_PERFIL_UNIDADE
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
        /// Exclue o registro da tabela TB160_PERFIL_UNIDADE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB160_PERFIL_UNIDADE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB160_PERFIL_UNIDADE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB160_PERFIL_UNIDADE.</returns>
        public static TB160_PERFIL_UNIDADE Delete(TB160_PERFIL_UNIDADE entity)
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
        public static int SaveOrUpdate(TB160_PERFIL_UNIDADE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB160_PERFIL_UNIDADE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB160_PERFIL_UNIDADE.</returns>
        public static TB160_PERFIL_UNIDADE SaveOrUpdate(TB160_PERFIL_UNIDADE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB160_PERFIL_UNIDADE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB160_PERFIL_UNIDADE.</returns>
        public static TB160_PERFIL_UNIDADE GetByEntityKey(EntityKey entityKey)
        {
            return (TB160_PERFIL_UNIDADE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB160_PERFIL_UNIDADE, ordenados pelo Id da unidade "CO_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB160_PERFIL_UNIDADE de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB160_PERFIL_UNIDADE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB160_PERFIL_UNIDADE.OrderBy( p => p.CO_EMP ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB160_PERFIL_UNIDADE pela chave primária "CO_EMP".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>Entidade TB160_PERFIL_UNIDADE</returns>
        public static TB160_PERFIL_UNIDADE RetornaPelaChavePrimaria(int CO_EMP)
        {
            return (from tb160 in RetornaTodosRegistros()
                    where tb160.CO_EMP == CO_EMP
                    select tb160).FirstOrDefault();
        }

        #endregion
    }
}
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
    public partial class TBS355_PROC_MEDIC_SGRUP
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
        /// Exclue o registro da tabela TBS355_PROC_MEDIC_SGRUP do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS355_PROC_MEDIC_SGRUP entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS355_PROC_MEDIC_SGRUP na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS355_PROC_MEDIC_SGRUP.</returns>
        public static TBS355_PROC_MEDIC_SGRUP Delete(TBS355_PROC_MEDIC_SGRUP entity)
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
        public static int SaveOrUpdate(TBS355_PROC_MEDIC_SGRUP entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS355_PROC_MEDIC_SGRUP na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS355_PROC_MEDIC_SGRUP.</returns>
        public static TBS355_PROC_MEDIC_SGRUP SaveOrUpdate(TBS355_PROC_MEDIC_SGRUP entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS355_PROC_MEDIC_SGRUP de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS355_PROC_MEDIC_SGRUP.</returns>
        public static TBS355_PROC_MEDIC_SGRUP GetByEntityKey(EntityKey entityKey)
        {
            return (TBS355_PROC_MEDIC_SGRUP)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS341_CAMP_ATEND, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS334_DIAGN_ATEND_MEDIC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS355_PROC_MEDIC_SGRUP> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS355_PROC_MEDIC_SGRUP.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS355_PROC_MEDIC_SGRUP pela chave primária "ID_PROC_MEDIC_SGRUP".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS355_PROC_MEDIC_SGRUP</returns>
        public static TBS355_PROC_MEDIC_SGRUP RetornaPelaChavePrimaria(int ID_PROC_MEDIC_SGRUP)
        {
            return (from tbs355 in RetornaTodosRegistros()
                    where tbs355.ID_PROC_MEDIC_SGRUP == ID_PROC_MEDIC_SGRUP
                    select tbs355).FirstOrDefault();
        }

        #endregion
    }
}

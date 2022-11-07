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
   public partial class TBS366_FUNCAO_SIMPL
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
        /// Exclue o registro da tabela TBS366_FUNCAO_SIMPL do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS366_FUNCAO_SIMPL entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS366_FUNCAO_SIMPL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS366_FUNCAO_SIMPL.</returns>
        public static TBS366_FUNCAO_SIMPL Delete(TBS366_FUNCAO_SIMPL entity)
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
        public static int SaveOrUpdate(TBS366_FUNCAO_SIMPL entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS339_CAMPSAUDE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS366_FUNCAO_SIMPL.</returns>
        public static TBS366_FUNCAO_SIMPL SaveOrUpdate(TBS366_FUNCAO_SIMPL entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS336_ISDA_ITENS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS366_FUNCAO_SIMPL.</returns>
        public static TBS366_FUNCAO_SIMPL GetByEntityKey(EntityKey entityKey)
        {
            return (TBS366_FUNCAO_SIMPL)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS366_FUNCAO_SIMPL, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS334_DIAGN_ATEND_MEDIC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS366_FUNCAO_SIMPL> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS366_FUNCAO_SIMPL.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS366_FUNCAO_SIMPL pela chave primária "ID_FUNCAO_SIMPL".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS366_FUNCAO_SIMPL</returns>
        public static TBS366_FUNCAO_SIMPL RetornaPelaChavePrimaria(int ID_FUNCAO_SIMPL)
        {
            return (from tbs366 in RetornaTodosRegistros()
                    where tbs366.ID_FUNCAO_SIMPL == ID_FUNCAO_SIMPL
                    select tbs366).FirstOrDefault();
        }

        #endregion
    }
}

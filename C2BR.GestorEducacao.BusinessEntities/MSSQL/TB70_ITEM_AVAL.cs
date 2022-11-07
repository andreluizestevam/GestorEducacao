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
    public partial class TB70_ITEM_AVAL
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
        /// Exclue o registro da tabela TB70_ITEM_AVAL do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB70_ITEM_AVAL entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB70_ITEM_AVAL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB70_ITEM_AVAL.</returns>
        public static TB70_ITEM_AVAL Delete(TB70_ITEM_AVAL entity)
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
        public static int SaveOrUpdate(TB70_ITEM_AVAL entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB70_ITEM_AVAL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB70_ITEM_AVAL.</returns>
        public static TB70_ITEM_AVAL SaveOrUpdate(TB70_ITEM_AVAL entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB70_ITEM_AVAL de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB70_ITEM_AVAL.</returns>
        public static TB70_ITEM_AVAL GetByEntityKey(EntityKey entityKey)
        {
            return (TB70_ITEM_AVAL)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB70_ITEM_AVAL, ordenados pelo número da questão "NU_QUES_AVAL".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB70_ITEM_AVAL de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB70_ITEM_AVAL> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB70_ITEM_AVAL.OrderBy( i => i.NU_QUES_AVAL ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB70_ITEM_AVAL pelas chaves primárias "CO_EMP", "CO_PESQ_AVAL", "CO_TIPO_AVAL", "CO_TITU_AVAL" e "NU_QUES_AVAL".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_PESQ_AVAL">Id da pesquisa avaliação</param>
        /// <param name="CO_TIPO_AVAL">Id do tipo de avaliação</param>
        /// <param name="CO_TITU_AVAL">Id do título de avaliação</param>
        /// <param name="NU_QUES_AVAL">Número da questão da avaliação</param>
        /// <returns>Entidade TB70_ITEM_AVAL</returns>
        public static TB70_ITEM_AVAL RetornaPelaChavePrimaria(int CO_EMP, int CO_PESQ_AVAL, int CO_TIPO_AVAL, int CO_TITU_AVAL, int NU_QUES_AVAL)
        {
            return (from tb70 in RetornaTodosRegistros()
                    where tb70.CO_EMP == CO_EMP && tb70.CO_PESQ_AVAL == CO_PESQ_AVAL && tb70.CO_TIPO_AVAL == CO_TIPO_AVAL
                    && tb70.CO_TITU_AVAL == CO_TITU_AVAL && tb70.NU_QUES_AVAL == NU_QUES_AVAL
                    select tb70).FirstOrDefault();
        }

        #endregion
    }
}
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
    public partial class TB72_TIT_QUES_AVAL
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
        /// Exclue o registro da tabela TB72_TIT_QUES_AVAL do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB72_TIT_QUES_AVAL entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB72_TIT_QUES_AVAL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB72_TIT_QUES_AVAL.</returns>
        public static TB72_TIT_QUES_AVAL Delete(TB72_TIT_QUES_AVAL entity)
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
        public static int SaveOrUpdate(TB72_TIT_QUES_AVAL entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB72_TIT_QUES_AVAL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB72_TIT_QUES_AVAL.</returns>
        public static TB72_TIT_QUES_AVAL SaveOrUpdate(TB72_TIT_QUES_AVAL entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB72_TIT_QUES_AVAL de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB72_TIT_QUES_AVAL.</returns>
        public static TB72_TIT_QUES_AVAL GetByEntityKey(EntityKey entityKey)
        {
            return (TB72_TIT_QUES_AVAL)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB72_TIT_QUES_AVAL, ordenados pelo nome "NO_TITU_AVAL".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB72_TIT_QUES_AVAL de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB72_TIT_QUES_AVAL> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB72_TIT_QUES_AVAL.OrderBy( t => t.NO_TITU_AVAL ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB72_TIT_QUES_AVAL pela chave primária "CO_TITU_AVAL".
        /// </summary>
        /// <param name="CO_TITU_AVAL">Id da chave primária</param>
        /// <returns>Entidade TB72_TIT_QUES_AVAL</returns>
        public static TB72_TIT_QUES_AVAL RetornaPelaChavePrimaria(int CO_TITU_AVAL)
        {
            return (from tb72 in RetornaTodosRegistros()
                    where tb72.CO_TITU_AVAL == CO_TITU_AVAL
                    select tb72).FirstOrDefault();
        }

        #endregion
    }
}
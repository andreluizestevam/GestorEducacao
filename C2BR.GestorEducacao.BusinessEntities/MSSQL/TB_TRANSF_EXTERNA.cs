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
    public partial class TB_TRANSF_EXTERNA
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
        /// Exclue o registro da tabela TB_TRANSF_EXTERNA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB_TRANSF_EXTERNA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB_TRANSF_EXTERNA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB_TRANSF_EXTERNA.</returns>
        public static TB_TRANSF_EXTERNA Delete(TB_TRANSF_EXTERNA entity)
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
        public static int SaveOrUpdate(TB_TRANSF_EXTERNA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB_TRANSF_EXTERNA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB_TRANSF_EXTERNA.</returns>
        public static TB_TRANSF_EXTERNA SaveOrUpdate(TB_TRANSF_EXTERNA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB_TRANSF_EXTERNA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB_TRANSF_EXTERNA.</returns>
        public static TB_TRANSF_EXTERNA GetByEntityKey(EntityKey entityKey)
        {
            return (TB_TRANSF_EXTERNA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB_TRANSF_EXTERNA, ordenados pela data de efetivação da transferência "DT_EFETI_TRANSF".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB_TRANSF_INTERNA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB_TRANSF_EXTERNA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB_TRANSF_EXTERNA.OrderBy(t => t.DT_EFETI_TRANSF).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna todos os registro da entidade TB_TRANSF_EXTERNA de acordo com a unidade atual informada "CO_UNIDA_ATUAL", ordenado pela data de efetivação da transferência "DT_EFETI_TRANSF".
        /// </summary>
        /// <param name="CO_UNIDA_ATUAL">Id da unidade atual</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB_TRANSF_INTERNA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB_TRANSF_EXTERNA> RetornaPelaEmpresa(int CO_UNIDA_ATUAL)
        {
            return GestorEntities.CurrentContext.TB_TRANSF_EXTERNA.Where(t => t.CO_UNIDA_ATUAL == CO_UNIDA_ATUAL).OrderBy(t => t.DT_EFETI_TRANSF).AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TB_TRANSF_EXTERNA onde o Id "NU_REF_TRANSF" é o informado no parâmetro.
        /// </summary>
        /// <param name="NU_REF_TRANSF">Id da chave primária</param>
        /// <returns>Entidade TB_TRANSF_EXTERNA</returns>
        public static TB_TRANSF_EXTERNA RetornaPelaChavePrimaria(int CO_UNIDA_ATUAL, decimal CO_NIS_ALUNO)
        {
            return (from tbTransExter in RetornaTodosRegistros()
                    where tbTransExter.CO_UNIDA_ATUAL == CO_UNIDA_ATUAL && tbTransExter.CO_NIS_ALUNO == CO_NIS_ALUNO
                    select tbTransExter).FirstOrDefault();
        }
        #endregion
    }
}
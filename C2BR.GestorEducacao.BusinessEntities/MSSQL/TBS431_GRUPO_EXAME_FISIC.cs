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
    public partial class TBS431_GRUPO_EXAME_FISIC
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
        /// Exclue o registro da tabela TB_EQUIPE_NUCLEO_INST do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS431_GRUPO_EXAME_FISIC entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS431_GRUPO_EXAME_FISIC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS431_GRUPO_EXAME_FISIC.</returns>
        public static TBS431_GRUPO_EXAME_FISIC Delete(TBS431_GRUPO_EXAME_FISIC entity)
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
        public static int SaveOrUpdate(TBS431_GRUPO_EXAME_FISIC entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB_EQUIPE_NUCLEO_INST na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS431_GRUPO_EXAME_FISIC.</returns>
        public static TBS431_GRUPO_EXAME_FISIC SaveOrUpdate(TBS431_GRUPO_EXAME_FISIC entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS431_GRUPO_EXAME_FISIC de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS431_GRUPO_EXAME_FISIC.</returns>
        public static TBS431_GRUPO_EXAME_FISIC GetByEntityKey(EntityKey entityKey)
        {
            return (TBS431_GRUPO_EXAME_FISIC)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS431_GRUPO_EXAME_FISIC, ordenados pelo Id "CO_COL_APLIC".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB_EQUIPE_NUCLEO_INST de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS431_GRUPO_EXAME_FISIC> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS431_GRUPO_EXAME_FISIC.OrderBy(t => t.ID_GRUPO_FISIC).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB_EQUIPE_NUCLEO_INST onde o Id "TBS431_GRUPO_EXAME_FISIC" é o informado no parâmetro.
        /// </summary>
        /// <param name="CO_EQUIP_NUCLEO">Id da chave primária</param>
        /// <returns>Entidade TBS431_GRUPO_EXAME_FISIC</returns>
        public static TBS431_GRUPO_EXAME_FISIC RetornaPelaChavePrimaria(int _ID_GRUPO_FISIC)
        {
            return (from TBS431 in RetornaTodosRegistros()
                    where TBS431.ID_GRUPO_FISIC == _ID_GRUPO_FISIC
                    select TBS431).FirstOrDefault();
        }

        #endregion
    }
}
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
    public partial class TB902_DESEMP_MATRI
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
        /// Exclue o registro da tabela TB902_DESEMP_MATRI do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB902_DESEMP_MATRI entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB902_DESEMP_MATRI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB902_DESEMP_MATRI.</returns>
        public static TB902_DESEMP_MATRI Delete(TB902_DESEMP_MATRI entity)
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
        public static int SaveOrUpdate(TB902_DESEMP_MATRI entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB902_DESEMP_MATRI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB902_DESEMP_MATRI.</returns>
        public static TB902_DESEMP_MATRI SaveOrUpdate(TB902_DESEMP_MATRI entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB902_DESEMP_MATRI de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB902_DESEMP_MATRI.</returns>
        public static TB902_DESEMP_MATRI GetByEntityKey(EntityKey entityKey)
        {
            return (TB902_DESEMP_MATRI)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB902_DESEMP_MATRI, ordenados pelo Id "ID_DESEMP_MATRI".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB902_DESEMP_MATRI de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB902_DESEMP_MATRI> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB902_DESEMP_MATRI.OrderBy( d => d.ID_DESEMP_MATRI ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB902_DESEMP_MATRI pela chave primária "ID_DESEMP_MATRI".
        /// </summary>
        /// <param name="ID_DESEMP_MATRI">Id da chave primária</param>
        /// <returns>Entidade TB902_DESEMP_MATRI</returns>
        public static TB902_DESEMP_MATRI RetornaPelaChavePrimaria(int ID_DESEMP_MATRI)
        {
            return (from tb902 in RetornaTodosRegistros()
                    where tb902.ID_DESEMP_MATRI == ID_DESEMP_MATRI
                    select tb902).FirstOrDefault();
        }

        #endregion
    }
}
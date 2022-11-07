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
    public partial class VW_RESUM_FINAN_ATEND_COLAB
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
        /// Exclue o registro da tabela VW_RESUM_FINAN_ATEND_COLAB do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(VW_RESUM_FINAN_ATEND_COLAB entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela VW_RESUM_FINAN_ATEND_COLAB na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade VW_RESUM_FINAN_ATEND_COLAB.</returns>
        public static VW_RESUM_FINAN_ATEND_COLAB Delete(VW_RESUM_FINAN_ATEND_COLAB entity)
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
        public static int SaveOrUpdate(VW_RESUM_FINAN_ATEND_COLAB entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela VW_RESUM_FINAN_ATEND_COLAB na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade VW_RESUM_FINAN_ATEND_COLAB.</returns>
        public static VW_RESUM_FINAN_ATEND_COLAB SaveOrUpdate(VW_RESUM_FINAN_ATEND_COLAB entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade VW_RESUM_FINAN_ATEND_COLAB de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade VW_RESUM_FINAN_ATEND_COLAB.</returns>
        public static VW_RESUM_FINAN_ATEND_COLAB GetByEntityKey(EntityKey entityKey)
        {
            return (VW_RESUM_FINAN_ATEND_COLAB)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade VW_RESUM_FINAN_ATEND_COLAB, ordenados pelo Id da unidade "CO_EMP" e pela data de cadastro "DT_CAD_DOC".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade VW_RESUM_FINAN_ATEND_COLAB de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<VW_RESUM_FINAN_ATEND_COLAB> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.VW_RESUM_FINAN_ATEND_COLAB.AsObjectQuery();
        }

        #endregion
    }
}

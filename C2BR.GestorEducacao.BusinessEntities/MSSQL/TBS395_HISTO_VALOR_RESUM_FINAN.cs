//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
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
    public partial class TBS395_HISTO_VALOR_RESUM_FINAN
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
        /// Exclue o registro da tabela TBS395_HISTO_VALOR_RESUM_FINAN do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS395_HISTO_VALOR_RESUM_FINAN entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS395_HISTO_VALOR_RESUM_FINAN na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS395_HISTO_VALOR_RESUM_FINAN.</returns>
        public static TBS395_HISTO_VALOR_RESUM_FINAN Delete(TBS395_HISTO_VALOR_RESUM_FINAN entity)
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
        public static int SaveOrUpdate(TBS395_HISTO_VALOR_RESUM_FINAN entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS395_HISTO_VALOR_RESUM_FINAN na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS395_HISTO_VALOR_RESUM_FINAN.</returns>
        public static TBS395_HISTO_VALOR_RESUM_FINAN SaveOrUpdate(TBS395_HISTO_VALOR_RESUM_FINAN entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS395_HISTO_VALOR_RESUM_FINAN de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS395_HISTO_VALOR_RESUM_FINAN.</returns>
        public static TBS395_HISTO_VALOR_RESUM_FINAN GetByEntityKey(EntityKey entityKey)
        {
            return (TBS395_HISTO_VALOR_RESUM_FINAN)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS395_HISTO_VALOR_RESUM_FINAN, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS395_HISTO_VALOR_RESUM_FINAN de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS395_HISTO_VALOR_RESUM_FINAN> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS395_HISTO_VALOR_RESUM_FINAN.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS395_HISTO_VALOR_RESUM_FINAN pela chave primária "ID_HISTO_VALOR_RESUM_FINAN".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS395_HISTO_VALOR_RESUM_FINAN</returns>
        public static TBS395_HISTO_VALOR_RESUM_FINAN RetornaPelaChavePrimaria(int ID_HISTO_VALOR_RESUM_FINAN)
        {
            return (from tbs395 in RetornaTodosRegistros()
                    where tbs395.ID_HISTO_VALOR_RESUM_FINAN == ID_HISTO_VALOR_RESUM_FINAN
                    select tbs395).FirstOrDefault();
        }

        #endregion
    }
}

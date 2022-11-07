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
    public partial class TB111_PLANEJ_FINAN
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
        /// Exclue o registro da tabela TB111_PLANEJ_FINAN do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB111_PLANEJ_FINAN entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB111_PLANEJ_FINAN na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB111_PLANEJ_FINAN.</returns>
        public static TB111_PLANEJ_FINAN Delete(TB111_PLANEJ_FINAN entity)
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
        public static int SaveOrUpdate(TB111_PLANEJ_FINAN entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB111_PLANEJ_FINAN na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB111_PLANEJ_FINAN.</returns>
        public static TB111_PLANEJ_FINAN SaveOrUpdate(TB111_PLANEJ_FINAN entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB111_PLANEJ_FINAN de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB111_PLANEJ_FINAN.</returns>
        public static TB111_PLANEJ_FINAN GetByEntityKey(EntityKey entityKey)
        {
            return (TB111_PLANEJ_FINAN)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB111_PLANEJ_FINAN, ordenados pelo ano de referência "CO_ANO_REF".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB111_PLANCONTAB de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB111_PLANEJ_FINAN> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB111_PLANEJ_FINAN.OrderBy(p => p.CO_ANO_REF).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB111_PLANEJ_FINAN pela chave primária "ID_PLANEJ_FINAN"
        /// </summary>
        /// <param name="ID_PLANEJ_FINAN">Id da chave primária</param>
        /// <returns></returns>
        public static TB111_PLANEJ_FINAN RetornaPelaChavePrimaria(int ID_PLANEJ_FINAN)
        {
            return (from tb111 in RetornaTodosRegistros()
                    where tb111.ID_PLANEJ_FINAN == ID_PLANEJ_FINAN
                    select tb111).FirstOrDefault();
        }

        #endregion
    }
}
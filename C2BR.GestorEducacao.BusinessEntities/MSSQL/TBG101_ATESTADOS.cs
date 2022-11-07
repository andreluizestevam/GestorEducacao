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
    public partial class TBG101_ATESTADOS
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
        /// Exclue o registro da tabela TBG101_ATESTADOS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBG101_ATESTADOS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBG101_ATESTADOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBG101_ATESTADOS.</returns>
        public static TBG101_ATESTADOS Delete(TBG101_ATESTADOS entity)
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
        public static int SaveOrUpdate(TBG101_ATESTADOS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBG101_ATESTADOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB232_PATRI_CARGA_ITEM.</returns>
        public static TBG101_ATESTADOS SaveOrUpdate(TBG101_ATESTADOS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBG101_ATESTADOS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBG101_ATESTADOS.</returns>
        public static TBG101_ATESTADOS GetByEntityKey(EntityKey entityKey)
        {
            return (TBG101_ATESTADOS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBG101_ATESTADOS.
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBG101_ATESTADOS.</returns>
        public static ObjectQuery<TBG101_ATESTADOS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBG101_ATESTADOS.AsObjectQuery();
        }
        /// <summary>
        /// Retorna um registro da entidade TBG101_ATESTADOS pela chave primária "ID_ATEST".
        /// </summary>
        /// <param name="ID_ATEST">Id da chave primária</param>
        /// <returns>Entidade TBG101_ATESTADOS</returns>
        public static TBG101_ATESTADOS RetornaPelaChavePrimaria(decimal ID_ATEST)
        {
            return (from tbg101 in RetornaTodosRegistros()
                    where tbg101.ID_ATEST == ID_ATEST
                    select tbg101).FirstOrDefault();
        }
        /// <summary>
        /// Retorna um registro da entidade TBG101_ATESTADOS pela fk "ID_FREQ_ALUNO".
        /// </summary>
        /// <param name="ID_FREQ_ALUNO">Id da chave primária</param>
        /// <returns>Entidade TBG101_ATESTADOS</returns>
        public static TBG101_ATESTADOS RetornaPelaFreq(decimal ID_FREQ_ALUNO)
        {
            return (from tbg101 in RetornaTodosRegistros()
                    where tbg101.TB132_FREQ_ALU.ID_FREQ_ALUNO == ID_FREQ_ALUNO
                    select tbg101).FirstOrDefault();
        }
        #endregion

        #endregion
    }
}
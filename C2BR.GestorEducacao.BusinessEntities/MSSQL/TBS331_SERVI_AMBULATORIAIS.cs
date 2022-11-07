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
    public partial class TBS331_SERVI_AMBULATORIAIS
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
        /// Exclue o registro da tabela TBE222_PAGTO_CHEQUE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS331_SERVI_AMBULATORIAIS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS194_PRE_ATEND na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBE222_PAGTO_CHEQUE.</returns>
        public static TBS331_SERVI_AMBULATORIAIS Delete(TBS331_SERVI_AMBULATORIAIS entity)
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
        public static int SaveOrUpdate(TBS331_SERVI_AMBULATORIAIS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS194_PRE_ATEND na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBE222_PAGTO_CHEQUE.</returns>
        public static TBS331_SERVI_AMBULATORIAIS SaveOrUpdate(TBS331_SERVI_AMBULATORIAIS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS331_SERVI_AMBULATORIAIS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS331_SERVI_AMBULATORIAIS.</returns>
        public static TBS331_SERVI_AMBULATORIAIS GetByEntityKey(EntityKey entityKey)
        {
            return (TBS331_SERVI_AMBULATORIAIS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS331_SERVI_AMBULATORIAIS, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS194_PRE_ATEND de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS331_SERVI_AMBULATORIAIS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS331_SERVI_AMBULATORIAIS.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS194_PRE_ATEND pela chave primária "ID_SERVI_AMBUL".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS331_SERVI_AMBULATORIAIS</returns>
        public static TBS331_SERVI_AMBULATORIAIS RetornaPelaChavePrimaria(int ID_SERVI_AMBUL)
        {
            return (from tbs331 in RetornaTodosRegistros()
                    where tbs331.ID_SERVI_AMBUL == ID_SERVI_AMBUL
                    select tbs331).FirstOrDefault();
        }

        #endregion
    }
}

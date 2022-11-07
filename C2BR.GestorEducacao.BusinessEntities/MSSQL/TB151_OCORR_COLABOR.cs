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
    public partial class TB151_OCORR_COLABOR
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
        /// Exclue o registro da tabela TB151_OCORR_COLABOR do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB151_OCORR_COLABOR entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB151_OCORR_COLABOR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB151_OCORR_COLABOR.</returns>
        public static TB151_OCORR_COLABOR Delete(TB151_OCORR_COLABOR entity)
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
        public static int SaveOrUpdate(TB151_OCORR_COLABOR entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB151_OCORR_COLABOR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB151_OCORR_COLABOR.</returns>
        public static TB151_OCORR_COLABOR SaveOrUpdate(TB151_OCORR_COLABOR entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB151_OCORR_COLABOR de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB151_OCORR_COLABOR.</returns>
        public static TB151_OCORR_COLABOR GetByEntityKey(EntityKey entityKey)
        {
            return (TB151_OCORR_COLABOR)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB151_OCORR_COLABOR, ordenados pelo Id do funcionário "TB03_COLABOR.CO_COL".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB151_OCORR_COLABOR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB151_OCORR_COLABOR> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB151_OCORR_COLABOR.OrderBy( o => o.TB03_COLABOR.CO_COL ).AsObjectQuery();
        }
        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB151_OCORR_COLABOR pela chave primária "IDE_OCORR_COLAB".
        /// </summary>
        /// <param name="IDE_OCORR_COLAB">Id da chave primária</param>
        /// <returns>Entidade TB151_OCORR_COLABOR</returns>
        public static TB151_OCORR_COLABOR RetornaPelaChavePrimaria(int IDE_OCORR_COLAB)
        {
            return (from tb151 in RetornaTodosRegistros()
                    where tb151.IDE_OCORR_COLAB == IDE_OCORR_COLAB
                    select tb151).FirstOrDefault();
        }

        #endregion
    }
}

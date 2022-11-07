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
    public partial class TB285_HIST_TRANS_ALUNO
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
        /// Exclue o registro da tabela TB285_HIST_TRANS_ALUNO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB285_HIST_TRANS_ALUNO entity, bool saveChanges) 
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB285_HIST_TRANS_ALUNO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB285_HIST_TRANS_ALUNO.</returns>
        public static TB285_HIST_TRANS_ALUNO Delete(TB285_HIST_TRANS_ALUNO entity) 
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
        public static int SaveOrUpdate(TB285_HIST_TRANS_ALUNO entity, bool saveChanges) 
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB285_HIST_TRANS_ALUNO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB285_HIST_TRANS_ALUNO.</returns>
        public static TB285_HIST_TRANS_ALUNO SaveOrUpdate(TB285_HIST_TRANS_ALUNO entity) 
        {
            SaveOrUpdate(entity, true);
            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB285_HIST_TRANS_ALUNO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB285_HIST_TRANS_ALUNO.</returns>
        public static TB285_HIST_TRANS_ALUNO GetByEntityKey(EntityKey entityKey)
        {
            return (TB285_HIST_TRANS_ALUNO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB285_HIST_TRANS_ALUNO, ordenados pelo Id "ID_HIST_TRANS".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB285_HIST_TRANS_ALUNO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB285_HIST_TRANS_ALUNO> RetornaTodosRegistros() 
        {
            return GestorEntities.CurrentContext.TB285_HIST_TRANS_ALUNO.OrderBy( h => h.ID_HIST_TRANS ).AsObjectQuery();
        }
        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB285_HIST_TRANS_ALUNO pela chave primária "ID_HIST_TRANS".
        /// </summary>
        /// <param name="ID_HIST_TRANS">Id da chave primária</param>
        /// <returns>Entidade TB285_HIST_TRANS_ALUNO</returns>
        public static TB285_HIST_TRANS_ALUNO RetornaPelaChavePrimaria(int ID_HIST_TRANS)
        {
            return (from tb285 in RetornaTodosRegistros()
                    where tb285.ID_HIST_TRANS == ID_HIST_TRANS
                    select tb285).FirstOrDefault();
        }
        #endregion
    }
}

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
    public partial class TB126_HIST_ALUNO_AVAL
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
        /// Exclue o registro da tabela TB126_HIST_ALUNO_AVAL do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB126_HIST_ALUNO_AVAL entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB126_HIST_ALUNO_AVAL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB126_HIST_ALUNO_AVAL.</returns>
        public static TB126_HIST_ALUNO_AVAL Delete(TB126_HIST_ALUNO_AVAL entity)
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
        public static int SaveOrUpdate(TB126_HIST_ALUNO_AVAL entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB126_HIST_ALUNO_AVAL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB126_HIST_ALUNO_AVAL.</returns>
        public static TB126_HIST_ALUNO_AVAL SaveOrUpdate(TB126_HIST_ALUNO_AVAL entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB126_HIST_ALUNO_AVAL de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB126_HIST_ALUNO_AVAL.</returns>
        public static TB126_HIST_ALUNO_AVAL GetByEntityKey(EntityKey entityKey)
        {
            return (TB126_HIST_ALUNO_AVAL)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB126_HIST_ALUNO_AVAL, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB126_HIST_ALUNO_AVAL de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB126_HIST_ALUNO_AVAL> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB126_HIST_ALUNO_AVAL.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TB126_HIST_ALUNO_AVAL pela chave primária "ID_HIST_AVAL".
        /// </summary>
        /// <param name="CO_CIDADE">Id da chave primária</param>
        public static TB126_HIST_ALUNO_AVAL RetornaPelaChavePrimaria(int ID_HIST_AVAL)
        {
            return (from tb126 in RetornaTodosRegistros()
                    where tb126.ID_HIST_AVAL == ID_HIST_AVAL
                    select tb126).FirstOrDefault();
        }


        #endregion

    }
}

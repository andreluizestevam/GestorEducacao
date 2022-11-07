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
    public partial class TB242_ALUNO_TELEFONE
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
        /// Exclue o registro da tabela TB242_ALUNO_TELEFONE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB242_ALUNO_TELEFONE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB242_ALUNO_TELEFONE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB242_ALUNO_TELEFONE.</returns>
        public static TB242_ALUNO_TELEFONE Delete(TB242_ALUNO_TELEFONE entity)
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
        public static int SaveOrUpdate(TB242_ALUNO_TELEFONE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB242_ALUNO_TELEFONE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB242_ALUNO_TELEFONE.</returns>
        public static TB242_ALUNO_TELEFONE SaveOrUpdate(TB242_ALUNO_TELEFONE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB242_ALUNO_TELEFONE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB242_ALUNO_TELEFONE.</returns>
        public static TB242_ALUNO_TELEFONE GetByEntityKey(EntityKey entityKey)
        {
            return (TB242_ALUNO_TELEFONE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB242_ALUNO_TELEFONE, ordenados pelo DDD "NR_DDD" e pelo telefone "NR_TELEFONE".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB242_ALUNO_TELEFONE de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB242_ALUNO_TELEFONE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB242_ALUNO_TELEFONE.OrderBy( a => a.NR_DDD ).ThenBy( a => a.NR_TELEFONE ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB242_ALUNO_TELEFONE pela chave primária "ID_ALUNO_TELEFONE".
        /// </summary>
        /// <param name="ID_ALUNO_TELEFONE">Id da chave primária</param>
        /// <returns>Entidade TB242_ALUNO_TELEFONE</returns>
        public static TB242_ALUNO_TELEFONE RetornaPelaChavePrimaria(int ID_ALUNO_TELEFONE)
        {
            return (from tb242 in RetornaTodosRegistros()
                    where tb242.ID_ALUNO_TELEFONE == ID_ALUNO_TELEFONE
                    select tb242).FirstOrDefault();
        }

        #endregion
    }
}
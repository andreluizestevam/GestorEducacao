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
    public partial class TB241_ALUNO_ENDERECO
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
        /// Exclue o registro da tabela TB241_ALUNO_ENDERECO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB241_ALUNO_ENDERECO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB241_ALUNO_ENDERECO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB241_ALUNO_ENDERECO.</returns>
        public static TB241_ALUNO_ENDERECO Delete(TB241_ALUNO_ENDERECO entity)
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
        public static int SaveOrUpdate(TB241_ALUNO_ENDERECO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB241_ALUNO_ENDERECO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB241_ALUNO_ENDERECO.</returns>
        public static TB241_ALUNO_ENDERECO SaveOrUpdate(TB241_ALUNO_ENDERECO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB241_ALUNO_ENDERECO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB241_ALUNO_ENDERECO.</returns>
        public static TB241_ALUNO_ENDERECO GetByEntityKey(EntityKey entityKey)
        {
            return (TB241_ALUNO_ENDERECO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB241_ALUNO_ENDERECO, ordenados pelo Id "ID_ALUNO_ENDERECO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB241_ALUNO_ENDERECO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB241_ALUNO_ENDERECO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB241_ALUNO_ENDERECO.OrderBy( a => a.ID_ALUNO_ENDERECO ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB241_ALUNO_ENDERECO pela chave primária "ID_ALUNO_ENDERECO".
        /// </summary>
        /// <param name="ID_ALUNO_ENDERECO">Id da chave primária</param>
        /// <returns>Entidade TB241_ALUNO_ENDERECO</returns>
        public static TB241_ALUNO_ENDERECO RetornaPelaChavePrimaria(int ID_ALUNO_ENDERECO)
        {
            return (from tb241 in RetornaTodosRegistros()
                    where tb241.ID_ALUNO_ENDERECO == ID_ALUNO_ENDERECO
                    select tb241).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB241_ALUNO_ENDERECO de acordo com o aluno informado.
        /// </summary>
        /// <param name="CO_ALU">Id do aluno</param>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB241_ALUNO_ENDERECO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB241_ALUNO_ENDERECO> RetornaPeloAluno(int CO_ALU, int CO_EMP)
        {
            return (from tb241 in RetornaTodosRegistros()
                    where tb241.TB07_ALUNO.CO_ALU == CO_ALU && tb241.TB07_ALUNO.CO_EMP == CO_EMP
                    select tb241).AsObjectQuery();
        }

        #endregion
    }
}
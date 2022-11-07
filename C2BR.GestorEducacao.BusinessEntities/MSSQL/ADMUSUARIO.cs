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
    public partial class ADMUSUARIO
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
        /// Exclue o registro da tabela ADMUSUARIO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(ADMUSUARIO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela ADMUSUARIO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade ADMUSUARIO.</returns>
        public static ADMUSUARIO Delete(ADMUSUARIO entity)
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
        public static int SaveOrUpdate(ADMUSUARIO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela ADMUSUARIO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade ADMUSUARIO.</returns>
        public static ADMUSUARIO SaveOrUpdate(ADMUSUARIO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade ADMUSUARIO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade ADMUSUARIO.</returns>
        public static ADMUSUARIO GetByEntityKey(EntityKey entityKey)
        {
            return (ADMUSUARIO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade ADMUSUARIO, ordenados pelo Id "ideAdmUsuario".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade ADMUSUARIO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<ADMUSUARIO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.ADMUSUARIO.OrderBy( a => a.ideAdmUsuario ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade ADMUSUARIO onde o Id do usuário "ideAdmUsuario" é o informado no parâmetro.
        /// </summary>
        /// <param name="ideAdmUsuario">Id da chave primária</param>
        /// <returns>Entidade ADMUSUARIO</returns>
        public static ADMUSUARIO RetornaPelaChavePrimaria(int ideAdmUsuario)
        {
            return (from admUsu in RetornaTodosRegistros()
                    where (admUsu.ideAdmUsuario == ideAdmUsuario)
                    select admUsu).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade ADMUSUARIO onde o login "desLogin" é o informado no parâmetro.
        /// </summary>
        /// <param name="desLogin">Login do usuário informado</param>
        /// <returns>Entidade ADMUSUARIO</returns>
        public static ADMUSUARIO RetornaPeloNomeUsuario(string desLogin)
        {
            return (from admUsu in RetornaTodosRegistros()
                    where admUsu.desLogin == desLogin
                    select admUsu).FirstOrDefault();
        }

        /// <summary>
        /// Retorna um registro da entidade ADMUSUARIO onde o Id da unidade "CO_EMP" e o Id do usuário "CodUsuario" é o informado no parâmetro.
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CodUsuario">Id do usuário</param>
        /// <returns>Entidade ADMUSUARIO</returns>
        public static ADMUSUARIO RetornaPelaUnidColabor(int CO_EMP, int CodUsuario)
        {
            return (from admUsu in ADMUSUARIO.RetornaTodosRegistros()
                    where admUsu.CO_EMP == CO_EMP
                    && admUsu.CodUsuario == CodUsuario
                    select admUsu).FirstOrDefault();
        }

        /// <summary>
        /// Retorna um registro da entidade ADMUSUARIO onde o Id da unidade "CO_EMP" e o Id do usuário "CodUsuario" é o informado no parâmetro.
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CodUsuario">Id do usuário</param>
        /// <returns>Entidade ADMUSUARIO</returns>
        public static ADMUSUARIO RetornaPeloCodUsuario(int CodUsuario)
        {
            return GestorEntities.CurrentContext.ADMUSUARIO.FirstOrDefault(p => p.CodUsuario == CodUsuario);
        }
        #endregion
    }
}

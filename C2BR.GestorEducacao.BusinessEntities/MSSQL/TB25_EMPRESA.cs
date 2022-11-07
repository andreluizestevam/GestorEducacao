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
    public partial class TB25_EMPRESA
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
        /// Exclue o registro da tabela TB25_EMPRESA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB25_EMPRESA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB25_EMPRESA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB25_EMPRESA.</returns>
        public static TB25_EMPRESA Delete(TB25_EMPRESA entity)
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
        public static int SaveOrUpdate(TB25_EMPRESA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB25_EMPRESA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB25_EMPRESA.</returns>
        public static TB25_EMPRESA SaveOrUpdate(TB25_EMPRESA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB25_EMPRESA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB25_EMPRESA.</returns>
        public static TB25_EMPRESA GetByEntityKey(EntityKey entityKey)
        {
            return (TB25_EMPRESA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB25_EMPRESA, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB25_EMPRESA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB25_EMPRESA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB25_EMPRESA.OrderBy( e => e.NO_FANTAS_EMP ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB25_EMPRESA pela chave primária "CO_EMP".
        /// </summary>
        /// <param name="CO_EMP">Id da chave primária</param>
        /// <returns>Entidade TB25_EMPRESA</returns>
        public static TB25_EMPRESA RetornaPelaChavePrimaria(int CO_EMP)
        {
            return (from tb25 in RetornaTodosRegistros()
                    where tb25.CO_EMP == CO_EMP
                    select tb25).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB25_EMPRESA de acordo com o usuário informado "CodUsuario".
        /// </summary>
        /// <param name="CodUsuario">Código do usuário</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB25_EMPRESA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB25_EMPRESA> RetornaPeloUsuario(int CodUsuario)
        {
            return (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                    join tb134 in C2BR.GestorEducacao.BusinessEntities.MSSQL.TB134_USR_EMP.RetornaTodosRegistros()
                        on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                    where tb134.ADMUSUARIO.CodUsuario == CodUsuario
                    select tb25).OrderBy( e => e.NO_FANTAS_EMP ).AsObjectQuery();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB25_EMPRESA de acordo com o id usuário informado "ideAdmUsuario".
        /// </summary>
        /// <param name="ideAdmUsuario">Id do usuário</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB25_EMPRESA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB25_EMPRESA> RetornaPeloIDeAdmUser(int ideAdmUsuario)
        {
            return (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                    join tb134 in C2BR.GestorEducacao.BusinessEntities.MSSQL.TB134_USR_EMP.RetornaTodosRegistros()
                        on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                    where tb134.ADMUSUARIO.ideAdmUsuario == ideAdmUsuario
                    select tb25).OrderBy( e => e.NO_FANTAS_EMP ).AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da tabela TB25_EMPRESA
        /// </summary>
        public static String RetornaUltimoNumContrato(int CO_EMP)
        {
            var res = RetornaPelaChavePrimaria(CO_EMP);

            return res != null && !String.IsNullOrEmpty(res.NU_ULTIMO_CONTRATO) ? res.NU_ULTIMO_CONTRATO : "000000";
        }

        #endregion
    }
}
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
    public partial class ADMMODULO
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
        /// Exclue o registro da tabela ADMMODULO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(ADMMODULO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela ADMMODULO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade ADMMODULO.</returns>
        public static ADMMODULO Delete(ADMMODULO entity)
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
        public static int SaveOrUpdate(ADMMODULO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela ADMMODULO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade ADMMODULO.</returns>
        public static ADMMODULO SaveOrUpdate(ADMMODULO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade ADMMODULO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade ADMMODULO.</returns>
        public static ADMMODULO GetByEntityKey(EntityKey entityKey)
        {   
            return (ADMMODULO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registros da entidade ADMMODULO onde o status "flaStatus" é "A"tivo e ordenado pelo número de ordem "numOrdemMenu".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade ADMMODULO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<ADMMODULO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.ADMMODULO.Where( a => a.flaStatus.Equals("A") ).OrderBy( a => a.numOrdemMenu ).AsObjectQuery();
        }
        public static ObjectQuery<ADMMODULO> RetornaRegistrosPorTexto(string texto)
        {
            return GestorEntities.CurrentContext.ADMMODULO.Where(a => a.nomModulo.Contains(texto)).OrderBy(a => a.numOrdemMenu).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna uma lista de todos os registros da entidade ADMMODULO onde o id do módulo pai é igual ao parâmetro informado.
        /// </summary>
        /// <param name="ideModuloPai">Id do módulo pai</param>
        /// <returns>Uma lista dos registros da entidade ADMMODULO de acordo com a filtragem desenvolvida.</returns>
        public static List<ADMMODULO> RetornaPeloIdeModuloPai(int ideModuloPai)
        {
            if (ideModuloPai == 0)
            {
                return (from admModulo in RetornaTodosRegistros()
                        where admModulo.ADMMODULO2.ideAdmModulo == null
                        select admModulo).OrderBy( a => a.numOrdemMenu ).ToList();
            }
            else
            {
                return (from admModulo in RetornaTodosRegistros()
                        where admModulo.ADMMODULO2.ideAdmModulo == ideModuloPai
                        select admModulo).OrderBy(a => a.numOrdemMenu).ToList();
            }
        }

        /// <summary>
        /// Retorna todos os registro da entidade ADMMODULO onde o ideModuloPai é nulo ("Areas de Conhecimento").
        /// </summary>
        /// <param name="ORG_CODIGO_ORGAO">Id da instituição</param>
        /// <param name="CO_EMP">Id da unidade selecionada</param>
        /// <param name="ideAdmUsuario">Id do usuário informado</param>
        /// <returns>ObjectQuery com todos os registros da entidade ADMMODULO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<ADMMODULO> RetornaAreasConhecimento(int ORG_CODIGO_ORGAO, int CO_EMP, int ideAdmUsuario)
        {
            ObjectQuery<ADMMODULO> resultado = (from admModulo in RetornaUsuarioModulos(ORG_CODIGO_ORGAO, CO_EMP, ideAdmUsuario)
                                                where admModulo.ADMMODULO2 == null
                                                select admModulo).OrderBy(a => a.numOrdemMenu).AsObjectQuery();

            return resultado;
        }

        /// <summary>
        /// Retorna todos os registro da entidade ADMMODULO onde o ideModuloPai é igual ao Id informado ("Módulos").
        /// </summary>
        /// <param name="ORG_CODIGO_ORGAO">Id da instituição</param>
        /// <param name="CO_EMP">Id da unidade selecionada</param>
        /// <param name="ideAdmUsuario">Id do usuário informado</param>
        /// <param name="ideAdmModuloPai">Id do módulo pai</param>
        /// <returns>ObjectQuery com todos os registros da entidade ADMMODULO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<ADMMODULO> RetornaUsuarioModulos(int ORG_CODIGO_ORGAO, int CO_EMP, int ideAdmUsuario, int ideAdmModuloPai)
        {
            ObjectQuery<ADMMODULO> resultado = (from admModulo in RetornaUsuarioModulos(ORG_CODIGO_ORGAO, CO_EMP, ideAdmUsuario)
                                                where admModulo.ADMMODULO2.ideAdmModulo == ideAdmModuloPai
                                                select admModulo).AsObjectQuery();

            return resultado;
        }

        public static ObjectQuery<ADMMODULO> RetornaUsuarioModulos(string nomemodulo)
        {
            ObjectQuery<ADMMODULO> resultado = (from admModulo in RetornaRegistrosPorTexto(nomemodulo)                                                 
                                                select admModulo).AsObjectQuery();

            return resultado;
        }
        /// <summary>
        /// Retorna todos os registros da entidade ADMMODULO independente do status "flaStatus" e ordenado pelo nome do módulo "nomModulo".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade ADMMODULO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<ADMMODULO> RetornaTodosRegistrosStatus()
        {
            return GestorEntities.CurrentContext.ADMMODULO.OrderBy( a => a.nomModulo ).AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade ADMMODULO pela chave primária "ideAdmModulo".
        /// </summary>
        /// <param name="ideAdmModulo">Id da chave primária</param>
        /// <returns>Entidade ADMMODULO</returns>
        public static ADMMODULO RetornaPelaChavePrimaria(int ideAdmModulo)
        {
            return (from admModulo in RetornaTodosRegistros()
                    where admModulo.ideAdmModulo == ideAdmModulo
                    select admModulo).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registros da entidade ADMMODULO de acordo com o usuário informado fazendo a verificação do perfil do mesmo.
        /// </summary>
        /// <param name="ORG_CODIGO_ORGAO">Id da instituição</param>
        /// <param name="CO_EMP">Id da unidade selecionada</param>
        /// <param name="ideAdmUsuario">Id do usuário informado</param>
        /// <returns>ObjectQuery com todos os registros da entidade ADMMODULO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<ADMMODULO> RetornaUsuarioModulos(int ORG_CODIGO_ORGAO, int CO_EMP, int ideAdmUsuario)
        {
            ObjectQuery<ADMMODULO> modulosUsuario = null;

            TB134_USR_EMP tb134 = (from lTb134 in TB134_USR_EMP.RetornaTodosRegistros().Include(typeof(AdmPerfilAcesso).Name)
                                   where lTb134.AdmPerfilAcesso.ORG_CODIGO_ORGAO.Equals(ORG_CODIGO_ORGAO)
                                   && lTb134.TB25_EMPRESA.CO_EMP.Equals(CO_EMP) && lTb134.ADMUSUARIO.ideAdmUsuario.Equals(ideAdmUsuario)
                                   select lTb134).FirstOrDefault();

            if (tb134 != null)
            {
                var resultado = (from admModulo in (from admPerfilMod in GestorEntities.CurrentContext.AdmPerfilModulo
                                                   where admPerfilMod.idePerfilAcesso.Equals(tb134.AdmPerfilAcesso.idePerfilAcesso)
                                                   && admPerfilMod.ORG_CODIGO_ORGAO.Equals(tb134.AdmPerfilAcesso.ORG_CODIGO_ORGAO)
                                                   && admPerfilMod.ADMMODULO.flaStatus.Equals("A")
                                                   select new { admPerfilMod.ADMMODULO }.ADMMODULO)
                                select admModulo).AsObjectQuery();

                if (resultado != null)
                    modulosUsuario = resultado;
            }

            return modulosUsuario;
        }        
        #endregion

        #region Estrutura

        public struct stADMMODULO
        {
            public int MOD_CODIGO;

            public string MOD_TITULO;

            public string MOD_DESCRICAO;

            public string MOD_ICONE;

            public string MOD_TIPO_ITEM_SUBMEN;

            public string MOD_MODULO_REFERENCIA;
        }
        #endregion
    }
}

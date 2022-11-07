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
    public partial class AdmPerfilModulo
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
        /// Exclue o registro da tabela AdmPerfilModulo do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(AdmPerfilModulo entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela AdmPerfilModulo na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade AdmPerfilModulo.</returns>
        public static AdmPerfilModulo Delete(AdmPerfilModulo entity)
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
        public static int SaveOrUpdate(AdmPerfilModulo entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela AdmPerfilModulo na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade AdmPerfilModulo.</returns>
        public static AdmPerfilModulo SaveOrUpdate(AdmPerfilModulo entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade AdmPerfilModulo de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade AdmPerfilModulo.</returns>
        public static AdmPerfilModulo GetByEntityKey(EntityKey entityKey)
        {
            return (AdmPerfilModulo)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade AdmPerfilModulo, ordenados pelo Id do perfil "idePerfilAcesso".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade AdmPerfilModulo de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<AdmPerfilModulo> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.AdmPerfilModulo.OrderBy(a => a.idePerfilAcesso).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade AdmPerfilModulo onde o Id do perfil "idePerfilAcesso", o Id do módulo "ideAdmModulo" e o Id da instituição "ORG_CODIGO_ORGAO" é o informado no parâmetro.
        /// </summary>
        /// <param name="idePerfilAcesso">Id do perfil selecionado</param>
        /// <param name="ideAdmModulo">Id do módulo</param>
        /// <param name="ORG_CODIGO_ORGAO">Id da instituição</param>
        /// <returns>Entidade AdmPerfilModulo</returns>
        public static AdmPerfilModulo RetornaPeloPerfilModulo(int idePerfilAcesso, int ideAdmModulo, int ORG_CODIGO_ORGAO)
        {
            return (from admPerfMod in RetornaTodosRegistros()
                    where admPerfMod.idePerfilAcesso == idePerfilAcesso
                    && admPerfMod.ORG_CODIGO_ORGAO == ORG_CODIGO_ORGAO
                    && admPerfMod.ideAdmModulo == ideAdmModulo
                    select admPerfMod).FirstOrDefault();
        }

        #endregion
    }
}

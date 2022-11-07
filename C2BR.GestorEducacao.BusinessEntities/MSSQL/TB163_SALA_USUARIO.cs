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
    public partial class TB163_SALA_USUARIO
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
        /// Exclue o registro da tabela TB163_SALA_USUARIO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB163_SALA_USUARIO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB163_SALA_USUARIO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB163_SALA_USUARIO.</returns>
        public static TB163_SALA_USUARIO Delete(TB163_SALA_USUARIO entity)
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
        public static int SaveOrUpdate(TB163_SALA_USUARIO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB163_SALA_USUARIO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB163_SALA_USUARIO.</returns>
        public static TB163_SALA_USUARIO SaveOrUpdate(TB163_SALA_USUARIO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB163_SALA_USUARIO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB163_SALA_USUARIO.</returns>
        public static TB163_SALA_USUARIO GetByEntityKey(EntityKey entityKey)
        {
            return (TB163_SALA_USUARIO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB163_SALA_USUARIO, ordenados pelo nome "NO_DEPTO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB163_SALA_USUARIO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB163_SALA_USUARIO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB163_SALA_USUARIO.AsObjectQuery();
        }

        public static TB163_SALA_USUARIO RetornaPelaChavePrimaria(int CO_SALA, int ideAdmUsuario)
        {
            return (from tb163 in RetornaTodosRegistros()
                        where tb163.CO_SALA == CO_SALA
                        && tb163.ideAdmUsuario == ideAdmUsuario
                        select tb163).FirstOrDefault();
        }

        #endregion

    }
}

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
    public partial class TB246_UNIDADE_PERFIL_VAGAS
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
        /// Exclue o registro da tabela TB246_UNIDADE_PERFIL_VAGAS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB246_UNIDADE_PERFIL_VAGAS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB246_UNIDADE_PERFIL_VAGAS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB246_UNIDADE_PERFIL_VAGAS.</returns>
        public static TB246_UNIDADE_PERFIL_VAGAS Delete(TB246_UNIDADE_PERFIL_VAGAS entity)
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
        public static int SaveOrUpdate(TB246_UNIDADE_PERFIL_VAGAS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB246_UNIDADE_PERFIL_VAGAS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB246_UNIDADE_PERFIL_VAGAS.</returns>
        public static TB246_UNIDADE_PERFIL_VAGAS SaveOrUpdate(TB246_UNIDADE_PERFIL_VAGAS entity)
        {
            SaveOrUpdate(entity, true);
            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB246_UNIDADE_PERFIL_VAGAS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB246_UNIDADE_PERFIL_VAGAS.</returns>
        public static TB246_UNIDADE_PERFIL_VAGAS GetByEntityKey(EntityKey entityKey)
        {
            return (TB246_UNIDADE_PERFIL_VAGAS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB246_UNIDADE_PERFIL_VAGAS, ordenados pelo ano "CO_ANO_PERFIL_VAGAS".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB246_UNIDADE_PERFIL_VAGAS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB246_UNIDADE_PERFIL_VAGAS> RetornaTodosRegistros() 
        {
            return GestorEntities.CurrentContext.TB246_UNIDADE_PERFIL_VAGAS.OrderBy( u => u.CO_ANO_PERFIL_VAGAS ).AsObjectQuery();
        }
        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB246_UNIDADE_PERFIL_VAGAS pela chave primária "ID_UNIDADE_PERFIL_VAGAS".
        /// </summary>
        /// <param name="ID_UNIDADE_PERFIL_VAGAS">Id da chave primária</param>
        /// <returns>Entidade TB246_UNIDADE_PERFIL_VAGAS</returns>
        public static TB246_UNIDADE_PERFIL_VAGAS RetornaPelaChavePrimaria(int ID_UNIDADE_PERFIL_VAGAS) 
        {
            return (from tb246 in RetornaTodosRegistros()
                    where tb246.ID_UNIDADE_PERFIL_VAGAS == ID_UNIDADE_PERFIL_VAGAS
                    select tb246).FirstOrDefault();
        }
        #endregion
    }
}

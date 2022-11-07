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
    public partial class TB052_RESERV_MATRI
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
        /// Exclue o registro da tabela TB052_RESERV_MATRI do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB052_RESERV_MATRI entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB052_RESERV_MATRI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB052_RESERV_MATRI.</returns>
        public static TB052_RESERV_MATRI Delete(TB052_RESERV_MATRI entity)
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
        public static int SaveOrUpdate(TB052_RESERV_MATRI entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB052_RESERV_MATRI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB052_RESERV_MATRI.</returns>
        public static TB052_RESERV_MATRI SaveOrUpdate(TB052_RESERV_MATRI entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB052_RESERV_MATRI de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB052_RESERV_MATRI.</returns>
        public static TB052_RESERV_MATRI GetByEntityKey(EntityKey entityKey)
        {
            return (TB052_RESERV_MATRI)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB052_RESERV_MATRI, ordenados pelo número da reserva "NU_RESERVA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB052_RESERV_MATRI de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB052_RESERV_MATRI> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB052_RESERV_MATRI.OrderBy( r => r.NU_RESERVA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB052_RESERV_MATRI pelas chaves primárias "NU_RESERVA", "CO_EMP_CADASTRO".
        /// </summary>
        /// <param name="NU_RESERVA">Número da reserva</param>
        /// <param name="CO_EMP_CADASTRO">Id da unidade de cadastro</param>
        /// <returns>Entidade TB052_RESERV_MATRI</returns>
        public static TB052_RESERV_MATRI RetornaPelaChavePrimaria(string NU_RESERVA, int CO_EMP_CADASTRO)
        {
            return (from tb052 in RetornaTodosRegistros()
                    where tb052.NU_RESERVA == NU_RESERVA && tb052.CO_EMP_CADASTRO == CO_EMP_CADASTRO
                    select tb052).FirstOrDefault();
        }

        #endregion
    }
}
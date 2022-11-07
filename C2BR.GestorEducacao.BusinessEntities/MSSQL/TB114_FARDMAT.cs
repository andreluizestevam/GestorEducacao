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
    public partial class TB114_FARDMAT
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
        /// Exclue o registro da tabela TB114_FARDMAT do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB114_FARDMAT entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB114_FARDMAT na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB114_FARDMAT.</returns>
        public static TB114_FARDMAT Delete(TB114_FARDMAT entity)
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
        public static int SaveOrUpdate(TB114_FARDMAT entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB114_FARDMAT na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB114_FARDMAT.</returns>
        public static TB114_FARDMAT SaveOrUpdate(TB114_FARDMAT entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB114_FARDMAT de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB114_FARDMAT.</returns>
        public static TB114_FARDMAT GetByEntityKey(EntityKey entityKey)
        {
            return (TB114_FARDMAT)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB114_FARDMAT, ordenados pelo id "ID_FARDMAT".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB114_FARDMAT de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB114_FARDMAT> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB114_FARDMAT.OrderBy( f => f.ID_FARDMAT ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB114_FARDMAT pelas chaves primárias "ID_FARDMAT".
        /// </summary>
        /// <param name="ID_FARDMAT">chave primária da tabela</param>
        /// <returns>Entidade TB114_FARDMAT</returns>
        public static TB114_FARDMAT RetornaPelaChavePrimaria(int ID_FARDMAT)
        {
            return (from tb114 in RetornaTodosRegistros()
                    where tb114.ID_FARDMAT == ID_FARDMAT
                    select tb114).FirstOrDefault();
        }

        #endregion
    }
}
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
    public partial class TB207_RESERVA_ITENS_BIBLIOTECA
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
        /// Exclue o registro da tabela TB207_RESERVA_ITENS_BIBLIOTECA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB207_RESERVA_ITENS_BIBLIOTECA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB207_RESERVA_ITENS_BIBLIOTECA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB207_RESERVA_ITENS_BIBLIOTECA.</returns>
        public static TB207_RESERVA_ITENS_BIBLIOTECA Delete(TB207_RESERVA_ITENS_BIBLIOTECA entity)
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
        public static int SaveOrUpdate(TB207_RESERVA_ITENS_BIBLIOTECA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB207_RESERVA_ITENS_BIBLIOTECA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB207_RESERVA_ITENS_BIBLIOTECA.</returns>
        public static TB207_RESERVA_ITENS_BIBLIOTECA SaveOrUpdate(TB207_RESERVA_ITENS_BIBLIOTECA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB207_RESERVA_ITENS_BIBLIOTECA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB207_RESERVA_ITENS_BIBLIOTECA.</returns>
        public static TB207_RESERVA_ITENS_BIBLIOTECA GetByEntityKey(EntityKey entityKey)
        {
            return (TB207_RESERVA_ITENS_BIBLIOTECA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB207_RESERVA_ITENS_BIBLIOTECA, ordenados pelo Id da instituição "ORG_CODIGO_ORGAO" e pelo código isbn "CO_ISBN_ACER"
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB207_RESERVA_ITENS_BIBLIOTECA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB207_RESERVA_ITENS_BIBLIOTECA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB207_RESERVA_ITENS_BIBLIOTECA.OrderBy( r => r.ORG_CODIGO_ORGAO).ThenBy( r => r.CO_ISBN_ACER).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB207_RESERVA_ITENS_BIBLIOTECA pelas chaves primárias "CO_RESERVA_BIBLIOTECA", "ORG_CODIGO_ORGAO", "CO_ISBN_ACER", "CO_ACERVO_AQUISI" e "CO_ACERVO_ITENS".
        /// </summary>
        /// <param name="CO_RESERVA_BIBLIOTECA">Id do código de reserva da biblioteca</param>
        /// <param name="ORG_CODIGO_ORGAO">Id da instituição</param>
        /// <param name="CO_ISBN_ACER">Código ISBN</param>
        /// <param name="CO_ACERVO_AQUISI">Id da aquisição do acervo</param>
        /// <param name="CO_ACERVO_ITENS">Id do item do acervo</param>
        /// <returns>Entidade TB207_RESERVA_ITENS_BIBLIOTECA</returns>
        public static TB207_RESERVA_ITENS_BIBLIOTECA RetornaPelaChavePrimaria(int CO_RESERVA_BIBLIOTECA, int ORG_CODIGO_ORGAO, int CO_ISBN_ACER, int CO_ACERVO_AQUISI, int CO_ACERVO_ITENS)
        {
            return (from tb207 in RetornaTodosRegistros()
                    where tb207.CO_RESERVA_BIBLIOTECA == CO_RESERVA_BIBLIOTECA && tb207.ORG_CODIGO_ORGAO == ORG_CODIGO_ORGAO 
                    && tb207.CO_ISBN_ACER == CO_ISBN_ACER && tb207.CO_ACERVO_AQUISI == CO_ACERVO_AQUISI && tb207.CO_ACERVO_ITENS == CO_ACERVO_ITENS
                    select tb207).FirstOrDefault();
        }

        #endregion
    }
}

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;
using System.Linq.Expressions;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TB03_COLABOR
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
        /// Exclue o registro da tabela TB03_COLABOR do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB03_COLABOR entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB03_COLABOR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB03_COLABOR.</returns>
        public static TB03_COLABOR Delete(TB03_COLABOR entity)
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
        public static int SaveOrUpdate(TB03_COLABOR entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB03_COLABOR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB03_COLABOR.</returns>
        public static TB03_COLABOR SaveOrUpdate(TB03_COLABOR entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB03_COLABOR de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB03_COLABOR.</returns>
        public static TB03_COLABOR GetByEntityKey(EntityKey entityKey)
        {
            return (TB03_COLABOR)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB03_COLABOR, ordenados pelo nome "NO_COL".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB03_COLABOR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB03_COLABOR> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB03_COLABOR.OrderBy( c => c.NO_COL ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna todos os registros da entidade TB03_COLABOR de acordo com a unidade de cadastro "CO_EMP"
        /// </summary>
        /// <param name="CO_EMP">Id da unidade de cadastro</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB03_COLABOR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB03_COLABOR> RetornaPelaEmpresa(int CO_EMP)
        {
            return (from tb03 in RetornaTodosRegistros()
                    where tb03.CO_EMP == CO_EMP
                    select tb03).OrderBy( c => c.NO_COL ).AsObjectQuery();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB03_COLABOR de acordo com a unidade de trabalho "TB25_EMPRESA1.CO_EMP"
        /// </summary>
        /// <param name="CO_EMP">Id da unidade de trabalho</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB03_COLABOR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB03_COLABOR> RetornaPeloCoUnid(int CO_EMP)
        {
            return (from tb03 in RetornaTodosRegistros()
                    where tb03.TB25_EMPRESA1.CO_EMP == CO_EMP
                    select tb03).OrderBy(c => c.NO_COL).AsObjectQuery();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB03_COLABOR onde o código da função é professor
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB03_COLABOR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB03_COLABOR> RetornaPelaEmpresaProfessor(int CO_EMP)
        {
            return (from tb03 in RetornaTodosRegistros()
                    where tb03.TB25_EMPRESA1.CO_EMP == CO_EMP && tb03.CO_FUN == 8
                    select tb03).OrderBy(c => c.NO_COL).AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TB03_COLABOR onde o Id da Unidade "CO_EMP" e o Id do funcionário "CO_COL" é o informado no parâmetro.
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_COL">Id do funcionário</param>
        /// <returns>Entidade TB03_COLABOR</returns>
        public static TB03_COLABOR RetornaPelaChavePrimaria(int CO_EMP, int CO_COL)
        {
            //return (from tb03 in RetornaTodosRegistros().Include(typeof(Image).Name)
            //        where tb03.CO_EMP == CO_EMP && tb03.CO_COL == CO_COL
            //        select tb03).OrderBy(c => c.NO_COL).FirstOrDefault();

            return GestorEntities.CurrentContext.TB03_COLABOR.Include(typeof(Image).Name).FirstOrDefault(p => p.CO_COL == CO_COL && p.CO_EMP == CO_EMP);
        }

        /// <summary>
        /// Retorna um registro da entidade TB03_COLABOR onde o Id do funcionário "CO_COL" é o informado no parâmetro.
        /// </summary>
        /// <param name="CO_COL">Id da chave primária</param>
        /// <returns>Entidade TB03_COLABOR</returns>
        public static TB03_COLABOR RetornaPeloCoCol(int CO_COL)
        {
            //return (from tb03 in RetornaTodosRegistros()
            //        where tb03.CO_COL == CO_COL
            //        select tb03).OrderBy(c => c.NO_COL).FirstOrDefault();

            return GestorEntities.CurrentContext.TB03_COLABOR.FirstOrDefault(p => p.CO_COL == CO_COL);
        }

        public static TB03_COLABOR RetornarColaborador(Expression<Func<TB03_COLABOR, bool>> predicate)
        {
            return GestorEntities.CurrentContext.TB03_COLABOR.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB03_COLABOR por departamento "CO_DEPTO" e por unidade "CO_EMP"
        /// </summary>
        /// <param name="CO_DEPTO">Id do departamento</param>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB03_COLABOR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB03_COLABOR> RetornaPeloDepartamento(int CO_DEPTO, int CO_EMP)
        {
            return (from tb03 in RetornaTodosRegistros()
                    where tb03.CO_DEPTO == CO_DEPTO && tb03.CO_EMP == CO_EMP
                    select tb03).OrderBy(c => c.NO_COL).AsObjectQuery();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB03_COLABOR por função "CO_FUN" e por unidade "CO_EMP"
        /// </summary>
        /// <param name="CO_FUN">Id da função</param>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB03_COLABOR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB03_COLABOR> RetornaPelaFuncao(int CO_FUN, int CO_EMP)
        {
            return (from tb03 in RetornaTodosRegistros()
                    where tb03.CO_FUN == CO_FUN && tb03.CO_EMP == CO_EMP
                    select tb03).OrderBy(c => c.NO_COL).AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TB03_COLABOR pela matrícula "CO_MAT_COL" e pela unidade "CO_EMP"
        /// </summary>
        /// <param name="CO_MAT_COL">Código da matrícula</param>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>Entidade TB03_COLABOR</returns>
        public static TB03_COLABOR RetornaPeloCoMatCol(string CO_MAT_COL, int CO_EMP)
        {
            return (from tb03 in RetornaTodosRegistros()
                    where tb03.CO_MAT_COL == CO_MAT_COL && tb03.CO_EMP == CO_EMP
                    select tb03).FirstOrDefault();
        }
        #endregion
    }
}
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
    public partial class ConteudoAjuda
    {
        //#region Métodos

        //#region Métodos Básicos

        //public static int SaveChanges()
        //{
        //    return GestorEntities.CurrentContext.SaveChanges();
        //}

        ///// <summary>
        ///// Delete a ConteudoAjuda from Current Object Context and you may choose to persist, or not, the changes to database.
        ///// </summary>
        ///// <param name="entity">The entity to be deleted.</param>
        ///// <param name="saveChanges">Determine if persist changes to database.</param>
        ///// <returns>The number of changed entities in Current Object Context when it's persisted to database.</returns>
        //public static int Delete(ConteudoAjuda entity, bool saveChanges)
        //{
        //    return GestorEntities.Delete(entity, saveChanges);
        //}

        ///// <summary>
        ///// Delete an the ConteudoAjuda from database.
        ///// </summary>
        ///// <param name="entity">The entity to be deleted.</param>
        ///// <returns>It own ConteudoAjuda entity.</returns>
        //public static ConteudoAjuda Delete(ConteudoAjuda entity)
        //{
        //    Delete(entity, true);

        //    return GetByEntityKey(entity.EntityKey);
        //}

        ///// <summary>
        ///// Checks the actual state of the entity and takes the correct action, you can use the saveChanges parameter to choose persists changes to database or not.
        ///// </summary>
        ///// <param name="entity">The entity to be added to the current context.</param>
        ///// <param name="saveChanges">Choose to persist to database.</param>
        ///// <returns>The number of changed entities in Current Object Context when it's persisted to database.</returns>
        //public static int SaveOrUpdate(ConteudoAjuda entity, bool saveChanges)
        //{
        //    return GestorEntities.SaveOrUpdate(entity, saveChanges);
        //}

        ///// <summary>
        ///// Checks the actual state of the entity and Save, Update or delete the ConteudoAjuda entity from database.
        ///// </summary>
        ///// <param name="entity">The entity to be added to the current context.</param>
        ///// <returns>It own ConteudoAjuda entity.</returns>
        //public static ConteudoAjuda SaveOrUpdate(ConteudoAjuda entity)
        //{
        //    SaveOrUpdate(entity, true);

        //    return GetByEntityKey(entity.EntityKey);
        //}

        ///// <summary>
        ///// Get a ConteudoAjuda entity by specified key.
        ///// </summary>
        ///// <param name="entityKey">The EntityKey to filter.</param>
        ///// <returns>The ConteudoAjuda entity with the same entityKey</returns>
        //public static ConteudoAjuda GetByEntityKey(EntityKey entityKey)
        //{
        //    return (ConteudoAjuda)GestorEntities.GetByEntityKey(entityKey);
        //}

        ///// <summary>
        ///// Get all ConteudoAjuda entities already ordered and filtered at default.
        ///// </summary>
        ///// <returns>A ObjectQuery containing all ConteudoAjuda entities that match with default filters.</returns>
        //public static ObjectQuery<ConteudoAjuda> RetornaTodosRegistros()
        //{
        //    return GestorEntities.CurrentContext.ConteudoAjuda;
        //}

        //#endregion

        //#endregion
    }
}

﻿//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
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
    public partial class TBS332_ATEND_SERV_AMBUL
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
    /// Exclue o registro da tabela TBE222_PAGTO_CHEQUE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
    /// </summary>
    /// <param name="entity">A entidade na qual será executada a ação.</param>
    /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
    /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
    public static int Delete(TBS332_ATEND_SERV_AMBUL entity, bool saveChanges)
    {
        return GestorEntities.Delete(entity, saveChanges);
    }

    /// <summary>
    /// Exclue o registro da tabela TBS194_PRE_ATEND na base de dados.
    /// </summary>
    /// <param name="entity">A entidade na qual será executada a ação.</param>
    /// <returns>A própria entidade TBE222_PAGTO_CHEQUE.</returns>
    public static TBS332_ATEND_SERV_AMBUL Delete(TBS332_ATEND_SERV_AMBUL entity)
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
    public static int SaveOrUpdate(TBS332_ATEND_SERV_AMBUL entity, bool saveChanges)
    {
        return GestorEntities.SaveOrUpdate(entity, saveChanges);
    }

    /// <summary>
    /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS194_PRE_ATEND na base de dados.
    /// </summary>
    /// <param name="entity">A entidade na qual será executada a ação.</param>
    /// <returns>A própria entidade TBE222_PAGTO_CHEQUE.</returns>
    public static TBS332_ATEND_SERV_AMBUL SaveOrUpdate(TBS332_ATEND_SERV_AMBUL entity)
    {
        SaveOrUpdate(entity, true);

        return GetByEntityKey(entity.EntityKey);
    }

    /// <summary>
    /// Retorna o registro da entidade TBS194_PRE_ATEND de acordo com a chave.
    /// </summary>
    /// <param name="entityKey">A chave da entidade para filtro.</param>
    /// <returns>A própria entidade TBE222_PAGTO_CHEQUE.</returns>
    public static TBS332_ATEND_SERV_AMBUL GetByEntityKey(EntityKey entityKey)
    {
        return (TBS332_ATEND_SERV_AMBUL)GestorEntities.GetByEntityKey(entityKey);
    }

    /// <summary>
    /// Retorna todos os registro da entidade TBS332_ATEND_SERV_AMBUL, ordenados pelo nome fantasia "NO_FANTAS_EMP".
    /// </summary>
    /// <returns>ObjectQuery com todos os registros da entidade TBS194_PRE_ATEND de acordo com a filtragem desenvolvida.</returns>
    public static ObjectQuery<TBS332_ATEND_SERV_AMBUL> RetornaTodosRegistros()
    {
        return GestorEntities.CurrentContext.TBS332_ATEND_SERV_AMBUL.AsObjectQuery();
    }

    /// <summary>
    /// Retorna um registro da entidade TBS194_PRE_ATEND pela chave primária "CO_TIPO_MOV".
    /// </summary>
    /// <param name="CO_TIPO_MOV">Id da chave primária</param>
    /// <returns>Entidade TBS332_ATEND_SERV_AMBUL</returns>
    public static TBS332_ATEND_SERV_AMBUL RetornaPelaChavePrimaria(int ID_ATEND_SERV_AMBUL)
    {
        return (from tbs332 in RetornaTodosRegistros()
                where tbs332.ID_ATEND_SERV_AMBUL == ID_ATEND_SERV_AMBUL
                select tbs332).FirstOrDefault();
    }

    /// <summary>
    /// Retorna um registro da entidade TBS332_ATEND_SERV_AMBUL pela chave primária "ID_ATEND_MEDIC".
    /// </summary>
    /// <param name="CO_TIPO_MOV">Id do Atendimento</param>
    /// <returns>Lista</returns>
    public static List<TBS332_ATEND_SERV_AMBUL> RetornaPeloIDAtendimento(int ID_ATEND_MEDIC)
    {
        return (from tbs332 in RetornaTodosRegistros()
                where tbs332.ID_ATEND_MEDIC == ID_ATEND_MEDIC
                select tbs332).ToList();
    }


    #endregion
    }
}

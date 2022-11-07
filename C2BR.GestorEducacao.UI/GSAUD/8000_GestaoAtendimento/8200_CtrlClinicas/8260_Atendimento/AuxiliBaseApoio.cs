using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxClasses.Internal;
using System.Resources;
using System.Globalization;
using System.Collections;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Resources;

namespace C2BR.GestorEducacao.UI.Library.Auxiliares
{
    public class AuxiliBaseApoio
    {
        private static Dictionary<string, string> statusMatricula = AuxiliBaseApoio.chave(statusMatriculaAluno.ResourceManager);
        private static string statusMatAtivo = statusMatricula[statusMatriculaAluno.A];
        private static string statusMatPre = statusMatricula[statusMatriculaAluno.R];
        private static string statusMatFim = statusMatricula[statusMatriculaAluno.F];
        #region ListItem para DropDownList
        #region ListItem de arquivo .resx
        /// <summary>
        /// Restorna um array de ListItem para ser adicionado no dropdownlist
        /// </summary>
        /// <param name="dados"></param>
        /// <param name="selecione"></param>
        /// <param name="todos"></param>
        /// <returns></returns>
        public static ListItem[] BaseApoioDDL(System.Resources.ResourceManager dados, bool selecione = false, bool todos = false)
        {
            ListItemCollection retorno = new ListItemCollection();
            IOrderedEnumerable<DictionaryEntry> banco = dados.GetResourceSet(CultureInfo.CurrentUICulture, true, true).OfType<DictionaryEntry>().OrderByDescending(o => o.Value);
            banco.OrderBy(O => O.Value);
            foreach (DictionaryEntry linha in banco)
                retorno.Insert(0, new ListItem(linha.Value.ToString(), linha.Key.ToString()));
            if (todos)
            {
                ListItem opTodos = new ListItem("Todos", "-1");
                if (!selecione)
                    opTodos.Selected = true;
                retorno.Insert(0, opTodos);
            }
            if (selecione)
            {
                ListItem opSelec = new ListItem("Selecione", "");
                opSelec.Selected = true;
                retorno.Insert(0, opSelec);
            }
            return retorno.Cast<ListItem>().ToArray();
        }
        #endregion
        #region ListItem do banco de dados
        /// <summary>
        /// Carrega as unidades disponiveis para o usuário logado atualmente.
        /// </summary>
        /// <typeparam name="t1">Tipo de dado do código de usuário logado</typeparam>
        /// <param name="idUsuario">Código do usuário atualmente logado no sistema</param>
        /// <param name="selecione">Marcar para adicionar a opção selecione</param>
        /// <param name="todos">Marcar para adicionar a opção todos</param>
        /// <returns>Retorna a lista de unidade/empresas habilitadas para o usuário informado</returns>
        public static ListItem[] UnidadesDDL<t1>(t1 idUsuario, bool selecione = false, bool todos = false)
        {
            
            ListItemCollection retorno = new ListItemCollection();
            int usuarioId = AuxiliFormatoExibicao.conversorGenerico<int, t1>(idUsuario);
            if (usuarioId > 0)
            {
                IEnumerable<TB25_EMPRESA> matriculas = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                                        join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                                        where tb134.ADMUSUARIO.ideAdmUsuario == usuarioId
                                                        orderby tb25.NO_FANTAS_EMP
                                                        select tb25).DistinctBy(d => d.CO_EMP).DefaultIfEmpty();
                if (matriculas != null && matriculas.Count() > 0 && matriculas.ElementAt(0) != null)
                {
                    foreach (var linha in matriculas)
                    {
                        if (linha != null)
                            retorno.Insert(0, new ListItem(linha.NO_FANTAS_EMP.ToString(), linha.CO_EMP.ToString()));
                    }
                }
            }
            if (todos)
            {
                ListItem opTodos = new ListItem("Todos", "-1");
                if (!selecione)
                    opTodos.Selected = true;
                retorno.Insert(0, opTodos);
            }
            if (selecione)
            {
                ListItem opSelec = new ListItem("Selecione", "");
                opSelec.Selected = true;
                retorno.Insert(0, opSelec);
            }
            return retorno.Cast<ListItem>().ToArray();
        }
        /// <summary>
        /// Carrega os anos atuais ativos de matrículas
        /// </summary>
        /// <typeparam name="t1">Tipo de dado da empresa</typeparam>
        /// <param name="empresa">Código da empresa relacionada ao ano</param>
        /// <param name="selecione">Marcar para adicionar a opção selecione</param>
        /// <param name="todos">Marcar para adicionar a opção todos</param>
        /// <returns>Retorna a lista de anos matrículados e habilitados no sistemas</returns>
        public static ListItem[] AnosDDL<t1>(t1 empresa, bool selecione = false, bool todos = false, bool atual = false)
        {
            ListItemCollection retorno = new ListItemCollection();
            int empresaInt = AuxiliFormatoExibicao.conversorGenerico<int, t1>(empresa);
            IEnumerable<TB08_MATRCUR> matriculas = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                                    where (empresaInt == -1 ? 0==0 : tb08.CO_EMP == empresaInt)
                                                    orderby tb08.CO_ANO_MES_MAT
                                                    select tb08).DistinctBy(d=>d.CO_ANO_MES_MAT).DefaultIfEmpty();
            if (matriculas != null && matriculas.Count() > 0 && matriculas.ElementAt(0) != null)
            {
                foreach (var linha in matriculas)
                {
                    if (linha != null)
                    {
                        ListItem opLinha = new ListItem(linha.CO_ANO_MES_MAT.ToString().Trim(), linha.CO_ANO_MES_MAT.ToString().Trim());
                        if (atual)
                        {
                            if(linha.CO_ANO_MES_MAT.ToString().Trim() == DateTime.Now.Year.ToString().Trim())
                                opLinha.Selected = true;
                        }
                        retorno.Insert(0, opLinha);
                    }
                }
            }
            if (todos)
            {
                ListItem opTodos = new ListItem("Todos", "-1");
                if (!atual && !selecione)
                    opTodos.Selected = true;
                retorno.Insert(0, opTodos);
            }
            if (selecione)
            {
                ListItem opSelec = new ListItem("Selecione", "");
                if(!atual)
                    opSelec.Selected = true;
                retorno.Insert(0, opSelec);
            }
            return retorno.Cast<ListItem>().ToArray();
        }
        /// <summary>
        /// Retorna os modulos de ensino do sistema
        /// </summary>
        /// <param name="orgao">Orgão para listagem</param>
        /// <param name="selecione">Marcar para adicionar o 'Selecione'</param>
        /// <param name="todos">Marcar para adicionar a opção todos</param>
        /// <returns></returns>
        public static ListItem[] ModulosDDL<t>(t orgao, bool selecione = false, bool todos = false)
        {
            ListItemCollection retorno = new ListItemCollection();
            int orgaoInt = AuxiliFormatoExibicao.conversorGenerico<int, t>(orgao);
            IEnumerable<TB44_MODULO> modulos = (from tb44 in TB44_MODULO.RetornaTodosRegistros()
                                                where (orgaoInt == -1 ? 0 == 0 : tb44.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == orgaoInt)
                                                orderby tb44.DE_MODU_CUR descending
                                                select tb44).DistinctBy(d => d.CO_MODU_CUR).DefaultIfEmpty();
            if (modulos != null && modulos.Count() > 0 && modulos.ElementAt(0) != null)
            {
                foreach (var linha in modulos)
                {
                    if(linha != null)
                        retorno.Insert(0, new ListItem(linha.DE_MODU_CUR.ToString(), linha.CO_MODU_CUR.ToString()));
                }
            }
            if (todos)
            {
                ListItem opTodos = new ListItem("Todos", "-1");
                if (!selecione)
                    opTodos.Selected = true;
                retorno.Insert(0, opTodos);
            }
            if (selecione)
            {
                ListItem opSelec = new ListItem("Selecione", "");
                opSelec.Selected = true;
                retorno.Insert(0, opSelec);
            }
            return retorno.Cast<ListItem>().ToArray();
        }
        /// <summary>
        /// Carrega todas as séries 
        /// </summary>
        /// <typeparam name="t1">Tipo de dado da código da empresa</typeparam>
        /// <typeparam name="t2">Tipo de dado do código do modulo</typeparam>
        /// <typeparam name="t3">Tipo de dado do ano referência</typeparam>
        /// <param name="empresa">Códigio da empresa a buscar as séries</param>
        /// <param name="modulo">Código do módulo a buscar as séries relacionadas</param>
        /// <param name="anoGrade">Ano de referência das séries</param>
        /// <param name="selecione">Marcar para adicionar a opção selecione</param>
        /// <param name="todos">Marcar para adicionar a opção todos</param>
        /// <returns>Retorna uma ListItem com o resultado das séries buscadas</returns>
        public static ListItem[] SeriesDDL<t1, t2, t3>(t1 empresa, t2 modulo, t3 anoGrade, bool selecione = false, bool todos = false)
        {
            ListItemCollection retorno = new ListItemCollection();
            int moduloInt = AuxiliFormatoExibicao.conversorGenerico<int, t2>(modulo);
            string anoStr = AuxiliFormatoExibicao.conversorGenerico<string, t3>(anoGrade);
            int empInt = AuxiliFormatoExibicao.conversorGenerico<int, t1>(empresa);
            if (anoStr != "" && (anoStr.Length == 6 || anoStr.Length == 4 || anoStr.Length == 2))
            {
                IEnumerable<TB01_CURSO> series = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                                  where (empInt == -1 ? 0 == 0 : tb01.CO_EMP == empInt)
                                                  && (moduloInt == -1 ? 0 == 0 : tb01.CO_MODU_CUR == moduloInt)
                                                  join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb01.CO_CUR equals tb43.CO_CUR into resultado
                                                  from tb43 in resultado.DefaultIfEmpty()
                                                  where tb43 != null
                                                  && (empInt == -1 ? 0 == 0 : tb43.CO_EMP == empInt)
                                                  && (anoStr == "-1" ? 0 == 0 : tb43.CO_ANO_GRADE == anoStr)
                                                  && (moduloInt == -1 ? 0 == 0 : tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR)
                                                  orderby tb01.NO_CUR descending
                                                  select tb01).DistinctBy(d => d.CO_CUR).DefaultIfEmpty();

                if (series != null && series.Count() > 0 && series.ElementAt(0) != null)
                {
                    foreach (var linha in series)
                    {
                        if(linha != null)
                            retorno.Insert(0, new ListItem(linha.NO_CUR.ToString(), linha.CO_CUR.ToString()));
                    }
                }
            }
            if (todos)
            {
                ListItem opTodos = new ListItem("Todos", "-1");
                if(!selecione)
                    opTodos.Selected = true;
                retorno.Insert(0, opTodos);
            }
            if (selecione)
            {
                ListItem opSelec = new ListItem("Selecione", "");
                opSelec.Selected = true;
                retorno.Insert(0, opSelec);
            }
            return retorno.Cast<ListItem>().ToArray();
        }
        /// <summary>
        /// Carrega todas as turmas de acordo com os filtros
        /// </summary>
        /// <typeparam name="t1">Tipo de valor da empresa</typeparam>
        /// <typeparam name="t2">Tipo de valor do modulo</typeparam>
        /// <typeparam name="t3">Tipo de valor da série</typeparam>
        /// <typeparam name="t4">Tipo de valor do ano da grade</typeparam>
        /// <param name="empresa">Código da empresa relacionado a turmas</param>
        /// <param name="modulo">Código do módulo a relacionado a turma</param>
        /// <param name="serie">Código da série relacionado a turmas</param>
        /// <param name="anoGrade">Ano da grande anual relacionado a turmas</param>
        /// <param name="selecione">Marcar para adicionar apção selecione</param>
        /// <param name="todos">Marcar para adicionar apção todos</param>
        /// <returns></returns>
        public static ListItem[] TurmasDDL<t1, t2, t3, t4>(t1 empresa, t2 modulo, t3 serie, t4 anoGrade, bool selecione = false, bool todos = false)
        {
            ListItemCollection retorno = new ListItemCollection();
            int moduloInt = AuxiliFormatoExibicao.conversorGenerico<int, t2>(modulo);
            int serieInt = AuxiliFormatoExibicao.conversorGenerico<int, t3>(serie);
            string anoStr = AuxiliFormatoExibicao.conversorGenerico<string, t4>(anoGrade);
            int empInt = AuxiliFormatoExibicao.conversorGenerico<int, t1>(empresa);
            if (anoStr != "" && (anoStr.Length == 6 || anoStr.Length == 4 || anoStr.Length == 2))
            {
                IEnumerable<TB129_CADTURMAS> turmas = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                                       where (moduloInt == -1 ? 0 == 0 : tb06.CO_MODU_CUR == moduloInt)
                                                       && (serieInt == -1 ? 0 == 0 : tb06.CO_CUR == serieInt)
                                                       && (empInt == -1 ? 0 == 0 : tb06.CO_EMP == empInt)
                                                       join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb06.CO_TUR equals tb129.CO_TUR into resultado
                                                       from tb129 in resultado.DefaultIfEmpty()
                                                       where tb129 != null
                                                       orderby tb129.NO_TURMA descending
                                                       select tb129).DistinctBy(d => d.CO_TUR).DefaultIfEmpty();

                if (turmas != null && turmas.Count() > 0 && turmas.ElementAt(0) != null)
                {
                    foreach (var linha in turmas)
                    {
                        if(linha != null)
                            retorno.Insert(0, new ListItem(linha.NO_TURMA.ToString(), linha.CO_TUR.ToString()));
                    }
                }
            }
            if (todos)
            {
                ListItem opTodos = new ListItem("Todos", "-1");
                if (!selecione)
                    opTodos.Selected = true;
                retorno.Insert(0, opTodos);
            }
            if (selecione)
            {
                ListItem opSelec = new ListItem("Selecione", "");
                opSelec.Selected = true;
                retorno.Insert(0, opSelec);
            }
            return retorno.Cast<ListItem>().ToArray();
        }
        /// <summary>
        /// Retorna os dados de combo dos alunos matrículados de acordo com filtro e situação especificos
        /// </summary>
        /// <typeparam name="t1">Tipo de dados da emrpesa/unidade</typeparam>
        /// <typeparam name="t2">Tipo de dados do módulo que contêm a matrícula</typeparam>
        /// <typeparam name="t3">Tipo de dados da série da matrícula</typeparam>
        /// <typeparam name="t4">Tipo de dados da turma da matrícula</typeparam>
        /// <typeparam name="t5">Tipo de dados do ano de referência da matrícula do aluno</typeparam>
        /// <param name="empresa">Codigo da unidade a qual pertence a matrícula</param>
        /// <param name="modulo">Módulo a qual a matrícula do aluno pertence</param>
        /// <param name="serie">Série da matrícula do aluno</param>
        /// <param name="turma">Turma da matrícula do aluno</param>
        /// <param name="anoGrade">Ano para filtro das matrículas do aluno</param>
        /// <param name="situacao">Define o filtro para situação do aluno default todos, situação especifica ou AR para ativos e pré-matrículas</param>
        /// <param name="codigoAluno">Se o valor dever ser o código do aluno, default retorna o código de matrícula</param>
        /// <param name="selecione">Marcar para adicionar apção selecione</param>
        /// <param name="todos">Marcar para adicionar apção todos</param>
        /// <returns>Retorna a lista de alunos matrículados no sistemas, podendo ser tanto por código do aluno quanto o código de cadastro do aluno</returns>
        public static ListItem[] AlunosDDL<t1, t2, t3, t4, t5>(t1 empresa, t2 modulo, t3 serie, t4 turma, t5 anoGrade, string situacao = "-1", bool codigoAluno =  false, bool selecione = false, bool todos = false, bool finalizado = true)
        {
            ListItemCollection retorno = new ListItemCollection();
            int moduloInt = AuxiliFormatoExibicao.conversorGenerico<int, t2>(modulo);
            int serieInt = AuxiliFormatoExibicao.conversorGenerico<int, t3>(serie);
            int turmaInt = AuxiliFormatoExibicao.conversorGenerico<int, t4>(turma);
            string anoStr = AuxiliFormatoExibicao.conversorGenerico<string, t5>(anoGrade);
            int empInt = AuxiliFormatoExibicao.conversorGenerico<int, t1>(empresa);
            if (anoStr != "" && (anoStr.Length == 6 || anoStr.Length == 4 || anoStr.Length == 2))
            {
                IEnumerable<TB08_MATRCUR> alunos = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                                    where (empInt == -1 ? 0==0 : tb08.TB25_EMPRESA.CO_EMP == empInt)
                                                       && (anoStr == "-1" ? 0==0 : tb08.CO_ANO_MES_MAT == anoStr)
                                                       && (serieInt == -1 ? 0==0 : tb08.CO_CUR == serieInt)
                                                       && (moduloInt == -1 ? 0 == 0 : tb08.TB44_MODULO.CO_MODU_CUR == moduloInt)
                                                       && (turmaInt == -1 ? 0 == 0 : tb08.CO_TUR == turmaInt)
                                                       && ((situacao == "-1" ? 0 == 0 : (situacao == "AR" ? (tb08.CO_SIT_MAT == statusMatAtivo || tb08.CO_SIT_MAT == statusMatPre) : (tb08.CO_SIT_MAT == situacao))) || (finalizado ? tb08.CO_SIT_MAT == statusMatFim : finalizado))
                                                       orderby tb08.TB07_ALUNO.NO_ALU descending
                                                       select tb08).DefaultIfEmpty();

                if (alunos != null && alunos.Count() > 0 && alunos.ElementAt(0) != null)
                {
                    if (codigoAluno)
                        alunos = alunos.DistinctBy(d => d.CO_ALU).OrderByDescending(o => o.TB07_ALUNO.NO_ALU);
                    else
                        alunos = alunos.DistinctBy(d => d.CO_ALU_CAD).OrderByDescending(o => o.TB07_ALUNO.NO_ALU);
                    foreach (var linha in alunos)
                    {
                        if (linha != null)
                        {
                            if (codigoAluno)
                                retorno.Insert(0, new ListItem(linha.TB07_ALUNO.NO_ALU.ToString(), linha.TB07_ALUNO.CO_ALU.ToString()));
                            else
                                retorno.Insert(0, new ListItem(linha.TB07_ALUNO.NO_ALU.ToString(), linha.CO_ALU_CAD.ToString()));
                        }
                    }
                }
            }
            if (todos)
            {
                ListItem opTodos = new ListItem("Todos", "-1");
                if (!selecione)
                    opTodos.Selected = true;
                retorno.Insert(0, opTodos);
            }
            if (selecione)
            {
                ListItem opSelec = new ListItem("Selecione", "");
                opSelec.Selected = true;
                retorno.Insert(0, opSelec);
            }
            return retorno.Cast<ListItem>().ToArray();
        }
        /// <summary>
        /// Retorna os dados de combo dos alunos matrículados de acordo com filtro e situação especificos
        /// </summary>
        /// <typeparam name="t1">Tipo de dado do responsavel</typeparam>
        /// <param name="responsavel">Código do responsavél referência</param>
        /// <param name="situacao">Situação do aluno tendo -1 como todos</param>
        /// <param name="codigoAluno">Marcar para retornar o codigo do aluno ao invês do código de matrícula</param>
        /// <param name="selecione">Marcar para adicionar a opção 'Selecione'</param>
        /// <param name="todos">Marcar para adicionar a opção 'Todos'</param>
        /// <returns></returns>
        public static ListItem[] AlunosDDL<t1>(t1 responsavel, string situacao = "-1", bool codigoAluno = true, bool selecione = false, bool todos = false, bool finalizado = true)
        {
            ListItemCollection retorno = new ListItemCollection();
            int responsavelInt = AuxiliFormatoExibicao.conversorGenerico<int, t1>(responsavel);
            IEnumerable<TB08_MATRCUR> alunos = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                                where (responsavelInt == -1 ? 0 == 0 : tb08.TB07_ALUNO.TB108_RESPONSAVEL.CO_RESP == responsavelInt)
                                                    && ((situacao == "-1" ? 0 == 0 : (situacao == "AR" ? (tb08.CO_SIT_MAT == statusMatAtivo || tb08.CO_SIT_MAT == statusMatPre) : (tb08.CO_SIT_MAT == situacao))) || (finalizado ? tb08.CO_SIT_MAT == statusMatFim : finalizado))
                                                orderby tb08.TB07_ALUNO.NO_ALU descending
                                                select tb08).DefaultIfEmpty();

            if (alunos != null && alunos.Count() > 0 && alunos.ElementAt(0) != null)
            {
                if (codigoAluno)
                    alunos = alunos.DistinctBy(d => d.CO_ALU).OrderByDescending(o => o.TB07_ALUNO.NO_ALU);
                else
                    alunos = alunos.DistinctBy(d => d.CO_ALU_CAD).OrderByDescending(o => o.TB07_ALUNO.NO_ALU);
                foreach (var linha in alunos)
                {
                    if (linha != null)
                    {
                        if (codigoAluno)
                            retorno.Insert(0, new ListItem(linha.TB07_ALUNO.NO_ALU.ToString(), linha.TB07_ALUNO.CO_ALU.ToString()));
                        else
                            retorno.Insert(0, new ListItem(linha.TB07_ALUNO.NO_ALU.ToString(), linha.CO_ALU_CAD.ToString()));
                    }
                }
            }
            if (todos)
            {
                ListItem opTodos = new ListItem("Todos", "-1");
                if (!selecione)
                    opTodos.Selected = true;
                retorno.Insert(0, opTodos);
            }
            if (selecione)
            {
                ListItem opSelec = new ListItem("Selecione", "");
                opSelec.Selected = true;
                retorno.Insert(0, opSelec);
            }
            return retorno.Cast<ListItem>().ToArray();
        }
        /// <summary>
        /// Lista de matérias da turma de acordo com o filtro especificado para combo
        /// </summary>
        /// <typeparam name="t1">Tipo de dado da unidade</typeparam>
        /// <typeparam name="t2">Tipo de dado do modulo</typeparam>
        /// <typeparam name="t3">Tipo de dado da série/curso</typeparam>
        /// <typeparam name="t4">Tipo de dado da turma</typeparam>
        /// <typeparam name="t5">Tipo de dado do ano</typeparam>
        /// <param name="empresa">Código da unidade a ser filtrado</param>
        /// <param name="modulo">Código do modulo a ser filtrado a turma</param>
        /// <param name="serie">Código da série/curso a ser filtrada a turma</param>
        /// <param name="turma">Código da turma a ser listada as materias</param>
        /// <param name="anoGrade">Ano de referência para lista de materias pela grade de curso</param>
        /// <param name="selecione">Informar se deverá retornar a opção 'Selecione'</param>
        /// <param name="todos">Informar se deverá retornar a opção 'Todos'</param>
        /// <returns>Retorna a lista de matérias da turma do filtro informado</returns>
        public static ListItem[] MateriasDDL<t1, t2, t3, t4, t5>(t1 empresa, t2 modulo, t3 serie, t4 turma, t5 anoGrade, bool selecione = false, bool todos = false)
        {
            ListItemCollection retorno = new ListItemCollection();
            int moduloInt = AuxiliFormatoExibicao.conversorGenerico<int, t2>(modulo);
            int serieInt = AuxiliFormatoExibicao.conversorGenerico<int, t3>(serie);
            int turmaInt = AuxiliFormatoExibicao.conversorGenerico<int, t4>(turma);
            string anoStr = AuxiliFormatoExibicao.conversorGenerico<string, t5>(anoGrade);
            int empInt = AuxiliFormatoExibicao.conversorGenerico<int, t1>(empresa);
            if (anoStr != "" && (anoStr.Length == 6 || anoStr.Length == 4 || anoStr.Length == 2))
            {
                var materias = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                                                    where (moduloInt == -1 ? 0 == 0 : tb43.TB44_MODULO.CO_MODU_CUR == moduloInt)
                                                    && (serieInt == -1 ? 0 == 0 : tb43.CO_CUR == serieInt)
                                                    && (anoStr == "-1" ? 0 == 0 : tb43.CO_ANO_GRADE == anoStr)
                                                    join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT into resultadoM
                                                    from tb02 in resultadoM.DefaultIfEmpty()
                                                    join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA into resultadoCM
                                                    from tb107 in resultadoCM.DefaultIfEmpty()
                                                    where tb02 != null
                                                    && tb107 != null
                                                    && (empInt == -1 ? 0==0 : tb107.CO_EMP == empInt)
                                                    orderby tb107.NO_MATERIA descending
                                                    select new { tb02, tb107 }).DistinctBy(d=>d.tb02.CO_MAT).DefaultIfEmpty();

                if (materias != null && materias.Count() > 0 && materias.ElementAt(0) != null)
                {
                    foreach (var linha in materias)
                    {
                        if (linha != null)
                        {
                            retorno.Insert(0, new ListItem(linha.tb107.NO_MATERIA.ToString(), linha.tb02.CO_MAT.ToString()));
                        }
                    }
                }
            }
            if (todos)
            {
                ListItem opTodos = new ListItem("Todos", "-1");
                if (!selecione)
                    opTodos.Selected = true;
                retorno.Insert(0, opTodos);
            }
            if (selecione)
            {
                ListItem opSelec = new ListItem("Selecione", "");
                opSelec.Selected = true;
                retorno.Insert(0, opSelec);
            }
            return retorno.Cast<ListItem>().ToArray();
        }
        /// <summary>
        /// Lista dos agrupadores financeiros
        /// </summary>
        /// <typeparam name="t1">Tipo de dado do tipo de agrupador</typeparam>
        /// <param name="tipoAgrupador">Tipo de agrupador a ser filtrado</param>
        /// <param name="selecione">Informar para adicionar a opção 'Selecionar'</param>
        /// <param name="todos">Informar para adicionar a opção 'Todos'</param>
        /// <returns>Retorna a lista de agrupadores financeiros de acordo com o tipo especificado</returns>
        public static ListItem[] AgrupadoresDDL<t1>(t1 tipoAgrupador, bool selecione = false, bool todos = false)
        {
            ListItemCollection retorno = new ListItemCollection();
            string tipo = AuxiliFormatoExibicao.conversorGenerico<string, t1>(tipoAgrupador);
            IEnumerable<TB315_AGRUP_RECDESP> agrupadores = (from tb315 in TB315_AGRUP_RECDESP.RetornaTodosRegistros()
                                                            where (tipo == "-1" ? 0==0 : tb315.TP_AGRUP_RECDESP == tipo)
                                                            && (tb315.CO_SITU_AGRUP_RECDESP == statusMatAtivo)
                                                            orderby tb315.DE_SITU_AGRUP_RECDESP descending
                                                            select tb315).DistinctBy(d=>d.ID_AGRUP_RECDESP).DefaultIfEmpty();
            if (agrupadores != null && agrupadores.Count() > 0 && agrupadores.ElementAt(0) != null)
            {
                foreach (var linha in agrupadores)
                {
                    if (linha != null)
                    {
                        retorno.Insert(0, new ListItem(linha.DE_SITU_AGRUP_RECDESP.ToString(), linha.ID_AGRUP_RECDESP.ToString()));
                    }
                }
            }
            if (todos)
            {
                ListItem opTodos = new ListItem("Todos", "-1");
                if (!selecione)
                    opTodos.Selected = true;
                retorno.Insert(0, opTodos);
            }
            if (selecione)
            {
                ListItem opSelec = new ListItem("Selecione", "");
                opSelec.Selected = true;
                retorno.Insert(0, opSelec);
            }
            return retorno.Cast<ListItem>().ToArray();
        }
        /// <summary>
        /// Retorna a lista de responsáveis da instituição
        /// </summary>
        /// <typeparam name="t1">Tipo de dado do código da instituição</typeparam>
        /// <param name="orgao">Código do orgão da instituição</param>
        /// <param name="selecione">Marque para adicionar a opção 'Selecione'</param>
        /// <param name="todos">Marque para dicionar a opção 'Todos'</param>
        /// <returns></returns>
        public static ListItem[] ResponsaveisDDL<t1>(t1 orgao, bool selecione = false, bool todos = false)
        {
            ListItemCollection retorno = new ListItemCollection();
            int orgaoInt = AuxiliFormatoExibicao.conversorGenerico<int, t1>(orgao);
            IEnumerable<TB108_RESPONSAVEL> responsaveis = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                                           where (orgaoInt == -1 ? 0 == 0 : tb108.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == orgaoInt)
                                                          orderby tb108.NO_RESP descending
                                                          select tb108).DistinctBy(d => d.CO_RESP).DefaultIfEmpty();
            if (responsaveis != null && responsaveis.Count() > 0 && responsaveis.ElementAt(0) != null)
            {
                foreach (var linha in responsaveis)
                {
                    if (linha != null)
                    {
                        retorno.Insert(0, new ListItem(linha.NO_RESP.ToString(), linha.CO_RESP.ToString()));
                    }
                }
            }
            if (todos)
            {
                ListItem opTodos = new ListItem("Todos", "-1");
                if (!selecione)
                    opTodos.Selected = true;
                retorno.Insert(0, opTodos);
            }
            if (selecione)
            {
                ListItem opSelec = new ListItem("Selecione", "");
                opSelec.Selected = true;
                retorno.Insert(0, opSelec);
            }
            return retorno.Cast<ListItem>().ToArray();
        }
        /// <summary>
        /// Gera uma lista com todos o anos de origem de alunos já cadastrados no sistema
        /// </summary>
        /// <param name="selecione">Marque para adicionar a opção 'Selecione'</param>
        /// <param name="todos">Marque para dicionar a opção 'Todos'</param>
        /// <returns>Retorna todos os anos de origem da tabela tb07</returns>
        public static ListItem[] AnosOrigemDDL(bool selecione = false, bool todos = false)
        {
            ListItemCollection retorno = new ListItemCollection();
            IEnumerable<TB07_ALUNO> alunos = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                                    where tb07.CO_ANO_ORI != null && tb07.CO_ANO_ORI != ""
                                                    orderby tb07.CO_ANO_ORI descending
                                                    select tb07).DistinctBy(d => d.CO_ANO_ORI).DefaultIfEmpty();
            if (alunos != null && alunos.Count() > 0 && alunos.ElementAt(0) != null)
            {
                foreach (var linha in alunos)
                {
                    if (linha != null)
                        retorno.Insert(0, new ListItem(linha.CO_ANO_ORI.ToString().Trim(), linha.CO_ANO_ORI.ToString().Trim()));
                }
            }
            if (todos)
            {
                ListItem opTodos = new ListItem("Todos", "-1");
                if (!selecione)
                    opTodos.Selected = true;
                retorno.Insert(0, opTodos);
            }
            if (selecione)
            {
                ListItem opSelec = new ListItem("Selecione", "");
                opSelec.Selected = true;
                retorno.Insert(0, opSelec);
            }
            return retorno.Cast<ListItem>().ToArray();
        }
        /// <summary>
        /// Retorna os dias para vencimento do título
        /// </summary>
        /// <param name="selecione">Marque para adicionar a opção 'Selecione'</param>
        /// <param name="todos">Marque para dicionar a opção 'Todos'</param>
        /// <returns>Retorna a lista de dias disponíveis</returns>
        public static ListItem[] DiaVencimentoTitulo(bool selecione = false, bool todos = false)
        {
            ListItemCollection retorno = new ListItemCollection();
            int i = 30;
            while (i >= 1)
            {
                retorno.Insert(0, new ListItem(i.ToString(), i.ToString()));
                i--;
            }
            if (todos)
            {
                ListItem opTodos = new ListItem("Todos", "-1");
                if (!selecione)
                    opTodos.Selected = true;
                retorno.Insert(0, opTodos);
            }
            if (selecione)
            {
                ListItem opSelec = new ListItem("Selecione", "");
                opSelec.Selected = true;
                retorno.Insert(0, opSelec);
            }
            return retorno.Cast<ListItem>().ToArray();
        }
        #endregion
        #endregion

        #region Diversos
        /// <summary>
        /// Retorna um dictionary com os valores do resource podendo ser tanto chave e valor, quanto chave e chave
        /// </summary>
        /// <param name="dados">ResourceManager do qual deverá ser puxado os dados</param>
        /// <param name="chaves">Marcar para retornar chave e chave</param>
        /// <returns></returns>
        public static Dictionary<string, string> chave(System.Resources.ResourceManager dados, bool normal = false)
        {
            Dictionary<string, string> retorno = new Dictionary<string, string>();
            IOrderedEnumerable<DictionaryEntry> banco = dados.GetResourceSet(CultureInfo.CurrentUICulture, true, true).OfType<DictionaryEntry>().OrderByDescending(o => o.Value);
            string valor = string.Empty;
            string chave = string.Empty;
            foreach (DictionaryEntry linha in banco)
            {
                valor = linha.Key.ToString();
                chave = linha.Value.ToString();
                if (normal)
                {
                    valor = linha.Value.ToString();
                    chave = linha.Key.ToString();
                }
                retorno.Add(chave, valor);
            }
            return retorno;
        }
        
        #endregion

    }
}
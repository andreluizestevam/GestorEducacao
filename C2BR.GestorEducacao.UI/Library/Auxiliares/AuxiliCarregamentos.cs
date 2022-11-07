//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Library.Auxiliares
{
    public enum EStatusAnalisePrevia
    {
        EmAberto,
        EmAnalise,
        Cancelado,
        Encaminhado,
    }

    public class AuxiliCarregamentos
    {
        #region EscolaW

        #region Carregamento de Ano

        /// <summary>
        /// Carrega o Ano Recebendo o objeto no qual a lista será carregada.
        /// </summary>
        /// <param name="ddlAno"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaAno(DropDownList ddlAno, int CO_EMP, bool Relatorio)
        {
            var res = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                       where CO_EMP != 0 ? tb08.TB25_EMPRESA.CO_EMP == CO_EMP : 0 == 0
                       select new { tb08.CO_ANO_MES_MAT }).ToList().OrderBy(w => w.CO_ANO_MES_MAT).Distinct();

            if (res != null)
            {
                ddlAno.DataTextField = "CO_ANO_MES_MAT";
                ddlAno.DataValueField = "CO_ANO_MES_MAT";
                ddlAno.DataSource = res;
                ddlAno.DataBind();
            }
            string ano = DateTime.Now.Year.ToString();
            if (ddlAno.Items.FindByValue(ano) != null)
                ddlAno.SelectedValue = ano;

            //Verificar se existe uma opção com o ano atual no DropDownList, caso exista, ele deixa ele selecionado, caso contrário, deixa como "Selecione"
            //string ano = DateTime.Now.Year.ToString();
            //ListItem item1 = new ListItem() { Value = ano, Text = ano };

            //if (Relatorio == false)
            //{
            //    ddlAno.Items.Insert(0, new ListItem("Selecione", ""));
            //    ddlAno.SelectedValue = (ddlAno.Items.Contains(item1) ? ano : "");
            //}
            //else
            //{
            //    ddlAno.Items.Insert(0, new ListItem("Todos", "0"));
            //    ddlAno.SelectedValue = (ddlAno.Items.Contains(item1) ? ano : "0");
            //}
        }

        /// <summary>
        /// Carrega os anos existentes na grade de cursos em um determinado DropDownList recebido como parâmetro.
        /// </summary>
        /// <param name="ddlAno"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaAnoGrdCurs(DropDownList ddlAno, int CO_EMP, bool Relatorio)
        {
            var res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                       where tb43.CO_SITU_MATE_GRC == "A"
                       && tb43.CO_EMP == CO_EMP
                       select new { tb43.CO_ANO_GRADE }).ToList().OrderByDescending(w => w.CO_ANO_GRADE).Distinct();

            if (res != null)
            {
                ddlAno.DataTextField = "CO_ANO_GRADE";
                ddlAno.DataValueField = "CO_ANO_GRADE";
                ddlAno.DataSource = res;
                ddlAno.DataBind();
            }
        }

        #endregion

        #region Carregamentos Unidades

        /// <summary>
        /// Carrega Todas as Unidades, recebendo como parâmetro, o DropDownList onde será carregado. De padrão a empresa logada já virá selecionada, mas nos parâmetros isso pode ser alterado.
        /// </summary>
        /// <param name="ddlUnidade"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaUnidade(DropDownList ddlUnidade, int ORG_CODIGO_ORGAO, bool Relatorio, bool mostraPadrao = true, bool selecionaLogada = true, bool InserPrimRegisVazio = false, bool sigla = false)
        {
            var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                       where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == ORG_CODIGO_ORGAO
                       && tb25.CO_SIT_EMP == "A"
                       select new { tb25.NO_FANTAS_EMP, tb25.sigla, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            if (res != null)
            {
                ddlUnidade.DataTextField = !sigla ? "NO_FANTAS_EMP" : "sigla";
                ddlUnidade.DataValueField = "CO_EMP";
                ddlUnidade.DataSource = res;
                ddlUnidade.DataBind();

                if (selecionaLogada)
                    ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
            }

            if (mostraPadrao)
            {
                if (Relatorio == false)
                    ddlUnidade.Items.Insert(0, new ListItem("Selecione", ""));
                else
                    ddlUnidade.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
            {
                //Insere item vazio caso assim tenha sido escolhido
                if (InserPrimRegisVazio)
                    ddlUnidade.Items.Insert(0, new ListItem("", ""));
            }
        }

        /// <summary>
        /// Carrega Todas as Unidades, recebendo como parâmetro, o DropDownList onde será carregado. De padrão a empresa logada já virá selecionada, mas nos parâmetros isso pode ser alterado.
        /// </summary>
        /// <param name="ddlUnidade"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaUnidade(DropDownList ddlUnidade, int ORG_CODIGO_ORGAO, int Cidade, bool Relatorio, bool mostraPadrao = true, bool selecionaLogada = true, bool InserPrimRegisVazio = false, bool sigla = false)
        {
            var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                       where tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == ORG_CODIGO_ORGAO
                       && tb25.CO_SIT_EMP == "A"
                       && (Cidade != 0 ? tb25.CO_CIDADE == Cidade : true)
                       select new { tb25.NO_FANTAS_EMP, tb25.sigla, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            if (res != null)
            {
                ddlUnidade.DataTextField = !sigla ? "NO_FANTAS_EMP" : "sigla";
                ddlUnidade.DataValueField = "CO_EMP";
                ddlUnidade.DataSource = res;
                ddlUnidade.DataBind();

                if (selecionaLogada && ddlUnidade.Items.FindByValue(LoginAuxili.CO_EMP.ToString()) != null)
                    ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
            }

            if (mostraPadrao)
            {
                if (Relatorio == false)
                    ddlUnidade.Items.Insert(0, new ListItem("Selecione", ""));
                else
                    ddlUnidade.Items.Insert(0, new ListItem("Todos", "0"));
            }

            //Insere item vazio caso assim tenha sido escolhido
            if (InserPrimRegisVazio)
                ddlUnidade.Items.Insert(0, new ListItem("", ""));
        }

        #endregion

        #region Carregamento de UF

        /// <summary>
        /// Carrega Todas as UF's recebendo o objeto do DropDownList onde será carregada a Lista.
        /// </summary>
        /// <param name="ddlUF"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaUFs(DropDownList ddlUF, bool Relatorio, int? CO_EMP = null, bool mostraPadrao = true, bool OpcaoBranca = false)
        {
            var res = TB74_UF.RetornaTodosRegistros();

            if (res != null)
            {
                ddlUF.Items.Clear();
                ddlUF.SelectedIndex = -1;
                ddlUF.SelectedValue = null;
                ddlUF.ClearSelection();

                ddlUF.DataTextField = "CODUF";
                ddlUF.DataValueField = "CODUF";
                ddlUF.DataSource = res;
                ddlUF.DataBind();
            }

            if (mostraPadrao)
            {
                if (Relatorio == false)
                    ddlUF.Items.Insert(0, new ListItem("Selecione", ""));
                else
                    ddlUF.Items.Insert(0, new ListItem("Todos", "0"));
            }

            //Seleciona a UF da empresa recebida como parâmetro ou a logada, dependendo se foi recebida ou não
            if (!CO_EMP.HasValue)
                ddlUF.SelectedValue = LoginAuxili.CO_UF_EMP.ToString();
            else
                ddlUF.SelectedValue = TB25_EMPRESA.RetornaPelaChavePrimaria(CO_EMP.Value).CO_UF_EMP;

            if (OpcaoBranca)
                ddlUF.Items.Insert(0, new ListItem("", ""));
        }

        #endregion

        #region Carregamento de Cidades

        /// <summary>
        /// Carrega as Cidades de acordo com a UF recebida, e seleciona a cidade da empresa recebida como parâmetro respeitando o bool selecCidadEmp, que quando falso não seleciona
        /// </summary>
        /// <param name="ddlCidade"></param>
        /// <param name="Relatorio"></param>
        /// <param name="CO_UF"></param>
        /// <param name="CO_EMP_SELEC_CID"></param>
        /// <param name="selecCidadEmp"></param>
        public static void CarregaCidades(DropDownList ddlCidade, bool Relatorio, string CO_UF, int CO_EMP_SELEC_CID, bool selecCidadEmp = false, bool mostraPadrao = false, bool OpcaoBranca = false)
        {
            var res = (from tb904 in TB904_CIDADE.RetornaTodosRegistros()
                       where tb904.CO_UF == CO_UF
                       select new { tb904.CO_CIDADE, tb904.NO_CIDADE }).ToList();

            if (res.Count > 0)
            {
                ddlCidade.Items.Clear();
                ddlCidade.SelectedIndex = -1;
                ddlCidade.SelectedValue = null;
                ddlCidade.ClearSelection();

                ddlCidade.DataTextField = "NO_CIDADE";
                ddlCidade.DataValueField = "CO_CIDADE";
                ddlCidade.DataSource = res;
                ddlCidade.DataBind();
            }

            if (mostraPadrao)
            {
                if (Relatorio)
                    ddlCidade.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddlCidade.Items.Insert(0, new ListItem("Selecione", ""));

                if (selecCidadEmp)
                {
                    int cidEmp = TB25_EMPRESA.RetornaPelaChavePrimaria(CO_EMP_SELEC_CID).CO_CIDADE;

                    ListItem li = ddlCidade.Items.FindByValue(cidEmp.ToString());
                    if (li != null)
                        ddlCidade.SelectedValue = cidEmp.ToString();
                }
            }

            if (OpcaoBranca)
                ddlCidade.Items.Insert(0, new ListItem("", ""));
        }

        #endregion

        #region Carregamento de Bairros

        /// <summary>
        /// Carrega todos os bairros em controle DropDonwList de acordo com os parâmetros recebidos
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="CO_UF"></param>
        /// <param name="CO_CIDADE"></param>
        /// <param name="Relatorio"></param>
        /// <param name="mostraPadra"></param>
        public static void CarregaBairros(DropDownList ddl, string CO_UF, int CO_CIDADE, bool Relatorio, bool mostraPadra = true, bool OpcaoBranca = false, int? CO_EMP_SELEC_CID = null, bool selecCidadEmp = false)
        {
            var res = (from tb905 in TB905_BAIRRO.RetornaTodosRegistros()
                       where tb905.CO_CIDADE == CO_CIDADE
                       && tb905.CO_UF == CO_UF
                       select new { tb905.NO_BAIRRO, tb905.CO_BAIRRO }).OrderBy(w => w.NO_BAIRRO).ToList();

            if (res.Count > 0)
            {
                ddl.Items.Clear();
                ddl.SelectedIndex = -1;
                ddl.SelectedValue = null;
                ddl.ClearSelection();

                ddl.DataTextField = "NO_BAIRRO";
                ddl.DataValueField = "CO_BAIRRO";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (mostraPadra)
            {
                if (Relatorio)
                    ddl.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddl.Items.Insert(0, new ListItem("Selecione", ""));

                if (selecCidadEmp && CO_EMP_SELEC_CID.HasValue)
                {
                    int cidEmp = TB25_EMPRESA.RetornaPelaChavePrimaria(CO_EMP_SELEC_CID.Value).CO_BAIRRO;

                    ListItem li = ddl.Items.FindByValue(cidEmp.ToString());
                    if (li != null)
                        ddl.SelectedValue = cidEmp.ToString();
                }
            }

            if (OpcaoBranca)
                ddl.Items.Insert(0, new ListItem("", ""));
        }

        #endregion

        #region Carregamentos de Professores

        /// <summary>
        /// Carrega os professores responsáveis por uma determinada matéria. (Caso não haja valores, enviar o valor 0)
        /// </summary>
        public static void CarregaProfessoresRespMateria(DropDownList ddlProfessor, int coUnid, int modalidade, int serieCurso, int turma, int disciplina, int ano, bool DesabilitaVazio = true, bool DiarioClasse = false)
        {

            var res = ddlProfessor.DataSource = (from tb03 in TB03_COLABOR.RetornaPelaEmpresa(coUnid)
                                                 join tbRM in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb03.CO_COL equals tbRM.CO_COL_RESP
                                                 where (
                              (modalidade != null ? tbRM.CO_MODU_CUR == modalidade : 0 == 0)
                              && (serieCurso != 0 ? tbRM.CO_CUR == serieCurso : 0 == 0)
                              && (turma != 0 ? tbRM.CO_TUR == turma : 0 == 0)
                              && (disciplina != 0 ? tbRM.CO_MAT == disciplina : 0 == 0)
                              && (tb03.FLA_PROFESSOR == "S")
                              && (ano != 0 ? tbRM.CO_ANO_REF == ano : 0 == 0)
                              )
                                                 select new { tb03.NO_COL, tb03.CO_COL }).Distinct().OrderBy(c => c.NO_COL);

            if (DiarioClasse)
            {
                res = ddlProfessor.DataSource = (from tb03 in TB03_COLABOR.RetornaPelaEmpresa(coUnid)
                                                 join tbRM in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb03.CO_COL equals tbRM.CO_COL_RESP
                                                 join tb132 in TB132_FREQ_ALU.RetornaTodosRegistros() on tb03.CO_COL equals tb132.CO_COL
                                                 where (
                              (modalidade != null ? tbRM.CO_MODU_CUR == modalidade : 0 == 0)
                              && (serieCurso != 0 ? tbRM.CO_CUR == serieCurso : 0 == 0)
                              && (turma != 0 ? tbRM.CO_TUR == turma : 0 == 0)
                              && (disciplina != 0 ? tbRM.CO_MAT == disciplina : 0 == 0)
                              && (tb03.FLA_PROFESSOR == "S")
                              && (ano != 0 ? tbRM.CO_ANO_REF == ano : 0 == 0)
                              )
                                                 select new { tb03.NO_COL, tb03.CO_COL }).Distinct().OrderBy(c => c.NO_COL);
            }

            if (res != null)
            {
                ddlProfessor.DataTextField = "NO_COL";
                ddlProfessor.DataValueField = "CO_COL";
                ddlProfessor.DataSource = res;
                ddlProfessor.DataBind();
            }

            if (DesabilitaVazio)
            {
                if (ddlProfessor.Items.Count == 0)
                {
                    ddlProfessor.Enabled = false;
                    ddlProfessor.Items.Insert(0, new ListItem("Não Existem Professores Nestes Parâmetros", ""));
                }
                else
                    ddlProfessor.Enabled = true;
            }
        }

        /// <summary>
        /// Carrega todos os Professores de uma determinada unidade.
        /// </summary>
        /// <param name="ddlProfessor"></param>
        /// <param name="coEmp"></param>
        public static void carregaProfessores(DropDownList ddlProfessor, int coEmp, bool Relatorio = true, bool MostraPadrao = false)
        {
            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       where tb03.FLA_PROFESSOR == "S"
                       && tb03.CO_EMP == coEmp
                       select new { tb03.CO_COL, tb03.NO_COL }).ToList();

            if (res != null)
            {
                ddlProfessor.DataTextField = "NO_COL";
                ddlProfessor.DataValueField = "CO_COL";
                ddlProfessor.DataSource = res;
                ddlProfessor.DataBind();
            }

            ddlProfessor.Enabled = true;

            if (MostraPadrao)
            {
                if (Relatorio)
                    ddlProfessor.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddlProfessor.Items.Insert(0, new ListItem("Selecione", ""));
            }



        }

        #endregion

        #region Carregamentos de Tipos de Atividades

        /// <summary>
        /// Carrega todos os tipos de atividades de acordo com as informações recebidas como parâmetro
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        /// <param name="MostraPadrao"></param>
        public static void CarregaTiposAtividades(DropDownList ddl, bool Relatorio, bool MostraPadrao = true)
        {
            var res = (from tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros()
                       where tb273.CO_SITUA_ATIV == "A"
                       select new { tb273.ID_TIPO_ATIV, tb273.NO_TIPO_ATIV }).OrderBy(o => o.NO_TIPO_ATIV);

            ddl.DataTextField = "NO_TIPO_ATIV";
            ddl.DataValueField = "ID_TIPO_ATIV";
            ddl.DataSource = res;
            ddl.DataBind();

            if (MostraPadrao)
            {
                if (Relatorio)
                    ddl.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddl.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        #endregion

        #region Carregamento de Modalidades

        /// <summary>
        /// Carrega Todas as Modalidades.
        /// </summary>
        /// <param name="ddlmodalidade"></param>
        /// <param name="relatorio"></param>
        public static void carregaModalidades(DropDownList ddlmodalidade, int ORG_CODIGO_ORGAO, bool relatorio, bool InserPrimRegisVazio = false, bool MostraPadrao = true, bool NovoCarregaPadrao = false)
        {
            var res = (from tb44 in TB44_MODULO.RetornaTodosRegistros()
                       where tb44.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == ORG_CODIGO_ORGAO
                       select new { tb44.CO_MODU_CUR, tb44.DE_MODU_CUR }).ToList();

            if (res != null)
            {
                ddlmodalidade.DataTextField = "DE_MODU_CUR";
                ddlmodalidade.DataValueField = "CO_MODU_CUR";
                ddlmodalidade.DataSource = res;
                ddlmodalidade.DataBind();
            }

            if (MostraPadrao == true) //Se é para mostrar o padrão
            {
                /* Inserido esse if com valor padrão false para não dar problema nos carregamentos de outras páginas */
                if (NovoCarregaPadrao)
                {
                    //Se for relatório, tendo mais de 2 registros ou não, eu preciso inserir a opção todos
                    if (relatorio)
                        ddlmodalidade.Items.Insert(0, new ListItem("Todos", "0"));
                    else if (res.Count() != 1) //Se não for relatório, eu insiro a opção "Selecione" apenas se houverem 0 ou 2 ou mais registros
                        ddlmodalidade.Items.Insert(0, new ListItem("Selecione", ""));
                }
                else
                    if (relatorio)
                        ddlmodalidade.Items.Insert(0, new ListItem("Todos", "0"));
                    else
                        ddlmodalidade.Items.Insert(0, new ListItem("Selecione", ""));
            }

            //Insere item vazio caso assim tenha sido escolhido
            if (InserPrimRegisVazio)
                ddlmodalidade.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Carrega as Modalidades relacionadas à um determinado professor, o qual o código é recebido no parâmetro
        /// </summary>
        /// <param name="ddlModalidade"></param>
        /// <param name="ORG_CODIGO_ORGAO"></param>
        /// <param name="CO_COL"></param>
        /// <param name="relatorio"></param>
        public static void carregaModalidadesProfeResp(DropDownList ddlModalidade, int CO_COL, int CO_ANO_REF, bool relatorio, bool MostraPadrao = true)
        {
            if (LoginAuxili.CLASSIFICACAO_USU_LOGADO == "M") // Se for usuário MASTER, carrega todas as disciplinas
            {
                carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, relatorio);
                return;
            }
            else // Se não for usuário MASTER, carrega apenas as disciplinas associadas
            {
                var res = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                           join mo in TB44_MODULO.RetornaTodosRegistros() on rm.CO_MODU_CUR equals mo.CO_MODU_CUR
                           where rm.CO_COL_RESP == CO_COL
                            && rm.CO_ANO_REF == CO_ANO_REF
                           select new
                           {
                               mo.DE_MODU_CUR,
                               rm.CO_MODU_CUR
                           }).Distinct();

                if (res != null)
                {
                    ddlModalidade.DataTextField = "DE_MODU_CUR";
                    ddlModalidade.DataValueField = "CO_MODU_CUR";
                    ddlModalidade.DataSource = res;
                    ddlModalidade.DataBind();
                }

                if ((res.Count() != 1) && (MostraPadrao == true))
                {
                    if (relatorio == false)
                        ddlModalidade.Items.Insert(0, new ListItem("Selecione", ""));
                    else
                        ddlModalidade.Items.Insert(0, new ListItem("Todos", "0"));
                }
            }
        }

        #endregion

        #region Carregamento de Cursos

        /// <summary>
        /// Carrega todas as Séries e Cursos de acordo com a modalidade selecionada
        /// </summary>
        /// <param name="ddlSerieCurso"></param>
        /// <param name="modalidade"></param>
        /// <param name="relatorio"></param>
        public static void carregaSeriesCursos(DropDownList ddlSerieCurso, int modalidade, int CO_EMP, bool relatorio, bool mostraSigla = false, bool InserPrimRegisVazio = false, bool MostraPadrao = true, bool NovoCarregaPadrao = false)
        {


            var res = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                       where tb01.CO_MODU_CUR == modalidade
                               && tb01.CO_EMP == CO_EMP
                       select new { tb01.CO_CUR, tb01.NO_CUR, tb01.CO_SIGL_CUR }).ToList();

            if (res != null)
            {
                //Trata de mostrar nome completo ou sigla de acordo com parâmetros
                if (mostraSigla == false)
                    ddlSerieCurso.DataTextField = "NO_CUR";
                else
                    ddlSerieCurso.DataTextField = "CO_SIGL_CUR";

                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataSource = res;
                ddlSerieCurso.DataBind();
            }

            if (MostraPadrao == true) //Se é para mostrar o padrão
            {
                /* Inserido esse if com valor padrão false para não dar problema nos carregamentos de outras páginas */
                if (NovoCarregaPadrao)
                {
                    //Se for relatório, tendo mais de 2 registros ou não, eu preciso inserir a opção todos
                    if (relatorio)
                        ddlSerieCurso.Items.Insert(0, new ListItem("Todos", "0"));
                    else if (res.Count() != 1) //Se não for relatório, eu insiro a opção "Selecione" apenas se houverem 0 ou 2 ou mais registros
                        ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
                }
                else
                    if (relatorio)
                        ddlSerieCurso.Items.Insert(0, new ListItem("Todos", "0"));
                    else
                        ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
            }

            //Insere item vazio caso assim tenha sido escolhido
            if (InserPrimRegisVazio)
                ddlSerieCurso.Items.Insert(0, new ListItem("", ""));


        }

        /// <summary>
        /// Carrega todos os Cursos em um determinado DropDown recebido como parâmetro, de acordo com o professor responsável recebido como parâmetro.
        /// </summary>
        /// <param name="ddlSerieCurso"></param>
        /// <param name="modalidade"></param>
        /// <param name="CO_COL"></param>
        /// <param name="CO_ANO_REF"></param>
        /// <param name="Relatorio"></param>
        public static void carregaSeriesCursosProfeResp(DropDownList ddlSerieCurso, int modalidade, int CO_COL, int CO_ANO_REF, bool Relatorio, bool MostraPadrao = true)
        {
            if (LoginAuxili.CLASSIFICACAO_USU_LOGADO == "M")
            {
                carregaSeriesCursos(ddlSerieCurso, modalidade, LoginAuxili.CO_EMP, Relatorio);
                return;
            }
            else
            {
                var res = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                           join c in TB01_CURSO.RetornaTodosRegistros() on rm.CO_CUR equals c.CO_CUR
                           //join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on rm.CO_CUR equals tb43.CO_CUR
                           where rm.CO_COL_RESP == CO_COL
                           && rm.CO_MODU_CUR == modalidade
                           && rm.CO_ANO_REF == CO_ANO_REF
                           select new
                           {
                               c.NO_CUR,
                               rm.CO_CUR
                           }).OrderByDescending(w => w.NO_CUR).Distinct();

                if (res != null)
                {
                    ddlSerieCurso.DataTextField = "NO_CUR";
                    ddlSerieCurso.DataValueField = "CO_CUR";
                    ddlSerieCurso.DataSource = res;
                    ddlSerieCurso.DataBind();
                }

                if ((res.Count() != 1) && (MostraPadrao == true))
                {
                    if (Relatorio == false)
                        ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
                    else
                        ddlSerieCurso.Items.Insert(0, new ListItem("Todos", "0"));
                }
            }
        }

        /// <summary>
        /// Carrega um controle DropDownList recebido como parâmetro com a lista de cursos de uma determinada grade respeitando o ano, modalidade e unidade
        /// </summary>
        /// <param name="ddlSerieCurso"></param>
        /// <param name="modalidade"></param>
        /// <param name="anoGrade"></param>
        /// <param name="coEmp"></param>
        /// <param name="relatorio"></param>
        public static void carregaSeriesGradeCurso(DropDownList ddlSerieCurso, int modalidade, string anoGrade, int coEmp, bool relatorio)
        {
            var res = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                       where tb01.CO_MODU_CUR == modalidade
                       join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(coEmp) on tb01.CO_CUR equals tb43.CO_CUR
                       where tb43.CO_ANO_GRADE == anoGrade && tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                       select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

            if (res != null)
            {
                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataSource = res;
                ddlSerieCurso.DataBind();
            }

            if (relatorio == false)
                ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddlSerieCurso.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion

        #region Carregamento de Turmas

        /// <summary>
        /// Método Responsável por carregar as Turmas de Acordo com as informações passadas nos parâmetros
        /// </summary>
        /// <param name="ddlTurma"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="coMod"></param>
        /// <param name="coCur"></param>
        /// <param name="relatorio"></param>
        public static void CarregaTurmas(DropDownList ddlTurma, int CO_EMP, int CO_MODU_CUR, int CO_CUR, bool relatorio, bool InserPrimRegisVazio = false, bool MostraPadrao = true, bool NovoCarregaPadrao = false)
        {
            var res = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                       join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb06.CO_TUR equals tb129.CO_TUR
                       where tb06.CO_EMP == CO_EMP
                     && tb06.CO_MODU_CUR == CO_MODU_CUR
                     && tb06.CO_CUR == CO_CUR
                       select new
                       {
                           tb129.NO_TURMA,
                           tb06.CO_TUR
                       });

            if (res != null)
            {
                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataSource = res;
                ddlTurma.DataBind();
            }

            if (MostraPadrao == true) //Se é para mostrar o padrão
            {
                /* Inserido esse if com valor padrão false para não dar problema nos carregamentos de outras páginas */
                if (NovoCarregaPadrao)
                {
                    //Se for relatório, tendo mais de 2 registros ou não, eu preciso inserir a opção todos
                    if (relatorio)
                        ddlTurma.Items.Insert(0, new ListItem("Todos", "0"));
                    else if (res.Count() != 1) //Se não for relatório, eu insiro a opção "Selecione" apenas se houverem 0 ou 2 ou mais registros
                        ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
                }
                else
                    if (relatorio)
                        ddlTurma.Items.Insert(0, new ListItem("Todos", "0"));
                    else
                        ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
            }

            //Insere item vazio caso assim tenha sido escolhido
            if (InserPrimRegisVazio)
                ddlTurma.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método Responsável por carregar as Turmas de Acordo com as informações passadas nos parâmetros (CARREGANDO AS SIGLAS)
        /// </summary>
        /// <param name="ddlTurma"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="coMod"></param>
        /// <param name="coCur"></param>
        /// <param name="relatorio"></param>
        public static void CarregaTurmasSigla(DropDownList ddlTurma, int CO_EMP, int CO_MODU_CUR, int CO_CUR, bool relatorio)
        {
            var res = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                       join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb06.CO_TUR equals tb129.CO_TUR
                       where tb06.CO_EMP == CO_EMP
                     && tb06.CO_MODU_CUR == CO_MODU_CUR
                     && tb06.CO_CUR == CO_CUR
                       select new
                       {
                           tb129.CO_SIGLA_TURMA,
                           tb06.CO_TUR
                       });

            if (res != null)
            {
                ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataSource = res;
                ddlTurma.DataBind();
            }

            if (relatorio == false)
                ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddlTurma.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Carrega um determinado DropDown de Turmas, de acordo com o professor responsável recebido como parâmetro
        /// </summary>
        /// <param name="ddlTurma"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="CO_MODU_CUR"></param>
        /// <param name="CO_CUR"></param>
        /// <param name="CO_COL"></param>
        /// <param name="CO_ANO_REF"></param>
        /// <param name="relatorio"></param>
        public static void CarregaTurmasProfeResp(DropDownList ddlTurma, int CO_EMP, int CO_MODU_CUR, int CO_CUR, int CO_COL, int CO_ANO_REF, bool relatorio, bool MostraPadrao = true)
        {
            if (LoginAuxili.CLASSIFICACAO_USU_LOGADO == "M")
            {
                CarregaTurmas(ddlTurma, CO_EMP, CO_MODU_CUR, CO_CUR, relatorio);
                return;
            }
            else
            {
                var res = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                           join t in TB129_CADTURMAS.RetornaTodosRegistros() on rm.CO_TUR equals t.CO_TUR
                           where rm.CO_COL_RESP == CO_COL
                          && rm.CO_MODU_CUR == CO_MODU_CUR
                          && rm.CO_CUR == CO_CUR
                          && rm.CO_ANO_REF == CO_ANO_REF
                           select new
                           {
                               t.NO_TURMA,
                               rm.CO_TUR,
                           }).Distinct();

                if (res != null)
                {
                    ddlTurma.DataTextField = "NO_TURMA";
                    ddlTurma.DataValueField = "CO_TUR";
                    ddlTurma.DataSource = res;
                    ddlTurma.DataBind();
                }

                if ((res.Count() != 1) && (MostraPadrao == true))
                {
                    if (relatorio == false)
                        ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
                    else
                        ddlTurma.Items.Insert(0, new ListItem("Todos", "0"));
                }
            }

        }

        /// <summary>
        /// Carrega um determinado DropDown de Turmas, de acordo com o professor responsável recebido como parâmetro
        /// </summary>
        /// <param name="ddlTurma"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="CO_MODU_CUR"></param>
        /// <param name="CO_CUR"></param>
        /// <param name="CO_COL"></param>
        /// <param name="CO_ANO_REF"></param>
        /// <param name="relatorio"></param>
        public static void CarregaTurmasSiglaProfeResp(DropDownList ddlTurma, int CO_EMP, int CO_MODU_CUR, int CO_CUR, int CO_COL, int CO_ANO_REF, bool relatorio, bool MostraPadrao = true)
        {
            if (LoginAuxili.CLASSIFICACAO_USU_LOGADO == "M")
            {
                CarregaTurmasSigla(ddlTurma, CO_EMP, CO_MODU_CUR, CO_CUR, relatorio);
                return;
            }
            else
            {
                var res = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                           join t in TB129_CADTURMAS.RetornaTodosRegistros() on rm.CO_TUR equals t.CO_TUR
                           where rm.CO_COL_RESP == CO_COL
                          && rm.CO_MODU_CUR == CO_MODU_CUR
                          && rm.CO_CUR == CO_CUR
                          && rm.CO_ANO_REF == CO_ANO_REF
                           select new
                           {
                               t.CO_SIGLA_TURMA,
                               rm.CO_TUR,
                           }).Distinct();

                if (res != null)
                {
                    ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                    ddlTurma.DataValueField = "CO_TUR";
                    ddlTurma.DataSource = res;
                    ddlTurma.DataBind();
                }

                if ((res.Count() != 1) && (MostraPadrao == true))
                {
                    if (relatorio == false)
                        ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
                    else
                        ddlTurma.Items.Insert(0, new ListItem("Todos", "0"));
                }
            }

        }

        #endregion

        #region Carregamento de Matérias

        /// <summary>
        /// Carrega as Matérias de acordo com os parâmetros passados.
        /// </summary>
        /// <param name="ddlMateria"></param>
        /// <param name="modalidade"></param>
        /// <param name="serie"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaMateriasGradeCurso(DropDownList ddlMateria, int CO_EMP, int CO_MODU_CUR, int CO_CUR, string CO_ANO_GRADE, bool Relatorio, bool MostraPadrao = true, bool ComDisciplinasFilhas = true, bool DiarioClasse = false)
        {
            var res = ddlMateria.DataSource;

            if (DiarioClasse)
            {
                res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                       where tb43.TB44_MODULO.CO_MODU_CUR == CO_MODU_CUR && tb43.CO_CUR == CO_CUR && tb43.CO_ANO_GRADE == CO_ANO_GRADE && tb43.CO_EMP == CO_EMP && (!ComDisciplinasFilhas ? tb43.ID_MATER_AGRUP == null : true)
                       join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                       join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                       join tb132 in TB132_FREQ_ALU.RetornaTodosRegistros() on tb02.CO_MAT equals tb132.CO_MAT
                       select new { tb02.CO_MAT, tb107.NO_MATERIA }).Distinct().OrderBy(m => m.NO_MATERIA);
            }
            else
            {
                res = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                       where tb43.TB44_MODULO.CO_MODU_CUR == CO_MODU_CUR && tb43.CO_CUR == CO_CUR && tb43.CO_ANO_GRADE == CO_ANO_GRADE && tb43.CO_EMP == CO_EMP && (!ComDisciplinasFilhas ? tb43.ID_MATER_AGRUP == null : true)
                       join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                       join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                       select new { tb02.CO_MAT, tb107.NO_MATERIA }).Distinct().OrderBy(m => m.NO_MATERIA);
            }

            if (res != null)
            {
                ddlMateria.DataTextField = "NO_MATERIA";
                ddlMateria.DataValueField = "CO_MAT";
                ddlMateria.DataSource = res;
                ddlMateria.DataBind();
            }

            if (MostraPadrao)
            {
                if (Relatorio == false)
                    ddlMateria.Items.Insert(0, new ListItem("Selecione", ""));
                else
                    ddlMateria.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }

        /// <summary>
        /// Carrega Todas as Matérias associadas à um determinado professor de acordo com o parâmetro informado
        /// </summary>
        /// <param name="ddlMateria"></param>
        /// <param name="CO_COL"></param>
        /// <param name="CO_MODU_CUR"></param>
        /// <param name="CO_CUR"></param>
        /// <param name="CO_ANO_GRADE"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaMateriasProfeRespon(DropDownList ddlMateria, int CO_COL, int CO_MODU_CUR, int CO_CUR, int CO_ANO_GRADE, bool Relatorio, bool MostraPadrao = true, bool ComDisciplinasFilhas = true, bool DiarioClasse = false)
        {
            string ano = CO_ANO_GRADE.ToString();

            if (LoginAuxili.CLASSIFICACAO_USU_LOGADO == "M")
            {
                CarregaMateriasGradeCurso(ddlMateria, LoginAuxili.CO_EMP, CO_MODU_CUR, CO_CUR, ano, Relatorio, MostraPadrao, ComDisciplinasFilhas, DiarioClasse);
            }
            else
            {
                var res = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                           join tb02 in TB02_MATERIA.RetornaTodosRegistros() on rm.CO_MAT equals tb02.CO_MAT
                           join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                           join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on rm.TB01_CURSO.CO_CUR equals tb43.CO_CUR
                           where rm.CO_COL_RESP == CO_COL
                          && rm.CO_MODU_CUR == CO_MODU_CUR
                          && rm.CO_CUR == CO_CUR
                          && rm.CO_ANO_REF == CO_ANO_GRADE
                          && tb43.CO_ANO_GRADE == ano
                          && tb43.TB44_MODULO.CO_MODU_CUR == CO_MODU_CUR
                          && tb43.CO_CUR == CO_CUR
                          && (!ComDisciplinasFilhas ? tb43.ID_MATER_AGRUP == null : 0 == 0)
                           //&& rm.CO_TUR == turma (tirado pq não está sendo atribuido)
                           select new
                           {
                               tb107.NO_MATERIA,
                               rm.CO_MAT,
                           }).OrderBy(w => w.NO_MATERIA).Distinct();

                if (DiarioClasse)
                {
                    res = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                           join tb02 in TB02_MATERIA.RetornaTodosRegistros() on rm.CO_MAT equals tb02.CO_MAT
                           join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                           join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on rm.TB01_CURSO.CO_CUR equals tb43.CO_CUR
                           join tb132 in TB132_FREQ_ALU.RetornaTodosRegistros() on rm.CO_COL_RESP equals tb132.CO_COL
                           where rm.CO_COL_RESP == CO_COL
                          && rm.CO_MODU_CUR == CO_MODU_CUR
                          && rm.CO_CUR == CO_CUR
                          && rm.CO_ANO_REF == CO_ANO_GRADE
                          && tb43.CO_ANO_GRADE == ano
                          && tb43.TB44_MODULO.CO_MODU_CUR == CO_MODU_CUR
                          && tb43.CO_CUR == CO_CUR
                          && (!ComDisciplinasFilhas ? tb43.ID_MATER_AGRUP == null : 0 == 0)
                           //&& rm.CO_TUR == turma (tirado pq não está sendo atribuido)
                           select new
                           {
                               tb107.NO_MATERIA,
                               rm.CO_MAT,
                           }).OrderBy(w => w.NO_MATERIA).Distinct();
                }

                if (res != null)
                {
                    ddlMateria.DataTextField = "NO_MATERIA";
                    ddlMateria.DataValueField = "CO_MAT";
                    ddlMateria.DataSource = res;
                    ddlMateria.DataBind();
                }

                if ((res.Count() != 1) && (MostraPadrao == true))
                {
                    if (Relatorio == false)
                        ddlMateria.Items.Insert(0, new ListItem("Selecione", ""));
                    else
                        ddlMateria.Items.Insert(0, new ListItem("Todos", "0"));
                }


            }


        }

        #endregion

        #region Carregamento de Medidas Temporais

        /// <summary>
        /// Carrega os tipos de medidas temporais
        /// </summary>
        public static void CarregaTiposMedidasTemporais(DropDownList ddl, bool Relatorio, bool mostraPadrao = false)
        {
            ddl.Items.Clear();

            ddl.Items.Insert(0, new ListItem("Semestre", "S"));
            ddl.Items.Insert(0, new ListItem("Trimestre", "T"));
            ddl.Items.Insert(0, new ListItem("Bimestre", "B"));

            if (mostraPadrao)
                if (Relatorio)
                    ddl.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega as medidas temporais
        /// </summary>
        public static void CarregaMedidasTemporais(DropDownList ddl, bool Relatorio, string tipo, bool medFinal = false, bool mostraPadrao = false)
        {
            ddl.Items.Clear();

            if (medFinal)
                ddl.Items.Insert(0, new ListItem("Média Final", "MF"));

            switch (tipo)
            {
                /*case "M":
                    ddl.Items.Insert(0, new ListItem("Dezembro", "M12"));
                    ddl.Items.Insert(0, new ListItem("Novembro", "M11"));
                    ddl.Items.Insert(0, new ListItem("Outubro", "M10"));
                    ddl.Items.Insert(0, new ListItem("Setembro", "M9"));
                    ddl.Items.Insert(0, new ListItem("Agosto", "M8"));
                    ddl.Items.Insert(0, new ListItem("Julho", "M7"));
                    ddl.Items.Insert(0, new ListItem("Junho", "M6"));
                    ddl.Items.Insert(0, new ListItem("Maio", "M5"));
                    ddl.Items.Insert(0, new ListItem("Abril", "M4"));
                    ddl.Items.Insert(0, new ListItem("Março", "M3"));
                    ddl.Items.Insert(0, new ListItem("Fevereiro", "M2"));
                    ddl.Items.Insert(0, new ListItem("Janeiro", "M1"));
                    break;*/
                case "B":
                    ddl.Items.Insert(0, new ListItem("4º Bimestre", "B4"));
                    ddl.Items.Insert(0, new ListItem("3º Bimestre", "B3"));
                    ddl.Items.Insert(0, new ListItem("2º Bimestre", "B2"));
                    ddl.Items.Insert(0, new ListItem("1º Bimestre", "B1"));
                    break;
                case "T":
                    ddl.Items.Insert(0, new ListItem("3º Trimestre", "T3"));
                    ddl.Items.Insert(0, new ListItem("2º Trimestre", "T2"));
                    ddl.Items.Insert(0, new ListItem("1º Trimestre", "T1"));
                    break;
                case "S":
                    ddl.Items.Insert(0, new ListItem("2º Semestre", "S2"));
                    ddl.Items.Insert(0, new ListItem("1º Semestre", "S1"));
                    break;
            }

            if (mostraPadrao)
                if (Relatorio)
                    ddl.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        #endregion

        #region Carregamento de Alunos

        /// <summary>
        /// Carrega os Alunos Matriculados de acordo com os parâmetros informados
        /// </summary>
        /// <param name="ddlAluno"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="CO_MODU_CUR"></param>
        /// <param name="CO_CUR"></param>
        /// <param name="CO_TUR"></param>
        /// <param name="CO_ANO_MES_MAT"></param>
        /// <param name="relatorio"></param>
        public static void CarregaAlunoMatriculado(DropDownList ddlAluno, int CO_EMP, int CO_MODU_CUR, int CO_CUR, int CO_TUR, string CO_ANO_MES_MAT, bool relatorio, bool AlunoMatriculaFinalizada = false)
        {
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb07.CO_ALU equals tb08.CO_ALU
                       where (CO_EMP != 0 ? tb08.CO_EMP == CO_EMP : 0 == 0)
                       && (CO_MODU_CUR != 0 ? tb08.TB44_MODULO.CO_MODU_CUR == CO_MODU_CUR : 0 == 0)
                       && (CO_CUR != 0 ? tb08.CO_CUR == CO_CUR : 0 == 0)
                       && (CO_TUR != 0 ? tb08.CO_TUR == CO_TUR : 0 == 0)
                       && tb08.CO_ANO_MES_MAT == CO_ANO_MES_MAT
                           //Filtra com os alunos finalizados também caso bool correspondente tenha sido recebido como true
                       && (AlunoMatriculaFinalizada ? tb08.CO_SIT_MAT == "A" || tb08.CO_SIT_MAT == "F" : tb08.CO_SIT_MAT == "A")
                       select new
                       {
                           tb07.NO_ALU,
                           tb07.CO_ALU
                       }).Distinct().OrderBy(o => o.NO_ALU);

            if (res != null)
            {
                ddlAluno.DataTextField = "NO_ALU";
                ddlAluno.DataValueField = "CO_ALU";
                ddlAluno.DataSource = res;
                ddlAluno.DataBind();
            }

            if (relatorio == false)
                ddlAluno.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddlAluno.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Carrega os Alunos Matriculados de acordo com os parâmetros informados sem filtrar pela Turma
        /// </summary>
        /// <param name="ddlAluno"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="CO_MODU_CUR"></param>
        /// <param name="CO_CUR"></param>
        /// <param name="CO_ANO_MES_MAT"></param>
        /// <param name="relatorio"></param>
        public static void CarregaAlunoMatriculadoSemTurma(DropDownList ddlAluno, int CO_EMP, int CO_MODU_CUR, int CO_CUR, string CO_ANO_MES_MAT, bool relatorio)
        {
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb07.CO_ALU equals tb08.CO_ALU
                       where tb08.CO_EMP == CO_EMP
                       && tb08.TB44_MODULO.CO_MODU_CUR == CO_MODU_CUR
                       && tb08.CO_CUR == CO_CUR
                           //&& tb08.CO_TUR == CO_TUR
                       && tb08.CO_ANO_MES_MAT == CO_ANO_MES_MAT
                       && tb08.CO_SIT_MAT == "A"
                       select new
                       {
                           tb07.NO_ALU,
                           tb07.CO_ALU
                       }).Distinct().OrderBy(o => o.NO_ALU);

            if (res != null)
            {
                ddlAluno.DataTextField = "NO_ALU";
                ddlAluno.DataValueField = "CO_ALU";
                ddlAluno.DataSource = res;
                ddlAluno.DataBind();
            }

            if (relatorio == false)
                ddlAluno.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddlAluno.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Carrega todos os Alunos Matriculados associados à um determinado professor, de acordo com os parâmetros informados
        /// </summary>
        /// <param name="ddlAluno"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="CO_MODU_CUR"></param>
        /// <param name="CO_CUR"></param>
        /// <param name="CO_TUR"></param>
        /// <param name="CO_COL"></param>
        /// <param name="CO_ANO_MES_MAT"></param>
        /// <param name="relatorio"></param>
        public static void CarregaAlunoMatriculadoProfeResp(DropDownList ddlAluno, int CO_EMP, int CO_COL, int CO_ANO_REF, bool relatorio)
        {
            var res = (from rm in TB_RESPON_MATERIA.RetornaTodosRegistros()
                       join tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros() on rm.CO_MAT equals tb48.CO_MAT
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb48.CO_ALU equals tb07.CO_ALU
                       //join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb07.CO_ALU equals tb08.CO_ALU                       
                       where rm.CO_EMP == CO_EMP
                       && rm.CO_COL_RESP == CO_COL
                           //&& rm.CO_MAT == tb48.CO_MAT
                           //&& rm.CO_MODU_CUR == CO_MODU_CUR
                           //&& rm.CO_CUR == CO_CUR
                           //&& tb08.CO_TUR == CO_TUR
                       && rm.CO_ANO_REF == CO_ANO_REF
                       //&& tb08.CO_SIT_MAT == "A"
                       select new
                       {
                           tb07.NO_ALU,
                           tb07.CO_ALU
                       }).Distinct().OrderBy(o => o.NO_ALU);

            if (res != null)
            {
                ddlAluno.DataTextField = "NO_ALU";
                ddlAluno.DataValueField = "CO_ALU";
                ddlAluno.DataSource = res;
                ddlAluno.DataBind();
            }

            if (relatorio == false)
                ddlAluno.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddlAluno.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Carrega uma lista em um determinado objeto recebido como parâmetro, de alunos relacionados à um determinado responsável recebido como parâmetro.
        /// </summary>
        /// <param name="ddlAluno"></param>
        /// <param name="CO_RESP"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaAlunoXResponsavel(DropDownList ddlAluno, int CO_RESP, bool Relatorio)
        {
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where (CO_RESP != 0 ? tb07.TB108_RESPONSAVEL.CO_RESP == CO_RESP : 0 == 0)
                       select new
                       {
                           tb07.NO_ALU,
                           tb07.CO_ALU,
                       }).ToList();

            if (res != null)
            {
                ddlAluno.DataTextField = "NO_ALU";
                ddlAluno.DataValueField = "CO_ALU";
                ddlAluno.DataSource = res;
                ddlAluno.DataBind();
            }

            if (Relatorio == false)
                ddlAluno.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddlAluno.Items.Insert(0, new ListItem("Todos", "0"));
        }

        public static void CarregaAlunoComGradeXResponsavel(DropDownList ddlAluno, int CO_RESP, bool Relatorio)
        {
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       join tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros() on tb07.CO_ALU equals tb48.CO_ALU
                       where (CO_RESP != 0 ? tb07.TB108_RESPONSAVEL.CO_RESP == CO_RESP : 0 == 0)
                       select new
                       {
                           tb07.NO_ALU,
                           tb07.CO_ALU,
                       }).Distinct().ToList();

            if (res != null)
            {
                ddlAluno.DataTextField = "NO_ALU";
                ddlAluno.DataValueField = "CO_ALU";
                ddlAluno.DataSource = res;
                ddlAluno.DataBind();
            }

            if (Relatorio == false)
                ddlAluno.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddlAluno.Items.Insert(0, new ListItem("Todos", "0"));
        }


        /// <summary>
        /// Carrega todos os alunos transferidos em um controle DropDownList de acordo com os parâmetros recebidos
        /// </summary>
        /// <param name="ddlAluno"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="CO_MODU_CUR"></param>
        /// <param name="CO_CUR"></param>
        /// <param name="CO_ANO_MES_MAT"></param>
        /// <param name="CO_TUR"></param>
        /// <param name="tipoTransferencia">Recebe o tipo da transferência, sendo 0 como todas.</param>
        /// <param name="Relatorio"></param>
        public static void CarregaAlunosTransferidos(DropDownList ddlAluno, int CO_EMP, int CO_MODU_CUR, int CO_CUR, string CO_ANO_MES_MAT, int CO_TUR, string tipoTransferencia, bool Relatorio)
        {
            var res = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                       where tb08.TB44_MODULO.CO_MODU_CUR == CO_MODU_CUR
                       && tb08.CO_ANO_MES_MAT == CO_ANO_MES_MAT
                       && tb08.CO_CUR == CO_CUR
                       && tb08.CO_EMP == CO_EMP
                       && tb08.CO_TUR == CO_TUR
                       && (tipoTransferencia != "0" ? tb08.CO_SIT_MAT == tipoTransferencia : ((tb08.CO_SIT_MAT == "X") || (tb08.CO_SIT_MAT == "T")))
                       select new
                       {
                           tb08.TB07_ALUNO.NO_ALU,
                           tb08.TB07_ALUNO.CO_ALU,
                       }).OrderBy(w => w.NO_ALU).ToList();

            if (res != null)
            {
                ddlAluno.DataTextField = "NO_ALU";
                ddlAluno.DataValueField = "CO_ALU";
                ddlAluno.DataSource = res;
                ddlAluno.DataBind();
            }

            if (Relatorio)
                ddlAluno.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddlAluno.Items.Insert(0, new ListItem("Selecione", ""));

        }

        /// <summary>
        /// Carrega todos os alunos da unidade recebida como parâmetro e insere no controle dropdownlist
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaAlunosDaUnidade(DropDownList ddl, int CO_EMP, bool Relatorio)
        {
            if (LoginAuxili.TIPO_USU.Equals("R"))
            {
                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb07.CO_ALU equals tb08.TB07_ALUNO.CO_ALU
                           where tb08.CO_ALU == tb07.CO_ALU
                           && tb08.TB108_RESPONSAVEL.CO_RESP == LoginAuxili.CO_RESP
                           select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).ToList();

                ddl.DataTextField = "NO_ALU";
                ddl.DataValueField = "CO_ALU";
                ddl.DataSource = res;
                ddl.DataBind();
            }
            else
            {
                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where tb07.CO_EMP == CO_EMP
                           select new { tb07.CO_ALU, tb07.NO_ALU }).OrderBy(w => w.NO_ALU).ToList();

                ddl.DataTextField = "NO_ALU";
                ddl.DataValueField = "CO_ALU";
                ddl.DataSource = res;
                ddl.DataBind();

                if (Relatorio)
                    ddl.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddl.Items.Insert(0, new ListItem("Selecione", ""));
            }


        }

        #endregion

        #region Carregamento de Responsáveis

        /// <summary>
        /// Carrega todos os responsáveis em um objeto DropDownList de acordo com o Código da Instituição recebido como parâmetro.
        /// </summary>
        /// <param name="ddlResponsavel"></param>
        /// <param name="CO_ORG_RG_RESP"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaResponsaveis(DropDownList ddlResponsavel, int ORG_CODIGO_ORGAO, bool Relatorio, bool MostraPadrao = true)
        {
            var res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                       where tb108.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == ORG_CODIGO_ORGAO
                       select new
                       {
                           tb108.NO_RESP,
                           tb108.CO_RESP,
                       }).ToList();

            if (res != null)
            {
                ddlResponsavel.DataTextField = "NO_RESP";
                ddlResponsavel.DataValueField = "CO_RESP";
                ddlResponsavel.DataSource = res;
                ddlResponsavel.DataBind();
            }

            if (MostraPadrao)
            {
                if (Relatorio == false)
                    ddlResponsavel.Items.Insert(0, new ListItem("Selecione", ""));
                else
                    ddlResponsavel.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }

        #endregion

        #region Carregamento de Bancos

        /// <summary>
        /// Carrega todos os bancos cadastrados no sistema, já concatenando (Ex. 001 - Banco do Brasil)
        /// </summary>
        /// <param name="ddlBco"></param>
        /// <param name="Relatorio"></param>
        /// <param name="mostraPadrao"></param>
        public static void CarregaBancos(DropDownList ddlBco, bool Relatorio, bool mostraPadrao = true)
        {
            var res = (from tb29 in TB29_BANCO.RetornaTodosRegistros()
                       select new saidaBancos
                       {
                           ideBanco = tb29.IDEBANCO,
                           Nomebanco = tb29.DESBANCO,
                       }).ToList();

            if (res != null)
            {
                ddlBco.DataTextField = "concat";
                ddlBco.DataValueField = "IDEBANCO";
                ddlBco.DataSource = res;
                ddlBco.DataBind();
            }

            if (mostraPadrao)
            {
                if (Relatorio)
                    ddlBco.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddlBco.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        public class saidaBancos
        {
            public string ideBanco { get; set; }
            public string Nomebanco { get; set; }
            public string concat
            {
                get
                {
                    return this.ideBanco + " - " + this.Nomebanco;
                }
            }
        }

        #endregion

        #region Carregamento de Boletos

        /// <summary>
        /// Carrega todos os bancos cadastrados no sistema, já concatenando (Ex. 001 - Banco do Brasil)
        /// </summary>
        /// <param name="ddlBco"></param>
        /// <param name="Relatorio"></param>
        /// <param name="mostraPadrao"></param>
        public static void CarregaBoletos(DropDownList drpBoleto, int coEmp, string tpTaxa, int coMod, int coCur, bool Relatorio, bool mostraPadrao = true)
        {
            var result = (from tb227 in TB227_DADOS_BOLETO_BANCARIO.RetornaTodosRegistros()
                          join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb227.CO_MODU_CUR equals tb44.CO_MODU_CUR into mod
                          from tb44 in mod.DefaultIfEmpty()
                          join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb227.CO_CUR equals tb01.CO_CUR into cur
                          from tb01 in cur.DefaultIfEmpty()
                          where (!String.IsNullOrEmpty(tpTaxa) ? tb227.TP_TAXA_BOLETO == tpTaxa : true)
                          && (coMod != 0 ? (tb227.CO_MODU_CUR == coMod || tb227.CO_MODU_CUR == null) : true)
                          && (coCur != 0 ? (tb227.CO_CUR == coCur || tb227.CO_CUR == null) : true)
                          select new
                          {
                              tb227.ID_BOLETO,
                              tb227.TB224_CONTA_CORRENTE,
                              NO_MODU = tb44 != null ? (!String.IsNullOrEmpty(tb44.CO_SIGLA_MODU_CUR) ? tb44.CO_SIGLA_MODU_CUR : tb44.DE_MODU_CUR) : "",
                              NO_CUR = tb01 != null ? (!String.IsNullOrEmpty(tb01.CO_SIGL_CUR) ? tb01.CO_SIGL_CUR : tb01.NO_CUR) : ""
                          }).ToList();

            var result2 = (from res in result
                           join tb225 in TB225_CONTAS_UNIDADE.RetornaTodosRegistros() on res.TB224_CONTA_CORRENTE.CO_CONTA equals tb225.CO_CONTA
                           where tb225.CO_EMP == coEmp && res.TB224_CONTA_CORRENTE.CO_AGENCIA == tb225.CO_AGENCIA
                           && tb225.IDEBANCO == res.TB224_CONTA_CORRENTE.IDEBANCO
                           select new
                           {
                               res.ID_BOLETO,
                               DESCRICAO = string.Format("BCO {0} - AGE {1} - CTA {2}{3}", res.TB224_CONTA_CORRENTE.IDEBANCO,
                               res.TB224_CONTA_CORRENTE.CO_AGENCIA, res.TB224_CONTA_CORRENTE.CO_CONTA, (!String.IsNullOrEmpty(res.NO_MODU) ? (" - MOD " + res.NO_MODU + " - CUR " + res.NO_CUR) : ""))
                           }).OrderBy(b => b.DESCRICAO);

            drpBoleto.DataSource = result2;

            drpBoleto.DataValueField = "ID_BOLETO";
            drpBoleto.DataTextField = "DESCRICAO";
            drpBoleto.DataBind();

            if (mostraPadrao)
            {
                if (Relatorio)
                    drpBoleto.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    drpBoleto.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        #endregion

        #region Carregamento de Tipos de Ocorrências

        /// <summary>
        /// Carrega os tipos (CAtegorias) de ocorrências
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaTiposOcorrencias(DropDownList ddl, bool Relatorio, string CO_CATEG)
        {
            if (CO_CATEG != "0")
            {
                var res = (from tb150 in TB150_TIPO_OCORR.RetornaTodosRegistros()
                           //Se for aluno ou responsável, carrega os tipos para aluno, se for funcionário ou professor
                           //carrega os tipos para funcionário
                           where (CO_CATEG == "A" || CO_CATEG == "R" ? tb150.TP_USU.Equals("A") : tb150.TP_USU.Equals("F"))
                           select new
                           {
                               tb150.CO_SIGL_OCORR,
                               tb150.DE_TIPO_OCORR,
                           }).OrderBy(w => w.DE_TIPO_OCORR).ToList();

                ddl.DataTextField = "DE_TIPO_OCORR";
                ddl.DataValueField = "CO_SIGL_OCORR";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (Relatorio)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        #endregion

        #region Carregamentos Padrões

        /// <summary>
        /// Carrega os tipos de situações de matrículas
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaSituacaoMatricula(DropDownList ddl, bool Relatorio)
        {
            ddl.Items.Insert(0, new ListItem("Cancelado", "C"));
            ddl.Items.Insert(0, new ListItem("Evadido", "E"));
            ddl.Items.Insert(0, new ListItem("Finalizado", "F"));
            ddl.Items.Insert(0, new ListItem("Matriculado", "A"));
            ddl.Items.Insert(0, new ListItem("Pendente", "P"));
            ddl.Items.Insert(0, new ListItem("Trancado", "T"));
            ddl.Items.Insert(0, new ListItem("Transferido", "X"));

            if (Relatorio == false)
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Carrega as categorias padrões para ocorrências disciplinares
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaCategoriaOcorrencias(DropDownList ddl, bool Relatorio, bool mostraPadrao = true)
        {
            if (LoginAuxili.TIPO_USU.Equals("R"))
            {
                ddl.Items.Insert(0, new ListItem("Aluno", "A"));
            }
            else
            {
                ddl.Items.Insert(0, new ListItem("Responsável", "R"));
                ddl.Items.Insert(0, new ListItem("Professor", "P"));
                ddl.Items.Insert(0, new ListItem("Funcionário", "F"));
                ddl.Items.Insert(0, new ListItem("Aluno", "A"));

                if (mostraPadrao)
                {
                    if (Relatorio)
                        ddl.Items.Insert(0, new ListItem("Todos", "0"));
                    else
                        ddl.Items.Insert(0, new ListItem("Selecione", ""));
                }
            }

        }

        #endregion

        #endregion

        #region SaudeW

        #region Carregamento de Especialidades

        /// <summary>
        /// Carrega todas as especialidades à partir de um Grupo e a Empresa recebidos como parâmetro
        /// </summary>
        /// <param name="ddlEspecialidadeColab"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="ID_GRUPO_ESPECI"></param>
        /// <param name="relatorio"></param>
        public static void CarregaEspeciacialidades(DropDownList ddlEspecialidadeColab, int CO_EMP, int? ID_GRUPO_ESPECI, bool relatorio, bool MostraPadrao = true)
        {
            if ((ID_GRUPO_ESPECI != null) && (ID_GRUPO_ESPECI != 0))
            {
                var res = (from tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros()
                           where tb63.TB115_GRUPO_ESPECIALIDADE.ID_GRUPO_ESPECI == ID_GRUPO_ESPECI
                           select new { tb63.CO_ESPECIALIDADE, tb63.NO_ESPECIALIDADE }).OrderBy(e => e.NO_ESPECIALIDADE);

                if (res != null)
                {
                    ddlEspecialidadeColab.DataTextField = "NO_ESPECIALIDADE";
                    ddlEspecialidadeColab.DataValueField = "CO_ESPECIALIDADE";
                    ddlEspecialidadeColab.DataSource = res;
                    ddlEspecialidadeColab.DataBind();
                }
            }
            else
            {
                var res = (from tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros()
                           where tb63.CO_EMP == CO_EMP
                           select new { tb63.CO_ESPECIALIDADE, tb63.NO_ESPECIALIDADE }).OrderBy(e => e.NO_ESPECIALIDADE);

                if (res != null)
                {
                    ddlEspecialidadeColab.DataTextField = "NO_ESPECIALIDADE";
                    ddlEspecialidadeColab.DataValueField = "CO_ESPECIALIDADE";
                    ddlEspecialidadeColab.DataSource = res;
                    ddlEspecialidadeColab.DataBind();
                }
            }

            if (MostraPadrao)
            {
                if (relatorio == false)
                    ddlEspecialidadeColab.Items.Insert(0, new ListItem("Selecione", ""));
                else
                    ddlEspecialidadeColab.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }

        /// <summary>
        /// Carrega todos os Grupos de Especialidades no controle DropDownList especificado.
        /// </summary>
        /// <param name="ddlGrupoEspecialidadeColab"></param>
        /// <param name="relatorio"></param>
        public static void CarregaGrupoEspecialidade(DropDownList ddlGrupoEspecialidadeColab, bool relatorio)
        {
            var res = (from tb115 in TB115_GRUPO_ESPECIALIDADE.RetornaTodosRegistros()
                       select new
                       {
                           tb115.ID_GRUPO_ESPECI,
                           tb115.DE_GRUPO_ESPECI
                       });

            ddlGrupoEspecialidadeColab.DataTextField = "DE_GRUPO_ESPECI";
            ddlGrupoEspecialidadeColab.DataValueField = "ID_GRUPO_ESPECI";
            ddlGrupoEspecialidadeColab.DataSource = res;
            ddlGrupoEspecialidadeColab.DataBind();

            if (relatorio == false)
                ddlGrupoEspecialidadeColab.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddlGrupoEspecialidadeColab.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion

        #region Carregamento de Departamentos

        /// <summary>
        /// Carrega todos os departamentos em um controle DropDownList relacionados à uma determinada empresa recebida como pâmetro.
        /// </summary>
        /// <param name="ddlLocal"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaDepartamentos(DropDownList ddlLocal, int CO_EMP, bool Relatorio, bool MostraPadrao = true, bool InserPrimRegisVazio = false)
        {
            var res = (from tb14 in TB14_DEPTO.RetornaTodosRegistros()
                       where CO_EMP != 0 ? tb14.TB25_EMPRESA.CO_EMP == CO_EMP : 0 == 0
                       select new { tb14.NO_DEPTO, tb14.CO_DEPTO });

            if (res != null)
            {
                ddlLocal.DataTextField = "NO_DEPTO";
                ddlLocal.DataValueField = "CO_DEPTO";
                ddlLocal.DataSource = res;
                ddlLocal.DataBind();
            }

            if (MostraPadrao == true)
            {
                if (Relatorio == false)
                    ddlLocal.Items.Insert(0, new ListItem("Selecione", ""));
                else
                    ddlLocal.Items.Insert(0, new ListItem("Todos", "0"));
            }

            //Insere item vazio caso assim tenha sido escolhido
            if (InserPrimRegisVazio)
                ddlLocal.Items.Insert(0, new ListItem("", ""));
        }

        #endregion

        #region Carregamento de Funções

        /// <summary>
        /// Carrega as Funções em controle DropDownList de acordo com os parâmetros recebidos
        /// </summary>
        /// <param name="ddlFuncao"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaFuncoes(DropDownList ddlFuncao, bool Relatorio)
        {
            var res = (from tb15 in TB15_FUNCAO.RetornaTodosRegistros()
                       select new SaidaFuncao { CO_CBO_FUN = tb15.CO_CBO_FUN, CO_FUN = tb15.CO_FUN, NO_FUN = tb15.NO_FUN }).OrderBy(p => p.CO_CBO_FUN);

            if (res != null)
            {
                ddlFuncao.DataTextField = "concatFuncao";
                ddlFuncao.DataValueField = "CO_FUN";
                ddlFuncao.DataSource = res;
                ddlFuncao.DataBind();
            }

            if (Relatorio)
                ddlFuncao.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddlFuncao.Items.Insert(0, new ListItem("Selecione", ""));
        }

        public class SaidaFuncao
        {
            public int CO_FUN { get; set; }
            public string CO_CBO_FUN { get; set; }
            public string NO_FUN { get; set; }
            public string concatFuncao
            {
                get
                {
                    return this.CO_CBO_FUN + " - " + this.NO_FUN;
                }
            }
        }

        #endregion

        #region Carregamento de Plantões

        /// <summary>
        /// Carrega todos os tipos de plantões em um determinado controle DropDownList, de acordo com a empresa recebida como parâmetro
        /// </summary>
        /// <param name="ddlTipoPlant"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaTiposPlantoes(DropDownList ddlTipoPlant, int CO_EMP, bool Relatorio)
        {
            var res = (from tb153 in TB153_TIPO_PLANT.RetornaTodosRegistros()
                       where tb153.TB25_EMPRESA.CO_EMP == CO_EMP
                       && tb153.CO_SITUA_TIPO_PLANT == "A"
                       select new SaidaTipoPlant
                       {
                           idTipo = tb153.ID_TIPO_PLANT,
                           sigla = tb153.CO_SIGLA_TIPO_PLANT,
                           qtHoras = tb153.QT_HORAS,
                           hrIni = tb153.HR_INI_TIPO_PLANT
                       });

            if (res != null)
            {
                ddlTipoPlant.DataTextField = "saida";
                ddlTipoPlant.DataValueField = "idTipo";
                ddlTipoPlant.DataSource = res;
                ddlTipoPlant.DataBind();
            }

            if (Relatorio == false)
                ddlTipoPlant.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddlTipoPlant.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #region Classes de Saída

        public class SaidaTipoPlant
        {
            public int idTipo { get; set; }
            public string sigla { get; set; }
            public int qtHoras { get; set; }
            public string hrIni { get; set; }
            public string saida
            {
                get
                {
                    return "Tipo: " + this.sigla + " - CH: " + this.qtHoras.ToString().PadLeft(2, '0') + "h - Início: " + this.hrIni;
                }
            }
        }

        #endregion

        #endregion

        #region Carregamento de Pacientes

        /// <summary>
        /// Carrega todos os pacientes de uma determinada empresa recebida como parâmetro
        /// </summary>
        /// <param name="ddlPaciente"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaPacientes(DropDownList ddlPaciente, int CO_EMP, bool Relatorio, bool todasUnidades = false)
        {
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where tb07.CO_SITU_ALU != "H" && tb07.CO_SITU_ALU != "O"
                       && (CO_EMP == 0 && todasUnidades == true ? 0 == 0 : tb07.CO_EMP == CO_EMP)
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).ToList();

            if (res != null)
            {
                ddlPaciente.DataTextField = "NO_ALU";
                ddlPaciente.DataValueField = "CO_ALU";
                ddlPaciente.DataSource = res;
                ddlPaciente.DataBind();
            }

            if (Relatorio == false)
                ddlPaciente.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddlPaciente.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Carrega todos os pacientes de uma determinada empresa recebida como parâmetro
        /// </summary>
        /// <param name="ddlPaciente"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaPacientesAgendamento(DropDownList ddlPaciente, bool Relatorio, int EmpCadastro, int EmpContrato, int Operadora, int Profissional, bool mostraPadrao = true)
        {
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tb07.CO_ALU equals tbs174.CO_ALU
                       where (EmpCadastro != 0 ? tb07.CO_EMP == EmpCadastro : 0 == 0)
                        && (EmpContrato != 0 ? tb07.CO_EMP_ORIGEM == EmpContrato : 0 == 0)
                        && (Operadora != 0 ? tb07.TB250_OPERA.ID_OPER == Operadora : 0 == 0)
                        && (Profissional != 0 ? tbs174.CO_COL == Profissional : 0 == 0)
                       select new { tb07.NO_ALU, tb07.CO_ALU }).OrderBy(w => w.NO_ALU).DistinctBy(p => p.CO_ALU).ToList();

            if (res != null)
            {
                ddlPaciente.DataTextField = "NO_ALU";
                ddlPaciente.DataValueField = "CO_ALU";
                ddlPaciente.DataSource = res;
                ddlPaciente.DataBind();
            }

            if (mostraPadrao)
                if (!Relatorio)
                    ddlPaciente.Items.Insert(0, new ListItem("Selecione", ""));
                else
                    ddlPaciente.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion

        #region Carregamento de Profissionais de Saúde

        /// <summary>
        /// Carrega uma lista de médicos de acordo com a empresa recebida como parâmetro
        /// </summary>
        /// <param name="ddlProfiSaude"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaMedicos(DropDownList ddlProfiSaude, int CO_EMP, bool Relatorio, bool todasUnidades = false)
        {
            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       where (CO_EMP == 0 && todasUnidades == true ? 0 == 0 : tb03.CO_EMP == CO_EMP)
                       && tb03.FLA_PROFESSOR == "S"
                       select new { tb03.NO_COL, tb03.CO_COL }).OrderBy(w => w.NO_COL).ToList();

            if (res.Count > 0)
            {
                ddlProfiSaude.DataValueField = "CO_COL";
                ddlProfiSaude.DataTextField = "NO_COL";
                ddlProfiSaude.DataSource = res;
                ddlProfiSaude.DataBind();
            }

            if (Relatorio == false)
                ddlProfiSaude.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddlProfiSaude.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Carrega uma lista de Profissionais de acordo com os parâmetros
        /// </summary>
        /// <param name="ddlProfiSaude"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="Relatorio"></param>
        /// <param name="classFuncional"></param>
        public static void CarregaProfissionaisSaude(DropDownList ddlProfiSaude, int CO_EMP, bool Relatorio, string classFuncional, bool todasUnidades = false, int co_depto = 0, bool apenasProfiSaude = true)
        {
            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       where (CO_EMP == 0 && todasUnidades == true ? 0 == 0 : tb03.CO_EMP == CO_EMP)
                       && (apenasProfiSaude ? tb03.FLA_PROFESSOR == "S" : 0 == 0)
                       && tb03.CO_SITU_COL == "ATI"
                       && (co_depto != 0 ? tb03.CO_DEPTO == co_depto : 0 == 0)
                       && (classFuncional != "0" ? tb03.CO_CLASS_PROFI == classFuncional : 0 == 0)
                       select new { tb03.NO_COL, tb03.CO_COL }).OrderBy(w => w.NO_COL).ToList();

            if (res.Count > 0)
            {
                ddlProfiSaude.DataValueField = "CO_COL";
                ddlProfiSaude.DataTextField = "NO_COL";
                ddlProfiSaude.DataSource = res;
                ddlProfiSaude.DataBind();
            }

            if (Relatorio == false)
                ddlProfiSaude.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddlProfiSaude.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion

        #region Carregamento do CID

        /// <summary>
        /// Método responsável por carregar todos os CID Gerais e inserir a lista em um controle DropDownList
        /// </summary>
        /// <param name="ddlCIDGeral"></param>
        /// <param name="Relatorio"></param>
        /// <param name="mostraPadrao"></param>
        public static void CarregaCIDGeral(DropDownList ddlCIDGeral, bool Relatorio, bool mostraPadrao = true)
        {
            var res = (from tbs338 in TBS338_CID_GERAL.RetornaTodosRegistros()
                       where tbs338.CO_SITUA_CID_CAPIT == "A"
                       select new CID
                       {
                           nomeCID = tbs338.DE_CID_CAPIT,
                           idCID = tbs338.ID_CID_CAPIT,
                           codCID = tbs338.NO_CID_CAPIT,
                       }).ToList();

            if (res != null)
            {
                ddlCIDGeral.DataTextField = "concatCID";
                ddlCIDGeral.DataValueField = "idCID";
                ddlCIDGeral.DataSource = res;
                ddlCIDGeral.DataBind();
            }

            if (mostraPadrao)
            {
                if (Relatorio)
                    ddlCIDGeral.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddlCIDGeral.Items.Insert(0, new ListItem("Selecione", ""));
            }

        }

        /// <summary>
        /// Carrega as CID's existentes na base no combobox recebido como parâmetro. Há a possibilidade de apresentar o nome simples ou o nomo composto do Código e do Nome da Doença
        /// </summary>
        /// <param name="ddlCID"></param>
        /// <param name="Relatorio"></param>
        /// <param name="ConcatCodNom"></param>
        public static void CarregaCID(DropDownList ddlCID, bool Relatorio, bool ConcatCodNom)
        {
            var res = (from tb117 in TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros()
                       where tb117.CO_SITUA_CID == "A"
                       select new CID
                       {
                           nomeCID = tb117.NO_CID,
                           idCID = tb117.IDE_CID,
                           codCID = tb117.CO_CID
                       }).ToList();

            ddlCID.Items.Clear();
            ddlCID.SelectedIndex = -1;
            ddlCID.SelectedValue = null;
            ddlCID.ClearSelection();

            if (ConcatCodNom == true)
            {
                if (res != null)
                {
                    ddlCID.DataTextField = "concatCID";
                    ddlCID.DataValueField = "idCID";
                    ddlCID.DataSource = res;
                    ddlCID.DataBind();
                }
            }
            else
            {
                if (res != null)
                {
                    ddlCID.DataTextField = "nomeCID";
                    ddlCID.DataValueField = "idCID";
                    ddlCID.DataSource = res;
                    ddlCID.DataBind();
                }
            }

            if (Relatorio)
                ddlCID.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddlCID.Items.Insert(0, new ListItem("Selecione", ""));
        }

        public class CID
        {
            public string nomeCID { get; set; }
            public string codCID { get; set; }
            public int idCID { get; set; }
            public string concatCID
            {
                get
                {
                    return this.codCID + " - " + this.nomeCID;
                }
            }
        }

        #endregion

        #region Carregamento de Serviços Ambulatoriais

        /// <summary>
        /// Método responsável por carregar todos os Serviços Ambulatoriais de uma determinada empresa ou de todas
        /// </summary>
        /// <param name="ddlServ"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaServicosAmbulatoriais(DropDownList ddlServ, bool Relatorio)
        {
            var res = (from tbs331 in TBS331_SERVI_AMBULATORIAIS.RetornaTodosRegistros()
                       select new { tbs331.ID_SERVI_AMBUL, tbs331.DE_SERVI_AMBUL }).ToList();

            ddlServ.DataTextField = "DE_SERVI_AMBUL";
            ddlServ.DataValueField = "ID_SERVI_AMBUL";
            ddlServ.DataSource = res;
            ddlServ.DataBind();

            if (Relatorio)
                ddlServ.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddlServ.Items.Insert(0, new ListItem("Selecione", ""));
        }

        #endregion

        #region Carregamento de Exames Médicos

        /// <summary>
        /// Carrega todos os tipos de exames médicos em um determinado objeto DropDownList recebido como parâmetro
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaExamesMedicos(DropDownList ddl, bool Relatorio)
        {
            var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                       select new { tbs356.NM_PROC_MEDI, tbs356.ID_PROC_MEDI_PROCE }).ToList();

            if (res != null)
            {
                ddl.DataTextField = "NM_PROC_MEDI";
                ddl.DataValueField = "ID_PROC_MEDI_PROCE";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (Relatorio)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        #region Carregamento de Medicamentos

        /// <summary>
        /// Carrega todos os produtos(medicamentos) cadastrados na tb90 em um determinado objeto DropDownList de acordo com a empresa recebida como parâmetro
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaProdutos(DropDownList ddl, int CO_EMP, bool Relatorio)
        {
            var res = (from tb90 in TB90_PRODUTO.RetornaTodosRegistros()
                       where tb90.TB25_EMPRESA.CO_EMP == CO_EMP
                       select new { tb90.NO_PROD, tb90.CO_PROD }).OrderBy(w => w.NO_PROD).ToList();

            if (res != null)
            {
                ddl.DataTextField = "NO_PROD";
                ddl.DataValueField = "CO_PROD";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (Relatorio)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        #endregion

        #region Carregamentos Padrões

        /// <summary>
        /// Carrega as Classificações de risco padrões
        /// </summary>
        /// <param name="ddlClass"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaClassificacaoRisco(DropDownList ddlClass, bool Relatorio)
        {
            ddlClass.Items.Insert(0, new ListItem("Emergência", "1"));
            ddlClass.Items.Insert(0, new ListItem("Muito Urgente", "2"));
            ddlClass.Items.Insert(0, new ListItem("Urgente", "3"));
            ddlClass.Items.Insert(0, new ListItem("Pouco Urgente", "4"));
            ddlClass.Items.Insert(0, new ListItem("Não Urgente", "5"));
            ddlClass.Items.Insert(0, new ListItem("Não Definido", "0"));

            if (Relatorio)
                ddlClass.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddlClass.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega as situações possíveis para os tipos de plantões
        /// </summary>
        /// <param name="ddlTipo"></param>
        /// <param name="Relatorio"></param>
        /// <param name="mostraPadrao"></param>
        public static void CarregaSituacaoTipoPlantao(DropDownList ddlTipo, bool Relatorio, bool mostraPadrao = true)
        {
            ddlTipo.Items.Insert(0, new ListItem("Suspenso", "S"));
            ddlTipo.Items.Insert(0, new ListItem("Inativo", "I"));
            ddlTipo.Items.Insert(0, new ListItem("Ativo", "A"));

            if (Relatorio)
                ddlTipo.SelectedValue = "A";

            if (mostraPadrao)
            {
                if (Relatorio)
                    ddlTipo.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddlTipo.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }
        #endregion

        #region Carregamento de Tipos ISDA

        /// <summary>
        /// Carrega todos os Tipos ISDA Cadastrados e insere a lista no objeto recebido como parâmetro
        /// </summary>
        /// <param name="ddlTipoISDA"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaTiposISDA(DropDownList ddlTipoISDA, bool Relatorio, bool mostraPadrao = true)
        {
            var res = (from tbs335 in TBS335_ISDA_TIPO.RetornaTodosRegistros()
                       where tbs335.CO_SITUA_ISDA == "A"
                       select new { tbs335.NM_TIPO_ISDA, tbs335.ID_TIPO_ISDA }).OrderBy(w => w.NM_TIPO_ISDA).ToList();

            if (res != null)
            {
                ddlTipoISDA.DataTextField = "NM_TIPO_ISDA";
                ddlTipoISDA.DataValueField = "ID_TIPO_ISDA";
                ddlTipoISDA.DataSource = res;
                ddlTipoISDA.DataBind();
            }

            if (mostraPadrao)
            {
                if (Relatorio)
                    ddlTipoISDA.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddlTipoISDA.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        #endregion

        #region Carregamento Campanhas de Saúde

        /// <summary>
        /// Carrega os tipos de Campanhas de Saúde
        /// </summary>
        /// <param name="ddlTipoCam"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaTiposCampanhaSaude(DropDownList ddlTipoCam, bool Relatorio)
        {
            ddlTipoCam.Items.Insert(0, new ListItem("Programas", "P"));
            ddlTipoCam.Items.Insert(0, new ListItem("Campanha Vacinação", "V"));
            ddlTipoCam.Items.Insert(0, new ListItem("Ações", "A"));

            if (Relatorio)
                ddlTipoCam.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddlTipoCam.Items.Insert(0, new ListItem("Selecione", ""));

        }

        /// <summary>
        /// Carrega os tipos de competências de Campanhas de Saúde
        /// </summary>
        /// <param name="ddlComSaud"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaCompetenciasCampanhaSaude(DropDownList ddlComSaud, bool Relatorio)
        {
            ddlComSaud.Items.Insert(0, new ListItem("Outras", "O"));
            ddlComSaud.Items.Insert(0, new ListItem("Municipal", "M"));
            ddlComSaud.Items.Insert(0, new ListItem("Federal", "F"));
            ddlComSaud.Items.Insert(0, new ListItem("Estadual", "E"));
            ddlComSaud.Items.Insert(0, new ListItem("Conjunta", "X"));

            if (Relatorio)
                ddlComSaud.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddlComSaud.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega todas as Classificações de Campanha de Saúde
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaClassificacoesCampanhaSaude(DropDownList ddl, bool Relatorio)
        {
            ddl.Items.Insert(0, new ListItem("Temática", "TEM"));
            ddl.Items.Insert(0, new ListItem("Programada", "PRO"));
            ddl.Items.Insert(0, new ListItem("Outras", "OUT"));
            ddl.Items.Insert(0, new ListItem("Epidemia", "EPI"));
            ddl.Items.Insert(0, new ListItem("Educativa", "EDU"));

            if (Relatorio)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega as Situações possíveis para as Campanhas de Saúde
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaSituacaoCampanhaSaude(DropDownList ddl, bool Relatorio, bool SelecionaAtivo = true)
        {
            ddl.Items.Insert(0, new ListItem("Suspenso", "S"));
            ddl.Items.Insert(0, new ListItem("Finalizado", "F"));
            ddl.Items.Insert(0, new ListItem("Inativo", "I"));
            ddl.Items.Insert(0, new ListItem("Cancelado", "C"));
            ddl.Items.Insert(0, new ListItem("Ativo", "A"));

            if (Relatorio)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));

            if (SelecionaAtivo)
                ddl.SelectedValue = "A";

        }

        /// <summary>
        /// Carrega as Situações possíveis para os caolaboradores de campanhas de saúde
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        /// <param name="SelecionaAtivo"></param>
        public static void CarregaSituacaoColaboradorCampanhaSaude(DropDownList ddl, bool Relatorio, bool SelecionaAtivo = true)
        {
            ddl.Items.Insert(0, new ListItem("Suspenso", "S"));
            ddl.Items.Insert(0, new ListItem("Inativo", "I"));
            ddl.Items.Insert(0, new ListItem("Cancelado", "C"));
            ddl.Items.Insert(0, new ListItem("Ativo", "A"));

            if (Relatorio)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));

            if (SelecionaAtivo)
                ddl.SelectedValue = "A";
        }

        #endregion

        #region Carregamento Procedimentos Médicos

        /// <summary>
        /// Carrega as áreas respeitando as classificações permitidas e pré-definidas no cadastro da unidade recebida como parâmetro
        /// </summary>
        public static void CarregaAreasGrupos(DropDownList ddl, bool Relatorio, ETiposClassificacoes tpClass = ETiposClassificacoes.padrao)
        {
            ddl.Items.Insert(0, new ListItem("Terapia Ocupacional", "T"));
            ddl.Items.Insert(0, new ListItem("Outros", "O"));
            ddl.Items.Insert(0, new ListItem("Nutrição", "N"));
            ddl.Items.Insert(0, new ListItem("Esteticista", "S"));
            ddl.Items.Insert(0, new ListItem("Odontologia", "D"));
            ddl.Items.Insert(0, new ListItem("Psicologia", "P"));
            ddl.Items.Insert(0, new ListItem("Médica", "M"));
            ddl.Items.Insert(0, new ListItem("Fonoaudiologia", "F"));
            ddl.Items.Insert(0, new ListItem("Fisioterapia", "I"));
            ddl.Items.Insert(0, new ListItem("Enfermagem", "E"));

            if (tpClass == ETiposClassificacoes.atendimento)
            {
                var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                           join tb83 in TB83_PARAMETRO.RetornaTodosRegistros() on tb25.TB83_PARAMETRO.CO_EMP equals tb83.CO_EMP
                           where tb25.CO_EMP == LoginAuxili.CO_EMP
                           select new
                           {
                               tb83.FL_PERM_ATEND_ENFER,
                               tb83.FL_PERM_ATEND_FISIO,
                               tb83.FL_PERM_ATEND_FONOA,
                               tb83.FL_PERM_ATEND_MEDIC,
                               tb83.FL_PERM_ATEND_ODONT,
                               tb83.FL_PERM_ATEND_OUTRO,
                               tb83.FL_PERM_ATEND_PSICO,
                               tb83.FL_PERM_ATEND_ESTET,
                               tb83.FL_PERM_ATEND_NUTRI,
                               tb83.FL_PERM_ATEND_TERAP_OCUPA
                           }).FirstOrDefault();

                if (res.FL_PERM_ATEND_MEDIC != "S")
                    ddl.Items.Remove(ddl.Items.FindByValue("M"));

                if (res.FL_PERM_ATEND_ENFER != "S")
                    ddl.Items.Remove(ddl.Items.FindByValue("E"));

                if (res.FL_PERM_ATEND_FISIO != "S")
                    ddl.Items.Remove(ddl.Items.FindByValue("I"));

                if (res.FL_PERM_ATEND_FONOA != "S")
                    ddl.Items.Remove(ddl.Items.FindByValue("F"));

                if (res.FL_PERM_ATEND_ODONT != "S")
                    ddl.Items.Remove(ddl.Items.FindByValue("D"));

                if (res.FL_PERM_ATEND_ESTET != "S")
                    ddl.Items.Remove(ddl.Items.FindByValue("S"));

                if (res.FL_PERM_ATEND_NUTRI != "S")
                    ddl.Items.Remove(ddl.Items.FindByValue("N"));

                if (res.FL_PERM_ATEND_OUTRO != "S")
                    ddl.Items.Remove(ddl.Items.FindByValue("O"));

                if (res.FL_PERM_ATEND_PSICO != "S")
                    ddl.Items.Remove(ddl.Items.FindByValue("P"));

                if (res.FL_PERM_ATEND_TERAP_OCUPA != "S")
                    ddl.Items.Remove(ddl.Items.FindByValue("T"));
            }

            if (Relatorio)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega todos os Grupos de Procedimentos Médicos Ativos
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaGruposProcedimentosMedicos(DropDownList ddl, bool Relatorio, bool mostraPadrao = true, bool insereVazio = false, ETiposClassificacoes tpClass = ETiposClassificacoes.padrao)
        {
            var clss = AuxiliCarregamentos.RecuperarClassificacoesDisponiveis(tpClass);

            var res = (from tbs354 in TBS354_PROC_MEDIC_GRUPO.RetornaTodosRegistros()
                       where tbs354.FL_SITUA_PROC_MEDIC_GRUPO == "A" && clss.Contains(tbs354.NM_AREA_GRUPO)
                       select new { tbs354.ID_PROC_MEDIC_GRUPO, tbs354.NM_PROC_MEDIC_GRUPO }).OrderBy(w => w.NM_PROC_MEDIC_GRUPO).ToList();

            if (res != null)
            {
                ddl.DataTextField = "NM_PROC_MEDIC_GRUPO";
                ddl.DataValueField = "ID_PROC_MEDIC_GRUPO";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (mostraPadrao)
            {
                if (Relatorio)
                    ddl.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddl.Items.Insert(0, new ListItem("Selecione", ""));
            }

            if (insereVazio)
                ddl.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Carrega todos os SubGrupos de Procedimentos Médicos Ativos
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaSubGruposProcedimentosMedicos(DropDownList ddl, int ID_PROC_MEDIC_GRUPO, bool Relatorio)
        {
            var res = (from tbs355 in TBS355_PROC_MEDIC_SGRUP.RetornaTodosRegistros()
                       where tbs355.FL_SITUA_PROC_MEDIC_GRUP == "A"
                       && tbs355.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == ID_PROC_MEDIC_GRUPO
                       select new { tbs355.ID_PROC_MEDIC_SGRUP, tbs355.NM_PROC_MEDIC_SGRUP }).OrderBy(w => w.NM_PROC_MEDIC_SGRUP).ToList();

            if (res != null)
            {
                ddl.DataTextField = "NM_PROC_MEDIC_SGRUP";
                ddl.DataValueField = "ID_PROC_MEDIC_SGRUP";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (Relatorio)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Sobrecarga do método de carregamento, para que no carregamento de subgrupos, seja possível receber o dropdownlist de grupo ao invés de apenas o ID
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="ddlGrupoProcedimento"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaSubGruposProcedimentosMedicos(DropDownList ddl, DropDownList ddlGrupoProcedimento, bool Relatorio, bool mostraPadrao = true, bool insereVazio = false)
        {
            int idGrupo = (!string.IsNullOrEmpty(ddlGrupoProcedimento.SelectedValue) ? int.Parse(ddlGrupoProcedimento.SelectedValue) : 0);
            var res = (from tbs355 in TBS355_PROC_MEDIC_SGRUP.RetornaTodosRegistros()
                       where tbs355.FL_SITUA_PROC_MEDIC_GRUP == "A"
                       && tbs355.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == idGrupo
                       select new { tbs355.ID_PROC_MEDIC_SGRUP, tbs355.NM_PROC_MEDIC_SGRUP }).OrderBy(w => w.NM_PROC_MEDIC_SGRUP).ToList();

            if (res != null)
            {
                ddl.DataTextField = "NM_PROC_MEDIC_SGRUP";
                ddl.DataValueField = "ID_PROC_MEDIC_SGRUP";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (mostraPadrao)
            {
                if (Relatorio)
                    ddl.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddl.Items.Insert(0, new ListItem("Selecione", ""));
            }

            if (insereVazio)
                ddl.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Carrega os tipos de procedimentos possíveis em um DropDownList
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaTiposProcedimentosMedicos(DropDownList ddl, bool Relatorio)
        {
            ddl.Items.Insert(0, new ListItem("Outros", "OU"));
            ddl.Items.Insert(0, new ListItem("Vacina", "VA"));
            ddl.Items.Insert(0, new ListItem("Internação", "IN"));
            ddl.Items.Insert(0, new ListItem("Serviço Saúde", "SS"));
            ddl.Items.Insert(0, new ListItem("Serviço Ambulatorial", "SA"));
            ddl.Items.Insert(0, new ListItem("Cirurgia", "CI"));
            ddl.Items.Insert(0, new ListItem("OPM - Órtese, Prótese e Materia Especiais", "OP"));
            ddl.Items.Insert(0, new ListItem("Procedimento", "PR"));
            ddl.Items.Insert(0, new ListItem("Exame", "EX"));
            ddl.Items.Insert(0, new ListItem("Consulta", "CO"));

            if (Relatorio)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega os procedimentos padrões da instituição
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        /// <param name="mostraPadrao"></param>
        public static void CarregaProcedimentosPadroesInstituicao(DropDownList ddl, bool Relatorio, bool mostraPadrao = true, string tipoProcedimento = null)
        {
            var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                       where tbs356.TB250_OPERA == null
                       && (tipoProcedimento != null ? tbs356.CO_TIPO_PROC_MEDI == tipoProcedimento : 0 == 0)
                       select new { tbs356.ID_PROC_MEDI_PROCE, tbs356.NM_PROC_MEDI }).OrderBy(w => w.NM_PROC_MEDI).ToList();

            if (res != null)
            {
                ddl.DataTextField = "NM_PROC_MEDI";
                ddl.DataValueField = "ID_PROC_MEDI_PROCE";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (mostraPadrao)
            {
                if (Relatorio)
                    ddl.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddl.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        /// <summary>
        /// Carrega os procedimentos padrões da instituição
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        /// <param name="mostraPadrao"></param>
        public static void CarregaProcedimentosMedicos(DropDownList ddl, DropDownList ddlGrupo, DropDownList ddlSubGrupo, int idOper, bool Relatorio, bool mostraPadrao = true, string tipoProcedimento = null, bool comCodigo = false, bool semOperad = true)
        {
            int idGrupo = (!string.IsNullOrEmpty(ddlGrupo.SelectedValue) ? int.Parse(ddlGrupo.SelectedValue) : 0);
            int idSubGrupo = (!string.IsNullOrEmpty(ddlSubGrupo.SelectedValue) ? int.Parse(ddlSubGrupo.SelectedValue) : 0);

            var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                       join tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs356.ID_PROC_MEDI_PROCE equals tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE
                       //Se o id da operadora recebido for inválido(0), então filtrará os procedimentos apenas da instituição
                       where (idOper != 0 ? tbs356.TB250_OPERA.ID_OPER == idOper : (semOperad ? tbs356.TB250_OPERA == null : 0 == 0))
                       && (tipoProcedimento != null ? tbs356.CO_TIPO_PROC_MEDI == tipoProcedimento : 0 == 0)
                       && (idGrupo != 0 ? tbs356.TBS354_PROC_MEDIC_GRUPO.ID_PROC_MEDIC_GRUPO == idGrupo : 0 == 0)
                       && (idSubGrupo != 0 ? tbs356.TBS355_PROC_MEDIC_SGRUP.ID_PROC_MEDIC_SGRUP == idSubGrupo : 0 == 0)
                       select new saidaProcedimentos
                       {
                           ID_PROC_MEDI_PROCE = tbs356.ID_PROC_MEDI_PROCE,
                           NM_PROC_MEDI = tbs356.NM_PROC_MEDI,
                           CO_PROC_MEDI = tbs356.CO_PROC_MEDI,
                       }).OrderBy(w => w.NM_PROC_MEDI).ToList();

            if (res != null)
            {
                ddl.DataTextField = (comCodigo ? "PROC_MEDI_CONCAT" : "NM_PROC_MEDI");
                ddl.DataValueField = "ID_PROC_MEDI_PROCE";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (mostraPadrao)
            {
                if (Relatorio)
                    ddl.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddl.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        public class saidaProcedimentos
        {
            public string NM_PROC_MEDI { get; set; }
            public int ID_PROC_MEDI_PROCE { get; set; }
            public string CO_PROC_MEDI { get; set; }
            public string PROC_MEDI_CONCAT
            {
                get
                {
                    return (this.CO_PROC_MEDI + " - " + this.NM_PROC_MEDI);
                }
            }
        }

        /// <summary>
        /// Carrega os procedimentos padrões da instituição
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        /// <param name="mostraPadrao"></param>
        public static void CarregaProcedimentosMedicos(DropDownList ddl, DropDownList ddlOper, bool Relatorio, bool mostraPadrao = true, string tipoProcedimento = null)
        {
            int idOper = (!string.IsNullOrEmpty(ddlOper.SelectedValue) ? int.Parse(ddlOper.SelectedValue) : 0);
            var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                       //Se o id da operadora recebido for inválido(0), então filtrará os procedimentos apenas da instituição
                       where (idOper != 0 ? tbs356.TB250_OPERA.ID_OPER == idOper : tbs356.TB250_OPERA == null)
                       && (tipoProcedimento != null ? tbs356.CO_TIPO_PROC_MEDI == tipoProcedimento : 0 == 0)
                       select new { tbs356.ID_PROC_MEDI_PROCE, tbs356.NM_PROC_MEDI }).OrderBy(w => w.NM_PROC_MEDI).ToList();

            if (res != null)
            {
                ddl.DataTextField = "NM_PROC_MEDI";
                ddl.DataValueField = "ID_PROC_MEDI_PROCE";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (mostraPadrao)
            {
                if (Relatorio)
                    ddl.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddl.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        /// <summary>
        /// Carrega os procedimentos padrões da instituição
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        /// <param name="mostraPadrao"></param>
        public static void CarregaProcedimentosMedicos(DropDownList ddl, int idOper, bool Relatorio, bool mostraPadrao = true, string tipoProcedimento = null, bool comCodigo = false)
        {
            var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                       //Se o id da operadora recebido for inválido(0), então filtrará os procedimentos apenas da instituição
                       //where (idOper != 0 ? tbs356.TB250_OPERA.ID_OPER == idOper : tbs356.TB250_OPERA == null)
                       where (idOper != 0 ? tbs356.TB250_OPERA.ID_OPER == idOper : 0 == 0)
                       && (tipoProcedimento != null ? tbs356.CO_TIPO_PROC_MEDI == tipoProcedimento : 0 == 0)
                       select new saidaProcedimentos
                       {
                           ID_PROC_MEDI_PROCE = tbs356.ID_PROC_MEDI_PROCE,
                           NM_PROC_MEDI = tbs356.NM_PROC_MEDI,
                           CO_PROC_MEDI = tbs356.CO_PROC_MEDI,
                       }).OrderBy(w => w.NM_PROC_MEDI).ToList();

            if (res != null)
            {
                ddl.DataTextField = (comCodigo ? "PROC_MEDI_CONCAT" : "NM_PROC_MEDI");
                ddl.DataValueField = "ID_PROC_MEDI_PROCE";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (mostraPadrao)
            {
                if (Relatorio)
                    ddl.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddl.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        #endregion

        #region Carregamento de Funcionários

        /// <summary>
        /// Carrega os Funcionários lotados na empresa recebida como parâmetro
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaFuncionarios(DropDownList ddl, int CO_EMP, bool Relatorio, bool mostraPadrao = true)
        {
            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       where tb03.CO_EMP == CO_EMP
                       && tb03.CO_SITU_COL == "ATI"
                       select new { tb03.NO_COL, tb03.CO_COL }).OrderBy(w => w.NO_COL).ToList();


            if (res != null)
            {
                ddl.DataTextField = "NO_COL";
                ddl.DataValueField = "CO_COL";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (mostraPadrao)
            {
                if (Relatorio)
                    ddl.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddl.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        /// <summary>
        /// Carrega os Tipos de Contratos de vínculo dos Colaboradores com as Instituições
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaTiposContrato(DropDownList ddl, bool Relatorio)
        {
            var res = TB20_TIPOCON.RetornaTodosRegistros();

            if (res != null)
            {
                ddl.DataTextField = "NO_TPCON";
                ddl.DataValueField = "CO_TPCON";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (Relatorio)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        #endregion

        #region Carregamento de Classificações Funcionais

        public enum ETiposClassificacoes
        {
            padrao,
            agendamento,
            direcionamento,
            atendimento,
            acolhimento,
        }

        /// <summary>
        /// Carrega as classificações funcionais respeitando as classificações permitidas e pré-definidas no cadastro da unidade recebida como parâmetro
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaClassificacoesFuncionais(DropDownList ddl, bool Relatorio, int? CO_EMP = null, ETiposClassificacoes tpClass = ETiposClassificacoes.padrao)
        {
            ddl.Items.Insert(0, new ListItem("Terapeuta Ocupacional", "T"));
            ddl.Items.Insert(0, new ListItem("Triagem", "O"));
            ddl.Items.Insert(0, new ListItem("Nutricionista", "N"));
            ddl.Items.Insert(0, new ListItem("Esteticista", "S"));
            ddl.Items.Insert(0, new ListItem("Odontólogo(a)", "D"));
            ddl.Items.Insert(0, new ListItem("Psicólogo", "P"));
            ddl.Items.Insert(0, new ListItem("Médico(a)", "M"));
            ddl.Items.Insert(0, new ListItem("Fonoaudiólogo(a)", "F"));
            ddl.Items.Insert(0, new ListItem("Fisioterapeuta", "I"));
            ddl.Items.Insert(0, new ListItem("Enfermeiro(a)", "E"));

            if (tpClass == ETiposClassificacoes.agendamento)
            {
                if (CO_EMP.HasValue)
                {
                    var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                               join tb83 in TB83_PARAMETRO.RetornaTodosRegistros() on tb25.TB83_PARAMETRO.CO_EMP equals tb83.CO_EMP
                               where tb25.CO_EMP == CO_EMP
                               select new
                               {
                                   tb83.FL_PERM_AGEND_ENFER,
                                   tb83.FL_PERM_AGEND_FISIO,
                                   tb83.FL_PERM_AGEND_FONOA,
                                   tb83.FL_PERM_AGEND_MEDIC,
                                   tb83.FL_PERM_AGEND_ODONT,
                                   tb83.FL_PERM_AGEND_OUTRO,
                                   tb83.FL_PERM_AGEND_PSICO,
                                   tb83.FL_PERM_AGEND_ESTET,
                                   tb83.FL_PERM_AGEND_NUTRI,
                                   tb83.FL_PERM_AGEND_TERAP_OCUPA
                               }).FirstOrDefault();

                    if (res != null)
                    {
                        if (res.FL_PERM_AGEND_MEDIC != "S")
                            ddl.Items.Remove(ddl.Items.FindByValue("M"));

                        if (res.FL_PERM_AGEND_ENFER != "S")
                            ddl.Items.Remove(ddl.Items.FindByValue("E"));

                        if (res.FL_PERM_AGEND_FISIO != "S")
                            ddl.Items.Remove(ddl.Items.FindByValue("I"));

                        if (res.FL_PERM_AGEND_FONOA != "S")
                            ddl.Items.Remove(ddl.Items.FindByValue("F"));

                        if (res.FL_PERM_AGEND_ODONT != "S")
                            ddl.Items.Remove(ddl.Items.FindByValue("D"));

                        if (res.FL_PERM_AGEND_ESTET != "S")
                            ddl.Items.Remove(ddl.Items.FindByValue("S"));

                        if (res.FL_PERM_AGEND_NUTRI != "S")
                            ddl.Items.Remove(ddl.Items.FindByValue("N"));

                        if (res.FL_PERM_AGEND_OUTRO != "S")
                            ddl.Items.Remove(ddl.Items.FindByValue("O"));

                        if (res.FL_PERM_AGEND_PSICO != "S")
                            ddl.Items.Remove(ddl.Items.FindByValue("P"));

                        if (res.FL_PERM_AGEND_TERAP_OCUPA != "S")
                            ddl.Items.Remove(ddl.Items.FindByValue("T"));
                    }                   
                }
            }

            if (Relatorio)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Recupera as Classificações funcionais da empresa do usúario logado
        /// </summary>
        /// <returns>List<String></returns>
        public static List<string> RecuperarClassificacoesDisponiveis(ETiposClassificacoes tpClass)
        {
            List<string> clss = new List<string>();

            clss.Add("T");//Terapeuta Ocupacional
            clss.Add("O");//Outros
            clss.Add("N");//Nutricionista
            clss.Add("S");//Esteticista
            clss.Add("D");//Odontólogo(a)
            clss.Add("P");//Psicólogo
            clss.Add("M");//Médico(a)
            clss.Add("F");//Fonoaudiólogo(a)
            clss.Add("I");//Fisioterapeuta
            clss.Add("E");//Enfermeiro(a)

            var prm = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                       join tb83 in TB83_PARAMETRO.RetornaTodosRegistros() on tb25.TB83_PARAMETRO.CO_EMP equals tb83.CO_EMP
                       where tb25.CO_EMP == LoginAuxili.CO_EMP
                       select new
                       {
                           tb83.FL_PERM_AGEND_ENFER,
                           tb83.FL_PERM_AGEND_FISIO,
                           tb83.FL_PERM_AGEND_FONOA,
                           tb83.FL_PERM_AGEND_MEDIC,
                           tb83.FL_PERM_AGEND_ODONT,
                           tb83.FL_PERM_AGEND_OUTRO,
                           tb83.FL_PERM_AGEND_PSICO,
                           tb83.FL_PERM_AGEND_ESTET,
                           tb83.FL_PERM_AGEND_NUTRI,
                           tb83.FL_PERM_AGEND_TERAP_OCUPA,
                           tb83.FL_PERM_ATEND_ENFER,
                           tb83.FL_PERM_ATEND_FISIO,
                           tb83.FL_PERM_ATEND_FONOA,
                           tb83.FL_PERM_ATEND_MEDIC,
                           tb83.FL_PERM_ATEND_ODONT,
                           tb83.FL_PERM_ATEND_OUTRO,
                           tb83.FL_PERM_ATEND_PSICO,
                           tb83.FL_PERM_ATEND_ESTET,
                           tb83.FL_PERM_ATEND_NUTRI,
                           tb83.FL_PERM_ATEND_TERAP_OCUPA
                       }).FirstOrDefault();

            if (tpClass == ETiposClassificacoes.agendamento)
            {
                if (prm.FL_PERM_AGEND_MEDIC != "S")
                    clss.Remove("M");

                if (prm.FL_PERM_AGEND_ENFER != "S")
                    clss.Remove("E");

                if (prm.FL_PERM_AGEND_FISIO != "S")
                    clss.Remove("I");

                if (prm.FL_PERM_AGEND_FONOA != "S")
                    clss.Remove("F");

                if (prm.FL_PERM_AGEND_ODONT != "S")
                    clss.Remove("D");

                if (prm.FL_PERM_AGEND_ESTET != "S")
                    clss.Remove("S");

                if (prm.FL_PERM_AGEND_NUTRI != "S")
                    clss.Remove("N");

                if (prm.FL_PERM_AGEND_OUTRO != "S")
                    clss.Remove("O");

                if (prm.FL_PERM_AGEND_PSICO != "S")
                    clss.Remove("P");

                if (prm.FL_PERM_AGEND_TERAP_OCUPA != "S")
                    clss.Remove("T");
            }
            else if (tpClass == ETiposClassificacoes.atendimento)
            {
                if (prm.FL_PERM_ATEND_MEDIC != "S")
                    clss.Remove("M");

                if (prm.FL_PERM_ATEND_ENFER != "S")
                    clss.Remove("E");

                if (prm.FL_PERM_ATEND_FISIO != "S")
                    clss.Remove("I");

                if (prm.FL_PERM_ATEND_FONOA != "S")
                    clss.Remove("F");

                if (prm.FL_PERM_ATEND_ODONT != "S")
                    clss.Remove("D");

                if (prm.FL_PERM_ATEND_ESTET != "S")
                    clss.Remove("S");

                if (prm.FL_PERM_ATEND_NUTRI != "S")
                    clss.Remove("N");

                if (prm.FL_PERM_ATEND_OUTRO != "S")
                    clss.Remove("O");

                if (prm.FL_PERM_ATEND_PSICO != "S")
                    clss.Remove("P");

                if (prm.FL_PERM_ATEND_TERAP_OCUPA != "S")
                    clss.Remove("T");
            }

            return clss;
        }

        /// <summary>
        /// Retorna o valor da classificação funcional convertida pelo tipo do agendamento
        /// </summary>
        /// <param name="tipoAgenda"></param>
        public static string RecuperarValorClassificacaoPorTipoAgend(String tipoAgenda)
        {
            switch (tipoAgenda)
            {
                case "TO":
                    return "T";
                case "OU":
                    return "O";
                case "NT":
                    return "N";
                case "ES":
                    return "S";
                case "PI":
                    return "P";
                case "FO":
                    return "F";
                case "FI":
                    return "I";
                case "EN":
                    return "E";
                case "AO":
                    return "D";
                case "AM":
                    return "M";
                default:
                    return "";
            }
        }

        #endregion

        #region Carregamento de Medicamentos

        /// <summary>
        /// Carrega os Grupos de Itens de Medicamentos
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaGruposItens(DropDownList ddl, bool Relatorio)
        {
            var res = (from tb260 in TB260_GRUPO.RetornaTodosRegistros()
                       where tb260.TP_GRUPO == "E"
                       select new { tb260.ID_GRUPO, tb260.NOM_GRUPO });

            ddl.DataValueField = "ID_GRUPO";
            ddl.DataTextField = "NOM_GRUPO";
            ddl.DataSource = res;
            ddl.DataBind();

            if (Relatorio)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carega os SubGrupos de Itens de acordo com o Grupo recebido como parâmetro e insere a lista em controle DropDownList
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="idGrupo"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaSubGruposItens(DropDownList ddl, int idGrupo, bool Relatorio)
        {
            var res = (from tb261 in TB261_SUBGRUPO.RetornaTodosRegistros()
                       where idGrupo != 0 ? tb261.TB260_GRUPO.ID_GRUPO == idGrupo : idGrupo == 0
                       select new { tb261.ID_SUBGRUPO, tb261.NOM_SUBGRUPO });

            ddl.DataValueField = "ID_SUBGRUPO";
            ddl.DataTextField = "NOM_SUBGRUPO";
            ddl.DataSource = res;
            ddl.DataBind();

            if (Relatorio)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega as Marcas de Itens de Produtos
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaMarcaItens(DropDownList ddl, bool Relatorio)
        {
            var res = (from tb93 in TB93_MARCA.RetornaTodosRegistros()
                       select new { tb93.DES_MARCA, tb93.CO_MARCA });

            ddl.DataTextField = "DES_MARCA";
            ddl.DataValueField = "CO_MARCA";
            ddl.DataSource = res;
            ddl.DataBind();

            if (Relatorio)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carrega as Unidades de Medidas
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaUnidadesMedidas(DropDownList ddl, bool Relatorio, bool MostraPadrao = true, string categUnid = "")
        {
            var res = (from tb89 in TB89_UNIDADES.RetornaTodosRegistros()
                       where categUnid != "" ? tb89.FL_CATEG_UNID == categUnid : categUnid == ""
                       select new { tb89.NO_UNID_ITEM, tb89.CO_UNID_ITEM });

            if (res != null)
            {
                ddl.DataTextField = "NO_UNID_ITEM";
                ddl.DataValueField = "CO_UNID_ITEM";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (MostraPadrao)
            {
                if (Relatorio)
                    ddl.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddl.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        /// <summary>
        /// Carrega as cores cadastradas no banco e preenche controle DropDownList
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaCores(DropDownList ddl, bool Relatorio)
        {
            var res = (from tb97 in TB97_COR.RetornaTodosRegistros()
                       select new { tb97.DES_COR, tb97.CO_COR });

            if (res != null)
            {
                ddl.DataTextField = "DES_COR";
                ddl.DataValueField = "CO_COR";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (Relatorio)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        #endregion

        #region Carregamento de Vacinas

        /// <summary>
        /// Carrega as Vacinas cadastradas
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        /// <param name="MostraPadrao"></param>
        public static void CarregaVacinas(DropDownList ddl, bool Relatorio, bool MostraPadrao = true)
        {
            var res = (from tbs345 in TBS345_VACINA.RetornaTodosRegistros()
                       where tbs345.CO_SITUA_VACINA == "A"
                       select new { tbs345.ID_VACINA, tbs345.NM_VACINA }).OrderBy(w => w.NM_VACINA).ToList();

            if (res != null)
            {
                ddl.DataTextField = "NM_VACINA";
                ddl.DataValueField = "ID_VACINA";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (MostraPadrao)
            {
                if (Relatorio)
                    ddl.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddl.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        #endregion

        #region Carregamento Planos de Saúde

        /// <summary>
        /// Carrega todas as Operadoras de planos de saúde em um determinado DropDownList informado
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaOperadorasPlanSaude(DropDownList ddl, bool Relatorio, bool mostraPadrao = true, bool insereVazio = false, bool siglaNome = false, bool siglaConcat = true)
        {
            var res = (from tb250 in TB250_OPERA.RetornaTodosRegistros()
                       where tb250.FL_SITU_OPER == "A"
                       select new saidaOperadora { ID_OPER = tb250.ID_OPER, NOM_OPER = tb250.NOM_OPER, NM_SIGLA_OPER = tb250.NM_SIGLA_OPER })
                       .OrderBy(w => w.NOM_OPER).ToList();

            if (res != null)
            {
                ddl.DataTextField = (siglaNome ? (siglaConcat ? "NM_OPER_CONCAT" : "NM_SIGLA_OPER") : "NOM_OPER"); //Mostra o nome ou sigla de acordo com necessidade
                ddl.DataValueField = "ID_OPER";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (mostraPadrao)
            {
                if (Relatorio)
                    ddl.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddl.Items.Insert(0, new ListItem("Selecione", ""));
            }

            if (insereVazio)
                ddl.Items.Insert(0, new ListItem("", ""));
        }
        
        public class saidaOperadora
        {
            public string NOM_OPER { get; set; }
            public string NM_SIGLA_OPER { get; set; }
            public int ID_OPER { get; set; }
            public string NM_OPER_CONCAT
            {
                get
                {
                    return (this.NM_SIGLA_OPER + " - " + this.NOM_OPER);
                }
            }
        }

        /// <summary>
        /// Carrega todos os planos de saúde de uma determinada operadora recebida como parâmetro
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        /// <param name="mostraPadrao"></param>
        public static void CarregaPlanosSaude(DropDownList ddl, string idOperadora, bool Relatorio, bool mostraPadrao = true, bool TodasOperadoras = false, bool insereVazio = false)
        {
            int idOper = (!string.IsNullOrEmpty(idOperadora) ? int.Parse(idOperadora) : 0);

            var res = (from tb251 in TB251_PLANO_OPERA.RetornaTodosRegistros()
                       //Se for para trazer todas as operadoras e o id da operadora for 0, traz todas
                       where (TodasOperadoras && idOper == 0 ? 0 == 0 : tb251.TB250_OPERA.ID_OPER == idOper)
                       && tb251.FL_SITU_PLAN == "A"
                       select new { tb251.NOM_PLAN, tb251.ID_PLAN });

            ddl.DataTextField = "NOM_PLAN";
            ddl.DataValueField = "ID_PLAN";
            ddl.DataSource = res;
            ddl.DataBind();

            if (mostraPadrao)
            {
                if (Relatorio)
                    ddl.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddl.Items.Insert(0, new ListItem("Selecione", ""));
            }

            if (insereVazio)
                ddl.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Carrega todos os planos de saúde de uma determinada operadora recebendo esta como objeto dropdownlist
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        /// <param name="mostraPadrao"></param>
        public static void CarregaPlanosSaude(DropDownList ddl, DropDownList ddlOper, bool Relatorio, bool mostraPadrao = true, bool insereVazio = false, bool siglaNome = false)
        {
            int idOper = (!string.IsNullOrEmpty(ddlOper.SelectedValue) ? int.Parse(ddlOper.SelectedValue) : 0);

            var res = (from tb251 in TB251_PLANO_OPERA.RetornaTodosRegistros()
                       where (idOper != 0 ? tb251.TB250_OPERA.ID_OPER == idOper : true)
                       && tb251.FL_SITU_PLAN == "A"
                       select new saidaPlanoSaude { NOM_PLAN = tb251.NOM_PLAN, ID_PLAN = tb251.ID_PLAN, NM_SIGLA_PLAN = tb251.NM_SIGLA_PLAN });

            //Trata para mostrar a concatenação caso tenha sido parametrizado dessa maneira
            ddl.DataTextField =  "NOM_PLAN";
            ddl.DataValueField = "ID_PLAN";
            ddl.DataSource = res;
            ddl.DataBind();

            if (mostraPadrao)
            {
                if (Relatorio)
                    ddl.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddl.Items.Insert(0, new ListItem("Selecione", ""));
            }

            if (insereVazio)
                ddl.Items.Insert(0, new ListItem("", ""));
        }

        public class saidaPlanoSaude
        {
            public string NOM_PLAN { get; set; }
            public string NM_SIGLA_PLAN { get; set; }
            public int ID_PLAN { get; set; }
            public string NOM_PLAN_CONCAT
            {
                get
                {
                    return (this.NM_SIGLA_PLAN + " - " + this.NOM_PLAN);
                }
            }
        }

        /// <summary>
        /// Carrega as categorias do plano de saúde recebido como parâmetro
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="ddlPlan"></param>
        /// <param name="Relatorio"></param>
        /// <param name="mostraPadrao"></param>
        /// <param name="insereVazio"></param>
        public static void CarregaCategoriaPlanoSaude(DropDownList ddl, DropDownList ddlPlan, bool Relatorio, bool mostraPadrao = true, bool insereVazio = false, bool siglaNome = false)
        {
            int idPlan = (!string.IsNullOrEmpty(ddlPlan.SelectedValue) ? int.Parse(ddlPlan.SelectedValue) : 0);

            var res = (from tbs367 in TB367_CATEG_PLANO_SAUDE.RetornaTodosRegistros()
                       where tbs367.TB251_PLANO_OPERA.ID_PLAN == idPlan
                       && tbs367.FL_SITUA == "A"
                       select new saidaCategoriaPlanoSaude
                       {
                           NM_CATEG = tbs367.NM_CATEG,
                           ID_CATEG_PLANO_SAUDE = tbs367.ID_CATEG_PLANO_SAUDE,
                           NM_SIGLA_CATEG = tbs367.NM_SIGLA_CATEG
                       }).ToList();

            //Mostra a sigla caso tenha sido parametrizado dessa maneira
            ddl.DataTextField = (siglaNome ? "NOM_CATEG_CONCAT" : "NM_CATEG");
            ddl.DataValueField = "ID_CATEG_PLANO_SAUDE";
            ddl.DataSource = res;
            ddl.DataBind();

            if (mostraPadrao)
            {
                if (Relatorio)
                    ddl.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddl.Items.Insert(0, new ListItem("Selecione", ""));
            }

            if (insereVazio)
                ddl.Items.Insert(0, new ListItem("", ""));
        }

        public class saidaCategoriaPlanoSaude
        {
            public string NM_CATEG { get; set; }
            public string NM_SIGLA_CATEG { get; set; }
            public int ID_CATEG_PLANO_SAUDE { get; set; }
            public string NOM_CATEG_CONCAT
            {
                get
                {
                    return (this.NM_SIGLA_CATEG + " - " + this.NM_CATEG);
                }
            }
        }

        #endregion

        #region Carregamento de Dores

        /// <summary>
        /// Carrega as Dores cadastradas e insere como lista em dropdownlist recebido como parâmetro
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaTiposDores(DropDownList ddl, bool Relatorio, bool mostraPadrao = true, bool CarregaItemVazio = false)
        {
            var res = (from tbs337 in TBS337_TIPO_DORES.RetornaTodosRegistros()
                       where tbs337.CO_SITUA_TIPO_DORES == "A"
                       select new { tbs337.NM_TIPO_DORES, tbs337.ID_TIPO_DORES }).OrderBy(w => w.NM_TIPO_DORES).ToList();

            if (res != null)
            {
                ddl.DataTextField = "NM_TIPO_DORES";
                ddl.DataValueField = "ID_TIPO_DORES";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (mostraPadrao)
            {
                if (Relatorio)
                    ddl.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddl.Items.Insert(0, new ListItem("Selecione", ""));
            }

            if (CarregaItemVazio)
                ddl.Items.Insert(0, new ListItem("", ""));
        }

        #endregion

        #region Carregamento de Consultas



        /// <summary>
        /// Carrega todos os tipos de agendamentos possíveis de forma fixa
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        /// <param name="sigla"></param>
        public static void CarregaTiposAgendamento(DropDownList ddl, bool Relatorio, bool sigla = false, bool insereVazio = false, int? CO_EMP = null, ETiposClassificacoes tpClass = ETiposClassificacoes.padrao)
        {
            if (sigla)
            {
                ddl.Items.Insert(0, new ListItem("TO", "TO"));
                ddl.Items.Insert(0, new ListItem("OU", "OU"));
                ddl.Items.Insert(0, new ListItem("ES", "ES"));
                ddl.Items.Insert(0, new ListItem("NT", "NT"));
                ddl.Items.Insert(0, new ListItem("PI", "PI"));
                ddl.Items.Insert(0, new ListItem("FO", "FO"));
                ddl.Items.Insert(0, new ListItem("FI", "FI"));
                ddl.Items.Insert(0, new ListItem("EN", "EN"));
                ddl.Items.Insert(0, new ListItem("AO", "AO"));
                ddl.Items.Insert(0, new ListItem("AM", "AM"));
            }
            else
            {
                ddl.Items.Insert(0, new ListItem("Outros", "OU"));
                ddl.Items.Insert(0, new ListItem("Terapia Ocupacional", "TO"));
                ddl.Items.Insert(0, new ListItem("Nutrição", "NT"));
                ddl.Items.Insert(0, new ListItem("Estética", "ES"));
                ddl.Items.Insert(0, new ListItem("Psicologia", "PI"));
                ddl.Items.Insert(0, new ListItem("Fonoaudiologia", "FO"));
                ddl.Items.Insert(0, new ListItem("Fisioterapia", "FI"));
                ddl.Items.Insert(0, new ListItem("Enfermaria", "EN"));
                ddl.Items.Insert(0, new ListItem("Atendimento Odontológico", "AO"));
                ddl.Items.Insert(0, new ListItem("Atendimento Médico", "AM"));
            }

            if (tpClass == ETiposClassificacoes.agendamento)
            {
                if (CO_EMP.HasValue && CO_EMP.Value != 0)
                {
                    var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                               join tb83 in TB83_PARAMETRO.RetornaTodosRegistros() on tb25.TB83_PARAMETRO.CO_EMP equals tb83.CO_EMP
                               where tb25.CO_EMP == CO_EMP
                               select new
                               {
                                   tb83.FL_PERM_AGEND_ENFER,
                                   tb83.FL_PERM_AGEND_FISIO,
                                   tb83.FL_PERM_AGEND_FONOA,
                                   tb83.FL_PERM_AGEND_MEDIC,
                                   tb83.FL_PERM_AGEND_ODONT,
                                   tb83.FL_PERM_AGEND_OUTRO,
                                   tb83.FL_PERM_AGEND_PSICO,
                                   tb83.FL_PERM_AGEND_TERAP_OCUPA,
                                   tb83.FL_PERM_AGEND_ESTET,
                                   tb83.FL_PERM_AGEND_NUTRI
                               }).FirstOrDefault();

                    if (res.FL_PERM_AGEND_MEDIC != "S")
                        ddl.Items.Remove(ddl.Items.FindByValue("AM"));

                    if (res.FL_PERM_AGEND_ENFER != "S")
                        ddl.Items.Remove(ddl.Items.FindByValue("EN"));

                    if (res.FL_PERM_AGEND_FISIO != "S")
                        ddl.Items.Remove(ddl.Items.FindByValue("FI"));

                    if (res.FL_PERM_AGEND_FONOA != "S")
                        ddl.Items.Remove(ddl.Items.FindByValue("FO"));

                    if (res.FL_PERM_AGEND_ODONT != "S")
                        ddl.Items.Remove(ddl.Items.FindByValue("AO"));

                    if (res.FL_PERM_AGEND_ESTET != "S")
                        ddl.Items.Remove(ddl.Items.FindByValue("ES"));

                    if (res.FL_PERM_AGEND_NUTRI != "S")
                        ddl.Items.Remove(ddl.Items.FindByValue("NT"));

                    if (res.FL_PERM_AGEND_OUTRO != "S")
                        ddl.Items.Remove(ddl.Items.FindByValue("OU"));

                    if (res.FL_PERM_AGEND_PSICO != "S")
                        ddl.Items.Remove(ddl.Items.FindByValue("PI"));

                    if (res.FL_PERM_AGEND_TERAP_OCUPA != "S")
                        ddl.Items.Remove(ddl.Items.FindByValue("TO"));
                }
            }

            if (Relatorio)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));

            if (insereVazio)
                ddl.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Carrega os tipos de consulta em controle DropDownList recebido como parâmetro
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaTiposConsulta(DropDownList ddl, bool Relatorio, bool insereVazio = false)
        {
            //ddl.Items.Insert(0, new ListItem("Consulta Inicial", "I"));
            //ddl.Items.Insert(0, new ListItem("Consulta Normal", "N"));
            //ddl.Items.Insert(0, new ListItem("Consulta Retorno", "R"));
            //ddl.Items.Insert(0, new ListItem("Consulta Emergêncial", "M"));
            //ddl.Items.Insert(0, new ListItem("Procedimento", "P"));
            //ddl.Items.Insert(0, new ListItem("Exame", "E"));
            //ddl.Items.Insert(0, new ListItem("Cirurgia", "C"));
            //ddl.Items.Insert(0, new ListItem("Vacina", "V"));
            //ddl.Items.Insert(0, new ListItem("Outros", "O"));
            //ddl.Items.Insert(0, new ListItem("Urgência", "U"));
            ddl.Items.Insert(0, new ListItem("(OU) Outros", "O"));
            ddl.Items.Insert(0, new ListItem("(PR) Procedimento", "P"));
            ddl.Items.Insert(0, new ListItem("(FA) Farmácia", "F"));
            ddl.Items.Insert(0, new ListItem("(VA) Vacina", "V"));
            ddl.Items.Insert(0, new ListItem("(SA) Serviço Ambulatorial", "S"));
            ddl.Items.Insert(0, new ListItem("(EX) Exame", "E"));
            ddl.Items.Insert(0, new ListItem("(CG) Consulta Gestante", "G"));
            ddl.Items.Insert(0, new ListItem("(CR) Consulta Retorno", "R"));
            ddl.Items.Insert(0, new ListItem("(CN) Consulta Normal", "N"));
            ddl.Items.Insert(0, new ListItem("(CI) Consulta Inicial", "I"));
            ddl.Items.Insert(0, new ListItem("(CE) Consulta Emergencial", "M"));

            if (Relatorio)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));

            if (insereVazio)
                ddl.Items.Insert(0, new ListItem("", ""));
        }

        public static void CarregaClassTiposConsulta(DropDownList ddl, string tipoConsulta)
        {
            ddl.Items.Clear();
            switch (tipoConsulta)
            {
                case "N":
                    ddl.Items.Insert(0, new ListItem("Normal", "CN"));
                    ddl.Items.Insert(0, new ListItem("Avaliação", "CA"));
                    ddl.Items.Insert(0, new ListItem("Encaixe", "CE"));
                    ddl.Items.Insert(0, new ListItem("Retorno", "CR"));
                    ddl.Items.Insert(0, new ListItem("Outros", "CO"));
                    break;
                case "P":
                    ddl.Items.Insert(0, new ListItem("Cirúrgico", "PC"));
                    ddl.Items.Insert(0, new ListItem("Ambulatorial", "PA"));
                    ddl.Items.Insert(0, new ListItem("Outros", "CO"));
                    break;
                case "E":
                    ddl.Items.Insert(0, new ListItem("Interno", "EI"));
                    ddl.Items.Insert(0, new ListItem("Externo", "EE"));
                    break;
                case "V":
                    ddl.Items.Insert(0, new ListItem("Outros", "VO"));
                    break;
                default:
                    ddl.Items.Insert(0, new ListItem("-", ""));
                    break;
            }
        }

        #endregion

        #region Carregametno de Função Simplificada

        /// <summary>
        /// Carrega as funções simplificadas da tb366
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaFuncoesSimplificadas(DropDownList ddl, bool Relatorio)
        {
            var res = (from tbs366 in TBS366_FUNCAO_SIMPL.RetornaTodosRegistros()
                       select new { tbs366.ID_FUNCAO_SIMPL, tbs366.NM_FUNCAO }).OrderBy(w => w.NM_FUNCAO).ToList();

            if (res != null)
            {
                ddl.DataTextField = "NM_FUNCAO";
                ddl.DataValueField = "ID_FUNCAO_SIMPL";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (Relatorio)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        #endregion

        #region Carregamento de profissionais indicativos

        /// <summary>
        /// Carrega os profissionais indicadores em objeto dropdownlist recebido como parâmetro
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaIndicadores(DropDownList ddl, bool Relatorio)
        {
            var res = (from tbs377 in TBS377_INDIC_PACIENTES.RetornaTodosRegistros()
                       where tbs377.CO_SITUA == "A"
                       select new saidaIndicadores
                       {
                           ID_INDIC_PACIENTES = tbs377.ID_INDIC_PACIENTES,
                           NM_PROFI_INDIC = tbs377.NM_PROFI_INDIC,
                           NU_CPF = tbs377.NU_CPF
                       }).OrderBy(w => w.NM_PROFI_INDIC).ToList();

            if (res.Count > 0)
            {
                ddl.DataTextField = "NM_PROFI_INDIC";
                ddl.DataValueField = "ID_INDIC_PACIENTES";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (Relatorio)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        public class saidaIndicadores
        {
            public int ID_INDIC_PACIENTES { get; set; }
            public string NM_PROFI_INDIC { get; set; }
            public string NU_CPF { get; set; }
            public string concat
            {
                get
                {
                    if (!string.IsNullOrEmpty(this.NU_CPF))
                        return AuxiliFormatoExibicao.preparaCPFCNPJ(this.NU_CPF) + " - " + this.NM_PROFI_INDIC;
                    else
                        return this.NM_PROFI_INDIC;
                }
            }
        }

        #endregion

        #region Carregamento de Restrições de Atendimento

        /// <summary>
        /// Carrega as restrições de atendimento
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaRestricoesAtendimento(DropDownList ddl, bool Relatorio)
        {
            var res = (from tbs379 in TBS379_RESTR_ATEND.RetornaTodosRegistros()
                       where tbs379.CO_SITUA == "A"
                       select new saidaRestricoesAtendimento
                       {
                           ID_RESTR_ATEND = tbs379.ID_RESTR_ATEND,
                           NM_RESTR = tbs379.NM_RESTR,
                           CO_RESTR = tbs379.CO_RESTR,
                       }).ToList();

            if (res.Count > 0)
            {
                ddl.DataTextField = "concat";
                ddl.DataValueField = "ID_RESTR_ATEND";
                ddl.DataSource = res;
                ddl.DataBind();
            }

            if (Relatorio)
            {
                ddl.Items.Insert(0, new ListItem("Nenhuma", "N"));
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        public class saidaRestricoesAtendimento
        {
            public string NM_RESTR { get; set; }
            public int ID_RESTR_ATEND { get; set; }
            public string CO_RESTR { get; set; }
            public string concat
            {
                get
                {
                    return this.CO_RESTR + " - " + this.NM_RESTR;
                }
            }
        }

        #endregion

        #region Carregamento de Informações de Saúde

        /// <summary>
        /// Carrega as informações de saúde em componente listbox
        /// </summary>
        /// <param name="lst"></param>
        public static void CarregaInformacoesSaude(ListBox lst)
        {
            var res = (from tbs382 in TBS382_INFOS_GERAIS.RetornaTodosRegistros()
                       where tbs382.CO_SITUA == "A"
                       select new { tbs382.ID_INFOS_GERAIS, tbs382.NM_INFOS_GERAIS }).OrderBy(w => w.NM_INFOS_GERAIS).ToList();

            lst.DataTextField = "NM_INFOS_GERAIS";
            lst.DataValueField = "ID_INFOS_GERAIS";
            lst.DataSource = res;
            lst.DataBind();
        }

        /// <summary>
        /// Carregamento de Formulas de Aplicação
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregarFormulasAplicacao(DropDownList ddl, bool Relatorio)
        {
            var res = (from tbs406 in TBS406_FORML_APLIC.RetornaTodosRegistros()
                       where tbs406.CO_SITUA == "A"
                       select new
                       {
                           tbs406.ID_FORML,
                           tbs406.NO_FORML
                       }).OrderBy(w => w.NO_FORML).ToList();

            ddl.DataTextField = "NO_FORML";
            ddl.DataValueField = "ID_FORML";
            ddl.DataSource = res;
            ddl.DataBind();

            if (Relatorio)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Carregamento de planos alimentares
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregarPlanosAlimentares(DropDownList ddl, bool Relatorio)
        {
            var res = (from tbs407 in TBS407_PLANOS_ALIMEN.RetornaTodosRegistros()
                       where tbs407.CO_SITUA == "A"
                       select new
                       {
                           tbs407.ID_PLANO,
                           tbs407.NO_PLANO
                       }).OrderBy(w => w.NO_PLANO).ToList();

            ddl.DataTextField = "NO_PLANO";
            ddl.DataValueField = "ID_PLANO";
            ddl.DataSource = res;
            ddl.DataBind();

            if (Relatorio)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        #endregion

        #region Carregamento de Deficiências Novas

        /// <summary>
        /// Carregamento novo das deficiências
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaDeficienciasNova(DropDownList ddl, bool Relatorio)
        {
            var res = (from tbs387 in TBS387_DEFIC.RetornaTodosRegistros()
                       where tbs387.CO_SITUA == "A"
                       select new
                       {
                           tbs387.NM_DEFIC,
                           tbs387.ID_DEFIC,
                       }).OrderBy(w => w.NM_DEFIC).ToList();

            ddl.DataTextField = "NM_DEFIC";
            ddl.DataValueField = "ID_DEFIC";
            ddl.DataSource = res;
            ddl.DataBind();

            if (Relatorio)
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
            else
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        #endregion

        #region Carregamento de Ocorrências

        /// <summary>
        /// Carrega uma lista de tipos de Ocorrência
        /// </summary>
        /// <param name="ddlProfiSaude"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaTiposOcorrencia(DropDownList ddl, bool Relatorio)
        {
            ddl.Items.Insert(0, new ListItem("Outros", "X"));
            ddl.Items.Insert(0, new ListItem("Telemarketing", "T"));
            ddl.Items.Insert(0, new ListItem("Recepção", "R"));
            ddl.Items.Insert(0, new ListItem("Ouvidoria", "O"));
            ddl.Items.Insert(0, new ListItem("Financeiro", "F"));
            ddl.Items.Insert(0, new ListItem("Cobrança", "C"));
            ddl.Items.Insert(0, new ListItem("Administrativo", "A"));

            if (Relatorio == false)
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
        }

        public static void CarregaTiposOcorrenciaParceiros(DropDownList ddl, bool Relatorio)
        {
            ddl.Items.Insert(0, new ListItem("Outros", "X"));
            ddl.Items.Insert(0, new ListItem("Telemarketing", "T"));
            ddl.Items.Insert(0, new ListItem("Recepção", "R"));
            ddl.Items.Insert(0, new ListItem("Ouvidoria", "O"));
            ddl.Items.Insert(0, new ListItem("Financeiro", "F"));
            ddl.Items.Insert(0, new ListItem("Cobrança", "C"));
            ddl.Items.Insert(0, new ListItem("Administrativo", "A"));
            ddl.Items.Insert(0, new ListItem("Pesquisa", "P"));

            if (Relatorio == false)
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion

        #region Carregamento de Comissoes

        /// <summary>
        /// Carrega uma lista de tipos de Comissoes
        /// </summary>
        /// <param name="ddlProfiSaude"></param>
        /// <param name="CO_EMP"></param>
        /// <param name="Relatorio"></param>
        public static void CarregaTiposComissao(DropDownList ddl, bool Relatorio)
        {
            ddl.Items.Insert(0, new ListItem("Planejamento", "PLA"));
            ddl.Items.Insert(0, new ListItem("Indicação de Procedimento", "IPR"));
            ddl.Items.Insert(0, new ListItem("Indicação de Paciente", "IPC"));
            ddl.Items.Insert(0, new ListItem("Contrato", "CNT"));
            ddl.Items.Insert(0, new ListItem("Cobrança/Negociação", "CBR"));
            ddl.Items.Insert(0, new ListItem("Avaliação", "AVL"));

            if (Relatorio == false)
                ddl.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddl.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion

        #region Carregamento de Resultados

        /// <summary>
        /// Carrega uma lista de tipos de Resultados
        /// </summary>
        public static void CarregaTiposResultado(DropDownList ddl, bool Relatorio, bool mostraPadrao = true)
        {
            ddl.Items.Insert(0, new ListItem("RP - Resultado Parcial", "RP"));
            ddl.Items.Insert(0, new ListItem("RF - Resultado Final", "RF"));
            ddl.Items.Insert(0, new ListItem("RE - Refazer Exame", "RE"));
            ddl.Items.Insert(0, new ListItem("NR - Não Realizado", "NR"));
            ddl.Items.Insert(0, new ListItem("CA - Item Cancelado", "CA"));

            if (mostraPadrao)
                if (Relatorio)
                    ddl.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddl.Items.Insert(0, new ListItem("Selecione", ""));
        }

        #endregion

        #endregion

        internal static void CarregaModelos(DropDownList DropDownListModelo,int ColaboradorId,string ModeloId,string Tipo)
        {
            var res = (from modelo in TBS461_ATEND_MODEL_PRESC_MEDIC.RetornaTodosRegistros().Where(x => x.FL_TIPO_MODELO == Tipo)
                       where (modelo.TB03_COLABOR.CO_COL == ColaboradorId)

                       select new { modelo.ID_MODEL_MEDIC, modelo.NO_MODEL_MEDIC }).OrderBy(w => w.ID_MODEL_MEDIC).OrderBy(x => x.NO_MODEL_MEDIC).ToList();

            if (res.Count > 0)
            {
                DropDownListModelo.DataValueField = "ID_MODEL_MEDIC";
                DropDownListModelo.DataTextField = "NO_MODEL_MEDIC";
                DropDownListModelo.DataSource = res;
                DropDownListModelo.DataBind();
                DropDownListModelo.Items.Insert(0, new ListItem("Selecione", ""));
                DropDownListModelo.SelectedValue = ModeloId;
            }
        }
    }
}
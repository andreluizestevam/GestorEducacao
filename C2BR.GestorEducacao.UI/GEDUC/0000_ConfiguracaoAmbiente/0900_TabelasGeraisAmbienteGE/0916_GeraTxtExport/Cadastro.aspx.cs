//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: TIPO DE TELEFONE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas

using System.Text;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Data;
using System.ServiceModel;
using System.IO;
using System.Reflection;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3010_CtrlPedagogicoSeries;
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno;
using System.Data.Objects;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0900_TabelasGeraisAmbienteGE._0916_GeraTxtExport
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            StringBuilder sConteudo = new StringBuilder();
            string caminhoArquivo = "";

            if (string.IsNullOrEmpty(txtCami.Text))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o local onde deverá ser salvo");
                return;
            }
            caminhoArquivo = @txtCami.Text;
            string nomea = "";
            //Gera a tabela que foi escolhida no parâmetro
            switch (ddlTab.SelectedValue)
            {
                    //Se foi aluno
                case "A":
                    caminhoArquivo = caminhoArquivo + @"\Alunos.txt";

                    var resAlu = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                  join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tb07.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP
                                  select new aluno
                                  {
                                      NU_NIRE = tb07.NU_NIRE,
                                      NO_ALU = tb07.NO_ALU,
                                      CO_SEX_ALU = tb07.CO_SEXO_ALU,
                                      DT_NASC_ALU = tb07.DT_NASC_ALU,
                                      NO_MAE = tb07.NO_MAE_ALU,
                                      NO_PAI = tb07.NO_PAI_ALU,
                                      NO_RESP = tb108.NO_RESP,
                                  }).ToList();

                    sConteudo.AppendLine("Nire, Nome, Nascimento, Sexo, Nome Pai, Nome Mãe, Nome Responsável");
                    //Percorre todos os alunos criando uma para cada um, uma linha de informações
                    foreach (var al in resAlu)
                    {
                        string auxalu = al.NU_NIRE + ";" + al.NO_ALU + ";" + al.DT_NASC_ALU + ";" + al.CO_SEX_ALU + ";" + al.NO_PAI + ";" + al.NO_MAE + ";" + al.NO_RESP;
                        sConteudo.AppendLine(auxalu);
                    }
                    nomea = "Alunos.txt";

                    break;
                case "R":
                    caminhoArquivo += @"\Responsaveis.txt";

                    var resResp = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                               join tb74 in TB74_UF.RetornaTodosRegistros() on tb108.TB74_UF.CODUF equals tb74.CODUF
                               join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb108.CO_CIDADE equals tb904.CO_CIDADE
                               join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb108.CO_BAIRRO equals tb905.CO_BAIRRO
                               select new responsavel
                               {
                                   NO_RESP = tb108.NO_RESP,
                                   NU_CPF = tb108.NU_CPF_RESP,
                                   DT_NASC_RESP = tb108.DT_NASC_RESP,
                                   CO_SEXO_RESP = tb108.CO_SEXO_RESP,
                                   COUF = tb74.CODUF,
                                   NO_CIDADE = tb904.NO_CIDADE,
                                   NO_BAIRRO = tb905.NO_BAIRRO,
                                   CO_ENDE_RESP = tb108.DE_ENDE_RESP,
                                   NR_TELE_CELU = tb108.NU_TELE_CELU_RESP,
                                   NR_TELE_FIXO = tb108.NU_TELE_RESI_RESP,
                               }).ToList();

                    sConteudo.AppendLine("CPF, Nome, Nascimento, Sexo, Endereço, Telefone Fixo, Telefone Celular");

                    //Percorre todos os responsáveis criando uma para cada um, uma linha de informações
                    foreach (var ar in resResp)
                    {
                        string auxResp = ar.NU_CPF +";" + ar.NO_RESP + ";" + ar.DT_NASC_RESP + ";" + ar.CO_SEXO_RESP + ";" + ar.concatEnde + ";" + ar.NR_TELE_FIXO + ";" + ar.NR_TELE_CELU;
                        sConteudo.AppendLine(auxResp);
                    }

                    break;
                case "M":
                    caminhoArquivo = caminhoArquivo + @"\Matricula.txt";

                    var resMatr = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                   join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb08.TB44_MODULO.CO_MODU_CUR equals tb44.CO_MODU_CUR
                                   join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                                   join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb129.CO_TUR
                                   join tb06 in TB06_TURMAS.RetornaTodosRegistros() on tb129.CO_TUR equals tb06.CO_TUR
                                   select new Matricula
                                   {
                                       nire = tb08.TB07_ALUNO.NU_NIRE,
                                       CO_ANO_MES_MAT = tb08.CO_ANO_MES_MAT,
                                       modalidade = tb44.DE_MODU_CUR,
                                       serie = tb01.NO_CUR,
                                       turma = tb129.NO_TURMA,
                                       turno = tb06.CO_PERI_TUR,
                                       CO_SITUA = (tb08.CO_SIT_MAT == "A" ? "Matriculado" : tb08.CO_SIT_MAT == "C" ? "Cancelado" : tb08.CO_SIT_MAT == "X" ? "Transferido" :
tb08.CO_SIT_MAT == "T" ? "MT" : tb08.CO_SIT_MAT == "D" ? "DE" : tb08.CO_SIT_MAT == "I" ? "IN" :
tb08.CO_SIT_MAT == "J" ? "JU" : tb08.CO_SIT_MAT == "Y" ? "TC" : "SM"),
                                   }).ToList();

                    sConteudo.AppendLine("Nire, Ano, Modalidade, Serie, Turma, Turno, Situação");

                    //Percorre todos as matrículas criando uma para cada uma, uma linha de informações
                    foreach (var am in resMatr)
                    {
                        string auxMatr = am.nire + ";" + am.CO_ANO_MES_MAT + ";" + am.modalidade + ";" + am.serie + ";" + am.turma + ";" + am.turno + ";" + am.CO_SITUA;
                        sConteudo.AppendLine(auxMatr);
                    }

                    break;
                case "H":
                    caminhoArquivo = caminhoArquivo + @"\Historico.txt";

                    var resHis = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                  join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb079.CO_ALU equals tb07.CO_ALU 
                                  join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb079.CO_MODU_CUR equals tb44.CO_MODU_CUR
                                  join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb079.CO_CUR equals tb01.CO_CUR 
                                  join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb079.CO_TUR equals tb129.CO_TUR
                                  join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb079.CO_MAT equals tb02.CO_MAT
                                  join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                  select new Historico
                                  {
                                      NU_NIRE = tb07.NU_NIRE,
                                      DE_MODU_CUR = tb44.DE_MODU_CUR,
                                      NO_CUR = tb01.NO_CUR,
                                      NO_TUR = tb129.NO_TURMA,
                                      CO_ANO = tb079.CO_ANO_REF,
                                      NO_MATERIA = tb107.NO_MATERIA,
                                      MEDIAB1 = tb079.VL_NOTA_BIM1,
                                      MEDIAB2 = tb079.VL_NOTA_BIM2,
                                      MEDIAB3 = tb079.VL_NOTA_BIM3,
                                      MEDIAB4 = tb079.VL_NOTA_BIM4,
                                      MEDIAFINAL = tb079.VL_MEDIA_FINAL,

                                  }).ToList();

                    sConteudo.AppendLine("Nire, Modalidade, Serie, Turma, Ano, Disciplina, Notas Bimestrais 1,2,3 e 4, Media Final");

                    //Percorre todos as Histórico criando uma para cada uma, uma linha de informações
                    foreach (var ah in resHis)
                    {
                        string auxHist = ah.NU_NIRE + ";" + ah.DE_MODU_CUR + ";" + ah.DE_MODU_CUR + ";" + ah.NO_CUR + ";" +
                            ah.CO_ANO + ";" + ah.NO_MATERIA + ";" + ah.MEDIAB1 + ";" + ah.MEDIAB2 + ";" + ah.MEDIAB3 + ";" + ah.MEDIAB4 + ";" 
                            + ah.MEDIAFINAL;
                        sConteudo.AppendLine(auxHist);
                    }

                    break;
                case "F":
                    caminhoArquivo = caminhoArquivo + @"\Financeiro.txt";

                    var resFina = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                   join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tb47.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP
                                   join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb47.CO_ALU equals tb07.CO_ALU
                                   join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb47.CO_MODU_CUR equals tb44.CO_MODU_CUR
                                   join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb47.CO_CUR equals tb01.CO_CUR
                                   join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb47.CO_TUR equals tb129.CO_TUR
                                   join tb45 in TB045_NOS_NUM.RetornaTodosRegistros() on tb47.NU_DOC equals tb45.NU_DOC into l1
                                   from lnosn in l1.DefaultIfEmpty()
                                   select new Financeiro
                                   {
                                       NU_CPF_RESP = tb108.NU_CPF_RESP,
                                       NU_NIRE = tb07.NU_NIRE,
                                       DE_MODU_CUR = tb44.DE_MODU_CUR,
                                       NO_CUR = tb01.NO_CUR,
                                       NO_TUR = tb129.NO_TURMA,
                                       NU_DOC = tb47.NU_DOC,
                                       NU_PAR = tb47.NU_PAR,
                                       VL_PAR = tb47.VR_PAR_DOC,
                                       SITUACAO = tb47.IC_SIT_DOC == "A" ? "Aberto" : tb47.IC_SIT_DOC == "Q" ? "Quitado" : "",
                                       CO_NOSS_NUMERO = lnosn.CO_NOS_NUM,
                                       DT_VENC = tb47.DT_VEN_DOC,
                                       DT_PAGTO = lnosn.DT_PGTO,
                                   }).ToList();

                    sConteudo.AppendLine("CPF Responsável, Nire, Modalidade, Serie, Turma, Nº Doc, Parcela, Valor, Situação, Nosso Número, Data Vencimento, Data Pagamento");

                    //Percorre todos as Histórico Financeiro criando uma para cada um, uma linha de informações
                    foreach (var af in resFina)
                    {
                        string auxFina = af.NU_CPF_RESP + ";" + af.NU_NIRE + ";" + af.DE_MODU_CUR + ";" + af.NO_CUR + ";" +
                            af.NO_TUR + ";" + af.NU_DOC + ";" + af.NU_PAR + ";" + af.VL_PAR + ";" + af.SITUACAO + ";" + af.CO_NOSS_NUMERO + ";" + af.DT_VENC + ";" + af.DT_PAGTO;
                        sConteudo.AppendLine(auxFina);
                    }

                    break;

                case "T":
                    caminhoArquivo = caminhoArquivo + @"\Atividades.txt";

                    var resAtiv = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                                   join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb119.CO_MODU_CUR equals tb44.CO_MODU_CUR
                                   join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb119.CO_CUR equals tb01.CO_CUR
                                   join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb119.CO_TUR equals tb129.CO_TUR
                                   join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb119.CO_MAT equals tb02.CO_MAT
                                   join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                   join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb119.CO_COL_ATIV equals tb03.CO_COL
                                   select new Atividades
                                   {
                                       NO_COL = tb03.NO_COL,
                                       DE_MODU_CUR = tb44.DE_MODU_CUR,
                                       NO_CUR = tb01.NO_CUR,
                                       NO_TUR = tb129.NO_TURMA,
                                       NO_MATERIA = tb107.NO_MATERIA,
                                       ANO = tb119.CO_ANO_MES_MAT,
                                       DATA = tb119.DT_ATIV_REAL,
                                       TEMA = tb119.DE_TEMA_AULA,
                                       DESC_ATIVIDADE = tb119.DE_RES_ATIV,
                                       CODIGO_ATIVIDADE = tb119.CO_ATIV_PROF_TUR,
                                       FL_HOMOL = tb119.FL_HOMOL_ATIV,
                                   }).ToList();

                    sConteudo.AppendLine("Professor(a), Modalidade, Curso, Turma, Disciplina, Ano, Data Atividade, Tema, Descrição, Código da Atividade, Homologação");

                    //Percorre todos as Histórico Financeiro criando uma para cada um, uma linha de informações
                    foreach (var af in resAtiv)
                    {
                        string auxAtiv = af.NO_COL + ";" + af.DE_MODU_CUR + ";" + af.NO_CUR + ";" + af.NO_TUR + ";" +
                            af.NO_MATERIA + ";" + af.ANO + ";" + af.DATA + ";" + af.TEMA + ";" + af.DESC_ATIVIDADE + ";"
                            + af.CODIGO_ATIVIDADE + ";" + af.FL_HOMOL;
                        sConteudo.AppendLine(auxAtiv);
                    }

                    break;

                case "Q":
                    caminhoArquivo = caminhoArquivo + @"\Frequencias.txt";

                    var resFreq = (from tb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                                   join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb132.TB07_ALUNO.CO_ALU equals tb07.CO_ALU
                                   join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb132.TB01_CURSO.CO_CUR equals tb01.CO_CUR
                                   join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb132.CO_TUR equals tb129.CO_TUR
                                   join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb132.CO_MAT equals tb02.CO_MAT
                                   join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                   select new Frequencias
                                   {
                                       NU_NIRE = tb07.NU_NIRE,
                                       DE_MODU_CUR = tb01.TB44_MODULO.DE_MODU_CUR,
                                       NO_CUR = tb01.NO_CUR,
                                       NO_TUR = tb129.NO_TURMA,
                                       NO_MATERIA = tb107.NO_MATERIA,
                                       ANO = tb132.CO_ANO_REFER_FREQ_ALUNO,
                                       DATA = tb132.DT_FRE,
                                       FLAG_FREQ = tb132.CO_FLAG_FREQ_ALUNO,
                                       CODIGO_ATIVIDADE = tb132.CO_ATIV_PROF_TUR,
                                       FL_HOMOL = tb132.FL_HOMOL_FREQU,
                                   }).ToList();

                    sConteudo.AppendLine("Nire, Modalidade, Curso, Turma, Disciplina, Ano, Data da Frequência, Status Frequência, Código da Atividade, Homologação");

                    //Percorre todos as Histórico Financeiro criando uma para cada um, uma linha de informações
                    foreach (var af in resFreq)
                    {
                        string auxFreq = af.NU_NIRE + ";" + af.DE_MODU_CUR + ";" + af.NO_CUR + ";" + af.NO_TUR + ";" +
                            af.NO_MATERIA + ";" + af.ANO + ";" + af.DATA + ";" + af.FLAG_FREQ + ";" + af.CODIGO_ATIVIDADE + ";"
                            + af.FL_HOMOL;
                        sConteudo.AppendLine(auxFreq);
                    }

                    break;
            }

            //string nomearquivo = Server.MapPath("~/Backup/" + nomea);

            //Stream arquivo = File.OpenWrite(@nomearquivo);

            //Response.Clear();
            //Response.AddHeader("content-disposition", "attachment; filename=" + nomea);
            //Response.WriteFile(@nomearquivo);
            //Response.ContentType = "txt";
            //Response.End();

            File.WriteAllText(caminhoArquivo, sConteudo.ToString());
            AuxiliPagina.RedirecionaParaPaginaSucesso("Arquivo gerado com êxisto para o diretório informado.", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
        }
        #endregion

        public class aluno
        {
            public int NU_NIRE { get; set; }
            public string NO_ALU { get; set; }
            public DateTime? DT_NASC_ALU { get; set; }
            public string CO_SEX_ALU { get; set; }
            public string NO_PAI { get; set; }
            public string NO_MAE { get; set; }
            public string NO_RESP { get; set; }

        }

        public class responsavel
        {
            public string NO_RESP { get; set; }
            public string NU_CPF { get; set; }
            public DateTime? DT_NASC_RESP { get; set; }
            public string CO_SEXO_RESP { get; set; }
            public string COUF { get; set; }
            public string NO_CIDADE { get; set; }
            public string NO_BAIRRO { get; set; }
            public string CO_ENDE_RESP { get; set; }
            public string concatEnde
            {
                get
                {
                    return this.COUF + "," + this.NO_CIDADE + "," + this.NO_BAIRRO + "," + this.CO_ENDE_RESP;
                }
            }
            public string NR_TELE_FIXO { get; set; }
            public string NR_TELE_CELU { get; set; }
        }

        public class Matricula
        {
            public int nire { get; set; }
            public string CO_ANO_MES_MAT { get; set; }
            public string modalidade { get; set; }
            public string serie { get; set; }
            public string turma { get; set; }
            public string turno { get; set; }
            public string CO_SITUA { get; set; }
                
        }
        public class Historico
        {
            public int NU_NIRE { get; set; }
            public string DE_MODU_CUR { get; set; }
            public string NO_CUR { get; set; }
            public string NO_TUR { get; set; }
            public string CO_ANO { get; set; }
            public string NO_MATERIA { get; set; }
            public decimal? MEDIAB1 { get; set; }
            public decimal? MEDIAB2 { get; set; }
            public decimal? MEDIAB3 { get; set; }
            public decimal? MEDIAB4 { get; set; }
            public decimal? MEDIAFINAL { get; set; }
        }

        public class Financeiro
        {
            public string NU_CPF_RESP { get; set; }
            public int NU_NIRE { get; set; }
            public string DE_MODU_CUR { get; set; }
            public string NO_CUR { get; set; }
            public string NO_TUR { get; set; }
            public string NU_DOC { get; set; }
            public int NU_PAR { get; set; }
            public decimal VL_PAR { get; set; }
            public string SITUACAO { get; set; }
            public string CO_NOSS_NUMERO { get; set; }
            public DateTime DT_VENC { get; set; }
            public DateTime? DT_PAGTO { get; set; }
        }

        public class Atividades
        {
            public string NO_COL { get; set; }
            public string DE_MODU_CUR { get; set; }
            public string NO_CUR { get; set; }
            public string NO_TUR { get; set; }
            public string NO_MATERIA { get; set; }
            public string ANO { get; set; }
            public DateTime DATA { get; set; }
            public string TEMA { get; set; }
            public string DESC_ATIVIDADE { get; set; }
            public int CODIGO_ATIVIDADE { get; set; }
            public string FL_HOMOL { get; set; }
        }

        public class Frequencias
        {
            public int NU_NIRE { get; set; }
            public string DE_MODU_CUR { get; set; }
            public string NO_CUR { get; set; }
            public string NO_TUR { get; set; }
            public string NO_MATERIA { get; set; }
            public decimal ANO { get; set; }
            public DateTime DATA { get; set; }
            public string FLAG_FREQ { get; set; }
            public int CODIGO_ATIVIDADE { get; set; }
            public string FL_HOMOL { get; set; }
        }

    }
}

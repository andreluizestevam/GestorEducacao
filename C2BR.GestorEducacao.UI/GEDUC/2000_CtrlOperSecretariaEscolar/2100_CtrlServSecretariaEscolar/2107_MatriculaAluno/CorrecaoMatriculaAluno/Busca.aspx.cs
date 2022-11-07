//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: MATRÍCULA NOVA PARA ALUNO
// OBJETIVO: CORREÇÃO DOS DADOS DA MATRÍCULA
// DATA DE CRIAÇÃO: 24/05/2013
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;


namespace C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno.CorrecaoMatriculaAluno
{
    public partial class Busca : System.Web.UI.Page
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        public class Retorno
        {
            public int coEmp { get; set; }
            public int coAlu { get; set; }
            public int coCur { get; set; }
            public string Ano { get; set; }
            public string Semestre { get; set; }
            public string Modalidade { get; set; }
            public string Serie { get; set; }
            public string Turma { get; set; }
            public string coPeri { get; set; }
            public string Turno
            {
                get
                {
                    string t = "";
                    switch (this.coPeri)
                    {
                        case "M":
                            t = "Matutino";
                            break;
                        case "V":
                            t = "Vespertino";
                            break;
                        case "N":
                            t = "Noturno";
                            break;
                    }
                    return t;
                }
            }
            public string coSta { get; set; }
            public string coStaFreq { get; set; }
            public string Observacao
            {
                get
                {
                    return this.coSta != null ? (this.coSta == "A" && (this.coStaFreq == "A" || this.coStaFreq == null) ? "Aprovado" : "Reprovado") : "Cursando";
                }
            }
            public string Responsavel { get; set; }
            public string coSitMat { get; set; }
            public string Situacao
            {
                get
                {
                    string s = "";
                    switch (this.coSitMat)
                    {
                        case "A":
                            s = "Em Aberto";
                            break;
                        case "F":
                            s = "Finalizada";
                            break;
                        case "T":
                            s = "Trancada";
                            break;
                        case "C":
                            s = "Cancelada";
                            break;
                    }
                    return s;
                }
            }
        }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaAnos();
                CarregaModalidade();
            }
        }
        
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "coEmp", "coAlu", "coCur", "Ano", "Semestre" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "Ano",
                HeaderText = "ANO"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "Semestre",
                HeaderText = "SEM"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "Modalidade",
                HeaderText = "Modalidade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "Serie",
                HeaderText = "Série"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "Turma",
                HeaderText = "Turma"
            });


            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "Turno",
                HeaderText = "Turno"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "Observacao",
                HeaderText = "Observação"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "Responsavel",
                HeaderText = "Responsável"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "Situacao",
                HeaderText = "Situação"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int nire = txtNire.Text != "" ? int.Parse(txtNire.Text) : 0;
            int coModu = int.Parse(ddlModalidade.SelectedValue);
            int coCur = int.Parse(ddlSerie.SelectedValue);
            int coTur = int.Parse(ddlTurma.SelectedValue);
            int coAlu = int.Parse(ddlAluno.SelectedValue);

            var resultado = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                             join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                             join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb129.CO_TUR
                             join tb06 in TB06_TURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb06.CO_TUR
                             where (nire != 0 ? tb08.TB07_ALUNO.NU_NIRE == nire : nire == 0)
                             && (coAlu != 0 ? tb08.TB07_ALUNO.CO_ALU == coAlu : coAlu == 0)
                             && (coModu != 0 ? tb08.TB44_MODULO.CO_MODU_CUR == coModu : coModu == 0)
                             && (coCur != 0 ? tb08.CO_CUR == coCur : coCur == 0)
                             && (coTur != 0 ? tb08.CO_TUR == coTur : coTur == 0)
                             select new Retorno
                             {
                                 coEmp = tb08.TB25_EMPRESA.CO_EMP,
                                 coAlu = tb08.TB07_ALUNO.CO_ALU,
                                 coCur = tb01.CO_CUR,
                                 Ano = tb08.CO_ANO_MES_MAT,
                                 Semestre = tb08.NU_SEM_LET,
                                 Modalidade = tb08.TB44_MODULO.DE_MODU_CUR,
                                 Serie = tb01.NO_CUR,
                                 Turma = tb129.NO_TURMA,
                                 coPeri = tb06.CO_PERI_TUR,
                                 coSta = tb08.CO_STA_APROV,
                                 coStaFreq = tb08.CO_STA_APROV_FREQ,
                                 Responsavel = tb08.TB108_RESPONSAVEL.NO_RESP,
                                 coSitMat = tb08.CO_SIT_MAT
                             });

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoEmp, "coEmp"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "coAlu"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.CoCur, "coCur"));
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Ano, "Ano"));
            queryStringKeys.Add(new KeyValuePair<string, string>("nuSem", "Semestre"));


            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento

        public void CarregaAnos()
        {
            ddlAno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                 where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                 select new { CO_ANO_MES_MAT = tb08.CO_ANO_MES_MAT.Trim() }).OrderByDescending(m => m.CO_ANO_MES_MAT).Distinct();

            ddlAno.DataTextField = "CO_ANO_MES_MAT";
            ddlAno.DataValueField = "CO_ANO_MES_MAT";
            ddlAno.DataBind();
        }

        public void CarregaModalidade()
        {
            var tb44 = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO).OrderBy(o => o.DE_MODU_CUR);

            ddlModalidade.DataSource = tb44;
            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();
            ddlModalidade.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        public void CarregaSerie()
        {
            int coModu = int.Parse(ddlModalidade.SelectedValue);
            string anoGrade = ddlAno.SelectedValue;

            if (coModu != 0)
            {
                ddlSerie.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                       where tb01.CO_MODU_CUR == coModu
                                       join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                                       where tb43.CO_ANO_GRADE == anoGrade && tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
                                       select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                ddlSerie.DataTextField = "NO_CUR";
                ddlSerie.DataValueField = "CO_CUR";
                ddlSerie.DataBind();
            }
            else
            {
                ddlSerie.Items.Clear();
            }
            ddlSerie.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        public void CarregaTurma()
        {
            int coModu = int.Parse(ddlModalidade.SelectedValue);
            int coCur = int.Parse(ddlSerie.SelectedValue);

            if (coModu != 0 && coCur != 0)
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == coModu && tb06.CO_CUR == coCur
                                       select new { tb06.TB129_CADTURMAS.CO_SIGLA_TURMA, tb06.CO_TUR }).OrderBy(t => t.CO_SIGLA_TURMA);

                ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
            {
                ddlTurma.Items.Clear();
            }
            ddlTurma.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        public void CarregaAlunos()
        {
            int coModu = int.Parse(ddlModalidade.SelectedValue);
            int coCur = int.Parse(ddlSerie.SelectedValue);
            int coTur = int.Parse(ddlTurma.SelectedValue);
            string anoGrade = ddlAno.SelectedValue;

            if (coModu != 0 && coCur != 0 && coTur != 0)
            {
                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb07.CO_ALU equals tb08.CO_ALU
                           where tb08.CO_ANO_MES_MAT == anoGrade
                           && tb08.TB44_MODULO.CO_MODU_CUR == coModu
                           && tb08.CO_CUR == coCur
                           select new
                           {
                               tb07.CO_ALU,
                               tb07.NO_ALU
                           }).Distinct().OrderBy(o => o.NO_ALU);

                ddlAluno.DataSource = res;

                ddlAluno.DataTextField = "NO_ALU";
                ddlAluno.DataValueField = "CO_ALU";

                ddlAluno.DataBind();
            }
            else
            {
                ddlAluno.Items.Clear();
            }

            ddlAluno.Items.Insert(0, new ListItem("Todos", "0"));
        }

        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerie();
        }

        protected void ddlSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAlunos();
        }
    }
}
using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.Reports.Helper;
using C2BR.GestorEducacao.Reports.Properties;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Globalization;

namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1200_GestaoOperColaboradores._1240_ContrDeDeclaracoes
{
    public partial class RptDeclaracoesFuncionais : C2BR.GestorEducacao.Reports.Base.RptDeclaracao
    {
        public RptDeclaracoesFuncionais()
        {
            InitializeComponent();
        }

        public int InitReport(int codEmp, int codCol, string tipoDoc, string SiglaDoc)
        {
            try
            {
                #region Setar o Header e as Labels

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(codEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.bsHeader.Clear();
                this.bsHeader.Add(header);

                #endregion
                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Matricula

                var res = from c in ctx.TB03_COLABOR
                          join uc in ctx.TB25_EMPRESA on c.CO_EMP_UNID_CONT equals uc.CO_EMP
                          join baiEmp in ctx.TB905_BAIRRO on uc.CO_BAIRRO equals baiEmp.CO_BAIRRO
                          join cidEmp in ctx.TB904_CIDADE on baiEmp.CO_CIDADE equals cidEmp.CO_CIDADE
                          join baiFunc in ctx.TB905_BAIRRO on c.CO_BAIRRO equals baiFunc.CO_BAIRRO
                          join cidFunc in ctx.TB904_CIDADE on baiFunc.CO_CIDADE equals cidFunc.CO_CIDADE
                          join f in ctx.TB15_FUNCAO on c.CO_FUN equals f.CO_FUN into fi
                          from f in fi.DefaultIfEmpty()
                          where c.CO_COL == codCol
                          select new
                          {
                              //Inicio dados Empresa
                              EmpresaNome = uc.NO_RAZSOC_EMP,
                              EmpresaCnpj = uc.CO_CPFCGC_EMP,
                              EmpEndCEP = uc.CO_CEP_EMP,
                              EmpEndDescricao = uc.DE_END_EMP,
                              EmpEndNumero = uc.NU_END_EMP,
                              EmpEndBairro = baiEmp.NO_BAIRRO,
                              EmpEndCidade = cidEmp.NO_CIDADE,
                              EmpEndUF = cidEmp.CO_UF,

                              //Dados funcionário
                              FuncNome = c.NO_COL,
                              FuncMatricula = c.CO_MAT_COL,
                              FuncFuncao = c.DE_FUNC_COL.ToUpper(),
                              FuncCargo = f.NO_FUN,
                              FuncRG = c.CO_RG_COL,
                              FuncCPF = c.NU_CPF_COL,
                              FuncNascimento = c.DT_NASC_COL,
                              FuncTelefone = c.NU_TELE_CELU_COL,
                              FuncEstCivil = c.CO_ESTADO_CIVIL,
                              FuncEndCEP = c.NU_CEP_ENDE_COL,
                              FuncEndDescricao = c.DE_ENDE_COL,
                              FuncEndNumero = c.NU_ENDE_COL,
                              FuncEndCidade = cidFunc.NO_CIDADE,
                              FuncEndBairro = baiFunc.NO_BAIRRO,
                              FuncEndUF = cidFunc.CO_UF
                          };

                var dados = res.FirstOrDefault();

                if (dados == null)
                    return -1;

                #endregion

                var lst = (from tb009 in ctx.TB009_RTF_DOCTOS
                           from tb010 in tb009.TB010_RTF_ARQUIVO.DefaultIfEmpty()
                           where tb009.TP_DOCUM == tipoDoc && tb009.CO_SIGLA_DOCUM == SiglaDoc
                           //&& tb009.TB25_EMPRESA.CO_EMP == codEmp
                           select new ContratoDetalhe
                           {
                               Pagina = tb010.NU_PAGINA,
                               Titulo = tb009.NM_TITUL_DOCUM,
                               Texto = tb010.AR_DADOS,
                               Fl_Hidelogo = tb009.FL_HIDELOGO
                           }).OrderBy(x => x.Pagina);


                if (lst != null && lst.Where(x => x.Pagina == 1).Any())
                {
                    if (lst.FirstOrDefault().Fl_Hidelogo == "N")
                    {
                        MostrarCabecalho(false);
                        GroupHeaderTitle.Visible = false;
                    }
                    else
                    {
                        MostrarCabecalho(true);
                        GroupHeaderTitle.Visible = true;
                    }
                    foreach (var Doc in lst)
                    {
                        lbltitulo.Text = Doc.Titulo;
                        SerializableString st = new SerializableString(Doc.Texto);
                        //Dados empresa
                        st.Value = st.Value.Replace("[EmpresaNome]", dados.EmpresaNome);
                        st.Value = st.Value.Replace("[EmpresaCNPJ]", Funcoes.Format(dados.EmpresaCnpj, TipoFormat.CNPJ));
                        st.Value = st.Value.Replace("[EmpEndCEP]", dados.EmpEndCEP);
                        st.Value = st.Value.Replace("[EmpEndDescricao]", dados.EmpEndDescricao);
                        st.Value = st.Value.Replace("[EmpEndNumero]", dados.EmpEndNumero.ToString());
                        st.Value = st.Value.Replace("[EmpEndBairro]", dados.EmpEndBairro.ToString());
                        st.Value = st.Value.Replace("[EmpEndCidade]", dados.EmpEndCidade);
                        st.Value = st.Value.Replace("[EmpEndUF]", dados.EmpEndUF);
                        st.Value = st.Value.Replace("[CidadeEstado]", dados.EmpEndCidade);
                        st.Value = st.Value.Replace("[Bairro]", dados.EmpEndBairro.ToString());
                        // Dados funcionário
                        st.Value = st.Value.Replace("[NomeFunc]", dados.FuncNome);
                        st.Value = st.Value.Replace("[MatriculaFuncionario]", dados.FuncMatricula);
                        st.Value = st.Value.Replace("[FuncaoFunc]", dados.FuncFuncao);
                        st.Value = st.Value.Replace("[CargoFunc]", dados.FuncCargo);
                        st.Value = st.Value.Replace("[RGFunc]", dados.FuncRG);
                        st.Value = st.Value.Replace("[CPFFunc]", Funcoes.Format(dados.FuncCPF, TipoFormat.CPF));
                        st.Value = st.Value.Replace("[FuncionarioNasc]", dados.FuncNascimento.ToString("dd/MM/yyyy"));
                        st.Value = st.Value.Replace("[TelFuncionario]", dados.FuncTelefone);
                        st.Value = st.Value.Replace("[EstadoCivil]", dados.FuncEstCivil);
                        st.Value = st.Value.Replace("[FuncEndCEP]", dados.FuncEndCEP);
                        string funcEnd = dados.FuncEndDescricao + ", ";
                        funcEnd += dados.FuncEndNumero.HasValue ? dados.FuncEndNumero.Value.ToString() : "s/n";
                        funcEnd += ", " + dados.FuncEndBairro + ", " + dados.FuncEndCidade;
                        funcEnd += "-" + dados.FuncEndUF;
                        st.Value = st.Value.Replace("[FuncEndereco]", funcEnd);
                        st.Value = st.Value.Replace("[Data]", DateTime.Today.ToString("dd/MM/yyyy"));
                        st.Value = st.Value.Replace("[DataDia]", DateTime.Today.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("pt-BR")));

                        //=======> Data atual
                        st.Value = st.Value.Replace("[DiaAtual]", DateTime.Now.Day.ToString());

                        #region Mês Atual
                        switch (DateTime.Now.Month.ToString())
                        {
                            case "1":
                                st.Value = st.Value.Replace("[MesAtual]", "Janeiro");
                                break;
                            case "2":
                                st.Value = st.Value.Replace("[MesAtual]", "Fevereiro");
                                break;
                            case "3":
                                st.Value = st.Value.Replace("[MesAtual]", "Março");
                                break;
                            case "4":
                                st.Value = st.Value.Replace("[MesAtual]", "Abril");
                                break;
                            case "5":
                                st.Value = st.Value.Replace("[MesAtual]", "Maio");
                                break;
                            case "6":
                                st.Value = st.Value.Replace("[MesAtual]", "Junho");
                                break;
                            case "7":
                                st.Value = st.Value.Replace("[MesAtual]", "Julho");
                                break;
                            case "8":
                                st.Value = st.Value.Replace("[MesAtual]", "Agosto");
                                break;
                            case "9":
                                st.Value = st.Value.Replace("[MesAtual]", "Setembro");
                                break;
                            case "10":
                                st.Value = st.Value.Replace("[MesAtual]", "Outubro");
                                break;
                            case "11":
                                st.Value = st.Value.Replace("[MesAtual]", "Novembro");
                                break;
                            case "12":
                                st.Value = st.Value.Replace("[MesAtual]", "Desembro");
                                break;
                        }
                        #endregion

                        st.Value = st.Value.Replace("[AnoAtual]", DateTime.Now.Year.ToString());

                        switch (Doc.Pagina)
                        {
                            case 1:
                                {
                                    richPagina1.Rtf = st.Value;
                                    richPagina1.Visible = true;
                                    break;
                                }
                        }
                    }
                }

                return 1;
            }
            catch { return 0; }
        }
        public class ContratoDetalhe
        {
            public bool HideLogo { get; set; }
            public string Titulo { get; set; }
            public string SubTitulo { get; set; }
            public int Pagina { get; set; }
            public string Texto { get; set; }
            public string Fl_Hidelogo { get; set; }
        }
    }
}
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

namespace C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2101_ServSolicItensServicEscolar
{
    public partial class RptDeclaracaoPasseEstudantil : C2BR.GestorEducacao.Reports.Base.RptDeclaracao
    {
        public RptDeclaracaoPasseEstudantil()
        {
            InitializeComponent();
        }

        public int InitReport(int codEmp, string codAluCad, string ano, int coModCur, int coCur, int coTurma, string strDiaSem)
        {
            try
            {
                #region Setar o Header e as Labels

                // Instancia o header do relatorio
                long intCodAluCad = long.Parse(codAluCad);
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

                var res = from m in ctx.TB08_MATRCUR
                          join tu in ctx.TB06_TURMAS on m.CO_TUR equals tu.CO_TUR
                          join t in ctx.TB129_CADTURMAS on m.CO_TUR equals t.CO_TUR
                          join cur in ctx.TB01_CURSO on m.CO_CUR equals cur.CO_CUR
                          join uc in ctx.TB25_EMPRESA on m.CO_EMP_UNID_CONT equals uc.CO_EMP
                          join unid in ctx.TB25_EMPRESA on codEmp equals unid.CO_EMP
                          join bEmp in ctx.TB905_BAIRRO on uc.CO_BAIRRO equals bEmp.CO_BAIRRO
                          join cidEmp in ctx.TB904_CIDADE on bEmp.CO_CIDADE equals cidEmp.CO_CIDADE
                          join bResp in ctx.TB905_BAIRRO on m.TB07_ALUNO.TB108_RESPONSAVEL.CO_BAIRRO equals bResp.CO_BAIRRO
                          join cidResp in ctx.TB904_CIDADE on bEmp.CO_CIDADE equals cidResp.CO_CIDADE
                          //join horario in ctx.TB131_TEMPO_AULA on new { m.CO_EMP, m.TB44_MODULO.CO_MODU_CUR, m.CO_CUR, tu.CO_PERI_TUR } equals new { horario.CO_EMP, horario.CO_MODU_CUR, horario.CO_CUR, horario.TP_TURNO }
                          where m.CO_ALU == intCodAluCad
                            && (m.CO_SIT_MAT == "A" || m.CO_SIT_MAT == "R")
                            && m.CO_EMP == codEmp
                            && m.CO_ANO_MES_MAT == ano
                            && (coModCur != 0 ? m.TB44_MODULO.CO_MODU_CUR == coModCur : 0 == 0)
                            && (coCur != 0 ? m.CO_CUR == coCur : 0 == 0) 
                            && (coTurma != 0 ? m.CO_TUR == coTurma : 0 == 0)
                          select new
                          {
                              //Dados Instituição
                              nomeSecretario = (unid.TB000_INSTITUICAO.TB149_PARAM_INSTI != null ?
                              (unid.TB000_INSTITUICAO.TB149_PARAM_INSTI.TB03_COLABOR != null ?
                              unid.TB000_INSTITUICAO.TB149_PARAM_INSTI.TB03_COLABOR.NO_COL : "") : ""),

                              //Dados Turma
                              turmaInicio = t.DT_INICI_AULA,
                              turmaTermin = t.DT_TERMI_AULAS,

                              //Inicio dados Empresa
                              EmpEndDescricao = uc.DE_END_EMP,
                              EmpEndNumero = uc.NU_END_EMP,
                              EmpEndComplemento = uc.DE_COM_ENDE_EMP != null ? uc.DE_COM_ENDE_EMP : "*****",
                              EmpEndBairro = bEmp.NO_BAIRRO,
                              EmpEndUf = uc.CO_UF_EMP,
                              EmpEndCEP = uc.CO_CEP_EMP,
                              CidadeEstado = cidEmp.NO_CIDADE,
                              EmpresaNome = uc.NO_RAZSOC_EMP,
                              EmpTelGeral = uc.cablinha4,
                              EmpresaCnpj = uc.CO_CPFCGC_EMP,
                              EmpWebSite = uc.NO_WEB_EMP,
                              EmpEmailGeral = uc.NO_EMAIL_EMP,
                              EmpTelSecretaria = uc.CO_TEL1_EMP,
                              EmpresaUF = cidEmp.CO_UF,
                              //!= null ? uc.CO_TEL1_EMP : "*****",
                              //Dados diretoria
                              DirNome = uc.TB000_INSTITUICAO.NO_RESPO_CONTR,
                              DirCPF = uc.TB000_INSTITUICAO.NU_CPF_RESPO_CONTR,
                              //----> DirRG = uc.TB000_INSTITUICAO.NU,
                              //Dados alunos
                              AlunoNome = m.TB07_ALUNO.NO_ALU,
                              NIRE = m.TB07_ALUNO.NU_NIRE,
                              AlunoPai = String.IsNullOrEmpty(m.TB07_ALUNO.NO_PAI_ALU) ? "*****" : m.TB07_ALUNO.NO_PAI_ALU,
                              AlunoMae = String.IsNullOrEmpty(m.TB07_ALUNO.NO_MAE_ALU) ? "*****" : m.TB07_ALUNO.NO_MAE_ALU,
                              AlunoNasc = m.TB07_ALUNO.DT_NASC_ALU != null ? m.TB07_ALUNO.DT_NASC_ALU.Value : DateTime.MinValue,
                              AlunoNatural = String.IsNullOrEmpty(m.TB07_ALUNO.DE_NATU_ALU) ? "*****" : m.TB07_ALUNO.DE_NATU_ALU,
                              AlunoRGNumero = m.TB07_ALUNO.CO_RG_ALU != null ? m.TB07_ALUNO.CO_RG_ALU : "*****",
                              AlunoCPF = m.TB07_ALUNO.NU_CPF_ALU != null ? m.TB07_ALUNO.NU_CPF_ALU : "*****",
                              AlunoRGOrgao = m.TB07_ALUNO.CO_ORG_RG_ALU != null ? m.TB07_ALUNO.CO_ORG_RG_ALU : "*****",
                              AlunoRGData = m.TB07_ALUNO.DT_EMIS_RG_ALU != null ? m.TB07_ALUNO.DT_EMIS_RG_ALU : DateTime.MinValue,
                              AlunoRGUF = m.TB07_ALUNO.CO_UF_CARTORIO != null ? m.TB07_ALUNO.CO_UF_CARTORIO : "*****",
                              AlunoCertidaoLivro = m.TB07_ALUNO.DE_CERT_LIVRO != null ? m.TB07_ALUNO.DE_CERT_LIVRO : "*****",
                              AlunoCertidaoFolha = m.TB07_ALUNO.NU_CERT_FOLHA != null ? m.TB07_ALUNO.NU_CERT_FOLHA : "*****",
                              AlunoCertidaoTipo = m.TB07_ALUNO.TP_CERTIDAO != null ? m.TB07_ALUNO.TP_CERTIDAO : "*****",
                              AlunoCertidaoNumero = m.TB07_ALUNO.NU_CERT != null ? m.TB07_ALUNO.NU_CERT : "*****",
                              AlunoCertidaoCartorio = m.TB07_ALUNO.NO_CIDA_CARTORIO_ALU,
                              AlunoContratoCodigo = cur.TB009_RTF_DOCTOS != null ? cur.TB009_RTF_DOCTOS.ID_DOCUM : 0,
                              AlunoCurso = String.IsNullOrEmpty(cur.NO_CUR) ? "*****" : cur.NO_CUR,
                              AlunoTurma = String.IsNullOrEmpty(t.NO_TURMA) ? "*****" : t.NO_TURMA,
                              AlunoUf = String.IsNullOrEmpty(m.TB07_ALUNO.CO_ESTA_ALU) ? "*****" : m.TB07_ALUNO.CO_ESTA_ALU,
                              AlunoTurno = t.TB06_TURMAS.Where(r => r.CO_TUR == t.CO_TUR).FirstOrDefault().CO_PERI_TUR,
                              AlunoCidade = m.TB07_ALUNO.TB108_RESPONSAVEL.TB904_CIDADE.NO_CIDADE != null ? m.TB07_ALUNO.TB108_RESPONSAVEL.TB904_CIDADE.NO_CIDADE : "*****",
                              AlunoBairro = String.IsNullOrEmpty(m.TB07_ALUNO.TB905_BAIRRO.NO_BAIRRO) ? "*****" : m.TB07_ALUNO.TB905_BAIRRO.NO_BAIRRO,
                              AlunoEnd = String.IsNullOrEmpty(m.TB07_ALUNO.DE_ENDE_ALU) ? "*****" : m.TB07_ALUNO.DE_ENDE_ALU,
                              AlunoCEP = String.IsNullOrEmpty(m.TB07_ALUNO.CO_CEP_ALU) ? "*****" : m.TB07_ALUNO.CO_CEP_ALU,
                              AlunoIniAula = ctx.TB131_TEMPO_AULA.Where(w => w.CO_EMP == m.CO_EMP && w.CO_MODU_CUR == m.TB44_MODULO.CO_MODU_CUR && w.CO_CUR == m.CO_CUR && w.TP_TURNO == tu.CO_PERI_TUR && w.NR_TEMPO == ctx.TB131_TEMPO_AULA.Where(d => d.CO_EMP == m.CO_EMP && d.CO_MODU_CUR == m.TB44_MODULO.CO_MODU_CUR && d.CO_CUR == m.CO_CUR && d.TP_TURNO == tu.CO_PERI_TUR).Min(s => s.NR_TEMPO)).FirstOrDefault().HR_INICIO,
                              AlunoFimAula = ctx.TB131_TEMPO_AULA.Where(w => w.CO_EMP == m.CO_EMP && w.CO_MODU_CUR == m.TB44_MODULO.CO_MODU_CUR && w.CO_CUR == m.CO_CUR && w.TP_TURNO == tu.CO_PERI_TUR && w.NR_TEMPO == ctx.TB131_TEMPO_AULA.Where(d => d.CO_EMP == m.CO_EMP && d.CO_MODU_CUR == m.TB44_MODULO.CO_MODU_CUR && d.CO_CUR == m.CO_CUR && d.TP_TURNO == tu.CO_PERI_TUR).Max(s => s.NR_TEMPO)).FirstOrDefault().HR_TERMI,
                              AlunoModulo = String.IsNullOrEmpty(m.TB44_MODULO.DE_MODU_CUR) ? "*****" : m.TB44_MODULO.DE_MODU_CUR,
                              AlunoNatuUf = String.IsNullOrEmpty(m.TB07_ALUNO.CO_UF_NATU_ALU) ? "*****" : m.TB07_ALUNO.CO_UF_NATU_ALU,
                              AnoLetivo = m.CO_ANO_MES_MAT,
                              AlunoSexo = String.IsNullOrEmpty(m.TB07_ALUNO.CO_SEXO_ALU) ? "*****" : m.TB07_ALUNO.CO_SEXO_ALU,
                              AlunoTel = String.IsNullOrEmpty(m.TB07_ALUNO.NU_TELE_RESI_ALU) ? "*****" : m.TB07_ALUNO.NU_TELE_RESI_ALU,
                              //Dados responsavel
                              ResponsavelEndereco = m.TB07_ALUNO.TB108_RESPONSAVEL.NU_ENDE_RESP,
                              RespNome = String.IsNullOrEmpty(m.TB108_RESPONSAVEL.NO_RESP) ? "*****" : m.TB108_RESPONSAVEL.NO_RESP,
                              RespEnd = m.TB07_ALUNO.TB108_RESPONSAVEL.DE_ENDE_RESP,
                              RespEndNumero = m.TB07_ALUNO.TB108_RESPONSAVEL.NU_ENDE_RESP,
                              RespBairro = bResp.NO_BAIRRO,
                              RespCidade = cidResp.NO_CIDADE,
                              RespUF = cidResp.CO_UF,
                              RespCep = m.TB07_ALUNO.TB108_RESPONSAVEL.CO_CEP_RESP,
                              RespCPF = m.TB07_ALUNO.TB108_RESPONSAVEL.NU_CPF_RESP,
                              RespRG = m.TB07_ALUNO.TB108_RESPONSAVEL.CO_RG_RESP,
                              RespRGORG = m.TB07_ALUNO.TB108_RESPONSAVEL.CO_ORG_RG_RESP,
                              RespRGEST = m.TB07_ALUNO.TB108_RESPONSAVEL.CO_ESTA_RG_RESP != null ? m.TB07_ALUNO.TB108_RESPONSAVEL.CO_ESTA_RG_RESP : "*****",

                          };
                #region dell
                var dados = res.FirstOrDefault();

                if (dados == null)
                    return -1;
                //TB47_CTA_RECEB
                //var par = (from m in ctx.TB47_CTA_RECEB where m.CO_ALU == dados.AlunoCodigo
                //           && m.CO_CUR == dados.AlunoCursoCodigo && m.CO_MODU_CUR == dados.AlunoModuloCodigo
                //           && m.NU_DOC.Substring(0,2) == "MN"
                //           select new { 
                //                m.DT_VEN_DOC,
                //                m.VR_DES_DOC,
                //                m.VR_PAR_DOC,
                //                m.VL_DES_BOLSA_ALUNO
                //           }).ToList();                 
                #endregion
                #endregion

                var lst = (from tb009 in ctx.TB009_RTF_DOCTOS
                           from tb010 in tb009.TB010_RTF_ARQUIVO.DefaultIfEmpty()
                           where tb009.TP_DOCUM == "DE" && tb009.CO_SIGLA_DOCUM == "DXXX003PASSE"//tb009.TP_DOCUM == "DE" && tb009.CO_SIGLA_DOCUM == "DA"
                           //b009.ID_DOCUM && tb009.TP_DOCUM == "DE" && tb009.CO_SITUS_DOCUM == "A"
                           //where tb009.ID_DOCUM == 6 
                           //&& tb009.TP_DOCUM == "CC" && tb009.CO_SITUS_DOCUM == "A"
                           //tb009.ID_DOCUM == dados.AlunoContratoCodigo //
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
                        GroupHeader1.Visible = false;
                    }
                    else
                    {
                        MostrarCabecalho(true);
                        GroupHeader1.Visible = true;
                    }
                    foreach (var Doc in lst)
                    {
                        //lblTitulo.Text = Doc.Titulo.Replace("[AnoLetivo]", dados.AnoLetivo);
                        // string st.Value = Doc.Texto;
                        lblTitulo.Text = Doc.Titulo;

                        SerializableString st = new SerializableString(Doc.Texto);
                        //Dados empresa
                        st.Value = st.Value.Replace("[EmpresaNome]", dados.EmpresaNome);
                        st.Value = st.Value.Replace("[EmpEndDescricao]", dados.EmpEndDescricao);
                        st.Value = st.Value.Replace("[EmpEndNumero]", dados.EmpEndNumero.ToString());
                        st.Value = st.Value.Replace("[EmpEndComplemento]", dados.EmpEndComplemento);
                        st.Value = st.Value.Replace("[EmpEndUf]", dados.EmpEndUf);
                        st.Value = st.Value.Replace("[EmpEndBairro]", dados.EmpEndBairro.ToString());
                        st.Value = st.Value.Replace("[EmpEndCEP]", dados.EmpEndCEP);
                        st.Value = st.Value.Replace("[CidadeEstado]", dados.CidadeEstado);
                        st.Value = st.Value.Replace("[EmpTelGeral]", dados.EmpTelGeral);
                        st.Value = st.Value.Replace("[EmpTelSecretaria]", dados.EmpTelGeral);
                        st.Value = st.Value.Replace("[EmpWebSite]", dados.EmpWebSite);
                        st.Value = st.Value.Replace("[EmpEmailGeral]", dados.EmpEmailGeral);
                        st.Value = st.Value.Replace("[EmpresaCNPJ]", Funcoes.Format(dados.EmpresaCnpj, TipoFormat.CNPJ));
                        //Secretário
                        st.Value = st.Value.Replace("[noSecrEscolar]", dados.nomeSecretario);
                        // Dados Diretoria
                        st.Value = st.Value.Replace("[NomeDirGeral]", dados.DirNome);
                        st.Value = st.Value.Replace("[CPFDiretorGeral]", Funcoes.Format(dados.DirCPF, TipoFormat.CPF));
                        st.Value = st.Value.Replace("[FuncaoDirGeral]", "Diretora Geral");
                        st.Value = st.Value.Replace("[RegMecDiretor]", "");
                        //Dados responsaveis
                        st.Value = st.Value.Replace("[ResponsavelNome]", dados.AlunoPai);
                        string respEnd = dados.RespEnd + ", ";
                        respEnd += dados.RespEndNumero.HasValue ? dados.RespEndNumero.Value.ToString() : "s/n";
                        respEnd += ", " + dados.RespBairro + ", " + dados.RespCidade;
                        respEnd += "-" + dados.RespUF;
                        st.Value = st.Value.Replace("[ResponsavelEndereco]", respEnd);
                        st.Value = st.Value.Replace("[RespUf]", dados.RespUF);
                        st.Value = st.Value.Replace("[RespCidade]", dados.RespCidade);
                        st.Value = st.Value.Replace("[RespBairro]", dados.RespBairro);
                        st.Value = st.Value.Replace("[AlunoPai]", dados.AlunoPai);
                        st.Value = st.Value.Replace("[AlunoMae]", dados.AlunoMae);
                        //Dados Aluno                       
                        st.Value = st.Value.Replace("[AlunoNome]", dados.AlunoNome);
                        st.Value = st.Value.Replace("[NIRE]", dados.NIRE.ToString());
                        st.Value = st.Value.Replace("[AlunoNasc]", dados.AlunoNasc.ToString("dd/MM/yyyy"));
                        st.Value = st.Value.Replace("[AlunoNatural]", dados.AlunoNatural);
                        st.Value = st.Value.Replace("[AlunoRGNumero]", dados.AlunoRGNumero);
                        st.Value = st.Value.Replace("[AlunoCPF]", dados.AlunoCPF);
                        st.Value = st.Value.Replace("[AlunoCertidaoNumero]", dados.AlunoCertidaoNumero.ToString());
                        st.Value = st.Value.Replace("[AlunoCertidaoLivro]", dados.AlunoCertidaoLivro);
                        st.Value = st.Value.Replace("[AlunoUf]", dados.AlunoUf);
                        st.Value = st.Value.Replace("[AlunoCidade]", dados.AlunoCidade);
                        st.Value = st.Value.Replace("[AlunoBairro]", dados.AlunoBairro);
                        st.Value = st.Value.Replace("[AlunoEnd]", dados.AlunoEnd);
                        st.Value = st.Value.Replace("[AlunoCEP]", dados.AlunoCEP);
                        st.Value = st.Value.Replace("[AlunoIniAula]", (!string.IsNullOrEmpty(dados.AlunoIniAula) ? dados.AlunoIniAula.ToString() : ""));
                        st.Value = st.Value.Replace("[AlunoFimAula]", (!string.IsNullOrEmpty(dados.AlunoFimAula) ? dados.AlunoFimAula.ToString() : ""));
                        st.Value = st.Value.Replace("[HoraAulaAluno]", dados.AlunoIniAula + " às " + dados.AlunoFimAula);
                        st.Value = st.Value.Replace("[EnderecoAluno]", dados.AlunoEnd + ", " + dados.AlunoBairro + ", " + dados.AlunoCidade + " / " + dados.AlunoUf);
                        st.Value = st.Value.Replace("[AlunoTel]", dados.AlunoTel);
                        st.Value = st.Value.Replace("[AlunoMatricula]", Convert.ToString(dados.NIRE) + "/" + dados.AnoLetivo);
                        switch (dados.AlunoSexo)
                        {
                            case "M":
                                st.Value = st.Value.Replace("[AlunoSexo]", "Masculino");
                                break;
                            case "F":
                                st.Value = st.Value.Replace("[AlunoSexo]", "Feminino");
                                break;
                            default:
                                st.Value = st.Value.Replace("[AlunoSexo]", "****");
                                break;
                        }
                        //Dados da Turma
                        st.Value = st.Value.Replace("[InicioAulas]", ( dados.turmaInicio.HasValue ? dados.turmaInicio.Value.ToString("dd/MM/yyyy") : "*****"));
                        st.Value = st.Value.Replace("[TerminoAulas]", (dados.turmaTermin.HasValue ? dados.turmaTermin.Value.ToString("dd/MM/yyyy") : "*****"));

                        switch (dados.AlunoTurno.ToString())
                        {
                            case "M":
                                st.Value = st.Value.Replace("[AlunoTurno]", "Matutino");
                                break;
                            case "V":
                                st.Value = st.Value.Replace("[AlunoTurno]", "Vespertino");
                                break;
                            case "N":
                                st.Value = st.Value.Replace("[AlunoTurno]", "Noturno");
                                break;
                        }

                        st.Value = st.Value.Replace("[Serie]", dados.AlunoCurso);
                        st.Value = st.Value.Replace("[AlunoTurma]", dados.AlunoTurma);
                        st.Value = st.Value.Replace("[Modalidade]", dados.AlunoModulo);
                        st.Value = st.Value.Replace("[AlunoCertidaoFolha]", dados.AlunoCertidaoFolha);
                        st.Value = st.Value.Replace("[AlunoCertidaoCartorio]", dados.AlunoCertidaoCartorio);
                        st.Value = st.Value.Replace("[AlunoRGOrgao]", dados.AlunoRGOrgao);
                        st.Value = st.Value.Replace("[AlunoRGData]", dados.AlunoRGData.ToString());
                        st.Value = st.Value.Replace("[AlunoNasc]", dados.AlunoNasc.ToString("dd/MM/yyyy"));
                        st.Value = st.Value.Replace("[AlunoRGUF]", dados.AlunoRGUF);
                        st.Value = st.Value.Replace("[AlunoCertidaoTipo]", dados.AlunoCertidaoTipo);
                        st.Value = st.Value.Replace("[AnoLetivo]", dados.AnoLetivo);
                        st.Value = st.Value.Replace("[CidadeEstado]", dados.CidadeEstado + "-" + dados.EmpresaUF);
                        st.Value = st.Value.Replace("[DiaSem]", strDiaSem);
                        st.Value = st.Value.Replace("[DataDia]", DateTime.Today.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("pt-BR")));
                        st.Value = st.Value.Replace("[Data]", DateTime.Today.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("pt-BR")));

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
                                    lblConteudo.Rtf = st.Value;
                                    lblConteudo.Visible = true;
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

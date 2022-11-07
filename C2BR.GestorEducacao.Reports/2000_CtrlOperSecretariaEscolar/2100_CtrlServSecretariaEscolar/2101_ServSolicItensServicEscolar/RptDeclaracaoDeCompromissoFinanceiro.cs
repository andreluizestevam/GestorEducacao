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
    public partial class RptDeclaracaoDeCompromissoFinanceiro : C2BR.GestorEducacao.Reports.Base.RptDeclaracao
    {
        public RptDeclaracaoDeCompromissoFinanceiro()
        {
            InitializeComponent();
        }
        public int InitReport(int codEmp, string codAluCad)
        {
            try
            {
                #region Setar o Header e as Labels

                // Instancia o header do relatorio
                int intCodAluCad = int.Parse(codAluCad);
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
                          join t in ctx.TB129_CADTURMAS on m.CO_TUR equals t.CO_TUR
                          join cur in ctx.TB01_CURSO on m.CO_CUR equals cur.CO_CUR
                          join uc in ctx.TB25_EMPRESA on m.CO_EMP_UNID_CONT equals uc.CO_EMP
                          join unid in ctx.TB25_EMPRESA on codEmp equals unid.CO_EMP
                          join bEmp in ctx.TB905_BAIRRO on uc.CO_BAIRRO equals bEmp.CO_BAIRRO
                          join cidEmp in ctx.TB904_CIDADE on bEmp.CO_CIDADE equals cidEmp.CO_CIDADE
                          join bResp in ctx.TB905_BAIRRO on m.TB07_ALUNO.TB108_RESPONSAVEL.CO_BAIRRO equals bResp.CO_BAIRRO
                          join cidResp in ctx.TB904_CIDADE on bEmp.CO_CIDADE equals cidResp.CO_CIDADE
                          where m.CO_ALU == intCodAluCad
                          select new
                          {
                              //Dados Instituição
                              nomeSecretario = (unid.TB000_INSTITUICAO.TB149_PARAM_INSTI != null ?
                              (unid.TB000_INSTITUICAO.TB149_PARAM_INSTI.TB03_COLABOR != null ?
                              unid.TB000_INSTITUICAO.TB149_PARAM_INSTI.TB03_COLABOR.NO_COL : "") : ""),

                              //Inicio dados Empresa
                              EmpEndDescricao = uc.DE_END_EMP,
                              EmpEndNumero = uc.NU_END_EMP,
                              EmpEndComplemento = uc.DE_COM_ENDE_EMP != null ? uc.DE_COM_ENDE_EMP : "*****",
                              EmpEndBairro = bEmp.NO_BAIRRO,
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
                              AlunoCodigo = m.TB07_ALUNO.CO_ALU,
                              AlunoNome = m.TB07_ALUNO.NO_ALU,
                              NIRE = m.TB07_ALUNO.NU_NIRE,
                              AlunoPai = m.TB07_ALUNO.NO_PAI_ALU,
                              AlunoMae = m.TB07_ALUNO.NO_MAE_ALU,
                              AlunoSexo = m.TB07_ALUNO.CO_SEXO_ALU,
                              AlunoNasc = m.TB07_ALUNO.DT_NASC_ALU != null ? m.TB07_ALUNO.DT_NASC_ALU.Value : DateTime.MinValue,
                              AlunoNatural = m.TB07_ALUNO.DE_NATU_ALU,
                              AlunoRGNumero = m.TB07_ALUNO.CO_RG_ALU != null ? m.TB07_ALUNO.CO_RG_ALU : "*****",
                              AlunoRGOrgao = m.TB07_ALUNO.CO_ORG_RG_ALU != null ? m.TB07_ALUNO.CO_ORG_RG_ALU : "*****",
                              AlunoRGData = m.TB07_ALUNO.DT_EMIS_RG_ALU != null ? m.TB07_ALUNO.DT_EMIS_RG_ALU : DateTime.MinValue,
                              AlunoRGUF = m.TB07_ALUNO.CO_UF_CARTORIO != null ? m.TB07_ALUNO.CO_UF_CARTORIO : "*****",
                              AlunoCertidaoLivro = m.TB07_ALUNO.DE_CERT_LIVRO != null ? m.TB07_ALUNO.DE_CERT_LIVRO : "*****",
                              AlunoCertidaoFolha = m.TB07_ALUNO.NU_CERT_FOLHA != null ? m.TB07_ALUNO.NU_CERT_FOLHA : "*****",
                              AlunoCertidaoTipo = m.TB07_ALUNO.TP_CERTIDAO != null ? m.TB07_ALUNO.TP_CERTIDAO : "*****",
                              AlunoCertidaoNumero = m.TB07_ALUNO.NU_CERT != null ? m.TB07_ALUNO.NU_CERT : "*****",
                              AlunoCertidaoCartorio = m.TB07_ALUNO.NO_CIDA_CARTORIO_ALU,
                              AlunoContratoCodigo = cur.TB009_RTF_DOCTOS != null ? cur.TB009_RTF_DOCTOS.ID_DOCUM : 0,
                              AlunoCurso = cur.NO_CUR,
                              AlunoCoTurma = t.CO_TUR,
                              AlunoCodCurso = cur.CO_CUR,
                              AlunoModulo = m.TB44_MODULO.DE_MODU_CUR,
                              AlunoCodModulo = m.TB44_MODULO.CO_MODU_CUR,
                              AlunoNatuUf = m.TB07_ALUNO.CO_UF_NATU_ALU,
                              AnoLetivo = m.CO_ANO_MES_MAT,
                              //Dados responsavel
                              ResponsavelEndereco = m.TB07_ALUNO.TB108_RESPONSAVEL.NU_ENDE_RESP,
                              RespNome = m.TB07_ALUNO.TB108_RESPONSAVEL.NO_RESP,
                              RespSexo = m.TB07_ALUNO.TB108_RESPONSAVEL.CO_SEXO_RESP,
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

                // Retorna as parcelas
                var par = (from m in ctx.TB47_CTA_RECEB
                           where m.CO_ALU == dados.AlunoCodigo
                               && m.CO_TUR == dados.AlunoCoTurma
                               && m.NU_DOC.Substring(0, 2) == "MN"
                           select new
                           {
                               m.DT_VEN_DOC,
                               m.VR_DES_DOC,
                               m.VR_PAR_DOC,
                               m.VL_DES_BOLSA_ALUNO,
                               m.VR_TOT_DOC
                           }).ToList();

                // Retorna os valores de material didático
                var mD = (from m in ctx.TB47_CTA_RECEB
                          where m.CO_ALU == dados.AlunoCodigo
                              && m.CO_TUR == dados.AlunoCoTurma
                              && m.NU_DOC.Substring(0, 2) == "SM"
                          select new
                          {
                              m.DT_VEN_DOC,
                              m.VR_DES_DOC,
                              m.VR_PAR_DOC,
                              m.VL_DES_BOLSA_ALUNO,
                              m.VR_TOT_DOC
                          }).ToList();

                #endregion
                #endregion

                var lst = (from tb009 in ctx.TB009_RTF_DOCTOS
                           from tb010 in tb009.TB010_RTF_ARQUIVO.DefaultIfEmpty()
                           where tb009.TP_DOCUM == "DE" && tb009.CO_SIGLA_DOCUM == "DCF"
                           //b009.ID_DOCUM && tb009.TP_DOCUM == "DE" && tb009.CO_SITUS_DOCUM == "A"
                           //where tb009.ID_DOCUM == 6 
                           //&& tb009.TP_DOCUM == "CC" && tb009.CO_SITUS_DOCUM == "A"
                           //tb009.ID_DOCUM == dados.AlunoContratoCodigo //
                           //&& tb009.TB25_EMPRESA.CO_EMP == codEmp
                           select new ContratoDetalhe
                           {
                               Pagina = tb010.NU_PAGINA,
                               Titulo = tb009.NM_TITUL_DOCUM,
                               Texto = tb010.AR_DADOS
                           }).OrderBy(x => x.Pagina);


                if (lst != null && lst.Where(x => x.Pagina == 1).Any())
                {
                    foreach (var Doc in lst)
                    {
                        //lblTitulo.Text = Doc.Titulo.Replace("[AnoLetivo]", dados.AnoLetivo);
                        // string st.Value = Doc.Texto;
                        lbltitulo.Text = Doc.Titulo;
                        SerializableString st = new SerializableString(Doc.Texto);
                        //Dados do contrato
                        // Inclui o valor total do contrato na declaração
                        st.Value = st.Value.Replace("[ConTotal]", "R$" + par[0].VR_TOT_DOC + " (" + toExtenso(par[0].VR_TOT_DOC) + ")");

                        // Inclui o valor da primeira parcela na declaração
                        st.Value = st.Value.Replace("[ConPriParcela]", "R$" + par[0].VR_PAR_DOC + " (" + toExtenso(par[0].VR_PAR_DOC) + ")");

                        // Calcula a quantidade de parcelas menos 1, que é a primeira parcela
                        int qtdParc = par.Count() - 1;

                        // Inclui a quantidade de parcelas restante na declaração
                        st.Value = st.Value.Replace("[ConQtdParcelas]", qtdParc.ToString());

                        // Inclui o valor das demais parcelas na declaração
                        st.Value = st.Value.Replace("[ConVlParcelas]", "R$" + par[1].VR_PAR_DOC + " (" + toExtenso(par[1].VR_PAR_DOC) + ")");

                        // Pega o mes da primeira parcela
                        string mesIniPar = toExtenso(par.OrderBy(r => r.DT_VEN_DOC).First().DT_VEN_DOC.Month.ToString());

                        // Pega o mes da ultima parcela
                        string mesFimPar = toExtenso(par.OrderBy(r => r.DT_VEN_DOC).Last().DT_VEN_DOC.Month.ToString());

                        // Pega o ano da ultima parcela
                        string anoFimPar = par.OrderBy(r => r.DT_VEN_DOC).Last().DT_VEN_DOC.Year.ToString();

                        // Inclui o texto com os meses da primeira parcela, a ultima parcela e com o ano
                        st.Value = st.Value.Replace("[ConMesesAno]", mesIniPar + " a " + mesFimPar + " de " + anoFimPar);

                        // Verifica se existe valor gasto com material dedático
                        if (mD.Any())
                        {
                            // Inclui o valor gasto com material didático na declaração
                            st.Value = st.Value.Replace("[ConMaterial]", " e R$" + mD.First().VR_TOT_DOC + " (" + toExtenso(mD.First().VR_TOT_DOC) + ") referente ao material didático de " + mD.First().DT_VEN_DOC.Year.ToString());
                        }
                        else
                        {
                            // Caso não exista valor gasto com material didático retira a tag.
                            st.Value = st.Value.Replace("[ConMaterial]", "");
                        }

                        //Dados empresa
                        st.Value = st.Value.Replace("[EmpresaNome]", dados.EmpresaNome);
                        st.Value = st.Value.Replace("[EmpEndDescricao]", dados.EmpEndDescricao);
                        st.Value = st.Value.Replace("[EmpEndNumero]", dados.EmpEndNumero.ToString());
                        st.Value = st.Value.Replace("[EmpEndComplemento]", dados.EmpEndComplemento);
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
                        switch (dados.RespSexo)
                        {
                            case "M":
                                st.Value = st.Value.Replace("[ResponsavelNome]", "do Sr. " + dados.RespNome);
                                break;
                            case "F":
                                st.Value = st.Value.Replace("[ResponsavelNome]", "da Sra. " + dados.RespNome);
                                break;
                        }

                        st.Value = st.Value.Replace("[ResponsavelNM]", dados.RespNome);
                        string respEnd = dados.RespEnd + ", ";
                        respEnd += dados.RespEndNumero.HasValue ? dados.RespEndNumero.Value.ToString() : "s/n";
                        respEnd += ", " + dados.RespBairro + ", " + dados.RespCidade;
                        respEnd += "-" + dados.RespUF;
                        st.Value = st.Value.Replace("[ResponsavelEndereco]", respEnd);
                        st.Value = st.Value.Replace("[ResponsavelCpf]", dados.RespCPF.ToString().Substring(0 ,3) + "." + dados.RespCPF.ToString().Substring(3, 3) + "." + dados.RespCPF.ToString().Substring(6, 3) + "-" + dados.RespCPF.ToString().Substring(9, 2));
                        st.Value = st.Value.Replace("[AlunoPai]", dados.AlunoPai);
                        st.Value = st.Value.Replace("[AlunoMae]", dados.AlunoMae);
                        //Dados Aluno                       
                        switch (dados.AlunoSexo)
                        {
                            case "M":
                                st.Value = st.Value.Replace("[AlunoNome]", "do menor " + dados.AlunoNome.Trim());
                                break;
                            case "F":
                                st.Value = st.Value.Replace("[AlunoNome]", "da menor " + dados.AlunoNome.Trim());
                                break;
                        }
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
                        st.Value = st.Value.Replace("[NIRE]", dados.NIRE.ToString());
                        st.Value = st.Value.Replace("[AlunoNasc]", dados.AlunoNasc.ToString("dd/MM/yyyy"));
                        st.Value = st.Value.Replace("[AlunoNatural]", dados.AlunoNatural);
                        st.Value = st.Value.Replace("[AlunoRGNumero]", dados.AlunoRGNumero);
                        st.Value = st.Value.Replace("[AlunoCertidaoNumero]", dados.AlunoCertidaoNumero.ToString());
                        st.Value = st.Value.Replace("[AlunoCertidaoLivro]", dados.AlunoCertidaoLivro);
                        st.Value = st.Value.Replace("[Serie]", dados.AlunoCurso);
                        st.Value = st.Value.Replace("[Modalidade]", dados.AlunoModulo);
                        st.Value = st.Value.Replace("[AlunoCertidaoFolha]", dados.AlunoCertidaoFolha);
                        st.Value = st.Value.Replace("[AlunoCertidaoCartorio]", dados.AlunoCertidaoCartorio);
                        st.Value = st.Value.Replace("[AlunoRGOrgao]", dados.AlunoRGOrgao);
                        st.Value = st.Value.Replace("[AlunoRGData]", dados.AlunoRGData.ToString());
                        st.Value = st.Value.Replace("[AlunoNasc]", dados.AlunoNasc.ToString("dd/MM/yyyy"));
                        st.Value = st.Value.Replace("[AlunoRGUF]", dados.AlunoRGUF);
                        st.Value = st.Value.Replace("[AlunoCertidaoTipo]", dados.AlunoCertidaoTipo);
                        st.Value = st.Value.Replace("[AnoLetivo]", dados.AnoLetivo.Trim());
                        st.Value = st.Value.Replace("[CidadeEstado]", dados.CidadeEstado + "-" + dados.EmpresaUF);
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

        // Retorna o mês por extenso
        public static string toExtenso(string mes)
        {
            string mesE = "";
            switch (mes)
            {
                case "1":
                    mesE = "Janeiro";
                    break;
                case "2":
                    mesE =  "Fevereiro";
                    break;
                case "3":
                    mesE = "Março";
                    break;
                case "4":
                    mesE =  "Abril";
                    break;
                case "5":
                    mesE =  "Maio";
                    break;
                case "6":
                    mesE =  "Junho";
                    break;
                case "7":
                    mesE =  "Julho";
                    break;
                case "8":
                    mesE =  "Agosto";
                    break;
                case "9":
                    mesE =  "Setembro";
                    break;
                case "10":
                    mesE =  "Outubro";
                    break;
                case "11":
                    mesE =  "Novembro";
                    break;
                case "12":
                    mesE =  "Desembro";
                    break;
            }

            return mesE;
        }

        // O método toExtenso recebe um valor do tipo decimal
        public static string toExtenso(decimal valor)
        {
            if (valor <= 0 | valor >= 1000000000000000)
                return "Valor não suportado pelo sistema.";
            else
            {
                string strValor = valor.ToString("000000000000000.00");
                string valor_por_extenso = string.Empty;
 
                for (int i = 0; i <= 15; i += 3)
                {
                    valor_por_extenso += escreva_parte(Convert.ToDecimal(strValor.Substring(i, 3)));
                    if (i == 0 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(0, 3)) == 1)
                            valor_por_extenso += " TRILHÃO" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
                            valor_por_extenso += " TRILHÕES" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 3 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(3, 3)) == 1)
                            valor_por_extenso += " BILHÃO" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
                            valor_por_extenso += " BILHÕES" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 6 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
                            valor_por_extenso += " MILHÃO" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1)
                            valor_por_extenso += " MILHÕES" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 9 & valor_por_extenso != string.Empty)
                        if (Convert.ToInt32(strValor.Substring(9, 3)) > 0)
                            valor_por_extenso += " MIL" + ((Convert.ToDecimal(strValor.Substring(12, 3)) > 0) ? " E " : string.Empty);
 
                    if (i == 12)
                    {
                        if (valor_por_extenso.Length > 8)
                            if (valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "BILHÃO" | valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "MILHÃO")
                                valor_por_extenso += " DE";
                            else
                                if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "BILHÕES" | valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "MILHÕES" | valor_por_extenso.Substring(valor_por_extenso.Length - 8, 7) == "TRILHÕES")
                                    valor_por_extenso += " DE";
                                else
                                    if (valor_por_extenso.Substring(valor_por_extenso.Length - 8, 8) == "TRILHÕES")
                                        valor_por_extenso += " DE";
 
                        if (Convert.ToInt64(strValor.Substring(0, 15)) == 1)
                            valor_por_extenso += " REAL";
                        else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1)
                            valor_por_extenso += " REAIS";
 
                        if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valor_por_extenso != string.Empty)
                            valor_por_extenso += " E ";
                    }
 
                    if (i == 15)
                        if (Convert.ToInt32(strValor.Substring(16, 2)) == 1)
                            valor_por_extenso += " CENTAVO";
                        else if (Convert.ToInt32(strValor.Substring(16, 2)) > 1)
                            valor_por_extenso += " CENTAVOS";
                }
                return valor_por_extenso;
            }
        }
 
        static string escreva_parte(decimal valor)
        {
            if (valor <= 0)
                return string.Empty;
            else
            {
                string montagem = string.Empty;
                if (valor > 0 & valor < 1)
                {
                    valor *= 100;
                }
                string strValor = valor.ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));
 
                if (a == 1) montagem += (b + c == 0) ? "CEM" : "CENTO";
                else if (a == 2) montagem += "DUZENTOS";
                else if (a == 3) montagem += "TREZENTOS";
                else if (a == 4) montagem += "QUATROCENTOS";
                else if (a == 5) montagem += "QUINHENTOS";
                else if (a == 6) montagem += "SEISCENTOS";
                else if (a == 7) montagem += "SETECENTOS";
                else if (a == 8) montagem += "OITOCENTOS";
                else if (a == 9) montagem += "NOVECENTOS";
 
                if (b == 1)
                {
                    if (c == 0) montagem += ((a > 0) ? " E " : string.Empty) + "DEZ";
                    else if (c == 1) montagem += ((a > 0) ? " E " : string.Empty) + "ONZE";
                    else if (c == 2) montagem += ((a > 0) ? " E " : string.Empty) + "DOZE";
                    else if (c == 3) montagem += ((a > 0) ? " E " : string.Empty) + "TREZE";
                    else if (c == 4) montagem += ((a > 0) ? " E " : string.Empty) + "QUATORZE";
                    else if (c == 5) montagem += ((a > 0) ? " E " : string.Empty) + "QUINZE";
                    else if (c == 6) montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSEIS";
                    else if (c == 7) montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSETE";
                    else if (c == 8) montagem += ((a > 0) ? " E " : string.Empty) + "DEZOITO";
                    else if (c == 9) montagem += ((a > 0) ? " E " : string.Empty) + "DEZENOVE";
                }
                else if (b == 2) montagem += ((a > 0) ? " E " : string.Empty) + "VINTE";
                else if (b == 3) montagem += ((a > 0) ? " E " : string.Empty) + "TRINTA";
                else if (b == 4) montagem += ((a > 0) ? " E " : string.Empty) + "QUARENTA";
                else if (b == 5) montagem += ((a > 0) ? " E " : string.Empty) + "CINQUENTA";
                else if (b == 6) montagem += ((a > 0) ? " E " : string.Empty) + "SESSENTA";
                else if (b == 7) montagem += ((a > 0) ? " E " : string.Empty) + "SETENTA";
                else if (b == 8) montagem += ((a > 0) ? " E " : string.Empty) + "OITENTA";
                else if (b == 9) montagem += ((a > 0) ? " E " : string.Empty) + "NOVENTA";
 
                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty) montagem += " E ";
 
                if (strValor.Substring(1, 1) != "1")
                    if (c == 1) montagem += "UM";
                    else if (c == 2) montagem += "DOIS";
                    else if (c == 3) montagem += "TRÊS";
                    else if (c == 4) montagem += "QUATRO";
                    else if (c == 5) montagem += "CINCO";
                    else if (c == 6) montagem += "SEIS";
                    else if (c == 7) montagem += "SETE";
                    else if (c == 8) montagem += "OITO";
                    else if (c == 9) montagem += "NOVE";
 
                return montagem;
            }
        }

        public class ContratoDetalhe
        {
            public bool HideLogo { get; set; }
            public string Titulo { get; set; }
            public string SubTitulo { get; set; }
            public int Pagina { get; set; }
            public string Texto { get; set; }
        }
    }
}
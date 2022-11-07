using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.Reports.Helper;
using C2BR.GestorEducacao.Reports.Properties;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Globalization;

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8401_Contrato
{
    public partial class RptContrato : XtraReport
    {
        public RptContrato()
        {
            InitializeComponent();
        }

        public int InitReport(int codEmp, string numContrato)
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

                var res = (from tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros()
                           join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tbs390.TBS174_AGEND_HORAR.ID_AGEND_HORAR equals tbs174.ID_AGEND_HORAR
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                           join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tb07.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP
                           join tb905_R in TB905_BAIRRO.RetornaTodosRegistros() on tb108.CO_BAIRRO equals tb905_R.CO_BAIRRO
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs390.CO_EMP_ATEND equals tb25.CO_EMP
                           join tb905_E in TB905_BAIRRO.RetornaTodosRegistros() on tb25.CO_BAIRRO equals tb905_E.CO_BAIRRO
                           where tbs390.NU_CONTRATO == numContrato
                           select new
                           {
                               NumContrato = tbs390.NU_CONTRATO,
                               IdDocumento = tbs390.TB009_RTF_DOCTOS != null ? tbs390.TB009_RTF_DOCTOS.ID_DOCUM : 0,
                               ValorTotal = tbs390.VL_TOTAL_FATU,
                               ValorDesconto = tbs390.VL_DSCTO_FATU,

                               EmpresaNome = tb25.NO_RAZSOC_EMP,
                               EmpresaCnpj = tb25.CO_CPFCGC_EMP,
                               EmpresaFantasia = tb25.NO_FANTAS_EMP,
                               EmpresaCep = tb25.CO_CEP_EMP,
                               EmpresaEnd = tb25.DE_END_EMP,
                               EmpresaEndNumero = tb25.NU_END_EMP ?? 0,
                               EmpresaBairro = tb905_E.NO_BAIRRO,
                               EmpresaCidade = tb905_E.TB904_CIDADE.NO_CIDADE,
                               EmpresaUF = tb905_E.TB904_CIDADE.CO_UF,

                               DirNome = tb25.TB000_INSTITUICAO.NO_RESPO_CONTR,
                               DirCPF = tb25.TB000_INSTITUICAO.NU_CPF_RESPO_CONTR,
                               InstCidade = tb25.TB000_INSTITUICAO.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                               InstUF = tb25.TB000_INSTITUICAO.CID_CODIGO_UF,

                               RespNome = tb108.NO_RESP,
                               RespEnd = tb108.DE_ENDE_RESP,
                               RespEndNumero = tb108.NU_ENDE_RESP,
                               RespBairro = tb905_R.NO_BAIRRO,
                               RespCidade = tb905_R.TB904_CIDADE.NO_CIDADE,
                               RespUF = tb905_R.TB904_CIDADE.CO_UF,
                               RespCep = tb108.CO_CEP_RESP,
                               RespEmail = tb108.DES_EMAIL_RESP,
                               RespCPF = tb108.NU_CPF_RESP,
                               RespRG = tb108.CO_RG_RESP,
                               RespRGORG = tb108.CO_ORG_RG_RESP,
                               RespDtNasc = tb108.DT_NASC_RESP,
                               RespCoEstaCivil = tb108.CO_ESTADO_CIVIL_RESP,
                               RespRGEST = tb108.CO_ESTA_RG_RESP,//*****
                               RespTelRes = tb108.NU_TELE_RESI_RESP,
                               RespTelCom = tb108.NU_TELE_COME_RESP,
                               RespTelCel = tb108.NU_TELE_CELU_RESP,
                               RespDataRg = tb108.DT_EMIS_RG_RESP,
                               RespProfissao = tb108.NO_FUNCAO_RESP,
                               RespNaturalidade = tb108.DE_NATU_RESP,
                               RespUFNaturalidade = tb108.CO_UF_NATU_RESP,

                               NIRE = tb07.NU_NIRE,
                               AlunoCodigo = tb07.CO_ALU,
                               AlunoNome = tb07.NO_ALU,
                               AlunoNasc = tb07.DT_NASC_ALU,
                               AlunoSexo = tb07.CO_SEXO_ALU,
                               AlunoPai = tb07.NO_PAI_ALU,
                               AlunoMae = tb07.NO_MAE_ALU,
                               AlunoCep = tb07.CO_CEP_ALU,
                               EnderecoAluno = tb07.DE_ENDE_ALU,
                               AlunoEmail = tb07.NO_WEB_ALU,
                               AlunoNaturalidade = tb07.DE_NATU_ALU,
                               AlunoCpf = tb07.NU_CPF_ALU,
                               AlunoRG = tb07.CO_RG_ALU,
                               AlunoBairro = tb07.TB905_BAIRRO != null ? tb07.TB905_BAIRRO.NO_BAIRRO : "*****",
                               AlunoCidade = tb07.TB905_BAIRRO != null ? tb07.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE : "*****",
                               AlunoUF = tb07.CO_ESTA_ALU,
                               AlunoTelFixo = tb07.NU_TELE_COME_ALU,
                               AlunoTelCelu = tb07.NU_TELE_CELU_ALU,
                               AlunoOrgaoRG = tb07.CO_ORG_RG_ALU
                           });

                //var res = VW_CONTR_MATRI.RetornaTodosRegistros().Where(w => w.CodAluCad == codAluCad);
                var dados = res.FirstOrDefault();

                if (dados == null)
                    return -1;
                
                var par = (from tbs47 in TBS47_CTA_RECEB.RetornaTodosRegistros()
                           join tb227 in TB227_DADOS_BOLETO_BANCARIO.RetornaTodosRegistros() on tbs47.TB227_DADOS_BOLETO_BANCARIO.ID_BOLETO equals tb227.ID_BOLETO
                           where tbs47.NU_CONTRATO == dados.NumContrato
                           select new
                           {
                               tbs47.NU_DOC,
                               tbs47.QT_PAR,
                               tbs47.NU_PAR,
                               tbs47.DT_VEN_DOC,
                               tbs47.IC_SIT_DOC,
                               tbs47.DT_REC_DOC,
                               tbs47.VL_DES_DOC,
                               tbs47.VL_PAR_DOC,
                               tbs47.VL_TOT_DOC,
                               tbs47.VL_LIQ_DOC,
                               tbs47.CO_NOS_NUM,
                               CodHistorico = tbs47.TB39_HISTORICO != null ? tbs47.TB39_HISTORICO.CO_HISTORICO : 0,
                               tb227.IDEBANCO_CARTEIRA
                           }).ToList();


                //Retorna informações da empresa logada
                var infoEmp = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                               where tb25.CO_EMP == codEmp
                               select new {tb25.CO_UF_EMP, tb25.FL_NONO_DIGITO_TELEF, tb25.CO_CEP_EMP}).FirstOrDefault();

                // CodHistorico = 31 -> Mensalidade (Matrícula Nova)
                var parcelasContrato = par.Where(w => w.CodHistorico == 31 && w.NU_PAR != 0).OrderBy(o => o.DT_VEN_DOC).ToList();

                var lst = (from tb009 in TB009_RTF_DOCTOS.RetornaTodosRegistros()
                           from tb010 in tb009.TB010_RTF_ARQUIVO.DefaultIfEmpty()
                           where tb009.ID_DOCUM == dados.IdDocumento
                           select new ContratoDetalhe
                           {
                               Pagina = tb010.NU_PAGINA,
                               Titulo = tb009.NM_TITUL_DOCUM,
                               Texto = tb010.AR_DADOS,
                               mostraCabec = tb009.FL_HIDELOGO,
                           }).OrderBy(x => x.Pagina);

                if (lst != null && lst.Where(x => x.Pagina == 1).Any())
                {
                    foreach (var Doc in lst)
                    {
                        //Valida se no cadastro do arquivo RTF, foi escolhido que mostrasse o cabeçalho ou não tratando isto de forma que mostra ou não a informação superior.
                        if (Doc.mostraCabec == "S")
                            Logo.Visible = lbTitulo.Visible = true;
                        else
                            Logo.Visible = lbTitulo.Visible = false;

                        lbTitulo.Text = Doc.Titulo;

                        SerializableString st = new SerializableString(Doc.Texto);

                        NumberFormatInfo nfi = new NumberFormatInfo();

                        nfi.NumberGroupSeparator = ".";
                        nfi.NumberDecimalSeparator = ",";

                        #region valores de contrato
                        if (par != null && par.Count > 0)
                        {
                            //Dados do contrato
                            // Inclui o valor total do contrato na declaração
                            st.Value = st.Value.Replace("[ConTotal]", "R$" + par[0].VL_TOT_DOC.ToString("N2", nfi) + " (" + toExtenso(par[0].VL_TOT_DOC) + ")");
                            // Calcula a quantidade de parcelas menos 1, que é a primeira parcela
                            int qtdParc = par.Where(w => w.NU_PAR != 0).Count();

                            // Inclui a quantidade de parcelas restante na declaração
                            st.Value = st.Value.Replace("[ConQtdParcelas]", qtdParc.ToString());

                            // Inclui o valor das demais parcelas na declaração
                            st.Value = st.Value.Replace("[ConVlParcelas]", "R$" + par[0].VL_PAR_DOC.ToString("N2", nfi) + " (" + toExtenso(par[0].VL_PAR_DOC) + ")");

                            // Pega o mes da primeira parcela
                            string mesIniPar = toExtenso(par.OrderBy(r => r.DT_VEN_DOC).First().DT_VEN_DOC.Month.ToString());
                            st.Value = st.Value.Replace("[ConMesIniPar]", mesIniPar);

                            // Pega o mes da ultima parcela
                            string mesFimPar = toExtenso(par.OrderBy(r => r.DT_VEN_DOC).Last().DT_VEN_DOC.Month.ToString());
                            st.Value = st.Value.Replace("[ConMesFimPar]", mesFimPar);

                            // Pega o ano da ultima parcela
                            string anoFimPar = par.OrderBy(r => r.DT_VEN_DOC).Last().DT_VEN_DOC.Year.ToString();
                            st.Value = st.Value.Replace("[ConAnoFimPar]", anoFimPar);
                        }
                        #endregion

                        #region Tags do RTF

                        // Número do contrato
                        st.Value = st.Value.Replace("[NrContratoMatricula]", dados.NumContrato);

                        //Valores do Contrato
                        //st.Value = st.Value.Replace("[ValorTotalLiquido]", vltottot.ToString());
                        //st.Value = st.Value.Replace("[ValorTotalLiquiExtenso]",toExtenso(vltottot));

                        ///Dados da Unidade
                        st.Value = st.Value.Replace("[EmpresaNome]", dados.EmpresaNome);
                        st.Value = st.Value.Replace("[EmpresaFantasia]", dados.EmpresaFantasia);
                        string enderecoEmp = dados.EmpresaEnd + ", ";
                        enderecoEmp += dados.EmpresaEndNumero != 0 ? dados.EmpresaEndNumero.ToString() : "s/n";
                        enderecoEmp += ", " + dados.EmpresaBairro + ", " + dados.EmpresaCidade;
                        enderecoEmp += "-" + dados.EmpresaUF;
                        st.Value = st.Value.Replace("[EmpresaEndereco]", enderecoEmp);
                        st.Value = st.Value.Replace("[cepEmp]", dados.EmpresaCep);
                        st.Value = st.Value.Replace("[EmpresaCNPJ]", Funcoes.Format(dados.EmpresaCnpj, TipoFormat.CNPJ));

                        //Dados da Instituição
                        st.Value = st.Value.Replace("[UFInstituicao]", dados.InstUF);
                        st.Value = st.Value.Replace("[CidadeInstituicao]", dados.InstCidade);

                        ///Dados Diretoria
                        st.Value = st.Value.Replace("[NomeDirGeral]", dados.DirNome);
                        st.Value = st.Value.Replace("[CargoDirGeral]", "");
                        st.Value = st.Value.Replace("[CPFDiretorGeral]", Funcoes.Format(dados.DirCPF, TipoFormat.CPF));
                        st.Value = st.Value.Replace("[RGNumeroDiretorGeral]", "");
                        st.Value = st.Value.Replace("[RGOrgaoDirGeral]", "");
                        st.Value = st.Value.Replace("[RGEstadoDirGeral]", "");

                        ///Dados do Responsável Financeiro
                        st.Value = st.Value.Replace("[ResponsavelNome]", dados.RespNome);
                        string respEnd = dados.RespEnd + ", ";
                        respEnd += dados.RespEndNumero.HasValue ? dados.RespEndNumero.Value.ToString() : "s/n";
                        respEnd += ", " + dados.RespBairro + ", " + dados.RespCidade;
                        respEnd += "-" + dados.RespUF;
                        st.Value = st.Value.Replace("[RespCEP]", Funcoes.Format(dados.RespCep, TipoFormat.CEP));
                        st.Value = st.Value.Replace("[RespCidade]", dados.RespCidade);
                        st.Value = st.Value.Replace("[RespBairro]", dados.RespBairro);
                        st.Value = st.Value.Replace("[RespUF]", dados.RespUF);
                        st.Value = st.Value.Replace("[ResponsavelEndereco]", respEnd);
                        st.Value = st.Value.Replace("[ResponsavelCPF]", Funcoes.Format(dados.RespCPF, TipoFormat.CPF));
                        st.Value = st.Value.Replace("[ResponsavelRG]", dados.RespRG);
                        st.Value = st.Value.Replace("[RespTelRes]", !String.IsNullOrEmpty(dados.RespTelRes) ? FormataTelefone(dados.RespTelRes) : "****");
                        st.Value = st.Value.Replace("[RespTelCom]", !String.IsNullOrEmpty(dados.RespTelCom) ?  FormataTelefone(dados.RespTelCom) : "****");
                        st.Value = st.Value.Replace("[RespTelCel]", !String.IsNullOrEmpty(dados.RespTelCel) ? (infoEmp.FL_NONO_DIGITO_TELEF != "S" ? FormataTelefone(dados.RespTelCel) : (dados.RespTelCel.Length == 10 ? FormataTelefone(dados.RespTelCel).Insert(5, "9").Replace("-", "").Insert(10, "-") : FormataTelefone(dados.RespTelCel).Replace("-", "").Insert(10, "-"))) : "****");
                        st.Value = st.Value.Replace("[OrgaoResponsavelRG]", dados.RespRGORG);
                        st.Value = st.Value.Replace("[EstadoResponsavelRG]", dados.RespRGEST);
                        st.Value = st.Value.Replace("[DataResponsavelRG]", dados.RespDataRg != null ? dados.RespDataRg.Value.ToString("dd/MM/yyyy") : DateTime.MinValue.ToString("dd/MM/yyyy"));
                        st.Value = st.Value.Replace("[ResponsavelProfissao]", dados.RespProfissao);
                        st.Value = st.Value.Replace("[ResponsavelEmail]", !string.IsNullOrEmpty(dados.RespEmail) ? dados.RespEmail : "****" );
                        st.Value = st.Value.Replace("[OrigemRespCidade]", (!string.IsNullOrEmpty(dados.RespNaturalidade) ? dados.RespNaturalidade : "***"));
                        st.Value = st.Value.Replace("[OrigemRespUF]", (!string.IsNullOrEmpty(dados.RespUFNaturalidade) ? dados.RespUFNaturalidade : "***"));
                        st.Value = st.Value.Replace("[RespDtNasc]", (dados.RespDtNasc.HasValue ? dados.RespDtNasc.Value.ToString("dd/MM/yyyy") : "***"));
                        st.Value = st.Value.Replace("[RespEstaCivil]", Funcoes.RetornaEstadoCivil(dados.RespCoEstaCivil));

                        ///Dados do Aluno
                        st.Value = st.Value.Replace("[NIRE]", dados.NIRE.ToString());
                        st.Value = st.Value.Replace("[AlunoNome]", dados.AlunoNome);
                        st.Value = st.Value.Replace("[AlunoIdade]", Funcoes.FormataDataNascimento(dados.AlunoNasc != null ? dados.AlunoNasc.Value : DateTime.MinValue));
                        st.Value = st.Value.Replace("[AlunoNaturalidade]", !string.IsNullOrEmpty(dados.AlunoNaturalidade) ? dados.AlunoEmail : "****");
                        st.Value = st.Value.Replace("[EnderecoAluno]", dados.EnderecoAluno);
                        st.Value = st.Value.Replace("[AlunoCep]", Funcoes.Format(dados.AlunoCep,TipoFormat.CEP));
                        st.Value = st.Value.Replace("[AlunoBairro]", dados.AlunoBairro);
                        st.Value = st.Value.Replace("[AlunoEmail]", !string.IsNullOrEmpty(dados.AlunoEmail) ? dados.AlunoEmail : "****");
                        st.Value = st.Value.Replace("[AlunoNasc]", dados.AlunoNasc != null ? dados.AlunoNasc.Value.ToString("dd/MM/yyyy") : DateTime.MinValue.ToString("dd/MM/yyyy"));
                        st.Value = st.Value.Replace("[AlunoCPF]", !String.IsNullOrEmpty(dados.AlunoCpf) ? dados.AlunoCpf.Insert(3, ".").Insert(7, ".").Insert(11, "-") : "--");
                        st.Value = st.Value.Replace("[AlunoRG]", dados.AlunoRG != null ? dados.AlunoRG : "--");
                        st.Value = st.Value.Replace("[AlunoCidade]", !String.IsNullOrEmpty(dados.AlunoCidade) ? dados.AlunoCidade : "--");
                        st.Value = st.Value.Replace("[AlunoUF]", !String.IsNullOrEmpty(dados.AlunoUF) ? dados.AlunoUF : "--");
                        st.Value = st.Value.Replace("[AlunoTelFixo]", !String.IsNullOrEmpty(dados.AlunoTelFixo) ? FormataTelefone(dados.AlunoTelFixo) : "--");
                        st.Value = st.Value.Replace("[AlunoTelCel]", !String.IsNullOrEmpty(dados.AlunoTelCelu) ? FormataTelefone(dados.AlunoTelCelu) : "--");
                        st.Value = st.Value.Replace("[orgaoAlunoRG]", (!string.IsNullOrEmpty(dados.AlunoOrgaoRG)) ? dados.AlunoOrgaoRG : "--");
                        
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

                        st.Value = st.Value.Replace("[AlunoSexo]", dados.AlunoSexo);
                        st.Value = st.Value.Replace("[AlunoPai]", dados.AlunoPai);
                        st.Value = st.Value.Replace("[AlunoMae]", dados.AlunoMae);

                        ///Dados do contrato
                        st.Value = st.Value.Replace("[ValorTotal]", dados.ValorTotal.HasValue ? dados.ValorTotal.Value.ToString("N2") : "****");
                        st.Value = st.Value.Replace("[ValorTotalExtenso]", dados.ValorTotal.HasValue ? toExtenso(Decimal.Parse(dados.ValorTotal.ToString())) : "-");
                        decimal ValorLiqTotal = dados.ValorTotal.Value - dados.ValorDesconto.Value;
                        st.Value = st.Value.Replace("[ValorTotalContratoLiq]", ValorLiqTotal.ToString("N2"));
                        st.Value = st.Value.Replace("[ValorTotalContratoLiqExtenso]", toExtenso(ValorLiqTotal));
                        
                        decimal ValorTotalContratoDesc = (dados.ValorDesconto ?? 0);
                        st.Value = st.Value.Replace("[ValorTotalContratoDesc]", ValorTotalContratoDesc.ToString("N2"));
                        st.Value = st.Value.Replace("[ValorTotalContratoDescExtenso]", toExtenso(ValorTotalContratoDesc));
                        
                        
                        //Monta as datas de início e fim das parcelas do Contrato.
                        //DateTime? dataPrimPar = (parcelasContrato.Count > 0 ? parcelasContrato.First().DT_VEN_DOC : (DateTime?)null );
                        //int? qtdPar = (qtdParcelasContrato > 0 ? Convert.ToInt32(qtdParcelasContrato) - 1 : (int?)null);
                        //st.Value = st.Value.Replace("[dataFimParc]", (( qtdPar.HasValue ) && (dataPrimPar.HasValue) ? calculaDatas(dataPrimPar.Value.AddMonths(qtdPar.Value)) : "****"));
                        //st.Value = st.Value.Replace("[dataInicParce]", ( parcelasContrato.Count > 0 ? calculaDatas(parcelasContrato.First().DT_VEN_DOC) : "***"));

                        ///Dados da Parcelas - TB47 Contas a Receber
                        int mes = 0;
                        foreach (var item in par)
                        {
                            mes++;
                            st.Value = st.Value.Replace("[DatMens" + mes.ToString().PadLeft(2, '0') + "]", (item.DT_VEN_DOC != DateTime.MinValue) ? item.DT_VEN_DOC.ToString("dd/MM/yyyy") : "*****");
                            st.Value = st.Value.Replace("[ValMens" + mes.ToString().PadLeft(2, '0') + "]", (item.VL_PAR_DOC > 0) ? item.VL_PAR_DOC.ToString() : "*****");
                            st.Value = st.Value.Replace("[ValMensExtenso]", item.VL_PAR_DOC > 0 ? toExtenso(item.VL_PAR_DOC) : "****");
                            st.Value = st.Value.Replace("[ValDesc" + mes.ToString().PadLeft(2, '0') + "]", (item.VL_DES_DOC > 0) ? item.VL_DES_DOC.ToString() : "*****");

                            if (mes == 1)
                                st.Value = st.Value.Replace("[DataPriVncto]", item.DT_VEN_DOC.ToString("dd/MM/yyyy"));

                            if(mes == par.Count)
                                st.Value = st.Value.Replace("[DataUltVncto]", item.DT_VEN_DOC.ToString("dd/MM/yyyy"));
                        }

                        #region Informações de Boletos


                        //Declara as Variáveis que auxiliarão no preenchimento das tags com caracteres referentes à campos nulos
                        int auxBolAlu = 0;

                        //Verifica se existe algum dado na lista, para preenchimento das tags
                        if (par != null)
                        {
                            //Preenche as tags de boletos com as informações pertinentes.
                            foreach (var lstbol in par)
                            {
                                auxBolAlu++;
                                string aux2Bol = auxBolAlu.ToString().PadLeft(2, '0');
                                st.Value = st.Value.Replace("[NrDocCob" + aux2Bol + "]", lstbol.NU_DOC != null ? lstbol.NU_DOC : "---");
                                st.Value = st.Value.Replace("[NrParCob" + aux2Bol + "]", lstbol.NU_PAR != null ? lstbol.NU_PAR.ToString() : "---");
                                st.Value = st.Value.Replace("[DtParCob" + aux2Bol + "]", lstbol.DT_VEN_DOC != null ? lstbol.DT_VEN_DOC.ToString("dd/MM/yyyy") : "---");
                                st.Value = st.Value.Replace("[VlParCob" + aux2Bol + "]", lstbol.VL_PAR_DOC != null ? lstbol.VL_PAR_DOC.ToString() : "---");
                                st.Value = st.Value.Replace("[NrBcoCob" + aux2Bol + "]", lstbol.IDEBANCO_CARTEIRA != null ? lstbol.IDEBANCO_CARTEIRA.PadLeft(0, '3') : "---");
                                st.Value = st.Value.Replace("[NnParCob" + aux2Bol + "]", lstbol.CO_NOS_NUM != null ? lstbol.CO_NOS_NUM : "---");
                            }
                        }

                        //Alimenta as Tags que não tiverem informações para preechê-las.
                        while (auxBolAlu <= 24)
                        {
                            auxBolAlu++;
                            string auxBol = auxBolAlu.ToString().PadLeft(2, '0');
                            st.Value = st.Value.Replace("[NrDocCobX]".Replace("X", auxBol), "---");
                            st.Value = st.Value.Replace("[NrParCobX]".Replace("X", auxBol), "---");
                            st.Value = st.Value.Replace("[DtParCobX]".Replace("X", auxBol), "---");
                            st.Value = st.Value.Replace("[VlParCobX]".Replace("X", auxBol), "---");
                            st.Value = st.Value.Replace("[NrBcoCobX]".Replace("X", auxBol), "---");
                            st.Value = st.Value.Replace("[NnParCobX]".Replace("X", auxBol), "---");
                        }

                        #endregion

                        ///Dados da Assinatura do Contrato
                        st.Value = st.Value.Replace("[CidadeEstado]", dados.EmpresaCidade + "-" + dados.EmpresaUF);
                        st.Value = st.Value.Replace("[Data]", DateTime.Today.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("pt-BR")));
                        #endregion
                        switch (Doc.Pagina)
                        {
                            case 1:
                                {
                                    richPagina1.Rtf = st.Value;
                                    richPagina1.Visible = true;
                                    break;
                                }
                            case 2:
                                {
                                    richPagina2.Rtf = st.Value;
                                    richPagina2.Visible = true;
                                    break;
                                }
                            case 3:
                                {
                                    richPagina3.Rtf = st.Value;
                                    richPagina3.Visible = true;
                                    break;
                                }
                            case 4:
                                {
                                    richPagina4.Rtf = st.Value;
                                    richPagina4.Visible = true;
                                    break;
                                }
                            case 5:
                                {
                                    richPagina5.Rtf = st.Value;
                                    richPagina5.Visible = true;
                                    break;
                                }
                            case 6:
                                {
                                    richPagina6.Rtf = st.Value;
                                    richPagina6.Visible = true;
                                    break;
                                }
                            case 7:
                                {
                                    richPagina7.Rtf = st.Value;
                                    richPagina7.Visible = true;
                                    break;
                                }
                            case 8:
                                {
                                    richPagina8.Rtf = st.Value;
                                    richPagina8.Visible = true;
                                    break;
                                }
                            case 9:
                                {
                                    richPagina9.Rtf = st.Value;
                                    richPagina9.Visible = true;
                                    break;
                                }
                            case 10:
                                {
                                    richPagina10.Rtf = st.Value;
                                    richPagina10.Visible = true;
                                    break;
                                }
                            case 11:
                                {
                                    richPagina11.Rtf = st.Value;
                                    richPagina11.Visible = true;
                                    break;
                                }
                            case 12:
                                {
                                    richPagina12.Rtf = st.Value;
                                    richPagina12.Visible = true;
                                    break;
                                }
                            default:
                                break;
                        }

                        // XRRichText XRPag = new XRRichText();
                        // ...
                        //  Detail.Container.Add(XRPag);                     
                    }
                }

                return 1;
            }
            catch (Exception e) { return 0; }
        }

        //Calcula as datas.
        public static string calculaDatas(DateTime dataMes)
        {
            int mes = dataMes.Month;
            int ano = dataMes.Year;
            string mesExtenso = "";
            switch (mes)
            {
                case 1:
                    mesExtenso = "Janeiro";
                    break;
                case 2:
                    mesExtenso = "Fevereiro";
                    break;
                case 3:
                    mesExtenso = "Março";
                    break;
                case 4:
                    mesExtenso = "Abril";
                    break;
                case 5:
                    mesExtenso = "Maio";
                    break;
                case 6:
                    mesExtenso = "Junho";
                    break;
                case 7:
                    mesExtenso = "Julho";
                    break;
                case 8:
                    mesExtenso = "Agosto";
                    break;
                case 9:
                    mesExtenso = "Setembro";
                    break;
                case 10:
                    mesExtenso = "Outubro";
                    break;
                case 11:
                    mesExtenso = "Novembro";
                    break;
                case 12:
                    mesExtenso = "Dezembro";
                    break;
            }
            return mesExtenso + " de " + ano;
        }

        //Calcula a Bandeira do Cartão de Crédito da Forma de Pagamento
        public static string calculaBandeiraCartao(string coBand)
        {
            string nomeBandeira = "***";
            switch (coBand)
            {
                case "Vis":
                    nomeBandeira = "Visa";
                    break;

                case "MasCar":
                    nomeBandeira = "Master Card";
                    break;

                case "HipCar":
                    nomeBandeira = "HiperCard";
                    break;

                case "Elo":
                    nomeBandeira = "Elo" ;
                    break;

                case "AmeExp":
                    nomeBandeira = "American Express";
                    break;

                case "BNDES":
                    nomeBandeira = "BNDES";
                    break;

                case "SorCr":
                    nomeBandeira = "SoroCred";
                    break;

                case "DinClub":
                    nomeBandeira = "Diners Club";
                    break;
            }

            return nomeBandeira;
        }

        private void insereTagsCred(string aux2CC, SerializableString st, TBE221_PAGTO_CARTAO at)
        {
            st.Value = st.Value.Replace("[MatrCCTituX]".Replace("X", aux2CC), at.NO_TITUL != null ? at.NO_TITUL : "***");
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
                    mesE = "Fevereiro";
                    break;
                case "3":
                    mesE = "Março";
                    break;
                case "4":
                    mesE = "Abril";
                    break;
                case "5":
                    mesE = "Maio";
                    break;
                case "6":
                    mesE = "Junho";
                    break;
                case "7":
                    mesE = "Julho";
                    break;
                case "8":
                    mesE = "Agosto";
                    break;
                case "9":
                    mesE = "Setembro";
                    break;
                case "10":
                    mesE = "Outubro";
                    break;
                case "11":
                    mesE = "Novembro";
                    break;
                case "12":
                    mesE = "Dezembro";
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
                            valor_por_extenso += " trilhão" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
                            valor_por_extenso += " trilhões" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " e " : string.Empty);
                    }
                    else if (i == 3 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(3, 3)) == 1)
                            valor_por_extenso += " bilhão" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
                            valor_por_extenso += " bilhões" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " e " : string.Empty);
                    }
                    else if (i == 6 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
                            valor_por_extenso += " milhão" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1)
                            valor_por_extenso += " milhões" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " e " : string.Empty);
                    }
                    else if (i == 9 & valor_por_extenso != string.Empty)
                        if (Convert.ToInt32(strValor.Substring(9, 3)) > 0)
                            valor_por_extenso += " mil" + ((Convert.ToDecimal(strValor.Substring(12, 3)) > 0) ? " e " : string.Empty);

                    if (i == 12)
                    {
                        if (valor_por_extenso.Length > 8)
                            if (valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "bilhão" | valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "milhão")
                                valor_por_extenso += " de";
                            else
                                if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "bilhões" | valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "milhões" | valor_por_extenso.Substring(valor_por_extenso.Length - 8, 7) == "trilhões")
                                    valor_por_extenso += " de";
                                else
                                    if (valor_por_extenso.Substring(valor_por_extenso.Length - 8, 8) == "trilhões")
                                        valor_por_extenso += " de";

                        if (Convert.ToInt64(strValor.Substring(0, 15)) == 1)
                            valor_por_extenso += " real";
                        else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1)
                            valor_por_extenso += " reais";

                        if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valor_por_extenso != string.Empty)
                            valor_por_extenso += " e ";
                    }

                    if (i == 15)
                        if (Convert.ToInt32(strValor.Substring(16, 2)) == 1)
                            valor_por_extenso += " centavo";
                        else if (Convert.ToInt32(strValor.Substring(16, 2)) > 1)
                            valor_por_extenso += " centavos";
                }
                return valor_por_extenso;
            }
        }

        public static string FormataTelefone(string telefone) 
        {            
            string retorno = "";

            string ddd = telefone.Substring(0,2);

            bool dddSP = ddd == "11" || ddd == "12" || ddd == "13" || ddd == "14" || ddd == "15" || ddd == "16" ||
                         ddd == "17" || ddd == "18" || ddd == "19";

            bool dddRJ = ddd == "21" || ddd == "22" || ddd == "24";

            bool dddES = ddd == "27" || ddd == "28";

            bool ehCelular = telefone[2] == '9' || telefone[2] == '8' || telefone[2] == '7';

            if ((dddSP || dddRJ || dddES) && ehCelular)
            {
                retorno = telefone.Insert(0, "(").Insert(3, ")").Insert(4, " ").Insert(10, "-");
            }
            else
            {
                retorno = telefone.Insert(0, "(").Insert(3, ")").Insert(4, " ").Insert(9, "-");
            
            }
            return retorno;
        }

        public static string FormataCEP(string cep)
        {
            string retorno = "";

            retorno = cep.Insert(5, "-");

            return retorno;
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

                if (a == 1) montagem += (b + c == 0) ? "cem" : "cento";
                else if (a == 2) montagem += "duzentos";
                else if (a == 3) montagem += "trezentos";
                else if (a == 4) montagem += "quatrocentos";
                else if (a == 5) montagem += "quinhentos";
                else if (a == 6) montagem += "seiscentos";
                else if (a == 7) montagem += "setecentos";
                else if (a == 8) montagem += "oitocentos";
                else if (a == 9) montagem += "novecentos";

                if (b == 1)
                {
                    if (c == 0) montagem += ((a > 0) ? " e " : string.Empty) + "dez";
                    else if (c == 1) montagem += ((a > 0) ? " e " : string.Empty) + "onze";
                    else if (c == 2) montagem += ((a > 0) ? " e " : string.Empty) + "doze";
                    else if (c == 3) montagem += ((a > 0) ? " e " : string.Empty) + "treze";
                    else if (c == 4) montagem += ((a > 0) ? " e " : string.Empty) + "quatorze";
                    else if (c == 5) montagem += ((a > 0) ? " e " : string.Empty) + "quinze";
                    else if (c == 6) montagem += ((a > 0) ? " e " : string.Empty) + "dezesseis";
                    else if (c == 7) montagem += ((a > 0) ? " e " : string.Empty) + "dezessete";
                    else if (c == 8) montagem += ((a > 0) ? " e " : string.Empty) + "dezoito";
                    else if (c == 9) montagem += ((a > 0) ? " e " : string.Empty) + "dezenove";
                }
                else if (b == 2) montagem += ((a > 0) ? " e " : string.Empty) + "vinte";
                else if (b == 3) montagem += ((a > 0) ? " e " : string.Empty) + "trinta";
                else if (b == 4) montagem += ((a > 0) ? " e " : string.Empty) + "quarenta";
                else if (b == 5) montagem += ((a > 0) ? " e " : string.Empty) + "cinquenta";
                else if (b == 6) montagem += ((a > 0) ? " e " : string.Empty) + "sessenta";
                else if (b == 7) montagem += ((a > 0) ? " e " : string.Empty) + "setenta";
                else if (b == 8) montagem += ((a > 0) ? " e " : string.Empty) + "oitenta";
                else if (b == 9) montagem += ((a > 0) ? " e " : string.Empty) + "noventa";

                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty) montagem += " e ";

                if (strValor.Substring(1, 1) != "1")
                    if (c == 1) montagem += "um";
                    else if (c == 2) montagem += "dois";
                    else if (c == 3) montagem += "três";
                    else if (c == 4) montagem += "quatro";
                    else if (c == 5) montagem += "cinco";
                    else if (c == 6) montagem += "seis";
                    else if (c == 7) montagem += "sete";
                    else if (c == 8) montagem += "oito";
                    else if (c == 9) montagem += "nove";

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
            public string mostraCabec { get; set; }
        }
    }
}

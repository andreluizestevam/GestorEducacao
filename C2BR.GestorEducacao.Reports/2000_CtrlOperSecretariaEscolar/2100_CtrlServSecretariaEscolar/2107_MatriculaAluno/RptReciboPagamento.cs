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
using System.Collections.Generic;

namespace C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno
{
    public partial class RptReciboPagamento : C2BR.GestorEducacao.Reports.Base.RptDeclaracao
    {
        public RptReciboPagamento()
        {
            InitializeComponent();
        }

        public int InitReport(int codEmp, string codAluCad, string Valor = "", string DescValor = "", string Motivo = "")
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

                var res = VW_CONTR_MATRI.RetornaTodosRegistros().Where(w => w.CodAluCad == codAluCad);
                var dados = res.FirstOrDefault();

                if (dados == null)
                    return -1;

                if (res.Count() == 0)
                    return -1;

                //Instancia um objeto referente à forma de Pagamento relacionada à matrícula em questão.
                TBE220_MATRCUR_PAGTO tbe220 = TBE220_MATRCUR_PAGTO.RetornaTodosRegistros().Where(w => w.CO_ALU_CAD == codAluCad).FirstOrDefault();

                var par = (from m in ctx.TB47_CTA_RECEB
                           where m.CO_ALU == dados.AlunoCodigo
                           && m.CO_CUR == dados.AlunoCursoCodigo && m.CO_MODU_CUR == dados.AlunoModuloCodigo
                           && m.NU_DOC.Substring(0, 2) == "MN"
                           select new
                           {
                               m.DT_VEN_DOC,
                               m.VR_DES_DOC,
                               m.VR_PAR_DOC,
                               m.VL_DES_BOLSA_ALUNO

                           }).ToList();

                var lst = (from tb009 in ctx.TB009_RTF_DOCTOS
                           from tb010 in tb009.TB010_RTF_ARQUIVO.DefaultIfEmpty()
                           where tb009.TP_DOCUM == "RE" && tb009.CO_SIGLA_DOCUM == "RXXX001REMEN" //&& tb009.CO_SITUS_DOCUM == "A" 
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
                        SerializableString st = new SerializableString(Doc.Texto);

                        lblTitulo.Text = Doc.Titulo;

                        //Número do contrato
                        st.Value = st.Value.Replace("[NrContratoMatricula]", dados.NrContrMatricur != null ? dados.NrContrMatricur : "*****");

                        //Dados da matrícula

                        st.Value = st.Value.Replace("[Serie]", !string.IsNullOrEmpty(dados.AlunoCurso) ? dados.AlunoCurso : "*****");
                        st.Value = st.Value.Replace("[Modalidade]", !string.IsNullOrEmpty(dados.AlunoModulo) ? dados.AlunoModulo : "*****");
                        st.Value = st.Value.Replace("[AnoLetivo]", !string.IsNullOrEmpty(dados.AnoLetivo) ? dados.AnoLetivo : "*****");

                        //Dados da Empresa
                        st.Value = st.Value.Replace("[EmpresaNome]", dados.EmpresaNome);
                        st.Value = st.Value.Replace("[EmpresaFantasia]", dados.EmpresaFantasia);

                        string enderecoEmp = dados.EmpresaEnd + ", ";
                        enderecoEmp += dados.EmpresaEndNumero.HasValue ? dados.EmpresaEndNumero.Value.ToString() : "s/n";
                        enderecoEmp += ", " + dados.EmpresaBairro + ", " + dados.EmpresaCidade;
                        enderecoEmp += "-" + dados.EmpresaUF;
                        st.Value = st.Value.Replace("[EmpresaEndereco]", enderecoEmp);
                        st.Value = st.Value.Replace("[EmpresaBairro]", dados.EmpresaBairro);
                        st.Value = st.Value.Replace("[Data]", DateTime.Today.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("pt-BR")));
                        st.Value = st.Value.Replace("[DataAtual]", DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("pt-BR")));
                        st.Value = st.Value.Replace("[EmpresaCNPJ]", Funcoes.Format(dados.EmpresaCnpj, TipoFormat.CNPJ));

                        // Dados Diretoria
                        st.Value = st.Value.Replace("[NomeDirGeral]", dados.DirNome);
                        st.Value = st.Value.Replace("[CargoDirGeral]", "");
                        st.Value = st.Value.Replace("[CPFDiretorGeral]", Funcoes.Format(dados.DirCPF, TipoFormat.CPF));
                        st.Value = st.Value.Replace("[RGNumeroDiretorGeral]", "");
                        st.Value = st.Value.Replace("[RGOrgaoDirGeral]", "");
                        st.Value = st.Value.Replace("[RGEstadoDirGeral]", "");

                        // Dados do Responsável
                        st.Value = st.Value.Replace("[ResponsavelNome]", !string.IsNullOrEmpty(dados.RespNome) ? dados.RespNome : "*****");
                        st.Value = st.Value.Replace("[ResponsavelCPF]", !string.IsNullOrEmpty(dados.RespCPF) ? Funcoes.Format(dados.RespCPF, TipoFormat.CPF) : "*****");
                        st.Value = st.Value.Replace("[ResponsavelRG]", dados.RespRG);
                        st.Value = st.Value.Replace("[OrgaoResponsavelRG]", dados.RespRGORG);
                        st.Value = st.Value.Replace("[EstadoResponsavelRG]", dados.RespRGEST);
                        st.Value = st.Value.Replace("[RespCEP]", !string.IsNullOrEmpty(dados.RespCep) ? Funcoes.Format(dados.RespCep, TipoFormat.CEP) : "*****");

                        string respEnd = dados.RespEnd + ", ";
                        respEnd += dados.RespEndNumero.HasValue ? dados.RespEndNumero.Value.ToString() : "s/n";
                        respEnd += ", " + dados.RespBairro + ", " + dados.RespCidade;
                        respEnd += "-" + dados.RespUF;
                        st.Value = st.Value.Replace("[ResponsavelEndereco]", !string.IsNullOrEmpty(respEnd) ? dados.RespEnd : "*****");
                        st.Value = st.Value.Replace("[RespBairro]", !string.IsNullOrEmpty(dados.RespBairro) ? dados.RespBairro : "*****");
                        st.Value = st.Value.Replace("[RespCidade]", !string.IsNullOrEmpty(dados.RespCidade) ? dados.RespCidade : "*****");
                        st.Value = st.Value.Replace("[RespTelCel]", dados.RespTelCel != "" && dados.RespTelCel != null ? FormataTelefone(dados.RespTelCel) : "****");
                        st.Value = st.Value.Replace("[RespTelRes]", dados.RespTelRes != "" && dados.RespTelRes != null ? FormataTelefone(dados.RespTelRes) : "****");
                        st.Value = st.Value.Replace("[ResponsavelEmail]", !string.IsNullOrEmpty(dados.RespEmail) ? dados.RespEmail : "****");


                        //Dados do Aluno
                        st.Value = st.Value.Replace("[AlunoNasc]", dados.AlunoNasc.Value.ToString("dd/MM/yyyy"));
                        st.Value = st.Value.Replace("[NIRE]", dados.NIRE.ToString());
                        st.Value = st.Value.Replace("[AlunoNome]", !string.IsNullOrEmpty(dados.AlunoNome) ? dados.AlunoNome : "*****");
                        st.Value = st.Value.Replace("[AlunoTurma]", dados.AlunoTurma);
                        st.Value = st.Value.Replace("[AlunoSexo]", dados.AlunoSexo);
                        st.Value = st.Value.Replace("[AlunoPai]", dados.AlunoPai);
                        st.Value = st.Value.Replace("[AlunoMae]", dados.AlunoMae);
                        st.Value = st.Value.Replace("[AlunoCPF]", dados.AlunoCpf != null ? dados.AlunoCpf.Insert(3, ".").Insert(7, ".").Insert(11, "-") : "--");
                        st.Value = st.Value.Replace("[AlunoRG]", (!string.IsNullOrEmpty(dados.AlunoRG)) ? dados.AlunoRG : "--");
                        st.Value = st.Value.Replace("[orgaoAlunoRG]", (!string.IsNullOrEmpty(dados.AlunoOrgaoRG)) ? dados.AlunoOrgaoRG : "--");
                        st.Value = st.Value.Replace("[EnderecoAluno]", dados.EnderecoAluno);
                        st.Value = st.Value.Replace("[AlunoCidade]", dados.AlunoCidade != null ? dados.AlunoCidade : "--");
                        st.Value = st.Value.Replace("[AlunoCep]", Funcoes.Format(dados.AlunoCep, TipoFormat.CEP));
                        st.Value = st.Value.Replace("[AlunoBairro]", dados.AlunoBairro);

                        #region Forma de Pagamento

                        //Declara as variáveis para receber os Totais do pagamento.
                        decimal? valTotCartaoDeb = decimal.Parse("0,00");
                        decimal? valTotCartaoCred = decimal.Parse("0,00");
                        decimal? valTotCheque = decimal.Parse("0,00");

                        //Declara as Variáveis que auxiliarão no preenchimento das tags com caracteres referentes à campos nulos
                        int auxCD = 0;
                        int auxCC = 0;
                        int auxCH = 0;

                        //Verifica se o Aluno em Questão possui cadastro de forma de pagamento, caso não tenha ele não passa por esse foreach e vai direto para a função de
                        //baixo.
                        if (tbe220 != null)
                        {
                            int idMatrPgto = tbe220.ID_MATRCUR_PAGTO;
                            //Cria uma lista com os pagamentos em cartão para a matrícula do aluno em questão
                            var listaTbe221 = (from tbe221 in TBE221_PAGTO_CARTAO.RetornaTodosRegistros()
                                               where tbe221.TBE220_MATRCUR_PAGTO.ID_MATRCUR_PAGTO == idMatrPgto
                                               select new
                                               {
                                                   tbe221.ID_PAGTO_CARTAO,
                                                   tbe221.VL_PAGTO,
                                                   tbe221.FL_TIPO_TRANSAC,
                                                   tbe221.NO_TITUL,
                                                   tbe221.CO_BANDE,
                                                   tbe221.CO_NUMER,
                                                   tbe221.DT_VENCI,
                                                   tbe221.CO_BCO,
                                               }).ToList();

                            //Cria uma lista com os pagamentos em cheque para a matrícula do aluno em questão
                            var listaTbe222 = (from tbe222 in TBE222_PAGTO_CHEQUE.RetornaTodosRegistros()
                                               where tbe222.TBE220_MATRCUR_PAGTO.ID_MATRCUR_PAGTO == idMatrPgto
                                               select new
                                               {
                                                   tbe222.NR_CPF,
                                                   tbe222.NO_TITUL,
                                                   tbe222.CO_BCO,
                                                   tbe222.NR_AGENCI,
                                                   tbe222.NR_CONTA,
                                                   tbe222.DT_VENC,
                                                   tbe222.VL_PAGTO,
                                                   tbe222.NR_CHEQUE,
                                               }).ToList();

                            //Calcula os Totais dos pagamentos em cartão de Débito e de Crédito
                            foreach (var li in listaTbe221)
                            {
                                if (li.FL_TIPO_TRANSAC == "D")
                                    valTotCartaoDeb += li.VL_PAGTO != null ? li.VL_PAGTO : decimal.Parse("0,00");

                                if (li.FL_TIPO_TRANSAC == "C")
                                    valTotCartaoCred += li.VL_PAGTO != null ? li.VL_PAGTO : decimal.Parse("0,00");
                            }

                            //Calcula os Totais dos pagamentos em Cheque
                            foreach (var li in listaTbe222)
                            {
                                valTotCheque += li.VL_PAGTO != null ? li.VL_PAGTO : decimal.Parse("0,00");
                            }

                            //Alimenta as Tags do Pagamento em Cartão de Débito e Crédito
                            foreach (var at in listaTbe221)
                            {
                                if (at.FL_TIPO_TRANSAC == "C")
                                {
                                    string band = calculaBandeiraCartao(at.CO_BANDE);

                                    auxCC++;
                                    string aux2CC = auxCC.ToString().PadLeft(2, '0');
                                    st.Value = st.Value.Replace("[MatrCCTitu" + aux2CC + "]", at.NO_TITUL != null ? at.NO_TITUL.ToUpper() : "---");
                                    st.Value = st.Value.Replace("[MatrCCBand" + aux2CC + "]", band);
                                    st.Value = st.Value.Replace("[MatrCCNume" + aux2CC + "]", at.CO_NUMER != null ? at.CO_NUMER : "---");
                                    st.Value = st.Value.Replace("[MatCCV" + aux2CC + "]", at.VL_PAGTO != null ? at.VL_PAGTO.ToString() : "---");
                                    st.Value = st.Value.Replace("[MtCCD" + aux2CC + "]", at.DT_VENCI != null ? at.DT_VENCI : "---");
                                }
                                else if (at.FL_TIPO_TRANSAC == "D")
                                {
                                    auxCD++;
                                    string aux2CD = auxCD.ToString().PadLeft(2, '0');
                                    st.Value = st.Value.Replace("[MatCDTit" + aux2CD + "]", at.NO_TITUL != null ? at.NO_TITUL.ToUpper() : "---");
                                    st.Value = st.Value.Replace("[MtCDB" + aux2CD + "]", at.CO_BCO.HasValue ? at.CO_BCO.Value.ToString() : "---");
                                    st.Value = st.Value.Replace("[MatCDNu" + aux2CD + "]", at.CO_NUMER != null ? at.CO_NUMER.ToString() : "---");
                                    st.Value = st.Value.Replace("[MatCDV" + aux2CD + "]", at.VL_PAGTO != null ? at.VL_PAGTO.ToString() : "---");
                                }
                            }

                            //Alimenta as Tags de pagamento em cheque
                            foreach (var at in listaTbe222)
                            {
                                auxCH++;
                                string tituTrat = (at.NO_TITUL != null ? (at.NO_TITUL.Length >= 25 ? at.NO_TITUL.Substring(0, 25) : at.NO_TITUL) : "");
                                string aux2CH = auxCH.ToString().PadLeft(2, '0');
                                st.Value = st.Value.Replace("[MatCHCPFX]".Replace("X", aux2CH), at.NR_CPF != null ? at.NR_CPF : "-");
                                st.Value = st.Value.Replace("[MatrCHTituX]".Replace("X", aux2CH), at.NO_TITUL != null ? at.NO_TITUL.Length > 25 ? tituTrat + "..." : tituTrat : "---");
                                st.Value = st.Value.Replace("[MtCHBX]".Replace("X", aux2CH), at.CO_BCO.HasValue ? at.CO_BCO.Value.ToString().PadLeft(0, '3') : "---");
                                st.Value = st.Value.Replace("[MtCHAX]".Replace("X", aux2CH), at.NR_AGENCI != null ? at.NR_AGENCI : "---");
                                st.Value = st.Value.Replace("[MtCHCX]".Replace("X", aux2CH), at.NR_CONTA != null ? at.NR_CONTA : "---");
                                st.Value = st.Value.Replace("[MtCHNX]".Replace("X", aux2CH), at.NR_CHEQUE != null ? at.NR_CHEQUE : "---");
                                st.Value = st.Value.Replace("[MtCHVX]".Replace("X", aux2CH), at.VL_PAGTO != null ? at.VL_PAGTO.ToString() : "---");
                                st.Value = st.Value.Replace("[MCDX]".Replace("X", aux2CH), at.DT_VENC.HasValue ? at.DT_VENC.Value.ToString("dd/MM/yy") : "---");
                            }
                        }

                        //Trata as Tags de Informações de Cartão de Crédito, para que não venham vazias
                        while (auxCC <= 3)
                        {
                            auxCC++;
                            string aux2CC = auxCC.ToString().PadLeft(2, '0');
                            st.Value = st.Value.Replace("[MatrCCTituX]".Replace("X", aux2CC), "---");
                            st.Value = st.Value.Replace("[MatrCCBandX]".Replace("X", aux2CC), "---");
                            st.Value = st.Value.Replace("[MatrCCNumeX]".Replace("X", aux2CC), "---");
                            st.Value = st.Value.Replace("[MatCDVX]".Replace("X", aux2CC), "---"); ;
                            st.Value = st.Value.Replace("[MtCCDX]".Replace("X", aux2CC), "---");
                        }

                        //Trata as Tags de Informações de Débito em Conta, para que não venham vazias
                        while (auxCD <= 3)
                        {
                            auxCD++;
                            string aux2CD = auxCD.ToString().PadLeft(2, '0');
                            st.Value = st.Value.Replace("[MatCDTitX]".Replace("X", aux2CD), "---");
                            st.Value = st.Value.Replace("[MtCDBX]".Replace("X", aux2CD), "---");
                            st.Value = st.Value.Replace("[MatCDNuX]".Replace("X", aux2CD), "---");
                            st.Value = st.Value.Replace("[MatCCVX]".Replace("X", aux2CD), "---");
                        }

                        //Alimenta as Tags que não tiverem informações para preechê-las.
                        while (auxCH <= 12)
                        {
                            auxCH++;
                            string aux2CH = auxCH.ToString().PadLeft(2, '0');
                            st.Value = st.Value.Replace("[MatCHCPFX]".Replace("X", aux2CH), "-");
                            st.Value = st.Value.Replace("[MatrCHTituX]".Replace("X", aux2CH), "---");
                            st.Value = st.Value.Replace("[MtCHBX]".Replace("X", aux2CH), "---");
                            st.Value = st.Value.Replace("[MtCHAX]".Replace("X", aux2CH), "---");
                            st.Value = st.Value.Replace("[MtCHCX]".Replace("X", aux2CH), "---");
                            st.Value = st.Value.Replace("[MtCHNX]".Replace("X", aux2CH), "---");
                            st.Value = st.Value.Replace("[MtCHVX]".Replace("X", aux2CH), "---");
                            st.Value = st.Value.Replace("[MCDX]".Replace("X", aux2CH), "---");
                        }

                        //Preenche os Totais da Forma de Pagamento 
                        if (tbe220 != null)
                        {
                            //Dinheiro
                            st.Value = st.Value.Replace("[MatrTotDN]", tbe220.VL_DINHE.HasValue ? tbe220.VL_DINHE.ToString() : "0,00");
                            st.Value = st.Value.Replace("[MatrTotDNExt]", tbe220.VL_DINHE.HasValue ? toExtenso(tbe220.VL_DINHE.Value) : "---");
                            //Depósito Bancário
                            st.Value = st.Value.Replace("[MatrTotDB]", tbe220.VL_DEPOS.HasValue ? tbe220.VL_DEPOS.ToString() : "0,00");
                            st.Value = st.Value.Replace("[MatrTotDBExt]", tbe220.VL_DEPOS.HasValue ? toExtenso(tbe220.VL_DEPOS.Value) : "---");
                            //Débito em Conta Corrente
                            st.Value = st.Value.Replace("[MatTotDCC]", tbe220.VL_DEBIT_CONTA.HasValue ? tbe220.VL_DEBIT_CONTA.ToString() : "0,00");
                            st.Value = st.Value.Replace("[MatrTotDCExt]", tbe220.VL_DEBIT_CONTA.HasValue ? toExtenso(tbe220.VL_DEBIT_CONTA.Value) : "---");
                            //Transferência Eletrônica
                            st.Value = st.Value.Replace("[MatrTotTE]", tbe220.VL_TRANS.HasValue ? tbe220.VL_TRANS.ToString() : "0,00");
                            st.Value = st.Value.Replace("[MatrTotTEExt]", tbe220.VL_TRANS.HasValue ? toExtenso(tbe220.VL_TRANS.Value) : "---");
                            //Boleto Bancário
                            st.Value = st.Value.Replace("[MatrTotBB]", tbe220.VL_OUTRO.HasValue ? tbe220.VL_OUTRO.ToString() : "0,00");
                            st.Value = st.Value.Replace("[MatrTotBBExt]", tbe220.VL_OUTRO.HasValue ? toExtenso(tbe220.VL_OUTRO.Value) : "---");
                            //Cartão de Crédito
                            st.Value = st.Value.Replace("[MatrTotCC]", valTotCartaoCred.ToString());
                            st.Value = st.Value.Replace("[MatrTotCCExt]", (valTotCartaoCred.HasValue ? (valTotCartaoCred.Value != 0 ? toExtenso(valTotCartaoCred.Value) : "---") : "---"));
                            //Cartão de Débito
                            st.Value = st.Value.Replace("[MatrTotCD]", valTotCartaoDeb.ToString());
                            st.Value = st.Value.Replace("[MatrTotCDExt]", (valTotCartaoDeb.HasValue ? (valTotCartaoDeb.Value != 0 ? toExtenso(valTotCartaoDeb.Value) : "---") : "---"));
                            //Cheque
                            st.Value = st.Value.Replace("[MatrTotCH]", valTotCheque.ToString());
                            st.Value = st.Value.Replace("[MatrTotCHExt]", (valTotCheque.HasValue ? (valTotCheque.Value != 0 ? toExtenso(valTotCheque.Value) : "---") : "---"));
                        }
                        else
                        {
                            //Dinheiro
                            st.Value = st.Value.Replace("[MatrTotDN]", "****");
                            st.Value = st.Value.Replace("[MatrTotDNExt]", "***");
                            //Depósito Bancário
                            st.Value = st.Value.Replace("[MatrTotDB]", "****");
                            st.Value = st.Value.Replace("[MatrTotDCExt]", "****");
                            //Débito Conta Corrente
                            st.Value = st.Value.Replace("[MatTotDCC]", "****");
                            st.Value = st.Value.Replace("[MatrTotDBExt]", "****");
                            //Transferência Eletrônica
                            st.Value = st.Value.Replace("[MatrTotTE]", "****");
                            st.Value = st.Value.Replace("[MatrTotTEExt]", "****");
                            //Boleto Bancário
                            st.Value = st.Value.Replace("[MatrTotBB]", "****");
                            st.Value = st.Value.Replace("[MatrTotBBExt]", "****");
                            //Cartão de Crédito
                            st.Value = st.Value.Replace("[MatrTotCC]", "****");
                            st.Value = st.Value.Replace("[MatrTotCCExt]", "****");
                            //Cartão de Débito
                            st.Value = st.Value.Replace("[MatrTotCD]", "****");
                            st.Value = st.Value.Replace("[MatrTotCDExt]", "****");
                            //Cheque
                            st.Value = st.Value.Replace("[MatrTotCH]", "****");
                            st.Value = st.Value.Replace("[MatrTotCHExt]", "****");
                        }


                        #endregion

                        switch (dados.AlunoTurno)
                        {
                            case "M":
                                st.Value = st.Value.Replace("[Turno]", "Matutino");
                                break;
                            case "V":
                                st.Value = st.Value.Replace("[Turno]", "Vespertino");
                                break;
                            case "N":
                                st.Value = st.Value.Replace("[Turno]", "Noturno");
                                break;
                        }

                        st.Value = st.Value.Replace("[ValorTotal]", dados.ValorTotal.HasValue
                            ? dados.ValorTotal.Value.ToString() : "-");
                        st.Value = st.Value.Replace("[ValorParcial]", dados.ValorParcela.HasValue
                            ? dados.ValorParcela.Value.ToString() : "-");
                        st.Value = st.Value.Replace("[QtdParcela]", dados.QtdParcela > 0 ? dados.QtdParcela.ToString() : "**");

                        st.Value = st.Value.Replace("[TipoBolsaConv]", dados.TipoDesconto);
                        st.Value = st.Value.Replace("[NomeBolsaConv]", dados.NomeDesconto);
                        st.Value = st.Value.Replace("[ValorBolsaConv]", (dados.ValorDesconto > 0) ? dados.ValorDesconto.ToString() : "*****");
                        st.Value = st.Value.Replace("[DtVencDescIni]", (dados.dtVencDescIni != null) ? dados.dtVencDescIni.ToString() : "*****");
                        st.Value = st.Value.Replace("[DtVencDescFim]", (dados.dtVencDescFim != null) ? dados.dtVencDescFim.ToString() : "*****");
                        st.Value = st.Value.Replace("[QtdDescParcela]", dados.QtdDescParcela > 0 ? dados.QtdDescParcela.ToString() : "**");

                        st.Value = st.Value.Replace("[Dia5]", dados.DiaVncto == 5 ? "X" : "");
                        st.Value = st.Value.Replace("[Dia7]", dados.DiaVncto == 7 ? "X" : "");
                        st.Value = st.Value.Replace("[Dia25]", dados.DiaVncto == 25 ? "X" : "");
                        st.Value = st.Value.Replace("[Dia30]", dados.DiaVncto == 30 ? "X" : "");
                        st.Value = st.Value.Replace("[DiaVncto]", dados.DiaVncto.ToString());



                        st.Value = st.Value.Replace("[CidadeEstado]", dados.EmpresaCidade + "-" + dados.EmpresaUF);
                        st.Value = st.Value.Replace("[DataAtual]", DateTime.Today.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("pt-BR")));

                        st.Value = st.Value.Replace("[Valor]", Valor);
                        st.Value = st.Value.Replace("[DescValor]", DescValor);
                        st.Value = st.Value.Replace("[Motivo]", Motivo);
                        st.Value = st.Value.Replace("[ValorExtenso]", toExtenso(decimal.Parse(Valor)).ToUpper());



                        int mes = 0;
                        foreach (var item in par)
                        {
                            mes++;
                            st.Value = st.Value.Replace("[DatMens" + mes.ToString().PadLeft(2, '0') + "]", (item.DT_VEN_DOC != DateTime.MinValue) ? item.DT_VEN_DOC.ToString("dd/MM/yyyy") : "*****");
                            st.Value = st.Value.Replace("[ValMens" + mes.ToString().PadLeft(2, '0') + "]", (item.VR_PAR_DOC > 0) ? item.VR_PAR_DOC.ToString() : "*****");
                            st.Value = st.Value.Replace("[ValBols" + mes.ToString().PadLeft(2, '0') + "]", (item.VL_DES_BOLSA_ALUNO > 0) ? item.VL_DES_BOLSA_ALUNO.ToString() : "*****");
                            st.Value = st.Value.Replace("[ValDesc" + mes.ToString().PadLeft(2, '0') + "]", (item.VR_DES_DOC > 0) ? item.VR_DES_DOC.ToString() : "*****");
                        }

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
                    nomeBandeira = "Elo";
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
                            valor_por_extenso += "  trilhão" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " e " : string.Empty);
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

        public static string FormataTelefone(string telefone)
        {
            string retorno = "";

            string ddd = telefone.Substring(0, 2);

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

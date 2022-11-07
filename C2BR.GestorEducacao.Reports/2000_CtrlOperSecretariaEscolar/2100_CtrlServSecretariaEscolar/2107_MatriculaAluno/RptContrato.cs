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

namespace C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno
{
    public partial class RptContrato : XtraReport
    {
        public RptContrato()
        {
            InitializeComponent();
        }

        public int InitReport(int codEmp, string codAluCad, string tpContrato, string ano)
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

                var res2 = from m in ctx.TB08_MATRCUR
                          join t in ctx.TB129_CADTURMAS on m.CO_TUR equals t.CO_TUR
                          join cur in ctx.TB01_CURSO on m.CO_CUR equals cur.CO_CUR
                          join uc in ctx.TB25_EMPRESA on t.CO_EMP_UNID_CONT equals uc.CO_EMP
                          //join gh in ctx.TB05_GRD_HORAR on m.CO_TUR equals gh.CO_TUR
                          //join mat in ctx.TB02_MATERIA on gh.CO_MAT equals mat.CO_MAT
                          //join cMat in ctx.TB107_CADMATERIAS on mat.ID_MATERIA equals cMat.ID_MATERIA

                          join bEmp in ctx.TB905_BAIRRO on uc.CO_BAIRRO equals bEmp.CO_BAIRRO
                          join cidEmp in ctx.TB904_CIDADE on bEmp.CO_CIDADE equals cidEmp.CO_CIDADE
                          join bResp in ctx.TB905_BAIRRO on m.TB07_ALUNO.TB108_RESPONSAVEL.CO_BAIRRO equals bResp.CO_BAIRRO
                          join cidResp in ctx.TB904_CIDADE on bEmp.CO_CIDADE equals cidResp.CO_CIDADE
                          where m.CO_ALU_CAD == codAluCad
                          select new
                          {
                              EmpresaNome = uc.NO_RAZSOC_EMP,
                              EmpresaCnpj = uc.CO_CPFCGC_EMP,
                              EmpresaFantasia = uc.NO_FANTAS_EMP,
                              EmpresaCep = uc.CO_CEP_EMP,
                              EmpresaEnd = uc.DE_END_EMP,
                              EmpresaEndNumero = uc.NU_END_EMP,
                              EmpresaBairro = bEmp.NO_BAIRRO,
                              EmpresaCidade = cidEmp.NO_CIDADE,
                              EmpresaUF = cidEmp.CO_UF,
                              EmpresaTelGeral = uc.cablinha4,
                              EmpWebSite = uc.NO_WEB_EMP,

                              DirNome = uc.TB000_INSTITUICAO.NO_RESPO_CONTR,
                              DirCPF = uc.TB000_INSTITUICAO.NU_CPF_RESPO_CONTR,
                              //  DirRG = uc.TB000_INSTITUICAO.nu_rg_RESPO_CONTR,

                              RespNome = m.TB07_ALUNO.TB108_RESPONSAVEL.NO_RESP,
                              RespEnd = m.TB07_ALUNO.TB108_RESPONSAVEL.DE_ENDE_RESP,
                              RespEndNumero = m.TB07_ALUNO.TB108_RESPONSAVEL.NU_ENDE_RESP,
                              RespBairro = bResp.NO_BAIRRO,
                              RespCidade = cidResp.NO_CIDADE,
                              RespUF = cidResp.CO_UF,
                              RespCep = m.TB07_ALUNO.TB108_RESPONSAVEL.CO_CEP_RESP,
                              RespEmail = m.TB07_ALUNO.TB108_RESPONSAVEL.DES_EMAIL_RESP,
                              RespCPF = m.TB07_ALUNO.TB108_RESPONSAVEL.NU_CPF_RESP,
                              RespRG = m.TB07_ALUNO.TB108_RESPONSAVEL.CO_RG_RESP,
                              RespRGORG = m.TB07_ALUNO.TB108_RESPONSAVEL.CO_ORG_RG_RESP,
                              RespRGEST = m.TB07_ALUNO.TB108_RESPONSAVEL.CO_ESTA_RG_RESP != null ? m.TB07_ALUNO.TB108_RESPONSAVEL.CO_ESTA_RG_RESP : "*****",
                              RespTelRes = m.TB07_ALUNO.TB108_RESPONSAVEL.NU_TELE_RESI_RESP,
                              RespTelCom = m.TB07_ALUNO.TB108_RESPONSAVEL.NU_TELE_COME_RESP,
                              RespTelCel = m.TB07_ALUNO.TB108_RESPONSAVEL.NU_TELE_CELU_RESP,
                              RespDataRg = m.TB07_ALUNO.TB108_RESPONSAVEL.DT_EMIS_RG_RESP ?? DateTime.MinValue,
                              RespProfissao = m.TB07_ALUNO.TB108_RESPONSAVEL.NO_FUNCAO_RESP,
                              RespNascionalidade = m.TB07_ALUNO.TB108_RESPONSAVEL.CO_NACI_RESP,
                              RespEndTrab = m.TB07_ALUNO.TB108_RESPONSAVEL.DE_ENDE_EMPRE_RESP,
                              RespConjNome = m.TB07_ALUNO.TB108_RESPONSAVEL.NO_CONJUG_RESP,
                              RespConjCPF = m.TB07_ALUNO.TB108_RESPONSAVEL.NU_CPF_CONJUG_RESP,


                              NIRE = m.TB07_ALUNO.NU_NIRE,
                              AlunoCodigo = m.CO_ALU,
                              AlunoNome = m.TB07_ALUNO.NO_ALU,
                              AlunoNasc = m.TB07_ALUNO.DT_NASC_ALU != null ? m.TB07_ALUNO.DT_NASC_ALU.Value : DateTime.MinValue,
                              AlunoBairro = m.TB07_ALUNO.TB905_BAIRRO.NO_BAIRRO,
                              AlunoSexo = m.TB07_ALUNO.CO_SEXO_ALU,
                              AlunoPai = m.TB07_ALUNO.NO_PAI_ALU,
                              AlunoMae = m.TB07_ALUNO.NO_MAE_ALU,
                              AlunoCep = m.TB07_ALUNO.CO_CEP_ALU,
                              EnderecoAluno = m.TB07_ALUNO.DE_ENDE_ALU,
                              AlunoEmail = m.TB07_ALUNO.NO_WEB_ALU,
                              AlunoNaturalidade = m.TB07_ALUNO.DE_NATU_ALU,

                              AlunoTurma = t.NO_TURMA,
                              AlunoCoTurma = t.CO_TUR,
                              AlunoTurno = m.CO_TURN_MAT,
                              AlunoModuloCodigo = m.TB44_MODULO.CO_MODU_CUR,
                              AlunoModulo = m.TB44_MODULO.DE_MODU_CUR,
                              AlunoCursoCodigo = cur.CO_CUR,
                              AlunoCurso = cur.NO_CUR,
                              AlunoContratoCodigo = cur.TB009_RTF_DOCTOS != null ? cur.TB009_RTF_DOCTOS.ID_DOCUM : 0,
                              AlunoContratoCodigoPre = cur.ID_DOCUM_PRE != null ? cur.ID_DOCUM_PRE : 0,

                              AnoLetivo = m.CO_ANO_MES_MAT,
                              DiaVncto = m.NU_DIA_VEN_MOD_MAT,
                              ValorTotal = m.VL_TOT_MODU_MAT,
                              ValorParcela = m.VL_PAR_MOD_MAT,
                              ValorDescontoMod = m.VL_DES_MOD_MAT,
                              ValorDescontoBolsa = m.VL_DES_BOL_MOD_MAT,
                              QtdParcela = m.QT_PAR_MOD_MAT ?? 0,

                              TipoDesconto = m.TB07_ALUNO.TB148_TIPO_BOLSA != null ? m.TB07_ALUNO.TB148_TIPO_BOLSA.NO_TIPO_BOLSA : "*****",
                              NomeDesconto = m.TB07_ALUNO.TB148_TIPO_BOLSA != null ? m.TB07_ALUNO.TB148_TIPO_BOLSA.TP_GRUPO_BOLSA : "*****",
                              QtdDescParcela = m.QT_PAR_DES_MAT ?? 0,
                              ValorDesconto = m.TB07_ALUNO.NU_VAL_DESBOL != null ? m.TB07_ALUNO.NU_VAL_DESBOL : 0,
                              dtVencDescIni = m.TB07_ALUNO.DT_VENC_BOLSA != null ? m.TB07_ALUNO.DT_VENC_BOLSA : null,
                              dtVencDescFim = m.TB07_ALUNO.DT_VENC_BOLSAF != null ? m.TB07_ALUNO.DT_VENC_BOLSAF : null,

                              qtParcelas = m.QT_PAR_MOD_MAT,
                              txMatricula = m.VL_TAXA_MATRIC
                          };

                #endregion

                var res = VW_CONTR_MATRI.RetornaTodosRegistros().Where(w => w.CodAluCad == codAluCad);
                var dados = res.FirstOrDefault();
                var dados2 = res2.FirstOrDefault();

                TB08_MATRCUR tb08 = TB08_MATRCUR.RetornaTodosRegistros().Where(w => w.CO_ALU_CAD == codAluCad).FirstOrDefault();

                #region Coleta as Informações sobre a Forma de Pagamento

                //Instancia um objeto referente à forma de Pagamento relacionada à matrícula em questão.
                TBE220_MATRCUR_PAGTO tbe220 = TBE220_MATRCUR_PAGTO.RetornaTodosRegistros().Where(w => w.CO_ALU_CAD == codAluCad).FirstOrDefault();
                
                #endregion

                if (dados == null)
                    return -1;
                /// Retorna os valores das mensalidades 
                var par = (from m in ctx.TB47_CTA_RECEB
                           where m.CO_ALU == dados.AlunoCodigo
                           && m.CO_CUR == dados.AlunoCursoCodigo
                           && m.CO_MODU_CUR == dados.AlunoModuloCodigo
                           && m.NU_DOC.Substring(0, 2) == "MN"
                           && m.CO_ANO_MES_MAT == ano
                           select new
                           {
                               m.NU_PAR,
                               m.DT_VEN_DOC,
                               m.IC_SIT_DOC,
                               m.DT_REC_DOC,
                               m.VR_DES_DOC,
                               m.VR_PAR_DOC,
                               m.VL_DES_BOLSA_ALUNO,
                               m.VR_TOT_DOC,
                               CodHistorico = m.TB39_HISTORICO.CO_HISTORICO
                           }).ToList();


                /// Retorna os valores das mensalidades 
                var parMU = (from m in ctx.TB47_CTA_RECEB
                           where m.CO_ALU == dados.AlunoCodigo
                           && m.CO_CUR == dados.AlunoCursoCodigo
                           && m.CO_MODU_CUR == dados.AlunoModuloCodigo
                           && m.NU_DOC.Substring(0, 2) == "MU"
                           && m.CO_ANO_MES_MAT == ano
                           select new
                           {
                               m.NU_PAR,
                               m.DT_VEN_DOC,
                               m.IC_SIT_DOC,
                               m.DT_REC_DOC,
                               m.VR_DES_DOC,
                               m.VR_PAR_DOC,
                               m.VL_DES_BOLSA_ALUNO,
                               m.VR_TOT_DOC,
                               CodHistorico = m.TB39_HISTORICO.CO_HISTORICO
                           }).ToList();

                //Retorna informações da empresa logada
                var infoEmp = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                               where tb25.CO_EMP == codEmp
                               select new {tb25.CO_UF_EMP, tb25.FL_NONO_DIGITO_TELEF, tb25.CO_CEP_EMP}).FirstOrDefault();

                // CodHistorico = 31 -> Mensalidade (Matrícula Nova)
                var parcelasContrato = par.Where(w => w.CodHistorico == 31 && w.NU_PAR != 0).OrderBy(o => o.DT_VEN_DOC).ToList();

                // Retorna os valores de material didático
                var mD = (from m in ctx.TB47_CTA_RECEB
                          where m.CO_ALU == dados.AlunoCodigo
                              && m.CO_TUR == dados.AlunoCoTurma
                              && m.IC_SIT_DOC == "Q"
                              && m.NU_DOC.Substring(0, 2) == "SM"
                              && m.CO_ANO_MES_MAT == ano
                          select new
                          {
                              m.DT_VEN_DOC,
                              m.VR_DES_DOC,
                              m.VR_PAR_DOC,
                              m.VL_DES_BOLSA_ALUNO,
                              m.VR_TOT_DOC
                          }).ToList();

                //Retorna os valores da grade horária
                //var gradeHoraria = (from gh in ctx.TB05_GRD_HORAR
                //                    join mat in ctx.TB02_MATERIA on gh.CO_MAT equals mat.CO_MAT
                //                    join cMat in ctx.TB107_CADMATERIAS on mat.ID_MATERIA equals cMat.ID_MATERIA
                //                    where gh.CO_TUR == dados.AlunoCoTurma
                //                    select new
                //                    {
                //                        CodigoMateria = mat.CO_MAT,
                //                        SiglaMateria = cMat.NO_SIGLA_MATERIA,
                //                        NomeMateria = cMat.NO_MATERIA,
                //                        CargaHorariaMateria = mat.QT_CARG_HORA_MAT
                //                    }).Distinct().OrderBy(o => o.NomeMateria).ToList();


                //Verifica as matérias na grade do Aluno.
                var gradeHoraria = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                    join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb48.CO_MAT equals tb02.CO_MAT
                                    join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                    join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb48.CO_CUR equals tb01.CO_CUR
                                    where tb48.CO_ANO_MES_MAT == dados.AnoLetivo
                                    &&  tb48.CO_ALU == dados.AlunoCodigo
                                    select new
                                    {
                                        CodigoMateria = tb02.CO_MAT,
                                        SiglaMateria = tb107.NO_SIGLA_MATERIA,
                                        NomeMateria = tb107.NO_MATERIA,
                                        cargaHrMat = tb02.QT_CARG_HORA_MAT,
                                        cursoMateria = tb01.CO_SIGL_CUR,
                                    }).Distinct().OrderBy(o => o.cursoMateria).ThenBy(w => w.NomeMateria).ToList();

                int valida = 0;
                var lst = (from tb009 in ctx.TB009_RTF_DOCTOS
                           from tb010 in tb009.TB010_RTF_ARQUIVO.DefaultIfEmpty()
                           where tb009.TP_DOCUM == "CO" && tb009.CO_SIGLA_DOCUM == "CINS001PRSER"
                                 &&(tpContrato == "R" ? tb009.ID_DOCUM == dados.AlunoContratoCodigoPre : tb009.ID_DOCUM == dados.AlunoContratoCodigo)
                           select new ContratoDetalhe
                           {
                               Pagina = tb010.NU_PAGINA,
                               Titulo = tb009.NM_TITUL_DOCUM,
                               Texto = tb010.AR_DADOS,
                               mostraCabec = tb009.FL_HIDELOGO,
                           }).OrderBy(x => x.Pagina);

                if (lst != null && lst.Where(x => x.Pagina == 1).Any())
                {
                    valida = 1;
                    foreach (var Doc in lst)
                    {
                        //Valida se no cadastro do arquivo RTF, foi escolhido que mostrasse o cabeçalho ou não tratando isto de forma que mostra ou não a informação superior.
                        if (Doc.mostraCabec == "S")
                            Logo.Visible = lbTitulo.Visible = true;
                        else
                            Logo.Visible = lbTitulo.Visible = false;

                        lbTitulo.Text = Doc.Titulo.Replace("[AnoLetivo]", dados.AnoLetivo);

                        // string st.Value = Doc.Texto;
                        SerializableString st = new SerializableString(Doc.Texto);

                        NumberFormatInfo nfi = new NumberFormatInfo();

                        nfi.NumberGroupSeparator = ".";
                        nfi.NumberDecimalSeparator = ",";

                        #region valores de contrato
                        if (par != null && par.Count > 0 && mD != null && mD.Count > 0)
                        {
                            //Dados do contrato
                            // Inclui o valor total do contrato na declaração
                            st.Value = st.Value.Replace("[ConTotal]", "R$" + par[0].VR_TOT_DOC.ToString("N2", nfi) + " (" + toExtenso(par[0].VR_TOT_DOC) + ")");
                            // Calcula a quantidade de parcelas menos 1, que é a primeira parcela
                            int qtdParc = par.Where(w => w.NU_PAR != 0).Count();

                            // Inclui a quantidade de parcelas restante na declaração
                            st.Value = st.Value.Replace("[ConQtdParcelas]", qtdParc.ToString());

                            // Inclui o valor das demais parcelas na declaração
                            st.Value = st.Value.Replace("[ConVlParcelas]", "R$" + par[0].VR_PAR_DOC.ToString("N2", nfi) + " (" + toExtenso(par[0].VR_PAR_DOC) + ")");

                            // Pega o mes da primeira parcela
                            string mesIniPar = toExtenso(par.OrderBy(r => r.DT_VEN_DOC).First().DT_VEN_DOC.Month.ToString());
                            st.Value = st.Value.Replace("[ConMesIniPar]", mesIniPar);

                            // Pega o mes da ultima parcela
                            string mesFimPar = toExtenso(par.OrderBy(r => r.DT_VEN_DOC).Last().DT_VEN_DOC.Month.ToString());
                            st.Value = st.Value.Replace("[ConMesFimPar]", mesFimPar);

                            // Pega o ano da ultima parcela
                            string anoFimPar = par.OrderBy(r => r.DT_VEN_DOC).Last().DT_VEN_DOC.Year.ToString();
                            st.Value = st.Value.Replace("[ConAnoFimPar]", anoFimPar);

                            // Verifica se existe valor gasto com material dedático
                            if (mD.Any())
                            {
                                // Inclui o valor gasto com material didático na declaração
                                st.Value = st.Value.Replace("[ConMaterial]", mD.First().VR_TOT_DOC.ToString("N2", nfi) + " (" + toExtenso(mD.First().VR_TOT_DOC) + ")");
                            }
                            else
                            {
                                // Caso não exista valor gasto com material didático retira a tag.
                                st.Value = st.Value.Replace("[ConMaterial]", "");
                            }
                        }
                        #endregion

                        #region Valores de Material Didático

                        if (parMU != null && parMU.Count() > 0)
                        {
                            st.Value = st.Value.Replace("[MatDidValTotal]", parMU[0].VR_TOT_DOC != null ? parMU[0].VR_TOT_DOC.ToString("N2", nfi) : "****");
                            st.Value = st.Value.Replace("[MatDidValTotalExt]", parMU[0].VR_TOT_DOC != null ? toExtenso(parMU[0].VR_TOT_DOC) : "****");

                            decimal totParc = parMU.Sum(s => s.VR_PAR_DOC) - parMU.Sum(s => s.VR_DES_DOC).Value;
                            st.Value = st.Value.Replace("[MatDidValTotParc]", totParc != 0 ? totParc.ToString("N2", nfi) : "****");
                            st.Value = st.Value.Replace("[MatDidValTotParcExt]", totParc != 0 ? toExtenso(totParc) : "****");

                            st.Value = st.Value.Replace("[MatDidQtdParc]", parMU.Count() != 0 ? parMU.Count().ToString() : "****");

                            st.Value = st.Value.Replace("[MatDidValParc]", parMU[0].VR_PAR_DOC != null ? parMU[0].VR_PAR_DOC.ToString("N2", nfi) : "****");
                            st.Value = st.Value.Replace("[MatDidValParcExt]", toExtenso(parMU[0].VR_PAR_DOC));

                            string meses = "";
                            int cMU = 1;
                            foreach (var pmu in parMU)
                            {
                                switch (pmu.DT_VEN_DOC.Month)
                                {
                                    case 1:
                                        meses += "Janeiro";
                                        break;
                                    case 2:
                                        meses += "Fevereiro";
                                        break;
                                    case 3:
                                        meses += "Março";
                                        break;
                                    case 4:
                                        meses += "Abril";
                                        break;
                                    case 5:
                                        meses += "Maio";
                                        break;
                                    case 6:
                                        meses += "Junho";
                                        break;
                                    case 7:
                                        meses += "Julho";
                                        break;
                                    case 8:
                                        meses += "Agosto";
                                        break;
                                    case 9:
                                        meses += "Setembro";
                                        break;
                                    case 10:
                                        meses += "Outubro";
                                        break;
                                    case 11:
                                        meses += "Novembro";
                                        break;
                                    case 12:
                                        meses += "Dezembro";
                                        break;
                                }

                                if (cMU == (parMU.Count() - 1))
                                {
                                    meses += " e ";
                                }
                                else
                                {
                                    if (cMU < parMU.Count())
                                    {
                                        meses += ", ";
                                    }
                                }
                                cMU++;
                            }

                            st.Value = st.Value.Replace("[MatDidMesesParc]", meses != "" ? meses : "****");
                        }
                        else
                        {
                            st.Value = st.Value.Replace("[MatDidValTotal]", "****");
                            st.Value = st.Value.Replace("[MatDidValTotalExt]", "****");

                            st.Value = st.Value.Replace("[MatDidValTotParc]", "****");
                            st.Value = st.Value.Replace("[MatDidValTotParcExt]", "****");

                            st.Value = st.Value.Replace("[MatDidQtdParc]", "****");

                            st.Value = st.Value.Replace("[MatDidValParc]", "****");
                            st.Value = st.Value.Replace("[MatDidValParcExt]", "****");

                            st.Value = st.Value.Replace("[MatDidMesesParc]", "****");
                        }

                        #endregion

                        #region Tags do RTF

                        // Número do contrato
                        st.Value = st.Value.Replace("[NrContratoMatricula]", tb08.NR_CONTR_MATRI != null ? tb08.NR_CONTR_MATRI : "*******");

                        //Valores do Contrato
                        decimal vltotmod = (tb08.VL_TOT_MODU_MAT.HasValue ? tb08.VL_TOT_MODU_MAT.Value : decimal.Parse("0,00"));
                        decimal vldesModMat = (tb08.VL_DES_MOD_MAT.HasValue ? tb08.VL_DES_MOD_MAT.Value : decimal.Parse("0,00"));
                        decimal vldesBolMod = (tb08.VL_DES_BOL_MOD_MAT.HasValue ? tb08.VL_DES_BOL_MOD_MAT.Value : decimal.Parse("0,00"));
                        decimal vltottot = vltotmod - vldesModMat - vldesBolMod;
                        st.Value = st.Value.Replace("[ValorTotalLiquido]", vltottot.ToString());
                        st.Value = st.Value.Replace("[ValorTotalLiquiExtenso]",toExtenso(vltottot));

                        ///Dados da Unidade
                        st.Value = st.Value.Replace("[EmpresaNome]", dados.EmpresaNome);
                        st.Value = st.Value.Replace("[EmpresaFantasia]", dados.EmpresaFantasia);
                        string enderecoEmp = dados.EmpresaEnd + ", ";
                        enderecoEmp += dados.EmpresaEndNumero.HasValue ? dados.EmpresaEndNumero.Value.ToString() : "s/n";
                        enderecoEmp += ", " + dados.EmpresaBairro + ", " + dados.EmpresaCidade;
                        enderecoEmp += "-" + dados.EmpresaUF;
                        st.Value = st.Value.Replace("[EmpresaEndereco]", enderecoEmp);
                        st.Value = st.Value.Replace("[cepEmp]", dados.EmpresaCep);
                        st.Value = st.Value.Replace("[EmpresaCNPJ]", Funcoes.Format(dados.EmpresaCnpj, TipoFormat.CNPJ));
                        st.Value = st.Value.Replace("[EmpTelGeral]", dados2.EmpresaTelGeral);
                        st.Value = st.Value.Replace("[EmpWebSite]", dados2.EmpWebSite);

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
                        st.Value = st.Value.Replace("[RespCEP]", Funcoes.Format(dados2.RespCep, TipoFormat.CEP));
                        st.Value = st.Value.Replace("[RespCidade]", dados.RespCidade);
                        st.Value = st.Value.Replace("[RespBairro]", dados.RespBairro);
                        st.Value = st.Value.Replace("[RespUF]", dados.RespUF);
                        st.Value = st.Value.Replace("[ResponsavelEndereco]", respEnd);
                        st.Value = st.Value.Replace("[ResponsavelCPF]", Funcoes.Format(dados.RespCPF, TipoFormat.CPF));
                        st.Value = st.Value.Replace("[ResponsavelRG]", dados.RespRG);
                        st.Value = st.Value.Replace("[RespTelRes]", dados2.RespTelRes != "" && dados2.RespTelRes != null ?  dados2.RespTelRes : "****");
                        st.Value = st.Value.Replace("[RespTelCom]", dados2.RespTelCom != "" && dados2.RespTelCom != null ?  dados2.RespTelCom : "****");
                        st.Value = st.Value.Replace("[RespTelCel]", dados2.RespTelCel != "" && dados2.RespTelCel != null ? (infoEmp.FL_NONO_DIGITO_TELEF != "S" ? dados2.RespTelCel : (dados2.RespTelCel.Length == 10 ? dados2.RespTelCel.Insert(5, "9").Replace("-", "").Insert(10, "-") : dados2.RespTelCel.Replace("-", "").Insert(10, "-"))) : "****");
                        st.Value = st.Value.Replace("[OrgaoResponsavelRG]", dados2.RespRGORG);
                        st.Value = st.Value.Replace("[EstadoResponsavelRG]", dados.RespRGEST);
                        st.Value = st.Value.Replace("[DataResponsavelRG]", dados.RespDataRg != null ? dados.RespDataRg.Value.ToString("dd/MM/yyyy") : DateTime.MinValue.ToString("dd/MM/yyyy"));
                        st.Value = st.Value.Replace("[ResponsavelProfissao]", dados2.RespProfissao);
                        st.Value = st.Value.Replace("[ResponsavelEmail]", !string.IsNullOrEmpty(dados.RespEmail) ? dados.RespEmail : "****" );
                        st.Value = st.Value.Replace("[OrigemRespCidade]", (!string.IsNullOrEmpty(dados.RespNaturalidade) ? dados.RespNaturalidade : "***"));
                        st.Value = st.Value.Replace("[OrigemRespUF]", (!string.IsNullOrEmpty(dados.RespUFNaturalidade) ? dados.RespUFNaturalidade : "***"));
                        st.Value = st.Value.Replace("[RespDtNasc]", (dados.RespDtNasc.HasValue ? dados.RespDtNasc.Value.ToString("dd/MM/yyyy") : "***"));
                        st.Value = st.Value.Replace("[RespEstaCivil]", Funcoes.RetornaEstadoCivil(dados.RespCoEstaCivil));
                        st.Value = st.Value.Replace("[ResponsavelNacionalidade]", (!string.IsNullOrEmpty(dados2.RespNascionalidade) ? dados2.RespNascionalidade : "***"));
                        st.Value = st.Value.Replace("[RespEndTrab]", (!string.IsNullOrEmpty(dados2.RespEndTrab) ? dados2.RespEndTrab : "****"));
                        st.Value = st.Value.Replace("[RespConjuNome]", (!string.IsNullOrEmpty(dados2.RespConjNome) ? dados2.RespConjNome : "****"));
                        st.Value = st.Value.Replace("[RespConjuCPF]", (!string.IsNullOrEmpty(dados2.RespConjCPF) ? dados2.RespConjCPF : "****"));
                        st.Value = st.Value.Replace("[RespConjuTelRes]", "****");
                        st.Value = st.Value.Replace("[RespConjuTelCel]", "****");
                        st.Value = st.Value.Replace("[RespConjuProf]", "****");
                        st.Value = st.Value.Replace("[RespConjuRG]", "****");
                        st.Value = st.Value.Replace("[RespConjuEmail]", "****");

                        //Variável da Data de Início das Aulas
                        st.Value = st.Value.Replace("[MatrDataInicioTurma]", dados.TurmaDataInicio.HasValue ? dados.TurmaDataInicio.Value.ToString("dd/MM/yyyy") : "***");
                        st.Value = st.Value.Replace("[MatrDataTerminTurma]", dados.turmaDataFinal.HasValue ? dados.turmaDataFinal.Value.ToString("dd/MM/yyyy") : "***");

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
                                    st.Value = st.Value.Replace("[MatrCCTitu" + aux2CC + "]", at.NO_TITUL != null ? at.NO_TITUL : "---");
                                    st.Value = st.Value.Replace("[MatrCCBand" + aux2CC + "]", band);
                                    st.Value = st.Value.Replace("[MatrCCNume" + aux2CC + "]", at.CO_NUMER != null ? at.CO_NUMER : "---");
                                    st.Value = st.Value.Replace("[MatCCV" + aux2CC + "]", at.VL_PAGTO != null ? at.VL_PAGTO.ToString() : "---");
                                    st.Value = st.Value.Replace("[MtCCD" + aux2CC + "]", at.DT_VENCI != null ? at.DT_VENCI : "---");
                                }
                                else if (at.FL_TIPO_TRANSAC == "D")
                                {
                                    auxCD++;
                                    string aux2CD = auxCD.ToString().PadLeft(2, '0');
                                    st.Value = st.Value.Replace("[MatCDTit" + aux2CD + "]", at.NO_TITUL != null ? at.NO_TITUL : "---");
                                    st.Value = st.Value.Replace("[MtCDB" + aux2CD + "]", at.CO_BCO.HasValue ? at.CO_BCO.Value.ToString() : "---");
                                    st.Value = st.Value.Replace("[MatCDNu" + aux2CD + "]", at.CO_NUMER != null ? at.CO_NUMER.ToString() : "---");
                                    st.Value = st.Value.Replace("[MatCDV" + aux2CD + "]", at.VL_PAGTO != null ? at.VL_PAGTO.ToString() : "---");
                                }
                            }

                            //Alimenta as Tags de pagamento em cheque
                            foreach (var at in listaTbe222)
                            {
                                auxCH++;
                                string tituTrat = ( at.NO_TITUL != null ? (at.NO_TITUL.Length >= 25 ? at.NO_TITUL.Substring(0, 25) : at.NO_TITUL) : "");
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
                            st.Value = st.Value.Replace("[MatrTotDN]", tbe220.VL_DINHE.HasValue ? tbe220.VL_DINHE.ToString() : "0,00");
                            st.Value = st.Value.Replace("[MatrTotDB]", tbe220.VL_DEPOS.HasValue ? tbe220.VL_DEPOS.ToString() : "0,00");
                            //st.Value = st.Value.Replace("[MatrTotBB]", tbe220.VL_OUTRO.HasValue ? tbe220.VL_OUTRO.ToString() : "0,00");
                            st.Value = st.Value.Replace("[MatrTotCC]", valTotCartaoCred.ToString());
                            st.Value = st.Value.Replace("[MatrTotCD]", valTotCartaoDeb.ToString());
                            st.Value = st.Value.Replace("[MatrTotCH]", valTotCheque.ToString());
                        }
                        else
                        {
                            st.Value = st.Value.Replace("[MatrTotDN]", "****");
                            //st.Value = st.Value.Replace("[MatrTotBB]", "****");
                            st.Value = st.Value.Replace("[MatrTotCC]", "****");
                            st.Value = st.Value.Replace("[MatrTotCD]", "****");
                            st.Value = st.Value.Replace("[MatrTotCH]", "****");
                        }


                        #endregion

                        ///Dados do Aluno
                        st.Value = st.Value.Replace("[NIRE]", dados.NIRE.ToString());
                        st.Value = st.Value.Replace("[AlunoNome]", dados.AlunoNome);
                        st.Value = st.Value.Replace("[AlunoNaturalidade]", !string.IsNullOrEmpty(dados.AlunoNaturalidade) ? dados.AlunoEmail : "****");
                        st.Value = st.Value.Replace("[EnderecoAluno]", dados.EnderecoAluno);
                        st.Value = st.Value.Replace("[AlunoCep]", Funcoes.Format(dados.AlunoCep,TipoFormat.CEP));
                        st.Value = st.Value.Replace("[AlunoBairro]", dados.AlunoBairro);
                        st.Value = st.Value.Replace("[AlunoEmail]", !string.IsNullOrEmpty(dados.AlunoEmail) ? dados.AlunoEmail : "****");
                        st.Value = st.Value.Replace("[AlunoTurma]", dados.AlunoTurma);
                        st.Value = st.Value.Replace("[AlunoNasc]", dados.AlunoNasc != null ? dados.AlunoNasc.Value.ToString("dd/MM/yyyy") : DateTime.MinValue.ToString("dd/MM/yyyy"));
                        st.Value = st.Value.Replace("[AlunoCPF]", dados.AlunoCpf != null ? dados.AlunoCpf.Insert(3, ".").Insert(7, ".").Insert(11, "-") : "--");
                        st.Value = st.Value.Replace("[AlunoRG]", dados.AlunoRG != null ? dados.AlunoRG : "--");
                        st.Value = st.Value.Replace("[AlunoCidade]", dados.AlunoCidade != null ? dados.AlunoCidade : "--");
                        st.Value = st.Value.Replace("[AlunoUF]", dados.AlunoUF != null ? dados.AlunoUF : "--");
                        st.Value = st.Value.Replace("[AlunoTelFixo]", dados.AlunoTelFixo != null ? FormataTelefone(dados.AlunoTelFixo) : "--");
                        st.Value = st.Value.Replace("[AlunoTelCel]", dados.AlunoTelCelu != null ? FormataTelefone(dados.AlunoTelCelu) : "--");
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
                        st.Value = st.Value.Replace("[Serie]", dados.AlunoCurso);
                        st.Value = st.Value.Replace("[Modalidade]", dados.AlunoModulo);
                        st.Value = st.Value.Replace("[AnoLetivo]", dados.AnoLetivo);
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
                                st.Value = st.Value.Replace("[MesAtual]", "Dezembro");
                                break;
                        }
                        #endregion

                        st.Value = st.Value.Replace("[AnoAtual]", DateTime.Now.Year.ToString());

                        ///Dados do contrato
                        st.Value = st.Value.Replace("[ValorTotal]", dados.ValorTotal.HasValue ? dados.ValorTotal.Value.ToString("N2") : "****");
                        st.Value = st.Value.Replace("[ValorTotalExtenso]", dados.ValorTotal.HasValue ? toExtenso(Decimal.Parse(dados.ValorTotal.ToString())) : "-");
                        decimal ValorTotalContratoLiq = ((dados.ValorTotal ?? 0) - (dados.ValorDescontoMod ?? 0) - (dados.ValorDescontoBolsa ?? 0));
                        st.Value = st.Value.Replace("[ValorTotalContratoLiq]", ValorTotalContratoLiq.ToString("N2"));
                        st.Value = st.Value.Replace("[ValorTotalContratoLiqExtenso]", toExtenso(ValorTotalContratoLiq));
                        decimal ValorTotalContratoBol = (dados.ValorDescontoBolsa ?? 0);
                        st.Value = st.Value.Replace("[ValorTotalContratoBol]", ValorTotalContratoBol.ToString("N2"));
                        st.Value = st.Value.Replace("[ValorTotalContratoBolExtenso]", toExtenso(ValorTotalContratoBol));
                        decimal PorcentagemDescontoBolsa = (dados.ValorDescontoBolsa > 0 && dados.ValorTotal > 0) ? Decimal.Parse(((dados.ValorDescontoBolsa / dados.ValorTotal) * 100).ToString()) : 0;
                        st.Value = st.Value.Replace("[PorcentoContratoBol]", (PorcentagemDescontoBolsa > 0) ? PorcentagemDescontoBolsa.ToString("N2") : "*****");
                        decimal ValorTotalContratoDesc = (dados.ValorDescontoMod ?? 0);
                        st.Value = st.Value.Replace("[ValorTotalContratoDesc]", ValorTotalContratoDesc.ToString("N2"));
                        st.Value = st.Value.Replace("[ValorTotalContratoDescExtenso]", toExtenso(ValorTotalContratoDesc));
                        
                        ///Dados da Grade horária
                        for (int i = 0; i < 12; i++)
                        {
                            int indice = i + 1;

                            if (gradeHoraria.Count() > i)
                            {
                                //st.Value = st.Value.Replace("[CodigoMateria" + indice + "]", gradeHoraria[i].CodigoMateria.ToString());
                                st.Value = st.Value.Replace("[CodigoMateria" + indice + "]", gradeHoraria[i].SiglaMateria.ToString());
                                st.Value = st.Value.Replace("[NomeMateria" + indice + "]", gradeHoraria[i].NomeMateria.ToString());
                                st.Value = st.Value.Replace("[CargaMateria" + indice + "]", gradeHoraria[i].cargaHrMat.ToString());
                                st.Value = st.Value.Replace("[CodCursoMatr" + indice + "]", gradeHoraria[i].cursoMateria);
                            }
                            else
                            {
                                //st.Value = st.Value.Replace("[CodigoMateria" + indice + "]", "***");
                                st.Value = st.Value.Replace("[CodigoMateria" + indice + "]", "---");
                                st.Value = st.Value.Replace("[NomeMateria" + indice + "]", "---");
                                st.Value = st.Value.Replace("[CargaMateria" + indice + "]", "---");
                                st.Value = st.Value.Replace("[CodCursoMatr" + indice + "]", "---");
                            }
                        }

                        ///Dados da Bolsa
                        st.Value = st.Value.Replace("[TipoBolsaConv]", dados.TipoDesconto);
                        st.Value = st.Value.Replace("[NomeBolsaConv]", dados.NomeDesconto);

                        st.Value = st.Value.Replace("[ValorBolsaConv]", (dados.ValorDesconto > 0) ? dados.ValorDesconto.ToString() : "*****");
                        st.Value = st.Value.Replace("[DtVencDescIni]", (dados.dtVencDescIni != null) ? dados.dtVencDescIni.Value.ToShortDateString() : "*****");
                        st.Value = st.Value.Replace("[DtVencDescFim]", (dados.dtVencDescFim != null) ? dados.dtVencDescFim.Value.ToShortDateString() : "*****");
                        st.Value = st.Value.Replace("[QtdDescParcela]", dados.QtdDescParcela > 0 ? dados.QtdDescParcela.ToString() : "**");

                        ///Dados da Parcela - TB08 Matrícula
                        var qtdParcelasContrato = (parcelasContrato.Count > 0) ? parcelasContrato.Count : (dados.qtParcelas.HasValue ? dados.qtParcelas.Value : 0);
                        st.Value = st.Value.Replace("[QtdParcelas]", dados.qtParcelas.ToString());
                        st.Value = st.Value.Replace("[QtdParcela]", dados.QtdParcela > 0 ? dados.QtdParcela.ToString() : "****");
                        st.Value = st.Value.Replace("[QuantidadeParcela]", qtdParcelasContrato > 0 ? qtdParcelasContrato.ToString() : "****");
                        st.Value = st.Value.Replace("[ValorParcial]", dados.ValorParcela.HasValue ? dados.ValorParcela.Value.ToString() : "****");
                        st.Value = st.Value.Replace("[ValorParcialExtenso]", dados.ValorParcela.HasValue ? toExtenso(Decimal.Parse(dados.ValorParcela.ToString())) : "-");

                        decimal ValorParcelaLiquido = (ValorTotalContratoLiq > 0 && dados.qtParcelas > 0) ? (ValorTotalContratoLiq / qtdParcelasContrato) : 0;
                        st.Value = st.Value.Replace("[ValorParcialLiquido]", ValorParcelaLiquido.ToString("N2"));
                        st.Value = st.Value.Replace("[ValorParcialLiquidoExtenso]", ValorParcelaLiquido > 0 ? toExtenso(ValorParcelaLiquido) : "****");
                        decimal ValorTaxaMatricula = dados.txMatricula != null ? dados.txMatricula.Value : 0;
                        st.Value = st.Value.Replace("[TaxaMatric]", ValorTaxaMatricula.ToString("N2"));
                        st.Value = st.Value.Replace("[TaxaMatricExtenso]", ValorTaxaMatricula > 0 ? toExtenso(ValorTaxaMatricula) : "****");
                        st.Value = st.Value.Replace("[Dia5]", dados.DiaVncto == 5 ? "X" : "");
                        st.Value = st.Value.Replace("[Dia7]", dados.DiaVncto == 7 ? "X" : "");
                        st.Value = st.Value.Replace("[Dia25]", dados.DiaVncto == 25 ? "X" : "");
                        st.Value = st.Value.Replace("[Dia30]", dados.DiaVncto == 30 ? "X" : "");
                        st.Value = st.Value.Replace("[DiaVncto]", dados.DiaVncto.ToString());
                        st.Value = st.Value.Replace("[DiaVncto1]", parcelasContrato.Count > 0 ? parcelasContrato.First().DT_VEN_DOC.ToShortDateString() : "*****");

                        //Monta as datas de início e fim das parcelas do Contrato.
                        DateTime? dataPrimPar = (parcelasContrato.Count > 0 ? parcelasContrato.First().DT_VEN_DOC : (DateTime?)null );
                        int? qtdPar = (qtdParcelasContrato > 0 ? Convert.ToInt32(qtdParcelasContrato) - 1 : (int?)null);
                        st.Value = st.Value.Replace("[dataFimParc]", (( qtdPar.HasValue ) && (dataPrimPar.HasValue) ? calculaDatas(dataPrimPar.Value.AddMonths(qtdPar.Value)) : "****"));
                        st.Value = st.Value.Replace("[dataInicParce]", ( parcelasContrato.Count > 0 ? calculaDatas(parcelasContrato.First().DT_VEN_DOC) : "***"));
                        st.Value = st.Value.Replace("[ValorTxMat]", ( tb08.VL_TAXA_MATRIC.HasValue ? tb08.VL_TAXA_MATRIC.Value.ToString() : "--" ));

                        ///Dados da Parcelas - TB47 Contas a Receber
                        int mes = 0;
                        foreach (var item in par)
                        {
                            mes++;
                            st.Value = st.Value.Replace("[DatMens" + mes.ToString().PadLeft(2, '0') + "]", (item.DT_VEN_DOC != DateTime.MinValue) ? item.DT_VEN_DOC.ToString("dd/MM/yyyy") : "*****");
                            st.Value = st.Value.Replace("[ValMens" + mes.ToString().PadLeft(2, '0') + "]", (item.VR_PAR_DOC > 0) ? item.VR_PAR_DOC.ToString() : "*****");
                            st.Value = st.Value.Replace("[ValMensExtenso]", item.VR_PAR_DOC > 0 ? toExtenso(item.VR_PAR_DOC) : "****");
                            st.Value = st.Value.Replace("[ValBols" + mes.ToString().PadLeft(2, '0') + "]", (item.VL_DES_BOLSA_ALUNO > 0) ? item.VL_DES_BOLSA_ALUNO.ToString() : "*****");
                            st.Value = st.Value.Replace("[ValDesc" + mes.ToString().PadLeft(2, '0') + "]", (item.VR_DES_DOC > 0) ? item.VR_DES_DOC.ToString() : "*****");

                            if (mes == 1)
                                st.Value = st.Value.Replace("[DataPriVncto]", item.DT_VEN_DOC.ToString("dd/MM/yyyy"));

                            if(mes == par.Count)
                                st.Value = st.Value.Replace("[DataUltVncto]", item.DT_VEN_DOC.ToString("dd/MM/yyyy"));
                        }

                        #region Informações de Boletos

                        //Dados dos boletos do aluno em questão.
                        var cntRecbAluno = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                            join tb227 in TB227_DADOS_BOLETO_BANCARIO.RetornaTodosRegistros() on tb47.TB227_DADOS_BOLETO_BANCARIO.ID_BOLETO equals tb227.ID_BOLETO
                                            where tb47.CO_ANO_MES_MAT == ano
                                            && tb47.CO_ALU == dados.AlunoCodigo
                                            && tb47.CO_MODU_CUR == dados.AlunoModuloCodigo
                                            && tb47.CO_CUR == dados.AlunoCursoCodigo
                                            && tb47.CO_TUR == dados.AlunoCoTurma
                                            select new
                                            {
                                                tb47.NU_DOC,
                                                tb47.NU_PAR,
                                                tb47.DT_VEN_DOC,
                                                tb47.VR_PAR_DOC,
                                                tb227.IDEBANCO_CARTEIRA,
                                                tb47.CO_NOS_NUM,
                                            }).ToList();

                        
                        decimal? vlbolet = 0;
                        if (cntRecbAluno.Count > 0)
                        {
                            vlbolet = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                        join tb227 in TB227_DADOS_BOLETO_BANCARIO.RetornaTodosRegistros() on tb47.TB227_DADOS_BOLETO_BANCARIO.ID_BOLETO equals tb227.ID_BOLETO
                                            where tb47.CO_ANO_MES_MAT == ano
                                            && tb47.CO_ALU == dados.AlunoCodigo
                                            && tb47.CO_MODU_CUR == dados.AlunoModuloCodigo
                                            && tb47.CO_CUR == dados.AlunoCursoCodigo
                                            && tb47.CO_TUR == dados.AlunoCoTurma
                                       select tb47).Sum(s => s.VR_PAR_DOC);
                        }
                        else                        
                            vlbolet = 0;
                        
                        st.Value = st.Value.Replace("[MatrTotBB]", vlbolet.ToString());

                        //Declara as Variáveis que auxiliarão no preenchimento das tags com caracteres referentes à campos nulos
                        int auxBolAlu = 0;

                        //Verifica se existe algum dado na lista, para preenchimento das tags
                        if (cntRecbAluno != null)
                        {
                            //Preenche as tags de boletos com as informações pertinentes.
                            foreach (var lstbol in cntRecbAluno)
                            {
                                auxBolAlu++;
                                string aux2Bol = auxBolAlu.ToString().PadLeft(2, '0');
                                st.Value = st.Value.Replace("[NrDocCob" + aux2Bol + "]", lstbol.NU_DOC != null ? lstbol.NU_DOC : "---");
                                st.Value = st.Value.Replace("[NrParCob" + aux2Bol + "]", lstbol.NU_PAR != null ? lstbol.NU_PAR.ToString() : "---");
                                st.Value = st.Value.Replace("[DtParCob" + aux2Bol + "]", lstbol.DT_VEN_DOC != null ? lstbol.DT_VEN_DOC.ToString("dd/MM/yyyy") : "---");
                                st.Value = st.Value.Replace("[VlParCob" + aux2Bol + "]", lstbol.VR_PAR_DOC != null ? lstbol.VR_PAR_DOC.ToString() : "---");
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
                if (valida == 1)
                {
                    return 1;
                }else{
                    return -1;
                }
            }
            catch { return 0; }
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

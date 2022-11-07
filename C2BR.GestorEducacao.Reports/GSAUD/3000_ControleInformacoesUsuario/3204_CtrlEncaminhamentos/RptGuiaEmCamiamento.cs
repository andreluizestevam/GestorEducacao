using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Text.RegularExpressions;
using C2BR.GestorEducacao.Reports.Helper;
using System.Globalization;

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3204_CtrlEncaminhamentos
{
    public partial class RptGuiaEmCamiamento : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptGuiaEmCamiamento()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                        string infos,
                        int coEmp,
                        int coEmcamiamento
                        )
        {


            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                lblParametros.Text = parametros;

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
                #region Query

                var rep = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs195.CO_ALU equals tb07.CO_ALU
                           join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tbs195.CO_RESP equals tb108.CO_RESP
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs195.CO_COL equals tb03.CO_COL
                           join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb108.CO_BAIRRO equals tb905.CO_BAIRRO
                           join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb108.CO_CIDADE equals tb904.CO_CIDADE
                           join tb250 in TB250_OPERA.RetornaTodosRegistros() on tbs195.ID_OPER equals tb250.ID_OPER
                           join tb251 in TB251_PLANO_OPERA.RetornaTodosRegistros() on tbs195.ID_PLAN equals tb251.ID_PLAN
                           join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals tb63.CO_ESPEC

                           where tbs195.ID_ENCAM_MEDIC == coEmcamiamento
                           select new
                           {
                               // Dados Paciente
                               NomePaci = tb07.NO_ALU,
                               sexoPaci = tb07.CO_SEXO_ALU,
                               dtNascPaci = tb07.DT_NASC_ALU,
                               NisPaci = tb07.NU_NIS,
                               NuCpfPaci = tb07.NU_CPF_ALU,
                               NuCartSaudePaci = tb07.NU_CARTAO_SAUDE_ALU,
                               NuRgPaci = tb07.CO_RG_ALU,
                               NuTelFixPaci = tb07.NU_TELE_CONTAT_ALU,
                               NuTelCelPaci = tb07.NU_TELE_CELU_ALU,
                               NuWhtsPaci = tb07.NU_TELE_WHATS_ALU,
                               NoEmailPaci = tb07.NO_WEB_ALU,
                               NoOrigePaci = tb07.CO_ORIGEM_ALU,
                               NoEntinia = tb07.CO_ORIGEM_ALU,
                               // Dados Do Responsavel
                               CpfResponsavel = tb108.NU_CPF_RESP,
                               NomeResponsavel = tb108.NO_RESP,
                               SexoResponsavel = tb108.CO_SEXO_RESP,
                               DtnacResponsavel = tb108.DT_NASC_RESP,
                               GraParenResponsavel = tb108.DE_GRAU_PAREN,
                               TelefoneFixoResponsavel = tb108.NU_TELE_RESI_RESP,
                               TelefoneCelResponsavel = tb108.NU_TELE_CELU_RESP,
                               WhatResponsavel = tb108.NU_TELE_WHATS_RESP,
                               UfResponsavel = tb108.CO_UF_NATU_RESP,
                               CidadeResponsavel = tb904.NO_CIDADE,
                               BairroResponsavel = tb905.NO_BAIRRO,
                               LograResponsavel = tb108.DE_ENDE_RESP,

                               //Informações Gerais

                               //FlPaciMoraCResp = tbs195.fl,
                               //FlRepsFinanceiro = tbs195.FL_PRE_ATEND,
                               //
                               NomeOperadoraSaude = tb250.NOM_OPER,
                               NomePlano = tb251.NOM_PLAN,
                               VencPlano = tbs195.DT_VENC_CART_PLANO,
                               NumeroPlano = tb251.NOM_PLAN,
                               //Informações do Profissional  de saúde

                               NomeProfSaude = tb03.NO_COL,
                               EspecProfSaude = tb63.NO_ESPECIALIDADE,
                               NoEntidadeProf = tb03.NU_ENTID_PROFI,
                               NuEntidadeProf = tb03.NU_ENTID_PROFI,
                               UfEntidadeProf = tb03.CO_UF_ENTID_PROFI,
                               //Informações gerais do direcionamento 
                               DataDirec = tbs195.DT_CADAS_ENCAM,
                               HoraDirec = tbs195.DT_CADAS_ENCAM,

                           }).ToList();

                var dados = rep.FirstOrDefault();

                if (dados == null)
                    return -1;


                #endregion

                var lst = (from tb009 in TB009_RTF_DOCTOS.RetornaTodosRegistros()
                           join tb010 in TB010_RTF_ARQUIVO.RetornaTodosRegistros() on tb009.ID_DOCUM equals tb010.TB009_RTF_DOCTOS.ID_DOCUM
                           where tb009.CO_SIGLA_DOCUM == "DCTGD"
                           select new GuiaConsultas
                           {
                               Pagina = tb010.NU_PAGINA,
                               Titulo = tb009.NM_TITUL_DOCUM,
                               Texto = tb010.AR_DADOS
                           }).OrderBy(x => x.Pagina);

                if (lst == null)
                    return -1;
                if (lst != null && lst.Where(x => x.Pagina == 1).Any())
                {
                    foreach (var Doc in lst)
                    {
                        SerializableString st = new SerializableString(Doc.Texto);

                        //Dados do Paciente
                        st.Value = st.Value.Replace("[nomePaci]", dados.NomePaci);
                        st.Value = st.Value.Replace("[sexoPaci]", (dados.sexoPaci == "M" ? "Mas" : "Fem"));
                        st.Value = st.Value.Replace("[dtNascPaci]", (dados.dtNascPaci.HasValue ? dados.dtNascPaci.Value.ToString("dd/MM/yyyy") : " - "));
                        st.Value = st.Value.Replace("[nisPaci]", dados.NisPaci.ToString().PadLeft(7, '0'));
                        st.Value = st.Value.Replace("[nuCpfPaci]", Funcoes.Format(dados.NuCpfPaci, TipoFormat.CPF));
                        st.Value = st.Value.Replace("[nuCartSaudePaci]", Convert.ToString(dados.NuCartSaudePaci));
                        st.Value = st.Value.Replace("[nuRgPaci]", dados.NuRgPaci);
                        st.Value = st.Value.Replace("[nuTelFixPaci]", dados.NuTelFixPaci);
                        st.Value = st.Value.Replace("[nuTelCelPaci]", dados.NuTelCelPaci);
                        st.Value = st.Value.Replace("[nuWhtsPaci]", dados.NuWhtsPaci);
                        st.Value = st.Value.Replace("[noEmailPaci]", dados.NoEmailPaci);
                        st.Value = st.Value.Replace("[noOrigePaci]", dados.NoOrigePaci);
                        st.Value = st.Value.Replace("[noEtiniaPaci]", dados.NoEntinia);
                        // Dados Do Responsavel
                        st.Value = st.Value.Replace("[nuCpfResp]", dados.CpfResponsavel);
                        st.Value = st.Value.Replace("[noResp]", dados.NomeResponsavel);
                        st.Value = st.Value.Replace("[sexoResp]", dados.SexoResponsavel);
                        st.Value = st.Value.Replace("[dtNascResp]", (dados.DtnacResponsavel.HasValue ? dados.DtnacResponsavel.Value.ToString("dd/MM/yyyy") : " - "));
                        st.Value = st.Value.Replace("[noGraPar]", CarregaGrauParentesco(dados.GraParenResponsavel));
                        st.Value = st.Value.Replace("[nuTelFixoResp]", dados.TelefoneFixoResponsavel);
                        st.Value = st.Value.Replace("[nuTelCelResp]", dados.TelefoneCelResponsavel);
                        st.Value = st.Value.Replace("[nuWhatsResp]", dados.WhatResponsavel);
                        st.Value = st.Value.Replace("[UFResps]", dados.UfResponsavel);
                        st.Value = st.Value.Replace("[CidadeResp]", dados.CidadeResponsavel);
                        st.Value = st.Value.Replace("[BairroResp]", dados.BairroResponsavel);
                        st.Value = st.Value.Replace("[LograResp]", dados.LograResponsavel);

                        //Informações Gerais
                        //st.Value = st.Value.Replace("[flpacMoraResp]", dados.FlPaciMoraCResp);
                        //st.Value = st.Value.Replace("[flRespFinanceiro]", dados.FlRepsFinanceiro);
                        //Informações Plano de saude
                        st.Value = st.Value.Replace("[noOperadora]", dados.NomeOperadoraSaude);
                        st.Value = st.Value.Replace("[noPlanoSaude]", dados.NomePlano);
                        st.Value = st.Value.Replace("[vencPlano]", dados.VencPlano);
                        st.Value = st.Value.Replace("[numerPlano]", dados.NumeroPlano);

                        //
                        st.Value = st.Value.Replace("[noProfiSaude]", dados.NomeProfSaude);
                        st.Value = st.Value.Replace("[EspecProfiSaude]", dados.EspecProfSaude);
                        st.Value = st.Value.Replace("[noEntidProfi]", dados.NoEntidadeProf);
                        st.Value = st.Value.Replace("[nuEntidProfi]", dados.NuEntidadeProf);
                        st.Value = st.Value.Replace("[nfEntidProfi]", dados.UfEntidadeProf);



                        //Informações gerais do direcionamento 
                        st.Value = st.Value.Replace("[DataDirec]", (dados.DataDirec.HasValue ? dados.DataDirec.Value.ToString("dd/MM/yyyy") : " - "));
                        st.Value = st.Value.Replace("[HoraDirec]", (dados.HoraDirec.HasValue ? dados.HoraDirec.Value.ToString("HH:mm") : " - "));



                    }
                }



                return 1;
            }
            catch { return 0; }
        }

        #endregion

        public class GuiaConsultas
        {
            public bool HideLogo { get; set; }
            public string Titulo { get; set; }
            public string SubTitulo { get; set; }
            public int Pagina { get; set; }
            public string Texto { get; set; }
        }

        /// <summary>
        /// Verifica de acordo com o código recebido, qual o grau de parentesco do Responsável
        /// </summary>
        /// <param name="CO_TIPO"></param>
        private string CarregaGrauParentesco(string CO_GRAU)
        {

            string s = "";
            switch (CO_GRAU)
            {
                case "PM":
                    s = "Pai/Mãe";
                    break;
                case "TI":
                    s = "Tio(a)";
                    break;
                case "AV":
                    s = "Avô/Avó";
                    break;
                case "PR":
                    s = "Primo(a)";
                    break;
                case "CN":
                    s = "Cunhado(a)";
                    break;
                case "TU":
                    s = "Tutor(a)";
                    break;
                case "IR":
                    s = "Irmão(ã)";
                    break;
                case "OU":
                    s = "Outros";
                    break;
                default:
                    s = " - ";
                    break;
            }
            return s;
        }
    }
}

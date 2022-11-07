using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8200_CtrlExames
{
    public partial class RptFichaAtend : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptFichaAtend()
        {
            InitializeComponent();
        }

        public int InitReport(
              string NomeFuncionalidade,
              string infos,
              int coEmp,
              int Paciente,
              int idAtend = 0,
              string Observacoes = "",
              string Queixas = "",
              string HDA = "",
              string Hipotese = "",
              string Exame = ""
            )
        {
            try
            {
                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                // Instancia o header do relatorio

                if (NomeFuncionalidade == "")
                    lblTitulo.Text = "-";
                else
                    lblTitulo.Text = NomeFuncionalidade.ToUpper();

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                if (idAtend != 0)
                {
                    var att = (from tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros()
                               where tbs390.ID_ATEND_AGEND == idAtend
                               select new
                               {
                                   tbs390.DE_QXA_PRINC,
                                   tbs390.DE_HDA,
                                   tbs390.DE_EXM_FISIC,
                                   tbs390.DE_HIP_DIAGN,
                                   tbs390.DE_OBSER
                               }).FirstOrDefault();

                    if (att != null)
                    {
                        Queixas = att.DE_QXA_PRINC;
                        HDA = att.DE_HDA;
                        Hipotese = att.DE_HIP_DIAGN;
                        Exame = att.DE_EXM_FISIC;
                        Observacoes = att.DE_OBSER;
                    }
                }

                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros().Where(a => a.CO_ALU == Paciente)
                           select new Prontuario
                           {
                               nomPac = tb07.NO_ALU,
                               nasPac = tb07.DT_NASC_ALU,
                               sexPac = !String.IsNullOrEmpty(tb07.CO_SEXO_ALU) ? tb07.CO_SEXO_ALU == "F" ? "Feminino" : "Masculino" : "-",
                               estCivil = tb07.CO_ESTADO_CIVIL,
                               naciPac = tb07.DE_NACI_ALU,
                               natuPac = tb07.DE_NATU_ALU + " - " + tb07.CO_UF_NATU_ALU,
                               tipSanPac = tb07.CO_TIPO_SANGUE_ALU,
                               defPac = tb07.TBS387_DEFIC.NM_SIGLA_DEFIC,
                               unidPac = tb07.TB25_EMPRESA.NO_FANTAS_EMP,
                               nirePac = tb07.NU_NIRE,
                               nisPac = tb07.NU_NIS.HasValue ? tb07.NU_NIS.Value : 0,
                               mailPac = tb07.NO_WEB_ALU,
                               rgPac = tb07.CO_RG_ALU + " - " + (!String.IsNullOrEmpty(tb07.CO_ORG_RG_ALU) ? tb07.CO_ORG_RG_ALU + "/" + tb07.CO_ESTA_RG_ALU : ""),
                               cpfPac = tb07.NU_CPF_ALU,
                               cartSaude = tb07.NU_CARTAO_SAUDE,
                               cartSus = tb07.NU_CARTAO_SAUDE_ALU,
                               cartVacina = tb07.CO_CART_VACIN,
                               operadora = !String.IsNullOrEmpty(tb07.TB250_OPERA.NM_SIGLA_OPER) ? tb07.TB250_OPERA.NM_SIGLA_OPER : "-",
                               plano = !String.IsNullOrEmpty(tb07.TB251_PLANO_OPERA.NM_SIGLA_PLAN) ? tb07.TB251_PLANO_OPERA.NM_SIGLA_PLAN : "-",
                               numPlano = tb07.NU_PLANO_SAUDE,
                               validPlano = tb07.DT_VENC_PLAN,
                               nomMaePac = tb07.NO_MAE_ALU,
                               nasMaePac = tb07.DT_NASC_MAE,
                               obtMaePac = tb07.FLA_OBITO_MAE,
                               nomPaiPac = tb07.NO_PAI_ALU,
                               nasPaiPac = tb07.DT_NASC_PAI,
                               obtPaiPac = tb07.FLA_OBITO_PAI,
                               nomResPac = tb07.TB108_RESPONSAVEL.NO_RESP,
                               nasResPac = tb07.TB108_RESPONSAVEL.DT_NASC_RESP,
                               cpfResPac = tb07.TB108_RESPONSAVEL.NU_CPF_RESP,
                               endereco = tb07.DE_ENDE_ALU,
                               cidade = tb07.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                               bairro = tb07.TB905_BAIRRO.NO_BAIRRO,
                               estado = tb07.CO_ESTA_ALU,
                               cep = tb07.CO_CEP_ALU,
                               queixas = Queixas,
                               hda = HDA,
                               hipotese = Hipotese,
                               exames = Exame,
                               observacoes = Observacoes
                           }).ToList();
                            
                if (res.Count == 0)
                    return -1;

                this.Parametros = "( Paciente : " + res.FirstOrDefault().nomPac
                                + " Nº Registro : " + res.FirstOrDefault().nirePac
                                + (res.FirstOrDefault().nisPac != 0 ? " Nº NIS : " + res.FirstOrDefault().nisPac : "") + ")";

                //Adiciona ao DataSource do Relatório
                bsReport.Clear();
                foreach (var item in res)
                {
                    bsReport.Add(item);
                }
                return 1;

            }
            catch { return 0; }
        }

        public class Prontuario
        {
            public string queixas { get; set; }

            public string hda { get; set; }

            public string hipotese { get; set; }

            public string exames { get; set; }

            public string observacoes { get; set; }

            public string nomPac { get; set; }

            public string numRap { get; set; }

            public DateTime? nasPac { get; set; }

            public string idadePac 
            {
                get
                {
                    if (nasPac.HasValue)
                        return Funcoes.FormataDataNascimento(nasPac.Value);
                    else
                        return " - ";
                }
            }

            public string sexPac { get; set; }

            public string _estCivil;
            public string estCivil
            {
                get { return Funcoes.RetornaEstadoCivil(this._estCivil); }
                set { this._estCivil = value; }
            }

            public string naciPac { get; set; }

            public string natuPac { get; set; }

            public string tipSanPac { get; set; }

            public string defPac { get; set; }

            public string unidPac { get; set; }

            public int nirePac { get; set; }

            public decimal nisPac { get; set; }

            public string mailPac { get; set; }

            public string rgPac { get; set; }

            public string _cpfPac;
            public string cpfPac
            {
                get { return this._cpfPac.Format(TipoFormat.CPF); }
                set { this._cpfPac = value; }
            }

            public string cartSaude { get; set; }

            public decimal? cartSus { get; set; }

            public string cartVacina { get; set; }

            public string operadora { get; set; }

            public string plano { get; set; }

            public string numPlano { get; set; }

            public DateTime? validPlano { get; set; }

            public string nomMaePac { get; set; }

            public DateTime? nasMaePac { get; set; }

            public string obtMaePac { get; set; }

            public string nomPaiPac { get; set; }

            public DateTime? nasPaiPac { get; set; }

            public string obtPaiPac { get; set; }

            public string nomResPac { get; set; }

            public DateTime? nasResPac { get; set; }

            public string _cpfResPac;
            public string cpfResPac
            {
                get { return this._cpfResPac.Format(TipoFormat.CPF); }
                set { this._cpfResPac = value; }
            }

            public string endereco { get; set; }

            public string cidade { get; set; }

            public string bairro { get; set; }

            public string estado { get; set; }

            public string _cep;
            public string cep
            {
                get { return this._cep.Format(TipoFormat.CEP); }
                set { this._cep = value; }
            }
        }
    }
}

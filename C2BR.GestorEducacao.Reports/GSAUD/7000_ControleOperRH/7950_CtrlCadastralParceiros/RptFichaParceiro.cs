using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;
using System.Data.Objects;

namespace C2BR.GestorEducacao.Reports.GSAUD._7000_ControleOperRH._7950_CtrlCadastralParceiros
{
    public partial class RptFichaParceiro : C2BR.GestorEducacao.Reports.RptRetrato
    {
        #region ctor

        public RptFichaParceiro()
        {
            InitializeComponent();
        }

        #endregion

        #region InitReport

        public int InitReport(int codEmp, int codParc, string infos, string NomeFuncionalidadeCadastrada)
        {
            try
            {
                #region Inicializa o header/Labels

                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;

                // Cria o header a partir do cod da instituicao
                var header = ReportHeader.GetHeaderFromEmpresa(codEmp);
                if (header == null)
                    return -1;

                // Inicializa o header
                base.BaseInit(header);
                //this.celFreq.Text = string.Format(celFreq.Text, anoBase);
                if (NomeFuncionalidadeCadastrada == "")
                {
                    lblTitulo.Text = "EMISSÃO DA FICHA INDIVIDUAL DE INFORMAÇÕES DE PARCEIROS";
                }
                else
                {
                    lblTitulo.Text = NomeFuncionalidadeCadastrada;
                }

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Parceiro

                var lstf = (from tb421 in TB421_PARCEIROS.RetornaTodosRegistros()
                            where tb421.CO_PARCE == codParc
                            select new Parceiro
                            {
                                
                                nomeParce = tb421.DE_RAZSOC_PARCE,
                                apelidoParce = tb421.NO_FANTAS_PARCE,
                                _areaParce = tb421.CO_AREA_PROSP_NEGOC,
                                webParce = tb421.DE_WEB_PARCE,
                                emailParce = tb421.DE_EMAIL_PARCE,
                                telParce1 = tb421.CO_TEL1_PARCE,
                                telParce2 = tb421.CO_TEL2_PARCE,
                                whatsParce = tb421.CO_WATHS_PARCE,
                                _dtCadasParce = tb421.DT_CAD_PARCE,
                                //Imagem = tb421., ADICIONAR NO BANCO COLUNA FOTO
                                skypeParce = tb421.NM_SKYPE_PARCE,
                                _propoParce = tb421.DE_PROPOS_NEGOC,
                                anexoParce = tb421.NO_ANEXO,


                            });

                #endregion
                var func = lstf.FirstOrDefault();

                //Se não encontrou o funcionário
                if (func == null)
                    return -1;

                var end = (from tb421 in TB421_PARCEIROS.RetornaTodosRegistros()
                             where tb421.CO_PARCE == codParc
                             select new Endereco
                                {
                                    Bairro = tb421.TB905_BAIRRO.NO_BAIRRO,
                                    CEP = tb421.CO_CEP_PARCE,
                                    Cidade = tb421.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                                    Complemento = tb421.DE_COMPLE_PARCE,
                                    Estado = tb421.TB74_UF.CODUF,
                                    Logradouro = tb421.DE_END_PARCE,
                                    Numero = tb421.NU_END_PARCE
                                });

                func.endParce = end.FirstOrDefault();

                var doc = (from tb421 in TB421_PARCEIROS.RetornaTodosRegistros()
                             where tb421.CO_PARCE == codParc
                             select new Documentos
                                {
                                    _tipoParce = tb421.TP_PARCE,
                                    ieParce = tb421.CO_INS_EST_PARCE,
                                    imParce = tb421.CO_INS_MUN_PARCE,
                                    cnpjParce = tb421.CO_CPFCGC_PARCE,
                                });

                func.docParce = doc.FirstOrDefault();

                var resp = (from tb421 in TB421_PARCEIROS.RetornaTodosRegistros()
                           where tb421.CO_PARCE == codParc
                            select new InformacoesResponsavel
                                 {
                                     nomeResp = tb421.NM_RESPO_PARCE,
                                     FuncResp = tb421.NM_FUNCAO_RESPO_PARCE,
                                     emailResp = tb421.DE_EMAIL_RESPO_PARCE,
                                     obserResp = tb421.DE_OBSER_RESPO_PARCE,
                                     cpfResp = tb421.CO_CPF_RESPO_PARCE,
                                     telResp = tb421.CO_TELEF_RESPO_PARCE,
                                     whatsResp = tb421.CO_WATHS_RESPO_PARCE,
                                     skypeResp = tb421.NM_SKYPE_RESPO_PARCE
                                 });

                func.inforResp = resp.FirstOrDefault();

                var indic = (from tb421 in TB421_PARCEIROS.RetornaTodosRegistros()
                             join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb421.CO_COL_INDIC_PARCE equals tb03.CO_COL
                             where tb421.CO_PARCE == codParc
                                select new InformacoesIndicacao
                                {
                                    nomeIndic = tb421.NM_INDIC_PARCE,
                                    empreIndic = tb03.TB25_EMPRESA.NO_RAZSOC_EMP,
                                    emailIndic = tb421.DE_EMAIL_INDIC_PARCE,
                                    obserIndic = tb421.DE_OBSER_INDIC_PARCE,
                                    _funciIndic = tb421.FL_FUNCIO_INDIC_PARCE,
                                    _dtIndicIndic = tb421.DT_INDIC_PARCE,
                                    telIndic = tb421.CO_TELEF_INDIC_PARCE,
                                    whatsIndic = tb421.CO_WATHS_INDIC_PARCE,
                                    skypeIndic = tb421.NM_SKYPE_INDIC_PARCE
                                });


                func.inforIndic = indic.FirstOrDefault();

                #region Ocorrencias do Parceiro
                
                var lstOcorr = (from tb422 in TB422_REGIS_OCORR_PARCE.RetornaTodosRegistros()
                                where tb422.TB421_PARCEIROS.CO_PARCE == codParc
                                select new Ocorrencia
                                {
                                    _tipoOcorr = tb422.TP_OCORR,
                                    dtOcorr = tb422.DT_OCORR,
                                    titulOcorr = tb422.NO_OCORR,
                                    descrOcorr = tb422.TX_OCORR,
                                    acaoOcorr = tb422.TX_ACAO_OCORR

                                }).ToList();

                func.ocorrParce = lstOcorr;

                if (func.ocorrParce.Count == 0)
                    this.xrLabelOcorr.Text = " ***Sem Registro";

                #endregion

                bsReport.Clear();
                bsReport.Add(func);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Parceiro do Relatorio

        public class Parceiro
        {
            public Parceiro()
            {
                this.ocorrParce = new List<Ocorrencia>();
            }

            public string nomeParce { get; set; }
            public string apelidoParce { get; set; }
            public string _areaParce { get; set; }
            public string areaParce
            {
                get
                {
                    string a = "";

                    switch (_areaParce)
                    {

                        case "EDU":
                            a = "Educação";
                            break;
                        case "SAU":
                            a = "Saúde";
                            break;
                        case "ENE":
                            a = "Energia";
                            break;
                        case "ESP":
                            a = "Esporte";
                            break;
                        case "SEG":
                            a = "Seguros";
                            break;
                        case "BIA":
                            a = "Bio-Agro";
                            break;
                        case "CON":
                            a = "Consórcios";
                            break;
                        case "COM":
                            a = "Commodites";
                            break;
                        case "INF":
                            a = "Infra-estrutura";
                            break;
                        case "MAM":
                            a = "Meio Ambiente";
                            break;
                        case "COI":
                            a = "Construção e Incorporação";
                            break;
                        case "ASM":
                            a = "Assest Management";
                            break;
                        case "GAI":
                            a = "Gestão de Ativos Imobiliários";
                            break;
                        case "TEC":
                            a = "Tecnologia";
                            break;
                        case "PAG":
                            a = "Meio de Pagamentos";
                            break;
                    }

                    return a;
                }
            }
            public string webParce { get; set; }
            public string emailParce { get; set; }
            public DateTime _dtCadasParce { get; set; }
            public string dtCadasParce
            {
                get
                {
                    DateTime dt = Convert.ToDateTime(this._dtCadasParce);

                    return dt.ToString("dd/MM/yyyy");
                }
            }
            public string tipoPessoa
            {
                get
                {
                    if (docParce._tipoParce == "F")
                    {
                        return "CPF:";
                    }
                    else
                    {
                        return "CNPJ:";
                    }
                }
            }
            public string inscrEstadual
            {
                get
                {
                    if (docParce._tipoParce == "F")
                    {
                        return "";
                    }
                    else
                    {
                        return "Insc. Estadual:";
                    }
                }
            }
            public string inscrMunicipal
            {
                get
                {
                    if (docParce._tipoParce == "F")
                    {
                        return "";
                    }
                    else
                    {
                        return "Insc. Municipal:";
                    }
                }
            }
            public string _propoParce { get; set; }
            public string propoParce
            {
                get
                {
                    string p = "", e = "";

                    if (_propoParce != null)
                    {
                        if (_propoParce.Length >= 500)
                        {
                            p = _propoParce.Substring(0, 499);
                            e = " ...";
                        }
                        else
                        {
                            p = _propoParce;
                        }
                    }
                    return p + e;

                }
            }
            public string anexoParce { get; set; }
            public string _telParce1;
            public string telParce1
            {
                get { return this._telParce1.Format(TipoFormat.Telefone); }
                set { this._telParce1 = value; }
            }
            public string _telParce2;
            public string telParce2
            {
                get { return this._telParce2.Format(TipoFormat.Telefone); }
                set { this._telParce2 = value; }
            }
            public string _whatsParce;
            public string whatsParce
            {
                get { return this._whatsParce.Format(TipoFormat.Telefone); }
                set { this._whatsParce = value; }
            }
            public string skypeParce { get; set; }
            //public byte[] Imagem { get; set; } ADICIONAR COLUNA
            public Endereco endParce { get; set; }
            public Documentos docParce { get; set; }
            public InformacoesResponsavel inforResp { get; set; }
            public InformacoesIndicacao inforIndic { get; set; }
            public List<Ocorrencia> ocorrParce { get; set; }
        }

        public class Endereco
        {
            public string Logradouro { get; set; }
            public int? Numero { get; set; }
            public string EnderecoStr
            {
                get { return Logradouro + ((Numero.HasValue) ? ", " + Numero.Value.ToString() : ""); }
            }
            public string Complemento { get; set; }
            public string Bairro { get; set; }
            public string Cidade { get; set; }
            public string Estado { get; set; }
            public string _CEP;
            public string CEP
            {
                get { return this._CEP.Format(TipoFormat.CEP); }
                set { this._CEP = value; }
            }
        }

        public class Documentos
        {
            public string _tipoParce { get; set; }
            public string tipoParce
            {
                get
                {
                    string tipo = "";

                    if (_tipoParce == "F")
                    {
                        tipo = "Pessoa Física";
                    }
                    else
                    {
                        tipo = "Pessoa Jurídica";
                    }

                    return tipo;
                }
            }
            public string _cnpj;
            public string cnpjParce
            {
                get
                {
                    if (_tipoParce == "F")
                    {
                        return this._cnpj.Format(TipoFormat.CPF);
                    }
                    else
                    {
                        return this._cnpj.Format(TipoFormat.CNPJ);
                    }
                }
                set { this._cnpj = value; }
            }
            public string ieParce { get; set; }
            public string imParce { get; set; }
        }

        public class InformacoesResponsavel
        {
            public string nomeResp { get; set; }
            public string FuncResp { get; set; }
            public string emailResp { get; set; }
            public string obserResp { get; set; }
            public string cpfResp { get; set; }
            public string _telResp;
            public string telResp
            {
                get { return this._telResp.Format(TipoFormat.Telefone); }
                set { this._telResp = value; }
            }
            public string _whatsResp;
            public string whatsResp
            {
                get { return this._whatsResp.Format(TipoFormat.Telefone); }
                set { this._whatsResp = value; }
            }
            public string skypeResp { get; set; }
        }
        public class InformacoesIndicacao
        {
            public string nomeIndic { get; set; }
            public string empreIndic { get; set; }
            public string emailIndic { get; set; }
            public string obserIndic { get; set; }
            public string _funciIndic { get; set; }
            public string funciIndic
            {
                get
                {
                    string f = "";

                    if (_funciIndic == "S")
                    {
                        f = "Sim";
                    }
                    else
                    {
                        f = "Não";
                    }

                    return f;
                }
            }
            public DateTime? _dtIndicIndic { get; set; }
            public string dtIndicIndic
            {
                get
                {
                    DateTime dt = Convert.ToDateTime(this._dtIndicIndic);

                    return dt.ToString("dd/MM/yyyy");
                }
            }
            public string telIndic { get; set; }
            public string whatsIndic { get; set; }
            public string skypeIndic { get; set; }
        }


        public class Ocorrencia
        {
            public string _tipoOcorr { get; set; }
            public string tipoOcorr
            {
                get
                {
                    string t = "";
                    switch (_tipoOcorr)
                    {
                        case "A":
                            t = "Administrativo";
                            break;

                        case "C":
                            t = "Cobrança";
                            break;

                        case "F":
                            t = "Financeiro";
                            break;

                        case "O":
                            t = "Ouvidoria";
                            break;

                        case "R":
                            t = "Recepção";
                            break;

                        case "T":
                            t = "Telemarketing";
                            break;

                        case "P":
                            t = "Pesquisa";
                            break;

                        case "X":
                            t = "Outros";
                            break;

                        default:
                            t = " - ";
                            break;
                    }

                    return t;
                }
            }
            public DateTime dtOcorr { get; set; }
            public string titulOcorr { get; set; }
            public string descrOcorr { get; set; }
            public string acaoOcorr { get; set; }
        }

        #endregion

    }

}




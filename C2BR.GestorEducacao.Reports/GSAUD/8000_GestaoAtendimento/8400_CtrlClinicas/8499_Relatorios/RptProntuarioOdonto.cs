using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8499_Relatorios
{
    public partial class RptProntuarioOdonto : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptProntuarioOdonto()
        {
            InitializeComponent();
        }

        public int InitReport(
              string infos,
              int coEmp,
              int Paciente,
              bool impAnamnese,
              bool impAgenda,
              string dtIniAgenda_,
              string dtFinAgenda_,
              bool impEvolucao,
              string dtIniEvolucao_,
              string dtFinEvolucao_,
              string NomeFuncionalidade
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

                DateTime dtIniAgenda = DateTime.Now;
                DateTime dtFinAgenda = DateTime.Now;

                if (impAgenda)
                {
                    dtIniAgenda = DateTime.Parse(dtIniAgenda_);
                    dtFinAgenda = DateTime.Parse(dtFinAgenda_);
                }

                DateTime dtIniEvolucao = DateTime.Now;
                DateTime dtFinEvolucao = DateTime.Now;

                if (impEvolucao)
                {
                    dtIniEvolucao = DateTime.Parse(dtIniEvolucao_);
                    dtFinEvolucao = DateTime.Parse(dtFinEvolucao_);
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
                               cep = tb07.CO_CEP_ALU
                           }).ToList();

                if (res.Count == 0)
                    return -1;

                this.Parametros = "( Paciente : " + res.FirstOrDefault().nomPac
                                + " Nº Registro : " + res.FirstOrDefault().nirePac
                                + (res.FirstOrDefault().nisPac != 0 ? " Nº NIS : " + res.FirstOrDefault().nisPac : "") + ")";

                var cons = new List<Contatos>();

                var ends = (from end in TB241_ALUNO_ENDERECO.RetornaTodosRegistros()
                            where end.TB07_ALUNO.CO_ALU == Paciente
                            select new Endereco
                            {
                                descEnd = end.DS_ENDERECO,
                                cep = end.CO_CEP,
                                tipoEnd = end.TB238_TIPO_ENDERECO.NM_TIPO_ENDERECO
                            }).ToList();

                var fons = (from fon in TB242_ALUNO_TELEFONE.RetornaTodosRegistros()
                            where fon.TB07_ALUNO.CO_ALU == Paciente
                            select new Telefone
                            {
                                _descFon = fon.NR_TELEFONE,
                                contato = fon.NO_CONTATO,
                                tipoFon = fon.TB239_TIPO_TELEFONE.NM_TIPO_TELEFONE
                            }).ToList();

                cons.Add(new Contatos() { Enderecos = ends, Telefones = fons });

                res.FirstOrDefault().Contatos = cons;

                //var ambulatorio = (from tbs428 in TBS428_APLIC_SERVI_AMBUL.RetornaTodosRegistros()
                //                   join tbs427 in TBS427_SERVI_AMBUL_ITENS.RetornaTodosRegistros() on tbs428.TBS427_SERVI_AMBUL_ITENS.ID_LISTA_SERVI_AMBUL equals tbs427.ID_LISTA_SERVI_AMBUL
                //                   join tbs426 in TBS426_SERVI_AMBUL.RetornaTodosRegistros() on tbs427.TBS426_SERVI_AMBUL.ID_SERVI_AMBUL equals tbs426.ID_SERVI_AMBUL
                //                   join tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros() on tbs427.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE equals tbs356.ID_PROC_MEDI_PROCE
                //                   where tbs426.TBS390_ATEND_AGEND.TB07_ALUNO.CO_ALU == Paciente
                //                   select new ServAbulatoriais
                //                   {
                //                       coCol = tbs426.CO_COL_CADAS,
                //                       dtCadastro = tbs426.DT_CADASTRO,
                //                       Procedimento = tbs356.NM_PROC_MEDI,
                //                       dtAtendimento = tbs428.IS_APLIC_SERVI_AMBUL.Equals("S") ? tbs428.DT_APLIC__SERVI_AMBUL : (DateTime?)null,
                //                       status = tbs428.IS_APLIC_SERVI_AMBUL
                //                   }).OrderBy(x => x.dtCadastro).ToList();

                //res.FirstOrDefault().ServAmbulatoriais = ambulatorio;


                if (impAnamnese)
                {
                    var anas = (from ana in TBS390_ATEND_AGEND.RetornaTodosRegistros()
                                join tb03 in TB03_COLABOR.RetornaTodosRegistros() on ana.CO_COL_ATEND equals tb03.CO_COL
                                where ana.TB07_ALUNO.CO_ALU == Paciente
                                && !String.IsNullOrEmpty(ana.DE_HDA)
                                select new Anamnese
                                {
                                    data = ana.DT_CADAS,
                                    idAna = ana.ID_ATEND_AGEND,
                                    descricao = ana.DE_HDA,
                                    nomProf = tb03.NO_COL
                                    //status = ana.CO_SITUA
                                }).ToList();

                    res.FirstOrDefault().Anamneses = anas;
                }

                var plas = new List<PlanAcoes>();
                var pls = (from tbs396 in TBS396_ATEND_ORCAM.RetornaTodosRegistros()
                           join tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros() on tbs396.TBS390_ATEND_AGEND.ID_ATEND_AGEND equals tbs390.ID_ATEND_AGEND
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs390.CO_COL_ATEND equals tb03.CO_COL
                           where tbs390.TBS174_AGEND_HORAR.CO_ALU == Paciente
               && (tbs390.FL_SITU_FATU == "A" || tbs390.FL_SITU_FATU == "F")
                           select new
                           {
                               nuDente = tbs396.NU_DENTE,
                               dtOrcamento = tbs396.DT_CADAS,
                               IdAtendOrcam = tbs396.ID_ATEND_ORCAM,
                               IdAtendAgend = tbs390.ID_ATEND_AGEND,
                               IdAgendHorar = tbs390.TBS174_AGEND_HORAR.ID_AGEND_HORAR,
                               dtAgenda = tbs396.DT_CADAS,
                               Profissional = tb03 != null ? tb03.NO_APEL_COL : " - ",
                               Procedimento = tbs396.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                               idProcedimento = tbs396.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE,
                               Local = tbs390.TB14_DEPTO != null ? tbs390.TB14_DEPTO.NO_DEPTO : "-"
                           }).ToList();

                foreach (var r in pls)
                {
                    var _tbs458 = TBS458_TRATA_PLANO.RetornaTodosRegistros().Where(x => x.TBS396_ATEND_ORCAM.ID_ATEND_ORCAM == r.IdAtendOrcam).FirstOrDefault();
                    var p = new PlanAcoes();
                    p.dtOrcamento = r.dtOrcamento;
                    p.dtAgenda = r.dtAgenda;
                    p.ACAO = _tbs458 != null && _tbs458.CO_SITUA.Equals("F") ? "Realizado" : "Não Realiz.";
                    p.nomeProcedimento = _tbs458 != null ? r.Procedimento : "*" + r.Procedimento;
                    p.nomeProfissional = r.Profissional;
                    p.dtRealizado = _tbs458 != null && _tbs458.CO_SITUA.Equals("F") ? _tbs458.DT_SITUA.Value : (DateTime?)null;
                    p.nuDente = r.nuDente.HasValue ? r.nuDente.Value : 0;
                    p.LOCAL = r.Local;
                    plas.Add(p);
                }

                res.FirstOrDefault().Planejamentos = plas;


                if (impAgenda)
                {
                    var agds = (from agd in TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(a => a.DT_AGEND_HORAR >= dtIniAgenda && a.DT_AGEND_HORAR <= dtFinAgenda && a.CO_ALU == Paciente)
                                join tb03 in TB03_COLABOR.RetornaTodosRegistros() on agd.CO_COL equals tb03.CO_COL
                                select new Agenda
                                {
                                    numRap = agd.NU_REGIS_CONSUL,
                                    dataAgend = agd.DT_AGEND_HORAR,
                                    horaAgend = agd.HR_AGEND_HORAR,
                                    sitAgn = agd.CO_SITUA_AGEND_HORAR,
                                    flgConAgn = agd.FL_CONF_AGEND,
                                    flgEncAgn = agd.FL_AGEND_ENCAM,
                                    flgJusCan = agd.FL_JUSTI_CANCE,
                                    nomProf = tb03.NO_COL,
                                    _classProf = tb03.CO_CLASS_PROFI,
                                    registroProf = tb03.CO_SIGLA_ENTID_PROFI + " " + (!String.IsNullOrEmpty(tb03.NU_ENTID_PROFI) ? tb03.NU_ENTID_PROFI + "/" + tb03.CO_UF_ENTID_PROFI : "")
                                }).ToList();

                    res.FirstOrDefault().Agendas = agds;
                }

                if (impEvolucao)
                {
                    var evls = (from evl in TBS390_ATEND_AGEND.RetornaTodosRegistros()
                                join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(a => a.DT_AGEND_HORAR >= dtIniEvolucao && a.DT_AGEND_HORAR <= dtFinEvolucao && a.CO_ALU == Paciente) on evl.TBS174_AGEND_HORAR.ID_AGEND_HORAR equals tbs174.ID_AGEND_HORAR
                                join tb03 in TB03_COLABOR.RetornaTodosRegistros() on evl.TBS174_AGEND_HORAR.CO_COL_ATEND equals tb03.CO_COL
                                select new Evolucao
                                {
                                    idEvolucao = evl.ID_ATEND_AGEND,
                                    numRap = evl.NU_REGIS,
                                    dataAgend = tbs174.DT_AGEND_HORAR,
                                    horaAgend = tbs174.HR_AGEND_HORAR,
                                    nomProf = tb03.NO_COL,
                                    descricao = evl.DE_ACAO_REALI
                                }).ToList();

                    res.FirstOrDefault().Evolucoes = evls;

                    var exms = new List<Exame>();
                    var mdcs = new List<Medicamento>();

                    foreach (var e in evls)
                    {
                        var mds = (from mdc in TBS399_ATEND_MEDICAMENTOS.RetornaTodosRegistros()
                                   join tb03 in TB03_COLABOR.RetornaTodosRegistros() on mdc.TBS390_ATEND_AGEND.TBS174_AGEND_HORAR.CO_COL equals tb03.CO_COL
                                   where mdc.TBS390_ATEND_AGEND.ID_ATEND_AGEND == e.idEvolucao
                                   select new Medicamento
                                   {
                                       numRap = mdc.TBS390_ATEND_AGEND.NU_REGIS,
                                       desc = mdc.DE_PRESC,
                                       data = mdc.DT_CADAS,
                                       nomProf = tb03.NO_COL,
                                       registroProf = tb03.CO_SIGLA_ENTID_PROFI + " " + (!String.IsNullOrEmpty(tb03.NU_ENTID_PROFI) ? tb03.NU_ENTID_PROFI + "/" + tb03.CO_UF_ENTID_PROFI : "")
                                   }).ToList();

                        mdcs.AddRange(mds);

                        var ems = (from mdc in TBS398_ATEND_EXAMES.RetornaTodosRegistros()
                                   join tb03 in TB03_COLABOR.RetornaTodosRegistros() on mdc.TBS390_ATEND_AGEND.TBS174_AGEND_HORAR.CO_COL_ATEND equals tb03.CO_COL
                                   where mdc.TBS390_ATEND_AGEND.ID_ATEND_AGEND == e.idEvolucao
                                   select new Exame
                                   {
                                       numRap = mdc.TBS390_ATEND_AGEND.NU_REGIS,
                                       desc = mdc.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                                       data = mdc.DT_CADAS,
                                       nomProf = tb03.NO_COL,
                                       classProf = tb03.CO_CLASS_PROFI,
                                       registroProf = tb03.CO_SIGLA_ENTID_PROFI + " " + (!String.IsNullOrEmpty(tb03.NU_ENTID_PROFI) ? tb03.NU_ENTID_PROFI + "/" + tb03.CO_UF_ENTID_PROFI : "")
                                   }).ToList();

                        exms.AddRange(ems);
                    }

                    res.FirstOrDefault().Medicamentos = mdcs;
                    res.FirstOrDefault().Exames = exms;
                }

                //var ats = (from tbs333 in TBS333_ATEST_MEDIC_PACIE.RetornaTodosRegistros()
                //           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs333.CO_ALU equals tb07.CO_ALU
                //           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs333.CO_COL_MEDIC equals tb03.CO_COL
                //           where tbs333.CO_ALU == Paciente
                //           select new Atestado
                //           {
                //               Data = tbs333.DT_ATEST_MEDIC,
                //               qtdDias = tbs333.QT_DIAS,
                //               observ = tbs333.DE_OBSER,
                //               numReg = tbs333.NU_REGIS_ATEST_MEDIC,
                //               nomMedic = tb03.NO_COL,
                //               registroMedic = tb03.NU_ENTID_PROFI,
                //           }).OrderBy(x => x.Data).ToList();

                //res.FirstOrDefault().Atestado = ats;

                //var tri = (from tbs194 in TBS194_PRE_ATEND.RetornaTodosRegistros()
                //           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs194.CO_ALU equals tb07.CO_ALU
                //           where tbs194.CO_ALU == Paciente
                //           select new Triagem
                //           {
                //               data = tbs194.DT_PRE_ATEND,
                //               altura = tbs194.NU_ALTU,
                //               peso = tbs194.NU_PESO,
                //               pressao = tbs194.NU_PRES_ARTE,
                //               temp = tbs194.NU_TEMP,
                //               InforPrevia = tbs194.DE_INFOR_PREV,
                //               ClassRisco = tbs194.CO_TIPO_RISCO
                //           }).ToList();

                //res.FirstOrDefault().Triagem = tri;

                //Adiciona ao DataSource do Relatório
                bsReport.Clear();
                foreach (var item in res)
                {
                    item.impAgenda = impAgenda;
                    item.impAnamnese = impAnamnese;
                    item.impEvolucao = impEvolucao;

                    bsReport.Add(item);
                }

                return 1;

            }
            catch { return 0; }
        }

        public class Prontuario
        {
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

            public bool impAnamnese { get; set; }

            public bool impAgenda { get; set; }

            public bool impEvolucao { get; set; }

            public List<Contatos> Contatos { get; set; }

            public List<Anamnese> Anamneses { get; set; }

            public List<PlanAcoes> Planejamentos { get; set; }

            public List<Agenda> Agendas { get; set; }

            public List<Evolucao> Evolucoes { get; set; }

            public List<Medicamento> Medicamentos { get; set; }

            public List<Exame> Exames { get; set; }

            //public List<Atestado> Atestado { get; set; }

            //public List<Triagem> Triagem { get; set; }

            //public List<ServAbulatoriais> ServAmbulatoriais { get; set; }
        }

        public class Contatos
        {
            public List<Endereco> Enderecos { get; set; }

            public List<Telefone> Telefones { get; set; }
        }

        public class Endereco
        {
            public string tipoEnd { get; set; }

            public string descEnd { get; set; }

            public string _cep;
            public string cep
            {
                get { return this._cep.Format(TipoFormat.CEP); }
                set { this._cep = value; }
            }
        }

        public class Telefone
        {
            public string tipoFon { get; set; }

            public decimal _descFon;
            public string descFon
            {
                get { return Funcoes.Format(_descFon.ToString(), TipoFormat.Telefone); }
            }

            public string contato { get; set; }
        }

        public class Anamnese
        {
            public DateTime data { get; set; }

            public int idAna { get; set; }

            public string descricao { get; set; }

            public string nomProf { get; set; }

            public string status { get; set; }
        }

        public class PlanAcoes
        {
            public int? nuDente { get; set; }
            public string nomeProcedimento { get; set; }
            public string nomeProfissional { get; set; }
            public DateTime dtAgenda { get; set; }
            public DateTime dtOrcamento { get; set; }
            public string DT_ORCAMENTO { get { return this.dtOrcamento.ToShortDateString(); } }
            public DateTime? dtRealizado { get; set; }
            public string DT_REALIZADO { get { return this.dtRealizado.HasValue ? this.dtRealizado.Value.ToShortDateString() : ""; } }
            public string ACAO { get; set; }
            public string LOCAL { get; set; }
        }

        public class Agenda
        {
            public string numRap { get; set; }

            public DateTime dataAgend { get; set; }

            public string horaAgend { get; set; }

            public string _classProf { get; set; }
            public string classProf
            {
                get
                {
                    return Funcoes.GetNomeClassificacaoFuncional(_classProf, false);
                }
            }

            public string nomProf { get; set; }

            public string registroProf { get; set; }

            public string sitAgn { get; set; }
            public string flgConAgn { get; set; }
            public string flgEncAgn { get; set; }
            public string flgJusCan { get; set; }

            public string status
            {
                get
                {
                    var s = " - ";

                    if (this.sitAgn == "C" && this.flgJusCan == "N")
                        s = "Falta";
                    else if (this.sitAgn == "C" && this.flgJusCan == "S")
                        s = "Falta Just.";
                    else if (this.sitAgn == "A" && this.flgConAgn == "S" && (this.flgEncAgn == "N" || String.IsNullOrEmpty(this.flgEncAgn)))
                        s = "Presença";
                    else if (this.sitAgn == "A" && this.flgConAgn == "S" && this.flgEncAgn == "S")
                        s = "Encaminhado";
                    else if (this.sitAgn == "R" && this.flgConAgn == "S" && this.flgEncAgn == "S")
                        s = "Atendido";
                    else if (this.sitAgn == "A" && this.flgConAgn == "N")
                        s = "Em Aberto";

                    return s;
                }
            }
        }

        public class Evolucao
        {
            public int idEvolucao { get; set; }

            public string numRap { get; set; }

            public DateTime dataAgend { get; set; }

            public string horaAgend { get; set; }

            public string nomProf { get; set; }

            public string descricao { get; set; }
        }

        public class Medicamento
        {
            public string numRap { get; set; }

            public int numItem { get; set; }

            public DateTime data { get; set; }

            public string desc { get; set; }

            public string nomProf { get; set; }

            public string registroProf { get; set; }
        }

        public class Exame
        {
            public string numRap { get; set; }

            public DateTime data { get; set; }

            public string desc { get; set; }

            public string nomProf { get; set; }

            public string registroProf { get; set; }

            public string classProf { get; set; }
        }

        //public class ServAbulatoriais
        //{
        //    public string Procedimento { get; set; }
        //    public int? coCol { get; set; }
        //    public string Profissional
        //    {
        //        get
        //        {
        //            int aux = coCol.HasValue ? coCol.Value : 0;
        //            return !string.IsNullOrEmpty(TB03_COLABOR.RetornaPeloCoCol(aux).NO_COL) ? TB03_COLABOR.RetornaPeloCoCol(aux).NO_COL : "-";
        //        }
        //    }
        //    public DateTime? dtCadastro { get; set; }
        //    public string dt_cadastro
        //    {
        //        get
        //        {
        //            return dtCadastro.HasValue ? dtCadastro.Value.ToString() : "-";
        //        }
        //    }
        //    public string status { get; set; }
        //    public DateTime? dtAtendimento { get; set; }
        //    public string dt_atendimento
        //    {
        //        get
        //        {
        //            return dtAtendimento.HasValue ? dtAtendimento.Value.ToString() : "-";
        //        }
        //    }
        //}

        //public class Atestado
        //{
        //    public DateTime? Data { get; set; }

        //    public string dtAtestado
        //    {
        //        get 
        //        {
        //            return this.Data.HasValue ? Data.Value.ToShortDateString() : "-";
        //        }
        //    }

        //    public int? qtdDias { get; set; }

        //    public string observ { get; set; }

        //    public string numReg { get; set; }

        //    public string nomMedic { get; set; }

        //    public string registroMedic { get; set; }
        //}

        //public class Triagem
        //{
        //    public string numRap { get; set; }

        //    public DateTime data { get; set; }

        //    public decimal? altura { get; set; }

        //    public decimal? peso { get; set; }

        //    public string pressao { get; set; }

        //    public decimal? temp { get; set; }

        //    public string InforPrevia { get; set; }

        //    public int ClassRisco { get; set; }

        //    public string risco
        //    {
        //        get
        //        {
        //            switch (ClassRisco)
        //            {
        //                case 0:
        //                    return "Não Definido";
        //                case 1:
        //                    return "Emergência";
        //                case 2:
        //                    return "Muito Urgente";
        //                case 3:
        //                    return "Urgente";
        //                case 4:
        //                    return "Pouco Urgente";
        //                case 5:
        //                    return "Não Urgente";
        //                default:
        //                    return "Não Definido";
        //            }
        //        }
        //    }
        //}

        private void Detail10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}

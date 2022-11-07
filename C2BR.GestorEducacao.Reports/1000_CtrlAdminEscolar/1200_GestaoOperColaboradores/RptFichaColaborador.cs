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

namespace C2BR.GestorEducacao.Reports._1000_CtrlAdminEscolar._1200_GestaoOperColaboradores
{
    public partial class RptFichaColaborador : C2BR.GestorEducacao.Reports.RptRetrato
    {
        #region ctor

        public RptFichaColaborador()
        {
            InitializeComponent();
        }

        #endregion

        #region Init Report

        public int InitReport(int codEmp, int codFun, int anoBase, string flagColab, string infos, int CO_EMP_LOTACAO ,string NomeFuncionalidadeCadastrada)
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
                this.celFreq.Text = string.Format(celFreq.Text, anoBase);
                if (NomeFuncionalidadeCadastrada == "")
                {
                    lblTitulo.Text = "EMISSÃO DA FICHA INDIVIDUAL DE INFORMAÇÕES DE COLABORADORES * ";
                }
                else
                {
                    lblTitulo.Text = NomeFuncionalidadeCadastrada;
                }
                
                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Colaborador

                var lstf = (from e in ctx.TB25_EMPRESA
                            join c in ctx.TB03_COLABOR on e.CO_EMP equals c.CO_EMP into ci
                            from c in ci.DefaultIfEmpty()
                            join tp in ctx.TB21_TIPOCAL on c.CO_TPCAL equals tp.CO_TPCAL into tpi
                            from tp in tpi.DefaultIfEmpty()
                            join i in ctx.TB18_GRAUINS on c.CO_INST equals i.CO_INST into ii
                            from i in ii.DefaultIfEmpty()
                            join t in ctx.TB20_TIPOCON on c.CO_TPCON equals t.CO_TPCON into ti
                            from t in ti.DefaultIfEmpty()
                            join f in ctx.TB15_FUNCAO on c.CO_FUN equals f.CO_FUN into fi
                            from f in fi.DefaultIfEmpty()
                            join d in ctx.TB14_DEPTO on c.CO_DEPTO equals d.CO_DEPTO into di
                            from d in di.DefaultIfEmpty()
                            join es in ctx.TB63_ESPECIALIDADE on c.CO_ESPEC equals es.CO_ESPECIALIDADE into esi
                            from es in esi.DefaultIfEmpty()
                            join bai in ctx.TB905_BAIRRO on c.CO_BAIRRO equals bai.CO_BAIRRO into ba
                            from bai in ba.DefaultIfEmpty()
                            where c.CO_FUN == f.CO_FUN && /*c.CO_EMP == codEmp &&*/ c.CO_COL == codFun
                                && (flagColab != "0" ? c.FLA_PROFESSOR == flagColab : flagColab == "0")
                                && (codEmp != 0 ? c.CO_EMP == codEmp : 0 == 0)
                                && (CO_EMP_LOTACAO != 0 ? c.CO_EMP_UNID_CONT == CO_EMP_LOTACAO : 0 == 0)
                            select new Colaborador
                            {
                                GrauInstrucao = i.NO_INST,
                                Nome = c.NO_COL,
                                Apelido = c.NO_APEL_COL,
                                Sexo = c.CO_SEXO_COL,
                                DtNascimento = c.DT_NASC_COL,
                                Matricula = c.CO_MAT_COL,
                                Categoria = (c.FLA_PROFESSOR == "N") ? "Funcionário" : "Professor",
                                Deficiencia = c.TP_DEF,
                                SituacaoLinha1 = c.CO_SITU_COL,
                                SituacaoLinha2 = (c.TIPO_SITU == "R") ? "Remunerado" : "Não remunerado",
                                SituacaoLinha3 = c.DT_ALT_REGISTRO,
                                Especialidade = es.NO_ESPECIALIDADE,
                                Email = c.CO_EMAI_COL,
                                Telefone = c.NU_TELE_RESI_COL,
                                Celular = c.NU_TELE_CELU_COL,
                                DtCadastro = c.DT_CADA_COL,
                                Imagem = c.Image.ImageStream,
                                EstadoCivil = c.CO_ESTADO_CIVIL,

                                Endereco = new Endereco()
                                {
                                    Bairro = bai.NO_BAIRRO,
                                    CEP = c.NU_CEP_ENDE_COL,
                                    Cidade = bai.TB904_CIDADE.NO_CIDADE,
                                    Complemento = c.DE_COMP_ENDE_COL,
                                    Estado = c.CO_ESTA_ENDE_COL,
                                    Logradouro = c.DE_ENDE_COL,
                                    Numero = c.NU_ENDE_COL
                                },

                                Documentos = new Documentos()
                                {
                                    CNH = c.CO_CNH_NDOC,
                                    CNHClass = c.CO_CNH_CATEG,
                                    CNHValidade = c.CO_CNH_VALID,
                                    CPF = c.NU_CPF_COL,
                                    CTPS = c.CO_CTPS_NUMERO,
                                    CTPSCod = c.CO_CTPS_SERIE,
                                    CTPSUF = c.CO_CTPS_UF,
                                    RegProfissional = c.CO_PIS_PASEP,
                                    RG = c.CO_RG_COL,
                                    RGData = c.DT_EMIS_RG_COL,
                                    RGOrgao = c.CO_EMIS_RG_COL,
                                    RGUF = c.CO_ESTA_RG_COL,
                                    Titulo = c.NU_TIT_ELE,
                                    TituloSessao = c.NU_SEC_ELE,
                                    TituloZona = c.NU_ZONA_ELE
                                },

                                Informacoes = new InformacoesFuncionais()
                                {
                                    TipoContrato = t.NO_TPCON,
                                    Departamento = d.NO_DEPTO,
                                    DtAdmissao = c.DT_INIC_ATIV_COL,
                                    DtDemissao = c.DT_TERM_ATIV_COL,
                                    Funcao = f.NO_FUN,
                                    Email = c.CO_EMAIL_FUNC_COL,
                                    Telefone = c.NU_TELE_FUNC_COL,
                                    Salario = c.VL_SALAR_COL,
                                    CargaHoraria = c.NU_CARGA_HORARIA,
                                    UnidadeFuncional = e.NO_RAZSOC_EMP
                                }

                            
                            });

                //var s = (lstf as ObjectQuery).ToTraceString();

                #endregion
                
                var func = lstf.FirstOrDefault();

                // Se não encontrou o funcionário
                if (func == null)
                    return -1;

                #region Benefícios do Funcionario

                var lst = (from b in ctx.TB287_COLABOR_BENEF
                           where b.TB03_COLABOR.CO_COL == codFun
                           select b.TB286_TIPO_BENECIF.NO_BENEFICIO).ToList();

                if (lst.Count == 0)
                {
                    func.Beneficios = "-";
                }
                else
                {
                    func.Beneficios = string.Empty;

                    foreach (var b in lst)
                        func.Beneficios += b + "/";

                    func.Beneficios = func.Beneficios.TrimEnd('/');
                }

                #endregion

                #region Cursos do Funcionário

                var lstCur = (from cf in ctx.TB62_CURSO_FORM.Include("TB100_ESPECIALIZACAO")
                              where /*cf.CO_EMP == codEmp &&*/ cf.CO_COL == codFun
                              select new Curso
                              {
                                  CargaHoraria = cf.NU_CARGA_HORARIA,
                                  Conclusao = cf.CO_MESANO_FIM,
                                  Instituicao = cf.NO_INSTIT_CURSO,
                                  Local = cf.NO_CIDADE_CURSO + "/" + cf.CO_UF_CURSO,
                                  Nome = cf.TB100_ESPECIALIZACAO.DE_ESPEC,
                                  Tipo = cf.TB100_ESPECIALIZACAO.TP_ESPEC
                              }).ToList();

                func.Cursos = lstCur;

                #endregion

                #region Ocorrencias do Funcionário

                var lstOcor = (from o in ctx.TB151_OCORR_COLABOR
                               where o.TB03_COLABOR.CO_COL == codFun
                               select new Ocorrencia
                               {
                                   Descricao = o.DE_OCORR,
                                   Tipo = o.TB150_TIPO_OCORR.DE_TIPO_OCORR,
                                   Data = o.DT_OCORR,
                                   DataPublicacao = o.DT_CADASTRO
                               }).ToList();

                func.Ocorrencias = lstOcor;

                #endregion

                #region Movimentos do Funcionário

                var lstMov = (from m in ctx.TB286_MOVIM_TRANSF_FUNCI
                              where m.TB03_COLABOR.CO_COL == codFun
                              select new Movimento
                              {
                                  Motivo = m.CO_TIPO_MOVIM,
                                  Destino = m.TB25_EMPRESA.sigla,
                                  Referencia = m.TB25_EMPRESA1.sigla,
                                  Tipo = m.CO_MOTIVO_AFAST,
                                  Data = m.DT_INI_MOVIM_TRANSF_FUNCI,
                              }).ToList();

                func.Movimentos = lstMov;

                #endregion

                #region Ocorrencias de Saude do Funcionário

                var lstOcorSaude = (from o in ctx.TB143_ATEST_MEDIC
                                    join c in ctx.TB03_COLABOR on o.CO_USU equals c.CO_COL
                                    where /*o.TB25_EMPRESA.CO_EMP == codEmp
                                    && */ o.DT_CONSU.Year == anoBase
                                    && (codFun == o.CO_USU )
                                    select new OcorrenciaSaude
                                    {
                                        Doenca = o.TB117_CODIGO_INTERNACIONAL_DOENCA.NO_CID,
                                        DtConsulta = o.DT_CONSU,
                                        DtEntrega = o.DT_ENTRE_ATEST,
                                        Medico = o.NO_MEDIC.ToUpper(),
                                        QtdDias = o.QT_DIAS_LICEN
                                    }).OrderBy(p => p.DtConsulta);

                func.OcorrenciasSaude = lstOcorSaude.ToList();

                #endregion

                #region Frequencia do Funcionário

                var lstFreq = (from m in ctx.TB199_FREQ_FUNC
                               where m.TB03_COLABOR.CO_COL == codFun
                               && m.DT_FREQ.Year == anoBase
                               && m.TP_FREQ == "E" && m.STATUS == "A"
                               select new
                               {
                                   Data = m.DT_FREQ,
                                   Flag = m.FLA_PRESENCA
                               }).DistinctBy(x => x.Data).ToList();

                var lstMes = lstFreq.GroupBy(x => x.Data.Month).OrderBy(x => x.Key);

                List<FrequenciaMensal> lstF = new List<FrequenciaMensal>();

                for (int mes = 1; mes < 13; mes++)
                {
                    if (lstMes.Any(x => x.Key == mes))
                    {
                        var li = lstMes.First(x => x.Key == mes);
                        FrequenciaMensal f = new FrequenciaMensal();
                        int tDias = DateTime.DaysInMonth(anoBase, li.Key);

                        f.Mes = Funcoes.GetMes(li.Key);
                        f.TotalFaltas = li.Count(x => x.Flag == "N");
                        f.TotalPresenca = li.Count(x => x.Flag == "S");
                        f.PercentualPresenca = ((decimal)f.TotalPresenca / (decimal)tDias);
                        f.PercentualFaltas = ((decimal)f.TotalFaltas / (decimal)tDias);
                        lstF.Add(f);
                    }
                    else
                    {
                        FrequenciaMensal fNew = new FrequenciaMensal();
                        fNew.Mes = Funcoes.GetMes(mes);
                        lstF.Add(fNew);
                    }
                }

                func.Frequencias = lstF;

                #endregion

                #region Evolução Funcional
              
                var lstEvoluFunc = (from evol in ctx.TB286_MOVIM_TRANSF_FUNCI 
                                    
                                    where (evol.TB03_COLABOR.CO_COL == codFun)
                                    
                                    select new EvolucaoFunc
                                    {
                                        DtInicio = evol.DT_INI_MOVIM_TRANSF_FUNCI,
                                        DtFim = evol.DT_FIM_MOVIM_TRANSF_FUNCI,
                                        EmpOrigem = evol.TB25_EMPRESA.sigla,
                                        EmpDestino = evol.TB25_EMPRESA1.sigla,
                                        FuncOrigem = evol.TB15_FUNCAO.NO_FUN,
                                        FuncDestino = evol.TB15_FUNCAO1.NO_FUN

                                    }).OrderBy(d => d.DtInicio);

                func.Evolucao = lstEvoluFunc.ToList();

                #endregion

                // Adiciona o Funcionario ao DataSource do Relatório
                bsReport.Clear();
                bsReport.Add(func);

                return 1;
            }
            catch { return 0; }
        }

        #endregion

        #region Classe Colaborador do Relatorio

        public class Colaborador
        {
            public Colaborador()
            {
                this.Cursos = new List<Curso>();
                this.Ocorrencias = new List<Ocorrencia>();
                this.OcorrenciasSaude = new List<OcorrenciaSaude>();
                this.Movimentos = new List<Movimento>();
                this.Evolucao = new List<EvolucaoFunc>();
            }

            public string Nome { get; set; }

            public string _matricula;
            public string Matricula
            {
                get { return this._matricula.Format(TipoFormat.MatriculaColaborador); }
                set { this._matricula = value; }
            }

            public DateTime DtNascimento { get; set; }
            public string DtNascStr
            {
                get
                {
                    return string.Format("{0:d} ({1})", DtNascimento, Funcoes.GetIdade(DtNascimento));
                }
            }

            public string Sexo { get; set; }
            public string _deficiencia;
            public string Deficiencia
            {
                get { return Funcoes.GetDeficienciaColabor(this._deficiencia); }
                set { this._deficiencia = value; }
            }

            public string Categoria { get; set; }
            public string Apelido { get; set; }
            public string Beneficios { get; set; }

            public string _estadoCivil;
            public string EstadoCivil
            {
                get { return Funcoes.GetEstadoCivil(this._estadoCivil); }
                set { this._estadoCivil = value; }
            }

            public string GrauInstrucao { get; set; }
            public string Especialidade { get; set; }
            public string Email { get; set; }

            public string _telefone;
            public string Telefone
            {
                get { return this._telefone.Format(TipoFormat.Telefone); }
                set { this._telefone = value; }
            }

            public string _celular;
            public string Celular
            {
                get { return this._celular.Format(TipoFormat.Telefone); }
                set { this._celular = value; }
            }

            public DateTime DtCadastro { get; set; }

            public string _situacaoLinha1;
            public string SituacaoLinha1
            {
                get { return Funcoes.GetSituacaoColabor(this._situacaoLinha1); }
                set { this._situacaoLinha1 = value; }
            }

            public string SituacaoLinha2 { get; set; }
            public DateTime? SituacaoLinha3 { get; set; }
            public byte[] Imagem { get; set; }
            public Endereco Endereco { get; set; }
            public Documentos Documentos { get; set; }
            public InformacoesFuncionais Informacoes { get; set; }
            public List<Curso> Cursos { get; set; }
            public List<Ocorrencia> Ocorrencias { get; set; }
            public List<OcorrenciaSaude> OcorrenciasSaude { get; set; }
            public List<Movimento> Movimentos { get; set; }
            public List<FrequenciaMensal> Frequencias { get; set; }
            public List<EvolucaoFunc> Evolucao { get; set; }
        }

        public class Endereco
        {
            public string Logradouro { get; set; }
            public decimal? Numero { get; set; }
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
            public decimal? CTPS { get; set; }
            public decimal? CTPSCod { get; set; }
            public string CTPSUF { get; set; }

            public string CTPSStr
            {
                get
                {
                    if (!CTPS.HasValue)
                        return "-";

                    return CTPS.Value.ToString()
                        + ((CTPSCod.HasValue) ? " - " + CTPSCod.Value.ToString() : "")
                        + ((!string.IsNullOrEmpty(CTPSUF)) ? " - " + CTPSUF : "");
                }
            }

            public string Titulo { get; set; }
            public string TituloZona { get; set; }
            public string TituloSessao { get; set; }

            public string TituloStr
            {
                get
                {
                    if (string.IsNullOrEmpty(Titulo))
                        return "-";

                    return Titulo
                        + ((!string.IsNullOrEmpty(TituloZona)) ? " - Z: " + TituloZona : "")
                        + ((!string.IsNullOrEmpty(TituloSessao)) ? " - S: " + TituloSessao : "");
                }
            }

            public string _CPF;
            public string CPF
            {
                get { return this._CPF.Format(TipoFormat.CPF); }
                set { this._CPF = value; }
            }
            public decimal? CNH { get; set; }
            public string CNHClass { get; set; }
            public DateTime? CNHValidade { get; set; }

            public string CNHStr
            {
                get
                {
                    if (!CNH.HasValue)
                        return "-";

                    return CNH.Value.ToString()
                        + ((!string.IsNullOrEmpty(CNHClass)) ? " - " + CNHClass : "")
                        + ((CNHValidade.HasValue) ? " - " + CNHValidade.Value.ToString("dd/MM/yyyy") : "");
                }
            }

            public string RG { get; set; }
            public string RGOrgao { get; set; }
            public string RGUF { get; set; }
            public DateTime RGData { get; set; }

            public string RGStr
            {
                get
                {
                    if (string.IsNullOrEmpty(RG))
                        return "-";

                    return RG
                        + ((!string.IsNullOrEmpty(RGOrgao)) ? " - " + RGOrgao : "")
                        + ((!string.IsNullOrEmpty(RGUF)) ? " - " + RGUF : "")
                        + " - " + (RGData.ToString("dd/MM/yyyy"));
                }
            }

            public decimal? RegProfissional { get; set; }
            public string RegProfissionalStr
            {
                get { return (RegProfissional.HasValue) ? RegProfissional.ToString() : "-"; }
            }
        }

        public class InformacoesFuncionais
        {
            public string UnidadeFuncional { get; set; }
            public string Email { get; set; }
            public string TipoContrato { get; set; }
            public string Funcao { get; set; }
            public string Departamento { get; set; }
            public DateTime DtAdmissao { get; set; }
            public DateTime? DtDemissao { get; set; }
            public string DtDemissaoStr
            {
                get { return DtDemissao.HasValue ? DtDemissao.Value.ToString("dd/MM/yyyy") : "Em Atividade"; }
            }
            public int CargaHoraria { get; set; }
            public double? Salario { get; set; }
            public string _telefone;
            public string Telefone
            {
                get { return this._telefone.Format(TipoFormat.Telefone); }
                set { this._telefone = value; }
            }
        }

        public class Curso
        {
            public string Nome { get; set; }
            public int CargaHoraria { get; set; }
            public string Instituicao { get; set; }
            public string Conclusao { get; set; }
            public string _tipo;
            public string Tipo
            {
                get { return Funcoes.GetTipoEspecializacao(this._tipo); }
                set { this._tipo = value; }
            }
            public string Local { get; set; }
        }

        public class Ocorrencia
        {
            public string Descricao { get; set; }
            public string Tipo { get; set; }
            public DateTime Data { get; set; }
            public DateTime DataPublicacao { get; set; }
        }

        public class Movimento
        {
            public string Motivo { get; set; }
            public string Referencia { get; set; }
            public string Destino { get; set; }
            public string Tipo { get; set; }
            public DateTime Data { get; set; }
            public string TipoDesc {
                get
                {
                    if(this.Tipo == "MI")
                    {
                        return "Movimentação Interna";
                    }
                    else if (this.Tipo == "ME")
                    {
                        return "Movimentação Externa";
                    }
                    else if (this.Tipo == "TE")
                    {
                        return "Transferência Externa"; 
                    }
                    else
                    {
                        return  "-";
                    }

                }
            }
            public string DescMotivo{
                get{
                    if(this.Motivo == "TEX")
                        return "Transferência Externa";
                    else if(this.Motivo == "DIS")
                        return "Disponibilidade";
                    else if(this.Motivo == "APO")
                        return "Atividade Pontual";
                    else if(this.Motivo == "OUT")
                        return "Outros";
                    else if(this.Motivo == "PRO")
                        return "Promoção";
                    else if(this.Motivo == "TIN")
                        return "Transferência Interna";
                    else if(this.Motivo == "DIS")
                        return "Disponibilidade";
                    else if(this.Motivo == "APO")
                        return "Atividade Pontual";
                    else if(this.Motivo == "FER")
                        return "Férias";
                    else if(this.Motivo == "LME")
                        return "Licença Médica";
                    else if(this.Motivo == "LMA")
                        return "Licença Maternidade";
                    else if(this.Motivo == "LPA")
                        return "Licença Paternidade";
                    else if(this.Motivo == "LPR")
                        return "Licença Prêmia";
                    else if(this.Motivo == "LFU")
                        return "Licença Funcional";
                    else if(this.Motivo == "OLI")
                        return "Outras Licenças";
                    else if(this.Motivo == "DEM")
                        return "Demissão";
                    else if(this.Motivo == "ECO")
                        return "Encerramento Contrato";
                    else if(this.Motivo == "AFA")
                        return "Afastamento";
                    else if(this.Motivo == "SUS")
                        return "Suspensão";
                    else if(this.Motivo == "CAP")
                        return "Capacitação";
                    else if(this.Motivo == "TRE")
                        return "Treinamento";
                    else if(this.Motivo == "MOU")
                        return "Motivos Outros";
                    else
                        return "Outros";
                    
                }
            }
        }

        public class OcorrenciaSaude
        {
            public string Doenca { get; set; }
            public string Medico { get; set; }
            public DateTime DtConsulta { get; set; }
            public DateTime DtEntrega { get; set; }
            public int? QtdDias { get; set; }
            public string QtdDiasStr
            {
                get { return (QtdDias.HasValue) ? QtdDias.Value.ToString("n0") : "-"; }
            }
        }

        public class FrequenciaMensal
        {
            public string Mes { get; set; }
            public int TotalPresenca { get; set; }
            public int TotalFaltas { get; set; }
            public int FaltasJustificadas { get; set; }
            public int FaltasNaoJustificadas { get; set; }
            public int LicencaMedica { get; set; }
            public decimal PercentualFaltas { get; set; }
            public decimal PercentualPresenca { get; set; }
        }

        public class EvolucaoFunc
        {

            public string EmpOrigem { get; set; }
            public string EmpDestino { get; set; }
            public string FuncOrigem { get; set; }
            public string FuncDestino { get; set; }
            public DateTime DtInicio { get; set; }
            public DateTime? DtFim { get; set; }
            public string DtInicioDesc
            {
                get
                {
                    if (this.DtInicio == null)
                        return "-";
                    else
                    {
                        return DtInicio.ToString("dd/MM/yy");
                    }
                }
            }
            public string DtFimDesc
            {
                get
                {
                    return this.DtFim.HasValue ? DtFim.Value.ToString("dd/MM/yy") : "-";
                }
            }
        }

        #endregion

        private void DetailReport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DetailReportBand Detail = (DetailReportBand)sender;
            if (Detail.RowCount == 0)
            {
                this.xrLabelCursos.Text = " ***Sem Registro";
            }
            else
            {
                this.xrLabelCursos.Text = string.Empty;
            }
         
        }

        private void DetailReport3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DetailReportBand Detail = (DetailReportBand)sender;
            if (Detail.RowCount == 0)
            {
                this.xrLabelOcorrenciasFunc.Text = " ***Sem Registro";
            }
            else
            {
                this.xrLabelOcorrenciasFunc.Text = string.Empty;
            }
        }

        private void DetailOcorrMedicas_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DetailReportBand Detail = (DetailReportBand)sender;
            if (Detail.RowCount == 0)
            {
                this.xrLabelOcorrenciasSaude.Text = " ***Sem Registro";
            }
            else
            {
                this.xrLabelOcorrenciasSaude.Text = string.Empty;
            }
        }

        private void DetailReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DetailReportBand Detail = (DetailReportBand)sender;
            if (Detail.RowCount == 0)
            {
                this.xrLabelMovFunc.Text = " ***Sem Registro";
            }
            else
            {
                this.xrLabelMovFunc.Text = string.Empty;
            }
        }

        private void detailReportBand1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DetailReportBand Detail = (DetailReportBand)sender;
            if (Detail.RowCount == 0)
            {
                this.xrLabelEvolFunc.Text = " ***Sem Registro";
            }
            else
            {
                this.xrLabelEvolFunc.Text = string.Empty;
            }
        }

        private void detailReportBand2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DetailReportBand Detail = (DetailReportBand)sender;
            if (Detail.RowCount == 0)
            {
                this.xrLabelRegFreq.Text = " ***Sem Registro";
            }
            else
            {
                this.xrLabelRegFreq.Text = string.Empty;
            }
        }


        }

}




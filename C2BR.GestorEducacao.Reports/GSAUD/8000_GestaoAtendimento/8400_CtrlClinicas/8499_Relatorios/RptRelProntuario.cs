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

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8400_CtrlClinicas._8499_Relatorios
{
    public partial class RptRelProntuario : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptRelProntuario()
        {
            InitializeComponent();
        }


        #region Init Report


        #region Init Report

        public int InitReport(string parametros,
                              string infos,
                              int coEmp,
                              int CoUnidade,
                              int coAlu,
                              string ClassFuncio,
                              int CoProfissional,
                              string Status,
                              string dataIni,
                              string dataFim,
                              string Titulo
            )
        {
            try
            {
                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.Parametros = parametros;
                // Instancia o header do relatorio

                if (Titulo == "")
                {
                    lblTitulo.Text = "-";
                }
                else
                {
                    lblTitulo.Text = Titulo.ToUpper();
                }

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                this.BaseInit(header);

                //var resAnamnese = (from tbs400 in TBS400_PRONT_MASTER.RetornaTodosRegistros()
                //                   join tbs401 in TBS401_PRONT_INTENS.RetornaTodosRegistros() on tbs400.ID_PRONT_MASTER equals tbs401.TBS400_PRONT_MASTER.ID_PRONT_MASTER
                //                   join c in TB03_COLABOR.RetornaTodosRegistros() on tbs401.CO_EMP_CADAS equals c.CO_EMP
                //                   where (tbs400.NU_REGIS == ClassFuncio)
                //                   select new Anamnese
                //                   {
                //                       Data = tbs401.DT_CADAS.Value,
                //                       Descricao = tbs401.DE_DESC,
                //                       Profissional = c.NO_COL,
                //                       Classif = c.CO_CLASS_PROFI,
                //                   }).ToList();

                var resAnamnese = (from tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros()
                                   where tbs390.TB07_ALUNO.CO_ALU == coAlu
                                         && tbs390.DT_CADAS >= DateTime.Parse(dataIni) && tbs390.DT_CADAS <= DateTime.Parse(dataFim)
                                   select new Anamnese
                                   {
                                       Data = tbs390.DT_CADAS,
                                       Descricao = tbs390.DE_HDA,
                                       coCol = tbs390.CO_COL_CADAS
                                   }).ToList();


                var EvolucaoAtendimento = (from tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros()
                                           join c in TB03_COLABOR.RetornaTodosRegistros() on tbs390.CO_EMP_CADAS equals c.CO_EMP
                                           where (tbs390.NU_REGIS == ClassFuncio)
                                           select new EvolucaoAtendimento
                                           {
                                               Data = tbs390.DT_CADAS,
                                               AcaoRealizada = tbs390.DE_ACAO_REALI,
                                               Profissional = c.NO_COL,
                                               classif = c.CO_CLASS_PROFI,
                                           }).ToList();


                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros().Where(a => a.CO_ALU == coAlu)
                           join c in TB905_BAIRRO.RetornaTodosRegistros() on tb07.TB905_BAIRRO.CO_BAIRRO equals c.CO_BAIRRO
                           join ci in TB904_CIDADE.RetornaTodosRegistros() on c.CO_CIDADE equals ci.CO_CIDADE
                           select new Colaborador
                            {
                                Apelido = tb07.NO_APE_ALU,
                                Nome = tb07.NO_ALU,
                                EstadoCivil = tb07.CO_ESTADO_CIVIL,
                                Sexo = tb07.CO_SEXO_ALU,
                                DtNascimento = tb07.DT_NASC_ALU.Value,
                                DtCadastro = tb07.DT_CADA_ALU.Value,
                                Email = tb07.NO_WEB_ALU == null ? "-" : tb07.NO_WEB_ALU == "" ? "-" : tb07.NO_WEB_ALU,
                                Telefone = tb07.NU_TELE_RESI_ALU,
                                Celular = tb07.NU_TELE_CELU_ALU == null ? "-" : tb07.NU_TELE_CELU_ALU == "" ? "-" : tb07.NU_TELE_CELU_ALU,
                                NomeMae = tb07.NO_MAE_ALU,
                                NomePai = tb07.NO_PAI_ALU,
                                CPF = tb07.NU_CPF_ALU == null ? "-" : tb07.NU_CPF_ALU == "" ? "-" : tb07.NU_CPF_ALU,
                                RG = tb07.CO_RG_ALU == null ? "-" : tb07.CO_RG_ALU == "" ? "-" : tb07.CO_RG_ALU,
                                RGData = tb07.DT_EMIS_RG_ALU, // == null ?   : tb07.DT_EMIS_RG_ALU,
                                RGOrgao = tb07.CO_ORG_RG_ALU == null ? "-" : tb07.CO_ORG_RG_ALU == "" ? "-" : tb07.CO_ORG_RG_ALU,
                                RGUF = tb07.CO_ESTA_RG_ALU == null ? "-" : tb07.CO_ESTA_RG_ALU == "" ? "-" : tb07.CO_ESTA_RG_ALU,
                                Titulo = tb07.NU_TIT_ELE == null ? "-" : tb07.NU_TIT_ELE == "" ? "-" : tb07.NU_TIT_ELE,
                                TituloSessao = tb07.NU_SEC_ELE == null ? "-" : tb07.NU_SEC_ELE == "" ? "-" : tb07.NU_SEC_ELE,
                                TituloZona = tb07.NU_ZONA_ELE == null ? "-" : tb07.NU_ZONA_ELE == "" ? "-" : tb07.NU_ZONA_ELE,
                                Deficiencia = tb07.TP_DEF == null ? "-" : tb07.TP_DEF == "" ? "-" : tb07.TP_DEF,
                                Imagem = tb07.Image.ImageStream,
                                Endereco = new Endereco()
                                                {
                                                    Bairro = tb07.TB905_BAIRRO.NO_BAIRRO == null ? "-" : tb07.TB905_BAIRRO.NO_BAIRRO == "" ? "-" : tb07.TB905_BAIRRO.NO_BAIRRO,
                                                    CEP = tb07.CO_CEP_ALU == null ? "-" : tb07.CO_CEP_ALU == "" ? "-" : tb07.CO_CEP_ALU,//Replace("-", ""),  
                                                    Cidade = ci.NO_CIDADE == null ? "-" : ci.NO_CIDADE == "" ? "-" : ci.NO_CIDADE,
                                                    Complemento = tb07.DE_COMP_ALU == null ? "-" : tb07.DE_COMP_ALU == "" ? "-" : tb07.DE_COMP_ALU,
                                                    Estado = tb07.CO_ESTA_ALU == null ? "-" : tb07.CO_ESTA_ALU == "" ? "-" : tb07.CO_ESTA_ALU,
                                                    Logradouro = tb07.DE_ENDE_ALU == null ? "-" : tb07.DE_ENDE_ALU == "" ? "-" : tb07.DE_ENDE_ALU,
                                                    Numero = tb07.NU_ENDE_ALU,
                                                },

                            }).ToList();

                if (res.Count == 0)
                    return -1;
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

        #endregion

        #endregion


        #region Classe Colaborador do Relatorio

        public class Colaborador
        {
            public string NomeMae { get; set; }
            public string NomePai { get; set; }
            public string Nome { get; set; }
            public string CPF { get; set; }
            public string RG { get; set; }
            public DateTime? RGData { get; set; }
            public string RGOrgao { get; set; }
            public string RGUF { get; set; }
            public string Titulo { get; set; }
            public string TituloSessao { get; set; }
            public string TituloZona { get; set; }



            public string _matricula;
            public string Matricula
            {
                get { return this._matricula.Format(TipoFormat.MatriculaColaborador); }
                set { this._matricula = value; }
            }

            public DateTime? DtNascimento { get; set; }
            public string DtNascStr
            {
                get
                {
                    DateTime dt = DateTime.Now;
                    return string.Format("{0:d} ({1})", DtNascimento, Funcoes.GetIdade(dt));
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

            public DateTime? DtCadastro { get; set; }



            public string SituacaoLinha2 { get; set; }
            public DateTime? SituacaoLinha3 { get; set; }
            public byte[] Imagem { get; set; }
            public Endereco Endereco { get; set; }
            public List<Anamnese> Ocorrencias { get; set; }

            public List<OcorrenciaSaude> OcorrenciasSaude { get; set; }
            public List<Movimento> Movimentos { get; set; }
            public List<FrequenciaMensal> Frequencias { get; set; }
            public List<EvolucaoFunc> Evolucao { get; set; }
            public List<Anamnese> Anamnese { get; set; }
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

        //--------------------------------------------------------------------------------------------------------------------------------
        public class Anamnese
        {
            public DateTime? Data { get; set; }
            public string data
            {
                get
                {
                    return this.Data.HasValue ? Data.ToString() : "";
                }
            }
            public string Descricao { get; set; }
            public int coCol { get; set; }
            public string Profissional
            {
                get
                {
                    if (coCol > 0)
                    {
                        return TB03_COLABOR.RetornaPeloCoCol(this.coCol).NO_COL;
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
        }

        public class AcaoPlanejada
        {
            public int NumeroAtendimento { get; set; }
            public string classif { get; set; }
            public string Profissional { get; set; }
            public string ItensPlanejado { get; set; }
        }

        public class EvolucaoAtendimento
        {
            public DateTime Data { get; set; }
            public DateTime Hora { get; set; }
            public string classif { get; set; }
            public string Profissional { get; set; }
            public string AcaoRealizada { get; set; }
            public string ClassifDados
            {
                get
                {
                    return Funcoes.GetNomeClassificacaoFuncional(classif, false);
                }
            }
        }

        public class Exames
        {
            public DateTime Data { get; set; }
            public string Tipo { get; set; }
            public string Descricao { get; set; }
            public string classif { get; set; }
            public string Profissional { get; set; }
        }

        public class Medicacao
        {
            public DateTime Data { get; set; }
            public string numeroAtendimento { get; set; }
            public string Descricao { get; set; }
            public string classif { get; set; }
            public string Profissional { get; set; }
        }

        public class Agenda
        {
            public DateTime Data { get; set; }
            public DateTime Hora { get; set; }
            public string NumeroAtendimento { get; set; }
            public string classif { get; set; }
            public string Profissional { get; set; }
            public string Stutus { get; set; }
        }

        //--------------------------------------------------------------------------------------------------------------------------------

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

            public string TipoDesc
            {
                get
                {
                    if (this.Tipo == "MI")
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
                        return "-";
                    }

                }
            }

            public string DescMotivo
            {
                get
                {
                    if (this.Motivo == "TEX")
                        return "Transferência Externa";
                    else if (this.Motivo == "DIS")
                        return "Disponibilidade";
                    else if (this.Motivo == "APO")
                        return "Atividade Pontual";
                    else if (this.Motivo == "OUT")
                        return "Outros";
                    else if (this.Motivo == "PRO")
                        return "Promoção";
                    else if (this.Motivo == "TIN")
                        return "Transferência Interna";
                    else if (this.Motivo == "DIS")
                        return "Disponibilidade";
                    else if (this.Motivo == "APO")
                        return "Atividade Pontual";
                    else if (this.Motivo == "FER")
                        return "Férias";
                    else if (this.Motivo == "LME")
                        return "Licença Médica";
                    else if (this.Motivo == "LMA")
                        return "Licença Maternidade";
                    else if (this.Motivo == "LPA")
                        return "Licença Paternidade";
                    else if (this.Motivo == "LPR")
                        return "Licença Prêmia";
                    else if (this.Motivo == "LFU")
                        return "Licença Funcional";
                    else if (this.Motivo == "OLI")
                        return "Outras Licenças";
                    else if (this.Motivo == "DEM")
                        return "Demissão";
                    else if (this.Motivo == "ECO")
                        return "Encerramento Contrato";
                    else if (this.Motivo == "AFA")
                        return "Afastamento";
                    else if (this.Motivo == "SUS")
                        return "Suspensão";
                    else if (this.Motivo == "CAP")
                        return "Capacitação";
                    else if (this.Motivo == "TRE")
                        return "Treinamento";
                    else if (this.Motivo == "MOU")
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
            public DateTime DtFim { get; set; }
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
                    if (this.DtFim == null)
                        return "-";
                    else
                    {
                        return DtFim.ToString("dd/MM/yy");
                    }
                }
            }
        }

        #endregion
    }
}

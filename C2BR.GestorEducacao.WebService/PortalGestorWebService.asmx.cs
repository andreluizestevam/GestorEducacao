using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;
using System.Data;
using System.Data.Entity;
using System.Web.Services;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.WebService.Models;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Web.Services.Protocols;
using System.Globalization;
using C2BR.GestorEducacao.WebService.Models.ModuloAgenda;
using C2BR.GestorEducacao.WebService.Models.ModuloPaciente;
using C2BR.GestorEducacao.WebService.Models.ModuloColaborador;
using System.Data.Objects;
using C2BR.GestorEducacao.WebService.Models.ModuloFinanceiro;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.WebService.Models.Geral;
namespace C2BR.GestorEducacao.WebService
{
    /// <summary>
    /// Summary description for PortalGestorWebService
    /// </summary>
    [WebService(Namespace = "http://www.c2br.com.br/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PortalGestorWebService : System.Web.Services.WebService
    {
        Autentica rep = new Autentica();
        string msgVazio = "Não há registros de dados (informações) para está  data";
        string msgVazio2 = "Não há registros de dados (informações)";

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string VersaoWebService()
        {
            return new JavaScriptSerializer().Serialize("2015-07-21");
        }

        /// <summary>
        /// Realiza o login no sistema de acordo com login e senha em parâmetro
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns> retorna string com dados do colaborador logado quando, para este, for permitido acesso </returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string RealizaLogin(string Login, string Senha)
        {
            try
            {
                if ((!string.IsNullOrEmpty(Login) || (!string.IsNullOrEmpty(Senha))))
                {
                    string Senhac = LoginAuxili.GerarMD5(Senha);
                    var admUsuario = (from tbs384 in TBS384_USUAR_APP.RetornaTodosRegistros()
                                      join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs384.CO_COL equals tb03.CO_COL into l1
                                      from lcol in l1.DefaultIfEmpty()
                                      join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs384.CO_ALU equals tb07.CO_ALU into l2
                                      from lalu in l2.DefaultIfEmpty()
                                      join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tbs384.CO_RESP equals tb108.CO_RESP into l3
                                      from lresp in l3.DefaultIfEmpty()
                                      join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs384.CO_EMP_CADAS equals tb25.CO_EMP
                                      where tbs384.DE_LOGIN.Equals(Login) && tbs384.DE_SENHA.Equals(Senhac)
                                      select new DadosLogin
                                      {
                                          idUsuarioApp = tbs384.ID_USUAR_APP,
                                          funcCol = lcol.DE_FUNC_COL,
                                          CodUsuario = (tbs384.CO_TIPO == "G" || tbs384.CO_TIPO == "C" ? lcol.CO_COL : tbs384.CO_TIPO == "R" ? lresp.CO_RESP : lalu.CO_ALU),
                                          desLogin = tbs384.DE_LOGIN,
                                          colabNome = tbs384.NM_USUAR,
                                          colabApeli = tbs384.NM_APELI_USUAR,
                                          co_class_funcion = lcol.CO_CLASS_PROFI,
                                          Co_emp = tb25.CO_EMP,
                                          Unidade = tb25.sigla,
                                          Senha = tbs384.DE_SENHA,
                                          CO_TIPO_USER = tbs384.CO_TIPO,
                                          //ORG_CODIGO_ORGAO = tb25.CodInstituicao,
                                      }).FirstOrDefault();

                    if (admUsuario != null)
                        return new JavaScriptSerializer().Serialize(admUsuario);
                    else
                        throw new Exception("Usuário ou Senha inválido, por favor tente novamente.");
                }
                else
                    throw new Exception("Senha ou login não Informados");
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível realizar esta operação " + Environment.NewLine + "Informação:" + ex.Message);
            }
        }

        #region Carregamentos

        /// <summary>
        /// Carrega todos os pacientes que possuem agenda em aberto com o colaborador recebido como parâmetro
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns> retorna string com dados do colaborador logado quando, para este, for permitido acesso </returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaPacientes(string coCol, string idUsuarioApp, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                if (string.IsNullOrEmpty(coCol))
                    throw new Exception("Falha na autenticação com o web Service");

                int IcoCol = int.Parse(coCol);
                string tipo = TBS384_USUAR_APP.RetornaPelaChavePrimaria(int.Parse(idUsuarioApp)).CO_TIPO;

                //Carregamento de pacientes para os Colaboradores (pacientes associados à ele)
                if (tipo == "C")
                {
                    #region Colaborador
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                               //Situações diferentes de cancelado e realizado
                               where tbs174.CO_COL == IcoCol
                                     && tb07.CO_SITU_ALU == "A"
                               select new { tb07.CO_ALU, tb07.NO_ALU }).DistinctBy(w => w.CO_ALU).OrderBy(w => w.NO_ALU).ToList();

                    if (res != null)
                        return new JavaScriptSerializer().Serialize(res);
                    else
                        throw new Exception(msgVazio);

                    #endregion
                }
                else if (tipo == "G") //Todos os pacientes ativos na base
                {
                    #region Gestor

                    var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                               where tb07.CO_SITU_ALU == "A"
                               select new { tb07.CO_ALU, tb07.NO_ALU }).DistinctBy(w => w.CO_ALU).OrderBy(w => w.NO_ALU).ToList();

                    if (res != null)
                        return new JavaScriptSerializer().Serialize(res);
                    else
                        throw new Exception(msgVazio);

                    #endregion
                }
                else if (tipo == "R") //Os pacientes desse responsável
                {
                    #region Responsável

                    var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                               where tb07.TB108_RESPONSAVEL.CO_RESP == IcoCol
                               && tb07.CO_SITU_ALU == "A"
                               select new { tb07.CO_ALU, tb07.NO_ALU }).DistinctBy(w => w.CO_ALU).OrderBy(w => w.NO_ALU).ToList();

                    if (res != null)
                        return new JavaScriptSerializer().Serialize(res);
                    else
                        throw new Exception(msgVazio);

                    #endregion
                }
                else
                    throw new Exception("O usuário logado não é de nenhum tipo aceitável!");
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// Carrega todos os colaboradores ativos
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns> retorna string com lista de colaboradores ativos</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaColaboradores(string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                           where tb03.CO_SITU_COL == "ATI"
                           select new
                           {
                               NO_COL = tb03.NO_COL,
                               CO_COL = tb03.CO_COL,
                               CO_CLASS_PROFI = tb03.CO_CLASS_PROFI
                           }).OrderBy(w => w.NO_COL).ToList();
                if (res != null)
                    return new JavaScriptSerializer().Serialize(res);
                else
                    throw new Exception(msgVazio);

            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }
        /// <summary>
        /// Carrega todos os colaboradores ativos
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns> retorna string com lista de colaboradores ativos</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaColaboradoresClassificacoesFuncionais(string idUsuarioApp, string CoClasseProfi, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");
                var userApp = TBS384_USUAR_APP.RetornaPelaChavePrimaria(int.Parse(idUsuarioApp));
                if (userApp.CO_TIPO == "C")
                {
                    var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                               where tb03.CO_COL == userApp.CO_COL
                               select new Colaborador
                               {
                                   No_Col = tb03.NO_COL,
                                   Co_col = tb03.CO_COL,
                                   Co_Class_Profi = tb03.CO_CLASS_PROFI,
                                   Numero = 1,
                               }).ToList();
                    if (res != null)
                    {
                        Colaborador repS = new Colaborador();
                        repS.No_Col = "Selecione...";
                        repS.Co_col = 0;
                        repS.Numero = 0;
                        res.Add(repS);
                        return new JavaScriptSerializer().Serialize(res.OrderBy(a => a.Numero).ToList());

                    }


                    else
                        throw new Exception(msgVazio);
                }
                else
                {
                    var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                               where tb03.CO_SITU_COL == "ATI" && tb03.CO_CLASS_PROFI == CoClasseProfi
                               select new Colaborador
                               {
                                   No_Col = tb03.NO_COL,
                                   Co_col = tb03.CO_COL,
                                   Co_Class_Profi = tb03.CO_CLASS_PROFI,
                                   Numero = 1,
                               }).OrderBy(w => w.No_Col).ToList();
                    if (res != null)
                    {
                        Colaborador repS = new Colaborador();
                        repS.No_Col = "Selecione...";
                        repS.Co_col = 0;
                        repS.Numero = 0;
                        res.Add(repS);
                        return new JavaScriptSerializer().Serialize(res.OrderBy(w => w.No_Col).ToList().OrderBy(a => a.Numero).ToList());

                    }
                    if (res != null)
                        return new JavaScriptSerializer().Serialize(res);
                    else
                        throw new Exception(msgVazio);
                }



            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// Carrega todos os colaboradores ativos
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns> retorna string com lista de colaboradores ativos</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaColaboradoresEspecialidadeBairro(string especialidade, string cidade, string bairro, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                var espec = !String.IsNullOrEmpty(especialidade) ? int.Parse(especialidade) : 0;
                var cid = !String.IsNullOrEmpty(cidade) ? int.Parse(cidade) : 0;
                var bai = !String.IsNullOrEmpty(bairro) ? int.Parse(bairro) : 0;

                var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                           where tb03.CO_SITU_COL == "ATI"
                           && (cid != 0 ? tb03.CO_CIDADE == cid : true)
                           && (bai != 0 ? tb03.CO_BAIRRO == bai : true)
                           && (espec != 0 ? tb03.CO_ESPEC == espec : true)
                           select new
                           {
                               NO_COL = tb03.NO_COL,
                               CO_COL = tb03.CO_COL,
                               CO_CLASS_PROFI = tb03.CO_CLASS_PROFI
                           }).OrderBy(w => w.NO_COL).ToList();
                if (res != null)
                    return new JavaScriptSerializer().Serialize(res);
                else
                    throw new Exception(msgVazio);

            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// Carrega todas as operadoras encontradas na base
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns> </returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaClassificacoesFuncionais(string idUsuarioApp, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");
                var userApp = TBS384_USUAR_APP.RetornaPelaChavePrimaria(int.Parse(idUsuarioApp));
                DropDownList DropDownListClas = new DropDownList();
                AuxiliCarregamentos.CarregaClassificacoesFuncionais(DropDownListClas, false);
                List<ClassificacoesFuncionais> ListaClassificacoesFuncionais = new List<ClassificacoesFuncionais>();
                foreach (ListItem item in DropDownListClas.Items)
                {
                    ClassificacoesFuncionais repClassificacoesFuncionais = new ClassificacoesFuncionais();
                    repClassificacoesFuncionais.Nome = item.Text;
                    repClassificacoesFuncionais.Valor = item.Value;
                    ListaClassificacoesFuncionais.Add(repClassificacoesFuncionais);
                }
                //Se for um responsável
                if (userApp.CO_TIPO == "R")
                {
                    if (ListaClassificacoesFuncionais != null)
                        return new JavaScriptSerializer().Serialize(ListaClassificacoesFuncionais);
                    else
                        throw new Exception(msgVazio);

                }
                else if (userApp.CO_TIPO == "P") //Se for um paciente
                {
                    if (ListaClassificacoesFuncionais != null)
                        return new JavaScriptSerializer().Serialize(ListaClassificacoesFuncionais);
                    else
                        throw new Exception(msgVazio);
                }
                else if (userApp.CO_TIPO == "C")
                {
                    var res = TB03_COLABOR.RetornaTodosRegistros().Where(a => a.CO_COL == userApp.CO_COL).SingleOrDefault();

                    List<ClassificacoesFuncionais> Lista = new List<ClassificacoesFuncionais>();
                    ClassificacoesFuncionais repClassi = new ClassificacoesFuncionais();
                    repClassi = ListaClassificacoesFuncionais.Where(a => a.Valor == res.CO_CLASS_PROFI).SingleOrDefault();
                    Lista.Add(repClassi);
                    if (repClassi != null)
                    {
                        ClassificacoesFuncionais Classif = new ClassificacoesFuncionais();
                        Classif.Nome = "Selecione...";
                        Classif.Valor = "";
                        Lista.Add(Classif);
                        return new JavaScriptSerializer().Serialize(Lista.OrderBy(a => a.Valor));
                    }
                    else
                        throw new Exception(msgVazio);
                }
                else
                {


                    if (ListaClassificacoesFuncionais != null)
                        return new JavaScriptSerializer().Serialize(ListaClassificacoesFuncionais);
                    else
                        throw new Exception(msgVazio);
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// Carrega todas as operadoras encontradas na base
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns> retorna string com dados do colaborador logado quando, para este, for permitido acesso </returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaOperadoras(string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                DropDownList DropDownList = new DropDownList();
                AuxiliCarregamentos.CarregaOperadorasPlanSaude(DropDownList, false);
                return RetornarListaPadrao(DropDownList);

            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns>  </returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaPlanosSaude(string IdOperadora, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                DropDownList DropDownListDados = new DropDownList();
                AuxiliCarregamentos.CarregaPlanosSaude(DropDownListDados, IdOperadora, false);
                List<CarregaDados> ListaCarregaDados = new List<CarregaDados>();
                foreach (ListItem item in DropDownListDados.Items)
                {
                    CarregaDados repListaCarregaDados = new CarregaDados();
                    repListaCarregaDados.Nome = item.Text;
                    repListaCarregaDados.Valor = item.Value;
                    ListaCarregaDados.Add(repListaCarregaDados);
                }
                if (ListaCarregaDados != null)
                    return new JavaScriptSerializer().Serialize(ListaCarregaDados);
                else
                    throw new Exception(msgVazio);

            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaProcedimentos(string IdOperadora, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");
                int idOper = Convert.ToInt32(IdOperadora);
                var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                           where tbs356.CO_SITU_PROC_MEDI == "A"
                           && (idOper != 0 ? tbs356.TB250_OPERA.ID_OPER == idOper : tbs356.TB250_OPERA == null)
                           select new { tbs356.ID_PROC_MEDI_PROCE, CO_PROC_MEDI = tbs356.CO_PROC_MEDI + " - " + tbs356.NM_PROC_MEDI }).OrderBy(w => w.CO_PROC_MEDI).ToList();

                if (res != null)
                    return new JavaScriptSerializer().Serialize(res);
                else
                    throw new Exception(msgVazio);

            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// Carrega todas as operadoras encontradas na base
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns> </returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaDeficiencias(string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                DropDownList DropDownList = new DropDownList();
                AuxiliCarregamentos.CarregaDeficienciasNova(DropDownList, false);
                return RetornarListaPadrao(DropDownList);

            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// Carrega todas as unidades encontradas na base
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns> </returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaUnidades(string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");
                                
                var res = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                           where tb25.CO_SIT_EMP == "A"
                           select new { Nome = tb25.sigla, Valor = tb25.CO_EMP }).Distinct().OrderBy(e => e.Nome).ToList();

                if (res != null)
                {
                    var lista = new List<CarregaDados>();
                    var it = new CarregaDados();
                    it.Nome = "Selecione";
                    it.Valor = "";
                    lista.Add(it);

                    foreach (var i in res)
                    {
                        it = new CarregaDados();
                        it.Nome = i.Nome;
                        it.Valor = i.Valor.ToString();
                        lista.Add(it);
                    }

                    return new JavaScriptSerializer().Serialize(lista);
                }
                else
                    throw new Exception(msgVazio);
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// Carrega todas as unidades encontradas do paciente
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns> </returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaUnidadesPaciente(string coAlu, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                var CO_ALU = !String.IsNullOrEmpty(coAlu) ? int.Parse(coAlu) : 0;

                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tb25.CO_EMP
                           where tbs174.CO_ALU == CO_ALU
                           && tb25.CO_SIT_EMP == "A"
                           select new { NO_UNID = tb25.sigla, UnidadeNome = tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_UNID);

                if (res != null)
                    return new JavaScriptSerializer().Serialize(res);
                else
                    throw new Exception(msgVazio);

            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// Carrega todas as unidades encontradas do paciente
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns> </returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaUnidadesCidadeBairro(string cidade, string bairro, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                var cid = !String.IsNullOrEmpty(cidade) ? int.Parse(cidade) : 0;
                var bai = !String.IsNullOrEmpty(bairro) ? int.Parse(bairro) : 0;

                var res = (from  tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                           where (cid != 0 ? tb25.CO_CIDADE == cid : true)
                           && (bai != 0 ? tb25.CO_BAIRRO == bai : true)
                           && (tb25.CO_SIT_EMP == "A")
                           select new { NO_UNID = tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_UNID);

                if (res != null)
                    return new JavaScriptSerializer().Serialize(res);
                else
                    throw new Exception(msgVazio);

            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// Carrega todas as ufs encontradas na base
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns> </returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaUFs(string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                var res = (from tb74 in TB74_UF.RetornaTodosRegistros()
                           select new { Nome = tb74.CODUF, Valor = tb74.CODUF }).OrderBy(e => e.Nome);

                if (res != null)
                    return new JavaScriptSerializer().Serialize(res);
                else
                    throw new Exception(msgVazio);
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// Carrega todas as ciadedes da uf informada encontradas na base
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns> </returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaCidades(string uf, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                var res = (from tb904 in TB904_CIDADE.RetornaTodosRegistros()
                           where tb904.CO_UF == uf
                           select new { Nome = tb904.NO_CIDADE, Valor = tb904.CO_CIDADE }).ToList();

                if (res != null)
                {
                    var lista = new List<CarregaDados>();
                    var it = new CarregaDados();
                    it.Nome = "Selecione";
                    it.Valor = "";
                    lista.Add(it);

                    foreach (var i in res)
                    {
                        it = new CarregaDados();
                        it.Nome = i.Nome;
                        it.Valor = i.Valor.ToString();
                        lista.Add(it);
                    }

                    return new JavaScriptSerializer().Serialize(lista);
                }
                else
                    throw new Exception(msgVazio);
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// Carrega todos os bairros da uf e cidade informados encontradas na base
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns> </returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaBairros(string uf, string cidade, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                var cid = !String.IsNullOrEmpty(cidade) ? int.Parse(cidade) : 0;
                DropDownList DropDownList = new DropDownList();
                AuxiliCarregamentos.CarregaBairros(DropDownList, uf, cid, true, true);
                var res = (from tb905 in TB905_BAIRRO.RetornaTodosRegistros()
                           where tb905.CO_CIDADE == cid
                           && tb905.CO_UF == uf
                           select new { Nome = tb905.NO_BAIRRO, Valor = tb905.CO_BAIRRO }).OrderBy(w => w.Nome).ToList();

                if (res != null)
                {
                    var lista = new List<CarregaDados>();
                    var it = new CarregaDados();
                    it.Nome = "Todos";
                    it.Valor = "";
                    lista.Add(it);

                    foreach (var i in res)
                    {
                        it = new CarregaDados();
                        it.Nome = i.Nome;
                        it.Valor = i.Valor.ToString();
                        lista.Add(it);
                    }

                    return new JavaScriptSerializer().Serialize(lista);
                }
                else
                    throw new Exception(msgVazio);
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// Carrega todas as especialidades encontradas na base
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns> </returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaEspecialidades(string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                var res = (from tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros()
                           select new { Nome = tb63.NO_ESPECIALIDADE, Valor = tb63.CO_ESPECIALIDADE }).OrderBy(e => e.Nome);

                if (res != null)
                {
                    var lista = new List<CarregaDados>();
                    var it = new CarregaDados();
                    it.Nome = "Selecione";
                    it.Valor = "";
                    lista.Add(it);

                    foreach (var i in res)
                    {
                        it = new CarregaDados();
                        it.Nome = i.Nome;
                        it.Valor = i.Valor.ToString();
                        lista.Add(it);
                    }

                    return new JavaScriptSerializer().Serialize(lista);
                }
                else
                    throw new Exception(msgVazio);
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        private string RetornarListaPadrao(DropDownList DropDownList)
        {
            List<CarregaDados> Lista = new List<CarregaDados>();
            foreach (ListItem item in DropDownList.Items)
            {
                CarregaDados repLista = new CarregaDados();
                repLista.Nome = item.Text;
                repLista.Valor = item.Value;
                Lista.Add(repLista);
            }

            if (Lista != null)
                return new JavaScriptSerializer().Serialize(Lista);
            else
                throw new Exception(msgVazio);
        }

        #endregion

        //=======================>  AGENDA =======================


        /// <summary>
        /// Lista a agenda do dia do profissional recebido como parâmetro com dados do paciente, operadora, horários e situação
        /// </summary>
        /// <param name="DataInicio"></param>
        /// <param name="Co_col"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns>Retorna lista das agendas do dia</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string AgendaDoDia(string DataInicio, string Co_col, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");
                if ((!string.IsNullOrEmpty(DataInicio)))
                {
                    int coCol = int.Parse(Co_col);

                    DateTime DataAgenda = DateTime.Parse(DataInicio);
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU into lalu
                               from IIlalu in lalu.DefaultIfEmpty()
                               join tb250 in TB250_OPERA.RetornaTodosRegistros() on IIlalu.TB250_OPERA.ID_OPER equals tb250.ID_OPER into l1
                               from loper in l1.DefaultIfEmpty()
                               join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tb25.CO_EMP
                               where tbs174.DT_AGEND_HORAR == DataAgenda
                               && tbs174.CO_COL == coCol
                               select new DodosAgenda
                               {
                                   Unidade = tb25.sigla,
                                   AgendaHora = tbs174.HR_AGEND_HORAR,
                                   Paciente = (IIlalu != null ? (IIlalu.NO_APE_ALU != null ? IIlalu.NO_APE_ALU : IIlalu.NO_ALU) : "*** LIVRE"),

                                   //**MAXWELL ALMEIDA, comentado à pedido do cordova.
                                   Operadora = (loper != null ? loper.NM_SIGLA_OPER : " - "),
                                   //Operadora = "*****",
                                   Situacao = tbs174.CO_SITUA_AGEND_HORAR,
                                   IdAgenda = tbs174.ID_AGEND_HORAR,
                                   agendaConfirm = tbs174.FL_CONF_AGEND,
                                   agendaEncamin = tbs174.FL_AGEND_ENCAM,
                                   //}).OrderBy(w => w.hora).ToList();
                               }).DistinctBy(w => w.IdAgenda).ToList();

                    if (res != null)
                    {
                        var res2 = res.OrderBy(w => w.Data).ThenBy(w => w.hora).ToList();
                        return new JavaScriptSerializer().Serialize(res2);
                    }
                    else
                        throw new Exception(msgVazio);

                }
                else
                    throw new Exception("Por favor informar a data da agenda");
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }

        }


        /// <summary>
        /// Lista a agenda de 30 dias apartir do dia recebido como parâmetro, devidamente identificando aqueles horários que estiverem livres
        /// </summary>
        /// <param name="DataInicio"></param>
        /// <param name="Co_col"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns>Retorna lista identificada</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string HorariosLivresSemana(string DataInicio, string Co_col, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                if (string.IsNullOrEmpty(DataInicio))
                    throw new Exception("A Data de Início deve ser informada!");

                var ctx = GestorEntities.CurrentContext;
                if ((!string.IsNullOrEmpty(DataInicio)))
                {
                    int Co_Col = int.Parse(Co_col);
                    DateTime dtInicial = DateTime.Parse(DataInicio);
                    string dataFim = (dtInicial.AddDays(30).ToString());
                    DateTime dtFinal = Convert.ToDateTime(dataFim);
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tb25.CO_EMP
                               join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                               where tbs174.DT_AGEND_HORAR >= dtInicial
                               && tbs174.DT_AGEND_HORAR <= dtFinal
                               && tb03.CO_COL == Co_Col
                               && tbs174.CO_ALU == null
                               select new DodosAgenda
                               {
                                   Data = tbs174.DT_AGEND_HORAR,
                                   AgendaHora = tbs174.HR_AGEND_HORAR,
                                   Unidade = tb25.sigla,
                               }).OrderBy(p => p.Data).ToList();

                    if (res == null)
                        throw new Exception(msgVazio);

                    var res2 = res.OrderBy(w => w.Data).ThenBy(w => w.hora).ToList();

                    return new JavaScriptSerializer().Serialize(res2);
                }
                else
                {
                    throw new Exception("Por favor informar a data da agenda");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// Lista a agenda de 30 dias apartir do dia recebido como parâmetro, devidamente identificando aqueles horários que estiverem livres
        /// </summary>
        /// <param name="DataInicio"></param>
        /// <param name="Co_col"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns>Retorna lista identificada</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string MinhaAgendaResponsavelPaciente(string DataInicio, string Co_alu, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                if (string.IsNullOrEmpty(DataInicio))
                    throw new Exception("A Data de Início deve ser informada!");

                var ctx = GestorEntities.CurrentContext;
                if ((!string.IsNullOrEmpty(DataInicio)))
                {
                    //int Co_Col = int.Parse(Co_col);
                    int Co_Alu = int.Parse(Co_alu);
                    DateTime dtInicial = DateTime.Parse(DataInicio);
                    string dataFim = (dtInicial.AddDays(60).ToString());
                    DateTime dtFinal = Convert.ToDateTime(dataFim);
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tb25.CO_EMP
                               join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                               where tbs174.DT_AGEND_HORAR >= dtInicial
                               && tbs174.DT_AGEND_HORAR <= dtFinal
                                   //&& tb03.CO_COL == Co_Col
                               && tbs174.CO_ALU == Co_Alu
                               select new DodosAgenda
                               {
                                   Data = tbs174.DT_AGEND_HORAR,
                                   AgendaHora = tbs174.HR_AGEND_HORAR,
                                   Unidade = tb25.sigla,
                                   UnidadeNome = tb25.NO_FANTAS_EMP,
                                   CoClassProf = tb03.CO_CLASS_PROFI,
                                   Profissional = !String.IsNullOrEmpty(tb03.NO_APEL_COL) ? tb03.NO_APEL_COL : tb03.NO_COL,
                                   Especialidade_ = tb03.CO_ESPEC
                               }).OrderBy(p => p.Data).ToList();

                    if (res == null)
                        throw new Exception(msgVazio);

                    var res2 = res.OrderBy(w => w.Data).ThenBy(w => w.hora).ToList();
                    return new JavaScriptSerializer().Serialize(res2);
                }
                else
                {
                    throw new Exception("Por favor informar a data da agenda");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// Lista os pacientes com atendimentos no mês corrente e seus quantitativos correspondentes
        /// </summary>
        /// <param name="Co_col"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string HistorAtendimentosMes(string Co_col, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                int Co_Col = int.Parse(Co_col);

                var res = (from vw in VW_ATENDIM_MES.RetornaTodosRegistros()
                           where vw.CO_COL == Co_Col
                           select new HistorAtendimentoMes
                           {
                               NO_APE_ALU = (vw.NO_APE_ALU != null ? vw.NO_APE_ALU : vw.NO_ALU),
                               pacienteDataN = vw.DT_NASC_ALU,
                               pacienteSexo = vw.CO_SEXO_ALU,
                               AGE = vw.AGE.Value,
                               CAN = vw.CAN,
                               FAL = vw.FAL.Value,
                               FAJ = vw.FAJ.Value,
                               QTF = vw.QFT.Value,
                               PRE = vw.PRE.Value,
                               AGA = vw.AGA.Value,
                               FAT = vw.FAT.Value,
                           }).OrderBy(w => w.NO_APE_ALU).ToList();

                if (res == null)
                    throw new Exception(msgVazio);

                HistorAtendimentoMes lfin = new HistorAtendimentoMes();
                lfin.NO_APE_ALU = lfin.NO_APE_ALU = "Total do mês";
                lfin.pacienteSexo = " - ";
                lfin.pacienteDataN = (DateTime?)null;
                lfin.AGE = res.Sum(w => w.AGE);
                lfin.CAN = res.Sum(w => w.CAN);
                lfin.FAL = res.Sum(w => w.FAL);
                lfin.FAJ = res.Sum(w => w.FAJ);
                lfin.QTF = res.Sum(w => w.QTF);
                lfin.PRE = res.Sum(w => w.PRE);
                lfin.AGA = res.Sum(w => w.AGA);
                lfin.FAT = res.Sum(w => w.FAT);
                res.Add(lfin);

                return new JavaScriptSerializer().Serialize(res);
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string QuadroAgendaSemanal(string DataInicio, string Co_col, string Login, string Senha)
        {
            try
            {
                //if (rep.Autenticacao(Login, Senha) == false)
                //{
                //    throw new Exception("Falha na autenticação com o web Service");
                //}
                var ctx = GestorEntities.CurrentContext;
                if ((!string.IsNullOrEmpty(DataInicio)))
                {
                    int Co_Col = int.Parse(Co_col);
                    DateTime dtInicial = DateTime.Parse(DataInicio);
                    string dataFim = (dtInicial.AddDays(7).ToString());
                    DateTime dtFinal = Convert.ToDateTime(dataFim);
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                               where tbs174.DT_AGEND_HORAR >= dtInicial && tbs174.DT_AGEND_HORAR <= dtFinal //&& tb03.CO_COL == Co_Col
                               select new
                               {
                                   Data = tbs174.DT_AGEND_HORAR,
                                   hora = tbs174.HR_AGEND_HORAR,
                                   coAlu = tbs174.CO_ALU,
                               }).OrderBy(p => p.Data).ToList();

                    if (res == null)
                        throw new Exception(msgVazio);

                    HorariosLivresPartir hrli = new HorariosLivresPartir();
                    List<DiaDaSemana> Dias = new List<DiaDaSemana>();
                    for (int i = 0; i < 6; i++)
                    {
                        DiaDaSemana dia = new DiaDaSemana();
                        dia.DiaSemana = dtInicial.AddDays(i).ToShortDateString() + " (" + dtInicial.AddDays(i).ToString("dddd", new CultureInfo("pt-BR")).ToUpper() + ")";
                        Dias.Add(dia);
                    }

                    //Distinct das datas encontradas
                    var lstDatas = res.DistinctBy(w => w.Data).OrderBy(p => p.Data).ToList();
                    //----------------------------------------------------------------------------
                    var lsthoras = res.DistinctBy(w => w.hora).OrderBy(p => p.hora).ToList();
                    //Para cada data

                    foreach (var item in lstDatas)
                    {

                        hrli.Data = item.Data.ToShortDateString();
                        //Horarios da data em contexto
                        var lstHoras = res.Where(w => w.Data == item.Data);
                        List<Hora> lshr = new List<Hora>();
                        foreach (var i in lstHoras)
                        {
                            Hora hr = new Hora();
                            hr.HoraRecebe = (i.coAlu.HasValue ? " - " : i.hora);
                            lshr.Add(hr);
                        }

                        hrli.Hora = lshr;
                    }
                    if (hrli != null)
                    {
                        return new JavaScriptSerializer().Serialize(hrli);
                    }
                    else
                    {
                        throw new Exception("Por favor informar a data da agenda");
                    }
                }
                else
                {
                    throw new Exception("Por favor informar a data da agenda");
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Informação:" + ex.Message);
            }

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string QuadroResumoAgenda(DateTime DataInicio, string Co_col, string Login, string Senha)
        {
            try
            {

                if (rep.Autenticacao(Login, Senha) == false)
                {
                    throw new Exception("Falha na autenticação com o web Service");
                }



                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                           join tb250 in TB250_OPERA.RetornaTodosRegistros() on tbs174.TB250_OPERA.CO_OPER equals tb250.CO_OPER
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb07.CO_EMP equals tb25.CO_EMP
                           where tbs174.DT_AGEND_HORAR == DataInicio
                           select new
                           {
                               Unidade = tb25.sigla,
                               AgendaHora = tbs174.HR_AGEND_HORAR,
                               Operadora = tb250.NM_SIGLA_OPER,
                               Situacao = tbs174.CO_SITUA_AGEND_HORAR
                           }).ToList().OrderBy(a => a.AgendaHora);



                if (res != null)
                {
                    return new JavaScriptSerializer().Serialize(res);
                }
                else
                    throw new Exception(msgVazio);
            }

            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }

        }

        /// <summary>
        /// Lista os pacientes de determinado profissional recebido como parâmetro, e quantifica as quantidades 
        /// de atendimentos, faltas, faltas justificadas, sessões faturadas e valor total resultante
        /// </summary>
        /// <param name="Co_col"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string MapaAtendPacientesMensal(string Co_col, string mes, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                if (string.IsNullOrEmpty(mes))
                    throw new Exception("É preciso informar o mês de referência");

                if ((!string.IsNullOrEmpty(Co_col)))
                {
                    int coCol = int.Parse(Co_col);
                    int Imes = int.Parse(mes);
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                               join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                               //Situações diferentes de cancelado e realizado
                               where tbs174.CO_COL == coCol
                               && tbs174.DT_AGEND_HORAR.Month == Imes
                               select new
                               {
                                   nomPaciente = tb07.NO_APE_ALU,
                                   coSitua = tbs174.CO_SITUA_AGEND_HORAR,
                                   flPresen = tbs174.FL_CONF_AGEND,
                                   justifCance = tbs174.FL_JUSTI_CANCE,
                                   valorProfi = tb03.VL_SALAR_COL,
                               }).OrderBy(w => w.nomPaciente).ToList();

                    var l = (from r in res
                             group r by new
                             {
                                 nomPaciente = r.nomPaciente,
                                 valor = r.valorProfi,
                             } into g
                             select new MapaAtendimMensal
                             {
                                 Nome = g.Key.nomPaciente,

                                 qtMes = g.Count().ToString("00"),
                                 QAR_R = g.Where(w => w.flPresen == "S").Count(),
                                 FAL_R = g.Where(w => w.coSitua == "C" && w.justifCance == "N").Count(),
                                 FAJ_R = g.Where(w => w.coSitua == "C" && w.justifCance == "S").Count(),
                                 QCA_R = 00,
                                 valorSessao = g.Key.valor,
                             }).ToList();

                    if (res != null)
                        return new JavaScriptSerializer().Serialize(res);
                    else
                        throw new Exception(msgVazio);
                }
                else
                    throw new Exception("Nenhum profissional informado!");
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string MapaAtendProfissioMensal(string mes, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                if (string.IsNullOrEmpty(mes))
                    throw new Exception("É preciso informar o mês de referência");

                int Imes = int.Parse(mes);
                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                           //Situações diferentes de cancelado e realizado
                           where tbs174.DT_AGEND_HORAR.Month == Imes
                           select new
                           {
                               Nome = tb03.NO_APEL_COL,
                               coSitua = tbs174.CO_SITUA_AGEND_HORAR,
                               flPresen = tbs174.FL_CONF_AGEND,
                               justifCance = tbs174.FL_JUSTI_CANCE,
                               valorProfi = tb03.VL_SALAR_COL,
                           }).OrderBy(w => w.Nome).ToList();

                var l = (from r in res
                         group r by new
                         {
                             nomPaciente = r.Nome,
                             valor = r.valorProfi,
                         } into g
                         select new MapaAtendimMensal
                         {
                             Nome = g.Key.nomPaciente,

                             qtMes = g.Count().ToString("00"),
                             QAR_R = g.Where(w => w.flPresen == "S").Count(),
                             FAL_R = g.Where(w => w.coSitua == "C" && w.justifCance == "N").Count(),
                             FAJ_R = g.Where(w => w.coSitua == "C" && w.justifCance == "S").Count(),
                             QCA_R = 00,
                             valorSessao = g.Key.valor,
                         }).ToList();

                if (res != null)
                    return new JavaScriptSerializer().Serialize(res);
                else
                    throw new Exception(msgVazio);
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }

        }

        /// <summary>
        /// Lista a agenda de 30 dias apartir do dia recebido como parâmetro, devidamente identificando aqueles horários que estiverem cancelados ou com falta
        /// </summary>
        /// <param name="DataInicio"></param>
        /// <param name="Co_col"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns>Retorna lista identificada</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string HorariosCancelados(string DataInicio, string Co_col, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                if ((!string.IsNullOrEmpty(DataInicio)))
                {
                    int Co_Col = !String.IsNullOrEmpty(Co_col) ? int.Parse(Co_col) : 0;
                    DateTime dtInicial = DateTime.Parse(DataInicio);
                    string dataFim = (dtInicial.AddDays(30).ToString());
                    DateTime dtFinal = Convert.ToDateTime(dataFim);
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tb25.CO_EMP
                               join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                               join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                               where tbs174.DT_AGEND_HORAR >= dtInicial
                               && tbs174.DT_AGEND_HORAR <= dtFinal
                               && tb03.CO_COL == Co_Col
                               && tbs174.CO_SITUA_AGEND_HORAR == "C"
                               select new DodosAgenda
                               {
                                   Data = tbs174.DT_AGEND_HORAR,
                                   AgendaHora = tbs174.HR_AGEND_HORAR,
                                   Unidade = tb25.sigla,
                                   faltaJustif = tbs174.FL_JUSTI_CANCE,
                                   Paciente = tb07.NO_APE_ALU,
                                   Situacao = tbs174.CO_SITUA_AGEND_HORAR,
                               }).OrderBy(p => p.Data).ToList();

                    if (res.Count == 0)
                        return new JavaScriptSerializer().Serialize(msgVazio);
                    //throw new Exception(msgVazio);

                    var res2 = res.OrderBy(w => w.Data).ThenBy(w => w.hora).ToList();

                    return new JavaScriptSerializer().Serialize(res2);
                }
                else
                {
                    throw new Exception("Por favor informar a data da agenda");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// Carrega todas as unidades encontradas do paciente
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns> </returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaPeriodoAgendaPaciente(string coAlu, string coEmp, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                var CO_ALU = !String.IsNullOrEmpty(coAlu) ? int.Parse(coAlu) : 0;
                var CO_EMP = !String.IsNullOrEmpty(coEmp) ? int.Parse(coEmp) : 0;

                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                           where (CO_ALU != 0 ? tbs174.CO_ALU == CO_ALU : true)
                           && (CO_EMP != 0 ? tbs174.CO_EMP == CO_EMP : true)
                           select new { tbs174.DT_AGEND_HORAR }).OrderBy(a => a.DT_AGEND_HORAR).ToList();

                if (res != null)
                {
                    var periodo = new
                    {
                        iniPer = res.FirstOrDefault() != null ? res.FirstOrDefault().DT_AGEND_HORAR.ToShortDateString() : DateTime.Now.ToShortDateString(),
                        finPer = res.LastOrDefault() != null ? res.LastOrDefault().DT_AGEND_HORAR.ToShortDateString() : DateTime.Now.ToShortDateString()
                    };

                    return new JavaScriptSerializer().Serialize(periodo);
                }
                else
                    throw new Exception(msgVazio);

            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// Lista a agenda de 30 dias apartir do dia recebido como parâmetro, devidamente identificando aqueles horários que estiverem livres
        /// </summary>
        /// <param name="DataInicio"></param>
        /// <param name="Co_col"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns>Retorna lista identificada</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string HistoricoAgendamento(string DataInicio, string DataFim, string coAlu, string coEmp, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                if (string.IsNullOrEmpty(DataInicio) || string.IsNullOrEmpty(DataFim))
                    throw new Exception("O Período deve ser informado!");

                if ((!string.IsNullOrEmpty(DataInicio)))
                {
                    int Co_Alu = !String.IsNullOrEmpty(coAlu) ? int.Parse(coAlu) : 0;
                    var CO_Emp = !String.IsNullOrEmpty(coEmp) ? int.Parse(coEmp) : 0;
                    DateTime dtInicial = DateTime.Parse(DataInicio);
                    DateTime dtFinal = DateTime.Parse(DataFim);
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tb25.CO_EMP
                               join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                               where (tbs174.DT_AGEND_HORAR >= dtInicial
                               && tbs174.DT_AGEND_HORAR <= dtFinal)
                               && (Co_Alu != 0 ? tbs174.CO_ALU == Co_Alu : true)
                               && (CO_Emp != 0 ? tbs174.CO_EMP == CO_Emp : true)
                               select new DodosAgenda
                               {
                                   Data = tbs174.DT_AGEND_HORAR,
                                   AgendaHora = tbs174.HR_AGEND_HORAR,
                                   Unidade = tb25.sigla,
                                   IdAgenda = tbs174.ID_AGEND_HORAR,
                                   Situacao = tbs174.CO_SITUA_AGEND_HORAR,
                                   agendaConfirm = tbs174.FL_CONF_AGEND,
                                   agendaEncamin = tbs174.FL_AGEND_ENCAM,
                                   faltaJustif = tbs174.FL_JUSTI_CANCE,
                                   CoClassProf = tb03.CO_CLASS_PROFI,
                                   Profissional = !String.IsNullOrEmpty(tb03.NO_APEL_COL) ? tb03.NO_APEL_COL : tb03.NO_COL,
                                   Especialidade_ = tb03.CO_ESPEC
                               }).OrderBy(p => p.Data).ToList();

                    if (res == null)
                        throw new Exception(msgVazio);

                    var res2 = res.OrderBy(w => w.Data).ThenBy(w => w.hora).ToList();
                    return new JavaScriptSerializer().Serialize(res2);
                }
                else
                {
                    throw new Exception("Por favor informar a data da agenda");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// Lista a agenda a apartir do período, especialidade, cidade e bairro recebido como parâmetro, devidamente identificando aqueles horários que estiverem livres
        /// </summary>
        /// <param name="DataInicio"></param>
        /// <param name="Co_col"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns>Retorna lista identificada</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DisponibilidadeEspecialidade(string DataInicio, string DataFim, string especialidade, string cidade, string bairro, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                if (string.IsNullOrEmpty(DataInicio) || string.IsNullOrEmpty(DataFim))
                    throw new Exception("O Período deve ser informado!");

                int espec = !String.IsNullOrEmpty(especialidade) ? int.Parse(especialidade) : 0;
                var cid = !String.IsNullOrEmpty(cidade) ? int.Parse(cidade) : 0;
                var bai = !String.IsNullOrEmpty(bairro) ? int.Parse(bairro) : 0;
                DateTime dtInicial = DateTime.Parse(DataInicio);
                DateTime dtFinal = DateTime.Parse(DataFim);
                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tb25.CO_EMP
                           join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb25.CO_BAIRRO equals tb905.CO_BAIRRO
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                           where (tbs174.DT_AGEND_HORAR >= dtInicial
                           && tbs174.DT_AGEND_HORAR <= dtFinal)
                           && (tbs174.CO_ALU == null || tbs174.CO_SITUA_AGEND_HORAR == "C")
                           && (espec != 0 ? tb03.CO_ESPEC == espec : true)
                           && (cid != 0 ? tb25.CO_CIDADE == cid : true)
                           && (bai != 0 ? tb25.CO_BAIRRO == bai : true)
                           select new DodosAgenda
                           {
                               Data = tbs174.DT_AGEND_HORAR,
                               AgendaHora = tbs174.HR_AGEND_HORAR,
                               Unidade = tb25.sigla,
                               UnidadeNome = tb25.NO_FANTAS_EMP,
                               UnidBairro = tb905.NO_BAIRRO,
                               UnidTelefone = tb25.CO_TEL1_EMP,
                               IdAgenda = tbs174.ID_AGEND_HORAR,
                               coCol = tb03.CO_COL,
                               Profissional = !String.IsNullOrEmpty(tb03.NO_APEL_COL) ? tb03.NO_APEL_COL : tb03.NO_COL,
                           }).OrderBy(p => p.Data).DistinctBy(p => p.coCol).ToList();

                if (res == null)
                    throw new Exception(msgVazio);

                return new JavaScriptSerializer().Serialize(res);
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// Lista a agenda a apartir do profissional, cidade e bairro recebidos como parâmetro, devidamente identificando aqueles horários que estiverem livres
        /// </summary>
        /// <param name="DataInicio"></param>
        /// <param name="Co_col"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns>Retorna lista identificada</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DisponibilidadeProfissional(string profissional, string cidade, string bairro, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                int prof = !String.IsNullOrEmpty(profissional) ? int.Parse(profissional) : 0;
                int cid = !String.IsNullOrEmpty(cidade) ? int.Parse(cidade) : 0;
                var bai = !String.IsNullOrEmpty(bairro) ? int.Parse(bairro) : 0;

                var pro = TB03_COLABOR.RetornaPeloCoCol(prof);

                var res_ = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                            join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb03.CO_EMP equals tb25.CO_EMP
                            where tb03.NU_CPF_COL == pro.NU_CPF_COL
                            && (cid != 0 ? tb25.CO_CIDADE == cid : true)
                            && (bai != 0 ? tb25.CO_BAIRRO == bai : true)
                            select new
                            {
                                tb03.CO_COL,
                                tb03.CO_EMP
                            }).ToList();

                var res = (from ts in res_
                           join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on ts.CO_COL equals tbs174.CO_COL
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on ts.CO_EMP equals tb25.CO_EMP
                           join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb25.CO_BAIRRO equals tb905.CO_BAIRRO
                           where (tbs174.CO_ALU == null || tbs174.CO_SITUA_AGEND_HORAR == "C")
                           select new DodosAgenda
                           {
                               coCol = ts.CO_COL,
                               Data = tbs174.DT_AGEND_HORAR,
                               AgendaHora = tbs174.HR_AGEND_HORAR,
                               Unidade = tb25.sigla,
                               UnidadeNome = tb25.NO_FANTAS_EMP,
                               UnidBairro = tb905.NO_BAIRRO,
                               UnidTelefone = tb25.CO_TEL1_EMP
                           }).OrderBy(p => p.Data).ThenBy(w => w.hora).DistinctBy(r => r.coCol).ToList();
                
                if (res == null)
                    throw new Exception(msgVazio);

                return new JavaScriptSerializer().Serialize(res);
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// Lista a agenda a apartir do dia recebido e unidade como parâmetro, devidamente identificando aqueles horários que estiverem livres
        /// </summary>
        /// <returns>Retorna lista identificada</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DisponibilidadeUnidade(string dtInicio,string unidade, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                var uni = !String.IsNullOrEmpty(unidade) ? int.Parse(unidade) : 0;
                var data = DateTime.Parse(dtInicio);

                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                           where tbs174.CO_EMP == uni && (tbs174.DT_AGEND_HORAR >= data)
                           && (tbs174.CO_ALU == null || tbs174.CO_SITUA_AGEND_HORAR == "C")
                           select new DodosAgenda
                           {
                               coCol = tb03.CO_COL,
                               Data = tbs174.DT_AGEND_HORAR,
                               AgendaHora = tbs174.HR_AGEND_HORAR,
                               Profissional = !String.IsNullOrEmpty(tb03.NO_APEL_COL) ? tb03.NO_APEL_COL : tb03.NO_COL,
                               Especialidade_ = tb03.CO_ESPEC
                           }).OrderBy(p => p.Especialidade_).ThenBy(p => p.Data).ThenBy(p => p.Profissional).DistinctBy(r => r.coCol).ToList();

                if (res == null)
                    throw new Exception(msgVazio);

                return new JavaScriptSerializer().Serialize(res);
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        #region Responsáveis pelo cancelamento

        /// <summary>
        /// Lista os agendamentos ainda em aberto apartir da data atual para o paciente recebido como parâmetro
        /// </summary>
        /// <param name="coAlu"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaAgendasAbertasProfissional(string coAlu, string Co_col, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                if ((!string.IsNullOrEmpty(coAlu)))
                {
                    int IcoAlu = int.Parse(coAlu);
                    int Cocol = int.Parse(Co_col);
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tb25.CO_EMP
                               join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                               where tbs174.CO_ALU == IcoAlu && tbs174.CO_COL == Cocol
                               && tbs174.CO_SITUA_AGEND_HORAR == "A"
                               && EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= DateTime.Now
                               select new AgendasPaciente
                               {
                                   idAgenda = tbs174.ID_AGEND_HORAR,
                                   dt = tbs174.DT_AGEND_HORAR,
                                   hr = tbs174.HR_AGEND_HORAR,
                                   plano = tbs174.DE_ACAO_PLAN,
                                   Numero = 1,
                                   Unidade = tb25.sigla,
                                   CoClassProf = tb03.CO_CLASS_PROFI,
                                   Especialidade_ = tb03.CO_ESPEC,
                                   Profissional = !String.IsNullOrEmpty(tb03.NO_APEL_COL) ? tb03.NO_APEL_COL : tb03.NO_COL
                               }).ToList();

                    if (res != null)
                    {
                        AgendasPaciente repS = new AgendasPaciente();
                        repS.Item = "Selecione a data e horário";
                        repS.Numero = 0;
                        res.Add(repS);
                        //var res2 = res.OrderBy(w => w.dt).ThenBy(w => w.hora).ToList();
                        return new JavaScriptSerializer().Serialize(res.OrderBy(w => w.dt).ThenBy(w => w.hora).ToList().OrderBy(a => a.Numero).ToList());
                    }
                    else
                        return new JavaScriptSerializer().Serialize(msgVazio);
                }
                else
                    throw new Exception("Nenhum paciente informado!");
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }

        }

        /// <summary>
        /// Lista os agendamentos ainda em aberto apartir da data atual para o paciente recebido como parâmetro
        /// </summary>
        /// <param name="coAlu"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaAgendasAbertasResponsavel(string coEmp, string coAlu, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                if ((!string.IsNullOrEmpty(coAlu)))
                {
                    int IcoAlu = !String.IsNullOrEmpty(coAlu) ? int.Parse(coAlu) : 0;
                    var CO_EMP = !String.IsNullOrEmpty(coEmp) ? int.Parse(coEmp) : 0;

                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tb25.CO_EMP
                               join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                               where (IcoAlu != 0 ? tbs174.CO_ALU == IcoAlu : true)
                               && (CO_EMP != 0 ? tbs174.CO_EMP == CO_EMP : true)
                               && (tbs174.CO_SITUA_AGEND_HORAR == "A")
                               && EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= DateTime.Now
                               select new AgendasPaciente
                               {
                                   idAgenda = tbs174.ID_AGEND_HORAR,
                                   dt = tbs174.DT_AGEND_HORAR,
                                   hr = tbs174.HR_AGEND_HORAR,
                                   plano = tbs174.DE_ACAO_PLAN,
                                   Numero = 1,
                                   Unidade = tb25.sigla,
                                   CoClassProf = tb03.CO_CLASS_PROFI,
                                   Especialidade_ = tb03.CO_ESPEC,
                                   Profissional = !String.IsNullOrEmpty(tb03.NO_APEL_COL) ? tb03.NO_APEL_COL : tb03.NO_COL
                               }).ToList();

                    if (res != null)
                    {
                        AgendasPaciente repS = new AgendasPaciente();
                        repS.Item = "Selecione a data e horário";
                        repS.Numero = 0;
                        res.Add(repS);
                        //var res2 = res.OrderBy(w => w.dt).ThenBy(w => w.hora).ToList();
                        return new JavaScriptSerializer().Serialize(res.OrderBy(w => w.dt).ThenBy(w => w.hora).ToList().OrderBy(a => a.Numero).ToList());
                    }
                    else
                        return new JavaScriptSerializer().Serialize(msgVazio);
                }
                else
                    throw new Exception("Nenhum paciente informado!");
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }

        }

        /// <summary>
        /// Retorna as informações de uma determinada consulta recebida como parâmetro
        /// </summary>
        /// <param name="idAgenda"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string InformacoesConsulta(string idAgenda, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                if ((!string.IsNullOrEmpty(idAgenda)))
                {
                    int idAgendaI = int.Parse(idAgenda);
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                               where tbs174.ID_AGEND_HORAR == idAgendaI
                               select new InfosAgendamento
                               {
                                   idAgenda = tbs174.ID_AGEND_HORAR,
                                   dt = tbs174.DT_AGEND_HORAR,
                                   hr = tbs174.HR_AGEND_HORAR,
                                   plano = tbs174.DE_ACAO_PLAN,
                                   coTipoConsulta = tbs174.TP_AGEND_HORAR,
                                   colabNome = tb03.NO_COL,
                                   funcCol = tb03.DE_FUNC_COL,
                                   idPlanej = (tbs174.TBS370_PLANE_AVALI != null ? tbs174.TBS370_PLANE_AVALI.ID_PLANE_AVALI : (int?)null),
                               }).FirstOrDefault();

                    if (res != null)
                        return new JavaScriptSerializer().Serialize(res);
                    else
                        throw new Exception(msgVazio);
                }
                else
                    throw new Exception("Nenhuma agenda informada!");
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }

        }

        /// <summary>
        /// Realiza o cancelamento justificado de uma determinada agenda recebida como parâmetro
        /// </summary>
        /// <param name="idAgenda"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CancelarAgenda(string idAgenda, string idUsuarioApp, string Obser, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                var userApp = TBS384_USUAR_APP.RetornaPelaChavePrimaria(int.Parse(idUsuarioApp));
                if ((!string.IsNullOrEmpty(idAgenda)))
                {
                    TBS174_AGEND_HORAR tbs174ant = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(idAgenda));

                    //Salva o log de alteração de status
                    TBS375_LOG_ALTER_STATUS_AGEND_HORAR tbs375 = new TBS375_LOG_ALTER_STATUS_AGEND_HORAR();
                    tbs375.TBS174_AGEND_HORAR = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(int.Parse(idAgenda));

                    tbs375.FL_JUSTI = "S";
                    tbs375.DE_OBSER = (!string.IsNullOrEmpty(Obser) ? Obser : null);
                    //Se estiver cancelada, vai gerar o log para abertura, senão, para cancelamento
                    tbs375.CO_SITUA_AGEND_HORAR = (tbs174ant.CO_SITUA_AGEND_HORAR == "C" ? "A" : "C");

                    tbs375.FL_TIPO_LOG = "C";
                    tbs375.DT_CADAS = DateTime.Now;
                    tbs375.IP_CADAS = "127.0.0.1"; // Por ora, ip sendo setado na mão

                    int coColPadrao = TB03_COLABOR.RetornaTodosRegistros().FirstOrDefault().CO_COL;

                    //Se for um responsável
                    if (userApp.CO_TIPO == "R")
                    {
                        tbs375.CO_RESP = userApp.CO_RESP;
                        tbs174ant.CO_EMP_SITUA =
                        tbs375.CO_EMP_CADAS = 221; //Empresa padrão
                        tbs174ant.CO_COL_SITUA =
                        tbs375.CO_COL_CADAS = coColPadrao;
                    }
                    else if (userApp.CO_TIPO == "P") //Se for um paciente
                    {
                        tbs375.CO_ALU = userApp.CO_ALU;
                        tbs174ant.CO_EMP_SITUA =
                        tbs375.CO_EMP_CADAS = TB07_ALUNO.RetornaPeloCoAlu(userApp.CO_ALU.Value).CO_EMP;
                        tbs174ant.CO_COL_SITUA =
                        tbs375.CO_COL_CADAS = coColPadrao;
                    }
                    else
                    {
                        tbs174ant.CO_COL_SITUA =
                        tbs375.CO_COL_CADAS = userApp.CO_COL.Value;
                        tbs174ant.CO_EMP_SITUA =
                        tbs375.CO_EMP_CADAS = TB03_COLABOR.RetornaPeloCoCol(userApp.CO_COL.Value).CO_EMP;
                    }

                    //Se estiver cancelado, abre, se não, cancela
                    tbs174ant.CO_SITUA_AGEND_HORAR = (tbs174ant.CO_SITUA_AGEND_HORAR == "C" ? "A" : "C");
                    tbs174ant.DT_SITUA_AGEND_HORAR = DateTime.Now;

                    //Grava as informações de cancelamento
                    #region Grava informações de situação de Cancelamento

                    //Se estiver com status de aberto grava as informações de cancelamento como NULL
                    if (tbs174ant.CO_SITUA_AGEND_HORAR == "A")
                    {
                        tbs174ant.FL_JUSTI_CANCE =
                        tbs174ant.DE_OBSER_CANCE = null;
                        tbs174ant.DT_CANCE = (DateTime?)null;
                        tbs174ant.CO_COL_CANCE =
                        tbs174ant.CO_EMP_CANCE = (int?)null;
                        tbs174ant.IP_CANCE = null;
                    }
                    else //Se estiver com status de cancelado, grava as informações de cancelamento
                    {
                        tbs174ant.FL_JUSTI_CANCE = "S";
                        tbs174ant.DE_OBSER_CANCE = (!string.IsNullOrEmpty(Obser) ? Obser : null);
                        tbs174ant.DT_CANCE = DateTime.Now;
                        tbs174ant.IP_CANCE = "127.0.0.1"; // Por ora, ip sendo setado na mão

                        if (userApp.CO_TIPO == "R" || userApp.CO_TIPO == "P")
                        {
                            tbs174ant.CO_COL_CANCE = coColPadrao;
                            tbs174ant.CO_EMP_CANCE = 221;
                        }
                        else
                        {
                            tbs174ant.CO_COL_CANCE = userApp.CO_COL.Value;
                            tbs174ant.CO_EMP_CANCE = TB03_COLABOR.RetornaPeloCoCol(userApp.CO_COL.Value).CO_EMP;
                        }
                    }

                    #endregion

                    TBS375_LOG_ALTER_STATUS_AGEND_HORAR.SaveOrUpdate(tbs375);
                    TBS174_AGEND_HORAR.SaveOrUpdate(tbs174ant, true);

                    return new JavaScriptSerializer().Serialize("Cancelamento realizado com sucesso!");
                }
                else
                    throw new Exception("Nenhuma agenda informada!");
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idAgenda"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string RealizarAgendamento(string NomePaciente, string SexoPaciente, string NascimentoPaciente,
        string DeficienciaPaciente, string TelCelularPaciente, string TelFixoPaciente, string EmailPaciente,
        string NomeMae, string ResponsavelMae, string NomePai, string ResponsavelPai, string coEmp,
        string idAgenda, string idOper, string idPlano, string TipoDeConsulta,
        string CodClassificacao, string Queixa, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");
                int CO_EMP = Convert.ToInt32(coEmp);

                #region Salva o Usuário na TB07

                TB07_ALUNO tb07;
                tb07 = new TB07_ALUNO();
                tb07.NO_ALU = NomePaciente;
                tb07.NU_CPF_ALU = "";
                tb07.DT_NASC_ALU = DateTime.Now;// Convert.ToDateTime(NascimentoPaciente);
                tb07.CO_SEXO_ALU = SexoPaciente;
                tb07.NU_TELE_CELU_ALU = TelCelularPaciente.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb07.NU_TELE_RESI_ALU = TelFixoPaciente.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb07.NU_TELE_WHATS_ALU = TelFixoPaciente.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                tb07.CO_GRAU_PAREN_RESP = "";
                tb07.NO_EMAIL_PAI = "";
                tb07.CO_EMP = CO_EMP;
                tb07.NO_ENDE_ELET_ALU = EmailPaciente;
                tb07.NO_WEB_ALU = EmailPaciente;
                tb07.NO_MAE_ALU = NomeMae;
                tb07.NO_PAI_ALU = NomePai;
                tb07.TBS387_DEFIC = (!string.IsNullOrEmpty(DeficienciaPaciente) ? TBS387_DEFIC.RetornaPelaChavePrimaria(int.Parse(DeficienciaPaciente)) : null);
                //tb07.FL_PAI_RESP_ATEND = ResponsavelPai;
                //tb07.FL_MAE_RESP_ATEND = ResponsavelMae;
                tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(CO_EMP);
                tb07.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(CO_EMP);
                //Salva os valores para os campos not null da tabela de Usuário
                tb07.CO_SITU_ALU = "A";
                tb07.TP_DEF = DeficienciaPaciente;
                #region trata para criação do nire

                var res = (from tb07pesq in TB07_ALUNO.RetornaTodosRegistros()
                           select new { tb07pesq.NU_NIRE }).OrderByDescending(w => w.NU_NIRE).FirstOrDefault();

                int nir = 0;
                if (res == null)
                {
                    nir = 1;
                }
                else
                {
                    nir = res.NU_NIRE;
                }

                int nirTot = nir + 1;

                #endregion
                tb07.NU_NIRE = nirTot;
                tb07 = TB07_ALUNO.SaveOrUpdate(tb07);
                #endregion

                int IdAgenda = Convert.ToInt32(idAgenda);
                var agend = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(IdAgenda);
                int coAlu = tb07.CO_ALU;
                var resal = TB07_ALUNO.RetornaPeloCoAlu(coAlu);

                agend.TP_AGEND_HORAR = CodClassificacao;
                agend.CO_EMP_ALU = resal.CO_EMP;
                agend.CO_ALU = resal.CO_ALU;
                agend.TP_CONSU = TipoDeConsulta;// TipoControle de consulta
                agend.FL_CONF_AGEND = "N";
                agend.FL_CONFIR_CONSUL_SMS = "N";
                agend.DE_QUEIX_PACIE = Queixa;
                agend.TB250_OPERA = (!string.IsNullOrEmpty(idOper) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(idOper)) : null);
                agend.TB251_PLANO_OPERA = (!string.IsNullOrEmpty(idPlano) ? TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(idPlano)) : null);

                #region Gera Código da Consulta
                string ano = DateTime.Now.Year.ToString().Substring(2, 2);
                var ress = (from tbs174pesq in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                            where tbs174pesq.CO_EMP == CO_EMP && tbs174pesq.NU_REGIS_CONSUL != null
                            select new { tbs174pesq.NU_REGIS_CONSUL }).OrderByDescending(w => w.NU_REGIS_CONSUL).FirstOrDefault();

                string seq;
                int seq2;
                int seqConcat;
                string seqcon;
                if (res == null)
                {
                    seq2 = 1;
                }
                else
                {
                    seq = ress.NU_REGIS_CONSUL.Substring(7, 7);
                    seq2 = int.Parse(seq);
                }
                seqConcat = seq2 + 1;
                seqcon = seqConcat.ToString().PadLeft(7, '0');
                string CoUnidade = Convert.ToString(resal.CO_EMP);
                agend.NU_REGIS_CONSUL = ano + CoUnidade.PadLeft(3, '0') + "CO" + seqcon;
                agend = TBS174_AGEND_HORAR.SaveOrUpdate(agend);
                #endregion
                return new JavaScriptSerializer().Serialize("Agenda  cadastrada com sucesso...");
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idAgenda"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string RealizarAgendamentoPacienteCadastrado(string coAlu, string coEmp,
        string idAgenda, string idOper, string idPlano, string TipoDeConsulta,
        string CodClassificacao, string Queixa, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                int CO_EMP = Convert.ToInt32(coEmp);

                int IdAgenda = Convert.ToInt32(idAgenda);
                var agend = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(IdAgenda);
                int coalu = Convert.ToInt32(coAlu);
                var resal = TB07_ALUNO.RetornaPeloCoAlu(coalu);
                #region trata para criação do nire

                var res = (from tb07pesq in TB07_ALUNO.RetornaTodosRegistros()
                           select new { tb07pesq.NU_NIRE }).OrderByDescending(w => w.NU_NIRE).FirstOrDefault();

                int nir = 0;
                if (res == null)
                {
                    nir = 1;
                }
                else
                {
                    nir = res.NU_NIRE;
                }

                int nirTot = nir + 1;
                #endregion
                agend.TP_AGEND_HORAR = CodClassificacao;
                agend.CO_EMP_ALU = resal.CO_EMP;
                agend.CO_ALU = coalu;
                agend.TP_CONSU = TipoDeConsulta;// TipoControle de consulta
                agend.FL_CONF_AGEND = "N";
                agend.FL_CONFIR_CONSUL_SMS = "N";
                agend.DE_QUEIX_PACIE = Queixa;
                agend.TB250_OPERA = (!string.IsNullOrEmpty(idOper) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(idOper)) : null);
                agend.TB251_PLANO_OPERA = (!string.IsNullOrEmpty(idPlano) ? TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(idPlano)) : null);


                string ano = DateTime.Now.Year.ToString().Substring(2, 2);
                var ress = (from tbs174pesq in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                            where tbs174pesq.CO_EMP == CO_EMP && tbs174pesq.NU_REGIS_CONSUL != null
                            select new { tbs174pesq.NU_REGIS_CONSUL }).OrderByDescending(w => w.NU_REGIS_CONSUL).FirstOrDefault();

                string seq;
                int seq2;
                int seqConcat;
                string seqcon;
                if (res == null)
                {
                    seq2 = 1;
                }
                else
                {
                    seq = ress.NU_REGIS_CONSUL.Substring(7, 7);
                    seq2 = int.Parse(seq);
                }
                seqConcat = seq2 + 1;
                seqcon = seqConcat.ToString().PadLeft(7, '0');
                string CoUnidade = Convert.ToString(resal.CO_EMP);
                agend.NU_REGIS_CONSUL = ano + CoUnidade.PadLeft(3, '0') + "CO" + seqcon;
                agend = TBS174_AGEND_HORAR.SaveOrUpdate(agend);
                return new JavaScriptSerializer().Serialize("Agendamento realizado com sucesso!");
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaDadosPacientesAgenda(string CoAlu, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");
                int CO_ALU = Convert.ToInt32(CoAlu);
                var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                           where tb07.CO_ALU == CO_ALU
                           select new TblAluno
                           {
                               Nome = tb07.NO_ALU,
                               Sexo = tb07.CO_SEXO_ALU,
                               DataNasc = tb07.DT_NASC_ALU,
                               Deficiencia = tb07.TBS387_DEFIC.ID_DEFIC != null ? tb07.TBS387_DEFIC.ID_DEFIC : 0,
                               TelefoneCelular = tb07.NU_TELE_CELU_ALU != null && tb07.NU_TELE_CELU_ALU != "" ? tb07.NU_TELE_CELU_ALU.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "--",
                               FixoCelular = tb07.NU_TELE_RESI_ALU != null && tb07.NU_TELE_RESI_ALU != "" ? tb07.NU_TELE_RESI_ALU.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "--",
                               Email = tb07.NO_ENDE_ELET_ALU,
                               NomeMae = tb07.NO_MAE_ALU,
                               NomePai = tb07.NO_PAI_ALU,
                               ResponsavelPai = tb07.FL_PAI_RESP_ATEND,
                               ResponsavelMae = tb07.FL_MAE_RESP_ATEND,
                           }).SingleOrDefault();

                if (res == null)
                    return new JavaScriptSerializer().Serialize(msgVazio2);
                return new JavaScriptSerializer().Serialize(res);

            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DataInicio"></param>
        /// <param name="Co_col"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaAgendasHorariosPorProfissional(string Co_col, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                if (string.IsNullOrEmpty(Co_col))
                    throw new Exception("O colaborador não foi identificado!");
                //var ctx = GestorEntities.CurrentContext;
                int Co_Col = int.Parse(Co_col);

                DateTime dtInicial = DateTime.Now;
                string dataFim = (dtInicial.AddDays(60).ToString());
                DateTime dtFinal = Convert.ToDateTime(dataFim);
                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs174.CO_EMP equals tb25.CO_EMP
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                           where tbs174.DT_AGEND_HORAR >= dtInicial
                                 && tbs174.DT_AGEND_HORAR <= dtFinal
                                 && tb03.CO_COL == Co_Col
                                 && tbs174.CO_ALU == null
                           select new DodosAgenda
                           {
                               IdAgenda = tbs174.ID_AGEND_HORAR,
                               Data = tbs174.DT_AGEND_HORAR,
                               AgendaHora = tbs174.HR_AGEND_HORAR,
                               Unidade = tb25.sigla,
                           }).OrderBy(p => p.Data).ToList();

                if (res == null)
                    throw new Exception(msgVazio);

                var res2 = res.OrderBy(w => w.Data).ThenBy(w => w.hora).ToList();

                return new JavaScriptSerializer().Serialize(res2);

            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }


        //=======================>  AGENDA =======================

        //=======================>  PACIENTES =======================

        #region Pacientes

        /// <summary>
        /// Lista os pacientes pelos quais o profissional recebido como parâmetro é responsável (Identifica verificando
        /// os pacientes que possuem agenda com este profissional com situação diferente de realizado ou cancelado/falta;
        /// </summary>
        /// <param name="DataInicio"></param>
        /// <param name="Co_col"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string PacientesProfissional(string Co_col, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");
                if ((!string.IsNullOrEmpty(Co_col)))
                {
                    int coCol = int.Parse(Co_col);
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                               //Situações diferentes de cancelado e realizado
                               where tbs174.CO_SITUA_AGEND_HORAR != "R" && tbs174.CO_SITUA_AGEND_HORAR != "C"
                               && tbs174.CO_COL == coCol
                               select new PacientesProfissional
                               {
                                   coAlu = tb07.CO_ALU,
                                   FL_MAE_RESP = tb07.FL_MAE_RESP_ATEND,
                                   FL_PAI_RESP = tb07.FL_PAI_RESP_ATEND,
                                   NO_MAE = tb07.NO_MAE_ALU,
                                   NO_PAI = tb07.NO_PAI_ALU,
                                   NO_RESP = tb07.TB108_RESPONSAVEL.NO_RESP,
                                   pacienteDataN = tb07.DT_NASC_ALU,
                                   pacienteNome = (tb07.NO_APE_ALU != null ? tb07.NO_APE_ALU : tb07.NO_ALU),
                                   pacienteSexo = (tb07.CO_SEXO_ALU != null ? tb07.CO_SEXO_ALU : " - "),
                                   TELEFONE_MAE = tb07.NU_TEL_MAE,
                                   TELEFONE_PAI = tb07.NU_TEL_PAI,
                                   TELEFONE_CEL = tb07.TB108_RESPONSAVEL.NU_TELE_CELU_RESP,
                                   CO_COL = coCol,
                               }).DistinctBy(w => w.coAlu).OrderBy(w => w.pacienteNome).ToList();

                    if (res != null)
                        return new JavaScriptSerializer().Serialize(res);
                    else
                        throw new Exception(msgVazio);
                }
                else
                    throw new Exception("Nenhum profissional informado!");
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }

        }

        /// <summary>
        /// Lista os pacientes pelos quais o profissional recebido como parâmetro é responsável (Identifica verificando
        /// os pacientes que possuem agenda com este profissional com situação diferente de realizado ou cancelado/falta)
        /// que estejam;
        /// </summary>
        /// <param name="DataInicio"></param>
        /// <param name="Co_col"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string PacientesAniversariantes(string Co_col, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                if ((!string.IsNullOrEmpty(Co_col)))
                {
                    int coCol = int.Parse(Co_col);
                    int mesCorre = DateTime.Now.Month;
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                               //Situações diferentes de cancelado e realizado
                               where tbs174.CO_SITUA_AGEND_HORAR != "R" && tbs174.CO_SITUA_AGEND_HORAR != "C"
                               && tbs174.CO_COL == coCol
                               && (tb07.DT_NASC_ALU.HasValue ? tb07.DT_NASC_ALU.Value.Month == mesCorre : 0 == 0)
                               select new PacientesProfissional
                               {
                                   coAlu = tb07.CO_ALU,
                                   FL_MAE_RESP = tb07.FL_MAE_RESP_ATEND,
                                   FL_PAI_RESP = tb07.FL_PAI_RESP_ATEND,
                                   NO_MAE = tb07.NO_MAE_ALU,
                                   NO_PAI = tb07.NO_PAI_ALU,
                                   NO_RESP = tb07.TB108_RESPONSAVEL.NO_RESP,
                                   pacienteDataN = tb07.DT_NASC_ALU,
                                   pacienteNome = (tb07.NO_APE_ALU != null ? tb07.NO_APE_ALU : tb07.NO_ALU),
                                   pacienteSexo = tb07.CO_SEXO_ALU,
                                   TELEFONE_MAE = tb07.NU_TEL_MAE,
                                   TELEFONE_PAI = tb07.NU_TEL_PAI,
                                   TELEFONE_CEL = tb07.TB108_RESPONSAVEL.NU_TELE_CELU_RESP,
                                   CO_COL = coCol,
                               }).DistinctBy(w => w.coAlu).ToList();

                    res = res.OrderBy(w => w.diaAniversarioINT).ToList();

                    if (res != null)
                        return new JavaScriptSerializer().Serialize(res);
                    else
                        throw new Exception(msgVazio);
                }
                else
                    throw new Exception("Nenhum profissional informado!");
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }

        }

        /// <summary>
        /// Lista os atendimentos que foram realizados filtrando-os 
        /// </summary>
        /// <param name="DataInicio"></param>
        /// <param name="Co_col"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string HistoricoAtendimRealizadosPaci(string coAlu, string coCol, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                if ((!string.IsNullOrEmpty(coAlu)))
                {
                    int IcoAlu = int.Parse(coAlu);
                    int IcoCol = int.Parse(coCol);
                    DateTime dtIni = DateTime.Now.AddDays(-45);
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                               join tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs390.TBS174_AGEND_HORAR.ID_AGEND_HORAR into l1
                               from latend in l1.DefaultIfEmpty()
                               //Situações diferentes de cancelado e realizado
                               where tb07.CO_ALU == IcoAlu
                               && EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni)
                               && EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(DateTime.Now)
                               && tbs174.CO_COL == IcoCol
                               select new HistoricoAtendimRealizadosPaci
                               {
                                   DT = tbs174.DT_AGEND_HORAR,
                                   HR = tbs174.HR_AGEND_HORAR,
                                   deAcao = (latend != null ? (latend.DE_ACAO_REALI != null ? latend.DE_ACAO_REALI : (tbs174.DE_ACAO_PLAN != null ? tbs174.DE_ACAO_PLAN : " - ")) : (tbs174.DE_ACAO_PLAN != null ? tbs174.DE_ACAO_PLAN : " - ")),
                                   Situacao = tbs174.CO_SITUA_AGEND_HORAR,
                                   agendaConfirm = tbs174.FL_CONF_AGEND,
                                   agendaEncamin = tbs174.FL_AGEND_ENCAM,
                                   faltaJustif = tbs174.FL_JUSTI_CANCE,
                               }).ToList();

                    if (res != null)
                    {
                        var res2 = res.OrderByDescending(w => w.DT).ThenBy(w => w.hora).ToList();
                        return new JavaScriptSerializer().Serialize(res2);
                    }
                    else
                        throw new Exception(msgVazio);
                }
                else
                    throw new Exception("Nenhum paciente informado!");
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }

        }

        /// <summary>
        /// Lista os atendimentos que foram realizados filtrando-os 
        /// </summary>
        /// <param name="DataInicio"></param>
        /// <param name="Co_col"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string MeuHistoricoDeAtendimento(string coAlu, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                if ((!string.IsNullOrEmpty(coAlu)))
                {
                    int IcoAlu = int.Parse(coAlu);
                    //int IcoCol = int.Parse(coCol);
                    DateTime dtIni = DateTime.Now.AddDays(-45);
                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                               join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                               join tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs390.TBS174_AGEND_HORAR.ID_AGEND_HORAR into l1
                               from latend in l1.DefaultIfEmpty()
                               //Situações diferentes de cancelado e realizado
                               where tb07.CO_ALU == IcoAlu
                               && EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni)
                               && EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(DateTime.Now)
                               //&& tbs174.CO_COL == IcoCol
                               select new HistoricoAtendimRealizadosPaci
                               {
                                   DT = tbs174.DT_AGEND_HORAR,
                                   HR = tbs174.HR_AGEND_HORAR,
                                   deAcao = (latend != null ? (latend.DE_ACAO_REALI != null ? latend.DE_ACAO_REALI : (tbs174.DE_ACAO_PLAN != null ? tbs174.DE_ACAO_PLAN : " - ")) : (tbs174.DE_ACAO_PLAN != null ? tbs174.DE_ACAO_PLAN : " - ")),
                                   Situacao = tbs174.CO_SITUA_AGEND_HORAR,
                                   agendaConfirm = tbs174.FL_CONF_AGEND,
                                   agendaEncamin = tbs174.FL_AGEND_ENCAM,
                                   faltaJustif = tbs174.FL_JUSTI_CANCE,
                                   Colaborador = tb03.NO_COL,
                               }).ToList();

                    if (res != null)
                    {
                        var res2 = res.OrderByDescending(w => w.DT).ThenBy(w => w.hora).ToList();
                        return new JavaScriptSerializer().Serialize(res2);
                    }
                    else
                        throw new Exception(msgVazio);
                }
                else
                    throw new Exception("Nenhum paciente informado!");
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }

        }

        /// <summary>
        /// Lista todos os medicamentos do paciente no período informado
        /// </summary>
        /// <param name="DataInicio"></param>
        /// <param name="Co_col"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns>Retorna lista identificada</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string MedicamentosPaciente(string DataInicio, string DataFim, string coAlu, string coEmp, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                if (string.IsNullOrEmpty(DataInicio) || string.IsNullOrEmpty(DataFim))
                    throw new Exception("O Período deve ser informado!");

                int Co_Alu = !String.IsNullOrEmpty(coAlu) ? int.Parse(coAlu) : 0;
                var CO_Emp = !String.IsNullOrEmpty(coEmp) ? int.Parse(coEmp) : 0;
                DateTime dtInicial = DateTime.Parse(DataInicio);
                DateTime dtFinal = DateTime.Parse(DataFim);
                var res = (from tbs399 in TBS399_ATEND_MEDICAMENTOS.RetornaTodosRegistros()
                           join tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros() on tbs399.TBS390_ATEND_AGEND.ID_ATEND_AGEND equals tbs390.ID_ATEND_AGEND
                           join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tbs390.TBS174_AGEND_HORAR.ID_AGEND_HORAR equals tbs174.ID_AGEND_HORAR
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs390.CO_COL_ATEND equals tb03.CO_COL
                           where (tbs399.DT_CADAS >= dtInicial
                           && tbs399.DT_CADAS <= dtFinal)
                           && (Co_Alu != 0 ? tbs174.CO_ALU == Co_Alu : true)
                           && (CO_Emp != 0 ? tbs174.CO_EMP == CO_Emp : true)
                           select new
                           {
                               Data = tbs399.DT_CADAS,
                               Medicamento = tbs399.TB90_PRODUTO.NO_PROD,
                               Prescricao = tbs399.DE_PRESC,
                               Profissional = !String.IsNullOrEmpty(tb03.NO_APEL_COL) ? tb03.NO_APEL_COL : tb03.NO_COL,
                           }).OrderBy(p => p.Data).ToList();

                if (res == null)
                    throw new Exception(msgVazio);

                return new JavaScriptSerializer().Serialize(res);
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// Lista todos os exames do paciente dentro do período informado
        /// </summary>
        /// <param name="DataInicio"></param>
        /// <param name="Co_col"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns>Retorna lista identificada</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ExamesPaciente(string DataInicio, string DataFim, string coAlu, string coEmp, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                if (string.IsNullOrEmpty(DataInicio) || string.IsNullOrEmpty(DataFim))
                    throw new Exception("O Período deve ser informado!");

                int Co_Alu = !String.IsNullOrEmpty(coAlu) ? int.Parse(coAlu) : 0;
                var CO_Emp = !String.IsNullOrEmpty(coEmp) ? int.Parse(coEmp) : 0;
                DateTime dtInicial = DateTime.Parse(DataInicio);
                DateTime dtFinal = DateTime.Parse(DataFim);
                var res = (from tbs398 in TBS398_ATEND_EXAMES.RetornaTodosRegistros()
                           join tbs390 in TBS390_ATEND_AGEND.RetornaTodosRegistros() on tbs398.TBS390_ATEND_AGEND.ID_ATEND_AGEND equals tbs390.ID_ATEND_AGEND
                           join tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros() on tbs390.TBS174_AGEND_HORAR.ID_AGEND_HORAR equals tbs174.ID_AGEND_HORAR
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs390.CO_COL_ATEND equals tb03.CO_COL
                           where (tbs398.DT_CADAS >= dtInicial
                           && tbs398.DT_CADAS <= dtFinal)
                           && (Co_Alu != 0 ? tbs174.CO_ALU == Co_Alu : true)
                           && (CO_Emp != 0 ? tbs174.CO_EMP == CO_Emp : true)
                           select new
                           {
                               Data = tbs398.DT_CADAS,
                               Exame = tbs398.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                               Profissional = !String.IsNullOrEmpty(tb03.NO_APEL_COL) ? tb03.NO_APEL_COL : tb03.NO_COL,
                           }).OrderBy(p => p.Data).ToList();

                if (res == null)
                    throw new Exception(msgVazio);

                return new JavaScriptSerializer().Serialize(res);
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        #endregion

        //=======================>  PACIENTES =======================

        //=======================>  COLABORADOR =======================

        #region Colaborador

        /// <summary>
        /// Lista as faltas que houveram corrente para o colaborador recebido como parâmetro
        /// </summary>
        /// <param name="Co_col"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string MinhasFaltasMes(string Co_col, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                int Co_Col = int.Parse(Co_col);

                System.Globalization.Calendar c = new GregorianCalendar();
                int DiasMes = c.GetDaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                DateTime dtIni = (DateTime.Now.AddDays(-DateTime.Now.Day + 1)); //Coleta o primeiro dia do mês
                //DateTime dtFim = (DateTime.Now.AddDays(Math.Abs(DateTime.Now.Day - DiasMes))); //Coleta o último dia do mês
                DateTime dtFim = DateTime.Now;

                //Cria e popula lista com as datas dos últimos 45 dias
                DateTime dtaux = dtIni;
                List<DateTime> lstDatas = new List<DateTime>();
                while (dtaux <= dtFim)
                {
                    lstDatas.Add(dtaux);
                    dtaux = dtaux.AddDays(1);
                }

                List<MinhasFaltasMes> lstMinhasFaltas = new List<MinhasFaltasMes>();

                //Lista de atestados médicos para teste funcionário apartir desse mês
                var tb143lst = TB143_ATEST_MEDIC.RetornaTodosRegistros().Where(w => w.CO_USU == Co_Col &&
                    EntityFunctions.TruncateTime(w.DT_CONSU) >= dtIni).ToList();

                lstDatas = lstDatas.OrderByDescending(w => w.Month).ThenByDescending(w => w.Day).ToList();

                //Para cada data dos últimos 45 dias
                foreach (var i in lstDatas)
                {
                    var tb199 = TB199_FREQ_FUNC.RetornaTodosRegistros().Where(w => w.CO_COL == Co_Col
                        && EntityFunctions.TruncateTime(w.DT_FREQ) == EntityFunctions.TruncateTime(i)).FirstOrDefault();

                    MinhasFaltasMes it = new MinhasFaltasMes();
                    it.dt_R = i;
                    if (tb199 != null) //Se for diferente de nulo
                    {
                        if (tb199.FLA_PRESENCA != "S")
                        {
                            #region Se tiver faltado

                            //Se a falta foi justificada
                            if (tb199.FL_JUSTI_FALTA == "S")
                            {
                                it.justific = true;
                                it.comAtestado = true;
                                lstMinhasFaltas.Add(it);
                            }

                            #endregion
                        }
                    }
                    else //Se for igual à nulo é porque realmente faltou
                    {
                        it.justific = false;
                        it.comAtestado = false;
                        lstMinhasFaltas.Add(it);
                    }
                }

                if (lstMinhasFaltas.Count == 0)
                    throw new Exception(msgVazio);
                else
                    return new JavaScriptSerializer().Serialize(lstMinhasFaltas);
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// Lista o histórico de atendimentos por colaborador com os valores correspondentes aos meses
        /// </summary>
        /// <param name="Co_col"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string HistorFinanAtendimColaborador(string Co_col, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                int Co_Col = int.Parse(Co_col);

                var res = VW_RESUM_FINAN_ATEND_COLAB.RetornaTodosRegistros().Where(w => w.CO_COL == Co_Col).ToList();

                DateTime dtEscolh = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                System.Globalization.Calendar c = new GregorianCalendar();
                int DiasMes = c.GetDaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                DateTime dtIni = DateTime.Now.AddMonths(-6);
                DateTime dtFim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DiasMes); //Coleta o último dia do mês escolhido
                var resul = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                             join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                             where tbs174.DT_AGEND_HORAR >= dtIni
                             && tbs174.DT_AGEND_HORAR <= dtFim
                             && tbs174.CO_COL == Co_Col
                             select new
                             {
                                 //Ano = tbs174.DT_AGEND_HORAR.Year,
                                 Mes = tbs174.DT_AGEND_HORAR.Month,
                                 paci = tbs174.CO_ALU,
                                 situacao = tbs174.CO_SITUA_AGEND_HORAR,
                                 justifFalta = tbs174.FL_JUSTI_CANCE,
                                 presenca = tbs174.FL_CONF_AGEND,
                                 valorSess = tb03.VL_SALAR_COL,
                                 coCol = tb03.CO_COL,
                             }).ToList();

                var l = (from r in resul
                         group r by new
                         {
                             mes = r.Mes,
                             coCol = r.coCol
                         } into g
                         select new MapaAtendimMensal
                         {
                             coMes = g.Key.mes,
                             AGE_R = g.Where(w => w.paci.HasValue).Count(),
                             CAN_R = 0,
                             FAL_R = g.Where(w => w.situacao == "C" && w.justifFalta == "N").Count(),
                             FAJ_R = g.Where(w => w.situacao == "C" && w.justifFalta == "S").Count(),
                             QTF_R = g.Where(w => w.situacao == "C").Count(),
                             PRE_R = g.Where(w => w.presenca == "S").Count(),
                             AGA_R = g.Where(w => w.situacao == "A" && w.presenca != "S" && w.paci.HasValue).Count(),
                             FAT_R = g.Where(w => w.presenca == "S" || (w.situacao == "C" && w.justifFalta == "N")).Count(),
                             coCol = g.Key.coCol,
                         }).ToList();

                if (l == null)
                    throw new Exception(msgVazio);

                #region Prepara linha de Totais

                MapaAtendimMensal lfin = new MapaAtendimMensal();
                lfin.coMes = 0;
                lfin.AGE_R = l.Sum(w => Convert.ToDecimal(w.AGE));
                lfin.CAN_R = l.Sum(w => Convert.ToDecimal(w.CAN));
                lfin.FAL_R = l.Sum(w => Convert.ToDecimal(w.FAL));
                lfin.FAJ_R = l.Sum(w => Convert.ToDecimal(w.FAJ));
                lfin.QTF_R = l.Sum(w => Convert.ToDecimal(w.QTF));
                lfin.PRE_R = l.Sum(w => Convert.ToDecimal(w.PRE));
                lfin.AGA_R = l.Sum(w => Convert.ToDecimal(w.AGA));
                lfin.FAT_R = l.Sum(w => Convert.ToDecimal(w.FAT));
                lfin.ValorTotalManual = l.Sum(w => Convert.ToDouble(w.ValorTotal));
                l.Add(lfin);

                #endregion

                return new JavaScriptSerializer().Serialize(l);
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }

        /// <summary>
        /// Lista o histórico de atendimentos por colaborador com os valores correspondentes aos meses
        /// </summary>
        /// <param name="Co_col"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string HistorNaoAtendimentoColaborador(string Co_col, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                int Co_Col = int.Parse(Co_col);
                DateTime dtIni = DateTime.Now.AddDays(-45);
                DateTime dtFim = DateTime.Now;

                //Cria e popula lista com as datas dos últimos 45 dias
                DateTime dtaux = dtIni;
                List<DateTime> lstDatas = new List<DateTime>();
                while (dtaux <= dtFim)
                {
                    lstDatas.Add(dtaux);
                    dtaux = dtaux.AddDays(1);
                }

                List<HistoricoNaoAtendimento> lstNaoAtendm = new List<HistoricoNaoAtendimento>();

                //Cria lista com as frequências do colaborador dentro do período
                var tb119lst = TB199_FREQ_FUNC.RetornaTodosRegistros().Where(w => w.CO_COL == Co_Col &&
                        EntityFunctions.TruncateTime(w.DT_FREQ) >= EntityFunctions.TruncateTime(dtIni));

                lstDatas = lstDatas.OrderByDescending(w => w.Month).ThenByDescending(w => w.Day).ToList();

                //Para cada data dos últimos 45 dias
                foreach (var i in lstDatas)
                {
                    //Se não houver nenhuma frequência para este colaborador nesta data
                    if (!tb119lst.Where(w => EntityFunctions.TruncateTime(w.DT_FREQ) == EntityFunctions.TruncateTime(i)).Any())
                    {
                        //lista as agendas que ficaram em aberto para esta data e colaborador
                        var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                   join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                                   where tbs174.CO_COL == Co_Col
                                   && EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) == EntityFunctions.TruncateTime(i)
                                   && tbs174.FL_CONF_AGEND != "S"
                                   select new HistoricoNaoAtendimento
                                   {
                                       dt_R = tbs174.DT_AGEND_HORAR,
                                       deAcao = (tbs174.DE_ACAO_PLAN != null ? tbs174.DE_ACAO_PLAN : " - "),
                                       nomePaciente = (tb07.NO_APE_ALU != null ? tb07.NO_APE_ALU : tb07.NO_ALU),
                                       hr = tbs174.HR_AGEND_HORAR,
                                   }).ToList();

                        //adiciona os itens dessa lista à lista que será fruto desse webservice
                        foreach (var ls in res)
                        {
                            lstNaoAtendm.Add(ls);
                        }
                    }
                }

                if (lstNaoAtendm.Count == 0)
                    return new JavaScriptSerializer().Serialize(null);
                else
                    return new JavaScriptSerializer().Serialize(lstNaoAtendm);
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string AgendaContatos(string tpContato, int coEmp, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");
                if (tpContato == "P")
                {
                    var resp = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                where (coEmp != 0 ? tb07.CO_EMP == coEmp : coEmp == 0)
                                select new
                                {
                                    CONTATO = (tb07.NO_ALU).ToUpper(),
                                    EMAIL = "",
                                    TRABALHO = "-",
                                    CELULAR = tb07.NU_TELE_CELU_ALU != null && tb07.NU_TELE_CELU_ALU != "" ? tb07.NU_TELE_CELU_ALU.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                    TELEFONE = tb07.NU_TELE_RESI_ALU != null && tb07.NU_TELE_RESI_ALU != "" ? tb07.NU_TELE_RESI_ALU.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : ""
                                }).OrderBy(a => a.CONTATO).ToList();
                    if (resp.Count == 0)
                        throw new Exception(msgVazio2);
                    else
                        return new JavaScriptSerializer().Serialize(resp);

                }
                if (tpContato == "S")//Profissional Saúde
                {
                    var resp = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                where tb03.FLA_PROFESSOR == "S"
                                && (coEmp != 0 ? tb03.CO_EMP == coEmp : coEmp == 0)
                                select new
                                {
                                    CONTATO = (tb03.NO_COL).ToUpper(),
                                    EMAIL = tb03.CO_EMAI_COL,
                                    CELULAR = tb03.NU_TELE_CELU_COL != null && tb03.NU_TELE_CELU_COL != "" ? tb03.NU_TELE_CELU_COL.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                    TELEFONE = tb03.NU_TELE_RESI_COL != null && tb03.NU_TELE_RESI_COL != "" ? tb03.NU_TELE_RESI_COL.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                    TRABALHO = tb03.NU_TELE_COME_COL != null && tb03.NU_TELE_COME_COL != "" ? tb03.NU_TELE_COME_COL.Insert(0, "(").Insert(3, ") ").Insert(9, "-") + " / " + tb03.NU_RAMA_COME_COL : "-"
                                }).OrderBy(f => f.CONTATO).ToList();
                    if (resp.Count == 0)
                        throw new Exception(msgVazio2);
                    else
                        return new JavaScriptSerializer().Serialize(resp);
                }
                if (tpContato == "N")//Funcionário
                {
                    var resp = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                where tb03.FLA_PROFESSOR == "N"
                                && (coEmp != 0 ? tb03.CO_EMP == coEmp : coEmp == 0)
                                select new
                                {
                                    CONTATO = (tb03.NO_COL).ToUpper(),
                                    EMAIL = tb03.CO_EMAI_COL,
                                    CELULAR = tb03.NU_TELE_CELU_COL != null && tb03.NU_TELE_CELU_COL != "" ? tb03.NU_TELE_CELU_COL.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                    TELEFONE = tb03.NU_TELE_RESI_COL != null && tb03.NU_TELE_RESI_COL != "" ? tb03.NU_TELE_RESI_COL.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                    TRABALHO = tb03.NU_TELE_COME_COL != null && tb03.NU_TELE_COME_COL != "" ? tb03.NU_TELE_COME_COL.Insert(0, "(").Insert(3, ") ").Insert(9, "-") + " / " + tb03.NU_RAMA_COME_COL : "-"
                                }).OrderBy(f => f.CONTATO).ToList();
                    if (resp.Count == 0)
                        throw new Exception(msgVazio2);
                    else
                        return new JavaScriptSerializer().Serialize(resp);
                }
                if (tpContato == "G")//GESTOR
                {
                    var resp = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                where tb03.FLA_PROFESSOR == "G"
                                && (coEmp != 0 ? tb03.CO_EMP == coEmp : coEmp == 0)
                                select new
                                {
                                    CONTATO = (tb03.NO_COL).ToUpper(),
                                    EMAIL = tb03.CO_EMAI_COL,
                                    CELULAR = tb03.NU_TELE_CELU_COL != null && tb03.NU_TELE_CELU_COL != "" ? tb03.NU_TELE_CELU_COL.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                    TELEFONE = tb03.NU_TELE_RESI_COL != null && tb03.NU_TELE_RESI_COL != "" ? tb03.NU_TELE_RESI_COL.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                    TRABALHO = tb03.NU_TELE_COME_COL != null && tb03.NU_TELE_COME_COL != "" ? tb03.NU_TELE_COME_COL.Insert(0, "(").Insert(3, ") ").Insert(9, "-") + " / " + tb03.NU_RAMA_COME_COL : "-"
                                }).OrderBy(f => f.CONTATO).ToList();
                    if (resp.Count == 0)
                        throw new Exception(msgVazio2);
                    else
                        return new JavaScriptSerializer().Serialize(resp);
                }
                if (tpContato == "O")//Outros
                {
                    var resp = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                where tb03.FLA_PROFESSOR == "O"
                                && (coEmp != 0 ? tb03.CO_EMP == coEmp : coEmp == 0)
                                select new
                                {
                                    CONTATO = (tb03.NO_COL).ToUpper(),
                                    EMAIL = tb03.CO_EMAI_COL,
                                    CELULAR = tb03.NU_TELE_CELU_COL != null && tb03.NU_TELE_CELU_COL != "" ? tb03.NU_TELE_CELU_COL.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                    TELEFONE = tb03.NU_TELE_RESI_COL != null && tb03.NU_TELE_RESI_COL != "" ? tb03.NU_TELE_RESI_COL.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                    TRABALHO = tb03.NU_TELE_COME_COL != null && tb03.NU_TELE_COME_COL != "" ? tb03.NU_TELE_COME_COL.Insert(0, "(").Insert(3, ") ").Insert(9, "-") + " / " + tb03.NU_RAMA_COME_COL : "-"
                                }).OrderBy(f => f.CONTATO).ToList();
                    if (resp.Count == 0)
                        throw new Exception(msgVazio2);
                    else
                        return new JavaScriptSerializer().Serialize(resp);
                }
                else
                {
                    var resp = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                where coEmp != 0 ? tb03.CO_EMP == coEmp : coEmp == 0
                                select new
                                {
                                    CONTATO = (tb03.NO_COL).ToUpper(),
                                    EMAIL = tb03.CO_EMAI_COL,
                                    CELULAR = tb03.NU_TELE_CELU_COL != null && tb03.NU_TELE_CELU_COL != "" ? tb03.NU_TELE_CELU_COL.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                    TELEFONE = tb03.NU_TELE_RESI_COL != null && tb03.NU_TELE_RESI_COL != "" ? tb03.NU_TELE_RESI_COL.Insert(0, "(").Insert(3, ") ").Insert(9, "-") : "",
                                    TRABALHO = tb03.NU_TELE_COME_COL != null && tb03.NU_TELE_COME_COL != "" ? tb03.NU_TELE_COME_COL.Insert(0, "(").Insert(3, ") ").Insert(9, "-") + " / " + tb03.NU_RAMA_COME_COL : "-"
                                }).OrderBy(f => f.CONTATO).ToList();
                    if (resp.Count == 0)
                        throw new Exception(msgVazio2);
                    else
                        return new JavaScriptSerializer().Serialize(resp);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string AlterarSenha(string SenhaAtual, string NovaSenha, string ConfirmaNovaSenha, int idUsuarioApp, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");
                if (NovaSenha != ConfirmaNovaSenha)
                    throw new Exception("Senhas não confere");
                TBS384_USUAR_APP tbs384 = TBS384_USUAR_APP.RetornaPelaChavePrimaria(idUsuarioApp);
                if (tbs384 != null)
                {
                    tbs384.DE_SENHA = LoginAuxili.GerarMD5(NovaSenha);
                    TBS384_USUAR_APP.SaveOrUpdate(tbs384);
                    var admUsuario = (from tbs in TBS384_USUAR_APP.RetornaTodosRegistros()
                                      where tbs.DE_LOGIN.Equals(Login) && tbs.DE_SENHA.Equals(tbs384.DE_SENHA)
                                      select new DadosLogin
                                      {
                                          desLogin = tbs384.DE_LOGIN,
                                          Senha = tbs384.DE_SENHA,
                                      }).FirstOrDefault();
                    return new JavaScriptSerializer().Serialize(admUsuario);
                }
                else
                {
                    throw new Exception("Não foi possível alterar a senha");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }
        #endregion

        //=======================>  COLABORADOR =======================

        //=======================>  COLABORADOR =======================

        #region Financeiro

        /// <summary>
        /// Apresenta o resumo financeiro do mês recebido como parâmetro
        /// </summary>
        /// <param name="DataInicio"></param>
        /// <param name="Co_col"></param>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns>Retorna lista das agendas do dia</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ResumoFinanceiro(string mes, string Co_col, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");
                if ((!string.IsNullOrEmpty(mes)))
                {
                    int coCol = int.Parse(Co_col);
                    int mesI = int.Parse(mes);

                    DateTime dtEscolh = new DateTime(DateTime.Now.Year, mesI, 1);
                    System.Globalization.Calendar c = new GregorianCalendar();
                    int DiasMes = c.GetDaysInMonth(DateTime.Now.Year, mesI);
                    DateTime dtIni = new DateTime(DateTime.Now.Year, mesI, 1); //Coleta o primeiro dia do mês escolhido
                    DateTime dtFim = (dtIni.AddDays(DiasMes)); //Coleta o último dia do mês escolhido

                    var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                               join tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros() on tbs174.ID_AGEND_HORAR equals tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR into l1
                               from ls in l1.DefaultIfEmpty()
                               join tbs353 in TBS353_VALOR_PROC_MEDIC_PROCE.RetornaTodosRegistros() on ls.TBS386_ITENS_PLANE_AVALI.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE equals tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE into lvl
                               from IIlvl in lvl.DefaultIfEmpty()
                               where tbs174.CO_COL == coCol
                               && EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) >= EntityFunctions.TruncateTime(dtIni)
                               && EntityFunctions.TruncateTime(tbs174.DT_AGEND_HORAR) <= EntityFunctions.TruncateTime(dtFim)
                               && tbs174.CO_ALU != null
                               select new
                               {
                                   data = tbs174.DT_AGEND_HORAR,
                                   idOper = (tbs174.TB250_OPERA != null ? tbs174.TB250_OPERA.ID_OPER : (int?)null),
                                   valorProced = (IIlvl != null ? IIlvl.VL_BASE : (decimal?)null),
                               }).OrderBy(m => m.data).ToList();

                    var l = (from r in res
                             group r by new
                             {
                                 data = r.data
                             } into g
                             select new ResumoFinanceiro
                             {
                                 data = g.Key.data,
                                 qtPart = g.Count(w => !w.idOper.HasValue),
                                 vlPart = g.Where(w => !w.idOper.HasValue).Sum(w => (w.valorProced.HasValue ? w.valorProced.Value : 0)).ToString("N2"),

                                 qtPlano = g.Count(w => w.idOper.HasValue),
                                 vlPlano = g.Where(w => w.idOper.HasValue).Sum(w => (w.valorProced.HasValue ? w.valorProced.Value : 0)).ToString("N2"),

                             }).ToList();

                    if (l != null)
                        return new JavaScriptSerializer().Serialize(l);
                    else
                        throw new Exception(msgVazio);
                }
                else
                    throw new Exception("Por favor informar o mês de referência");
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }

        }

        #endregion

        //=======================>  COLABORADOR =======================
        #region GEDUC

        /// <summary>
        /// Carrega todos os pacientes que possuem agenda em aberto com o colaborador recebido como parâmetro
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Senha"></param>
        /// <returns> retorna string com dados do colaborador logado quando, para este, for permitido acesso </returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaAlunos(string coCol, string idUsuarioApp, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                if (string.IsNullOrEmpty(coCol))
                    throw new Exception("-");

                int IcoCol = int.Parse(coCol);
                string tipo = TBS384_USUAR_APP.RetornaPelaChavePrimaria(int.Parse(idUsuarioApp)).CO_TIPO;

                //Carregamento de pacientes para os Colaboradores (pacientes associados à ele)
                if (tipo == "C")
                {
                    
 
                    #region Colaborador
                    //var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                    //           join TB in TB_RESPON_MATERIA.RetornaTodosRegistros() on tb07.CO equals TB03.TBC
                    //           join TB03 in TB03_COLABOR.RetornaTodosRegistros() on tb07.CO equals TB03.TBC
                    //           //Situações diferentes de cancelado e realizado
                    //           where tbs174.CO_COL == IcoCol
                    //                 && tb07.CO_SITU_ALU == "A"
                    //           select new { tb07.CO_ALU, tb07.NO_ALU }).DistinctBy(w => w.CO_ALU).OrderBy(w => w.NO_ALU).ToList();

                    //if (res != null)
                    //    return new JavaScriptSerializer().Serialize(res);
                    //else
                        throw new Exception(msgVazio);

                    #endregion
                }
                else if (tipo == "G") //Todos os pacientes ativos na base
                {
                    #region Gestor

                    var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                               where tb07.CO_SITU_ALU == "A"
                               select new { tb07.CO_ALU, tb07.NO_ALU }).DistinctBy(w => w.CO_ALU).OrderBy(w => w.NO_ALU).ToList();

                    if (res != null)
                        return new JavaScriptSerializer().Serialize(res);
                    else
                        throw new Exception(msgVazio);

                    #endregion
                }
                else if (tipo == "R") //Os pacientes desse responsável
                {
                    #region Responsável

                    var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                               where tb07.TB108_RESPONSAVEL.CO_RESP == IcoCol
                               && tb07.CO_SITU_ALU == "A"
                               select new { tb07.CO_ALU, tb07.NO_ALU }).DistinctBy(w => w.CO_ALU).OrderBy(w => w.NO_ALU).ToList();

                    if (res != null)
                        return new JavaScriptSerializer().Serialize(res);
                    else
                        throw new Exception(msgVazio);

                    #endregion
                }
                else
                    throw new Exception("O usuário logado não é de nenhum tipo aceitável!");
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaModalidadesGE(int  orgCodigoOrgao, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");
                DropDownList ddlModalidade = new DropDownList();
                AuxiliCarregamentos.carregaModalidades(ddlModalidade, 17, true);
                List<Geral> ListaModalidade = new List<Geral>();
                foreach (ListItem item in ddlModalidade.Items)
                {
                    Geral repClassificacoesFuncionais = new Geral();
                    repClassificacoesFuncionais.Nome = item.Text;
                    repClassificacoesFuncionais.Valor = item.Value;
                    ListaModalidade.Add(repClassificacoesFuncionais);
                }
                if (ListaModalidade != null)
                {
                    return new JavaScriptSerializer().Serialize(ListaModalidade.OrderBy(a => a.Valor));
                }
                else
                    throw new Exception(msgVazio2);
                
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaSerieCursoGE(int modalidade, int coEmp, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                DropDownList ddlSerieCurso = new DropDownList();
                AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, coEmp, true);
                List<Geral> ListaSerieCurso = new List<Geral>();
                foreach (ListItem item in ddlSerieCurso.Items)
                {
                    Geral repSerieCurso = new Geral();
                    repSerieCurso.Nome = item.Text;
                    repSerieCurso.Valor = item.Value;
                    ListaSerieCurso.Add(repSerieCurso);
                }
                if (ListaSerieCurso != null)
                {
                    return new JavaScriptSerializer().Serialize(ListaSerieCurso.OrderBy(a => a.Valor));
                }
                else
                    throw new Exception(msgVazio2);

            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CarregaTurmaGE(int modalidade, int coEmp, int serie, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");

                DropDownList ddlTurma = new DropDownList();
                AuxiliCarregamentos.CarregaTurmas(ddlTurma, coEmp, modalidade, serie, true);
                List<Geral> ListaTurma = new List<Geral>();
                foreach (ListItem item in ddlTurma.Items)
                {
                    Geral repTurma = new Geral();
                    repTurma.Nome = item.Text;
                    repTurma.Valor = item.Value;
                    ListaTurma.Add(repTurma);
                }
                if (ListaTurma != null)
                {
                    return new JavaScriptSerializer().Serialize(ListaTurma.OrderBy(a => a.Valor));
                }
                else
                    throw new Exception(msgVazio2);

            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }

        }

        #region Aluno / responsável

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string RelacaoAlunoSerieGE(int codMod, int codCurso, int codTurma, string Login, string Senha)
        {
            try
            {
                if (rep.Autenticacao(Login, Senha) == false)
                    throw new Exception("Falha na autenticação com o web Service");


                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
                var lst = from mm in ctx.TB08_MATRCUR
                          from a in ctx.TB07_ALUNO
                          from t in ctx.TB06_TURMAS
                          from c in ctx.TB01_CURSO
                          from r in ctx.TB108_RESPONSAVEL
                          from ct in ctx.TB129_CADTURMAS
                          where
                           mm.CO_EMP == a.CO_EMP && mm.CO_EMP == t.CO_EMP &&
                           mm.CO_EMP == c.CO_EMP && a.CO_ALU == mm.CO_ALU &&
                           mm.CO_TUR == t.CO_TUR && ct.CO_TUR == t.CO_TUR &&
                           mm.TB44_MODULO.CO_MODU_CUR == t.CO_MODU_CUR &&
                           t.CO_MODU_CUR == ct.TB44_MODULO.CO_MODU_CUR &&
                           mm.CO_CUR == c.CO_CUR && r.CO_RESP == a.TB108_RESPONSAVEL.CO_RESP
                           && (codMod != 0 ? mm.TB44_MODULO.CO_MODU_CUR == codMod : 0 == 0)
                           && (codCurso != 0 ? c.CO_CUR == codCurso : 0 == 0)
                           && (codTurma != 0 ? ct.CO_TUR == codTurma : 0 == 0)
                          select new 
                          {
                              Sexo = a.CO_SEXO_ALU,
                              CodCurso = mm.CO_CUR,
                              CodEmpresa = mm.CO_EMP,
                              CodMod = mm.TB44_MODULO.CO_MODU_CUR,
                              AnoMat = mm.CO_ANO_MES_MAT,
                              CodTurma = mm.CO_TUR.Value,
                              DataNascimento = a.DT_NASC_ALU,
                              Matricula = a.NU_NIRE,
                              Nome = a.NO_ALU,
                              Responsavel = r.NO_RESP,
                              Situacao = mm.CO_SIT_MAT,
                              Telefone = a.NU_TELE_RESI_ALU,
                              Turma = ct.CO_SIGLA_TURMA,
                              NO_MODALIDADE = mm.TB44_MODULO.DE_MODU_CUR,
                              NO_CURSO = c.NO_CUR,
                              NO_TURMA = ct.NO_TURMA,
                          };
                lst = lst.Where(x => x.AnoMat == DateTime.Now.DayOfYear.ToString());
                    lst = lst.Where(x => x.Situacao == "A");

                if (lst != null)
                {
                    var res = lst.OrderByDescending(w => w.NO_MODALIDADE).ThenByDescending(w => w.NO_CURSO).ThenByDescending(w => w.NO_TURMA).ThenBy(w => w.Nome).ToList();
                    return new JavaScriptSerializer().Serialize(res);
                }
                else
                    throw new Exception(msgVazio2);
                
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }

        }


        #endregion

        #endregion






    }
}

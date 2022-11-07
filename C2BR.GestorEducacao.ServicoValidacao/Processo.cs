//===============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: Serviço validação
// OBJETIVO: Serviço de validação
// DATA DE CRIAÇÃO: ??/??/2013
//-------------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-------------------------------------------------------------------------------
//  DATA      |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// -----------+----------------------------+-------------------------------------
// ??/??/2013 | Caio Mendonça              | Criação do serviço
// -----------+----------------------------+-------------------------------------
// 29/03/2013 | Caio Mendonça              | Atualização das regras na função do loop diário: validação do ntp e 
//            |                            |    validação de acontecimento de erro
// -----------+----------------------------+-------------------------------------
//
//
//  
//================================================================================


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Management;
using System.Data.SqlClient;
using C2BR.GestorEducacao.LicenseValidator;

namespace C2BR.GestorEducacao.ServicoValidacao
{
    public class Processo
    {

        #region Varíaveis

        private static bool ocupado = false;
        private System.Diagnostics.EventLog _AppEventLog;
        private string connection = System.Configuration.ConfigurationManager.ConnectionStrings["CONEXAO1"].ToString();
        private int qtd = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["qtd"].ToString());
        private string caminho = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Log\";
        // Cria varíavel de time, loop diário
        private System.Timers.Timer _Timer;

        // Cria varíavel de time, loop hora
        private System.Timers.Timer _TimerCont;

        // Varíavel de intervalo
        // 1 * 1000 - 1 segundo
        // 60 * 1000 - 60 segundos (1 minuto)
        // 60 * 60 * 1000 - 1 hora
        // Corrigir para 24 horas e outro para 1 hora quando for para produção
        private int _Interval = (60 * 1000); // 24 horas (24 * (60 * 60 * 1000))
        private int _IntervalCont = (10 * 1000); // 1 hora (60 * 60 * 1000)

        public int Interval
        {
            get { return this._Interval; }
            set { this._Interval = value; }
        }

        public int IntervalCont
        {
            get { return this._IntervalCont; }
            set { this._IntervalCont = value; }
        }

        #endregion


        // Construtor, cria os loops
        public Processo()
        {
            // loop diário
            this._Timer = new System.Timers.Timer();
            this._Timer.Elapsed +=
                new System.Timers.ElapsedEventHandler(_Timer_Elapsed);
            this._Timer.Enabled = false;
            this._Timer.Interval = this.Interval;

            // loop hora
            this._TimerCont = new System.Timers.Timer();
            this._TimerCont.Elapsed +=
                new System.Timers.ElapsedEventHandler(_Timer_Elapsed_Cont);
            this._TimerCont.Enabled = false;
            this._TimerCont.Interval = this.IntervalCont;

        }


        #region Funções que ativam/desativam loops

        // Aciona os loops
        public void StartTimer()
        {
            // Grava no log para saber data exata que foi ativado
            using (StreamWriter sw = new StreamWriter(caminho + @"logServico.txt", true))
            {
                sw.WriteLine("Iniciado " + DateTime.Now.ToString());
            }

            // Passa por todas as conecções existentes no app.config
            for (int x = 1; x <= qtd; x++)
            {
                connection = System.Configuration.ConfigurationManager.ConnectionStrings["CONEXAO" + x.ToString()].ToString();

                AtualizaDados(x.ToString());
                AtualizaContador(0,x.ToString());
            }
            this._Timer.Enabled = true;
            this._TimerCont.Enabled = true;
        }

        // Desativa os loops
        public void StopTimer()
        {
            // Grava no log
            using (StreamWriter sw = new StreamWriter(caminho + @"logServico.txt", true))
            {
                sw.WriteLine("Parado " + DateTime.Now.ToString());
            }

            this._Timer.Enabled = false;
            this._TimerCont.Enabled = false;
        }

        // Função chamada a cada loop diário
        private void _Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ocupado = true;
            // Passa por todas as conecções existentes no app.config
            for (int x = 1; x <= qtd; x++)
            {
                connection = System.Configuration.ConfigurationManager.ConnectionStrings["CONEXAO" + x.ToString()].ToString();
                // Função que atualiza dados da bios
                AtualizaDados(x.ToString());
            }
            ocupado = false;
        }

        // Função chamada a cada loop hora
        private void _Timer_Elapsed_Cont(object sender, System.Timers.ElapsedEventArgs e)
        {
            // Passa por todas as conecções existentes no app.config
            for (int x = 1; x <= qtd; x++)
            {
                connection = System.Configuration.ConfigurationManager.ConnectionStrings["CONEXAO" + x.ToString()].ToString();
                if (!ocupado)
                {
                    AtualizaContador(0, x.ToString());
                }
            }
        }

        #endregion 


        #region Funções que rodam nos loops

        // Atualiza dados da bios - loop diário
        private void AtualizaDados(string nome)
        {
            try
            {
                Criptografia cripto = new Criptografia();


                #region Busca informações de atualização, BIOS e datas

                // Classe para poder realizar busca de informações da bios
                ManagementObjectSearcher search = new ManagementObjectSearcher("Select * from Win32_BIOS");

                // Guarda resultado da busca
                ManagementObjectCollection collection = search.Get();

                string md5bios = "";
                string biosName = "";
                string biosElementId = "";
                string biosVersion = "";
                string data = DateTime.Now.ToString();
                string datantp = DateTime.Now.ToString(); // AtualizaDataNTP().ToString();
                string verifData = data;
                string verifDatantp = datantp;
                bool verifntp = true;
                bool v = true;

                // Grava dados da bios
                foreach (ManagementObject obj in collection)
                {
                    biosName = obj["Name"].ToString().Trim();
                    biosElementId = obj["SoftwareElementID"].ToString().Trim();
                    biosVersion = obj["Version"].ToString().Trim();
                }

                md5bios = biosName + biosElementId + biosVersion;
                // Encriptografa dados da bios
                md5bios = cripto.CalculaMD5(md5bios);
                // Encriptografa data a partir do valor do hash do md5
                data = cripto.Encrypt(data, md5bios);
                datantp = cripto.Encrypt(datantp, md5bios);

                #endregion


                #region Correção criptografia do contador e arruma data do ntp, caso não seja primeira vez

                string licenca = "";
                string chave = "";
                string chaventp = "";
                string contador = "";
                bool vcontador = false;

                using (SqlConnection con = new SqlConnection(connection))
                {
                    // Pre-requisito: sempre terá apenas um valor na tabela tb000
                    string sql = "SELECT TOP 1 ORG_LICENCA, ORG_VALIDACAO2, ORG_VALIDACAO4, ORG_VALIDACAO3 FROM TB000_INSTITUICAO";
                    SqlCommand cmd = new SqlCommand(sql, con);

                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        licenca = reader[0].ToString();
                        chave = reader[1].ToString();
                        contador = reader[2].ToString();
                        chaventp = reader[3].ToString();
                    }

                }

                // DE1717D81C36CA88C3814539197D4CE3
                if ((contador.Trim() != cripto.CalculaMD5("harlem shake").Trim()) && contador != "")
                {
                    // Tentando descriptografar o contador
                    try
                    {
                        contador = cripto.Decrypt(contador, chave);
                    }
                    catch (Exception e)
                    {
                        // Se não conseguir zera contador
                        contador = "0";
                    }

                    #region Valida data do ntp

                    //chave = cripto.Decrypt(chave, md5bios);

                    DateTime dtantigantp = DateTime.Now;
                    DateTime dtnovantp = DateTime.Parse(verifDatantp);

                    // Tenta transformar em data o valor descriptografado
                    try
                    {
                        // Descriptografa dados do ntp
                        chaventp = cripto.Decrypt(chaventp, md5bios);
                        dtantigantp = DateTime.Parse(chaventp);
                    }
                    catch (Exception e)
                    {
                        // se não conseguir então pega a data nova e diminui um dia
                        dtnovantp = dtnovantp.AddDays(-1);
                        v = false;
                    }

                    // Substitui a data do NTP apenas se for maior ou igual a data que já estava antes
                    if (dtnovantp < dtantigantp && v == true)
                    {
                        datantp = cripto.Encrypt(chaventp, md5bios);
                        verifntp = false;
                    }

                    // Fica com a data nova menos um dia
                    if (v == false)
                    {
                        datantp = cripto.Encrypt(dtnovantp.ToString(), md5bios);
                    }

                    #endregion

                    // Criptografa o contador
                    contador = cripto.Encrypt(contador, data);
                    vcontador = true;
                }

                #endregion


                #region Atualiza no banco

                // Atualiza tabela com dados da bios e data atual
                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = "UPDATE TB000_INSTITUICAO SET ORG_VALIDACAO1 = @BIOS, ORG_VALIDACAO2 = @DATA, ORG_VALIDACAO3 = @DATANTP";

                    if (vcontador)
                    {
                        sql += " , ORG_VALIDACAO4 = @CONTADOR";
                    }

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("BIOS", md5bios);
                    cmd.Parameters.AddWithValue("DATA", data);
                    cmd.Parameters.AddWithValue("DATANTP", datantp);
                    cmd.Parameters.AddWithValue("CONTADOR", contador);

                    con.Open();

                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        Exception ex = new Exception("Erro ao atualizar dados. Data: " + cmd.Parameters[1].ToString() + " Bios: " + cmd.Parameters[0].ToString());
                        throw ex;
                    }

                    //AtualizaContador(1);

                }

                #endregion


                #region log

                // Grava log
                using (StreamWriter sw = new StreamWriter(caminho + @"logServico_" + nome + ".txt", true))
                {
                    sw.WriteLine("************** DIÁRIO ****************");
                    sw.WriteLine("Executado " + DateTime.Now.ToString());
                    // Apagar dados da bios do log, apenas para teste
                    sw.WriteLine("biosname " + biosName);
                    sw.WriteLine("bioselement "+ biosElementId);
                    sw.WriteLine("biosversion " +biosVersion);
                    sw.WriteLine("Data - Atualizado: SIM -  " + verifData);
                    if(verifntp)
                        if(v == false)
                            sw.WriteLine("NTP - Atualizado: SIM -  " + DateTime.Parse(verifDatantp).AddDays(-1).ToString() + "  - NTP antigo não descriptografado, data nova menos um dia");
                        else
                        sw.WriteLine("NTP - Atualizado: SIM -  " + verifDatantp);
                    else
                        sw.WriteLine("NTP - Atualizado: NÃO -  " + chaventp);
                    sw.WriteLine("**************************************");
                }

                #endregion

            }
            catch (Exception ex)
            {
                // Grava erro em log de erro
                using (StreamWriter sw = new StreamWriter(caminho + @"erroServico_" + nome + ".txt", true))
                {
                    sw.WriteLine("************** DIÁRIO ****************");
                    sw.WriteLine("Erro ao executar " + DateTime.Now.ToString());
                    sw.WriteLine("Exceção: " + ex.ToString());
                    sw.WriteLine("**************************************");
                }

            }


        }

        // Atualiza a data de acordo com o NTP - loop diário
        private DateTime AtualizaDataNTP()
        {
            NTPClient ntpclient = new NTPClient();
            DateTime datantp = ntpclient.GetNetworkTime(1);
            if (datantp == null)
                datantp = ntpclient.GetNetworkTime(2);

            return datantp;
        }

        // Atualiza contador regressivo de tempo restante de utilização do sistema - loop hora
        private void AtualizaContador(int i,string nome)
        {
            try
            {
                Criptografia cripto = new Criptografia();

                string licenca = "";
                string contador = "";
                string chave = "";
                double valorfinal = 0;
                bool primeiravez = false;

                // Atualiza tabela com dados da bios e data atual
                using (SqlConnection con = new SqlConnection(connection))
                {
                    // Pre-requisito: sempre terá apenas um valor na tabela tb000
                    string sql = "SELECT TOP 1 ORG_LICENCA, ORG_VALIDACAO2, ORG_VALIDACAO4 FROM TB000_INSTITUICAO";
                    SqlCommand cmd = new SqlCommand(sql, con);

                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        licenca = reader[0].ToString();
                        chave = reader[1].ToString();
                        contador = reader[2].ToString();
                    }

                }

                // DE1717D81C36CA88C3814539197D4CE3
                if (contador.Trim() == cripto.CalculaMD5("harlem shake").Trim())
                {
                    // pegar valores da licença e pegar a quantidade de milisegundos
                    License lic = LicenseHelper.RetornaLicenca(licenca);
                    DateTime dataInicial = lic.DataInicio;
                    DateTime dataFinal = lic.DataFim;
                    TimeSpan diferenca = dataFinal.Subtract(dataInicial);
                    contador = diferenca.TotalMilliseconds.ToString();


                    // Apagar, log feito para teste
                    using (StreamWriter sw = new StreamWriter(caminho + @"logServico_" + nome + ".txt", true))
                    {
                        sw.WriteLine("************** HORA - primeira vez ******************");
                        sw.WriteLine("inicio " + dataInicial.ToString());
                        sw.WriteLine("fim " + dataFinal.ToString());
                        sw.WriteLine("contador " + contador);
                        sw.WriteLine("**************************************");
                    }

                    primeiravez = true;

                }else
                    if (contador.Trim() == "")
                    {
                            contador = "0";
                    }
                    else
                    {
                        // pega contador e subtrai 1h
                        try
                        {
                            contador = cripto.Decrypt(contador, chave);
                            if (Convert.ToDouble(contador) > 0)
                            {
                                double valorinicial = Convert.ToDouble(contador);
                                valorfinal = valorinicial - (60 * 60 * 1000);
                                if (i != 1)
                                {
                                    contador = valorfinal.ToString();
                                }
                                else
                                {
                                    contador = valorinicial.ToString();
                                }
                            }
                            else
                            {
                                contador = "0";
                            }

                        }
                        catch(Exception e){
                            
                            contador = "0";

                        }
                    }

                contador = cripto.Encrypt(contador, chave);

                // Atualiza tabela com dados da bios e data atual
                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = "UPDATE TB000_INSTITUICAO SET ORG_VALIDACAO4 = @CONTADOR";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("CONTADOR", contador);

                    con.Open();

                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        Exception ex = new Exception("Erro ao atualizar contador. Chave: " + chave + " Contador: " + cmd.Parameters[0].ToString());
                        throw ex;
                    }

                }

                if (!primeiravez)
                {
                    // Grava log
                    using (StreamWriter sw = new StreamWriter(caminho + @"logServico_" + nome + ".txt", true))
                    {
                        sw.WriteLine("************** HORA ******************");
                        sw.WriteLine("Executado " + DateTime.Now.ToString());
                        // Apagar contador do log, apenas para teste
                        sw.WriteLine("Contador " + valorfinal);
                        sw.WriteLine("**************************************");
                    }
                }
            }
            catch (Exception ex)
            {
                // Grava erro em log de erro
                using (StreamWriter sw = new StreamWriter(caminho + @"erroServico_" + nome + ".txt", true))
                {
                    sw.WriteLine("************** HORA ******************");
                    sw.WriteLine("Erro ao executar " + DateTime.Now.ToString());
                    sw.WriteLine("Exceção: " + ex.ToString());
                    sw.WriteLine("**************************************");
                }
            }
        }

        #endregion

    }
}

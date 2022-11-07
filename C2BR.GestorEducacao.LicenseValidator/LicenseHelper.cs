//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: Lincença
// OBJETIVO: Funções auxiliares de linceça
// DATA DE CRIAÇÃO: ??/??/????
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA      |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// -----------+----------------------------+-------------------------------------
// 20/03/2013 | Caio Mendonça              | Atualização das função de validar licença, inserção das regras de NTP, bios e contador
//
//
//  




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.LicenseValidator
{
    public static class LicenseHelper
    {
        public static string GetLicenseString(License lic)
        {
            try
            {
                Exception ex;
                string xml;
                if (!SerializableHelper.Serialize<License>(lic, out xml, out ex))
                    throw ex;

                string signed = Signer.SignXml(xml);
                if (string.IsNullOrEmpty(signed))
                    return null;

                return Signer.EncryptXml(signed);
            }
            catch { throw; }
        }

        public static bool Validate(int codInst, out string messagem, out License lic)
        {
            messagem = null;
            lic = null;

            var inst = TB000_INSTITUICAO.RetornaTodosRegistros()
                .FirstOrDefault(x => x.ORG_CODIGO_ORGAO == codInst);

            if (inst == null)
            {
                messagem = "Não existe instituição associada ao código informado. Entre em contato com o suporte.";
                return false;
            }

            if (string.IsNullOrEmpty(inst.ORG_LICENCA))
            {
                messagem = "Não existe uma licença associada a instituição. Entre em contato com o suporte.";
                return false;
            }

            Exception ex;
            string xmlLic = Signer.DecryptXml(inst.ORG_LICENCA);
            if (!SerializableHelper.Deserialize<License>(xmlLic, out lic, out ex))
            {
                messagem = ex.ToString();
                return false;
            }

            if (!Signer.VerifySignature(xmlLic))
            {
                messagem = "Assinatura da licença inválida. Entre em contato com o suporte.";
                return false;
            }

            if (inst.ORG_NUMERO_CNPJ != decimal.Parse(lic.Cnpj)
                || lic.ContatoCpf != inst.NU_CPF_RESPO_CONTR
                || lic.ContatoDataNasc.Date != inst.DT_NASCTO_RESPO_CONTR
                || lic.ContratoData.Date != inst.DT_INI_CONTR
                || lic.ContratoNumero != inst.ORG_NUMERO_CONTR)
            {
                messagem = "Licença Inválida para a instituição. Entre em contato com o suporte.";
                return false;
            }



            // ORG_VALIDACAO1 => dados da máquina (biosname + bioselementid + biosversion), MD5
            // ORG_VALIDACAO2 => data que foi rodado o serviço, datetime.now, encriptografa a partir do MD5 do ORG_VALIDACAO1
            // ORG_VALIDACAO3 => data do ntp, encriptografa a partir do MD5 do ORG_VALIDACAO1
            // ORG_VALIDACAO4 => contador, encriptograf, encriptografa a partir do valor do ORG_VALIDACAO2

            Criptografia cripto = new Criptografia();
            string md5bios = lic.BiosName + lic.BiosElementId + lic.BiosVersion;
            md5bios = cripto.CalculaMD5(md5bios);


            if (lic.Local == true)
                md5bios = inst.ORG_VALIDACAO1;

            // Posteriormente aqui será o valor do NTP
            DateTime dataAtual = DateTime.Now;

            if (lic.Local == false && lic.BiosName == null)
            {
                // licença antiga
            }
            else
            {
                //*********************** LICENÇA NOVA ******************************
                if (String.IsNullOrEmpty(inst.ORG_VALIDACAO1) && String.IsNullOrEmpty(inst.ORG_VALIDACAO2) && String.IsNullOrEmpty(inst.ORG_VALIDACAO3))
                {
                    messagem = "Serviço de validação da licença de uso não está ativo. Entre em contato com o suporte.";
                    return false;
                }
                else
                {
                    if (String.IsNullOrEmpty(inst.ORG_VALIDACAO3))
                    {
                        messagem = "Licença corrompida, código 3. Entre em contato com o suporte.";
                        return false;
                    }

                    // Valida se o valor do campo está correto
                    try
                    {
                        dataAtual = Convert.ToDateTime(cripto.Decrypt(inst.ORG_VALIDACAO3, md5bios));
                    }
                    catch (Exception e)
                    {
                        messagem = "Erro no serviço de validação da licença de uso, código 3. Entre em contato com o suporte.";
                        return false;
                    }
                }
                //**********************************************************************
            }

            if (lic.DataInicio.Date > dataAtual.Date || lic.DataFim.Date < dataAtual.Date)
            {
                messagem = "Licença expirada, código D. Entre em contato com o suporte.";
                return false;
            }


            if (lic.Local == false && lic.BiosName == null)
            {
                // licença antiga
            }
            else
            {
                //******************* LICENÇA NOVA ******************************************
                if (!lic.Local)
                {

                    if (String.IsNullOrEmpty(inst.ORG_VALIDACAO1))
                    {
                        messagem = "Licença corrompida, código 1. Entre em contato com o suporte.";
                        return false;
                    }

                    // Validação do hash dos dados da bios
                    if (inst.ORG_VALIDACAO1 != md5bios)
                    {
                        messagem = "Servidor não cadastrado para utilizar a licença. Entre em contato com o suporte.";
                        return false;
                    }
                }

                if (String.IsNullOrEmpty(inst.ORG_VALIDACAO2))
                {
                    messagem = "Licença corrompida, código 2. Entre em contato com o suporte.";
                    return false;
                }

                // Pega data que o serviço foi atualizado, datetime.now
                DateTime dataAtualizacao = DateTime.Now;

                // Valida se o valor do campo está correto
                try
                {
                    dataAtualizacao = Convert.ToDateTime(cripto.Decrypt(inst.ORG_VALIDACAO2, md5bios));
                }
                catch (Exception e)
                {
                    messagem = "Erro no serviço de validação da licença de uso, código 2. Entre em contato com o suporte.";
                    return false;
                }

                // Valida data de atualização do serviço, 24 horas antes ou depois
                TimeSpan timespan = DateTime.Now.Subtract(dataAtualizacao);
                if (timespan.TotalHours >= 24)
                {
                    messagem = "Serviço de validação da licença de uso não está ativo. Entre em contato com o suporte.";
                    return false;
                }

                // Quando o usuário mexer na data
                if (dataAtualizacao.Date != dataAtual.Date)
                {
                    messagem = "Licença corrompida, código 23. Entre em contato com o suporte.";
                    return false;
                }

                // Quando o usuário mexer na data e parar o serviço
                if (DateTime.Now.Date < dataAtual.Date)
                {
                    messagem = "Licença corrompida, código A3. Entre em contato com o suporte.";
                    return false;
                }

                if (String.IsNullOrEmpty(inst.ORG_VALIDACAO4))
                {
                    messagem = "Licença corrompida, código 4. Entre em contato com o suporte.";
                    return false;
                }

                // Valida contador
                try
                {
                    if (Convert.ToDouble(cripto.Decrypt(inst.ORG_VALIDACAO4, inst.ORG_VALIDACAO2)) <= 0)
                    {
                        messagem = "Licença expirada, código C. Entre em contato com o suporte.";
                        return false;
                    }
                }
                catch (Exception e)
                {
                    messagem = "Erro no serviço de validação da licença de uso, código 4. Entre em contato com o suporte.";
                    return false;
                }
                //*****************************************************************************

            }

            return true;
        }

        public static License RetornaLicenca(string licenca)
        {
            string mensagem = null;
            License lic = null;

            Exception ex;
            string xmlLic = Signer.DecryptXml(licenca);
            if (!SerializableHelper.Deserialize<License>(xmlLic, out lic, out ex))
            {
                mensagem = ex.ToString();
                return null;
            }

            return lic;
        }

    }
}

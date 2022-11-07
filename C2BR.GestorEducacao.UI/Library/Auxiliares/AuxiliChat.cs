//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
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
    public class AuxiliChat
    {
        public TipoMsg type;

        public string[] Tipo(TipoMsg tipo)
        {
            string[] tp = new string[2];
            ADMUSUARIO usu = ADMUSUARIO.RetornaPelaChavePrimaria(LoginAuxili.IDEADMUSUARIO);
            switch (tipo)
            {
                case TipoMsg.Inicio:
                    tp[0] = usu.desLogin + " criou a sala";
                    tp[1] = "I";
                    break;
                case TipoMsg.Entrar:
                    tp[0] = usu.desLogin + " entrou na sala";
                    tp[1] = "E";
                    break;
                case TipoMsg.Msg:
                    tp[0] = "";
                    tp[1] = "M";
                    break;
                case TipoMsg.Sair:
                    tp[0] = usu.desLogin + " saiu da sala";
                    tp[1] = "S";
                    break;
            }
            return tp;
        }

        public void enviaMsg(TipoMsg tipo, int coSala, string msg = null)
        {
            string[] tp;
            
            TB170_MENSAGEM m = new TB170_MENSAGEM();
            ADMUSUARIO usu = ADMUSUARIO.RetornaPelaChavePrimaria(LoginAuxili.IDEADMUSUARIO);
            TB161_SALA sala = TB161_SALA.RetornaPelaChavePrimaria(coSala);

            tp = Tipo(tipo);

            m.ADMUSUARIO = usu;
            m.TB161_SALA = sala;
            m.DE_MSG = tp[0] != "" ? tp[0] : msg;
            m.DT_MSG = DateTime.Now;
            m.DT_SITUA = DateTime.Now;
            m.CO_SITUA = "A";
            m.CO_TIPO_MSG = tp[1];

            TB170_MENSAGEM.SaveOrUpdate(m, true);
        }

        public bool sairSala(int coSala, int idUsu, int coEmp)
        {
            try
            {
                ADMUSUARIO usu = ADMUSUARIO.RetornaPelaChavePrimaria(idUsu);
                TB161_SALA sala = TB161_SALA.RetornaPelaChavePrimaria(coSala);
                TB163_SALA_USUARIO lusu = TB163_SALA_USUARIO.RetornaPelaChavePrimaria(coSala, idUsu);

                lusu.CO_SITUA = "F";
                TB163_SALA_USUARIO.SaveOrUpdate(lusu, true);

                if (sala.ADMUSUARIO.ideAdmUsuario == lusu.ADMUSUARIO.ideAdmUsuario)
                {
                    sala.CO_SITUA = "F";
                    TB161_SALA.SaveOrUpdate(sala, true);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void enviaConvite(int coSala, int idUsu)
        {
            TB161_SALA sala = TB161_SALA.RetornaPelaChavePrimaria(coSala);
            ADMUSUARIO usua = ADMUSUARIO.RetornaPelaChavePrimaria(idUsu);

            TB163_SALA_USUARIO lusu = new TB163_SALA_USUARIO();

            lusu.ADMUSUARIO = usua;
            lusu.TB161_SALA = sala;
            lusu.CO_SITUA = "P";
            lusu.DT_SITUA = DateTime.Now;
            lusu.DT_SALA_USUARIO = DateTime.Now;

            TB163_SALA_USUARIO.SaveOrUpdate(lusu, true);
        }

        public int criaSala(string noSala, int idUsu, int coEmp)
        {
            ADMUSUARIO usu = ADMUSUARIO.RetornaPelaChavePrimaria(idUsu);

            TB161_SALA sala = new TB161_SALA();

            sala.ADMUSUARIO = usu;
            sala.NO_SALA = noSala;
            sala.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
            sala.DT_CRIACAO = DateTime.Now;
            sala.CO_SITUA = "A";
            sala.DT_SITUA = DateTime.Now;

            sala = TB161_SALA.SaveOrUpdate(sala);

            TB163_SALA_USUARIO lusu = new TB163_SALA_USUARIO();

            lusu.TB161_SALA = sala;
            lusu.ADMUSUARIO = usu;
            lusu.DT_SALA_USUARIO = DateTime.Now;
            lusu.CO_SITUA = "A";
            lusu.DT_SITUA = DateTime.Now;

            TB163_SALA_USUARIO.SaveOrUpdate(lusu, true);

            enviaMsg(TipoMsg.Inicio, sala.CO_SALA);

            return sala.CO_SALA;
        }
    }

    public enum TipoMsg { Msg, Inicio, Entrar, Sair }
}
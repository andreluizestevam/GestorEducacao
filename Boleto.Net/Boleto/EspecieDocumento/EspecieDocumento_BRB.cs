using System;
using System.Collections.Generic;
using System.Text;

namespace BoletoNet
{
    #region Enumerado

    public enum EnumEspecieDocumento_BRB
    {
        DuplicataMercantil = 21, //DM – DUPLICATA MERCANTIL
        DuplicataPrestacao = 31, //DM – DUPLICATA PRESTAÇÃO
        NotaPromissoria = 22, //NP – NOTA PROMISSÓRIA
        Recibo = 25, //RC – RECIBO
        Outros = 39 //OUTROS
    }

    #endregion

    public class EspecieDocumento_BRB : AbstractEspecieDocumento, IEspecieDocumento
    {
        #region Construtores

        public EspecieDocumento_BRB()
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        public EspecieDocumento_BRB(int codigo)
        {
            try
            {
                this.carregar(codigo);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        #endregion

        #region Metodos Privados

        private void carregar(int idCodigo)
        {
            try
            {
                this.Banco = new Banco_BRB();

                switch ((EnumEspecieDocumento_BRB)idCodigo)
                {
                    case EnumEspecieDocumento_BRB.DuplicataMercantil:
                        this.Codigo = (int)EnumEspecieDocumento_BRB.DuplicataMercantil;
                        this.Especie = "DUPLICATA MERCANTIL";
                        this.Sigla = "DM";
                        break;
                    case EnumEspecieDocumento_BRB.DuplicataPrestacao:
                        this.Codigo = (int)EnumEspecieDocumento_BRB.DuplicataPrestacao;
                        this.Especie = "DUPLICATA DE SERVIÇO";
                        this.Sigla = "DS";
                        break;
                    case EnumEspecieDocumento_BRB.NotaPromissoria:
                        this.Codigo = (int)EnumEspecieDocumento_BRB.NotaPromissoria;
                        this.Especie = "NOTA PROMISSÓRIA";
                        this.Sigla = "NP";
                        break;
                    case EnumEspecieDocumento_BRB.Recibo:
                        this.Codigo = (int)EnumEspecieDocumento_BRB.Recibo;
                        this.Especie = "RECIBO";
                        this.Sigla = "RC";
                        break;
                    case EnumEspecieDocumento_BRB.Outros:
                        this.Codigo = (int)EnumEspecieDocumento_BRB.Outros;
                        this.Especie = "OUTROS";
                        this.Sigla = "OUTROS";
                        break;
                    default:
                        this.Codigo = 0;
                        this.Especie = "( Selecione )";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        public static EspeciesDocumento CarregaTodas()
        {
            try
            {
                EspeciesDocumento alEspeciesDocumento = new EspeciesDocumento();

                alEspeciesDocumento.Add(new EspecieDocumento_BRB((int)EnumEspecieDocumento_BRB.DuplicataMercantil));
                alEspeciesDocumento.Add(new EspecieDocumento_BRB((int)EnumEspecieDocumento_BRB.DuplicataPrestacao));
                alEspeciesDocumento.Add(new EspecieDocumento_BRB((int)EnumEspecieDocumento_BRB.NotaPromissoria));
                alEspeciesDocumento.Add(new EspecieDocumento_BRB((int)EnumEspecieDocumento_BRB.Recibo));
                alEspeciesDocumento.Add(new EspecieDocumento_BRB((int)EnumEspecieDocumento_BRB.Outros));

                return alEspeciesDocumento;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar objetos", ex);
            }
        }

        #endregion
    }
}

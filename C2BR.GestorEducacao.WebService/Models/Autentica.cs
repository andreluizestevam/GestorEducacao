using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.WebService.Models
{
    public class Autentica
    {
        public bool Autenticacao(string Login, string Senha)
        {
            try
            {
                var admUsuario = (from tbs384 in TBS384_USUAR_APP.RetornaTodosRegistros()
                                  where tbs384.DE_LOGIN.Equals(Login) && tbs384.DE_SENHA.Equals(Senha)
                                  select new
                                  {
                                      CodUsuario = tbs384.CO_COL,
                                  }).FirstOrDefault();

                if (admUsuario != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Informação:" + ex.Message);
            }

        }

    }
}
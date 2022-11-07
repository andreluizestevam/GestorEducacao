//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Configuration;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.DLLRelatorioWeb.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Configuration lcgfConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ServiceModelSectionGroup lsrvServiceModel = ServiceModelSectionGroup.GetSectionGroup(lcgfConfiguration);

            foreach (ServiceElement litmServiceItem in lsrvServiceModel.Services.Services)
            {
                ServiceHost lsrvServiceHost = new ServiceHost(GetTypeService(litmServiceItem.Name), litmServiceItem.Endpoints[0].Address);
                lsrvServiceHost.Open();
            }

            System.Console.WriteLine("C2BR RelatorioWeb: Remote Server On. Waiting request.");
            System.Console.ReadLine();
        }

        static Type GetTypeService(string strServiceName)
        {
            Assembly itemAssembly = Assembly.Load("C2BR.GestorEducacao.DLLRelatorioWeb");
            int intPosition = (strServiceName.LastIndexOf(".") + 1);
            strServiceName = strServiceName.Substring(intPosition, (strServiceName.Length - intPosition));

            var resultado = from lAssembly in itemAssembly.GetTypes()
                            where lAssembly.Name == strServiceName && lAssembly.IsClass
                            select lAssembly;

            return resultado.ToArray().First();
        }
    }
}

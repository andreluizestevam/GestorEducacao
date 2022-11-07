//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Reflection;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Configuration;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.DLLRelatorioWeb.Service
{
    public partial class C2BRDLLRelatorioWebService : ServiceBase
    {
        public C2BRDLLRelatorioWebService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Configuration lcgfConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ServiceModelSectionGroup lsrvServiceModel = ServiceModelSectionGroup.GetSectionGroup(lcgfConfiguration);

            foreach (ServiceElement litmServiceItem in lsrvServiceModel.Services.Services)
            {
                ServiceHost lsrvServiceHost = new ServiceHost(GetTypeService(litmServiceItem.Name), litmServiceItem.Endpoints[0].Address);
                lsrvServiceHost.Open();
            }
        }

        protected override void OnStop()
        {
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

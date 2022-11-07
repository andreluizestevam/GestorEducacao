using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Threading;

namespace C2BR.GestorEducacao.ServicoValidacao
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        // Função chamada ao inicializar serviço
        protected override void OnStart(string[] args)
        {
            ThreadStart ts = new ThreadStart(Executar);

            var work = new Thread(ts);

            work.Start();
        }

        private void Executar()
        {
            try
            {
                // Ativa loop
                Processo p = new Processo();
                p.StartTimer();
            }
            catch (Exception ex)
            {
                // Grava erro no log caso não inicialize
                using (StreamWriter sw = new StreamWriter("C:\\" + "erroServico.txt", true))
                {
                    sw.WriteLine(ex.ToString());
                }
            }
        }

        // Função chamada ao parar serviço
        protected override void OnStop()
        {
            Processo p = new Processo();
            p.StopTimer();
        }
    }
}

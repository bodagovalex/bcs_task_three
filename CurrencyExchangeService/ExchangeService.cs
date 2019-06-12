using CurrencyExchangeService.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeService
{
    public partial class ExchangeService : ServiceBase
    {
        public ExchangeService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            UpdateServices.Start();
        }

        protected override void OnStop()
        {
            UpdateServices.Stop();
        }
    }
}

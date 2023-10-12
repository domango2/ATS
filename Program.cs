using System;
using System.Text.Json;

namespace ATS
{
    class Program
    {
        static void Main(string[] args)
        {
            ATScompany company = ATScompany.Instance;


            company.LoadAll();

            company.PrintAllClients();

            company.PrintAllCalls();

            company.PrintAllInvoices();


            //Client client1 = ATScompany.Instance.Clients[1];
            //client1.PrintClientInfo();
        }
    }
}
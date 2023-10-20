using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ATS
{
    public class ATScompany
    {
        public static ATScompany Instance { get; } = new ATScompany();
        public List<Client> Clients { get; set; }

        public List<Call> Calls { get; set; }

        public List<Invoice> Invoices { get; set; }

        private ATScompany() 
        {
            Clients = new List<Client>();
            Calls = new List<Call>();
            Invoices = new List<Invoice>();
        }

        public void AddClient(Client client)
        {
            Clients.Add(client);
        }

        public void AddCall(Call call)
        {
            Calls.Add(call);
        }

        public void AddInvoice(Invoice invoice)

        {
            Invoices.Add(invoice);
        }

        public void SendInvoice()
        {
            DateTime currentDate = DateTime.Now;

            foreach (var client in Clients)
            {
                var callsAfterLastInvoice = client.CallHistory
                    .Where(call => call.CallDate > client.LastInvoiceDate)
                    .ToList();

                if (callsAfterLastInvoice.Count > 0)
                {
                    decimal callCost = callsAfterLastInvoice.Sum(call => call.Cost);

                    if (callCost == (decimal)0.0) break; 
                    Invoice invoice = new Invoice(currentDate, currentDate.AddDays(10), callCost, client.Id);

                    client.Invoices.Add(invoice);
                    Invoices.Add(invoice);

                    client.LastInvoiceDate = currentDate;
                }
            }
        }



        public void PrintAllClients()
        {
            Console.WriteLine("\n\nСписок клиентов:");
            foreach (var client in Clients)
            {
                Console.WriteLine();

                Console.WriteLine(client.ToString()); 
            }
        }

        public void PrintAllCalls()
        {
            Console.WriteLine("\n\nСписок разговоров:");
            foreach (var call in Calls)
            {
                Console.WriteLine();

                Console.WriteLine(call.ToString());
            }
        }

        public void PrintAllInvoices()
        {
            Console.WriteLine("\n\nСписок счетов:");
            foreach (var invoice in Invoices)
            {
                Console.WriteLine();

                Console.WriteLine(invoice.ToString());
            }
        }

        public Client? FindClientById(string clientId)
        {
            return Clients.FirstOrDefault(client => client.Id == clientId);
        }

        public Client? FindClient(string inputLogin, string inputPassword)
        {
            return Clients.FirstOrDefault(client => (client.Login == inputLogin && client.Password == inputPassword));
        }

        public void Authorization()
        {
            Console.Write("Введите логин -> ");
            string inputLogin = Console.ReadLine();
            Console.Write("Введите пароль -> ");
            string inputPassword = Console.ReadLine();
            if (inputLogin != "admin" && inputPassword != "admin")
            {
                Client client1 = FindClient(inputLogin, inputPassword);
                client1.PrintClientInfo();
            }
            else
            {
                Console.WriteLine("\nПОЛУЧЕН ДОСТУП АДМИНИСТРАТОРА");
            }
        }

        public void SaveAll()
        {
            string dataClients = "dataClients.json";
            string dataCalls = "dataCalls.json";
            string dataInvoices = "dataInvoices.json";

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;

            var jsonClients = JsonSerializer.Serialize(Clients , options);
            var jsonCalls = JsonSerializer.Serialize(Calls, options);
            var jsonInvoices = JsonSerializer.Serialize(Invoices, options);

            File.WriteAllText(dataClients, jsonClients);
            File.WriteAllText(dataCalls, jsonCalls);
            File.WriteAllText(dataInvoices, jsonInvoices);

            Console.WriteLine("\nДанные сохранены.");
        }


        public void LoadAll()
        {
            string dataClients = "dataClients.json";
            string dataCalls = "dataCalls.json";
            string dataInvoices = "dataInvoices.json";

            if (File.Exists(dataClients))
            {
                var jsonClients = File.ReadAllText(dataClients);
                var deserializedClients = JsonSerializer.Deserialize<List<Client>>(jsonClients);
                if (deserializedClients != null)
                {
                    if (File.Exists(dataCalls))
                    {
                        var jsonCalls = File.ReadAllText(dataCalls);
                        var deserializedCalls = JsonSerializer.Deserialize<List<Call>>(jsonCalls);
                    }

                    if (File.Exists(dataInvoices))
                    {
                        var jsonInvoices = File.ReadAllText(dataInvoices);
                        var deserializedInvoices = JsonSerializer.Deserialize<List<Invoice>>(jsonInvoices);
                    }
                }
            }
        }
    }
}

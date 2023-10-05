using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ATS
{
    public class Client
    {
        // Поля
        private string id = "";       
        private string name = "";         
        private string address = "";      
        private string phoneNumber = "";
        private DateTime birthDate;
        private decimal debt = 0;
        private decimal overpayment = 0;

        public List<Call> CallHistory { get; set; }   
        public List<Invoice> Invoices { get; set; }
        public string PhoneNumber { get; private set; } 
        public static int NextClientId { get; private set; } = 1;
        public DateTime LastInvoiceDate { get; set; }


        public string Id
        {
            get { return id; }
            private set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        public decimal Debt
        {
            get 
            {
                decimal unpaidInvoicesDebt = Invoices.Where(i => !i.IsPaid).Sum(i => i.Amount - i.PaidAmount);
                decimal overdueInterestDebt = Invoice.CalculateLateFee(Invoices);
                return unpaidInvoicesDebt + overdueInterestDebt;
            }
            private set { debt = Debt; }
        }

        public decimal Overpayment
        {
            get { return overpayment; }
            set { overpayment = value; }
        }

        public Client(string name, string address, DateTime birthDate)
        {
            Id = GenerateUniqueClientId();

            Name = name;
            Address = address;
            BirthDate = birthDate;

            CallHistory = new List<Call>();
            Invoices = new List<Invoice>();

            PhoneNumber = GeneratePhoneNumber(int.Parse(Id));
            LastInvoiceDate = DateTime.Today.AddDays(-1);
        }

        private string GenerateUniqueClientId()
        {
            string newClientId;

            do
            {
                newClientId = GenerateRandomClientId();
            }
            while (ClientIdExists(newClientId));

            return newClientId;
        }

        private string GenerateRandomClientId()
        {
            Random random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private bool ClientIdExists(string clientId)
        {
            return ATScompany.Instance.Clients.Any(client => client.Id == clientId);
        }

        private string GeneratePhoneNumber(int clientId)
        {
            return $"8-0152-{clientId:D6}"; // Пример генерации номера
        }

        public override string ToString()
        {
            return $"Клиент: {Name}, \nТелефон: {PhoneNumber}, \nАдрес: {Address}, \nЗадолженность: {Debt}";
        }

        public void MakeCall(DateTime callDate, int duration, bool isIncoming, string clientId)
        {
            Call call = new Call(callDate, duration, isIncoming, clientId);

            CallHistory.Add(call);
            ATScompany.Instance.Calls.Add(call);
        }

        public void PrintClientInfo()
        {
            Console.WriteLine($"Имя: {Name}");
            Console.WriteLine($"Адрес: {Address}");
            Console.WriteLine($"Дата рождения: {BirthDate.ToShortDateString()}");
            Console.WriteLine($"Номер телефона: {PhoneNumber}");
            Console.WriteLine($"Текущая задолженность: {Debt:C}");
            if (Overpayment > 0) Console.WriteLine($"Переплата: {Overpayment:C}");


            Console.WriteLine("История звонков:");
            foreach (var call in CallHistory)
            {
                Console.WriteLine($"{call.CallDate}, Длительность: {call.Duration}");
            }

            Console.WriteLine("Список счетов:");
            foreach (var invoice in Invoices)
            {
                Console.WriteLine($"{invoice.InvoiceDate}: {invoice.Amount:C} - {invoice.PaidAmount}");
            }
        }

        public void ReceiveInvoice()
        {
            bool unpaidInvoicesExist = Invoices.Any(i => !i.IsPaid);
            bool overdueInterestExists = Debt > 0;
            decimal lateFee = Invoice.CalculateLateFee(Invoices);

            if (unpaidInvoicesExist || overdueInterestExists)
            {
                Console.WriteLine("У вас есть финансовые обязательства:");

                if (unpaidInvoicesExist)
                {
                    Console.WriteLine("Неоплаченные счета:");
                    foreach (var unpaidInvoice in Invoices.Where(i => !i.IsPaid))
                    {
                        Console.WriteLine($"- Счет за {unpaidInvoice.InvoiceDate}, Сумма: {unpaidInvoice.Amount:C}");
                    }
                }

                if (overdueInterestExists)
                {
                    Console.WriteLine($"Задолженность по процентам просрочки оплаты: {lateFee:C}");
                }

                Console.WriteLine($"Итого к оплате: {Debt}");
            }
            else
            {
                Console.WriteLine("Ваши финансовые обязательства полностью погашены.");
            }
        }

        public void PayInvoice(decimal payment)
        {
            bool unpaidInvoicesExist = Invoices.Any(i => !i.IsPaid);
            decimal lateFee = Invoice.CalculateLateFee(Invoices);
            bool overdueInterestExists = lateFee > 0;

            decimal remainingPayment = payment;

            if (unpaidInvoicesExist || overdueInterestExists)
            {
                if (unpaidInvoicesExist)
                {
                    foreach (var unpaidInvoice in Invoices.Where(i => !i.IsPaid))
                    {
                        decimal invoiceAmount = unpaidInvoice.Amount - unpaidInvoice.PaidAmount;

                        if (remainingPayment >= invoiceAmount)
                        {
                            unpaidInvoice.IsPaid = true;
                            unpaidInvoice.PaidAmount = invoiceAmount;
                            remainingPayment -= invoiceAmount;
                        }
                        else
                        {
                            unpaidInvoice.PaidAmount += remainingPayment;
                            remainingPayment = 0;
                        }

                        if (remainingPayment <= 0)
                            break;
                    }
                }

                if (overdueInterestExists)
                {
                    if (remainingPayment >= lateFee)
                    {
                        Debt = 0;
                        remainingPayment -= lateFee;
                    }
                    else
                    {
                        Debt -= remainingPayment;
                        remainingPayment = 0;
                        Console.WriteLine($"Оставшаяся задолженность: {Debt}");
                    }
                }
                Console.WriteLine("Операция выполнена.");
            }
            else
            {
                Console.WriteLine("Все счета оплачены. Внесённая сумма сохранена как переплата.");
            }

            if (remainingPayment > 0)
            {
                Overpayment = remainingPayment;
            }
        }

        public void AddOverpayment(decimal amount)
        {
            Overpayment += amount;
        }
    }
}

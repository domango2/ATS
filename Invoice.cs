using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC
{
    public class Invoice
    {
        // Поля
        private int _invoiceId;         // Идентификатор счета
        private DateTime _issuedDate;   // Дата выставления счета
        private DateTime _dueDate;      // Дата платежа
        private decimal _amount;        // Сумма счета
        private decimal _penalty;       // Пеня

        // Свойства
        public int InvoiceId
        {
            get { return _invoiceId; }
            set { _invoiceId = value; }
        }

        public DateTime IssuedDate
        {
            get { return _issuedDate; }
            set { _issuedDate = value; }
        }

        public DateTime DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }

        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public decimal Penalty
        {
            get { return _penalty; }
            set { _penalty = value; }
        }

        public Client Client { get; set; } // Связь с клиентом, которому выставлен счет

        // Конструктор для создания объекта счета
        public Invoice(int invoiceId, DateTime issuedDate, DateTime dueDate, decimal amount)
        {
            _invoiceId = invoiceId;
            _issuedDate = issuedDate;
            _dueDate = dueDate;
            _amount = amount;
            _penalty = 0; // Изначально пеня равна нулю
        }

        // Метод для рассчета пени за просрочку платежа
        public decimal CalculatePenalty()
        {
            if (DateTime.Now > _dueDate)
            {
                // Рассчитываем пени как 1% от суммы за каждый день просрочки
                decimal daysLate = (decimal)(DateTime.Now - _dueDate).TotalDays;
                _penalty = _amount * (0.01m * daysLate);
            }
            else
            {
                _penalty = 0; // Нет просрочки, пеня равна нулю
            }

            return _penalty;
        }
    }

}

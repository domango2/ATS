using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC
{
    public class Call
    {
        // Поля
        private int _callId;            // Идентификатор разговора
        private DateTime _startTime;    // Дата и время начала разговора
        private TimeSpan _duration;     // Длительность разговора
        private decimal _ratePerMinute; // Тариф за минуту разговора

        // Свойства
        public int CallId
        {
            get { return _callId; }
            set { _callId = value; }
        }

        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        public TimeSpan Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public decimal RatePerMinute
        {
            get { return _ratePerMinute; }
            set { _ratePerMinute = value; }
        }

        private List<Call> _callHistory = new List<Call>();


        public Client Client { get; set; } // Связь с клиентом, осуществившим разговор

        // Конструктор для создания объекта разговора
        public Call(int callId, DateTime startTime, TimeSpan duration, decimal ratePerMinute)
        {
            _callId = callId;
            _startTime = startTime;
            _duration = duration;
            _ratePerMinute = ratePerMinute;
        }

        // Метод для рассчета стоимости разговора
        public decimal CalculateCost()
        {
            // Рассчитываем стоимость разговора на основе длительности и тарифа
            decimal cost = _ratePerMinute * (decimal)_duration.TotalMinutes;
            return cost;
        }
    }

}

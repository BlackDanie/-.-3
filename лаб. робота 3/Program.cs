
using System;

namespace TouristTours
{
    // Базовый класс Tour
    public class Tour
    {
        // Поля
        protected string destination;
        protected int duration;
        protected decimal price;

        // Стоимость за день (по умолчанию)
        protected decimal basePricePerDay = 200m;

        // Свойства
        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        public int Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                UpdatePrice(); // Автоматическое обновление цены при изменении длительности
            }
        }

        public decimal Price
        {
            get { return price; }
        }

        // Конструктор по умолчанию
        public Tour()
        {
            destination = "Unknown Destination";
            duration = 3;
            UpdatePrice();
        }

        // Параметризованный конструктор
        public Tour(string destination, int duration)
        {
            this.destination = destination;
            this.duration = duration;
            UpdatePrice();
        }

        // Метод для обновления цены
        protected virtual void UpdatePrice()
        {
            price = duration * basePricePerDay;
        }

        // Методы
        public virtual void BookTour()
        {
            Console.WriteLine("Тур до " + destination + " на " + duration + " днів заброньовано.");
        }

        public virtual void GetTourDetails()
        {
            Console.WriteLine($"Місце призначення: {destination}, Тривалість: {duration} днів, Ціна: {price}.");
        }

        // Метод для расчета общей стоимости тура (можно расширить для доп. услуг)
        public virtual decimal CalculateTotalCost()
        {
            return price;
        }

        // Перегрузка оператора +
        public static Tour operator +(Tour t1, Tour t2)
        {
            string combinedDestination = t1.Destination + " и " + t2.Destination;
            int combinedDuration = t1.Duration + t2.Duration;
            decimal combinedPrice = t1.Price + t2.Price;

            return new Tour(combinedDestination, combinedDuration);
        }
    }

    // Дочерний класс AdventureTour
    public class AdventureTour : Tour
    {
        // Дополнительное поле
        private string difficultyLevel;

        // Стоимость за день для приключенческого тура
        private decimal adventurePricePerDay = 250m;

        // Конструктор
        public AdventureTour(string destination, int duration, string difficultyLevel)
            : base(destination, duration)
        {
            this.difficultyLevel = difficultyLevel;
            basePricePerDay = adventurePricePerDay; // Устанавливаем свою цену за день
            UpdatePrice();
        }

        // Переопределенный метод BookTour
        public override void BookTour()
        {
            Console.WriteLine("Пригодницький тур до " + destination + " заброньовано. Рівень складності: " + difficultyLevel + ".");
        }

        // Дополнительный метод
        public void SetDifficultyLevel(string level)
        {
            difficultyLevel = level;
            Console.WriteLine("Рівень складності туру змінено на " + level + ".");
        }
    }

    // Дочерний класс RelaxationTour
    public class RelaxationTour : Tour
    {
        // Дополнительное поле
        private bool spaIncluded;

        // Стоимость за день для релаксационного тура
        private decimal relaxationPricePerDay = 300m;

        // Конструктор
        public RelaxationTour(string destination, int duration, bool spaIncluded)
            : base(destination, duration)
        {
            this.spaIncluded = spaIncluded;
            basePricePerDay = relaxationPricePerDay; // Устанавливаем свою цену за день
            UpdatePrice();
        }

        // Переопределенный метод BookTour
        public override void BookTour()
        {
            Console.WriteLine("Релаксаційний тур до " + destination + " заброньовано. Спа: " + (spaIncluded ? "включено" : "не включено") + ".");
        }

        // Дополнительный метод
        public void IncludeSpa()
        {
            spaIncluded = true;
            Console.WriteLine("Спа включено до туру.");
        }

        // Переопределенный метод CalculateTotalCost (добавляет стоимость спа, если включено)
        public override decimal CalculateTotalCost()
        {
            decimal totalPrice = price;
            if (spaIncluded)
            {
                totalPrice += 200; // Дополнительная стоимость за спа
            }
            return totalPrice;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Выбор типа тура
            Console.WriteLine("Выберите тип тура: 1 - Приключенческий, 2 - Релаксационный");
            string tourType = Console.ReadLine();

            Tour selectedTour = null;

            // Если приключенческий тур
            if (tourType == "1")
            {
                Console.WriteLine("Введите место назначения для приключенческого тура (например, 'Горы'):");
                string destination = Console.ReadLine();

                Console.WriteLine("Введите длительность тура (например, 5 дней):");
                int duration = int.Parse(Console.ReadLine());

                Console.WriteLine("Выберите уровень сложности: Легкий, Средний, Высокий");
                string difficultyLevel = Console.ReadLine();

                selectedTour = new AdventureTour(destination, duration, difficultyLevel);
                selectedTour.BookTour();
            }
            // Если релаксационный тур
            else if (tourType == "2")
            {
                Console.WriteLine("Введите место назначения для релаксационного тура (например, 'Мальдіви'):");
                string destination = Console.ReadLine();

                Console.WriteLine("Введите длительность тура (например, 7 дней):");
                int duration = int.Parse(Console.ReadLine());

                Console.WriteLine("Включить спа? (да/нет)");
                string spaInput = Console.ReadLine();
                bool spaIncluded = spaInput.ToLower() == "да";

                selectedTour = new RelaxationTour(destination, duration, spaIncluded);
                selectedTour.BookTour();
            }

            // Вывод деталей тура
            if (selectedTour != null)
            {
                selectedTour.GetTourDetails();
                Console.WriteLine("Общая стоимость тура: " + selectedTour.CalculateTotalCost());
            }

            Console.WriteLine("Программа завершена.");
        }
    }
}


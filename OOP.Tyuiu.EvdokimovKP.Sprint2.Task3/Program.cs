using System;
using System.Collections.Generic;

namespace LibrarySystem
{
    // 1. БАЗОВЫЙ КЛАСС Book
    public class Book
    {
        // Обязательные атрибуты из Варианта № 5
        public string Title { get; set; }
        public string Author { get; set; }
        public int YearOfPublication { get; set; }

        // Дополнительные атрибуты (+3 по доп. заданию)
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int Rating { get; set; } = 5;

        public Book(string title, string author, int yearOfPublication, decimal price)
        {
            Title = title;
            Author = author;
            YearOfPublication = yearOfPublication;
            Price = price;
        }

        // Обязательные методы из Варианта № 5
        public virtual string GetInfo()
        {
            return $"\"{Title}\" - {Author} ({YearOfPublication} г.) | Цена: {Price} руб.";
        }

        // ПЕРЕГРУЗКА МЕТОДА Read (Overloading #1 - без параметров)
        public virtual void Read()
        {
            Console.WriteLine($"Вы читаете книгу: \"{Title}\".");
        }

        // ПЕРЕГРУЗКА МЕТОДА Read (Overloading #2 - с параметром страниц)
        public void Read(int pagesCount)
        {
            Console.WriteLine($"Вы прочитали {pagesCount} страниц книги \"{Title}\".");
        }

        public virtual void Borrow()
        {
            if (IsAvailable)
            {
                IsAvailable = false;
                Console.WriteLine($"Книга \"{Title}\" выдана на руки.");
            }
            else
            {
                Console.WriteLine($"Книга \"{Title}\" сейчас недоступна.");
            }
        }

        // Дополнительные методы (+3 по доп. заданию)
        public void ApplyDiscount(decimal discountPercent)
        {
            Price -= Price * (discountPercent / 100m);
            Console.WriteLine($"На книгу \"{Title}\" сделана скидка {discountPercent}%. Новая цена: {Price} руб.");
        }

        public void ReturnBook()
        {
            IsAvailable = true;
            Console.WriteLine($"Книга \"{Title}\" возвращена в библиотеку.");
        }
    }

    // 2. ПРОИЗВОДНЫЙ КЛАСС: Учебник (Textbook)
    public class Textbook : Book
    {
        public string Subject { get; set; } // Предмет

        public Textbook(string title, string author, int year, decimal price, string subject)
            : base(title, author, year, price)
        {
            Subject = subject;
        }

        // ПЕРЕКРЫТИЕ МЕТОДА Read() по Варианту № 5 (с информацией о предмете)
        public override void Read()
        {
            Console.WriteLine($"Вы изучаете учебник по предмету \"{Subject}\": \"{Title}\".");
        }
    }

    // 3. ПРОИЗВОДНЫЙ КЛАСС: Художественная литература (Fiction)
    public class Fiction : Book
    {
        public string Genre { get; set; } // Жанр

        public Fiction(string title, string author, int year, decimal price, string genre)
            : base(title, author, year, price)
        {
            Genre = genre;
        }

        // ПЕРЕКРЫТИЕ МЕТОДА Borrow() по Варианту № 5 (с информацией о жанре)
        public override void Borrow()
        {
            base.Borrow();
            Console.WriteLine($"  -> Категория: Художественная литература (Жанр: {Genre}).");
        }
    }

    // 4. ПРОИЗВОДНЫЙ КЛАСС: Научная литература (ScientificLiterature)
    public class ScientificLiterature : Book
    {
        public string FieldOfScience { get; set; } // Область науки

        public ScientificLiterature(string title, string author, int year, decimal price, string fieldOfScience)
            : base(title, author, year, price)
        {
            FieldOfScience = fieldOfScience;
        }

        // ПЕРЕКРЫТИЕ МЕТОДА GetInfo() по Варианту № 5 (с областью науки)
        public override string GetInfo()
        {
            return base.GetInfo() + $" | Область науки: {FieldOfScience}";
        }
    }

    // 5. GENERIC-КЛАСС (Обобщённое хранилище библиотечных книг)
    public class LibraryStorage<T> where T : Book
    {
        private List<T> _books = new List<T>();

        public void AddBook(T book)
        {
            _books.Add(book);
            Console.WriteLine($"[Хранилище] Добавлена книга: \"{book.Title}\"");
        }

        // Вызов полиморфного метода GetInfo() для всех книг в Generic-коллекции
        public void ShowAllBooks()
        {
            Console.WriteLine("\n--- Список всех книг в хранилище ---");
            foreach (var book in _books)
            {
                Console.WriteLine(book.GetInfo());
            }
        }
    }

    // ТОЧКА ВХОДА (Проверка работы)
    class Program
    {
        static void Main(string[] args)
        {
            // Создание объектов разных типов
            Textbook mathTextbook = new Textbook("Высшая математика", "Иванов И.И.", 2021, 1200m, "Математика");
            Fiction novel = new Fiction("Преступление и наказание", "Достоевский Ф.М.", 1866, 600m, "Роман");
            ScientificLiterature physics = new ScientificLiterature("Квантовая физика", "Петров П.П.", 2019, 1500m, "Физика");

            // 1. Демонстрация ПЕРЕКРЫТИЯ (Override) методов из Варианта № 5
            Console.WriteLine("=== 1. Перекрытие методов (Override) ===");
            mathTextbook.Read();   // Изменённый Read() для учебника
            novel.Borrow();        // Изменённый Borrow() для художественной литературы
            Console.WriteLine(physics.GetInfo()); // Изменённый GetInfo() для научной литературы

            // 2. Демонстрация ПЕРЕГРУЗКИ (Overloading)
            Console.WriteLine("\n=== 2. Перегрузка методов (Overloading) ===");
            novel.Read();          // Вызов Read() без параметров
            novel.Read(120);       // Вызов Read(int) с параметром кол-ва страниц

            // 3. Демонстрация работы дополнительных методов
            Console.WriteLine("\n=== 3. Дополнительные методы ===");
            mathTextbook.ApplyDiscount(15); // Применение скидки
            novel.ReturnBook();            // Возврат книги

            // 4. Демонстрация работы GENERIC-класса
            Console.WriteLine("\n=== 4. Работа Generic-класса (LibraryStorage<T>) ===");
            LibraryStorage<Book> myLibrary = new LibraryStorage<Book>();
            myLibrary.AddBook(mathTextbook);
            myLibrary.AddBook(novel);
            myLibrary.AddBook(physics);

            myLibrary.ShowAllBooks();
        }
    }
}
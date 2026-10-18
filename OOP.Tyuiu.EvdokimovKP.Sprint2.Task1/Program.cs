using System;
using System.Collections.Generic;

namespace AnimalOverloadingAndOverriding
{
    // Базовый класс для всех животных
    public class Animal
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Animal(string name, int age)
        {
            Name = name;
            Age = age;
        }

        // 1. ПЕРЕГРУЗКА МЕТОДОВ (Overloading) в одном классе
        // Метод MakeSound объявлен 3 раза с разными параметрами

        public virtual void MakeSound()
        {
            Console.WriteLine($"{Name} издает звук.");
        }

        public void MakeSound(int times)
        {
            Console.WriteLine($"{Name} издает звук {times} раза подряд.");
        }

        public void MakeSound(string mood)
        {
            Console.WriteLine($"{Name} издает звук, потому что оно {mood}.");
        }

        // Виртуальный метод для перекрытия в дочерних классах
        public virtual void Move(int distance)
        {
            Console.WriteLine($"{Name} преодолевает {distance} метров.");
        }
    }

    // Дочерний класс: Собака
    public class Dog : Animal
    {
        public Dog(string name, int age) : base(name, age) { }

        // 2. ПЕРЕКРЫТИЕ МЕТОДА (Overriding / Переопределение)
        public override void MakeSound()
        {
            Console.WriteLine($"Собака {Name} лает: Гав-гав!");
        }

        public override void Move(int distance)
        {
            Console.WriteLine($"Собака {Name} бежит {distance} метров.");
        }
    }

    // Дочерний класс: Кошка
    public class Cat : Animal
    {
        public Cat(string name, int age) : base(name, age) { }

        public override void MakeSound()
        {
            Console.WriteLine($"Кошка {Name} мяукает: Мяу-мяу!");
        }

        // Перекрытие с вызовом логики родительского класса через 'base'
        public override void Move(int distance)
        {
            base.Move(distance); // Вызов Animal.Move()
            Console.WriteLine($"  -> Кошка {Name} крадется незаметно.");
        }
    }

    // Дочерний класс: Птица
    public class Bird : Animal
    {
        public Bird(string name, int age) : base(name, age) { }

        public override void MakeSound()
        {
            Console.WriteLine($"Птица {Name} поет: Чирик-чирик!");
        }

        public override void Move(int distance)
        {
            Console.WriteLine($"Птица {Name} летит {distance} метров.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Dog dog = new Dog("Шарик", 3);
            Cat cat = new Cat("Мурка", 2);
            Bird bird = new Bird("Кеша", 1);

            Console.WriteLine("=== 1. Демонстрация ПЕРЕГРУЗКИ методов ===");
            // Одно имя метода (MakeSound), но разные аргументы
            dog.MakeSound();          // Переопределенный метод без параметров
            dog.MakeSound(3);         // Перегруженный метод (int)
            dog.MakeSound("веселое"); // Перегруженный метод (string)

            Console.WriteLine("\n=== 2. Демонстрация ПЕРЕКРЫТИЯ методов (Полиморфизм) ===");
            List<Animal> animals = new List<Animal> { dog, cat, bird };

            foreach (var animal in animals)
            {
                // Вызывается своя версия Move() для каждого животного
                animal.Move(50);
            }
        }
    }
}
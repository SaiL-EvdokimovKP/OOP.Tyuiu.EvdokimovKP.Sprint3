using System;
using System.Collections.Generic;

namespace AnimalPolymorphism
{
    // Базовый класс для всех питомцев
    public class Animal
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Animal(string name, int age)
        {
            Name = name;
            Age = age;
        }

        // Базовый метод движения (переопределим у наследников)
        public virtual void Move(int speed)
        {
            Console.WriteLine($"{Name} передвигается со скоростью {speed} км/ч.");
        }
    }

    public class Dog : Animal
    {
        public Dog(string name, int age) : base(name, age) { }

        public override void Move(int speed)
        {
            Console.WriteLine($"Собака {Name} бежит со скоростью {speed} км/ч.");
        }
    }

    public class Cat : Animal
    {
        public Cat(string name, int age) : base(name, age) { }

        public override void Move(int speed)
        {
            Console.WriteLine($"Кошка {Name} крадется со скоростью {speed} км/ч.");
        }
    }

    public class Bird : Animal
    {
        public Bird(string name, int age) : base(name, age) { }

        public override void Move(int speed)
        {
            Console.WriteLine($"Птица {Name} летит со скоростью {speed} км/ч.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Проверяем работу полиморфизма через общий список
            List<Animal> animals = new List<Animal>
            {
                new Dog("Шарик", 3),
                new Cat("Мурка", 2),
                new Bird("Кеша", 1),
                new Animal("Неизвестное животное", 5)
            };

            // Вызываем Move — у каждого класса сработает своя версия
            foreach (var animal in animals)
            {
                animal.Move(15);
            }
        }
    }
}
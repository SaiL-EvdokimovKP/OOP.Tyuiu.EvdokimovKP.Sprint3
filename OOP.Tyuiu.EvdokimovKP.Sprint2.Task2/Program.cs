using System;
using System.Collections.Generic;

namespace AnimalGenerics
{
    // Базовый класс Animal
    public class Animal
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Animal(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public virtual void MakeSound()
        {
            Console.WriteLine($"{Name} издает звук.");
        }
    }

    public class Dog : Animal
    {
        public Dog(string name, int age) : base(name, age) { }

        public override void MakeSound()
        {
            Console.WriteLine($"Собака {Name} лает: Гав!");
        }
    }

    public class Cat : Animal
    {
        public Cat(string name, int age) : base(name, age) { }

        public override void MakeSound()
        {
            Console.WriteLine($"Кошка {Name} мяукает: Мяу!");
        }
    }

    public class Bird : Animal
    {
        public Bird(string name, int age) : base(name, age) { }

        public override void MakeSound()
        {
            Console.WriteLine($"Птица {Name} поет: Чирик!");
        }
    }

    // Generic-класс (Обобщенная коллекция с ограничением по типу Animal)
    public class AnimalShelter<T> where T : Animal
    {
        private List<T> _animals = new List<T>();

        public void Add(T animal)
        {
            _animals.Add(animal);
            Console.WriteLine($"В приют добавлен(а): {animal.Name}");
        }

        public void Remove(T animal)
        {
            _animals.Remove(animal);
            Console.WriteLine($"Из приюта забрали: {animal.Name}");
        }

        // Вызов метода элементов через generic-тип
        public void MakeAllSounds()
        {
            Console.WriteLine("\n--- Животные подают голос ---");
            foreach (var animal in _animals)
            {
                animal.MakeSound();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 1. Приют только для кошек (Generic типизирован конкретным классом Cat)
            AnimalShelter<Cat> catShelter = new AnimalShelter<Cat>();
            catShelter.Add(new Cat("Мурка", 2));
            catShelter.Add(new Cat("Барсик", 4));
            catShelter.MakeAllSounds();

            Console.WriteLine();

            // 2. Общий приют для любых животных (Generic типизирован базовым классом Animal)
            AnimalShelter<Animal> generalShelter = new AnimalShelter<Animal>();
            generalShelter.Add(new Dog("Шарик", 3));
            generalShelter.Add(new Cat("Васька", 1));
            generalShelter.Add(new Bird("Кеша", 5));
            generalShelter.MakeAllSounds();
        }
    }
}
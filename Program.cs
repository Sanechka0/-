using System;

class Program
{
    static void Main()
    {
        var index = new SubjectIndex();
        
        while(true)
        {
            Console.WriteLine("\n1. Добавить запись");
            Console.WriteLine("2. Удалить запись");
            Console.WriteLine("3. Поиск страниц");
            Console.WriteLine("4. Сохранить в файл");
            Console.WriteLine("5. Загрузить из файла");
            Console.WriteLine("6. Показать весь указатель");
            Console.WriteLine("7. Выход");
            
            Console.Write("\nВыберите действие: ");
            var choice = Console.ReadLine();
            
            switch(choice)
            {
                case "1":
                    AddEntry(index);
                    break;
                case "2":
                    RemoveEntry(index);
                    break;
                case "3":
                    SearchPages(index);
                    break;
                case "4":
                    SaveToFile(index);
                    break;
                case "5":
                    index = LoadFromFile();
                    break;
                case "6":
                    index.Print();
                    break;
                case "7":
                    return;
            }
        }
    }

    static void AddEntry(SubjectIndex index)
    {
        Console.Write("Введите слово и страницы через пробел: ");
        var input = Console.ReadLine()?.Split(' ');
        
        if (input?.Length < 2)
        {
            Console.WriteLine("Некорректный ввод!");
            return;
        }
        
        var pages = input.Skip(1)
            .Select(p => int.TryParse(p, out int page) ? page : 0)
            .Where(p => p > 0);
        
        index.AddEntry(input[0], pages);
    }

    static void RemoveEntry(SubjectIndex index)
    {
        Console.Write("Введите слово для удаления: ");
        var word = Console.ReadLine();
        if(index.RemoveEntry(word))
            Console.WriteLine("Успешно удалено!");
        else
            Console.WriteLine("Слово не найдено!");
    }

    static void SearchPages(SubjectIndex index)
    {
        Console.Write("Введите слово для поиска: ");
        var word = Console.ReadLine();
        var pages = index.GetPages(word);
        
        if(pages.Count > 0)
            Console.WriteLine($"Страницы: {string.Join(", ", pages)}");
        else
            Console.WriteLine("Слово не найдено!");
    }

    static void SaveToFile(SubjectIndex index)
    {
        Console.Write("Введите имя файла: ");
        var fileName = Console.ReadLine();
        index.SaveToFile(fileName);
        Console.WriteLine("Файл сохранен!");
    }

    static SubjectIndex LoadFromFile()
    {
        Console.Write("Введите имя файла: ");
        var fileName = Console.ReadLine();
        return SubjectIndex.LoadFromFile(fileName);
    }
}

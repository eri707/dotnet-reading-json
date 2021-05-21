using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace dotnet_reading_json
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataName = args[0];
            //Error handling1
            if (!dataName.Contains("."))
            {
                Console.WriteLine("Please specify file extension.");
                return;
            }
            //Error handling2
            if (!File.Exists($"./{dataName}"))
            {
                Console.WriteLine("This file doesn't exist.");
                return;
            }
            using (StreamReader r = new StreamReader($"./{dataName}"))
            {
                // Read entire text file with ReadToEnd
                var jsonString = r.ReadToEnd();
                //Error handling3
                try
                {
                    //JsonSerializer is able to read and write JSON directly 
                    //Deserialize convert Json string format to the Fruit calss objects
                    //<List<Fruit>> is a model which I want to convert to
                    var fruits = JsonSerializer.Deserialize<List<Fruit>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    //var fruits = new List<Fruit>{}
                    fruits.Add(new Fruit { Id = 3, Name = "melon", Price = 0.7 });
                    var serialize = JsonSerializer.Serialize(fruits, new JsonSerializerOptions { WriteIndented = true });
                    Console.WriteLine(serialize);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"There is a problem with the data: {ex.Message}");
                    return;
                }
            }
        }
    }
}

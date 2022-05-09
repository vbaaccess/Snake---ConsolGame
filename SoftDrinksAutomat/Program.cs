using System;

namespace Main
{
    public class Program 
    {
        private static string file_input;
        private static string file_output;
        
        Program()
        {
          //Konstruktor
          
            Console.Clear();

            Console.WriteLine(new string('-', 35), "\n");
            
            Console.WriteLine("1. Herbata");
            Console.WriteLine("2. Kawa");
            Console.WriteLine("3. Batony");
            Console.WriteLine("4. Zimne napoje");
            Console.WriteLine("9. wyswietla menu");
            Console.WriteLine("0  KONIEC");
            Console.Write("\r\n   Wybierz opcje: ");
        }        
    }
}

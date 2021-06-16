using System;

namespace ConsoleSnake
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;          // wylaczenie migajacego kursora

            bool bTheEnd = false;
            double predkosc = 5.0;                  // liczba klatek na sekunde
            double przyspieszenie = 1.1;            // jak bardzo snak przyspiesza co znjedzenie
            double frameRate = 1000 / predkosc;     // czestotliwość ramki; czyli liczba milisekund co ile bedzie 'wyswietlana' klatka graficzna
                                                    // 1000 milisekund = 1 sek; po podzieleniu przez predkosc mamy ilosc klatek na sekunde

            DateTime lastDate = DateTime.Now;       // domyslnei zainicjowany czas to moment startu gry
            double frameMilliseconds;               // pomocniczy licznik czasu [ilosc milisekund]

            Meal meal = new Meal();                 // obiek jedzenia
            Snake snake = new Snake();              // obiek snake

            // game loop - glwona petla
            while (!bTheEnd)
            {

                // przechwycenie wciskanych klawiszy (STEROWANIE)
                if (Console.KeyAvailable)   // ustawienie dzieki czemu game loop nie oczekuje ale dziala dopiero jak cos wcisne
                {
                    
                    ConsoleKeyInfo input = Console.ReadKey();
                    switch (input.Key)
                    {
                        case ConsoleKey.Escape:
                            bTheEnd = true;
                            break;
                        case ConsoleKey.LeftArrow:
                            snake.Direction = Direction.Left;
                            break;
                        case ConsoleKey.RightArrow:
                            snake.Direction = Direction.Right;
                            break;
                        case ConsoleKey.UpArrow:
                            snake.Direction = Direction.Up;
                            break;
                        case ConsoleKey.DownArrow:
                            snake.Direction = Direction.Down;
                            break;
                    }
                }

                // Ramka Gry
                frameMilliseconds = (DateTime.Now - lastDate).TotalMilliseconds;
                if (frameMilliseconds >= frameRate) 
                {
                    //TO DO - utworzyc klase GAME ktora by laczyla Snake z Melem
                    //akcja (Ramka) gry

                    snake.Move();

                    if (   meal.CurrentTarget.x == snake.HeadPosition.x
                        && meal.CurrentTarget.y == snake.HeadPosition.y
                        )
                    {
                        snake.EatMeal();                // zjadamy
                        meal = new Meal();              // losujemy nowy posilek
                        frameRate /= przyspieszenie;    // zwiekszamy poziom predkosci
                    }                    

                    if (snake.GameOver)
                    {
                        Console.Clear();
                        Console.WriteLine($"GAME OVER\nYOUR SCORE: {0}",snake.Length);
                        bTheEnd = true;
                        Console.ReadLine();
                    }

                    lastDate = DateTime.Now;
                }

            }

            Console.WriteLine("Hello World!");
        }
    }
}

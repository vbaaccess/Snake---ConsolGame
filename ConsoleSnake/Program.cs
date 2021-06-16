using System;

namespace ConsoleSnake
{

    class Program
    {
        static void Main(string[] args)
        {
            bool bExitGame = false;                 // wyjscie z gry (zakonczenie dzialania aplikacji)
            bool bEndOfTheGame = false;             // zakonczenie rozgrywki
            int gameBoardSize = 50;                 // ustawienia rozmiaru obszaru gry
            double predkosc = 5.0;                  // liczba klatek na sekunde
            double przyspieszenie = 1.1;            // jak bardzo snak przyspiesza co znjedzenie
            double frameRate = 1000 / predkosc;     // czestotliwość ramki; czyli liczba milisekund co ile bedzie 'wyswietlana' klatka graficzna
                                                    // 1000 milisekund = 1 sek; po podzieleniu przez predkosc mamy ilosc klatek na sekunde

            DateTime lastDate = DateTime.Now;       // domyslnei zainicjowany czas to moment startu gry
            double frameMilliseconds;               // pomocniczy licznik czasu [ilosc milisekund]

            Meal meal;                              // obiek jedzenia
            Snake snake;                            // obiek snake

            while (!bExitGame)
            {
                
                

                //ustawienia okna konsoli:
                GameConsolWindow.Size(gameBoardSize     // - ustawienia rozmiaru
                                , gameBoardSize);
                GameConsolWindow.Center();              // - wysrodkowanie okna
                
                Console.Clear();
                Console.Title = "SNAKE";
                Console.CursorVisible = false;          // wylaczenie migajacego kursora

                bExitGame = true;                       // TO DO - docelow wyjscie z petli gry i zamkniecie konsoli

                meal = new Meal();                      // obiek jedzenia
                snake = new Snake();                    // obiek snake

                // game loop - glwona petla rozgrywki
                while (!bEndOfTheGame)
                {

                    // przechwycenie wciskanych klawiszy (STEROWANIE)
                    if (Console.KeyAvailable)   // ustawienie dzieki czemu game loop nie oczekuje ale dziala dopiero jak cos wcisne
                    {

                        ConsoleKeyInfo input = Console.ReadKey();
                        switch (input.Key)
                        {
                            case ConsoleKey.Escape:
                                bEndOfTheGame = true;
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

                        if (meal.CurrentTarget.x == snake.HeadPosition.x
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
                            Console.ResetColor();
                            Console.SetCursorPosition(1, 1);
                            Console.WriteLine($"GAME OVER");
                            Console.SetCursorPosition(1, 2);
                            Console.WriteLine($"YOUR SCORE: " + snake.Length);
                            bEndOfTheGame = true;
                            Console.ReadLine();
                        }

                        lastDate = DateTime.Now;
                    }

                }
            }
        }

    }


}

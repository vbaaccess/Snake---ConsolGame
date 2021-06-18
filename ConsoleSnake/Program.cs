using System;

namespace ConsoleSnake
{

    class Program
    {
        static void Main(string[] args)
        {
            bool bExitGame = false;                 // wyjscie z gry (zakonczenie dzialania aplikacji)
            bool bEndOfTheGame = false;             // zakonczenie rozgrywki
            int gameBoardSize = 54;                 // ustawienia rozmiaru obszaru gry
            double predkosc = 5.0;                  // liczba klatek na sekunde
            double przyspieszenie = 1.1;            // jak bardzo snak przyspiesza co znjedzenie
            double frameRate = 1000 / predkosc;     // czestotliwość ramki; czyli liczba milisekund co ile bedzie 'wyswietlana' klatka graficzna
                                                    // 1000 milisekund = 1 sek; po podzieleniu przez predkosc mamy ilosc klatek na sekunde

            DateTime lastDate = DateTime.Now;       // domyslnei zainicjowany czas to moment startu gry
            double frameMilliseconds;               // pomocniczy licznik czasu [ilosc milisekund]

            Board board;                            // obiekt planszy
            Snake snake;                            // obiek snake
            Meal meal;                              // obiek jedzenia
            
            while (!bExitGame)
            {



                //ustawienia okna konsoli:
                Console.CursorVisible = false;          // wylaczenie migajacego kursora
                GameConsolWindow.Size(gameBoardSize     // - ustawienia rozmiaru
                                , gameBoardSize);
                GameConsolWindow.Center();              // - wysrodkowanie okna

                // wstepne menu gry (nazie bez opcji)
                string s;
                Console.Clear();                
                Console.Title = "CONSOL GAME - SNAKE 2021 (c)";
                s = "CONSOL GAME SNAKE";
                Console.SetCursorPosition((int)gameBoardSize / 2 - ((int)(s.Length) / 2) , 1);
                Console.Write(s);

                s = "PRES KEY TO START GAME!";
                Console.SetCursorPosition((int)gameBoardSize/2 - ((int)(s.Length) / 2), gameBoardSize / 2);
                Console.Write(s);
                Console.ReadLine();                
                Console.Clear();

                bExitGame = true;                       // TO DO - docelow wyjscie z petli gry i zamkniecie konsoli

                board = new Board(gameBoardSize     
                                , gameBoardSize);       // obiek planszy
                snake = new Snake();                    // obiek snake
                meal = new Meal();                      // obiek jedzenia

                //--------------------------------------------------------------------------
                //po wykonanu ruchu (wykonaniu metody move obiektu klasy Snake) 
                //klas bedzie wywolywala metode klasy Board (oddelegowana)
                //przekazana metoda ma za zadanie:
                // - zapamietac koordynaty (np. na potrzey wyswietlenia ich na planszy)
                // - sprawdzic czy nie nastepuje kolizja ze sciana planzsy
                snake.RegisterWithEventAfterNewCoordinate(board.CoordinateToCheck);
                snake.RegisterWithEventAfterNewLength(board.LenghtToCheck);

                snake.RegisterWithEventAfterMove(board.PrintBootomInformation);
                snake.RegisterWithEventAfterEatMeal(board.PrintTopInformation);

                board.RegisterWithEventAfterCollision(snake.HeadColision);
                //--------------------------------------------------------------------------

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
                                if (snake.Direction != Direction.Right)
                                    snake.Direction = Direction.Left;
                                break;
                            case ConsoleKey.RightArrow:
                                if (snake.Direction != Direction.Left)
                                    snake.Direction = Direction.Right;
                                break;
                            case ConsoleKey.UpArrow:
                                if (snake.Direction != Direction.Down)
                                    snake.Direction = Direction.Up;
                                break;
                            case ConsoleKey.DownArrow:
                                if (snake.Direction != Direction.Up)
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
                            board.PrintGameOverDetail(snake.Length);
                            bEndOfTheGame = true;
                        } else
                        {
                            //board.PrintBootomInformation();
                            //board.PrintTopInformation(snake.Length);
                        }



                        lastDate = DateTime.Now;
                    }

                }

                Console.ReadLine();
            }
        }

    }


}

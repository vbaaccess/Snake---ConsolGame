using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleSnake
{
    // biezacy kierunek w ktorym porusza sie snake
    public enum Direction   
    {   Left
      , Right
      , Up
      , Down
    }

    public class Snake : ISnake
    {
        private readonly string SnakeHead = "@";                            //symbol glowy sneka
        private readonly string SnakeBody = "#";                            //symbol ciala sneka
        private readonly ConsoleColor SnakeColor = ConsoleColor.Green;      //kolor  sneka

        public int Length { get; set; } = 1;                                // poczatkowa dlugosc
        public Direction Direction { get; set; } = Direction.Right;         // poczatkowy kierunek glowy
        public Coordinate HeadPosition { get; set; } = new Coordinate();    // poczatkowy punkt startowy glowy
        List<Coordinate> Tail { get; set; } = new List<Coordinate>();

        private bool outOfRange = false;        

        public bool GameOver
        {
            // czy w naszym ogonie ktoras z czesci ogona nie pokrywa sie z koordynatami glowy
            // czli czy waz nie wpadl na swoj ogon

            get
            {
                return
                   (Tail                                    // sprawdzamy Liste zawierajacych koordynaty ( =>c)
                        .Where(                             // sprawdzamy wykorzystujac System.Linq.Where
                                c =>                        // gdzie kordynaty (c =>)
                                  c.x == HeadPosition.x     // pokrywaj sie w pozyji X z glowa
                               && c.y == HeadPosition.y     // oraz pokrywaj sie w pozyji Y z glowa
                              ).ToList()                    // sprawdzamy dla wszystkich elementow Listy koordynatow ogona
                                        .Count > 1)         // sprawdzamy czy kolizja nastapila w na conajmniej jednym odcinku ogona
                              ||                            // lub
                              outOfRange                    // gdy waz wyjedzie poza ekran
                              ;
            } 

            // TO DO - do zrobienie przypadke gdy glowa jest poza zakresem planszy
        }

        public void EatMeal() { Length++; }

        public void Move()
        {
            switch (Direction)
            {
                case Direction.Left:
                    HeadPosition.x--;
                    break;
                case Direction.Right:
                    HeadPosition.x++;
                    break;
                case Direction.Up:
                    HeadPosition.y--;
                    break;
                case Direction.Down:
                    HeadPosition.y++;
                    break;
            }

            Draw(); //wyswietlenie
        }

        //Rysowanie calego snake 
        void Draw()
        {
            //TO DO - zamiast try/catch zrobic sprawdzenie bufora ekranu: czye head position nie wykracza poza ekran
            try
            {
                //rysowanie nowej pozycji glowy
                Console.SetCursorPosition(HeadPosition.x, HeadPosition.y);
                Console.ForegroundColor = SnakeColor;
                Console.Write(SnakeBody);

                //zbieramy koordynaty po ktorych przeslismy
                Tail.Add(new Coordinate(HeadPosition.x, HeadPosition.y));

                //czyszczenie ogona
                if (Tail.Count > this.Length)
                {
                    var endTail = Tail.First();                         //wybieram pierwszy element listy => koniec ogona
                    Console.SetCursorPosition(endTail.x, endTail.y);    //1 - czyscimy na ekranie
                    Console.Write(" ");
                    Tail.Remove(endTail);                               //2 - czyscimy informacje (z listy)
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                outOfRange = true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleSnake
{
    public class Meal
    {
        private readonly string MealChar        = "$";                      // symbol nagrody
        private readonly ConsoleColor MealColor = ConsoleColor.Yellow;      // kolor  nagrody
        private readonly Coordinate MealBoard = new Coordinate(20, 20);     // obszar na ktorym pojawia sie posilek 

        public Meal()
        {
            // gdy utworzymy obiekt klasy to ma sie losowo wygenerowac koordynata
            Random rand = new Random();
            var randomX = rand.Next(1, MealBoard.x);
            var randomY = rand.Next(1, MealBoard.y);

            CurrentTarget = new Coordinate(randomX, randomY);

            Draw();
        }

        //udostepnienie informacji o miejscu pojawienia sie posilku
        public Coordinate CurrentTarget { get; set; }
        
        //Wyswietlenie posilku 
        void Draw()
        {
            Console.SetCursorPosition(CurrentTarget.x, CurrentTarget.y);
            Console.ForegroundColor = MealColor;
            Console.Write(MealChar);
        }
    }
}

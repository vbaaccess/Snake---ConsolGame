using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleSnake
{
    public class Meal
    {
        private readonly string MealChar        = "$";                      // symbol nagrody
        private readonly ConsoleColor MealColor = ConsoleColor.Yellow;      // kolor  nagrody
        private Coordinate MealBoard;                                       // obszar na ktorym pojawia sie posilek 

        //udostepnienie informacji o miejscu pojawienia sie posilku
        public Coordinate CurrentTarget { get; private set; }

        public Meal()
        {
            // okreslenie obszaru pojawiania sie posilku
            MealBoard = new Coordinate(51, 51);

            // gdy utworzymy obiekt klasy to ma sie losowo wygenerowac koordynata
            Random rand = new Random();
            var randomX = rand.Next(3, MealBoard.x);
            var randomY = rand.Next(3, MealBoard.y);

            CurrentTarget = new Coordinate(randomX, randomY);

            Draw();
        }
        
        //Wyswietlenie posilku 
        void Draw()
        {
            Console.SetCursorPosition(CurrentTarget.x, CurrentTarget.y);
            Console.ForegroundColor = MealColor;
            Console.Write(MealChar);
        }

        // --------------------------------------------------------- --- DELEGATY --- --------------------------------------------------------- ---
        /* --- 1 --- */
        public delegate void EventBeforCreateMeal(Coordinate NewMealCoordinate);
        private EventBeforCreateMeal listOfHandlersBeforCreateMeal;

        public void RegisterWithEventBeforCreateMeal(EventBeforCreateMeal methodToCall) => listOfHandlersBeforCreateMeal += methodToCall;
        private void RiseEventBeforCreateMeal() => listOfHandlersBeforCreateMeal?.Invoke(this.CurrentTarget);

        // --------------------------------------------------------- ----------------- --- ---------------------------------------------------------

    }
}

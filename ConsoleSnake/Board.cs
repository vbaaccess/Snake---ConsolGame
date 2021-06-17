using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleSnake
{
    public class Board
    {
        private readonly string WallChar = "*";                               // symbol sciany
        private readonly ConsoleColor WallColor = ConsoleColor.DarkCyan;      // kolor  scian

        public int _boardSize_X { get; private set; }
        public int _boardSize_y { get; private set; }

        List<Coordinate> BoardWall { get; set; } = new List<Coordinate>();     // lista wspolzednych scian

        public Board(int boardSize_XY)
        {
            _boardSize_X = boardSize_XY;
            _boardSize_y = boardSize_XY;

            Draw();
        }

        public Board(int boardSize_X, int boardSize_y)
        {            
            _boardSize_X = boardSize_X;
            _boardSize_y = boardSize_y;

            Draw();
        }

        private void Draw()
        {
            // gdy utworzymy obiekt klasy to ma sie wygenerowac obrys planszy
            
            Console.ForegroundColor = WallColor;

            for (int x=1;x<_boardSize_X-1; x++)
            {
                // gorna sciana
                Console.SetCursorPosition(x, 2);
                Console.Write(WallChar);
                
                // dolna sciana
                Console.SetCursorPosition(x, _boardSize_y-2);
                Console.Write(WallChar);
            }

            for (int y = 3; y < _boardSize_y-2; y++)
            {
                // lewa sciana
                Console.SetCursorPosition(1, y);
                Console.Write(WallChar);

                // prawa sciana
                Console.SetCursorPosition(_boardSize_X-2, y);
                Console.Write(WallChar);
            }

        }
    }
}

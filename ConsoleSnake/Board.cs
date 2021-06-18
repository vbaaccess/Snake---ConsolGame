using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleSnake
{
    public class Board
    {
        private readonly string WallChar = "*";                               // symbol sciany
        private readonly ConsoleColor WallColor = ConsoleColor.DarkCyan;      // kolor  scian

        private Coordinate _CheckedCoordinates;

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
            int x;
            int y;

            Console.ForegroundColor = WallColor;

            for (x=1;x<_boardSize_X-1; x++)
            {
                // gorna sciana
                y = 2;
                SaveWallCoordinate(x, y);
                Console.SetCursorPosition(x, y);
                Console.Write(WallChar);                

                // dolna sciana
                y = _boardSize_y - 2;
                SaveWallCoordinate(x, y);
                Console.SetCursorPosition(x, _boardSize_y-2);
                Console.Write(WallChar);                
            }

            for (y = 3; y < _boardSize_y-2; y++)
            {
                // lewa sciana
                x = 1;
                SaveWallCoordinate(x, y);
                Console.SetCursorPosition(x, y);
                Console.Write(WallChar);
                

                // prawa sciana
                x = _boardSize_X - 2;
                SaveWallCoordinate(x, y);
                Console.SetCursorPosition(x, y);
                Console.Write(WallChar);                
            }


            // Testy - przeszkody na planszy

            //Console.ForegroundColor = ConsoleColor.White;
            //x = 2; y = 8;
            //SaveWallCoordinate(x, y);
            //Console.SetCursorPosition(x, y);
            //Console.Write(WallChar);

        }

        private void SaveWallCoordinate(int x,int y)
        {
            //zbieramy koordynaty sciany
            BoardWall.Add(new Coordinate(x, y));
        }

        //public void PrintBootomInformation(Coordinate CheckedCoordinates, bool bColision = false)
        public void CoordinateToCheck(Coordinate CheckedCoordinates)
        {
            //zapisz koordynaty do sprawdzenia
            if (_CheckedCoordinates == null)
                _CheckedCoordinates = new Coordinate();

            _CheckedCoordinates.x = CheckedCoordinates.x;
            _CheckedCoordinates.y = CheckedCoordinates.y;

            CheckColisionWithWall(CheckedCoordinates);
            //if (CheckColisionWithWall(CheckedCoordinates))
            //    bColision = true;   
        }
        
        private bool CheckColisionWithWall(Coordinate CheckedCoordinates)
        {
            bool Collision = false;

            if (BoardWall
                        .Where(
                            b =>
                                    b.x == CheckedCoordinates.x
                                && b.y == CheckedCoordinates.y
                                                                ).ToList()
                                                                            .Count > 0
                )
                Collision = true;

            if (Collision)
                RiseEventAfterCollision();

            return Collision;
        }

        public void PrintBootomInformation()
        {
            int BootomTextIndex = _boardSize_y - 1;

            ClearLine(BootomTextIndex);
            string s = $"Head Coordinates => x: {_CheckedCoordinates.x} , y: {_CheckedCoordinates.y}";

            Console.SetCursorPosition(4, BootomTextIndex);
            Console.Write($"x: " + _CheckedCoordinates.x);

            Console.SetCursorPosition(10, BootomTextIndex);
            Console.Write($"y: " + _CheckedCoordinates.y);

            //Console.SetCursorPosition(20, _boardSize_y - 1);
            //Console.Write($"y: " + CheckedCoordinates.y);

            //return CoordinatesCompatible;
        }

        //CheckCoordinateCompatibility

        //Wyczyszczenie linijki na konsoli
        private void ClearLine(int LineTextIndex)
        {
            int LineLenght = _boardSize_X;

            for (int i=0;i< LineLenght-1;i++)
            {
                Console.SetCursorPosition(i, LineTextIndex);
                Console.Write($" ");
            }
        }


        // --------------------------------------------------------- --- DELEGATY --- --------------------------------------------------------- ---
        /* --- 1 --- */
        public delegate void EventAfterCollision();
        private EventAfterCollision listOfHandlersAfterCollision;

        public void RegisterWithEventAfterCollision(EventAfterCollision methodToCall) => listOfHandlersAfterCollision += methodToCall;
        private void RiseEventAfterCollision() => listOfHandlersAfterCollision?.Invoke();

        // --------------------------------------------------------- ----------------- --- ---------------------------------------------------------
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleSnake
{
    // okreslamy jak wyglada typowy waz
    public interface ISnake
    {
        void Move();    // umiejetnosc poruszania sie
        void EatMeal(); // umiejetnosc jedzenia
                        // + efekt => rosniecie
    }
}

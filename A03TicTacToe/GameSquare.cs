using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A03TicTacToe
{
    class GameSquare
    {
        public bool IsFilled { get; set; }
        public char Token { get; set; }  //needed for win condition
        public int Age { get; set; }


        public void clearSquare()
        {
            IsFilled = false;
            Token = ' ';
        }

    }
}
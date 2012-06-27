using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EarthCoreEngine.Input
{
    public class KeyUtil
    {

        public static Keys GetKeyFromString(string value)
        {
            switch (value.ToUpper())
            {
                case "A":
                    return Keys.A;
                case "B":
                    return Keys.B;
                case "C":
                    return Keys.C;
                case "D":
                    return Keys.D;
                case "E":
                    return Keys.E;
                case "F":
                    return Keys.F;
                case "G":
                    return Keys.G;
                case "H":
                    return Keys.H;
                case "I":
                    return Keys.I;
                case "J":
                    return Keys.J;
                case "K":
                    return Keys.K;
                case "L":
                    return Keys.L;
                case "M":
                    return Keys.M;
                case "N":
                    return Keys.N;
                case "O":
                    return Keys.O;
                case "P":
                    return Keys.P;
                case "Q":
                    return Keys.Q;
                case "R":
                    return Keys.R;
                case "S":
                    return Keys.S;
                case "T":
                    return Keys.T;
                case "U":
                    return Keys.U;
                case "V":
                    return Keys.V;
                case "W":
                    return Keys.W;
                case "X":
                    return Keys.X;
                case "Y":
                    return Keys.Y;
                case "Z":
                    return Keys.Z;
                case "0":
                    return Keys.D0;
                case "1":
                    return Keys.D1;
                case "2":
                    return Keys.D2;
                case "3":
                    return Keys.D3;
                case "4":
                    return Keys.D4;
                case "5":
                    return Keys.D5;
                case "6":
                    return Keys.D6;
                case "7":
                    return Keys.D7;
                case "8":
                    return Keys.D8;
                case "9":
                    return Keys.D9;
                case "ESC":
                    return Keys.Escape;
                case "ESCAPE":
                    return Keys.Escape;
                case "ENTER":
                    return Keys.Enter;
                case "RETURN":
                    return Keys.Enter;
                case "UP":
                    return Keys.Up;
                case "DOWN":
                    return Keys.Down;
                case "LEFT":
                    return Keys.Left;
                case "RIGHT":
                    return Keys.Right;
                default:
                    throw new Exception("Value not recognized by KeyUtil: " + value);
            }
        }

    }
}

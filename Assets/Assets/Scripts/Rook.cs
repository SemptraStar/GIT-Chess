using UnityEngine;
using System.Collections;

public class Rook : Chessman {
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        var board = BoardManager.Instance;
        Chessman c;
        int i;

        //Right
        i = CurrentX;

        while (true)
        {
            i++;
            if (i >= 8)
                break;

            c = board.Chessmans[i, CurrentY];
            if (c == null)
                r[i, CurrentY] = true;
            else
            {
                if (c.isWhite != isWhite)               
                    r[i, CurrentY] = true;

                    break;             
            }
        }

        //Left
        i = CurrentX;

        while (true)
        {
            i--;
            if (i < 0)
                break;

            c = board.Chessmans[i, CurrentY];
            if (c == null)
                r[i, CurrentY] = true;
            else
            {
                if (c.isWhite != isWhite)
                    r[i, CurrentY] = true;

                break;
            }
        }

        //Top
        i = CurrentY;

        while (true)
        {
            i++;
            if (i >= 8)
                break;

            c = board.Chessmans[CurrentX, i];
            if (c == null)
                r[CurrentX, i] = true;
            else
            {
                if (c.isWhite != isWhite)
                    r[CurrentX, i] = true;

                break;
            }
        }

        //Bot
        i = CurrentY;

        while (true)
        {
            i--;
            if (i < 0)
                break;

            c = board.Chessmans[CurrentX, i];
            if (c == null)
                r[CurrentX, i] = true;
            else
            {
                if (c.isWhite != isWhite)
                    r[CurrentX, i] = true;

                break;
            }
        }

        return r;
    }
}

using UnityEngine;
using System.Collections;

public class Bishop : Chessman {
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        var board = BoardManager.Instance;
        Chessman c;
        int i, j;

        //Top - left
        i = CurrentX;
        j = CurrentY;

        while(true)
        {
            i--;
            j++;

            if (i < 0 || j >= 8)
                break;

            c = board.Chessmans[i, j];
            if (c == null)
                r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                   r[i, j] = true;

                break;
            }
        }

        //Top - right
        i = CurrentX;
        j = CurrentY;

        while (true)
        {
            i++;
            j++;

            if (i >= 8 || j >= 8)
                break;

            c = board.Chessmans[i, j];
            if (c == null)
                r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[i, j] = true;

                break;
            }
        }

        //Bot - left
        i = CurrentX;
        j = CurrentY;

        while (true)
        {
            i--;
            j--;

            if (i < 0 || j < 0)
                break;

            c = board.Chessmans[i, j];
            if (c == null)
                r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[i, j] = true;

                break;
            }
        }

        //Bot - right
        i = CurrentX;
        j = CurrentY;

        while (true)
        {
            i++;
            j--;

            if (i >= 8 || j < 0)
                break;

            c = board.Chessmans[i, j];
            if (c == null)
                r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[i, j] = true;

                break;
            }
        }

        return r;
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class King : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        var board = BoardManager.Instance;
        Chessman c;
        int i, j;

        //TOp
        i = CurrentX - 1;
        j = CurrentY + 1;

        if (CurrentY != 7)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 || i < 8)
                {
                    c = board.Chessmans[i, j];
                    if (c == null)
                        r[i, j] = true;
                    else if (isWhite != c.isWhite)
                        r[i, j] = true;
                }

                i++;
            }
        }

        //Bot
        i = CurrentX - 1;
        j = CurrentY - 1;

        if (CurrentY != 0)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 || i < 8)
                {
                    c = board.Chessmans[i, j];
                    if (c == null)
                        r[i, j] = true;
                    else if (isWhite != c.isWhite)
                            r[i, j] = true;
                }

                i++;
            }
        }

        //Left
        if (CurrentX != 0)
        {
            c = board.Chessmans[CurrentX - 1, CurrentY];

            if (c == null)
                r[CurrentX - 1, CurrentY] = true;
            else if (isWhite != c.isWhite)
                r[CurrentX - 1, CurrentY] = true;
        }

        //Right
        if (CurrentX != 7)
        {
            c = board.Chessmans[CurrentX + 1, CurrentY];

            if (c == null)
                r[CurrentX + 1, CurrentY] = true;
            else if (isWhite != c.isWhite)
                r[CurrentX + 1, CurrentY] = true;
        }


        return r;
    }
}
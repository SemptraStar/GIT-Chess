using UnityEngine;
using System.Collections;

public class Knight : Chessman
{
    BoardManager board = BoardManager.Instance;

    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        //Up - left
        KnightMove(CurrentX - 1, CurrentY + 2, ref r);

        //Up - right
        KnightMove(CurrentX + 1, CurrentY + 2, ref r);

        //Bot - left
        KnightMove(CurrentX - 1, CurrentY - 2, ref r);

        //Bot - right
        KnightMove(CurrentX + 1, CurrentY - 2, ref r);

        //Left - up
        KnightMove(CurrentX - 2, CurrentY + 1, ref r);

        //Right - up
        KnightMove(CurrentX + 2, CurrentY + 1, ref r);

        //Left - bot
        KnightMove(CurrentX - 2, CurrentY - 1, ref r);

        //Right - bot
        KnightMove(CurrentX + 2, CurrentY - 1, ref r);


        return r;
    }

    public void KnightMove(int x, int y, ref bool[,] r)
    {
        Chessman c;
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            c = board.Chessmans[x, y];
            if (c == null)
                r[x, y] = true;
            else if (isWhite != c.isWhite)
                r[x, y] = true;

        }
    }
}

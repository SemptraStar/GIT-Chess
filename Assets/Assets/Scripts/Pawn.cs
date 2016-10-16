using UnityEngine;
using System.Collections;

public class Pawn : Chessman {

    public override bool[,] PossibleMove()
    {
        BoardManager board = BoardManager.Instance;

        bool[,] r = new bool[8, 8];

        Chessman C1, C2;

        //White turn.
        if (isWhite)
        {
            //Diagonal left.
            if (CurrentX != 0 && CurrentY != 7)
            {
                C1 = board.Chessmans[CurrentX - 1, CurrentY + 1];
                if (C1 != null && !C1.isWhite)
                    r[CurrentX - 1, CurrentY + 1] = true;
            }

            //Diagonal right.
            if (CurrentX != 7 && CurrentY != 7)
            {
                C1 = board.Chessmans[CurrentX + 1, CurrentY + 1];
                if (C1 != null && !C1.isWhite)
                    r[CurrentX + 1, CurrentY + 1] = true;
            }

            //Middle.
            if (CurrentY != 7)
            {
                C1 = board.Chessmans[CurrentX, CurrentY + 1];
                if (C1 == null)
                    r[CurrentX, CurrentY + 1] = true;
            }
            //Middle turn 1.
            if (CurrentY == 1)
            {
                C1 = board.Chessmans[CurrentX, CurrentY + 1];
                C2 = board.Chessmans[CurrentX, CurrentY + 2];

                if (C1 == null && C2 == null)
                    r[CurrentX, CurrentY + 2] = true;
            }
        }
        else
        {
            //Diagonal left.
            if (CurrentX != 0 && CurrentY != 0)
            {
                C1 = board.Chessmans[CurrentX - 1, CurrentY - 1];
                if (C1 != null && C1.isWhite)
                    r[CurrentX - 1, CurrentY - 1] = true;
            }

            //Diagonal right.
            if (CurrentX != 7 && CurrentY != 0)
            {
                C1 = board.Chessmans[CurrentX + 1, CurrentY - 1];
                if (C1 != null && C1.isWhite)
                    r[CurrentX + 1, CurrentY - 1] = true;
            }

            //Middle.
            if (CurrentY != 0)
            {
                C1 = board.Chessmans[CurrentX, CurrentY - 1];
                if (C1 == null)
                    r[CurrentX, CurrentY - 1] = true;
            }
            //Middle turn 1.
            if (CurrentY == 6)
            {
                C1 = board.Chessmans[CurrentX, CurrentY - 1];
                C2 = board.Chessmans[CurrentX, CurrentY - 2];

                if (C1 == null && C2 == null)
                    r[CurrentX, CurrentY - 2] = true;
            }
        }

        return r;
    }
}

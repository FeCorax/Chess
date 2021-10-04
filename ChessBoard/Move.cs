using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChessBoard
{
   class Move
   {
      public static char IntToChar(int i)
      {
         string cols = "ABCDEFGH";
         return cols[i];
      }
      public static int CharToInt(char a)
      {
         return a - 'A';
      }
      public Move(bool isSpecialMove, string moveCode)
      {
         this.startRow = -1;
         this.startCol = -1;
         this.endRow = -1;
         this.endCol = -1;
         this.isSpecialMove = isSpecialMove;
         this.moveCode = moveCode;
      }

      

      public Move(int startRow, int startCol, int endRow, int endCol, bool isSpecialMove, Board.piece movingPiece, Board.piece takenPiece)
      {
         this.startRow = startRow;
         this.startCol = startCol;
         this.endRow = endRow;
         this.endCol = endCol;
         this.isSpecialMove = isSpecialMove;
         this.movingPiece = movingPiece;
         this.takenPiece = takenPiece;
         moveCode = string.Format("{0}{1}->{2}{3}", IntToChar(startCol), startRow + 1, IntToChar(endCol), endRow + 1);
      }

      public Move(int startRow, int startCol, int endRow, int endCol, Board.piece movingPiece, Board.piece takenPiece, string moveCode, bool isSpecialMove=true)
      {
         this.startRow = startRow;
         this.startCol = startCol;
         this.endRow = endRow;
         this.endCol = endCol;
         this.isSpecialMove = isSpecialMove;
         this.moveCode = moveCode;
         this.movingPiece = movingPiece;
         this.takenPiece = takenPiece;
      }

      public int startRow { get; }
      public int startCol { get; }
      public int endRow { get; }
      public int endCol { get; }
      public bool isSpecialMove { get; }
      public string moveCode { get; }
      public Board.piece movingPiece { get; }
      public Board.piece takenPiece { get; }
      
      public static string MakeMoveCode(int startRow, int startCol, int endRow, int endCol)
      {
         return string.Format("{0}{1}->{2}{3}", IntToChar(startCol), startRow + 1, IntToChar(endCol), endRow + 1);
      }
   }

}

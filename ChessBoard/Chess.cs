using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoard
{
   public struct Move
   {
      
      public Move(bool isSpecialMove, string moveCode)
      {
         this.startRow = -1;
         this.startCol = -1;
         this.endRow = -1;
         this.endCol = -1;
         this.isSpecialMove = isSpecialMove;
         this.moveCode = moveCode;
      }

      public Move(int startRow, int startCol, int endRow, int endCol, bool isSpecialMove, string moveCode)
      {
         this.startRow = startRow;
         this.startCol = startCol;
         this.endRow = endRow;
         this.endCol = endCol;
         this.isSpecialMove = isSpecialMove;
         this.moveCode = moveCode;
      }

      public int startRow { get; }
      public int startCol { get; }
      public int endRow { get; }
      public int endCol { get; }
      public bool isSpecialMove { get; }
      public string moveCode { get; }

   }
   class Board
   {
      enum piece {empty, pawn_w, pawn_b, knight_w, knight_b, bishop_w, bishop_b, rook_w, rook_b, queen_w, queen_b, king_w, king_b };
      private piece[,] squares;
      //Inits the squares object, and places starting peieces.
      public void Init()
      {
         squares = new piece[8,8];
         for (int i = 0; i < 8; i++)
         {
            for(int j = 0; j < 8; j++)
            {
               squares[i, j] = piece.empty;
               //Console.WriteLine(i.ToString() + j.ToString());
            }
         }
         for (int i = 0; i < 8; i++)
         {
            squares[1, i] = piece.pawn_w;
            squares[6, i] = piece.pawn_b;
         }
         squares[0, 0] = piece.rook_w;
         squares[0, 1] = piece.knight_w;
         squares[0, 2] = piece.bishop_w;
         squares[0, 3] = piece.queen_w;
         squares[0, 4] = piece.king_w;
         squares[0, 5] = piece.bishop_w;
         squares[0, 6] = piece.knight_w;
         squares[0, 7] = piece.rook_w;
         squares[7, 0] = piece.rook_b;
         squares[7, 1] = piece.knight_b;
         squares[7, 2] = piece.bishop_b;
         squares[7, 3] = piece.queen_b;
         squares[7, 4] = piece.king_b;
         squares[7, 5] = piece.bishop_b;
         squares[7, 6] = piece.knight_b;
         squares[7, 7] = piece.rook_b;

      }
      private static char IntToChar(int i)
      {
         string cols = "ABCDEFGH";
         return cols[i];
      }
      public static int CharToInt(char a)
      {
         return a - 'A';
      }
      private char Piece_convert(piece input)
      {
         switch (input)
         {
            case piece.empty:
               return '-';
            case piece.pawn_w:
               return 'P';
            case piece.pawn_b:
               return 'p';
            case piece.knight_w:
               return 'N';
            case piece.knight_b:
               return 'n';
            case piece.bishop_w:
               return 'B';
            case piece.bishop_b:
               return 'b';
            case piece.rook_w:
               return 'R';
            case piece.rook_b:
               return 'r';
            case piece.queen_w:
               return 'Q';
            case piece.queen_b:
               return 'q';
            case piece.king_w:
               return 'K';
            case piece.king_b:
               return 'k';

            default:
               break;
         }
         return ' ';
      }
      public void PrintBoard()
      {
         for (int i = 7; i >= 0; i--)
         {
            Console.Write((i + 1).ToString() + "  |");
            for(int j = 0; j < 8; j++)
            {
               Console.Write(Piece_convert(squares[i, j]) + " ");
            }
            Console.Write('\n');
         }
         Console.WriteLine("-------------------");
         Console.WriteLine("*  |A B C D E F G H\n");
      }
      public void MakeMove(Move move)
      {
         if (!move.isSpecialMove)
         {
            squares[move.endRow, move.endCol] = squares[move.startRow, move.startCol];
            squares[move.startRow, move.startCol] = piece.empty;
         }
         else
         {
            Console.WriteLine("special moves not ready yet");
            //note to self, impliment special moves
         }
      }
      private bool WhiteCanTake(piece dest)
      {
         if(dest == piece.empty || dest == piece.pawn_b || dest == piece.knight_b || dest == piece.bishop_b || dest == piece.rook_b || dest == piece.queen_b || dest == piece.king_b)
         {
            return true;
         }
         else
         {
            return false;
         }
      }
      private bool BlackCanTake(piece dest)
      {
         if (dest == piece.empty || dest == piece.pawn_w || dest == piece.knight_w || dest == piece.bishop_w || dest == piece.rook_w || dest == piece.queen_w || dest == piece.king_w)
         {
            return true;
         }
         else
         {
            return false;
         }
      }
      public List<Move> KnighMoves(int startRow, int startCol)
      {
         bool knightIsWhite = false;
         if(squares[startRow, startCol] == piece.knight_w)
         {
            knightIsWhite = true;
         }
         else if(squares[startRow, startCol] != piece.knight_b)
         {
            return null;
         }
         int[,] directions = new int[,] { { 1, 2 }, { 2, 1 }, { 2, -1 }, { 1, -2 }, { -2, -1 }, { -1, -2 }, { 1, -2 }, { 2, -1 } };
         List<Move> ret = new List<Move>();
         int endRow;
         int endCol;
         for (int i = 0; i < 8; i++)
         {
            endRow = startRow + directions[i, 0];
            endCol = startCol + directions[i, 1];
            if(endRow >= 0 && endRow < 9)
            {
               if(endCol >= 0 && endRow < 9)
               {
                  if(knightIsWhite)
                  {
                     if(WhiteCanTake(squares[endRow, endCol]))
                     {
                        ret.Add(new Move(startRow, startCol, endRow, endCol, false, ""));
                     }
                  }
                  else
                  {
                     if (BlackCanTake(squares[endRow, endCol]))
                     {
                        ret.Add(new Move(startRow, startCol, endRow, endCol, false, ""));
                     }
                  }
               }
            }
         }
         return ret;
      }
      static void Main(string[] args)
      {
         Board board = new Board();
         board.Init();
         board.PrintBoard();
         //board.MakeMove(new Move(1, 4, 3, 4, false, "test"));
         //board.PrintBoard();

         System.Threading.Thread.Sleep(5000);
      }
   }
}

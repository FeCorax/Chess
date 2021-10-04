using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoard
{
   class ConsolePlayer : BoardCommunicator
   {
      private Board.piece[,] squares;
      public static char Piece_convert(Board.piece input)
      {
         switch (input)
         {
            case Board.piece.empty:
               return '-';
            case Board.piece.pawn_w:
               return 'P';
            case Board.piece.pawn_b:
               return 'p';
            case Board.piece.knight_w:
               return 'N';
            case Board.piece.knight_b:
               return 'n';
            case Board.piece.bishop_w:
               return 'B';
            case Board.piece.bishop_b:
               return 'b';
            case Board.piece.rook_w:
               return 'R';
            case Board.piece.rook_b:
               return 'r';
            case Board.piece.queen_w:
               return 'Q';
            case Board.piece.queen_b:
               return 'q';
            case Board.piece.king_w:
               return 'K';
            case Board.piece.king_b:
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
            for (int j = 0; j < 8; j++)
            {
               Console.Write(Piece_convert(squares[i, j]) + " ");
            }
            Console.Write('\n');
         }
         Console.WriteLine("-------------------");
         Console.WriteLine("*  |A B C D E F G H\n");
      }
      public string GetMove()
      {
         Console.Clear();
         PrintBoard();
         return Console.ReadLine();
      }

      public void SetBoardState(Board.piece[,] pieces)
      {
         squares = pieces;
      }

      public void SetLegalMoves(List<string> moves)
      {
         //throw new NotImplementedException();
      }

      public void SetWinner(string winner)
      {
         Console.WriteLine(winner);
         Console.ReadLine();
      }
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoard
{
   
   class Board
   {
      public enum piece {empty, pawn_w, pawn_b, knight_w, knight_b, bishop_w, bishop_b, rook_w, rook_b, queen_w, queen_b, king_w, king_b };
      private piece[,] squares;
      private int[,] whiteKingSquare;
      private int[,] blackKingSquare;
      private bool blackKingHasMoved;
      private bool blackKingsRookHasMoved;
      private bool blackQueensRookHasMoved;
      private bool whiteKingHasMoved;
      private bool whiteKingsRookHasMoved;
      private bool whiteQueensRookHasMoved;
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
         /*
         for (int i = 0; i < 8; i++)
         {
            squares[1, i] = piece.pawn_w;
            squares[6, i] = piece.pawn_b;
         }
         */
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
         this.whiteKingSquare = new int[0, 4];
         this.blackKingSquare = new int[7, 4];
         blackKingHasMoved = false;
         blackKingsRookHasMoved = false;
         blackQueensRookHasMoved = false;
         whiteKingHasMoved = false;
         whiteKingsRookHasMoved = false;
         whiteQueensRookHasMoved = false;

      }
      public static char IntToChar(int i)
      {
         string cols = "ABCDEFGH";
         return cols[i];
      }
      public static int CharToInt(char a)
      {
         return a - 'A';
      }
      public static char Piece_convert(piece input)
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
      public void PrintMoves(List<Move> moves)
      {
         foreach(Move move in moves)
         {
            Console.WriteLine(move.moveCode);
         }
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
      private bool pieceCanTake(piece movingPiece, piece destPiece)
      {
         if(movingPiece == piece.empty)
         {
            return false;
         }
         if(movingPiece == piece.pawn_b || movingPiece == piece.knight_b || movingPiece == piece.bishop_b || movingPiece == piece.rook_b || movingPiece == piece.queen_b || movingPiece == piece.king_b)
         {
            return BlackCanTake(destPiece);
         }
         else
         {
            return WhiteCanTake(destPiece);
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
         List<Move> ret = new List<Move>();
         piece color = squares[startRow, startCol];
         if (color != piece.knight_b && color != piece.knight_w)
         {
            Console.WriteLine("thats not a knight");
            return ret;
         }
         int[,] directions = new int[,] { { 1, 2 }, { 2, 1 }, { -2, -1 }, { -1, -2 }, { 2, -1 }, { 1, -2 }, { -1, 2 }, { -2, 1 } };
         
         int endRow;
         int endCol;
         piece endingPiece;
         for (int i = 0; i < 8; i++)
         {
            endRow = startRow + directions[i, 0];
            endCol = startCol + directions[i, 1];
            
            if(endRow >= 0 && endRow < 9 && endCol >= 0 && endRow < 9)
            {
               endingPiece = squares[endRow, endCol];
               if(pieceCanTake(color, endingPiece))
               {
                  ret.Add(new Move(startRow, startCol, endRow, endCol, false, color, endingPiece));
               }
            }
         }
         return ret;
      }
      public List<Move> RookMoves(int startRow, int startCol)
      {
         List<Move> ret = new List<Move>();
         int rowItr = startRow;
         int colItr = startCol;
         piece color = squares[startRow, startCol];
         if(color != piece.rook_b && color != piece.rook_w)
         {
            Console.WriteLine("thats not a rook");
            return ret;
         }
         int[,] cardinalDicrections = new int[,] { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
         //check the 4 directions a rook can move, ending when it reaches a board edge or another piece
         piece endingPeice;
         for (int i = 0; i < 4; i++)
         {
            rowItr = startRow;
            colItr = startCol;
            while (true)
            {
               rowItr += cardinalDicrections[i, 0];
               colItr += cardinalDicrections[i, 1];
               if( !(rowItr < 8 && rowItr >= 0 && colItr < 8 && colItr >= 0))
               {
                  break;
               }
               endingPeice = squares[rowItr, colItr];
               if (endingPeice == piece.empty)
               {
                  ret.Add(new Move(startRow, startCol, rowItr, colItr, false, color, endingPeice));
                  continue;
               }
               else if (pieceCanTake(color, squares[rowItr, colItr]))
               {
                  ret.Add(new Move(startRow, startCol, rowItr, colItr, false, color, endingPeice));
                  break;
               }
               else
               {
                  break;
               }
            }

         }



         return ret;
      }
      public List<Move> BishopMoves(int startRow, int startCol)
      {
         List<Move> ret = new List<Move>();
         int rowItr = startRow;
         int colItr = startCol;
         piece color = squares[startRow, startCol];
         if (color != piece.bishop_b && color != piece.bishop_w)
         {
            Console.WriteLine("thats not a bishop");
            return ret;
         }
         piece endingPeice;
         int[,] cardinalDicrections = new int[,] { { 1, 1 }, { -1, -1 }, { -1, 1 }, { 1, -1 } };
         //check the 4 directions a bishop can move, ending when it reaches a board edge or another piece
         for (int i = 0; i < 4; i++)
         {
            rowItr = startRow;
            colItr = startCol;
            while (true)
            {
               rowItr += cardinalDicrections[i, 0];
               colItr += cardinalDicrections[i, 1];
               if (!(rowItr < 8 && rowItr >= 0 && colItr < 8 && colItr >= 0))
               {
                  break;
               }
               endingPeice = squares[rowItr, colItr];
               if (endingPeice == piece.empty)
               {
                  ret.Add(new Move(startRow, startCol, rowItr, colItr, false, color, endingPeice));
                  continue;
               }
               else if (pieceCanTake(color, endingPeice))
               {
                  ret.Add(new Move(startRow, startCol, rowItr, colItr, false, color, endingPeice));
                  break;
               }
               else
               {
                  break;
               }
            }

         }



         return ret;
      }
      public List<Move> QueenMoves(int startRow, int startCol)
      {
         List<Move> ret = new List<Move>();
         int rowItr = startRow;
         int colItr = startCol;
         piece color = squares[startRow, startCol];
         if (color != piece.queen_b && color != piece.queen_w)
         {
            Console.WriteLine("thats not a queen");
            return null;
         }
         piece endingPeice;
         int[,] cardinalDicrections = new int[,] { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 }, { 1, 1 }, { -1, -1 }, { -1, 1 }, { 1, -1 } };
         //check the 8 directions a queen can move, ending when it reaches a board edge or another piece
         for (int i = 0; i < 8; i++)
         {
            rowItr = startRow;
            colItr = startCol;
            while (true)
            {
               rowItr += cardinalDicrections[i, 0];
               colItr += cardinalDicrections[i, 1];
               if (!(rowItr < 8 && rowItr >= 0 && colItr < 8 && colItr >= 0))
               {
                  break;
               }
               endingPeice = squares[rowItr, colItr];
               if (endingPeice == piece.empty)
               {
                  ret.Add(new Move(startRow, startCol, rowItr, colItr, false, color, endingPeice));
                  continue;
               }
               else if (pieceCanTake(color, endingPeice))
               {
                  ret.Add(new Move(startRow, startCol, rowItr, colItr, false, color, endingPeice));
                  break;
               }
               else
               {
                  break;
               }
            }

         }



         return ret;
      }
      public List<Move> KingMoves(int startRow, int startCol)
      {
         List<Move> ret = new List<Move>();
         piece color = squares[startRow, startCol];
         if (color != piece.king_b && color != piece.king_w)
         {
            Console.WriteLine("thats not a king");
            return ret;
         }
         int[,] directions = new int[,] { { 1, 0 }, { 1, 1 }, { 0, 1 }, { -1, 1 }, { -1, 0 }, { -1, -1 }, { 0, -1 }, { 1, -1 }  };
         
         int endRow;
         int endCol;
         piece endingPiece;
         for (int i = 0; i < 8; i++)
         {
            endRow = startRow + directions[i, 0];
            endCol = startCol + directions[i, 1];

            if (endRow >= 0 && endRow < 9 && endCol >= 0 && endRow < 9)
            {
               endingPiece = squares[endRow, endCol];
               if (pieceCanTake(color, endingPiece))
               {
                  ret.Add(new Move(startRow, startCol, endRow, endCol, false, color, endingPiece));
               }
            }
         }
         if(color == piece.king_w)
         {
            if(!whiteKingHasMoved && ! whiteKingsRookHasMoved)
            {
               //Check for kings-side caslte
            }
            if(!whiteKingHasMoved && !whiteQueensRookHasMoved)
            {
               //Check for Queens-side caslte
            }
         }
         else
         {
            if (!blackKingHasMoved && !blackKingsRookHasMoved)
            {
               //Check for kings-side caslte
            }
            if (!blackKingHasMoved && !blackQueensRookHasMoved)
            {
               //Check for Queens-side caslte
            }
         }
         return ret;
      }
      public List<Move> PawnMoves(int startRow, int startCol)
      {
         List<Move> ret = new List<Move>();
         piece color = squares[startRow, startCol];
         int primaryDirection;
         int promotionRow;
         int initRow;
         if (color != piece.pawn_b && color != piece.pawn_w)
         {
            Console.WriteLine("thats not a king");
            return ret;
         }
         if(color == piece.pawn_w)
         {
            primaryDirection = 1;
            promotionRow = 7;
            initRow = 1;
         }
         else
         {
            primaryDirection = -1;
            promotionRow = 0;
            initRow = 6;
         }
         if(squares[startRow + primaryDirection, startCol] == piece.empty)
         {
            ret.Add(new Move(startRow, startCol, startRow + primaryDirection, startCol, false, color, piece.empty));
            if(startRow == initRow && squares[(startRow + (2 * primaryDirection)), startCol] == piece.empty)
            {
               ret.Add(new Move(startRow, startCol, (startRow + (2 * primaryDirection)), startCol, true, color, piece.empty));
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
         //System.Threading.Thread.Sleep(5000);
         board.PrintMoves(board.KingMoves(0, 4));
         var name = Console.ReadLine();
      }
   }
}

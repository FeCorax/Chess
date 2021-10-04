
using System.Collections.Generic;

namespace ChessBoard
{
   interface BoardCommunicator
   {
      string GetMove();
      void SetLegalMoves(List<string> moves);
      void SetBoardState(Board.piece[,] pieces);
      void SetWinner(string winner);

   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    class Game
    {
        private Board board = new Board();

        private PieceType currentPlayer = PieceType.BLACK;

        private PieceType winner = PieceType.NONE;
        public PieceType Winner { get { return winner; } }
        public bool CanBePlaced(int x, int y)
        {
            return board.CanBePlaced(x, y);
        }
        public Piece PlaceAPiece(int x, int y)
        {

            Piece piece = board.PlaceAPiece(x, y, currentPlayer);//e.X, e.Y為滑鼠座標位置
            if (piece != null)
            {
                CheckWinner();   //檢查獲勝

                //交換選手
                if (currentPlayer == PieceType.BLACK)
                    currentPlayer = PieceType.WHITE;
                else if (currentPlayer == PieceType.WHITE)
                    currentPlayer = PieceType.BLACK;

                return piece;
            }

            return null;
        }
        public void CheckWinner()
        {
            int centerX = board.LastPlacedNode.X;
            int centerY = board.LastPlacedNode.Y;

            //檢查8個方向
            for (int xDir = -1; xDir <= 1; xDir++)
            {
                for (int yDir = -1; yDir <= 1; yDir++)
                {
                    //排除中間的情況
                    if (xDir == 0 && yDir == 0)
                        continue;  //略過這次迴圈

                    //紀錄看到的棋子
                    int count = 1;
                    while (count < 5)
                    {
                        int targetX = centerX + count * xDir;
                        int targetY = centerY + count * yDir;

                        //檢查顏色是否相同 
                        if (targetX < 0 || targetX >= Board.NODE_COUNT ||   //超越邊界的話不用檢查
                            targetY < 0 || targetY >= Board.NODE_COUNT ||
                            board.GetPieceType(targetX, targetY) != currentPlayer)
                            break;

                        count++;
                    }

                    int count2 = 1;
                    while (count2 < 5)
                    {
                        int targetX = centerX - count2 * xDir;
                        int targetY = centerY - count2 * yDir;

                        if (targetX < 0 || targetX >= Board.NODE_COUNT ||
                        targetY < 0 || targetY >= Board.NODE_COUNT ||
                        board.GetPieceType(targetX, targetY) != currentPlayer)
                        {
                            break;
                        }
                        count2++;
                    }

                    //檢查是否有5顆棋子
                    if (count == 5)
                        winner = currentPlayer;
                    if (count + count2 > 5)
                        winner = currentPlayer;
                }
            }
        }
        public void gameReset()
        {
            board.gameReset();
            winner = PieceType.NONE;
            currentPlayer = PieceType.BLACK;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Gomoku
{

    class Board
    {
        public static readonly int NODE_COUNT = 9;
        private static readonly Point No_Match_Node = new Point(-1, -1);   //不存在點座標

        private static readonly int OFFSET = 60; //邊界距離 
        private static readonly int NODE_REDIUS = 10; //節點半徑
        private static readonly int NODE_DISTANCE = 72; //兩節點距離

        private Piece[,] pieces = new Piece[NODE_COUNT, NODE_COUNT];  //存放棋子 

        private Point lastPlacedNode = No_Match_Node;       //只能修改這邊
        public Point LastPlacedNode { get{return lastPlacedNode ; } }

        public PieceType GetPieceType(int nodeIDX, int nodeIDY)
        {
            if (pieces[nodeIDX, nodeIDY] == null)
                return PieceType.NONE;
            else
                return pieces[nodeIDX, nodeIDY].GetPieceType();
        }
        public bool CanBePlaced(int x, int y)
        {
            Point nodeID = FindTheClosetNode(x, y);  //找出最近的節點

            if (nodeID == No_Match_Node)
                return false;

            if (pieces[nodeID.X, nodeID.Y] != null)   //判斷是否為空位
                return false;

            return true;
        }
        public Piece PlaceAPiece(int x, int y, PieceType type)
        {
            Point nodeID = FindTheClosetNode(x, y);  //找出最近的節點

            if (nodeID == No_Match_Node)
                return null;

            if (pieces[nodeID.X, nodeID.Y] != null)   //判斷是否為空位
                return null;


            Point formPos = convertToFormPosition(nodeID);
            if (type == PieceType.BLACK)//根據type產生相對應的棋子
                pieces[nodeID.X, nodeID.Y] = new Piece.BlackPiece(formPos.X, formPos.Y);
            else if (type == PieceType.WHITE)
                pieces[nodeID.X, nodeID.Y] = new Piece.WhitePiece(formPos.X, formPos.Y);


            //紀錄最後下棋位置
            lastPlacedNode = nodeID;

            return pieces[nodeID.X, nodeID.Y];
        }

        private Point convertToFormPosition(Point nodeID)
        {
            Point formPosition = new Point();
            formPosition.X = nodeID.X * NODE_DISTANCE + OFFSET;
            formPosition.Y = nodeID.Y * NODE_DISTANCE + OFFSET;

            return formPosition;
        }
        private Point FindTheClosetNode(int x, int y)   //先做一維判斷,二維等於判斷兩次
        {
            int NodeX = FindTheClosetNode(x);
            if (NodeX == -1 || NodeX >= NODE_COUNT)
                return No_Match_Node;

            int NodeY = FindTheClosetNode(y);
            if (NodeY == -1 || NodeY >= NODE_COUNT)
                return No_Match_Node;

            return new Point(NodeX, NodeY);
        }
        private int FindTheClosetNode(int pos)
        {
            if (pos < OFFSET - NODE_REDIUS) //判斷是否落在棋盤內
                return -1;

            pos -= OFFSET;

            int quotient = pos / NODE_DISTANCE;  //商數 = 棋子座標
            int remainder = pos % NODE_DISTANCE; //餘數

            if (remainder <= NODE_REDIUS)
                return quotient;
            else if (remainder >= NODE_DISTANCE - NODE_REDIUS)
                return quotient + 1;
            else
                return -1;
        }
        public void gameReset()
        {
            pieces = new Piece[NODE_COUNT, NODE_COUNT];
        }
    }
}

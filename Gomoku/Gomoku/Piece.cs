using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Gomoku
{
    abstract class Piece : PictureBox
    {
        private static readonly int img_width = 65;
        public Piece(int x, int y)
        {
            this.BackColor = Color.Transparent;
            this.Location = new Point(x - img_width / 2, y - img_width / 2);
            this.Size = new Size(img_width, img_width);

        }

        public abstract PieceType GetPieceType();
        public class BlackPiece : Piece
        {
            public BlackPiece(int x, int y) : base(x, y)
            {
                this.Image = Properties.Resources.black;
            }

            public override PieceType GetPieceType()
            {
                return PieceType.BLACK;
            }
        }
        public class WhitePiece : Piece
        {
            public WhitePiece(int x, int y) : base(x, y)
            {
                this.Image = Properties.Resources.white;
            }
            public override PieceType GetPieceType()
            {
                return PieceType.WHITE;
            }
        }
    }
}

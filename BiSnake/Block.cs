using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiSnake
{
    class Block
    {
        SolidBrush myBrush = new SolidBrush(Color.DodgerBlue);

        static int width = 20;
        public static int Width
        {
            get { return Block.width; }
        }

        static int height = 20;
        public static int Height
        {
            get { return Block.height; }
        } 

        int corX;
        public int CorX
        {
            get { return corX; }
            set { corX = value; }
        }

        int corY;
        public int CorY
        {
            get { return corY; }
            set { corY = value; }
        }

        public Block(int x,int y)
        {
            this.corX = x;
            this.corY = y;
        }

        public void Draw(Graphics g,SolidBrush br=null)
        {
            try
            {
                if(br==null)
                    g.FillRectangle(myBrush,this.corX,this.corY,Block.width,Block.height);
                else
                    g.FillRectangle(br, this.corX, this.corY, Block.width, Block.height);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

    }
}

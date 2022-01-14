using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiSnake
{
    class Snake
    {
        const int maxLength = 50;
        public int MaxLength
        {
            get { return maxLength; }
        }

        int nowLength;
        public int NowLength
        {
            get { return nowLength; }
            set { nowLength = value; }
        }

        Block[] body = new Block[maxLength];
        internal Block[] Body
        {
            get { return body; }
            set { body = value; }
        }

        Direction direction;
        internal Direction Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        //KHOI TAO SNAKE VOI HEAD, DIRECTION VA TOA DO HEAD
        public Snake()
        {
            body[0] = new Block(0, 0);
            nowLength = 1;
            direction = Direction.RIGHT;
        }

        //CAP NHAT TOA DO CUA CAC PHAN CON LAI THEO HEAD SAU KHI MOVE
        private void ConfigureCordinate()
        {
            for (int i = nowLength - 1; i > 0; i--)
            {
                body[i].CorX = body[i - 1].CorX;
                body[i].CorY = body[i - 1].CorY;
            }
        }

        //DI CHUYEN CON RAN LA DI CHUYEN THAY DOI PHAN DAU
        public void Move()
        {
            this.ConfigureCordinate();
            if (direction == Direction.RIGHT)
                body[0].CorX += (Block.Width);
            else if (direction == Direction.UP)
                body[0].CorY -= (Block.Height);
            else if (direction == Direction.DOWN)
                body[0].CorY += (Block.Height);
            else if (direction == Direction.LEFT)
                body[0].CorX -= (Block.Width);

        }

        //KHI CON RAN LON LEN ->THEM BLOCK MOI VAO BODY
        //Grow sau do phai configure/move de dam bao toa do block moi la dung
        public void Grow()
        {
            body[nowLength] = new Block(body[nowLength - 1].CorX-1, body[nowLength - 1].CorY-1);
            nowLength++;
        }

        public bool BiteItself()
        {
            for (int i = 1; i < NowLength; i++)
                if (Body[0].CorX == Body[i].CorX && Body[0].CorY == Body[i].CorY)
                    return true;
            return false;
        }
        //VE TOAN BO SNAKE
        public void Display(Graphics g)
        {
            //ko dung body.Length => tranh err: Object is not set to reference 
            //khoi tao 1 mang class nhung chua  tao tung phan tu
            for (int i = 0; i < nowLength; i++)
            {
                body[i].Draw(g);
            }
        }

    }
}

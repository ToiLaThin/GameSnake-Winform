using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiSnake
{
    class GameProcessor
    {
        //TO DO import sound khi ran an dc moi
        //cap nhat link tham khao 
        int sleepTime;

        Graphics myGraphics;
        Label score;
        System.Windows.Forms.Timer timer;
        Panel canvas; int maxX; int maxY;
        Snake mySnake;
        Random rander; Block prey;
        bool regeneratePrey;//to know if(whether) the prey is alive or not

        public GameProcessor (Graphics g,Panel pnl,Snake aSnake,Label score,System.Windows.Forms.Timer timer)
        {
            this.mySnake = aSnake;
            this.score = score;
            this.myGraphics = g;
            this.canvas = pnl;
            this.timer = timer;//de dung game
            //chieu rong la boi so cua block width, height
            this.canvas.Width = 19 * Block.Width;
            this.canvas.Height = 19 * Block.Height;
            this.maxX = canvas.Width;
            this.maxY = canvas.Height;
            this.prey = new Block(0, 0);
            this.regeneratePrey = true;


            //khoi tao do kho ban dau EASY
            this.sleepTime = (int)SleepTime.EASY;
        }    

        private void GeneratePrey()
        {
            //make sure ko co loi object not set to ref
            this.rander = new Random();
            regeneratePrey=false;
            do
            {
                int num = rander.Next(mySnake.NowLength - 1);
                prey.CorX = rander.Next(0, 18) * Block.Width;
                prey.CorY = rander.Next(0, 18) * Block.Height;

                if (prey.CorX < 0 || prey.CorY < 0 || prey.CorX > this.canvas.Width - Block.Width || prey.CorY > this.canvas.Height - Block.Height)
                {
                    regeneratePrey = true;
                    break;
                }

                for (int i = 0; i < mySnake.NowLength; i++)
                    if (prey.CorX == mySnake.Body[i].CorX && prey.CorY == mySnake.Body[i].CorY)
                    {
                        regeneratePrey = true; 
                        break;
                    }
            }while(regeneratePrey);
        }

        
        //phai make sure prey da duoc khoi tao(khac null)
        private bool PreyKilled()
        {
            //dk kha phuc tap do toa do bi lech
            if (mySnake.Body[0].CorX == prey.CorX && mySnake.Body[0].CorY == prey.CorY)
                return true;
            else
                return false;
        }
        
        public void GameLoop(Object sender,EventArgs e)
        {
            //update snake cordinations & draw food,prey with cordi
            while (regeneratePrey)
                GeneratePrey();
            mySnake.Move();
            dealIfSnakeHitBorder();
            prey.Draw(myGraphics, new SolidBrush(Color.OrangeRed));
            mySnake.Display(myGraphics);

            //check if this snake now cordination we can execute prey or not
            if (PreyKilled())
            {
                mySnake.Grow();
                this.regeneratePrey = true;
            }

            this.score.Text = "Score: " + (mySnake.NowLength - 1).ToString();

            if (mySnake.BiteItself())
                mySnake.NowLength = mySnake.NowLength / 2;

            if(this.mySnake.NowLength > 0 && this.mySnake.NowLength <= 20)
                this.sleepTime = (int)SleepTime.EASY;
            else if (this.mySnake.NowLength >= 21 && this.mySnake.NowLength <= 40)
                this.sleepTime = (int)SleepTime.NORMAL;
            else if (this.mySnake.NowLength >= 41)
            {
                this.sleepTime = (int)SleepTime.HARD;
                if (mySnake.NowLength == mySnake.MaxLength)
                {
                    timer.Stop();//TO DO tai sao mbox.show phai nam o sau timer.Stop()                
                    MessageBox.Show("You Won!!!");
                    return;
                }
            }
            
            Thread.Sleep(this.sleepTime);
            canvas.Invalidate();
        }

        private void dealIfSnakeHitBorder()
        {
            if (mySnake.Body[0].CorX + Block.Width > this.canvas.Width)
                mySnake.Body[0].CorX = 0;
            else if (mySnake.Body[0].CorX < 0)
                mySnake.Body[0].CorX = this.canvas.Width;
            //nho chinh height cua panel1 lon hon voi height tinh ra
            //keo dai form1 xuong de nhin thay toan bo height cua panel1 
            else if (mySnake.Body[0].CorY + Block.Height > this.canvas.Height)
                mySnake.Body[0].CorY = 0;
            else if (mySnake.Body[0].CorY < 0)
                mySnake.Body[0].CorY = this.canvas.Height;
        }
    }

    enum SleepTime
    {
        HARD = 25,
        NORMAL = 50,
        EASY = 100,
    }

}

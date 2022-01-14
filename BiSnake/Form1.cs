using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiSnake
{
    public partial class Form1 : Form
    {
        Graphics g;
        GameProcessor gameProcessor;
        Snake mySnake;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public Form1()
        {
            InitializeComponent();
            //dam bao form se uu tien nhan su kien nhan phim truoc tien
            this.KeyPreview = true;         
            this.timer.Interval = 1;

            //phan code nay la de timer.Start trong Form1_KeyDown ko bi loi
            g = this.panel1.CreateGraphics();
            mySnake = new Snake();
            gameProcessor = new GameProcessor(g, this.panel1, mySnake, this.lbScore, this.timer);          
            this.timer.Tick += this.gameProcessor.GameLoop;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            panel1.Invalidate();
            mySnake = new Snake();
            this.timer.Tick -= this.gameProcessor.GameLoop;//-= GameLoop cua gameProcessor cu

            gameProcessor = new GameProcessor(g, this.panel1, mySnake, this.lbScore, this.timer);
            //vua -= 1 GameLoop va sau do += GameLoop cua gameProcessor moi
            this.timer.Tick += this.gameProcessor.GameLoop;
            //to perform a game loop every time the timer tick 
            //ko duoc dung vong while vi khong chay dc
            //xem lai delegate event multicast callback
            //tai sao ko la GameLoop()
            timer.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
                mySnake.Direction = Direction.UP;
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
                mySnake.Direction = Direction.RIGHT;
            else if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
                mySnake.Direction = Direction.DOWN;
            else if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
                mySnake.Direction = Direction.LEFT;
            this.timer.Start(); 
        }
    }
}

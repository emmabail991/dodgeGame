using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dodgeGame
{
    public partial class Form1 : Form
    {




        int obstacle1Counter = 0;
        int obstacle2Counter = 0;

        //playerspeed
        int playerSpeed = 4;


        //obsticle movment speed and direction
        int obstacle1Speed = 4;
        int obstacle2Speed = -4;


        //momvemt keys
        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;

        //hero size and starting location
        Rectangle hero = new Rectangle(20, 200, 20, 20);
      
        //obsticle spawn
        List<Rectangle> obstacle1 = new List<Rectangle>();
        List<Rectangle> obstacle2 = new List<Rectangle>();


         //brush
        SolidBrush whiteBrush = new SolidBrush(Color.White);


        public Form1()
        {
            InitializeComponent();
        }

        //hero movment
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                 

            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                 

            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {

            //obstacle spawn ammount
            obstacle1Counter++;
            obstacle2Counter++;

            //move hero
            if (wDown == true && hero.Y > 0)
            {
                hero.Y -= playerSpeed;
            }

            if (sDown == true && hero.Y < this.Height - hero.Height)
            {
                hero.Y += playerSpeed;
            }
            if (aDown == true && hero.X > 0)
            {
                hero.X -= playerSpeed;
            }
            if (dDown == true && hero.X < this.Width - hero.Width)
            {
                hero.X += playerSpeed;
            }

             
            //obsticle 1 spawn
            if (obstacle1Counter == 20)  
            {                 
                obstacle1.Add(new Rectangle(200, 0, 10, 40));
                obstacle1Counter = 0;
                
            }

            //obsticle 2 spawn
            if (obstacle2Counter == 20)   
            {

                obstacle2.Add(new Rectangle(350, 400, 10, 40));
                obstacle2Counter = 0;
            }

            
            for (int i = 0; i < obstacle1.Count(); i++)
            {    
                int y = obstacle1[i].Y + obstacle1Speed;

                //replace the rectangle in the list with updated one using new y 
                obstacle1[i] = new Rectangle(obstacle1[i].X, y, 10, 40);
            }
            for (int i = 0; i < obstacle1.Count(); i++)
            {
                if (hero.IntersectsWith(obstacle1[i]))
                {
                    gameTimer.Enabled = false;        
                }
            }


            for (int i = 0; i < obstacle2.Count(); i++)
            {
                //find the new postion of y based on speed 
                int y = obstacle2[i].Y + obstacle2Speed;

                //replace the rectangle in the list with updated one using new y 
                obstacle2[i] = new Rectangle(obstacle2[i].X, y, 10, 40);
            }
            for (int i = 0; i < obstacle2.Count(); i++)
            {
                if (hero.IntersectsWith(obstacle2[i]))
                {
                    gameTimer.Enabled = false;
                }
            }

            if(hero.X >this.Width-25)
            {
                gameTimer.Enabled = false;
            }

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
          
            //draw hero 
            e.Graphics.FillRectangle(whiteBrush, hero);

            //draw balls 
            for (int i = 0; i < obstacle1.Count(); i++)
            {
                e.Graphics.FillRectangle(whiteBrush, obstacle1[i]);
            }

            //draw balls 
            for (int i = 0; i < obstacle2.Count(); i++)
            {
                e.Graphics.FillRectangle(whiteBrush, obstacle2[i]);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Donkey_Kong_ProjectX
{
	public partial class Form1 : Form
	{
		DateTime date;
		public Point start = new Point(26, 410);
		int lifes = 3;
		public Form1()
		{
			InitializeComponent();
			date = DateTime.Now;

			timer3.Interval = 10;
			timer3.Tick += new EventHandler(tickTimer);
			timer3.Start();
		}
		public void tickTimer(object sender, EventArgs e)
		{
			long tick = DateTime.Now.Ticks - date.Ticks;
			DateTime stopWatc = new DateTime();
			stopWatc = stopWatc.AddTicks(tick);
			timelable.Text = String.Format("Time {0:mm:ss}", stopWatc);
		}
		bool goleft, goright,gotop,godown,ladder,key,jump;

		int havegold = 0;
		string[] picture = 
			{
			@"F:\Donkey Kong ProjectX\Donkey Kong ProjectX\Resources\goldx1.png",
			@"F:\Donkey Kong ProjectX\Donkey Kong ProjectX\Resources\goldx2.png",
			@"F:\Donkey Kong ProjectX\Donkey Kong ProjectX\Resources\goldx3.png",
			};
		public int goldspin = 0;

		Image goldx;
		public void GoldSpin() //Spinning of gold
		{
			if (goldspin == 0)
			{
				goldx = Image.FromFile(picture[2]);
				gold.Image = goldx;
				gold1.Image = goldx;
				gold2.Image = goldx;
				gold3.Image = goldx;
				gold4.Image = goldx;
				gold5.Image = goldx;
				gold6.Image = goldx;
				gold7.Image = goldx;
				gold8.Image = goldx;
				gold9.Image = goldx;
				gold10.Image = goldx;
				gold11.Image = goldx;
				goldspin = 1;
			}
			else if (goldspin == 1)
			{
				goldx = Image.FromFile(picture[1]);
				gold.Image = goldx;
				gold1.Image = goldx;
				gold2.Image = goldx;
				gold3.Image = goldx;
				gold4.Image = goldx;
				gold5.Image = goldx;
				gold6.Image = goldx;
				gold7.Image = goldx;
				gold8.Image = goldx;
				gold9.Image = goldx;
				gold10.Image = goldx;
				gold11.Image = goldx;
				goldspin = 2;
			}
			else if (goldspin == 2)
			{
				goldx = Image.FromFile(picture[0]);
				gold.Image = goldx;
				gold1.Image = goldx;
				gold2.Image = goldx;
				gold3.Image = goldx;
				gold4.Image = goldx;
				gold5.Image = goldx;
				gold6.Image = goldx;
				gold7.Image = goldx;
				gold8.Image = goldx;
				gold9.Image = goldx;
				gold10.Image = goldx;
				gold11.Image = goldx;
				goldspin = 3;
			}
			else if (goldspin == 3)
			{
				goldx = Image.FromFile(picture[1]);
				gold.Image = goldx;
				gold1.Image = goldx;
				gold2.Image = goldx;
				gold3.Image = goldx;
				gold4.Image = goldx;
				gold5.Image = goldx;
				gold6.Image = goldx;
				gold7.Image = goldx;
				gold8.Image = goldx;
				gold9.Image = goldx;
				gold10.Image = goldx;
				gold11.Image = goldx;
				goldspin = 0;
			}
		}
		private void timer2_Tick(object sender, EventArgs e)
		{
			GoldSpin();
		}

		int jumpSpeed =12;
		int force;
		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Up)
			{
				gotop = false;
			}
			if (e.KeyCode == Keys.Down)
			{
				godown = false;
				ladder = false;
			}
			if (e.KeyCode == Keys.Left)
			{
				goleft = false;
			}
			if (e.KeyCode == Keys.Right)
			{
				goright = false;
			}
			if (jump)
			{
				jump = false;
			}
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Up)
			{
				gotop = true;
			}
			if (e.KeyCode == Keys.Down && ladder)
			{
				godown = true;
			}
			if (e.KeyCode == Keys.Left)
			{
				goleft = true;
			}
			if (e.KeyCode == Keys.Right)
			{
				goright = true;
			}
			if (e.KeyCode == Keys.Up && !jump)
			{
				jump = true;

			}
		}
		private void timer1_Tick(object sender, EventArgs e)
		{
			player.Top += jumpSpeed; //physics
			if (jump && force<0 )
			{
				jump = false;
			}
			if (jump && !ladder)
			{
				jumpSpeed = -12;
				force -= 1;
			}
			else
			{
				jumpSpeed =3;
			}

			if (goleft)
			{
				player.Image = Image.FromFile("F:/Donkey Kong ProjectX/Donkey Kong ProjectX/Resources/player1.png");
				player.Left -= 10;
				if (jump)
				{
					player.Left -= 1;
				}
			}
			if (goright)
			{
				player.Image = Image.FromFile("F:/Donkey Kong ProjectX/Donkey Kong ProjectX/Resources/playerright.png");
				player.Left += 10;
				if (jump)
				{
					player.Left += 1;
				}
			}
			if (gotop)
			{
				player.Top += 5;
			}
			foreach (Control x in this.Controls)
			{
				if (x is PictureBox && Convert.ToString(x.Tag) == "ground") //Ground
				{
					if (player.Bounds.IntersectsWith(x.Bounds) && player.Top<x.Top && !godown)
					{
						player.Top = x.Top - player.Height +1;
						force = 8;
						ladder = false;
					}
					
				}
				if (x is PictureBox && Convert.ToString(x.Tag) == "ladder") //Ladders 
				{
					if (player.Bounds.IntersectsWith(x.Bounds))
					{
						ladder = true;
					}
					if (player.Bounds.IntersectsWith(x.Bounds) && gotop)
					{
						jumpSpeed = 0;
						player.Top -=10;
						
					}
					if (player.Bounds.IntersectsWith(x.Bounds) && godown && player.Top < x.Top)
					{
						player.Top =x.Top+20;
						player.Top += 4;
					}
				}
				if (x is PictureBox && Convert.ToString(x.Tag) == "key") //Gold key
				{
					if (player.Bounds.IntersectsWith(x.Bounds))
					{
						this.Controls.Remove(x);
						key = true;
					}
				}
				if (x is PictureBox && Convert.ToString(x.Tag) == "gold") //Gold
				{
					if (player.Bounds.IntersectsWith(x.Bounds))
					{
						this.Controls.Remove(x);
						havegold++;
						goldlable.Text = "Gold:" + havegold;
					}
				}
				if (x is PictureBox && Convert.ToString(x.Tag) == "walll") //WALS
				{
					if (player.Bounds.IntersectsWith(x.Bounds))
					{
						player.Left -= 8;
					}
				}
				if (x is PictureBox && Convert.ToString(x.Tag) == "wallr")
				{
					if (player.Bounds.IntersectsWith(x.Bounds))
					{
						player.Left += 8;
					}
				}
				if (x is PictureBox && Convert.ToString(x.Tag) == "roof")//Invisible roof
				{
					if (player.Bounds.IntersectsWith(x.Bounds))
					{
						force = -1;
					}
				}
				if (x is PictureBox && Convert.ToString(x.Tag) == "spikes") //SPIKES
				{
					if (player.Bounds.IntersectsWith(x.Bounds))
					{
						player.Location = start;
						lifes--;
						lifeslable.Text = "Lifes : " + lifes;
					}
				}

			}
			if (player.Bounds.IntersectsWith(door.Bounds) && key)
			{

				timer1.Stop();
				timer2.Stop();
				timer3.Stop();
				MessageBox.Show("Your time "+ timelable.Text + "\n " + "Gold you have "+havegold, "Level complete");
			}
			if (lifes == 0)
			{
				timer1.Stop();
				timer2.Stop();
				timer3.Stop();
				MessageBox.Show("Game over");
			}
		}
		
	}
}

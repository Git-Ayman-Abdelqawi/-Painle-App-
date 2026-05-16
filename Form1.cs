using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Painle_App
{
    public partial class Form1: Form
    {
        private Bitmap bm;
        private Graphics g;
        private bool Paint = false;
        private Point px, py;
        private int select = 0;
        private Pen P = new Pen(Color.Black, 1);
        private Pen ereas = new Pen(Color.Wheat, 40);
        int x1, y1, sx, sy, cx, cy;

        int cb1 = 0;
        private Color SelectedC = Color.Black; // اللون الافتراضي

        private Stack<Bitmap> undoStack = new Stack<Bitmap>();

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = cb1;
            bm = new Bitmap(pic.Width, pic.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.Wheat);
            pic.Image = bm;

        }

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            Paint = true;
            px = e.Location;

            cx = e.X;
            cy = e.Y;
            
        }

        void chickPicx(Bitmap bm, Stack<Point> sp, int x, int y,
            Color oildColor, Color new_color)
        {
            sp.Push(new Point(x, y));

            while(sp.Count > 0)
            {
                if (oildColor == Color.Wheat) return;
  
                Point p = sp.Pop();

                if(p.X < 0 || p.Y <0 ||p.X >= bm.Width - 1 || p.Y >= bm.Height-1)
                        continue;
                if(bm.GetPixel(p.X,p.Y) == oildColor)
                {
                    bm.SetPixel(p.X, p.Y, new_color);


                    sp.Push(new Point(p.X + 1, p.Y));
                    sp.Push(new Point(p.X - 1, p.Y));
                    sp.Push(new Point(p.X , p.Y + 1));
                    sp.Push(new Point(p.X , p.Y - 1));

                }

            }
        }

        private void Undo()
        {
            if (undoStack.Count > 0)
            {
                bm = undoStack.Pop();
                g = Graphics.FromImage(bm);
                pic.Image = bm;
                pic.Invalidate();
            }
        }

        private void pic_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (Paint)
            {
                if (select == 3)
                {
                    g.DrawEllipse(P, cx, cy, sx, sy);

                }
                if (select == 4)
                {
                    g.DrawRectangle(P, cx, cy, sx, sy);

                }

                if (select == 5)
                {
                    g.DrawLine(P, cx, cy, x1, y1);

                }

                if (select == 6)
                {
                    Point[] trianglPounts =
                        {
                        new Point(cx,cy),
                        new Point(x1,y1),
                        new Point(cx,y1)
                    };

                    g.DrawPolygon(P, trianglPounts);

                }
            }
        }

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            if(Paint)
            {
                if(select==1)
                {            
                        py = e.Location;
                        g.DrawLine(P, px, py);
                        px = py;
                }

                if (select==2)
                {                 
                        py = e.Location;
                        g.DrawLine(ereas, px, py);
                        px = py;
                }

                x1 = e.X;
                y1 = e.Y;
                sx = e.X -cx;
                sy = e.Y - cy;

               
                    pic.Invalidate();

                
            }
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            Paint = false;
            undoStack.Push((Bitmap)bm.Clone());
            sx = e.X - cx;
            sy = e.Y - cy;
            if (select == 3)
            {
                g.DrawEllipse(P, cx, cy, sx, sy);
                undoStack.Push((Bitmap)bm.Clone());
            }
            if (select == 4)
            {
                g.DrawRectangle(P, cx, cy, sx, sy);
                undoStack.Push((Bitmap)bm.Clone());

            }

            if (select == 5)
            {
                g.DrawLine(P, cx, cy, x1, y1);
                undoStack.Push((Bitmap)bm.Clone());
            }

            if (select == 6)
            {
                Point[] trianglPounts =
                    {
                        new Point(cx,cy),
                        new Point(x1,y1),
                        new Point(cx,y1)
                    };

                g.DrawPolygon(P, trianglPounts);
                undoStack.Push((Bitmap)bm.Clone());
            }
        }
        private void label6_Click(object sender, EventArgs e)
        {
            cb1++;
            comboBox1.SelectedIndex = cb1;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex <= 0)
                return;
            cb1--;
            comboBox1.SelectedIndex = cb1;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Size = int.Parse(comboBox1.SelectedItem.ToString());
            P.Width = Size;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            int Size = int.Parse(comboBox1.SelectedItem.ToString());
            P.Width = Size;
        }

        private void CreateColorButtons(object sender, EventArgs e)
        {
            Button btn = ((Button)sender);
            int tagn = int.Parse(btn.Tag.ToString());

            switch(tagn)
            {
                case 1: P.Color = Color.Pink; break;
                case 2: P.Color = Color.Beige; break;
                case 3: P.Color = Color.FromArgb(255, 255, 255, 200);break ;
                case 4: P.Color = Color.FromArgb(255, 144, 238, 144); break;
                case 5: P.Color = Color.FromArgb(255, 224, 255, 255); break;
                case 6: P.Color = Color.FromArgb(255, 216, 191, 216); break;
                case 7: P.Color = Color.FromArgb(255, 255, 182, 193); break;
                case 8: P.Color = Color.FromArgb(255, 128, 128, 128); break;
                case 9: P.Color = Color.FromArgb(255, 255, 192, 203); break;
                case 10: P.Color = Color.FromArgb(255, 255, 165, 0); break;
                case 11: P.Color = Color.FromArgb(255, 255, 255, 0); break;
                case 12: P.Color = Color.FromArgb(255, 144, 238, 144); break;
                case 13: P.Color = Color.FromArgb(255, 173, 216, 230); break;
                case 14: P.Color = Color.FromArgb(255, 0, 0, 139); break;
                case 15: P.Color = Color.FromArgb(255, 255, 0, 0); break;
                case 16: P.Color = Color.FromArgb(255, 255, 165, 0); break;
                case 17: P.Color = Color.FromArgb(255, 255, 255, 0); break;
                case 18: P.Color = Color.FromArgb(255, 0, 255, 0); break;
                case 19: P.Color = Color.FromArgb(255, 0, 255, 255); break;
                case 20: P.Color = Color.FromArgb(255, 0, 100, 0); break;
                case 21: P.Color = Color.FromArgb(55, 0, 0, 255); break;
                case 22: P.Color = Color.FromArgb(255, 0, 0, 0); break;
                case 23: P.Color = Color.FromArgb(255, 139, 0, 0); break;
                case 24: P.Color = Color.FromArgb(255, 165, 42, 42); break;
                case 25: P.Color = Color.FromArgb(255, 128, 128, 0); break;
                case 26: P.Color = Color.FromArgb(255, 0, 100, 0); break;
                case 27: P.Color = Color.FromArgb(255, 128, 0, 128); break;
                case 28: P.Color = Color.FromArgb(255, 0, 128, 128); break;


            }
            btColore2.BackColor = P.Color;
            SelectedC = P.Color;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            select = 4;
        }

        private void button42_Click(object sender, EventArgs e)
        {
            select = 5;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            select = 6;
        }      
        private void pic_MouseClick(object sender, MouseEventArgs e)
        {
          if(select == 7)
            {
                if (bm == null) return;

                Color Old_Color = bm.GetPixel(e.X, e.Y);
                Color New_Color = SelectedC;

                if (Old_Color == SelectedC) return;

                Stack<Point> sp = new Stack<Point>();
                chickPicx(bm, sp, e.X, e.Y, Old_Color, New_Color);
                undoStack.Push((Bitmap)bm.Clone());
                pic.Invalidate();

            }
        }

        private void button43_Click(object sender, EventArgs e)
        {
            select = 7;
        }
        private void button44_Click(object sender, EventArgs e)
        {
            Undo();
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
             SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
            sfd.Title = "اختر مكان حفظ الصورة";

            if(sfd.ShowDialog() == DialogResult.OK)
            {
                Bitmap btm = bm.Clone(new Rectangle(0, 0, pic.Width, pic.Height), bm.PixelFormat);

                btm.Save(sfd.FileName);
            }

               
        }
        private void newConvseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.Clear(Color.Wheat);
            pic.Image = bm;
            select = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button18.Click += CreateColorButtons;
            button17.Click += CreateColorButtons;
            button16.Click += CreateColorButtons;
            button15.Click += CreateColorButtons;
            button8.Click += CreateColorButtons;
            button20.Click += CreateColorButtons;
            button19.Click += CreateColorButtons;
            button27.Click += CreateColorButtons;
            button26.Click += CreateColorButtons;
            button25.Click += CreateColorButtons;
            button24.Click += CreateColorButtons;
            button23.Click += CreateColorButtons;
            button22.Click += CreateColorButtons;
            button21.Click += CreateColorButtons;
            button20.Click += CreateColorButtons;
            button34.Click += CreateColorButtons;
            button33.Click += CreateColorButtons;
            button32.Click += CreateColorButtons;
            button31.Click += CreateColorButtons;
            button30.Click += CreateColorButtons;
            button29.Click += CreateColorButtons;
            button28.Click += CreateColorButtons;
            button41.Click += CreateColorButtons;
            button40.Click += CreateColorButtons;
            button39.Click += CreateColorButtons;
            button38.Click += CreateColorButtons;
            button37.Click += CreateColorButtons;
            button36.Click += CreateColorButtons;
            button35.Click += CreateColorButtons;
            button43.Click += button43_Click;



        }

        private void button11_Click(object sender, EventArgs e)
        {
            select = 3;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            select = 1;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            select = 2;
        }


        private void button9_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                P.Color = colorDialog1.Color;
                btColore2.BackColor = P.Color;
                SelectedC = P.Color;


            }
        }

   


    

    }
}

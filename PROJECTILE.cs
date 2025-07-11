using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Media;
using System.Drawing.Drawing2D;

namespace PROJECTILE
{
    public partial class Form1 : Form
    {


        float C = 0;

        private float ballWidth = 12;
        private float ballHeight = 12;
        private float ballPosX = 103, ballPosXX = 103;
        private float ballPosY = 235, ballPosYY = 235;

        private int _ticks;



        float angleArrowX = 133, angleArrowY = 235;
        float Vx = 0, Vy = 0, V0x = 0, V0y = 0, V = 0, accX = 0, accY = 0, teta0 = 0, V0;
        float fx = 0, fy = 0, j, Ec = 0, Epp = 0, Em = 0, ymax = 0;
        float arrowIndexX = 103, arrowIndexY = 235;
        int Ry = 0, Rx = 0, P = 0, poidsIndex;
     

        float[] x; float[] y;
        float[] time;
        float[] tabEc; float[] tabEpp; float[] tabEm;
        float[] tabVx; float[] tabVy;
        float[] tabax ; float[] tabay;
      

       
        int i=0, indexpicture = 0, imageIndex = 0, startIndex=0;

        private Graphics ballGraphics, arrowGraphics;

        float PosY;


        public Form1()
        {
            InitializeComponent();
           
          
        }

        public void graph()
        {



            Bitmap ball1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            ballGraphics = Graphics.FromImage(ball1);
            ballGraphics.FillEllipse(Brushes.Black, ballPosX, ballPosY, ballWidth, ballHeight);
            ballGraphics.DrawEllipse(Pens.Black, ballPosX, ballPosY , ballWidth, ballHeight);
            pictureBox1.Image = ball1;
            ballGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;


           

        }

        private void MoveBall(object sender, EventArgs e)
        {
            graph();





          
            ballPosX = ((V0x * i + float.Parse(x0Box.Text)) + 103);

            //ballPosY = (((0.5f * float.Parse(gravityBox.Text) * i * i - V0y * i * 3 + float.Parse(y0Box.Text)) + 235) / 3);
            //PosY = (((-0.5f * float.Parse(gravityBox.Text) * i * i + V0y * i + float.Parse(y0Box.Text)) + 1200));
            ballPosY = (((0.5f * float.Parse(gravityBox.Text) * i * i - V0y * i * 3 + float.Parse(y0Box.Text)) + 235) / 2);
            PosY = (((-0.5f * float.Parse(gravityBox.Text) * i * i + V0y * i + float.Parse(y0Box.Text))+1000));
         
            //PosY = (((-0.5f * float.Parse(gravityBox.Text) * i * i + V0y * i + float.Parse(y0Box.Text)) ));
           





            Ylabel.Text = (PosY).ToString();
            Xlabel.Text = (ballPosX).ToString();
          

            counterLabel.Text = i.ToString();

            i++;

            if (PosY<0)
            {


                SoundPlayer splayer = new SoundPlayer("Boom.wav");
                splayer.Stop();

                SoundPlayer ssplayer = new SoundPlayer("touch-earth.wav");
                ssplayer.Play();

                
            }

            if (PosY<0)
            {
                timer1.Stop();
                GraphButton.Enabled = true;
                timer2.Start();
               
               
                Vx = V0x;
                VxLabel.Text = Vx.ToString();
                Vy = -1 * float.Parse(gravityBox.Text) * i + V0y;
                VyLabel.Text = Vy.ToString();

                accX = 0;
                accY = -float.Parse(gravityBox.Text);
                axLabel.Text = accX.ToString();
                ayLabel.Text = accY.ToString();

                V = (float)Math.Sqrt(Vx * Vx + Vy * Vy);
                Ec = 0.5f * float.Parse(massBox.Text) * V * V;
                Epp = float.Parse(massBox.Text) * float.Parse(gravityBox.Text) * float.Parse(Ylabel.Text);
                Em = Ec + Epp;
                EcL.Text = Ec.ToString();
                EppL.Text = Epp.ToString();
                EmL.Text = Em.ToString();
                stop.Enabled = false;



                pictureBox1.Hide();
                pictureBox4.Show();
               
                pictureBox4.Image = Properties.Resources.gif_explosion;
                referenceLbl.Hide();
                
            }

          
              


       

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            poidsIndex = 0;
            GraphButton.Enabled = false;
            locationbtn.Enabled = false;

            angleTrackbar.Enabled = false;
           if(initialVelocityBox.Text!="") initialVelocityBox.Enabled = false;
           if(massBox.Text!="") massBox.Enabled = false;

            if ((massBox.Text == "") || (initialVelocityBox.Text == ""))
            {
                SystemSounds.Beep.Play();
                if (this.comboBox1.SelectedItem == "English") MessageBox.Show("First complete the initial conditions to unlock the movement!");

                else MessageBox.Show("Compléter d'abord les conditions initiales pour debarrer le mouvement!");
            }

            else
                if (Rx != 1 || Ry != 2)
                {
                    SystemSounds.Beep.Play();
                    if (this.comboBox1.SelectedItem == "Frensh") MessageBox.Show("Fixer d'abord les deux positions x et y");

                    else MessageBox.Show("First fix the two positions x and y");

                }

            else
        
            {
                timer1.Start();
                stop.Enabled = true;
                btnStart.Enabled = false;



                teta0 = float.Parse(teta0Box.Text);
                 V0x = float.Parse(initialVelocityBox.Text) * (float)Math.Cos(teta0 * Math.PI / 180);
                 V0y = float.Parse(initialVelocityBox.Text) * (float)Math.Sin(teta0 * Math.PI / 180);
                V0=(float)Math.Sqrt(V0x+V0y);




                graph();
                Bitmap ball1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                ballGraphics = Graphics.FromImage(ball1);
                ballGraphics.FillEllipse(Brushes.Black, ballPosX, ballPosY, ballWidth, ballHeight);
                ballGraphics.DrawEllipse(Pens.Black, ballPosX, ballPosY, ballWidth, ballHeight);
                pictureBox1.Image = ball1;
                ballGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;






                if (startIndex == 0)
                {
                    SoundPlayer splayer = new SoundPlayer("Boom.wav");
                    splayer.Play();
                }
                startIndex = 1;
            }


        }

        private void stop_Click(object sender, EventArgs e)
        {
            C = 1;
            pictureBox1.Refresh();

            x = new float[20];
            y = new float[20];
            tabVx = new float[20];
            tabVy = new float[20];
            tabax = new float[20];
            tabay = new float[20];
            tabEm = new float[20];
            tabEc = new float[20];
            tabEpp = new float[20];
            time = new float[20];

            //x=new float[int.Parse(counterLabel.Text)];
            //y = new float[int.Parse(counterLabel.Text)];
            //tabVx = new float[int.Parse(counterLabel.Text)];
            //tabVy= new float[int.Parse(counterLabel.Text)];
            //tabax = new float[int.Parse(counterLabel.Text)];
            //tabay = new float[int.Parse(counterLabel.Text)];
            //tabEm= new float[int.Parse(counterLabel.Text)];
            //tabEc= new float[int.Parse(counterLabel.Text)];
            //tabEpp= new float[int.Parse(counterLabel.Text)];
            //time =  new float[int.Parse(counterLabel.Text)];


            SoundPlayer splayer = new SoundPlayer("Boom.wav");
            splayer.Stop();
            
            
            
            
            poidsIndex = 1;
            GraphButton.Enabled = true;
            
            timer1.Stop();
            btnStart.Enabled = true;
            stop.Enabled = false;

            Vx =  V0x;
            VxLabel.Text = Vx.ToString();
            Vy = -1*float.Parse(gravityBox.Text)*i+ V0y;
            VyLabel.Text = Vy.ToString();

            accX = 0;
             accY =  - float.Parse(gravityBox.Text);
            axLabel.Text = accX.ToString();
            ayLabel.Text = accY.ToString();

            V = (float)Math.Sqrt(Vx * Vx + Vy * Vy);
            Ec = 0.5f * float.Parse(massBox.Text) * V * V;
            Epp = float.Parse(massBox.Text) * float.Parse(gravityBox.Text) * float.Parse(Ylabel.Text);
            Em = Ec + Epp;
            EcL.Text = Ec.ToString();
            EppL.Text = Epp.ToString();
            EmL.Text = Em.ToString();

         


           
            this.label3.Location = new Point((int)ballPosX+290,(int) ballPosY+180);
            label3.Visible = true;

        



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedItem = "Frensh";


            //chart6.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            //chart6.ChartAreas["ChartArea1"].AxisX.Maximum = 60;
            //chart6.ChartAreas["ChartArea1"].AxisY.Minimum = -5;
            //chart6.ChartAreas["ChartArea1"].AxisY.Maximum = 5;
            //chart6.ChartAreas["ChartArea1"].AxisY.Interval = 1;//mnchen el carro tt2assam 
            //chart6.ChartAreas["ChartArea1"].AxisX.Interval = 2;

            //chart6.ChartAreas["ChartArea1"].AxisY.MinorGrid.LineDashStyle = ChartDashStyle.Dash;//b3te2d style
            //chart6.ChartAreas["ChartArea1"].AxisY.MinorGrid.Interval = chart1.ChartAreas["ChartArea1"].AxisY.MinorGrid.Interval / 2;//fina nchil Minorgrid iza bdna
            //chart6.ChartAreas["ChartArea1"].AxisY.MinorGrid.LineColor = Color.LightPink;
           
            //chart6.Legends.Clear();


            //if (x0Box.Text != "") arrowIndexX -= float.Parse(x0Box.Text);
            //pictureBox1.Refresh();
            //arrowIndexX = 103;


        }

      
       

        private void btnReset_Click(object sender, EventArgs e)
        {

            //for (int k = 0; k < int.Parse(counterLabel.Text); k++)
            //{
            //    x[k] = 0; y[k] = 0; tabVx[k] = 0; tabVy[k] = 0; tabax[k] = 0; tabay[k] = 0; tabEc[k] = 0; tabEm[k] = 0; tabEpp[k] = 0;
            //}

            P = 0;
            referenceLbl.Show();
            pictureBox4.Hide();
            pictureBox1.Show();
            startIndex = 0;


            angleTrackbar.Enabled = true;
            initialVelocityBox.Enabled = true;
            massBox.Enabled = true;

                foreach (var grpBox in this.Controls.OfType<GroupBox>())
                {
                  
                    massBox.Text = string.Empty;
                    initialVelocityBox.Text = string.Empty;
                }



            foreach (var item in this.Controls)
            {
                

                if (item.GetType().Equals(typeof(Label)))
                {
                    counterLabel.Text = "0";
                    axLabel.Text = string.Empty;
                    ayLabel.Text = string.Empty;
                    VxLabel.Text = string.Empty;
                    VyLabel.Text = string.Empty;
                    EcL.Text = string.Empty;
                    EppL.Text = string.Empty;
                    EmL.Text = string.Empty;


                    x0Trackbar.Enabled = true;
                    x0Trackbar.Value = 0;
                    x0Box.Text = "0";
                    btnFixerX.Enabled = true;
                    y0Box.Text = "0";
                    y0Trackbar.Value = 0;

                  


                    locationbtn.Enabled = true;
                    GraphButton.Enabled = false;

                   
                    ballPosX = 103;
                    ballPosY = 235;

                    Bitmap ball1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    ballGraphics = Graphics.FromImage(ball1);
                    ballGraphics.FillEllipse(Brushes.Black, ballPosX, ballPosY, ballWidth, ballHeight);
                    ballGraphics.DrawEllipse(Pens.Black, ballPosX, ballPosY, ballWidth, ballHeight);
                    pictureBox1.Image = ball1;
                    ballGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    angleTrackbar.Value = 0;
                    teta0Box.Text = "0";


                    label3.Hide();
                    i = 0;
                    timer1.Stop();
                    btnStart.Enabled = true;
                    stop.Enabled = false;
                   
                    Ylabel.Text = "";
                    Xlabel.Text = "";
                    ymax = 0;
                }

                arrowIndexX=103;arrowIndexY=235;
                angleArrowX=133;angleArrowY=235;
                pictureBox1.Refresh();



            }
        }

       

        private void changebtn_Click(object sender, EventArgs e)
        {
            
            






            if (imageIndex == 0)
            {
                pictureBox1.BackgroundImage = Properties.Resources.Mars_1_;
                imageIndex = 1;
                if (this.comboBox1.SelectedItem == "Frensh") PlaneteLabel.Text = "Sur Mars";
                else PlaneteLabel.Text = "On Mars";
         
                gravityBox.Text = "3.721";
                pictureBox3.Image = Properties.Resources.outMars;
            }
            else
                if (imageIndex == 1)
                {
                    pictureBox1.BackgroundImage = Properties.Resources.lune_1_;
                    imageIndex = 2;
                    if (this.comboBox1.SelectedItem == "Frensh") PlaneteLabel.Text = "Sur la Lune";
                    else PlaneteLabel.Text = "On the Moon";
         
                    gravityBox.Text = "1.62";

                    pictureBox3.Image = Properties.Resources.outLLune;
                }
                else

                    if (imageIndex == 2)
                    {
                        pictureBox1.BackgroundImage = Properties.Resources.Mercure_1_;
                        imageIndex = 3;
                        if (this.comboBox1.SelectedItem == "Frensh") PlaneteLabel.Text = "Sur Mercure";
                        else PlaneteLabel.Text = "On Mercury";
                        gravityBox.Text = "3.7";
                        pictureBox3.Image = Properties.Resources.outMercure;

                    }
                    else
                        if (imageIndex == 3)
                        {
                            pictureBox1.BackgroundImage = Properties.Resources.Jupiter__1_;
                            imageIndex = 4;
                            PlaneteLabel.Text = "Sur Jupiter";
                            gravityBox.Text = "24.79";
                            pictureBox3.Image = Properties.Resources.outJupiter;
                        }
                        else
                            if (imageIndex == 4)
                            {
                                pictureBox1.BackgroundImage = Properties.Resources.Venus_1_;
                                imageIndex = 5;
                                if (this.comboBox1.SelectedItem == "Frensh") PlaneteLabel.Text = "Sur Vénus";
                                else PlaneteLabel.Text = "On Venus"; 
                                gravityBox.Text = "8.87";
                                pictureBox3.Image = Properties.Resources.outVenus;
                            }
                            else
                                if (imageIndex == 5)
                                {
                                    pictureBox1.BackgroundImage = Properties.Resources.uranus_1_;
                                    imageIndex = 6;
                                    PlaneteLabel.Text = "Sur Uranus";
                                    gravityBox.Text = "8.87";
                                    pictureBox3.Image = Properties.Resources.outUranus;
                                }
                                else
                                    if (imageIndex == 6)
                                    {
                                        pictureBox1.BackgroundImage = Properties.Resources.Saturne_1_;
                                        imageIndex = 7;
                                        if (this.comboBox1.SelectedItem == "Frensh") PlaneteLabel.Text = "Sur Saturne";
                                        else PlaneteLabel.Text = "On Saturn"; 
                                        gravityBox.Text = "10.44";
                                        pictureBox3.Image = Properties.Resources.outSaturn;

                                    }
                                    else
                                        if (imageIndex == 7)
                                        {
                                            pictureBox1.BackgroundImage = Properties.Resources.Neptune_1_;
                                            imageIndex = 8;
                                            PlaneteLabel.Text = "Sur Neptune";
                                            gravityBox.Text = "11.15";
                                            pictureBox3.Image = Properties.Resources.outNeptune;
                                        }
                                        else

                                            if (imageIndex == 8)
                                            {
                                                pictureBox1.BackgroundImage = Properties.Resources.terre;
                                                imageIndex = 0;
                                                if (this.comboBox1.SelectedItem == "Frensh") PlaneteLabel.Text = "Sur Terre";
                                                else PlaneteLabel.Text = "On Earth"; 
                                                gravityBox.Text = "9.81";
                                                pictureBox3.Image = Properties.Resources.outTerre;
                                            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (counterLabel.Text != "0" && C==1 )
            {
                using (Pen p = new Pen(Brushes.DarkGray, 4f))
                {

                    p.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;


                    e.Graphics.DrawLine(p, ballPosX-20, ballPosY-10, ballPosX-20, ballPosY + 30);

                }
               
                
            }





            using (Pen p = new Pen(Brushes.Black, 3f))
            {

                p.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;


                e.Graphics.DrawLine(p, arrowIndexX, arrowIndexY, ballPosXX+300, arrowIndexY );

            }
            using (Pen p = new Pen(Brushes.Black, 3f))
            {

                p.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;


                e.Graphics.DrawLine(p, arrowIndexX, arrowIndexY, arrowIndexX, ballPosYY -200);

            }


            Graphics reference = this.pictureBox1.CreateGraphics();
            Pen blackpen = new Pen(Color.Black, 2);
            blackpen.DashStyle = DashStyle.Dash;
            PointF pt1 = new PointF(arrowIndexX-200,arrowIndexY);
            PointF pt2 = new PointF(ballPosXX+3000,arrowIndexY);
            e.Graphics.DrawLine(blackpen, pt1, pt2);



            using (Pen p = new Pen(Brushes.Red, 4f))
            {

                p.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;


                e.Graphics.DrawLine(p, 103, 235, angleArrowX,angleArrowY);

               

            }







        }

        private void circular_button1_Click(object sender, EventArgs e)
        {

            pictureBox1.Hide(); pictureBox2.Hide(); pictureBox3.Hide(); circular_pictureBox1.Hide();  panel1.Hide(); comboBox1.Hide();
           gravityBox.Hide(); initialVelocityBox.Hide(); massBox.Hide(); angleTrackbar.Hide(); label1.Hide(); label10.Hide(); label11.Hide(); label12.Hide(); label13.Hide();
             InformationLbl.Hide();label3.Hide(); label4.Hide(); label5.Hide(); label6.Hide(); label7.Hide(); label8.Hide(); label9.Hide();
            EcLab.Hide(); PlaneteLabel.Hide(); V0Labell.Hide(); VyLab.Hide(); Vxlab.Hide(); VyLabel.Hide(); EppLab.Hide(); EppL.Hide(); EcL.Hide(); Emlab.Hide(); EmL.Hide(); btnReset.Hide(); btnStart.Hide(); stop.Hide(); locationbtn.Hide();
            equationLabel.Hide(); label17.Hide(); label16.Hide(); groupBox1.Hide(); axlab.Hide(); axLabel.Hide(); aylab.Hide(); ayLabel.Hide(); chronPictureBox.Hide(); xlab.Hide(); Xlabel.Hide(); ylab.Hide(); Ylabel.Hide(); VxLabel.Hide(); counterLabel.Hide(); GraphButton.Hide(); returnbtn.Visible = true;
            groupBox2.Hide(); circular_pictureBox2.Hide(); circular_pictureBox3.Hide(); circular_pictureBox4.Hide(); circular_pictureBox5.Hide(); circular_pictureBox6.Hide(); circular_pictureBox7.Hide(); circular_pictureBox8.Hide(); circular_pictureBox9.Hide(); btnFixerX.Hide(); btnFixerY.Hide();
            referenceLbl.Hide();
            if (this.comboBox1.SelectedItem == "Frensh")
            { returnbtn.Text = "Revenir";
            LblGraphPos.Text="Graphe représentant la variation de position de l'obus suivant x et y\nen fonction de temps ";
            LblGraphVitesse.Text = "Graphe représentant la variation de vitesse de l'obus suivant x et y\nen fonction de temps";
            LblGraphAcc.Text = "Graphe représentant la variation de l'accéleration de l'obus suivant x et y\nen fonction de temps";
            LblGraphEnergie.Text = "Graphe représentant la variation de l'énergie cinétique, potentiel de pesenteur,\net mécanique de l'obus en fonction de temps";
            
            }
            else
            { returnbtn.Text = "Return"; 
              LblGraphPos.Text = "Graph representing the variation in position of the shell along x and y\ndepending on time";
              LblGraphVitesse.Text = "Graph representing the variation in velocity of the shell along x and y\ndepending on time";
              LblGraphAcc.Text = "Graph representing the variation of the acceleration of the shell along x and y\ndepending on time";
              LblGraphEnergie.Text = "Graph representing the variation of kinetic energy, gravitational potential,\nand mechanics of the shell as a function of time";
            
            }

            chart1.Location = new Point(20, 20); chart1.Visible = true;
            chart2.Location = new Point(470, 20); chart2.Visible = true;
            chart3.Location = new Point(930, 20); chart3.Visible = true;
            chart4.Location = new Point(470, 370); chart4.Visible = true;
            LblGraphPos.Location = new Point(20, 320); LblGraphPos.Visible = true;
            LblGraphVitesse.Location = new Point(470, 320); LblGraphVitesse.Visible = true;
            LblGraphAcc.Location = new Point(930, 320); LblGraphAcc.Visible = true;
            LblGraphEnergie.Location = new Point(470, 670); LblGraphEnergie.Visible = true;


            for (int test = 0; test <20; test++)
            {
                    x[test] = (V0x * test + float.Parse(x0Box.Text));
                  
                    y[test] = -0.5f * float.Parse(gravityBox.Text) * test * test + V0y * test + float.Parse(y0Box.Text);
                    time[test] = test;
                

            }
            chart1.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            chart1.ChartAreas["ChartArea1"].AxisX.Maximum = 20;
            chart1.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
            chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 50;

            chart1.Series["X"].XValueMember ="time";
            chart1.Series["X"].YValueMembers =  "X";
            chart1.Series["X"].Points.DataBindXY(time, x);

            chart1.Series["Y"].XValueMember ="time";
            chart1.Series["Y"].YValueMembers = "Y";
            chart1.Series["Y"].Points.DataBindXY(time, y);



            for (int test = 0; test < 20; test++)
            {
               
                tabVx[test] = V0x;

             
                tabVy[test] = -(float.Parse(gravityBox.Text))* test + V0y;
            }
            chart2.Series["Vy"].XValueMember = "time";
            chart2.Series["Vy"].YValueMembers = "Vy";
            chart2.Series["Vy"].Points.DataBindXY(time, tabVy);

            chart2.Series["Vx"].XValueMember = "time";
            chart2.Series["Vx"].YValueMembers = "Vx";
            chart2.Series["Vx"].Points.DataBindXY(time, tabVx);



            for (int test = 0; test < 20; test++)
            {
              
                tabax[test] = 0;
                tabay[test] = - float.Parse(gravityBox.Text);
            }
            chart3.Series["ay"].XValueMember = "time";
            chart3.Series["ay"].YValueMembers = "ay";
            chart3.Series["ay"].Points.DataBindXY(time, tabay);

            chart3.Series["ax"].XValueMember = "time";
            chart3.Series["ax"].YValueMembers = "ax";
            chart3.Series["ax"].Points.DataBindXY(time, tabax);




            for (int test = 0; test < 20; test++)
            {
                V = (float)Math.Sqrt(tabVx[test] * tabVx[test] + tabVy[test] * tabVy[test]);
                tabEc[test] = (0.5f) * float.Parse(massBox.Text) * V * V;
                tabEpp[test] = float.Parse(massBox.Text) * float.Parse(gravityBox.Text) * (float.Parse(y0Box.Text)+ y[test]);
                tabEm[test] = Ec + Epp;
            }


            chart4.Series["Ec"].XValueMember = "time";
            chart4.Series["Ec"].YValueMembers = "Ec";
            chart4.Series["Ec"].Points.DataBindXY(time, tabEc);

            chart4.Series["Epp"].XValueMember = "time";
            chart4.Series["Epp"].YValueMembers = "Epp";
            chart4.Series["Epp"].Points.DataBindXY(time, tabEpp);

            chart4.Series["Em"].XValueMember = "time";
            chart4.Series["Em"].YValueMembers = "Em";
            chart4.Series["Em"].Points.DataBindXY(time, tabEm);
             


            
            
        
       
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            //if (this.comboBox1.SelectedItem == "English")
            //{
            //    foreach (Control c in this.Controls)
            //    {
            //        int size = (int)c.Font.Size;
            //        c.Font = new Font("Lucida Bright",size);
            //    }

            //    PlaneteLabel.Font = new Font("Sitka Small",14);

               

            //}
            //else
            //{
            //    foreach (Control c in this.Controls)
            //    {
            //        int size = (int)c.Font.Size;
            //        c.Font = new Font("Lucida Calligraphy", size);
            //    }
            //}
















            if (this.comboBox1.SelectedItem == "English")
            {
                PlaneteLabel.Text = "On Earth";
                mass.Text = "Mass";
               
                V0Labell.Text = "Initial Velocity";
                gravite.Text = "Gravity";
                btnStart.TextAlign = ContentAlignment.MiddleCenter;
                btnStart.Text = "     Start"; btnReset.Text = "   Reset";
              
                stop.Text = "   Stop"; locationbtn.Text = "    Location";
                xlab.Text = "Position along the\nx-axis";
                ylab.Text = "Position along the\ny-axis";
                VyLab.Text = "Velocity along the\nx-axis";
                Vxlab.Text = "Velocity along the\ny-axis";
                axlab.Text = "Acceleration along\nthe x-axis";
                aylab.Text = "Acceleration along\nthe y-axis";
                EcLab.Text = "kinetic energy";
                EppLab.Text = "gravity potential\nenergy";
                Emlab.Text = "mechanical energy";
                
                label3.Text = "weight";
                GraphButton.Text = "  Graph";
                InformationLbl.Text = "More\nInformations";
                teta.Text = "launch\nangle:";
                equationLabel.Text = "Hourly equation\nof motion:";
                groupBox1.Text = "Initial conditions";
                groupBox2.Text = "Results";
                btnFixerX.Text = "Fix X";
                btnFixerY.Text = "Fix Y";
                

            }
            else
            {
                PlaneteLabel.Text = "Sur Terre";
                mass.Text = "Masse";
               
                V0Labell.Text = "Vitesse Initial";
                gravite.Text = "Gravité";
                btnStart.TextAlign = ContentAlignment.MiddleRight;
                btnStart.Text = "Commencer"; btnReset.Text = "    Recommencer";
                stop.Text = "   Arrêter"; locationbtn.Text = "    Milieu";
                xlab.Text = "Position suivant\nl'axe des x";
                ylab.Text = "Position suivant\nl'axe des y";
                VyLab.Text = "Vitesse suivant\nl'axe des y";
                Vxlab.Text = "Vitesse suivant\nl'axe des x"; ;
                axlab.Text = "Acceleration  suivant\nl'axe des x"; ;
                aylab.Text = "Acceleration suivant\nl'axe des y";
                EcLab.Text = "Énergie cinétique";
                EppLab.Text = "Énergie Potentiel de\nPesenteur";
                Emlab.Text ="Énergie mécanique" ;
           
                label3.Text = "Poids";
                GraphButton.Text = "  Graphe";
                InformationLbl.Text = "Plus\nd'informations";
                teta.Text = "angle de\nlancement:";
                equationLabel.Text = "Equations Horaires du\nMouvement:";
                btnStart.Text = "Commencer";
                stop.Text = "  Arrêter";
                groupBox1.Text = "Conditions Initiales";
                groupBox2.Text = "Résultats";
                btnFixerY.Text = "Fixer Y";
                btnFixerX.Text = "Fixer X";


            }

           
        }

        private void label13_Click(object sender, EventArgs e)
        {
        }

        private void angle_Scroll(object sender, EventArgs e)
        {
            teta0Box.Text = angleTrackbar.Value.ToString();
            angleArrowX = ((193-(angleTrackbar.Value/2))/1.45f);
        
            angleArrowY = (2 * 235 - (float)Math.Sqrt(4 * 235 * 235 - 4 * (angleArrowX * angleArrowX - 2 * 103 * angleArrowX + 103 * 103 - 30 * 30 + 235 * 235))) / 2;
            pictureBox1.Refresh();
        }

        private void PlaneteLabel_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void xlab_Click(object sender, EventArgs e)
        {

        }

        private void initialVelocityBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {
           
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void InformationLbl_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedItem == "Frensh")
            {
                this.Hide();
                var Details = new Details();
                Details.ShowDialog();
                Details = null;
                this.Show();
            }

            else
                if (this.comboBox1.SelectedItem == "English")
                {
                    this.Hide();
                    var More_Details = new More_Details();
                    More_Details.ShowDialog();
                    More_Details = null;
                    this.Show();
                }
        }

        private void returnbtn_Click(object sender, EventArgs e)
        {
            pictureBox1.Show(); pictureBox2.Show(); pictureBox3.Show(); circular_pictureBox1.Show(); panel1.Show(); comboBox1.Show();
            gravityBox.Show(); initialVelocityBox.Show(); massBox.Show(); angleTrackbar.Show(); label1.Show(); label10.Show(); label11.Show(); label12.Show(); label13.Show();
          InformationLbl.Show(); label3.Show(); label4.Show(); label5.Show(); label6.Show(); label7.Show(); label8.Show(); label9.Show();
            EcLab.Show(); PlaneteLabel.Show(); V0Labell.Show(); VyLab.Show(); Vxlab.Show(); VyLabel.Show(); EppLab.Show(); EppL.Show(); EcL.Show(); Emlab.Show(); EmL.Show(); btnReset.Show(); btnStart.Show(); stop.Show(); locationbtn.Show();
            equationLabel.Show(); label17.Show(); label16.Show(); groupBox1.Show(); axlab.Show(); axLabel.Show(); aylab.Show(); ayLabel.Show(); chronPictureBox.Show(); xlab.Show(); Xlabel.Show(); ylab.Show(); Ylabel.Show(); VxLabel.Show(); counterLabel.Show(); GraphButton.Show(); returnbtn.Visible = false;
            groupBox2.Show(); circular_pictureBox2.Show(); circular_pictureBox3.Show(); circular_pictureBox4.Show(); circular_pictureBox5.Show(); circular_pictureBox6.Show(); circular_pictureBox7.Show(); circular_pictureBox8.Show(); circular_pictureBox9.Show();
            LblGraphAcc.Hide(); LblGraphEnergie.Hide(); LblGraphPos.Hide(); LblGraphVitesse.Hide(); btnFixerX.Show(); btnFixerY.Show(); referenceLbl.Show();
            chart1.Visible = false;
            chart2.Visible = false;
            chart3.Visible = false;
            chart4.Visible = false;

        }

        private void circular_pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnFixerX_Click(object sender, EventArgs e)
        {
            Rx = 1;
            arrowIndexX -= 2 * float.Parse(x0Box.Text); if (P == 0) { y0Trackbar.Enabled = true; x0Trackbar.Enabled = false; btnFixerY.Enabled = true; btnFixerX.Enabled =false; }
           P = 1;


        }

        private void btnFixerY_Click(object sender, EventArgs e)
        {
            Ry = 2;
            y0Trackbar.Enabled = false; arrowIndexY += 2 * float.Parse(y0Box.Text); btnFixerY.Enabled = false;

        }

        private void x0Trackbar_Scroll(object sender, EventArgs e)
        {
            x0Box.Text= x0Trackbar.Value.ToString();
            arrowIndexX -= 2*float.Parse(x0Box.Text);
            pictureBox1.Refresh();
            arrowIndexX = 103;
        }

        private void x0Box_Click(object sender, EventArgs e)
        {

        }

        private void y0Trackbar_Scroll(object sender, EventArgs e)
        {
            y0Box.Text = y0Trackbar.Value.ToString();
            if (y0Box.Text != "") arrowIndexY +=2* float.Parse(y0Box.Text);
            referenceLbl.Location = new Point(924, (int)arrowIndexY+180);
            pictureBox1.Refresh();
            arrowIndexY = 235;
          
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            _ticks++;
            if (_ticks == 62) pictureBox4.Enabled = false;
        }

        private void Ylabel_Click(object sender, EventArgs e)
        {

        }

       
    }
}

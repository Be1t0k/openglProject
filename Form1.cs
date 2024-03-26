using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tao.DevIl;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace Moshkov_Pavel_PRI_120_PrKG_KP
{
    public partial class Form1 : Form
    {
        double angle = 3, angleX = -96, angleY = 0, angleZ = -30;
        double sizeX = 1, sizeY = 1, sizeZ = 1;

        double translateX = -9, translateY = -60, translateZ = -10;

        double cameraSpeed;
        float global_time = 0;

        bool isError = false;
        //ночь
        bool night = false;
        float goods_move_i = 0.2f;


        //Флаги, отражающие использование камня и наличие света
        bool isGoodChecked = false;
        bool isGoodChecking = false;

        //Дельта перемещения объектов на столе в аптеке
        double deltaMaccharoni, deltaLays, deltaGum, deltaBottle; double deltaCase;
        //Дельта перемещения объектов по оси Z на столе в аптеке
        double deltaZMaccharoni, deltaZLays, deltaZGum, deltaZBottle;
        //Дельта перемещения объектов по оси Z на столе в аптеке
        double deltaYGum, deltaYBottle;
        //Дельта опускания весов
        public double deltaZScales = 20;
        public int level;

        //Текстуры
        uint signboardSign, lefttopsignboardSign, middlesignboardSign, maccheroniSign, laysSign, bottleSign, gumSign, errorSign;
        int imageId;
        string signboardTexture = "white_logo.png";
        string lefttopsignboardTexture = "yellow_logo.png";
        string middlesignboardTexture = "nails_logo.png";
        string maccheroniTexture = "maccheroni.png";
        string laysTexture = "lays.png";
        string bottleTexture = "bottle.png";
        string gumTexture = "gum.png";
        string errorTexture = "error.png";

        //Взрыв денег с использованием системы частиц
        private readonly Explosion explosion = new Explosion(50, 120, 26, 30, 50);

        private void button1_Click_1(object sender, EventArgs e)
        {
            deltaMaccharoni = 0;
            deltaLays = 0;
            deltaGum = 0;
            deltaBottle = 0;
            deltaCase = 1;
            deltaZMaccharoni = 0;
            deltaZMaccharoni = 0;
            deltaZLays = 0;
            deltaZGum = 0;
            deltaZBottle = 0;
            deltaZScales = 20;

            isGoodChecked = false;
            isGoodChecking = false;
            isError = false;
        }

        //Проигрывание аудио
        public WMPLib.WindowsMediaPlayer WMP = new WMPLib.WindowsMediaPlayer();

        private void Form1_Load(object sender, EventArgs e)
        {
            // инициализация openGL (glut)
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            Il.ilInit();
            Il.ilEnable(Il.IL_ORIGIN_SET);

            // цвет очистки окна
            Gl.glClearColor(255, 255, 255, 1);

            // настройка порта просмотра
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(60, (float)AnT.Width / (float)AnT.Height, 0.1, 900);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Gl.glEnable(Gl.GL_DEPTH_TEST);

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 2;
            comboBox3.SelectedIndex = 0;
            cameraSpeed = 5;
            label8.Visible = false;

            signboardSign = genImage(signboardTexture);
            lefttopsignboardSign = genImage(lefttopsignboardTexture);
            middlesignboardSign = genImage(middlesignboardTexture);
            maccheroniSign = genImage(maccheroniTexture);
            laysSign = genImage(laysTexture);
            gumSign = genImage(gumTexture);
            bottleSign = genImage(bottleTexture);
            errorSign = genImage(errorTexture);

            RenderTimer.Start();

            // Включение освещения
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);
            Gl.glEnable(Gl.GL_LIGHT1);
            Gl.glEnable(Gl.GL_COLOR_MATERIAL);
            Gl.glEnable(Gl.GL_NORMALIZE);
        }

        private void Draw()
        {

            // в зависимости от установленного режима отрисовываем сцену в черном или белом цвете
            if (comboBox3.SelectedIndex == 0)
            {
                Gl.glDisable(Gl.GL_LIGHTING);
                Gl.glClearColor(255, 255, 255, 1);
            }
            else
            {
                Gl.glEnable(Gl.GL_LIGHTING);
                Gl.glClearColor(0, 0, 0, 1);
            }

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glLoadIdentity();

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glClearColor(255, 255, 255, 1);
            Gl.glLoadIdentity();
            Gl.glPushMatrix();
            Gl.glRotated(angleX, 1, 0, 0);
            Gl.glRotated(angleY, 0, 1, 0);
            Gl.glRotated(angleZ, 0, 0, 1);
            Gl.glTranslated(translateX, translateY, translateZ);
            Gl.glScaled(sizeX, sizeY, sizeZ);

            explosion.Calculate(global_time);

            shop.drawAsphalt();
            shop.drawShop(signboardSign, lefttopsignboardSign, middlesignboardSign);
            shop.drawTable();
            shop.drawBasket();
            shop.drawScales(deltaZScales);
            shop.drawCashier(deltaZScales, deltaCase);
            drawObjectsOnTheTable();
            drawFractal(level);

            if (isGoodChecking)
            {
                isGoodChecked = true;
                isGoodChecking = false;
                Gl.glPushMatrix();
                explosion.SetNewPosition(50, 100, 20);
                explosion.SetNewPower(5);
                explosion.Boooom(global_time);
                Gl.glPopMatrix();
            }

            if (isError)
            {
                shop.drawScanGoods(errorSign);
            }

            if (deltaMaccharoni >= 5 & deltaMaccharoni < 30)
            {
                shop.drawScanGoods(maccheroniSign);
                deltaZMaccharoni = 1;

            }
            if (deltaLays >= 5 & deltaLays < 30)
            {
                shop.drawScanGoods(laysSign);
                deltaZLays = 1;
            }
            if (deltaGum >= 5 & deltaGum < 30)
            {
                shop.drawScanGoods(gumSign);
                deltaZGum = 1;
            }
            if (deltaBottle >= 5 & deltaBottle < 30)
            {
                shop.drawScanGoods(bottleSign);
                deltaZBottle = 1;
            }
            shop.drawCloset();

            Gl.glPopMatrix();
            Gl.glFlush();
            AnT.Invalidate();
        }

        private void sendMoney()
        {
            isGoodChecking = true;
        }

        //Метод для отрисовки объектов на столе
        private void drawObjectsOnTheTable()
        {
            Gl.glTranslated(-30, -130, -20);
            shop.drawMaccharoni(maccheroniSign, deltaMaccharoni, deltaZMaccharoni);
            shop.drawLays(laysSign, deltaLays, deltaZLays);
            shop.drawGum(gumSign, deltaGum, deltaZGum, deltaYGum);
            shop.drawBottle(bottleSign, deltaBottle, deltaZBottle, deltaYBottle);
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                angle = 3; angleX = -90; angleY = 0; angleZ = -30;
                sizeX = 1; sizeY = 1; sizeZ = 1;
                translateX = -100; translateY = 10; translateZ = -25;
                label7.Visible = false;
                button1.Visible = false;
                WMP.controls.stop();
            }
            if (comboBox1.SelectedIndex == 1)
            {
                translateX = -50; translateY = -70; translateZ = -40;
                angleX = -70;
                angleZ = 0;
                label7.Visible = true;
                button1.Visible = true;
                WMP.URL = @"kassa.mp3";
                WMP.controls.play();
            }
            AnT.Focus();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            AnT.Focus();
        }

        private void AnT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                translateY -= cameraSpeed;

            }
            if (e.KeyCode == Keys.S)
            {
                translateY += cameraSpeed;
            }
            if (e.KeyCode == Keys.A)
            {
                translateX += cameraSpeed;
            }
            if (e.KeyCode == Keys.D)
            {
                translateX -= cameraSpeed;

            }
            if (e.KeyCode == Keys.ControlKey)
            {
                translateZ += cameraSpeed;

            }
            if (e.KeyCode == Keys.Space)
            {
                translateZ -= cameraSpeed;
            }
            if (e.KeyCode == Keys.R)
            {
                switch (comboBox2.SelectedIndex)
                {
                    case 0:
                        angleX += angle;

                        break;
                    case 1:
                        angleY += angle;

                        break;
                    case 2:
                        angleZ += angle;

                        break;
                    default:
                        break;
                }
            }
            if (e.KeyCode == Keys.E)
            {
                switch (comboBox2.SelectedIndex)
                {
                    case 0:
                        angleX -= angle;
                        break;
                    case 1:
                        angleY -= angle;
                        break;
                    case 2:
                        angleZ -= angle;
                        break;
                    default:
                        break;
                }
            }
            if (e.KeyCode == Keys.D1 && deltaMaccharoni < 23)
            {
                if (!isError)
                {
                    if (deltaMaccharoni == 32)
                    {
                        deltaMaccharoni = 32;
                    }
                    else
                    {
                        deltaMaccharoni += 1;
                        checkObjectsOnTheTable();
                    }
                }
                
                

            }
            if (e.KeyCode == Keys.D2)
            {
                if (!isError && deltaLays < 23)
                {
                    if (deltaLays == 34)
                    {
                        deltaLays = 34;
                    }
                    else
                    {
                        deltaLays += 1;
                        checkObjectsOnTheTable();
                    }
                }
                
                
            }
            if (e.KeyCode == Keys.D3)
            {
                if (!isError && deltaGum < 23)
                {
                    if (deltaGum == 35)
                    {
                        deltaGum = 35;
                    }
                    else
                    {
                        deltaGum += 1;
                        checkObjectsOnTheTable();
                    }
                }
                
                
            }
            if (e.KeyCode == Keys.D4)
            {
                if (!isError && deltaBottle < 21)
                {
                    if (deltaBottle == 34)
                    {
                        deltaBottle = 34;
                    }
                    else
                    {
                        deltaBottle += 1;
                        checkObjectsOnTheTable();
                    }
                }
                
            }
            if (e.KeyCode == Keys.Q)
            {
                if (deltaCase == 10)
                {
                    isError = true;
                }
                else
                {
                    if (deltaMaccharoni >= 5 & deltaMaccharoni < 30 & deltaLays >= 5 & deltaLays < 30)
                    {
                        isError = true;
                        sendMoney();
                    }
                    if (deltaMaccharoni >= 5 & deltaMaccharoni < 30)
                    {
                        deltaMaccharoni = 32;
                        deltaZScales = 21;
                    }
                    if (deltaBottle >= 5 & deltaBottle < 30)
                    {
                        deltaBottle = 34;
                        deltaYBottle= 2;
                        deltaZScales = 21;
                    }
                    if (deltaGum >= 5 & deltaGum < 30)
                    {
                        deltaGum = 35;
                        deltaYGum = 7;
                        deltaZScales = 21;
                    }
                    if (deltaLays >= 5 & deltaLays < 30)
                    {
                        deltaLays = 34;
                        deltaZScales = 21;
                    }
                    if (deltaMaccharoni >= 30 & deltaLays >= 30 & deltaGum >= 30 & deltaBottle >= 30)
                    {
                        label8.Visible = true;
                    }
                }
            }

        }

        private void checkObjectsOnTheTable()
        {
            if ((deltaMaccharoni >= 5 & deltaMaccharoni < 30) || (deltaLays >= 5 & deltaLays < 30) || (deltaGum >= 5 & deltaGum < 30) || (deltaBottle >= 5 & deltaBottle < 30))
            {
                deltaZScales = 19;
            }


        }
        int tick_count = 0;
        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            global_time += (float)RenderTimer.Interval / 1000;
            Draw();

            shop.goods_move += goods_move_i;

            if (isError)
            {
                deltaCase += goods_move_i;
            }

            if (tick_count < 15)
            {
                tick_count++;
            }
            else
            {
                if (goods_move_i == 0.3f)
                    goods_move_i = -0.3f;
                else
                    goods_move_i = 0.3f;
                tick_count = 0;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            level = (int)numericUpDown1.Value;
            AnT.Focus();
        }

        Shop shop = new Shop();

        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        public void drawFractal(int level)
        {
            Gl.glPushMatrix();

            Gl.glTranslated(35, 56, 22);
            Gl.glRotated(90, 0, 1, 0);
            Gl.glRotated(90, 0, 0, 1);
            Gl.glRotated(90, 0, 1, 0);
            Gl.glScalef(1, 0.65f, 0.5f);

            Gl.glBegin(Gl.GL_LINES);
            DrawKochLine(-10, -10, 20, 0, level);
            Gl.glEnd();
            Gl.glPopMatrix();
        }

        private const double SQRT_3 = 1.7320508075688772;

        public void DrawKochLine(double x1, double y1, double x2, double y2, int level)
        {
            if (level == 0)
            {
                // прямая линия
                Gl.glBegin(Gl.GL_LINES);
                Gl.glColor3f(1, 0, 0);
                Gl.glVertex2d(x1, y1);
                Gl.glVertex2d(x2, y2);
                Gl.glEnd();
            }
            else
            {
                //делим на 4 части
                double dx = (x2 - x1) / 3.0;
                double dy = (y2 - y1) / 3.0;

                // считаем координаты пиковых точек
                double peakX = x1 + dx - dy * Math.Cos(Math.PI / 3.0);
                double peakY = y1 + dy + dx * Math.Sin(Math.PI / 3.0);

                // рисовалка 4х сегментов
                DrawKochLine(x1, y1, x1 + dx, y1 + dy, level - 1);
                DrawKochLine(x1 + dx, y1 + dy, peakX, peakY, level - 1);
                DrawKochLine(peakX, peakY, x1 + 2 * dx, y1 + 2 * dy, level - 1);
                DrawKochLine(x1 + 2 * dx, y1 + 2 * dy, x2, y2, level - 1);
            }
        }


        private void информацияОПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private uint genImage(string image)
        {
            uint sign = 0;
            Il.ilGenImages(1, out imageId);
            Il.ilBindImage(imageId);
            if (Il.ilLoadImage(image))
            {
                int width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
                int height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);
                int bitspp = Il.ilGetInteger(Il.IL_IMAGE_BITS_PER_PIXEL);
                switch (bitspp)
                {
                    case 24:
                        sign = MakeGlTexture(Gl.GL_RGB, Il.ilGetData(), width, height);
                        break;
                    case 32:
                        sign = MakeGlTexture(Gl.GL_RGBA, Il.ilGetData(), width, height);
                        break;
                }
            }
            Il.ilDeleteImages(1, ref imageId);
            return sign;
        }

        private static uint MakeGlTexture(int Format, IntPtr pixels, int w, int h)
        {
            uint texObject;
            Gl.glGenTextures(1, out texObject);
            Gl.glPixelStorei(Gl.GL_UNPACK_ALIGNMENT, 1);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texObject);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_REPLACE);
            switch (Format)
            {

                case Gl.GL_RGB:
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB, w, h, 0, Gl.GL_RGB, Gl.GL_UNSIGNED_BYTE, pixels);
                    break;

                case Gl.GL_RGBA:
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, w, h, 0, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, pixels);
                    break;

            }
            return texObject;
        }
    }
}

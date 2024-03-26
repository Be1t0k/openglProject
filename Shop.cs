using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace Moshkov_Pavel_PRI_120_PrKG_KP
{
    //Класс RGB для удобства задания цвета
    class RGB
    {
        private float R;
        private float G;
        private float B;

        public RGB(float R, float G, float B)
        {
            this.R = R;
            this.G = G;
            this.B = B;
        }

        public float getR()
        {
            return R;
        }

        public float getG()
        {
            return G;
        }

        public float getB()
        {
            return B;
        }
    }


    class Shop
    {
        float deltaColor = 0;
        public float goods_move = 0;

        private void setColor(float R, float G, float B)
        {
            RGB color = new RGB(R - deltaColor, G - deltaColor, B - deltaColor);
            Gl.glColor3f(color.getR(), color.getG(), color.getB());
        }

        //Отрисовка асфальта
        public void drawAsphalt()
        {


            Gl.glPushMatrix();
            setColor(0.22f, 0.19f, 0.22f);
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex3d(-250, 0, 0);
            Gl.glVertex3d(250, 0, 0);
            Gl.glVertex3d(250, 300, 0);
            Gl.glVertex3d(-250, 300, 0);
            Gl.glEnd();
            Gl.glPopMatrix();
        }

        //Отрисовка магазина
        public void drawShop(uint sign, uint signleft, uint signmiddle)
        {

            Gl.glPushMatrix();
            Gl.glTranslated(35, 95, 30);
            Gl.glScaled(1.4, 1, 1);
            setColor(0.7f, 0.8f, 0.7f);
            Glut.glutSolidCube(70);
            Gl.glLineWidth(6f);
            Gl.glTranslated(65, -5, -10);
            Glut.glutSolidCube(60);
            drawSignboard(sign, signleft, signmiddle);
            drawDoor();
            drawSteel();
            drawGarage();
            Gl.glPopMatrix();
        }

        //отрисовка отсканированных товаров
        public void drawScanGoods(uint goods_sign)
        {
            Gl.glPushMatrix();
            Gl.glTranslated(43, 132.5, 12 + goods_move);
            Gl.glRotated(-90, 0, 1, 0);
            Gl.glRotated(-180, 1, 0, 0);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, goods_sign);
            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(0, 5, 10);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(0, 5, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(5, 5, 0);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(5, 5, 10);
            Gl.glTexCoord2f(1, 0);
            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix();
        }

        //Отрисовка вывески
        private void drawSignboard(uint sign, uint signleft, uint signmiddle)
        {
            //rst video
            Gl.glPushMatrix();
            Gl.glTranslated(4, -27, 13);
            Gl.glRotated(-90, 0, 1, 0);
            Gl.glRotated(-180, 1, 0, 0);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, sign);
            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(0, 5, 20);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(0, 5, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(10, 5, 0);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(10, 5, 20);
            Gl.glTexCoord2f(1, 0);
            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix();

            //groceries
            Gl.glPushMatrix();
            Gl.glTranslated(-85, -27, 25);
            Gl.glRotated(-90, 0, 1, 0);
            Gl.glRotated(-180, 1, 0, 0);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, signleft);
            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(0, 5, 50);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(0, 5, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(17, 5, 0);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(17, 5, 50);
            Gl.glTexCoord2f(1, 0);
            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix();

            //nails
            Gl.glPushMatrix();
            Gl.glTranslated(-29.5, -26, 3);
            Gl.glRotated(-90, 0, 1, 0);
            Gl.glRotated(-180, 1, 0, 0);
            Gl.glScaled(1, 1, 0.9f);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, signmiddle);
            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(0, 5, 10);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(0, 5, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(20, 5, 0);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(20, 5, 10);
            Gl.glTexCoord2f(1, 0);
            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix();
        }

        public void drawDoor()
        {
            //дверь
            Gl.glPushMatrix();
            Gl.glTranslated(-20, -37, -20);
            setColor(0.16f, 0.16f, 0.16f);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(0, 5, 35);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(0, 5, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(15, 5, 0);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(15, 5, 35);
            Gl.glTexCoord2f(1, 0);
            Gl.glEnd();
            Gl.glPopMatrix();

            //2дверь
            Gl.glPushMatrix();
            Gl.glTranslated(-45, -40, -20);
            setColor(0.16f, 0.16f, 0.16f);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(0, 5, 35);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(0, 5, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(15, 5, 0);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(15, 5, 35);
            Gl.glTexCoord2f(1, 0);
            Gl.glEnd();
            Gl.glPopMatrix();
        }

        //шкафчики
        public void drawCloset()
        {
            //низ
            Gl.glPushMatrix();
            Gl.glTranslated(0, 95, 10);
            setColor(0.28f, 0.28f, 0.28f);
            Gl.glScaled(0.7, 3.6, 0.1);
            Glut.glutSolidCube(20);
            setColor(0.08f, 0.08f, 0.08f);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(20);
            Gl.glPopMatrix();

            //верх
            Gl.glPushMatrix();
            Gl.glTranslated(0, 95, 20);
            setColor(0.28f, 0.28f, 0.28f);
            Gl.glScaled(0.7, 3.6, 0.1);
            Glut.glutSolidCube(20);
            setColor(0.08f, 0.08f, 0.08f);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(20);
            Gl.glPopMatrix();
        }

        //Отрисовка балки
        public void drawSteel()
        {
            Gl.glPushMatrix();
            Gl.glRotated(90, 0, 0, 1);
            Gl.glRotated(90, 1, 0, 0);
            Gl.glTranslated(-37, 23, -85);
            setColor(0.18f, 0.18f, 0.18f);
            Gl.glScaled(1.2, 1, 1);
            Glut.glutSolidCylinder(4, 30, 7, 7);
            setColor(0.08f, 0.08f, 0.08f);
            Gl.glLineWidth(5f);
            Glut.glutWireCylinder(4, 30, 7, 7);
            Gl.glPopMatrix();
        }

        //Отрисовка гаража
        public void drawGarage()
        {
            Gl.glPushMatrix();
            Gl.glTranslated(-70, -30, 0);
            setColor(0.28f, 0.28f, 0.28f);
            Gl.glScaled(1.4, 0.3f, 2);
            Glut.glutSolidCube(20);
            setColor(0.08f, 0.08f, 0.08f);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(20);
            Gl.glPopMatrix();
        }

        //Отрисовка столешницы в аптеке
        public void drawTable()
        {
            Gl.glPushMatrix();
            Gl.glTranslated(50, 120, 5);
            Gl.glScaled(2f, 0.8f, 0.5f);
            setColor(0.8f, 0.8f, 0.8f);
            Glut.glutSolidCube(20);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(20);
            Gl.glPopMatrix();


            Gl.glPushMatrix();
            Gl.glTranslated(50, 120, 10);
            Gl.glScaled(2.3f, 0.9f, 0.1f);
            setColor(0.8f, 0.8f, 0.8f);
            Glut.glutSolidCube(20);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(20);
            Gl.glPopMatrix();
        }

        //Отрисовка корзины
        public void drawBasket()
        {
            Gl.glPushMatrix();
            Gl.glTranslated(70, 120, 16);
            Gl.glRotated(270, 0, 0, 1);
            Gl.glRotated(-10, 0, 0, 1);

            Gl.glPushMatrix();
            Gl.glTranslated(0, 0, -4);
            Gl.glScaled(1f, 1f, 0.1f);
            setColor(0.7f, 0.2f, 0.2f);
            Glut.glutSolidCube(10);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(0, 5, 1);
            Gl.glScaled(1f, 0.1f, 1f);
            setColor(0.7f, 0.2f, 0.2f);
            Glut.glutSolidCube(10);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(5, 0, 1);
            Gl.glScaled(0.1f, 1f, 1f);
            setColor(0.7f, 0.2f, 0.2f);
            Glut.glutSolidCube(10);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(0, -5, 1);
            Gl.glScaled(1f, 0.1f, 1f);
            setColor(0.7f, 0.2f, 0.2f);
            Glut.glutSolidCube(10);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(-5, 0, 1);
            Gl.glScaled(0.1f, 1f, 1f);
            setColor(0.7f, 0.2f, 0.2f);
            Glut.glutSolidCube(10);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(10);
            Gl.glPopMatrix();

            Gl.glPopMatrix();
        }

        //Отрисовка весов
        public void drawScales(double deltaZScales)
        {
            Gl.glPushMatrix();
            Gl.glTranslated(52, 120, deltaZScales);
            setColor(0.3f, 0.4f, 0.4f);
            Gl.glTranslated(-2, 0, -8);
            Gl.glScaled(1f, 0.5f, 0.1f);
            Glut.glutSolidCube(20);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(20);
            Gl.glPopMatrix();
        }

        //Отрисовка кассы
        public void drawCashier(double deltaZScales, double deltaCase)
        {
            Gl.glPushMatrix();
            Gl.glTranslated(50, 130, 20);
            Gl.glPushMatrix();
            Gl.glScaled(1.5f, 0.1f, 1f);
            Gl.glRotated(180, 0, 0, 1);
            setColor(0.3f, 0.3f, 0.3f);
            Glut.glutSolidCube(20);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(20);
            Gl.glPopMatrix();

            //экран
            Gl.glPushMatrix();
            if (deltaZScales == 19)
            {
                setColor(0.13f, 0.94f, 0.93f);
                float[] position = { 0, 0, 5, 1 };
                float[] direction = { 0, 1, 0 };
                float[] ambient = { 0.13f, 0.94f, 0.93f, 1 };

                //СВЕЧЕНИЕ
                Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, position);
                Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPOT_DIRECTION, direction);
                Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, ambient);
            }
            else
            {
                setColor(0.33f, 0.64f, 0.73f);
                float[] position = { 0, 0, 0, 1 };
                float[] direction = { 0, 1, 0 };
                float[] ambient = { 0, 0, 0, 1 };

                //СВЕЧЕНИЕ
                Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, position);
                Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPOT_DIRECTION, direction);
                Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, ambient);
            }
            Gl.glTranslated(15, -2, -9);
            Gl.glRotated(180, 0, 0, 1);
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex3d(28, 0, 0);
            Gl.glVertex3d(28, 0, 17);
            Gl.glVertex3d(2, 0, 17);
            Gl.glVertex3d(2, 0, 0);
            Gl.glEnd();


            Gl.glPushMatrix();
            Gl.glTranslated(15, 0 + deltaCase, -19.9 + deltaZScales);
            Gl.glPushMatrix();
            Gl.glScaled(0.9f, 1f, 0.1f);
            setColor(0.3f, 0.3f, 0.3f);
            Glut.glutSolidCube(20);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(20);
            Gl.glPopMatrix();

            Gl.glPopMatrix();
        }

        //Отрисовка макарон
        public void drawMaccharoni(uint texture, double deltaX, double deltaZ)
        {
            Gl.glPushMatrix();
            Gl.glTranslated(20 + deltaX, 120, 12 + deltaZ);
            Gl.glRotated(90, 0, 0, 1);

            Gl.glPushMatrix();
            Gl.glTranslated(-1, 4, 0.1);
            Gl.glScaled(0.2f, 0.3f, 0.1f);
            setColor(1, 1, 1);
            Glut.glutSolidCube(13);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(13);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(0.4, 6, -4.1);
            Gl.glScaled(0.4f, 0.6f, 1f);
            Gl.glRotated(90, 1, 0, 0);
            Gl.glRotated(-90, 0, 1, 0);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture);
            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(0, 5, 7);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(0, 5, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(7, 5, 0);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(7, 5, 7);
            Gl.glTexCoord2f(1, 0);
            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix();

            Gl.glPopMatrix();
        }

        //Отрисовка чипсов
        public void drawLays(uint texture, double deltaX, double deltaZ)
        {
            Gl.glPushMatrix();
            Gl.glTranslated(40 + deltaX, 125, 12 + deltaZ);
            Gl.glRotated(90, 0, 0, 1);

            Gl.glPushMatrix();
            Gl.glTranslated(-1, 4, 0.1);
            Gl.glScaled(0.2f, 0.3f, 0.1f);
            setColor(1, 1, 0);
            Glut.glutSolidCube(13);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(13);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(0.4, 6, -4.1);
            Gl.glScaled(0.4f, 0.6f, 1f);
            Gl.glRotated(90, 1, 0, 0);
            Gl.glRotated(-90, 0, 1, 0);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture);
            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(0, 5, 7);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(0, 5, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(7, 5, 0);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(7, 5, 7);
            Gl.glTexCoord2f(1, 0);
            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix();

            Gl.glPopMatrix();
        }

        //Отрисовка жвачки
        public void drawGum(uint texture, double deltaX, double deltaZ, double deltaY)
        {
            Gl.glPushMatrix();
            Gl.glTranslated(40 + deltaX, 115 + deltaY, 12 + deltaZ);
            Gl.glPushMatrix();
            Gl.glTranslated(-5, 0, 0.1);
            Gl.glScaled(0.2f, 0.3f, 0.1f);
            setColor(0.8f, 0, 0);
            Glut.glutSolidCube(10);
            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(5f);
            Glut.glutWireCube(10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(-4, 1.5, -4.2);
            Gl.glScaled(0.4f, 0.6f, 1f);
            Gl.glRotated(90, 1, 0, 0);
            Gl.glRotated(-90, 0, 1, 0);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture);
            Gl.glPushMatrix();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(0, 5, 5);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(0, 5, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(5, 5, 0);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(5, 5, 5);
            Gl.glTexCoord2f(1, 0);
            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix();

            Gl.glPopMatrix();
        }

        //отрисовка бутылки
        public void drawBottle(uint texture, double deltaX, double deltaZ, double deltaY)
        {
            Gl.glPushMatrix();
            Gl.glTranslated(39 + deltaX, 115 + deltaY, 10.5 + deltaZ);
            Gl.glScaled(0.3f, 0.3f, 0.3f);
            Gl.glPushMatrix();
            Gl.glTranslated(-5, 0, 0.1);
            setColor(0.1f, 0, 0.4f);
            Glut.glutSolidCylinder(5, 20, 5, 10);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(-5, 0, 0.1);
            setColor(0.1f, 0.2f, 0.4f);
            Glut.glutSolidCylinder(2.5f, 30, 5, 10);
            Gl.glPopMatrix();

            Gl.glPopMatrix();
        }

    }
}


using System;
using System.Collections.Generic;
using System.Text;
using CsGL.OpenGL;
using System.Drawing;
using System.IO;
using System.Collections;

namespace CsOpenGL
{
    class csglViewer:OpenGLControl
    {
        public int flag = 0;
        //����������
        public  Point3d cube_center = new Point3d();
        /// <summary>
        /// 
        /// </summary>
        public csglViewer()
        {

        }
        /// <summary>
        /// �����ع��ĺ���
        /// </summary>
        //protected override void OnCreateControl()
        //{
        //    base.OnCreateControl();

        //    GL.glClearDepth(1.0f);
        //    GL.glDepthFunc(GL.GL_LEQUAL);
        //    GL.glEnable(GL.GL_DEPTH_TEST);
        //}

        //��ʼ��OpenGL���Ʋ��� 
        protected override void InitGLContext()
        {
            GL.glShadeModel(GL.GL_SMOOTH); // ����Smooth Shading

            GL.glClearColor(0.0f, 0.0f, 0.0f, 0.5f);// ��ɫ����
            GL.glClearDepth(1.0f);   // ������Ȼ�����
            GL.glEnable(GL.GL_DEPTH_TEST);// ������Ȳ���
            GL.glDepthFunc(GL.GL_LEQUAL);//������Ȳ��Է�ʽ
            GL.glHint(GL.GL_PERSPECTIVE_CORRECTION_HINT, GL.GL_NICEST);// ��ȷ��͸��ͶӰ���㷽ʽ
            //GL.glClearColor(0.0f, 0.0f, 0.0f, 0.5f); ; 
            //GL.glMatrixMode(GL.GL_PROJECTION); 
            //GL.glLoadIdentity(); 
            //GL.gluOrtho2D(0.0, Size.Width, 0.0, Size.Height);
        }

        //���ڴ�С�ı�ʱ������������ص�ʱ�������һ�Σ�
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            Size s = Size;
            double aspect_ratio = (double)s.Width / (double)s.Height;
            GL.glViewport(0, 0, s.Width, s.Height);//�ӿڴ�С
            GL.glMatrixMode(GL.GL_PROJECTION);//��ͶӰ�任ģʽ����ȡ����, ѡ��ǰ�任����ΪͶӰ����
            GL.glLoadIdentity();//��һ�����󣬼������κα仯
            GL.gluPerspective(45.0f, aspect_ratio, 0.1f, 100.0f);// ����һ���Ӿ��壬�����Ӿ���������Ƕ�Ϊ�ȣ��Ӿ���Զ������Ϊ.1��.
            GL.glMatrixMode(GL.GL_MODELVIEW);//�ָ���ģʽ��ͼ�任ģʽ
            GL.glLoadIdentity(); 
        }
        public override void glDraw()
        {
            base.glDraw();  //��̬������Ҫ�Ķ���
            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);   // �����Ļ����Ȼ���
            GL.glLoadIdentity();
            GL.glTranslatef(0f, 0f, -1.94f);
            DrawForm();           
            this.SwapBuffer();//˫������
            GL.glFlush();//ǿ��ˢ��
        }

        private void DrawSinCurve()
        {
            
        }

        List<Point> curvePointList = new List<Point> { };
        //List<int> curveIdList = new List<int> { };
        int maxPointNum = 2000;
        /// <summary>
        /// Ϊ���������߼ӵ�
        /// </summary>
        /// <param name="curvePoint"></param>
        void CurveAddPoints(Point curvePoint)
        {
            if (curvePointList.Count < maxPointNum)
                curvePointList.Add(curvePoint);
            else
            {
                curvePointList.RemoveAt(0);
                curvePointList.Add(curvePoint);
            }
        }

        void DrawForm()
        {       
            float i;
            GL.glPushMatrix();
            GL.glColor3f(0.0f, 0.4f, 0.4f);
            GL.glLineWidth(1.0f);
            //GL.glEnable(GL.GL_LINE_STRIP);
            //GL.glLineStipple(1, 0x00ff);
            GL.glBegin(GL.GL_LINES);	//���ƶ���ı���
            //for (i = -1 + 0.166667f; i < 1; i += 0.166667f)
            for (i = -1 + 0.18f; i < 1; i += 0.18f)
            {
                GL.glVertex3f(1, i, 0.0f);
                GL.glVertex3f(-1, i, 0.0f);
            }
            for (i = -1 + 0.1f*1.5f; i < 1; i += 0.1f*1.5f)
            {
                GL.glVertex3f(i, 1, 0.0f);
                GL.glVertex3f(i, -1, 0.0f);
            }
            GL.glEnd();
            //GL.glDisable(GL.GL_LINE_STRIP);

            GL.glColor3f(0.0f, 1.0f, 1.0f);
            GL.glLineWidth(2.5f);
            GL.glBegin(GL.GL_LINES);	//���ƾ��α߿�

            GL.glVertex3f(1, -1, 0.0f);
            GL.glVertex3f(1, 1, 0.0f);

            GL.glVertex3f(1, 1, 0.0f);
            GL.glVertex3f(-1, 1, 0.0f);

            GL.glVertex3f(-1, 1, 0.0f);
            GL.glVertex3f(-1, -1, 0.0f);

            GL.glVertex3f(-1, -1, 0.0f);
            GL.glVertex3f(1, -1, 0.0f);

            GL.glEnd();

            GL.glPopMatrix();
        }
    }
}

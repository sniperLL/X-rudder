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
        //立方体条件
        public  Point3d cube_center = new Point3d();
        /// <summary>
        /// 
        /// </summary>
        public csglViewer()
        {

        }
        /// <summary>
        /// 至少重构的函数
        /// </summary>
        //protected override void OnCreateControl()
        //{
        //    base.OnCreateControl();

        //    GL.glClearDepth(1.0f);
        //    GL.glDepthFunc(GL.GL_LEQUAL);
        //    GL.glEnable(GL.GL_DEPTH_TEST);
        //}

        //初始化OpenGL绘制参数 
        protected override void InitGLContext()
        {
            GL.glShadeModel(GL.GL_SMOOTH); // 开启Smooth Shading

            GL.glClearColor(0.0f, 0.0f, 0.0f, 0.5f);// 黑色背景
            GL.glClearDepth(1.0f);   // 设置深度缓冲区
            GL.glEnable(GL.GL_DEPTH_TEST);// 开启深度测试
            GL.glDepthFunc(GL.GL_LEQUAL);//设置深度测试方式
            GL.glHint(GL.GL_PERSPECTIVE_CORRECTION_HINT, GL.GL_NICEST);// 精确的透视投影计算方式
            //GL.glClearColor(0.0f, 0.0f, 0.0f, 0.5f); ; 
            //GL.glMatrixMode(GL.GL_PROJECTION); 
            //GL.glLoadIdentity(); 
            //GL.gluOrtho2D(0.0, Size.Width, 0.0, Size.Height);
        }

        //窗口大小改变时发生（窗体加载的时候会运行一次）
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            Size s = Size;
            double aspect_ratio = (double)s.Width / (double)s.Height;
            GL.glViewport(0, 0, s.Width, s.Height);//视口大小
            GL.glMatrixMode(GL.GL_PROJECTION);//以投影变换模式设置取景区, 选择当前变换矩阵为投影矩阵
            GL.glLoadIdentity();//归一化矩阵，即不做任何变化
            GL.gluPerspective(45.0f, aspect_ratio, 0.1f, 100.0f);// 创建一个视景体，设置视景体参数：角度为度，视景体远近距离为.1和.
            GL.glMatrixMode(GL.GL_MODELVIEW);//恢复到模式视图变换模式
            GL.glLoadIdentity(); 
        }
        public override void glDraw()
        {
            base.glDraw();  //动态绘制想要的东西
            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);   // 清除屏幕及深度缓存
            GL.glLoadIdentity();
            GL.glTranslatef(0f, 0f, -1.94f);
            DrawForm();           
            this.SwapBuffer();//双缓冲区
            GL.glFlush();//强制刷新
        }

        private void DrawSinCurve()
        {
            
        }

        List<Point> curvePointList = new List<Point> { };
        //List<int> curveIdList = new List<int> { };
        int maxPointNum = 2000;
        /// <summary>
        /// 为所绘制曲线加点
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
            GL.glBegin(GL.GL_LINES);	//绘制多个四边形
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
            GL.glBegin(GL.GL_LINES);	//绘制矩形边框

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

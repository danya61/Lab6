/*
 * Created by SharpDevelop.
 * User: User
 * Date: 23.12.2017
 * Time: 22:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace l6
{
	/// <summary>
	/// Description of Form2.
	/// </summary>
	public partial class Form2 : Form
	{
		Bitmap bmp;
		Graphics g;
		Pen pen_facets = new Pen(Color.Green);
		int maxx = Int32.MinValue;
		int minx = Int32.MaxValue;
		int maxy = Int32.MinValue;
		int miny = Int32.MaxValue;
		int centerX;
		int centerY;
		public Form2(List<edges> fNew)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			foreach (edges f in fNew){
				foreach (point3D p in f.points){
					if (p.X>maxx)
						maxx=(int)p.X;
					if (p.X<minx)
						minx=(int)p.X;
					if (p.Y>maxy)
						maxy=(int)p.Y;
					if (p.Y<miny)
						miny=(int)p.Y;
				}
			}
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			bmp = new Bitmap(pictureBox1.Width,pictureBox1.Height);
			centerX=pictureBox1.Width/2; centerY=pictureBox1.Height/2;
			pictureBox1.Image=bmp;
			g = Graphics.FromImage(bmp);
			
			foreach (edges ee in fNew)
				eDraw2(ee);
			
			g.Dispose();
			pictureBox1.Update();
		}
		
		void eDraw2(edges f) {
			int n = f.points.Count - 1;
			int x1 = (int)Math.Round(maxx - f.points[0].X+25); int x2 = (int)Math.Round(maxx-f.points[n].X+25);
			int y1 = (int)Math.Round(maxy-f.points[0].Y+25); int y2 = (int)Math.Round(maxy-f.points[n].Y+25);
			g.DrawLine(new Pen(Color.Black), x1, y1, x2, y2);
			
			for (int i = 0; i < n; i++)
			{
			    x1 = (int)Math.Round(maxx-f.points[i].X+25); x2 = (int)Math.Round(maxx-f.points[i + 1].X+25);
			    y1 = (int)Math.Round(maxy-f.points[i].Y+25); y2 = (int)Math.Round(maxy-f.points[i + 1].Y+25);
			    g.DrawLine(new Pen(Color.Black), x1, y1, x2, y2);
			}
		}
	}
}

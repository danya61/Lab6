/*
 * Created by SharpDevelop.
 * User: User
 * Date: 28.11.2017
 * Time: 1:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace l6
{
	public delegate double fnc(double x, double y);
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		double sumq(double x, double y) {
			return x * x + y * y;
		}
		double minq(double x, double y) {
			return x * x - y * y;
		}
		
		Bitmap bmp;
		Graphics g;
		int centerX, centerY;
		point3D P1, P2;
		List<edges> figure = new List<edges>();
		List<point3D> points = new List<point3D>();
		double[,] pMatr = new double[4,4];
		double[,] iMatr = new double[4,4];
		double[,] oMatr = new double[4,4];
		List<List<point3D>> rFig = new List<List<point3D>>();
		int pts = 0;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			bmp = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
			pictureBox.Image = bmp; centerX = pictureBox.Width / 2; centerY = pictureBox.Height / 2;
			this.pMatr[3,3]=1;
			this.iMatr[3,3]=1;
			this.oMatr[3,3]=1;
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			g = Graphics.FromImage(bmp);
		}
		void ComboBox1SelectionChangeCommitted(object sender, EventArgs e)
		{
			figure.Clear();
			switch (comboBox1.SelectedItem.ToString()) {
				case "тетраэдр":
					tetrahedron();
					break;
				case "октаэдр":
					octahedron();
					break;
				case "гексаэдр":
					cube();
					break;
				case "икосаэдр":
					ikosaedr();
					break;
			}
			fillPoints();
			imDraw();
		}
		
		void prDraw(point3D p) {
			g.FillEllipse(new SolidBrush(Color.Black), (int)Math.Round(p.X + centerX - 3), (int)Math.Round(-p.Y + centerY - 3), 6, 6);
		}
		
		void imDraw() {
			g.Clear(Color.White);
			foreach (edges f in figure)
			    eDraw(f);
			if (P1 != null && P2 != null)
			{
			    pDraw(P1);
			    pDraw(P2);
			    g.DrawLine(new Pen(Color.Black), (int)Math.Round(P1.X + centerX), (int)Math.Round(-P1.Y + centerY), (int)Math.Round(P2.X + centerX), (int)Math.Round(-P2.Y + centerY));
			}
			pictureBox.Image = bmp;
		}
		
		void eDraw(edges f) {
			int n = f.points.Count - 1;
			int x1 = (int)Math.Round(f.points[0].X + centerX); 
			int x2 = (int)Math.Round(f.points[n].X + centerX);
			int y1 = (int)Math.Round(-f.points[0].Y + centerY); 
			int y2 = (int)Math.Round(-f.points[n].Y + centerY);
			g.DrawLine(new Pen(Color.Black), x1, y1, x2, y2);
			
			for (int i = 0; i < n; i++)
			{
			    x1 = (int)Math.Round(f.points[i].X + centerX); x2 = (int)Math.Round(f.points[i + 1].X + centerX);
			    y1 = (int)Math.Round(-f.points[i].Y + centerY); y2 = (int)Math.Round(-f.points[i + 1].Y + centerY);
			    g.DrawLine(new Pen(Color.Black), x1, y1, x2, y2);
			}
		}
		
		void pDraw(point3D p) {
			g.FillEllipse(new SolidBrush(Color.Black), (int)Math.Round(p.X + centerX - 6), (int)Math.Round(-p.Y + centerY - 6), 5, 5);
		}
		
		void fillPoints() {
			points.Clear();
			foreach (edges e in figure)
			    for (int i = 0; i < e.points.Count; i++)
			        if (!points.Contains(e.points[i]))
			            points.Add(e.points[i]);
		}
		
		void tetrahedron() {	
			var p1 = new point3D(-100, -100 / 2, 0);
			var p2 = new point3D(0, -100 / 2, -100);
			var p3 = new point3D(100, -100 / 2, 0);
			var p4 = new point3D(0, 100 / 2, 0);
			
			var f1 = new edges();
			var f2 = new edges();
			var f3 = new edges();
			var f4 = new edges();
			
			f1.add(p1); 
			f1.add(p2); 
			f1.add(p3);
			
			f2.add(p1); 
			f2.add(p4); 
			f2.add(p2);
			
			f3.add(p4); 
			f3.add(p2); 
			f3.add(p3);
			
			f4.add(p1); 
			f4.add(p4); 
			f4.add(p3);
			
			figure.Add(f1);
			figure.Add(f2);
			figure.Add(f3);
			figure.Add(f4);
		}
		
		void ikosaedr() {
			var p1 = new point3D(-100 * 1 / 2, 100, 0);
			var p2 = new point3D(100 * 1 / 2, 100, 0);
			var p3 = new point3D(0, 100 * 1 / 2, 100);
			var p4 = new point3D(0, -100 * 1 / 2, 100);
			var p5 = new point3D(-100 * 1 / 2, -100, 0);
			var p6 = new point3D(100 * 1 / 2, -100, 0);
			var p7 = new point3D(-100, 0, 100 * 1 / 2);
			var p8 = new point3D(-100, 0, -100 * 1 / 2);
			var p9 = new point3D(100, 0, 100 * 1 / 2);
			var p10 = new point3D(100, 0, -100 * 1 / 2);
			var p11 = new point3D(0, 100 * 1 / 2, -100);
			var p12 = new point3D(0, -100 * 1 / 2, -100);
			
			var f1 = new edges();
			var f2 = new edges();
			var f3 = new edges();
			var f4 = new edges();
			var f5 = new edges();
			var f6 = new edges();
			var f7 = new edges();
			var f8 = new edges();
			var f9 = new edges();
			var f10 = new edges();
			var f11 = new edges();
			var f12 = new edges();
			var f13 = new edges();
			var f14 = new edges();
			var f15 = new edges();
			var f16 = new edges();
			var f17 = new edges();
			var f18 = new edges();
			var f19 = new edges();
			var f20 = new edges();
			
			f1.add(p1); 
			f1.add(p2); 
			f1.add(p3);
			
			f2.add(p1); 
			f2.add(p3);
			f2.add(p7);
			
			f3.add(p1);
			f3.add(p8);
			f3.add(p11);
			
			f4.add(p1);
			f4.add(p2);
			f4.add(p11);
			
			f5.add(p1);
			f5.add(p7);
			f5.add(p8);
			
			f6.add(p2);
			f6.add(p3);
			f6.add(p9);
			
			f7.add(p2);
			f7.add(p9); 
			f7.add(p10);
			
			f8.add(p2);
			f8.add(p10);
			f8.add(p11);
			
			f9.add(p3);
			f9.add(p4);
			f9.add(p7);
			
			f10.add(p3);
			f10.add(p4);
			f10.add(p9);
			
			f11.add(p4);
			f11.add(p5);
			f11.add(p7);
			
			f12.add(p4);
			f12.add(p5);
			f12.add(p6);
			
			f13.add(p4);
			f13.add(p6);
			f13.add(p9);
			
			f14.add(p5);
			f14.add(p7);
			f14.add(p8);
			
			f15.add(p5);
			f15.add(p8);
			f15.add(p12);
			
			f16.add(p5);
			f16.add(p6);
			f16.add(p12);
			
			f17.add(p6);
			f17.add(p9);
			f17.add(p10);
			
			f18.add(p6);
			f18.add(p10);
			f18.add(p12);
			
			f19.add(p8); 
			f19.add(p11); 
			f19.add(p12);
			
			f20.add(p10); 
			f20.add(p11); 
			f20.add(p12);
			
			figure.Add(f1);
			figure.Add(f2);
			figure.Add(f3);
			figure.Add(f4);
			figure.Add(f5);
			figure.Add(f6);
			figure.Add(f7);
			figure.Add(f8);
			figure.Add(f9);
			figure.Add(f10);
			figure.Add(f11);
			figure.Add(f12);
			figure.Add(f13);
			figure.Add(f14);
			figure.Add(f15);
			figure.Add(f16);
			figure.Add(f17);
			figure.Add(f18);
			figure.Add(f19);
			figure.Add(f20);
		}
		
		void cube() {
			var p1 = new point3D(-100, -100, -100);
			var p2 = new point3D(-100, 100, -100);
			var p3 = new point3D(100, 100, -100);
			var p4 = new point3D(100, -100, -100);
			var p5 = new point3D(-100, -100, 100);
			var p6 = new point3D(-100, 100, 100);
			var p7 = new point3D(100, 100, 100);
			var p8 = new point3D(100, -100, 100);
			
			var f1 = new edges();
			var f2 = new edges();
			var f3 = new edges();
			var f4 = new edges();
			var f5 = new edges();
			var f6 = new edges();
			
			f1.add(p1);
			f1.add(p2);
			f1.add(p3);
			f1.add(p4);
			
			f2.add(p1);
			f2.add(p2);
			f2.add(p6);
			f2.add(p5);
			
			f3.add(p5);
			f3.add(p6);
			f3.add(p7);
			f3.add(p8);
			
			f4.add(p4);
			f4.add(p3);
			f4.add(p7);
			f4.add(p8);
			
			f5.add(p2);
			f5.add(p6);
			f5.add(p7);
			f5.add(p3);
			
			f6.add(p1);
			f6.add(p5);
			f6.add(p8);
			f6.add(p4);
			
			figure.Add(f1);
			figure.Add(f2);
			figure.Add(f3);
			figure.Add(f4);
			figure.Add(f5);
			figure.Add(f6);
		}
		
		void octahedron() {
			double a = Math.Sqrt(3) * 100;
			double p = (a + a + (100 / 2)) / 2;
			double h = 2 * Math.Sqrt(p * (p - (100 / 2)) * (p - a) * (p - a)) / (100 / 2);
			
			var p1 = new point3D(0, -h, 0);
			var p2 = new point3D(-100, 0, -100);
			var p3 = new point3D(0, h, 0);
			var p4 = new point3D(100, 0, -100);
			var p5 = new point3D(-100, 0, 100);
			var p6 = new point3D(100, 0, 100);
			
			var f1 = new edges();
			var f2 = new edges();
			var f3 = new edges();
			var f4 = new edges();
			var f5 = new edges();
			var f6 = new edges();
			var f7 = new edges();
			var f8 = new edges();
			
			f1.add(p2);
			f1.add(p3);
			f1.add(p4);
			
			f2.add(p2);
			f2.add(p1);
			f2.add(p4);
			
			f3.add(p2);
			f3.add(p3);
			f3.add(p5);
			
			f4.add(p2);
			f4.add(p1);
			f4.add(p5);
			
			f5.add(p4);
			f5.add(p3);
			f5.add(p6);
			
			f6.add(p4);
			f6.add(p1);
			f6.add(p6);
			
			f7.add(p5);
			f7.add(p3);
			f7.add(p6);
			
			f8.add(p5);
			f8.add(p1);
			f8.add(p6);
			
			figure.Add(f1);
			figure.Add(f2);
			figure.Add(f3);
			figure.Add(f4);
			figure.Add(f5);
			figure.Add(f6);
			figure.Add(f7);
			figure.Add(f8);
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			if(this.checkBox1.Checked) {
				foreach (point3D p in points) {
			    	p.X += Int32.Parse(this.afX.Text);
			    	p.Y += Int32.Parse(this.afY.Text);
			    	p.Z += Int32.Parse(this.afZ.Text);
				}
			}
			else if(this.checkBox3.Checked) {
				foreach (point3D p in points) {
			   		p.X *= double.Parse(this.afX.Text);
			    	p.Y *= double.Parse(this.afY.Text);
			    	p.Z *= double.Parse(this.afZ.Text);
				}
			}
			else if(this.checkBox2.Checked) {
				foreach (point3D p in points) {
			    	xRotate(p, (double.Parse(this.afX.Text) * Math.PI) / 180);
			    	yRotate(p, (double.Parse(this.afY.Text) * Math.PI) / 180);
			    	zRotate(p, (double.Parse(this.afZ.Text) * Math.PI) / 180);
				}
			}
			imDraw();
		}
			
		void xRotate(point3D p, double a) {
			double y = p.Y;
			double z = p.Z;
			p.Y = y * Math.Cos(a) + z * Math.Sin(a);
			p.Z = y * -Math.Sin(a) + z * Math.Cos(a);
		}
		
		 void yRotate(point3D p, double a) {
			double x = p.X;
			double z = p.Z;
			p.X = x * Math.Cos(a) + z * -Math.Sin(a);
			p.Z = x * Math.Sin(a) + z * Math.Cos(a);
		}
		
		void zRotate(point3D p, double a) {
			double x = p.X;
			double y = p.Y;
			p.X = x * Math.Cos(a) + y * Math.Sin(a);
			p.Y = x * -Math.Sin(a) + y * Math.Cos(a);
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			if(this.radioButton2.Checked) {
				foreach (point3D p in points)
						p.X = -p.X;
			}
			else if(this.radioButton1.Checked) {
				foreach (point3D p in points)
						p.Y = -p.Y;
			}
			else if(this.radioButton3.Checked) {
				foreach (point3D p in points)
						p.Z = -p.Z;
			}
			imDraw();
		}
		
		void PictureBoxClick(object sender, EventArgs e)
		{
		}
		
		void PictureBoxMouseClick(object sender, MouseEventArgs e)
		{
			if(this.checkBox4.Checked) {
				if(P1 == null) {
					rFig.Add(new List<point3D>());
					P1 = new point3D((e.X - centerX), (-e.Y + centerY), 0);
				}
				else if(P2 == null) {
					P2 = new point3D((e.X - centerX), (-e.Y + centerY), 0);
					g.DrawLine(new Pen(Color.Black), (int)Math.Round(P1.X + centerX), (int)Math.Round(-P1.Y + centerY), (int)Math.Round(P2.X + centerX), (int)Math.Round(-P2.Y + centerY));
				}
				else {
					if (pts == 0)
						return;
					point3D pt = new point3D((e.X - centerX), (-e.Y + centerY), 0);
					rFig[0].Add(pt);
					prDraw(pt);
				}
			}
			pictureBox.Image = bmp;
		}
		void Button5MouseClick(object sender, MouseEventArgs e)
		{
			if(this.checkBox5.Checked) {
				double angle = (double.Parse(this.textBox1.Text) * Math.PI) / 180;
				axR(P1, P2, angle);
				imDraw();
			}
		}
	
	point3D normV (point3D p1, point3D p2) {
			double x = p2.X - p1.X;
			double y = p2.Y - p1.Y;
			double z = p2.Z - p1.Z;
			double length = Math.Sqrt(x * x + y * y + z * z);
			return new point3D(x / length, y / length, z / length);
		}
		
	void axR(point3D p1, point3D p2, double angle) {
			point3D c = normV(p1, p2);
			double x = c.X, y = c.Y, z = c.Z;
			double d = Math.Sqrt(y * y + z * z);
			double alpha = -Math.Asin(y / d);
			double beta = Math.Asin(x);
			
			foreach (point3D p in points) {
			    p.X -= p1.X;
			    p.Y -= p1.Y;
			    p.Z -= p1.Z;
			
			    xRotate(p, alpha);
			    yRotate(p, beta);
			    zRotate(p, angle);
			    yRotate(p, -beta);
			    xRotate(p, -alpha);
			
			    p.X += p1.X;
			    p.Y += p1.Y;
			    p.Z += p1.Z;
			}
		}
		
		void Button1MouseClick(object sender, MouseEventArgs e)
		{
			var fNew = new List<edges>();
			switch (comboBox2.SelectedItem.ToString()) {
				case "перспективная":
					int x = Int32.Parse(this.textBox4.Text);
			    	int y = Int32.Parse(this.textBox3.Text);
			    	int z = Int32.Parse(this.textBox2.Text);
			    	pMatr[0,3]=0;
					pMatr[1,3]=0;
					pMatr[2,3]=0;
					pMatr[0,3]=-1/x;
					pMatr[1,3]=-1/y;
					pMatr[2,3]=-1/z;
					pMatr[0,0]=1;
					pMatr[1,1]=1;
					foreach (edges f in figure){
						var f1 = new edges();
						foreach (point3D pt in f.points) {
							double xx = pt.X*pMatr[0,0]+pt.Y*pMatr[1,0]+pt.Z*pMatr[2,0]+pMatr[3,0];
							double yy = pt.X*pMatr[0,1]+pt.Y*pMatr[1,1]+pt.Z*pMatr[2,1]+pMatr[3,1];
							double d = pt.X*pMatr[0,3]+pt.Y*pMatr[1,3]+pt.Z*pMatr[2,3]+pMatr[3,3];
							double t = 1 / d;
							xx*=t; 
							yy*=t;
							f1.add(new point3D(xx,yy,0));
						}
					fNew.Add(f1);
					}
					break;
				case "изометрическая":
					double rad = 120 * Math.PI / 180;
					double sin = Math.Sin(rad);
					double cos = Math.Cos(rad);
					iMatr[0,0]=cos;
					iMatr[0,1]=sin*sin;
					iMatr[1,1]=cos;
					iMatr[2,0]=sin;
					iMatr[2,1]=-sin*cos;
					iMatr[3,3]=1;
				
					foreach (edges f in figure) {
						var f1 = new edges();
						foreach (point3D pt in f.points) {
							double xx = pt.X*iMatr[0,0]+pt.Y*iMatr[1,0]+pt.Z*iMatr[2,0]+iMatr[3,0];
							double yy = pt.X*iMatr[0,1]+pt.Y*iMatr[1,1]+pt.Z*iMatr[2,1]+iMatr[3,1];
						
							f1.add(new point3D(xx, yy, 0));
						}
						fNew.Add(f1);
					}
					break;
				case "ортографическая":
					oMatr[3,3]=1;
					switch (comboBox3.SelectedItem.ToString()) {
						case "xy":
							oMatr[0, 0] = 1;
							oMatr[1, 1] = 1;
							break;
						case "zx":
							oMatr[0, 0] = 1;
							oMatr[2, 2] = 1;
							break;
						case "yz":
							oMatr[1, 1] = 1;
							oMatr[2, 2] = 1;
							break;
					}
					foreach (edges f in figure){
						var f1=new edges();
						foreach (point3D pt in f.points) {
							double xx = pt.X*oMatr[0,0]+pt.Y*oMatr[1,0]+pt.Z*oMatr[2,0]+oMatr[3,0];
							double yy = pt.X*oMatr[0,1]+pt.Y*oMatr[1,1]+pt.Z*oMatr[2,1]+oMatr[3,1];
							double zz = pt.X*oMatr[0,2]+pt.Y*oMatr[1,2]+pt.Z*oMatr[2,2]+oMatr[3,2];
						
							if (zz == 0)
								f1.add(new point3D(xx, yy, 0));
							else if (yy == 0)
								f1.add(new point3D(xx, zz, 0));
							else if (xx == 0)
								f1.add(new point3D(yy, zz, 0));
						
						}
						fNew.Add(f1);
					}
					break;
			}
			
			for(int i = 0; i < 4; i++)
				for(int j = 0; j < 4; j++)
					oMatr[i, j]=0;
			var t3 = new Form2(fNew);
			t3.Show();  
		}
		
		void Button2MouseClick(object sender, MouseEventArgs e)
		{
			if(figure.Count != 0) {
				List<string> res = new List<string>();
				for (int i = 0; i < figure.Count; i++) {
					var ee = figure[i];
					string s = "";
					for (int j = 0; j < ee.points.Count - 1; j++) {
						var pt = ee.points[j];
						s += pt.X.ToString() + " " + pt.Y.ToString() + " " + pt.Z.ToString();
						s += ": ";
					}
					s += ee.points[ee.points.Count - 1].X.ToString() + " " + ee.points[ee.points.Count - 1].Y.ToString() +" "+ ee.points[ee.points.Count - 1].Z.ToString();
					res.Add(s);
				}
				File.WriteAllLines("figure.txt", res.ToArray());
				MessageBox.Show("сохранено");
			}
		}
		void Button6MouseClick(object sender, MouseEventArgs e)
		{
			OpenFileDialog load = new OpenFileDialog();
			load.ShowDialog();
			if (!load.CheckFileExists){
				MessageBox.Show("Choose File");
				return;
			}
			
			string[] lines = File.ReadAllLines(load.FileName);
			for (int i = 0; i < lines.Length; i++){
				string[] p = lines[i].Split(':');
				var ee = new edges();
				for (int j = 0; j < p.Length; j++) {
					string s = p[j].TrimStart(' ');
					string[] xyz = s.Split(' ');
					var x = Double.Parse(xyz[0]);
					var y = Double.Parse(xyz[1]);
					var z = Double.Parse(xyz[2]);
					ee.add(new point3D(x,y,z));
				}
				figure.Add(ee);
			}
			fillPoints();
			imDraw();
		}
		
	void xRotatef(point3D p, double angle) {
			double y = p.Y;
			double z = p.Z;
			p.Y = y * Math.Cos(angle) + z * Math.Sin(angle);
			p.Z = y * -Math.Sin(angle) + z * Math.Cos(angle);
		}
		
	void yRotatef(point3D p, double angle) {
			double x = p.X;
			double z = p.Z;
			p.X = x * Math.Cos(angle) + z * -Math.Sin(angle);
			p.Z = x * Math.Sin(angle) + z * Math.Cos(angle);
		}
		
	void zRotatef(point3D p, double angle) {
			double x = p.X;
			double y = p.Y;
			p.X = x * Math.Cos(angle) + y * Math.Sin(angle);
			p.Y = x * -Math.Sin(angle) + y * Math.Cos(angle);
		}
		
	point3D axRp(point3D p1, point3D p2, point3D p, double angle){
			point3D c = normV(p1, p2);
			
			double x = c.X, y = c.Y, z = c.Z;
			double d = Math.Sqrt(y * y + z * z);
			double alpha = -Math.Asin(y / d);
			double beta = Math.Asin(x);
			point3D res = new point3D(p.X,p.Y,p.Z);
			res.X -= p1.X;
			res.Y -= p1.Y;
			res.Z -= p1.Z;
			
			xRotatef(res, alpha);
			yRotatef(res, beta);
			zRotatef(res, angle);
			yRotatef(res, -beta);
			xRotatef(res, -alpha);
			
			res.X += p1.X;
			res.Y += p1.Y;
			res.Z += p1.Z;
			
			return res;
		}
		
		void Button7MouseClick(object sender, MouseEventArgs e)
		{
			pts++;
			if (pts<2) 
				return;
			pts = 0;
			double st = Int32.Parse(textBox5.Text);
			double angle = ((360.0/st)* Math.PI) / 180;

			for (int i = 0; i < st; i++) {
				rFig.Add(new List<point3D>());
				for (int j = 0; j <  rFig[i].Count; j++)
					rFig[i+1].Add(axRp(P1,P2,rFig[i][j], angle));
			}

			for (int i = 0; i < rFig.Count-1; i++) {
				for (int j = 0; j < rFig[i].Count-1; j++) {
					var ee = new edges();
					ee.add(rFig[i][j]);
					ee.add(rFig[i][j+1]);
					ee.add(rFig[i+1][j+1]);
					ee.add(rFig[i+1][j]);
					figure.Add(ee);
				}
			}
			fillPoints();
			imDraw();
		}
		
		void Button8MouseClick(object sender, MouseEventArgs e)
		{
			fnc f = sumq;
			switch (comboBox4.SelectedIndex) {
				case 0:
					break;
				case 2:
					f = minq;
					break;
			}
			
			int x1 = Int32.Parse(this.x1.Text);
			int y1 = Int32.Parse(this.y1.Text);
			int x2 = Int32.Parse(this.x2.Text);
			int y2 = Int32.Parse(this.y2.Text);
			int st = Int32.Parse(this.step.Text);
			
			int i = x1;
			int j = y1;
			
			int ky = (y2 - y1) / st + 1;
			int kx = (x2 - x1) / st + 1;
			
			while (i <= x2) {
				j = y1;
				while (j <= y2) {
					points.Add(new point3D(i, j, f(i, j)));
					j += st;
				}
				i += st;
			}
			
			for(int ii = 0; ii < ky - 1; ii++){
				for (int jj = 0; jj < kx - 1; jj++) {
					var edg = new edges();
					edg.add(points[ky*ii + jj]);
					edg.add(points[ky*ii + jj +1]);
					edg.add(points[ky*(ii + 1) + jj + 1]);
					edg.add(points[ky*(ii + 1) + jj]);
					figure.Add(edg);
				}
			}
			fillPoints();
			imDraw();
		}
		
	}
	
	public class point3D
    {
        public double X, Y, Z;

        public point3D()
        {
            this.X = this.Y = this.Z = 0;
        }

        public point3D(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

    }

    public class edges
    {
        public List<point3D> points;

        public edges()
        {
            points = new List<point3D>();
        }

        public void add(point3D p)
        {
            points.Add(p);
        }
        public bool is_visible()
        {
            point3D p1 = points[0];
            point3D p2 = points[1];
            point3D p3 = points[2];
            double aX = p2.X - p1.X;
            double aY = p2.Y - p1.Y;
            double aZ = p2.Z - p1.Z;
            double bX = p3.X - p2.X;
            double bY = p3.Y - p2.Y;
            double bZ = p3.Z - p2.Z;

            var n = new point3D(aY * bZ - aZ * bY, aZ * bX - aX * bZ, aX * bY - aY * bX);
            return (aX * bY - aY * bX <= 0);
        }
        
			
	}
}
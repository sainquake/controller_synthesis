/*
 * Created by SharpDevelop.
 * User: SAINQUAKE
 * Date: 03.06.2014
 * Time: 10:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ODE01;

namespace controller_synthesis
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		/*double delta = 8;
		double Vmax = 60;
		double amax = 120;
		double omega = 0;
		double a0 = 0;
		
		double K = 16.128; //  1/Ce
		double Km = 1.258; // R/(CmCe)
		double Ta = 0.0001;
		double T = 0.3; // a22*R/(CmCe)
		double Tu = 0.000006; //L/R
		
		double Ke = 0;
		double Tc = 0;
		
		double M = 1.1;//1.1 1.25
		double eps = 0.1;*/
		
		//private System.Collections.Generic.Dictionary<int, int> m;
		
		CDataIO DIO;
		public MainForm()
		{
			DIO=new CDataIO();
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			//Calc();
			//
			//dataGrid1.Rows
			resetData();
			
			//autoSave();
			autoLoad();
		}
		void resetData(){
			dataGridView1.Rows.Clear();
			dataGridView1.Rows.Add("Маркировка привода","ЭБМ-230","ЭБМ-320");
			dataGridView1.Rows.Add("Номинальная скорость вращения \u03A9ном (об/мин)",108,40);
			dataGridView1.Rows.Add("Минимальная скорость вращения \u03A9min (об/мин)",0.1,0.1);
			dataGridView1.Rows.Add("Номинальный момент Мном (Нм)",3.5,6.8);
			dataGridView1.Rows.Add("Пусковой момент Мп (Нм)",7.5,16);
			dataGridView1.Rows.Add("Номинальный ток Iном (А)",6.2,6.2);
			dataGridView1.Rows.Add("Пусковой ток Iп (А)",12.5,13.4);
			dataGridView1.Rows.Add("Номинальное напряжение питания U(В)",21.6,24.3);
			dataGridView1.Rows.Add("Сопротивление статорной обмотки  Rс(Ом)",3.09,3.06);
			dataGridView1.Rows.Add("Электромагнитная постоянная времени  Тэ(мкс)",58.25,91.5);
			dataGridView1.Rows.Add("Момент инерции ротора  Jр(кгм^2)",0.017,0.078);
			dataGridView1.Rows.Add("Момент инерции нагрузки  Jн(кгм^2)",0.158,0.372);
			dataGridView1.Rows.Add("Максимальный момент сопротивления Mc(Нм)",0.525,1.13);
			
			dataGridView2.Rows.Clear();
			dataGridView2.Rows.Add("Максимальный допустимая ошибка (угл.сек)",4,4);
			dataGridView2.Rows.Add("Максимальная скорость (град/сек)",60,60);
			dataGridView2.Rows.Add("Максимальное ускорение (град/сек^2)",120,120);
			dataGridView2.Rows.Add("Показатель колебательности M (1.05 - 1.25)",1.1,1.1);
			dataGridView2.Rows.Add("Точность инвариантности ",0.01,0.01);
			dataGridView2.Rows.Add("Коэффициент передачи датчика, Кd ",1,1);
			
			dataGridView3.Rows.Clear();
			dataGridView3.Rows.Add("Rc",0,0);
			dataGridView3.Rows.Add("Te",0,0);
			dataGridView3.Rows.Add("Cm",0,0);
			dataGridView3.Rows.Add("Ce",0,0);
			dataGridView3.Rows.Add("Jn",0,0);
			dataGridView3.Rows.Add("Km",0,0);
			dataGridView3.Rows.Add("Tm",0,0);
			
			dataGridView3.Rows.Add("\u03b10",0,0);
			dataGridView3.Rows.Add("\u03c9k",0,0);
			dataGridView3.Rows.Add("Keps",0,0);
			dataGridView3.Rows.Add("K(I)",0,0);
			dataGridView3.Rows.Add("\u03C4",0,0);
			dataGridView3.Rows.Add("\u03b1",0,0);
			dataGridView3.Rows.Add("Kdus",0,0);
			dataGridView3.Rows.Add("P",0,0);
			dataGridView3.Rows.Add("D",0,0);
			
			dataGridView3.Rows.Add("tau 2",0,0);
			dataGridView3.Rows.Add("omega 2",0,0);
			dataGridView3.Rows.Add("T2",0,0);
			dataGridView3.Rows.Add("f",0,0);
			dataGridView3.Rows.Add("omega p",0,0);
			//
			dataGridView4.Rows.Clear();
			dataGridView4.Rows.Add("Cm",0,0);
			dataGridView4.Rows.Add("Ce",0,0);
			dataGridView4.Rows.Add("Tm, сек",0,0);
			dataGridView4.Rows.Add("Km",0,0);
			dataGridView4.Rows.Add("Сдв",0,0);
			
			dataGridView5.Rows.Clear();
			dataGridView5.Rows.Add("\u03b10, град",0,0);
			dataGridView5.Rows.Add("\u03c9k, рад/сек",0,0);
			dataGridView5.Rows.Add("Keps",0,0);
			dataGridView5.Rows.Add("K",0,0);
			dataGridView5.Rows.Add("\u03C4",0,0);
			dataGridView5.Rows.Add("\u03b1",0,0);
			dataGridView5.Rows.Add("Kdus",0,0);
			dataGridView5.Rows.Add("P",0,0);
			dataGridView5.Rows.Add("I",0,0);
			dataGridView5.Rows.Add("D",0,0);
			dataGridView5.Rows.Add("f датчика, Гц",0,0);
			
			toolStripStatusLabel1.Text = "reset";
		}
		void autoSave(string path=".autosave"){
			string ds="";
			
			foreach(DataGridViewRow r in dataGridView1.Rows)
				ds+=r.Cells[1].Value.ToString()+":"+r.Cells[2].Value.ToString()+":";
			ds+=";";
			foreach(DataGridViewRow r in dataGridView2.Rows)
				ds+=r.Cells[1].Value.ToString()+":"+r.Cells[2].Value.ToString()+":";
			ds+=";";
			
			// Write the string to a file.
			System.IO.StreamWriter file = new System.IO.StreamWriter(path);
			file.WriteLine(ds);
			
			file.Close();
			
			toolStripStatusLabel1.Text = "autoSave";
		}
		void autoLoad(string path=".autosave"){
			System.IO.StreamReader file = new System.IO.StreamReader(path);
			string ds = file.ReadLine();
			file.Close();
			
			string[] arr =  ds.Split(';');
			string[] cells = arr[0].Split(':');
			
			int i =0;
			foreach(DataGridViewRow r in dataGridView1.Rows){
				r.Cells[1].Value = cells[2*i];
				r.Cells[2].Value = cells[2*i+1];
				i++;
			}
			
			cells = arr[1].Split(':');
			
			i =0;
			foreach(DataGridViewRow r in dataGridView2.Rows){
				r.Cells[1].Value = cells[2*i];
				r.Cells[2].Value = cells[2*i+1];
				i++;
			}
			
			toolStripStatusLabel1.Text = "autoLoad";
			
			
			calc(1);
			calc(2);
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			/*delta = Cnv(textBox1);
			Vmax = Cnv(textBox2);
			amax = Cnv(textBox3);
			
			K = Cnv(textBox6);
			Km = Cnv(textBox9);
			Ta = Cnv(textBox11);
			T = Cnv(textBox10);
			Tu = Cnv(textBox12);
			
			M = Cnv(textBox8);
			eps = Cnv(textBox7);
			
			Calc();*/
			calc(1);
			calc(2);
			
			autoSave();
			
		}
		void calc(int n){
			Motor m = getMotor(n);
			PID pid = getPID(n,m);
			
			//label1.Text = m.ToString()+"\n"+ pid.ToString();
			
			dataGridView3.Rows[0].Cells[n].Value = m.Rc;
			dataGridView3.Rows[1].Cells[n].Value = m.Te;
			dataGridView3.Rows[2].Cells[n].Value = m.Cm;
			dataGridView3.Rows[3].Cells[n].Value = m.Ce;
			dataGridView3.Rows[4].Cells[n].Value = m.Jn;
			dataGridView3.Rows[5].Cells[n].Value = m.Km;
			dataGridView3.Rows[6].Cells[n].Value = m.Tm;
			//motor data output
			dataGridView4.Rows[0].Cells[n].Value = m.Cm;
			dataGridView4.Rows[1].Cells[n].Value = m.Ce;
			dataGridView4.Rows[2].Cells[n].Value = m.Tm;
			dataGridView4.Rows[3].Cells[n].Value = m.Km;
			dataGridView4.Rows[4].Cells[n].Value = m.K;
			//pid data output
			dataGridView5.Rows[0].Cells[n].Value = pid.A0;
			dataGridView5.Rows[1].Cells[n].Value = pid.Wk;
			dataGridView5.Rows[2].Cells[n].Value = pid.Keps;
			dataGridView5.Rows[3].Cells[n].Value = pid.K;
			dataGridView5.Rows[4].Cells[n].Value = pid.tau;
			dataGridView5.Rows[5].Cells[n].Value = m.Tm;
			dataGridView5.Rows[6].Cells[n].Value = (1-pid.eps)/m.Km;
			dataGridView5.Rows[7].Cells[n].Value = pid.P;
			dataGridView5.Rows[8].Cells[n].Value = pid.I;
			dataGridView5.Rows[9].Cells[n].Value = pid.D;
			
			
			dataGridView3.Rows[7].Cells[n].Value = pid.A0;
			dataGridView3.Rows[8].Cells[n].Value = pid.Wk;
			dataGridView3.Rows[9].Cells[n].Value = pid.Keps;
			dataGridView3.Rows[10].Cells[n].Value = pid.K;
			dataGridView3.Rows[11].Cells[n].Value = pid.tau;
			dataGridView3.Rows[12].Cells[n].Value = m.Tm;
			dataGridView3.Rows[13].Cells[n].Value = (1-pid.eps)/m.Km;
			dataGridView3.Rows[14].Cells[n].Value = pid.P;
			dataGridView3.Rows[15].Cells[n].Value = pid.D;
			
			double tau2 = Math.Sqrt( pid.M/(pid.Keps*pid.eps*(pid.M-1)) );
			double omega2 = pid.Keps*pid.eps*tau2;
			double T2 = Math.Sqrt(4*pid.M*(pid.M-1)/(pid.Keps*(pid.M+1)*(pid.M+1)));
			
			dataGridView3.Rows[16].Cells[n].Value = tau2;
			dataGridView3.Rows[17].Cells[n].Value = omega2;
			dataGridView3.Rows[18].Cells[n].Value = T2;
			dataGridView3.Rows[19].Cells[n].Value = 1/T2;
			dataGridView3.Rows[20].Cells[n].Value = 2/T2;
			//
			dataGridView5.Rows[10].Cells[n].Value = 1/T2;
		}
		PID getPID(int n,Motor m){
			return new PID(Convert.ToDouble(dataGridView2.Rows[1].Cells[n].Value)*60*60,
			               Convert.ToDouble(dataGridView2.Rows[2].Cells[n].Value)*60*60,
			                     Convert.ToDouble(dataGridView2.Rows[0].Cells[n].Value),
			                     Convert.ToDouble(dataGridView2.Rows[3].Cells[n].Value),
			                     Convert.ToDouble(dataGridView2.Rows[4].Cells[n].Value),
			                     m.Tm,m.K*Convert.ToDouble(dataGridView2.Rows[5].Cells[n].Value) );
		}
		Motor getMotor(int n){
			return new Motor(Convert.ToDouble(dataGridView1.Rows[1].Cells[n].Value),
			                     Convert.ToDouble(dataGridView1.Rows[4].Cells[n].Value),
			                     Convert.ToDouble(dataGridView1.Rows[3].Cells[n].Value),
			                     Convert.ToDouble(dataGridView1.Rows[10].Cells[n].Value)+Convert.ToDouble(dataGridView1.Rows[11].Cells[n].Value),
			                     Convert.ToDouble(dataGridView1.Rows[8].Cells[n].Value),
			                     Convert.ToDouble(dataGridView1.Rows[7].Cells[n].Value),
			                     Convert.ToDouble(dataGridView1.Rows[9].Cells[n].Value));
		}
		
		double Cnv(TextBox lb){
			return Convert.ToDouble(lb.Text);
		}
		
		double ApeLAC(double k,double T,double w){
			return 20*Math.Log10(k)- 20*Math.Log10(Math.Sqrt(1+T*T*w*w));
		}
		double ForLAC(double k,double T,double w){
			return 20*Math.Log10(k)+ 10*Math.Log10(Math.Sqrt(1+T*T*w*w));
		}
		double IntLAC(double k,double w){
			return 20*Math.Log10(k)- 20*Math.Log10(w);
		}
		
		void TextBox7TextChanged(object sender, EventArgs e)
		{
			
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			double[] y=new double[100000];
			double[] x=new double[100000];
			//x[0]=0;
			//y[0]=/*ApeLAC(100,0.01,0.00001)+*/IntLAC(10,1);
				
			for (int i=0;i<100000;i++)
			{
				x[i]=Math.Log10(i+1);
				y[i]=1;//ForLAC(1,Tc,i+1)+ApeLAC(1,Tu,i+1)+ApeLAC(Ke,Ta,i+1)+IntLAC(1,i+1)+IntLAC(1,i+1)+ForLAC(1,T,i+1);
				
			}
			//double [] delta=row;
			DIO.WriteArrayToFile("y.dat",100000,y);
			DIO.WriteArrayToFile("x.dat",100000,x);
			DIO.ShowDataImager("DataImager2.2.exe","x y");
		}
		void DataGrid1Navigate(object sender, System.Windows.Forms.NavigateEventArgs ne)
		{
			throw new NotImplementedException();
		}
		void LadToolStripMenuItemClick(object sender, EventArgs e)
		{
			toolStripStatusLabel1.Text = "save";
			save.Filter = "Save files (*.save)|*.save|All files (*.*)|*.*";
			save.ShowDialog();//.OpenFile();
		}
		void ToolStripMenuItem1Click(object sender, EventArgs e)
		{
			toolStripStatusLabel1.Text = "load";
			open.Filter = "Save files (*.save)|*.save|All files (*.*)|*.*";
			open.ShowDialog();//.OpenFile();
		}
		void ResetToolStripMenuItemClick(object sender, EventArgs e)
		{
			resetData();
		}
		void OpenFileOk(object sender, EventArgs e)
		{
			//resetData();
			autoLoad( open.FileName );
			toolStripStatusLabel1.Text = "open ok";
			autoSave();
		}
		void SaveFileOk(object sender, EventArgs e)
		{
			//resetData();
			autoSave( save.FileName );
			toolStripStatusLabel1.Text = "save ok";
		}
		void HelpToolStripMenuItemClick(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(@"readme.docx");
			toolStripStatusLabel1.Text = "help";
		}
	}
	
	public class Motor{
		public double Wst=0;
		public double Mst=0;//пусковой
		public double Mn=0;//номинальный
		
		public double Wxx{ get{return Mn*Wst/(Mst-Mn);} }//холостого хода
		
		public double Jn=0;
		
		public double Tm{ get{return Jn*Math.PI*Wxx/(Mst*30);} }
		
		public double Rc=0;
		public double U=0;
		
		public double Cm{ get{return Mst*Rc/U;} }
		public double Ce{ get{return U/Wxx;} }
		
		public double Te=0;
		
		public double K{ get{return 1/Ce;} }
		public double Km{ get{return Rc/(Ce*Cm);} }
		
		
		public Motor(double Wst_,double Mst_,double Mn_,double Jn_,double Rc_,double U_,double Te_){
			Wst=Wst_;
			Mst= Mst_;
			Mn= Mn_;
			Jn= Jn_;
			Rc= Rc_;
			U= U_;
			Te= Te_/1000000;
		}
		public override string ToString(){
			return "[Rc="+Rc.ToString()+ "; Te=" +Te.ToString()+"; Cm=" +Cm.ToString()+"; Ce=" +Ce.ToString()+"; Jn=" +Jn.ToString()+"; K=" +K.ToString()+"; Km=" +Km.ToString()+"; Tm=" +Tm.ToString()+";]";
		}
	}
	public class PID{
		public double vel=0;
		public double att=0;
		public double Wk{ get{return att/vel;} }
		public double A0{ get{return vel*vel/(att*60*60);} }
		
		public double error=0;
		public double Keps{ get{return att/error;} }
		
		public double M=0;
		public double tau{ get{return Math.Sqrt(M/(Keps*eps*(M-1)));} }
		
		public double Km=0;
		public double eps=0;
		public double K{ get{return Keps/(Km);} }
		
		public double I{ get{return K;} }
		
		public double Tm=0;
		public double P{ get{return K*(tau+Tm);} }
		public double D{ get{return K*tau*Tm;} }
		
		public PID(double vel_,double att_,double error_,double M_,double eps_,double Tm_,double Km_){
			vel=vel_;att=att_;error=error_;M=M_;eps=eps_;Km=Km_;Tm=Tm_;
		}
		public override string ToString(){
			return "[Keps="+Keps.ToString()+" tau="+tau.ToString()+" K(I)="+K.ToString()+" P="+P.ToString()+" D="+D.ToString()+"]";
		}
	}
}

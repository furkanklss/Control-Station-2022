using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using AForge.Video;
using AForge.Video.DirectShow;
using Accord.Video.FFMPEG;
using Accord.Video.VFW;
using TextBox = Guna.UI2.WinForms.Guna2TextBox;
using System.IO;
using System.Net;


namespace GAZI_UZAY_NOVA_YER_KONTROL_ISTASYONU
{
    public partial class Form1 : Form
    {
        public string Username;
        public string Filename;
        public string Fullname;
        public string Server;
        public string Password;
        public string path;
        public string localdest;
        double x, y, z;
        string[] veriPaketi;
        private string gelenTelemetriVerileri;
        int line = 1;
        Color renk1 = Color.Gray, renk2 = Color.Maroon;
        string xDegree, yDegree, zDegree;

        private FilterInfoCollection VideoCaptureDevices;
        private VideoCaptureDevice FinalVideo = null;
        private VideoCaptureDeviceForm captureDevice;
        private Bitmap video;
        private VideoFileWriter FileWriter = new VideoFileWriter();
        private SaveFileDialog saveAvi;


        public Form1()
        {
            InitializeComponent();
            modifyProgressBarColor.SetState(verticalProgressBar1, 2);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            textBox24.Visible = false;
            textBox25.Visible = false;
            textBox26.Visible = false;
            textBox27.Visible = false;
            textBox28.Visible = false;
            textBox29.Visible = false;
            textBox30.Visible = false;
            textBox31.Visible = false;
            textBox32.Visible = false;



            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                guna2ComboBox1.Items.Add(port);

            }
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);

            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.DragButton = MouseButtons.Left;

            gMapControl2.MapProvider = GMapProviders.GoogleMap;
            gMapControl2.DragButton = MouseButtons.Left;

            GL.ClearColor(Color.Black);

            VideoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            captureDevice = new VideoCaptureDeviceForm();

            circularProgressBar1.Value = 0;

        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            gelenTelemetriVerileri = serialPort1.ReadLine();
            this.Invoke(new EventHandler(displaydata));

        }

        private void displaydata(object sender, EventArgs e)
        {
            veriPaketi = gelenTelemetriVerileri.Split(',');
            
            int index = veriPaketi.Length;

            for (int i = 0; i < index; i++)
            {
                if (i == 0 || i == 1)
                {
                    ((TextBox)panel5.Controls["textBox" + (i + 1)]).Text = veriPaketi[i];

                    if (textBox1.Text == "")
                    {
                        textBox1.Text = "0";
                    }
                    if (textBox2.Text == "")
                    {
                        textBox2.Text = "0";
                    }

                }
                else if (i == 2 || i == 3)
               
                {
                    string a = veriPaketi[2] + "," + veriPaketi[3];
                    textBox3.Text = a;
                    if (textBox3.Text == "")
                    {
                        textBox3.Text = "0";
                    }
                }              
              
                else
                {
              
                     try
                     {
                         ((TextBox)panel5.Controls["textBox" + (i)]).Text = veriPaketi[i];
                     }

                     catch
                     {
                        
                            textBox4.Text = "0";
                            textBox5.Text = "0";
                            textBox6.Text = "0";
                            textBox7.Text = "0";
                            textBox8.Text = "0";
                            textBox9.Text = "0";
                            textBox10.Text = "0";
                            textBox11.Text = "0";
                            textBox12.Text = "0";
                            textBox13.Text = "0";
                            textBox14.Text = "0";
                            textBox15.Text = "0";
                            textBox16.Text = "0";
                            textBox17.Text = "0";
                            textBox18.Text = "0";
                            textBox19.Text = "0.00";
                            textBox20.Text = "0.00";
                            textBox21.Text = "0.00";
                            textBox22.Text = "0";
                            textBox23.Text = "0";
                    }

                }
            }

            // VERİLERİN EXCELE AKTARILMASI İÇİN DATAGRİDVİEWE AKTARILMASI.
            line = dataGridView1.Rows.Add();                          // Verileri excele aktarmak için atama yapılıyor.
            dataGridView1.Rows[line].Cells[0].Value = textBox1.Text;
            dataGridView1.Rows[line].Cells[1].Value = textBox2.Text;
            dataGridView1.Rows[line].Cells[2].Value = textBox3.Text;
            dataGridView1.Rows[line].Cells[3].Value = textBox4.Text;
            dataGridView1.Rows[line].Cells[4].Value = textBox5.Text;
            dataGridView1.Rows[line].Cells[5].Value = textBox6.Text;
            dataGridView1.Rows[line].Cells[6].Value = textBox7.Text;
            dataGridView1.Rows[line].Cells[7].Value = textBox8.Text;
            dataGridView1.Rows[line].Cells[8].Value = textBox9.Text;
            dataGridView1.Rows[line].Cells[9].Value = textBox10.Text;
            dataGridView1.Rows[line].Cells[10].Value = textBox11.Text;
            dataGridView1.Rows[line].Cells[11].Value = textBox12.Text;
            dataGridView1.Rows[line].Cells[12].Value = textBox13.Text;
            dataGridView1.Rows[line].Cells[13].Value = textBox14.Text;
            dataGridView1.Rows[line].Cells[14].Value = textBox15.Text;
            dataGridView1.Rows[line].Cells[15].Value = textBox16.Text;
            dataGridView1.Rows[line].Cells[16].Value = textBox17.Text;
            dataGridView1.Rows[line].Cells[17].Value = textBox18.Text;
            dataGridView1.Rows[line].Cells[18].Value = textBox19.Text;
            dataGridView1.Rows[line].Cells[19].Value = textBox20.Text;
            dataGridView1.Rows[line].Cells[20].Value = textBox21.Text;
            dataGridView1.Rows[line].Cells[21].Value = textBox22.Text;
            dataGridView1.Rows[line].Cells[22].Value = textBox23.Text;

            // GRAFİKLERE VERİ ATAMASI YAPILIYOR.
            this.chart1.Series[0].Points.AddXY(textBox2.Text, textBox10.Text);     // SICAKLIK GRAFİĞİ
            this.chart2.Series[0].Points.AddXY(textBox2.Text, textBox4.Text);      // BASINÇ GRAFİĞİ
            this.chart3.Series[0].Points.AddXY(textBox2.Text, textBox9.Text);      // İNİŞ HIZI GRAFİĞİ
            this.chart4.Series[0].Points.AddXY(textBox2.Text, textBox6.Text);      // YÜKSEKLİK GRAFİĞİ
            this.chart5.Series[0].Points.AddXY(textBox2.Text, textBox11.Text);     // VOLTAJ GRAFİĞİ

            
            
            try
            {
                xDegree = string.Format(textBox19.Text);
                yDegree = string.Format(textBox20.Text);
                zDegree = string.Format(textBox21.Text);
                xDegree = xDegree.Replace('.', ',');
                yDegree = yDegree.Replace('.', ',');
                zDegree = zDegree.Replace('.', ',');
            }
            catch
            {
                xDegree = "0.00";
                yDegree = "0.00";
                zDegree = "0.00";
                xDegree = xDegree.Replace('.', ',');
                yDegree = yDegree.Replace('.', ',');
                zDegree = zDegree.Replace('.', ',');
            }
      

        
            

            // GÖREV YÜKÜ HARİTASINA ATAMA YAPILIYOR.
            try
            {
                string gLat = string.Format(textBox12.Text);
                string gLong = string.Format(textBox13.Text);
                gLat = gLat.Replace('.', ',');
                gLong = gLong.Replace('.', ',');
                double mapGLat = Convert.ToDouble(gLat);
                double mapGLong = Convert.ToDouble(gLong);
                gMapControl1.Position = new GMap.NET.PointLatLng(mapGLat, mapGLong);
                gMapControl1.MinZoom = 10;
                gMapControl1.MaxZoom = 1000;
                gMapControl1.Zoom = 16;
                PointLatLng point = new PointLatLng();
                GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red);
                GMapOverlay markers = new GMapOverlay("markers");
                markers.Markers.Add(marker);
                gMapControl1.Overlays.Add(markers);
            }
            catch
            {
                string gLat = "0.00";
                string gLong = "0.00";
                gLat = gLat.Replace('.', ',');
                gLong = gLong.Replace('.', ',');
                double mapGLat = Convert.ToDouble(gLat);
                double mapGLong = Convert.ToDouble(gLong);
                gMapControl1.Position = new GMap.NET.PointLatLng(mapGLat, mapGLong);
                gMapControl1.MinZoom = 10;
                gMapControl1.MaxZoom = 1000;
                gMapControl1.Zoom = 16;
                PointLatLng point = new PointLatLng();
                GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red);
                GMapOverlay markers = new GMapOverlay("markers");
                markers.Markers.Add(marker);
                gMapControl1.Overlays.Add(markers);

            }

            try
            {
                string tLat = string.Format(textBox15.Text);
                string tLong = string.Format(textBox16.Text);
                tLat = tLat.Replace('.', ',');
                tLong = tLong.Replace('.', ',');
                double mapTLat = Convert.ToDouble(tLat);
                double mapTLong = Convert.ToDouble(tLong);
                gMapControl2.Position = new GMap.NET.PointLatLng(mapTLat, mapTLong);
                gMapControl2.MinZoom = 10;
                gMapControl2.MaxZoom = 1000;
                gMapControl2.Zoom = 16;
                PointLatLng point2 = new PointLatLng();
                GMapMarker marker2 = new GMarkerGoogle(point2, GMarkerGoogleType.red);
                GMapOverlay markers2 = new GMapOverlay("markers");
                markers2.Markers.Add(marker2);
                gMapControl2.Overlays.Add(markers2);

            }

            catch
            {
                string tLat = "0.00";
                string tLong = "0.00";
                tLat = tLat.Replace('.', ',');
                tLong = tLong.Replace('.', ',');
                double mapTLat = Convert.ToDouble(tLat);
                double mapTLong = Convert.ToDouble(tLong);
                gMapControl2.Position = new GMap.NET.PointLatLng(mapTLat, mapTLong);
                gMapControl2.MinZoom = 10;
                gMapControl2.MaxZoom = 1000;
                gMapControl2.Zoom = 16;
                PointLatLng point2 = new PointLatLng();
                GMapMarker marker2 = new GMarkerGoogle(point2, GMarkerGoogleType.red);
                GMapOverlay markers2 = new GMapOverlay("markers");
                markers2.Markers.Add(marker2);
                gMapControl2.Overlays.Add(markers2);


            }


            //
            try
            {
                x = Convert.ToDouble(xDegree);   // Eksen duruş modellemesi için veriler atanıp dönüştürülüyor.
                y = Convert.ToDouble(yDegree);   // Eksen duruş modellemesi için veriler atanıp dönüştürülüyor.
                z = Convert.ToDouble(zDegree);   // Eksen duruş modellemesi için veriler atanıp dönüştürülüyor.
                glControl1.Invalidate();

            }
            catch
            {
                x = 0;   // Eksen duruş modellemesi için veriler atanıp dönüştürülüyor.
                y = 0;   // Eksen duruş modellemesi için veriler atanıp dönüştürülüyor.
                z = 0;   // Eksen duruş modellemesi için veriler atanıp dönüştürülüyor.
                glControl1.Invalidate();

            }

            
            int stateINFO = Convert.ToInt32(textBox18.Text);
            int stateUydu = stateINFO + 1 ;         // Vertical Progress Bar için atama yapılıyor.
            verticalProgressBar1.Value = stateINFO + 1 ;
        }

        private void btnBaglan_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = guna2ComboBox1.Text;
                serialPort1.BaudRate = 115200;
                serialPort1.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ("Error:"));
            }
        }

        private void btnKes_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ("Error:"));
            }
        }

        private void btnAyrıl_Click(object sender, EventArgs e)
        {
            serialPort1.Write("G5*");        
        }

        private void btnTahrik_Click(object sender, EventArgs e)
        {
            serialPort1.Write("G3*");
        }     

        private void btnMDurdur_Click(object sender, EventArgs e)
        {
            serialPort1.Write("G4*");
        }

        private void btn_ARM_Click(object sender, EventArgs e)
        {
            serialPort1.Write("G1*");
        }

        private void btn_DISARM_Click(object sender, EventArgs e)
        {
            serialPort1.Write("G2*");
        }


        private void btn_LED_Click(object sender, EventArgs e)
        {
            serialPort1.Write("G6*");
        }
        private void btn_LEDOFF_Click(object sender, EventArgs e)
        {
            serialPort1.Write("G7*");

        }




        private void btnExcel_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application uyg = new Microsoft.Office.Interop.Excel.Application();
            uyg.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook kitap = uyg.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)kitap.Sheets[1];
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[1, i + 1];
                myRange.Value2 = dataGridView1.Columns[i].HeaderText;
            }

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = dataGridView1[i, j].Value;
                }
            }
        }

        private void btnVideo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = true, ValidateNames = true, Filter = "All Files|*.*" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fi = new FileInfo(ofd.FileName);
                    Username = "pi";
                    Password = "1";
                    Server = "ftp://192.168.1.245";
                    Filename = fi.Name;
                    Fullname = fi.FullName;
                }



            }

            //Upload Method.
           try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("{0}/{1}", Server, Filename)));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(Username, Password);
                Stream ftpstream = request.GetRequestStream();
                FileStream fs = File.OpenRead(Fullname);

                // Method to calculate and show the progress.
                byte[] buffer = new byte[1024];
                double total = (double)fs.Length;
                int byteRead = 0;
                double read = 0;
                do
                {
                    byteRead = fs.Read(buffer, 0, 1024);
                    ftpstream.Write(buffer, 0, byteRead);
                    read += (double)byteRead;

                    double percentage = read / total * 100;

                }
                while (byteRead != 0);
                fs.Close();
                ftpstream.Close();

            }
            catch
            {

            }



        }



        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (captureDevice.ShowDialog(this) == DialogResult.OK)
            {

                VideoCaptureDevice videoSource = captureDevice.VideoDevice;
                FinalVideo = captureDevice.VideoDevice;
                FinalVideo.NewFrame += new NewFrameEventHandler(FinalVideo_NewFrame);
                FinalVideo.Start();
            }



        }



        void FinalVideo_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (guna2Button3.Text == "KAYDI BITIR")
            {
                video = (Bitmap)eventArgs.Frame.Clone();
                pictureBox2.Image = (Bitmap)eventArgs.Frame.Clone();
                FileWriter.WriteVideoFrame(video);
            }
            else
            {
                video = (Bitmap)eventArgs.Frame.Clone();
                pictureBox2.Image = (Bitmap)eventArgs.Frame.Clone();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            saveAvi = new SaveFileDialog();
            saveAvi.Filter = "Avi Files (*.avi)|*.avi";
            if (saveAvi.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int h = captureDevice.VideoDevice.VideoResolution.FrameSize.Height;
                int w = captureDevice.VideoDevice.VideoResolution.FrameSize.Width;
                FileWriter.Open(saveAvi.FileName, w, h, 25, VideoCodec.Default, 5000000);
                FileWriter.WriteVideoFrame(video);

                guna2Button3.Text = "KAYDI BITIR";
            }

        }



        //Simülasyon kodları başlangıcı.


        private void glControl1_Load(object sender, EventArgs e)
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            float step = 1.0f;//Adım genişliği çözünürlük
            float topla = step;//Tanpon 
            float radius = 4.0f;//Yarıçap Modle Uydunun
            GL.Clear(ClearBufferMask.ColorBufferBit);//Buffer temizlenmez ise görüntüler üst üste bine o yüzden temizliyoruz.
            GL.Clear(ClearBufferMask.DepthBufferBit);

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(1.04f, 4 / 3, 1, 10000);
            Matrix4 lookat = Matrix4.LookAt(25, 0, 0, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.LoadMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.LoadMatrix(ref lookat);
            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GL.Rotate(-x, 2.0, 0.0, 0.0);
            GL.Rotate(-z, 0.0, 2.0, 0.0);
            GL.Rotate(y, 0.0, 0.0, 2.0);

            silindir(step, topla, radius, 3, -5);
            koni(0.01f, 0.01f, radius, 3.0f, 3, 4);
            koni(0.01f, 0.01f, radius, 2.0f, -5.0f, -7.0f);
            silindir(0.01f, topla, 0.07f, 9, 3);
            silindir(0.01f, topla, 0.2f, 7, 7.3f);

            silindir(0.01f, topla, 0.2f, 7.3f, 7f);
            Pervane(7.0f, 7.0f, 0.3f, 0.3f);
            GL.Begin(BeginMode.Lines);

            GL.Color3(Color.FromArgb(250, 0, 0));
            GL.Vertex3(-1000, 0, 0);
            GL.Vertex3(1000, 0, 0);

            GL.Color3(Color.FromArgb(25, 150, 100));
            GL.Vertex3(0, 0, -1000);
            GL.Vertex3(0, 0, 1000);

            GL.Color3(Color.FromArgb(0, 0, 0));
            GL.Vertex3(0, 1000, 0);
            GL.Vertex3(0, -1000, 0);

            GL.End();
            glControl1.SwapBuffers();
        }

        private void silindir(float step, float topla, float radius, float dikey1, float dikey2)
        {
            float eski_step = 0.1f;
            GL.Begin(BeginMode.Quads);//Y EKSEN CIZIM DAİRENİN
            while (step <= 360)
            {
                renk_ataması(step);
                float ciz1_x = (float)(radius * Math.Cos(step * Math.PI / 180F));
                float ciz1_y = (float)(radius * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz1_x, dikey1, ciz1_y);

                float ciz2_x = (float)(radius * Math.Cos((step + 2) * Math.PI / 180F));
                float ciz2_y = (float)(radius * Math.Sin((step + 2) * Math.PI / 180F));
                GL.Vertex3(ciz2_x, dikey1, ciz2_y);

                GL.Vertex3(ciz1_x, dikey2, ciz1_y);
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);
                step += topla;
            }
            GL.End();
            GL.Begin(BeginMode.Lines);
            step = eski_step;
            topla = step;
            while (step <= 180)// UST KAPAK
            {
                renk_ataması(step);
                float ciz1_x = (float)(radius * Math.Cos(step * Math.PI / 180F));
                float ciz1_y = (float)(radius * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz1_x, dikey1, ciz1_y);

                float ciz2_x = (float)(radius * Math.Cos((step + 180) * Math.PI / 180F));
                float ciz2_y = (float)(radius * Math.Sin((step + 180) * Math.PI / 180F));
                GL.Vertex3(ciz2_x, dikey1, ciz2_y);

                GL.Vertex3(ciz1_x, dikey1, ciz1_y);
                GL.Vertex3(ciz2_x, dikey1, ciz2_y);
                step += topla;
            }
            step = eski_step;
            topla = step;
            while (step <= 180)//ALT KAPAK
            {
                renk_ataması(step);

                float ciz1_x = (float)(radius * Math.Cos(step * Math.PI / 180F));
                float ciz1_y = (float)(radius * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz1_x, dikey2, ciz1_y);

                float ciz2_x = (float)(radius * Math.Cos((step + 180) * Math.PI / 180F));
                float ciz2_y = (float)(radius * Math.Sin((step + 180) * Math.PI / 180F));
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);

                GL.Vertex3(ciz1_x, dikey2, ciz1_y);
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);
                step += topla;
            }
            GL.End();
        }

       

        private void koni(float step, float topla, float radius1, float radius2, float dikey1, float dikey2)
        {
            float eski_step = 0.1f;
            GL.Begin(BeginMode.Lines);//Y EKSEN CIZIM DAİRENİN
            while (step <= 360)
            {
                renk_ataması(step);
                float ciz1_x = (float)(radius1 * Math.Cos(step * Math.PI / 180F));
                float ciz1_y = (float)(radius1 * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz1_x, dikey1, ciz1_y);

                float ciz2_x = (float)(radius2 * Math.Cos(step * Math.PI / 180F));
                float ciz2_y = (float)(radius2 * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);
                step += topla;
            }
            GL.End();

            GL.Begin(BeginMode.Lines);
            step = eski_step;
            topla = step;
            while (step <= 180)// UST KAPAK
            {
                renk_ataması(step);
                float ciz1_x = (float)(radius2 * Math.Cos(step * Math.PI / 180F));
                float ciz1_y = (float)(radius2 * Math.Sin(step * Math.PI / 180F));
                GL.Vertex3(ciz1_x, dikey2, ciz1_y);

                float ciz2_x = (float)(radius2 * Math.Cos((step + 180) * Math.PI / 180F));
                float ciz2_y = (float)(radius2 * Math.Sin((step + 180) * Math.PI / 180F));
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);

                GL.Vertex3(ciz1_x, dikey2, ciz1_y);
                GL.Vertex3(ciz2_x, dikey2, ciz2_y);
                step += topla;
            }
            step = eski_step;
            topla = step;
            GL.End();
        }
        private void Pervane(float yukseklik, float uzunluk, float kalinlik, float egiklik)
        {
            float radius = 10, angle = 45.0f;
            GL.Begin(BeginMode.Quads);

            GL.Color3(renk2);
            GL.Vertex3(uzunluk, yukseklik, kalinlik);
            GL.Vertex3(uzunluk, yukseklik + egiklik, -kalinlik);
            GL.Vertex3(0, yukseklik + egiklik, -kalinlik);
            GL.Vertex3(0, yukseklik, kalinlik);

            GL.Color3(renk2);
            GL.Vertex3(-uzunluk, yukseklik + egiklik, kalinlik);
            GL.Vertex3(-uzunluk, yukseklik, -kalinlik);
            GL.Vertex3(0, yukseklik, -kalinlik);
            GL.Vertex3(0, yukseklik + egiklik, kalinlik);

            GL.Color3(renk1);
            GL.Vertex3(kalinlik, yukseklik, -uzunluk);
            GL.Vertex3(-kalinlik, yukseklik + egiklik, -uzunluk);
            GL.Vertex3(-kalinlik, yukseklik + egiklik, 0.0);//+
            GL.Vertex3(kalinlik, yukseklik, 0.0);//-

            GL.Color3(renk1);
            GL.Vertex3(kalinlik, yukseklik + egiklik, +uzunluk);
            GL.Vertex3(-kalinlik, yukseklik, +uzunluk);
            GL.Vertex3(-kalinlik, yukseklik, 0.0);
            GL.Vertex3(kalinlik, yukseklik + egiklik, 0.0);
            GL.End();

        }
        private void renk_ataması(float step)
        {
            if (step < 45)
                GL.Color3(renk2);
            else if (step < 90)
                GL.Color3(renk1);
            else if (step < 135)
                GL.Color3(renk2);
            else if (step < 180)
                GL.Color3(renk1);
            else if (step < 225)
                GL.Color3(renk2);
            else if (step < 270)
                GL.Color3(renk1);
            else if (step < 315)
                GL.Color3(renk2);
            else if (step < 360)
                GL.Color3(renk1);
        }
    

    }
}

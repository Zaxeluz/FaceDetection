using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;
using System.IO;

namespace FaceDetection
{
    public partial class Form1 : Form
    {
        Image<Bgr, byte> imgInput;

        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imgInput = new Image<Bgr, byte>(dialog.FileName);
                    pictureBox1.Image = imgInput.Bitmap;
                } 
                else
                {
                    throw new Exception("Selecciona una imagen");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void detectFaceHarrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (imgInput == null)
                {
                    throw new Exception("Selecciona una imagen");
                }

                DetectFaceHaar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DetectFaceHaar()
        {
            try
            {
                string facePath = Path.GetFullPath(@"../../Haar/haarcascade_frontalface_default.xml");

                CascadeClassifier classifierFace = new CascadeClassifier(facePath);

                var imgGray = imgInput.Convert<Gray, byte>().Clone();
                Rectangle[] faces = classifierFace.DetectMultiScale(imgGray, 1.1, 4);
                foreach(var face in faces)
                {
                    imgInput.Draw(face, new Bgr(0, 0, 255), 2);
                }
                pictureBox1.Image = imgInput.Bitmap;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}

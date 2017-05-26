using AForge.Imaging;
using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace ImUENP.OCR
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Image = System.Drawing.Image.FromFile(openFileDialog.FileName);
            }
        }
        private void processOcr(Bitmap img, String texto) {
            var datapath = @"tessdata";
            var lang = "por";
            var ocr = new TesseractEngine(datapath, lang, EngineMode.TesseractOnly);
            var page = ocr.Process(img);
            
            txtText.Text += texto + ": " +page.GetText() + Environment.NewLine;
        }
        private void btnOCR_Click(object sender, EventArgs e)
        {
            txtText.Clear();
            var img = (Bitmap) pictureBox.Image;

            img = new BrightnessCorrection(50).Apply(img);
            img = new ContrastCorrection(90).Apply(img);
            //Cinza
           img = Grayscale.CommonAlgorithms.Y.Apply(img);
            //Mediana
            //img = new Median(2).Apply(img);
            //Otsu
            // img = new Threshold(225).Apply(img);
            img = new OtsuThreshold().Apply(img);


            //   img = new Mean().Apply(img);

            // img = new Dilatation().Apply(img);
            //img = new Opening().Apply(img);

           

            Bitmap imga= new Bitmap(img,new Size(785,492));

            //MessageBox.Show("" + imga.Width);

            pictureBox.Image = imga;
            pictureBox.Refresh();

            int xNomeOriginal = 39;
            int yNomeOriginal = 147;

            int xNome = (xNomeOriginal * img.Width) / 785;
            int yNome = (yNomeOriginal * img.Height) / 492;
      
            var retanguloNome = new Rectangle(xNome, yNome, 698, 82);
            //var retanguloNome = new Rectangle(51, 150, 698, 82);
            Bitmap nome = imga.Clone(retanguloNome, imga.PixelFormat);
            nome.Save(@"C:\Users\ProBook\Desktop\teste imgs\nome.jpg");
            processOcr(nome, "NOME");

            int xDataNascimentoOriginal = 39;
            int yDataNascimentoOriginal = 266;

            int xDataNascimento = (xDataNascimentoOriginal * img.Width) / 785;
            int yDataNascimento = (yDataNascimentoOriginal * img.Height) / 492;

            var retanguloDataNascimento = new Rectangle(xDataNascimento, yDataNascimento, 200, 32);
            Bitmap dataDeNascimento = imga.Clone(retanguloDataNascimento, imga.PixelFormat);
            dataDeNascimento.Save(@"C:\Users\ProBook\Desktop\teste imgs\dataDeNascimento.jpg");
            processOcr(dataDeNascimento,"DATA DE NASCIMENTO");

            int xNumInscricaoOriginal = 259;
            int yNumInscricaoOrignal = 266;

            int xNumInscricao = (xNumInscricaoOriginal * img.Width) / 785;
            int yNumInscricao = (yNumInscricaoOrignal * img.Height) / 492; 
            var retanguloNEscricao = new Rectangle(xNumInscricao,yNumInscricao, 286, 32);
            Bitmap nEscricao = imga.Clone(retanguloNEscricao, imga.PixelFormat);
            processOcr(nEscricao,"Nº INSCRIÇÃO");
            nEscricao.Save(@"C:\Users\ProBook\Desktop\teste imgs\ninscricao.jpg");

            int xZonaOriginal = 565;
            int yZonaOriginal = 266;

            int xZona = (xZonaOriginal * img.Width) / 785;
            int yZona = (yZonaOriginal * img.Height) / 492;

            var retanguloZona = new Rectangle(xZona, yZona, 68, 32);
            Bitmap zona = imga.Clone(retanguloZona, imga.PixelFormat);
            processOcr(zona, "ZONA");
            zona.Save(@"C:\Users\ProBook\Desktop\teste imgs\zona.jpg");

            int xSecaoOriginal = 664;
            int ySecaoOriginal = 266;

            int xSecao = (xSecaoOriginal * img.Width) / 785;
            int ySecao = (ySecaoOriginal * img.Height) / 492;
            var retanguloSecao = new Rectangle(xSecao, ySecao, 77, 32);
            Bitmap secao = imga.Clone(retanguloSecao, imga.PixelFormat);
            processOcr(secao,"SEÇÃO");
            secao.Save(@"C:\Users\ProBook\Desktop\teste imgs\secao.jpg");

            int xMunicipioOriginal = 38;
            int yMunicipioOriginal = 329;

            int xMunicipio = (xMunicipioOriginal * img.Width) / 785;
            int yMunicipio = (yMunicipioOriginal * img.Height) / 492;
            var retanguloMunicipio = new Rectangle(xMunicipio, yMunicipio, 502, 34);
            Bitmap municipio = imga.Clone(retanguloMunicipio, imga.PixelFormat);
            processOcr(municipio, "MUNICIPIO");
            municipio.Save(@"C:\Users\ProBook\Desktop\teste imgs\muncipio.jpg");

            int xDataEmissaoOriginal = 544;
            int yDataEmissaoOriginal = 330;

            int xDataEmissao = (xDataEmissaoOriginal * img.Width) / 785;
            int yDataEmissao = (yDataEmissaoOriginal * img.Height) / 492;
            var retanguloDataEmissao = new Rectangle(xDataEmissao, yDataEmissao, 194, 34);
            Bitmap dataEmissao = imga.Clone(retanguloDataEmissao, imga.PixelFormat);
            processOcr(dataEmissao, "DATA DE EMISSÃO");
            dataEmissao.Save(@"C:\Users\ProBook\Desktop\teste imgs\dataemissao.jpg");
            // img = new BinaryDilatation3x3(0,0,0,0,1,0,1,0,0).Apply(img);
            //img = new Mor
            //img.RotateFlip(RotateFlipType.Rotate270FlipNone);





        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var img = (Bitmap)pictureBox.Image;
            img = new Invert().Apply(img);
            BlobCounter bc = new BlobCounter();
            bc.ProcessImage(img);
            var blobs = bc.GetObjectsRectangles();
            MessageBox.Show("Objetos: " + blobs.Length);
        }
    }
}

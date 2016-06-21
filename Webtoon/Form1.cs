using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace Webtoon
{
    public partial class Form1 : Form
    {
        LinkedList<string> outputstrs;
        string tempImagePath;
        string path;
        string URL;
        string outputstr;
        int startNum;
        int endNum;
        int resultCount;
       
        public Form1()
        {
            InitializeComponent();
        }

        private void init()
        {
            tempImagePath = "";
            path = "";
            URL = "";
            startNum = 0;
            endNum = 0;
            resultCount = 0;
            progressBar.Value = 0;
            outputstrs = new LinkedList<string>();
            
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (URLTextBox.Text.Equals(""))
            {
                MessageBox.Show("URL을 지정해주세요");
                return;
            }else if (folderPath.Text.Equals(""))
            {
                MessageBox.Show("폴더경로를 지정해주세요");
                return;
            }
            
            init();
            URL = URLTextBox.Text;

            try {
                startNum = int.Parse(startPage.Text);
                endNum = int.Parse(endPage.Text);
                resultCount = startNum;
            }catch(Exception ex)
            {
                MessageBox.Show("잘못된 문자가 있습니다."+ex.ToString());
                return;
            }

            progressBar.Maximum = endNum - startNum+1;
            progressBar.Minimum = 0;

            path = folderPath.Text + "\\";
            tempImagePath =path+"temp";
            if (!Directory.Exists(tempImagePath))
                System.IO.Directory.CreateDirectory(tempImagePath);
            tempImagePath += "\\";
            tempFolderFilesRemove();

            new Thread(new ThreadStart(mainloop)).Start();

        }

        /*폴더지정*/
        private void folderSelect_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.ShowDialog();
            folderPath.Text = FBD.SelectedPath;
        }

        /*파일 다운로드*/
        /*URL 변형*/
        private void URLEdit(ref string url, int num)
        {
            int startidx = url.IndexOf("&no=");
            url = url.Remove(startidx);
            url += "&no=" + num.ToString();

        }
        /*웹페이지의 request반환*/
        private string getWebRequest(string webURL)
        {
            WebRequest request = WebRequest.Create(webURL);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            
            return reader.ReadToEnd();
        }
        /*웹페이지에서 웹툰컷들 다운로드*/
        private void pageDownload(string fullString, string start = "imgcomic.", string end = ".jpg")
        {
            int count = 0;
            int count2 = 0;
            int startidx = 0;
            int endidx = 0;
            String str = fullString;
            WebClient WB = new WebClient();

            char[] tempstr = new char[str.Length];

            while (true)
            {
                startidx = str.IndexOf("imgcomic.");
                do
                {
                    endidx = str.IndexOf(".jpg");

                    if (startidx > endidx && endidx!=-1)
                    {
                        tempstr = str.ToArray<char>();
                        tempstr[endidx] = ' ';
                        str = new String(tempstr);
                    }

                } while (endidx < startidx && endidx != -1 && startidx != -1);


                if (startidx != -1 && endidx != -1)
                {
                    string tempURL = str.Substring(startidx, endidx + 4 - startidx);
                    count++;
                    if (count == 10)
                    {
                        count2++;
                        count -= 10;
                    }
                    try {
                        WB.DownloadFile("http://" + tempURL, count2.ToString() + count.ToString() + ".jpg");
                    }catch(Exception e)
                    {
                        MessageBox.Show("파일 다운로드중 오류 발생!"+e.Message);

                    }
                }
                else
                    break;

                tempstr = str.ToArray<char>();
                for (int i = startidx; i < endidx; i++)
                    tempstr[i] = '!';
                str = new String(tempstr);

            }
  
            DirectoryInfo direcinfo = new DirectoryInfo(System.Environment.CurrentDirectory);
            FileInfo[] fileinfo = direcinfo.GetFiles("*.jpg");
            foreach(FileInfo f in fileinfo)
                File.Move(f.FullName, tempImagePath + f.Name);
        }
        //////////////파일다운로드//////////

        /*이미지 만들기*/
        private void makeImage()
        {
            DirectoryInfo di = new DirectoryInfo(path + "temp");
            FileInfo[] files = di.GetFiles("*.jpg");
            List<Image> images = new List<Image>();
            List<Bitmap> result = new List<Bitmap>();
            List<Graphics> gra = new List<Graphics>();
            const int cutmaxSize = 50000;
            int heightSum = 0;
            int widthSum = 0;

            foreach (FileInfo f in files)
            {
                images.Add(Image.FromFile(f.FullName));
                heightSum += images.LastOrDefault().Height;
                
            }
            files = null;
            di = null;
            

            if (images.Count == 0)
                return;
            widthSum = images[0].Width;

            //이미지 사이즈 할당
            
            int tempHeightSum = heightSum;
            Bitmap tempbmp;
            while (cutmaxSize < tempHeightSum)
            {
                tempbmp = new Bitmap(widthSum, cutmaxSize);
                result.Add(tempbmp);
                gra.Add(Graphics.FromImage(tempbmp));
                tempHeightSum -= cutmaxSize;
               
            }
            tempbmp = new Bitmap(widthSum, tempHeightSum);
            result.Add(tempbmp);
            gra.Add(Graphics.FromImage(tempbmp));
            tempbmp = null;
          

            int gount = 0;
            tempHeightSum = 0;
            for (int i= 0; i < images.Count; i++)
            {
                gra[gount].DrawImage(images[i], 0, tempHeightSum);
                tempHeightSum += images[i].Height;
                
                if (tempHeightSum > cutmaxSize)
                {
                    tempHeightSum -= images[i].Height+cutmaxSize;
                    gount++;
                    i--;
                }
                
            }
            gount = 1;
            foreach (Bitmap b in result)
            {
                b.Save(path + resultCount.ToString() + "_" + gount.ToString() + ".jpg", ImageFormat.Jpeg);
                b.Dispose();
                gount++;
            }
            foreach(Image img in images)
            {
                img.Dispose();
            }
            foreach(Graphics gr in gra)
            {
                gr.Dispose();
            }

            resultCount++;
            result.Clear();
            gra.Clear();
            images.Clear();

            GC.Collect();

            
        }
        /*임시파일 삭제*/
        private void tempFolderFilesRemove()
        {
            DirectoryInfo di = new DirectoryInfo(path + "temp");
            FileInfo[] files = di.GetFiles("*.jpg");
            
            foreach (FileInfo f in files)
                f.Delete();

            di = null;

        }

        private void mainloop()
        {
            for (int pageNum = startNum; pageNum <= endNum; pageNum++)
            {
                
                URLEdit(ref URL, pageNum);
                pageDownload(getWebRequest(URL));
                makeImage();
                tempFolderFilesRemove();
                progressBar.Value += 1;

                if (outputstrs.Count < 20)
                    outputstrs.AddLast(pageNum.ToString() + "\n");
                else
                {
                    outputstrs.RemoveFirst();
                    outputstrs.AddLast(pageNum.ToString() + "\n");
                }
                outputstr = "";
                foreach(string s in outputstrs)
                {
                    outputstr += s; 
                }
                output.Text = outputstr;
                
            }
            MessageBox.Show("끝!");

        }

        //웹페이지로 뷰어 만들기
        private void button1_Click(object sender, EventArgs e)
        {
           
            if (folderPath.Text.Equals(""))
                return;
            path = folderPath.Text;
            DirectoryInfo di = new DirectoryInfo(path);
            for(int i = 1; di.GetFiles(i.ToString() + "_?.jpg").Count() != 0; i++)
            {
                FileInfo[] files = di.GetFiles(i.ToString() + "_*.jpg");
                using (FileStream fs = new FileStream(path+"/" + i.ToString() + "Page.html", FileMode.OpenOrCreate))
                {
                    StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                    sw.Write("<html><head><title>"+i.ToString()+"화</title><style>img{width: 100%;height: auto;}</style></head><body bgcolor=#000000>");
                    foreach(FileInfo fi in files)
                    {
                        sw.Write("<img src="+fi.Name+"><br>");
                    }
                    sw.Write(
                        " <font size=7><a href=" + (i - 1).ToString() + "Page.html> <strong>이전화</strong> </a> &nbsp &nbsp <font color=#ffffff>"+i.ToString()+ "화 </font> &nbsp &nbsp"
                        + "<a href=" + (i + 1).ToString() + "Page.html> <strong>다음화</strong> </a> </body> </html>"
                        );
                    sw.Flush();
                }
                


            }
            MessageBox.Show(path+"Active!");
        
       }
        
    }
}
rel="stylesheet"
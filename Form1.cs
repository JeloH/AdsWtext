
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.Data.OleDb;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;

using HtmlAgilityPack;


namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {



        public static int gener = 500;
        public static double CrossOver = 0.7;
        public static double Mution = 0.5;
        public static int Pouple = 500;
        public static int Genem = 30;

        public Form1()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Main();
        }


        public static double theActualFunction2(params double[] values)
        {
            //    double result=0;
            //int[] coeficients=new int[100];
            //coeficients[0] = -5;
            //coeficients[0] = -7;
            //coeficients[0] = 100;
            // //..........
            //coeficients[99] = 100;
            // for(int i=0;i<100;i++)
            // {
            //     int x = values[i] > 0.5 ? 1 : 0;
            //      result +=( coeficients[i]*x);
            // }

            // return result;

            int x0 = values[0] > 0.5 ? 1 : 0;
            int x1 = values[1] > 0.5 ? 1 : 0;
            int x2 = values[2] > 0.5 ? 1 : 0;
            int x3 = values[3] > 0.5 ? 1 : 0;
            int x4 = values[4] > 0.5 ? 1 : 0;
            int x5 = values[5] > 0.5 ? 1 : 0;
            int x6 = values[6] > 0.5 ? 1 : 0;
            int x7 = values[7] > 0.5 ? 1 : 0;
            int x8 = values[8] > 0.5 ? 1 : 0;
            int x9 = values[9] > 0.5 ? 1 : 0;

            double f1 = ((x0 * -5) + (x1 * -7) + (x2 * 100) + (x3 * -15) + (x4 * -8) + (x5 * 100) + (x6 * -15) + (x7 * -15) + (x8 * 100) + (x9 * 100));

            return f1;
        }


        public static int[] coeficients = new int[Form1.Genem];


        public static double theActualFunction(double[] values)
        {
            double result = 0;

            /*
                        coeficients[0] = 100;
                        coeficients[1] = -7;
                        coeficients[2] = -4;
                        coeficients[3] = -8;
                        coeficients[4] = -4;
                        coeficients[5] = -4;
                        coeficients[6] = -4;
                        coeficients[7] = -8;
                        coeficients[8] = -4;
                        coeficients[9] = -8;  
                        coeficients[10] = -7;
                        coeficients[11] = -7;
                        coeficients[12] = -4;
                        coeficients[13] = -8;
                        coeficients[14] = -8;
                        coeficients[15] = -4;
                        coeficients[16] = -4;
                        coeficients[17] = -8;
                        coeficients[18] = -4;
                        coeficients[19] = -8;
                        coeficients[20] = -8;
                        coeficients[21] = -7;
                        coeficients[22] = -4;
                        coeficients[23] = -8;
                        coeficients[24] = -4;
                        coeficients[25] = -4;
                        coeficients[26] = -4;
                        coeficients[27] = -8;
                        coeficients[28] = -4;
                        coeficients[29] = -8; 
                     //   coeficients[99] = 100;
                     //   for (int j = 10; j < 98; j++)
                     //   {
                            coeficients[j] = -4;
                        }

                        */


            //..........
            //  coeficients[99] = 100;
            for (int i = 0; i < Form1.Genem; i++)
            {
                int x = values[i] > 0.5 ? 1 : 0;
                result += (coeficients[i] * x);
            }

            return result;

        }



        public void Main()
        {
            GA ga = new GA(Form1.CrossOver, Form1.Mution, Form1.Pouple, Form1.gener, Form1.Genem);
            ga.FitnessFunction = new GAFunction(theActualFunction);


            ga.Elitism = true;
            ga.Go();

            double[] values;
            double fitness;

            List<Genome> allgens = new List<Genome>();
            int x0 = 1;
            string result = "";
            string temp2 = "";

            ga.GetBest(out values, out fitness, out allgens);
            //   System.Console.WriteLine("\nall genoms :\n");
            foreach (Genome g in allgens)
            {
                double[] tempvalues = new double[g.Length];
                g.GetValues(ref tempvalues);
                foreach (double valeur in tempvalues)
                    result += string.Format("{0} ", valeur > 0.5 ? 1 : 0);
                x0 += 1;

            //    textBox6.Text = result + "  " + theActualFunction(tempvalues) + "round:" + x0 + "|||";
                temp2 = result + "  " + theActualFunction(tempvalues) + "round:" + x0 + "|||";
                 
                this.Text = x0.ToString();
                // System.Console.Write("   value= {0} ", theActualFunction(tempvalues));
                // System.Console.WriteLine("\n");

                result = "";

            }

            textBox6.Text += temp2;

            textBox7.Text = fitness.ToString(); ;


        }

        private void button2_Click(object sender, EventArgs e)
        {

            listBox1.Items.Clear();

            var accountKey = "EMdPzpSl/qTbvz6/ScfE5sFLzuxNfU/crNSmIYjQDes=";

            var bingContainer = new Bing.BingSearchContainer(new Uri("https://api.datamarket.azure.com/Bing/Search"));

            bingContainer.Credentials = new NetworkCredential(accountKey, accountKey);
            var webQuery = bingContainer.Web(txtSearch.Text.ToString(), null, null, null, null, null, null, null);
            webQuery = webQuery.AddQueryOption("$top", 30);
            var webResults = webQuery.Execute();
            foreach (var result in webQuery)
            {
                listBox1.Items.Add(ConvertUrlsToLinks(result.Url));

                textBox6.Text +=  result.Url  + Environment.NewLine;

                //listBox5.Items.Add(result.Url);

            }




        }












        private void button1_Click_1(object sender, EventArgs e)
        {
             backgroundWorker3.RunWorkerAsync();

        }

        int c_k = 0;
        string c_d = "";
        string link_count = "", txthtml = "";
        private void button3_Click(object sender, EventArgs e)
        {
            //label12.Text = listBox1.Items.Count.ToString();
           // listBox3.Items.Clear();

          
            backgroundWorker1.RunWorkerAsync();
            
           // backgroundWorker3.RunWorkerAsync();
           




        }


        public void run2()
        {


           // listBox4_blk.Items.Clear();

            // calcu get cost/size keyword | for example www.taiol.ir= 400
            //  textBox1.Text = key_web(AcquireHTML(textBox2.Text));
            for (int i = 0; i < listBox1.Items.Count; i++)
            {



                int keyword_firstpage = 0;
                int Feature_Url = 0;

                txthtml = AcquireHTML(listBox1.Items[i].ToString());

                //بررسی وجود کلمات کلیدی تبلیغاتی در توضیحات وب سایت
                // c_k = info_link_count(key_web(txthtml));

                //بررسی وجود کلمات کلیدی در صفحه اول
                // c_k += info_link_count(txthtml);
                c_k = 0;

                // feature url
                //بررسی ویژگی های یو آر ال

                Feature_Url = info_link_url(listBox1.Items[i].ToString());

                // c_k= in_lik(txthtml).ToString();




                // calcu get cost/size description website | for example www.taiol.ir= 290

                // بررسی و محاسبه تعداد کلمات کلیدی در صفحه اول
                keyword_firstpage += TextTool.CountStringOccurrences(txthtml, "تبلیغات");
                keyword_firstpage += TextTool.CountStringOccurrences(txthtml, "آگهی");
                keyword_firstpage += TextTool.CountStringOccurrences(txthtml, "رایگان");
                keyword_firstpage += TextTool.CountStringOccurrences(txthtml, "تومان");
                keyword_firstpage += TextTool.CountStringOccurrences(txthtml, "خرید");
                keyword_firstpage += TextTool.CountStringOccurrences(txthtml, "درج آگهی");
                keyword_firstpage += TextTool.CountStringOccurrences(txthtml, "برنده");
                keyword_firstpage += TextTool.CountStringOccurrences(txthtml, " تبلیغات رایگان");
                keyword_firstpage += TextTool.CountStringOccurrences(txthtml, "فروش");
                keyword_firstpage += TextTool.CountStringOccurrences(txthtml, "ماهانه فقط");
                //j += TextTool.CountStringOccurrences(AcquireHTML(listBox1.Items[i].ToString()), "پردا");
                txthtml = "";



                // check is or is'nt website in blacklist | for example www.taiol.ir= false



                ///clacu get count of local link each link
                ///
                string g = "null";

                //  search2("site:" + listBox1.Items[i].ToString() + " " + "تومان + آگهی + تبلیغات + خرید", listBox1.Items[i].ToString() ,out g);

                link_count = g;




                 //textBox8.Text += i + ":" + listBox1.Items[i].ToString() + ": " + keyword_firstpage + "-" + c_k + Environment.NewLine;

                //  listBox4.Items.Insert(i, j +" - "+ int.Parse(c_k) + " - " + link_count);


                // check in black list

                int blk=0;


                blk = chech_blk_list(listBox1.Items[i].ToString());

                textBox6.Text += i + ":" + listBox1.Items[i].ToString() + " --->      keyword_firstpage:" + keyword_firstpage + " blk_list:" + blk + " Feature_Url:" + Feature_Url + Environment.NewLine;

               

  
                c_adv(keyword_firstpage, c_k, Feature_Url, i, listBox1.Items[i].ToString(),blk);

                //  j.ToString(), c_k.ToString(), link_count, link_feature

                // c_adv(c_k,j,i);

                //if (c_k > 70 & j > 30 || j > 70 & c_k > 30)
                //{

                //     listBox4.Items.Insert(i, listBox1.Items[i].ToString());
                // textBox8.Text += i + ":" + listBox1.Items[i].ToString() + ": " + j + "-" + c_k + Environment.NewLine;

                //   }

               // string blk = "0";
               //  insert_gt(i.ToString(), listBox1.Items[i].ToString(), keyword_firstpage.ToString(), "", Feature_Url,"");

           //   insert_gt(i.ToString(), listBox1.Items[i].ToString(), blk.ToString(), keyword_firstpage.ToString(),Feature_Url.ToString(),"0");


               // label1.Text = i2 + "";

                // c_k = 0;



            }

        }


        public int chech_blk_list(string name_link)
        {

            int blk = 0;

            for (int i = 0; i < Class1.blk_list_array.Length; i++)
            {

                name_link= name_link.Replace("http://www.", "");

                name_link= name_link.Replace("www.", "");

                name_link = name_link.Replace("http://", "");

                name_link = name_link.Replace("/", "");

                name_link = name_link.Replace(".com", "");
                name_link = name_link.Replace(".ir", "");
                name_link = name_link.Replace(".net", "");
                name_link = name_link.Replace(".org", "");
                name_link = name_link.Replace(".biz", "");
                name_link = name_link.Replace(".in", "");

                this.Text = i.ToString();


                
                if ( Class1.blk_list_array[i].Contains(name_link)==true)
                {

                    blk = 1;
                   // listBox3.Items.Add("is blk: " +name_link);

                 //   MessageBox.Show("is blk");

                    i = 1000; // break
                }



                
            }

            return blk;
        }



        public int fitnes_calcu()
        {

            return 0;
        }

        int ci = 1;

        public void c_adv(int keyword_firstpage, int is_key, int Feature_Url, int i, string link, int blk_list)
        {
            coeficients[i] = -4;


            int cost = fitnes_calcu();

            if (keyword_firstpage > 55)
            {
                ci = 30;

            }
              if (is_key > 700)
            {
                ci += 30;
            }

              if (Feature_Url < 1)
            {
                ci = -30;
            }

                if (Feature_Url > 30)
            {
                ci = 100;
            }


              if (ci >= 90)
            {

                listBox3.Items.Add( link + " Score: " + keyword_firstpage + " - " + is_key + " - " + Feature_Url);
                coeficients[i] = 85;
                
            }


              if (blk_list == 1)
            {

                listBox3.Items.Add(link + " Score: " + keyword_firstpage + " - " + is_key + " - " + Feature_Url);
              coeficients[i] = 99;
                
            }




            ci = 0;


        }


        public void insert_gt(string code, string name_link, string blk_list_check, string key_first_page, string link_feature, string link_internal)
        {

            string str1 = "";
            str1 = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Directory.GetCurrentDirectory() + "\\db2.mdb;Persist Security Info=True";

            
            OleDbConnection ol = new OleDbConnection(str1);

            OleDbCommand cmd = new OleDbCommand("insert into Final_table_100query2(name_link,blk_list_check,key_first_page,link_feature,link_internal)Values( '" + name_link + "','" + blk_list_check + "','" + key_first_page + "','" + link_feature + "','" + link_internal + "')", ol);

            ol.Open();
            cmd.ExecuteReader();
            ol.Close();


        }



        public void search2(string str_search, string str_search2, out string r2)
        {

            r2 = "";

            try
            {


                var doc = new HtmlWeb().Load("http://www.google.com/search?q=" + str_search);
                var div = doc.DocumentNode.SelectSingleNode("//div[@id='resultStats']");
                var text = div.InnerHtml.ToString();
                //     TextBox2.Text = div.ToString();



                Match match = Regex.Match(text, @"About ([0-9,]+)");




                //    textBox8.Text += match.Groups[1].Value + " -> " + str_search2 + Environment.NewLine;






                r2 = match.Groups[1].Value;

            }
            catch (Exception)
            {

                string i2 = "";

                i2 = "d";

            }
            //   var matches = Regex.Matches(text, @"About ([0-9,]+) ");
            //  var total = matches.Groups[1].Value;

            // Label1.Text = total;

            //  ListBox2.Items.Add(total);



        }



        public string metaTitle;
        public string metaDescription;
        public string metaKeywords;

        public bool GetMetaTags(string url)
        {
            try
            {
                //get the HTML of the given page and put into a string
                string html = AcquireHTML(url);

                if (GetMeta(html))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // do something with the error
                return false;
            }
        }

        private string AcquireHTML(string address)
        {
            HttpWebRequest request;
            HttpWebResponse response = null;
            StreamReader reader;
            StringBuilder sbSource;

            try
            {
                // Create and initialize the web request  
                request = System.Net.WebRequest.Create(address) as HttpWebRequest;
                request.UserAgent = "your-search-bot";
                request.KeepAlive = false;
                request.Timeout = 10 * 1000;

                // Get response  
                response = request.GetResponse() as HttpWebResponse;

                if (request.HaveResponse == true && response != null)
                {
                    // Get the response stream  
                    reader = new StreamReader(response.GetResponseStream());

                    // Read it into a StringBuilder  
                    sbSource = new StringBuilder(reader.ReadToEnd());

                    response.Close();

                    // Console application output  
                    return sbSource.ToString();
                }
                else
                    return "";
            }
            catch (Exception ex)
            {

                // response.Close();
                return "";
            }
        }

        private bool GetMeta(string strIn)
        {
            try
            {
                // --- Parse the title
                Match TitleMatch = Regex.Match(strIn, "<title>([^<]*)</title>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                metaTitle = TitleMatch.Groups[1].Value;

                // --- Parse the meta keywords
                Match KeywordMatch = Regex.Match(strIn, "<meta name=\"keywords\" content=\"([^<]*)\">", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                metaKeywords = KeywordMatch.Groups[1].Value;

                // --- Parse the meta description
                Match DescriptionMatch = Regex.Match(strIn, "<meta name=\"description\" content=\"([^<]*)\">", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                metaDescription = DescriptionMatch.Groups[1].Value;

                return true;
            }
            catch (Exception ex)
            {
                // do something with the error
                return false;
            }
        }




        string count_index_key = "";
        int i2 = 0;

        public string key_web(string str_search)
        {

            try
            {

                var text = str_search;


                //   textBox2.Text = div.ToString();

                Match match = Regex.Match(text, "<meta name=\"description\" content=\"([^<]*)\"([^<]*)>", RegexOptions.IgnoreCase | RegexOptions.Multiline); ;


                if (match.Success)
                {

                    count_index_key = match.Groups[1].Value + " -> ";
                    //     count_index_key += count_index_key.Replace(",", "");
                }

            }
            catch (Exception)
            {


                return "";


            }


            return count_index_key;

        }







        public int info_link_count(string Address_link_d)
        {


            string text_info = Address_link_d;

            int cost_info_link = 1;


            cost_info_link += search_key(text_info, "تبلیغات", 20);
            cost_info_link += search_key(text_info, "آگهی", 20);
            cost_info_link += search_key(text_info, "رایگان", 10);
            cost_info_link += search_key(text_info, "خرید", 5);
            cost_info_link += search_key(text_info, "فروش", 5);
            cost_info_link += search_key(text_info, "تومان", 5);
            cost_info_link += search_key(text_info, "درج آگهی", 25);
            cost_info_link += search_key(text_info, "نیازمندی", 15);






            return cost_info_link;

        }


        public int info_link_url(string Address_link_d)
        {

            string text_info = Address_link_d;

            int cost_info_link = 1;


            cost_info_link += search_key(text_info, "agahi", 70);
            cost_info_link += search_key(text_info, "niaz", 70);
            cost_info_link += search_key(text_info, "bazar", 40);
            cost_info_link += search_key(text_info, "shop", 60);
            cost_info_link += search_key(text_info, "market", 60);
            cost_info_link += search_key(text_info, "forush", 70);
            cost_info_link += search_key(text_info, "forosh", 70);
            cost_info_link += search_key(text_info, "tabligh", 75);
            cost_info_link += search_key(text_info, "payam", 30);
            cost_info_link += search_key(text_info, "kala", 30);

            cost_info_link += search_key(text_info, "bank", -10);
            cost_info_link += search_key(text_info, "news", -50);
            cost_info_link += search_key(text_info, "download", -40);
            cost_info_link += search_key(text_info, "music", -70);
            cost_info_link += search_key(text_info, "khabar", -30);



            return cost_info_link;

        }




        public int search_key(string text_info, string skey, int cost)
        {


            if (text_info.Contains(skey) == true)
            {

                cost += 2;
            }

            else
            {
                cost = 0;

            }

            return cost;

        }


        public static class TextTool
        {
            /// <summary>
            /// Count occurrences of strings.
            /// </summary>
            /// int i2 = 0;

            static int i2 = 0;


            public static int CountStringOccurrences(string text, string pattern)
            {
                

                string[] keywords =pattern.Split("".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                if (SearchKeywords(text, keywords) == true)
                {

                    i2 += 1;

                }
                  
                
                return i2;
            }









        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            MessageBox.Show("Obtained the score for advertisement website discovery ");

            coeficients[0] = -4;
            coeficients[1] = -4;
            coeficients[2] = -4;
            coeficients[3] = -4;
            coeficients[4] = -4;
            coeficients[5] = -4;
            coeficients[6] = -4;
            coeficients[7] = -4;
            coeficients[8] = -4;
            coeficients[9] = -4;

            coeficients[10] = -4;
            coeficients[11] = -4;
            coeficients[12] = -4;
            coeficients[13] = -4;
            coeficients[14] = -4;
            coeficients[15] = -4;
            coeficients[16] = -4;
            coeficients[17] = -4;
            coeficients[18] = -4;
            coeficients[19] = -4;

            coeficients[20] = -4;
            coeficients[21] = -4;
            coeficients[22] = -4;
            coeficients[23] = -4;
            coeficients[24] = -4;
            coeficients[25] = -4;
            coeficients[26] = -4;
            coeficients[27] = -4;
            coeficients[28] = -4;
            coeficients[29] = -4;
        
            run2();



          ///  MessageBox.Show("Step2: Run Genetic Algorithem");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string g = "";

            listBox3.Items.Clear();

            // calcu get cost/size keyword | for example www.taiol.ir= 400
            //  textBox1.Text = key_web(AcquireHTML(textBox2.Text));
            for (int i = 0; i < listBox1.Items.Count; i++)
            {

                search2("site:" + listBox1.Items[i].ToString() + " " + "تومان + آگهی + تبلیغات + خرید", listBox1.Items[i].ToString(), out g);

                textBox6.Text += g + Environment.NewLine;

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {

            backgroundWorker2.RunWorkerAsync();

        }

        public void get_max100(int index_gen3)
        {



            //connection = new SqlConnection(...); 

            OleDbConnection CN = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\Users\12\Documents\Visual Studio 2010\Projects\WindowsFormsApplication5\WindowsFormsApplication5\bin\Debug\db2.mdb;Persist Security Info=True");

            OleDbCommand cmd = new OleDbCommand("select * from d_2_Alexa_100 where code=" + index_gen3, CN);

            CN.Open();

            OleDbDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                //dis_a,key_a,link_feature
                int dis_a = int.Parse(dr["dis_a"].ToString());
                int key_a = int.Parse(dr["key_a"].ToString());
                string link_feature = dr["link_feature"].ToString();
                string link = dr["link"].ToString();

                c_adv(dis_a, key_a, int.Parse(link_feature), index_gen3, link,0);


            }



            //all other stuff goes here


            CN.Close(); //This will always close the connection, even with exceptions.





        }


        private string ConvertUrlsToLinks(string msg)
        {

            Uri originalUrl = new Uri(msg); // Request.Url
            string domain = originalUrl.Host; // www.mydomain.com
            string domainUrl = String.Concat(originalUrl.Scheme, Uri.SchemeDelimiter, originalUrl.Host); // http://www.mydomain.com



            return domainUrl;
        }








        public static int[] GetKmpNext(string pattern)
        {
            int[] next = new int[pattern.Length];
            next[0] = -1;
            if (pattern.Length < 2) return next;
            next[1] = 0;
            int i = 2, j = 0;
            while (i < pattern.Length)
            {
                if (pattern[i - 1] == pattern[j])
                {
                    next[i++] = ++j;
                }
                else
                {
                    j = next[j];
                    if (j == -1)
                    {
                        next[i++] = ++j;
                    }
                }
            }
            return next;
        }
        /// <summary>  
        ///   
        /// </summary>  
        /// <param name="source"></param>  
        /// <param name="keywords">|</param>  
        /// <returns>true?false?</returns>  
        public static bool SearchKeywords(string source, string[] keywords)
        {
            int wordCount = keywords.Length;
            int[][] nexts = new int[wordCount][];
            int i = 0;
            for (i = 0; i < wordCount; i++)
            {
                nexts[i] = GetKmpNext(keywords[i]);
            }
            i = 0;
            int[] j = new int[nexts.Length];
            while (i < source.Length)
            {
                for (int k = 0; k < wordCount; k++)
                {
                    if (source[i] == keywords[k][j[k]])
                    {
                        j[k]++;
                    }
                    else
                    {
                        j[k] = nexts[k][j[k]];
                        if (j[k] == -1)
                        {
                            j[k]++;
                        }
                    }
                    if (j[k] >= keywords[k].Length)
                    {
                        return true;
                    }
                }
                i++;
            }
            return false;
        }










        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                get_max100(i);

                this.Text = i.ToString();
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

            //  textBox8.Text = listBox5.SelectedItem.ToString();// listBox5.GetItemText(listBox5.SelectedItem);

            //  this.webBrowser1.Url = new System.Uri(textBox8.Text, System.UriKind.Absolute);
        }

        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  textBox8.Text = listBox5.SelectedItem.ToString();// listBox5.GetItemText(listBox5.SelectedItem);

        }

        private void button7_Click(object sender, EventArgs e)
        {
         //   Clipboard.SetText(listBox2.SelectedItem.ToString());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //  Clipboard.SetText(listBox3.SelectedItem.ToString());

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            


            
            
            try
            {

            for (int i = 0; i < 922; i++)
            {
             
            textBox6.Text +=ConvertUrlsToLinks(listBox3.Items[i].ToString())+ Environment.NewLine;


            }


            }
            catch (Exception)
            {

                int i = 0 ;
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            Form1.CrossOver = double.Parse(textBox1.Text);//CrossOver
            Form1.Mution = double.Parse(textBox2.Text);//Mution
            Form1.Pouple = int.Parse(textBox3.Text);//Pouple
            Form1.gener = int.Parse(textBox4.Text);//Generation
            Form1.Genem = int.Parse(textBox5.Text);//Genem

           
            Main();


            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (coeficients[i] > 70)
                {

                    listBox3.Items.Add(i + ": " + listBox1.Items[i].ToString());
                    //textBox8.Text += listBox1.Items[i].ToString() + Environment.NewLine;
                }
                else
                {
                    //listBox2.Items.Add(i + ": " + listBox1.Items[i].ToString());

                    //textBox9.Text += listBox1.Items[i].ToString() + Environment.NewLine;

                }


            }


            //label10.Text = listBox2.Items.Count.ToString();
            label11.Text = listBox3.Items.Count.ToString();


            for (int i = 0; i < 30; i++)
            {
                listBox3.Items.Add(coeficients[i]);
            }


            MessageBox.Show("Successfully");
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }




    }
}

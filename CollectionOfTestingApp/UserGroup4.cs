﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CollectionOfTestingApp
{
    public partial class UserGroup4 : UserControl
    {
        public UserGroup4()
        {
            InitializeComponent();
        }
        public Dictionary<string, string> GetInternSpeedInfo()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            Process process = null;

            try
            {
                process = new Process();

                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                process.StartInfo.FileName = "speedtest.exe";

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                List<string> list_output = new List<string>();
                list_output = output.Split('\n').ToList();

                result["Server"] = list_output[3].Replace("Server:", "").Trim();
                result["ISP"] = list_output[4].Split(':')[1].Trim();
                result["IdleLatency"] = list_output[5].Replace("Idle Latency:", "").Trim();
                result["Download"] = list_output[6].Replace("Download:", "").Trim();
                result["JJ"] = list_output[7].Trim();
                result["Upload"] = list_output[8].Replace("Upload:", "").Trim();
                result["J"] = list_output[9].Trim();
                result["PacketLoss"] = list_output[10].Split(':')[1].Trim();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                if (process != null) process.Dispose();
            }
            return result;
        }

        //private void UserGroup4_Load(object sender, EventArgs e)
        //{
        //    var result = GetInternSpeedInfo();
        //    richTextBox1.Text = result["Server"];
        //    richTextBox2.Text = result["ISP"];
        //    richTextBox3.Text = result["IdleLatency"];
        //    richTextBox4.Text = result["Download"];
        //    richTextBox5.Text = result["JJ"];
        //    richTextBox6.Text = result["Upload"];
        //    richTextBox7.Text = result["J"];
        //    richTextBox8.Text = result["PacketLoss"];

        //    //Form2 form= new Form2();
        //    //form.ShowDialog();

        //}



        //private void button1_Click(object sender, EventArgs e)
        //{
        //    using (var info = new XLWorkbook())
        //    {
        //        var worksheet = info.Worksheets.Add("Info");
        //        worksheet.Column("A").Width= 15;
        //        worksheet.Cell("A1").Value = "Server    :";
        //        worksheet.Cell("A2").Value = "ISP   :";
        //        worksheet.Cell("A3").Value = "Idle Latency   :";
        //        worksheet.Cell("A4").Value = "Download  :";
        //        worksheet.Cell("A5").Value = "Jitter    :";
        //        worksheet.Cell("A6").Value = "Upload    :";
        //        worksheet.Cell("A7").Value = "Jitter    :";
        //        worksheet.Cell("A8").Value = "Packet Loss    :";

        //        worksheet.Column("B").Width = 60;
        //        worksheet.Cell("B1").Value = richTextBox1.Text;
        //        worksheet.Cell("B2").Value = richTextBox2.Text;
        //        worksheet.Cell("B3").Value = richTextBox3.Text;
        //        worksheet.Cell("B4").Value = richTextBox4.Text;
        //        worksheet.Cell("B5").Value = richTextBox5.Text;
        //        worksheet.Cell("B6").Value = richTextBox6.Text;
        //        worksheet.Cell("B7").Value = richTextBox7.Text;
        //        worksheet.Cell("B8").Value = richTextBox8.Text;
        //        info.SaveAs(Environment.CurrentDirectory + "/" + "Dataterbaru.xlsx");
        //        MessageBox.Show("Result Created");
        //    }
        //}
        private void button1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV|*.csv", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    var internetData = new List<string>();
                    internetData.Add("Server;ISP;Idle Latency;Download;Jitter;Upload;Jitter;Packet Loss");
                    internetData.Add($"{richTextBox1.Text};{richTextBox2.Text};{richTextBox3.Text};{richTextBox4.Text};{richTextBox5.Text};{richTextBox6.Text};{richTextBox7.Text};{richTextBox8.Text}");

                    File.WriteAllLines(sfd.FileName, internetData);

                    MessageBox.Show("Your data has been successfully saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = GetInternSpeedInfo();

            if (result.Count != 0)
            {
                //MessageBox.Show("Done");
                richTextBox1.Text = result["Server"];
                richTextBox2.Text = result["ISP"];
                richTextBox3.Text = result["IdleLatency"];
                richTextBox4.Text = result["Download"];
                richTextBox5.Text = result["JJ"];
                richTextBox6.Text = result["Upload"];
                richTextBox7.Text = result["J"];
                richTextBox8.Text = result["PacketLoss"];
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Click \"Start\" button to run the internet speed test\n2. Click \"Export\" button to generate the results into CSV", "Help");
        }
    }
}
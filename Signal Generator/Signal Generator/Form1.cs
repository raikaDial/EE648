using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Signal_Generator
{
    public partial class Form1 : Form
    {
        DataTable DT;
        BindingSource BS;

        public Form1()
        {
            InitializeComponent();
            DT = new DataTable();
            BS = new BindingSource();

            DT.Columns.Add("Signal Node", typeof(string));
            DT.Columns.Add("Ground Node", typeof(string));
            DT.Columns.Add("Sweep?", typeof(bool));
            DT.Columns.Add("High?", typeof(bool));

            DT.Columns["Ground Node"].DefaultValue = defaultGround.Text;
            DT.Columns["Sweep?"].DefaultValue = true;
            DT.Columns["High?"].DefaultValue = false;

            BS.DataSource = DT;
            pointsGrid.DataSource = BS;
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in DT.Rows)
            {
                if (row["Ground Node"] == DBNull.Value || row["Signal Node"] == DBNull.Value)
                {
                    MessageBox.Show("There is an empty value in the table!!");
                    return;
                }

                if (string.IsNullOrWhiteSpace((string)row["Ground Node"]) || string.IsNullOrWhiteSpace((string)row["Signal Node"]))
                {
                    MessageBox.Show("There is an empty value in the table!!");
                    return;
                }
            }

            SaveFileDialog FD = new SaveFileDialog();
            FD.DefaultExt = ".txt";
            FD.Filter = "Text files (*.txt) | *.txt";
            DialogResult DR = FD.ShowDialog();

            if (DR == DialogResult.Cancel)
            {
                return;
            }

            try
            {

                decimal perWidth = decimal.Round(activeTime.Value/dutyCycle.Value);
                decimal acTime = activeTime.Value - riseTime.Value - fallTime.Value;
                decimal sigCount = 0;

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(FD.FileName, false))
                {
                    

                    foreach (DataRow row in DT.Rows)
                    {
                        file.Write("V_" + suffix.Text + "_" + sigCount.ToString() + " ");
                        sigCount++;
                        file.Write((string)row["Signal Node"] + " " + (string)row["Ground Node"]);
                        if (!(bool)row["Sweep?"])
                        {
                            
                            file.Write(" DC ");

                            if((bool)row["High?"])
                            {
                                file.WriteLine(logicLevel.Value.ToString());
                            }
                            else
                            {
                                file.WriteLine("0");
                            }
                            
                        }
                        else
                        {
                            file.Write(" PULSE(");
                            file.Write("0 ");
                            file.Write(logicLevel.Value + " ");
                            file.Write(delayTime.Value + "n ");
                            file.Write(riseTime.Value + "n ");
                            file.Write(fallTime.Value + "n ");
                            file.Write(acTime + "n ");
                            file.WriteLine(perWidth + "n)");

                            acTime = perWidth + acTime;
                            perWidth = perWidth * 4;

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something bad happened: \n" + ex.Message);
            }

        }
    }
}

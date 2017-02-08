using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureARMFormatter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string _lastFilePath { get; set; }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (!string.IsNullOrEmpty(_lastFilePath))
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(_lastFilePath);
            else
                openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog1.Title = "Browse Text Files";

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            openFileDialog1.DefaultExt = "json";
            openFileDialog1.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                Properties.Settings.Default.LastFilePath = openFileDialog1.FileName;
                _lastFilePath = openFileDialog1.FileName;
                Properties.Settings.Default.Save();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string lstpath = Properties.Settings.Default.LastFilePath;
            if (!string.IsNullOrEmpty(lstpath))
            {
                textBox1.Text = lstpath;
                _lastFilePath = lstpath;
                Properties.Settings.Default.Save();
            }
        }

        private void btnFormat_Click(object sender, EventArgs e)
        {
            try
            {
                TemplateFormatter azfmt = new TemplateFormatter(_lastFilePath);
                txtOutput.Text = azfmt.FormatText();
                if (chkInplaceEdit.Checked)
                    File.WriteAllText(_lastFilePath, txtOutput.Text);
                else { 
                    var fn = Path.GetFileNameWithoutExtension(_lastFilePath);
                    var ext = Path.GetExtension(_lastFilePath);
                    var dir = Path.GetDirectoryName(_lastFilePath);
                    fn += "_formatted" + ext;
                    var newFile = Path.Combine(dir,fn);
                    File.WriteAllText(newFile, txtOutput.Text);
                }
            }
            catch (Exception ex)
            {
                txtOutput.Text = ex.ToString();
            }
        }
    }
}

using System;
using System.IO;
using System.Windows.Forms;
using WIA;

namespace Scanner
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void startScanButton_Click(object sender, EventArgs e)
        {
            string savePath = this.saveFileNameTextBox.Text;
            if( savePath == "")
            {
                MessageBox.Show("ファイル名が設定されていません。", "エラー", MessageBoxButtons.OK,
                     MessageBoxIcon.Error);
                return;
            }
            if (savePath.EndsWith(".jpg") || savePath.EndsWith(".jpeg"))
            {
                ; // nop
            }
            else
            {
                savePath += ".jpeg";
            }
            if( File.Exists(savePath))
            {
                MessageBox.Show(savePath + " はすでに存在します。", "エラー", MessageBoxButtons.OK,
                     MessageBoxIcon.Error);
                return;
            }
            WIA.CommonDialog dlg = new WIA.CommonDialog();
            Device d = dlg.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, true, false);
            if (d == null)
            {
                return;
            }
            Item item = d.Items[1];
            ImageFile image = dlg.ShowTransfer(item, FormatID.wiaFormatJPEG, false) as ImageFile;
            image.SaveFile(savePath);
        }

    }
}

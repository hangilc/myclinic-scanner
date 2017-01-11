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
            string savePath = getSavePath();
            if( savePath == "")
            {
                return;
            }
            try
            {
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
            catch (Exception ex)
            {
                showError(ex.Message);
            }
        }

        private string getSavePath()
        {
            string dirPath = saveDirPathTextBox.Text;
            {
                if (dirPath == "")
                {
                    showError("フォルダー名が設定されていません。");
                    return "";
                }
                try
                {
                    FileAttributes attr = File.GetAttributes(dirPath);
                    if( !attr.HasFlag(FileAttributes.Directory))
                    {
                        showError("フォルダー名に該当するものがフォルダーでありません。");
                        return "";
                    }
                }
                catch(Exception ex)
                {
                    if (ex is FileNotFoundException || ex is DirectoryNotFoundException)
                    {
                        showError("該当するフォルダーが見つけられません");
                    }
                    else
                    {
                        showError(ex.Message);
                    }
                    return "";
                }
            }
            string fileName = saveFileNameTextBox.Text;
            {
                if (fileName == "")
                {
                    showError("ファイル名が設定されていません。");
                    return "";
                }
                if( fileName.EndsWith(".jpg") || fileName.EndsWith(".jpeg") )
                {
                    ; // nop
                }
                else
                {
                    fileName += ".jpeg";
                }
            }
            string path = Path.Combine(dirPath, fileName);
            {
                if( File.Exists(path) )
                {
                    showError("ファイルが既に存在します。");
                    return "";
                }
            }
            return path;
        }

        private void showError(string message)
        {
            MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}

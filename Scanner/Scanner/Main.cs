using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            WIA.CommonDialog dlg = new WIA.CommonDialog();
            Device d = dlg.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, true, false);
            if (d == null)
            {
                return;
            }
            Item item = d.Items[1];
            ImageFile image = dlg.ShowTransfer(item, FormatID.wiaFormatJPEG, false) as ImageFile;
            image.SaveFile("scanned.jpeg");
        }
    }
}

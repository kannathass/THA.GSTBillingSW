using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace THA.GSTBillingSW.PopupControl
{
    public partial class RatePopup : Form
    {
        public string ItemRate { get; set; }
        public RatePopup()
        {
            InitializeComponent();
        }

        private void textBoxRate_TextChanged(object sender, EventArgs e)
        {
            ItemRate = textBoxRate.Text.Trim();
        }
    }
}

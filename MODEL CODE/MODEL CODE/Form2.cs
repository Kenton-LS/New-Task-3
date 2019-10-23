using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MODEL_CODE
{
    public partial class frm2 : Form
    {
        public static int mapX; //values for the map
        public static int mapY;

        public frm2()
        {
            InitializeComponent();
        }

        private void btnConfrim_Click(object sender, EventArgs e)
        {
            var myForm = new frm1();
            myForm.Show();
            this.Hide();
        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(txtX.Text, out mapX); //get map x
            int.TryParse(txtY.Text, out mapY); //get map y

            if (txtX.Text == "") //default values if nothing is entered
            {
                mapX = 20;
            }

            if (txtY.Text == "")
            {
                mapY = 20;
            }
        }
    }
}

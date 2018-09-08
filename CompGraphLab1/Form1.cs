using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompGraphLab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        Castle castle = new Castle();
        
        /// <summary>
        /// Отрисовка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            castle.Draw(e.Graphics, trackBar1.Value, trackBar4.Value, trackBar2.Value, trackBar3.Value);
        }

        /// <summary>
        /// Перерисовка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }
    }
}

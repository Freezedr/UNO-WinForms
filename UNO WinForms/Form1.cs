﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UNO_WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            mhk = new MyHookClass(this);
            mhk.fl = false;
        }

        public MyHookClass mhk;

        private void button1_Click(object sender, EventArgs e)
        {
            mhk.g.play(this);
        }
    }
}

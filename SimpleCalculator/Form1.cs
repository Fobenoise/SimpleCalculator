using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleCalculator
{
    public partial class SmplCalc : Form
    {
        public SmplCalc()
        {
            InitializeComponent();
        }
        double x;
        double y;
        string operation;
        bool isOperPerf = false;
        bool isFirstPerformed = false;
        int eqCount = 0;
      
        private void parameter_Click(object sender, EventArgs e)
        {
            Button BtnVal = (Button)sender;

            if (display.Text == "0" && BtnVal.Text != "." || isOperPerf) //usuwanie zera po kliknieciu cyfry
            {
                if (BtnVal.Text == ".")
                {
                    display.Text = 0 + BtnVal.Text; // dodanie zera przy wcisnieciu kropki po opreratorze
                }
                else
                    display.Clear();
            }
            isOperPerf = false;

            if (display.Text.Contains(".") && BtnVal.Text == ".")
            {
                return;
            }
            display.Text = display.Text + BtnVal.Text;

            if (!isOperPerf)
            {
                return;
            }
            intel.Text = display.Text;
        }
        private void oper_Click(object sender, EventArgs e)
        {
            eqCount = 0;
            if (isOperPerf && intel.Text.Contains(operation))
            {
                return;
            }
            isOperPerf = true;

            Button operClick = (Button)sender;
            operation = display.Text + operClick.Text;
            intel.Text = operation;

            if (isFirstPerformed)
            {
                y = Double.Parse(display.Text);
                var calculate = new Calculate();

                if (operation.Contains("+"))
                {
                    var result = calculate.Add(x, y);
                    x = result;
                    display.Text = result.ToString();
                    intel.Text = display.Text + operClick.Text;
                }
                if (operation.Contains("-"))
                {
                    var result = calculate.Subtract(x, y);
                    x = result;
                    display.Text = result.ToString();
                    intel.Text = display.Text + operClick.Text;
                }
                if (operation.Contains("×"))
                {
                    var result = calculate.Multiply(x, y);
                    x = result;
                    display.Text = result.ToString();
                    intel.Text = display.Text + operClick.Text;
                }
                if (operation.Contains("÷"))
                {
                    var result = calculate.Subtract(x, y);
                    x = result;
                    display.Text = result.ToString();
                    intel.Text = display.Text + operClick.Text;
                }
                return;
            }
            isFirstPerformed = true;
            x = double.Parse(display.Text);
        }
        private void equals_Click(object sender, EventArgs e)
        {
            var tempIntel = intel.Text;
            var r = Regex.Replace(tempIntel, @"(?<=[0-9])[+\-÷×]", string.Empty);
            // zamiana znakow nastepujacuch po liczbie na pusty string

            var op = Regex.Replace(tempIntel, @"((?!\+|-÷×)[-0-9.])", string.Empty);
            // wyodrebnienie operacji z labelki intel

            isOperPerf = true;
            isFirstPerformed = false;

            eqCount += 1;
            var calculate = new Calculate();            

            if (eqCount == 1)
            {
                y = double.Parse(display.Text);
                x = double.Parse(r);
            }
            if (eqCount > 1)
            {
                x = double.Parse(display.Text);
            }
            if (op.Contains("+"))
            {
                var result = calculate.Add(x, y);                
                display.Text = result.ToString();
                intel.Text = display.Text + op;
                return;
            }
            if (op.Contains("-"))
            {
                var result = calculate.Subtract(x, y);
                display.Text = result.ToString();
                intel.Text = display.Text + op;
                return;
            }
            if (op.Contains("×"))
            {
                var result = calculate.Multiply(x, y);
                display.Text = result.ToString();
                intel.Text = display.Text + op;
                return;
            }
            if (op.Contains("÷"))
            {
                var result = calculate.Divide(x, y);
                display.Text = result.ToString();
                intel.Text = display.Text + op;
                return;
            }
        }
        private void Clear_Click(object sender, EventArgs e)
        {
            x = 0;
            y = 0;
            operation = null;
            isFirstPerformed = false;
            isOperPerf = false;
            display.Text = "0";
            intel.Text = string.Empty;
            return;
        }
        private void CE_Click(object sender, EventArgs e)
        {
            display.Text = "0";
            return;
        }
        private void polarity_Click(object sender, EventArgs e)
        {
            var calculate = new Calculate();
            x = Double.Parse(display.Text);
            var result = calculate.signChange(x);
            x = result;
            display.Text = result.ToString();
            eqCount = 0;
        }

        private void reciprocal_click(object sender, EventArgs e)
        {
            var calculate = new Calculate();
            x = Double.Parse(display.Text);
            var result = calculate.Reciprocal(x);
            x = result;
            display.Text = result.ToString();
            eqCount = 0;
        }
    }
}

using System;
using System.Text.RegularExpressions;
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
        bool error = false;
        int eqCount = 0;

        private void parameter_Click(object sender, EventArgs e)
        {
            if (error) return;
            Button BtnVal = (Button)sender;

            if (display.Text == "0" && BtnVal.Text != "." || isOperPerf || error) //usuwanie zera po kliknieciu cyfry
            {
                if (BtnVal.Text == ".")
                {
                    display.Text = 0 + BtnVal.Text; // dodanie zera przy wcisnieciu kropki po opreratorze
                }
                else
                    display.Clear(); error = false;
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
            if (isOperPerf && intel.Text.Contains(operation) || error)
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
                var calculate = new Calculation();

                if (operation.Contains("+"))
                {
                    var result = calculate.Add(x, y);
                    x = result;
                    display.Text = result.ToString();
                }
                if (operation.Contains("-"))
                {
                    var result = calculate.Subtract(x, y);
                    x = result;
                    display.Text = result.ToString();
                }
                if (operation.Contains("×"))
                {
                    var result = calculate.Multiply(x, y);
                    x = result;
                    display.Text = result.ToString();
                }
                if (operation.Contains("÷"))
                {
                    var result = calculate.Subtract(x, y);
                    x = result;
                    display.Text = result.ToString();
                }
                if (error) eMssg();

                intel.Text = display.Text + operClick.Text;
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
            var calculate = new Calculation();

            if (eqCount == 1)
            {
                y = double.Parse(display.Text);
                x = double.Parse(r);
            }
            if (eqCount > 1)
            {
                if (display.Text.Length > 18 || error) return;
                
                    x = double.Parse(display.Text);
            }
            if (op.Contains("+"))
            {
                var result = calculate.Add(x, y);
                display.Text = result.ToString();
                if (error) return;
                intel.Text = display.Text + op;
            }
            if (op.Contains("-"))
            {
                var result = calculate.Subtract(x, y);
                display.Text = result.ToString();
                if (error) return;
                intel.Text = display.Text + op;
            }
            if (op.Contains("×"))
            {
                var result = calculate.Multiply(x, y);
                display.Text = result.ToString();
                if (error) return;
                intel.Text = display.Text + op;
            }
            if (op.Contains("÷"))
            {
                var result = calculate.Divide(x, y);
                display.Text = result.ToString();
                if (error) return;
                intel.Text = display.Text + op;
            }
            return;
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
            error = false;
            return;
        }
        private void CE_Click(object sender, EventArgs e)
        {
            display.Text = "0";
            return;
        }
        private void polarity_Click(object sender, EventArgs e)
        {
            var calculate = new Calculation();
            if (error) return;
            x = Double.Parse(display.Text);
            var result = calculate.signChange(x);
            x = result;
            display.Text = result.ToString();
            eqCount = 0;
        }
        private void reciprocal_click(object sender, EventArgs e)
        {
            var calculate = new Calculation();
            if (error) return;
            x = Double.Parse(display.Text);
            var result = calculate.Reciprocal(x);
            x = result;
            display.Text = result.ToString();
            eqCount = 0;
        }
        private void display_TextChange(object sender, EventArgs e)
        {            
                eMssg();
        }
        private void eMssg()
        {
            if (display.Text.Length > 18 || display.Text == "error")
            {
                display.Text = "error";
                intel.Text = "out of range";

                error = true;
            }                
        }
    }
}

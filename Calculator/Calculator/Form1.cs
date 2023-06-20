using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        double result = 0;
        string operation = string.Empty;
        string fstNum, sndNum;
        bool enterValue = false;
        double currentNumber = 0.0;
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnMathOperation_Click(object sender, EventArgs e)
        {
            try
            {
                if (result != 0)
                {
                    BtnEquals.PerformClick();
                }
                else
                {
                    result = Double.Parse(TxtDisplay1.Text);
                }

                Button button = (Button)sender;
                operation = button.Text;
                enterValue = true;

                if (TxtDisplay1.Text != "0")
                {
                    TxtDisplay2.Text = fstNum = $"{result}{operation}";
                    TxtDisplay1.Text = string.Empty;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Некорректное число");
            }
        }


        private void BtnOp_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            operation = button.Text;

            try
            {
                sndNum = currentNumber.ToString();
                TxtDisplay2.Text = $"{TxtDisplay2.Text} {currentNumber} {operation}";

                if (TxtDisplay1.Text != string.Empty)
                {
                    if (TxtDisplay1.Text == "0")
                    {
                        TxtDisplay2.Text = string.Empty;
                    }

                    double result = 0;

                    switch (operation)
                    {
                        case "^2":
                            result = Math.Pow(currentNumber, 2);
                            TxtDisplay2.Text = $"^2({currentNumber})";
                            TxtDisplay1.Text = result.ToString();
                            break;
                        case "%":
                            result = currentNumber / 100;
                            TxtDisplay2.Text = $"%({currentNumber})";
                            TxtDisplay1.Text = result.ToString();
                            break;
                        case "√x":
                            if (currentNumber >= 0)
                            {
                                result = Math.Sqrt(currentNumber);
                                TxtDisplay2.Text = $"√({currentNumber})";
                                TxtDisplay1.Text = result.ToString();
                            }
                            else
                            {
                                throw new Exception("Некорректное число");
                            }
                            break;
                        case "1/":
                            if (currentNumber != 0)
                            {
                                result = 1 / currentNumber;
                                TxtDisplay2.Text = $"1/({currentNumber})";
                                TxtDisplay1.Text = result.ToString();
                            }
                            else
                            {
                                throw new Exception("Деление на ноль невозможно");
                            }
                            break;
                        case "±":
                            result = -currentNumber;
                            TxtDisplay1.Text = result.ToString();
                            break;
                        default:
                            TxtDisplay2.Text = $"{currentNumber} {operation}";
                            break;
                    }

                    RtBoxDisplayHistory.AppendText($"{fstNum} {sndNum} {operation} = {TxtDisplay1.Text} \n");

                    this.result = double.Parse(TxtDisplay1.Text);
                    operation = string.Empty;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Некорректное число");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnEquals_Click(object sender, EventArgs e)
        {
            sndNum = currentNumber.ToString();
            TxtDisplay2.Text = $"{TxtDisplay2.Text} {currentNumber} {operation}";

            if (TxtDisplay1.Text != string.Empty)
            {
                if (TxtDisplay1.Text == "0")
                {
                    TxtDisplay2.Text = string.Empty;
                }

                double secondNumber;
                if (double.TryParse(TxtDisplay1.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out secondNumber))
                {
                    try
                    {
                        switch (operation)
                        {
                            case "+":
                                TxtDisplay1.Text = (result + secondNumber).ToString();
                                RtBoxDisplayHistory.AppendText($"{fstNum} {sndNum} ={TxtDisplay1.Text} \n");
                                break;
                            case "-":
                                TxtDisplay1.Text = (result - secondNumber).ToString();
                                RtBoxDisplayHistory.AppendText($"{fstNum} {sndNum} ={TxtDisplay1.Text} \n");
                                break;
                            case "×":
                                TxtDisplay1.Text = (result * secondNumber).ToString();
                                RtBoxDisplayHistory.AppendText($"{fstNum} {sndNum} ={TxtDisplay1.Text} \n");
                                break;
                            case "÷":
                                if (secondNumber != 0)
                                {
                                    TxtDisplay1.Text = (result / secondNumber).ToString();
                                    RtBoxDisplayHistory.AppendText($"{fstNum} {sndNum} ={TxtDisplay1.Text} \n");
                                }
                                else
                                {
                                    MessageBox.Show("Деление на ноль невозможно");
                                }
                                break;
                            default:
                                TxtDisplay2.Text = $"{currentNumber} =";
                                break;
                        }

                        result = double.Parse(TxtDisplay1.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                        operation = string.Empty;
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Некорректное число");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Некорректное число");
                }
            }
        }



        private void BtnHistory_Click(object sender, EventArgs e)
        {
            PnlHistory.Height = (PnlHistory.Height == 5)? PnlHistory.Height = 345 : 5;

        }

        private void BtnClearHistory_Click(object sender, EventArgs e)
        {
            RtBoxDisplayHistory.Clear();
            if (RtBoxDisplayHistory.Text == string.Empty)
                RtBoxDisplayHistory.Text = "Нет истории";
        }

        private void BtnBackSpace_Click(object sender, EventArgs e)
        {
            if (TxtDisplay1.Text.Length > 0)
                TxtDisplay1.Text = TxtDisplay1.Text.Remove(TxtDisplay1.Text.Length - 1, 1);
            if (TxtDisplay1.Text == string.Empty) TxtDisplay1.Text = "0";
        }

        private void BtnC_Click(object sender, EventArgs e)
        {
            TxtDisplay1.Text = "0";
            TxtDisplay2.Text = string.Empty;
            result = 0;
        }

        private void BtnCE_Click(object sender, EventArgs e)
        {
            TxtDisplay1.Text = "0";

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BtnOp(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            operation = button.Text;

            try
            {
                sndNum = currentNumber.ToString();
                TxtDisplay2.Text = $"{TxtDisplay2.Text} {currentNumber} {operation}";

                if (TxtDisplay1.Text != string.Empty)
                {
                    if (TxtDisplay1.Text == "0")
                    {
                        TxtDisplay2.Text = string.Empty;
                    }

                    double result = 0;

                    switch (operation)
                    {
                        case "^2":
                            result = Math.Pow(currentNumber, 2);
                            TxtDisplay2.Text = $"({currentNumber})^2";
                            TxtDisplay1.Text = result.ToString();
                            break;
                        case "%":
                            result = currentNumber / 100;
                            TxtDisplay2.Text = $"%({currentNumber})";
                            TxtDisplay1.Text = result.ToString();
                            break;
                        case "√x":
                            if (currentNumber >= 0)
                            {
                                result = Math.Sqrt(currentNumber);
                                TxtDisplay2.Text = $"√({currentNumber})";
                                TxtDisplay1.Text = result.ToString();
                            }
                            else
                            {
                                throw new Exception("Некорректное число");
                            }
                            break;
                        case "1/":
                            if (currentNumber != 0)
                            {
                                result = 1 / currentNumber;
                                TxtDisplay2.Text = $"1/({currentNumber})";
                                TxtDisplay1.Text = result.ToString();
                            }
                            else
                            {
                                throw new Exception("Деление на ноль невозможно");
                            }
                            break;
                        case "±":
                            result = -currentNumber;
                            TxtDisplay1.Text = result.ToString();
                            break;
                        default:
                            TxtDisplay2.Text = $"{currentNumber} {operation}";
                            break;
                    }

                    RtBoxDisplayHistory.AppendText($"{fstNum} {sndNum} {operation} = {TxtDisplay1.Text} \n");

                    this.result = double.Parse(TxtDisplay1.Text);
                    operation = string.Empty;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Некорректное число");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            Point screenPoint = BtnMenu.PointToScreen(new Point(BtnMenu.Left, BtnMenu.Bottom));
            if (screenPoint.Y + contextMenuStrip1.Size.Height > Screen.PrimaryScreen.WorkingArea.Height)
            {
                contextMenuStrip1.Show(BtnMenu, new Point(0, -contextMenuStrip1.Size.Height));
            }
            else
            {
                contextMenuStrip1.Show(BtnMenu, new Point(0, BtnMenu.Height));
            }
        }

        private void vToolStripMenuItem_Click(object sender, EventArgs e)
        {
             Form1 form1 = new Form1();
             form1.ShowDialog();
             this.Close(); 
        }

        private void программированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            progcalc form2 = new progcalc();
            form2.ShowDialog();
            this.Close();
        }

        private void инжнерныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sincecalc form3 = new sincecalc();
            form3.ShowDialog();
            this.Close();
        }

        private void BtnNum_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (TxtDisplay1.Text == "0" || enterValue)
            {
                TxtDisplay1.Text = string.Empty;
                currentNumber = 0.0;
                enterValue = false;
            }

            if (button.Text == ".")
            {
                if (!TxtDisplay1.Text.Contains("."))
                {
                    TxtDisplay1.Text = TxtDisplay1.Text + button.Text;
                }
            }
            else
            {
                TxtDisplay1.Text = TxtDisplay1.Text + button.Text;
            }

            if (double.TryParse(TxtDisplay1.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double parsedNumber))
            {
                currentNumber = parsedNumber;
            }
            else
            {
                // Обработка ошибки некорректного формата числа
                MessageBox.Show("Некорректное число");
            }


        }
    }
}

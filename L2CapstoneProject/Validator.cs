using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L2CapstoneProject
{
    public static class Validator
	{
        private static string Title = "Input Error";
		public static bool IsPresent(Control control)
		{
            if (control.GetType() == typeof(TextBox))
            {
                if (control.Text == "")
                {
                    MessageBox.Show(control.Tag + " is a required field.", Title);
                    control.Focus();
                    return false;
                }
            }
            return true;
        }

        public static bool IsDecimal(TextBox textBox)
        {
            decimal number = 0m;
            if (Decimal.TryParse(textBox.Text, out number))
            {
                return true;
            }
            else
            {
                MessageBox.Show(textBox.Tag + " must be a decimal value.", Title);
                textBox.Focus();
                return false;
            }
        }

        public static bool IsInt32(TextBox textBox)
        {
            int number = 0;
            if (Int32.TryParse(textBox.Text, out number))
            {
                return true;
            }
            else
            {
                MessageBox.Show(textBox.Tag + " must be an integer.", Title);
                textBox.Focus();
                return false;
            }
        }

        public static bool IsDouble(TextBox textBox)
        {
            double number = 0;
            if (Double.TryParse(textBox.Text, out number))
            {
                return true;
            }
            else
            {
                MessageBox.Show(textBox.Tag + " must be a double.", Title);
                textBox.Focus();
                return false;
            }
        }

        public static bool IsWithinRange(Control control, decimal min, decimal max)
		{
            decimal number;
            if (control.GetType() == typeof(TextBox)) 
            {
                number = Decimal.Parse(control.Text); 
            }
            else if (control.GetType() == typeof(NumericUpDown))
            {
                NumericUpDown numControl = (NumericUpDown)control;
                number = numControl.Value;
            }
            else
            {
                throw new NotImplementedException();
            }
  
			if (number < min || number > max)
			{
				MessageBox.Show(control.Tag + " must be between " + min
					+ " and " + max + ".", Title);
				control.Focus();
				return false;
			}
			return true;
		}

    }
}

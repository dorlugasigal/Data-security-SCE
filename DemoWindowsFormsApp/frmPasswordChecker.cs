using System;
using System.Windows.Forms;
using PasswordStrengthDLL;
using Strength = PasswordStrengthDLL.Constants.Strength;

namespace DemoWindowsFormsApp
{
    public partial class frmPasswordChecker : Form
    {
        public frmPasswordChecker()
        {
            InitializeComponent();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            string pass = txtPassword.Text;

            Strength strength = PasswordStrengthLogic.CheckStrength(pass);
            switch (strength)
            {
                case Strength.VeryWeak:
                    
                    lblResult.Text = "Your password is Very Weak";
                    break;
                case Strength.Weak:
                    lblResult.Text = "Your password is Weak";
                    break;
                case Strength.Medium:
                    lblResult.Text = "Your password is Medium";
                    break;
                case Strength.Strong:
                    lblResult.Text = "Your password is Strong";
                    break;
                case Strength.VeryStrong:
                    lblResult.Text = "Your password is Very Strong";
                    break;
            }

        }
    }
}

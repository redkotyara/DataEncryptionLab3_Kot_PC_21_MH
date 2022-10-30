using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataEncryptionLab3_Kot_PC_21_MH
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void myShowToolTip(TextBox tb, byte[] arr)
        {
            var result = "";

            foreach (var item in arr)
            {
                var convertedString = Convert.ToString(item, 2);
                result += convertedString;
            }
            
            toolTip_HEX.SetToolTip(tb, result.TrimStart('0'));
        }

        private byte[] myXOR(byte[] arr_text, byte[] arr_key)
        {
            var len_text = arr_text.Length;
            var len_key = arr_key.Length;
            var arr_cipher = new byte[len_text];

            for (var i = 0; i < len_text; i++)
            {
                var p = arr_text[i];
                var k = arr_key[i % len_key];
                var c = (byte)(p ^ k);

                arr_cipher[i] = c;
            }

            return arr_cipher;
        }

        private string myCipher(TextBox tb_text, TextBox tb_Key, TextBox tb_cipher, string cipher = "")
        {
            var text = tb_text.Text;

            var arr_text = Encoding.UTF8.GetBytes(cipher == "" ? text : cipher);
            myShowToolTip(tb_text, arr_text);

            var key = tb_Key.Text;
            var arr_key = Encoding.UTF8.GetBytes(key);
            myShowToolTip(tb_Key, arr_key);

            var arr_cipher = myXOR(arr_text, arr_key);

            cipher = Encoding.UTF8.GetString(arr_cipher);
            tb_cipher.Text = cipher;
            myShowToolTip(tb_cipher, arr_cipher);

            return cipher;
        }

        private void button_clean_Click(object sender, EventArgs e)
        {
            textBox_C_IN.Text = textBox_Key_IN.Text = textBox_P_IN.Text = "";
            textBox_C_OUT.Text = textBox_Key_OUT.Text = textBox_P_OUT.Text = "";
        }

        private void button_XOR_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_Key_IN.Text))
            {
                textBox_Key_OUT.Text = "";
                textBox_P_OUT.Text = textBox_C_OUT.Text = textBox_C_IN.Text = textBox_P_IN.Text;
                return;
            }

            var cipher = myCipher(textBox_P_IN, textBox_Key_IN, textBox_C_IN);
            textBox_P_OUT.Text = textBox_C_IN.Text;
            textBox_Key_OUT.Text = textBox_Key_IN.Text;
            myCipher(textBox_P_OUT, textBox_Key_OUT, textBox_C_OUT, cipher);
        }
    }
}

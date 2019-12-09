using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace test_emulator
{
    public class TestEmulator
    {
        public static bool isEmulator() {
            
            bool a = RegEditLinq.RegistryCtrl.Display.ToLower().Contains("emulator");
            bool b = RegEditLinq.RegistryCtrl.DisplayDll.ToLower().Contains("emulator");
            bool c = RegEditLinq.RegistryCtrl.Launch98.ToLower().Contains("emulator");
            bool d = RegEditLinq.RegistryCtrl.Name.ToLower().Contains("emulator");

            return a || b || c || d;
        }

        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}

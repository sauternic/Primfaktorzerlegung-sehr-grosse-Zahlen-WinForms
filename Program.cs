using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Diagnostics;


namespace Primfaktorzerlegung
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    //#regiong Reine Info:
    //27 Stellen :  100000000000000000000000000
    //25 Stellen :  1000000000000000000000000
    //21 Stellen :  100000000000000000000
    //20 Stellen:   10000000000000000000
    //17 Stellen:   10000000000000000
    //16 Stellen:   1000000000000000

    //Max ulong =  18446744073709551615;
    //Max decimal = 79228162514264337593543950335M;

    //Zwei starke fermatische Pseudoprimzahlen
    //2152302898747
    //3474749660383
    //#endregion

    public partial class Form1 : Form
    {
        //Fields
        //ulong zwischenRest;
        //int i;

        void Zerlegen()
        {
            BigInteger wert = 0;
            //Wert Auslesen:
            try
            {
                wert = BigInteger.Parse(textBox1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Bitte nur Zahlen Eingeben!");
            }
            //Textbox leeren
            textBox2.Invoke(new Action(() => textBox2.Text = ""));

            //Zeit Messung:
            Stopwatch s = new Stopwatch();


            s.Start();
            //Zerlegungs-Engine! :)
            label1:
            //Kein Flaschenhals!! :O
            double Wurzel_anfang = 0;
            try
            {
                Wurzel_anfang = Math.Pow(Convert.ToDouble(wert.ToString()), 0.5);
            }
            catch (Exception)
            {
                //Notlösung wenn ToDouble Methode am Anschlag! :)))
                for (ulong teiler = 2; teiler <= wert; teiler++)
                {
                    if (wert % teiler == 0)
                    {
                        textBox2.Invoke(new Action(() => textBox2.Text += String.Format("{0:#,#}\r\n", teiler)));
                        wert = wert / teiler;
                        goto label1;
                    }
                }
                textBox2.Invoke(new Action(() => textBox2.Text += String.Format("{0:#,#}\r\n", wert)));
            }

            for (ulong teiler = 2; teiler <= Wurzel_anfang; teiler++)
            {
                if (wert % teiler == 0)
                {
                    textBox2.Invoke(new Action(() => textBox2.Text += String.Format("{0:#,#}\r\n", teiler)));
                    wert = wert / teiler;
                    goto label1;
                }
            }
            textBox2.Invoke(new Action(() => textBox2.Text += String.Format("{0:#,#}\r\n", wert)));

            //Zeit Messung Ausgeben:
            s.Stop();
            TimeSpan t = s.Elapsed;
            textBox2.Invoke(new Action(() => textBox2.Text +=
            String.Format("Time: {0}d {1}h {2}m {3}s {4}ms\r\n", t.Days, t.Hours, t.Minutes, t.Seconds, t.Milliseconds)));
            textBox2.Invoke(new Action(() => textBox2.Text += "Copyright © Nicolas Sauter"));

        }
    }
}








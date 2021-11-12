using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELTE_FuggvenyRajzolo
{
    public partial class Form1 : Form
    {
        void FvRajz(int xa, int xf, Func<double, double> f)
        { 

        }

        void Főprogram(PictureBox vászon)
        {
            double xa = -10 * Math.PI;
            double xf = 10 * Math.PI;
            int xh = vászon.Size.Width;
            Func<double, double> f = x => Math.Sin(x) * Math.Sin(x)/(x*x) ;
            double[] fv = Számítás(xa,xf,xh, f);
            (double ya, double yf) = Szélsőérték(xa, xf, xh, fv);
            Normálás(ya, yf,xh, fv);
            Rajzolás(vászon, fv);
            /*
            */

        }

        double[] Számítás(double xa, double xf, int xh, Func<double, double> f)
        {
            double hol = xa;
            int j = 0;
            double lepx = (xf - xa) / xh;
            double[] fv = new double[xh+1];
            while (hol <= xf)
            {
                fv[j] = f(hol);
                hol += lepx;
                j++;
            }
            return fv;
        }
        (double, double) Szélsőérték(double xa, double xf, double xh, double[] fv)
        {
            double ya = fv[0];
            double yf = fv[0];
            for (int i = 1; i < xh; i++)
            {
                if (ya > fv[i]) ya = fv[i];
                if (yf < fv[i]) yf = fv[i];
            }
            return (ya, yf);
        }
        void Normálás(double ya, double yf, int xh, double[] fv)
        {
            double norm = Math.Abs(yf - ya);
            for (int i = 0; i < xh; i++)
                fv[i] /= norm;
        }

        void Rajzolás(PictureBox vászon, double[] fv)
        {
            int ox = 0; // hol az origo vízszintesen
            int yh = vászon.Size.Height;
            int xh = vászon.Size.Width;
            int oy = yh/2; // hol az origó függőlegesen
            Bitmap kep = new Bitmap(xh,yh);
            for (int i = 0; i < xh; i++)
            {
                int xp = ox + i;
                int yp = (int)Math.Round((double)oy - (double)yh * fv[i]);
                Pont(xp, yp, kep);
            }
            vászon.Image = kep;
            vászon.Refresh();

        }

        void Pont(int xp, int yp, Bitmap kep)
        {
            if (0<yp && yp<kep.Height)
            {
                kep.SetPixel(xp, yp, Color.White);
            }
        }

        public Form1()
        {
            InitializeComponent();
            Főprogram(vaszon);
        }
    }
}

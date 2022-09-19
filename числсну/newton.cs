using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace числсну
{
    class newton
    {
        public void jacobi(double x0, double y0, double z0, double[,] a, double[,] koefs,int vid)
        {
            if(vid ==1)
            {
                a[0, 0] = -2 * x0 * koefs[0, 1] - koefs[0, 0]; //производные первого ур.
                a[0, 1] = -z0 * koefs[0, 2];
                a[0, 2] = -y0 * koefs[0, 2];

                a[1, 0] = -koefs[1, 2] * z0; //производные всторого ур.
                a[1, 1] = -2 * koefs[1, 1] * y0 - koefs[1, 0];
                a[1, 2] = -koefs[1, 2] * x0;

                a[2, 0] = -koefs[2, 2] * y0; //производные третьего ур.
                a[2, 1] = -koefs[2, 2] * x0;
                a[2, 2] = -2 * koefs[2, 1] * z0 - koefs[2, 0];
            }
            if(vid == 2)
            {
                a[0, 0] = -koefs[0, 0] * koefs[0, 1] * Math.Cos(koefs[0, 1] * x0);
                a[0, 1] = -koefs[0, 2];
                a[1, 0] = -koefs[1, 0];
                a[1, 1] = koefs[1, 1] * koefs[1, 2] * Math.Sin(koefs[1, 2] * y0);
            }
         
        }
        public void inverse(double[,] a, double[,] a1,int vid)
        {
            if(vid == 1)
            {
                double det;
                double[,] s = new double[3, 3];
                det = a[0, 0] * a[1, 1] * a[2, 2] + a[1, 0] * a[2, 1] * a[0, 2] + a[0, 1] * a[1, 2] * a[2, 0] - a[2, 0] * a[1, 1] * a[0, 2] - a[2, 1] * a[1, 2] * a[0, 0] - a[1, 0] * a[0, 1] * a[2, 2];
                s[0, 0] = a[1, 1] * a[2, 2] - a[1, 2] * a[2, 1];
                s[0, 1] = -a[0, 1] * a[2, 2] + a[0, 2] * a[2, 1];
                s[0, 2] = a[0, 1] * a[1, 2] - a[0, 2] * a[1, 1];
                s[1, 0] = -a[1, 0] * a[2, 2] + a[1, 2] * a[2, 0];
                s[1, 1] = a[0, 0] * a[2, 2] - a[0, 2] * a[2, 0];
                s[1, 2] = -a[0, 0] * a[1, 2] + a[0, 2] * a[1, 0];
                s[2, 0] = a[1, 0] * a[2, 1] - a[1, 1] * a[2, 0];
                s[2, 1] = -a[0, 0] * a[2, 1] + a[0, 1] * a[2, 0];
                s[2, 2] = a[0, 0] * a[1, 1] - a[0, 1] * a[1, 0];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        a1[i, j] = (1 / det) * s[i, j];
                    }
                }
            }
            if(vid == 2)
            {
                double det = a[0,0]*a[1,1]-a[0,1]*a[1,0];     
                double[,] s = new double[2, 2];
                s[0, 0] = a[1, 1];
                s[0, 1] = -a[1, 0];
                s[1, 0] = -a[0, 1];
                s[1, 1] = a[0, 0];
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        a1[i, j] = (1 / det) * s[i, j];
                    }
                }

            }
            
        }
        public void fx(double x0, double y0, double z0, double[] f, double[,] koefs,int vid)
        {
            if(vid == 1)
            {
                f[0] = -x0 * koefs[0, 0] - x0 * x0 * koefs[0, 1] - y0 * z0 * koefs[0, 2] + koefs[0, 3];
                f[1] = -y0 * koefs[1, 0] - y0 * y0 * koefs[1, 1] - x0 * z0 * koefs[1, 2] + koefs[1, 3];
                f[2] = -z0 * koefs[2, 0] - z0 * z0 * koefs[2, 1] - x0 * y0 * koefs[2, 3] + koefs[2, 3];
            }
            if(vid == 2)
            {
                //f[0] = -x0 * koefs[0, 0] - x0 * x0 * koefs[0, 1] - y0 * z0 * koefs[0, 2] + koefs[0, 3];
                //f[1] = -koefs[1, 0] * Math.Cos(x0) * Math.Sin(y0) - Math.Sin(y0) * koefs[1, 1] + koefs[1, 2];
                //f[2] = -koefs[2, 0] * z0 - koefs[2, 1] * y0 + koefs[2, 2];
                f[0] = -koefs[0, 0] * Math.Sin(koefs[0, 1] * x0) - koefs[0, 2] * y0 + koefs[0, 3];
                f[1] = -koefs[1, 0] * x0 - koefs[1, 1] * Math.Cos(koefs[1, 2] * y0) + koefs[1, 3];
            }
           
        }
        public void minus(double[] n, double[] temp, double[] z,int vid)
        {
            if(vid == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    z[i] = n[i] - temp[i];
                }
            }
            if(vid == 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    z[i] = n[i] - temp[i];
                }
            }
            
        }
        public void mult(double[,] a1, double[] f, double[] temp,int vid) //обратная матрица умноженная на функцию
        {
            if(vid == 1)
            {
                temp[0] = a1[0, 0] * f[0] + a1[0, 1] * f[1] + a1[0, 2] * f[2];
                temp[1] = a1[1, 0] * f[0] + a1[1, 1] * f[1] + a1[1, 2] * f[2];
                temp[2] = a1[2, 0] * f[0] + a1[2, 1] * f[1] + a1[2, 2] * f[2];
            }
            if(vid == 2)
            {
                temp[0] = a1[0, 0] * f[0] + a1[0, 1] * f[1];
                temp[1] = a1[1, 0] * f[0] + a1[1, 1] * f[1];
            }
        }
    }
}

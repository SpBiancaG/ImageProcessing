using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Sections
{
    public class Thresholding
    {
        public static byte CalculateIntermeansThreshold(Image<Gray, byte> inputImage)
        {
            // prag initial
            byte threshold = 128;
            while (true)
            {
                // Calculare medii pentru cele doua clase
                double sum1 = 0, sum2 = 0;
                int count1 = 0, count2 = 0;

                for (int y = 0; y < inputImage.Height; y++)
                {
                    for (int x = 0; x < inputImage.Width; x++)
                    {
                        if (inputImage.Data[y, x, 0] > threshold)
                        {
                            sum1 += inputImage.Data[y, x, 0];
                            count1++;
                        }
                        else
                        {
                            sum2 += inputImage.Data[y, x, 0];
                            count2++;
                        }
                    }
                }

                double mean1 = count1 > 0 ? sum1 / count1 : 0;
                double mean2 = count2 > 0 ? sum2 / count2 : 0;

                // pragul nou
                byte newThreshold = (byte)((mean1 + mean2) / 2);

                // verificare convergenta
                if (newThreshold == threshold)
                {
                    break;
                }

                // Actualizarea pragului pentru următoarea iteratie
                threshold = newThreshold;
            }

            return threshold;
        }
    }
}
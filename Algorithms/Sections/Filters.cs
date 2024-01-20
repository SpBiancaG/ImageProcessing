using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static OpenTK.Graphics.OpenGL.GL;

namespace Algorithms.Sections
{
    public class Filters
    {
     
        public static Image<Gray, byte> KuwaharaFilter(Image<Gray, byte> inputImage, int windowSize)
        {
            int width = inputImage.Width;
            int height = inputImage.Height;

            Image<Gray, byte> result = new Image<Gray, byte>(width, height);
            int halfWindowSize = windowSize / 2;

            for (int x = halfWindowSize; x < width - halfWindowSize; x++)
            {
                for (int y = halfWindowSize; y < height - halfWindowSize; y++)
                {
                    double[] meanValues = new double[4];
                    double[] varianceValues = new double[4];

                    CalculateRegionStats(inputImage, x, y, windowSize, meanValues, varianceValues);

                    int minVarianceIndex = FindMinVarianceIndex(varianceValues);
                    // se setează valoarea pixelului de ieșire
                    result.Data[y, x, 0] = (byte)meanValues[minVarianceIndex];
                }
            }

            return result;
        }


         private static void CalculateRegionStats(Image<Gray, byte> inputImage, int x, int y, int windowSize, double[] meanValues, double[] varianceValues)
         {
             int halfWindowSize = windowSize / 2;

             //Pentru fiecare subregiune, se calculează media și varianța
             for (int i = 0; i < windowSize - 1; i++)
             {
                 double mean = 0;
                 double variance = 0;

                 // Determinarea limitelor subregiunii
                 int startX = x - halfWindowSize + (i % 2) * halfWindowSize;
                 int startY = y - halfWindowSize + (i / 2) * halfWindowSize;
                 int endX = startX + halfWindowSize;
                 int endY = startY + halfWindowSize;

                // media și varianța pentru fiecare subregiune
                for (int row = startY; row < endY; row++)
                 {
                     for (int col = startX; col < endX; col++)
                     {
                         byte pixelValue = inputImage.Data[row, col, 0];
                         mean += pixelValue;
                         variance += pixelValue * pixelValue;
                     }
                 }

                 // normalizarea valorilor mediei și varianței
                 mean /= halfWindowSize * halfWindowSize;
                 variance = variance / (halfWindowSize * halfWindowSize) - mean * mean;

                 meanValues[i] = mean;
                 varianceValues[i] = variance;
             }
         }



        private static int FindMinVarianceIndex(double[] varianceValues)
        {
            int minVarianceIndex = 0;
            double minVariance = varianceValues[0];
            // se alege regiunea cu cea mai mică varianță ca valoare de ieșire
            for (int i = 1; i < varianceValues.Length; i++)
            {
                if (varianceValues[i] < minVariance)
                {
                    minVariance = varianceValues[i];
                    minVarianceIndex = i;
                }
            }

            return minVarianceIndex;
        }

    }
}

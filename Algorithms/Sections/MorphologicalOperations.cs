using System.Drawing;
using System;
using Emgu.CV.Structure;
using Emgu.CV;

namespace Algorithms.Sections
{
    public class MorphologicalOperations
    {
        public static byte[,] PerformXOR(byte[,] binaryImage, int width, int height, int erosionIterations)
        {
            // Clonarea imaginii binare pentru a evita modificarea originalului
            byte[,] grayImage = CloneArray(binaryImage, width, height);

            // Imaginea erodata
            byte[,] erodedImage = Erode(grayImage, width, height, erosionIterations);

            // Aplicare XOR
            byte[,] resultImage = new byte[width, height];
            PerformXOROperation(grayImage, erodedImage, resultImage, width, height);

            return resultImage;
        }

        private static void PerformXOROperation(byte[,] inputImage1, byte[,] inputImage2, byte[,] outputImage, int width, int height)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    byte pixelValue1 = inputImage1[x, y];
                    byte pixelValue2 = inputImage2[x, y];
         
                    byte resultPixelValue = (byte)(pixelValue1 ^ pixelValue2);

                    outputImage[x, y] = resultPixelValue;
                }
            }
        }

        private static byte[,] Erode(byte[,] inputImage, int width, int height, int iterations)
        {
            byte[,] erodedImage = CloneArray(inputImage, width, height);

            int kernelSize = 3; 
            int halfKernelSize = kernelSize / 2;

            for (int iteration = 0; iteration < iterations; iteration++)
            {
                byte[,] tempImage = CloneArray(erodedImage, width, height);

                for (int x = halfKernelSize; x < width - halfKernelSize; x++)
                {
                    for (int y = halfKernelSize; y < height - halfKernelSize; y++)
                    {
                        byte minPixelValue = 255;
 
                        for (int i = -halfKernelSize; i <= halfKernelSize; i++)
                        {
                            for (int j = -halfKernelSize; j <= halfKernelSize; j++)
                            {
                                byte pixelValue = inputImage[x + i, y + j];
                                minPixelValue = Math.Min(minPixelValue, pixelValue);
                            }
                        }

                        tempImage[x, y] = minPixelValue;
                    }
                }

                erodedImage = CloneArray(tempImage, width, height);
            }

            return erodedImage;
        }

        private static byte[,] CloneArray(byte[,] array, int width, int height)
        {
            byte[,] result = new byte[width, height];
            Array.Copy(array, result, array.Length);
            return result;
        }


    }
}
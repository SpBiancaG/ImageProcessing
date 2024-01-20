using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Windows.Forms;

namespace Algorithms.Sections
{
    public class PointwiseOperations
    {

        #region Brightness with LUT
        public static byte[] CreateBrightnessLUT(double brightnessValue)
        {
            byte[] LUT = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                double adjustedValue = i + brightnessValue;
                adjustedValue = (adjustedValue < 0) ? 0 : ((adjustedValue > 255) ? 255 : adjustedValue);
                LUT[i] = (byte)(adjustedValue +0.5);
            }
            return LUT;
        }

        public static Image<Gray, byte> AdjustBrightness(Image<Gray, byte> inputImage, double brightnessValue)
        {
            byte[] LUT = CreateBrightnessLUT(brightnessValue);
            Image<Gray, byte> result = inputImage.Clone();

            for (int y = 0; y < inputImage.Height; y++)
            {
                for (int x = 0; x < inputImage.Width; x++)
                {
                    result.Data[y, x, 0] = LUT[inputImage.Data[y, x, 0]];
                }
            }
            return result;
        }

        public static Image<Bgr, byte> AdjustBrightness(Image<Bgr, byte> inputImage, double brightnessValue)
        {
            byte[] LUT = CreateBrightnessLUT(brightnessValue);
            Image<Bgr, byte> result = inputImage.Clone();

            for (int y = 0; y < inputImage.Height; y++)
            {
                for (int x = 0; x < inputImage.Width; x++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        result.Data[y, x, c] = LUT[inputImage.Data[y, x, c]];
                    }
                }
            }
            return result;
        }
        #endregion



        #region Brightness and Contrast with LUT

        public static byte[] CreateBrightnessContrastLUT(double contrastFactor, double brightnessFactor = 0)
        {
            byte[] LUT = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                double adjustedValue = i * contrastFactor + brightnessFactor;
                adjustedValue = (adjustedValue < 0) ? 0 : ((adjustedValue > 255) ? 255 : adjustedValue);
                LUT[i] = (byte)adjustedValue;
            }
            return LUT;
        }

        public static Image<Gray, byte> AdjustBrightnessAndContrast(Image<Gray, byte> inputImage, double contrastFactor)
        {
            if (contrastFactor <= 0)
            {
                MessageBox.Show("The value must be positive.");
                return inputImage.Clone();
            }

            byte[] LUT = CreateBrightnessContrastLUT(contrastFactor);
            Image<Gray, byte> result = inputImage.Clone();

            for (int y = 0; y < inputImage.Height; y++)
            {
                for (int x = 0; x < inputImage.Width; x++)
                {
                    result.Data[y, x, 0] = LUT[inputImage.Data[y, x, 0]];
                }
            }
            return result;
        }

        public static Image<Bgr, byte> AdjustBrightnessAndContrast(Image<Bgr, byte> inputImage, double contrastFactor)
        {
            if (contrastFactor <= 0)
            {
                MessageBox.Show("The value must be positive.");
                return inputImage.Clone();
            }

            byte[] LUT = CreateBrightnessContrastLUT(contrastFactor);
            Image<Bgr, byte> result = inputImage.Clone();

            for (int y = 0; y < inputImage.Height; y++)
            {
                for (int x = 0; x < inputImage.Width; x++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        result.Data[y, x, c] = LUT[inputImage.Data[y, x, c]];
                    }
                }
            }
            return result;
        }

        #endregion

    }
}

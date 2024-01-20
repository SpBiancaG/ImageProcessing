using Emgu.CV.Structure;
using Emgu.CV;
using System;

namespace Algorithms.Sections
{
    public class GeometricTransformations
    {
        public static Image<Bgr, byte> ApplyTwirl(Image<Bgr, byte> inputImage, double twirlValue)
        {
            int width = inputImage.Width;
            int height = inputImage.Height;

            Image<Bgr, byte> twirledImage = new Image<Bgr, byte>(width, height);

            double centerX = width / 2.0;
            double centerY = height / 2.0;

            double twirlAngle = twirlValue * Math.PI / 180.0; //conversie grade radiani

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double dx = x - centerX;//valorile pixel actual-centru
                    double dy = y - centerY;

                    double radius = Math.Sqrt(dx * dx + dy * dy);//distanta centru rotatie la pixelul curent
                    double angle = Math.Atan2(dy, dx) + twirlAngle * Math.Exp(-radius / 100.0);//beta

                    int sourceX = (int)(centerX + radius * Math.Cos(angle));
                    int sourceY = (int)(centerY + radius * Math.Sin(angle));

                    if (sourceX >= 0 && sourceX < width && sourceY >= 0 && sourceY < height)
                    {
                        twirledImage[y, x] = inputImage[sourceY, sourceX];
                    }
                }
            }

            return twirledImage;
        }


        public static Image<Gray, byte> ApplyTwirl(Image<Gray, byte> inputImage, double twirlValue)
        {
            int width = inputImage.Width;
            int height = inputImage.Height;

            Image<Gray, byte> twirledImage = new Image<Gray, byte>(width, height);

            double centerX = width / 2.0;
            double centerY = height / 2.0;

            double twirlAngle = twirlValue * Math.PI / 180.0;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double dx = x - centerX;
                    double dy = y - centerY;

                    double radius = Math.Sqrt(dx * dx + dy * dy);
                    double angle = Math.Atan2(dy, dx) + twirlAngle * Math.Exp(-radius / 100.0);

                    int sourceX = (int)(centerX + radius * Math.Cos(angle));
                    int sourceY = (int)(centerY + radius * Math.Sin(angle));

                    if (sourceX >= 0 && sourceX < width && sourceY >= 0 && sourceY < height)
                    {
                        twirledImage[y, x] = inputImage[sourceY, sourceX];
                    }
                }
            }

            return twirledImage;
        }

    }
}
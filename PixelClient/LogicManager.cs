using PixelModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PixelClient
{
    public class LogicManager
    {
        static List<Pixel> pixels;
        static Logger logger;
        static Timer timer;
        static int matingGenerationStart;
        static int matingGenerationEnd;
        static int fatalGeneration;
        public LogicManager(string FileName)
        {
            string fileName = FileName;
            logger = new Logger(fileName);
            InitColony(4);
            File.Create(fileName).Close();
            InitTimer();
            InitGenerationParams();
            while (true)
            {
                Application.DoEvents();
            }
        }
        private static void InitGenerationParams()
        {
            matingGenerationStart = 3;
            matingGenerationEnd = 6;
            fatalGeneration = 8;
        }
        private static void InitTimer()
        {
            timer = new Timer();
            timer.Interval = 5000;
            timer.Tick += new EventHandler(Iterate);
            timer.Start();
        }

        private static void Iterate(object sender, EventArgs e)
        {
            int eightGenCount = 0;
            int currentCount = pixels.Count;
            for (int i = 0; i < currentCount; i++)
            {
                if (pixels[i].Generation >= matingGenerationStart && pixels[i].Generation <= matingGenerationEnd)
                {
                    if (pixels[i].Mate == null)
                    {
                        FindMate(pixels[i], i + 1);
                    }
                    else
                    {
                        Multiply(pixels[i]);
                    }
                }
                else if (pixels[i].Generation == fatalGeneration)
                {
                    eightGenCount++;
                }
                pixels[i].Generation++;
            }
            if (eightGenCount > 0)
            {
                pixels.RemoveRange(0, eightGenCount);
            }
            logger.Log($"The Pixel settlement is growing, it now has {pixels.Count} pixels!");
            for (int i = 0; i < pixels.Count; i++)
            {
                logger.Log($"Pixel {i} is : {pixels[i].DNA[0]}, {pixels[i].DNA[1]},{pixels[i].DNA[2]}");
            }
        }
        private static void FindMate(Pixel pixel, int startIndex)
        {
            Pixel compromiseMatch = null;
            for (int i = startIndex; i < pixels.Count; i++)
            {
                if (pixels[i].Generation > matingGenerationEnd)
                {
                    break;
                }
                else if (ArePixelsCompatible(pixel, pixels[i]))
                {
                    pixel.Mate = pixels[i];
                    Multiply(pixel);
                    return;
                }
                else if(compromiseMatch==null && CompromiseCompatability(pixel,pixels[i]))
                {
                    compromiseMatch = pixels[i];
                }
            }
            pixel.Mate = compromiseMatch;
            pixel.Mate.Mate = pixel;
            Multiply(pixel);
        }
        private static bool ArePixelsCompatible(Pixel firstPixel, Pixel secondPixel)
        {
            if (firstPixel.Parents[0] == secondPixel.Parents[0] && firstPixel.Parents[1] == secondPixel.Parents[1])
            {
                return false;
            }
            for (int i = 0; i < firstPixel.DNA.Length; i++)
            {
                if (firstPixel.DNA[i] == secondPixel.DNA[i] && secondPixel.Mate == null)
                    return false;
            }
            return true;
        }
        private static bool CompromiseCompatability(Pixel firstPixel, Pixel secondPixel)
        {
            if(firstPixel.Parents[0] == secondPixel.Parents[0] && firstPixel.Parents[1] == secondPixel.Parents[1])
            {
                return false;
            }
            return true;
        }
        private static void Multiply(Pixel pixel)
        {
            if (pixel.Mate.Generation <= 6)
            {
                pixel.HasMated = true;
                pixel.Mate.HasMated = true;
                pixels.Add(pixel.Multiply(pixel.Mate));
            }
        }
        static void InitColony(int colonySize)
        {
            pixels = new List<Pixel>();
            Random rnd = new Random();
            for (int i = 0; i < colonySize; i++)
            {
                byte[] dna = new byte[3];
                rnd.NextBytes(dna);
                pixels.Add(new Pixel(dna));
                pixels[i].Parents = new Pixel[2];
            }
        }

    }
}

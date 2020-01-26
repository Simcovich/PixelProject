using System;

namespace PixelModel
{
    public class Pixel
    {
        public byte[] DNA { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public Pixel[] Parents { get; set; }
        public Pixel Mate { get; set; }
        public int Generation { get; set; }
        public bool HasMated { get; set; }

        public Pixel(byte[] dna)
        {
            DNA = dna;
            Height = 1;
            Width = 1;
            Generation = 1;
        }
        public Pixel Multiply(Pixel pixel)
        {
            byte[] newArr = new byte[3];
            Random random = new Random();
            for (int i = 0; i < DNA.Length; i++)
            {
                newArr[i] = (byte)random.Next(Math.Min(DNA[i], pixel.DNA[i]), Math.Max(DNA[i], pixel.DNA[i]));
            }
            return new Pixel(newArr) { Parents = new Pixel[2] { this, pixel }} ;
        }
    }
}

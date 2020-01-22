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
            int flag = random.Next(0, 2);
            for (int i = 0; i < DNA.Length; i++)
            {
                if (flag == 1)
                {
                    newArr[i] = this.DNA[i];
                }
                else
                {
                    newArr[i] = pixel.DNA[i];
                }
                flag = random.Next(0, 2);
            }
            return new Pixel(newArr) { Parents = new Pixel[2] { this, pixel }} ;
        }
    }
}

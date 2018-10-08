/*
MIT License

Copyright (c) 2018 Warren Ashcroft

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace ResistorImageGenerator
{
    public class ResistorSeries
    {
        public string Name;
        public double[] Values;
        public int Bands;
        public double Tolerance;
    }
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                CreateResistorSeries();
            }
            else
            {
                using (var bitmap = CreateResistorBitmap(Convert.ToDouble(args[0]), Convert.ToDouble(args[1])))
                {
                    bitmap.Save(args[2], System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }

        static void CreateResistorSeries()
        {
            var resistorSerieses = new ResistorSeries[]
            {
                new ResistorSeries { Name = "E3", Bands = 4, Tolerance = 50, Values = new[]{ 1, 2.2, 4.7 } },
                new ResistorSeries { Name = "E6", Bands = 4, Tolerance = 20, Values = new[]{ 1, 1.5, 2.2, 3.3, 4.7, 6.8 } },
                new ResistorSeries { Name = "E12", Bands = 4, Tolerance = 10, Values = new[]{ 1, 1.2, 1.5, 1.8, 2.2, 2.7, 3.3, 3.9, 4.7, 5.6, 6.8, 8.2 } },
                new ResistorSeries { Name = "E24", Bands = 4, Tolerance = 5, Values = new[]{ 1, 1.1, 1.2, 1.3, 1.5, 1.6, 1.8, 2, 2.2, 2.4, 2.7, 3, 3.3, 3.6, 3.9, 4.3, 4.7, 5.1, 5.6, 6.2, 6.8, 7.5, 8.2, 9.1 } },
                new ResistorSeries { Name = "E48", Bands = 5, Tolerance = 2, Values = new[]{ 1, 1.05, 1.10, 1.15, 1.21, 1.27, 1.33, 1.40, 1.47, 1.54, 1.62, 1.69, 1.78, 1.87, 1.96, 2.05, 2.15, 2.26, 2.37, 2.49, 2.61, 2.74, 2.87, 3.01, 3.16, 3.32, 3.48, 3.65, 3.83, 4.02, 4.22, 4.42, 4.64, 4.87, 5.11, 5.36, 5.62, 5.90, 6.19, 6.49, 6.81, 7.15, 7.50, 7.87, 8.25, 8.66, 9.09, 9.53 } },
                new ResistorSeries { Name = "E96", Bands = 5, Tolerance = 1, Values = new[]{ 1, 1.02, 1.05, 1.07, 1.10, 1.13, 1.15, 1.18, 1.21, 1.24, 1.27, 1.30, 1.33, 1.37, 1.40, 1.43, 1.47, 1.50, 1.54, 1.58, 1.62, 1.65, 1.69, 1.74, 1.78, 1.82, 1.87, 1.91, 1.96, 2, 2.05, 2.10, 2.16, 2.21, 2.26, 2.32, 2.37, 2.43, 2.49, 2.55, 2.61, 2.67, 2.74, 2.80, 2.87, 2.94, 3.01, 3.09, 3.16, 3.24, 3.32, 3.40, 3.48, 3.57, 3.65, 3.74, 3.83, 3.92, 4.02, 4.12, 4.22, 4.32, 4.42, 4.53, 4.64, 4.75, 4.87, 4.99, 5.11, 5.23, 5.36, 5.49, 5.62, 5.76, 5.90, 6.04, 6.19, 6.34, 6.49, 6.65, 6.81, 6.98, 7.15, 7.32, 7.50, 7.68, 7.87, 8.06, 8.25, 8.45, 8.66, 8.87, 9.09, 9.31, 9.53, 9.76 } },
            };

            foreach (var resistorSeries in resistorSerieses)
            {
                var path = Path.Combine(Environment.CurrentDirectory, resistorSeries.Name);
                Directory.CreateDirectory(path);

                foreach (var value in resistorSeries.Values)
                {
                    int multiplier = 1;

                    for (var i = 0; i < 9; i++)
                    {
                        var thisValue = value * multiplier;
                        using (var bitmap = CreateResistorBitmap(thisValue, resistorSeries.Tolerance, resistorSeries.Bands))
                        {
                            bitmap.Save(Path.Combine(path, thisValue.ToString(CultureInfo.InvariantCulture) + ".png"), System.Drawing.Imaging.ImageFormat.Png);
                        }

                        multiplier = multiplier * 10;
                    }
                }
            }
        }

        private static Stream GetResource(string dotFilePath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName().Name;
            return assembly.GetManifestResourceStream(assemblyName + "." + dotFilePath);
        }

        static Bitmap CreateResistorBitmap(double value, double tolerance = 0, int bands = 0)
        {
            int precision = 0;
            Bitmap bitmap = null;
            var significantDigits = value.ToString(System.Globalization.CultureInfo.InvariantCulture).Replace(".", "").TrimEnd('0').ToCharArray();

            if(significantDigits.Length == 3)
            {
                bands = 5;
            }

            switch (bands)
            {
                case 4:
                    precision = 100;
                    break;

                case 5:
                    precision = 1000;
                    break;

                default:
                    precision = (significantDigits.Length == 3) ? 1000 : 100;
                    break;
            }

            if (value > (1000000000))
            {
                throw new ArgumentOutOfRangeException("value too large");
            }
            else if (value < 0.1)
            {
                throw new ArgumentOutOfRangeException("value too small");
            }
            else if (significantDigits.Length > 3)
            {
                throw new ArgumentOutOfRangeException("value too precise");
            }

            double multiplier = 0.01;

            while (true)
            {
                if ((value / multiplier) < precision)
                {
                    value = value / multiplier;
                    break;
                }
                else
                {
                    multiplier = multiplier * 10;
                }
            }

            var digits = value.ToString(System.Globalization.CultureInfo.InvariantCulture).Replace(".", "").ToCharArray();

            try
            {
                // Left [127x82], Digit 1 [21x82], Digit 2 [21x82], (Digit 3 [21x82]), Multiplier [21x82], Spacer [17x82], Tolerance [21x82], Right [127x82]
                int width = 127 + (digits.Length * 21) + 21 + 17 + 21 + 127;
                int height = 182;

                bitmap = new Bitmap(width, height);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    int offset = 0;
                    string colour = "";
                    graphics.Clear(Color.Transparent);

                    // Left
                    using (var stream = GetResource("Images.left.gif"))
                    {
                        using (var image = Image.FromStream(stream))
                        {
                            graphics.DrawImage(image, new Point(offset, 0));
                            offset += 127;
                        }
                    }

                    // Digits
                    foreach (var digit in digits)
                    {
                        switch (digit)
                        {
                            case '0':
                                colour = "black";
                                break;
                            case '1':
                                colour = "brown";
                                break;
                            case '2':
                                colour = "red";
                                break;
                            case '3':
                                colour = "orange";
                                break;
                            case '4':
                                colour = "yellow";
                                break;
                            case '5':
                                colour = "green";
                                break;
                            case '6':
                                colour = "blue";
                                break;
                            case '7':
                                colour = "violet";
                                break;
                            case '8':
                                colour = "grey";
                                break;
                            case '9':
                                colour = "white";
                                break;
                            default:
                                colour = "none";
                                break;
                        }

                        using (var stream = GetResource("Images." + colour + ".gif"))
                        {
                            using (var image = Image.FromStream(stream))
                            {
                                graphics.DrawImage(image, new Point(offset, 0));
                                offset += 21;
                            }
                        }
                    }

                    // Multiplier
                    switch (multiplier)
                    {
                        case 1:
                            colour = "black";
                            break;
                        case 10:
                            colour = "brown";
                            break;
                        case 100:
                            colour = "red";
                            break;
                        case 1000:
                            colour = "orange";
                            break;
                        case 10000:
                            colour = "yellow";
                            break;
                        case 100000:
                            colour = "green";
                            break;
                        case 1000000:
                            colour = "blue";
                            break;
                        case 10000000:
                            colour = "violet";
                            break;
                        case 100000000:
                            colour = "grey";
                            break;
                        case 1000000000:
                            colour = "white";
                            break;
                        case 0.1:
                            colour = "gold";
                            break;
                        case 0.01:
                            colour = "silver";
                            break;
                        default:
                            colour = "none";
                            break;
                    }

                    using (var stream = GetResource("Images." + colour + ".gif"))
                    {
                        using (var image = Image.FromStream(stream))
                        {
                            graphics.DrawImage(image, new Point(offset, 0));
                            offset += 21;
                        }
                    }

                    // Spacer
                    using (var stream = GetResource("Images.spacer.gif"))
                    {
                        using (var image = Image.FromStream(stream))
                        {
                            graphics.DrawImage(image, new Point(offset, 0));
                            offset += 17;
                        }
                    }

                    // Tolerance
                    switch (tolerance)
                    {
                        case 1:
                            colour = "brown";
                            break;
                        case 2:
                            colour = "red";
                            break;
                        case 0.5:
                            colour = "green";
                            break;
                        case 0.25:
                            colour = "blue";
                            break;
                        case 0.1:
                            colour = "violet";
                            break;
                        case 0.05:
                            colour = "grey";
                            break;
                        case 5:
                            colour = "gold";
                            break;
                        case 10:
                            colour = "silver";
                            break;
                        default:
                            colour = "none";
                            break;
                    }

                    using (var stream = GetResource("Images." + colour + ".gif"))
                    {
                        using (var image = Image.FromStream(stream))
                        {
                            graphics.DrawImage(image, new Point(offset, 0));
                            offset += 21;
                        }
                    }

                    // Right
                    using (var stream = GetResource("Images.right.gif"))
                    {
                        using (var image = Image.FromStream(stream))
                        {
                            graphics.DrawImage(image, new Point(offset, 0));
                            offset += 21;
                        }
                    }
                }

                return bitmap;
            }
            catch (Exception ex)
            {
                if (bitmap != null)
                {
                    bitmap.Dispose();
                }

                throw ex;
            }
        }
    }
}
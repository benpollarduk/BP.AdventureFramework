using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BP.AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a Frame for displaying ASCII images.
    /// </summary>
    public class ASCIIImageFrame : Frame
    {
        #region StaticProperties

        /// <summary>
        /// Get the grayscale ASCII dictionary.
        /// </summary>
        public static Dictionary<byte, char> GrayscaleASCIIDictionary { get; private set; } = CreateDefaultASCIIDictionary();

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the lines that make up this ASCII image.
        /// </summary>
        public string[] Lines { get; set; } = new string[0];

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ASCIIImageFrame class.
        /// </summary>
        public ASCIIImageFrame()
        {
            ShowCursor = false;
            AcceptsInput = false;
        }

        /// <summary>
        /// Initializes a new instance of the ASCIIImageFrame class.
        /// </summary>
        /// <param name="lines">Specify the lines of the ASCII image.</param>
        public ASCIIImageFrame(params string[] lines)
        {
            Lines = lines;
            ShowCursor = false;
            AcceptsInput = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Build this ASCIIImageFrame into a text based display.
        /// </summary>
        /// <param name="width">Specify the width of the Frame.</param>
        /// <param name="height">Specify the height of the Frame.</param>
        /// <param name="drawer">The FrameDrawer to draw the Frame with.</param>
        /// <returns>A string representing the Frame</returns>
        public override string BuildFrame(int width, int height, FrameDrawer drawer)
        {
            var builder = new StringBuilder();
            builder.Append(drawer.ConstructDivider(width));
            var asciiDrawing = string.Empty;

            foreach (var t in Lines)
                asciiDrawing += drawer.ConstructCentralisedString(t, width);

            var bufferHeight = height - asciiDrawing.Length / width - 2;

            if (bufferHeight / 2 % 2 != 0)
                bufferHeight -= 1;

            var buffer = drawer.ConstructPaddedArea(width, bufferHeight / 2);

            builder.Append(buffer);
            builder.Append(asciiDrawing);
            builder.Append(buffer);

            if (height - drawer.DetermineLinesInString(builder.ToString()) > 2)
                builder.Append(drawer.ConstructPaddedArea(width, 1));

            var bottomdivider = drawer.ConstructDivider(width);
            builder.Append(bottomdivider.Remove(bottomdivider.Length - 1));

            return builder.ToString();
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Convert a BitmapImage to an ASCII image string.
        /// </summary>
        /// <param name="width">The width of the output ASCII image.</param>
        /// <param name="height">The height of the output ASCII image.</param>
        /// <param name="image">The source image.</param>
        /// <param name="contrast">The contrast between light and dark. Default is 0, use 1 - 100 to increase the contrast, use -1 - -100 to decrease the contrast.</param>
        /// <returns>A ASCII string representation of the source image.</returns>
        public static string[] ConvertToASCIIImageString(int width, int height, Bitmap image, short contrast)
        {
            if (contrast > 100 || contrast < -100)
                throw new ArgumentException("Contrast is not within range (-100 - 100)");

            int resizedWidth;
            int resizedHeight;

            const double courierNewRatio = 1.6d;

            var lines = new string[height];

            var paddedBMP = new Bitmap(width, height);

            if (image.Width - width > image.Height - height)
            {
                resizedWidth = (int)(image.Width * (width / (double)image.Width));
                resizedHeight = (int)(image.Height * (width / (double)image.Width));
            }
            else
            {
                resizedWidth = (int)(image.Width * (height / (double)image.Height));
                resizedHeight = (int)(image.Height * (height / (double)image.Height));
            }

            resizedWidth = (int)(resizedWidth * courierNewRatio);

            for (var rowIndex = 0; rowIndex < height; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < width; columnIndex++)
                {
                    paddedBMP.SetPixel(columnIndex, rowIndex, Color.White);
                }
            }

            using (var g = Graphics.FromImage(paddedBMP))
                g.DrawImage(image, width / 2 - resizedWidth / 2, height / 2 - resizedHeight / 2, resizedWidth, resizedHeight);

            for (var rowIndex = 0; rowIndex < height; rowIndex++)
            {
                var line = string.Empty;

                for (var columnIndex = 0; columnIndex < width; columnIndex++)
                    line += ConvertPixelsToASCII(contrast, paddedBMP.GetPixel(columnIndex, rowIndex));

                lines[rowIndex] = line;
            }

            return lines;
        }

        /// <summary>
        /// Convert a pixel to an ASCII character.
        /// </summary>
        /// <param name="pixel">The pixel to convert.</param>
        /// <param name="contrast">The contrast between light and dark. Default is 0, use 1 - 100 to increase the contrast, use -1 - -100 to decrease the contrast.</param>
        /// <returns>The ACSII representation of the pixel.</returns>
        protected static string ConvertPixelToASCII(Color pixel, short contrast)
        {
            var average = (pixel.R + pixel.G + pixel.B) / 3 * (pixel.A / 255);

            if (average > byte.MaxValue / 2)
                average = Math.Min(average + contrast, byte.MaxValue);
            else
                average = Math.Max(average - contrast, 0);

            return GrayscaleASCIIDictionary[(byte)average].ToString();
        }

        /// <summary>
        /// Convert some pixels to an ASCII character
        /// </summary>
        /// <param name="contrast">The contrast between light and dark. Default is 0, use 1 - 100 to increase the contrast, use -1 - -100 to decrease the contrast</param>
        /// <param name="pixels">The pixels to convert</param>
        /// <returns>The ACSII representation of the pixels</returns>
        protected static string ConvertPixelsToASCII(short contrast, params Color[] pixels)
        {
            var r = 0;
            var g = 0;
            var b = 0;

            for (var pixelIndex = 0; pixelIndex < pixels.Length; pixelIndex++)
            {
                r += pixels[pixelIndex].R;
                g += pixels[pixelIndex].G;
                b += pixels[pixelIndex].B;
            }

            return ConvertPixelToASCII(Color.FromArgb(r /= pixels.Length, g /= pixels.Length, b /= pixels.Length), contrast);
        }

        /// <summary>
        /// Create a new ASCIIImageFrame.
        /// </summary>
        /// <param name="image">The image to use as the source.</param>
        /// <param name="width">Specify the width of the ASCII image.</param>
        /// <param name="height">Specify the height of the ASCII image.</param>
        /// <returns>A new ASCIIImageFrame created from the parameters.</returns>
        public static ASCIIImageFrame Create(Image image, int width, int height)
        {
            return Create(image as Bitmap, width, height, 0);
        }

        /// <summary>
        /// Create a new ASCIIImageFrame.
        /// </summary>
        /// <param name="image">The image to use as the source.</param>
        /// <param name="width">Specify the width of the ASCII image.</param>
        /// <param name="height">Specify the height of the ASCII image.</param>
        /// <param name="contrast">The contrast between light and dark. Default is 0, use 1 - 100 to increase the contrast, use -1 - -100 to decrease the contrast.</param>
        /// <returns>A new ASCIIImageFrame created from the parameters.</returns>
        public static ASCIIImageFrame Create(Image image, int width, int height, short contrast)
        {
            return Create(image as Bitmap, width, height, contrast);
        }

        /// <summary>
        /// Create a new ASCIIImageFrame.
        /// </summary>
        /// <param name="image">The image to use as the source.</param>
        /// <param name="width">Specify the width of the ASCII image.</param>
        /// <param name="height">Specify the height of the ASCII image.</param>
        /// <returns>A new ASCIIImageFrame created from the parameters.</returns>
        public static ASCIIImageFrame Create(Bitmap image, int width, int height)
        {
            return new ASCIIImageFrame { Lines = ConvertToASCIIImageString(width - 4, height - 3, image, 0) };
        }

        /// <summary>
        /// Create a new ASCIIImageFrame.
        /// </summary>
        /// <param name="image">The image to use as the source.</param>
        /// <param name="width">Specify the width of the ASCII image.</param>
        /// <param name="height">Specify the height of the ASCII image.</param>
        /// <param name="contrast">The contrast between light and dark. Default is 0, use 1 - 100 to increase the contrast, use -1 - -100 to decrease the contrast.</param>
        /// <returns>A new ASCIIImageFrame created from the parameters.</returns>
        public static ASCIIImageFrame Create(Bitmap image, int width, int height, short contrast)
        {
            return new ASCIIImageFrame { Lines = ConvertToASCIIImageString(width - 4, height - 3, image, contrast) };
        }

        /// <summary>
        /// Create a new default ASCII dictionary.
        /// </summary>
        /// <returns>A standard ASCII dictionary.</returns>
        public static Dictionary<byte, char> CreateDefaultASCIIDictionary()
        {
            // standard character ramp
            //$@B%8&WM#*oahkbdpqwmZO0QLCJUYXzcvunxrjft/\|()1{}[]?-_+~<>i!lI;:,"^`'. http://paulbourke.net/dataformats/asciiart/

            // create dictionary
            return new Dictionary<byte, char>
            {
                { 0, Convert.ToChar("$") },
                { 1, Convert.ToChar("$") },
                { 2, Convert.ToChar("$") },
                { 3, Convert.ToChar("$") },
                { 4, Convert.ToChar("@") },
                { 5, Convert.ToChar("@") },
                { 6, Convert.ToChar("@") },
                { 7, Convert.ToChar("@") },
                { 8, Convert.ToChar("B") },
                { 9, Convert.ToChar("B") },
                { 10, Convert.ToChar("B") },
                { 11, Convert.ToChar("B") },
                { 12, Convert.ToChar("%") },
                { 13, Convert.ToChar("%") },
                { 14, Convert.ToChar("%") },
                { 15, Convert.ToChar("%") },
                { 16, Convert.ToChar("8") },
                { 17, Convert.ToChar("8") },
                { 18, Convert.ToChar("8") },
                { 19, Convert.ToChar("8") },
                { 20, Convert.ToChar("&") },
                { 21, Convert.ToChar("&") },
                { 22, Convert.ToChar("&") },
                { 23, Convert.ToChar("&") },
                { 24, Convert.ToChar("W") },
                { 25, Convert.ToChar("W") },
                { 26, Convert.ToChar("W") },
                { 27, Convert.ToChar("W") },
                { 28, Convert.ToChar("M") },
                { 29, Convert.ToChar("M") },
                { 30, Convert.ToChar("M") },
                { 31, Convert.ToChar("M") },
                { 32, Convert.ToChar("#") },
                { 33, Convert.ToChar("#") },
                { 34, Convert.ToChar("#") },
                { 35, Convert.ToChar("#") },
                { 36, Convert.ToChar("*") },
                { 37, Convert.ToChar("*") },
                { 38, Convert.ToChar("*") },
                { 39, Convert.ToChar("*") },
                { 40, Convert.ToChar("o") },
                { 41, Convert.ToChar("o") },
                { 42, Convert.ToChar("o") },
                { 43, Convert.ToChar("o") },
                { 44, Convert.ToChar("a") },
                { 45, Convert.ToChar("a") },
                { 46, Convert.ToChar("a") },
                { 47, Convert.ToChar("a") },
                { 48, Convert.ToChar("h") },
                { 49, Convert.ToChar("h") },
                { 50, Convert.ToChar("h") },
                { 51, Convert.ToChar("h") },
                { 52, Convert.ToChar("k") },
                { 53, Convert.ToChar("k") },
                { 54, Convert.ToChar("k") },
                { 55, Convert.ToChar("k") },
                { 56, Convert.ToChar("b") },
                { 57, Convert.ToChar("b") },
                { 58, Convert.ToChar("b") },
                { 59, Convert.ToChar("b") },
                { 60, Convert.ToChar("d") },
                { 61, Convert.ToChar("d") },
                { 62, Convert.ToChar("d") },
                { 63, Convert.ToChar("d") },
                { 64, Convert.ToChar("p") },
                { 65, Convert.ToChar("p") },
                { 66, Convert.ToChar("p") },
                { 67, Convert.ToChar("p") },
                { 68, Convert.ToChar("q") },
                { 69, Convert.ToChar("q") },
                { 70, Convert.ToChar("q") },
                { 71, Convert.ToChar("q") },
                { 72, Convert.ToChar("w") },
                { 73, Convert.ToChar("w") },
                { 74, Convert.ToChar("w") },
                { 75, Convert.ToChar("w") },
                { 76, Convert.ToChar("m") },
                { 77, Convert.ToChar("m") },
                { 78, Convert.ToChar("m") },
                { 79, Convert.ToChar("m") },
                { 80, Convert.ToChar("Z") },
                { 81, Convert.ToChar("Z") },
                { 82, Convert.ToChar("Z") },
                { 83, Convert.ToChar("Z") },
                { 84, Convert.ToChar("O") },
                { 85, Convert.ToChar("O") },
                { 86, Convert.ToChar("O") },
                { 87, Convert.ToChar("O") },
                { 88, Convert.ToChar("0") },
                { 89, Convert.ToChar("0") },
                { 90, Convert.ToChar("0") },
                { 91, Convert.ToChar("0") },
                { 92, Convert.ToChar("Q") },
                { 93, Convert.ToChar("Q") },
                { 94, Convert.ToChar("Q") },
                { 95, Convert.ToChar("Q") },
                { 96, Convert.ToChar("L") },
                { 97, Convert.ToChar("L") },
                { 98, Convert.ToChar("L") },
                { 99, Convert.ToChar("L") },
                { 100, Convert.ToChar("C") },
                { 101, Convert.ToChar("C") },
                { 102, Convert.ToChar("C") },
                { 103, Convert.ToChar("C") },
                { 104, Convert.ToChar("J") },
                { 105, Convert.ToChar("J") },
                { 106, Convert.ToChar("J") },
                { 107, Convert.ToChar("J") },
                { 108, Convert.ToChar("U") },
                { 109, Convert.ToChar("U") },
                { 110, Convert.ToChar("U") },
                { 111, Convert.ToChar("U") },
                { 112, Convert.ToChar("Y") },
                { 113, Convert.ToChar("Y") },
                { 114, Convert.ToChar("Y") },
                { 115, Convert.ToChar("Y") },
                { 116, Convert.ToChar("X") },
                { 117, Convert.ToChar("X") },
                { 118, Convert.ToChar("X") },
                { 119, Convert.ToChar("X") },
                { 120, Convert.ToChar("z") },
                { 121, Convert.ToChar("z") },
                { 122, Convert.ToChar("z") },
                { 123, Convert.ToChar("z") },
                { 124, Convert.ToChar("c") },
                { 125, Convert.ToChar("c") },
                { 126, Convert.ToChar("c") },
                { 127, Convert.ToChar("c") },
                { 128, Convert.ToChar("v") },
                { 129, Convert.ToChar("v") },
                { 130, Convert.ToChar("v") },
                { 131, Convert.ToChar("v") },
                { 132, Convert.ToChar("u") },
                { 133, Convert.ToChar("u") },
                { 134, Convert.ToChar("u") },
                { 135, Convert.ToChar("u") },
                { 136, Convert.ToChar("n") },
                { 137, Convert.ToChar("n") },
                { 138, Convert.ToChar("n") },
                { 139, Convert.ToChar("n") },
                { 140, Convert.ToChar("x") },
                { 141, Convert.ToChar("x") },
                { 142, Convert.ToChar("x") },
                { 143, Convert.ToChar("x") },
                { 144, Convert.ToChar("r") },
                { 145, Convert.ToChar("r") },
                { 146, Convert.ToChar("r") },
                { 147, Convert.ToChar("r") },
                { 148, Convert.ToChar("j") },
                { 149, Convert.ToChar("j") },
                { 150, Convert.ToChar("j") },
                { 151, Convert.ToChar("j") },
                { 152, Convert.ToChar("f") },
                { 153, Convert.ToChar("f") },
                { 154, Convert.ToChar("f") },
                { 155, Convert.ToChar("f") },
                { 156, Convert.ToChar("t") },
                { 157, Convert.ToChar("t") },
                { 158, Convert.ToChar("t") },
                { 159, Convert.ToChar("t") },
                { 160, Convert.ToChar("/") },
                { 161, Convert.ToChar("/") },
                { 162, Convert.ToChar("/") },
                { 163, Convert.ToChar("/") },
                { 164, Convert.ToChar("\\") },
                { 165, Convert.ToChar("\\") },
                { 166, Convert.ToChar("\\") },
                { 167, Convert.ToChar("\\") },
                { 168, Convert.ToChar("|") },
                { 169, Convert.ToChar("|") },
                { 170, Convert.ToChar("|") },
                { 171, Convert.ToChar("|") },
                { 172, Convert.ToChar("(") },
                { 173, Convert.ToChar("(") },
                { 174, Convert.ToChar("(") },
                { 175, Convert.ToChar("(") },
                { 176, Convert.ToChar("1") },
                { 177, Convert.ToChar("1") },
                { 178, Convert.ToChar("1") },
                { 179, Convert.ToChar("1") },
                { 180, Convert.ToChar("{") },
                { 181, Convert.ToChar("{") },
                { 182, Convert.ToChar("{") },
                { 183, Convert.ToChar("{") },
                { 184, Convert.ToChar("[") },
                { 185, Convert.ToChar("[") },
                { 186, Convert.ToChar("[") },
                { 187, Convert.ToChar("[") },
                { 188, Convert.ToChar("?") },
                { 189, Convert.ToChar("?") },
                { 190, Convert.ToChar("?") },
                { 191, Convert.ToChar("?") },
                { 192, Convert.ToChar("-") },
                { 193, Convert.ToChar("-") },
                { 194, Convert.ToChar("-") },
                { 195, Convert.ToChar("-") },
                { 196, Convert.ToChar("_") },
                { 197, Convert.ToChar("_") },
                { 198, Convert.ToChar("_") },
                { 199, Convert.ToChar("_") },
                { 200, Convert.ToChar("+") },
                { 201, Convert.ToChar("+") },
                { 202, Convert.ToChar("+") },
                { 203, Convert.ToChar("+") },
                { 204, Convert.ToChar("~") },
                { 205, Convert.ToChar("~") },
                { 206, Convert.ToChar("~") },
                { 207, Convert.ToChar("~") },
                { 208, Convert.ToChar("<") },
                { 209, Convert.ToChar("<") },
                { 210, Convert.ToChar("<") },
                { 211, Convert.ToChar("<") },
                { 212, Convert.ToChar("i") },
                { 213, Convert.ToChar("i") },
                { 214, Convert.ToChar("i") },
                { 215, Convert.ToChar("i") },
                { 216, Convert.ToChar("!") },
                { 217, Convert.ToChar("!") },
                { 218, Convert.ToChar("!") },
                { 219, Convert.ToChar("!") },
                { 220, Convert.ToChar("l") },
                { 221, Convert.ToChar("l") },
                { 222, Convert.ToChar("l") },
                { 223, Convert.ToChar("l") },
                { 224, Convert.ToChar("I") },
                { 225, Convert.ToChar("I") },
                { 226, Convert.ToChar("I") },
                { 227, Convert.ToChar("I") },
                { 228, Convert.ToChar(";") },
                { 229, Convert.ToChar(";") },
                { 230, Convert.ToChar(";") },
                { 231, Convert.ToChar(";") },
                { 232, Convert.ToChar(":") },
                { 233, Convert.ToChar(":") },
                { 234, Convert.ToChar(":") },
                { 235, Convert.ToChar(":") },
                { 236, Convert.ToChar(",") },
                { 237, Convert.ToChar(",") },
                { 238, Convert.ToChar(",") },
                { 239, Convert.ToChar(",") },
                { 240, Convert.ToChar("\"") },
                { 241, Convert.ToChar("\"") },
                { 242, Convert.ToChar("\"") },
                { 243, Convert.ToChar("^") },
                { 244, Convert.ToChar("^") },
                { 245, Convert.ToChar("^") },
                { 246, Convert.ToChar("`") },
                { 247, Convert.ToChar("`") },
                { 248, Convert.ToChar("'") },
                { 249, Convert.ToChar("'") },
                { 250, Convert.ToChar("'") },
                { 251, Convert.ToChar(".") },
                { 252, Convert.ToChar(".") },
                { 253, Convert.ToChar(".") },
                { 254, Convert.ToChar(" ") },
                { 255, Convert.ToChar(" ") }
            };
        }

        #endregion
    }
}
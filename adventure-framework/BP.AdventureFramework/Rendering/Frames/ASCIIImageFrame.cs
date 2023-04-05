using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a Frame for displaying ASCII images
    /// </summary>
    public class ASCIIImageFrame : Frame
    {
        #region StaticProperties

        /// <summary>
        /// Get the grayscale ASCII dictionary
        /// </summary>
        public static Dictionary<byte, char> GrayscaleASCIIDictionary
        {
            get { return grayscaleASCIIDictionary; }
            private set { grayscaleASCIIDictionary = value; }
        }

        /// <summary>
        /// Get or set the grayscale ASCII dictionary
        /// </summary>
        private static Dictionary<byte, char> grayscaleASCIIDictionary = CreateDefaultASCIIDictionary();

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the lines that make up this ASCII image
        /// </summary>
        public string[] Lines
        {
            get { return lines; }
            set { lines = value; }
        }

        /// <summary>
        /// Get or set the lines that make up this ASCII image
        /// </summary>
        private string[] lines = new string[0];

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the ASCIIImageFrame class
        /// </summary>
        public ASCIIImageFrame()
        {
            // don't show cursor
            ShowCursor = false;

            // don't allow input
            AcceptsInput = false;
        }

        /// <summary>
        /// Initializes a new instance of the ASCIIImageFrame class
        /// </summary>
        /// <param name="lines">Specify the lines of the ASCII image</param>
        public ASCIIImageFrame(params string[] lines)
        {
            // set lines
            Lines = lines;

            // don't show cursor
            ShowCursor = false;

            // don't allow input
            AcceptsInput = false;
        }

        /// <summary>
        /// Build this ASCIIImageFrame into a text based display
        /// </summary>
        /// <param name="width">Specify the width of the Frame</param>
        /// <param name="height">Specify the height of the Frame</param>
        /// <param name="drawer">The FrameDrawer to draw the Frame with</param>
        /// <returns>A string representing the Frame</returns>
        public override string BuildFrame(int width, int height, FrameDrawer drawer)
        {
            // create builder
            var builder = new StringBuilder();

            // add
            builder.Append(drawer.ConstructDevider(width));

            // hold ascii drawing
            var asciiDrawing = string.Empty;

            // itterate each line
            for (var index = 0; index < Lines.Length; index++)
                // append line
                asciiDrawing += drawer.ConstructCentralisedString(Lines[index], width);

            // hold buffer height
            var bufferHeight = height - asciiDrawing.Length / width - 2;

            // if buffer is odd
            if (bufferHeight / 2 % 2 != 0)
                // reduce by one
                bufferHeight -= 1;

            // hold buffer
            var buffer = drawer.ConstructPaddedArea(width, bufferHeight / 2);

            // add buffer
            builder.Append(buffer);

            // add drawing
            builder.Append(asciiDrawing);

            // add buffer
            builder.Append(buffer);

            // may need additional line
            if (height - drawer.DetermineLinesInString(builder.ToString()) > 2)
                // append extra line
                builder.Append(drawer.ConstructPaddedArea(width, 1));

            // hold bottom devider
            var bottomDevider = drawer.ConstructDevider(width);

            // add bottom devider removing the last \n
            builder.Append(bottomDevider.Remove(bottomDevider.Length - 1));

            // return frame
            return builder.ToString();
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Convert a BitmapImage to an ASCII image string
        /// </summary>
        /// <param name="width">The width of the output ASCII image</param>
        /// <param name="height">The height of the output ASCII image</param>
        /// <param name="image">The source image</param>
        /// <param name="contrast">The contrast between light and dark. Default is 0, use 1 - 100 to increase the contrast, use -1 - -100 to decrease the contrast</param>
        /// <returns>A ASCII string representation of the source image</returns>
        public static string[] ConvertToASCIIImageString(int width, int height, Bitmap image, short contrast)
        {
            // if contrast is out of range
            if (contrast > 100 || contrast < -100)
                // throw exception
                throw new ArgumentException("Contrast is not within range (-100 - 100)");

            // hold new sizes
            int resizedWidth;
            int resizedHeight;

            // hold the ratio for courier new (5:3 or 1.6)
            const double courierNewRatio = 1.6d;

            // hold lines
            var lines = new string[height];

            // get bitmap
            var paddedBMP = new Bitmap(width, height);

            // if width difference is greater than height difference
            if (image.Width - width > image.Height - height)
            {
                // hold width
                resizedWidth = (int)(image.Width * (width / (double)image.Width));

                // hold height
                resizedHeight = (int)(image.Height * (width / (double)image.Width));
            }
            else
            {
                // hold width
                resizedWidth = (int)(image.Width * (height / (double)image.Height));

                // hold height
                resizedHeight = (int)(image.Height * (height / (double)image.Height));
            }

            // redefine width based on cell size of courier new
            resizedWidth = (int)(resizedWidth * courierNewRatio);

            // itterate rows
            for (var rowIndex = 0; rowIndex < height; rowIndex++)
                // itterate columns
            for (var columnIndex = 0; columnIndex < width; columnIndex++)
                // set white
                paddedBMP.SetPixel(columnIndex, rowIndex, Color.White);

            // create grahics
            using (var g = Graphics.FromImage(paddedBMP))
            {
                // draw the image
                g.DrawImage(image, width / 2 - resizedWidth / 2, height / 2 - resizedHeight / 2, resizedWidth, resizedHeight);
            }

            // itterate rows
            for (var rowIndex = 0; rowIndex < height; rowIndex++)
            {
                // hold line
                var line = string.Empty;

                // itterate columns
                for (var columnIndex = 0; columnIndex < width; columnIndex++)
                    // get ASCII
                    line += ConvertPixelsToASCII(contrast, paddedBMP.GetPixel(columnIndex, rowIndex));

                // add line
                lines[rowIndex] = line;
            }

            // return lines
            return lines;
        }

        /// <summary>
        /// Convert a pixel to an ASCII character
        /// </summary>
        /// <param name="pixel">The pixel to convert</param>
        /// <param name="contrast">The contrast between light and dark. Default is 0, use 1 - 100 to increase the contrast, use -1 - -100 to decrease the contrast</param>
        /// <returns>The ACSII representation of the pixel</returns>
        protected static string ConvertPixelToASCII(Color pixel, short contrast)
        {
            // get average colour
            var average = (pixel.R + pixel.G + pixel.B) / 3 * (pixel.A / 255);

            // if in light range
            if (average > byte.MaxValue / 2)
                // set average or limit
                average = Math.Min(average + contrast, byte.MaxValue);
            else
                // set to max or limit
                average = Math.Max(average - contrast, 0);

            // look up ascii character
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
            // hold r g and b values
            var r = 0;
            var g = 0;
            var b = 0;

            // itterate all pixels
            for (var pixelIndex = 0; pixelIndex < pixels.Length; pixelIndex++)
            {
                // add r
                r += pixels[pixelIndex].R;

                // add g
                g += pixels[pixelIndex].G;

                // add blue
                b += pixels[pixelIndex].B;
            }

            // get character from average pixel
            return ConvertPixelToASCII(Color.FromArgb(r /= pixels.Length, g /= pixels.Length, b /= pixels.Length), contrast);
        }

        /// <summary>
        /// Create a new ASCIIImageFrame
        /// </summary>
        /// <param name="image">The image to use as the source</param>
        /// <param name="width">Specify the width of the ASCII image</param>
        /// <param name="height">Specify the height of the ASCII image</param>
        /// <returns>A new ASCIIImageFrame created from the parameters</returns>
        public static ASCIIImageFrame Create(Image image, int width, int height)
        {
            // return created frame
            return Create(image as Bitmap, width, height, 0);
        }

        /// <summary>
        /// Create a new ASCIIImageFrame
        /// </summary>
        /// <param name="image">The image to use as the source</param>
        /// <param name="width">Specify the width of the ASCII image</param>
        /// <param name="height">Specify the height of the ASCII image</param>
        /// <param name="contrast">The contrast between light and dark. Default is 0, use 1 - 100 to increase the contrast, use -1 - -100 to decrease the contrast</param>
        /// <returns>A new ASCIIImageFrame created from the parameters</returns>
        public static ASCIIImageFrame Create(Image image, int width, int height, short contrast)
        {
            // return created frame
            return Create(image as Bitmap, width, height, contrast);
        }

        /// <summary>
        /// Create a new ASCIIImageFrame
        /// </summary>
        /// <param name="image">The image to use as the source</param>
        /// <param name="width">Specify the width of the ASCII image</param>
        /// <param name="height">Specify the height of the ASCII image</param>
        /// <returns>A new ASCIIImageFrame created from the parameters</returns>
        public static ASCIIImageFrame Create(Bitmap image, int width, int height)
        {
            // create frame
            var frame = new ASCIIImageFrame();

            // set lines
            frame.Lines = ConvertToASCIIImageString(width - 4, height - 3, image, 0);

            // retunr the frame
            return frame;
        }

        /// <summary>
        /// Create a new ASCIIImageFrame
        /// </summary>
        /// <param name="image">The image to use as the source</param>
        /// <param name="width">Specify the width of the ASCII image</param>
        /// <param name="height">Specify the height of the ASCII image</param>
        /// <param name="contrast">The contrast between light and dark. Default is 0, use 1 - 100 to increase the contrast, use -1 - -100 to decrease the contrast</param>
        /// <returns>A new ASCIIImageFrame created from the parameters</returns>
        public static ASCIIImageFrame Create(Bitmap image, int width, int height, short contrast)
        {
            // create frame
            var frame = new ASCIIImageFrame();

            // set lines
            frame.Lines = ConvertToASCIIImageString(width - 4, height - 3, image, contrast);

            // retunr the frame
            return frame;
        }

        /// <summary>
        /// Create a new default ASCII dictionary
        /// </summary>
        /// <returns>A standard ASCII dictionary</returns>
        public static Dictionary<byte, char> CreateDefaultASCIIDictionary()
        {
            // create dictionary
            var dict = new Dictionary<byte, char>();

            // standard character ramp
            //$@B%8&WM#*oahkbdpqwmZO0QLCJUYXzcvunxrjft/\|()1{}[]?-_+~<>i!lI;:,"^`'. http://paulbourke.net/dataformats/asciiart/

            // add elements
            dict.Add(0, Convert.ToChar("$"));
            dict.Add(1, Convert.ToChar("$"));
            dict.Add(2, Convert.ToChar("$"));
            dict.Add(3, Convert.ToChar("$"));
            dict.Add(4, Convert.ToChar("@"));
            dict.Add(5, Convert.ToChar("@"));
            dict.Add(6, Convert.ToChar("@"));
            dict.Add(7, Convert.ToChar("@"));
            dict.Add(8, Convert.ToChar("B"));
            dict.Add(9, Convert.ToChar("B"));
            dict.Add(10, Convert.ToChar("B"));
            dict.Add(11, Convert.ToChar("B"));
            dict.Add(12, Convert.ToChar("%"));
            dict.Add(13, Convert.ToChar("%"));
            dict.Add(14, Convert.ToChar("%"));
            dict.Add(15, Convert.ToChar("%"));
            dict.Add(16, Convert.ToChar("8"));
            dict.Add(17, Convert.ToChar("8"));
            dict.Add(18, Convert.ToChar("8"));
            dict.Add(19, Convert.ToChar("8"));
            dict.Add(20, Convert.ToChar("&"));
            dict.Add(21, Convert.ToChar("&"));
            dict.Add(22, Convert.ToChar("&"));
            dict.Add(23, Convert.ToChar("&"));
            dict.Add(24, Convert.ToChar("W"));
            dict.Add(25, Convert.ToChar("W"));
            dict.Add(26, Convert.ToChar("W"));
            dict.Add(27, Convert.ToChar("W"));
            dict.Add(28, Convert.ToChar("M"));
            dict.Add(29, Convert.ToChar("M"));
            dict.Add(30, Convert.ToChar("M"));
            dict.Add(31, Convert.ToChar("M"));
            dict.Add(32, Convert.ToChar("#"));
            dict.Add(33, Convert.ToChar("#"));
            dict.Add(34, Convert.ToChar("#"));
            dict.Add(35, Convert.ToChar("#"));
            dict.Add(36, Convert.ToChar("*"));
            dict.Add(37, Convert.ToChar("*"));
            dict.Add(38, Convert.ToChar("*"));
            dict.Add(39, Convert.ToChar("*"));
            dict.Add(40, Convert.ToChar("o"));
            dict.Add(41, Convert.ToChar("o"));
            dict.Add(42, Convert.ToChar("o"));
            dict.Add(43, Convert.ToChar("o"));
            dict.Add(44, Convert.ToChar("a"));
            dict.Add(45, Convert.ToChar("a"));
            dict.Add(46, Convert.ToChar("a"));
            dict.Add(47, Convert.ToChar("a"));
            dict.Add(48, Convert.ToChar("h"));
            dict.Add(49, Convert.ToChar("h"));
            dict.Add(50, Convert.ToChar("h"));
            dict.Add(51, Convert.ToChar("h"));
            dict.Add(52, Convert.ToChar("k"));
            dict.Add(53, Convert.ToChar("k"));
            dict.Add(54, Convert.ToChar("k"));
            dict.Add(55, Convert.ToChar("k"));
            dict.Add(56, Convert.ToChar("b"));
            dict.Add(57, Convert.ToChar("b"));
            dict.Add(58, Convert.ToChar("b"));
            dict.Add(59, Convert.ToChar("b"));
            dict.Add(60, Convert.ToChar("d"));
            dict.Add(61, Convert.ToChar("d"));
            dict.Add(62, Convert.ToChar("d"));
            dict.Add(63, Convert.ToChar("d"));
            dict.Add(64, Convert.ToChar("p"));
            dict.Add(65, Convert.ToChar("p"));
            dict.Add(66, Convert.ToChar("p"));
            dict.Add(67, Convert.ToChar("p"));
            dict.Add(68, Convert.ToChar("q"));
            dict.Add(69, Convert.ToChar("q"));
            dict.Add(70, Convert.ToChar("q"));
            dict.Add(71, Convert.ToChar("q"));
            dict.Add(72, Convert.ToChar("w"));
            dict.Add(73, Convert.ToChar("w"));
            dict.Add(74, Convert.ToChar("w"));
            dict.Add(75, Convert.ToChar("w"));
            dict.Add(76, Convert.ToChar("m"));
            dict.Add(77, Convert.ToChar("m"));
            dict.Add(78, Convert.ToChar("m"));
            dict.Add(79, Convert.ToChar("m"));
            dict.Add(80, Convert.ToChar("Z"));
            dict.Add(81, Convert.ToChar("Z"));
            dict.Add(82, Convert.ToChar("Z"));
            dict.Add(83, Convert.ToChar("Z"));
            dict.Add(84, Convert.ToChar("O"));
            dict.Add(85, Convert.ToChar("O"));
            dict.Add(86, Convert.ToChar("O"));
            dict.Add(87, Convert.ToChar("O"));
            dict.Add(88, Convert.ToChar("0"));
            dict.Add(89, Convert.ToChar("0"));
            dict.Add(90, Convert.ToChar("0"));
            dict.Add(91, Convert.ToChar("0"));
            dict.Add(92, Convert.ToChar("Q"));
            dict.Add(93, Convert.ToChar("Q"));
            dict.Add(94, Convert.ToChar("Q"));
            dict.Add(95, Convert.ToChar("Q"));
            dict.Add(96, Convert.ToChar("L"));
            dict.Add(97, Convert.ToChar("L"));
            dict.Add(98, Convert.ToChar("L"));
            dict.Add(99, Convert.ToChar("L"));
            dict.Add(100, Convert.ToChar("C"));
            dict.Add(101, Convert.ToChar("C"));
            dict.Add(102, Convert.ToChar("C"));
            dict.Add(103, Convert.ToChar("C"));
            dict.Add(104, Convert.ToChar("J"));
            dict.Add(105, Convert.ToChar("J"));
            dict.Add(106, Convert.ToChar("J"));
            dict.Add(107, Convert.ToChar("J"));
            dict.Add(108, Convert.ToChar("U"));
            dict.Add(109, Convert.ToChar("U"));
            dict.Add(110, Convert.ToChar("U"));
            dict.Add(111, Convert.ToChar("U"));
            dict.Add(112, Convert.ToChar("Y"));
            dict.Add(113, Convert.ToChar("Y"));
            dict.Add(114, Convert.ToChar("Y"));
            dict.Add(115, Convert.ToChar("Y"));
            dict.Add(116, Convert.ToChar("X"));
            dict.Add(117, Convert.ToChar("X"));
            dict.Add(118, Convert.ToChar("X"));
            dict.Add(119, Convert.ToChar("X"));
            dict.Add(120, Convert.ToChar("z"));
            dict.Add(121, Convert.ToChar("z"));
            dict.Add(122, Convert.ToChar("z"));
            dict.Add(123, Convert.ToChar("z"));
            dict.Add(124, Convert.ToChar("c"));
            dict.Add(125, Convert.ToChar("c"));
            dict.Add(126, Convert.ToChar("c"));
            dict.Add(127, Convert.ToChar("c"));
            dict.Add(128, Convert.ToChar("v"));
            dict.Add(129, Convert.ToChar("v"));
            dict.Add(130, Convert.ToChar("v"));
            dict.Add(131, Convert.ToChar("v"));
            dict.Add(132, Convert.ToChar("u"));
            dict.Add(133, Convert.ToChar("u"));
            dict.Add(134, Convert.ToChar("u"));
            dict.Add(135, Convert.ToChar("u"));
            dict.Add(136, Convert.ToChar("n"));
            dict.Add(137, Convert.ToChar("n"));
            dict.Add(138, Convert.ToChar("n"));
            dict.Add(139, Convert.ToChar("n"));
            dict.Add(140, Convert.ToChar("x"));
            dict.Add(141, Convert.ToChar("x"));
            dict.Add(142, Convert.ToChar("x"));
            dict.Add(143, Convert.ToChar("x"));
            dict.Add(144, Convert.ToChar("r"));
            dict.Add(145, Convert.ToChar("r"));
            dict.Add(146, Convert.ToChar("r"));
            dict.Add(147, Convert.ToChar("r"));
            dict.Add(148, Convert.ToChar("j"));
            dict.Add(149, Convert.ToChar("j"));
            dict.Add(150, Convert.ToChar("j"));
            dict.Add(151, Convert.ToChar("j"));
            dict.Add(152, Convert.ToChar("f"));
            dict.Add(153, Convert.ToChar("f"));
            dict.Add(154, Convert.ToChar("f"));
            dict.Add(155, Convert.ToChar("f"));
            dict.Add(156, Convert.ToChar("t"));
            dict.Add(157, Convert.ToChar("t"));
            dict.Add(158, Convert.ToChar("t"));
            dict.Add(159, Convert.ToChar("t"));
            dict.Add(160, Convert.ToChar("/"));
            dict.Add(161, Convert.ToChar("/"));
            dict.Add(162, Convert.ToChar("/"));
            dict.Add(163, Convert.ToChar("/"));
            dict.Add(164, Convert.ToChar("\\"));
            dict.Add(165, Convert.ToChar("\\"));
            dict.Add(166, Convert.ToChar("\\"));
            dict.Add(167, Convert.ToChar("\\"));
            dict.Add(168, Convert.ToChar("|"));
            dict.Add(169, Convert.ToChar("|"));
            dict.Add(170, Convert.ToChar("|"));
            dict.Add(171, Convert.ToChar("|"));
            dict.Add(172, Convert.ToChar("("));
            dict.Add(173, Convert.ToChar("("));
            dict.Add(174, Convert.ToChar("("));
            dict.Add(175, Convert.ToChar("("));
            dict.Add(176, Convert.ToChar("1"));
            dict.Add(177, Convert.ToChar("1"));
            dict.Add(178, Convert.ToChar("1"));
            dict.Add(179, Convert.ToChar("1"));
            dict.Add(180, Convert.ToChar("{"));
            dict.Add(181, Convert.ToChar("{"));
            dict.Add(182, Convert.ToChar("{"));
            dict.Add(183, Convert.ToChar("{"));
            dict.Add(184, Convert.ToChar("["));
            dict.Add(185, Convert.ToChar("["));
            dict.Add(186, Convert.ToChar("["));
            dict.Add(187, Convert.ToChar("["));
            dict.Add(188, Convert.ToChar("?"));
            dict.Add(189, Convert.ToChar("?"));
            dict.Add(190, Convert.ToChar("?"));
            dict.Add(191, Convert.ToChar("?"));
            dict.Add(192, Convert.ToChar("-"));
            dict.Add(193, Convert.ToChar("-"));
            dict.Add(194, Convert.ToChar("-"));
            dict.Add(195, Convert.ToChar("-"));
            dict.Add(196, Convert.ToChar("_"));
            dict.Add(197, Convert.ToChar("_"));
            dict.Add(198, Convert.ToChar("_"));
            dict.Add(199, Convert.ToChar("_"));
            dict.Add(200, Convert.ToChar("+"));
            dict.Add(201, Convert.ToChar("+"));
            dict.Add(202, Convert.ToChar("+"));
            dict.Add(203, Convert.ToChar("+"));
            dict.Add(204, Convert.ToChar("~"));
            dict.Add(205, Convert.ToChar("~"));
            dict.Add(206, Convert.ToChar("~"));
            dict.Add(207, Convert.ToChar("~"));
            dict.Add(208, Convert.ToChar("<"));
            dict.Add(209, Convert.ToChar("<"));
            dict.Add(210, Convert.ToChar("<"));
            dict.Add(211, Convert.ToChar("<"));
            dict.Add(212, Convert.ToChar("i"));
            dict.Add(213, Convert.ToChar("i"));
            dict.Add(214, Convert.ToChar("i"));
            dict.Add(215, Convert.ToChar("i"));
            dict.Add(216, Convert.ToChar("!"));
            dict.Add(217, Convert.ToChar("!"));
            dict.Add(218, Convert.ToChar("!"));
            dict.Add(219, Convert.ToChar("!"));
            dict.Add(220, Convert.ToChar("l"));
            dict.Add(221, Convert.ToChar("l"));
            dict.Add(222, Convert.ToChar("l"));
            dict.Add(223, Convert.ToChar("l"));
            dict.Add(224, Convert.ToChar("I"));
            dict.Add(225, Convert.ToChar("I"));
            dict.Add(226, Convert.ToChar("I"));
            dict.Add(227, Convert.ToChar("I"));
            dict.Add(228, Convert.ToChar(";"));
            dict.Add(229, Convert.ToChar(";"));
            dict.Add(230, Convert.ToChar(";"));
            dict.Add(231, Convert.ToChar(";"));
            dict.Add(232, Convert.ToChar(":"));
            dict.Add(233, Convert.ToChar(":"));
            dict.Add(234, Convert.ToChar(":"));
            dict.Add(235, Convert.ToChar(":"));
            dict.Add(236, Convert.ToChar(","));
            dict.Add(237, Convert.ToChar(","));
            dict.Add(238, Convert.ToChar(","));
            dict.Add(239, Convert.ToChar(","));
            dict.Add(240, Convert.ToChar("\""));
            dict.Add(241, Convert.ToChar("\""));
            dict.Add(242, Convert.ToChar("\""));
            dict.Add(243, Convert.ToChar("^"));
            dict.Add(244, Convert.ToChar("^"));
            dict.Add(245, Convert.ToChar("^"));
            dict.Add(246, Convert.ToChar("`"));
            dict.Add(247, Convert.ToChar("`"));
            dict.Add(248, Convert.ToChar("'"));
            dict.Add(249, Convert.ToChar("'"));
            dict.Add(250, Convert.ToChar("'"));
            dict.Add(251, Convert.ToChar("."));
            dict.Add(252, Convert.ToChar("."));
            dict.Add(253, Convert.ToChar("."));
            dict.Add(254, Convert.ToChar(" "));
            dict.Add(255, Convert.ToChar(" "));

            // return dictionary
            return dict;
        }

        #endregion
    }
}
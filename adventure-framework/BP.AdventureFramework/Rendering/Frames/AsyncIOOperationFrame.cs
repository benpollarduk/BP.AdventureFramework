using System;
using System.Text;
using AdventureFramework.Structure;

namespace AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a frame for a file input/output operation
    /// </summary>
    public class AsyncIOOperationFrame : Frame
    {
        #region Properties

        /// <summary>
        /// Get the operation
        /// </summary>
        public EIOOperation Operation
        {
            get { return operation; }
            protected set { operation = value; }
        }

        /// <summary>
        /// Get or set the operation
        /// </summary>
        private EIOOperation operation;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the IOOperationFrame class
        /// </summary>
        protected AsyncIOOperationFrame()
        {
            // no input
            AcceptsInput = false;

            // do not show cursor
            ShowCursor = false;
        }

        /// <summary>
        /// Initializes a new instance of the IOOperationFrame class
        /// </summary>
        /// <param name="operation">The type of IO operation</param>
        public AsyncIOOperationFrame(EIOOperation operation)
        {
            // no input
            AcceptsInput = false;

            // do not show cursor
            ShowCursor = false;

            // set
            Operation = operation;
        }

        /// <summary>
        /// Build this AsyncIOOperationFrame into a text based display
        /// </summary>
        /// <param name="width">Specify the width of the Frame</param>
        /// <param name="height">Specify the height of the Frame</param>
        /// <param name="drawer">The FrameDrawer to draw the Frame with</param>
        /// <returns>A string representing the Frame</returns>
        public override string BuildFrame(int width, int height, FrameDrawer drawer)
        {
            // create builder
            var builder = new StringBuilder();

            // hold message
            var message = string.Empty;

            // select operation
            switch (Operation)
            {
                case EIOOperation.Load:
                    {
                        // set message
                        message = "Loading...";

                        break;
                    }
                case EIOOperation.Save:
                    {
                        // set message
                        message = "Saving...";

                        break;
                    }
                default:
                    {
                        throw new NotImplementedException();
                    }
            }

            // create top
            builder.Append(drawer.ConstructDevider(width));

            // get buffer height
            var bufferHeight = height / 2 - 2;

            // add buffer
            builder.Append(drawer.ConstructPaddedArea(width, bufferHeight));

            // add message
            builder.Append(drawer.ConstructCentralisedString(message, width));

            // add buffer
            builder.Append(drawer.ConstructPaddedArea(width, bufferHeight));

            // create devider
            var devider = drawer.ConstructDevider(width);

            // add devider removing the last \n
            builder.Append(devider.Remove(devider.Length - 1));

            // return builder
            return builder.ToString();
        }

        #endregion
    }
}
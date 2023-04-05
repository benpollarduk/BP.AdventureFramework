using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a Frame used for ASCII animation
    /// </summary>
    public class ASCIIAnimationFrame : Frame
    {
        #region EventHandlers

        private void animationClock_Elapsed(object sender, ElapsedEventArgs e)
        {
            // invalidate
            Invalidate();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the frames that make up this animation
        /// </summary>
        public List<ASCIIImageFrame> Frames
        {
            get { return frames; }
            set { frames = value; }
        }

        /// <summary>
        /// Get or set the frames that make up this animation
        /// </summary>
        private List<ASCIIImageFrame> frames = new List<ASCIIImageFrame>();

        /// <summary>
        /// Get or set the interval between frames in ms
        /// </summary>
        public double Interval
        {
            get { return interval; }
            set { interval = value; }
        }

        /// <summary>
        /// Get or set the interval between frames in ms
        /// </summary>
        private double interval = 100;

        /// <summary>
        /// Get or set the amount of times this animation should be looped. For infinate use System.Threading.Timeout.Infinate
        /// </summary>
        public int Loops
        {
            get { return repeats; }
            set { repeats = value; }
        }

        /// <summary>
        /// Get or set the amount of times this animation should be looped. For infinate use System.Threading.Timeout.Infinate
        /// </summary>
        private int repeats = 1;

        /// <summary>
        /// Get or set if the animation should be automatically reversed
        /// </summary>
        public bool AutoReverse
        {
            get { return autoReverse; }
            set { autoReverse = value; }
        }

        /// <summary>
        /// Get or set if the animation should be automatically reversed
        /// </summary>
        private bool autoReverse;

        /// <summary>
        /// Get if this animation is running
        /// </summary>
        public bool IsRunning
        {
            get { return isRunning; }
        }

        /// <summary>
        /// Get or set if animation this is running
        /// </summary>
        private bool isRunning;

        /// <summary>
        /// Get or set the timer used for the animation timeline
        /// </summary>
        private Timer animationClock = new Timer();

        /// <summary>
        /// Get or set the current frame index
        /// </summary>
        private int currentFrameIndex;

        /// <summary>
        /// Get or set the current loop index
        /// </summary>
        private int currentLoopIndex;

        /// <summary>
        /// Get or set if this is currently handling a frame update
        /// </summary>
        private bool isHandlingFrameUpdate;

        /// <summary>
        /// Get or set if this is currently in reverse
        /// </summary>
        private bool isInReverse;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the ASCIIAnimationFrame class
        /// </summary>
        public ASCIIAnimationFrame()
        {
            // don't show cursor
            ShowCursor = false;

            // don't allow input
            AcceptsInput = false;
        }

        /// <summary>
        /// Initializes a new instance of the ASCIIAnimationFrame class
        /// </summary>
        /// <param name="frames">Specify the frames of the animation</param>
        public ASCIIAnimationFrame(params ASCIIImageFrame[] frames)
        {
            // set lines
            Frames = new List<ASCIIImageFrame>(frames);

            // don't show cursor
            ShowCursor = false;

            // don't allow input
            AcceptsInput = false;
        }

        /// <summary>
        /// Initializes a new instance of the ASCIIAnimationFrame class
        /// </summary>
        /// <param name="loops">Specify the amount of times this animation should be looped. For infinate use System.Threading.Timeout.Infinate</param>
        /// <param name="frames">Specify the frames of the animation</param>
        public ASCIIAnimationFrame(int loops, params ASCIIImageFrame[] frames)
        {
            // set loops
            Loops = loops;

            // set lines
            Frames = new List<ASCIIImageFrame>(frames);

            // don't show cursor
            ShowCursor = false;

            // don't allow input
            AcceptsInput = false;
        }

        /// <summary>
        /// Initializes a new instance of the ASCIIAnimationFrame class
        /// </summary>
        /// <param name="loops">Specify the amount of times this animation should be looped. For infinate use System.Threading.Timeout.Infinate</param>
        /// <param name="interval">Specify the interval to be used between frames in ms</param>
        /// <param name="autoReverse">Specify if the animation should be automatically reversed</param>
        /// <param name="frames">Specify the frames of the animation</param>
        public ASCIIAnimationFrame(int loops, int interval, bool autoReverse, params ASCIIImageFrame[] frames)
        {
            // set loops
            Loops = loops;

            // set interval
            Interval = interval;

            // set auto reverse
            AutoReverse = autoReverse;

            // set lines
            Frames = new List<ASCIIImageFrame>(frames);

            // don't show cursor
            ShowCursor = false;

            // don't allow input
            AcceptsInput = false;
        }

        /// <summary>
        /// Build this ASCIIAnimationFrame into a text based display
        /// </summary>
        /// <param name="width">Specify the width of the Frame</param>
        /// <param name="height">Specify the height of the Frame</param>
        /// <param name="drawer">The FrameDrawer to draw the Frame with</param>
        /// <returns>A string representing the Frame</returns>
        public override string BuildFrame(int width, int height, FrameDrawer drawer)
        {
            // if not already handling an update
            if (!isHandlingFrameUpdate)
            {
                // handling update
                isHandlingFrameUpdate = true;

                try
                {
                    // if not already running
                    if (!isRunning)
                    {
                        // start
                        Start();

                        // build the 1st frame
                        return Frames[0].BuildFrame(width, height, drawer);
                    }
                    else
                    {
                        // see what loop to do

                        // if this is in reverse
                        if (isInReverse)
                        {
                            // running backwards

                            // if index is greater than first frame
                            if (currentFrameIndex > 0)
                            {
                                // deincrement frame index
                                currentFrameIndex--;
                            }
                            else
                            {
                                // put into foward motion
                                isInReverse = false;

                                // increment loop
                                currentLoopIndex++;

                                // increment frame so we dont get a repeat frame
                                currentFrameIndex++;
                            }
                        }
                        else
                        {
                            // running fowards

                            // if index is less than end frame
                            if (currentFrameIndex < Frames.Count - 1)
                            {
                                // increment frame index
                                currentFrameIndex++;
                            }
                            else
                            {
                                // if switching to reverse
                                if (AutoReverse)
                                {
                                    // put in reverse
                                    isInReverse = true;

                                    // knock back a frame
                                    currentFrameIndex--;
                                }
                                else
                                {
                                    // set to zero frame
                                    currentFrameIndex = 0;

                                    // increment loop
                                    currentLoopIndex++;
                                }
                            }
                        }

                        // check looping status
                        if (Loops != Timeout.Infinite && currentFrameIndex == Frames.Count - 1 && !AutoReverse && currentLoopIndex == Loops || currentFrameIndex == 0 && AutoReverse && isInReverse && currentLoopIndex == Loops)
                        {
                            // stop
                            Stop();

                            // build the last frame
                            return Frames[currentFrameIndex].BuildFrame(width, height, drawer);
                        }
                        else
                        {
                            // build the next frame
                            return Frames[currentFrameIndex].BuildFrame(width, height, drawer);
                        }
                    }
                }
                finally
                {
                    // not handling update
                    isHandlingFrameUpdate = false;
                }
            }

            // build the last frame
            return Frames[currentFrameIndex].BuildFrame(width, height, drawer);
        }

        /// <summary>
        /// Stop any running animation
        /// </summary>
        public void Stop()
        {
            // if a timer
            if (animationClock != null)
            {
                // stop
                animationClock.Stop();

                // release handle for elapse
                animationClock.Elapsed -= animationClock_Elapsed;

                // not running
                isRunning = false;

                // release clock
                animationClock = null;
            }
        }

        /// <summary>
        /// Start the animation
        /// </summary>
        public void Start()
        {
            // create new clock
            animationClock = new Timer(interval);

            // enable
            animationClock.Enabled = true;

            // handle elapse
            animationClock.Elapsed += animationClock_Elapsed;

            // start clock
            animationClock.Start();

            // running
            isRunning = true;
        }

        /// <summary>
        /// Reset the animation
        /// </summary>
        public void Reset()
        {
            // set current frame
            currentFrameIndex = 0;

            // set current loop
            currentLoopIndex = 0;
        }

        /// <summary>
        /// Handle disposal of this ASCIIAnimationFrame
        /// </summary>
        protected override void OnDisposed()
        {
            // stop if running
            Stop();

            // follow through to base
            base.OnDisposed();
        }

        #endregion
    }
}
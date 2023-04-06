using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace BP.AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a Frame used for ASCII animation.
    /// </summary>
    public class ASCIIAnimationFrame : Frame
    {
        #region Fields

        private bool isRunning;
        private Timer animationClock = new Timer();
        private int currentFrameIndex;
        private int currentLoopIndex;
        private bool isHandlingFrameUpdate;
        private bool isInReverse;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the frames that make up this animation.
        /// </summary>
        public List<ASCIIImageFrame> Frames { get; set; } = new List<ASCIIImageFrame>();

        /// <summary>
        /// Get or set the interval between frames in ms.
        /// </summary>
        public double Interval { get; set; } = 100;

        /// <summary>
        /// Get or set the amount of times this animation should be looped. For infinite use System.Threading.Timeout.Infinate.
        /// </summary>
        public int Loops { get; set; } = 1;

        /// <summary>
        /// Get or set if the animation should be automatically reversed.
        /// </summary>
        public bool AutoReverse { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ASCIIAnimationFrame class.
        /// </summary>
        public ASCIIAnimationFrame()
        {
            ShowCursor = false;
            AcceptsInput = false;
        }

        /// <summary>
        /// Initializes a new instance of the ASCIIAnimationFrame class.
        /// </summary>
        /// <param name="frames">Specify the frames of the animation.</param>
        public ASCIIAnimationFrame(params ASCIIImageFrame[] frames)
        {
            Frames = new List<ASCIIImageFrame>(frames);
            ShowCursor = false;
            AcceptsInput = false;
        }

        /// <summary>
        /// Initializes a new instance of the ASCIIAnimationFrame class.
        /// </summary>
        /// <param name="loops">Specify the amount of times this animation should be looped. For infinite use System.Threading.Timeout.Infinite.</param>
        /// <param name="frames">Specify the frames of the animation.</param>
        public ASCIIAnimationFrame(int loops, params ASCIIImageFrame[] frames)
        {
            Loops = loops;
            Frames = new List<ASCIIImageFrame>(frames);
            ShowCursor = false;
            AcceptsInput = false;
        }

        /// <summary>
        /// Initializes a new instance of the ASCIIAnimationFrame class.
        /// </summary>
        /// <param name="loops">Specify the amount of times this animation should be looped. For infinite use System.Threading.Timeout.Infinite.</param>
        /// <param name="interval">Specify the interval to be used between frames in ms.</param>
        /// <param name="autoReverse">Specify if the animation should be automatically reversed.</param>
        /// <param name="frames">Specify the frames of the animation.</param>
        public ASCIIAnimationFrame(int loops, int interval, bool autoReverse, params ASCIIImageFrame[] frames)
        {
            Loops = loops;
            Interval = interval;
            AutoReverse = autoReverse;
            Frames = new List<ASCIIImageFrame>(frames);
            ShowCursor = false;
            AcceptsInput = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Build this ASCIIAnimationFrame into a text based display.
        /// </summary>
        /// <param name="width">Specify the width of the Frame.</param>
        /// <param name="height">Specify the height of the Frame.</param>
        /// <param name="drawer">The FrameDrawer to draw the Frame with.</param>
        /// <returns>A string representing the Frame.</returns>
        public override string BuildFrame(int width, int height, FrameDrawer drawer)
        {
            if (isHandlingFrameUpdate) 
                return Frames[currentFrameIndex].BuildFrame(width, height, drawer);
            
            isHandlingFrameUpdate = true;

            try
            {
                if (!isRunning)
                {
                    Start();
                    return Frames[0].BuildFrame(width, height, drawer);
                }
                else
                {
                    if (isInReverse)
                    {
                        if (currentFrameIndex > 0)
                        {
                            currentFrameIndex--;
                        }
                        else
                        {
                            isInReverse = false;
                            currentLoopIndex++;
                            currentFrameIndex++;
                        }
                    }
                    else
                    {
                        if (currentFrameIndex < Frames.Count - 1)
                        {
                            currentFrameIndex++;
                        }
                        else
                        {
                            if (AutoReverse)
                            {
                                isInReverse = true;
                                currentFrameIndex--;
                            }
                            else
                            {
                                currentFrameIndex = 0;
                                currentLoopIndex++;
                            }
                        }
                    }

                    if (Loops != Timeout.Infinite && currentFrameIndex == Frames.Count - 1 && !AutoReverse && currentLoopIndex == Loops || currentFrameIndex == 0 && AutoReverse && isInReverse && currentLoopIndex == Loops)
                    {
                        Stop();
                        return Frames[currentFrameIndex].BuildFrame(width, height, drawer);
                    }
                    else
                    {
                        return Frames[currentFrameIndex].BuildFrame(width, height, drawer);
                    }
                }
            }
            finally
            {
                isHandlingFrameUpdate = false;
            }
        }

        /// <summary>
        /// Stop any running animation.
        /// </summary>
        public void Stop()
        {
            if (animationClock == null)
                return;

            animationClock.Stop();
            animationClock.Elapsed -= AnimationClock_Elapsed;
            isRunning = false;
            animationClock = null;
        }

        /// <summary>
        /// Start the animation.
        /// </summary>
        public void Start()
        {
            animationClock = new Timer(Interval) { Enabled = true };
            animationClock.Elapsed += AnimationClock_Elapsed;
            animationClock.Start();
            isRunning = true;
        }

        /// <summary>
        /// Reset the animation.
        /// </summary>
        public void Reset()
        {
            currentFrameIndex = 0;
            currentLoopIndex = 0;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            Stop();
            base.Dispose();
        }

        #endregion

        #region EventHandlers

        private void AnimationClock_Elapsed(object sender, ElapsedEventArgs e)
        {
            Invalidate();
        }

        #endregion
    }
}
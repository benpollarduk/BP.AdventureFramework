using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a Frame used for ASCII animation
    /// </summary>
    public class ASCIIAnimationFrame : Frame
    {
        #region Properties

        /// <summary>
        /// Get or set the frames that make up this animation
        /// </summary>
        public List<ASCIIImageFrame> Frames
        {
            get { return this.frames; }
            set { this.frames = value; }
        }

        /// <summary>
        /// Get or set the frames that make up this animation
        /// </summary>
        private List<ASCIIImageFrame> frames = new List<ASCIIImageFrame>();

        /// <summary>
        /// Get or set the interval between frames in ms
        /// </summary>
        public Double Interval
        {
            get { return this.interval; }
            set { this.interval = value; }
        }

        /// <summary>
        /// Get or set the interval between frames in ms
        /// </summary>
        private Double interval = 100;

        /// <summary>
        /// Get or set the amount of times this animation should be looped. For infinate use System.Threading.Timeout.Infinate
        /// </summary>
        public Int32 Loops
        {
            get { return this.repeats; }
            set { this.repeats = value; }
        }

        /// <summary>
        /// Get or set the amount of times this animation should be looped. For infinate use System.Threading.Timeout.Infinate
        /// </summary>
        private Int32 repeats = 1;

        /// <summary>
        /// Get or set if the animation should be automatically reversed
        /// </summary>
        public Boolean AutoReverse
        {
            get { return this.autoReverse; }
            set { this.autoReverse = value; }
        }

        /// <summary>
        /// Get or set if the animation should be automatically reversed
        /// </summary>
        private Boolean autoReverse = false;

        /// <summary>
        /// Get if this animation is running
        /// </summary>
        public Boolean IsRunning
        {
            get { return this.isRunning; }
        }

        /// <summary>
        /// Get or set if animation this is running
        /// </summary>
        private Boolean isRunning = false;

        /// <summary>
        /// Get or set the timer used for the animation timeline
        /// </summary>
        private Timer animationClock = new Timer();

        /// <summary>
        /// Get or set the current frame index
        /// </summary>
        private Int32 currentFrameIndex = 0;

        /// <summary>
        /// Get or set the current loop index
        /// </summary>
        private Int32 currentLoopIndex = 0;

        /// <summary>
        /// Get or set if this is currently handling a frame update
        /// </summary>
        private Boolean isHandlingFrameUpdate;

        /// <summary>
        /// Get or set if this is currently in reverse
        /// </summary>
        private Boolean isInReverse = false;

        #endregion 
        
        #region Methods

        /// <summary>
        /// Initializes a new instance of the ASCIIAnimationFrame class
        /// </summary>
        public ASCIIAnimationFrame()
        {
            // don't show cursor
            this.ShowCursor = false;

            // don't allow input
            this.AcceptsInput = false;
        }

        /// <summary>
        /// Initializes a new instance of the ASCIIAnimationFrame class
        /// </summary>
        /// <param name="frames">Specify the frames of the animation</param>
        public ASCIIAnimationFrame(params ASCIIImageFrame[] frames)
        {
            // set lines
            this.Frames = new List<ASCIIImageFrame>(frames);

            // don't show cursor
            this.ShowCursor = false;

            // don't allow input
            this.AcceptsInput = false;
        }

        /// <summary>
        /// Initializes a new instance of the ASCIIAnimationFrame class
        /// </summary>
        /// <param name="loops">Specify the amount of times this animation should be looped. For infinate use System.Threading.Timeout.Infinate</param>
        /// <param name="frames">Specify the frames of the animation</param>
        public ASCIIAnimationFrame(Int32 loops, params ASCIIImageFrame[] frames)
        {
            // set loops
            this.Loops = loops;

            // set lines
            this.Frames = new List<ASCIIImageFrame>(frames);

            // don't show cursor
            this.ShowCursor = false;

            // don't allow input
            this.AcceptsInput = false;
        }

        /// <summary>
        /// Initializes a new instance of the ASCIIAnimationFrame class
        /// </summary>
        /// <param name="loops">Specify the amount of times this animation should be looped. For infinate use System.Threading.Timeout.Infinate</param>
        /// <param name="interval">Specify the interval to be used between frames in ms</param>
        /// <param name="autoReverse">Specify if the animation should be automatically reversed</param>
        /// <param name="frames">Specify the frames of the animation</param>
        public ASCIIAnimationFrame(Int32 loops, Int32 interval, Boolean autoReverse, params ASCIIImageFrame[] frames)
        {
            // set loops
            this.Loops = loops;

            // set interval
            this.Interval = interval;

            // set auto reverse
            this.AutoReverse = autoReverse;

            // set lines
            this.Frames = new List<ASCIIImageFrame>(frames);

            // don't show cursor
            this.ShowCursor = false;

            // don't allow input
            this.AcceptsInput = false;
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
            if (!this.isHandlingFrameUpdate)
            {
                // handling update
                this.isHandlingFrameUpdate = true;

                try
                {
                    // if not already running
                    if (!this.isRunning)
                    {
                        // start
                        this.Start();

                        // build the 1st frame
                        return this.Frames[0].BuildFrame(width, height, drawer);
                    }
                    else
                    {
                        // see what loop to do

                        // if this is in reverse
                        if (this.isInReverse)
                        {
                            // running backwards

                            // if index is greater than first frame
                            if (currentFrameIndex > 0)
                            {
                                // deincrement frame index
                                this.currentFrameIndex--;
                            }
                            else
                            {
                                // put into foward motion
                                this.isInReverse = false;

                                // increment loop
                                this.currentLoopIndex++;

                                // increment frame so we dont get a repeat frame
                                this.currentFrameIndex++;
                            }
                        }
                        else
                        {
                            // running fowards

                            // if index is less than end frame
                            if (currentFrameIndex < this.Frames.Count - 1)
                            {
                                // increment frame index
                                this.currentFrameIndex++;
                            }
                            else
                            {
                                // if switching to reverse
                                if (this.AutoReverse)
                                {
                                    // put in reverse
                                    this.isInReverse = true;

                                    // knock back a frame
                                    currentFrameIndex--;
                                }
                                else
                                {
                                    // set to zero frame
                                    currentFrameIndex = 0;

                                    // increment loop
                                    this.currentLoopIndex++;
                                }
                            }
                        }

                        // check looping status
                        if ((this.Loops != System.Threading.Timeout.Infinite) &&
                            ((this.currentFrameIndex == this.Frames.Count - 1) && (!this.AutoReverse) && (this.currentLoopIndex == this.Loops)) || ((this.currentFrameIndex == 0) && (this.AutoReverse) && (this.isInReverse) && (this.currentLoopIndex == this.Loops)))
                        {
                            // stop
                            this.Stop();

                            // build the last frame
                            return this.Frames[this.currentFrameIndex].BuildFrame(width, height, drawer);
                        }
                        else
                        {
                            // build the next frame
                            return this.Frames[this.currentFrameIndex].BuildFrame(width, height, drawer);
                        }
                    }
                }
                finally
                {
                    // not handling update
                    this.isHandlingFrameUpdate = false;
                }
            }
            else
            {
                // build the last frame
                return this.Frames[this.currentFrameIndex].BuildFrame(width, height, drawer);
            }
        }

        /// <summary>
        /// Stop any running animation
        /// </summary>
        public void Stop()
        {
            // if a timer
            if (this.animationClock != null)
            {
                // stop
                this.animationClock.Stop();

                // release handle for elapse
                this.animationClock.Elapsed -= new ElapsedEventHandler(animationClock_Elapsed);

                // not running
                this.isRunning = false;

                // release clock
                this.animationClock = null;
            }
        }

        /// <summary>
        /// Start the animation
        /// </summary>
        public void Start()
        {
            // create new clock
            this.animationClock = new Timer(this.interval);

            // enable
            this.animationClock.Enabled = true;

            // handle elapse
            this.animationClock.Elapsed += new ElapsedEventHandler(animationClock_Elapsed);

            // start clock
            this.animationClock.Start();

            // running
            this.isRunning = true;
        }

        /// <summary>
        /// Reset the animation
        /// </summary>
        public void Reset()
        {
            // set current frame
            this.currentFrameIndex = 0;

            // set current loop
            this.currentLoopIndex = 0;
        }

        /// <summary>
        /// Handle disposal of this ASCIIAnimationFrame
        /// </summary>
        protected override void OnDisposed()
        {
            // stop if running
            this.Stop();

            // follow through to base
            base.OnDisposed();
        }

        #endregion

        #region EventHandlers

        void animationClock_Elapsed(object sender, ElapsedEventArgs e)
        {
            // invalidate
            this.Invalidate();
        }

        #endregion
    }
}

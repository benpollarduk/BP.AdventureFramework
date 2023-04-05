using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AdventureFramework.Sound;
using System.Reflection;
using AdventureFramework.Structure;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using AdventureFramework.Sound.Players;
using System.Threading;

namespace ChainEditor
{
    public partial class ChainEditorForm : Form
    {
        #region StaticProperties

        /// <summary>
        /// Get the ionian scale in c
        /// </summary>
        private static readonly Int32[] ionianScaleInC = new Int32[] { 0, 261, 293, 329, 370, 415, 466, 493, 523 };

        /// <summary>
        /// Get the choromatic scale in c
        /// </summary>
        private static readonly Int32[] choromaticScaleInC = new Int32[] { 0, 261, 293, 329, 349, 392, 440, 493, 523 };

        /// <summary>
        /// Get the htZ increments in 50 htZ steps
        /// </summary>
        private static readonly Int32[] incremental50htZSteps = new Int32[] { 0, 50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800, 850, 900, 950, 1000 };

        #endregion

        #region Properties

        /// <summary>
        /// Get or set if playing has been cancelled
        /// </summary>
        private Boolean cancelled = false;

        /// <summary>
        /// Get if this is playing
        /// </summary>
        public Boolean IsPlaying
        {
            get { return this.isPlaying;}
            private set { this.isPlaying = value; }
        }

        /// <summary>
        /// Get or set if this is playing
        /// </summary>
        private Boolean isPlaying = false;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the ChainEditorForm class
        /// </summary>
        public ChainEditorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add a note to the chain
        /// </summary>
        /// <param name="frequency">The frequency of the note to add</param>
        /// <param name="duration">The duration of the note to add</param>
        /// <param name="insertionIndex">The index at which to add the note</param>
        public virtual void AddNote(Int32 frequency, Int32 duration, Int32 insertionIndex)
        {
            // create beep
            Beep b = new Beep(frequency, duration);

            // add
            this.AddNote(b, insertionIndex);
        }

        /// <summary>
        /// Add a note to the chain
        /// </summary>
        /// <param name="note">The beep to add</param>
        /// <param name="insertionIndex">The index at which to add the note</param>
        public virtual void AddNote(IBeep note, Int32 insertionIndex)
        {
            // add to the combo box
            this.chainDataListBox.Items.Insert(insertionIndex, note);

            // if no selected item
            if (this.chainDataListBox.SelectedItems == null)
            {
                // reset to start
                this.ResetToStart();
            }
            else
            {
                // set index
                this.chainDataListBox.SelectedIndex = insertionIndex;
            }

            // refresh
            this.RefreshStatusBar();
        }

        /// <summary>
        /// Show the import file dialog
        /// </summary>
        private void showImportFileDialog()
        {
            // create dialog
            OpenFileDialog importBinDialog = new OpenFileDialog();

            // default extension
            importBinDialog.DefaultExt = "*.bin";

            // view extensions
            importBinDialog.Filter = "Binary Files (*.bin)|*.bin";

            // set location
            importBinDialog.InitialDirectory = Assembly.GetAssembly(typeof(Game)).Location;

            // set title
            importBinDialog.Title = "Import *.bin...";

            // if ok
            importBinDialog.FileOk += new CancelEventHandler(importBinDialog_FileOk);

            // show
            importBinDialog.ShowDialog();
        }

        /// <summary>
        /// Show the export file dialog
        /// </summary>
        private void showExportFileDialog()
        {
            // create dialog
            SaveFileDialog exportBinDialog = new SaveFileDialog();

            // default extension
            exportBinDialog.DefaultExt = "*.bin";

            // view extensions
            exportBinDialog.Filter = "Binary Files (*.bin)|*.bin";

            // set location
            exportBinDialog.InitialDirectory = Assembly.GetAssembly(typeof(Game)).Location;

            // set title
            exportBinDialog.Title = "Export .bin...";

            // if ok
            exportBinDialog.FileOk += new CancelEventHandler(exportBinDialog_FileOk);

            // show
            exportBinDialog.ShowDialog();
        }

        /// <summary>
        /// Load a Chain from a file
        /// </summary>
        /// <param name="fullPath">The full path of the Chain</param>
        /// <returns>True if the load is sucsessful</returns>
        public Boolean LoadChain(String fullPath)
        {
            try
            {
                // create deserializer
                BinaryFormatter deserializer = new BinaryFormatter();

                // create reader
                using (StreamReader reader = new StreamReader(fullPath))
                {
                    // add new element, using its name as the key
                    this.SetChain((Chain)deserializer.Deserialize(reader.BaseStream));
                }
            }
            catch (Exception e)
            {
                // show exception
                MessageBox.Show(String.Format("There was an exception loading the chain: {0}", e.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // fail
                return false;
            }

            // pass
            return true;
        }

        /// <summary>
        /// Save a Chain to a file
        /// </summary>
        /// <param name="fullPath">The full path of the chain to save</param>
        /// <returns>True is the save is sucsessful</returns>
        public Boolean SaveChain(String fullPath)
        {
            // serialize
            Boolean result = this.ConstructChain().SerializeToFile(fullPath);
            
            // check result
            if (!result)
            {
                // show
                MessageBox.Show("The chain could not be saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // return
            return result;
        }

        /// <summary>
        /// Clear the current Chain
        /// </summary>
        public void Clear()
        {
            // clear
            this.chainDataListBox.Items.Clear();

            // refresh
            this.RefreshStatusBar();
        }

        /// <summary>
        /// Get an IBeep from the input parameters
        /// </summary>
        /// <returns>A constructed IBeep</returns>
        private IBeep getBeepFromInput()
        {
            try
            {
                // get note
                Int32 frequency = Int32.Parse(this.noteComboBox.Text.ToString());
                Int32 duration = Int32.Parse(this.durationComboBox.Text.ToString());

                // if out of range
                if ((frequency < 0) ||
                    (frequency > Int16.MaxValue) ||
                    (duration < 0) ||
                    (duration > Int16.MaxValue))
                {
                    // throw exception
                    throw new Exception("Please enter values between 0 and 32767");
                }

                // return
                return new Beep(frequency, duration);
            }
            catch (Exception e)
            {
                // show error
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // nothing
                return null;
            }
        }

        /// <summary>
        /// Play the chain from the current position
        /// </summary>
        public void PlayFromCurrent()
        {
            // begin play
            this.OnPlayBegin();

            // reset cancelled
            this.cancelled = false;

            // if some items
            if (this.chainDataListBox.Items.Count > 0)
            {
                // if nothing selected
                if (this.chainDataListBox.SelectedItem == null)
                {
                    // reset to start
                    this.ResetToStart();
                }

                // play on thread pool
                ThreadPool.QueueUserWorkItem(new WaitCallback((object o) =>
                    {
                        // get the items in the box
                        Int32 items = (Int32)this.chainDataListBox.Invoke(new GetInt32Delegate(() => { return this.chainDataListBox.Items.Count; }));

                        // get start item
                        Int32 startItem = (Int32)this.chainDataListBox.Invoke(new GetInt32Delegate(() => { return this.chainDataListBox.SelectedIndex; }));

                        // itterate all items
                        for (Int32 index = startItem; index < items; index++)
                        {
                            // if cancelled
                            if (this.cancelled)
                            {
                                break;
                            }
                            else
                            {
                                // set selected element
                                this.chainDataListBox.Invoke(new LambdaCallback(() =>
                                    {
                                        // set selected
                                        this.chainDataListBox.SelectedIndex = index;
                                    }));

                                // get note
                                IBeep note = (IBeep)this.chainDataListBox.Invoke(new GetINoteDelegate(() => { return this.chainDataListBox.SelectedItem as IBeep; }));

                                // play
                                BeepPlayer.PlayBeep(note);
                            }
                        }

                        // play finished
                        this.Invoke(new LambdaCallback(() => this.OnPlayFinished()));
                    }));
            }
            else
            {
                // handle finish
                this.OnPlayFinished();
            }
        }

        /// <summary>
        /// Reset chain to the start
        /// </summary>
        public void ResetToStart()
        {
            // if something to select
            if (this.chainDataListBox.Items.Count > 0)
            {
                // select
                this.SeekPosition(0);
            }
        }

        /// <summary>
        /// Seek to a position in the chain
        /// </summary>
        /// <param name="position">The index of the position to seek</param>
        public void SeekPosition(Int32 position)
        {
            // select
            this.chainDataListBox.SelectedIndex = position;
        }

        /// <summary>
        /// Cancel any current playing
        /// </summary>
        public void CancelPlay()
        {
            // cancel
            this.cancelled = true;
        }

        /// <summary>
        /// Set the chain
        /// </summary>
        /// <param name="chain">The chain to set to</param>
        public void SetChain(Chain chain)
        {
            // clear all
            this.chainDataListBox.Items.Clear();

            // if a chain
            if (chain != null)
            {
                // itterate all beeps
                for (Int32 index = 0; index < chain.Beeps.Length; index++)
                {
                    // add element
                    this.chainDataListBox.Items.Add(chain.Beeps[index]);
                }

                // reset
                this.ResetToStart();

                // refresh
                this.RefreshStatusBar();
            }
        }

        /// <summary>
        /// Construct a Chain from the values in this control
        /// </summary>
        /// <returns>A new Chain</returns>
        public Chain ConstructChain()
        {
            // create chain
            Chain c = new Chain();

            // itterate each note
            foreach (IBeep note in this.chainDataListBox.Items)
            {
                // add
                c.AddBeep(note);
            }

            // return
            return c;
        }

        /// <summary>
        /// Handle the beginning of playing 
        /// </summary>
        protected virtual void OnPlayBegin()
        {
            // playing
            this.IsPlaying = true;

            // disable all
            this.mainMenu.Enabled = false;
            this.testButton.Enabled = false;
            this.addButton.Enabled = false;
            this.noteComboBox.Enabled = false;
            this.durationComboBox.Enabled = false;
            this.chainDataListBox.Enabled = false;
        }

        /// <summary>
        /// Handle the finishing of playing 
        /// </summary>
        protected virtual void OnPlayFinished()
        {
            // not playing
            this.IsPlaying = false;

            // enable all
            this.mainMenu.Enabled = true;
            this.testButton.Enabled = true;
            this.addButton.Enabled = true;
            this.noteComboBox.Enabled = true;
            this.durationComboBox.Enabled = true;
            this.chainDataListBox.Enabled = true;
        }

        /// <summary>
        /// Refresh the status bar
        /// </summary>
        public void RefreshStatusBar()
        {
            // set beeps
            this.totaCountlLabel.Text = this.chainDataListBox.Items.Count.ToString();

            // get total
            Int32 totalLength = 0;

            // itterate all data
            for (Int32 index = 0; index < this.chainDataListBox.Items.Count; index++)
            {
                // increment
                totalLength += ((IBeep)this.chainDataListBox.Items[index]).Duration;
            }

            // set total length
            this.durationMsLabel.Text = totalLength.ToString();

            // set current
            this.currentLabel.Text = (this.chainDataListBox.SelectedIndex + 1).ToString();
        }

        /// <summary>
        /// Invert the order of the beeps
        /// </summary>
        public void InvertBeeps()
        {
            // hold beeps
            List<IBeep> beeps = new List<IBeep>();

            // copy array
            for (Int32 index = 0; index < this.chainDataListBox.Items.Count; index++)
            {
                // add
                beeps.Add(this.chainDataListBox.Items[index] as IBeep);
            }

            // reverse
            beeps.Reverse();

            // clear
            this.Clear();

            // itterate all beeps
            for (Int32 index = 0; index < beeps.Count; index++)
            {
                // add
                this.AddNote(beeps[index], index);
            }
        }

        /// <summary>
        /// Create a back to back inversion of the beeps
        /// </summary>
        public void BackToBackInversion()
        {
            // hold beeps
            List<IBeep> beeps = new List<IBeep>();

            // copy array
            for (Int32 index = 0; index < this.chainDataListBox.Items.Count; index++)
            {
                // add
                beeps.Add(this.chainDataListBox.Items[index] as IBeep);
            }

            // reverse
            beeps.Reverse();

            // itterate all beeps
            for (Int32 index = 0; index < beeps.Count; index++)
            {
                // add
                this.AddNote(beeps[index], this.chainDataListBox.Items.Count);
            }
        }

        /// <summary>
        /// Populate the frequency combo box with the specified frequencies
        /// </summary>
        /// <param name="frequencies">The frequncies to populate the box with</param>
        private void populateFrequencyComboBox(params Int32[] frequencies)
        {
            // clear
            this.noteComboBox.Items.Clear();

            // itterate all
            foreach (Int32 f in frequencies)
            {
                // add f
                this.noteComboBox.Items.Add(f);
            }
        }

        /// <summary>
        /// Uncheck all menu items in a collection except a specified item
        /// </summary>
        /// <param name="leaveChecked">The item to leave checked</param>
        private void uncheckAllFrequencyMenuItemsExcept(ToolStripItemCollection items, ToolStripMenuItem leaveChecked)
        {
            // itterate all
            foreach (ToolStripMenuItem i in items)
            {
                // if not the one to leave checked
                if (i != leaveChecked)
                {
                    // uncheck
                    i.Checked = false;
                }
                else
                {
                    // leave checked
                    leaveChecked.Checked = true;
                }
            }
        }

        #endregion

        #region EventHandlers

        #region Menu

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // exit application
            Application.Exit();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // show import
            this.showImportFileDialog();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // show export
            this.showExportFileDialog();
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // invert
            this.InvertBeeps();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // clear
            this.Clear();
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // cancel play
            this.CancelPlay();

            // reset
            this.ResetToStart();

            // play
            this.PlayFromCurrent();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // cancel
            this.CancelPlay();
        }

        private void topToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // if playing
            if (this.IsPlaying)
            {
                // cancel play
                this.CancelPlay();

                // reset
                this.ResetToStart();

                // play
                this.PlayFromCurrent();
            }
            else
            {
                // reset
                this.ResetToStart();
            }
        }

        private void ionianScaleMenuItem_Click(object sender, EventArgs e)
        {
            // populate ionian
            this.populateFrequencyComboBox(ChainEditorForm.ionianScaleInC);

            // handle checking
            this.uncheckAllFrequencyMenuItemsExcept(this.frequencyMenuItem.DropDownItems, sender as ToolStripMenuItem);
        }

        private void choromaticScaleCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // populate choromatic
            this.populateFrequencyComboBox(ChainEditorForm.choromaticScaleInC);

            // handle checking
            this.uncheckAllFrequencyMenuItemsExcept(this.frequencyMenuItem.DropDownItems, sender as ToolStripMenuItem);
        }

        private void htZIncrementsMenuItem_Click(object sender, EventArgs e)
        {
            // populate choromatic
            this.populateFrequencyComboBox(ChainEditorForm.incremental50htZSteps);

            // handle checking
            this.uncheckAllFrequencyMenuItemsExcept(this.frequencyMenuItem.DropDownItems, sender as ToolStripMenuItem);
        }

        private void playFromCurrentPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // play
            this.PlayFromCurrent();
        }

        private void stepFowardMenuItem_Click(object sender, EventArgs e)
        {
            // if still stepping room
            if (this.chainDataListBox.SelectedIndex < this.chainDataListBox.Items.Count - 1)
            {
                // increment selection
                this.chainDataListBox.SelectedIndex++;

                // play note
                BeepPlayer.PlayBeep(this.chainDataListBox.SelectedItem as IBeep);
            }
        }

        private void stepBackwardMenuItem_Click(object sender, EventArgs e)
        {
            // if still stepping room
            if (this.chainDataListBox.SelectedIndex > 0)
            {
                // increment selection
                this.chainDataListBox.SelectedIndex--;

                // play note
                BeepPlayer.PlayBeep(this.chainDataListBox.SelectedItem as IBeep);
            }
        }

        private void backToBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // back to back inversion
            this.BackToBackInversion();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // show message box
            MessageBox.Show("Chain Editor - Copyright Ben Pollard 2011\n\nChain creation software for the AdventureFramework", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        private void ChainEditorForm_Load(object sender, EventArgs e)
        {
            // populate ionian
            this.populateFrequencyComboBox(ChainEditorForm.ionianScaleInC);

            // reset all combo values
            this.noteComboBox.SelectedIndex = 0;
            this.durationComboBox.SelectedIndex = 0;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            // get beep
            IBeep beep = this.getBeepFromInput();

            // if a beep
            if (beep != null)
            {
                // add a new note
                this.AddNote(this.getBeepFromInput(), this.chainDataListBox.SelectedIndex >= 0 ? this.chainDataListBox.SelectedIndex + 1 : 0);
            }
        }

        void exportBinDialog_FileOk(object sender, CancelEventArgs e)
        {
            // save
            this.SaveChain(((SaveFileDialog)sender).FileName);
        }

        void importBinDialog_FileOk(object sender, CancelEventArgs e)
        {
            // load
            this.LoadChain(((OpenFileDialog)sender).FileName);
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            // play note
            BeepPlayer.PlayBeep(this.getBeepFromInput());
        }

        private void chainDataListBox_KeyDown(object sender, KeyEventArgs e)
        {
            // if a selected item and deleted
            if ((this.chainDataListBox.SelectedItem != null) && 
                (e.KeyCode == Keys.Delete))
            {
                // hold selected index
                Int32 index = this.chainDataListBox.SelectedIndex;

                // remove
                this.chainDataListBox.Items.Remove(this.chainDataListBox.SelectedItem);

                // if stil some items
                if (this.chainDataListBox.Items.Count > 0)
                {
                    // check index
                    if (index < this.chainDataListBox.Items.Count)
                    {
                        // set index
                        this.chainDataListBox.SelectedIndex = index;
                    }
                    else
                    {
                        // select last
                        this.chainDataListBox.SelectedIndex = this.chainDataListBox.Items.Count - 1;
                    }
                }

                // refresh
                this.RefreshStatusBar();
            }
        }

        private void chainDataListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // set current in status bar
            this.currentLabel.Text = (this.chainDataListBox.SelectedIndex + 1).ToString();
        }

        #endregion
    }

    /// <summary>
    /// Delegate for returning Int32's
    /// </summary>
    internal delegate Int32 GetInt32Delegate();

    /// <summary>
    /// Delegate for invoking lambda callbacks
    /// </summary>
    internal delegate void LambdaCallback();

    /// <summary>
    /// Delegate use for getting INote values
    /// </summary>
    internal delegate IBeep GetINoteDelegate();
}

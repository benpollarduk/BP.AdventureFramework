using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;

namespace ChainEditor
{
    public partial class ChainEditorForm : Form
    {
        #region StaticProperties

        /// <summary>
        /// Get the ionian scale in c
        /// </summary>
        private static readonly int[] ionianScaleInC = { 0, 261, 293, 329, 370, 415, 466, 493, 523 };

        /// <summary>
        /// Get the choromatic scale in c
        /// </summary>
        private static readonly int[] choromaticScaleInC = { 0, 261, 293, 329, 349, 392, 440, 493, 523 };

        /// <summary>
        /// Get the htZ increments in 50 htZ steps
        /// </summary>
        private static readonly int[] incremental50htZSteps = { 0, 50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800, 850, 900, 950, 1000 };

        #endregion

        #region Properties

        /// <summary>
        /// Get or set if playing has been cancelled
        /// </summary>
        private bool cancelled;

        /// <summary>
        /// Get if this is playing
        /// </summary>
        public bool IsPlaying
        {
            get { return isPlaying; }
            private set { isPlaying = value; }
        }

        /// <summary>
        /// Get or set if this is playing
        /// </summary>
        private bool isPlaying;

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
        public virtual void AddNote(int frequency, int duration, int insertionIndex)
        {
            // create beep
            Beep b = new Beep(frequency, duration);

            // add
            AddNote(b, insertionIndex);
        }

        /// <summary>
        /// Add a note to the chain
        /// </summary>
        /// <param name="note">The beep to add</param>
        /// <param name="insertionIndex">The index at which to add the note</param>
        public virtual void AddNote(IBeep note, int insertionIndex)
        {
            // add to the combo box
            chainDataListBox.Items.Insert(insertionIndex, note);

            // if no selected item
            if (chainDataListBox.SelectedItems == null)
                // reset to start
                ResetToStart();
            else
                // set index
                chainDataListBox.SelectedIndex = insertionIndex;

            // refresh
            RefreshStatusBar();
        }

        /// <summary>
        /// Show the import file dialog
        /// </summary>
        private void showImportFileDialog()
        {
            // create dialog
            var importBinDialog = new OpenFileDialog();

            // default extension
            importBinDialog.DefaultExt = "*.bin";

            // view extensions
            importBinDialog.Filter = "Binary Files (*.bin)|*.bin";

            // set location
            importBinDialog.InitialDirectory = Assembly.GetAssembly(typeof(Game)).Location;

            // set title
            importBinDialog.Title = "Import *.bin...";

            // if ok
            importBinDialog.FileOk += importBinDialog_FileOk;

            // show
            importBinDialog.ShowDialog();
        }

        /// <summary>
        /// Show the export file dialog
        /// </summary>
        private void showExportFileDialog()
        {
            // create dialog
            var exportBinDialog = new SaveFileDialog();

            // default extension
            exportBinDialog.DefaultExt = "*.bin";

            // view extensions
            exportBinDialog.Filter = "Binary Files (*.bin)|*.bin";

            // set location
            exportBinDialog.InitialDirectory = Assembly.GetAssembly(typeof(Game)).Location;

            // set title
            exportBinDialog.Title = "Export .bin...";

            // if ok
            exportBinDialog.FileOk += exportBinDialog_FileOk;

            // show
            exportBinDialog.ShowDialog();
        }

        /// <summary>
        /// Load a Chain from a file
        /// </summary>
        /// <param name="fullPath">The full path of the Chain</param>
        /// <returns>True if the load is sucsessful</returns>
        public bool LoadChain(string fullPath)
        {
            try
            {
                // create deserializer
                var deserializer = new BinaryFormatter();

                // create reader
                using (var reader = new StreamReader(fullPath))
                {
                    // add new element, using its name as the key
                    SetChain((Chain)deserializer.Deserialize(reader.BaseStream));
                }
            }
            catch (Exception e)
            {
                // show exception
                MessageBox.Show(string.Format("There was an exception loading the chain: {0}", e.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
        public bool SaveChain(string fullPath)
        {
            // serialize
            bool result = ConstructChain().SerializeToFile(fullPath);

            // check result
            if (!result)
                // show
                MessageBox.Show("The chain could not be saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // return
            return result;
        }

        /// <summary>
        /// Clear the current Chain
        /// </summary>
        public void Clear()
        {
            // clear
            chainDataListBox.Items.Clear();

            // refresh
            RefreshStatusBar();
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
                var frequency = int.Parse(noteComboBox.Text);
                var duration = int.Parse(durationComboBox.Text);

                // if out of range
                if (frequency < 0 ||
                    frequency > short.MaxValue ||
                    duration < 0 ||
                    duration > short.MaxValue)
                    // throw exception
                    throw new Exception("Please enter values between 0 and 32767");

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
            OnPlayBegin();

            // reset cancelled
            cancelled = false;

            // if some items
            if (chainDataListBox.Items.Count > 0)
            {
                // if nothing selected
                if (chainDataListBox.SelectedItem == null)
                    // reset to start
                    ResetToStart();

                // play on thread pool
                ThreadPool.QueueUserWorkItem(o =>
                {
                    // get the items in the box
                    var items = (int)chainDataListBox.Invoke(new GetInt32Delegate(() => { return chainDataListBox.Items.Count; }));

                    // get start item
                    var startItem = (int)chainDataListBox.Invoke(new GetInt32Delegate(() => { return chainDataListBox.SelectedIndex; }));

                    // itterate all items
                    for (var index = startItem; index < items; index++)
                        // if cancelled
                        if (cancelled)
                        {
                            break;
                        }
                        else
                        {
                            // set selected element
                            chainDataListBox.Invoke(new LambdaCallback(() =>
                            {
                                // set selected
                                chainDataListBox.SelectedIndex = index;
                            }));

                            // get note
                            IBeep note = (IBeep)chainDataListBox.Invoke(new GetINoteDelegate(() => { return chainDataListBox.SelectedItem as IBeep; }));

                            // play
                            BeepPlayer.PlayBeep(note);
                        }

                    // play finished
                    Invoke(new LambdaCallback(() => OnPlayFinished()));
                });
            }
            else
            {
                // handle finish
                OnPlayFinished();
            }
        }

        /// <summary>
        /// Reset chain to the start
        /// </summary>
        public void ResetToStart()
        {
            // if something to select
            if (chainDataListBox.Items.Count > 0)
                // select
                SeekPosition(0);
        }

        /// <summary>
        /// Seek to a position in the chain
        /// </summary>
        /// <param name="position">The index of the position to seek</param>
        public void SeekPosition(int position)
        {
            // select
            chainDataListBox.SelectedIndex = position;
        }

        /// <summary>
        /// Cancel any current playing
        /// </summary>
        public void CancelPlay()
        {
            // cancel
            cancelled = true;
        }

        /// <summary>
        /// Set the chain
        /// </summary>
        /// <param name="chain">The chain to set to</param>
        public void SetChain(Chain chain)
        {
            // clear all
            chainDataListBox.Items.Clear();

            // if a chain
            if (chain != null)
            {
                // itterate all beeps
                for (var index = 0; index < chain.Beeps.Length; index++)
                    // add element
                    chainDataListBox.Items.Add(chain.Beeps[index]);

                // reset
                ResetToStart();

                // refresh
                RefreshStatusBar();
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
            foreach (IBeep note in chainDataListBox.Items)
                // add
                c.AddBeep(note);

            // return
            return c;
        }

        /// <summary>
        /// Handle the beginning of playing
        /// </summary>
        protected virtual void OnPlayBegin()
        {
            // playing
            IsPlaying = true;

            // disable all
            mainMenu.Enabled = false;
            testButton.Enabled = false;
            addButton.Enabled = false;
            noteComboBox.Enabled = false;
            durationComboBox.Enabled = false;
            chainDataListBox.Enabled = false;
        }

        /// <summary>
        /// Handle the finishing of playing
        /// </summary>
        protected virtual void OnPlayFinished()
        {
            // not playing
            IsPlaying = false;

            // enable all
            mainMenu.Enabled = true;
            testButton.Enabled = true;
            addButton.Enabled = true;
            noteComboBox.Enabled = true;
            durationComboBox.Enabled = true;
            chainDataListBox.Enabled = true;
        }

        /// <summary>
        /// Refresh the status bar
        /// </summary>
        public void RefreshStatusBar()
        {
            // set beeps
            totaCountlLabel.Text = chainDataListBox.Items.Count.ToString();

            // get total
            var totalLength = 0;

            // itterate all data
            for (var index = 0; index < chainDataListBox.Items.Count; index++)
                // increment
                totalLength += ((IBeep)chainDataListBox.Items[index]).Duration;

            // set total length
            durationMsLabel.Text = totalLength.ToString();

            // set current
            currentLabel.Text = (chainDataListBox.SelectedIndex + 1).ToString();
        }

        /// <summary>
        /// Invert the order of the beeps
        /// </summary>
        public void InvertBeeps()
        {
            // hold beeps
            List<IBeep> beeps = new List<IBeep>();

            // copy array
            for (var index = 0; index < chainDataListBox.Items.Count; index++)
                // add
                beeps.Add(chainDataListBox.Items[index] as IBeep);

            // reverse
            beeps.Reverse();

            // clear
            Clear();

            // itterate all beeps
            for (var index = 0; index < beeps.Count; index++)
                // add
                AddNote(beeps[index], index);
        }

        /// <summary>
        /// Create a back to back inversion of the beeps
        /// </summary>
        public void BackToBackInversion()
        {
            // hold beeps
            List<IBeep> beeps = new List<IBeep>();

            // copy array
            for (var index = 0; index < chainDataListBox.Items.Count; index++)
                // add
                beeps.Add(chainDataListBox.Items[index] as IBeep);

            // reverse
            beeps.Reverse();

            // itterate all beeps
            for (var index = 0; index < beeps.Count; index++)
                // add
                AddNote(beeps[index], chainDataListBox.Items.Count);
        }

        /// <summary>
        /// Populate the frequency combo box with the specified frequencies
        /// </summary>
        /// <param name="frequencies">The frequncies to populate the box with</param>
        private void populateFrequencyComboBox(params int[] frequencies)
        {
            // clear
            noteComboBox.Items.Clear();

            // itterate all
            foreach (var f in frequencies)
                // add f
                noteComboBox.Items.Add(f);
        }

        /// <summary>
        /// Uncheck all menu items in a collection except a specified item
        /// </summary>
        /// <param name="leaveChecked">The item to leave checked</param>
        private void uncheckAllFrequencyMenuItemsExcept(ToolStripItemCollection items, ToolStripMenuItem leaveChecked)
        {
            // itterate all
            foreach (ToolStripMenuItem i in items)
                // if not the one to leave checked
                if (i != leaveChecked)
                    // uncheck
                    i.Checked = false;
                else
                    // leave checked
                    leaveChecked.Checked = true;
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
            showImportFileDialog();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // show export
            showExportFileDialog();
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // invert
            InvertBeeps();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // clear
            Clear();
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // cancel play
            CancelPlay();

            // reset
            ResetToStart();

            // play
            PlayFromCurrent();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // cancel
            CancelPlay();
        }

        private void topToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // if playing
            if (IsPlaying)
            {
                // cancel play
                CancelPlay();

                // reset
                ResetToStart();

                // play
                PlayFromCurrent();
            }
            else
            {
                // reset
                ResetToStart();
            }
        }

        private void ionianScaleMenuItem_Click(object sender, EventArgs e)
        {
            // populate ionian
            populateFrequencyComboBox(ionianScaleInC);

            // handle checking
            uncheckAllFrequencyMenuItemsExcept(frequencyMenuItem.DropDownItems, sender as ToolStripMenuItem);
        }

        private void choromaticScaleCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // populate choromatic
            populateFrequencyComboBox(choromaticScaleInC);

            // handle checking
            uncheckAllFrequencyMenuItemsExcept(frequencyMenuItem.DropDownItems, sender as ToolStripMenuItem);
        }

        private void htZIncrementsMenuItem_Click(object sender, EventArgs e)
        {
            // populate choromatic
            populateFrequencyComboBox(incremental50htZSteps);

            // handle checking
            uncheckAllFrequencyMenuItemsExcept(frequencyMenuItem.DropDownItems, sender as ToolStripMenuItem);
        }

        private void playFromCurrentPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // play
            PlayFromCurrent();
        }

        private void stepFowardMenuItem_Click(object sender, EventArgs e)
        {
            // if still stepping room
            if (chainDataListBox.SelectedIndex < chainDataListBox.Items.Count - 1)
            {
                // increment selection
                chainDataListBox.SelectedIndex++;

                // play note
                BeepPlayer.PlayBeep(chainDataListBox.SelectedItem as IBeep);
            }
        }

        private void stepBackwardMenuItem_Click(object sender, EventArgs e)
        {
            // if still stepping room
            if (chainDataListBox.SelectedIndex > 0)
            {
                // increment selection
                chainDataListBox.SelectedIndex--;

                // play note
                BeepPlayer.PlayBeep(chainDataListBox.SelectedItem as IBeep);
            }
        }

        private void backToBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // back to back inversion
            BackToBackInversion();
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
            populateFrequencyComboBox(ionianScaleInC);

            // reset all combo values
            noteComboBox.SelectedIndex = 0;
            durationComboBox.SelectedIndex = 0;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            // get beep
            IBeep beep = getBeepFromInput();

            // if a beep
            if (beep != null)
                // add a new note
                AddNote(getBeepFromInput(), chainDataListBox.SelectedIndex >= 0 ? chainDataListBox.SelectedIndex + 1 : 0);
        }

        private void exportBinDialog_FileOk(object sender, CancelEventArgs e)
        {
            // save
            SaveChain(((SaveFileDialog)sender).FileName);
        }

        private void importBinDialog_FileOk(object sender, CancelEventArgs e)
        {
            // load
            LoadChain(((OpenFileDialog)sender).FileName);
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            // play note
            BeepPlayer.PlayBeep(getBeepFromInput());
        }

        private void chainDataListBox_KeyDown(object sender, KeyEventArgs e)
        {
            // if a selected item and deleted
            if (chainDataListBox.SelectedItem != null &&
                e.KeyCode == Keys.Delete)
            {
                // hold selected index
                var index = chainDataListBox.SelectedIndex;

                // remove
                chainDataListBox.Items.Remove(chainDataListBox.SelectedItem);

                // if stil some items
                if (chainDataListBox.Items.Count > 0)
                {
                    // check index
                    if (index < chainDataListBox.Items.Count)
                        // set index
                        chainDataListBox.SelectedIndex = index;
                    else
                        // select last
                        chainDataListBox.SelectedIndex = chainDataListBox.Items.Count - 1;
                }

                // refresh
                RefreshStatusBar();
            }
        }

        private void chainDataListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // set current in status bar
            currentLabel.Text = (chainDataListBox.SelectedIndex + 1).ToString();
        }

        #endregion
    }

    /// <summary>
    /// Delegate for returning Int32's
    /// </summary>
    internal delegate int GetInt32Delegate();

    /// <summary>
    /// Delegate for invoking lambda callbacks
    /// </summary>
    internal delegate void LambdaCallback();

    /// <summary>
    /// Delegate use for getting INote values
    /// </summary>
    internal delegate IBeep GetINoteDelegate();
}
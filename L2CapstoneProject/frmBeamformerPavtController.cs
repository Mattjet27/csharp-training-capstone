using System;
using System.Windows.Forms;
using NationalInstruments.ModularInstruments.NIRfsg;
using NationalInstruments.RFmx.InstrMX;
using NationalInstruments.RFmx.SpecAnMX;
using NationalInstruments.ModularInstruments.SystemServices.DeviceServices;
using System.Collections.Generic;
using L2CapstoneProject.Beamformer;

namespace L2CapstoneProject
{

    public partial class frmBeamformerPavtController : Form
    {
        NIRfsg rfsg;
        public List<PhaseAmplitudeOffset> offsetList = new List<PhaseAmplitudeOffset>();
        private bool simulated;
        private PavtMeasurement pavt = new PavtMeasurement();
        private BeamformerBase beamformer;
        private bool isStepped;
        int segmentSamples;
        double segmentTime;
        public frmBeamformerPavtController()
        {
            InitializeComponent();
            LoadDeviceNames();
            simulated = true; // setting this to true as default
            isStepped = false; // setting this to false as default
            btnSequenced.Checked = true; // setting this true false as default
        }

        private void LoadDeviceNames()
        {
            ModularInstrumentsSystem rfsgSystem = new ModularInstrumentsSystem("NI-Rfsg");
            foreach (DeviceInfo device in rfsgSystem.DeviceCollection)
                rfsgNameComboBox.Items.Add(device.Name);
            if (rfsgSystem.DeviceCollection.Count > 0)
                rfsgNameComboBox.SelectedIndex = 0;

            ModularInstrumentsSystem rfmxSystem = new ModularInstrumentsSystem("NI-Rfsa");
            foreach (DeviceInfo device in rfmxSystem.DeviceCollection)
                rfsaNameComboBox.Items.Add(device.Name);
            if (rfsgSystem.DeviceCollection.Count > 0)
                rfsaNameComboBox.SelectedIndex = 0;
        }
        #region UI Events
        private void btnAddOffset_Click(object sender, EventArgs e)
        {
            AddOffset();
        }
        private void EditListViewItem(object sender, EventArgs e)
        {
            if (CheckSelection(out int selected))
            {
                EditOffset(selected);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CheckSelection(out int selected))
            {
                RemoveOffset(selected);
            }
        }
        private void lsvOffsets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CheckSelection(out int _))
            {
                btnDeleteOffset.Enabled = btnEditOffset.Enabled = true;
            }
            else
            {
                btnDeleteOffset.Enabled = btnEditOffset.Enabled = false;
            }
        }
        private void lsvOffsets_KeyDown(object sender, KeyEventArgs e)
        {
            if (CheckSelection(out int selected))
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        EditOffset(selected);
                        break;
                    case Keys.Delete:
                        RemoveOffset(selected);
                        break;
                }
            }
        }

        private void frmBeamformerPavtController_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseInstruments();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartGeneration();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            AbortGeneration();
        }
        private void btnSequenced_CheckedChanged(object sender, EventArgs e)
        {
            isStepped = false;
            btnStepped.Checked = false;
            AbortGeneration();
        }

        private void btnStepped_CheckedChanged(object sender, EventArgs e)
        {
            isStepped = true;
            btnSequenced.Checked = false;
            AbortGeneration();
        }
        private void btnSequenced_MouseDown(object sender, MouseEventArgs e)
        {
            btnSequenced.Checked = true;
        }

        private void btnStepped_MouseDown(object sender, MouseEventArgs e)
        {
            btnStepped.Checked = true;
        }

        #endregion
        #region Program Functions
        private void StartGeneration()
        {
            double frequency, power;
            try
            { 
                SetButtonState(true);
                lsvResults.Items.Clear();
                errorTextBox.Text = " ";
                // Initialize sessions
                rfsg = new NIRfsg(rfsgNameComboBox.Text, true, false);
                pavt.InitSession(rfsaNameComboBox.Text);
                // Subscribe to Rfsg warnings
                rfsg.DriverOperation.Warning += new EventHandler<RfsgWarningEventArgs>(DriverOperation_Warning);

                // Read in all the control values 
                frequency = (double)frequencyNumeric.Value;
                power = (double)powerLevelNumeric.Value;

                // Configure SG
                rfsg.RF.Configure(frequency, power);

                // Initiate Generation 
                rfsg.Initiate();

                //Configure measurement
                segmentTime = (double)(measurementLengthNumeric.Value + measurementOffsetNumeric.Value) * 1e-6 * 2;
                pavt.Configure(isStepped, frequency, power, measurementLengthNumeric.Value * (decimal)1e-6, measurementOffsetNumeric.Value * (decimal)1e-6,
                                offsetList, RFmxSpecAnMXConstants.PxiTriggerLine1, segmentTime);//tentative trig source

                // Check beamformer type
                BeamformerBase.BeamformerType beamformerType;
                if (isStepped)
                    beamformerType = BeamformerBase.BeamformerType.Stepped;
                else
                    beamformerType = BeamformerBase.BeamformerType.Sequeneced;
                                
                // Initialize beamformer    
                if (simulated)
                {
                    beamformer = new SimulatedBeamformer(rfsg, beamformerType);
                }
                else
                {
                    // Prompt user to configure DCPower and Digital insturments for controlling the hardware beamformer.'
                    throw new NotImplementedException();
                }

                // Connect beamformer
                beamformer.Connect();

                segmentSamples = (int)Math.Round(segmentTime * rfsg.Arb.IQRate);


                if (!isStepped)
                {
                    // Configure beamformer's phase and amplitude offset values, as well as segment timing
                    beamformer.ConfigureSequence(offsetList, segmentSamples);

                    // Init measurement
                    pavt.Initiate();

                    // Start beamformer
                    beamformer.InitiateSequence();
                }
                else
                {
                    // Init measurement
                    pavt.Initiate();
                    // Write Offsets, one at a time.
                    foreach (PhaseAmplitudeOffset offset in offsetList)
                    {                        
                        beamformer.WriteOffset(offset);
                        // Wait in SW for measurment to be done.
                        System.Threading.Thread.Sleep(1000);
                    }                    
                }



                //get results      
                List<PhaseAmplitudeOffset> results = pavt.FetchResults();
                for (int i = 0; i < results.Count; i++)
                {
                    lsvResults.Items.Add(results[i].GetDisplayItem(i));
                }

            }
            catch (Exception ex)
            {
                ShowError("StartGeneration()", ex);
            }
            finally
            {
                //cleanup
                CloseInstruments();
            }
        }

        private void AbortGeneration()
        {
            SetButtonState(false);

            if (beamformer != null)
            {
                beamformer.AbortSequence();
                beamformer.Disconnect();
            }

            if (rfsg!= null && !rfsg.IsDisposed)
            {
                rfsg.Abort();
            }
        }
        private void CloseInstruments()
        {
            try
            {
                AbortGeneration();
                rfsg?.Close();
                pavt.CloseSession();
            }
            catch (Exception e)
            {
                ShowError("CloseInstruments()", e);
            }
        }
        private void SetButtonState(bool started)
        {
            btnStart.Enabled = !started;
            btnStop.Enabled = started;
        }
        void ShowError(string functionName, Exception exception)
        {
            AbortGeneration();
            errorTextBox.Text = "Error in " + functionName + ": " + exception.Message;
        }
        void SetStatus(string statusMessage)
        {
            errorTextBox.Text = statusMessage;
        }
        void DriverOperation_Warning(object sender, RfsgWarningEventArgs e)
        {
            // Display the rfsg warning
            SetStatus(e.Message);
        }
        #endregion
        #region Offset Functions
        private void AddOffset()
        {
            frmOffset dialog = new frmOffset(frmOffset.Mode.Add);

            DialogResult r = dialog.ShowDialog();

            if (r == DialogResult.OK)
            {
                // Add the offset to the listview
                offsetList.Add(dialog.Offset);
                lsvOffsets.Items.Add(dialog.Offset.GetDisplayItem());
            }
        }
        private void EditOffset(int selected)
        {
            // Check selected item
            CheckSelection(out int indexSelected);

            PhaseAmplitudeOffset offset = new PhaseAmplitudeOffset();
            frmOffset dialog = new frmOffset(frmOffset.Mode.Edit);
            ListViewItem selectedOffsetItem = lsvOffsets.Items[indexSelected];

            offset.Phase = Decimal.Parse(selectedOffsetItem.SubItems[0].Text);
            offset.Amplitude = Decimal.Parse(selectedOffsetItem.SubItems[1].Text);

            // Set initial value of offset to pre-populate the dialog box with.
            dialog.Offset = offset;
            DialogResult r = dialog.ShowDialog();

            if (r == DialogResult.OK)
            {
                // Edit the offset shown in the listview
                offsetList.RemoveAt(indexSelected);
                lsvOffsets.Items.RemoveAt(indexSelected);
                offsetList.Insert(indexSelected, dialog.Offset);
                lsvOffsets.Items.Insert(indexSelected, dialog.Offset.GetDisplayItem());
            }
        }

        private void RemoveOffset(int selected)
        {
            offsetList.RemoveAt(selected);
            lsvOffsets.Items.RemoveAt(selected);
        }
        #endregion
        #region Utility Functions

        /// <summary>
        /// Validates that the listview has at least one value selected. Optionally returns the selected index.
        /// </summary>
        /// <param name="selectedIndex">Current selected index in the list view.</param>
        /// <returns></returns>
        private bool CheckSelection(out int selectedIndex)
        {
            if (lsvOffsets.SelectedItems.Count == 1)
            {
                selectedIndex = lsvOffsets.SelectedIndices[0];
                return true;
            }
            else
            {
                selectedIndex = -1;
                return false;
            }
        }

        #endregion
    }
}
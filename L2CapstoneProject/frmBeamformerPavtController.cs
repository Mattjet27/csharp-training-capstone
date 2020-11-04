﻿using System;
using System.Windows.Forms;
using NationalInstruments.ModularInstruments.NIRfsg;
using NationalInstruments.RFmx.InstrMX;
using NationalInstruments.ModularInstruments.SystemServices.DeviceServices;
using System.Collections.Generic;

namespace L2CapstoneProject
{

    public partial class frmBeamformerPavtController : Form
    {
        NIRfsg rfsg;
        RFmxInstrMX instr;
        public List<PhaseAmplitudeOffset> offsetList = new List<PhaseAmplitudeOffset>();
        private SimulatedBeamformer beamformer;

        public frmBeamformerPavtController()
        {
            InitializeComponent();
            LoadDeviceNames();
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

        #endregion
        #region Program Functions
        private void StartGeneration()
        {
            double frequency, power;
            try
            {
                SetButtonState(true);                
                // Initialize the NIRfsg session
                rfsg = new NIRfsg(rfsgNameComboBox.Text, true, false);
                // Subscribe to Rfsg warnings
                rfsg.DriverOperation.Warning += new EventHandler<RfsgWarningEventArgs>(DriverOperation_Warning);

                // Read in all the control values 
                frequency = (double)frequencyNumeric.Value;
                power = (double)powerLevelNumeric.Value;

                // Configure the RFSG instrument 
                rfsg.RF.Configure(frequency, power);
                // Initiate Generation 
                rfsg.Initiate();

                // Initialize beamformer
                beamformer = new SimulatedBeamformer(rfsg);
                // Connect and iniate beamformer
                beamformer.Connect();

                // Configure beamformer's phase and amplitude offset values
                foreach (PhaseAmplitudeOffset offset in offsetList)
                    beamformer.WriteOffset(offset);
                    MeasureResponse();

            }
            catch (Exception ex)
            {
                ShowError("UpdateGeneration()", ex);
            }
        }
        
        private void MeasureResponse()
        {
            // Add mesuremnet code here...
        }
        private void AbortGeneration()
        {
            SetButtonState(false);
                      
            if (beamformer != null)
            {
                beamformer.Disconnect();
            }

            if (rfsg?.IsDisposed == false)
            {
                rfsg.Abort();
            }
        }
        private void CloseInstruments()
        {
            AbortGeneration();
            rfsg?.Close();
            instr?.Close();
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
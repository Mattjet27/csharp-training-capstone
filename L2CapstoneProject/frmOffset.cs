using System;
using System.Windows.Forms;
using static L2CapstoneProject.Validator;

namespace L2CapstoneProject
{
    public partial class frmOffset : Form
    {
        private PhaseAmplitudeOffset offset = new PhaseAmplitudeOffset();
        
        public PhaseAmplitudeOffset Offset 
        { 
            get
            {
                return offset;
            }
            set
            {
                if (ViewMode == Mode.Edit)
                {
                    offset = value;
                    numPhase.Value = value.Phase;
                    numAmp.Value = value.Amplitude;
                }
                else
                    throw new Exception($"Cannot write to the {nameof(Offset)} property when the form object is instaniated in Edit mode.");
            }
        } 
        public enum Mode { Add, Edit }

        public Mode ViewMode { get; }

        public frmOffset(Mode mode)
        {
            InitializeComponent();
            ViewMode = mode;

            switch (ViewMode)
            {
                case Mode.Add:
                    this.Text = "Add New Offset";
                    break;
                case Mode.Edit:
                    this.Text = "Edit Offset";
                    break;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                offset.Amplitude = numAmp.Value;
                offset.Phase = numPhase.Value;
            }
            Close();
        }

        public bool ValidateInput() => IsPresent(numPhase) && IsPresent(numAmp) && IsWithinRange(numPhase, -180, 180) && IsWithinRange(numAmp,-50,50);

    }
}

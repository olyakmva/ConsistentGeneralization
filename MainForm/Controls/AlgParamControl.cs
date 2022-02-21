using System;
using System.Globalization;
using System.Windows.Forms;
using AlgorithmsLibrary;

namespace MainForm.Controls
{
    public partial class AlgParamControl : UserControl
    {
        private ISimplificationAlgm _algm;
        public event EventHandler CopyingParams ;
        
        public double Tolerance
        {
            get => Convert.ToDouble(paramUpDown.Value);
            set => paramUpDown.Value = Convert.ToDecimal( value);
        }

        public double DetailSize
        {
            get => double.Parse(detailUpDown.Text);
            set => detailUpDown.Value = Convert.ToDecimal( value);
        }

        public string AlgmName
        {
            set => lblName.Text = value;
            get => lblName.Text;
        }

        public double OutScale
        {
            get => double.Parse(scaleUpDown.Text);
            set => scaleUpDown.Text = value.ToString(CultureInfo.InvariantCulture);
        }

        public bool IsChecked
        {
            get => checkRun.Checked;
            private set => checkRun.Checked = value;
        }

        public AlgParamControl()
        {
            InitializeComponent();
        }
        public ISimplificationAlgm GetAlgorithm()
        {
            var p = new SimplificationAlgmParameters();
            _algm = AlgmFabrics.GetAlgmByNameAndParam(AlgmName);
            p.Tolerance = Math.Truncate(OutScale * Convert.ToDouble(paramUpDown.Value));
            p.OutScale = Convert.ToInt32(OutScale);
            _algm.Options = p;
            return _algm;
        }

        
        private void BtnCopyClick(object sender, EventArgs e)
        {
            CopyingParams?.Invoke(this, EventArgs.Empty);
        }

        public void OnCopyingParams(object sender, EventArgs e)
        {
            AlgParamControl ctrl = (AlgParamControl) sender;
            Tolerance  = ctrl.Tolerance;
            DetailSize = ctrl.DetailSize;
            OutScale = ctrl.OutScale;
            IsChecked = ctrl.IsChecked;
        }
    }
}

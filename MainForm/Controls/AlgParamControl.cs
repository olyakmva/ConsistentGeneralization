using System;
using System.Globalization;
using System.Windows.Forms;
using AlgorithmsLibrary;

namespace MainForm.Controls
{
    public partial class AlgParamControl : UserControl
    {
        private ISimplificationAlgm _algm;
                
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

        public string ScaleCellName
        {
            get => lblOutScale.Text;
            set=> lblOutScale.Text=value;
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
    }
}

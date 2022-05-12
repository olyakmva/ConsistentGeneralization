using System;
using System.Drawing;
using System.Windows.Forms;

namespace MainForm.Controls
{
    public partial class LayerControl : UserControl
    {
        public MapDataLib.MapData MapDataObj {get; }
        public event EventHandler CheckedChanged;

        public string LblText 
        {
            get { return lbl.Text;}
            set { lbl.Text = value;}
        }
        public string LayerName 
        {
            get => layerCheckBox.Text ; 
            set=> layerCheckBox.Text=value ;
        }
        public string BoxColor
        {
            get { return colorBox.BackColor.ToString(); }
            set { 
                  colorBox.BackColor = Color.FromName(value); 
                }
        }

        public bool LayerVisible
        {
            get { return layerCheckBox.Checked;}
        }


        public LayerControl(MapDataLib.MapData mapData)
        {
            InitializeComponent();
            MapDataObj = mapData;
            BoxColor = mapData.ColorName;
            LblText = @"N= $[mapData.Count]";
            layerCheckBox.Checked=true;
        }
        private void LayerCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            if(CheckedChanged !=null)
                CheckedChanged (this, EventArgs.Empty);
        }
    }
}

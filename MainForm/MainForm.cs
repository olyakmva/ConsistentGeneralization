using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GridLib;
using MainForm.Controls;
using  MapDataLib;
using Point = System.Drawing.Point;


namespace MainForm
{
    public partial class MainForm : Form
    {
        private Map _inputMap;
        readonly GraphicsState _state = new GraphicsState();
        private double _currentScale;
        private List<AlgParamControl> _listCtrls;
        private readonly string _applicationPath;
        private Grid _grid;
        public MainForm()
        {
            InitializeComponent();
            _state.Scale = 2;
            mapPictureBox.MouseWheel += MapPictureBoxMouseWheel;
             CreateAlgmControls();
            _applicationPath = Environment.CurrentDirectory;
            _inputMap = new Map();
            Colors.Init();
        }

        private void CreateAlgmControls()
        {
            int x = 0, y = 55, ctrlHeight = 120;
            _listCtrls = new List<AlgParamControl>();
            //AlgParamControl douglasCtrl = new AlgParamControl()
            //{
            //    Location = new Point(x, y),
            //    AlgmName = "DouglasPeuckerAlgm"
            //};
            //_listCtrls.Add(douglasCtrl);
            //y += ctrlHeight;
            //AlgParamControl liCtrl = new AlgParamControl()
            //{
            //    AlgmName = "LiOpenshawAlgm",
            //    Location = new Point(x, y)
            //};
            //_listCtrls.Add(liCtrl);
            //y += ctrlHeight;
            //AlgParamControl visvWhyatCtrl = new AlgParamControl()
            //{
            //    AlgmName = "VisvWhyattAlgm",
            //    Location = new Point(x, y)
            //};
            //y += ctrlHeight;
            //_listCtrls.Add(visvWhyatCtrl);

            AlgParamControl sleeveFitCtrl = new AlgParamControl()
            {
                AlgmName = "SleeveFitAlgm",
                Location = new Point(x, y)
            };
            y += ctrlHeight;
            _listCtrls.Add(sleeveFitCtrl);
             //mainContainer.Panel1.Controls .Add(douglasCtrl);
            //mainContainer.Panel1.Controls.Add(liCtrl);
            //mainContainer.Panel1 .Controls.Add(visvWhyatCtrl);
            mainContainer .Panel1.Controls.Add(sleeveFitCtrl);
           

            //foreach (var ctrl in _listCtrls)
            //{
            //    foreach (var otherCtrl in _listCtrls)
            //    {
            //        if(ctrl == otherCtrl)
            //            continue;
            //        ctrl.CopyingParams += otherCtrl.OnCopyingParams;
            //    }
            //}
         
            btnProcess.Location = new Point(x,y);
        }
        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Path.Combine(_applicationPath, "Data");
            openFileDialog1.Filter = @"shape files (*.shp)|*.shp|All files (*.*)|*.*";
            openFileDialog1.DefaultExt = "*.shp";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                try
                {
                    string shapeFileName = openFileDialog1.FileName;
                    var shapeFile = new ShapeFileIO(); 
                    var mapObj = shapeFile.Open(shapeFileName);
                    _inputMap.Add(mapObj);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, @"Error : " + ex.Message);
                    return;
                }
            string fileName = Path.GetFileName(openFileDialog1.FileName);
            Text = $@"Algorithm Comparision  {fileName} ";
             
        }
  
       private void SetGraphicsParams(Map inputMap)
       {
            double xmin = inputMap.Xmin, xmax=inputMap.Xmax;
            double ymin=inputMap.Ymin, ymax=inputMap.Ymax;
            
            var g = CreateGraphics();
            if ((xmax - xmin) / mapPictureBox.Width > (ymax - ymin) / mapPictureBox.Height)
            {
                    _state.Scale = (xmax - xmin) / (mapPictureBox.Width - 40);
                    _state.InitG(g, 1);
            }
            else
            {
                    _state.Scale = (ymax - ymin) / (mapPictureBox.Height - 40);
                    _state.InitG(g, 2);
            }
            _currentScale = Math.Truncate(_state.Scale * _state.PixelPerMm);
            lblCurScaleValue.Text = _currentScale.ToString(CultureInfo.InvariantCulture);
            _state.CenterX = xmin;
            _state.CenterY = ymax;
            _state.DefscaleX = (xmax - xmin);
            _state.DefscaleY = (ymax - ymin);
            _state.DefCenterX = xmin;
            _state.DefCenferY = ymax;
       }
        
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        #region Отрисовка карты

        private void MapPictureBoxPaint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);
            foreach (var mapData in _inputMap.MapLayers )
            {

                var c = Color.FromName(Colors.GetNext()) ; 
                var pen = new Pen(c, 1.75f);
                Display(g, mapData, pen);
            }

            if (_grid != null)
            {
                var c = Color.FromName("Black");
                var pen = new Pen(c, 1.25f);
                DisplayGrid(g,_grid,pen);
            }
            g.Flush();
        }

        /// <summary>
        /// Отображение MapData md на графике g
        /// </summary>
        /// <param name="g"></param>
        /// <param name="md"></param>
        /// <param name="pen">Цвет в случае отображения нескольких MapData на одном picturebox</param>
        private void Display(Graphics g, MapData md, Pen pen)
        {
            foreach (var pair in md.MapObjDictionary)
            {
                var list = pair.Value;
                if (list.Count == 0)
                    continue;
                for (var j = 0; j < (list.Count - 1); j++)
                {
                    var pt1 = _state.GetPoint(list[j], mapPictureBox.Height - 1);
                    var pt2 = _state.GetPoint(list[j + 1], mapPictureBox.Height - 1);
                    g.DrawLine(pen, pt1, pt2);
                }
            }
        }

        private void DisplayGrid(Graphics g, Grid grid, Pen pen)
        {
            for (int i = 0; i < grid.Cells.GetLength(0); i++)
            {
                for (var j = 0; j < grid.Cells.GetLength(1); j++)
                {
                    if (grid.Cells[i, j] == null) continue;
                    foreach (var cell in grid.Cells[i,j].GetAllCells())
                    {
                        var pts = cell.GetPoints().ToList();
                        for (var k = 0; k < pts.Count - 1; k++)
                        {
                            var pt1 = _state.GetPoint(pts[k], mapPictureBox.Height - 1);
                            var pt2 = _state.GetPoint(pts[k+1], mapPictureBox.Height - 1);
                            g.DrawLine(pen, pt1, pt2);
                        }
                        var p1 = _state.GetPoint(pts[0], mapPictureBox.Height - 1);
                        var p2 = _state.GetPoint(pts[pts.Count-1], mapPictureBox.Height - 1);
                        g.DrawLine(pen, p1, p2);
                    }
                }
            }
        }
        private void MapPictureBoxMouseLeave(object sender, EventArgs e)
        {
            if (mapPictureBox.Focused)
                mapPictureBox.Parent.Focus();
        }

        private void MapPictureBoxMouseUp(object sender, MouseEventArgs e)
        {
            mapPictureBox.Invalidate();
        }

        private void MapPictureBoxMouseDown(object sender, MouseEventArgs e)
        {
            _state.Mousex = e.X;
            _state.Mousey = e.Y;
        }

        private void MapPictureBoxMouseEnter(object sender, EventArgs e)
        {
            if (!mapPictureBox.Focused)
                mapPictureBox.Focus();
        }

        private void MapPictureBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button.HasFlag(MouseButtons.Left))
            {
                _state.CenterX += (_state.Mousex - e.X) * _state.Scale;
                _state.CenterY -= (_state.Mousey - e.Y) * _state.Scale;
                _state.Mousex = e.X;
                _state.Mousey = e.Y;
            }
        }
        private void MapPictureBoxMouseWheel(object sender, MouseEventArgs e)
        {
            var x = 0;
            if (e.Delta > 0)
                x = -1;
            else if (e.Delta < 0)
                x = 1;
            _state.Zoom(x, mapPictureBox.Width, mapPictureBox.Height);
            _currentScale = Math.Truncate(_state.Scale * _state.PixelPerMm);
            lblCurScaleValue.Text = _currentScale.ToString(CultureInfo.InvariantCulture);
            mapPictureBox.Invalidate();
        }
        private void MapSplitContainerPanel1Resize(object sender, EventArgs e)
        {
            mapPictureBox.Width = Width;
            mapPictureBox.Height = Height;
            mapPictureBox.Invalidate();
        }
        private void MapPictureBoxMouseDoubleClick(object sender, MouseEventArgs e)
        {
            _state.Scale = Math.Max(_state.DefscaleX / mapPictureBox.Width, _state.DefscaleY / mapPictureBox.Height);
            _state.CenterX = _state.DefCenterX;
            _state.CenterY = _state.DefCenferY;
            _currentScale = Math.Truncate(_state.Scale * _state.PixelPerMm);
            lblCurScaleValue.Text = _currentScale.ToString(CultureInfo.InvariantCulture);
            mapPictureBox.Invalidate();
        }
        private void BtnSetScaleClick(object sender, EventArgs e)
        {
            if (viewScaleUpDown.Text.Length <= 2)
            {
                MessageBox.Show(@"Задайте масштаб. ");
                return;
            }
            var viewScale = double.Parse(viewScaleUpDown.Text);
            _state.Scale = Math.Round(viewScale / _state.PixelPerMm);
            _currentScale = Math.Truncate(_state.Scale * _state.PixelPerMm);
            lblCurScaleValue.Text = _currentScale.ToString(CultureInfo.InvariantCulture);
            mapPictureBox.Invalidate();
        }


        #endregion

        private void BtnProcessClick(object sender, EventArgs e)
        {
            if (_inputMap == null)
            {
                MessageBox.Show(@"Please, load a map ");
                return;
            }

            double cellSize = _listCtrls[0].OutScale * 2;
            double detail = _listCtrls[0].DetailSize * _listCtrls[0].OutScale;
            _grid = new Grid(_inputMap, cellSize,detail);

            //foreach (var ctrl in _listCtrls)
            //{
            //    if (!ctrl.IsChecked)
            //        continue;
            //    var map = _inputMap.Clone();
                
            //    ISimplificationAlgm algm = ctrl.GetAlgorithm();
            //    if (ctrl.IsPercentParametr || ctrl.IsBendParametr)
            //    {
            //        algm.Options.PointNumberGap = Convert.ToDouble(percentErrUpDn.Value);
            //    }
            //    algm.Run(map);
                
            //    var layerName = $"{ctrl.AlgmName}";
            //    var l = new Layer(map, layerName, algm.Options.OutScale)
            //    {
            //        GenHausdDist = GenHausdorfDistance.Get(_inputMap, map)
            //    };
            //    l.Characteristics.ParamValue = Math.Round(algm.Options.OutParam);
            //    if (ctrl.IsPercentParametr)
            //    {
            //        l.Characteristics.IsPercent = true;
            //    }
            //    if (ctrl.IsBendParametr)
            //    {
            //        l.Characteristics.IsBend = true;
            //    }
            //    l.Characteristics.Length = Math.Round(l.Characteristics.Length / _layers[0].Characteristics.Length, 2);

            //    if (_gaussFilterMap != null)
            //    {
            //        l.FilterModifHausdDistance = GenHausdorfDistance.Get(_gaussFilterMap, map);
            //    }
            //    _layers.Add(l);
            //    AddNewRowToResultTable(l);
            //}
            mapPictureBox.Invalidate();
        }

        private void OnLayerVisibleChanged(object sender, EventArgs e)
        {
            mapPictureBox.Invalidate();
        }

        private void ShowToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_inputMap == null)
            {
                MessageBox.Show(@"Please, load a map ");
                return;
            }
            SetGraphicsParams(_inputMap);
            mapPictureBox.Invalidate();
        }
    }
}

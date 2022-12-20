using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AmteCreator.Controls
{
    public delegate void OnSelectedControlChangedHandler();

    public partial class ControlList<T> : UserControl where T : Control
    {
        public enum ControlListMode
        {
            Horizontal,
            Vertical
        }

        private readonly List<Lazy<T>> _controls = new List<Lazy<T>>();
        private readonly List<Lazy<T>> _visibleControls = new List<Lazy<T>>();
        private T _selectedControl;

        public event OnSelectedControlChangedHandler OnSelectedControlChanged;

        public T SelectedControl
        {
            get { return _selectedControl; }
            private set
            {
                if (_selectedControl == value)
                    return;
                if (value != null)
                    value.Enabled = false;
                if (_selectedControl != null)
                    _selectedControl.Enabled = true;
                _selectedControl = value;
                if (OnSelectedControlChanged != null)
                    OnSelectedControlChanged();
            }
        }

        public ControlListMode Mode = ControlListMode.Vertical;

        public ControlList()
        {
            InitializeComponent();
        }

        public void AddControl(Lazy<T> control)
        {
            _controls.Add(control);
            _UpdateControls();
        }

        public void RemoveControl(Lazy<T> control)
        {
            if (control.IsValueCreated)
                _Hide(control);
            _controls.Remove(control);
            _UpdateControls();
        }

        private void _Show(Lazy<T> control)
        {
            Controls.Add(control.Value);
            control.Value.MouseClick += control_MouseClick;
            control.Value.MouseDown += ControlList_MouseDown;
            control.Value.MouseMove += ControlList_MouseMove;
            control.Value.MouseDoubleClick += ControlList_MouseDoubleClick;
            _visibleControls.Add(control);
        }

        private void _Hide(Lazy<T> control)
        {
            Controls.Remove(control.Value);
            control.Value.MouseClick -= control_MouseClick;
            control.Value.MouseDown -= ControlList_MouseDown;
            control.Value.MouseMove -= ControlList_MouseMove;
            control.Value.MouseDoubleClick -= ControlList_MouseDoubleClick;
            _visibleControls.Remove(control);
        }

        private float _current = 0;
        private const int _sizeH = 128;
		private const int _sizeV = 40;

		private void _UpdateControls()
        {
			if (Mode == ControlListMode.Horizontal)
			{
				int canDisplay = Width / _sizeH + 1;
				if (canDisplay <= 0 || _controls.Count == 0)
					return;
				int min = (int)_current;

				hScrollBar1.Maximum = _controls.Count;
				hScrollBar1.LargeChange = Width / _sizeH;

				var deleted = _visibleControls.ToList();
				for (int i = 0; min < _current + canDisplay && min < _controls.Count; ++min, ++i)
				{
					_controls[min].Value.Location = new Point(i * _sizeH, 0);
					if (!_visibleControls.Contains(_controls[min]))
						_Show(_controls[min]);
					else if (deleted.Contains(_controls[min]))
						deleted.Remove(_controls[min]);
				}
				deleted.ForEach(_Hide);
			}
			else
			{
				int canDisplay = Height / _sizeV + 1;
				if (canDisplay <= 0 || _controls.Count == 0)
					return;
				int min = (int)_current;

				vScrollBar1.Maximum = _controls.Count;
				vScrollBar1.LargeChange = Height / _sizeV;

				var deleted = _visibleControls.ToList();
				for (int i = 0; min < _current + canDisplay && min < _controls.Count; ++min, ++i)
				{
					_controls[min].Value.Location = new Point(0, i * _sizeV);
					if (!_visibleControls.Contains(_controls[min]))
						_Show(_controls[min]);
					else if (deleted.Contains(_controls[min]))
						deleted.Remove(_controls[min]);
				}
				deleted.ForEach(_Hide);
			}
        }

        private void control_MouseClick(object sender, MouseEventArgs e)
        {
            var ctrl = sender as T;
            SelectedControl = ctrl;
        }

        private int _mouseLastLoc;
        private void ControlList_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    Point good = ((Control)sender).Location + (Size)e.Location;
                    if (Mode == ControlListMode.Horizontal)
                    {
                        int delta = -good.X + _mouseLastLoc;
                        if (delta + HorizontalScroll.Value > 0)
                            HorizontalScroll.Value += delta;
                        else
                            HorizontalScroll.Value = 0;
                        _mouseLastLoc = good.X;
                    }
                    else
                    {
                        int delta = good.Y - _mouseLastLoc;
                        if (delta + VerticalScroll.Value > 0)
                            VerticalScroll.Value += delta;
                        else
                            VerticalScroll.Value = 0;
                        _mouseLastLoc = good.Y;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void ControlList_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseLastLoc = (Mode == ControlListMode.Horizontal ? ((Control)sender).Location.X + e.Location.X : ((Control)sender).Location.Y + e.Location.Y);
        }

        private void ControlList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnMouseDoubleClick(e);
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
			if (Mode == ControlListMode.Horizontal)
			{
				_current = hScrollBar1.Value;
				_UpdateControls();
			}
        }
		private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
		{
			if (Mode == ControlListMode.Vertical)
			{
				_current = vScrollBar1.Value;
				_UpdateControls();
			}
		}

		private void ControlList_SizeChanged(object sender, EventArgs e)
        {
            _UpdateControls();
        }

        public void SetSelected(int selected)
        {
			if (Mode == ControlListMode.Horizontal)
			{
				int canDisplay = (Width / _sizeH + 1) / 2;
				_current = (selected >= canDisplay ? selected - canDisplay : selected);
				hScrollBar1.Value = (int)_current;
			}
			else
			{
				int canDisplay = (Height / _sizeV + 1) / 2;
				_current = (selected >= canDisplay ? selected - canDisplay : selected);
				vScrollBar1.Value = (int)_current;
			}
			_UpdateControls();
        }
	}
}

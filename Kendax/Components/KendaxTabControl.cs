using System;
using System.Linq;
using System.Text;
using Sulakore.Design;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Kendax.Components
{
    public class KendaxTabControl : TabControl
    {
        #region Public Properties
        private bool _DisplayBoundary;
        #region Attributes
        [DefaultValue(false)]
        #endregion
        public bool DisplayBoundary
        {
            get { return _DisplayBoundary; }
            set { _DisplayBoundary = value; Invalidate(); }
        }

        public new Size ItemSize
        {
            get
            {
                if (Alignment == TabAlignment.Left || Alignment == TabAlignment.Right)
                    return new Size(base.ItemSize.Height, base.ItemSize.Width);
                return base.ItemSize;
            }
            set
            {
                if (Alignment == TabAlignment.Left || Alignment == TabAlignment.Right)
                    base.ItemSize = new Size(value.Height, value.Width);
                else
                    base.ItemSize = value;

                Invalidate();
            }
        }
        #endregion

        #region Constructor(s)
        public KendaxTabControl()
            : base()
        {
            SetStyle((ControlStyles)2050, true);
            DoubleBuffered = true;

            SizeMode = TabSizeMode.Fixed;
            ItemSize = new Size(95, 24);

            DrawMode = TabDrawMode.OwnerDrawFixed;
        }
        #endregion
        #region Protected Methods ( Overrides )
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            foreach (TabPage TP in TabPages)
            {
                int CurIndex = TabPages.IndexOf(TP);
                Rectangle TR = GetTabRect(CurIndex);
                Rectangle TitleR = new Rectangle(TR.X, TR.Y, TR.Width, TR.Height);
                Rectangle NTR = new Rectangle(TR.X + (CurIndex == 0 ? 2 : 0), (TR.Y + TR.Height) - 3, TR.Width - (CurIndex == 0 ? 4 : 2), 2);

                if (Alignment == TabAlignment.Top || Alignment == TabAlignment.Bottom)
                {
                    e.Graphics.FillRectangle(SelectedIndex == CurIndex ? Color.Teal : Color.Silver, NTR);
                    e.Graphics.DrawString(TP.Text, Font, Color.Black, TitleR, StringAlignment.Center);
                }
                else if (Alignment == TabAlignment.Right || Alignment == TabAlignment.Left)
                {
                    TitleR = new Rectangle(TitleR.X + 4, TitleR.Y, TitleR.Width, TitleR.Height);
                    NTR = new Rectangle(TR.X, TR.Y + (CurIndex == 0 ? 2 : 0), 2, TR.Height - (CurIndex == 0 ? 4 : 2));
                    e.Graphics.FillRectangle(SelectedIndex == CurIndex ? Color.Teal : Color.Silver, NTR);
                    e.Graphics.DrawString(TP.Text, Font, Color.Black, TitleR, StringAlignment.Near, StringAlignment.Center);
                }
            }

            if (_DisplayBoundary)
                e.Graphics.DrawLine(Color.Teal, 0, Height - 1, Width - 1, Height - 1);
            base.OnPaint(e);
        }
        #endregion
    }
}
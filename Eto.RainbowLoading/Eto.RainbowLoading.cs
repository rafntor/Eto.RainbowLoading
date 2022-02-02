
namespace Eto.RainbowLoading
{
	using System;
	using System.Collections.Generic;
	using Eto.Drawing;
	using Eto.Forms;
	public class RainbowLoading : Drawable
	{
		private Color _currentColor;
		private double _progress0 = 0.0;
		private double _progress = 0.0;
		private double _rotation = 0.0;
		private int _currentColorIndex = 0;
		private const float _progressArcDiameterProportion = 0.65f;
		private readonly UITimer _progressTimer = new UITimer() { Interval = 0.02 };

		public TimeSpan ProgressDuration { get; set; } = TimeSpan.FromMilliseconds(1400);
		public TimeSpan RotationDuration { get; set; } = TimeSpan.FromMilliseconds(2000);

		public Color ControlColor { get; set; } = SystemColors.Control;
		public List<Color> ProgressColors { get; set; } = new List<Color>
			 {
					 Color.FromArgb(66,133,244),
					 Color.FromArgb(219,68,55),
					 Color.FromArgb(244,160,0),
					 Color.FromArgb(15,157,88)
			 };

		public RainbowLoading()
		{
			BackgroundColor = SystemColors.ControlBackground;

			_currentColor = ProgressColors[_currentColorIndex];

			_progressTimer.Elapsed += progressTimer_Elapsed;
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			_progressTimer.Start();
		}
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);

			_progressTimer.Stop();
		}
		private void progressTimer_Elapsed(object sender, EventArgs e)
		{
			_rotation += _progressTimer.Interval / RotationDuration.TotalSeconds;
			_progress0 += _progressTimer.Interval / ProgressDuration.TotalSeconds;

			if (_progress0 > 1)
			{
				_currentColorIndex = ++_currentColorIndex % ProgressColors.Count;
				_currentColor = ProgressColors[_currentColorIndex];
				_progress0 -= 1;
			}
			if (_rotation > 1)
			{
				_rotation -= 1;
			}

			_progress = _progress0 < 0.5f ? Math.Pow(_progress0 * 2.0f, 3.0f) / 2.0f : (Math.Pow((_progress0 - 1) * 2.0f, 3.0f) + 2.0f) / 2.0f; // easing:CubicInOut

			this.Invalidate();
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			var canvasCenter = new PointF(Width, Height) / 2;

			// Clear the canvas and move the canvas center point to the viewport center
			e.Graphics.Clear(BackgroundColor);
			e.Graphics.TranslateTransform(canvasCenter);

			var radius = Math.Min(Height, Width) / 2;

			var boundingRect = new RectangleF(-radius, -radius, 2 * radius, 2* radius);

			e.Graphics.FillEllipse(ControlColor, boundingRect);

			// Rotate the canvas
			e.Graphics.RotateTransform((float)(_rotation * 360.0f));

			// Draw the progress arc
			var progressArcBoundingRect = boundingRect * _progressArcDiameterProportion;

			Pen progressPaint = new Pen(_currentColor, radius * 0.2f) { LineCap = PenLineCap.Butt };

			using (var arcPath = new GraphicsPath())
			{
				float startAngle = 0.0f;
				float sweepAngle = 0.0f;
				if (_progress < 0.5)
				{
					sweepAngle = (float)(2 * _progress * 360.0f);
					startAngle = -90.0f;
				}
				else
				{
					startAngle = ((float)_progress - 0.5f) // shift the range to (0, 0.5]
						 * 2 // map the range to (0, 1.0]
						 * 360.0f // map the range to (0, 360]
						 - 90.0f; // shift the range to (-90.0, 270]
					sweepAngle = 270.0f - startAngle;
				}

				sweepAngle = Math.Min(sweepAngle, 359); // bad effect if it bites its tail

				arcPath.AddArc(progressArcBoundingRect, startAngle, sweepAngle);

				e.Graphics.DrawPath(progressPaint, arcPath);
			}
		}
	}
}

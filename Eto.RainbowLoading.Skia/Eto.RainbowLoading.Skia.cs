
namespace Eto.RainbowLoading.Skia
{
	using System;
	using System.Collections.Generic;
	using Eto.Drawing;
	using Eto.Forms;
	using Eto.SkiaDraw;
	using SkiaSharp;

	public class RainbowLoading : SkiaDraw.SkiaDrawable
	{
		private double _progress0 = 0.0;
		private double _progress = 0.0;
		private double _rotation = 0.0;
		private int _currentColorIndex = 0;
		private const float _progressArcDiameterProportion = 0.65f;
		private readonly UITimer _progressTimer = new UITimer() { Interval = 0.02 };

		readonly SKPaint controlPaint = new SKPaint()
		{
			Color = SKColors.White,
			IsAntialias = true,
			IsStroke = false,
		};

		readonly SKPaint progressPaint = new SKPaint()
		{
			StrokeWidth = 10,
			IsAntialias = true,
			IsStroke = true,
		};

		public TimeSpan ProgressDuration { get; set; } = TimeSpan.FromMilliseconds(1400);
		public TimeSpan RotationDuration { get; set; } = TimeSpan.FromMilliseconds(2000);

		public Color ControlColor
		{
			get { return to_eto(controlPaint.Color); }
			set { controlPaint.Color = from_eto(value); }
		}
		public List<Color> ProgressColors { get; set; } = new List<Color>
			 {
					 Color.FromArgb(66,133,244),
					 Color.FromArgb(219,68,55),
					 Color.FromArgb(244,160,0),
					 Color.FromArgb(15,157,88)
			 };
		public Color ShadowColor { get; set; } = Colors.LightGrey;
		public bool HasShadow { get; set; } = true;
		public RainbowLoading()
		{
			BackgroundColor = Colors.White;

			progressPaint.Color = from_eto(ProgressColors[_currentColorIndex]);

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
				progressPaint.Color = from_eto(ProgressColors[_currentColorIndex]);
				_progress0 -= 1;
			}
			if (_rotation > 1)
			{
				_rotation -= 1;
			}

			_progress = _progress0 < 0.5f ? Math.Pow(_progress0 * 2f, 3f) / 2f : (Math.Pow((_progress0 - 1) * 2f, 3f) + 2f) / 2f; // easing:CubicInOut

			this.Invalidate();
		}
		static Color to_eto(SKColor c)
		{
			return Color.FromArgb(c.Red, c.Green, c.Blue, c.Alpha);
		}
		static SKColor from_eto(Color c)
		{
			return new SKColor((byte)c.Rb, (byte)c.Gb, (byte)c.Bb, (byte)c.Ab);
		}
		protected override void OnSizeChanged(EventArgs e)
		{
			if (Height < 1 || Width < 1)
				return;

			if (HasShadow)
			{
				controlPaint.ImageFilter = SKImageFilter.CreateDropShadow(
					 Width * 0.05f,
					 Height * 0.05f,
					 Width * 0.1f,
					 Height * 0.1f,
				  from_eto(ShadowColor),
				  SKDropShadowImageFilterShadowMode.DrawShadowAndForeground);
			}
			else
			{
				controlPaint.ImageFilter = null;
			}

			progressPaint.StrokeWidth = Width * 0.08f;
		}
		protected override void OnPaint(SKPaintEventArgs e)
		{
			var canvas = e.Surface.Canvas;

			var canvasCenter = new SKPoint(Width / 2f, Height / 2f);

			// Clear the canvas and move the canvas center point to the viewport center
			canvas.Clear();
			canvas.Translate(canvasCenter);

			// Draw background as a disc
			if (HasShadow)
			{
				canvas.Scale(0.73f, 0.73f);
			}
			var radius = Math.Min(Height, Width) / 2;

			canvas.DrawCircle(0, 0, radius, controlPaint);

			// Rotate the canvas
			canvas.RotateDegrees((float)(_rotation * 360f));

			// Draw the progress arc
			var progressArcBoundingRect = new SKRect(
				-radius * _progressArcDiameterProportion,
				-radius * _progressArcDiameterProportion,
				radius * _progressArcDiameterProportion,
				radius * _progressArcDiameterProportion);

			using (var arcPath = new SKPath())
			{
				float startAngle = 0f;
				float sweepAngle = 0f;

				if (_progress < 0.5)
				{
					sweepAngle = (float)(2 * _progress * 360f);
					startAngle = -90f;
				}
				else
				{
					startAngle = ((float)_progress - 0.5f) // shift the range to (0, 0.5]
						 * 2 // map the range to (0, 1]
						 * 360f // map the range to (0, 360]
						 - 90f; // shift the range to (-90, 270]
					sweepAngle = 270f - startAngle;
				}

				arcPath.AddArc(progressArcBoundingRect, startAngle, sweepAngle);

				canvas.DrawPath(arcPath, progressPaint);
			}
		}
	}
}

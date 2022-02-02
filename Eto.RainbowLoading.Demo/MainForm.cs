
namespace Eto.RainbowLoading.Demo
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using Eto.Drawing;
	using Eto.Forms;
	public partial class MainForm : Form
	{
		private readonly Random rand = new Random();
		private readonly CheckBox skia = new CheckBox() { Text = "Use Skia", Font = Fonts.Sans(20) };
		public MainForm()
		{
			Title = "My Eto Form";
			ClientSize = new Size(3, 2) * 300;

			skia.CheckedChanged += (o, e) => { MakeLayout(); };

			MakeLayout();
		}

		void MakeLayout()
		{ 
			var cells = GetVariationCells();

			var layout1 = new TableLayout() { Spacing = Size.Empty + 4 };
			layout1.Rows.Add(new TableRow(new TableCell(skia)));
			layout1.Rows.Add(new TableRow(cells[0], cells[1], cells[2]) { ScaleHeight = true });
			layout1.Rows.Add(new TableRow(cells[3], cells[4], cells[5]) { ScaleHeight = true });

			Content = layout1;
		}
		private List<TableCell> GetVariationCells()
		{
			return GetVariationLayouts().Select(i => new TableCell(i, true)).ToList();
		}
		private List<TableLayout> GetVariationLayouts()
		{
			List<TableLayout> list = new List<TableLayout>();

			if (skia.Checked ?? false)
			{
				list.Add(new TableLayout(
					new Label { Text = "Defaults" },
					new Skia.RainbowLoading()));

				list.Add(new TableLayout(
					new Label { Text = "Progress Duration = 4000 millis" },
					new Skia.RainbowLoading { ProgressDuration = TimeSpan.FromMilliseconds(4000) }));

				list.Add(new TableLayout(
					new Label { Text = "Progress Duration = 1000 millis" },
					new Skia.RainbowLoading { ProgressDuration = TimeSpan.FromMilliseconds(1000) }));

				list.Add(new TableLayout(
					new Label { Text = "Custom Shadow Color" },
					new Skia.RainbowLoading { ShadowColor = new Color(GetRandomPastelColor(), 0.9f) }));

				list.Add(new TableLayout(
					new Label { Text = "Custom Background Color" },
					new Skia.RainbowLoading { BackgroundColor = new Color(GetRandomPastelColor(), 0.5f) }));

				list.Add(new TableLayout(
					new Label { Text = "Custom Control Color" },
					new Skia.RainbowLoading { ControlColor = GetRandomPastelColor() }));
			}
			else
			{
				list.Add(new TableLayout(
					new Label { Text = "Defaults" },
					new RainbowLoading()));

				list.Add(new TableLayout(
					new Label { Text = "Progress Duration = 4000 millis" },
					new RainbowLoading { ProgressDuration = TimeSpan.FromMilliseconds(4000) }));

				list.Add(new TableLayout(
					new Label { Text = "Progress Duration = 1000 millis" },
					new RainbowLoading { ProgressDuration = TimeSpan.FromMilliseconds(1000) }));

				list.Add(new TableLayout(
					new Label { Text = "Pastel Colors" },
					new RainbowLoading { ProgressColors = GetListOfRandomPastelColors(28) }));

				list.Add(new TableLayout(
					new Label { Text = "Custom Background Color" },
					new RainbowLoading { BackgroundColor = new Color(GetRandomPastelColor(), 0.5f) }));

				list.Add(new TableLayout(
					new Label { Text = "Custom Control Color" },
					new RainbowLoading { ControlColor = GetRandomPastelColor() }));
			}

			return list;
		}

		private List<Color> GetListOfRandomPastelColors(int count)
		{
			return Enumerable.Range(0, count).Select(i => GetRandomPastelColor()).ToList();
		}

		private Color GetRandomPastelColor()
		{
			return Color.FromArgb(NextByte(), NextByte(), NextByte(), (byte)(0.8*255));
		}
		private byte NextByte()
		{
			return (byte)(rand.NextDouble() * 255);
		}
	}
}

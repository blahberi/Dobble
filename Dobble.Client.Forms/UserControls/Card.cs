using System;
using System.Drawing;
using System.Windows.Forms;
using Dobble.Shared;

namespace Dobble.Client.Forms.UserControls
{
	public partial class Card : UserControl
	{
		private Symbol[] symbols;
		private int symbolCount;

		public Card()
		{
			this.InitializeComponent();

			this.symbolCount = GameConfig.ORDER + 1;
			this.symbols = new Symbol[this.symbolCount];
			this.CreatePictureBoxes();
			this.Selected = -1;
			this.CardBackColor = GruvboxTheme.CreamText;
		}

		public event Action OnSymbolSelected;

		private Color CardBackColor { get; set; }

		public int Selected { get; private set; }
		public bool Disabled { get; set; }

		public void LoadSymbols(int[] symbols)
		{
			for (int i = 0; i < this.symbols.Length; i++)
			{
				this.symbols[i].LoadSymbol(symbols[i]);
			}
		}

		public void MarkAsWrong(int index)
		{
			this.MarkAs(index, symbol => symbol.MarkSymbolAsWrong(), true);
		}

		public void MarkAsCorrect(int index)
		{
			this.MarkAs(index, symbol => symbol.MarkSymbolAsCorrect(), true);
		}

		public void Unmark(int index)
		{
			this.MarkAs(index, symbol => symbol.ResetCircleBackColor(), false);
		}
		public void UnmarkAll()
		{
			for (int i = 0; i < this.symbols.Length; i++)
			{
				this.Unmark(i);
			}
		}

		public void ResetSelection()
		{
			if (this.Selected == -1)
			{
				return;
			}
			this.Unmark(this.Selected);
			this.Selected = -1;
		}

		public void IsolateSymbol(int index)
		{
			for (int i = 0; i < this.symbols.Length; i++)
			{
				if (i != index)
				{
					this.symbols[i].Visible = false;
				}
			}
		}

		public void ResetIsolation()
		{
			for (int i = 0; i < this.symbols.Length; i++)
			{
				this.symbols[i].Visible = true;
			}
		}

		private void MarkAs(int index, Action<Symbol> markAction, bool disable)
		{
			markAction(this.symbols[index]);
			this.Disabled = disable;
		}

		private void CreatePictureBoxes()
		{
			double phi = (1 + Math.Sqrt(5)) / 2;
			double radius = this.Width / Math.E;
			int size = (int)(radius / phi);
			double angle = 2 * Math.PI / (this.symbolCount - 1);
			Point origin = new Point(this.Width / 2, this.Height / 2);

			for (int i = 0; i < this.symbols.Length; i++)
			{
				this.symbols[i] = this.CreateSymbol(i, size, origin, radius, angle);
			}

			this.symbols[GameConfig.ORDER] = this.CreateCenterSymbol(size);

			for (int i = 0; i < this.symbols.Length; i++)
			{
				this.symbols[i].Tag = i;
			}
			this.AddEvents();
		}

		private Symbol CreateSymbol(int index, int size, Point origin, double radius, double angle)
		{
			Symbol symbol = new Symbol();
			int x = (int)(origin.X + radius * Math.Cos(index * angle) - size / 2);
			int y = (int)(origin.Y + radius * Math.Sin(index * angle) - size / 2);
			symbol.Location = new Point(x, y);
			symbol.Name = $"symbol{index}";
			symbol.Size = new Size(size, size);
			symbol.SizeMode = PictureBoxSizeMode.StretchImage;
			symbol.TabStop = false;
			this.Controls.Add(symbol);

			symbol.CreateBackgrounds();
			return symbol;
		}

		private Symbol CreateCenterSymbol(int size)
		{
			Symbol centerSymbol = new Symbol();
			centerSymbol.Location = new Point(this.Width / 2 - size / 2, this.Height / 2 - size / 2);
			centerSymbol.Name = "symbol7";
			centerSymbol.Size = new Size(size, size);
			centerSymbol.SizeMode = PictureBoxSizeMode.StretchImage;
			centerSymbol.TabStop = false;
			this.Controls.Add(centerSymbol);

			centerSymbol.CreateBackgrounds();
			return centerSymbol;
		}

		private void AddEvents()
		{
			foreach (Symbol pictureBox in this.symbols)
			{
				pictureBox.MouseClick += this.PictureBoxMouseClick;
			}
		}

		private void PictureBoxMouseClick(object sender, MouseEventArgs e)
		{
			if (this.Disabled)
			{
				return;
			}
			Symbol pictureBox = (Symbol)sender;
			int id = (int)pictureBox.Tag;
			this.togglePictureBox(id);
		}

		private void togglePictureBox(int id)
		{
			Symbol symbol = this.symbols[id];
			if (this.Selected != -1)
			{
				this.symbols[this.Selected].ResetCircleBackColor();
			}
			symbol.SelectSymbol();
			this.Selected = id;
			this.OnSymbolSelected?.Invoke();
		}

		private void Card_Load(object sender, EventArgs e)
		{
			Bitmap bmp = new Bitmap(this.Width, this.Height);
			using (Graphics graphics = Graphics.FromImage(bmp))
			{
				using (SolidBrush brush = new SolidBrush(this.CardBackColor))
				{
					// Calculate the circle dimensions to be slightly larger than the PictureBox
					int diameter = Math.Max(this.Width, this.Height);
					int x = 0;
					int y = 0;

					// Draw the filled ellipse on the parent control
					graphics.FillEllipse(brush, new Rectangle(x, y, diameter, diameter));
				}
			}
			this.BackgroundImage = bmp;
		}
	}

	internal class Symbol : PictureBox
	{
		private Bitmap emptyBackgroundImage;
		private Bitmap selectedBackgroundImage;
		private Bitmap wrongBackgroundImage;
		private Bitmap correctBackgroundImage;

		public Symbol()
		{
			this.BackColor = Color.Transparent;
			this.SizeMode = PictureBoxSizeMode.StretchImage;
			this.TabStop = false;
		}

		public void CreateBackgrounds()
		{
			// create empty background image
			this.emptyBackgroundImage = new Bitmap(this.Width, this.Height);

			// create selected background image
			this.selectedBackgroundImage = new Bitmap(this.Width, this.Height);
			using (Graphics graphics = Graphics.FromImage(this.selectedBackgroundImage))
			{
				using (SolidBrush brush = new SolidBrush(GruvboxTheme.Yellow))
				{
					// Calculate the circle dimensions to fit within the PictureBox
					int diameter = Math.Min(this.Width, this.Height);

					// Draw the filled ellipse centered within the control
					graphics.FillEllipse(brush, 0, 0, diameter, diameter);
				}
			}

			// create wrong background image
			this.wrongBackgroundImage = new Bitmap(this.Width, this.Height);
			using (Graphics graphics = Graphics.FromImage(this.wrongBackgroundImage))
			{
				using (SolidBrush brush = new SolidBrush(GruvboxTheme.Red))
				{
					// Calculate the circle dimensions to fit within the PictureBox
					int diameter = Math.Min(this.Width, this.Height);

					// Draw the filled ellipse centered within the control
					graphics.FillEllipse(brush, 0, 0, diameter, diameter);
				}
			}

			// create correct background image
			this.correctBackgroundImage = new Bitmap(this.Width, this.Height);
			using (Graphics graphics = Graphics.FromImage(this.correctBackgroundImage))
			{
				using (SolidBrush brush = new SolidBrush(GruvboxTheme.DarkGreen))
				{
					// Calculate the circle dimensions to fit within the PictureBox
					int diameter = Math.Min(this.Width, this.Height);

					// Draw the filled ellipse centered within the control
					graphics.FillEllipse(brush, 0, 0, diameter, diameter);
				}
			}
		}

		public void LoadSymbol(int symbolId)
		{
			// load symbol from resources
			this.Image = Properties.Resources.ResourceManager.GetObject($"_{symbolId}") as Image;
		}

		public void ResetCircleBackColor()
		{
			this.BackgroundImage = this.emptyBackgroundImage;
		}

		public void SelectSymbol()
		{
			this.BackgroundImage = this.selectedBackgroundImage;
		}

		public void MarkSymbolAsWrong()
		{
			this.BackgroundImage = this.wrongBackgroundImage;
		}

		public void MarkSymbolAsCorrect()
		{
			this.BackgroundImage = this.correctBackgroundImage;
		}
	}
}

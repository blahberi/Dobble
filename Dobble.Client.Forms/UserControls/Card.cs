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
			this.BackColor = Color.Transparent;
		}

		public event Action OnSymbolSelected;

		private Color CardBackColor { get; set; }

		public int Selected { get; private set; }
		public bool Disabled { get; set; }

		public void PaintCard()
		{
			this.Parent.Paint += this.Card_Paint;
		}

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

			symbol.InitSelectionDrawer();
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

			centerSymbol.InitSelectionDrawer();
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

		private void Card_Paint(object sender, PaintEventArgs e)
		{
			using (SolidBrush brush = new SolidBrush(this.CardBackColor))
			{
				// Calculate the circle dimensions to be slightly larger than the PictureBox
				int diameter = Math.Max(this.Width, this.Height);
				int x = this.Location.X;
				int y = this.Location.Y;

				// Draw the filled ellipse on the parent control
				e.Graphics.FillEllipse(brush, new Rectangle(x, y, diameter, diameter));
			}
		}
	}

	internal class Symbol : PictureBox
	{
		private Color backgroundCircleColor;

		public Symbol()
		{
			this.BackColor = Color.Transparent;
			this.SizeMode = PictureBoxSizeMode.StretchImage;
			this.TabStop = false;

			this.backgroundCircleColor = Color.Transparent;
		}

		public void InitSelectionDrawer()
		{
			this.Parent.Paint += (sender, e) =>
			{
				using (SolidBrush brush = new SolidBrush(this.backgroundCircleColor))
				{
					// Calculate the circle dimensions to be slightly larger than the PictureBox
					int padding = 10;  // You can adjust the padding size
					int diameter = Math.Max(this.Width, this.Height) + padding;
					int x = this.Location.X - (padding / 2);
					int y = this.Location.Y - (padding / 2);

					// Draw the filled ellipse on the parent control
					e.Graphics.FillEllipse(brush, new Rectangle(x, y, diameter, diameter));
				}
			};
		}

		public void LoadSymbol(int symbolId)
		{
			// load symbol from resources
			this.Image = Properties.Resources.ResourceManager.GetObject($"_{symbolId}") as Image;
		}

		public void ResetCircleBackColor()
		{
			this.backgroundCircleColor = Color.Transparent;
			if (this.Parent == null) { return; };
			this.Parent.Refresh();
		}

		public void SelectSymbol()
		{
			this.backgroundCircleColor = GruvboxTheme.PrimaryColor;
			if (this.Parent == null) { return; };
			this.Parent.Refresh();
		}

		public void MarkSymbolAsWrong()
		{
			this.backgroundCircleColor = GruvboxTheme.Red;
			if (this.Parent == null) { return; };
			this.Parent.Refresh();
		}

		public void MarkSymbolAsCorrect()
		{
			this.backgroundCircleColor = GruvboxTheme.DarkGreen;
			if (this.Parent == null) { return; };
			this.Parent.Refresh();
		}
	}
}

using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dobble.Client.Forms
{
	public static class GruvboxTheme
	{
		public static Color Orange { get; } = ColorFromHex("fe8019");
		public static Color DarkOrange { get; } = ColorFromHex("d65d0e");
		public static Color Green { get; } = ColorFromHex("b8bb26");
		public static Color DarkGreen { get; } = ColorFromHex("98971a");
		public static Color Red { get; } = ColorFromHex("fb4934");
		public static Color DarkRed { get; } = ColorFromHex("cc241d");
		public static Color Blue { get; } = ColorFromHex("83a598");
		public static Color DarkBlue { get; } = ColorFromHex("458588");
		public static Color Purple { get; } = ColorFromHex("d3869b");
		public static Color DarkPurple { get; } = ColorFromHex("b16286");
		public static Color Aqua { get; } = ColorFromHex("8ec07c");
		public static Color DarkAqua { get; } = ColorFromHex("689d6a");
		public static Color Yellow { get; } = ColorFromHex("fabd2f");
		public static Color DarkYellow { get; } = ColorFromHex("d79921");


		public static Color Background { get; } = ColorFromHex("282828");
		public static Color DarkBackground { get; } = ColorFromHex("1d2021");
		public static Color WhiteText { get; } = ColorFromHex("fbf1c7");
		public static Color CreamText { get; } = ColorFromHex("ebdbb2");
		public static Color PrimaryColor { get; } = Purple;
		public static Color SecondaryColor { get; } = Blue;

		private static Color ColorFromHex(string hex)
		{
			int value = int.Parse("ff" + hex, System.Globalization.NumberStyles.HexNumber);
			return Color.FromArgb(value);
		}
	}

	public class GruvboxLabel : Label
	{
		public GruvboxLabel()
		{
			this.ForeColor = GruvboxTheme.WhiteText;
		}
	}

	public class GruvboxButton : Button
	{
		public GruvboxButton()
		{
			this.BackColor = GruvboxTheme.PrimaryColor;
			this.ForeColor = GruvboxTheme.Background;
			this.FlatStyle = FlatStyle.Flat;
			this.FlatAppearance.BorderSize = 0;
			this.FlatAppearance.MouseOverBackColor = GruvboxTheme.CreamText;
			this.FlatAppearance.MouseDownBackColor = GruvboxTheme.WhiteText;
		}
	}

	public class GruvboxTextBox : TextBox
	{
		public GruvboxTextBox()
		{
			this.BackColor = GruvboxTheme.CreamText;
			this.ForeColor = GruvboxTheme.Background;
			this.BorderStyle = BorderStyle.FixedSingle;
		}
	}

	public class GruvboxLink: LinkLabel
	{
		public GruvboxLink()
		{
			this.LinkColor = GruvboxTheme.SecondaryColor;
			this.ActiveLinkColor = GruvboxTheme.PrimaryColor;
		}
	}

	public class GruvboxComboBox : ComboBox
	{
		public GruvboxComboBox()
		{
			this.BackColor = GruvboxTheme.CreamText;
			this.ForeColor = GruvboxTheme.Background;
			this.FlatStyle = FlatStyle.Flat;
		}
	}

	public class GruvboxListBox : ListBox
	{
		public GruvboxListBox()
		{
			this.BackColor = GruvboxTheme.CreamText;
			this.ForeColor = GruvboxTheme.Background;
			this.BorderStyle = BorderStyle.FixedSingle;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Mahou
{
	public partial class MahouUI
	{
		const int UiMargin = 10;
		const int UiGap = 8;
		const int UiCompactGap = 6;

		TableLayoutPanel responsiveRoot;
		FlowLayoutPanel commandBar;

		void InitializeResponsiveLayout()
		{
			SuspendLayout();
			AutoScaleMode = AutoScaleMode.Dpi;
			Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
			FormBorderStyle = FormBorderStyle.Sizable;
			MaximizeBox = true;
			MinimizeBox = true;
			MinimumSize = ScaleSize(920, 700);
			Padding = Padding.Empty;

			ConfigureTabControl();
			ConfigureCommandButtons();
			BuildFormShell();
			ModernizeTabPages();

			ResumeLayout(true);
		}

		void ConfigureTabControl()
		{
			tabs.Dock = DockStyle.Fill;
			tabs.Margin = Padding.Empty;
			tabs.Padding = new Point(ScaleLogical(12), ScaleLogical(6));
			tabs.Multiline = true;
			tabs.SizeMode = TabSizeMode.Normal;
		}

		void ConfigureCommandButtons()
		{
			foreach (var button in new[] { btn_OK, btn_Apply, btn_Cancel })
			{
				button.AutoSize = true;
				button.AutoSizeMode = AutoSizeMode.GrowAndShrink;
				button.MinimumSize = ScaleSize(120, 34);
				button.Margin = new Padding(UiCompactGap, 0, 0, 0);
				button.Padding = new Padding(ScaleLogical(10), ScaleLogical(4), ScaleLogical(10), ScaleLogical(4));
				button.Anchor = AnchorStyles.Right;
			}
		}

		void BuildFormShell()
		{
			responsiveRoot = new TableLayoutPanel();
			responsiveRoot.Name = "responsiveRoot";
			responsiveRoot.ColumnCount = 1;
			responsiveRoot.RowCount = 2;
			responsiveRoot.Dock = DockStyle.Fill;
			responsiveRoot.Padding = new Padding(ScaleLogical(UiMargin));
			responsiveRoot.Margin = Padding.Empty;
			responsiveRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			responsiveRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			responsiveRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));

			commandBar = new FlowLayoutPanel();
			commandBar.Name = "commandBar";
			commandBar.AutoSize = true;
			commandBar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			commandBar.FlowDirection = FlowDirection.RightToLeft;
			commandBar.WrapContents = false;
			commandBar.Dock = DockStyle.Fill;
			commandBar.Margin = new Padding(0, ScaleLogical(UiMargin), 0, 0);
			commandBar.Padding = Padding.Empty;

			commandBar.Controls.Add(btn_Cancel);
			commandBar.Controls.Add(btn_Apply);
			commandBar.Controls.Add(btn_OK);

			foreach (var control in new Control[] { tabs, btn_OK, btn_Apply, btn_Cancel })
			{
				if (Controls.Contains(control))
				{
					Controls.Remove(control);
				}
			}

			responsiveRoot.Controls.Add(tabs, 0, 0);
			responsiveRoot.Controls.Add(commandBar, 0, 1);
			Controls.Add(responsiveRoot);
		}

		void ModernizeTabPages()
		{
			foreach (TabPage page in tabs.TabPages)
			{
				ModernizeTabPage(page);
			}
		}

		void ModernizeTabPage(TabPage page)
		{
			var children = SnapshotChildren(page);
			page.SuspendLayout();
			page.Controls.Clear();
			page.Padding = Padding.Empty;

			if (page.Name == "tab_hotkeys")
			{
				BuildHotkeysTabLayout(page, children);
				page.ResumeLayout(true);
				return;
			}

			var scrollHost = CreateScrollHost("scroll_" + page.Name);
			var content = CreateMainLayoutTable("content_" + page.Name, true);
			scrollHost.Controls.Add(content);
			scrollHost.Resize += (_, __) => SyncContentWidth(scrollHost, content);

			page.Controls.Add(scrollHost);
			PopulateLayoutTable(content, children, true, page.Name);
			if (content.RowStyles.Cast<RowStyle>().Any(r => r.SizeType == SizeType.Percent))
			{
				content.AutoSize = false;
				content.Dock = DockStyle.Fill;
			}
			SyncContentWidth(scrollHost, content);

			page.ResumeLayout(true);
		}

		void BuildHotkeysTabLayout(TabPage page, List<ControlSnapshot> children)
		{
			var listSnap = children.FirstOrDefault(s => s.Control != null && s.Control.Name == "lsb_Hotkeys");
			var grpSnap = children.FirstOrDefault(s => s.Control != null && s.Control.Name == "grb_Hotkey");
			if (listSnap.Control == null || grpSnap.Control == null) return;

			var table = new TableLayoutPanel();
			table.Name = "hotkeysLayout";
			table.Dock = DockStyle.Fill;
			table.ColumnCount = 2;
			table.RowCount = 1;
			table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			table.Padding = new Padding(ScaleLogical(UiMargin));
			table.Margin = Padding.Empty;

			var list = listSnap.Control;
			list.Dock = DockStyle.Fill;
			list.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			list.Margin = new Padding(0, 0, ScaleLogical(UiGap), 0);
			list.MinimumSize = new Size(ScaleLogical(200), ScaleLogical(120));
			table.Controls.Add(list, 0, 0);

			ModernizeGroupBox(grpSnap.Control as GroupBox);
			var grp = grpSnap.Control;
			grp.Dock = DockStyle.Fill;
			grp.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			grp.Margin = Padding.Empty;
			table.Controls.Add(grp, 1, 0);

			page.Controls.Add(table);
		}

		void ModernizeGroupBox(GroupBox groupBox)
		{
			if (groupBox.Controls.Count == 1 && groupBox.Controls[0] is TableLayoutPanel)
			{
				return;
			}

			var children = SnapshotChildren(groupBox);
			groupBox.SuspendLayout();
			groupBox.Controls.Clear();
			groupBox.AutoSize = true;
			groupBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			groupBox.Dock = DockStyle.Top;
			groupBox.Margin = new Padding(0, 0, 0, ScaleLogical(UiGap));
			groupBox.Padding = new Padding(ScaleLogical(UiMargin), ScaleLogical(24), ScaleLogical(UiMargin), ScaleLogical(UiMargin));

			var content = CreateMainLayoutTable("content_" + groupBox.Name, false);
			content.Dock = DockStyle.Fill;
			content.AutoSize = true;
			content.Margin = Padding.Empty;
			content.Padding = Padding.Empty;

			groupBox.Controls.Add(content);
			PopulateLayoutTable(content, children, false, null);
			groupBox.ResumeLayout(true);
		}

		List<ControlSnapshot> SnapshotChildren(Control parent)
		{
			return parent.Controls
				.Cast<Control>()
				.Where(control => control != null && !string.Equals(control.Name, "responsiveRoot", StringComparison.Ordinal))
				.Select(control => new ControlSnapshot(control))
				.OrderBy(snapshot => snapshot.Top)
				.ThenBy(snapshot => snapshot.Left)
				.ToList();
		}

		void PopulateLayoutTable(TableLayoutPanel target, List<ControlSnapshot> snapshots, bool topLevel, string pageName)
		{
			var rows = ClusterRows(snapshots);
			var rowIndex = 0;
			for (var i = 0; i < rows.Count; i++)
			{
				var currentRow = rows[i];
				if (IsCheckboxRow(currentRow))
				{
					var checkRows = new List<List<ControlSnapshot>>();
					checkRows.Add(currentRow);
					while (i + 1 < rows.Count && IsCheckboxRow(rows[i + 1]))
					{
						checkRows.Add(rows[++i]);
					}

					AddRow(target, CreateCheckboxGroup(checkRows), rowIndex++, false);
					continue;
				}

				var isFillRow = currentRow.Count == 1 && IsFillControl(currentRow[0].Control, pageName);
				var rowControl = isFillRow ? PrepareFillControl(currentRow[0].Control) : CreateRowControl(currentRow, topLevel);
				AddRow(target, rowControl, rowIndex++, isFillRow);
			}
		}

		bool IsFillControl(Control c, string pageName)
		{
			if (c == null || string.IsNullOrEmpty(pageName)) return false;
			var n = c.Name ?? "";
			switch (pageName)
			{
				case "tab_about": return n == "txt_Help";
				case "tab_snippets": return n == "txt_Snippets" || n == "pan_NoConvertRules";
				case "tab_excluded": return n == "txt_ExcludedPrograms";
				case "tab_autoswitch": return n == "txt_AutoSwitchDictionary";
				case "tab_updates": return n == "grb_MahouReleaseTitle";
				default: return false;
			}
		}

		Control PrepareFillControl(Control c)
		{
			if (c is GroupBox gb)
			{
				ModernizeGroupBox(gb);
				gb.AutoSize = false;
				gb.Dock = DockStyle.Fill;
			}
			else
			{
				c.Dock = DockStyle.Fill;
				c.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			}
			c.Margin = new Padding(0, ScaleLogical(UiCompactGap), 0, 0);
			if (c is TextBox tb && (tb.Multiline || tb.ScrollBars != ScrollBars.None))
				tb.ScrollBars = ScrollBars.Vertical;
			return c;
		}

		void AddRow(TableLayoutPanel target, Control rowControl, int rowIndex, bool fill)
		{
			target.RowCount = rowIndex + 1;
			target.RowStyles.Add(new RowStyle(fill ? SizeType.Percent : SizeType.AutoSize, fill ? 100F : 0F));
			target.Controls.Add(rowControl, 0, rowIndex);
		}

		List<List<ControlSnapshot>> ClusterRows(List<ControlSnapshot> snapshots)
		{
			var rows = new List<List<ControlSnapshot>>();
			var threshold = ScaleLogical(UiGap + 2);
			foreach (var snapshot in snapshots)
			{
				if (IsDecorativeSpacer(snapshot.Control))
				{
					snapshot.Control.Visible = false;
					continue;
				}

				if (rows.Count == 0)
				{
					rows.Add(new List<ControlSnapshot> { snapshot });
					continue;
				}

				var referenceTop = rows[rows.Count - 1][0].Top;
				if (Math.Abs(snapshot.Top - referenceTop) <= threshold)
				{
					rows[rows.Count - 1].Add(snapshot);
				}
				else
				{
					rows.Add(new List<ControlSnapshot> { snapshot });
				}
			}

			foreach (var row in rows)
			{
				row.Sort((left, right) => left.Left.CompareTo(right.Left));
			}

			return rows;
		}

		Control CreateCheckboxGroup(List<List<ControlSnapshot>> rows)
		{
			var group = new FlowLayoutPanel();
			group.AutoSize = true;
			group.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			group.Dock = DockStyle.Top;
			group.FlowDirection = FlowDirection.TopDown;
			group.WrapContents = false;
			group.Margin = new Padding(0, 0, 0, ScaleLogical(UiGap));
			group.Padding = Padding.Empty;

			foreach (var row in rows)
			{
				if (row.Count == 1)
				{
					var control = PrepareControlForLayout(row[0].Control, false);
					control.Margin = new Padding(0, 0, 0, ScaleLogical(UiCompactGap));
					group.Controls.Add(control);
					continue;
				}

				var rowFlow = CreateFlowRow("checkRow");
				foreach (var snapshot in row)
				{
					var control = PrepareControlForLayout(snapshot.Control, false);
					control.Margin = new Padding(0, 0, ScaleLogical(UiGap), ScaleLogical(UiCompactGap));
					rowFlow.Controls.Add(control);
				}

				group.Controls.Add(rowFlow);
			}

			return group;
		}

		Control CreateRowControl(List<ControlSnapshot> row, bool topLevel)
		{
			if (row.Count == 1)
			{
				return PrepareControlForLayout(row[0].Control, topLevel);
			}

			if (row.Count == 2 && IsInputControl(row[1].Control))
			{
				var table = new TableLayoutPanel();
				table.Name = "labelInputRow";
				table.AutoSize = true;
				table.AutoSizeMode = AutoSizeMode.GrowAndShrink;
				table.Dock = DockStyle.Top;
				table.ColumnCount = 2;
				table.RowCount = 1;
				table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
				table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
				table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
				table.Margin = new Padding(0, 0, 0, ScaleLogical(UiGap));
				table.Padding = Padding.Empty;

				var label = PrepareControlForLayout(row[0].Control, false);
				label.Margin = new Padding(0, 0, ScaleLogical(UiGap), ScaleLogical(UiCompactGap));
				var input = PrepareControlForLayout(row[1].Control, false);
				input.Margin = new Padding(0, 0, 0, ScaleLogical(UiCompactGap));
				input.Dock = DockStyle.Fill;
				input.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

				table.Controls.Add(label, 0, 0);
				table.Controls.Add(input, 1, 0);
				return table;
			}

			var flow = CreateFlowRow("row");
			foreach (var snapshot in row)
			{
				var control = PrepareControlForLayout(snapshot.Control, false);
				control.Margin = new Padding(0, 0, ScaleLogical(UiGap), ScaleLogical(UiCompactGap));
				flow.Controls.Add(control);
			}

			return flow;
		}

		bool IsInputControl(Control control)
		{
			return control is ComboBox || control is TextBox || control is NumericUpDown || control is ListBox;
		}

		FlowLayoutPanel CreateFlowRow(string name)
		{
			var flow = new FlowLayoutPanel();
			flow.Name = name;
			flow.AutoSize = true;
			flow.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			flow.Dock = DockStyle.Top;
			flow.FlowDirection = FlowDirection.LeftToRight;
			flow.WrapContents = true;
			flow.Margin = new Padding(0, 0, 0, ScaleLogical(UiGap));
			flow.Padding = Padding.Empty;
			return flow;
		}

		Panel CreateScrollHost(string name)
		{
			var scrollHost = new Panel();
			scrollHost.Name = name;
			scrollHost.Dock = DockStyle.Fill;
			scrollHost.AutoScroll = true;
			scrollHost.Margin = Padding.Empty;
			scrollHost.Padding = Padding.Empty;
			return scrollHost;
		}

		TableLayoutPanel CreateMainLayoutTable(string name, bool topLevel)
		{
			var table = new TableLayoutPanel();
			table.Name = name;
			table.AutoSize = true;
			table.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			table.ColumnCount = 1;
			table.RowCount = 0;
			table.Dock = DockStyle.Top;
			table.Margin = Padding.Empty;
			table.Padding = new Padding(ScaleLogical(topLevel ? UiMargin : UiCompactGap));
			table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			return table;
		}

		void SyncContentWidth(ScrollableControl scrollHost, Control content)
		{
			var width = scrollHost.ClientSize.Width - ScaleLogical(2);
			if (scrollHost.VerticalScroll.Visible)
			{
				width -= SystemInformation.VerticalScrollBarWidth;
			}

			content.Width = Math.Max(width, ScaleLogical(360));
		}

		Control PrepareControlForLayout(Control control, bool topLevel)
		{
			if (control is GroupBox groupBox)
			{
				ModernizeGroupBox(groupBox);
				groupBox.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
				groupBox.MinimumSize = new Size(ScaleLogical(280), 0);
				return groupBox;
			}

			if (control is Panel panel && IsDynamicPanel(panel))
			{
				panel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
				panel.Margin = new Padding(0, 0, 0, ScaleLogical(UiGap));
				if (panel.Height < ScaleLogical(140))
				{
					panel.Height = ScaleLogical(180);
				}
				return panel;
			}

			control.Margin = new Padding(0, 0, 0, ScaleLogical(UiCompactGap));
			control.Padding = control.Padding;
			control.Anchor = AnchorStyles.Left | AnchorStyles.Top;

			if (control is Label label)
			{
				label.AutoSize = true;
			}
			else if (control is LinkLabel linkLabel)
			{
				linkLabel.AutoSize = true;
			}
			else if (control is CheckBox checkBox)
			{
				checkBox.AutoSize = true;
			}
			else if (control is RadioButton radioButton)
			{
				radioButton.AutoSize = true;
			}
			else if (control is Button button)
			{
				button.AutoSize = true;
				button.AutoSizeMode = AutoSizeMode.GrowAndShrink;
				button.MinimumSize = ScaleSize(96, 30);
				button.Padding = new Padding(ScaleLogical(8), ScaleLogical(3), ScaleLogical(8), ScaleLogical(3));
			}
			else if (control is TextBox textBox)
			{
				if (textBox.Multiline || textBox.ScrollBars != ScrollBars.None)
				{
					textBox.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
					if (textBox.Height < ScaleLogical(120))
					{
						textBox.Height = ScaleLogical(180);
					}
				}
				else
				{
					textBox.MinimumSize = new Size(ScaleLogical(200), 0);
				}
			}
			else if (control is ListBox listBox)
			{
				listBox.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
				listBox.MinimumSize = new Size(ScaleLogical(320), listBox.MinimumSize.Height);
				if (listBox.Height < ScaleLogical(120))
				{
					listBox.Height = ScaleLogical(180);
				}
			}
			else if (control is ComboBox comboBox)
			{
				comboBox.MinimumSize = new Size(ScaleLogical(320), 0);
				comboBox.DropDownWidth = Math.Max(Math.Max(comboBox.Width, comboBox.MinimumSize.Width), ScaleLogical(320));
			}
			else if (control is NumericUpDown numericUpDown)
			{
				numericUpDown.MinimumSize = new Size(ScaleLogical(80), 0);
			}

			if (topLevel && IsWideStandaloneControl(control))
			{
				control.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
				control.MinimumSize = new Size(ScaleLogical(360), control.MinimumSize.Height);
			}

			return control;
		}

		bool IsCheckboxRow(List<ControlSnapshot> row)
		{
			return row.All(snapshot => IsCheckLike(snapshot.Control));
		}

		bool IsCheckLike(Control control)
		{
			return control is CheckBox || control is RadioButton;
		}

		bool IsDynamicPanel(Panel panel)
		{
			return panel.Name == "pan_KeySets" ||
			       panel.Name == "pan_TrSets" ||
			       panel.Name == "pan_NoConvertRules";
		}

		bool IsDecorativeSpacer(Control control)
		{
			if (!(control is Label))
			{
				return false;
			}

			var name = control.Name ?? string.Empty;
			return name.IndexOf("scrollpastcontent", StringComparison.OrdinalIgnoreCase) >= 0;
		}

		bool IsWideStandaloneControl(Control control)
		{
			return control is GroupBox ||
			       control is Panel ||
			       control is TextBox && ((TextBox)control).Multiline ||
			       control is ListBox;
		}

		int ScaleLogical(int value)
		{
			return (int)Math.Round(value * DeviceDpi / 96F);
		}

		Size ScaleSize(int width, int height)
		{
			return new Size(ScaleLogical(width), ScaleLogical(height));
		}

		readonly struct ControlSnapshot
		{
			public ControlSnapshot(Control control)
			{
				Control = control;
				Top = control.Top;
				Left = control.Left;
			}

			public Control Control { get; }
			public int Top { get; }
			public int Left { get; }
		}
	}
}

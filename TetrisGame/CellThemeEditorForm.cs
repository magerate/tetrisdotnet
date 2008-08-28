using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using TetrisGame.Drawing;
using System.IO;

namespace TetrisGame
{
    public partial class CellThemeEditorForm : Form
    {
        public CellThemeEditorForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            bindingSource1.DataSource = CellTheme.GetAllThemes();
            themeList.DataSource = bindingSource1;
            cellThemeImageList1.ContextMenuStrip = contextMenuStrip1;
            base.OnLoad(e);
        }

        private void themeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            cellThemeImageList1.CellTheme = (CellTheme)themeList.SelectedItem;
        }


        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in cellThemeImageList1.SelectedItems)
            {
                cellThemeImageList1.CellTheme.Remove((Stream)lvi.Tag);
            }
            cellThemeImageList1.RefreshTheme();
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            contextMenuStrip1.Items[1].Enabled = cellThemeImageList1.SelectedItems.Count > 0;
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                foreach (string fileName in openFileDialog1.FileNames)
                {
                    cellThemeImageList1.CellTheme.Add(File.OpenRead(fileName));
                }
            }
            cellThemeImageList1.RefreshTheme();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            CellTheme theme = new CellTheme(themeList.Items.Count);
            bindingSource1.List.Add(theme);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            cellThemeImageList1.CellTheme.Save();
        }
    }
}

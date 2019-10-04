using System;
using System.Windows.Forms;

namespace Сerts
{
    class MenuStripHelper
    {
        static ToolStripMenuItem diskManager = new ToolStripMenuItem();

        public static ToolStripItem AddMenuDiskManager()
        {
            diskManager.DropDownItems.Add(AddMountDiskMenu());
            diskManager.DropDownItems.Add(UnmountAllDiskMenu());
            diskManager.DropDownItems.Add(AddSeparator());
            diskManager.Text = "Управление дисками";
            diskManager.MouseEnter += DiskManager_MouseEnter;
            return diskManager;
        }

        public static ToolStripItem [] GetMountedDisks()
        {
            var md = ImDiskHelper.MountedDisks();

            ToolStripItem[] toolStripMenuItemColl = new ToolStripItem[md.Count];

            for (int i = 0; i < md.Count; i++)
            {
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
                toolStripMenuItem.Click += ToolStripMenuItem_Click;
                toolStripMenuItem.Text = md[i];
                toolStripMenuItemColl[i] = toolStripMenuItem;
            }

            return toolStripMenuItemColl;
        }

        private static void DiskManager_MouseEnter(object sender, EventArgs e)
        {
            diskManager.DropDownItems.Clear();
            AddMenuDiskManager();
            diskManager.DropDownItems.AddRange(GetMountedDisks());
        }

        private static ToolStripItem AddMountDiskMenu()
        {
            ToolStripMenuItem mountDiskMenu = new ToolStripMenuItem();
            mountDiskMenu.Text = "Смонтировать образ";
            mountDiskMenu.Click += MountDiskMenu_Click;
            return mountDiskMenu;
        }

        private static ToolStripSeparator AddSeparator()
        {
            ToolStripSeparator separator = new ToolStripSeparator();
            return separator;
        }

        private static ToolStripItem UnmountAllDiskMenu()
        {
            ToolStripMenuItem unmountDiskMenu = new ToolStripMenuItem();
            unmountDiskMenu.Text = "Размонтировать все диски";
            unmountDiskMenu.Click += UnmountDiskMenu_Click;
            return unmountDiskMenu;
        }

        private static void UnmountDiskMenu_Click(object sender, EventArgs e)
        {
            if (ImDiskHelper.ForceRemoveDevice())
                MessageBox.Show("Все диски размонтированны", "", MessageBoxButtons.OK);
        }

        private static void MountDiskMenu_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = @"C:\";
            fileDialog.Filter = "VFD files (*.VFD)|*.VFD| All files(*.*) | *.*";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                ImDiskHelper.MountImage(fileDialog.FileName);
            }
        }

        private static void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}

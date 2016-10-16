﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetCharm.Image.Addins;

namespace BatchProcess
{
    public partial class BatchProcessForm : Form
    {
        private AddinHost Host = null;
        private IAddin addin;

        public BatchProcessForm()
        {
            InitializeComponent();
        }

        public BatchProcessForm(AddinHost host)
        {
            Host = host;
            InitializeComponent();

            AddinUtils.Translate( addin, this, toolTip, new object[] { cmsFileList, dlgOpen } );
        }

        public BatchProcessForm( IAddin addin )
        {
            this.addin = addin;
            InitializeComponent();

            AddinUtils.Translate( addin, this, toolTip, new object[] { cmsFileList, dlgOpen } );
        }

        private void BatchProcessForm_Load( object sender, EventArgs e )
        {
            //
        }

        protected internal void AddFiles( string file )
        {
            lvFiles.Items.Add( new ListViewItem( new string[] { Path.GetFileName( file ), file } ) );
        }

        protected internal void AddFiles(string[] files)
        {
            foreach ( string f in files )
            {
                lvFiles.Items.Add( new ListViewItem( new string[] { Path.GetFileName( f ), f } ) );
            }
        }

        private void tsmiAddImage_Click( object sender, EventArgs e )
        {
            if(dlgOpen.ShowDialog() == DialogResult.OK)
            {
                AddFiles( dlgOpen.FileNames );
            }
        }

        private void tsmiRemoveImage_Click( object sender, EventArgs e )
        {
            foreach(ListViewItem item in lvFiles.SelectedItems)
            {
                lvFiles.Items.Remove( item );
            }
        }

        private void tsmiClear_Click( object sender, EventArgs e )
        {
            lvFiles.Items.Clear();
        }

        private void tsmiProcessSelected_Click( object sender, EventArgs e )
        {
            //
        }

        private void tsmiProcessAll_Click( object sender, EventArgs e )
        {
            //
        }

        private void lvFiles_ItemSelectionChanged( object sender, ListViewItemSelectionChangedEventArgs e )
        {
            if( imgPreview.Image != null) imgPreview.Image.Dispose();

            imgPreview.Image = new Bitmap( e.Item.SubItems[1].Text );
        }
    }
}
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace FileOpenerClient
{
	public class frmMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblFileName;
		private System.Windows.Forms.TextBox txtFileName;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.Button btnReadFile;
		private System.Windows.Forms.Label lblFileSize;
		private System.Windows.Forms.Label lblFileSizeValue;
		private System.ComponentModel.Container components = null;

		public frmMain()
		{
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblFileName = new System.Windows.Forms.Label();
			this.txtFileName = new System.Windows.Forms.TextBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.btnReadFile = new System.Windows.Forms.Button();
			this.lblFileSize = new System.Windows.Forms.Label();
			this.lblFileSizeValue = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblFileName
			// 
			this.lblFileName.Location = new System.Drawing.Point(8, 8);
			this.lblFileName.Name = "lblFileName";
			this.lblFileName.Size = new System.Drawing.Size(64, 16);
			this.lblFileName.TabIndex = 0;
			this.lblFileName.Text = "&File Name:";
			this.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtFileName
			// 
			this.txtFileName.Location = new System.Drawing.Point(72, 8);
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.TabIndex = 1;
			this.txtFileName.Text = "";
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(184, 8);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.TabIndex = 2;
			this.btnBrowse.Text = "&Browse";
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// btnReadFile
			// 
			this.btnReadFile.Location = new System.Drawing.Point(184, 40);
			this.btnReadFile.Name = "btnReadFile";
			this.btnReadFile.TabIndex = 3;
			this.btnReadFile.Text = "&Read File";
			this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
			// 
			// lblFileSize
			// 
			this.lblFileSize.Location = new System.Drawing.Point(112, 72);
			this.lblFileSize.Name = "lblFileSize";
			this.lblFileSize.Size = new System.Drawing.Size(64, 16);
			this.lblFileSize.TabIndex = 4;
			this.lblFileSize.Text = "File Size:";
			this.lblFileSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblFileSizeValue
			// 
			this.lblFileSizeValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblFileSizeValue.Location = new System.Drawing.Point(184, 72);
			this.lblFileSizeValue.Name = "lblFileSizeValue";
			this.lblFileSizeValue.Size = new System.Drawing.Size(72, 16);
			this.lblFileSizeValue.TabIndex = 5;
			this.lblFileSizeValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(272, 102);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.lblFileSizeValue,
																		  this.lblFileSize,
																		  this.btnReadFile,
																		  this.btnBrowse,
																		  this.txtFileName,
																		  this.lblFileName});
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "File Opener";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnBrowse_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog openFileDlg = new OpenFileDialog();
			openFileDlg.CheckPathExists = true;
			openFileDlg.CheckFileExists = true;
			openFileDlg.Multiselect = false;
			openFileDlg.InitialDirectory = Application.StartupPath;
			openFileDlg.RestoreDirectory = true;
			openFileDlg.Filter = "All files (*.*)|*.*";
			DialogResult result = openFileDlg.ShowDialog(this);

			if(result == DialogResult.OK)
			{
				this.txtFileName.Text = openFileDlg.FileName;
			}
		}

		private void btnReadFile_Click(object sender, System.EventArgs e)
		{
			this.lblFileSizeValue.Text = string.Empty;

			string fileName = this.txtFileName.Text;
			
			if(fileName != null && fileName.Length > 0)
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					FileOpener opener = new FileOpener();
					int fileSize = opener.ReadFile(fileName);
					this.lblFileSizeValue.Text = fileSize.ToString();
				}
				catch(Exception ex)
				{
					this.Cursor = Cursors.Default;
					MessageBox.Show(this, ex.Message, "File Opener Exception", 
						MessageBoxButtons.OK, 
						MessageBoxIcon.Exclamation);
					MessageBox.Show(this, ex.StackTrace, "File Opener Exception", 
						MessageBoxButtons.OK, 
						MessageBoxIcon.Exclamation);
				}
				finally
				{
					this.Cursor = Cursors.Default;
				}
			}
		}

		static void Main(string[] args)
		{
			Application.Run(new frmMain());
		}
	}
}

using Apress.NetAttributes;
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace Apress.NetAttributes
{
	public class CountryClient : System.Windows.Forms.Form
	{
		private const string COUNTRY_FILE = @"\country.txt";
		private ImprovedCountry mCountry = null;
		private System.Windows.Forms.Label lblMaximumPopulation;
		private System.Windows.Forms.Label lblMaximumPopulationValue;
		private System.Windows.Forms.Label lblMinimumPopulationValue;
		private System.Windows.Forms.Label lblMinimumPopulation;
		private System.Windows.Forms.Label lblPopulation;
		private System.Windows.Forms.TextBox txtPopulation;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnSavePersist;
		private System.Windows.Forms.Button btnLoadPersist;
		private System.ComponentModel.Container components = null;

		private void ShowPopulationLimits()
		{
			this.lblMaximumPopulationValue.Text = 
				ImprovedCountry.MAXIMUM_POPULATION.ToString();
			this.lblMinimumPopulationValue.Text = 
				ImprovedCountry.MINIMUM_POPULATION.ToString();
		}

		private void ShowCountry()
		{
			if(this.mCountry != null)
			{
				this.txtName.Text = this.mCountry.Name;
				this.txtPopulation.Text = this.mCountry.Population.ToString();
			}
		}

		public CountryClient()
		{
			InitializeComponent();
			this.ShowCountry();
			this.ShowPopulationLimits();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.lblMaximumPopulation = new System.Windows.Forms.Label();
			this.lblMaximumPopulationValue = new System.Windows.Forms.Label();
			this.lblMinimumPopulationValue = new System.Windows.Forms.Label();
			this.lblMinimumPopulation = new System.Windows.Forms.Label();
			this.lblPopulation = new System.Windows.Forms.Label();
			this.txtPopulation = new System.Windows.Forms.TextBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblName = new System.Windows.Forms.Label();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnSavePersist = new System.Windows.Forms.Button();
			this.btnLoadPersist = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblMaximumPopulation
			// 
			this.lblMaximumPopulation.Location = new System.Drawing.Point(8, 8);
			this.lblMaximumPopulation.Name = "lblMaximumPopulation";
			this.lblMaximumPopulation.Size = new System.Drawing.Size(120, 16);
			this.lblMaximumPopulation.TabIndex = 0;
			this.lblMaximumPopulation.Text = "Ma&ximum Population:";
			this.lblMaximumPopulation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblMaximumPopulationValue
			// 
			this.lblMaximumPopulationValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblMaximumPopulationValue.Location = new System.Drawing.Point(128, 8);
			this.lblMaximumPopulationValue.Name = "lblMaximumPopulationValue";
			this.lblMaximumPopulationValue.Size = new System.Drawing.Size(128, 16);
			this.lblMaximumPopulationValue.TabIndex = 1;
			this.lblMaximumPopulationValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblMinimumPopulationValue
			// 
			this.lblMinimumPopulationValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblMinimumPopulationValue.Location = new System.Drawing.Point(128, 32);
			this.lblMinimumPopulationValue.Name = "lblMinimumPopulationValue";
			this.lblMinimumPopulationValue.Size = new System.Drawing.Size(128, 16);
			this.lblMinimumPopulationValue.TabIndex = 3;
			this.lblMinimumPopulationValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblMinimumPopulation
			// 
			this.lblMinimumPopulation.Location = new System.Drawing.Point(8, 32);
			this.lblMinimumPopulation.Name = "lblMinimumPopulation";
			this.lblMinimumPopulation.Size = new System.Drawing.Size(120, 16);
			this.lblMinimumPopulation.TabIndex = 2;
			this.lblMinimumPopulation.Text = "Mi&nimum Population:";
			this.lblMinimumPopulation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblPopulation
			// 
			this.lblPopulation.Location = new System.Drawing.Point(8, 64);
			this.lblPopulation.Name = "lblPopulation";
			this.lblPopulation.Size = new System.Drawing.Size(120, 16);
			this.lblPopulation.TabIndex = 4;
			this.lblPopulation.Text = "&Population:";
			this.lblPopulation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtPopulation
			// 
			this.txtPopulation.Location = new System.Drawing.Point(128, 64);
			this.txtPopulation.Name = "txtPopulation";
			this.txtPopulation.Size = new System.Drawing.Size(128, 20);
			this.txtPopulation.TabIndex = 5;
			this.txtPopulation.Text = "";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(128, 88);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(128, 20);
			this.txtName.TabIndex = 7;
			this.txtName.Text = "";
			// 
			// lblName
			// 
			this.lblName.Location = new System.Drawing.Point(8, 88);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(120, 16);
			this.lblName.TabIndex = 6;
			this.lblName.Text = "Na&me:";
			this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnUpdate
			// 
			this.btnUpdate.Location = new System.Drawing.Point(56, 120);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(96, 24);
			this.btnUpdate.TabIndex = 8;
			this.btnUpdate.Text = "&Update";
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(160, 120);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(96, 24);
			this.btnSave.TabIndex = 9;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnSavePersist
			// 
			this.btnSavePersist.Location = new System.Drawing.Point(56, 152);
			this.btnSavePersist.Name = "btnSavePersist";
			this.btnSavePersist.Size = new System.Drawing.Size(96, 24);
			this.btnSavePersist.TabIndex = 10;
			this.btnSavePersist.Text = "&Save (Persist)";
			this.btnSavePersist.Click += new System.EventHandler(this.btnSavePersist_Click);
			// 
			// btnLoadPersist
			// 
			this.btnLoadPersist.Location = new System.Drawing.Point(160, 152);
			this.btnLoadPersist.Name = "btnLoadPersist";
			this.btnLoadPersist.Size = new System.Drawing.Size(96, 24);
			this.btnLoadPersist.TabIndex = 11;
			this.btnLoadPersist.Text = "&Load (Persist)";
			this.btnLoadPersist.Click += new System.EventHandler(this.btnLoadPersist_Click);
			// 
			// CountryClient
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(266, 184);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnLoadPersist,
																		  this.btnSavePersist,
																		  this.btnSave,
																		  this.btnUpdate,
																		  this.txtName,
																		  this.lblName,
																		  this.txtPopulation,
																		  this.lblPopulation,
																		  this.lblMinimumPopulationValue,
																		  this.lblMinimumPopulation,
																		  this.lblMaximumPopulationValue,
																		  this.lblMaximumPopulation});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "CountryClient";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Country Client";
			this.Load += new System.EventHandler(this.CountryClient_Load);
			this.ResumeLayout(false);

		}
		#endregion

		static void Main() 
		{
			Application.Run(new CountryClient());
		}

		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(this.mCountry == null)
				{
					this.mCountry = new ImprovedCountry(this.txtName.Text, 
						Int32.Parse(this.txtPopulation.Text));
				}
				else
				{
					this.mCountry.Population = 
						Int32.Parse(this.txtPopulation.Text);
					this.mCountry.Name = this.txtName.Text;
				}

				this.ShowCountry();
			}
			catch(FormatException ex)
			{
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			if(this.mCountry != null)
			{
				TextWriter countryFile = 
					File.CreateText(Application.StartupPath + 
					@"\country.txt");

				try
				{
					countryFile.WriteLine(
						"name:{0}", this.mCountry.Name);
					countryFile.WriteLine(
						"population:{0}", this.mCountry.Population);
				}
				finally
				{
					countryFile.Close();
				}
			}
		}

		private void CountryClient_Load(object sender, System.EventArgs e)
		{
		}

		private void btnSavePersist_Click(object sender, System.EventArgs e)
		{
			if(this.mCountry != null)
			{
				FileStream fs = null;

				try
				{
					PersistedCountry pc = new PersistedCountry(
						this.mCountry.Name, this.mCountry.Population);
					fs = File.Create(Application.StartupPath + 
						COUNTRY_FILE);
					pc.Save(fs);
				}
				finally
				{
					fs.Close();
				}
			}
		}

		private void btnLoadPersist_Click(object sender, System.EventArgs e)
		{
			FileStream fs = null;

			try
			{
				PersistedCountry pc = new PersistedCountry(string.Empty, 
					0);
				fs = File.Open(Application.StartupPath + 
					COUNTRY_FILE, FileMode.Open);
				pc.Load(fs);

				this.mCountry = new ImprovedCountry(pc.Name, pc.Population);
				this.txtName.Text = this.mCountry.Name;
				this.txtPopulation.Text = this.mCountry.Population.ToString();
			}
			finally
			{
				fs.Close();
			}
		
		}
	}
}

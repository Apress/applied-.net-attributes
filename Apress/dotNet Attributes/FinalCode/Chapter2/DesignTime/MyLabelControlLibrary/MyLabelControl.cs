using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace MyLabelControlLibrary
{
public class MyLabelControl : UserControl
{
   private const string DEFAULT_MESSAGE = "My custom label control";
   private System.Windows.Forms.Label lblMessage;
   
   [DefaultValue(DEFAULT_MESSAGE)]
   [Category("My Custom Properties")]
   [Description("The message displayed in the control")]
   [ParenthesizePropertyName(true)]
   public string Message
   {
      get { return lblMessage.Text; }
      set { lblMessage.Text = value; }
   }

   
	#region Component Designer generated code

   /// <summary> 
   /// Required designer variable.
   /// </summary>
   private System.ComponentModel.Container components = null;

   public MyLabelControl()
   {
      // This call is required by the Windows.Forms Form Designer.
      InitializeComponent();

      // TODO: Add any initialization after the InitForm call

   }

   /// <summary> 
   /// Clean up any resources being used.
   /// </summary>
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
	/// <summary> 
	/// Required method for Designer support - do not modify 
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
      this.lblMessage = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // lblMessage
      // 
      this.lblMessage.Location = new System.Drawing.Point(16, 24);
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.Size = new System.Drawing.Size(424, 40);
      this.lblMessage.TabIndex = 0;
      // 
      // MyLabelControl
      // 
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                     this.lblMessage});
      this.Name = "MyLabelControl";
      this.Size = new System.Drawing.Size(448, 88);
      this.Load += new System.EventHandler(this.MyLabelControl_Load);
      this.ResumeLayout(false);

   }
	#endregion

   private void MyLabelControl_Load(object sender, System.EventArgs e)
   {
      lblMessage.Text = DEFAULT_MESSAGE;
   }
}
}

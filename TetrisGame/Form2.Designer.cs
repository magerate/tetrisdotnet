namespace TetrisGame
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }

                if (leftAutoTetris!=null)
                {
                    leftAutoTetris.Dispose();
                }
                if (rightAutoTetris!=null)
                {
                    rightAutoTetris.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.tetrisBox1 = new TetrisGame.TetrisBox();
            this.tetrisBox2 = new TetrisGame.TetrisBox();
            this.SuspendLayout();
            // 
            // tetrisBox1
            // 
            this.tetrisBox1.BackColor = System.Drawing.Color.Transparent;
            this.tetrisBox1.Location = new System.Drawing.Point(36, 23);
            this.tetrisBox1.Name = "tetrisBox1";
            this.tetrisBox1.ShowGrid = true;
            this.tetrisBox1.Size = new System.Drawing.Size(321, 641);
            this.tetrisBox1.TabIndex = 0;
            this.tetrisBox1.Text = "tetrisBox1";
            // 
            // tetrisBox2
            // 
            this.tetrisBox2.BackColor = System.Drawing.Color.Transparent;
            this.tetrisBox2.Location = new System.Drawing.Point(511, 23);
            this.tetrisBox2.Name = "tetrisBox2";
            this.tetrisBox2.ShowGrid = true;
            this.tetrisBox2.Size = new System.Drawing.Size(321, 641);
            this.tetrisBox2.TabIndex = 1;
            this.tetrisBox2.Text = "tetrisBox2";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(927, 683);
            this.Controls.Add(this.tetrisBox2);
            this.Controls.Add(this.tetrisBox1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private TetrisBox tetrisBox1;
        private TetrisBox tetrisBox2;

    }
}
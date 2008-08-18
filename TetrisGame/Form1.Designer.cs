namespace TetrisGame
{
    partial class Form1
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
                if (autoDropTetris != null)
                {
                    autoDropTetris.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelHeight = new System.Windows.Forms.Label();
            this.labelShapeCount = new System.Windows.Forms.Label();
            this.labelScore = new System.Windows.Forms.Label();
            this.labelKilled = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelLevel = new System.Windows.Forms.Label();
            this.tetrisBox1 = new TetrisGame.TetrisBox();
            this.shapeBox1 = new TetrisGame.ShapeBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(359, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Height:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(359, 225);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Shape Count:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(359, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Score:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(359, 269);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Killed:";
            // 
            // labelHeight
            // 
            this.labelHeight.BackColor = System.Drawing.Color.Transparent;
            this.labelHeight.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeight.Location = new System.Drawing.Point(456, 140);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(100, 14);
            this.labelHeight.TabIndex = 5;
            this.labelHeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelShapeCount
            // 
            this.labelShapeCount.BackColor = System.Drawing.Color.Transparent;
            this.labelShapeCount.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShapeCount.Location = new System.Drawing.Point(456, 223);
            this.labelShapeCount.Name = "labelShapeCount";
            this.labelShapeCount.Size = new System.Drawing.Size(100, 14);
            this.labelShapeCount.TabIndex = 6;
            this.labelShapeCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelScore
            // 
            this.labelScore.BackColor = System.Drawing.Color.Transparent;
            this.labelScore.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScore.Location = new System.Drawing.Point(456, 177);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(100, 14);
            this.labelScore.TabIndex = 8;
            this.labelScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelKilled
            // 
            this.labelKilled.BackColor = System.Drawing.Color.Transparent;
            this.labelKilled.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKilled.Location = new System.Drawing.Point(456, 267);
            this.labelKilled.Name = "labelKilled";
            this.labelKilled.Size = new System.Drawing.Size(100, 14);
            this.labelKilled.TabIndex = 9;
            this.labelKilled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(359, 309);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Level:";
            // 
            // labelLevel
            // 
            this.labelLevel.BackColor = System.Drawing.Color.Transparent;
            this.labelLevel.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLevel.Location = new System.Drawing.Point(456, 307);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(100, 14);
            this.labelLevel.TabIndex = 11;
            this.labelLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tetrisBox1
            // 
            this.tetrisBox1.BackColor = System.Drawing.Color.Transparent;
            this.tetrisBox1.Location = new System.Drawing.Point(12, 12);
            this.tetrisBox1.Name = "tetrisBox1";
            this.tetrisBox1.ShowGrid = true;
            this.tetrisBox1.Size = new System.Drawing.Size(321, 641);
            this.tetrisBox1.TabIndex = 14;
            this.tetrisBox1.Text = "tetrisBox1";
            // 
            // shapeBox1
            // 
            this.shapeBox1.BackColor = System.Drawing.Color.Transparent;
            this.shapeBox1.Location = new System.Drawing.Point(361, 12);
            this.shapeBox1.Name = "shapeBox1";
            this.shapeBox1.Shape = null;
            this.shapeBox1.Size = new System.Drawing.Size(96, 96);
            this.shapeBox1.TabIndex = 13;
            this.shapeBox1.Text = "shapeBox1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(581, 663);
            this.Controls.Add(this.tetrisBox1);
            this.Controls.Add(this.shapeBox1);
            this.Controls.Add(this.labelLevel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelKilled);
            this.Controls.Add(this.labelScore);
            this.Controls.Add(this.labelShapeCount);
            this.Controls.Add(this.labelHeight);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Label labelShapeCount;
        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.Label labelKilled;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelLevel;
        private ShapeBox shapeBox1;
        private TetrisBox tetrisBox1;


    }
}


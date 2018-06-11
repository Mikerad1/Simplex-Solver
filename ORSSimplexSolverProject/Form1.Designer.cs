namespace ORSSimplexSolverProject
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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.btnGetProblem = new System.Windows.Forms.Button();
            this.rtbProblem = new System.Windows.Forms.RichTextBox();
            this.btnSolveProblem = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rtbSolution = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnGetProblem
            // 
            this.btnGetProblem.Location = new System.Drawing.Point(12, 12);
            this.btnGetProblem.Name = "btnGetProblem";
            this.btnGetProblem.Size = new System.Drawing.Size(75, 23);
            this.btnGetProblem.TabIndex = 0;
            this.btnGetProblem.Text = "Get problem";
            this.btnGetProblem.UseVisualStyleBackColor = true;
            this.btnGetProblem.Click += new System.EventHandler(this.btnGetProblem_Click);
            // 
            // rtbProblem
            // 
            this.rtbProblem.Location = new System.Drawing.Point(12, 75);
            this.rtbProblem.Name = "rtbProblem";
            this.rtbProblem.ReadOnly = true;
            this.rtbProblem.Size = new System.Drawing.Size(334, 208);
            this.rtbProblem.TabIndex = 1;
            this.rtbProblem.Text = "";
            // 
            // btnSolveProblem
            // 
            this.btnSolveProblem.Enabled = false;
            this.btnSolveProblem.Location = new System.Drawing.Point(93, 12);
            this.btnSolveProblem.Name = "btnSolveProblem";
            this.btnSolveProblem.Size = new System.Drawing.Size(86, 23);
            this.btnSolveProblem.TabIndex = 2;
            this.btnSolveProblem.Text = "Solve problem";
            this.btnSolveProblem.UseVisualStyleBackColor = true;
            this.btnSolveProblem.Click += new System.EventHandler(this.btnSolveProblem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Problem:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(349, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Optimal Tablue:";
            // 
            // rtbSolution
            // 
            this.rtbSolution.Location = new System.Drawing.Point(352, 75);
            this.rtbSolution.Name = "rtbSolution";
            this.rtbSolution.Size = new System.Drawing.Size(334, 208);
            this.rtbSolution.TabIndex = 5;
            this.rtbSolution.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 295);
            this.Controls.Add(this.rtbSolution);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSolveProblem);
            this.Controls.Add(this.rtbProblem);
            this.Controls.Add(this.btnGetProblem);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetProblem;
        private System.Windows.Forms.RichTextBox rtbProblem;
        private System.Windows.Forms.Button btnSolveProblem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rtbSolution;
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ORSSimplexSolverProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region GlobalVariables
        FileHandler fh;
        public double[,] LHS;
        public double[] RHS;
        public double[] objectiveFunction;
        public string lpTpye;
        public int amountOfVariables;
        public double[,] B_1;
        public double zValue;
        public double[] newRHS;
        public int[] iB;
        #endregion

        #region GetProblemButtonClicked
        private void btnGetProblem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fh = new FileHandler(openFileDialog1.FileName);
                fh.SetProblem();
                LHS = fh.LHS;
                RHS = fh.RHS;
                objectiveFunction = fh.objectiveFunction;
                amountOfVariables = fh.amountOfVariables;
                lpTpye = fh.lpType;
                string line = "";
                rtbProblem.AppendText("Objective function:\n");
                for (int i = 0; i < objectiveFunction.Length; i++)
                {
                    line = line + objectiveFunction[i] + "\t";
                }
                rtbProblem.AppendText(line);
                rtbProblem.AppendText("\nConstraints LHS:\n");
                for (int row = 0; row < LHS.GetLength(1); row++)
                {
                    line = "";
                    for (int column = 0; column < LHS.GetLength(0); column++)
                    {
                        line = line + LHS[column, row] + "\t";
                    }
                    rtbProblem.AppendText("\n" + line);
                }
                rtbProblem.AppendText("\nConstraints RHS:\n");
                line = "";
                for (int i = 1; i < RHS.Length; i++)
                {
                    line = line + RHS[i] + "\n";
                }
                rtbProblem.AppendText(line);
                btnGetProblem.Enabled = false;
                btnSolveProblem.Enabled = true;
            }
        }
        #endregion

        #region SolveProblemButtonClicked
        private void btnSolveProblem_Click(object sender, EventArgs e)
        {
            Solver solver = new Solver(LHS, RHS, objectiveFunction, lpTpye, amountOfVariables);

            zValue = solver.zValue;
            B_1 = solver.B_1;
            newRHS = solver.newRHS;
            iB = solver.iB;
            string line = "";
            
            rtbSolution.AppendText(line);
            rtbSolution.AppendText("\nB_1:\n");
            for (int row = 0; row < B_1.GetLength(1); row++)
            {
                line = "";
                for (int column = 0; column < B_1.GetLength(0); column++)
                {
                    line = line + B_1[column, row] + "\t";
                }
                rtbSolution.AppendText("\n" + line);
            }
            rtbSolution.AppendText("\nBasic variables columns:\tBasic variables values:\n");
            line = "";
            for (int i = 0; i < newRHS.Length; i++)
            {
                line = line + (iB[i] + 1).ToString() + "\t\t\t" + newRHS[i] + "\n";
            }
            rtbSolution.AppendText(line);
            btnGetProblem.Enabled = true;
            btnSolveProblem.Enabled = false;
            List<string> answer = new List<string>();
            foreach (string item in rtbSolution.Lines)
            {
                answer.Add(item);
            }
            FileHandler fh = new FileHandler(answer);
        } 
        #endregion
    }
}

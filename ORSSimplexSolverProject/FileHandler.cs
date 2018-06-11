using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ORSSimplexSolverProject
{
    public class FileHandler
    {
        static FileStream f;
        string probelm = "";
        string line;
        StreamReader sr;
        StreamWriter sw;
        int amountOfLines = 0;
        int amountOfColumns = 0;
        public double[,] LHS;
        public double[] RHS;
        public double[] objectiveFunction;
        public int amountOfVariables;
        public string lpType;

        public FileHandler(string filename)
        {
            f = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            sr = new StreamReader(f);
        }
        public FileHandler(List<string> toWrite)
        {
            f = new FileStream("Output.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
            sw = new StreamWriter(f);
            foreach (string line in toWrite)
            {
                sw.WriteLine(line);
            }
            f.Close();
        }
        #region SetProblem
        public void SetProblem()
        {
            while ((line = sr.ReadLine()) != null)
            {
                probelm = probelm + line + " ; ";
                string[] temp = line.Split(' ');
                if (amountOfColumns < temp.Length - 2)
                {
                    amountOfColumns = temp.Length - 2;
                    amountOfVariables = amountOfColumns;
                }
                if (temp[0] == "MAX" || temp[0] == "Max" || temp[0] == "max")
                {
                    lpType = "max";
                }
                else if (temp[0] == "MIN" || temp[0] == "Min" || temp[0] == "min")
                {
                    lpType = "min";
                }
                amountOfLines++;
            }
            string[] problem1 = probelm.Split(';');
            string newProblem = "";
            //This creates the table and adds the slack and eccess variables
            for (int i = 0; i < problem1.Length - 1; i++)
            {
                string newline = "";
                string[] line = problem1[i].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    if (line[j] == "<=")
                    {
                        if (i == 1)
                        {
                            newline = newline + " 1";
                            for (int k = 0; k < amountOfLines - 2; k++)
                            {
                                newline = newline + " 0";
                            }
                        }
                        else
                        {
                            for (int l = 1; l <= i; l++)
                            {
                                if (l == i)
                                {
                                    newline = newline + " 1";
                                    for (int k = 0; k < amountOfLines - l - 1; k++)
                                    {
                                        newline = newline + " 0";
                                    }
                                    break;
                                }
                                else
                                {
                                    newline = newline + " 0";
                                }
                            }
                        }
                    }
                    else if (line[j] == ">=")
                    {
                        if (i == 1)
                        {
                            newline = newline + " -1";
                            for (int k = 0; k < amountOfLines - 2; k++)
                            {
                                newline = newline + " 0";
                            }
                        }
                        else
                        {
                            for (int l = 1; l <= i; l++)
                            {
                                if (l == i)
                                {
                                    newline = newline + " -1";
                                    for (int k = 0; k < amountOfLines - l; k++)
                                    {
                                        newline = newline + " 0";
                                    }
                                    break;
                                }
                                else
                                {
                                    newline = newline + " 0";
                                }
                            }
                        }
                    }
                    else if (line[j] == "")
                    {

                    }
                    else if (j == line.Length - 2)
                    {
                        if (i == 0)
                        {
                            newline = newline + " " + line[j];
                        }
                        else
                        {
                            newline = newline + " = " + line[j];
                        }
                    }
                    else
                    {
                        newline = newline + " " + line[j];
                    }
                }
                newProblem = newProblem + newline + " ;";
            }
            problem1 = newProblem.Split(';');
            for (int i = 0; i < problem1.Length; i++)
            {
                string[] temp = problem1[i].Split(' ');
                if (amountOfColumns < temp.Length - 4)
                {
                    amountOfColumns = temp.Length - 4;
                }
            }
            string[] problem2 = newProblem.Split(';');
            string[] problem3 = newProblem.Split(' ');
            double holder = 0;

            LHS = new double[amountOfColumns, amountOfLines];
            RHS = new double[amountOfLines];
            objectiveFunction = new double[amountOfColumns];
            bool symbolFound = false;
            bool semiFOund = false;
            int currentLine = 0;
            int currentColumn = 0;
            for (int i = 0; i < problem3.Length; i++)
            {
                if (problem3[i] == "=")
                {
                    symbolFound = true;
                }
                else if (problem3[i] == ";")
                {
                    semiFOund = true;
                }
                if (double.TryParse(problem3[i], out holder) == true)
                {
                    if (symbolFound == true)
                    {
                        currentColumn++;
                        RHS[currentLine] = holder;
                        symbolFound = false;
                    }
                    else if (semiFOund == true)
                    {
                        currentLine++;
                        currentColumn = 0;
                        LHS[currentColumn, currentLine] = holder;
                        currentColumn++;
                        semiFOund = false;
                    }
                    else
                    {
                        LHS[currentColumn, currentLine] = holder;
                        currentColumn++;
                    }
                }
            }
            for (int i = 0; i < LHS.GetLength(0); i++)
            {
                objectiveFunction[i] = LHS[i, 0];
            }
            double[,] newLHS = new double[amountOfColumns, amountOfLines - 1];
            for (int column = 0; column < LHS.GetLength(0); column++)
            {
                for (int row = 0; row < LHS.GetLength(1) - 1; row++)
                {
                    newLHS[column, row] = LHS[column, row + 1];
                }
            }
            LHS = newLHS;
        } 
        #endregion
    }
}

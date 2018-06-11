using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORSSimplexSolverProject
{
    public class Solver
    {
        //The constraint matrix
        double[,] A;
        //The right hand side
        double[] b;
        //The coeficients of the objective function
        double[] c;
        //Specifies the location of the basic variables
        public int[] iB;
        //Specifies the location of the non basic variables
        int[] iN;
        //Specifies what rule to use. if irule=0 then the smallest coeficient rule will be used. if irule=1 then Bland's rule will be used
        int irule;
        //Basic varibales in the rows i think
        public double[,] B_1;
        //This is the coefiecients of the basic variables in the objective function
        double[] cB;
        //This is the final answer
        public double zValue;
        //This is the identity matrix
        double[,] I;
        double[] cBB_1;
        string lpType;
        public double[] newRHS;
        public Solver(double[,] lhs, double[] rhs, double[] objectiveFunction, string lptype, int amountOfVariables)
        {
            A = lhs;
            b = rhs;
            c = objectiveFunction;
            newRHS = new double[b.Length - 1];
            int iColumn = 0;
            lpType = lptype;
            I = new double[amountOfVariables, A.GetLength(1)];
            for (int column = amountOfVariables; column < A.GetLength(0); column++)
            {
                for (int row = 0; row < A.GetLength(1); row++)
                {
                    I[iColumn, row] = A[column, row];
                }
                iColumn++;
            }
            B_1 = new double[I.GetLength(0), I.GetLength(1)];
            for (int i = 0; i < I.GetLength(0); i++)
            {
                for (int j = 0; j < I.GetLength(1); j++)
                {
                    B_1[i, j] = I[i, j];
                }
            }
            cB = new double[A.GetLength(1)];
            iB = new int[A.GetLength(1)];
            for (int i = 0; i < cB.Length; i++)
            {
                cB[i] = objectiveFunction[amountOfVariables + i];
                iB[i] = amountOfVariables + i;
            }
            iN = new int[amountOfVariables];
            for (int i = 0; i < iN.Length; i++)
            {
                iN[i] = i;
            }
            cBB_1 = new double[cB.Length];
            bool some = PriceOut();            
            zValue = FindZValue();
            string temp = "";
        }

        
        public double FindZValue()
        {
            double finalAnswer = 0;
            double tempanswer = 0;
            for (int k = 0; k < cBB_1.Length; k++)
            {
                finalAnswer = finalAnswer + cBB_1[k] * b[k + 1];
            }
            return finalAnswer;
        }
        public bool PriceOut()
        {
            
            for (int j = 0; j < B_1.GetLength(0); j++)
            {
                double answer2 = 0;
                for (int k = 0; k < B_1.GetLength(1); k++)
                {
                    answer2 = answer2 + cB[k] * B_1[j, k];
                }
                cBB_1[j] = answer2;
            }
            
            double[,] nBV = new double[iN.Length, A.GetLength(1)];
            nBV = GetNBVArray();
            double[] answers = new double[iN.Length];
            
            for (int j = 0; j < nBV.GetLength(0); j++)
            {
                double answer = 0;
                double tempanswer = 0;
                for (int k = 0; k < nBV.GetLength(1); k++)
                {
                    tempanswer = tempanswer + cBB_1[k] * nBV[j, k];
                }
                answer = answer + tempanswer;
                answers[j] = answer;
            }
            double[] tempNBVObjetiveCoeficientsPositions = new double[iN.Length];
            for (int i = 0; i < tempNBVObjetiveCoeficientsPositions.Length; i++)
            {
                tempNBVObjetiveCoeficientsPositions[i] = iN[i];
            }
            for (int i = 0; i < answers.Length; i++)
            {
                answers[i] = answers[i] - c[int.Parse(tempNBVObjetiveCoeficientsPositions[i].ToString())];
            }
            bool allNonNegative = false;
            bool allNegative = false; 
            if (lpType == "max")
            {
                allNonNegative = answers.All(x => x >= 0);
                allNegative = false;
            }
            else
            {
                allNegative = answers.All(x => x <= 0);
                allNonNegative = false;
            }
            if ((allNonNegative == true && allNegative == false) || (allNegative == true && allNonNegative == false))
            {
                for (int j = 0; j < B_1.GetLength(0); j++)
                {
                    double answer3 = 0;
                    for (int k = 0; k < B_1.GetLength(1); k++)
                    {
                        answer3 = answer3 + b[k + 1] * B_1[k, j];
                    }
                    newRHS[j] = answer3;
                }
                return true;
            }
            else
            {
                double tempFind = answers.Min();
                int position = 0;
                int tempNBVPosition = 0;
                for (int i = 0; i < answers.Length; i++)
                {
                    if (answers[i] == tempFind)
                    {
                        position = iN[i];
                        tempNBVPosition = i;
                    }
                }
                int tempNBVToBVNumber = position;
                
                //To compute the column in the current table
                double[] tempcolum = new double[nBV.GetLength(0)];
                for (int j = 0; j < B_1.GetLength(0); j++)
                {
                    double answer3 = 0;
                    for (int k = 0; k < B_1.GetLength(1); k++)
                    {
                        answer3 = answer3 + A[position, k] * B_1[k, j];
                    }
                    tempcolum[j] = answer3;
                }
                //To compute the RHS of the current table
                double[] tempRHS = new double[nBV.GetLength(0)];
                for (int j = 0; j < B_1.GetLength(0); j++)
                {
                    double answer3 = 0;
                    for (int k = 0; k < B_1.GetLength(1); k++)
                    {
                        answer3 = answer3 + b[k+1] * B_1[k, j];
                    }
                    tempRHS[j] = answer3;
                }
                for (int i = 0; i < newRHS.Length; i++)
                {
                    newRHS[i] = tempRHS[i];
                }
                //Ratio test
                double[] ratios = new double[tempRHS.Length];
                for (int i = 0; i < tempRHS.Length; i++)
                {
                    ratios[i] = tempRHS[i] / tempcolum[i];
                }
                //Finding the smallest ratio
                double temp = 0;
                for (int i = 0; i < ratios.Length; i++)
                {
                    for (int j = 0; j < ratios.Length - 1; j++)
                    {
                        if (ratios[j] > ratios[j + 1])
                        {
                            if (ratios[j+1]>0)
                            {
                                temp = ratios[j+1];
                            }                            
                        }
                        else
                        {
                            if (ratios[j] > 0)
                            {
                                temp = ratios[j];
                            }
                        }
                    }
                }

                for (int i = 0; i < ratios.Length; i++)
                {
                    if (ratios[i]==temp)
                    {
                        position = i;
                    }
                }
                int tempBVToNBVposition = position;
                int tempBVToNBV = 0;
                for (int i = 0; i < iB.Length; i++)
                {
                    if (i == tempBVToNBVposition)
                    {
                        tempBVToNBV = iB[i];
                    }
                }
                //Used to define the new basic and non basic variables
                iB[tempBVToNBVposition] = tempNBVToBVNumber;
                iN[tempNBVPosition] = tempBVToNBV;

                for (int i = 0; i < iB.Length; i++)
                {
                    cB[i] = c[iB[i]];
                }
                //Calculate the new values of the column using product form
                double[] tempColumn = new double[tempcolum.Length];
                double piviotValue = tempcolum[position];
                for (int i = 0; i < tempColumn.Length; i++)
                {
                    if (i == position)
                    {
                        tempColumn[i] = 1 / tempcolum[i];
                    }
                    else
                    {
                        tempColumn[i] = tempcolum[i] / piviotValue;
                        tempColumn[i] = tempColumn[i] * -1;
                    }
                }
                double[,] elementaryMatrix = new double[I.GetLength(0), I.GetLength(1)];
                for (int i = 0; i < I.GetLength(0); i++)
                {
                    for (int j = 0; j < I.GetLength(1); j++)
                    {
                        elementaryMatrix[i, j] = I[i, j];
                    }
                }
                double[,] tempB_1 = new double[B_1.GetLength(0), B_1.GetLength(1)];
                //Creating the elementary matrix
                for (int i = 0; i < tempColumn.Length; i++)
                {
                    elementaryMatrix[position, i] = tempColumn[i];
                }
                tempB_1 = multiplyarr(elementaryMatrix, B_1);
                B_1 = tempB_1;
                return PriceOut();
            }
        }
        private double[,] multiplyarr(double[,] firstarr, double[,] secondarr)
        {
            int matrixlength = firstarr.GetLength(1);
            double[,] temparr = new double[matrixlength, matrixlength];
            for (int row = 0; row < matrixlength; row++)
            {
                for (int col = 0; col < matrixlength; col++)
                {
                    temparr[col, row] = 0;

                    for (int i = 0; i < matrixlength; i++)
                    {
                        temparr[col, row] += firstarr[i, row] * secondarr[col, i];

                    }
                }
            }
            return temparr;
        }
        private double[,] GetNBVArray()
        {
            double[,] nBVArray = new double[iN.Length, A.GetLength(1)];
            for (int i = 0; i < iN.Length; i++)
            {
                int position = iN[i];
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    nBVArray[i, j] = A[position, j];
                }
            }
            return nBVArray;
        }

        double[,] GenerateIdentityMatrix(int amountOfBV, int amountOfLines)
        {
            double[,] identity = new double[amountOfBV, amountOfLines];
            int place1 = -1;
            for (int column = 0; column < amountOfBV; column++)
            {
                for (int row = 0; row < amountOfLines; row++)
                {
                    place1++;
                    if (column == place1)
                    {
                        identity[column, row] = 1;
                    }
                    else
                    {
                        identity[column, row] = 0;
                    }
                }
            }
            return identity;
        }
    }
}

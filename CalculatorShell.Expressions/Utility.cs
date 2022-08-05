using System.Text;

namespace CalculatorShell.Expressions
{
    /// <summary>
    /// Provides various Utility methods for Expressions
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Create a polinomial string expression that fits to a given set of points
        /// </summary>
        /// <param name="xValues">x values of data to fit</param>
        /// <param name="yValues">y values of data to fit</param>
        /// <param name="degree">degree of polinome to use</param>
        /// <returns>a string expression that fits to the data</returns>
        public static string Fit(IReadOnlyList<double> xValues, IReadOnlyList<double> yValues, int degree = 3)
        {
            int dataCount = Math.Min(xValues.Count, yValues.Count);
            //Array that will store the values of sigma(xi),sigma(xi^2),sigma(xi^3)....sigma(xi^2n)
            double[] X = new double[(2 * degree) + 1];
            for (int i = 0; i < (2 * degree) + 1; i++)
            {
                X[i] = 0;
                for (int j = 0; j < dataCount; j++)
                {
                    //consecutive positions of the array will store N,sigma(xi),sigma(xi^2),sigma(xi^3)....sigma(xi^2n)
                    X[i] += Math.Pow(xValues[j], i);
                }
            }

            //B is the Normal matrix(augmented) that will store the equations, 'a' is for value of the final coefficients
            double[,] B = new double[degree + 1, degree + 2];
            double[] a = new double[degree + 1];
            for (int i = 0; i <= degree; i++)
            {
                for (int j = 0; j <= degree; j++)
                {
                    //Build the Normal matrix by storing the corresponding coefficients at the right positions except the last column of the matrix
                    B[i, j] = X[i + j];
                }
            }

            //Array to store the values of sigma(yi),sigma(xi*yi),sigma(xi^2*yi)...sigma(xi^n*yi)
            double[] Y = new double[degree + 1];
            for (int i = 0; i < degree + 1; i++)
            {
                Y[i] = 0;
                for (int j = 0; j < dataCount; j++)
                {
                    //consecutive positions will store sigma(yi),sigma(xi*yi),sigma(xi^2*yi)...sigma(xi^n*yi)
                    Y[i] += Math.Pow(xValues[j], i) * yValues[j];
                }
            }
            for (int i = 0; i <= degree; i++)
            {
                //load the values of Y as the last column of B(Normal Matrix but augmented)
                B[i, degree + 1] = Y[i];
            }

            //n is made n+1 because the Gaussian Elimination part below was for n equations, but here n is the degree of polynomial and for n degree we get n+1 equations
            degree++;

            //From now Gaussian Elimination starts(can be ignored) to solve the set of linear equations (Pivotisation)
            for (int i = 0; i < degree; i++)
            {
                for (int k = i + 1; k < degree; k++)
                {
                    if (B[i, i] < B[k, i])
                    {
                        for (int j = 0; j <= degree; j++)
                        {
                            (B[k, j], B[i, j]) = (B[i, j], B[k, j]);
                        }
                    }
                }
            }

            //loop to perform the gauss elimination
            for (int i = 0; i < degree - 1; i++)
            {
                for (int k = i + 1; k < degree; k++)
                {
                    double t = B[k, i] / B[i, i];
                    for (int j = 0; j <= degree; j++)
                    {
                        //make the elements below the pivot elements equal to zero or elimnate the variables
                        B[k, j] -= t * B[i, j];
                    }
                }
            }

            //back-substitution
            for (int i = degree - 1; i >= 0; i--)
            {
                //x is an array whose values correspond to the values of x,y,z..
                //make the variable to be calculated equal to the rhs of the last equation
                a[i] = B[i, degree];
                for (int j = 0; j < degree; j++)
                {
                    //then subtract all the lhs values except the coefficient of the variable whose value is being calculated
                    if (j != i)
                    {
                        a[i] -= B[i, j] * a[j];
                    }
                }

                //now finally divide the rhs by the coefficient of the variable to be calculated
                a[i] /= B[i, i];
            }

            StringBuilder expression = new();
            for (int i = 0; i < degree; i++)
            {
                expression.AppendFormat("{0}*x^{1}", a[i], i);
                if (i < degree - 1)
                {
                    expression.Append('+');
                }
            }
            return expression.ToString();
        }
    }
}

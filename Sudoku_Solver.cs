using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ConsoleApplication1
{
    public struct cell
    {
        private int row;
        private int column;

        public cell(int x, int y)
        {
            row = x;
            column = y;
        }

        public int r
        {
            get
            {
                return this.row;
            }
            set
            {
                row = value;
            }
        }

        public int c
        {
            get
            {
                return column;
            }
            set
            {
                column = value;
            }
        }

        public override string ToString()
        {
            return (String.Format("{0}, {1}", row, column));
        }
    }

    class Program
    {
        // constants
        const int size = 9;

        // variables
        int[,] matrix;

        public void initialize_matrix()
        // populate matrix
        {
            matrix = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = 0;
                }
            }
        }

        public void display_matrix()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(matrix[i, j]);
                    if ((j + 1) % Math.Sqrt(size) == 0)
                        System.Console.Write("|");
                }
                if ((i + 1) % Math.Sqrt(size) == 0)
                {
                    System.Console.WriteLine();
                    System.Console.WriteLine("------------");
                }
                else
                    System.Console.WriteLine();
            }
        }
        /* displays current sudoku. undetermined numbers are 0 */

        public void assign_cell(cell c, int value)
        /* assigns value to cell and removes value from all domains in box, row, column */
        {
            matrix[c.r - 1, c.c - 1] = value;
        }

        public cell next_cell(cell c)
        {
            if (c.c == size)
            {
                c.r++;
                c.c = 1;
            }
            else
                c.c++;
            return c;
        }
        /* returns next cell in matrix */

        public Boolean is_last_cell(cell c)
        /* returns true if c is last unfilled cell */
        {
            return (c.r == size && c.c == size);
        }

        public ArrayList domain_of(cell c)
        /* returns domain of cell */
        {
            ArrayList domain = new ArrayList();
            for (int i = 1; i <= size; i++)
                domain.Add(i);
            cell temp = new cell(1, 1);
            // check row
            temp.r = c.r;
            for (int i = 1; i <= size; i++)
            {
                temp.c = i;
                domain.Remove(has_set_number(temp));
            }
            // check column
            temp.c = c.c;
            for (int i = 1; i <= size; i++)
            {
                temp.r = i;
                domain.Remove(has_set_number(temp));
            }
            // check box
            int temp_row = c.r - ((c.r - 1) % (int)Math.Sqrt(size));
            int temp_col = c.c - ((c.c - 1) % (int)Math.Sqrt(size));
            for (int i = temp_row; i < temp_row + Math.Sqrt(size); i++)
                for (int j = temp_col; j < temp_col + Math.Sqrt(size); j++)
                    domain.Remove(has_set_number(new cell(i, j)));
            return domain;
        }

        public int has_set_number(cell c)
        /* returns set number if cell is determined, else return 0 */
        {
            return matrix[c.r - 1, c.c - 1];
        }

        public Boolean main_helper(cell c)
        {
            //display_matrix();
            //Console.Write("Cell: ");
            //Console.WriteLine(c);
            if (has_set_number(c) == 0)
            {
                ArrayList domain = domain_of(c);
                if (domain.Count == 0)
                    return false;
                //Console.Write("Domain: ");
                //foreach (int i in domain)
                //    Console.Write(" {0},", i);
                //Console.WriteLine();
                foreach (int i in domain)
                {
                    //domain_print_string(matrix, c);
                    assign_cell(c, i);
                    if (is_last_cell(c))
                        return true;
                    if (main_helper(next_cell(c)))
                        return true;
                    assign_cell(c, 0);
                }
            }
            else
            {
                if (is_last_cell(c))
                    return true;
                if (main_helper(next_cell(c)))
                    return true;
            }
            //Console.WriteLine("Test A");
            return false;
        }

        /// <summary>
        /// Populate matrix with console input
        /// </summary>
        /// <param name="args"></param>
        public void input_matrix()
        {
            for (int i = 1; i <= size; i++)
            {
                string input;
                do
                {
                    Console.WriteLine("Enter Row {0}", i);
                    input = Console.ReadLine();
                } while (input.Length != size || !all_ints(input));
                for (int j = 1; j <= size; j++)
                {
                    assign_cell(new cell(i, j), (int) Char.GetNumericValue(input[j-1]));
                }
            }
        }

        /// <summary>
        /// Returns true if input has all integer values
        /// </summary>
        /// <param name="args"></param>
        public bool all_ints(string input)
        {
            for(int i = 0; i < input.Length; i++)
            {
                string nums = "0123456789";
                if(!nums.Contains(input[i]))
                    return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Start Sudoku Solver");
            cell cell1 = new cell(1, 1);
            Program p = new Program();
            p.initialize_matrix();
            p.input_matrix();
            p.display_matrix();
            Console.WriteLine("Sudoku Solver Result: {0}", p.main_helper(cell1));
            p.display_matrix();
            Console.WriteLine("End Sudoku Solver");
            System.Console.ReadKey();
        }
    }
}

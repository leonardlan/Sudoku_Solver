The Sudoku Solver is a console application built in C# for solving 3x3 Sudokus. It uses brute force by iterating all empty cells, while populating them until a solution is achieved.

To use the solver, run the application and enter the Sudoku row by row starting from the top, replacing empty cells with 0.

For example, if you were to enter the following Sudoku,

  2|3  | 5 
 1 | 4 |   
9  |  5| 2 
-----------
8  |76 | 3 
7  |   |  4
 6 | 34|  5
-----------
 5 |2  |  6
   | 1 | 7 
 7 |  9|8  

you should enter:

002300050
010040000
900005020
800760030
700000004
060034005
050200006
000010070
070009800

The solver will return with the solution:

682|397|451
517|642|398
934|185|627
-----------
845|761|239
793|528|164
261|934|785
-----------
158|273|946
429|816|573
376|459|812

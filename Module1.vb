Module Module1
    'Lukah Youngs
    '11/21/22
    'SD Battleship

    'this custom data type will be used to represent the state of each cell
    Public Enum cellstate
        empty
        miss
        hit
        cpuship
    End Enum

    Public rows As Integer = 4
    Public cols As Integer = 5
    'this 2D array will be used to represent the board
    Public board(rows - 1, cols - 1) As cellstate

    Sub Main()
        Dim gameover As Boolean = False
        Dim shotcount As Integer = 0
        resetboard()
        placecpuship()

        Do
            shotcount += 1
            printboard()
            Dim row As Integer = getuserinput("please enter the row to fire on -> ", rows - 1)
            Dim col As Integer = getuserinput("please enter the column to fire on -> ", cols - 1)
            If board(row, col) = cellstate.cpuship Then
                board(row, col) = cellstate.hit
                gameover = True
            Else
                board(row, col) = cellstate.miss
            End If

        Loop While Not gameover
        printboard()
        Console.WriteLine("You hit it in {0} shots!", shotcount.ToString)
    End Sub


    ''' <summary>
    ''' Sets all cells in the board to cellstate empty
    ''' </summary>
    Sub resetboard()
        For i As Integer = 0 To board.GetUpperBound(0)
            For j As Integer = 0 To board.GetUpperBound(1)
                board(i, j) = cellstate.empty
            Next
        Next


    End Sub
    ''' <summary>
    ''' selects a random spot on the board and changes the spot value to cmpship
    ''' </summary>
    Sub placecpuship()
        Dim randomgenerator As New Random
        Dim row As Integer
        Dim col As Integer
        row = randomgenerator.Next(0, rows)
        col = randomgenerator.Next(0, cols)
        board(row, col) = cellstate.cpuship
        Console.WriteLine(row & ", " & col)
    End Sub

    Sub printboard()
        'print the top row (column headers)
        Console.Write("  ")
        For row As Integer = 0 To cols - 1
            Console.Write(row & " ")
        Next
        Console.Write(vbNewLine)

        'loop through each row of the board, print the row number and then the column values
        For row As Integer = 0 To board.GetUpperBound(0)
            Console.Write(row & " ")
            For col As Integer = 0 To board.GetUpperBound(1)
                Select Case board(row, col)
                    Case cellstate.empty, cellstate.cpuship
                        Console.Write("- ")
                    Case cellstate.miss
                        Console.ForegroundColor = ConsoleColor.Blue
                        Console.Write("O ")
                        Console.ResetColor()
                    Case cellstate.hit
                        Console.ForegroundColor = ConsoleColor.Red
                        Console.Write("X ")
                        Console.ResetColor()
                End Select
            Next
            Console.Write(vbNewLine)
        Next
    End Sub

    ''' <summary>
    ''' Repeats the prompt to the user until a number between 0  and max(inclusive) is given
    ''' return that number
    ''' </summary>
    ''' <param name="prompt"></param>
    ''' <param name="max"></param>
    ''' <returns>an int between 0 and max(inclusive)</returns>
    Function getuserinput(prompt As String, max As Integer) As Integer
        Dim valid As Boolean = False
        Dim inputstr As String
        Dim userinput As Integer
        'ask the user for input until valid input is given
        Do
            Console.Write(prompt)
            inputstr = Console.ReadLine
            valid = Integer.TryParse(inputstr, userinput)
            If Not (valid AndAlso userinput >= 0 AndAlso userinput <= max) Then
                valid = False
            End If
        Loop While Not valid

        Return userinput

    End Function

End Module

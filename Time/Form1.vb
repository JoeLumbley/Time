' Time🕒

' MIT License
' Copyright(c) 2024 Joseph W. Lumbley

' Permission Is hereby granted, free Of charge, to any person obtaining a copy
' of this software And associated documentation files (the "Software"), to deal
' in the Software without restriction, including without limitation the rights
' to use, copy, modify, merge, publish, distribute, sublicense, And/Or sell
' copies of the Software, And to permit persons to whom the Software Is
' furnished to do so, subject to the following conditions:

' The above copyright notice And this permission notice shall be included In all
' copies Or substantial portions of the Software.

' THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or
' IMPLIED, INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY,
' FITNESS FOR A PARTICULAR PURPOSE And NONINFRINGEMENT. IN NO EVENT SHALL THE
' AUTHORS Or COPYRIGHT HOLDERS BE LIABLE For ANY CLAIM, DAMAGES Or OTHER
' LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or OTHERWISE, ARISING FROM,
' OUT OF Or IN CONNECTION WITH THE SOFTWARE Or THE USE Or OTHER DEALINGS IN THE
' SOFTWARE.

' Monica is our an AI assistant.
' https://monica.im/

Public Class Form1

    Private Context As BufferedGraphicsContext
    Private Buffer As BufferedGraphics
    Private DisplayText As String
    Private DisplayFont As New Font("Segoe UI", 12, FontStyle.Regular)
    Private DisplayFontSize As Single
    Private DisplayPosition As New Point(100, 0)
    Private ReadOnly AlineCenterMiddle As New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        InitializeForm()

        InitializeBuffer()

        Timer1.Interval = 20

        Timer1.Enabled = True

        Debug.Print($"Program running... {Now.ToShortTimeString}")

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        DisplayText = Now.ToLocalTime.ToShortTimeString()

        Refresh() ' Calls OnPaint Sub

    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize

        ' Set the font size for the time display based on the width of the client rectangle
        DisplayFontSize = ClientRectangle.Width \ 12

        DisplayFont = New Font("Segoe UI", DisplayFontSize, FontStyle.Regular)

        ' Center the time display in the client rectangle.
        DisplayPosition.X = ClientRectangle.Width \ 2
        DisplayPosition.Y = ClientRectangle.Height \ 2

        ' Dispose of the existing buffer
        If Buffer IsNot Nothing Then

            Buffer.Dispose()

            Buffer = Nothing ' Set to Nothing to avoid using a disposed object

        End If

        ' The buffer will be reallocated in OnPaint

    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)

        ' Allocate the buffer if it hasn't been allocated yet
        If Buffer Is Nothing Then

            Buffer = Context.Allocate(e.Graphics, ClientRectangle)

        End If

        DrawFrame()

        Buffer.Render(e.Graphics)

    End Sub

    Private Sub DrawFrame()

        If Buffer IsNot Nothing Then

            Try

                With Buffer.Graphics

                    .CompositingMode = Drawing2D.CompositingMode.SourceOver
                    .TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
                    .SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                    .PixelOffsetMode = Drawing2D.PixelOffsetMode.None
                    .CompositingQuality = Drawing2D.CompositingQuality.HighQuality

                    .Clear(Color.Black)

                    .DrawString(DisplayText, DisplayFont, Brushes.White, DisplayPosition, AlineCenterMiddle)

                End With

            Catch ex As Exception

                Debug.Print("Draw error: " & ex.Message)

            End Try

        Else

            Debug.Print("Buffer is not initialized.")

        End If

    End Sub

    Private Sub InitializeForm()

        CenterToScreen()

        SetStyle(ControlStyles.UserPaint, True)

        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)

        SetStyle(ControlStyles.AllPaintingInWmPaint, True)

        Text = "Time🕒 - Code with Joe"

        Me.WindowState = FormWindowState.Maximized

    End Sub

    Private Sub InitializeBuffer()

        ' Set context to the context of this app.
        Context = BufferedGraphicsManager.Current

        Context.MaximumBuffer = Screen.PrimaryScreen.WorkingArea.Size

        ' Allocate the buffer initially using the current client rectangle
        Buffer = Context.Allocate(CreateGraphics(), ClientRectangle)

    End Sub

End Class

# Time🕒

A simple Windows application that displays the current local time. 

The application utilizes optimized rendering techniques to ensure smooth updates and an aesthetically pleasing user interface.


![001](https://github.com/user-attachments/assets/92e3e427-61f7-4924-bf7e-c364bb192af0)


Features

- Real-time display of the current time.

- Responsive design that adjusts font size and position based on the window size.

- Utilizes buffered graphics for improved performance and visual quality.




---


### Code Walkthrough

#### 1. **Class Declaration**
```vb
Public Class Form1
```
This line defines a new class named `Form1`. In Windows Forms applications, a class represents a window or form.

#### 2. **Variable Declarations**
```vb
Private Context As BufferedGraphicsContext
Private Buffer As BufferedGraphics
Private DisplayText As String
Private DisplayFont As New Font("Segoe UI", 12, FontStyle.Regular)
Private DisplayFontSize As Single
Private DisplayPosition As New Point(100, 0)
```
- **Context**: Manages the buffered graphics.
- **Buffer**: Holds the graphics that will be drawn to the screen.
- **DisplayText**: Stores the current time as a string.
- **DisplayFont**: Defines the font used to display the time.
- **DisplayFontSize**: Stores the size of the font, which will change based on the window size.
- **DisplayPosition**: Determines where the time will be drawn on the form.

#### 3. **Form Load Event**
```vb
Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    InitializeForm()
    InitializeBuffer()
    Timer1.Interval = 20
    Timer1.Enabled = True
    Debug.Print($"Program running... {Now.ToShortTimeString()}")
End Sub
```
- This method runs when the form loads.
- **InitializeForm()**: Sets up the form's appearance and properties.
- **InitializeBuffer()**: Prepares the graphics buffer.
- **Timer1.Interval = 20**: Sets the timer to tick every 20 milliseconds.
- **Timer1.Enabled = True**: Starts the timer, which triggers the `Timer1_Tick` event.
- **Debug.Print**: Outputs a message to the debug console.

#### 4. **Timer Tick Event**
```vb
Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
    DisplayText = Now.ToShortTimeString()
    Refresh() ' Calls OnPaint Sub
End Sub
```
- This method runs every time the timer ticks.
- **DisplayText**: Updates to the current time.
- **Refresh()**: Triggers the form to redraw itself, calling the `OnPaint` method.

#### 5. **Form Resize Event**
```vb
Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
    DisplayFontSize = ClientRectangle.Width \ 12
    DisplayFont = New Font("Segoe UI", DisplayFontSize, FontStyle.Regular)
    DisplayPosition.X = ClientRectangle.Width \ 2
    DisplayPosition.Y = ClientRectangle.Height \ 2

    If Buffer IsNot Nothing Then Buffer.Dispose()
    Buffer = Nothing
End Sub
```
- This method runs when the form is resized.
- **DisplayFontSize**: Adjusts the font size based on the form's width.
- **DisplayPosition**: Centers the time display in the form.
- **Buffer.Dispose()**: Cleans up the existing buffer to prepare for a new one.

#### 6. **OnPaint Method**
```vb
Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
    If Buffer Is Nothing Then Buffer = Context.Allocate(e.Graphics, ClientRectangle)
    DrawFrame()
    Buffer.Render(e.Graphics)
End Sub
```
- This method is called when the form needs to be redrawn.
- **Buffer Allocation**: Allocates graphics if it hasn't been done yet.
- **DrawFrame()**: Calls a method to draw the current time.
- **Buffer.Render**: Draws the buffer content to the screen.

#### 7. **DrawFrame Method**
```vb
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
```
- This method draws the current time onto the buffer.
- **Graphics Settings**: Sets various drawing quality settings.
- **Clear(Color.Black)**: Clears the buffer with a black background.
- **DrawString**: Draws the time string in white at the specified position.
- **Error Handling**: Catches any errors during drawing and prints them to the debug console.

#### 8. **InitializeForm Method**
```vb
Private Sub InitializeForm()
    CenterToScreen()
    SetStyle(ControlStyles.UserPaint, True)
    SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
    SetStyle(ControlStyles.AllPaintingInWmPaint, True)
    Text = "Time🕒 - Code with Joe"
    Me.WindowState = FormWindowState.Maximized
End Sub
```
- This method sets up the form's initial properties.
- **CenterToScreen()**: Centers the form on the screen.
- **SetStyle**: Configures the form to use double buffering for smoother rendering.
- **Text**: Sets the title of the window.
- **WindowState**: Maximizes the form when it opens.

#### 9. **InitializeBuffer Method**
```vb
Private Sub InitializeBuffer()
    Context = BufferedGraphicsManager.Current
    Context.MaximumBuffer = Screen.PrimaryScreen.WorkingArea.Size
    Buffer = Context.Allocate(CreateGraphics(), ClientRectangle)
End Sub
```
- This method initializes the graphics buffer.
- **BufferedGraphicsManager.Current**: Gets the current graphics manager.
- **MaximumBuffer**: Sets the maximum buffer size.
- **Allocate**: Allocates the buffer for drawing.


This application uses Windows Forms to create a simple clock that updates every 20 milliseconds. It utilizes buffered graphics for smooth rendering and adjusts the display based on the size of the window.














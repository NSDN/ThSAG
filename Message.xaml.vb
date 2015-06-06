Imports System.Windows.Media.Animation
Imports System.Threading
Imports ThSAG.DispatcherHelper

Class Message
    Private MousePos As Point
    Private Ani As Animation.DoubleAnimation
    Private Flag As Integer = -1

    Private Sub Me_MouseMove(sender As Object, e As MouseEventArgs)
        If e.LeftButton = MouseButtonState.Pressed Then
            Me.DragMove()
        End If
    End Sub

    Private Sub Msg_Load()
        Ani = New DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.1))
        Me.BeginAnimation(Border.OpacityProperty, Ani)
    End Sub

    Public Sub HideMe()
        Ani = New DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.2))
        Me.BeginAnimation(Border.OpacityProperty, Ani)
        While Me.Opacity > 0
            DoEvents()
        End While
        Me.Hide()
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As RoutedEventArgs)
        HideMe()
        Flag = 1
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As RoutedEventArgs)
        HideMe()
        Flag = 0
    End Sub

    Public Function MsgShow(ByVal Title As String, ByVal Context As String) As Boolean
        Return MsgShow(300, 200, Title, Context, False)
    End Function

    Public Function MsgShow(ByVal Title As String, ByVal Context As String, ByVal UseCancel As Boolean) As Boolean
        Return MsgShow(300, 200, Title, Context, UseCancel)
    End Function

    Public Function MsgShow(ByVal Title As String, ByVal Context As String, ByVal UseCancel As Boolean, ByVal UseWait As Boolean) As Boolean
        Return MsgShow(300, 200, Title, Context, UseCancel, UseWait)
    End Function

    Public Function MsgShow(ByVal Width As Double, ByVal Height As Double, ByVal Title As String, ByVal Context As String, ByVal UseCancel As Boolean) As Boolean
        Return MsgShow(Width, Height, Title, Context, UseCancel, True)
    End Function

    Public Function MsgShow(ByVal Width As Double, ByVal Height As Double, ByVal Title As String, ByVal Context As String, ByVal UseCancel As Boolean, ByVal UseWait As Boolean) As Boolean
        Me.Width = Width
        Me.Height = Height
        Me.Head.Content = Title
        Me.Context.Content = Context
        Me.Top = (Windows.SystemParameters.PrimaryScreenHeight - Me.Height) / 2
        Me.Left = (Windows.SystemParameters.PrimaryScreenWidth - Me.Width) / 2
        If UseCancel Then
            BtnCancel.Visibility = Windows.Visibility.Visible
        Else
            BtnCancel.Visibility = Windows.Visibility.Hidden
        End If
        Me.Opacity = 0
        Flag = -1
        Me.Show()
        Msg_Load()
        While Flag = -1 And UseWait
            DoEvents()
        End While
        Return Flag
    End Function

End Class

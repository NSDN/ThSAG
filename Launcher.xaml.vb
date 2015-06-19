Imports System.Windows.Media.Animation
Imports System.Threading
Imports ThSAG.DispatcherHelper

Class Launcher
    Private MousePos As Point
    Private MsgBox As New Message
    Private Ani As Animation.DoubleAnimation
    Private InClose As Boolean = False

    Private Sub Me_MouseMove(sender As Object, e As MouseEventArgs)
        If e.LeftButton = MouseButtonState.Pressed Then
            Me.DragMove()
        End If
    End Sub

    Private Sub ThSAG_StateChanged(sender As Object, e As EventArgs)
        If Me.WindowState = Windows.WindowState.Normal And Me.Opacity = 0 Then
            Ani = New DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.1))
            Me.BeginAnimation(Border.OpacityProperty, Ani)
            While Me.Opacity < 1
                DoEvents()
            End While
        End If
    End Sub

    Private Sub ThSAG_Loaded(sender As Object, e As RoutedEventArgs)
        Switch.IsEnabled = False
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As RoutedEventArgs)
        InClose = True
        If MsgBox.IsVisible And MsgBox.Opacity > 0 Then MsgBox.HideMe()
        Ani = New DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3))
        Me.BeginAnimation(Border.OpacityProperty, Ani)
        While Me.Opacity > 0
            DoEvents()
        End While
        Me.Close()
        End
    End Sub

    Private Sub MinMe()
        Ani = New DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.2))
        Me.BeginAnimation(Border.OpacityProperty, Ani)
        While Me.Opacity > 0
            DoEvents()
        End While
        Me.WindowState = Windows.WindowState.Minimized
    End Sub

    Private Sub HideMe()
        Ani = New DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.2))
        Me.BeginAnimation(Border.OpacityProperty, Ani)
        While Me.Opacity > 0
            DoEvents()
        End While
        Me.Hide()
    End Sub

    Public Sub ShowMe()
        Me.Hide()
        Me.Opacity = 0
        Me.Show()
        Ani = New DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.1))
        Me.BeginAnimation(Border.OpacityProperty, Ani)
        While Me.Opacity < 1
            DoEvents()
        End While
    End Sub

    Private Sub LaunchSniff() Handles LaunchCtrl.IsEnabledChanged
        If LaunchCtrl.Flag = 1 Then HideMe()
        If LaunchCtrl.Flag = 0 Then ShowMe()
        LaunchCtrl.Flag = -1
    End Sub

    Private Sub EditSniff() Handles EditCtrl.IsEnabledChanged
        If EditCtrl.Flag = 1 Then HideMe()
        If EditCtrl.Flag = 0 Then ShowMe()
        EditCtrl.Flag = -1
    End Sub

    Private Sub BtnMin_Click(sender As Object, e As RoutedEventArgs)
        MinMe()
    End Sub

    Private Sub BtnHelp_Click(sender As Object, e As RoutedEventArgs)
        If MsgBox.MsgShow("About", "ThSAG ReBuild ver0.001" & Chr(13) & "Copyright the WDJ 2005 - 2015" & Chr(13) & "All rights reserved") Then
            If Not MsgBox.MsgShow("Bingo!", "ThSAG ReBuild ver0.001" & Chr(13) & "Count: 1", True) Then
                MsgBox.MsgShow("Bingo!", "ThSAG ReBuild ver0.001" & Chr(13) & "Count: 2")
            End If
        End If
    End Sub

    Private Sub Switch_Click(sender As Object, e As RoutedEventArgs)
        If Switch.IsChecked Then
            With LaunchCtrl
                Ani = New DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.2))
                .BeginAnimation(Border.OpacityProperty, Ani)
                While .Opacity > 0
                    DoEvents()
                End While
                .Visibility = Windows.Visibility.Hidden
                .Opacity = 0
            End With

            With Me
                Dim WidthAni As New DoubleAnimation(.Width, 800, TimeSpan.FromSeconds(0.1))
                Dim HeightAni As New DoubleAnimation(.Height, 450, TimeSpan.FromSeconds(0.1))
                Dim TopAni As New DoubleAnimation(.Top, (Windows.SystemParameters.PrimaryScreenHeight - 450) / 2, TimeSpan.FromSeconds(0.1))
                Dim LeftAni As New DoubleAnimation(.Left, (Windows.SystemParameters.PrimaryScreenWidth - 800) / 2, TimeSpan.FromSeconds(0.1))
                .BeginAnimation(Border.WidthProperty, WidthAni, HandoffBehavior.Compose)
                While Not (.Width = 800)
                    DoEvents()
                End While
                .BeginAnimation(Border.HeightProperty, HeightAni, HandoffBehavior.Compose)
                While Not (.Height = 450)
                    DoEvents()
                End While
                .BeginAnimation(TopProperty, TopAni, HandoffBehavior.Compose)
                While Not (.Top = (Windows.SystemParameters.PrimaryScreenHeight - 450) / 2)
                    DoEvents()
                End While
                .BeginAnimation(LeftProperty, LeftAni, HandoffBehavior.Compose)
                While Not (.Left = (Windows.SystemParameters.PrimaryScreenWidth - 800) / 2)
                    DoEvents()
                End While
            End With

            With EditCtrl
                .Opacity = 0
                .Visibility = Windows.Visibility.Visible
                Ani = New DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2))
                .BeginAnimation(Border.OpacityProperty, Ani)
                While .Opacity > 0
                    DoEvents()
                End While
            End With
        Else
            With EditCtrl
                Ani = New DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.2))
                .BeginAnimation(Border.OpacityProperty, Ani)
                While .Opacity > 0
                    DoEvents()
                End While
                .Visibility = Windows.Visibility.Hidden
                .Opacity = 0
            End With

            With Me
                Dim WidthAni As New DoubleAnimation(.Width, 500, TimeSpan.FromSeconds(0.1))
                Dim HeightAni As New DoubleAnimation(.Height, 375, TimeSpan.FromSeconds(0.1))
                Dim TopAni As New DoubleAnimation(.Top, (Windows.SystemParameters.PrimaryScreenHeight - 375) / 2, TimeSpan.FromSeconds(0.1))
                Dim LeftAni As New DoubleAnimation(.Left, (Windows.SystemParameters.PrimaryScreenWidth - 500) / 2, TimeSpan.FromSeconds(0.1))
                .BeginAnimation(Border.WidthProperty, WidthAni, HandoffBehavior.Compose)
                While Not (.Width = 500)
                    DoEvents()
                End While
                .BeginAnimation(Border.HeightProperty, HeightAni, HandoffBehavior.Compose)
                While Not (.Height = 375)
                    DoEvents()
                End While
                .BeginAnimation(TopProperty, TopAni, HandoffBehavior.Compose)
                While Not (.Top = (Windows.SystemParameters.PrimaryScreenHeight - 450) / 2)
                    DoEvents()
                End While
                .BeginAnimation(LeftProperty, LeftAni, HandoffBehavior.Compose)  
                While Not (.Left = (Windows.SystemParameters.PrimaryScreenWidth - 800) / 2)
                    DoEvents()
                End While
            End With

            With LaunchCtrl
                .Opacity = 0
                .Visibility = Windows.Visibility.Visible
                Ani = New DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2))
                .BeginAnimation(Border.OpacityProperty, Ani)
                While .Opacity > 0
                    DoEvents()
                End While
            End With
        End If
    End Sub

End Class

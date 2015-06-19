#Const _DEBUG_ = 0

Imports ThSAG.DxVBDLL
Imports ThSAG.EngineCore.Base

Public Class EngineCore
    Public Class Base
        ' Base类用于放置基础函数以及数据类型
        ' 对象比较小的使用结构体，大的使用类
        ' 此类不可实例化
        Public Const JUDGE_MISS As Integer = 0
        Public Const JUDGE_GRAZE As Integer = 1
        Public Const JUDGE_AWAY As Integer = 2
        Public Const JUDGE_NONE As Integer = 3
        Public Const IMAGE_BG As Integer = 0
        Public Const IMAGE_CG As Integer = 1
        Public Const IMAGE_BULLET As Integer = 2

        Public Structure Point3D
            ' 因为Drawing里还有一个二维的Point
            ' 这里如此定义

            Public x As Double
            Public y As Double
            Public z As Double
        End Structure

        Public Structure Point2D
            ' 给弹幕用的坐标

            Public x As Double
            Public y As Double
        End Structure

        Public Enum Keys
            KeyBACK = 14
            KeyTAB = 15
            KeyRETURN = 28
            KeyLSHIFT = 42
            KeyRSHIFT = 54
            KeyLCONTROL = 29
            KeyRCONTROL = 157
            KeyESCAPE = 1
            KeySPACE = 57
            KeyPGUP = 201
            KeyPGDN = 209
            KeyEND = 207
            KeyHOME = 199
            KeyLEFT = 203
            KeyUP = 200
            KeyRIGHT = 205
            KeyDOWN = 208
            KeyINSERT = 210
            KeyDELETE = 211
            KeyMINUS = 12
            KeyYEN = 125
            KeyPREVTRACK = 144
            KeyPERIOD = 52
            KeySLASH = 53
            KeyLALT = 56
            KeyRALT = 184
            KeySCROLL = 70
            KeySEMICOLON = 39
            KeyCOLON = 146
            KeyLBRACKET = 26
            KeyRBRACKET = 27
            KeyAT = 145
            KeyBACKSLASH = 43
            KeyCOMMA = 51
            KeyKANJI = 148
            KeyCONVERT = 121
            KeyNOCONVERT = 123
            KeyKANA = 112
            KeyAPPS = 221
            KeyCAPSLOCK = 58
            KeySYSRQ = 183
            KeyPAUSE = 197
            KeyLWIN = 219
            KeyRWIN = 220
            KeyNUMLOCK = 69
            KeyNUMPAD0 = 82
            KeyNUMPAD1 = 79
            KeyNUMPAD2 = 80
            KeyNUMPAD3 = 81
            KeyNUMPAD4 = 75
            KeyNUMPAD5 = 76
            KeyNUMPAD6 = 77
            KeyNUMPAD7 = 71
            KeyNUMPAD8 = 72
            KeyNUMPAD9 = 73
            KeyMULTIPLY = 55
            KeyADD = 78
            KeySUBTRACT = 74
            KeyDECIMAL = 83
            KeyDIVIDE = 181
            KeyNUMPADENTER = 156
            KeyF1 = 59
            KeyF2 = 60
            KeyF3 = 61
            KeyF4 = 62
            KeyF5 = 63
            KeyF6 = 64
            KeyF7 = 65
            KeyF8 = 66
            KeyF9 = 67
            KeyF10 = 68
            KeyF11 = 87
            KeyF12 = 88
            KeyA = 30
            KeyB = 48
            KeyC = 46
            KeyD = 32
            KeyE = 18
            KeyF = 33
            KeyG = 34
            KeyH = 35
            KeyI = 23
            KeyJ = 36
            KeyK = 37
            KeyL = 38
            KeyM = 50
            KeyN = 49
            KeyO = 24
            KeyP = 25
            KeyQ = 16
            KeyR = 19
            KeyS = 31
            KeyT = 20
            KeyU = 22
            KeyV = 47
            KeyW = 17
            KeyX = 45
            KeyY = 21
            KeyZ = 44
            Key0 = 11
            Key1 = 2
            Key2 = 3
            Key3 = 4
            Key4 = 5
            Key5 = 6
            Key6 = 7
            Key7 = 8
            Key8 = 9
            Key9 = 10
        End Enum

        Public Shared Function Distance3D(ByVal Point1 As Point3D, ByVal Point2 As Point3D) As Double
            ' 距离计算公式，3D

            Return Math.Sqrt(Math.Pow(Point2.x - Point1.x, 2) _
                + Math.Pow(Point2.y - Point1.y, 2) _
                + Math.Pow(Point2.z - Point1.z, 2))
        End Function

        Public Shared Function Distance2D(ByVal Point1 As Point2D, ByVal Point2 As Point2D) As Double
            ' 距离计算公式，2D

            Return Math.Sqrt(Math.Pow(Point2.x - Point1.x, 2) _
                + Math.Pow(Point2.y - Point1.y, 2))
        End Function

        Public Shared Function Average3D(ByVal Point As Point3D) As Double
            Return (Point.x + Point.y + Point.z) / 3D
        End Function

        Public Shared Function Average2D(ByVal Point As Point2D) As Double
            Return (Point.x + Point.y) / 2D
        End Function

        Public Shared Function GetKey(ByVal KeyCode As Keys) As Boolean
            ' 键盘输入检测

            Return Convert.ToBoolean(DX.CheckHitKey(KeyCode))
        End Function

        Public Class FPSCounter
            ' 进行帧率计算和控制的类
            ' 此类需要实例化
            ' 精度为小数点后一位
            ' 其中的Update要加入主游戏循环

            Private StartTime As Integer
            Private Count As Integer
            Private FPSNum As Single
            Private Const N As Integer = 60
            Private Const FPS As Integer = 60

            Public Sub New()
                StartTime = 0
                Count = 0
                FPSNum = 0
            End Sub

            Public Sub Update()
                If Count = 0 Then
                    StartTime = System.Environment.TickCount
                End If
                If Count = N Then
                    Dim TmpTime As Integer = System.Environment.TickCount
                    FPSNum = 1000.0F / ((CType(TmpTime, Single) - CType(StartTime, Single)) / CType(N, Single))
                    Count = 0
                    StartTime = TmpTime
                End If
                Count = Count + 1
            End Sub

            Public Sub WaitTime()
                Dim TookTime As Integer = System.Environment.TickCount - StartTime
                Dim WaitTime As Integer = Count * 1000 / FPS - TookTime

                If WaitTime > 0 Then
                    DX.WaitTimer(WaitTime)
                End If
            End Sub

            Public Function GetFPS(ByVal Digits As Integer) As Single
                Return Math.Round(FPSNum, Digits)
            End Function

            Public Function GetFPS() As Single
                Return FPSNum
            End Function

        End Class

        Private Shared Function Modulator(ByRef Context As String, ByRef Keys As String) As String
            Return Modulator(Context, Keys, False)
        End Function

        Private Shared Function Modulator(ByRef Context As String, ByRef Keys As String, ByVal UsePath As Boolean) As String
            Dim FileBytes As Byte(), KeyBytes As Byte()
            Dim Key As Integer = 0

            If UsePath Then
                FileBytes = IO.File.ReadAllBytes(Context)
                KeyBytes = IO.File.ReadAllBytes(Keys)
            Else
                FileBytes = Text.Encoding.Unicode.GetBytes(Context)
                KeyBytes = Text.Encoding.Unicode.GetBytes(Keys)
            End If

            For i = 0 To FileBytes.LongLength - 1
                If KeyBytes(Key) < &H7F Then
                    FileBytes(i) = &HFF - FileBytes(i)
                End If

                If Key < KeyBytes.LongLength - 1 Then
                    Key += 1
                Else
                    Key = 0
                End If
            Next i

            Return Text.Encoding.Unicode.GetString(FileBytes)
        End Function

        Private Shared Function GetHead(ByVal Content As String) As String
            Dim Tmp As String = ""
            Dim Count As Integer = 0
            Do
                Tmp += Content.Chars(Count)
            Loop
        End Function

    End Class

    Public Class DxVB
        ' 对DxLib的二次封装
        ' 操作DxLib的函数尽量在这里进行封装

        Public Structure DxImage
            ' 这里是一个图片的结构体
            ' 包含资源句柄和图片尺寸

            Public Handle As Integer
            Public Width As Integer
            Public Height As Integer
        End Structure

        Public Class AniObject
            Public Enum Type
                Sin
                Cos
                Liner
            End Enum

            Private ImgSrc As DxImage
            Private Counter As Long
            Private AniValue As Double

            Public Sub New(ByRef DxTex As DxImage, ByVal StartTime As Long)
                ImgSrc = DxTex
                Counter = StartTime
            End Sub

            Private Sub Draw(ByVal x As Single, ByVal y As Single, Optional ByVal Angle As Double = 0, Optional ByVal Scale As Double = 1)
                DrawPic(ImgSrc, x, y, Angle, Scale)
            End Sub

            Public Function Fade(ByVal x As Single, ByVal y As Single, ByVal NowTime As Long) As Boolean
                Return FadeIn(x, y, NowTime) And FadeOut(x, y, NowTime - 128)
            End Function

            Public Function FadeIn(ByVal x As Single, ByVal y As Single, ByVal NowTime As Long) As Boolean
                Return FadeIn(x, y, 1, NowTime)
            End Function

            Public Function FadeIn(ByVal x As Single, ByVal y As Single, ByVal NowTime As Long, ByVal TotalTime As Integer) As Boolean
                Return Trans(x, y, 1, 0, 0, x, y, 1, 0, 255, TotalTime, NowTime)
            End Function

            Public Function FadeIn(ByVal x As Single, ByVal y As Single, ByVal Scale As Single, ByVal NowTime As Long) As Boolean
                Return Trans(x, y, Scale, 0, 0, x, y, Scale, 0, 255, 128, NowTime)
            End Function

            Public Function FadeOut(ByVal x As Single, ByVal y As Single, ByVal NowTime As Long) As Boolean
                Return FadeOut(x, y, 1, NowTime)
            End Function

            Public Function FadeOut(ByVal x As Single, ByVal y As Single, ByVal NowTime As Long, ByVal TotalTime As Integer) As Boolean
                Return Trans(x, y, 1, 0, 255, x, y, 1, 0, 0, TotalTime, NowTime)
            End Function

            Public Function FadeOut(ByVal x As Single, ByVal y As Single, ByVal Scale As Single, ByVal NowTime As Long) As Boolean
                Return Trans(x, y, Scale, 0, 255, x, y, Scale, 0, 0, 128, NowTime)
            End Function

            Public Function ScaleIn(ByVal x As Single, ByVal y As Single, ByVal NowTime As Long, Optional ByVal WithFade As Boolean = False) As Boolean
                Return ScaleIn(x, y, 0, NowTime, WithFade)
            End Function

            Public Function ScaleIn(ByVal x As Single, ByVal y As Single, ByVal StaticAngle As Double, ByVal NowTime As Long, Optional ByVal WithFade As Boolean = False) As Boolean
                Return ScaleIn(x, y, StaticAngle, 1, NowTime, WithFade)
            End Function

            Public Function ScaleIn(ByVal x As Single, ByVal y As Single, ByVal StaticAngle As Double, ByVal MaxScale As Single, ByVal NowTime As Long, Optional ByVal WithFade As Boolean = False) As Boolean
                If WithFade Then
                    Return Trans(x, y, 0, StaticAngle, 0, x, y, MaxScale, StaticAngle, 255, 50, NowTime)
                Else
                    Return Trans(x, y, 0, StaticAngle, 255, x, y, MaxScale, StaticAngle, 255, 50, NowTime)
                End If
            End Function

            Public Function ScaleOut(ByVal x As Single, ByVal y As Single, ByVal NowTime As Long, Optional ByVal WithFade As Boolean = False) As Boolean
                Return ScaleOut(x, y, 0, NowTime, WithFade)
            End Function

            Public Function ScaleOut(ByVal x As Single, ByVal y As Single, ByVal StaticAngle As Double, ByVal NowTime As Long, Optional ByVal WithFade As Boolean = False) As Boolean
                Return ScaleOut(x, y, StaticAngle, 1, NowTime, WithFade)
            End Function

            Public Function ScaleOut(ByVal x As Single, ByVal y As Single, ByVal StaticAngle As Double, ByVal MaxScale As Single, ByVal NowTime As Long, Optional ByVal WithFade As Boolean = False) As Boolean
                If WithFade Then
                    Return Trans(x, y, MaxScale, StaticAngle, 255, x, y, 0, StaticAngle, 0, 50, NowTime)
                Else
                    Return Trans(x, y, MaxScale, StaticAngle, 255, x, y, 0, StaticAngle, 255, 50, NowTime)
                End If
            End Function

            Public Sub Rotate(ByVal x As Single, ByVal y As Single, ByVal SpeedReduce As Single, ByVal NowTime As Long)
                Rotate(x, y, (NowTime - Counter) / SpeedReduce)
            End Sub

            Public Sub Rotate(ByVal x As Single, ByVal y As Single, ByVal Angle As Single)
                Draw(x, y, Angle)
            End Sub

            Public Function Trans(ByVal sX As Single, ByVal sY As Single, ByVal sScale As Single, ByVal sAngle As Double, ByVal sFade As Single, _
                                  ByVal tX As Single, ByVal tY As Single, ByVal tScale As Single, ByVal tAngle As Double, ByVal tFade As Single, _
                                  ByVal TotalTime As Long, ByVal NowTime As Long) As Boolean
                Return Trans(sX, sY, sScale, sAngle, sFade, tX, tY, tScale, tAngle, tFade, TotalTime, NowTime, Type.Sin)
            End Function

            Public Function Trans(ByVal sX As Single, ByVal sY As Single, ByVal sScale As Single, ByVal sAngle As Double, ByVal sFade As Single, _
                                  ByVal tX As Single, ByVal tY As Single, ByVal tScale As Single, ByVal tAngle As Double, ByVal tFade As Single, _
                                  ByVal TotalTime As Long, ByVal NowTime As Long, ByVal Type As Type) As Boolean
                If NowTime - Counter < 0 Then
                    DxFade(CInt(sFade))
                    Draw(sX, sY, sAngle, sScale)
                    DxFade(255)
                ElseIf NowTime - Counter <= TotalTime Then
                    Select Case Type
                        Case Type.Sin
                            AniValue = Math.Sin((NowTime - Counter) / (2 * TotalTime) * Math.PI)
                        Case Type.Cos
                            AniValue = 1 - Math.Cos((NowTime - Counter) / (2 * TotalTime) * Math.PI)
                        Case Type.Liner
                            AniValue = (NowTime - Counter) / TotalTime
                    End Select

                    DxFade(CInt(sFade + (tFade - sFade) * AniValue))
                    Draw(sX + (tX - sX) * AniValue, sY + (tY - sY) * AniValue, sAngle + (tAngle - sAngle) * AniValue, sScale + (tScale - sScale) * AniValue)
                    DxFade(255)
                Else
                    Return True
                End If
                Return False
            End Function

        End Class

        Public Shared Function DxInit(ByVal IconHandle As IntPtr, ByRef Title As String, ByVal IsFullScreen As Boolean, ByVal Width As Integer, ByVal Height As Integer, ByVal Samples As Integer, ByVal Quality As Integer) As Boolean
            ' 游戏窗口创建
            ' 注意当返回值为false的时候
            ' 需要提示信息，DirectX加载失败
            ' 这里的全屏是一种比较友好的全屏
            ' 可以随时切换回桌面

            DX.SetOutApplicationLogValidFlag(0)
            DX.SetWindowIconHandle(IconHandle)

            If IsFullScreen Then
                DX.ChangeWindowMode(1)
                DX.SetWindowStyleMode(2)
                DX.SetWindowSize(Windows.SystemParameters.PrimaryScreenWidth, Windows.SystemParameters.PrimaryScreenHeight)
            Else
                DX.ChangeWindowMode(1)
                DX.SetWindowStyleMode(5)
                DX.SetWindowSize(Width, Height)
            End If

            DX.SetGraphMode(1920, 1080, 32)
            'DX.SetUse3DFlag(1)
            'DX.SetWaitVSyncFlag(0)
            DX.SetAlwaysRunFlag(1)
            DX.SetWindowText(Title)
            DX.SetFullSceneAntiAliasingMode(Samples, Quality) 'NOTICE: Only in 3D Scene
            DX.SetCreateDrawValidGraphMultiSample(Samples, Quality)

            DX.SetUseDXArchiveFlag(1)
            DX.SetDXArchiveExtension("zip")
            DX.SetDXArchiveKeyString("zip")

            If DX.DxLib_Init() = -1 Then
                Return False
            End If
            DX.SetDrawScreen(DX.DX_SCREEN_BACK)
            Return True
        End Function

        Public Shared Function DxEndInfo()
            Return DX.ProcessMessage()
        End Function

        Public Shared Sub DxEnd()
            ' 关闭游戏窗口
            ' 处理这条信息
            ' DX.ProcessMessage == -1
            ' 退出主游戏循环后执行这个函数

            DX.DxLib_End()
        End Sub

        Public Shared Sub DxFlip()
            ' 翻转调色板
            ' 并清除另一面

            DX.ScreenFlip()
            DX.ClearDrawScreen()
        End Sub

        Public Shared Sub DxRef()
            ' 参考线绘制

            DX.DrawLine3D(DX.VGet(100, 100, 100), DX.VGet(1000, 100, 100), DX.GetColor(255, 0, 0))
            DX.DrawLine3D(DX.VGet(100, 100, 100), DX.VGet(100, 1000, 100), DX.GetColor(0, 255, 0))
            DX.DrawLine3D(DX.VGet(100, 100, 100), DX.VGet(0, 0, 1000), DX.GetColor(0, 0, 255))
        End Sub

        Public Shared Sub DxTitle(ByVal Title As String)
            DX.SetWindowText(Title)
        End Sub

        Public Shared Sub Dx3DCamera()
            DX.SetCameraPositionAndAngle(DX.VGet(960.0F, 1035.0F, -150.0F), Math.PI / 4.0F, 0, 0)
            DX.SetGlobalAmbientLight(DX.GetColorF(1.0F, 1.0F, 1.0F, 1.0F))
            DX.SetLightDirection(DX.VGet(0.0F, 1.0F, -1.0F))
        End Sub

        Public Shared Sub DxAlpha(ByVal R As Integer, ByVal G As Integer, ByVal B As Integer)
            DX.SetDrawBright(R, G, B)
        End Sub

        Public Shared Sub DxFade(ByVal Alpha As Integer)
            If Alpha >= 0 And Alpha <= 255 Then
                DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, Alpha)
            ElseIf Alpha <= 512 Then
                DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 511 - Alpha)
            End If
        End Sub

        Public Shared Sub DxWait(ByVal msTime As Integer)
            ' 延时的函数

            DX.WaitTimer(msTime)
        End Sub

        Public Shared Sub PlaySE(ByVal Path As String)
            DX.PlaySoundFile(Path, DX.DX_PLAYTYPE_BACK)
        End Sub

        Public Shared Sub VolSE(ByVal Volume As Integer)
            DX.SetVolumeSoundFile(Volume)
        End Sub

        Public Shared Sub PlayMusic(ByVal Music As AVG.Music)
            If Not Music.Handle = -1 Then
                DX.PlayMusicMem(Music.Handle, DX.DX_PLAYTYPE_LOOP)
            End If
        End Sub

        Public Shared Sub PlayMusic(ByVal Handle As Integer)
            DX.PlayMusicMem(Handle, DX.DX_PLAYTYPE_LOOP)
        End Sub

        Public Shared Sub PlayMusic(ByVal Path As String)
            DX.PlayMusic(Path, DX.DX_PLAYTYPE_LOOP)
        End Sub

        Public Shared Function LoadTexture(ByRef Path As String) As DxImage
            ' 这里的路径是相对路径
            ' 不带头部的斜杠
            ' 注意处理返回值

            Dim Image As DxImage
            Image.Handle = DX.LoadGraph(Path)
            DX.GetGraphSize(Image.Handle, Image.Width, Image.Height)
            If Image.Handle = -1 Then
                Return Nothing
            Else
                Return Image
            End If
        End Function

        Public Shared Function LoadTexture(ByRef Path As String, ByRef Image As DxImage) As Boolean
            ' 这里的路径是相对路径
            ' 不带头部的斜杠
            ' 注意处理返回值

            'If (System.IO.File.Exists(Environment.CurrentDirectory + "\" + Path)) Then
            '    Image.Handle = DX.LoadGraph(Environment.CurrentDirectory + "\" + Path)
            '    DX.GetGraphSize(Image.Handle, Image.Width, Image.Height)
            '    Return True
            'Else
            '    Return False
            'End If

            ' 因为DX.LoadGraph()已经处理了路径问题，就不必考虑了
            Image.Handle = DX.LoadGraph(Path)
            DX.GetGraphSize(Image.Handle, Image.Width, Image.Height)
            If Image.Handle = -1 Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Shared Function LoadMuisc(ByVal Path As String) As Integer
            Return DX.LoadMusicMem(Path)
        End Function

        Public Shared Function LoadMuisc(ByVal Music As AVG.Music)
            Music.Handle = DX.LoadMusicMem(Music.Path)
            If Music.Handle = -1 Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Shared Sub DrawPic(ByRef Image As DxImage, ByVal x As Single, ByVal y As Single)
            ' 图片绘制函数
            '要看看DxLib的3D图片绘制是什么情况

            DX.DrawGraphF(x - (Image.Width / 2), y - (Image.Height / 2), Image.Handle, 1)
        End Sub

        Public Shared Sub DrawPic(ByRef Image As DxImage, ByVal x As Single, ByVal y As Single, ByVal Angle As Double, Optional ByVal Scale As Double = 1)
            ' 图片绘制函数
            '要看看DxLib的3D图片绘制是什么情况

            DX.DrawRotaGraph(x, y, Scale, Angle, Image.Handle, 1)
        End Sub

        Public Shared Sub DrawPic(ByRef Image As DxImage, ByVal x As Single, ByVal y As Single, ByVal Scale As Double, ByVal Angle As Double, ByVal Fade As Integer)
            ' 图片绘制函数
            '要看看DxLib的3D图片绘制是什么情况

            DxFade(Fade)
            DX.DrawRotaGraph(x, y, Scale, Angle, Image.Handle, 1)
            DxFade(255)
        End Sub

        Public Shared Sub DrawSphere(ByVal x As Single, ByVal y As Single, ByVal z As Single, ByVal r As Single, ByVal DivNum As Integer, ByVal DifColorR As Integer, ByVal DifColorG As Integer, ByVal DifColorB As Integer, _
                                     ByVal SpcColorR As Integer, ByVal SpcColorG As Integer, ByVal SpcColorB As Integer, ByVal IsFill As Boolean)
            If IsFill Then
                DX.DrawSphere3D(DX.VGet(x, y, z), r, DivNum, DX.GetColor(DifColorR, DifColorG, DifColorB), DX.GetColor(SpcColorR, SpcColorG, SpcColorB), 1)
            Else
                DX.DrawSphere3D(DX.VGet(x, y, z), r, DivNum, DX.GetColor(DifColorR, DifColorG, DifColorB), DX.GetColor(SpcColorR, SpcColorG, SpcColorB), 0)
            End If
        End Sub

        Public Shared Sub DrawSphere(ByVal x As Double, ByVal y As Double, ByVal z As Double, ByVal r As Double, ByVal DivNum As Integer, ByVal DifColorR As Integer, ByVal DifColorG As Integer, ByVal DifColorB As Integer, _
                                     ByVal SpcColorR As Integer, ByVal SpcColorG As Integer, ByVal SpcColorB As Integer, ByVal IsFill As Boolean)
            If IsFill Then
                DX.DrawSphere3DD(DX.VGetD(x, y, z), r, DivNum, DX.GetColor(DifColorR, DifColorG, DifColorB), DX.GetColor(SpcColorR, SpcColorG, SpcColorB), 1)
            Else
                DX.DrawSphere3DD(DX.VGetD(x, y, z), r, DivNum, DX.GetColor(DifColorR, DifColorG, DifColorB), DX.GetColor(SpcColorR, SpcColorG, SpcColorB), 0)
            End If
        End Sub

        Public Shared Function LoadString(ByRef Context As String, ByRef Brush As System.Drawing.Brush, ByVal Size As Integer, _
                                                  Optional ByVal Width As Integer = 1600, Optional ByVal Height As Integer = 64) As DxImage
            Dim Image As System.Drawing.Bitmap = New System.Drawing.Bitmap(Width, Height)
            Dim GDI As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(Image)
            Dim FontFamily As System.Drawing.FontFamily = New System.Drawing.FontFamily("华文中宋")
            Dim Font As System.Drawing.Font = New System.Drawing.Font(FontFamily, Size, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
            GDI.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
            GDI.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High
            GDI.DrawString(Context, Font, Brush, 0, 0)
            Image.Save("String.dat", System.Drawing.Imaging.ImageFormat.Png)
            GDI.Dispose() : Image.Dispose() : Font.Dispose() : FontFamily.Dispose()
            Return LoadTexture("String.dat")
        End Function

        Public Shared Sub DrawString(ByRef Context As String, ByVal x As Single, ByVal y As Single)
            ' 采用默认字体的字符绘制

            DX.DrawStringF(x, y, Context, &HFFFFFF)
        End Sub

        Public Shared Sub DrawString(ByRef Context As String, ByVal x As Single, ByVal y As Single, ByVal Size As Integer)
            ' 采用默认字体的字符绘制

            DX.SetFontSize(Size)
            DX.DrawStringF(x, y, Context, &HFFFFFF)
        End Sub

    End Class

    Public Class STG2D
        Public Structure Objects
            Public x As Single
            Public y As Single
            Public Width As Single
            Public Height As Single
            Public Angle As Single
            Public Scale As Single
            Public k As Single
            Public dy As Single
            Public dx As Single
            Public dxIsZero As Boolean
            Public GrazeEnabled As Boolean
            Public DrawingEnabled As Boolean
            Public Life As Integer
            Public Type As Integer
            Public Index As Integer
            Public Sub Initilize()
                x = 0 : y = 0 : Angle = 0 : Scale = 0 : k = 0 : dy = 0 : dx = 0 : dxIsZero = False : GrazeEnabled = False _
                  : DrawingEnabled = False : Life = 0 : Type = 0 : Index = 0 : Width = 0 : Height = 0
            End Sub
        End Structure

        Shared Function BulletPreJudge(ByVal PlayerX As Single, ByVal PlayerY As Single, ByVal BulletX As Single, ByVal BulletY As Single, ByVal SetDistance As Single) As Boolean
            If Math.Abs(PlayerX - BulletX) < SetDistance And Math.Abs(PlayerY - BulletY) < SetDistance Then
                Return True
            Else
                Return False
            End If
        End Function

        Shared Function BulletJudge(ByRef Player As Base.Point2D, ByRef Bullet As Base.Point2D, ByVal SetMinDistance As Single, ByVal SetMaxDistance As Single) As Integer
            If Base.Distance2D(Player, Bullet) > SetMaxDistance Then
                Return Base.JUDGE_AWAY
                Exit Function
            ElseIf Base.Distance2D(Player, Bullet) > SetMinDistance Then
                Return Base.JUDGE_GRAZE
            Else
                Return Base.JUDGE_MISS
            End If
        End Function

    End Class

    Public Class STG3D
        ' STG相关的数据结构和函数
        ' 此类不可实例化

        Public Const DivideNum As Integer = 12

        Public Const JUDGE_GRAZE As Integer = 1
        Public Const JUDGE_MISS As Integer = 2
        Public Const JUDGE_AWAY As Integer = 3

        Public Const TEX_SPHERE As Integer = -2
        Public Const TEX_CIRCLE As Integer = -1

        Public Class Bullet
            ' 抽象的弹幕类
            ' 也可作为物体的基类

            Private Position As Base.Point3D
            Private Speed As Base.Point3D
            Private Rotate As Base.Point3D
            Private Scale As Base.Point3D
            Private TexPtr As Integer
            Private Texture As DxVB.DxImage
            Public IsEnabled As Boolean
            Public IsGrazed As Boolean

            Public Sub New()
                Position.x = 0 : Position.y = 0 : Position.z = 0
                Speed.x = 0 : Speed.y = 0 : Speed.z = 0
                Rotate.x = 0 : Rotate.y = 0 : Rotate.z = 0
                Scale.x = 0 : Scale.y = 0 : Scale.z = 0
                TexPtr = 0 : IsEnabled = False : IsGrazed = True
                Texture = New DxVB.DxImage
            End Sub

            Public Sub New(ByVal Texture As DxVB.DxImage)
                Position.x = 0 : Position.y = 0 : Position.z = 0
                Speed.x = 0 : Speed.y = 0 : Speed.z = 0
                Rotate.x = 0 : Rotate.y = 0 : Rotate.z = 0
                Scale.x = 0 : Scale.y = 0 : Scale.z = 0
                TexPtr = Texture.Handle : IsEnabled = False : IsGrazed = True
                Me.Texture = Texture
            End Sub

            Public Sub SetTexture(ByVal Texture As DxVB.DxImage)
                TexPtr = Texture.Handle
                Me.Texture = Texture
            End Sub

            Public Function GetPosition() As Base.Point3D
                Return Position
            End Function

            Public Sub SetPosition(ByVal x As Double, ByVal y As Double, ByVal z As Double)
                Position.x = x : Position.y = y : Position.z = z
            End Sub

            Public Sub SetPosition(ByRef Point As Base.Point3D)
                Position = Point
            End Sub

            Public Function GetSpeed() As Base.Point3D
                Return Speed
            End Function

            Public Sub SetSpeed(ByVal x As Double, ByVal y As Double, ByVal z As Double)
                Speed.x = x : Speed.y = y : Speed.z = z
            End Sub

            Public Sub SetSpeed(ByRef Point As Base.Point3D)
                Speed = Point
            End Sub

            Public Sub SetRotate(ByVal x As Double, ByVal y As Double, ByVal z As Double)
                Rotate.x = x : Rotate.y = y : Rotate.z = z
            End Sub

            Public Sub SetScale(ByVal x As Double, ByVal y As Double, ByVal z As Double)
                Scale.x = x : Scale.y = y : Scale.z = z
            End Sub

            Public Function PreJudge(ByRef Point As Base.Point3D, ByVal Range As Double) As Boolean
                Return Math.Abs(Point.x - Position.x) < Range And Math.Abs(Point.y - Position.y) < Range And Math.Abs(Point.z - Position.z) < Range
            End Function

            Public Function PreJudge(ByRef Point As Base.Point3D, ByVal RangeX As Double, ByVal RangeY As Double, ByVal RangeZ As Double) As Boolean
                Return Math.Abs(Point.x - Position.x) < RangeX And Math.Abs(Point.y - Position.y) < RangeY And Math.Abs(Point.z - Position.z) < RangeZ
            End Function

            Public Function Judge(ByRef Point As Base.Point3D) As Integer
                If (Base.Distance3D(Point, Position) > Base.Average3D(Scale) * 1.5D) Then
                    Return JUDGE_AWAY
                ElseIf (Base.Distance3D(Point, Position) > Base.Average3D(Scale)) Then
                    Return JUDGE_GRAZE
                Else
                    Return JUDGE_MISS
                End If
            End Function

            Public Function Distance(ByRef Bullet As Bullet) As Single
                Return Base.Distance3D(Position, Bullet.GetPosition())
            End Function

            Public Sub Draw()
                'If TexPtr = -2 Then DxVB.DrawSphere(Position.x, Position.y, Position.z, Base.Average3D(Scale), DivideNum, 255, 255, 255, 0, 0, 0, True)
                'If TexPtr = -1 Then DxVBDLL.DX.DrawCircle(Position.x, Position.z, Base.Average3D(Scale), DX.GetColor(255, 255, 255))
                'If TexPtr > 0 Then DxVB.DrawPic(Texture, Position.x, Position.z)
                If TexPtr = 0 Then DX.DrawPixel(Position.x, Position.z, &HFFFFFF)
                DxVB.DrawPic(Texture, Position.x, Position.z, Base.Average3D(Rotate))
            End Sub
        End Class

        Public Class STGCore
            ' STG的核心函数在这里
            ' 包括绘制，判定和控制
            ' 弹幕生成需要继承这个类
            ' 此类需要实例化

            Protected ArrayNumA As Integer
            Protected ArrayNumB As Integer
            Protected Bullets As Bullet(,)
            Public Shared Player As Bullet
            Protected Shared OriPlayer As Base.Point3D
            Protected Shared GameCenter As Bullet
            Protected CorePosition As Bullet
            Protected Shared GameRange As Double
            Protected Shared DefaultTex As Integer
            Protected Shared Graze, Miss, Num As Integer
            'Protected STGThread As Thread

            Public Shared Sub InitShared(ByVal CenterX As Double, ByVal CenterY As Double, ByVal CenterZ As Double, ByVal Range As Double, _
                            ByVal PlayerX As Double, ByVal PlayerY As Double, ByVal PlayerZ As Double, ByVal PlayerSize As Double, ByVal DefaultTexture As Integer)
                Player = New Bullet(New DxVB.DxImage)
                Player.SetPosition(PlayerX, PlayerY, PlayerZ)
                Player.SetScale(PlayerSize, PlayerSize, PlayerSize)
                Player.IsEnabled = True
                OriPlayer = Player.GetPosition()
                GameCenter = New Bullet()
                GameCenter.SetPosition(CenterX, CenterY, CenterZ)
                GameRange = Range : DefaultTex = DefaultTexture
                Graze = 0 : Miss = 0 : Num = 0
            End Sub

            Public Sub New(ByVal ArrayNum1 As Integer, ByVal ArrayNum2 As Integer, ByVal CenterX As Double, ByVal CenterY As Double, ByVal CenterZ As Double, ByVal Range As Double, _
                            ByVal PlayerX As Double, ByVal PlayerY As Double, ByVal PlayerZ As Double, ByVal PlayerSize As Double, ByVal DefaultTexture As Integer)
                ArrayNumA = ArrayNum1
                ArrayNumB = ArrayNum2
                ReDim Bullets(ArrayNumA, ArrayNumB)
                InitBullet()
                Player = New Bullet(New DxVB.DxImage)
                Player.SetPosition(PlayerX, PlayerY, PlayerZ)
                Player.SetScale(PlayerSize, PlayerSize, PlayerSize)
                Player.IsEnabled = True
                OriPlayer = Player.GetPosition()
                GameCenter = New Bullet()
                GameCenter.SetPosition(CenterX, CenterY, CenterZ)
                CorePosition = New Bullet()
                CorePosition.SetPosition(CenterX, CenterY, CenterZ)
                GameRange = Range : DefaultTex = DefaultTexture
                'STGThread = New Thread(JudgeACtrl)
            End Sub

            Public Sub New(ByVal ArrayNum1 As Integer, ByVal ArrayNum2 As Integer, ByVal CenterX As Double, ByVal CenterY As Double, ByVal CenterZ As Double)
                ArrayNumA = ArrayNum1
                ArrayNumB = ArrayNum2
                ReDim Bullets(ArrayNumA, ArrayNumB)
                InitBullet()
                CorePosition = New Bullet()
                CorePosition.SetPosition(CenterX, CenterY, CenterZ)
                'STGThread = New Thread(JudgeACtrl)
            End Sub

            Public Sub Run()
                'STGThread.Start();
            End Sub

            Public Sub Close()
                'STGThread.Abort();
            End Sub

            Public Sub GetInfo(ByVal x As Single, ByVal y As Single)
                DxVB.DrawString("Graze: " + Graze.ToString(), x, y, 20)
                DxVB.DrawString("Miss:  " + Miss.ToString(), x, y + 25, 20)
                DxVB.DrawString("Num: " + Num.ToString(), x, y + 50, 20)
            End Sub

            Public Shared Sub GetInfo(ByRef Miss As Integer, ByRef Graze As Integer, ByRef Num As Integer)
                Miss = STGCore.Miss : Graze = STGCore.Graze : Num = STGCore.Num
            End Sub

            Public Function GetCenter() As Base.Point3D
                Return CorePosition.GetPosition()
            End Function

            Public Sub SetCenter(ByVal Point As Base.Point3D)
                CorePosition.SetPosition(Point)
            End Sub

            Private Sub InitBullet()
                For i = 0 To Bullets.GetUpperBound(0) Step 1
                    For j = 0 To Bullets.GetUpperBound(1) Step 1
                        Bullets(i, j) = New Bullet()
                    Next j
                Next i
            End Sub

            Public Sub MainCircle()
                GenBullet()
                JudgeBullet()
                DrawBullet()
            End Sub

            Public Sub MainCircle(ByVal Time As Long, ByVal JmpRate As Integer)
                GenBullet()
                JudgeBullet()
                If Time Mod JmpRate = 0 Then DrawBullet()
            End Sub

            Protected Overridable Sub GenBullet()
                Dim TmpPoint As Base.Point3D
                For i = 0 To ArrayNumA - 1
                    For j = 0 To ArrayNumB - 1
                        If Not Bullets(i, j).IsEnabled Then
                            TmpPoint.x = CorePosition.GetPosition().x
                            TmpPoint.y = CorePosition.GetPosition().y
                            TmpPoint.z = CorePosition.GetPosition().z
                            Bullets(i, j).SetPosition(TmpPoint)
                            Bullets(i, j).SetScale(20, 20, 20)
                            Bullets(i, j).IsEnabled = True
                        End If
                        TmpPoint.x = 0
                        TmpPoint.y = 0
                        TmpPoint.z = 0
                        TmpPoint.x = Bullets(i, j).GetPosition().x + TmpPoint.x
                        TmpPoint.y = Bullets(i, j).GetPosition().y + TmpPoint.y
                        TmpPoint.z = Bullets(i, j).GetPosition().z + TmpPoint.z
                        If Base.Distance3D(Bullets(i, j).GetPosition(), GameCenter.GetPosition()) > GameRange Then
                            Bullets(i, j) = New EngineCore.STG3D.Bullet()
                        End If
                    Next j
                Next i
            End Sub

            Protected Overridable Sub DrawBullet()
                For i = 0 To Bullets.GetUpperBound(0) Step 1
                    For j = 0 To Bullets.GetUpperBound(1) Step 1
                        If Bullets(i, j).IsEnabled Then Bullets(i, j).Draw()
                    Next j
                Next i
                DrawPlayer()
            End Sub

            Protected Shared Sub DrawPlayer()
                Player.Draw()
            End Sub

            Protected Overridable Sub JudgeBullet()
                Num = 0
                For i = 0 To Bullets.GetUpperBound(0) Step 1
                    For j = 0 To Bullets.GetUpperBound(1) Step 1
                        If Bullets(i, j).IsEnabled Then
                            If GameCenter.PreJudge(Bullets(i, j).GetPosition(), GameRange) Then
                                Num = Num + 1
                            Else
                                Bullets(i, j) = New Bullet()
                            End If
                        End If
                    Next j
                Next i

                For i = 0 To Bullets.GetUpperBound(0) Step 1
                    For j = 0 To Bullets.GetUpperBound(1) Step 1
                        If Bullets(i, j).IsEnabled Then
                            If Bullets(i, j).IsEnabled And Player.PreJudge(Bullets(i, j).GetPosition(), GameRange) Then
                                If Player.Judge(Bullets(i, j).GetPosition()) = JUDGE_AWAY And Not Bullets(i, j).IsGrazed Then
                                    Bullets(i, j).IsGrazed = True
                                End If
                                If Player.Judge(Bullets(i, j).GetPosition()) = JUDGE_GRAZE And Bullets(i, j).IsGrazed Then
                                    Bullets(i, j).IsGrazed = False
                                    Graze = Graze + 1
                                End If
                                If Player.Judge(Bullets(i, j).GetPosition()) = JUDGE_MISS Then
                                    Bullets(i, j) = New Bullet()
                                    Miss = Miss + 1
                                    Player.SetPosition(OriPlayer)
                                End If
                            End If
                        End If
                    Next j
                Next i
            End Sub

            Public Shared Sub Control()
                Dim TmpPos As Base.Point3D
                TmpPos = Player.GetPosition()
                If DefaultTex = TEX_SPHERE Then
                    If Base.GetKey(Base.Keys.KeyLSHIFT) Or Base.GetKey(Base.Keys.KeyRSHIFT) Then
                        If Base.GetKey(Base.Keys.KeyUP) Then
                            TmpPos.z = TmpPos.z + 3
                        End If
                        If Base.GetKey(Base.Keys.KeyDOWN) Then
                            TmpPos.z = TmpPos.z - 3
                        End If
                        If Base.GetKey(Base.Keys.KeyLEFT) Then
                            TmpPos.x = TmpPos.x - 3
                        End If
                        If Base.GetKey(Base.Keys.KeyRIGHT) Then
                            TmpPos.x = TmpPos.x + 3
                        End If
                    Else
                        If Base.GetKey(Base.Keys.KeyUP) Then
                            TmpPos.z = TmpPos.z + 5
                        End If
                        If Base.GetKey(Base.Keys.KeyDOWN) Then
                            TmpPos.z = TmpPos.z - 5
                        End If
                        If Base.GetKey(Base.Keys.KeyLEFT) Then
                            TmpPos.x = TmpPos.x - 5
                        End If
                        If Base.GetKey(Base.Keys.KeyRIGHT) Then
                            TmpPos.x = TmpPos.x + 5
                        End If
                    End If
                Else
                    If Base.GetKey(Base.Keys.KeyLSHIFT) Or Base.GetKey(Base.Keys.KeyRSHIFT) Then
                        If Base.GetKey(Base.Keys.KeyUP) Then
                            TmpPos.z = TmpPos.z - 3
                        End If
                        If Base.GetKey(Base.Keys.KeyDOWN) Then
                            TmpPos.z = TmpPos.z + 3
                        End If
                        If Base.GetKey(Base.Keys.KeyLEFT) Then
                            TmpPos.x = TmpPos.x - 3
                        End If
                        If Base.GetKey(Base.Keys.KeyRIGHT) Then
                            TmpPos.x = TmpPos.x + 3
                        End If
                    Else
                        If Base.GetKey(Base.Keys.KeyUP) Then
                            TmpPos.z = TmpPos.z - 6
                        End If
                        If Base.GetKey(Base.Keys.KeyDOWN) Then
                            TmpPos.z = TmpPos.z + 6
                        End If
                        If Base.GetKey(Base.Keys.KeyLEFT) Then
                            TmpPos.x = TmpPos.x - 6
                        End If
                        If Base.GetKey(Base.Keys.KeyRIGHT) Then
                            TmpPos.x = TmpPos.x + 6
                        End If
                    End If
                End If
                If GameCenter.PreJudge(TmpPos, 432, 1, 504) Then Player.SetPosition(TmpPos)
            End Sub
        End Class

    End Class

    Public Class AVG
        ' 关于对话相关都在这里了
        ' 其实主要是字符的绘制

        Public Structure Image
            Public Texture As DxVB.DxImage
            Public Path As String
            Public Index As Integer
        End Structure
        Public Structure Music
            Public Path As String
            Public Handle As Integer
        End Structure
        Public Structure PicObj
            Public Location As Single     'Can use Enum
            Public IMG As Image
        End Structure
        Public Structure Selection
            Public Texture As DxVB.DxImage
            Public Context As String
            Public JumpSceneIndex As Integer
        End Structure
        Public Structure Config
            Public GameName As String
            Public ScriptCount As Integer
            Public ScriptPath() As String
            Public SavePath As String
        End Structure
        Public Enum WordType
            Comment
            Script
            Title
            GameRun
            Saving
            Loading
        End Enum
        Private Enum ScriptHead
            NULL
            StartSceneDim
            EndSceneDim
            Background
            BGM
            StartCGDim
            EndCGDim
            CGPath
            CGLocation
            StartWordDim
            Choice
            Jump
            EndWordDim
        End Enum
        Public Class Scene
            Public SceneIndex As Integer
            Public BG As Image
            Public CG() As PicObj
            Public BGM As Music
            Public WordType As WordType
            Public Choices() As Selection
            Public IsBuilt As Boolean

            Private IsAniIn As Boolean
            Private IsAniOut As Boolean
            Private AniTime() As Integer
            Private AniBG, AniCursor As DxVB.AniObject
            Private AniCG() As DxVB.AniObject
            Private AniChoice() As DxVB.AniObject
            Public Sub New()
                IsBuilt = False
                SceneIndex = -1
                BG = New Image
                BG.Path = ""
                BG.Index = -1
                BGM = New Music
                BGM.Path = ""
                BGM.Handle = -1
                WordType = -1
                ReDim CG(10)
                ReDim Choices(100)

                IsAniIn = True : IsAniOut = True
                AniTime = {0, 0}
                ReDim AniCG(10)
                ReDim AniChoice(100)
            End Sub

            Public Sub InitAnimation()
                IsAniIn = True : IsAniOut = True
                AniTime = {0, 0}
                IsBuilt = False
            End Sub

            Public Function AnimateIn(ByRef FormScene As Scene) As Boolean
                If Not Me Is Nothing Then
                    If IsAniIn Then
                        AniBG = New DxVB.AniObject(BG.Texture, 0)
                        AniCursor = New DxVB.AniObject(AVGCore.Cursor, 16)
                        For i = 0 To 10 Step 1
                            AniCG(i) = New DxVB.AniObject(CG(i).IMG.Texture, 16)
                        Next i
                        For i = 0 To 100 Step 1
                            AniChoice(i) = New DxVB.AniObject(Choices(i).Texture, 16)
                        Next i
                        IsAniIn = False
                    End If

                    If Not BG.Texture.Handle = -1 Then
                        If BG.Path = FormScene.BG.Path Then
                            DxVB.DrawPic(BG.Texture, 960, 540)
                        Else
                            AniBG.FadeIn(960, 540, AniTime(0), 16)
                        End If
                    End If
                    For i = 0 To CG.GetUpperBound(0) Step 1
                        If Not CG(i).IMG.Texture.Handle = -1 Then
                            If CG(i).IMG.Path = FormScene.CG(i).IMG.Path And CG(i).Location = FormScene.CG(i).Location Then
                                DxVB.DrawPic(CG(i).IMG.Texture, CG(i).Location, 800)
                            Else
                                AniCG(i).FadeIn(CG(i).Location, 800, AniTime(0), 16)
                            End If
                        End If
                    Next i
                    Select Case WordType
                        Case WordType.Comment
                            For i = 0 To Choices.GetUpperBound(0) Step 1
                                If Not Choices(i).Texture.Handle = -1 Then
                                    If Choices(i).Context = FormScene.Choices(i).Context Then
                                        DxVB.DrawPic(Choices(i).Texture, 960, 500 + i * 50)
                                    Else
                                        AniChoice(i).FadeIn(960, 500 + i * 50, AniTime(0), 16)
                                    End If
                                    AniCursor.FadeIn(120, 490 + AVGCore.AVGControl * 50, AniTime(0), 16)
                                End If
                            Next i
                        Case WordType.GameRun
                            For i = 0 To Choices.GetUpperBound(0) Step 1
                                If Not Choices(i).Texture.Handle = -1 Then
                                    If Choices(i).Context = FormScene.Choices(i).Context Then
                                        DxVB.DrawPic(Choices(i).Texture, 960, 400 + i * 100)
                                    Else
                                        AniChoice(i).FadeIn(960, 400 + i * 100, AniTime(0), 16)
                                    End If
                                End If
                            Next i
                        Case WordType.Loading
                            For i = 0 To Choices.GetUpperBound(0) Step 1
                                If Not Choices(i).Texture.Handle = -1 Then
                                    AniChoice(i).FadeIn(960, 100 + i * 50, AniTime(0), 16)
                                    AniCursor.FadeIn(120, 90 + AVGCore.AVGControl * 50, AniTime(0), 16)
                                End If
                            Next i
                        Case WordType.Saving
                            For i = 0 To Choices.GetUpperBound(0) Step 1
                                If Not Choices(i).Texture.Handle = -1 Then
                                    If Choices(i).Context = FormScene.Choices(i).Context Then
                                        DxVB.DrawPic(Choices(i).Texture, 960, 100 + i * 100)
                                    Else
                                        AniChoice(i).FadeIn(960, 100 + i * 100, AniTime(0), 16)
                                    End If
                                    AniCursor.FadeIn(120, 90 + AVGCore.AVGControl * 100, AniTime(0), 16)
                                End If
                            Next i
                        Case WordType.Script
                            For i = 0 To Choices.GetUpperBound(0) Step 1
                                If Not Choices(i).Texture.Handle = -1 Then
                                    If Choices(i).Context = FormScene.Choices(i).Context Then
                                        DxVB.DrawPic(Choices(i).Texture, 960, 600 + i * 50)
                                    Else
                                        AniChoice(i).FadeIn(960, 600 + i * 50, AniTime(0), 16)
                                    End If
                                End If
                            Next i
                        Case WordType.Title
                            For i = 0 To Choices.GetUpperBound(0) Step 1
                                If Not Choices(i).Texture.Handle = -1 Then
                                    If Choices(i).Context = FormScene.Choices(i).Context Then
                                        DxVB.DrawPic(Choices(i).Texture, 960, 100 + i * 100)
                                    Else
                                        AniChoice(i).FadeIn(960, 100 + i * 100, AniTime(0), 16)
                                    End If
                                    AniCursor.FadeIn(120, 90 + AVGCore.AVGControl * 100, AniTime(0), 16)
                                End If
                            Next i
                    End Select

                    AniTime(0) += 1
                    If AniTime(0) > 32 Then Return True

                End If
                Return False
            End Function

            Public Function AnimateOut(ByRef NextScene As Scene) As Boolean
                If Not Me Is Nothing Then
                    If IsAniOut Then
                        AniBG = New DxVB.AniObject(BG.Texture, 16)
                        AniCursor = New DxVB.AniObject(AVGCore.Cursor, 0)
                        For i = 0 To 10 Step 1
                            AniCG(i) = New DxVB.AniObject(CG(i).IMG.Texture, 0)
                        Next i
                        For i = 0 To 100 Step 1
                            AniChoice(i) = New DxVB.AniObject(Choices(i).Texture, 0)
                        Next i
                        IsAniOut = False
                    End If

                    If Not BG.Texture.Handle = -1 Then
                        If BG.Path = NextScene.BG.Path Then
                            DxVB.DrawPic(BG.Texture, 960, 540)
                        Else
                            AniBG.FadeOut(960, 540, AniTime(1), 16)
                        End If
                    End If
                    For i = 0 To CG.GetUpperBound(0) Step 1
                        If Not CG(i).IMG.Texture.Handle = -1 Then
                            If CG(i).IMG.Path = NextScene.CG(i).IMG.Path And CG(i).Location = NextScene.CG(i).Location Then
                                DxVB.DrawPic(CG(i).IMG.Texture, CG(i).Location, 800)
                            Else
                                AniCG(i).FadeOut(CG(i).Location, 800, AniTime(1), 16)
                            End If
                        End If
                    Next i
                    Select Case WordType
                        Case WordType.Comment
                            For i = 0 To Choices.GetUpperBound(0) Step 1
                                If Not Choices(i).Texture.Handle = -1 Then
                                    If Choices(i).Context = NextScene.Choices(i).Context Then
                                        DxVB.DrawPic(Choices(i).Texture, 960, 500 + i * 50)
                                    Else
                                        AniChoice(i).FadeOut(960, 500 + i * 50, AniTime(1), 16)
                                    End If
                                    AniCursor.FadeOut(120, 490 + AVGCore.AVGControl * 50, AniTime(1), 16)
                                End If
                            Next i
                        Case WordType.GameRun
                            For i = 0 To Choices.GetUpperBound(0) Step 1
                                If Not Choices(i).Texture.Handle = -1 Then
                                    If Choices(i).Context = NextScene.Choices(i).Context Then
                                        DxVB.DrawPic(Choices(i).Texture, 960, 400 + i * 100)
                                    Else
                                        AniChoice(i).FadeOut(960, 400 + i * 100, AniTime(1), 16)
                                    End If
                                End If
                            Next i
                        Case WordType.Loading
                            For i = 0 To Choices.GetUpperBound(0) Step 1
                                If Not Choices(i).Texture.Handle = -1 Then
                                    AniChoice(i).FadeOut(960, 100 + i * 50, AniTime(1), 16)
                                    AniCursor.FadeOut(120, 90 + AVGCore.AVGControl * 50, AniTime(1), 16)
                                End If
                            Next i
                        Case WordType.Saving
                            For i = 0 To Choices.GetUpperBound(0) Step 1
                                If Not Choices(i).Texture.Handle = -1 Then
                                    If Choices(i).Context = NextScene.Choices(i).Context Then
                                        DxVB.DrawPic(Choices(i).Texture, 960, 100 + i * 100)
                                    Else
                                        AniChoice(i).FadeOut(960, 100 + i * 100, AniTime(1), 16)
                                    End If
                                    AniCursor.FadeOut(120, 90 + AVGCore.AVGControl * 100, AniTime(1), 16)
                                End If
                            Next i
                        Case WordType.Script
                            For i = 0 To Choices.GetUpperBound(0) Step 1
                                If Not Choices(i).Texture.Handle = -1 Then
                                    If Choices(i).Context = NextScene.Choices(i).Context Then
                                        DxVB.DrawPic(Choices(i).Texture, 960, 600 + i * 50)
                                    Else
                                        AniChoice(i).FadeOut(960, 600 + i * 50, AniTime(1), 16)
                                    End If
                                End If
                            Next i
                        Case WordType.Title
                            For i = 0 To Choices.GetUpperBound(0) Step 1
                                If Not Choices(i).Texture.Handle = -1 Then
                                    If Choices(i).Context = NextScene.Choices(i).Context Then
                                        DxVB.DrawPic(Choices(i).Texture, 960, 100 + i * 100)
                                    Else
                                        AniChoice(i).FadeOut(960, 100 + i * 100, AniTime(1), 16)
                                    End If
                                    AniCursor.FadeOut(120, 90 + AVGCore.AVGControl * 100, AniTime(1), 16)
                                End If
                            Next i
                    End Select

                    AniTime(1) += 1
                    If AniTime(1) > 32 Then Return True

                End If
                Return False
            End Function

            Public Sub Build()
                If Not Me Is Nothing Then
                    If Not BG.Texture.Handle = -1 Then
                        DxVB.DrawPic(BG.Texture, 960, 540)
                    End If
                    For i = 0 To CG.GetUpperBound(0) Step 1
                        If Not CG(i).IMG.Texture.Handle = -1 Then
                            DxVB.DrawPic(CG(i).IMG.Texture, CG(i).Location, 800)
                        End If
                    Next i
                    Select Case WordType
                        Case WordType.Comment
                            For i = 0 To Choices.GetUpperBound(0) Step 1
                                If Not Choices(i).Texture.Handle = -1 Then
                                    DxVB.DrawPic(Choices(i).Texture, 960, 500 + i * 50)
                                    DxVB.DrawPic(AVGCore.Cursor, 120, 490 + AVGCore.AVGControl * 50)
                                End If
                            Next i
                        Case WordType.GameRun
                            For i = 0 To Choices.GetUpperBound(0) Step 1
                                If Not Choices(i).Texture.Handle = -1 Then
                                    DxVB.DrawPic(Choices(i).Texture, 960, 400 + i * 100)
                                End If
                            Next i
                        Case WordType.Loading
                            For i = 0 To Choices.GetUpperBound(0) Step 1
                                If Not Choices(i).Texture.Handle = -1 Then
                                    DxVB.DrawPic(Choices(i).Texture, 960, 100 + i * 50)
                                    DxVB.DrawPic(AVGCore.Cursor, 120, 90 + AVGCore.AVGControl * 50)
                                End If
                            Next i
                        Case WordType.Saving
                            For i = 0 To Choices.GetUpperBound(0) Step 1
                                If Not Choices(i).Texture.Handle = -1 Then
                                    DxVB.DrawPic(Choices(i).Texture, 960, 100 + i * 100)
                                    DxVB.DrawPic(AVGCore.Cursor, 120, 90 + AVGCore.AVGControl * 100)
                                End If
                            Next i
                        Case WordType.Script
                            For i = 0 To Choices.GetUpperBound(0) Step 1
                                If Not Choices(i).Texture.Handle = -1 Then
                                    DxVB.DrawPic(Choices(i).Texture, 960, 600 + i * 50)
                                End If
                            Next i
                        Case WordType.Title
                            For i = 0 To Choices.GetUpperBound(0) Step 1
                                If Not Choices(i).Texture.Handle = -1 Then
                                    DxVB.DrawPic(Choices(i).Texture, 960, 100 + i * 100)
                                    DxVB.DrawPic(AVGCore.Cursor, 120, 90 + AVGCore.AVGControl * 100)
                                End If
                            Next i
                    End Select

                    If Not IsBuilt Then
                        DxVB.PlayMusic(BGM)
                    End If

                    IsBuilt = True
                End If
            End Sub

        End Class

        Public Class AVGCore
            ' AVG的核心类
            ' 有些成员函数只能在AVG类

            Public Shared AVGControl As Integer
            Private SceneStep, SceneStepTmp As Integer
            Public Shared Cursor As DxVB.DxImage
            Private Scenes() As Scene
            Private IsKeyDown, IsKeyPress As Boolean
            Private KeyPressTime As Integer

            Public Sub New(ByVal CursorPath As String)
                Cursor = DxVB.LoadTexture(CursorPath)
                AVGControl = 0 : SceneStep = 0 : SceneStepTmp = 0
                IsKeyDown = False : IsKeyPress = False : KeyPressTime = 0
            End Sub

            Public Sub LoadScenes(ByRef Scene() As Scene)
                If Scenes Is Nothing Then
                    Scenes = Scene
                Else
                    For i = 0 To Scene.Length - 1 Step 1
                        If Not Scene(i) Is Nothing Then Scenes(i) = Scene(i)
                    Next i
                End If
            End Sub

            Public Function ResourcesLoad() As Boolean
                Dim FlagTmp As Boolean = True
                For i = 0 To Scenes.GetUpperBound(0) Step 1
                    If Not Scenes(i) Is Nothing Then
                        FlagTmp = FlagTmp And DxVB.LoadTexture(Scenes(i).BG.Path, Scenes(i).BG.Texture)
                        Scenes(i).BGM.Handle = DxVB.LoadMuisc(Scenes(i).BGM.Path)
                        If FlagTmp = False Then Return False
                        For j = 0 To Scenes(i).CG.GetUpperBound(0) Step 1
                            If Not Scenes(i).CG(j).IMG.Path = Nothing Then
                                FlagTmp = FlagTmp And DxVB.LoadTexture(Scenes(i).CG(j).IMG.Path, Scenes(i).CG(j).IMG.Texture)
                                If FlagTmp = False Then Return False
                            End If
                        Next j
                    End If
                Next i

                For i = 0 To Scenes.GetUpperBound(0) Step 1
                    If Not Scenes(i) Is Nothing Then
                        Scenes(i).BGM.Handle = DxVB.LoadMuisc(Scenes(i).BGM.Path)
                    End If
                Next i
                LoadString()
                Return True
            End Function

            Private Sub LoadString()
                For i = 0 To Scenes.GetUpperBound(0) Step 1
                    If Not Scenes(i) Is Nothing Then
                        Select Case Scenes(i).WordType
                            Case AVG.WordType.Comment
                                For k = 0 To Scenes(i).Choices.GetUpperBound(0) Step 1
                                    If Not Scenes(i).Choices(k).Context = Nothing Then
                                        Scenes(i).Choices(k).Texture = DxVB.LoadString(Scenes(i).Choices(k).Context, System.Drawing.Brushes.White, 40)
                                    End If
                                Next k
                            Case AVG.WordType.GameRun
                                For k = 0 To Scenes(i).Choices.GetUpperBound(0) Step 1
                                    If Not Scenes(i).Choices(k).Context = Nothing Then
                                        Scenes(i).Choices(k).Texture = DxVB.LoadString(Scenes(i).Choices(k).Context, System.Drawing.Brushes.Cyan, 40)
                                    End If
                                Next k
                            Case AVG.WordType.Loading
                                For k = 0 To Scenes(i).Choices.GetUpperBound(0) Step 1
                                    If Not Scenes(i).Choices(k).Context = Nothing Then
                                        Scenes(i).Choices(k).Texture = DxVB.LoadString(Scenes(i).Choices(k).Context, System.Drawing.Brushes.White, 40)
                                    End If
                                Next k
                            Case AVG.WordType.Saving
                                For k = 0 To Scenes(i).Choices.GetUpperBound(0) Step 1
                                    If Not Scenes(i).Choices(k).Context = Nothing Then
                                        Scenes(i).Choices(k).Texture = DxVB.LoadString(Scenes(i).Choices(k).Context, System.Drawing.Brushes.White, 40)
                                    End If
                                Next k
                            Case AVG.WordType.Script
                                For k = 0 To Scenes(i).Choices.GetUpperBound(0) Step 1
                                    If Not Scenes(i).Choices(k).Context = Nothing Then
                                        Scenes(i).Choices(k).Texture = DxVB.LoadString(Scenes(i).Choices(k).Context, System.Drawing.Brushes.White, 40)
                                    End If
                                Next k
                            Case AVG.WordType.Title
                                For k = 0 To Scenes(i).Choices.GetUpperBound(0) Step 1
                                    If Not Scenes(i).Choices(k).Context = Nothing Then
                                        Scenes(i).Choices(k).Texture = DxVB.LoadString(Scenes(i).Choices(k).Context, System.Drawing.Brushes.White, 40)
                                    End If
                                Next k
                        End Select
                    End If
                Next i
            End Sub

            Public Sub Work()
                If SceneStepTmp = SceneStep Then
                    Scenes(SceneStep).Build()
                    Control()
                Else
                    If Scenes(SceneStep).IsBuilt Then Scenes(SceneStep).InitAnimation()
                    If Scenes(SceneStepTmp).AnimateOut(Scenes(SceneStep)) Then
                        AVGControl = 0
                        If Scenes(SceneStep).AnimateIn(Scenes(SceneStepTmp)) Then
                            SceneStepTmp = SceneStep
                        End If
                    End If
                End If

#If _DEBUG_ = 1 Then
                DxVB.DrawString("SceneStep = " & SceneStep, 100, 100, 20)
                DxVB.DrawString("AVGControl = " & AVGControl, 100, 120, 20)
                DxVB.DrawString("IsKeyPress = " & IsKeyPress, 100, 140, 20)
#End If

            End Sub

            Private Sub Control()
                If Not Scenes(SceneStep) Is Nothing Then
                    If Scenes(SceneStep).WordType = AVG.WordType.Loading Or Scenes(SceneStep).WordType = AVG.WordType.Saving Or _
                        Scenes(SceneStep).WordType = AVG.WordType.Comment Or Scenes(SceneStep).WordType = AVG.WordType.Title Then

                        Dim UpTmp As Integer = 0
                        Do
                            UpTmp = UpTmp + 1
                        Loop Until Scenes(SceneStep).Choices(UpTmp).Context = Nothing
                        UpTmp = UpTmp - 1

                        If GetKey(Keys.KeyUP) Then
                            AVGControl = AVGControl - 1
                            DxVB.PlaySE("DATAs\Audio\select.wav")
                            DxVBDLL.DX.WaitTimer(100)
                            'If IsKeyPress Then
                            '    DxVBDLL.DX.WaitTimer(100)
                            'Else
                            '    If Not IsKeyDown Then
                            '        KeyPressTime = System.Environment.TickCount
                            '        IsKeyDown = True
                            '    End If
                            '    If System.Environment.TickCount - KeyPressTime > 100 Then
                            '        IsKeyPress = True
                            '        KeyPressTime = 0
                            '        DxVBDLL.DX.WaitTimer(500)
                            '    End If
                            'End If
                        ElseIf GetKey(Keys.KeyLEFT) Then
                            AVGControl = AVGControl - 1
                            DxVB.PlaySE("DATAs\Audio\select.wav")
                            DxVBDLL.DX.WaitTimer(100)
                            'If IsKeyPress Then
                            '    DxVBDLL.DX.WaitTimer(100)
                            'Else
                            '    If Not IsKeyDown Then
                            '        KeyPressTime = System.Environment.TickCount
                            '        IsKeyDown = True
                            '    End If
                            '    If System.Environment.TickCount - KeyPressTime > 100 Then
                            '        IsKeyPress = True
                            '        KeyPressTime = 0
                            '        DxVBDLL.DX.WaitTimer(500)
                            '    End If
                            'End If
                        End If

                        If GetKey(Keys.KeyDOWN) Then
                            AVGControl = AVGControl + 1
                            DxVB.PlaySE("DATAs\Audio\select.wav")
                            DxVBDLL.DX.WaitTimer(100)
                            'If IsKeyPress Then
                            '    DxVBDLL.DX.WaitTimer(100)
                            'Else
                            '    If Not IsKeyDown Then
                            '        KeyPressTime = System.Environment.TickCount
                            '        IsKeyDown = True
                            '    End If
                            '    If System.Environment.TickCount - KeyPressTime > 100 Then
                            '        IsKeyPress = True
                            '        KeyPressTime = 0
                            '        DxVBDLL.DX.WaitTimer(500)
                            '    End If
                            'End If
                        ElseIf GetKey(Keys.KeyRIGHT) Then
                            AVGControl = AVGControl + 1
                            DxVB.PlaySE("DATAs\Audio\select.wav")
                            DxVBDLL.DX.WaitTimer(100)
                            'If IsKeyPress Then
                            '    DxVBDLL.DX.WaitTimer(100)
                            'Else
                            '    If Not IsKeyDown Then
                            '        KeyPressTime = System.Environment.TickCount
                            '        IsKeyDown = True
                            '    End If
                            '    If System.Environment.TickCount - KeyPressTime > 100 Then
                            '        IsKeyPress = True
                            '        KeyPressTime = 0
                            '        DxVBDLL.DX.WaitTimer(500)
                            '    End If
                            'End If
                        End If

                        'If Not (GetKey(Keys.KeyUP) Or GetKey(Keys.KeyDOWN) Or GetKey(Keys.KeyLEFT) Or GetKey(Keys.KeyRIGHT)) Then
                        '    IsKeyDown = False : IsKeyPress = False
                        'End If


                        If AVGControl < 0 Then AVGControl = UpTmp
                        If AVGControl > UpTmp Then AVGControl = 0

                        If GetKey(Keys.KeyZ) Then
                            SceneStep = Scenes(SceneStep).Choices(AVGControl).JumpSceneIndex
                            DxVB.PlaySE("DATAs\Audio\ok.wav")
                            DxVBDLL.DX.WaitTimer(100)
                        End If
                    Else
                        If GetKey(Keys.KeyZ) Then
                            SceneStep = SceneStep + 1
                            DxVB.PlaySE("DATAs\Audio\ok.wav")
                            DxVBDLL.DX.WaitTimer(100)
                        End If
                    End If
                End If

            End Sub

        End Class

        Public Shared Function LoadConfig(ByVal Path As String) As Config
            Return LoadConfig(False, Path)
        End Function

        Public Shared Function LoadConfig(ByVal IsFullPath As Boolean, ByVal Path As String) As Config
            Dim TmpConfig As New Config
            Dim Reader As IO.StreamReader
            Dim TmpString() As String : Dim TmpHead, ScriptCount As Integer
            If IsFullPath Then
                Reader = New IO.StreamReader(Path)
            Else
                Reader = New IO.StreamReader(Environment.CurrentDirectory & "\" & Path)
            End If

            ReDim TmpConfig.ScriptPath(128)
            ScriptCount = 0 : TmpConfig.ScriptCount = 0

            While Not Reader.EndOfStream
                TmpString = Reader.ReadLine.Split({CChar(Space(1)), CChar(vbCr), CChar(vbTab)})
                TmpHead = 0
                Try
                    While TmpString(TmpHead) = ""
                        TmpHead += 1
                    End While
                Catch ex As Exception
                    GoTo Bottom
                End Try

                Select Case TmpString(TmpHead)
                    Case "def.name", "游戏名"
                        For i = TmpHead + 1 To TmpString.Length - 1
                            If i < TmpString.Length - 1 Then
                                TmpConfig.GameName += TmpString(i) & " "
                            Else
                                TmpConfig.GameName += TmpString(i)
                            End If
                        Next i
                    Case "def.save", "存档"
                        For i = TmpHead + 1 To TmpString.Length - 1
                            If i < TmpString.Length - 1 Then
                                TmpConfig.SavePath += TmpString(i) & " "
                            Else
                                TmpConfig.SavePath += TmpString(i)
                            End If
                        Next i
                    Case "def.script", "脚本"
                        TmpConfig.ScriptPath(ScriptCount) = ""
                        For i = TmpHead + 1 To TmpString.Length - 1
                            If i < TmpString.Length - 1 Then
                                TmpConfig.ScriptPath(ScriptCount) += TmpString(i) & " "
                            Else
                                TmpConfig.ScriptPath(ScriptCount) += TmpString(i)
                            End If
                        Next i
                        ScriptCount += 1
                        TmpConfig.ScriptCount = ScriptCount
                End Select
Bottom:
            End While

            Return TmpConfig
        End Function

        Public Shared Function LoadScript(ByVal Path As String) As Scene()
            Dim Scenes() As Scene
            Dim SceneIndexTmp, CGIndexTmp, ChoiceAutoIndexTmp As Integer
            Dim FullPath As String = Environment.CurrentDirectory & "\" & Path
            Dim Reader As IO.StreamReader
            Try
                Reader = New IO.StreamReader(FullPath)
            Catch ex As Exception
                Dim MsgBox As New Message
                MsgBox.MsgShow("Error", "Config file contains error.")
                MsgBox.Close()
                Return Nothing
            End Try

            Dim TmpString() As String : Dim TmpHead As Integer
            ReDim Scenes(4096)

            While Not Reader.EndOfStream
                TmpString = Reader.ReadLine.Split({CChar(Space(1)), CChar(vbCr), CChar(vbTab)})
                TmpHead = 0
                Try
                    While TmpString(TmpHead) = ""
                        TmpHead += 1
                    End While
                Catch ex As Exception
                    GoTo Bottom
                End Try

                Select Case TmpString(TmpHead)
                    Case "开始定义场景", "defsc", "def.scene"
                        SceneIndexTmp = CInt(TmpString(TmpHead + 1))
                        Scenes(SceneIndexTmp) = New Scene()
                        Scenes(SceneIndexTmp).SceneIndex = SceneIndexTmp
                    Case "结束定义场景", "endsc", "end.scene"
                        SceneIndexTmp = -1
                    Case "背景", "backg", "set.back"
                        Scenes(SceneIndexTmp).BG.Path = TmpString(TmpHead + 1)
                        Scenes(SceneIndexTmp).BG.Index = SceneIndexTmp
                    Case "音乐", "music", "set.bgm"
                        Scenes(SceneIndexTmp).BGM.Path = TmpString(TmpHead + 1)
                    Case "开始定义立绘", "defcg", "def.cg"
                        CGIndexTmp = CInt(TmpString(TmpHead + 1))
                        Scenes(SceneIndexTmp).CG(CGIndexTmp).IMG.Index = CGIndexTmp
                    Case "结束定义立绘", "endcg", "end.cg"
                        CGIndexTmp = -1
                    Case "立绘地址", "cgloc", "set.path"
                        Scenes(SceneIndexTmp).CG(CGIndexTmp).IMG.Path = TmpString(TmpHead + 1)
                    Case "立绘位置", "cgpos", "set.pos"
                        Scenes(SceneIndexTmp).CG(CGIndexTmp).Location = CSng(TmpString(TmpHead + 1))
                    Case "开始定义文字", "defwd", "def.word"
                        ChoiceAutoIndexTmp = 0
                        Select Case TmpString(TmpHead + 1)
                            Case "对话", "comm", "type.comm"
                                Scenes(SceneIndexTmp).WordType = WordType.Comment
                            Case "读档", "read", "type.read"
                                Scenes(SceneIndexTmp).WordType = WordType.Loading
                            Case "存档", "save", "type.save"
                                Scenes(SceneIndexTmp).WordType = WordType.Saving
                            Case "旁白", "scpt", "type.script"
                                Scenes(SceneIndexTmp).WordType = WordType.Script
                            Case "标题", "titl", "type.title"
                                Scenes(SceneIndexTmp).WordType = WordType.Title
                            Case "启动", "stup", "type.start"
                                Scenes(SceneIndexTmp).WordType = WordType.GameRun
                        End Select
                    Case "选择", "cword", "set.word"
                        For i = TmpHead + 1 To TmpString.Length - 1
                            If i < TmpString.Length - 1 Then
                                Scenes(SceneIndexTmp).Choices(ChoiceAutoIndexTmp).Context += TmpString(i) & " "
                            Else
                                Scenes(SceneIndexTmp).Choices(ChoiceAutoIndexTmp).Context += TmpString(i)
                            End If
                        Next i
                    Case "跳转", "jmpto", "set.jump"
                        Scenes(SceneIndexTmp).Choices(ChoiceAutoIndexTmp).JumpSceneIndex = CInt(TmpString(TmpHead + 1))
                        ChoiceAutoIndexTmp = ChoiceAutoIndexTmp + 1
                    Case "结束定义文字", "endwd", "end.word"
                        ChoiceAutoIndexTmp = -1
                End Select

Bottom:
            End While

            Return Scenes
        End Function

        Public Shared Function SaveGame(ByVal SaveScene As Scene)

        End Function

        '        Public Shared Function LoadScript(ByVal IsFullPath As Boolean, ByVal ScriptPath As String) As Scene()
        '            Dim SceneTmp(4096) As Scene
        '            Dim SceneIndexTmp As Integer = Nothing
        '            Dim CGIndexTmp As Integer = Nothing
        '            Dim ChoiceAutoIndexTmp As Integer = Nothing
        '            Dim ScriptFullPath As String = ""
        '            Dim AppStartPath As String = Environment.CurrentDirectory & "\"        '获取当前位置

        '            If IsFullPath Then        '确定脚本是在程序目录下被代码自动调用，还是手动选择调用
        '                ScriptFullPath = ScriptPath
        '            Else
        '                ScriptFullPath = AppStartPath & ScriptPath
        '            End If

        '            Dim Head As ScriptHead = ScriptHead.NULL        '定义脚本头
        '            Dim LineBreak As Integer = 10        '设定换行符
        '            Dim Size As Long = Microsoft.VisualBasic.FileIO.FileSystem.GetFileInfo(ScriptFullPath).Length     '获取文件大小
        '            Dim CookedString As String = ""     '定义已处理字符串变量
        '            Dim WordTmp As String = ""        '定义选择文字临时变量
        '            Dim Strings As String = Microsoft.VisualBasic.FileIO.FileSystem.ReadAllText(ScriptFullPath, System.Text.Encoding.UTF8)     '读取文件
        '            Dim Steps As Integer = Strings.Length     '获取字符串长度
        '            Dim x As Integer = 0, y As Integer = 0      '定义循环控制变量

        '            Do Until x >= Steps     '检测是否读到文件结尾
        '                Do Until Strings.Chars(x) = " " Or Strings.Chars(x) = Chr(LineBreak)    '检测是否读到空格或行结尾,并避开回车符
        '                    CookedString = CookedString & Strings.Chars(x)     '存入已处理字符串变量数组
        '                    x = x + 1     '下一个字符
        '                    If x = Steps Then                '这个String的编号（String.Chars(Integer)）是从0开始的，这里防止数组越界。
        '                        GoTo EndFlag                 '即字符数为Num的字符串其编号为0到Num-1，上面的+1实际已经超出界限，这里检
        '                    End If                           '测并跳出。
        '                Loop
        '                Select Case Head
        '                    Case ScriptHead.NULL
        '                        Select Case CookedString
        '                            Case "开始定义场景", "defsc", "def.scene"
        '                                Head = ScriptHead.StartSceneDim
        '                            Case "结束定义场景", "endsc", "end.scene"
        '                                Head = ScriptHead.EndSceneDim
        '                            Case "背景", "backg", "set.back"
        '                                Head = ScriptHead.Background
        '                            Case "音乐", "music", "set.bgm"
        '                                Head = ScriptHead.BGM
        '                            Case "开始定义立绘", "defcg", "def.cg"
        '                                Head = ScriptHead.StartCGDim
        '                            Case "结束定义立绘", "endcg", "end.cg"
        '                                Head = ScriptHead.EndCGDim
        '                            Case "立绘地址", "cgloc", "set.path"
        '                                Head = ScriptHead.CGPath
        '                            Case "立绘位置", "cgpos", "set.pos"
        '                                Head = ScriptHead.CGLocation
        '                            Case "开始定义文字", "defwd", "def.word"
        '                                Head = ScriptHead.StartWordDim
        '                            Case "选择", "cword", "set.word"
        '                                Head = ScriptHead.Choice
        '                            Case "跳转", "jmpto", "set.jump"
        '                                Head = ScriptHead.Jump
        '                            Case "结束定义文字", "endwd", "end.word"
        '                                Head = ScriptHead.EndWordDim
        '                        End Select
        '                    Case ScriptHead.StartSceneDim
        '                        SceneIndexTmp = CInt(CookedString)
        '                        SceneTmp(SceneIndexTmp) = New Scene
        '                        SceneTmp(SceneIndexTmp).SceneIndex = SceneIndexTmp
        '                        Head = ScriptHead.NULL
        '                    Case ScriptHead.EndSceneDim
        '                        SceneTmp = Nothing
        '                        Head = ScriptHead.NULL
        '                    Case ScriptHead.Background
        '                        SceneTmp(SceneIndexTmp).BG.Path = AppStartPath & CookedString
        '                        SceneTmp(SceneIndexTmp).BG.Index = SceneIndexTmp
        '                        Head = ScriptHead.NULL
        '                    Case ScriptHead.BGM
        '                        SceneTmp(SceneIndexTmp).BGM = AppStartPath & CookedString
        '                        Head = ScriptHead.NULL
        '                    Case ScriptHead.StartCGDim
        '                        CGIndexTmp = CInt(CookedString)
        '                        SceneTmp(SceneIndexTmp).CG(CGIndexTmp).IMG.Index = CGIndexTmp
        '                        Head = ScriptHead.NULL
        '                    Case ScriptHead.EndCGDim
        '                        CGIndexTmp = Nothing
        '                        Head = ScriptHead.NULL
        '                    Case ScriptHead.CGPath
        '                        SceneTmp(SceneIndexTmp).CG(CGIndexTmp).IMG.Path = AppStartPath & CookedString
        '                        Head = ScriptHead.NULL
        '                    Case ScriptHead.CGLocation
        '                        SceneTmp(SceneIndexTmp).CG(CGIndexTmp).Location = CSng(CookedString)
        '                        Head = ScriptHead.NULL
        '                    Case ScriptHead.StartWordDim
        '                        ChoiceAutoIndexTmp = 0
        '                        Select Case CookedString
        '                            Case "对话" & vbCr, "comm" & vbCr, "type.comm" & vbCr
        '                                SceneTmp(SceneIndexTmp).WordType = WordType.Comment
        '                            Case "读档" & vbCr, "read" & vbCr, "type.read" & vbCr
        '                                SceneTmp(SceneIndexTmp).WordType = WordType.Loading
        '                            Case "存档" & vbCr, "save" & vbCr, "type.save" & vbCr
        '                                SceneTmp(SceneIndexTmp).WordType = WordType.Saving
        '                            Case "旁白" & vbCr, "scpt" & vbCr, "type.script" & vbCr
        '                                SceneTmp(SceneIndexTmp).WordType = WordType.Script
        '                            Case "标题" & vbCr, "titl" & vbCr, "type.title" & vbCr
        '                                SceneTmp(SceneIndexTmp).WordType = WordType.Title
        '                            Case "启动" & vbCr, "stup" & vbCr, "type.start" & vbCr
        '                                SceneTmp(SceneIndexTmp).WordType = WordType.GameRun
        '                        End Select
        '                        Head = ScriptHead.NULL
        '                    Case ScriptHead.Choice
        '                        WordTmp = WordTmp & CookedString & " "
        '                        If Strings.Chars(x) = Chr(LineBreak) Then
        '                            SceneTmp(SceneIndexTmp).Choices(ChoiceAutoIndexTmp).Context = WordTmp
        '                            WordTmp = ""
        '                            Head = ScriptHead.NULL
        '                        End If
        '                    Case ScriptHead.Jump
        '                        SceneTmp(SceneIndexTmp).Choices(ChoiceAutoIndexTmp).JumpSceneIndex = CInt(CookedString)
        '                        ChoiceAutoIndexTmp = ChoiceAutoIndexTmp + 1
        '                        Head = ScriptHead.NULL
        '                    Case ScriptHead.EndWordDim
        '                        ChoiceAutoIndexTmp = 0
        '                        Head = ScriptHead.NULL
        '                End Select

        '                CookedString = ""

        '                If Strings.Chars(x) = " " Or Strings.Chars(x) = Chr(LineBreak) Then     '检测是否读到行结尾
        '                    x = x + 1
        '                End If

        '            Loop
        'EndFlag:
        '            Return SceneTmp
        '        End Function

        'Public Shared Sub BuildScene(ByVal Scene As Scene)
        '    If Not Scene Is Nothing Then
        '        If Not Scene.BG.Path = Nothing Or Not Scene.BG.Path = "" Then
        '            DxVB.DrawPic(ImageArray(IMAGE_BG, Scene.BG.Index), 960, 540)
        '        End If
        '        For i = 0 To Scene.CG.GetUpperBound(0) Step 1
        '            If Not Scene.CG(i).IMG.Path = Nothing Or Not Scene.CG(i).IMG.Path = "" Then
        '                DxVB.DrawPic(ImageArray(IMAGE_CG, Scene.CG(i).IMG.Index), Scene.CG(i).Location, 800)
        '            End If
        '        Next i
        '        Select Case Scene.WordType
        '            Case WordType.Comment
        '                For i = 0 To Scene.Choices.GetUpperBound(0) Step 1
        '                    If Not Scene.Choices(i).Handle = Nothing Then
        '                        'DxLibDLL.DX.DrawString(100, 900 + i * 20, Scene.Choices(i).Handle, 16777215)
        '                        DxVB.DrawString(100, 500 + i * 100, Scene.Choices(i).Handle)
        '                        'DxLibDLL.DX.DrawString(100, 900 + AVGControl * 20, Scene.Choices(AVGControl).Context, 65535)
        '                        DxVB.DrawPic(ImageArray(IMAGE_BULLET, 0), 100, 550 + AVGControl * 100)
        '                    End If
        '                Next i
        '            Case WordType.GameRun
        '                For i = 0 To Scene.Choices.GetUpperBound(0) Step 1
        '                    If Not Scene.Choices(i).Handle = Nothing Then
        '                        'DxLibDLL.DX.DrawString(100, 400 + i * 20, Scene.Choices(i).Handle, 16777215)
        '                        DxVB.DrawString(100, 400 + i * 200, Scene.Choices(i).Handle)
        '                    End If
        '                Next i
        '            Case WordType.Loading
        '                For i = 0 To Scene.Choices.GetUpperBound(0) Step 1
        '                    If Not Scene.Choices(i).Handle = Nothing Then
        '                        'DxLibDLL.DX.DrawString(100, 100 + i * 20, Scene.Choices(i).Handle, 16777215)
        '                        DxVB.DrawString(100, 100 + i * 100, Scene.Choices(i).Handle)
        '                        'DxLibDLL.DX.DrawString(100, 100 + AVGControl * 20, Scene.Choices(AVGControl).Context, 65535)
        '                        DxVB.DrawPic(ImageArray(IMAGE_BULLET, 0), 100, 150 + AVGControl * 100)
        '                    End If
        '                Next i
        '            Case WordType.Saving
        '                For i = 0 To Scene.Choices.GetUpperBound(0) Step 1
        '                    If Not Scene.Choices(i).Handle = Nothing Then
        '                        'DxLibDLL.DX.DrawString(100, 100 + i * 20, Scene.Choices(i).Handle, 16777215)
        '                        DxVB.DrawString(100, 100 + i * 100, Scene.Choices(i).Handle)
        '                        'DxLibDLL.DX.DrawString(100, 100 + AVGControl * 20, Scene.Choices(AVGControl).Context, 65535)
        '                        DxVB.DrawPic(ImageArray(IMAGE_BULLET, 0), 100, 150 + AVGControl * 100)
        '                    End If
        '                Next i
        '            Case WordType.Script
        '                For i = 0 To Scene.Choices.GetUpperBound(0) Step 1
        '                    If Not Scene.Choices(i).Handle = Nothing Then
        '                        'DxLibDLL.DX.DrawString(100, 900 + i * 20, Scene.Choices(i).Handle, 16777215)
        '                        DxVB.DrawString(100, 600 + i * 100, Scene.Choices(i).Handle)
        '                    End If
        '                Next i
        '            Case WordType.Title
        '                For i = 0 To Scene.Choices.GetUpperBound(0) Step 1
        '                    If Not Scene.Choices(i).Handle = Nothing Then
        '                        'DxLibDLL.DX.DrawString(920, 100 + i * 20, Scene.Choices(i).Handle, 16777215)
        '                        DxVB.DrawString(800, 100 + i * 100, Scene.Choices(i).Handle)
        '                        'DxLibDLL.DX.DrawString(920, 100 + AVGControl * 20, Scene.Choices(AVGControl).Context, 65535)
        '                        DxVB.DrawPic(ImageArray(IMAGE_BULLET, 0), 800, 150 + AVGControl * 100)
        '                    End If
        '                Next i
        '        End Select

        '        If Not Scene.BGM = Nothing Then
        '            DxVB.PlayMusic(Scene.BGM)
        '        End If

        '        Scene.IsBuilt = True
        '    End If
        'End Sub

    End Class

End Class

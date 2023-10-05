Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Public Class Forgot_Password
    Dim DrawingFont As New Font("Arial", 25)
    Dim CaptchaImage As New Bitmap(140, 40)
    Dim CaptchaGraf As Graphics = Graphics.FromImage(CaptchaImage)
    Dim Alphabet As String = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz"
    Dim CaptchaString, TickRandom As String
    Dim ProcessNumber As Integer
    Dim con As New SqlConnection
    Dim password As String

    Private Function Decrypt(cipherText As String) As String
        Dim EncryptionKey As String = "0ram@1234xxxxxxxxxxtttttuuuuuiiiiio"
        cipherText = cipherText.Replace(" ", "+")
        Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
         &H65, &H64, &H76, &H65, &H64, &H65,
         &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(cipherBytes, 0, cipherBytes.Length)
                    cs.Close()
                End Using
                cipherText = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using
        Return cipherText
    End Function

    Private Sub GenerateCaptcha()
        ProcessNumber = DateTime.UtcNow.Millisecond
        If ProcessNumber < 521 Then
            ProcessNumber = ProcessNumber \ 10
            CaptchaString = Alphabet.Substring(ProcessNumber, 1)
        Else
            CaptchaString = CStr(DateTime.UtcNow.Second \ 6)
        End If
        ProcessNumber = DateTime.UtcNow.Second
        If ProcessNumber < 30 Then
            ProcessNumber = Math.Abs(ProcessNumber - 8)
            CaptchaString += Alphabet.Substring(ProcessNumber, 1)
        Else
            CaptchaString += CStr(DateTime.UtcNow.Minute \ 6)
        End If
        ProcessNumber = DateTime.UtcNow.DayOfYear
        If ProcessNumber Mod 2 = 0 Then
            ProcessNumber = ProcessNumber \ 8
            CaptchaString += Alphabet.Substring(ProcessNumber, 1)
        Else
            CaptchaString += CStr(ProcessNumber \ 37)
        End If
        TickRandom = DateTime.UtcNow.Ticks.ToString
        ProcessNumber = Val(TickRandom.Substring(TickRandom.Length - 1, 1))
        If ProcessNumber Mod 2 = 0 Then
            CaptchaString += CStr(ProcessNumber)
        Else
            ProcessNumber = Math.Abs(Int(Math.Cos(Val(TickRandom)) * 51))
            CaptchaString += Alphabet.Substring(ProcessNumber, 1)
        End If
        ProcessNumber = DateTime.UtcNow.Hour
        If ProcessNumber Mod 2 = 0 Then
            ProcessNumber = Math.Abs(Int(Math.Sin(Val(DateTime.UtcNow.Year)) * 51))
            CaptchaString += Alphabet.Substring(ProcessNumber, 1)
        Else
            CaptchaString += CStr(ProcessNumber \ 3)
        End If
        ProcessNumber = DateTime.UtcNow.Millisecond
        If ProcessNumber > 521 Then
            ProcessNumber = Math.Abs((ProcessNumber \ 10) - 52)
            CaptchaString += Alphabet.Substring(ProcessNumber, 1)
        Else
            CaptchaString += CStr(DateTime.UtcNow.Second \ 6)
        End If
        CaptchaGraf.Clear(Color.White)

        For hasher As Integer = 0 To 5
            CaptchaGraf.DrawString(CaptchaString.Substring(hasher, 1), DrawingFont, Brushes.Black, hasher * 20 + hasher + ProcessNumber \ 200, (hasher Mod 3) * (ProcessNumber \ 200))
        Next
        PictureBox1.Image = CaptchaImage
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = CaptchaString Then
            MessageBox.Show("Captcha Correct", "Captcha", MessageBoxButtons.OK, MessageBoxIcon.Information)
            If LinkLabel1.Text = Nothing Then
                MessageBox.Show("User-id should not be Empty!!", "User-Id", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If
            Dim Command As New SqlClient.SqlCommand
            Command.Connection = con
            Dim IsExist As Boolean
            con.Open()
            Dim cmd As New SqlCommand("select * from registration where user_id='" + LinkLabel1.Text + "'", con)
            Dim sdr As SqlDataReader = cmd.ExecuteReader()
            If (sdr.Read) Then
                password = sdr.GetString(1)
                IsExist = True
            End If
            con.Close()
            If IsExist Then
                MessageBox.Show(Decrypt(password), "Password", MessageBoxButtons.OK, MessageBoxIcon.Information)
                TextBox1.Clear()
                GenerateCaptcha()
            Else
                MessageBox.Show("password Not Exist!!:(", "Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox1.Clear()
                GenerateCaptcha()
            End If
        Else
            MessageBox.Show("Captcha Incorrect", "Captcha", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Clear()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        GenerateCaptcha()
    End Sub

    Private Sub Forgot_Password_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Data Source=localhost\SQLEXPRESS;Initial Catalog=Authentication;Integrated Security=True"
        GenerateCaptcha()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button2_Click(Nothing, Nothing)
            e.SuppressKeyPress = True
        End If
        If e.KeyCode = Keys.Space Then
            Button1_Click(Nothing, Nothing)
            e.SuppressKeyPress = True
        End If
    End Sub
End Class
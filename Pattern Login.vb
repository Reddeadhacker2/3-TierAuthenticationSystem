Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Public Class Pattern_Login
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim password As String

    'Decryption
    Protected Sub Decrypt(sender As Object, e As EventArgs)
        TextBox1.Text = Me.Decrypt(TextBox1.Text.Trim())
    End Sub
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
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If TextBox1.Text.Length.Equals(0) Then
            MessageBox.Show("Empty!! No Combination Selected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            TextBox1.Clear()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.TextLength > 24 Then
            MessageBox.Show("Choose only '9' Combination of any Order!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            TextBox1.AppendText(111)
            TextBox1.Focus()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.TextLength > 24 Then
            MessageBox.Show("Choose only '9' Combination of any Order!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            TextBox1.AppendText(222)
            TextBox1.Focus()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.TextLength > 24 Then
            MessageBox.Show("Choose only '9' Combination of any Order!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            TextBox1.AppendText(333)
            TextBox1.Focus()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim Command As New SqlClient.SqlCommand
        Command.Connection = con
        Dim IsExist As Boolean
        con.Open()
        Dim cmd As New SqlCommand("select * from patregistration where user_id='" + LinkLabel1.Text + "'", con)
        Dim sdr As SqlDataReader = cmd.ExecuteReader()
        If (sdr.Read) Then
            password = sdr.GetString(1)
            IsExist = True
        End If

        con.Close()
        If IsExist Then
            If Decrypt(password) = (TextBox1.Text) Then
                MessageBox.Show("Login Success !!:)", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Dim Img As New Graphical_Image_Login
                Me.Hide()
                Img.LinkLabel1.Text = LinkLabel1.Text
                Img.Show()
            Else
                MessageBox.Show("Pattern is wrong!...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TextBox1.Clear()
            End If
        Else
            MessageBox.Show("Please enter the valid credentials !!:(", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox1.Clear()
        End If
    End Sub

    Private Sub Pattern_Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Data Source=localhost\SQLEXPRESS;Initial Catalog=Authentication;Integrated Security=True"
    End Sub

    Private Sub Pattern_Login_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim dialog As DialogResult
        dialog = MessageBox.Show("Are you sure!! You want to close?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If dialog = DialogResult.No Then
            e.Cancel = True
        Else
            Application.ExitThread()
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If TextBox1.TextLength > 24 Then
            MessageBox.Show("Choose only '9' Combination of any Order!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            TextBox1.AppendText(444)
            TextBox1.Focus()
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If TextBox1.TextLength > 24 Then
            MessageBox.Show("Choose only '9' Combination of any Order!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            TextBox1.AppendText(555)
            TextBox1.Focus()
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If TextBox1.TextLength > 24 Then
            MessageBox.Show("Choose only '9' Combination of any Order!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            TextBox1.AppendText(666)
            TextBox1.Focus()
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If TextBox1.TextLength > 24 Then
            MessageBox.Show("Choose only '9' Combination of any Order!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            TextBox1.AppendText(777)
            TextBox1.Focus()
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If TextBox1.TextLength > 24 Then
            MessageBox.Show("Choose only '9' Combination of any Order!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            TextBox1.AppendText(888)
            TextBox1.Focus()
        End If
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        If TextBox1.TextLength > 24 Then
            MessageBox.Show("Choose only '9' Combination of any Order!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            TextBox1.AppendText(999)
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button4_Click(Nothing, Nothing)
            e.SuppressKeyPress = True
        End If
        If e.KeyCode = Keys.Back Then
            Button5_Click(Nothing, Nothing)
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim forgot As New Forgot_Pattern
        forgot.LinkLabel1.Text = LinkLabel1.Text
        forgot.Show()
    End Sub
End Class
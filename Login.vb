Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Public Class Login
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim password As String

    'Decryption
    Protected Sub Decrypt(sender As Object, e As EventArgs)
        TextBox2.Text = Me.Decrypt(TextBox2.Text.Trim())
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
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Data Source=localhost\SQLEXPRESS;Initial Catalog=Authentication;Integrated Security=True"
        TextBox2.UseSystemPasswordChar = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Auth As New Authenticator
        Me.Hide()
        Auth.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim Command As New SqlClient.SqlCommand
        Command.Connection = con
        Dim IsExist As Boolean
        con.Open()
        Dim cmd As New SqlCommand("select * from registration where user_id='" + TextBox1.Text + "'", con)
        Dim sdr As SqlDataReader = cmd.ExecuteReader()
        If (sdr.Read) Then
            password = sdr.GetString(1)
            IsExist = True
        End If

        con.Close()
        If IsExist Then
            If Decrypt(password) = (TextBox2.Text) Then
                MessageBox.Show("Login Success !!:)", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Dim Pat As New Pattern_Login
                Me.Hide()
                Pat.LinkLabel1.Text = TextBox1.Text
                Pat.Show()
            Else
                MessageBox.Show("Password is wrong!...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TextBox2.Clear()
            End If
        Else
            MessageBox.Show("Please enter the valid credentials !!:(", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox1.Clear()
            TextBox2.Clear()
        End If
    End Sub

    Private Sub Login_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim dialog As DialogResult
        dialog = MessageBox.Show("Are you sure!! You want to close?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If dialog = DialogResult.No Then
            e.Cancel = True
        Else
            Application.ExitThread()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If TextBox2.UseSystemPasswordChar = True Then
            TextBox2.UseSystemPasswordChar = False
        Else
            TextBox2.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
            e.SuppressKeyPress = True
        End If
        If e.KeyCode = Keys.Escape Then
            Button1_Click(Nothing, Nothing)
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button3_Click(Nothing, Nothing)
            e.SuppressKeyPress = True
        End If
        If e.KeyCode = Keys.Escape Then
            Button1_Click(Nothing, Nothing)
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim forgot As New Forgot_Password
        forgot.LinkLabel1.Text = TextBox1.Text
        forgot.Show()
    End Sub
End Class

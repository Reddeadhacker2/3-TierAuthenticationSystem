Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Public Class Registration
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim dr As SqlDataReader

    'Encryption 
    Protected Sub Encrypt(sender As Object, e As EventArgs)
        TextBox2.Text = Me.Encrypt(TextBox2.Text.Trim())
        TextBox3.Text = Me.Encrypt(TextBox3.Text.Trim())
    End Sub

    Private Function Encrypt(clearText As String) As String
        Dim EncryptionKey As String = "0ram@1234xxxxxxxxxxtttttuuuuuiiiiio"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(clearText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
         &H65, &H64, &H76, &H65, &H64, &H65,
         &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using
                clearText = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using
        Return clearText
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Auth As New Authenticator
        Me.Hide()
        Auth.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Empty TextBox
        While TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = ""
            MessageBox.Show("Empty Fields Not Allowed !!:)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End While

        'Matching and insertion
        If TextBox2.Text = TextBox3.Text Then
            Dim UserName As String = TextBox1.Text
            Dim password As String = Encrypt(TextBox3.Text.ToString)
            con.ConnectionString = "Data Source=localhost\SQLEXPRESS;Initial Catalog=Authentication;Integrated Security=True"
            con.Open()
            cmd = con.CreateCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "insert into registration values('" + UserName + "','" + password + "')"
            cmd.ExecuteNonQuery()
            MessageBox.Show("Registration Successfull !!:)", "Registered", MessageBoxButtons.OK, MessageBoxIcon.Information)
            con.Close()
            Dim Pat As New Pattern
            Me.Hide()
            Pat.LinkLabel1.Text = TextBox1.Text
            Pat.Show()
        Else
            MessageBox.Show("Password not Matches !!:(", "Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox3.Clear()
        End If
    End Sub

    Private Sub Registration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Data Source=localhost\SQLEXPRESS;Initial Catalog=Authentication;Integrated Security=True"
        TextBox2.UseSystemPasswordChar = True
        TextBox3.UseSystemPasswordChar = True
    End Sub

    Private Sub Registration_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim dialog As DialogResult
        dialog = MessageBox.Show("Are you sure!! You want to close?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If dialog = DialogResult.No Then
            e.Cancel = True
        Else
            Application.ExitThread()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If TextBox2.UseSystemPasswordChar = True And TextBox3.UseSystemPasswordChar = True Then
            TextBox2.UseSystemPasswordChar = False
            TextBox3.UseSystemPasswordChar = False
        Else
            TextBox2.UseSystemPasswordChar = True
            TextBox3.UseSystemPasswordChar = True
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
            TextBox3.Focus()
            e.SuppressKeyPress = True
        End If
        If e.KeyCode = Keys.Escape Then
            Button1_Click(Nothing, Nothing)
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button3_Click(Nothing, Nothing)
            e.SuppressKeyPress = True
        End If
        If e.KeyCode = Keys.Escape Then
            Button1_Click(Nothing, Nothing)
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs) Handles TextBox1.LostFocus
        'Duplicate Value 
        con.Open()
        cmd = New SqlCommand("SELECT user_id FROM registration WHERE user_id = '" + TextBox1.Text + "'", con)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            MessageBox.Show("Already Exist", "Existing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox1.Clear()
            TextBox1.Focus()
            cmd.Dispose()
            con.Close()
            Return
        End If
        cmd.Dispose()
        con.Close()
    End Sub
End Class
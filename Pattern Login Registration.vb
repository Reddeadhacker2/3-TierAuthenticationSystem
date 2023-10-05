Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Public Class Pattern
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim da As New SqlDataAdapter
    Dim dt As New DataTable
    Dim dr As SqlDataReader

    'Encryption 
    Protected Sub Encrypt(sender As Object, e As EventArgs)
        TextBox1.Text = Me.Encrypt(TextBox1.Text.Trim())
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

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If TextBox1.Text.Length.Equals(0) Then
            MessageBox.Show("Empty!! No Combination Selected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            TextBox1.Clear()
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If TextBox1.Text.Length.Equals(0) Then
            MessageBox.Show("Empty!! No Combination Selected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            TextBox1.Text = TextBox1.Text.Remove(TextBox1.Text.Length - 3)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'Empty TextBox
        While TextBox1.Text = ""
            MessageBox.Show("Empty Fields Not Allowed !!:)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End While
        'Insertion
        Dim password As String = Encrypt(TextBox1.Text.ToString)
        con.ConnectionString = "Data Source=localhost\SQLEXPRESS;Initial Catalog=Authentication;Integrated Security=True"
        con.Open()
        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "insert into patregistration values('" + LinkLabel1.Text + "','" + password + "')"
        cmd.ExecuteNonQuery()
        MessageBox.Show("Registration Successfull !!:)", "Registered", MessageBoxButtons.OK, MessageBoxIcon.Information)
        con.Close()
        Dim Img As New Image
        Me.Hide()
        Img.LinkLabel2.Text = LinkLabel1.Text
        Img.Show()
    End Sub

    Private Sub Pattern_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
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
        If e.KeyCode = Keys.Delete Then
            Button6_Click(Nothing, Nothing)
            e.SuppressKeyPress = True
        End If
        If e.KeyCode = Keys.Back Then
            Button5_Click(Nothing, Nothing)
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub Pattern_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
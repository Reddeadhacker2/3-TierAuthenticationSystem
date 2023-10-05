Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public Class Graphical_Image_Login
    Dim con As New SqlConnection("Data Source=localhost\SQLEXPRESS;Initial Catalog=Authentication;Integrated Security=True")
    Dim img As Bitmap
    Dim filepath As String
    Public Function CropBitmap(ByVal bmp As Bitmap, ByVal cropX As Integer, ByVal cropY As Integer, ByVal cropWidth As Integer, ByVal cropHeight As Integer) As Bitmap
        Dim rect As New Rectangle(cropX, cropY, cropWidth, cropHeight)
        Dim cropped As Bitmap = bmp.Clone(rect, bmp.PixelFormat)
        Return cropped
    End Function
    'Decryption
    Protected Sub Decrypt(sender As Object, e As EventArgs)
        filepath = Me.Decrypt(filepath.Trim())
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

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If TextBox1.TextLength > 24 Then
            MessageBox.Show("Choose only '9' Combination of any Order!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            TextBox1.AppendText(111)
            TextBox1.Focus()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox1.Clear()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If TextBox1.TextLength > 24 Then
            MessageBox.Show("Choose only '9' Combination of any Order!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            TextBox1.AppendText(222)
            TextBox1.Focus()
        End If
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        If TextBox1.TextLength > 24 Then
            MessageBox.Show("Choose only '9' Combination of any Order!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            TextBox1.AppendText(333)
            TextBox1.Focus()
        End If
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        If TextBox1.TextLength > 24 Then
            MessageBox.Show("Choose only '9' Combination of any Order!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            TextBox1.AppendText(444)
            TextBox1.Focus()
        End If
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        If TextBox1.TextLength > 24 Then
            MessageBox.Show("Choose only '9' Combination of any Order!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            TextBox1.AppendText(555)
            TextBox1.Focus()
        End If
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        If TextBox1.TextLength > 24 Then
            MessageBox.Show("Choose only '9' Combination of any Order!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            TextBox1.AppendText(666)
            TextBox1.Focus()
        End If
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        If TextBox1.TextLength > 24 Then
            MessageBox.Show("Choose only '9' Combination of any Order!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            TextBox1.AppendText(777)
            TextBox1.Focus()
        End If
    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        If TextBox1.TextLength > 24 Then
            MessageBox.Show("Choose only '9' Combination of any Order!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            TextBox1.AppendText(888)
            TextBox1.Focus()
        End If
    End Sub

    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        If TextBox1.TextLength > 24 Then
            MessageBox.Show("Choose only '9' Combination of any Order!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            TextBox1.AppendText(999)
            TextBox1.Focus()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Cmd As New SqlClient.SqlCommand
        Cmd.Connection = con
        con.Open()
        Cmd.CommandText = "SELECT COUNT(1) FROM imgregistration WHERE user_id =@UserId AND passcode = @passcode"
        Cmd.Parameters.Add(New SqlClient.SqlParameter("@UserId", LinkLabel1.Text))
        Cmd.Parameters.Add(New SqlClient.SqlParameter("@passcode", TextBox1.Text))
        If Cmd.ExecuteScalar = 1 Then
            MessageBox.Show("Login Succeeded !!:)", "Logged In", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Cmd = con.CreateCommand
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = "select path from Locker where user_id=@UserId"
            Cmd.Parameters.Add(New SqlClient.SqlParameter("@UserId", LinkLabel1.Text))
            Cmd.ExecuteNonQuery()
            Dim dr As SqlDataReader = Cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                filepath = dr.GetValue(0)
            Else
                MessageBox.Show("Data Not Found!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
            Dim file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Decrypt(filepath))
            Dim psi As New ProcessStartInfo(file)
            psi.UseShellExecute = True
            Process.Start(psi)
            Cmd.Dispose()
            con.Close()
        Else
            MessageBox.Show("Invalid Credentials !!:(", "No Matches", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Cmd.Dispose()
            con.Close()
            TextBox1.Clear()
            Return
        End If
        Dim Auth As New Authenticator
        Me.Hide()
        Auth.Show()
    End Sub

    Private Sub Graphical_Image_Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim conn As New SqlConnection("Data Source=localhost\SQLEXPRESS;Initial Catalog=Authentication;Integrated Security=True")
        Try
            conn.ConnectionString = "Data Source=localhost\SQLEXPRESS;Initial Catalog=Authentication;Integrated Security=True"
            conn.Open()
        Catch ex As Exception
            MessageBox.Show("Fail")
        Finally
            conn.Close()
            conn.Dispose()
        End Try
        Dim con As New SqlConnection("Data Source=localhost\SQLEXPRESS;Initial Catalog=Authentication;Integrated Security=True")
        Dim cmd As New SqlCommand("select img from imgregistration where user_id=@UserId", con)
        cmd.Parameters.Add(New SqlClient.SqlParameter("@UserId", LinkLabel1.Text))
        con.Open()
        img = System.Drawing.Image.FromStream(New IO.MemoryStream(CType(cmd.ExecuteScalar, Byte())))
        con.Close()
        PictureBox3.Image = CropBitmap(New Bitmap(img), 100, 100, 200, 100)
        PictureBox4.Image = CropBitmap(New Bitmap(img), 300, 100, 400, 100)
        PictureBox1.Image = CropBitmap(New Bitmap(img), 500, 100, 600, 100)

        PictureBox5.Image = CropBitmap(New Bitmap(img), 100, 200, 200, 200)
        PictureBox6.Image = CropBitmap(New Bitmap(img), 300, 200, 400, 200)
        PictureBox7.Image = CropBitmap(New Bitmap(img), 500, 200, 600, 200)


        PictureBox8.Image = CropBitmap(New Bitmap(img), 100, 300, 200, 300)
        PictureBox9.Image = CropBitmap(New Bitmap(img), 300, 300, 400, 300)
        PictureBox2.Image = CropBitmap(New Bitmap(img), 500, 300, 600, 300)


    End Sub

    Private Sub Graphical_Image_Login_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim dialog As DialogResult
        dialog = MessageBox.Show("Are you sure!! You want to close?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If dialog = DialogResult.No Then
            e.Cancel = True
        Else
            Application.ExitThread()
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1_Click(Nothing, Nothing)
            e.SuppressKeyPress = True
        End If
        If e.KeyCode = Keys.Back Then
            Button4_Click(Nothing, Nothing)
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim forgot As New Forgot_Image
        forgot.LinkLabel1.Text = LinkLabel1.Text
        forgot.Show()
    End Sub
End Class
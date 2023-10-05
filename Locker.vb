Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public Class Locker
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim filepath As String
    Private MyFile As FileInfo
    'Encryption 
    Protected Sub Encrypt(sender As Object, e As EventArgs)
        filepath = Me.Encrypt(filepath.Trim())
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
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        OpenFileDialog1.ShowDialog()
        filepath = OpenFileDialog1.FileName
        MyFile = New FileInfo(filepath)
        ShowFileInfo()
        LinkLabel2.Text = filepath
    End Sub

    Private Sub PictureBox1_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub PictureBox1_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragDrop
        filepath = e.Data.GetData(DataFormats.FileDrop)(0)
        MyFile = New FileInfo(filepath)
        If String.IsNullOrWhiteSpace(MyFile.Extension) Then Exit Sub
        ShowFileInfo()
        LinkLabel2.Text = filepath
    End Sub

    Private Sub ShowFileInfo()
        PictureBox2.Image = Icon.ExtractAssociatedIcon(MyFile.FullName).ToBitmap
        Label7.Text = "File Name: " & MyFile.Name
        Label6.Text = "File Type: " & MyFile.Extension
        Label3.Text = "Size: " & Math.Round(MyFile.Length / 1024) & "KB"
        Label1.Text = "Created: " & MyFile.CreationTime
        Label5.Text = "Modified: " & MyFile.LastWriteTime
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Try
            Dim file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), filepath)
            Dim psi As New ProcessStartInfo(file)
            psi.UseShellExecute = True
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("File not Selected!!:(", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If filepath Is Nothing Then
            MessageBox.Show("File not Selected!!:(", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Dim con As New SqlConnection("Data Source=localhost\SQLEXPRESS;Initial Catalog=Authentication;Integrated Security=True")
            con.Open()
            cmd = con.CreateCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "insert into Locker values('" + LinkLabel1.Text + "','" + Encrypt(filepath) + "')"
            cmd.ExecuteNonQuery()
            MessageBox.Show("File Locked Successfully!!;)", "Locked", MessageBoxButtons.OK, MessageBoxIcon.Information)
            con.Close()
            Dim Auth As New Authenticator
            Me.Hide()
            Auth.Show()
        End If
    End Sub

    Private Sub Locker_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim dialog As DialogResult
        dialog = MessageBox.Show("Are you sure!! You want to close?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If dialog = DialogResult.No Then
            e.Cancel = True
        Else
            Application.ExitThread()
        End If
    End Sub

    Private Sub Locker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBox1.AllowDrop = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        PictureBox1.Image = Nothing
        PictureBox2.Image = Nothing
        LinkLabel2.Text = Nothing
        Label7.Text = Nothing
        Label6.Text = Nothing
        Label3.Text = Nothing
        Label1.Text = Nothing
        Label5.Text = Nothing
        filepath = Nothing
        PictureBox1.Load("C:\Users\gagan\Downloads\ppt Images\1091585.png")
        PictureBox2.Load("C:\Users\gagan\Downloads\ppt Images\imagess.png")
        LinkLabel2.Text = "filepath..."
        Label7.Text = "File Name:"
        Label6.Text = "File Type:"
        Label3.Text = "File Size:"
        Label1.Text = "Created:"
        Label5.Text = "Modified:"

    End Sub
End Class
Imports System.Data.SqlClient
Imports System.IO
Public Class Image
    Dim con As New SqlConnection("Data Source=localhost\SQLEXPRESS;Initial Catalog=Authentication;Integrated Security=True")
    Public Function CropBitmap(ByVal bmp As Bitmap, ByVal cropX As Integer, ByVal cropY As Integer, ByVal cropWidth As Integer, ByVal cropHeight As Integer) As Bitmap
        Dim rect As New Rectangle(cropX, cropY, cropWidth, cropHeight)
        Dim cropped As Bitmap = bmp.Clone(rect, bmp.PixelFormat)
        Return cropped
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName <> "" Then
            PictureBox1.ImageLocation = OpenFileDialog1.FileName
        End If
        'ReCleaning
        PictureBox2.Image = Nothing
        PictureBox3.Image = Nothing
        PictureBox4.Image = Nothing

        PictureBox5.Image = Nothing
        PictureBox6.Image = Nothing
        PictureBox7.Image = Nothing

        PictureBox8.Image = Nothing
        PictureBox9.Image = Nothing
        PictureBox10.Image = Nothing
        'PictureBox1.ClientSize = New Size(300, 300)
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        While PictureBox1.Image Is Nothing
            MessageBox.Show("No Image is Selected!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End While
        Try
            PictureBox2.Image = CropBitmap(New Bitmap(PictureBox1.Image), 100, 100, 200, 100)
            PictureBox3.Image = CropBitmap(New Bitmap(PictureBox1.Image), 300, 100, 400, 100)
            PictureBox4.Image = CropBitmap(New Bitmap(PictureBox1.Image), 500, 100, 600, 100)

            PictureBox5.Image = CropBitmap(New Bitmap(PictureBox1.Image), 100, 200, 200, 200)
            PictureBox6.Image = CropBitmap(New Bitmap(PictureBox1.Image), 300, 200, 400, 200)
            PictureBox7.Image = CropBitmap(New Bitmap(PictureBox1.Image), 500, 200, 600, 200)

            PictureBox8.Image = CropBitmap(New Bitmap(PictureBox1.Image), 100, 300, 200, 300)
            PictureBox9.Image = CropBitmap(New Bitmap(PictureBox1.Image), 300, 300, 400, 300)
            PictureBox10.Image = CropBitmap(New Bitmap(PictureBox1.Image), 500, 300, 600, 300)
        Catch ex As System.OutOfMemoryException
            MessageBox.Show("Image *Size or *Dimension is too Large.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        While PictureBox1.Image Is Nothing
            MessageBox.Show("No Image is Selected!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End While
        While PictureBox2.Image Is Nothing
            MessageBox.Show("Split!! the Image", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End While
        Dim command As New SqlCommand("insert into imgregistration(user_id,img,passcode) values(@userid,@img,111222333444555666777888999)", con)
        Dim ms As New MemoryStream
        PictureBox1.Image.Save(ms, PictureBox1.Image.RawFormat)
        command.Parameters.Add("@img", SqlDbType.Image).Value = ms.ToArray()
        command.Parameters.Add("@userid", SqlDbType.Text).Value = LinkLabel2.Text
        con.Open()
        If command.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Image Successfully Registered !!:)", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Image Not Registered !!(:", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        con.Close()

        Dim Lock As New Locker
        Me.Hide()
        Lock.LinkLabel1.Text = LinkLabel2.Text
        Lock.Show()

    End Sub
    Private Sub Image_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            con.ConnectionString = "Data Source=localhost\SQLEXPRESS;Initial Catalog=Authentication;Integrated Security=True"
        Catch ex As Exception
            MessageBox.Show("Fail")
        End Try

    End Sub
    Private Sub Image_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim dialog As DialogResult
        dialog = MessageBox.Show("Are you sure!! You want to close?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If dialog = DialogResult.No Then
            e.Cancel = True
        Else
            Application.ExitThread()
        End If
    End Sub
End Class
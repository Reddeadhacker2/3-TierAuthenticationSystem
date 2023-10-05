Public Class Authenticator
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Reg As New Registration
        Me.Hide()
        Reg.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Log As New Login
        Me.Hide()
        Log.Show()
    End Sub

    Private Sub Authenticator_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim dialog As DialogResult
        dialog = MessageBox.Show("Are you sure!! You want to close?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If dialog = DialogResult.No Then
            e.Cancel = True
        Else
            Application.ExitThread()
        End If
    End Sub

    Private Sub Authenticator_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
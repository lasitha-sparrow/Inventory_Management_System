Public Class Form1
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ProgressBar1.Hide()
        Label4.Hide()
    End Sub

    Private Sub btnNXT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNXT.Click

        If txtUN.Text = "username" And txtPW.Text = "password" Then
          
            ProgressBar1.Show()
            btnNXT.Hide()
            btnExit.Hide()
            Label1.Hide()
            Label2.Hide()
            txtPW.Hide()
            txtUN.Hide()
            Timer1.Start()
            Panel1.Hide()
            Label4.Show()
        Else
            MsgBox("Sorry, The Username or Password was incorrect.", MsgBoxStyle.Critical, "Information")
            txtPW.Clear()
            txtUN.Clear()
        End If
    End Sub

  

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ProgressBar1.Increment(35)
        Label4.Show()
        If ProgressBar1.Value = ProgressBar1.Maximum Then
            Timer1.Stop()
            Form5.Show()
            Me.Hide()
            MsgBox("wellcome, User", MsgBoxStyle.Information)
            ProgressBar1.Hide()
            Label4.Hide()
            btnNXT.Show()
            btnExit.Show()
            Label1.Show()
            Label2.Show()
            txtPW.Show()
            txtUN.Show()
            Panel1.Show()
            txtPW.Clear()
            txtUN.Clear()

        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class

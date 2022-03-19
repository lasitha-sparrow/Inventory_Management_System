Public Class Form5
    Private con As New System.Data.SqlClient.SqlConnection
    Private com As New System.Data.SqlClient.SqlCommand
    Private ada As New System.Data.SqlClient.SqlDataAdapter
    Private ds As New DataSet
   
    Private Sub AddToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToolStripMenuItem.Click
        Form2.MdiParent = Me
        Form2.Show()
    End Sub

    Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click
        Try
            If Form2.IsMdiChild Then
                ada = New System.Data.SqlClient.SqlDataAdapter("SELECT * FROM Inventory_Items1 WHERE Item_Code='" & Form2.txtItCode.Text & "'", con)
                ds.Clear()
                ada.Fill(ds, "Stock_Management_System")
                Dim result As Integer
                result = ds.Tables(0).Rows.Count
                If result = 1 Then

                    ItCode = Form2.txtItCode.Text
                    ItName = Form2.txtName.Text
                    Stock = Form2.txtStockValue.Text
                    Qu = Form2.txtQuantity.Text
                    ItPrice = Form2.txtItPrice.Text
                    Form3.MdiParent = Me
                    Form3.Show()
                Else
                    MsgBox("                    This Record is not in files." & Chr(13) & " Please add the Record before entering theTransaction Details Form")
                End If
            Else
                Form3.MdiParent = Me
                Form3.Show()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SearchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchToolStripMenuItem.Click
        Form4.Show()
        Form4.MdiParent = Me
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub LogOutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogOutToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub Form5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            con.ConnectionString = ("Data Source=LOCALHOST;Initial Catalog=Stock_Management_System;Integrated Security=True")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
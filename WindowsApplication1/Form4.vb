Public Class Form4
    Private con As New System.Data.SqlClient.SqlConnection
    Private com As New System.Data.SqlClient.SqlCommand
    Private ada As New System.Data.SqlClient.SqlDataAdapter
    Private ds4 As New DataSet

  
    Private Sub Form4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            con.ConnectionString = ("Data Source=LOCALHOST;Initial Catalog=Stock_Management_System;Integrated Security=True")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Dim msg1 As Integer
        msg1 = MsgBox("Do you want to Clear this Details?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo, "Delete")
        If msg1 = vbYes Then
            txtCredit.Clear()
            txtemail.Clear()
            txtName.Clear()
            txtNo.Clear()
            txtSupID.Clear()
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Dim msg2 As Integer
        msg2 = MsgBox("Do you want to Exit?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo, "Delete")
        If msg2 = vbYes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnTrans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrans.Click
        Form3.MdiParent = Form5
        Me.Hide()
        Form3.Show()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            con.Open()
            com.Connection = con
            com.CommandText = "insert into Supplier_Details1 (Supplier_ID,Supplier_Name,Contact_No,E_mail,Start_Date,End_Date,Credit_Terms)Values ('" + txtSupID.Text + "','" + txtName.Text + "','" + txtNo.Text + "','" + txtemail.Text + "','" + DateTimePicker1.Value + "','" + DateTimePicker2.Value + "','" + txtCredit.Text + "');"
            com.ExecuteNonQuery()
            MsgBox("Records are successfully added")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            con.Open()
            com.Connection = con
            com.CommandText = "Update Supplier_Details1 SET Supplier_Name='" & txtName.Text & "',Contact_No='" & txtNo.Text & "',E_mail='" & txtemail.Text & "',Start_Date='" & DateTimePicker1.Value & "',End_Date='" & DateTimePicker2.Value & "',Credit_Terms='" & txtCredit.Text & "'WHERE Supplier_ID='" & txtSupID.Text & "'"
            com.ExecuteNonQuery()
            MsgBox("Record has been updated")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            con.Open()
            com.Connection = con
            com.CommandText = "Delete FROM Supplier_Details1 WHERE Supplier_ID='" & txtSupID.Text & "'"
            com.ExecuteNonQuery()
            MsgBox("Record has been Deleted")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
        txtCredit.Clear()
        txtemail.Clear()
        txtName.Clear()
        txtNo.Clear()
        txtSupID.Clear()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            ada = New System.Data.SqlClient.SqlDataAdapter("SELECT * FROM Supplier_Details1 WHERE Supplier_ID='" & txtSupID.Text & "'", con)
            ds4.Clear()
            ada.Fill(ds4, "Stock_Management_System")
            Dim result As Integer
            result = ds4.Tables(0).Rows.Count
            If result = 1 Then
                MsgBox("Record Exist")
                txtName.Text = ds4.Tables("Stock_Management_System").Rows(0).Item(1)
                txtNo.Text = ds4.Tables("Stock_Management_System").Rows(0).Item(2)
                txtemail.Text = ds4.Tables("Stock_Management_System").Rows(0).Item(3)
                DateTimePicker1.Value = ds4.Tables("Stock_Management_System").Rows(0).Item(4)
                DateTimePicker2.Value = ds4.Tables("Stock_Management_System").Rows(0).Item(5)
                txtCredit.Text = ds4.Tables("Stock_Management_System").Rows(0).Item(6)
            Else
                MsgBox("Record not exist")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnInven_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInven.Click
        Form2.MdiParent = Form5
        Me.Hide()
        Form2.Show()
    End Sub

    Private Sub txtSupID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSupID.LostFocus, TextBox6.LostFocus, TextBox1.LostFocus, TextBox13.LostFocus, TextBox11.LostFocus
        If (txtSupID.Text) = "" Then
            MsgBox("Please enter the Supplier ID", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Incorrect Item Number")
            txtSupID.Focus()
        ElseIf Len(txtSupID.Text) <> 4 Then
            MsgBox("Supplier ID is incorrect." & Chr(13) & "It should be 4 numeric charactors", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Incorrect Item Number")
            txtSupID.Clear()
            txtSupID.Focus()
        ElseIf Not IsNumeric(txtSupID.Text) Then
            MsgBox("Supplier ID should be 4 numeric charactors", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Incorrect Item Number")
            txtSupID.Clear()
            txtSupID.Focus()
        End If
    End Sub
End Class
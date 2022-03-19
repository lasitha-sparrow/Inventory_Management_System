Public Class Form2
    Private con As New System.Data.SqlClient.SqlConnection
    Private com As New System.Data.SqlClient.SqlCommand
    Private ada As New System.Data.SqlClient.SqlDataAdapter
    Private ds As New DataSet

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Dim msg1 As Integer
        msg1 = MsgBox("Do you want to Clear this Record?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo, "Delete")
        If msg1 = vbYes Then
            txtItCode.Clear()
            txtItPrice.Clear()
            txtName.Clear()
            txtQuantity.Clear()
            txtStockValue.Clear()
            txtStoreLocation.Clear()
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
        Try
            ada = New System.Data.SqlClient.SqlDataAdapter("SELECT * FROM Inventory_Items1 WHERE Item_Code='" & txtItCode.Text & "'", con)
            ds.Clear()
            ada.Fill(ds, "Stock_Management_System")
            Dim result As Integer
            result = ds.Tables(0).Rows.Count
            If result = 1 Then
                ItCode = txtItCode.Text
                ItName = txtName.Text
                Stock = txtStockValue.Text
                Qu = txtQuantity.Text
                ItPrice = txtItPrice.Text
                Form3.MdiParent = Form5
                Me.Hide()
                Form3.Show()
            Else
                MsgBox("                    This Record is not in files." & Chr(13) & " Please add the Record before entering theTransaction Details Form")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSup.Click
        Form4.MdiParent = Form5
        Me.Hide()
        Form4.Show()
    End Sub

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            con.ConnectionString = ("Data Source=LOCALHOST;Initial Catalog=Stock_Management_System;Integrated Security=True")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            con.Open()
            com.Connection = con
            com.CommandText = "insert into Inventory_Items1 Values ('" + txtItCode.Text + "','" + txtName.Text + "','" + txtQuantity.Text + "','" + txtItPrice.Text + "','" + DateTimePicker1.Value + "','" + txtStockValue.Text + "','" + txtStoreLocation.Text + "');"
            com.ExecuteNonQuery()
            MsgBox("Records are successfully added")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try

        Try
            con.Open()
            com.Connection = con
            com.CommandText = "insert into Transaction_Details(Item_Code,Item_Name,Previous_Stock,Quantity,Item_Price) values('" + txtItCode.Text + "','" + txtName.Text + "','" + txtStockValue.Text + "','" + txtQuantity.Text + "','" + txtItPrice.Text + "')"
            com.ExecuteNonQuery()
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
            com.CommandText = "Update Inventory_Items1 SET Item_Name='" & txtName.Text & "',Quantity='" & txtQuantity.Text & "',Item_Price='" & txtItPrice.Text & "',Last_of_Issued='" & DateTimePicker1.Value & "',Stock_Value='" & txtStockValue.Text & "',Store_Location='" & txtStoreLocation.Text & "'WHERE Item_Code='" & txtItCode.Text & "'"
            com.ExecuteNonQuery()
            MsgBox("Record has been updated")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
        Try
            con.Open()
            com.Connection = con
            com.CommandText = "Update Transaction_Details SET Item_Name='" & txtName.Text & "',Quantity='" & txtQuantity.Text & "',Item_Price='" & txtItPrice.Text & "',Previous_Stock='" & txtStockValue.Text & "'WHERE Item_Code='" & txtItCode.Text & "'"
            com.ExecuteNonQuery()
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
            com.CommandText = "Delete FROM Inventory_Items1 WHERE Item_Code='" & txtItCode.Text & "'"
            com.ExecuteNonQuery()
            MsgBox("Record has been Deleted")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try

        Try
            con.Open()
            com.Connection = con
            com.CommandText = "Delete FROM Transaction_Details WHERE Item_Code='" & txtItCode.Text & "'"
            com.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try

        txtItCode.Clear()
        txtItPrice.Clear()
        txtName.Clear()
        txtQuantity.Clear()
        txtStockValue.Clear()
        txtStoreLocation.Clear()
    End Sub
    Private Sub txtItCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtItCode.LostFocus
        If (txtItCode.Text) = "" Then
            MsgBox("Please enter the Item Code", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Incorrect Item Number")
            txtItCode.Focus()
        ElseIf Len(txtItCode.Text) <> 4 Then
            MsgBox("Item Code is incorrect." & Chr(13) & "It should be 4 numeric charactors", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Incorrect Item Number")
            txtItCode.Clear()
            txtItCode.Focus()
        ElseIf Not IsNumeric(txtItCode.Text) Then
            MsgBox("Item Code should be 4 numeric charactors", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Incorrect Item Number")
            txtItCode.Clear()
            txtItCode.Focus()
        End If
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If txtItCode.Text = "" Then
            MessageBox.Show("Please enter Item No")
        Else
        End If
        Try
            ada = New System.Data.SqlClient.SqlDataAdapter("SELECT * FROM Inventory_Items1 WHERE Item_Code='" & txtItCode.Text & "'", con)
            ds.Clear()
            ada.Fill(ds, "Stock_Management_System")
            Dim result As Integer
            result = ds.Tables(0).Rows.Count
            If result = 1 Then
                MsgBox("Record Found", MsgBoxStyle.Information)
                txtName.Text = ds.Tables("Stock_Management_System").Rows(0).Item(1)
                txtQuantity.Text = ds.Tables("Stock_Management_System").Rows(0).Item(2)
                txtItPrice.Text = ds.Tables("Stock_Management_System").Rows(0).Item(3)
                DateTimePicker1.Value = ds.Tables("Stock_Management_System").Rows(0).Item(4)
                txtStockValue.Text = ds.Tables("Stock_Management_System").Rows(0).Item(5)
                txtStoreLocation.Text = ds.Tables("Stock_Management_System").Rows(0).Item(6)
            Else
                MsgBox("Record is not exist")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnStockValue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStockValue.Click
        ItPrice = txtItPrice.Text
        Qu = txtQuantity.Text
        Stock = ItPrice * Qu
        txtStockValue.Text = Stock
    End Sub


End Class
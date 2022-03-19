Public Class Form3
    Private con As New System.Data.SqlClient.SqlConnection
    Private com As New System.Data.SqlClient.SqlCommand
    Private ada As New System.Data.SqlClient.SqlDataAdapter
    Private ds3 As New DataSet

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            con.ConnectionString = ("Data Source=LOCALHOST;Initial Catalog=Stock_Management_System;Integrated Security=True")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        End Try
        txtItCode.Text = ItCode
        txtName.Text = ItName
        txtPrevious.Text = Stock
        txtQuantity.Text = Qu
        txtItPrice.Text = ItPrice
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Dim msg1 As Integer
        msg1 = MsgBox("Do you want to Clear this Record?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo, "Delete")
        If msg1 = vbYes Then
            txtItCode.Clear()
            txtItPrice.Clear()
            txtName.Clear()
            txtPrevious.Clear()
            txtNewSt.Clear()
            txtQuantity.Clear()
            RadioButton1.Checked = False
            RadioButton2.Checked = False
            RadioButton3.Checked = False
            RadioButton4.Checked = False
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Dim msg2 As Integer
        msg2 = MsgBox("Do you want to Exit?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo, "Delete")
        If msg2 = vbYes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnInven_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInven.Click
        Form2.MdiParent = Form5
        Me.Hide()
        Form2.Show()
    End Sub

    Private Sub btnNewStockValue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewStockValue.Click
        ItPrice = txtItPrice.Text
        TrQty = txtTrQty.Text
        Stock = txtPrevious.Text
        If RadioButton1.Checked Then
            NewStock = Stock + (ItPrice * TrQty)
        ElseIf RadioButton2.Checked Then
            NewStock = Stock - (ItPrice * TrQty)
        ElseIf RadioButton3.Checked Then
            NewStock = Stock + (ItPrice * TrQty)
        ElseIf RadioButton4.Checked Then
            NewStock = Stock - (ItPrice * TrQty)
        End If
        txtNewSt.Text = NewStock
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            If RadioButton1.Checked Then
                TextBox1.Text = "Issued in"
            ElseIf RadioButton2.Checked Then
                TextBox1.Text = "Issued out"
            ElseIf RadioButton3.Checked Then
                TextBox1.Text = "Returned in"
            ElseIf RadioButton4.Checked Then
                TextBox1.Text = "Returned out"
            End If
            con.Open()
            com.Connection = con
            com.CommandText = "Update Transaction_Details SET Item_Name='" & txtName.Text & "',Previous_Stock='" & txtPrevious.Text & "',Quantity='" & txtQuantity.Text & "',Item_Price='" & txtItPrice.Text & "',New_Stock='" & txtNewSt.Text & "',Transaction_Date='" & DateTimePicker1.Value & "',Transaction_Type='" & TextBox1.Text & "'WHERE Item_Code='" & txtItCode.Text & "'"
            com.ExecuteNonQuery()
            MsgBox("Record has been updated" & Chr(13) & "    in Inventory Items and Transaction Details Files")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
        Try
            con.Open()
            com.Connection = con
            com.CommandText = "Update Inventory_Items1 SET Item_Name='" + txtName.Text + "',Quantity='" + txtNwQty.Text + "',Item_Price='" + txtItPrice.Text + "',Stock_Value='" + txtNewSt.Text + "' where Item_Code='" + txtItCode.Text + "'"
            com.ExecuteNonQuery()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            ada = New System.Data.SqlClient.SqlDataAdapter("SELECT * FROM Inventory_Items1 WHERE Item_Code='" & txtItCode.Text & "'", con)
            ds3.Clear()
            ada.Fill(ds3, "Stock_Management_System")
            Dim result As Integer
            result = ds3.Tables(0).Rows.Count
            If result = 1 Then
                MsgBox("Record Exist")
                txtName.Text = ds3.Tables("Stock_Management_System").Rows(0).Item(1)
                txtPrevious.Text = ds3.Tables("Stock_Management_System").Rows(0).Item(5)
                txtQuantity.Text = ds3.Tables("Stock_Management_System").Rows(0).Item(2)
                txtItPrice.Text = ds3.Tables("Stock_Management_System").Rows(0).Item(3)
            Else
                MsgBox("Record not exist")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        End Try
        If TextBox1.Text = "Issued in" Then
            RadioButton1.Checked = True

        ElseIf TextBox1.Text = "Issued out" Then
            RadioButton2.Checked = True

        ElseIf TextBox1.Text = "Returned in" Then
            RadioButton3.Checked = True

        ElseIf TextBox1.Text = "Returned out" Then
            RadioButton4.Checked = True

        End If
    End Sub

    Private Sub txtTrQty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTrQty.LostFocus
        Qu = txtQuantity.Text
        TrQty = txtTrQty.Text

        If RadioButton1.Checked Then
            NwQty = Qu + TrQty
        ElseIf RadioButton2.Checked Then
            NwQty = Qu - TrQty
        ElseIf RadioButton3.Checked Then
            NwQty = Qu + TrQty
        ElseIf RadioButton4.Checked Then
            NwQty = Qu - TrQty
        End If
        txtNwQty.Text = NwQty
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

    Private Sub btnSup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSup.Click
        Form4.MdiParent = Form5
        Me.Hide()
        Form4.Show()
    End Sub
End Class
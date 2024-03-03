Imports System.ComponentModel

Partial Public Class Form1

    Inherits Form

    Private _dtPeople As DataTable
    Private _Person As clsPerson.Homework_Business.clsPerson

    Private Sub _RefreshGuestList()
        _dtPeople = clsPerson.Homework_Business.clsPerson.GetAllPeople()
        dgvPeopleList.DataSource = _dtPeople
    End Sub

    Private Function _GetGuestIDFromDGV() As Integer?
        Return DirectCast(dgvPeopleList.CurrentRow.Cells("PersonID").Value, Integer?)
    End Function

    Private Sub frmListGuests_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load
        _RefreshGuestList()
        dgvPeopleList.ClearSelection()
        _Reset()
        btnUpdate.Enabled = False
        btnDelete.Enabled = False
        btnAdd.Enabled = True
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        If Not Me.ValidateChildren() Then
            MessageBox.Show("Some fields are not valid! Put the mouse over the red icon(s) to see the error.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        _Person = New clsPerson.Homework_Business.clsPerson()

        _SavePerson()

        _RefreshGuestList()

    End Sub

    Private Function _ShowMassageAboutSavePersonInfo() As Boolean
        If (_Person.Save()) Then
            MessageBox.Show("Data Saved Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return True
        Else
            MessageBox.Show("Data Save Failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
    End Function

    Private Function _SavePerson()
        _Person.FirstName = txtFirstName.Text
        _Person.LastName = txtLastName.Text
        _Person.PhoneNumber = txtPhoneNumber.Text
        _Person.Email = txtEmail.Text
        _Person.City = txtCity.Text
        _Person.Street = txtStreet.Text
        _Person.State = txtState.Text
        _Person.ZipCode = txtZipCode.Text

        Return _ShowMassageAboutSavePersonInfo()
    End Function

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        If Not Me.ValidateChildren() Then
            MessageBox.Show("Some fields are not valid! Put the mouse over the red icon(s) to see the error.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        _Person = clsPerson.Homework_Business.clsPerson.Find(_GetGuestIDFromDGV())

        If (_Person Is Nothing) Then
            MessageBox.Show("Person does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If (_SavePerson()) Then
            _RefreshGuestList()
            _Reset()
            btnAdd.Enabled = True
            btnUpdate.Enabled = False
        Else
            btnAdd.Enabled = False
            btnUpdate.Enabled = True
        End If
    End Sub

    Private Sub _Reset()
        txtFirstName.Text = ""
        txtLastName.Text = ""
        txtPhoneNumber.Text = ""
        txtEmail.Text = ""
        txtCity.Text = ""
        txtStreet.Text = ""
        txtState.Text = ""
        txtZipCode.Text = ""
    End Sub

    Private Sub dgvPeopleList_SelectionChanged(sender As Object, e As EventArgs) Handles dgvPeopleList.SelectionChanged
        If dgvPeopleList.SelectedRows.Count > 0 Then

            Dim selectedRow As DataGridViewRow = dgvPeopleList.SelectedRows(0)

            ' Fill text boxes with data from the selected row
            txtFirstName.Text = selectedRow.Cells("FirstName").Value.ToString()
            txtLastName.Text = selectedRow.Cells("LastName").Value.ToString()
            txtPhoneNumber.Text = selectedRow.Cells("PhoneNumber").Value.ToString()
            txtEmail.Text = selectedRow.Cells("Email").Value.ToString()
            txtCity.Text = selectedRow.Cells("City").Value.ToString()
            txtStreet.Text = selectedRow.Cells("Street").Value.ToString()
            txtState.Text = selectedRow.Cells("State").Value.ToString()
            txtZipCode.Text = selectedRow.Cells("ZipCode").Value.ToString()

            btnAdd.Enabled = False
            btnDelete.Enabled = True
            btnUpdate.Enabled = True
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgvPeopleList.SelectedRows.Count <= 0 Then
            Return
        End If

        If (MessageBox.Show("Are you sure you want to delete this person?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes) Then

            If (clsPerson.Homework_Business.clsPerson.DeletePerson(_GetGuestIDFromDGV())) Then

                MessageBox.Show("Person Deleted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                _RefreshGuestList()
                _Reset()
                btnAdd.Enabled = True
                btnDelete.Enabled = False
                btnUpdate.Enabled = False

            End If

        End If

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        _Reset()
        btnAdd.Enabled = True
        btnUpdate.Enabled = False
        btnDelete.Enabled = False
    End Sub

    Private Sub txtZipCode_Validating(sender As Object, e As CancelEventArgs) Handles txtZipCode.Validating
        If String.IsNullOrWhiteSpace(txtZipCode.Text.Trim()) Then
            e.Cancel = True
            ErrorProvider1.SetError(txtZipCode, "This field cannot be empty!")
        Else
            ErrorProvider1.SetError(txtZipCode, Nothing)
        End If
    End Sub

    Private Sub txtState_Validating(sender As Object, e As CancelEventArgs) Handles txtState.Validating
        If String.IsNullOrWhiteSpace(txtState.Text.Trim()) Then
            e.Cancel = True
            ErrorProvider1.SetError(txtState, "This field cannot be empty!")
        Else
            ErrorProvider1.SetError(txtState, Nothing)
        End If
    End Sub

    Private Sub txtCity_Validating(sender As Object, e As CancelEventArgs) Handles txtCity.Validating
        If String.IsNullOrWhiteSpace(txtCity.Text.Trim()) Then
            e.Cancel = True
            ErrorProvider1.SetError(txtCity, "This field cannot be empty!")
        Else
            ErrorProvider1.SetError(txtCity, Nothing)
        End If
    End Sub

    Private Sub txtStreet_Validating(sender As Object, e As CancelEventArgs) Handles txtStreet.Validating
        If String.IsNullOrWhiteSpace(txtStreet.Text.Trim()) Then
            e.Cancel = True
            ErrorProvider1.SetError(txtStreet, "This field cannot be empty!")
        Else
            ErrorProvider1.SetError(txtStreet, Nothing)
        End If
    End Sub

    Private Sub txtEmail_Validating(sender As Object, e As CancelEventArgs) Handles txtEmail.Validating
        If String.IsNullOrWhiteSpace(txtEmail.Text.Trim()) Then
            e.Cancel = True
            ErrorProvider1.SetError(txtEmail, "This field cannot be empty!")
        Else
            ErrorProvider1.SetError(txtEmail, Nothing)
        End If
    End Sub

    Private Sub txtPhoneNumber_Validating(sender As Object, e As CancelEventArgs) Handles txtPhoneNumber.Validating
        If String.IsNullOrWhiteSpace(txtPhoneNumber.Text.Trim()) Then
            e.Cancel = True
            ErrorProvider1.SetError(txtPhoneNumber, "This field cannot be empty!")
        Else
            ErrorProvider1.SetError(txtPhoneNumber, Nothing)
        End If
    End Sub

    Private Sub txtLastName_Validating(sender As Object, e As CancelEventArgs) Handles txtLastName.Validating
        If String.IsNullOrWhiteSpace(txtLastName.Text.Trim()) Then
            e.Cancel = True
            ErrorProvider1.SetError(txtLastName, "This field cannot be empty!")
        Else
            ErrorProvider1.SetError(txtLastName, Nothing)
        End If
    End Sub

    Private Sub txtFirstName_Validating(sender As Object, e As CancelEventArgs) Handles txtFirstName.Validating
        If String.IsNullOrWhiteSpace(txtFirstName.Text.Trim()) Then
            e.Cancel = True
            ErrorProvider1.SetError(txtFirstName, "This field cannot be empty!")
        Else
            ErrorProvider1.SetError(txtFirstName, Nothing)
        End If
    End Sub
End Class

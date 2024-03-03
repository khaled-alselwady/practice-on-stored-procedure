Imports clsPersonData.Homework_DataAccess.clsPersonData

Namespace Homework_Business
    Public Class clsPerson
        Public Enum enMode
            AddNew = 0
            Update = 1
        End Enum

        Public Mode As enMode = enMode.AddNew

        Public Property PersonID As Integer?
        Public Property FirstName As String
        Public Property LastName As String
        Public Property PhoneNumber As String
        Public Property Email As String
        Public Property Street As String
        Public Property City As String
        Public Property State As String
        Public Property ZipCode As String

        Public Sub New()
            Me.PersonID = Nothing
            Me.FirstName = String.Empty
            Me.LastName = String.Empty
            Me.PhoneNumber = String.Empty
            Me.Email = String.Empty
            Me.Street = String.Empty
            Me.City = String.Empty
            Me.State = String.Empty
            Me.ZipCode = String.Empty
            Mode = enMode.AddNew
        End Sub

        Private Sub New(ByVal PersonID As Integer?, ByVal FirstName As String, ByVal LastName As String, ByVal PhoneNumber As String, ByVal Email As String, ByVal Street As String, ByVal City As String, ByVal State As String, ByVal ZipCode As String)
            Me.PersonID = PersonID
            Me.FirstName = FirstName
            Me.LastName = LastName
            Me.PhoneNumber = PhoneNumber
            Me.Email = Email
            Me.Street = Street
            Me.City = City
            Me.State = State
            Me.ZipCode = ZipCode
            Mode = enMode.Update
        End Sub

        Private Function _AddNewPerson() As Boolean
            Me.PersonID = AddNewPerson(Me.FirstName, Me.LastName, Me.PhoneNumber, Me.Email, Me.Street, Me.City, Me.State, Me.ZipCode)
            Return Me.PersonID.HasValue
        End Function

        Private Function _UpdatePerson() As Boolean
            Return UpdatePerson(Me.PersonID, Me.FirstName, Me.LastName, Me.PhoneNumber, Me.Email, Me.Street, Me.City, Me.State, Me.ZipCode)
        End Function

        Public Function Save() As Boolean
            Select Case Mode
                Case enMode.AddNew
                    If _AddNewPerson() Then
                        Mode = enMode.Update
                        Return True
                    Else
                        Return False
                    End If
                Case enMode.Update
                    Return _UpdatePerson()
            End Select
            Return False
        End Function

        Public Shared Function Find(ByVal PersonID As Integer?) As clsPerson
            Dim FirstName As String = String.Empty
            Dim LastName As String = String.Empty
            Dim PhoneNumber As String = String.Empty
            Dim Email As String = String.Empty
            Dim Street As String = String.Empty
            Dim City As String = String.Empty
            Dim State As String = String.Empty
            Dim ZipCode As String = String.Empty

            Dim IsFound As Boolean = GetPersonInfoByID(PersonID, FirstName, LastName, PhoneNumber, Email, Street, City, State, ZipCode)

            Return If(IsFound, New clsPerson(PersonID, FirstName, LastName, PhoneNumber, Email, Street, City, State, ZipCode), Nothing)
        End Function

        Public Shared Function DeletePerson(ByVal PersonID As Integer?) As Boolean
            Return clsPersonData.Homework_DataAccess.clsPersonData.DeletePerson(PersonID)
        End Function

        Public Shared Function DoesPersonExist(ByVal PersonID As Integer?) As Boolean
            Return clsPersonData.Homework_DataAccess.clsPersonData.DoesPersonExist(PersonID)
        End Function

        Public Shared Function GetAllPeople() As DataTable
            Return clsPersonData.Homework_DataAccess.clsPersonData.GetAllPeople()
        End Function
    End Class

End Namespace
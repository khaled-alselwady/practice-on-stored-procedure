Imports System.Data.SqlClient

Namespace Homework_DataAccess

    Public Class clsPersonData
        Public Shared Function GetPersonInfoByID(ByVal PersonID As Integer?, ByRef FirstName As String, ByRef LastName As String, ByRef PhoneNumber As String, ByRef Email As String, ByRef Street As String, ByRef City As String, ByRef State As String, ByRef ZipCode As String) As Boolean
            Dim IsFound As Boolean = False

            Try
                Using connection As New SqlConnection(clsDataAccessSettings.ConnectionString)
                    connection.Open()

                    Using command As New SqlCommand("SP_GetPersonInfoByID", connection)
                        command.CommandType = CommandType.StoredProcedure

                        command.Parameters.AddWithValue("@PersonID", If(PersonID.HasValue, PersonID, DBNull.Value))

                        Using reader As SqlDataReader = command.ExecuteReader()
                            If reader.Read() Then
                                ' The record was found
                                IsFound = True

                                FirstName = CStr(reader("FirstName"))
                                LastName = CStr(reader("LastName"))
                                PhoneNumber = CStr(reader("PhoneNumber"))
                                Email = CStr(reader("Email"))
                                Street = CStr(reader("Street"))
                                City = CStr(reader("City"))
                                State = CStr(reader("State"))
                                ZipCode = CStr(reader("ZipCode"))
                            Else
                                ' The record was not found
                                IsFound = False
                            End If
                        End Using
                    End Using
                End Using
            Catch ex As SqlException
                IsFound = False
            Catch ex As Exception
                IsFound = False
            End Try

            Return IsFound
        End Function

        Public Shared Function AddNewPerson(ByVal FirstName As String, ByVal LastName As String, ByVal PhoneNumber As String, ByVal Email As String, ByVal Street As String, ByVal City As String, ByVal State As String, ByVal ZipCode As String) As Integer?
            ' This function will return the new person id if succeeded and null if not
            Dim PersonID As Integer? = Nothing

            Try
                Using connection As New SqlConnection(clsDataAccessSettings.ConnectionString)
                    connection.Open()

                    Using command As New SqlCommand("SP_AddNewPerson", connection)
                        command.CommandType = CommandType.StoredProcedure

                        command.Parameters.AddWithValue("@FirstName", FirstName)
                        command.Parameters.AddWithValue("@LastName", LastName)
                        command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber)
                        command.Parameters.AddWithValue("@Email", Email)
                        command.Parameters.AddWithValue("@Street", Street)
                        command.Parameters.AddWithValue("@City", City)
                        command.Parameters.AddWithValue("@State", State)
                        command.Parameters.AddWithValue("@ZipCode", ZipCode)

                        Dim outputIdParam As New SqlParameter("@NewPersonID", SqlDbType.Int)
                        outputIdParam.Direction = ParameterDirection.Output
                        command.Parameters.Add(outputIdParam)

                        command.ExecuteNonQuery()

                        PersonID = CType(outputIdParam.Value, Integer?)
                    End Using
                End Using
            Catch ex As SqlException
            Catch ex As Exception
            End Try

            Return PersonID
        End Function

        Public Shared Function UpdatePerson(ByVal PersonID As Integer?, ByVal FirstName As String, ByVal LastName As String, ByVal PhoneNumber As String, ByVal Email As String, ByVal Street As String, ByVal City As String, ByVal State As String, ByVal ZipCode As String) As Boolean
            Dim RowAffected As Integer = 0

            Try
                Using connection As New SqlConnection(clsDataAccessSettings.ConnectionString)
                    connection.Open()

                    Using command As New SqlCommand("SP_UpdatePerson", connection)
                        command.CommandType = CommandType.StoredProcedure

                        command.Parameters.AddWithValue("@PersonID", If(PersonID.HasValue, PersonID, DBNull.Value))
                        command.Parameters.AddWithValue("@FirstName", FirstName)
                        command.Parameters.AddWithValue("@LastName", LastName)
                        command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber)
                        command.Parameters.AddWithValue("@Email", Email)
                        command.Parameters.AddWithValue("@Street", Street)
                        command.Parameters.AddWithValue("@City", City)
                        command.Parameters.AddWithValue("@State", State)
                        command.Parameters.AddWithValue("@ZipCode", ZipCode)

                        RowAffected = command.ExecuteNonQuery()
                    End Using
                End Using
            Catch ex As SqlException
            Catch ex As Exception
            End Try

            Return RowAffected > 0
        End Function

        Public Shared Function DeletePerson(ByVal PersonID As Integer?) As Boolean
            Dim RowAffected As Integer = 0

            Try
                Using connection As New SqlConnection(clsDataAccessSettings.ConnectionString)
                    connection.Open()

                    Using command As New SqlCommand("SP_DeletePerson", connection)
                        command.CommandType = CommandType.StoredProcedure

                        command.Parameters.AddWithValue("@PersonID", If(PersonID.HasValue, PersonID, DBNull.Value))

                        RowAffected = command.ExecuteNonQuery()
                    End Using
                End Using
            Catch ex As SqlException
            Catch ex As Exception
            End Try

            Return RowAffected > 0
        End Function

        Public Shared Function DoesPersonExist(ByVal PersonID As Integer?) As Boolean
            Dim IsFound As Boolean = False

            Try
                Using connection As New SqlConnection(clsDataAccessSettings.ConnectionString)
                    connection.Open()

                    Using command As New SqlCommand("SP_DoesPersonExist", connection)
                        command.CommandType = CommandType.StoredProcedure

                        command.Parameters.AddWithValue("@PersonID", If(PersonID.HasValue, PersonID, DBNull.Value))

                        ' @ReturnVal could be any name, and we don't need to add it to the SP, just use it here in the code.
                        Dim returnParameter As New SqlParameter("@ReturnVal", SqlDbType.Int)
                        returnParameter.Direction = ParameterDirection.ReturnValue
                        command.Parameters.Add(returnParameter)

                        command.ExecuteNonQuery()

                        IsFound = CType(returnParameter.Value, Integer) = 1
                    End Using
                End Using
            Catch ex As SqlException
                IsFound = False
            Catch ex As Exception
                IsFound = False
            End Try

            Return IsFound
        End Function

        Public Shared Function GetAllPeople() As DataTable
            Dim dt As New DataTable()

            Try
                Using connection As New SqlConnection(clsDataAccessSettings.ConnectionString)
                    connection.Open()

                    Using command As New SqlCommand("SP_GetAllPeople", connection)
                        command.CommandType = CommandType.StoredProcedure

                        Using reader As SqlDataReader = command.ExecuteReader()
                            If reader.HasRows Then
                                dt.Load(reader)
                            End If
                        End Using
                    End Using
                End Using
            Catch ex As SqlException
            Catch ex As Exception
            End Try

            Return dt
        End Function
    End Class

End Namespace


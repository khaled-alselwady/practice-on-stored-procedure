Imports System.Configuration

Namespace Homework_DataAccess
    Public Class clsDataAccessSettings
        Public Shared ConnectionString As String = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
    End Class
End Namespace

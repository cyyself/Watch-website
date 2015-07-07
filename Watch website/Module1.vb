Imports System.Net
Imports System.IO

Module Module1

    Sub Main()
        Console.WriteLine("Enter the url you want to watch:")
        Dim url As String = Console.ReadLine()
        If url.Contains("http://") Or url.Contains("https://") Then
        Else
            url = "http://" & url
        End If
        Dim last As String = HttpGet(url)
        Console.WriteLine("We will watch this website every 5 second.")
        Do
            Dim justnow As String = ""
            Dim err As Boolean = False
            Try
                justnow = HttpGet(url)
            Catch ex As Exception
                err = True
                Console.WriteLine(Now & " Error.")
            End Try
            If err Then
            Else
                If justnow = last Then
                    Console.WriteLine(Now & " No updates.")
                Else
                    If justnow = "" Then
                        Console.WriteLine(Now & " Error.")
                    Else
                        Console.WriteLine(Now & " The website has been updated.")
                        Beep()
                        System.Diagnostics.Process.Start(url)
                        Exit Do
                    End If

                End If
                System.Threading.Thread.Sleep(5000)
            End If
        Loop
        Console.ReadKey()
    End Sub
    Function HttpGet(URL As String) As String
        Dim request As WebRequest = WebRequest.Create(URL)
        request.Credentials = CredentialCache.DefaultCredentials
        Dim response As WebResponse = request.GetResponse()
        If CType(response, HttpWebResponse).StatusCode = HttpStatusCode.OK Then
            Dim dataStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            reader.Close()
            response.Close()
            Return responseFromServer
        Else
            Return ""
        End If
    End Function
End Module

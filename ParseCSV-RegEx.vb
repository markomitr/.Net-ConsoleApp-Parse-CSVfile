Imports System.IO
Imports System.Text.RegularExpressions
Module Module1

    Sub Main()
        Dim fileIn As New StreamReader("C:\formanova.csv")
        Dim strData As String = ""
        Dim lngCount As Long = 1
        Using writer As StreamWriter = New StreamWriter("myfile.txt")
            While (Not (fileIn.EndOfStream))
                strData = fileIn.ReadLine()
                If (lngCount <> 1) Then
                    Dim podeli() As String = strData.Split(";"c)
                    Dim pom = podeli(1)
                    Dim podeliTel As String = Regex.Replace(pom, "[a-zA-Z\[`~{}\]@\\\|""'\^\.]", "")
                    Dim Ime As String = Regex.Replace(podeli(1), "[0-9,\\/-]", "")
                    Dim tocenTel As String = ""
                    'Console.WriteLine(Ime & ": " & podeliTel)
                    podeliTel = podeliTel.Replace(",", " ")
                    Dim podelTelPOM = podeliTel
                    Dim podeliTelNeMob As String = Regex.Replace(podelTelPOM, "(070|071|072|075|076|077|078)[ ]?[- /]?[ ]?[0-9][0-9][0-9][- ]?[0-9][0-9][0-9]", "")
                    If Not String.IsNullOrEmpty(podeliTelNeMob.Trim) Then
                        tocenTel = Regex.Replace(podeliTel, "(070|071|072|075|076|077|078)[ ]?[- /]?[ ]?[0-9][0-9][0-9][- ]?[0-9][0-9][0-9])", "") podeliTel.Replace(podeliTelNeMob.Trim, "")
                    Else
                        tocenTel = podeliTel
                    End If
                    Dim mList As MatchCollection = Regex.Matches(podelTelPOM, "(070|071|072|075|076|077|078)[ ]?[- /]?[ ]?[0-9][0-9][0-9][- ]?[0-9][0-9][0-9]")                   
                    Dim broevi As New List(Of String)
                    For index = 0 To mList.Count - 1
                        Dim br As String = mList(index).ToString
                        br = Regex.Replace(br, "^(07)", "+3897")
                        broevi.Add(br)
                    Next

                    Dim broeviStr As String = ""

                    For indexBr = 0 To broevi.Count - 1
                        broeviStr += broevi(indexBr).ToString & ";"
                    Next
                    writer.WriteLine(Ime.Trim & ";" & broeviStr)

                    If Not String.IsNullOrEmpty(tocenTel.Trim) Then
                        Dim m1 As Match = Regex.Match(tocenTel, "(070|071|072|075|076|077|078)[ ]?[- /]?[ ]?[0-9][0-9][0-9][- ]?[0-9][0-9][0-9]")
                        tocenTel = tocenTel.Replace(m1.ToString, "")
                        Dim m2 As Match = Regex.Match(tocenTel, "(070|071|072|075|076|077|078)[ ]?[- /]?[ ]?[0-9][0-9][0-9][- ]?[0-9][0-9][0-9]")
                        writer.WriteLine(Ime.Trim & ";" & m1.ToString & ";" & m2.ToString)
                    End If                  
                End If

                lngCount = lngCount + 1
            End While

        End Using
        Console.WriteLine("Zavrsiv")
        Console.ReadLine()
    End Sub

End Module

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.IO;

namespace ParseWord
{


    public static class ParseWord
    {
        static Word._Application application;
        static Word._Document document;
        static Object missingObj = System.Reflection.Missing.Value;
        static Object trueObj = true;
        static Object falseObj = false;

       public static void ParseFile(Stream fileStream1)
        {//создаем обьект приложения word
            application = new Word.Application();
            // создаем путь к файлу
            FileStream fileStream = File.Create("temp.doc", (int)fileStream1.Length);
            // Initialize the bytes array with the stream length and then fill it with data
            byte[] bytesInStream = new byte[fileStream1.Length];
            fileStream1.Read(bytesInStream, 0, bytesInStream.Length);
            // Use write method to write to the file specified above
            fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            Object templatePathObj = "temp.doc";

            try
            {
                document = application.Documents.Add(ref  templatePathObj, ref missingObj, ref missingObj, ref missingObj);
            }
            catch (Exception error)
            {
                document.Close(ref falseObj, ref  missingObj, ref missingObj);
                application.Quit(ref missingObj, ref  missingObj, ref missingObj);
                document = null;
                application = null;
                throw error;
            }
            application.Visible = true;

        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.IO;
using Microsoft.Office.Interop.Word;
using Warehouse.Model.Db;

namespace ParseWord
{


    public static class ParseWord
    {
        static Word._Application application;
        static Word._Document document;
        static Object missingObj = System.Reflection.Missing.Value;
        static Object trueObj = true;
        static Object falseObj = false;

        public static EventCouch ParseFile(Stream fileStream1)
        {//создаем обьект приложения word
            application = new Word.Application();
            // создаем путь к файлу
           var str=Path.GetTempPath();
           var temp = str + "temp" + DateTime.Now.Ticks + ".doc";
           FileStream fileStream = File.Create(temp, (int)fileStream1.Length);
       
            // Initialize the bytes array with the stream length and then fill it with data
            byte[] bytesInStream = new byte[fileStream1.Length];
            fileStream1.Read(bytesInStream, 0, bytesInStream.Length);
            // Use write method to write to the file specified above
            fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            Object templatePathObj = temp;
            object missing = Type.Missing;
            fileStream.Close();
            EventCouch EC = new EventCouch();
            EC.Data_ismenen =  DateTime.Now.Date;
            EC.Kolichestvo = 1;

            try
            {
                Word.Document document = application.Documents.Open(ref templatePathObj, ref missing, ref missing, ref missing, ref missing,
                 ref missing, ref missing, ref missing, ref missing, ref missing,
                 ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
            //  Word.Table table = document.Tables[0]; //define this index depending on the number of table which you want to get
                var t=0;
                var flag = false;
                var countT = document.Tables.Count;
                foreach (Table tb in document.Tables)
                {
                    for (int row = 1; row <= tb.Rows.Count; row++)
                    {
                        if (t == 0)
                        {
                            if (row == 1)
                            {
                                var cell = tb.Cell(row, 2);
                                var text = cell.Range.Text;
                                text = text.Replace("\r\a", "").Replace("\r", "").Replace("\a", "");
                                int Nomer_upakovki;
                                try
                                {
                                    Nomer_upakovki = Convert.ToInt32(text);
                                    EC.Nomer_upakovki = Nomer_upakovki;
                                }
                                catch (Exception e)
                                {
                                }
                            }

                            if (row == 2)
                            {
                                var cell = tb.Cell(row, 2);
                                var text = cell.Range.Text;
                                text = text.Replace("\r\a", "").Replace("\r", "").Replace("\a", "");
                                EC.Sistema = text;
                                cell = tb.Cell(row, 4);
                                text = cell.Range.Text;
                                text = text.Replace("\r\a", "").Replace("\r", "").Replace("\a", "");
                                EC.Mestonahozhdenie_na_sklade = text;
                            }

                            if (row == 3)
                            {
                                var cell = tb.Cell(row, 2);
                                var text = cell.Range.Text;
                                text = text.Replace("\r\a", "").Replace("\r", "").Replace("\a", "");
                                EC.Naimenovanie_izdeliya = text;
                                cell = tb.Cell(row, 4);
                                text = cell.Range.Text;
                                text = text.Replace("\r\a", "").Replace("\r", "").Replace("\a", "");
                                double Ves_brutto;
                                try
                                {
                                    Ves_brutto = Convert.ToDouble(text);
                                    EC.Ves_brutto = (float)Ves_brutto;
                                }
                                catch (Exception e)
                                {
                                }
                            }
                            if (row == 4)
                            {
                                var cell = tb.Cell(row, 2);
                                var text = cell.Range.Text;
                                text = text.Replace("\r\a", "").Replace("\r", "").Replace("\a", "");
                                EC.Zavodskoj_nomer = text;
                            }
                            if (row == 5)
                            {
                                var cell = tb.Cell(row, 2);
                                var text = cell.Range.Text;
                                text = text.Replace("\r\a", "").Replace("\r", "").Replace("\a", "");
                                EC.Prinadlezhnost = text;
                                cell = tb.Cell(row, 5);
                                text = cell.Range.Text;
                                text = text.Replace("\r\a", "").Replace("\r", "").Replace("\a", "");
                                double Dlina;
                                try
                                {
                                    Dlina = Convert.ToDouble(text);
                                    EC.Dlina = (float)Dlina;
                                }
                                catch (Exception e)
                                {
                                }
                            }
                            if (row == 6)
                            {
                                var cell = tb.Cell(row, 2);
                                var text = cell.Range.Text;
                                text = text.Replace("\r\a", "").Replace("\r", "").Replace("\a", "");
                                EC.Otvetstvennyj = text;
                                cell = tb.Cell(row, 5);
                                text = cell.Range.Text;
                                text = text.Replace("\r\a", "").Replace("\r", "").Replace("\a", "");
                                double Shirina;
                                try
                                {
                                    Shirina = Convert.ToDouble(text);
                                    EC.Shirina = (float)Shirina;
                                }
                                catch (Exception e)
                                {
                                }
                            }
                            if (row == 7 )
                            {
                                var cell = tb.Cell(row, 5);
                                var text = cell.Range.Text;
                                text = text.Replace("\r\a", "").Replace("\r", "").Replace("\a", "");
                                double Vysota;
                                try
                                {
                                    Vysota = Convert.ToDouble(text);
                                    EC.Vysota = (float)Vysota;
                                }
                                catch (Exception e)
                                {
                                }
                            }
                            if (row > 10 && row < 31)
                            {
                              
                                var cell = tb.Cell(row, 2);
                                var text = cell.Range.Text;
                                text = text.Replace("\r\a", "").Replace("\r", "").Replace("\a", "");
                                var Naimenovanie_sostavnoj_edinicy = text;
                                cell = tb.Cell(row, 3);
                                text = cell.Range.Text;
                                text = text.Replace("\r\a", "").Replace("\r", "").Replace("\a", "");
                                var Oboznachenie_sostavnoj_edinicy = text;
                                cell = tb.Cell(row, 4);
                                text = cell.Range.Text;
                                text = text.Replace("\r\a", "").Replace("\r", "").Replace("\a", "");
                                int Kolichestvo_sostavnyh_edinic=0;
                                try
                                {
                                    Kolichestvo_sostavnyh_edinic = Convert.ToInt32(text);

                                }
                                catch (Exception e)
                                {
                                }
                                if (!(String.IsNullOrWhiteSpace(Naimenovanie_sostavnoj_edinicy) && String.IsNullOrWhiteSpace(Oboznachenie_sostavnoj_edinicy) && Kolichestvo_sostavnyh_edinic == 0))
                                {
                                    EC.Soderzhimoe.Add(new SubEvent() { Naimenovanie_sostavnoj_edinicy = Naimenovanie_sostavnoj_edinicy, Kolichestvo_sostavnyh_edinic = Kolichestvo_sostavnyh_edinic, Oboznachenie_sostavnoj_edinicy = Oboznachenie_sostavnoj_edinicy });
                                }
                            }
                            if (row == 33)
                            {
                                var cell = tb.Cell(row, 4);
                                var text = cell.Range.Text;
                                text = text.Replace("\r\a", "").Replace("\r", "").Replace("\a", "");
                                DateTime Data_priyoma = DateTime.Now.Date;
                                try
                                {
                                    Data_priyoma = Convert.ToDateTime(text).Date;
                                    EC.Data_priyoma = Data_priyoma;
                                }
                                catch (Exception e)
                                {
                                }
                                flag = true;
                            }

                        }
                        else
                        {
                            if (row > 2 && row < 22)
                            {
                                var cell = tb.Cell(row, 2);
                                var text = cell.Range.Text;
                                text = text.Replace("\r\a", "").Replace("\r", "").Replace("\a", "");
                                var Naimenovanie_sostavnoj_edinicy = text;
                                cell = tb.Cell(row, 3);
                                text = cell.Range.Text;
                                text = text.Replace("\r\a", "").Replace("\r", "").Replace("\a", "");
                                var Oboznachenie_sostavnoj_edinicy = text;
                                cell = tb.Cell(row, 4);
                                text = cell.Range.Text;
                                text = text.Replace("\r\a", "").Replace("\r", "").Replace("\a", "");
                                int Kolichestvo_sostavnyh_edinic = 0;
                                try
                                {
                                    Kolichestvo_sostavnyh_edinic = Convert.ToInt32(text);

                                }
                                catch (Exception e)
                                {
                                }
                                if (!(String.IsNullOrWhiteSpace(Naimenovanie_sostavnoj_edinicy) && String.IsNullOrWhiteSpace(Oboznachenie_sostavnoj_edinicy) && Kolichestvo_sostavnyh_edinic == 0))
                                {
                                    EC.Soderzhimoe.Add(new SubEvent() { Naimenovanie_sostavnoj_edinicy = Naimenovanie_sostavnoj_edinicy, Kolichestvo_sostavnyh_edinic = Kolichestvo_sostavnyh_edinic, Oboznachenie_sostavnoj_edinicy = Oboznachenie_sostavnoj_edinicy });
                                }
                            }
                            if (t==countT-1)
                            {

                                var cell = tb.Cell(24, 4);
                                var text = cell.Range.Text;
                                text = text.Replace("\r\a", "").Replace("\r", "").Replace("\a", "");
                                DateTime Data_priyoma = DateTime.Now.Date;
                                try
                                {
                                    Data_priyoma = Convert.ToDateTime(text).Date;
                                    EC.Data_priyoma = Data_priyoma;
                                }
                                catch (Exception e)
                                {
                                }
                                flag = true;
                            }
                        }
                    }
                    if (flag)
                        break;
                    t++;
                }
                
                document.Close(ref missing, ref missing, ref missing);
                File.Delete(templatePathObj.ToString());
            }
            catch (Exception error)
            {
                document.Close(ref falseObj, ref  missingObj, ref missingObj);
                File.Delete(templatePathObj.ToString());
                application.Quit(ref missingObj, ref  missingObj, ref missingObj);
                document = null;
                application = null;
            }
            application.Visible = false;
            return EC;
        }
        
    }
}

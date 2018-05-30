using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;

namespace ParseWord
{


    public static class ParseWord
    {
        Word._Application application;
        Word._Document document;
        Object missingObj = System.Reflection.Missing.Value;
        Object trueObj = true;
        Object falseObj = false;
        
    }
}

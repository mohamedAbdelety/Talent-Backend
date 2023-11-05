using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Models
{
    public class GlobalConstants
    {
       
        static public string Excel_Content_Type { get => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
        static public string ExcelSheetErrorName { get => $"ValidationFailure-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx"; }
        static public string ImgsUploadPath { get => $"wwwroot\\images"; }

    }
}

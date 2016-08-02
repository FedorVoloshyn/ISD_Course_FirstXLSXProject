using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace FirstXLSXProject
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SpreadsheetDocument spreadsheetDocument =
    SpreadsheetDocument.Open("test.xlsx", false))
            {
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                SharedStringTablePart sstpart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
                SharedStringTable sst = sstpart.SharedStringTable;

                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                Worksheet sheet = worksheetPart.Worksheet;

                var cells = sheet.Descendants<Cell>();
                var rows = sheet.Descendants<Row>();

                Console.WriteLine("Row count = {0}", rows.LongCount());
                Console.WriteLine("Cell count = {0}", cells.LongCount());

                // One way: go through each cell in the sheet
                foreach (Cell cell in cells)
                {
                    if ((cell.DataType != null) && (cell.DataType == CellValues.SharedString))
                    {
                        int ssid = int.Parse(cell.CellValue.Text);
                        string str = sst.ChildElements[ssid].InnerText;
                        Console.WriteLine("{1}", ssid, str);
                    }
                    else if (cell.CellValue != null)
                    {
                        Console.WriteLine("{0}", cell.CellValue.Text);
                    }
                }
            }
        }
    }
}

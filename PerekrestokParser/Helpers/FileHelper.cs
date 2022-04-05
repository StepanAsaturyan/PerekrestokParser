using ClosedXML.Excel;
using PerekrestokParser.Models;

namespace PerekrestokParser.Helpers
{
    internal class FileHelper
    {
        public void SaveDataToFile(Product[] collection, string worksheetName, string divisionName)
        {
            XLWorkbook workbook;

            if (File.Exists("Выгрузка.xlsx"))
            {
                workbook = new XLWorkbook("Выгрузка.xlsx");
            }
            else
            {
                workbook = new();
            }
            IXLWorksheet worksheet = null;
            if (workbook.Worksheets.Any(w => w.Name == worksheetName))
            {
                worksheet = workbook.Worksheets.Where(w => w.Name == worksheetName).FirstOrDefault();
            }
            else
            {
                worksheet = workbook.Worksheets.Add(worksheetName ?? divisionName);
                worksheet.Cell("A1").SetActive();
                IXLCell cellA1 = worksheet.Cell("A1");
                IXLCell firstInRow = worksheet.Cell("A2");
                cellA1.Value = "№";
                cellA1.CellRight().Value = "Товар";
                cellA1.CellRight(2).Value = "Старая цена";
                cellA1.CellRight(3).Value = "Новая цена";
                cellA1.CellRight(4).Value = "Рейтинг";
                cellA1.CellRight(5).Value = "Голосов";
                cellA1.CellRight(6).Value = "Цена за шт.";
            }

            

            int lastrow = worksheet.LastRowUsed().RowNumber();
            IXLCell freeEmptyCell = worksheet.Cell($"A{lastrow + 1}");
            freeEmptyCell.Value = divisionName;
            worksheet.Range(freeEmptyCell, worksheet.Cell(worksheet.LastRowUsed().RowNumber(), 7)).Merge();
            worksheet.LastRowUsed().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.LastRowUsed().Style.Font.Bold = true;
            freeEmptyCell = freeEmptyCell.CellBelow();

            for (int i = 0; i < collection.Length; i++)
            {
                freeEmptyCell.Value = i + 1;
                freeEmptyCell.CellRight(1).Value = collection[i].Name;
                freeEmptyCell.CellRight(2).Value = collection[i].OldPrice;
                freeEmptyCell.CellRight(3).Value = collection[i].ActualPrice;
                freeEmptyCell.CellRight(4).Value = collection[i].Rating;
                freeEmptyCell.CellRight(5).Value = collection[i].NumOfVotes;
                freeEmptyCell.CellRight(6).Value = collection[i].PricePerPiece;
                freeEmptyCell = freeEmptyCell.CellBelow();
            }

            worksheet.Columns().AdjustToContents();
            worksheet.Column(2).Width = 70;
            worksheet.Columns(3, 4).Width = 13;
            worksheet.Columns(3, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Range(worksheet.Cell(1,1), freeEmptyCell.CellRight(6).CellAbove()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Range(worksheet.Cell(1,1), freeEmptyCell.CellRight(6).CellAbove()).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            worksheet.Range(worksheet.Cell(1,1), worksheet.Cell(1, 1).CellRight(6)).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            worksheet.Row(1).Style.Font.Bold = true;

            workbook.SaveAs("Выгрузка.xlsx");
        }
    }
}

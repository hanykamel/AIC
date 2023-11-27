using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.Service.Helper
{
    public static class ExcelStyle
    {
        public static XSSFCellStyle SetWorkbookStyle(IWorkbook workbook)
        {
            XSSFFont headerFont = (XSSFFont)workbook.CreateFont();
            //headerFont.FontHeightInPoints = (short)12;
            headerFont.FontName = "Calibri";
            headerFont.Color = IndexedColors.Black.Index;
            headerFont.IsBold = true;
            headerFont.IsItalic = false;

            XSSFCellStyle headerStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            headerStyle.WrapText = true;
            headerStyle.FillForegroundColor = IndexedColors.Aqua.Index;
            headerStyle.FillPattern = FillPattern.SolidForeground;
            headerStyle.Alignment = HorizontalAlignment.Center;
            headerStyle.VerticalAlignment = VerticalAlignment.Center;
            headerStyle.BorderBottom = BorderStyle.Thin;
            headerStyle.BorderTop = BorderStyle.Thin;
            headerStyle.BorderLeft = BorderStyle.Thin;
            headerStyle.BorderRight = BorderStyle.Thin;
            headerStyle.SetFont(headerFont);
            return headerStyle;
        }
    }
}

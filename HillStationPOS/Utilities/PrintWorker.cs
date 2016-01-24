using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using HillStationPOS.ViewModel;
using Spire.Pdf;
using Syncfusion.ReportWriter;
using Syncfusion.Windows.Reports;

namespace HillStationPOS.Utilities
{
    internal class PrintWorker
    {
        public void PrintOrder(MainViewModel model)
        {
            var reportStream =
                Assembly.GetExecutingAssembly().GetManifestResourceStream("HillStationPOS.Reports.Order.rdlc");

            var writer = new ReportWriter {ReportProcessingMode = ProcessingMode.Local};
            writer.DataSources.Clear();
            writer.DataSources.Add(new ReportDataSource {Name = "OrderItems", Value = model.Order});
            writer.LoadReport(reportStream);

            var parameters = new List<ReportParameter>();
            foreach (var parameter in writer.GetParameters())
            {
                var param = new ReportParameter
                {
                    Name = parameter.Name,
                    Prompt = parameter.Prompt
                };
                switch (param.Name)
                {
                    case "OrderNumber":
                        param.Values.Add(model.OrderNumber);
                        break;
                    case "Address":
                        param.Values.Add(model.Address);
                        break;
                    default:
                        throw new InvalidEnumArgumentException(@"Invalid parameter name: " + param.Name);
                }
                parameters.Add(param);
            }

            writer.SetParameters(parameters);

            var stream = new MemoryStream();
            writer.Save(@"D:\Documents\xxx.pdf", WriterFormat.PDF);
            writer.Save(stream, WriterFormat.PDF);

            var pdf = new PdfDocument(stream);

            var size = pdf.Pages[0].Size;
            var paper = new PaperSize("Custom", (int) size.Width, (int) size.Height)
            {
                RawKind = (int) PaperKind.Custom
            };
            pdf.PageScaling = PdfPrintPageScaling.ActualSize;
            var printDocument = pdf.PrintDocument;
            //            printDocument.PrinterSettings.Copies = 2;
            printDocument.DefaultPageSettings.PaperSize = paper;

            //            printDocument.Print();
        }
    }
}
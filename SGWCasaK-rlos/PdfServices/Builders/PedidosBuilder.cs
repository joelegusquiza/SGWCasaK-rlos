using Core.DTOs.Pedidos;
using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PdfServices.Builders
{
	public static class PedidosBuilder
	{
		public static Document Build(PedidosPdfModel model)
		{
			var document = new Document
			{
				Info =
				{
					Title = $"Peiddo Nro: {model.Id}",
					Author = "DMS",
					Subject = "Invoice PDF"
				}
			};
			var section = document.AddSection();

			SetupDocument(document, section);
			AddHeader(section, model);
			AddInvoiceSummary(section, model);
			AddInvoiceItems(section, model);			

			return document;
		}

		private static void SetupDocument(Document document, Section section)
		{
			//Set Font
			var style = document.Styles["Normal"];
			style.Font.Name = "Arial";
			//Page Setup
			var pageSetup = document.DefaultPageSetup.Clone();
			pageSetup.Orientation = Orientation.Portrait;
			Unit pageWidth, pageHeight;
			PageSetup.GetPageSize(PageFormat.Letter, out pageWidth, out pageHeight);
			pageSetup.LeftMargin = new Unit(0.5, UnitType.Inch);
			pageSetup.RightMargin = new Unit(0.5, UnitType.Inch);
			pageSetup.TopMargin = new Unit(0.27, UnitType.Inch);
			pageSetup.BottomMargin = new Unit(0.5, UnitType.Inch);
			pageSetup.PageWidth = pageWidth;
			pageSetup.PageHeight = pageHeight;
			pageSetup.FooterDistance = new Unit(-0.1, UnitType.Centimeter);
			pageSetup.HeaderDistance = new Unit(0.7, UnitType.Centimeter);
			pageSetup.DifferentFirstPageHeaderFooter = false;
			section.PageSetup = pageSetup;
		}

		private static void AddHeader(Section section, PedidosPdfModel model)
		{
			//HeaderFooter header = section.Headers.Primary;
			var table = section.AddTable();
			table.AddColumn(new Unit(3.2, UnitType.Centimeter));
			table.AddColumn(new Unit(8.9, UnitType.Centimeter));
			table.AddColumn(new Unit(7.4, UnitType.Centimeter));

			var row = table.AddRow();
			row.Format.Font.Size = new Unit(9, UnitType.Point);
			row.Cells[0].AddParagraph(Environment.NewLine);
			row.Cells[0].AddParagraph("PEDIDO #").Format.Font.Bold = true; ;
			row.Cells[0].AddParagraph("FECHA");
			

			row.Cells[1].AddParagraph(Environment.NewLine);
			row.Cells[1].AddParagraph(model.Id.ToString().ToUpper()).Format.Font.Bold = true;
			row.Cells[1].AddParagraph(model.DateCreated.ToString("dd/MM/yyyy").ToUpper());

			row.Cells[2].AddParagraph("CASA K-RLOS").Format.Alignment = ParagraphAlignment.Right;
			row.Cells[2].AddParagraph("Ruta 3 Gral Elizardo Aquino Km 325").Format.Alignment = ParagraphAlignment.Right;
			row.Cells[2].AddParagraph("Santa Rosa del Aguaray").Format.Alignment = ParagraphAlignment.Right;
			row.Cells[2].AddParagraph("Telefono 0971 - 222 333").Format.Alignment = ParagraphAlignment.Right;
			//Add Image
			//var fileName = PdfHelpers.DownloadNewLogo(model.Branding);
			//row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
			//row.Cells[2].VerticalAlignment = VerticalAlignment.Top;
			//var par = row.Cells[2].AddParagraph();
			//par.Format.Borders.DistanceFromTop = new Unit(0.33, UnitType.Centimeter);
			//var image = par.AddImage(fileName);
			//image.LockAspectRatio = true;
			//image.Width = new Unit(6, UnitType.Centimeter);
			//section.Headers.FirstPage = header.Clone();

		}

		private static void AddInvoiceSummary(Section section, PedidosPdfModel model)
		{
			
			var table = section.AddTable();
			table.AddColumn(new Unit(9.7, UnitType.Centimeter));
			table.AddColumn(new Unit(9.8, UnitType.Centimeter));
			var row = table.AddRow();

			//var par = row.Cells[0].AddParagraph($"{model.CustomerName.ToUpper()}");
			//par.Format.Borders.DistanceFromTop = new Unit(-0.40, UnitType.Centimeter);
			//par.Format.LeftIndent = new Unit(1.71, UnitType.Centimeter);
			//par = row.Cells[0].AddParagraph(model.Address.ToUpper() ?? "");
			//par.Format.Borders.DistanceFromTop = new Unit(-0.40, UnitType.Centimeter);
			//par.Format.LeftIndent = new Unit(1.71, UnitType.Centimeter);


			table = row.Cells[1].AddTextFrame().AddTable();
			table.AddColumn(new Unit(2.5, UnitType.Centimeter));
			table.AddColumn(new Unit(7.30, UnitType.Centimeter));
			table.TopPadding = new Unit(0.55, UnitType.Centimeter);
			row = table.AddRow();

			var par = row.Cells[1].AddParagraph($"Monto Total: {model.MontoTotal}".ToUpper());
		
			par.Format.Alignment = ParagraphAlignment.Center;
			par.Format.Shading.Color = Color.Parse("black");
			par.Format.Font.Color = Color.Parse("white");
			par.Format.Font.Bold = true;
			par.Format.Font.Size = new Unit(12, UnitType.Point);
			par.Format.Borders.DistanceFromTop = new Unit(0.2, UnitType.Centimeter);
			par.Format.Borders.DistanceFromBottom = new Unit(0.2, UnitType.Centimeter);

		}

		private static void AddInvoiceItems(Section section, PedidosPdfModel model)
		{
			var table = section.AddTable();
			table.AddColumn(new Unit(4.5, UnitType.Centimeter));
			table.AddColumn(new Unit(12.3, UnitType.Centimeter));
			table.AddColumn(new Unit(2.5, UnitType.Centimeter));

			var row = table.AddRow();

			row.Shading.Color = Color.Parse("black");

			row.Format.Font.Color = Color.Parse("white");
			row.Format.Font.Bold = true;

			var par = row.Cells[0].AddParagraph("CANT");
			par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);
			par.Format.Alignment = ParagraphAlignment.Left;
			row.Format.Borders.DistanceFromTop = new Unit(0.1, UnitType.Centimeter);
			row.Format.Borders.DistanceFromBottom = new Unit(0.1, UnitType.Centimeter);
			par = row.Cells[1].AddParagraph("DETALLE");
			par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);
			par.Format.Alignment = ParagraphAlignment.Left;
			row.Format.Borders.DistanceFromTop = new Unit(0.1, UnitType.Centimeter);
			row.Format.Borders.DistanceFromBottom = new Unit(0.1, UnitType.Centimeter);

			par = row.Cells[2].AddParagraph("TOTAL");
			par.Format.RightIndent = new Unit(3, UnitType.Millimeter);
			row.Format.Borders.DistanceFromTop = new Unit(0.1, UnitType.Centimeter);
			row.Format.Borders.DistanceFromBottom = new Unit(0.1, UnitType.Centimeter);
			par.Format.Alignment = ParagraphAlignment.Right;
			//row = table.AddRow();
			var invoiceCounter = 0;
			for (var i = 0; i < model.DetallePedido.Count; i++)
			{
				var item = model.DetallePedido[i];
				row = table.AddRow();
				row.Format.Borders.DistanceFromTop = new Unit(0.2, UnitType.Centimeter);
				row.Format.Borders.DistanceFromBottom = new Unit(0.2, UnitType.Centimeter);
				row.Format.Font.Size = new Unit(8, UnitType.Point);
				
				par = row.Cells[0].AddParagraph(item.Cantidad.ToString());
				par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);
				par = row.Cells[1].AddParagraph(item.Descripcion);
				par = row.Cells[2].AddParagraph(item.PrecioVenta.ToString());
				par.Format.RightIndent = new Unit(3, UnitType.Millimeter);
				par.Format.Alignment = ParagraphAlignment.Right;
				if (invoiceCounter > 0)
				{
					row.Cells[0].Borders.Top.Visible = true;
					row.Cells[1].Borders.Top.Visible = true;
					row.Cells[2].Borders.Top.Visible = true;
					row.Cells[0].Borders.Top.Width = new Unit(.1, UnitType.Millimeter);
					row.Cells[1].Borders.Top.Width = new Unit(.1, UnitType.Millimeter);
					row.Cells[2].Borders.Top.Width = new Unit(.1, UnitType.Millimeter);
				}

				invoiceCounter++;
			}			

			row = table.AddRow();
			row = table.AddRow();

			//row.TopPadding = new Unit(0.2, UnitType.Centimeter);
			par = row.Cells[0].AddParagraph($"TOTAL");
			row.Cells[0].MergeRight = 1;
			par.Format.Font.Bold = true;
			par.Format.Font.Size = new Unit(12, UnitType.Point);
			par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);


			par = row.Cells[2].AddParagraph($"{model.MontoTotal}");
			par.Format.Font.Bold = true;
			par.Format.Font.Size = new Unit(12, UnitType.Point);
			par.Format.Alignment = ParagraphAlignment.Right;
			par.Format.RightIndent = new Unit(3, UnitType.Millimeter);


		}
		
	}
}

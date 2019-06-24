using Core.DTOs.Pedidos;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
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
			//AddInvoiceSummary(section, model);
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
			table.AddColumn(new Unit(2.8, UnitType.Centimeter));
			table.AddColumn(new Unit(10, UnitType.Centimeter));
			table.AddColumn(new Unit(0.3, UnitType.Centimeter));
			table.AddColumn(new Unit(3.2, UnitType.Centimeter));
			table.AddColumn(new Unit(3.2, UnitType.Centimeter));

			var row = table.AddRow();
			row.Format.Font.Size = new Unit(9, UnitType.Point);
			var logo = $"{Environment.CurrentDirectory}\\wwwroot\\images\\logo.png";
			var image = row.Cells[0].AddImage(logo);
			row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
			image.LockAspectRatio = true;
			image.Width = new Unit(2, UnitType.Centimeter);
			row.Cells[0].Borders.Top.Visible = true;
			row.Cells[0].Borders.Left.Visible = true;
			row.Cells[0].Borders.Bottom.Visible = true;

			row.Cells[1].AddParagraph("CASA K-RLOS").Format.Alignment = ParagraphAlignment.Center;
			row.Cells[1].AddParagraph("Ruta 3 Gral Elizardo Aquino Km 325").Format.Alignment = ParagraphAlignment.Center;
			row.Cells[1].AddParagraph("Santa Rosa del Aguaray").Format.Alignment = ParagraphAlignment.Center;
			row.Cells[1].AddParagraph("Telefono 0971 - 222 333").Format.Alignment = ParagraphAlignment.Center;
			row.Cells[1].Borders.Visible = true;
			row.Cells[1].Borders.Left.Visible = false;
			row.Cells[1].Borders.Width = new Unit(.1, UnitType.Millimeter);

			row.Cells[3].AddParagraph("PEDIDO #").Format.Font.Bold = true; ;
			row.Cells[3].AddParagraph("FECHA");
			row.Cells[3].Borders.Visible = true;
			row.Cells[3].Borders.Width = new Unit(.1, UnitType.Millimeter);

			row.Cells[4].AddParagraph(model.Id.ToString().ToUpper()).Format.Font.Bold = true;
			row.Cells[4].AddParagraph(model.DateCreated.ToString("dd/MM/yyyy").ToUpper());
			row.Cells[4].Borders.Visible = true;
			row.Cells[4].Borders.Width = new Unit(.1, UnitType.Millimeter);
			section.AddParagraph(Environment.NewLine);
			section.AddParagraph(Environment.NewLine);
			//Add Image
			


		}

		//private static void AddInvoiceSummary(Section section, PedidosPdfModel model)
		//{
			
		//	var table = section.AddTable();
		//	table.AddColumn(new Unit(9.7, UnitType.Centimeter));
		//	table.AddColumn(new Unit(9.8, UnitType.Centimeter));
		//	var row = table.AddRow();

		//	//var par = row.Cells[0].AddParagraph($"{model.CustomerName.ToUpper()}");
		//	//par.Format.Borders.DistanceFromTop = new Unit(-0.40, UnitType.Centimeter);
		//	//par.Format.LeftIndent = new Unit(1.71, UnitType.Centimeter);
		//	//par = row.Cells[0].AddParagraph(model.Address.ToUpper() ?? "");
		//	//par.Format.Borders.DistanceFromTop = new Unit(-0.40, UnitType.Centimeter);
		//	//par.Format.LeftIndent = new Unit(1.71, UnitType.Centimeter);


		//	table = row.Cells[1].AddTextFrame().AddTable();
		//	table.AddColumn(new Unit(2.5, UnitType.Centimeter));
		//	table.AddColumn(new Unit(7.30, UnitType.Centimeter));
		//	table.TopPadding = new Unit(0.55, UnitType.Centimeter);
		//	row = table.AddRow();

		//	var par = row.Cells[1].AddParagraph($"Monto Total: {model.MontoTotal}".ToUpper());
		
		//	par.Format.Alignment = ParagraphAlignment.Center;
		//	par.Format.Shading.Color = Color.Parse("black");
		//	par.Format.Font.Color = Color.Parse("white");
		//	par.Format.Font.Bold = true;
		//	par.Format.Font.Size = new Unit(12, UnitType.Point);
		//	par.Format.Borders.DistanceFromTop = new Unit(0.2, UnitType.Centimeter);
		//	par.Format.Borders.DistanceFromBottom = new Unit(0.2, UnitType.Centimeter);

		//}

		private static void AddInvoiceItems(Section section, PedidosPdfModel model)
		{
			var table = section.AddTable();
			table.AddColumn(new Unit(2, UnitType.Centimeter));
			table.AddColumn(new Unit(9.5, UnitType.Centimeter));
			table.AddColumn(new Unit(3.9, UnitType.Centimeter));
			table.AddColumn(new Unit(3.9, UnitType.Centimeter));

			var row = table.AddRow();
			row.Borders.Visible = true;
			row.Borders.Width = new Unit(.1, UnitType.Millimeter);
			row.Shading.Color = Color.Parse("white");

			row.Format.Font.Color = Color.Parse("black");
			row.Format.Font.Bold = true;

			var par = row.Cells[0].AddParagraph("CANT");
			par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);
			par.Format.Alignment = ParagraphAlignment.Left;
			row.Format.Borders.DistanceFromTop = new Unit(0.2, UnitType.Centimeter);
			row.Format.Borders.DistanceFromBottom = new Unit(0.2, UnitType.Centimeter);
			par = row.Cells[1].AddParagraph("DETALLE");
			par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);
			par.Format.Alignment = ParagraphAlignment.Left;
			par = row.Cells[2].AddParagraph("PRECIO UNITARIO");
			par.Format.RightIndent = new Unit(3, UnitType.Millimeter);
			par.Format.Alignment = ParagraphAlignment.Center;
			par = row.Cells[3].AddParagraph("PRECIO TOTAL");
			par.Format.RightIndent = new Unit(3, UnitType.Millimeter);
			
			par.Format.Alignment = ParagraphAlignment.Center;
			//row = table.AddRow();
			var invoiceCounter = 0;
			for (var i = 0; i < model.DetallePedido.Count; i++)
			{
				var item = model.DetallePedido[i];
				row = table.AddRow();
				row.Cells[0].Borders.Visible = true;
				row.Cells[1].Borders.Visible = true;
				row.Cells[2].Borders.Visible = true;
				row.Cells[3].Borders.Visible = true;
				row.Cells[0].Borders.Width = new Unit(.1, UnitType.Millimeter);
				row.Cells[1].Borders.Width = new Unit(.1, UnitType.Millimeter);
				row.Cells[2].Borders.Width = new Unit(.1, UnitType.Millimeter);
				row.Cells[3].Borders.Width = new Unit(.1, UnitType.Millimeter);
				row.Format.Borders.DistanceFromTop = new Unit(0.2, UnitType.Centimeter);
				row.Format.Borders.DistanceFromBottom = new Unit(0.2, UnitType.Centimeter);
				row.Format.Font.Size = new Unit(8, UnitType.Point);
				
				par = row.Cells[0].AddParagraph(item.Cantidad.ToString());
				par.Format.Alignment = ParagraphAlignment.Center;

				par = row.Cells[1].AddParagraph(item.Descripcion);
				par.Format.Alignment = ParagraphAlignment.Left;
				par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);
				par = row.Cells[2].AddParagraph(item.PrecioVenta.ToString());
				par.Format.Alignment = ParagraphAlignment.Center;
				par = row.Cells[3].AddParagraph(item.MontoTotal.ToString());
				par.Format.Alignment = ParagraphAlignment.Center;

			}			

			row = table.AddRow();
			row = table.AddRow();

			//row.TopPadding = new Unit(0.2, UnitType.Centimeter);
			par = row.Cells[0].AddParagraph($"TOTAL");
			row.Cells[0].MergeRight = 2;
			par.Format.Alignment = ParagraphAlignment.Right;
			par.Format.Font.Bold = true;
			par.Format.Font.Size = new Unit(10, UnitType.Point);
			par.Format.RightIndent = new Unit(4, UnitType.Millimeter);


			par = row.Cells[3].AddParagraph($"{model.MontoTotal}");
			par.Format.Font.Bold = true;
			par.Format.Font.Size = new Unit(10, UnitType.Point);
			par.Format.Alignment = ParagraphAlignment.Center;
			par.Format.RightIndent = new Unit(3, UnitType.Millimeter);


		}
		
	}
}

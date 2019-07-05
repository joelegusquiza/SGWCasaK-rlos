using Core.DTOs.Pdf;
using Core.DTOs.Reportes;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PdfServices.Builders
{
	public class ReporteGeneralBuilder
	{
		public static Document Build(ReportesIndexViewModel model)
		{
			var document = new Document
			{
				Info =
				{
					Title = $"Reporte",
					Author = "DMS",
					Subject = "Reporte PDF"
				}
			};
			var section = document.AddSection();

			SetupDocument(document, section);
			AddHeader(section, model);
			AddSummary(section, model);
			//AddInvoiceSummary(section, model);
			AddVentaItems(section, model);
			AddCompraItems(section, model);
			AddPedidoItems(section, model);
			return document;
		}

		private static void AddSummary(Section section, ReportesIndexViewModel model)
		{
			var table = section.AddTable();
			table.AddColumn(new Unit(1.5, UnitType.Centimeter));
			table.AddColumn(new Unit(4.325, UnitType.Centimeter));
			table.AddColumn(new Unit(3.825, UnitType.Centimeter));
			table.AddColumn(new Unit(1.5, UnitType.Centimeter));
			table.AddColumn(new Unit(4.325, UnitType.Centimeter));
			table.AddColumn(new Unit(3.825, UnitType.Centimeter));

			var row = table.AddRow();
			table.Borders.Visible = true;
			row.Shading.Color = Color.Parse("black");

			row.Format.Font.Color = Color.Parse("white");
			row.Format.Font.Bold = true;

			var par = row.Cells[0].AddParagraph("VENTAS");
			row.Cells[0].MergeRight = 2;
			par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);
			par.Format.Alignment = ParagraphAlignment.Left;
			row.Format.Borders.DistanceFromTop = new Unit(0.1, UnitType.Centimeter);
			row.Format.Borders.DistanceFromBottom = new Unit(0.1, UnitType.Centimeter);
			par = row.Cells[3].AddParagraph("COMPRAS");
			row.Cells[3].MergeRight = 2;
			par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);
			par.Format.Alignment = ParagraphAlignment.Left;
			row.Format.Borders.DistanceFromTop = new Unit(0.1, UnitType.Centimeter);
			row.Format.Borders.DistanceFromBottom = new Unit(0.1, UnitType.Centimeter);
		


			row = table.AddRow();
			row.Cells[0].MergeRight = 0;
			par = row.Cells[0].AddParagraph("CANT");
			par = row.Cells[1].AddParagraph("DESCRIPCION");
			par = row.Cells[2].AddParagraph("MONTO");
			par = row.Cells[3].AddParagraph("CANT");
			par = row.Cells[4].AddParagraph("DESCRIPCION");
			par = row.Cells[5].AddParagraph("MONTO");

			
			row = table.AddRow();
			par = row.Cells[0].AddParagraph(model.Ventas.CantVentasPendientes.ToString());
			par = row.Cells[1].AddParagraph("VENTAS POR COBRAS");
			par = row.Cells[2].AddParagraph(model.Ventas.MontoTotalPendientes.ToString());
			par = row.Cells[3].AddParagraph(model.Compras.CantComprasPendientes.ToString());
			par = row.Cells[4].AddParagraph("COMPRAS A PAGAR");
			par = row.Cells[5].AddParagraph(model.Compras.MontoTotalPendientes.ToString());

			row = table.AddRow();
			par = row.Cells[0].AddParagraph(model.Ventas.CantVentasCobradas.ToString());
			par = row.Cells[1].AddParagraph("VENTAS COBRADAS");
			par = row.Cells[2].AddParagraph(model.Ventas.MontoTotalCobradas.ToString());
			par = row.Cells[3].AddParagraph(model.Compras.CantComprasPagado.ToString());
			par = row.Cells[4].AddParagraph("COMPRAS PAGADAS");
			par = row.Cells[5].AddParagraph(model.Compras.MontoTotalPagado.ToString());

			row = table.AddRow();
			par = row.Cells[0].AddParagraph(model.Ventas.CantVentas.ToString());
			par = row.Cells[1].AddParagraph("TOTAL VENTAS");
			par = row.Cells[2].AddParagraph(model.Ventas.MontoTotal.ToString());
			par = row.Cells[3].AddParagraph(model.Compras.CantCompras.ToString());
			par = row.Cells[4].AddParagraph("TOTAL COMPRAS");
			par = row.Cells[5].AddParagraph(model.Compras.MontoTotal.ToString());



			section.AddParagraph(Environment.NewLine);
			section.AddParagraph(Environment.NewLine);


		}

		private static void AddPedidoItems(Section section, ReportesIndexViewModel model)
		{

			var table = section.AddTable();
			table.AddColumn(new Unit(19.3, UnitType.Centimeter));

			var row = table.AddRow();

			row.Shading.Color = Color.Parse("black");

			row.Format.Font.Color = Color.Parse("white");
			row.Format.Font.Bold = true;

			var par = row.Cells[0].AddParagraph("PEDIDOS");
			par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);
			par.Format.Alignment = ParagraphAlignment.Left;
			row.Format.Borders.DistanceFromTop = new Unit(0.1, UnitType.Centimeter);
			row.Format.Borders.DistanceFromBottom = new Unit(0.1, UnitType.Centimeter);

			table = section.AddTable();
			table.AddColumn(new Unit(2, UnitType.Centimeter));
			table.AddColumn(new Unit(6.5, UnitType.Centimeter));
			table.AddColumn(new Unit(3.9, UnitType.Centimeter));
			table.AddColumn(new Unit(3.9, UnitType.Centimeter));
			table.AddColumn(new Unit(3.0, UnitType.Centimeter));
			row = table.AddRow();
			row.Borders.Visible = true;
			row.Borders.Width = new Unit(.1, UnitType.Millimeter);
			row.Shading.Color = Color.Parse("white");

			row.Format.Font.Color = Color.Parse("black");
			row.Format.Font.Bold = true;

			par = row.Cells[0].AddParagraph("#");
			par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);
			par.Format.Alignment = ParagraphAlignment.Center;
			row.Format.Borders.DistanceFromTop = new Unit(0.2, UnitType.Centimeter);
			row.Format.Borders.DistanceFromBottom = new Unit(0.2, UnitType.Centimeter);
			par = row.Cells[1].AddParagraph("Cliente");
			par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);
			par.Format.Alignment = ParagraphAlignment.Center;
			par = row.Cells[2].AddParagraph("Monto Total");
			par.Format.RightIndent = new Unit(3, UnitType.Millimeter);
			par.Format.Alignment = ParagraphAlignment.Center;
			par = row.Cells[3].AddParagraph("Estado");
			par.Format.RightIndent = new Unit(3, UnitType.Millimeter);
			par = row.Cells[4].AddParagraph("Con Delivery");
			par.Format.RightIndent = new Unit(3, UnitType.Millimeter);

			par.Format.Alignment = ParagraphAlignment.Center;
			//row = table.AddRow();

			for (var i = 0; i < model.Pedidos.Detalle.Count; i++)
			{
				var item = model.Pedidos.Detalle[i];
				row = table.AddRow();
				row.Cells[0].Borders.Visible = true;
				row.Cells[1].Borders.Visible = true;
				row.Cells[2].Borders.Visible = true;
				row.Cells[3].Borders.Visible = true;
				row.Cells[4].Borders.Visible = true;
				row.Cells[0].Borders.Width = new Unit(.1, UnitType.Millimeter);
				row.Cells[1].Borders.Width = new Unit(.1, UnitType.Millimeter);
				row.Cells[2].Borders.Width = new Unit(.1, UnitType.Millimeter);
				row.Cells[3].Borders.Width = new Unit(.1, UnitType.Millimeter);
				row.Cells[4].Borders.Width = new Unit(.1, UnitType.Millimeter);
				row.Format.Borders.DistanceFromTop = new Unit(0.2, UnitType.Centimeter);
				row.Format.Borders.DistanceFromBottom = new Unit(0.2, UnitType.Centimeter);
				row.Format.Font.Size = new Unit(8, UnitType.Point);

				par = row.Cells[0].AddParagraph(item.Id.ToString());
				par.Format.Alignment = ParagraphAlignment.Center;

				par = row.Cells[1].AddParagraph(item.DisplayName);
				par.Format.Alignment = ParagraphAlignment.Center;
				par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);
				par = row.Cells[2].AddParagraph(item.MontoTotal.ToString());
				par.Format.Alignment = ParagraphAlignment.Center;
				par = row.Cells[3].AddParagraph(item.DateCreated.ToString("dd/MM/yyyy"));
				par.Format.Alignment = ParagraphAlignment.Center;
				par = row.Cells[4].AddParagraph(item.Delivery ? "SI": "NO");
				par.Format.Alignment = ParagraphAlignment.Center;

			}

			row = table.AddRow();
			row = table.AddRow();


			//par = row.Cells[0].AddParagraph($"TOTAL");
			//row.Cells[0].MergeRight = 2;
			//par.Format.Alignment = ParagraphAlignment.Right;
			//par.Format.Font.Bold = true;
			//par.Format.Font.Size = new Unit(10, UnitType.Point);
			//par.Format.RightIndent = new Unit(4, UnitType.Millimeter);


			//par = row.Cells[3].AddParagraph($"{model.MontoTotal}");
			//par.Format.Font.Bold = true;
			//par.Format.Font.Size = new Unit(10, UnitType.Point);
			//par.Format.Alignment = ParagraphAlignment.Center;
			//par.Format.RightIndent = new Unit(3, UnitType.Millimeter);


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

		private static void AddHeader(Section section, ReportesIndexViewModel model)
		{
			//HeaderFooter header = section.Headers.Primary;
			var table = section.AddTable();
			table.AddColumn(new Unit(2.8, UnitType.Centimeter));
			table.AddColumn(new Unit(16.5, UnitType.Centimeter));


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
		

		
			section.AddParagraph(Environment.NewLine);
			section.AddParagraph(Environment.NewLine);
			//Add Image



		}		

		private static void AddVentaItems(Section section, ReportesIndexViewModel model)
		{


			var table = section.AddTable();
			table.AddColumn(new Unit(19.3, UnitType.Centimeter));
			
			var row = table.AddRow();

			row.Shading.Color = Color.Parse("black");

			row.Format.Font.Color = Color.Parse("white");
			row.Format.Font.Bold = true;

			var par = row.Cells[0].AddParagraph("VENTAS");
			par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);
			par.Format.Alignment = ParagraphAlignment.Left;
			row.Format.Borders.DistanceFromTop = new Unit(0.1, UnitType.Centimeter);
			row.Format.Borders.DistanceFromBottom = new Unit(0.1, UnitType.Centimeter);



			table = section.AddTable();
			table.AddColumn(new Unit(2, UnitType.Centimeter));
			table.AddColumn(new Unit(6.5, UnitType.Centimeter));
			table.AddColumn(new Unit(3.9, UnitType.Centimeter));
			table.AddColumn(new Unit(3.9, UnitType.Centimeter));
			table.AddColumn(new Unit(3.0, UnitType.Centimeter));

			




			row = table.AddRow();
			row.Borders.Visible = true;
			row.Borders.Width = new Unit(.1, UnitType.Millimeter);
			row.Shading.Color = Color.Parse("white");

			row.Format.Font.Color = Color.Parse("black");
			row.Format.Font.Bold = true;

		    par = row.Cells[0].AddParagraph("ID");
			par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);
			par.Format.Alignment = ParagraphAlignment.Center;
			row.Format.Borders.DistanceFromTop = new Unit(0.2, UnitType.Centimeter);
			row.Format.Borders.DistanceFromBottom = new Unit(0.2, UnitType.Centimeter);
			par = row.Cells[1].AddParagraph("NRO FACTURA");
			par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);
			par.Format.Alignment = ParagraphAlignment.Center;
			par = row.Cells[2].AddParagraph("FECHA");
			par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);
			par.Format.Alignment = ParagraphAlignment.Center;
			par = row.Cells[3].AddParagraph("MONTO");
			par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);
			par = row.Cells[4].AddParagraph("ESTADO");
			par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);

			par.Format.Alignment = ParagraphAlignment.Center;
			//row = table.AddRow();
			
			for (var i = 0; i < model.Ventas.Detalle.Count; i++)
			{
				var item = model.Ventas.Detalle[i];
				row = table.AddRow();
				row.Cells[0].Borders.Visible = true;
				row.Cells[1].Borders.Visible = true;
				row.Cells[2].Borders.Visible = true;
				row.Cells[3].Borders.Visible = true;
				row.Cells[4].Borders.Visible = true;
				row.Cells[0].Borders.Width = new Unit(.1, UnitType.Millimeter);
				row.Cells[1].Borders.Width = new Unit(.1, UnitType.Millimeter);
				row.Cells[2].Borders.Width = new Unit(.1, UnitType.Millimeter);
				row.Cells[3].Borders.Width = new Unit(.1, UnitType.Millimeter);
				row.Cells[4].Borders.Width = new Unit(.1, UnitType.Millimeter);
				row.Format.Borders.DistanceFromTop = new Unit(0.2, UnitType.Centimeter);
				row.Format.Borders.DistanceFromBottom = new Unit(0.2, UnitType.Centimeter);
				row.Format.Font.Size = new Unit(8, UnitType.Point);

				par = row.Cells[0].AddParagraph(item.Id.ToString());
				par.Format.Alignment = ParagraphAlignment.Center;

				par = row.Cells[1].AddParagraph(item.NroFacturaString);
				par.Format.Alignment = ParagraphAlignment.Center;
				par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);
				par = row.Cells[2].AddParagraph(item.DateCreated.ToString("dd/MM/yyyy"));
				par.Format.Alignment = ParagraphAlignment.Center;
				par = row.Cells[3].AddParagraph(item.MontoTotal.ToString());
				par.Format.Alignment = ParagraphAlignment.Center;
				par = row.Cells[4].AddParagraph(item.EstadoDescription.ToString());
				par.Format.Alignment = ParagraphAlignment.Center;

			}

			row = table.AddRow();
			row = table.AddRow();

			
			//par = row.Cells[0].AddParagraph($"TOTAL");
			//row.Cells[0].MergeRight = 2;
			//par.Format.Alignment = ParagraphAlignment.Right;
			//par.Format.Font.Bold = true;
			//par.Format.Font.Size = new Unit(10, UnitType.Point);
			//par.Format.RightIndent = new Unit(4, UnitType.Millimeter);


			//par = row.Cells[3].AddParagraph($"{model.MontoTotal}");
			//par.Format.Font.Bold = true;
			//par.Format.Font.Size = new Unit(10, UnitType.Point);
			//par.Format.Alignment = ParagraphAlignment.Center;
			//par.Format.RightIndent = new Unit(3, UnitType.Millimeter);


		}

		private static void AddCompraItems(Section section, ReportesIndexViewModel model)
		{
			var table = section.AddTable();
			table.AddColumn(new Unit(19.3, UnitType.Centimeter));

			var row = table.AddRow();

			row.Shading.Color = Color.Parse("black");

			row.Format.Font.Color = Color.Parse("white");
			row.Format.Font.Bold = true;

			var par = row.Cells[0].AddParagraph("COMPRAS");
			par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);
			par.Format.Alignment = ParagraphAlignment.Left;
			row.Format.Borders.DistanceFromTop = new Unit(0.1, UnitType.Centimeter);
			row.Format.Borders.DistanceFromBottom = new Unit(0.1, UnitType.Centimeter);

			table = section.AddTable();
			table.AddColumn(new Unit(2, UnitType.Centimeter));
			table.AddColumn(new Unit(3.5, UnitType.Centimeter));
			table.AddColumn(new Unit(3.9, UnitType.Centimeter));
			table.AddColumn(new Unit(3.9, UnitType.Centimeter));
			table.AddColumn(new Unit(3.0, UnitType.Centimeter));
			table.AddColumn(new Unit(3.0, UnitType.Centimeter));
			row = table.AddRow();
			row.Borders.Visible = true;
			row.Borders.Width = new Unit(.1, UnitType.Millimeter);
			row.Shading.Color = Color.Parse("white");

			row.Format.Font.Color = Color.Parse("black");
			row.Format.Font.Bold = true;

			par = row.Cells[0].AddParagraph("ID");
			par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);
			par.Format.Alignment = ParagraphAlignment.Center;
			row.Format.Borders.DistanceFromTop = new Unit(0.2, UnitType.Centimeter);
			row.Format.Borders.DistanceFromBottom = new Unit(0.2, UnitType.Centimeter);
			par = row.Cells[1].AddParagraph("NRO FACTURA");
			par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);
			par.Format.Alignment = ParagraphAlignment.Center;
			par = row.Cells[2].AddParagraph("FECHA");
			par.Format.RightIndent = new Unit(3, UnitType.Millimeter);
			par.Format.Alignment = ParagraphAlignment.Center;
			par = row.Cells[3].AddParagraph("Proveedor");
			par.Format.RightIndent = new Unit(3, UnitType.Millimeter);
			par = row.Cells[4].AddParagraph("Monto Total");
			par.Format.RightIndent = new Unit(3, UnitType.Millimeter);
			par = row.Cells[5].AddParagraph("Estado");
			par.Format.RightIndent = new Unit(3, UnitType.Millimeter);
			par.Format.Alignment = ParagraphAlignment.Center;
			//row = table.AddRow();

			for (var i = 0; i < model.Compras.Detalle.Count; i++)
			{
				var item = model.Compras.Detalle[i];
				row = table.AddRow();
				row.Cells[0].Borders.Visible = true;
				row.Cells[1].Borders.Visible = true;
				row.Cells[2].Borders.Visible = true;
				row.Cells[3].Borders.Visible = true;
				row.Cells[4].Borders.Visible = true;
				row.Cells[5].Borders.Visible = true;
				row.Cells[0].Borders.Width = new Unit(.1, UnitType.Millimeter);
				row.Cells[1].Borders.Width = new Unit(.1, UnitType.Millimeter);
				row.Cells[2].Borders.Width = new Unit(.1, UnitType.Millimeter);
				row.Cells[3].Borders.Width = new Unit(.1, UnitType.Millimeter);
				row.Cells[4].Borders.Width = new Unit(.1, UnitType.Millimeter);
				row.Cells[5].Borders.Width = new Unit(.1, UnitType.Millimeter);
				row.Format.Borders.DistanceFromTop = new Unit(0.2, UnitType.Centimeter);
				row.Format.Borders.DistanceFromBottom = new Unit(0.2, UnitType.Centimeter);
				row.Format.Font.Size = new Unit(8, UnitType.Point);

				par = row.Cells[0].AddParagraph(item.Id.ToString());
				par.Format.Alignment = ParagraphAlignment.Center;

				par = row.Cells[1].AddParagraph(item.NroFactura);
				par.Format.Alignment = ParagraphAlignment.Center;
				par.Format.LeftIndent = new Unit(3, UnitType.Millimeter);
				par = row.Cells[2].AddParagraph(item.DateCompra.ToString("dd/MM/yyyy"));
				par.Format.Alignment = ParagraphAlignment.Center;
				par = row.Cells[3].AddParagraph(item.DisplayName.ToString());
				par.Format.Alignment = ParagraphAlignment.Center;
				par = row.Cells[4].AddParagraph(item.MontoTotal.ToString());
				par.Format.Alignment = ParagraphAlignment.Center;
				par = row.Cells[5].AddParagraph(item.EstadoDescripcion.ToString());
				par.Format.Alignment = ParagraphAlignment.Center;
			}

			row = table.AddRow();
			row = table.AddRow();


			//par = row.Cells[0].AddParagraph($"TOTAL");
			//row.Cells[0].MergeRight = 2;
			//par.Format.Alignment = ParagraphAlignment.Right;
			//par.Format.Font.Bold = true;
			//par.Format.Font.Size = new Unit(10, UnitType.Point);
			//par.Format.RightIndent = new Unit(4, UnitType.Millimeter);


			//par = row.Cells[3].AddParagraph($"{model.MontoTotal}");
			//par.Format.Font.Bold = true;
			//par.Format.Font.Size = new Unit(10, UnitType.Point);
			//par.Format.Alignment = ParagraphAlignment.Center;
			//par.Format.RightIndent = new Unit(3, UnitType.Millimeter);


		}
	}
}

using Core.DTOs.Pedidos;
using MigraDoc.Rendering;
using PdfServices.Builders;
using PdfServices.Interfaces;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PdfServices.Services
{
	public class PdfCreationServices : IPdfCreation
	{
		public PdfCreationServices()
		{
		
		}
		public byte[] GetPedidosPdf(PedidosPdfModel model)
		{
			var pdfDocument = PedidosBuilder.Build(model);
			var renderer = new PdfDocumentRenderer() { Document = pdfDocument };
			renderer.RenderDocument();
			byte[] fileContents;
			using (var stream = new MemoryStream())
			{
				renderer.PdfDocument.Save(stream, false);
				fileContents = stream.ToArray();
			}
			return fileContents;
		}

	}
}

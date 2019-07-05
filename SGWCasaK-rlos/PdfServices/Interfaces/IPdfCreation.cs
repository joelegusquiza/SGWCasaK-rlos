using Core.DTOs.Pedidos;
using Core.DTOs.Reportes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PdfServices.Interfaces
{
	public interface IPdfCreation
	{
		byte[] GetPedidosPdf(PedidosPdfModel model);
		byte[] GetReportePdf(ReportesIndexViewModel model);
	}
}

using ApplicationContext;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Dashboard;
using Core.DTOs.Reportes;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DAL.Services
{
	public class ReportesService : IReportes
	{
		private readonly DataContext _context;

		public ReportesService(DataContext context)
		{
			_context = context;
		}
		
		public ReportesIndexViewModel GetReporteIndex (ReporteParameterViewModel parameters, int sucursalId)
		{
			var data = new ReportesIndexViewModel();
			data.Parameters = parameters;			
			data.Ventas = GetReporteVentas(parameters.FechaInicio.ToUniversalTime(), parameters.FechaFin.ToUniversalTime(), sucursalId);
			data.Compras = GetReporteCompras(parameters.FechaInicio.ToUniversalTime(), parameters.FechaFin.ToUniversalTime(), sucursalId);
			data.Pedidos = GetReportePedidos(parameters.FechaInicio.ToUniversalTime(), parameters.FechaFin.ToUniversalTime(), sucursalId);
			return data;
		}

		public ReporteImpuestosIndexViewModel GetReporteImpuestos(ReporteParameterViewModel parameters, int sucursalId)
		{
			var data = new ReporteImpuestosIndexViewModel();
			data.Parameters = parameters;
			
			var ventas = _context.Set<Venta>().Where(x => x.DateCreated.Date >= parameters.FechaInicio.ToUniversalTime().Date && x.DateCreated.Date <= parameters.FechaFin.ToUniversalTime().Date && x.SucursalId == sucursalId && x.Estado == Constants.EstadoVenta.Pagado);
			var compras = _context.Set<Compra>().Where(x => x.DateCreated.Date >= parameters.FechaInicio.ToUniversalTime().Date && x.DateCreated.Date <= parameters.FechaFin.ToUniversalTime().Date && x.SucursalId == sucursalId && x.Estado == Constants.EstadoCompra.Pagado);
			data.Reporte.MontoCincoDebito = ventas.Sum(x => x.IvaCinco);
			data.Reporte.MontoDiezDebito = ventas.Sum(x => x.IvaDiez);
			data.Reporte.MontoDebito = data.Reporte.MontoCincoDebito + data.Reporte.MontoDiezDebito;
			data.Reporte.MontoCincoCredito = compras.Sum(x => x.IvaCinco);
			data.Reporte.MontoDiezCredito = compras.Sum(x => x.IvaDiez);
			data.Reporte.MontoCredito = data.Reporte.MontoCincoCredito + data.Reporte.MontoDiezCredito;
			return data;
		}

		public ReportesProductoIndexViewModel GetReporteProductos(ProductoParametersViewModel parameters, int sucursalId)
		{
			var data = new ReportesProductoIndexViewModel();
			data.Parameters = parameters;
			var detalleVentas = _context.Set<DetalleVenta>().Where(x => x.Active && x.Venta.Estado == Constants.EstadoVenta.Pagado && x.Venta.SucursalId == sucursalId && x.DateCreated.Date >= parameters.FechaInicio.ToUniversalTime().Date && x.DateCreated.Date <= parameters.FechaFin.ToUniversalTime().Date);
			var detalleCompras = _context.Set<DetalleCompra>().Where(x => x.Active && x.Compra.Estado == Constants.EstadoCompra.Confirmado && x.Compra.SucursalId == sucursalId && x.DateCreated.Date >= parameters.FechaInicio.ToUniversalTime().Date && x.DateCreated.Date <= parameters.FechaFin.ToUniversalTime().Date);
			var productosIds = detalleVentas.Select(x => x.ProductoId).Concat(detalleCompras.Select(x => x.ProductoId)).Distinct().ToList();
			var productos = _context.Set<Producto>().Include(x => x.CategoriaProducto).Where(x => x.Active && productosIds.Contains(x.Id));
			if (parameters.CategoriaId != 0)
				productos = productos.Where(x => x.CategoriaProductoId == parameters.CategoriaId);
			foreach (var producto in productos)
			{
				var ventas = detalleVentas.Where(x => x.ProductoId == producto.Id);
				var compras = detalleCompras.Where(x => x.ProductoId == producto.Id);
				var item = new ReporteProductoViewModel()
				{
					Id = producto.Id,
					Categoria = producto.CategoriaProducto.Nombre,
					CategoriaId = producto.CategoriaProducto.Id,
					Nombre = producto.Nombre,
					PrecioVenta = producto.PrecioVenta,
					CantComprada = compras.Count(),
					CantVendida = ventas.Count(),
					MontoTotalComprado = compras.Sum(x => x.PrecioCompra),
					MontoTotalVendido = ventas.Sum(x => x.PrecioVenta),
				};
				item.Resultado = item.MontoTotalVendido - item.MontoTotalComprado;
				data.Reporte.Add(item);
			}
			return data;
		}

		private ReportesVentasIndexViewModel GetReporteVentas(DateTime inicio, DateTime fin, int sucursalId)
		{
			var ventaReporte = new ReportesVentasIndexViewModel();
			var ventas = _context.Set<Venta>().Where(x => x.DateCreated.Date >= inicio.ToUniversalTime().Date && x.DateCreated.Date <= fin.ToUniversalTime().Date && x.SucursalId == sucursalId);
			ventaReporte.CantVentas = ventas.Count();
			ventaReporte.MontoTotal = ventas.Sum(x => x.MontoTotal);
			ventaReporte.CantVentasPendientes = ventas.Where(x => x.Estado == Constants.EstadoVenta.PendientedePago).Count();
			ventaReporte.MontoTotalPendientes = ventas.Where(x => x.Estado == Constants.EstadoVenta.PendientedePago).Sum(x => x.MontoTotal);
			ventaReporte.CantVentasCobradas = ventas.Where(x => x.Estado == Constants.EstadoVenta.Pagado).Count();
			ventaReporte.MontoTotalCobradas = ventas.Where(x => x.Estado == Constants.EstadoVenta.Pagado).Sum(x => x.MontoTotal);

			ventaReporte.Detalle = Mapper.Map<List<ReportesVentasDetalleViewModel>>(ventas);
			return ventaReporte;
		}

		private ReportesComprasIndexViewModel GetReporteCompras(DateTime inicio, DateTime fin, int sucursalId)
		{
			var compraReporte = new ReportesComprasIndexViewModel();
			var compras = _context.Set<Compra>().Where(x => x.DateCreated.Date >= inicio.ToUniversalTime().Date && x.DateCreated.Date <= fin.ToUniversalTime().Date && x.SucursalId == sucursalId);
			compraReporte.CantCompras = compras.Count();
			compraReporte.MontoTotal = compras.Sum(x => x.MontoTotal);
			compraReporte.CantComprasPendientes = compras.Where(x => x.Estado == Constants.EstadoCompra.PendientedePago).Count();
			compraReporte.MontoTotalPendientes = compras.Where(x => x.Estado == Constants.EstadoCompra.PendientedePago).Sum(x => x.MontoTotal);
			compraReporte.CantComprasPagado = compras.Where(x => x.Estado == Constants.EstadoCompra.Pagado).Count();
			compraReporte.MontoTotalPagado = compras.Where(x => x.Estado == Constants.EstadoCompra.Pagado).Sum(x => x.MontoTotal);

			compraReporte.Detalle = Mapper.Map<List<ReportesComprasDetalleViewModel>>(compras);
			return compraReporte;
		}

		private ReportesPedidosIndexViewModel GetReportePedidos(DateTime inicio, DateTime fin, int sucursalId)
		{
			var pedidoReporte = new ReportesPedidosIndexViewModel();
			var pedidos = _context.Set<Pedido>().Where(x => x.DateCreated.Date >= inicio.ToUniversalTime().Date && x.DateCreated.Date <= fin.ToUniversalTime().Date && x.SucursalId == sucursalId);
			pedidoReporte.Detalle = Mapper.Map<List<ReportesPedidosDetalleViewModel>>(pedidos);
			return pedidoReporte;
		}

		public DashboardIndexViewModel GetDashboardData(int sucursalId)
		{
			var data = new DashboardIndexViewModel();
			var ventas = _context.Set<Venta>().Where(x => x.Active && x.Estado != Constants.EstadoVenta.Anulado && x.DateCreated.Date == DateTime.UtcNow.Date && x.SucursalId == sucursalId);
			var compras = _context.Set<Compra>().Where(x => x.Active && x.Estado != Constants.EstadoCompra.Anulado && x.DateCreated.Date == DateTime.UtcNow.Date && x.SucursalId == sucursalId);
			var pedidos = _context.Set<Pedido>().Where(x => x.Active && x.Estado != Constants.EstadoPedido.Anulado && x.Estado != Constants.EstadoPedido.EntregadoPorDelivery && x.DateCreated.Date == DateTime.UtcNow.Date && x.SucursalId == sucursalId);

			data.Ventas.CantVentas = ventas.Count();
			data.Compras.CantCompras = compras.Count();					
			data.Pedidos.CantPedidos = pedidos.Count();

			data.Ventas.MontoTotal = ventas.Sum(x => x.MontoTotal);
			data.Compras.MontoTotal = compras.Sum(x => x.MontoTotal);
			data.Pedidos.MontoTotal = pedidos.Sum(x => x.MontoTotal);

			data.Pedidos.PedidosDelivery = Mapper.Map<List<DashboardPedidoViewModel>>(pedidos.Where(x => x.Delivery).OrderBy(x => x.FechaEntrega));
			return data;
		}
	}
}

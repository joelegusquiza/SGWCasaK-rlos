function showError(message) {
    swal("Error!", message, "error");
}

function showSuccess(message) {
    swal("Exito!", message, "success");
}

function showSuccessAndGoToUrl(message, url) {
    swal("Exito!", message, "success")
        .then(function (result) {
            window.location.replace(url);
        });
}

function printFacturaCaida(data) {
	var windowUrl = 'about:blank';
	var uniqueName = new Date();
	var windowName = 'CloseTillPrint';
	var PrintWindow = window.open(windowUrl/*, windowName, 'left=1000,top=1000,width=1700,height=500'*/);


	PrintWindow.document.open('text/plain');

	var cmds = '<style>  @media print { @page { margin : 0;} body { margin: 1.6cm;}} td { text-align: center; }</style>'
	cmds += '<div id= "original" style= " float:left;">'
	cmds += '        <div  style=" border-color: black; border-width: 1px; width: 165px; height: 20px; ">'
	cmds += ''
	cmds += '        </div>'
	cmds += '        <div  style="border-width: 1px;width: 335px; height: 20px;">'
	cmds += '                <div style="text-align: right; width: 98%; font-size: 12px; margin-top: 10px; ">'
	cmds += data.nroFactura
	cmds += '                </div>'
	cmds += '        </div>'
	cmds += '        <div style="width: 335px;height: 80px;">'
	cmds += '            <div style="width: 100%;">'
	cmds += '                <div align="right" style=" width: 48%; float: left; font-size: 7px; margin-top: 10px; ">'
	cmds += data.fecha
	cmds += '                </div>'
	cmds += '                <div align="right" style=" width: 48%;float: right; padding-right: 7px; font-size: 7px;margin-top: 10px;">'

	cmds += '                   <div class="" style=" float: left; margin-left: 40px; ">'
	cmds += '                           <div style="margin-left: 55px;border-color: black; display: inline-block;">&nbsp; </div>('
	if (data.tipoFactura == "Credito") {
		cmds += 'x'
	}

	cmds += ') &nbsp &nbsp '
	cmds += '                   </div>'

	cmds += '                   <div class="" style="float: rigth; ">'
	cmds += '                           <span style="color: white">&nbsp; &nbsp;</span>('
	if (data.tipoFactura == "Contado" || data.tipoFactura == null) {
		cmds += 'x'
	}
	cmds += ')'
	cmds += '                   </div>'

	cmds += '             </div>'
	cmds += '            </div>'
	cmds += '            <div style="width: 100%;">'
	cmds += '                <div align="right" style="width: 48%;float: left; font-size: 7px; margin-top: 10px;">'
	cmds += data.displayName
	cmds += '                </div>'
	cmds += '                <div align="right" style="width: 48%; float: right; padding-right: 7px; font-size: 7px; margin-top: 10px;">'
	cmds += data.rucEntitie
	cmds += '                </div>'
	cmds += '            </div>'
	cmds += '            <div style="width: 100%;">'
	cmds += '                <div align="right" style="width: 48%;float: left;font-size: 7px;margin-top: 10px;" >'
	cmds += '                    '
	cmds += '                </div>'
	cmds += '                <div align="right" style="width: 48%;float: right; padding-right: 7px; font-size: 7px; margin-top: 10px;" >'
	cmds += data.telefonoEntitie
	cmds += '                </div>'
	cmds += '            </div>'
	cmds += '            <div style="width: 100%">'
	cmds += '                <div  align="right" style=" width: 48%;float: left;font-size: 7px;margin-top: 10px;" style="width: 70%">'
	cmds += data.direccionEntitie
	cmds += '                </div>'

	cmds += '            </div>'
	cmds += '        </div>'
	cmds += '        <div style="width: 335px; height: 390px;">'
	cmds += '            <table  style="border: none; border-collapse: collapse;" border="1">'
	cmds += '                <thead>'
	cmds += '                    <tr>'
	cmds += '                        <th  rowspan="2" style="width: 80px; font-size: 8px;">&nbsp;</th>'
	cmds += '                        <th  rowspan="2" style="width: 650px; font-size: 8px;">&nbsp;</th>'
	cmds += '                        <th  rowspan="2" style="width: 300px; font-size: 8px;">&nbsp;</th>'
	cmds += '                        <th  style="width: 10px; font-size: 8px;" colspan="3">&nbsp;</th>'


	cmds += '                    </tr>'
	cmds += '                    <tr>'
	cmds += '                        <th  style="width: 250px; font-size: 8px;">&nbsp;</th>'
	cmds += '                        <th  style="width: 250px; font-size: 8px;">&nbsp;</th>'
	cmds += '                        <th style="width: 200px; font-size: 8px;">&nbsp;</th>'
	cmds += '                    </tr>'
	cmds += '                </thead>'

	cmds += '                <tbody>'
	for (var i = 0; i < 17; i++) {
		if (i < data.detalles.length) {
			cmds += '<tr>'
			cmds += '<td style="text-align: center; font-size: 60%; height: 20px;" id=""><div>' + data.detalles[i].cantidad + '</div></td>';
			cmds += '<td style="text-align: center; font-size: 50%; height: 20px;" id=""><div>' + data.detalles[i].descripcion + '</div></td>';
			cmds += '<td style="text-align: right; font-size: 60%; height: 20px;" id=""><div>' + data.detalles[i].precioUnitario + '</div></td>';
			if (data.detalles[i].precioTotalExcenta != 0) {
				cmds += '<td style="text-align: right; font-size: 60%; height: 20px;" id=""><div>' + data.detalles[i].precioTotalExcenta + '</div></td>';
			} else {
				cmds += '<td style="text-align: center; font-size: 60%; height: 20px;" id=""><div></div></td>';
			}
			if (data.detalles[i].precioTotalCinco != 0) {
				cmds += '<td style="text-align: right; font-size: 60%; height: 20px;" id=""><div>' + data.detalles[i].precioTotalCinco + '</div></td>';
			} else {
				cmds += '<td style="text-align: center; font-size: 60%; height: 20px;" id=""><div></div></td>';
			}
			if (data.detalles[i].precioTotalDiez != 0) {
				cmds += '<td style="text-align: right; font-size: 60%; height: 20px;" id=""><div>' + data.detalles[i].precioTotalDiez + '</div></td>';
			} else {
				cmds += '<td style="text-align: center; font-size: 60%; height: 20px;" id=""><div></div></td>';
			}


			cmds += '</tr>'
		} else {
			cmds += '<tr>'
			cmds += '<td style="font-size: 60%; height: 20px;" id=""><div></div></td>';
			cmds += '<td style=" font-size: 50%; height: 20px;"id=""><div></div></td>';
			cmds += '<td style=" font-size: 60%; height: 20px;" id=""><div></div></td>';
			cmds += '<td style=" font-size: 60%; height: 20px;" id=""><div></div></td>'
			cmds += '<td style=" font-size: 60%; height: 20px;" id=""><div></div></td>';
			cmds += '<td style=" font-size: 60%; height: 20px;" id=""><div></div></td>';
			cmds += '</tr>'
		}

	}


	cmds += '            </tbody>'
	cmds += '                <tfooter>'
	cmds += '                    <tr>'
	cmds += '                        <td style="font-size: 50%; height: 20px; text-align: left; padding-right: 7px;" colspan="3"></td>'
	cmds += '                        <td style="font-size: 8px; height: 20px; text-align: right; padding-right: 2px;" rowspan="1">' + data.subTotalExcenta + '</td>'
	cmds += '                        <td style="font-size: 8px; height: 20px; text-align: right; padding-right: 2px;" rowspan="1">' + data.subTotalCinco + '</td>'
	cmds += '                        <td style="font-size: 8px; height: 20px; text-align: right; padding-right: 2px;" rowspan="1">' + data.subTotalDiez + '</td>'


	cmds += '                   </tr>'
	cmds += '                    <tr>'
	cmds += '                         <td  style="font-size: 8px; height: 20px; text-align: right; padding-right: 7px;" colspan="5">' + data.montoTotalString + '</td>'
	cmds += '                       <td  style="font-size: 8px; height: 20px; text-align: right;" rowspan="1">' + data.montoTotal + '</td>'
	cmds += '                    </tr>'
	cmds += '                    <tr>'
	cmds += '                           <td style="font-size: 8px; height: 20px; text-align: right; padding-right: 7px;" colspan="2">' + data.ivaCinco + '</td>'
	cmds += '                        <td style="font-size: 8px; height: 20px; text-align: right; padding-right: 7px;" colspan="2">' + data.ivaDiez + '</td>'
	cmds += '                        <td style="font-size: 8px; height: 20px; text-align: right; padding-right: 7px;" colspan="2">' + data.ivaTotal + '</td>'
	cmds += '                    </tr>'

	cmds += '                </tfooter>'
	cmds += '            </table>'

	cmds += '        </div>'
	cmds += '        <div>'

	cmds += '       </div>'

	cmds += '</div >'


	cmds += '<div id= "original" style= " float:right;">'
	cmds += '        <div  style=" border-color: black; border-width: 1px; width: 165px; height: 20px; ">'
	cmds += ''
	cmds += '        </div>'
	cmds += '        <div  style="border-width: 1px;width: 335px; height: 20px;">'
	cmds += '                <div style="text-align: right; width: 98%; font-size: 12px; margin-top: 10px; ">'
	cmds += data.nroFactura
	cmds += '                </div>'
	cmds += '        </div>'
	cmds += '        <div style="width: 335px;height: 80px;">'
	cmds += '            <div style="width: 100%;">'
	cmds += '                <div align="right" style=" width: 48%; float: left; font-size: 7px; margin-top: 10px; ">'
	cmds += data.fecha
	cmds += '                </div>'
	cmds += '                <div align="right" style=" width: 48%;float: right; padding-right: 7px; font-size: 7px;margin-top: 10px;">'

	cmds += '                   <div class="" style=" float: left; margin-left: 40px; ">'
	cmds += '                           <div style="margin-left: 55px;border-color: black; display: inline-block;">&nbsp; </div>('
	if (data.tipoFactura == "Credito") {
		cmds += 'x'
	}

	cmds += ') &nbsp &nbsp '
	cmds += '                   </div>'

	cmds += '                   <div class="" style="float: rigth; ">'
	cmds += '                           <span style="color: white">&nbsp; &nbsp;</span>('
	if (data.tipoFactura == "Contado" || data.tipoFactura == null) {
		cmds += 'x'
	}
	cmds += ')'
	cmds += '                   </div>'

	cmds += '             </div>'
	cmds += '            </div>'
	cmds += '            <div style="width: 100%;">'
	cmds += '                <div align="right" style="width: 48%;float: left; font-size: 7px; margin-top: 10px;">'
	cmds += data.displayName
	cmds += '                </div>'
	cmds += '                <div align="right" style="width: 48%; float: right; padding-right: 7px; font-size: 7px; margin-top: 10px;">'
	cmds += data.rucEntitie
	cmds += '                </div>'
	cmds += '            </div>'
	cmds += '            <div style="width: 100%;">'
	cmds += '                <div align="right" style="width: 48%;float: left;font-size: 7px;margin-top: 10px;" >'
	cmds += '                   '
	cmds += '                </div>'
	cmds += '                <div align="right" style="width: 48%;float: right; padding-right: 7px; font-size: 7px; margin-top: 10px;" >'
	cmds += data.telefonoEntitie
	cmds += '                </div>'
	cmds += '            </div>'
	cmds += '            <div style="width: 100%">'
	cmds += '                <div  align="right" style=" width: 48%;float: left;font-size: 7px;margin-top: 10px;" style="width: 70%">'
	cmds += data.direccionEntitie
	cmds += '                </div>'

	cmds += '            </div>'
	cmds += '        </div>'
	cmds += '        <div style="width: 335px; height: 390px;">'
	cmds += '            <table  style="border: none; border-collapse: collapse;" border="1">'
	cmds += '                <thead>'
	cmds += '                    <tr>'
	cmds += '                        <th  rowspan="2" style="width: 80px; font-size: 8px;">&nbsp;</th>'
	cmds += '                        <th  rowspan="2" style="width: 650px; font-size: 8px;">&nbsp;</th>'
	cmds += '                        <th  rowspan="2" style="width: 300px; font-size: 8px;">&nbsp;</th>'
	cmds += '                        <th  style="width: 10px; font-size: 8px;" colspan="3">&nbsp;</th>'


	cmds += '                    </tr>'
	cmds += '                    <tr>'
	cmds += '                        <th  style="width: 250px; font-size: 8px;">&nbsp;</th>'
	cmds += '                        <th  style="width: 250px; font-size: 8px;">&nbsp;</th>'
	cmds += '                        <th style="width: 200px; font-size: 8px;">&nbsp;</th>'
	cmds += '                    </tr>'
	cmds += '                </thead>'

	cmds += '                <tbody>'
	for (var i = 0; i < 17; i++) {
		if (i < data.detalles.length) {
			cmds += '<tr>'
			cmds += '<td style=" font-size: 60%; height: 20px;" id=""><div>' + data.detalles[i].cantidad + '</div></td>';
			cmds += '<td style=" font-size: 50%; height: 20px;" id=""><div>' + data.detalles[i].descripcion + '</div></td>';
			cmds += '<td style="text-align: right; font-size: 60%; height: 20px;" id=""><div>' + data.detalles[i].precioUnitario + '</div></td>';
			if (data.detalles[i].precioTotalExcenta != 0) {
				cmds += '<td style="text-align: right; font-size: 60%; height: 20px;" id=""><div>' + data.detalles[i].precioTotalExcenta + '</div></td>';
			} else {
				cmds += '<td style="text-align: center; font-size: 60%; height: 20px;" id=""><div></div></td>';
			}
			if (data.detalles[i].precioTotalCinco != 0) {
				cmds += '<td style="text-align: right; font-size: 60%; height: 20px;" id=""><div>' + data.detalles[i].precioTotalCinco + '</div></td>';
			} else {
				cmds += '<td style="text-align: center; font-size: 60%; height: 20px;" id=""><div></div></td>';
			}
			if (data.detalles[i].precioTotalDiez != 0) {
				cmds += '<td style="text-align: right; font-size: 60%; height: 20px;" id=""><div>' + data.detalles[i].precioTotalDiez + '</div></td>';
			} else {
				cmds += '<td style="text-align: center; font-size: 60%; height: 20px;" id=""><div></div></td>';
			}
			cmds += '</tr>'
		} else {
			cmds += '<tr>'
			cmds += '<td style="font-size: 60%; height: 20px;" id=""><div></div></td>';
			cmds += '<td style=" font-size: 50%; height: 20px;"id=""><div></div></td>';
			cmds += '<td style=" font-size: 60%; height: 20px;" id=""><div></div></td>';
			cmds += '<td style=" font-size: 60%; height: 20px;" id=""><div></div></td>'
			cmds += '<td style=" font-size: 60%; height: 20px;" id=""><div></div></td>';
			cmds += '<td style=" font-size: 60%; height: 20px;" id=""><div></div></td>';
			cmds += '</tr>'
		}
	}


	cmds += '            </tbody>'
	cmds += '                <tfooter>'
	cmds += '                    <tr>'
	cmds += '                        <td style="font-size: 50%; height: 20px; text-align: left; padding-right: 7px;" colspan="3"></td>'
	cmds += '                        <td style="font-size: 8px; height: 20px; text-align: right; padding-right: 2px;" rowspan="1">' + data.subTotalExcenta + '</td>'
	cmds += '                        <td style="font-size: 8px; height: 20px; text-align: right; padding-right: 2px;" rowspan="1">' + data.subTotalCinco + '</td>'
	cmds += '                        <td style="font-size: 8px; height: 20px; text-align: right; padding-right: 2px;" rowspan="1">' + data.subTotalDiez + '</td>'


	cmds += '                   </tr>'
	cmds += '                    <tr>'
	cmds += '                         <td  style="font-size: 8px; height: 20px; text-align: right; padding-right: 7px;" colspan="5">' + data.montoTotalString + '</td>'
	cmds += '                       <td  style="font-size: 8px; height: 20px; text-align: right;" rowspan="1">' + data.montoTotal + '</td>'
	cmds += '                    </tr>'
	cmds += '                    <tr>'
	cmds += '                           <td style="font-size: 8px; height: 20px; text-align: right; padding-right: 7px;" colspan="2">' + data.ivaCinco + '</td>'
	cmds += '                        <td style="font-size: 8px; height: 20px; text-align: right; padding-right: 7px;" colspan="2">' + data.ivaDiez + '</td>'
	cmds += '                        <td style="font-size: 8px; height: 20px; text-align: right; padding-right: 7px;" colspan="2">' + data.ivaTotal + '</td>'
	cmds += '                    </tr>'

	cmds += '                </tfooter>'
	cmds += '            </table>'

	cmds += '        </div>'
	cmds += '        <div>'

	cmds += '       </div>'

	cmds += '</div >'




	PrintWindow.document.write(cmds);
	PrintWindow.document.close();
	PrintWindow.focus();
	PrintWindow.print();
	PrintWindow.close();


}
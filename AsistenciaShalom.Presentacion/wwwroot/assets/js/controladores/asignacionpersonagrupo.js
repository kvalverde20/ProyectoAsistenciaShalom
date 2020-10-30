
function pintarPopupAsignaciones(divTabla, url, cabeceras,
	campos, propiedadId, propiedadMostrar = "") {

	var contenido = "";
	$.get(url, function (data) {

		contenido += "<div class='table-responsive'>";
		//contenido += "<table id='tablaPopup' class='display table table-bordered table-striped table-hover blueTable'>";
		contenido += "<table id='tablaPopup' style='width: 100%;' class='display table  table-bordered table-hover blueTable'>";
		contenido += "<thead>";
		contenido += "<tr>";
		for (var i = 0; i < cabeceras.length; i++) {
			if (i == 0) {
				contenido += "<td style='display: none'>" + cabeceras[i] + "</td>"
			} else {
				contenido += "<th>" + cabeceras[i] + "</th>"
			}
		}
		//contenido += "<th></th>";
		contenido += "</tr>";
		contenido += "</thead>";
		contenido += "<tbody>";
		var objetoActual;
		for (var i = 0; i < data.length; i++) {
			contenido += "<tr>";
			for (var j = 0; j < campos.length; j++) {
				nombreCampo = campos[j];
				objetoActual = data[i];

					if (j == 0) {
						contenido += "<td style='display: none'>" + objetoActual[nombreCampo] + "</td>"
					} else {
						contenido += "<td>" + objetoActual[nombreCampo] + "</td>"
					}
				//contenido += "<td>" + objetoActual[nombreCampo] + "</td>"
			}

			//contenido += `
			//		<td>

			//			<i style='padding: 3px;' class='fa fa-check btn btn-success' aria-hidden="true"
			//			   onclick="AsignarNombre(${objetoActual[propiedadId]},
			//                           '${objetoActual[propiedadMostrar]}')">

			//			</i>
			//		</td>`;

			contenido += "</tr>";
		}
		contenido += "</tbody>";
		contenido += "</table>";
		contenido += "</div>";
		document.getElementById(divTabla).innerHTML = contenido;
		$('#tablaPopup').DataTable({
			"order": [[1, "desc"]],
			"language": {
				"emptyTable": "No hay registros",
				"info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
				"infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
				"infoFiltered": "(Filtrado de _MAX_ total entradas)",
				"infoPostFix": "",
				"thousands": ",",
				"lengthMenu": "Mostrar _MENU_ Entradas",
				"loadingRecords": "Cargando...",
				"processing": "Procesando...",
				"zeroRecords": "Sin resultados encontrados",
				"paginate": {
					"first": "Primero",
					"last": "Ultimo",
					"next": "Siguiente",
					"previous": "Anterior"
				}
			},
			"pageLength": 7,
			"searching": false,
			"info": false,
			"lengthChange": false
		});
	});


}
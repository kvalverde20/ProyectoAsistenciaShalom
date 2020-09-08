
function pintarTableAsigGrupoFase(url, campos, propiedadId, nombreController, id = "tbDatos", idTabla = "table", propiedadMostrar = "") {
	var contenido = "";

	if (id == null || id == undefined || id == "") {
		var tbody = document.getElementById("tbDatos");
	} else {
		var tbody = document.getElementById(id);
	}

	var nombreCampo;
	var objetoActual;
	$.get(url, function (data) {

		for (var i = 0; i < data.length; i++) {
			if (data[i].estadoAsignacionFase == 'A') {
				contenido += "<tr>";
			}
			else {
				contenido += "<tr class='text-danger'>";
			}

			// contenido += "<tr class='table-warning'>";
			for (var j = 0; j < campos.length; j++) {
				nombreCampo = campos[j];
				objetoActual = data[i];
				//contenido += "<td>" + objetoActual[nombreCampo] + "</td>"
				if (j == 0) {
					contenido += "<td style='display: none'>" + objetoActual[nombreCampo] + "</td>"
				} else {
					contenido += "<td>" + objetoActual[nombreCampo] + "</td>"
				}

			}

			contenido += `
							<td style="text-align:center">
								<div class="form-button-action"> `

			if (data[i].estadoAsignacionFase == 'A') {

				//contenido += `<a  
				//				href="${nombreController}/Asignar/${objetoActual[propiedadId]}" data-toggle="tooltip" data-placement="top" data-original-title="Asignado"
				//				class="btn btn-link btn-success disabled">
				//				<i class="fas fa-user-check"></i>	
				//				</a>`
				contenido += `<span class="badge badge-info">Asignado</span>`
			} else {
				contenido += `<a type="button" 
								href="${nombreController}/Asignar/${objetoActual[propiedadId]}" data-toggle="tooltip"
								class="btn btn-lg btn-link btn-danger" data-original-title="Asignar" >
								<i class="fas fa-tasks"></i>	
								</a>`
			}

			contenido += ` </div> 
							</td>`;


			contenido += "</tr>";
		}

		tbody.innerHTML = contenido;
		if (idTabla == null || idTabla == undefined || idTabla == "") {
			$('#table').DataTable(
				{
					"order": [[0, "desc"]],
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
						"search": "Buscar:",
						"zeroRecords": "Sin resultados encontrados",
						"paginate": {
							"first": "Primero",
							"last": "Ultimo",
							"next": "Siguiente",
							"previous": "Anterior"
						}
					}
				}
			);
		} else {
			$('#' + idTabla).DataTable(
				{
					"order": [[0, "desc"]],
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
						"search": "Buscar:",
						"zeroRecords": "Sin resultados encontrados",
						"paginate": {
							"first": "Primero",
							"last": "Ultimo",
							"next": "Siguiente",
							"previous": "Anterior"
						}
					}
				}
			);
		}
	})

}


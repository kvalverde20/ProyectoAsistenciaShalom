
function pintarTablaUsuarios(url, campos, propiedadId, nombreController, id = "tbDatos", idTabla = "table", propiedadMostrar = "") {
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

			//if (data[i].estadoAsignacionFase == 'A') {
			//	contenido += "<tr>";
			//}
			//else {
			//	contenido += "<tr class='text-danger'>";
			//}
			contenido += "<tr>";

			for (var j = 0; j < campos.length; j++) {
				nombreCampo = campos[j];
				objetoActual = data[i];
				//contenido += "<td>" + objetoActual[nombreCampo] + "</td>"
				if (j == 0) {
					contenido += "<td style='display: none'>" + objetoActual[nombreCampo] + "</td>"
				} else if (nombreCampo == "estado") {
					if (objetoActual[nombreCampo]) {
						contenido += `<td><span class="badge badge-success">Activo</span></td>`
					} else {
						contenido += `<td><span class="badge badge-danger">Inactivo</span></td>`
                    }

				} else {
					contenido += "<td>" + objetoActual[nombreCampo] + "</td>"
                }
					                

			}

			contenido += `
							<td style="text-align:center">
								<div class="form-button-action"> `


			contenido += `<a  
							   href="${nombreController}/Editar/${objetoActual[propiedadId]}" data-toggle="tooltip"
								class="btn btn-link btn-lg btn-info" data-original-title="Editar" 
							  >
									<i class="fas fa-user-edit" aria-hidden="true"></i>	
							</a>`

			contenido += `  <a  data-toggle="tooltip" class="btn btn-link btn-lg btn-danger" data-original-title="Eliminar"
												onclick="Eliminar(${objetoActual[propiedadId]})">
												<i class="fa fa-times" aria-hidden="true"></i>
							</a>`	
			

			contenido += ` </div> 
							</td>`;


			contenido += "</tr>";
		}

		//tbody.innerHTML = contenido;
		if (idTabla == null || idTabla == undefined || idTabla == "") {

			$('#table').DataTable().clear();
			$('#table').DataTable().destroy();
			tbody.innerHTML = contenido;
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
					},
					drawCallback: function (settings) {
						$('[data-toggle="tooltip"]').tooltip();
					}

				}
			);

			$('#table').on('draw', function () {
				//console.log('draw event');
				$('[data-toggle="tooltip"]').tooltip();
			});

		} else {

			$('#' + idTabla).DataTable().clear();
			$('#' + idTabla).DataTable().destroy();
			tbody.innerHTML = contenido;
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
					,
					drawCallback: function (settings) {						
						$('[data-toggle="tooltip"]').tooltip();
					}
				}
			);
			$('#' + idTabla).on('draw', function () {
				$('[data-toggle="tooltip"]').tooltip();
			});
		}
	})

}


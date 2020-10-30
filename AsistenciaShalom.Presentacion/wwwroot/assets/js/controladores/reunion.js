

function pintarTableReuniones(url, campos, propiedadId, nombreController, id = "tbDatos", idTabla = "table", propiedadMostrar = "") {
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
			//if (data[i].estadoRegistroAsistencia == 'A') {
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
				} else {
					contenido += "<td>" + objetoActual[nombreCampo] + "</td>"
				}

			}

			//contenido += `
			//				<td>
			//					<div class="form-button-action"> `

			if (data[i].estadoRegistroAsistencia == true) {

				contenido += `
							<td  style="justify-content: center; display: table-cell; text-align: center;">
								<div class="form-button-action">`
				contenido += `<a  
								href="${nombreController}/Ver/${objetoActual[propiedadId]}" data-toggle="tooltip"  
								class="btn btn-link btn-default btn-lg" data-original-title="Ver"> 
								<i class="fas fa-eye"></i>	
								</a>`
				contenido += `<a  
								href="${nombreController}/EditarReunionAsistencia/${objetoActual[propiedadId]}" data-toggle="tooltip"  
								class="btn btn-link btn-secondary btn-lg"  data-original-title="Editar">
								<i class="fas fa-edit"></i>	
								</a>`
			} else {
				contenido += `
							<td  style="justify-content: center; display: table-cell; text-align: center;">
								<div class="form-button-action">`
				contenido += `<a 
								href="${nombreController}/Asistencia/${objetoActual[propiedadId]}" data-toggle="tooltip"
								class="btn btn-link btn-success btn-lg" data-original-title="Registrar Asistencia" >
								<i class="fas fa-calendar-plus"></i>	
								</a>`
			}
			//contenido += `<a  
			//							   href="${nombreController}/Editar/${objetoActual[propiedadId]}" data-toggle="tooltip"
			//								class="btn btn-link btn-primary" data-original-title="Editar" >
			//								<i class="fa fa-edit aria-hidden="true"></i>	
			//								</a>`
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
					"order": [[3, "desc"]],
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
				$('[data-toggle="tooltip"]').tooltip();
			});

		} else {

			$('#' + idTabla).DataTable().clear();
			$('#' + idTabla).DataTable().destroy();
			tbody.innerHTML = contenido;
			$('#' + idTabla).DataTable(
				{
					"order": [[3, "desc"]],
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

			$('#' + idTabla).on('draw', function () {
				$('[data-toggle="tooltip"]').tooltip();
			});
		}
	})

}



function pintarTableAsistencias (url, campos, propiedadId, nombreController, id = "tbDatos", idTabla = "table", propiedadMostrar = "") {
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

			contenido += "<tr>";
			for (var j = 0; j < campos.length; j++) {
				nombreCampo = campos[j];
				objetoActual = data[i];

				if (j == 0) {
					contenido += "<td style='display: none'>" + objetoActual[nombreCampo] + "</td>"
				} else {
					contenido += "<td>" + objetoActual[nombreCampo] + "</td>"
				}

			}

			contenido += `
						<td  style="justify-content: center; display: flex;>
							<div class="form-button-action"> `
			contenido += `<input type="radio"  class="form-radio-input" />`
			
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
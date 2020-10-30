

function mostrarModalConfirmacion(titulo, texto) {
		return 	Swal.fire({
				title: titulo,
				text: texto,
				icon: 'warning',
				showCancelButton: true,
				confirmButtonColor: '#3085d6',
				cancelButtonColor: '#d33',
				confirmButtonText: 'Si'
			})
}

function Imprimir() {
	//<table><thead></thead><tbody></tbody></table>
	var tcheck = document.getElementById("tcheck").outerHTML;
	var table = "<h1>Reporte a Imprimir</h1>"
	table += document.getElementById("table").outerHTML;
	table = table.replace(tcheck, "");
	var pagina = window.document.body;
	var ventana = window.open();
	ventana.document.write(table);
	ventana.print();
	ventana.close();
	window.document.body = pagina;
}

window.onload = function () {
	$(document).ready(function () {
		
		$('[data-toggle="tooltip"]').tooltip();
				
		$('#table').DataTable(
				{
					//"order": [[0, "desc"]],
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

		$(".only-numbers").keydown(function (event) {

			if (!event.shiftKey) {
				var k = Number(event.keyCode);
				if (48 <= k && k <= 57 || // numeros teclado normal
					96 <= k && k <= 105 || // numeros teclado numerico
					k == 8 || // backspace
					(37 <= k && k <= 40) || // flechitas
					k == 46 || // suprimir           
					k == 9)    // tab
					return true;
				return false;
			}
			return false;
		});


	});
}

function Preloader() {

	$.preloader.start({
		modal: true,
		//src: 'sprites2.png'
		src: 'sprites1.png'
	});

	setTimeout(function () {
		$.preloader.stop();
	}, 2200);
}


function ExportarExcel() {
	document.getElementById("tipoReporte").value = "Excel";
	var frmReporte = document.getElementById("frmReporte");
	frmReporte.submit();

}



function ExportarWord() {
	document.getElementById("tipoReporte").value = "Word";
	var frmReporte = document.getElementById("frmReporte");
	frmReporte.submit();
}

function ExportarPDF(nombreController) {
	/*
	document.getElementById("tipoReporte").value = "PDF";
	var frmReporte = document.getElementById("frmReporte");
	frmReporte.submit();
	*/
	var frm = new FormData();
	var checks = document.getElementsByName("nombrePropiedades");
	var nchecks = checks.length;
	for (var i = 0; i < nchecks; i++) {
		if (checks[i].checked == true) {
			frm.append("nombrePropiedades[]", checks[i].value);
		}
	}

	$.ajax({
		type: "POST",
		url: nombreController+"/exportarDatosPDF",
		data: frm,
		contentType: false,
		processData: false,
		success: function (data) {
			//base64 (Descargas ese base 64)
			var a = document.createElement("a");
			a.href = data;
			a.download = "reporte.pdf";
			a.click();
		}
	})


}


function pintar(url, campos, propiedadId, nombreController,
					popup = false, opciones = true, id = "tbDatos",idTabla="table",propiedadMostrar="") {
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
				//contenido += "<td>" + objetoActual[nombreCampo] + "</td>"

				if (j == 0) {
						contenido += "<td style='display: none'>" + objetoActual[nombreCampo] + "</td>"
					} else {
						contenido += "<td>" + objetoActual[nombreCampo] + "</td>"
					}
				}
			//contenido += "<td>" + data[i].iidespecialidad + "</td>";
			//contenido += "<td>" + data[i].nombre + "</td>";
			//contenido += "<td>" + data[i].descripcion + "</td>";
			if (opciones == true) {
				contenido += `
					<td>
						<div class="form-button-action"> `  
							
							contenido += ` <a data-toggle="tooltip" class="btn btn-link btn-lg btn-danger" data-original-title="Eliminar"
												onclick="Eliminar(${objetoActual[propiedadId]})">
												<i class="fa fa-times"></i>
												</a>`															
				if (popup == false) {
					contenido += `
						
							<a  
							   href="${nombreController}/Editar/${objetoActual[propiedadId]}" data-toggle="tooltip"
								class="btn btn-link btn-lg btn-primary" data-original-title="Editar" 
							  >
									<i class="fa fa-edit aria-hidden="true"></i>	
							</a>
						</div>	
                 `
				} else {

					contenido += `
	                    <i class="fa fa-edit btn btn-primary" aria-hidden="true"
						   data-toggle="modal"  data-target="#exampleModal"
						   onclick="Abrir(${objetoActual[propiedadId]})">

						</i>
					</div>

                        `;
				}
				contenido += `</td>`;
			} else {
				contenido += `
					<td>

						<i class="fa fa-check btn btn-success" aria-hidden="true"
						   onclick="AsignarNombre(${objetoActual[propiedadId]},
                              '${objetoActual[propiedadMostrar]}')">

						</i>
					</td>`;
			}
			contenido += "</tr>";
		}


		// tbody.innerHTML = contenido;
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
					}
					,
					drawCallback: function (settings) {
						//console.log('drawCallback');
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
						//console.log('drawCallback');
						$('[data-toggle="tooltip"]').tooltip();
					}
				}
			);

			$('#' + idTabla).on('draw', function () {
				//console.log('draw event');
				$('[data-toggle="tooltip"]').tooltip();
			});
		}
		
	})
}

var dataTable;
function pintarTableAsignaciones(url, campos, propiedadId, nombreController, id = "tbDatos", idTabla = "table", propiedadMostrar = "") {
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
			if (data[i].estadoAsignacionGrupo == 'A') {
				contenido += "<tr>";
				contenido += "<td>" + "<input type='checkbox' class='checkAsistencia' disabled  value='1'/>" + "</td>"
			}
			else {
				contenido += "<tr class='text-danger'>";
				contenido += "<td>" + "<input type='checkbox' style='margin: 14px;' class='checkAsistencia' id='chk" + data[i][propiedadId] +"'  value='"+ data[i][propiedadId]+"'/>" + "</td>"
			}

			//contenido += "<td>" + "<input type='checkbox' class='checkAsistencia'  value='1'/>" + "</td>"
			
			for (var j = 0; j < campos.length; j++) {
				nombreCampo = campos[j];
				objetoActual = data[i];
				//contenido += "<td>" + objetoActual[nombreCampo] + "</td>"
				 
				//contenido += "<td>" + "<input type='checkbox'  value='1'/>" + "</td>"

				if (j == 0) {
					
					contenido += "<td style='display: none'>" + objetoActual[nombreCampo] + "</td>"
				} else {
					
					contenido += "<td>" + objetoActual[nombreCampo] + "</td>"
				}

			}

			//contenido += `
			//				<td>
			//					<div class="form-button-action"> `

			//if (data[i].estadoAsignacionGrupo == 'A') {

			//	contenido += `<a  
			//					href="${nombreController}/Asignar/${objetoActual[propiedadId]}" data-toggle="tooltip" data-placement="top" data-original-title="Asignado"
			//					class="btn btn-link btn-lg btn-success disabled">
			//					<i class="fas fa-user-check"></i>	
			//					</a>`
			//} else {
			//	contenido += `<a type="button" 
			//					href="${nombreController}/Asignar/${objetoActual[propiedadId]}" data-toggle="tooltip"
			//					class="btn btn-link btn-lg btn-danger" data-original-title="Editar" >
			//					<i class="fas fa-user-edit"></i>	
			//					</a>`
			//}

			//contenido += ` </div> 
			//				</td>`;


			contenido += "</tr>";
		}
		
		//tbody.innerHTML = contenido;
		if (idTabla == null || idTabla == undefined || idTabla == "") {
			$('#table').DataTable().clear();
			$('#table').DataTable().destroy();
			tbody.innerHTML = contenido;
			dataTable =	$('#table').DataTable(				
				{
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
					"select": 'multi'
				}
				
			);
		} else {
			$('#' + idTabla).DataTable().clear();
			$('#' + idTabla).DataTable().destroy();
			tbody.innerHTML = contenido;
			dataTable = $('#' + idTabla).DataTable(
				{
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
						"search": "Buscar:",
						"zeroRecords": "Sin resultados encontrados",
						"paginate": {
							"first": "Primero",
							"last": "Ultimo",
							"next": "Siguiente",
							"previous": "Anterior"
						}
					},
					"select": 'multi'
				}
			);
		}
	})

}

// Handle click on "Select all" control
$('#example-select-all').on('click', function () {
	// Check/uncheck all checkboxes in the table
	var rows = dataTable.rows({ 'search': 'applied' }).nodes();
	$('input[type="checkbox"]', rows).prop('checked', this.checked);
});

// Handle click on checkbox to set state of "Select all" control
$('#table tbody').on('change', 'input[type="checkbox"]', function () {
	// If checkbox is not checked
	if (!this.checked) {
		var el = $('#example-select-all').get(0);
		// If "Select all" control is checked and has 'indeterminate' property
		if (el && el.checked && ('indeterminate' in el)) {
			// Set visual state of "Select all" control 
			// as 'indeterminate'
			el.indeterminate = true;
		}
	}
});



function pintarMultiplePopup(divTabla,url, cabeceras = ["Id","Nombre Completo"],
	campos, propiedadId,propiedadMostrar = "") {

	var contenido = "";
	$.get(url, function (data) {

		
		contenido += "<div class='table-responsive'>";
		//contenido += "<table id='tablaPopup' class='display table table-bordered table-striped table-hover blueTable'>";
		contenido += "<table id='tablaPopup' class='table blueTable table-bordered table-hover'>";
		contenido += "<thead>";
		contenido += "<tr>";
		for (var i = 0; i < cabeceras.length; i++) {
			contenido += "<th>" + cabeceras[i] + "</th>"
		}
		contenido += "<th></th>";
		contenido += "</tr>";
		contenido += "</thead>";
		contenido += "<tbody>";
		var objetoActual;
		for (var i = 0; i < data.length; i++) {
			contenido += "<tr>";
			for (var j = 0; j < campos.length; j++) {
				nombreCampo = campos[j];
				objetoActual = data[i];
				contenido += "<td>" + objetoActual[nombreCampo] + "</td>"
			}

			contenido += `
					<td>

						<i style='padding: 3px;' class='fa fa-check btn btn-success' aria-hidden="true"
						   onclick="AsignarNombre(${objetoActual[propiedadId]},
                              '${objetoActual[propiedadMostrar]}')">

						</i>
					</td>`;

			contenido += "</tr>";
		}
		contenido += "</tbody>";
		contenido += "</table>";
		contenido += "</div>";
		document.getElementById(divTabla).innerHTML = contenido;
		$('#tablaPopup').DataTable({
			"searching": false, 
			"info": false,
			"lengthChange": false 
		});
	});
	
	
}

function errorHtml(msje) {

	Swal.fire({
		icon: 'error',
		title: 'ERROR',
		html: msje
	});
}


function correcto(title) {
	
	Swal.fire({
		position: 'top-end',
		icon: 'success',
		title: title,
		showConfirmButton: false,
		timer: 3500
	})
}

function error(title) {

	Swal.fire({
		icon: 'error',
		title: 'ERROR',
		text: title
		
	})
}


function advertencia(title) {

	Swal.fire({
		icon: 'warning',
		title: 'ADVERTENCIA',
		text: title
	})
}


function LimpiarDatos() {
	var controles = document.getElementsByClassName("form-control");
	var ncontroles = controles.length;
	for (var i = 0; i < ncontroles; i++) {
		controles[i].value = "";
	}
}

function obj(id, valor) {
	document.getElementById(id).value = valor;
}

function enviar(frm) {
	var controles = document.getElementsByClassName("enviar");
	var ncontroles = controles.length;
	for (var i = 0; i < ncontroles; i++) {
		frm.append(controles[i].name, controles[i].value);
	}
}
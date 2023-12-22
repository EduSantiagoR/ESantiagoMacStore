$(document).ready(function () {
    GetAll();
});

function GetAll() {
    var id = sessionStorage.getItem('IdUsuario');
    if (id != null) {
        $.ajax({
            url: 'http://localhost:50621/api/usuario',
            type: 'GET',
            success: function (result) {
                $('#tableBody').empty();
                $.each(result.Objects, function (i, Usuario) {
                    var filas = '<tr>'
                        + '<td>'
                        + '<button onclick="ModalOpen(' + Usuario.IdUsuario + ')" class="btn btn-warning">' + '</button>'
                        + '</td>'
                        + '<td>' + Usuario.Nombre + '</td>'
                        + '<td>' + Usuario.ApellidoPaterno + '</td>'
                        + '<td>' + Usuario.ApellidoMaterno + '</td>'
                        + '<td>' + Usuario.Edad + '</td>'
                        + '<td>' + Usuario.Email + '</td>'
                        + '<td>'
                        + '<button onclick="Delete(' + Usuario.IdUsuario + ')" class="btn btn-danger">' + '</button>'
                        + '</td>'
                        + '</tr>';
                    $('#tableBody').append(filas);
                });
            },
            error: function (result) {
                alert(result.Message);
            }
        });
    }
    else {
        alert('Debes iniciar sesión para acceder a las funciones del sistema.');
    }
}

//Abrir modal y cargar información
function ModalOpen(idUsuario) {
    var id = sessionStorage.getItem('IdUsuario');
    if (id != null) {
        if (parseInt(idUsuario) != 0) {
            $.ajax({
                url: 'http://localhost:50621/api/usuario/' + parseInt(idUsuario),
                type: 'GET',
                success: function (result) {
                    var { IdUsuario, Nombre, ApellidoPaterno, ApellidoMaterno, Edad, Email, Password } = result.Object;

                    $('#txtIdUsuario').val(IdUsuario);
                    $('#txtNombre').val(Nombre);
                    $('#txtApellidoPaterno').val(ApellidoPaterno);
                    $('#txtApellidoMaterno').val(ApellidoMaterno);
                    $('#txtEdad').val(Edad);
                    $('#txtEmail').val(Email);
                    $('#txtPassword').val(Password);
                },
                error: function (result) {
                    alert(result.Message);
                }
            });
        }
        else {
            CleanModal();
        }
        $('#myModal').modal('show');
    }
    else {
        alert('No tienes permitida esta acción, inicia sesión primero.');
    }
    
}

//Limpiar modal
function CleanModal() {
    $('#txtIdUsuario').val(0);
    $('#txtNombre').val('');
    $('#txtApellidoPaterno').val('');
    $('#txtApellidoMaterno').val('');
    $('#txtEdad').val('');
    $('#txtEmail').val('');
    $('#txtPassword').val('');
}

//Cerrar modales
function ModalClose() {
    $('#myModal').modal('hide');
    $('#modalAviso').modal('hide');
}

//Guarda los cambios o agrega nuevo usuario
function Save() {
    var Usuario = {
        IdUsuario: parseInt($('#txtIdUsuario').val()),
        Nombre: $('#txtNombre').val(),
        ApellidoPaterno: $('#txtApellidoPaterno').val(),
        ApellidoMaterno: $('#txtApellidoMaterno').val(),
        Edad: $('#txtEdad').val(),
        Email: $('#txtEmail').val(),
        Password: $('#txtPassword').val()
    };
    if (Usuario.IdUsuario == 0) {
        $.ajax({
            url: 'http://localhost:50621/api/usuario',
            type: 'POST',
            dataType: 'json',
            data: Usuario,
            success: function (result) {
                document.getElementById('txtMessage').innerHTML = result.Message;
                GetAll();
            },
            error: function (result) {
                document.getElementById('txtMessage').innerHTML = result.Message;
            }
        });
    }
    else {
        $.ajax({
            url: 'http://localhost:50621/api/usuario/' + Usuario.IdUsuario,
            type: 'PUT',
            dataType: 'json',
            data: Usuario,
            success: function (result) {
                document.getElementById('txtMessage').innerHTML = result.Message;
                GetAll();
            },
            error: function (result) {
                document.getElementById('txtMessage').innerHTML = result.Message;
            }
        });
    }
    $('#modalAviso').modal('show');
}

function Delete(idUsuario) {
    if (confirm('¿Estás seguro que quieres eliminar este usuario?')) {
        $.ajax({
            url: 'http://localhost:50621/api/usuario/' + parseInt(idUsuario),
            type: 'DELETE',
            success: function (result) {
                document.getElementById('txtMessage').innerHTML = result.Message;
                GetAll();
            },
            error: function (result) {
                document.getElementById('txtMessage').innerHTML = result.Message;
            }
        });
        $('#modalAviso').modal('show');
    }
}
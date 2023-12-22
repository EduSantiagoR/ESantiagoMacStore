
function Login() {
    var txtemail = document.getElementById('txtEmail').value;
    var txtpassword = document.getElementById('txtPassword').value;
    $.ajax({
        url: 'http://localhost:50621/api/usuario/login/' + txtemail + '/' + txtpassword,
        type: 'POST',
        success: function (result) {
            if (result.Correct == true) {
                var { IdUsuario, Nombre } = result.Object;
                document.getElementById('txtMessage').innerHTML = 'Acceso concedido. :) Bienvenido, ' + Nombre;
                document.getElementById('btnContinuar').setAttribute('href', 'http://localhost:50536/Usuario/GetAll');
                sessionStorage.setItem('IdUsuario', IdUsuario);
                $('#myModal').modal('show');
            }
        },
        error: function (result) {
            document.getElementById('txtMessage').innerHTML = 'Acceso denegado. :(';
            $('#myModal').modal('show');
        }
    });
}

function modalClose() {
    $('#myModal').modal('hide');
}
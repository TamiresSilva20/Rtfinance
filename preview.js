
function previewImagem() {
    var preview = document.getElementById('imgPalestra');
    var file = document.getElementById('fileImagem').files[0];
    var reader = new FileReader();

    reader.onloadend = function () {
        preview.src = reader.result;
        preview.style.display = "block";
    }

    if (file) {
        reader.readAsDataURL(file);
    } else {
        preview.src = "";
    }
}

function mostrarCampos() {
    document.getElementById('imgPalestra').style.display = 'block';
    document.getElementById('fileImagem').style.display = 'block';
    document.getElementById('txtTitulo').style.display = 'block';
    document.getElementById('txtDescricao').style.display = 'block';
    document.getElementById('txtData').style.display = 'block';
    document.getElementById('txtHora').style.display = 'block';
    document.getElementById('txtLocal').style.display = 'block';
    document.getElementById('CreateButton').style.display = 'block';
    document.getElementById('lblMensagem').style.display = 'block';
    document.getElementById('btnOculta').style.display = 'block';
}
function ocultarCampos() {
    document.getElementById('imgPalestra').style.display = 'none';
    document.getElementById('fileImagem').style.display = 'none';
    document.getElementById('txtTitulo').style.display = 'none';
    document.getElementById('txtDescricao').style.display = 'none';
    document.getElementById('txtData').style.display = 'none';
    document.getElementById('txtHora').style.display = 'none';
    document.getElementById('txtLocal').style.display = 'none';
    document.getElementById('CreateButton').style.display = 'none';
    document.getElementById('lblMensagem').style.display = 'none';
}

function mostrarEditar() {
    document.getElementById('imgPalestra').style.display = 'block';
    document.getElementById('fileImagem').style.display = 'block';
    document.getElementById('txtTitulo').style.display = 'block';
    document.getElementById('txtDescricao').style.display = 'block';
    document.getElementById('txtData').style.display = 'block';
    document.getElementById('txtHora').style.display = 'block';
    document.getElementById('txtLocal').style.display = 'block';
    document.getElementById('CreateButton').style.display = 'none';
    document.getElementById('Salvar').style.display = 'block';
    document.getElementById('lblMensagem').style.display = 'block';
    document.getElementById('btnOculta').style.display = 'block';
}
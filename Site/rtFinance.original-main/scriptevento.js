$(document).ready(function () {
    function carregarEventos() {
        $.ajax({
            type: "GET",
            url: "EventosHandler.ashx",
            dataType: "json",
            success: function (response) {
                var eventosHtml = "";
                response.forEach(function (evento) {
                    eventosHtml += "<li>" + evento.Titulo + "<br>" +
                        "<span><img src='data:image/jpeg;base64," + evento.ImagemBase64 + "'/></span><br>" + // Exibe a imagem
                        "<span id='dt'>Data: " + evento.Data + "<span/><br>" + "<span id='dt'>Hora:" + evento.Hora +
                        "<span/><br>" + "Local: " + evento.Local + "</li><br>";
                });
                $('#lista-eventos').html(eventosHtml);
            },
            error: function (xhr, status, error) {
                console.error("Erro ao carregar os eventos:", error);
            }
        });
    }

    carregarEventos();
});
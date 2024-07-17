$(document).ready(function () {
    $.get("../assets/components/header-admin.html", function (data) {
        $("#header-admin").replaceWith(data);

        const accessToken = localStorage.getItem('access');
        if (accessToken) {
            $('#menu-logout').removeClass('d-none').addClass('d-block');
        } else {
            $('#menu-logout').removeClass('d-block').addClass('d-done');
        }

        $('#menu-logout').on('click', function () {
            localStorage.clear();
            location.href = '/index.html';
        });

    }).fail(function () {
        console.error('Error al cargar el encabezado');
    }).always(function () {
        $('#loading-indicator').fadeOut();
    });
});
$(document).ready(function () {
    $.get("../assets/components/header-admin.html", function (data) {
        $("#header-admin").replaceWith(data);

        const accessToken = localStorage.getItem('access');
        if (accessToken) {
            $('#menu-wishlist').removeClass('d-none').addClass('d-block');
            $('#menu-my-purchases').removeClass('d-none').addClass('d-block');
            $('#menu-logout').removeClass('d-none').addClass('d-block');
            $('#menu-login').removeClass('d-block').addClass('d-none');
        } else {
            $('#menu-wishlist').removeClass('d-block').addClass('d-none');
            $('#menu-my-purchases').removeClass('d-block').addClass('d-none');
            $('#menu-logout').removeClass('d-block').addClass('d-done');
            $('#menu-login').removeClass('d-done').addClass('d-block');
        }

        $('#menu-logout').on('click', function () {
            localStorage.clear();
            location.href = 'index.html';
        });

    }).fail(function () {
        console.error('Error al cargar el encabezado');
    }).always(function () {
        $('#loading-indicator').fadeOut();
    });
});
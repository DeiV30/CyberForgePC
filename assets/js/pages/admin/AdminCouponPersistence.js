$(document).ready(function () {

    function getQueryParam(param) {
        const urlParams = new URLSearchParams(window.location.search);
        return urlParams.get(param);
    }

    const couponId = getQueryParam('item');

    if (couponId) {
        $('#coupon-title-form').text('Actualizar Cupon');

        makeAuthenticatedRequest({
            method: 'GET',
            url: `${API_URL}/coupon/${couponId}`
        }).then(response => {
            const coupon = response.data.data;
            if (coupon) {
                $('#code').val(coupon.code);
                $('#discount').val(coupon.discount);
                $('#expirationDate').val(coupon.expirationDate.split('T')[0]);
            } else {
                showNotFound();
            }
        }).catch(error => {
            handleRequestError(error);
        });
    }

    $('#coupon-form').validate({
        rules: {
            'code': {
                required: true,
                minlength: 3
            },
            discount: {
                required: true,
            },
            expirationDate: {
                required: true,
            },
        },
        messages: {
            'name': {
                required: "Por favor, introduce el código del cupon",
                minlength: "El codigo del cupon debe tener al menos 3 caracteres"
            },
            discount: {
                required: "Por favor, introduce el descuento",
            },
            expirationDate: {
                required: "Por favor, introduce la fecha de expiración",
            },
        },
        errorClass: 'is-invalid',
        validClass: 'is-valid',
        errorElement: 'div',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.mb-3').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass(errorClass).removeClass(validClass);
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass(errorClass).addClass(validClass);
        },
        submitHandler: function (form) {

            const couponData = {
                code: $('#code').val(),
                discount: parseFloat($('#discount').val()),
                expirationDate: $('#expirationDate').val()
            };

            const method = couponId ? 'PUT' : 'POST';
            const url = couponId ? `${API_URL}/coupon/${couponId}` : `${API_URL}/coupon`;

            makeAuthenticatedRequest({
                method: method,
                url: url,
                data: couponData
            }).then(function (response) {
                Swal.fire({
                    icon: 'success',
                    title: '¡Cupon guardado con éxito!',
                    text: 'Tu cupon ha sido guardada.',
                    confirmButtonText: 'OK'
                }).then((result) => {
                    if (result.isConfirmed) {
                        window.location.href = '/pages/coupon-list.html';
                    }
                });
            }).catch(function (error) {
                handleRequestError(error);
            });

        }
    });

    function showNotFound() {
        Swal.fire({
            icon: 'error',
            title: 'Cupon no encontrado',
            text: 'El cupon especifico no existe.'
        }).then(() => {
            // window.location.href = '/pages/coupon-list.html';
        });
    }

});
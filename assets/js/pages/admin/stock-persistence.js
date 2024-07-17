$(document).ready(function () {

    function getQueryParam(param) {
        const urlParams = new URLSearchParams(window.location.search);
        return urlParams.get(param);
    }

    const productId = getQueryParam('item');

    makeAuthenticatedRequest({
        method: 'GET',
        url: `${API_URL}/product/${productId}`
    }).then(response => {

        const product = response.data.data;
        if (product) {
            $('#name').val(product.name);
            $('#stock').html(`Stock: ${product.stock}`);
        }

    }).catch(error => {
        handleRequestError(error);
    });

    $('#product-form').validate({
        rules: {
            'name': {
                required: true,
                minlength: 3
            },
            stockupdate: {
                required: true,
                number: true,
                min: 0
            }
        },
        messages: {
            'name': {
                required: "Por favor, introduce el nombre del producto",
                minlength: "El nombre del producto debe tener al menos 3 caracteres"
            },
            stockupdate: {
                required: "Por favor, introduce la cantidad en stock",
                number: "Por favor, introduce un número válido",
                min: "El stock debe ser un número positivo"
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
            const productData = {
                Quantity: parseInt($('#stockupdate').val()),
            };

            makeAuthenticatedRequest({
                method: 'PUT',
                url: `${API_URL}/product/${productId}/stock`,
                data: productData
            }).then(function (response) {
                Swal.fire({
                    icon: 'success',
                    title: '¡Producto actualizado con éxito!',
                    text: 'Tu inventario ha sido actualizado.',
                    confirmButtonText: 'OK'
                }).then((result) => {
                    if (result.isConfirmed) {

                        window.location.href = '/admin/stock-list.html';
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
            title: 'Producto no encontrado',
            text: 'El producto especifico no existe.'
        }).then(() => {
            window.location.href = '/admin/product-list.html';
        });
    }

});
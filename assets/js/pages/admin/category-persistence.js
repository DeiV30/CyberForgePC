$(document).ready(function () {

    function getQueryParam(param) {
        const urlParams = new URLSearchParams(window.location.search);
        return urlParams.get(param);
    }

    const categoryId = getQueryParam('item');

    if (categoryId) {
        $('#category-title-form').text('Editar Categoría');

        makeAuthenticatedRequest({
            method: 'GET',
            url: `${API_URL}/category/${categoryId}`
        }).then(response => {
            const category = response.data.data;
            if (category) {
                $('#name').val(category.name);
                $('#description').val(category.description);
            } else {
                showNotFound();
            }
        }).catch(error => {
            handleRequestError(error);
        });
    }

    $('#category-form').validate({
        rules: {
            'name': {
                required: true,
                minlength: 3
            },
            description: {
                required: true,
                minlength: 2
            },
        },
        messages: {
            'name': {
                required: "Por favor, introduce el nombre de la categoría",
                minlength: "El nombre de la categoría debe tener al menos 3 caracteres"
            },
            description: {
                required: "Por favor, introduce la descripción de la categoría",
                minlength: "La descripción debe tener al menos 10 caracteres"
            }
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

            const categoryData = {
                name: $('#name').val(),
                description: $('#description').val(),
                isActive: parseFloat($('#isActive').val())
            };

            const method = categoryId ? 'PUT' : 'POST';
            const url = categoryId ? `${API_URL}/category/${categoryId}` : `${API_URL}/category`;

            makeAuthenticatedRequest({
                method: method,
                url: url,
                data: categoryData
            }).then(function (response) {
                Swal.fire({
                    icon: 'success',
                    title: '¡Categoría guardada con éxito!',
                    text: 'Tu categoría ha sido guardada.',
                    confirmButtonText: 'OK'
                }).then((result) => {
                    if (result.isConfirmed) {
                        window.location.href = '/admin/category-list.html';
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
            title: 'Categoría no encontrada',
            text: 'La categoría especificada no existe.'
        }).then(() => {
            // window.location.href = '/pages/category-list.html';
        });
    }

});
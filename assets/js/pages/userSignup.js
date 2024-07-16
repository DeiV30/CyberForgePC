$(document).ready(function () {

    $.validator.addMethod("customEmail", function (value, element) {
        return this.optional(element) || /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/i.test(value);
    }, "Por favor, introduce una dirección de correo electrónico válida.");

    $('#user-form').validate({
        rules: {
            'name': {
                required: true,
                minlength: 3
            },
            email: {
                required: true,
                customEmail: true,
            },
            password: {
                required: true,
                minlength: 4
            }
        },
        messages: {
            'name': {
                required: "Por favor, introduce el nombre del producto",
                minlength: "El nombre del producto debe tener al menos 3 caracteres"
            },
            email: {
                required: "Por favor, introduce tu dirección de correo electrónico",
                email: "Por favor, introduce una dirección de correo electrónico válida"
            },
            password: {
                required: "Por favor, introduce tu contraseña",
                minlength: "Tu contraseña debe tener al menos 6 caracteres"
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
        submitHandler: function () {

            const formData = {
                name: $('#name').val(),
                email: $('#email').val(),
                password: $('#password').val()
            };

            makeAnonymousRequest({
                method: 'POST',
                url: `${API_URL}/user`,
                data: formData
            }).then(function (response) {
                Swal.fire({
                    icon: 'success',
                    title: 'Genial!',
                    text: 'Tu usuario ha sido registrado. Seras redirigido al inicio de sesión',
                    confirmButtonText: 'OK'
                }).then((result) => {
                    if (result.isConfirmed) {
                        window.location.href = '/login.html';
                    }
                });
            }).catch(function (error) {
                handleRequestError(error);
            });

        }
    });

});
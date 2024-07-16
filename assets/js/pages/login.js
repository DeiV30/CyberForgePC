$(document).ready(function () {

    $.validator.addMethod("customEmail", function (value, element) {
        return this.optional(element) || /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/i.test(value);
    }, "Por favor, introduce una dirección de correo electrónico válida.");

    $('#login-form').validate({
        rules: {
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
        submitHandler: function (form) {
            $('#error-message').addClass('d-none').text('');

            let formData = {
                email: $('#email').val(),
                password: $('#password').val()
            }

            makeAnonymousRequest({
                method: 'POST',
                url: `${API_URL}/authentication/loginemail`,
                data: formData
            }).then(function (response) {

                const decodedToken = jwt_decode(response.data.data.accessToken);

                const token = response.data.data.accessToken;
                const refreshtoken = response.data.data.refreshToken;
                const userRole = decodedToken.role;
                const userId = decodedToken.nameid;

                localStorage.setItem('access', token);
                localStorage.setItem('refresh', refreshtoken);
                localStorage.setItem('userRole', userRole);
                localStorage.setItem('userId', userId);

                const role = localStorage.getItem('userRole');

                if (role === 'Admin') {
                    window.location.href = '/dashboard.html';
                } else {
                    window.location.href = '/products.html';
                }

            }).catch(function (error) {
                handleRequestError(error);
            });
        }
    });
});
$(document).ready(function () {

    function loadCategories() {
        return new Promise((resolve, reject) => {
            makeAuthenticatedRequest({
                method: 'GET',
                url: `${API_URL}/category`
            }).then(response => {
                const categories = response.data.data;
                const categorySelect = $('#category');
                categorySelect.empty();
                categories.forEach(function (category) {
                    const option = `<option value="${category.id}">${category.name}</option>`;
                    categorySelect.append(option);
                });
                resolve();
            }).catch(error => {
                handleRequestError(error);
                reject(error);
            });
        });
    }

    function getProduct(productId) {
        return new Promise((resolve, reject) => {
            makeAuthenticatedRequest({
                method: 'GET',
                url: `${API_URL}/product/${productId}`
            }).then(response => {
                const product = response.data.data;
                if (product) {
                    resolve(product);
                }
            }).catch(error => {
                showNotFound();
                reject(error);
            });
        });
    }

    function initializeProductForm(product) {
        $('#name').val(product.name);
        $('#description').val(product.description);
        $('#price').val(product.price);
        $('#stock').val(product.stock);
        $('#category').val(product.category.id);
        $('#image-preview').attr('src', product.image);
    }

    function getQueryParam(param) {
        const urlParams = new URLSearchParams(window.location.search);
        return urlParams.get(param);
    }

    const productId = getQueryParam('item');

    if (productId) {
        $('#product-title-form').text('Actualizar Producto');


        loadCategories().then(() => {
            return getProduct(productId);
        }).then(product => {
            initializeProductForm(product);
        }).catch(error => {
            Swal.fire({
                icon: 'error',
                title: 'General',
                text: 'Error al carga el producto o categoría.'
            });
        });
    } else {
        loadCategories();
    }

    $('#image').on('change', function () {
        const file = this.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                $('#image-preview').attr('src', e.target.result).show();
            }
            reader.readAsDataURL(file);
        } else {
            $('#image-preview').hide();
        }
    });

    $('#product-form').validate({
        rules: {
            'name': {
                required: true,
                minlength: 3
            },
            description: {
                required: true,
                minlength: 10
            },
            price: {
                required: true,
                number: true,
                min: 0
            },
            stock: {
                required: true,
                number: true,
                min: 0
            },
            category: {
                required: true
            },
            image: {
                extension: "jpg|jpeg|png"
            }
        },
        messages: {
            'name': {
                required: "Por favor, introduce el nombre del producto",
                minlength: "El nombre del producto debe tener al menos 3 caracteres"
            },
            description: {
                required: "Por favor, introduce la descripción del producto",
                minlength: "La descripción debe tener al menos 10 caracteres"
            },
            price: {
                required: "Por favor, introduce el precio del producto",
                number: "Por favor, introduce un número válido",
                min: "El precio debe ser un número positivo"
            },
            stock: {
                required: "Por favor, introduce la cantidad en stock",
                number: "Por favor, introduce un número válido",
                min: "El stock debe ser un número positivo"
            },
            category: {
                required: "Por favor, selecciona una categoría"
            },
            image: {
                extension: "Por favor, sube un archivo de imagen válido (jpg, jpeg, png)"
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

            const formData = new FormData(form);
            const file = $('#image')[0].files[0];
            let uploadImagePromise;

            if (file) {
                formData.append('image', file);
                uploadImagePromise = makeAuthenticatedRequest({
                    method: 'POST',
                    url: `${API_URL}/images/upload`,
                    data: formData,
                    headers: {
                        'Content-Type': 'multipart/form-data'
                    }
                }).then(response => response.data.url);
            } else {
                uploadImagePromise = Promise.resolve($('#image-preview').attr('src'));
            }

            uploadImagePromise.then(response => {
                const imageUrl = response;

                const productData = {
                    name: $('#name').val(),
                    description: $('#description').val(),
                    price: parseFloat($('#price').val()),
                    stock: parseInt($('#stock').val()),
                    categoryId: $('#category').val(),
                    image: imageUrl
                };

                const method = productId ? 'PUT' : 'POST';
                const url = productId ? `${API_URL}/product/${productId}` : `${API_URL}/product`;

                makeAuthenticatedRequest({
                    method: method,
                    url: url,
                    data: productData
                }).then(function (response) {
                    Swal.fire({
                        icon: 'success',
                        title: '¡Producto guardado con éxito!',
                        text: 'Tu producto ha sido guardado.',
                        confirmButtonText: 'OK'
                    }).then((result) => {
                        if (result.isConfirmed) {

                            window.location.href = '/admin/product-list.html';
                        }
                    });
                }).catch(function (error) {
                    handleRequestError(error);
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
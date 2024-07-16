$(window).on('load', function () {
    function addToCart(product) {
        const cart = JSON.parse(localStorage.getItem('cart')) || [];
        const existingProduct = cart.find(item => item.id === product.id);

        if (existingProduct) {
            existingProduct.quantity += 1;
        } else {
            cart.push({
                id: product.id,
                name: product.name,
                price: product.price,
                image: product.image,
                quantity: 1
            });
        }

        localStorage.setItem('cart', JSON.stringify(cart));
        updateCartBadge();
    }

    function updateCartBadge() {
        const cart = JSON.parse(localStorage.getItem('cart')) || [];
        const totalItems = cart.reduce((total, item) => total + item.quantity, 0);
        $('#cart-badge').text(totalItems);
    }

    updateCartBadge();

    $('.add-to-cart').on('click', function () {
        const productId = $(this).data('id');
        makeAnonymousRequest({
            method: 'GET',
            url: `${API_URL}/product/${productId}`
        }).then(response => {
            const product = response.data.data;
            addToCart({
                id: product.id,
                name: product.name,
                price: product.price,
                image: product.image
            });
            Swal.fire({
                icon: 'success',
                title: '¡Añadido al carrito!',
                text: 'El producto ha sido añadido a tu carrito.',
                timer: 1500,
                showConfirmButton: false
            });
        }).catch(error => {
            handleRequestError(error);
        });
    });


    $('.wishList-add').on('click', function () {
        const userId = localStorage.getItem('userId');

        if (!userId) {
            Swal.fire(
                '¡Oops!',
                'Debes estar autenticado para agregar productos a favoritos.',
                'warning'
            );
            return;
        }

        const wishListdata = {
            userId: userId,
            productId: $(this).data('id')
        };

        makeAuthenticatedRequest({
            method: 'POST',
            url: `${API_URL}/wishlist`,
            data: wishListdata
        }).then(response => {
            Swal.fire(
                '¡Genial!',
                'Tu producto ha sido guardado en tus favoritos.',
                'success'
            ).then(() => {
                location.reload();
            });
        }).catch(error => {
            handleRequestError(error);
        });
    });



});

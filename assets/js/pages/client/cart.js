$(document).ready(function () {

    const targetNode = document.body;
    const config = { childList: true, subtree: true };

    const callback = function (mutationsList, observer) {
        for (const mutation of mutationsList) {
            if (mutation.type === 'childList') {
                if ($('#cart-badge').length > 0) {
                    updateCartBadge();
                    observer.disconnect();
                }
            }
        }
    };

    const observer = new MutationObserver(callback);
    observer.observe(targetNode, config);

    function updateCartBadge() {
        const cart = JSON.parse(localStorage.getItem('cart')) || [];
        const totalItems = cart.reduce((total, item) => total + item.quantity, 0);
        $('#cart-badge').text(totalItems);
    }

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


    $('.container-cart').on('click', '.add-to-cart', function () {
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

    $('.container-cart').on('click', '.wishList-add', function () {
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

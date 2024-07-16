$(document).ready(function () {
    let totalAmount = 0;

    function loadCartItems() {
        const cart = JSON.parse(localStorage.getItem('cart')) || [];
        const cartItemsContainer = $('#cart-items');
        const cartTotalContainer = $('#cart-total');

        cartItemsContainer.empty();
        cartTotalContainer.empty();

        if (cart.length === 0) {
            cartItemsContainer.append('<p>Tu carrito esta vacío.</p>');
            return;
        } else {
            cart.forEach(item => {
                totalAmount += item.price * item.quantity;

                const cartItem = `
                <li>
                    <div class="d-flex justify-content-between align-items-lg-center mb-4">
                    <img src="${item.image}" class="object-fit-cover border rounded" style="width:130px;height: 90px;" />
                        <div class="d-flex">
                            <div class="px-4">
                                <h3 class="mb-0">${item.name}</h3>
                                <p class="mb-0">$${formatNumberWithCommas(item.price)}</p>
                                <small>${item.quantity}</small>
                            </div>
                        </div>
                        <div><a class="btn btn-primary remove-from-cart" data-id="${item.id}"><svg xmlns="http://www.w3.org/2000/svg" viewBox="-32 0 512 512" width="1em" height="1em" fill="currentColor">
                                    <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free (Icons: CC BY 4.0, Fonts: SIL OFL 1.1, Code: MIT License) Copyright 2023 Fonticons, Inc. -->
                                    <path d="M64 80c-8.8 0-16 7.2-16 16V416c0 8.8 7.2 16 16 16H384c8.8 0 16-7.2 16-16V96c0-8.8-7.2-16-16-16H64zM0 96C0 60.7 28.7 32 64 32H384c35.3 0 64 28.7 64 64V416c0 35.3-28.7 64-64 64H64c-35.3 0-64-28.7-64-64V96zM152 232H296c13.3 0 24 10.7 24 24s-10.7 24-24 24H152c-13.3 0-24-10.7-24-24s10.7-24 24-24z"></path>
                                </svg></a></div>
                    </div>
                </li>
            `;
                cartItemsContainer.append(cartItem);
            });

            updateTotalDisplay(totalAmount);

            cartItemsContainer.on('click', '.remove-from-cart', function () {
                const productId = $(this).data('id');
                removeFromCart(productId);
                loadCartItems();
            });
        }
    }

    function applyCoupon(couponCode) {
        makeAuthenticatedRequest({
            method: 'GET',
            url: `${API_URL}/coupon/${couponCode}`,
        }).then(response => {
            const discount = response.data.data.discount;
            const discountedTotal = totalAmount - (totalAmount * (discount / 100));
            updateTotalDisplay(discountedTotal);
            totalAmount = discountedTotal;
            Swal.fire({
                icon: 'success',
                title: 'Cupón aplicado!',
                text: `Se ha aplicado un descuento del ${discount}%.`,
                timer: 1500,
                showConfirmButton: false
            });

            $('#cart-coupon').removeClass('d-none').addClass('d-block');
            $('#cart-coupon').html(`Cupon: ${discount}%`);
            $('#apply-coupon').prop('disabled', true);
            $('#coupon-code').prop('disabled', true);
        }).catch(error => {
            handleRequestError(error);
        });
    }

    $('#apply-coupon').on('click', function () {
        const couponCode = $('#coupon-code').val();
        if (couponCode) {
            applyCoupon(couponCode);
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Cupón inválido',
                text: 'Por favor, introduce un código de cupón válido.'
            });
        }
    });


    $('#place-order').on('click', function () {
        const cart = JSON.parse(localStorage.getItem('cart')) || [];
        if (cart.length === 0) {
            Swal.fire({
                icon: 'error',
                title: 'Carrito vacío',
                text: 'No hay productos en el carrito para realizar un pedido.'
            });
            return;
        }

        const orderData = {
            items: cart,
            totalAmount: totalAmount
        };

        Swal.fire({
            icon: 'warning',
            title: 'Pedido',
            text: '¿Estas seguro de realizar el pedido?',
            confirmButtonText: 'OK'
        }).then((result) => {
            if (result.isConfirmed) {
                makeAuthenticatedRequest({
                    method: 'POST',
                    url: `${API_URL}/order`,
                    data: orderData
                }).then(response => {
                    window.location.href = '/resume.html';
                }).catch(error => {
                    handleRequestError(error);
                });
            }
        });

    });


    loadCartItems();


    function updateTotalDisplay(total) {
        $('#cart-total').html(`<p>Total: $${formatNumberWithCommas(total)}</p>`);
    }

    function removeFromCart(productId) {
        let cart = JSON.parse(localStorage.getItem('cart')) || [];
        cart = cart.filter(item => item.id !== productId);
        localStorage.setItem('cart', JSON.stringify(cart));
        updateCartBadge();
    }

    function updateCartBadge() {
        const cart = JSON.parse(localStorage.getItem('cart')) || [];
        const totalItems = cart.reduce((total, item) => total + item.quantity, 0);
        $('#cart-badge').text(totalItems);
    }

});
$(document).ready(function () {
    const userId = localStorage.getItem('userId');

    makeAuthenticatedRequest({
        method: 'GET',
        url: `${API_URL}/wishlist/${userId}`
    }).then(response => {
        const wishlist = response.data.data;
        renderWishList(wishlist);
    }).catch(error => {
        handleRequestError(error);
    });

    function renderWishList(wishlist) {
        const wishlistList = $('#wishlist-list');
        wishlistList.empty();

        wishlist.forEach(item => {
            const productCard = `
                <div class="col mb-4">
                    <div><a href="#"><img class="rounded img-fluid shadow w-100 fit-cover" src="${item.product.image}" style="height: 250px;"></a>
                        <div class="py-4"><span class="badge bg-primary mb-2">${item.product.category.name}</span>
                            <h4 class="fw-bold">${item.product.name}</h4>
                            <p class="text-muted line-clamp">${item.product.description}</p>                                  
                            <div class="btn-group" role="group"><a class="btn btn-primary add-to-cart" data-id="${item.product.id}"><svg class="bi bi-cart3" xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" fill="currentColor" viewBox="0 0 16 16">
                                <path d="M0 1.5A.5.5 0 0 1 .5 1H2a.5.5 0 0 1 .485.379L2.89 3H14.5a.5.5 0 0 1 .49.598l-1 5a.5.5 0 0 1-.465.401l-9.397.472L4.415 11H13a.5.5 0 0 1 0 1H4a.5.5 0 0 1-.491-.408L2.01 3.607 1.61 2H.5a.5.5 0 0 1-.5-.5M3.102 4l.84 4.479 9.144-.459L13.89 4H3.102zM5 12a2 2 0 1 0 0 4 2 2 0 0 0 0-4m7 0a2 2 0 1 0 0 4 2 2 0 0 0 0-4m-7 1a1 1 0 1 1 0 2 1 1 0 0 1 0-2m7 0a1 1 0 1 1 0 2 1 1 0 0 1 0-2"></path>
                            </svg></a><a class="btn btn-primary wishList-delete" data-id="${item.id}"><svg xmlns="http://www.w3.org/2000/svg" viewBox="-32 0 512 512" width="1em" height="1em" fill="currentColor">
                                <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free (Icons: CC BY 4.0, Fonts: SIL OFL 1.1, Code: MIT License) Copyright 2023 Fonticons, Inc. -->
                                <path d="M64 80c-8.8 0-16 7.2-16 16V416c0 8.8 7.2 16 16 16H384c8.8 0 16-7.2 16-16V96c0-8.8-7.2-16-16-16H64zM0 96C0 60.7 28.7 32 64 32H384c35.3 0 64 28.7 64 64V416c0 35.3-28.7 64-64 64H64c-35.3 0-64-28.7-64-64V96zM152 232H296c13.3 0 24 10.7 24 24s-10.7 24-24 24H152c-13.3 0-24-10.7-24-24s10.7-24 24-24z"></path>
                            </svg></a></div>

                        </div>
                    </div>
                </div>
                `;
            wishlistList.append(productCard);
        });

        $('.wishList-delete').on('click', function () {
            const wishListId = $(this).data('id');
            Swal.fire({
                title: '¿Estás seguro?',
                text: '¡No podrás revertir esto!',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Si, Borrarlo'
            }).then((result) => {
                if (result.isConfirmed) {
                    makeAuthenticatedRequest({
                        method: 'DELETE',
                        url: `${API_URL}/wishlist/${wishListId}`
                    }).then(response => {
                        Swal.fire(
                            '¡Eliminado!',
                            'Tu producto ha sido eliminado de tus favoritos.',
                            'success'
                        ).then(() => {
                            location.reload();
                        });
                    }).catch(error => {
                        handleRequestError(error);
                    });
                }
            });
        });

    }

});
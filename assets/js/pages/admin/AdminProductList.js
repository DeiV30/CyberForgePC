$(document).ready(function () {

    makeAnonymousRequest({
        method: 'GET',
        url: `${API_URL}/product`
    }).then(response => {
        const products = response.data.data;
        renderProducts(products);
    }).catch(error => {
        handleRequestError(error);
    });

    function renderProducts(products) {
        const productList = $('#product-list');
        productList.empty();

        products.forEach(item => {

            let ribbon = '';
            if (item.deleteKey !== null) {
                ribbon = `<div class="ribbon-1 left p-2">Producto eliminado</div>`;
            }

            let handler = '';
            if (item.deleteKey == null) {
                handler = ` <div class="btn-group" role="group"><a class="btn btn-primary" href="productPersistence.html?item=${item.id}"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" width="1em" height="1em" fill="currentColor">
                                        <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free (Icons: CC BY 4.0, Fonts: SIL OFL 1.1, Code: MIT License) Copyright 2023 Fonticons, Inc. -->
                                        <path d="M441 58.9L453.1 71c9.4 9.4 9.4 24.6 0 33.9L424 134.1 377.9 88 407 58.9c9.4-9.4 24.6-9.4 33.9 0zM209.8 256.2L344 121.9 390.1 168 255.8 302.2c-2.9 2.9-6.5 5-10.4 6.1l-58.5 16.7 16.7-58.5c1.1-3.9 3.2-7.5 6.1-10.4zM373.1 25L175.8 222.2c-8.7 8.7-15 19.4-18.3 31.1l-28.6 100c-2.4 8.4-.1 17.4 6.1 23.6s15.2 8.5 23.6 6.1l100-28.6c11.8-3.4 22.5-9.7 31.1-18.3L487 138.9c28.1-28.1 28.1-73.7 0-101.8L474.9 25C446.8-3.1 401.2-3.1 373.1 25zM88 64C39.4 64 0 103.4 0 152V424c0 48.6 39.4 88 88 88H360c48.6 0 88-39.4 88-88V312c0-13.3-10.7-24-24-24s-24 10.7-24 24V424c0 22.1-17.9 40-40 40H88c-22.1 0-40-17.9-40-40V152c0-22.1 17.9-40 40-40H200c13.3 0 24-10.7 24-24s-10.7-24-24-24H88z"></path>
                                    </svg></a><a class="btn btn-primary product-delete" data-id="${item.id}"><svg xmlns="http://www.w3.org/2000/svg" viewBox="-32 0 512 512" width="1em" height="1em" fill="currentColor">
                                        <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free (Icons: CC BY 4.0, Fonts: SIL OFL 1.1, Code: MIT License) Copyright 2023 Fonticons, Inc. -->
                                        <path d="M64 80c-8.8 0-16 7.2-16 16V416c0 8.8 7.2 16 16 16H384c8.8 0 16-7.2 16-16V96c0-8.8-7.2-16-16-16H64zM0 96C0 60.7 28.7 32 64 32H384c35.3 0 64 28.7 64 64V416c0 35.3-28.7 64-64 64H64c-35.3 0-64-28.7-64-64V96zM152 232H296c13.3 0 24 10.7 24 24s-10.7 24-24 24H152c-13.3 0-24-10.7-24-24s10.7-24 24-24z"></path>
                                    </svg></a></div>`;
            }

            const productCard = `
                <div class="col mb-4">                    
                    <div class="position-relative overflow-hidden">
                    <img class="rounded img-fluid shadow w-100 fit-cover" src="${item.image}" style="height: 250px;">
                     ${ribbon}
                    </div>
                        <div class="py-4"><span class="badge bg-primary mb-2">${item.category.name}</span>
                            <h4 class="fw-bold">${item.name}</h4>
                            <p class="text-muted line-clamp">${item.description}</p>               
                            ${handler}
                        </div>                    
                </div>
                `;
            productList.append(productCard);
        });


        $('.product-delete').on('click', function () {
            const productId = $(this).data('id');
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
                        url: `${API_URL}/product/${productId}`
                    }).then(response => {
                        Swal.fire(
                            '¡Eliminado!',
                            'Tu producto ha sido eliminado.',
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
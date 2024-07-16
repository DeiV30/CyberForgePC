$(document).ready(function () {

    function getQueryParam(param) {
        const urlParams = new URLSearchParams(window.location.search);
        return urlParams.get(param);
    }

    const productId = getQueryParam('item');

    makeAnonymousRequest({
        method: 'GET',
        url: `${API_URL}/product/${productId}`
    }).then(response => {
        const product = response.data.data;

        if (product) {
            renderProductDetail(product);
        } else {
            renderProductNotFound();
        }
    }).catch(error => {
        handleRequestError(error);
    });

    function renderProductDetail(product) {
        const productDetail = $('#product-detail');
        productDetail.empty();

        const productCard = `
                <div class="col mb-5"><img class="rounded img-fluid shadow object-fit-cover" src="${product.image}" style="width:100%; height:300px"/></div>
                <div class="col d-md-flex align-items-md-end align-items-lg-center mb-5">
                    <div><a class="btn btn-primary btn-sm mb-3 wishList-add" data-id="${product.id}"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" width="1em" height="1em" fill="currentColor">
                                <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free (Icons: CC BY 4.0, Fonts: SIL OFL 1.1, Code: MIT License) Copyright 2023 Fonticons, Inc. -->
                                <path d="M225.8 468.2l-2.5-2.3L48.1 303.2C17.4 274.7 0 234.7 0 192.8v-3.3c0-70.4 50-130.8 119.2-144C158.6 37.9 198.9 47 231 69.6c9 6.4 17.4 13.8 25 22.3c4.2-4.8 8.7-9.2 13.5-13.3c3.7-3.2 7.5-6.2 11.5-9c0 0 0 0 0 0C313.1 47 353.4 37.9 392.8 45.4C462 58.6 512 119.1 512 189.5v3.3c0 41.9-17.4 81.9-48.1 110.4L288.7 465.9l-2.5 2.3c-8.2 7.6-19 11.9-30.2 11.9s-22-4.2-30.2-11.9zM239.1 145c-.4-.3-.7-.7-1-1.1l-17.8-20c0 0-.1-.1-.1-.1c0 0 0 0 0 0c-23.1-25.9-58-37.7-92-31.2C81.6 101.5 48 142.1 48 189.5v3.3c0 28.5 11.9 55.8 32.8 75.2L256 430.7 431.2 268c20.9-19.4 32.8-46.7 32.8-75.2v-3.3c0-47.3-33.6-88-80.1-96.9c-34-6.5-69 5.4-92 31.2c0 0 0 0-.1 .1s0 0-.1 .1l-17.8 20c-.3 .4-.7 .7-1 1.1c-4.5 4.5-10.6 7-16.9 7s-12.4-2.5-16.9-7z"></path>
                            </svg></a>
                        <h3 class="fw-bold">${product.name}</h3>
                        <h5>${product.category.name}</h5>
                        <p class="text-muted mb-0">${product.description}</p>
                        <h1 class="position-relative d-table my-3">$${formatNumberWithCommas(product.price)}</h1>
                        <a class="btn btn-primary add-to-cart" data-id="${product.id}">Añadir al carrito</a>                        
                    </div>
                </div>
                `;

        productDetail.append(productCard);
    }

    function renderProductNotFound() {
        const productDetail = $('#product-detail');
        productDetail.empty();

        const notFoundMessage = `
          <div class="alert alert-warning w-100" role="alert"><svg class="me-2" xmlns="http://www.w3.org/2000/svg" viewBox="-64 0 512 512" width="1em" height="1em" fill="currentColor">
        <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free (Icons: CC BY 4.0, Fonts: SIL OFL 1.1, Code: MIT License) Copyright 2023 Fonticons, Inc. -->
        <path d="M297.2 248.9C311.6 228.3 320 203.2 320 176c0-70.7-57.3-128-128-128S64 105.3 64 176c0 27.2 8.4 52.3 22.8 72.9c3.7 5.3 8.1 11.3 12.8 17.7l0 0c12.9 17.7 28.3 38.9 39.8 59.8c10.4 19 15.7 38.8 18.3 57.5H109c-2.2-12-5.9-23.7-11.8-34.5c-9.9-18-22.2-34.9-34.5-51.8l0 0 0 0c-5.2-7.1-10.4-14.2-15.4-21.4C27.6 247.9 16 213.3 16 176C16 78.8 94.8 0 192 0s176 78.8 176 176c0 37.3-11.6 71.9-31.4 100.3c-5 7.2-10.2 14.3-15.4 21.4l0 0 0 0c-12.3 16.8-24.6 33.7-34.5 51.8c-5.9 10.8-9.6 22.5-11.8 34.5H226.4c2.6-18.7 7.9-38.6 18.3-57.5c11.5-20.9 26.9-42.1 39.8-59.8l0 0 0 0 0 0c4.7-6.4 9-12.4 12.7-17.7zM192 128c-26.5 0-48 21.5-48 48c0 8.8-7.2 16-16 16s-16-7.2-16-16c0-44.2 35.8-80 80-80c8.8 0 16 7.2 16 16s-7.2 16-16 16zm0 384c-44.2 0-80-35.8-80-80V416H272v16c0 44.2-35.8 80-80 80z"></path>
        </svg><span>Producto no encontrado.</span></div>
        `;

        productDetail.append(notFoundMessage);
    }

});
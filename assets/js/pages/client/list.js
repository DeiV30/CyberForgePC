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
                handler = ` <div class="btn-group pb-4" role="group"><a class="btn btn-primary wishList-add" data-id="${item.id}"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" width="1em" height="1em" fill="currentColor">
                                    <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free (Icons: CC BY 4.0, Fonts: SIL OFL 1.1, Code: MIT License) Copyright 2023 Fonticons, Inc. -->
                                    <path d="M225.8 468.2l-2.5-2.3L48.1 303.2C17.4 274.7 0 234.7 0 192.8v-3.3c0-70.4 50-130.8 119.2-144C158.6 37.9 198.9 47 231 69.6c9 6.4 17.4 13.8 25 22.3c4.2-4.8 8.7-9.2 13.5-13.3c3.7-3.2 7.5-6.2 11.5-9c0 0 0 0 0 0C313.1 47 353.4 37.9 392.8 45.4C462 58.6 512 119.1 512 189.5v3.3c0 41.9-17.4 81.9-48.1 110.4L288.7 465.9l-2.5 2.3c-8.2 7.6-19 11.9-30.2 11.9s-22-4.2-30.2-11.9zM239.1 145c-.4-.3-.7-.7-1-1.1l-17.8-20c0 0-.1-.1-.1-.1c0 0 0 0 0 0c-23.1-25.9-58-37.7-92-31.2C81.6 101.5 48 142.1 48 189.5v3.3c0 28.5 11.9 55.8 32.8 75.2L256 430.7 431.2 268c20.9-19.4 32.8-46.7 32.8-75.2v-3.3c0-47.3-33.6-88-80.1-96.9c-34-6.5-69 5.4-92 31.2c0 0 0 0-.1 .1s0 0-.1 .1l-17.8 20c-.3 .4-.7 .7-1 1.1c-4.5 4.5-10.6 7-16.9 7s-12.4-2.5-16.9-7z"></path>
                                </svg></a><a class="btn btn-primary add-to-cart" data-id="${item.id}"><svg class="bi bi-cart3" xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" fill="currentColor" viewBox="0 0 16 16">
                                    <path d="M0 1.5A.5.5 0 0 1 .5 1H2a.5.5 0 0 1 .485.379L2.89 3H14.5a.5.5 0 0 1 .49.598l-1 5a.5.5 0 0 1-.465.401l-9.397.472L4.415 11H13a.5.5 0 0 1 0 1H4a.5.5 0 0 1-.491-.408L2.01 3.607 1.61 2H.5a.5.5 0 0 1-.5-.5M3.102 4l.84 4.479 9.144-.459L13.89 4H3.102zM5 12a2 2 0 1 0 0 4 2 2 0 0 0 0-4m7 0a2 2 0 1 0 0 4 2 2 0 0 0 0-4m-7 1a1 1 0 1 1 0 2 1 1 0 0 1 0-2m7 0a1 1 0 1 1 0 2 1 1 0 0 1 0-2"></path>
                                </svg></a></div>`;
            }

            const productCard = `
                <div class="col mb-4">          
                     <a href="/detail.html?item=${item.id}">          
                    <div class="position-relative overflow-hidden">
                        <img class="rounded img-fluid shadow w-100 fit-cover" src="${item.image}" style="height: 250px;">
                        ${ribbon}
                    </div>
                        <div class="pt-4"><span class="badge bg-primary mb-2">${item.category.name}</span>
                            <h4 class="fw-bold">${item.name}</h4>
                            <p class="text-muted line-clamp">${item.description}</p>                                          
                        </div>    
                    </a>                
                    ${handler}
                </div>
                `;
            productList.append(productCard);
        });

    }

});
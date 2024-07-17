$(document).ready(function () {

    const userId = localStorage.getItem('userId');

    makeAuthenticatedRequest({
        method: 'GET',
        url: `${API_URL}/order/${userId}`
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
            const product = `
                <tr>
                            <td>${item.id}</td>
                            <td>${formatNumberWithCommas(item.subTotal)}</td>
                            <td>${item.coupon.discount}%</td>
                            <td>${formatNumberWithCommas(item.total)}</td>                            
                            <td>
                                <div class="btn-group" role="group"> <button class="btn btn-primary btn-sm view-order-items" data-order-id="${item.id}" data-bs-toggle="modal" data-bs-target="#orderItemsModal"><svg class="bi bi-search" xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" fill="currentColor" viewBox="0 0 16 16">
    <path d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001c.03.04.062.078.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1.007 1.007 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0"></path>
</svg></button></div>
                            </td>
                            </tr>
                `;
            productList.append(product);
        });

        $('.view-order-items').on('click', function () {
            const orderId = $(this).data('order-id');

            makeAuthenticatedRequest({
                method: 'GET',
                url: `${API_URL}/order/${orderId}/orderitem`
            }).then(response => {
                const orderItems = response.data.data;
                renderOrderItems(orderItems);
                $('#orderItemsModal').modal('show');
            }).catch(error => {
                handleRequestError(error);
            });

        });
    }

    function renderOrderItems(orderItems) {
        const orderItemsList = $('#orderItemsList');
        orderItemsList.empty();

        orderItems.forEach(item => {
            const orderItem = `
                <tr>                    
                    <td>${item.productName}</td>
                    <td>${item.productCategory}</td>
                    <td>${item.quantity}</td>
                    <td>${formatNumberWithCommas(item.price)}</td>
                </tr>
            `;
            orderItemsList.append(orderItem);
        });
    }

    // <th>Id</th>
    // <th>Nombre</th>
    // <th>Categoría</th>
    // <th>Cantidad</th>
    // <th>Precio</th>

});
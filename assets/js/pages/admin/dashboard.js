$(document).ready(function () {

    makeAuthenticatedRequest({
        method: 'GET',
        url: `${API_URL}/layout`
    }).then(response => {
        const count = response.data.data;
        renderDashboard(count);
    }).catch(error => {
        handleRequestError(error);
    });

    function renderDashboard(count) {
        $('#total-order').text(count.totalOrder);
        $('#total-user').text(count.totalUser);
    }


    makeAuthenticatedRequest({
        method: 'GET',
        url: `${API_URL}/layout/inventory`
    }).then(response => {
        const list = response.data.data;
        renderInventory(list);
    }).catch(error => {
        handleRequestError(error);
    });


    function renderInventory(list) {
        const itemsList = $('#iventory-list');
        itemsList.empty();

        list.forEach(item => {
            const inventoryList = `
                <tr>                    
                    <td>${item.productName}</td>
                    <td>${(item.transactionType.trim() == 'IN') ? '<b class="text-success">Entrada</b>' : '<b class="text-danger">Salida</b>'}</td>
                    <td>${(item.transactionType.trim() == 'IN') ? "+" + item.quantity : "-" + item.quantity}</td>
                    <td>${formatDateShort(new Date(item.created))}</td>
                </tr>
            `;
            itemsList.append(inventoryList);
        });
    }



});
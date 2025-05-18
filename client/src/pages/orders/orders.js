import * as http from '../../lib/helpers/httpClient.js';

const customerSelect = document.querySelector('#customerId');
const orderList = document.querySelector('#orders');
const orderItemsContainer = document.querySelector('#order-items');
const addItemBtn = document.querySelector('#add-item');
const totalPriceDisplay = document.querySelector('#total-price');
const submitOrderBtn = document.querySelector('#submit-order');
const form = document.querySelector('#form');

let products = [];

const initApp = () => {
    loadOrders();
    loadCustomers();
    loadProducts();
};

if (submitOrderBtn) {
    submitOrderBtn.addEventListener('click', submitOrder);
}

const createHtml = (order) => {
    const li = document.createElement('li');
    li.classList.add('card');

    const date = order.orderDate.split('T')[0];

    li.innerHTML = `
        <p>Ordernummer: <span>${order.orderId}</span></p>
        <p>Kund: <span>${order.customerName}</span></p>
        <p>Datum: <span>${date}</span></p>
    `;

    return li;
};
const loadOrders = async () => {
    const result = await http.get('orders');
    console.log(result);

    result.data.forEach((order) => {
        console.log(order);
        orderList.appendChild(createHtml(order));
    });
};


const loadCustomers = async () => {
    try {
        const response = await http.get('customers');
        const customers = await response.data;

        customers.forEach(customer => {
            const option = document.createElement('option');
            option.value = customer.id;
            option.textContent = customer.name;
            customerSelect.appendChild(option);
        });
    } catch (error) {
        console.error('Kunde inte hämta kunder:', error);
    }
};

const loadProducts = async () => {
    try {
        const response = await http.get('products');
        products = await response.data;
        console.log('Produkter laddade:', products);
    } catch (error) {
        console.error('Kunde inte hämta produkter', error);
    }
};

addItemBtn.addEventListener('click', () => {
    addOrderItemRow();
    calculateTotalPrice();
});

const addOrderItemRow = () => {
    const row = document.createElement('div');
    row.classList.add('order-item');

    const productSelect = document.createElement('select');
    productSelect.innerHTML = '<option value="">-- Välj produkt --</option>';
    products.forEach(product => {
        const option = document.createElement('option');
        option.value = product.id;
        option.textContent = product.name;
        productSelect.appendChild(option);
    });

    const quantityInput = document.createElement('input');
    quantityInput.type = 'number';
    quantityInput.min = '1';
    quantityInput.value = '1';

    const priceSpan = document.createElement('span');
    priceSpan.textContent = 'price';

    const quantityPerPackSpan = document.createElement('span');
    quantityPerPackSpan.textContent = '';

    const lineTotalSpan = document.createElement('span');
    lineTotalSpan.classList.add('line-total');
    lineTotalSpan.textContent = ' ';

    const updateLineTotal = () => {
        const selectedProduct = products.find(p => p.id == productSelect.value);

        if (!selectedProduct) {
            lineTotalSpan.textContent = '';
            return;
        }
        const price = selectedProduct.pricePerUnit;
        const quantityPerPack = selectedProduct.quantityPerPack;
        const packs = parseInt(quantityInput.value);

        const lineTotal = price * quantityPerPack * packs;
        lineTotalSpan.textContent = `${lineTotal.toFixed(2)} kr`;
        calculateTotalPrice();
    };

    productSelect.addEventListener('change', () => {
        const selectedProduct = products.find(p => p.id == productSelect.value);
        console.log('Vald produkt:', selectedProduct);
        if (selectedProduct) {
            priceSpan.textContent = selectedProduct.pricePerUnit.toFixed(2);
            quantityPerPackSpan.textContent = `${selectedProduct.quantityPerPack} st/förpackning`;
        } else {
            priceSpan.textContent = '0.00';
            quantityPerPackSpan.textContent = '';
        }
        updateLineTotal();
    });

    quantityInput.addEventListener('input', updateLineTotal);

    row.appendChild(productSelect);
    row.appendChild(quantityInput);
    row.appendChild(quantityPerPackSpan);
    row.appendChild(document.createTextNode(' '));
    row.appendChild(lineTotalSpan);

    orderItemsContainer.appendChild(row);
};

const calculateTotalPrice = () => {
    const lineTotals = orderItemsContainer.querySelectorAll('.line-total');
    let total = 0;

    lineTotals.forEach(span => {
        const lineTotal = parseFloat(span.textContent);
        if (!isNaN(lineTotal)) {
            total += lineTotal;
        }
    });

    totalPriceDisplay.textContent = total.toFixed(2);
};

async function submitOrder() {
    const orderItems = [];
    const rows = orderItemsContainer.querySelectorAll('.order-item');

    rows.forEach(row => {
        const productsSelect = row.querySelector('select');
        const quantityInput = row.querySelector('input');

        const selectedProduct = products.find(p => p.id == productsSelect.value);
        const quantity = parseInt(quantityInput.value);

        if (selectedProduct) {
            orderItems.push({
                productId: selectedProduct.id,
                productName: selectedProduct.name,
                price: selectedProduct.pricePerUnit,
                quantity: quantity,
                lineTotal: selectedProduct.pricePerUnit * selectedProduct.quantityPerPack * quantity
            });
        }
    });

    const orderData = {
        customerId: customerSelect.value,
        orderItems: orderItems,
    };
    console.log('Skickar till API:', JSON.stringify(orderData, null, 2));

    try {
        const result = await http.post('orders', orderData);
        if (result.success) {
            alert('Ordern har lagts till!');
        } else {
            alert('Det gick inte att lägga till ordern');
        }

        form.reset();
    } catch (error) {
        console.log(error);
        alert('Ett fel uppstod när ordern skulle sparas');
    }
};

document.addEventListener('DOMContentLoaded', initApp);
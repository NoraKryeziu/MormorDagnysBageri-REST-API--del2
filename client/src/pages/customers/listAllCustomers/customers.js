import * as http from '../../../lib/helpers/httpClient.js';

const customerList = document.querySelector('#customers');

const initApp = () => {
    loadCustomers();
};

const loadCustomers = async() => {
    const result = await http.get('customers');

    result.data.forEach(customer => 
        customerList.appendChild(createHtml(customer))
    );
};

const createHtml = (customer) => {
    const li = document.createElement('li');
    li.classList.add('card');

    let html = `
        <p>Namn: <span>${customer.name}</span></p>
        <p>E-post: <span>${customer.email}</span></p>
        <p>Telefon: <span>${customer.phone}</span></p>
    `;

    li.innerHTML = html;
    return li;
};


document.addEventListener('DOMContentLoaded', initApp);
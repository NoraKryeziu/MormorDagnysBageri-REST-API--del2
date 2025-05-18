import * as http from '../../lib/helpers/httpClient.js';

const supplierList = document.querySelector('#suppliers');

const initApp = () => {
    loadSuppliers();
};

const loadSuppliers = async() => {
    const result = await http.get('suppliers');
    
    result.data.forEach(supplier => 
        supplierList.appendChild(createHtml(supplier))
    );
};

const createHtml = (supplier) => {
    const li = document.createElement('li');
    li.classList.add('card');

    let html = `
        <p>Namn: <span>${supplier.name}</span></p>
        <p>E-post: <span>${supplier.email}</span></p>
        <p>Telefon: <span>${supplier.phone}</span></p>
    `;

    li.innerHTML = html;
    return li;
};

document.addEventListener('DOMContentLoaded', initApp);
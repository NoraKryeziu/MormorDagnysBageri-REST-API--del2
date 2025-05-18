import * as http from '../../../lib/helpers/httpClient.js';

const productList = document.querySelector('#products');
const initApp = () => {
  loadProducts();
};

const loadProducts = async () => {
  const result = await http.get('products');
  console.log(result);

  result.data.forEach((product) =>{
    console.log(product);
    productList.appendChild(createHtml(product));
  });
};
const createHtml = (product) => {
  const li = document.createElement('li');
  li.classList.add('card');

  li.innerHTML = `
        <p>Produkt: <span>${product.name}</span></p>
        <p>Artikelnummer: <span>${product.itemNumber}</span></p>
        <p>
            Styckpris: 
            <span class="price">${product.pricePerUnit.toFixed(2)}</span> kr
            <button class="editBtn" title="Redigera pris">✏️</button>
        </p>
        <div class="editArea" style="display: none;">
            <input type="number" step="0.01" min="0" value="${product.pricePerUnit}" class="priceInput" />
            <button class="saveBtn">Spara</button>
            <button class="cancelBtn">Avbryt</button>
        </div>        
        <p>En förpackning innehåller: <span>${product.quantityPerPack}</span> st.</p>
    `;
    const editBtn = li.querySelector('.editBtn');
    const editArea = li.querySelector('.editArea');
    const saveBtn = li.querySelector('.saveBtn');
    const cancelBtn = li.querySelector('.cancelBtn');
    const priceInput = li.querySelector('.priceInput');
    const priceDisplay = li.querySelector('.price');

    editBtn.addEventListener('click', () => {
        editArea.style.display = 'block';
        editBtn.style.display = 'none';
    });

    cancelBtn.addEventListener('click', () => {
        editArea.style.display = 'none';
        editBtn.style.display = 'inline';
        priceInput.value = priceDisplay.textContent;
    });

    saveBtn.addEventListener('click', async () => {
        const newPrice = parseFloat(priceInput.value);
        if (isNaN(newPrice) || newPrice <= 0) {
            alert('Ange ett giltigt pris.');
            return;
        }

        console.log('skickar Patch anrop till servern')

        try {
            console.log('Produkt ID:', product.id)
            await patchProductPrice(product.id, newPrice);
            priceDisplay.textContent = newPrice.toFixed(2);
            editArea.style.display = 'none';
            editBtn.style.display = 'inline';
            alert('Priset har uppdaterats!');
        } catch (error) {
            console.error(error);
            alert('Kunde inte uppdatera priset.');
        }
    });
    return li;
};

const patchProductPrice = async (productId, newPrice) => {
    const response = await http.patch(`products/${productId}/price`, {
        pricePerUnit: newPrice
    });
    console.log(response);
    return response;
};

document.addEventListener('DOMContentLoaded', initApp);


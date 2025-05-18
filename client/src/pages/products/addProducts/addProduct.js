import * as http from '../../../lib/helpers/httpClient.js';

const form = document.querySelector('#form');

form.addEventListener('submit', async (e) => {
    e.preventDefault();

    const product = {
        name: form.name.value,
        itemNumber: form.itemNumber.value,
        pricePerUnit: parseFloat(form.pricePerUnit.value),
        quantityPerPack: parseInt(form.quantityPerPack.value),
    };

    try {

        const result = await http.post('products', product);
        console.log('Product added:', result);

        form.reset();
        alert('Produkten har lagts till!');
        
    } catch (error) {
        console.log(error);
        alert('Ett fel uppstod när produkten skulle sparas');
    }
});
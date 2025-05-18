import * as http from '../../../lib/helpers/httpClient.js';

const form = document.querySelector('form');

form.addEventListener('submit', async (e) => {
    e.preventDefault();

    const customer = {
        name: form.name.value,
        email: form.email.value,
        phone: form.phone.value,
        contactPerson: form.contactPerson.value,
        addresses:[{
            addressLine: form.addressLine.value,
            postalCode: form.postalCode.value,
            city: form.city.value,
            addressType: form.addressType.value
        }]
    };

    console.log('Skickar till API:', JSON.stringify(customer, null, 2));

    try {
        const result = await http.post('customers', customer);
        console.log('Customer added:', result);

        if(result === true){
            console.log('Customer added:', result);
            
            alert('Kunden har lagts till!');
        } else {
            console.error('Kunde inte lägga till kund:', result);
            alert('Ett fel uppstod när kunden skulle sparas');
        }
        form.reset();
    } catch (error) {
        console.log(error);
        alert('Ett fel uppstod när kunden skulle sparas');
    }
});

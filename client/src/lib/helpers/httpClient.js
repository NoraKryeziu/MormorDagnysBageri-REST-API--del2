import { config } from './config.js';


export const get = async (endpoint) => {
  const url = `${config.apiUrl}/${endpoint}`;
  try {
    const response = await fetch(url);

    if (response.ok) {
      return await response.json();
    } else {
      throw new Error(
        `Det gick inte så bra ${response.status}, ${response.statusText}`
      );
    }
  } catch (error) {
    console.error(error);
  }
};

export const post = async (endpoint, object) => {
  try {
    const response = await fetch(`${config.apiUrl}/${endpoint}`, {
      method: 'POST',
      headers: {
        'x-apikey': config.apiKey,
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(object),
    });

    if (response.ok) {
      const data = response.status === 201 ? true : await response.json();
      return data;
      
    } else {
      throw new Error(
        `Något gick fel: ${response.status} - ${response.statusText}`
      );
    }
  } catch (error) {
    console.error(error.message);
  }
};

export const patch = async (endpoint, body) => {
  const url = `${config.apiUrl}/${endpoint}`;
  try {
    const response = await fetch(url, {
      method: 'PATCH',
      headers: {
        'x-api-key': config.apiKey,
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(body),
    });

    if (response.ok) {
      return await response.json();
    } else {
      throw new Error(
        `Patch misslyckades: ${response.status} - ${response.statusText}`
      );
    }
  } catch (error) {
    console.error(error.message);
  }

};

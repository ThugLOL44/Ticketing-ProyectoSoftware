const API_BASE_URL = 'http://localhost:5158';

async function getEvents(page, pageSize) {
    const response = await fetch(`${API_BASE_URL}/api/v1/events?page=${page}&pageSize=${pageSize}`);    
    if (!response.ok) {
        throw new Error('Error al obtener eventos');
    }
    
    return response.json();
}
const API_BASE_URL = 'http://localhost:5158';


async function getSeats(eventId) {
    const response = await fetch(`${API_BASE_URL}/api/v1/events/${eventId}/seats`);
    if (!response.ok) throw new Error('Error al obtener las butacas');
    return response.json();
}


async function createReservation(seatId, userId) {
    const response = await fetch(`${API_BASE_URL}/api/v1/reservations`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ seatId, userId })
    });

    if (!response.ok) {
        const error = await response.json();
        throw new Error(error.error || 'Error al crear la reserva');
    }

    return response.json();
}
async function getEvents(page, pageSize) {
    const response = await fetch(`${API_BASE_URL}/api/v1/events?page=${page}&pageSize=${pageSize}`);    
    if (!response.ok) {
        throw new Error('Error al obtener eventos');
    }
    
    return response.json();
}

async function confirmAllPayments(reservationIds) {
    const response = await fetch(`${API_BASE_URL}/api/v1/payments`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ reservationIds })
    });
    if (!response.ok) {
        const error = await response.json();
        throw new Error(error.message || 'Error al procesar el pago');
    }
    return response.json();
}